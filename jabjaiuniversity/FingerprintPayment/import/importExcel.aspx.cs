using EntityFramework.BulkInsert.Extensions;
using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using Jabjai.Common;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Ninject;
using PostgreSQL;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace FingerprintPayment.import
{
    public partial class importExcel : System.Web.UI.Page
    {
        [Inject]
        public ICommonService CommonService { get; set; }
        private JWTToken.userData userData = new JWTToken.userData();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Response.Redirect("~/Default.aspx");

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            string sEntities = Session["sEntities"] + "";

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                //var nCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                {


                    var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                    Button1.Click += new EventHandler(Button1_Click);
                    Button2.Click += new EventHandler(Button2_Click);


                    if (!IsPostBack)
                    {
                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();

                        var item2 = new ListItem
                        {
                            Text = "เลือกปีการศึกษา",
                            Value = "0"
                        };
                        DropDownList1.Items.Add(item2);

                        foreach (var a in _db.TYears.Where(x => x.SchoolID == userData.CompanyID && x.cDel == false).OrderByDescending(o => o.numberYear).ToList())
                        {
                            var item = new ListItem
                            {
                                Text = a.numberYear.ToString(),
                                Value = a.numberYear.ToString()
                            };
                            DropDownList1.Items.Add(item);

                        }

                        List<TSubLevel> SubLevel = new List<TSubLevel>();
                        TSubLevel sub = new TSubLevel();

                        foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1))
                        {
                            sub = new TSubLevel();
                            sub = a;
                            SubLevel.Add(sub);
                        }

                        ddlSubLevel.Items.Clear();
                        ddlSubLevel.Items.Add(new ListItem
                        {
                            Text = "กรุณาเลือก",
                            Value = "0"
                        });

                        foreach (var t in SubLevel)
                        {
                            var item = new ListItem
                            {
                                Text = t.SubLevel,
                                Value = t.nTSubLevel.ToString()
                            };
                            ddlSubLevel.Items.Add(item);
                        }

                        ddlPlan.Items.Clear();
                        ddlPlan.Items.Add(new ListItem
                        {
                            Text = "กรุณาเลือก",
                            Value = "0"
                        });


                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();


                        dgd.DataSource = returnlist(1);
                        dgd.PageSize = 1;
                        dgd.DataBind();

                        GridView1.DataSource = returnlist(1);
                        GridView1.PageSize = 1;
                        GridView1.DataBind();

                        GridView2.DataSource = returnlist(1);
                        GridView2.PageSize = 1;
                        GridView2.DataBind();

                        GvDesirableAttribute.DataSource = returnlist(1);
                        GvDesirableAttribute.PageSize = 1;
                        GvDesirableAttribute.DataBind();

                        GvReadWrite.DataSource = returnlist(1);
                        GvReadWrite.PageSize = 1;
                        GvReadWrite.DataBind();

                        GvSamattana.DataSource = returnlist(1);
                        GvSamattana.PageSize = 1;
                        GvSamattana.DataBind();
                    }
                }
            }

        }

        protected void dgdSetup(object sender, EventArgs e)
        {
            int dgd1 = int.Parse(TextBox11.Text);
            int dgd2 = int.Parse(TextBox12.Text);
            int dgd3 = int.Parse(TextBox13.Text);
            int dgd4 = int.Parse(txtDesirableAttribute.Text);
            int dgd5 = int.Parse(txtReadWrite.Text);
            int dgd6 = int.Parse(txtSamattana.Text);

            if (dgd1 != 0)
            {
                dgd.DataSource = returnlist(dgd1);
                dgd.PageSize = dgd1;
                dgd.DataBind();
            }

            if (dgd2 != 0)
            {
                GridView1.DataSource = returnlist(dgd2);
                GridView1.PageSize = dgd2;
                GridView1.DataBind();
            }

            if (dgd3 != 0)
            {
                GridView2.DataSource = returnlist(dgd3);
                GridView2.PageSize = dgd3;
                GridView2.DataBind();
            }

            if (dgd4 != 0)
            {
                GvDesirableAttribute.DataSource = returnlist(dgd4);
                GvDesirableAttribute.PageSize = dgd4;
                GvDesirableAttribute.DataBind();
            }

            if (dgd5 != 0)
            {
                GvReadWrite.DataSource = returnlist(dgd5);
                GvReadWrite.PageSize = dgd5;
                GvReadWrite.DataBind();
            }

            if (dgd6 != 0)
            {
                GvSamattana.DataSource = returnlist(dgd6);
                GvSamattana.PageSize = dgd6;
                GvSamattana.DataBind();
            }
        }

        //protected void DesirableAttributeDgdSetup(object sender, EventArgs e)
        //{
        //    int dgd4 = int.Parse(txtDesirableAttribute.Text);

        //    GvDesirableAttribute.DataSource = returnlist(dgd4);
        //    GvDesirableAttribute.PageSize = 1;
        //    GvDesirableAttribute.DataBind();
        //}


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected List<PlanList> returnlist(int index)
        {
            index = index + 1;
            PlanList Plan = new PlanList();
            List<PlanList> PlanList = new List<PlanList>();

            for (int x = 1; x < index; x++)
            {
                Plan = new PlanList();
                Plan.number = x;
                PlanList.Add(Plan);
            }

            return PlanList;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                {


                    DropDownList2.Items.Clear();

                    var item = new ListItem
                    {
                        Text = "เลือกเทอม",
                        Value = "0"
                    };
                    DropDownList2.Items.Add(item);

                    string year = DropDownList1.SelectedValue;
                    int? nYear = int.Parse(year);
                    var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    ddl ddl = new ddl();
                    List<ddl> ddlList = new List<ddl>();

                    var yeardata = _db.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == nYear && w.cDel == false).FirstOrDefault();
                    foreach (var a in _db.TTerms.Where(w => w.SchoolID == schoolid && w.nYear == yeardata.nYear && w.cDel == null))
                    {
                        ddl = new ddl();
                        ddl.SchoolID = schoolid;
                        ddl.name = a.sTerm;
                        ddl.value = a.sTerm;
                        ddlList.Add(ddl);
                    }
                    ddlList = ddlList.OrderBy(x => x.name).ToList();

                    foreach (var data in ddlList)
                    {
                        var item2 = new ListItem
                        {
                            Text = data.name,
                            Value = data.name
                        };
                        DropDownList2.Items.Add(item2);
                    }
                }
            }

        }

        void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
                string sEntities = Session["sEntities"] + "";
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                {
                    var nCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    int schoolid = (int)nCompany.nCompany;

                    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                    {

                        var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                        string year = DropDownList1.SelectedValue;
                        int yearNumber = Int32.Parse(DropDownList1.SelectedItem.Text);
                        string term = DropDownList2.SelectedValue;
                        string ddln = ddlSubLevel.SelectedValue;
                        var planId = !string.IsNullOrEmpty(ddlPlan.SelectedValue) ? Int32.Parse(ddlPlan.SelectedValue) : 0;
                        int? useryear = Int32.Parse(year);
                        int ntSubLevel = Int32.Parse(ddln);

                        int nyear = 0;
                        string nterm = "";
                        //int termint = int.Parse(term);

                        foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == useryear && w.cDel == false))
                        {
                            nyear = ff.nYear;
                        }

                        foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear && w.cDel == null))
                        {
                            nterm = ee.nTerm;
                        }
                        var sEmp = Utils.GetUserId();
                        var planCourseDTOs = CommonService.GetPlanCoursesForImportGrade(ntSubLevel, 0, nterm, nyear, Utils.GetSchoolId(), sEmp);
                        if (planCourseDTOs != null && planCourseDTOs.Count == 0)
                        {
                            ServiceHelper.CreatePlanFromPage(ntSubLevel, 0, nterm, nyear, Utils.GetSchoolId(), sEmp, yearNumber);
                            planCourseDTOs = CommonService.GetPlanCourses(ntSubLevel, 0, nterm, nyear, Utils.GetSchoolId(), sEmp);
                        }


                        if (planCourseDTOs != null)
                        {
                            planCourseDTOs = (from p in planCourseDTOs
                                              join pc in _db.TPlanCourses.Where(w => w.SchoolID == userData.CompanyID) on new { p.PlanCourseId, sPlaneID = p.SPlaneId } equals new { pc.PlanCourseId, pc.sPlaneID }
                                              join t in _db.TPlans.Where(w => w.SchoolID == userData.CompanyID && w.PlanId == planId && w.cDel == false) on pc.PlanId equals t.PlanId
                                              select p).Distinct().ToList();
                        }



                        string[] planid = new string[50];
                        string[] plancode = new string[50];

                        //int havedata = 0;
                        //var q_plane = planCourseDTOs.Where(w => w.nTerm == termint && w.cDel == null && w.courseStatus == 1).ToList();
                        var courseList = new List<CourseDTO>();
                        foreach (GridViewRow dgItem in dgd.Rows)
                        {
                            TextBox planStatustxt = (TextBox)dgItem.FindControl("planStatustxt");
                            if (planStatustxt.Text == "พบข้อมูล")
                            {
                                TextBox planCodetxt = (TextBox)dgItem.FindControl("planCodetxt");
                                var data = planCourseDTOs.Where(w => w.CourseCode == planCodetxt.Text.Trim()).FirstOrDefault();
                                if (data != null)
                                {
                                    courseList.Add(new CourseDTO { SPlaneId = data.SPlaneId, CourseCode = data.CourseCode });
                                }
                            }
                        }

                        List<GradeDetails> GridView2Values = GridView2.Rows.Cast<GridViewRow>().Select(row => new GradeDetails { codetxt = ((TextBox)row.FindControl("codetxt")).Text.Trim(), stdnumtxt = ((TextBox)row.FindControl("stdnumtxt")).Text.Trim(), gradetxt = ((TextBox)row.FindControl("gradetxt")).Text.Trim() }).Distinct().ToList();
                        GridView2Values = (GridView2Values != null) ? GridView2Values.Where(w => !string.IsNullOrEmpty(w.codetxt) && !string.IsNullOrEmpty(w.stdnumtxt) && !string.IsNullOrEmpty(w.gradetxt)).ToList() : GridView2Values;

                        List<GradeDetails> gvDesirableAttributeValues = GvDesirableAttribute.Rows.Cast<GridViewRow>().Select(row => new GradeDetails { stdnumtxt = ((TextBox)row.FindControl("DesirableStdnumtxt")).Text.Trim(), gradetxt = ((TextBox)row.FindControl("DesirableGradetxt")).Text.Trim() }).Distinct().ToList();
                        gvDesirableAttributeValues = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => !string.IsNullOrEmpty(w.gradetxt) && !string.IsNullOrEmpty(w.stdnumtxt)).ToList() : gvDesirableAttributeValues;

                        List<GradeDetails> gvReadWriteValues = GvReadWrite.Rows.Cast<GridViewRow>().Select(row => new GradeDetails { stdnumtxt = ((TextBox)row.FindControl("ReadWriteStdnumtxt")).Text.Trim(), gradetxt = ((TextBox)row.FindControl("ReadWriteGradetxt")).Text.Trim() }).Distinct().ToList();
                        gvReadWriteValues = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => !string.IsNullOrEmpty(w.gradetxt) && !string.IsNullOrEmpty(w.stdnumtxt)).ToList() : gvReadWriteValues;

                        List<GradeDetails> gvSamattanaValues = GvSamattana.Rows.Cast<GridViewRow>().Select(row => new GradeDetails { stdnumtxt = ((TextBox)row.FindControl("SamattanaStdnumtxt")).Text.Trim(), gradetxt = ((TextBox)row.FindControl("SamattanaGradetxt")).Text.Trim() }).Distinct().ToList();
                        gvSamattanaValues = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => !string.IsNullOrEmpty(w.gradetxt) && !string.IsNullOrEmpty(w.stdnumtxt)).ToList() : gvSamattanaValues;


                        List<stdList> stdListList = new List<stdList>();
                        stdList stdList = new stdList();
                        foreach (GridViewRow dgItem3 in GridView1.Rows)
                        {
                            TextBox stdCodetxt = (TextBox)dgItem3.FindControl("stdCodetxt");
                            TextBox sidtxt = (TextBox)dgItem3.FindControl("sidtxt");
                            if (!string.IsNullOrEmpty(sidtxt.Text))
                            {
                                stdList = new stdList();
                                stdList.SchoolID = schoolid;
                                stdList.code = stdCodetxt.Text;
                                stdList.sid = int.Parse(sidtxt.Text);
                                stdListList.Add(stdList);
                            }
                        }
                        var IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(useryear ?? 0, nterm, schoolid);
                        string scoreEnteredFromScoreEntryPage = string.Empty;
                        if (IsRequestForCurrentAcademicYear)
                        {
                            scoreEnteredFromScoreEntryPage = ImportCurrentAcademicYear(nterm, courseList, GridView2Values, stdListList,
                                gvDesirableAttributeValues, gvReadWriteValues, gvSamattanaValues, planCourseDTOs, schoolid, planId);


                        }
                        else
                        {
                            scoreEnteredFromScoreEntryPage = ImportPreviouseAcademicYear(nterm, courseList, GridView2Values, stdListList,
                                gvDesirableAttributeValues, gvReadWriteValues, gvSamattanaValues, planCourseDTOs, schoolid, planId);
                        }

                        if (!string.IsNullOrEmpty(scoreEnteredFromScoreEntryPage))
                        {
                            Response.Redirect("importExcel.aspx?coursecode=" + scoreEnteredFromScoreEntryPage);
                        }
                        else Response.Redirect("importExcel.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string ImportCurrentAcademicYear(string nterm, List<CourseDTO> courseList, List<GradeDetails> GridView2Values, List<stdList> stdListList,
            List<GradeDetails> gvDesirableAttributeValues, List<GradeDetails> gvReadWriteValues, List<GradeDetails> gvSamattanaValues, List<PlanCourseDTO> planCourseDTOs, int schoolid, int planId)
        {

            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
            {
                using (var pgLiteDB = new PGGradeDBEntities())
                {
                    var q_Grade = new List<PGTGrade>();
                    var isScoreEnteredFromScoreEntryPage = false;
                    string[] courseCode = { };
                    if (courseList.Count > 0 && GridView2Values != null)
                    {
                        q_Grade = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                   join c in courseList on g.sPlaneID equals c.SPlaneId
                                   select g).ToList();

                        List<int> _commUser = new List<int>();
                        List<int> _sPlaneIds = new List<int>();
                        //string _sPlaneIds = "";

                        if (stdListList != null)
                        {
                            stdListList.ToList().ForEach(f =>
                            {
                                _commUser.Add(f.sid);


                            });
                        }

                        if (courseList != null)
                        {
                            courseList.ToList().ForEach(f =>
                            {
                                _sPlaneIds.Add(f.SPlaneId);
                            });
                        }

                        List<int> q_GradeRoom = new List<int>();
                        if (_commUser.Count > 0 && _sPlaneIds.Count > 0)
                        {

                            var q_GradeRoom1 = (from GD in pgLiteDB.PGTGradeDetails
                                                join G in pgLiteDB.PGTGrades on GD.nGradeId equals G.nGradeId
                                                where GD.SchoolID == userData.CompanyID
                                                && G.nTerm == nterm
                                                && _commUser.Contains(GD.sID)
                                                && _sPlaneIds.Contains((int)G.sPlaneID)
                                                && G.nTermSubLevel2 != null
                                                && GD.cDel == false
                                                && (GD.getSpecial != "-1" || (GD.getSpecial == "-1" && ((GD.getScore100 != null && GD.getScore100 != "" && GD.getScore100 != "0")
                                                || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "") || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "")
                                                || (GD.scoreMidTerm != null && GD.scoreMidTerm != "") || (GD.scoreFinalTerm != null && GD.scoreFinalTerm != ""))))
                                                select new
                                                {
                                                    G.sPlaneID
                                                }).Distinct().ToList();

                            if (q_GradeRoom1 != null)
                            {
                                q_GradeRoom1.ToList().ForEach(f =>
                                {
                                    q_GradeRoom.Add((int)f.sPlaneID);
                                });
                            }

                        }
                        else if (_commUser.Count > 0 && _sPlaneIds.Count == 0)
                        {

                            var q_GradeRoom1 = (from GD in pgLiteDB.PGTGradeDetails
                                                join G in pgLiteDB.PGTGrades on GD.nGradeId equals G.nGradeId
                                                where GD.SchoolID == userData.CompanyID
                                                && G.nTerm == nterm
                                                && _commUser.Contains(GD.sID)
                                                && G.nTermSubLevel2 != null
                                                && GD.cDel == false
                                                && (GD.getSpecial != "-1" || (GD.getSpecial == "-1" && ((GD.getScore100 != null && GD.getScore100 != "" && GD.getScore100 != "0")
                                                || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "") || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "")
                                                || (GD.scoreMidTerm != null && GD.scoreMidTerm != "") || (GD.scoreFinalTerm != null && GD.scoreFinalTerm != ""))))
                                                select new
                                                {
                                                    G.sPlaneID
                                                }).Distinct().ToList();

                            if (q_GradeRoom1 != null)
                            {
                                q_GradeRoom1.ToList().ForEach(f =>
                                {
                                    q_GradeRoom.Add((int)f.sPlaneID);
                                });
                            }

                        }
                        else if (_commUser.Count == 0 && _sPlaneIds.Count == 0)
                        {

                            var q_GradeRoom1 = (from GD in pgLiteDB.PGTGradeDetails
                                                join G in pgLiteDB.PGTGrades on GD.nGradeId equals G.nGradeId
                                                where GD.SchoolID == userData.CompanyID
                                                && G.nTerm == nterm
                                                && G.nTermSubLevel2 != null
                                                && GD.cDel == false
                                                && (GD.getSpecial != "-1" || (GD.getSpecial == "-1" && ((GD.getScore100 != null && GD.getScore100 != "" && GD.getScore100 != "0")
                                                || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "") || (GD.scoreBeforeAfterMidTerm != null && GD.scoreBeforeAfterMidTerm != "")
                                                || (GD.scoreMidTerm != null && GD.scoreMidTerm != "") || (GD.scoreFinalTerm != null && GD.scoreFinalTerm != ""))))
                                                select new
                                                {
                                                    G.sPlaneID
                                                }).Distinct().ToList();

                            if (q_GradeRoom1 != null)
                            {
                                q_GradeRoom1.ToList().ForEach(f =>
                                {
                                    q_GradeRoom.Add((int)f.sPlaneID);
                                });
                            }

                        }





                        if (q_GradeRoom != null && q_GradeRoom.Count > 0)
                        {
                            isScoreEnteredFromScoreEntryPage = true;
                            courseCode = (from c in courseList
                                          join id in q_GradeRoom on c.SPlaneId equals id
                                          select c.CourseCode).ToArray();
                        }
                    }
                    else if ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0))
                    {
                        q_Grade = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                   join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                   select g).ToList();

                        courseList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                      join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                      select new CourseDTO { SPlaneId = g.sPlaneID ?? 0, CourseCode = c.CourseCode }).ToList();

                        var q_GradeRoom = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 != null).ToList()
                                           join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == userData.CompanyID) on g.nGradeId equals gd.nGradeId
                                           join s in stdListList on gd.sID equals s.sid
                                           join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                           where g.nTerm == nterm && (gd.getSpecial != "-1" || (gd.getSpecial == "-1" && ((!string.IsNullOrEmpty(gd.getScore100) && gd.getScore100 != "0") || !string.IsNullOrEmpty(gd.ScoreBeforeMidTerm)
                            || !string.IsNullOrEmpty(gd.ScoreAfterMidTerm) || !string.IsNullOrEmpty(gd.scoreMidTerm) || !string.IsNullOrEmpty(gd.scoreFinalTerm))))
                                           select g.sPlaneID).Distinct().ToList();
                        if (q_GradeRoom != null && q_GradeRoom.Count > 0)
                        {
                            isScoreEnteredFromScoreEntryPage = true;
                            courseCode = (from c in courseList
                                          join id in q_GradeRoom on c.SPlaneId equals id
                                          select c.CourseCode).ToArray();
                        }

                    }

                    if (!isScoreEnteredFromScoreEntryPage)
                    {
                        //var max_id = q_Grade.Select(s => s.nGradeId).DefaultIfEmpty(0).Max();
                        //max_id = max_id + 1;

                        List<PlanList2> PlanList2List = new List<PlanList2>();
                        PlanList2 PlanList2 = new PlanList2();
                        //List<stdList> stdListList = new List<stdList>();

                        var userId = Utils.GetUserId();
                        int x = 0;
                        if (!string.IsNullOrEmpty(nterm))
                        {
                            //for (x = 0; x < havedata; x++)
                            //{
                            foreach (var c in courseList)
                            {
                                PlanList2 = new PlanList2();
                                //if (!string.IsNullOrEmpty(nterm))
                                //{
                                var check = q_Grade.Where(w => w.nTerm == nterm && w.sPlaneID == c.SPlaneId).FirstOrDefault();
                                if (check == null)
                                {
                                    //int sPlaneId = 0;
                                    //int.TryParse(planid[x], out sPlaneId);
                                    var tgrade = new PGTGrade
                                    {
                                        dAdd = DateTime.Now,
                                        dUpdate = DateTime.Now,
                                        sPlaneID = c.SPlaneId,
                                        //nGradeId = max_id,
                                        nTerm = nterm,
                                        SchoolID = schoolid,
                                        nUserAdd = userId,
                                        nUserUpdate = userId,
                                        //PlanId = planId

                                    };
                                    pgLiteDB.PGTGrades.Add(tgrade);
                                    pgLiteDB.SaveChanges();
                                    SchoolBright.DataAccess.DataAccessHelper._TGradeList.Add(tgrade);

                                    //PlanList2.code = plancode[x];
                                    PlanList2.code = c.CourseCode;
                                    PlanList2.gradeId = (int)tgrade.nGradeId;

                                    //max_id = max_id + 1;
                                }
                                else
                                {
                                    //check.PlanId = planId;
                                    check.nUserUpdate = userId;
                                    check.dUpdate = DateTime.Now;


                                    pgLiteDB.PGTGrades.AddOrUpdate(check);
                                    pgLiteDB.SaveChanges();
                                    SchoolBright.DataAccess.DataAccessHelper._TGradeList.Add(check);


                                    PlanList2.SchoolID = schoolid;
                                    //PlanList2.code = plancode[x];
                                    PlanList2.code = c.CourseCode;
                                    PlanList2.gradeId = (int)check.nGradeId;

                                }
                                //}

                                PlanList2List.Add(PlanList2);
                            }
                            //}

                            PlanList2List = PlanList2List.Distinct().ToList();
                            // _dbGrade.SaveChanges();

                            int countloop = int.Parse(TextBox13.Text);

                            string[] detailCode = new string[countloop];
                            string[] detailStd = new string[countloop];
                            string[] detailGrade = new string[countloop];



                            List<int> _icommUser = new List<int>();
                            List<int> _icommGrade = new List<int>();


                            string _commUser = "", _commGrade = "";

                            if (stdListList != null)
                            {
                                stdListList.ToList().ForEach(f =>
                                {
                                    _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sid + "'";
                                    _icommUser.Add(f.sid);
                                });
                            }

                            if (PlanList2List != null)
                            {
                                PlanList2List.ForEach(f =>
                                {
                                    _commGrade += (string.IsNullOrEmpty(_commGrade) ? "" : ",") + "'" + f.gradeId + "'";
                                    _icommGrade.Add(f.gradeId);
                                });
                            }

                            if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";
                            if (!string.IsNullOrEmpty(_commGrade)) _commGrade = " AND nGradeId  IN (" + _commGrade + ")";


                            List<PGTGradeDetail> PGTGradeDetailList = new List<PGTGradeDetail>();
                            if (_icommUser.Count > 0 && _icommGrade.Count > 0)
                            {
                                PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(p => p.SchoolID == userData.CompanyID && _icommGrade.Contains(p.nGradeId) && _icommUser.Contains(p.sID)).ToList();
                            }
                            else if (_icommUser.Count > 0 && _icommGrade.Count == 0)
                            {
                                PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(p => p.SchoolID == userData.CompanyID && _icommUser.Contains(p.sID)).ToList();

                            }
                            else
                            {
                                PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(p => p.SchoolID == userData.CompanyID).ToList();

                            }



                            var q_GradeDetailView = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(PGTGradeDetailList);


                            var newGradeDetails = (from gv in GridView2Values
                                                   join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                   join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                   join gd in q_GradeDetailView on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where (gdetail == null || (gdetail != null && gdetail.nGradeDetailId == 0))
                                                   select new PGTGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gradedata.gradeId,
                                                       getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                       getSpecial = GetGradeSpecial(gv.gradetxt),
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       cDel = false,
                                                       PlanId = planId
                                                   }).ToList();
                            if (newGradeDetails != null && newGradeDetails.Count > 0)
                            {

                                foreach (PGTGradeDetail PGTGradeDetail in newGradeDetails)
                                {
                                    pgLiteDB.PGTGradeDetails.AddOrUpdate(PGTGradeDetail);
                                    pgLiteDB.SaveChanges();


                                    SchoolBright.DataAccess.DataAccessHelper._TGradeDetailList.Add(PGTGradeDetail);

                                }

                                //pgLiteDB.BulkInsert<PGTGradeDetail>(newGradeDetails);
                                //pgLiteDB.SaveChanges();




                                //BulkInsertTGradeDetail(newGradeDetails);
                            }
                            var oldGradeDetails = new List<PGTGradeDetail>();
                            if ((GridView2Values != null && GridView2Values.Count == 0) && ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0)))
                            {
                                oldGradeDetails = (from stddata in stdListList
                                                   join gd in q_GradeDetailView on new { sID = stddata.sid } equals new { gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                   select new PGTGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gdetail.nGradeId,
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       cDel = false,
                                                       nGradeDetailId = gdetail.nGradeDetailId,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       PlanId = planId
                                                   }).ToList();
                                if (oldGradeDetails != null && oldGradeDetails.Count > 0)
                                {
                                    foreach (var gd in oldGradeDetails)
                                    {
                                        var tgradeDetail = pgLiteDB.PGTGradeDetails.FirstOrDefault(w => w.sID == gd.sID && w.nGradeId == gd.nGradeId && w.SchoolID == userData.CompanyID && w.cDel == false);
                                        if (tgradeDetail != null)
                                        {
                                            if (gd.getBehaviorLabel != null)
                                            {
                                                tgradeDetail.getBehaviorLabel = gd.getBehaviorLabel;
                                                tgradeDetail.getBehaviorTotal = gd.getBehaviorTotal;
                                                tgradeDetail.PlanId = planId;
                                            }

                                            if (gd.getReadWrite != null)
                                            {
                                                tgradeDetail.getReadWrite = gd.getReadWrite;
                                                tgradeDetail.getReadWriteTotal = gd.getReadWriteTotal;
                                                tgradeDetail.PlanId = planId;
                                            }

                                            if (gd.getSamattana != null)
                                            {
                                                tgradeDetail.getSamattana = gd.getSamattana;
                                                tgradeDetail.getSamattanaTotal = gd.getSamattanaTotal;
                                                tgradeDetail.PlanId = planId;
                                            }

                                            pgLiteDB.PGTGradeDetails.AddOrUpdate(tgradeDetail);
                                            pgLiteDB.SaveChanges();

                                            SchoolBright.DataAccess.DataAccessHelper._TGradeDetailList.Add(tgradeDetail);

                                        }
                                    }

                                }
                            }
                            else
                            {
                                oldGradeDetails = (from gv in GridView2Values
                                                   join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                   join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                   join gd in q_GradeDetailView on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                   select new PGTGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gradedata.gradeId,
                                                       getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                       getSpecial = GetGradeSpecial(gv.gradetxt),
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       cDel = false,
                                                       nGradeDetailId = gdetail.nGradeDetailId,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       PlanId = planId
                                                   }).ToList();

                                if (oldGradeDetails != null && oldGradeDetails.Count > 0)
                                {
                                    foreach (var gd in oldGradeDetails)
                                    {
                                        var tgradeDetail = pgLiteDB.PGTGradeDetails.FirstOrDefault(w => w.sID == gd.sID && w.nGradeId == gd.nGradeId && w.SchoolID == userData.CompanyID && w.cDel == false);
                                        if (tgradeDetail != null)
                                        {
                                            tgradeDetail.getGradeLabel = gd.getGradeLabel;
                                            tgradeDetail.getSpecial = gd.getSpecial;
                                            tgradeDetail.PlanId = gd.PlanId;

                                            if (gd.getBehaviorLabel != null)
                                            {
                                                tgradeDetail.getBehaviorLabel = gd.getBehaviorLabel;
                                                tgradeDetail.getBehaviorTotal = gd.getBehaviorTotal;
                                            }

                                            if (gd.getReadWrite != null)
                                            {
                                                tgradeDetail.getReadWrite = gd.getReadWrite;
                                                tgradeDetail.getReadWriteTotal = gd.getReadWriteTotal;
                                            }

                                            if (gd.getSamattana != null)
                                            {
                                                tgradeDetail.getSamattana = gd.getSamattana;
                                                tgradeDetail.getSamattanaTotal = gd.getSamattanaTotal;
                                            }


                                            pgLiteDB.PGTGradeDetails.AddOrUpdate(tgradeDetail);
                                            pgLiteDB.SaveChanges();

                                            SchoolBright.DataAccess.DataAccessHelper._TGradeDetailList.Add(tgradeDetail);
                                        }


                                    }

                                }
                            }


                        }

                    }

                    return String.Join(",", courseCode);
                }
            }
            else
            {

                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade()))
                {
                    var q_Grade = new List<TGrade>();
                    var isScoreEnteredFromScoreEntryPage = false;
                    string[] courseCode = { };
                    if (courseList.Count > 0 && GridView2Values != null)
                    {
                        q_Grade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                   join c in courseList on g.sPlaneID equals c.SPlaneId
                                   select g).ToList();

                        string _commUser = "";
                        string _sPlaneIds = "";
                        if (stdListList != null)
                        {
                            stdListList.ToList().ForEach(f =>
                            {
                                _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sid + "'";
                            });
                        }
                        if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";

                        if (courseList != null)
                        {
                            courseList.ToList().ForEach(f =>
                            {
                                _sPlaneIds += (string.IsNullOrEmpty(_sPlaneIds) ? "" : ",") + "" + f.SPlaneId + "";
                            });
                        }
                        if (!string.IsNullOrEmpty(_sPlaneIds)) _sPlaneIds = " AND sPlaneID  IN (" + _sPlaneIds + ")";

                        var _nTerm = " AND G.nTerm='" + nterm + "'";

                        var SQL = string.Format("SELECT distinct G.sPlaneID FROM TGradeDetail GD JOIN TGrade G ON G.nGradeId = GD.nGradeId  " +
                            "WHERE 1=1 {0}  {1} and GD.SchoolID={2} {3} and G.nTermSubLevel2 IS NOT NULL and GD.cDel = 0 and (gd.getSpecial != '-1' or (gd.getSpecial = '-1' and ((gd.getScore100 IS NOT NULL and gd.getScore100 != '' and gd.getScore100 != '0')" +
                            " or (gd.ScoreBeforeMidTerm IS NOT NULL and gd.ScoreBeforeMidTerm != '') or (gd.ScoreAfterMidTerm IS NOT NULL and gd.ScoreAfterMidTerm != '') or (gd.scoreMidTerm IS NOT NULL and gd.scoreMidTerm != '') or (gd.scoreFinalTerm IS NOT NULL and gd.scoreFinalTerm != ''))))", _commUser, _nTerm, userData.CompanyID, _sPlaneIds);
                        var q_GradeRoom = (SQL != null) ? _dbGrade.Database.SqlQuery<int>(SQL).ToList() : new List<int>();

                        if (q_GradeRoom != null && q_GradeRoom.Count > 0)
                        {
                            isScoreEnteredFromScoreEntryPage = true;
                            courseCode = (from c in courseList
                                          join id in q_GradeRoom on c.SPlaneId equals id
                                          select c.CourseCode).ToArray();
                        }
                    }
                    else if ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0))
                    {
                        q_Grade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                   join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                   select g).ToList();

                        courseList = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                      join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                      select new CourseDTO { SPlaneId = g.sPlaneID ?? 0, CourseCode = c.CourseCode }).ToList();

                        var q_GradeRoom = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 != null).ToList()
                                           join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID) on g.nGradeId equals gd.nGradeId
                                           join s in stdListList on gd.sID equals s.sid
                                           join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                           where g.nTerm == nterm && (gd.getSpecial != "-1" || (gd.getSpecial == "-1" && ((!string.IsNullOrEmpty(gd.getScore100) && gd.getScore100 != "0") || !string.IsNullOrEmpty(gd.ScoreBeforeMidTerm)
                            || !string.IsNullOrEmpty(gd.ScoreAfterMidTerm) || !string.IsNullOrEmpty(gd.scoreMidTerm) || !string.IsNullOrEmpty(gd.scoreFinalTerm))))
                                           select g.sPlaneID).Distinct().ToList();
                        if (q_GradeRoom != null && q_GradeRoom.Count > 0)
                        {
                            isScoreEnteredFromScoreEntryPage = true;
                            courseCode = (from c in courseList
                                          join id in q_GradeRoom on c.SPlaneId equals id
                                          select c.CourseCode).ToArray();
                        }

                    }

                    if (!isScoreEnteredFromScoreEntryPage)
                    {
                        //var max_id = q_Grade.Select(s => s.nGradeId).DefaultIfEmpty(0).Max();
                        //max_id = max_id + 1;

                        List<PlanList2> PlanList2List = new List<PlanList2>();
                        PlanList2 PlanList2 = new PlanList2();
                        //List<stdList> stdListList = new List<stdList>();

                        var userId = Utils.GetUserId();
                        int x = 0;
                        if (!string.IsNullOrEmpty(nterm))
                        {
                            //for (x = 0; x < havedata; x++)
                            //{
                            foreach (var c in courseList)
                            {
                                PlanList2 = new PlanList2();
                                //if (!string.IsNullOrEmpty(nterm))
                                //{
                                var check = q_Grade.Where(w => w.nTerm == nterm && w.sPlaneID == c.SPlaneId).FirstOrDefault();
                                if (check == null)
                                {
                                    //int sPlaneId = 0;
                                    //int.TryParse(planid[x], out sPlaneId);
                                    var tgrade = new TGrade
                                    {
                                        dAdd = DateTime.Now,
                                        dUpdate = DateTime.Now,
                                        sPlaneID = c.SPlaneId,
                                        //nGradeId = max_id,
                                        nTerm = nterm,
                                        SchoolID = schoolid,
                                        nUserAdd = userId,
                                        nUserUpdate = userId,
                                        //PlanId = planId

                                    };
                                    _dbGrade.TGrades.Add(tgrade);
                                    _dbGrade.SaveChanges();
                                    //PlanList2.code = plancode[x];
                                    PlanList2.code = c.CourseCode;
                                    PlanList2.gradeId = tgrade.nGradeId;

                                    //max_id = max_id + 1;
                                }
                                else
                                {
                                    //check.PlanId = planId;
                                    check.nUserUpdate = userId;
                                    check.dUpdate = DateTime.Now;
                                    _dbGrade.SaveChanges();

                                    PlanList2.SchoolID = schoolid;
                                    //PlanList2.code = plancode[x];
                                    PlanList2.code = c.CourseCode;
                                    PlanList2.gradeId = check.nGradeId;

                                }
                                //}

                                PlanList2List.Add(PlanList2);
                            }
                            //}

                            PlanList2List = PlanList2List.Distinct().ToList();
                            _dbGrade.SaveChanges();

                            int countloop = int.Parse(TextBox13.Text);

                            string[] detailCode = new string[countloop];
                            string[] detailStd = new string[countloop];
                            string[] detailGrade = new string[countloop];





                            string _commUser = "", _commGrade = "";

                            if (stdListList != null)
                            {
                                stdListList.ToList().ForEach(f =>
                                {
                                    _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sid + "'";
                                });
                            }

                            if (PlanList2List != null)
                            {
                                PlanList2List.ForEach(f =>
                                {
                                    _commGrade += (string.IsNullOrEmpty(_commGrade) ? "" : ",") + "'" + f.gradeId + "'";
                                });
                            }

                            if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";
                            if (!string.IsNullOrEmpty(_commGrade)) _commGrade = " AND nGradeId  IN (" + _commGrade + ")";

                            var SQL = (!string.IsNullOrEmpty(_commUser) && !string.IsNullOrEmpty(_commGrade)) ? string.Format("SELECT * FROM TGradeDetail WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", _commUser, _commGrade, userData.CompanyID) : null;
                            var q_GradeDetailView = (SQL != null) ? _dbGrade.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();



                            var newGradeDetails = (from gv in GridView2Values
                                                   join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                   join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                   join gd in q_GradeDetailView on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where (gdetail == null || (gdetail != null && gdetail.nGradeDetailId == 0))
                                                   select new TGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gradedata.gradeId,
                                                       getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                       getSpecial = GetGradeSpecial(gv.gradetxt),
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       cDel = false,
                                                       PlanId = planId
                                                   }).ToList();
                            if (newGradeDetails != null && newGradeDetails.Count > 0)
                            {
                                _dbGrade.BulkInsert<JabjaiSchoolGradeEntity.TGradeDetail>(newGradeDetails);
                                _dbGrade.SaveChanges();

                                //BulkInsertTGradeDetail(newGradeDetails);
                            }
                            var oldGradeDetails = new List<TGradeDetail>();
                            if ((GridView2Values != null && GridView2Values.Count == 0) && ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0)))
                            {
                                oldGradeDetails = (from stddata in stdListList
                                                   join gd in q_GradeDetailView on new { sID = stddata.sid } equals new { gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                   select new TGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gdetail.nGradeId,
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       cDel = false,
                                                       nGradeDetailId = gdetail.nGradeDetailId,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       PlanId = planId
                                                   }).ToList();
                                if (oldGradeDetails != null && oldGradeDetails.Count > 0)
                                {
                                    foreach (var gd in oldGradeDetails)
                                    {
                                        var tgradeDetail = _dbGrade.TGradeDetails.FirstOrDefault(w => w.sID == gd.sID && w.nGradeId == gd.nGradeId && w.SchoolID == userData.CompanyID && w.cDel == false);
                                        if (tgradeDetail != null)
                                        {
                                            if (gd.getBehaviorLabel != null)
                                            {
                                                tgradeDetail.getBehaviorLabel = gd.getBehaviorLabel;
                                                tgradeDetail.getBehaviorTotal = gd.getBehaviorTotal;
                                                tgradeDetail.PlanId = planId;
                                            }

                                            if (gd.getReadWrite != null)
                                            {
                                                tgradeDetail.getReadWrite = gd.getReadWrite;
                                                tgradeDetail.getReadWriteTotal = gd.getReadWriteTotal;
                                                tgradeDetail.PlanId = planId;
                                            }

                                            if (gd.getSamattana != null)
                                            {
                                                tgradeDetail.getSamattana = gd.getSamattana;
                                                tgradeDetail.getSamattanaTotal = gd.getSamattanaTotal;
                                                tgradeDetail.PlanId = planId;
                                            }
                                        }
                                    }
                                    _dbGrade.SaveChanges();
                                }
                            }
                            else
                            {
                                oldGradeDetails = (from gv in GridView2Values
                                                   join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                   join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                   join gd in q_GradeDetailView on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                   from gdetail in gdetails.DefaultIfEmpty()
                                                   where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                   select new TGradeDetail
                                                   {
                                                       SchoolID = schoolid,
                                                       sID = stddata.sid,
                                                       nGradeId = gradedata.gradeId,
                                                       getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                       getSpecial = GetGradeSpecial(gv.gradetxt),
                                                       UpdatedBy = Utils.GetUserId(),
                                                       CreatedBy = Utils.GetUserId(),
                                                       CreatedDate = DateTime.Now,
                                                       UpdatedDate = DateTime.Now,
                                                       cDel = false,
                                                       nGradeDetailId = gdetail.nGradeDetailId,
                                                       getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                       PlanId = planId
                                                   }).ToList();

                                if (oldGradeDetails != null && oldGradeDetails.Count > 0)
                                {
                                    foreach (var gd in oldGradeDetails)
                                    {
                                        var tgradeDetail = _dbGrade.TGradeDetails.FirstOrDefault(w => w.sID == gd.sID && w.nGradeId == gd.nGradeId && w.SchoolID == userData.CompanyID && w.cDel == false);
                                        if (tgradeDetail != null)
                                        {
                                            tgradeDetail.getGradeLabel = gd.getGradeLabel;
                                            tgradeDetail.getSpecial = gd.getSpecial;
                                            tgradeDetail.PlanId = gd.PlanId;

                                            if (gd.getBehaviorLabel != null)
                                            {
                                                tgradeDetail.getBehaviorLabel = gd.getBehaviorLabel;
                                                tgradeDetail.getBehaviorTotal = gd.getBehaviorTotal;
                                            }

                                            if (gd.getReadWrite != null)
                                            {
                                                tgradeDetail.getReadWrite = gd.getReadWrite;
                                                tgradeDetail.getReadWriteTotal = gd.getReadWriteTotal;
                                            }

                                            if (gd.getSamattana != null)
                                            {
                                                tgradeDetail.getSamattana = gd.getSamattana;
                                                tgradeDetail.getSamattanaTotal = gd.getSamattanaTotal;
                                            }
                                        }
                                    }
                                    _dbGrade.SaveChanges();
                                }
                            }


                        }

                    }

                    return String.Join(",", courseCode);
                }
            }
        }


        private string ImportPreviouseAcademicYear(string nterm, List<CourseDTO> courseList, List<GradeDetails> GridView2Values, List<stdList> stdListList,
            List<GradeDetails> gvDesirableAttributeValues, List<GradeDetails> gvReadWriteValues, List<GradeDetails> gvSamattanaValues, List<PlanCourseDTO> planCourseDTOs, int schoolid, int planId)
        {
            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString()))
            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade()))
            {
                var q_GradeHistory = new List<GradeHistoryEntity.TGradeHistory>();
                var isScoreEnteredFromScoreEntryPage = false;
                string[] courseCode = { };
                if (courseList.Count > 0 && GridView2Values != null)
                {
                    q_GradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                      join c in courseList on g.sPlaneID equals c.SPlaneId
                                      select g).ToList();

                    string _commUser = "";
                    string _sPlaneIds = "";

                    if (stdListList != null)
                    {
                        stdListList.ToList().ForEach(f =>
                        {
                            _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "" + f.sid + "";
                        });
                    }
                    if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";

                    if (courseList != null)
                    {
                        courseList.ToList().ForEach(f =>
                        {
                            _sPlaneIds += (string.IsNullOrEmpty(_sPlaneIds) ? "" : ",") + "" + f.SPlaneId + "";
                        });
                    }
                    if (!string.IsNullOrEmpty(_sPlaneIds)) _sPlaneIds = " AND sPlaneID  IN (" + _sPlaneIds + ")";

                    var _nTerm = " AND G.nTerm='" + nterm + "'";

                    var SQL = string.Format("SELECT distinct G.sPlaneID FROM TGradeDetailHistory GD JOIN TGradeHistory G ON G.nGradeId = GD.nGradeId  " +
                        "WHERE 1=1 {0}  {1} and GD.SchoolID={2} {3} and G.nTermSubLevel2 IS NOT NULL  and GD.cDel = 0 and (gd.getSpecial != '-1' or (gd.getSpecial = '-1' and ((gd.getScore100 IS NOT NULL and gd.getScore100 != '' and gd.getScore100 != '0')" +
                        " or (gd.ScoreBeforeMidTerm IS NOT NULL and gd.ScoreBeforeMidTerm != '' and gd.ScoreBeforeMidTerm != '0') or (gd.ScoreAfterMidTerm IS NOT NULL and gd.ScoreAfterMidTerm != '' and gd.ScoreAfterMidTerm != '0') or (gd.scoreMidTerm IS NOT NULL and gd.scoreMidTerm != '' and gd.scoreMidTerm != '0') or (gd.scoreFinalTerm IS NOT NULL and gd.scoreFinalTerm != '' and gd.scoreFinalTerm != '0'))))", _commUser, _nTerm, userData.CompanyID, _sPlaneIds);
                    var q_GradeRoom = (SQL != null) ? _dbGradeHistory.Database.SqlQuery<int>(SQL).ToList() : new List<int>();

                    if (q_GradeRoom != null && q_GradeRoom.Count > 0)
                    {
                        isScoreEnteredFromScoreEntryPage = true;
                        courseCode = (from c in courseList
                                      join id in q_GradeRoom on c.SPlaneId equals id
                                      select c.CourseCode).ToArray();
                    }
                }
                else if ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0))
                {
                    q_GradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                      join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                      select g).ToList();

                    courseList = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == null).ToList()
                                  join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                  select new CourseDTO { SPlaneId = g.sPlaneID ?? 0, CourseCode = c.CourseCode }).ToList();

                    var q_GradeRoomHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 != null).ToList()
                                              join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == userData.CompanyID) on g.nGradeId equals gd.nGradeId
                                              join s in stdListList on gd.sID equals s.sid
                                              join c in planCourseDTOs on g.sPlaneID equals c.SPlaneId
                                              where g.nTerm == nterm && (gd.getSpecial != "-1" || (gd.getSpecial == "-1" && ((!string.IsNullOrEmpty(gd.getScore100) && gd.getScore100 != "0") || !string.IsNullOrEmpty(gd.ScoreBeforeMidTerm)
                               || !string.IsNullOrEmpty(gd.ScoreAfterMidTerm) || !string.IsNullOrEmpty(gd.scoreMidTerm) || !string.IsNullOrEmpty(gd.scoreFinalTerm)))) && gd.cDel == false
                                              select g.sPlaneID).Distinct().ToList();

                    if (q_GradeRoomHistory != null && q_GradeRoomHistory.Count > 0)
                    {
                        isScoreEnteredFromScoreEntryPage = true;
                        courseCode = (from c in courseList
                                      join id in q_GradeRoomHistory on c.SPlaneId equals id
                                      select c.CourseCode).ToArray();
                    }

                }

                if (!isScoreEnteredFromScoreEntryPage)
                {
                    List<PlanList2> PlanList2List = new List<PlanList2>();
                    PlanList2 PlanList2 = new PlanList2();

                    var userId = Utils.GetUserId();
                    int x = 0;
                    if (!string.IsNullOrEmpty(nterm))
                    {

                        foreach (var c in courseList)
                        {
                            PlanList2 = new PlanList2();

                            var check = q_GradeHistory.Where(w => w.nTerm == nterm && w.sPlaneID == c.SPlaneId).FirstOrDefault();
                            if (check == null)
                            {
                                using (DbContextTransaction gradeTransaction = _dbGrade.Database.BeginTransaction())
                                using (DbContextTransaction gradeHistoryTransaction = _dbGradeHistory.Database.BeginTransaction())
                                {
                                    try
                                    {
                                        // Add to main table and move to history
                                        var tgrade = new TGrade
                                        {
                                            dAdd = DateTime.Now,
                                            dUpdate = DateTime.Now,
                                            sPlaneID = c.SPlaneId,
                                            nTerm = nterm,
                                            SchoolID = schoolid,
                                            nUserAdd = userId,
                                            nUserUpdate = userId,
                                            //PlanId = planId
                                        };

                                        _dbGrade.TGrades.Add(tgrade);
                                        _dbGrade.SaveChanges();

                                        PlanList2.code = c.CourseCode;

                                        //Move to History
                                        if (tgrade.nGradeId > 0)
                                        {

                                            PlanList2.gradeId = tgrade.nGradeId;
                                            var tgradeHistory = new GradeHistoryEntity.TGradeHistory
                                            {
                                                dAdd = tgrade.dAdd,
                                                dUpdate = tgrade.dUpdate,
                                                sPlaneID = tgrade.sPlaneID,
                                                nTerm = tgrade.nTerm,
                                                SchoolID = tgrade.SchoolID,
                                                nUserAdd = tgrade.nUserAdd,
                                                nUserUpdate = tgrade.nUserUpdate,
                                                nGradeId = tgrade.nGradeId,
                                                //PlanId = tgrade.PlanId
                                            };

                                            _dbGradeHistory.TGradeHistories.Add(tgradeHistory);
                                            _dbGradeHistory.SaveChanges();

                                            //Delete the record from Main table after moved to history
                                            //_dbGrade.TGrades.Remove(tgrade);
                                            //_dbGrade.SaveChanges();
                                        }



                                        gradeTransaction.Commit();
                                        gradeHistoryTransaction.Commit();
                                    }
                                    catch (System.Exception)
                                    {
                                        gradeTransaction?.Rollback();
                                        gradeHistoryTransaction?.Rollback();
                                        throw;
                                    }
                                }

                            }
                            else
                            {
                                //check.PlanId = planId;
                                check.nUserUpdate = userId;
                                check.dUpdate = DateTime.Now;
                                _dbGradeHistory.SaveChanges();

                                PlanList2.SchoolID = schoolid;
                                PlanList2.code = c.CourseCode;
                                PlanList2.gradeId = check.nGradeId;
                            }

                            PlanList2List.Add(PlanList2);
                        }


                        PlanList2List = PlanList2List.Distinct().ToList();
                        //_dbGradeHistory.SaveChanges();

                        int countloop = int.Parse(TextBox13.Text);

                        string[] detailCode = new string[countloop];
                        string[] detailStd = new string[countloop];
                        string[] detailGrade = new string[countloop];

                        string _commUser = "", _commGrade = "";

                        if (stdListList != null)
                        {
                            stdListList.ToList().ForEach(f =>
                            {
                                _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sid + "'";
                            });
                        }

                        if (PlanList2List != null)
                        {
                            PlanList2List.ForEach(f =>
                            {
                                _commGrade += (string.IsNullOrEmpty(_commGrade) ? "" : ",") + "'" + f.gradeId + "'";
                            });
                        }

                        if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND sID  IN (" + _commUser + ")";
                        if (!string.IsNullOrEmpty(_commGrade)) _commGrade = " AND nGradeId  IN (" + _commGrade + ")";

                        var SQL = (!string.IsNullOrEmpty(_commUser) && !string.IsNullOrEmpty(_commGrade)) ? string.Format("SELECT * FROM TGradeDetailHistory WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", _commUser, _commGrade, userData.CompanyID) : null;
                        var q_GradeDetailViewHistory = (SQL != null) ? _dbGradeHistory.Database.SqlQuery<GradeHistoryEntity.TGradeDetailHistory>(SQL).ToList() : new List<GradeHistoryEntity.TGradeDetailHistory>();

                        using (DbContextTransaction gradeTransaction = _dbGrade.Database.BeginTransaction())
                        using (DbContextTransaction gradeHistoryTransaction = _dbGradeHistory.Database.BeginTransaction())
                        {
                            try
                            {
                                var newGradeDetails = (from gv in GridView2Values
                                                       join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                       join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                       join gd in q_GradeDetailViewHistory on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                       from gdetail in gdetails.DefaultIfEmpty()
                                                       where (gdetail == null || (gdetail != null && gdetail.nGradeDetailId == 0))
                                                       select new TGradeDetail
                                                       {
                                                           SchoolID = schoolid,
                                                           sID = stddata.sid,
                                                           nGradeId = gradedata.gradeId,
                                                           getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                           getSpecial = GetGradeSpecial(gv.gradetxt),
                                                           UpdatedBy = Utils.GetUserId(),
                                                           CreatedBy = Utils.GetUserId(),
                                                           CreatedDate = DateTime.Now,
                                                           UpdatedDate = DateTime.Now,
                                                           getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                           cDel = false,
                                                           PlanId = planId
                                                       }).ToList();
                                if (newGradeDetails != null && newGradeDetails.Count > 0)
                                {
                                    newGradeDetails = newGradeDetails.Where(w => w.nGradeId > 0).ToList();

                                    var ngradeIds = newGradeDetails.Select(w => w.nGradeId).Distinct().ToList();
                                    //Insert into main table and move to history
                                    _dbGrade.BulkInsert<JabjaiSchoolGradeEntity.TGradeDetail>(newGradeDetails);
                                    _dbGrade.SaveChanges();

                                    _commGrade = ""; _commUser = "";
                                    if (ngradeIds != null)
                                    {
                                        ngradeIds.ForEach(f =>
                                        {
                                            _commGrade += (string.IsNullOrEmpty(_commGrade) ? "" : ",") + "" + f + "";
                                        });
                                    }

                                    if (stdListList != null)
                                    {
                                        stdListList.ToList().ForEach(f =>
                                        {
                                            _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "" + f.sid + "";
                                        });
                                    }

                                    if (!string.IsNullOrEmpty(_commUser)) _commUser = " AND GD.sID  IN (" + _commUser + ")";
                                    if (!string.IsNullOrEmpty(_commGrade)) _commGrade = " AND GD.nGradeId  IN (" + _commGrade + ")";

                                    SQL = (!string.IsNullOrEmpty(_commUser) && !string.IsNullOrEmpty(_commGrade)) ? string.Format("SELECT * FROM TGradeDetail GD LEFT JOIN jabjaiSchoolGradeHistory.dbo.TGradeDetailHistory GH ON GD.nGradeDetailId = GH.nGradeDetailId and GD.nGradeId = GH.nGradeId and GD.sID = GH.sID WHERE 1=1 AND GH.nGradeId IS NULL {0}  {1} and GD.SchoolID={2} and GD.cDel = 0", _commUser, _commGrade, userData.CompanyID) : null;
                                    var tGradeDetailsInsertedRecord = (SQL != null) ? _dbGrade.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();




                                    //new records move to history
                                    var newGradeDetailsHistory = (from gd in tGradeDetailsInsertedRecord
                                                                  select new GradeHistoryEntity.TGradeDetailHistory
                                                                  {
                                                                      SchoolID = schoolid,
                                                                      sID = gd.sID,
                                                                      nGradeId = gd.nGradeId,
                                                                      getGradeLabel = gd.getGradeLabel,
                                                                      getSpecial = gd.getSpecial,
                                                                      UpdatedBy = gd.UpdatedBy,
                                                                      CreatedBy = gd.CreatedBy,
                                                                      CreatedDate = gd.CreatedDate,
                                                                      UpdatedDate = gd.UpdatedDate,
                                                                      getBehaviorLabel = gd.getBehaviorLabel,
                                                                      getBehaviorTotal = gd.getBehaviorTotal,
                                                                      getReadWrite = gd.getReadWrite,
                                                                      getReadWriteTotal = gd.getReadWriteTotal,
                                                                      getSamattana = gd.getSamattana,
                                                                      getSamattanaTotal = gd.getSamattanaTotal,
                                                                      cDel = false,
                                                                      nGradeDetailId = gd.nGradeDetailId,
                                                                      PlanId = gd.PlanId
                                                                  }).ToList();

                                    var newGradeDetailsForDelete = (from gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && ngradeIds.Contains(w.nGradeId))
                                                                    join nGradeId in ngradeIds on gd.nGradeId equals nGradeId
                                                                    select gd).ToList();

                                    _dbGradeHistory.TGradeDetailHistories.AddRange(newGradeDetailsHistory);
                                    _dbGradeHistory.SaveChanges();

                                    //After moved to history delete from main table
                                    _dbGrade.TGradeDetails.RemoveRange(newGradeDetailsForDelete);
                                    _dbGrade.SaveChanges();

                                    //BulkInsertTGradeDetail(newGradeDetails);
                                }
                                var oldGradeDetailsHistory = new List<GradeHistoryEntity.TGradeDetailHistory>();
                                if ((GridView2Values != null && GridView2Values.Count == 0) && ((gvDesirableAttributeValues != null && gvDesirableAttributeValues.Count > 0) || (gvReadWriteValues != null && gvReadWriteValues.Count > 0) || (gvSamattanaValues != null && gvSamattanaValues.Count > 0)))
                                {
                                    //Update Only Behavior/ReadWrite/Samattana - Ex: If Excel don't have grade data and only have Behavior/ReadWrite/Samattan 
                                    //It is for import the Behaviour/ReadWrite/Samattana values.
                                    oldGradeDetailsHistory = (from stddata in stdListList
                                                              join gd in q_GradeDetailViewHistory on new { sID = stddata.sid } equals new { gd.sID } into gdetails
                                                              from gdetail in gdetails.DefaultIfEmpty()
                                                              where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                              select new GradeHistoryEntity.TGradeDetailHistory
                                                              {
                                                                  SchoolID = schoolid,
                                                                  sID = stddata.sid,
                                                                  nGradeId = gdetail.nGradeId,
                                                                  UpdatedBy = Utils.GetUserId(),
                                                                  CreatedBy = Utils.GetUserId(),
                                                                  CreatedDate = DateTime.Now,
                                                                  UpdatedDate = DateTime.Now,
                                                                  cDel = false,
                                                                  nGradeDetailId = gdetail.nGradeDetailId,
                                                                  getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == stddata.code).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  PlanId = planId
                                                              }).ToList();


                                    if (oldGradeDetailsHistory != null && oldGradeDetailsHistory.Count > 0)
                                    {
                                        foreach (var gd in oldGradeDetailsHistory)
                                        {
                                            var tgradeDetailHistory = _dbGradeHistory.TGradeDetailHistories.FirstOrDefault(w => w.SchoolID == userData.CompanyID && w.nGradeId == gd.nGradeId && w.sID == gd.sID && w.cDel == false);
                                            if (tgradeDetailHistory != null)
                                            {
                                                if (gd.getBehaviorLabel != null)
                                                {
                                                    tgradeDetailHistory.getBehaviorLabel = gd.getBehaviorLabel;
                                                    tgradeDetailHistory.getBehaviorTotal = gd.getBehaviorTotal;
                                                    tgradeDetailHistory.PlanId = planId;
                                                }

                                                if (gd.getReadWrite != null)
                                                {
                                                    tgradeDetailHistory.getReadWrite = gd.getReadWrite;
                                                    tgradeDetailHistory.getReadWriteTotal = gd.getReadWriteTotal;
                                                    tgradeDetailHistory.PlanId = planId;
                                                }

                                                if (gd.getSamattana != null)
                                                {
                                                    tgradeDetailHistory.getSamattana = gd.getSamattana;
                                                    tgradeDetailHistory.getSamattanaTotal = gd.getSamattanaTotal;
                                                    tgradeDetailHistory.PlanId = planId;
                                                }

                                            }
                                        }
                                        _dbGradeHistory.SaveChanges();
                                    }
                                }
                                else
                                {
                                    oldGradeDetailsHistory = (from gv in GridView2Values
                                                              join gradedata in PlanList2List on gv.codetxt equals gradedata.code
                                                              join stddata in stdListList on gv.stdnumtxt equals stddata.code
                                                              join gd in q_GradeDetailViewHistory on new { nGradeId = gradedata.gradeId, sID = stddata.sid } equals new { gd.nGradeId, gd.sID } into gdetails
                                                              from gdetail in gdetails.DefaultIfEmpty()
                                                              where ((gdetail != null && gdetail.nGradeDetailId > 0))
                                                              select new GradeHistoryEntity.TGradeDetailHistory
                                                              {
                                                                  SchoolID = schoolid,
                                                                  sID = stddata.sid,
                                                                  nGradeId = gradedata.gradeId,
                                                                  getGradeLabel = GetGradeLabel(gv.gradetxt),
                                                                  getSpecial = GetGradeSpecial(gv.gradetxt),
                                                                  UpdatedBy = Utils.GetUserId(),
                                                                  CreatedBy = Utils.GetUserId(),
                                                                  CreatedDate = DateTime.Now,
                                                                  UpdatedDate = DateTime.Now,
                                                                  cDel = false,
                                                                  nGradeDetailId = gdetail.nGradeDetailId,
                                                                  getBehaviorLabel = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getBehaviorTotal = (gvDesirableAttributeValues != null) ? gvDesirableAttributeValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getReadWrite = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getReadWriteTotal = (gvReadWriteValues != null) ? gvReadWriteValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getSamattana = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  getSamattanaTotal = (gvSamattanaValues != null) ? gvSamattanaValues.Where(w => w.stdnumtxt == gv.stdnumtxt).Select(s => s.gradetxt).FirstOrDefault() : null,
                                                                  PlanId = planId
                                                              }).ToList();

                                    if (oldGradeDetailsHistory != null && oldGradeDetailsHistory.Count > 0)
                                    {
                                        foreach (var gd in oldGradeDetailsHistory)
                                        {
                                            var tgradeDetailHistory = _dbGradeHistory.TGradeDetailHistories.FirstOrDefault(w => w.sID == gd.sID && w.nGradeId == gd.nGradeId && w.SchoolID == userData.CompanyID && w.cDel == false);
                                            if (tgradeDetailHistory != null)
                                            {
                                                tgradeDetailHistory.getGradeLabel = gd.getGradeLabel;
                                                tgradeDetailHistory.getSpecial = gd.getSpecial;
                                                tgradeDetailHistory.PlanId = planId;

                                                if (gd.getBehaviorLabel != null)
                                                {
                                                    tgradeDetailHistory.getBehaviorLabel = gd.getBehaviorLabel;
                                                    tgradeDetailHistory.getBehaviorTotal = gd.getBehaviorTotal;
                                                }

                                                if (gd.getReadWrite != null)
                                                {
                                                    tgradeDetailHistory.getReadWrite = gd.getReadWrite;
                                                    tgradeDetailHistory.getReadWriteTotal = gd.getReadWriteTotal;
                                                }

                                                if (gd.getSamattana != null)
                                                {
                                                    tgradeDetailHistory.getSamattana = gd.getSamattana;
                                                    tgradeDetailHistory.getSamattanaTotal = gd.getSamattanaTotal;
                                                }
                                            }
                                        }
                                        _dbGradeHistory.SaveChanges();
                                    }
                                }

                                gradeTransaction.Commit();
                                gradeHistoryTransaction.Commit();
                            }
                            catch (System.Exception ex)
                            {
                                gradeTransaction?.Rollback();
                                gradeHistoryTransaction?.Rollback();
                                throw;
                            }
                        }


                    }

                }

                return String.Join(",", courseCode);
            }
        }
        private void BulkInsertTGradeDetail(List<TGradeDetail> newRecord)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {
                var connStr = schoolDbContext.Database.Connection.ConnectionString;

                using (var connection = new SqlConnection(connStr))
                {
                    var startTime = DateTime.Now;
                    connection.Open();
                    var transaction = connection.BeginTransaction();
                    try
                    {

                        //var connStr = connection.ConnectionString;
                        using (var sbCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        {
                            sbCopy.BulkCopyTimeout = 0;
                            sbCopy.BatchSize = 10000;
                            sbCopy.DestinationTableName = "TGradeDetail";

                            sbCopy.WriteToServer(newRecord.AsDataTable());
                        }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine(ex.Message);
                        transaction.Rollback();
                    }
                    finally
                    {
                        transaction.Dispose();
                        connection.Close();
                        var endTime = DateTime.Now;
                        //Console.WriteLine("Upload time elapsed: {0} seconds", (endTime - startTime).TotalSeconds);
                    }
                }
            }

        }

        public string GetGradeLabel(string gradetxt)
        {
            double n;
            bool isNumeric = double.TryParse(gradetxt, out n);
            if (isNumeric == true)
            {
                return gradetxt;
            }
            else if ((gradetxt == "ผ") || (gradetxt == "ผ."))
                return gradetxt;
            else if ((gradetxt == "มผ") || (gradetxt == "มผ."))
                return gradetxt;
            else if ((gradetxt == "ร") || (gradetxt == "ร."))
                return gradetxt;
            else if ((gradetxt == "มส") || (gradetxt == "มส."))
                return gradetxt;
            else if ((gradetxt == "มก") || (gradetxt == "มก."))
                return gradetxt;
            else if ((gradetxt == "ขร") || (gradetxt == "ขร."))
                return gradetxt;
            else if ((gradetxt == "ขส") || (gradetxt == "ขส."))
                return gradetxt;
            else if (gradetxt == "ท")
                return gradetxt;
            else if (gradetxt == "อื่นๆ")
                return gradetxt;
            else if ((gradetxt == "ดีเยี่ยม") || (gradetxt == "ดี") || (gradetxt == "พอใช้") || (gradetxt == "ปรับปรุง"))
            {
                return "";
            }
            else
            {
                return "";
            }

        }

        public string GetGradeSpecial(string gradetxt)
        {
            double n;
            bool isNumeric = double.TryParse(gradetxt, out n);
            if (isNumeric == true)
            {
                return "-1";
            }
            else if ((gradetxt == "ผ") || (gradetxt == "ผ."))
                return "4";
            else if ((gradetxt == "มผ") || (gradetxt == "มผ."))
                return "5";
            else if ((gradetxt == "ร") || (gradetxt == "ร."))
                return "1";
            else if ((gradetxt == "มส") || (gradetxt == "มส."))
                return "2";
            else if ((gradetxt == "มก") || (gradetxt == "มก."))
                return "3";
            else if ((gradetxt == "ขร") || (gradetxt == "ขร."))
                return "7";
            else if ((gradetxt == "ขส") || (gradetxt == "ขส."))
                return "8";
            else if (gradetxt == "ท")
                return "9";
            else if (gradetxt == "อื่นๆ")
                return "6";
            else if (gradetxt == "ดีเยี่ยม")
            {
                return "10";
            }
            else if (gradetxt == "ดี")
            {
                return "11";
            }
            else if (gradetxt == "พอใช้")
            {
                return "12";
            }
            else if (gradetxt == "ปรับปรุง")
            {
                return "13";
            }
            return "-1";
        }

        void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("importExcel.aspx");
        }

        //protected class PlanList
        //{
        //    public int number { get; set; }           
        //}
        protected class PlanList2
        {
            public string code { get; set; }
            public int gradeId { get; set; }
            public int SchoolID { get; set; }
        }
        protected class stdList
        {
            public string code { get; set; }
            public int sid { get; set; }
            public int SchoolID { get; set; }
        }
        protected class ddl
        {
            public string name { get; set; }
            public string value { get; set; }
            public int sort { get; set; }
            public int SchoolID { get; set; }
        }

        protected class GradeDetails
        {
            public string codetxt { get; set; }
            public string stdnumtxt { get; set; }
            public string gradetxt { get; set; }
        }

        protected void ddlSubLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var numberYear = !string.IsNullOrEmpty(DropDownList1.SelectedValue) ? int.Parse(DropDownList1.SelectedValue) : 0;
            var sTerm = DropDownList2.SelectedValue;
            var nTSubLevel = !string.IsNullOrEmpty(ddlSubLevel.SelectedValue) ? int.Parse(ddlSubLevel.SelectedValue) : 0;

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {
                //var yearDTO = CommonService.GetYearByYearNumber(numberYear, userData.CompanyID, userData.UserID);
                var termDTOs = CommonService.GetTermsByNumberYear(numberYear, false, userData.CompanyID, userData.UserID);
                if (termDTOs != null)
                {
                    var term = termDTOs.Where(w => w.sTerm == sTerm).FirstOrDefault();
                    if (term != null && nTSubLevel > 0)
                    {
                        var planCourseDTOs = CommonService.GetPlanCoursesForImportGrade(nTSubLevel, 0, term.nTerm, term.nYear, userData.CompanyID, userData.UserID);
                        if (planCourseDTOs != null && planCourseDTOs.Count == 0)
                        {
                            ServiceHelper.CreatePlanFromPage(nTSubLevel, 0, term.nTerm, term.nYear, userData.CompanyID, userData.UserID, numberYear);
                            planCourseDTOs = CommonService.GetPlanCourses(nTSubLevel, 0, term.nTerm, term.nYear, userData.CompanyID, userData.UserID);

                        }

                        if (planCourseDTOs != null)
                        {
                            var planCourseIds = planCourseDTOs.Select(w => w.PlanCourseId).ToList();
                            var planDetail = (from p in planCourseDTOs
                                              join pc in _db.TPlanCourses.Where(w => w.SchoolID == userData.CompanyID) on new { p.PlanCourseId, sPlaneID = p.SPlaneId } equals new { pc.PlanCourseId, pc.sPlaneID }
                                              join t in _db.TPlans.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false && w.nTSubLevel == nTSubLevel) on pc.PlanId equals t.PlanId
                                              select new
                                              {
                                                  PlanId = t.PlanId,
                                                  PlanName = t.PlanName
                                              }).Distinct().ToList();

                            ddlPlan.Items.Clear();
                            ddlPlan.Items.Add(new ListItem
                            {
                                Text = "กรุณาเลือก",
                                Value = "0"
                            });

                            if (planDetail != null)
                            {
                                foreach (var t in planDetail)
                                {
                                    var item = new ListItem
                                    {
                                        Text = t.PlanName,
                                        Value = t.PlanId.ToString()
                                    };
                                    ddlPlan.Items.Add(item);
                                }
                            }

                        }
                    }

                }
                //userData.CompanyID
            }

        }
    }
}