using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;


namespace FingerprintPayment
{
    public partial class grade_classroom : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
           
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");


            using (JabJaiMasterEntities _dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                //btnSearch.Click += new EventHandler(btnSearch_Click);
                if (!IsPostBack)
                {
                    OpenData();
                    fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "- ทั้งหมด -", "nTSubLevel", "SubLevel");

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string termm = Request.QueryString["term"];
                    DropDownList2.SelectedValue = termm;
                    string idlv = Request.QueryString["idlv"];
                    ddlsublevel.SelectedValue = idlv;

                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }
                    DropDownList1.DataSource = tylist;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();

                    ddlterm tm = new ddlterm();
                    List<ddlterm> tmlist = new List<ddlterm>();
                    List<string> tmc = new List<string>();

                    foreach (var b in _db.TTerms.ToList())
                    {
                        tmc.Add(b.sTerm);
                    }
                    List<string> tmc2 = tmc.Distinct().ToList<string>();
                    foreach (var c in tmc2)
                    {
                        tm = new ddlterm();
                        tm.sTerm = c;
                        tmlist.Add(tm);
                    }
                    DropDownList2.DataSource = tmlist;
                    DropDownList2.DataTextField = "sTerm";
                    DropDownList2.DataValueField = "sTerm";
                    DropDownList2.DataBind();
                }
            }
        }


        protected List<StudentScore> returnlist()
        {
            
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();

          
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                string SQL = "";
                string idlv = Request.QueryString["idlv"];
                string idlv2 = Request.QueryString["idlv2"];
                string DropDownList1 = Request.QueryString["year"];
                string DropDownList2 = Request.QueryString["term"];
                string sname = Request.QueryString["sname"];

                int number = 1;
                StudentScore xa = new StudentScore();
                List<StudentScore> list = new List<StudentScore>();

                foreach (var a in _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany && w.cDel != "1" && w.cType == "0"))
                {
                    xa = new StudentScore();
                    if (idlv2 == "" || idlv2 == null)
                    {
                        var b = _db.TUser.Where(w => w.sID == a.nSystemID).FirstOrDefault();
                        if (b != null)
                        {
                            xa.sName = b.sName;
                            xa.sLastName = b.sLastname;
                            xa.sIdentification = b.sID.ToString();
                            xa.idnumber = b.sIdentification;
                            var c = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == b.nTermSubLevel2).FirstOrDefault();
                            var d = _db.TSubLevels.Where(w => w.nTSubLevel == c.nTSubLevel).FirstOrDefault();

                            xa.SubLevel = d.SubLevel;
                            xa.nTSubLevel2 = c.nTSubLevel2.ToString();
                            xa.year = DropDownList1;
                            xa.term = DropDownList2;
                            xa.number = number;
                            xa.idlv = idlv;
                            xa.idlv2 = idlv2;
                            list.Add(xa);
                            number = number + 1;
                        }

                    }
                    else
                    {
                        int nidlv2 = Int32.Parse(idlv2);
                        var b = _db.TUser.Where(w => w.sID == a.nSystemID && w.nTermSubLevel2 == nidlv2).FirstOrDefault();
                        if (b != null)
                        {
                            xa.sName = b.sName;
                            xa.sLastName = b.sLastname;
                            xa.sIdentification = b.sID.ToString();
                            xa.idnumber = b.sIdentification;
                            var c = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == b.nTermSubLevel2).FirstOrDefault();
                            var d = _db.TSubLevels.Where(w => w.nTSubLevel == c.nTSubLevel).FirstOrDefault();

                            xa.SubLevel = d.SubLevel;
                            xa.nTSubLevel2 = c.nTSubLevel2.ToString();

                            xa.year = DropDownList1;
                            xa.term = DropDownList2;
                            xa.number = number;
                            xa.idlv = idlv;
                            xa.idlv2 = idlv2;
                            list.Add(xa);
                            number = number + 1;
                        }

                    }


                }



                if (idlv == null && idlv2 == null && DropDownList1 == null && DropDownList2 == null)
                {
                    return null;
                }
                else
                {
                    return list;
                }
                return list;
            }
        }

        private void OpenData()
        {

            dgd.DataSource = returnlist();
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
        protected class StudentScore
        {
            public string idnumber { get; set; }
            public string idlv { get; set; }
            public string idlv2 { get; set; }
            public string year { get; set; }
            public string term { get; set; }
            public string sName { get; set; }
            public string sLastName { get; set; }
            public string sIdentification { get; set; }
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int endScore { get; set; }
            public int number { get; set; }
        }

       

       

       

       

        public class ddlterm
        {            
            public string sTerm { get; set; }
        }
    }
}