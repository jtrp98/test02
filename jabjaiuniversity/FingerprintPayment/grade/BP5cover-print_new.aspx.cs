using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using MasterEntity;
using Ninject;
using SchoolBright.Business.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    public partial class BP5cover_print_new : System.Web.UI.Page
    {

        [Inject]
        public ICommonService CommonService { get; set; }

        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();


            string id = Request.QueryString["id"];
            //var data = _db.TPlanes.Where(w => w.sPlaneID == id).FirstOrDefault();
            var plandata = _db.TPlanes.Where(w => w.sPlaneID.ToString() == id).FirstOrDefault();

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            var schooldata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();

            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);
            string idlv2 = Request.QueryString["idlv2"];
            int? idlv2n = Int32.Parse(idlv2);
            var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();

            var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();


            planid.Text = plandata.courseCode + " " + plandata.sPlaneName;
            cover2planid.Text = plandata.courseCode + " " + plandata.sPlaneName;
            Year2.Text = Request.QueryString["term"] + "/" + Request.QueryString["year"];
            Year3.Text = Request.QueryString["year"];
            cover2year.Text = Request.QueryString["year"];


            if (schooldata != null)
            {
                schoolpicture.Src = schooldata.sImage;
                schoolpicture2.Src = schooldata.sImage;
                Img1.Src = schooldata.sImage;
            }

            txtschool.Text = schooldata.sCompany;
            txtschool2.Text = schooldata.sCompany;
            Label13.Text = room.SubLevel + " / " + room2.nTSubLevel2;
            cover2class.Text = room.SubLevel + " / " + room2.nTSubLevel2;

            string year = Request.QueryString["year"];
            string userterm = Request.QueryString["term"];
            string userterm2 = "";
            if (userterm == "1") userterm2 = "2";
            if (userterm == "2") userterm2 = "1";

            var roomz = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();
            var room2z = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();
            var tcourseGroup = _db.TCourseGroups.Where(w => w.courseGroupId == plandata.courseGroup).FirstOrDefault();

            if (tcourseGroup != null && tcourseGroup.Description == "รายวิชาพื้นฐาน")
            {
                cover3type1.Attributes["class"] = "col-xs-4 lefttext font23 h25 ";
                cover3type2.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
                cover3type3.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
            }
            else if (tcourseGroup != null && tcourseGroup.Description == "รายวิชาเพิ่มเติม")
            {
                cover3type1.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
                cover3type2.Attributes["class"] = "col-xs-4 lefttext font23 h25 ";
                cover3type3.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
            }
            else
            {
                cover3type1.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
                cover3type2.Attributes["class"] = "col-xs-4 lefttext font23 h25 hidden";
                cover3type3.Attributes["class"] = "col-xs-4 lefttext font23 h25 ";
            }
            coverhead1.Text = "ปีการศึกษา " + year;
            coverhead2.Text = "ภาคเรียนที่ " + userterm;
            cover4sign.Text = "ผู้อำนวยการ" + nCompany.sCompany;
            if (nCompany.sCompany.Length > 35)
            {
                Textbox8.Text = "ผู้อำนวยการ" + nCompany.sCompany;
                cover4sign.Text = "";
            }

            var tcourseType = _db.TCourseTypes.Where(w => w.SchoolID == nCompany.nCompany && w.courseTypeId == plandata.courseType).FirstOrDefault();

            string planType = "";
            planType = (tcourseType != null) ? tcourseType.Description : string.Empty;

            //string planType = "";
            //if (plandata.courseType == 1) planType = "ภาษาไทย";
            //else if (plandata.courseType == 2) planType = "คณิตศาสตร์";
            //else if (plandata.courseType == 3) planType = "วิทยาศาสตร์";
            //else if (plandata.courseType == 4) planType = "สังคมศึกษา ศาสนา และวัฒนธรรม";
            //else if (plandata.courseType == 5) planType = "สุขศึกษาและพลศึกษา";
            //else if (plandata.courseType == 6) planType = "ศิลปะ";
            //else if (plandata.courseType == 7) planType = "การงานอาชีพและเทคโนโลยี";
            //else if (plandata.courseType == 8) planType = "ภาษาต่างประเทศ";
            //else if (plandata.courseType == 9) planType = "กิจกรรมพัฒนาผู้เรียน";
            //else if (plandata.courseType == 10) planType = "การศึกษาค้นคว้าด้วยตนเอง";
            //else if (plandata.courseType == 11) planType = "อิสลามศึกษา";

            string comPro = nCompany.sProvince;
            string comAum = nCompany.sAumpher;
            int n;
            bool isNumeric = int.TryParse(nCompany.sProvince, out n);
            if (isNumeric == true)
            {
                var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                if (userpro != null)
                    comPro = userpro.PROVINCE_NAME;
            }

            isNumeric = int.TryParse(nCompany.sAumpher, out n);
            if (isNumeric == true)
            {
                var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                if (useraum != null)
                    comAum = useraum.AMPHUR_NAME;
            }

            cover3_1.Text = "แบบบันทึกผลการพัฒนาคุณภาพผู้เรียน";
            cover3_2.Text = "( กลุ่มสาระการเรียนรู้ / รายวิชา ) &nbsp;&nbsp;&nbsp; ปีการศึกษา " + year;
            cover3_3.Text = "หลักสูตร" + nCompany.sCompany + " " + comPro + " ตามหลักสูตรการศึกษาขั้นพื้นฐาน พุทธศักราช 2551";
            cover3_4.Text = nCompany.sCompany + " อำเภอ" + comAum + " จังหวัด" + comPro;
            cover3_5.Text = "ระดับชั้น" + roomz.fullName + " / " + room2z.nTSubLevel2 + " ปีการศึกษา " + year;
            cover3_6.Text = "กลุ่มสาระการเรียนรู้" + planType;
            cover3_7.Text = "รายวิชา" + plandata.sPlaneName + " &nbsp;&nbsp; รหัสวิชา " + plandata.courseCode;

            paper5.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper6.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper16.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper17.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper8.Text = "ประเมินผลการเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper22.Text = "ประเมินผลการเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper7.Text = "ประเมินผลการเรียนรายปี วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper12.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper13.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper18.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper19.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper11header.Text = "ผลการประเมินคุณลักษณะอันพึงประสงค์ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper20.Text = "ผลการประเมินคุณลักษณะอันพึงประสงค์ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;


            paper5Ex.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper6Ex.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper16Ex.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper17Ex.Text = "คะแนนเก็บ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper8Ex.Text = "ประเมินผลการเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper22Ex.Text = "ประเมินผลการเรียน วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper7Ex.Text = "ประเมินผลการเรียนรายปี วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper12Ex.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper13Ex.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper18Ex.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper19Ex.Text = "คะแนนหน่วยชี้วัด วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper11headerEx.Text = "ผลการประเมินคุณลักษณะอันพึงประสงค์ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;
            paper20Ex.Text = "ผลการประเมินคุณลักษณะอันพึงประสงค์ วิชา" + plandata.sPlaneName + " ปีการศึกษา " + year + " ภาคเรียนที่ " + userterm2 + " ชั้น " + roomz.SubLevel + " / " + room2z.nTSubLevel2;

            DateTime now = DateTime.Now;
            Textbox4.Text = now.Day.ToString() + " " + monthtxt(now.Month) + " " + now.AddYears(543).Year.ToString();

            if (!IsPostBack)
            {
                OpenData();


            }
        }

        private void OpenData()
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities dbmaster = Connection.MasterEntities();

            string sEntities = Session["sEntities"].ToString();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            string id = Request.QueryString["id"];
            string year = Request.QueryString["year"];
            string userterm = Request.QueryString["term"];
            string userterm2 = "";
            if (userterm == "1") userterm2 = "2";
            else if (userterm == "2") userterm2 = "1";
            string idlv2 = Request.QueryString["idlv2"];
            string idlv = Request.QueryString["idlv"];
            int? idlvn = Int32.Parse(idlv);

            int? useryear = Int32.Parse(year);
            int? idlv2n = Int32.Parse(idlv2);
            int nyear = 0;
            string nterm = "";
            string nterm2 = "";
            int? userterm2n = int.Parse(userterm2);
            var room = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn).FirstOrDefault();
            var room2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idlv2n).FirstOrDefault();

            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == userterm2 && w.nYear == nyear && w.cDel == null))
            {
                nterm2 = ee.nTerm;
            }

            var planCourseDTO = ServiceHelper.GetPlanCoursesWithTerm((int)idlvn, ((idlv2n != null) ? (int)idlv2n : 0), nyear, Utils.GetSchoolId(), _db);

            if (tCompany.nSchoolHeadid != null)
            {
                var emp = _db.TEmployees.Where(w => w.sEmp == tCompany.nSchoolHeadid).FirstOrDefault();
                int titlen;
                string title = "";
                bool checkTitle = int.TryParse(emp.sTitle, out titlen);
                if (checkTitle == true)
                {
                    var titledb = _db.TTitleLists.Where(w => w.nTitleid == titlen).FirstOrDefault();
                    title = titledb.titleDescription;
                }
                else title = emp.sTitle;
                schoolheadsign.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                Textbox3.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                cover2headersign.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
            }
            else
            {
                Textbox3.Text = "( " + tCompany.SchoolHeadName + " " + tCompany.SchoolHeadLastname + " )";
                schoolheadsign.Text = "( " + tCompany.SchoolHeadName + " " + tCompany.SchoolHeadLastname + " )";
                cover2headersign.Text = "( " + tCompany.SchoolHeadName + " " + tCompany.SchoolHeadLastname + " )";
            }

            if (tCompany.nRegistraDirectorid != null)
            {
                var emp = _db.TEmployees.Where(w => w.sEmp == tCompany.nRegistraDirectorid).FirstOrDefault();
                int titlen;
                string title = "";
                bool checkTitle = int.TryParse(emp.sTitle, out titlen);
                if (checkTitle == true)
                {
                    var titledb = _db.TTitleLists.Where(w => w.nTitleid == titlen).FirstOrDefault();
                    title = titledb.titleDescription;
                }
                else title = emp.sTitle;
                cover1Accounting.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                Textbox1.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                cover2Accounting.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
            }

            if (tCompany.nAcademicSubDirectorid != null)
            {
                var emp = _db.TEmployees.Where(w => w.sEmp == tCompany.nAcademicSubDirectorid).FirstOrDefault();
                int titlen;
                string title = "";
                bool checkTitle = int.TryParse(emp.sTitle, out titlen);
                if (checkTitle == true)
                {
                    var titledb = _db.TTitleLists.Where(w => w.nTitleid == titlen).FirstOrDefault();
                    title = titledb.titleDescription;
                }
                else title = emp.sTitle;
                cover1subEducationHead.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                cover2subEducationHead.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                Textbox2.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
            }

            var dd = _db.TTermTimeTables.Where(w => w.nTerm == nterm && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
            if (dd != null)
            {
                var aa = _db.TSchedules.Where(w => w.nTermTable == dd.nTermTable && w.sPlaneID.ToString() == id && w.sEmp != null).FirstOrDefault();
                if (aa != null)
                {
                    var emp = _db.TEmployees.Where(w => w.sEmp == aa.sEmp).FirstOrDefault();
                    int titlen;
                    string title = "";
                    bool checkTitle = int.TryParse(emp.sTitle, out titlen);
                    if (checkTitle == true)
                    {
                        var titledb = _db.TTitleLists.Where(w => w.nTitleid == titlen).FirstOrDefault();
                        title = titledb.titleDescription;
                    }
                    else title = emp.sTitle;
                    cover1teacher.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                    cover2teacher.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                    cover4sign1.Text = "( " + title + emp.sName + " " + emp.sLastname + " )";
                }
            }

            var grade = _db.TB_GradeViews.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == id && w.nTermSubLevel2 == idlv2n).FirstOrDefault();

            ddlstyle1.SelectedValue = "0";
            if (grade.GradeCloseGrade == "1")
                ddlstyle1.SelectedValue = "1";
            //ddlattendance.SelectedValue = userterm;
            ddlattendance2.SelectedValue = "0";
            ddlquiz.SelectedValue = "0";

            ddlterm.SelectedValue = "0";

            ddlcover.SelectedValue = "3";

            //if (grade.GradeAddBehavior == "1")
            //{
            //   ddlcover.SelectedValue = "1";
            // ddlbehave.SelectedValue = "0";
            //}
            //else {
            //    ddlcover.SelectedValue = "0";
            //    ddlbehave.SelectedValue = "2";
            //}


            if (grade.GradeAddCheewat == "1")
                ddlcheewat.SelectedValue = "0";
            else ddlcheewat.SelectedValue = "2";


            ddlyear.SelectedValue = "0";
            ddlgraph.SelectedValue = "2";

            int totalstudent = 0;

            double ratiomid = grade.fRatioMidTerm;
            double ratiolate = grade.fRatioLateTerm;
            double ratioquiz = grade.fRatioQuiz;

            page8head1.Text = "ระหว่างภาค" + "<br />" + "(" + grade.fRatioQuiz + ")";
            page8head2.Text = "กลางภาค" + "<br />" + "(" + grade.fRatioMidTerm + ")";
            page8head3.Text = "ปลายภาค" + "<br />" + "(" + grade.fRatioLateTerm + ")";
            page8head4.Text = "รวม" + "<br />" + "(100)";

            page8exhead1.Text = "ระหว่างภาค" + "<br />" + "(" + grade.fRatioQuiz + ")";
            page8exhead2.Text = "กลางภาค" + "<br />" + "(" + grade.fRatioMidTerm + ")";
            page8exhead3.Text = "ปลายภาค" + "<br />" + "(" + grade.fRatioLateTerm + ")";
            page8exhead4.Text = "รวม" + "<br />" + "(100)";

            page22head1.Text = "ระหว่างภาค" + "<br />" + "(" + grade.fRatioQuiz + ")";
            page22head2.Text = "กลางภาค" + "<br />" + "(" + grade.fRatioMidTerm + ")";
            page22head3.Text = "ปลายภาค" + "<br />" + "(" + grade.fRatioLateTerm + ")";
            page22head4.Text = "รวม" + "<br />" + "(100)";

            page22exhead1.Text = "ระหว่างภาค" + "<br />" + "(" + grade.fRatioQuiz + ")";
            page22exhead2.Text = "กลางภาค" + "<br />" + "(" + grade.fRatioMidTerm + ")";
            page22exhead3.Text = "ปลายภาค" + "<br />" + "(" + grade.fRatioLateTerm + ")";
            page22exhead4.Text = "รวม" + "<br />" + "(100)";


            double total40 = 0;
            double total35 = 0;
            double total30 = 0;
            double total25 = 0;
            double total20 = 0;
            double total15 = 0;
            double total10 = 0;
            double total00 = 0;

            double behave1_3 = 0;
            double behave1_2 = 0;
            double behave1_1 = 0;
            double behave1_0 = 0;
            double behave2_3 = 0;
            double behave2_2 = 0;
            double behave2_1 = 0;
            double behave2_0 = 0;
            double behave3_3 = 0;
            double behave3_2 = 0;
            double behave3_1 = 0;
            double behave3_0 = 0;
            double behave4_3 = 0;
            double behave4_2 = 0;
            double behave4_1 = 0;
            double behave4_0 = 0;
            double behave5_3 = 0;
            double behave5_2 = 0;
            double behave5_1 = 0;
            double behave5_0 = 0;
            double behave6_3 = 0;
            double behave6_2 = 0;
            double behave6_1 = 0;
            double behave6_0 = 0;
            double behave7_3 = 0;
            double behave7_2 = 0;
            double behave7_1 = 0;
            double behave7_0 = 0;
            double behave8_3 = 0;
            double behave8_2 = 0;
            double behave8_1 = 0;
            double behave8_0 = 0;
            double behave9_3 = 0;
            double behave9_2 = 0;
            double behave9_1 = 0;
            double behave9_0 = 0;
            double behave10_3 = 0;
            double behave10_2 = 0;
            double behave10_1 = 0;
            double behave10_0 = 0;

            double t2behave1_3 = 0;
            double t2behave1_2 = 0;
            double t2behave1_1 = 0;
            double t2behave1_0 = 0;
            double t2behave2_3 = 0;
            double t2behave2_2 = 0;
            double t2behave2_1 = 0;
            double t2behave2_0 = 0;
            double t2behave3_3 = 0;
            double t2behave3_2 = 0;
            double t2behave3_1 = 0;
            double t2behave3_0 = 0;
            double t2behave4_3 = 0;
            double t2behave4_2 = 0;
            double t2behave4_1 = 0;
            double t2behave4_0 = 0;
            double t2behave5_3 = 0;
            double t2behave5_2 = 0;
            double t2behave5_1 = 0;
            double t2behave5_0 = 0;
            double t2behave6_3 = 0;
            double t2behave6_2 = 0;
            double t2behave6_1 = 0;
            double t2behave6_0 = 0;
            double t2behave7_3 = 0;
            double t2behave7_2 = 0;
            double t2behave7_1 = 0;
            double t2behave7_0 = 0;
            double t2behave8_3 = 0;
            double t2behave8_2 = 0;
            double t2behave8_1 = 0;
            double t2behave8_0 = 0;
            double t2behave9_3 = 0;
            double t2behave9_2 = 0;
            double t2behave9_1 = 0;
            double t2behave9_0 = 0;
            double t2behave10_3 = 0;
            double t2behave10_2 = 0;
            double t2behave10_1 = 0;
            double t2behave10_0 = 0;

            double ror = 0;
            double MS = 0;
            double MK = 0;
            double P = 0;
            double MP = 0;
            double Other = 0;


            double xbar = 0;
            double x2 = 0;

            double Bthree = 0;
            double Btwo = 0;
            double Bone = 0;
            double Bzero = 0;

            double Rthree = 0;
            double Rtwo = 0;
            double Rone = 0;
            double Rzero = 0;

            double SMthree = 0;
            double SMtwo = 0;
            double SMone = 0;
            double SMzero = 0;

                var gradeid_term1 = planCourseDTO.Where(w => w.SPlaneId == grade.sPlaneID && w.NTerm == nterm).FirstOrDefault();
            //if (userterm == "1") term1id.Text = gradeid_term1.sPlaneID;
            //else if (userterm == "2") term2id.Text = gradeid_term1.sPlaneID;
                var gradeid_term2 = planCourseDTO.Where(w => w.CourseName == gradeid_term1.CourseName && w.NTerm == nterm2).FirstOrDefault();
            var q_user = _db.TUsers.Where(w => (w.cDel ?? "0") != "1").ToList();
            if (gradeid_term2 != null)
            {
                //if (userterm2 == "1") term1id.Text = gradeid_term2.sPlaneID;
                //else if (userterm2 == "2") term2id.Text = gradeid_term2.sPlaneID;
                term2id.Text = gradeid_term2.SPlaneId.ToString();
                var grade2 = _db.TGrades.Where(w => w.nTerm == nterm2 && w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
                if (grade2 != null)
                {
                    foreach (var aa in _db.TB_GradeDetailView.Where(w => w.nGradeId == grade2.nGradeId))
                    {
                        var thisuser = q_user.Where(w => w.sID == aa.sID).FirstOrDefault();
                        if (thisuser != null)
                        {
                            int alreadyQuit = 0;

                            if (thisuser.nStudentStatus == 2)
                            {
                                if (thisuser.DayQuit != null)
                                {
                                    var thisyear = _db.TYears.Where(w => w.numberYear == useryear).FirstOrDefault();
                                    var thisterm = _db.TTerms.Where(w => w.nYear == thisyear.nYear && w.sTerm == userterm2 && w.cDel == null).FirstOrDefault();

                                    if (thisterm != null)
                                    {
                                        if (thisuser.DayQuit <= thisterm.dEnd)
                                            alreadyQuit = 1;
                                    }
                                }
                                else alreadyQuit = 1;
                            }

                            if (alreadyQuit == 0)
                            {
                                if (aa.scoreBehavior1 == "3") t2behave1_3 = t2behave1_3 + 1;
                                else if (aa.scoreBehavior1 == "2") t2behave1_2 = t2behave1_2 + 1;
                                else if (aa.scoreBehavior1 == "1") t2behave1_1 = t2behave1_1 + 1;
                                else if (aa.scoreBehavior1 == "0") t2behave1_0 = t2behave1_0 + 1;

                                if (aa.scoreBehavior2 == "3") t2behave2_3 = t2behave2_3 + 1;
                                else if (aa.scoreBehavior2 == "2") t2behave2_2 = t2behave2_2 + 1;
                                else if (aa.scoreBehavior2 == "1") t2behave2_1 = t2behave2_1 + 1;
                                else if (aa.scoreBehavior2 == "0") t2behave2_0 = t2behave2_0 + 1;

                                if (aa.scoreBehavior3 == "3") t2behave3_3 = t2behave3_3 + 1;
                                else if (aa.scoreBehavior3 == "2") t2behave3_2 = t2behave3_2 + 1;
                                else if (aa.scoreBehavior3 == "1") t2behave3_1 = t2behave3_1 + 1;
                                else if (aa.scoreBehavior3 == "0") t2behave3_0 = t2behave3_0 + 1;

                                if (aa.scoreBehavior4 == "3") t2behave4_3 = t2behave4_3 + 1;
                                else if (aa.scoreBehavior4 == "2") t2behave4_2 = t2behave4_2 + 1;
                                else if (aa.scoreBehavior4 == "1") t2behave4_1 = t2behave4_1 + 1;
                                else if (aa.scoreBehavior4 == "0") t2behave4_0 = t2behave4_0 + 1;

                                if (aa.scoreBehavior5 == "3") t2behave5_3 = t2behave5_3 + 1;
                                else if (aa.scoreBehavior5 == "2") t2behave5_2 = t2behave5_2 + 1;
                                else if (aa.scoreBehavior5 == "1") t2behave5_1 = t2behave5_1 + 1;
                                else if (aa.scoreBehavior5 == "0") t2behave5_0 = t2behave5_0 + 1;

                                if (aa.scoreBehavior6 == "3") t2behave6_3 = t2behave6_3 + 1;
                                else if (aa.scoreBehavior6 == "2") t2behave6_2 = t2behave6_2 + 1;
                                else if (aa.scoreBehavior6 == "1") t2behave6_1 = t2behave6_1 + 1;
                                else if (aa.scoreBehavior6 == "0") t2behave6_0 = t2behave6_0 + 1;

                                if (aa.scoreBehavior7 == "3") t2behave7_3 = t2behave7_3 + 1;
                                else if (aa.scoreBehavior7 == "2") t2behave7_2 = t2behave7_2 + 1;
                                else if (aa.scoreBehavior7 == "1") t2behave7_1 = t2behave7_1 + 1;
                                else if (aa.scoreBehavior7 == "0") t2behave7_0 = t2behave7_0 + 1;

                                if (aa.scoreBehavior8 == "3") t2behave8_3 = t2behave8_3 + 1;
                                else if (aa.scoreBehavior8 == "2") t2behave8_2 = t2behave8_2 + 1;
                                else if (aa.scoreBehavior8 == "1") t2behave8_1 = t2behave8_1 + 1;
                                else if (aa.scoreBehavior8 == "0") t2behave8_0 = t2behave8_0 + 1;

                                if (aa.scoreBehavior9 == "3") t2behave9_3 = t2behave9_3 + 1;
                                else if (aa.scoreBehavior9 == "2") t2behave9_2 = t2behave9_2 + 1;
                                else if (aa.scoreBehavior9 == "1") t2behave9_1 = t2behave9_1 + 1;
                                else if (aa.scoreBehavior9 == "0") t2behave9_0 = t2behave9_0 + 1;

                                if (aa.scoreBehavior10 == "3") t2behave10_3 = t2behave10_3 + 1;
                                else if (aa.scoreBehavior10 == "2") t2behave10_2 = t2behave10_2 + 1;
                                else if (aa.scoreBehavior10 == "1") t2behave10_1 = t2behave10_1 + 1;
                                else if (aa.scoreBehavior10 == "0") t2behave10_0 = t2behave10_0 + 1;
                            }
                        }
                    }
                }
            }


            foreach (var aa in _db.TB_GradeDetailView.Where(w => w.nGradeId == grade.nGradeId))
            {
                var thisuser = q_user.Where(w => w.sID == aa.sID).FirstOrDefault();
                if (thisuser != null)
                {

                    int alreadyQuit = 0;

                    if (thisuser.nStudentStatus == 2)
                    {
                        if (thisuser.DayQuit != null)
                        {
                            var thisyear = _db.TYears.Where(w => w.numberYear == useryear).FirstOrDefault();
                            var thisterm = _db.TTerms.Where(w => w.nYear == thisyear.nYear && w.sTerm == userterm && w.cDel == null).FirstOrDefault();

                            if (thisuser.DayQuit <= thisterm.dEnd)
                                alreadyQuit = 1;
                        }
                        else alreadyQuit = 1;
                    }

                    if (alreadyQuit == 0)
                    {
                        if (aa.scoreBehavior1 == "3") behave1_3 = behave1_3 + 1;
                        else if (aa.scoreBehavior1 == "2") behave1_2 = behave1_2 + 1;
                        else if (aa.scoreBehavior1 == "1") behave1_1 = behave1_1 + 1;
                        else if (aa.scoreBehavior1 == "0") behave1_0 = behave1_0 + 1;

                        if (aa.scoreBehavior2 == "3") behave2_3 = behave2_3 + 1;
                        else if (aa.scoreBehavior2 == "2") behave2_2 = behave2_2 + 1;
                        else if (aa.scoreBehavior2 == "1") behave2_1 = behave2_1 + 1;
                        else if (aa.scoreBehavior2 == "0") behave2_0 = behave2_0 + 1;

                        if (aa.scoreBehavior3 == "3") behave3_3 = behave3_3 + 1;
                        else if (aa.scoreBehavior3 == "2") behave3_2 = behave3_2 + 1;
                        else if (aa.scoreBehavior3 == "1") behave3_1 = behave3_1 + 1;
                        else if (aa.scoreBehavior3 == "0") behave3_0 = behave3_0 + 1;

                        if (aa.scoreBehavior4 == "3") behave4_3 = behave4_3 + 1;
                        else if (aa.scoreBehavior4 == "2") behave4_2 = behave4_2 + 1;
                        else if (aa.scoreBehavior4 == "1") behave4_1 = behave4_1 + 1;
                        else if (aa.scoreBehavior4 == "0") behave4_0 = behave4_0 + 1;

                        if (aa.scoreBehavior5 == "3") behave5_3 = behave5_3 + 1;
                        else if (aa.scoreBehavior5 == "2") behave5_2 = behave5_2 + 1;
                        else if (aa.scoreBehavior5 == "1") behave5_1 = behave5_1 + 1;
                        else if (aa.scoreBehavior5 == "0") behave5_0 = behave5_0 + 1;

                        if (aa.scoreBehavior6 == "3") behave6_3 = behave6_3 + 1;
                        else if (aa.scoreBehavior6 == "2") behave6_2 = behave6_2 + 1;
                        else if (aa.scoreBehavior6 == "1") behave6_1 = behave6_1 + 1;
                        else if (aa.scoreBehavior6 == "0") behave6_0 = behave6_0 + 1;

                        if (aa.scoreBehavior7 == "3") behave7_3 = behave7_3 + 1;
                        else if (aa.scoreBehavior7 == "2") behave7_2 = behave7_2 + 1;
                        else if (aa.scoreBehavior7 == "1") behave7_1 = behave7_1 + 1;
                        else if (aa.scoreBehavior7 == "0") behave7_0 = behave7_0 + 1;

                        if (aa.scoreBehavior8 == "3") behave8_3 = behave8_3 + 1;
                        else if (aa.scoreBehavior8 == "2") behave8_2 = behave8_2 + 1;
                        else if (aa.scoreBehavior8 == "1") behave8_1 = behave8_1 + 1;
                        else if (aa.scoreBehavior8 == "0") behave8_0 = behave8_0 + 1;

                        if (aa.scoreBehavior9 == "3") behave9_3 = behave9_3 + 1;
                        else if (aa.scoreBehavior9 == "2") behave9_2 = behave9_2 + 1;
                        else if (aa.scoreBehavior9 == "1") behave9_1 = behave9_1 + 1;
                        else if (aa.scoreBehavior9 == "0") behave9_0 = behave9_0 + 1;

                        if (aa.scoreBehavior10 == "3") behave10_3 = behave10_3 + 1;
                        else if (aa.scoreBehavior10 == "2") behave10_2 = behave10_2 + 1;
                        else if (aa.scoreBehavior10 == "1") behave10_1 = behave10_1 + 1;
                        else if (aa.scoreBehavior10 == "0") behave10_0 = behave10_0 + 1;

                        double totalScore = 0;

                        totalstudent = totalstudent + 1;

                        if (aa.getBehaviorLabel == "0")
                            Bzero = Bzero + 1;
                        else if (aa.getBehaviorLabel == "1")
                            Bone = Bone + 1;
                        else if (aa.getBehaviorLabel == "2")
                            Btwo = Btwo + 1;
                        else if (aa.getBehaviorLabel == "3")
                            Bthree = Bthree + 1;

                        if (aa.getReadWrite == "0")
                            Rzero = Rzero + 1;
                        else if (aa.getReadWrite == "1")
                            Rone = Rone + 1;
                        else if (aa.getReadWrite == "2")
                            Rtwo = Rtwo + 1;
                        else if (aa.getReadWrite == "3")
                            Rthree = Rthree + 1;

                        if (aa.getSamattana == "0")
                            SMzero = SMzero + 1;
                        else if (aa.getSamattana == "1")
                            SMone = SMone + 1;
                        else if (aa.getSamattana == "2")
                            SMtwo = SMtwo + 1;
                        else if (aa.getSamattana == "3")
                            SMthree = SMthree + 1;

                        if (aa.getSpecial != null && aa.getSpecial != "")
                        {
                            if (aa.getSpecial == "1")
                                ror = ror + 1;
                            if (aa.getSpecial == "2")
                                MS = MS + 1;
                            if (aa.getSpecial == "3")
                                MK = MK + 1;
                            if (aa.getSpecial == "4")
                                P = P + 1;
                            if (aa.getSpecial == "5")
                                MP = MP + 1;
                            if (aa.getSpecial == "6")
                                Other = Other + 1;
                        }

                        string total100 = !string.IsNullOrEmpty(aa.getScore100) ? aa.getScore100 : "0";// Common.getScore100(grade, aa);
                        totalScore = Double.Parse(total100);
                        xbar = xbar + totalScore;
                        x2 = x2 + (totalScore * totalScore);

                        if (totalScore > 79)
                            total40 = total40 + 1;
                        else if (totalScore < 80 && totalScore > 74)
                            total35 = total35 + 1;
                        else if (totalScore < 75 && totalScore > 69)
                            total30 = total30 + 1;
                        else if (totalScore < 70 && totalScore > 64)
                            total25 = total25 + 1;
                        else if (totalScore < 65 && totalScore > 59)
                            total20 = total20 + 1;
                        else if (totalScore < 60 && totalScore > 54)
                            total15 = total15 + 1;
                        else if (totalScore < 55 && totalScore > 49)
                            total10 = total10 + 1;
                        else total00 = total00 + 1;
                    }
                }
            }


            double totalstudentNoQuit = 0;
            double behave2term_0 = 0;
            double behave2term_1 = 0;
            double behave2term_2 = 0;
            double behave2term_3 = 0;
            double readwrite2term_0 = 0;
            double readwrite2term_1 = 0;
            double readwrite2term_2 = 0;
            double readwrite2term_3 = 0;
            double samattana2term_0 = 0;
            double samattana2term_1 = 0;
            double samattana2term_2 = 0;
            double samattana2term_3 = 0;
            double get40_2term = 0;
            double get35_2term = 0;
            double get30_2term = 0;
            double get25_2term = 0;
            double get20_2term = 0;
            double get15_2term = 0;
            double get10_2term = 0;
            double get0_2term = 0;
            double ror_2term = 0;
            double MS_2term = 0;
            double MK_2term = 0;
            double P_2term = 0;
            double MP_2term = 0;
            double other_2term = 0;



            if (gradeid_term1 != null && gradeid_term2 != null)
            {
                var q_data1 = _db.TUsers.Where(w => (w.cDel ?? "0") != "1").ToList();
                foreach (var aa in _db.TGradeDetails.Where(w => w.nGradeId == grade.nGradeId && w.cDel == false))
                {
                    var data1 = q_data1.FirstOrDefault(f => f.sID == aa.sID);
                    if (data1 == null) continue;
                    if (data1.nStudentStatus == null || data1.nStudentStatus == 0 || data1.nStudentStatus == 4)
                    {
                        totalstudentNoQuit = totalstudentNoQuit + 1;

                        var grade2 = _db.TGrades.Where(w => w.nTerm == nterm2 && w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == idlv2n).FirstOrDefault();

                        if (grade2 != null)
                        {
                            var detail1 = _db.TGradeDetails.Where(w => w.nGradeId == grade.nGradeId && w.sID == data1.sID).FirstOrDefault();
                            var detail2 = _db.TGradeDetails.Where(w => w.nGradeId == grade2.nGradeId && w.sID == data1.sID).FirstOrDefault();
                            if (detail2 == null)
                            {
                                var room3 = (from n2 in _db.TRoomChanges
                                             orderby n2.RoomChangeID descending
                                             where n2.sID == data1.sID
                                             select n2).FirstOrDefault();
                                if (room3 != null)
                                {
                                    var newgrade = _db.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == room3.Level2Old && w.nTerm == nterm2).FirstOrDefault();
                                    if (newgrade != null)
                                        detail2 = _db.TGradeDetails.Where(w => w.nGradeId == newgrade.nGradeId && w.sID == data1.sID).FirstOrDefault();
                                }
                            }
                            if (detail1 == null)
                            {
                                var room3 = (from n2 in _db.TRoomChanges
                                             orderby n2.RoomChangeID descending
                                             where n2.sID == data1.sID
                                             select n2).FirstOrDefault();
                                if (room3 != null)
                                {
                                    var newgrade = _db.TGrades.Where(w => w.sPlaneID == gradeid_term1.SPlaneId && w.nTermSubLevel2 == room3.Level2Old && w.nTerm == nterm).FirstOrDefault();
                                    if (newgrade != null)
                                        detail1 = _db.TGradeDetails.Where(w => w.nGradeId == newgrade.nGradeId && w.sID == data1.sID).FirstOrDefault();
                                }
                            }
                            if (detail1 != null && detail2 != null)
                            {
                                if ((string.IsNullOrEmpty(detail2.getScore100) || detail2.getScore100 == "0") && string.IsNullOrEmpty(detail2.scoreMidTerm) && string.IsNullOrEmpty(detail2.scoreFinalTerm) &&
                                            detail2.getSpecial == "-1")
                                {
                                    var room3 = (from n2 in _db.TRoomChanges
                                                 orderby n2.sID descending
                                                 where n2.sID == data1.sID
                                                 select n2).FirstOrDefault();
                                    if (room3 != null)
                                    {
                                        var newgrade = _db.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == room3.Level2Old && w.nTerm == nterm2).FirstOrDefault();
                                        if (newgrade != null)
                                        {
                                            var newdetail2 = _db.TGradeDetails.Where(w => w.nGradeId == newgrade.nGradeId && w.sID == data1.sID).FirstOrDefault();
                                            if (newdetail2 != null)
                                                detail2 = newdetail2;
                                        }
                                    }
                                }

                                if ((string.IsNullOrEmpty(detail1.getScore100) || detail1.getScore100 == "0") &&
                                            string.IsNullOrEmpty(detail1.scoreMidTerm) && string.IsNullOrEmpty(detail1.scoreFinalTerm) &&
                                            detail1.getSpecial == "-1")
                                {
                                    var room3 = (from n2 in _db.TRoomChanges
                                                 orderby n2.RoomChangeID descending
                                                 where n2.sID == data1.sID
                                                 select n2).FirstOrDefault();
                                    if (room3 != null)
                                    {
                                        var newgrade = _db.TGrades.Where(w => w.sPlaneID == gradeid_term1.SPlaneId && w.nTermSubLevel2 == room3.Level2Old && w.nTerm == nterm).FirstOrDefault();
                                        if (newgrade != null)
                                        {
                                            var newdetail1 = _db.TGradeDetails.Where(w => w.nGradeId == newgrade.nGradeId).FirstOrDefault();
                                            if (newdetail1 != null)
                                                detail1 = newdetail1;
                                        }
                                    }
                                }


                                string getBehave = "";
                                int check1;
                                int check2;
                                bool checkInt1 = int.TryParse(detail1.getBehaviorLabel, out check1);
                                bool checkInt2 = int.TryParse(detail2.getBehaviorLabel, out check2);

                                if (userterm == "1")
                                {
                                    if (checkInt2 == true)
                                        getBehave = detail2.getBehaviorLabel;
                                }
                                else
                                {
                                    if (checkInt1 == true)
                                        getBehave = detail1.getBehaviorLabel;
                                }
                                                                    

                                if (getBehave == "0")
                                    behave2term_0 = behave2term_0 + 1;
                                else if (getBehave == "1")
                                    behave2term_1 = behave2term_1 + 1;
                                else if (getBehave == "2")
                                    behave2term_2 = behave2term_2 + 1;
                                else if (getBehave == "3")
                                    behave2term_3 = behave2term_3 + 1;

                                string getReadwrite = "";
                                checkInt1 = int.TryParse(detail1.getReadWrite, out check1);
                                checkInt2 = int.TryParse(detail2.getReadWrite, out check2);

                                if (userterm == "1")
                                {
                                    if (checkInt2 == true)
                                        getReadwrite = detail2.getReadWrite;
                                }
                                else
                                {
                                    if (checkInt1 == true)
                                        getReadwrite = detail1.getReadWrite;
                                }
                                

                                if (getReadwrite == "0")
                                    readwrite2term_0 = readwrite2term_0 + 1;
                                else if (getReadwrite == "1")
                                    readwrite2term_1 = readwrite2term_1 + 1;
                                else if (getReadwrite == "2")
                                    readwrite2term_2 = readwrite2term_2 + 1;
                                else if (getReadwrite == "3")
                                    readwrite2term_3 = readwrite2term_3 + 1;

                                string getSamattana = "";
                                checkInt1 = int.TryParse(detail1.getSamattana, out check1);
                                checkInt2 = int.TryParse(detail2.getSamattana, out check2);

                                if (userterm == "1")
                                {
                                    if (checkInt2 == true)
                                        getSamattana = detail2.getSamattana;
                                }
                                else
                                {
                                    if (checkInt1 == true)
                                        getSamattana = detail1.getSamattana;
                                }                                

                                if (getSamattana == "0")
                                    samattana2term_0 = samattana2term_0 + 1;
                                else if (getSamattana == "1")
                                    samattana2term_1 = samattana2term_1 + 1;
                                else if (getSamattana == "2")
                                    samattana2term_2 = samattana2term_2 + 1;
                                else if (getSamattana == "3")
                                    samattana2term_3 = samattana2term_3 + 1;

                                if (detail1.getSpecial == "-1" && detail2.getSpecial == "-1")
                                {
                                    string get100_term1 = !string.IsNullOrEmpty(detail1.getScore100) ? detail1.getScore100 : "0";// Common.getScore100(grade, detail1);
                                    string get100_term2 = !string.IsNullOrEmpty(detail2.getScore100) ? detail2.getScore100 : "0";// Common.getScore100(grade2, detail2);

                                    double sum = double.Parse(get100_term1) + double.Parse(get100_term2);
                                    sum = sum / 2;
                                    int sumint = (int)Math.Round(sum, MidpointRounding.AwayFromZero);

                                    if (sumint > 79.99)
                                        get40_2term = get40_2term + 1;
                                    else if (sumint > 74.99)
                                        get35_2term = get35_2term + 1;
                                    else if (sumint > 69.99)
                                        get30_2term = get30_2term + 1;
                                    else if (sumint > 64.99)
                                        get25_2term = get25_2term + 1;
                                    else if (sumint > 59.99)
                                        get20_2term = get20_2term + 1;
                                    else if (sumint > 54.99)
                                        get15_2term = get15_2term + 1;
                                    else if (sumint > 49.99)
                                        get10_2term = get10_2term + 1;
                                    else get0_2term = get0_2term + 1;
                                }
                                else
                                {
                                    if (detail1.getSpecial == "1" || detail2.getSpecial == "1")
                                        ror_2term = ror_2term + 1;
                                    else if (detail1.getSpecial == "2" || detail2.getSpecial == "2")
                                        MS_2term = MS_2term + 1;
                                    else if (detail1.getSpecial == "3" || detail2.getSpecial == "3")
                                        MK_2term = MK_2term + 1;
                                    else if (detail1.getSpecial == "4" || detail2.getSpecial == "4")
                                        P_2term = P_2term + 1;
                                    else if (detail1.getSpecial == "5" || detail2.getSpecial == "5")
                                        MP_2term = MP_2term + 1;
                                    else if (detail1.getSpecial == "6" || detail2.getSpecial == "6")
                                        other_2term = other_2term + 1;
                                }
                            }
                        }

                    }

                }


            }


            if (get40_2term != 0) std40per_2.Text = Math.Round(((get40_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std40per_2);
            if (get35_2term != 0) std35per_2.Text = Math.Round(((get35_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std35per_2);
            if (get30_2term != 0) std30per_2.Text = Math.Round(((get30_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std30per_2);
            if (get25_2term != 0) std25per_2.Text = Math.Round(((get25_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std25per_2);
            if (get20_2term != 0) std20per_2.Text = Math.Round(((get20_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std20per_2);
            if (get15_2term != 0) std15per_2.Text = Math.Round(((get15_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std15per_2);
            if (get10_2term != 0) std10per_2.Text = Math.Round(((get10_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std10per_2);
            if (get0_2term != 0) std0per_2.Text = Math.Round(((get0_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(std0per_2);
            if (ror_2term != 0) stdrorper_2.Text = Math.Round(((ror_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdrorper_2);
            if (MS_2term != 0) stdmsper_2.Text = Math.Round(((MS_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdmsper_2);
            if (MK_2term != 0) stdmkper_2.Text = Math.Round(((MK_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdmkper_2);
            if (P_2term != 0) stdpper_2.Text = Math.Round(((P_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdpper_2);
            if (MP_2term != 0) stdmpper_2.Text = Math.Round(((MP_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdmpper_2);
            if (other_2term != 0) stdotherper_2.Text = Math.Round(((other_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(stdotherper_2);

            if (get40_2term != 0) cover4g40per.Text = Math.Round(((get40_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g40per);
            if (get35_2term != 0) cover4g35per.Text = Math.Round(((get35_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g35per);
            if (get30_2term != 0) cover4g30per.Text = Math.Round(((get30_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g30per);
            if (get25_2term != 0) cover4g25per.Text = Math.Round(((get25_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g25per);
            if (get20_2term != 0) cover4g20per.Text = Math.Round(((get20_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g20per);
            if (get15_2term != 0) cover4g15per.Text = Math.Round(((get15_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g15per);
            if (get10_2term != 0) cover4g10per.Text = Math.Round(((get10_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g10per);
            if (get0_2term != 0) cover4g0per.Text = Math.Round(((get0_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4g0per);
            if (ror_2term != 0) cover4Rorper.Text = Math.Round(((ror_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4Rorper);
            if (MS_2term != 0) cover4MSper.Text = Math.Round(((MS_2term * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4MSper);


            txttotalstudent_2.Text = totalstudentNoQuit.ToString();

            cover4Sum.Text = totalstudentNoQuit.ToString();
            cover4Sumper.Text = "100.00";


            if (ror_2term != 0) stdror_2.Text = ror_2term.ToString();
            else DashText(stdror_2);
            if (MS_2term != 0) stdms_2.Text = MS_2term.ToString();
            else DashText(stdms_2);
            if (MK_2term != 0) stdmk_2.Text = MK_2term.ToString();
            else DashText(stdmk_2);
            if (P_2term != 0) stdp_2.Text = P_2term.ToString();
            else DashText(stdp_2);
            if (MP_2term != 0) stdmp_2.Text = MP_2term.ToString();
            else DashText(stdmp_2);
            if (other_2term != 0) stdother_2.Text = other_2term.ToString();
            else DashText(stdother_2);

            if (ror_2term != 0) cover4Ror.Text = ror_2term.ToString();
            else DashText(cover4Ror);
            if (MS_2term != 0) cover4MS.Text = MS_2term.ToString();
            else DashText(cover4MS);



            if (behave2term_3 != 0) coverType3_11_2.Text = behave2term_3.ToString();
            else DashText(coverType3_11_2);
            if (behave2term_2 != 0) coverType3_13_2.Text = behave2term_2.ToString();
            else DashText(coverType3_13_2);
            if (behave2term_1 != 0) coverType3_15_2.Text = behave2term_1.ToString();
            else DashText(coverType3_15_2);
            if (behave2term_1 != 0) coverType3_16_2.Text = Math.Round(((behave2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_16_2);
            if (behave2term_2 != 0) coverType3_14_2.Text = Math.Round(((behave2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_14_2);
            if (behave2term_3 != 0) coverType3_12_2.Text = Math.Round(((behave2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_12_2);

            if (behave2term_3 != 0) cover4behave3.Text = behave2term_3.ToString();
            else DashText(cover4behave3);
            if (behave2term_2 != 0) cover4behave2.Text = behave2term_2.ToString();
            else DashText(cover4behave2);
            if (behave2term_1 != 0) cover4behave1.Text = behave2term_1.ToString();
            else DashText(cover4behave1);
            if (behave2term_3 != 0) cover4behave3per.Text = Math.Round(((behave2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4behave3per);
            if (behave2term_2 != 0) cover4behave2per.Text = Math.Round(((behave2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4behave2per);
            if (behave2term_1 != 0) cover4behave1per.Text = Math.Round(((behave2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4behave1per);

            if (samattana2term_3 != 0) coverType3_31_2.Text = samattana2term_3.ToString();
            else DashText(coverType3_31_2);
            if (samattana2term_2 != 0) coverType3_33_2.Text = samattana2term_2.ToString();
            else DashText(coverType3_33_2);
            if (samattana2term_1 != 0) coverType3_35_2.Text = samattana2term_1.ToString();
            else DashText(coverType3_35_2);
            if (samattana2term_1 != 0) coverType3_36_2.Text = Math.Round(((samattana2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_36_2);
            if (samattana2term_2 != 0) coverType3_34_2.Text = Math.Round(((samattana2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_34_2);
            if (samattana2term_3 != 0) coverType3_32_2.Text = Math.Round(((samattana2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_32_2);

            if (samattana2term_3 != 0) cover4samat3.Text = samattana2term_3.ToString();
            else DashText(cover4samat3);
            if (samattana2term_2 != 0) cover4samat2.Text = samattana2term_2.ToString();
            else DashText(cover4samat2);
            if (samattana2term_1 != 0) cover4samat1.Text = samattana2term_1.ToString();
            else DashText(cover4samat1);
            if (samattana2term_3 != 0) cover4samat3per.Text = Math.Round(((samattana2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4samat3per);
            if (samattana2term_2 != 0) cover4samat2per.Text = Math.Round(((samattana2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4samat2per);
            if (samattana2term_1 != 0) cover4samat1per.Text = Math.Round(((samattana2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4samat1per);


            if (readwrite2term_3 != 0) coverType3_21_2.Text = readwrite2term_3.ToString();
            else DashText(coverType3_21_2);
            if (readwrite2term_2 != 0) coverType3_23_2.Text = readwrite2term_2.ToString();
            else DashText(coverType3_23_2);
            if (readwrite2term_1 != 0) coverType3_25_2.Text = readwrite2term_1.ToString();
            else DashText(coverType3_25_2);
            if (readwrite2term_1 != 0) coverType3_26_2.Text = Math.Round(((readwrite2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_26_2);
            if (readwrite2term_2 != 0) coverType3_24_2.Text = Math.Round(((readwrite2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_24_2);
            if (readwrite2term_3 != 0) coverType3_22_2.Text = Math.Round(((readwrite2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(coverType3_22_2);

            if (readwrite2term_3 != 0) cover4read3.Text = readwrite2term_3.ToString();
            else DashText(cover4read3);
            if (readwrite2term_2 != 0) cover4read2.Text = readwrite2term_2.ToString();
            else DashText(cover4read2);
            if (readwrite2term_1 != 0) cover4read1.Text = readwrite2term_1.ToString();
            else DashText(cover4read1);
            if (readwrite2term_3 != 0) cover4read3per.Text = Math.Round(((readwrite2term_3 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4read3per);
            if (readwrite2term_2 != 0) cover4read2per.Text = Math.Round(((readwrite2term_2 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4read2per);
            if (readwrite2term_1 != 0) cover4read1per.Text = Math.Round(((readwrite2term_1 * 100) / totalstudentNoQuit), 2).ToString("0.00");
            else DashText(cover4read1per);

            if (get40_2term != 0) std40_2.Text = get40_2term.ToString();
            else DashText(std40_2);
            if (get35_2term != 0) std35_2.Text = get35_2term.ToString();
            else DashText(std35_2);
            if (get30_2term != 0) std30_2.Text = get30_2term.ToString();
            else DashText(std30_2);
            if (get25_2term != 0) std25_2.Text = get25_2term.ToString();
            else DashText(std25_2);
            if (get20_2term != 0) std20_2.Text = get20_2term.ToString();
            else DashText(std20_2);
            if (get15_2term != 0) std15_2.Text = get15_2term.ToString();
            else DashText(std15_2);
            if (get10_2term != 0) std10_2.Text = get10_2term.ToString();
            else DashText(std10_2);
            if (get0_2term != 0) std0_2.Text = get0_2term.ToString();
            else DashText(std0_2);

            if (get40_2term != 0) cover4g40.Text = get40_2term.ToString();
            else DashText(cover4g40);
            if (get35_2term != 0) cover4g35.Text = get35_2term.ToString();
            else DashText(cover4g35);
            if (get30_2term != 0) cover4g30.Text = get30_2term.ToString();
            else DashText(cover4g30);
            if (get25_2term != 0) cover4g25.Text = get25_2term.ToString();
            else DashText(cover4g25);
            if (get20_2term != 0) cover4g20.Text = get20_2term.ToString();
            else DashText(cover4g20);
            if (get15_2term != 0) cover4g15.Text = get15_2term.ToString();
            else DashText(cover4g15);
            if (get10_2term != 0) cover4g10.Text = get10_2term.ToString();
            else DashText(cover4g10);
            if (get0_2term != 0) cover4g0.Text = get0_2term.ToString();
            else DashText(cover4g0);


            if (total40 != 0) std40.Text = total40.ToString();
            else DashText(std40);
            if (total40 != 0) cover2grade4.Text = total40.ToString();
            else DashText(cover2grade4);
            if (total35 != 0) std35.Text = total35.ToString();
            else DashText(std35);
            if (total35 != 0) cover2grade35.Text = total35.ToString();
            else DashText(cover2grade35);
            if (total30 != 0) std30.Text = total30.ToString();
            else DashText(std30);
            if (total30 != 0) cover2grade3.Text = total30.ToString();
            else DashText(cover2grade3);
            if (total25 != 0) std25.Text = total25.ToString();
            else DashText(std25);
            if (total25 != 0) cover2grade25.Text = total25.ToString();
            else DashText(cover2grade25);
            if (total20 != 0) std20.Text = total20.ToString();
            else DashText(std20);
            if (total20 != 0) cover2grade2.Text = total20.ToString();
            else DashText(cover2grade2);
            if (total15 != 0) std15.Text = total15.ToString();
            else DashText(std15);
            if (total15 != 0) cover2grade15.Text = total15.ToString();
            else DashText(cover2grade15);
            if (total10 != 0) std10.Text = total10.ToString();
            else DashText(std10);
            if (total10 != 0) cover2grade1.Text = total10.ToString();
            else DashText(cover2grade1);
            if (total00 != 0) std0.Text = total00.ToString();
            else DashText(std0);
            if (total00 != 0) cover2grade0.Text = total00.ToString();
            else DashText(cover2grade0);

            if (totalstudent != 0) txttotalstudent.Text = totalstudent.ToString();
            else DashText(txttotalstudent);
            if (ror != 0) stdror.Text = ror.ToString();
            else DashText(stdror);
            if (MS != 0) stdms.Text = MS.ToString();
            else DashText(stdms);
            if (MK != 0) stdmk.Text = MK.ToString();
            else DashText(stdmk);
            if (P != 0) stdp.Text = P.ToString();
            else DashText(stdp);
            if (MP != 0) stdmp.Text = MP.ToString();
            else DashText(stdmp);
            if (Other != 0) stdother.Text = Other.ToString();
            else DashText(stdother);

            cover4g40_2.Text = std40.Text;
            cover4g35_2.Text = std35.Text;
            cover4g30_2.Text = std30.Text;
            cover4g25_2.Text = std25.Text;
            cover4g20_2.Text = std20.Text;
            cover4g15_2.Text = std15.Text;
            cover4g10_2.Text = std10.Text;
            cover4g0_2.Text = std0.Text;
            cover4Ror_2.Text = stdror.Text;
            cover4MS_2.Text = stdms.Text;

            cover4Sum_2.Text = totalstudent.ToString();
            cover4Sumper_2.Text = "100.00";

            if (cover4g40_2.Text.Length == 1)
                cover4g40_2.CssClass = "cover4style1 bigdash";
            if (cover4g35_2.Text.Length == 1)
                cover4g35_2.CssClass = "cover4style1 bigdash";
            if (cover4g30_2.Text.Length == 1)
                cover4g30_2.CssClass = "cover4style1 bigdash";
            if (cover4g25_2.Text.Length == 1)
                cover4g25_2.CssClass = "cover4style1 bigdash";
            if (cover4g20_2.Text.Length == 1)
                cover4g20_2.CssClass = "cover4style1 bigdash";
            if (cover4g15_2.Text.Length == 1)
                cover4g15_2.CssClass = "cover4style1 bigdash";
            if (cover4g10_2.Text.Length == 1)
                cover4g10_2.CssClass = "cover4style1 bigdash";
            if (cover4g0_2.Text.Length == 1)
                cover4g0_2.CssClass = "cover4style1 bigdash";
            if (cover4Ror_2.Text.Length == 1)
                cover4Ror_2.CssClass = "cover4style1 bigdash";
            if (cover4MS_2.Text.Length == 1)
                cover4MS_2.CssClass = "cover4style1 bigdash";

            if (total40 != 0) std40per.Text = Math.Round(((total40 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std40per);
            if (total35 != 0) std35per.Text = Math.Round(((total35 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std35per);
            if (total30 != 0) std30per.Text = Math.Round(((total30 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std30per);
            if (total25 != 0) std25per.Text = Math.Round(((total25 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std25per);
            if (total20 != 0) std20per.Text = Math.Round(((total20 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std20per);
            if (total15 != 0) std15per.Text = Math.Round(((total15 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std15per);
            if (total10 != 0) std10per.Text = Math.Round(((total10 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std10per);
            if (total00 != 0) std0per.Text = Math.Round(((total00 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(std0per);
            if (total40 != 0) cover2per4.Text = Math.Round(((total40 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per4);
            if (total35 != 0) cover2per35.Text = Math.Round(((total35 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per35);
            if (total30 != 0) cover2per3.Text = Math.Round(((total30 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per3);
            if (total25 != 0) cover2per25.Text = Math.Round(((total25 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per25);
            if (total20 != 0) cover2per2.Text = Math.Round(((total20 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per2);
            if (total15 != 0) cover2per15.Text = Math.Round(((total15 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per15);
            if (total10 != 0) cover2per1.Text = Math.Round(((total10 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per1);
            if (total00 != 0) cover2per0.Text = Math.Round(((total00 * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2per0);
            if (ror != 0) stdrorper.Text = Math.Round(((ror * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdrorper);
            if (MS != 0) stdmsper.Text = Math.Round(((MS * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdmsper);
            if (MK != 0) stdmkper.Text = Math.Round(((MK * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdmkper);
            if (P != 0) stdpper.Text = Math.Round(((P * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdpper);
            if (MP != 0) stdmpper.Text = Math.Round(((MP * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdmpper);
            if (Other != 0) stdotherper.Text = Math.Round(((Other * 100) / totalstudent), 2).ToString("0.00");
            else DashText(stdotherper);

            cover4g40per_2.Text = std40per.Text;
            cover4g35per_2.Text = std35per.Text;
            cover4g30per_2.Text = std30per.Text;
            cover4g25per_2.Text = std25per.Text;
            cover4g20per_2.Text = std20per.Text;
            cover4g15per_2.Text = std15per.Text;
            cover4g10per_2.Text = std10per.Text;
            cover4g0per_2.Text = std0per.Text;
            cover4Rorper_2.Text = stdrorper.Text;
            cover4MSper_2.Text = stdmsper.Text;
            if (cover4g40per_2.Text.Length == 1)
                cover4g40per_2.CssClass = "cover4style1 bigdash";
            if (cover4g35per_2.Text.Length == 1)
                cover4g35per_2.CssClass = "cover4style1 bigdash";
            if (cover4g30per_2.Text.Length == 1)
                cover4g30per_2.CssClass = "cover4style1 bigdash";
            if (cover4g25per_2.Text.Length == 1)
                cover4g25per_2.CssClass = "cover4style1 bigdash";
            if (cover4g20per_2.Text.Length == 1)
                cover4g20per_2.CssClass = "cover4style1 bigdash";
            if (cover4g15per_2.Text.Length == 1)
                cover4g15per_2.CssClass = "cover4style1 bigdash";
            if (cover4g10per_2.Text.Length == 1)
                cover4g10per_2.CssClass = "cover4style1 bigdash";
            if (cover4Rorper_2.Text.Length == 1)
                cover4Rorper_2.CssClass = "cover4style1 bigdash";
            if (cover4g0per_2.Text.Length == 1)
                cover4g0per_2.CssClass = "cover4style1 bigdash";
            if (cover4MSper_2.Text.Length == 1)
                cover4MSper_2.CssClass = "cover4style1 bigdash";

            if (Bone != 0) behavior1.Text = Bone.ToString();
            else DashText(behavior1);
            if (Btwo != 0) behavior2.Text = Btwo.ToString();
            else DashText(behavior2);
            if (Bthree != 0) behavior3.Text = Bthree.ToString();
            else DashText(behavior3);
            if (Bzero != 0) behavior0.Text = Bzero.ToString();
            else DashText(behavior0);
            if (Bthree != 0) coverType3_11.Text = Bthree.ToString();
            else DashText(coverType3_11);
            if (Btwo != 0) coverType3_13.Text = Btwo.ToString();
            else DashText(coverType3_13);
            if (Bone != 0) coverType3_15.Text = Bone.ToString();
            else DashText(coverType3_15);




            if (Bone != 0) behavior1per.Text = Math.Round(((Bone * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(behavior1per);
            if (Btwo != 0) behavior2per.Text = Math.Round(((Btwo * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(behavior2per);
            if (Bthree != 0) behavior3per.Text = Math.Round(((Bthree * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(behavior3per);
            if (Bone != 0) coverType3_16.Text = Math.Round(((Bone * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_16);
            if (Btwo != 0) coverType3_14.Text = Math.Round(((Btwo * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_14);
            if (Bthree != 0) coverType3_12.Text = Math.Round(((Bthree * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_12);
            if (Bzero != 0) behavior0per.Text = Math.Round(((Bzero * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(behavior0per);



            if (SMthree != 0) coverType3_31.Text = SMthree.ToString();
            else DashText(coverType3_31);
            if (SMtwo != 0) coverType3_33.Text = SMtwo.ToString();
            else DashText(coverType3_33);
            if (SMone != 0) coverType3_35.Text = SMone.ToString();
            else DashText(coverType3_35);


            if (SMone != 0) coverType3_36.Text = Math.Round(((SMone * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_36);
            if (SMtwo != 0) coverType3_34.Text = Math.Round(((SMtwo * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_34);
            if (SMthree != 0) coverType3_32.Text = Math.Round(((SMthree * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_32);

            if (Rone != 0) reading1.Text = Rone.ToString();
            else DashText(reading1);
            if (Rone != 0) cover2read1.Text = Rone.ToString();
            else DashText(cover2read1);
            if (Rtwo != 0) reading2.Text = Rtwo.ToString();
            else DashText(reading2);
            if (Rtwo != 0) cover2read2.Text = Rtwo.ToString();
            else DashText(cover2read2);
            if (Rthree != 0) reading3.Text = Rthree.ToString();
            else DashText(reading3);
            if (Rthree != 0) cover2read3.Text = Rthree.ToString();
            else DashText(cover2read3);
            if (Rzero != 0) reading0.Text = Rzero.ToString();
            else DashText(reading0);
            if (Rzero != 0) cover2read0.Text = Rzero.ToString();
            else DashText(cover2read0);
            if (Rthree != 0) coverType3_21.Text = Rthree.ToString();
            else DashText(coverType3_21);
            if (Rtwo != 0) coverType3_23.Text = Rtwo.ToString();
            else DashText(coverType3_23);
            if (Rone != 0) coverType3_25.Text = Rone.ToString();
            else DashText(coverType3_25);

            cover4behave3_2.Text = coverType3_11.Text;
            cover4behave2_2.Text = coverType3_13.Text;
            cover4behave1_2.Text = coverType3_15.Text;
            cover4read3_2.Text = coverType3_21.Text;
            cover4read2_2.Text = coverType3_23.Text;
            cover4read1_2.Text = coverType3_25.Text;
            cover4samat3_2.Text = coverType3_31.Text;
            cover4samat2_2.Text = coverType3_33.Text;
            cover4samat1_2.Text = coverType3_35.Text;
            if (cover4behave3_2.Text.Length == 1)
                cover4behave3_2.CssClass = "cover4style1 bigdash";
            if (cover4behave2_2.Text.Length == 1)
                cover4behave2_2.CssClass = "cover4style1 bigdash";
            if (cover4behave1_2.Text.Length == 1)
                cover4behave1_2.CssClass = "cover4style1 bigdash";
            if (cover4read3_2.Text.Length == 1)
                cover4read3_2.CssClass = "cover4style1 bigdash";
            if (cover4read2_2.Text.Length == 1)
                cover4read2_2.CssClass = "cover4style1 bigdash";
            if (cover4read1_2.Text.Length == 1)
                cover4read1_2.CssClass = "cover4style1 bigdash";
            if (cover4samat3_2.Text.Length == 1)
                cover4samat3_2.CssClass = "cover4style1 bigdash";
            if (cover4samat2_2.Text.Length == 1)
                cover4samat2_2.CssClass = "cover4style1 bigdash";
            if (cover4samat1_2.Text.Length == 1)
                cover4samat1_2.CssClass = "cover4style1 bigdash";

            if (Rone != 0) reading1per.Text = Math.Round(((Rone * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(reading1per);
            if (Rone != 0) cover2perread1.Text = Math.Round(((Rone * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2perread1);
            if (Rtwo != 0) reading2per.Text = Math.Round(((Rtwo * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(reading2per);
            if (Rtwo != 0) cover2perread2.Text = Math.Round(((Rtwo * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2perread2);
            if (Rthree != 0) reading3per.Text = Math.Round(((Rthree * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(reading3per);
            if (Rthree != 0) cover2perread3.Text = Math.Round(((Rthree * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2perread3);
            if (Rzero != 0) reading0per.Text = Math.Round(((Rzero * 100) / totalstudent), 2).ToString("0.00") + " %";
            else DashText(reading0per);
            if (Rzero != 0) cover2perread0.Text = Math.Round(((Rzero * 100) / totalstudent), 2).ToString("0.00");
            else DashText(cover2perread0);
            if (Rone != 0) coverType3_26.Text = Math.Round(((Rone * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_26);
            if (Rtwo != 0) coverType3_24.Text = Math.Round(((Rtwo * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_24);
            if (Rthree != 0) coverType3_22.Text = Math.Round(((Rthree * 100) / totalstudent), 2).ToString("0.00");
            else DashText(coverType3_22);

            cover4behave3per_2.Text = coverType3_12.Text;
            cover4behave2per_2.Text = coverType3_14.Text;
            cover4behave1per_2.Text = coverType3_16.Text;
            cover4read3per_2.Text = coverType3_22.Text;
            cover4read2per_2.Text = coverType3_24.Text;
            cover4read1per_2.Text = coverType3_26.Text;
            cover4samat3per_2.Text = coverType3_32.Text;
            cover4samat2per_2.Text = coverType3_34.Text;
            cover4samat1per_2.Text = coverType3_36.Text;
            if (cover4behave3per_2.Text.Length == 1)
                cover4behave3per_2.CssClass = "cover4style1 bigdash";
            if (cover4behave2per_2.Text.Length == 1)
                cover4behave2per_2.CssClass = "cover4style1 bigdash";
            if (cover4behave1per_2.Text.Length == 1)
                cover4behave1per_2.CssClass = "cover4style1 bigdash";
            if (cover4read3per_2.Text.Length == 1)
                cover4read3per_2.CssClass = "cover4style1 bigdash";
            if (cover4read2per_2.Text.Length == 1)
                cover4read2per_2.CssClass = "cover4style1 bigdash";
            if (cover4read1per_2.Text.Length == 1)
                cover4read1per_2.CssClass = "cover4style1 bigdash";
            if (cover4samat3per_2.Text.Length == 1)
                cover4samat3per_2.CssClass = "cover4style1 bigdash";
            if (cover4samat2per_2.Text.Length == 1)
                cover4samat2per_2.CssClass = "cover4style1 bigdash";
            if (cover4samat1per_2.Text.Length == 1)
                cover4samat1per_2.CssClass = "cover4style1 bigdash";

            pageheader.Text = totalstudent.ToString();

            totalstudentcheck.Text = totalstudent.ToString();
            cover2totalstudent.Text = totalstudent.ToString();
            cover2behavestudent.Text = totalstudent.ToString();
            cover2behave11.Text = grade.nameBehavior1;
            cover2behave21.Text = grade.nameBehavior2;
            cover2behave31.Text = grade.nameBehavior3;
            cover2behave41.Text = grade.nameBehavior4;
            cover2behave51.Text = grade.nameBehavior5;
            cover2behave61.Text = grade.nameBehavior6;
            cover2behave71.Text = grade.nameBehavior7;
            cover2behave81.Text = grade.nameBehavior8;
            if (grade.maxBehavior9 != null || grade.maxBehavior9 != "")
                cover2behave91.Text = grade.nameBehavior9;
            else cover2behave91.Text = "";
            if (grade.maxBehavior10 != null || grade.maxBehavior10 != "")
                cover2behave101.Text = grade.nameBehavior10;
            else cover2behave101.Text = "";

            if (behave1_3 != 0) cover2behave12.Text = behave1_3.ToString();
            else DashText(cover2behave12);
            if (behave1_2 != 0) cover2behave13.Text = behave1_2.ToString();
            else DashText(cover2behave13);
            if (behave1_1 != 0) cover2behave14.Text = behave1_1.ToString();
            else DashText(cover2behave14);
            if (behave1_0 != 0) cover2behave15.Text = behave1_0.ToString();
            else DashText(cover2behave15);
            if (t2behave1_3 != 0) cover2behave16.Text = t2behave1_3.ToString();
            else DashText(cover2behave16);
            if (t2behave1_2 != 0) cover2behave17.Text = t2behave1_2.ToString();
            else DashText(cover2behave17);
            if (t2behave1_1 != 0) cover2behave18.Text = t2behave1_1.ToString();
            else DashText(cover2behave18);
            if (t2behave1_0 != 0) cover2behave19.Text = t2behave1_0.ToString();
            else DashText(cover2behave19);

            if (behave2_3 != 0) cover2behave22.Text = behave2_3.ToString();
            else DashText(cover2behave22);
            if (behave2_2 != 0) cover2behave23.Text = behave2_2.ToString();
            else DashText(cover2behave23);
            if (behave2_1 != 0) cover2behave24.Text = behave2_1.ToString();
            else DashText(cover2behave24);
            if (behave2_0 != 0) cover2behave25.Text = behave2_0.ToString();
            else DashText(cover2behave25);
            if (t2behave2_3 != 0) cover2behave26.Text = t2behave2_3.ToString();
            else DashText(cover2behave26);
            if (t2behave2_2 != 0) cover2behave27.Text = t2behave2_2.ToString();
            else DashText(cover2behave27);
            if (t2behave2_1 != 0) cover2behave28.Text = t2behave2_1.ToString();
            else DashText(cover2behave28);
            if (t2behave2_0 != 0) cover2behave29.Text = t2behave2_0.ToString();
            else DashText(cover2behave29);

            if (behave3_3 != 0) cover2behave32.Text = behave3_3.ToString();
            else DashText(cover2behave32);
            if (behave3_2 != 0) cover2behave33.Text = behave3_2.ToString();
            else DashText(cover2behave33);
            if (behave3_1 != 0) cover2behave34.Text = behave3_1.ToString();
            else DashText(cover2behave34);
            if (behave3_0 != 0) cover2behave35.Text = behave3_0.ToString();
            else DashText(cover2behave35);
            if (t2behave3_3 != 0) cover2behave36.Text = t2behave3_3.ToString();
            else DashText(cover2behave36);
            if (t2behave3_2 != 0) cover2behave37.Text = t2behave3_2.ToString();
            else DashText(cover2behave37);
            if (t2behave3_1 != 0) cover2behave38.Text = t2behave3_1.ToString();
            else DashText(cover2behave38);
            if (t2behave3_0 != 0) cover2behave39.Text = t2behave3_0.ToString();
            else DashText(cover2behave39);

            if (behave4_3 != 0) cover2behave42.Text = behave4_3.ToString();
            else DashText(cover2behave42);
            if (behave4_2 != 0) cover2behave43.Text = behave4_2.ToString();
            else DashText(cover2behave43);
            if (behave4_1 != 0) cover2behave44.Text = behave4_1.ToString();
            else DashText(cover2behave44);
            if (behave4_0 != 0) cover2behave45.Text = behave4_0.ToString();
            else DashText(cover2behave45);
            if (t2behave4_3 != 0) cover2behave46.Text = t2behave4_3.ToString();
            else DashText(cover2behave46);
            if (t2behave4_2 != 0) cover2behave47.Text = t2behave4_2.ToString();
            else DashText(cover2behave47);
            if (t2behave4_1 != 0) cover2behave48.Text = t2behave4_1.ToString();
            else DashText(cover2behave48);
            if (t2behave4_0 != 0) cover2behave49.Text = t2behave4_0.ToString();
            else DashText(cover2behave49);

            if (behave5_3 != 0) cover2behave52.Text = behave5_3.ToString();
            else DashText(cover2behave52);
            if (behave5_2 != 0) cover2behave53.Text = behave5_2.ToString();
            else DashText(cover2behave53);
            if (behave5_1 != 0) cover2behave54.Text = behave5_1.ToString();
            else DashText(cover2behave54);
            if (behave5_0 != 0) cover2behave55.Text = behave5_0.ToString();
            else DashText(cover2behave55);
            if (t2behave5_3 != 0) cover2behave56.Text = t2behave5_3.ToString();
            else DashText(cover2behave56);
            if (t2behave5_2 != 0) cover2behave57.Text = t2behave5_2.ToString();
            else DashText(cover2behave57);
            if (t2behave5_1 != 0) cover2behave58.Text = t2behave5_1.ToString();
            else DashText(cover2behave58);
            if (t2behave5_0 != 0) cover2behave59.Text = t2behave5_0.ToString();
            else DashText(cover2behave59);

            if (behave6_3 != 0) cover2behave62.Text = behave6_3.ToString();
            else DashText(cover2behave62);
            if (behave6_2 != 0) cover2behave63.Text = behave6_2.ToString();
            else DashText(cover2behave63);
            if (behave6_1 != 0) cover2behave64.Text = behave6_1.ToString();
            else DashText(cover2behave64);
            if (behave6_0 != 0) cover2behave65.Text = behave6_0.ToString();
            else DashText(cover2behave65);
            if (t2behave6_3 != 0) cover2behave66.Text = t2behave6_3.ToString();
            else DashText(cover2behave66);
            if (t2behave6_2 != 0) cover2behave67.Text = t2behave6_2.ToString();
            else DashText(cover2behave67);
            if (t2behave6_1 != 0) cover2behave68.Text = t2behave6_1.ToString();
            else DashText(cover2behave68);
            if (t2behave6_0 != 0) cover2behave69.Text = t2behave6_0.ToString();
            else DashText(cover2behave69);

            if (behave7_3 != 0) cover2behave72.Text = behave7_3.ToString();
            else DashText(cover2behave72);
            if (behave7_2 != 0) cover2behave73.Text = behave7_2.ToString();
            else DashText(cover2behave73);
            if (behave7_1 != 0) cover2behave74.Text = behave7_1.ToString();
            else DashText(cover2behave74);
            if (behave7_0 != 0) cover2behave75.Text = behave7_0.ToString();
            else DashText(cover2behave75);
            if (t2behave7_3 != 0) cover2behave76.Text = t2behave7_3.ToString();
            else DashText(cover2behave76);
            if (t2behave7_2 != 0) cover2behave77.Text = t2behave7_2.ToString();
            else DashText(cover2behave77);
            if (t2behave7_1 != 0) cover2behave78.Text = t2behave7_1.ToString();
            else DashText(cover2behave78);
            if (t2behave7_0 != 0) cover2behave79.Text = t2behave7_0.ToString();
            else DashText(cover2behave79);

            if (behave8_3 != 0) cover2behave82.Text = behave8_3.ToString();
            else DashText(cover2behave82);
            if (behave8_2 != 0) cover2behave83.Text = behave8_2.ToString();
            else DashText(cover2behave83);
            if (behave8_1 != 0) cover2behave84.Text = behave8_1.ToString();
            else DashText(cover2behave84);
            if (behave8_0 != 0) cover2behave85.Text = behave8_0.ToString();
            else DashText(cover2behave85);
            if (t2behave8_3 != 0) cover2behave86.Text = t2behave8_3.ToString();
            else DashText(cover2behave86);
            if (t2behave8_2 != 0) cover2behave87.Text = t2behave8_2.ToString();
            else DashText(cover2behave87);
            if (t2behave8_1 != 0) cover2behave88.Text = t2behave8_1.ToString();
            else DashText(cover2behave88);
            if (t2behave8_0 != 0) cover2behave89.Text = t2behave8_0.ToString();
            else DashText(cover2behave89);

            if (behave9_3 != 0) { cover2behave92.Text = behave9_3.ToString(); }
            else DashText(cover2behave92);
            if (behave9_2 != 0) { cover2behave93.Text = behave9_2.ToString(); }
            else DashText(cover2behave93);
            if (behave9_1 != 0) { cover2behave94.Text = behave9_1.ToString(); }
            else DashText(cover2behave94);
            if (behave9_0 != 0) { cover2behave95.Text = behave9_0.ToString(); }
            else DashText(cover2behave95);
            if (t2behave9_3 != 0) { cover2behave96.Text = t2behave9_3.ToString(); }
            else DashText(cover2behave96);
            if (t2behave9_2 != 0) { cover2behave97.Text = t2behave9_2.ToString(); }
            else DashText(cover2behave97);
            if (t2behave9_1 != 0) { cover2behave98.Text = t2behave9_1.ToString(); }
            else DashText(cover2behave98);
            if (t2behave9_0 != 0) { cover2behave99.Text = t2behave9_0.ToString(); }
            else DashText(cover2behave99);

            if (behave10_3 != 0) { cover2behave102.Text = behave10_3.ToString(); }
            else DashText(cover2behave102);
            if (behave10_2 != 0) { cover2behave103.Text = behave10_2.ToString(); }
            else DashText(cover2behave103);
            if (behave10_1 != 0) { cover2behave104.Text = behave10_1.ToString(); }
            else DashText(cover2behave104);
            if (behave10_0 != 0) { cover2behave105.Text = behave10_0.ToString(); }
            else DashText(cover2behave105);
            if (t2behave10_3 != 0) { cover2behave106.Text = t2behave10_3.ToString(); }
            else DashText(cover2behave106);
            if (t2behave10_2 != 0) { cover2behave107.Text = t2behave10_2.ToString(); }
            else DashText(cover2behave107);
            if (t2behave10_1 != 0) { cover2behave108.Text = t2behave10_1.ToString(); }
            else DashText(cover2behave108);
            if (t2behave10_0 != 0) { cover2behave109.Text = t2behave10_0.ToString(); }
            else DashText(cover2behave109);

            if (grade.maxBehavior9 == null)
            {
                cover2behavehide.Text = "0";
            }
            else
            {
                if (grade.maxBehavior9 == "" || grade.maxBehavior9 == "0")
                {
                    cover2behavehide.Text = "0";
                }
                else
                {
                    cover2behavehide.Text = "1";
                }
            }

            if (grade.maxBehavior10 == null)
            {
                cover2hide2.Text = "0";
            }
            else
            {
                if (grade.maxBehavior10 == "" || grade.maxBehavior10 == "0")
                {
                    cover2hide2.Text = "0";
                }
                else
                {
                    cover2hide2.Text = "1";
                }
            }
        }

        //protected string getScore100(TB_GradeViews grade, TB_GradeDetailView detail)
        //{
        //    string txt = "";


        //    double quizmax = 0;
        //    double midmax = 0;
        //    double finalmax = 0;
        //    double quizget = 0;
        //    double midget = 0;
        //    double finalget = 0;

        //    double ratioquiz = grade.fRatioQuiz;
        //    double ratiomid = grade.fRatioMidTerm;
        //    double ratiofinal = grade.fRatioLateTerm;

        //    bool checkMidMax = double.TryParse(grade.maxMidTerm, out midmax);
        //    bool checkFinalMax = double.TryParse(grade.maxFinalTerm, out finalmax);
        //    bool checkMidGet = double.TryParse(detail.scoreMidTerm, out midget);
        //    bool checkFinalGet = double.TryParse(detail.scoreFinalTerm, out finalget);

        //    double g1;
        //    double g2;
        //    double g3;
        //    double g4;
        //    double g5;
        //    double g6;
        //    double g7;
        //    double g8;
        //    double g9;
        //    double g10;
        //    double g11;
        //    double g12;
        //    double g13;
        //    double g14;
        //    double g15;
        //    double g16;
        //    double g17;
        //    double g18;
        //    double g19;
        //    double g20;

        //    double cw1;
        //    double cw2;
        //    double cw3;
        //    double cw4;
        //    double cw5;
        //    double cw6;
        //    double cw7;
        //    double cw8;
        //    double cw9;
        //    double cw10;
        //    double cw11;
        //    double cw12;
        //    double cw13;
        //    double cw14;
        //    double cw15;
        //    double cw16;
        //    double cw17;
        //    double cw18;
        //    double cw19;
        //    double cw20;

        //    bool g1n = double.TryParse(grade.maxGrade1, out g1);
        //    bool g2n = double.TryParse(grade.maxGrade2, out g2);
        //    bool g3n = double.TryParse(grade.maxGrade3, out g3);
        //    bool g4n = double.TryParse(grade.maxGrade4, out g4);
        //    bool g5n = double.TryParse(grade.maxGrade5, out g5);
        //    bool g6n = double.TryParse(grade.maxGrade6, out g6);
        //    bool g7n = double.TryParse(grade.maxGrade7, out g7);
        //    bool g8n = double.TryParse(grade.maxGrade8, out g8);
        //    bool g9n = double.TryParse(grade.maxGrade9, out g9);
        //    bool g10n = double.TryParse(grade.maxGrade10, out g10);
        //    bool g11n = double.TryParse(grade.maxGrade11, out g11);
        //    bool g12n = double.TryParse(grade.maxGrade12, out g12);
        //    bool g13n = double.TryParse(grade.maxGrade13, out g13);
        //    bool g14n = double.TryParse(grade.maxGrade14, out g14);
        //    bool g15n = double.TryParse(grade.maxGrade15, out g15);
        //    bool g16n = double.TryParse(grade.maxGrade16, out g16);
        //    bool g17n = double.TryParse(grade.maxGrade17, out g17);
        //    bool g18n = double.TryParse(grade.maxGrade18, out g18);
        //    bool g19n = double.TryParse(grade.maxGrade19, out g19);
        //    bool g20n = double.TryParse(grade.maxGrade20, out g20);

        //    bool cw1n = double.TryParse(grade.maxCheewat1, out cw1);
        //    bool cw2n = double.TryParse(grade.maxCheewat2, out cw2);
        //    bool cw3n = double.TryParse(grade.maxCheewat3, out cw3);
        //    bool cw4n = double.TryParse(grade.maxCheewat4, out cw4);
        //    bool cw5n = double.TryParse(grade.maxCheewat5, out cw5);
        //    bool cw6n = double.TryParse(grade.maxCheewat6, out cw6);
        //    bool cw7n = double.TryParse(grade.maxCheewat7, out cw7);
        //    bool cw8n = double.TryParse(grade.maxCheewat8, out cw8);
        //    bool cw9n = double.TryParse(grade.maxCheewat9, out cw9);
        //    bool cw10n = double.TryParse(grade.maxCheewat10, out cw10);
        //    bool cw11n = double.TryParse(grade.maxCheewat11, out cw11);
        //    bool cw12n = double.TryParse(grade.maxCheewat12, out cw12);
        //    bool cw13n = double.TryParse(grade.maxCheewat13, out cw13);
        //    bool cw14n = double.TryParse(grade.maxCheewat14, out cw14);
        //    bool cw15n = double.TryParse(grade.maxCheewat15, out cw15);
        //    bool cw16n = double.TryParse(grade.maxCheewat16, out cw16);
        //    bool cw17n = double.TryParse(grade.maxCheewat17, out cw17);
        //    bool cw18n = double.TryParse(grade.maxCheewat18, out cw18);
        //    bool cw19n = double.TryParse(grade.maxCheewat19, out cw19);
        //    bool cw20n = double.TryParse(grade.maxCheewat20, out cw20);

        //    if (g1n == true)
        //        quizmax = quizmax + g1;
        //    if (g2n == true)
        //        quizmax = quizmax + g2;
        //    if (g3n == true)
        //        quizmax = quizmax + g3;
        //    if (g4n == true)
        //        quizmax = quizmax + g4;
        //    if (g5n == true)
        //        quizmax = quizmax + g5;
        //    if (g6n == true)
        //        quizmax = quizmax + g6;
        //    if (g7n == true)
        //        quizmax = quizmax + g7;
        //    if (g8n == true)
        //        quizmax = quizmax + g8;
        //    if (g9n == true)
        //        quizmax = quizmax + g9;
        //    if (g10n == true)
        //        quizmax = quizmax + g10;
        //    if (g11n == true)
        //        quizmax = quizmax + g11;
        //    if (g12n == true)
        //        quizmax = quizmax + g12;
        //    if (g13n == true)
        //        quizmax = quizmax + g13;
        //    if (g14n == true)
        //        quizmax = quizmax + g14;
        //    if (g15n == true)
        //        quizmax = quizmax + g15;
        //    if (g16n == true)
        //        quizmax = quizmax + g16;
        //    if (g17n == true)
        //        quizmax = quizmax + g17;
        //    if (g18n == true)
        //        quizmax = quizmax + g18;
        //    if (g19n == true)
        //        quizmax = quizmax + g19;
        //    if (g20n == true)
        //        quizmax = quizmax + g20;

        //    if (cw1n == true)
        //        quizmax = quizmax + cw1;
        //    if (cw2n == true)
        //        quizmax = quizmax + cw2;
        //    if (cw3n == true)
        //        quizmax = quizmax + cw3;
        //    if (cw4n == true)
        //        quizmax = quizmax + cw4;
        //    if (cw5n == true)
        //        quizmax = quizmax + cw5;
        //    if (cw6n == true)
        //        quizmax = quizmax + cw6;
        //    if (cw7n == true)
        //        quizmax = quizmax + cw7;
        //    if (cw8n == true)
        //        quizmax = quizmax + cw8;
        //    if (cw9n == true)
        //        quizmax = quizmax + cw9;
        //    if (cw10n == true)
        //        quizmax = quizmax + cw10;
        //    if (cw11n == true)
        //        quizmax = quizmax + cw11;
        //    if (cw12n == true)
        //        quizmax = quizmax + cw12;
        //    if (cw13n == true)
        //        quizmax = quizmax + cw13;
        //    if (cw14n == true)
        //        quizmax = quizmax + cw14;
        //    if (cw15n == true)
        //        quizmax = quizmax + cw15;
        //    if (cw16n == true)
        //        quizmax = quizmax + cw16;
        //    if (cw17n == true)
        //        quizmax = quizmax + cw17;
        //    if (cw18n == true)
        //        quizmax = quizmax + cw18;
        //    if (cw19n == true)
        //        quizmax = quizmax + cw19;
        //    if (cw20n == true)
        //        quizmax = quizmax + cw20;


        //    double get1;
        //    double get2;
        //    double get3;
        //    double get4;
        //    double get5;
        //    double get6;
        //    double get7;
        //    double get8;
        //    double get9;
        //    double get10;
        //    double get11;
        //    double get12;
        //    double get13;
        //    double get14;
        //    double get15;
        //    double get16;
        //    double get17;
        //    double get18;
        //    double get19;
        //    double get20;

        //    double getcw1;
        //    double getcw2;
        //    double getcw3;
        //    double getcw4;
        //    double getcw5;
        //    double getcw6;
        //    double getcw7;
        //    double getcw8;
        //    double getcw9;
        //    double getcw10;
        //    double getcw11;
        //    double getcw12;
        //    double getcw13;
        //    double getcw14;
        //    double getcw15;
        //    double getcw16;
        //    double getcw17;
        //    double getcw18;
        //    double getcw19;
        //    double getcw20;

        //    bool get1n = double.TryParse(detail.scoreGrade1, out get1);
        //    bool get2n = double.TryParse(detail.scoreGrade2, out get2);
        //    bool get3n = double.TryParse(detail.scoreGrade3, out get3);
        //    bool get4n = double.TryParse(detail.scoreGrade4, out get4);
        //    bool get5n = double.TryParse(detail.scoreGrade5, out get5);
        //    bool get6n = double.TryParse(detail.scoreGrade6, out get6);
        //    bool get7n = double.TryParse(detail.scoreGrade7, out get7);
        //    bool get8n = double.TryParse(detail.scoreGrade8, out get8);
        //    bool get9n = double.TryParse(detail.scoreGrade9, out get9);
        //    bool get10n = double.TryParse(detail.scoreGrade10, out get10);
        //    bool get11n = double.TryParse(detail.scoreGrade11, out get11);
        //    bool get12n = double.TryParse(detail.scoreGrade12, out get12);
        //    bool get13n = double.TryParse(detail.scoreGrade13, out get13);
        //    bool get14n = double.TryParse(detail.scoreGrade14, out get14);
        //    bool get15n = double.TryParse(detail.scoreGrade15, out get15);
        //    bool get16n = double.TryParse(detail.scoreGrade16, out get16);
        //    bool get17n = double.TryParse(detail.scoreGrade17, out get17);
        //    bool get18n = double.TryParse(detail.scoreGrade18, out get18);
        //    bool get19n = double.TryParse(detail.scoreGrade19, out get19);
        //    bool get20n = double.TryParse(detail.scoreGrade20, out get20);

        //    bool getcw1n = double.TryParse(detail.scoreCheewat1, out getcw1);
        //    bool getcw2n = double.TryParse(detail.scoreCheewat2, out getcw2);
        //    bool getcw3n = double.TryParse(detail.scoreCheewat3, out getcw3);
        //    bool getcw4n = double.TryParse(detail.scoreCheewat4, out getcw4);
        //    bool getcw5n = double.TryParse(detail.scoreCheewat5, out getcw5);
        //    bool getcw6n = double.TryParse(detail.scoreCheewat6, out getcw6);
        //    bool getcw7n = double.TryParse(detail.scoreCheewat7, out getcw7);
        //    bool getcw8n = double.TryParse(detail.scoreCheewat8, out getcw8);
        //    bool getcw9n = double.TryParse(detail.scoreCheewat9, out getcw9);
        //    bool getcw10n = double.TryParse(detail.scoreCheewat10, out getcw10);
        //    bool getcw11n = double.TryParse(detail.scoreCheewat11, out getcw11);
        //    bool getcw12n = double.TryParse(detail.scoreCheewat12, out getcw12);
        //    bool getcw13n = double.TryParse(detail.scoreCheewat13, out getcw13);
        //    bool getcw14n = double.TryParse(detail.scoreCheewat14, out getcw14);
        //    bool getcw15n = double.TryParse(detail.scoreCheewat15, out getcw15);
        //    bool getcw16n = double.TryParse(detail.scoreCheewat16, out getcw16);
        //    bool getcw17n = double.TryParse(detail.scoreCheewat17, out getcw17);
        //    bool getcw18n = double.TryParse(detail.scoreCheewat18, out getcw18);
        //    bool getcw19n = double.TryParse(detail.scoreCheewat19, out getcw19);
        //    bool getcw20n = double.TryParse(detail.scoreCheewat20, out getcw20);

        //    if (get1n == true)
        //        quizget = quizget + get1;
        //    if (get2n == true)
        //        quizget = quizget + get2;
        //    if (get3n == true)
        //        quizget = quizget + get3;
        //    if (get4n == true)
        //        quizget = quizget + get4;
        //    if (get5n == true)
        //        quizget = quizget + get5;
        //    if (get6n == true)
        //        quizget = quizget + get6;
        //    if (get7n == true)
        //        quizget = quizget + get7;
        //    if (get8n == true)
        //        quizget = quizget + get8;
        //    if (get9n == true)
        //        quizget = quizget + get9;
        //    if (get10n == true)
        //        quizget = quizget + get10;
        //    if (get11n == true)
        //        quizget = quizget + get11;
        //    if (get12n == true)
        //        quizget = quizget + get12;
        //    if (get13n == true)
        //        quizget = quizget + get13;
        //    if (get14n == true)
        //        quizget = quizget + get14;
        //    if (get15n == true)
        //        quizget = quizget + get15;
        //    if (get16n == true)
        //        quizget = quizget + get16;
        //    if (get17n == true)
        //        quizget = quizget + get17;
        //    if (get18n == true)
        //        quizget = quizget + get18;
        //    if (get19n == true)
        //        quizget = quizget + get19;
        //    if (get20n == true)
        //        quizget = quizget + get20;

        //    if (getcw1n == true)
        //        quizget = quizget + getcw1;
        //    if (getcw2n == true)
        //        quizget = quizget + getcw2;
        //    if (getcw3n == true)
        //        quizget = quizget + getcw3;
        //    if (getcw4n == true)
        //        quizget = quizget + getcw4;
        //    if (getcw5n == true)
        //        quizget = quizget + getcw5;
        //    if (getcw6n == true)
        //        quizget = quizget + getcw6;
        //    if (getcw7n == true)
        //        quizget = quizget + getcw7;
        //    if (getcw8n == true)
        //        quizget = quizget + getcw8;
        //    if (getcw9n == true)
        //        quizget = quizget + getcw9;
        //    if (getcw10n == true)
        //        quizget = quizget + getcw10;
        //    if (getcw11n == true)
        //        quizget = quizget + getcw11;
        //    if (getcw12n == true)
        //        quizget = quizget + getcw12;
        //    if (getcw13n == true)
        //        quizget = quizget + getcw13;
        //    if (getcw14n == true)
        //        quizget = quizget + getcw14;
        //    if (getcw15n == true)
        //        quizget = quizget + getcw15;
        //    if (getcw16n == true)
        //        quizget = quizget + getcw16;
        //    if (getcw17n == true)
        //        quizget = quizget + getcw17;
        //    if (getcw18n == true)
        //        quizget = quizget + getcw18;
        //    if (getcw19n == true)
        //        quizget = quizget + getcw19;
        //    if (getcw20n == true)
        //        quizget = quizget + getcw20;

        //    double quiz100 = 0;
        //    double mid100 = 0;
        //    double final100 = 0;

        //    if (quizmax != 0)
        //        quiz100 = (quizget * ratioquiz) / quizmax;
        //    if (midmax != 0)
        //        mid100 = (midget * ratiomid) / midmax;
        //    if (finalmax != 0)
        //        final100 = (finalget * ratiofinal) / finalmax;

        //    int quizn = int.Parse(quiz100.ToString("0"));
        //    int midn = int.Parse(mid100.ToString("0"));
        //    int finaln = int.Parse(final100.ToString("0"));

        //    int sum = quizn + midn + finaln;

        //    txt = sum.ToString();



        //    return txt;
        //}

        string monthtxt(int month)
        {
            string txt = "";
            if (month == 1)
                txt = "มกราคม";
            else if (month == 2)
                txt = "กุมภาพันธ์";
            else if (month == 3)
                txt = "มีนาคม";
            else if (month == 4)
                txt = "เมษายน";
            else if (month == 5)
                txt = "พฤษภาคม";
            else if (month == 6)
                txt = "มิถุนายน";
            else if (month == 7)
                txt = "กรกฎาคม";
            else if (month == 8)
                txt = "สิงหาคม";
            else if (month == 9)
                txt = "กันยายน";
            else if (month == 10)
                txt = "ตุลาคม";
            else if (month == 11)
                txt = "พฤศจิกายน";
            else if (month == 12)
                txt = "ธันวาคม";
            return txt;
        }
        protected string GetMax(string first, string second)
        {
            int one = int.Parse(first);
            int two = int.Parse(second);

            if (one > two)
            {
                return first;
            }
            else return second;
        }

        protected void DashText(Label box)
        {
            box.Text = "-";
            box.CssClass = box.CssClass + " bigdash";
        }
    }

}