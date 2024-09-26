using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.ViewModels;
using FingerprintPayment.Class;

namespace FingerprintPayment.PreRegister
{
    public partial class preRegisterList : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
          

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                importSave.Click += new EventHandler(import_Click);
                btnExport.Click += new EventHandler(ExportToExcel);
                importSave2.Click += new EventHandler(import_Click2);
                deleteBtn.Click += new EventHandler(delete);
                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                string sEntities = Session["sEntities"] + "";
                string url = HttpContext.Current.Request.Url.AbsoluteUri;

                Button1.Click += new EventHandler(importAll_Click);


                if (!IsPostBack)
                {
                    OpenData();
                    url = url.Insert(4, "s");
                    genlink.Text = url + "/" + "registerUser.aspx?id=" + Rot13.Transform(sEntities);

                    string yearr = Request.QueryString["year"];
                    DropDownList1.SelectedValue = yearr;
                    string mode = Request.QueryString["mode"];
                    DropDownList2.SelectedValue = mode;

                    string branch = Request.QueryString["branch"];
                    string time = Request.QueryString["time"];
                    string type = Request.QueryString["type"];
                    string idlv = Request.QueryString["idlv"];
                    string name = Request.QueryString["name"];
                    string planID = Request.QueryString["planID"];

                    if (idlv != "" && idlv != null)
                    {
                        int idlvn = int.Parse(idlv);
                        var sublv = _db.TSubLevels.Where(w => w.nTSubLevel == idlvn && w.nWorkingStatus == 1).FirstOrDefault();

                        ddlCourseTime.SelectedValue = time;
                        ddlCourseType.SelectedValue = type;
                        if (idlv == "0")
                        {
                            optionLevel.SelectedValue = "0/0";
                            Textbox4.Text = "0/0";
                        }
                        else
                        {
                            optionLevel.SelectedValue = sublv.nTSubLevel.ToString() + "/" + sublv.nTLevel.ToString().Trim();
                            Textbox4.Text = sublv.nTSubLevel.ToString() + "/" + sublv.nTLevel.ToString().Trim();
                        }

                        tags2.Text = branch;
                        tags.Text = name;
                    }


                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();

                    foreach (var a in _db.TSubLevels.Where(w => w.nWorkingStatus == 1))
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
                        editidlv.Items.Add(item);
                        DropDownList4.Items.Add(item);
                    }

                    foreach (var t in SubLevel)
                    {
                        var item = new ListItem
                        {
                            Text = t.SubLevel,
                            Value = t.nTSubLevel.ToString() + "/" + t.nTLevel.ToString()
                        };
                        optionLevel.Items.Add(item);
                    }



                    List<TYear> tylist = new List<TYear>();
                    TYear ty = new TYear();
                    foreach (var a in _db.TYears.Where(w => w.cDel == false).ToList())
                    {
                        ty = new TYear();
                        ty = a;
                        tylist.Add(ty);
                    }

                    var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                    DropDownList1.DataSource = newList;
                    DropDownList1.DataTextField = "numberYear";
                    DropDownList1.DataValueField = "numberYear";
                    DropDownList1.DataBind();

                    ddlyear.DataSource = newList;
                    ddlyear.DataTextField = "numberYear";
                    ddlyear.DataValueField = "numberYear";
                    ddlyear.DataBind();

                    DropDownList8.DataSource = newList;
                    DropDownList8.DataTextField = "numberYear";
                    DropDownList8.DataValueField = "numberYear";
                    DropDownList8.DataBind();

