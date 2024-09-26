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
using OBS;
using OBS.Model;

namespace FingerprintPayment.PreRegister
{
    public partial class preRegisterAddUser2 : PreRegisterGateway
    {
        protected string NoAssumptionSriracha = "";
        protected string NoSuankularbNonthaburi = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            var userData = UserData;
            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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
                    var levelID1 = _db.TLevels.Where(w => w.LevelName == "ปวช.").Select(s => s.LevelID).ToList();
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
                    var levelID2 = _db.TLevels.Where(w => w.LevelName == "ปวส.").Select(s => s.LevelID).ToList();
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

                    var itemz = new ListItem
                    {
                        Text = "เลือกคำนำหน้า",
                        Value = "0"
                    };
                    DropDownList4.Items.Add(itemz);
                    stayWithTitle.Items.Add(itemz);
                    famTitle.Items.Add(itemz);
                    fatherTitle.Items.Add(itemz);
                    motherTitle.Items.Add(itemz);

                    DropDownList4.SelectedValue = "0";
                    stayWithTitle.SelectedValue = "0";
                    famTitle.SelectedValue = "0";
                    fatherTitle.SelectedValue = "0";
                    motherTitle.SelectedValue = "0";

                    foreach (var t in titlelist)
                    {
                        var item = new ListItem
                        {
                            Text = t.titleDescription,
                            Value = t.nTitleid.ToString()
                        };
                        DropDownList4.Items.Add(item);
                        stayWithTitle.Items.Add(item);
                    }

                    foreach (var y in titlelist)
                    {
                        var item = new ListItem
                        {
                            Text = y.titleDescription,
                            Value = y.nTitleid.ToString()
                        };
                        famTitle.Items.Add(item);
                        fatherTitle.Items.Add(item);
                        motherTitle.Items.Add(item);
                    }

                    //var item3 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(543).Year.ToString(),
                    //    Value = DateTime.Now.Year.ToString()
                    //};
                    //var item4 = new ListItem
                    //{
                    //    Text = DateTime.Now.AddYears(544).Year.ToString(),
                    //    Value = DateTime.Now.AddYears(1).Year.ToString(),
                    //};

                    //optionYear.Items.Add(item3);
                    //optionYear.Items.Add(item4);

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

                    fcommon.ListYears(ddlAge, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(dFamilyBirthDayYY, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(dFatherBirthDayYY, "", 1900, "en-us", "th-TH");
                    fcommon.ListYears(dMotherBirthDayYY, "", 1900, "en-us", "th-TH");

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

                    fcommon.LinqToDropDownList(dtprovinces, houseRegistrationProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), houseRegistrationAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), houseRegistrationTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

                    fcommon.LinqToDropDownList(dtprovinces, bornFromProvince, "", "PROVINCE_ID", "PROVINCE_NAME");
                    fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), bornFromAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");
                    fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), bornFromTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");

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
                    txtTumbon.Items.Add(itemtum);
                    txtAumper.Items.Add(itemaum);

                    List<ListItem> listCopy = new List<ListItem>();
                    List<ListItem> listCopyAum = new List<ListItem>();
                    List<ListItem> listCopyTum = new List<ListItem>();

                    foreach (ListItem item in ddlprovince.Items)
                        listCopy.Add(item);
                    foreach (ListItem item in txtAumper.Items)
                        listCopyAum.Add(item);
                    foreach (ListItem item in txtTumbon.Items)
                        listCopyTum.Add(item);

                    ddlprovince.Items.Clear();
                    houseRegistrationProvince.Items.Clear();
                    bornFromProvince.Items.Clear();
                    oldSchoolProvince.Items.Clear();
                    famProvince.Items.Clear();
                    fatherProvince.Items.Clear();
                    motherProvince.Items.Clear();

                    txtAumper.Items.Clear();
                    houseRegistrationAumpher.Items.Clear();
                    bornFromAumpher.Items.Clear();
                    oldSchoolAumpher.Items.Clear();
                    famaumpher.Items.Clear();
                    fatherAumpher.Items.Clear();
                    motherAumpher.Items.Clear();

                    txtTumbon.Items.Clear();
                    houseRegistrationTumbon.Items.Clear();
                    bornFromTumbon.Items.Clear();
                    oldSchoolTumbon.Items.Clear();
                    famTumbon.Items.Clear();
                    fatherTumbon.Items.Clear();
                    motherTumbon.Items.Clear();

                    foreach (ListItem item in listCopy.OrderBy(item => item.Value))
                    {
                        ddlprovince.Items.Add(item);
                        houseRegistrationProvince.Items.Add(item);
                        bornFromProvince.Items.Add(item);
                        oldSchoolProvince.Items.Add(item);
                        famProvince.Items.Add(item);
                        fatherProvince.Items.Add(item);
                        motherProvince.Items.Add(item);
                    }

                    foreach (ListItem item in listCopyAum.OrderBy(item => item.Value))
                    {
                        txtAumper.Items.Add(item);
                        houseRegistrationAumpher.Items.Add(item);
                        bornFromAumpher.Items.Add(item);
                        oldSchoolAumpher.Items.Add(item);
                        famaumpher.Items.Add(item);
                        fatherAumpher.Items.Add(item);
                        motherAumpher.Items.Add(item);
                    }

