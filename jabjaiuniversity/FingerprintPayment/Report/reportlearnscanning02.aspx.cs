using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class reportlearnscanning02 : BehaviorGateway
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
                    hdfschoolname.Value = tCompany.sCompany;
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");

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
        public static string SearchPlane(string term_id, int level2)
        {
            var userData = GetUserData();

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                //term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                //var q1 = db.TTermTimeTables.Where(w => w.nTermSubLevel2 == level2 && w.nTerm == term_id).FirstOrDefault();
                //var q2 = db.TSchedules.Where(w => w.cDel == null & w.nTermTable == q1.nTermTable).Select(s => s.sPlaneID).ToList();
                //dynamic rss = new JArray(from a in db.TPlanes.Where(w => w.cDel == null)
                //                         where q2.Contains(a.sPlaneID)
                //                         group a by new { a.sPlaneID, a.sPlaneName, a.courseCode } into ag
                //                         select new JObject {
                //                      new JProperty("value",ag.Key.sPlaneID),
                //                      new JProperty("text",ag.Key .courseCode + " - " + ag.Key.sPlaneName),
                //                  });

                //term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                var q3 = (from a in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                          join b in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals b.nTerm
                          join c in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                          join d in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on c.sPlaneID equals d.sPlaneID
                          join e in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals e.nTermSubLevel2
                          where e.nTSubLevel == level2 && a.nTerm == term_id && c.cDel == null
                          group c by new { b.sTerm, b.nTerm, d.sPlaneID, d.courseCode, d.sPlaneName } into gb
                          select new
                          {
                              sPlaneID = gb.Key.sPlaneID,
                              courseCode = gb.Key.courseCode,
                              sPlaneName = gb.Key.sPlaneName,
                              sTerm = gb.Key.sTerm
                          }).ToList();

                dynamic rss = new JArray(from a in q3
                                         select new JObject {
                                             new JProperty("value",a.sPlaneID),
                                             new JProperty("text",a.courseCode + " - " + a.sPlaneName +" ( เทอม " + a.sTerm + " )"),
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

            if (!string.IsNullOrEmpty(data.daystart))
            {
                tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.LogLearnDate >= LogLearnDate && w.LogLearnDate <= LogLearnDateEnd && w.sScheduleID == userData.CompanyID).ToList();
                DayStart = LogLearnDate;
                DayEnd = LogLearnDateEnd;
            }
            else
            {
                foreach (var term_data in db.TTerms.Where(w => w.nTerm == data.term && w.SchoolID == userData.CompanyID))
                {
                    //DayStart = DateTime.Today;
                    DayEnd = DateTime.Today <= term_data.dEnd.Value ? DateTime.Today : term_data.dEnd.Value;
                }
                tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogLearnDate >= DayStart && w.LogLearnDate <= DayEnd).ToList();
            }

            #region Get List Data
            //List<TPlane> tPlane = db.TPlanes.ToList();
            List<TLogLearnTimeScan> tLogLearnTimeScanUser = tLogLearnTimeScan.Where(w => w.sUserType == "0").ToList();
            //List<TLogLearnTimeScan> tLogLearnTimeScanTeacher = tLogLearnTimeScan.Where(w => w.sUserType != "0").ToList();
            //List<JabjaiEntity.DB.TUser> tUser = db.TUsers.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            //List<TEmployee> tEmployees = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();
            

            int NumberUser = db.TUser.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).Count();
            var DataSchedule = (from a in db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                                join b in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable.Value equals b.nTermTable
                                join c in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals c.sPlaneID
                                join d in db.TTerms.Where(w => w.SchoolID == userData.CompanyID) on b.nTerm.Trim() equals d.nTerm.Trim()
                                where b.nTermSubLevel2 == data.level2 && b.nTerm == data.term
                                select new report_data
                                {
                                    sPlaneName = c.sPlaneName,
                                    sPlaneID = c.sPlaneID.ToString(),
                                    sEmp = a.sEmp,
                                    sScheduleID = a.sScheduleID,
                                    nPlaneDay = a.nPlaneDay,
                                    tStart = a.tStart,
                                    tEnd = a.tEnd,
                                    dEnd = d.dEnd,
                                    dStart = d.dStart,
                                    courseCode = c.courseCode,
                                    Status_0 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                    && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                    && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "0").Count(),

                                    Status_1 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                     && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                     && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "1").Count(),

                                    Status_2 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                     && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                     && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "3").Count(),

                                    Status_3 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                     && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                     && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "4").Count(),

                                    Status_4 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                     && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                     && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "5").Count(),

                                    Status_5 = tLogLearnTimeScanUser.Where(w => w.LogLearnType.Trim() == "0"
                                    && w.sScheduleID == a.sScheduleID && w.sID == data.student_id //&& w.sUserType == "0"
                                    && (d.dStart <= w.LogLearnDate && d.dEnd >= w.LogLearnDate) && w.LogLearnScanStatus.Trim() == "10").Count(),
                                }).ToList();

            if (!string.IsNullOrEmpty(data.plane_id))
            {
                DataSchedule = DataSchedule.Where(w => w.sPlaneID == data.plane_id).ToList();
            }

            #endregion
           

            dynamic rss = new JArray(from a in DataSchedule
                                     group a by new { a.courseCode, a.sPlaneName, a.sPlaneID } into gb
                                     select new JObject(
                                         new JProperty("planename", gb.Key.sPlaneName),
                                         new JProperty("courseCode", gb.Key.courseCode),
                                         new JProperty("planeid", gb.Key.sPlaneID),
                                         new JProperty("Status_0", gb.Sum(sum => sum.Status_0)),
                                         new JProperty("Status_1", gb.Sum(sum => sum.Status_1)),
                                         new JProperty("Status_2", gb.Sum(sum => sum.Status_2)),
                                         new JProperty("Status_3", gb.Sum(sum => sum.Status_3)),
                                         new JProperty("Status_4", gb.Sum(sum => sum.Status_4)),
                                         new JProperty("Status_5", gb.Sum(sum => sum.Status_5))
                                     ));
            return rss.ToString();
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

            if (!string.IsNullOrEmpty(data.daystart))
            {
                tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogLearnDate >= LogLearnDate && w.LogLearnDate <= LogLearnDateEnd).ToList();
                DayStart = LogLearnDate;
                DayEnd = LogLearnDateEnd;
            }
            else
            {
                foreach (var term_data in db.TTerms.Where(w => w.nTerm == data.term && w.SchoolID == userData.CompanyID))
                {
                    //DayStart = DateTime.Today;
                    DayEnd = DateTime.Today <= term_data.dEnd.Value ? DateTime.Today : term_data.dEnd.Value;
                }
                tLogLearnTimeScan = db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.LogLearnDate >= DayStart && w.LogLearnDate <= DayEnd).ToList();
            }

            #region Get List Data
            //List<TPlane> tPlane = db.TPlanes.ToList();
            List<TLogLearnTimeScan> tLogLearnTimeScanUser = tLogLearnTimeScan.Where(w => w.sUserType == "0"
            && w.LogLearnType.Trim() == "0" && (DayStart <= w.LogLearnDate && DayEnd >= w.LogLearnDate)
            && w.sID == data.student_id).ToList();
            List<TLogLearnTimeScan> tLogLearnTimeScanTeacher = tLogLearnTimeScan.Where(w => w.sUserType != "0").ToList();
            //List<JabjaiEntity.DB.TUser> tUser = db.TUsers.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            //List<TEmployee> tEmployees = db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).ToList();

            int NumberUser = db.TUser.Where(w => w.nTermSubLevel2 == data.level2 && w.cDel == null && w.SchoolID == userData.CompanyID).Count();
            var DataSchedule = (from a in db.TSchedules.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                                join b in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable.Value equals b.nTermTable
                                join c in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals c.sPlaneID
                                where b.nTermSubLevel2 == data.level2 && b.nTerm == data.term
                                select new
                                {
                                    c.sPlaneName,
                                    c.sPlaneID,
                                    a.sEmp,
                                    a.sScheduleID,
                                    a.nPlaneDay,
                                    a.tStart,
                                    a.tEnd,
                                    courseCode = c.courseCode,
                                }).ToList();

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
                        Status_0 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("0") ? 1 : 0;
                        Status_1 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("1") ? 1 : 0;
                        Status_2 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("3") ? 1 : 0;
                        Status_3 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("4") ? 1 : 0;
                        Status_4 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("5") ? 1 : 0;
                        Status_5 = LogLearnTimeIn.LogLearnScanStatus.Trim().Equals("10") ? 1 : 0;


                        _reportindividualmain.Add(new report_data
                        {
                            dScan = LogLearnDate.ToString("dd/MM/yyyy"),
                            sScheduleID = dataSchedule.sScheduleID,
                            sPlaneName = dataSchedule.sPlaneName,
                            sPlaneID = dataSchedule.courseCode,
                            Status_0 = Status_0,
                            Status_1 = Status_1,
                            Status_2 = Status_2,
                            Status_3 = Status_3,
                            Status_4 = Status_4,
                            Status_5 = Status_5,
                            timescan = LogLearnTimeIn.LogLearnTime.HasValue ? LogLearnTimeIn.LogLearnTime.Value.ToString(@"hh\:mm") : dataSchedule.tStart.Value.ToString(@"hh\:mm")
                        });

                    }
                    //else
                    //{
                    //    Status_2 = 1;
                    //}

                }
            }

            if (data.status == "0") _reportindividualmain = _reportindividualmain.Where(w => w.Status_0 == 1).ToList();
            else if (data.status == "1") _reportindividualmain = _reportindividualmain.Where(w => w.Status_1 == 1).ToList();
            else if (data.status == "2") _reportindividualmain = _reportindividualmain.Where(w => w.Status_2 == 1).ToList();
            else if (data.status == "3") _reportindividualmain = _reportindividualmain.Where(w => w.Status_3 == 1).ToList();
            else if (data.status == "4") _reportindividualmain = _reportindividualmain.Where(w => w.Status_4 == 1).ToList();
            else if (data.status == "5") _reportindividualmain = _reportindividualmain.Where(w => w.Status_5 == 1).ToList();

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
    }

    public class searchreports_data
    {
        public int student_id { get; set; }
        public string daystart { get; set; }
        public string dayend { get; set; }
        public string term { get; set; }
        public string plane_id { get; set; }
        public int level2 { get; set; }
        public string status { get; set; }
    }

    public class report_data
    {
        public string dScan { get; set; }
        public string timescan { get; set; }
        public string studentname { get; set; }
        public string sPlaneName { get; set; }
        public string sPlaneID { get; set; }
        public int sScheduleID { get; set; }
        public int Status_0 { get; set; }
        public int Status_1 { get; set; }
        public int Status_2 { get; set; }
        public int Status_3 { get; set; }
        public int Status_4 { get; set; }
        public int Status_5 { get; set; }
        public int? sEmp { get; internal set; }
        public int? nPlaneDay { get; internal set; }
        public TimeSpan? tStart { get; internal set; }
        public TimeSpan? tEnd { get; internal set; }
        public DateTime? dEnd { get; internal set; }
        public DateTime? dStart { get; internal set; }
        public string courseCode { get; internal set; }
    }

}