                    if (!string.IsNullOrEmpty(planID) || !string.IsNullOrEmpty(idlv))
                    {
                        int idlvn = int.Parse(idlv);
                        var planList = _db.TRegisterPlanSetups.Where(w => w.nTSubLevel == idlvn).Select(s => new EntityPlanSetupEduProgram { ID = s.RegisterPlanSetupID, Planname = s.PlanName }).OrderBy(o => o.ID).ToList();
                        ddlPlan.DataSource = planList;
                        ddlPlan.DataTextField = "Planname";
                        ddlPlan.DataValueField = "ID";
                        ddlPlan.DataBind();

                        ddlPlan.SelectedValue = planID;

                        ListItem li = new ListItem();
                        li.Text = "ทั้งหมด";
                        li.Value = "";
                        //this line add list item at list top (index 0) position
                        ddlPlan.Items.Insert(0, li);
                    }

                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityPlanSetupEduProgram> LoadPlan(string planID)
        {
            List<EntityPlanSetupEduProgram> result = null;

            if (!string.IsNullOrEmpty(planID))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));

                int pID = Convert.ToInt32(planID);
                result = dbschool.TRegisterPlanSetups.Where(w => w.nTSubLevel == pID).Select(s => new EntityPlanSetupEduProgram { ID = s.RegisterPlanSetupID, Planname = s.PlanName }).ToList();
            }

            return result;
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=preRegisterStudentReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == entities).FirstOrDefault();


                List<exportExcel> exportExcelList = new List<exportExcel>();
                exportExcel exportExcel = new exportExcel();
                int count = 0;
                foreach (var data1 in dbschool.TPreRegisters.Where(w => w.cDel == null))
                {
                    count = count + 1;
                    exportExcel = new exportExcel();
                    exportExcel.number = count;
                    //exportExcel.registerYear = data1.registerYear;
                    exportExcel.registerYear = data1.registerYear + 543;

                    if (data1.optionBranch != null)
                    {
                        var b = dbschool.TBranchSpecs.Where(w => w.BranchSpecId == data1.optionBranch).FirstOrDefault();
                        if (b != null)
                            exportExcel.optionBranch = b.BranchSpecName;
                        else exportExcel.optionBranch = "";
                    }
                    else exportExcel.optionBranch = "";

                    if (data1.optionTime == 1)
                        exportExcel.optionTime = "รอบเช้า";
                    else if (data1.optionTime == 2)
                        exportExcel.optionTime = "บ่าย 1";
                    else if (data1.optionTime == 3)
                        exportExcel.optionTime = "บ่าย 2";

                    if (data1.optionCourse == 1)
                        exportExcel.optionCourse = "ปกติ";
                    else if (data1.optionCourse == 3)
                        exportExcel.optionCourse = "ทวิภาคี";

                    exportExcel.planName = "";
                    if (data1.RegisterPlanSetupID != null)
                    {
                        var plan = dbschool.TRegisterPlanSetups.Where(w => w.RegisterPlanSetupID == data1.RegisterPlanSetupID && w.nTSubLevel == data1.optionLevel).FirstOrDefault();
                        if (plan != null)
                        {
                            exportExcel.planName = plan.PlanName;
                        }
                    }

                    var room = dbschool.TSubLevels.Where(w => w.nTSubLevel == data1.optionLevel).FirstOrDefault();
                    exportExcel.optionLevel = room.SubLevel;

                    exportExcel.sStudentID = data1.sStudentID;

                    exportExcel.examCode = data1.ExamCode;

                    if (data1.cSex == "0")
                        exportExcel.cSex = "ชาย";
                    else if (data1.cSex == "1")
                        exportExcel.cSex = "หญิง";

                    string stitle = "";
                    var stdtitle = dbschool.TTitleLists.Where(w => w.nTitleid == data1.StudentTitle).FirstOrDefault();
                    if (stdtitle != null)
                        stitle = stdtitle.titleDescription;

                    exportExcel.sName = stitle + data1.sName + " " + data1.sLastname;
                    exportExcel.sNameEN = data1.sStudentNameEN + " " + data1.sStudentLastEN;
                    exportExcel.sNickname = data1.sNickName;
                    exportExcel.sStudentIdCardNumber = data1.sStudentIdCardNumber; // รหัสปปช
                    if (data1.dBirth != null)
                        exportExcel.dBirth = data1.dBirth.Value.Day.ToString() + " " + monthtxt(data1.dBirth.Value.Month) + " พ.ศ." + data1.dBirth.Value.AddYears(543).Year.ToString();
                    exportExcel.sStudentRace = data1.sStudentRace;
                    exportExcel.sStudentNation = data1.sStudentNation;
                    exportExcel.sStudentReligion = data1.sStudentReligion;
                    exportExcel.nSonNumber = data1.nSonNumber.ToString();
                    exportExcel.sStudentHomeNumber = data1.sStudentHomeNumber;
                    exportExcel.sStudentSoy = data1.sStudentSoy;
                    exportExcel.sStudentMuu = data1.sStudentMuu;
                    exportExcel.sStudentRoad = data1.sStudentRoad;

                    int n;
                    bool isNumeric = int.TryParse(data1.sStudentProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                            exportExcel.sStudentProvince = userpro.PROVINCE_NAME;
                    }

                    isNumeric = int.TryParse(data1.sStudentAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                            exportExcel.sStudentAumpher = useraum.AMPHUR_NAME;
                    }

                    isNumeric = int.TryParse(data1.sStudentTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                            exportExcel.sStudentTumbon = usertum.DISTRICT_NAME;
                    }

                    exportExcel.sStudentPost = data1.sStudentPost;
                    exportExcel.sPhone = data1.sPhone; // houseRegistrationPhone, sStudentHousePhone
                    if (string.IsNullOrEmpty(exportExcel.sPhone))
                    {
                        exportExcel.sPhone = data1.houseRegistrationPhone;
                    }
                    if (string.IsNullOrEmpty(exportExcel.sPhone))
                    {
                        exportExcel.sPhone = data1.sStudentHousePhone;
                    }

                    exportExcel.sEmail = data1.sEmail;

                    exportExcel.nWeight = data1.nWeight.ToString();
                    exportExcel.nHeight = data1.nHeight.ToString();
                    exportExcel.sBlood = data1.sBlood;
                    exportExcel.sSickFood = data1.sSickFood;
                    exportExcel.sSickDrug = data1.sSickDrug;
                    exportExcel.sSickOther = data1.sSickOther;
                    exportExcel.sSickNormal = data1.sSickNormal;
                    exportExcel.sSickDanger = data1.sSickDanger;



                    if (data1.addDate != null)
                        exportExcel.addDate = data1.addDate.Value.Day.ToString() + " " + monthtxt(data1.addDate.Value.Month) + " พ.ศ." + data1.addDate.Value.AddYears(543).Year.ToString();
                    else exportExcel.addDate = "";

                    if (data1.moveInDate != null)
                        exportExcel.moveInDate = data1.moveInDate.Value.Day.ToString() + " " + monthtxt(data1.moveInDate.Value.Month) + " พ.ศ." + data1.moveInDate.Value.AddYears(543).Year.ToString();
                    else exportExcel.moveInDate = "";

                    if (data1.saveAsSID != null)
                    {
                        var data2 = dbschool.TUser.Where(w => w.sID == data1.saveAsSID).FirstOrDefault();
                        exportExcel.registerCode = data2.sStudentID;
                    }

                    if (data1.nTermSubLevel2 != null)
                    {
                        var room1 = dbschool.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data1.nTermSubLevel2).FirstOrDefault();
                        var sub1 = dbschool.TSubLevels.Where(w => w.nTSubLevel == room1.nTSubLevel).FirstOrDefault();

                        string txt = sub1.SubLevel + " / " + room1.nTSubLevel2;
                        exportExcel.nTermSubLevel2 = txt;
                    }

                    if (data1.paymentStatus == 0)
                        exportExcel.paymentStatus = "ยังไม่ชำระค่าสมัคร";
                    else if (data1.paymentStatus == 1)
                        exportExcel.paymentStatus = "ชำระค่าสมัครไม่ครบ";
                    else if (data1.paymentStatus == 2)
                        exportExcel.paymentStatus = "ชำระค่าสมัครแล้ว";

                    exportExcel.oldSchoolName = data1.oldSchoolName;

                    isNumeric = int.TryParse(data1.oldSchoolProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                            exportExcel.oldSchoolProvince = userpro.PROVINCE_NAME;
                    }

                    isNumeric = int.TryParse(data1.oldSchoolAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                            exportExcel.oldSchoolAumpher = useraum.AMPHUR_NAME;
                    }

                    isNumeric = int.TryParse(data1.oldSchoolTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                            exportExcel.oldSchoolTumbon = usertum.DISTRICT_NAME;
                    }

                    exportExcel.oldSchoolGPA = data1.oldSchoolGPA.ToString();

                    if (data1.oldSchoolGraduated == "1")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 1";
                    else if (data1.oldSchoolGraduated == "2")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 2";
                    else if (data1.oldSchoolGraduated == "3")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 3";
                    else if (data1.oldSchoolGraduated == "4")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 4";
                    else if (data1.oldSchoolGraduated == "5")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 5";
                    else if (data1.oldSchoolGraduated == "6")
                        exportExcel.oldSchoolGraduated = "ประถมศึกษาปีที่ 6";
                    else if (data1.oldSchoolGraduated == "7")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาตอนต้น";
                    else if (data1.oldSchoolGraduated == "8")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาตอนปลาย";
                    else if (data1.oldSchoolGraduated == "9")
                        exportExcel.oldSchoolGraduated = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1";
                    else if (data1.oldSchoolGraduated == "10")
                        exportExcel.oldSchoolGraduated = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1";
                    else if (data1.oldSchoolGraduated == "11")
                        exportExcel.oldSchoolGraduated = "เตรียมอนุบาลศึกษา";
                    else if (data1.oldSchoolGraduated == "12")
                        exportExcel.oldSchoolGraduated = "อนุบาลศึกษา 1";
                    else if (data1.oldSchoolGraduated == "13")
                        exportExcel.oldSchoolGraduated = "อนุบาลศึกษา 2";
                    else if (data1.oldSchoolGraduated == "14")
                        exportExcel.oldSchoolGraduated = "อนุบาลศึกษา 3";
                    else if (data1.oldSchoolGraduated == "15")
                        exportExcel.oldSchoolGraduated = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2";
                    else if (data1.oldSchoolGraduated == "16")
                        exportExcel.oldSchoolGraduated = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3";
                    else if (data1.oldSchoolGraduated == "17")
                        exportExcel.oldSchoolGraduated = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2";
                    else if (data1.oldSchoolGraduated == "18")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 1";
                    else if (data1.oldSchoolGraduated == "19")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 2";
                    else if (data1.oldSchoolGraduated == "20")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 3";
                    else if (data1.oldSchoolGraduated == "21")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 4";
                    else if (data1.oldSchoolGraduated == "22")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 5";
                    else if (data1.oldSchoolGraduated == "23")
                        exportExcel.oldSchoolGraduated = "มัธยมศึกษาปีที่ 6";

                    stitle = "";
                    var famtitle = dbschool.TTitleLists.Where(w => w.nTitleid == data1.nFamilyTitle).FirstOrDefault();
                    if (famtitle != null)
                        stitle = famtitle.titleDescription;
                    exportExcel.sFamilyName = stitle + data1.sFamilyName + " " + data1.sFamilyLast;

                    exportExcel.sFamilyIdCardNumber = data1.sFamilyIdCardNumber;
                    exportExcel.sFamilyRace = data1.sFamilyRace;
                    exportExcel.sFamilyNation = data1.sFamilyNation;
                    exportExcel.sFamilyReligion = data1.sFamilyReligion;
                    exportExcel.sFamilyHomeNumber = data1.sFamilyHomeNumber;
                    exportExcel.sFamilySoy = data1.sFamilySoy;
                    exportExcel.sFamilyMuu = data1.sFamilyMuu;
                    exportExcel.sFamilyRoad = data1.sFamilyRoad;

                    isNumeric = int.TryParse(data1.sFamilyProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                            exportExcel.sFamilyProvince = userpro.PROVINCE_NAME;
                    }

                    isNumeric = int.TryParse(data1.sFamilyAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                            exportExcel.sFamilyAumpher = useraum.AMPHUR_NAME;
                    }

                    isNumeric = int.TryParse(data1.sFamilyTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                            exportExcel.sFamilyTumbon = usertum.DISTRICT_NAME;
                    }

                    exportExcel.sFamilyPost = data1.sFamilyPost;
                    exportExcel.sPhoneOne = data1.sPhoneOne;
                    exportExcel.sPhoneTwo = data1.sPhoneTwo;
                    exportExcel.sPhoneThree = data1.sPhoneThree;
                    exportExcel.sFamilyRelate = data1.sFamilyRelate;

                    stitle = "";
                    var fatitle = dbschool.TTitleLists.Where(w => w.nTitleid == data1.FatherTitle).FirstOrDefault();
                    if (fatitle != null)
                        stitle = fatitle.titleDescription;

                    exportExcel.sFatherName = stitle + data1.sFatherFirstName + " " + data1.sFatherLastName;
                    exportExcel.sFatherIdCardNumber = data1.sFatherIdCardNumber;
                    exportExcel.sFatherRace = data1.sFatherRace;
                    exportExcel.sFatherNation = data1.sFatherNation;
                    exportExcel.sFatherReligion = data1.sFatherReligion;
                    exportExcel.sFatherHomeNumber = data1.sFatherHomeNumber;
                    exportExcel.sFatherSoy = data1.sFatherSoy;
                    exportExcel.sFatherMuu = data1.sFatherMuu;
                    exportExcel.sFatherRoad = data1.sFatherRoad;

                    isNumeric = int.TryParse(data1.sFatherProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                            exportExcel.sFatherProvince = userpro.PROVINCE_NAME;
                    }

                    isNumeric = int.TryParse(data1.sFatherAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                            exportExcel.sFatherAumpher = useraum.AMPHUR_NAME;
                    }

                    isNumeric = int.TryParse(data1.sFatherTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                            exportExcel.sFatherTumbon = usertum.DISTRICT_NAME;
                    }

                    exportExcel.sFatherPost = data1.sFatherPost;
                    exportExcel.sFatherPhone = data1.sFatherPhone;
                    exportExcel.fatherIncome = data1.fatherIncome; // nFatherIncome
                    if (string.IsNullOrEmpty(exportExcel.fatherIncome) && data1.nFatherIncome != null)
                    {
                        exportExcel.fatherIncome = data1.nFatherIncome.Value.ToString("#,0.00");
                    }

                    stitle = "";
                    var matitle = dbschool.TTitleLists.Where(w => w.nTitleid == data1.MotherTitle).FirstOrDefault();
                    if (matitle != null)
                        stitle = matitle.titleDescription;

                    exportExcel.Mothername = stitle + data1.sMotherFirstName + " " + data1.sMotherLastName;
                    exportExcel.sMotherIdCardNumber = data1.sMotherIdCardNumber;
                    exportExcel.sMotherRace = data1.sMotherRace;
                    exportExcel.sMotherNation = data1.sMotherNation;
                    exportExcel.sMotherReligion = data1.sMotherReligion;
                    exportExcel.sMotherHomeNumber = data1.sMotherHomeNumber;
                    exportExcel.sMotherSoy = data1.sMotherSoy;
                    exportExcel.sMotherMuu = data1.sMotherMuu;
                    exportExcel.sMotherRoad = data1.sMotherRoad;

                    isNumeric = int.TryParse(data1.sMotherProvince, out n);
                    if (isNumeric == true)
                    {
                        var userpro = _dbMaster.provinces.Where(w => w.PROVINCE_ID == n).FirstOrDefault();
                        if (userpro != null)
                            exportExcel.sMotherProvince = userpro.PROVINCE_NAME;
                    }

                    isNumeric = int.TryParse(data1.sMotherAumpher, out n);
                    if (isNumeric == true)
                    {
                        var useraum = _dbMaster.amphurs.Where(w => w.AMPHUR_ID == n).FirstOrDefault();
                        if (useraum != null)
                            exportExcel.sMotherAumpher = useraum.AMPHUR_NAME;
                    }

                    isNumeric = int.TryParse(data1.sMotherTumbon, out n);
                    if (isNumeric == true)
                    {
                        var usertum = _dbMaster.districts.Where(w => w.DISTRICT_ID == n).FirstOrDefault();
                        if (usertum != null)
                            exportExcel.sMotherTumbon = usertum.DISTRICT_NAME;
                    }

                    exportExcel.sMotherPost = data1.sMotherPost;
                    exportExcel.sMotherPhone = data1.sMotherPhone;
                    exportExcel.motherIncome = data1.motherIncome; // nMotherIncome
                    if (string.IsNullOrEmpty(exportExcel.motherIncome) && data1.nMotherIncome != null)
                    {
                        exportExcel.motherIncome = data1.nMotherIncome.Value.ToString("#,0.00");
                    }

                    if (data1.knowFrom1 == 1)
                        exportExcel.knowFrom1 = "ใช่";
                    else exportExcel.knowFrom1 = "";
                    if (data1.knowFrom2 == 1)
                        exportExcel.knowFrom2 = "ใช่";
                    else exportExcel.knowFrom2 = "";
                    if (data1.knowFrom3 == 1)
                        exportExcel.knowFrom3 = "ใช่";
                    else exportExcel.knowFrom3 = "";
                    if (data1.knowFrom4 == 1)
                        exportExcel.knowFrom4 = "ใช่";
                    else exportExcel.knowFrom4 = "";
                    if (data1.knowFrom5 == 1)
                        exportExcel.knowFrom5 = "ใช่";
                    else exportExcel.knowFrom5 = "";
                    if (data1.knowFrom6 == 1)
                        exportExcel.knowFrom6 = "ใช่";
                    else exportExcel.knowFrom6 = "";
                    if (data1.knowFrom7 == 1)
                        exportExcel.knowFrom7 = "ใช่";
                    else exportExcel.knowFrom7 = "";
                    if (data1.knowFrom8 == 1)
                        exportExcel.knowFrom8 = "ใช่";
                    else exportExcel.knowFrom8 = "";
                    if (data1.knowFrom9 == 1)
                        exportExcel.knowFrom9 = "ใช่";
                    else exportExcel.knowFrom9 = "";
                    if (data1.knowFrom10 == 1)
                        exportExcel.knowFrom10 = "ใช่";
                    else exportExcel.knowFrom10 = "";
                    if (data1.knowFrom11 == 1)
                        exportExcel.knowFrom11 = "ใช่";
                    else exportExcel.knowFrom11 = "";

                    exportExcelList.Add(exportExcel);
                }



                GridView1.DataSource = exportExcelList;
                GridView1.PageSize = 999;
                GridView1.DataBind();

                SetHeader(heaerType_1, GridView1, nCompany.sCompany).RenderControl(hw);

                //style to format numbers to string
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            }
        }
        List<string> heaerType_1 = new List<string> { "ลำดับ", "ปีการศึกษา", "สาขา", "รอบการเรียน", "หลักสูตร", "แผน", "ระดับ", "เลขที่ใบสมัคร", "เลขที่สอบ","เพศ", "ชื่อ-นามสกุล", "ชื่อ-นามสกุล (อังกฤษ)"
        ,"ชื่อเล่น","รหัสประจำตัวประชาชน","วันเกิด","เชื้อชาติ","สัญชาติ","ศาสนา","เป็นบุตรคนที่","บ้านเลขที่","ซอย","หมู่","ถนน","จังหวัด","อำเภอ","ตำบล","รหัสไปรษณีย์","เบอร์โทรศัพท์","อีเมล์"
        ,"น้ำหนัก","ส่วนสูง","กรุ๊ปเลือด","แพ้อาหาร","แพ้ยา","แพ้อื่นๆ","โรคประจำตัว","โรคร้ายแรง","วันที่สมัคร","วันที่ย้ายเข้า","รหัสนักเรียน","ย้ายไปยังห้อง","สถานะการจ่ายเงิน","ชื่อสถานศึกษาเดิม","จังหวัด"
        ,"อำเภอ","ตำบล","คะแนน GPA","วุฒิการศึกษา","ชื่อ-นามสกุล (ผู้ปกครอง)","รหัสประจำตัวประชาชน (ผู้ปกครอง)","เชื้อชาติ (ผู้ปกครอง)","สัญชาติ (ผู้ปกครอง)","ศาสนา (ผู้ปกครอง)","บ้านเลขที่ (ผู้ปกครอง)"
        ,"ซอย (ผู้ปกครอง)","หมู่ (ผู้ปกครอง)","ถนน (ผู้ปกครอง)","จังหวัด (ผู้ปกครอง)","อำเถอ (ผู้ปกครอง)","ตำบล (ผู้ปกครอง)","รหัสไปรษณีย์ (ผู้ปกครอง)","เบอร์โทรศัพท์ 1 (ผู้ปกครอง)","เบอร์โทรศัพท์ 2 (ผู้ปกครอง)"
        ,"เบอร์โทรศัพท์ 3 (ผู้ปกครอง)","ความสัมพันธ์ (ผู้ปกครอง)","ชื่อ-นามสกุล (บิดา)","รหัสประจำตัวประชาชน (บิดา)","เชื้อชาติ (บิดา)","สัญชาติ (บิดา)","ศาสนา (บิดา)","บ้านเลขที่ (บิดา)","ซอย (บิดา)","หมู่ (บิดา)","ถนน (บิดา)"
        ,"จังหวัด (บิดา)","อำเภอ (บิดา)","ตำบล (บิดา)","รหัสไปรษณีย์ (บิดา)","เบอร์โทรศัพท์ (บิดา)","รายได้ต่อเดือน (บิดา)","ชื่อ-นามสกุล (มารดา)","รหัสประจำตัวประชาชน (มารดา)","เชื้อชาติ (มารดา)","สัญชาติ (มารดา)","ศาสนา (มารดา)"
        ,"บ้านเลขที่ (มารดา)","ซอย (มารดา)","หมู่ (มารดา)","ถนน (มารดา)","จังหวัด (มารดา)","อำเภอ (มารดา)","ตำบล (มารดา)","รหัสไปรษณีย์ (มารดา)","เบอร์โทรศัพท์ (มารดา)","รายได้ต่อเดือน (มารดา)"
        ,"รู้จักสถานศึกษาจาก บิดา-มารดา","รู้จักสถานศึกษาจาก ญาติ","รู้จักสถานศึกษาจาก พี่/น้อง กำลังศึกษา","รู้จักสถานศึกษาจาก เอกสารแนะแนว","รู้จักสถานศึกษาจาก ป้ายโฆษณา","รู้จักสถานศึกษาจาก ป้ายรถเมล์","รู้จักสถานศึกษาจาก งานแนะแนว"
        ,"รู้จักสถานศึกษาจาก มีผู้แนะนำ","รู้จักสถานศึกษาจาก สื่อออนไลน์ (website)","รู้จักสถานศึกษาจาก สื่อออนไลน์ (google)","รู้จักสถานศึกษาจาก สื่อออนไลน์ (facebook)"};

        string style = @"<style> 
