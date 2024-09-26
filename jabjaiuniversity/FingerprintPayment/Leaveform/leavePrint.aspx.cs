using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FingerprintPayment.Leaveform;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using static FingerprintPayment.Leaveform.AllLeaveLogic;
using System.Globalization;

namespace FingerprintPayment
{
    public partial class leavePrint : System.Web.UI.Page
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

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

               

                string sEntities = Session["sEntities"] + "";
                int? nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;

                int id = 0;
                Int32.TryParse(Request.QueryString["id"], out id);
                var data1 = _db.TLeaveLetter.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.letterId == id && w.cDel == false).FirstOrDefault();

                var _dr = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany).FirstOrDefault();
                labelSchool.Text = _dr.sCompany;

                int? empid = data1.writerId;
                var whoRequest = "";


                if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

                if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

                var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == _dr.nCompany).FirstOrDefault();


                string addressnum = schooldata.sHomeNumber;
                string soy = " ซ." + schooldata.sSoy;
                string road = " ถ." + schooldata.sRoad;
                string aumper = " " + schooldata.sAumpher;
                string province = " " + schooldata.sProvince;
                string post = " " + schooldata.sPost;
                string phone = "โทร. " + schooldata.sPhoneOne;

                if (schooldata.sRoad == "")
                    road = "";
                if (schooldata.sSoy == "")
                    soy = "";
                if (schooldata.sPhoneTwo != "" && schooldata.sPhoneTwo != null)
                    phone = "โทร. " + schooldata.sPhoneOne + ", " + schooldata.sPhoneTwo;

                p1header1.Text = schooldata.sCompany;
                p1header2.Text = addressnum + soy + road + aumper + province + post;
                p1header3.Text = phone;


                if (schooldata != null)
                {
                    schoolpicture.Src = schooldata.sImage;
                }

                if (data1.writerJob == "นักเรียน" || data1.writerJob == "0")
                {
                    var studentLogic = new StudentLogic(_db);
                    string TermId = studentLogic.GetTermId(userData);
                    var student = _db.TB_StudentViews
                        .Where(w => w.SchoolID == userData.CompanyID && w.sID == empid && w.nTerm == TermId)
                        .FirstOrDefault();

                    whoRequest = student.sName + " " + student.sLastname + "";
                    txtName.Text = whoRequest;
                    //txtName2.Text = name.sName + " " + name.sLastname;
                    txtJob.Text = "นักเรียน " + student.SubLevel + "/" + student.nTSubLevel2;
                }
                else
                {
                    var name2 = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == empid).FirstOrDefault();
                    whoRequest = name2.sName + " " + name2.sLastname;
                    txtName.Text = whoRequest;
                    //txtName2.Text = name2.sName + " " + name2.sLastname;

                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == data1.writerId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            txtJob.Text = jobdata.jobDescription;
                    }

                }

                string uncheck = "<i class='glyphicon glyphicon-unchecked'></i>";
                string check = "<i class='glyphicon glyphicon-check'></i>";


                txtSchool.Text = _dr.sCompany;

                if (data1.letterType == "ป่วย" || data1.letterType == "ลาป่วย" || data1.letterType == "0")
                {
                    txtType.Text = "ป่วย";
                }
                else if (data1.letterType == "กิจส่วนตัว" || data1.letterType == "ลากิจ" || data1.letterType == "1")
                {
                    txtType.Text = "กิจส่วนตัว ";
                }
                else if (data1.letterType == "คลอดบุตร" || data1.letterType == "ลาคลอด" || data1.letterType == "2")
                {
                    txtType.Text = "คลอดบุตร";
                }
                else if (data1.letterType == "3" || data1.letterType == "ลาพักผ่อน" || data1.letterType == "ลาพักร้อน")
                    txtType.Text = "ลาพักร้อน";
                else if (data1.letterType == "4" || data1.letterType == "ลาอุปสมบท")
                    txtType.Text = "ลาอุปสมบทหรือการไปประกอบพิธีฮัจญ์";
                else if (data1.letterType == "5" || data1.letterType == "ลาทหาร")
                    txtType.Text = "ลาเข้ารับการตรวจเลือกหรือเข้ารับการเตรียมพล";
                else if (data1.letterType == "6")
                    txtType.Text = "ลาไปศึกษาฝึกอบรม ปฏิบัติการวิจัยหรือดูงาน";
                else if (data1.letterType == "7" || data1.letterType == "ลาไปราชการ")
                    txtType.Text = "ลาไปราชการ";
                else
                {
                    var leaveType = _db.TLeave_Type.FirstOrDefault(o => o.SchoolID == userData.CompanyID && o.TypeID == data1.LeaveTypeID);
                    txtType.Text = leaveType?.TypeName ?? "อื่นๆ";
                }


                txtReason.Text = data1.writerComment; ;

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

                stmonth = start.Value.ToString("MMMM", new CultureInfo("th-th"));
                edmonth = end.Value.ToString("MMMM", new CultureInfo("th-th"));
                ltmonth = letterDate.Value.ToString("MMMM", new CultureInfo("th-th"));

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

                labelDate.Text = letterday + " " + ltmonth + " " + letterYear;
                txtHeader.Text = "ขออนุญาตลา"/* + data1.letterHeader*/;
                TextBox1.Text = "ผู้อำนวยการ" + _dr.sCompany;

                dStart.Text = startday + " " + stmonth + " พ.ศ." + startYear;
                dEnd.Text = endday + " " + edmonth + " พ.ศ." + endYear;
                if (data1.Season == 0 || data1.Season == 1) typeLeave.Text = "ครึ่งวัน";
                else typeLeave.Text = "เต็มวัน";
                sumLeave.Text = nod4 + " วัน";

                if (data1.contactHomenumber != null && data1.contactHomenumber != "") txtHomenumber.Text = data1.contactHomenumber;
                if (data1.contactRoad != null && data1.contactRoad != "") txtRoad.Text = data1.contactRoad;
                if (data1.contactTumbon != null && data1.contactTumbon != "") txtsubDistrict.Text = data1.contactTumbon;
                if (data1.contactAumpher != null && data1.contactAumpher != "") txtDistrict.Text = data1.contactAumpher;
                if (data1.contactProvince != null && data1.contactProvince != "") txtProvince.Text = data1.contactProvince;
                if (data1.contactPhone != null && data1.contactPhone != "") txtPhone.Text = data1.contactPhone;


                var jobWhoRequest = "";

                if (data1.writerJob == "นักเรียน" || data1.writerJob == "0")
                {
                    jobWhoRequest = "ตำแหน่ง นักเรียน";
                }
                else
                {
                    var empdata = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.sEmp == data1.writerId).FirstOrDefault();
                    if (empdata != null)
                    {
                        var jobdata = _db.TJobLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nJobid == empdata.nJobid).FirstOrDefault();
                        if (jobdata != null)
                            jobWhoRequest = "ตำแหน่ง " + jobdata.jobDescription;
                    }
                }

                int sickCounter = 0;
                int? sickDayTotal = 0;
                int? sickDayMaLaew = 0;
                int businessCounter = 0;
                int? businessDayTotal = 0;
                int? businessDayMaLaew = 0;
                int sonCounter = 0;
                int? sonDayTotal = 0;
                int? sonDayMaLaew = 0;
                int? otherDayTotal = 0;
                int otherCounter = 0;
                int? otherDayMaLaew = 0;

                int thisYear = letterYear;
                if (thisYear > 2550)
                    thisYear = thisYear - 543;

                AllLeaveLogic allLeaveLogic = new AllLeaveLogic();

                var GetAdmins = allLeaveLogic.GetAdmins(id);

                if (GetAdmins.Count() == 1)
                {
                    txtName2.Text = whoRequest;
                    txtJob2.Text = jobWhoRequest;

                    txtApproveComment1.Text = GetAdmins[0].comment;
                    txtApproveNameType1.Text = GetAdmins[0].name;
                    txtApproveDateType1.Text = GetAdmins[0].dateString;
                    txtApprovePositionType1.Text = "ตำแหน่ง " + GetAdmins[0].job;
                }
                else if (GetAdmins.Count() != 0 && GetAdmins.Count() != 1)
                {
                    txtname3.Text = whoRequest;
                    txtJob3.Text = jobWhoRequest;

                    if (GetAdmins.Count() == 2)
                    {
                        txtApproveCommentType2_1.Text = GetAdmins[0].comment;
                        txtApproveNameType2_1.Text = GetAdmins[0].name;
                        txtApproveDateType2_1.Text = GetAdmins[0].dateString;

                        txtApproveCommentType2_2.Text = GetAdmins[0].comment;
                        txtApproveNameType2_2.Text = GetAdmins[1].name;
                        txtApproveDateType2_2.Text = GetAdmins[1].dateString;
                    }
                    else if (GetAdmins.Count() == 3)
                    {
                        txtApproveCommentType3_1.Text = GetAdmins[0].comment;
                        txtApproveNameType3_1.Text = GetAdmins[0].name;
                        txtApproveDateType3_1.Text = GetAdmins[0].dateString;

                        txtApproveCommentType3_2.Text = GetAdmins[0].comment;
                        txtApproveNameType3_2.Text = GetAdmins[1].name;
                        txtApproveDateType3_2.Text = GetAdmins[1].dateString;

                        txtApproveCommentType3_3.Text = GetAdmins[0].comment;
                        txtApproveNameType3_3.Text = GetAdmins[2].name;
                        txtApproveDateType3_3.Text = GetAdmins[2].dateString;
                    }
                }

                LeaveCount leave = new LeaveCount();
                leave = allLeaveLogic.Leave(empid.Value, data1.startDate, (data1.writerJob == "นักเรียน" || data1.writerJob == "0") ? 0 : 1, data1);

                StatsLeave.Text = "สถิติการลาในปีการศึกษา " + leave.year;

                sickLeave.Text = leave.sickDayTotal.ToString();
                bLeave.Text = leave.businessDayTotal.ToString();
                gsLeave.Text = leave.governmentServicesTotal.ToString();
                studyLeave.Text = leave.studyTotal.ToString();
                etcLeave.Text = leave.otherDayTotal.ToString();

                var totalLeave1 = leave.sickDayTotal + leave.businessDayTotal + leave.governmentServicesTotal + leave.studyTotal + leave.otherDayTotal;
                totalLeave.Text = totalLeave1.ToString(); ;
            }


        }
    }
}