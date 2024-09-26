using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using System.Globalization;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using OfficeOpenXml.ConditionalFormatting;

namespace FingerprintPayment
{
    public partial class leaveListTeacher : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (!IsPostBack)
                {
                    //Opendata();
                    int sEmpID = int.Parse(Session["sEmpID"] + "");

                    List<TYear> newList = new List<TYear>();
                    var f_year = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderBy(o => o.numberYear).FirstOrDefault();
                    for (int i = 0; f_year.numberYear.Value <= DateTime.Today.AddYears(i).Year + 543; i--)
                    {
                        newList.Add(new TYear
                        {
                            numberYear = DateTime.Today.AddYears(i).Year + 543
                        });
                    }

                    string yearr = Request.QueryString["year"];
                    string status = Request.QueryString["status"];
                    string start = Request.QueryString["start"];
                    string end = Request.QueryString["end"];
                    string job = Request.QueryString["job"];
                    string name = Request.QueryString["name"];

                    DropDownList1.SelectedValue = yearr;
                    ddlstatus.SelectedValue = status;
                    startDay.Text = start;
                    endDay.Text = end;
                    ddlJob.SelectedValue = job;

                    DropDownList1.DataSource = newList;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();

                    //dgd.Columns[5].Visible = fcommon.SettingPermission(sEmpID, 10);
                    //dgd.Columns[4].Visible = fcommon.SettingPermission(sEmpID, 10);
                }
            }
        }


        //private void Opendata()
        //{

        //    dgd.DataSource = returnlist();
        //    dgd.DataBind();
        //}

        //protected void CustomersGridView_DataBound(Object sender, EventArgs e)
        //{

        //    // Retrieve the pager row.
        //    GridViewRow pagerRow = dgd.BottomPagerRow;
        //    if (pagerRow != null)
        //    {
        //        // Retrieve the DropDownList and Label controls from the row.
        //        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        //        DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
        //        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        //        int num = 100;
        //        int num2 = 300;
        //        int num3 = 500;
        //        ListItem item2 = new ListItem(num.ToString());
        //        pageList2.Items.Add(item2);
        //        if (num == dgd.PageSize)
        //        {
        //            item2.Selected = true;
        //        }
        //        ListItem item3 = new ListItem(num2.ToString());
        //        pageList2.Items.Add(item3);
        //        if (num2 == dgd.PageSize)
        //        {
        //            item3.Selected = true;
        //        }
        //        ListItem item4 = new ListItem(num3.ToString());
        //        pageList2.Items.Add(item4);
        //        if (num3 == dgd.PageSize)
        //        {
        //            item4.Selected = true;
        //        }
        //        if (pageList != null)
        //        {

        //            // Create the values for the DropDownList control based on 
        //            // the  total number of pages required to display the data
        //            // source.
        //            for (int i = 0; i < dgd.PageCount; i++)
        //            {

        //                // Create a ListItem object to represent a page.
        //                int pageNumber = i + 1;
        //                ListItem item = new ListItem(pageNumber.ToString());

        //                // If the ListItem object matches the currently selected
        //                // page, flag the ListItem object as being selected. Because
        //                // the DropDownList control is recreated each time the pager
        //                // row gets created, this will persist the selected item in
        //                // the DropDownList control.   
        //                if (i == dgd.PageIndex)
        //                {
        //                    item.Selected = true;
        //                }

        //                // Add the ListItem object to the Items collection of the 
        //                // DropDownList.
        //                pageList.Items.Add(item);

        //            }

        //        }

        //        if (pageLabel != null)
        //        {

        //            // Calculate the current page number.
        //            int currentPage = dgd.PageIndex + 1;

        //            // Update the Label control with the current page information.
        //            pageLabel.Text = "หน้าที่ " + currentPage.ToString() +
        //              " จากทั้งหมด " + dgd.PageCount.ToString() + " หน้า ";

        //        }
        //    }
        //}


