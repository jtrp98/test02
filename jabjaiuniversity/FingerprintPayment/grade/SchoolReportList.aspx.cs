using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class SchoolReportList : System.Web.UI.Page
    {

        [Inject]
        public ICommonService CommonService { get; set; }
        private JWTToken.userData userData = new JWTToken.userData();
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");
            btnExport.Click += new EventHandler(ExportToExcel);
            btnExport2.Click += new EventHandler(ExportToExcel2);
            btnExport3.Click += new EventHandler(ExportToExcel3);
            btnExport4.Click += new EventHandler(ExportToExcel4);
            //btnExport5.Click += new EventHandler(ExportToExcel5);
            //btnExport6.Click += new EventHandler(ExportToExcel6);


            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {

                //SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {

                    fcommon.LinqToDropDownList(_db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1).ToList(), ddlsublevel, "", "nTSubLevel", "SubLevel");
                    fcommon.LinqToDropDownList(_db.TYears.Where(x => x.SchoolID == userData.CompanyID).OrderByDescending(order => order.numberYear).ToList(), DropDownList1, "", "numberYear", "numberYear");

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;
                    if (termm == null || termm == "")
                        DropDownList2.SelectedIndex = 1;
                    string idlv = Request.QueryString["idlv"];
                    ddlsublevel.SelectedValue = idlv;
                    string idlv2 = Request.QueryString["idlv2"];
                    if (string.IsNullOrEmpty(idlv2))
                        ddlsublevel2.SelectedIndex = 1;
                    else ddlsublevel2.SelectedValue = idlv2;

                    txtddl2.Text = idlv2;
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    OpenData(_db, entities);
                }
            }
        }

        private void OpenData(JabJaiEntities dbschool, string entities)
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];

            List<PlanList> planlist = new List<PlanList>();

            //List<sortPlan> sortPlanList = new List<sortPlan>();
            //sortPlan sortPlan = new sortPlan();

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            //JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());

            if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
            {
                int? idlv2n = Int32.Parse(idlv2);
                int idlvn = int.Parse(idlv);
                int? useryear = Int32.Parse(year);
                var f_term = GetTerm(useryear, term, dbschool, userData.CompanyID);
                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                int number = 1;

                int nyear = 0;
                string nterm = "";
                foreach (var ff in dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.numberYear == useryear))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.sTerm == "1" && w.nYear == nyear && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }


                var sign1 = dbschool.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n && w.nTerm == nterm).FirstOrDefault();
                if (sign1 != null)
                {
                    if (sign1.nTeacherHeadid != null)
                    {
                        var semp = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == sign1.nTeacherHeadid).FirstOrDefault();
                        int g1;
                        string title = "";
                        bool g1n = int.TryParse(semp.sTitle, out g1);
                        if (g1n == true)
                        {
                            var emptitle = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.nTitleid == g1).FirstOrDefault();
                            title = emptitle.titleDescription;
                        }
                        else title = semp.sTitle;

                        signname1.Text = title + semp.sName + " " + semp.sLastname;
                    }

                }

                if (tCompany.nSchoolHeadid != null)
                {
                    var semp = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == tCompany.nSchoolHeadid).FirstOrDefault();
                    int g1;
                    string title = "";
                    bool g1n = int.TryParse(semp.sTitle, out g1);
                    if (g1n == true)
                    {
                        var emptitle = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.nTitleid == g1).FirstOrDefault();
                        title = emptitle.titleDescription;
                    }
                    else title = semp.sTitle;

                    signname2.Text = title + semp.sName + " " + semp.sLastname;
                }
                else signname2.Text = tCompany.SchoolHeadName + " " + tCompany.SchoolHeadLastname;

                signjob.Text = "ผู้อำนวยการ" + tCompany.sCompany;

                bool isNumeric;
                int n;
                if (tCompany.nRegistraDirectorid != null)
                {
                    var emp = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == tCompany.nRegistraDirectorid).FirstOrDefault();
                    var emptitle = "";
                    if (emp != null)
                    {
                        isNumeric = int.TryParse(emp.sTitle, out n);
                        if (isNumeric == true)
                        {
                            var title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.nTitleid == n).FirstOrDefault();
                            emptitle = title.titleDescription;
                        }
                        else emptitle = emp.sTitle;

                        signname3.Text = emptitle + emp.sName + " " + emp.sLastname;
                    }
                }
                job3.Text = "หัวหน้าฝ่ายวัดผลและประเมินผล";

                if (tCompany.nAcademicDirectorid != null)
                {
                    var emp = dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == tCompany.nAcademicDirectorid).FirstOrDefault();
                    var emptitle = "";
                    if (emp != null)
                    {
                        isNumeric = int.TryParse(emp.sTitle, out n);
                        if (isNumeric == true)
                        {
                            var title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.nTitleid == n).FirstOrDefault();
                            emptitle = title.titleDescription;
                        }
                        else emptitle = emp.sTitle;

                        signname4.Text = emptitle + emp.sName + " " + emp.sLastname;
                    }
                }
                job4.Text = "หัวหน้าฝ่ายวิชาการ";

                var q_planes = ServiceHelper.GetPlanCourses(idlvn, (idlv2n != null) ? (int)idlv2n : 0, f_term.nTerm.Trim(), nyear, userData.CompanyID, dbschool);
                var studentGradeInfo = ServiceHelper.GetStudentGradeInfoCommon(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, idlvn, q_planes, 0);
                if (studentGradeInfo != null && q_planes != null)
                {
                    string[] planid = GetPlanListsNew(studentGradeInfo, true, q_planes);

                    var stdlist2 = ServiceHelper.GetXMLStudentParameter(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, null).ToList().Select(s => s.SId).Distinct().ToList();
                    int countMaxScore = q_planes.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));
                    foreach (var data2 in stdlist2)
                    {
                        var data = studentGradeInfo.Where(w => w.sID == data2).ToList();
                        if (data != null && data.Count() > 0)
                        {
                            double sumall = 0;
                            double? maxweight = 0;
                            double? sumweight = 0;
                            PlanList plan = getPlanList(data.FirstOrDefault(), null, userData.CompanyID);

                            int index = 1;
                            foreach (var f_plan in planid)
                            {
                                var f_grades = data.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan && w.sID == data2);
                                if (f_grades != null)
                                {
                                    var result = new Grades_Result();

                                    sumall += Double.Parse(!string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100 : "0");
                                    maxweight += f_grades.NCredit;
                                    sumweight += f_grades.GradeXNCredit;


                                    if (f_grades.getSpecial != "-1" && (f_grades.getScore100 == "0" || f_grades.getScore100 == ""))
                                    {
                                        result.score = "\\" + f_grades.getGradeLabel;

                                    }
                                    else if (f_grades.getSpecial == "-1" && string.IsNullOrEmpty(f_grades.getScore100) && !string.IsNullOrEmpty(f_grades.getGradeLabel))
                                    {
                                        result.score = "\\" + f_grades.getGradeLabel;
                                    }
                                    else
                                    {
                                        result.score = f_grades.getScore100;
                                        if (f_grades.GradeSet != null)
                                        {
                                            result.score = string.Format("{0}/{1}", f_grades.getScore100Display, f_grades.getGradeLabel);
                                        }
                                    }

                                    result.label = !string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100Display : "" + "\\" + f_grades.getGradeLabel;
                                    result.grade = f_grades.getGradeLabel;

                                    typeof(PlanList).GetProperty("score" + index).SetValue(plan, !string.IsNullOrEmpty(result.score) ? result.score : "", null);
                                    typeof(PlanList).GetProperty("label" + index).SetValue(plan, result.label, null);
                                    typeof(PlanList).GetProperty("grade" + index).SetValue(plan, result.grade, null);
                                }
                                index++;
                            }

                            plan.scoreall = countMaxScore * 100;
                            plan.scoreget = sumall;
                            plan.sumweight = sumweight;
                            plan.maxweight = maxweight;

                            plan.grade = CalGrade(maxweight, sumweight);

                            number = number + 1;
                            planlist.Add(plan);
                        }
                        else
                        {
                            var tuser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == data2).FirstOrDefault();
                            if (tuser != null)
                            {
                                int nTitleid;
                                int.TryParse(tuser.sStudentTitle, out nTitleid);
                                var f_title = dbschool.TTitleLists.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.titleDescription == tuser.sStudentTitle || f.nTitleid == nTitleid);

                                PlanList plan = getPlanList(tuser, (f_title != null && f_title.titleDescription != null) ? f_title.titleDescription : string.Empty, userData.CompanyID);
                                planlist.Add(plan);

                            }
                        }
                    }
                }
                //}
            }

            planlist = List_Sorting(planlist);
            dgd2.DataSource = planlist;
            dgd2.PageSize = 999;
            dgd2.DataBind();

            dgd3.DataSource = planlist;
            dgd3.PageSize = 999;
            dgd3.DataBind();

            dgd4.DataSource = planlist;
            dgd4.PageSize = 999;
            dgd4.DataBind();

            dgd5.DataSource = planlist;
            dgd5.PageSize = 999;
            dgd5.DataBind();
        }

        public string getSpecial(string special, string grade)
        {
            string specialLebel = "";

            if (!string.IsNullOrEmpty(special.Trim()))
            {
                if (special == "1")
                    specialLebel = "ร";
                else if (special == "2")
                    specialLebel = "มส";
                else if (special == "3")
                    specialLebel = "มก";
                else if (special == "4")
                    specialLebel = "ผ";
                else if (special == "5")
                    specialLebel = "มผ";
                else if (special == "6")
                    specialLebel = "อื่นๆ";
                else if (special == "7")
                    specialLebel = "ขร";
                else if (special == "8")
                    specialLebel = "ขส";
                else if (special == "9")
                    specialLebel = "ท";
                else if (special == "10")
                    specialLebel = "ดีเยี่ยม";
                else if (special == "11")
                    specialLebel = "ดี";
                else if (special == "12")
                    specialLebel = "พอใช้";
                else if (special == "13")
                    specialLebel = "ปรับปรุง";
                else specialLebel = grade;
            }
            else specialLebel = "0";

            return specialLebel;
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GradeReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                //To Export all pages
                GridView1.AllowPaging = false;

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                {
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                    {

                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];
                        string year = Request.QueryString["year"];
                        string term = Request.QueryString["term"];

                        List<PlanList> planlist = new List<PlanList>();
                        if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                        {
                            int idlvn = int.Parse(idlv);
                            int? idlv2n = Int32.Parse(idlv2);
                            int? useryear = Int32.Parse(year);
                            var f_term = GetTerm(useryear, term, dbschool, userData.CompanyID);
                            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();

                            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, userData.CompanyID, userData.UserID);

                            var q_planes = ServiceHelper.GetPlanCourses(idlvn, (idlv2n != null) ? (int)idlv2n : 0, f_term.nTerm.Trim(), (yearDTO != null) ? yearDTO.NYear : 0, userData.CompanyID, dbschool);
                            var studentGradeInfo = ServiceHelper.GetStudentGradeInfoCommon(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, idlvn, q_planes, 0);
                            if (studentGradeInfo != null && q_planes != null)
                            {
                                string[] planid = GetPlanListsNew(studentGradeInfo, true, q_planes);

                                //int countMaxScore = q_planes.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));

                                //int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                //                     join g in studentGradeInfo on p.SPlaneId equals g.sPlaneID
                                //                     select g.sPlaneID).Distinct().Count();

                                var stdlist2 = ServiceHelper.GetXMLStudentParameter(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, null).ToList().Select(s => s.SId).Distinct().ToList();
                                foreach (var data2 in stdlist2)
                                {
                                    int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                                         join g in studentGradeInfo.Where(w => w.sID == data2).ToList() on new { sPlaneID = p.SPlaneId, p.NTerm, nTermSubLevel2 = p.nTermSubLevel2 } equals new { sPlaneID = g.sPlaneID ?? 0, g.NTerm, nTermSubLevel2 = g.nTermSubLevel2 ?? 0 }
                                                         select g.sPlaneID).Distinct().Count();

                                    var data = studentGradeInfo.Where(w => w.sID == data2).ToList();
                                    if (data != null && data.Count > 0)
                                    {
                                        double sumall = 0;
                                        double? maxweight = 0;
                                        double? sumweight = 0;
                                        PlanList plan = getPlanList(data.FirstOrDefault(), null, userData.CompanyID);

                                        int index = 1;
                                        foreach (var f_plan in planid)
                                        {
                                            var f_grades = data.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan && w.sID == data2);
                                            if (f_grades != null)
                                            {
                                                var result = new Grades_Result();
                                                if (q_planes.Where(w => w.SPlaneId.ToString() == f_plan && (w.CourseGroupName == "รายวิชาพื้นฐาน" || w.CourseGroupName == "รายวิชาเพิ่มเติม")).Count() > 0)
                                                {
                                                    sumall += Double.Parse(!string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100 : "0");
                                                    maxweight += f_grades.NCredit;
                                                    sumweight += f_grades.GradeXNCredit;
                                                }

                                                if (f_grades.getSpecial != "-1" && f_grades.getScore100 == "0")
                                                {
                                                    result.score = f_grades.getGradeLabel;
                                                    result.label = f_grades.getGradeLabel;
                                                }
                                                else
                                                {
                                                    result.score = f_grades.getScore100;
                                                    if (f_grades.GradeSet != null)
                                                    {
                                                        result.score = string.Format("{0}/{1}", f_grades.getScore100Display, f_grades.getGradeLabel);
                                                    }

                                                    if (!string.IsNullOrEmpty(f_grades.getGradeLabel) && !string.IsNullOrEmpty(f_grades.getScore100))
                                                    {
                                                        result.label = f_grades.getScore100Display + "\\" + f_grades.getGradeLabel;
                                                    }
                                                    else if (!string.IsNullOrEmpty(f_grades.getScore100))
                                                    {
                                                        result.label = f_grades.getScore100Display;
                                                    }
                                                    else
                                                    {
                                                        result.label = "" + "\\" + f_grades.getGradeLabel;
                                                    }
                                                }


                                                result.grade = f_grades.getGradeLabel;

                                                //}

                                                typeof(PlanList).GetProperty("score" + index).SetValue(plan, !string.IsNullOrEmpty(result.score) ? result.score : "", null);
                                                typeof(PlanList).GetProperty("label" + index).SetValue(plan, result.label, null);
                                                typeof(PlanList).GetProperty("grade" + index).SetValue(plan, result.grade, null);
                                            }
                                            index++;
                                        }

                                        plan.scoreall = countMaxScore * 100;
                                        plan.scoreget = sumall;
                                        plan.sumweight = sumweight;
                                        plan.maxweight = maxweight;

                                        plan.grade = CalGrade(maxweight, sumweight);


                                        planlist.Add(plan);
                                    }
                                    else
                                    {
                                        var tuser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == data2).FirstOrDefault();
                                        if (tuser != null)
                                        {
                                            int nTitleid;
                                            int.TryParse(tuser.sStudentTitle, out nTitleid);
                                            var f_title = dbschool.TTitleLists.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.titleDescription == tuser.sStudentTitle || f.nTitleid == nTitleid);

                                            PlanList plan = getPlanList(tuser, (f_title != null && f_title.titleDescription != null) ? f_title.titleDescription : string.Empty, userData.CompanyID);
                                            planlist.Add(plan);

                                        }
                                    }
                                }

                                var newSortList = List_Sorting(planlist);

                                GridView1.DataSource = newSortList;
                                GridView1.PageSize = 999;
                                GridView1.DataBind();

                                var room = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                                var sub = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();
                                SetHeader(heaerType_1, GridView1, q_planes, sub.SubLevel + " / " + room.nTSubLevel2, tCompany.sCompany, int.Parse(registerstudy.Text), planid).RenderControl(hw);
                            }


                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

        protected void ExportToExcel2(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GradeReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                {
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                    {

                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];
                        string year = Request.QueryString["year"];
                        string term = Request.QueryString["term"];

                        List<PlanList> planlist = new List<PlanList>();

                        if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                        {
                            int? idlv2n = Int32.Parse(idlv2);

                            int? useryear = Int32.Parse(year);
                            var f_term = GetTerm(useryear, term, dbschool, userData.CompanyID);

                            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                            int number = 1;

                            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, userData.CompanyID, userData.UserID);

                            int idlvn = int.Parse(idlv);

                            var q_planes = ServiceHelper.GetPlanCourses(idlvn, (idlv2n != null) ? (int)idlv2n : 0, f_term.nTerm.Trim(), (yearDTO != null) ? yearDTO.NYear : 0, userData.CompanyID, dbschool);

                            var studentGradeInfo = ServiceHelper.GetStudentGradeInfoCommon(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, idlvn, q_planes, 0);
                            if (studentGradeInfo != null && q_planes != null)
                            {
                                string[] planid = GetPlanListsNew(studentGradeInfo, true, q_planes);
                                //var planid = studentGradeInfo.Select(s => s.sPlaneID).Distinct().ToList();

                                //int countMaxScore = q_planes.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));


                                //var stdlist2 = studentGradeInfo.Select(s => s.sID).Distinct().ToList();
                                var stdlist2 = ServiceHelper.GetXMLStudentParameter(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, null).ToList().Select(s => s.SId).Distinct().ToList();

                                foreach (var data2 in stdlist2)
                                {

                                    int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                                         join g in studentGradeInfo.Where(w => w.sID == data2).ToList() on new { sPlaneID = p.SPlaneId, p.NTerm, nTermSubLevel2 = p.nTermSubLevel2 } equals new { sPlaneID = g.sPlaneID ?? 0, g.NTerm, nTermSubLevel2 = g.nTermSubLevel2 ?? 0 }
                                                         select g.sPlaneID).Distinct().Count();

                                    var data = studentGradeInfo.Where(w => w.sID == data2).ToList();
                                    //var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
                                    if (data != null && data.Count > 0)
                                    {
                                        double sumall = 0;
                                        double? maxweight = 0;
                                        double? sumweight = 0;
                                        PlanList plan = getPlanList(data.FirstOrDefault(), null, userData.CompanyID);



                                        int index = 1;
                                        foreach (var f_plan in planid)
                                        {
                                            var f_grades = data.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan && w.sID == data2);
                                            if (f_grades != null)
                                            {
                                                var result = new Grades_Result();
                                                if (q_planes.Where(w => w.SPlaneId.ToString() == f_plan && (w.CourseGroupName == "รายวิชาพื้นฐาน" || w.CourseGroupName == "รายวิชาเพิ่มเติม")).Count() > 0)
                                                {
                                                    sumall += Double.Parse(!string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100 : "0");
                                                    maxweight += f_grades.NCredit;
                                                    sumweight += f_grades.GradeXNCredit;
                                                }
                                                if (f_grades.getSpecial != "-1" && f_grades.getScore100 == "0")
                                                {
                                                    result.score = f_grades.getGradeLabel;
                                                }
                                                else
                                                {
                                                    result.score = f_grades.getScore100;
                                                    if (f_grades.GradeSet != null)
                                                    {
                                                        result.score = string.Format("{0}/{1}", f_grades.getScore100Display, f_grades.getGradeLabel);
                                                    }

                                                    result.label = !string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100Display : "" + "\\" + f_grades.getGradeLabel;
                                                    result.grade = f_grades.getGradeLabel;
                                                }

                                                typeof(PlanList).GetProperty("score" + index).SetValue(plan, !string.IsNullOrEmpty(result.score) ? result.score : "", null);
                                                typeof(PlanList).GetProperty("label" + index).SetValue(plan, result.label, null);
                                                typeof(PlanList).GetProperty("grade" + index).SetValue(plan, result.grade, null);
                                            }
                                            index++;
                                        }

                                        plan.scoreall = countMaxScore * 100;
                                        plan.scoreget = sumall;
                                        plan.sumweight = sumweight;
                                        plan.maxweight = maxweight;

                                        plan.grade = CalGrade(maxweight, sumweight);

                                        number = number + 1;
                                        planlist.Add(plan);
                                    }
                                    else
                                    {
                                        var tuser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == data2).FirstOrDefault();
                                        if (tuser != null)
                                        {
                                            int nTitleid;
                                            int.TryParse(tuser.sStudentTitle, out nTitleid);
                                            var f_title = dbschool.TTitleLists.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.titleDescription == tuser.sStudentTitle || f.nTitleid == nTitleid);

                                            PlanList plan = getPlanList(tuser, (f_title != null && f_title.titleDescription != null) ? f_title.titleDescription : string.Empty, userData.CompanyID);
                                            planlist.Add(plan);

                                        }
                                    }
                                }

                                var newSortList = List_Sorting(planlist);

                                GridView2.DataSource = newSortList;
                                GridView2.PageSize = 999;
                                GridView2.DataBind();

                                var room = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                                var sub = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();
                                SetHeader(heaerType_1, GridView2, q_planes, sub.SubLevel + " / " + room.nTSubLevel2, tCompany.sCompany, int.Parse(registerstudy.Text), planid).RenderControl(hw);
                            }

                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();

                        }
                    }
                }
            }
        }

        protected void ExportToExcel3(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GradeReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                {
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                    {

                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];
                        string year = Request.QueryString["year"];
                        string term = Request.QueryString["term"];

                        List<PlanList> planlist = new List<PlanList>();

                        if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                        {
                            int? idlv2n = Int32.Parse(idlv2);
                            int? useryear = Int32.Parse(year);
                            var f_term = GetTerm(useryear, term, dbschool, userData.CompanyID);
                            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                            int number = 1;


                            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, userData.CompanyID, userData.UserID);

                            int idlvn = int.Parse(idlv);

                            var q_planes = ServiceHelper.GetPlanCourses(idlvn, (idlv2n != null) ? (int)idlv2n : 0, f_term.nTerm.Trim(), (yearDTO != null) ? yearDTO.NYear : 0, userData.CompanyID, dbschool);


                            var studentGradeInfo = ServiceHelper.GetStudentGradeInfoCommon(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, idlvn, q_planes, 0);
                            if (studentGradeInfo != null && q_planes != null)
                            {
                                string[] planid = GetPlanListsNew(studentGradeInfo, true, q_planes);

                                //int countMaxScore = q_planes.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));

                                //int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                //                     join g in studentGradeInfo on p.SPlaneId equals g.sPlaneID
                                //                     select g.sPlaneID).Distinct().Count();
                                //var stdlist2 = studentGradeInfo.Select(s => s.sID).Distinct().ToList();

                                var stdlist2 = ServiceHelper.GetXMLStudentParameter(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, null).ToList().Select(s => s.SId).Distinct().ToList();
                                foreach (var data2 in stdlist2)
                                {
                                    int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                                         join g in studentGradeInfo.Where(w => w.sID == data2) on new { sPlaneID = p.SPlaneId, p.NTerm, nTermSubLevel2 = p.nTermSubLevel2 } equals new { sPlaneID = g.sPlaneID ?? 0, g.NTerm, nTermSubLevel2 = g.nTermSubLevel2 ?? 0 }
                                                         select g.sPlaneID).Distinct().Count();

                                    var data = studentGradeInfo.Where(w => w.sID == data2).ToList();
                                    if (data != null && data.Count > 0)
                                    {
                                        double sumall = 0;
                                        double? maxweight = 0;
                                        double? sumweight = 0;
                                        PlanList plan = getPlanList(data.FirstOrDefault(), null, userData.CompanyID);

                                        int index = 1;
                                        foreach (var f_plan in planid)
                                        {
                                            var f_grades = data.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan && w.sID == data2);
                                            if (f_grades != null)
                                            {
                                                var result = new Grades_Result();

                                                if (q_planes.Where(w => w.SPlaneId.ToString() == f_plan && (w.CourseGroupName == "รายวิชาพื้นฐาน" || w.CourseGroupName == "รายวิชาเพิ่มเติม")).Count() > 0)
                                                {
                                                    sumall += Double.Parse(!string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100 : "0");
                                                    maxweight += f_grades.NCredit;
                                                    sumweight += f_grades.GradeXNCredit;
                                                }

                                                if (f_grades.getSpecial != "-1" && f_grades.getScore100 == "0")
                                                {
                                                    result.score = f_grades.getGradeLabel;
                                                }
                                                else
                                                {
                                                    result.score = f_grades.getScore100;
                                                    if (f_grades.GradeSet != null)
                                                    {
                                                        result.score = string.Format("{0}/{1}", f_grades.getScore100Display, f_grades.getGradeLabel);
                                                    }
                                                }

                                                result.label = !string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100Display : "" + "\\" + f_grades.getGradeLabel;
                                                result.grade = f_grades.getGradeLabel;

                                                typeof(PlanList).GetProperty("score" + index).SetValue(plan, !string.IsNullOrEmpty(result.score) ? result.score : "", null);
                                                typeof(PlanList).GetProperty("label" + index).SetValue(plan, result.label, null);
                                                typeof(PlanList).GetProperty("grade" + index).SetValue(plan, result.grade, null);
                                            }
                                            index++;
                                        }

                                        plan.scoreall = countMaxScore * 100;
                                        plan.scoreget = sumall;
                                        plan.sumweight = sumweight;
                                        plan.maxweight = maxweight;

                                        plan.grade = CalGrade(maxweight, sumweight);
                                        plan.percentage = Convert.ToDouble(ServiceHelper.GetGradeAverage(Math.Round((sumall / (countMaxScore * 100)) * 100, 2)));
                                        number = number + 1;
                                        planlist.Add(plan);
                                    }
                                    else
                                    {
                                        var tuser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == data2).FirstOrDefault();
                                        if (tuser != null)
                                        {
                                            int nTitleid;
                                            int.TryParse(tuser.sStudentTitle, out nTitleid);
                                            var f_title = dbschool.TTitleLists.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.titleDescription == tuser.sStudentTitle || f.nTitleid == nTitleid);

                                            PlanList plan = getPlanList(tuser, (f_title != null && f_title.titleDescription != null) ? f_title.titleDescription : string.Empty, userData.CompanyID);
                                            planlist.Add(plan);
                                        }
                                    }
                                }

                                var newSortList = List_Sorting(planlist);
                                newSortList.ForEach(f => f.percentage = f.grade);

                                GridView4.DataSource = newSortList;
                                GridView4.PageSize = 999;
                                GridView4.DataBind();

                                var room = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                                var sub = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();
                                SetHeader(heaerType_1, GridView4, q_planes, sub.SubLevel + " / " + room.nTSubLevel2, tCompany.sCompany, int.Parse(registerstudy.Text), planid).RenderControl(hw);
                            }

                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

        protected void ExportToExcel4(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GradeReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                {
                    string entities = HttpContext.Current.Session["sEntities"].ToString();
                    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                    {

                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];
                        string year = Request.QueryString["year"];
                        string term = Request.QueryString["term"];

                        List<PlanList> planlist = new List<PlanList>();

                        if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                        {
                            int? idlv2n = Int32.Parse(idlv2);
                            int? useryear = Int32.Parse(year);
                            var f_term = GetTerm(useryear, term, dbschool, userData.CompanyID);

                            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();
                            int number = 1;


                            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, userData.CompanyID, Utils.GetUserId());

                            int idlvn = int.Parse(idlv);

                            var q_planes = ServiceHelper.GetPlanCourses(idlvn, (idlv2n != null) ? (int)idlv2n : 0, f_term.nTerm.Trim(), (yearDTO != null) ? yearDTO.NYear : 0, userData.CompanyID, dbschool);


                            var studentGradeInfo = ServiceHelper.GetStudentGradeInfoCommon(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, idlvn, q_planes, 0);
                            if (studentGradeInfo != null && q_planes != null)
                            {
                                string[] planid = GetPlanListsNew(studentGradeInfo, true, q_planes);
                                //var planid = studentGradeInfo.Select(s => s.sPlaneID).Distinct().ToList();

                                //int countMaxScore = q_planes.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));

                                //int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                //                     join g in studentGradeInfo on p.SPlaneId equals g.sPlaneID
                                //                     select g.sPlaneID).Distinct().Count();

                                //var stdlist2 = studentGradeInfo.Select(s => s.sID).Distinct().ToList();
                                var stdlist2 = ServiceHelper.GetXMLStudentParameter(userData.CompanyID, f_term.nTerm, idlv2n ?? 0, null).ToList().Select(s => s.SId).Distinct().ToList();
                                foreach (var data2 in stdlist2)
                                {
                                    int countMaxScore = (from p in q_planes.Where(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"))
                                                         join g in studentGradeInfo.Where(w => w.sID == data2) on new { sPlaneID = p.SPlaneId, p.NTerm, nTermSubLevel2 = p.nTermSubLevel2 } equals new { sPlaneID = g.sPlaneID ?? 0, g.NTerm, nTermSubLevel2 = g.nTermSubLevel2 ?? 0 }
                                                         select g.sPlaneID).Distinct().Count();


                                    var data = studentGradeInfo.Where(w => w.sID == data2).ToList();
                                    //var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
                                    if (data != null && data.Count > 0)
                                    {
                                        double sumall = 0;
                                        double? maxweight = 0;
                                        double? sumweight = 0;
                                        PlanList plan = getPlanList(data.FirstOrDefault(), null, userData.CompanyID);



                                        int index = 1;
                                        foreach (var f_plan in planid)
                                        {
                                            var f_grades = data.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan && w.sID == data2);
                                            if (f_grades != null)
                                            {
                                                var result = new Grades_Result();

                                                if (q_planes.Where(w => w.SPlaneId.ToString() == f_plan && (w.CourseGroupName == "รายวิชาพื้นฐาน" || w.CourseGroupName == "รายวิชาเพิ่มเติม")).Count() > 0)
                                                {
                                                    sumall += Double.Parse(!string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100 : "0");
                                                    maxweight += f_grades.NCredit;
                                                    sumweight += f_grades.GradeXNCredit;
                                                }

                                                //if (f_grades.getSpecial != "-1" && f_grades.getScore100 == "0")
                                                //{
                                                //    result.score = f_grades.getGradeLabel;
                                                //}
                                                //else
                                                //{
                                                //    result.score = f_grades.getScore100;
                                                //    if (f_grades.GradeSet != null)
                                                //    {
                                                //        result.score = string.Format("{0}/{1}", f_grades.getScore100Display, f_grades.getGradeLabel);
                                                //    }
                                                //}

                                                result.score = (f_grades.getSpecial != "-1" && f_grades.getScore100 == "0")
                                                        ? f_grades.getGradeLabel
                                                        : f_grades.GradeSet != null
                                                            ? $"{f_grades.getScore100Display}/{f_grades.getGradeLabel}"
                                                            : f_grades.getScore100;

                                                result.label = !string.IsNullOrEmpty(f_grades.getScore100) ? f_grades.getScore100Display : "" + "\\" + f_grades.getGradeLabel;
                                                result.grade = f_grades.getGradeLabel;



                                                typeof(PlanList).GetProperty("score" + index).SetValue(plan, !string.IsNullOrEmpty(result.score) ? result.score : "", null);
                                                typeof(PlanList).GetProperty("label" + index).SetValue(plan, result.label, null);
                                                typeof(PlanList).GetProperty("grade" + index).SetValue(plan, result.grade, null);
                                            }
                                            index++;
                                        }

                                        plan.scoreall = countMaxScore * 100;
                                        plan.scoreget = sumall;
                                        plan.sumweight = sumweight;
                                        plan.maxweight = maxweight;

                                        plan.grade = CalGrade(maxweight, sumweight);

                                        plan.percentage = Convert.ToDouble(ServiceHelper.GetGradeAverage(Math.Round((sumall / (countMaxScore * 100)) * 100, 2)));
                                        number = number + 1;
                                        planlist.Add(plan);
                                    }
                                    else
                                    {
                                        var tuser = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID && w.sID == data2).FirstOrDefault();
                                        if (tuser != null)
                                        {
                                            if (int.TryParse(tuser.sStudentTitle, out int nTitleid))
                                            {
                                                var f_title = dbschool.TTitleLists.FirstOrDefault(f => f.SchoolID == userData.CompanyID && (f.titleDescription == tuser.sStudentTitle || f.nTitleid == nTitleid));

                                                string titleDescription = f_title?.titleDescription ?? string.Empty;

                                                PlanList plan = getPlanList(tuser, titleDescription, userData.CompanyID);
                                                planlist.Add(plan);
                                            }
                                        }
                                    }

                                }
                                var newSortList = List_Sorting(planlist);

                                GridView4.DataSource = newSortList;
                                GridView4.PageSize = 999;
                                GridView4.DataBind();

                                var room = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                                var sub = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();
                                SetHeader(heaerType_2, GridView4, q_planes, sub.SubLevel + " / " + room.nTSubLevel2, tCompany.sCompany, int.Parse(registerstudy.Text), planid).RenderControl(hw);
                            }

                            Response.Write(style);
                            Response.Output.Write(sw.ToString());
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
            }
        }

        private double CalGrade(double? maxweight, double? sumweight)
        {
            double? sumall2 = Math.Round((maxweight == 0 ? 0 : (sumweight / maxweight)) ?? 0, 10, MidpointRounding.ToEven);
            return double.Parse(string.Format("{0:0.0000000000}", sumall2 ?? 0).Substring(0, 4));
        }

        private TTerm GetTerm(int? useryear, string term, JabJaiEntities dbschool, int schoolid)
        {
            return (from a in dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID)
                    join b in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nYear equals b.nYear
                    where a.SchoolID == schoolid && b.SchoolID == schoolid && a.numberYear == useryear && b.sTerm == term && b.cDel == null
                    select b).FirstOrDefault();
        }

        private string[] GetPlanListsNew(List<GetStudentGradeInfoCommon_Result> grades, bool columns_all, List<PlanCourseDTO> planCourseDTOs)
        {
            string[] planid = new string[52];
            sortList sort = new sortList();
            //var sPlaneIds = (from a in grades
            //                 select a.sPlaneID).Distinct().ToList();


            int count = 0;
            int register = 0;
            foreach (var planCourse in planCourseDTOs)
            {

                var data2 = grades.FirstOrDefault(w => w.sPlaneID == planCourse.SPlaneId);
                if (data2 != null)
                {
                    var q_gradeDetails = grades.Where(w => w.nGradeId == data2.nGradeId && w.nTermSubLevel2 != null && !((string.IsNullOrEmpty(w.getScore100) || w.getScore100 == "0") && w.getSpecial == "-1" && string.IsNullOrEmpty(w.scoreMidTerm) &&
                         string.IsNullOrEmpty(w.scoreFinalTerm))).ToList();

                    if (q_gradeDetails != null && q_gradeDetails.Count() > 0)
                    {
                        if (!columns_all && q_gradeDetails.Count(c => string.IsNullOrEmpty(c.getScore100) && c.nGradeId == data2.nGradeId) == q_gradeDetails.Count(c => c.nGradeId == data2.nGradeId)) continue;
                        register += 1;
                    }
                }
                else if (!columns_all) continue;

                ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("MainContent");
                count += 1;
                TextBox text = (TextBox)cph.FindControl("id" + count);
                if (text != null)
                {
                    string host = (Request != null && Request.Url != null) ? Request.Url.Host : string.Empty;
                    if (!string.IsNullOrEmpty(host) && host.Contains("localhost"))
                    {
                        text.Text = string.Format("{0} {1} {2}", planCourse.SPlaneId, planCourse.CourseCode, planCourse.CourseName);
                    }
                    else
                    {
                        text.Text = string.Format("{0} {1}", planCourse.CourseCode, planCourse.CourseName);

                    }
                    if (Array.IndexOf(planid, planCourse.SPlaneId.ToString()) == -1 && count <= 52)
                    {
                        planid[count - 1] = planCourse.SPlaneId.ToString();
                    }

                }
            }

            totalstudy.Text = (planCourseDTOs != null) ? planCourseDTOs.Count().ToString() : string.Empty;
            registerstudy.Text = register + "";

            return planid;
        }

        private PlanList getPlanList(GetStudentGradeInfoCommon_Result data, List<TTitleList> q_title, int schoolid)
        {
            PlanList plan = new PlanList();
            plan.studentcode = data.sStudentId;
            plan.studentnumber = data.nStudentNumber ?? 9999;
            plan.sId = data.sID ?? 0;
            plan.name = string.Format("{0}{1} {2}", data.TitleDesc, data.FirstName, data.LastName);

            int n;
            bool isNumeric = int.TryParse(data.sStudentId, out n);
            if (isNumeric == true)
            {
                plan.sort1int = Int32.Parse(data.sStudentId);
                plan.sort1txt = data.sStudentId;
                plan.sort2 = 999999;
            }
            else if (data.sStudentId == null || data.sStudentId == "")
            {
                plan.sort1int = 0;
                plan.sort1txt = "";
                plan.sort2 = 999999;
            }
            else
            {
                plan.sort1txt = data.sStudentId;
                plan.sort2 = 999999;
            }

            return plan;
        }
        private PlanList getPlanList(JabjaiEntity.DB.TUser data, string titleDescription, int schoolid)
        {
            PlanList plan = new PlanList();
            plan.studentcode = data.sStudentID;
            plan.studentnumber = data.nStudentNumber ?? 9999;
            plan.sId = data.sID;
            plan.name = (titleDescription == null ? "" : titleDescription) + "" + data.sName + " " + data.sLastname;

            int n;
            bool isNumeric = int.TryParse(data.sStudentID, out n);
            if (isNumeric == true)
            {
                plan.sort1int = Int32.Parse(data.sStudentID);
                plan.sort1txt = data.sStudentID;
                plan.sort2 = 999999;
            }
            else if (data.sStudentID == null || data.sStudentID == "")
            {
                plan.sort1int = 0;
                plan.sort1txt = "";
                plan.sort2 = 999999;
            }
            else
            {
                plan.sort1txt = data.sStudentID;
                plan.sort2 = 999999;
            }

            return plan;
        }

        private List<PlanList> List_Sorting(List<PlanList> newSortList)
        {
            int ranking = 1;
            List<PlanList> lists = new List<PlanList>();
           


            var query = newSortList.GroupBy(x => new { x.sId, x.grade, x.scoreget, x.score1, x.score2 })
                                      .OrderByDescending(g => g.Key.grade).ThenByDescending(g => g.Key.scoreget).ThenByDescending(g => g.Key.score1).ThenByDescending(g => g.Key.score2)
                                      .AsEnumerable()
                                      .Select((g, i) => new { g, i, Ranking = i + 1 })
                                      .SelectMany(x =>
                                          x.g.Select((y, j) => new
                                          {
                                              sId = y.sId,
                                              DenseRank = x.i,
                                              DenseRank1 = j,
                                              DenseRank2 = x.Ranking,
                                              Ranking = j + x.Ranking
                                          }
                                      )).ToList();

            newSortList.ToList().ForEach(f => f.ranking = (query.Where(w => w.sId == f.sId).Count() > 0) ? query.Where(w => w.sId == f.sId).Select(s => s.DenseRank2).FirstOrDefault() : 0);


            

            int number = 1;
            newSortList = newSortList.OrderBy(o => o.studentnumber).ThenBy(o => o.studentcode).ToList();
            newSortList.ForEach(f => f.number = number++);

            return newSortList;
        }

        string style = @"<style> 
