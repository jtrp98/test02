using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FingerprintPayment.Report.Models;
using JabjaiMainClass;
using JabjaiEntity.DB;
using System.Threading.Tasks;
using System.Globalization;

namespace FingerprintPayment.Report.Functions.Reports_03
{
    public class ReportsType_01
    {

        public static Reports_01 getData(Search search, JabJaiEntities dbschool, DateTime dStart, DateTime dEnd, bool RoomChanges, JWTToken.userData userData, bool reports)
        {

            #region Report 01
            Reports_01 reports_01 = new Reports_01();
            List<Reports_01.HeaderReports> header = new List<Reports_01.HeaderReports>();

            //if (DateTime.Now <= dEnd) //new condition by kong 22/06/2019
            //{
            //    dEnd = DateTime.Now;
            //}

            int RowsIndex = 1;
            var q_title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
            if (search.student_id.HasValue)
            {
                search.level2_id = null;
                search.level_id = null;
            }

            int[] nStudentStatus = new int[] { 0, 4, 2 };
            var outStatus = new int[] { 1, 2, 3, 5, 6, 7 };
            var q1 = (from a1 in (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                  join b1 in dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b1.sID

                                  join b3 in dbschool.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID) on new { sID = b1.sID, b1.nTerm } equals new { sID = b3.sID.Value, b3.nTerm } into jab2
                                  from jb2 in jab2.DefaultIfEmpty()

                                  where b1.nTerm.Trim() == search.term_id.Trim() && (b1.moveInDate <= dEnd || b1.moveInDate == null) //&& b1.nTermSubLevel2 == search.level2_id

                                  //where (a.cDel ?? "0") != "1" && (nStudentStatus.Contains((b1.nStudentStatus ?? 0)) || ((b1.nStudentStatus ?? 0) == 2 && (b1.MoveOutDate >= b1.dStart || b1.MoveOutDate == null))) &&
                                  where (a.cDel ?? "0") != "1" && /*(nStudentStatus.Contains((b1.nStudentStatus ?? 0))) &&*/
                                  (!search.level2_id.HasValue || b1.nTermSubLevel2 == search.level2_id) &&
                                  (!search.student_id.HasValue || search.student_id == a.sID)
                                  orderby b1.nStudentNumber
                                  select new
                                  {
                                      a.sID,
                                      a.sStudentTitle,
                                      b1.sName,
                                      b1.sLastname,
                                      b1.sStudentID,
                                      nStudentStatus = b1.nStudentStatus ?? 0,
                                      a.Note,
                                      b1.nStudentNumber,
                                      b1.moveInDate,
                                      b1.MoveOutDate,
                                      a.DayQuit,
                                      G_Day = jb2 == null ? null : jb2.DayAdd
                                  }).ToList()

                      select new StudentList
                      {
                          StudentId = a1.sID,
                          sName = geTitelName(q_title, a1.sStudentTitle) + " " + a1.sName,
                          sLastname = a1.sLastname,
                          nStudentStatus = outStatus.Contains(a1.nStudentStatus) ? "Q" : a1.nStudentStatus == 4 ? "G" : a1.nStudentStatus + "",
                          nStudentStatus2 = a1.nStudentStatus,
                          sStudentID = a1.sStudentID,
                          DayQuit = a1.MoveOutDate,
                          DayIn = 0,//a1.DayQuit.HasValue ? (a1.DayQuit.Value.DayOfYear > dStart.DayOfYear ? a1.DayQuit.Value.DayOfYear - dStart.DayOfYear : 0) : 0,
                          DayOut = a1.G_Day.HasValue ? (a1.G_Day.Value.DayOfYear > dStart.DayOfYear ? a1.G_Day.Value.DayOfYear - dStart.DayOfYear : 0) : a1.MoveOutDate.HasValue ? (a1.MoveOutDate.Value.DayOfYear > dStart.DayOfYear ? a1.MoveOutDate.Value.DayOfYear - dStart.DayOfYear : 0) : 0,
                          NoteOut = a1.G_Day.HasValue ? a1.G_Day.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : a1.Note,
                          nStudentNumber = a1.nStudentNumber,
                          moveInDate = a1.moveInDate,
                          MoveOutDate = a1.MoveOutDate,
                      }).ToList();

