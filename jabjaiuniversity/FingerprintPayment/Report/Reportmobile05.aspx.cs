
using FingerprintPayment.Class;
using FingerprintPayment.Employees;
using FingerprintPayment.Helper;
using FingerprintPayment.Report.Models;
using FingerprintPayment.UpdateStatus;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FingerprintPayment.Report.Functions.Reports_03.ReportsType_02;

namespace FingerprintPayment.Report
{

    public partial class Reportmobile05 : BehaviorGateway
    {
        protected List<TLeave_Type> LeaveTypeList = new List<TLeave_Type>();

        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    hdfschoolname.Value = tCompany.sCompany;

                    using (var dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                    {
                        LeaveTypeList = dbschool.TLeave_Type
                            .Where(o => o.SchoolID == userData.CompanyID && o.Type == "T" && o.IsDel == false)
                            .ToList();

                        if (tCompany.nSchoolHeadid != null) //update 7/11/2019
                        {
                            var data1 = dbschool.TEmployees
                                .Where(w => w.sEmp == tCompany.nSchoolHeadid && w.SchoolID == userData.CompanyID)
                                .FirstOrDefault();
                            var temp = (data1.sName + " " + data1.sLastname);
                            txtHead.Text = temp;
                        }
                        else
                        {
                            var temp1 = string.IsNullOrEmpty(tCompany.SchoolHeadName)
                                ? string.Empty :
                                tCompany.SchoolHeadName;

                            var temp2 = string.IsNullOrEmpty(tCompany.SchoolHeadLastname)
                                ? string.Empty :
                                tCompany.SchoolHeadLastname;

                            txtHead.Text = temp1 + " " + temp2;
                        }

                        if (tCompany.nPersonnel != null) //update 7/11/2019
                        {
                            var data1 = dbschool.TEmployees
                                .Where(w => w.sEmp == tCompany.nPersonnel && w.SchoolID == userData.CompanyID)
                                .FirstOrDefault();
                            var temp = (data1.sName + " " + data1.sLastname);
                            txtHeadOfHuman.Text = temp;
                        }
                    }


                }

                select_user_type.DataSource = Common.GetEmployeeTypeToDDL(userData.CompanyID);
                select_user_type.DataBind();

                //using (var _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    var nCompany = _dbMaster.TCompanies.Where(w => w.nCompany == ).FirstOrDefault();
                //}
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(SearchEmployees search)
        {
            var userData = GetUserData();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //search.dStart = search.date1.ToEnCulture() ?? DateTime.Now;
                    //search.dEnd = search.date2.ToEnCulture() ?? DateTime.Now;

                    DateTime? dStart = DateTime.Today, dEnd = DateTime.Today;
                    var q2 = dbschool.TEmployeeTypes.Where(w => w.SchoolID == tCompany.nCompany && w.IsDel == false).ToList();
                    var leaveTypeList = dbschool.TLeave_Type.Where(o => o.SchoolID == tCompany.nCompany && o.IsActive == true).ToList();

                    if (search.sort_type == 1)
                    {
                        dStart = search.dStart;
                        dEnd = search.dStart.AddMonths(1).AddDays(-1);
                    }
                    else if (search.sort_type == 3)//ระบุช่วงเวลา
                    {
                        dStart = search.dStart;
                        dEnd = search.dEnd;

                        if (dEnd > dStart)
                        {
                            //dStart = search.dStart.Value;
                            //dEnd = search.dEnd.Value;
                        }
                        else
                        {
                            (dStart, dEnd) = (dEnd, dStart);
                            //dStart = search.dEnd.Value;
                            //dEnd = search.dStart.Value;
                        }
                    }
                    else if (search.sort_type == 4)//ปีการ ศษ
                    {
                        dStart = search.dStart;

                        var year = dStart?.ToString("yyyy", new CultureInfo("th-TH"));

                        if (year != null)
                        {
                            var qry = from a in dbschool.TYears.Where(o => o.SchoolID == userData.CompanyID && o.numberYear + "" == year && o.cDel != true)
                                      from b in dbschool.TTerms.Where(o => o.SchoolID == userData.CompanyID && o.nYear == a.nYear && o.cDel != "1")
                                      select new
                                      {
                                          b.dStart,
                                          b.dEnd,
                                      };

                            var d = qry.ToList();

                            dStart = d.Min(o => o.dStart) ?? DateTime.Now;
                            dEnd = d.Max(o => o.dEnd) ?? DateTime.Now;

                            if (DateTime.Now < dEnd)
                                dEnd = DateTime.Now;
                        }

                    }
                    else //search.sort_type == 0
                    {
                        dStart = search.dStart;
                        dEnd = search.dEnd;
                    }

                    int RowsIndex = 1;

                    var dtHolidayYear = (from a in dbschool.THolidays.Where(x => x.SchoolID == userData.CompanyID).ToList()
                                         where a.cDel == null
                                         && a.sHolidayType == "0"
                                         && (new List<string> { "0", "1" }).Contains(a.sWhoSeeThis)
                                         select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = 0 }).ToList();

