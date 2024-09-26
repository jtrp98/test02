using JabjaiMainClass;
using FingerprintPayment.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using JabjaiEntity.DB;
using MasterEntity;
using FingerprintPayment.App_Code;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for ReportCome2School
    /// </summary>
    public class ReportCome2Schoolteacher : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string sEntities = HttpContext.Current.Session["sEntities"].ToString(); //"JabJaiEntities";

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                                                                                        //string sEntities = "sbactestEntities";
                dynamic rss = new JObject();
                string strMode = fcommon.ReplaceInjection(context.Request["mode"]);
                DateTime dStart = DateTime.Today; //DateTime.Today.AddDays(-120);
                DateTime dEnd = DateTime.Today; //DateTime.Today.AddDays(-90);
                if (!string.IsNullOrEmpty(context.Request["day"]))
                {
                    string day = context.Request["day"].ToString();
                    dStart = DateTime.ParseExact(day, "dd/MM/yyyy", new CultureInfo("en-us"));
                    if (!string.IsNullOrEmpty(context.Request["end"]))
                    {
                        string end = context.Request["end"].ToString();
                        dEnd = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us"));
                    }
                    else
                    {
                        dEnd = dStart;
                    }
                }

                List<TLogEmpTimeScan> logUserIn = db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && (w.LogEmpDate >= dStart && w.LogEmpDate <= dEnd) && w.LogEmpType.Contains("0")).ToList();
                List<TLogEmpTimeScan> logUserOut = db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && (w.LogEmpDate >= dStart && w.LogEmpDate <= dEnd) && w.LogEmpType.Trim().Contains("1")).ToList();
                List<employess4day> _employess4day = new employess4day().getListEmployess(sEntities, dStart, dEnd, userData);
                var q_time = db.TTimes.Where(w => w.SchoolID == userData.CompanyID).ToList();

                if (!string.IsNullOrEmpty(context.Request["status"]))
                {
                    string status = context.Request["status"];
                    //logUserIn = logUserIn.Where(w => w.LogEmpScanStatus.Contains(status)).ToList();
                }
                if (!string.IsNullOrEmpty(context.Request["userid"]))
                {
                    int employessid = int.Parse(context.Request["userid"]);
                    _employess4day = _employess4day.Where(w => w.employessid == employessid).ToList();
                }
                switch (strMode)
                {
                    case "reportscome2school01teacher":
                        #region
                        rss = reportscome2school01teacher(logUserIn, _employess4day, context.Request["status"], q_time);
                        #endregion
                        break;
                    case "reportscome2school02teacher":
                        #region
                        rss = reportscome2school02teacher(logUserIn, logUserOut, _employess4day, context.Request["status"], q_time);
                        #endregion
                        break;
                }
                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        private dynamic reportscome2school01teacher(List<TLogEmpTimeScan> logUser, List<employess4day> _employess4day, string status, List<TTime> times)
        {
            dynamic rss = new JObject();
            var groupLog = (from a in logUser
                            select new
                            {
                                LogDate = a.LogEmpDate.Value,
                                employessid = a.sEmp.Value,
                                status_0 = a.LogEmpScanStatus.Trim() == "0" ? 1 : 0,
                                status_1 = a.LogEmpScanStatus.Trim() == "1" ? 1 : 0,
                                status_2 = a.LogEmpScanStatus.Trim() == "3" ? 1 : 0,
                                status_9 = a.LogEmpScanStatus.Trim() == "9" || a.LogEmpScanStatus.Trim() == "8" ? 1 : 0,
                                status_3 = a.LogEmpScanStatus.Trim() == "10" ? 1 : 0,
                                status_4 = a.LogEmpScanStatus.Trim() == "11" ? 1 : 0,
                                timescan = a.LogEmpTime
                            }).ToList();

            var LogTime = (from a in _employess4day
                           join b in groupLog on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.employessid }
                           equals new { JoinProperty1 = b.LogDate, JoinProperty2 = b.employessid }
                           into ab
                           from bb in ab.DefaultIfEmpty()

                           join b1 in times
                           on new { a.dayOfWeek, a.nTimeType } equals new { dayOfWeek = b1.nDay ?? 0, b1.nTimeType }
                           into ab1
                           from bb1 in ab1.DefaultIfEmpty()

                           select new Report02Employess
                           {
                               day = a.dayscan.ToString("dd/MM/yyyy"),
                               statusIn_0 = bb == null ? 0 : bb.status_0,
                               statusIn_1 = bb == null ? 0 : bb.status_1,
                               statusIn_3 = bb == null ? 0 : bb.status_3,
                               statusIn_4 = bb == null ? 0 : bb.status_4,
                               statusIn_2 = bb == null ? (bb1 != null && bb1.cDel == "1" && a.bHoliday == false ? 1 : 0) : bb.status_2,
                               statusIn_9 = bb == null ? (bb1 == null || bb1.cDel == "0" || a.bHoliday == true ? 1 : 0) : bb.status_9,
                           }).ToList();

            if (status == "0") LogTime = LogTime.Where(w => w.statusIn_0 == 1).ToList();
            else if (status == "1") LogTime = LogTime.Where(w => w.statusIn_1 == 1).ToList();
            else if (status == "3") LogTime = LogTime.Where(w => w.statusIn_2 == 1).ToList();
            else if (status == "10") LogTime = LogTime.Where(w => w.statusIn_3 == 1).ToList();
            else if (status == "11") LogTime = LogTime.Where(w => w.statusIn_4 == 1).ToList();

            rss = new JArray(from a in LogTime
                             group a by a.day into ag
                             select new JObject {
                                         new JProperty("day",ag.Key),
                                         new JProperty("employessnumber",ag.Where(w=>w.day ==ag.Key).Count()),
                                         new JProperty("status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.statusIn_0)),
                                         new JProperty("status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.statusIn_1)),
                                         new JProperty("status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.statusIn_2)),
                                         new JProperty("status_3",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.statusIn_3)),
                                         new JProperty("status_4",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.statusIn_4))
                                     });
            return rss;
        }
        private dynamic reportscome2school02teacher(List<TLogEmpTimeScan> logUserIn, List<TLogEmpTimeScan> logUserOut, List<employess4day> _employess4day, string status, List<TTime> times)
        {
            dynamic rss = new JObject();
            var groupLogIn = (from a in logUserIn
                              select new
                              {
                                  LogDate = a.LogEmpDate.Value,
                                  employessid = a.sEmp.Value,
                                  status_0 = a.LogEmpScanStatus.Trim() == "0" ? 1 : 0,
                                  status_1 = a.LogEmpScanStatus.Trim() == "1" ? 1 : 0,
                                  status_2 = a.LogEmpScanStatus.Trim() == "3" ? 1 : 0,
                                  status_9 = a.LogEmpScanStatus.Trim() == "9" || a.LogEmpScanStatus.Trim() == "8" ? 1 : 0,
                                  status_3 = a.LogEmpScanStatus.Trim() == "10" ? 1 : 0,
                                  status_4 = a.LogEmpScanStatus.Trim() == "11" ? 1 : 0,
                                  timescan = a.LogEmpTime
                              }).ToList();

            var groupLogOut = (from a in logUserOut
                               select new
                               {
                                   LogDate = a.LogEmpDate.Value,
                                   employessid = a.sEmp.Value,
                                   status_0 = (a.LogEmpScanStatus.Trim() == "0" || a.LogEmpScanStatus.Trim() == "3") ? 1 : 0,
                                   status_1 = a.LogEmpScanStatus.Trim() == "1" ? 1 : 0,
                                   status_2 = 0,
                                   status_9 = a.LogEmpScanStatus.Trim() == "9" || a.LogEmpScanStatus.Trim() == "8" ? 1 : 0,
                                   status_3 = a.LogEmpScanStatus.Trim() == "10" ? 1 : 0,
                                   status_4 = a.LogEmpScanStatus.Trim() == "11" ? 1 : 0,
                                   timescan = a.LogEmpTime
                               }).ToList();

            var LogTime = (from a in _employess4day
                           join b in groupLogIn on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.employessid }
                           equals new { JoinProperty1 = b.LogDate, JoinProperty2 = b.employessid }
                           into ab
                           from bb in ab.DefaultIfEmpty()

                           join b1 in times
                           on new { a.dayOfWeek, a.nTimeType } equals new { dayOfWeek = b1.nDay ?? 0, b1.nTimeType }
                           into ab1
                           from bb1 in ab1.DefaultIfEmpty()

                           join c in groupLogOut on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.employessid }
                           equals new { JoinProperty1 = c.LogDate, JoinProperty2 = c.employessid }
                           into ac
                           from cc in ac.DefaultIfEmpty()
                           select new Report02Employess
                           {
                               day = a.dayscan.ToString("dd/MM/yyyy"),
                               employessid = a.employessid,
                               employessname = a.employessname,
                               employesstype = a.employesstype,
                               statusIn_0 = bb == null ? 0 : bb.status_0,
                               statusIn_1 = bb == null ? 0 : bb.status_1,
                               statusIn_2 = bb == null ? (bb1 != null && bb1.cDel == "1" && a.bHoliday == false ? 1 : 0) : bb.status_2,
                               statusIn_9 = bb == null ? (bb1 == null || bb1.cDel == "0" || a.bHoliday == true ? 1 : 0) : bb.status_9,
                               statusIn_3 = bb == null ? 0 : bb.status_3,
                               statusIn_4 = bb == null ? 0 : bb.status_4,
                               timeinscan = bb != null && bb.timescan.HasValue ? bb.timescan.Value.ToString(@"hh\:mm\:ss") : "-",
                               statusOut_0 = cc == null ? 0 : cc.status_0,
                               statusOut_1 = cc == null ? 0 : cc.status_1,
                               statusOut_2 = cc == null ? (bb1 != null && bb1.cDel == "1" && a.bHoliday == false ? 1 : 0) : cc.status_2,
                               statusOut_3 = cc == null ? 0 : cc.status_3,
                               statusOut_4 = cc == null ? 0 : cc.status_4,
                               timeoutscan = cc != null && cc.timescan.HasValue ? cc.timescan.Value.ToString(@"hh\:mm\:ss") : "-"
                           }).ToList();

            if (status == "0") LogTime = LogTime.Where(w => w.statusIn_0 == 1).ToList();
            else if (status == "1") LogTime = LogTime.Where(w => w.statusIn_1 == 1).ToList();
            else if (status == "3") LogTime = LogTime.Where(w => w.statusIn_2 == 1).ToList();

            rss = new JArray(from a in LogTime
                             orderby a.employessname
                             orderby a.employesstype descending
                             select new JObject {
                                 new JProperty("day",a.day),
                                 new JProperty("employessname",a.employessname),
                                 new JProperty("employessid",a.employessid),
                                 new JProperty("employesstype",a.employesstype),
                                 new JProperty("timeinscan",a.timeinscan),
                                 new JProperty("statusIn_0",a.statusIn_0),
                                 new JProperty("statusIn_1",a.statusIn_1),
                                 new JProperty("statusIn_2",a.statusIn_2),
                                 new JProperty("statusIn_9",a.statusIn_9),
                                 new JProperty("statusIn_3",a.statusIn_3),
                                 new JProperty("statusIn_4",a.statusIn_4),
                                 new JProperty("timeoutscan",a.timeoutscan),
                                 new JProperty("statusOut_0",a.statusOut_0),
                                 new JProperty("statusOut_1",a.statusOut_1),
                                 new JProperty("statusOut_2",a.statusOut_2),
                                 new JProperty("statusOut_3",a.statusOut_3),
                                 new JProperty("statusOut_4",a.statusOut_4)
                             });
            return rss;
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