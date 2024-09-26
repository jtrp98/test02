using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class leaveSetting_list : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            Button1.Click += new EventHandler(Button1_Click);
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("leaveSetting.aspx");
        }


        private void Opendata()
        {
            
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sname = Request.QueryString["name"] == null ? "" : Request.QueryString["name"];
            string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"];
            dgd.DataSource = returnlist("", 0);
            dgd.PageSize = 30;
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

        protected List<checkerList> returnlist(string name, int ddl)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();


            int counter = 1;
            List<checkerList> empList = new List<checkerList>();
            checkerList emp = new checkerList();

            var tEmployees = _db.TEmployees.Where(w => w.cDel == null).ToList();
            var tMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "1" && w.cDel == null).ToList();

            var q = (from a in tEmployees
                     join master in tMaster on a.sEmp equals master.nSystemID.Value
                     where a.leavecheck == 0||a.leavecheck==null
                     select new checkerList
                     {
                         number = counter++,
                         sName = a.sName,
                         sLast = a.sLastname,
                         sEmp = a.sEmp,
                     }).ToList();

            return q;
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            
            int ddlsub = 0;

            dgd.DataSource = returnlist(name, ddlsub);
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }



        public void backbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            
            int ddlsub = 0;

            dgd.DataSource = returnlist(name, ddlsub);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string name = "";
            
            int ddlsub = 0;
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.DataSource = returnlist(name, ddlsub);
            dgd.PageIndex = pageList.SelectedIndex;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {
            string name = "";
            
            int ddlsub = 0;
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            dgd.DataSource = returnlist(name, ddlsub);
            int xxx = Int32.Parse(pageList2.SelectedValue);
            dgd.PageSize = xxx;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        protected class checkerList
        {
            public int number { get; set; }
            public string sName { get; set; }
            public string sLast { get; set; }
            public int sEmp { get; set; }
        }

    }
}