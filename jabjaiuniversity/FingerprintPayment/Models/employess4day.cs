using JabjaiMainClass;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MasterEntity;
using Microsoft.Ajax.Utilities;

namespace FingerprintPayment.Models
{
    public class employess4day
    {
        public string employessname { get; set; }
        public int employessid { get; set; }
        public string employesstype { get; set; }
        public DateTime dayscan { get; set; }
        public int nDepartmentId { get; set; }
        public int? nTimeType { get; set; }
        public int dayOfWeek { get; set; }
        public bool bHoliday { get; set; }
        public DateTime? DayQuit { get; set; }

        public List<employess4day> getListEmployess(string sEntities, DateTime dStart, DateTime dEnd, JWTToken.userData userData)
        {
            List<employess4day> _employess4day = new List<employess4day>();
            JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));
            var tEmployess = (from a in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
                              join c1 in db.TDepartments.Where(w => w.SchoolID == userData.CompanyID) on (a.nDepartmentId ?? 0) equals c1.DepID into jac1
                              from jc1 in jac1.DefaultIfEmpty()

                              join c2 in db.TTimes.Where(w => w.SchoolID == userData.CompanyID) on a.nTimeType equals c2.nTimeType
                              join b in db.TEmpSalaries.Where(w => w.SchoolID == userData.CompanyID) on a.sEmp equals b.sEmp into jab

                              from jb in jab.DefaultIfEmpty()
                              where a.cDel == null
                              select new
                              {
                                  a.sName,
                                  a.sLastname,
                                  a.sEmp,
                                  cType = jc1 != null ? jc1.departmentName : (a.cType == "2" ? "อาจารย์" : "พนักงาน"),
                                  WorkStatus = jb == null ? 1 : (jb.WorkStatus ?? 1),
                                  DayQuit = jb == null ? null : jb.DayQuit,
                                  a.nDepartmentId,
                                  a.nTimeType,
                                  dayOfWeek = c2.nDay

                              }).ToList();

            var lUser = (from a in tEmployess
                             //where a.WorkStatus == 1 && (a.DayQuit == null || dStart <= a.DayQuit)
                         select new
                         {
                             employessname = a.sName + " " + a.sLastname,
                             employessid = a.sEmp,
                             employesstype = a.cType,
                             nDepartmentId = a.nDepartmentId,
                             nTimeType = a.nTimeType,
                             DayQuit = a.DayQuit,
                             WorkStatus = a.WorkStatus,
                             dayOfWeek = a.dayOfWeek
                         }).ToList();

            var dtHolidayYear = (from a in db.THolidays.Where(x => x.SchoolID == userData.CompanyID).ToList()
                                 where a.cDel == null
                                 && a.sHolidayType == "0"
                                 && (new List<string> { "0", "1" }).Contains(a.sWhoSeeThis)
                                 select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = 0 }).ToList();

            dtHolidayYear.AddRange((from a in db.THolidays.Where(x => x.SchoolID == userData.CompanyID)
                                    join b in db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID) on new { a.nHoliday, a.SchoolID } equals new { b.nHoliday, b.SchoolID }
                                    where a.cDel == null
                                    && a.sHolidayType == "0" //&& b.nTSubLevel == nTSubLevel
                                    && (new List<string> { "5" }).Contains(a.sWhoSeeThis)
                                    select new TM_Holiday { dHolidayStart = a.dHolidayStart, dHolidayEnd = a.dHolidayEnd, nTSubLevel = b.nTSubLevel }).ToList());

            var employeeTypes = db.TEmployeeTypes.Where(w => w.SchoolID == userData.CompanyID && w.IsDel == false).ToList();


            for (int i = 0; dStart <= dEnd.AddDays(-i); i++)
            {
                DateTime _dayscan = dEnd.AddDays(-i);
                int dayOfWeek = 0;
                switch (string.Format("{0:dddd}", _dayscan))
                {
                    case "Monday": dayOfWeek = 0; break;
                    case "Tuesday": dayOfWeek = 1; break;
                    case "Wednesday": dayOfWeek = 2; break;
                    case "Thursday": dayOfWeek = 3; break;
                    case "Friday": dayOfWeek = 4; break;
                    case "Saturday": dayOfWeek = 5; break;
                    case "Sunday": dayOfWeek = 6; break;
                }

                foreach (var datauser in lUser.Where(w => w.dayOfWeek == dayOfWeek))
                {
                    int nTSubLevel = 0;
                    int.TryParse(datauser.employesstype, out nTSubLevel);

                    if ((datauser.WorkStatus != 1) && (datauser.DayQuit == null || _dayscan > datauser.DayQuit))
                        continue;

                    var empType = employeeTypes.FirstOrDefault(f => f.nTypeId == nTSubLevel || f.nTypeId2 == nTSubLevel);
                    if (empType != null) nTSubLevel = empType.nTypeId;

                    bool _bHoliday = false;
                    if (dtHolidayYear.Count(c => c.dHolidayStart <= _dayscan && c.dHolidayEnd >= _dayscan && c.nTSubLevel == 0) > 0)
                    {
                        _bHoliday = true;
                    }
                    else if (dtHolidayYear.Count(c => c.dHolidayStart <= _dayscan && c.dHolidayEnd >= _dayscan && c.nTSubLevel == nTSubLevel) > 0)
                    {
                        _bHoliday = true;
                    }
                    else
                    {
                        _bHoliday = false;
                    }


                    _employess4day.Add(new employess4day
                    {
                        employessname = datauser.employessname,
                        employessid = datauser.employessid,
                        employesstype = datauser.employesstype,
                        dayscan = _dayscan,
                        nTimeType = datauser.nTimeType ?? 0,
                        dayOfWeek = dayOfWeek,
                        nDepartmentId = datauser.nDepartmentId ?? 0,
                        bHoliday = _bHoliday,
                        DayQuit = datauser.DayQuit
                    });
                }
            }

            return _employess4day;
        }

        public class TM_Holiday
        {
            public DateTime? dHolidayStart { get; set; }
            public DateTime? dHolidayEnd { get; set; }
            public int? nTSubLevel { get; set; }
        }

    }
}