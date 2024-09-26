using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public class JobGateway : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }

    public partial class Joblist : JobGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);
            Button1.Click += new EventHandler(Button1_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            //btnCancle.Click += new EventHandler(btnCancle_Click);
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");

                //dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10);
                //dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10);
            }
        }
        protected void dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "workingStatus")) == 1)
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.PaleGreen;
            }
            if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "workingStatus")) == 2)
            {
                e.Row.Cells[4].BackColor = System.Drawing.Color.HotPink;
            }
        }

        //void btnSave_Click(object sender, EventArgs e)
        //{
        //    JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
        //    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) { 


        //    string sEntities = Session["sEntities"] + "";

        //    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

        //    int count = 1;
        //    foreach (var aa in _db.TJobLists.ToList())
        //    {
        //        if (aa.nJobid > count || aa.nJobid == count)
        //        {
        //            count = aa.nJobid + 1;
        //        }

        //    }

        //    TJobList member = new TJobList();
        //    member.nJobid = count;
        //    member.jobDescription = txt.Text;
        //    member.deleted = "0";
        //    member.workStatus = "working";
        //    member.nSchoolId = nCompany.nCompany;
        //    member.empType = UserType.SelectedValue;

        //    _db.TJobLists.Add(member);


        //    _db.SaveChanges();
        //    Response.Redirect("Joblist.aspx");
        //}
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Joblist.aspx");
        }
        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgd.DataSource = returnlist("");
            dgd.DataBind();
        }

        private void Opendata()
        {
            dgd.DataSource = returnlist("");
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

        protected List<joblist> returnlist(string name)
        {
           

            int schoolID = UserData.CompanyID;

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                ;

                List<joblist> joblist = new List<joblist>();
                joblist job = new joblist();

                int counter = 1;

                foreach (var aa in _db.TJobLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.jobDescription.Contains(name)))
                {
                    job = new joblist();
                    if (aa.empType == "1")
                        job.emptype = "พนักงาน";
                    if (aa.empType == "2")
                        job.emptype = "อาจารย์";
                    job.jobid = aa.nJobid;
                    job.description = aa.jobDescription;
                    job.number = counter;
                    if (aa.workStatus == "notworking")
                    {
                        job.status = "ไม่ใช้งาน";
                        job.toggleOn = false;
                        job.toggleOff = true;
                    }

                    else
                    {
                        job.status = "ใช้งาน";
                        job.toggleOn = true;
                        job.toggleOff = false;
                    }

                    if (aa.workStatus == "working")
                        job.workingStatus = 1;
                    else job.workingStatus = 2;
                    joblist.Add(job);
                    counter = counter + 1;
                }

                return joblist;
            }
        }

        public void Button1_Click(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
            {
                name = txtSearch.Text;
                dgd.DataSource = returnlist(name);
                dgd.DataBind();
            }

            else
            {
                dgd.DataSource = returnlist(name);
                dgd.DataBind();
            }
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;

            dgd.DataSource = returnlist(name);
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;

            dgd.DataSource = returnlist(name);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.DataSource = returnlist(name);
            dgd.PageIndex = pageList.SelectedIndex;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            dgd.DataSource = returnlist(name);
            int xxx = Int32.Parse(pageList2.SelectedValue);
            dgd.PageSize = xxx;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string StatusChange(int job_id)
        {

           

            var userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var q = _db.TJobLists.Find(job_id, schoolID);

                if (q.workStatus == "notworking")
                    q.workStatus = "working";
                else
                    q.workStatus = "notworking";

                q.UpdatedBy = userData.UserID;
                q.UpdatedDate = DateTime.Now;

                _db.SaveChanges();

                return q.workStatus;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(job job)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (job.nJobid == 0)
                    {
                        //string queryJobID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nJobid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TJobList WHERE SchoolID = {0}", schoolID);
                        //int newJobID = _db.Database.SqlQuery<int>(queryJobID).FirstOrDefault();
                        //int jobID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newJobID.ToString().PadLeft(6, '0')));

                        //job.nJobid = _db.TJobLists.Count() == 0 ? 1 : (_db.TJobLists.Max(m => m.nJobid) + 1);
                        //job.nJobid = jobID;

                        _db.TJobLists.Add(new TJobList
                        {
                            //nJobid = job.nJobid,
                            jobDescription = job.jobDescription,
                            empType = job.empType,
                            deleted = "0",
                            workStatus = "working",
                            nSchoolId = schoolID,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        var q = _db.TJobLists.Find(job.nJobid, schoolID);
                        q.jobDescription = job.jobDescription;
                        q.empType = job.empType;
                        q.UpdatedBy = userData.UserID;
                        q.UpdatedDate = DateTime.Now;
                    }

                    _db.SaveChanges();
                }
                return "Success";
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static job getdata(int job_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = (from a in _db.TJobLists.ToList()
                             where a.nJobid == job_id
                             select new job
                             {
                                 nJobid = a.nJobid,
                                 jobDescription = a.jobDescription,
                                 empType = a.empType
                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        public class job
        {
            public int nJobid { get; set; }
            public string jobDescription { get; set; }
            public string empType { get; set; }
        }

        protected class joblist
        {
            public int jobid { get; set; }
            public int number { get; set; }
            public string description { get; set; }
            public string status { get; set; }
            public int workingStatus { get; set; }
            public string emptype { get; set; }
            public bool toggleOn { get; set; }
            public bool toggleOff { get; set; }

        }
    }
}