using JabjaiMainClass;
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
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for getUser
    /// </summary>
    public class getUser : IHttpHandler, IRequiresSessionState
    {

        JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
        //private SecuBSPMx m_SecuBSP;
        public void ProcessRequest(HttpContext context)
        {
            //string _str = fcommon.ReplaceInjection(context.Request["ID"]);
            //string[] _array = _str.Split('_');
            //string sHtml = _array[0];
            ////int sID = int.Parse(_str);
            ////_str = _str.Replace("2F", "/");
            //SecuBSPMx m_SecuBSP = new SecuBSPMx();
            ////Int32 match_score = 0;
            ////m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, ref match_score);
            //string sFinger = _array[2];// "AwAAABQAAACEAQAAAQASAAEAZAAAAAAAfgEAAPGmyp3OLYvX1xCXSxXatmYazLl6OVa6a2cVzeGtd5iSFfFaSz0SIgmh3oogDCVUADNCKCLMJ8mspzdsulSEJyQTca7wkjJkFDWChA6HZ8yG/NvR/oZGRJsL3JI8wukHIyDvxoJxFFfWuGDh/c0gx1BBDXj2dlVHGPob4lnfgkW3Ho95dO8sBWmK7iZC5zSU*9t85ezxhVv2NYb7F7xs96S/4moUJuCIt53LPjYlF1VmZzhYUn82Vmo7zZXCk/vFG48LZNBFwYng1KZTuwcHJRzPQ1xXC45kmabBvJd48PendRCQH6jiYh9/V0BNCtUUtfU8RRa4uhTs4MRWTUVrRvd9Fx1KGKr1Ss7guwe*fgc68UwRuFj/pWCn9OVZZt4gD/0BE0vHqDvRiAoH0T97vIY/6M2H1p2IXAQFHUeZtjypnWLWDyOEUeRDcFGdMS9p23Pcy2XpO8ShmRr/ApE*sfHVukrljlgtLH4xeP*R60LXrFKN4qHKGfNatIE/K9MnJQ";
            //if (_array[1] == "JobScan")
            //{
            //    foreach (var _data in _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel) && w.sIdentification.EndsWith(sHtml)))
            //    {
            //        if (m_SecuBSP.VerifyMatch(sFinger, _data.sFinger.ToString()) == BSPError.ERROR_NONE || m_SecuBSP.VerifyMatch(sFinger, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
            //        {
            //            if (m_SecuBSP.IsMatched)
            //            {
            //                sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.sFinger + "|" + _data.sFinger2;
            //            }
            //        }
            //        //if (m_SecuBSP.VerifyMatch(_str, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
            //        //{
            //        //    if (m_SecuBSP.IsMatched)
            //        //        sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value;
            //        //}
            //    }
            //}
            //else if (_array[1] == "CheckMoney")
            //{
            //    foreach (var _data in _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel) && w.sIdentification.EndsWith(sHtml)))
            //    {
            //        if (m_SecuBSP.VerifyMatch(sFinger, _data.sFinger.ToString()) == BSPError.ERROR_NONE || m_SecuBSP.VerifyMatch(sFinger, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
            //        {
            //            if (m_SecuBSP.IsMatched)
            //            {
            //                sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney + "|" + _data.nMax + "|" + _data.nMax;
            //            }
            //        }
            //    }
            //}
            //else if (_array[1] == "CheckEmpScan")
            //{
            //    int sEmp = int.Parse(_array[0]);
            //    foreach (var _data in _db.TEmployees.Where(w => string.IsNullOrEmpty(w.cDel) && w.sEmp == sEmp))
            //    {
            //        sHtml += _data.sEmp + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.sFinger + "|" + _data.sFinger2;
            //    }
            //}
            //else
            //{
            //    foreach (var _data in _db.TEmployees.Where(w => string.IsNullOrEmpty(w.cDel) && w.sIdentification.EndsWith(sHtml)))
            //    {
            //        sHtml += _data.sEmp + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.sFinger + "|" + _data.sFinger2;
            //    }
            //}
            //context.Response.Expires = -1;
            //context.Response.ContentType = "text/plain";
            //context.Response.ContentEncoding = Encoding.UTF8;
            //context.Response.Write(sHtml.ToString());
            //context.Response.End();
        }


        //public void ProcessRequest(HttpContext context)
        //{
        //    string _str = fcommon.ReplaceInjection(context.Request["ID"]);
        //    string sHtml = "";
        //    //int sID = int.Parse(_str);
        //    _str = _str.Replace("2F", "/");
        //    m_SecuBSP = new SecuBSPMx();
        //    //Int32 match_score = 0;
        //    //m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, ref match_score);
        //    foreach (var _data in _db.TUsers.Where(w => string.IsNullOrEmpty(w.cDel)))
        //    {
        //        if (m_SecuBSP.VerifyMatch(_str, _data.sFinger.ToString()) == BSPError.ERROR_NONE || m_SecuBSP.VerifyMatch(_str, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
        //        {
        //            if (m_SecuBSP.IsMatched)
        //            {
        //                sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value;
        //                //fcommon.LED8(_data.nMoney.Value.ToString(), 2);
        //            }
        //        }
        //        //if (m_SecuBSP.VerifyMatch(_str, _data.sFinger2.ToString()) == BSPError.ERROR_NONE)
        //        //{
        //        //    if (m_SecuBSP.IsMatched)
        //        //        sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value;
        //        //}
        //    }
        //    context.Response.Expires = -1;
        //    context.Response.ContentType = "text/plain";
        //    context.Response.ContentEncoding = Encoding.UTF8;
        //    context.Response.Write(sHtml.ToString());
        //    context.Response.End();
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}