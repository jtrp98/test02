using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using JabjaiEntity.DB;
using System.Web.SessionState;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for GetProductName
    /// </summary>
    public class GetProductName : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            string sHtml = "";

            foreach (var _data in _db.TProducts.Where(w => w.sBarCode == _str && string.IsNullOrEmpty(w.cDel)).ToList())
            {
                sHtml += _data.sProduct.ToString();
                //sHtml = _data.sFinger;
            }

            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sHtml.ToString());
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