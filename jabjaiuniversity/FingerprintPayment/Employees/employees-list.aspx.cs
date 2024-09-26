using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees
{
    [System.Web.Script.Services.ScriptService]
    [WebService(Namespace = "http://xmlforasp.net")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public partial class employees_list : System.Web.UI.Page
    {
        [System.Web.Services.WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod()]
        public static string delete(int employess_id)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var quser = dbmaster.TUsers.Where(w => w.nSystemID == employess_id && w.nCompany == tcompany.nCompany && w.cType == "1").ToList().FirstOrDefault();
                quser.cDel = "1";
                quser.dUpdate = DateTime.Now;

                dbmaster.SaveChanges();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    var _data = dbschool.TEmployees.Where(w => w.sEmp == employess_id).FirstOrDefault();
                    _data.cDel = "1";
                    _data.dUpdate = DateTime.Now;

                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", "ลบข้อมูลพนักงาน " + _data.sName + " " + _data.sLastname,
                        HttpContext.Current.Session["sEntities"].ToString(),
                        HttpContext.Current.Request, 13, 4, 0);
                    dbschool.SaveChanges();
                }
            }
            return "Success";
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static userdata getpassword(int user_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                var q = (from a in _dbMaster.TUsers.ToList()
                         where a.nSystemID == user_id && a.cType == "1" && a.nCompany == qcompany.nCompany
                         select new userdata
                         {
                             password = a.userpassword,
                             username = a.username
                         }).FirstOrDefault();
                return q;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string changepassword(Newpassword password)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                {
                    var q = _dbMaster.TUsers.FirstOrDefault(f => f.nSystemID == password.user_id && f.cType == "1" && f.nCompany == qcompany.nCompany);
                    if (password.oldpass == q.userpassword)
                    {
                        q.userpassword = password.newpass;
                        q.Token = null;
                        q.dUpdatePass = DateTime.Now;
                        _dbMaster.SaveChanges();
                        return "Success";
                    }
                    else
                    {
                        return "Fail";
                    }
                }
            }
        }

        public class Newpassword
        {
            public string oldpass { get; set; }
            public string newpass { get; set; }
            public int user_id { get; set; }
        }

        public class userdata
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            btnsearch.Click += new EventHandler(btnSearch_Click);
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);
            txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);
            dgd.RowCommand += Dgd_RowCommand;
            dgd.RowDeleting += Dgd_RowDeleting;
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                //dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10);
                //dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10);
            }
        }

        private void Dgd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        private void Dgd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    int ID = int.Parse(e.CommandArgument.ToString());
                    using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
                    {
                        string sEntities = Session["sEntities"].ToString();
                        var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        var quser = dbmaster.TUsers.Where(w => w.nSystemID == ID && w.nCompany == tcompany.nCompany && w.cType == "1").ToList().FirstOrDefault();
                        quser.cDel = "1";
                        quser.dUpdate = DateTime.Now;

                        dbmaster.SaveChanges();
                        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities)))
                        {
                            var _data = dbschool.TEmployees.Where(w => w.sEmp == ID).FirstOrDefault();
                            _data.cDel = "1";
                            _data.dUpdate = DateTime.Now;

                            database.InsertLog(Session["sEmpID"] + "", "ลบข้อมูลพนักงาน " + _data.sName + " " + _data.sLastname,
                                HttpContext.Current.Session["sEntities"].ToString(),
                                Request, 13, 4, 0);
                            dbschool.SaveChanges();
                        }
                        Opendata();
                    }
                    break;
                case "Edit":
                    Response.Redirect("/employees/employees-edit.aspx?id=" + e.CommandArgument.ToString());
                    break;
                case "Add":
                    Response.Redirect("/employees/employees-add.aspx");
                    break;
                default:
                    break;
            }
        }

        protected void dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[6].Text.ToLower() == "false")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        void txtSearch_TextChanged(object sender, EventArgs e)
        {
            Opendata();
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            Opendata();
        }

        private void Opendata()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sname = Request.QueryString["name"] == null ? "" : Request.QueryString["name"];
            string type = Request.QueryString["type"] == null ? "" : Request.QueryString["type"];
            dgd.DataSource = returnlist("", ddlType.SelectedValue);
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

        protected List<empList> returnlist(string name, string ddl)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

            int counter = 1;

            var tEmployees = _db.TEmployees.Where(w => w.cDel == null).ToList();
            var tMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "1" && w.cDel == null).ToList();
            //if (!string.IsNullOrEmpty(sName)) tEmployees = tEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(sName)).ToList();
            var qtimetable = _db.TTimetypes.ToList();

            int index = 1;
            var q = (from a in tEmployees
                     join master in tMaster on a.sEmp equals master.nSystemID.Value
                     join timetable in qtimetable on a.nTimeType equals timetable.nTimeType
                     select new empList
                     {
                         number = 1,
                         sName = master.sName,
                         sLastName = master.sLastname,
                         sIdentification = a.sIdentification,
                         sEmp = a.sEmp,
                         dBirth = a.dBirth.HasValue ? a.dBirth.Value.ToString("dd/MM/yyyy") : null,
                         fingerstatus = master.sFinger != null && master.sFinger2 != null || tCompany.sotfware == true,
                         cType = a.cType,
                         phone = a.sPhone,
                         timetype = timetable.sTimeType,
                         memberType = a.cType == "1" ? "พนักงาน" : "อาจารย์"
                     }).ToList();

            if (!string.IsNullOrEmpty(ddl)) q = q.Where(w => w.cType.Contains(ddl)).ToList();
            if (!string.IsNullOrEmpty(txtSearch.Text)) q = q.Where(w => (w.sName + " " + w.sLastName).Contains(txtSearch.Text)).ToList();

            q.ForEach(f => f.number = index++);
            return q;
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;

            dgd.DataSource = returnlist(name, ddlType.SelectedValue);
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

            dgd.DataSource = returnlist(name, ddlType.SelectedValue);
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
            dgd.DataSource = returnlist(name, ddlType.SelectedValue);
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
            dgd.DataSource = returnlist(name, ddlType.SelectedValue);
            int xxx = Int32.Parse(pageList2.SelectedValue);
            dgd.PageSize = xxx;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        protected class empList
        {
            public int number { get; set; }
            public string sName { get; set; }
            public string sLastName { get; set; }
            public string memberType { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public string dBirth { get; set; }
            public string sIdentification { get; set; }
            public string finger2 { get; set; }
            public string finger { get; set; }
            public int sEmp { get; set; }
            public bool fingerstatus { get; set; }
            public string cType { get; internal set; }
            public string timetype { get; set; }
        }

        protected void dgd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

    }
}