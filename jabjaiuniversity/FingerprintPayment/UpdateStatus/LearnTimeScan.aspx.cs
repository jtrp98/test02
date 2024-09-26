using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Handlers.AspNet;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using SchoolBright.DTO.Parameters;

namespace FingerprintPayment.UpdateStatus
{
    public partial class LearnTimeScan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                //fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = '" + userData.CompanyID + "' order by numberYear desc", "", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    //var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    //fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        private static void SetBeginSegment(AWSXRayRecorder recorder)
        {
            string url = HttpContext.Current.Request.Url.Host;

            var traceId = TraceId.NewId();
            recorder.BeginSegment(url, traceId);
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            AWSXRay xray = new AWSXRay();
            //xray.Init();

            AWSXRayRecorder recorder = xray.Register();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                recorder.AddAnnotation("Method", "LogLearn Scans Report");
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
                    var f_terms = new TTerm();

                    //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                    var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                    dStart = f_term.dStart.Value;
                    dEnd = f_term.dEnd.Value;
                    int RowsIndex = 1;

                    #region Report 01
                    Reports_01 reports_01 = new Reports_01();
                    List<HeaderReports> header = new List<HeaderReports>();

                    recorder.BeginSubsegment("Get Student Data");
                    List<int> StatusNoShow = new List<int> { 2 };

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

                    StudentLogTime studentLogTime = new StudentLogTime(dbschool, userData);
                    List<int> q_studentId = studentLogTime.GetStudents_PlanCourse(commonRequest, dbschool);
                    var outStatus = new int?[] { 1, 2, 3, 5, 6, 7 };
                    List<ReportsData> data = (from a in dbschool.TB_StudentViews
                                              .Where(w => w.SchoolID == userData.CompanyID
                                                && (w.cDel ?? "0") != "1"
                                                && w.nTermSubLevel2 == search.level2_id
                                                && w.nTerm.Trim() == search.term_id.Trim()
                                                && (
                                                   (((w.nStudentStatus ?? 0) == 0) && DbFunctions.TruncateTime(w.moveInDate ?? dEnd) <= dEnd)
                                                   || outStatus.Contains(w.nStudentStatus)
                                                //|| (w.nStudentStatus == 2)
                                                //|| (w.nStudentStatus == 1)
                                                //|| (w.nStudentStatus == 2 && dEnd < DbFunctions.TruncateTime(w.MoveOutDate ?? dEnd))
                                                // || (w.nStudentStatus == 1 && dEnd < DbFunctions.TruncateTime(w.MoveOutDate ?? dEnd)) 
                                                )
                                              ).AsEnumerable()

                                                  //from b in dbschool.TUsers.Where( o => o.sID == a.sID && o.SchoolID == a.SchoolID)
                                              orderby a.nStudentNumber, a.sStudentID

                                              select new ReportsData
                                              {
                                                  Student_Name = a.sName + " " + a.sLastname,
                                                  Student_Id = a.sStudentID,
                                                  Student_Number = a.nStudentNumber,
                                                  User_Id = a.sID,
                                                  Sum_Status_0 = 0,
                                                  Sum_Status_1 = 0,
                                                  Sum_Status_2 = 0,
                                                  Sum_Status_3 = 0,
                                                  Sum_Status_4 = 0,
                                                  Sum_Status_5 = 0,
                                                  Sum_Status_6 = 0,
                                                  TotalHour = 0,
                                                  scanDatas = new List<ScanData>(),

                                                  StudentStatus = a.nStudentStatus ?? 0,
                                                  MoveOutDate = a.MoveOutDate
                                              }).AsQueryable().ToList();

                    if (q_studentId != null && q_studentId.Count() > 0)
                    {
                        data = (from a in data
                                join b in q_studentId on a.User_Id equals b
                                select a).ToList();
                    }

                    data.ForEach(f => f.RowsIndex = RowsIndex++);

                    recorder.EndSubsegment();

                    recorder.BeginSubsegment("Get Schedules Data");
                    //var q_time = (from b in dbschool.TSchedules
                    //              join c in dbschool.TTermTimeTables on b.nTermTable equals c.nTermTable
                    //              where b.sPlaneID == search.plane_Id && c.nTermSubLevel2 == search.level2_id
                    //              && b.cDel == null && c.nTerm.Trim() == search.term_id.Trim()
                    //              select new
                    //              {
                    //                  b.sScheduleID,
                    //                  b.nPlaneDay,
                    //                  b.tStart,
                    //                  b.tEnd,
                    //              }).AsParallel();

