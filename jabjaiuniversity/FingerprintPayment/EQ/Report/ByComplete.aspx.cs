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
using FingerprintPayment.App_Start;

namespace FingerprintPayment.EQ.Report
{
    public partial class ByComplete : BehaviorGateway
    {

        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new EQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, "");
                var d = result
                    .Select(o => new
                    {
                        o.Room,
                        ScoreAll = o.Score11 + o.Score12 + o.Score13
                        + o.Score21 + o.Score22 + o.Score23
                        + o.Score31 + o.Score32 + o.Score33
                    })
                    .GroupBy(o => o.Room)
                    .Select((o, i) => new
                    {
                        Index = i + 1,
                        Room = o.Key,
                        Count = o.Count(),
                        Done = o.Count(j => j.ScoreAll.HasValue),
                        NotDone = o.Count(j => !j.ScoreAll.HasValue)
                    });

                return new
                {
                    data = d,
                    percent = new
                    {
                        s1 = d.Sum(o => o.Count) > 0 ? (d.Sum(o => o.Done) * 100) / d.Sum(o => o.Count) : 0,
                        s2 = d.Sum(o => o.Count) > 0 ? (d.Sum(o => o.NotDone) * 100) / d.Sum(o => o.Count) : 0,
                    },
                };

            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new EQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, term, level1, level2, "");

                var data = result
                    .Select(o => new
                    {
                        o.Room,
                        ScoreAll = o.Score11 + o.Score12 + o.Score13
                        + o.Score21 + o.Score22 + o.Score23
                        + o.Score31 + o.Score32 + o.Score33
                    })
                    .GroupBy(o => o.Room)
                    .Select((o, i) => new
                    {
                        Index = i + 1,
                        Room = o.Key,
                        Count = o.Count(),
                        Done = o.Count(j => j.ScoreAll.HasValue),
                        NotDone = o.Count(j => !j.ScoreAll.HasValue)
                    });

                var percent = new
                {
                    s1 = data.Sum(o => o.Count) > 0 ? (data.Sum(o => o.Done) * 100) / data.Sum(o => o.Count) : 0,
                    s2 = data.Sum(o => o.Count) > 0 ? (data.Sum(o => o.NotDone) * 100) / data.Sum(o => o.Count) : 0,
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปสถานะการประเมิน");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปสถานะการประเมิน"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 4]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 4]
                        , isMerge: true
                        , text: $"รายงานสรุปสถานะการประเมิน"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 4]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ระดับชั้น");
                    SetCell(worksheet.Cells[row, 2, row + 1, 2], isMerge: true, isHeader: true, text: "จำนวนนักเรียน");
                    SetCell(worksheet.Cells[row, 3, row++, 4], isMerge: true, isHeader: true, text: "สถานะการประเมิน");
                    //SetCell(worksheet.Cells[row++, 4], isMerge: true , isHeader: true, text: "ชื่อ-นามสกุล");

                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ประเมินแล้ว");
                    SetCell(worksheet.Cells[row++, 4], isHeader: true, text: "ยังไม่ประเมิน");

                    foreach (var r in data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Count + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Done + "");
                        SetCell(worksheet.Cells[row, 4], text: r.NotDone + "");

                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1, row++, 4]
                 , isMerge: true
                 , horizotal: ExcelHorizontalAlignment.Left
                 , text: $"นักเรียนประเมินแล้ว คิดเป็นร้อยละ {percent.s1.ToString("0.00")}% / นักเรียนยังไม่ประเมิน คิดเป็นร้อยละ {percent.s2.ToString("0.00")}%"
                 );

                    worksheet.Cells.AutoFitColumns(20, 40);

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

        private static void SetCell(ExcelRange xrange
          , bool isHeader = false
          , bool isMerge = false
          , string text = ""
          , int fontSize = 11
          , bool isBold = false
          , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
          , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
          , Color? color = null
      )
        {
            using (xrange)
            {
                xrange.Merge = isMerge;
                xrange.Value = text;
                xrange.Style.Font.Bold = isBold;
                xrange.Style.HorizontalAlignment = horizotal;
                xrange.Style.VerticalAlignment = vetical;
                xrange.Style.Font.Size = fontSize;

                if (color.HasValue)
                {
                    xrange.Style.Font.Color.SetColor(color.Value);
                }

                if (isHeader)
                {
                    xrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    xrange.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
                }
                //    xrange.Style.Fill.BackgroundColor.SetColor(bgColor);

                xrange.AutoFitColumns();

            }
        }
    }
}