//        [WebMethod(EnableSession = true)]
//        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
//        public static object LoadLeaveLetter()
//        {
//            JWTToken token = new JWTToken();
//            var userData = new JWTToken().UserData;
//            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

//            var jsonStream = "";
//            HttpContext.Current.Request.InputStream.Position = 0;
//            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
//            {
//                jsonStream = inputStream.ReadToEnd();
//            }
//            var serializer = new JavaScriptSerializer();
//            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

//            int draw = Convert.ToInt32(jsonObject["draw"]);
//            int pageIndex = Convert.ToInt32(jsonObject["page"]);
//            int pageSize = Convert.ToInt32(jsonObject["length"]);
//            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
//            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

//            string sortBy = "letterId";
//            switch (sortIndex)
//            {
//                case "1": sortBy = "letterType"; break;
//                case "2": sortBy = "date"; break;
//                case "3": sortBy = "dateStartLeave"; break;
//                case "4": sortBy = "writerName"; break;
//                case "5": sortBy = "homeRoomTeacher"; break;
//                case "6": sortBy = "letterStatus"; break;
//            }
//            if (sortBy == "letterId")
//            {
//                sortBy = "letterId DESC";
//            }
//            else
//            {
//                sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());
//            }

//            //
//            string jobtype = Convert.ToString(jsonObject["jobtype"]);
//            string username = Convert.ToString(jsonObject["username"]);
//            string daystart = Convert.ToString(jsonObject["daystart"]);
//            string dayend = Convert.ToString(jsonObject["dayend"]);
//            string year = Convert.ToString(jsonObject["year"]);
//            string status = Convert.ToString(jsonObject["status"]);

//            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
//            {
//                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
//                {
//                    int schoolID = userData.CompanyID;

//                    int lowerBound = (pageIndex * pageSize) + 1;
//                    int upperBound = lowerBound + pageSize;

//                    string sqlCondition = "";
//                    string sqlCondition2 = "";
//                    if (jobtype == "student")
//                    {
//                        sqlCondition += " AND writerJob IN ('0', N'นักเรียน')";
//                    }
//                    else if (jobtype == "teacher")
//                    {
//                        sqlCondition += " AND writerJob NOT IN ('0', N'นักเรียน')";
//                    }

//                    if (!string.IsNullOrEmpty(username))
//                    {
//                        sqlCondition2 += string.Format(@" AND (ISNULL(u.sName, 0)+' '+ISNULL(u.sLastname, 0) = N'{0}' OR ISNULL(e2.sName, 0)+' '+ISNULL(e2.sLastname, 0) = N'{0}')", username);
//                    }

//                    if (!string.IsNullOrEmpty(daystart) && !string.IsNullOrEmpty(dayend))
//                    {
//                        DateTime? _daystart = DateTime.ParseExact(daystart, "dd/MM/yyyy", new CultureInfo("en-us"));
//                        DateTime? _dayend = DateTime.ParseExact(dayend, "dd/MM/yyyy", new CultureInfo("en-us"));
//                        sqlCondition += string.Format(" AND (startDate BETWEEN '{0:MM/dd/yyyy}' AND '{1:MM/dd/yyyy 23:59:59}' OR endDate BETWEEN '{0:MM/dd/yyyy}' AND '{1:MM/dd/yyyy 23:59:59}')", _daystart, _dayend);
//                    }
//                    else if (!string.IsNullOrEmpty(daystart))
//                    {
//                        DateTime? _daystart = DateTime.ParseExact(daystart, "dd/MM/yyyy", new CultureInfo("en-us"));
//                        sqlCondition += string.Format(" AND startDate BETWEEN '{0:MM/dd/yyyy}' AND '{0:MM/dd/yyyy 23:59:59}'", _daystart);
//                    }
//                    else if (!string.IsNullOrEmpty(dayend))
//                    {
//                        DateTime? _dayend = DateTime.ParseExact(dayend, "dd/MM/yyyy", new CultureInfo("en-us"));
//                        sqlCondition += string.Format(" AND startDate BETWEEN '{0:MM/dd/yyyy}' AND '{0:MM/dd/yyyy 23:59:59}'", _dayend);
//                    }
//                    else if (!string.IsNullOrEmpty(year))
//                    {
//                        sqlCondition += string.Format(" AND (startDate BETWEEN '01/01/{0}' AND '12/31/{0}' OR endDate BETWEEN '01/01/{0}' AND '12/31/{0}')", int.Parse(year) - 543);
//                    }

