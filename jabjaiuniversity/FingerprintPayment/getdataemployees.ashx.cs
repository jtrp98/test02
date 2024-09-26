using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using JabjaiEntity.DB;
using System.Web.SessionState;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for getdataemployees
    /// </summary>
    public class getdataemployees : IHttpHandler, IRequiresSessionState
    {

        //private SecuBSPMx m_SecuBSP;
        JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
        public void ProcessRequest(HttpContext context)
        {
            //string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            //string sHtml = "";

            //BSPError err;
            //_str = _str.Replace("2F", "/");
            //m_SecuBSP = new SecuBSPMx();
            //foreach (var _data in _db.TEmployees.Where(w => string.IsNullOrEmpty(w.cDel)).ToList())
            //{
            //    if (m_SecuBSP.VerifyMatch(_str, _data.sFinger.ToString()) == BSPError.ERROR_NONE || m_SecuBSP.VerifyMatch(_str, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
            //    {
            //        if (m_SecuBSP.IsMatched)
            //            sHtml = STCrypt.Encrypt(_data.sEmp.ToString());
            //    }
            //    //if (m_SecuBSP.VerifyMatch(_data.sFinger2.ToString(), _str) == BSPError.ERROR_NONE)
            //    //{
            //    //    if (m_SecuBSP.IsMatched)
            //    //        sHtml = STCrypt.Encrypt(_data.sEmp.ToString());
            //    //}
            //}


            //context.Response.Expires = -1;
            //context.Response.ContentType = "text/plain";
            //context.Response.ContentEncoding = Encoding.UTF8;
            //context.Response.Write(sHtml.ToString());
            //context.Response.End();
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