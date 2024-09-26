using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using static FingerprintPayment.Report.Reportmobile05;

namespace FingerprintPayment.Report.Functions.Report_05
{
    public class reportsType_01
    {
        private JWTToken.userData userData = new JWTToken.userData();

        public reportsType_01()
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
        }

        public Reports_05Model getReports(JabJaiEntities dbschool, SearchEmployees search)
        {
            //var q_employees = dbschool.TEmployees.Where(w => w.cDel == null).Select(s => new { s.sName, s.sLastname, s.sEmp, s.nTimeType, s.nDepartmentId, s.cType }).ToList();
            //var q1 = (from e in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
            //                   join i in dbschool.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID) on new { e.sEmp } equals new { sEmp = i.sEmp } into i_join

            //                   join b in dbschool.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID) on e.sEmp equals b.sEmp into jab
            //                   from jb in jab.DefaultIfEmpty()

            //                   from i in i_join.DefaultIfEmpty()
            //                   where
            //                     e.cDel == null && (jb == null || (jb.WorkStatus ?? 1) == 1)

            var q1 = (from e in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                      from i in dbschool.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                      from b in dbschool.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                      from d in dbschool.TTitleLists.Where(o => o.nTitleid + "" == e.sTitle && o.SchoolID == e.SchoolID).DefaultIfEmpty()
                          // from i in i_join.DefaultIfEmpty()
                          //on new { e.sEmp } equals new { sEmp = i.sEmp } into i_join
                          //on e.sEmp equals b.sEmp into jab
                          //from jb in jab.DefaultIfEmpty()

                          //where
                          //  e.cDel == null && (jb == null || (jb.WorkStatus ?? 1) == 1)

                      where b == null || (b.WorkStatus == 1 || (b.WorkStatus == 2 && search.dStart <= b.DayQuit))

                      orderby
                                 i.Code,
                                 e.sName,
                                 e.sLastname
                               select new
                               {
                                   title = (d == null ? e.sTitle : d.titleDescription),
                                   i.Code,
                                   e.sName,
                                   e.sLastname,
                                   e.sEmp,
                                   e.nTimeType,
                                   e.nDepartmentId,
                                   e.cType,
                                   b.WorkStatus,
                                   b.DayQuit,
                               });

            if (search.department_id.HasValue) q1 = q1.Where(w => w.nDepartmentId == search.department_id);
            if (!string.IsNullOrEmpty(search.user_type)) q1 = q1.Where(w => w.cType == search.user_type);
            if (search.emp_id.HasValue) q1 = q1.Where(w => w.sEmp == search.emp_id);

            var q_employees = q1.ToList();

            int RowsIndex = 1;
            var q2 = dbschool.TEmployeeTypes.Where(w => w.SchoolID == userData.CompanyID && w.IsDel == false).ToList();
            var dtHolidayYear = (from a in dbschool.THolidays.Where(x => x.SchoolID == userData.CompanyID).ToList()
                                 where a.cDel == null
                                 && a.sHolidayType == "0"
                                 && (new List<string> { "0", "1" }).Contains(a.sWhoSeeThis)
                                 select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = 0 }).ToList();

            dtHolidayYear.AddRange((from a in dbschool.THolidays.Where(x => x.SchoolID == userData.CompanyID)
                                    join b in dbschool.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID) on new { a.nHoliday, a.SchoolID } equals new { b.nHoliday, b.SchoolID }
                                    where a.cDel == null
                                    && a.sHolidayType == "0" //&& b.nTSubLevel == nTSubLevel
                                    && (new List<string> { "5" }).Contains(a.sWhoSeeThis)
                                    select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = b.nTSubLevel }).ToList());

            DateTime dStart = search.dStart, dEnd = search.dEnd;
            #region Report 01
            Reports_05Model reports_01 = new Reports_05Model();
            List<Reports_05Model.HeaderReports> header = new List<Reports_05Model.HeaderReports>();

            var data = (from a in q_employees
                        where !search.emp_id.HasValue || search.emp_id.Value == a.sEmp
                        select new Reports_05Model.ReportsData
                        {
                            Code = a.Code + "",
                            Name = a.title + " " +  a.sName + " " + a.sLastname,
                            Id = a.sEmp,
                            Time_Id = a.nTimeType,
                            EmpType = a.cType,
                            Sum_Status_0 = 0,
                            Sum_Status_1 = 0,
                            Sum_Status_2 = 0,
                            Sum_Status_3 = 0,
                            Sum_Status_4 = 0,
                            Sum_Status_5 = 0,
                            Sum_Status_6 = 0,

                            WorkStatus= a.WorkStatus,
                            DayQuit=  a.DayQuit,
                        }).ToList();

            data.ForEach(f => f.RowsIndex = RowsIndex++);
            data.ForEach(f => f.scanDatas = new List<Reports_05Model.ReportsData.ScanData>());

            List<TLogEmpTimeScan> q_logscan = new List<TLogEmpTimeScan>();

            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
                $@"SELECT * FROM TLogEmpTimeScan WHERE SchoolID = {userData.CompanyID} AND 
