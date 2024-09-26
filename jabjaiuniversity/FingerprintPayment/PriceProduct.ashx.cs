using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for PriceProduct
    /// </summary>
    public class PriceProduct : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string _str = fcommon.ReplaceInjection(context.Request["ID"]);
                string sList = fcommon.ReplaceInjection(context.Request["List"]);
                string sHtml = "";
                string sProduct = _str;
                foreach (var _data in _db.TProducts.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sBarCode == sProduct && string.IsNullOrEmpty(w.cDel)))
                {
                    sHtml = _data.nPrice.Value.ToString() + "|" + _data.sProduct;
                    //fcommon.LED8(_data.nPrice.Value.ToString(), 1);
                }
                context.Response.Expires = -1;
                context.Response.ContentType = "text/plain";
                context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(sHtml.ToString());
                context.Response.End();
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