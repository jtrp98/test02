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
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using static FingerprintPayment.SDQ.Report.SummaryByStatus;
using FingerprintPayment.Class;
using System.Windows;

namespace FingerprintPayment.Screening.Report
{
    public partial class BySummary : BehaviorGateway
    {
        private static Color SUCCESS = Color.FromArgb(76, 175, 80);
        private static Color INFO = Color.FromArgb(188, 212, 1);
        private static Color DANGER = Color.FromArgb(244, 67, 54);
        private static Color GRAY = Color.FromArgb(153, 153, 153);
        private static Color BLACK = Color.FromArgb(0, 0, 0);
        private static Color BGCOLOR = Color.FromArgb(231, 231, 231);

        private static string CHECK_UNICODE = "\u2705";
        private static string SQUARE_UNICODE = "\u25FE";

        private static string STATUS1 = "ต่ำกว่าปกติ";
        private static string STATUS2 = "ปกติ";
        private static string STATUS3 = "สูงกว่าปกติ";

        public class Search
        {
            public string yearNo { get; set; }
            public string term { get; set; }
            public string termNo { get; set; }
            public int? level1 { get; set; }
            public int? level2 { get; set; }

        }

        private class Model1
        {
            public int type { get; set; }
            public string group { get; set; }
            public int c1 { get; set; }
            public float p1 { get; set; }
            public int c2 { get; set; }
            public float p2 { get; set; }
            public int c3 { get; set; }
            public float p3 { get; set; }
        }
        private class Model2
        {
            public int Count1 { get; set; }

            public int Count2 { get; set; }
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

                var (summary, summary2) = GetSummaryData(ctx, search, userData);

                return new
                {
                    data = summary,
                    //summary = new { 
                    //    c1 = summary.Sum(x => x.c1),
                    //}
                    //data = result.Select((o, i) => new
                    //{

                    //}),
                    //countAll = result.Count,
                };
            }
        }

