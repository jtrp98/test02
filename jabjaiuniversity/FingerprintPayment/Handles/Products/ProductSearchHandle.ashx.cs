using Jabjai.BusinessLogic;
using Jabjai.Object;
using entityPeak = Jabjai.Object.Entity.Peak;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Products
{
    /// <summary>
    /// Summary description for ProductSearchHandle
    /// </summary>
    public class ProductSearchHandle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string json = string.Empty;
            int schoolId = 3;// #HardcodeSchoolId

            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
            json = new JavaScriptSerializer().Serialize(this.GetDataJSON(context.Request.Form["term"], schoolId));
            context.Response.ContentType = "text/json";
            context.Response.Write(json);
            context.Response.End();
        }

        private BaseResult<List<entityPeak.Product>> GetDataJSON(string filter, int schoolId)
        {
            var productLogic = new ProductLogic();
            return productLogic.GetPeakByTextSearch(filter, schoolId);
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