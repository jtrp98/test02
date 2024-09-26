using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using FingerprintPayment.Card.CsCode;
using System.Web.WebPages.Html;
using SelectListItem = System.Web.WebPages.Html.SelectListItem;
using System.Windows.Media.TextFormatting;
using System.Globalization;
using FingerprintPayment.Handles.Products;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System.Net;

namespace FingerprintPayment.Card.PermissionCard
{
    public partial class Report1 : CardGateway
    {

        public class Search
        {
            public int? type1 { get; set; }
            public int? type2 { get; set; }
            public DateTime? date { get; set; }
            public DateTime? month { get; set; }
            public string term { get; set; }
            public int? level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
        }

        public List<SelectListItem> ListYear { get; set; }
        public List<SelectListItem> ListTerm { get; set; }
        public List<SelectListItem> ListGroup { get; set; }
        public List<SelectListItem> ListLevel1 { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialControl();
        }
        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                string yearID = "";
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();

                if (term != null)
                {
                    yearID = term.nYear + "";
                }

                ListYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false)
                    .OrderByDescending(x => x.numberYear)
                     .AsEnumerable()
                        .Select(o => new SelectListItem
                        {
                            Selected = false,
                            Text = o.numberYear + "",
                            Value = o.nYear + "",
                        })
                    .ToList();

                foreach (var l in ListYear)
                {
                    if (l.Value == yearID)
                    {
                        l.Selected = true;
                        yearID = l.Value;
                    }

                    if (yearID == "") yearID = l.Value;

                }

                if (yearID != "")
                {
                    ListTerm = en.TTerms
                        .Where(w => w.SchoolID == schoolID && w.nYear + "" == yearID && w.cDel == null)
                        .OrderByDescending(o => o.nTerm)
                        .AsEnumerable()
                        .Select(o => new SelectListItem
                        {
                            Selected = false,
                            Text = o.sTerm,
                            Value = o.nTerm.Trim()
                        })
                        .ToList();

                    foreach (var l in ListTerm)
                    {
                        if (l.Value == currentTerm)
                        {
                            l.Selected = true;
                        }
                    }
                }

                ListGroup = en.TPermissionCardType.Where(o => o.SchoolID == schoolID && o.IsDel == false).AsEnumerable()
                  .Select(o => new SelectListItem
                  {
                      Selected = false,
                      Value = o.ID + "",
                      Text = o.Permission,
                  })
                  .ToList();

