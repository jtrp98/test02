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
using System.Runtime.InteropServices;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class jobscaning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
            if (!IsPostBack)
            {
                SiteMapPath siteMapPath = (SiteMapPath)this.Master.FindControl("SiteMapPath1");
                siteMapPath.Visible = false;
                HttpCookie aCookie = Request.Cookies["sEntities"];
                string sEntities = Server.HtmlEncode(aCookie.Value);
                //Response.Write(sEntities);
                Session["sEntities"] = sEntities;
            }


            //Response.Write(username + "1");
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
        private void empscan(int sID, int nTimeType)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            ViewState["sUserID"] = sID;
            DateTime dtCurrentDays = DateTime.Now;
            string identData = String.Empty;
            if (!String.IsNullOrEmpty(txtsID.Text) && txtsID.Text.Length == 4)
            {
                identData = txtsID.Text;
            }
            DataTable dtUser = fcommon.LinqToDataTable(_db.TEmployees.Where(w => w.sEmp == sID && w.sIdentification.Contains(identData)));
            DataTable dtYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.YearStatus == "1"));
            int currentTimetype = int.Parse(dtUser.Rows[0]["nTimeType"].ToString());
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
                var today = DateTime.Today;
                int dtHolidayYear = _db.THolidays.Where(w => w.dHolidayStart <= today && w.dHolidayEnd >= today && w.sHolidayType == "0" && w.cDel == null).Count();

                if (dtHolidayYear == 0)
                {
                    DateTime dateTime = DateTime.Now;
                    string thisDay = dateTime.DayOfWeek.ToString();
                    int days = convertDay(thisDay);
                    DataTable dtTime = fcommon.LinqToDataTable(_db.TTimes.Where(w => w.nDay == days && w.nTimeType == nTimeType && w.cDel == "1" && w.nTimeType == currentTimetype));
                    if (dtTime.Rows.Count == 0)
                    {
                        var stopwatch = Stopwatch.StartNew();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>วันที่ไม่ได้ถูกตั้งค่าให้บันทึกข้อมูล</lable>'); return false;});", true);
                    }
                    else
                    {
                        DataTable DTTimeScan = fcommon.LinqToDataTable(_db.TLogEmpTimeScans.Where(w => w.LogEmpDate == DateTime.Today && w.sEmp == sID));

                        TLogEmpTimeScan tlogs = new TLogEmpTimeScan();
                        int nLogEmpScanID = 1;
                        if (_db.TLogEmpTimeScans.ToList().Count > 0) nLogEmpScanID = _db.TLogEmpTimeScans.Max(m => m.nLogEmpScanID) + 1;
                        tlogs.nLogEmpScanID = nLogEmpScanID;
                        tlogs.LogEmpTime = dateTime.TimeOfDay;
                        tlogs.sEmp = sID;
                        tlogs.LogEmpnDay = days;
                        tlogs.nYear = currentYear;
                        tlogs.LogEmpDate = dateTime.Date;

                        string sName = dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"];
                        string LogEmpType = "", LogEmpScanStatus = "";
                        CheackStatus(DTTimeScan, dtTime, ref LogEmpType, ref LogEmpScanStatus, sName, "Emp");
                        tlogs.LogEmpType = LogEmpType;
                        tlogs.LogEmpScanStatus = LogEmpScanStatus;

                        if (tlogs.LogEmpScanStatus != "-1")
                        {
                            _db.TLogEmpTimeScans.Add(tlogs);
                            _db.SaveChanges();
                        }
                    }
                }
                else if (dtHolidayYear > 0)
                {
                    string sHoliday = "";
                    foreach (var _data in _db.THolidays.Where(w => w.dHolidayStart <= today && w.dHolidayEnd >= today && w.sHolidayType == "0" && w.cDel == null))
                    {
                        sHoliday = _data.sHoliday;
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>วันเป็นวันหยุดเนื่องจากเป็นวัน " + sHoliday + "</lable>'); return false;});", true);
                }
                else
                {
                    if (!String.IsNullOrEmpty(identData))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>ไม่พบข้อมูล</lable>'); return false;});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>กรอกรหัสสี่ตัว</lable>'); return false;});", true);
                    }
                }
            }
            //else {
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>ไม่พบข้อมูล Error </lable>'); return false;});", true);
            //}
        }
        private void studentscan(int sID, int nTimeType)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
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
                var today = DateTime.Today;
                int dtHolidayYear = _db.THolidays.Where(w => w.dHolidayStart <= today && w.dHolidayEnd >= today && w.sHolidayType == "0" && w.cDel == null).Count();
                DataTable dtStudentLevel = fcommon.Get_Data(fcommon.ConfigSqlConnection(Session["sEntities"] + ""), @"select * from tuser left join TTermSubLevel2 on TUsers.nTermSubLevel2 =TTermSubLevel2.nTermSubLevel2
where sid =" + sID);
                int currentSubLV = Int32.Parse(dtStudentLevel.Rows[0]["nTermSubLevel2"].ToString());
                int? currentTimeType = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == currentSubLV).SingleOrDefault().nTimeType;
                DataTable dtHolidaySome = fcommon.LinqToDataTable(_db.THolidays.Where(w => w.dHolidayStart <= today && w.dHolidayEnd >= today && w.sHolidayType == "1"));
                string dataHolidaySomeID = "";
                if (dtHolidaySome != null)
                {
                    dataHolidaySomeID = dtHolidaySome.Rows[0]["nHoliday"].ToString();
                }
                DataTable dtHolidaySomeCheck = new DataTable();

                if (!String.IsNullOrEmpty(dataHolidaySomeID))
                {
                    var queryHoliday = from HolidayS in _db.THolidaySomes.ToList()
                                       where HolidayS.nTSubLevel == currentSubLV && HolidayS.nHoliday == dataHolidaySomeID
                                       select new
                                       {
                                           HolidayS = HolidayS.nHolidaySomeID
                                       };

                    if (queryHoliday.ToList().Count > 0)
                        dtHolidaySomeCheck = fcommon.LinqToDataTable(queryHoliday);

                }
                if ((dtHolidaySomeCheck.Rows.Count == 0 || dtHolidaySomeCheck == null) && dtHolidayYear == 0)
                {
                    DateTime dateTime = DateTime.Now;
                    string thisDay = dateTime.DayOfWeek.ToString();
                    int days = convertDay(thisDay);
                    DataTable dtTime = fcommon.LinqToDataTable(_db.TTimes.Where(w => w.nDay == days && w.nTimeType == nTimeType && w.cDel == "1" && w.nTimeType == currentTimeType));
                    if (_db.TTimes.Where(w => w.nDay == days && w.cDel == "0").Count() > 0)
                    {
                        var stopwatch = Stopwatch.StartNew();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>วันที่ไม่ได้ถูกตั้งค่าให้บันทึกข้อมูล</lable>'); return false;});", true);
                    }
                    else
                    {
                        DataTable DTTimeScan = fcommon.LinqToDataTable(_db.TLogUserTimeScans.Where(w => w.LogDate == DateTime.Today && w.sID == sID));
                        DateTime dateDBStartOut = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_OUT"].ToString());

                        DateTime dateDB = DateTime.Parse(dtTime.Rows[0]["dTimeStart_IN"].ToString());
                        DateTime dateDBOut = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_IN"].ToString());

                        TLogUserTimeScan tlogs = new TLogUserTimeScan();
                        int nLogScanID = 1;
                        if (_db.TLogUserTimeScans.ToList().Count > 0) nLogScanID = _db.TLogUserTimeScans.Max(m => m.nLogScanID) + 1;
                        tlogs.nLogScanID = nLogScanID;
                        tlogs.LogTime = dateTime.TimeOfDay;
                        tlogs.sID = sID;
                        tlogs.LogType = "0";
                        tlogs.LognDay = days;
                        tlogs.LogDate = dateTime.Date;
                        tlogs.nYear = currentYear;
                        string LogEmpType = "", LogEmpScanStatus = "", sName = "";
                        sName = dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastName"];
                        CheackStatus(DTTimeScan, dtTime, ref LogEmpType, ref LogEmpScanStatus, sName, "");
                        tlogs.LogType = LogEmpType;
                        tlogs.LogScanStatus = LogEmpScanStatus;

                        if (tlogs.LogScanStatus != "-1")
                        {
                            _db.TLogUserTimeScans.Add(tlogs);
                            _db.SaveChanges();
                        }
                        //}
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>วันนี้ตรงกับวันหยุดไม่สามารถสแกนได้</lable>'); return false;});", true);
                }
                // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtplanelist.Rows[0]["sName"] + " " + dtplanelist.Rows[0]["sLastname"] + "',''); return false;});", true);
            }
            else
            {
                if (!String.IsNullOrEmpty(identData))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>ไม่พบข้อมูล</lable>'); return false;});", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<lable>กรอกรหัสสี่ตัว</lable>'); return false;});", true);
                }
            }
        }
        private void CheackStatus(DataTable DTTimeScan, DataTable dtTime, ref string LogEmpType, ref string LogEmpScanStatus, string sName, string sType)
        {
            DateTime dateTime = DateTime.Now;

            DateTime dateDB_sIN = DateTime.Parse(dtTime.Rows[0]["dTimeStart_IN"].ToString());
            DateTime dateDB_eIN = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_IN"].ToString());

            DateTime dateDB_sOUT = DateTime.Parse(dtTime.Rows[0]["dTimeStart_OUT"].ToString());
            DateTime dateDB_eOUT = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_OUT"].ToString());

            DateTime dateDB_Timehalf = DateTime.Parse(dtTime.Rows[0]["dTimeHalf"].ToString());
            DateTime dateDB_TimeLate = dateDB_eIN.AddMinutes(int.Parse(dtTime.Rows[0]["nTimeLate"].ToString()));

            bool _CheckDTTimeScan = false;
            _CheckDTTimeScan = DTTimeScan == null;
            //ครึ่งเช้า
            if (dateTime.TimeOfDay <= dateDB_Timehalf.TimeOfDay)
            {
                if (!_CheckDTTimeScan) _CheckDTTimeScan = DTTimeScan.Select("Log" + sType + "Type = '0' ").Length == 0;
                if (_CheckDTTimeScan)
                {
                    //เข้าตรงเวลา
                    if (dateTime.TimeOfDay >= dateDB_sIN.TimeOfDay && dateTime.TimeOfDay <= dateDB_eIN.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "0";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','0',''); return false;});", true);
                    }
                    //สาย
                    else if (dateTime.TimeOfDay >= dateDB_eIN.TimeOfDay && dateTime.TimeOfDay <= dateDB_TimeLate.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "1";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','1',''); return false;});", true);
                    }
                    else if (dateTime.TimeOfDay <= dateDB_sIN.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "0";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','7',''); return false;});", true);
                    }
                    else if (dateTime.TimeOfDay >= dateDB_TimeLate.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "3";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','9',''); return false;});", true);
                    }
                    else //if (dateTime.TimeOfDay <= dateDB_eIN.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "-1";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','1',''); return false;});", true);
                    }
                }
                else
                {
                    string sTime = DateTime.Parse(DTTimeScan.Select("Log" + sType + "Type = '0' ")[0]["Log" + sType + "Time"] + "").ToString("HH:mm:ss");
                    LogEmpType = "1";
                    LogEmpScanStatus = "-1";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','5','" + sTime + "'); return false;});", true);
                }
            }
            else
            {
                if (!_CheckDTTimeScan) _CheckDTTimeScan = DTTimeScan.Select("Log" + sType + "Type = '1' ").Length == 0;
                if (_CheckDTTimeScan)
                {
                    //ออกตรงเวลา
                    if (dateTime.TimeOfDay >= dateDB_sOUT.TimeOfDay && dateTime.TimeOfDay <= dateDB_eOUT.TimeOfDay)
                    {
                        LogEmpType = "1";
                        LogEmpScanStatus = "0";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','0',''); return false;});", true);
                    }
                    //ออกช้า
                    else if (dateTime.TimeOfDay >= dateDB_eOUT.TimeOfDay)
                    {
                        LogEmpType = "1";
                        LogEmpScanStatus = "3";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','3',''); return false;});", true);
                    }
                    else
                    {
                        LogEmpType = "1";
                        LogEmpScanStatus = "2";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','2',''); return false;});", true);
                    }
                }
                else
                {
                    string sTime = DateTime.Parse(DTTimeScan.Select("Log" + sType + "Type = '1' ")[0]["Log" + sType + "Time"] + "").ToString("HH:mm:ss");
                    LogEmpType = "1";
                    LogEmpScanStatus = "-1";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','6','" + sTime + "'); return false;});", true);
                }
            }
        }
        void btnSavepage1_Click(object sender, EventArgs e)
        {
            string stxt = txtCheckFinger.Text;
            try
            {
                if (stxt.IndexOf("E") != -1)
                {
                    int sID = int.Parse(stxt.Split('E')[0]);
                    int nTimeType = int.Parse(stxt.Split('E')[1]);
                    empscan(sID, nTimeType);
                }
                else if (stxt.IndexOf("S") != -1)
                {
                    int sID = int.Parse(stxt.Split('S')[0]);
                    int nTimeType = int.Parse(stxt.Split('S')[1]);
                    studentscan(sID, nTimeType);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + ex.Message + "','99',''); return false;});", true);
            }

        }
        public void sendSMS(int statusType, int statusData, string userDataTel)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            string messageSend = "";
            DataTable dtSMS = fcommon.LinqToDataTable(_db.TSMS.Where(w => w.SMSType == 0 && w.SMSSubType == statusType && w.SMSStatus != "-1").OrderByDescending(w => w.nSMS));
            if (dtSMS != null)
            {
                messageSend = dtSMS.Rows[0]["SMSDesp"].ToString();
            }
            else
            {
                int sUserID = int.Parse(ViewState["sUserID"] + "");
                string sName = "";
                foreach (var _str in _db.TUsers.Where(w => w.sID == sUserID))
                {
                    sName = _str.sName + " " + _str.sLastname;
                }
                switch (statusType)
                {
                    case 0:
                        messageSend = sName + " สแกนเข้าโรงเรียนแล้ว " + statusScan(statusData);//"บุตรหลานของท่านสแกนเข้าโรงเรียนแล้ว ";
                        // เข้าเรียน
                        break;
                    case 1:
                        messageSend = sName + " สแกนออกโรงเรียนแล้ว " + statusScan(statusData); //"บุตรหลานของท่านสแกนออกโรงเรียนแล้ว ";
                        // ออกเรียน
                        break;
                }
            }

            mailFunc(messageSend, userDataTel);

        }
        public static bool mailFunc(string msg, string telNumber)
        {
            string telData = telNumber.Remove(0, 1);
            telData = "66" + telData;
            try
            {
                WebClient client = new WebClient();


                client.QueryString.Add("user", ConfigurationSettings.AppSettings["smsuser"]);
                client.QueryString.Add("password", ConfigurationSettings.AppSettings["smspassword"]);
                client.QueryString.Add("SMSText", msg);
                client.QueryString.Add("sender", ConfigurationSettings.AppSettings["smssender"]);
                client.QueryString.Add("GSM", telData);
                client.QueryString.Add("datacoding", "8");
                client.QueryString.Add("type", "longsms");
                string baseurl = "http://api.sms-delivery.com/api/sendsms/plain";


                Stream data = client.OpenRead(baseurl);
                StreamReader reader = new StreamReader(data); reader.ReadToEnd();

                data.Close();

                reader.Close();
            }
            catch { return false; }
            return true;
        }
        public string statusScan(int statusData)
        {
            string returnStatus = "";
            switch (statusData)
            {
                case 0:
                    returnStatus = "ตรงเวลาที่กำหนด";
                    break;
                case 1:
                    returnStatus = "สาย";
                    break;
                case 2:
                    returnStatus = "ออกก่อนเวลา";
                    break;
                case 3:
                    returnStatus = "ออกช้ากว่าที่กำหนด";
                    break;
                case 4:
                    returnStatus = "สแกนทั่วไป";
                    break;
            }
            return returnStatus;
        }
        public int convertDay(string days)
        {
            switch (days)
            {
                case "Monday": return 0;
                case "Tuesday": return 1;
                case "Wednesday": return 2;
                case "Thursday": return 3;
                case "Friday": return 4;
                case "Saturday": return 5;
                case "Sunday": return 6;
            }
            return 0;
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);
        public void GetMacAddress(string sName)
        {
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP);
                Int32 lhost = inet_addr("");
                Int64 macinfo = new Int64();
                Int32 len = 6;
                int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    if (userip == "127.0.0.1")
                        Response.Write("visited Localhost!");
                    else
                        Response.Write("the IP from" + userip + "" + "<br>");
                    return;
                }

                while (mac_src.Length < 12)
                {
                    mac_src = mac_src.Insert(0, "0");
                }

                string mac_dest = "";

                for (int i = 0; i < 11; i++)
                {
                    if (0 == (i % 2))
                    {
                        if (i == 10)
                        {
                            mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                        else
                        {
                            mac_dest = "-" + mac_dest.Insert(0, mac_src.Substring(i, 2));
                        }
                    }
                }

                Response.Write("welcome" + userip + "<br>" + ",the mac address is" + mac_dest + "."

                 + "<br>");
            }
            catch (Exception err)
            {
                Response.Write(err.Message);
            }


        }

    }
}