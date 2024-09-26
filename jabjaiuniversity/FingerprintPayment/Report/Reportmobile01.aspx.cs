using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System.Web.Script.Services;
using System.Web.Services;
using System.Globalization;
using Newtonsoft.Json.Linq;
using FingerprintPayment.App_Code;
using JabjaiMainClass.Models;

namespace FingerprintPayment.Report
{
    public partial class Reportmobile01 : BehaviorGateway
    {
        public schoolData SchoolData = new schoolData();

        //private JWTToken.userData userData = new JWTToken.userData();
        protected void Page_Load(object sender, EventArgs e)
        {
            var userData = GetUserData();
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                //fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = " + userData.CompanyID + " order by numberYear desc ", "", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read));
                    var q = QueryDataBases.SubLevel_Query.GetData(dbschool, userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;


                    //var f_term = (from a in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID)
                    //              join b in dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.nYear equals b.nYear
                    //              where a.dStart <= DateTime.Today && a.dEnd >= DateTime.Today
                    //              select new
                    //              {
                    //                  a.sTerm,
                    //                  b.numberYear
                    //              }).FirstOrDefault();

                    StudentLogic logic = new StudentLogic(dbschool);
                    var f_term = logic.GetTermDATA(DateTime.Today, new JWTToken.userData { CompanyID = userData.CompanyID });
                    var f_year = dbschool.TYears.FirstOrDefault(f => f.nYear == f_term.nYear);


                    if (f_term != null)
                    {
                        SchoolData.Term = f_term.sTerm;
                        SchoolData.Year = f_year.numberYear.ToString() ?? "";
                    }

                    if (tCompany.nSchoolHeadid != null)
                    {
                        var data1 = (from a in dbschool.TEmployees
                                        .Where(w => w.sEmp == tCompany.nSchoolHeadid && w.SchoolID == userData.CompanyID)

                                     from d in dbschool.TTitleLists
                                        .Where(o => o.nTitleid + "" == a.sTitle && o.SchoolID == a.SchoolID).DefaultIfEmpty()

                                     select (d == null ? a.sTitle : d.titleDescription) + a.sName + " " + a.sLastname)
                                    .FirstOrDefault();

                        SchoolData.SchoolHeadName = data1;
                    }


                }
            }
        }

