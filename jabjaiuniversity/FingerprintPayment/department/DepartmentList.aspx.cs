using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.department
{
    public class DepartmentGateway : System.Web.UI.Page
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

    public partial class DepartmentList : DepartmentGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += new EventHandler(Button1_Click);
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgd.DataSource = returnlist("");
            dgd.PageSize = 999;
            dgd.DataBind();
        }


        private void Opendata()
        {
            dgd.DataSource = returnlist("");
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

        protected List<departmentlist> returnlist(string name)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID, ConnectionDB.Read)))
            {

                List<departmentlist> departmentList = new List<departmentlist>();
                departmentlist department = new departmentlist();

                int counter = 1;
                var q = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList();

                foreach (var aa in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID && w.deleted != 1 && w.departmentName.Contains(name)))
                {
                    department = new departmentlist();
                    department.number = counter;
                    department.departmentId = aa.DepID;
                    department.departmentName = aa.departmentName;
                    var headname = q.Where(w => w.sEmp == aa.userHeadId).FirstOrDefault();
                    if (headname != null)
                    {
                        department.departmentHeadName = headname.sName + " " + headname.sLastname;
                    }
                    departmentList.Add(department);
                    counter = counter + 1;
                }

                return departmentList;
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
        public static Department_data getdata(int department_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = (from a in _db.TDepartments.Where(w => w.SchoolID == schoolID).ToList()
                             join b in _db.TEmployees.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList() on a.userHeadId equals b.sEmp into jab
                             from ab in jab.DefaultIfEmpty()
                             where a.DepID == department_id
                             select new Department_data
                             {
                                 department_id = a.DepID,
                                 department_name = a.departmentName,
                                 department_head_id = a.userHeadId,
                                 department_head_name = ab != null ? ab.sName + " " + ab.sLastname : ""
                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(Department_data department_data)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (department_data.department_id == 0)
                    {
                        //int department_id = dbschool.TDepartments.Count() == 0 ? 1 : dbschool.TDepartments.Max(max => max.departmentId + 1);
                        dbschool.TDepartments.Add(new TDepartment
                        {
                            //departmentId = department_id,
                            userHeadId = department_data.department_head_id,
                            departmentName = department_data.department_name,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        });
                    }
                    else
                    {
                        var q = dbschool.TDepartments.Find(department_data.department_id, schoolID);
                        q.departmentName = department_data.department_name;
                        q.userHeadId = department_data.department_head_id;
                        q.UpdatedBy = userData.UserID;
                        q.UpdatedDate = DateTime.Now;
                    }
                    dbschool.SaveChanges();
                }
                return "Success";
            }
        }

        public class Department_data
        {
            public string department_name { get; set; }
            public int department_id { get; set; }

            public string department_head_name { get; set; }
            public int? department_head_id { get; set; }
        }

        protected class departmentlist
        {

            public int number { get; set; }
            public int departmentId { get; set; }

            public string departmentName { get; set; }
            public string departmentHeadName { get; set; }

        }
    }
}