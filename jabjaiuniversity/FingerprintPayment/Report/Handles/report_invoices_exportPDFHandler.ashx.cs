using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using FingerprintPayment.Report.Functions;

namespace FingerprintPayment.Report.Handles
{
    /// <summary>
    /// Summary description for report_invoices_exportPDFHandler
    /// </summary>
    public class report_invoices_exportPDFHandler : IHttpHandler, IRequiresSessionState
    {
        // Bold
        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font h1 = new Font(bf_bold, 14);
        private Font bold = new Font(bf_bold, 12);
        private Font smallBold = new Font(bf_bold, 10);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private Font normal = new Font(bf_normal, 10);
        private Font smallNormal = new Font(bf_normal, 8);
        private Font smallNormal_Black = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
        private Font Header_smallNormal = new Font(bf_normal, 8, 0 /*,new BaseColor(255, 255, 255)*/);

        JWTToken.userData userData;
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                JWTToken token = new JWTToken();
                userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);
                }

                var jsonString = new StreamReader(context.Request.InputStream).ReadToEnd();
                ReportInvoice_Search search = JsonConvert.DeserializeObject<ReportInvoice_Search>(jsonString);

                ReportInvoicesFunctions reportInvoices = new ReportInvoicesFunctions();
                var reports_Data = reportInvoices.Init(search, userData.CompanyID);

                // Create PDF document
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Example.pdf");

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.AddAuthor("Me");

                pdfDoc.Open();

                //pdfDoc.Add(GetHeaderDetail(levelData.levelname, levelData.level2name));
                pdfDoc.Add(GetHeader(search, userData.CompanyID, reports_Data.teacher_name));
                pdfDoc.Add(GetBody(reports_Data));
                pdfDoc.NewPage();

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

        private PdfPTable GetHeader(string school_name, string report_name, string name, string lastname, string code)
        {

            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(school_name, h1)), Element.ALIGN_CENTER));
            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(report_name, bold)), Element.ALIGN_CENTER));
            if (name != "")
            {
                headerTable.AddCell(AddCell(new PdfPCell(new Phrase("ชื่อ  " + name + "  นามสกุล  " + lastname + "  เลขประจำตัว  " + code, bold)), Element.ALIGN_CENTER));
            }

            headerTable.AddCell(AddCell(new PdfPCell(new Phrase(string.Format("พิมพ์วันที่ : {0} ", DateTime.Now.ToString("dd MMM yyyy", new CultureInfo("th-th")) + (string.Format(" เวลา : {0:HH:mm:ss}", DateTime.Now))), smallBold)), Element.ALIGN_RIGHT));
            //headerTable.AddCell(AddCell(new PdfPCell(new Phrase(string.Format("เวลา : {0:HH:mm:ss}", DateTime.Now), smallBold)), Element.ALIGN_RIGHT));

            return headerTable;
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

        private PdfPTable GetBody(Reports_data reports_Datas)
        {
            PdfPTable table = new PdfPTable(7);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] WidthPercentage = new float[] { 8, 11, 24, 11, 11, 11, 22 };
            table.SetTotalWidth(WidthPercentage);
            //table.TotalWidth = 630f;
            //table.HorizontalAlignment = 0;
            //table.SpacingAfter = 20;
            //headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            foreach (var str in new string[] { "ลำดับ", "รหัสนักเรียน", "ชื่อ-นามสกุล", "ยอดเต็ม", "ชำระแล้ว", "ค้างชำระ", "หมายเหตุ" })
            {
                table.AddCell(setCell(new CellProperty
                {
                    Text = str,
                    Font = Header_smallNormal,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    //backgroundColor = new CellProperty.Color
                    //{
                    //    Red = 51,
                    //    Green = 122,
                    //    Blue = 183,
                    //}
                }));
                //table.AddCell(AddCell(new PdfPCell(new Phrase(str, smallBold)), Element.ALIGN_CENTER, 1, 1));
            }

            int indexRows = 1;
            decimal paymentAmount = 0, payment = 0, debt = 0;

            foreach (var studentData in reports_Datas.invoice_Datas)
            {
                table.AddCell(setCell(new CellProperty { Text = indexRows.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = studentData.student_id, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = studentData.student_name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));
                table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", studentData.paymentAmount), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", studentData.payment), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", studentData.debt), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
                table.AddCell(setCell(new CellProperty { Text = studentData.TotalDiscount, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));

                paymentAmount += string.IsNullOrEmpty(studentData.paymentAmount) ? 0 : decimal.Parse(studentData.paymentAmount);
                payment += string.IsNullOrEmpty(studentData.payment) ? 0 : decimal.Parse(studentData.payment);
                debt += string.IsNullOrEmpty(studentData.debt) ? 0 : decimal.Parse(studentData.debt);
                indexRows++;
            }

            table.AddCell(setCell(new CellProperty { Text = "รวม", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 }));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", paymentAmount), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", payment), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            table.AddCell(setCell(new CellProperty { Text = string.Format("{0:#,#0.00}", debt), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            table.AddCell(setCell(new CellProperty { Text = "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));

            return table;
        }
        private PdfPTable GetHeader(ReportInvoice_Search search, int SchoolId, string teacher_name)
        {
            PdfPTable table = new PdfPTable(4);
            table.WidthPercentage = 100;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] WidthPercentage = new float[] { 10, 57, 11, 22 };
            table.SetTotalWidth(WidthPercentage);

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var f_company = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == SchoolId);
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(SchoolId,ConnectionDB.Read)))
                {
                    var f_report = entities.TB_StudentViews.Where(f => f.nTerm == search.term_id && f.nTermSubLevel2 == search.level2_id).AsQueryable().FirstOrDefault();

                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("{0}", f_company.sCompany), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "รายงานรายชื่อลูกหนี้รายห้อง", Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Colspan = 4 }));

                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "ปีการศึกษา :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = f_report.numberYear + "", Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "เทอม :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = f_report.sTerm, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));


                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "ระดับชั้นเรียน :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = f_report.SubLevel, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "ชั้นเรียน :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = f_report.nTSubLevel2, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));


                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "อาจารย์ประจำชั้น :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = teacher_name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));

                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = "พิมพ์วันที่ :", Font = smallNormal, HorizontalAlignment = Element.ALIGN_RIGHT, Colspan = 3 }));
                    table.AddCell(setCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = DateTime.Now.ToString("dd/MM/yyyy เวลา : HH:mm:ss น."), Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT }));

                    return table;
                }
            }


        }

        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_BOTTOM;
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
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
            TableCell.Border = property.Border ?? Rectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            if (property.backgroundColor != null) TableCell.BackgroundColor = new BaseColor(property.backgroundColor.Red, property.backgroundColor.Green, property.backgroundColor.Blue);

            return TableCell;
        }

        private PdfPCell setCell(CellProperty property, BaseColor backgroundColor, BaseColor fontColor)
        {
            var font = property.Font;
            font.SetColor(fontColor.R, fontColor.G, fontColor.B);

            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, smallNormal_Black));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
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
    }
}