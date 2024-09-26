using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Data;
using System.IO;
using System.Configuration;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class profilescanning : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //SecuAPI_MonitorDevice();
            //Response.Write(GetLanIPAddress());
            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btnSavepage1);
            if (!IsPostBack)
            {
                SiteMapPath siteMapPath = (SiteMapPath)this.Master.FindControl("SiteMapPath1");
                siteMapPath.Visible = false;
            }
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((BeforeLoginMaster)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }

        public void focusTxtBarCode()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
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

        void btnSavepage1_Click(object sender, EventArgs e)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            int? sID = int.Parse(txtCheckFinger.Text);
            ViewState["sUserID"] = int.Parse(txtCheckFinger.Text);

            DateTime dtCurrentDays = DateTime.Now;
            DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.YearStatus == "1"));
            string identData = String.Empty;
            if (!String.IsNullOrEmpty(txtsID.Text) && txtsID.Text.Length == 4)
            {
                identData = txtsID.Text;
            }
            DataTable dtUser = fcommon.LinqToDataTable(_db.TUsers.Where(w => w.sID == sID && w.sIdentification.Contains(identData) && w.cDel == null));
            int currentYear = 0;
            if (dtYear != null)
            {
                currentYear = Int32.Parse(dtYear.Rows[0]["YearStatus"].ToString());
            }
            else
            {
                currentYear = dtCurrentDays.Date.Year;
            }


            if (dtUser != null && !String.IsNullOrEmpty(identData))
            {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CheckMoney", "<script type='text/javascript'>$(function(){hideScan('" + dtUser.Rows[0]["sName"] + "','" + dtUser.Rows[0]["sLastname"] + "','" + dtUser.Rows[0]["nMoney"] + "','" + dtUser.Rows[0]["nMax"] + "','" + dtUser.Rows[0]["sID"] + "'); getReportByID('" + dtUser.Rows[0]["sID"] + "','" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "'); return false;});</script>", false);
            }
            else
            {
                if (!String.IsNullOrEmpty(identData))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CheckMoney", "<script type='text/javascript'>$(function(){j_infosell('<span>ไม่พบข้อมูล</span>'); return false;});</script>", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "CheckMoney", "<script type='text/javascript'>$(function(){j_infosell('<span>กรอกรหัสสี่ตัว</span>'); return false;});</script>", true);
                }
            }
        }
    }
}