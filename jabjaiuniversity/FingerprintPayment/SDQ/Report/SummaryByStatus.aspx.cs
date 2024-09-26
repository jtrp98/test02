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
using FingerprintPayment.Class;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Globalization;
using ScottPlot;
using System.Web.UI.DataVisualization.Charting;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using ScottPlot.Plottable;
using System.Xml.Linq;
using System.Drawing.Drawing2D;

namespace FingerprintPayment.SDQ.Report
{
    public partial class SummaryByStatus : BehaviorGateway
    {

        public class Search
        {
            public string yearNo { get; set; }
            public string term { get; set; }
            public string termNo { get; set; }
            public int? level1 { get; set; }
            public int? level2 { get; set; }
            public int type { get; set; }
        }

        public class SummaryModel
        {
            public string Name { get; set; }
            public int Count1 { get; set; }
            public int Count2 { get; set; }
            public int Count3 { get; set; }
            public int Count4 { get; set; }
            public int Count5 { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static int? CalcTypeScore(int? type, int? group, int? score)
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
                        return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static int? CalcGroupScore1(int? group, int? score)
        {
            switch (group)
            {
                case 1:
                    if (score >= 0 && score <= 5)
                        return 1;
                    else if (score == 6)
                        return 2;
                    else
                        return 3;
                case 2:
                    if (score >= 0 && score <= 4)
                        return 1;
                    else if (score == 5)
                        return 2;
                    else
                        return 3;
                case 3:
                    if (score >= 0 && score <= 5)
                        return 1;
                    else if (score == 6)
                        return 2;
                    else
                        return 3;
                case 4:
                    if (score >= 0 && score <= 3)
                        return 1;
                    else if (score == 4)
                        return 2;
                    else
                        return 3;
                case 5:
                    if (score >= 4 && score <= 10)
                        return 4;
                    else
                        return 5;
                //case 6:
                //    if (score >= 0 && score <= 16)
                //        return 1;
                //    else if (score >= 17 && score <= 18)
                //        return 2;
                //    else
                //        return 3;

                default:
                    return null;
            }
        }

        public static int? CalcGroupScore2(int? group, int? score)
        {
            switch (group)
            {
                case 1:
                    if (score >= 0 && score <= 3)
                        return 1;
                    else if (score == 4)
                        return 2;
                    else
                        return 3;
                case 2:
                    if (score >= 0 && score <= 3)
                        return 1;
                    else if (score == 4)
                        return 2;
                    else
                        return 3;
                case 3:
                    if (score >= 0 && score <= 5)
                        return 1;
                    else if (score == 6)
                        return 2;
                    else
                        return 3;
                case 4:
                    if (score >= 0 && score <= 5)
                        return 1;
                    else if (score == 6)
                        return 2;
                    else
                        return 3;
                case 5:
                    if (score >= 4 && score <= 10)
                        return 4;
                    else
                        return 5;
                //case 6:
                //    if (score >= 0 && score <= 15)
                //        return 1;
                //    else if (score >= 16 && score <= 17)
                //        return 2;
                //    else
                //        return 3;
                default:
                    return null;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object LoadData(Search search)
        {
            var userData = GetUserData();
            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, "", search.type);

                var d = result.Select(o => new
                {
                    Name = "",
                    o.sId,
                    Score1 = CalcTypeScore(search.type, 1, o.Score1),
                    Score2 = CalcTypeScore(search.type, 2, o.Score2),
                    Score3 = CalcTypeScore(search.type, 3, o.Score3),
                    Score4 = CalcTypeScore(search.type, 4, o.Score4),
                    Score5 = CalcTypeScore(search.type, 5, o.Score5),
                });

                var info = new List<SummaryModel> {
                        new SummaryModel {
                            Name = "ปกติ" ,
                            Count1 = d.Count( o => o.Score1 == 1),
                            Count2 = d.Count( o => o.Score2 == 1),
                            Count3 = d.Count( o => o.Score3 == 1),
                            Count4 = d.Count( o => o.Score4 == 1),
                            Count5 = d.Count( o => o.Score5 == 1),
                        },

                        new SummaryModel{
                            Name = "มีปัญหา" ,
                            Count1 = d.Count( o => o.Score1 == 3),
                            Count2 = d.Count( o => o.Score2 == 3),
                            Count3 = d.Count( o => o.Score3 == 3),
                            Count4 = d.Count( o => o.Score4 == 3),
                            Count5 = d.Count( o => o.Score5 == 3),
                        },

                        new SummaryModel{
                            Name = "เสี่ยง" ,
                            Count1 = d.Count( o => o.Score1 == 2),
                            Count2 = d.Count( o => o.Score2 == 2),
                            Count3 = d.Count( o => o.Score3 == 2),
                            Count4 = d.Count( o => o.Score4 == 2),
                            Count5 = d.Count( o => o.Score5 == 2),
                        },

                        new SummaryModel {
                            Name = "มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 4),
                            Count2 = d.Count( o => o.Score2 == 4),
                            Count3 = d.Count( o => o.Score3 == 4),
                            Count4 = d.Count( o => o.Score4 == 4),
                            Count5 = d.Count( o => o.Score5 == 4),
                        },

                        new SummaryModel {
                            Name = "ไม่มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 5),
                            Count2 = d.Count( o => o.Score2 == 5),
                            Count3 = d.Count( o => o.Score3 == 5),
                            Count4 = d.Count( o => o.Score4 == 5),
                            Count5 = d.Count( o => o.Score5 == 5),
                        },

                    };

                return new
                {
                    data = info,

                    summary = new
                    {
                        Count1 = info.Sum(o => o.Count1) + "/" + d.Count(),
                        Count2 = info.Sum(o => o.Count2) + "/" + d.Count(),
                        Count3 = info.Sum(o => o.Count3) + "/" + d.Count(),
                        Count4 = info.Sum(o => o.Count4) + "/" + d.Count(),
                        Count5 = info.Sum(o => o.Count5) + "/" + d.Count(),
                    },

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
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, search.level1, search.level2, "", search.type);

                var d = result.Select(o => new
                {
                    Name = "",
                    o.sId,
                    Score1 = CalcTypeScore(search.type, 1, o.Score1),
                    Score2 = CalcTypeScore(search.type, 2, o.Score2),
                    Score3 = CalcTypeScore(search.type, 3, o.Score3),
                    Score4 = CalcTypeScore(search.type, 4, o.Score4),
                    Score5 = CalcTypeScore(search.type, 5, o.Score5),
                });

                var info = new List<SummaryModel> {

                        new SummaryModel {
                            Name = "ปกติ" ,
                            Count1 = d.Count( o => o.Score1 == 1),
                            Count2 = d.Count( o => o.Score2 == 1),
                            Count3 = d.Count( o => o.Score3 == 1),
                            Count4 = d.Count( o => o.Score4 == 1),
                            Count5 = d.Count( o => o.Score5 == 1),
                        },

                        new SummaryModel{
                            Name = "มีปัญหา" ,
                            Count1 = d.Count( o => o.Score1 == 3),
                            Count2 = d.Count( o => o.Score2 == 3),
                            Count3 = d.Count( o => o.Score3 == 3),
                            Count4 = d.Count( o => o.Score4 == 3),
                            Count5 = d.Count( o => o.Score5 == 3),
                        },

                        new SummaryModel{
                            Name = "เสี่ยง" ,
                            Count1 = d.Count( o => o.Score1 == 2),
                            Count2 = d.Count( o => o.Score2 == 2),
                            Count3 = d.Count( o => o.Score3 == 2),
                            Count4 = d.Count( o => o.Score4 == 2),
                            Count5 = d.Count( o => o.Score5 == 2),
                        },

                        new SummaryModel {
                            Name = "มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 4),
                            Count2 = d.Count( o => o.Score2 == 4),
                            Count3 = d.Count( o => o.Score3 == 4),
                            Count4 = d.Count( o => o.Score4 == 4),
                            Count5 = d.Count( o => o.Score5 == 4),
                        },

                        new SummaryModel {
                            Name = "ไม่มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 5),
                            Count2 = d.Count( o => o.Score2 == 5),
                            Count3 = d.Count( o => o.Score3 == 5),
                            Count4 = d.Count( o => o.Score4 == 5),
                            Count5 = d.Count( o => o.Score5 == 5),
                        },

                    };

                var data = new
                {
                    data = info,
                    summary = new
                    {
                        Count1 = info.Sum(o => o.Count1) + "/" + d.Count(),
                        Count2 = info.Sum(o => o.Count2) + "/" + d.Count(),
                        Count3 = info.Sum(o => o.Count3) + "/" + d.Count(),
                        Count4 = info.Sum(o => o.Count4) + "/" + d.Count(),
                        Count5 = info.Sum(o => o.Count5) + "/" + d.Count(),
                    },
                };

                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("รายงานสรุปผลรวม");

                    var worksheet = excel.Workbook.Worksheets["รายงานสรุปผลรวม"];
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    var row = 1;

                    using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        var school = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);

                        worksheet.Cells[row, 1, row++, 6].SetCellRange(isMerge: true
                          , text: school.sCompany
                          , fontSize: 14
                          , isBold: true);

                        worksheet.Cells[row, 1, row++, 6].SetCellRange(isMerge: true, text: $"รายงานสรุปผลรายบุคคล");

                        worksheet.Cells[row, 1, row++, 6].SetCellRange(isMerge: true
                            , text: $"ปีการศึกษา {search.yearNo} ภาคเรียนที่ {search.termNo}"
                         );
                    }

                    worksheet.Cells[row, 1].SetCellRange(isMerge: true, isHeader: true, text: "แปลผล");
                    worksheet.Cells[row, 2].SetCellRange(isMerge: true, isHeader: true, text: "ด้านอารมณ์");
                    worksheet.Cells[row, 3].SetCellRange(isMerge: true, isHeader: true, text: "ด้านความประพฤติ");
                    worksheet.Cells[row, 4].SetCellRange(isMerge: true, isHeader: true, text: "ด้านพฤติกรรมไม่อยู่นิ่ง");
                    worksheet.Cells[row, 5].SetCellRange(isMerge: true, isHeader: true, text: "ด้านความสัมพันธ์กับเพื่อน");
                    worksheet.Cells[row, 6].SetCellRange(isMerge: true, isHeader: true, text: "ด้านสัมพันธภาพทางสังคม");
                    row++;

                    foreach (var r in data.data)
                    {
                        worksheet.Cells[row, 1].SetCellRange(text: r.Name + "");
                        worksheet.Cells[row, 2].SetCellRange(text: r.Count1 + "");
                        worksheet.Cells[row, 3].SetCellRange(text: r.Count2 + "");
                        worksheet.Cells[row, 4].SetCellRange(text: r.Count3 + "");
                        worksheet.Cells[row, 5].SetCellRange(text: r.Count4 + "");
                        worksheet.Cells[row, 6].SetCellRange(text: r.Count5 + "");

                        row++;
                    }

                    worksheet.Cells[row, 1].SetCellRange(text: "รวมทั้งสิ้น", isBold: true);
                    worksheet.Cells[row, 2].SetCellRange(text: data.summary.Count1 + "");
                    worksheet.Cells[row, 3].SetCellRange(text: data.summary.Count2 + "");
                    worksheet.Cells[row, 4].SetCellRange(text: data.summary.Count3 + "");
                    worksheet.Cells[row, 5].SetCellRange(text: data.summary.Count4 + "");
                    worksheet.Cells[row, 6].SetCellRange(text: data.summary.Count5 + "");
                    row++;

                    worksheet.Cells.AutoFitColumns(30, 50);

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
                    .Select(o => new TCompany { sCompany = o.sCompany, SchoolAreaCode = o.SchoolAreaCode })
                    .FirstOrDefault();

                schoolAreas = dbmaster.TSchoolAreas
                 .Where(o => o.Code == school.SchoolAreaCode)
                 .Select(s => s.Area)
                 .FirstOrDefault();
            }

            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var logic = new SDQRepository(ctx);