//                    switch (status)
//                    {
//                        case "reject": sqlCondition2 += " AND letterStatus IN ('reject')"; break;
//                        case "accept": sqlCondition2 += " AND letterStatus IN ('accept')"; break;
//                        case "wait": sqlCondition2 += " AND letterStatus IN ('wait')"; break;
//                        default: break;
//                    }

//                    string sqlQueryFilter = string.Format(@"
//SELECT A.*
//FROM 
//(
//	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
//    FROM (
//        SELECT letterId, 
//        (CASE 
//	        WHEN letterType IN ('0','ป่วย','ลาป่วย') THEN 'ลาป่วย'
//	        WHEN letterType IN ('1','ลากิจ','กิจส่วนตัว') THEN 'ลากิจ'
//	        WHEN letterType IN ('2','ลาคลอด','คลอดบุตร') THEN 'ลาคลอด'
//	        WHEN letterType IN ('3') THEN 'ลาพักร้อน'
//	        WHEN letterType IN ('4') THEN 'ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์'
//	        WHEN letterType IN ('5') THEN 'ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล'
//	        WHEN letterType IN ('6') THEN 'ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัยหรือดูงาน'
//	        WHEN letterType IN ('7') THEN 'ลาไปราชการ'
//	        ELSE L.TypeName --'อื่นๆ' 
//         END) 'letterType',
//		 FORMAT(letterDate, 'dd/MM/yyyy HH:mm:ss น.', 'th-th') 'date', FORMAT(startDate, 'dd/MM/yyyy', 'th-th') 'dateStartLeave',
//        (CASE 
//	        WHEN writerJob = '0' AND sv.sID IS NULL THEN u.sName+' '+u.sLastname 
//	        WHEN writerJob = '0' AND sv.sID IS NOT NULL THEN sv.sStudentID+' '+u.sName+' '+u.sLastname+' '+sv.SubLevel+' / '+sv.nTSubLevel2
//	        ELSE e2.sName + ' ' + e2.sLastname 
//         END) 'writerName',
//        (CASE WHEN writerJob = '0' THEN e1.sName+' '+e1.sLastname ELSE e3.sName+' '+e3.sLastname END) 'homeRoomTeacher',
//        (CASE 
//	        WHEN letterStatus = 'wait' OR letterStatus = 'pending'  THEN 'รอการอนุมัติ'
//	        WHEN letterStatus = 'reject' THEN 'ไม่อนุมัติ'
//	        WHEN letterStatus = 'cancel' THEN 'ยกเลิกการลา'
//	        WHEN letterStatus = 'accept' THEN 'อนุมัติ' 	        
//            WHEN letterStatus = 'requestcancel' THEN 'รออนุญาตยกเลิก' 

