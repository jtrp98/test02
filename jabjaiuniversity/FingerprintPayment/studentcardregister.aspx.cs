using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using FingerprintPayment.Class;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.studentCard
{
    public partial class studentcardregister : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

            if (!IsPostBack)
            {
                JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                OpenData();
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string name = Request.QueryString["name"];
                string year = Request.QueryString["year"];
                List<TSubLevel> SubLevel = new List<TSubLevel>();
                TSubLevel sub = new TSubLevel();
                foreach (var a in _db.TSubLevels.Where(w => w.nWorkingStatus == 1))
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
            }
        }

        private List<studentlist2> returnlist()
        {
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            List<studentlist2> unique = new List<studentlist2>();
            List<string> planIdlist = new List<string>();
            string idlv = Request.QueryString["idlv"];

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            var provincelist = dbmaster.provinces.ToList();
            var titlelist = _db.TTitleLists.ToList();

            var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
            studentlist2 student = new studentlist2();

            List<ddlUser> userlist = new List<ddlUser>();
            ddlUser user = new ddlUser();

            foreach (var a in _db.TUsers.Where(w => (w.cDel ?? "0") != "1"))
            {
                var TUserMaster = q_usermaster.Where(w => w.nSystemID == a.sID).FirstOrDefault();
                if (TUserMaster != null)
                {
                    student = new studentlist2();
                    student.sName = a.sName + " " + a.sLastname;
                    student.sstudentid = a.sStudentID;
                    student.sort1txt = a.sStudentID;
                    student.stdYear = a.sStudentID;
                    student.stdId = a.sID.ToString();
                    student.idlv2 = a.nTermSubLevel2;
                    student.idlv = idlv;
                    student.sID = a.sID;
                    student.NFC = TUserMaster.NFC;
                    int n;
                    bool isNumeric = int.TryParse(a.sStudentID, out n);
                    if (isNumeric == true)
                        student.sort1int = n;
                    else student.sort1int = 99999;
                    unique.Add(student);

                    user = new ddlUser();
                    user.sName = a.sName;
                    user.sLast = a.sLastname;
                    userlist.Add(user);
                }

            }

            var newSortList4 = unique;
            newSortList4 = unique.OrderBy(x => x.sort1int).ThenBy(x => x.sort1txt).ToList();

            int counter = 1;
            var newsort = newSortList4;

            string idlv2 = Request.QueryString["idlv2"];
            string name = Request.QueryString["name"];

            if (idlv2 != "" && idlv2 != null)
            {
                int? idlv2n = Int32.Parse(idlv2);
                newsort = newSortList4.Where(w => w.idlv2 == idlv2n).ToList();
            }

            if (name != "")
                newsort = newSortList4.Where(w => w.sName == name).ToList();

            foreach (var a in newsort)
            {
                a.number = counter.ToString();
                counter = counter + 1;
            }

            return newsort;
        }

        private void OpenData()
        {

            dgd.DataSource = returnlist();
            dgd.PageSize = 100;
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

        protected void AddStudentCard(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(hdnStdId.Value))
                {
                    JWTToken token = new JWTToken();
                    if (!token.CheckToken(HttpContext.Current)) { }
                    JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
                    string sEntities = Session["sEntities"].ToString();
                    var f_school = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);

                    int id = int.Parse(hdnStdId.Value);
                    int cardNumber;
                    bool isNumeric = int.TryParse(txtStudentCardNumber.Text, out cardNumber);
                    if (isNumeric)
                    {
                        var a = _dbMaster.TUsers.FirstOrDefault(w => w.nSystemID == id && w.cType == "0" && w.nCompany == f_school.nCompany);
                        a.NFC = FormatStudentCardNumber(txtStudentCardNumber.Text);
                        _dbMaster.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string FormatStudentCardNumber(String input)
        {
            input = String.Format("{0:x}", int.Parse(input));
            var result = string.Empty;
            while (input.Length > 0)
            {
                result += (input.Length > 0) ? input.Substring(((input.Length - 2) >= 0 ? (input.Length - 2) : 0), (input.Length < 2) ? input.Length : 2) : String.Empty;
                input = input.Substring(0, ((input.Length - 2) >= 0 ? (input.Length - 2) : 0));
            }
            return result;
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

        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public string sstudentid { get; set; }
            public string number { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public int? idlv2 { get; set; }
            public string term { get; set; }
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public string stdId { get; set; }
            public string stdYear { get; set; }
            public string stdTerm { get; set; }
            public DateTime? dBirth { get; set; }
            public string NFC { get; set; }
        }

        public class ddlterm
        {
            public string sTerm { get; set; }
        }

        public class ddlUser
        {
            public string sName { get; set; }
            public string sLast { get; set; }
        }
    }
}