LogEmpDate BETWEEN '{dStart.ToString("yyyy-MM-dd")}' AND '{dEnd.ToString("yyyy-MM-dd")}' "
                ).ToList());
            
            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
         $@"SELECT * FROM [JabjaiSchoolHistory].[dbo].TLogEmpTimeScan WHERE SchoolID = {userData.CompanyID} AND 
LogEmpDate BETWEEN '{dStart.ToString("yyyy-MM-dd")}' AND '{dEnd.ToString("yyyy-MM-dd")}' "
         ).ToList());

            var oldDate = new DateTime(DateTime.Today.Year, 05, 01);
            if (dStart <= oldDate)
            {
                q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
                 $@"SELECT * 
FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan_Backup 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart.ToString("yyyyMMdd")}' AND '{dEnd.ToString("yyyyMMdd")}' ").ToList()
                   );
            }
            //var q_logscan = (from a in data
            //                 join b in dbschool.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID)
            //                 on a.Id equals b.sEmp
            //                 where dStart <= b.LogEmpDate && dEnd >= b.LogEmpDate
            //                 select b).ToList();

            //var f_level = dbschool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == search.level2_id);
            var q_time = (from q_data in (from a in dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID) on a.nTimeType equals b.nTimeType
                                          where a.cUserType == "2"
                                          select new
                                          {
                                              b.nDay,
                                              b.cDel,
                                              a.nTimeType,
                                              b.dTimeEnd_IN,
                                              b.nTimeLate
                                          }).ToList()
                          select new Time_types
                          {
                              nDay = q_data.nDay,
                              cDel = q_data.cDel,
                              nTimeType = q_data.nTimeType,
                              Time_Late = q_data.dTimeEnd_IN.Value.TimeOfDay
                          }).ToList();

            int WeeksIndex = 1;
            for (int MonthsIndex = 0; dStart.AddMonths(MonthsIndex) <= dEnd; MonthsIndex++)
            {
                DateTime dStartMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us"));
                DateTime dEndMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart.AddMonths(MonthsIndex).Month, dStart.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us")).AddMonths(1).AddDays(-1);
                List<Reports_05Model.HeaderReports.Weeks> weeks = new List<Reports_05Model.HeaderReports.Weeks>();
                List<Reports_05Model.HeaderReports.Weeks.Days> days = new List<Reports_05Model.HeaderReports.Weeks.Days>();
                if (dStart > dStartMonth && MonthsIndex == 0) dStartMonth = dStart;
                if (dEndMonth > dEnd) dEndMonth = dEnd;
                else { }

                for (int DaysIndex = 0; dStartMonth.AddDays(DaysIndex) <= dEndMonth; DaysIndex++)
                {
                    int DayOfWeek = (int)dStartMonth.AddDays(DaysIndex).DayOfWeek;
                    var f_Days = q_time.FirstOrDefault(f => f.nDay == (DayOfWeek == 0 ? 6 : DayOfWeek - 1));
                    days.Add(new Reports_05Model.HeaderReports.Weeks.Days
                    {
                        Days_Id = DayOfWeek,
                        Days_Name = ShotDayName(DayOfWeek) + "<br/>" + dStartMonth.AddDays(DaysIndex).Day,
                        Days_Status = f_Days.cDel == "1" ? "0" : "1",
                    });

                    bool bDay = dStartMonth.AddDays(DaysIndex).Date <= DateTime.Today || dStart > dStartMonth.AddDays(DaysIndex).Date;
                    var q_logscanDay = q_logscan.Where(f => f.LogEmpDate == dStartMonth.AddDays(DaysIndex).Date).ToList();

                    //Thread thread = new Thread(async delegate ()
                    //{
                    AddscanDatas(data, q_logscanDay, dStartMonth.AddDays(DaysIndex), bDay, q_time, dtHolidayYear, q2);
                    //});

                    //thread.IsBackground = true;
                    //thread.Start();

                    if (dStartMonth.AddDays(DaysIndex).DayOfWeek == 0)
                    {
                        weeks.Add(new Reports_05Model.HeaderReports.Weeks
                        {
                            Weeks_Id = WeeksIndex,
                            Weeks_Name = "",
                            days = days
                        });
                        days = new List<Reports_05Model.HeaderReports.Weeks.Days>();
                    }
                    else if (dStartMonth.AddDays(DaysIndex) == dEndMonth)
                    {
                        weeks.Add(new Reports_05Model.HeaderReports.Weeks
                        {
                            Weeks_Id = WeeksIndex,
                            Weeks_Name = "",
                            days = days
                        });
                        days = new List<Reports_05Model.HeaderReports.Weeks.Days>();
                    }
                    else if ((int)dStartMonth.AddDays(DaysIndex).DayOfWeek == 1)
                    {
                        WeeksIndex += 1;
                    }
                }

                header.Add(new Reports_05Model.HeaderReports
                {
                    Month_Id = MonthsIndex,
                    Month_Name = MonthName(dStart.AddMonths(MonthsIndex).Month) + " " + dStart.AddMonths(MonthsIndex).ToString("yyyy", new CultureInfo("th-th")),
                    weeks = weeks
                });
            }

            foreach (var q_data in data)
            {
                q_data.scanDatas = q_data.scanDatas.OrderBy(o => o.DayOfYear).ToList();
            }

            reports_01.headerReports = header;
            reports_01.reportsDatas = data;
            return reports_01;
            #endregion
        }

        private static void AddscanDatas(List<Reports_05Model.ReportsData> data, List<TLogEmpTimeScan> q_logscanDay, DateTime Scan_Date, bool bDay, List<Time_types> time_Types, List<TM_Holiday> _Holidays, List<TEmployeeType> employeeTypes)
        {
            List<string> _leave = new List<string> { "10", "11", "13", "21", "22", "23", "24", "25", "26" };

            foreach (var Employees_data in data)
            {
                int dayOfWeek = 0;
                switch (string.Format("{0:dddd}", Scan_Date))
                {
                    case "Monday": dayOfWeek = 0; break;
                    case "Tuesday": dayOfWeek = 1; break;
                    case "Wednesday": dayOfWeek = 2; break;
                    case "Thursday": dayOfWeek = 3; break;
                    case "Friday": dayOfWeek = 4; break;
                    case "Saturday": dayOfWeek = 5; break;
                    case "Sunday": dayOfWeek = 6; break;
                }

                var f_timeType = time_Types.FirstOrDefault(f => f.nTimeType == Employees_data.Time_Id && f.nDay == dayOfWeek);
                var f_logscanIn = q_logscanDay.FirstOrDefault(f => f.sEmp == Employees_data.Id && f.LogEmpType == "0");
                var f_logscanOut = q_logscanDay.FirstOrDefault(f => f.sEmp == Employees_data.Id && f.LogEmpType == "1");
                bool Days_Status = f_timeType == null ? false : (f_timeType.cDel == "1");

                int nTSubLevel = 0;
                int.TryParse(Employees_data.EmpType, out nTSubLevel);

                var empType = employeeTypes.FirstOrDefault(f => f.nTypeId == nTSubLevel || f.nTypeId2 == nTSubLevel);
                if (empType != null) nTSubLevel = empType.nTypeId;

                if (_Holidays.Count(c => c.dHolidayStart <= Scan_Date && c.dHolidayEnd >= Scan_Date && c.nTSubLevel == 0) > 0)
                {
                    Days_Status = false;
                }
                else if (_Holidays.Count(c => c.dHolidayStart <= Scan_Date && c.dHolidayEnd >= Scan_Date && c.nTSubLevel == nTSubLevel) > 0)
                {
                    Days_Status = false;
                }

                Reports_05Model.ReportsData.ScanData scanData = new Reports_05Model.ReportsData.ScanData();

                scanData.Days_Status = Days_Status ? "0" : "1";
                scanData.Scan_Date = Scan_Date.ToShortDateString();
                scanData.Scan_StatusIn = f_logscanIn == null ? (bDay ? (Days_Status ? "3" : "9") : "") : (Days_Status ? f_logscanIn.LogEmpScanStatus : "9");
                scanData.Scan_TimeIn = f_logscanIn == null ? "-" : f_logscanIn.LogEmpTime.HasValue ? f_logscanIn.LogEmpTime.Value.ToString(@"hh\:mm") : "-";
                scanData.Scan_StatusOut = f_logscanOut == null ? (bDay ? "3" : "") : (Days_Status ? f_logscanOut.LogEmpScanStatus : "9");
                scanData.Scan_TimeOut = f_logscanOut == null ? "-" : f_logscanOut.LogEmpTime.HasValue ? f_logscanOut.LogEmpTime.Value.ToString(@"hh\:mm") : "-";
                scanData.DayOfYear = Scan_Date.DayOfYear;

                if (Employees_data.WorkStatus == 2)
                {
                    if (Scan_Date.Date >= Employees_data.DayQuit?.Date)
                    {
                        scanData.Scan_StatusOut = "98";
                        scanData.Scan_TimeOut = "-";

                        scanData.Scan_StatusIn = "98";
                        scanData.Scan_TimeIn = "-";

                        Employees_data.scanDatas.Add(scanData);

                        continue;
                    }
                }

                if (f_logscanOut != null)
                {
                    if (_leave.Contains(f_logscanOut.LogEmpScanStatus.Trim()))
                    {
                        scanData.Scan_StatusOut = f_logscanOut.LogEmpScanStatus;
                        scanData.Scan_TimeOut = "-";
                    }
                }

                if (f_logscanIn != null)
                {
                    if (_leave.Contains(f_logscanIn.LogEmpScanStatus.Trim()))
                    {
                        scanData.Scan_StatusIn = f_logscanIn.LogEmpScanStatus;
                        scanData.Scan_TimeIn = "-";
                    }
                }
                Employees_data.scanDatas.Add(scanData);
                //if (bDay || f_logscanIn != null)
                //{
                //    if (f_logscanIn == null) Employees_data.Sum_Status_2 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "0") Employees_data.Sum_Status_0 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "1" && f_timeType != null)
                //    {
                //        Employees_data.Sum_Status_1 += 1;
                //        //TimeSpan Time_Late = TimeSpan.FromMilliseconds((f_logscanIn.LogTime.Value - f_timeType.Time_Late).TotalMilliseconds);
                //        Employees_data.TotalMilliseconds += (f_logscanIn.LogEmpTime.Value - f_timeType.Time_Late).TotalMilliseconds;
                //        //Employees_data.Time_Late += (TimeSpan.FromMilliseconds());
                //    }
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "3") Employees_data.Sum_Status_2 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "11") Employees_data.Sum_Status_3 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "10") Employees_data.Sum_Status_4 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "13") Employees_data.Sum_Status_5 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "21") Employees_data.Sum_Status_21 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "22") Employees_data.Sum_Status_22 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "23") Employees_data.Sum_Status_23 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "24") Employees_data.Sum_Status_24 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "25") Employees_data.Sum_Status_25 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "26") Employees_data.Sum_Status_26 += 1;
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "8") { }
                //    else if (f_logscanIn.LogEmpScanStatus.Trim() == "9") { }
                //    //else if (f_logscan.LogScanStatus.Trim() == "12") Student_data.Sum_Status_6 += 1;
                //    else if (!Days_Status) { }
                //    else Employees_data.Sum_Status_2 += 1;
                //}

                if (bDay && Days_Status)//|| f_logscanIn != null)
                {
                    //if (scanData.Scan_StatusIn != "3") { }
                    //else 
                    if (f_logscanIn == null)
                    {
                        if (scanData.Scan_StatusIn == "3")
                            Employees_data.Sum_Status_2 += .5f;
                    }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "0") Employees_data.Sum_Status_0 += .5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "1")
                    {
                        Employees_data.Sum_Status_1 += 1f;
                        if (f_timeType != null)
                        {
                            //TimeSpan Time_Late = TimeSpan.FromMilliseconds((f_logscanIn.LogTime.Value - f_timeType.Time_Late).TotalMilliseconds);
                            Employees_data.TotalMilliseconds += (f_logscanIn.LogEmpTime.Value - f_timeType.Time_Late).TotalMilliseconds;
                            //Employees_data.Time_Late += (TimeSpan.FromMilliseconds());
                        }
                    }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "11") Employees_data.Sum_Status_3 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "10") Employees_data.Sum_Status_4 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "13") Employees_data.Sum_Status_5 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "21") Employees_data.Sum_Status_21 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "22") Employees_data.Sum_Status_22 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "23") Employees_data.Sum_Status_23 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "24") Employees_data.Sum_Status_24 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "25") Employees_data.Sum_Status_25 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "26") Employees_data.Sum_Status_26 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "8") { }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "9") { }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "3") Employees_data.Sum_Status_2 += 0.5f;
                    else if (!Days_Status) { }
                    else if (_leave.Contains(f_logscanIn.LogEmpScanStatus.Trim())) { }
                    //else if (f_logscan.LogScanStatus.Trim() == "12") Student_data.Sum_Status_6 += 1;
                    else Employees_data.Sum_Status_2 += 0.5f;
                }

                if (bDay && Days_Status)//|| f_logscanOut != null)
                {
                    //if (scanData.Scan_StatusIn != "3") { }
                    //else 
                    if (f_logscanOut == null)
                    {
                        if (scanData.Scan_StatusIn != "9" && scanData.Scan_StatusOut == "3" && Days_Status)
                            Employees_data.Sum_Status_2 += .5f;
                    }
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "0") Employees_data.Sum_Status_0 += .5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "1")
                    {
                        //Employees_data.Sum_Status_1 += .5f;
                        //if (f_timeType != null)
                        //{
                        //    //TimeSpan Time_Late = TimeSpan.FromMilliseconds((f_logscanOut.LogTime.Value - f_timeType.Time_Late).TotalMilliseconds);
                        //    Employees_data.TotalMilliseconds += (f_logscanOut.LogEmpTime.Value - f_timeType.Time_Late).TotalMilliseconds;
                        //    //Employees_data.Time_Late += (TimeSpan.FromMilliseconds());
                        //}
                    }
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "3" && f_logscanOut.LogEmpTime == null) 
                    { 
                        Employees_data.Sum_Status_2 += 0.5f; 
                    }//Employees_data.Sum_Status_2 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "11") Employees_data.Sum_Status_3 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "10") Employees_data.Sum_Status_4 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "13") Employees_data.Sum_Status_5 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "21") Employees_data.Sum_Status_21 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "22") Employees_data.Sum_Status_22 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "23") Employees_data.Sum_Status_23 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "24") Employees_data.Sum_Status_24 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "25") Employees_data.Sum_Status_25 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "26") Employees_data.Sum_Status_26 += 0.5f;
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "8") { }
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "9") { }
                    else if (!Days_Status) { }
                    else if (_leave.Contains(f_logscanOut.LogEmpScanStatus.Trim())) { }
                    //else if (f_logscan.LogScanStatus.Trim() == "12") Student_data.Sum_Status_6 += 1;
                    else { }//Employees_data.Sum_Status_2 += .5f;
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

        private class Time_types
        {
            public int? nDay { get; set; }
            public string cDel { get; set; }
            public int nTimeType { get; set; }
            public TimeSpan Time_Late { get; set; }
        }
    }
}