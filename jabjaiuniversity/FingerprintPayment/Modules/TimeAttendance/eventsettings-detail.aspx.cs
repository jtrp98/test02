using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class eventsettings_detail : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEmpID"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            btnCancle.Click += new EventHandler(btnCancle_Click);
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            if (!IsPostBack)
            {
                Opendata();
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                //dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10);
                //dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10);
            }
        }

        void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("holidaysettings.aspx");
        }



        private void Opendata()
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string id = Request.QueryString["id"];
            var a1 = _db.THolidays.Where(w => w.nHoliday == id).FirstOrDefault();
            txt.Text = a1.sHoliday;
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

        protected List<holidayroom> returnlist(string name)
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            string id = Request.QueryString["id"];

            List<holidayroom> holidayroomList = new List<holidayroom>();
            holidayroom holidayroom = new holidayroom();

            int counter = 1;

            foreach (var a2 in _db.THolidaySomes.Where(w => w.nHoliday == id).ToList())
            {
                holidayroom = new holidayroom();
                holidayroom.number = counter;
                counter = counter + 1;
                holidayroom.nTSubLevel = a2.nTSubLevel;
                var room = _db.TSubLevels.Where(w => w.nTSubLevel == holidayroom.nTSubLevel).FirstOrDefault();
                holidayroom.room = room.SubLevel;
                holidayroomList.Add(holidayroom);
            }



            return holidayroomList;
        }



        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string name = "";


            dgd.DataSource = returnlist(name);
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            string name = "";


            dgd.DataSource = returnlist(name);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string name = "";

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.DataSource = returnlist(name);
            dgd.PageIndex = pageList.SelectedIndex;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {
            string name = "";

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            dgd.DataSource = returnlist(name);
            int xxx = Int32.Parse(pageList2.SelectedValue);
            dgd.PageSize = xxx;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        protected class holidayroom
        {
            public int number { get; set; }
            public int? nTSubLevel { get; set; }
            public string room { get; set; }
        }

        protected class holidaydetail
        {
            public int number { get; set; }
            public string type { get; set; }
            public string description { get; set; }
        }
    }
}