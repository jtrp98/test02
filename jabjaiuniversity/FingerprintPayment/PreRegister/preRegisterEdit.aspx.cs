using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using FingerprintPayment.Class;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using WebGrease.Css.Extensions;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OBS;
using OBS.Model;

namespace FingerprintPayment.PreRegister
{
    public partial class preRegisterEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = Session["sEntities"].ToString();

                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
                Button1.Click += new EventHandler(Button1_Click);
                Button2.Click += new EventHandler(Button2_Click);

                if (!IsPostBack)
                {
                    List<TSubLevel> SubLevel = new List<TSubLevel>();
                    TSubLevel sub = new TSubLevel();


                    foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nWorkingStatus == 1))
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
                            Value = t.nTSubLevel.ToString() + "/" + t.nTLevel.ToString()
                        };
                        optionLevel.Items.Add(item);
                    }


                    List<branchSpec> SpecList1 = new List<branchSpec>();
                    branchSpec Spec1 = new branchSpec();

                    List<branchSpec> SpecList2 = new List<branchSpec>();
                    branchSpec Spec2 = new branchSpec();

                    SpecList1.AddRange((from a in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID)
                                        join b in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID) on a.BranchId equals b.BranchId
                                        join c in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID) on b.BranchSubjectId equals c.BranchSubjectId
                                        where a.cDel == null && b.cDel == null && c.cDel == null && a.nTLevel == 1

                                        select new branchSpec
                                        {
                                            branchSpecId = c.BranchSpecId,
                                            name = c.BranchSpecName,
                                        }
                                     ).ToList());

                    foreach (var t in SpecList1)
                    {
                        var item = new ListItem
                        {
                            Text = t.name,
                            Value = t.branchSpecId.ToString()
                        };
                        optionBranch1.Items.Add(item);
                    }

                    SpecList2.AddRange((from a in _db.TBranches.Where(w => w.SchoolID == userData.CompanyID)
                                        join b in _db.TBranchSubjects.Where(w => w.SchoolID == userData.CompanyID) on a.BranchId equals b.BranchId
                                        join c in _db.TBranchSpecs.Where(w => w.SchoolID == userData.CompanyID) on b.BranchSubjectId equals c.BranchSubjectId
                                        where a.cDel == null && b.cDel == null && c.cDel == null && a.nTLevel == 2

                                        select new branchSpec
                                        {
                                            branchSpecId = c.BranchSpecId,
                                            name = c.BranchSpecName,
                                        }
                                     ).ToList());

                    foreach (var t in SpecList2)
                    {
                        var item = new ListItem
                        {
                            Text = t.name,
                            Value = t.branchSpecId.ToString()
                        };
                        optionBranch2.Items.Add(item);
                    }

                    var titlelist = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.deleted != "1" && w.workStatus != "notworking").ToList();

                    foreach (var t in titlelist)
                    {
                        var item = new ListItem
                        {
                            Text = t.titleDescription,
                            Value = t.nTitleid.ToString()
                        };
                        stdtitle.Items.Add(item);
                        staywithTitle.Items.Add(item);
                        famTitle2.Items.Add(item);
                        fatherTitle2.Items.Add(item);
                        motherTitle2.Items.Add(item);
                    }



                    var item3 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(544).Year.ToString(),
                        Value = DateTime.Now.AddYears(1).Year.ToString()
                    };
                    var item4 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(543).Year.ToString(),
                        Value = DateTime.Now.Year.ToString()
                    };
                    var item5 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(542).Year.ToString(),
                        Value = DateTime.Now.AddYears(-1).Year.ToString()
                    };
                    var item6 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(541).Year.ToString(),
                        Value = DateTime.Now.AddYears(-2).Year.ToString()
                    };
                    var item7 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(540).Year.ToString(),
                        Value = DateTime.Now.AddYears(-3).Year.ToString()
                    };
                    var item8 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(539).Year.ToString(),
                        Value = DateTime.Now.AddYears(-4).Year.ToString()
                    };

                    optionYear.Items.Add(item3);
                    optionYear.Items.Add(item4);




                    fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(DropDownList17, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(DropDownList21, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(DropDownList26, "", 1900, "en-us", "th-TH");

                    var dtprovinces = dbmaster.provinces.ToList();
                    fcommon.LinqToDropDownList(dtprovinces, ddlprovince, "", "PROVINCE_ID", "PROVINCE_NAME");

                    int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper, "", "AMPHUR_ID", "AMPHUR_NAME");

                    int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, famProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, fatherProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), fatherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, motherProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), motherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, oldSchoolProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), oldSchoolAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, ddlprovince2, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper2, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon2, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, ddlprovince3, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper3, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon3, "", "DISTRICT_ID", "DISTRICT_NAME");

                    OpenData();
                }
                SetBodyEventOnLoad("");
            }
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("preRegisterList.aspx");
        }

        protected void Upload(object sender, EventArgs e)
        {

        }



        void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {



                if ((RadioButtonList8.SelectedValue != "ชาย") && (RadioButtonList8.SelectedValue != "หญิง"))
                {
                    Response.Write("<script>alert('กรุณาเลือกเพศ');</script>");
                    return;
                }

                if (DropDownList1.SelectedValue == "-1")
                {
                    Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                    return;
                }
                if (DropDownList2.SelectedValue == "-1")
                {
                    Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                    return;
                }
                if (ddlAge.SelectedValue == "-1")
                {
                    Response.Write("<script>alert('กรุณาเลือกวัน/เดือน/ปีเกิด');</script>");
                    return;
                }




                JabjaiEntity.DB.TPreRegister _User = new JabjaiEntity.DB.TPreRegister();


                string id = Request.QueryString["id"];
                int idn = int.Parse(id);
                string link = "";
                //int sID = 1;
                //if (_dbMaster.TUsers.ToList().Count > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;
                if (FileUpload1.HasFile)
                {
                    link = preRegisterPic(FileUpload1, 150, idn);
                }


                string birthDate = DropDownList1.SelectedValue;
                string birthMonth = DropDownList2.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
                string sex = "";
                if (RadioButtonList8.SelectedValue == "ชาย") sex = "0";
                else sex = "1";
                DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int? sonddl = 0;
                if (DropDownList5.SelectedValue != "")
                    sonddl = Int32.Parse(DropDownList5.SelectedValue);
                else sonddl = null;


                DateTime? _dBirth;
                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                int StudentTitle = int.Parse(stdtitle.SelectedValue);



                string sStudentRace = stdRace.Text;

                string sStudentNation = stdNation.Text;

                string sStudentReligion = stdReligion.Text;

                double n;
                double? oldGPA = null;
                bool isNumeric = double.TryParse(oldSchoolGPA.Text, out n);
                if (isNumeric == true)
                {
                    oldGPA = n;
                }

                string FatherR = fatherRace.Text;
                string FatherN = fatherNation.Text;
                string FatherRLG = fatherReligion.Text;

                string MotherR = motherRace.Text;
                string MotherN = motherNation.Text;
                string MotherRLG = motherReligion.Text;

                string GuardianR = famRace.Text;
                string GuardianN = famNation.Text;
                string GuardianRLG = famReligion.Text;

                string homenumFather = fatherHome.Text;
                string soyFather = fatherSoy.Text;
                string muuFather = fatherMuu.Text;
                string roadFather = fatherRoad.Text;
                string provinceFather = fatherProvince.SelectedValue;
                string aumpherFather = fatherAumpher.SelectedValue;
                string tumbonFather = fatherTumbon.SelectedValue;
                string postFather = fatherPost.Text;

                string homenumMother = motherHome.Text;
                string soyMother = motherSoy.Text;
                string muuMother = motherMuu.Text;
                string roadMother = motherRoad.Text;
                string provinceMother = motherProvince.SelectedValue;
                string aumpherMother = motherAumpher.SelectedValue;
                string tumbonMother = motherTumbon.SelectedValue;
                string postMother = motherPost.Text;

                double n2;
                double? Weight = null;
                bool isNumeric2 = double.TryParse(stdWeight.Text, out n2);
                if (isNumeric2 == true)
                {
                    Weight = n2;
                }

                double n3;
                double? Height = null;
                bool isNumeric3 = double.TryParse(stdHeight.Text, out n3);
                if (isNumeric3 == true)
                {
                    Height = n3;
                }

                string sickfood = sickFood.Text;

                string sickdrug = sickDrug.Text;

                string sickother = sickOther.Text;

                string sicknormal = sickNormal.Text;

                string sickdanger = sickDanger.Text;

                int registeryear = int.Parse(optionYear.SelectedValue);
                int thisyear = registeryear;
                if (thisyear < 2500) thisyear = thisyear + 543;

                int? opTime = null;
                int? opLevel = int.Parse(nTSubLevel.Text);
                int? opbrach = null;
                int? opCourse = int.Parse(optionCourse.SelectedValue);


                if (nTLevel.Text == "1")
                {
                    opTime = int.Parse(optionTime.SelectedValue);
                    opbrach = int.Parse(optionBranch1.SelectedValue);

                }
                else if (nTLevel.Text == "2")
                {
                    opTime = int.Parse(optionTime.SelectedValue);
                    opbrach = int.Parse(optionBranch2.SelectedValue);
                }



                double lat;
                double? lat2 = null;
                bool isNumericlat = double.TryParse(stdlat.Text, out lat);
                if (isNumericlat == true)
                {
                    lat2 = lat;
                }
                double lng;
                double? lng2 = null;
                bool isNumericlng = double.TryParse(stdlon.Text, out lng);
                if (isNumericlng == true)
                {
                    lng2 = lng;
                }




                var data = _db.TPreRegisters.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.preRegisterId == idn).FirstOrDefault();

                if (famaumpher.SelectedValue != "0")
                    data.sFamilyAumpher = famaumpher.SelectedValue;
                else data.sFamilyAumpher = "";
                data.sFamilyHomeNumber = famHomenum.Text;
                data.sFamilyIdCardNumber = famIdCard.Text;
                data.sFamilyLast = famLast.Text;
                data.sFamilyMuu = famMuu.Text;
                data.sFamilyName = famName.Text;
                data.sFamilyNation = GuardianN;
                data.sFamilyPost = famPost.Text;
                if (famProvince.SelectedValue != "0")
                    data.sFamilyProvince = famProvince.SelectedValue;
                else data.sFamilyProvince = "";
                data.sFamilyRace = GuardianR;
                data.sFamilyRelate = famRelation.Text;
                data.sFamilyReligion = GuardianRLG;
                data.sFamilyRoad = famRoad.Text;
                data.sFamilySoy = famSoy.Text;
                data.nFamilyTitle = int.Parse(famTitle2.SelectedValue);
                if (famTumbon.SelectedValue != "0")
                    data.sFamilyTumbon = famTumbon.SelectedValue;
                else data.sFamilyTumbon = "";
                data.sFatherAumpher = aumpherFather;
                data.sFatherFirstName = fatherName.Text;
                data.sFatherHomeNumber = homenumFather;
                data.sFatherIdCardNumber = fatherIdCard.Text;
                data.sFatherLastName = fatherLast.Text;
                data.sFatherMuu = muuFather;
                data.sFatherNation = FatherN;
                data.sFatherPhone = TextBox34.Text;
                data.sFatherPost = postFather;
                data.sFatherProvince = provinceFather;
                data.sFatherRace = FatherR;
                data.sFatherReligion = FatherRLG;
                data.sFatherRoad = roadFather;
                data.sFatherSoy = soyFather;
                data.FatherTitle = int.Parse(fatherTitle2.SelectedValue);
                data.sFatherTumbon = tumbonFather;
                data.cSex = sex;
                data.dBirth = _dBirth;
                data.dPicUpdate = DateTime.Now;
                data.MotherTitle = int.Parse(motherTitle2.SelectedValue);
                data.nHeight = Height;
                data.nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0;
                data.nSonNumber = sonddl;
                data.nWeight = Weight;

                if (oldSchoolName.Text != "")
                {

                    data.oldSchoolName = oldSchoolName.Text;
                    if (oldSchoolProvince.SelectedValue != "0")
                    {
                        data.oldSchoolProvince = oldSchoolProvince.SelectedValue;
                        data.oldSchoolTumbon = oldSchoolTumbon.SelectedValue;
                        data.oldSchoolAumpher = oldSchoolAumpher.SelectedValue;
                    }
                }
                if (oldSchoolGraduated.SelectedValue == "")
                    data.oldSchoolGraduated = null;
                else data.oldSchoolGraduated = oldSchoolGraduated.SelectedValue;

                bool isNum = double.TryParse(oldSchoolGPA.Text, out n);
                if (isNum == true)
                {
                    data.oldSchoolGPA = n;
                }

                data.registerYear = registeryear;
                data.sAddress = famHomenum.Text + " " + famMuu.Text + " " + famSoy.Text + " " + famRoad.Text + " " +
                             famTumbon.Text + " " + famaumpher.Text + " " + famProvince.Text + " " + famPost.Text;
                data.sBlood = ddlBlood.SelectedValue;
                data.sEmail = Emailtxt.Text;
                data.sIdentification = citizenID.Text;
                data.sLastname = sLastTH.Text;
                data.sMotherAumpher = aumpherMother;
                data.sMotherFirstName = motherName.Text;
                data.sMotherHomeNumber = homenumMother;
                data.sMotherIdCardNumber = motherIdCard.Text;
                data.sMotherLastName = motherLast.Text;
                data.sMotherMuu = muuMother;
                data.sMotherNation = MotherN;
                data.sMotherPhone = TextBox45.Text;
                data.sMotherPost = postMother;
                data.sMotherProvince = provinceMother;
                data.sMotherRace = MotherR;
                data.sMotherReligion = MotherRLG;
                data.sMotherRoad = roadMother;
                data.sMotherSoy = soyMother;
                data.sMotherTumbon = tumbonMother;
                data.sName = sNameTH.Text;
                data.sNickName = sNick.Text;
                data.sPhone = sPhone.Text;
                //data.sPhoneMail = famEmail.Text;
                data.sPhoneOne = famPhone1.Text;
                data.sPhoneThree = famPhone3.Text;

                data.sPhoneTwo = famPhone2.Text;
                data.sSickDanger = sickdanger;
                data.sSickDrug = sickdrug;
                data.sSickFood = sickfood;
                data.sSickNormal = sicknormal;
                data.sSickOther = sickother;
                if (txtAumper.SelectedValue != "0")
                    data.sStudentAumpher = txtAumper.SelectedValue;
                else data.sStudentAumpher = "";
                data.sStudentHomeNumber = stdHomenum.Text;
                data.sStudentID = sStudentidtxt.Text;
                data.sStudentIdCardNumber = citizenID.Text;
                data.sStudentLastEN = sLastEN.Text;
                data.sStudentMuu = stdMuu.Text;
                data.sStudentNameEN = sNameEN.Text;
                data.sStudentNation = sStudentNation;
                data.sStudentNameOther = sStudentNameOther.Text;
                data.sStudentLastOther = sStudentLastOther.Text;
                if (link != "")
                    data.sStudentPicture = link;
                data.sStudentPost = txtPost.Text;
                if (ddlprovince.SelectedValue != "0")
                    data.sStudentProvince = ddlprovince.SelectedValue;
                else data.sStudentProvince = "";
                data.sStudentRace = sStudentRace;
                data.sStudentReligion = sStudentReligion;
                data.sStudentRoad = stdRoad.Text;
                data.sStudentSoy = stdSoy.Text;
                if (txtTumbon.SelectedValue != "0")
                    data.sStudentTumbon = txtTumbon.SelectedValue;
                else data.sStudentTumbon = "";
                data.StudentTitle = StudentTitle;
                data.optionBranch = opbrach;
                data.optionCourse = opCourse;
                data.optionLevel = opLevel;

                data.optionTime = opTime;
                data.addressLat = lat2;
                data.addressLng = lng2;
                data.fatherIncome = TextBox33.Text;
                data.motherIncome = TextBox40.Text;

                int? totalson = Int32.Parse(DropDownList3.SelectedValue);
                if (totalson == 0)
                    data.nSonTotal = null;
                else data.nSonTotal = totalson;

                int? relativeStudyHere = Int32.Parse(DropDownList6.SelectedValue);
                if (relativeStudyHere == -1)
                    data.nRelativeStudyHere = null;
                else data.nRelativeStudyHere = relativeStudyHere;

                data.sFamilyNameEN = TextBox24.Text;
                data.sFamilyLastEN = TextBox25.Text;

                string combinedateFamily = DropDownList15.SelectedValue + "/" + DropDownList16.SelectedValue + "/" + DropDownList17.SelectedValue;
                DateTime? _dBirthFamily = null;
                if (DropDownList15.SelectedValue != "" && DropDownList16.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }
                data.dFamilyBirthDay = _dBirthFamily;

                if (DropDownList14.SelectedValue != "-1")
                    data.nFamilyRequestStudyMoney = int.Parse(DropDownList14.SelectedValue);
                else data.nFamilyRequestStudyMoney = null;

                if (DropDownList18.SelectedValue != "0")
                    data.sFamilyGraduated = int.Parse(DropDownList18.SelectedValue);
                else data.sFamilyGraduated = null;

                data.sFamilyJob = TextBox27.Text;
                data.sFamilyWorkPlace = TextBox26.Text;

                if (TextBox28.Text != "")
                    data.nFamilyIncome = double.Parse(TextBox28.Text);
                else data.nFamilyIncome = null;

                data.sFatherNameEN = TextBox29.Text;

                string combinedateFather = DropDownList19.SelectedValue + "/" + DropDownList20.SelectedValue + "/" + DropDownList21.SelectedValue;
                DateTime? _dBirthFather = null;
                if (DropDownList19.SelectedValue != "" && DropDownList20.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }
                data.dFatherBirthDay = _dBirthFather;
                data.sFatherLastEN = TextBox30.Text;

                if (DropDownList22.SelectedValue != "0")
                    data.sFatherGraduated = int.Parse(DropDownList22.SelectedValue);
                else data.sFatherGraduated = null;

                data.sFatherJob = TextBox31.Text;
                data.sFatherWorkPlace = TextBox32.Text;
                data.sFatherPhone = TextBox34.Text;
                data.sFatherPhone2 = TextBox35.Text;
                data.sFatherPhone3 = TextBox37.Text;

                if (TextBox33.Text != "")
                    data.nFatherIncome = double.Parse(TextBox33.Text);
                else data.nFatherIncome = null;

                data.sMotherNameEN = TextBox48.Text;
                data.sMotherLastEN = TextBox49.Text;

                string combinedateMother = DropDownList24.SelectedValue + "/" + DropDownList25.SelectedValue + "/" + DropDownList26.SelectedValue;
                DateTime? _dBirthMother = null;
                if (DropDownList24.SelectedValue != "" && DropDownList25.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }
                data.dMotherBirthDay = _dBirthMother;

                if (DropDownList23.SelectedValue != "0")
                    data.sMotherGraduated = int.Parse(DropDownList23.SelectedValue);
                else data.sMotherGraduated = null;

                data.sMotherJob = TextBox38.Text;
                data.sMotherWorkPlace = TextBox39.Text;
                data.sMotherPhone = TextBox45.Text;
                data.sMotherPhone2 = TextBox46.Text;
                data.sMotherPhone3 = TextBox47.Text;

                if (TextBox40.Text != "")
                    data.nMotherIncome = double.Parse(TextBox40.Text);
                else data.nMotherIncome = null;

                data.sNickNameEN = sNickEN.Text;
                data.sStudentHomeRegisterCode = stdHomenum2.Text;

                data.stayWithTitle = int.Parse(staywithTitle.SelectedValue);
                data.stayWithName = TextBox1.Text;
                data.stayWithLast = TextBox20.Text;
                data.stayWithEmergencyCall = TextBox6.Text;
                data.stayWithEmail = TextBox51.Text;

                if (DropDownList7.SelectedValue != "0")
                    data.HomeType = int.Parse(DropDownList7.SelectedValue);
                else data.HomeType = null;




                data.houseRegistrationNumber = TextBox13.Text;
                data.houseRegistrationMuu = TextBox15.Text;
                data.houseRegistrationSoy = TextBox16.Text;
                data.houseRegistrationRoad = TextBox19.Text;
                data.houseRegistrationPost = txtPost2.Text;
                data.houseRegistrationPhone = TextBox21.Text;
                data.bornFrom = TextBox22.Text;
                data.moveOutReason = TextBox23.Text;
                data.sStudentHousePhone = TextBox50.Text;
                data.friendName = friendName.Text;
                data.friendPhone = friendPhone.Text;
                data.friendLastName = friendLast.Text;
                if (friendSublevel.SelectedValue != "0")
                    data.friendSubLevel = int.Parse(friendSublevel.SelectedValue);
                data.familyStatus = int.Parse(familyStatus.SelectedValue);
                _db.SaveChanges();


                Response.Redirect("preRegisterList.aspx");
            }
        }
        private void OpenData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string id = Request.QueryString["id"];
            int idn = int.Parse(id);
            var data = _db.TPreRegisters.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.preRegisterId == idn).FirstOrDefault();

            profileimage.ImageUrl = data.sStudentPicture;
            if (profileimage.ImageUrl == "")
            {
                profileimage.ImageUrl = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                profileimage.Width = 180;
                profileimage.Height = 180;
            }

            optionYear.SelectedValue = data.registerYear.ToString();

            optionCourse.SelectedValue = data.optionCourse.ToString();

            if (data.optionLevel != null)
            {
                var qq = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == data.optionLevel).FirstOrDefault();
                optionLevel.SelectedValue = data.optionLevel + "/" + qq.nTLevel;

                nTSubLevel.Text = Convert.ToString(data.optionLevel);
                nTLevel.Text = Convert.ToString(qq.nTLevel);
            }

            if (data.optionTime != null)
                optionTime.SelectedValue = data.optionTime.ToString();
            if (data.optionBranch != null)
            {
                optionBranch1.SelectedValue = data.optionBranch.ToString();
                optionBranch2.SelectedValue = data.optionBranch.ToString();
            }


            if (data.nSonNumber != null)
                DropDownList5.SelectedValue = data.nSonNumber.ToString();

            if (data.cSex == "1") RadioButtonList8.SelectedIndex = 1;
            else RadioButtonList8.SelectedIndex = 0;
            sNameTH.Text = data.sName;
            sLastTH.Text = data.sLastname;
            sNameEN.Text = data.sStudentNameEN;
            sLastEN.Text = data.sStudentLastEN;
            sStudentNameOther.Text = data.sStudentNameOther;
            sStudentLastOther.Text = data.sStudentLastOther;
            sNick.Text = data.sNickName;
            citizenID.Text = data.sIdentification;
            string day = data.dBirth.Value.Day.ToString();
            if (day.Length == 1)
                day = "0" + day;
            DropDownList1.SelectedValue = day;
            DropDownList2.SelectedValue = data.dBirth.Value.Month.ToString("00");
            ddlAge.SelectedValue = data.dBirth.Value.Year.ToString();
            stdNation.Text = data.sStudentNation;
            stdRace.Text = data.sStudentRace;
            stdReligion.Text = data.sStudentReligion;



            int n;
            bool isNumeric = int.TryParse(data.sStudentProvince, out n);
            if (isNumeric == true && data.sStudentProvince != "0")
            {
                ddlprovince.SelectedValue = n.ToString();
                int PROVINCE_ID = int.Parse(data.sStudentProvince);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.sStudentAumpher != null)
                {
                    txtAumper.SelectedValue = data.sStudentAumpher;
                    if (data.sStudentAumpher != null && data.sStudentAumpher != "")
                    {
                        int AMPHUR_CODE = int.Parse(data.sStudentAumpher);
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        txtTumbon.SelectedValue = data.sStudentTumbon;
                    }
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                ddlprovince.Items.Add(item2);
                txtAumper.Items.Add(itemtum);
                txtTumbon.Items.Add(itemaum);

                ddlprovince.SelectedValue = "0";
                txtAumper.SelectedValue = "0";
                txtTumbon.SelectedValue = "0";
            }

            if (data.houseRegistrationProvince != null)
            {
                ddlprovince2.SelectedValue = data.houseRegistrationProvince.ToString();
                int PROVINCE_ID = (int)data.houseRegistrationProvince;
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper2, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.houseRegistrationAumpher != null)
                {
                    txtAumper2.SelectedValue = data.houseRegistrationAumpher.ToString();
                    int AMPHUR_CODE = (int)data.houseRegistrationAumpher;
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon2, "", "DISTRICT_ID", "DISTRICT_NAME");
                    txtTumbon2.SelectedValue = data.houseRegistrationTumbon.ToString();
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                ddlprovince2.Items.Add(item2);
                txtAumper2.Items.Add(itemtum);
                txtTumbon2.Items.Add(itemaum);

                ddlprovince2.SelectedValue = "0";
                txtAumper2.SelectedValue = "0";
                txtTumbon2.SelectedValue = "0";
            }

            if (data.bornFromProvince != null)
            {
                ddlprovince3.SelectedValue = data.bornFromProvince.ToString();
                int PROVINCE_ID = (int)data.bornFromProvince;
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper3, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.bornFromAumpher != null)
                {
                    txtAumper3.SelectedValue = data.bornFromAumpher.ToString();
                    int AMPHUR_CODE = (int)data.bornFromAumpher;
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon3, "", "DISTRICT_ID", "DISTRICT_NAME");
                    txtTumbon3.SelectedValue = data.bornFromTumbon.ToString();
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                ddlprovince3.Items.Add(item2);
                txtAumper3.Items.Add(itemtum);
                txtTumbon3.Items.Add(itemaum);

                ddlprovince3.SelectedValue = "0";
                txtAumper3.SelectedValue = "0";
                txtTumbon3.SelectedValue = "0";
            }

            if (data.oldSchoolProvince == null || data.oldSchoolProvince == "")
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                oldSchoolProvince.Items.Add(item2);
                oldSchoolAumpher.Items.Add(itemtum);
                oldSchoolTumbon.Items.Add(itemaum);

                oldSchoolProvince.SelectedValue = "0";
                oldSchoolAumpher.SelectedValue = "0";
                oldSchoolTumbon.SelectedValue = "0";
            }
            else
            {
                if (data.oldSchoolProvince != null)
                {
                    oldSchoolProvince.SelectedValue = data.oldSchoolProvince;
                    int PROVINCE_ID = int.Parse(data.oldSchoolProvince);
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), oldSchoolAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    if (data.oldSchoolAumpher != null)
                    {
                        oldSchoolAumpher.SelectedValue = data.oldSchoolAumpher;
                        if (data.oldSchoolAumpher != null && data.oldSchoolAumpher != "")
                        {
                            int AMPHUR_CODE = int.Parse(data.oldSchoolAumpher);
                            fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                            oldSchoolTumbon.SelectedValue = data.oldSchoolTumbon;
                        }
                    }
                }
            }
            oldSchoolName.Text = data.oldSchoolName;
            oldSchoolGPA.Text = data.oldSchoolGPA.ToString();
            if (data.oldSchoolGraduated != null)
                oldSchoolGraduated.SelectedValue = data.oldSchoolGraduated.ToString();

            famHomenum.Text = data.sFamilyHomeNumber;
            famIdCard.Text = data.sFamilyIdCardNumber;
            famLast.Text = data.sFamilyLast;
            famMuu.Text = data.sFamilyMuu;
            famName.Text = data.sFamilyName;
            famPhone1.Text = data.sPhoneOne;
            famPhone2.Text = data.sPhoneTwo;
            famPhone3.Text = data.sPhoneThree;
            famPost.Text = data.sFamilyPost;

            famRoad.Text = data.sFamilyRoad;
            famSoy.Text = data.sFamilySoy;
            famTitle2.SelectedValue = data.nFamilyTitle.ToString();

            var famtitle = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTitleid == data.nFamilyTitle).FirstOrDefault();

            isNumeric = int.TryParse(data.sFamilyProvince, out n);
            if (isNumeric == true && data.sFamilyProvince != "0")
            {
                famProvince.SelectedValue = data.sFamilyProvince.ToString();
                int PROVINCE_ID = int.Parse(data.sFamilyProvince);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.sFamilyAumpher != null)
                {
                    famaumpher.SelectedValue = data.sFamilyAumpher;
                    if (data.sFamilyAumpher != null && data.sFamilyAumpher != "")
                    {
                        int AMPHUR_CODE = int.Parse(data.sFamilyAumpher);
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        if (data.sFamilyTumbon != null) famTumbon.SelectedValue = data.sFamilyTumbon;
                    }
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                famProvince.Items.Add(item2);
                famaumpher.Items.Add(itemtum);
                famTumbon.Items.Add(itemaum);

                famProvince.SelectedValue = "0";
                famaumpher.SelectedValue = "0";
                famTumbon.SelectedValue = "0";
            }

            famRace.Text = data.sFamilyRace;
            if (data.sFamilyRelate == "พุทธ")
                famRelation.Text = "บิดา";
            else famRelation.Text = data.sFamilyRelate;
            famNation.Text = data.sFamilyNation;
            famReligion.Text = data.sFamilyReligion;
            sStudentidtxt.Text = data.sStudentID;

            fatherHome.Text = data.sFatherHomeNumber;
            fatherIdCard.Text = data.sFatherIdCardNumber;
            //fatherJob.Text = data.fatherIncome.ToString();
            fatherLast.Text = data.sFatherLastName;
            fatherMuu.Text = data.sFatherMuu;
            fatherName.Text = data.sFatherFirstName;
            fatherNation.Text = data.sFatherNation;
            //fatherPhone.Text = data.sFatherPhone;
            fatherPost.Text = data.sFatherPost;

            fatherRace.Text = data.sFatherRace;
            fatherReligion.Text = data.sFatherReligion;
            fatherRoad.Text = data.sFatherRoad;
            fatherSoy.Text = data.sFatherSoy;
            fatherTitle2.SelectedValue = data.FatherTitle.ToString();
            var fatitle = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTitleid == data.FatherTitle).FirstOrDefault();
            //fatitletxt.Text = fatitle.titleDescription;
            //fatitlenum.Text = fatitle.nTitleid.ToString();

            isNumeric = int.TryParse(data.sFatherProvince, out n);
            if (isNumeric == true && data.sFatherProvince != "0")
            {
                fatherProvince.SelectedValue = data.sFatherProvince.ToString();
                int PROVINCE_ID = int.Parse(data.sFatherProvince);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), fatherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.sFatherAumpher != null)
                {
                    fatherAumpher.SelectedValue = data.sFatherAumpher;
                    if (data.sFatherAumpher != null && data.sFatherAumpher != "")
                    {
                        int AMPHUR_CODE = int.Parse(data.sFatherAumpher);
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        if (data.sFatherTumbon != null) fatherTumbon.SelectedValue = data.sFatherTumbon;
                    }
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                fatherProvince.Items.Add(item2);
                fatherAumpher.Items.Add(itemtum);
                fatherTumbon.Items.Add(itemaum);

                fatherProvince.SelectedValue = "0";
                fatherAumpher.SelectedValue = "0";
                fatherTumbon.SelectedValue = "0";
            }


            motherHome.Text = data.sMotherHomeNumber;
            motherIdCard.Text = data.sMotherIdCardNumber;
            //motherJob.Text = data.motherIncome;
            motherLast.Text = data.sMotherLastName;
            motherMuu.Text = data.sMotherMuu;
            motherName.Text = data.sMotherFirstName;
            motherNation.Text = data.sMotherNation;
            //motherPhone.Text = data.sMotherPhone;
            motherPost.Text = data.sMotherPost;

            motherRace.Text = data.sMotherRace;
            motherReligion.Text = data.sMotherReligion;
            motherRoad.Text = data.sMotherRoad;
            motherSoy.Text = data.sMotherSoy;
            motherTitle2.SelectedValue = data.MotherTitle.ToString();
            var motitle = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTitleid == data.MotherTitle).FirstOrDefault();
            //motitletxt.Text = motitle.titleDescription;
            //motitlenum.Text = motitle.nTitleid.ToString();

            isNumeric = int.TryParse(data.sMotherProvince, out n);
            if (isNumeric == true && data.sMotherProvince != "0")
            {
                motherProvince.SelectedValue = data.sMotherProvince.ToString();
                int PROVINCE_ID = int.Parse(data.sMotherProvince);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), motherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                if (data.sMotherAumpher != null)
                {
                    motherAumpher.SelectedValue = data.sMotherAumpher;
                    if (data.sMotherAumpher != null && data.sMotherAumpher != "")
                    {
                        int AMPHUR_CODE = int.Parse(data.sMotherAumpher);
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        if (data.sMotherTumbon != null) motherTumbon.SelectedValue = data.sMotherTumbon;
                    }
                }
            }
            else
            {
                var item2 = new ListItem
                {
                    Text = "เลือกจังหวัด",
                    Value = "0"
                };

                var itemtum = new ListItem
                {
                    Text = "เลือกตำบล",
                    Value = "0"
                };
                var itemaum = new ListItem
                {
                    Text = "เลือกอำเภอ",
                    Value = "0"
                };

                motherProvince.Items.Add(item2);
                motherAumpher.Items.Add(itemtum);
                motherTumbon.Items.Add(itemaum);

                motherProvince.SelectedValue = "0";
                motherAumpher.SelectedValue = "0";
                motherTumbon.SelectedValue = "0";
            }


            stdWeight.Text = data.nWeight.ToString();
            stdHeight.Text = data.nHeight.ToString();
            ddlBlood.SelectedValue = data.sBlood.ToString();
            sickDanger.Text = data.sSickDanger;
            sickDrug.Text = data.sSickDrug;
            sickFood.Text = data.sSickFood;
            sickNormal.Text = data.sSickNormal;
            sickOther.Text = data.sSickOther;

            stdHomenum.Text = data.sStudentHomeNumber;
            stdMuu.Text = data.sStudentMuu;
            stdRoad.Text = data.sStudentRoad;
            stdSoy.Text = data.sStudentSoy;

            stdlat.Text = data.addressLat.ToString();
            stdlon.Text = data.addressLng.ToString();
            SendA.Value = data.addressLat.ToString();
            SendB.Value = data.addressLng.ToString();
            txtPost.Text = data.sStudentPost;
            sPhone.Text = data.sPhone;
            Emailtxt.Text = data.sEmail;

            if (data.nSonTotal != null)
                DropDownList3.SelectedValue = data.nSonTotal.ToString();
            if (data.nRelativeStudyHere != null)
                DropDownList6.SelectedValue = data.nRelativeStudyHere.ToString();

            TextBox24.Text = data.sFamilyNameEN;
            TextBox25.Text = data.sFamilyLastEN;

            if (data.dFamilyBirthDay != null)
            {
                DropDownList15.SelectedValue = data.dFamilyBirthDay.Value.Day.ToString("00");
                DropDownList16.SelectedValue = data.dFamilyBirthDay.Value.Month.ToString("00");
                DropDownList17.SelectedValue = data.dFamilyBirthDay.Value.Year.ToString();
            }

            DropDownList14.SelectedValue = data.nFamilyRequestStudyMoney.ToString();
            DropDownList18.SelectedValue = data.sFamilyGraduated.ToString();
            TextBox27.Text = data.sFamilyJob;
            TextBox26.Text = data.sFamilyWorkPlace;
            if (data.nFamilyIncome != null)
            {
                double familyin = (double)data.nFamilyIncome;
                TextBox28.Text = familyin.ToString("0.00");
            }

            TextBox29.Text = data.sFatherNameEN;
            if (data.dFatherBirthDay != null)
            {
                DropDownList19.SelectedValue = data.dFatherBirthDay.Value.Day.ToString("00");
                DropDownList20.SelectedValue = data.dFatherBirthDay.Value.Month.ToString("00");
                DropDownList21.SelectedValue = data.dFatherBirthDay.Value.Year.ToString();
            }
            TextBox30.Text = data.sFatherLastEN;

            DropDownList22.SelectedValue = data.sFatherGraduated.ToString();
            TextBox31.Text = data.sFatherJob;
            TextBox32.Text = data.sFatherWorkPlace;
            TextBox34.Text = data.sFatherPhone;
            TextBox35.Text = data.sFatherPhone2;
            TextBox37.Text = data.sFatherPhone3;

            if (data.nFatherIncome != null)
            {
                double fain = (double)data.nFatherIncome;
                TextBox33.Text = fain.ToString("0.00");
            }

            TextBox48.Text = data.sMotherNameEN;
            TextBox49.Text = data.sMotherLastEN;
            if (data.dMotherBirthDay != null)
            {
                DropDownList24.SelectedValue = data.dMotherBirthDay.Value.Day.ToString("00");
                DropDownList25.SelectedValue = data.dMotherBirthDay.Value.Month.ToString("00");
                DropDownList26.SelectedValue = data.dMotherBirthDay.Value.Year.ToString();
            }

            DropDownList23.SelectedValue = data.sMotherGraduated.ToString();
            TextBox38.Text = data.sMotherJob;
            TextBox39.Text = data.sMotherWorkPlace;
            TextBox45.Text = data.sMotherPhone;
            TextBox46.Text = data.sMotherPhone2;
            TextBox47.Text = data.sMotherPhone3;

            if (data.nMotherIncome != null)
            {
                double moin = (double)data.nMotherIncome;
                TextBox40.Text = moin.ToString("0.00");
            }

            sNickEN.Text = data.sNickNameEN;
            stdHomenum2.Text = data.sStudentHomeRegisterCode;
            staywithTitle.SelectedValue = data.stayWithTitle.ToString();
            TextBox1.Text = data.stayWithName;
            TextBox20.Text = data.stayWithLast;
            TextBox6.Text = data.stayWithEmergencyCall;
            TextBox51.Text = data.stayWithEmail;
            if (data.HomeType != null)
                DropDownList7.SelectedValue = data.HomeType.ToString();


            TextBox13.Text = data.houseRegistrationNumber;
            TextBox15.Text = data.houseRegistrationMuu;
            TextBox16.Text = data.houseRegistrationSoy;
            TextBox19.Text = data.houseRegistrationRoad;
            txtPost2.Text = data.houseRegistrationPost;
            TextBox21.Text = data.houseRegistrationPhone;
            TextBox22.Text = data.bornFrom;
            TextBox23.Text = data.moveOutReason;
            TextBox50.Text = data.sStudentHousePhone;

            if (data.StudentTitle != null)
                titlestd.Text = data.StudentTitle.ToString();
            if (data.stayWithTitle != null)
                titlestay.Text = data.stayWithTitle.ToString();
            if (data.nFamilyTitle != null)
                titlefam.Text = data.nFamilyTitle.ToString();
            if (data.FatherTitle != null)
                titlefa.Text = data.FatherTitle.ToString();
            if (data.MotherTitle != null)
                titlema.Text = data.MotherTitle.ToString();

            friendName.Text = data.friendName;
            friendLast.Text = data.friendLastName;
            friendPhone.Text = data.friendPhone;
            if (data.friendSubLevel != null)
                friendSublevel.SelectedValue = data.friendSubLevel.ToString();
            if (data.familyStatus != null)
                familyStatus.SelectedValue = data.familyStatus.ToString();
        }
        }

        class branchSpec
        {

            public int branchSpecId { get; set; }
            public string name { get; set; }
        }

        protected void ddlprovince_SelectedIndexChanged(object sender, EventArgs e)
        {

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(ddlprovince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }

        }

        protected void ddlprovince2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(ddlprovince2.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper2, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(txtAumper2.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon2, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void txtAumper2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(txtAumper2.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon2, "", "DISTRICT_ID", "DISTRICT_NAME");
                txtPost2.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void ddlprovince3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(ddlprovince3.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), txtAumper3, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(txtAumper3.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon3, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void txtAumper3_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(txtAumper3.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon3, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void famaumpher2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(famaumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
                famPost.Text = qAMPHUR.POSTCODE;
            }
        }



        protected void txtAumper_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(txtAumper.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), txtTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                txtPost.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void famaumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(famaumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
                famPost.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void famProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(famProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), famaumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(famaumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), famTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }



        protected void fatheraumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(fatherAumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
                fatherPost.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void fatherProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(fatherProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), fatherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(fatherAumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), fatherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void motheraumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(motherAumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
                motherPost.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void motherProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(motherProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), motherAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(motherAumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), motherTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void oldschoolaumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(oldSchoolAumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "- เลือกแขวง/ตำบล -", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void oldschoolProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(oldSchoolProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), oldSchoolAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(oldSchoolAumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), oldSchoolTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        public static string preRegisterPic(FileUpload files, int newwidthimg, int userid)
        {
            string link = "";
            var newname = "";
            string fileName = Path.GetFileName(files.PostedFile.FileName);

            var path = "";
            Dictionary<string, object> dict = new Dictionary<string, object>();
            HttpPostedFile postedFile = files.PostedFile;
            var name = postedFile.FileName.Split('.');
            //เช็ดไอดีนักเรียนตามรูปภาพ
            if (postedFile.ContentLength > 0)
            {
                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                string fileName2 = files.PostedFile.FileName;

                string month = DateTime.Now.Month.ToString();
                string year = DateTime.Now.Year.ToString();
                string date = DateTime.Now.DayOfYear.ToString();
                string hour = DateTime.Now.Hour.ToString();
                string min = DateTime.Now.Minute.ToString();
                string sec = DateTime.Now.Second.ToString();
                Random rnd = new Random();
                string rng = rnd.Next(10000000, 99999999).ToString();

                newname = "preRegisterPicture.png";
                link = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/" + userid + "/" + newname;
                //resize
                if (!System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid)))
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid));
                }
                path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
                System.Drawing.Image image = System.Drawing.Image.FromStream(postedFile.InputStream);
                if (image.Size.Width != newwidthimg)
                {
                    float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
                    int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
                    Bitmap resizeBitmap = new Bitmap(image, newwidthimg, newHeight);
                    Graphics thumbnailGraph = Graphics.FromImage(resizeBitmap);
                    thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
                    thumbnailGraph.DrawImage(image, imageRectangle);
                    resizeBitmap.Save(path, ImageFormat.Png);
                    thumbnailGraph.Dispose();
                    resizeBitmap.Dispose();
                    image.Dispose();
                }

            }

            try
            {

                ObsClient client;
                string bucketName = "userstorage";
                string endpoint = "obs.ap-southeast-2.myhuaweicloud.com";
                string AK = "UVIKYLYS2BECK7RGLFVN";
                string SK = "LCQcPRii1eSYVHInqMvJU0TklY5lH6VrUeikwhdK";

                ObsConfig config = new ObsConfig();
                config.Endpoint = endpoint;
                client = new ObsClient(AK, SK, config);

                var request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    ObjectKey = "userprofile/" + userid + "/" + newname,
                    FilePath = path,
                    
                };

                
                PutObjectResponse response = client.PutObject(request);

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname)))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/images/profile/" + userid + "/" + newname));
                }

                return response.ObjectUrl;
            }
            catch (Exception ex)
            {
                return "";
            }

        }
    }
}