using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using SecuGen.SecuBSPPro.Windows;
using System.Text;
using JabjaiEntity.DB;
using System.Web.SessionState;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for getUserMoney
    /// </summary>
    public class getUserMoney : IHttpHandler, IRequiresSessionState
    {
        //private SecuBSPMx m_SecuBSP;
        public void ProcessRequest(HttpContext context)
        {
            //JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            //string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            //string sHtml = "";
            ////int sID = int.Parse(_str);
            //_str = _str.Replace("2F", "/");
            //m_SecuBSP = new SecuBSPMx();
            //foreach (var _data in _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)))
            //{
            //    if (m_SecuBSP.VerifyMatch(_str, _data.sFinger.ToString()) == BSPError.ERROR_NONE || m_SecuBSP.VerifyMatch(_str, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
            //    {
            //        if (m_SecuBSP.IsMatched)
            //        {
            //            //fcommon.LED8(_data.nMoney.Value.ToString(), 2);
            //            sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value;
            //        }
            //    }
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