                var result = logic.LoadReportStudent(userData.CompanyID, search.term, null, null, "", search.type);

                var d = result.Select(o => new
                {
                    Name = "",
                    o.sId,
                    Score1 = CalcTypeScore(search.type, 1, o.Score1),
                    Score2 = CalcTypeScore(search.type, 2, o.Score2),
                    Score3 = CalcTypeScore(search.type, 3, o.Score3),
                    Score4 = CalcTypeScore(search.type, 4, o.Score4),
                    Score5 = CalcTypeScore(search.type, 5, o.Score5),
                });

                var info = new List<SummaryModel> {
                         new SummaryModel {
                            Name = "ปกติ" ,
                            Count1 = d.Count( o => o.Score1 == 1),
                            Count2 = d.Count( o => o.Score2 == 1),
                            Count3 = d.Count( o => o.Score3 == 1),
                            Count4 = d.Count( o => o.Score4 == 1),
                            Count5 = d.Count( o => o.Score5 == 1),
                        },

                        new SummaryModel{
                            Name = "มีปัญหา" ,
                            Count1 = d.Count( o => o.Score1 == 3),
                            Count2 = d.Count( o => o.Score2 == 3),
                            Count3 = d.Count( o => o.Score3 == 3),
                            Count4 = d.Count( o => o.Score4 == 3),
                            Count5 = d.Count( o => o.Score5 == 3),
                        },

                        new SummaryModel{
                            Name = "เสี่ยง" ,
                            Count1 = d.Count( o => o.Score1 == 2),
                            Count2 = d.Count( o => o.Score2 == 2),
                            Count3 = d.Count( o => o.Score3 == 2),
                            Count4 = d.Count( o => o.Score4 == 2),
                            Count5 = d.Count( o => o.Score5 == 2),
                        },

                        new SummaryModel {
                            Name = "มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 4),
                            Count2 = d.Count( o => o.Score2 == 4),
                            Count3 = d.Count( o => o.Score3 == 4),
                            Count4 = d.Count( o => o.Score4 == 4),
                            Count5 = d.Count( o => o.Score5 == 4),
                        },

                        new SummaryModel {
                            Name = "ไม่มีจุดแข็ง" ,
                            Count1 = d.Count( o => o.Score1 == 5),
                            Count2 = d.Count( o => o.Score2 == 5),
                            Count3 = d.Count( o => o.Score3 == 5),
                            Count4 = d.Count( o => o.Score4 == 5),
                            Count5 = d.Count( o => o.Score5 == 5),
                        },


                    };

