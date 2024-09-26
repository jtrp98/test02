using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class GradeCheckList : System.Web.UI.Page
    {
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
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!IsPostBack)
            {
                OpenData();

                string yearr = Request.QueryString["year"];
                DropDownList1.SelectedValue = yearr;
                string termm = Request.QueryString["term"];
                DropDownList2.SelectedValue = termm;
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                
                List<TSubLevel> SubLevel = new List<TSubLevel>();
                TSubLevel sub = new TSubLevel();

                foreach (var a in _db.TSubLevels.Where(w => w.nWorkingStatus == 1 && w.SchoolID == userData.CompanyID))
                {
                    sub = new TSubLevel();
                    sub = a;
                    SubLevel.Add(sub);
                }

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
                foreach (var a in _db.TYears.Where(w => w.SchoolID == userData.CompanyID).ToList())
                {
                    ty = new TYear();
                    ty = a;
                    tylist.Add(ty);
                }
                var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                DropDownList1.DataSource = newList;
                DropDownList1.DataTextField = "numberYear";
                DropDownList1.DataValueField = "numberYear";
                DropDownList1.DataBind();
            }
        }


        protected List<PlanList> returnlist()
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            string idlv = Request.QueryString["idlv"];
            string idlv2 = Request.QueryString["idlv2"];
            string year = Request.QueryString["year"];
            string term = Request.QueryString["term"];
           
            PlanList Plan = new PlanList();
            List<PlanList> PlanList = new List<PlanList>();

            if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
            {
                int? idlv2n = Int32.Parse(idlv2);
                int? useryear = Int32.Parse(year);
                int nyear = 0;
                string nterm = "";
                int ntermtable = 0;
                List<string> planIdlist = new List<string>();

                foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
                {
                    nyear = ff.nYear;
                }

                foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                {
                    nterm = ee.nTerm;
                }

                foreach (var dd in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID))
                {
                    ntermtable = dd.nTermTable;
                    foreach (var hh in _db.TSchedules.Where(w => w.nTermTable == ntermtable && w.SchoolID == userData.CompanyID))
                    {
                        string planId = "";
                        if (hh.sPlaneID != null)
                        {
                            planId = hh.sPlaneID.ToString();
                            planIdlist.Add(planId);
                        }
                    }

                }

                List<string> unique = planIdlist.Distinct().ToList();


                int number = 1;

                foreach (var ii in unique)
                {
                     var kk = _db.
                        TPlanes.Where(w => w.sPlaneID.ToString() == ii).FirstOrDefault();
                    if (kk != null)
                    {
                        Plan = new PlanList();
                        Plan.number = number;
                        number = number + 1;
                        Plan.planName = kk.sPlaneName;
                        Plan.planId = kk.sPlaneID.ToString();
                        Plan.code = kk.courseCode;
                        Plan.year = year;
                        Plan.idlv = idlv;
                        Plan.idlv2 = idlv2;
                        Plan.term = term;                       
                        Plan.havedata = "0";
                        var grade = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID == kk.sPlaneID && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (grade != null)
                        {
                            var detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade.nGradeId).FirstOrDefault();
                            if (detail != null)
                                Plan.havedata = "1";
                        }

                        PlanList.Add(Plan);
                    }
                }
            }

            return PlanList;
        }
        private void OpenData()
        {
            dgd.DataSource = returnlist();
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
        //protected class PlanList
        //{
        //    public int number { get; set; }
        //    public string planName { get; set; }
        //    public string planId { get; set; }
        //    public string code { get; set; }
        //    public string year { get; set; }
        //    public string idlv { get; set; }
        //    public string idlv2 { get; set; }
        //    public string term { get; set; }            
        //    public string havedata { get; set; }
        //    public string status { get; set; }
        //    public string registerTeacher { get; set; }
        //}

        //public class ddlterm
        //{
        //    public string sTerm { get; set; }
        //}
    }
}