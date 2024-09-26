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
using System.Text.RegularExpressions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace FingerprintPayment.classconfig
{
    public partial class classSetting : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read);
            int sEmpID = int.Parse(Session["sEmpID"] + "");
            string idlv = Request.QueryString["idlv"];

            Modal2Button.Click += new EventHandler(Modal2Button_Click);
            modal3btn.Click += new EventHandler(Modal3Button_Click);
            btnSave.Click += new EventHandler(btnSave_Click);
            editclassroom.Click += new EventHandler(editclassroom_Click);
            delete1.Click += new EventHandler(delete1Button_Click);

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                if (!IsPostBack)
                {
                    OpenData();


                    foreach (var data1 in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel != 1))
                    {
                        var data2 = _db.TBranchSubjects.Where(w => w.BranchSubjectId == data1.BranchSubjectId && w.SchoolID == userData.CompanyID && w.cDel != 1).FirstOrDefault();
                        var data3 = _db.TBranches.Where(w => w.BranchId == data2.BranchId && w.SchoolID == userData.CompanyID && w.cDel != 1).FirstOrDefault();
                        var tTLevel = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID && w.LevelID == data3.nTLevel).FirstOrDefault();

                        if (tTLevel != null)
                        {
                            string type = "";
                            if (tTLevel.LevelName == "ปวช.")
                                type = "ปวช.";
                            else if (tTLevel.LevelName == "ปวส.")
                                type = "ปวส.";
                            var item = new ListItem
                            {
                                Text = data1.BranchSpecName + " (" + type + ")" + " (" + data3.BranchName + ")",
                                Value = data1.BranchSpecId.ToString()
                            };
                            ddlBranch.Items.Add(item);
                            ddlBranch2.Items.Add(item);
                        }
                    }



                    var level = _db.TLevels.Where(w => w.sortValue > 0 && w.sortValue != null && w.SchoolID == userData.CompanyID).ToList();
                    var newList = level.OrderBy(x => x.sortValue).ToList();

                    foreach (var t in newList)
                    {
                        var item = new ListItem
                        {
                            Text = t.LevelName,
                            Value = t.LevelID.ToString()
                        };
                        modalType.Items.Add(item);
                    }

                    var item2 = new ListItem
                    {
                        Text = "อื่นๆ",
                        Value = "999"
                    };
                    modalType.Items.Add(item2);


                    var timetype = _db.TTimetypes.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList();

                    var itemz = new ListItem
                    {
                        Text = "เลือกตารางเวลา",
                        Value = "-1"
                    };
                    ddlTimeTable.Items.Add(itemz);

                    foreach (var t in timetype)
                    {
                        var item = new ListItem
                        {
                            Text = t.sTimeType,
                            Value = t.nTimeType.ToString()
                        };
                        ddlTimeTable.Items.Add(item);
                        timeEdit.Items.Add(item);
                    }

                }
            }
        }

        void editclassroom_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int modal4idn = int.Parse(modal4id.Text);


                var a = _db.TSubLevels.Where(w => w.nTSubLevel == modal4idn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                a.fullName = modal4name.Text.Trim();
                a.SubLevel = modal4nick.Text.Trim();
                a.fullNameEN = modal4nameEN.Text.Trim();
                a.SubLevelEN = modal4nickEN.Text.Trim();
                a.CreatedDate = DateTime.Now;
                a.CreatedBy = userData.UserID;

                _db.SaveChanges();

                Response.Redirect("classSetting.aspx");
            }
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int count = 1;
                int type = 0;

                if (modalType.SelectedValue == "999")
                {
                    //int count2 = 0;
                    int sortvalue = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).Select(s => s.sortValue ?? 0).DefaultIfEmpty(0).Max();

                    TLevel tlv = new TLevel();
                    //tlv.LevelID = count2 + 1;
                    tlv.LevelName = modalName.Text;
                    tlv.sortValue = sortvalue + 100000;
                    tlv.SchoolID = userData.CompanyID;
                    tlv.CreatedBy = userData.UserID;
                    tlv.CreatedDate = DateTime.Now;

                    _db.TLevels.Add(tlv);
                    _db.SaveChanges();

                    TSubLevel tsublv = new TSubLevel();
                    //tsublv.nTSubLevel = count + 1;
                    tsublv.SubLevel = modalNickName.Text;
                    tsublv.SubLevelEN = modalNickNameEN.Text;
                    tsublv.nTLevel = tlv.LevelID;
                    tsublv.nDeleted = 0;
                    tsublv.fullName = modalName.Text;
                    tsublv.fullNameEN = modalNameEN.Text;
                    tsublv.SchoolID = userData.CompanyID;
                    tsublv.CreatedBy = userData.UserID;
                    tsublv.CreatedDate = DateTime.Now;

                    _db.TSubLevels.Add(tsublv);
                    _db.SaveChanges();
                }
                else if (modalType.SelectedValue == "0")
                {

                }
                else
                {
                    //foreach (var a in _db.TSubLevels)
                    //{
                    //    if (count <= a.nTSubLevel)
                    //        count = a.nTSubLevel;
                    //}
                    TSubLevel tsublv = new TSubLevel();
                    //tsublv.nTSubLevel = count + 1;
                    tsublv.SubLevel = modalNickName.Text;
                    tsublv.SubLevelEN = modalNickNameEN.Text;
                    tsublv.nTLevel = int.Parse(modalType.SelectedValue);
                    tsublv.nDeleted = 0;
                    tsublv.fullName = modalName.Text;
                    tsublv.fullNameEN = modalNameEN.Text;
                    tsublv.SchoolID = userData.CompanyID;
                    tsublv.CreatedBy = userData.UserID;
                    tsublv.CreatedDate = DateTime.Now;

                    _db.TSubLevels.Add(tsublv);
                    _db.SaveChanges();
                }



                Response.Redirect("classSetting.aspx");
            }
        }

        void Modal3Button_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                string sEntities = Session["sEntities"] + "";
                var id = int.Parse(modal3id.Text);

                var data = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == id).FirstOrDefault();
                data.nTSubLevel2 = modal3name.Text;
                data.nTimeType = int.Parse(timeEdit.SelectedValue);
                data.UpdatedDate = DateTime.Now;
                data.UpdatedBy = userData.UserID;

                if (!string.IsNullOrEmpty(ddlBranch.SelectedValue))
                {
                    data.nBranchSpecId = int.Parse(ddlBranch.SelectedValue);
                }

                _db.SaveChanges();
                Response.Redirect("classSetting.aspx");
            }
        }

        void delete1Button_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";
                var id = int.Parse(delid.Text);
                var type = deltype.Text;

                var tstudent = _db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList();
                var tlevel2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();
                var tsublevel = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();


                if (type == "sublevel")
                {
                    var list1 = (from a in tstudent
                                 join b in tlevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                                 join c in tsublevel on b.nTSubLevel equals c.nTSubLevel
                                 where c.nTSubLevel == id
                                 select new { studentID = a.sID }).Count();

                    if (list1 == 0)
                    {
                        var data = _db.TSubLevels.Where(w => w.nTSubLevel == id).First();
                        data.nWorkingStatus = 0;
                        data.nDeleted = 1;
                        foreach (var a in _db.TTermSubLevel2.Where(w => w.nTSubLevel == id))
                        {
                            a.nTermSubLevel2Status = "0";
                            a.nWorkingStatus = 0;
                        }
                        _db.SaveChanges();
                        Response.Write("<script>alert('ลบชั้นเรียนเรียบร้อยแล้ว'); window.location.replace('classSetting.aspx');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('ชั้นเรียนนี้มีนักเรียนอยู่ ไม่สามารถลบได้'); window.location.replace('classSetting.aspx');</script>");
                    }
                }
                else
                {
                    var list2 = (from a in tstudent
                                 join b in tlevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                                 join c in tsublevel on b.nTSubLevel equals c.nTSubLevel
                                 where b.nTermSubLevel2 == id
                                 select new { studentID = a.sID }).Count();

                    if (list2 == 0)
                    {
                        var data = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == id).FirstOrDefault();
                        data.nTermSubLevel2Status = "0";
                        data.nWorkingStatus = 0;

                        _db.SaveChanges();
                        Response.Write("<script>alert('ลบห้องเรียนเรียบร้อยแล้ว'); window.location.replace('classSetting.aspx');</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('ห้องนี้มีนักเรียนอยู่ไม่สามารถลบได้'); window.location.replace('classSetting.aspx');</script>");
                    }
                }

            }
        }

        void Modal2Button_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int counter = 1;
                foreach (var a in _db.TTermSubLevel2)
                {
                    if (a.nTermSubLevel2 >= counter)
                        counter = a.nTermSubLevel2 + 1;
                }

                int nTSubLevel = int.Parse(modal2nTSubLevel.Text);
                var check = _db.TSubLevels.Where(w => w.nTSubLevel == nTSubLevel).FirstOrDefault();


                TTermSubLevel2 tsub = new TTermSubLevel2();
                tsub.nTermSubLevel2 = counter;
                tsub.nTimeType = int.Parse(ddlTimeTable.SelectedValue);
                tsub.nTSubLevel = nTSubLevel;
                tsub.nTSubLevel2 = modal2Name.Text;
                tsub.nTermSubLevel2Status = "1";
                tsub.nWorkingStatus = 0;
                tsub.SchoolID = userData.CompanyID;
                tsub.CreatedBy = userData.UserID;
                tsub.CreatedDate = DateTime.Now;

                if (check.nTLevel == 1 || check.nTLevel == 2)
                {
                    tsub.nBranchSpecId = int.Parse(ddlBranch2.SelectedValue);
                }
                else tsub.nBranchSpecId = null;
                _db.TTermSubLevel2.Add(tsub);

                _db.SaveChanges();
                Response.Redirect("classSetting.aspx");
            }
        }

        protected List<classData> returnlist()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                List<classData> classDataList = new List<classData>();
                classData classData = new classData();

                List<classValue> classValueList = new List<classValue>();
                classValue classValue = new classValue();

                var TLevel = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();
                var TSubLevel = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nDeleted != null).ToList();
                var TTermSubLevel2 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();

                int counter = 1;
                int? valuecounter = 1000;
                int? lastcounter = 99999;

                var sublvdata = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nDeleted == 0).ToList();
                var newList = sublvdata.OrderBy(x => x.nTLevel).ToList();
                //var Tlevel = _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();

                foreach (var a in newList)
                {
                    classData = new classData();

                    classData.fullName = a.fullName;
                    classData.NickName = a.SubLevel;
                    classData.fullNameEN = a.fullNameEN;
                    classData.NickNameEN = a.SubLevelEN;
                    classData.nTLevel = a.nTLevel;
                    classData.type = "sublevel";
                    classData.status = a.nDeleted;
                    classData.id = a.nTSubLevel;
                    classData.SchoolID = userData.CompanyID;

                    var value = TLevel.Where(w => w.LevelID == a.nTLevel).FirstOrDefault();
                    if (value != null)
                    {
                        if (value.sortValue != null)
                            classData.sortValue = value.sortValue;
                        else classData.sortValue = 999;
                        classData.LevelName = value.LevelName;

                        classDataList.Add(classData);

                    }
                    else
                    {
                        int nTLevel = a.nTSubLevel;
                        //classData.LevelName = value.LevelName;
                    }
                }

                var newList2 = classDataList.OrderBy(x => x.sortValue).ThenBy(x => x.fullName).ToList();
                int finalcount = 1000;
                foreach (var a in newList2)
                {
                    a.finalValue = finalcount;
                    finalcount = finalcount + 1000;
                }

                foreach (var a in TTermSubLevel2.Where(w => w.nWorkingStatus != null && w.nTermSubLevel2Status == "1" && w.nTermSubLevel2Status == "1"))
                {
                    classData = new classData();

                    var sublv = TSubLevel.Where(w => w.nTSubLevel == a.nTSubLevel).FirstOrDefault();
                    classData.fullName = sublv.fullName + " / " + a.nTSubLevel2;
                    classData.NickName = sublv.SubLevel.Trim() + " / " + a.nTSubLevel2;
                    if (sublv.SubLevelEN != null)
                        classData.NickNameEN = sublv.SubLevelEN.Trim() + " / " + a.nTSubLevel2;
                    if (sublv.fullNameEN != null)
                        classData.fullNameEN = sublv.fullNameEN + " / " + a.nTSubLevel2;
                    classData.nTLevel = sublv.nTLevel;
                    classData.status = a.nWorkingStatus;
                    classData.type = "termsublevel";
                    classData.id = a.nTermSubLevel2;
                    classData.SchoolID = userData.CompanyID;
                    var findvalue = newList2.Where(w => w.id == a.nTSubLevel).FirstOrDefault();
                    if (findvalue == null) continue;
                    valuecounter = findvalue.finalValue;


                    int n;
                    bool isNumeric = int.TryParse(a.nTSubLevel2, out n);
                    if (isNumeric == true)
                        classData.finalValue = valuecounter + Int32.Parse(a.nTSubLevel2);
                    else classData.finalValue = valuecounter + 500;

                    var value = (from a1 in TSubLevel.Where(w => w.nTSubLevel == a.nTSubLevel)
                                 join b1 in TLevel on a1.nTLevel equals b1.LevelID
                                 select b1
                                 ).FirstOrDefault();

                    if (value != null)
                    {
                        classData.LevelName = value.LevelName;
                    }

                    classDataList.Add(classData);
                }

                var newList3 = classDataList.OrderBy(x => x.finalValue).ThenBy(x => x.fullName).ToList();

                foreach (var b in newList3)
                {
                    b.number = counter;
                    counter = counter + 1;
                }

                return newList3;

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
        protected class classData
        {
            public int number { get; set; }
            public string fullName { get; set; }
            public string NickName { get; set; }
            public string fullNameEN { get; set; }
            public string NickNameEN { get; set; }
            public int? sortValue { get; set; }
            public int? finalValue { get; set; }
            public int? status { get; set; }
            public int? nTLevel { get; set; }
            public string type { get; set; }
            public int id { get; set; }
            public int SchoolID { get; set; }
            public string LevelName { get; set; }
        }

        protected class classValue
        {
            public int nTSubLevel { get; set; }
            public int? value { get; set; }

        }
    }
}