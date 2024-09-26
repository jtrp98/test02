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


namespace FingerprintPayment
{
    public partial class reportLeave : System.Web.UI.Page
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
                btnExport.Click += new EventHandler(ExportToExcel);
                //Button1.Click += new EventHandler(Button1_Click);            
                if (!IsPostBack)
                {

                    int sEmpID = int.Parse(Session["sEmpID"] + "");
                    string yearr = Request.QueryString["year"];
                    string status = Request.QueryString["status"];
                    string start = Request.QueryString["start"];
                    string end = Request.QueryString["end"];
                    string job = Request.QueryString["job"];
                    string name = Request.QueryString["name"];

                    if (job != null)
                        Opendata();

                    DropDownList1.SelectedValue = yearr;
                    ddlstatus.SelectedValue = status;
                    startDay.Text = start;
                    endDay.Text = end;
                    ddlJob.SelectedValue = job;


                    string idlv = Request.QueryString["idlv"];

                    int now = DateTime.Now.Year;
                    if (now < 2500) now = now + 543;
                    int check = 0;
                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                        if (a.numberYear == now)
                            check = 1;
                    }

                    if (check == 0)
                    {
                        ty = new TYear();
                        ty.numberYear = now;
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


        private void Opendata()
        {

            dgd.DataSource = returnlist();
            dgd.PageSize = 30000;
            dgd.DataBind();
        }


        protected List<leavelist> returnlist()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(),ConnectionDB.Read)))
            {
                string job = Request.QueryString["job"];
                string name = Request.QueryString["name"];
                string start = Request.QueryString["start"];
                string end = Request.QueryString["end"];
                string year = Request.QueryString["year"];
                string status = Request.QueryString["status"];




                if (start == "")
                    start = "01/01/" + year;
                if (end == "")
                    end = "31/12/" + year;

                DateTime endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("th-th"));
                DateTime startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("th-th"));

                if (endDate.Year > 2500) endDate = endDate.AddYears(-543);
                if (startDate.Year > 2500) startDate = startDate.AddYears(-543);
                List<leavelist> leave_list = new List<leavelist>();
                leavelist leave = new leavelist();

                List<daycount> daycount_list = new List<daycount>();
                daycount daycount = new daycount();


                var db_leave = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.startDate >= startDate && w.endDate <= endDate && (w.writerJob == "1" || w.writerJob == "0") && w.cDel == false).ToList();

                db_leave = db_leave.OrderByDescending(w => w.letterId).ToList();

                string SQL = @"

DECLARE @SCHOOLID INT = {2}
DECLARE @startDate DATE =  '{0:yyyy-MM-dd}'
DECLARE @endDate DATE = '{1:yyyy-MM-dd}'


SELECT startDate,endDate,
CASE WHEN startDate = endDate AND Season != -1 THEN 0.5
ELSE CONVERT(float,(endDate - startDate)+1) END DayCount,
ISNULL(B.sName,C.sName) AS sName,ISNULL(B.sLastname,C.sLastname) AS sLastname,
A.writerId AS sID,A.LetterConfirmdate,A.letterStatus,adminOneComfirm,adminTwoComfirm,adminThreeComfirm,
letterType,writerJob,Season,letterDate,letterId , A.LeaveTypeID
FROM TLeaveLetter AS A
LEFT JOIN TUser AS B 
ON A.SchoolID = B.SchoolID AND A.writerId = B.sID AND A.writerJob = '0'

LEFT JOIN TEmployees AS C 
ON A.SchoolID = C.SchoolID AND A.writerId = C.sEmp AND A.writerJob = '1'