                    string SQL = string.Format(@"SELECT   A.sScheduleID,A.nPlaneDay,A.tStart,A.tEnd,A.sPlaneID,
C.dStart, C.dEnd FROM TSchedule AS A
INNER JOIN TTermTimeTable AS B ON A.nTermTable = B.nTermTable
INNER JOIN TTerm AS C ON B.nTerm = C.nTerm
WHERE A.cDel IS NULL AND B.nTermSubLevel2 = {1} AND A.sPlaneID = '{0}' AND B.nTerm =  '{2}' AND A.SchoolID = {3} ", search.plane_Id, search.level2_id, search.term_id, userData.CompanyID);
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

                    var q_logscan = new List<LogScan>();
                    SQL = string.Format(@"SELECT sID,LogLearnScanStatus,LogLearnDate,LogLearnTime FROM TLogLearnTimeScan 
WHERE LogLearnDate BETWEEN '{0:MM/dd/yyyy}' AND '{1:MM/dd/yyyy}' {2} {3} AND SchoolID = {4} ", dStart, dEnd, _commUser, _commSchedule, userData.CompanyID);
                    q_logscan.AddRange(dbschool.Database.SqlQuery<LogScan>(SQL).ToList());

                    SQL = string.Format(@"SELECT sID,LogLearnScanStatus,LogLearnDate,LogLearnTime FROM [JabjaiSchoolHistory].[dbo].[TLogLearnTimeScan] 
WHERE LogLearnDate BETWEEN '{0:MM/dd/yyyy}' AND '{1:MM/dd/yyyy}' {2} {3} AND SchoolID = {4} ", dStart, dEnd, _commUser, _commSchedule, userData.CompanyID);
                    q_logscan.AddRange(dbschool.Database.SqlQuery<LogScan>(SQL).ToList());

                    recorder.EndSubsegment();

                    recorder.BeginSubsegment("Get LogLearn Scans Data");
                    var q_schedule = q_time.Select(s => s.sScheduleID).ToList();
                    var q_userId = data.AsParallel().Select(s => s.User_Id).ToList();
                    //var q_logscan = dbschool.TLogLearnTimeScans
                    //    .Where(w => w.sUserType.Trim() == "0" && q_schedule.Contains(w.sScheduleID) && q_userId.Contains(w.nUserID))
                    //    .Select(s => new LogScan
                    //    {
                    //        nUserID = s.nUserID,
                    //        LogLearnScanStatus = s.LogLearnScanStatus,
                    //        LogLearnDate = s.LogLearnDate,
                    //        LogLearnTime = s.LogLearnTime
                    //    }).AsParallel().WithDegreeOfParallelism(5).ToList();

                    var f_level = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);
                    //var q_time = dbschool.TTimes.Where(w => w.nTimeType == f_level.nTimeType).Select(s => new { s.nDay, s.cDel }).ToList();
                    recorder.EndSubsegment();
                    var arrWho = new string[] { "0", "2", "3", "4" };
                    var q_HoliDay = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID && arrWho.Contains(w.sWhoSeeThis))
                        .Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
                    || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3").ToList();
                    var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID)
                        .Where(w => w.nTSubLevel == search.level2_id)
                        .ToList();

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

                        //if (DateTime.Today < dStartMonth) continue;
                        for (int DaysIndex = 0; dStartMonth.AddDays(DaysIndex) <= dEndMonth; DaysIndex++)
                        {
                            DateTime LogLearnDate = dStartMonth.AddDays(DaysIndex);
                            recorder.BeginSubsegment("Add Data " + dStartMonth.AddDays(DaysIndex).ToString("dd/MM/yyyy"));
                            int DayOfWeek = (int)dStartMonth.AddDays(DaysIndex).DayOfWeek;
                            var f_Days = q_time.FirstOrDefault(f => f.nPlaneDay == DayOfWeek);
                            int TimeTotal = 0;
                            foreach (var q_data in q_time.Where(w => w.nPlaneDay == DayOfWeek && w.dStart <= dStartMonth.AddDays(DaysIndex) && w.dEnd >= dStartMonth.AddDays(DaysIndex)))
                            {
                                double SumTotal = (q_data.tEnd - q_data.tStart).TotalMinutes;
                                TimeTotal += SumTotal < 50 ? 1 : (Convert.ToInt32(SumTotal) / 50);
                            }
                            int? Schedule_Id = null;

                            if (f_Days != null) Schedule_Id = f_Days.sScheduleID;

                            bool HoliDay_Status = false;
                            string HoliDay_Name = "";

                            if (LogLearnDate <= dEnd)
                            {
                                bool bDay = (LogLearnDate <= DateTime.Today || dStart > LogLearnDate);
                                var q_logscanDay = q_logscan.Where(f => f.LogLearnDate == LogLearnDate).ToList();

                                var f_HoliDay = q_HoliDay.FirstOrDefault(f => LogLearnDate >= f.dHolidayStart && LogLearnDate <= f.dHolidayEnd);
                                if (f_HoliDay != null )
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
                                    Days_Name = LogLearnDate.Day.ToString(),
                                    Days_Status = f_Days != null || !HoliDay_Status ? "0" : "1",
                                    Session = f_Days == null || HoliDay_Status ? "" : (TimeTotal > 1 ? (SessionIndex) + " ~ " + ((SessionIndex + TimeTotal) - 1) : SessionIndex + ""),
                                    HoliDay_Name = HoliDay_Name,
                                    HoliDay_Status = HoliDay_Status,
                                    Days_string = LogLearnDate.ToString("MM/dd/yyyy")
                                });

