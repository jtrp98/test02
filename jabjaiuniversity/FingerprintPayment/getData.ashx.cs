using System;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
//using SecuGen.SecuBSPPro.Windows;
using JabjaiEntity.DB;
using System.Web.SessionState;
using JabjaiMainClass;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for getData
    /// </summary>
    public class getData : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities _db = new JabJaiEntities(HttpContext.Current.Session["sEntities"].ToString());
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            string sHtml = "";

            foreach (var _data in _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)).ToList())
            {
                sHtml += (sHtml == "" ? "" : " ") + _data.sID + "_" + _data.sFinger;
                sHtml += (sHtml == "" ? "" : " ") + _data.sID + "_" + _data.sFinger2;
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