WHERE A.schoolid = @SCHOOLID
AND 
( 
    @startDate BETWEEN startDate AND endDate  OR  
    @endDate BETWEEN startDate AND endDate OR  
    startDate BETWEEN  @startDate  AND  @endDate OR
    endDate BETWEEN  @startDate  AND  @endDate
) 
AND ISNULL(A.cDel,0) = 0
";
                if (!string.IsNullOrEmpty(name))
                {
                    SQL += $" AND REPLACE(ISNULL(B.sName,C.sName) + ISNULL(B.sLastname,C.sLastname),' ','') like '%' + REPLACE('{name}', ' ', '') + '%' ";
                }

                var q = _db.Database.SqlQuery<ILeaveLetters>(string.Format(SQL, startDate, endDate, userData.CompanyID)).ToList();

                foreach (var a in q.OrderBy(o => o.letterId))
                {
                    leave = new leavelist();

                    leave.letterDate = a.letterDate.Value.ToString("dd/MM/yyyy HH:mm:ss");
                    //leave.letterDate = leave.letterDate.Substring(0, leave.letterDate.Length - 11);
                    leave.letterId = a.letterId;
                    leave.sID = a.sID;

                    if (a.LetterConfirmdate == null)
                        leave.letterStatus = "รอการอนุมัติ";
                    else if (a.letterStatus == "Canceled")
                    {
                        leave.letterStatus = "ยกเลิกการลา";
                    }
                    else
                    {
                        if (a.adminOneComfirm == "reject" || a.adminTwoComfirm == "reject" || a.adminThreeComfirm == "reject" ||
                            a.adminOneComfirm == "0" || a.adminTwoComfirm == "0" || a.adminThreeComfirm == "0")
                            leave.letterStatus = "ไม่อนุมัติ";
                        else leave.letterStatus = "อนุมัติ";
                    }

                    if (a.letterType == "0" || a.letterType == "ป่วย" || a.letterType == "ลาป่วย")
                        leave.letterType = "ลาป่วย";
                    else if (a.letterType == "1" || a.letterType == "ลากิจ" || a.letterType == "กิจส่วนตัว")
                        leave.letterType = "ลากิจ";
                    else if (a.letterType == "2" || a.letterType == "ลาคลอด" || a.letterType == "คลอดบุตร")
                        leave.letterType = "ลาคลอด";
                    else if (a.letterType == "3") leave.letterType = "ลาพักร้อน";
                    else if (a.letterType == "4") leave.letterType = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจย์";
                    else if (a.letterType == "5") leave.letterType = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล";
                    else if (a.letterType == "6") leave.letterType = "ลาไปศึกษา ฝึกอบรม ปฏิบัติการวิจัยหรือดูงาน";
                    else if (a.letterType == "7") leave.letterType = "ลาไปราชการ";
                    else
                    {
                        var leaveType = _db.TLeave_Type.FirstOrDefault(o => o.SchoolID == userData.CompanyID && o.TypeID == a.LeaveTypeID);
                        leave.letterType = leaveType?.TypeName ?? "อื่นๆ";
                    }

                    if (a.writerJob == "0")
                    {
                        //var _data = leaveLetters.Where(w => w.sID == a.writerId).FirstOrDefault();
                        leave.writerName = a.sName + " " + a.sLastname;
                        leave.writerJob = "นักเรียน";
                    }
                    else if (a.writerJob == "1")
                    {
                        leave.writerName = a.sName + " " + a.sLastname;
                        leave.writerJob = "อาจารย์/พนักงาน";
                    }

                    var countletter = leave_list.Where(w => w.sID == a.sID).OrderByDescending(o => o.letterId).FirstOrDefault();

                    Double sickCounter = 0;
                    Double businessCounter = 0;
                    Double sonCounter = 0;
                    Double otherCounter = 0;
                    Double leaveTotal = 0;

                    if (a.letterStatus.ToLower().Trim() == "accept")
                    {
                        if (countletter != null)
                        {

                            sickCounter = countletter.leaveSick;
                            businessCounter = countletter.leaveBusiness;
                            sonCounter = countletter.leaveSon;
                            otherCounter = countletter.leaveOther;

                            leaveTotal = countletter.leaveTotal;
                        }

                        if (a.letterType == "ป่วย" || a.letterType == "ลาป่วย" || a.letterType == "0")
                            sickCounter += a.DayCount;
                        else if (a.letterType == "กิจส่วนตัว" || a.letterType == "ลากิจ" || a.letterType == "1")
                            businessCounter += a.DayCount;
                        else if (a.letterType == "คลอดบุตร" || a.letterType == "ลาคลอด" || a.letterType == "2")
                            sonCounter += a.DayCount;
                        else otherCounter += a.DayCount;

                        leaveTotal += a.DayCount;
                    }
                    else
                    {
                        if (countletter != null)
                        {
                            sickCounter = countletter.leaveSick;
                            businessCounter = countletter.leaveBusiness;
                            sonCounter = countletter.leaveSon;
                            otherCounter = countletter.leaveOther;

                            leaveTotal = 0;
                        }
                        else
                        {
                            leaveTotal = 0;
                        }
                    }

                    leave.leaveSick = sickCounter;
                    leave.leaveBusiness = businessCounter;
                    leave.leaveSon = sonCounter;
                    leave.leaveOther = otherCounter;

                    leave.leaveTotal = leaveTotal;

                    leave.leaveDay = a.DayCount;
                    leave_list.Add(leave);
                }

                if (status == "reject")
                    leave_list = leave_list.Where(w => w.letterStatus == "ไม่อนุมัติ").ToList();
                else if (status == "accept")
                    leave_list = leave_list.Where(w => w.letterStatus == "อนุมัติ").ToList();
                else if (status == "wait")
                    leave_list = leave_list.Where(w => w.letterStatus == "รอการอนุมัติ").ToList();

                if (job == "teacher")
                    leave_list = leave_list.Where(w => w.writerJob == "อาจารย์/พนักงาน").ToList();
                else if (job == "student")
                    leave_list = leave_list.Where(w => w.writerJob == "นักเรียน").ToList();

                //if (name != "")
                //    leave_list = leave_list.Where(w => w.writerName.Contains(name)).ToList();


                var newlist = leave_list.OrderByDescending(w => w.letterId).ToList();


                int count = 1;
                foreach (var a in newlist)
                {
                    a.number = count;
                    count++;
                }

                return newlist;

            }
        }

        protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 2, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "Department";
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Employee";
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.ColumnSpan = 2;
                HeaderCell.BackColor = Color.White;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Employee";
                HeaderCell.ColumnSpan = 2;
                HeaderGridRow.Cells.Add(HeaderCell);

                dgd.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }
        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=LeaveReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;

                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    string job = Request.QueryString["job"];
                    string name = Request.QueryString["name"];
                    string start = Request.QueryString["start"];
                    string end = Request.QueryString["end"];
                    string year = Request.QueryString["year"];
                    string status = Request.QueryString["status"];

                    DateTime endDate = new DateTime();
                    DateTime startDate = new DateTime();
                    if (start == null)
                        start = "01/01/3018";
                    if (end == null)
                        end = "02/02/3018";
                    if (start == "")
                        start = "01/01/" + year;
                    if (end == "")
                        end = "31/12/" + year;

                    if (DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);



                    if (DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                    if (job != null && job != "")
                    {
                        GridView1.DataSource = returnlist();
                    }
                    GridView1.PageSize = 999;
                    GridView1.DataBind();



                    GridView1.RenderControl(hw);


                    //style to format numbers to string
                    string style = @"<style> .big { text-align: center;\@; }</style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected class daycount
        {
            public Double totalLeave { get; set; }
            public string writerJob { get; set; }
            public int? writerID { get; set; }
            public Double leaveSick { get; set; }
            public Double leaveBusiness { get; set; }
            public Double leaveSon { get; set; }
            public Double leaveOther { get; set; }
            public string letterStatus { get; set; }

        }

        public class TR_LeaveLetters
        {
            public int sID { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
        }

        public class ILeaveLetters
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public string sName { get; set; }
            public string sLastname { get; set; }
            public int? sID { get; set; }
            public DateTime? LetterConfirmdate { get; set; }
            public string letterStatus { get; set; }
            public string adminOneComfirm { get; set; }
            public string adminTwoComfirm { get; set; }
            public string adminThreeComfirm { get; set; }
            public string letterType { get; set; }
            public string writerJob { get; set; }
            public int Season { get; set; }
            public DateTime? letterDate { get; set; }
            public int letterId { get; set; }
            public Double DayCount { get; set; }
            public int? LeaveTypeID { get; set; }
        }

        protected class leavelist
        {
            public int number { get; set; }
            public int letterId { get; set; }
            public string writerJob { get; set; }
            public string letterType { get; set; }
            public string writerName { get; set; }
            public string letterDate { get; set; }
            public Double leaveDay { get; set; }
            public Double leaveSick { get; set; }
            public Double leaveBusiness { get; set; }
            public string letterStatus { get; set; }
            public Double leaveSon { get; set; }
            public Double leaveOther { get; set; }
            public Double leaveTotal { get; set; }
            public int? sID { get; set; }
        }

    }
}