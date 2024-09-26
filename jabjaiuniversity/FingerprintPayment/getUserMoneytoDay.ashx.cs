using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
//using SecuGen.SecuBSPPro.Windows;
using System.Net;
using JabjaiEntity.DB;
using System.Web.SessionState;
using MasterEntity;

namespace FingerprintPayment
{
    /// <summary>
    /// Summary description for getUserMoneytoDay
    /// </summary>
    public class getUserMoneytoDay : IHttpHandler, IRequiresSessionState
    {

        JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
        //private SecuBSPMx m_SecuBSP;
        public void ProcessRequest(HttpContext context)
        {
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
            //            decimal? _MoneyDay = 0;
            //            if (_db.TSells.Where(w => w.sID == _data.sID && w.dSell.Value >= DateTime.Today).Count() > 0)
            //            {
            //                _MoneyDay = _db.TSells.Where(w => w.sID == _data.sID && w.dSell.Value >= DateTime.Today).Sum(s => s.nTotal).Value;
            //            }
                        
            //            sHtml += _data.sID + "|" + _data.sName + "|" + _data.sLastname + "|" + _data.nMoney.Value + "|" + (_data.nMax == 0 ? "ไม่จำกัดวงเงิน" : (_data.nMax - _MoneyDay) + "");
            //        }
            //        //WebForm4.sendsms(_data.sName + " " + _data.sLastname + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "66918233139");
            //    }
            //}

            ////int? _sID = 1;
            ////TStudenttime _Studenttime = new TStudenttime();
            ////_Studenttime.nTime = 1;
            ////_Studenttime.sClassIP = GetLanIPAddress();
            ////_Studenttime.sID = _sID;
            ////_db.AddToTStudenttime(_Studenttime);
            ////_db.SaveChanges();


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
        private String GetLanIPAddress(bool GetLan = false)
        {
            string visitorIPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (String.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(visitorIPAddress))
                visitorIPAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(visitorIPAddress) || visitorIPAddress.Trim() == "::1")
            {
                GetLan = true;
                visitorIPAddress = string.Empty;
            }

            if (GetLan && string.IsNullOrEmpty(visitorIPAddress))
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = Dns.GetHostName();
                //Get Ip Host Entry
                IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    visitorIPAddress = arrIpAddress[arrIpAddress.Length - 2].ToString();
                }
                catch
                {
                    try
                    {
                        visitorIPAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = Dns.GetHostAddresses(stringHostName);
                            visitorIPAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            visitorIPAddress = "127.0.0.1";
                        }
                    }
                }

            }
            return visitorIPAddress;
        }
    }
}