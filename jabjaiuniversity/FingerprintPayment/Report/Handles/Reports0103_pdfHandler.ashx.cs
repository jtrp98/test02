using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using System;
using System.Web;
using FingerprintPayment.Report.Functions.Reports_03;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using AjaxControlToolkit.MaskedEditValidatorCompatibility;
using Org.BouncyCastle.Crypto.Tls;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for Reports03_pdfHandler
    /// </summary>
    public class Reports0103_pdfHandler : IHttpHandler, IRequiresSessionState
    {
        // Bold
        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font h1 = new Font(bf_bold, 14);
        private Font bold = new Font(bf_bold, 12);
        private Font smallBold = new Font(bf_bold, 8);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font normal = new Font(bf_normal, 10);
        private Font smallNormal = new Font(bf_normal, 8);
        private Font small = new Font(bf_normal, 6);
        private Font smallNormal_Black = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
        private Font Header_smallNormal = new Font(bf_normal, 8, 0 /*,new BaseColor(255, 255, 255)*/);

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                // Create PDF document
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Example.pdf");

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);

                pdfDoc.AddAuthor("Me");

                pdfDoc.Open();

                string ReportType = context.Request.QueryString["ReportType"] as string;
                var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                var search = JsonConvert.DeserializeObject<Reportmobile01.Search>(jsonString);

                if (!search.dStart.HasValue)
                {
                    search.dStart = DateTime.Today;
                    search.dEnd = DateTime.Today;
                }

                var q = Reportmobile01.report02UsersView02(search);
                string report_name = search.dStart.Value.ToString("รายงานสรุปสถิติการมาโรงเรียนประจำวันที่ dd MMM yyyy", new CultureInfo("th-th"));
                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var f_company = masterEntities.TCompanies.Find(userData.CompanyID);

                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                    {
                        string SchoolHeadName = "";
                        if (f_company.nSchoolHeadid.HasValue)
                        {
                            var employee = entities.TEmployees.FirstOrDefault(f => f.sEmp == f_company.nSchoolHeadid.Value);
                            if (employee != null)
                            {
                                int sTitle = 0;
                                int.TryParse(employee.sTitle, out sTitle);
                                if (sTitle == 0)
                                {
                                    SchoolHeadName = employee.sTitle + " ";
                                }
                                else
                                {
                                    var f2 = entities.TTitleLists.FirstOrDefault(f => f.nTitleid == sTitle);
                                    SchoolHeadName = f2.titleDescription + " ";
                                }
                                SchoolHeadName += employee.sName + " " + employee.sLastname;
                            }
                        }

                        pdfDoc.Add(GetHeader(f_company.sCompany, report_name, search, entities, userData));
                        int[] wantedRows = { 0, 2, 3 };
                        float RowHeights = 0;
                        float contentHeight = 0;
                        if (ReportType == "2")
                        {
                            var table = GetBody(q);
                            contentHeight = table.TotalHeight;
                            //RowHeights = TotalRowHeights(pdfDoc, pdfWriter.DirectContent, table, wantedRows);
                            pdfDoc.Add(table);
                            //Check Empty Page
                            //RowHeights += 200;
                            //if (RowHeights % 1123 >= 800)
                            //{
                            //    pdfDoc.NewPage();
                            //}
                        }
                        else if (ReportType == "4")
                        {
                            var table = GetBody_T04(q);
                            contentHeight = table.TotalHeight;
                            //RowHeights = TotalRowHeights(pdfDoc, pdfWriter.DirectContent, table, wantedRows);
                            pdfDoc.Add(table);
                            //Check Empty Page
                            //RowHeights += 200;
                            //if (RowHeights % 1123 >= 800)
                            //{
                            //    pdfDoc.NewPage();
                            //}
                        }
                        var footer = GetFooter(SchoolHeadName, f_company.sCompany);

                        var footerHeight = footer.TotalHeight;
                        float space = 0;// pdfDoc.PageSize.Height - contentHeight;
                        if (pdfDoc.PageSize.Height > contentHeight)
                        {
                            space = pdfDoc.PageSize.Height - contentHeight;
                        }
                        else
                        {
                            do
                            {
                                contentHeight = contentHeight - pdfDoc.PageSize.Height;
                            } while (contentHeight >= pdfDoc.PageSize.Height);
                            space = pdfDoc.PageSize.Height - contentHeight;
                        }
                        if (footerHeight >= space)
                        {
                            pdfDoc.NewPage();
                        }
                        pdfDoc.Add(footer);
                    }
                }

                pdfDoc.Close();

                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
                //HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }

        private PdfPTable GetHeader(string school_name, string report_name, Reportmobile01.Search search, JabJaiEntities entities, JWTToken.userData userData)
        {

            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;

            float[] WidthPercentage = new float[] { 25, 25, 25, 25 };
            table.SetTotalWidth(WidthPercentage);

            string SubLevel = "ทั้งหมด", SubLevel2 = "ทั้งหมด";

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase(school_name, h1)), Element.ALIGN_CENTER, 4, 1));

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase(report_name, smallBold)), Element.ALIGN_CENTER, 4, 1));

            if (search.sort_type != 2)
            {
                StudentLogic logic = new StudentLogic(entities);
                search.term_id = logic.GetTermId(search.dStart.Value, userData);
            }

            if (search.level_id.HasValue)
            {
                var f_1 = entities.TSubLevels.FirstOrDefault(f => f.nTSubLevel == search.level_id.Value);
                SubLevel = f_1.SubLevel;
            }

            //var f_0 =(from a in  entities.TB_StudentViews.FirstOrDefault(f => f.nTerm == search.term_id && (!search.level_id.HasValue || f.nTSubLevel == search.level_id));
            var f_0 = (from a in entities.TTerms.Where(w => w.SchoolID == userData.CompanyID)
                       join b in entities.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.nYear equals b.nYear
                       where a.nTerm == search.term_id
                       select new { a.sTerm, b.numberYear }).FirstOrDefault();



            table.AddCell(AddCell(new PdfPCell(new Phrase("ปีการศึกษา :", smallBold)), Element.ALIGN_RIGHT));
            table.AddCell(AddCell(new PdfPCell(new Phrase(f_0.numberYear + "", smallBold)), Element.ALIGN_LEFT));
            table.AddCell(AddCell(new PdfPCell(new Phrase("เทอม :", smallBold)), Element.ALIGN_RIGHT));
            table.AddCell(AddCell(new PdfPCell(new Phrase(f_0.sTerm, smallBold)), Element.ALIGN_LEFT));

            table.AddCell(AddCell(new PdfPCell(new Phrase("ระดับชั้นเรียน :", smallBold)), Element.ALIGN_RIGHT));
            table.AddCell(AddCell(new PdfPCell(new Phrase(SubLevel, smallBold)), Element.ALIGN_LEFT));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ชั้นเรียน :", smallBold)), Element.ALIGN_RIGHT));
            table.AddCell(AddCell(new PdfPCell(new Phrase(SubLevel2, smallBold)), Element.ALIGN_LEFT));

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase("พิมพ์วันที่ :", smallBold)), Element.ALIGN_RIGHT, 3, 1));
            table.AddCell(AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString("dd MMM yyyy เวลา : HH:MM:ss น." + Environment.NewLine, new CultureInfo("th-th")), smallBold)), Element.ALIGN_LEFT));

            //table.AddCell(AddCellHeader(new PdfPCell(new Phrase("", smallBold)), Element.ALIGN_RIGHT, 3, 1));
            //table.AddCell(AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString("เวลา : HH:MM:ss น.", new CultureInfo("th-th")), smallBold)), Element.ALIGN_LEFT));

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase(" ", smallBold)), Element.ALIGN_RIGHT, 4, 1));

            return table;
        }

        private PdfPTable GetFooter(string SchoolHead, string SchoolName)
        {

            PdfPTable table = new PdfPTable(3);
            table.WidthPercentage = 100;

            float[] WidthPercentage = new float[] { 30, 35, 35 };
            table.SetTotalWidth(WidthPercentage);

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase(" ", smallBold)), Element.ALIGN_CENTER, 3, 1));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ครูที่รับผิดชอบประจำวัน", smallBold)), Element.ALIGN_RIGHT));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ..................................................", smallBold)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ..................................................", smallBold)), Element.ALIGN_CENTER));

            table.AddCell(AddCell(new PdfPCell(new Phrase("", smallBold)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("(.....................................................)", smallBold)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("(.....................................................)", smallBold)), Element.ALIGN_CENTER));

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase(" ", smallBold)), Element.ALIGN_CENTER, 3, 1));

            table.AddCell(AddCellHeader(new PdfPCell(new Phrase("รับทราบ ลงชื่อ.......................................................", smallBold)), Element.ALIGN_CENTER, 3, 1));
            table.AddCell(AddCellHeader(new PdfPCell(new Phrase("(" + (SchoolHead ?? "                                              ") + ")", smallBold)), Element.ALIGN_CENTER, 3, 1));
            table.AddCell(AddCellHeader(new PdfPCell(new Phrase("ผู้อำนวยการ" + SchoolName, smallBold)), Element.ALIGN_CENTER, 3, 1));

            return table;
        }


        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }

        private PdfPCell AddCellHeader(PdfPCell pdfPCell, int horizontalAlignment, int colspan, int rowspan)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.Rowspan = rowspan;
            TableCell.Colspan = colspan;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
        }

        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment, int colspan, int rowspan)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.Rowspan = rowspan;
            TableCell.Colspan = colspan;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_TOP;
            return TableCell;
        }

        private PdfPTable GetHeaderDetail(string levelName, string className)
        {
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 530f;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] tableWidths = new float[2];
            tableWidths[0] = 265;
            tableWidths[1] = 265;

            table.SetWidths(tableWidths);
            table.LockedWidth = true;

            Chunk blank = new Chunk(" ", normal);

            Phrase p = new Phrase();

            p.Add(new Chunk("ระดับชั้นเรียน : ", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk(levelName, normal));

            PdfPCell cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            p = new Phrase();

            p.Add(new Chunk("ชั้นเรียน : ", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk(className, normal));

            cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            return table;
        }

        private PdfPTable GetBody(List<Reportmobile01.Report02UsersView01> studentDatas)
        {
            PdfPTable table = new PdfPTable(13);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] WidthPercentage = new float[] { 8, 14, 6, 6, 6, 6, 6, 6, 6, 6, 6, 12, 12 };
            table.SetTotalWidth(WidthPercentage);

            BaseColor backgroundColor = BaseColor.WHITE;
            BaseColor fontColor = BaseColor.BLACK;

            table.AddCell(setCell(new CellProperty { Text = "ลำดับ", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ชั้นเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "จำนวนนักเรียนทั้งหมด", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ไม่มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รายชื่อนักเรียนที่ไม่มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หมายเหตุ", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));


            table.AddCell(setCell(new CellProperty { Text = "ชาย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หญิง	", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "ชาย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หญิง	", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "ป่วย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ลา", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ขาด", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            int indexRows = 1;
            backgroundColor = new BaseColor(238, 238, 238);
            fontColor = BaseColor.BLACK;
            int _t2 = 0;
            int sum_f_all = 0;
            int sum_m_all = 0;

            int sum_f_0_all = 0;
            int sum_m_0_all = 0;

            int sum_3_all = 0;
            int sum_4_all = 0;
            int sum_5_all = 0;

            foreach (var _dataGroup in studentDatas)
            {
                foreach (var _dataDetail_0 in _dataGroup.level.OrderBy(o => o.sortValue))
                {
                    int sum_f_level2 = 0;
                    int sum_m_level2 = 0;

                    int sum_f_0_level2 = 0;
                    int sum_m_0_level2 = 0;

                    int sum_3_level2 = 0;
                    int sum_4_level2 = 0;
                    int sum_5_level2 = 0;

                    foreach (var _dataDetail_1 in _dataDetail_0.level2)
                    {


                        int sum_f = _dataDetail_1.female_status_0 + _dataDetail_1.female_status_1 + _dataDetail_1.female_status_2 + _dataDetail_1.female_status_3 + _dataDetail_1.female_status_4 + _dataDetail_1.female_status_5 + _dataDetail_1.female_status_6;
                        int sum_m = _dataDetail_1.male_status_0 + _dataDetail_1.male_status_1 + _dataDetail_1.male_status_2 + _dataDetail_1.male_status_3 + _dataDetail_1.male_status_4 + _dataDetail_1.male_status_5 + _dataDetail_1.male_status_6;

                        int sum_f_0 = _dataDetail_1.female_status_0 + _dataDetail_1.female_status_1 + _dataDetail_1.female_status_2;
                        int sum_m_0 = _dataDetail_1.male_status_0 + _dataDetail_1.male_status_1 + _dataDetail_1.male_status_2;

                        int sum_3 = _dataDetail_1.male_status_3 + _dataDetail_1.female_status_3;
                        int sum_4 = _dataDetail_1.male_status_4 + _dataDetail_1.female_status_4;
                        int sum_5 = _dataDetail_1.male_status_5 + _dataDetail_1.female_status_5;

                        sum_f_level2 += sum_f;
                        sum_m_level2 += sum_m;

                        sum_f_0_level2 += sum_f_0;
                        sum_m_0_level2 += sum_m_0;

                        sum_3_level2 += sum_3;
                        sum_4_level2 += sum_4;
                        sum_5_level2 += sum_5;

                        string note3 = "";
                        if (!string.IsNullOrEmpty(_dataDetail_1.note3))
                        {
                            for (int ii = 0; ii < (_dataDetail_1.note3 ?? "").Split(',').Length; ii++)
                            {
                                note3 += (string.IsNullOrEmpty(note3) ? "" : Environment.NewLine) + (ii + 1) + ". " + (_dataDetail_1.note3 ?? "").Split(',')[ii];
                            }
                        }

                        table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = _dataDetail_1.level2name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f + sum_m), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0 + sum_m_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = note3, Font = small, HorizontalAlignment = Element.ALIGN_LEFT }));
                        table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        indexRows++;
                    }

                    table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_level2 + sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_level2 + sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

                    int _t1 = sum_m_level2 + sum_f_level2;

                    table.AddCell(setCell(new CellProperty { Text = "คิดเป็นร้อยละ", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, _t1), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_0_level2 + sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_3_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_4_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_5_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

                    sum_f_all += sum_f_level2;
                    sum_m_all += sum_m_level2;

                    sum_f_0_all += sum_f_0_level2;
                    sum_m_0_all += sum_m_0_level2;

                    sum_3_all += sum_3_level2;
                    sum_4_all += sum_4_level2;
                    sum_5_all += sum_5_level2;
                }
                _t2 = sum_f_all + sum_m_all;
            }

            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_all + sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_all + sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));


            table.AddCell(setCell(new CellProperty { Text = "คิดเป็นร้อยละ", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, _t2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_0_all + sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_3_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_4_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_5_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

            return table;
        }

        private PdfPTable GetBody_T04(List<Reportmobile01.Report02UsersView01> studentDatas)
        {
            PdfPTable table = new PdfPTable(13);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] WidthPercentage = new float[] { 6, 18, 6, 6, 6, 6, 6, 6, 6, 6, 6, 16, 8 };
            table.SetTotalWidth(WidthPercentage);

            BaseColor backgroundColor = BaseColor.WHITE;
            BaseColor fontColor = BaseColor.BLACK;

            table.AddCell(setCell(new CellProperty { Text = "ลำดับ", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ชั้นเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "จำนวนนักเรียนทั้งหมด", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "ไม่มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 1, Colspan = 3 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รายชื่อนักเรียนที่ไม่มาเรียน", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หมายเหตุ", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Rowspan = 2, Colspan = 1 }, backgroundColor, fontColor));


            table.AddCell(setCell(new CellProperty { Text = "ชาย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หญิง	", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "ชาย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หญิง	", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "ชาย", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "หญิง	", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = Header_smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            int indexRows = 1;
            backgroundColor = new BaseColor(238, 238, 238);
            fontColor = BaseColor.BLACK;
            int _t2 = 0;
            int sum_f_all = 0;
            int sum_m_all = 0;

            int sum_f_0_all = 0;
            int sum_m_0_all = 0;

            int sum_3_all = 0;
            int sum_4_all = 0;
            int sum_5_all = 0;

            foreach (var _dataGroup in studentDatas)
            {
                foreach (var _dataDetail_0 in _dataGroup.level.OrderBy(o => o.sortValue))
                {
                    int sum_f_level2 = 0;
                    int sum_m_level2 = 0;

                    int sum_f_0_level2 = 0;
                    int sum_m_0_level2 = 0;

                    int sum_3_level2 = 0;
                    int sum_4_level2 = 0;
                    int sum_5_level2 = 0;

                    foreach (var _dataDetail_1 in _dataDetail_0.level2)
                    {


                        int sum_f = _dataDetail_1.female_status_0 + _dataDetail_1.female_status_1 + _dataDetail_1.female_status_2 + _dataDetail_1.female_status_3 + _dataDetail_1.female_status_4 + _dataDetail_1.female_status_5 + _dataDetail_1.female_status_6;
                        int sum_m = _dataDetail_1.male_status_0 + _dataDetail_1.male_status_1 + _dataDetail_1.male_status_2 + _dataDetail_1.male_status_3 + _dataDetail_1.male_status_4 + _dataDetail_1.male_status_5 + _dataDetail_1.male_status_6;

                        int sum_f_0 = _dataDetail_1.female_status_0 + _dataDetail_1.female_status_1 + _dataDetail_1.female_status_2;
                        int sum_m_0 = _dataDetail_1.male_status_0 + _dataDetail_1.male_status_1 + _dataDetail_1.male_status_2;

                        int sum_3 = _dataDetail_1.male_status_3 + _dataDetail_1.male_status_4 + _dataDetail_1.male_status_5 + _dataDetail_1.male_status_6;
                        int sum_4 = _dataDetail_1.female_status_3 + _dataDetail_1.female_status_4 + _dataDetail_1.female_status_5 + _dataDetail_1.female_status_6;
                        int sum_5 = sum_3 + sum_4;

                        sum_f_level2 += sum_f;
                        sum_m_level2 += sum_m;

                        sum_f_0_level2 += sum_f_0;
                        sum_m_0_level2 += sum_m_0;

                        sum_3_level2 += sum_3;
                        sum_4_level2 += sum_4;
                        sum_5_level2 += sum_5;

                        string note3 = "";
                        if (!string.IsNullOrEmpty(_dataDetail_1.note3))
                        {
                            for (int ii = 0; ii < (_dataDetail_1.note3 ?? "").Split(',').Length; ii++)
                            {
                                note3 += (string.IsNullOrEmpty(note3) ? "" : Environment.NewLine + Environment.NewLine) + (ii + 1) + ". " + (_dataDetail_1.note3 ?? "").Split(',')[ii];
                            }
                        }


                        table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = _dataDetail_1.level2name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f + sum_m), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0 + sum_m_0), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

                        table.AddCell(setCellType2(new CellProperty { Text = note3, Font = small, HorizontalAlignment = Element.ALIGN_LEFT }));
                        table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                        indexRows++;
                    }

                    table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_level2 + sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_level2 + sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

                    int _t1 = sum_m_level2 + sum_f_level2;

                    table.AddCell(setCell(new CellProperty { Text = "คิดเป็นร้อยละ", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_f_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, _t1), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_m_0_level2 + sum_f_0_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_3_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_4_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
                    table.AddCell(setCell(new CellProperty { Text = percent(_t1, sum_5_level2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

                    table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

                    sum_f_all += sum_f_level2;
                    sum_m_all += sum_m_level2;

                    sum_f_0_all += sum_f_0_level2;
                    sum_m_0_all += sum_m_0_level2;

                    sum_3_all += sum_3_level2;
                    sum_4_all += sum_4_level2;
                    sum_5_all += sum_5_level2;
                }
                _t2 = sum_f_all + sum_m_all;
            }

            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_all + sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_m_0_all + sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_3_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_4_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0}", sum_5_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));


            table.AddCell(setCell(new CellProperty { Text = "คิดเป็นร้อยละ", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 2 }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_f_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, _t2), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_m_0_all + sum_f_0_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_3_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_4_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));
            table.AddCell(setCell(new CellProperty { Text = percent(_t2, sum_5_all), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }, backgroundColor, fontColor));

            table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 2 }, backgroundColor, fontColor));

            return table;
        }

        private string percent(int maxuser, int countstatus)
        {
            if (maxuser == 0) return "0.00 %";
            else return string.Format("{0:0.00}", ((countstatus / (maxuser * 1.00)) * 100f));
        }

        private PdfPTable Licen()
        {
            PdfPTable table = new PdfPTable(2);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;


            for (int i = 0; i < 10; i++)
            {
                table.AddCell(AddCell(new PdfPCell(new Phrase(" ", normal)), Element.ALIGN_CENTER));
                table.AddCell(AddCell(new PdfPCell(new Phrase(" ", normal)), Element.ALIGN_CENTER));
            }

            // table.AddCell(setCell(new CellProperty { Text = "asdasd", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ ............................................. หัวหน้าระดับชั้น / ผู้ช่วยระดับชั้น", normal)), Element.ALIGN_LEFT));

            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงชื่อ ............................................. หัวหน้าฝ่ายปกครอง", normal)), Element.ALIGN_LEFT));

            table.AddCell(AddCell(new PdfPCell(new Phrase("", normal)), Element.ALIGN_CENTER));
            table.AddCell(AddCell(new PdfPCell(new Phrase("ลงวันที่ ............../............../..............", normal)), Element.ALIGN_LEFT));

            return table;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private PdfPCell setCell(CellProperty property)
        {
            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;

            if (property.backgroundColor != null) TableCell.BackgroundColor = new BaseColor(property.backgroundColor.Red, property.backgroundColor.Green, property.backgroundColor.Blue);

            return TableCell;
        }

        private PdfPCell setCellType2(CellProperty property)
        {
            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            TableCell.Padding = 1.2f;
            TableCell.PaddingTop = 1.2f;
            TableCell.PaddingBottom = 1.2f;

            if (property.backgroundColor != null) TableCell.BackgroundColor = new BaseColor(property.backgroundColor.Red, property.backgroundColor.Green, property.backgroundColor.Blue);

            return TableCell;
        }

        private PdfPCell setCell(CellProperty property, BaseColor backgroundColor, BaseColor fontColor)
        {
            var font = property.Font;
            font.SetColor(fontColor.R, fontColor.G, fontColor.B);

            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, font));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            TableCell.BackgroundColor = backgroundColor;

            return TableCell;
        }

        public class CellProperty
        {
            public string Text { get; set; }
            public Font Font { get; set; }
            public int? Rowspan { get; set; }
            public int? Colspan { get; set; }
            public int? HorizontalAlignment { get; set; }
            public int? VerticalAlignment { get; set; }
            public int? Border { get; set; }
            public Color backgroundColor { get; set; }
            public class Color
            {
                public int Red { get; set; }
                public int Green { get; set; }
                public int Blue { get; set; }
            }
        }

        public static float TotalRowHeights(Document document, PdfContentByte content, PdfPTable table, params int[] wantedRows)
        {
            float height = 0f;
            ColumnText ct = new ColumnText(content);
            // respect current Document.PageSize    
            ct.SetSimpleColumn(
              document.Left, document.Bottom,
              document.Right, document.Top
            );
            ct.AddElement(table);
            // **simulate** adding the PdfPTable to calculate total height
            ct.Go(true);
            int Rows = table.Rows.Count();
            for (int i = 0; i < Rows; i++)
            {
                height += table.GetRowHeight(i);
            }
            return height;
        }
    }
}