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
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class empscanning : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
            if (!IsPostBack)
            {
                SiteMapPath siteMapPath = (SiteMapPath)this.Master.FindControl("SiteMapPath1");
                siteMapPath.Visible = false;
            }
        }

        public void focusTxtBarCode()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
        }

        void btnSavepage1_Click(object sender, EventArgs e)
        {


            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            /*  if (string.IsNullOrEmpty(txtCheckFinger.Text) || txtCheckFinger.Text == "1")
              {
                  var stopwatch = Stopwatch.StartNew();

                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>กรุณาสแกนลายนิ้วมือเพื่อทำรายการ</span>'); return false;});", true);
              }
              else
              {*/
            int? sID = int.Parse(txtCheckFinger.Text);
            ViewState["sUserID"] = int.Parse(txtCheckFinger.Text);
            // txtsBarCode.Focus();
            //  focusTxtBarCode();
            //int emp = _db.TEmployees.Where(w => w.sEmp == int.Parse(txtCheckFinger.Text) && w.cDel == null).Count();
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
                int dtHolidayYear = _db.THolidays.Where(w => w.dHolidayStart >= today && w.dHolidayEnd <= today && w.sHolidayType == "0" && w.cDel == null).Count();

                if (dtHolidayYear == 0)
                {
                    DateTime dateTime = DateTime.Now;
                    string thisDay = dateTime.DayOfWeek.ToString();
                    int days = convertDay(thisDay);
                    DataTable dtTime = fcommon.LinqToDataTable(_db.TTimes.Where(w => w.nDay == days && w.cDel == "1" && w.nTimeType == currentTimetype));
                    if (_db.TTimes.Where(w => w.nDay == days && w.cDel == "0").Count() > 0)
                    {
                        var stopwatch = Stopwatch.StartNew();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>วันที่ไม่ได้ถูกตั้งค่าให้บันทึกข้อมูล</span>'); return false;});", true);
                    }
                    else
                    {
                        DataTable DTTimeScan = fcommon.LinqToDataTable(_db.TLogEmpTimeScans.Where(w => w.LogEmpnDay == days && w.sEmp == sID));

                        TLogEmpTimeScan tlogs = new TLogEmpTimeScan();
                        tlogs.nLogEmpScanID = _db.TLogEmpTimeScans.Count() + 1;
                        tlogs.LogEmpTime = dateTime.TimeOfDay;
                        tlogs.sEmp = sID;
                        tlogs.LogEmpnDay = days;
                        tlogs.nYear = currentYear;
                        tlogs.LogEmpDate = dateTime.Date;

                        string sName = dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"];
                        string LogEmpType = "", LogEmpScanStatus = "";
                        CheackStatus(DTTimeScan, dtTime, ref  LogEmpType, ref  LogEmpScanStatus, sName);
                        tlogs.LogEmpType = LogEmpType;
                        tlogs.LogEmpScanStatus = LogEmpScanStatus;

                        if (tlogs.LogEmpScanStatus != "-1")
                        {
                            _db.TLogEmpTimeScans.Add(tlogs);
                            _db.SaveChanges();
                        }
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(identData))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>ไม่พบข้อมูล</span>'); return false;});", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>กรอกรหัสสี่ตัว</span>'); return false;});", true);
                    }
                }

            }
        }

        private void CheackStatus(DataTable DTTimeScan, DataTable dtTime, ref string LogEmpType, ref string LogEmpScanStatus, string sName)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
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
                if (!_CheckDTTimeScan) _CheckDTTimeScan = DTTimeScan.Select("LogEmpType = '0' ").Length == 0;
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
                    else if (dateTime.TimeOfDay <= dateDB_eIN.TimeOfDay)
                    {
                        LogEmpType = "0";
                        LogEmpScanStatus = "3";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','1',''); return false;});", true);
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
                    string sTime = DateTime.Parse(DTTimeScan.Select("LogEmpType = '0' ")[0]["LogEmpTime"] + "").ToShortTimeString();
                    LogEmpType = "1";
                    LogEmpScanStatus = "-1";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','5','" + sTime + "'); return false;});", true);
                }
            }
            else
            {
                if (!_CheckDTTimeScan) _CheckDTTimeScan = DTTimeScan.Select("LogEmpType = '1' ").Length == 0;
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
                    string sTime = DateTime.Parse(DTTimeScan.Select("LogEmpType = '1' ")[0]["LogEmpTime"] + "").ToShortTimeString();
                    LogEmpType = "1";
                    LogEmpScanStatus = "-1";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + sName + "','6','" + sTime + "'); return false;});", true);
                }
            }
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

    }
}