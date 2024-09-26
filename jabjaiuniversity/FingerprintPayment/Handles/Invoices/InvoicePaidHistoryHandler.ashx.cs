using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoicePaidHistoryHandler
    /// </summary>
    public class InvoicePaidHistoryHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            else context.Response.Redirect("~/Default.aspx");

            PaymentLogic logic = new PaymentLogic();
            InvoiceLogic invoiceLogic = new InvoiceLogic();
            BaseResult<List<PaidPaymentDTO>> result = new BaseResult<List<PaidPaymentDTO>>();
            BaseResult<InvoiceDTO> baseResult = new BaseResult<InvoiceDTO>();
            string invoiceId = context.Request.Form["InvoiceId"];
            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString());
            try
            {
                Dictionary<string, string> filters = new Dictionary<string, string>();
                filters.Add("InvoiceId", invoiceId);
                result = logic.GetHistory(filters);
                baseResult = invoiceLogic.GetById(int.Parse(invoiceId));
                List<PaidPaymentDTO> Model = result.Result;

                Model.RemoveAll(w => w.isDel ?? false);
                if ((baseResult.Result.ManualDiscount ?? 0) == 0)
                {
                    foreach (var paidPaymentDTO in Model)
                    {
                        paidPaymentDTO.Payments.RemoveAll(r => r.Amount == 0 || r.isDel == true);
                    }
                }
                else
                {
                    foreach (var paidPaymentDTO in Model)
                    {
                        paidPaymentDTO.Payments.RemoveAll(r => r.isDel == true);
                    }
                }


                result.Result = Model.OrderBy(o => o.PaymentGroupId).ToList();

                context.Response.ContentType = "text/json";
                context.Response.Write(new JavaScriptSerializer().Serialize(result));
                context.Response.End();
            }
            catch (Exception ex)
            {
                result.StatusCode = "500";
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }
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