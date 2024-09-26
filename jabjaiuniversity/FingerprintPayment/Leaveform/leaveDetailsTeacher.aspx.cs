using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using System.Web.DynamicData;
using System.Data;
using System.Data.SqlClient;
using FingerprintPayment.Class;
using JabjaiMainClass;
using System.Threading;
using urbanairship;
using static FingerprintPayment.Leaveform.AllLeaveLogic;
using FingerprintPayment.Leaveform;
using System.Globalization;
using FingerprintPayment.Helper;

namespace FingerprintPayment
{
    public partial class leaveDetailsTeacher : System.Web.UI.Page
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
            string adminOneDate = "", adminTwoDate = "", adminThreeDate = "";

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"] + "";
                Button1.Click += new EventHandler(Button1_Click);
                Button2.Click += new EventHandler(Button2_Click);
                int? nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);
                var data1 = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.letterId == id && w.cDel == false).FirstOrDefault();

                foreach (var _dr in _dbMaster.TCompanies.Where(w => w.nCompany == nCompany))
                {
                    labelSchool.Text = _dr.sCompany;
                }

                int? sEmpID = data1.writerId;

                if (data1.writerJob == "นักเรียน" || data1.writerJob == "0")
                {
                    var _data = _db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == sEmpID).FirstOrDefault();
                    labelName.Text = _data.sName + " " + _data.sLastname;

                    StudentLogic studentLogic = new StudentLogic(_db);
                    string termID = studentLogic.GetTermId(data1.letterDate ?? DateTime.Now, userData);
                    var studentClassHistory = studentLogic.GetStudentClassroomHistory(data1.writerId ?? 0, data1.letterDate ?? DateTime.Now, userData);
                    var classMember = _db.TClassMembers.FirstOrDefault(f => f.nTerm == termID && f.nTermSubLevel2 == studentClassHistory.nTermSubLevel2);

                    if (classMember != null)
                    {
                        if (classMember.nTeacherHeadid.HasValue)
                        {
                            adminOneDate = data1.adminOneDate.ToString();
                            if (data1.adminTwoId == classMember.nTeacherHeadid)
                            {
                                adminOneDate = data1.adminTwoDate.ToString();
                            }
                            else if (data1.adminThreeId == classMember.nTeacherHeadid)
                            {
                                adminOneDate = data1.adminThreeDate.ToString();
                            }
                        }

                        if (classMember.nTeacherAssistOne.HasValue)
                        {
                            adminTwoDate = data1.adminTwoDate.ToString();
                            if (data1.adminOneId == classMember.nTeacherAssistOne)
                            {
                                adminTwoDate = data1.adminOneDate.ToString();
                            }
                            else if (data1.adminThreeId == classMember.nTeacherAssistOne)
                            {
                                adminTwoDate = data1.adminThreeDate.ToString();
                            }
                        }

                        if (classMember.nTeacherAssistTwo.HasValue)
                        {
                            adminThreeDate = data1.adminThreeDate.ToString();
                            if (data1.adminOneId == classMember.nTeacherAssistTwo)
                            {
                                adminThreeDate = data1.adminOneDate.ToString();
                            }
                            else if (data1.adminTwoId == classMember.nTeacherAssistTwo)
                            {
                                adminThreeDate = data1.adminTwoDate.ToString();
                            }
                        }

                        data1.adminOneId = classMember.nTeacherHeadid;
                        data1.adminTwoId = classMember.nTeacherAssistOne;
                        data1.adminThreeId = classMember.nTeacherAssistTwo;
                    }

                }
                else
                {
                    var _data = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == sEmpID).FirstOrDefault();
                    labelName.Text = _data.sName + " " + _data.sLastname;
                }


                int sEmpID2 = int.Parse(Session["sEmpID"] + "");

                foreach (var _data2 in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == sEmpID2))
                {
                    registerName.Text = _data2.sName + " " + _data2.sLastname;
                    if (_data2.nJobid != null)
                    {
                        int? v1 = _data2.nJobid;
                        int v2;
                        v2 = v1.GetValueOrDefault();
                        var job = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.nJobid == v2).FirstOrDefault();
                        registerJobHead.Text = job.jobDescription;
                    }
                }

                string date = DateTime.Now.Day.ToString();
                string month = DateTime.Now.Month.ToString();

                DateTime? start = data1.startDate;
                DateTime? end = data1.endDate;
                DateTime? letterDate = data1.letterDate;

                //now use array to get specific date object
                string cutday1 = start.Value.Day.ToString();
                string cutmonth1 = start.Value.Month.ToString();
                string cutyear1 = start.Value.Year.ToString();
                string cutday2 = end.Value.Day.ToString();
                string cutmonth2 = end.Value.Month.ToString();
                string cutyear2 = end.Value.Year.ToString();
                string cutday3 = letterDate.Value.Day.ToString();
                string cutmonth3 = letterDate.Value.Month.ToString();
                string cutyear3 = letterDate.Value.Year.ToString();

                int startYear = Int32.Parse(cutyear1);
                if (startYear < 2550)
                    startYear = startYear + 543;
                int startmonth = Int32.Parse(cutmonth1);
                int startday = Int32.Parse(cutday1);
                int endYear = Int32.Parse(cutyear2);
                if (endYear < 2550)
                    endYear = endYear + 543;
                int endmonth = Int32.Parse(cutmonth2);
                int endday = Int32.Parse(cutday2);
                int letterYear = Int32.Parse(cutyear3);
                if (letterYear < 2550)
                    letterYear = letterYear + 543;
                int lettermonth = Int32.Parse(cutmonth3);
                int letterday = Int32.Parse(cutday3);

                string stmonth = startmonth.ToString();
                string edmonth = endmonth.ToString();
                string ltmonth = lettermonth.ToString();

                if (month == "1")
                    month = "มกราคม";
                else if (month == "2")
                    month = "กุมภาพันธ์";
                else if (month == "3")
                    month = "มีนาคม";
                else if (month == "4")
                    month = "เมษายน";
                else if (month == "5")
                    month = "พฤษภาคม";
                else if (month == "6")
                    month = "มิถุนายน";
                else if (month == "7")
                    month = "กรกฎาคม";
                else if (month == "8")
                    month = "สิงหาคม";
                else if (month == "9")
                    month = "กันยายน";
                else if (month == "10")
                    month = "ตุลาคม";
                else if (month == "11")
                    month = "พฤศจิกายน";
                else if (month == "12")
                    month = "ธันวาคม";

                if (stmonth == "1")
                    stmonth = "มกราคม";
                else if (stmonth == "2")
                    stmonth = "กุมภาพันธ์";
                else if (stmonth == "3")
                    stmonth = "มีนาคม";
                else if (stmonth == "4")
                    stmonth = "เมษายน";
                else if (stmonth == "5")
                    stmonth = "พฤษภาคม";
                else if (stmonth == "6")
                    stmonth = "มิถุนายน";
                else if (stmonth == "7")
                    stmonth = "กรกฎาคม";
                else if (stmonth == "8")
                    stmonth = "สิงหาคม";
                else if (stmonth == "9")
                    stmonth = "กันยายน";
                else if (stmonth == "10")
                    stmonth = "ตุลาคม";
                else if (stmonth == "11")
                    stmonth = "พฤศจิกายน";
                else if (stmonth == "12")
                    stmonth = "ธันวาคม";

                if (edmonth == "1")
                    edmonth = "มกราคม";
                else if (edmonth == "2")
                    edmonth = "กุมภาพันธ์";
                else if (edmonth == "3")
                    edmonth = "มีนาคม";
                else if (edmonth == "4")
                    edmonth = "เมษายน";
                else if (edmonth == "5")
                    edmonth = "พฤษภาคม";
                else if (edmonth == "6")
                    edmonth = "มิถุนายน";
                else if (edmonth == "7")
                    edmonth = "กรกฎาคม";
                else if (edmonth == "8")
                    edmonth = "สิงหาคม";
                else if (edmonth == "9")
                    edmonth = "กันยายน";
                else if (edmonth == "10")
                    edmonth = "ตุลาคม";
                else if (edmonth == "11")
                    edmonth = "พฤศจิกายน";
                else if (edmonth == "12")
                    edmonth = "ธันวาคม";

                if (ltmonth == "1")
                    ltmonth = "มกราคม";
                else if (ltmonth == "2")
                    ltmonth = "กุมภาพันธ์";
                else if (ltmonth == "3")
                    ltmonth = "มีนาคม";
                else if (ltmonth == "4")
                    ltmonth = "เมษายน";
                else if (ltmonth == "5")
                    ltmonth = "พฤษภาคม";
                else if (ltmonth == "6")
                    ltmonth = "มิถุนายน";
                else if (ltmonth == "7")
                    ltmonth = "กรกฎาคม";
                else if (ltmonth == "8")
                    ltmonth = "สิงหาคม";
                else if (ltmonth == "9")
                    ltmonth = "กันยายน";
                else if (ltmonth == "10")
                    ltmonth = "ตุลาคม";
                else if (ltmonth == "11")
                    ltmonth = "พฤศจิกายน";
                else if (ltmonth == "12")
                    ltmonth = "ธันวาคม";

                int y = DateTime.Now.Year;
                if (y < 2550)
                    y = y + 543;
                string year = y.ToString();
                registerDate.Text = date + " " + month + " " + y;
                labelDate.Text = letterday + " " + ltmonth + " " + letterYear;

                //txtheader.Text = data1.letterHeader;
                txtheader.Text = "ขออนุญาตลา";
                if (data1.writerJob != "นักเรียน" && data1.writerJob != "0")
                {
                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data1.writerId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            txtJob.Text = jobdata.jobDescription;
                    }
                }
                else txtJob.Text = "นักเรียน";

                string type = "";
                if (data1.letterType == "0" || data1.letterType == "ป่วย" || data1.letterType == "ลาป่วย")
                    type = "ลาป่วย";
                else if (data1.letterType == "1" || data1.letterType == "ลากิจ" || data1.letterType == "กิจส่วนตัว")
                    type = "ลากิจ";
                else if (data1.letterType == "2" || data1.letterType == "ลาคลอด" || data1.letterType == "คลอดบุตร")
                    type = "ลาคลอด";
                else if (data1.letterType == "3" || data1.letterType == "ลาพักผ่อน" || data1.letterType == "ลาพักร้อน")
                    type = "ลาพักร้อน";
                else if (data1.letterType == "4" || data1.letterType == "ลาอุปสมบท")
                    type = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจญ์";
                else if (data1.letterType == "5" || data1.letterType == "ลาทหาร")
                    type = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล";
                else if (data1.letterType == "6")
                    type = "ลาไปศึกษาฝึกอบรม ปฏิบัติการวิจัยหรือดูงาน";
                else if (data1.letterType == "7" || data1.letterType == "ลาไปราชการ")
                    type = "ลาไปราชการ";
                else
                {
                    var leaveType = _db.TLeave_Type.FirstOrDefault(o => o.SchoolID == userData.CompanyID && o.TypeID == data1.LeaveTypeID);
                    type = leaveType?.TypeName ?? "อื่นๆ";
                    //type = data1.letterType;
                }

                txtType.Text = type;
                txtReason.Text = data1.writerComment;
                txtStartDate.Text = startday + " " + stmonth + " " + startYear;
                txtEndDate.Text = endday + " " + edmonth + " " + endYear;

                TimeSpan? nod = (data1.endDate - data1.startDate);
                double nod4;
                if (data1.endDate == data1.startDate)
                    nod4 = 1;
                else if (data1.endDate < data1.startDate)
                {
                    Response.Write("<script>alert('กรุณาตรวจสอบความถูกต้องของวันที่ลา');</script>");
                    return;
                }
                else
                {
                    nod4 = nod.Value.Days + 1;
                }

                if (data1.Season == 0 || data1.Season == 1) nod4 = 0.5;

                if (data1.Season == 0) txtTimeLeave.Text = "เช้า";
                else if (data1.Season == 1) txtTimeLeave.Text = "บ่าย";
                else if (data1.Season == -1)
                {
                    nod4 = 0;
                    txtTimeLeave.Text = "ทั้งวัน";
                    var user = _dbMaster.TUsers.FirstOrDefault(f => f.sID == data1.writerId);

                    LeaveLogic leaveLogic = new LeaveLogic(_db);
                    nod4 = leaveLogic.CalculaDay(data1.letterId, user);
                }

                txtTotalleave.Text = nod4 + " วัน";

                txtHomenum.Text = data1.contactHomenumber;
                txtRoad.Text = data1.contactRoad;
                txtAumpher.Text = data1.contactAumpher;
                txtPhone.Text = data1.contactPhone;
                txtProvince.Text = data1.contactProvince;
                txtTumbon.Text = data1.contactTumbon;

                int thisYear = letterYear;
                if (thisYear > 2550)
                    thisYear = thisYear - 543;

                DateTime? maxDate = new DateTime(thisYear, 12, 31);
                DateTime? minDate = new DateTime(thisYear, 1, 1);


                int sickCounter = 0;
                double sickDayTotal = 0;
                double sickDayHistory = 0;

                int businessCounter = 0;
                double businessDayTotal = 0;
                double businessDayHistory = 0;

                //int sonCounter = 0;
                //int? sonDayTotal = 0;
                //int? sonDayMaLaew = 0;

                int holidayCounter = 0;
                double holidayTotal = 0;
                double holidayHistory = 0;

                int otherCounter = 0;
                double otherDayTotal = 0;
                double otherDayHistory = 0;

                LeaveCount leave = new LeaveCount();
                AllLeaveLogic allLeaveLogic = new AllLeaveLogic();
                //DateTime date = DateTime.ParseExact(startday + "/" + stmonth + "/" + startYear; "dd/MM/yyyy", new cu)
                leave = allLeaveLogic.Leave(sEmpID.Value, data1.startDate, (data1.writerJob == "นักเรียน" || data1.writerJob == "0") ? 0 : 1, data1);


                sickLeave.Text = leave.sickDayTotal.ToString();
                bLeave.Text = leave.businessDayTotal.ToString();
                gsLeave.Text = leave.governmentServicesTotal.ToString();
                studyLeave.Text = leave.studyTotal.ToString();
                etcLeave.Text = leave.otherDayTotal.ToString();

                var totalLeave1 = leave.sickDayTotal + leave.businessDayTotal + leave.governmentServicesTotal + leave.studyTotal + leave.otherDayTotal;
                totalLeave.Text = totalLeave1.ToString();

                if (data1.adminOneId != null)
                {
                    admin1Date.Text = adminOneDate;

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data1.adminOneId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            admin1Job.Text = jobdata.jobDescription;
                    }

                    foreach (var name in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data1.adminOneId))
                    {
                        admin1Name.Text = name.sName + " " + name.sLastname;
                    }
                    admin1Comment.Text = data1.adminOneComment;
                }

                if (data1.adminTwoId != null)
                {
                    admin2Date.Text = adminTwoDate;

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data1.adminTwoId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID && w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            admin2Job.Text = jobdata.jobDescription;
                    }

                    foreach (var name in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == data1.adminTwoId))
                    {
                        admin2Name.Text = name.sName + " " + name.sLastname;
                    }
                    admin2Comment.Text = data1.adminTwoComment;
                }

                if (data1.adminThreeId != null)
                {
                    admin3Date.Text = adminThreeDate;

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == data1.adminThreeId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            admin3Job.Text = jobdata.jobDescription;
                    }

                    foreach (var name in _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == data1.adminThreeId))
                    {
                        admin3Name.Text = name.sName + " " + name.sLastname;
                    }
                    admin3Comment.Text = data1.adminThreeComment;
                }
            }
        }

        void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = Session["sEntities"] + "";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");

                string comment = "";
                string confirm = "", letterStatus = "";
                if (registerCommentAccept.Checked == true)
                {
                    comment = "อนุมัติ";
                    letterStatus = "Accept";
                    confirm = "1";
                }
                else if (registerCommentReject2.Checked == true)
                {
                    comment = "ไม่อนุมัติ";
                    confirm = "0";
                    letterStatus = "Canceled";
                }
                if (comment == "")
                {
                    Response.Write("<script>alert('กรุณาเลือกความเห็น');</script>");
                    return;
                }

                TLeaveLetter Letter = new TLeaveLetter();

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);

                int sEmpID = int.Parse(Session["sEmpID"] + "");
                var emp = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == sEmpID).FirstOrDefault();

                var d = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.letterId == id && w.cDel == false).FirstOrDefault();
                int? checkerNumber = nCompany.checker;


                if (d.LetterConfirmdate != null)
                {
                    Response.Write("<script>alert('คำร้องนี้ปิดการลงทะเบียนความเห็นแล้ว');</script>");
                    return;
                }
                if (checkerNumber == 1 && d.adminOneId != null)
                {
                    Response.Write("<script>alert('จำนวนความเห็นของผู้ตรวจสอบเต็มแล้ว');</script>");
                    return;
                }
                if (checkerNumber == 2 && d.adminOneId != null && d.adminTwoId != null)
                {
                    Response.Write("<script>alert('จำนวนความเห็นของผู้ตรวจสอบเต็มแล้ว');</script>");
                    return;
                }
                if (checkerNumber == 3 && d.adminOneId != null && d.adminTwoId != null && d.adminThreeId != null)
                {
                    Response.Write("<script>alert('จำนวนความเห็นของผู้ตรวจสอบเต็มแล้ว');</script>");
                    return;
                }

                List<int?> checkApprover = new List<int?>();

                var quserwriter = new MasterEntity.TUser();

                StudentLogic logic = new StudentLogic(_db);
                string termID = logic.GetTermId(d.startDate.Value, userData);

                if (d.writerJob == "นักเรียน" || d.writerJob == "0")
                {
                    var writerUser = _db.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == d.writerId).FirstOrDefault();
                    quserwriter = _dbMaster.TUsers.FirstOrDefault(f => f.nSystemID == d.writerId && f.cType == "0" && f.nCompany == nCompany.nCompany);
                    var classMember = _db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == writerUser.nTermSubLevel2 && w.nTerm == termID).FirstOrDefault();
                    if (classMember != null)
                    {
                        if (classMember.nTeacherHeadid != null) checkApprover.Add(classMember.nTeacherHeadid);
                        if (classMember.nTeacherAssistOne != null) checkApprover.Add(classMember.nTeacherAssistOne);
                        if (classMember.nTeacherAssistTwo != null) checkApprover.Add(classMember.nTeacherAssistTwo);
                    }
                }
                else
                {
                    var writerUser = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == d.writerId).FirstOrDefault();
                    quserwriter = _dbMaster.TUsers.FirstOrDefault(f => f.nSystemID == d.writerId && f.cType == "1" && f.nCompany == nCompany.nCompany);
                    var department = _db.TDepartments.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.DepID == writerUser.nDepartmentId).FirstOrDefault();
                    if (department != null)
                    {
                        if (department.userHeadId != null) checkApprover.Add(department.userHeadId);
                        if (department.userApproveOne != null) checkApprover.Add(department.userApproveOne);
                        if (department.userApproveTwo != null) checkApprover.Add(department.userApproveTwo);
                    }
                }

                int count = 0;
                foreach (int? a in checkApprover)
                {
                    if (emp.sEmp == a)
                        count = count + 1;
                }

                if (count == 0)
                {
                    Response.Write("<script>alert('ไม่สามารถลงทะเบียนความเห็นได้ เนื่องจากผู้ใช้ไม่มีสิทธิในการลงความเห็น');</script>");
                    return;
                }

                var job = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == emp.nJobid).FirstOrDefault();

                if (d.adminOneId == null)
                {
                    if (job != null)
                    {
                        d.adminOneJob = job.jobDescription;
                    }
                    d.adminOneDate = DateTime.Now.FixSecond(1).FixMillisecond(1);
                    d.adminOneComment = comment;
                    d.adminOneId = sEmpID;
                    d.adminOneComfirm = confirm;
                }
                else if (d.adminTwoId == null)
                {
                    if (job != null)
                    {
                        d.adminTwoJob = job.jobDescription;
                    }
                    d.adminTwoDate = DateTime.Now.FixSecond(2).FixMillisecond(1);
                    d.adminTwoComment = comment;
                    d.adminTwoId = sEmpID;
                    d.adminTwoComfirm = confirm;
                }
                else
                {
                    if (job != null)
                    {
                        d.adminThreeJob = job.jobDescription;
                    }
                    d.adminThreeDate = DateTime.Now.FixSecond(3).FixMillisecond(1);
                    d.adminThreeComment = comment;
                    d.adminThreeId = sEmpID;
                    d.adminThreeComfirm = confirm;
                }

                d.LetterConfirmdate = DateTime.Now.FixMillisecond(1);
                d.letterStatus = letterStatus;
                _db.SaveChanges();

                sms(quserwriter.sID, "1", d.letterId);

                var user = _dbMaster.TUsers.FirstOrDefault(f => f.sID == d.writerId);

                LeaveLogic leaveLogic = new LeaveLogic(_db);
                switch (d.letterStatus.ToLower())
                {
                    case "accept":
                        var f_writer = _dbMaster.TUsers.FirstOrDefault(f => f.sID == d.writerId);
                        leaveLogic.ApproveStatus(d.letterId, f_writer);
                        break;
                    case "canceled":
                        //leaveLogic.CancelStatus(d.letterId, d.writerId);
                        break;
                }

                Response.Redirect("leaveList.aspx");
            }
        }

        void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("leaveList.aspx");
        }

        private void sms(int? userid, string approve, int? letterid)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {

                var quser = _dbMaster.TUsers.Where(w => w.sID == userid).FirstOrDefault();
                var qcompany = _dbMaster.TCompanies.FirstOrDefault(f => f.nCompany == quser.nCompany);
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(qcompany.sEntities, ConnectionDB.Read)))
                {

                    var letter = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.letterId == letterid && w.cDel == false).FirstOrDefault();
                    var user = _dbMaster.TUsers.FirstOrDefault(f => f.sID == letter.writerId.Value);

                    LeaveLogic leaveLogic = new LeaveLogic(_db);

                    List<int?> check = new List<int?>();
                    string comment = "";
                    switch (approve)
                    {
                        case "0": comment = "คำร้องไม่ผ่านการอนุมัติ"; break;
                        case "1": comment = "คำร้องผ่านการอนุมัติ"; leaveLogic.ApproveStatus(letter.letterId, user); break;
                        case "3":
                            comment = "คำร้องผ่านการยกเลิกแล้ว";
                            //var f_letter = _db.TLeaveLetters.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.letterId == letterid && f.cDel == false);
                            //if (f_letter.writerJob == "0")
                            //{
                            //    _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.LogDate > DateTime.Today && w.LogDate <= f_letter.endDate && w.sID == f_letter.writerId).ToList()
                            //    .ForEach(f => _db.TLogUserTimeScans.Remove(f));

                            //    _db.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.LogLearnDate >= f_letter.startDate && w.LogLearnDate <= f_letter.endDate && w.sID == f_letter.writerId && w.sUserType == "0")
                            //        .ToList().ForEach(f =>
                            //        {
                            //            _db.TLogLearnTimeScans.Remove(f);
                            //        });

                            //    var f_data = _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(w => w.LogDate == DateTime.Today && w.sID == f_letter.writerId);
                            //    if (f_data != null)
                            //    {
                            //        f_data.bLockStatus = null;
                            //        f_data.LogScanStatus = "99";
                            //    }

                            //    _db.SaveChanges();

                            //}
                            break;
                        case "4": comment = "คำร้องไม่ผ่านการยกเลิก"; break;
                        case "5": comment = "คำร้องถูกยกเลิกแล้วโดยผู้ส่งคำร้อง"; break;
                    }
                    check.Add(userid);

                    var user_messagebox = new List<messagebox.user_messagebox>();

                    user_messagebox.Add(new messagebox.user_messagebox
                    {
                        user_id = userid.Value,
                    });

                    int nMessageID = messagebox.insert_message(
                        new messagebox.MessageBox
                        {
                            message_type = messagebox.Leave,
                            message_type_id = letterid,
                            school_id = qcompany.nCompany,
                            user_messagebox = user_messagebox,
                            message = "approve=" + approve,
                            send_time = DateTime.Now
                        });


                    Thread notification = new Thread(async delegate ()
                    {
                        Double _span = (DateTime.Now - DateTime.UtcNow).TotalMinutes;
                    //await pushdata.push("[ \"" + userid + "\"]", comment, "แจ้งประกาศคำร้องแจ้งลา", DateTime.Now.AddMinutes(-_span), nMessageID, qcompany.nCompany);
                })
                    {
                        IsBackground = true
                    };
                    notification.Start();
                }
            }
        }

        private void stempStatus(JabJaiEntities dbschool, MasterEntity.TUser user, TLeaveLetter leaveLetter)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string LogScanStatus = "";
            switch (leaveLetter.letterType)
            {
                case "ลาป่วย": case "0": LogScanStatus = "11"; break;
                case "ลากิจ": case "1": LogScanStatus = "10"; break;
                case "2": LogScanStatus = "21"; break;
                case "3": LogScanStatus = "22"; break;
                case "4": LogScanStatus = "23"; break;
                case "5": LogScanStatus = "24"; break;
                case "6": LogScanStatus = "25"; break;
                case "7": LogScanStatus = "26"; break;
                default: LogScanStatus = "11"; break;
            }

            var q_logLearn = dbschool.TLogLearnTimeScans.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sID == user.nSystemID).ToList();

            if (user.cType == "0")
            {
                var q_studentt = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.sID == user.nSystemID);
                var q1 = (from a1 in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID)
                          join a2 in dbschool.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a1.nTerm.Trim() equals a2.nTerm
                          join b1 in dbschool.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a2.nTermTable equals b1.nTermTable
                          where a2.nTermSubLevel2 == q_studentt.nTermSubLevel2 && a1.cDel == null
                          select new
                          {
                              a1.dStart,
                              a1.dEnd,
                              b1.nPlaneDay,
                              b1.sScheduleID
                          }).ToList();

                for (int i = 0; leaveLetter.startDate.Value.AddDays(i) <= leaveLetter.endDate.Value; i++)
                {
                    #region Log Scan
                    //int nLogEmpScanID = dbschool.TLogUserTimeScans.Select(s => s.nLogScanID).DefaultIfEmpty(0).Max() + 1;
                    DateTime LogDate = leaveLetter.startDate.Value.AddDays(i);
                    int LognDay = (int)LogDate.DayOfWeek;
                    var q_log = dbschool.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID && w.sID == user.nSystemID && w.LogDate == LogDate).AsQueryable().FirstOrDefault();
                    if (q_log == null)
                    {
                        dbschool.TLogUserTimeScans.Add(new TLogUserTimeScan
                        {
                            LogDate = LogDate,
                            LogType = "0",
                            LognDay = LognDay,
                            sID = user.nSystemID,
                            LogScanStatus = LogScanStatus,
                            //nLogScanID = nLogEmpScanID,
                            SchoolID = userData.CompanyID,
                            CreatedDate = DateTime.Now,
                            CreatedBy = userData.UserID
                        });
                    }
                    else
                    {
                        q_log.LogScanStatus = LogScanStatus;
                        q_log.TeacherId = null;
                    }
                    #endregion
                    #region Log Schedules 
                    foreach (var s_log in q1.Where(w => w.dStart <= LogDate && w.dEnd >= LogDate && w.nPlaneDay == LognDay))
                    {
                        string LogLearnScanStatus = LogScanStatus == "10" ? "4" : "5";
                        var q_slog = q_logLearn.FirstOrDefault(f => f.sScheduleID == s_log.sScheduleID && f.LogLearnDate == LogDate);
                        if (q_slog == null)
                        {
                            dbschool.TLogLearnTimeScans.Add(new TLogLearnTimeScan
                            {
                                LogLearnDate = LogDate,
                                LogLearnType = "0",
                                LogLearnnDay = LognDay,
                                sID = user.nSystemID.Value,
                                LogLearnScanStatus = LogLearnScanStatus,
                                sScheduleID = s_log.sScheduleID,
                                sUserType = "0",
                                SchoolID = userData.CompanyID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = userData.UserID
                            });
                        }
                        else
                        {
                            q_slog.LogLearnScanStatus = LogLearnScanStatus;
                            q_slog.nTeacherId = null;
                        }
                    }
                    #endregion
                    dbschool.SaveChanges();
                }
            }
            else
            {
                for (int i = 0; leaveLetter.startDate.Value.AddDays(i) <= leaveLetter.endDate.Value; i++)
                {
                    //int nLogEmpScanID = dbschool.TLogEmpTimeScans.Select(s => s.nLogEmpScanID).DefaultIfEmpty(0).Max() + 1;
                    DateTime LogEmpDate = leaveLetter.startDate.Value.AddDays(i);
                    int LogEmpnDay = (int)LogEmpDate.DayOfWeek;
                    var q_log = dbschool.TLogEmpTimeScans.FirstOrDefault(f => f.SchoolID == userData.CompanyID && f.sEmp == user.nSystemID && f.LogEmpDate == LogEmpDate);
                    if (q_log == null)
                    {
                        dbschool.TLogEmpTimeScans.Add(new TLogEmpTimeScan
                        {
                            LogEmpDate = LogEmpDate,
                            LogEmpType = "0",
                            LogEmpnDay = LogEmpnDay,
                            sEmp = user.nSystemID,
                            LogEmpScanStatus = LogScanStatus,
                            //nLogEmpScanID = nLogEmpScanID,
                            SchoolID = userData.CompanyID,
                            CreatedDate = DateTime.Now,
                            CreatedBy = userData.UserID
                        });
                    }
                    else
                    {
                        q_log.LogEmpScanStatus = LogScanStatus;
                    }
                    dbschool.SaveChanges();
                }
            }
        }
    }
}