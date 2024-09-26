using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.SDQ.Report
{
    public partial class ByRoomSDQType : BehaviorGateway
    {
        private static Color SUCCESS = Color.FromArgb(76, 175, 80);
        private static Color WARNING = Color.FromArgb(255, 152, 0);
        private static Color DANGER = Color.FromArgb(244, 67, 54);
        private static Color GRAY = Color.FromArgb(153, 153, 153);
        public class Search
        {
            public string term { get; set; }
            public int level1 { get; set; }
            public int? level2 { get; set; }

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

                var result = logic.LoadReportStudentByType(userData.CompanyID, search.term, search.level1, search.level2, "");

                var d = result//.GroupBy( o => new { o.Room})
                    .Select(o => new
                    {
                        //Index = i + 1,
                        o.sId,
                        o.Room,
                        Type1 = CalcScore1(o.Score1),
                        Type2 = CalcScore2(o.Score2),
                        Type3 = CalcScore2(o.Score3),
                    })
                    .Select( o => new { 
                        o.sId,
                        o.Room,
                        Score1Type1 = ((o.Type1 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type1 = ((o.Type1 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type1 = ((o.Type1 == 3) ? 1 : (Nullable<int>)null),

                        Score1Type2 = ((o.Type2 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type2 = ((o.Type2 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type2 = ((o.Type2 == 3) ? 1 : (Nullable<int>)null),

                        Score1Type3 = ((o.Type3 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type3 = ((o.Type3 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type3 = ((o.Type3 == 3) ? 1 : (Nullable<int>)null),
                    })
                    .GroupBy( o => new { o.Room })
                    .Select((o,i) => new {
                        Index = i + 1,
                        Room = o.Key.Room,
                        CountAll = o.Count(),

                        Score1Type1 = o.Sum(j => j.Score1Type1),
                        Score2Type1 = o.Sum(j => j.Score2Type1),
                        Score3Type1 = o.Sum(j => j.Score3Type1),

                        Score1Type2 = o.Sum(j => j.Score1Type2),
                        Score2Type2 = o.Sum(j => j.Score2Type2),
                        Score3Type2 = o.Sum(j => j.Score3Type2),

                        Score1Type3 = o.Sum(j => j.Score1Type3),
                        Score2Type3 = o.Sum(j => j.Score2Type3),
                        Score3Type3 = o.Sum(j => j.Score3Type3),
                    });

                return new
                {
                    data = d,
                    percent = new
                    {
                        Score1Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type1) * 100) / d.Sum(o => o.CountAll) : 0,

                        Score1Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type2) * 100) / d.Sum(o => o.CountAll) : 0,

                        Score1Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Score1Type1 = d.Sum(o => o.Score1Type1),
                        Score2Type1 = d.Sum(o => o.Score2Type1),
                        Score3Type1 = d.Sum(o => o.Score3Type1),

                        Score1Type2 = d.Sum(o => o.Score1Type2),
                        Score2Type2 = d.Sum(o => o.Score2Type2),
                        Score3Type2 = d.Sum(o => o.Score3Type2),

                        Score1Type3 = d.Sum(o => o.Score1Type3),
                        Score2Type3 = d.Sum(o => o.Score2Type3),
                        Score3Type3 = d.Sum(o => o.Score3Type3),
                    },
                };

            }

        }

        private static int? CalcScore2(int? score)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score <= 15)
                    return 1;
                else if (score >= 16 && score <= 17)
                    return 2;
                else
                    return 3;
            }

            return null;
        }

        private static int? CalcScore1(int? score)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score <= 16)
                    return 1;
                else if (score >= 17 && score <= 18)
                    return 2;
                else
                    return 3;
            }

            return null;

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudentByType(userData.CompanyID, term, level1, level2, "");

                var d = result//.GroupBy( o => new { o.Room})
                    .Select(o => new
                    {
                        //Index = i + 1,
                        o.sId,
                        o.Room,
                        Type1 = CalcScore1(o.Score1),
                        Type2 = CalcScore2(o.Score2),
                        Type3 = CalcScore2(o.Score3),
                    })
                    .Select(o => new {
                        o.sId,
                        o.Room,
                        Score1Type1 = ((o.Type1 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type1 = ((o.Type1 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type1 = ((o.Type1 == 3) ? 1 : (Nullable<int>)null),

                        Score1Type2 = ((o.Type2 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type2 = ((o.Type2 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type2 = ((o.Type2 == 3) ? 1 : (Nullable<int>)null),

                        Score1Type3 = ((o.Type3 == 1) ? 1 : (Nullable<int>)null),
                        Score2Type3 = ((o.Type3 == 2) ? 1 : (Nullable<int>)null),
                        Score3Type3 = ((o.Type3 == 3) ? 1 : (Nullable<int>)null),
                    })
                    .GroupBy(o => new { o.Room })
                    .Select((o, i) => new {
                        Index = i + 1,
                        Room = o.Key.Room,
                        CountAll = o.Count(),

                        Score1Type1 = o.Sum(j => j.Score1Type1),
                        Score2Type1 = o.Sum(j => j.Score2Type1),
                        Score3Type1 = o.Sum(j => j.Score3Type1),

                        Score1Type2 = o.Sum(j => j.Score1Type2),
                        Score2Type2 = o.Sum(j => j.Score2Type2),
                        Score3Type2 = o.Sum(j => j.Score3Type2),

                        Score1Type3 = o.Sum(j => j.Score1Type3),
                        Score2Type3 = o.Sum(j => j.Score2Type3),
                        Score3Type3 = o.Sum(j => j.Score3Type3),
                    });

                var data = new
                {
                    data = d,
                    percent = new
                    {
                        Score1Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type1) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type1 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type1) * 100) / d.Sum(o => o.CountAll) : 0,

                        Score1Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type2) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type2 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type2) * 100) / d.Sum(o => o.CountAll) : 0,

                        Score1Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score1Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score2Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score2Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                        Score3Type3 = d.Sum(o => o.CountAll) > 0 ? (d.Sum(o => o.Score3Type3) * 100) / d.Sum(o => o.CountAll) : 0,
                    },
                    summary = new
                    {
                        CountAll = d.Sum(o => o.CountAll),
                        Score1Type1 = d.Sum(o => o.Score1Type1),
                        Score2Type1 = d.Sum(o => o.Score2Type1),
                        Score3Type1 = d.Sum(o => o.Score3Type1),

                        Score1Type2 = d.Sum(o => o.Score1Type2),
                        Score2Type2 = d.Sum(o => o.Score2Type2),
                        Score3Type2 = d.Sum(o => o.Score3Type2),

                        Score1Type3 = d.Sum(o => o.Score1Type3),
                        Score2Type3 = d.Sum(o => o.Score2Type3),
                        Score3Type3 = d.Sum(o => o.Score3Type3),
                    },
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปผลรายบุคคล");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรายบุคคล"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++,12]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 12]
                        , isMerge: true
                        , text: $"รายงานสรุปผลรายบุคคล"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 12]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ลำดับ");
                    SetCell(worksheet.Cells[row, 2, row + 1, 2], isMerge: true, isHeader: true, text: "ระดับชั้น");
                    SetCell(worksheet.Cells[row, 3, row + 1, 3], isMerge: true, isHeader: true, text: "จำนวนนักเรียน");
                    SetCell(worksheet.Cells[row, 4, row, 6], isMerge: true, isHeader: true, text: "นักเรียน");
                    SetCell(worksheet.Cells[row, 7, row, 9], isMerge: true, isHeader: true, text: "ครู");
                    SetCell(worksheet.Cells[row, 10, row, 12], isMerge: true, isHeader: true, text: "ผู้ปกครอง");
                    row++;

                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "ปกติ" ,color:SUCCESS);
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "เสี่ยง", color: WARNING);
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "มีปัญหา", color: DANGER);

                    SetCell(worksheet.Cells[row, 7], isHeader: true, text: "ปกติ", color: SUCCESS);
                    SetCell(worksheet.Cells[row, 8], isHeader: true, text: "เสี่ยง", color: WARNING);
                    SetCell(worksheet.Cells[row, 9], isHeader: true, text: "มีปัญหา", color: DANGER);

                    SetCell(worksheet.Cells[row, 10], isHeader: true, text: "ปกติ", color: SUCCESS);
                    SetCell(worksheet.Cells[row, 11], isHeader: true, text: "เสี่ยง", color: WARNING);
                    SetCell(worksheet.Cells[row, 12], isHeader: true, text: "มีปัญหา", color: DANGER);
                    row++;

                    foreach (var r in data.data)
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.Index + "");
                        SetCell(worksheet.Cells[row, 2], text: r.Room + "");
                        SetCell(worksheet.Cells[row, 3], text: r.CountAll + "");

                        SetCell(worksheet.Cells[row, 4], text: r.Score1Type1 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.Score2Type1 + "");
                        SetCell(worksheet.Cells[row, 6], text: r.Score3Type1 + "");

                        SetCell(worksheet.Cells[row, 7], text: r.Score1Type2 + "");
                        SetCell(worksheet.Cells[row, 8], text: r.Score2Type2 + "");
                        SetCell(worksheet.Cells[row, 9], text: r.Score3Type2 + "");

                        SetCell(worksheet.Cells[row, 10], text: r.Score1Type3 + "");
                        SetCell(worksheet.Cells[row, 11], text: r.Score2Type3 + "");
                        SetCell(worksheet.Cells[row, 12], text: r.Score3Type3 + "");
                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1, row, 2], isMerge: true, text: "รวมทั้งสิ้น");//\u2705
                    SetCell(worksheet.Cells[row, 3], text: data.summary.CountAll + "");
                    SetCell(worksheet.Cells[row, 4], text: data.summary.Score1Type1 + "");
                    SetCell(worksheet.Cells[row, 5], text: data.summary.Score2Type1 + "");
                    SetCell(worksheet.Cells[row, 6], text: data.summary.Score3Type1 + "");

                    SetCell(worksheet.Cells[row, 7], text: data.summary.Score1Type2 + "");
                    SetCell(worksheet.Cells[row, 8], text: data.summary.Score2Type2 + "");
                    SetCell(worksheet.Cells[row, 9], text: data.summary.Score3Type2 + "");

                    SetCell(worksheet.Cells[row, 10], text: data.summary.Score1Type3 + "");
                    SetCell(worksheet.Cells[row, 11], text: data.summary.Score2Type3 + "");
                    SetCell(worksheet.Cells[row, 12], text: data.summary.Score3Type3 + "");
                    row++;

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                     , isMerge: true
                     , horizotal: ExcelHorizontalAlignment.Left
                     , text: $"นักเรียนประเมินแล้วปกติ คิดเป็นร้อยละ {data.percent.Score1Type1?.ToString("0.00")}% / นักเรียนประเมินแล้วเสี่ยง คิดเป็นร้อยละ {data.percent.Score2Type1?.ToString("0.00")}% / นักเรียนประเมินแล้วมีปัญหา คิดเป็นร้อยละ {data.percent.Score3Type1?.ToString("0.00")}%"
                     );

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                     , isMerge: true
                      , horizotal: ExcelHorizontalAlignment.Left
                     , text: $"ครูประเมินแล้วปกติ คิดเป็นร้อยละ {data.percent.Score1Type2?.ToString("0.00")}% / ครูประเมินแล้วเสี่ยง คิดเป็นร้อยละ {data.percent.Score2Type2?.ToString("0.00")}% / ครูประเมินแล้วมีปัญหา คิดเป็นร้อยละ {data.percent.Score3Type2?.ToString("0.00")}%"
                     );

                    SetCell(worksheet.Cells[row, 1, row++, 9]
                    , isMerge: true
                     , horizotal: ExcelHorizontalAlignment.Left
                    , text: $"ผู้ปกครองประเมินแล้วปกติ คิดเป็นร้อยละ {data.percent.Score1Type3?.ToString("0.00")}% / ผู้ปกครองประเมินแล้วเสี่ยง คิดเป็นร้อยละ {data.percent.Score2Type3?.ToString("0.00")}% / ผู้ปกครองประเมินแล้วมีปัญหา คิดเป็นร้อยละ {data.percent.Score3Type3?.ToString("0.00")}%"
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
                
                if (color.HasValue)
                {
                    xrange.Style.Font.Color.SetColor(color.Value);
                }

                xrange.AutoFitColumns();

            }
        }
    }
}