using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;
using TemplateBuilder;

namespace FingerprintPayment.grade
{
    public partial class BP7List : System.Web.UI.Page
    {
        protected List<SelectListItem> ReportTypeList { get; set; } = new List<SelectListItem>();

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

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (var context = new TemplateBuilderContext())
            {
                var forms = context.TblReportForm
                   .Where(o => ("," + o.sSchoolIDs + ",").Contains("," + userData.CompanyID + ",") && o.sFormType == "ปพ.7" && o.isShown == true)
                   .ToList();

                foreach (var f in forms)
                {
                    ReportTypeList.Add(new SelectListItem()
                    {
                        Text = f.sFormName,
                        Value = f.nFormID + "",
                    });
                }
            }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {
                    OpenData();

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;
                    string idlv = Request.QueryString["idlv"];
                    string idlv2 = Request.QueryString["idlv2"];

                    string mode = Request.QueryString["mode"];
                    modetxt.Text = mode;

                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();

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

                    txtddl1.Text = termm;
                    txtddl2.Text = idlv2;

                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.Where(w => w.SchoolID == userData.CompanyID).ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }
                    var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                    DropDownList1.DataSource = newList;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();


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

                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string year = Request.QueryString["year"];
                string term = Request.QueryString["term"];
                string mode = Request.QueryString["mode"];

                studentlist2 student = new studentlist2();
                List<studentlist2> studentlist2List = new List<studentlist2>();
                List<studentlist2> newSortList = new List<studentlist2>();

                if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
                {
                    int? idlv2n = Int32.Parse(idlv2);
                    int? useryear = Int32.Parse(year);
                    string username = "";
                    int? ntermsublv2 = 0;
                    int nyear = 0;
                    string nterm = "";
                    int ntermtable = 0;
                    List<string> planIdlist = new List<string>();


                    foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                    {
                        nterm = ee.nTerm;
                    }



                    int number = 1;


                    int sorttype1int = 0;
                    int sorttype1txt = 0;
                    int sorttype2 = 0;
                    var q_usermaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();



                    foreach (var data in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2n && w.nTerm == nterm && ((w.cDel ?? "0") != "1")))
                    {
                        var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
                        if (TUserMaster != null)
                        {
                            student = new studentlist2();

                            student.number = number.ToString();
                            student.sID = data.sID;
                            student.sstudentid = data.sStudentID;
                            student.year = year;
                            student.idlv = idlv;
                            student.idlv2 = idlv2;
                            student.term = term; 
                            student.nterm = nterm;
                            student.sName = data.sName + " " + data.sLastname;
                            int n;
                            bool isNumeric = int.TryParse(data.sStudentID, out n);
                            if (isNumeric == true)
                            {
                                sorttype1int = sorttype1int + 1;
                                student.sort1int = Int32.Parse(data.sStudentID);
                                student.sort1txt = data.sStudentID;
                                student.sort2 = 999999;
                            }
                            else if (data.sStudentID == null || data.sStudentID == "")
                            {
                                sorttype1int = sorttype1int + 1;
                                student.sort1int = 0;
                                student.sort1txt = "";
                                student.sort2 = 999999;
                            }
                            else
                            {
                                sorttype1txt = sorttype1txt + 1;
                                student.sort1txt = data.sStudentID;
                                student.sort2 = 999999;
                            }

                            if (data.nStudentNumber != null)
                            {
                                sorttype2 = sorttype2 + 1;
                                student.sort2 = data.nStudentNumber;
                            }

                            number = number + 1;
                            studentlist2List.Add(student);
                        }
                    }


                    newSortList = studentlist2List;

                    newSortList = studentlist2List.OrderBy(x => x.sort1int).ToList();

                    if (sorttype1txt != 0)
                        newSortList = studentlist2List.OrderBy(x => x.sort1txt).ToList();

                    if (sorttype2 != 0)
                        newSortList = studentlist2List.OrderBy(x => x.sort2).ToList();

                    int counter = 1;


                    foreach (var a in newSortList)
                    {
                        a.number = counter.ToString();
                        counter = counter + 1;
                    }
                }



                return newSortList;
            }
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
        class studentlist2
        {
            public int? sID { get; set; }
            public string sName { get; set; }
            public string note { get; set; }
            public string sstudentid { get; set; }

            public string number { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public string idlv2 { get; set; }
            public string term { get; set; }
            public string nterm { get; set; }
            public int? sort2 { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }

        }



        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }
}