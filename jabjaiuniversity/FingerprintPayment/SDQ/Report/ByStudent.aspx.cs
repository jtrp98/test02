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

namespace FingerprintPayment.SDQ.Report
{
    public partial class ByStudent : BehaviorGateway
    {
        private static Color SUCCESS = Color.FromArgb(76, 175, 80);
        private static Color WARNING = Color.FromArgb(255, 152, 0);
        private static Color DANGER = Color.FromArgb(244, 67, 54);
        private static Color GRAY = Color.FromArgb(153, 153, 153);
        private static string STATUS1 = "ปกติ";
        private static string STATUS2 = "เสี่ยง";
        private static string STATUS3 = "มีปัญหา";
        private static string STRENGTH1 = "เป็นจุดแข็ง";
        private static string STRENGTH2 = "ไม่มีจุดแข็ง";
        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }
            public string name { get; set; }
            public int type { get; set; }
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
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, search.name, search.type);

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
                        o.Score1,
                        o.Score2,
                        o.Score3,
                        o.Score4,
                        o.Score5,

                    })
                };

            }

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2, string name, int type)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, term, level1, level2, name, type);

                var data = result.Select((o, i) => new
                {
                    Index = i + 1,
                    o.sId,
                    o.Number,
                    o.Code,
                    o.FullName,
                    o.Room,
                    o.Score1,
                    o.Score2,
                    o.Score3,
                    o.Score4,
                    o.Score5,
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

                        SetCell(worksheet.Cells[row, 1, row++, 10]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 10]
                        , isMerge: true
                        , text: $"รายงานสรุปผลรายบุคคล"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 10]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1], isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "รหัสนักเรียน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ชั้นเรียน");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ชื่อ-นามสกุล");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "อารมณ์");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "ความพฤติ");
                    SetCell(worksheet.Cells[row, 7], isHeader: true, text: "สมาธิ");
                    SetCell(worksheet.Cells[row, 8], isHeader: true, text: "ความสัมพันธ์กับเพื่อน");
                    SetCell(worksheet.Cells[row, 9], isHeader: true, text: "สังคม");
                    SetCell(worksheet.Cells[row++, 10], isHeader: true, text: "ผลประเมิน");

                    foreach (var r in data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Code + "");
                        SetCell(worksheet.Cells[row, 3], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 4], text: r.FullName + "");

                        var c = CalcTypeScore(type, 1, r.Score1);
                        SetCell(worksheet.Cells[row, 5], text: c.Item2, color: c.Item1);
                        c = CalcTypeScore(type, 2, r.Score2);
                        SetCell(worksheet.Cells[row, 6], text: c.Item2, color: c.Item1);
                        c = CalcTypeScore(type, 3, r.Score3);
                        SetCell(worksheet.Cells[row, 7], text: c.Item2, color: c.Item1);
                        c = CalcTypeScore(type, 4, r.Score4);
                        SetCell(worksheet.Cells[row, 8], text: c.Item2, color: c.Item1);
                        c = CalcTypeScore(type, 5, r.Score5);
                        SetCell(worksheet.Cells[row, 9], text: c.Item2, color: c.Item1);

                        int? sumAll = 0;
                        switch (type)
                        {
                            case 1:
                                sumAll = r.Score1 + r.Score2 + r.Score3 + r.Score4;
                                break;

                            case 2:
                            case 3:
                                sumAll = r.Score1 + r.Score2 + r.Score3 + r.Score4 + r.Score5;
                                break;

                            default:
                                break;
                        }

                        c = CalcTypeScore(type, 6, sumAll);
                        SetCell(worksheet.Cells[row, 10], text: c.Item2, color: c.Item1);

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


        private static (Color, string) CalcTypeScore(int type, int group, int? score)
        {
            if (score.HasValue)
            {
                switch (type)
                {
                    case 1:
                        return CalcGroupScore1(group, score);

                    case 2:
                    case 3:
                        return CalcGroupScore2(group, score);

                    default:
                        return (GRAY, "");
                }
            }
            else
            {
                return (GRAY, "");
            }
        }

        private static (Color, string) CalcGroupScore2(int group, int? score)
        {
            switch (group)
            {
                case 1:
                    if (score >= 0 && score <= 3)
                        return (SUCCESS, STATUS1);
                    else if (score == 4)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 2:
                    if (score >= 0 && score <= 3)
                        return (SUCCESS, STATUS1);
                    else if (score == 4)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 3:
                    if (score >= 0 && score <= 5)
                        return (SUCCESS, STATUS1);
                    else if (score == 6)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 4:
                    if (score >= 0 && score <= 5)
                        return (SUCCESS, STATUS1);
                    else if (score == 6)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 5:
                    if (score >= 4 && score <= 10)
                        return (SUCCESS, STRENGTH1);
                    else
                        return (WARNING, STRENGTH2);
                   
                case 6:
                    if (score >= 0 && score <= 15)
                        return (SUCCESS, STATUS1);
                    else if (score >= 16 && score <= 17)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                default:
                    return (GRAY, "");
            }
        }

        private static (Color, string) CalcGroupScore1(int group, int? score)
        {
            switch (group)
            {
                case 1:
                    if (score >= 0 && score <= 5)
                        return (SUCCESS, STATUS1);
                    else if (score == 6)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 2:
                    if (score >= 0 && score <= 4)
                        return (SUCCESS, STATUS1);
                    else if (score == 5)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 3:
                    if (score >= 0 && score <= 5)
                        return (SUCCESS, STATUS1);
                    else if (score == 6)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 4:
                    if (score >= 0 && score <= 3)
                        return (SUCCESS, STATUS1);
                    else if (score == 4)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
                case 5:
                    if (score >= 4 && score <= 10)
                        return (SUCCESS, STRENGTH1);
                    else
                        return (WARNING, STRENGTH2);
                case 6:
                    if (score >= 0 && score <= 16)
                        return (SUCCESS, STATUS1);
                    else if (score >= 17 && score <= 18)
                        return (WARNING, STATUS2);
                    else
                        return (DANGER, STATUS3);
              
                default:
                    return (GRAY, "");
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