using FingerprintPayment.Report.Models;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Functions.Reports_03
{
    public class ReportsType_03
    {
        public static List<ReportsData_03Models> getData(Search search, JabJaiEntities dbschool, DateTime dStart, DateTime dEnd, JWTToken.userData userData)
        {

            var q_class = QueryDataBases.SubLevel_Query.GetData(dbschool, userData);
            var q_level2 = QueryDataBases.SubLevel2_Query.GetData(dbschool, userData);
            var q_title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();

            var _company = dbschool.Database.SqlQuery<TCompany>("SELECT * FROM [JabjaiMasterSingleDB].[dbo].[TCompany] WHERE nCompany = " + userData.CompanyID).FirstOrDefault();

            string TermId = "";

            if (search.sort_type == 2) TermId = search.term_id;
            else
            {
                StudentLogic logic = new StudentLogic(dbschool);
                TermId = logic.GetTermId(search.dStart ?? DateTime.Today, userData);
            }

            int[] nStudentStatus = new int[] { 0, 4 };
            var q_student = (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                             join b in dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                             where (a.cDel ?? "0") != "1" && b.nTerm.Trim() == TermId.Trim() && (b.moveInDate <= dEnd || b.moveInDate == null)
                             && ((!search.level2_id.HasValue && b.nTSubLevel == search.level_id) || (search.level2_id.HasValue && search.level2_id == b.nTermSubLevel2))
                             && (nStudentStatus.Contains((b.nStudentStatus ?? 0)) || ((b.nStudentStatus ?? 0) == 2 && (b.MoveOutDate <= b.dStart || b.MoveOutDate == null)))
                             select new
                             {
                                 a.sName,
                                 a.sLastname,
                                 b.nTermSubLevel2,
                                 a.sStudentID,
                                 a.sID,
                                 a.nStudentNumber,
                                 a.sStudentTitle,
                                 b.nTimeType
                             }).AsQueryable().ToList();


            var q_logscan = new List<TLogUserTimeScan>();

            string User_Id = "";
            q_student.ForEach(f => { User_Id += (string.IsNullOrEmpty(User_Id) ? "" : ",") + f.sID; });

            string SQL = string.Format("SELECT * FROM TLogUserTimeScan WHERE SchoolID = {0} AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = 0",
                userData.CompanyID, dStart, dEnd, User_Id);

            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());

            SQL = string.Format("SELECT * FROM [JabjaiSchoolHistory].[dbo].[TLogUserTimeScan] WHERE SchoolID = {0} AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = 0",
                userData.CompanyID, dStart, dEnd, User_Id);
            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());

            List<DateTimeLog> dateTimes = new List<DateTimeLog>();

            if (DateTime.Now <= dEnd) //new condition by kong 22/06/2019
            {
                dEnd = DateTime.Now;
            }

            var q_HoliDay = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
            || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null && w.sHolidayType != "3" && w.SchoolID == userData.CompanyID).ToList();
            var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => (w.nTSubLevel == search.level2_id || w.nTSubLevel == search.level_id) && w.SchoolID == userData.CompanyID).ToList();

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
                var f_student = dbschool.TB_StudentViews.FirstOrDefault(f => f.nTerm == TermId.Trim() && f.sID == search.student_id);
                if (f_student != null)
                {
                    search.level2_id = f_student.nTermSubLevel2;
                }
                else
                {
                    var f_student2 = dbschool.TUser.FirstOrDefault(f => f.sID == search.student_id);
                    search.level2_id = f_student2.nTermSubLevel2;
                }
            }

            var q_times = dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var q_Term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel != "1").ToList();

            var q = (from a in q_level2
                     join c in q_class on a.class_id equals c.class_id
                     where ((!search.level2_id.HasValue && !search.level_id.HasValue) ||
                     (!search.level2_id.HasValue && search.level_id == c.class_id) ||
                     (search.level2_id.HasValue && search.level2_id == a.classRoom_id))
                     group a by new { a.classRoom_name, c.class_name, a.classRoom_id } into gb
                     select new ReportsData_03Models
                     {
                         level2name = gb.Key.classRoom_name,
                         levelname = gb.Key.class_name,

                         studentDatas = (from a1 in q_student
                                             //join b1 in q_logscan on a1.sID equals b1.sID
                                         where a1.nTermSubLevel2 == gb.Key.classRoom_id
                                         orderby a1.nStudentNumber, a1.sStudentID
                                         //group b1 by new { a1.sName, a1.sLastname, a1.sStudentID, a1.sID } into gb2
                                         where (!search.student_id.HasValue || search.student_id == a1.sID)
                                         select new ReportsData_03Models.StudentData
                                         {
                                             Student_Name = geTitelName(q_title, a1.sStudentTitle) + " " + a1.sName + " " + a1.sLastname,
                                             Student_Id = a1.sStudentID,
                                             Id = a1.sID,

                                             scanDatas = (from b2 in dateTimes
                                                          join c2 in q_times.Where(w => w.nTimeType == a1.nTimeType) on b2.dayOfWeek equals c2.nDay
                                                          join a2 in q_logscan.Where(w => w.sID == a1.sID) on b2.dateTime equals a2.LogDate into ja2b2
                                                          from ja2 in ja2b2.DefaultIfEmpty()

                                                          select new ReportsData_03Models.StudentData.ScanData
                                                          {
                                                              Scan_Date = b2.dateTime.ToString("dd/MM/yyyy", new CultureInfo("th-th")),
                                                              Scan_Status = get_LogtStatus(q_HoliDay, q_SomeHoliyDay, q_Term, b2.dateTime, c2, a1.nTermSubLevel2, ja2, _company.sotfware),
                                                              LogDay = b2.dateTime,
                                                              TimeIn = ja2 == null ? null : ja2.LogTime,
                                                              TimeLate = c2.dTimeEnd_IN.Value.TimeOfDay,
                                                          })
                                                          .Where(x => (x.Scan_Status == "1" || x.Scan_Status == "3" || x.Scan_Status == "10" || x.Scan_Status == "11")) //add new by kong 22/06/2019
                                                          .ToList()

                                         })
                                        .Where(y => y.scanDatas.Count != 0) //add new by kong 22/06/2019
                                        .ToList()
                     }).ToList();

            return q;
        }

        private static string get_LogtStatus(List<THoliday> q_HoliDay, List<THolidaySome> q_SomeHoliyDay, List<TTerm> terms, DateTime date, TTime time, int level_id, TLogUserTimeScan userTimeScan, bool? sotfware)
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
            else if (userTimeScan == null) return (sotfware ?? false ? "99" : "3");
            else return userTimeScan.LogScanStatus;

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
}