using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;

namespace FingerprintPayment
{
    public class BehaviorGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class Behavior_Setting : BehaviorGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                OpenData();
            }
        }

        private void OpenData()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var tBehaviorSetting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == schoolID).FirstOrDefault();
                score.Text = tBehaviorSetting.MaxScore.Value.ToString();

                type.Text = setType(tBehaviorSetting.Type);

                late.Text = (tBehaviorSetting.Late ?? 0).ToString();
                absence.Text = (tBehaviorSetting.Absence ?? 0).ToString();
                errand.Text = (tBehaviorSetting.Errand ?? 0).ToString(); // ###
                sick.Text = (tBehaviorSetting.Sick ?? 0).ToString(); // ###
                uncheckname.Text = (tBehaviorSetting.UncheckName ?? 0).ToString(); // ###
                begin_late.Text = (tBehaviorSetting.BeginLate ?? 0).ToString();
                begin_absence.Text = (tBehaviorSetting.BeginAbsence ?? 0).ToString();
                begin_errand.Text = (tBehaviorSetting.BeginErrand ?? 0).ToString(); // ###
                begin_sick.Text = (tBehaviorSetting.BeginSick ?? 0).ToString(); // ###
                begin_uncheckname.Text = (tBehaviorSetting.BeginUncheckName ?? 0).ToString(); // ###
                time_start_cut_point.Text = string.IsNullOrEmpty(tBehaviorSetting.TimeStartCutPoint) ? "-" : tBehaviorSetting.TimeStartCutPoint + " น."; // ###
                absence_half_point.Checked = (tBehaviorSetting.AbsenceHalfPoint == "1"); // ###
                show_minus_sign.Text = tBehaviorSetting.ShowMinusSign.Value == 1 ? "แสดง" : "ไม่แสดง";
                show_behavior_alert.Text = (tBehaviorSetting.Alert ?? 0).ToString();
                late_setting_point.Checked = tBehaviorSetting.LateSettingTime ?? false;

                if (late_setting_point.Checked)
                {
                    foreach (var data in dbschool.TBehaviorTimeSettings.Where(w => w.cDel == false && w.SchoolID == schoolID))
                    {
                        LateSettingTime.Text += string.Format(@"<div class=""row"">
                        <div class=""col-md-3""></div>
                        <div class=""col-md-6"">
                            <div class=""col-md-5"">
                                <label class=""col-md-1 col-form-label"">{0}</label>
                            </div>
                            <label class=""col-md-1 col-form-label"">ถึง</label>
                            <div class=""col-md-5"">
                                <label class=""col-md-1 col-form-label"">{1}</label>
                            </div>
                            <label class=""col-md-1 col-form-label"">น.</label>
                        </div>
                        <div class=""col-md-2"">
                            <label class=""col-md-1 col-form-label"">{2}</label>
                        </div>
                        <div class=""col-md-1"" style=""padding-left: 0px; padding-right: 0px;"" >
                            <div class=""form-group"">
                                <label class=""col-form-label"" style=""font-size: 20px;"">คะแนน</label>
                            </div>
                        </div>
                </div>", data.TimeStatr, data.TimeEnd, data.Cut_Point);
                    }

                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Behavior_data getdata()
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = (from a in _db.TBehaviorSettings.Where(w => w.SchoolID == schoolID).ToList()
                             select new Behavior_data
                             {
                                 behavior_type = a.Type,
                                 behavior_type_name = setType(a.Type),
                                 behavior_score = a.MaxScore,
                                 behavior_late = a.Late ?? 0,
                                 behavior_absence = a.Absence ?? 0,
                                 behavior_errand = a.Errand ?? 0, // ###
                                 behavior_sick = a.Sick ?? 0, // ###
                                 behavior_uncheckname = a.UncheckName ?? 0, // ###
                                 behavior_begin_late = a.BeginLate ?? 0,
                                 behavior_begin_absence = a.BeginAbsence ?? 0,
                                 behavior_begin_errand = a.BeginErrand ?? 0, // ###
                                 behavior_begin_sick = a.BeginSick ?? 0, // ###
                                 behavior_begin_uncheckname = a.BeginUncheckName ?? 0, // ###
                                 behavior_time_start_cut_point = string.IsNullOrEmpty(a.TimeStartCutPoint) ? "11:30" : a.TimeStartCutPoint, // ###
                                 behavior_absence_half_point = a.AbsenceHalfPoint, // ###
                                 behavior_show_minus_sign = a.ShowMinusSign ?? 1,
                                 behavior_show_minus_sign_name = a.ShowMinusSign == 1 ? "แสดง" : "ไม่แสดง",
                                 behavior_show_alert = a.Alert ?? 0,
                                 behavior_Late_SettingTime = a.LateSettingTime,
                                 lateSettingTimes = a.LateSettingTime ?? false ?
                                 (from a1 in _db.TBehaviorTimeSettings.Where(w => w.SchoolID == schoolID && w.cDel == false)
                                  select new Behavior_data.LateSettingTime
                                  {
                                      BehaviorTimeSettingID = a1.BehaviorTimeSettingID,
                                      cut_point = a1.Cut_Point ?? 0,
                                      TimeEnd = a1.TimeEnd,
                                      TimeStatr = a1.TimeStatr
                                  }).ToList() : new List<Behavior_data.LateSettingTime>()

                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        private static string setType(int? Type)
        {
            switch (Type)
            {
                case 1:
                    return "รายเทอม";
                case 2:
                    return "รายปีการศึกษา";
                case 3:
                    return "ตามช่วงชั้น";
                default:
                    return "";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Behavior_data updatedata(Behavior_data behavior_data)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {

                    var q = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
                    var serializer = new JavaScriptSerializer();

                    serializer.RegisterConverters(new[] { new DateTimeConverter() });
                    string json = serializer.Serialize(q);

                    TB_HistorySetting _HistorySetting = new TB_HistorySetting()
                    {
                        Fd_SettingData = json,
                        Fd_UpdatedDate = DateTime.Now,
                        Fd_FunctionName = "Behavior Time Settings",
                        Fd_HistoryID = Guid.NewGuid(),
                        Fd_SchoolID = userData.CompanyID,
                        Fd_UpdatedBy = userData.UserID
                    };

                    dbschool.TB_HistorySetting.Add(_HistorySetting);

                    q.Type = behavior_data.behavior_type;
                    q.MaxScore = behavior_data.behavior_score;
                    q.Late = behavior_data.behavior_late;
                    q.Absence = behavior_data.behavior_absence;
                    q.Errand = behavior_data.behavior_errand; // ###
                    q.Sick = behavior_data.behavior_sick; // ###
                    q.UncheckName = behavior_data.behavior_uncheckname; // ###
                    q.BeginLate = behavior_data.behavior_begin_late;
                    q.BeginAbsence = behavior_data.behavior_begin_absence;
                    q.BeginErrand = behavior_data.behavior_begin_errand; // ###
                    q.BeginSick = behavior_data.behavior_begin_sick; // ###
                    q.BeginUncheckName = behavior_data.behavior_begin_uncheckname; // ###
                    q.TimeStartCutPoint = behavior_data.behavior_time_start_cut_point; // ###
                    q.AbsenceHalfPoint = behavior_data.behavior_absence_half_point; // ###
                    q.ShowMinusSign = behavior_data.behavior_show_minus_sign;
                    q.Alert = behavior_data.behavior_show_alert;
                    q.UpdatedDate = DateTime.Now;
                    q.UpdatedBy = userData.UserID;
                    q.LateSettingTime = behavior_data.behavior_Late_SettingTime;

                    foreach (var behaviorTime in dbschool.TBehaviorTimeSettings.Where(w => w.cDel == false && w.SchoolID == userData.CompanyID))
                    {
                        behaviorTime.cDel = true;
                        behaviorTime.UpdatedBy = userData.UserID;
                        behaviorTime.UpdatedDate = DateTime.Now;
                    }

                    dbschool.SaveChanges();

                    List<Behavior_data.LateSettingTime> lateSettingTimes = new List<Behavior_data.LateSettingTime>();

                    if (behavior_data.behavior_Late_SettingTime ?? false)
                    {
                        if (behavior_data.lateSettingTimes != null)
                        {
                            foreach (var settingTime in behavior_data.lateSettingTimes)
                            {
                                var behaviorTimeSetting = dbschool.TBehaviorTimeSettings.FirstOrDefault(f => f.BehaviorTimeSettingID == settingTime.BehaviorTimeSettingID);

                                if (behaviorTimeSetting == null) behaviorTimeSetting = new TBehaviorTimeSetting();

                                behaviorTimeSetting.TimeEnd = settingTime.TimeEnd;
                                behaviorTimeSetting.TimeStatr = settingTime.TimeStatr;
                                behaviorTimeSetting.Cut_Point = settingTime.cut_point;
                                behaviorTimeSetting.cDel = false;

                                if (behaviorTimeSetting.BehaviorTimeSettingID == 0)
                                {
                                    behaviorTimeSetting.CreatedBy = userData.UserID;
                                    behaviorTimeSetting.CreatedDate = DateTime.Now;
                                    behaviorTimeSetting.SchoolID = userData.CompanyID;
                                    dbschool.TBehaviorTimeSettings.Add(behaviorTimeSetting);
                                }
                                else
                                {
                                    behaviorTimeSetting.UpdatedBy = userData.UserID;
                                    behaviorTimeSetting.UpdatedDate = DateTime.Now;
                                }
                            }

                            dbschool.SaveChanges();

                            lateSettingTimes = (from a1 in dbschool.TBehaviorTimeSettings.Where(w => w.SchoolID == schoolID && w.cDel == false)
                                                select new Behavior_data.LateSettingTime
                                                {
                                                    BehaviorTimeSettingID = a1.BehaviorTimeSettingID,
                                                    cut_point = a1.Cut_Point ?? 0,
                                                    TimeEnd = a1.TimeEnd,
                                                    TimeStatr = a1.TimeStatr,
                                                }).ToList();
                        }

                    }


                    database.InsertLog(userData.UserID.ToString(),
                           "ทำการตั้งค่าตัดคะแนนความประพฤติอัตโนมัติ ",
                           userData.Entities,
                           HttpContext.Current.Request, 4, 3, 0);

                    return (from a in dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                //where a.SchoolID == userData.CompanyID
                            select new Behavior_data
                            {
                                behavior_type = a.Type,
                                behavior_type_name = setType(a.Type),
                                behavior_score = a.MaxScore,
                                behavior_late = a.Late,
                                behavior_absence = a.Absence ?? 0,
                                behavior_errand = a.Errand ?? 0, // ###
                                behavior_sick = a.Sick ?? 0, // ###
                                behavior_uncheckname = a.UncheckName ?? 0, // ###
                                behavior_begin_late = a.BeginLate ?? 0,
                                behavior_begin_absence = a.BeginAbsence ?? 0,
                                behavior_begin_errand = a.BeginErrand ?? 0, // ###
                                behavior_begin_sick = a.BeginSick ?? 0, // ###
                                behavior_begin_uncheckname = a.BeginUncheckName ?? 0, // ###
                                behavior_time_start_cut_point = string.IsNullOrEmpty(a.TimeStartCutPoint) ? "11:30" : a.TimeStartCutPoint, // ###
                                behavior_absence_half_point = a.AbsenceHalfPoint, // ###
                                behavior_show_minus_sign = a.ShowMinusSign ?? 1,
                                behavior_show_minus_sign_name = a.ShowMinusSign == 1 ? "แสดง" : "ไม่แสดง",
                                behavior_show_alert = a.Alert ?? 0,
                                behavior_Late_SettingTime = a.LateSettingTime,
                                lateSettingTimes = lateSettingTimes
                            }).FirstOrDefault();
                }
            }
        }



        public class Behavior_data
        {
            public decimal? behavior_score { get; set; }
            public int? behavior_type { get; set; }
            public string behavior_type_name { get; set; }
            public decimal? behavior_late { get; set; }
            public decimal? behavior_absence { get; set; }
            public decimal? behavior_errand { get; set; }
            public decimal? behavior_sick { get; set; }
            public decimal? behavior_uncheckname { get; set; }
            public int? behavior_begin_late { get; set; }
            public int? behavior_begin_absence { get; set; }
            public int? behavior_begin_errand { get; set; }
            public int? behavior_begin_sick { get; set; }
            public int? behavior_begin_uncheckname { get; set; }
            public string behavior_time_start_cut_point { get; set; }
            public string behavior_absence_half_point { get; set; }
            public int? behavior_show_minus_sign { get; set; }
            public int? behavior_show_alert { get; set; }
            public string behavior_show_minus_sign_name { get; set; }
            public List<LateSettingTime> lateSettingTimes { get; set; }
            public bool? behavior_Late_SettingTime { get; set; } = false;

            public class LateSettingTime
            {
                public string TimeStatr { get; set; }
                public string TimeEnd { get; set; }
                public int cut_point { get; set; }
                public int BehaviorTimeSettingID { get; set; }
            }
        }
    }
}