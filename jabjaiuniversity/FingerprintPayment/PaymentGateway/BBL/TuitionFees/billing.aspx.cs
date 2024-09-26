using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Web.Services;
using System.Web.Script.Services;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using IFont = iTextSharp.text.Font;
using IRectangle = iTextSharp.text.Rectangle;
using SImage = System.Drawing.Image;
using IImage = iTextSharp.text.Image;
using iTextSharp.text.pdf.draw;
using System.Configuration;
using AccountingEntity;
using Jabjai.Object.Entity.Peak;

namespace FingerprintPayment.PaymentGateway.BBL.TuitionFees
{
    public partial class billing : System.Web.UI.Page
    {
        private static InvoiceLogic invoiceLogic;
        private static PaymentLogic paymentLogic;
        private static UserLogic userLogic;
        private static TermLogic termLogic;
        public static InvoiceDTO Model { get; set; }
        public static Jabjai.Object.Entity.Jabjai.TUser ModelUser { get; set; }
        public static string ModelJson { get; set; }
        public static bool HideCheckBox { get; set; }
        public static int PaidPeriod { get; set; }
        public static decimal TotalAllPaid { get; set; }
        public static TCompany schoolData { get; set; }

        public static string ThaiBahtOutstandingAmount = "";
        public static string imageDataUri = "";
        public static string imageQRCODEUri = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            invoiceLogic = new InvoiceLogic();
            paymentLogic = new PaymentLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();

            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                {
                    using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                    {
                        int invoiceID = int.Parse(Request.QueryString["invoiceID"]);
                        var accountInvoice = masterEntities.Database.SqlQuery<AccountInvoice>("SELECT * FROM [AccountingDB].dbo.AccountInvoice WHERE AccountInvoiceId =" + invoiceID).FirstOrDefault();
                        var invoiceStudent = masterEntities.Database.SqlQuery<AccountInvoiceStudent>("SELECT * FROM [AccountingDB].dbo.AccountInvoiceStudent WHERE AccountInvoiceId =" + invoiceID).FirstOrDefault();
                        var invoiceDetails = masterEntities.Database.SqlQuery<AccountInvoiceDetail>("SELECT * FROM [AccountingDB].dbo.AccountInvoiceDetail WHERE DeleteDate IS NULL AND AccountInvoiceId =" + invoiceID).ToList();

                        //BaseResult<InvoiceDTO> invoice = new BaseResult<InvoiceDTO>(); //invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));

                        schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == accountInvoice.SchoolId);

                        Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), schoolData.sEntities);
                        //string entities = Session["sEntities"].ToString();


                        var term = termLogic.GetById(accountInvoice.TermId);


