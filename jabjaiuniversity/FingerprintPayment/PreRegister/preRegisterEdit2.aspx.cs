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
using FingerprintPayment.PreRegister.CsCode;
using Newtonsoft.Json;
using OBS;
using OBS.Model;
using System.Diagnostics.Eventing.Reader;

namespace FingerprintPayment.PreRegister
{
    public partial class preRegisterEdit2 : PreRegisterGateway
    {
        protected string NoAssumptionSriracha = "";
        protected string NoSuankularbNonthaburi = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            var userData = UserData;
            int schoolID = userData.CompanyID;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                SqlConnection _conn = fcommon.ConfigSqlConnection(userData.Entities);
                Button1.Click += new EventHandler(Button1_Click);
                Button2.Click += new EventHandler(Button2_Click);

                if (!IsPostBack)
                {
                    List<branchSpec> SpecList1 = new List<branchSpec>();
                    branchSpec Spec1 = new branchSpec();

                    List<branchSpec> SpecList2 = new List<branchSpec>();
                    branchSpec Spec2 = new branchSpec();

                    // levelID for "ปวช." (1) & "ปวส." (2)
                    var levelID1 = _db.TLevels.Where(w => w.LevelName == "ปวช." && w.SchoolID == schoolID).Select(s => s.LevelID).ToList();
                    SpecList1.AddRange((from a in _db.TBranches.Where(w => w.SchoolID == schoolID)
                                        join b in _db.TBranchSubjects.Where(w => w.SchoolID == schoolID) on a.BranchId equals b.BranchId
                                        join c in _db.TBranchSpecs.Where(w => w.SchoolID == schoolID) on b.BranchSubjectId equals c.BranchSubjectId
                                        where a.cDel == null && b.cDel == null && c.cDel == null && levelID1.Contains(a.nTLevel.Value)
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

                    // levelID for "ปวช." (1) & "ปวส." (2)
                    var levelID2 = _db.TLevels.Where(w => w.LevelName == "ปวส." && w.SchoolID == schoolID).Select(s => s.LevelID).ToList();
                    SpecList2.AddRange((from a in _db.TBranches.Where(w => w.SchoolID == schoolID)
                                        join b in _db.TBranchSubjects.Where(w => w.SchoolID == schoolID) on a.BranchId equals b.BranchId
                                        join c in _db.TBranchSpecs.Where(w => w.SchoolID == schoolID) on b.BranchSubjectId equals c.BranchSubjectId
                                        where a.cDel == null && b.cDel == null && c.cDel == null && levelID2.Contains(a.nTLevel.Value)
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

                    var titlelist = _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus != "notworking").ToList();

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

                    // Year
                    string sqlQuery = string.Format(@"SELECT DISTINCT CAST([Year]-543 AS VARCHAR(4)) 'id', CAST([Year] AS VARCHAR(4)) 'name' FROM TRegisterSetup WHERE SchoolID = {0} AND cDel = 0 ORDER BY CAST([Year] AS VARCHAR(4)) DESC", schoolID);
                    List<EntityDropdown> resultYear = _db.Database.SqlQuery<EntityDropdown>(sqlQuery).ToList();
                    if (resultYear.Count > 0)
                    {
                        foreach (var r in resultYear)
                        {
                            optionYear.Items.Add(new ListItem { Text = r.name, Value = r.id, });
                        }
                    }

                    // Nation
                    var listNation = _db.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                    foreach (var l in listNation)
                    {
                        var item = new ListItem
                        {
                            Text = l.MasterDes,
                            Value = l.MasterCode
                        };
                        ddlNation.Items.Add(item);
                        ddlFamNation.Items.Add(item);
                        ddlFatherNation.Items.Add(item);
                        ddlMotherNation.Items.Add(item);
                    }
                    ddlNation.SelectedValue = "099";
                    ddlFamNation.SelectedValue = "099";
                    ddlFatherNation.SelectedValue = "099";
                    ddlMotherNation.SelectedValue = "099";

                    // Religion
                    var listReligion = _db.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                    foreach (var l in listReligion)
                    {
                        var item = new ListItem
                        {
                            Text = l.MasterDes,
                            Value = l.MasterCode
                        };
                        ddlReligion.Items.Add(item);
                        ddlFamReligion.Items.Add(item);
                        ddlFatherReligion.Items.Add(item);
                        ddlMotherReligion.Items.Add(item);
                    }

                    // Race
                    var listRace = _db.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                    foreach (var l in listRace)
                    {
                        var item = new ListItem
                        {
                            Text = l.MasterDes,
                            Value = l.MasterCode
                        };
                        ddlRace.Items.Add(item);
                        ddlFamRace.Items.Add(item);
                        ddlFatherRace.Items.Add(item);
                        ddlMotherRace.Items.Add(item);
                    }
                    ddlRace.SelectedValue = "099";
                    ddlFamRace.SelectedValue = "099";
                    ddlFatherRace.SelectedValue = "099";
                    ddlMotherRace.SelectedValue = "099";

                    //var item3 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(544).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(1).Year.ToString()
                    //};
                    //var item4 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(543).Year.ToString(),
                    //    Value = DateTime.Now.Year.ToString()
                    //};
                    //var item5 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(542).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(-1).Year.ToString()
                    //};
                    //var item6 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(541).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(-2).Year.ToString()
                    //};
                    //var item7 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(540).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(-3).Year.ToString()
                    //};
                    //var item8 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(539).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(-4).Year.ToString()
                    //};

                    //optionYear.Items.Add(item3);
                    //optionYear.Items.Add(item4);

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

                    LoadStudentType();

                    LoadFriendClass();

                    OpenData();

                    if (schoolID != 229)
                    {
                        NoAssumptionSriracha = "no-assumption-sriracha";
                    }
                    if (schoolID != 1043)
                    {
                        NoSuankularbNonthaburi = "no-suankularb-nonthaburi";
                    }
                }
                SetBodyEventOnLoad("");
            }
        }

        public void LoadStudentType()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT DISTINCT r.StudentType 'ID', (CASE r.StudentType WHEN 1 THEN N'นักเรียนใหม่' WHEN 2 THEN N'นักเรียนรักษาสิทธิ์' ELSE '' END) 'StudentType'
FROM TRegisterSetup r WHERE SchoolID = {0}", schoolID);
                List<EntityStudentTypeSetup> result = en.Database.SqlQuery<EntityStudentTypeSetup>(sqlQuery).ToList();

                ddlStudentType.DataSource = result;
                ddlStudentType.DataTextField = "StudentType";
                ddlStudentType.DataValueField = "ID";
                ddlStudentType.DataBind();

                // Set the default selected item, if desired.
                ddlStudentType.SelectedIndex = 0;
            }
        }

