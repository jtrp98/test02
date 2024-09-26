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
    /// Summary description for PaymentMethodListHandler
    /// </summary>
    public class PaymentMethodListHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) context.Response.Redirect("~/Default.aspx");

            string jsonString = string.Empty;
            int schoolId = 3;   //#HardcodeSchoolId
            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
            PaymentMethodLogic logic = new PaymentMethodLogic();
            BaseResult<DTResult<PaymentMethodDTO>> result = new BaseResult<DTResult<PaymentMethodDTO>>();

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