                        var studentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany && w.sID == invoiceStudent.StudentId && w.nTerm == accountInvoice.Term).AsQueryable().FirstOrDefault();
                        var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == schoolData.nCompany).ToList();
                        //var user = userLogic.GetById(invoiceStudent.StudentId);
                        if (string.IsNullOrEmpty(accountInvoice.TermId) || studentViews == null)
                        {
                            //invoice.Result.StudentIdCard = user.Result.Identification;
                            //invoice.Result.StudentCode = user.Result.StudentID;
                            //if (user.Result.TTermSubLevel2 != null && invoice.Result.SubLevel2 == user.Result.TTermSubLevel2.TSubLevel2)
                            //{
                            //    invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                            //    invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                            //}

                            var f_user = dbschool.TUser.Find(invoiceStudent.StudentId, schoolData.nCompany);
                            Model.StudentName = getTitlte(q_titles, f_user.sStudentTitle) + " " + f_user.sName + " " + f_user.sLastname;
                            Model.StudentCode = f_user.sStudentID;
                            Model.SubLevel = invoiceStudent.LevelName;
                            Model.SubLevel2 = invoiceStudent.ClassName;
                        }
                        else
                        {
                            //invoice.Result.StudentIdCard = user.Result.Identification;
                            //invoice.Result.StudentCode = studentViews.sStudentID;
                            Model.StudentName = getTitlte(q_titles, studentViews.sStudentTitle) + " " + studentViews.sName + " " + studentViews.sLastname;
                            Model.SubLevel = studentViews.SubLevel;
                            Model.SubLevel2 = studentViews.nTSubLevel2;
                            Model.Term = studentViews.sTerm;
                            Model.TermYear = studentViews.numberYear.Value.ToString();
                            Model.StudentCode = studentViews.sStudentID;
                        }

                        Model.Code = invoiceStudent.InvoiceCode.ToString();
                        Model.SchoolId = accountInvoice.SchoolId ?? 0;

                        //if (term.Result.sTerm != null)
                        //{
                        //    invoice.Result.Term = term.Result.sTerm;
                        //    invoice.Result.nYear = term.Result.Year.Number.Value;
                        //}

                        //if (Request.QueryString["paidPaymentId"] != null)
                        //{
                        //    PaidPaymentDTO paidPayment = invoice.Result.PaidPayments
                        //         .OrderBy(o => o.PaidPaymentId)
                        //         .Where(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]))
                        //         .FirstOrDefault();

                        //    if (paidPayment.PaymentDate.HasValue)
                        //    {
                        //        Model.Date = paidPayment.PaymentDate.Value;
                        //    }

                        //    foreach (var PaidPaymentsData in Model.PaidPayments)
                        //    {
                        //        PaidPaymentsData.Payments.RemoveAll(r => (r.isDel ?? false) == true);
                        //    }

                        //    TotalAllPaid = invoice.Result.PaidPayments.Where(w => w.Status != EnumPaymentStatus.Void.ToString()).Sum(s => s.Amount).Value;

                        //    if (paidPayment != null)
                        //    {
                        //        //HideCheckBox = true;
                        //        int periodIndex = invoice.Result.PaidPayments
                        //            .OrderBy(o => o.PaidPaymentId)
                        //            .ToList()
                        //            .FindIndex(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]));
                        //        PaidPeriod = periodIndex += 1;

                        //        List<int> paidPaymentItem = new List<int>();
                        //        foreach (PaymentDTO payment in paidPayment.Payments)
                        //        {
                        //            paidPaymentItem.Add(payment.InvoiceItemId.Value);
                        //            invoice.Result.InvoiceItems.Where(w => w.InvoiceItemId == payment.InvoiceItemId)
                        //                .ToList().ForEach(w =>
                        //                {
                        //                    w.OutstandingAmount = payment.Amount;
                        //                });
                        //        }
                        //        invoice.Result.InvoiceItems.RemoveAll(w => !paidPaymentItem.Contains(w.InvoiceItemId));

                        //        if (invoice.Result.StudentId == 0)
                        //        {
                        //            if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                        //            {
                        //                invoice.Result.SubLevel2 = invoice.Result.Fd_NewTermSubLevel2;
                        //                invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                        //            }
                        //            else if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                        //            {
                        //                invoice.Result.SubLevel2 = string.Empty;
                        //                invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                        //            }
                        //        }

                        //        Model.ReceiptNo = paidPayment.ReceiptNo;
                        //        Model.paid_Discount = paidPayment.paid_Discount ?? 0;
                        //    }
                        //}
                        Model.OutstandingAmount = invoiceDetails.Sum(s => s.TotalPrice ?? 0);
                        double OutstandingAmount = (double)invoiceDetails.Sum(s => s.TotalPrice ?? 0);
                        if (ConfigurationManager.AppSettings["demo"].ToString() == "true")
                        {
                            OutstandingAmount = OutstandingAmount / 10.0;
                        }

                        int Term = 0, StudentCode;
                        int.TryParse(Model.Term, out Term);
                        string Fd_Suffix = "00";
                        int.TryParse(Model.StudentCode, out StudentCode);

                        TB_PaymentGateway paymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany);
                        if (paymentGateway != null)
                        {
                            Fd_Suffix = paymentGateway.Fd_Suffix ?? "00";
                        }

                        string REF1 = string.Format("{0}{1:00}{2:0000000000}", Model.TermYear, Term, StudentCode);
                        string REF2 = string.Format("{0:00000}{1}", Model.SchoolId, Model.Code.Replace("IV-", ""));
                        string TaxId = schoolData.TaxId.Replace("-", "") + Fd_Suffix;
                        string Amount = OutstandingAmount.ToString("#0.00").Replace(".", "");

                        string BarCode = "|" + TaxId + "\r\n" + REF1 + "\r\n" + REF2 + "\r\n" + Amount;
                        imageQRCODEUri = QRCodeFunction.Create(BarCode, QRCoder.QRCodeGenerator.ECCLevel.H);

                        Bitmap barcode = GetBarCoce(Model, schoolData, paymentGateway);

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
                }
            }
            catch (Exception ex)
            {
                Model = new InvoiceDTO();
            }

            ThaiBahtOutstandingAmount = ThaiBaht((Model.OutstandingAmount ?? 0).ToString("#0.00"));
            ModelJson = new JavaScriptSerializer().Serialize(Model);

        }

        private static Bitmap GetBarCoce(InvoiceDTO Model, TCompany schoolData, TB_PaymentGateway paymentGateway)
        {
            // Create a barcode writer object
            BarcodeWriter writer = new BarcodeWriter();

            // Set the barcode format and options
            writer.Format = BarcodeFormat.CODE_128;
            EncodingOptions options = new EncodingOptions
            {
                Width = 500,
                Height = 100,
                Margin = 10,
                PureBarcode = true
            };
            writer.Options = options;

            double OutstandingAmount = (double)Model.OutstandingAmount.Value;
            if (ConfigurationManager.AppSettings["demo"].ToString() == "true")
            {
                OutstandingAmount = OutstandingAmount / 10.0;
            }

            int Term = 0, StudentCode;
            int.TryParse(Model.Term, out Term);
            string Fd_Suffix = "00";
            int.TryParse(Model.StudentCode, out StudentCode);

            if (paymentGateway != null)
            {
                Fd_Suffix = paymentGateway.Fd_Suffix ?? "00";
            }

            string REF1 = string.Format("{0}{1:00}{2:0000000000}", Model.TermYear, Term, StudentCode);
            string REF2 = string.Format("{0:00000}{1}", Model.SchoolId, Model.Code.Replace("IV-", ""));
            string TaxId = schoolData.TaxId.Replace("-", "") + Fd_Suffix;
            string Amount = OutstandingAmount.ToString("#0.00").Replace(".", "");

            string BarCode = "|" + TaxId + "\r\n" + REF1 + "\r\n" + REF2 + "\r\n" + Amount;

            // Get the barcode image
            Bitmap barcode = writer.Write(BarCode);

            return barcode;

        }

        // Bold
        private static BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private static IFont h1 = new IFont(bf_bold, 14);
        private static IFont bold = new IFont(bf_bold, 12);
        private static IFont smallBold = new IFont(bf_bold, 8);

        // Normal
        private static BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        private static IFont normal = new IFont(bf_normal, 10);
        private static IFont smallNormal = new IFont(bf_normal, 8);
        private static IFont small = new IFont(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.White));
        private static IFont smallNormal_Black = new IFont(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
        private static IFont Header_small_0 = new IFont(bf_normal, 18, 0, new BaseColor(System.Drawing.Color.Black));
        private static IFont Header_small_1 = new IFont(bf_normal, 14, 0, new BaseColor(System.Drawing.Color.Black));
        private static IFont Header_smallNormal = new IFont(bf_normal, 12, 0 /*,new BaseColor(255, 255, 255)*/);

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static void ExportPDF()
        {
            invoiceLogic = new InvoiceLogic();
            paymentLogic = new PaymentLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();
            //JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read);
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
            {

                //BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(InvoiceId);
                BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(HttpContext.Current.Request.QueryString["InvoiceId"].ToString()));

                schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == invoice.Result.SchoolId);

                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), schoolData.sEntities);
                //string entities = Session["sEntities"].ToString();

                Model = invoice.Result;
                Model.InvoiceItems.RemoveAll(w => w.IsDelete);

                var term = termLogic.GetById(invoice.Result.TermId);


                var studentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany && w.sID == invoice.Result.StudentId && w.nTerm == invoice.Result.TermId).AsQueryable().FirstOrDefault();
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

                    var f_user = dbschool.TUser.Find(invoice.Result.StudentId, schoolData.nCompany);
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

                string ThaiBahtOutstandingAmount = ThaiBaht((Model.OutstandingAmount ?? 0).ToString("#0.00"));

                Document pdfDoc = new Document(PageSize.A4, 10, 5, 20, 50);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);

                pdfDoc.AddAuthor("Me");
                pdfDoc.Open();

                PdfPCell TableCell;
                PdfPTable table = new PdfPTable(1);
                table.WidthPercentage = 100;
                float[] WidthPercentage = new float[] { 100 };
                table.SetTotalWidth(WidthPercentage);

                

                // Load the image from a URL
                IImage image = IImage.GetInstance(schoolData.sImage);
                image.Alignment = IImage.ALIGN_CENTER;

                // Set the image position and size on the page
                //image.SetAbsolutePosition(800, 800);
                //image.ScaleToFit(50, 50);
                image.ScaleAbsolute(120f, 120f); // Set image size.

                // Add the image to the PDF document
                PdfPCell imgCell1 = new PdfPCell(image);
                imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                //imgCell1.BackgroundColor = new BaseColor(255, 255, 255);
                imgCell1.Border = IRectangle.NO_BORDER;

                table.AddCell(imgCell1);

                BaseColor fontColor = new BaseColor(0, 0, 0);

                table.AddCell(setCell(new CellProperty { Text = schoolData.sCompany, Font = Header_small_0, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = schoolData.sNameEN, Font = Header_small_0, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));

                table.AddCell(setCell(new CellProperty
                {
                    Text = "\r\n เลขที่ " + (string.IsNullOrEmpty(schoolData.sHomeNumber) ? " - " : schoolData.sHomeNumber) + " หมู่ " + (string.IsNullOrEmpty(schoolData.sMuu) ? " - " : schoolData.sMuu) + " ถนน " + (string.IsNullOrEmpty(schoolData.sRoad) ? " - " : schoolData.sRoad) + " ซอย "
                    + (string.IsNullOrEmpty(schoolData.sSoy) ? " - " : schoolData.sSoy),
                    Font = Header_small_1,
                    HorizontalAlignment = Element.ALIGN_CENTER
                }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\n ตำบล " + schoolData.sTumbon + " อำเภอ " + schoolData.sAumpher + " จังหวัด " + schoolData.sProvince, Font = Header_small_1, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = schoolData.sPost + " โทร. " + schoolData.sPhoneOne, Font = Header_small_1, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\n หมายเลขผู้เสียภาษี : " + schoolData.TaxId + "\r\n", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = "\r\n", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_CENTER }, null, fontColor));

                pdfDoc.Add(table);

                double OutstandingAmount = (double)Model.OutstandingAmount.Value;
                if (ConfigurationManager.AppSettings["demo"].ToString() == "true")
                {
                    OutstandingAmount = OutstandingAmount / 10.0;
                }

                int Term = 0, StudentCode;
                int.TryParse(Model.Term, out Term);
                string Fd_Suffix = "00";
                int.TryParse(Model.StudentCode, out StudentCode);

                TB_PaymentGateway paymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany);
                if (paymentGateway != null)
                {
                    Fd_Suffix = paymentGateway.Fd_Suffix ?? "00";
                }

                string REF1 = string.Format("{0}{1:00}{2:0000000000}", Model.TermYear, Term, StudentCode);
                string REF2 = string.Format("{0:00000}{1}", Model.SchoolId, Model.Code.Replace("IV-", ""));
                string TaxId = schoolData.TaxId.Replace("-", "") + Fd_Suffix;
                string Amount = OutstandingAmount.ToString("#0.00").Replace(".", "");

                table = new PdfPTable(2);
                table.WidthPercentage = 100;
                WidthPercentage = new float[] { 50, 50 };
                table.SetTotalWidth(WidthPercentage);

                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, 1);
                pdfDoc.Add(line);

                table.AddCell(setCell(new CellProperty { Text = "\r\nวันที่ " + Model.DueDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH")), Font = Header_small_1, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nชื่อ-สกุล ", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = ": " + Model.StudentName, Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nเลขประจำตัวนักเรียน ", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = ": " + Model.StudentCode, Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nชั้นเรียน/Class ", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = ": " + Model.SubLevel + " / " + Model.SubLevel2, Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nREF.1 " + REF1, Font = Header_small_1, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nREF.2 " + REF2, Font = Header_small_1, Colspan = 2, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\nยอดเงิน", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_LEFT }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = Model.OutstandingAmount.Value.ToString("#,#0.00"), Font = Header_small_1, HorizontalAlignment = Element.ALIGN_RIGHT }, null, fontColor));

                table.AddCell(setCell(new CellProperty { Text = "\r\n" + ThaiBahtOutstandingAmount + "\r\n", Font = Header_small_1, Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT }, null, fontColor));
                table.AddCell(setCell(new CellProperty { Text = "\r\n", Font = Header_small_1, Colspan = 2, HorizontalAlignment = Element.ALIGN_RIGHT }, null, fontColor));

                pdfDoc.Add(table);

                pdfDoc.Add(line);

                string BarCode = "|" + TaxId + "\r\n" + REF1 + "\r\n" + REF2 + "\r\n" + Amount;
                System.Drawing.Image IQRCODE = (System.Drawing.Image)QRCodeFunction.CreateBitmap(BarCode, QRCoder.QRCodeGenerator.ECCLevel.H);
                Bitmap barcode = GetBarCoce(Model, schoolData, paymentGateway);

                IImage image2 = IImage.GetInstance(IQRCODE, ImageFormat.Png);
                image2.Alignment = IImage.ALIGN_CENTER;
                image2.ScaleAbsolute(80f, 80f); // Set image size.

                // Load the image from a URL
                System.Drawing.Image IBarcode = (System.Drawing.Image)barcode;


                string imageDataUri = "";

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


                IImage image1 = IImage.GetInstance(IBarcode, ImageFormat.Png);
                image1.Alignment = IImage.ALIGN_CENTER;
                //image.ScaleAbsolute(800f, 80f); // Set image size.
                image1.ScaleAbsolute(450f, 80f); // Set image size.

                table = new PdfPTable(3);
                table.WidthPercentage = 100;
                WidthPercentage = new float[] { 20, 70, 10 };
                table.SetTotalWidth(WidthPercentage);

                table.AddCell(setCell(new CellProperty { Text = "\r\n", Font = Header_small_1, Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT }, null, fontColor));

                // Add the image to the PDF document
                imgCell1 = new PdfPCell(image2);
                imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                //imgCell1.BackgroundColor = new BaseColor(255, 255, 255);
                imgCell1.Border = IRectangle.NO_BORDER;

                table.AddCell(imgCell1);

                // Add the image to the PDF document
                imgCell1 = new PdfPCell(image1);
                imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                //imgCell1.BackgroundColor = new BaseColor(255, 255, 255);
                imgCell1.Border = IRectangle.NO_BORDER;

                table.AddCell(imgCell1);

                table.AddCell(setCell(new CellProperty { Text = "\r\n", Font = Header_small_1, HorizontalAlignment = Element.ALIGN_RIGHT }, null, fontColor));

                pdfDoc.Add(table);

                pdfDoc.Close();

                // Create PDF document
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.Write(pdfDoc);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
            
            }
        }

        private static PdfPCell setCell(CellProperty property, BaseColor backgroundColor, BaseColor fontColor)
        {
            var font = property.Font;
            font.SetColor(fontColor.R, fontColor.G, fontColor.B);

            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, Header_small_0));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
            TableCell.Border = property.Border ?? IRectangle.NO_BORDER;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            TableCell.BackgroundColor = backgroundColor;

            return TableCell;
        }

        private static PdfPCell setCell(CellProperty property)
        {
            PdfPCell TableCell = new PdfPCell(new Phrase(property.Text, property.Font));
            TableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            TableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_BOTTOM;
            TableCell.Border = property.Border ?? IRectangle.BOX;
            TableCell.Colspan = property.Colspan ?? 1;
            TableCell.Rowspan = property.Rowspan ?? 1;
            if (property.backgroundColor != null) TableCell.BackgroundColor = new BaseColor(property.backgroundColor.Red, property.backgroundColor.Green, property.backgroundColor.Blue);

            return TableCell;
        }

        public class CellProperty
        {
            public string Text { get; set; }
            public IFont Font { get; set; }
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

        public static string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        private static string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }
    }
}