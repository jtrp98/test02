using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class GradeRoomList : System.Web.UI.Page
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
           

            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                {
                    string comid = Request.QueryString["comid"];
                    int sEmpID = int.Parse(Session["sEmpID"] + "");



                    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                    if (!IsPostBack)
                    {
                        OpenData();

                        DateTime today = DateTime.Now;
                        var check1 = _db.TTerms.Where(w => w.SchoolID == schoolid && w.dStart <= today && w.dEnd >= today && w.cDel == null).FirstOrDefault();
                        if (check1 != null)
                            todayterm.Text = check1.sTerm;

                        string yearr = Request.QueryString["year"];
                        DropDownList1.SelectedValue = yearr;
                        string termm = Request.QueryString["term"];
                        DropDownList2.SelectedValue = termm;
                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];

                        string mode = Request.QueryString["mode"];
                        modetxt.Text = mode;

                        List<TSubLevel> SubLevel = new List<TSubLevel>();
                        TSubLevel sub = new TSubLevel();

                        foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == schoolid && w.nWorkingStatus == 1))
                        {
                            sub = new TSubLevel();
                            sub = a;
                            SubLevel.Add(sub);
                        }

                        ddlsublevel.Items.Clear();
                        ddlsublevel.Items.Add(new ListItem
                        {
                            Text = "กรุณาเลือก",
                            Value = ""
                        });

                        foreach (var t in SubLevel)
                        {
                            var item = new ListItem
                            {
                                Text = t.SubLevel,
                                Value = t.nTSubLevel.ToString()
                            };
                            ddlsublevel.Items.Add(item);
                        }

                        ddlsublevel.SelectedValue = idlv;

                        txtddl1.Text = termm;
                        txtddl2.Text = idlv2;

                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();
                        var selectedYear = yearr;
                        foreach (var a in _db.TYears.Where(w =>  w.SchoolID == userData.CompanyID).ToList())
                        {
                            ty = new TYear();
                            ty = a;
                            tylist.Add(ty);
                            if (string.IsNullOrEmpty(selectedYear) && check1 != null && a.nYear == check1.nYear)
                            {
                                selectedYear = a.numberYear.ToString();
                            }
                        }
                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();
                        DropDownList1.SelectedValue = selectedYear;
                        DropDownList1.DataSource = newList;
                        DropDownList1.DataTextField = "numberYear";
                        DropDownList1.DataValueField = "numberYear";
                        DropDownList1.DataBind();


                    }
                }
            }
        }


        protected List<PlanList> returnlist()
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                //using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade()))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
                {
                    int sEmpID = int.Parse(Session["sEmpID"] + "");

                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];
                    string year = Request.QueryString["year"];
                    string term = Request.QueryString["term"];
                    string mode = Request.QueryString["mode"];

                    PlanList Plan = new PlanList();
                    List<PlanList> PlanList = new List<PlanList>();

                    if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                    {
                        int? idlv2n = Int32.Parse(idlv2);
                        int? idlvn = Int32.Parse(idlv);
                        int? useryear = Int32.Parse(year);
                        int nyear = 0;
                        string nterm = "";
                        List<string> planIdlist = new List<string>();


                        foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == useryear))
                        {
                            nyear = ff.nYear;
                        }

                        foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear && w.cDel == null))
                        {
                            nterm = ee.nTerm;
                        }

                        var isRequestForCurrentAcademicYear = ServiceHelper.IsRequestForCurrentAcademicYear(useryear ?? 0, nterm, schoolid);

                        if (nCompany.settingTimePeriod == 1)
                        {

                            var time = _db.TGradeRegisterPeriods.Where(w => w.SchoolID == schoolid && w.nTerm == nterm).FirstOrDefault();
                            if (time == null)
                                Textbox1.Text = "null";
                            else
                            {
                                var now = DateTime.Now.Date;

                                if (time.afterMidtermStart != null || time.beforeMidtermStart != null ||
                                    time.duringMidtermStart != null || time.ExtraStart != null ||
                                    time.FinaltermStart != null || time.afterMidtermEnd != null ||
                                    time.beforeMidtermEnd != null || time.duringMidtermEnd != null ||
                                    time.ExtraEnd != null || time.FinaltermEnd != null)
                                {
                                    settingtime.Text = "1";
                                    Textbox1.Text = "out";
                                    if (time.beforeMidtermEnd != null && time.beforeMidtermStart != null)
                                        if ((now <= time.beforeMidtermEnd.Value.Date || now <= DateTime.ParseExact(time.beforeMidtermEnd.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture)) && now >= DateTime.ParseExact(time.beforeMidtermStart.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                            Textbox1.Text = "beforeMid";
                                    if (time.afterMidtermEnd != null && time.afterMidtermStart != null)
                                        if ((now <= time.afterMidtermEnd.Value.Date || now <= DateTime.ParseExact(time.afterMidtermEnd.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture)) && now >= DateTime.ParseExact(time.afterMidtermStart.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                            Textbox1.Text = "aftermid";
                                    if (time.duringMidtermEnd != null && time.duringMidtermStart != null)
                                        if ((now <= time.duringMidtermEnd.Value.Date || now <= DateTime.ParseExact(time.duringMidtermEnd.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture)) && now >= DateTime.ParseExact(time.duringMidtermStart.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                            Textbox1.Text = "duringMid";

                                    if (time.FinaltermEnd != null && time.FinaltermStart != null)
                                        if ((now <= time.FinaltermEnd.Value.Date || now <= DateTime.ParseExact(time.FinaltermEnd.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture)) && now >= DateTime.ParseExact(time.FinaltermStart.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                            Textbox1.Text = "Final";
                                    if (time.ExtraEnd != null && time.ExtraStart != null)
                                        if ((now <= time.ExtraEnd.Value.Date || now <= DateTime.ParseExact(time.ExtraEnd.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture)) && now >= DateTime.ParseExact(time.ExtraStart.Value.Date.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture))
                                            Textbox1.Text = "Extra";
                                }
                            }
                        }
                        else Textbox1.Text = "";


                        int vipStatus = 0;
                        if (userData.IsAdmin)
                        {
                            vipStatus = 1;
                            Textbox1.Text = "";
                        }
                        else if (nCompany.settingGradeAdmin == 1)
                        {
                            var check = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == sEmpID).FirstOrDefault();
                            if (check != null)
                            {
                                if (check.gradeSystemAdmin == 1)
                                {
                                    vipStatus = 1;
                                    Textbox1.Text = "";
                                }
                            }
                        }

                        List<PlanCourseDTO> planCourseDTOs = new List<PlanCourseDTO>();

                        if (vipStatus == 1 || nCompany.settingPlanTeacher == 0)
                        {
                            planCourseDTOs = ServiceHelper.GetPlanCourses(idlvn ?? 0, idlv2n ?? 0, nterm, nyear, Utils.GetSchoolId(), _db);
                        }
                        else if (nCompany.settingPlanTeacher == 1 || (nCompany.settingPlanTeacher == null))
                        {

                            planCourseDTOs = ServiceHelper.GetPlanCoursesWithTeachers(idlvn ?? 0, idlv2n ?? 0, nterm, nyear, schoolid, _db);
                            if (planCourseDTOs != null)
                            {
                                planCourseDTOs = planCourseDTOs.Where(c => c.PlanCourseTeacherDTOs != null && c.PlanCourseTeacherDTOs.Any(a => a.SEmp == sEmpID)).ToList();
                            }

                            //If Teacher Mapped with any subject in the Time Table then show that subject only
                            if (planCourseDTOs != null && planCourseDTOs.Count > 0)
                            {
                                var checkTeacherExistInTimeTable = (from t in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == schoolid).ToList()
                                                                    join s in _db.TSchedules.Where(w => w.SchoolID == schoolid && w.cDel != true && (w.sEmp == sEmpID || w.sEmp == null)).ToList()
                                                                    on t.nTermTable equals s.nTermTable
                                                                    join p in planCourseDTOs on s.sPlaneID equals p.SPlaneId
                                                                    select p).Distinct().ToList();



                                if (checkTeacherExistInTimeTable != null && checkTeacherExistInTimeTable.Count > 0)
                                {
                                    planCourseDTOs = planCourseDTOs.Except(checkTeacherExistInTimeTable).ToList();

                                    if (planCourseDTOs != null && planCourseDTOs.Count > 0)
                                    {
                                        var checkAnyOtherTeacherMappedInTimeTable = (from t in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == schoolid).ToList()
                                                                                     join s in _db.TSchedules.Where(w => w.SchoolID == schoolid && w.cDel != true && (w.sEmp != sEmpID && w.sEmp != null)).ToList()
                                                                                     on t.nTermTable equals s.nTermTable
                                                                                     join p in planCourseDTOs on s.sPlaneID equals p.SPlaneId
                                                                                     select p).Distinct().ToList();
                                        planCourseDTOs = planCourseDTOs.Except(checkAnyOtherTeacherMappedInTimeTable).ToList();
                                    }
                                    planCourseDTOs.AddRange(checkTeacherExistInTimeTable);
                                    //planCourseDTOs = checkTeacherExistInTimeTable;
                                }
                                else if (checkTeacherExistInTimeTable != null && checkTeacherExistInTimeTable.Count == 0)
                                {
                                    if (planCourseDTOs != null && planCourseDTOs.Count > 0)
                                    {
                                        var checkAnyOtherTeacherMappedInTimeTable = (from t in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == schoolid).ToList()
                                                                                     join s in _db.TSchedules.Where(w => w.SchoolID == schoolid && w.cDel != true && (w.sEmp != sEmpID && w.sEmp != null)).ToList()
                                                                                     on t.nTermTable equals s.nTermTable
                                                                                     join p in planCourseDTOs on s.sPlaneID equals p.SPlaneId
                                                                                     select p).Distinct().ToList();
                                        if (checkAnyOtherTeacherMappedInTimeTable != null && checkAnyOtherTeacherMappedInTimeTable.Count > 0)
                                        {
                                            //Teacher assigned in time table but plan teacher removed. then we have remove the teacher from time table also.
                                            var getOtherTeacherMappedInTimeTable = (from t in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == schoolid).ToList()
                                                                                    join s in _db.TSchedules.Where(w => w.SchoolID == schoolid && w.cDel != true && (w.sEmp != sEmpID && w.sEmp != null)).ToList()
                                                                                    on t.nTermTable equals s.nTermTable
                                                                                    join p in planCourseDTOs on s.sPlaneID equals p.SPlaneId
                                                                                    select new { p.SPlaneId, s.sEmp, p.PlanCourseId, s.sScheduleID }).Distinct().ToList();

                                            foreach (var t in getOtherTeacherMappedInTimeTable)
                                            {
                                                var checkTeacherExistInPlan = planCourseDTOs.Where(w => w.SPlaneId == t.SPlaneId && w.PlanCourseTeacherDTOs.Any(a => a.SEmp == t.sEmp)).ToList();
                                                if (checkTeacherExistInPlan == null || (checkTeacherExistInPlan != null && checkTeacherExistInPlan.Count == 0))
                                                {
                                                    checkAnyOtherTeacherMappedInTimeTable = checkAnyOtherTeacherMappedInTimeTable.Where(w => w.SPlaneId != t.SPlaneId).ToList();
                                                    var tschedule = _db.TSchedules.Where(w => w.sScheduleID == t.sScheduleID).FirstOrDefault();
                                                    tschedule.sEmp = null;
                                                    _db.SaveChanges();
                                                }
                                            }
                                        }

                                        planCourseDTOs = planCourseDTOs.Except(checkAnyOtherTeacherMappedInTimeTable).ToList();
                                    }
                                }
                            }
                        }

                        List<sortList> sortList = new List<sortList>();
                        sortList sort = new sortList();

                        var students = (from s in _db.TB_StudentViews.Where(c => c.nTerm == nterm && c.nTermSubLevel2 == idlv2n && (c.cDel ?? "0") == "0" && c.SchoolID == schoolid)
                                        join u in _db.TUser.Where(w => w.SchoolID == schoolid) on s.sID equals u.sID
                                        select s).Distinct().ToList();

                        if (students == null || (students != null && students.Count == 0))
                        {
                            planCourseDTOs = new List<PlanCourseDTO>();
                        }

                        foreach (var planCourseDTO in planCourseDTOs)
                        {

                               
                            // var kk = _db.TPlanes.Where(w => w.sPlaneID == ii).FirstOrDefault();
                            //if (kk != null)
                            //{
                            sort = new sortList();
                            sort.PlanCourseId = planCourseDTO.PlanCourseId;
                            sort.planId = planCourseDTO.SPlaneId.ToString();
                            sort.planName = planCourseDTO.CourseName;

                            if (planCourseDTO.CourseType != null)
                            {
                                sort.sortnumberType = planCourseDTO.NOrder;
                            }
                            else sort.sortnumberType = 10;

                            if (planCourseDTO.CourseGroup != null)
                            {
                                sort.sortnumberGroup = planCourseDTO.CourseGroup;
                            }
                            else sort.sortnumberGroup = 10;

                            if (planCourseDTO.CourseCode != null)
                            {
                                sort.planCode = planCourseDTO.CourseCode;
                            }
                            else sort.planCode = "zzzz";

                            sortList.Add(sort);
                        }


                        var newSortList2 = sortList;
                        newSortList2 = sortList.OrderBy(x => x.sortnumberGroup).ThenBy(x => x.sortnumberType).ThenBy(x => x.planCode).ToList();


                        int number = 1;

                        foreach (var ii in newSortList2)
                        {
                            var kk = _db.TPlanes.Where(w => w.SchoolID == schoolid && w.sPlaneID.ToString() == ii.planId).FirstOrDefault();
                            if (kk != null)
                            {
                                Plan = new PlanList();
                                Plan.SchoolID = schoolid;
                                Plan.number = number;
                                number = number + 1;
                                Plan.planName = kk.sPlaneName;
                                Plan.entity = sEntities;
                                Plan.planId = kk.sPlaneID.ToString();
                                Plan.code = kk.courseCode;
                                Plan.year = year;
                                Plan.idlv = idlv;
                                Plan.idlv2 = idlv2;
                                Plan.term = term;
                                Plan.eid = sEmpID.ToString();
                                Plan.mode = mode;
                                Plan.havedata = "0";
                                Plan.PlanCourseId = ii.PlanCourseId;
                                if (kk.courseGroup != null)
                                {
                                    Plan.sortnumber = kk.courseGroup;
                                }
                                else Plan.sortnumber = 1;
                                AutoMapper.IMapper mapper = ServiceHelper.GetMapper();
                                var grade = ServiceHelper.GetTGradeInfo(nterm, userData.CompanyID, kk.sPlaneID, idlv2n, mapper, isRequestForCurrentAcademicYear); //_dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID == kk.sPlaneID && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                if (grade != null)
                                {
                                    var detail = ServiceHelper.GetTGradeDetailInfo(userData.CompanyID, grade.nGradeId, isRequestForCurrentAcademicYear, mapper); //_dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade.nGradeId).FirstOrDefault();
                                    if (detail != null && detail.Count() > 0)
                                        Plan.havedata = "1";
                                    if (grade.sortNumber != null)
                                        Plan.sortnumber = grade.sortNumber;
                                }

                                if (nCompany.settingTimePeriod == 1)
                                {
                                    DateTime now = DateTime.Now;
                                    var check = _db.TSettingExtraTimes.Where(w => w.SchoolID == schoolid && w.sPlaneID == kk.sPlaneID && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.sEMP == sEmpID && w.useToken == null && w.cDel != 1 && w.addDate.Day == now.Day && w.addDate.Month == now.Month).FirstOrDefault();
                                    if (check != null)
                                        Plan.extratime = "1";
                                }
                                else Plan.extratime = "";

                                PlanList.Add(Plan);


                            }
                        }
                    }

                    var newSortList = PlanList;

                    int counter = 1;
                    foreach (var a in newSortList)
                    {
                        a.number = counter;
                        counter = counter + 1;
                    }

                    return newSortList;
                }
            }

        }
        private void OpenData()
        {

            dgd.DataSource = returnlist();
            dgd.PageSize = 999;
            dgd.DataBind();
        }




        protected void CustomersGridView_DataBound(Object sender, EventArgs e)
        {

            // Retrieve the pager row.
            GridViewRow pagerRow = dgd.BottomPagerRow;
            if (pagerRow != null)
            {
                // Retrieve the DropDownList and Label controls from the row.
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
                DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
                Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

                int num = 20;
                int num2 = 50;
                int num3 = 100;
                ListItem item2 = new ListItem(num.ToString());
                pageList2.Items.Add(item2);
                if (num == dgd.PageSize)
                {
                    item2.Selected = true;
                }
                ListItem item3 = new ListItem(num2.ToString());
                pageList2.Items.Add(item3);
                if (num2 == dgd.PageSize)
                {
                    item3.Selected = true;
                }
                ListItem item4 = new ListItem(num3.ToString());
                pageList2.Items.Add(item4);
                if (num3 == dgd.PageSize)
                {
                    item4.Selected = true;
                }
                if (pageList != null)
                {

                    // Create the values for the DropDownList control based on 
                    // the  total number of pages required to display the data
                    // source.
                    for (int i = 0; i < dgd.PageCount; i++)
                    {

                        // Create a ListItem object to represent a page.
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        // If the ListItem object matches the currently selected
                        // page, flag the ListItem object as being selected. Because
                        // the DropDownList control is recreated each time the pager
                        // row gets created, this will persist the selected item in
                        // the DropDownList control.   
                        if (i == dgd.PageIndex)
                        {
                            item.Selected = true;
                        }

                        // Add the ListItem object to the Items collection of the 
                        // DropDownList.
                        pageList.Items.Add(item);

                    }

                }

                if (pageLabel != null)
                {

                    // Calculate the current page number.
                    int currentPage = dgd.PageIndex + 1;

                    // Update the Label control with the current page information.
                    pageLabel.Text = "หน้าที่ " + currentPage.ToString() +
                      " จากทั้งหมด " + dgd.PageCount.ToString() + " หน้า ";

                }
            }
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {


            dgd.DataSource = returnlist();
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }



        public void backbutton_Click(Object sender, EventArgs e)
        {


            dgd.DataSource = returnlist();
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.DataSource = returnlist();
            dgd.PageIndex = pageList.SelectedIndex;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            dgd.DataSource = returnlist();
            int xxx = Int32.Parse(pageList2.SelectedValue);
            dgd.PageSize = xxx;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }
        protected class PlanList
        {
            public int number { get; set; }
            public string planName { get; set; }
            public string planId { get; set; }
            public string code { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public string idlv2 { get; set; }
            public string term { get; set; }
            public string mode { get; set; }
            public string havedata { get; set; }
            public int? sortnumber { get; set; }
            public string extratime { get; set; }
            public string entity { get; set; }
            public string eid { get; set; }

            public int SchoolID { get; set; }
            public int PlanCourseId { get; set; }
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