                    dtHolidayYear.AddRange((from a in dbschool.THolidays.Where(x => x.SchoolID == userData.CompanyID)
                                            join b in dbschool.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID) on new { a.nHoliday, a.SchoolID } equals new { b.nHoliday, b.SchoolID }
                                            where a.cDel == null && b.Deleted == null
                                            && a.sHolidayType == "0" //&& b.nTSubLevel == nTSubLevel
                                            && (new List<string> { "5" }).Contains(a.sWhoSeeThis)
                                            select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = b.nTSubLevel }).ToList());

                    var q1 = (from e in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                              from i in dbschool.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                              from b in dbschool.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                              from d in dbschool.TTitleLists.Where(o => o.nTitleid + "" == e.sTitle && o.SchoolID == e.SchoolID).DefaultIfEmpty()

                              from et in dbschool.TEmployeeTypes.Where(o => o.SchoolID == e.SchoolID && ((o.nTypeId2 ?? o.nTypeId) + "" == e.cType)).DefaultIfEmpty()

                              from dept in dbschool.TDepartments.Where(o => o.SchoolID == e.SchoolID && o.DepID == e.nDepartmentId).DefaultIfEmpty()

                              where b == null
                              || ((b.WorkStatus == 1 && (dStart >= b.WorkInEducationDate || b.WorkInEducationDate <= dEnd || b.WorkInEducationDate == null))
                              || (b.WorkStatus == 2 && dStart <= b.DayQuit))

                              orderby
                               i.Code,
                               e.sName,
                               e.sLastname
                              select new employess_data
                              {
                                  Title = (d == null ? e.sTitle : d.titleDescription),
                                  Department = dept.departmentName ?? "",
                                  EmpType = et.Title ?? "",
                                  sName = e.sName,
                                  sLastname = e.sLastname,
                                  sEmp = e.sEmp,
                                  nTimeType = e.nTimeType,
                                  nDepartmentId = e.nDepartmentId,
                                  cType = e.cType,
                                  Code = i.Code,
                                  WorkStatus = b.WorkStatus,
                                  DayQuit = b.DayQuit,
                                  WorkInEducationDate = b.WorkInEducationDate
                              }).AsQueryable();

                    if (search.department_id.HasValue) q1 = q1.Where(w => w.nDepartmentId == search.department_id);
                    if (!string.IsNullOrEmpty(search.user_type)) q1 = q1.Where(w => w.cType == search.user_type);
                    if (search.emp_id.HasValue) q1 = q1.Where(w => w.sEmp == search.emp_id);

                    var q_employees = q1.ToList();
                    var leaveType = dbschool.TLeave_Type
                        .Where(o => o.SchoolID == userData.CompanyID && o.Type == "T")
                        .ToList();

                    if (search.sort_type == 1)
                    {
                        #region Report 01
                        Reports_01 reports_01 = new Reports_01();
                        List<HeaderReports> header = new List<HeaderReports>();

                        var data = (from a in q_employees
                                    where !search.emp_id.HasValue || search.emp_id.Value == a.sEmp
                                    select new ReportsData
                                    {
                                        Code = a.Code + "",
                                        Name = a.Title + " " + a.sName + " " + a.sLastname,
                                        Id = a.sEmp,
                                        Time_Id = a.nTimeType,
                                        EmpType = a.cType,
                                        WorkStatus = a.WorkStatus,
                                        DayQuit = a.DayQuit,
                                        WorkInEducationDate = a.WorkInEducationDate,
                                        Sum_Status_0 = 0,
                                        Sum_Status_1 = 0,
                                        Sum_Status_2 = 0,
                                        Sum_Status_3 = 0,
                                        Sum_Status_4 = 0,
                                        Sum_Status_5 = 0,
                                        Sum_Status_6 = 0,
                                    }).ToList();

                        data.ForEach(f => f.RowsIndex = RowsIndex++);
                        data.ForEach(f => f.scanDatas = new List<ScanData>());

                        List<TLogEmpTimeScan> q_logscan = new List<TLogEmpTimeScan>();

                        q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
                            $@"SELECT * 
FROM JabjaiSchoolSingleDB.dbo.TLogEmpTimeScan 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart.Value.ToString("yyyyMMdd")}' AND '{dEnd.Value.ToString("yyyyMMdd")}' ").ToList()
                              );

                        if (dStart <= DateTime.Today.AddDays(-90))
                        {
                            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
                              $@"SELECT * 
FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart.Value.ToString("yyyyMMdd")}' AND '{dEnd.Value.ToString("yyyyMMdd")}' ").ToList()
                                );
                        }

                        var oldDate = new DateTime(DateTime.Today.Year, 05, 01);
                        if (dStart <= oldDate)
                        {
                            q_logscan.AddRange(dbschool.Database.SqlQuery<TLogEmpTimeScan>(
                             $@"SELECT * 
FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan_Backup 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart.Value.ToString("yyyyMMdd")}' AND '{dEnd.Value.ToString("yyyyMMdd")}' ").ToList()
                               );
                        }

                        //(from a in data
                        // join b in dbschool.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID) on a.Id equals b.sEmp
                        // where dStart <= b.LogEmpDate && dEnd >= b.LogEmpDate
                        // select b).ToList();

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

                        for (int MonthsIndex = 0; dStart.Value.AddMonths(MonthsIndex) <= dEnd; MonthsIndex++)
                        {
                            DateTime dStartMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart?.AddMonths(MonthsIndex).Month, dStart.Value.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us"));
                            DateTime dEndMonth = DateTime.ParseExact(string.Format("{0:00}/01/{1}", dStart?.AddMonths(MonthsIndex).Month, dStart.Value.AddMonths(MonthsIndex).Year), "MM/dd/yyyy", new CultureInfo("en-us")).AddMonths(1).AddDays(-1);
                            List<Weeks> weeks = new List<Weeks>();
                            List<Days> days = new List<Days>();
                            if (dStart > dStartMonth && MonthsIndex == 0) dStartMonth = dStart.Value;
                            if (dEndMonth > dEnd) dEndMonth = dEnd.Value;
                            else { }

                            for (int DaysIndex = 0; dStartMonth.AddDays(DaysIndex) <= dEndMonth; DaysIndex++)
                            {
                                //int DayOfWeek = (int)dStartMonth.AddDays(DaysIndex).DayOfWeek;
                                int dayOfWeek = 0;
                                switch (string.Format("{0:dddd}", dStartMonth.AddDays(DaysIndex)))
                                {
                                    case "Monday": dayOfWeek = 0; break;
                                    case "Tuesday": dayOfWeek = 1; break;
                                    case "Wednesday": dayOfWeek = 2; break;
                                    case "Thursday": dayOfWeek = 3; break;
                                    case "Friday": dayOfWeek = 4; break;
                                    case "Saturday": dayOfWeek = 5; break;
                                    case "Sunday": dayOfWeek = 6; break;
                                }

                                var f_Days = q_time.FirstOrDefault(f => f.nDay == (dayOfWeek == 0 ? 6 : dayOfWeek - 1));
                                days.Add(new Days
                                {
                                    Days_Id = dayOfWeek,
                                    Days_Name = dStartMonth.AddDays(DaysIndex).ToString("ddd", new CultureInfo("th-th")) + "<br/>" + dStartMonth.AddDays(DaysIndex).Day,
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
                                    weeks.Add(new Weeks
                                    {
                                        Weeks_Id = WeeksIndex,
                                        Weeks_Name = "",
                                        days = days
                                    });
                                    days = new List<Days>();
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
                                else if ((int)dStartMonth.AddDays(DaysIndex).DayOfWeek == 1)
                                {
                                    WeeksIndex += 1;
                                }
                            }

                            header.Add(new HeaderReports
                            {
                                Month_Id = MonthsIndex,
                                Month_Name = MonthName(dStart.Value.AddMonths(MonthsIndex).Month) + " " + dStart.Value.AddMonths(MonthsIndex).ToString("yyyy", new CultureInfo("th-th")),
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

                    else if (search.sort_type == 3 || search.sort_type == 4)//ระบุช่วงเวลา //รายปีการศึกษา
                    {



                        var time_Types = (from a in dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID)
                                          join b in dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID) on a.nTimeType equals b.nTimeType
                                          where b.cType == "1" && (b.cDel ?? "1") == "1"
                                          select a).ToList();

                        List<Reports_05> reports5 = new List<Reports_05>();

                        //var options = new ParallelOptions()
                        //{
                        //    MaxDegreeOfParallelism = 4
                        //};

                        List<TM_Calendar> _Calendars = new List<TM_Calendar>();
                        if (dEnd >= DateTime.Today) dEnd = DateTime.Today;

                        for (var day = dStart.Value; day.Date <= dEnd; day = day.AddDays(1))
                        {
                            int dayOfWeek = 0;
                            switch (string.Format("{0:dddd}", day))
                            {
                                case "Monday": dayOfWeek = 0; break;
                                case "Tuesday": dayOfWeek = 1; break;
                                case "Wednesday": dayOfWeek = 2; break;
                                case "Thursday": dayOfWeek = 3; break;
                                case "Friday": dayOfWeek = 4; break;
                                case "Saturday": dayOfWeek = 5; break;
                                case "Sunday": dayOfWeek = 6; break;
                            }

                            _Calendars.Add(new TM_Calendar
                            {
                                date = day,
                                DayOfWeek = dayOfWeek,
                                gHoliday = dtHolidayYear.Where(c => c.dHolidayStart <= day && c.dHolidayEnd >= day).Select(w => w.nTSubLevel).ToList(),
                                bdHoliday = dtHolidayYear.Count(c => c.dHolidayStart <= day && c.dHolidayEnd >= day && c.nTSubLevel == 0) > 0
                            });
                        }

                        if (search.formtype == 1)
                        {

                            var qry2 = $@"
SELECT B1.sEmp, B1.LogEmpTime , B1.LogEmpType , B1.LogEmpDate ,B1.LogEmpScanStatus ,  B1.DeviceType , C1.FaceScanUrl, C1.Temperature
    FROM [JabjaiSchoolSingleDB].[dbo].TLogEmpTimeScan B1  
   LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TFaceScanLog C1 ON B1.SchoolID = C1.SchoolID AND B1.sEmp = C1.sID 
    --AND FORMAT(C1.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B1.LogEmpDate AS DATETIME) + CAST(B1.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
    AND FORMAT(CAST(B1.LogEmpDate AS DATETIME) + CAST(B1.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C1.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C1.LogTime), 'yyyyMMddHHmm')
WHERE B1.SchoolID = {userData.CompanyID} 
AND B1.LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' 

UNION 

SELECT  B2.sEmp,B2.LogEmpTime , B2.LogEmpType , B2.LogEmpDate ,B2.LogEmpScanStatus,  B2.DeviceType , C2.FaceScanUrl, C2.Temperature
FROM JabjaiSchoolHistory.[dbo].TLogEmpTimeScan B2 
LEFT JOIN JabjaiSchoolHistory.[dbo].TFaceScanLog C2 ON B2.SchoolID = C2.SchoolID AND B2.sEmp = C2.sID 
--AND FORMAT(C2.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
AND FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C2.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C2.LogTime), 'yyyyMMddHHmm')
WHERE B2.SchoolID = {userData.CompanyID} 
AND B2.LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' 
";
                            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);

                            if (dStart <= oldDate)
                            {
                                qry2 += $@"
UNION

SELECT B1.sEmp, B1.LogEmpTime , B1.LogEmpType , B1.LogEmpDate ,B1.LogEmpScanStatus ,  B1.DeviceType , C1.FaceScanUrl, C1.Temperature
 FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan_Backup B1  
   LEFT JOIN [JabjaiSchoolHistory].[dbo].TFaceScanLog C1 ON B1.SchoolID = C1.SchoolID AND B1.sEmp = C1.sID 
    --AND FORMAT(C1.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B1.LogEmpDate AS DATETIME) + CAST(B1.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
    AND FORMAT(CAST(B1.LogEmpDate AS DATETIME) + CAST(B1.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C1.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C1.LogTime), 'yyyyMMddHHmm')
WHERE B1.SchoolID = {userData.CompanyID} 
AND B1.LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' ";


                            }

                            var faceData = dbschool.Database.SqlQuery<FaceScanModel>(qry2).ToList();


                            //var d1 = (from a in q_employees
                            //               from b in faceData.Where(o => o.sEmp == a.sEmp).DefaultIfEmpty()
                            //               select new 
                            //               {
                            //                   Name = a.Title + " " + a.sName + " " + a.sLastname,
                            //                   sEmp = a.sEmp,
                            //                   nTimeType = a.nTimeType,
                            //                   nDepartmentId = a.nDepartmentId,
                            //                   cType = a.cType,
                            //                   Code = a.Code,
                            //                   WorkStatus = a.WorkStatus,
                            //                   DayQuit = a.DayQuit,

                            //                   FaceScanUrl = b?.FaceScanUrl,
                            //                   DeviceType = b?.DeviceType,
                            //                   Temperature = b?.Temperature,

                            //                   EmpType = a.EmpType,
                            //                   Department = a.Department,
                            //               }).ToList();

                            var d1 = q_employees
                               .Where(o => !search.emp_id.HasValue || search.emp_id.Value == o.sEmp)
                               //.AsEnumerable()
                               .Select((o, i) => new
                               {
                                   // RowsIndex = i + 1,
                                   Code = o.Code,
                                   Id = o.sEmp,
                                   Name = o.Title + " " + o.sName + " " + o.sLastname,
                                   empType = o.cType,
                                   Time_Id = o.nTimeType,
                                   o.WorkStatus,
                                   o.DayQuit,
                                   o.Department,
                                   o.EmpType,
                               })
                               .OrderBy(o => o.Code);

                            var lst = new List<Report3Form1>();

                            foreach (var emp in d1.OrderBy(o => o.Code))
                            {
                                var log = faceData.Where(o => o.sEmp == emp.Id);

                                var _d = (from a in _Calendars

                                              // from b1 in log.Where(o => o.Type == "0" && o.Date == a.date).DefaultIfEmpty()
                                          from f1 in log.Where(o => o.LogEmpType == "0" && o.LogEmpDate == a.date && new string[] { "0", "1" }.Contains(o.LogEmpScanStatus)).DefaultIfEmpty()

                                          from f2 in log.Where(o => o.LogEmpType == "1" && o.LogEmpDate == a.date && new string[] { "0", "2", "3" }.Contains(o.LogEmpScanStatus)).DefaultIfEmpty()

                                          from c in time_Types.Where(o => o.nDay == a.DayOfWeek && o.nTimeType == emp.Time_Id).DefaultIfEmpty()

                                          select new Report3Form1
                                          {
                                              Date = a.date,
                                              //isEarly = b1?.Time < c?.dTimeStart_IN?.TimeOfDay && b2?.Time > c?.dTimeEnd_OUT?.TimeOfDay ,
                                              Time1 = f1?.LogEmpTime?.ToString(@"hh\:mm"),
                                              Status1 = f1?.LogEmpScanStatus,
                                              Face1 = f1?.FaceScanUrl,
                                              Device1 = fcommon.GetDeviceTypeName(f1?.DeviceType),
                                              IsEarly = f1?.LogEmpTime?.Ticks != 0 && f1?.LogEmpTime < (c?.dTimeStart_IN?.TimeOfDay),
                                              IsEarly2 = f1?.LogEmpTime?.Ticks != 0 && f1?.LogEmpTime < (search.time1 ?? c?.dTimeStart_IN?.TimeOfDay),

                                              Time2 = f2?.LogEmpTime?.ToString(@"hh\:mm"),
                                              Status2 = f2?.LogEmpScanStatus,
                                              Face2 = f2?.FaceScanUrl,
                                              Device2 = fcommon.GetDeviceTypeName(f2?.DeviceType),
                                              IsLate = f2?.LogEmpTime?.Ticks != 0 && f2?.LogEmpTime > (c?.dTimeEnd_OUT?.TimeOfDay),
                                              IsLate2 = f2?.LogEmpTime?.Ticks != 0 && f2?.LogEmpTime > (search.time2 ?? c?.dTimeEnd_OUT?.TimeOfDay),

                                              EmpType = emp.EmpType,
                                              Department = emp.Department,
                                              Code = emp.Code,
                                              Name = emp.Name,
                                          }).Where(o => o.IsLate2 && o.IsEarly2).ToList();

                                //var d = qry.Where(o => o.isEarly);
                                lst.AddRange(_d);
                            }

                            var d = lst.GroupBy(o => o.Date)
                                .Select(o => new
                                {
                                    Date = o.Key,
                                    date = o.Key.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                    lst = o.Select(i => i).OrderBy(i => i.Code),
                                    count = o.Count()
                                })
                                .OrderBy(o => o.Date);

                            return d;
                        }
                        else
                        {
                            var sql = $@"
SELECT * 
FROM JabjaiSchoolSingleDB.dbo.TLogEmpTimeScan 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' {(search.formtype == 0 && search.sort_type == 3 ? " --AND LogEmpScanStatus = 0 " : "")}

UNION 

SELECT * 
FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' {(search.formtype == 0 && search.sort_type == 3 ? " --AND LogEmpScanStatus = 0 " : "")}
";
                            var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                            if (dStart <= oldDate)
                            {
                                sql += $@"
UNION 

SELECT * 
FROM JabjaiSchoolHistory.dbo.TLogEmpTimeScan_Backup 
WHERE SchoolID = {userData.CompanyID} 
AND LogEmpDate between '{dStart?.ToString("yyyyMMdd")}' AND '{dEnd?.ToString("yyyyMMdd")}' {(search.formtype == 0 && search.sort_type == 3 ? " --AND LogEmpScanStatus = 0 " : "")}
";
                            }

                            var q_logscan = dbschool.Database.SqlQuery<TLogEmpTimeScan>(sql).ToList();

                            var d2 = (from b in q_logscan.Where(w => w.SchoolID == userData.CompanyID && w.cDel != true)

                                      select new
                                      {
                                          b.sEmp,
                                          Status = b.LogEmpScanStatus,
                                          Date = b.LogEmpDate,
                                          Type = b.LogEmpType,
                                          Time = b.LogEmpTime
                                      })
                                        .ToList();

                            var d1 = q_employees
                               .Where(o => !search.emp_id.HasValue || search.emp_id.Value == o.sEmp)
                               //.AsEnumerable()
                               .Select((o, i) => new
                               {
                                   // RowsIndex = i + 1,
                                   Code = o.Code,
                                   Id = o.sEmp,
                                   Name = o.Title + " " + o.sName + " " + o.sLastname,
                                   empType = o.cType,
                                   Time_Id = o.nTimeType,
                                   o.WorkStatus,
                                   o.DayQuit,
                                   o.Department,
                                   o.EmpType,
                               })
                               .OrderBy(o => o.Code);
                            //.ToList();

                            //Parallel.ForEach(d1.OrderBy(c => c.Code), (emp) =>
                            //int index = 0;

                            foreach (var emp in d1.OrderBy(o => o.Code))
                            {
                                int nTSubLevel = 0;
                                int.TryParse(emp.empType, out nTSubLevel);

                                var empType = q2.FirstOrDefault(f => f.nTypeId == nTSubLevel || f.nTypeId2 == nTSubLevel);
                                if (empType != null) nTSubLevel = empType.nTypeId;

                                var log = d2.Where(o => o.sEmp == emp.Id);
                                var rep5 = new Reports_05();
                                //rep5.RowsIndex = index++;
                                rep5.Id = emp.Id;
                                rep5.Code = emp.Code + "";
                                rep5.Name = emp.Name;

                                //rep5.Sum_Status_1 = log.Where(o => o.Type == "0" && o.Status == "1").Count() * 1f;

                                rep5.Sum_Status_1 += (from a in _Calendars
                                                      from b in log.Where(w => w.Type == "0" && w.Status == "1" && w.Date == a.date)

                                                          //join c in time_Types on
                                                          //new { DayOfWeek = a.DayOfWeek, nTimeType = emp.Time_Id }
                                                          //equals new { DayOfWeek = c.nDay, nTimeType = c.nTimeType } into jac
                                                          //from jc in jac.DefaultIfEmpty()

                                                          //where (jc != null && jc.cDel == "1")
                                                          //&& (jb == null || jb.Status == "1")
                                                      select new
                                                      {
                                                          a.date,
                                                          Sum_Status = (emp.WorkStatus == 2 && a.date >= emp.DayQuit?.Date) || (a.bdHoliday == true || a.gHoliday.Count(c => c == nTSubLevel) > 0) ? 0 : 1
                                                      }).Sum(s => s.Sum_Status);

                                rep5.Sum_Status_10 = log.Where(o => o.Status == "10").Count() * 0.5f;
                                rep5.Sum_Status_11 = log.Where(o => o.Status == "11").Count() * 0.5f;
                                rep5.Sum_Status_21 = log.Where(o => o.Status == "21").Count() * 0.5f;
                                rep5.Sum_Status_22 = log.Where(o => o.Status == "22").Count() * 0.5f;
                                rep5.Sum_Status_23 = log.Where(o => o.Status == "23").Count() * 0.5f;
                                rep5.Sum_Status_24 = log.Where(o => o.Status == "24").Count() * 0.5f;
                                rep5.Sum_Status_25 = log.Where(o => o.Status == "25").Count() * 0.5f;
                                rep5.Sum_Status_26 = log.Where(o => o.Status == "26").Count() * 0.5f;

                                rep5.Sum_Status_3 += (from a in _Calendars
                                                      join b in log.Where(w => w.Type == "0") on a.date equals b.Date into jab
                                                      from jb in jab.DefaultIfEmpty()

                                                      join c in time_Types on
                                                      new { DayOfWeek = a.DayOfWeek, nTimeType = emp.Time_Id }
                                                      equals new { DayOfWeek = c.nDay, nTimeType = c.nTimeType } into jac
                                                      from jc in jac.DefaultIfEmpty()

                                                      where (jc != null && jc.cDel == "1")
                                                      && (jb == null || jb.Status == "3")
                                                      select new
                                                      {
                                                          a.date,
                                                          Sum_Status_3 = (emp.WorkStatus == 2 && a.date >= emp.DayQuit?.Date) || (a.bdHoliday == true || a.gHoliday.Count(c => c == nTSubLevel) > 0) ? 0 : 0.5f
                                                          //Sum_Status_3 = 0.5f
                                                      }).Sum(s => s.Sum_Status_3);

                                rep5.Sum_Status_3 += (from a in _Calendars
                                                      join b in log.Where(w => w.Type == "1") on a.date equals b.Date into jab
                                                      from jb in jab.DefaultIfEmpty()

                                                      join c in time_Types on
                                                      new { DayOfWeek = a.DayOfWeek, nTimeType = emp.Time_Id }
                                                      equals new { DayOfWeek = c.nDay, nTimeType = c.nTimeType } into jac
                                                      from jc in jac.DefaultIfEmpty()

                                                      where (jc != null && jc.cDel == "1")
                                                      && jb == null
                                                      select new
                                                      {
                                                          a.date,
                                                          Sum_Status_3 = (log.Count(c => c.Date.Value.Date == a.date && c.Type == "0" && (c.Status.Trim() == "8" || c.Status.Trim() == "9")) > 0)
                                                          || (emp.WorkStatus == 2 && a.date >= emp.DayQuit?.Date)
                                                          || (a.bdHoliday == true || a.gHoliday.Count(c => c == nTSubLevel) > 0) ? 0 : 0.5f
                                                          //Sum_Status_3 = 0.5f
                                                      }).Sum(s => s.Sum_Status_3);

                                var qryEarly = (from a in _Calendars.Where(a => a.bdHoliday == false
                                                       && a.gHoliday.Count(c => c == nTSubLevel) == 0
                                                       && !(new int?[] { 5, 6 }.Contains(a.DayOfWeek))
                                                )
                                                from b1 in log.Where(o => o.Type == "0" && o.Date == a.date).DefaultIfEmpty()
                                                from b2 in log.Where(o => o.Type == "1" && o.Date == a.date).DefaultIfEmpty()
                                                from c in time_Types.Where(o => o.nDay == a.DayOfWeek && o.nTimeType == emp.Time_Id).DefaultIfEmpty()

                                                select new
                                                {
                                                    date = a.date.ToString("dd/MM/yyyy"),
                                                    isEarly = b1?.Time?.Ticks == 0 || b1?.Time < c?.dTimeStart_IN?.TimeOfDay,
                                                    isLate = b2?.Time?.Ticks == 0 || b2?.Time > c?.dTimeEnd_OUT?.TimeOfDay,
                                                    time1 = b1?.Time?.ToString(@"hh\:mm"),
                                                    time2 = b2?.Time?.ToString(@"hh\:mm"),
                                                });

                                rep5.Sum_Status_Early += qryEarly.Where(o => o.isEarly && o.isLate).Count();

                                rep5.Sum_Day = _Calendars.Where(a => a.bdHoliday == false
                                                   && a.gHoliday.Count(c => c == nTSubLevel) == 0
                                                   && !(new int?[] { 5, 6 }.Contains(a.DayOfWeek))
                                               ).Count() - rep5.Sum_Status_10 - rep5.Sum_Status_11
                                        - rep5.Sum_Status_26 - rep5.Sum_Status_25 - rep5.Sum_Status_21
                                        - rep5.Sum_Status_22 - rep5.Sum_Status_23 - rep5.Sum_Status_24;

                                reports5.Add(rep5);
                            }

                            return reports5;
                        }

                    }

                    else//search.sort_type == 0
                    {
                        List<Reports_02> reports_02s = new List<Reports_02>();
                        //var q_LogScan = dbschool.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogEmpDate == dStart).ToArray();

                        //var q1 = (from e in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                        //          from i in dbschool.TEmployeeInfoes.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                        //          from b in dbschool.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == e.sEmp).DefaultIfEmpty()
                        //          from d in dbschool.TTitleLists.Where(o => o.nTitleid + "" == e.sTitle && o.SchoolID == e.SchoolID).DefaultIfEmpty()

                        //          from et in dbschool.TEmployeeTypes.Where(o => o.SchoolID == e.SchoolID && ((o.nTypeId2 ?? o.nTypeId) + "" == e.cType)).DefaultIfEmpty()

                        //          from dept in dbschool.TDepartments.Where(o => o.SchoolID == e.SchoolID && o.DepID == e.nDepartmentId).DefaultIfEmpty()

                        //          where b == null
                        //          || ((b.WorkStatus == 1 && (dStart >= b.WorkInEducationDate || b.WorkInEducationDate == null)) || (b.WorkStatus == 2 && dStart <= b.DayQuit))
                        //          orderby
                        //           i.Code,
                        //           e.sName,
                        //           e.sLastname
                        //          select new employess_data
                        //          {
                        //              Title = (d == null ? e.sTitle : d.titleDescription),
                        //              Department = dept.departmentName ?? "",
                        //              EmpType = et.Title ?? "",
                        //              sName = e.sName,
                        //              sLastname = e.sLastname,
                        //              sEmp = e.sEmp,
                        //              nTimeType = e.nTimeType,
                        //              nDepartmentId = e.nDepartmentId,
                        //              cType = e.cType,
                        //              Code = i.Code,
                        //              WorkStatus = b.WorkStatus,
                        //              DayQuit = b.DayQuit,
                        //          }).AsQueryable();

                        //if (search.department_id.HasValue) q1 = q1.Where(w => w.nDepartmentId == search.department_id);
                        //if (!string.IsNullOrEmpty(search.user_type)) q1 = q1.Where(w => w.cType == search.user_type);
                        //if (search.emp_id.HasValue) q1 = q1.Where(w => w.sEmp == search.emp_id);

                        //dbschool.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogEmpDate == dStart).ToArray();
                        //var q_employees2 = q1.ToList();
                        var q_time = dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID).ToList();

                        string qry2 = "";
                        var oldDate = new DateTime(DateTime.Now.Year, 05, 01);
                        if (dStart <= oldDate)
                        {
                            qry2 += $@"    
    SELECT  B2.sEmp , B2.LogEmpTime , B2.LogEmpDate , B2.LogEmpType , B2.LogEmpScanStatus , B2.LogEmpnDay , C2.FaceScanUrl, C2.Temperature, B2.DeviceType  , B2.LeaveID
    FROM JabjaiSchoolHistory.[dbo].TLogEmpTimeScan_Backup B2 
    LEFT JOIN JabjaiSchoolHistory.[dbo].TFaceScanLog C2 ON B2.SchoolID = C2.SchoolID AND B2.sEmp = C2.sID 
    --AND FORMAT(C2.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
    AND FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C2.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C2.LogTime), 'yyyyMMddHHmm')
    WHERE B2.SchoolID = {userData.CompanyID} AND B2.LogEmpDate = '{dStart?.ToString("yyyyMMdd")}' --AND B2.LogEmpType = '0'
  ";
                        }
                        else if (dStart > DateTime.Today.AddDays(-90))
                        {
                            qry2 = $@"
    SELECT  B2.sEmp , B2.LogEmpTime , B2.LogEmpDate , B2.LogEmpType , B2.LogEmpScanStatus , B2.LogEmpnDay , C2.FaceScanUrl, C2.Temperature, B2.DeviceType , B2.LeaveID
    FROM [JabjaiSchoolSingleDB].[dbo].TLogEmpTimeScan B2  
    LEFT JOIN [JabjaiSchoolSingleDB].[dbo].TFaceScanLog C2 ON B2.SchoolID = C2.SchoolID AND B2.sEmp = C2.sID 
    --AND FORMAT(C2.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
    AND FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C2.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C2.LogTime), 'yyyyMMddHHmm')
    WHERE B2.SchoolID = {userData.CompanyID} AND B2.LogEmpDate = '{dStart?.ToString("yyyyMMdd")}' --AND B2.LogEmpType = '0'
 ";
                        }
                        else
                        {
                            qry2 = $@"
    SELECT  B2.sEmp , B2.LogEmpTime , B2.LogEmpDate , B2.LogEmpType , B2.LogEmpScanStatus , B2.LogEmpnDay , C2.FaceScanUrl, C2.Temperature, B2.DeviceType ,B2.LeaveID
    FROM JabjaiSchoolHistory.[dbo].TLogEmpTimeScan B2 
    LEFT JOIN JabjaiSchoolHistory.[dbo].TFaceScanLog C2 ON B2.SchoolID = C2.SchoolID AND B2.sEmp = C2.sID 
    --AND FORMAT(C2.LogTime, 'yyyyMMddHHmm') = FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm')
    AND FORMAT(CAST(B2.LogEmpDate AS DATETIME) + CAST(B2.LogEmpTime AS DATETIME), 'yyyyMMddHHmm') BETWEEN FORMAT(DATEADD(ss, -2, C2.LogTime), 'yyyyMMddHHmm') AND FORMAT(DATEADD(ss, 2, C2.LogTime), 'yyyyMMddHHmm')
    WHERE B2.SchoolID = {userData.CompanyID} AND B2.LogEmpDate = '{dStart?.ToString("yyyyMMdd")}' --AND B2.LogEmpType = '0'
  ";
                        }

                        var faceData = dbschool.Database.SqlQuery<FaceScanModel>(qry2).ToList();
                        var q_LogScan = (from a in faceData
                                         from b in leaveTypeList.Where(o => o.TypeID == a.LeaveID).DefaultIfEmpty()
                                         select new TLogEmpTimeScan
                                         {
                                             sEmp = a.sEmp,
                                             LogEmpTime = a.LogEmpTime,
                                             LogEmpDate = a.LogEmpDate,
                                             LogEmpType = a.LogEmpType,
                                             deviceType = a.DeviceType,
                                             LogEmpScanStatus = a.LogEmpScanStatus,
                                             LogEmpnDay = a.LogEmpnDay,
                                             LeaveID = a.LeaveID,
                                             //LeaveName = b?.TypeName,
                                         }).ToList();

                        q_employees = (from a in q_employees
                                       from b in faceData.Where(o => o.sEmp == a.sEmp && o.LogEmpType == "0").Take(1).DefaultIfEmpty()
                                       select new employess_data
                                       {
                                           Title = a.Title,
                                           sName = a.sName,
                                           sLastname = a.sLastname,
                                           sEmp = a.sEmp,
                                           nTimeType = a.nTimeType,
                                           nDepartmentId = a.nDepartmentId,
                                           cType = a.cType,
                                           Code = a.Code,
                                           WorkStatus = a.WorkStatus,
                                           DayQuit = a.DayQuit,

                                           FaceScanUrl = b?.FaceScanUrl,
                                           DeviceType = b?.DeviceType,
                                           Temperature = b?.Temperature,
                                           LeaveID = b?.LeaveID,
                                           EmpType = a.EmpType,
                                           Department = a.Department,
                                       }).ToList();

                        foreach (var data in q_employees)
                        {
                            int dayOfWeek = 0;
                            switch (string.Format("{0:dddd}", dStart))
                            {
                                case "Monday": dayOfWeek = 0; break;
                                case "Tuesday": dayOfWeek = 1; break;
                                case "Wednesday": dayOfWeek = 2; break;
                                case "Thursday": dayOfWeek = 3; break;
                                case "Friday": dayOfWeek = 4; break;
                                case "Saturday": dayOfWeek = 5; break;
                                case "Sunday": dayOfWeek = 6; break;
                            }

                            var f_time = q_time.FirstOrDefault(f => f.nTimeType == data.nTimeType && f.nDay == dayOfWeek);

                            var tmp = GetReports_02(data, q_LogScan, f_time, leaveType);

                            if (data.WorkStatus == 2)
                            {
                                if (dStart?.Date >= data.DayQuit?.Date)
                                {
                                    tmp.ScanIn_Status = "98";
                                    tmp.ScanOut_Status = "98";

                                    reports_02s.Add(tmp);

                                    continue;
                                }
                            }

                            //reports_02s.Add(GetReports_02(data, q_LogScan));

                            int nTSubLevel = 0;
                            int.TryParse(data.cType, out nTSubLevel);

                            var empType = q2.FirstOrDefault(f => f.nTypeId == nTSubLevel || f.nTypeId2 == nTSubLevel);
                            if (empType != null) nTSubLevel = empType.nTypeId;

                            if (dtHolidayYear.Count(c => c.dHolidayStart <= dStart && c.dHolidayEnd >= dStart && c.nTSubLevel == 0) > 0)
                            {
                                tmp.ScanOut_Status = "8";
                                tmp.ScanIn_Status = "8";
                            }
                            else if (dtHolidayYear.Count(c => c.dHolidayStart <= dStart && c.dHolidayEnd >= dStart && c.nTSubLevel == nTSubLevel) > 0)
                            {
                                tmp.ScanOut_Status = "8";
                                tmp.ScanIn_Status = "8";
                            }

                            if (search.status == -1)
                            {
                                reports_02s.Add(tmp);
                            }
                            else if (search.status == 4)
                            {
                                if (tmp.ScanIn_Status == "10" || tmp.ScanIn_Status == "11" || tmp.ScanIn_Status == "21"
                                    || tmp.ScanIn_Status == "22" || tmp.ScanIn_Status == "23" || tmp.ScanIn_Status == "24"
                                    || tmp.ScanIn_Status == "25" || tmp.ScanIn_Status == "26")
                                {
                                    reports_02s.Add(tmp);
                                }
                            }
                            else if (search.status == 5)
                            {
                                if (tmp.ScanIn_Status == "1" || tmp.ScanIn_Status == "3"
                                    || tmp.ScanIn_Status == "10" || tmp.ScanIn_Status == "11" || tmp.ScanIn_Status == "21"
                                    || tmp.ScanIn_Status == "22" || tmp.ScanIn_Status == "23" || tmp.ScanIn_Status == "24"
                                    || tmp.ScanIn_Status == "25" || tmp.ScanIn_Status == "26")
                                {
                                    reports_02s.Add(tmp);
                                }
                            }
                            else if (search.status == 99)
                            {
                                if (tmp.ScanOut_Status == "2")
                                {
                                    reports_02s.Add(tmp);
                                }
                            }
                            else if (search.status == 6)
                            {
                                if (tmp.IsOutLate && tmp.IsScanEarly)
                                {
                                    reports_02s.Add(tmp);
                                }
                            }
                            else
                            {
                                if (tmp.ScanIn_Status == search.status.ToString())
                                {
                                    reports_02s.Add(tmp);
                                }
                            }
                        }

                        var summary = new
                        {
                            CountAll = reports_02s.Count(),

                            Count3 = reports_02s.Count(o => o.ScanIn_Status == "3"),//ขาด
                            Count10 = reports_02s.Count(o => o.ScanIn_Status == "10"),//ลากิจ
                            Count11 = reports_02s.Count(o => o.ScanIn_Status == "11"),//ลาป่วย
                            Count21 = reports_02s.Count(o => o.ScanIn_Status == "21"),//ลาคลอดบุตร
                            Count22 = reports_02s.Count(o => o.ScanIn_Status == "22"),//ลาพักร้อน

                            Count23 = reports_02s.Count(o => o.ScanIn_Status == "23"),// ลาบวช
                            Count24 = reports_02s.Count(o => o.ScanIn_Status == "24"),// ลาทหาร
                            Count25 = reports_02s.Count(o => o.ScanIn_Status == "25"),//ลาไปศึกษา
                            Count26 = reports_02s.Count(o => o.ScanIn_Status == "26"),//ลาไปราชการ
                            Count0 = reports_02s.Count(o => o.ScanIn_Status == "0" || o.ScanIn_Status == "1"),//มา
                        };
                        return new { data = reports_02s, summary = summary };
                    }
                }
            }
        }

        private static void CompareStatus(Reports_05 rep5, string status)
        {
            switch (status + "")
            {
                //case "1":
                //    rep5.Sum_Status_1 += 1f;
                //    break;

                case "3":
                case "":
                    rep5.Sum_Status_3 += 0.5f;
                    break;

                //case "10":
                //    rep5.Sum_Status_10 += 0.5f;
                //    break;

                //case "11":
                //    rep5.Sum_Status_11 += 0.5f;
                //    break;

                //case "21":
                //    rep5.Sum_Status_21 += 0.5f;
                //    break;

                //case "22":
                //    rep5.Sum_Status_22 += 0.5f;
                //    break;

                //case "23":
                //    rep5.Sum_Status_23 += 0.5f;
                //    break;

                //case "24":
                //    rep5.Sum_Status_24 += 0.5f;
                //    break;

                //case "25":
                //    rep5.Sum_Status_25 += 0.5f;
                //    break;

                //case "26":
                //    rep5.Sum_Status_26 += 0.5f;
                //    break;

                default:
                    //rep5.Sum_Status_3 += 0.5f;
                    break;
            }
        }

        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }

        private static Reports_02 GetReports_02(employess_data employee
            , List<TLogEmpTimeScan> q_LogScan, TTime time
            , List<TLeave_Type> leaveType)
        {
            string Student_Name = employee.sName + " " + employee.sLastname;
            var ScanIn = q_LogScan.FirstOrDefault(f => f.sEmp == employee.sEmp && f.LogEmpType == "0");
            var ScanOut = q_LogScan.FirstOrDefault(f => f.sEmp == employee.sEmp && f.LogEmpType == "1");

            string ScanIn_Status = "3", ScanOut_Status = "3";
            
            if (ScanIn != null) ScanIn_Status = ScanIn.LogEmpScanStatus.Trim();
            if (ScanOut != null) ScanOut_Status = ScanOut.LogEmpScanStatus.Trim();

            //if (ScanIn == null && (time == null || time.cDel == "0")) ScanIn_Status = "8";
            //if (ScanOut == null && (time == null || time.cDel == "0")) ScanOut_Status = "8";
            bool IsScanEarly, IsOutLate;

            if (time == null || time.cDel == "0")
            {
                ScanIn_Status = "8";
                ScanOut_Status = "8";
                IsScanEarly = false;
                IsOutLate = false;
            }
            else
            {
                if (ScanIn?.LogEmpTime?.Ticks == 0)
                {
                    IsScanEarly = false;
                }
                else
                {
                    IsScanEarly = ScanIn?.LogEmpTime < time.dTimeStart_IN?.TimeOfDay;
                }

                if (ScanOut?.LogEmpTime?.Ticks == 0)
                {
                    IsOutLate = false;
                }
                else
                {
                    IsOutLate = ScanOut?.LogEmpTime > time?.dTimeEnd_OUT?.TimeOfDay;
                }
            }


            return new Reports_02
            {
                Code = employee.Code + "",
                Student_Name = employee.Title + " " + employee.sName + " " + employee.sLastname,
                ScanIn_Time = ScanIn == null ? "-" : (ScanIn.LogEmpTime.HasValue ? ScanIn.LogEmpTime.Value.ToString(@"hh\:mm") : "-"),
                ScanInText = leaveType.FirstOrDefault(o => o.TypeID == ScanIn?.LeaveID)?.TypeName,
                ScanOut_Time = ScanOut == null ? "-" : (ScanOut.LogEmpTime.HasValue ? ScanOut.LogEmpTime.Value.ToString(@"hh\:mm") : "-"),
                ScanOutText = leaveType.FirstOrDefault(o => o.TypeID == ScanOut?.LeaveID)?.TypeName,
                ScanIn_Status = ScanIn_Status,
                ScanOut_Status = ScanOut_Status,
                FaceUrl = employee.FaceScanUrl,
                Temp = employee.Temperature,
                Department = employee.Department,
                EmpType = employee.EmpType,
                DeviceType = fcommon.GetDeviceTypeName(employee.DeviceType),
                IsScanEarly = IsScanEarly,
                IsOutLate = IsOutLate,
            };
        }

        private static void AddscanDatas(List<ReportsData> data, List<TLogEmpTimeScan> q_logscanDay, DateTime Scan_Date, bool bDay, List<Time_types> time_Types, List<TM_Holiday> _Holidays, List<TEmployeeType> employeeTypes)
        {
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

                List<string> _leave = new List<string> { "10", "11", "13", "21", "22", "23", "24", "25", "26" };
                ScanData scanData = new ScanData();

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

                if (Employees_data.WorkStatus == 1)
                {
                    if (Scan_Date.Date < Employees_data.WorkInEducationDate?.Date)
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
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "10")
                        Employees_data.Sum_Status_4 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "13") Employees_data.Sum_Status_5 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "21") Employees_data.Sum_Status_21 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "22") Employees_data.Sum_Status_22 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "23") Employees_data.Sum_Status_23 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "24") Employees_data.Sum_Status_24 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "25") Employees_data.Sum_Status_25 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "26") Employees_data.Sum_Status_26 += 0.5f;
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "8") { }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "9") { }
                    else if (f_logscanIn.LogEmpScanStatus.Trim() == "3")
                        Employees_data.Sum_Status_2 += 0.5f;
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
                    else if (f_logscanOut.LogEmpScanStatus.Trim() == "10")
                        Employees_data.Sum_Status_4 += 0.5f;
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

        public static List<TDepartment> getDepartment()
        {
            string session = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(session, ConnectionDB.Read)))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                return (from a in jabJaiEntities.TDepartments
                        where (a.deleted ?? 0) == 0 && userData.CompanyID == a.SchoolID
                        select a).ToList();
            }
        }

        public class Search
        {
            public int sort_type { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
            public string plane_Id { get; set; }
            public int? emp_id { get; set; }
            public int? department_id { get; set; }
            public string user_type { get; set; }
        }

        public class Reports_01
        {
            public List<HeaderReports> headerReports { get; set; }
            public List<ReportsData> reportsDatas { get; set; }
        }

        public class TM_Calendar
        {
            public DateTime date { get; set; }
            public int? DayOfWeek { get; set; }
            public bool bdHoliday { get; set; } = false;
            public List<int?> gHoliday { get; set; }
        }


        public class Reports_05
        {
            public int RowsIndex { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public float Sum_Status_11 { get; set; }
            public float Sum_Status_10 { get; set; }
            public float Sum_Status_21 { get; set; }
            public float Sum_Status_22 { get; set; }
            public float Sum_Status_23 { get; set; }
            public float Sum_Status_24 { get; set; }
            public float Sum_Status_25 { get; set; }
            public float Sum_Status_26 { get; set; }
            public float Sum { get; set; }
            public float Sum_Status_3 { get; internal set; }
            public float Sum_Status_1 { get; internal set; }

            public float Sum_Status_Early { get; internal set; }
            public float Sum_Day { get; internal set; }
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

        public class TM_Holiday
        {
            public DateTime? dHolidayStart { get; set; }
            public DateTime? dHolidayEnd { get; set; }
            public int? nTSubLevel { get; set; }
        }

        public class Days
        {
            public string Days_Name { get; set; }
            public int Days_Id { get; set; }
            public string Days_Status { get; set; }
        }

        public class ReportsData
        {
            public string EmpType;
            public int RowsIndex { get; set; }
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public List<ScanData> scanDatas { get; set; }
            public float Sum_Status_0 { get; set; }
            public float Sum_Status_1 { get; set; }
            public float Sum_Status_2 { get; set; }
            public float Sum_Status_3 { get; set; }
            public float Sum_Status_4 { get; set; }
            public float Sum_Status_5 { get; set; }
            public float Sum_Status_6 { get; set; }

            public float Sum_Status_11 { get; set; }
            public float Sum_Status_10 { get; set; }
            public float Sum_Status_21 { get; set; }
            public float Sum_Status_22 { get; set; }
            public float Sum_Status_23 { get; set; }
            public float Sum_Status_24 { get; set; }
            public float Sum_Status_25 { get; set; }
            public float Sum_Status_26 { get; set; }

            public int? Time_Id { get; set; }
            public string Time_Late
            {
                get
                {
                    double TotalMilliseconds = this.TotalMilliseconds;
                    TimeSpan time = TimeSpan.FromMilliseconds(TotalMilliseconds);
                    return time.ToString(@"hh\:mm\:ss");
                }
            }

            public int? WorkStatus { get; internal set; }
            public DateTime? DayQuit { get; internal set; }
            public DateTime? WorkInEducationDate { get; internal set; }
            internal double TotalMilliseconds { get; set; }
        }

        public class ScanData
        {
            public string Scan_StatusIn { get; set; }
            public string Scan_TimeIn { get; set; }
            public string Scan_StatusOut { get; set; }
            public string Scan_TimeOut { get; set; }
            public string Scan_Date { get; set; }
            public string Days_Status { get; set; }
            public int DayOfYear { get; set; }
        }

        public class Reports_02
        {
            public string Student_Name { get; set; }
            public string Student_Id { get; set; }
            public string ScanIn_Time { get; set; }
            public string ScanOut_Time { get; set; }
            public string ScanIn_Status { get; set; }
            public string ScanOut_Status { get; set; }
            public string TeacherName { get; set; }
            public string Code { get; internal set; }
            public string FaceUrl { get; internal set; }
            public double? Temp { get; internal set; }
            public string DeviceType { get; internal set; }
            public string Department { get; internal set; }
            public string EmpType { get; internal set; }
            public bool IsScanEarly { get; internal set; }
            public bool IsOutLate { get; internal set; }
            public string ScanInText { get; internal set; }
            public string ScanOutText { get; internal set; }
        }

        private class Time_types
        {
            public int? nDay { get; set; }
            public string cDel { get; set; }
            public int nTimeType { get; set; }
            public TimeSpan Time_Late { get; set; }
        }

        private class FaceScanModel
        {
            public int LogEmpnDay { get; set; }
            public int sEmp { get; set; }
            public string FaceScanUrl { get; set; }
            public double? Temperature { get; set; }
            public int? DeviceType { get; set; }
            public DateTime? LogEmpDate { get; set; }
            public string LogEmpType { get; set; }
            public string LogEmpScanStatus { get; set; }
            public TimeSpan? LogEmpTime { get; set; }
            public int? LeaveID { get; set; }

        }

        private class Report3Form1
        {
            public DateTime Date { get; set; }
            public string Time1 { get; set; }
            public string Status1 { get; set; }
            public string Face1 { get; set; }
            public string Device1 { get; set; }
            public string Time2 { get; set; }
            public string Status2 { get; set; }
            public string Face2 { get; set; }
            public string Device2 { get; set; }
            public string EmpType { get; set; }
            public string Department { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
            public bool IsEarly { get; internal set; }
            public bool IsLate { get; internal set; }
            public bool IsEarly2 { get; internal set; }
            public bool IsLate2 { get; internal set; }
        }
    }

    internal class employess_data
    {
        public string sName { get; set; }
        public string sLastname { get; set; }
        public int sEmp { get; set; }
        public int? nTimeType { get; set; }
        public int? nDepartmentId { get; set; }
        public string cType { get; set; }
        public string Code { get; internal set; }
        public int? WorkStatus { get; internal set; }
        public DateTime? DayQuit { get; internal set; }

        public string FaceScanUrl { get; set; }
        public double? Temperature { get; set; }
        public int? DeviceType { get; set; }

        public string Title { get; internal set; }
        public string Department { get; internal set; }
        public string EmpType { get; internal set; }
        public DateTime? WorkInEducationDate { get; internal set; }
        public int? LeaveID { get; internal set; }
    }
}