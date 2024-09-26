using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace FingerprintPayment.Setting
{
    public partial class settingGradeAdmin : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                int sEmpID = int.Parse(Session["sEmpID"] + "");



                deleteBtn.Click += new EventHandler(deleteBtn_Click);
                btnSave.Click += new EventHandler(btnSave_Click);
                Button1.Click += new EventHandler(Button1_Click);
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                { 

                    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                    if (!IsPostBack)
                    {
                        var item3 = new ListItem
                        {
                            Text = "",
                            Value = ""
                        };

                        teacherRegis1.Items.Add(item3);


                        foreach (var data2 in _db.TEmployees.Where(w => w.SchoolID == schoolid &&  w.cDel == null))
                        {
                            var item2 = new ListItem
                            {
                                Text = data2.sName + " " + data2.sLastname,
                                Value = data2.sEmp.ToString()
                            };


                            teacherRegis1.Items.Add(item2);

                        }

                        OpenData();


                    }
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    int idn = int.Parse(teacherRegis1.SelectedValue);
                    var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid &&  w.sEmp == idn).FirstOrDefault();

                    data1.gradeSystemAdmin = 1;
                    data1.SchoolID = schoolid;
                    
                    _db.SaveChanges();
                    Response.Redirect("settingGradeAdmin.aspx");
                }
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            
            Response.Redirect("settingMainPage.aspx");
        }

        void deleteBtn_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    int idn = int.Parse(deleteID.Text);
                    var data1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == idn).FirstOrDefault();

                    data1.gradeSystemAdmin = null;
                    data1.SchoolID = schoolid;

                    _db.SaveChanges();
                    Response.Redirect("settingGradeAdmin.aspx");
                }
            }
        }
        
        protected List<gradeAdmin> returnlist()
        {
            string sEntities = Session["sEntities"] + "";
            gradeAdmin gradeAdmin = new gradeAdmin();
            List<gradeAdmin> gradeAdminList = new List<gradeAdmin>();
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                   

                    int number = 1;
                    foreach (var data in _db.TEmployees.Where(w => w.SchoolID == schoolid &&  w.gradeSystemAdmin == 1 && w.cDel == null))
                    {
                        gradeAdmin = new gradeAdmin();
                        gradeAdmin.SchoolID = data.SchoolID;
                        gradeAdmin.sEMP = data.sEmp;
                        gradeAdmin.name = data.sName + " " + data.sLastname;
                        gradeAdmin.number = number;
                        number = number + 1;
                        gradeAdminList.Add(gradeAdmin);
                    }
                }
            }

            return gradeAdminList;

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
        protected class gradeAdmin
        {
            public int number { get; set; }
            public string name { get; set; }
            public int sEMP { get; set; }
            public int SchoolID { get; set; }

        }


    }
}