                var data = new
                {
                    data = info,
                    summary = new
                    {
                        Count1 = info.Sum(o => o.Count1) + "/" + d.Count(),
                        Count2 = info.Sum(o => o.Count2) + "/" + d.Count(),
                        Count3 = info.Sum(o => o.Count3) + "/" + d.Count(),
                        Count4 = info.Sum(o => o.Count4) + "/" + d.Count(),
                        Count5 = info.Sum(o => o.Count5) + "/" + d.Count(),
                    },
                };

                string filename = $"รายงานไม่มีสิทธ์สอบ_{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";

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

                            var type = "";
                            switch (search.type)
                            {
                                case 1:
                                    type = "ฉบับนักเรียนประเมินตนเอง";
                                    break;

                                case 2:
                                    type = "ฉบับครูประเมินนักเรียน";
                                    break;
                                case 3:
                                    type = "ฉบับผู้ปกครองประเมินนักเรียน";
                                    break;

                                default:
                                    break;
                            }
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: "สรุปผลการประเมินพฤติกรรมนักเรียน (SDQ) เป็นรายบุคคล " + type));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"{school.sCompany} ปีการศึกษา {search.yearNo}"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"สํานักงานเขตพื้นที่การศึกษา {schoolAreas}"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: "\r\n"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                                , text: $"สรุปการแปลผลในภาพรวมของการประเมินพฤติกรรมนักเรียน"));
                            table1.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                              , text: "\r\n"));
                            doc1.Add(table1);

                            PdfPTable table2 = new PdfPTable(6);
                            table2.WidthPercentage = 99f;
                            var lstWidth = new List<float>(new float[] { 14, 16, 16, 16, 16, 16 });
                            table2.SetTotalWidth(lstWidth.ToArray());

                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "แปลผล"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้านอารมณ์"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้านความประพฤติ"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้านพฤติกรรมไม่\r\nอยู่นิ่ง"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้านความสัมพันธ์\r\nกับเพือน"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "ด้านสัมพันธภาพ\r\nทางสังคม"));
                            var _data = data.data;
                            for (int row = 0; row < _data.Count; row++)
                            {
                                var s = _data[row];

                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Name + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Count1 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Count2 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Count3 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Count4 + ""));
                                table2.AddCell(new PdfPCell().SetCellPDF(font, text: s.Count5 + ""));

                            }

                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: "รวม"));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: data.summary.Count1));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: data.summary.Count2));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: data.summary.Count3));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: data.summary.Count4));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, text: data.summary.Count5));

                            table2.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                              , text: "\r\n\r\n", colspan: 6));
                            table2.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                             , text: "สรุปการแปลผลในภาพรวมของการประเมินพฤติกรรมนักเรียน", colspan: 6));
                            //table2.AddCell(new PdfPCell().SetCellPDF(font, border: 0
                            //  , text: "\r\n", colspan: 6));

                            doc1.Add(table2);

                            var img1 = iTextSharp.text.Image.GetInstance(GetChart1(_data)
                                , System.Drawing.Imaging.ImageFormat.Png);
                            img1.ScaleAbsolute(500, 300);
                            doc1.Add(img1);

                            PdfPTable table3 = new PdfPTable(1);
                            table3.WidthPercentage = 99f;

                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));

                            //table4.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                            PdfPTable table4 = new PdfPTable(5);
                            table4.WidthPercentage = 99f;
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "สีเขียว = ปกติ", border: 0));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "สีส้ม = เสี่ยง", border: 0));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "สีแดง = มีปัญหา", border: 0));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "สีน้ำเงิน = มีจุดแข็ง", border: 0));
                            table4.AddCell(new PdfPCell().SetCellPDF(font, text: "สีม่วง = ไม่มีจุดแข็ง", border: 0));

                            table3.AddCell(new PdfPCell(table4).SetCellPDF(font, text: ""));
                            table3.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));
                            //table3.AddCell(new PdfPCell().SetCellPDF(font,
                            //    text: "** หมายเหตุสัมพันธภาพทางสังคม การแปลผลปกติ = มีจุดแข็ง, มีปัญหา = ไม่มีจุดแข็ง", border: 0));
                            //table3.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));
                            doc1.Add(table3);
                            doc1.NewPage();

                            PdfPTable table5 = new PdfPTable(1);
                            table5.WidthPercentage = 99f;
                            table5.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));
                            table5.AddCell(new PdfPCell().SetCellPDF(font, text: "สรุปภาพรวมทัง 5 ด้านของการประเมินพฤติกรรมนักเรียน", border: 0));
                            table5.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));
                            var img2 = iTextSharp.text.Image.GetInstance(GetChart2(_data)
                                , System.Drawing.Imaging.ImageFormat.Png);
                            img2.ScaleAbsolute(500, 300);
                            table5.AddCell(new PdfPCell(img2).SetCellPDF(font, text: "", border: 0));
                            table5.AddCell(new PdfPCell().SetCellPDF(font, text: "\r\n", border: 0));
                            doc1.Add(table5);

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

        private static Bitmap GetChart1(List<SummaryModel> _data)
        {
            var plt = new Plot(500, 300);

            var v1 = _data.Where(o => o.Name == "ปกติ")
                .SelectMany(o => new double[] { o.Count1, o.Count2, o.Count3, o.Count4, o.Count5 })
                .ToArray();
            var v2 = _data.Where(o => o.Name == "เสี่ยง")
                .SelectMany(o => new double[] { o.Count1, o.Count2, o.Count3, o.Count4, o.Count5 })
                .ToArray();
            var v3 = _data.Where(o => o.Name == "มีปัญหา")
                 .SelectMany(o => new double[] { o.Count1, o.Count2, o.Count3, o.Count4, o.Count5 })
                 .ToArray();
            var v4 = _data.Where(o => o.Name == "มีจุดแข็ง")
               .SelectMany(o => new double[] { o.Count1, o.Count2, o.Count3, o.Count4, o.Count5 })
               .ToArray();
            var v5 = _data.Where(o => o.Name == "ไม่มีจุดแข็ง")
               .SelectMany(o => new double[] { o.Count1, o.Count2, o.Count3, o.Count4, o.Count5 })
               .ToArray();

            var err1 = DataGen.RandomNormal(0, 5, 0, 0);
            string[] groupNames = { "ด้านอารมณ์", "ด้านความประพฤติ", "ด้านพฤติกรรมไม่อยู่นิ่ง", "ด้านความสัมพันธ์กับเพือน", "ด้านสัมพันธภาพทางสังคม" };
            string[] seriesNames = { "ปกติ", "เสี่ยง", "มีปัญหา", "มีจุดแข็ง", "ไม่มีจุดแข็ง" };
            double[][] valuesBySeries = { v1, v2, v3, v4, v5 };
            double[][] errorsBySeries = { err1, err1, err1, err1, err1 };
            var bar = plt.AddBarGroups(groupNames, seriesNames, valuesBySeries, errorsBySeries);
            bar[0].Color = Color.FromArgb(0, 128, 0);
            bar[1].Color = Color.FromArgb(255, 168, 0);
            bar[2].Color = Color.FromArgb(255, 0, 0);
            bar[3].Color = Color.FromArgb(51, 51, 255);
            bar[4].Color = Color.FromArgb(204, 51, 255);

            //plt.Legend(location: Alignment.UpperRight);
            var max = new double[] { v1.Max(), v2.Max(), v3.Max(), v4.Max(), v5.Max() }.Max();
            plt.SetAxisLimits(yMin: 0, yMax: max + (0.1f * max));
            //plt.Title("สรุปการแปลผลในภาพรวมของการประเมินพฤติกรรมนักเรียน", bold: false, size: 14, fontName: "thsarabun1");
            plt.XAxis.TickLabelStyle(rotation: 40);
            var img = plt.GetBitmap(scale: 5);// ScaleImage(, 300);
            return img;


        }

        //public static Bitmap ScaleImage(Bitmap image, int height)
        //{
        //    double ratio = (double)height / image.Height;
        //    int newWidth = (int)(image.Width * ratio);
        //    int newHeight = (int)(image.Height * ratio);
        //    Bitmap newImage = new Bitmap(newWidth, newHeight);
        //    using (Graphics g = Graphics.FromImage(newImage))
        //    {
        //        g.DrawImage(image, 0, 0, newWidth, newHeight);
        //    }
        //    image.Dispose();
        //    return newImage;
        //}

        //public static Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        //{
        //    Bitmap result = new Bitmap(width, height);
        //    using (Graphics g = Graphics.FromImage(result))
        //    {
        //        g.DrawImage(bmp, 0, 0, width, height);
        //        g.InterpolationMode = InterpolationMode.High;
        //        g.CompositingQuality = CompositingQuality.HighQuality;
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //    }

        //    return result;
        //}

        private static Bitmap GetChart2(List<SummaryModel> _data)
        {

            var plt = new Plot(500, 300);

            var v1 = _data.Where(o => o.Name == "ปกติ")
                .Select(o => o.Count1 + o.Count2 + o.Count3 + o.Count4 + o.Count5).FirstOrDefault() * 1d;
            var v2 = _data.Where(o => o.Name == "เสี่ยง")
                .Select(o => o.Count1 + o.Count2 + o.Count3 + o.Count4 + o.Count5).FirstOrDefault() * 1d;
            var v3 = _data.Where(o => o.Name == "มีปัญหา")
                .Select(o => o.Count1 + o.Count2 + o.Count3 + o.Count4 + o.Count5).FirstOrDefault() * 1d;
            var v4 = _data.Where(o => o.Name == "มีจุดแข็ง")
             .Select(o => o.Count1 + o.Count2 + o.Count3 + o.Count4 + o.Count5).FirstOrDefault() * 1d;
            var v5 = _data.Where(o => o.Name == "ไม่มีจุดแข็ง")
             .Select(o => o.Count1 + o.Count2 + o.Count3 + o.Count4 + o.Count5).FirstOrDefault() * 1d;

            var bars = new List<Bar>();
            bars.Add(new Bar()
            {
                Value = v1,
                Position = 0,
                FillColor = Color.FromArgb(0, 128, 0),
                LineWidth = 1,
                Thickness = 0.2

            });

            bars.Add(new Bar()
            {
                Value = v2,
                Position = 1,
                FillColor = Color.FromArgb(255, 168, 0),
                LineWidth = 1,
                Thickness = 0.2
            });

            bars.Add(new Bar()
            {
                Value = v3,
                Position = 2,
                FillColor = Color.FromArgb(255, 0, 0),
                LineWidth = 1,
                Thickness = 0.2

            });

            bars.Add(new Bar()
            {
                Value = v4,
                Position = 3,
                FillColor = Color.FromArgb(51, 51, 255),
                LineWidth = 1,
                Thickness = 0.2

            });

            bars.Add(new Bar()
            {
                Value = v5,
                Position = 4,
                FillColor = Color.FromArgb(204, 51, 255),
                LineWidth = 1,
                Thickness = 0.2

            });


            double[] positions = { 0, 1, 2, 3, 4 };
            string[] labels = { "ปกติ", "เสี่ยง", "มีปัญหา", "มีจุดแข็ง", "ไม่มีจุดแข็ง" };
            var x = plt.AddBarSeries(bars);
            var max = new double[] { v1, v2, v3, v4, v5 }.Max();
            plt.SetAxisLimits(yMin: 0, yMax: max + (0.1f * max));
            plt.XTicks(positions, labels);

            var img = plt.GetBitmap(scale: 5);// ScaleImage();
            return img;
        }

        //  private static void ExcelRange xrange
        //    , bool isHeader = false
        //    , bool isMerge = false
        //    , string text = ""
        //    , int fontSize = 11
        //    , bool isBold = false
        //    , ExcelHorizontalAlignment horizotal = ExcelHorizontalAlignment.Center
        //    , ExcelVerticalAlignment vetical = ExcelVerticalAlignment.Center
        //    , Color? color = null
        //)
        //  {
        //      using (xrange)
        //      {
        //          xrange.Merge = isMerge;
        //          xrange.Value = text;
        //          xrange.Style.Font.Bold = isBold;
        //          xrange.Style.HorizontalAlignment = horizotal;
        //          xrange.Style.VerticalAlignment = vetical;
        //          xrange.Style.Font.Size = fontSize;

        //          if (isHeader)
        //          {
        //              xrange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
        //              xrange.Style.Fill.PatternType = ExcelFillStyle.Solid;
        //              xrange.Style.Font.Color.SetColor(System.Drawing.Color.White);
        //              xrange.Style.Fill.BackgroundColor.SetColor(0, 51, 122, 183);
        //          }

        //          if (color.HasValue)
        //          {
        //              xrange.Style.Font.Color.SetColor(color.Value);
        //          }

        //          xrange.AutoFitColumns();

        //      }
        //  }

        public class ITextEvents : PdfPageEventHelper
        {
            //string summary1, summary2;
            TCompany school;
            public ITextEvents()
            {
            }
            // This is the contentbyte object of the writer  
            PdfContentByte cb;

            // we will put the final number of pages in a template  
            PdfTemplate headerTemplate, footerTemplate;

            // this is the BaseFont we are going to use for the header / footer  
            BaseFont bf = null;

            // This keeps track of the creation time  
            DateTime PrintTime = DateTime.Now;
            string dateNow = "";
            #region Fields  
            private string _header;
            #endregion

            #region Properties  
            public string Header
            {
                get { return _header; }
                set { _header = value; }
            }
            #endregion

            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    dateNow = DateTime.Now.ToString("d MMMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));
                    //bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    iTextSharp.text.Font f = FontFactory.GetFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14, iTextSharp.text.Font.NORMAL);
                    //bf = BaseFont.CreateFont("thsarabun1", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    bf = f.BaseFont;
                    cb = writer.DirectContent;
                    headerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                    footerTemplate = cb.CreateTemplate(document.PageSize.Width - 100, 50);
                }
                catch (DocumentException de)
                {
                }
                catch (System.IO.IOException ioe)
                {
                }
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);

                //Add paging to footer  
                {
                    cb.BeginText();
                    cb.SetFontAndSize(bf, 10);

                    cb.SetTextMatrix(document.PageSize.GetLeft(30), document.PageSize.GetBottom(5));
                    var text1 = $"รายงานข้อมูล ณ วันที่ {dateNow}";
                    cb.ShowText(text1);

                    cb.SetTextMatrix(document.PageSize.GetRight(30), document.PageSize.GetBottom(5));
                    var text2 = $"{writer.PageNumber}/";
                    cb.ShowText(text2);
                    cb.EndText();
                    float len = bf.GetWidthPoint(text2, 10);
                    // cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(30));document.PageSize.GetRight(20)
                    cb.AddTemplate(footerTemplate, document.PageSize.GetRight(30) + len, document.PageSize.GetBottom(5));// document.PageSize.GetBottom(20)
                }
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);

                //headerTemplate.BeginText();
                //headerTemplate.SetFontAndSize(bf, 12);
                //headerTemplate.SetTextMatrix(0, 0);
                //headerTemplate.ShowText((writer.PageNumber - 1).ToString());
                //headerTemplate.EndText();

                footerTemplate.BeginText();
                footerTemplate.SetFontAndSize(bf, 10);
                footerTemplate.SetTextMatrix(0, 0);
                footerTemplate.ShowText((writer.PageNumber) + "");
                footerTemplate.EndText();
            }
        }
    }
}