                    foreach (ListItem item in listCopyTum.OrderBy(item => item.Value))
                    {
                        txtTumbon.Items.Add(item);
                        houseRegistrationTumbon.Items.Add(item);
                        bornFromTumbon.Items.Add(item);
                        oldSchoolTumbon.Items.Add(item);
                        famTumbon.Items.Add(item);
                        fatherTumbon.Items.Add(item);
                        motherTumbon.Items.Add(item);
                    }

                    ddlprovince.SelectedIndex = 0;
                    houseRegistrationProvince.SelectedIndex = 0;
                    bornFromProvince.SelectedIndex = 0;
                    oldSchoolProvince.SelectedIndex = 0;
                    famProvince.SelectedIndex = 0;
                    fatherProvince.SelectedIndex = 0;
                    motherProvince.SelectedIndex = 0;

                    txtAumper.SelectedIndex = 0;
                    houseRegistrationAumpher.SelectedIndex = 0;
                    bornFromAumpher.SelectedIndex = 0;
                    oldSchoolAumpher.SelectedIndex = 0;
                    famaumpher.SelectedIndex = 0;
                    fatherAumpher.SelectedIndex = 0;
                    motherAumpher.SelectedIndex = 0;

                    txtTumbon.SelectedIndex = 0;
                    houseRegistrationTumbon.SelectedIndex = 0;
                    bornFromTumbon.SelectedIndex = 0;
                    oldSchoolTumbon.SelectedIndex = 0;
                    famTumbon.SelectedIndex = 0;
                    fatherTumbon.SelectedIndex = 0;
                    motherTumbon.SelectedIndex = 0;

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
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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

