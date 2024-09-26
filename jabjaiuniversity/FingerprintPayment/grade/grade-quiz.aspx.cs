using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace FingerprintPayment
{
    public partial class grade_quiz : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {
                    OpenData();

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;

                    fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "- เลือกชั้นเรียน -", "nTSubLevel", "SubLevel");
                    int sEmpID = int.Parse(Session["sEmpID"] + "");

                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }
                    DropDownList1.DataSource = tylist;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();

                    ddlterm tm = new ddlterm();
                    List<ddlterm> tmlist = new List<ddlterm>();
                    List<string> tmc = new List<string>();

                    foreach (var b in _db.TTerms.ToList())
                    {
                        tmc.Add(b.sTerm);
                    }
                    List<string> tmc2 = tmc.Distinct().ToList<string>();
                    foreach (var c in tmc2)
                    {
                        tm = new ddlterm();
                        tm.sTerm = c;
                        tmlist.Add(tm);
                    }
                    DropDownList2.DataSource = tmlist;
                    DropDownList2.DataTextField = "sTerm";
                    DropDownList2.DataValueField = "sTerm";
                    DropDownList2.DataBind();
                }
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            OpenData();
        }

        protected List<quiz> returnlist()
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                string SQL = "";
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string ddlyear = Request.QueryString["year"];
                string ddlterm = Request.QueryString["term"];
                string sname = Request.QueryString["sname"];




                int sEmpID = int.Parse(Session["sEmpID"] + "");
                int nSchool = int.Parse(Session["nCompany"] + "");

            
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                { 

                    SQL = @"select TB1.*,SubLevel,TB2.nTSubLevel2
                    from TUser AS TB1 left join TTermSubLevel2 AS TB2 on TB1.nTermSubLevel2 = TB2.nTermSubLevel2
                    left join TSubLevel AS TB3 on TB3.nTSubLevel = TB2.nTSubLevel
                    where cDel IS NULL ";

                if (!string.IsNullOrEmpty(idlv2)) SQL += " AND TB1.nTermSubLevel2 = " + idlv2;
                else if (!string.IsNullOrEmpty(idlv)) SQL += " AND TB2.nTSubLevel = " + idlv;
                if (!string.IsNullOrEmpty(ddlyear)) SQL += " AND sName + ' ' + sLastname LIKE '%" + sname + "%'";

                string sid = "";

                var x = fcommon.Get_Data(_conn, SQL);
                foreach (DataRow row in x.Rows)
                {
                    sid = row["sId"].ToString();
                }


                    if (idlv == null && idlv2 == null && DropDownList1 == null && DropDownList2 == null)
                    {
                        return null;

                    }
                    else
                    {
                        int userid = Int32.Parse(sid);
                        if (ddlyear == null)
                            ddlyear = "99999999";
                        int? useryear = Int32.Parse(ddlyear);
                        int? ntermsublv2 = 0;
                        int nyear = 0;
                        string nterm = "";
                        int ntermtable = 0;
                        List<string> planIdlist = new List<string>();


                        foreach (var year in _db.TYears.Where(w => w.numberYear == useryear))
                        {
                            nyear = year.nYear;
                        }

                        foreach (var term in _db.TTerms.Where(w => w.sTerm == ddlterm && w.nYear == nyear && w.cDel == null))
                        {
                            nterm = term.nTerm;
                        }

                        foreach (var user in _db.TUser.Where(w => w.sID == userid))
                        {
                            ntermsublv2 = user.nTermSubLevel2;
                        }


                        foreach (var time in _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == ntermsublv2))
                        {
                            ntermtable = time.nTermTable;
                        }

                        foreach (var schedule in _db.TSchedules.Where(w => w.nTermTable == ntermtable))
                        {
                            string planId = "";
                            if (schedule.sPlaneID != null)
                            {
                                planId = schedule.sPlaneID.ToString();
                                planIdlist.Add(planId);
                            }
                        }

                        int countplan = 1;
                        List<string> unique = planIdlist.Distinct().ToList();

                        quiz quiz = new quiz();
                        List<quiz> quizlist = new List<quiz>();



                        foreach (var ii in unique)
                        {
                            int counttest = 0;

                            if (idlv2 != null)
                            {
                                int idlv2int = Int32.Parse(idlv2);

                                foreach (var data2 in _db.TQuizs.Where(w => w.sPlanid == ii && w.sTerm == nterm))
                                {
                                    var student = _db.TUser.Where(w => w.nTermSubLevel2 == idlv2int && w.sIdentification == data2.sID).FirstOrDefault();

                                    if (student != null && counttest < data2.nNumber)
                                    {
                                        counttest = data2.nNumber;
                                    }
                                }
                            }


                            foreach (var kk in _db.TPlanes.Where(w => w.sPlaneID.ToString() == ii))
                            {
                                quiz = new quiz
                                {
                                    planid = ii,
                                    idlv = Request.QueryString["idlv"],
                                    idlv2 = Request.QueryString["idlv2"],
                                    number = countplan,
                                    sPlanName = kk.sPlaneName,
                                    sPlanID = kk.sPlaneID.ToString(),
                                    year = ddlyear,
                                    term = ddlterm,
                                    totaltest = counttest
                                };
                                quizlist.Add(quiz);
                                countplan = countplan + 1;
                            }
                        }
                        return quizlist;
                    }
                }

            }
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
        //public class quiz
        //{
        //    public string planid { get; set; }
        //    public string idlv { get; set; }
        //    public string idlv2 { get; set; }
        //    public int number { get; set; }
        //    public string year { get; set; }
        //    public string term { get; set; }
        //    public string sPlanName { get; set; }
        //    public string sPlanID { get; set; }
        //    public int totaltest { get; set; }
        //}



        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }
}