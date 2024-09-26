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
    public partial class SDQIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities));

            btnsearch.Click += new EventHandler(btnSearch_Click);
            dgd.RowDataBound += new GridViewRowEventHandler(dgd_RowDataBound);
            txtSearch.TextChanged += new EventHandler(txtSearch_TextChanged);

            if (!IsPostBack)
            {
                Showdata();

                List<TYear> yearsList = new List<TYear>();
                TYear tb_year = new TYear();
                foreach (var a in jabJaiEntities.TYears.Where(w => w.SchoolID == userData.CompanyID).ToList())
                {
                    tb_year = new TYear();
                    tb_year = a;
                    yearsList.Add(tb_year);
                }

                DropDownList1.DataSource = yearsList;
                DropDownList1.DataTextField = "numberYear";
                DropDownList1.DataValueField = "numberYear";
                DropDownList1.DataBind();

            }
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

            dgd.DataSource = sDQStudentDatas(name);
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

            dgd.DataSource = sDQStudentDatas(name);

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

            dgd.DataSource = sDQStudentDatas(name);
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

            dgd.DataSource = sDQStudentDatas(name);
            if (dgd.PageIndex > 0)
                dgd.PageIndex = dgd.PageIndex - 1;
            dgd.DataBind();
        }

        protected List<SDQStudentData> sDQStudentDatas(string name)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string strEntities = Session["sEntities"].ToString();
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(strEntities));

            int index = 1;

            T_FSDQ_Data _check = new T_FSDQ_Data();

            var data = (from a in jabJaiEntities.TUsers.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList()
                        join b in jabJaiEntities.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                        join c in jabJaiEntities.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                        where b.nTermSubLevel2 == 3911 && c.nTSubLevel == 1704
                        orderby a.nStudentNumber ascending

                        select new SDQStudentData
                        {
                            Autonumber = index++,
                            sID = a.sID,
                            studentName = a.sName,
                            studentLastname = a.sLastname,
                            studentId = a.sStudentID,
                            studentStatus = a.nStudentStatus,
                            studentClass = c.SubLevel + "/" + b.nTSubLevel2,
                            professorOfclass = 1111,
                        }).ToList();

            foreach (var data2 in data)
            {
                var check = jabJaiEntities.T_FSDQ_Data.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == data2.sID).FirstOrDefault();
                if (check != null)
                    data2.haveData = 1;
                else data2.haveData = 0;
            }

            if (!string.IsNullOrEmpty(txtSearch.Text)) data = data.Where(w => (w.studentName + "" + w.studentLastname).Contains(txtSearch.Text)).ToList();

            return data;
        }



        private void Showdata()
        {
            JabJaiEntities jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            dgd.DataSource = sDQStudentDatas("");
            dgd.DataBind();
        }



    }

    public class SDQStudentData
    {
        public int sID { get; set; }
        public string studentName { get; set; }
        public string studentLastname { get; set; }
        public string studentId { get; set; }
        public int? studentStatus { get; set; }
        public string studentClass { get; set; }
        public int? professorOfclass { get; set; }
        public int? Autonumber { get; set; }
        public int? Years { get; set; }
        public int haveData { get; set; }
    }
}