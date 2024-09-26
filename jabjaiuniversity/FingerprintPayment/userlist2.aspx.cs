using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;
using FingerprintPayment.Class;
using JabjaiNoSQL;
using JabjaiNoSQL.Behavior;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class userlist2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            btnSearch.Click += new EventHandler(btnSearch_Click);
            //ddlSubLV2.Enabled = false;
            //ddlSubLV2.Items.Insert(0, new ListItem("เลือกห้อง", "0"));
            dgd.RowDataBound += Dgd_RowDataBound;
            dgd.RowCommand += Dgd_RowCommand;
            dgd.RowDeleting += Dgd_RowDeleting;

            if (!IsPostBack)
            {
                ViewState["pagesize"] = 100;
                var q = QueryDataBases.SubLevel_Query.GetData(_db);
                var q_1 = _db.TYears.OrderByDescending(o => o.numberYear).ToList();
                fcommon.LinqToDropDownList(q, ddlSubLV, "ทั้งหมด", "class_id", "class_name");
                fcommon.LinqToDropDownList(q_1, ddlYear, "ทั้งหมด", "nYear", "numberYear");

                var nTerm = new TTerm();

                if (string.IsNullOrEmpty(Request.QueryString["termId"]))
                {
                    nTerm = _db.TTerms.FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today);
                    if (nTerm == null) nTerm = _db.TTerms.OrderBy(o => o.dEnd).FirstOrDefault(f => f.dStart >= DateTime.Today);
                    if (nTerm == null) nTerm = _db.TTerms.OrderByDescending(o => o.dEnd).FirstOrDefault(f => f.dEnd <= DateTime.Today);

                    var q_2 = _db.TTerms.Where(w => w.nYear == nTerm.nYear).ToList();
                    fcommon.LinqToDropDownList(q_2, ddlTerm, "", "nTerm", "sTerm");
                }
                else
                {
                    string termId = Request.QueryString["termId"];
                    if (termId.Length < 5) termId = string.Format("TS{0:0000000}", int.Parse(termId));
                    nTerm = _db.TTerms.FirstOrDefault(f => f.nTerm.Trim() == termId);
                    var q_2 = _db.TTerms.Where(w => w.nYear == nTerm.nYear).ToList();
                    fcommon.LinqToDropDownList(q_2, ddlTerm, "", "nTerm", "sTerm");
                }

                ddlTerm.SelectedValue = nTerm.nTerm;
                ddlYear.SelectedValue = nTerm.nYear.Value.ToString();

                ViewState["termId"] = nTerm.nTerm;
                OpenData(nTerm.nTerm);

                int sEmpID = int.Parse(Session["sEmpID"] + "");
                //Response.Write(Role);
                //dgd.Columns[5].Visible = mp.Permission_Page.permission == "0";
                //dgd.Columns[4].Visible = mp.Permission_Page.permission == "0";
                int sEmp = int.Parse(Session["sEmpID"] + "");
                string sClaimReport = _db.TEmployees.Where(w => w.sEmp == sEmp).SingleOrDefault().sStatusReport;
                //if (sClaimReport.Length > 5 && sClaimReport.Substring(5, 1) == "1") ltrMenu.Text = "<a href='report07.aspx' class='btn btn-primary'>รายงานรายชื่อนักเรียน</a>";
            }
        }

        private void Dgd_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        private void Dgd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ID = 0;
            switch (e.CommandName)
            {
                case "Delete":
                    ID = int.Parse(e.CommandArgument.ToString());
                    JabJaiMasterEntities dbmaster = Connection.MasterEntities();
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                    var tcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var qmaster = dbmaster.TUsers.Where(w => w.nSystemID == ID && w.nCompany == tcompany.nCompany && w.cType == "0").FirstOrDefault();
                    qmaster.cDel = "1";
                    qmaster.dUpdate = DateTime.Now;
                    dbmaster.SaveChanges();
                    JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
                    var quser = dbschool.TUsers.Find(ID);
                    quser.cDel = "1";
                    quser.dPicUpdate = DateTime.Now;
                    database.InsertLog(Session["sEmpID"] + "", "ลบข้อมูลนักเรียน " + quser.sName + " " + quser.sLastname,
                        HttpContext.Current.Session["sEntities"].ToString(), Request, 14, 4, 0);
                    dbschool.SaveChanges();
                    string termId = Request.QueryString["termId"];
                    if (termId.Length < 5) termId = string.Format("TS{0:000000}", int.Parse(termId));
                    if (string.IsNullOrEmpty(termId)) termId = (string)ViewState["termId"];
                    OpenData(termId);
                    break;
                case "Edit":
                    ID = int.Parse(e.CommandArgument.ToString());
                    Response.Redirect("/userlist2-edit.aspx?id=" + ID);
                    break;
                case "Add":
                    Response.Redirect("/userlist2-register.aspx");
                    break;
            }
        }

        private void Dgd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text.ToLower() == "false")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;//#ff7979
                    e.Row.ForeColor = System.Drawing.Color.White;
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
            }
        }

        protected void ddlSubLV_Change(object sender, EventArgs e)
        {
            //ddlSubLV2.Items.Clear();
            //int stateId = int.Parse(ddlSubLV.SelectedItem.Value);
            //if (stateId > 0)
            //{
            //    BindDropDownList(stateId);
            //    ddlSubLV2.Enabled = true;
            //}
        }

        private void BindDropDownList(int value)
        {
            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            //var _listslv2 = _db.TTermSubLevel2;
            //ddlSubLV2.Items.Add(new ListItem("ทั้งหมด", ""));
            //foreach (var DataLV2 in _listslv2.Where(w => w.nTSubLevel == value))
            //{
            //    ddlSubLV2.Items.Add(new ListItem(DataLV2.nTSubLevel2.ToString(), DataLV2.nTermSubLevel2.ToString()));
            //}
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            string termId = Request.QueryString["termId"];
            if (termId.Length < 5) termId = string.Format("TS{0:000000}", int.Parse(termId));
            OpenData(termId);
        }

        void dgd_ItemCommand(object source, GridViewCommandEventArgs e)
        {

        }
        private void OpenData(string termId)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                List<StudentList> StudentList = new List<StudentList>();
                StudentList std = new StudentList();
                string sEntities = Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var TUserMaster = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
                using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString())))
                {

                    var tUser = (from a in db.TB_StudentViews
                                 where a.nTerm.Trim() == termId.Trim()
                                 select a).ToList();

                    var tLevel2 = db.TTermSubLevel2.ToList();
                    var tLevel = db.TSubLevels.ToList();
                    var timetype = db.TTimetypes.ToList();

                    int rowindex = 1;
                    var data = (from a in tUser
                                join b in tLevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                                join c in tLevel on b.nTSubLevel equals c.nTSubLevel
                                join master in TUserMaster on a.sID equals master.nSystemID.Value
                                join d in timetype on b.nTimeType equals d.nTimeType into dd
                                from jd in dd.DefaultIfEmpty()

                                where (a.cDel ?? "0").Trim() != "1"
                                select new StudentList
                                {
                                    sName = string.IsNullOrEmpty(a.sName) ? "" : a.sName,
                                    sLastName = string.IsNullOrEmpty(a.sLastname) ? "" : a.sLastname,
                                    nTSubLevel2 = a.nTermSubLevel2,
                                    sIdentification = string.IsNullOrEmpty(a.sStudentID) ? "" : a.sStudentID.Trim(),
                                    sId = a.sID,
                                    fingerStatus = master.sFinger != null && master.sFinger2 != null || tCompany.sotfware == true,
                                    birthday = master.dBirth.Value.ToString("dd/MM/yyyy"),
                                    email = master.sEmail,
                                    phone = master.sPhone,
                                    nTermSubLevel2 = a.nTermSubLevel2,
                                    nTSubLevel = c.nTSubLevel,
                                    timetype = jd == null ? "" : jd.sTimeType,
                                    classroom = c.SubLevel.Trim() + " / " + b.nTSubLevel2,
                                    status = student_status(a.nStudentStatus)
                                }).ToList();

                    string ddlSubLV2 = Request.QueryString["idlv2"];
                    string sname = Request.QueryString["sname"];
                    int idlv = int.Parse(string.IsNullOrEmpty(Request.QueryString["idlv"]) ? "0" : Request.QueryString["idlv"].ToString());
                    int idlv2 = int.Parse(string.IsNullOrEmpty(ddlSubLV2) ? "0" : ddlSubLV2);

                    if (idlv2 > 0) data = data.Where(w => w.nTermSubLevel2 == idlv2).ToList();
                    else if (idlv > 0) data = data.Where(w => w.nTSubLevel == idlv).ToList();
                    if (!string.IsNullOrEmpty(sname)) data = data.Where(w => (w.sName + " " + w.sLastName).Contains(sname) || w.sIdentification.Contains(sname)).ToList();

                    var q = new List<StudentList>();
                    q = (from a in data
                         orderby a.sIdentification
                         select new StudentList
                         {
                             number = rowindex++,
                             sName = a.sName,
                             sLastName = a.sLastName,
                             nTSubLevel2 = a.nTermSubLevel2,
                             sIdentification = a.sIdentification,
                             sId = a.sId,
                             fingerStatus = a.fingerStatus,
                             birthday = a.birthday,
                             email = a.email,
                             phone = a.phone,
                             nTermSubLevel2 = a.nTermSubLevel2,
                             nTSubLevel = a.nTSubLevel,
                             timetype = a.timetype,
                             classroom = a.classroom,
                             status = a.status
                         }).ToList();

                    dgd.DataSource = q;
                    dgd.PageSize = int.Parse(ViewState["pagesize"].ToString());
                    dgd.PagerSettings.Visible = true;
                    dgd.DataBind();
                }
            }
        }

        private string student_status(int? nStudentStatus)
        {
            switch (nStudentStatus)
            {
                case 0: return "กำลังศึกษา";
                case 1: return "จำหน่าย";
                case 2: return "ลาออก";
                case 3: return "พักการเรียน";
                case 4: return "สำเร็จการศึกษา";
                case 5: return "ขาดการติดต่อ";
                case 6: return "พ้นสภาพ";
                default: return "กำลังศึกษา";
            }
        }

        protected List<StudentList> returnlist(string name, int ddl)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));

            int counter = 1;
            List<StudentList> StudentList = new List<StudentList>();
            StudentList std = new StudentList();

            if (ddl != 0)
            {
                foreach (var a in _db.TUsers.Where(w => w.cDel == null && w.nTermSubLevel2 == ddl))
                {
                    std = new StudentList();
                    std.number = counter;
                    counter = counter + 1;
                    std.sName = a.sName;
                    std.sLastName = a.sLastname;
                    std.nTSubLevel2 = a.nTermSubLevel2;
                    std.sIdentification = a.sStudentID;
                    std.sId = a.sID;
                    std.fingerStatus = false;
                    string str = a.dBirth.ToString();
                    str = str.Substring(0, str.Length - 11);
                    std.birthday = str;
                    std.email = a.sEmail;
                    std.phone = a.sPhone;
                    //var roomdata = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == a.nTermSubLevel2).FirstOrDefault();
                    //var roomdata2 = _db.TSubLevel.Where(w => w.nTSubLevel == roomdata.nTSubLevel).FirstOrDefault();
                    //std.classroom = roomdata2.SubLevel;
                    //std.room = roomdata.nTSubLevel2.ToString();

                    StudentList.Add(std);
                }
            }
            else
            {
                foreach (var a in _db.TUsers.Where(w => w.cDel == null))
                {
                    std = new StudentList();
                    std.number = counter;
                    counter = counter + 1;
                    std.sName = a.sName;
                    std.sLastName = a.sLastname;
                    std.nTSubLevel2 = a.nTermSubLevel2;
                    std.sIdentification = a.sStudentID;
                    std.sId = a.sID;
                    std.fingerStatus = false;
                    string str = a.dBirth.ToString();
                    str = str.Substring(0, str.Length - 11);
                    std.birthday = str;
                    std.email = a.sEmail;
                    std.phone = a.sPhone;
                    //var roomdata = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == a.nTermSubLevel2).FirstOrDefault();
                    //var roomdata2 = _db.TSubLevel.Where(w => w.nTSubLevel == roomdata.nTSubLevel).FirstOrDefault();
                    //std.classroom = roomdata2.SubLevel;
                    //std.room = roomdata.nTSubLevel2.ToString();

                    StudentList.Add(std);
                }
            }

            return StudentList;
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString()));
            dgd.DataSource = _db.TUsers.Where(w => (w.sName + " " + w.sLastname).Contains(txtSearch.Text) && string.IsNullOrEmpty(w.cDel)).ToList();
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string termId = Request.QueryString["termId"];
            //if (termId.Length < 5) termId = string.Format("TS{0:000000}", int.Parse(termId));
            if (string.IsNullOrEmpty(termId)) termId = (string)ViewState["termId"];
            OpenData(termId);
            dgd.PageIndex = dgd.PageIndex + 1;
            if (dgd.PageIndex > dgd.PageCount)
                dgd.PageIndex = dgd.PageCount;
            dgd.DataBind();
        }

        public void backbutton_Click(Object sender, EventArgs e)
        {
            string termId = Request.QueryString["termId"];
            if (termId.Length < 5) termId = string.Format("TS{0:000000}", int.Parse(termId));
            if (string.IsNullOrEmpty(termId)) termId = (string)ViewState["termId"];
            OpenData(termId);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {

            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
            dgd.PageIndex = pageList.SelectedIndex;
            string termId = Request.QueryString["termId"];
            if (string.IsNullOrEmpty(termId)) termId = (string)ViewState["termId"];
            OpenData(termId);
            dgd.DataBind();
        }

        protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        {
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
            ViewState["pagesize"] = Int32.Parse(pageList2.SelectedValue);
            dgd.PageIndex = 0;
            string termId = Request.QueryString["termId"];
            //if (termId.Length < 5) termId = string.Format("TS{0:000000}", int.Parse(termId));
            if (string.IsNullOrEmpty(termId)) termId = (string)ViewState["termId"];
            OpenData(termId);
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

                int num = 100;
                int num2 = 200;
                int num3 = 300;
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static userdata getpassword(int user_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);

                var q = (from a in _dbMaster.TUsers
                         where a.nSystemID == user_id && a.cType == "0" && a.nCompany == qcompany.nCompany
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
                    var q = _dbMaster.TUsers.FirstOrDefault(f => f.nSystemID == password.user_id && f.cType == "0" && f.nCompany == qcompany.nCompany);
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool getinvoice_status(int user_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities())
                {
                    var q = (from a in peakengineEntities.TInvoices
                             where a.student_id == user_id && a.school_id == qcompany.nCompany && a.invoices_status.ToLower() != "void" && (a.isDel == false) == false
                             select a.invoices_Id).Count();

                    return q == 0;
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool resettpin(int user_id)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                var f_data = _dbMaster.TUsers.FirstOrDefault(f => f.nSystemID == user_id && f.cType == "0" && f.nCompany == qcompany.nCompany);
                f_data.pin = string.Empty;
                return _dbMaster.SaveChanges() != -1;
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

        protected class StudentList
        {
            public int? nTermSubLevel2;
            public int? nTSubLevel;
            public int number { get; set; }
            public string sName { get; set; }
            public string sLastName { get; set; }
            public string sIdentification { get; set; }
            public int? sId { get; set; }
            public string SubLevel { get; set; }
            public int? nTSubLevel2 { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public bool fingerStatus { get; set; }
            public string birthday { get; set; }
            public string classroom { get; set; }
            public string room { get; set; }
            public string timetype { get; set; }
            public string status { get; set; }
        }
    }
}