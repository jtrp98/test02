using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class Reportmobile04 : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                var currentYear = Convert.ToInt32(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                var userData = GetUserData();
                fcommon.ListDBToDropDownList(_conn, ddlyear, $"select * from TYear where SchoolID = {userData.CompanyID} and numberYear <= {currentYear}  order by numberYear desc", "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string returnlist(Search search)
        {
            var userData = GetUserData();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;

                    if (search.sort_type == 0)
                    {
                        dStart = search.dStart.Value;
                        dEnd = search.dStart.Value.AddDays(1);
                    }
                    else if (search.sort_type == 1)
                    {
                        dStart = search.dStart.Value;
                        dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                        var f_terms = dbschool.TTerms
                            .Where(w => w.SchoolID == userData.CompanyID && (dStart >= w.dStart && dStart <= w.dEnd || dEnd >= w.dStart && dEnd <= w.dEnd))
                            .FirstOrDefault();
                        search.term_id = f_terms.nTerm;
                    }
                    else if (search.sort_type == 2)
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        dStart = f_term.dStart.Value;
                        dEnd = f_term.dEnd.Value;
                    }
                    else
                    {
                        dStart = search.dStart.Value;
                        dEnd = search.dEnd is null ? search.dStart.Value.AddDays(1) : search.dEnd.Value.AddDays(1);
                    }

                    StudentLogic logic = new StudentLogic(dbschool);
                    string TermId = string.IsNullOrEmpty(search.term_id) ? logic.GetTermId(dStart, userData) : search.term_id;

                    int RowsIndex = 1;
                    if (search.sort_type > 0)
                    {
                        #region Report 01
                        Reports_01 reports_01 = new Reports_01();
                        List<HeaderReports> header = new List<HeaderReports>();
                        var roomdata2 = dbschool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);
                        var qlevel2 = QueryDataBases.SubLevel2_Query.GetData(dbschool, roomdata2.nTSubLevel, userData);

                        StudentLogTime studentLogTime = new StudentLogTime(dbschool, userData);
                        StudentLogic studentLogic = new StudentLogic(dbschool);
                        var f_term = studentLogic.GetTermDATA(dStart, userData);

                        int sPlaneID = int.Parse(search.plane_Id ?? "0");
                        CommonRequest commonRequest = new CommonRequest()
                        {
                            nTerm = f_term.nTerm,
                            nTermSubLevel2 = search.level2_id,
                            nTSubLevel = search.level_id,
                            sPlaneID = sPlaneID,
                            SchoolId = userData.CompanyID,
                            nYear = f_term.nYear,
                            //PlanCourseId = f_planCourse.PlanCourseId
                        };

                        List<int> q_studentId = studentLogTime.GetStudents_PlanCourse(commonRequest, dbschool);
                        var q1 = (from a1 in (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                              join b in dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                              where (a.cDel ?? "0") != "1" && b.nTermSubLevel2 == search.level2_id && b.nTerm.Trim() == TermId.Trim()
                                              orderby a.nStudentNumber
                                              select new
                                              {
                                                  a.sID,
                                                  a.sName,
                                                  a.sLastname,
                                                  b.nStudentStatus,
                                                  b.sStudentID,
                                                  a.DayQuit,
                                                  a.Note,
                                                  b.nStudentNumber
                                              }).AsQueryable().ToList()
                                  select new StudentList
                                  {
                                      StudentId = a1.sID,
                                      sName = a1.sName,
                                      sLastname = a1.sLastname,
                                      nStudentStatus = a1.nStudentStatus == 2 ? "Q" : "",
                                      sStudentID = a1.sStudentID,
                                      DayQuit = a1.DayQuit,
                                      DayIn = 0,//a1.DayQuit.HasValue ? (a1.DayQuit.Value.DayOfYear > dStart.DayOfYear ? a1.DayQuit.Value.DayOfYear - dStart.DayOfYear : 0) : 0,
                                      DayOut = a1.DayQuit.HasValue ? (a1.DayQuit.Value.DayOfYear > dStart.DayOfYear ? a1.DayQuit.Value.DayOfYear - dStart.DayOfYear : 0) : 0,
                                      NoteIn = "",
                                      NoteOut = a1.Note,
                                      nStudentNumber = a1.nStudentNumber
                                  }).ToList();

                        if (q_studentId != null && q_studentId.Count() > 0)
                        {
                            q1 = (from a in q1
                                  join b in q_studentId on a.StudentId equals b
                                  select a).ToList();
                        }

                        var q2 = (from a1 in (from a in dbschool.TRoomChanges.Where(w => w.SchoolID == userData.CompanyID)
                                              join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                              where a.DayChange > dStart && a.DayChange <= dEnd && (a.Level2New == search.level2_id || a.Level2Old == search.level2_id)
                                              select new
                                              {
                                                  b.sID,
                                                  b.sName,
                                                  b.sLastname,
                                                  b.sStudentID,
                                                  a.Level2New,
                                                  a.Level2Old,
                                                  a.DayChange,
                                                  b.nStudentNumber
                                              }).ToList()

                                  select new StudentList
                                  {
                                      StudentId = a1.sID,
                                      sName = a1.sName,
                                      sLastname = a1.sLastname,
                                      nStudentStatus = "C",
                                      sStudentID = a1.sStudentID,
                                      DayIn = a1.Level2New == search.level2_id ? a1.DayChange.Value.DayOfYear - dStart.DayOfYear : 0,
                                      DayOut = a1.Level2Old == search.level2_id ? a1.DayChange.Value.DayOfYear - dStart.DayOfYear : 0,
                                      NoteIn = a1.Level2New == search.level2_id ? GetClassName(qlevel2, a1.Level2Old) : "",
                                      NoteOut = a1.Level2Old == search.level2_id ? GetClassName(qlevel2, a1.Level2New) : "",
                                      nStudentNumber = a1.nStudentNumber
                                  }).ToList();

                        List<StudentList> q3 = new List<StudentList>();
                        foreach (var dataIn in q1)
                        {
                            var f1 = q2.FirstOrDefault(f => f.StudentId == dataIn.StudentId);
                            if (f1 == null) q3.Add(dataIn);
                            else q3.Add(f1);
                        }

                        foreach (var dataIn in q2)
                        {
                            var f1 = q1.FirstOrDefault(f => f.StudentId == dataIn.StudentId);
                            if (f1 == null) q3.Add(dataIn);
                        }

                        var data = (from a in q3
                                    orderby a.nStudentNumber, a.sStudentID.Length, a.sStudentID
                                    select new ReportsData
                                    {
                                        Student_Name = a.sName + " " + a.sLastname,
                                        Student_Id = a.sStudentID,
                                        Student_Number = a.nStudentNumber,
                                        Student_Status = a.nStudentStatus,
                                        DayStart = a.DayIn,
                                        DayEnd = a.DayOut,
                                        NoteIn = a.NoteIn,
                                        NoteOut = a.NoteOut,
                                        User_Id = a.StudentId,
                                        Sum_Status_0 = 0,
                                        Sum_Status_1 = 0,
                                        Sum_Status_2 = 0,
                                        Sum_Status_3 = 0,
                                        Sum_Status_4 = 0,
                                        Sum_Status_5 = 0,
                                        Sum_Status_6 = 0,
                                        TotalHour = 0,
                                    }).ToList();

                        data.ForEach(f =>
                        {
                            f.RowsIndex = RowsIndex++;
                            f.scanDatas = new List<ScanData>();
                        });

                        //var q_time = (from b in dbschool.TSchedules
                        //              join c in dbschool.TTermTimeTables on b.nTermTable equals c.nTermTable
                        //              join d in dbschool.TTerms on c.nTerm equals d.nTerm.Trim()
                        //              where b.sPlaneID == search.plane_Id && c.nTermSubLevel2 == search.level2_id
                        //              && b.cDel == null  //&& c.nTerm == Term
                        //              select new
                        //              {
                        //                  b.sScheduleID,
                        //                  b.nPlaneDay,
                        //                  b.tStart,
                        //                  b.tEnd,
                        //                  d.dStart,
                        //                  d.dEnd
                        //              }).ToList();


                        //var q_logscan = (from a in data
                        //                 join b in dbschool.TLogLearnTimeScans on a.User_Id equals b.nUserID
                        //                 join c in q_time on b.sScheduleID equals c.sScheduleID
                        //                 where dStart <= b.LogLearnDate && dEnd >= b.LogLearnDate && b.sUserType.Trim() == "0"
                        //                 select new LogScan
                        //                 {
                        //                     nUserID = b.nUserID,
                        //                     LogLearnScanStatus = b.LogLearnScanStatus,
                        //                     LogLearnDate = b.LogLearnDate,
                        //                     LogLearnTime = b.LogLearnTime,
                        //                     Schedule_Id = c.sScheduleID
                        //                 }).ToList();

                        string SQL = string.Format(@"SELECT  A.sScheduleID,A.nPlaneDay,A.tStart,A.tEnd,A.sPlaneID,
C.dStart, C.dEnd FROM TSchedule AS A
INNER JOIN TTermTimeTable AS B ON A.nTermTable = B.nTermTable
INNER JOIN TTerm AS C ON B.nTerm = C.nTerm
WHERE A.cDel IS NULL AND B.nTermSubLevel2 = {1} AND A.sPlaneID = '{0}' AND A.SchoolID = {2} ", search.plane_Id, search.level2_id, userData.CompanyID);
                        var q_time = dbschool.Database.SqlQuery<TM_Schedule>(SQL).ToList();

                        string _commUser = "";
                        string _commSchedule = "";

                        data.ForEach(f =>
                        {
                            _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.User_Id + "'";
                        });
                        q_time.ForEach(f =>
                        {
                            _commSchedule += (string.IsNullOrEmpty(_commSchedule) ? "" : ",") + f.sScheduleID;
                        });


                        if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";
                        if (!string.IsNullOrEmpty(_commSchedule)) _commSchedule = " AND sScheduleID  IN (" + _commSchedule + ")";

                        SQL = string.Format(@"SELECT * FROM TLogLearnTimeScan 
WHERE  SchoolID = {0} AND LogLearnDate BETWEEN '{1:MM/dd/yyyy}' AND '{2:MM/dd/yyyy}' {3} {4}", userData.CompanyID, dStart, dEnd, _commUser, _commSchedule);
                        var q_logscan = new List<LogScan>();
                        q_logscan.AddRange(dbschool.Database.SqlQuery<LogScan>(SQL).ToList());

                        SQL = string.Format(@"SELECT * FROM JabjaiSchoolHistory.dbo.TLogLearnTimeScan 
WHERE SchoolID = {0} AND LogLearnDate BETWEEN '{1:MM/dd/yyyy}' AND '{2:MM/dd/yyyy}' {3} {4}", userData.CompanyID, dStart, dEnd, _commUser, _commSchedule);
                        q_logscan.AddRange(dbschool.Database.SqlQuery<LogScan>(SQL).ToList());

                        var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                        if (dStart <= oldDate)
                        {
                            SQL = string.Format(@"SELECT * FROM JabjaiSchoolHistory.dbo.TLogLearnTimeScan_Backup
WHERE SchoolID = {0} AND LogLearnDate BETWEEN '{1:MM/dd/yyyy}' AND '{2:MM/dd/yyyy}' {3} {4}", userData.CompanyID, dStart, dEnd, _commUser, _commSchedule);
                            q_logscan.AddRange(dbschool.Database.SqlQuery<LogScan>(SQL).ToList());
                        }

                        var f_level = dbschool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);
                        //var q_time = dbschool.TTimes.Where(w => w.nTimeType == f_level.nTimeType).Select(s => new { s.nDay, s.cDel }).ToList();
                        var arrWho = new string[] { "0", "2", "3", "4" };
                        var q_HoliDay = dbschool.THolidays
                            .Where(w => w.SchoolID == userData.CompanyID && arrWho.Contains(w.sWhoSeeThis))
                            .Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
                        || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3" && w.SchoolID == userData.CompanyID).ToList();
                        var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => w.nTSubLevel == search.level2_id && w.SchoolID == userData.CompanyID).ToList();

                        int WeeksIndex = 1, SessionIndex = 1;
                        for (int MonthsIndex = 0; dStart.AddMonths(MonthsIndex) <= dEnd; MonthsIndex++)
                        {
                            DateTime dStartMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us"));
                            DateTime dEndMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us")).AddMonths(1).AddDays(-1);
                            List<Weeks> weeks = new List<Weeks>();
                            List<Days> days = new List<Days>();
                            if (dStart > dStartMonth && MonthsIndex == 0) dStartMonth = dStart;
                            if (dEndMonth > dEnd) dEndMonth = dEnd;
                            else { }

                            for (int DaysIndex = 0; dStartMonth.AddDays(DaysIndex) <= dEndMonth; DaysIndex++)
                            {
                                DateTime LogLearnDate = dStartMonth.AddDays(DaysIndex);
                                int DayOfWeek = (int)dStartMonth.AddDays(DaysIndex).DayOfWeek;
                                var f_Days = q_time.OrderBy(o => o.tStart).FirstOrDefault(f => f.nPlaneDay == DayOfWeek && f.dStart <= dStartMonth.AddDays(DaysIndex) && f.dEnd >= dStartMonth.AddDays(DaysIndex));

                                int TimeTotal = 0;
                                foreach (var q_data in q_time.Where(w => w.nPlaneDay == DayOfWeek && w.dStart <= dStartMonth.AddDays(DaysIndex) && w.dEnd >= dStartMonth.AddDays(DaysIndex)))
                                {
                                    double SumTotal = (q_data.tEnd - q_data.tStart).TotalMinutes;
                                    TimeTotal += SumTotal < 50 ? 1 : (Convert.ToInt32(SumTotal) / 50);
                                }

                                bool HoliDay_Status = false;
                                string HoliDay_Name = "";
                                var f_HoliDay = q_HoliDay.FirstOrDefault(f => dStartMonth.AddDays(DaysIndex) >= f.dHolidayStart && dStartMonth.AddDays(DaysIndex) <= f.dHolidayEnd);
                                if (f_HoliDay != null)
                                {
                                    if (f_HoliDay.sWhoSeeThis == "0" || f_HoliDay.sWhoSeeThis == "2")
                                    {
                                        HoliDay_Status = true;
                                        HoliDay_Name = f_HoliDay.sHoliday;
                                    }
                                    else if (f_HoliDay.sWhoSeeThis != "1")
                                    {
                                        if (q_SomeHoliyDay.FirstOrDefault(f => f.nHoliday == f_HoliDay.nHoliday && f.nTSubLevel == search.level2_id) != null)
                                        {
                                            HoliDay_Status = true;
                                            HoliDay_Name = f_HoliDay.sHoliday;
                                        }
                                    }
                                }

                                days.Add(new Days
                                {
                                    Days_Id = DayOfWeek,
                                    Days_Name = dStartMonth.AddDays(DaysIndex).Day.ToString(),
                                    Days_Status = f_Days != null || !HoliDay_Status ? "0" : "1",
                                    Session = f_Days == null || HoliDay_Status ? "" : (TimeTotal > 1 ? (SessionIndex) + " ~ " + ((SessionIndex + TimeTotal) - 1) : SessionIndex + ""),
                                    HoliDay_Name = HoliDay_Name,
                                    HoliDay_Status = HoliDay_Status,
                                });

                                bool bDay = (dStartMonth.AddDays(DaysIndex).Date <= DateTime.Today || dStart > dStartMonth.AddDays(DaysIndex).Date);
                                var q_logscanDay = q_logscan.Where(f => f.LogLearnDate == LogLearnDate).ToList();

                                //Thread thread = new Thread(async delegate ()
                                //{
                                AddscanDatas(data, q_logscanDay, f_Days != null && !HoliDay_Status, dStartMonth.AddDays(DaysIndex), bDay, TimeTotal);
                                //});

                                //thread.IsBackground = true;
                                //thread.Start();

                                if (dStartMonth.AddDays(DaysIndex).DayOfWeek == 0)
                                {
                                    weeks.Add(new Weeks
                                    {
                                        Weeks_Id = WeeksIndex,
                                        Weeks_Name = "",
                                        days = days
                                    });
                                    days = new List<Days>();
                                    WeeksIndex += 1;
                                }
                                else if (dStartMonth.AddDays(DaysIndex) == dEndMonth)
                                {
                                    weeks.Add(new Weeks
                                    {
                                        Weeks_Id = WeeksIndex,
                                        Weeks_Name = "",
                                        days = days
                                    });
                                    days = new List<Days>();
                                }

                                if (f_Days != null && !HoliDay_Status)
                                {
                                    if (TimeTotal > 1) SessionIndex = SessionIndex + TimeTotal;
                                    else SessionIndex += 1;
                                }
                            }

                            header.Add(new HeaderReports
                            {
                                Month_Id = MonthsIndex,
                                Month_Name = MonthName(dStart.AddMonths(MonthsIndex).Month) + " " + dStart.AddMonths(MonthsIndex).ToString("yyyy", new CultureInfo("th-th")),
                                weeks = weeks
                            });

                            if (MonthsIndex == 0) dStart = DateTime.ParseExact(dStart.ToString("MM/01/yyyy"), "MM/dd/yyyy", new CultureInfo("en-us"));
                        }

                        foreach (var q_data in data)
                        {
                            q_data.scanDatas = q_data.scanDatas.OrderBy(o => o.Date).ToList();
                        }

                        reports_01.headerReports = header;
                        reports_01.reportsDatas = data;

                        return fcommon.EntityToJson(reports_01);

                        #endregion
                    }
                    else
                    {
                        var q1 = (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                  join b1 in (from b in dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogDate == dStart && w.LogType.Trim() == "0")
                                              select new
                                              {
                                                  sID = b.sID.Value,
                                                  b.LogTime,
                                                  b.LogScanStatus
                                              }).AsQueryable().ToList()
                                  on new { User_Id = a.sID }
                                  equals new { User_Id = b1.sID } into jab1
                                  from jb1 in jab1.DefaultIfEmpty()

                                  join b2 in (from b in dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogDate == dStart && w.LogType.Trim() == "1")
                                              select new
                                              {
                                                  sID = b.sID.Value,
                                                  b.LogTime,
                                                  b.LogScanStatus
                                              }).AsQueryable().ToList()

                                  on new { User_Id = a.sID }
                                  equals new { User_Id = b2.sID } into jab2
                                  from jb2 in jab1.DefaultIfEmpty()

                                  where a.nTermSubLevel2 == search.level2_id && (a.cDel ?? "0") != "1"
                                  orderby a.nStudentNumber, a.sStudentID

                                  select new Reports_02
                                  {
                                      Student_Name = a.sName + " " + a.sLastname,
                                      Student_Id = a.sStudentID,
                                      ScanIn_Time = jb1 == null ? "-" : (jb1.LogTime.HasValue ? jb1.LogTime.Value.ToString(@"hh\:mm") : "-"),
                                      ScanOut_Time = jb2 == null ? "-" : (jb2.LogTime.HasValue ? jb2.LogTime.Value.ToString(@"hh\:mm") : "-"),
                                      ScanIn_Status = jb1 == null ? "3" : jb1.LogScanStatus.Trim(),
                                      ScanOut_Status = jb2 == null ? "3" : jb2.LogScanStatus.Trim(),
                                  }).ToList();

                        return fcommon.EntityToJson(q1);
                    }
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string SearchPlaneByMonth(int level_id, int month, int year)
        {
            var userData = GetUserData();
            var tterm = new TTerm();

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var date = DateTime.ParseExact($"1/{month}/{year}", "d/M/yyyy", new CultureInfo("en-US"));

                var current = db.TTerms
                        .Where(o => o.dStart <= date && date <= o.dEnd && o.SchoolID == userData.CompanyID && o.cDel != "1")
                    .FirstOrDefault();

                if (current == null)
                    current = db.TTerms
                        .Where(w => w.SchoolID == userData.CompanyID && w.cDel != "1")
                        .OrderBy(o => o.dEnd)
                        .FirstOrDefault(f => f.dStart >= date);

                if (current == null)
                    current = db.TTerms
                        .Where(w => w.SchoolID == userData.CompanyID && w.cDel != "1")
                        .OrderByDescending(o => o.dEnd)
                        .FirstOrDefault(f => f.dEnd <= date);

                tterm = current;

            }
            return SearchPlane(tterm?.nTerm, level_id);
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string SearchPlane(string term_id, int level_id)
        {
            //return Common.SearchPlane(term_id, level_id);
            var userData = GetUserData();

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                //term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                var q3 = (from a in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                          join b in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals b.nTerm
                          join c in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                          join d in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on c.sPlaneID equals d.sPlaneID
                          join e in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals e.nTermSubLevel2
                          where e.nTermSubLevel2 == level_id && a.nTerm == term_id && c.cDel == null
                          group c by new { b.sTerm, b.nTerm, d.sPlaneID, d.courseCode, d.sPlaneName } into gb
                          select new
                          {
                              sPlaneID = gb.Key.sPlaneID,
                              courseCode = gb.Key.courseCode,
                              sPlaneName = gb.Key.sPlaneName,
                              sTerm = gb.Key.sTerm
                          }).ToList();

                //var q3 = db.TPlanes.Where(w => w.cDel == null && w.nTSubLevel == level_id && w.courseStatus == 1).ToList();
                //if (!string.IsNullOrEmpty(term_id))
                //{
                //    term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                //    var f_term = db.TTerms.FirstOrDefault(f => f.nTerm == term_id);
                //    if (f_term != null)
                //    {
                //        int? nTerm = int.Parse(f_term.sTerm);
                //        q3 = q3.Where(w => w.nTerm == nTerm).ToList();
                //    }
                //}

                dynamic rss = new JArray(from a in q3
                                         select new JObject {
                                             new JProperty("value",a.sPlaneID),
                                             new JProperty("text",a.courseCode + " - " + a.sPlaneName +" ( เทอม " + a.sTerm + " )"),
                                         });

                return rss.ToString();
            }
        }

        private static string GetClassName(List<QueryDataBases.SubLevel2_Query.Classroom> classrooms, int? ClassRoomsId)
        {
            var f_data = classrooms.FirstOrDefault(f => f.classRoom_id == ClassRoomsId);
            if (f_data == null) return null;
            else return f_data.classRoom_name;
        }

        private static async Task AddscanDatas(List<ReportsData> data, List<LogScan> q_logscanDay, bool Days_Status, DateTime Scan_Date, bool bDay, int TimeTotal)
        {
            foreach (var Student_data in data.ToList())
            {
                var f_logscan = q_logscanDay.FirstOrDefault(f => f.sID == Student_data.User_Id);
                Student_data.scanDatas.Add(new ScanData
                {
                    Days_Status = Days_Status ? "0" : "1",
                    Scan_Date = Scan_Date.ToShortDateString(),
                    Date = Scan_Date,
                    Scan_Status = f_logscan == null ? (bDay ? "3" : "") : f_logscan.LogLearnScanStatus.Trim(),
                    Scan_Time = f_logscan == null ? "-" : f_logscan.LogLearnTime.HasValue ? f_logscan.LogLearnTime.Value.ToString(@"hh\:mm") : "-",
                });

                if (bDay)
                {
                    if (!Days_Status) { }
                    else if (f_logscan == null) Student_data.Sum_Status_1 += TimeTotal;
                    else if (f_logscan.LogLearnScanStatus.Trim() == "0") { Student_data.Sum_Status_0 += TimeTotal; }
                    else if (f_logscan.LogLearnScanStatus.Trim() == "3") Student_data.Sum_Status_1 += TimeTotal;
                    else if (f_logscan.LogLearnScanStatus.Trim() == "4") Student_data.Sum_Status_3 += TimeTotal;
                    else if (f_logscan.LogLearnScanStatus.Trim() == "5") Student_data.Sum_Status_4 += TimeTotal;
                    else if (f_logscan.LogLearnScanStatus.Trim() == "6") Student_data.Sum_Status_5 += TimeTotal;
                    else if (f_logscan.LogLearnScanStatus.Trim() == "9") { }
                    else { Student_data.Sum_Status_0 += TimeTotal; }

                    if (Scan_Date <= DateTime.Today && Days_Status) Student_data.TotalHour += TimeTotal;
                    else { }
                }
            }
        }

        private static string ShotDayName(int DayNumber)
        {
            switch (DayNumber)
            {
                case 0: return "อา.";
                case 1: return "จ.";
                case 2: return "อ.";
                case 3: return "พ.";
                case 4: return "พฤ.";
                case 5: return "ศ.";
                default: return "ส.";
            }
        }

        private static string MonthName(int DayNumber)
        {
            switch (DayNumber)
            {
                case 1: return "มกราคม";
                case 2: return "กุมภาพันธ์";
                case 3: return "มีนาคม";
                case 4: return "เมษายน";
                case 5: return "พฤษภาคม";
                case 6: return "มิถุนายน";
                case 7: return "กรกฎาคม";
                case 8: return "สิงหาคม";
                case 9: return "กันยายน";
                case 10: return "ตุลาคม";
                case 11: return "พฤศจิกายน";
                default: return "ธันวาคม";
            }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string term_id { get; set; }
            public int? level2_id { get; set; }
            public int? level_id { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public string plane_Id { get; set; }
        }


        public class Reports_01
        {
            public List<HeaderReports> headerReports { get; set; }
            public List<ReportsData> reportsDatas { get; set; }
        }

        public class HeaderReports
        {
            public string Month_Name { get; set; }
            public int Month_Id { get; set; }
            public List<Weeks> weeks { get; set; }
        }

        public class Weeks
        {
            public string Weeks_Name { get; set; }
            public int Weeks_Id { get; set; }
            public List<Days> days { get; set; }
        }

        public class Days
        {
            public string Days_Name { get; set; }
            public int Days_Id { get; set; }
            public string Session { get; set; }
            public string Days_Status { get; set; }
            public string HoliDay_Name { get; set; }
            public bool HoliDay_Status { get; set; }
        }

        public class ReportsData
        {
            public int RowsIndex { get; set; }
            public int User_Id { get; set; }
            public string Student_Id { get; set; }
            public string Student_Name { get; set; }
            public List<ScanData> scanDatas { get; set; }
            public int TotalHour { get; set; }
            public int Sum_Status_0 { get; set; }
            public int Sum_Status_1 { get; set; }
            public int Sum_Status_2 { get; set; }
            public int Sum_Status_3 { get; set; }
            public int Sum_Status_4 { get; set; }
            public int Sum_Status_5 { get; set; }
            public int Sum_Status_6 { get; set; }
            public int? Student_Number { get; set; }
            public string Student_Status { get; set; }
            public int DayStart { get; set; }
            public int DayEnd { get; set; }
            public string NoteIn { get; set; }
            public string NoteOut { get; set; }
        }

        public class ScanData
        {
            public string Scan_Status { get; set; }
            public string Scan_Time { get; set; }
            public string Scan_Date { get; set; }
            public string Days_Status { get; set; }
            public DateTime Date { get; set; }
        }

        public class Reports_02
        {
            public string Student_Name { get; set; }
            public string Student_Id { get; set; }
            public string ScanIn_Time { get; set; }
            public string ScanOut_Time { get; set; }
            public string ScanIn_Status { get; set; }
            public string ScanOut_Status { get; set; }
        }

        private class LogScan
        {
            internal int sID { get; set; }
            public string LogLearnScanStatus { get; set; }
            public DateTime LogLearnDate { get; set; }
            public TimeSpan? LogLearnTime { get; set; }
            public int Schedule_Id { get; set; }
        }

        public class TM_Schedule
        {
            public int sScheduleID { get; set; }
            public int nPlaneDay { get; set; }
            public TimeSpan tStart { get; set; }
            public TimeSpan tEnd { get; set; }
            public int sPlaneID { get; set; }
            public DateTime dStart { get; set; }
            public DateTime dEnd { get; set; }
        }
    }
}