                ListLevel1 = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).AsEnumerable()
                  .Select(o => new SelectListItem
                  {
                      Selected = false,
                      Value = o.nTSubLevel + "",
                      Text = o.SubLevel,
                  })
                  .ToList();
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new CardsRepository(ctx);
                var sl = new StudentLogic(ctx);
                TTerm term = new TTerm();

                DateTime? start = null, end = null;
                string termId = "";
                switch (search.type2)
                {
                    case 1:
                        term = sl.GetTermDATA(search.date.Value, userData);
                        termId = term.nTerm;
                        break;
                    case 2:
                        term = sl.GetTermDATA(search.month.Value, userData);
                        termId = term.nTerm;
                        break;
                    case 3:
                        term = ctx.TTerms.AsNoTracking().FirstOrDefault(o => o.nTerm == search.term);
                        start = term.dStart;
                        end = term.dEnd;
                        termId = term.nTerm;
                        break;
                    default:
                        termId = search.term;
                        break;
                }
                var result = logic.LoadPermissionCardReport(userData.CompanyID, search.date, search.month, termId, search.level1, search.level2, search.name, start, end);

                return new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.PID,
                        o.sID,
                        Number = o.Number.HasValue ? o.Number + "" : "-",
                        o.Code,
                        o.FullName,
                        o.Room,
                        o.Term,
                        Created = o.Created.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                        StartDate = o.StartDate.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")),
                        o.Cause,
                        o.GroupName,
                        o.RefNo,
                        o.Employee,
                        Files = o.AttachUrl?.Split(','),
                    })
                };

            }

        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new CardsRepository(ctx);
                var sl = new StudentLogic(ctx);
                TTerm term = new TTerm();

                DateTime? start = null, end = null;
                string termId = "";
                switch (search.type2)
                {
                    case 1:
                        term = sl.GetTermDATA(search.date.Value, userData);
                        termId = term.nTerm;
                        break;
                    case 2:
                        term = sl.GetTermDATA(search.month.Value, userData);
                        termId = term.nTerm;
                        break;
                    case 3:
                        term = ctx.TTerms.AsNoTracking().FirstOrDefault(o => o.nTerm == search.term);
                        start = term.dStart;
                        end = term.dEnd;
                        termId = term.nTerm;
                        break;
                    default:
                        termId = search.term;
                        break;
                }
                var result = logic.LoadPermissionCardReport(userData.CompanyID, search.date, search.month, termId, search.level1, search.level2, search.name, start, end);

                var data = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.PID,
                    o.sID,
                    Number = o.Number.HasValue ? o.Number + "" : "-",
                    o.Code,
                    o.FullName,
                    o.Room,
                    o.Term,
                    Created = o.Created.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                    StartDate = o.StartDate.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")),
                    o.Cause,
                    o.GroupName,
                    o.RefNo,
                    o.Employee,
                    Files = o.AttachUrl?.Split(','),
                });

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานประวัติการขออนุญาต");

                    var worksheet = excel.Workbook.Worksheets["รายงานประวัติการขออนุญาต"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    //using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    //{
                    //    var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                    //    worksheet.Cells[row, 1, row++, 10].SetCellRange(isMerge: true, text: school.sCompany, fontSize: 16, isBold: true);
                    //    worksheet.Cells[row, 1, row++, 10].SetCellRange(isMerge: true, text: $"รายงานประวัติการขออนุญาต", fontSize: 16, isBold: true);
                    //}

                    worksheet.Cells[row, 1].SetCellRange(isHeader: true, text: "ลำดับ");
                    worksheet.Cells[row, 2].SetCellRange(isHeader: true, text: "เลขที่");
                    worksheet.Cells[row, 3].SetCellRange(isHeader: true, text: "รหัสนักเรียน");
                    worksheet.Cells[row, 4].SetCellRange(isHeader: true, text: "ชื่อ-นามสกุล");
                    worksheet.Cells[row, 5].SetCellRange(isHeader: true, text: "วันที่ทำรายการ");
                    worksheet.Cells[row, 6].SetCellRange(isHeader: true, text: "เลขที่ใบอนุญาติ");
                    worksheet.Cells[row, 7].SetCellRange(isHeader: true, text: "กลุ่มรายการ");
                    worksheet.Cells[row, 8].SetCellRange(isHeader: true, text: "รายละเอียด");
                    worksheet.Cells[row, 9].SetCellRange(isHeader: true, text: "วันที่ขออนุญาต");
                    worksheet.Cells[row, 10].SetCellRange(isHeader: true, text: "แนบรูป");
                    worksheet.Cells[row++, 11].SetCellRange(isHeader: true, text: "ชื่อผู้ทำรายการ");

                    foreach (var r in data)
                    {
                        var col = 1;
                        worksheet.Cells[row, col++].SetCellRange(text: r.Index + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.Number + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.Code + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.FullName + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.Created + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.RefNo + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.GroupName + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.Cause + "");
                        worksheet.Cells[row, col++].SetCellRange(text: r.StartDate + "");
                        worksheet.Cells[row, col].SetCellRange(text:  "");
                        //worksheet.Cells[row, col++].SetCellRange(text: "แนบรูป");
                        if (r.Files != null)
                        {
                            var slipcount = 0;
                            foreach (var f in r.Files)
                            {
                                if (!string.IsNullOrEmpty(f))
                                {
                                    System.Drawing.Image logo;
                                    var request = WebRequest.Create(f);
                                    using (var response = request.GetResponse())
                                    using (var stream = response.GetResponseStream())
                                    {
                                        logo = System.Drawing.Image.FromStream(stream);
                                    }

                                    using (logo)
                                    {
                                        var excelImage = worksheet.Drawings.AddPicture($" แนบรูป #{r.PID} / {slipcount + 1}", logo);

                                        excelImage.SetPosition(row - 1, 0, col - 1, (slipcount * 70) + 10);
                                        excelImage.SetSize(70, 120);
                                    }

                                    slipcount++;
                                }
                            }
                        }
                        col++;
                        worksheet.Cells[row, col++].SetCellRange(text: r.Employee + "");
                        worksheet.Row(row).Height = 100;
                        row++;
                    }

                    worksheet.Column(1).AutoFit(10);
                    worksheet.Column(2).AutoFit(15);
                    worksheet.Column(3).AutoFit(20);
                    worksheet.Column(4).AutoFit(40);
                    worksheet.Column(5).AutoFit(20);
                    worksheet.Column(6).AutoFit(20);
                    worksheet.Column(7).AutoFit(30);
                    worksheet.Column(8).AutoFit(40);
                    worksheet.Column(9).AutoFit(25);
                    worksheet.Column(10).AutoFit(22);
                    worksheet.Column(11).AutoFit(30);

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/text;";
                    HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                    HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                    HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                    HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

                }
            }
        }

    }
}