        private static (List<Model1>, Model2) GetSummaryData(JabJaiEntities ctx, Search search, JWTToken.userData userData)
        {
            var logic1 = new ScreeningRepository(ctx);

            var d1 = logic1.LoadReportSummary(userData.CompanyID, search.term, search.level1, search.level2);

            var logic2 = new SDQRepository(ctx);

            var d2 = logic2.LoadReportStudentByType(userData.CompanyID, search.term, search.level1, search.level2, "")
                .Select(o => new
                {
                    o.sId,
                    Score = CalcSDQNo4(o.Score2),
                })
                .Select(o => new
                {
                    o.sId,
                    Score41 = o.Score == 1 ? 1 : 0,
                    Score42 = o.Score == 2 ? 1 : 0,
                    Score43 = o.Score == 3 ? 1 : 0,
                });

            var summary = new List<Model1>();

            summary.Add(new Model1
            {
                type = 1,
                group = "2.ด้านความสามารถอื่นๆ",
                c1 = d1.Sum(o => o.Score21),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score21) * 100 / (d1.Count() * 1f) : 0,
                c2 = d1.Sum(o => o.Score22),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score22) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "1.ด้านการเรียน",
                c1 = d1.Sum(o => o.Score11),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score11) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score12),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score12) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score13),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score13) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "3.ด้านสุขภาพ",
                c1 = d1.Sum(o => o.Score31),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score31) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score32),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score32) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score33),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score33) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "4.ด้านสุขภาพจิตและพฤติกรรม (พิจารณาจากแบบประเมิน SDQ)",
                c1 = d2.Sum(o => o.Score41),
                p1 = d2.Count() > 0 ? d2.Sum(o => o.Score41) * 100 / (d2.Count() * 1f) : 0,

                c2 = d2.Sum(o => o.Score42),
                p2 = d2.Count() > 0 ? d2.Sum(o => o.Score42) * 100 / (d2.Count() * 1f) : 0,

                c3 = d2.Sum(o => o.Score43),
                p3 = d2.Count() > 0 ? d2.Sum(o => o.Score43) * 100 / (d2.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "5.ด้านเศรษฐกิจ",
                c1 = d1.Sum(o => o.Score51),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score51) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score52),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score52) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score53),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score53) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "6.ด้านการคุ้มครองนักเรียน",
                c1 = d1.Sum(o => o.Score61),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score61) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score62),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score62) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score63),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score63) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "7.ด้านการใช้สารเสพติต",
                c1 = d1.Sum(o => o.Score71),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score71) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score72),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score72) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score73),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score73) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "8.ด้านเพศสัมพันธ์",
                c1 = d1.Sum(o => o.Score81),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score81) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score82),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score82) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score83),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score83) * 100 / (d1.Count() * 1f) : 0,
            });

            summary.Add(new Model1
            {
                type = 2,
                group = "9.ด้านอื่นๆ",
                c1 = d1.Sum(o => o.Score91),
                p1 = d1.Count() > 0 ? d1.Sum(o => o.Score91) * 100 / (d1.Count() * 1f) : 0,

                c2 = d1.Sum(o => o.Score92),
                p2 = d1.Count() > 0 ? d1.Sum(o => o.Score92) * 100 / (d1.Count() * 1f) : 0,

                c3 = d1.Sum(o => o.Score93),
                p3 = d1.Count() > 0 ? d1.Sum(o => o.Score93) * 100 / (d1.Count() * 1f) : 0,
            });

            var summ2 = summary.Where(o => o.type == 2);
            var summAll = summ2.Sum(o => o.c1) + summ2.Sum(o => o.c2) + summ2.Sum(o => o.c3) * 1f;
            summary.Add(new Model1
            {
                type = 3,
                group = "รวม",
                c1 = summ2.Sum(o => o.c1),
                p1 = summAll > 0 ? summ2.Sum(o => o.c1) * 100 / summAll : 0,

                c2 = summ2.Sum(o => o.c2),
                p2 = summAll > 0 ? summ2.Sum(o => o.c2) * 100 / summAll : 0,

                c3 = summ2.Sum(o => o.c3),
                p3 = summAll > 0 ? summ2.Sum(o => o.c3) * 100 / summAll : 0,
            });

            var d3 = logic1.LoadReportStatus(userData.CompanyID, search.term, search.level1, search.level2, "");
            var summary2 = new Model2()
            {
                Count1 = d3.Where(o => o.Status == 1).Count(),
                Count2 = d3.Count(),
            };
            return (summary, summary2);
        }

        private static int CalcSDQNo4(int? score)
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
            else
            {
                return 0;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportExcel(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var (summary, summary2) = GetSummaryData(ctx, search, userData);

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("สรุปผลการประเมิน");

                    var worksheet = excel.Workbook.Worksheets["สรุปผลการประเมิน"];
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
                        , text: $"สรุปผลการประเมิน"
                        );

                        SetCell(worksheet.Cells[row, 1, row++, 7]
                         , isMerge: true
                         , text: $"ปีการศึกษา {search.yearNo} ภาคเรียนที่ {search.termNo}"
                         );
                    }

                    //SetCell(worksheet.Cells[row, 1, row++, 5], isMerge: true, horizotal: ExcelHorizontalAlignment.Left, text: $"รายชื่อนักเรียนทั้งหมด " + data.Count() + " คน");


                    //worksheet.Cells[row, 1, row, 5].IsRichText = true;
                    //var rt = worksheet.Cells[row, 1, row, 5].RichText;
                    //ExcelRichText ert = rt.Add("สถานะการประเมิน : ");
                    ////ert.Bold = true;

                    //ert = rt.Add(" ยังไม่ได้ประเมิน ");
                    //ert.Color = BLACK;
                    //ert = rt.Add(SQUARE_UNICODE);
                    //ert.Color = GRAY;

                    //ert = rt.Add(" ประเมินแล้ว ");
                    //ert.Color = BLACK;
                    //ert = rt.Add(CHECK_UNICODE);
                    //ert.Color = SUCCESS;

                    //row++;



                    SetCell(worksheet.Cells[row, 1, row + 1, 1], isMerge: true, isHeader: true, text: "ด้าน");
                    SetCell(worksheet.Cells[row, 2, row, 3], isMerge: true, isHeader: true, text: "ปกติ");
                    SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, isHeader: true, text: "เสี่ยง");
                    SetCell(worksheet.Cells[row, 6, row++, 7], isMerge: true, isHeader: true, text: "มีปัญหา");

                    SetCell(worksheet.Cells[row, 2], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 3], isHeader: true, text: "ร้อยละ");
                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "ร้อยละ");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row++, 7], isHeader: true, text: "ร้อยละ");

                    foreach (var r in summary.Where(o => o.type == 2))
                    {

                        SetCell(worksheet.Cells[row, 1], text: r.group + "", horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row, 2], text: r.c1 + "");
                        SetCell(worksheet.Cells[row, 3], text: r.p1.ToString("0.00") + "%");
                        SetCell(worksheet.Cells[row, 4], text: r.c2 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.p2.ToString("0.00") + "%");
                        SetCell(worksheet.Cells[row, 6], text: r.c3 + "");
                        SetCell(worksheet.Cells[row, 7], text: r.p3.ToString("0.00") + "%");

                        row++;
                    }

                    SetCell(worksheet.Cells[row, 1], text: "", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row, 2], text: "รวม", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row, 3], text: "ร้อยละ", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row, 4], text: "จำนวน", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row, 5], text: "ร้อยละ", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row, 6], text: "จำนวน", bgColor: BGCOLOR);
                    SetCell(worksheet.Cells[row++, 7], text: "ร้อยละ", bgColor: BGCOLOR);

                    foreach (var r in summary.Where(o => o.type == 3))
                    {
                        SetCell(worksheet.Cells[row, 1], text: "");
                        SetCell(worksheet.Cells[row, 2], text: r.c1 + "");
                        SetCell(worksheet.Cells[row, 3], text: r.p1.ToString("0.00") + "%");
                        SetCell(worksheet.Cells[row, 4], text: r.c2 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.p2.ToString("0.00") + "%");
                        SetCell(worksheet.Cells[row, 6], text: r.c3 + "");
                        SetCell(worksheet.Cells[row, 7], text: r.p3.ToString("0.00") + "%");

                        row++;
                    }

                    row++;

                    SetCell(worksheet.Cells[row, 1, row + 1, 3], isMerge: true, isHeader: true, text: "ด้าน");
                    SetCell(worksheet.Cells[row, 4, row, 5], isMerge: true, isHeader: true, text: "มี");
                    SetCell(worksheet.Cells[row, 6, row++, 7], isMerge: true, isHeader: true, text: "ไม่ชัดเจน");

                    SetCell(worksheet.Cells[row, 4], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row, 5], isHeader: true, text: "ร้อยละ");
                    SetCell(worksheet.Cells[row, 6], isHeader: true, text: "จำนวน");
                    SetCell(worksheet.Cells[row++, 7], isHeader: true, text: "ร้อยละ");

                    foreach (var r in summary.Where(o => o.type == 1))
                    {
                        SetCell(worksheet.Cells[row, 1, row, 3], isMerge: true, text: r.group + "", horizotal: ExcelHorizontalAlignment.Left);
                        SetCell(worksheet.Cells[row, 4], text: r.c1 + "");
                        SetCell(worksheet.Cells[row, 5], text: r.p1.ToString("0.00") + "%");
                        SetCell(worksheet.Cells[row, 6], text: r.c2 + "");
                        SetCell(worksheet.Cells[row, 7], text: r.p2.ToString("0.00") + "%");
                        row++;
                    }

                    worksheet.Cells.AutoFitColumns(30, 40);

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

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportPDF(Search search)
        {
            if (!FontFactory.IsRegistered("thsarabun1"))
            {
                var path = HttpContext.Current.Server.MapPath("~/Fonts/THSarabun.ttf");
                FontFactory.Register(path, "thsarabun1");
            }

            var userData = GetUserData();

            TCompany school;
            string schoolAreas;
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                school = dbmaster.TCompanies.Where(w => w.nCompany == userData.CompanyID)
                    .AsEnumerable()
                    .Select(o => new TCompany { sCompany = o.sCompany , SchoolAreaCode = o.SchoolAreaCode })
                    .FirstOrDefault();

                schoolAreas = dbmaster.TSchoolAreas
                    .Where(o => o.Code == school.SchoolAreaCode)
                    .Select(s => s.Area)
                    .FirstOrDefault();
            }

            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var (summary, summary2) = GetSummaryData(ctx, search, userData);

                string filename = $"สรุปผลการประเมิน_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";

                byte[] docfinal = null;

                Document doc = new Document(PageSize.A4, 30, 30, 25, 25);

                using (MemoryStream finalStream = new MemoryStream())
                {
                    var copy = new PdfCopy(doc, finalStream);
                    doc.Open();

                    using (MemoryStream ms1 = new MemoryStream())
                    {
                        Document doc1 = new Document(PageSize.A4, 30, 30, 25, 25);

                        using (PdfWriter writer1 = PdfWriter.GetInstance(doc1, ms1))
                        {
                            var font = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                            writer1.CloseStream = false;

                            writer1.PageEvent = new ITextEvents();

                            doc1.Open();
                            PdfPTable table1 = new PdfPTable(1);
                            table1.WidthPercentage = 99f;
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , fontStyle: iTextSharp.text.Font.BOLD
                                , text: "ทั้งโรงเรียน\r\n"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"สรุปผลการคัดกรอง"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"{school.sCompany} ปีการศึกษา {search.yearNo}"));                            
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"สำนักงานเขตพื้นที่การศึกษา {schoolAreas} \r\n"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                              , text: "\r\n"));
                            doc1.Add(table1);

                            PdfPTable table2 = new PdfPTable(7);
                            table2.WidthPercentage = 99f;
                            table2.SetTotalWidth(new float[] { 40, 10, 10, 10, 10, 10, 10 });
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้าน", rowspan: 2));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ปกติ", colspan: 2));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "เสี่ยง", colspan: 2));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "มีปัญหา", colspan: 2));

                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "จำนวน"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "จำนวน"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "จำนวน"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ"));

                            foreach (var r in summary.Where(o => o.type == 2))
                            {

                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.group, horizotal: Element.ALIGN_LEFT));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c1 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p1.ToString("0.00") + "%"));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c2 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p2.ToString("0.00") + "%"));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c3 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p3.ToString("0.00") + "%"));
                            }

                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "รวม", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "รวม", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "รวม", bgColor: BGCOLOR));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ", bgColor: BGCOLOR));

                            foreach (var r in summary.Where(o => o.type == 3))
                            {
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c1 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p1.ToString("0.00") + "%"));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c2 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p2.ToString("0.00") + "%"));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.c3 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: r.p3.ToString("0.00") + "%"));
                            }
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n" , border:0 , colspan:7));
                            doc1.Add(table2);

                            PdfPTable table3 = new PdfPTable(5);
                            table3.WidthPercentage = 99f;
                            table3.SetTotalWidth(new float[] { 40, 15, 15, 15, 15 });
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้าน", rowspan: 2));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "มี", colspan: 2));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "ไม่ชัดเจน", colspan: 2));

                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "จำนวน"));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ"));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "จำนวน"));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "ร้อยละ"));

                            foreach (var r in summary.Where(o => o.type == 1))
                            {
                                table3.AddCell(new PdfPCell().SetCellPDF(font, text: r.group, horizotal: Element.ALIGN_LEFT));
                                table3.AddCell(new PdfPCell().SetCellPDF(font, text: r.c1 + ""));
                                table3.AddCell(new PdfPCell().SetCellPDF(font, text: r.p1.ToString("0.00") + "%"));
                                table3.AddCell(new PdfPCell().SetCellPDF(font, text: r.c2 + ""));
                                table3.AddCell(new PdfPCell().SetCellPDF(font, text: r.p2.ToString("0.00") + "%"));
                            }
                            doc1.Add(table3);

                            PdfPTable table4 = new PdfPTable(2);
                            table4.WidthPercentage = 99f;
                            table4.SetTotalWidth(new float[] { 50, 50 });
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0, colspan: 2));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, border: 0, horizotal: Element.ALIGN_LEFT
                                , text: $"จํานวนนักเรียน {summary2.Count1}/{summary2.Count2} คน"));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, border: 0, horizotal: Element.ALIGN_RIGHT
                                , text: "ลงชื่อ.................................................ผู้อํานวยการ"));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "", border: 0));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, border: 0, horizotal: Element.ALIGN_RIGHT
                                , text: "(...........................................................................)"));
                            doc1.Add(table4);
                            doc1.Close();
                        }

                        ms1.Position = 0;
                        var x = new PdfReader(ms1);
                        copy.AddDocument(x);
                        ms1.Dispose();
                    }

                    copy.Close();
                    doc.Close();

                    docfinal = finalStream.ToArray();
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.BinaryWrite(docfinal);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            }
        }

        private static (Color, string) CalcStatus(int? score)
        {
            if (score == 1)
            {
                return (SUCCESS, CHECK_UNICODE);
            }
            else
            {
                return (GRAY, SQUARE_UNICODE);
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
          , Color? bgColor = null
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

                if (bgColor.HasValue)
                {
                    xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    xrange.Style.Fill.BackgroundColor.SetColor(bgColor.Value);
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