        public void LoadStudentClass(string studentType)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT DISTINCT CAST(r.nTSubLevel AS VARCHAR(5))+'/'+CAST(s.nTLevel AS VARCHAR(5)) 'nTSubLevel', s.SubLevel 
FROM TRegisterSetup r LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel
WHERE r.SchoolID = {0} AND r.StudentType = '{1}'", schoolID, studentType);
                List<EntityStudentClass> result = en.Database.SqlQuery<EntityStudentClass>(sqlQuery).ToList();

                optionLevel.DataSource = result;
                optionLevel.DataTextField = "SubLevel";
                optionLevel.DataValueField = "nTSubLevel";
                optionLevel.DataBind();
            }
        }

        public void LoadStudentPlan(string studentType, string classLevel)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT DISTINCT r.RegisterPlanSetupID 'ID', IIF(r.RegisterPlanSetupID=0, 'ทั้งหมด', p.PlanName) 'PlanName'
FROM TRegisterSetup r LEFT JOIN TRegisterPlanSetup p ON r.RegisterPlanSetupID = p.RegisterPlanSetupID AND r.nTSubLevel = p.nTSubLevel
WHERE r.SchoolID = {0} AND r.StudentType = '{1}' AND r.nTSubLevel = {2} AND r.cDel = 0", schoolID, studentType, classLevel);
                List<EntityPlanSetupEduProgram> result = en.Database.SqlQuery<EntityPlanSetupEduProgram>(sqlQuery).ToList();

                ddlPlan.DataSource = result;
                ddlPlan.DataTextField = "PlanName";
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataBind();
            }
        }

        public void LoadStudentBackupPlans(string jsonBackupPlans)
        {
            // แสดงเฉพาะแผนสำรองที่นักเรียนเลือกตอนลงทะเบียนสมัครเรียนออนไลน์ กรณีเลือก แผนการเรียน = ทั้งหมด
            List<RegisterOnline03.BackupPlans> backupPlans = JsonConvert.DeserializeObject<List<RegisterOnline03.BackupPlans>>(jsonBackupPlans);
            List<EntityPlanSetupEduProgram> result = backupPlans.Where(w => w.PlanID != null).Select(s => new EntityPlanSetupEduProgram { ID = (int)s.PlanID, Planname = s.PlanName }).ToList();

            ddlPlan.DataSource = result;
            ddlPlan.DataTextField = "PlanName";
            ddlPlan.DataValueField = "ID";
            ddlPlan.DataBind();
        }

        public void LoadFriendClass()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT CAST(nTSubLevel AS VARCHAR(10)) 'nTSubLevel', SubLevel 
