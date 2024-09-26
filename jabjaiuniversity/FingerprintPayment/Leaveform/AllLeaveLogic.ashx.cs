using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Leaveform
{
    /// <summary>
    /// Summary description for AllGradeLogic
    /// </summary>
    public class AllLeaveLogic : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public LeaveCount Leave(int sEmpID, DateTime? startDate, int userType,TLeaveLetter leaveLetter)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                var today = DateTime.Now;

                StudentLogic logic = new StudentLogic(_db);
                var yearTerm = logic.GetTermDATA(startDate.Value, userData);

                DateTime? yearStart = DateTime.ParseExact(startDate.Value.ToString("01/01/yyyy"), "MM/dd/yyyy", null);
                DateTime? yearEnd = DateTime.ParseExact(startDate.Value.ToString("12/31/yyyy"), "MM/dd/yyyy", null);

                if (userType == 0)
                {

                    yearStart = yearTerm.dStart;
                    yearEnd = yearTerm.dEnd;
                }

                int sickCounter = 0;
                double sickDayTotal = 0;

                int businessCounter = 0;
                double businessDayTotal = 0;

                int studyCounter = 0;
                double studyTotal = 0;

                double governmentServicesCounter = 0;
                double governmentServicesTotal = 0;

                int otherCounter = 0;
                double otherDayTotal = 0;

                var historyLeave = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID && w.writerId == sEmpID && w.LetterConfirmdate != null && w.cDel == false
                && w.letterDate <= leaveLetter.letterDate && w.letterDate >= yearStart
                && w.letterStatus == "Accept" && w.deleted != 1

                //&& (w.adminOneComfirm != "reject" && w.adminTwoComfirm != "reject" && w.adminThreeComfirm != "reject" && w.adminOneComfirm != "0" && w.adminTwoComfirm != "0" && w.adminThreeComfirm != "0"
                //&& (w.adminOneComfirm != null || w.adminTwoComfirm != null || w.adminThreeComfirm != null) && w.deleted != 1
                //)

                );

                var listHistory = historyLeave.ToList();

                foreach (var b in historyLeave.Where(w => w.startDate <= startDate))
                {
                    if (b.letterType == "ป่วย" || b.letterType == "ลาป่วย" || b.letterType == "0")
                    {

                        if (b.Season == -1 || b.Season == null)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = calDate.Value.Days + 1;
                            sickDayTotal = sickDayTotal + calDateInt;
                        }
                        else if (b.Season == 0 || b.Season == 1)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = 0.5;
                            sickDayTotal = sickDayTotal + calDateInt;
                        }
                        sickCounter += 1;
                    }
                    else if (b.letterType == "กิจส่วนตัว" || b.letterType == "ลากิจ" || b.letterType == "1")
                    {

                        if (b.Season == -1 || b.Season == null)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = calDate.Value.Days + 1;
                            businessDayTotal = businessDayTotal + calDateInt;
                        }
                        else if (b.Season == 0 || b.Season == 1)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = 0.5;
                            businessDayTotal = businessDayTotal + calDateInt;
                        }
                        businessCounter += 1;
                    }
                    else if (b.letterType == "6")
                    {

                        if (b.Season == -1 || b.Season == null)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = calDate.Value.Days + 1;
                            studyTotal = studyTotal + calDateInt;
                        }
                        else if (b.Season == 0 || b.Season == 1)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = 0.5;
                            studyTotal = studyTotal + calDateInt;
                        }
                        studyCounter += 1;
                    }
                    else if (b.letterType == "7")
                    {

                        if (b.Season == -1 || b.Season == null)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = calDate.Value.Days + 1;
                            governmentServicesTotal = governmentServicesTotal + calDateInt;
                        }
                        else if (b.Season == 0 || b.Season == 1)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = 0.5;
                            governmentServicesTotal = governmentServicesTotal + calDateInt;
                        }
                        governmentServicesCounter += 1;
                    }

                    //else if (b.letterType == "ลาพักผ่อน" || b.letterType == "3")
                    //{
                    //    if (b.Season == -1 || b.Season == null)
                    //    {
                    //        TimeSpan? calDate = (b.endDate - b.startDate);
                    //        double calDateInt = calDate.Value.Days + 1;
                    //        holidayTotal = holidayTotal + calDateInt;
                    //    }
                    //    else if (b.Season == 0 || b.Season == 1)
                    //    {
                    //        TimeSpan? calDate = (b.endDate - b.startDate);
                    //        double calDateInt = 0.5;
                    //        holidayTotal = holidayTotal + calDateInt;
                    //    }
                    //    holidayCounter = holidayCounter + 1;
                    //}
                    else
                    {
                        if (b.Season == -1 || b.Season == null)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = calDate.Value.Days + 1;
                            otherDayTotal = otherDayTotal + calDateInt;
                        }
                        else if (b.Season == 0 || b.Season == 1)
                        {
                            TimeSpan? calDate = (b.endDate - b.startDate);
                            double calDateInt = 0.5;
                            otherDayTotal = otherDayTotal + calDateInt;
                        }
                        otherCounter += 1;
                    }
                }

                LeaveCount leave = new LeaveCount();
                leave.year = startDate.Value.Year + 543;
                leave.sickCounter = sickCounter;
                leave.sickDayTotal = sickDayTotal;
                leave.businessCounter = businessCounter;
                leave.businessDayTotal = businessDayTotal;
                leave.studyCounter = studyCounter;
                leave.studyTotal = studyTotal;
                leave.governmentServicesCounter = governmentServicesCounter;
                leave.governmentServicesTotal = governmentServicesTotal;
                leave.otherCounter = otherCounter;
                leave.otherDayTotal = otherDayTotal;

                return leave;
            }
        }
        public class LeaveCount
        {
            public int year { get; set; }
            public int sickCounter { get; set; }
            public double sickDayTotal { get; set; }

            public int businessCounter { get; set; }
            public double businessDayTotal { get; set; }

            public int studyCounter { get; set; }
            public double studyTotal { get; set; }

            public double governmentServicesCounter { get; set; }
            public double governmentServicesTotal { get; set; }

            public int otherCounter { get; set; }
            public double otherDayTotal { get; set; }
        }

        public List<Admin> GetAdmins(int id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var data = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.letterId == id && w.cDel == false).FirstOrDefault();

                Admin admin;
                List<Admin> adminList = new List<Admin>();

                if (data.adminOneId != null)
                {
                    admin = new Admin();

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data.adminOneId).FirstOrDefault();
                    if (empdata != null)
                    {
                        admin.name = empdata.sName + " " + empdata.sLastname;

                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.nJobid == empdata.nJobid).FirstOrDefault();
                        admin.job = "";
                        if (jobdata != null)
                        {
                            admin.job = jobdata.jobDescription;
                        }
                    }

                    admin.dateString = "วันที่ " + data.adminOneDate?.ToString("d MMMMM yyyy", new CultureInfo("th-TH"));
                    admin.comment = data.adminOneComment;
                    adminList.Add(admin);
                }

                if (data.adminTwoId != null)
                {
                    admin = new Admin();

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data.adminTwoId).FirstOrDefault();
                    if (empdata != null)
                    {
                        admin.name = empdata.sName + " " + empdata.sLastname;

                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        admin.job = "";
                        if (jobdata != null)
                        {
                            admin.job = jobdata.jobDescription;
                        }
                    }

                    admin.dateString = "วันที่ " + data.adminTwoDate?.ToString("d MMMMM yyyy", new CultureInfo("th-TH"));
                    admin.comment = data.adminTwoComment;
                    adminList.Add(admin);
                }

                if (data.adminThreeId != null)
                {
                    admin = new Admin();

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == data.adminThreeId).FirstOrDefault();
                    if (empdata != null)
                    {
                        admin.name = empdata.sName + " " + empdata.sLastname;

                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        admin.job = "";
                        if (jobdata != null)
                        {
                            admin.job = jobdata.jobDescription;
                        }
                    }

                    admin.dateString = "วันที่ " + data.adminThreeDate?.ToString("d MMMMM yyyy", new CultureInfo("th-TH"));
                    admin.comment = data.adminThreeComment;
                    adminList.Add(admin);
                }

                return adminList;
            }
        }

        public class Admin
        {
            public string name { get; set; }
            public string job { get; set; }
            public string comment { get; set; }
            public string dateString { get; set; }
        }

        //public Tuple<string, int, int> Regrade(TGradeDetail gradeDetail, List<TGradeEdit> listGradeEdits,int chkRegrade)
        //{

        //    return Tuple.Create(label, creditPass, chkRegrade);
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}