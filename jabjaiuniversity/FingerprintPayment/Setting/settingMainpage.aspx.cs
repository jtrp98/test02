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
using System.Web.Services;

namespace FingerprintPayment.Setting
{
    public partial class settingMainpage : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }

        protected decimal CostGradeRepairMid = 0m;
        protected decimal CostGradeRepairFinal = 0m;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //  JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string comid = Request.QueryString["comid"];
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {

                    string sEntities = Session["sEntities"] + "";
                    using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                    {
                        var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        int schoolid = (int)nCompany.nCompany;

                        CostGradeRepairMid = nCompany.CostGradeRepairMid == null ? 0m : nCompany.CostGradeRepairMid.Value;
                        CostGradeRepairFinal = nCompany.CostGradeRepairFinal == null ? 0m : nCompany.CostGradeRepairFinal.Value;

                        OpenData();

                        string yearr = Request.QueryString["year"];

                        string termm = Request.QueryString["term"];

                        string idlv = Request.QueryString["idlv"];
                        string idlv2 = Request.QueryString["idlv2"];

                        string mode = Request.QueryString["mode"];

                        List<TSubLevel> SubLevel = new List<TSubLevel>();
                        TSubLevel sub = new TSubLevel();

                        foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == schoolid && w.nWorkingStatus == 1))
                        {
                            sub = new TSubLevel();
                            sub = a;
                            sub.SchoolID = schoolid;
                            SubLevel.Add(sub);
                        }

                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();
                        foreach (var a in _db.TYears.Where(x => x.SchoolID == schoolid && x.cDel == false).ToList())
                        {
                            ty = new TYear();
                            ty = a;
                            ty.SchoolID = schoolid;
                            tylist.Add(ty);
                        }

                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();
                    }
                }
            }
        }

        protected List<systemList> returnlist()
        {
            // JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString())))
            {
                int sEmpID = int.Parse(Session["sEmpID"] + "");
                string sEntities = Session["sEntities"] + "";
                systemList system = new systemList();
                List<systemList> sysList = new List<systemList>();
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities())
                {
                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    int schoolid = (int)nCompany.nCompany;

                    system = new systemList();
                    system.number = 1;
                    system.SchoolID = schoolid;
                    system.name = "อนุญาตให้บันทึกคะแนนเฉพาะช่วงเวลาที่กำหนดไว้เท่านั้น";
                    system.status = "ไม่ใช้งาน";
                    system.click = 1;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 2;
                    system.SchoolID = schoolid;
                    system.name = "ผู้ใช้งานจะเห็นรายชื่อวิชาเฉพาะวิชาที่เป็นครูผู้สอนเท่านั้น";
                    system.status = "ไม่ใช้งาน";
                    system.click = 2;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 5;
                    system.SchoolID = schoolid;
                    system.name = "ผู้ใช้งานจะดูรายงานการพัฒนาผู้เรียนได้เฉพาะห้องที่เป็นครูประจำชั้นเท่านั้น";
                    system.status = "ไม่ใช้งาน";
                    system.click = 5;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 3;
                    system.SchoolID = schoolid;
                    system.name = "อนุญาตให้บุคคลต่อไปนี้สามารถแก้ไขคะแนนได้ทุกวิชาและทุกช่วงเวลา";
                    system.status = "ไม่ใช้งาน";
                    system.click = 3;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 4;
                    system.SchoolID = schoolid;
                    system.name = "เปิดสิทธิ์ให้ผู้ใช้งานแก้ไขคะแนน (มีผลถึงเที่ยงคืน)";
                    system.status = "";
                    system.click = 4;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 5;
                    system.SchoolID = schoolid;
                    system.name = "เพิ่มความคิดเห็นครูประจำชั้นสำหรับสุ่มในใบรายงานผลการเรียน";
                    system.status = "";
                    system.click = 5;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 6;
                    system.name = "อนุมัติการเข้าถึงเกรดของผู้ปกครองผ่านแอปพลิเคชัน School Bright";
                    system.status = "";
                    system.click = 6;
                    sysList.Add(system);

                    system = new systemList();
                    system.number = 7;
                    system.SchoolID = schoolid;
                    system.name = "ค่าลงทะเบียนสอบแก้ตัว";
                    system.status = "";
                    system.click = 7;
                    sysList.Add(system);

                }

                return sysList;
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

        protected class systemList
        {
            public int number { get; set; }
            public string name { get; set; }
            public string status { get; set; }
            public int click { get; set; }
            public int SchoolID { get; set; }
        }

        public class ddlterm
        {
            public string sTerm { get; set; }
        }

        [WebMethod]
        public static object SaveCostGradeRepair(CostData costData)
        {
            object result = "success";
            string message = "";

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

            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities())
            {
                try
                {
                    var schoolObj = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        schoolObj.CostGradeRepairMid = costData.costGradeRepairMid;
                        schoolObj.CostGradeRepairFinal = costData.costGradeRepairFinal;

                        dbMaster.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลค่าลงทะเบียนสอบแก้ตัว[schoolID:" + schoolID + ", costGradeRepairMid:" + schoolObj.CostGradeRepairMid + ", costGradeRepairFinal:" + schoolObj.CostGradeRepairFinal + "]", userData.Entities, HttpContext.Current.Request, 122, 3, 0);
                    }
                    else
                    {
                        result = "error";
                    }
                }
                catch
                {
                    result = "error";
                }
            }

            return result;
        }

        public class CostData
        {
            public decimal costGradeRepairMid { get; set; }
            public decimal costGradeRepairFinal { get; set; }
        }
    }
}