using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System.Data.SqlClient;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FingerprintPayment.Report
{
    public partial class reportlearnscanning03 : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                var userData = GetUserData();
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                DataTable dtYear = fcommon.LinqToDataTable(dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            var userData = GetUserData();

            JabJaiEntities _db2 = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            DataTable dtName = fcommon.LinqToDataTable(_db2.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null && (w.sName.Contains(prefixText) || w.sLastname.Contains(prefixText) || w.sIdentification.Contains(prefixText))));
            List<string> customers = new List<string>();
            if (dtName != null)
            {
                foreach (DataRow dr in dtName.Rows)
                {
                    customers.Add(dr["sName"] + " " + dr["sLastname"]);
                }
            }
            return customers;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static string SearchPlane(string trem_id, int level2)
        {
            var userData = GetUserData();

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                //trem_id = string.Format("TS{0:0000000}", int.Parse(trem_id));
                var q1 = db.TTermTimeTables.Where(w => w.nTermSubLevel2 == level2 && w.nTerm == trem_id && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var q2 = db.TSchedules.Where(w => w.cDel == null & w.nTermTable == q1.nTermTable && w.SchoolID == userData.CompanyID).Select(s => s.sPlaneID).ToList();
                var q3 = db.TPlanes.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList();

                dynamic rss = new JArray(from a in q3
                                         where q2.Contains(a.sPlaneID)
                                         group a by new { a.sPlaneID, a.sPlaneName, a.courseCode } into ag
                                         select new JObject {
                                      new JProperty("value",ag.Key.sPlaneID),
                                      new JProperty("text",ag.Key .courseCode + " - " + ag.Key.sPlaneName),
                                  });

                return rss.ToString();
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SearchReports(searchreports_data data)
        {
            var userData = GetUserData();

            JabJaiEntities db = new JabJaiEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            DateTime LogLearnDate = string.IsNullOrEmpty(data.daystart) ? DateTime.Today : DateTime.ParseExact(data.daystart, "dd/MM/yyyy", new CultureInfo("en-us"));
            DateTime LogLearnDateEnd = string.IsNullOrEmpty(data.dayend) ? LogLearnDate : DateTime.ParseExact(data.dayend, "dd/MM/yyyy", new CultureInfo("en-us"));
            //if (!string.IsNullOrEmpty(data.term)) data.term = string.Format("TS{0:0000000}", int.Parse(data.term));
            DateTime DayStart = DateTime.Today, DayEnd = DateTime.Today;
            List<TLogLearnTimeScan> tLogLearnTimeScan = new List<TLogLearnTimeScan>();

            StudentLogic logic = new StudentLogic(db);
            string TermId = "";

            string SQL = "";
            if (!string.IsNullOrEmpty(data.daystart))
            {
                //tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.LogLearnDate >= LogLearnDate && w.LogLearnDate <= LogLearnDateEnd && w.SchoolID == userData.CompanyID).ToList();
                DayStart = LogLearnDate;
                DayEnd = LogLearnDateEnd;
                TermId = logic.GetTermId(DayStart, userData);

            }
            else
            {
                foreach (var term_data in db.TTerms.Where(w => w.nTerm == data.term && w.SchoolID == userData.CompanyID))
                {
                    //DayStart = DateTime.Today;
                    DayStart = term_data.dStart.Value;
                    DayEnd = DateTime.Today <= term_data.dEnd.Value ? DateTime.Today : term_data.dEnd.Value;
                }

                TermId = logic.GetTermId(DayEnd, userData);
            }


            #region Get List Data
            //List<TPlane> tPlane = db.TPlanes.ToList();
            List<TLogLearnTimeScan> tLogLearnTimeScanUser = tLogLearnTimeScan.Where(w => w.SchoolID == userData.CompanyID && w.sUserType == "0" ).AsQueryable().ToList();
            //List<TLogLearnTimeScan> tLogLearnTimeScanTeacher = tLogLearnTimeScan.Where(w => w.sUserType != "0" && w.SchoolID == userData.CompanyID).ToList();
            //List<JabjaiEntity.DB.TUser> tUser = db.TUsers.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            var tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                         join b in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                         where a.cDel == null && b.nTermSubLevel2 == data.level2 && b.nTerm.Trim() == TermId.Trim()
                         orderby a.nStudentNumber
                         select a).AsQueryable().ToList();

            string _Comm = "";
            tUser.ForEach(f =>
            {
                _Comm += (string.IsNullOrEmpty(_Comm) ? "" : ",") + f.sID;
            });

            SQL = string.Format("select * from TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
            tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

            SQL = string.Format("select * from [JabjaiSchoolHistory].dbo.TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
            tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

            var qstudent = (from a in tUser
                            where a.cDel == null && a.SchoolID == userData.CompanyID
                            orderby a.sStudentID
                            select new data_report_01
                            {
                                student_name = a.sName + " " + a.sLastname,
                                student_code = a.sStudentID,
                                student_id = a.sID,
                                Status_0 = 0,
                                Status_1 = 0,
                                Status_2 = 0,
                                Status_3 = 0,
                                Status_4 = 0,
                                Status_5 = 0
                            }).ToList();

            int NumberUser = qstudent.Count();
            var DataSchedule = (from a in db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                                join b in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable.Value equals b.nTermTable
                                join c in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals c.sPlaneID
                                where b.nTermSubLevel2 == data.level2 && b.nTerm == data.term
                                select new { c.sPlaneName, c.sPlaneID, a.sEmp, a.sScheduleID, a.nPlaneDay, a.tStart, a.tEnd }).ToList();

            if (!string.IsNullOrEmpty(data.plane_id))
            {
                DataSchedule = DataSchedule.Where(w => w.sPlaneID.ToString() == data.plane_id).ToList();
            }

            #endregion
            List<data_report_01> _reportindividualmain = new List<data_report_01>();

            for (int i = 0; DayStart <= DayEnd.AddDays(-i); i++)
            {
                LogLearnDate = DayEnd.AddDays(-i);
                int nDay = (int)LogLearnDate.DayOfWeek;

                foreach (var dataSchedule in DataSchedule.Where(w => w.nPlaneDay == nDay).OrderBy(o => o.tStart))
                {
                    if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;


                    var LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.LogLearnType == "0"
                     && w.sScheduleID == dataSchedule.sScheduleID && w.sUserType == "0"
                     && w.LogLearnDate == LogLearnDate).ToList();

                    foreach (var StatusUpdate in qstudent)
                    {
                        var findlog = LogLearnTimeIn.Where(w => w.sID == StatusUpdate.student_id).FirstOrDefault();
                        if (findlog == null) StatusUpdate.Status_2 += 1;
                        else
                        {
                            switch (findlog.LogLearnScanStatus.Trim())
                            {
                                case "0":
                                    StatusUpdate.Status_0++;
                                    break;
                                case "1":
                                    StatusUpdate.Status_1++;
                                 break;                            
                                case "4":
                                    StatusUpdate.Status_4++;
                                    break;
                                case "5":
                                    StatusUpdate.Status_3++;
                                    break;
                                case "6":
                                    StatusUpdate.Status_5++;
                                    break;
                                default:
                                    StatusUpdate.Status_2++;
                                    break;
                            }
                            //StatusUpdate.Status_0 += findlog.LogLearnScanStatus.Trim().Equals("0") ? 1 : 0;
                            //StatusUpdate.Status_1 += findlog.LogLearnScanStatus.Trim().Equals("1") ? 1 : 0;
                            //StatusUpdate.Status_2 += findlog.LogLearnScanStatus.Trim().Equals("3") ? 1 : 0;
                            //StatusUpdate.Status_3 += findlog.LogLearnScanStatus.Trim().Equals("4") ? 1 : 0;
                            //StatusUpdate.Status_4 += findlog.LogLearnScanStatus.Trim().Equals("5") ? 1 : 0;
                            //StatusUpdate.Status_5 += findlog.LogLearnScanStatus.Trim().Equals("10") ? 1 : 0;
                        }
                    }
                }
            }

            dynamic rss = new JArray(from a in qstudent
                                     select new JObject(
                                         new JProperty("student_code", a.student_code),
                                         new JProperty("student_id", a.student_id),
                                         new JProperty("student_name", a.student_name),
                                         new JProperty("Status_0", a.Status_0),
                                         new JProperty("Status_1", a.Status_1),
                                         new JProperty("Status_2", a.Status_2),
                                         new JProperty("Status_3", a.Status_3),
                                         new JProperty("Status_4", a.Status_4),
                                         new JProperty("Status_5", a.Status_5),
                                         new JProperty("percen_Status_0", percen(a, a.Status_0)),
                                         new JProperty("percen_Status_1", percen(a, a.Status_1)),
                                         new JProperty("percen_Status_2", percen(a, a.Status_2)),
                                         new JProperty("percen_Status_3", percen(a, a.Status_3)),
                                         new JProperty("percen_Status_4", percen(a, a.Status_4)),
                                         new JProperty("percen_Status_5", percen(a, a.Status_5))
                                     ));
            return rss.ToString();
        }


        private static string percen(data_report_01 a, int StatusValues)
        {
            if (StatusValues == 0) return " (0%)";
            else return " (" + Math.Round(((StatusValues * 100.00) / (a.Status_0 + a.Status_1 + a.Status_2 + a.Status_3 + a.Status_4 + a.Status_5)), 2) + ")%";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string SearchReports_Detail(searchreports_data data)
        {
            var userData = GetUserData();

            JabJaiEntities db = new JabJaiEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read));

            DateTime LogLearnDate = string.IsNullOrEmpty(data.daystart) ? DateTime.Today : DateTime.ParseExact(data.daystart, "dd/MM/yyyy", new CultureInfo("en-us"));
            DateTime LogLearnDateEnd = string.IsNullOrEmpty(data.dayend) ? LogLearnDate : DateTime.ParseExact(data.dayend, "dd/MM/yyyy", new CultureInfo("en-us"));
            //if (!string.IsNullOrEmpty(data.term)) data.term = string.Format("TS{0:0000000}", int.Parse(data.term));
            DateTime DayStart = DateTime.Today, DayEnd = DateTime.Today;
            List<TLogLearnTimeScan> tLogLearnTimeScan = new List<TLogLearnTimeScan>();
            string SQL = "";
            StudentLogic logic = new StudentLogic(db);
            string TermId = "";

            if (!string.IsNullOrEmpty(data.daystart))
            {
                //tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.LogLearnDate >= LogLearnDate && w.LogLearnDate <= LogLearnDateEnd && w.SchoolID == userData.CompanyID).ToList();
                //SQL = string.Format("select * from TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' ", userData.CompanyID, LogLearnDate, LogLearnDateEnd);
                //tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

                //SQL = string.Format("select * from [JabjaiSchoolHistory].dbo.TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' ", userData.CompanyID, LogLearnDate, LogLearnDateEnd);
                //tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

                DayStart = LogLearnDate;
                DayEnd = LogLearnDateEnd;
                TermId = logic.GetTermId(DayStart, userData);
            }
            else
            {
                foreach (var term_data in db.TTerms.Where(w => w.nTerm == data.term && w.SchoolID == userData.CompanyID))
                {
                    //DayStart = DateTime.Today;
                    DayStart = term_data.dStart.Value;
                    DayEnd = DateTime.Today <= term_data.dEnd.Value ? DateTime.Today : term_data.dEnd.Value;
                }
                //tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.LogLearnDate >= DayStart && w.LogLearnDate <= DayEnd && w.SchoolID == userData.CompanyID).ToList();

                //SQL = string.Format("select * from TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' ", userData.CompanyID, DayStart, DayEnd);
                //tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

                //SQL = string.Format("select * from [JabjaiSchoolHistory].dbo.TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' ", userData.CompanyID, DayStart, DayEnd);
                //tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());
                TermId = logic.GetTermId(DayEnd, userData);
            }

            #region Get List Data
            //List<TPlane> tPlane = db.TPlanes.ToList();
            List<TLogLearnTimeScan> tLogLearnTimeScanUser = tLogLearnTimeScan.Where(w => w.sUserType == "0" && data.student_id == w.sID && w.LogLearnType == "0").ToList();
            //List<TLogLearnTimeScan> tLogLearnTimeScanTeacher = tLogLearnTimeScan.Where(w => w.sUserType != "0").ToList();
            //List<JabjaiEntity.DB.TUser> tUser = db.TUsers.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            //List<TEmployee> tEmployees = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
            //List<TTermTimeTable> tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).ToList();
            //var tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID);
            //List<TSchedule> tSchedule = db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            //var tSchedule = db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID);

            var tUser = (from a in db.TUser.Where(w => w.SchoolID == userData.CompanyID)
                         join b in db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                         where a.cDel == null && b.nTermSubLevel2 == data.level2 && b.nTerm.Trim() == TermId.Trim()
                         orderby a.nStudentNumber
                         select a).AsQueryable().ToList();

            string _Comm = "";
            tUser.ForEach(f =>
            {
                _Comm += (string.IsNullOrEmpty(_Comm) ? "" : ",") + f.sID;
            });

            SQL = string.Format("select * from TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
            tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

            SQL = string.Format("select * from [JabjaiSchoolHistory].dbo.TLogLearnTimeScan where SchoolID = {0} and  LogLearnDate between '{1:MM/dd/yyyy}' and '{2:MM/dd/yyyy}' AND sID IN ({3})  ", userData.CompanyID, DayStart, DayEnd, _Comm);
            tLogLearnTimeScan.AddRange(db.Database.SqlQuery<TLogLearnTimeScan>(SQL).ToList());

            int NumberUser = tUser.Count();
            var DataSchedule = (from a in db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                                join b in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable.Value equals b.nTermTable
                                join c in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals c.sPlaneID
                                where b.nTermSubLevel2 == data.level2 && b.nTerm == data.term
                                select new { c.sPlaneName, c.sPlaneID, a.sEmp, a.sScheduleID, a.nPlaneDay, a.tStart, a.tEnd }).ToList();

            if (!string.IsNullOrEmpty(data.plane_id))
            {
                DataSchedule = DataSchedule.Where(w => w.sPlaneID.ToString() == data.plane_id).ToList();
            }

            #endregion
            List<report_data> _reportindividualmain = new List<report_data>();
            for (int i = 0; DayStart <= DayEnd.AddDays(-i); i++)
            {
                LogLearnDate = DayEnd.AddDays(-i);
                int nDay = (int)LogLearnDate.DayOfWeek;

                foreach (var dataSchedule in DataSchedule.Where(w => w.nPlaneDay == nDay).OrderBy(o => o.tStart))
                {
                    if ((int)LogLearnDate.DayOfWeek != dataSchedule.nPlaneDay) continue;
                    var LogLearnTimeIn = tLogLearnTimeScanUser.Where(w => w.sScheduleID == dataSchedule.sScheduleID
                     && w.LogLearnDate == LogLearnDate).FirstOrDefault();

                    int Status_0 = 0, Status_1 = 0, Status_2 = 0, Status_3 = 0, Status_4 = 0, Status_5 = 0;
                    if (LogLearnTimeIn != null)
                    {
                        //Status_0 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("0") ? 1 : 0;
                        //Status_1 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("1") ? 1 : 0;
                        //Status_2 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("3") ? 1 : 0;
                        //Status_3 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("4") ? 1 : 0;
                        //Status_4 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("5") ? 1 : 0;
                        //Status_5 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("10") ? 1 : 0;

                        switch (LogLearnTimeIn.LogLearnScanStatus.Trim())
                        {
                            case "0":
                                Status_0++;
                                break;
                            case "1":
                                Status_1++;
                                break;
                            case "4":
                                Status_4++;
                                break;
                            case "5":
                                Status_3++;
                                break;
                            case "6":
                                Status_5++;
                                break;
                            default:
                                Status_2++;
                                break;
                        }
                    }
                    else
                    {
                        Status_2 = 1;
                    }

                    _reportindividualmain.Add(new report_data
                    {
                        dScan = LogLearnDate.ToString("dd/MM/yyyy"),
                        sScheduleID = dataSchedule.sScheduleID,
                        sPlaneName = dataSchedule.sPlaneName,
                        sPlaneID = dataSchedule.sPlaneID.ToString(),
                        Status_0 = Status_0,
                        Status_1 = Status_1,
                        Status_2 = Status_2,
                        Status_3 = Status_3,
                        Status_4 = Status_4,
                        Status_5 = Status_5,
                        timescan = dataSchedule.tStart.Value.ToString(@"hh\:mm")
                    });
                }
            }

            if (data.status == "0") _reportindividualmain = _reportindividualmain.Where(w => w.Status_0 == 1).ToList();
            else if (data.status == "1") _reportindividualmain = _reportindividualmain.Where(w => w.Status_1 == 1).ToList();
            else if (data.status == "2") _reportindividualmain = _reportindividualmain.Where(w => w.Status_2 == 1).ToList();
            else if (data.status == "5") _reportindividualmain = _reportindividualmain.Where(w => w.Status_3 == 1).ToList();
            else if (data.status == "4") _reportindividualmain = _reportindividualmain.Where(w => w.Status_4 == 1).ToList();
            else if (data.status == "6") _reportindividualmain = _reportindividualmain.Where(w => w.Status_5 == 1).ToList();

            dynamic rss = new JArray(from a in _reportindividualmain
                                     select new JObject(
                                        new JProperty("planename", a.sPlaneName),
                                        new JProperty("planeid", a.sPlaneID),
                                        new JProperty("studentname", a.studentname),
                                        new JProperty("dScan", a.dScan),
                                        new JProperty("timescan", a.timescan),
                                        new JProperty("Status_0", a.Status_0),
                                        new JProperty("Status_1", a.Status_1),
                                        new JProperty("Status_2", a.Status_2),
                                        new JProperty("Status_3", a.Status_3),
                                        new JProperty("Status_4", a.Status_4),
                                        new JProperty("Status_5", a.Status_5)
                                     ));
            return rss.ToString();
        }

        internal class data_report_01
        {
            public string student_name { get; set; }
            public int student_id { get; set; }
            public string student_code { get; set; }
            public int Status_0 { get; set; }
            public int Status_1 { get; set; }
            public int Status_2 { get; set; }
            public int Status_3 { get; set; }
            public int Status_4 { get; set; }
            public int Status_5 { get; set; }
        }
    }
}