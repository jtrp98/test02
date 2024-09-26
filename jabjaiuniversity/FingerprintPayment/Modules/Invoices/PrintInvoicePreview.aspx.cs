using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using JabjaiEntity.DB;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using ZXing.Common;
using ZXing;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class PrintInvoicePreview : Page
    {
        private InvoiceLogic invoiceLogic;
        private UserLogic userLogic;
        private TermLogic termLogic;
        public InvoiceDTO Model { get; set; }
        public Jabjai.Object.Entity.Jabjai.TUser ModelUser { get; set; }
        public string REF { get; set; }
        public string REF2 { get; set; }
        public string ClassLabel { get; set; }
        public string ModelJson { get; set; }
        public string DueDate { get; set; }
        public string NameEng { get; set; }
        public string Issue { get; set; }
        public decimal FirstPaid { get; set; }
        public decimal SecondPaid { get; set; }
        public TCompany schoolData { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OutStadingAmount { get; set; }
        public List<PaymentMethodDTO> paymentMethod { get; set; }

        public static InvoiceImage invoiceImage = new InvoiceImage();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            invoiceLogic = new InvoiceLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();
            BankLogic bankLogic = new BankLogic();
            PaymentMethodLogic paymentMethodLogic = new PaymentMethodLogic();
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                {
                    Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
                    string entities = Session["sEntities"].ToString();

                    schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == entities);


                    BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));
                    Model = invoice.Result;
                    Model.InvoiceItems.RemoveAll(w => w.IsDelete);
                    var term = termLogic.GetById(invoice.Result.TermId);
                    var q_titles = dbschool.TTitleLists.ToList();

                    var studentViews = dbschool.TB_StudentViews.Where(f => f.SchoolID == invoice.Result.SchoolId && f.sID == invoice.Result.StudentId && f.nTerm == invoice.Result.TermId).AsQueryable().FirstOrDefault();
                    var user = userLogic.GetById(invoice.Result.StudentId);
                    invoice.Result.StudentIdCard = user.Result.StudentIdCardNumber;
                    if (string.IsNullOrEmpty(invoice.Result.TermId) || studentViews == null)
                    {
                        invoice.Result.StudentCode = user.Result.StudentID;
                        if (user.Result.TTermSubLevel2 != null)
                        {
                            invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                            invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                        }
                        var f_user = dbschool.TUser.Find(invoice.Result.StudentId, schoolData.nCompany);
                        Model.StudentName = getTitlte(q_titles, f_user.sStudentTitle) + " " + f_user.sName + " " + f_user.sLastname;
                        NameEng = f_user.sStudentNameEN + " " + f_user.sStudentLastEN;
                    }
                    else
                    {
                        invoice.Result.StudentIdCard = user.Result.StudentIdCardNumber;
                        invoice.Result.StudentCode = studentViews.sStudentID;
                        Model.StudentName = getTitlte(q_titles, studentViews.sStudentTitle) + " " + studentViews.sName + " " + studentViews.sLastname;
                        Model.SubLevel = studentViews.SubLevel;
                        Model.SubLevel2 = studentViews.nTSubLevel2;
                        Model.Term = studentViews.sTerm;
                        Model.TermYear = studentViews.numberYear.Value.ToString();
                        NameEng = studentViews.sStudentNameEN + " " + studentViews.sStudentLastEN;
                    }


                    if (!string.IsNullOrEmpty(Model.LabelSubLevel)) Model.SubLevel = Model.LabelSubLevel;
                    if (!string.IsNullOrEmpty(Model.LabelSubLevel2)) Model.SubLevel2 = Model.LabelSubLevel2;

                    var q_banks = bankLogic.GetAll().Result;
                    paymentMethod = paymentMethodLogic.GetBySchoolId(schoolData.nCompany).Result;
                    paymentMethod.ForEach(f =>
                    {
                        if (f.BankId.HasValue)
                        {
                            var f_banks = q_banks.FirstOrDefault(f1 => f1.BankId == f.BankId);
                            if (f_banks != null) f.BankName = f_banks.BankName;
                        }
                    });

                    if (term.Result != null)
                    {
                        invoice.Result.Term = term.Result.sTerm;
                        invoice.Result.nYear = term.Result.Year.Number.Value;
                    }

                    DueDate = invoice.Result.DueDate.ToBuddhishDateString();
                    Issue = invoice.Result.Date.ToBuddhishDateString();

                    string SQL = string.Format(@"DECLARE @nTermSubLevel2 INT = {0};
DECLARE @nTerm NVARCHAR(20) = '{1}'

SELECT TOP 1 D1.* 
FROM TPlan AS A
INNER JOIN TPlanCourse AS B
ON A.SchoolID = B.SchoolID AND A.PlanId = B.PlanId 
INNER JOIN TPlanCourseTerm AS C
ON B.SchoolID = C.SchoolID AND B.PlanCourseId = C.PlanCourseId 
INNER JOIN TPlanTermSubLevel2 AS D
ON A.PlanId = D.PlanId AND A.SchoolID = D.SchoolID
INNER JOIN TCurriculum AS D1
ON D1.CurriculumId = A.CurriculumId AND A.SchoolID = D1.SchoolId
WHERE C.nTerm = @nTerm AND D.nTermSubLevel2 = @nTermSubLevel2 
AND D.IsActive = 1 AND A.IsActive = 1
", (Model.Fd_NewTermClass_id ?? Model.SubLevel2Id), Model.TermId);

                    var paidPayments = invoice.Result.PaidPayments.Where(w => string.IsNullOrEmpty(w.Status)).Sum(s => s.Payments.Sum(s1 => s1.Amount)) ?? 0;
                    invoice.Result.PaidPayments = invoice.Result.PaidPayments.Where(w => string.IsNullOrEmpty(w.Status)).ToList();

                    //FirstPaid = Convert.ToInt32(Math.Round(((decimal)invoice.Result.GrandTotal * 60 / 100)));
                    //SecondPaid = Convert.ToInt32(Math.Round(((decimal)invoice.Result.GrandTotal * 40 / 100)));

                    FirstPaid = ((decimal)invoice.Result.GrandTotal * 60 / 100);
                    SecondPaid = ((decimal)invoice.Result.GrandTotal * 40 / 100);

                    if (FirstPaid >= paidPayments) FirstPaid -= paidPayments;
                    else
                    {
                        SecondPaid -= (paidPayments - FirstPaid);
                        FirstPaid = 0;
                    }

                    TotalPaid = invoice.Result.GrandTotal.Value - invoice.Result.OutstandingAmount.Value;
                    OutStadingAmount = invoice.Result.OutstandingAmount.Value;

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

                    var q1 = dbschool.Database.SqlQuery<TCurriculum>(SQL).FirstOrDefault();
                    if (q1 != null)
                    {
                        ClassLabel = Model.SubLevel + " หลักสูตร " + q1.CurriculumName;
                    }
                    else
                    {
                        ClassLabel = Model.SubLevel;
                    }

                    //REF = invoice.Result.StudentCode + "-" + invoice.Result.nYear + "-" + invoice.Result.Term;
                    //REF2 = invoice.Result.SubLevel + invoice.Result.SubLevel2 + invoice.Result.Term + invoice.Result.TermYear;

                    int Term = 0, StudentCode;
                    int.TryParse(Model.Term, out Term);
                    string Fd_Suffix = "00";
                    int.TryParse(Model.StudentCode, out StudentCode);
                    decimal OutstandingAmount = Model.OutstandingAmount ?? 0;

                    TB_PaymentGateway paymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany);
                    if (paymentGateway != null)
                    {
                        Fd_Suffix = paymentGateway.Fd_Suffix ?? "00";
                    }

                    REF = string.Format("{0}{1:00}{2:0000000000}", Model.TermYear, Term, StudentCode);
                    REF2 = string.Format("{0:00000}{1}", Model.SchoolId, Model.Code.Replace("IV-", ""));
                    string TaxId = schoolData.TaxId.Replace("-", "") + Fd_Suffix;
                    string Amount = OutstandingAmount.ToString("#0.00").Replace(".", "");

                    string BarCode = "|" + TaxId + "\r\n" + REF + "\r\n" + REF2 + "\r\n" + Amount;

                    // Set the desired size of the barcode image in centimeters
                    float widthCm = 8f;
                    float heightCm = 1f;

                    // Convert centimeters to inches
                    float widthIn = widthCm / 2.54f;
                    float heightIn = heightCm / 2.54f;

                    // Set the DPI of the barcode image
                    int dpi = 300;

                    // Calculate the size of the barcode image in pixels based on the desired size and DPI
                    //int widthPx = (int)(widthIn * dpi);
                    //int heightPx = (int)(heightIn * dpi);
                    int widthPx = 170;
                    int heightPx = 38;

                    // Create a barcode writer object
                    BarcodeWriter writer = new BarcodeWriter
                    {
                        // Set the barcode format and options
                        Format = BarcodeFormat.CODE_128,
                    };

                    EncodingOptions options = new EncodingOptions
                    {
                        Width = widthPx,
                        Height = heightPx,
                        Margin = 10,
                        PureBarcode = true,
                        //Height = 30 // adjust the height of the text label
                    };

                    writer.Options = options;
                    Bitmap barcode = writer.Write(BarCode);

                    // Define the area of the image to be cropped
                    int x = 50;  // x-coordinate of the upper-left corner of the crop area
                    int y = 50;  // y-coordinate of the upper-left corner of the crop area

                    // Create a new bitmap with extra space at the bottom for padding
                    Bitmap paddedBitmap = new Bitmap(barcode.Width, barcode.Height + 14);
                    using (Graphics g = Graphics.FromImage(paddedBitmap))
                    {
                        g.Clear(Color.White);
                        //g.DrawImage(barcode, new Rectangle(0, 0, widthPx, heightPx), new Rectangle(x, y, widthPx, heightPx), GraphicsUnit.Pixel);
                        g.DrawImage(barcode, new PointF(0, 0));
                    }

                    using (Graphics g = Graphics.FromImage(paddedBitmap))
                    {
                        string text = BarCode.Replace("\r\n", " ").Replace("|", "");
                        Font font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Near
                        };
                        RectangleF rect = new RectangleF(10, barcode.Height - 1, barcode.Width, 0);
                        g.DrawString(text, font, Brushes.Black, rect, format);
                    }

                    int Width = (paddedBitmap.Width * 80) / 100, Height = (paddedBitmap.Height * 80) / 100;

                    // Resize the barcode image
                    //Bitmap resizedBarcode = new Bitmap(paddedBitmap, new Size(Width, Height));

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        // Save the barcode image to a memory stream
                        paddedBitmap.Save(memoryStream, ImageFormat.Png);

                        // Get the byte array of the image data
                        byte[] imageData = memoryStream.ToArray();

                        // Convert the byte array to a base64 string
                        string imageDataBase64 = Convert.ToBase64String(imageData);

                        // Create the data URI
                        invoiceImage.BarCode = "data:image/png;base64," + imageDataBase64;
                    }

                    invoiceImage.QRCode = QRCodeFunction.Create(BarCode, QRCoder.QRCodeGenerator.ECCLevel.H);
                }

                ModelJson = new JavaScriptSerializer().Serialize(Model);
            }
        }


        private string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }
    }

    public class InvoiceImage
    {
        public string QRCode { get; set; }
        public string BarCode { get; set; }
    }
}

