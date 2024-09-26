using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Web.SessionState;
using JabjaiEntity.DB;
using System.Text;
using JabjaiMainClass;
using Newtonsoft.Json.Linq;
using MasterEntity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class ReportLearnScanning : IHttpHandler, IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {

                string userid = fcommon.ReplaceInjection(context.Request["userid"]);
                string Day = fcommon.ReplaceInjection(context.Request["day"]);
                string StrDayEnd = fcommon.ReplaceInjection(context.Request["dayend"]);
                string nTerm = fcommon.ReplaceInjection(context.Request["nTerm"]);//"TS0000004";
                string PlaneId = fcommon.ReplaceInjection(context.Request["planeid"]);// "30001101";
                int nSubLevel2 = int.Parse(fcommon.ReplaceInjection(context.Request["sublevel2"]));// 11;
                string strMode = fcommon.ReplaceInjection(context.Request["mode"]);
                string strStatus = fcommon.ReplaceInjection(context.Request["status"]);
                int studentId = int.Parse(fcommon.ReplaceInjection(context.Request["studentid"] ?? "0"));

                DateTime LogLearnDate = string.IsNullOrEmpty(Day) ? DateTime.Today : DateTime.ParseExact(Day, "dd/MM/yyyy", new CultureInfo("en-us"));
                DateTime LogLearnDateEnd = string.IsNullOrEmpty(StrDayEnd) ? LogLearnDate : DateTime.ParseExact(StrDayEnd, "dd/MM/yyyy", new CultureInfo("en-us"));
                //if (!string.IsNullOrEmpty(nTerm)) nTerm = string.Format("TS{0:0000000}", int.Parse(nTerm));
                dynamic rss = new JObject();
                DateTime DayStart = DateTime.Today, DayEnd = DateTime.Today;
                List<TLogLearnTimeScan> tLogLearnTimeScan = new List<TLogLearnTimeScan>();
                rss.Data = new JArray();

                StudentLogic logic = new StudentLogic(db);
                string TermId = "", SQL = "";

                if (!string.IsNullOrEmpty(Day))
                {
                    //tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.LogLearnDate >= LogLearnDate && w.LogLearnDate <= LogLearnDateEnd).ToList();
                    DayStart = LogLearnDate;
                    DayEnd = LogLearnDateEnd;
                    TermId = logic.GetTermId(DayStart, userData);
                }
                else
                {
                    foreach (var data in db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == nTerm))
                    {
                        //DayStart = DateTime.Today;
                        DayStart = data.dStart.Value;
                        DayEnd = DateTime.Today <= data.dEnd.Value ? DateTime.Today : data.dEnd.Value;
                    }
                    //tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.LogLearnDate >= DayStart && w.LogLearnDate <= DayEnd).ToList();
                    TermId = logic.GetTermId(DayEnd, userData);
                }

                #region Get List Data
                var qStudent = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                join b in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                where a.cDel == null && b.nTermSubLevel2 == nSubLevel2 && b.nTerm.Trim() == TermId.Trim()
                                orderby a.nStudentNumber
                                select a.sID).AsQueryable().ToList();

                string _Comm = "";
                qStudent.ForEach(f =>
                {
                    _Comm += (string.IsNullOrEmpty(_Comm) ? "" : ",") + f;
                });

                SQL = string.Format("select * from TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
                tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

                SQL = string.Format("select * from [JabjaiSchoolHistory].dbo.TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
                tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

                //List<TPlane> tPlane = db.TPlanes.ToList();
                List<TLogLearnTimeScan> tLogLearnTimeScanUser = tLogLearnTimeScan.Where(w => w.sUserType == "0" && qStudent.Contains(w.sID)).ToList();

                List<TLogLearnTimeScan> tLogLearnTimeScanTeacher = tLogLearnTimeScan.Where(w => w.sUserType != "0").ToList();
                List<JabjaiEntity.DB.TUser> tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                                     join b in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                                     where a.cDel == null && b.nTermSubLevel2 == nSubLevel2 && b.nTerm.Trim() == TermId.Trim()
                                                     orderby a.nStudentNumber
                                                     select a).AsQueryable().ToList();

                List<TEmployee> tEmployees = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList();
                //List<TTermTimeTable> tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).ToList();
                //List<TSchedule> tSchedule = db.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

                int NumberUser = db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == nSubLevel2 && w.cDel == null).Count();
                var DataSchedule = (from a in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                                    join b in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable.Value equals b.nTermTable
                                    join c in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals c.sPlaneID
                                    where b.nTermSubLevel2 == nSubLevel2 && b.nTerm == nTerm
                                    select new { c.sPlaneName, c.sPlaneID, a.sEmp, a.sScheduleID, a.nPlaneDay, a.tStart, a.tEnd, c.courseCode }).ToList();

                if (!string.IsNullOrEmpty(PlaneId) && studentId == 0)
                {
                    int sScheduleID = int.Parse(PlaneId);
                    DataSchedule = DataSchedule.Where(w => w.sScheduleID == sScheduleID).ToList();
                }
                #endregion

                switch (strMode)
                {
                    case "reportmain":
                        #region
                        for (int i = 0; DayStart <= DayEnd.AddDays(-i); i++)
                        {
                            LogLearnDate = DayEnd.AddDays(-i);
                            int nDay = (int)LogLearnDate.DayOfWeek;
                            dynamic Data = new JObject();
                            //LogLearnDate;
                            Data.DateScan = LogLearnDate.ToString("dd/MM/yyyy");
                            //rss.DateScan = DayStart.AddDays(i);
                            Data.DataScan = new JArray();
                            foreach (var dataSchedule in DataSchedule.Where(w => w.nPlaneDay == nDay).OrderBy(o => o.tStart))
                            {
                                if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                                List<TLogLearnTimeScan> LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0"
                                && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();
                                List<TLogLearnTimeScan> LogLearnTimeOut = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "1"
                                && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();

                                dynamic JSonData = new JObject();
                                JSonData.sPlaneName = dataSchedule.sPlaneName;
                                JSonData.sPlaneID = dataSchedule.sPlaneID;
                                JSonData.courseCode = dataSchedule.courseCode;
                                JSonData.ScheduleTime = dataSchedule.tStart.Value.ToString(@"hh\:mm") + " - " + dataSchedule.tEnd.Value.ToString(@"hh\:mm");
                                JSonData.sScheduleID = dataSchedule.sScheduleID;

                                int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0, Status_4 = 0, Status_5 = 0;
                                Status_0 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "0").Count();
                                Status_1 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "1").Count();
                                Status_2 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "3").Count();
                                Status_3 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "4").Count();
                                Status_4 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "5").Count();
                                Status_5 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Trim() == "6").Count();

                                JSonData.Status_0 = Status_0;
                                JSonData.Status_1 = Status_1;
                                JSonData.Status_2 = Status_2 + (NumberUser - (Status_0 + Status_1 + Status_2 + Status_3 + Status_4 + Status_5));
                                JSonData.Status_3 = Status_3;
                                JSonData.Status_4 = Status_4;
                                JSonData.Status_5 = Status_5;

                                Data.DataScan.Add(JSonData);
                            }
                            if (Data.DataScan.Count > 0) rss.Data.Add(Data);
                        }
                        #endregion
                        break;
                    case "repostplane":
                        #region
                        //rss.ScheduleData = new JArray();
                        for (int i = 0; DayStart >= DayEnd.AddDays(-i); i++)
                        {
                            int nDay = (int)LogLearnDate.DayOfWeek;
                            LogLearnDate = DayEnd.AddDays(-i);
                            dynamic Data = new JObject();
                            //LogLearnDate;
                            Data.DateScan = LogLearnDate.ToString("dd/MM/yyyy");
                            //rss.DateScan = DayStart.AddDays(i);
                            Data.DataScan = new JArray();
                            foreach (var dataSchedule in DataSchedule)
                            {
                                if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                                List<TLogLearnTimeScan> LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0" && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();
                                List<TLogLearnTimeScan> LogLearnTimeOut = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "1" && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();

                                var tUserScan = (from a in tUser
                                                 join b in LogLearnTimeIn on a.sID equals b.sID into jb
                                                 from timein in jb.DefaultIfEmpty()
                                                 join c in LogLearnTimeOut on a.sID equals c.sID into jc
                                                 from timeout in jc.DefaultIfEmpty()
                                                 where a.nTermSubLevel2 == nSubLevel2
                                                 select new
                                                 {
                                                     a.sName,
                                                     a.sLastname,
                                                     Status_IN = timein == null ? "3" : timein.LogLearnScanStatus,
                                                     Status_OUT = timeout == null ? "3" : timeout.LogLearnScanStatus,
                                                     Time_IN = timein == null ? null : timein.LogLearnTime,
                                                     Time_OUT = timeout == null ? null : timeout.LogLearnTime,
                                                     a.sID
                                                 }).ToList();

                                dynamic JSonData = new JObject();
                                JSonData.sPlaneName = dataSchedule.sPlaneName + " เวลา " + dataSchedule.tStart.Value + " - " + dataSchedule.tEnd.Value;
                                JSonData.sPlaneID = dataSchedule.sPlaneID;
                                JSonData.courseCode = dataSchedule.courseCode;
                                JSonData.sScheduleID = dataSchedule.sScheduleID;
                                JSonData.UserScan = new JArray(
                                          from a in tUserScan
                                          select new JObject(
                                              //new JProperty("UserId", a.sID),
                                              new JProperty("sName", a.sName + " " + a.sLastname),
                                              new JProperty("Status_IN", a.Status_IN.Trim()),
                                              new JProperty("Status_OUT", a.Status_OUT.Trim()),
                                              new JProperty("Time_IN", a.Time_IN),
                                              new JProperty("Time_OUT", a.Time_OUT)));

                                if (dataSchedule.sEmp.HasValue)
                                {
                                    List<TLogLearnTimeScan> LogLearnEmployessTimeIn = tLogLearnTimeScanTeacher.Where(w => w.LogLearnType == "0" && w.sScheduleID == dataSchedule.sScheduleID && w.sID == dataSchedule.sEmp).ToList();
                                    List<TLogLearnTimeScan> LogLearnEmployessTimeOut = tLogLearnTimeScanTeacher.Where(w => w.LogLearnType == "1" && w.sScheduleID == dataSchedule.sScheduleID && w.sID == dataSchedule.sEmp).ToList();

                                    var tTeacher = from a in tEmployees
                                                   join tb in LogLearnEmployessTimeIn on a.sEmp equals tb.sID into jtb
                                                   from Teacherin in jtb.DefaultIfEmpty()
                                                   join tc in LogLearnEmployessTimeOut on a.sEmp equals tc.sID into jtc
                                                   from Teacherout in jtc.DefaultIfEmpty()
                                                   where a.sEmp == dataSchedule.sEmp
                                                   select new
                                                   {
                                                       a.sName,
                                                       a.sLastname,
                                                       Status_IN = Teacherin == null ? "3" : Teacherin.LogLearnScanStatus,
                                                       Status_OUT = Teacherout == null ? "3" : Teacherout.LogLearnScanStatus,
                                                       Time_IN = Teacherin == null ? null : Teacherin.LogLearnTime,
                                                       Time_OUT = Teacherout == null ? null : Teacherout.LogLearnTime,
                                                   };

                                    JSonData.TeacherScan = new JArray(
                                     from a in tTeacher
                                     select new JObject(
                                         new JProperty("sName", a.sName + " " + a.sLastname),
                                         new JProperty("Status_IN", a.Status_IN.Trim()),
                                         new JProperty("Status_OUT", a.Status_OUT.Trim()),
                                         new JProperty("Time_IN", a.Time_IN),
                                         new JProperty("Time_OUT", a.Time_OUT)));
                                }
                                else
                                {
                                    JSonData.TeacherScan = new JArray();
                                }
                                Data.DataScan.Add(JSonData);
                            }
                            if (Data.DataScan.Count > 0) rss.Data.Add(Data);
                        }
                        #endregion
                        break;
                    case "dailyreport":
                        #region
                        for (int i = 0; DayStart == DayEnd.AddDays(-i); i++)
                        {
                            dynamic Data = new JObject();
                            int nDay = (int)LogLearnDate.DayOfWeek;
                            LogLearnDate = DayEnd;
                            //LogLearnDate;
                            Data.DateScan = LogLearnDate.ToString("dd/MM/yyyy");
                            //rss.DateScan = DayStart.AddDays(i);
                            Data.DataScan = new JArray();

                            foreach (var dataSchedule in DataSchedule)
                            {
                                if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                                List<TLogLearnTimeScan> LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0" && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();
                                List<TLogLearnTimeScan> LogLearnTimeOut = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "1" && w.sScheduleID == dataSchedule.sScheduleID && w.LogLearnDate == LogLearnDate).ToList();

                                var tUserScan = (from a in tUser
                                                 join b in LogLearnTimeIn on a.sID equals b.sID into jb
                                                 from timein in jb.DefaultIfEmpty()
                                                 join c in LogLearnTimeOut on a.sID equals c.sID into jc
                                                 from timeout in jc.DefaultIfEmpty()
                                                 where a.nTermSubLevel2 == nSubLevel2
                                                 select new
                                                 {
                                                     a.sName,
                                                     a.sLastname,
                                                     Status_IN = timein == null ? "3" : timein.LogLearnScanStatus,
                                                     Status_OUT = timeout == null ? "3" : timeout.LogLearnScanStatus,
                                                     Time_IN = timein == null ? null : timein.LogLearnTime,
                                                     Time_OUT = timeout == null ? null : timeout.LogLearnTime,
                                                     a.sID
                                                 }).ToList();

                                if (!string.IsNullOrEmpty(strStatus))
                                {
                                    tUserScan = tUserScan.Where(w => w.Status_IN.Contains(strStatus)).ToList();
                                }

                                dynamic JSonData = new JObject();
                                JSonData.sPlaneName = dataSchedule.sPlaneName;
                                JSonData.sPlaneID = dataSchedule.sPlaneID;
                                JSonData.courseCode = dataSchedule.courseCode;
                                JSonData.sScheduleID = dataSchedule.sScheduleID;
                                JSonData.ScheduleTime = dataSchedule.tStart.Value.ToString(@"hh\:mm") + " - " + dataSchedule.tEnd.Value.ToString(@"hh\:mm");
                                JSonData.UserScan = new JArray(
                                          from a in tUserScan
                                          select new JObject(
                                              //new JProperty("UserId", a.sID),
                                              new JProperty("sName", a.sName + " " + a.sLastname),
                                              new JProperty("Status_IN", a.Status_IN.Trim()),
                                              new JProperty("Status_OUT", a.Status_OUT.Trim()),
                                              new JProperty("Time_IN", a.Time_IN),
                                              new JProperty("Time_OUT", a.Time_OUT)));

                                if (dataSchedule.sEmp.HasValue)
                                {
                                    List<TLogLearnTimeScan> LogLearnEmployessTimeIn = tLogLearnTimeScanTeacher.Where(w => w.LogLearnType == "0" && w.sScheduleID == dataSchedule.sScheduleID && w.sID == dataSchedule.sEmp).ToList();
                                    List<TLogLearnTimeScan> LogLearnEmployessTimeOut = tLogLearnTimeScanTeacher.Where(w => w.LogLearnType == "1" && w.sScheduleID == dataSchedule.sScheduleID && w.sID == dataSchedule.sEmp).ToList();

                                    var tTeacher = from a in tEmployees
                                                   join tb in LogLearnEmployessTimeIn on a.sEmp equals tb.sID into jtb
                                                   from Teacherin in jtb.DefaultIfEmpty()
                                                   join tc in LogLearnEmployessTimeOut on a.sEmp equals tc.sID into jtc
                                                   from Teacherout in jtc.DefaultIfEmpty()
                                                   where a.sEmp == dataSchedule.sEmp
                                                   select new
                                                   {
                                                       a.sName,
                                                       a.sLastname,
                                                       Status_IN = Teacherin == null ? "3" : Teacherin.LogLearnScanStatus,
                                                       Status_OUT = Teacherout == null ? "3" : Teacherout.LogLearnScanStatus,
                                                       Time_IN = Teacherin == null ? null : Teacherin.LogLearnTime,
                                                       Time_OUT = Teacherout == null ? null : Teacherout.LogLearnTime,
                                                   };

                                    JSonData.TeacherScan = new JArray(
                                     from a in tTeacher
                                     select new JObject(
                                         new JProperty("sName", a.sName + " " + a.sLastname),
                                         new JProperty("Status_IN", a.Status_IN.Trim()),
                                         new JProperty("Status_OUT", a.Status_OUT.Trim()),
                                         new JProperty("Time_IN", a.Time_IN),
                                         new JProperty("Time_OUT", a.Time_OUT)));
                                }
                                else
                                {
                                    JSonData.TeacherScan = new JArray();
                                }
                                Data.DataScan.Add(JSonData);
                            }
                            if (Data.DataScan.Count > 0) rss.Data.Add(Data);
                        }
                        #endregion
                        break;
                    case "repoststatus":
                        break;
                    case "reportindividualmain":
                        List<reportindividualmain> _reportindividualmain = new List<reportindividualmain>();
                        for (int i = 0; DayStart <= DayEnd.AddDays(-i); i++)
                        {
                            LogLearnDate = DayEnd.AddDays(-i);
                            int nDay = (int)LogLearnDate.DayOfWeek;

                            foreach (var dataSchedule in DataSchedule.Where(w => w.nPlaneDay == nDay).OrderBy(o => o.tStart))
                            {
                                if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                                List<TLogLearnTimeScan> LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0"
                                && w.sScheduleID == dataSchedule.sScheduleID && w.sID == studentId && w.sUserType == "0"
                                && w.LogLearnDate == LogLearnDate).ToList();
                                //List<TLogLearnTimeScan> LogLearnTimeOut = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "1"
                                //&& w.sScheduleID == dataSchedule.sScheduleID && w.sID == studentId && w.sUserType == "0"
                                //&& w.LogLearnDate == LogLearnDate).ToList();

                                int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0, Status_4 = 0, Status_5 = 0;
                                Status_0 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("0")).Count();
                                Status_1 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("1")).Count();
                                Status_2 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("3")).Count();
                                Status_3 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("4")).Count();
                                Status_4 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("5")).Count();
                                Status_5 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("6")).Count();

                                _reportindividualmain.Add(new reportindividualmain
                                {
                                    dScan = LogLearnDate.ToString("dd/MM/yyyy"),
                                    sScheduleID = dataSchedule.sScheduleID,
                                    sPlaneName = dataSchedule.sPlaneName,
                                    sPlaneID = dataSchedule.sPlaneID.ToString(),
                                    Status_0 = Status_0,
                                    Status_1 = Status_1,
                                    Status_2 = Status_2,
                                    Status_3 = Status_3,
                                    Status_4 = Status_4,
                                    Status_5 = Status_5,
                                });
                            }
                        }

                        rss = new JArray(from a in _reportindividualmain
                                         group a by new { a.sPlaneID, a.sPlaneName } into gb
                                         select new JObject(
                                             new JProperty("planename", gb.Key.sPlaneName),
                                             new JProperty("planeid", gb.Key.sPlaneID),
                                             new JProperty("Status_0", gb.Sum(sum => sum.Status_0)),
                                             new JProperty("Status_1", gb.Sum(sum => sum.Status_1)),
                                             new JProperty("Status_2", gb.Sum(sum => sum.Status_2)),
                                             new JProperty("Status_3", gb.Sum(sum => sum.Status_3)),
                                             new JProperty("Status_4", gb.Sum(sum => sum.Status_4)),
                                             new JProperty("Status_5", gb.Sum(sum => sum.Status_5))
                                         ));
                        break;
                    case "reportindividualdetail":
                        List<reportindividualmain> _reportdata04 = new List<reportindividualmain>();
                        var student = db.TUser.FirstOrDefault(f => f.sID == studentId);
                        for (int i = 0; DayStart <= DayEnd.AddDays(-i); i++)
                        {
                            LogLearnDate = DayEnd.AddDays(-i);
                            int nDay = (int)LogLearnDate.DayOfWeek;

                            foreach (var dataSchedule in DataSchedule.Where(w => w.nPlaneDay == nDay && w.sPlaneID.ToString() == PlaneId).OrderBy(o => o.tStart))
                            {
                                if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                                List<TLogLearnTimeScan> LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0"
                                && w.sScheduleID == dataSchedule.sScheduleID && w.sID == studentId && w.sUserType == "0"
                                && w.LogLearnDate == LogLearnDate && w.LogLearnScanStatus.Contains(strStatus ?? strStatus)).ToList();

                                int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0, Status_4 = 0, Status_5 = 0;
                                Status_0 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("0")).Count();
                                Status_1 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("1")).Count();
                                Status_2 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("3")).Count();
                                Status_3 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("4")).Count();
                                Status_4 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("5")).Count();
                                Status_5 = LogLearnTimeIn.Where(w => w.LogLearnScanStatus.Contains("6")).Count();

                                _reportdata04.Add(new reportindividualmain
                                {
                                    dScan = LogLearnDate.ToString("dd/MM/yyyy"),
                                    sScheduleID = dataSchedule.sScheduleID,
                                    sPlaneName = dataSchedule.sPlaneName,
                                    sPlaneID = dataSchedule.sPlaneID.ToString(),
                                    Status_0 = Status_0,
                                    Status_1 = Status_1,
                                    Status_2 = Status_2,
                                    Status_3 = Status_3,
                                    Status_4 = Status_4,
                                    Status_5 = Status_5,
                                    studentname = student.sName + " " + student.sLastname,
                                    timescan = dataSchedule.tStart.Value.ToString(@"hh\:mm")
                                });
                            }
                        }

                        if (strStatus == "0") _reportdata04 = _reportdata04.Where(w => w.Status_0 == 1).ToList();
                        if (strStatus == "1") _reportdata04 = _reportdata04.Where(w => w.Status_1 == 1).ToList();
                        if (strStatus == "3") _reportdata04 = _reportdata04.Where(w => w.Status_2 == 1).ToList();
                        if (strStatus == "4") _reportdata04 = _reportdata04.Where(w => w.Status_3 == 1).ToList();
                        if (strStatus == "5") _reportdata04 = _reportdata04.Where(w => w.Status_4 == 1).ToList();
                        if (strStatus == "6") _reportdata04 = _reportdata04.Where(w => w.Status_5 == 1).ToList();

                        rss = new JArray(from a in _reportdata04
                                         select new JObject(
                                             new JProperty("planename", a.sPlaneName),
                                             new JProperty("planeid", a.sPlaneID),
                                             new JProperty("studentname", a.studentname),
                                             new JProperty("dScan", a.dScan),
                                             new JProperty("timescan", a.timescan),
                                             new JProperty("Status_0", a.Status_0),
                                             new JProperty("Status_1", a.Status_1),
                                             new JProperty("Status_2", a.Status_2),
                                             new JProperty("Status_3", a.Status_3),
                                             new JProperty("Status_4", a.Status_4),
                                             new JProperty("Status_5", a.Status_5)
                                             ));
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        internal class reportindividualmain
        {
            public string dScan { get; set; }
            public string timescan { get; set; }
            public string studentname { get; set; }
            public string sPlaneName { get; set; }
            public string sPlaneID { get; set; }
            public int sScheduleID { get; set; }
            public int Status_0 { get; set; }
            public int Status_1 { get; set; }
            public int Status_2 { get; set; }
            public int Status_3 { get; set; }
            public int Status_4 { get; set; }
            public int Status_5 { get; set; }
        }
    }

    public class LearnScanning
    {
        public DateTime dScan { get; set; }
        public string sPlaneName { get; set; }
        public List<ListUser> lUser { get; set; }
    }

    public class ListUser
    {
        public string sName { get; set; }
        public string Status_IN { get; set; }
        public string Status_OUT { get; set; }
        public TimeSpan Time_IN { get; set; }
        public TimeSpan Time_OUT { get; set; }
    }
}