//         END) 'letterStatus',
//        (CASE WHEN writerJob = '0' THEN 'นักเรียน' ELSE 'อาจารย์/พนักงาน' END) 'writerJob'
//        FROM
//        (
//	        SELECT A.SchoolID, letterId, letterDate, startDate, writerJob, letterType, writerId, B.TypeName,
//	        (CASE 
//		        WHEN letterStatus IN ('DeletedByUser','Canceled') THEN 'cancel'
//		        WHEN adminOneComfirm IN ('0','reject') OR adminTwoComfirm IN ('0','reject') OR adminThreeComfirm IN ('0','reject') THEN 'reject'
//		        WHEN LetterConfirmdate IS NULL THEN 'wait'
//                WHEN letterStatus = 'RequestCancel' THEN 'requestcancel'
//                ELSE 'accept' END) 'letterStatus' 
//	        FROM JabjaiSchoolSingleDB.dbo.TLeaveLetter A
//            LEFT JOIN JabjaiSchoolSingleDB.dbo.TLeave_Type B ON A.SchoolID = B.SchoolID AND B.TypeID = A.LeaveTypeID
//	        WHERE A.SchoolID={1} AND ISNULL(cDel, 0)=0 {2} 
//        ) L
//        LEFT JOIN TUser u ON L.SchoolID = u.SchoolID AND L.writerId = u.sID
//        LEFT JOIN TB_StudentViews sv ON L.SchoolID = sv.SchoolID AND L.writerId = sv.sID AND L.startDate BETWEEN sv.dStart AND sv.dEnd
//        LEFT JOIN TClassMember cm ON sv.SchoolID = cm.SchoolID AND sv.nTerm = cm.nTerm AND sv.nTermSubLevel2 = cm.nTermSubLevel2
//        LEFT JOIN TEmployees e1 ON cm.SchoolID = e1.SchoolID AND cm.nTeacherHeadid = e1.sEmp
//        LEFT JOIN TEmployees e2 ON L.SchoolID = e2.SchoolID AND L.writerId = e2.sEmp
//        LEFT JOIN TDepartment d ON e2.SchoolID = d.SchoolID AND e2.nDepartmentId = d.DepID
//        LEFT JOIN TEmployees e3 ON d.SchoolID = e3.SchoolID AND d.userHeadId = e3.sEmp
//        WHERE 1=1 {5}
//    ) AS T
//) AS A
//WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound, sqlCondition2);

//                    string sqlQueryCount = string.Format(@"
//SELECT COUNT(*) 
//FROM
//(
//	SELECT SchoolID, letterId, letterDate, startDate, writerJob, letterType, writerId,
//	(CASE 
//		WHEN letterStatus IN ('DeletedByUser','Canceled') THEN 'cancel'
//		WHEN adminOneComfirm IN ('0','reject') OR adminTwoComfirm IN ('0','reject') OR adminThreeComfirm IN ('0','reject') THEN 'reject'
//		WHEN LetterConfirmdate IS NULL THEN 'wait'
//	    WHEN letterStatus = 'RequestCancel' THEN 'requestcancel'
//		ELSE 'accept' END) 'letterStatus' 
//	FROM TLeaveLetter 
//	WHERE SchoolID={0} AND ISNULL(cDel, 0)=0 {1} 
//) L
//LEFT JOIN TUser u ON L.SchoolID = u.SchoolID AND L.writerId = u.sID
//LEFT JOIN TB_StudentViews sv ON L.SchoolID = sv.SchoolID AND L.writerId = sv.sID AND L.startDate BETWEEN sv.dStart AND sv.dEnd
//LEFT JOIN TClassMember cm ON sv.SchoolID = cm.SchoolID AND sv.nTerm = cm.nTerm AND sv.nTermSubLevel2 = cm.nTermSubLevel2
//LEFT JOIN TEmployees e1 ON cm.SchoolID = e1.SchoolID AND cm.nTeacherHeadid = e1.sEmp
//LEFT JOIN TEmployees e2 ON L.SchoolID = e2.SchoolID AND L.writerId = e2.sEmp
//LEFT JOIN TDepartment d ON e2.SchoolID = d.SchoolID AND e2.nDepartmentId = d.DepID
//LEFT JOIN TEmployees e3 ON d.SchoolID = e3.SchoolID AND d.userHeadId = e3.sEmp
//WHERE 1=1 {2}", schoolID, sqlCondition, sqlCondition2);

//                    //var json = QueryEngine.LoadStudentJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, year, term, level, className, stdName);

//                    int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
//                    var resultFilter = en.Database.SqlQuery<leavelist>(sqlQueryFilter).ToList();

