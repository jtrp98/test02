using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoiceDetailHandler
    /// </summary>
    public class InvoiceDetailHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString());
            var invoiceId = context.Request.QueryString["invoice_id"];

            InvoiceLogic logic = new InvoiceLogic();
            BaseResult<InvoiceDTO> result = logic.GetById(Convert.ToInt32(invoiceId));
            result.Result.InvoiceItems = result.Result.InvoiceItems.Where(w => w.IsDelete == false).ToList();
            result.Result.PaidPayments.ForEach(f =>
                 f.Payments.RemoveAll(r => (r.isDel ?? false) == false || r.IsDelete == false)
            );

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