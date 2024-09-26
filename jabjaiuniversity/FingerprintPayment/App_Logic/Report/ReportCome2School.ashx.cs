using JabjaiMainClass;
using JabjaiEntity.DB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Globalization;
using MasterEntity;
using FingerprintPayment.App_Code;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for ReportCome2School
    /// </summary>
    public class ReportCome2School : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            dynamic rss = new JObject();
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (HttpContext.Current.Session["sEntities"] == null)
            {
                rss = new JObject()
                {
                    new JProperty("status", "Session Time Out")
                };
            }
            else
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {

                    //string sEntities = "JabJaiEntities";
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
                    Dictionary<DateTime, int> ListDay = new Dictionary<DateTime, int>();
                    for (int i = 0; dStart <= dEnd.AddDays(-i); i++)
                    {
                        int nDay = (int)dEnd.AddDays(-i).DayOfWeek;
                        DateTime Day = dEnd.AddDays(-i);
                        ListDay.Add(Day, nDay);
                    }

                    List<TLogUserTimeScan> logScanInUser = db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && (w.LogDate >= dStart && w.LogDate <= dEnd) && w.LogType.Contains("0")).AsQueryable().ToList();
                    List<TLogUserTimeScan> logScanOutUser = db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && (w.LogDate >= dStart && w.LogDate <= dEnd )&& w.LogType.Contains("1")).AsQueryable().ToList();
                    List<Student> lStudent = new Student().getStudent(sEntities);
                    List<student4day> _student4day = new student4day().getListStudent(sEntities, dStart, dEnd);

                    if (!string.IsNullOrEmpty(context.Request["level2"]))
                    {
                        int level2id = int.Parse(context.Request["level2"]);

                        _student4day = _student4day.Where(w => w.studentlevel2id == level2id).ToList();
                    }
                    else if (!string.IsNullOrEmpty(context.Request["level"]))
                    {
                        int levelid = int.Parse(context.Request["level"]);
                        _student4day = _student4day.Where(w => w.studentlevelid == levelid).ToList();
                    }

                    if (!string.IsNullOrEmpty(context.Request["status"]))
                    {
                        string status = context.Request["status"];
                        logScanInUser = logScanInUser.Where(w => w.LogScanStatus.Contains(status)).ToList();
                    }
                    if (!string.IsNullOrEmpty(context.Request["userid"]))
                    {
                        int studentid = int.Parse(context.Request["userid"]);
                        _student4day = _student4day.Where(w => w.studentid == studentid).ToList();
                    }
                    switch (strMode)
                    {
                        case "reportscome2school01user":
                            #region 
                            rss = reportscome2school01user(logScanInUser, _student4day, lStudent);
                            #endregion
                            break;
                        case "reportscome2school02user":
                            #region
                            rss = reportscome2school02user(logScanInUser, _student4day, lStudent);
                            #endregion
                            break;
                        case "reportscome2school03user":
                            #region
                            rss = reportscome2school03user(logScanInUser, logScanOutUser, _student4day, lStudent);
                            #endregion
                            break;
                    }
                }
                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        private dynamic reportscome2school01user(List<TLogUserTimeScan> logUser, List<student4day> _student4day, List<Student> lStudent)
        {
            dynamic rss = new JObject();
            var groupLog = (from a in logUser
                            select new
                            {
                                LogDate = a.LogDate.Value,
                                studentid = a.sID.Value,
                                status_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                status_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                status_2 = a.LogScanStatus.Contains("3") ? 1 : 0
                            }).ToList();


            var LogTime = (from a in _student4day
                           join b in groupLog on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.studentid }
                           equals new { JoinProperty1 = b.LogDate, JoinProperty2 = b.studentid }
                           into ab
                           from bb in ab.DefaultIfEmpty()
                           select new Report02User
                           {
                               day = a.dayscan.ToString("dd/MM/yyyy"),
                               levelname = a.studentlevel,
                               level2name = a.studentlevel2,
                               studentnumber = lStudent.Count(),
                               level2id = a.studentlevel2id,
                               levelid = a.studentlevelid,
                               female_status_0 = bb == null ? 0 : (a.sex == "1" ? bb.status_0 : 0),
                               female_status_1 = bb == null ? 0 : (a.sex == "1" ? bb.status_1 : 0),
                               female_status_2 = bb == null ? (a.sex == "1" ? 1 : 0) : (a.sex == "1" ? bb.status_2 : 0),
                               male_status_0 = bb == null ? 0 : (a.sex == "0" ? bb.status_0 : 0),
                               male_status_1 = bb == null ? 0 : (a.sex == "0" ? bb.status_1 : 0),
                               male_status_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.status_2 : 0),
                           }).ToList();

            rss = new JArray(from a in LogTime
                             group a by a.day into ag
                             select new JObject {
                                 new JProperty("day",ag.Key),
                                 new JProperty("studentnumber",ag.Where(w=>w.day ==ag.Key).Count()),
                                 new JProperty("male_status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_0)),
                                 new JProperty("male_status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_1)),
                                 new JProperty("male_status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_2)),
                                 new JProperty("female_status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_0)),
                                 new JProperty("female_status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_1)),
                                 new JProperty("female_status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_2))
                             });
            return rss;
        }
        private dynamic reportscome2school02user(List<TLogUserTimeScan> logUser, List<student4day> _student4day, List<Student> lStudent)
        {
            dynamic rss = new JObject();
            var groupLog2 = (from a in logUser
                             select new
                             {
                                 LogDate = a.LogDate.Value,
                                 studentid = a.sID.Value,
                                 status_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                 status_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                 status_2 = a.LogScanStatus.Contains("3") ? 1 : 0
                             }).ToList();

            var LogTime2 = (from a in _student4day
                            join b in groupLog2 on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.studentid }
                            equals new { JoinProperty1 = b.LogDate, JoinProperty2 = b.studentid }
                            into ab
                            from bb in ab.DefaultIfEmpty()
                            select new Report02User
                            {
                                day = a.dayscan.ToString("dd/MM/yyyy"),
                                levelname = a.studentlevel,
                                level2name = a.studentlevel2,
                                studentnumber = lStudent.Count(),
                                level2id = a.studentlevel2id,
                                levelid = a.studentlevelid,
                                female_status_0 = bb == null ? 0 : (a.sex == "1" ? bb.status_0 : 0),
                                female_status_1 = bb == null ? 0 : (a.sex == "1" ? bb.status_1 : 0),
                                female_status_2 = bb == null ? (a.sex == "1" ? 1 : 0) : (a.sex == "1" ? bb.status_2 : 0),
                                male_status_0 = bb == null ? 0 : (a.sex == "0" ? bb.status_0 : 0),
                                male_status_1 = bb == null ? 0 : (a.sex == "0" ? bb.status_1 : 0),
                                male_status_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.status_2 : 0),
                            }).ToList();

            dynamic Data = new JObject();
            Data.dayscan = new JObject();
            Data.level = new JArray();
            rss = new JArray();
            foreach (var gbday in (from a in LogTime2
                                   group a by a.day into ag
                                   select new { ag }))
            {
                Data.dayscan = gbday.ag.Key;
                foreach (var gblevel in ((from b in LogTime2
                                          where b.day == gbday.ag.Key
                                          group b by new { b.levelid, b.levelname } into bg
                                          select new { bg })).OrderBy(o => o.bg.Key.levelname))
                {
                    dynamic arraylevel = new JObject();
                    arraylevel.levelname = new JArray();
                    arraylevel.levelname = gblevel.bg.Key.levelname;
                    arraylevel.level2 = new JArray();
                    arraylevel.level2 = new JArray((from c in LogTime2
                                                    where c.levelid == gblevel.bg.Key.levelid
                                                    group c by new { c.level2name, c.level2id } into cg
                                                    orderby cg.Key.level2id ascending
                                                    select new JObject{
                                                        new JProperty("level2name",cg.Key.level2name),
                                                        new JProperty("level2id",cg.Key.level2id),
                                                        new JProperty("studentnember",cg.Where(w=>w.level2id == cg.Key.level2id).Count()),
                                                        new JProperty("female_status_0", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.female_status_0)),
                                                        new JProperty("female_status_1", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.female_status_1)),
                                                        new JProperty("female_status_2", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.female_status_2)),
                                                        new JProperty("male_status_0", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.male_status_0)),
                                                        new JProperty("male_status_1", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.male_status_1)),
                                                        new JProperty("male_status_2", cg.Where(w=>w.level2id == cg.Key.level2id).Sum(s=> s.male_status_2))
                                                    }));
                    Data.level.Add(arraylevel);
                }
                rss.Add(Data);
            }
            return rss;
        }
        private dynamic reportscome2school03user(List<TLogUserTimeScan> logScanInUser, List<TLogUserTimeScan> logScanOutUser, List<student4day> _student4day, List<Student> lStudent)
        {
            dynamic rss = new JObject();
            var groupLogScanIn3 = (from a in logScanInUser
                                   select new
                                   {
                                       LogDate = a.LogDate.Value,
                                       studentid = a.sID.Value,
                                       statusIn_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                       statusIn_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                       statusIn_2 = a.LogScanStatus.Contains("3") ? 1 : 0,
                                       timein = a.LogTime
                                   }).ToList();

            var groupLogScanOut3 = (from a in logScanOutUser
                                    select new
                                    {
                                        LogDate = a.LogDate.Value,
                                        studentid = a.sID.Value,
                                        statusOut_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                        statusOut_1 = a.LogScanStatus.Contains("2") ? 1 : 0,
                                        statusOut_2 = a.LogScanStatus.Contains("3") ? 1 : 0,
                                        timeout = a.LogTime
                                    }).ToList();

            var LogTime3 = (from a in _student4day
                            join b in groupLogScanIn3 on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.studentid }
                            equals new { JoinProperty1 = b.LogDate, JoinProperty2 = b.studentid }
                            into ab
                            from bb in ab.DefaultIfEmpty()

                            join c in groupLogScanOut3 on new { JoinProperty1 = a.dayscan, JoinProperty2 = a.studentid }
                            equals new { JoinProperty1 = c.LogDate, JoinProperty2 = c.studentid }
                            into ac
                            from cc in ac.DefaultIfEmpty()

                            select new Report03User
                            {
                                day = a.dayscan.ToString("dd/MM/yyyy"),
                                levelname = a.studentlevel,
                                level2name = a.studentlevel2,
                                studentname = a.studentname,
                                studentlastname = a.studentlastname,
                                studentid = a.studentid,
                                level2id = a.studentlevel2id,
                                levelid = a.studentlevelid,

                                female_statusin_0 = bb == null ? 0 : (a.sex == "1" ? bb.statusIn_0 : 0),
                                female_statusin_1 = bb == null ? 0 : (a.sex == "1" ? bb.statusIn_1 : 0),
                                female_statusin_2 = bb == null ? (a.sex == "1" ? 1 : 0) : (a.sex == "1" ? bb.statusIn_2 : 0),
                                male_statusin_0 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_0 : 0),
                                male_statusin_1 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_1 : 0),
                                male_statusin_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.statusIn_2 : 0),
                                timein = bb != null && bb.timein.HasValue ? bb.timein.Value.ToString(@"hh\:mm\:ss") : "",

                                female_statusout_0 = cc == null ? 0 : (a.sex == "1" ? cc.statusOut_0 : 0),
                                female_statusout_1 = cc == null ? 0 : (a.sex == "1" ? cc.statusOut_1 : 0),
                                female_statusout_2 = cc == null ? (a.sex == "1" ? 1 : 0) : (a.sex == "1" ? cc.statusOut_2 : 0),
                                male_statusout_0 = cc == null ? 0 : (a.sex == "0" ? cc.statusOut_0 : 0),
                                male_statusout_1 = cc == null ? 0 : (a.sex == "0" ? cc.statusOut_1 : 0),
                                male_statusout_2 = cc == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? cc.statusOut_2 : 0),
                                timeout = cc != null && cc.timeout.HasValue ? cc.timeout.Value.ToString(@"hh\:mm\:ss") : ""
                            }).ToList();


            rss = new JArray();
            var qgb = (from a in LogTime3
                       group a by a.day into ag
                       select new { dayscan = ag.Key }).ToList();
            foreach (var gbday in qgb)
            {
                dynamic Data = new JObject();
                Data.dayscan = new JObject();
                Data.student = new JArray();
                Data.dayscan = gbday.dayscan;
                Data.student = new JArray((from c in LogTime3
                                           where c.day == gbday.dayscan
                                           select new JObject{
                                               new JProperty("level2name",c.level2name),
                                               new JProperty("level2id",c.level2id),
                                               new JProperty("studentname",c.studentname),
                                               new JProperty("studentlastname",c.studentlastname),
                                               new JProperty("studentid",c.studentid),
                                               new JProperty("timein",c.timein),
                                               new JProperty("female_statusin_0", c.female_statusin_0),
                                               new JProperty("female_statusin_1", c.female_statusin_1),
                                               new JProperty("female_statusin_2", c.female_statusin_2),
                                               new JProperty("male_statusin_0", c.male_statusin_0),
                                               new JProperty("male_statusin_1", c.male_statusin_1),
                                               new JProperty("male_statusin_2", c.male_statusin_2),
                                               new JProperty("timeout",c.timeout),
                                               new JProperty("female_statusout_0", c.female_statusout_0),
                                               new JProperty("female_statusout_1", c.female_statusout_1),
                                               new JProperty("female_statusout_2", c.female_statusout_2),
                                               new JProperty("male_statusout_0", c.male_statusout_0),
                                               new JProperty("male_statusout_1", c.male_statusout_1),
                                               new JProperty("male_statusout_2", c.male_statusout_2)
                                           }));

                rss.Add(Data);
            }
            return rss;
        }

        class reports03
        {
            public string dayscan { get; set; }
            public List<level> sublevel { get; set; }
        }
        class level
        {
            public string levelname { get; set; }
            public List<student> student { get; set; }
        }
        class student
        {
            public string studentname { get; set; }
            public string studentid { get; set; }
            public string timein { get; set; }
            public string statusin_0 { get; set; }
            public string statusin_1 { get; set; }
            public string statusin_2 { get; set; }
            public string timeout { get; set; }
            public string statusout_0 { get; set; }
            public string statusout_1 { get; set; }
            public string statusout_2 { get; set; }
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

