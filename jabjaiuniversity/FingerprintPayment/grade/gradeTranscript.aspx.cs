using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class gradeTranscript : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        private JWTToken.userData userData = new JWTToken.userData();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
      
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");


            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {
                    OpenData();

                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];


                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();

                    var item4 = new ListItem
                    {
                        Text = "ทั้งหมด",
                        Value = ""
                    };
                    ddlsublevel.Items.Add(item4);

                    foreach (var a in _db.TSubLevels.Where(w => w.nWorkingStatus == 1 && w.SchoolID == userData.CompanyID))
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

                    txtddl2.Text = idlv2;

                }
            }
        }

        private List<studentlist2> returnlist()
        {
            

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();



                List<studentlist2> unique = new List<studentlist2>();

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string name = Request.QueryString["name"];

                
                int? idlv2n = (idlv2 != "" && idlv2 != null) ? Int32.Parse(idlv2) : 0;
                   
              

                List<string> planIdlist = new List<string>();


                var provincelist = dbmaster.provinces.ToList();
                var titlelist = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();


                var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
                studentlist2 student = new studentlist2();

                var item3 = new ListItem
                {
                    Text = "",
                    Value = ""
                };
                ddlName.Items.Add(item3);
                List<ddlUser> userlist = new List<ddlUser>();
                ddlUser user = new ddlUser();
                unique = _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") != "1" && (w.nTermSubLevel2 == idlv2n || (idlv2n == 0)) )
                    .Select(a=> new studentlist2 { 
                        sName = a.sName + " " + a.sLastname,
                        sstudentid = a.sStudentID,
                        sort1txt = a.sStudentID,
                        stdYear = a.sStudentID,
                        stdId = a.sID.ToString(),
                        //idlv2 = a.nTermSubLevel2,
                        sID = a.sID,
                }).DistinctBy(d=> d.sID).ToList();
                if (unique != null)
                    unique = unique.OrderBy(o => o.sstudentid, new SemiNumericComparer()).ToList();
            //foreach (var a in students)
            //{
            //    //var TUserMaster = q_usermaster.Where(w => w.nSystemID == a.sID).FirstOrDefault();
            //    //if (TUserMaster != null)
            //    //{
            //        student = new studentlist2();


                    //        int n;
                    //        bool isNumeric = int.TryParse(a.sStudentID, out n);
                    //        if (isNumeric == true)
                    //            student.sort1int = n;
                    //        else student.sort1int = 99999;
                    //        unique.Add(student);

                    //        user = new ddlUser();
                    //        user.sName = a.sName;
                    //        user.sLast = a.sLastname;
                    //        userlist.Add(user);
                    //    //}

                    //}

                    //List<ddlUser> userlist2 = new List<ddlUser>();
                var userlist2 = unique.Select(s=> s.sName).Distinct().OrderBy(w => w).ToList();
                foreach (var sName in userlist2)
                {
                    var item2 = new ListItem
                    {
                        Text = sName,
                        Value = sName
                    };
                    ddlName.Items.Add(item2);
                }



                //var newSortList4 = unique;
                //newSortList4 = unique.DistinctBy(o => o.sstudentid).OrderBy(x => x.sort1int).ThenBy(x => x.sort1txt).ToList();

                int counter = 1;

                var newsort = unique;

               


                //if (idlv2 != "" && idlv2 != null)
                //{
                //    idlv2n = Int32.Parse(idlv2);
                //    newsort = newSortList4.Where(w => w.idlv2 == idlv2n).ToList();
                //}
                //else 
                if (name == "" && (idlv2 == "" || idlv2 == null))
                {
                    newsort = unique.Take(50).ToList();
                }


                if (name != "")
                    newsort = unique.Where(w => w.sName == name).ToList();

                foreach (var a in newsort)
                {
                    a.number = counter.ToString();
                    counter = counter + 1;
                }

                return newsort;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetStudentNamesByClassRoom(int nTSubLevel, int nTermSubLevel2)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var userlist = _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && (w.cDel ?? "0") != "1" && (w.nTSubLevel == nTSubLevel || nTSubLevel == 0) && (w.nTermSubLevel2 == nTermSubLevel2 || nTermSubLevel2 == 0) ).Select(s => new ddlUser { sName = s.sName + " " + s.sLastname }).Distinct().ToList();
                    return (userlist != null && userlist.Count > 0)? userlist.Distinct().OrderBy(w => w.sName).ToList() : new List<ddlUser>();
                }
            }
            catch (Exception ex)
            {
                return new List<ddlUser>();
            }
        }

        private void OpenData()
        {

            dgd.DataSource = returnlist();
            dgd.PageSize = 100;
            dgd.DataBind();
        }

        string oldgraduate(string index)
        {
            string txt = "";
            if (index == null)
                txt = "";
            else if (index == "1")
                txt = "ประถมศึกษาปีที่ 1";
            else if (index == "2")
                txt = "ประถมศึกษาปีที่ 2";
            else if (index == "3")
                txt = "ประถมศึกษาปีที่ 3";
            else if (index == "4")
                txt = "ประถมศึกษาปีที่ 4";
            else if (index == "5")
                txt = "ประถมศึกษาปีที่ 5";
            else if (index == "6")
                txt = "ประถมศึกษาปีที่ 6";
            else if (index == "7")
                txt = "มัธยมศึกษาปีที่ 3";
            else if (index == "8")
                txt = "มัธยมศึกษาปีที่ 6";
            return txt;
        }

        string txtprovince(string index, List<province> list)
        {
            string txt = "";
            
            if (index != null)
            {
                int n;
                bool isNumeric = int.TryParse(index, out n);
                if (isNumeric == true)
                {
                    int indexn = int.Parse(index);
                    var x = list.Where(w => w.PROVINCE_ID == indexn).FirstOrDefault();
                    txt = x.PROVINCE_NAME;
                }               
            }

            return txt;
        }
        string txttitle (string index, List<TTitleList> list)
        {
            string txt = index;
            int n;
            bool isNumeric = int.TryParse(index, out n);
            if (isNumeric == true)
            {
                var a = list.Where(w => w.nTitleid == n).FirstOrDefault();
                if (a != null)
                    txt = a.titleDescription;
            }
            return txt;
        }
        
        string monthtxt(int month)
        {
            string txt = "";
            if (month == 1)
                txt = "มกราคม";
            else if (month == 2)
                txt = "กุมภาพันธ์";
            else if (month == 3)
                txt = "มีนาคม";
            else if (month == 4)
                txt = "เมษายน";
            else if (month == 5)
                txt = "พฤษภาคม";
            else if (month == 6)
                txt = "มิถุนายน";
            else if (month == 7)
                txt = "กรกฎาคม";
            else if (month == 8)
                txt = "สิงหาคม";
            else if (month == 9)
                txt = "กันยายน";
            else if (month == 10)
                txt = "ตุลาคม";
            else if (month == 11)
                txt = "พฤศจิกายน";
            else if (month == 12)
                txt = "ธันวาคม";
            return txt;
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
        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public string sstudentid { get; set; }

            public string number { get; set; }
            public string year { get; set; }
            public int? idlv { get; set; }
            public int? idlv2 { get; set; }
            public string term { get; set; }
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }                       
            public string stdId { get; set; }
            
            public string stdYear { get; set; }
            public string stdTerm { get; set; }                    
            public DateTime? dBirth { get; set; }
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