            if (search.student_id.HasValue && !search.level2_id.HasValue)
            {
                var stundetData = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.sID == search.student_id);
                search.level2_id = stundetData.nTermSubLevel2;
            }

            var roomdata2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);
            var qlevel2 = QueryDataBases.SubLevel2_Query.GetData(dbschool, userData);

            if (RoomChanges)
            {
                var q2 = (from a1 in (from a in dbschool.TRoomChanges.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                      join b in dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == search.term_id.Trim()) on a.sID equals b.sID
                                      where a.DayChange > dStart && a.DayChange <= dEnd && (a.Level2New == search.level2_id || a.Level2Old == search.level2_id) &&
                                      (!search.student_id.HasValue || search.student_id == a.sID)
                                      //&& !q12.Contains(a.sID ?? 0)

                                      select new
                                      {
                                          b.sID,
                                          b.sName,
                                          b.sStudentTitle,
                                          b.nStudentNumber,
                                          b.sLastname,
                                          b.sStudentID,
                                          a.Level2New,
                                          a.Level2Old,
                                          a.DayChange,
                                          b.moveInDate
                                      }).AsQueryable().ToList()

                          select new StudentList
                          {
                              moveInDate = a1.moveInDate,
                              StudentId = a1.sID,
                              sName = geTitelName(q_title, a1.sStudentTitle) + " " + a1.sName,
                              nStudentNumber = a1.nStudentNumber,
                              sLastname = a1.sLastname,
                              nStudentStatus = "C",
                              sStudentID = a1.sStudentID,
                              DayIn = a1.Level2New == search.level2_id ? a1.DayChange.Value.DayOfYear - dStart.DayOfYear : 0,
                              DayOut = a1.Level2Old == search.level2_id ? a1.DayChange.Value.DayOfYear - dStart.DayOfYear : 0,
                              NoteIn = a1.Level2New == search.level2_id ? GetClassroom(qlevel2, a1.Level2Old).classRoom_name : "",
                              NoteOut = a1.Level2Old == search.level2_id ? GetClassroom(qlevel2, a1.Level2New).classRoom_name : "",
                          }).ToList();
                var arrSid = q2.Select(o => o.StudentId);
                q1 = q1.Where(o => !arrSid.Contains(o.StudentId)).ToList();
                q1.AddRange(q2);
            }



            var data = (from a in q1
                        orderby a.nStudentNumber, a.sStudentID.Length, a.sStudentID
                        select new Reports_01.ReportsData
                        {
                            Student_Name = a.sName + " " + a.sLastname,
                            Student_Id = a.sStudentID,
                            Student_Status = a.nStudentStatus,
                            Student_Status2 = a.nStudentStatus2,
                            DayStart = a.DayIn,
                            DayEnd = a.DayOut,
                            NoteIn = a.NoteIn,
                            NoteOut = a.NoteOut,
                            User_Id = a.StudentId,
                            Student_Number = a.nStudentNumber,
                            Sum_Status_0 = 0,
                            Sum_Status_1 = 0,
                            Sum_Status_2 = 0,
                            Sum_Status_3 = 0,
                            Sum_Status_4 = 0,
                            Sum_Status_5 = 0,
                            Sum_Status_6 = 0,
                            moveInDate = a.moveInDate,
                            MoveOutDate = a.MoveOutDate
                        })
                        .OrderBy(o => o.Student_Number)
                        .ToList();

            data.ForEach(f => f.RowsIndex = RowsIndex++);
            data.ForEach(f => f.scanDatas = new List<Reports_01.ScanData>());

            var q_logscan = new List<TLogUserTimeScan>();
            string User_Id = "";
            data.ForEach(f => { User_Id += (string.IsNullOrEmpty(User_Id) ? "" : ",") + f.User_Id; });

            if (data != null && data.Count > 0)
            {
                string SQL = string.Format("SELECT * FROM TLogUserTimeScan WHERE SchoolID = {0} AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = 0",
                    userData.CompanyID, dStart, dEnd, User_Id);

                q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());

                SQL = string.Format("SELECT * FROM [JabjaiSchoolHistory].[dbo].[TLogUserTimeScan] WHERE SchoolID = {0} AND sID IN ({3}) AND LogDate BETWEEN '{1:d}' AND '{2:d}' AND LogType = 0",
                    userData.CompanyID, dStart, dEnd, User_Id);
                q_logscan.AddRange(dbschool.Database.SqlQuery<TLogUserTimeScan>(SQL).ToList());
            }

            var f_level = dbschool.TTermSubLevel2
                .Where(w => w.SchoolID == userData.CompanyID)
                .FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);

            var q_time = dbschool.TTimes
                .Where(w => w.SchoolID == userData.CompanyID)
                .AsEnumerable()
                .Where(w => w.nTimeType == f_level?.nTimeType)
                .Select(s => new { s.nDay, s.cDel })
                .ToList();
            int WeeksIndex = 1;

            var q_HoliDay = dbschool.THolidays
                .Where(w => w.SchoolID == userData.CompanyID)
                .Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
                    || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null
                    && w.sHolidayType != "3" //&& w.sWhoSeeThis != "5"
                    && w.SchoolID == userData.CompanyID)
                .ToList();

            var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => (w.nTSubLevel == search.level2_id || w.nTSubLevel == search.level_id) && w.SchoolID == userData.CompanyID).ToList();

            DateTime DayStartReport = dStart;
            DateTime DayEndReport = dEnd.AddDays(1);
            if (search.sort_type != 3)
            {
                DayStartReport = new DateTime(dStart.Year, dStart.Month, 1);
                DayEndReport = new DateTime(dEnd.Year, dEnd.Month, 1).AddDays(1);
            }
            else
            {
                if (dStart.Month == dEnd.Month)
                {
                    DayStartReport = dStart;
                    DayEndReport = dEnd;
                }
                else
                {
                    DayStartReport = new DateTime(dStart.Year, dStart.Month, 1);
                    DayEndReport = new DateTime(dEnd.Year, dEnd.Month, 1).AddDays(1);
                }
            }

            int Days_Id = 1;
            for (int MonthsIndex = 0; DayStartReport.AddMonths(MonthsIndex) < DayEndReport; MonthsIndex++)
            {
                DateTime dStartMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us"));
                DateTime dEndMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us")).AddMonths(1).AddDays(-1);
                List<Reports_01.Weeks> weeks = new List<Reports_01.Weeks>();
                List<Reports_01.Days> days = new List<Reports_01.Days>();
                if (dStart > dStartMonth && MonthsIndex == 0) dStartMonth = dStart;
                if (dEndMonth > dEnd) dEndMonth = dEnd;
                else { }
                if (DateTime.Today < dStartMonth) continue;

                //sWhoSeeThis
                //0 = ทุกคน
                //1 = เฉพาะอาจารย์/พนักงาน
                //2 = เฉพาะนักเรียน
                //3 = เฉพาะระดับชั้น
                //4 = เฉพาะห้อง
                for (int DaysIndex = 0; dStartMonth.AddDays(DaysIndex) <= dEndMonth; DaysIndex++)
                {
                    bool HoliDay_Status = false;
                    string HoliDay_Name = "";
                    var LogDate = dStartMonth.AddDays(DaysIndex);
                    int DayOfWeek = 0;
                    switch (string.Format("{0:dddd}", LogDate))
                    {
                        case "Monday": DayOfWeek = 0; break;
                        case "Tuesday": DayOfWeek = 1; break;
                        case "Wednesday": DayOfWeek = 2; break;
                        case "Thursday": DayOfWeek = 3; break;
                        case "Friday": DayOfWeek = 4; break;
                        case "Saturday": DayOfWeek = 5; break;
                        case "Sunday": DayOfWeek = 6; break;
                    }

                    var f_Days = q_time.FirstOrDefault(f => f.nDay == DayOfWeek);
                    var f_HoliDayList = q_HoliDay
                        .Where(f => dStartMonth.AddDays(DaysIndex) >= f.dHolidayStart && dStartMonth.AddDays(DaysIndex) <= f.dHolidayEnd)
                        .ToList();
                    foreach (var f_HoliDay in f_HoliDayList)
                    {
                        if (f_HoliDay != null && f_HoliDay.sHolidayType != "3")
                        {
                            if (f_HoliDay.sWhoSeeThis == "0" || f_HoliDay.sWhoSeeThis == "2")
                            {
                                HoliDay_Status = true;
                                HoliDay_Name = f_HoliDay.sHoliday;
                            }
                            else if (f_HoliDay.sWhoSeeThis == "4")
                            {
                                var f1 = (from a in q_HoliDay
                                          join b in q_SomeHoliyDay on a.nHoliday equals b.nHoliday
                                          where dStartMonth.AddDays(DaysIndex) >= a.dHolidayStart && dStartMonth.AddDays(DaysIndex) <= a.dHolidayEnd
                                           && b.nTSubLevel == search.level_id
                                          select a).FirstOrDefault();

                                if (f1 != null)
                                {
                                    HoliDay_Status = true;
                                    HoliDay_Name = f1.sHoliday;
                                }
                            }
                            else if (f_HoliDay.sWhoSeeThis == "3")
                            {
                                if (reports || (reports == false && (f_HoliDay.sHolidayType == "0" || (f_HoliDay.sHolidayType == "1" && (f_HoliDay.cStatusActive ?? false)))))
                                {
                                    if (q_SomeHoliyDay.FirstOrDefault(f => f.nHoliday == f_HoliDay.nHoliday && f.nTSubLevel == search.level2_id) != null)
                                    {
                                        HoliDay_Status = true;
                                        HoliDay_Name = f_HoliDay.sHoliday;
                                    }
                                }
                            }
                        }
                    }

                    days.Add(new Reports_01.Days
                    {
                        Days_Id = Days_Id++,
                        Days_Name = ShotDayName(DayOfWeek) + "<br/>" + dStartMonth.AddDays(DaysIndex).Day,
                        Days_Status = f_Days?.cDel,
                        HoliDay_Name = HoliDay_Name,
                        HoliDay_Status = HoliDay_Status,
                        Days_string = dStartMonth.AddDays(DaysIndex).ToString("MM/dd/yyyy")
                    });

                    bool bDay = dStartMonth.AddDays(DaysIndex).Date <= DateTime.Today || dStart > dStartMonth.AddDays(DaysIndex).Date;
                    var q_logscanDay = q_logscan.Where(f => f.LogDate == dStartMonth.AddDays(DaysIndex).Date).ToList();

                    //Thread thread = new Thread(async delegate ()
                    //{
                    AddscanDatas(data, q_logscanDay, (f_Days.cDel == "1" && HoliDay_Status) || HoliDay_Status, dStartMonth.AddDays(DaysIndex), bDay);
                    //});

                    //thread.IsBackground = true;
                    //thread.Start();

                    if (dStartMonth.AddDays(DaysIndex).DayOfWeek == 0)
                    {
                        weeks.Add(new Reports_01.Weeks
                        {
                            Weeks_Id = WeeksIndex,
                            Weeks_Name = "",
                            days = days
                        });
                        days = new List<Reports_01.Days>();
                    }
                    else if (dStartMonth.AddDays(DaysIndex) == dEndMonth)
                    {
                        weeks.Add(new Reports_01.Weeks
                        {
                            Weeks_Id = WeeksIndex,
                            Weeks_Name = "",
                            days = days
                        });
                        days = new List<Reports_01.Days>();
                    }
                    else if ((int)dStartMonth.AddDays(DaysIndex).DayOfWeek == 1)
                    {
                        WeeksIndex += 1;
                    }
                }

                header.Add(new Reports_01.HeaderReports
                {
                    Month_Id = MonthsIndex,
                    Month_Name = MonthName(dStart.AddMonths(MonthsIndex).Month) + " " + dStart.AddMonths(MonthsIndex).ToString("yyyy", new CultureInfo("th-th")),
                    weeks = weeks
                });
            }

            foreach (var q_data in data)
            {
                q_data.scanDatas = (from a in q_data.scanDatas
                                    orderby a.Year, a.DayOfYear
                                    select a).ToList();
                //q_data.scanDatas.OrderBy(o => o.Year).ThenBy(n => n.Scan_Date).ToList();
            }

            reports_01.headerReports = header;
            reports_01.reportsDatas = data;
            return reports_01;
            #endregion
        }

        private static QueryDataBases.SubLevel2_Query.Class_Classroom GetClassroom(List<QueryDataBases.SubLevel2_Query.Class_Classroom> q, int? Level_Id)
        {
            var f_data = q.FirstOrDefault(f => f.classRoom_id == Level_Id);
            if (f_data == null) f_data = new QueryDataBases.SubLevel2_Query.Class_Classroom();
            return f_data;
        }

        private static async Task AddscanDatas(List<Reports_01.ReportsData> data, List<TLogUserTimeScan> q_logscanDay, bool Days_Status, DateTime Scan_Date, bool bDay)
        {
            foreach (var Student_data in data)
            {
                var f_logscan = q_logscanDay.FirstOrDefault(f => f.sID == Student_data.User_Id);
                string Scan_Status = "";
                if (f_logscan == null)
                {
                    if (Student_data.Student_Status != "Q"
                        && Student_data.moveInDate.HasValue
                        && Scan_Date < Student_data.moveInDate)
                        Scan_Status = "Q";
                    else if (Student_data.Student_Status == "Q"
                        && Student_data.MoveOutDate.HasValue
                        && Scan_Date >= Student_data.MoveOutDate)
                        Scan_Status = "Q";
                    else if (bDay == false)
                        Scan_Status = "8";
                    else if (Days_Status == false)
                        Scan_Status = "8";
                }
                else
                {
                    if (Student_data.Student_Status != "Q"
                       && Student_data.moveInDate.HasValue
                       && Scan_Date < Student_data.moveInDate)
                        Scan_Status = "Q";
                    else if (Student_data.Student_Status == "Q"
                        && Student_data.MoveOutDate.HasValue
                        && Scan_Date >= Student_data.MoveOutDate)
                        Scan_Status = "Q";
                    else
                        Scan_Status = f_logscan.LogScanStatus;
                }

                Student_data.scanDatas.Add(new Reports_01.ScanData
                {
                    Days_Status = Days_Status ? "1" : "0",
                    Scan_Date = Scan_Date.ToString("MM/dd/yyyy"),
                    Scan_Status = Scan_Status,                    
                    Scan_Time = f_logscan == null ? "-" : f_logscan.LogTime.HasValue ? f_logscan.LogTime.Value.ToString(@"hh\:mm") : "-",
                    DayOfYear = Scan_Date.DayOfYear,
                    Year = Scan_Date.Year
                });

                if (bDay || f_logscan != null)
                {
                    if (Days_Status == true) { }
                    //if (Student_data.moveInDate != null && ) { }
                    //else if (f_logscan == null) Student_data.Sum_Status_5 += 1;
                    else if (Scan_Status == "0") Student_data.Sum_Status_0 += 1;
                    else if (Scan_Status == "12") Student_data.Sum_Status_1 += 1;
                    else if (Scan_Status == "1") Student_data.Sum_Status_2 += 1;
                    else if (Scan_Status == "11") Student_data.Sum_Status_3 += 1;
                    else if (Scan_Status == "10") Student_data.Sum_Status_4 += 1;
                    else if (Scan_Status == "3") Student_data.Sum_Status_5 += 1;
                    else if (Scan_Status == "99") Student_data.Sum_Status_6 += 1;
                    else if (Scan_Status == "8" || Scan_Status == "9") { }
                    //else if (f_logscan.LogScanStatus.Trim() == "12") Student_data.Sum_Status_6 += 1;
                    else Student_data.Sum_Status_5 += 1;
                }
            }
        }

        private static string ShotDayName(int DayNumber)
        {
            switch (DayNumber)
            {
                case 0: return "จ.";
                case 1: return "อ.";
                case 2: return "พ.";
                case 3: return "พฤ.";
                case 4: return "ศ.";
                case 5: return "ส.";
                case 6: return "อา.";
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