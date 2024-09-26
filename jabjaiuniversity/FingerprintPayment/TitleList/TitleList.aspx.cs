using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Modules.TimeAttendance;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.TitleList
{
    public class TitleGateway : System.Web.UI.Page
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

    public partial class TitleList : TitleGateway
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
        void btnSave_Click(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                //string queryTitleID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nTitleid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TTitleList WHERE SchoolID = {0}", schoolID);
                //int newTitleID = _db.Database.SqlQuery<int>(queryTitleID).FirstOrDefault();
                //int titleID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newTitleID.ToString().PadLeft(6, '0')));

                TTitleList member = new TTitleList();
                //member.nTitleid = titleID;
                member.titleDescription = txt.Text;
                member.deleted = "0";
                member.workStatus = "working";
                member.nSchoolId = schoolID;
                member.SchoolID = schoolID;
                member.CreatedBy = UserData.UserID;
                member.CreatedDate = DateTime.Now;

                _db.TTitleLists.Add(member);

                _db.SaveChanges();
            }

            Response.Redirect("TitleList.aspx");

        }
        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TitleList.aspx");
        }
        void txtSearch_TextChanged(object sender, EventArgs e)
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

        protected List<titlelist> returnlist(string name)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                List<titlelist> TitleList = new List<titlelist>();
                titlelist Title = new titlelist();

                int counter = 1;

                foreach (var aa in _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && (w.titleDescription.Contains(name) || w.titleDescriptionEn.Contains(name))))
                {
                    Title = new titlelist();
                    Title.titleid = aa.nTitleid;
                    Title.description = aa.titleDescription;
                    Title.descriptionEn = aa.titleDescriptionEn;
                    Title.number = counter;
                    if (aa.workStatus == "notworking")
                    {
                        Title.status = "ไม่ใช้งาน";
                        Title.toggleOn = false;
                        Title.toggleOff = true;
                    }
                    else
                    {
                        Title.status = "ใช้งาน";
                        Title.toggleOn = true;
                        Title.toggleOff = false;
                    }

                    if (aa.workStatus == "working")
                        Title.workingStatus = 1;
                    else
                        Title.workingStatus = 2;
                    TitleList.Add(Title);
                    counter = counter + 1;
                }

                return TitleList;
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
        public static string StatusChange(int title_id)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var q = _db.TTitleLists.Find(title_id, schoolID);

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
        public static string updatedata(title title)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    if (title.nTitleid == 0)
                    {
                        //string queryTitleID = string.Format(@"SELECT ISNULL(MAX(CAST(REPLACE(CAST(nTitleid AS VARCHAR(10)), CAST(SchoolID AS VARCHAR(4)), '') AS INT)), 0) + 1 FROM TTitleList WHERE SchoolID = {0}", schoolID);
                        //int newTitleID = _db.Database.SqlQuery<int>(queryTitleID).FirstOrDefault();
                        //int titleID = Convert.ToInt32(string.Format(@"{0}{1}", schoolID.ToString().PadRight(3, '0'), newTitleID.ToString().PadLeft(6, '0')));

                        //title.nTitleid = _db.TTitleLists.Count() == 0 ? 1 : _db.TTitleLists.Max(m => m.nTitleid + 1);
                        //title.nTitleid = titleID;

                        _db.TTitleLists.Add(new TTitleList
                        {
                            //nTitleid = title.nTitleid,
                            titleDescription = title.titleDescription,
                            titleDescriptionEn = title.titleDescriptionEn,
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
                        var q = _db.TTitleLists.Find(title.nTitleid, schoolID);
                        q.titleDescription = title.titleDescription;
                        q.titleDescriptionEn = title.titleDescriptionEn;
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
        public static title getdata(int title_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = (from a in _db.TTitleLists.Where(w => w.SchoolID == schoolID).ToList()
                             where a.nTitleid == title_id
                             select new title
                             {
                                 nTitleid = a.nTitleid,
                                 titleDescription = a.titleDescription,
                                 titleDescriptionEn = a.titleDescriptionEn
                             }).FirstOrDefault();

                    return q;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string deletedata(int title_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var q = _db.TTitleLists.Find(title_id, schoolID);
                    q.deleted = "1";
                    q.cDel = true;
                    q.UpdatedBy = userData.UserID;
                    q.UpdatedDate = DateTime.Now;
                    _db.SaveChanges();
                    return "Success";
                }
            }
        }

        public class title
        {
            public int nTitleid { get; set; }
            public string titleDescription { get; set; }
            public string titleDescriptionEn { get; set; }
        }

        protected class titlelist
        {
            public int titleid { get; set; }
            public int number { get; set; }
            public string description { get; set; }
            public string descriptionEn { get; set; }
            public string status { get; set; }
            public int workingStatus { get; set; }
            public bool toggleOn { get; set; }
            public bool toggleOff { get; set; }
        }

        protected void dgd_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}