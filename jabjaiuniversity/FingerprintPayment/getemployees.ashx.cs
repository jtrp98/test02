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
    /// Summary description for getemployees
    /// </summary>
    public class getemployees : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            string sHtml = "";
            int sID = int.Parse(_str);
            foreach (var _data in _db.TEmployees.Where(w => w.sEmp == sID && string.IsNullOrEmpty(w.cDel)).ToList())
            {
                sHtml += _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value;
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