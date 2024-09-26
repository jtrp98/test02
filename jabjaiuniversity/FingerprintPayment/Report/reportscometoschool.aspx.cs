using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using JabjaiEntity.DB;
using FingerprintPayment.App_Code;
using System.Globalization;

namespace FingerprintPayment.Report
{
    [System.Web.Script.Services.ScriptService]
    [WebService(Namespace = "http://xmlforasp.net")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class reportscometoschool : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static string reports01(search search_data)
        {
            var userData = GetUserData();

            getdata(search_data, HttpContext.Current.Session["sEntities"].ToString());
            dynamic rss = new JObject();
            var groupLog = (from a in logScanInUser
                            select new
                            {
                                LogDate = a.LogDate.Value,
                                studentid = a.sID.Value,
                                status_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                status_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                status_2 = a.LogScanStatus.Contains("3") ? 1 : 0,
                                status_3 = a.LogScanStatus.Contains("4") ? 1 : 0,
                                status_4 = a.LogScanStatus.Contains("5") ? 1 : 0,
                                status_5 = a.LogScanStatus.Contains("10") ? 1 : 0,
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
                               female_status_3 = bb == null ? 0 : (a.sex == "1" ? bb.status_3 : 0),
                               female_status_4 = bb == null ? 0 : (a.sex == "1" ? bb.status_4 : 0),
                               female_status_5 = bb == null ? 0 : (a.sex == "1" ? bb.status_5 : 0),
                               male_status_0 = bb == null ? 0 : (a.sex == "0" ? bb.status_0 : 0),
                               male_status_1 = bb == null ? 0 : (a.sex == "0" ? bb.status_1 : 0),
                               male_status_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.status_2 : 0),
                               male_status_3 = bb == null ? 0 : (a.sex == "0" ? bb.status_3 : 0),
                               male_status_4 = bb == null ? 0 : (a.sex == "0" ? bb.status_4 : 0),
                               male_status_5 = bb == null ? 0 : (a.sex == "0" ? bb.status_5 : 0),
                           }).ToList();

            var index = 1;
            rss.data = new JArray(from a in LogTime
                                  group a by a.day into ag
                                  select new JObject {
                                      new JProperty("index",index++),
                                      new JProperty("day",ag.Key),
                                      new JProperty("studentnumber",ag.Where(w=>w.day ==ag.Key).Count()),
                                      new JProperty("status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_0 + s.female_status_0)),
                                      new JProperty("status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_1 + s.female_status_1)),
                                      new JProperty("status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_2 + s.female_status_2)),
                                      new JProperty("status_3",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_3 + s.female_status_3)),
                                      new JProperty("status_4",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_4 + s.female_status_4)),
                                      new JProperty("status_5",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_5 + s.female_status_5)),
                                      new JProperty("male_status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_0)),
                                      new JProperty("male_status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_1)),
                                      new JProperty("male_status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_2)),
                                      new JProperty("male_status_3",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_3)),
                                      new JProperty("male_status_4",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_4)),
                                      new JProperty("male_status_5",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.male_status_5)),
                                      new JProperty("female_status_0",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_0)),
                                      new JProperty("female_status_1",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_1)),
                                      new JProperty("female_status_2",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_2)),
                                      new JProperty("female_status_3",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_3)),
                                      new JProperty("female_status_4",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_4)),
                                      new JProperty("female_status_5",ag.Where(w=>w.day ==ag.Key).Sum(s=>s.female_status_5))
                                  });

            int male = _student4day.Where(w => w.sex == "0").Count();
            int female = _student4day.Where(w => w.sex == "1").Count();

            rss.footer = new JObject();
            rss.footer.status_0 = LogTime.Sum(s => s.male_status_0 + s.female_status_0);
            rss.footer.status_1 = LogTime.Sum(s => s.male_status_1 + s.female_status_1);
            rss.footer.status_2 = LogTime.Sum(s => s.male_status_2 + s.female_status_2);
            rss.footer.status_3 = LogTime.Sum(s => s.male_status_3 + s.female_status_3);
            rss.footer.status_4 = LogTime.Sum(s => s.male_status_4 + s.female_status_4);
            rss.footer.status_5 = LogTime.Sum(s => s.male_status_5 + s.female_status_5);

            rss.footer.female_status_0 = LogTime.Sum(s => s.female_status_0);
            rss.footer.female_status_1 = LogTime.Sum(s => s.female_status_1);
            rss.footer.female_status_2 = LogTime.Sum(s => s.female_status_2);
            rss.footer.female_status_3 = LogTime.Sum(s => s.female_status_3);
            rss.footer.female_status_4 = LogTime.Sum(s => s.female_status_4);
            rss.footer.female_status_5 = LogTime.Sum(s => s.female_status_5);

            rss.footer.male_status_0 = LogTime.Sum(s => s.male_status_0);
            rss.footer.male_status_1 = LogTime.Sum(s => s.male_status_1);
            rss.footer.male_status_2 = LogTime.Sum(s => s.male_status_2);
            rss.footer.male_status_3 = LogTime.Sum(s => s.male_status_3);
            rss.footer.male_status_4 = LogTime.Sum(s => s.male_status_4);
            rss.footer.male_status_5 = LogTime.Sum(s => s.male_status_5);

            rss.footer.percent_status_0 = percent(male + female, LogTime.Sum(s => s.male_status_0 + s.female_status_0)).ToString("N2");
            rss.footer.percent_status_1 = percent(male + female, LogTime.Sum(s => s.male_status_1 + s.female_status_1)).ToString("N2");
            rss.footer.percent_status_2 = percent(male + female, LogTime.Sum(s => s.male_status_2 + s.female_status_2)).ToString("N2");
            rss.footer.percent_status_3 = percent(male + female, LogTime.Sum(s => s.male_status_3 + s.female_status_3)).ToString("N2");
            rss.footer.percent_status_4 = percent(male + female, LogTime.Sum(s => s.male_status_4 + s.female_status_4)).ToString("N2");
            rss.footer.percent_status_5 = percent(male + female, LogTime.Sum(s => s.male_status_5 + s.female_status_5)).ToString("N2");

            rss.footer.percent_female_status_0 = percent(female, LogTime.Sum(s => s.female_status_0)).ToString("N2");
            rss.footer.percent_female_status_1 = percent(female, LogTime.Sum(s => s.female_status_1)).ToString("N2");
            rss.footer.percent_female_status_2 = percent(female, LogTime.Sum(s => s.female_status_2)).ToString("N2");
            rss.footer.percent_female_status_3 = percent(female, LogTime.Sum(s => s.female_status_3)).ToString("N2");
            rss.footer.percent_female_status_4 = percent(female, LogTime.Sum(s => s.female_status_4)).ToString("N2");
            rss.footer.percent_female_status_5 = percent(female, LogTime.Sum(s => s.female_status_5)).ToString("N2");

            rss.footer.percent_male_status_0 = percent(male, LogTime.Sum(s => s.male_status_0)).ToString("N2");
            rss.footer.percent_male_status_1 = percent(male, LogTime.Sum(s => s.male_status_1)).ToString("N2");
            rss.footer.percent_male_status_2 = percent(male, LogTime.Sum(s => s.male_status_2)).ToString("N2");
            rss.footer.percent_male_status_3 = percent(male, LogTime.Sum(s => s.male_status_3)).ToString("N2");
            rss.footer.percent_male_status_4 = percent(male, LogTime.Sum(s => s.male_status_4)).ToString("N2");
            rss.footer.percent_male_status_5 = percent(male, LogTime.Sum(s => s.male_status_5)).ToString("N2");

            return rss.ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static string reports02(search search_data)
        {
            var userData = GetUserData();

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            getdata(search_data, sEntities);
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            var qlevel = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var qlevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();

            dynamic rss = new JObject();
            var groupLog = (from a in logScanInUser
                            select new
                            {
                                LogDate = a.LogDate.Value,
                                studentid = a.sID.Value,
                                status_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                status_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                status_2 = a.LogScanStatus.Contains("3") ? 1 : 0,
                                status_3 = a.LogScanStatus.Contains("4") ? 1 : 0,
                                status_4 = a.LogScanStatus.Contains("5") ? 1 : 0,
                                status_5 = a.LogScanStatus.Contains("10") ? 1 : 0,
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
                               female_status_3 = bb == null ? 0 : (a.sex == "1" ? bb.status_3 : 0),
                               female_status_4 = bb == null ? 0 : (a.sex == "1" ? bb.status_4 : 0),
                               female_status_5 = bb == null ? 0 : (a.sex == "1" ? bb.status_5 : 0),
                               male_status_0 = bb == null ? 0 : (a.sex == "0" ? bb.status_0 : 0),
                               male_status_1 = bb == null ? 0 : (a.sex == "0" ? bb.status_1 : 0),
                               male_status_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.status_2 : 0),
                               male_status_3 = bb == null ? 0 : (a.sex == "0" ? bb.status_3 : 0),
                               male_status_4 = bb == null ? 0 : (a.sex == "0" ? bb.status_4 : 0),
                               male_status_5 = bb == null ? 0 : (a.sex == "0" ? bb.status_5 : 0),
                           }).ToList();

            var index = 1;
            string levelname = "";
            rss.data = new JArray(from a in LogTime
                                  group a by new { a.levelid, a.levelname } into ag
                                  select new JObject {
                                      new JProperty("levelname",ag.Key.levelname),
                                      new JProperty("studentnember",ag.Where(w=>w.levelid == ag.Key.levelid).Count()),
                                      new JProperty("footer_status_0", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_0 + s.male_status_0)),
                                      new JProperty("footer_status_1", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_1 + s.male_status_1)),
                                      new JProperty("footer_status_2", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_2 + s.male_status_2)),
                                      new JProperty("footer_status_3", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_3 + s.male_status_3)),
                                      new JProperty("footer_status_4", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_4 + s.male_status_4)),
                                      new JProperty("footer_status_5", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_5 + s.male_status_5)),
                                      new JProperty("footer_female_status_0", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_0)),
                                      new JProperty("footer_female_status_1", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_1)),
                                      new JProperty("footer_female_status_2", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_2)),
                                      new JProperty("footer_female_status_3", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_3)),
                                      new JProperty("footer_female_status_4", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_4)),
                                      new JProperty("footer_female_status_5", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.female_status_5)),
                                      new JProperty("footer_male_status_0", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_0)),
                                      new JProperty("footer_male_status_1", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_1)),
                                      new JProperty("footer_male_status_2", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_2)),
                                      new JProperty("footer_male_status_3", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_3)),
                                      new JProperty("footer_male_status_4", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_4)),
                                      new JProperty("footer_male_status_5", ag.Where(w=>w.levelid == ag.Key.levelid).Sum(s=> s.male_status_5)),
                                      new JProperty("level2",(from c in LogTime
                                                              where c.levelid == ag.Key.levelid
                                                              group c by new { c.level2name, c.level2id } into cg
                                                              orderby cg.Key.level2id ascending
                                                              select new JObject{
                                                                  new JProperty("index",index++),
                                                                  new JProperty("levelname",ag.Key.levelname),
                                                                  new JProperty("show_levelname",setheader(ref levelname,ag.Key.levelname)),
                                                                  new JProperty("level2name",cg.Key.level2name),
                                                                  new JProperty("level2id",cg.Key.level2id),
                                                                  new JProperty("studentnember",cg.Where(w=>w.level2id == cg.Key.level2id).Count()),
                                                                  new JProperty("status_0", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_0 + s.male_status_0)),
                                                                  new JProperty("status_1", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_1 + s.male_status_1)),
                                                                  new JProperty("status_2", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_2 + s.male_status_2)),
                                                                  new JProperty("status_3", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_3 + s.male_status_3)),
                                                                  new JProperty("status_4", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_4 + s.male_status_4)),
                                                                  new JProperty("status_5", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_5 + s.male_status_5)),
                                                                  new JProperty("female_status_0", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_0)),
                                                                  new JProperty("female_status_1", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_1)),
                                                                  new JProperty("female_status_2", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_2)),
                                                                  new JProperty("female_status_3", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_3)),
                                                                  new JProperty("female_status_4", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_4)),
                                                                  new JProperty("female_status_5", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.female_status_5)),
                                                                  new JProperty("male_status_0", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_0)),
                                                                  new JProperty("male_status_1", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_1)),
                                                                  new JProperty("male_status_2", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_2)),
                                                                  new JProperty("male_status_3", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_3)),
                                                                  new JProperty("male_status_4", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_4)),
                                                                  new JProperty("male_status_5", cg.Where(w=>w.level2id == cg.Key.level2id)
                                                                  .Sum(s=> s.male_status_5))
                                                              }))
                                  });

            #region Footer
            int male = _student4day.Where(w => w.sex == "0").Count();
            int female = _student4day.Where(w => w.sex == "1").Count();

            rss.footer = new JObject();
            rss.footer.status_0 = LogTime.Sum(s => s.male_status_0 + s.female_status_0);
            rss.footer.status_1 = LogTime.Sum(s => s.male_status_1 + s.female_status_1);
            rss.footer.status_2 = LogTime.Sum(s => s.male_status_2 + s.female_status_2);
            rss.footer.status_3 = LogTime.Sum(s => s.male_status_3 + s.female_status_3);
            rss.footer.status_4 = LogTime.Sum(s => s.male_status_4 + s.female_status_4);
            rss.footer.status_5 = LogTime.Sum(s => s.male_status_5 + s.female_status_5);

            rss.footer.female_status_0 = LogTime.Sum(s => s.female_status_0);
            rss.footer.female_status_1 = LogTime.Sum(s => s.female_status_1);
            rss.footer.female_status_2 = LogTime.Sum(s => s.female_status_2);
            rss.footer.female_status_3 = LogTime.Sum(s => s.female_status_3);
            rss.footer.female_status_4 = LogTime.Sum(s => s.female_status_4);
            rss.footer.female_status_5 = LogTime.Sum(s => s.female_status_5);

            rss.footer.male_status_0 = LogTime.Sum(s => s.male_status_0);
            rss.footer.male_status_1 = LogTime.Sum(s => s.male_status_1);
            rss.footer.male_status_2 = LogTime.Sum(s => s.male_status_2);
            rss.footer.male_status_3 = LogTime.Sum(s => s.male_status_3);
            rss.footer.male_status_4 = LogTime.Sum(s => s.male_status_4);
            rss.footer.male_status_5 = LogTime.Sum(s => s.male_status_5);

            rss.footer.percent_status_0 = percent(male + female, LogTime.Sum(s => s.male_status_0 + s.female_status_0)).ToString("N2");
            rss.footer.percent_status_1 = percent(male + female, LogTime.Sum(s => s.male_status_1 + s.female_status_1)).ToString("N2");
            rss.footer.percent_status_2 = percent(male + female, LogTime.Sum(s => s.male_status_2 + s.female_status_2)).ToString("N2");
            rss.footer.percent_status_3 = percent(male + female, LogTime.Sum(s => s.male_status_3 + s.female_status_3)).ToString("N2");
            rss.footer.percent_status_4 = percent(male + female, LogTime.Sum(s => s.male_status_4 + s.female_status_4)).ToString("N2");
            rss.footer.percent_status_5 = percent(male + female, LogTime.Sum(s => s.male_status_5 + s.female_status_5)).ToString("N2");

            rss.footer.percent_female_status_0 = percent(female, LogTime.Sum(s => s.female_status_0)).ToString("N2");
            rss.footer.percent_female_status_1 = percent(female, LogTime.Sum(s => s.female_status_1)).ToString("N2");
            rss.footer.percent_female_status_2 = percent(female, LogTime.Sum(s => s.female_status_2)).ToString("N2");
            rss.footer.percent_female_status_3 = percent(female, LogTime.Sum(s => s.female_status_3)).ToString("N2");
            rss.footer.percent_female_status_4 = percent(female, LogTime.Sum(s => s.female_status_4)).ToString("N2");
            rss.footer.percent_female_status_5 = percent(female, LogTime.Sum(s => s.female_status_5)).ToString("N2");

            rss.footer.percent_male_status_0 = percent(male, LogTime.Sum(s => s.male_status_0)).ToString("N2");
            rss.footer.percent_male_status_1 = percent(male, LogTime.Sum(s => s.male_status_1)).ToString("N2");
            rss.footer.percent_male_status_2 = percent(male, LogTime.Sum(s => s.male_status_2)).ToString("N2");
            rss.footer.percent_male_status_3 = percent(male, LogTime.Sum(s => s.male_status_3)).ToString("N2");
            rss.footer.percent_male_status_4 = percent(male, LogTime.Sum(s => s.male_status_4)).ToString("N2");
            rss.footer.percent_male_status_5 = percent(male, LogTime.Sum(s => s.male_status_5)).ToString("N2");
            #endregion
            return rss.ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static string reports03(search search_data)
        {
            var userData = GetUserData();

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            getdata(search_data, sEntities);
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            var qlevel = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var qlevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();

            dynamic rss = new JObject();
            var groupLogScanIn3 = (from a in logScanInUser
                                   select new
                                   {
                                       LogDate = a.LogDate.Value,
                                       studentid = a.sID.Value,
                                       statusIn_0 = a.LogScanStatus.Contains("0") ? 1 : 0,
                                       statusIn_1 = a.LogScanStatus.Contains("1") ? 1 : 0,
                                       statusIn_2 = a.LogScanStatus.Contains("3") ? 1 : 0,
                                       statusIn_3 = a.LogScanStatus.Contains("4") ? 1 : 0,
                                       statusIn_4 = a.LogScanStatus.Contains("5") ? 1 : 0,
                                       statusIn_5 = a.LogScanStatus.Contains("10") ? 1 : 0,
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

            var LogTime = (from a in _student4day
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
                               female_statusin_3 = bb == null ? 0 : (a.sex == "1" ? bb.statusIn_3 : 0),
                               female_statusin_4 = bb == null ? 0 : (a.sex == "1" ? bb.statusIn_4 : 0),
                               female_statusin_5 = bb == null ? 0 : (a.sex == "1" ? bb.statusIn_5 : 0),
                               male_statusin_0 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_0 : 0),
                               male_statusin_1 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_1 : 0),
                               male_statusin_2 = bb == null ? (a.sex == "0" ? 1 : 0) : (a.sex == "0" ? bb.statusIn_2 : 0),
                               male_statusin_3 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_3 : 0),
                               male_statusin_4 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_4 : 0),
                               male_statusin_5 = bb == null ? 0 : (a.sex == "0" ? bb.statusIn_5 : 0),
                               timein = bb != null && bb.timein.HasValue ? bb.timein.Value.ToString(@"hh\:mm\:ss") : "-",
                           }).ToList();

            int index = 1;
            switch (search_data.status)
            {
                case "0": LogTime = LogTime.Where(w => w.male_statusin_0 == 1 || w.female_statusin_0 == 1).ToList(); break;
                case "1": LogTime = LogTime.Where(w => w.male_statusin_1 == 1 || w.female_statusin_1 == 1).ToList(); break;
                case "3": LogTime = LogTime.Where(w => w.male_statusin_2 == 1 || w.female_statusin_2 == 1).ToList(); break;
                case "4": LogTime = LogTime.Where(w => w.male_statusin_3 == 1 || w.female_statusin_3 == 1).ToList(); break;
                case "5": LogTime = LogTime.Where(w => w.male_statusin_4 == 1 || w.female_statusin_4 == 1).ToList(); break;
                case "10": LogTime = LogTime.Where(w => w.male_statusin_5 == 1 || w.female_statusin_5 == 1).ToList(); break;
            }
            rss.data = new JArray(from a in LogTime
                                  group a by a.day into ag
                                  select new JObject(
                                      new JProperty("day", ag.Key),
                                      new JProperty("student_data", (from c in LogTime
                                                                     where c.day == ag.Key
                                                                     select new JObject(
                                                                         new JProperty("index", index++),
                                                                         new JProperty("level2name", c.level2name),
                                                                         new JProperty("level2id", c.level2id),
                                                                         new JProperty("studentname", c.studentname),
                                                                         new JProperty("studentlastname", c.studentlastname),
                                                                         new JProperty("studentid", c.studentid),
                                                                         new JProperty("timein", c.timein),
                                                                         new JProperty("statusin_0", (c.female_statusin_0 + c.male_statusin_0) == 1 ? true : false),
                                                                         new JProperty("statusin_1", (c.female_statusin_1 + c.male_statusin_1) == 1 ? true : false),
                                                                         new JProperty("statusin_2", (c.female_statusin_2 + c.male_statusin_2) == 1 ? true : false),
                                                                         new JProperty("statusin_3", (c.female_statusin_3 + c.male_statusin_3) == 1 ? true : false),
                                                                         new JProperty("statusin_4", (c.female_statusin_4 + c.male_statusin_4) == 1 ? true : false),
                                                                         new JProperty("statusin_5", (c.female_statusin_5 + c.male_statusin_5) == 1 ? true : false),
                                                                         new JProperty("timeout", c.timeout),
                                                                         new JProperty("female_statusout_0", c.female_statusout_0),
                                                                         new JProperty("female_statusout_1", c.female_statusout_1),
                                                                         new JProperty("female_statusout_2", c.female_statusout_2),
                                                                         new JProperty("male_statusout_0", c.male_statusout_0),
                                                                         new JProperty("male_statusout_1", c.male_statusout_1),
                                                                         new JProperty("male_statusout_2", c.male_statusout_2)
                                                                         )
                                               ))));

            #region Footer
            int male = _student4day.Where(w => w.sex == "0").Count();
            int female = _student4day.Where(w => w.sex == "1").Count();

            rss.footer = new JObject();
            rss.footer.studentnember = LogTime.Count() == 0 ? 0 : (LogTime.Count() / 2);
            rss.footer.sum_status_0 = LogTime.Sum(s => s.male_statusin_0 + s.female_statusin_0);
            rss.footer.sum_status_1 = LogTime.Sum(s => s.male_statusin_1 + s.female_statusin_1);
            rss.footer.sum_status_2 = LogTime.Sum(s => s.male_statusin_2 + s.female_statusin_2);
            rss.footer.sum_status_3 = LogTime.Sum(s => s.male_statusin_3 + s.female_statusin_3);
            rss.footer.sum_status_4 = LogTime.Sum(s => s.male_statusin_4 + s.female_statusin_4);
            rss.footer.sum_status_5 = LogTime.Sum(s => s.male_statusin_5 + s.female_statusin_5);

            rss.footer.female_status_0 = LogTime.Sum(s => s.female_statusin_0);
            rss.footer.female_status_1 = LogTime.Sum(s => s.female_statusin_1);
            rss.footer.female_status_2 = LogTime.Sum(s => s.female_statusin_2);
            rss.footer.female_status_3 = LogTime.Sum(s => s.female_statusin_3);
            rss.footer.female_status_4 = LogTime.Sum(s => s.female_statusin_4);
            rss.footer.female_status_5 = LogTime.Sum(s => s.female_statusin_5);

            rss.footer.male_status_0 = LogTime.Sum(s => s.male_statusin_0);
            rss.footer.male_status_1 = LogTime.Sum(s => s.male_statusin_1);
            rss.footer.male_status_2 = LogTime.Sum(s => s.male_statusin_2);
            rss.footer.male_status_3 = LogTime.Sum(s => s.male_statusin_3);
            rss.footer.male_status_4 = LogTime.Sum(s => s.male_statusin_4);
            rss.footer.male_status_5 = LogTime.Sum(s => s.male_statusin_5);

            rss.footer.percent_status_0 = percent(male + female, LogTime.Sum(s => s.male_statusin_0 + s.female_statusin_0)).ToString("N2");
            rss.footer.percent_status_1 = percent(male + female, LogTime.Sum(s => s.male_statusin_1 + s.female_statusin_1)).ToString("N2");
            rss.footer.percent_status_2 = percent(male + female, LogTime.Sum(s => s.male_statusin_2 + s.female_statusin_2)).ToString("N2");
            rss.footer.percent_status_3 = percent(male + female, LogTime.Sum(s => s.male_statusin_3 + s.female_statusin_3)).ToString("N2");
            rss.footer.percent_status_4 = percent(male + female, LogTime.Sum(s => s.male_statusin_4 + s.female_statusin_4)).ToString("N2");
            rss.footer.percent_status_5 = percent(male + female, LogTime.Sum(s => s.male_statusin_5 + s.female_statusin_5)).ToString("N2");

            rss.footer.percent_female_status_0 = percent(female, LogTime.Sum(s => s.female_statusin_0)).ToString("N2");
            rss.footer.percent_female_status_1 = percent(female, LogTime.Sum(s => s.female_statusin_1)).ToString("N2");
            rss.footer.percent_female_status_2 = percent(female, LogTime.Sum(s => s.female_statusin_2)).ToString("N2");
            rss.footer.percent_female_status_3 = percent(female, LogTime.Sum(s => s.female_statusin_3)).ToString("N2");
            rss.footer.percent_female_status_4 = percent(female, LogTime.Sum(s => s.female_statusin_4)).ToString("N2");
            rss.footer.percent_female_status_5 = percent(female, LogTime.Sum(s => s.female_statusin_5)).ToString("N2");

            rss.footer.percent_male_status_0 = percent(male, LogTime.Sum(s => s.male_statusin_0)).ToString("N2");
            rss.footer.percent_male_status_1 = percent(male, LogTime.Sum(s => s.male_statusin_1)).ToString("N2");
            rss.footer.percent_male_status_2 = percent(male, LogTime.Sum(s => s.male_statusin_2)).ToString("N2");
            rss.footer.percent_male_status_3 = percent(male, LogTime.Sum(s => s.male_statusin_3)).ToString("N2");
            rss.footer.percent_male_status_4 = percent(male, LogTime.Sum(s => s.male_statusin_4)).ToString("N2");
            rss.footer.percent_male_status_5 = percent(male, LogTime.Sum(s => s.male_statusin_5)).ToString("N2");
            #endregion
            return rss.ToString();
        }

        private static bool setheader(ref string header, string word)
        {
            if (header != word)
            {
                header = word;
                return true;
            }
            else
            {
                return false;
            }

        }
        private static double percent(int total, int values)
        {
            if (total == 0) return 0;
            return (100.0 * values) / total;
        }
        private static List<TLogUserTimeScan> logScanInUser = new List<TLogUserTimeScan>();
        private static List<TLogUserTimeScan> logScanOutUser = new List<TLogUserTimeScan>();
        private static List<Student> lStudent = new List<Student>();
        private static List<student4day> _student4day = new List<student4day>();
        private static void getdata(search search_data, string sEntities)
        {
            DateTime daystart = DateTime.Today;
            DateTime dayend = DateTime.Today;
            var userData = GetUserData();
            if (!string.IsNullOrEmpty(search_data.daystart))
            {
                daystart = DateTime.ParseExact(search_data.daystart, "dd/MM/yyyy", new CultureInfo("en-us"));
                if (string.IsNullOrEmpty(search_data.dayend)) dayend = daystart;
                else dayend = DateTime.ParseExact(search_data.dayend, "dd/MM/yyyy", new CultureInfo("en-us"));
            }

            Dictionary<DateTime, int> ListDay = new Dictionary<DateTime, int>();
            for (int i = 0; daystart <= dayend.AddDays(-i); i++)
            {
                int nDay = (int)dayend.AddDays(-i).DayOfWeek;
                DateTime Day = dayend.AddDays(-i);
                ListDay.Add(Day, nDay);
            }

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                logScanInUser = db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && (w.LogDate >= daystart && w.LogDate <= dayend) && w.LogType.Contains("0")).AsQueryable().ToList();
                logScanOutUser = db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && ( w.LogDate >= daystart && w.LogDate <= dayend ) && w.LogType.Contains("1")).AsQueryable().ToList();
                lStudent = new Student().getStudent(sEntities);
                _student4day = new student4day().getListStudent(sEntities, daystart, dayend);

                if (search_data.level_id.HasValue)
                {
                    _student4day = _student4day.Where(w => w.studentlevel2id == search_data.level_id).ToList();
                }
                else if (search_data.level2_id.HasValue)
                {
                    _student4day = _student4day.Where(w => w.studentlevel2id == search_data.level2_id).ToList();
                }

                if (!string.IsNullOrEmpty(search_data.status))
                {
                    logScanInUser = logScanInUser.Where(w => w.LogScanStatus.Contains(search_data.status)).ToList();
                }
                if (search_data.user_id.HasValue)
                {
                    _student4day = _student4day.Where(w => w.studentid == search_data.user_id).ToList();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["sEntities"] = "JabJaiEntities";
            if (string.IsNullOrEmpty(Session["sEntities"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear order by numberYear desc", "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    hdfschoolname.Value = tCompany.sCompany;
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");

                }
            }
        }

        public class search
        {
            public String daystart { get; set; }
            public String dayend { get; set; }

            public int? user_id { get; set; }
            public int? level_id { get; set; }
            public int? level2_id { get; set; }
            public string status { get; set; }
        }
    }
}