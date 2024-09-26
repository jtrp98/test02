using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class PrintPreview : System.Web.UI.Page
    {
        private InvoiceLogic invoiceLogic;
        private UserLogic userLogic;
        private TermLogic termLogic;
        public InvoiceDTO Model { get; set; }
        public Jabjai.Object.Entity.Jabjai.TUser ModelUser { get; set; }
        public string ModelJson { get; set; }
        public TCompany schoolData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            invoiceLogic = new InvoiceLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                {
                    string entities = Session["sEntities"].ToString();

                    schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == entities);
                    BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Page.RouteData.Values["InvoiceId"].ToString()));
                    Model = invoice.Result;
                    Model.InvoiceItems.RemoveAll(w => w.IsDelete);
                    BaseResult<TTerm> term = termLogic.GetById(invoice.Result.TermId);
                    var user = userLogic.GetById(invoice.Result.StudentId);
                    invoice.Result.StudentIdCard = user.Result.Identification;
                    invoice.Result.StudentCode = user.Result.StudentID;
                    invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                    invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                    if (term.Result != null)
                    {
                        invoice.Result.Term = term.Result.sTerm;
                        invoice.Result.nYear = term.Result.Year.Number.Value;
                    }
                }

                ModelJson = new JavaScriptSerializer().Serialize(Model);
            }
            /*  HtmlToPdf converter = new HtmlToPdf();

              // set converter options
              //// converter.Options.PdfPageSize = pageSize;
              //// converter.Options.PdfPageOrientation = pdfOrientation;
              //// converter.Options.WebPageWidth = webPageWidth;
              //// converter.Options.WebPageHeight = webPageHeight;

              ////// create a new pdf document converting an html string
              converter.Options.PdfPageSize = PdfPageSize.A4;
              converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
              converter.Options.MarginLeft = 10;
              converter.Options.MarginRight = 10;
              converter.Options.MarginTop = 20;
              converter.Options.MarginBottom = 20;
              PdfDocument doc = converter.ConvertHtmlString(page2.InnerHtml);//, baseUrl);*/

            // save pdf document
            //  doc.Save(Response, false, "Invoice.pdf");

            // close pdf document
            //  doc.Close();
        }

    }
}