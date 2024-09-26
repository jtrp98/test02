using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Functions.Reports_03
{
    public class ReportsType_0303
    {
        public static List<ReportsData_03type03> getData(Search search, JabJaiEntities dbschool, DateTime dStart, DateTime dEnd, string nTemId, JWTToken.userData userData)
        {

            var q_class = QueryDataBases.SubLevel_Query.GetData(dbschool, userData);
            var q_level2 = QueryDataBases.SubLevel2_Query.GetData(dbschool, userData);

            StudentLogic logic = new StudentLogic(dbschool);
            string TermId = "";
            if (search.dStart.HasValue) TermId = logic.GetTermId(search.dStart.Value, userData);
            else if (!string.IsNullOrEmpty(search.term_id)) TermId = search.term_id;
            else TermId = logic.GetTermId(userData);

            var _company = dbschool.Database.SqlQuery<TCompany>("SELECT * FROM [JabjaiMasterSingleDB].[dbo].[TCompany] WHERE nCompany = " + userData.CompanyID).FirstOrDefault();

            var q_student = dbschool.TB_StudentViews.Where(w => (w.cDel ?? "0") != "1" && w.SchoolID == userData.CompanyID && w.nTerm == TermId
            && (w.moveInDate <= dEnd || w.moveInDate == null)).Select(s => new
            {
                s.sName,
                s.sLastname,
                s.nTermSubLevel2,
                s.sStudentID,
                s.sID,
                s.nStudentNumber,
                s.sStudentTitle,
                s.nStudentStatus,
                s.moveInDate,
                s.nTimeType
            }).AsQueryable().ToList();

            var q_logscan = new List<TLogUserTimeScan>();
            string User_Id = "";
            q_student.ForEach(f => { User_Id += (string.IsNullOrEmpty(User_Id) ? "" : ",") + f.sID; });

            string SQL = string.Format("SELECT * FROM TLogUserTimeScan WHERE SchoolID = {0}  AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = '0' ",
                userData.CompanyID, dStart, dEnd, User_Id);

            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());

            SQL = string.Format("SELECT * FROM [JabjaiSchoolHistory].[dbo].[TLogUserTimeScan] WHERE SchoolID = {0}  AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = '0' ",
                userData.CompanyID, dStart, dEnd, User_Id);
            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());

            var q_title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

            List<DateTimeLog> dateTimes = new List<DateTimeLog>();

            var q_HoliDay = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
            || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3" && w.SchoolID == userData.CompanyID).ToList();
            var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => (w.nTSubLevel == search.level2_id || w.nTSubLevel == search.level_id) && w.SchoolID == userData.CompanyID).ToList();

            if (DateTime.Now <= dEnd) //new condition by kong 22/06/2019
            {
                dEnd = DateTime.Now;
            }

            for (int i = 0; dStart.AddDays(i) <= dEnd; i++)
            {
                var LogDate = dStart.AddDays(i);
                int dayOfWeek = 0;
                switch (string.Format("{0:dddd}", LogDate))
                {
                    case "Monday": dayOfWeek = 0; break;
                    case "Tuesday": dayOfWeek = 1; break;
                    case "Wednesday": dayOfWeek = 2; break;
                    case "Thursday": dayOfWeek = 3; break;
                    case "Friday": dayOfWeek = 4; break;
                    case "Saturday": dayOfWeek = 5; break;
                    case "Sunday": dayOfWeek = 6; break;
                }
                dateTimes.Add(new DateTimeLog { dateTime = LogDate, dayOfWeek = dayOfWeek });
            }

            if (search.student_id.HasValue)
            {
                var f_student = q_student.FirstOrDefault(f => f.sID == search.student_id);
                search.level2_id = f_student.nTermSubLevel2;
            }

            dEnd = dEnd.AddDays(1);

            var f_setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
            int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);
            int ShowMinusSign1 = f_setting == null ? 1 : (f_setting.ShowMinusSign == 0 ? 1 : -1);
            DateTime? _dateHistoryStart = dStart, _dateHistoryEnd = dEnd;
            var f_term = new TTerm();
            if (search.sort_type != 3)
            {
                if (!string.IsNullOrEmpty(nTemId))
                {
                    f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm.Trim() == nTemId);
                }
                else
                {
                    f_term = dbschool.TTerms.FirstOrDefault(f => f.dStart <= dStart && f.dEnd >= dStart || f.dStart <= dEnd && f.dEnd >= dEnd && f.SchoolID == userData.CompanyID);
                }
                if (f_term != null)
                {
                    if (f_setting.Type == 1)
                    {
                        _dateHistoryStart = f_term.dStart;
                        _dateHistoryEnd = _dateHistoryEnd > f_term.dEnd ? f_term.dEnd : _dateHistoryEnd;

                    }
                    else
                    {
                        var q_term = dbschool.TTerms.Where(w => w.nYear == f_term.nYear && w.SchoolID == userData.CompanyID).ToList();
                        _dateHistoryStart = q_term.Min(m => m.dStart);
                        //_dateHistoryEnd = q_term.Max(m => m.dEnd);
                        _dateHistoryEnd = q_term.Max(m => m.dEnd) >= _dateHistoryEnd ? _dateHistoryEnd : f_term.dEnd;
                    }
                }
            }

            var history = dbschool.TBehaviorHistories
                .Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                //.Where(w => ((w.dAdd >= _dateHistoryStart && w.dAdd <= _dateHistoryEnd) || (w.LogDate >= _dateHistoryStart && w.LogDate < _dateHistoryEnd))
                .Where(w => ((w.LogDate ?? w.dAdd) >= _dateHistoryStart &&
                (w.LogDate ?? w.dAdd) < _dateHistoryEnd)
                && w.dCanCel == null && w.Type == "1" && w.SchoolID == userData.CompanyID)
                .ToList();

            var q_behavior = dbschool.TBehaviors.Where(w => w.SchoolID == userData.CompanyID && w.nAutoType.HasValue).ToList();
            var q_times = dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID).ToList();

            decimal ScoreAlert = ShowMinusSign * (f_setting.Alert ?? 0);

            var maxBehav = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).ToList();

            var q_Term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel != "1").ToList();

            var q_sID = q_student.Select(s => s.sID).ToList();
            var behaviorHistories = (from a in dbschool.TBehaviorHistories.Where(w => w.LogDate.HasValue && q_sID.Contains(w.StudentId ?? 0) && !w.dCanCel.HasValue && w.cDel == false)
                                     join b in dbschool.TBehaviors.Where(w => w.SchoolID == userData.CompanyID) on a.BehaviorId equals b.BehaviorId
                                     where a.dAdd >= dStart && a.dAdd <= dEnd
                                     select new
                                     {
                                         a.BehaviorId,
                                         a.BehaviorName,
                                         b.nAutoType,
                                         a.Score,
                                         a.LogDate,
                                         a.StudentId
                                     }).ToList();

            if (search.student_id.HasValue)
            {
                behaviorHistories = behaviorHistories.Where(w => w.StudentId == search.student_id).ToList();
            }

            var q = (from a in q_level2
                     join c in q_class on a.class_id equals c.class_id
                     where ((!search.level2_id.HasValue && !search.level_id.HasValue) ||
                     (!search.level2_id.HasValue && search.level_id == c.class_id) ||
                     (search.level2_id.HasValue && search.level2_id == a.classRoom_id))
                     group a by new { a.classRoom_name, c.class_name, a.classRoom_id } into gb
                     select new ReportsData_03type03
                     {
                         level2name = gb.Key.classRoom_name,
                         levelname = gb.Key.class_name,

                         studentDatas = (from a1 in q_student
                                             //join b1 in q_logscan on a1.sID equals b1.sID
                                         where a1.nTermSubLevel2 == gb.Key.classRoom_id
                                         orderby a1.nStudentNumber, a1.sStudentID
                                         //group b1 by new { a1.sName, a1.sLastname, a1.sStudentID, a1.sID } into gb2
                                         where (!search.student_id.HasValue || search.student_id == a1.sID)
                                         select new ReportsData_03type03.StudentData
                                         {
                                             Student_Name = geTitelName(q_title, a1.sStudentTitle) + " " + a1.sName + " " + a1.sLastname,
                                             Student_Id = a1.sStudentID,
                                             Id = a1.sID,
                                             StudentStatus = a1.nStudentStatus,
                                             scanDatas = (from b2 in dateTimes
                                                          join c2 in q_times.Where(w => w.nTimeType == a1.nTimeType) on b2.dayOfWeek equals c2.nDay
                                                          join a2 in q_logscan.Where(w => w.sID == a1.sID) on b2.dateTime equals a2.LogDate into ja2b2
                                                          from ja2 in ja2b2.DefaultIfEmpty()

                                                          join a3 in behaviorHistories on new { LogDate = b2.dateTime, StudentId = a1.sID } equals new { LogDate = a3.LogDate ?? new DateTime(1999, 1, 1), StudentId = a3.StudentId ?? 0 } into ja2a3
                                                          from ja3 in ja2a3.DefaultIfEmpty()

                                                          select new ReportsData_03type03.StudentData.ScanData
                                                          {
                                                              Scan_Date = b2.dateTime.ToString("dd/MM/yyyy", new CultureInfo("th-th")),
                                                              timeIn = ja2 == null ? "" : ja2.LogTime.ToString(),
                                                              Scan_Status = (a1.nStudentStatus ?? 0) == 2 || (a1.moveInDate.HasValue && a1.moveInDate > b2.dateTime) ? "8" : (ja2 == null ? get_LogtStatus(q_HoliDay, q_SomeHoliyDay, q_Term, b2.dateTime, c2, a1.nTermSubLevel2, _company.sotfware) : ja2.LogScanStatus),
                                                              Late = ja3 != null && ja3.nAutoType == 0 ? 1 : 0,
                                                              Absence = ja3 != null && ja3.nAutoType == 1 && ja3.BehaviorName == "ขาด" ? 1 : 0,
                                                              Absence_Half = ja3 != null && ja3.nAutoType == 1 && ja3.BehaviorName == "ขาดครึ่งวัน" ? 1 : 0,
                                                              Errand = ja3 != null && ja3.nAutoType == 2 ? 1 : 0,
                                                              Sick = ja3 != null && ja3.nAutoType == 3 ? 1 : 0,
                                                              UncheckName = ja3 != null && ja3.nAutoType == 4 ? 1 : 0,
                                                          })
                                              .Where(x => (x.Scan_Status == "1" || x.Scan_Status == "3" || x.Scan_Status == "10" || x.Scan_Status == "11" || x.Scan_Status == "8")) //add new by kong 22/06/2019
                                              .ToList(),

                                             behavAuto = GetbehaviorAuto(history, a1.sID, maxBehav, f_setting, q_behavior) * ShowMinusSign1,
                                             behavManual = GetbehaviorManual(history, a1.sID, maxBehav, f_setting, q_behavior) * ShowMinusSign1,
                                             ScoreAlert = ScoreAlert,
                                             behavAutoTotal = GetbehaviorAuto(history, a1.sID, maxBehav, f_setting, q_behavior) * ShowMinusSign1,
                                             behavManualTotal = GetbehaviorManual(history, a1.sID, maxBehav, f_setting, q_behavior) * ShowMinusSign1,
                                             //behaviorScore = GetbehaviorScore(history, a1.sID, maxBehav, f_setting) * ShowMinusSign
                                             //behaviorScore = (GetbehaviorAuto(history, a1.sID, maxBehav, f_setting) + GetbehaviorManual(history, a1.sID, maxBehav, f_setting)) * ShowMinusSign1

                                         })
                                        .Where(y => y.scanDatas.Count != 0) //add new by kong 22/06/2019
                                        .ToList()
                     }).ToList();

            return q;
        }

        private static decimal GetbehaviorAuto(List<TBehaviorHistory> histories, int StudentId, List<TBehaviorSetting> maxBehav, TBehaviorSetting f_setting, List<TBehavior> behavior)
        {
            var f_data = (from a in histories
                          join b in behavior on a.BehaviorId equals b.BehaviorId
                          where a.StudentId == StudentId && a.cDel == false
                          select a).ToList();

            var setting = maxBehav[0].MaxScore;
            //return f_data == null ? 100 : f_data.ResidualScore ?? 100;
            decimal ResidualScore = f_setting == null ? 0 : f_setting.MaxScore.Value;
            if (f_data != null)
            {
                ResidualScore = f_data.Sum(f => f.Score ?? 0);
            }
            return ResidualScore;
        }

        private static string get_LogtStatus(List<THoliday> q_HoliDay, List<THolidaySome> q_SomeHoliyDay, List<TTerm> terms, DateTime date, TTime time, int level_id, bool? sotfware)
        {
            var f_HoliDay = q_HoliDay.FirstOrDefault(f => date >= f.dHolidayStart && date <= f.dHolidayEnd);
            if (f_HoliDay != null && f_HoliDay.sHolidayType != "3")
            {
                if (f_HoliDay.sWhoSeeThis == "0" || f_HoliDay.sWhoSeeThis == "2")
                {
                    return "8";
                }
                else if (f_HoliDay.sWhoSeeThis == "4")
                {
                    var f1 = (from a in q_HoliDay
                              join b in q_SomeHoliyDay on a.nHoliday equals b.nHoliday
                              where date >= a.dHolidayStart && date <= a.dHolidayEnd
                               && b.nTSubLevel == level_id
                              select a).FirstOrDefault();

                    if (f1 != null)
                    {
                        return "8";
                    }

                }
                else if (f_HoliDay.sWhoSeeThis == "3")
                {
                    if (q_SomeHoliyDay.FirstOrDefault(f => f.nHoliday == f_HoliDay.nHoliday && f.nTSubLevel == level_id) != null)
                    {
                        return "8";
                    }
                }
            }

            if (time.cDel == "0") return "8";
            else if (terms.Count(c => c.dStart <= date && c.dEnd >= date) == 0) return "8";
            return (sotfware == false ? "99" : "3");
        }

        private static decimal GetbehaviorManual(List<TBehaviorHistory> histories, int StudentId, List<TBehaviorSetting> maxBehav, TBehaviorSetting f_setting, List<TBehavior> behavior)
        {
            var q1 = behavior.Select(s => s.BehaviorId).ToList();
            var f_data = histories.Where(f => f.StudentId == StudentId)
                .Where(f => !q1.Contains(f.BehaviorId ?? 0))
                .Sum(f => f.Score);
            var setting = maxBehav[0].MaxScore;
            //return f_data == null ? 100 : f_data.ResidualScore ?? 100;
            decimal ResidualScore = f_setting == null ? 0 : f_setting.MaxScore.Value;
            if (f_data != null)
            {
                ResidualScore = f_data.Value;
            }
            return ResidualScore;
        }

        private static decimal GetbehaviorScore(List<TBehaviorHistory> histories, int StudentId, List<TBehaviorSetting> maxBehav, TBehaviorSetting f_setting)
        {
            var f_data = histories.OrderByDescending(o => o.dAdd).FirstOrDefault(f => f.StudentId == StudentId);
            var setting = maxBehav[0].MaxScore;
            //return f_data == null ? 100 : f_data.ResidualScore ?? 100;
            decimal ResidualScore = f_setting == null ? 0 : f_setting.MaxScore.Value;
            if (f_data != null)
            {
                ResidualScore = f_data.ResidualScore.Value;
            }
            return ResidualScore;
        }

        private static string geTitelName(List<TTitleList> q_title, string titelId)
        {
            int title_Id = 0;
            int.TryParse(titelId, out title_Id);
            if (title_Id == 0) return titelId;
            else
            {
                var f_title = q_title.FirstOrDefault(f1 => f1.nTitleid == title_Id);
                return f_title == null ? titelId : f_title.titleDescription;
            }
        }
    }

    public class DateTimeLog
    {
        public DateTime dateTime { get; set; }
        public int dayOfWeek { get; set; }
    }
}