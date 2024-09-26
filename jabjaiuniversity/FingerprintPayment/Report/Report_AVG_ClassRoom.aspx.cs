using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;
using FingerprintPayment.Helper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;

namespace FingerprintPayment.Report
{
    public partial class Report_AVG_ClassRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                
    
                using(JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {

                    DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.OrderByDescending(o => o.numberYear).ToList());

                    fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "เลือกปีการศึกษา", "nYear", "numberYear");
                    ddlyear.SelectedValue = DateTime.Now.Year.ToString();

                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    hdfschoolname.Value = tCompany.sCompany;
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);

                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                }

            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object Report_AVGStudent_ClassRoom(Search search)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return "Session Time Out";
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {


                var RoomList = (from a in db.TUser
                                join b in db.TTermSubLevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                                join c in db.TSubLevels on b.nTSubLevel equals c.nTSubLevel
                                join d in db.TLevels on c.nTLevel equals d.LevelID

                                where a.cDel == null
                                && (!search.sUbLV_Id.HasValue || search.sUbLV_Id == c.nTSubLevel)

                                select new
                                {
                                    a.sID,
                                    a.sStudentID,
                                    a.nStudentNumber,
                                    a.sStudentTitle,
                                    a.sName,
                                    a.sLastname,
                                    a.cSex,

                                    facultyID = d.LevelID,
                                    facultyName = d.LevelName,
                                    facultyNumber = d.sortValue,

                                    classroomid = c.nTSubLevel,
                                    classroomname = c.SubLevel,
                                    roomid = b.nTermSubLevel2,
                                    roomname = b.nTSubLevel2
                                }).ToList();

                var LaYer0s = (from a in RoomList
                               group a by new { a.facultyID, a.facultyName, a.facultyNumber } into gb0
                               orderby gb0.Key.facultyNumber
                               select new LaYer0
                               {
                                   FacultyclassID = gb0.Key.facultyID,
                                   FacultyclassName = gb0.Key.facultyName,
                                   LaYer1s = (from a in gb0
                                              where a.facultyID == gb0.Key.facultyID
                                              group a by new { a.classroomid, a.classroomname } into gb1
                                              select new LaYer1
                                              {
                                                  ClassRoomName = gb1.Key.classroomname,
                                                  CountMale = gb1.Count(c => c.cSex == "0"),
                                                  CountFemale = gb1.Count(c => c.cSex == "1"),
                                                  CountMaleFemale = gb1.Select(s => s.cSex).Count(),
                                                  CountRoom = (from a in gb1
                                                               where a.classroomid == gb1.Key.classroomid
                                                               group a by new { a.roomid } into gb2
                                                               select new { gb2.Key }).Count()
                                              }).OrderBy(o => o.ClassRoomName, new SemiNumericComparer()).ToList()
                               }).ToList();

                var HeaderText = "รายงานค่าเฉลี่ยนักเรียน";

                return new view
                {
                    HeaderText = HeaderText,
                    LaYer0s = LaYer0s
                };
            }
        }


        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void export_data(Search search)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }

            var ReportView = Report_AVGStudent_ClassRoom(search) as view;

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("รายงานค่าเฉลี่ยนักเรียน");
                var worksheet = excel.Workbook.Worksheets["รายงานค่าเฉลี่ยนักเรียน"];
                string entities = HttpContext.Current.Session["sEntities"].ToString();
            
                var tCompany = db.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                int Rows = 6;
                SetHeader(worksheet, "A1:F1", true, tCompany.sCompany, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A2:F2", true, ReportView.HeaderText, 15, ExcelHorizontalAlignment.Center);
                SetHeader(worksheet, "A3:F3", true,
                        "พิมวันที่ : " + DateTime.Today.ToString("dd MMM yyyy", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);
                SetHeader(worksheet, "A4:F4", true,
                    "เวลา : " + DateTime.Now.ToString("HH:mm:ss", new CultureInfo("th-th")), 15, ExcelHorizontalAlignment.Right);

                string[] Header = { "ชั้นเรียน", "ชาย", "หญิง", "จำนวนนักเรียนรวม", "จำนวนห้อง", "เฉลี่ยต่อห้อง" };
                int Columuns = 1;
                foreach (string DataHeadder in Header)
                {
                    SetTableHeader(worksheet.Cells[5, Columuns++], false, DataHeadder, ExcelHorizontalAlignment.Center);
                }

                Decimal TotalMale = 0; Decimal TotalFemale = 0; Decimal TotalMaleFemale = 0;

                foreach (var StdDataLV0 in ReportView.LaYer0s)
                {
                    Decimal SumMale = 0; Decimal SumFemale = 0; Decimal SumMaleFemale = 0; Decimal SumRoom = 0;

                    if (StdDataLV0.LaYer1s.Count() == 0) continue;
                    int rowsEnd = Rows + (StdDataLV0.LaYer1s.Count() - 1);
                    foreach (var StdDataLV1 in StdDataLV0.LaYer1s)
                    {
                        SetTableRows(worksheet.Cells[Rows, 1, rowsEnd, 1], false, StdDataLV1.ClassRoomName, ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 2, rowsEnd, 2], false, string.Format("{0:0}", StdDataLV1.CountMale), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 3, rowsEnd, 3], false, string.Format("{0:0}", StdDataLV1.CountFemale), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 4, rowsEnd, 4], false, string.Format("{0:0}", StdDataLV1.CountMaleFemale), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 5, rowsEnd, 5], false, string.Format("{0:0}", StdDataLV1.CountRoom), ExcelHorizontalAlignment.Center);
                        SetTableRows(worksheet.Cells[Rows, 6, rowsEnd, 6], false, string.Format("{0:#,0.00}", (StdDataLV1.CountMaleFemale / StdDataLV1.CountRoom)), ExcelHorizontalAlignment.Center);
                        Rows += 1;

                        SumMale += StdDataLV1.CountMale;
                        SumFemale += StdDataLV1.CountFemale;
                        SumMaleFemale += StdDataLV1.CountMaleFemale;
                        SumRoom += StdDataLV1.CountRoom;

                        TotalMale += StdDataLV1.CountMale;
                        TotalFemale += StdDataLV1.CountFemale;
                        TotalMaleFemale += StdDataLV1.CountMaleFemale;
                    }

                    SetTableFooter(worksheet.Cells[Rows, 1, Rows, 1], true, "รวม" + StdDataLV0.FacultyclassName, ExcelHorizontalAlignment.Center);
                    SetTableFooter(worksheet.Cells[Rows, 2, Rows, 2], true, string.Format("{0:0}", SumMale), ExcelHorizontalAlignment.Center);
                    SetTableFooter(worksheet.Cells[Rows, 3, Rows, 3], true, string.Format("{0:0}", SumFemale), ExcelHorizontalAlignment.Center);
                    SetTableFooter(worksheet.Cells[Rows, 4, Rows, 4], true, string.Format("{0:0}", SumMaleFemale), ExcelHorizontalAlignment.Center);
                    SetTableFooter(worksheet.Cells[Rows, 5, Rows, 5], true, string.Format("{0:0}", SumRoom), ExcelHorizontalAlignment.Center);
                    SetTableFooter(worksheet.Cells[Rows, 6, Rows, 6], true, "", ExcelHorizontalAlignment.Center);
                    Rows += 1;
                }

                SetTableFooter(worksheet.Cells[Rows, 1, Rows, 1], true, "รวมจำนวนนักเรียนทั้งหมด", ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 2, Rows, 2], true, string.Format("{0:0}", TotalMale), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 3, Rows, 3], true, string.Format("{0:0}", TotalFemale), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 4, Rows, 4], true, string.Format("{0:0}", TotalMaleFemale), ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 5, Rows, 5], true, "", ExcelHorizontalAlignment.Center);
                SetTableFooter(worksheet.Cells[Rows, 6, Rows, 6], true, "", ExcelHorizontalAlignment.Center);

                worksheet.Cells.AutoFitColumns();
                worksheet.Column(1).Width = 30;
                worksheet.Column(2).Width = 15;
                worksheet.Column(3).Width = 15;
                worksheet.Column(4).Width = 25;
                worksheet.Column(5).Width = 20;
                worksheet.Column(6).Width = 25;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls");
                HttpContext.Current.Response.ContentType = "application/text";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(excel.GetAsByteArray());
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**

            }

        }



        private static void SetHeader(ExcelWorksheet excelWorksheet, string Cells, bool Merge, string strValues, int? fontSize, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = excelWorksheet.Cells[Cells])
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Font.Size = fontSize ?? 10;
            }
        }
        private static void SetTableHeader(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
            }
        }
        private static void SetTableRows(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            }
        }
        private static void SetTableFooter(ExcelRange Cells, bool Merge, string strValues, ExcelHorizontalAlignment excelHorizontal)
        {
            using (ExcelRange rng = Cells)
            {
                rng.Merge = Merge;
                rng.Value = strValues;
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = excelHorizontal;
                rng.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                rng.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(0, 217, 217, 217);
            }
        }




        public class Search
        {
            public string yEar_Id { get; set; }
            public int? sUbLV_Id { get; set; }
        }
        public class view
        {
            public string HeaderText { get; set; }
            public List<LaYer0> LaYer0s { get; set; }
        }
        public class LaYer0
        {
            public List<LaYer1> LaYer1s { get; set; }
            public int FacultyclassID { get; set; }
            public string FacultyclassName { get; set; }
        }
        public class LaYer1
        {
            public string ClassRoomName { get; set; }
            public decimal CountMale { get; set; }
            public decimal CountFemale { get; set; }
            public decimal CountMaleFemale { get; set; }
            public decimal CountRoom { get; set; }
        }




    }
}