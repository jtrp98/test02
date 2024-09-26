using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Text;
using FingerprintPayment.AssetManagement.CsCode;

namespace FingerprintPayment.AssetManagement.Ashx
{
    /// <summary>
    /// Summary description for ExportReport02Excel
    /// </summary>
    public class ExportReport02Excel : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string year = (string)context.Request["year"];
            string cate = (string)context.Request["cate"];
            string col = (string)context.Request["col"];
            string dir = (string)context.Request["dir"];

            ExcelPackage excel = ReportEngine.ExportReport02Excel(col, dir, year, cate);

            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=Report_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
            context.Response.ContentType = "application/text";
            context.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
            context.Response.BinaryWrite(excel.GetAsByteArray());
            context.Response.Flush(); // Sends all currently buffered output to the client.
            context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
        }

        public bool IsReusable { get { return false; } }
    }
}