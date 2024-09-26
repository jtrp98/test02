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
using FingerprintPayment.ViewModels;
using FingerprintPayment.Helper;

namespace FingerprintPayment.Setting
{
    public partial class settingTeacherExtraTime : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            // JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
            int sEmpID = int.Parse(Session["sEmpID"] + "");

            deleteBtn.Click += new EventHandler(deleteBtn_Click);
            //btnSave.Click += new EventHandler(btnSave_Click);
            Button1.Click += new EventHandler(Button1_Click);
            // using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read))) { 
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                    if (!IsPostBack)
                    {
                        if (ddlyear.SelectedItem == null && ddlyear.SelectedValue == "")
                        {
                            List<TYear> tylist = new List<TYear>();
                            TYear ty = new TYear();
                            foreach (var a in _db.TYears.Where(w => w.SchoolID == schoolid && w.cDel == false).ToList())
                            {
                                ty = new TYear();
                                ty = a;
                                tylist.Add(ty);
                            }
                            var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                            ddlyear.DataSource = newList;
                            ddlyear.DataTextField = "numberYear";
                            ddlyear.DataValueField = "nYear";
                            ddlyear.DataBind();

                            var nYear = newList[0].nYear;
                            var tTerms = _db.TTerms.Where(x => x.SchoolID == schoolid && x.nYear == nYear && x.cDel == null).OrderBy(o => o.sTerm).ToList();

                            ddlterm.DataSource = tTerms;
                            ddlterm.DataTextField = "sTerm";
                            ddlterm.DataValueField = "nTerm";
                            ddlterm.DataBind();

                            //foreach (var data2 in _db.TEmployees.Where(w => w.SchoolID == schoolid && w.cDel == null))
                            //{
                            //    var item2 = new ListItem
                            //    {
                            //        Text = data2.sName + " " + data2.sLastname,
                            //        Value = data2.sEmp.ToString()
                            //    };
                            //    userlist.Items.Add(item2);
                            //}

                            List<TSubLevel> SubLevel = new List<TSubLevel>();
                            TSubLevel sub = new TSubLevel();

                            foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == schoolid && w.nWorkingStatus == 1))
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
                                room1.Items.Add(item);
                            }


                        }
                        //else if (ddlyear.SelectedValue != "")
                        //{
                        //    var nYear = Int32.Parse(ddlyear.SelectedValue);
                        //    var tTerms = _db.TTerms.Where(x => x.SchoolID == schoolid && x.nYear == nYear).OrderBy(o => o.sTerm).ToList();

                        //    ddlterm.DataSource = tTerms;
                        //    ddlterm.DataTextField = "sTerm";
                        //    ddlterm.DataValueField = "sTerm";
                        //    ddlterm.DataBind();
                        //}
                        //string termm = Request.QueryString["term"];
                        //ddlyear.SelectedValue = termm;




                        OpenData();



                    }
                    //else if (ddlyear.SelectedValue != "" && ddlterm.SelectedValue == "")
                    //{
                    //    var nYear = Int32.Parse(ddlyear.SelectedValue);
                    //    var tTerms = _db.TTerms.Where(x => x.SchoolID == schoolid && x.nYear == nYear).OrderBy(o => o.sTerm).ToList();

                    //    ddlterm.DataSource = tTerms;
                    //    ddlterm.DataTextField = "sTerm";
                    //    ddlterm.DataValueField = "sTerm";
                    //    ddlterm.DataBind();
                    //}
                }
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    string year = ddlyear.SelectedValue;
                    string term = ddlterm.SelectedValue;

                    int? nYear = Int32.Parse(year);
                    int nyear = 0;
                    string nterm = "";
                    List<string> planIdlist = new List<string>();


                    foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.nYear == nYear && w.cDel == false))
                    {
                        nyear = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear))
                    {
                        nterm = ee.nTerm;
                    }

                    int room2n = int.Parse(room2.SelectedValue);
                    int sEMPn = int.Parse(userlist.SelectedValue);

                    DateTime now = DateTime.Now;

                    var check = _db.TSettingExtraTimes.Where(w => w.SchoolID == schoolid && w.nTerm == nterm && w.nTermSubLevel2 == room2n && w.sEMP == sEMPn && w.sPlaneID.ToString() == planlist.SelectedValue && w.cDel == null && w.useToken == null && w.addDate.Day == now.Day && w.addDate.Month == now.Month).FirstOrDefault();

                    //int countID = _db.TSettingExtraTimes.Count() == 0 ? 0 : _db.TSettingExtraTimes.Max(max => max.nSettingExtraTime);
                    //countID = countID + 1;
                    int sPlaneId = 0;
                    int.TryParse(planlist.SelectedValue, out sPlaneId);
                    TSettingExtraTime extra = new TSettingExtraTime();
                    extra.addDate = DateTime.Now;
                    //extra.nSettingExtraTime = countID;
                    extra.nTerm = nterm;
                    extra.nTermSubLevel2 = room2n;
                    extra.sEMP = sEMPn;
                    extra.SchoolID = schoolid;
                    extra.sPlaneID = sPlaneId;

                    if (check == null)
                        _db.TSettingExtraTimes.Add(extra);

                    _db.SaveChanges();
                    Response.Redirect("settingTeacherExtraTime.aspx");
                }
            }

        }

        void Button1_Click(object sender, EventArgs e)
        {

            Response.Redirect("settingMainPage.aspx");
        }

        void deleteBtn_Click(object sender, EventArgs e)
        {
            string sEntities = Session["sEntities"] + "";
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    int idn = int.Parse(deleteID.Text);
                    var data1 = _db.TSettingExtraTimes.Where(w => w.nSettingExtraTime == idn).FirstOrDefault();

                    data1.useToken = 1;
                    data1.SchoolID = schoolid;
                    _db.SaveChanges();
                    Response.Redirect("settingTeacherExtraTime.aspx");
                }
            }
        }

        string monthtxt(DateTime? original)
        {


            string txt1 = original.Value.Day.ToString();
            string txt2 = original.Value.Month.ToString();
            string txt3 = original.Value.Year.ToString();

            if (txt2 == "1")
                txt2 = "ม.ค.";
            else if (txt2 == "2")
                txt2 = "ก.พ.";
            else if (txt2 == "3")
                txt2 = "มี.ค.";
            else if (txt2 == "4")
                txt2 = "เม.ย.";
            else if (txt2 == "5")
                txt2 = "พ.ค.";
            else if (txt2 == "6")
                txt2 = "มิ.ย.";
            else if (txt2 == "7")
                txt2 = "ก.ค.";
            else if (txt2 == "8")
                txt2 = "ส.ค.";
            else if (txt2 == "9")
                txt2 = "ก.ย.";
            else if (txt2 == "10")
                txt2 = "ต.ค.";
            else if (txt2 == "11")
                txt2 = "พ.ย.";
            else if (txt2 == "12")
                txt2 = "ธ.ค.";

            int year = int.Parse(txt3);
            if (year < 2500)
                year = year + 543;


            return txt1 + " " + txt2 + " " + year.ToString();
        }

        //protected void userlist_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string sEntities = Session["sEntities"] + "";
        //    using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
        //        int schoolid = (int)nCompany.nCompany;
        //        using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //        {
        //            Textbox2.Text = "ssss";
        //            int sEmpID = int.Parse(Textbox1.Text);
        //            Textbox2.Text = Textbox1.Text;



        //            string idlv = room1.SelectedValue;
        //            string idlv2 = room2.SelectedValue;
        //            string year = ddlyear.SelectedValue;
        //            string term = ddlterm.SelectedValue;

        //            PlanList Plan = new PlanList();
        //            List<PlanList> PlanList = new List<PlanList>();

        //            planlist.Items.Clear();

        //            if ((year != "" && year != null) && (idlv2 != "" && idlv2 != null))
        //            {
        //                int? idlv2n = Int32.Parse(idlv2);
        //                int? idlvn = Int32.Parse(idlv);
        //                int? nYear = Int32.Parse(year);
        //                int nyear = 0;
        //                string nterm = "";
        //                //int ntermtable = 0;
        //                List<string> planIdlist = new List<string>();


        //                foreach (var ff in _db.TYears.Where(w => w.SchoolID == schoolid && w.nYear == nYear))
        //                {
        //                    nyear = ff.nYear;
        //                }

        //                foreach (var ee in _db.TTerms.Where(w => w.SchoolID == schoolid && w.sTerm == term && w.nYear == nyear))
        //                {
        //                    nterm = ee.nTerm;
        //                }
        //                var plancourseDTOs = ServiceHelper.GetPlanCourses(idlvn ?? 0, idlv2n ?? 0, nterm, nyear, schoolid, _db);
        //                if (plancourseDTOs != null)
        //                {
        //                    var teacherSubject = (from p in plancourseDTOs
        //                                          join t in _db.TPlanCourseTeachers.Where(w => w.SchoolID == schoolid && w.IsActive == true && w.sEmp == sEmpID).ToList() on p.PlanCourseId equals t.PlanCourseId
        //                                          select p).OrderBy(o => o.CourseGroup).ThenBy(o => o.NOrder).ThenBy(o => o.CourseCode).ToList();
        //                    if (teacherSubject != null)
        //                    {
        //                        foreach (var s in teacherSubject)
        //                        {
        //                            var item = new ListItem
        //                            {
        //                                Text = s.CourseName,
        //                                Value = s.SPlaneId.ToString()
        //                            };
        //                            planlist.Items.Add(item);
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //    }

        //}

        //protected void room1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string sEntities = Session["sEntities"] + "";
        //    using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
        //        int schoolid = (int)nCompany.nCompany;
        //        using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //        {

        //            string idlv = room1.SelectedValue;
        //            int nidlv = int.Parse(idlv);
        //            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

        //            ddl ddl = new ddl();
        //            List<ddl> ddlList = new List<ddl>();

        //            foreach (var lv1 in _db.TTermSubLevel2.Where(w => w.SchoolID == schoolid && w.nTSubLevel == nidlv && w.nTermSubLevel2Status == "1" && w.nWorkingStatus == 1))
        //            {
        //                var lv2 = _db.TSubLevels.Where(w => w.nTSubLevel == nidlv).FirstOrDefault();
        //                ddl = new ddl();
        //                ddl.name = lv2.SubLevel.TrimEnd() + " / " + lv1.nTSubLevel2;
        //                ddl.value = lv1.nTermSubLevel2.ToString();
        //                int n;
        //                bool isNumeric = int.TryParse(lv1.nTSubLevel2, out n);
        //                if (isNumeric == true)
        //                    ddl.sort = Int32.Parse(lv1.nTSubLevel2);
        //                else ddl.sort = 500;


        //                ddlList.Add(ddl);
        //            }

        //            var newList3 = ddlList.OrderBy(x => x.sort).ThenBy(x => x.name).ToList();

        //            room2.Items.Clear();

        //            foreach (var t in newList3)
        //            {
        //                var item = new ListItem
        //                {
        //                    Text = t.name,
        //                    Value = t.value.ToString()
        //                };
        //                room2.Items.Add(item);
        //            }
        //        }
        //    }
        //}

        protected List<fixExtraTime> returnlist()
        {
            string sEntities = Session["sEntities"] + "";
            fixExtraTime gradeAdmin = new fixExtraTime();
            List<fixExtraTime> gradeAdminList = new List<fixExtraTime>();
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int schoolid = (int)nCompany.nCompany;
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {



                    DateTime now = DateTime.Now;
                    foreach (var data in _db.TSettingExtraTimes.Where(w => w.SchoolID == schoolid && w.cDel == null))
                    {
                        gradeAdmin = new fixExtraTime();
                        gradeAdmin.sEMP = data.sEMP;
                        var a1 = _db.TEmployees.Where(w => w.SchoolID == schoolid && w.sEmp == data.sEMP).FirstOrDefault();
                        if (a1 != null)
                            gradeAdmin.name = a1.sName + " " + a1.sLastname;

                        var a2 = _db.TPlanes.Where(w => w.SchoolID == schoolid && w.sPlaneID == data.sPlaneID).FirstOrDefault();
                        if (a2 != null)
                            gradeAdmin.planName = a2.sPlaneName;

                        gradeAdmin.Date = monthtxt(data.addDate);
                        if (data.useToken == null && data.addDate.Day == now.Day && data.addDate.Month == now.Month)
                            gradeAdmin.status = "มีสิทธิแก้ไข";
                        else
                            gradeAdmin.status = "ไม่มีสิทธิแก้ไข";

                        gradeAdmin.dataID = data.nSettingExtraTime;
                        var room = _db.TTermSubLevel2.Where(w => w.SchoolID == schoolid && w.nTermSubLevel2 == data.nTermSubLevel2).FirstOrDefault();
                        if (room != null)
                        {
                            var sub = _db.TSubLevels.Where(w => w.SchoolID == schoolid && w.nTSubLevel == room.nTSubLevel).FirstOrDefault();
                            gradeAdmin.room = sub.SubLevel + " / " + room.nTSubLevel2;
                        }

                        gradeAdmin.SchoolID = schoolid;
                        gradeAdminList.Add(gradeAdmin);
                    }

                    int number = 1;
                    var newList = gradeAdminList.OrderByDescending(x => x.dataID).ToList();
                    foreach (var data in newList)
                    {
                        data.number = number;
                        number = number + 1;
                    }

                    return newList;
                }
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
        protected class fixExtraTime
        {
            public int number { get; set; }
            public string name { get; set; }
            public int sEMP { get; set; }
            public string Date { get; set; }
            public string planName { get; set; }
            public string room { get; set; }
            public string status { get; set; }
            public int dataID { get; set; }
            public int SchoolID { get; set; }
        }

        protected class extraPlan
        {
            public string name { get; set; }
            public string planid { get; set; }

        }

        protected class ddl
        {
            public string name { get; set; }
            public string value { get; set; }
            public int sort { get; set; }
        }

        protected class PlanList
        {
            public int number { get; set; }
            public string planName { get; set; }
            public string planId { get; set; }
            public string code { get; set; }
            public string year { get; set; }
            public string idlv { get; set; }
            public string idlv2 { get; set; }
            public string term { get; set; }
            public string mode { get; set; }
            public string havedata { get; set; }
            public int? sortnumber { get; set; }
            public string extratime { get; set; }
        }

        //protected class sortList
        //{
        //    public string planId { get; set; }
        //    public int? sortnumberType { get; set; }
        //    public int? sortnumberGroup { get; set; }
        //    public string planName { get; set; }
        //    public string planCode { get; set; }

        //}
    }
}