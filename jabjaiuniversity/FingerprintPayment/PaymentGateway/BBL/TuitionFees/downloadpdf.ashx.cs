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


namespace FingerprintPayment.PaymentGateway.BBL.TuitionFees
{
    /// <summary>
    /// Summary description for downloadpdf
    /// </summary>
    public class downloadpdf : IHttpHandler
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
                #region getdata
                try
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                    {
                        JabJaiMasterEntities masterEntities = Connection.MasterEntities();
                        BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));

                        schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == invoice.Result.SchoolId);

                        Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(), schoolData.sEntities);
                        //string entities = Session["sEntities"].ToString();

                        Model = invoice.Result;
                        Model.InvoiceItems.RemoveAll(w => w.IsDelete);

                        var term = termLogic.GetById(invoice.Result.TermId);
                        JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData));

                        var studentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany).FirstOrDefault(f => f.sID == invoice.Result.StudentId && f.nTerm == invoice.Result.TermId);
                        var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == schoolData.nCompany).ToList();
                        var user = userLogic.GetById(invoice.Result.StudentId);
                        if (string.IsNullOrEmpty(invoice.Result.TermId) || studentViews == null)
                        {
                            invoice.Result.StudentIdCard = user.Result.Identification;
                            invoice.Result.StudentCode = user.Result.StudentID;
                            if (user.Result.TTermSubLevel2 != null && invoice.Result.SubLevel2 == user.Result.TTermSubLevel2.TSubLevel2)
                            {
                                invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                                invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                            }

                            var f_user = dbschool.TUsers.Find(invoice.Result.StudentId, schoolData.nCompany);
                            Model.StudentName = getTitlte(q_titles, f_user.sStudentTitle) + " " + f_user.sName + " " + f_user.sLastname;
                        }
                        else
                        {
                            invoice.Result.StudentIdCard = user.Result.Identification;
                            invoice.Result.StudentCode = studentViews.sStudentID;
                            Model.StudentName = getTitlte(q_titles, studentViews.sStudentTitle) + " " + studentViews.sName + " " + studentViews.sLastname;
                            Model.SubLevel = studentViews.SubLevel;
                            Model.SubLevel2 = studentViews.nTSubLevel2;
                            Model.Term = studentViews.sTerm;
                            Model.TermYear = studentViews.numberYear.Value.ToString();
                        }

                        if (term.Result.sTerm != null)
                        {
                            invoice.Result.Term = term.Result.sTerm;
                            invoice.Result.nYear = term.Result.Year.Number.Value;
                        }

                        if (Request.QueryString["paidPaymentId"] != null)
                        {
                            PaidPaymentDTO paidPayment = invoice.Result.PaidPayments
                                 .OrderBy(o => o.PaidPaymentId)
                                 .Where(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]))
                                 .FirstOrDefault();

                            if (paidPayment.PaymentDate.HasValue)
                            {
                                Model.Date = paidPayment.PaymentDate.Value;
                            }

                            foreach (var PaidPaymentsData in Model.PaidPayments)
                            {
                                PaidPaymentsData.Payments.RemoveAll(r => (r.isDel ?? false) == true);
                            }

                            TotalAllPaid = invoice.Result.PaidPayments.Where(w => w.Status != EnumPaymentStatus.Void.ToString()).Sum(s => s.Amount).Value;

                            if (paidPayment != null)
                            {
                                //HideCheckBox = true;
                                int periodIndex = invoice.Result.PaidPayments
                                    .OrderBy(o => o.PaidPaymentId)
                                    .ToList()
                                    .FindIndex(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]));
                                PaidPeriod = periodIndex += 1;

                                List<int> paidPaymentItem = new List<int>();
                                foreach (PaymentDTO payment in paidPayment.Payments)
                                {
                                    paidPaymentItem.Add(payment.InvoiceItemId.Value);
                                    invoice.Result.InvoiceItems.Where(w => w.InvoiceItemId == payment.InvoiceItemId)
                                        .ToList().ForEach(w =>
                                        {
                                            w.OutstandingAmount = payment.Amount;
                                        });
                                }
                                invoice.Result.InvoiceItems.RemoveAll(w => !paidPaymentItem.Contains(w.InvoiceItemId));

                                if (invoice.Result.StudentId == 0)
                                {
                                    if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                                    {
                                        invoice.Result.SubLevel2 = invoice.Result.Fd_NewTermSubLevel2;
                                        invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                                    }
                                    else if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                                    {
                                        invoice.Result.SubLevel2 = string.Empty;
                                        invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                                    }
                                }

                                Model.ReceiptNo = paidPayment.ReceiptNo;
                                Model.paid_Discount = paidPayment.paid_Discount ?? 0;
                            }
                        }
                    }

                    // Create a barcode writer object
                    BarcodeWriter writer = new BarcodeWriter();

                    // Set the barcode format and options
                    writer.Format = BarcodeFormat.CODE_128;
                    EncodingOptions options = new EncodingOptions
                    {
                        Width = 800,
                        Height = 100,
                        Margin = 10,
                        PureBarcode = true
                    };
                    writer.Options = options;

                    // Get the barcode image
                    Bitmap barcode = writer.Write("0020020221148320");

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the barcode image to a memory stream
                        barcode.Save(memoryStream, ImageFormat.Png);

                        // Get the byte array of the image data
                        byte[] imageData = memoryStream.ToArray();

                        // Convert the byte array to a base64 string
                        string imageDataBase64 = Convert.ToBase64String(imageData);

                        // Create the data URI
                        imageDataUri = "data:image/png;base64," + imageDataBase64;
                    }
                }
                catch (Exception ex)
                {
                    Model = new InvoiceDTO();
                }

                ThaiBahtOutstandingAmount = ThaiBaht((Model.OutstandingAmount ?? 0).ToString("#0.00"));
                ModelJson = new JavaScriptSerializer().Serialize(Model);
                #endregion

                ReportInvoicesFunctions reportInvoices = new ReportInvoicesFunctions();
      

                // Create PDF document
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=Example.pdf");

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
                pdfDoc.AddAuthor("Me");

                pdfDoc.Open();

                // Load the image from a URL
                Image image = Image.GetInstance("https://www.example.com/images/myimage.png");

                // Set the image position and size on the page
                image.SetAbsolutePosition(100, 100);
                image.ScaleToFit(200, 200);

                // Add the image to the PDF document
                pdfDoc.Add(image);

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
    
        private PdfPCell AddCell(PdfPCell pdfPCell, int horizontalAlignment)
        {
            PdfPCell TableCell = pdfPCell;
            TableCell.HorizontalAlignment = horizontalAlignment;
            TableCell.VerticalAlignment = Element.ALIGN_BOTTOM;
            TableCell.Border = Rectangle.NO_BORDER;
            return TableCell;
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