FROM TSubLevel 
WHERE SchoolID = {0} AND nWorkingStatus = 1 AND cDel = 0", schoolID);
                List<EntityStudentClass> result = en.Database.SqlQuery<EntityStudentClass>(sqlQuery).ToList();

                friendSublevel.DataSource = result;
                friendSublevel.DataTextField = "SubLevel";
                friendSublevel.DataValueField = "nTSubLevel";
                friendSublevel.DataBind();

                friendSublevel.Items.Insert(0, new ListItem("เลือกระดับชั้น", "0"));
                friendSublevel.Items.Insert(friendSublevel.Items.Count, new ListItem("อื่นๆ", "-1"));

                // Set the default selected item, if desired.
                friendSublevel.SelectedIndex = 0;
            }
        }

        private void SetBodyEventOnLoad(string myFunc)
        {
            ((mp)this.Master).SetBody.Attributes.Add("onLoad", myFunc);
        }
        void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("preRegisterList2.aspx");
        }

        protected void Upload(object sender, EventArgs e)
        {

        }

        void Button1_Click(object sender, EventArgs e)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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

                string sStudentRace = ddlRace.SelectedValue;

                //string sStudentNation = stdNation.Text;
                string sStudentNation = ddlNation.SelectedValue;

                //string sStudentReligion = stdReligion.Text;
                string sStudentReligion = ddlReligion.SelectedValue;

                double n;
                double? oldGPA = null;
                bool isNumeric = double.TryParse(oldSchoolGPA.Text, out n);
                if (isNumeric == true)
                {
                    oldGPA = n;
                }

                string FatherR = ddlFatherRace.SelectedValue;
                //string FatherN = fatherNation.Text;
                string FatherN = ddlFatherNation.SelectedValue;
                string FatherRLG = ddlFatherReligion.SelectedValue;

                string MotherR = ddlMotherRace.SelectedValue;
                //string MotherN = motherNation.Text;
                string MotherN = ddlMotherNation.SelectedValue;
                string MotherRLG = ddlMotherReligion.SelectedValue;

                string GuardianR = ddlFamRace.SelectedValue;
                //string GuardianN = famNation.Text;
                string GuardianN = ddlFamNation.SelectedValue;
                string GuardianRLG = ddlFamReligion.SelectedValue;

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

                int? levelID = Convert.ToInt32(nTLevel.Text);
                var levelObj = _db.TLevels.Where(w => w.SchoolID == schoolID && w.LevelID == levelID).FirstOrDefault();
                if (levelObj.LevelName == "ปวช.")
                {
                    opTime = string.IsNullOrEmpty(optionTime.SelectedValue) ? (int?)null : int.Parse(optionTime.SelectedValue);
                    opbrach = string.IsNullOrEmpty(optionBranch1.SelectedValue) ? (int?)null : int.Parse(optionBranch1.SelectedValue);
                }
                else if (levelObj.LevelName == "ปวส.")
                {
                    opTime = string.IsNullOrEmpty(optionTime.SelectedValue) ? (int?)null : int.Parse(optionTime.SelectedValue);
                    opbrach = string.IsNullOrEmpty(optionBranch2.SelectedValue) ? (int?)null : int.Parse(optionBranch2.SelectedValue);
                }
                //if (nTLevel.Text == "1")
                //{
                //    opTime = int.Parse(optionTime.SelectedValue);
                //    opbrach = int.Parse(optionBranch1.SelectedValue);
                //}
                //else if (nTLevel.Text == "2")
                //{
                //    opTime = int.Parse(optionTime.SelectedValue);
                //    opbrach = int.Parse(optionBranch2.SelectedValue);
                //}

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




                var data = _db.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == idn).FirstOrDefault();

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
                data.ParentEmail = txtParentEmail.Text; // ###

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
                data.StudentCategory = rdlStudentCategory.SelectedValue; // ###
                data.optionCourse = opCourse;
                data.StudentType = (ddlStudentType.SelectedValue == "" ? null : ddlStudentType.SelectedValue); // ###
                data.optionLevel = opLevel;

                int? newPlanID = (ddlPlan.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlPlan.SelectedValue));
                if (data.RegisterPlanSetupID != newPlanID && newPlanID != null)
                {
                    data.RegisterPlanSetupID = newPlanID; // ###

                    // ###
                    //int? levelID = Convert.ToInt32(nTLevel.Text);

                    var subLevel = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == data.optionLevel).FirstOrDefault();
                    string levelName = _db.TLevels.Where(w => w.SchoolID == schoolID && w.LevelID == subLevel.nTLevel).FirstOrDefault().LevelName;

                    int classRunID = _db.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registeryear && w.optionLevel == data.optionLevel && w.RegisterPlanSetupID == data.RegisterPlanSetupID).Count() + 1;
                    var examCode = ComFunction.GenerateExamCode2(thisyear, levelName, subLevel.SubLevel, (int)data.RegisterPlanSetupID, classRunID); // [year{2}]-[levelID][subLevelID]-[planID]-[runID] = 63-11-01-001
                    data.ExamCode = examCode; // ###
                }

                // Check old and new value register year & student type & register level
                if ((int)Session["preRegisterEdit2_oldRegisterYear"] != data.registerYear || (string)Session["preRegisterEdit2_oldStudentType"] != data.StudentType || (int?)Session["preRegisterEdit2_oldOptionLevel"] != data.optionLevel)
                {
                    data.MainPlan = null;
                }

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
                data.ParentAnnualIncome = (ddlParentYearIncome.SelectedValue == "" ? null : ddlParentYearIncome.SelectedValue); // ###

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
                data.FatherEmail = txtFatherEmail.Text; // ###

                if (TextBox33.Text != "")
                    data.nFatherIncome = double.Parse(TextBox33.Text);
                else data.nFatherIncome = null;
                data.FatherAnnualIncome = (ddlFatherYearIncome.SelectedValue == "" ? null : ddlFatherYearIncome.SelectedValue); // ###

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
                data.MotherEmail = txtMotherEmail.Text; // ###

                if (TextBox40.Text != "")
                    data.nMotherIncome = double.Parse(TextBox40.Text);
                else data.nMotherIncome = null;
                data.MotherAnnualIncome = (ddlMotherYearIncome.SelectedValue == "" ? null : ddlMotherYearIncome.SelectedValue); // ###

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
                data.houseRegistrationProvince = string.IsNullOrEmpty(ddlprovince2.SelectedValue) ? (int?)null : int.Parse(ddlprovince2.SelectedValue);
                data.houseRegistrationAumpher = string.IsNullOrEmpty(txtAumper2.SelectedValue) ? (int?)null : int.Parse(txtAumper2.SelectedValue);
                data.houseRegistrationTumbon = string.IsNullOrEmpty(txtTumbon2.SelectedValue) ? (int?)null : int.Parse(txtTumbon2.SelectedValue);
                data.bornFrom = TextBox22.Text;
                data.bornFromProvince = string.IsNullOrEmpty(ddlprovince3.SelectedValue) ? (int?)null : int.Parse(ddlprovince3.SelectedValue);
                data.bornFromAumpher = string.IsNullOrEmpty(txtAumper3.SelectedValue) ? (int?)null : int.Parse(txtAumper3.SelectedValue);
                data.bornFromTumbon = string.IsNullOrEmpty(txtTumbon3.SelectedValue) ? (int?)null : int.Parse(txtTumbon3.SelectedValue);
                data.moveOutReason = TextBox23.Text;
                data.sStudentHousePhone = TextBox50.Text;
                data.friendName = friendName.Text;
                data.friendPhone = friendPhone.Text;
                data.friendLastName = friendLast.Text;
                if (friendSublevel.SelectedValue != "0")
                    data.friendSubLevel = int.Parse(friendSublevel.SelectedValue);
                data.familyStatus = int.Parse(familyStatus.SelectedValue);
                data.UpdatedBy = UserData.UserID;
                data.UpdatedDate = DateTime.Now;


                // Upload document file
                string fileName = "", byteData = "", linkFile = "", contentType = "";
                int docID = 0, typeID = 0, vfiID = 0;

                if (FileDocument01.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument01.PostedFile.FileName);
                    contentType = FileDocument01.PostedFile.ContentType;

                    Stream fs = FileDocument01.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 1; typeID = 1; vfiID = 1;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument021.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument021.PostedFile.FileName);
                    contentType = FileDocument021.PostedFile.ContentType;

                    Stream fs = FileDocument021.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 1; vfiID = 3;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument022.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument022.PostedFile.FileName);
                    contentType = FileDocument022.PostedFile.ContentType;

                    Stream fs = FileDocument022.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 2; vfiID = 4;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument023.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument023.PostedFile.FileName);
                    contentType = FileDocument023.PostedFile.ContentType;

                    Stream fs = FileDocument023.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 3; vfiID = 5;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument024.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument024.PostedFile.FileName);
                    contentType = FileDocument024.PostedFile.ContentType;

                    Stream fs = FileDocument024.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 4; vfiID = 171;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument031.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument031.PostedFile.FileName);
                    contentType = FileDocument031.PostedFile.ContentType;

                    Stream fs = FileDocument031.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 3; typeID = 1; vfiID = 7;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument032.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument032.PostedFile.FileName);
                    contentType = FileDocument032.PostedFile.ContentType;

                    Stream fs = FileDocument032.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 3; typeID = 2; vfiID = 8;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument041.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument041.PostedFile.FileName);
                    contentType = FileDocument041.PostedFile.ContentType;

                    Stream fs = FileDocument041.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 4; typeID = 1; vfiID = 169;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument042.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument042.PostedFile.FileName);
                    contentType = FileDocument042.PostedFile.ContentType;

                    Stream fs = FileDocument042.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 4; typeID = 2; vfiID = 170;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument051.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument051.PostedFile.FileName);
                    contentType = FileDocument051.PostedFile.ContentType;

                    Stream fs = FileDocument051.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 1; vfiID = 11;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument052.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument052.PostedFile.FileName);
                    contentType = FileDocument052.PostedFile.ContentType;

                    Stream fs = FileDocument052.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 2; vfiID = 12;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument053.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument053.PostedFile.FileName);
                    contentType = FileDocument053.PostedFile.ContentType;

                    Stream fs = FileDocument053.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 3; vfiID = 13;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument06.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument06.PostedFile.FileName);
                    contentType = FileDocument06.PostedFile.ContentType;

                    Stream fs = FileDocument06.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 6; typeID = 1; vfiID = 14;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument07.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument07.PostedFile.FileName);
                    contentType = FileDocument07.PostedFile.ContentType;

                    Stream fs = FileDocument07.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 7; typeID = 1; vfiID = 15;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument08.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument08.PostedFile.FileName);
                    contentType = FileDocument08.PostedFile.ContentType;

                    Stream fs = FileDocument08.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 8; typeID = 1; vfiID = 16;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument09.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument09.PostedFile.FileName);
                    contentType = FileDocument09.PostedFile.ContentType;

                    Stream fs = FileDocument09.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 9; typeID = 1; vfiID = 17;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument10.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument10.PostedFile.FileName);
                    contentType = FileDocument10.PostedFile.ContentType;

                    Stream fs = FileDocument10.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 10; typeID = 1; vfiID = 18;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument11.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument11.PostedFile.FileName);
                    contentType = FileDocument11.PostedFile.ContentType;

                    Stream fs = FileDocument11.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 11; typeID = 1; vfiID = 19;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument12.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument12.PostedFile.FileName);
                    contentType = FileDocument12.PostedFile.ContentType;

                    Stream fs = FileDocument12.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 12; typeID = 1; vfiID = 166;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument13.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument13.PostedFile.FileName);
                    contentType = FileDocument13.PostedFile.ContentType;

                    Stream fs = FileDocument13.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 13; typeID = 1; vfiID = 167;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                if (FileDocument14.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument14.PostedFile.FileName);
                    contentType = FileDocument14.PostedFile.ContentType;

                    Stream fs = FileDocument14.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 14; typeID = 1; vfiID = 168;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, data.preRegisterId);

                    TPreRegisterDocument preRegisterDocument = _db.TPreRegisterDocument.Where(w => w.preRegisterId == data.preRegisterId && w.DocumentID == docID && w.Type == typeID && w.VFIID == vfiID).FirstOrDefault();
                    if (preRegisterDocument == null)
                    {
                        preRegisterDocument = new TPreRegisterDocument
                        {
                            preRegisterId = data.preRegisterId,
                            DocumentID = docID,
                            Type = typeID,
                            VFIID = vfiID,
                            FileName = fileName,
                            ContentType = contentType,
                            FilePath = linkFile,
                            SchoolID = schoolID,
                            UpdateDate = DateTime.Now
                        };
                        _db.TPreRegisterDocument.Add(preRegisterDocument);
                    }
                    else
                    {
                        preRegisterDocument.FileName = fileName;
                        preRegisterDocument.ContentType = contentType;
                        preRegisterDocument.FilePath = linkFile;
                        preRegisterDocument.UpdateDate = DateTime.Now;
                    }
                }

                _db.SaveChanges();
                Accounting.Tuitionfee.Setting.SaveInvoiceData(data);

                Response.Redirect("preRegisterList2.aspx");
            }
        }

        private void OpenData()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {


                string id = Request.QueryString["id"];
                int idn = int.Parse(id);
                var data = _db.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == idn).FirstOrDefault();

                if (string.IsNullOrEmpty(data.sStudentPicture))
                {
                    profileimage.ImageUrl = "https://jabjaistorage.obs.ap-southeast-3.myhuaweicloud.com/sb_userprofile/201782151010735704913.png";
                    profileimage.Width = 180;
                    profileimage.Height = 180;
                }
                else
                {
                    if (data.sStudentPicture.IndexOf("?x-image-process=image/resize,m_fill,h_300,w_270") >= 0)
                    {
                        profileimage.ImageUrl = data.sStudentPicture + "&" + DateTime.Now.Ticks;
                    }
                    else
                    {
                        profileimage.ImageUrl = data.sStudentPicture + "?" + DateTime.Now.Ticks;
                    }
                }

                optionYear.SelectedValue = data.registerYear.ToString();
                rdlStudentCategory.SelectedValue = data.StudentCategory; // ###

                optionCourse.SelectedValue = data.optionCourse.ToString();
                ddlStudentType.SelectedValue = data.StudentType; // ###

                LoadStudentClass(ddlStudentType.SelectedValue);
                if (data.optionLevel != null)
                {
                    var qq = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == data.optionLevel).FirstOrDefault();
                    optionLevel.SelectedValue = data.optionLevel + "/" + qq.nTLevel;

                    nTSubLevel.Text = Convert.ToString(data.optionLevel);
                    nTLevel.Text = Convert.ToString(qq.nTLevel);
                }

                // Store old value register year & student type & register level
                Session["preRegisterEdit2_oldRegisterYear"] = data.registerYear;
                Session["preRegisterEdit2_oldStudentType"] = data.StudentType;
                Session["preRegisterEdit2_oldOptionLevel"] = data.optionLevel;

                if (data.MainPlan == null)
                {
                    LoadStudentPlan(ddlStudentType.SelectedValue, nTSubLevel.Text);
                }
                else
                {
                    LoadStudentBackupPlans(data.BackupPlans);
                }
                //LoadStudentPlan(ddlStudentType.SelectedValue, nTSubLevel.Text);
                ddlPlan.SelectedValue = (data.RegisterPlanSetupID == null ? "" : data.RegisterPlanSetupID.Value.ToString()); // ###

                // Disable change plan when moved to Student Data(TUser)
                if (data.saveAsSID != null)
                {
                    optionYear.Enabled = false;
                    ddlStudentType.Enabled = false;
                    optionLevel.Enabled = false;
                    ddlPlan.Enabled = false;
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
                if (data.dBirth != null)
                {
                    string day = data.dBirth.Value.Day.ToString();
                    if (day.Length == 1)
                        day = "0" + day;
                    DropDownList1.SelectedValue = day;
                    DropDownList2.SelectedValue = data.dBirth.Value.Month.ToString("00");
                    ddlAge.SelectedValue = data.dBirth.Value.Year.ToString();
                }
                //stdNation.Text = data.sStudentNation;
                ddlNation.SelectedValue = data.sStudentNation;
                ddlRace.SelectedValue = data.sStudentRace;
                ddlReligion.SelectedValue = data.sStudentReligion;


                SetPAT(data.sStudentProvince, data.sStudentAumpher, data.sStudentTumbon, ddlprovince, txtAumper, txtTumbon);

                SetPAT2(data.houseRegistrationProvince, data.houseRegistrationAumpher, data.houseRegistrationTumbon, ddlprovince2, txtAumper2, txtTumbon2);

                SetPAT2(data.bornFromProvince, data.bornFromAumpher, data.bornFromTumbon, ddlprovince3, txtAumper3, txtTumbon3);

                SetPAT(data.oldSchoolProvince, data.oldSchoolAumpher, data.oldSchoolTumbon, oldSchoolProvince, oldSchoolAumpher, oldSchoolTumbon);


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
                txtParentEmail.Text = data.ParentEmail; // ###
                famPost.Text = data.sFamilyPost;

                famRoad.Text = data.sFamilyRoad;
                famSoy.Text = data.sFamilySoy;
                famTitle2.SelectedValue = data.nFamilyTitle.ToString();

                var famtitle = _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == data.nFamilyTitle).FirstOrDefault();

                SetPAT(data.sFamilyProvince, data.sFamilyAumpher, data.sFamilyTumbon, famProvince, famaumpher, famTumbon);

                ddlFamRace.SelectedValue = data.sFamilyRace;
                if (data.sFamilyRelate == "พุทธ")
                    famRelation.Text = "บิดา";
                else famRelation.Text = data.sFamilyRelate;
                //famNation.Text = data.sFamilyNation;
                ddlFamNation.SelectedValue = data.sFamilyNation;
                ddlFamReligion.SelectedValue = data.sFamilyReligion;
                sStudentidtxt.Text = data.sStudentID;
                txtExamCode.Text = data.ExamCode; // ###

                fatherHome.Text = data.sFatherHomeNumber;
                fatherIdCard.Text = data.sFatherIdCardNumber;
                //fatherJob.Text = data.fatherIncome.ToString();
                fatherLast.Text = data.sFatherLastName;
                fatherMuu.Text = data.sFatherMuu;
                fatherName.Text = data.sFatherFirstName;
                //fatherNation.Text = data.sFatherNation;
                ddlFatherNation.SelectedValue = data.sFatherNation;
                //fatherPhone.Text = data.sFatherPhone;
                fatherPost.Text = data.sFatherPost;

                ddlFatherRace.SelectedValue = data.sFatherRace;
                ddlFatherReligion.SelectedValue = data.sFatherReligion;
                fatherRoad.Text = data.sFatherRoad;
                fatherSoy.Text = data.sFatherSoy;
                fatherTitle2.SelectedValue = data.FatherTitle.ToString();
                var fatitle = _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == data.FatherTitle).FirstOrDefault();
                //fatitletxt.Text = fatitle.titleDescription;
                //fatitlenum.Text = fatitle.nTitleid.ToString();

                SetPAT(data.sFatherProvince, data.sFatherAumpher, data.sFatherTumbon, fatherProvince, fatherAumpher, fatherTumbon);

                motherHome.Text = data.sMotherHomeNumber;
                motherIdCard.Text = data.sMotherIdCardNumber;
                //motherJob.Text = data.motherIncome;
                motherLast.Text = data.sMotherLastName;
                motherMuu.Text = data.sMotherMuu;
                motherName.Text = data.sMotherFirstName;
                //motherNation.Text = data.sMotherNation;
                ddlMotherNation.SelectedValue = data.sMotherNation;
                //motherPhone.Text = data.sMotherPhone;
                motherPost.Text = data.sMotherPost;

                ddlMotherRace.SelectedValue = data.sMotherRace;
                ddlMotherReligion.SelectedValue = data.sMotherReligion;
                motherRoad.Text = data.sMotherRoad;
                motherSoy.Text = data.sMotherSoy;
                motherTitle2.SelectedValue = data.MotherTitle.ToString();
                var motitle = _db.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == data.MotherTitle).FirstOrDefault();
                //motitletxt.Text = motitle.titleDescription;
                //motitlenum.Text = motitle.nTitleid.ToString();

                SetPAT(data.sMotherProvince, data.sMotherAumpher, data.sMotherTumbon, motherProvince, motherAumpher, motherTumbon);

                stdWeight.Text = data.nWeight.ToString();
                stdHeight.Text = data.nHeight.ToString();
                if (data.sBlood != null) ddlBlood.SelectedValue = data.sBlood.ToString();
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
                ddlParentYearIncome.SelectedValue = data.ParentAnnualIncome; // ###

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
                txtFatherEmail.Text = data.FatherEmail; // ###

                if (data.nFatherIncome != null)
                {
                    double fain = (double)data.nFatherIncome;
                    TextBox33.Text = fain.ToString("0.00");
                }
                ddlFatherYearIncome.SelectedValue = data.FatherAnnualIncome; // ###

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
                txtMotherEmail.Text = data.MotherEmail; // ###

                if (data.nMotherIncome != null)
                {
                    double moin = (double)data.nMotherIncome;
                    TextBox40.Text = moin.ToString("0.00");
                }
                ddlMotherYearIncome.SelectedValue = data.MotherAnnualIncome; // ###

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

                string documentScript = "";
                List<TPreRegisterDocument> preRegisterDocuments = _db.TPreRegisterDocument.Where(w => w.SchoolID == schoolID && w.preRegisterId == data.preRegisterId).ToList();
                foreach (var rd in preRegisterDocuments)
                {
                    documentScript += string.Format(@"$('.no-file[data-did={0}][data-tid={1}]').addClass('ready').removeClass('no-file');", rd.DocumentID, rd.Type);
                }

                string specialScript = "";
                if (schoolID == 1043)
                {
                    specialScript = string.Format(@"$('.re-order').each(function(i){{$(this).text(parseInt($(this).text())+3);}});");
                }

                ltrScript.Text = string.Format(@"<script type=""text/javascript"" language=""javascript"">
    $(document).ready(function () {{
        {0} {1}
    }});
</script>", documentScript, specialScript);
            }
        }

        public void SetPAT(string provinceData, string aumpherData, string tumbonData, DropDownList province, DropDownList aumper, DropDownList tumbon)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (!string.IsNullOrEmpty(provinceData))
                {
                    province.SelectedValue = provinceData;
                    int.TryParse(provinceData, out int provinceID);
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == provinceID).ToList(), aumper, "", "AMPHUR_ID", "AMPHUR_NAME");
                    if (!string.IsNullOrEmpty(aumpherData))
                    {
                        aumper.SelectedValue = aumpherData;
                        int.TryParse(aumpherData, out int aumpherID);
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == aumpherID).ToList(), tumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        if (!string.IsNullOrEmpty(tumbonData))
                        {
                            tumbon.SelectedValue = tumbonData;
                        }
                        else
                        {
                            tumbon.Items.Insert(0, new ListItem { Text = "เลือกตำบล", Value = "0" });
                            tumbon.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        aumper.Items.Insert(0, new ListItem { Text = "เลือกอำเภอ", Value = "0" });
                        aumper.SelectedValue = "0";

                        tumbon.Items.Clear();
                        tumbon.Items.Add(new ListItem { Text = "เลือกตำบล", Value = "0" });
                        tumbon.SelectedValue = "0";
                    }
                }
                else
                {
                    province.Items.Insert(0, new ListItem { Text = "เลือกจังหวัด", Value = "0" });
                    province.SelectedValue = "0";

                    aumper.Items.Clear();
                    aumper.Items.Add(new ListItem { Text = "เลือกอำเภอ", Value = "0" });
                    aumper.SelectedValue = "0";

                    tumbon.Items.Clear();
                    tumbon.Items.Add(new ListItem { Text = "เลือกตำบล", Value = "0" });
                    tumbon.SelectedValue = "0";
                }
            }
        }

        public void SetPAT2(int? provinceData, int? aumpherData, int? tumbonData, DropDownList province, DropDownList aumper, DropDownList tumbon)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                if (provinceData != null)
                {
                    province.SelectedValue = provinceData?.ToString();
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == provinceData).ToList(), aumper, "", "AMPHUR_ID", "AMPHUR_NAME");
                    if (aumpherData != null)
                    {
                        aumper.SelectedValue = aumpherData?.ToString();
                        fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == aumpherData).ToList(), tumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                        if (tumbonData != null)
                        {
                            tumbon.SelectedValue = tumbonData?.ToString();
                        }
                        else
                        {
                            tumbon.Items.Insert(0, new ListItem { Text = "เลือกตำบล", Value = "0" });
                            tumbon.SelectedValue = "0";
                        }
                    }
                    else
                    {
                        aumper.Items.Insert(0, new ListItem { Text = "เลือกอำเภอ", Value = "0" });
                        aumper.SelectedValue = "0";

                        tumbon.Items.Clear();
                        tumbon.Items.Add(new ListItem { Text = "เลือกตำบล", Value = "0" });
                        tumbon.SelectedValue = "0";
                    }
                }
                else
                {
                    province.Items.Insert(0, new ListItem { Text = "เลือกจังหวัด", Value = "0" });
                    province.SelectedValue = "0";

                    aumper.Items.Clear();
                    aumper.Items.Add(new ListItem { Text = "เลือกอำเภอ", Value = "0" });
                    aumper.SelectedValue = "0";

                    tumbon.Items.Clear();
                    tumbon.Items.Add(new ListItem { Text = "เลือกตำบล", Value = "0" });
                    tumbon.SelectedValue = "0";
                }
            }
        }

        class branchSpec
        {

            public int branchSpecId { get; set; }
            public string name { get; set; }
        }

        // ###
        protected void ddlStudentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentClass(ddlStudentType.SelectedValue);
            LoadStudentPlan(ddlStudentType.SelectedValue, nTSubLevel.Text);
        }

        protected void optionLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentPlan(ddlStudentType.SelectedValue, nTSubLevel.Text);
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