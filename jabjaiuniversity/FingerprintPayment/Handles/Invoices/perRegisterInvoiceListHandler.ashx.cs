using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoiceListHandler
    /// </summary>
    public class perRegisterInvoiceListHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            else context.Response.Redirect("~/Default.aspx");

            string jsonString = string.Empty;
            int schoolId = 3;   //#HardcodeSchoolId
            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
            InvoiceLogic logic = new InvoiceLogic();
            BaseResult<DTResult<InvoiceDTO>> result = new BaseResult<DTResult<InvoiceDTO>>();

            try
            {
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonString = reader.ReadToEnd();
                }

                var dTParameters = JsonConvert.DeserializeObject<DTParameters>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                result = logic.Retrieve(dTParameters, schoolId);
            }
            catch (Exception ex)
            {
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
                context.Response.StatusCode = 500;
            }

            foreach (var data in result.Result.data)
            {
                data.OutstandingAmount = data.GrandTotal - (data.PaidPayments.Where(w => w.Status != "Void").Sum(s => s.Amount) ?? 0);
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result.Result));
            context.Response.End();
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