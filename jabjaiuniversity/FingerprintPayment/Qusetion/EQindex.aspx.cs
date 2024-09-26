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

namespace FingerprintPayment.Qusetion
{
    public partial class EQindex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities));

            btnsearch.Click += new EventHandler(btnSearch_Click);
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);
            txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());

            if (!IsPostBack)
            {
                Showdata();

                //string RequestClass = Request.QueryString["RequestClasss"];
                //DropDownClass.SelectedValue = RequestClass;

                //fcommon.ListDBToDropDownList(_conn, DropDownClass, "select * from TSubLevel", "- ทั้งหมด -", "nTSubLevel", "SubLevel");


                List<TYear> yearsList = new List<TYear>();
                TYear tb_year = new TYear();
                foreach (var a in jabJaiEntities.TYears.ToList())
                {
                    tb_year = new TYear();
                    tb_year = a;
                    yearsList.Add(tb_year);
                }
                DropDownYears.DataSource = yearsList;
                DropDownYears.DataTextField = "numberYear";
                DropDownYears.DataValueField = "numberYear";
                DropDownYears.DataBind();

                //string RequestTerm = Request.QueryString["RequestTerms"];
                //DropDownTerm.SelectedValue = RequestTerm;
      
                //ddlterm ddlterm = new ddlterm();
                //List<ddlterm> termList = new List<ddlterm>();
                //List<string> tm1 = new List<string>();
                //foreach (var b in jabJaiEntities.TTerms.ToList())
                //{
                //    tm1.Add(b.sTerm);
                //}
                //List<string> tm2 = tm1.Distinct().ToList<string>();
                //foreach (var c in tm2)
                //{
                //    ddlterm = new ddlterm();
                //    ddlterm.sTerm = c;
                //    termList.Add(ddlterm);
                //}
                //DropDownTerm.DataSource = termList;
                //DropDownTerm.DataTextField = "sTerm";
                //DropDownTerm.DataValueField = "sTerm";
                //DropDownTerm.DataBind();
            }
        }

        protected void DropDownYears_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }


        protected void CustomersGridView_DataBound(Object sender, EventArgs e)
        {
            GridViewRow pagerRow = dgd.BottomPagerRow;
            if (pagerRow != null)
            {
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
                    for (int i = 0; i < dgd.PageCount; i++)
                    {
                        int pageNumber = i + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        if (i == dgd.PageIndex)
                        {
                            item.Selected = true;
                        }
                        pageList.Items.Add(item);
                    }
                }

                if (pageLabel != null)
                {
                    int currentPage = dgd.PageIndex + 1;

                    pageLabel.Text = "หน้าที่ " + currentPage.ToString() + " จากทั้งหมด " + dgd.PageCount.ToString() + " หน้า ";
                }
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
            Showdata();
        }
        void btnSearch_Click(object sender, EventArgs e)
        {
            Showdata();
        }

        protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;
            GridViewRow pagerRow = dgd.BottomPagerRow;
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");

            dgd.DataSource = studentDatas(name);
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

            dgd.DataSource = studentDatas(name);

            int size = Int32.Parse(pageList2.SelectedValue);

            dgd.PageSize = size;
            dgd.PageIndex = 0;
            dgd.DataBind();
        }

        public void nextbutton_Click(Object sender, EventArgs e)
        {
            string name = "";
            if (txtSearch.Text != "")
                name = txtSearch.Text;

            dgd.DataSource = studentDatas(name);
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

            dgd.DataSource = studentDatas(name);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected List<StudentDatas> studentDatas(string name)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities));

            int index = 1;

            var users = jabJaiEntities.TUsers.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
            var termSubLevel2s = jabJaiEntities.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var subLevels = jabJaiEntities.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var classMembers = jabJaiEntities.TClassMembers.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var tstudentClassroomHistory = jabJaiEntities.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var tterm = jabJaiEntities.TTerms.Where(w => w.SchoolID == userData.CompanyID).ToList();
            var ttyer = jabJaiEntities.TYears.Where(w => w.SchoolID == userData.CompanyID).ToList();

            var data = (from a in users
                        join b in termSubLevel2s on a.nTermSubLevel2 equals b.nTermSubLevel2
                        join c in subLevels on b.nTSubLevel equals c.nTSubLevel
                        join d in classMembers on b.nTermSubLevel2 equals d.nClassMemberid


                        select new StudentDatas
                        {
                            Autonumber = index++,
                            sID = a.sID,
                            studentName = a.sName,
                            studentLastname = a.sLastname,
                            studentId = a.sStudentID,
                            studentStatus = a.nStudentStatus,
                            studentClass = c.SubLevel + "/" + b.nTSubLevel2,
                            professorOfclass = d.nTeacherHeadid
                        }).ToList();

            foreach (var data2 in data)
            {
                var check = jabJaiEntities.TB_EQ_Data.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == data2.sID).FirstOrDefault();
                if (check != null)
                    data2.STDcheckID = 1;
                else data2.STDcheckID = 0;
            }

            if (!string.IsNullOrEmpty(txtSearch.Text)) data = data.Where(w => (w.studentName + "" + w.studentLastname).Contains(txtSearch.Text)).ToList();

            return data;

        }




        private void Showdata()
        {
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            dgd.DataSource = studentDatas("");
            dgd.DataBind();
        }

    }

    public class StudentDatas
    {
        public int sID { get; set; }
        public string studentName { get; set; }
        public string studentLastname { get; set; }
        public string studentId { get; set; }
        public int? studentStatus { get; set; }
        public string studentClass { get; set; }
        public int? professorOfclass { get; set; }
        public int? Autonumber { get; set; }
        public int STDcheckID { get; set; }
    }

    public class ddlterm
    {
        public string sTerm { get; set; }
    }


}