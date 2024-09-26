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

namespace FingerprintPayment.EQ.Report
{
    public partial class ByStudentDetail : BehaviorGateway
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

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, search.name);

                return new
                {
                    data = result.Select((o, i) => new
                    {
                        Index = i + 1,
                        o.sId,
                        o.Number,
                        o.Code,
                        o.FullName,
                        o.Room,
                        o.Score11,
                        o.Score12,
                        o.Score13,
                        o.Score21,
                        o.Score22,
                        o.Score23,
                        o.Score31,
                        o.Score32,
                        o.Score33,

                        ScoreAll = o.Score11 + o.Score12 + o.Score13
                        + o.Score21 + o.Score22 + o.Score23
                        + o.Score31 + o.Score32 + o.Score33
                    })
                };

            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2, string name)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new EQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, term, level1, level2, name);

                var data = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.sId,
                    o.Number,
                    o.Code,
                    o.FullName,
                    o.Room,
                    o.Score11,
                    o.Score12,
                    o.Score13,
                    o.Score21,
                    o.Score22,
                    o.Score23,
                    o.Score31,
                    o.Score32,
                    o.Score33,

                    ScoreAll = o.Score11 + o.Score12 + o.Score13
                        + o.Score21 + o.Score22 + o.Score23
                        + o.Score31 + o.Score32 + o.Score33
                });

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปผลรายบุคคล");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรายบุคคล"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 14]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 14]
                        , isMerge: true
                        , text: $"รายงานสรุปผลรายบุคคล"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 14]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2, row + 1, 2], isMerge: true, isHeader: true, text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[row, 3, row + 1, 3], isMerge: true, isHeader: true, text: "ชั้นเรียน");
                    SetCell(worksheet.Cells[row, 4, row + 1, 4], isMerge: true, isHeader: true, text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row, 5, row, 7], isMerge: true, isHeader: true, text: "ความดี");
                    SetCell(worksheet.Cells[row, 8, row, 10], isMerge: true, isHeader: true, text: "ความเก่ง");
                    SetCell(worksheet.Cells[row, 11, row, 13], isMerge: true, isHeader: true, text: "ความสุข");
                    SetCell(worksheet.Cells[row, 14, row + 1, 14], isMerge: true, isHeader: true, text: "ผลการประเมิน");
                    row++;

                    SetCell(worksheet.Cells[row, 5], isMerge: true, isHeader: true, text: "1.1");
                    SetCell(worksheet.Cells[row, 6], isMerge: true, isHeader: true, text: "1.2");
                    SetCell(worksheet.Cells[row, 7], isMerge: true, isHeader: true, text: "1.3");

                    SetCell(worksheet.Cells[row, 8], isMerge: true, isHeader: true, text: "2.1");
                    SetCell(worksheet.Cells[row, 9], isMerge: true, isHeader: true, text: "2.2");
                    SetCell(worksheet.Cells[row, 10], isMerge: true, isHeader: true, text: "2.3");

                    SetCell(worksheet.Cells[row, 11], isMerge: true, isHeader: true, text: "3.1");
                    SetCell(worksheet.Cells[row, 12], isMerge: true, isHeader: true, text: "3.2");
                    SetCell(worksheet.Cells[row, 13], isMerge: true, isHeader: true, text: "3.3");
                    row++;

                    foreach (var r in data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Code + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 4], text: r.FullName + "");

                        SetCell(worksheet.Cells[row, 5], text: CalcScoreByRange(r.Score11, 13, 18));
                        SetCell(worksheet.Cells[row, 6], text: CalcScoreByRange(r.Score12, 16, 21));
                        SetCell(worksheet.Cells[row, 7], text: CalcScoreByRange(r.Score13, 17, 23));

                        SetCell(worksheet.Cells[row, 8], text: CalcScoreByRange(r.Score21, 15, 21));
                        SetCell(worksheet.Cells[row, 9], text: CalcScoreByRange(r.Score22, 14, 20));
                        SetCell(worksheet.Cells[row, 10], text: CalcScoreByRange(r.Score23, 15, 20));

                        SetCell(worksheet.Cells[row, 11], text: CalcScoreByRange(r.Score31, 9, 14));
                        SetCell(worksheet.Cells[row, 12], text: CalcScoreByRange(r.Score32, 16, 22));
                        SetCell(worksheet.Cells[row, 13], text: CalcScoreByRange(r.Score33, 15, 22));

                        SetCell(worksheet.Cells[row, 14], text: CalcScore(r.ScoreAll));

                        row++;
                    }

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

        private static string CalcScore(int? score)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score < 140)
                    return "ต่ำกว่าเกณฑ์";
                else if (score >= 140 && score <= 170)
                    return "ปกติ";
                else
                    return "สูงกว่าเกณฑ์";
            }
            else
            {
                return "";
            }
        }

        private static string CalcScoreByRange(int? score, int low, int high)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score < low)
                    return "ต่ำกว่าเกณฑ์";
                else if (score >= low && score <= high)
                    return "ปกติ";
                else
                    return "สูงกว่าเกณฑ์";
            }
            else
            {
                return "";
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