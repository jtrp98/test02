using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoicePrintHandle
    /// </summary>
    public class InvoicePrintHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //HtmlToPdf converter = new HtmlToPdf();

            //// set converter options
            ////// converter.Options.PdfPageSize = pageSize;
            ////// converter.Options.PdfPageOrientation = pdfOrientation;
            ////// converter.Options.WebPageWidth = webPageWidth;
            ////// converter.Options.WebPageHeight = webPageHeight;

            //////// create a new pdf document converting an html string
            //converter.Options.PdfPageSize = PdfPageSize.A4;
            //converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            //converter.Options.MarginLeft = 10;
            //converter.Options.MarginRight = 10;
            //converter.Options.MarginTop = 20;
            //converter.Options.MarginBottom = 20;
            //PdfDocument doc = converter.ConvertHtmlString("111");//, baseUrl);

            // save pdf document
            //doc.Save(Response, false, "Invoice.pdf");

            // close pdf document
            //doc.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}