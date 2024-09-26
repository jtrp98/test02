using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.SessionState;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for ShowDisplyLed
    /// </summary>
    public class ShowDisplyLed : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            //int sID = int.Parse(_str);
            try
            {
            }
            catch { }
            string sHtml = _str;
            context.Response.Expires = -1;
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(sHtml);
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