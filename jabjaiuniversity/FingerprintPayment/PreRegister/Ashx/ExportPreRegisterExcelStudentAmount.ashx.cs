using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using System.Text;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiMainClass;

namespace FingerprintPayment.PreRegister.Ashx
{
    /// <summary>
    /// Summary description for ExportPreRegisterExcelStudentAmount
    /// </summary>
    public class ExportPreRegisterExcelStudentAmount : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            string year = (string)context.Request["year"];
            string regStatus = (string)context.Request["regStatus"];
            string optLevel = (string)context.Request["optLevel"];
            string couType = (string)context.Request["couType"];
            string couTime = (string)context.Request["couTime"];
            string branch = (string)context.Request["branch"];
            string stdName = (string)context.Request["stdName"];
            string plan = (string)context.Request["plan"];

            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            ExcelPackage excel = ReportEngine.ExportPreRegisterExcelStudentAmount(userData.CompanyID, year, regStatus, optLevel, couType, couTime, branch, stdName, plan);

            context.Response.Clear();
            context.Response.AddHeader("content-disposition", "attachment; filename=ReportStudentAmount_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
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