        public class schoolData
        {
            public string Term { get; set; }
            public string Year { get; set; }
            public string SchoolHeadName { get; set; }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static List<Report02User> report02UsersView01(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            DateTime dStart = DateTime.Today, dEnd = DateTime.Today;

            if (search.sort_type == 0)
            {
                dStart = search.dStart.Value;
                dEnd = search.dStart.Value;
            }
            else if (search.sort_type == 1)
            {
                dStart = search.dStart.Value;
                dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
            }
            else if (search.sort_type == 2)
            {
                //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                var f_term = db.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                dStart = f_term.dStart.Value;
                dEnd = f_term.dEnd.Value;
            }
            else
            {
                dStart = search.dStart.Value;
                dEnd = search.dEnd.HasValue ? search.dEnd.Value : dStart;
            }

            LogScanCome2SchoolLogic studentLogic = new LogScanCome2SchoolLogic(db, userData);
            List<LogScanCome2SchoolModel> studentModel = new List<LogScanCome2SchoolModel>();

            if (search.level_id.HasValue) studentModel = studentLogic.GetByClass(search.level_id.Value, dStart, dEnd);
            else studentModel = studentLogic.GetAll(dStart, dEnd);

            var q = (from a in studentModel
                     group a by new { a.DayScan } into gb

                     select new Report02User
                     {
                         day = gb.Key.DayScan.Value.ToString("dd/MM/yyyy"),
                         dayTH = gb.Key.DayScan.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                         studentnumber = studentModel.Count(c => c.DayScan == gb.Key.DayScan),
                         female_status_0 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "0"),
                         female_status_1 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "12"),
                         female_status_2 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "1"),
                         female_status_3 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "11"),
                         female_status_4 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "10"),
                         female_status_5 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "3"),
                         female_status_6 = gb.Count(s => s.Sex == "1" && s.ScanStatus == "99"),
                         male_status_0 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "0"),
                         male_status_1 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "12"),
                         male_status_2 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "1"),
                         male_status_3 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "11"),
                         male_status_4 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "10"),
                         male_status_5 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "3"),
                         male_status_6 = gb.Count(s => s.Sex == "0" && s.ScanStatus == "99"),
                     }).ToList();
            return q;
        }

        private static string getStudent_name(IEnumerable<string> ag)
        {
            var student_name = "";
            foreach (var data in ag)
            {
                student_name += (string.IsNullOrEmpty(student_name) ? "" : ",") + data;
            }
            return student_name;
        }

        private static string getStudent_name(IEnumerable<LogScanCome2SchoolModel> ag)
        {
            var student_name = "";
            int _Index = 1;
            foreach (var data in ag)
            {
                student_name += (string.IsNullOrEmpty(student_name) ? "" : ",") + data.Student_Title + " " + data.Student_Name;
            }
            return student_name;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static List<Report02UsersView01> report02UsersView02(Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(HttpContext.Current.Session["sEntities"] + "")) return null;
            string entities = HttpContext.Current.Session["sEntities"].ToString();
            var db = new JabJaiEntities(Connection.StringConnectionSchool(entities, ConnectionDB.Read));
            List<student4day> _student4day = new List<student4day>();

            DateTime dStart = DateTime.Today, dEnd = DateTime.Today;
            if (search.sort_type == 0)
            {
                dStart = search.dStart.Value;
                dEnd = search.dStart.Value;
                _student4day = new student4day().getListStudent(entities, dStart, dEnd);
            }
            else if (search.sort_type == 1)
            {
                dStart = search.dStart.Value;
                dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                _student4day = new student4day().getListStudent(entities, dStart, dEnd);
            }
            else if (search.sort_type == 2)
            {
                //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                var f_term = db.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                dStart = f_term.dStart.Value;
                dEnd = f_term.dEnd.Value;
            }
            else
            {
                dStart = search.dStart.Value;
                dEnd = search.dEnd.Value;
                _student4day = new student4day().getListStudent(entities, dStart, dEnd);
            }

            LogScanCome2SchoolLogic studentLogic = new LogScanCome2SchoolLogic(db, userData);
            List<LogScanCome2SchoolModel> studentModel = new List<LogScanCome2SchoolModel>();
            if (search.level_id.HasValue) studentModel = studentLogic.GetByClass(search.level_id.Value, dStart, dEnd);
            else studentModel = studentLogic.GetAll(dStart, dEnd);

            List<string> noteStatus = new List<string>() { "3", "10", "11", "99" };
            var q1 = (from a in studentModel
                      group a by a.DayScan into ag
                      select new Report02UsersView01
                      {
                          dayscan = ag.Key.Value.ToString("dd/MM/yyyy"),
                          level = (from a1 in ag
                                   where a1.DayScan == ag.Key
                                   group a1 by new { a1.Class_Id, a1.Class_Name, a1.sortValue } into bg
                                   orderby bg.Key.sortValue, bg.Key.Class_Name
                                   select new Level
                                   {
                                       levelname = bg.Key.Class_Name,
                                       sortValue = bg.Key.sortValue,
                                       level2 = (from a2 in bg
                                                 where a2.Class_Id == bg.Key.Class_Id
                                                 group a2 by new { a2.Room_Name, a2.Room_Id, a2.OrderId, a2.sortValue } into cg
                                                 orderby cg.Key.sortValue, cg.Key.OrderId
                                                 select new Level2
                                                 {
                                                     level2name = bg.Key.Class_Name + " / " + cg.Key.Room_Name,
                                                     level2id = cg.Key.Room_Id ?? 0,
                                                     studentnember = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(),
                                                     female_status_0 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "0"),
                                                     female_status_1 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "12"),
                                                     female_status_2 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "1"),
                                                     female_status_3 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "11"),
                                                     female_status_4 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "10"),
                                                     female_status_5 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "3"),
                                                     female_status_6 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "1" && s.ScanStatus == "99"),
                                                     male_status_0 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "0"),
                                                     male_status_1 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "12"),
                                                     male_status_2 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "1"),
                                                     male_status_3 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "11"),
                                                     male_status_4 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "10"),
                                                     male_status_5 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "3"),
                                                     male_status_6 = cg.Where(w => w.Room_Id == cg.Key.Room_Id).Count(s => s.Sex == "0" && s.ScanStatus == "99"),

                                                     note = getStudent_name(cg.Where(w => w.Room_Id == cg.Key.Room_Id && noteStatus.Contains(w.ScanStatus)).ToList()),

                                                     note2 = getStudent_name(cg.Where(w => w.Room_Id == cg.Key.Room_Id && w.ScanStatus == "1").ToList()),

                                                     note3 = getStudent_name(cg.Where(w => w.Room_Id == cg.Key.Room_Id && noteStatus.Contains(w.ScanStatus))
                                                        .Select(s => s.Student_Name).ToList()),

                                                     note4 = getStudent_name(cg.Where(w => w.Room_Id == cg.Key.Room_Id && w.ScanStatus == "1")
                                                        .Select(s => s.Student_Name).ToList()),

                                                 }).ToList()
                                   }).ToList()
                      }).ToList();
            return q1;
        }

        public class Search
        {
            public int sort_type { get; set; }
            public string term_id { get; set; }
            public int? level_id { get; set; }
            public DateTime? dStart { get; set; }
            public DateTime? dEnd { get; set; }
        }

        public class Report02UsersView01
        {
            public string dayscan { get; set; }
            public List<Level> level { get; set; }
        }

        public class Level
        {
            public string levelname { get; set; }
            public int? sortValue { get; set; }
            public List<Level2> level2 { get; set; }
        }

        public class Level2
        {
            public string level2name { get; set; }
            public int level2id { get; set; }
            public int studentnember { get; set; }
            public int female_status_0 { get; set; }
            public int female_status_1 { get; set; }
            public int female_status_2 { get; set; }
            public int female_status_3 { get; set; }
            public int female_status_4 { get; set; }
            public int female_status_5 { get; set; }
            public int female_status_6 { get; set; }
            public int male_status_0 { get; set; }
            public int male_status_1 { get; set; }
            public int male_status_2 { get; set; }
            public int male_status_3 { get; set; }
            public int male_status_4 { get; set; }
            public int male_status_5 { get; set; }
            public int male_status_6 { get; set; }
            public string note { get; set; }
            public string note2 { get; set; }
            public string note3 { get; set; }
            public string note4 { get; set; }
        }
    }
}