//                    int totalData = resultCount;
//                    int filterData = resultFilter.Count();

//                    CollectionData<leavelist> data = new CollectionData<leavelist>();
//                    data.draw = draw;
//                    data.pageIndex = pageIndex;
//                    data.pageSize = pageSize;
//                    data.pageCount = (totalData / pageSize) + 1;
//                    data.recordsTotal = totalData;
//                    data.recordsFiltered = filterData;
//                    data.data = resultFilter;

//                    var json = JsonConvert.SerializeObject(data);

//                    return json;
//                }
//            }
//        }

//        protected List<leavelist> returnlist()
//        {
//            JWTToken token = new JWTToken();
//            var userData = new JWTToken().UserData;
//            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

//            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
//            {
//                string job = Request.QueryString["job"];
//                string name = Request.QueryString["name"];
//                string start = Request.QueryString["start"];
//                string end = Request.QueryString["end"];
//                string year = Request.QueryString["year"];
//                string status = Request.QueryString["status"];

//                if (start == "")
//                    start = "01/01/" + year;
//                if (end == "")
//                    end = "31/12/" + year;

//                string thismonth = DateTime.Now.Month.ToString();
//                if (thismonth.Length == 1)
//                    thismonth = "0" + thismonth;

//                if (start == null)
//                    start = "01/01" + "/" + DateTime.Now.Year;
//                if (end == null)
//                    end = "31/12" + "/" + DateTime.Now.Year;

//                DateTime endDate = DateTime.ParseExact(end, "dd/MM/yyyy", CultureInfo.InvariantCulture);
//                DateTime startDate = DateTime.ParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture);

//                if (endDate.Year > 2500) endDate = endDate.AddYears(-543);
//                if (startDate.Year > 2500) startDate = startDate.AddYears(-543);
//                List<leavelist> leave_list = new List<leavelist>();
//                leavelist leave = new leavelist();




//                //var db_leave = _db.TLeaveLetters.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.startDate >= startDate && w.endDate <= endDate && (w.writerJob == "1" || w.writerJob == "0")).ToList();
//                var db_user = (from a in _db.TUser.Where(w => w.SchoolID == userData.CompanyID)
//                               join b in _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sID equals b.writerId
//                               join c in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals c.sID into jac
//                               from jc in jac.Where(w => w.dStart <= b.startDate && w.dEnd >= b.startDate).DefaultIfEmpty()

//                               where b.writerJob == "0"
//                               select new
//                               {
//                                   a.SchoolID,
//                                   sName = a.sStudentID + " " + a.sName,
//                                   sLastname = a.sLastname + (jc == null ? "" : " " + jc.SubLevel + " / " + jc.nTSubLevel2),
//                                   a.sID,
//                                   a.nTermSubLevel2,
//                                   b.letterId
//                               }).AsQueryable().ToList();

//                var db_classmember = (from a in _db.TUser.Where(w => w.SchoolID == userData.CompanyID)
//                                          //join b in _db.TLeaveLetters.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sID equals b.writerId
//                                      join c in _db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals c.nTermSubLevel2
//                                      join d in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on c.nTeacherHeadid equals d.sEmp
//                                      //where b.writerJob == "0"
//                                      select new
//                                      {
//                                          a.SchoolID,
//                                          d.sName,
//                                          d.sLastname,
//                                          a.nTermSubLevel2,
//                                          c.nTerm
//                                      }).AsQueryable().ToList();

//                var db_emp = (from a in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
//                              join b in _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sEmp equals b.writerId
//                              where b.writerJob == "1"
//                              select new
//                              {
//                                  a.SchoolID,
//                                  a.sName,
//                                  a.sLastname,
//                                  a.sEmp
//                              }).AsQueryable().ToList();