                // Set the default selected item, if desired.
                optionLevel.SelectedIndex = 0;
            }
        }

        public void LoadStudentPlan(string studentType, string classLevel)
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT DISTINCT p.RegisterPlanSetupID 'ID', p.PlanName
FROM TRegisterSetup r LEFT JOIN TRegisterPlanSetup p ON r.RegisterPlanSetupID = p.RegisterPlanSetupID AND r.nTSubLevel = p.nTSubLevel
WHERE r.SchoolID = {0} AND r.StudentType = '{1}' AND r.nTSubLevel = {2} AND r.cDel = 0 AND p.RegisterPlanSetupID IS NOT NULL", schoolID, studentType, classLevel);
                List<EntityPlanSetupEduProgram> result = en.Database.SqlQuery<EntityPlanSetupEduProgram>(sqlQuery).ToList();

                ddlPlan.DataSource = result;
                ddlPlan.DataTextField = "PlanName";
                ddlPlan.DataValueField = "ID";
                ddlPlan.DataBind();

                // Set the default selected item, if desired.
                if (result.Count > 0) ddlPlan.SelectedIndex = 0;
            }
        }

        public void LoadFriendClass()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
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
                

                string birthDate = DropDownList1.SelectedValue;
                string birthMonth = DropDownList2.SelectedValue;
                string birthYear = ddlAge.SelectedValue;
                string combinedate = birthDate + "/" + birthMonth + "/" + birthYear;
                string sex = "";
                if (RadioButtonList8.SelectedValue == "ชาย") sex = "0";
                else sex = "1";
                DateTime dt1 = DateTime.ParseExact(combinedate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                int? sonddl = Int32.Parse(DropDownList5.SelectedValue);
                if (sonddl == 0)
                    sonddl = null;
                int? totalson = Int32.Parse(nSonTotal.SelectedValue);
                if (totalson == 0)
                    totalson = null;
                int? relativeStudyHere = Int32.Parse(nRelativeStudyHere.SelectedValue);
                if (relativeStudyHere == -1)
                    relativeStudyHere = null;


                DateTime? _dBirth;
                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                string combinedateFamily = dFamilyBirthDayDD.SelectedValue + "/" + dFamilyBirthDayMM.SelectedValue + "/" + dFamilyBirthDayYY.SelectedValue;
                DateTime? _dBirthFamily = null;
                if (dFamilyBirthDayDD.SelectedValue != "" && dFamilyBirthDayMM.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }

                string combinedateFather = dFatherBirthDayDD.SelectedValue + "/" + dFatherBirthDayMM.SelectedValue + "/" + dFatherBirthDayYY.SelectedValue;
                DateTime? _dBirthFather = null;
                if (dFatherBirthDayDD.SelectedValue != "" && dFatherBirthDayMM.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }

                string combinedateMother = dMotherBirthDayDD.SelectedValue + "/" + dMotherBirthDayMM.SelectedValue + "/" + dMotherBirthDayYY.SelectedValue;
                DateTime? _dBirthMother = null;
                if (dMotherBirthDayDD.SelectedValue != "" && dMotherBirthDayMM.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }


                int StudentTitle = int.Parse(DropDownList4.SelectedValue);
                int stayWithTitle2 = int.Parse(stayWithTitle.SelectedValue);

                //bool raceThai = RadioButton1.Checked;
                //bool nationThai = RadioButton13.Checked;
                //bool Buddist = RadioButton15.Checked;
                //bool Christ = RadioButton17.Checked;
                //bool Islam = RadioButton18.Checked;

                string sStudentRace = ddlRace.SelectedValue;
                //if (raceThai == true)
                //    sStudentRace = "ไทย";
                //else sStudentRace = TextBox3.Text;

                string sStudentNation = ddlNation.SelectedValue;
                //if (nationThai == true)
                //    sStudentNation = "ไทย";
                //else sStudentNation = TextBox8.Text;

                string sStudentReligion = ddlReligion.SelectedValue;
                //if (Buddist == true)
                //    sStudentReligion = "พุทธ";
                //else if (Christ == true)
                //    sStudentReligion = "คริสต์";
                //else if (Islam == true)
                //    sStudentReligion = "อิสลาม";
                //else sStudentReligion = TextBox10.Text;

                double n;
                double? oldGPA = null;
                bool isNumeric = double.TryParse(oldSchoolGPA.Text, out n);
                if (isNumeric == true)
                {
                    oldGPA = n;
                }

                string FatherR = ddlFatherRace.SelectedValue;
                string FatherN = ddlFatherNation.SelectedValue;
                string FatherRLG = ddlFatherReligion.SelectedValue;
                //bool FatherRaceThai = fatherChurChadddl1.Checked;
                //bool FatherNationThai = fatherSunChadddl1.Checked;
                //bool FatherBuddist = fatherReligion1.Checked;
                //bool FatherChrist = fatherReligion2.Checked;
                //bool FatherIslam = fatherReligion3.Checked;
                //if (FatherRaceThai == true)
                //    FatherR = "ไทย";
                //else FatherR = fatherChurChad.Text;
                //if (FatherNationThai == true)
                //    FatherN = "ไทย";
                //else FatherN = fatherSunChad.Text;
                //if (FatherBuddist == true)
                //    FatherRLG = "พุทธ";
                //else if (FatherChrist == true)
                //    FatherRLG = "คริสต์";
                //else if (FatherIslam == true)
                //    FatherRLG = "อิสลาม";
                //else FatherRLG = fatherReligiontxt.Text;

                string MotherR = ddlMotherRace.SelectedValue;
                string MotherN = ddlMotherNation.SelectedValue;
                string MotherRLG = ddlMotherReligion.SelectedValue;
                //bool MotherRaceThai = motherChurChad1.Checked;
                //bool MotherNationThai = motherSunChad1.Checked;
                //bool MotherBuddist = motherReligion1.Checked;
                //bool MotherChrist = motherReligion2.Checked;
                //bool MotherIslam = motherReligion3.Checked;
                //if (MotherRaceThai == true)
                //    MotherR = "ไทย";
                //else MotherR = motherChurChadtxt.Text;
                //if (MotherNationThai == true)
                //    MotherN = "ไทย";
                //else MotherN = motherSunChadtxt.Text;
                //if (MotherBuddist == true)
                //    MotherRLG = "พุทธ";
                //else if (MotherChrist == true)
                //    MotherRLG = "คริสต์";
                //else if (MotherIslam == true)
                //    MotherRLG = "อิสลาม";
                //else MotherRLG = motherReligiontxt.Text;

                string GuardianR = ddlFamRace.SelectedValue;
                string GuardianN = ddlFamNation.SelectedValue;
                string GuardianRLG = ddlFamReligion.SelectedValue;
                //bool GuardianRaceThai = GuardianChurChad1.Checked;
                //bool GuardianNationThai = GuardianSunChad1.Checked;
                //bool GuardianBuddist = GuardianReligion1.Checked;
                //bool GuardianChrist = GuardianReligion2.Checked;
                //bool GuardianIslam = GuardianReligion3.Checked;
                //if (GuardianRaceThai == true)
                //    GuardianR = "ไทย";
                //else GuardianR = GuardianChurChadtxt.Text;
                //if (GuardianNationThai == true)
                //    GuardianN = "ไทย";
                //else GuardianN = GuardianSunChadtxt.Text;
                //if (GuardianBuddist == true)
                //    GuardianRLG = "พุทธ";
                //else if (GuardianChrist == true)
                //    GuardianRLG = "คริสต์";
                //else if (GuardianIslam == true)
                //    GuardianRLG = "อิสลาม";
                //else GuardianRLG = GuardianReligiontxt.Text;


                string homenumFather = fatherHome.Text;
                string soyFather = fatherSoy.Text;
                string muuFather = fatherMuu.Text;
                string roadFather = fatherRoad.Text;
                string provinceFather = fatherProvince.SelectedValue;
                string aumpherFather = fatherAumpher.SelectedValue;
                string tumbonFather = fatherTumbon.SelectedValue;
                string postFather = fatherPost.Text;

                string homenumMother = fatherHome.Text;
                string soyMother = motherSoy.Text;
                string muuMother = motherMuu.Text;
                string roadMother = motherRoad.Text;
                string provinceMother = motherProvince.SelectedValue;
                string aumpherMother = motherAumpher.SelectedValue;
                string tumbonMother = motherTumbon.SelectedValue;
                string postMother = motherPost.Text;

                string provinceHouseHold = houseRegistrationProvince.SelectedValue;
                string aumpherHouseHold = houseRegistrationAumpher.SelectedValue;
                string tumbonHouseHold = houseRegistrationTumbon.SelectedValue;
                string postHouseHold = txtPost2.Text;

                if (customCheck1.Checked == true)
                {
                    homenumFather = famHomenum.Text;
                    soyFather = famSoy.Text;
                    muuFather = famMuu.Text;
                    roadFather = famRoad.Text;
                    provinceFather = famProvince.SelectedValue;
                    aumpherFather = famaumpher.SelectedValue;
                    tumbonFather = famTumbon.SelectedValue;
                    postFather = famPost.Text;
                }

                if (customCheck2.Checked == true)
                {
                    homenumMother = famHomenum.Text;
                    soyMother = famSoy.Text;
                    muuMother = famMuu.Text;
                    roadMother = famRoad.Text;
                    provinceMother = famProvince.SelectedValue;
                    aumpherMother = famaumpher.SelectedValue;
                    tumbonMother = famTumbon.SelectedValue;
                    postMother = famPost.Text;
                }
                string oAumpher = "";
                double? oGPA = null;
                string oGraduated = "";
                string oName = "";
                string oProvince = "";
                string oTumbon = "";
                string oWhyMove = "";

                if (Checkbox12.Checked != true)
                {
                    if (oldSchoolAumpher.SelectedValue != "0")
                        oAumpher = oldSchoolAumpher.SelectedValue;
                    oGPA = oldGPA;
                    oGraduated = oldSchoolGraduated.SelectedValue;
                    oName = oldSchoolName.Text;
                    if (oldSchoolProvince.SelectedValue != "0")
                        oProvince = oldSchoolProvince.SelectedValue;
                    if (oldSchoolTumbon.SelectedValue != "0")
                        oTumbon = oldSchoolTumbon.SelectedValue;
                    oWhyMove = moveOutReason.Text;
                }

                string userPro = ddlprovince.SelectedValue;
                string userAum = txtAumper.SelectedValue;
                string userTum = txtTumbon.SelectedValue;

                string famPro = famProvince.SelectedValue;
                string famAum = famaumpher.SelectedValue;
                string famTum = famTumbon.SelectedValue;
                string fatherPro = provinceFather;
                string fatherAum = aumpherFather;
                string fatherTum = tumbonFather;
                string motherPro = provinceMother;
                string motherAum = aumpherMother;
                string motherTum = tumbonMother;



                if (userPro == "0")
                    userPro = "";
                if (userAum == "0")
                    userAum = "";
                if (userTum == "0")
                    userTum = "";

                if (famPro == "0")
                    famPro = "";
                if (famAum == "0")
                    famAum = "";
                if (famTum == "0")
                    famTum = "";
                if (fatherPro == "0")
                    fatherPro = "";
                if (fatherAum == "0")
                    fatherAum = "";
                if (fatherTum == "0")
                    fatherTum = "";
                if (motherPro == "0")
                    motherPro = "";
                if (motherAum == "0")
                    motherAum = "";
                if (motherTum == "0")
                    motherTum = "";

                double n2;
                double? Weight = null;
                bool isNumeric2 = double.TryParse(TextBox42.Text, out n2);
                if (isNumeric2 == true)
                {
                    Weight = n2;
                }

                double n3;
                double? Height = null;
                bool isNumeric3 = double.TryParse(TextBox43.Text, out n3);
                if (isNumeric3 == true)
                {
                    Height = n3;
                }

                string sickfood = "";
                bool sickFood1 = RadioButton3.Checked;
                if (sickFood1 == true)
                    sickfood = "ไม่แพ้";
                else sickfood = TextBox54.Text;

                string sickdrug = "";
                bool sickDrug1 = RadioButton5.Checked;
                if (sickDrug1 == true)
                    sickdrug = "ไม่แพ้";
                else sickdrug = TextBox17.Text;

                string sickother = "";
                bool sickOther1 = RadioButton7.Checked;
                if (sickOther1 == true)
                    sickother = "ไม่แพ้";
                else sickother = TextBox36.Text;

                string sicknormal = "";
                bool sickNormal1 = RadioButton9.Checked;
                if (sickNormal1 == true)
                    sicknormal = "ไม่มีโรค";
                else sicknormal = TextBox41.Text;

                string sickdanger = "";
                bool sickDanger1 = RadioButton11.Checked;
                if (sickDanger1 == true)
                    sickdanger = "ไม่มีโรค";
                else sickdanger = TextBox44.Text;

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
                    opTime = int.Parse(optionTime.SelectedValue);
                    opbrach = int.Parse(optionBranch1.SelectedValue);

                }
                else if (levelObj.LevelName == "ปวส.")
                {
                    opTime = int.Parse(optionTime.SelectedValue);
                    opbrach = int.Parse(optionBranch2.SelectedValue);
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

                var yearRunID = _db.TPreRegisters.Where(o => o.SchoolID == schoolID && o.registerYear == registeryear).Count();
                yearRunID += 1;

                string studentID = "";
                if (!string.IsNullOrEmpty(sStudentidtxt.Text))
                {
                    studentID = sStudentidtxt.Text.Trim();
                }
                else
                {
                    // Auto Generate StudentID
                    studentID = thisyear.ToString().Remove(0, 2) + yearRunID.ToString("0000");
                }

                double lat;
                double? lat2 = null;
                bool isNumericlat = double.TryParse(SendA.Value, out lat);
                if (isNumericlat == true)
                {
                    lat2 = lat;
                }
                double lng;
                double? lng2 = null;
                bool isNumericlng = double.TryParse(SendB.Value, out lng);
                if (isNumericlng == true)
                {
                    lng2 = lng;
                }

                int? box1 = 0;
                int? box2 = 0;
                int? box3 = 0;
                int? box4 = 0;
                int? box5 = 0;
                int? box6 = 0;
                int? box7 = 0;
                int? box8 = 0;
                int? box9 = 0;
                int? box10 = 0;
                int? box11 = 0;
                if (CheckBox1.Checked == true) box1 = 1;
                if (CheckBox2.Checked == true) box2 = 1;
                if (CheckBox3.Checked == true) box3 = 1;
                if (CheckBox4.Checked == true) box4 = 1;
                if (CheckBox5.Checked == true) box5 = 1;
                if (CheckBox6.Checked == true) box6 = 1;
                if (CheckBox7.Checked == true) box7 = 1;
                if (CheckBox8.Checked == true) box8 = 1;
                if (CheckBox9.Checked == true) box9 = 1;
                if (CheckBox10.Checked == true) box10 = 1;
                if (CheckBox11.Checked == true) box11 = 1;


                int? hometype2 = null;
                if (HomeType.SelectedValue != "0")
                    hometype2 = int.Parse(HomeType.SelectedValue);

                int? familyRequestStudyMonney = null;
                if (nFamilyRequestStudyMoney.SelectedValue != "-1")
                    familyRequestStudyMonney = int.Parse(nFamilyRequestStudyMoney.SelectedValue);

                int? province2 = null;
                if (houseRegistrationProvince.SelectedValue != "0")
                    province2 = int.Parse(houseRegistrationProvince.SelectedValue);

                int? aumpher2 = null;
                if (houseRegistrationAumpher.SelectedValue != "0")
                    aumpher2 = int.Parse(houseRegistrationAumpher.SelectedValue);

                int? tumbon2 = null;
                if (houseRegistrationTumbon.SelectedValue != "0")
                    tumbon2 = int.Parse(houseRegistrationTumbon.SelectedValue);

                int? province3 = null;
                if (bornFromProvince.SelectedValue != "0")
                    province3 = int.Parse(bornFromProvince.SelectedValue);

                int? aumpher3 = null;
                if (bornFromAumpher.SelectedValue != "0")
                    aumpher3 = int.Parse(bornFromAumpher.SelectedValue);

                int? tumbon3 = null;
                if (bornFromTumbon.SelectedValue != "0")
                    tumbon3 = int.Parse(bornFromTumbon.SelectedValue);


                double? famincome = null;
                double? faincome = null;
                double? moincome = null;
                if (nFamilyIncome.Text != "")
                {
                    famincome = double.Parse(nFamilyIncome.Text);
                }
                if (nFatherIncome.Text != "")
                {
                    faincome = double.Parse(nFatherIncome.Text);
                }
                if (nMotherIncome.Text != "")
                {
                    moincome = double.Parse(nMotherIncome.Text);
                }

                int? planID = (ddlPlan.SelectedValue == "" ? (int?)null : Convert.ToInt32(ddlPlan.SelectedValue)); // ###

                // ###
                //int? levelID = Convert.ToInt32(nTLevel.Text);

                var subLevel = _db.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == opLevel).FirstOrDefault();
                string levelName = _db.TLevels.Where(w => w.SchoolID == schoolID && w.LevelID == subLevel.nTLevel).FirstOrDefault().LevelName;

                int classRunID = _db.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registeryear && w.optionLevel == opLevel && w.RegisterPlanSetupID == planID).Count() + 1;
                var examCode = ComFunction.GenerateExamCode2(thisyear, levelName, subLevel.SubLevel, (int)planID, classRunID); // [year{2}]-[levelID][subLevelID]-[planID]-[runID] = 63-11-01-001

                TPreRegister preRegister = new TPreRegister
                {
                    //preRegisterId = nID,
                    sFamilyAumpher = famAum,
                    sFamilyHomeNumber = famHomenum.Text,
                    sFamilyIdCardNumber = famIdCard.Text,
                    sFamilyLast = famLast.Text,
                    sFamilyMuu = famMuu.Text,
                    sFamilyName = famName.Text,
                    sFamilyNameEN = sFamilyNameEN.Text,
                    sFamilyLastEN = sFamilyLastEN.Text,
                    dFamilyBirthDay = _dBirthFamily,
                    nFamilyRequestStudyMoney = familyRequestStudyMonney,
                    sFamilyGraduated = int.Parse(sFamilyGraduated.SelectedValue),
                    sFamilyJob = sFamilyJob.Text,
                    sFamilyWorkPlace = sFamilyWorkPlace.Text,
                    nFamilyIncome = famincome,
                    ParentAnnualIncome = (ddlParentYearIncome.SelectedValue == "" ? null : ddlParentYearIncome.SelectedValue), // ###
                    sPhoneOne = famPhone1.Text,
                    sPhoneThree = famPhone3.Text,
                    ParentEmail = txtParentEmail.Text, // ###
                    sPhoneTwo = famPhone2.Text,
                    sFamilyNation = GuardianN,
                    sFamilyPost = famPost.Text,
                    sFamilyProvince = famPro,
                    sFamilyRace = GuardianR,
                    sFamilyRelate = RadioButtonList1.SelectedValue,
                    sFamilyReligion = GuardianRLG,
                    sFamilyRoad = famRoad.Text,
                    sFamilySoy = famSoy.Text,
                    nFamilyTitle = int.Parse(famTitle.SelectedValue),
                    sFamilyTumbon = famTum,
                    sFatherAumpher = fatherAum,
                    sFatherFirstName = fatherName.Text,
                    sFatherHomeNumber = homenumFather,
                    sFatherIdCardNumber = fatherIdCard.Text,
                    sFatherLastName = fatherLast.Text,
                    sFatherMuu = muuFather,
                    sFatherNation = FatherN,

                    sFatherPost = postFather,
                    sFatherProvince = fatherPro,
                    sFatherRace = FatherR,
                    sFatherReligion = FatherRLG,
                    sFatherRoad = roadFather,
                    sFatherSoy = soyFather,
                    FatherTitle = int.Parse(fatherTitle.SelectedValue),
                    sFatherTumbon = fatherTum,
                    sFatherNameEN = sFatherNameEN.Text,
                    sFatherLastEN = sFatherLastEN.Text,
                    dFatherBirthDay = _dBirthFather,
                    sFatherGraduated = int.Parse(sFatherGraduated.SelectedValue),
                    sFatherJob = sFatherJob.Text,
                    sFatherWorkPlace = sFatherWorkPlace.Text,
                    nFatherIncome = faincome,
                    FatherAnnualIncome = (ddlFatherYearIncome.SelectedValue == "" ? null : ddlFatherYearIncome.SelectedValue), // ###
                    sFatherPhone = sFatherPhone.Text,
                    sFatherPhone2 = sFatherPhone2.Text,
                    sFatherPhone3 = sFatherPhone3.Text,
                    FatherEmail = txtFatherEmail.Text, // ###

                    cDel = null,
                    cSex = sex,
                    cType = "0",
                    dBirth = _dBirth,
                    dPicUpdate = DateTime.Now,
                    MotherTitle = int.Parse(motherTitle.SelectedValue),
                    nHeight = Height,
                    //nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0,
                    nSonNumber = sonddl,
                    nSonTotal = totalson,
                    nRelativeStudyHere = relativeStudyHere,
                    nStudentNumber = null,
                    nStudentStatus = 0,
                    nTermSubLevel2 = null,
                    nWeight = Weight,
                    oldSchoolAumpher = oAumpher,
                    oldSchoolGPA = oGPA,
                    oldSchoolGraduated = oGraduated,
                    oldSchoolName = oName,
                    oldSchoolProvince = oProvince,
                    oldSchoolTumbon = oTumbon,
                    paymentStatus = 0,
                    registerCode = yearRunID.ToString("0000"), // ###
                    registerStatus = 0,
                    registerYear = registeryear,
                    sAddress = famHomenum.Text + " " + famMuu.Text + " " + famSoy.Text + " " + famRoad.Text + " " +
                                 famTumbon.Text + " " + famaumpher.Text + " " + famProvince.Text + " " + famPost.Text,
                    sBlood = ddlBlood.SelectedValue,
                    sCity = "",
                    sCountry = "",
                    sEmail = Emailtxt.Text,
                    sIdentification = citizenID.Text,
                    sLastname = sLastTH.Text,
                    sMotherAumpher = motherAum,
                    sMotherFirstName = motherName.Text,
                    sMotherHomeNumber = homenumMother,
                    sMotherIdCardNumber = motherIdCard.Text,
                    sMotherLastName = motherLast.Text,
                    sMotherMuu = muuMother,
                    sMotherNation = MotherN,
                    sMotherPost = postMother,
                    sMotherProvince = motherPro,
                    sMotherRace = MotherR,
                    sMotherReligion = MotherRLG,
                    sMotherRoad = roadMother,
                    sMotherSoy = soyMother,
                    sMotherTumbon = motherTum,
                    sMotherNameEN = sMotherNameEN.Text,
                    sMotherLastEN = sMotherLastEN.Text,
                    dMotherBirthDay = _dBirthMother,
                    sMotherGraduated = int.Parse(sMotherGraduated.SelectedValue),
                    sMotherJob = sMotherJob.Text,
                    sMotherWorkPlace = sMotherWorkPlace.Text,
                    nMotherIncome = moincome,
                    MotherAnnualIncome = (ddlMotherYearIncome.SelectedValue == "" ? null : ddlMotherYearIncome.SelectedValue), // ###
                    sMotherPhone = sMotherPhone.Text,
                    sMotherPhone2 = sMotherPhone2.Text,
                    sMotherPhone3 = sMotherPhone3.Text,
                    MotherEmail = txtMotherEmail.Text, // ###
                    sName = sNameTH.Text,
                    sNickName = sNick.Text,
                    sNickNameEN = sNickNameEN.Text,

                    sPhone = sPhone.Text,

                    sPostalcode = "",
                    sSickDanger = sickdanger,
                    sSickDrug = sickdrug,
                    sSickFood = sickfood,
                    sSickNormal = sicknormal,
                    sSickOther = sickother,
                    sStudentAumpher = userAum,
                    sStudentHomeNumber = stdHomenum.Text,
                    sStudentHomeRegisterCode = sStudentHomeRegisterCode.Text,
                    //sStudentID = str,
                    sStudentID = studentID,  // ###
                    sStudentIdCardNumber = citizenID.Text,
                    sStudentLastEN = sLastEN.Text,
                    sStudentMuu = stdMuu.Text,
                    sStudentNameEN = sNameEN.Text,
                    sStudentNation = sStudentNation,
                    //sStudentPicture = link,
                    sStudentPost = txtPost.Text,
                    sStudentProvince = userPro,
                    sStudentRace = sStudentRace,
                    sStudentReligion = sStudentReligion,
                    sStudentRoad = stdRoad.Text,
                    sStudentSoy = stdSoy.Text,
                    sStudentHousePhone = sStudentHousePhone.Text,
                    sStudentTumbon = userTum,
                    StudentTitle = StudentTitle,
                    stayWithTitle = stayWithTitle2,
                    stayWithName = stayWithName.Text,
                    stayWithLast = stayWithLast.Text,
                    stayWithEmergencyCall = stayWithEmergencyCall.Text,
                    stayWithEmail = stayWithEmail.Text,
                    HomeType = hometype2,
                    friendLastName = friendLast.Text,
                    friendName = friendName.Text,
                    friendPhone = friendPhone.Text,
                    friendSubLevel = int.Parse(friendSublevel.SelectedValue),
                    houseRegistrationNumber = houseRegistrationNumber.Text,
                    houseRegistrationMuu = houseRegistrationMuu.Text,
                    houseRegistrationSoy = houseRegistrationSoy.Text,
                    houseRegistrationRoad = houseRegistrationRoad.Text,
                    houseRegistrationProvince = province2,
                    houseRegistrationAumpher = aumpher2,
                    houseRegistrationTumbon = tumbon2,
                    houseRegistrationPost = txtPost2.Text,
                    houseRegistrationPhone = houseRegistrationPhone.Text,
                    bornFrom = bornFrom.Text,
                    bornFromProvince = province3,
                    bornFromAumpher = aumpher3,
                    bornFromTumbon = tumbon3,
                    moveOutReason = oWhyMove,
                    optionBranch = opbrach,
                    StudentCategory = rdlStudentCategory.SelectedValue, // ###
                    optionCourse = opCourse,
                    StudentType = (ddlStudentType.SelectedValue == "" ? null : ddlStudentType.SelectedValue), // ###
                    optionLevel = opLevel,
                    RegisterPlanSetupID = planID, // ###
                    ExamCode = examCode, // ###
                    optionTime = opTime,
                    addressLat = lat2,
                    addressLng = lng2,

                    knowFrom1 = box1,
                    knowFrom2 = box2,
                    knowFrom3 = box3,
                    knowFrom4 = box4,
                    knowFrom5 = box5,
                    knowFrom6 = box6,
                    knowFrom7 = box7,
                    knowFrom8 = box8,
                    knowFrom9 = box9,
                    knowFrom10 = box10,
                    knowFrom11 = box11,
                    addDate = DateTime.Now,
                    familyStatus = int.Parse(familyStatus.SelectedValue),
                    sStudentLastOther = sStudentLastOther.Text,
                    sStudentNameOther = sStudentNameOther.Text,
                    SchoolID = schoolID,
                    CreatedBy = UserData.UserID,
                    CreatedDate = DateTime.Now
                };
                _db.TPreRegisters.Add(preRegister);

                _db.SaveChanges();

                var preRegisterID = preRegister.preRegisterId;

                string link = "";
                if (FileUpload1.HasFile)
                {
                    link = preRegisterPic(FileUpload1, 150, preRegisterID);
                }

                preRegister.nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0;
                preRegister.sStudentPicture = link;
                _db.SaveChanges();

                int registerYearBE = preRegister.registerYear + 543;

                //TRegisterSetup registerSetup = _db.TRegisterSetups.FirstOrDefault(f => f.RegisterPlanSetupID == preRegister.RegisterPlanSetupID && f.Year == registerYearBE && f.nTSubLevel == preRegister.optionLevel);
                //if (registerSetup != null && registerSetup.PaymentGroupID != null)
                //{
                //bool statusSaveInvoice = Accounting.Tuitionfee.Setting.SaveInvoiceData(preRegister);
                //    if (!statusSaveInvoice)
                //    {
                //        Response.Write("<script>alert('บันทึกใบแจ้งหนี้ไม่สำเร็จ');</script>");
                //        return;
                //    }
                //}


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
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument021.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument021.PostedFile.FileName);
                    contentType = FileDocument021.PostedFile.ContentType;

                    Stream fs = FileDocument021.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 1; vfiID = 3;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument022.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument022.PostedFile.FileName);
                    contentType = FileDocument022.PostedFile.ContentType;

                    Stream fs = FileDocument022.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 2; vfiID = 4;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument023.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument023.PostedFile.FileName);
                    contentType = FileDocument023.PostedFile.ContentType;

                    Stream fs = FileDocument023.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 2; typeID = 3; vfiID = 5;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument031.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument031.PostedFile.FileName);
                    contentType = FileDocument031.PostedFile.ContentType;

                    Stream fs = FileDocument031.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 3; typeID = 1; vfiID = 7;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument032.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument032.PostedFile.FileName);
                    contentType = FileDocument032.PostedFile.ContentType;

                    Stream fs = FileDocument032.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 3; typeID = 2; vfiID = 8;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument04.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument04.PostedFile.FileName);
                    contentType = FileDocument04.PostedFile.ContentType;

                    Stream fs = FileDocument04.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 4; typeID = 1; vfiID = 9;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument051.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument051.PostedFile.FileName);
                    contentType = FileDocument051.PostedFile.ContentType;

                    Stream fs = FileDocument051.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 1; vfiID = 11;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument052.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument052.PostedFile.FileName);
                    contentType = FileDocument052.PostedFile.ContentType;

                    Stream fs = FileDocument052.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 2; vfiID = 12;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument053.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument053.PostedFile.FileName);
                    contentType = FileDocument053.PostedFile.ContentType;

                    Stream fs = FileDocument053.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 5; typeID = 3; vfiID = 13;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument06.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument06.PostedFile.FileName);
                    contentType = FileDocument06.PostedFile.ContentType;

                    Stream fs = FileDocument06.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 6; typeID = 1; vfiID = 14;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument07.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument07.PostedFile.FileName);
                    contentType = FileDocument07.PostedFile.ContentType;

                    Stream fs = FileDocument07.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 7; typeID = 1; vfiID = 15;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument08.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument08.PostedFile.FileName);
                    contentType = FileDocument08.PostedFile.ContentType;

                    Stream fs = FileDocument08.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 8; typeID = 1; vfiID = 16;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument09.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument09.PostedFile.FileName);
                    contentType = FileDocument09.PostedFile.ContentType;

                    Stream fs = FileDocument09.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 9; typeID = 1; vfiID = 17;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument10.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument10.PostedFile.FileName);
                    contentType = FileDocument10.PostedFile.ContentType;

                    Stream fs = FileDocument10.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 10; typeID = 1; vfiID = 18;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument11.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument11.PostedFile.FileName);
                    contentType = FileDocument11.PostedFile.ContentType;

                    Stream fs = FileDocument11.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 11; typeID = 1; vfiID = 19;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument12.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument12.PostedFile.FileName);
                    contentType = FileDocument12.PostedFile.ContentType;

                    Stream fs = FileDocument12.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 12; typeID = 1; vfiID = 166;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument13.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument13.PostedFile.FileName);
                    contentType = FileDocument13.PostedFile.ContentType;

                    Stream fs = FileDocument13.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 13; typeID = 1; vfiID = 167;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                if (FileDocument14.HasFile)
                {
                    fileName = Path.GetFileName(FileDocument14.PostedFile.FileName);
                    contentType = FileDocument14.PostedFile.ContentType;

                    Stream fs = FileDocument14.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    byteData = Convert.ToBase64String(bytes, 0, bytes.Length);

                    docID = 14; typeID = 1; vfiID = 168;
                    linkFile = ComFunction.UploadFileFromByteData(docID + "_" + typeID + "_" + fileName, byteData, "preregister/document", schoolID, preRegisterID);

                    TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                    {
                        preRegisterId = preRegisterID,
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

                _db.SaveChanges();

                Accounting.Tuitionfee.Setting.SaveInvoiceData(preRegister);

                Response.Redirect("preRegisterList2.aspx");
            }
        }

        private void OpenData()
        {
            LoadStudentClass(ddlStudentType.SelectedValue);
            if (optionLevel.SelectedValue != "")
            {
                string[] optionLevelValue = optionLevel.SelectedValue.Split('/');

                nTSubLevel.Text = Convert.ToString(optionLevelValue[0]);
                nTLevel.Text = Convert.ToString(optionLevelValue[1]);

                LoadStudentPlan(ddlStudentType.SelectedValue, nTSubLevel.Text);
            }

            int schoolID = UserData.CompanyID;
            string specialScript = "";
            if (schoolID == 1043)
            {
                specialScript = string.Format(@"$('.re-order').each(function(i){{$(this).text(parseInt($(this).text())+3);}});");
            }
            ltrScript.Text = string.Format(@"<script type=""text/javascript"" language=""javascript"">
    $(document).ready(function () {{
        {0}
    }});
</script>", specialScript);
        }

        class branchSpec
        {
            public int branchSpecId { get; set; }
            public string name { get; set; }
        }

        class idlv2ddl
        {
            public string value { get; set; }
            public string name { get; set; }
            public int sort { get; set; }
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

        protected void houseRegistrationProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(houseRegistrationProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), houseRegistrationAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(houseRegistrationAumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), houseRegistrationTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void houseRegistrationAumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(houseRegistrationAumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), houseRegistrationTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
                txtPost2.Text = qAMPHUR.POSTCODE;
            }
        }

        protected void bornFromProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int PROVINCE_ID = int.Parse(bornFromProvince.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.amphurs.Where(w => w.PROVINCE_ID == PROVINCE_ID).ToList(), bornFromAumpher, "", "AMPHUR_ID", "AMPHUR_NAME");

                int AMPHUR_CODE = int.Parse(bornFromAumpher.SelectedValue);
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), bornFromTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
            }
        }

        protected void bornFromAumpher_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int AMPHUR_CODE = int.Parse(bornFromAumpher.SelectedValue);
                var qAMPHUR = dbmaster.amphurs.Where(w => w.AMPHUR_ID == AMPHUR_CODE).FirstOrDefault();
                fcommon.LinqToDropDownList(dbmaster.districts.Where(w => w.AMPHUR_ID == AMPHUR_CODE).ToList(), bornFromTumbon, "", "DISTRICT_ID", "DISTRICT_NAME");
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