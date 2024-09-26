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
using FingerprintPayment.PaymentGateway.KBank.KBankPromptPay;

namespace FingerprintPayment.EQ.Report
{
    public partial class BySummary : BehaviorGateway
    {

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
                var summary = GetSummaryData(ctx, search, userData);

                return new
                {
                    data = summary,

                };

            }


        }

        private static List<Model1> GetSummaryData(JabJaiEntities ctx, Search search, JWTToken.userData userData)
        {
            var logic = new EQRepository(ctx);
            var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, "");
            var d = result
                .Select(o => new
                {
                    o.sId,
                    o.Score11,
                    o.Score12,
                    o.Score13,
                    o.Score21,
                    o.Score22,
                    o.Score23,
                    o.Score31,
                    o.Score32,
                    o.Score33,
                })
                .Select(r => new
                {
                    Count11 = CalcScoreByRange(r.Score11, 13, 18),
                    Count12 = CalcScoreByRange(r.Score12, 16, 21),
                    Count13 = CalcScoreByRange(r.Score13, 17, 23),

                    Count21 = CalcScoreByRange(r.Score21, 15, 21),
                    Count22 = CalcScoreByRange(r.Score22, 14, 20),
                    Count23 = CalcScoreByRange(r.Score23, 15, 20),

                    Count31 = CalcScoreByRange(r.Score31, 9, 14),
                    Count32 = CalcScoreByRange(r.Score32, 16, 22),
                    Count33 = CalcScoreByRange(r.Score33, 15, 22),
                });
            var summary = new List<Model1>();

            summary.Add(new Model1
            {
                group = "ด้านดี",
                title = "ควบคุมตนเอง",
                c1 = d.Count(o => o.Count11 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count11 == 1) * 100 / (d.Count()*1f) : 0,

                c2 = d.Count(o => o.Count11 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count11 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count11 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count11 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านดี",
                title = "เห็นใจผู้อื่น",
                c1 = d.Count(o => o.Count12 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count12 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count12 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count12 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count12 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count12 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านดี",
                title = "รับผิดชอบ",
                c1 = d.Count(o => o.Count13 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count12 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count13 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count12 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count13 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count12 == 3) * 100 / (d.Count() * 1f) : 0,
            });

            //2
            summary.Add(new Model1
            {
                group = "ด้านเก่ง",
                title = "มีแรงจูงใจ",
                c1 = d.Count(o => o.Count21 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count21 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count21 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count21 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count21 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count21 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านเก่ง",
                title = "ตัดสินใจและแก้ปัญหา",
                c1 = d.Count(o => o.Count22 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count22 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count22 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count22 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count22 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count22 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านเก่ง",
                title = "สัมพันธภาพ",
                c1 = d.Count(o => o.Count23 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count23 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count23 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count23 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count23 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count23 == 3) * 100 / (d.Count() * 1f) : 0,
            });

            //3
            summary.Add(new Model1
            {
                group = "ด้านสุข",
                title = "ภูมิใจในตนเอง",
                c1 = d.Count(o => o.Count31 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count31 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count31 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count31 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count31 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count31 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านสุข",
                title = "พอใจในชีวิต",
                c1 = d.Count(o => o.Count32 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count32 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count32 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count32 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count32 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count32 == 3) * 100 / (d.Count() * 1f) : 0,
            });
            summary.Add(new Model1
            {
                group = "ด้านสุข",
                title = "สุขสงบทางใจ",
                c1 = d.Count(o => o.Count33 == 1),
                p1 = d.Count() > 0 ? d.Count(o => o.Count33 == 1) * 100 / (d.Count() * 1f) : 0,

                c2 = d.Count(o => o.Count33 == 2),
                p2 = d.Count() > 0 ? d.Count(o => o.Count33 == 2) * 100 / (d.Count() * 1f) : 0,

                c3 = d.Count(o => o.Count33 == 3),
                p3 = d.Count() > 0 ? d.Count(o => o.Count33 == 3) * 100 / (d.Count() * 1f) : 0,
            });

            return summary;
        }

        private static int? CalcScoreByRange(int? score, int low, int high)
        {
            if (score.HasValue)
            {
                if (score >= 0 && score < low)
                    return 1;
                else if (score >= low && score <= high)
                    return 2;
                else
                    return 3;
            }
            else
            {
                return null;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(string yearNo, string term, string termNo, int level1, int? level2)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var summary = GetSummaryData(ctx, new Search() { term = term, level1 = level1, level2 = level2 }, userData);


                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปผลรวม");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรวม"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                          , isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                        , isMerge: true
                        , text: $"รายงานสรุปผลรวม"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                         , isMerge: true
                         , text: $"ปีการศึกษา {yearNo} ภาคเรียนที่ {termNo}"
                         );
                    }

                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ด้าน");
                    SetCell(worksheet.Cells[row, 2, row, 3], isMerge: true, isHeader: true, text: "ต่ำกว่าเกณฑ์");
                    SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, isHeader: true, text: "เกณฑ์ปกติ");
                    SetCell(worksheet.Cells[row, 6, row, 7], isMerge: true, isHeader: true, text: "สูงกว่าเกณฑ์");
                    row++;

                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ร้อยละ");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "ร้อยละ");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 7], isHeader: true, text: "ร้อยละ");
                    row++;

                    foreach (var g in summary.GroupBy(o => o.group))
                    {
                        SetCell(worksheet.Cells[row, 1, row++, 7], isMerge: true, text: g.Key + "" , horizotal:ExcelHorizontalAlignment.Left);

                        foreach (var r in g)
                        {
                            SetCell(worksheet.Cells[row, 1], text: r.title + "");
                            SetCell(worksheet.Cells[row, 2], text: r.c1 + "");
                            SetCell(worksheet.Cells[row, 3], text: r.p1.ToString("0.00") + "%");
                            SetCell(worksheet.Cells[row, 4], text: r.c2 + "");
                            SetCell(worksheet.Cells[row, 5], text: r.p2.ToString("0.00") + "%");
                            SetCell(worksheet.Cells[row, 6], text: r.c3 + "");
                            SetCell(worksheet.Cells[row, 7], text: r.p3.ToString("0.00") + "%");

                            row++;
                        }
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

        private class Model1
        {
            public string group { get; set; }
            public string title { get; set; }
            public int c1 { get; set; }
            public float p1 { get; set; }
            public int c2 { get; set; }
            public float p2 { get; set; }
            public int c3 { get; set; }
            public float p3 { get; set; }
        }
    }
}