//                var db_department = (from a in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
//                                     join b in _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false) on a.sEmp equals b.writerId
//                                     join c in _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID) on a.nDepartmentId equals c.DepID
//                                     join d in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on c.userHeadId equals d.sEmp
//                                     where b.writerJob == "1"
//                                     select new
//                                     {
//                                         a.SchoolID,
//                                         d.sName,
//                                         d.sLastname,
//                                         a.sEmp,
//                                         b.letterId
//                                     }).AsQueryable().ToList();


//                //db_leave = db_leave.OrderByDescending(w => w.letterId).ToList();
//                var q_term = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID);
//                var leaveLetters = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID && ((startDate <= w.startDate && w.startDate <= endDate) || (startDate <= w.endDate && w.endDate <= endDate) || (startDate <= w.startDate && w.endDate <= endDate) || (w.startDate <= startDate && endDate <= w.endDate)) && (w.deleted != 1 || w.letterStatus == "DeletedByUser") && w.cDel == false).ToList();
//                foreach (var a in leaveLetters)
//                {
//                    leave = new leavelist();

//                    var tmpDateTime = a.letterDate.Value;
//                    leave.date = tmpDateTime.Day.ToString() + "/" + tmpDateTime.Month.ToString() + "/" + (tmpDateTime.Year + 543).ToString() + " " + tmpDateTime.ToString("HH:mm") + " น."; // kong update 9/10/2019

//                    var tmpDateTime1 = a.startDate.Value;
//                    leave.dateStartLeave = tmpDateTime1.Day.ToString() + "/" + tmpDateTime1.Month.ToString() + "/" + (tmpDateTime1.Year + 543).ToString(); // kong update 9/10/2019

//                    leave.letterId = a.letterId;

//                    if (a.LetterConfirmdate == null)
//                        leave.letterStatus = "รอการอนุมัติ";
//                    else
//                    {
//                        if (a.adminOneComfirm == "reject" || a.adminTwoComfirm == "reject" || a.adminThreeComfirm == "reject" ||
//                            a.adminOneComfirm == "0" || a.adminTwoComfirm == "0" || a.adminThreeComfirm == "0")
//                            leave.letterStatus = "ไม่อนุมัติ";
//                        else leave.letterStatus = "อนุมัติ";
//                    }

//                    if (a.letterStatus == "DeletedByUser" || a.letterStatus == "RequestCancel") leave.letterStatus = "ยกเลิกการลา";

//                    if (a.letterType == "0" || a.letterType == "ป่วย" || a.letterType == "ลาป่วย")
//                        leave.letterType = "ลาป่วย";
//                    else if (a.letterType == "1" || a.letterType == "ลากิจ" || a.letterType == "กิจส่วนตัว")
//                        leave.letterType = "ลากิจ";
//                    else if (a.letterType == "2" || a.letterType == "ลาคลอด" || a.letterType == "คลอดบุตร")
//                        leave.letterType = "ลาคลอด";
//                    else if (a.letterType == "3") leave.letterType = "ลาพักร้อน";
//                    else if (a.letterType == "4") leave.letterType = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์";
//                    else if (a.letterType == "5") leave.letterType = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล";
//                    else if (a.letterType == "6") leave.letterType = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัยหรือดูงาน";
//                    else if (a.letterType == "7") leave.letterType = "ลาไปราชการ";
//                    else leave.letterType = "อื่นๆ";


//                    StudentLogic studentLogic = new StudentLogic(_db);
//                    if (a.writerJob == "0")
//                    {
//                        var _data = db_user.Where(w => w.sID == a.writerId && w.letterId == a.letterId).FirstOrDefault();
//                        if (_data == null) continue;
//                        leave.writerName = _data.sName + " " + _data.sLastname;
//                        leave.writerJob = "นักเรียน";
//                        var thisterm = studentLogic.GetTermDATA(a.letterDate ?? DateTime.Now, userData);
//                        //var thisterm =  q_term.Where(w => w.SchoolID == userData.CompanyID && w.dStart <= a.letterDate && w.dEnd >= a.letterDate).FirstOrDefault();

