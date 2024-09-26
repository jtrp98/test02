using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.UpdateEmpStatus
{
    /// <summary>
    /// Summary description for LogicUpdateEmpStatus
    /// </summary>
    public class LogicUpdateEmpStatus : IHttpHandler, IRequiresSessionState
    {



        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string _mode = fcommon.ReplaceInjection(context.Request["Mode"]);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                Dictionary<string, object> row;

                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();

                switch (_mode)
                {
                    #region GetEmpList
                    case "GetEmpList":
                        List<Emp> empList = new List<Emp>();
                        empList = GetTeacherList(_db);
                        foreach (var l in empList)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("EmpId", l.EmpId);
                            row.Add("Name", l.Name);
                            row.Add("Phone", l.Phone);
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region GetData
                    case "GetData":
                        List<EmpData> Data = new List<EmpData>();
                        Data = GetData(fcommon.ReplaceInjection(context.Request["empId"])
                            , _db
                               );
                        foreach (var l in Data)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("EmpId", l.EmpId);
                            row.Add("Name", l.Name);
                            row.Add("cType", l.cType);
                            row.Add("Job", l.Job);
                            row.Add("TimeType", l.TimeType);
                            row.Add("Depa", l.Depa);
                            row.Add("Birthday", l.Birthday);
                            row.Add("Pic", l.Pic);
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region EmpWorkScan
                    case "EmpWorkScan":
                        List<EmpSumScan> empSumScan = new List<EmpSumScan>();
                        empSumScan = EmpScanStatus(fcommon.ReplaceInjection(context.Request["empId"])
                            , _db
                               );
                        foreach (var l in empSumScan)
                        {
                            row = new Dictionary<string, object>();
                            row.Add("sumOnTime", l.sumOnTime);
                            row.Add("sumLate", l.sumLate);
                            row.Add("sumAbsent", l.sumAbsent);

                            row.Add("sumErrandLeave", l.sumErrandLeave);
                            row.Add("sumSickLeave", l.sumSickLeave);
                            row.Add("sumGovernmentLeave", l.sumGovernmentLeave);
                            row.Add("sumStudyLeave", l.sumStudyLeave);

                            row.Add("sumVacationLeave", l.sumVacationLeave);
                            row.Add("sumOrdinationLeave", l.sumOrdinationLeave);
                            row.Add("sumMilitaryLeave", l.sumMilitaryLeave);
                            rows.Add(row);
                        }
                        break;
                    #endregion
                    #region EmpUpdateStatus
                    case "EmpUpdateStatus":
                        UpdateStatus(fcommon.ReplaceInjection(context.Request["empId"])
                            , fcommon.ReplaceInjection(context.Request["typeScanTime"])
                            , fcommon.ReplaceInjection(context.Request["status"])
                            , fcommon.ReplaceInjection(context.Request["dateStart"])
                            , fcommon.ReplaceInjection(context.Request["dateEnd"])
                            , _db
                              );
                        break;
                        #endregion

                }
                context.Response.Expires = -1;
                context.Response.ContentType = "application/json";

                context.Response.Write(serializer.Serialize(rows));

                context.Response.End();
            }
        }

        private class Emp
        {
            public string Name { get; set; }
            public int EmpId { get; set; }
            public string Phone { get; set; }

        }

        private List<Emp> GetTeacherList(JabJaiEntities _db)
        {
            //List<Emp> setCol = new List<Emp>();

            var queryEmp = (from emp in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
                            where emp.cDel != "1"
                            select new Emp
                            {
                                Name = emp.sName + " " + emp.sLastname,
                                EmpId = emp.sEmp,
                                Phone = emp.sPhone
                            }).ToList();

            return queryEmp;
        }

        private class EmpData
        {
            public int EmpId { get; set; }
            public string Name { get; set; }
            public string cType { get; set; }
            public string Job { get; set; }
            public string TimeType { get; set; }
            public string Depa { get; set; }
            public string Birthday { get; set; }
            public string Pic { get; set; }
        }

        private List<EmpData> GetData(string empId, JabJaiEntities _db)
        {
            //List<Emp> setCol = new List<Emp>();

            int empIdInt = Int32.Parse(empId);

            var queryEmp = (from emp in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
                            join depa in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID) on emp.nDepartmentId equals depa.DepID into depa1
                            from d in depa1.DefaultIfEmpty()
                            join job in _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID) on emp.nJobid equals job.nJobid into job1
                            from j in job1.DefaultIfEmpty()
                            join tt in _db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID) on emp.nTimeType equals tt.nTimeType into tt1
                            from t in tt1.DefaultIfEmpty()
                            where emp.sEmp == empIdInt
                            select new EmpData
                            {
                                EmpId = emp.sEmp,
                                Name = emp.sName + " " + emp.sLastname,
                                Job = j.jobDescription == null ? "ยังไม่ระบุ" : j.jobDescription,
                                cType = emp.cType,
                                Depa = d.departmentName == null ? "ยังไม่ระบุ" : d.departmentName,
                                TimeType = t.sTimeType == null ? "ยังไม่ระบุ" : t.sTimeType,
                                Birthday = emp.dBirth.Value.Day.ToString() + "/" + emp.dBirth.Value.Month.ToString() + "/" + emp.dBirth.Value.Year.ToString(),
                                Pic = emp.sPicture
                            }).ToList();


            //if (queryEmp[0].cType == "1") queryEmp[0].cType = "บุคลากรทั่วไป";
            //else if (queryEmp[0].cType == "2") queryEmp[0].cType = "ครูประจำการ";
            //else if (queryEmp[0].cType == "3") queryEmp[0].cType = "บุคลากรทางการศึกษา";
            //else if (queryEmp[0].cType == "4") queryEmp[0].cType = "ครูพิเศษ";
            //else if (queryEmp[0].cType == "5") queryEmp[0].cType = "ครูพี่เลี้ยง";
            //else if (queryEmp[0].cType == "6") queryEmp[0].cType = "ผู้บริหาร";
            //else queryEmp[0].cType = "ยังไม่ระบุ";

            // string type = "";
            if (queryEmp.Count > 0)
            {
                var t = queryEmp[0].cType;
                var type = _db.TEmployeeTypes
                   .Where(o => o.SchoolID == userData.CompanyID && o.IsActive == true && o.IsDel == false && ((o.nTypeId2 ?? o.nTypeId) + "") == t)
                   .Select(o => o.Title)
                   .FirstOrDefault();

                if (string.IsNullOrEmpty(type))
                {
                    queryEmp[0].cType = "ยังไม่ระบุ";
                }
                else
                {
                    queryEmp[0].cType = type;
                }
            }

            return queryEmp;
        }


        private class EmpSumScan
        {
            public int sumOnTime = 0;
            public int sumLate = 0;
            public int sumAbsent = 0;

            public int sumErrandLeave = 0;
            public int sumSickLeave = 0;
            public int sumGovernmentLeave = 0;
            public int sumStudyLeave = 0;
            public int sumMaternityLeave = 0;

            public int sumVacationLeave = 0;
            public int sumOrdinationLeave = 0;
            public int sumMilitaryLeave = 0;
        }
        private List<EmpSumScan> EmpScanStatus(string empId, JabJaiEntities _db)
        {
            //List<Emp> setCol = new List<Emp>();

            int empIdInt = Int32.Parse(empId);

            var queryEmpScan = (from et in _db.TLogEmpTimeScans
                                where et.SchoolID == userData.CompanyID && et.sEmp == empIdInt && et.LogEmpType == "0"
                                select new
                                {
                                    et.LogEmpScanStatus,
                                    et.LogEmpDate
                                }).ToList();

            //            0 เข้าตรงเวลาตรงเวลา
            //1 สาย
            //3 ขาด
            //10 ลากิจ
            //11 ลาป่วย
            //26 ลาไปราชการ
            //25 ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัย หรือดูงาน
            //21 ลาคลอดบุตร
            //22 ลาพักผ่อน ลาพักร้อน
            //23 ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์
            //24 ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล
            int sumOnTime = 0;
            int sumLate = 0;
            int sumAbsent = 0;

            int sumErrandLeave = 0;
            int sumSickLeave = 0;
            int sumGovernmentLeave = 0;
            int sumStudyLeave = 0;
            int sumMaternityLeave = 0;

            int sumVacationLeave = 0;
            int sumOrdinationLeave = 0;
            int sumMilitaryLeave = 0;


            for (int i = 0; i < queryEmpScan.Count(); i++)
            {
                if (queryEmpScan[i].LogEmpScanStatus == "0") sumOnTime++;
                else if (queryEmpScan[i].LogEmpScanStatus == "1") sumLate++;
                else if (queryEmpScan[i].LogEmpScanStatus == "3") sumAbsent++;
                else if (queryEmpScan[i].LogEmpScanStatus == "10") sumErrandLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "11") sumSickLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "26") sumGovernmentLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "25") sumStudyLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "21") sumMaternityLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "22") sumVacationLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "23") sumOrdinationLeave++;
                else if (queryEmpScan[i].LogEmpScanStatus == "24") sumMilitaryLeave++;
            }

            EmpSumScan workScan = new EmpSumScan();
            List<EmpSumScan> listWorkScan = new List<EmpSumScan>();

            workScan.sumOnTime = sumOnTime;
            workScan.sumLate = sumOnTime;
            workScan.sumAbsent = sumAbsent;

            workScan.sumErrandLeave = sumErrandLeave;
            workScan.sumSickLeave = sumSickLeave;
            workScan.sumGovernmentLeave = sumGovernmentLeave;
            workScan.sumStudyLeave = sumStudyLeave;
            workScan.sumMaternityLeave = sumMaternityLeave;

            workScan.sumVacationLeave = sumVacationLeave;
            workScan.sumOrdinationLeave = sumOrdinationLeave;
            workScan.sumMilitaryLeave = sumMilitaryLeave;
            listWorkScan.Add(workScan);

            return listWorkScan;
        }


        private void UpdateStatus(string empId, string typeScanTime, string status, string dateStart, string dateEnd, JabJaiEntities _db)
        {
            int empIdInt = Int32.Parse(empId);
            int typeScanTimeInt = Int32.Parse(typeScanTime);
            int statusInt = Int32.Parse(status);
            DateTime dStart = DateTime.ParseExact(dateStart, "dd/MM/yyyy", new CultureInfo("en-US"));
            DateTime dEnd = DateTime.ParseExact(dateEnd, "dd/MM/yyyy", new CultureInfo("en-US"));
            var dd = DateTime.Now;
            var leaveType = _db.TLeave_Type
                .Where(o => o.SchoolID == userData.CompanyID && o.TypeID == statusInt)
                .FirstOrDefault();
    
            if (leaveType != null)
            {        
                status = leaveType.DefaultID + "";
                status = fcommon.ConvertLeaveIDTolLogID(status);
            }           

            var query = from tt in _db.TLogEmpTimeScans.Where(w => w.SchoolID == userData.CompanyID)
                        select tt;

            var queryAllSomeScan = query.Where(x => x.sEmp == empIdInt);

            List<DateTime> allDates = new List<DateTime>();
            for (DateTime date = dStart; date <= dEnd; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            TimeSpan setLogTimes0 = new TimeSpan(0, 0, 0);

            //var maxPkLogEmpScan = query.Max(x => x.nLogEmpScanID);

            var list = queryAllSomeScan.ToList();

            //int no = 0;

            for (int i = 0; i < allDates.Count(); i++)
            {

                TLogEmpTimeScan logEmpTimeScan = new TLogEmpTimeScan();
                TLogEmpTimeScan logEmpTimeScan1 = new TLogEmpTimeScan();

                DateTime dayUpdate = allDates[i];
                int dayOfWeek = (int)dayUpdate.DayOfWeek;

                var findDay = from q in queryAllSomeScan
                              where q.LogEmpDate == dayUpdate
                              select q;

                if (typeScanTime == "0" || typeScanTime == "2")
                {
                    var rowDupScanIn = findDay.Where(x => x.LogEmpType == "0").ToList();

                    if (rowDupScanIn.Count() != 0)
                    {
                        rowDupScanIn[0].LogEmpScanStatus = status;
                        rowDupScanIn[0].LogEmpTime = setLogTimes0;
                        rowDupScanIn[0].UpdatedBy = userData.UserID;
                        rowDupScanIn[0].UpdatedDate = DateTime.Now;

                        rowDupScanIn[0].LeaveID = leaveType?.TypeID;
                    }
                    else
                    {
                        //no = no + 1;
                        //maxPkLogEmpScan = maxPkLogEmpScan + no;
                        logEmpTimeScan.sEmp = empIdInt;
                        logEmpTimeScan.LogEmpTime = setLogTimes0;
                        logEmpTimeScan.LogEmpType = "0";
                        logEmpTimeScan.LogEmpScanStatus = status;
                        logEmpTimeScan.LogEmpnDay = dayOfWeek;
                        logEmpTimeScan.LogEmpDate = dayUpdate;
                        logEmpTimeScan.CreatedBy = userData.UserID;
                        logEmpTimeScan.CreatedDate = DateTime.Now;
                        //logEmpTimeScan.nYear
                        //logEmpTimeScan.nLogEmpScanID=maxPkLogEmpScan;
                        logEmpTimeScan.SchoolID = userData.CompanyID;
                        logEmpTimeScan.LeaveID = leaveType?.TypeID;

                        _db.TLogEmpTimeScans.Add(logEmpTimeScan);


                    }
                    _db.SaveChanges();

                }

                if (typeScanTime == "1" || typeScanTime == "2")
                {
                    var rowDupScanOut = findDay.Where(x => x.LogEmpType == "1").ToList();

                    if (rowDupScanOut.Count() != 0)
                    {
                        rowDupScanOut[0].LogEmpScanStatus = status;
                        rowDupScanOut[0].LogEmpTime = setLogTimes0;
                        rowDupScanOut[0].UpdatedBy = userData.UserID;
                        rowDupScanOut[0].UpdatedDate = DateTime.Now;
                        rowDupScanOut[0].LeaveID = leaveType?.TypeID;
                    }
                    else
                    {
                        //no = no + 1;
                        //maxPkLogEmpScan = maxPkLogEmpScan + no;
                        logEmpTimeScan1.sEmp = empIdInt;
                        logEmpTimeScan1.LogEmpTime = setLogTimes0;
                        logEmpTimeScan1.LogEmpType = "1";
                        logEmpTimeScan1.LogEmpScanStatus = status;
                        logEmpTimeScan1.LogEmpnDay = dayOfWeek;
                        logEmpTimeScan1.LogEmpDate = dayUpdate;
                        logEmpTimeScan1.CreatedBy = userData.UserID;
                        logEmpTimeScan1.CreatedDate = DateTime.Now;
                        //logEmpTimeScan.nYear
                        //logEmpTimeScan1.nLogEmpScanID = maxPkLogEmpScan;
                        logEmpTimeScan1.SchoolID = userData.CompanyID;
                        logEmpTimeScan1.LeaveID = leaveType?.TypeID;
                        _db.TLogEmpTimeScans.Add(logEmpTimeScan1);

                    }
                    _db.SaveChanges();
                }
            }
            int end = 1;
        }

        private List<TLogEmpTimeScan> ChecktimeScan(IQueryable<TLogEmpTimeScan> queryIdScan, DateTime dayUpdate, string typeScan)
        {
            var query = (from q in queryIdScan
                         where q.LogEmpDate == dayUpdate && q.LogEmpType == typeScan
                         select q).ToList();

            return query;
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}