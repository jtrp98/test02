using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoicePaymentMethodHandler
    /// </summary>
    public class InvoicePaymentMethodHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) context.Response.Redirect("~/Default.aspx");

            var jsonString = context.Request.Form["invoice"];
            int schoolId = 3;   //#HardcodeSchoolId
            string userName = context.Session["Emp_Name"].ToString();   //#HardcodeuserName
            BaseResult<bool> result = new BaseResult<bool>();

            try
            {

                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(),ref schoolId);
                PaymentMethodDTO paymentMethod = JsonConvert.DeserializeObject<PaymentMethodDTO>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                paymentMethod.PaymentMethodId = Guid.NewGuid().ToString();
                paymentMethod.SchoolId = schoolId;
                paymentMethod.CreateDate = DateTime.Now;
                paymentMethod.UpdateDate = DateTime.Now;
                paymentMethod.CreateBy = userName;
                paymentMethod.UpdateBy = userName;
                paymentMethod.IsActive = true;

                PaymentMethodLogic paymentMethodLogic = new PaymentMethodLogic();

                result = paymentMethodLogic.Create(paymentMethod);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.SystemErrorMessage = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
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