//                        if (thisterm != null)
//                        {
//                            var homeroom = db_classmember.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == _data.nTermSubLevel2 && w.nTerm == thisterm.nTerm).FirstOrDefault();
//                            if (homeroom != null)
//                            {
//                                leave.homeRoomTeacher = homeroom.sName + " " + homeroom.sLastname;
//                            }
//                        }
//                    }
//                    else if (a.writerJob == "1")
//                    {
//                        var _data = db_emp.Where(w => w.sEmp == a.writerId).FirstOrDefault();
//                        leave.writerName = _data.sName + " " + _data.sLastname;
//                        leave.writerJob = "อาจารย์/พนักงาน";
//                        var thisdepartment = db_department.Where(w => w.sEmp == a.writerId).FirstOrDefault();
//                        if (thisdepartment != null)
//                            leave.homeRoomTeacher = thisdepartment.sName + " " + thisdepartment.sLastname;
//                    }

//                    leave_list.Add(leave);
//                }

//                if (status == "reject")
//                    leave_list = leave_list.Where(w => w.letterStatus == "ไม่อนุมัติ").ToList();
//                else if (status == "accept")
//                    leave_list = leave_list.Where(w => w.letterStatus == "อนุมัติ").ToList();
//                else if (status == "wait")
//                    leave_list = leave_list.Where(w => w.letterStatus == "รอการอนุมัติ").ToList();

//                if (job == "teacher")
//                    leave_list = leave_list.Where(w => w.writerJob == "อาจารย์/พนักงาน").ToList();
//                else if (job == "student")
//                    leave_list = leave_list.Where(w => w.writerJob == "นักเรียน").ToList();

//                if (name != null)
//                {
//                    if (name != "")
//                        leave_list = leave_list.Where(w => w.writerName == name).ToList();
//                }

//                var newlist = leave_list.OrderByDescending(w => w.letterId).ToList();

//                int count = 1;
//                foreach (var a in newlist)
//                {
//                    a.number = count;
//                    count++;
//                }

//                return newlist;
//            }
//        }


        //public void nextbutton_Click(Object sender, EventArgs e)
        //{

        //    dgd.DataSource = returnlist();
        //    dgd.PageIndex = dgd.PageIndex + 1;
        //    if (dgd.PageIndex > dgd.PageCount)
        //        dgd.PageIndex = dgd.PageCount;
        //    dgd.DataBind();
        //}



        //public void backbutton_Click(Object sender, EventArgs e)
        //{

        //    dgd.DataSource = returnlist();
        //    if (dgd.PageIndex > 0)
        //        dgd.PageIndex = dgd.PageIndex - 1;
        //    dgd.DataBind();
        //}

        //protected void PageDropDownList_SelectedIndexChanged(Object sender, EventArgs e)
        //{

        //    GridViewRow pagerRow = dgd.BottomPagerRow;
        //    DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        //    dgd.DataSource = returnlist();
        //    dgd.PageIndex = pageList.SelectedIndex;
        //    dgd.DataBind();
        //}

        //protected void PageDropDownList_SelectedIndexChanged2(Object sender, EventArgs e)
        //{

        //    GridViewRow pagerRow = dgd.BottomPagerRow;
        //    DropDownList pageList2 = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList2");
        //    dgd.DataSource = returnlist();
        //    int xxx = Int32.Parse(pageList2.SelectedValue);
        //    dgd.PageSize = xxx;
        //    dgd.PageIndex = 0;
        //    dgd.DataBind();
        //}

        public class leavelist
        {
            public Int64 number { get; set; }
            public string date { get; set; }
            public string dateStartLeave { get; set; }
            public string time { get; set; }
            public int letterId { get; set; }    
            public string writerJob { get; set; }
            public string letterType { get; set; }
            public string writerName { get; set; }
            public string homeRoomTeacher { get; set; }
            public string letterStatus { get; set; }
            public int LeaveTypeID { get; set; }
        }

    }
}