                                //AddscanDatas(data, q_logscanDay, f_Days != null, dStartMonth.AddDays(DaysIndex), bDay, TimeTotal, Schedule_Id);
                                data.AsParallel().ForAll(f => AddscanDatas(f, q_logscanDay, f_Days != null && !HoliDay_Status, LogLearnDate, bDay, TimeTotal, Schedule_Id));

                                if (LogLearnDate.DayOfWeek == 0)
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
                        q_data.scanDatas = (from a in q_data.scanDatas
                                            orderby a.Year, a.DayOfYear
                                            select a).ToList();
                    }

                    reports_01.headerReports = header;
                    reports_01.reportsDatas = data;

                    return reports_01;
                    #endregion

                }
            }
        }

        [ScriptMethod()]
        [WebMethod]
        public static string SearchPlane(string term_id, int level_id)
        {

            //return Common.SearchPlane(term_id, level_id);

            //using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    var q3 = db.TPlanes.Where(w => w.cDel == null && w.nTSubLevel == level_id && w.courseStatus == 1).ToList();
            //    if (!string.IsNullOrEmpty(term_id))
            //    {
            //        term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
            //        var f_term = db.TTerms.FirstOrDefault(f => f.nTerm == term_id);
            //        if (f_term != null)
            //        {
            //            int? nTerm = int.Parse(f_term.sTerm);
            //            q3 = q3.Where(w => w.nTerm == nTerm).ToList();
            //        }
            //    }
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                //term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                var q3 = (from a in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                          join b in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals b.nTerm
                          join c in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                          join d in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on c.sPlaneID equals d.sPlaneID
                          join e in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals e.nTermSubLevel2
                          where e.nTermSubLevel2 == level_id && a.nTerm == term_id && c.cDel == null && d.cDel == null
                          group c by new { b.sTerm, b.nTerm, d.sPlaneID, d.courseCode, d.sPlaneName } into gb
                          select new
                          {
                              sPlaneID = gb.Key.sPlaneID,
                              courseCode = gb.Key.courseCode,
                              sPlaneName = gb.Key.sPlaneName,
                              sTerm = gb.Key.sTerm
                          }).ToList();

                dynamic rss = new JArray(from a in q3
                                         select new JObject {
                                                 new JProperty("value",a.sPlaneID),
                                                 new JProperty("text",a.courseCode + " - " + a.sPlaneName +" ( เทอม " + a.sTerm + " )"),
                                             });

                return rss.ToString();
            }


            //    dynamic rss = new JArray(from a in q3
            //                             select new JObject {
            //                                 new JProperty("value",a.sPlaneID),
            //                                 new JProperty("text",a.courseCode + " - " + a.sPlaneName +" ( เทอม " + a.nTerm + " )"),
            //                             });

            //    return rss.ToString();
            //}
        }

        private static async Task AddscanDatas(List<ReportsData> data, List<LogScan> q_logscanDay, bool Days_Status, DateTime Scan_Date, bool bDay, int TimeTotal, Nullable<int> Schedule_Id)
        {
            //if (Scan_Date > DateTime.Today) return;

            foreach (var Student_data in data.ToList())
            {
                var f_logscan = q_logscanDay.FirstOrDefault(f => f.sID == Student_data.User_Id);
                Student_data.scanDatas.Add(new ScanData
                {
                    Days_Status = Days_Status ? "0" : "1",
                    Scan_Date = Scan_Date.ToString("MM/dd/yyyy"),
                    Date = Scan_Date,
                    Scan_Status = f_logscan == null ? (bDay ? "3" : "") : f_logscan.LogLearnScanStatus,
                    Scan_Time = f_logscan == null ? "-" : f_logscan.LogLearnTime.HasValue ? f_logscan.LogLearnTime.Value.ToString(@"hh\:mm") : "-",
                    Schedule_Id = Schedule_Id,
                    DayOfYear = Scan_Date.DayOfYear,
                    Year = Scan_Date.Year
                });

                if (bDay)
                {
                    if (!Days_Status) { }
                    else if (f_logscan == null) Student_data.Sum_Status_1 += TimeTotal;
                    else
                    {
                        switch (f_logscan.LogLearnScanStatus.Trim())
                        {
                            case "0": Student_data.Sum_Status_0 += TimeTotal; break;
                            case "3": Student_data.Sum_Status_1 += TimeTotal; break;
                            case "4": Student_data.Sum_Status_3 += TimeTotal; break;
                            case "5": Student_data.Sum_Status_4 += TimeTotal; break;
                            case "6": Student_data.Sum_Status_5 += TimeTotal; break;
                            default: Student_data.Sum_Status_0 += TimeTotal; break;
                        }
                    }

                    //if (Scan_Date <= DateTime.Today && Days_Status) Student_data.TotalHour += TimeTotal;
                    //else { }
                }
            }
        }

        private static void AddscanDatas(ReportsData data, List<LogScan> q_logscanDay, bool Days_Status, DateTime Scan_Date, bool bDay, int TimeTotal, Nullable<int> Schedule_Id)
        {
            var f_logscan = q_logscanDay.FirstOrDefault(f => f.sID == data.User_Id);

            var arrOut = new List<int?>() { 1, 2, 3, 5, 6, 7 };

            if (arrOut.Contains(data.StudentStatus) && Scan_Date.Date >= (data.MoveOutDate ?? DateTime.Today).Date)
                return;

            data.scanDatas.Add(new ScanData
            {
                Days_Status = Days_Status ? "0" : "1",
                Scan_Date = Scan_Date.ToString("MM/dd/yyyy"),
                Date = Scan_Date,
                Scan_Status = f_logscan == null ? (bDay ? "3" : "") : f_logscan.LogLearnScanStatus,
                Scan_Time = f_logscan == null ? "-" : f_logscan.LogLearnTime.HasValue ? f_logscan.LogLearnTime.Value.ToString(@"hh\:mm") : "-",
                Schedule_Id = Schedule_Id,
                DayOfYear = Scan_Date.DayOfYear,
                Year = Scan_Date.Year
            });

            if (bDay)
            {
                if (!Days_Status) { }
                else if (f_logscan == null) data.Sum_Status_1 += TimeTotal;
                else
                {
                    switch (f_logscan.LogLearnScanStatus.Trim())
                    {
                        case "0": data.Sum_Status_0 += TimeTotal; break;
                        case "3": data.Sum_Status_1 += TimeTotal; break;
                        case "4": data.Sum_Status_3 += TimeTotal; break;
                        case "5": data.Sum_Status_4 += TimeTotal; break;
                        case "6": data.Sum_Status_5 += TimeTotal; break;
                        default: data.Sum_Status_0 += TimeTotal; break;
                    }
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

        [ScriptMethod()]
        [WebMethod]
        public static string updateData(updateScanData updateScanData)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var user_id = HttpContext.Current.Session["sEmpID"].ToString();
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read)))
            {
                try
                {
                    //updateScanData.term_id = string.Format("TS{0:0000000}", int.Parse(updateScanData.term_id));
                    var q_schedule = (from a in dbschool.TTermTimeTables
                                      join b in dbschool.TSchedules on a.nTermTable equals b.nTermTable
                                      join c in dbschool.TPlanes on b.sPlaneID equals c.sPlaneID
                                      where a.nTerm.Trim() == updateScanData.term_id.Trim() && a.nTermSubLevel2 == updateScanData.level2_id
                                      && c.sPlaneID.ToString() == updateScanData.plane_id && b.cDel == null
                                      select new
                                      {
                                          b.sScheduleID,
                                          b.nPlaneDay,
                                          c.sPlaneName
                                      }).ToList();

                    var q_term = (from a in dbschool.TTerms
                                  where a.nTerm.Trim() == updateScanData.term_id.Trim() && a.cDel == null
                                  select new
                                  {
                                      a.dStart,
                                      a.dEnd
                                  }).FirstOrDefault();

                    if (q_term == null) return "fail";
                    var l_schedule = q_schedule.Select(s => s.sScheduleID).ToList();
                    //var q_log = (from a in dbschool.TLogLearnTimeScans
                    //             join b in dbschool.TB_StudentViews on a.sID equals b.sID
                    //             where l_schedule.Contains(a.sScheduleID) && b.nTermSubLevel2 == updateScanData.level2_id
                    //             && b.nTerm.Trim() == updateScanData.term_id
                    //             select a).ToList();

                    int i = 0;
                    TimeSpan LogLearnTime = new TimeSpan(0, 0, 0);

                    List<SP_TLogLearnTimeScan> sp_TLogLearnTimeScans = new List<SP_TLogLearnTimeScan>();
                    for (i = 0; q_term.dStart.Value.AddDays(i) <= q_term.dEnd; i++)
                    {
                        var add_log = new List<TLogLearnTimeScan>();
                        int? nTeacherId = null;
                        DateTime LogLearnDate = q_term.dStart.Value.AddDays(i);
                        int nPlaneDay = (int)LogLearnDate.DayOfWeek;
                        int sScheduleID = 0;
                        if (LogLearnDate == DateTime.Today)
                        {
                            nTeacherId = int.Parse(user_id);
                        }

                        foreach (var log_data in q_schedule.Where(w => w.nPlaneDay == nPlaneDay))
                        {
                            foreach (var student_data in updateScanData.updateUsers)
                            {
                                SP_TLogLearnTimeScan sP_TLog = new SP_TLogLearnTimeScan();

                                //var f_log = q_log.FirstOrDefault(w => w.LogLearnDate == LogLearnDate && w.sScheduleID == log_data.sScheduleID && w.sID == student_data.student_id);
                                var f_update = student_data.updateScanLogs.FirstOrDefault(w => w.date_log == LogLearnDate);
                                if (f_update != null)
                                {
                                    sP_TLog.USERID = student_data.student_id;
                                    sP_TLog.SCHOOLID = userData.CompanyID;
                                    sP_TLog.SCHEDULEID = log_data.sScheduleID;
                                    sP_TLog.LOGSTATUS = f_update.status_log;
                                    sP_TLog.LOGTYPE = "0";
                                    sP_TLog.LOGDATE = LogLearnDate.ToString("MM/dd/yyyy");
                                    sP_TLog.LOGTIME = LogLearnTime.ToString(@"hh\:mm\:ss");
                                    sP_TLog.TEACHERID = nTeacherId;
                                    sP_TLog.USERUPDATE = userData.UserID;
                                    sP_TLog.NDAY = nPlaneDay;

                                    sp_TLogLearnTimeScans.Add(sP_TLog);
                                }
                            }
                        }
                    }

                    if (sp_TLogLearnTimeScans.Count > 0)
                    {
                        string JSon_DATA = fcommon.EntityToJson(sp_TLogLearnTimeScans);
                        dbschool.UpdateLogLearnStatus_v3(DateTime.Now, JSon_DATA);
                    }

                    var f_plane = dbschool.TPlanes.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.sPlaneID.ToString() == updateScanData.plane_id);
                    var f_class = QueryDataBases.SubLevel2_Query.GetRoom(dbschool, updateScanData.level2_id, userData);
                    database.InsertLog(user_id, "แก้ไขเช็คชื่อย้อนหลังรายวิชา " + f_plane.sPlaneName + " ห้อง " + f_class.classRoom_name
                          , entities, HttpContext.Current.Request, 1, SystemAction.Edit, SystemType.WebSite);
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }

                return "Success";
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
            public string Days_string { get; set; }
            public int Year { get; set; }
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
            public int? StudentStatus { get; internal set; }
            public DateTime? MoveOutDate { get; internal set; }
        }

        public class ScanData
        {
            public string Scan_Status { get; set; }
            public string Scan_Time { get; set; }
            public string Scan_Date { get; set; }
            public string Days_Status { get; set; }
            public DateTime Date { get; set; }
            public Nullable<int> Schedule_Id { get; set; }
            public int DayOfYear { get; set; }
            public int Year { get; set; }
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
        }


        public class updateScanData
        {
            public string term_id { get; set; }
            public int level2_id { get; set; }
            public string plane_id { get; set; }
            public List<updateUser> updateUsers { get; set; }
        }

        public class updateUser
        {
            public int student_id { get; set; }
            public List<updateScanLog> updateScanLogs { get; set; }
        }

        public class updateScanLog
        {
            public string status_log { get; set; }
            public DateTime date_log { get; set; }
            public int schedule_id { get; set; }
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

        public class SP_TLogLearnTimeScan
        {
            public string LOGSTATUS { get; set; }
            public string LOGTYPE { get; set; }
            public int USERID { get; set; }
            public int USERUPDATE { get; set; }
            public int SCHEDULEID { get; set; }
            public int SCHOOLID { get; set; }
            public string LOGDATE { get; set; }
            public string LOGTIME { get; set; }
            public int? TEACHERID { get; set; }
            public int? NDAY { get; set; }
        }
    }
}