.headerCell {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.headerCell130 {width:130px;background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.headerCell40 {width:40px;background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.centertext {border-style: solid;border-width: thin;}
.centertext2 {text-align: center;}
.headerCell22 {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;mso-rotate:90;vertical-align:bottom;}
.centertext22 {border-style: solid;border-width: thin;}
.headerCell33 {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;mso-rotate:90;vertical-align:bottom;}
.centertext33 {border-style: solid;border-width: thin;}
.rowLeft {text-align: left;border-style: solid;border-width: thin;}
.rowRight {text-align: right;border-style: solid;border-width: thin;}
.rowCenter {text-align: center;border-style: solid;border-width: thin;}
.headerLeft {text-align: left;}
.headerRight {text-align: right;}
.num {mso-number-format:Fixed; text-align: center;border-style: solid;border-width: thin;}
.text{mso-number-format: ""\@"";/*force text*/}
.empty {background-color: white;color: White;border-width: 0px;width:0px;}</style>";

        List<string> heaerType_1 = new List<string> { "ลำดับ", "รหัสนักเรียน", "ชื่อ-สกุล", "เต็ม", "ได้", "เกรดเฉลี่ย", "ลำดับคะแนน" };
        List<string> heaerType_2 = new List<string> { "ลำดับ", "รหัสนักเรียน", "ชื่อ-สกุล", "เต็ม", "ได้", "เปอร์เซ็น", "ลำดับคะแนน" };
        List<string> heaerType_3 = new List<string> { "ลำดับ", "รหัสนักเรียน", "ชื่อ-สกุล", };
        List<string> heaerType_4 = new List<string> { "ลำดับ", "วิชา" };

        private GridView SetHeader(List<string> HeaderList, GridView grid, List<PlanCourseDTO> q_planes, string txtclass2, string school_name, int count, string[] planid)
        {
            #region Add Header

            GridViewRow headerRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow4 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow5 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            if (grid.Controls.Count > 0)
            {
                TableHeaderCell headerTableCell = new TableHeaderCell();

                foreach (var headertxt in HeaderList)
                {
                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = headertxt;
                    headerTableCell.RowSpan = 3;
                    headerRow1.Controls.Add(headerTableCell);
                }

                foreach (var plan in planid)
                {
                    if (string.IsNullOrEmpty(plan))
                    {
                        headerTableCell = new TableHeaderCell();
                        headerTableCell.CssClass = "empty";
                        headerTableCell.Text = "";
                        headerRow5.Controls.Add(headerTableCell);
                    }
                    else
                    {
                        var f_planes = q_planes.FirstOrDefault(f => f.SPlaneId.ToString() == plan);
                        headerTableCell = new TableHeaderCell();
                        headerTableCell.CssClass = "headerCell22";
                        headerTableCell.Text = f_planes.CourseCode + " " + f_planes.CourseName;
                        headerRow1.Controls.Add(headerTableCell);

                        headerTableCell = new TableHeaderCell();
                        headerTableCell.CssClass = "headerCell";
                        headerTableCell.Text = (f_planes.NCredit ?? 0) + "";
                        headerRow5.Controls.Add(headerTableCell);
                    }
                }

                headerTableCell = new TableHeaderCell();
                headerTableCell.ColumnSpan = 4;
                headerTableCell.CssClass = "headerLeft";
                headerTableCell.Text = "ภาคเรียนที่ " + Request.QueryString["term"] + " ปีการศึกษา " + Request.QueryString["year"];
                headerRow2.Controls.Add(headerTableCell);

                headerTableCell = new TableHeaderCell();
                headerTableCell.ColumnSpan = count + 3;
                headerTableCell.CssClass = "headerRight";
                headerTableCell.Text = "ชั้นเรียน " + txtclass2;
                headerRow2.Controls.Add(headerTableCell);

                headerTableCell = new TableHeaderCell();
                headerTableCell.ColumnSpan = count + 8;
                headerTableCell.CssClass = "centertext2";
                headerTableCell.Text = "รายงานการพัฒนาผู้เรียน " + school_name;
                headerRow3.Controls.Add(headerTableCell);

                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.Text = "หน่วยกิต";
                headerTableCell.ColumnSpan = count;
                headerRow4.Controls.Add(headerTableCell);

                grid.Controls[0].Controls.AddAt(0, headerRow5);
                grid.Controls[0].Controls.AddAt(0, headerRow4);
                grid.Controls[0].Controls.AddAt(0, headerRow1);
                grid.Controls[0].Controls.AddAt(0, headerRow2);
                grid.Controls[0].Controls.AddAt(0, headerRow3);
            }
            #endregion

            foreach (GridViewRow dgr in grid.Rows)
            {
                if (dgr.RowType == DataControlRowType.DataRow)
                {
                    dgr.Cells[0].CssClass = "rowCenter";
                    dgr.Cells[1].CssClass = "rowCenter";
                    dgr.Cells[2].CssClass = "rowLeft";
                    dgr.Cells[3].CssClass = "rowCenter";
                    dgr.Cells[4].CssClass = "rowCenter";
                    dgr.Cells[5].CssClass = "num";
                    dgr.Cells[6].CssClass = "rowCenter";
                    for (int index = 0; index < 52; index++)
                    {
                        if (string.IsNullOrEmpty(planid[index])) dgr.Cells[index + 7].CssClass = "empty";
                    }
                }
            }

            return grid;
        }

        private GridView SetHeader2(List<string> HeaderList, GridView grid, List<PlanCourseDTO> q_planes, string txtclass2, string school_name, int count, string[] planid)
        {
            #region Add Header

            GridViewRow headerRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow4 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow5 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell headerTableCell = new TableHeaderCell();

            foreach (var headertxt in HeaderList)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.Text = headertxt;
                headerTableCell.RowSpan = 3;
                headerRow1.Controls.Add(headerTableCell);
            }
            int counterGroup1 = 0;
            int counterGroup2 = 0;
            int counterGroup3 = 0;
            int counterGroup4 = 0;
            foreach (var plan in planid)
            {
                if (string.IsNullOrEmpty(plan))
                {
                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "empty";
                    headerTableCell.Text = "";
                    headerRow5.Controls.Add(headerTableCell);
                }
                else
                {
                    var f_planes = q_planes.FirstOrDefault(f => f.SPlaneId.ToString() == plan);

                    if (f_planes.CourseGroupName == "รายวิชาพื้นฐาน")
                        counterGroup1++;
                    else if (f_planes.CourseGroupName == "รายวิชาเพิ่มเติม")
                        counterGroup2++;
                    else if (f_planes.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        counterGroup3++;
                    else if (f_planes.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต")
                        counterGroup4++;

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = f_planes.CourseName;
                    headerTableCell.ColumnSpan = 5;
                    headerRow4.Controls.Add(headerTableCell);

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = "ภาค 1";
                    headerRow5.Controls.Add(headerTableCell);

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = "ภาค 2";
                    headerRow5.Controls.Add(headerTableCell);

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = "รวม";
                    headerRow5.Controls.Add(headerTableCell);

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = "เฉลี่ย";
                    headerRow5.Controls.Add(headerTableCell);

                    headerTableCell = new TableHeaderCell();
                    headerTableCell.CssClass = "headerCell";
                    headerTableCell.Text = "ระดับ";
                    headerRow5.Controls.Add(headerTableCell);
                }
            }

            if (counterGroup1 != 0)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.ColumnSpan = 5 * counterGroup1;
                headerTableCell.Text = "รายวิชาพื้นฐาน";
                headerRow1.Controls.Add(headerTableCell);
            }

            if (counterGroup2 != 0)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.ColumnSpan = 5 * counterGroup2;
                headerTableCell.Text = "รายวิชาเพิ่มเติม";
                headerRow1.Controls.Add(headerTableCell);
            }

            if (counterGroup3 != 0)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.ColumnSpan = 5 * counterGroup3;
                headerTableCell.Text = "กิจกรรมพัฒนาผู้เรียน";
                headerRow1.Controls.Add(headerTableCell);
            }

            if (counterGroup4 != 0)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.ColumnSpan = 5 * counterGroup4;
                headerTableCell.Text = "ราวิชาเสริมไม่คิดหน่วยกิต";
                headerRow1.Controls.Add(headerTableCell);
            }


            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = 4;
            headerTableCell.CssClass = "headerLeft";
            headerTableCell.Text = "ภาคเรียนที่ " + Request.QueryString["term"] + " ปีการศึกษา " + Request.QueryString["year"];
            headerRow2.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = count + 3;
            headerTableCell.CssClass = "headerRight";
            headerTableCell.Text = "ชั้นเรียน " + txtclass2;
            headerRow2.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = count + 8;
            headerTableCell.CssClass = "centertext2";
            headerTableCell.Text = "รายงานการพัฒนาผู้เรียน " + school_name;
            headerRow3.Controls.Add(headerTableCell);



            grid.Controls[0].Controls.AddAt(0, headerRow5);
            grid.Controls[0].Controls.AddAt(0, headerRow4);
            grid.Controls[0].Controls.AddAt(0, headerRow1);
            grid.Controls[0].Controls.AddAt(0, headerRow2);
            grid.Controls[0].Controls.AddAt(0, headerRow3);
            #endregion

            foreach (GridViewRow dgr in grid.Rows)
            {
                if (dgr.RowType == DataControlRowType.DataRow)
                {
                    dgr.Cells[0].CssClass = "rowCenter";
                    dgr.Cells[1].CssClass = "rowCenter";
                    dgr.Cells[2].CssClass = "rowLeft";

                    for (int index = 0; index < planid.Length; index++)
                    {

                        if (string.IsNullOrEmpty(planid[index]))
                        {
                            int index2 = index * 5;
                            dgr.Cells[index2 + 3].CssClass = "empty";
                            dgr.Cells[index2 + 4].CssClass = "empty";
                            dgr.Cells[index2 + 5].CssClass = "empty";
                            dgr.Cells[index2 + 6].CssClass = "empty";
                            dgr.Cells[index2 + 7].CssClass = "empty";
                        }
                        //if (string.IsNullOrEmpty(planid[index])) dgr.Cells[index + 8].CssClass = "empty";
                        //if (string.IsNullOrEmpty(planid[index])) dgr.Cells[index + 9].CssClass = "empty";
                        //if (string.IsNullOrEmpty(planid[index])) dgr.Cells[index + 10].CssClass = "empty";
                    }
                }
            }

            return grid;
        }
        private GridView SetHeader3(List<string> HeaderList, GridView grid, List<PlanCourseDTO> q_planes, string txtclass2, string school_name, int count, string[] planid)
        {
            #region Add Header

            GridViewRow headerRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow4 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow5 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell headerTableCell = new TableHeaderCell();

            foreach (var headertxt in HeaderList)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.Text = headertxt;
                headerTableCell.RowSpan = 2;
                headerRow1.Controls.Add(headerTableCell);
            }
            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell130";
            headerTableCell.Text = "จำนวนที่เข้าสอบ";
            headerTableCell.RowSpan = 2;
            headerRow1.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell";
            headerTableCell.Text = "จำนวนนักเรียนที่มีผลการเรียนรู้";
            headerTableCell.ColumnSpan = 8;
            headerRow1.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "0";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "1.0";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "1.5";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "2.0";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "2.5";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "3.0";
            headerRow5.Controls.Add(headerTableCell);


            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "3.5";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell40";
            headerTableCell.Text = "4.0";
            headerRow5.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell130";
            headerTableCell.Text = "จำนวนนักเรียนที่ได้ระดับ 3 ขึ้นไป";
            headerTableCell.RowSpan = 2;
            headerRow1.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.CssClass = "headerCell130";
            headerTableCell.Text = "ร้อยละของนักเรียนที่ได้ระดับ 3 ขึ้นไป";
            headerTableCell.RowSpan = 2;
            headerRow1.Controls.Add(headerTableCell);


            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = 7;
            headerTableCell.CssClass = "headerLeft";
            headerTableCell.Text = "ภาคเรียนที่ " + Request.QueryString["term"] + " ปีการศึกษา " + Request.QueryString["year"];
            headerRow2.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = 6;
            headerTableCell.CssClass = "headerRight";
            headerTableCell.Text = "ชั้นเรียน " + txtclass2;
            headerRow2.Controls.Add(headerTableCell);

            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = 13;
            headerTableCell.CssClass = "centertext2";
            headerTableCell.Text = "รายงานการพัฒนาผู้เรียน " + school_name;
            headerRow3.Controls.Add(headerTableCell);


            grid.Controls[0].Controls.AddAt(0, headerRow5);

            grid.Controls[0].Controls.AddAt(0, headerRow1);
            grid.Controls[0].Controls.AddAt(0, headerRow2);
            grid.Controls[0].Controls.AddAt(0, headerRow3);
            #endregion

            foreach (GridViewRow dgr in grid.Rows)
            {
                if (dgr.RowType == DataControlRowType.DataRow)
                {
                    dgr.Cells[0].CssClass = "rowCenter";
                    dgr.Cells[1].CssClass = "rowCenter";
                    dgr.Cells[2].CssClass = "rowCenter";

                }
            }

            return grid;
        }

        class Grades_Result
        {
            internal double sumall { get; set; }
            internal double maxweight { get; set; }
            internal double sumweight { get; set; }
            internal string score { get; set; }
            internal string label { get; set; }
            internal string grade { get; set; }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected class userlist
        {
            public int sID { get; set; }
            //public int nYear { get; set; }
            //public string nTerm { get; set; }
            //public int nTSubLevel { get; set; }
            //public int nTermSubLevel2 { get; set; }

        }

        protected class PlanList
        {
            public int ranking { get; set; }
            public int studentnumber { get; set; }
            public int sId { get; set; }
            public string studentcode { get; set; }
            public int number { get; set; }
            public string name { get; set; }
            public string totalPlan { get; set; }
            public string registerPlan { get; set; }
            public string score1 { get; set; }
            public string score2 { get; set; }
            public string score3 { get; set; }
            public string score4 { get; set; }
            public string score5 { get; set; }
            public string score6 { get; set; }
            public string score7 { get; set; }
            public string score8 { get; set; }
            public string score9 { get; set; }
            public string score10 { get; set; }
            public string score11 { get; set; }
            public string score12 { get; set; }
            public string score13 { get; set; }
            public string score14 { get; set; }
            public string score15 { get; set; }
            public string score16 { get; set; }
            public string score17 { get; set; }
            public string score18 { get; set; }
            public string score19 { get; set; }
            public string score20 { get; set; }
            public string score21 { get; set; }
            public string score22 { get; set; }
            public string score23 { get; set; }
            public string score24 { get; set; }
            public string score25 { get; set; }
            public string score26 { get; set; }
            public string score27 { get; set; }
            public string score28 { get; set; }
            public string score29 { get; set; }
            public string score30 { get; set; }
            public string score31 { get; set; }
            public string score32 { get; set; }
            public string score33 { get; set; }
            public string score34 { get; set; }
            public string score35 { get; set; }
            public string score36 { get; set; }
            public string score37 { get; set; }
            public string score38 { get; set; }
            public string score39 { get; set; }
            public string score40 { get; set; }
            public string score41 { get; set; }
            public string score42 { get; set; }
            public string score43 { get; set; }
            public string score44 { get; set; }
            public string score45 { get; set; }
            public string score46 { get; set; }
            public string score47 { get; set; }
            public string score48 { get; set; }
            public string score49 { get; set; }
            public string score50 { get; set; }
            public string score51 { get; set; }
            public string score52 { get; set; }

            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public int? sort2 { get; set; }

            public int scoreall { get; set; }
            public double scoreget { get; set; }
            public double? maxweight { get; set; }
            public double? sumweight { get; set; }
            public double grade { get; set; }

            public double percentage { get; set; }

            public string label1 { get; set; }
            public string label2 { get; set; }
            public string label3 { get; set; }
            public string label4 { get; set; }
            public string label5 { get; set; }
            public string label6 { get; set; }
            public string label7 { get; set; }
            public string label8 { get; set; }
            public string label9 { get; set; }
            public string label10 { get; set; }
            public string label11 { get; set; }
            public string label12 { get; set; }
            public string label13 { get; set; }
            public string label14 { get; set; }
            public string label15 { get; set; }
            public string label16 { get; set; }
            public string label17 { get; set; }
            public string label18 { get; set; }
            public string label19 { get; set; }
            public string label20 { get; set; }
            public string label21 { get; set; }
            public string label22 { get; set; }
            public string label23 { get; set; }
            public string label24 { get; set; }
            public string label25 { get; set; }
            public string label26 { get; set; }
            public string label27 { get; set; }
            public string label28 { get; set; }
            public string label29 { get; set; }
            public string label30 { get; set; }
            public string label31 { get; set; }
            public string label32 { get; set; }
            public string label33 { get; set; }
            public string label34 { get; set; }
            public string label35 { get; set; }
            public string label36 { get; set; }
            public string label37 { get; set; }
            public string label38 { get; set; }
            public string label39 { get; set; }
            public string label40 { get; set; }
            public string label41 { get; set; }
            public string label42 { get; set; }
            public string label43 { get; set; }
            public string label44 { get; set; }
            public string label45 { get; set; }
            public string label46 { get; set; }
            public string label47 { get; set; }
            public string label48 { get; set; }
            public string label49 { get; set; }
            public string label50 { get; set; }
            public string label51 { get; set; }
            public string label52 { get; set; }

            public string grade1 { get; set; }
            public string grade2 { get; set; }
            public string grade3 { get; set; }
            public string grade4 { get; set; }
            public string grade5 { get; set; }
            public string grade6 { get; set; }
            public string grade7 { get; set; }
            public string grade8 { get; set; }
            public string grade9 { get; set; }
            public string grade10 { get; set; }
            public string grade11 { get; set; }
            public string grade12 { get; set; }
            public string grade13 { get; set; }
            public string grade14 { get; set; }
            public string grade15 { get; set; }
            public string grade16 { get; set; }
            public string grade17 { get; set; }
            public string grade18 { get; set; }
            public string grade19 { get; set; }
            public string grade20 { get; set; }
            public string grade21 { get; set; }
            public string grade22 { get; set; }
            public string grade23 { get; set; }
            public string grade24 { get; set; }
            public string grade25 { get; set; }
            public string grade26 { get; set; }
            public string grade27 { get; set; }
            public string grade28 { get; set; }
            public string grade29 { get; set; }
            public string grade30 { get; set; }
            public string grade31 { get; set; }
            public string grade32 { get; set; }
            public string grade33 { get; set; }
            public string grade34 { get; set; }
            public string grade35 { get; set; }
            public string grade36 { get; set; }
            public string grade37 { get; set; }
            public string grade38 { get; set; }
            public string grade39 { get; set; }
            public string grade40 { get; set; }
            public string grade41 { get; set; }
            public string grade42 { get; set; }
            public string grade43 { get; set; }
            public string grade44 { get; set; }
            public string grade45 { get; set; }
            public string grade46 { get; set; }
            public string grade47 { get; set; }
            public string grade48 { get; set; }
            public string grade49 { get; set; }
            public string grade50 { get; set; }
            public string grade51 { get; set; }
            public string grade52 { get; set; }
            public int? sortnumberType { get; set; }
            public int? sortnumberGroup { get; set; }
        }

        protected class sortPlan
        {
            public string planId { get; set; }
            public int number { get; set; }
            public string planName { get; set; }
            public int? sortnumberType { get; set; }
            public int? sortnumberGroup { get; set; }
            public string planCode { get; set; }
        }

        //protected class sortList
        //{
        //    public string planId { get; set; }
        //    public int? sortnumberType { get; set; }
        //    public int? sortnumberGroup { get; set; }
        //    public string planName { get; set; }
        //    public string planCode { get; set; }
        //}

        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }
}