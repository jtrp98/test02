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
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class learnscaning : System.Web.UI.Page
    {
        public string ipRoom;

        protected void Page_Load(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST
            Console.WriteLine(hostName);
            // Get the IP
            ipRoom = Dns.GetHostByName(hostName).AddressList[0].ToString();
            txtRoom.Text = ipRoom;

            DataTable dtCurrentClass = fcommon.LinqToDataTable(_db.TClasses.Where(w => w.sClassIP == ipRoom));
            if (dtCurrentClass != null && dtCurrentClass.Rows.Count > 0)
            {
                txtRoomName.Text = dtCurrentClass.Rows[0]["sClass"].ToString();
            }
            else
            {
                txtRoomName.Text = "ไม่พบข้อมูลตรวจสอบ ip";
            }

            btnSavepage1.Click += new EventHandler(btnSavepage1_Click);
        }

        public void focusTxtBarCode()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "postbackReady", "postbackReady();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "txtsBarCode", "$('input[id*=txtsBarCode]').select();", true);
        }

        void btnSavepage1_Click(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            /*  if (string.IsNullOrEmpty(txtCheckFinger.Text) || txtCheckFinger.Text == "1")
              {
                  var stopwatch = Stopwatch.StartNew();

                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>กรุณาสแกนลายนิ้วมือเพื่อทำรายการ</span>'); return false;});", true);
              }
              else
              {*/
            ViewState["sUserID"] = int.Parse(txtCheckFinger.Text);
            // txtsBarCode.Focus();
            // focusTxtBarCode();
            DateTime dateTime = DateTime.Now;
            string thisDay = dateTime.DayOfWeek.ToString();
            int days = convertDay(thisDay);

            DataTable dtUser = fcommon.LinqToDataTable(_db.TUsers.Where(w => w.sID == 2));
            //if (dtUser != null)
            //{
            //    if (dtUser.Rows[0]["cType"].ToString() == "0")
            //    {
            //        DataTable dtStudentLevel = fcommon.LinqToDataTable(_db.TStudentLevels.Where(w => w.sID == 2));
            //        if (dtStudentLevel != null || dtStudenTLevels.Rows.Count > 0)
            //        {
            //            int currentSubLV2 = Int32.Parse(dtStudenTLevels.Rows[0]["nTermSubLevel2"].ToString());

            //            DataTable dtClass = fcommon.LinqToDataTable(_db.TClass.Where(w => w.sClassIP == ipRoom));

            //            if (dtClass != null)
            //            {
            //                string classID = dtClass.Rows[0]["sClassID"].ToString();
            //                TimeSpan timeCheck = dateTime.TimeOfDay;

            //                DataTable dtTime = fcommon.LinqToDataTable(
            //                    _db.TSchedules.Where(w => w.nPlaneDay == days &&
            //                      timeCheck >= w.dTimeStart_IN &&
            //                        timeCheck <= w.dTimeEnd_OUT
            //                    && w.sClassID == classID && w.TermSublv == currentSubLV2 && w.cDel == null));
            //                if (dtTime != null)
            //                {
            //                    int sSchedultID = Int32.Parse(dtTime.Rows[0]["sScheduleID"].ToString());

            //                    DataTable DTTimeScan = fcommon.LinqToDataTable(_db.TLogLearnTimeScan.
            //                        Where(w => w.LogLearnnDay == days && w.LogLearnType == "0" && w.sID == 2 && w.sScheduleID == sSchedultID));
            //                    if (DTTimeScan == null)
            //                    {
            //                        DateTime dateDB = DateTime.Parse(dtTime.Rows[0]["dTimeStart_IN"].ToString());
            //                        DateTime dateDBOut = DateTime.Parse(dtTime.Rows[0]["dTimeStart_OUT"].ToString());

            //                        TLogLearnTimeScan tlogs = new TLogLearnTimeScan();
            //                        //tlogs.nLogLearnScanID = _db.TLogLearnTimeScan.Count() + 1;
            //                        tlogs.LogLearnTime = dateTime.TimeOfDay;
            //                        //tlogs.sID = 2;
            //                        tlogs.LogLearnType = "0";
            //                        tlogs.LogLearnnDay = days;
            //                        tlogs.LogLearnDate = dateTime.Date;
            //                        tlogs.sScheduleID = sSchedultID;
            //                        if (TimeSpan.Compare(dateTime.TimeOfDay, dateDB.TimeOfDay) == 0
            //                            || TimeSpan.Compare(dateTime.TimeOfDay, dateDB.TimeOfDay) == 1 &&
            //                            TimeSpan.Compare(dateTime.TimeOfDay, dateDBOut.TimeOfDay) == -1
            //                            || TimeSpan.Compare(dateTime.TimeOfDay, dateDBOut.TimeOfDay) == 0)
            //                        {
            //                            tlogs.LogLearnScanStatus = "0";
            //                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','0'); return false;});", true);
            //                        }
            //                        else
            //                        {
            //                            tlogs.LogLearnScanStatus = "1";
            //                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','1'); return false;});", true);
            //                        }
            //                        _db.TLogLearnTimeScan.Add(tlogs);
            //                        _db.SaveChanges();
            //                    }
            //                    else
            //                    {
            //                        DateTime dateDB = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_IN"].ToString());
            //                        DateTime dateDBOut = DateTime.Parse(dtTime.Rows[0]["dTimeEnd_OUT"].ToString());

            //                        DataTable DTTimeScanSecond = fcommon.LinqToDataTable(_db.TLogLearnTimeScan.
            //                       Where(w => w.LogLearnnDay == days && w.LogLearnType == "1" && w.nUserID == 2 && w.sScheduleID == sSchedultID));

            //                        if (DTTimeScanSecond == null)
            //                        {
            //                            TLogLearnTimeScan tlogs = new TLogLearnTimeScan();
            //                            //tlogs.nLogLearnScanID = _db.TLogLearnTimeScan.Count() + 1;
            //                            tlogs.LogLearnTime = dateTime.TimeOfDay;
            //                            //tlogs.sID = 2;
            //                            tlogs.LogLearnType = "1";
            //                            tlogs.LogLearnnDay = days;
            //                            tlogs.LogLearnDate = dateTime.Date;
            //                            tlogs.sScheduleID = sSchedultID;

            //                            if (TimeSpan.Compare(dateTime.TimeOfDay, dateDB.TimeOfDay) == 0
            //                                || TimeSpan.Compare(dateTime.TimeOfDay, dateDB.TimeOfDay) == 1 &&
            //                                TimeSpan.Compare(dateTime.TimeOfDay, dateDBOut.TimeOfDay) == -1
            //                                || TimeSpan.Compare(dateTime.TimeOfDay, dateDBOut.TimeOfDay) == 0)
            //                            {
            //                                tlogs.LogLearnScanStatus = "0";
            //                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','0'); return false;});", true);
            //                            }
            //                            else if (TimeSpan.Compare(dateTime.TimeOfDay, dateDB.TimeOfDay) == -1)
            //                            {
            //                                tlogs.LogLearnScanStatus = "2";
            //                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','2'); return false;});", true);
            //                            }
            //                            else
            //                            {
            //                                tlogs.LogLearnScanStatus = "3";
            //                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','3'); return false;});", true);
            //                            }
            //                            _db.TLogLearnTimeScan.Add(tlogs);
            //                            _db.SaveChanges();
            //                        }
            //                        else
            //                        {

            //                            TLogLearnTimeScan tlogs = new TLogLearnTimeScan();
            //                            //tlogs.nLogLearnScanID = _db.TLogLearnTimeScan.Count() + 1;
            //                            tlogs.LogLearnTime = dateTime.TimeOfDay;
            //                            //tlogs.sID = 2;
            //                            tlogs.LogLearnType = "1";
            //                            tlogs.LogLearnnDay = days;
            //                            tlogs.LogLearnDate = dateTime.Date;
            //                            tlogs.LogLearnScanStatus = "4";
            //                            tlogs.sScheduleID = sSchedultID;
            //                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){hideScan('" + dtUser.Rows[0]["sName"] + " " + dtUser.Rows[0]["sLastname"] + "','4'); return false;});", true);
            //                            _db.TLogLearnTimeScan.Add(tlogs);
            //                            _db.SaveChanges();
            //                        }
            //                    }

            //                }
            //                else
            //                {
            //                    modalMessage("กรุณาตรวจสอบชั่วโมงเรียน");
            //                }

            //            }
            //            else
            //            {
            //                modalMessage("ไม่พบข้อมูลห้องเรียนกรุณาตรวจสอบ IP");
            //            }
            //        }
            //        else
            //        {
            //            modalMessage("ติดต่อฝ่ายทะเบียน");
            //        }
            //    }
            //    else
            //    {
            //        modalMessage("ไม่พบข้อมูล");
            //    }

            //}
            //else
            //{
            //}
        }

        public void modalMessage(string msg)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scanFinger", "$(function(){j_infosell('<span>" + msg + "</span>'); return false;});", true);
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