using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Slip
{
    public partial class AllowEnterClass : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string sid = Request.QueryString["sid"];
                string term = Request.QueryString["term"];
                string fullname = Request.QueryString["name"];
                string code = Request.QueryString["code"];

                string numLate = Request.QueryString["late"];
                if (numLate == "0") numLate = "-";
                string numAbsent = Request.QueryString["absent"];
                if (numAbsent == "0") numAbsent = "-";
                string numBusinessLeave = Request.QueryString["businessLeave"];
                if (numBusinessLeave == "0") numBusinessLeave = "-";
                string numSickLeave = Request.QueryString["sickLeave"];
                if (numSickLeave == "0") numSickLeave = "-";

                //var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                //var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                if (!IsPostBack)
                {
                    var dateNow = DateTime.Now;

                    var dateTime = DateTime.Now.TimeOfDay;

                    //var nameSplit = fullname.Split(' ');
                    //var name = nameSplit[0];

                    //var getnTerm = (from y in _db.TYears.Where(w => w.SchoolID == userData.CompanyID)
                    //                join t in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on y.nYear equals t.nYear
                    //                where dateNow > t.dStart && dateNow < t.dEnd
                    //                select t).FirstOrDefault();
                    //var sl = new StudentLogic(_db);
                    //var getnTerm = sl.GetTermDATA(dateNow, userData);
                    //var codeTerm = getnTerm.nTerm.ToString();

                    var f_term = _db.TTerms
                      .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == term)
                      .FirstOrDefault();

                    var user = (from sv in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                                where /*sv.sName == name &&*/ sv.sID + "" == sid
                                    && sv.nTerm == term
                                select sv).FirstOrDefault();

                    var _dateHistoryStart = f_term.dStart;
                    var _dateHistoryEnd = f_term.dEnd;

                    var f_setting = _db.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
                    //int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);
                    //int ShowMinusSign1 = f_setting == null ? 1 : (f_setting.ShowMinusSign == 0 ? 1 : -1);


                    //var f_term = new TTerm();
                    //if (!string.IsNullOrEmpty(codeTerm))
                    //{
                    //    f_term = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm.Trim() == codeTerm);
                    //}
                    //else
                    //{
                    //    f_term = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart <= _dateHistoryStart && f.dEnd >= _dateHistoryStart || f.dStart <= _dateHistoryEnd && f.dEnd >= _dateHistoryEnd);
                    //}
                    //if (f_term != null)
                    //{

                    //    //if (f_setting.Type == 1)
                    //    //{
                    //    //    _dateHistoryStart = f_term.dStart;
                    //    //    _dateHistoryEnd = _dateHistoryEnd > f_term.dEnd ? f_term.dEnd : _dateHistoryEnd;

                    //    //}
                    //    //else
                    //    //{
                    //    //    var q_term = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nYear == f_term.nYear).ToList();
                    //    //    _dateHistoryStart = q_term.Min(m => m.dStart);
                    //    //    //_dateHistoryEnd = q_term.Max(m => m.dEnd);
                    //    //    _dateHistoryEnd = q_term.Max(m => m.dEnd) >= _dateHistoryEnd ? _dateHistoryEnd : f_term.dEnd;
                    //    //}
                    //}

                    var history = _db.TBehaviorHistories
                        .Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                        .Where(w => w.dAdd >= _dateHistoryStart && w.dAdd <= _dateHistoryEnd && w.dCanCel == null && w.Type == "1" && w.StudentId == user.sID)
                        .ToList();

                    var autoId = _db.TBehaviors.Where(w => w.SchoolID == userData.CompanyID && w.nAutoType.HasValue).Select(s => s.BehaviorId).ToList();

                    var behavAuto = GetbehaviorAuto(history.Where(w => autoId.Contains(w.BehaviorId ?? 0)).ToList(), user.sID, f_setting);
                    var behavManual = GetbehaviorManual(history, user.sID, f_setting);
                    var behavSum = behavAuto + behavManual;

                    txtBehavAuto.Text = behavAuto.ToString();
                    txtBehavManual.Text = behavManual.ToString();
                    txtBehavSum.Text = behavSum.ToString();

                    txtDate.Text = dateNow.Day.ToString();
                    txtMonth.Text = MonthTransform(dateNow.Month.ToString());
                    txtYear.Text = (dateNow.Year + 543).ToString();
                    txtHour.Text = dateNow.Hour.ToString();
                    txtMinute.Text = dateNow.Minute.ToString();

                    txtName.Text = user.sName;
                    txtLastname.Text = user.sLastname;

                    txtCode.Text = code;
                    txtClassroom.Text = user.SubLevel + "/" + user.nTSubLevel2;

                    StudentLogTime studentLogTime = new StudentLogTime(_db, userData);


                    var logTime = studentLogTime.GetLog4Student(user.sID, _dateHistoryStart.Value, _dateHistoryEnd.Value);

                    var f_log = (from g in logTime
                                 group g by g.student_Id into gb
                                 select new
                                 {
                                     Status_0 = gb.Count(c => c.LogStatus == "0"),
                                     Status_1 = gb.Count(c => c.LogStatus == "1"),//สาย
                                     Status_2 = gb.Count(c => c.LogStatus == "3"),//ขาด
                                     Status_3 = gb.Count(c => c.LogStatus == "12"),
                                     Status_4 = gb.Count(c => c.LogStatus == "10"),//ลากิจ
                                     Status_5 = gb.Count(c => c.LogStatus == "11"),//ลาป่วย
                                     Status_6 = gb.Count(c => c.LogStatus == "99"),
                                 }).FirstOrDefault();

                    txtLate.Text = f_log.Status_1 + "";
                    txtAbsent.Text = f_log.Status_2 + "";
                    txtBusinessLeave.Text = f_log.Status_4 + "";
                    txtSickLeave.Text = f_log.Status_5 + "";
                }
            }
        }

        //duplicate code form FingerprintPayment.Report.Functions.Reports_03

        private static decimal GetbehaviorAuto(List<TBehaviorHistory> histories, int StudentId, TBehaviorSetting f_setting)
        {
            var f_data = histories.Where(f => f.StudentId == StudentId)
                .Sum(f => f.Score);
            //var setting = maxBehav[0].MaxScore;
            //return f_data == null ? 100 : f_data.ResidualScore ?? 100;
            decimal ResidualScore = f_setting == null ? 0 : f_setting.MaxScore.Value;
            if (f_data != null)
            {
                ResidualScore = f_data.Value;
            }
            return ResidualScore;
        }

        private static decimal GetbehaviorManual(List<TBehaviorHistory> histories, int StudentId, TBehaviorSetting f_setting)
        {
            var f_data = histories.Where(f => f.StudentId == StudentId)
                .Where(f => f.BehaviorId < 9000)
                .Sum(f => f.Score);
            //var setting = maxBehav[0].MaxScore;
            //return f_data == null ? 100 : f_data.ResidualScore ?? 100;
            decimal ResidualScore = f_setting == null ? 0 : f_setting.MaxScore.Value;
            if (f_data != null)
            {
                ResidualScore = f_data.Value;
            }
            return ResidualScore;
        }

        public string MonthTransform(string numMounth)
        {
            if (numMounth == "1")
                numMounth = "มกราคม";
            else if (numMounth == "2")
                numMounth = "กุมภาพันธ์";
            else if (numMounth == "3")
                numMounth = "มีนาคม";
            else if (numMounth == "4")
                numMounth = "เมษายน";
            else if (numMounth == "5")
                numMounth = "พฤษภาคม";
            else if (numMounth == "6")
                numMounth = "มิถุนายน";
            else if (numMounth == "7")
                numMounth = "กรกฎาคม";
            else if (numMounth == "8")
                numMounth = "สิงหาคม";
            else if (numMounth == "9")
                numMounth = "กันยายน";
            else if (numMounth == "10")
                numMounth = "ตุลาคม";
            else if (numMounth == "11")
                numMounth = "พฤศจิกายน";
            else if (numMounth == "12")
                numMounth = "ธันวาคม";

            return numMounth;
        }

    }
}