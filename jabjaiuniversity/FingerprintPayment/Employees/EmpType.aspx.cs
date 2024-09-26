using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Amazon.Runtime.Internal.Util;
using FingerprintPayment.Employees.CsCode;
using FingerprintPayment.Modules.TimeAttendance;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Employees
{

    public partial class EmpType : EmployeeGateway
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);
            Button1.Click += new EventHandler(Button1_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            btnCancle.Click += new EventHandler(btnCancle_Click);
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                //dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10);
                //dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10);

                //AddOldData();
            }
        }

        private void AddOldData()
        {
            int schoolID = UserData.CompanyID;
            var tcompany = new List<TCompany>();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                tcompany = dbmaster.TCompanies
                    .Where(o => o.cDel == false)
                    .ToList();
            }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                foreach (var company in tcompany)
                {
                    //<option value="1">บุคลากรทั่วไป</option>
                    //   <option value="2">ครูประจำการ</option>
                    //   <option value="3">บุคลากรทางการศึกษา</option>
                    //   <option value="4">ครูพิเศษ</option>
                    //   <option value="5">ครูพี่เลี้ยง</option>
                    //   <option value="6">ผู้บริหาร</option>
                    var arr = new List<string>() {
                        "บุคลากรทั่วไป",
                        "ครูประจำการ",
                        "บุคลากรทางการศึกษา",
                        "ครูพิเศษ",
                        "ครูพี่เลี้ยง",
                        "ผู้บริหาร",
                    };

                    for (int i = 1; i <= arr.Count; i++)
                    {
                        _db.TEmployeeTypes.Add(new TEmployeeType()
                        {
                            nTypeId2 = i,
                            Title = arr[i - 1],
                            SchoolID = company.nCompany,
                            IsDel = false,
                            IsActive = true,               
                            CreatedDate = DateTime.Now,
                            UpdatedDate = DateTime.Now,
                        });
                    }
                }

                _db.SaveChanges();
            }
        }

        protected void dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")) == true)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.PaleGreen;
                }
                if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsActive")) == false)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.HotPink;
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            

            int schoolID = UserData.CompanyID;

            
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {



                //string queryTitleID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nTitleid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TTitleList WHERE SchoolID = {0}", schoolID);
                //int newTitleID = _db.Database.SqlQuery<int>(queryTitleID).FirstOrDefault();
                //int titleID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newTitleID.ToString().PadLeft(6, '0')));

                var etype = new TEmployeeType();
                //member.nTitleid = titleID;
                etype.Title = txt.Text;
                etype.IsActive = true;
                etype.SchoolID = schoolID;
                etype.CreatedBy = UserData.UserID;
                etype.CreatedDate = DateTime.Now;
                etype.UpdatedBy = UserData.UserID;
                etype.UpdatedDate = DateTime.Now;

                _db.TEmployeeTypes.Add(etype);

                _db.SaveChanges();

            }

            Response.Redirect(".aspx");
        }
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpType.aspx");
        }
        //void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    dgd.DataSource = returnlist("");
        //    dgd.PageSize = 999;
        //    dgd.DataBind();
        //}


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

        protected List<EmpTypeList> returnlist(string name)
        {
           

            int schoolID = UserData.CompanyID;

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var q = _db.TEmployeeTypes.Where(o => o.SchoolID == schoolID && o.IsDel == false);

                if (!string.IsNullOrEmpty(name))
                    q = q.Where(o => o.Title.Contains(name));

                var d = q
                    .AsEnumerable()
                    .Select((o, i) => new EmpTypeList
                    {
                        index = i + 1,
                        title = o.Title,
                        id = o.nTypeId,
                        IsActive = o.IsActive,
                        IsDel = o.nTypeId2 != null ? false : true,
                    })
                    .ToList();

                return d;
            }
        }

        public void Button1_Click(Object sender, EventArgs e)
        {
            string name = "";
            dgd.DataSource = returnlist(txtSearch.Text);
            dgd.DataBind();
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
        public static bool StatusChange(int title_id)
        {


            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var q = _db.TEmployeeTypes.Find(title_id, schoolID);

                q.IsActive = !q.IsActive;
                //if (q.IsActive == "notworking")
                //    q.workStatus = "working";
                //else
                //    q.workStatus = "notworking";

                q.UpdatedBy = userData.UserID;
                q.UpdatedDate = DateTime.Now;

                _db.SaveChanges();

                return q.IsActive;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string updatedata(FilterData filter)
        {

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                if (filter.id == 0)
                {
                    _db.TEmployeeTypes.Add(new TEmployeeType
                    {
                        //nTitleid = title.nTitleid,
                        Title = filter.title,
                        IsDel = false,
                        IsActive = true,
                        SchoolID = schoolID,
                        CreatedBy = userData.UserID,
                        CreatedDate = DateTime.Now,
                        UpdatedBy = userData.UserID,
                        UpdatedDate = DateTime.Now,
                    });
                }
                else
                {
                    var q = _db.TEmployeeTypes.Find(filter.id, schoolID);
                    q.Title = filter.title;
                    q.UpdatedBy = userData.UserID;
                    q.UpdatedDate = DateTime.Now;
                }

                _db.SaveChanges();
            }
            return "Success";

        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static FilterData getdata(int id)
        {

            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var q = (from a in _db.TEmployeeTypes.Where(w => w.SchoolID == schoolID && w.nTypeId == id)
                             //  where a.nTitleid == id
                         select new FilterData
                         {
                             id = a.nTypeId,
                             title = a.Title
                         }).FirstOrDefault();

                return q;
            }

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string deletedata(int id)
        {

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                var q = _db.TEmployeeTypes.Find(id, schoolID);
                q.IsDel = true;
                q.IsActive = false;
                q.UpdatedBy = userData.UserID;
                q.UpdatedDate = DateTime.Now;
                _db.SaveChanges();
                return "success";
            }

        }

        public class FilterData
        {
            public int id { get; set; }
            public string title { get; set; }
        }

        protected class EmpTypeList
        {

            public int index { get; set; }
            public int id { get; set; }
            public string title { get; set; }
            public bool IsActive { get; set; }

            public bool IsDel { get; set; }
        }
    }
}