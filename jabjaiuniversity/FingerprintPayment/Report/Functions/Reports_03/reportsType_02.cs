using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JabjaiEntity.DB;
using FingerprintPayment.Report.Models;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Report.Functions.Reports_03
{
    public class ReportsType_02
    {
        public static List<Reports_02> getData(Search search, JabJaiEntities dbschool, DateTime dStart, DateTime dEnd, JWTToken.userData userData)
        {

            var q_title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
            //var qTeacher = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var _company = dbschool.Database.SqlQuery<TCompany>("SELECT * FROM [JabjaiMasterSingleDB].[dbo].[TCompany] WHERE nCompany = " + userData.CompanyID).FirstOrDefault();

            StudentLogic logic = new StudentLogic(dbschool);
            string TermId = logic.GetTermId(search.dStart ?? DateTime.Today, userData).Trim();

            //var q1 = (from a in dbschool.TUsers.Where(w => w.SchoolID == userData.CompanyID).ToList()
            //          join a1 in dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == TermId).ToList() on a.sID equals a1.sID
            //          join b1 in (from b in dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
            //                      join a1 in qTeacher on b.TeacherId equals a1.sEmp into a1bj
            //                      from a1j in a1bj.DefaultIfEmpty()

            //                      where b.LogDate == dStart && b.LogType.Trim() == "0"
            //                      select new
            //                      {
            //                          sID = b.sID.Value,
            //                          b.LogTime,
            //                          b.LogScanStatus,
            //                          TeacherName = a1j == null ? "" : a1j.sName + " " + a1j.sLastname,
            //                      })
            //          on new { User_Id = a.sID }
            //          equals new { User_Id = b1.sID } into jab1
            //          from jb1 in jab1.DefaultIfEmpty()

            //          join b2 in (from b in dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
            //                      join a1 in qTeacher on b.TeacherId equals a1.sEmp into a1bj
            //                      from a1j in a1bj.DefaultIfEmpty()

            //                      where b.LogDate == dStart && b.LogType.Trim() == "1"
            //                      select new
            //                      {
            //                          sID = b.sID.Value,
            //                          b.LogTime,
            //                          b.LogScanStatus,
            //                          b.TeacherId,
            //                          TeacherName = a1j == null ? "" : a1j.sName + " " + a1j.sLastname,
            //                      })

            //          on new { User_Id = a.sID }
            //          equals new { User_Id = b2.sID } into jab2
            //          from jb2 in jab2.DefaultIfEmpty()

            //          where a.cDel == null
            //          && (a1.nTermSubLevel2 == search.level2_id || search.student_id == a.sID) && (a.nStudentStatus ?? 0) == 0
            //          //&& a1.nTerm.Trim() == TermId.Trim()
            //          orderby a.nStudentNumber, a.sStudentID
            //          select new Reports_02
            //          {
            //              Student_Name = geTitelName(q_title, a.sStudentTitle) + " " + a.sName + " " + a.sLastname,
            //              Student_Id = a.sStudentID,
            //              ScanIn_Time = jb1 == null ? "-" : (jb1.LogTime.HasValue ? jb1.LogTime.Value.ToString(@"hh\:mm") : "-"),
            //              ScanOut_Time = jb2 == null ? "-" : (jb2.LogTime.HasValue ? jb2.LogTime.Value.ToString(@"hh\:mm") : "-"),
            //              ScanIn_Status = jb1 == null ? "3" : jb1.LogScanStatus.Trim(),
            //              ScanOut_Status = jb2 == null ? "3" : jb2.LogScanStatus.Trim(),
            //              TeacherName = jb1 == null ? "" : jb1.TeacherName,
            //          }).ToList();

            string _comm = "", _Sql = "";

            if (search.student_id.HasValue) _comm = " AND A.sID = " + search.student_id;
            else if (search.level2_id.HasValue) _comm = " AND A.nTermSubLevel2 = " + search.level2_id;
            var q = new List<M_LogScan>();

            _Sql = string.Format(@"
select 	sStudentTitle ,sName,sLastname,sStudentID,MIN(LogTime_IN) 'LogTime_IN', LogScanStatus_IN
	, MAX(LogTime_OUT)'LogTime_OUT',LogScanStatus_OUT,nTermSubLevel2,
	 Emp_Name, Emp_Lastname,nStudentNumber, nStudentStatus,
	SchoolID,nTimeType   ,Temperature, FaceScanUrl , deviceType
from (
    SELECT A.sStudentTitle,A.sName,A.sLastname,A.sStudentID,ISNULL(B01.LogTime,B.LogTime) AS LogTime_IN,ISNULL(B.LogScanStatus,B01.LogScanStatus) AS LogScanStatus_IN
    ,ISNULL(C01.LogTime,C.LogTime) AS LogTime_OUT,ISNULL(C.LogScanStatus,C01.LogScanStatus) AS LogScanStatus_OUT,A.nTermSubLevel2,
    ISNULL(B1.sName,B02.sName) AS Emp_Name,ISNULL(B1.sLastname,B02.sLastname) AS Emp_Lastname,A.nStudentNumber,ISNULL(A.nStudentStatus,0)  AS nStudentStatus,
    A.SchoolID,A.nTimeType   ,ISNULL(Z1.Temperature,Z2.Temperature) 'Temperature', ISNULL(Z1.FaceScanUrl ,Z2.FaceScanUrl )'FaceScanUrl' , ISNULL(B01.deviceType,B.deviceType) 'deviceType' 
    FROM TB_StudentViews AS A 
    INNER JOIN TUser AS A2 ON A.sID = A2.sID AND A.SchoolID = A2.SchoolID 
    LEFT OUTER JOIN TLogUserTimeScan AS B ON A.sID = B.sID AND A.SchoolID = B.SchoolID 
    AND B.LogType = '0' AND B.LogDate =  '{2:d}' 
    LEFT OUTER JOIN TEmployees AS B1 ON B.TeacherId = B1.sEmp AND B.SchoolID = B1.SchoolID

    LEFT OUTER JOIN TLogUserTimeScan AS C ON A.sID = C.sID AND A.SchoolID = C.SchoolID 
    AND C.LogType = '1' AND C.LogDate =  '{2:d}' 
    LEFT OUTER JOIN TEmployees AS C1 ON C.TeacherId = C1.sEmp AND C.SchoolID = C1.SchoolID

    LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TLogUserTimeScan AS B01 ON A.sID = B01.sID 
    AND A.SchoolID = B01.SchoolID AND B01.LogType = '0' AND B01.LogDate =  '{2:d}' 
    LEFT OUTER JOIN TEmployees AS B02 ON B01.TeacherId = B02.sEmp AND B01.SchoolID = B02.SchoolID

    LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TLogUserTimeScan AS C01 ON A.sID = C01.sID 
    AND A.SchoolID = C01.SchoolID AND C01.LogType = '1' AND C01.LogDate =  '{2:d}' 
    LEFT OUTER JOIN TEmployees AS C02 ON C01.TeacherId = C02.sEmp AND C01.SchoolID = C02.SchoolID

    LEFT JOIN TFaceScanLog Z1 on Z1.sID = B.sID AND Z1.SchoolID = B.SchoolID AND  format(Z1.LogTime,'yyyyMMddHHmm') = format( CAST(B.LogDate as DATETIME) + CAST(B.LogTime as DATETIME),'yyyyMMddHHmm')
    LEFT JOIN TFaceScanLog Z2 on Z2.sID = B01.sID AND Z2.SchoolID = B01.SchoolID AND  format(Z2.LogTime,'yyyyMMddHHmm') = format( CAST(B01.LogDate as DATETIME) + CAST(B01.LogTime as DATETIME),'yyyyMMddHHmm')

    WHERE ISNULL(A.moveInDate,'19990101') <= '{2:d}' 
    AND (ISNULL(A.nStudentStatus,0) IN (0,4) OR (A.nStudentStatus = 2 AND ('{2:d}' < ISNULL(A.MoveOutDate,'{2:d}' ))))
    AND ISNULL(A.cDel,'0') <> '1'
    AND A.SchoolID =  {0}  AND A.nTerm =  '{3}' {1} 

)T
group by 	sStudentTitle,sName,sLastname,sStudentID, LogScanStatus_IN ,LogScanStatus_OUT,nTermSubLevel2, Emp_Name, Emp_Lastname,nStudentNumber, nStudentStatus, SchoolID,nTimeType   ,Temperature, FaceScanUrl , deviceType",
userData.CompanyID, _comm, dStart, TermId);


            q.AddRange(dbschool.Database.SqlQuery<M_LogScan>(_Sql).ToList());

            #region Get History LogScan Data
            //            _Sql = string.Format(@"SELECT A.sStudentTitle,A.sName,A.sLastname,A.sStudentID,B.LogTime AS LogTime_IN,B.LogScanStatus AS LogScanStatus_IN
            //,C.LogTime AS LogTime_OUT,C.LogScanStatus AS LogScanStatus_OUT,A.nTermSubLevel2,B1.sName AS Emp_Name,B1.sLastname AS Emp_Lastname,A.nStudentNumber,
            //A.SchoolID
            //FROM TB_StudentViews AS A 
            //LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TLogUserTimeScan AS B ON A.sID = B.sID AND A.SchoolID = B.SchoolID AND B.LogType = '0'
            //LEFT OUTER JOIN TEmployees AS B1 ON B.TeacherId = B1.sEmp AND B.SchoolID = B1.SchoolID

            //LEFT OUTER JOIN JabjaiSchoolHistory.dbo.TLogUserTimeScan AS C ON A.sID = C.sID AND A.SchoolID = C.SchoolID AND C.LogType = '1'
            //LEFT OUTER JOIN TEmployees AS C1 ON C.TeacherId = C1.sEmp AND C.SchoolID = C1.SchoolID

            //WHERE ISNULL(nStudentStatus,0) = 0 AND ISNULL(A.cDel,'0') <> '1' AND (B.LogDate =  '{2:d}' OR B.LogDate IS NULL) AND (C.LogDate =  '{2:d}' OR C.LogDate IS NULL) AND A.SchoolID = {0} AND A.nTerm = '{3}' {1} ",
            //userData.CompanyID, _comm, dStart, TermId);
            //            q.AddRange(dbschool.Database.SqlQuery<M_LogScan>(_Sql).ToList());

            #endregion

            //q.AddRange(dbschool.Database.SqlQuery<M_LogScan>(_Sql).ToList());

            string LogScanStatus_IN = (_company.sotfware ?? false ? "99" : "3");
            if (q != null)
            {
                var f1 = q.FirstOrDefault();
                int DayOfWeek = 0;
                int? nTimeType = 0;
                switch (string.Format("{0:dddd}", dStart))
                {
                    case "Monday": DayOfWeek = 0; break;
                    case "Tuesday": DayOfWeek = 1; break;
                    case "Wednesday": DayOfWeek = 2; break;
                    case "Thursday": DayOfWeek = 3; break;
                    case "Friday": DayOfWeek = 4; break;
                    case "Saturday": DayOfWeek = 5; break;
                    case "Sunday": DayOfWeek = 6; break;
                }
                var c_time = dbschool.TTimes.Where(w => w.SchoolID == userData.CompanyID).Count(w => w.nTimeType == f1.nTimeType && w.nDay == DayOfWeek && (w.cDel ?? "1") == "0");
                if (c_time > 0) LogScanStatus_IN = "8";
            }

            var q_HoliDay = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => ((dStart <= w.dHolidayStart && dStart <= w.dHolidayEnd)
            || (dEnd >= w.dHolidayStart && dEnd <= w.dHolidayEnd)) && w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            var q_SomeHoliyDay = dbschool.THolidaySomes.Where(w => (w.nTSubLevel == search.level2_id || w.nTSubLevel == search.level_id) && w.SchoolID == userData.CompanyID).ToList();

            var f_HoliDay = q_HoliDay.FirstOrDefault(f => dStart >= f.dHolidayStart && dStart <= f.dHolidayEnd);
            if (f_HoliDay != null && f_HoliDay.sHolidayType != "3")
            {
                if (f_HoliDay.sWhoSeeThis == "0" || f_HoliDay.sWhoSeeThis == "2")
                {
                    LogScanStatus_IN = "8";
                }
                else if (f_HoliDay.sWhoSeeThis == "4")
                {
                    var f1 = (from a in q_HoliDay
                              join b in q_SomeHoliyDay on a.nHoliday equals b.nHoliday
                              where dStart >= a.dHolidayStart && dStart <= a.dHolidayEnd
                               && b.nTSubLevel == search.level_id
                              select a).FirstOrDefault();

                    if (f1 != null)
                    {
                        LogScanStatus_IN = "8";
                    }

                }
                else if (f_HoliDay.sWhoSeeThis == "3")
                {
                    if (q_SomeHoliyDay.FirstOrDefault(f => f.nHoliday == f_HoliDay.nHoliday && f.nTSubLevel == search.level_id) != null)
                    {
                        LogScanStatus_IN = "8";
                    }
                }
            }

            if (q_HoliDay.Count(c => (dStart >= c.dHolidayStart && dStart <= c.dHolidayEnd) && (c.sHolidayType == "2" || c.sHolidayType == "0") && (c.cDel ?? "0") == "0") > 0)
            {
                LogScanStatus_IN = "8";
            }

            var q1 = (from a in q
                      orderby a.nStudentNumber, a.sStudentID.Length, a.sStudentID
                      select new Reports_02
                      {
                          Student_Name = geTitelName(q_title, a.sStudentTitle) + " " + a.sName + " " + a.sLastname,
                          Student_Id = a.sStudentID,
                          ScanIn_Time = !a.LogTime_IN.HasValue ? "-" : a.LogTime_IN.Value.ToString(@"hh\:mm"),
                          ScanOut_Time = !a.LogTime_OUT.HasValue ? "-" : a.LogTime_OUT.Value.ToString(@"hh\:mm"),
                          ScanIn_Status = string.IsNullOrEmpty(a.LogScanStatus_IN) ? LogScanStatus_IN : a.LogScanStatus_IN.Trim(),
                          ScanOut_Status = string.IsNullOrEmpty(a.LogScanStatus_OUT) ? LogScanStatus_IN : a.LogScanStatus_OUT.Trim(),
                          TeacherName = string.IsNullOrEmpty(a.Emp_Name) ? "" : a.Emp_Name + " " + a.Emp_Lastname,
                          nStudentStatus = a.nStudentStatus,
                          Temperature = a.Temperature?.ToString("#0.##"),
                          FaceScanUrl = string.IsNullOrEmpty(a.FaceScanUrl) ? "" : a.FaceScanUrl,
                          DeviceType = fcommon.GetDeviceTypeName(a.deviceType),
                      }).ToList();

            return q1;
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

        internal class M_LogScan
        {
            public string sStudentTitle { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sStudentID { get; set; }
            public TimeSpan? LogTime_IN { get; set; }
            public string LogScanStatus_IN { get; set; }
            public TimeSpan? LogTime_OUT { get; set; }
            public string LogScanStatus_OUT { get; set; }
            public string Emp_Name { get; set; }
            public string Emp_Lastname { get; set; }
            public int? nStudentNumber { get; set; }
            public int nStudentStatus { get; set; }
            public int? nTimeType { get; set; }
            public double? Temperature { get; set; }
            public string FaceScanUrl { get; set; }
            public int? deviceType { get; set; }
        }
    }
}