.headerCell {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.headerCell130 {width:130px;background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.headerCell40 {width:40px;background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;}
.centertext {border-style: solid;border-width: thin;}
.centertext2 {text-align: center;}
.headerCell22 {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;mso-rotate:90;vertical-align:bottom;}
.centertext22 {border-style: solid;border-width: thin;}
.headerCell33 {background-color: #337AB7;color: White;font-weight: bold;border-style: solid;border-width: thin;mso-rotate:90;vertical-align:bottom;}
.centertext33 {border-style: solid;border-width: thin;}
.rowLeft {text-align: left;border-style: solid;border-width: thin;}
.rowRight {text-align: right;border-style: solid;border-width: thin;}
.rowCenter {text-align: center;border-style: solid;border-width: thin;}
.headerLeft {text-align: left;}
.headerRight {text-align: right;}
.num {mso-number-format:Fixed; text-align: center;border-style: solid;border-width: thin;}
.text{border-style: solid;border-width: thin;mso-number-format: ""\@"";/*force text*/}
.empty {background-color: white;color: White;border-width: 0px;width:0px;}</style>";


        private GridView SetHeader(List<string> HeaderList, GridView grid, string schoolname)
        {
            #region Add Header

            GridViewRow headerRow1 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow3 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow4 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow headerRow5 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableHeaderCell headerTableCell = new TableHeaderCell();

            foreach (var headertxt in HeaderList)
            {
                headerTableCell = new TableHeaderCell();
                headerTableCell.CssClass = "headerCell";
                headerTableCell.Text = headertxt;
                headerTableCell.RowSpan = 3;
                headerRow1.Controls.Add(headerTableCell);
            }





            headerTableCell = new TableHeaderCell();
            headerTableCell.ColumnSpan = 104;
            headerTableCell.CssClass = "centertext2";
            headerTableCell.Text = "ข้อมูลผู้สมัครเข้าเรียน" + schoolname;
            headerRow3.Controls.Add(headerTableCell);



            grid.Controls[0].Controls.AddAt(0, headerRow5);
            grid.Controls[0].Controls.AddAt(0, headerRow4);
            grid.Controls[0].Controls.AddAt(0, headerRow1);
            grid.Controls[0].Controls.AddAt(0, headerRow2);
            grid.Controls[0].Controls.AddAt(0, headerRow3);
            #endregion



            return grid;
        }

        static class Rot13
        {
            /// <summary>
            /// Performs the ROT13 character rotation.
            /// </summary>
            public static string Transform(string value)
            {
                char[] array = value.ToCharArray();
                for (int i = 0; i < array.Length; i++)
                {
                    int number = (int)array[i];

                    if (number >= 'a' && number <= 'z')
                    {
                        if (number > 'm')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    else if (number >= 'A' && number <= 'Z')
                    {
                        if (number > 'M')
                        {
                            number -= 13;
                        }
                        else
                        {
                            number += 13;
                        }
                    }
                    array[i] = (char)number;
                }
                return new string(array);
            }
        }
        private string RandomNumber()
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                Random rand = new Random((int)DateTime.Now.Ticks);
                int numIterations = 0;
                string snumIterations = "";
                do
                {
                    numIterations = rand.Next(100000, 999999);
                    snumIterations = numIterations.ToString();

                } while (_dbMaster.TUsers.Where(w => w.sPassword == snumIterations && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

                return numIterations.ToString();
            }
        }

        void import_Click2(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int preid = int.Parse(editid.Text);
                var pre = _db.TPreRegisters.Where(w => w.preRegisterId == preid).FirstOrDefault();

                pre.paymentStatus = int.Parse(ddlpayment.SelectedValue);
                _db.SaveChanges();


                Response.Redirect("preRegisterList.aspx");
            }
        }

        void import_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int preid = int.Parse(editid.Text); //registerID
                var pre = _db.TPreRegisters.Where(w => w.preRegisterId == preid).FirstOrDefault();
                string link = "";
                int sID = 1;
                if (_dbMaster.TUsers.Count() > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;

                int sID2 = 0;

                JabjaiEntity.DB.TUser _User = new JabjaiEntity.DB.TUser();
                int nID = 2;
                if (_db.TUser.ToList().Count() > 0)
                    nID = _db.TUser.Max(o => o.sID) + 1;

                sID2 = nID;

                int idlv2n = int.Parse(editidlv2.Text); //nTermSubLevel2

                string moveDate = editDay.SelectedValue;
                string moveMonth = editMonth.SelectedValue;
                string moveYear = ddlyear.SelectedValue;
                string combinedate = moveDate + "/" + moveMonth + "/" + moveYear;


                DateTime? moveinday;
                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    moveinday = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    moveinday = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                var thisterm = _db.TTerms.Where(w => w.dStart <= moveinday && w.dEnd > moveinday).FirstOrDefault();


                if (thisterm == null)
                {
                    var nextTermList = _db.TTerms.Where(w => w.dStart > moveinday).ToList();

                    if (nextTermList.Count() == 0)
                    {
                        Response.Write("<script>alert('กรุณาสร้างภาคเรียนการศึกษาภาคถัดไปก่อนทำการย้ายนักเรียน');</script>");
                        return;
                    }
                    else
                    {
                        var nextTermList2 = nextTermList.OrderBy(x => x.dStart).ToList();
                        thisterm = nextTermList2.First();
                    }
                }


                pre.moveInDate = moveinday;
                pre.nTermSubLevel2 = idlv2n;
                pre.saveAsSID = nID;
                _db.SaveChanges();


                string passwordDD = pre.dBirth.Value.Day.ToString("00");
                string passwordMM = pre.dBirth.Value.Month.ToString("00");
                string passwordYY = pre.dBirth.Value.Year.ToString();

                string stdPassword = passwordDD + passwordMM + passwordYY;

                double? gpa = 0;
                if (pre.oldSchoolGPA != null)
                    gpa = pre.oldSchoolGPA;
                _db.TUser.Add(new JabjaiEntity.DB.TUser()
                {
                    moveInDate = moveinday,
                    baseSalary = 0,
                    cDel = null,
                    cSex = pre.cSex,
                    cSMS = "1",
                    cTelSMS = pre.sPhone,
                    cType = "0",
                    DayQuit = null,
                    dBirth = pre.dBirth,
                    dPicUpdate = DateTime.Now,
                    dUpdate = null,
                    nMax = 0,
                    nMoney = 0,
                    Note = null,
                    nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0,
                    nSonNumber = pre.nSonNumber,
                    nStudentNumber = null,
                    nStudentStatus = null,
                    nTermSubLevel2 = idlv2n,
                    oldSchoolAumpher = pre.oldSchoolAumpher,
                    //oldSchoolGPA = (decimal)gpa,
                    oldSchoolGraduated = pre.oldSchoolGraduated,
                    oldSchoolName = pre.oldSchoolName,
                    oldSchoolProvince = pre.oldSchoolProvince,
                    oldSchoolTumbon = pre.oldSchoolTumbon,
                    sAddress = pre.sAddress,
                    sCity = null,
                    sCountry = null,
                    sEmail = pre.sEmail,
                    sFinger = null,
                    sFinger2 = null,
                    sID = nID,
                    sIdentification = pre.sIdentification,
                    sLastname = pre.sLastname,
                    sName = pre.sName,
                    sNickName = pre.sNickName,
                    sPassword = stdPassword,
                    sPhone = pre.sPhone,
                    sPostalcode = null,
                    sStudentAumpher = pre.sStudentAumpher,
                    sStudentHomeNumber = pre.sStudentHomeNumber,
                    sStudentID = editstudentid.Text, /////////
                    sStudentIdCardNumber = pre.sStudentIdCardNumber,
                    sStudentLastEN = pre.sStudentLastEN,
                    sStudentMuu = pre.sStudentMuu,
                    sStudentNameEN = pre.sStudentLastEN,
                    sStudentNation = pre.sStudentNation,
                    sStudentPicture = pre.sStudentPicture,
                    sStudentPost = pre.sStudentPost,
                    sStudentProvince = pre.sStudentProvince,
                    sStudentRace = pre.sStudentRace,
                    sStudentReligion = pre.sStudentReligion,
                    sStudentRoad = pre.sStudentRoad,
                    sStudentSoy = pre.sStudentSoy,
                    sStudentTitle = pre.StudentTitle.ToString(),
                    sStudentTumbon = pre.sStudentTumbon,
                    sSubtopic = null,
                    sToken = null,
                    addressLat = pre.addressLat,
                    addressLng = pre.addressLng,
                    moveOutReason = pre.moveOutReason,
                    sNickNameEN = pre.sNickNameEN,
                    sStudentHomeRegisterCode = pre.sStudentHomeRegisterCode,
                    sStudentHousePhone = pre.sStudentHousePhone,
                    sStudentLastOther = pre.sStudentLastOther,
                    sStudentNameOther = pre.sStudentNameOther,
                    CreatedDate = DateTime.Now

                });
                _db.SaveChanges();

                #region Add Data Master
                string sPassword = RandomNumber();
                int nCompany2 = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;

                _dbMaster.TUsers.Add(new MasterEntity.TUser
                {
                    sID = sID,
                    sName = pre.sName,
                    sLastname = pre.sLastname,
                    sIdentification = pre.sIdentification,
                    cSex = pre.cSex,
                    sPhone = pre.sPhone,
                    sEmail = pre.sEmail,
                    userpassword = stdPassword,
                    PasswordHash = ComFunction.HashSHA1(stdPassword),
                    UseEncryptPassword = false,
                    dCreate = DateTime.Now,
                    cType = "0",
                    nCompany = nCompany2,
                    nSystemID = pre.saveAsSID,
                    dBirth = pre.dBirth,
                    username = editstudentid.Text,/////////////
                });
                _dbMaster.SaveChanges();

                #endregion

                foreach (var termData in _db.TTerms.Where(w => w.nYear == thisterm.nYear && w.cDel == null).ToList())
                {
                    _db.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                    {
                        sID = pre.saveAsSID,
                        nTerm = termData.nTerm,
                        nTermSubLevel2 = idlv2n
                    });
                }

                _db.SaveChanges();

                //int nSTDID = 1;
                //if (_db.TStudentLevels.ToList().Count() > 0)
                //    nSTDID = _db.TStudentLevels.Max(o => o.nStdLvID) + 1;
                //TStudentLevel TStdLV = new TStudentLevel();
                //TStdLV.nStdLvID = nSTDID;
                //TStdLV.nTermSubLevel2 = idlv2n;
                //TStdLV.sID = nID;
                //TStdLV.nTSubLevel = Int32.Parse(editidlv.SelectedValue);
                //_db.TStudentLevels.Add(TStdLV);
                //fcommon.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลนักเรียน " + pre.sName + " " + pre.sLastname, Session["sEntities"].ToString());

                //textbox

                TFamilyProfile famprofile = new TFamilyProfile();
                // tab 2
                int countfamily = _db.TFamilyProfiles.Count() == 0 ? 0 : _db.TFamilyProfiles.Max(max => max.nFamilyID);

                double? faIncome = null;
                if (pre.nFatherIncome == null)
                {
                    if (pre.fatherIncome == "ต่ำกว่า 9,000")
                        faIncome = 9000;
                    else if (pre.fatherIncome == "9,000 - 12,000")
                        faIncome = 12000;
                    else if (pre.fatherIncome == "12,000 - 15,000")
                        faIncome = 15000;
                    else if (pre.fatherIncome == "15,000 - 18,000")
                        faIncome = 18000;
                    else if (pre.fatherIncome == "18,000 - 21,000")
                        faIncome = 21000;
                    else if (pre.fatherIncome == "มากกว่า 21,000")
                        faIncome = 24000;

                }
                else faIncome = pre.nFatherIncome;

                double? moIncome = null;
                if (pre.nMotherIncome == null)
                {
                    if (pre.motherIncome == "ต่ำกว่า 9,000")
                        moIncome = 9000;
                    else if (pre.motherIncome == "9,000 - 12,000")
                        moIncome = 12000;
                    else if (pre.motherIncome == "12,000 - 15,000")
                        moIncome = 15000;
                    else if (pre.motherIncome == "15,000 - 18,000")
                        moIncome = 18000;
                    else if (pre.motherIncome == "18,000 - 21,000")
                        moIncome = 21000;
                    else if (pre.motherIncome == "มากกว่า 21,000")
                        moIncome = 24000;

                }
                else moIncome = pre.nMotherIncome;


                _db.TFamilyProfiles.Add(new TFamilyProfile
                {
                    nFamilyID = countfamily + 1,
                    sID = sID2,
                    sDeleted = "false",
                    sFamilyTitle = pre.nFamilyTitle.ToString(),
                    sFamilyName = pre.sFamilyName,
                    sFamilyLast = pre.sFamilyLast,
                    sFamilyRace = pre.sFamilyRace,
                    sFamilyNation = pre.sFamilyNation,
                    sFamilyReligion = pre.sFamilyReligion,
                    sFamilyIdCardNumber = pre.sFamilyIdCardNumber,
                    sFamilyRelate = pre.sFamilyRelate,
                    sFamilyHomeNumber = pre.sFamilyHomeNumber,
                    sFamilySoy = pre.sFamilySoy,
                    sFamilyTumbon = pre.sFamilyTumbon,
                    sFamilyProvince = pre.sFamilyProvince,
                    sPhoneOne = pre.sPhoneOne,
                    sPhoneTwo = pre.sPhoneTwo,
                    sPhoneThree = pre.sPhoneThree,
                    sPhoneMail = pre.sPhoneMail,
                    sFamilyMuu = pre.sFamilyMuu,
                    sFamilyRoad = pre.sFamilyRoad,
                    sFamilyAumpher = pre.sFamilyAumpher,
                    sFamilyPost = pre.sFamilyPost,
                    sFatherTitle = pre.FatherTitle.ToString(),
                    sFatherFirstName = pre.sFatherFirstName,
                    sFatherLastName = pre.sFatherLastName,
                    sFatherIdCardNumber = pre.sFatherIdCardNumber,
                    sFatherNation = pre.sFatherNation,
                    sFatherRace = pre.sFatherRace,
                    sFatherReligion = pre.sFatherReligion,
                    sMotherTitle = pre.MotherTitle.ToString(),
                    sMotherFirstName = pre.sMotherFirstName,
                    sMotherIdCardNumber = pre.sMotherIdCardNumber,
                    sMotherLastName = pre.sMotherLastName,
                    sMotherNation = pre.sMotherNation,
                    sMotherRace = pre.sMotherRace,
                    sMotherReligion = pre.sMotherReligion,
                    sFatherAumpher = pre.sFatherAumpher,
                    sFatherHomeNumber = pre.sFatherHomeNumber,
                    sFatherMuu = pre.sFatherMuu,
                    sFatherPhone = pre.sFatherPhone,
                    sFatherPost = pre.sFatherPost,
                    sFatherProvince = pre.sFatherProvince,
                    sFatherRoad = pre.sFatherRoad,
                    sFatherSoy = pre.sFatherSoy,
                    sFatherTumbon = pre.sFatherTumbon,
                    sMotherAumpher = pre.sMotherAumpher,
                    sMotherHomeNumber = pre.sMotherHomeNumber,
                    sMotherMuu = pre.sMotherMuu,
                    sMotherPhone = pre.sMotherPhone,
                    sMotherPost = pre.sMotherPost,
                    sMotherProvince = pre.sMotherProvince,
                    sMotherRoad = pre.sMotherRoad,
                    sMotherSoy = pre.sMotherSoy,
                    sMotherTumbon = pre.sMotherTumbon,
                    bornFrom = pre.bornFrom,
                    bornFromAumpher = pre.bornFromAumpher,
                    bornFromProvince = pre.bornFromProvince,
                    bornFromTumbon = pre.bornFromTumbon,
                    dFamilyBirthDay = pre.dFamilyBirthDay,
                    dFatherBirthDay = pre.dFatherBirthDay,
                    dMotherBirthDay = pre.dMotherBirthDay,
                    friendSID = pre.friendSID,
                    HomeType = pre.HomeType,
                    houseRegistrationAumpher = pre.houseRegistrationAumpher,
                    houseRegistrationMuu = pre.houseRegistrationMuu,
                    houseRegistrationNumber = pre.houseRegistrationNumber,
                    houseRegistrationPhone = pre.houseRegistrationPhone,
                    houseRegistrationPost = pre.houseRegistrationPost,
                    houseRegistrationProvince = pre.houseRegistrationProvince,
                    houseRegistrationRoad = pre.houseRegistrationRoad,
                    houseRegistrationSoy = pre.houseRegistrationSoy,
                    houseRegistrationTumbon = pre.houseRegistrationTumbon,
                    nFamilyIncome = pre.nFamilyIncome,
                    nFamilyRequestStudyMoney = pre.nFamilyRequestStudyMoney,
                    nFatherIncome = faIncome,
                    nMotherIncome = moIncome,
                    nRelativeStudyHere = pre.nRelativeStudyHere,
                    nSonTotal = pre.nSonTotal,
                    sFamilyGraduated = pre.sFamilyGraduated,
                    sFamilyJob = pre.sFamilyJob,
                    sFamilyLastEN = pre.sFamilyLastEN,
                    sFamilyNameEN = pre.sFamilyNameEN,
                    sFamilyWorkPlace = pre.sFamilyWorkPlace,
                    sFatherGraduated = pre.sFatherGraduated,
                    sFatherJob = pre.sFatherJob,
                    sFatherLastEN = pre.sFatherLastEN,
                    sFatherNameEN = pre.sFatherNameEN,
                    sFatherPhone2 = pre.sFatherPhone2,
                    sFatherPhone3 = pre.sFatherPhone3,
                    sFatherWorkPlace = pre.sFatherWorkPlace,
                    sMotherGraduated = pre.sMotherGraduated,
                    sMotherJob = pre.sMotherJob,
                    sMotherLastEN = pre.sMotherLastEN,
                    sMotherNameEN = pre.sMotherNameEN,
                    sMotherPhone2 = pre.sMotherPhone2,
                    sMotherPhone3 = pre.sMotherPhone3,
                    sMotherWorkPlace = pre.sMotherWorkPlace,
                    stayWithEmail = pre.stayWithEmail,
                    stayWithEmergencyCall = pre.stayWithEmergencyCall,
                    stayWithLast = pre.stayWithLast,
                    stayWithName = pre.stayWithName,
                    stayWithTitle = pre.stayWithTitle,
                    familyStatus = pre.familyStatus,
                    friendLastName = pre.friendLastName,
                    friendName = pre.friendName,
                    friendPhone = pre.friendPhone,
                    friendSubLevel = pre.friendSubLevel
                });

                THealtProfile health = new THealtProfile();
                //tab 3
                int counthealth = _db.THealtProfiles.Count() == 0 ? 0 : _db.THealtProfiles.Max(max => max.nHealthID);
                health.nHealthID = counthealth + 1;
                health.sID = sID2;
                health.sDeleted = "false";
                health.nWeight = pre.nWeight;
                health.nHeight = pre.nHeight;
                health.sBlood = pre.sBlood;
                health.sSickFood = pre.sSickFood;
                health.sSickDrug = pre.sSickDrug;
                health.sSickOther = pre.sSickOther;
                health.sSickNormal = pre.sSickNormal;
                health.sSickDanger = pre.sSickDanger;

                _db.THealtProfiles.Add(health);
                _db.SaveChanges();

                Response.Redirect("preRegisterList.aspx");
            }
        }

        void importAll_Click(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string editmulti = TextBox3.Text;
                editmulti = editmulti.Remove(editmulti.Length - 1);

                string[] words = editmulti.Split('~');
                foreach (var preredId in words)
                {
                    int preid = int.Parse(preredId);
                    var pre = _db.TPreRegisters.Where(w => w.preRegisterId == preid).FirstOrDefault();
                    string link = "";
                    int sID = 1;
                    if (_dbMaster.TUsers.Count() > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;

                    int sID2 = 0;

                    int nID = 2;
                    if (_db.TUser.ToList().Count() > 0)
                        nID = _db.TUser.Max(o => o.sID) + 1;

                    sID2 = nID;

                    int idlv2n = int.Parse(TextBox6.Text);

                    string moveDate = DropDownList6.SelectedValue;
                    string moveMonth = DropDownList7.SelectedValue;
                    string moveYear = DropDownList8.SelectedValue;
                    string combinedate = moveDate + "/" + moveMonth + "/" + moveYear;


                    DateTime? moveinday;
                    if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        moveinday = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        moveinday = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                    var thisterm = _db.TTerms.Where(w => w.dStart <= moveinday && w.dEnd > moveinday).FirstOrDefault();


                    if (thisterm == null)
                    {
                        var nextTermList = _db.TTerms.Where(w => w.dStart > moveinday).ToList();

                        if (nextTermList.Count() == 0)
                        {
                            Response.Write("<script>alert('กรุณาสร้างภาคเรียนการศึกษาภาคถัดไปก่อนทำการย้ายนักเรียน');</script>");
                            return;
                        }
                        else
                        {
                            var nextTermList2 = nextTermList.OrderBy(x => x.dStart).ToList();
                            thisterm = nextTermList2.First();
                        }
                    }


                    pre.moveInDate = moveinday;
                    pre.nTermSubLevel2 = idlv2n;
                    pre.saveAsSID = nID;
                    _db.SaveChanges();


                    var max_id = _db.TStudentClassroomHistories.Select(s => s.nHistoryId).DefaultIfEmpty(0).Max();
                    max_id = max_id + 1;

                    string passwordDD = pre.dBirth.Value.Day.ToString("00");
                    string passwordMM = pre.dBirth.Value.Month.ToString("00");
                    string passwordYY = pre.dBirth.Value.Year.ToString();

                    string stdPassword = passwordDD + passwordMM + passwordYY;

                    double? gpa = 0;
                    if (pre.oldSchoolGPA != null)
                        gpa = pre.oldSchoolGPA;
                    _db.TUser.Add(new JabjaiEntity.DB.TUser()
                    {
                        moveInDate = moveinday,
                        baseSalary = 0,
                        cDel = null,
                        cSex = pre.cSex,
                        cSMS = "1",
                        cTelSMS = pre.sPhone,
                        cType = "0",
                        DayQuit = null,
                        dBirth = pre.dBirth,
                        dPicUpdate = DateTime.Now,
                        dUpdate = null,
                        nMax = 0,
                        nMoney = 0,
                        Note = null,
                        nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0,
                        nSonNumber = pre.nSonNumber,
                        nStudentNumber = null,
                        nStudentStatus = null,
                        nTermSubLevel2 = idlv2n,
                        oldSchoolAumpher = pre.oldSchoolAumpher,
                        //oldSchoolGPA = (decimal)gpa,
                        oldSchoolGraduated = pre.oldSchoolGraduated,
                        oldSchoolName = pre.oldSchoolName,
                        oldSchoolProvince = pre.oldSchoolProvince,
                        oldSchoolTumbon = pre.oldSchoolTumbon,
                        sAddress = pre.sAddress,
                        sCity = null,
                        sCountry = null,
                        sEmail = pre.sEmail,
                        sFinger = null,
                        sFinger2 = null,
                        sID = nID,
                        sIdentification = pre.sIdentification,
                        sLastname = pre.sLastname,
                        sName = pre.sName,
                        sNickName = pre.sNickName,
                        sPassword = stdPassword,
                        sPhone = pre.sPhone,
                        sPostalcode = null,
                        sStudentAumpher = pre.sStudentAumpher,
                        sStudentHomeNumber = pre.sStudentHomeNumber,
                        sStudentID = pre.sStudentID,
                        sStudentIdCardNumber = pre.sStudentIdCardNumber,
                        sStudentLastEN = pre.sStudentLastEN,
                        sStudentMuu = pre.sStudentMuu,
                        sStudentNameEN = pre.sStudentLastEN,
                        sStudentNation = pre.sStudentNation,
                        sStudentPicture = pre.sStudentPicture,
                        sStudentPost = pre.sStudentPost,
                        sStudentProvince = pre.sStudentProvince,
                        sStudentRace = pre.sStudentRace,
                        sStudentReligion = pre.sStudentReligion,
                        sStudentRoad = pre.sStudentRoad,
                        sStudentSoy = pre.sStudentSoy,
                        sStudentTitle = pre.StudentTitle.ToString(),
                        sStudentTumbon = pre.sStudentTumbon,
                        sSubtopic = null,
                        sToken = null,
                        addressLat = pre.addressLat,
                        addressLng = pre.addressLng,
                        moveOutReason = pre.moveOutReason,
                        sNickNameEN = pre.sNickNameEN,
                        sStudentHomeRegisterCode = pre.sStudentHomeRegisterCode,
                        sStudentHousePhone = pre.sStudentHousePhone,
                        sStudentLastOther = pre.sStudentLastOther,
                        sStudentNameOther = pre.sStudentNameOther,
                        CreatedDate = DateTime.Now
                    });
                    _db.SaveChanges();



                    #region Add Data Master
                    string sPassword = RandomNumber();
                    int nCompany2 = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;

                    _dbMaster.TUsers.Add(new MasterEntity.TUser
                    {
                        sID = sID,
                        sName = pre.sName,
                        sLastname = pre.sLastname,
                        sIdentification = pre.sIdentification,
                        cSex = pre.cSex,
                        sPhone = pre.sPhone,
                        sEmail = pre.sEmail,
                        userpassword = stdPassword,
                        PasswordHash = ComFunction.HashSHA1(stdPassword),
                        UseEncryptPassword = false,
                        dCreate = DateTime.Now,
                        cType = "0",
                        nCompany = nCompany2,
                        nSystemID = pre.saveAsSID,
                        dBirth = pre.dBirth,
                        username = pre.sStudentID,
                    });
                    _dbMaster.SaveChanges();

                    #endregion

                    foreach (var termData in _db.TTerms.Where(w => w.nYear == thisterm.nYear && w.cDel == null).ToList())
                    {
                        _db.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                        {
                            sID = pre.saveAsSID,
                            nTerm = termData.nTerm,
                            nTermSubLevel2 = idlv2n
                        });
                    }

                    //int nSTDID = 1;
                    //if (_db.TStudentLevels.ToList().Count() > 0)
                    //    nSTDID = _db.TStudentLevels.Max(o => o.nStdLvID) + 1;
                    //TStudentLevel TStdLV = new TStudentLevel();
                    //TStdLV.nStdLvID = nSTDID;
                    //TStdLV.nTermSubLevel2 = idlv2n;
                    //TStdLV.sID = nID;
                    //TStdLV.nTSubLevel = Int32.Parse(editidlv.SelectedValue);
                    //_db.TStudentLevels.Add(TStdLV);
                    //fcommon.InsertLog(Session["sEmpID"] + "", "เพิ่มข้อมูลนักเรียน " + pre.sName + " " + pre.sLastname, Session["sEntities"].ToString());

                    //textbox

                    TFamilyProfile famprofile = new TFamilyProfile();
                    // tab 2
                    int countfamily = _db.TFamilyProfiles.Count() == 0 ? 0 : _db.TFamilyProfiles.Max(max => max.nFamilyID);

                    double? faIncome = null;
                    if (pre.nFatherIncome == null)
                    {
                        if (pre.fatherIncome == "ต่ำกว่า 9,000")
                            faIncome = 9000;
                        else if (pre.fatherIncome == "9,000 - 12,000")
                            faIncome = 12000;
                        else if (pre.fatherIncome == "12,000 - 15,000")
                            faIncome = 15000;
                        else if (pre.fatherIncome == "15,000 - 18,000")
                            faIncome = 18000;
                        else if (pre.fatherIncome == "18,000 - 21,000")
                            faIncome = 21000;
                        else if (pre.fatherIncome == "มากกว่า 21,000")
                            faIncome = 24000;

                    }
                    else faIncome = pre.nFatherIncome;

                    double? moIncome = null;
                    if (pre.nMotherIncome == null)
                    {
                        if (pre.motherIncome == "ต่ำกว่า 9,000")
                            moIncome = 9000;
                        else if (pre.motherIncome == "9,000 - 12,000")
                            moIncome = 12000;
                        else if (pre.motherIncome == "12,000 - 15,000")
                            moIncome = 15000;
                        else if (pre.motherIncome == "15,000 - 18,000")
                            moIncome = 18000;
                        else if (pre.motherIncome == "18,000 - 21,000")
                            moIncome = 21000;
                        else if (pre.motherIncome == "มากกว่า 21,000")
                            moIncome = 24000;

                    }
                    else moIncome = pre.nMotherIncome;


                    _db.TFamilyProfiles.Add(new TFamilyProfile
                    {
                        nFamilyID = countfamily + 1,
                        sID = sID2,
                        sDeleted = "false",
                        sFamilyTitle = pre.nFamilyTitle.ToString(),
                        sFamilyName = pre.sFamilyName,
                        sFamilyLast = pre.sFamilyLast,
                        sFamilyRace = pre.sFamilyRace,
                        sFamilyNation = pre.sFamilyNation,
                        sFamilyReligion = pre.sFamilyReligion,
                        sFamilyIdCardNumber = pre.sFamilyIdCardNumber,
                        sFamilyRelate = pre.sFamilyRelate,
                        sFamilyHomeNumber = pre.sFamilyHomeNumber,
                        sFamilySoy = pre.sFamilySoy,
                        sFamilyTumbon = pre.sFamilyTumbon,
                        sFamilyProvince = pre.sFamilyProvince,
                        sPhoneOne = pre.sPhoneOne,
                        sPhoneTwo = pre.sPhoneTwo,
                        sPhoneThree = pre.sPhoneThree,
                        sPhoneMail = pre.sPhoneMail,
                        sFamilyMuu = pre.sFamilyMuu,
                        sFamilyRoad = pre.sFamilyRoad,
                        sFamilyAumpher = pre.sFamilyAumpher,
                        sFamilyPost = pre.sFamilyPost,
                        sFatherTitle = pre.FatherTitle.ToString(),
                        sFatherFirstName = pre.sFatherFirstName,
                        sFatherLastName = pre.sFatherLastName,
                        sFatherIdCardNumber = pre.sFatherIdCardNumber,
                        sFatherNation = pre.sFatherNation,
                        sFatherRace = pre.sFatherRace,
                        sFatherReligion = pre.sFatherReligion,
                        sMotherTitle = pre.MotherTitle.ToString(),
                        sMotherFirstName = pre.sMotherFirstName,
                        sMotherIdCardNumber = pre.sMotherIdCardNumber,
                        sMotherLastName = pre.sMotherLastName,
                        sMotherNation = pre.sMotherNation,
                        sMotherRace = pre.sMotherRace,
                        sMotherReligion = pre.sMotherReligion,
                        sFatherAumpher = pre.sFatherAumpher,
                        sFatherHomeNumber = pre.sFatherHomeNumber,
                        sFatherMuu = pre.sFatherMuu,
                        sFatherPhone = pre.sFatherPhone,
                        sFatherPost = pre.sFatherPost,
                        sFatherProvince = pre.sFatherProvince,
                        sFatherRoad = pre.sFatherRoad,
                        sFatherSoy = pre.sFatherSoy,
                        sFatherTumbon = pre.sFatherTumbon,
                        sMotherAumpher = pre.sMotherAumpher,
                        sMotherHomeNumber = pre.sMotherHomeNumber,
                        sMotherMuu = pre.sMotherMuu,
                        sMotherPhone = pre.sMotherPhone,
                        sMotherPost = pre.sMotherPost,
                        sMotherProvince = pre.sMotherProvince,
                        sMotherRoad = pre.sMotherRoad,
                        sMotherSoy = pre.sMotherSoy,
                        sMotherTumbon = pre.sMotherTumbon,
                        bornFrom = pre.bornFrom,
                        bornFromAumpher = pre.bornFromAumpher,
                        bornFromProvince = pre.bornFromProvince,
                        bornFromTumbon = pre.bornFromTumbon,
                        dFamilyBirthDay = pre.dFamilyBirthDay,
                        dFatherBirthDay = pre.dFatherBirthDay,
                        dMotherBirthDay = pre.dMotherBirthDay,
                        friendSID = pre.friendSID,
                        HomeType = pre.HomeType,
                        houseRegistrationAumpher = pre.houseRegistrationAumpher,
                        houseRegistrationMuu = pre.houseRegistrationMuu,
                        houseRegistrationNumber = pre.houseRegistrationNumber,
                        houseRegistrationPhone = pre.houseRegistrationPhone,
                        houseRegistrationPost = pre.houseRegistrationPost,
                        houseRegistrationProvince = pre.houseRegistrationProvince,
                        houseRegistrationRoad = pre.houseRegistrationRoad,
                        houseRegistrationSoy = pre.houseRegistrationSoy,
                        houseRegistrationTumbon = pre.houseRegistrationTumbon,
                        nFamilyIncome = pre.nFamilyIncome,
                        nFamilyRequestStudyMoney = pre.nFamilyRequestStudyMoney,
                        nFatherIncome = faIncome,
                        nMotherIncome = moIncome,
                        nRelativeStudyHere = pre.nRelativeStudyHere,
                        nSonTotal = pre.nSonTotal,
                        sFamilyGraduated = pre.sFamilyGraduated,
                        sFamilyJob = pre.sFamilyJob,
                        sFamilyLastEN = pre.sFamilyLastEN,
                        sFamilyNameEN = pre.sFamilyNameEN,
                        sFamilyWorkPlace = pre.sFamilyWorkPlace,
                        sFatherGraduated = pre.sFatherGraduated,
                        sFatherJob = pre.sFatherJob,
                        sFatherLastEN = pre.sFatherLastEN,
                        sFatherNameEN = pre.sFatherNameEN,
                        sFatherPhone2 = pre.sFatherPhone2,
                        sFatherPhone3 = pre.sFatherPhone3,
                        sFatherWorkPlace = pre.sFatherWorkPlace,
                        sMotherGraduated = pre.sMotherGraduated,
                        sMotherJob = pre.sMotherJob,
                        sMotherLastEN = pre.sMotherLastEN,
                        sMotherNameEN = pre.sMotherNameEN,
                        sMotherPhone2 = pre.sMotherPhone2,
                        sMotherPhone3 = pre.sMotherPhone3,
                        sMotherWorkPlace = pre.sMotherWorkPlace,
                        stayWithEmail = pre.stayWithEmail,
                        stayWithEmergencyCall = pre.stayWithEmergencyCall,
                        stayWithLast = pre.stayWithLast,
                        stayWithName = pre.stayWithName,
                        stayWithTitle = pre.stayWithTitle,
                        familyStatus = pre.familyStatus,
                        friendLastName = pre.friendLastName,
                        friendName = pre.friendName,
                        friendPhone = pre.friendPhone,
                        friendSubLevel = pre.friendSubLevel
                    });

                    THealtProfile health = new THealtProfile();
                    //tab 3
                    int counthealth = _db.THealtProfiles.Count() == 0 ? 0 : _db.THealtProfiles.Max(max => max.nHealthID);
                    health.nHealthID = counthealth + 1;
                    health.sID = sID2;
                    health.sDeleted = "false";
                    health.nWeight = pre.nWeight;
                    health.nHeight = pre.nHeight;
                    health.sBlood = pre.sBlood;
                    health.sSickFood = pre.sSickFood;
                    health.sSickDrug = pre.sSickDrug;
                    health.sSickOther = pre.sSickOther;
                    health.sSickNormal = pre.sSickNormal;
                    health.sSickDanger = pre.sSickDanger;

                    _db.THealtProfiles.Add(health);
                    _db.SaveChanges();
                }



                Response.Redirect("preRegisterList.aspx");
            }
        }

        void delete(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";

                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                int deleten = int.Parse(deleteid.Text);
                var a = _db.TPreRegisters.Where(w => w.preRegisterId == deleten).FirstOrDefault();
                a.cDel = 1;

                _db.SaveChanges();


                Response.Redirect("preRegisterList.aspx");
            }
        }

        protected List<preRegister> returnlist()
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"] + "";
                string year = Request.QueryString["year"];
                string mode = Request.QueryString["mode"];
                string name = Request.QueryString["name"];
                string idlv = Request.QueryString["idlv"];
                string Coursetype = Request.QueryString["type"];
                string time = Request.QueryString["time"];
                string branch = Request.QueryString["branch"];
                string planID = Request.QueryString["planID"];

                preRegister Plan = new preRegister();
                List<preRegister> PlanList = new List<preRegister>();



                int number = 1;

                var sublv_db = _db.TSubLevels.ToList();

                List<sortList> sortList = new List<sortList>();
                sortList sort = new sortList();

                if (year != "" && year != null)
                {
                    int? useryear = Int32.Parse(year);
                    useryear = useryear - 543;
                    foreach (var data in _db.TPreRegisters.Where(w => w.registerYear == useryear && w.cDel == null))
                    {
                        Plan = new preRegister();
                        Plan.number = number;
                        number = number + 1;
                        DateTime dateadd = data.addDate.Value.AddYears(543);
                        Plan.registerDate = dateadd.Day + " " + monthtxt(dateadd.Month) + " " + dateadd.Year;
                        Plan.idSchool = Rot13.Transform(sEntities);
                        Plan.Name = data.sName + " " + data.sLastname;
                        Plan.preRegisterId = data.preRegisterId.ToString();
                        Plan.year = data.registerYear.ToString();
                        Plan.Code = data.sStudentID;
                        if (data.nTermSubLevel2 != null)
                        {
                            var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data.nTermSubLevel2).FirstOrDefault();
                            var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                            string txt = sub.SubLevel + " / " + room.nTSubLevel2;
                            Plan.Status = "ย้ายเข้า " + txt;
                            Plan.mode = "4";
                        }
                        else if (data.paymentStatus == 0)
                        {
                            Plan.Status = "ยังไม่ชำระค่าสมัคร";
                            Plan.mode = "1";
                        }
                        else if (data.paymentStatus == 1)
                        {
                            Plan.Status = "ชำระค่าสมัครไม่ครบ";
                            Plan.mode = "2";
                        }
                        else if (data.paymentStatus == 2)
                        {
                            Plan.Status = "ชำระค่าสมัครแล้ว";
                            Plan.mode = "3";
                        }

                        if (data.optionBranch != null)
                        {
                            var branch2 = _db.TBranchSpecs.Where(w => w.BranchSpecId == data.optionBranch).FirstOrDefault();
                            Plan.branch = branch2.BranchSpecName;
                        }
                        else Plan.branch = "";

                        if (data.optionTime != null)
                            Plan.coursetime = data.optionTime.ToString();
                        else Plan.coursetime = "";
                        Plan.courseType = data.optionCourse.ToString();
                        Plan.idlv = data.optionLevel.ToString();
                        var idlvdb = sublv_db.Where(w => w.nTSubLevel == data.optionLevel).FirstOrDefault();
                        Plan.idlvName = idlvdb.SubLevel;
                        Plan.planID = Convert.ToString(data.RegisterPlanSetupID);

                        PlanList.Add(Plan);
                    }

                    if (mode == "1")
                        PlanList = PlanList.Where(w => w.mode == "1").ToList();
                    else if (mode == "2")
                        PlanList = PlanList.Where(w => w.mode == "2").ToList();
                    else if (mode == "3")
                        PlanList = PlanList.Where(w => w.mode == "3").ToList();
                    else if (mode == "4")
                        PlanList = PlanList.Where(w => w.mode == "4").ToList();

                    if (idlv != "0")
                        PlanList = PlanList.Where(w => w.idlv == idlv).ToList();

                    if (time != "0")
                        PlanList = PlanList.Where(w => w.coursetime == time).ToList();

                    if (Coursetype != "0")
                        PlanList = PlanList.Where(w => w.courseType == Coursetype).ToList();

                    if (branch != "")
                        PlanList = PlanList.Where(w => w.branch == branch).ToList();

                    if (planID != "")
                        PlanList = PlanList.Where(w => w.planID == planID).ToList();
                }

                else
                {
                    foreach (var data in _db.TPreRegisters.Where(w => w.cDel == null))
                    {
                        Plan = new preRegister();
                        Plan.number = number;
                        number = number + 1;
                        Plan.idSchool = Rot13.Transform(sEntities);
                        DateTime dateadd = data.addDate.Value.AddYears(543);
                        Plan.registerDate = dateadd.Day + " " + monthtxt(dateadd.Month) + " " + dateadd.Year;
                        Plan.Name = data.sName + " " + data.sLastname;
                        Plan.preRegisterId = data.preRegisterId.ToString();
                        Plan.year = data.registerYear.ToString();
                        Plan.Code = data.sStudentID;
                        if (data.nTermSubLevel2 != null)
                        {
                            var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data.nTermSubLevel2).FirstOrDefault();
                            var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                            string txt = sub.SubLevel + " / " + room.nTSubLevel2;
                            Plan.Status = "ย้ายเข้า " + txt;
                            Plan.mode = "4";
                        }
                        else if (data.paymentStatus == 0)
                            Plan.Status = "ยังไม่ชำระค่าสมัคร";
                        else if (data.paymentStatus == 1)
                            Plan.Status = "ชำระค่าสมัครไม่ครบ";
                        else if (data.paymentStatus == 2)
                            Plan.Status = "ชำระค่าสมัครแล้ว";
                        if (data.optionBranch != null)
                        {
                            var branch2 = _db.TBranchSpecs.Where(w => w.BranchSpecId == data.optionBranch).FirstOrDefault();
                            Plan.branch = branch2.BranchSpecName;
                        }
                        else Plan.branch = "";

                        if (data.optionTime != null)
                            Plan.coursetime = data.optionTime.ToString();
                        else Plan.coursetime = "";
                        Plan.courseType = data.optionCourse.ToString();
                        Plan.idlv = data.optionLevel.ToString();
                        var idlvdb = sublv_db.Where(w => w.nTSubLevel == data.optionLevel).FirstOrDefault();
                        Plan.idlvName = idlvdb.SubLevel;
                        PlanList.Add(Plan);
                    }
                }

                if (name != null && name != "")
                {
                    preRegister Plan2 = new preRegister();
                    List<preRegister> PlanList2 = new List<preRegister>();

                    number = 1;
                    foreach (var data in _db.TPreRegisters.Where(w => w.cDel == null && w.sName + " " + w.sLastname == name))
                    {
                        Plan2 = new preRegister();
                        Plan2.number = number;
                        number = number + 1;
                        Plan2.idSchool = Rot13.Transform(sEntities);
                        DateTime dateadd = data.addDate.Value.AddYears(543);
                        Plan2.registerDate = dateadd.Day + " " + monthtxt(dateadd.Month) + " " + dateadd.Year;
                        Plan2.Name = data.sName + " " + data.sLastname;
                        Plan2.preRegisterId = data.preRegisterId.ToString();
                        Plan2.year = data.registerYear.ToString();
                        Plan2.Code = data.sStudentID;
                        if (data.nTermSubLevel2 != null)
                        {
                            var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == data.nTermSubLevel2).FirstOrDefault();
                            var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel).FirstOrDefault();

                            string txt = sub.SubLevel + " / " + room.nTSubLevel2;
                            Plan2.Status = "ย้ายเข้า " + txt;
                            Plan2.mode = "4";
                        }
                        else if (data.paymentStatus == 0)
                            Plan2.Status = "ยังไม่ชำระค่าสมัคร";
                        else if (data.paymentStatus == 1)
                            Plan2.Status = "ชำระค่าสมัครไม่ครบ";
                        else if (data.paymentStatus == 2)
                            Plan2.Status = "ชำระค่าสมัครแล้ว";
                        if (data.optionBranch != null)
                        {
                            var branch2 = _db.TBranchSpecs.Where(w => w.BranchSpecId == data.optionBranch).FirstOrDefault();
                            Plan.branch = branch2.BranchSpecName;
                        }
                        else Plan.branch = "";

                        if (data.optionTime != null)
                            Plan.coursetime = data.optionTime.ToString();
                        else Plan.coursetime = "";
                        Plan.courseType = data.optionCourse.ToString();
                        Plan.idlv = data.optionLevel.ToString();
                        var idlvdb = sublv_db.Where(w => w.nTSubLevel == data.optionLevel).FirstOrDefault();
                        Plan.idlvName = idlvdb.SubLevel;
                        PlanList2.Add(Plan2);
                    }
                    PlanList = PlanList2;
                }

                int numbersort = 1;
                foreach (var sortnum in PlanList)
                {
                    sortnum.number = numbersort;
                    numbersort++;
                }
                return PlanList;

            }
        }

        private void OpenData()
        {

            dgd.DataSource = returnlist();
            dgd.PageSize = 100;
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        string monthtxt(int month)
        {
            string txt = "";
            if (month == 1)
                txt = "ม.ค.";
            else if (month == 2)
                txt = "ก.พ.";
            else if (month == 3)
                txt = "มี.ค.";
            else if (month == 4)
                txt = "ม.ย.";
            else if (month == 5)
                txt = "พ.ค.";
            else if (month == 6)
                txt = "มิ.ย.";
            else if (month == 7)
                txt = "ก.ค.";
            else if (month == 8)
                txt = "ส.ค.";
            else if (month == 9)
                txt = "ก.ย.";
            else if (month == 10)
                txt = "ต.ค.";
            else if (month == 11)
                txt = "พ.ย.";
            else if (month == 12)
                txt = "ธ.ค.";
            return txt;
        }

        protected class preRegister
        {
            public string mode { get; set; }
            public int number { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string Status { get; set; }
            public string year { get; set; }
            public string preRegisterId { get; set; }
            public int? sortnumber { get; set; }
            public string idSchool { get; set; }
            public string registerDate { get; set; }
            public string branch { get; set; }
            public string coursetime { get; set; }
            public string idlv { get; set; }
            public string idlvName { get; set; }
            public string courseType { get; set; }
            public string planID { get; set; }
        }
        class branchSpec
        {

            public int branchSpecId { get; set; }
            public string name { get; set; }
        }
        protected class exportExcel
        {
            public int number { get; set; }
            public int registerYear { get; set; }
            public string optionBranch { get; set; }

            public string optionTime { get; set; }
            public string optionCourse { get; set; }
            public string planName { get; set; }
            public string optionLevel { get; set; }
            public string sStudentID { get; set; }
            public string examCode { get; set; }
            public string cSex { get; set; }
            public string sName { get; set; }
            public string sNameEN { get; set; }
            public string sNickname { get; set; }
            public string sStudentIdCardNumber { get; set; }

            public string dBirth { get; set; }
            public string sStudentRace { get; set; }
            public string sStudentNation { get; set; }
            public string sStudentReligion { get; set; }
            public string nSonNumber { get; set; }
            public string sStudentHomeNumber { get; set; }
            public string sStudentSoy { get; set; }
            public string sStudentMuu { get; set; }
            public string sStudentRoad { get; set; }
            public string sStudentProvince { get; set; }
            public string sStudentAumpher { get; set; }
            public string sStudentTumbon { get; set; }
            public string sStudentPost { get; set; }
            public string sPhone { get; set; }
            public string sEmail { get; set; }
            public string nWeight { get; set; }
            public string nHeight { get; set; }
            public string sBlood { get; set; }
            public string sSickFood { get; set; }
            public string sSickDrug { get; set; }
            public string sSickOther { get; set; }
            public string sSickNormal { get; set; }
            public string sSickDanger { get; set; }
            public string registerCode { get; set; }
            public string addDate { get; set; }
            public string moveInDate { get; set; }
            public string nTermSubLevel2 { get; set; }
            public string paymentStatus { get; set; }
            public string oldSchoolName { get; set; }
            public string oldSchoolProvince { get; set; }
            public string oldSchoolAumpher { get; set; }
            public string oldSchoolTumbon { get; set; }
            public string oldSchoolGPA { get; set; }
            public string oldSchoolGraduated { get; set; }
            public string sFamilyName { get; set; }
            public string sFamilyIdCardNumber { get; set; }
            public string sFamilyRace { get; set; }
            public string sFamilyNation { get; set; }
            public string sFamilyReligion { get; set; }
            public string sFamilyHomeNumber { get; set; }
            public string sFamilySoy { get; set; }
            public string sFamilyMuu { get; set; }
            public string sFamilyRoad { get; set; }
            public string sFamilyProvince { get; set; }
            public string sFamilyAumpher { get; set; }
            public string sFamilyTumbon { get; set; }
            public string sFamilyPost { get; set; }
            public string sPhoneOne { get; set; }
            public string sPhoneTwo { get; set; }
            public string sPhoneThree { get; set; }
            public string sFamilyRelate { get; set; }
            public string sFatherName { get; set; }
            public string sFatherIdCardNumber { get; set; }
            public string sFatherRace { get; set; }
            public string sFatherNation { get; set; }
            public string sFatherReligion { get; set; }
            public string sFatherHomeNumber { get; set; }
            public string sFatherSoy { get; set; }
            public string sFatherMuu { get; set; }
            public string sFatherRoad { get; set; }
            public string sFatherProvince { get; set; }
            public string sFatherAumpher { get; set; }
            public string sFatherTumbon { get; set; }
            public string sFatherPost { get; set; }
            public string sFatherPhone { get; set; }
            public string fatherIncome { get; set; }
            public string Mothername { get; set; }
            public string sMotherIdCardNumber { get; set; }
            public string sMotherRace { get; set; }
            public string sMotherNation { get; set; }
            public string sMotherReligion { get; set; }
            public string sMotherHomeNumber { get; set; }
            public string sMotherSoy { get; set; }
            public string sMotherMuu { get; set; }
            public string sMotherRoad { get; set; }
            public string sMotherProvince { get; set; }
            public string sMotherAumpher { get; set; }
            public string sMotherTumbon { get; set; }
            public string sMotherPost { get; set; }
            public string sMotherPhone { get; set; }
            public string motherIncome { get; set; }
            public string knowFrom1 { get; set; }
            public string knowFrom2 { get; set; }
            public string knowFrom3 { get; set; }
            public string knowFrom4 { get; set; }
            public string knowFrom5 { get; set; }
            public string knowFrom6 { get; set; }
            public string knowFrom7 { get; set; }
            public string knowFrom8 { get; set; }
            public string knowFrom9 { get; set; }
            public string knowFrom10 { get; set; }
            public string knowFrom11 { get; set; }
        }

        //protected class sortList
        //{
        //    public string planId { get; set; }
        //    public int? sortnumberType { get; set; }
        //    public int? sortnumberGroup { get; set; }
        //    public string planName { get; set; }
        //    public string planCode { get; set; }
        //}

        public class ddlterm
        {
            public string sTerm { get; set; }
        }
    }
}