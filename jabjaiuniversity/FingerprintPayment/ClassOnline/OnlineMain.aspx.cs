using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.Data.OData.Query.SemanticAst;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineMain : BaseOnlinePage
    {
        [Serializable]
        protected class CurrentPlan
        {
            public int nYear { get; set; }
            public int numberYear { get; set; }
            public string nTerm { get; set; }
            public string sTerm { get; set; }
            public int sPlaneID { get; set; }
            public string courseCode { get; set; }
            public int courseGroup { get; set; }
            public int courseType { get; set; }
            public string sPlaneName { get; set; }
            public int nTSubLevel { get; set; }
            public string SubLevel { get; set; }

            //public int? YearId { get; set; }
            //public int? Year { get; set; }
            //public string Term { get; set; }
            //public int? SubjectId { get; set; }
            //public string Subject { get; set; }
            //public string Level { get; set; }
            ////public string Room { get; set; }
            ////public int? TeacherId { get; set; }
            ////public string TeacherFirst { get; set; }
            ////public string TeacherLast { get; set; }
            //public string TermId { get; internal set; }
            //public int LevelId { get; internal set; }
            //public string SubjectCode { get; internal set; }
            //public int? CourseGroup { get; internal set; }
            //public int? CourseType { get; internal set; }
        }

        protected List<CurrentPlan> ListPlan
        {
            get
            {
                object o = ViewState["ListPlan"];
                return o as List<CurrentPlan>;
            }

            set
            {
                ViewState["ListPlan"] = value;
            }
        }

        protected List<CurrentPlan> _listDataPlay = new List<CurrentPlan>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");
            //ddlcType.SelectedIndexChanged += DdlcType_SelectedIndexChanged;
            //dgd.ItemCommand += Dgd_ItemCommand;
            if (!this.IsPostBack)
            {
                //var u = UserFunction.FindUserByNFCCard("5078587a");

                Initial();

                //LoadList(ctx, current?.nYear, current?.nTerm);

            }
        }

        private void Initial()
        {
            var dateNow = DateTime.Now;
            var teacherId = UserData.UserID;
            var SchoolId = UserData.CompanyID;

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                //ctx.Configuration.ProxyCreationEnabled = false;

                if (ListPlan == null)
                {
                    //var qry = from a in ctx.TSchedules
                    //            .Where(o => o.sEmp == teacherId && o.cDel != true && o.SchoolID == SchoolId)
                    //          join b in ctx.TTermTimeTables on a.nTermTable equals b.nTermTable
                    //          join c in ctx.TPlanes on a.sPlaneID equals c.sPlaneID
                    //          join d in ctx.TTermSubLevel2 on b.nTermSubLevel2 equals d.nTermSubLevel2
                    //          join e in ctx.TSubLevels on d.nTSubLevel equals e.nTSubLevel
                    //          join f in ctx.TTerms on b.nTerm equals f.nTerm
                    //          join g in ctx.TYears on f.nYear equals g.nYear
                    //          join h in ctx.TEmployees on a.sEmp equals h.sEmp

                    //var qry = from a in ctx.TPlanCourseTeachers
                    //          .Where(o => o.SchoolID == SchoolId && o.cDel == false && o.sEmp == teacherId)
                    //          from b in ctx.TPlanCourses.Where(o => o.PlanCourseId == a.PlanCourseId && o.IsActive == true)
                    //          from c in ctx.TPlanes.Where(o => o.sPlaneID == b.sPlaneID && o.cDel != "1")
                    //          from d in ctx.TPlanCourseTerms.Where(o => o.IsActive == true && o.PlanCourseId == b.PlanCourseId)
                    //          from e in ctx.TTerms.Where(o => o.nTerm == d.nTerm && o.cDel != "1")
                    //          from f in ctx.TYears.Where(o => o.nYear == e.nYear && o.cDel == false)

                    //          from g in ctx.TPlans.Where(o => o.PlanId == b.PlanId && o.IsActive == true)
                    //          from h in ctx.TSubLevels.Where(o => o.cDel == false && o.nTSubLevel == g.nTSubLevel)

                    //          select new CurrentPlan
                    //          {
                    //              YearId = f.nYear,
                    //              Year = f.numberYear,
                    //              TermId = e.nTerm,
                    //              Term = e.sTerm,
                    //              SubjectId = c.sPlaneID,
                    //              SubjectCode = c.courseCode,
                    //              Subject = c.sPlaneName,
                    //              LevelId = h.nTSubLevel,
                    //              Level = h.SubLevel,
                    //              //Room = d.nTSubLevel2,
                    //              //TeacherId = a.sEmp,
                    //              //TeacherFirst = h.sName,
                    //              //TeacherLast = h.sLastname,

                    //              CourseGroup = c.courseGroup,
                    //              CourseType = c.courseType,
                    //          };

                    var q = $@" select 
    Y.nYear , Y.numberYear , T.nTerm , T.sTerm ,
    C.sPlaneID , P.courseCode , P.courseGroup , P.courseType ,P.sPlaneName , TL1.nTSubLevel , TL1.SubLevel
		
    from TPlanCourse C
    join TPlane P ON C.sPlaneID = P.sPlaneID and C.SchoolID = P.SchoolID
    join TPlanTermSubLevel2 TS2 ON C.PlanId = TS2.PlanId and TS2.SchoolID = C.SchoolID
    join TPlan ON C.PlanId = TPlan.PlanId and C.SchoolID = TPlan.SchoolID		
    join TCurriculum Cu ON TPlan.CurriculumId = Cu.CurriculumId and Cu.SchoolId = TPlan.SchoolID
    join TYear Y ON Cu.nYear = Y.nYear and Cu.SchoolID = Y.SchoolID
    join TTerm T ON Y.nYear = T.nYear and Y.SchoolID = T.SchoolID
    join TPlanCourseTerm CT ON CT.PlanCourseId = C.PlanCourseId  and T.nTerm = CT.nTerm 
    and CT.IsActive = 1 and CT.SchoolID = C.SchoolID  
    join TTermSubLevel2 TL2 on  TL2.nTermSubLevel2 = TS2.nTermSubLevel2 and TL2.SchoolID = TS2.SchoolID
    join TSubLevel TL1 ON TL2.nTSubLevel = TL1.nTSubLevel and TL2.SchoolID = TL1.SchoolID
    join TPlanCourseTeacher TT on c.PlanCourseId = TT.PlanCourseId and TT.SchoolID = c.SchoolID

where C.CourseStatus = 1 and C.IsActive = 1     
    and P.cDel IS NULL 
    and TPlan.IsActive = 1   
    and TS2.IsActive = 1 
    and Cu.IsActive = 1  and Cu.SchoolId = {SchoolId}
    and T.cDel IS NULL 
    and C.SchoolID = {SchoolId} { (IsSuperUser() ? "" : "and TT.sEmp = " + teacherId)} 
    and TT.IsActive = 1 
    and ISNULL(TT.cDel,0) = 0

order by P.courseGroup , P.courseType asc ";

                    var qry = ctx.Database.SqlQuery<CurrentPlan>(q);

                    ListPlan = qry.ToList();
                }

                var _lst = ListPlan;

                if (_lst.Select(o => o.numberYear).GroupBy(o => o).Count() > 1)
                {
                    var current = ctx.TTerms
                        .Where(o => o.dStart <= dateNow && dateNow <= o.dEnd && o.SchoolID == SchoolId && o.cDel != "1")
                        .FirstOrDefault();

                    if (current == null)
                        current = ctx.TTerms
                            .Where(w => w.SchoolID == SchoolId)
                            .OrderBy(o => o.dEnd)
                            .FirstOrDefault(f => f.dStart >= dateNow);

                    if (current == null)
                        current = ctx.TTerms
                            .Where(w => w.SchoolID == SchoolId)
                            .OrderByDescending(o => o.dEnd)
                            .FirstOrDefault(f => f.dEnd <= dateNow);

                    if (current != null && _lst.Count(o => o.nYear == current.nYear) > 0)
                    {
                        SetListYear(_lst);
                        SetListTerm(_lst.Where(o => o.nYear == current.nYear).ToList());
                        SetListLevel(_lst.Where(o => o.nYear == current.nYear && o.nTerm == current.nTerm).ToList());

                        ddlYear.SelectedValue = current.nYear + "";
                        ddlTerm.SelectedValue = current.nTerm;
                    }
                    else
                    {
                        var year = _lst.Select(o => o.nYear).OrderByDescending(o => o).First();
                        var term = _lst.Select(o => o.nTerm).OrderByDescending(o => o).First();

                        SetListYear(_lst);
                        SetListTerm(_lst.Where(o => o.nYear == year).ToList());
                        SetListLevel(_lst.Where(o => o.nYear == year && o.nTerm == term).ToList());

                        ddlYear.SelectedValue = year + "";
                        ddlTerm.SelectedValue = term;
                    }
                }
                else
                {
                    SetListYear(_lst);
                    SetListTerm(_lst.ToList());
                    SetListLevel(_lst.ToList());
                }



            }
        }

        private void LoadList(int? year, string term, int? level)
        {
            //using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            //{
            //var teacherId = Convert.ToInt32(Session["sEmpID"] + "");

            //var qry = from a in ctx.TSchedules.Where(o => o.sEmp == teacherId)
            //          join b in ctx.TTermTimeTables on a.nTermTable equals b.nTermTable
            //          join c in ctx.TPlanes on a.sPlaneID equals c.sPlaneID
            //          join d in ctx.TTermSubLevel2 on b.nTermSubLevel2 equals d.nTermSubLevel2
            //          join e in ctx.TSubLevels on d.nTSubLevel equals e.nTSubLevel
            //          join f in ctx.TTerms on b.nTerm equals f.nTerm
            //          join g in ctx.TYears on f.nYear equals g.nYear
            //          join h in ctx.TEmployees on a.sEmp equals h.sEmp

            //          select new CurrentPlan
            //          {
            //              YearId = g.nYear,
            //              Year = g.numberYear,
            //              TermId = f.nTerm,
            //              Term = f.sTerm,
            //              SubjectId = c.sPlaneID,
            //              SubjectCode = c.courseCode,
            //              Subject = c.sPlaneName,
            //              LevelId = e.nTSubLevel,
            //              Level = e.SubLevel,
            //              Room = d.nTSubLevel2,
            //              TeacherId = a.sEmp,
            //              TeacherFirst = h.sName,
            //              TeacherLast = h.sLastname,
            //          };

            var qry = ListPlan.AsQueryable();

            if (year.HasValue)
            {
                qry = qry.Where(o => o.nYear == year);
            }

            if (!string.IsNullOrEmpty(term))
            {
                qry = qry.Where(o => o.nTerm == term);
            }

            if (level.HasValue)
            {
                qry = qry.Where(o => o.nTSubLevel == level);
            }

            _listDataPlay = qry.ToList();
            // }
        }

        private void SetListLevel(List<CurrentPlan> lst)
        {
            var lstLevel = lst
               .Select(o => new ListItem { Value = o.nTSubLevel + "", Text = o.SubLevel + "" })
               .Distinct()
               .OrderBy(o => o.Text)
               .ToList();
            lstLevel.Insert(0, new ListItem { Value = "", Text = "ทุกชั้นเรียน" });
            ddlLevel.DataSource = lstLevel;
            ddlLevel.DataBind();
        }

        private void SetListTerm(List<CurrentPlan> lst)
        {
            var lstTerm = lst//.Where(o => o.TermId == ddlTerm.SelectedValue)
                .Select(o => new ListItem { Value = o.nTerm + "", Text = o.sTerm })
                .Distinct()
                .OrderBy(o => o.Text)
                .ToList();
            //lstTerm.Insert(0, new ListItem { Value = "", Text = "ทุกเทอม" });
            ddlTerm.DataSource = lstTerm;
            ddlTerm.DataBind();
        }

        private void SetListYear(List<CurrentPlan> lst)
        {
            var lstYear = lst
                .Select(o => new ListItem { Value = o.nYear + "", Text = o.numberYear + "" })
                .Distinct()
                .OrderByDescending(o => o.Text)
                .ToList();
            //lstYear.Insert(0, new ListItem { Value = "", Text = "ทุกปีการศึกษา" });
            ddlYear.DataSource = lstYear;
            ddlYear.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var year = ToNullableInt(ddlYear.SelectedValue);
            var term = ddlTerm.SelectedValue;
            var level = ToNullableInt(ddlLevel.SelectedValue);
            LoadList(year, term, level);

        }


        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _lst = ListPlan;
            var year = ToNullableInt(ddlYear.SelectedValue);


            if (year.HasValue)
            {
                _lst = _lst.Where(o => o.nYear == year).ToList();
            }

            SetListTerm(_lst);
            SetListLevel(_lst);
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _lst = ListPlan;
            var year = ToNullableInt(ddlYear.SelectedValue);
            var term = ddlTerm.SelectedValue;

            if (year.HasValue)
            {
                _lst = _lst.Where(o => o.nYear == year).ToList();
            }
            if (!string.IsNullOrEmpty(term))
            {
                _lst = _lst.Where(o => o.nTerm == term).ToList();
            }
            SetListLevel(_lst);
        }

        


    }
}