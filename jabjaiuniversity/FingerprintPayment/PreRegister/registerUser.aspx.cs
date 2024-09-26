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
using System.Security.Cryptography;
using System.Text;
using OBS;
using OBS.Model;

namespace FingerprintPayment.PreRegister
{
    public partial class registerUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = Rot13.Transform(Request.QueryString["id"]);

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using(JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                
                

                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                SqlConnection _conn = fcommon.ConfigSqlConnection(sEntities);
                Button1.Click += new EventHandler(Button1_Click);


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

                    var itemz = new ListItem
                    {
                        Text = "เลือกคำนำหน้า",
                        Value = "0"
                    };
                    DropDownList4.Items.Add(itemz);
                    DropDownList8.Items.Add(itemz);
                    famTitle.Items.Add(itemz);
                    fatherTitle.Items.Add(itemz);
                    motherTitle.Items.Add(itemz);

                    DropDownList4.SelectedValue = "0";
                    DropDownList8.SelectedValue = "0";
                    famTitle.SelectedValue = "0";
                    fatherTitle.SelectedValue = "0";
                    motherTitle.SelectedValue = "0";

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
                        DropDownList4.Items.Add(item);
                        DropDownList8.Items.Add(item);
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

                    var item3 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(543).Year.ToString(),
                        Value = DateTime.Now.Year.ToString()
                    };
                    var item4 = new ListItem
                    {
                        Text = DateTime.Now.AddYears(544).Year.ToString(),
                        Value = DateTime.Now.AddYears(1).Year.ToString(),
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
                    ddlprovince2.Items.Clear();
                    ddlprovince3.Items.Clear();
                    oldSchoolProvince.Items.Clear();
                    famProvince.Items.Clear();
                    fatherProvince.Items.Clear();
                    motherProvince.Items.Clear();

                    txtAumper.Items.Clear();
                    txtAumper2.Items.Clear();
                    txtAumper3.Items.Clear();
                    oldSchoolAumpher.Items.Clear();
                    famaumpher.Items.Clear();
                    fatherAumpher.Items.Clear();
                    motherAumpher.Items.Clear();

                    txtTumbon.Items.Clear();
                    txtTumbon2.Items.Clear();
                    txtTumbon3.Items.Clear();
                    oldSchoolTumbon.Items.Clear();
                    famTumbon.Items.Clear();
                    fatherTumbon.Items.Clear();
                    motherTumbon.Items.Clear();

                    foreach (ListItem item in listCopy.OrderBy(item => item.Value))
                    {
                        ddlprovince.Items.Add(item);
                        ddlprovince2.Items.Add(item);
                        ddlprovince3.Items.Add(item);
                        oldSchoolProvince.Items.Add(item);
                        famProvince.Items.Add(item);
                        fatherProvince.Items.Add(item);
                        motherProvince.Items.Add(item);
                    }

                    foreach (ListItem item in listCopyAum.OrderBy(item => item.Value))
                    {
                        txtAumper.Items.Add(item);
                        txtAumper2.Items.Add(item);
                        txtAumper3.Items.Add(item);
                        oldSchoolAumpher.Items.Add(item);
                        famaumpher.Items.Add(item);
                        fatherAumpher.Items.Add(item);
                        motherAumpher.Items.Add(item);
                    }

                    foreach (ListItem item in listCopyTum.OrderBy(item => item.Value))
                    {
                        txtTumbon.Items.Add(item);
                        txtTumbon2.Items.Add(item);
                        txtTumbon3.Items.Add(item);
                        oldSchoolTumbon.Items.Add(item);
                        famTumbon.Items.Add(item);
                        fatherTumbon.Items.Add(item);
                        motherTumbon.Items.Add(item);
                    }

                    ddlprovince.SelectedIndex = 0;
                    ddlprovince2.SelectedIndex = 0;
                    ddlprovince3.SelectedIndex = 0;
                    oldSchoolProvince.SelectedIndex = 0;
                    famProvince.SelectedIndex = 0;
                    fatherProvince.SelectedIndex = 0;
                    motherProvince.SelectedIndex = 0;

                    txtAumper.SelectedIndex = 0;
                    txtAumper2.SelectedIndex = 0;
                    txtAumper3.SelectedIndex = 0;
                    oldSchoolAumpher.SelectedIndex = 0;
                    famaumpher.SelectedIndex = 0;
                    fatherAumpher.SelectedIndex = 0;
                    motherAumpher.SelectedIndex = 0;

                    txtTumbon.SelectedIndex = 0;
                    txtTumbon2.SelectedIndex = 0;
                    txtTumbon3.SelectedIndex = 0;
                    oldSchoolTumbon.SelectedIndex = 0;
                    famTumbon.SelectedIndex = 0;
                    fatherTumbon.SelectedIndex = 0;
                    motherTumbon.SelectedIndex = 0;
                    OpenData();
                }
            }

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


        protected void Upload(object sender, EventArgs e)
        {

        }

        void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities2 = Request.QueryString["id"];
            string sEntities = Rot13.Transform(Request.QueryString["id"]);

          

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

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {


                int nID = _db.TPreRegisters.Count() == 0 ? 0 : _db.TPreRegisters.Max(max => max.preRegisterId);
                nID = nID + 1;
                string link = "";
                int sID = 1;
                if (_dbMaster.TUsers.ToList().Count > 0) sID = _dbMaster.TUsers.Max(M => M.sID) + 1;
                if (FileUpload1.HasFile)
                {
                    link = preRegisterPic(FileUpload1, 150, nID);
                }


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
                int? totalson = Int32.Parse(DropDownList3.SelectedValue);
                if (totalson == 0)
                    totalson = null;
                int? relativeStudyHere = Int32.Parse(DropDownList6.SelectedValue);
                if (relativeStudyHere == -1)
                    relativeStudyHere = null;


                DateTime? _dBirth;
                if (DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    _dBirth = DateTime.ParseExact(combinedate, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                string combinedateFamily = DropDownList15.SelectedValue + "/" + DropDownList16.SelectedValue + "/" + DropDownList17.SelectedValue;
                DateTime? _dBirthFamily = null;
                if (DropDownList15.SelectedValue != "" && DropDownList16.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFamily = DateTime.ParseExact(combinedateFamily, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }

                string combinedateFather = DropDownList19.SelectedValue + "/" + DropDownList20.SelectedValue + "/" + DropDownList21.SelectedValue;
                DateTime? _dBirthFather = null;
                if (DropDownList19.SelectedValue != "" && DropDownList20.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthFather = DateTime.ParseExact(combinedateFather, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }

                string combinedateMother = DropDownList24.SelectedValue + "/" + DropDownList25.SelectedValue + "/" + DropDownList26.SelectedValue;
                DateTime? _dBirthMother = null;
                if (DropDownList24.SelectedValue != "" && DropDownList25.SelectedValue != "")
                {
                    if (DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us"));
                    else
                        _dBirthMother = DateTime.ParseExact(combinedateMother, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);
                }


                int StudentTitle = int.Parse(DropDownList4.SelectedValue);
                int stayWithTitle = int.Parse(DropDownList8.SelectedValue);

                bool raceThai = RadioButton1.Checked;
                bool nationThai = RadioButton13.Checked;
                bool Buddist = RadioButton15.Checked;
                bool Christ = RadioButton17.Checked;
                bool Islam = RadioButton18.Checked;

                string sStudentRace = "";
                if (raceThai == true)
                    sStudentRace = "ไทย";
                else sStudentRace = TextBox3.Text;

                string sStudentNation = "";
                if (nationThai == true)
                    sStudentNation = "ไทย";
                else sStudentNation = TextBox8.Text;

                string sStudentReligion = "";
                if (Buddist == true)
                    sStudentReligion = "พุทธ";
                else if (Christ == true)
                    sStudentReligion = "คริสต์";
                else if (Islam == true)
                    sStudentReligion = "อิสลาม";
                else sStudentReligion = TextBox10.Text;

                double n;
                double? oldGPA = null;
                bool isNumeric = double.TryParse(oldSchoolGPA.Text, out n);
                if (isNumeric == true)
                {
                    oldGPA = n;
                }

                string FatherR = "";
                string FatherN = "";
                string FatherRLG = "";
                bool FatherRaceThai = fatherChurChadddl1.Checked;
                bool FatherNationThai = fatherSunChadddl1.Checked;
                bool FatherBuddist = fatherReligion1.Checked;
                bool FatherChrist = fatherReligion2.Checked;
                bool FatherIslam = fatherReligion3.Checked;
                if (FatherRaceThai == true)
                    FatherR = "ไทย";
                else FatherR = fatherChurChad.Text;
                if (FatherNationThai == true)
                    FatherN = "ไทย";
                else FatherN = fatherSunChad.Text;
                if (FatherBuddist == true)
                    FatherRLG = "พุทธ";
                else if (FatherChrist == true)
                    FatherRLG = "คริสต์";
                else if (FatherIslam == true)
                    FatherRLG = "อิสลาม";
                else FatherRLG = fatherReligiontxt.Text;

                string MotherR = "";
                string MotherN = "";
                string MotherRLG = "";
                bool MotherRaceThai = motherChurChad1.Checked;
                bool MotherNationThai = motherSunChad1.Checked;
                bool MotherBuddist = motherReligion1.Checked;
                bool MotherChrist = motherReligion2.Checked;
                bool MotherIslam = motherReligion3.Checked;
                if (MotherRaceThai == true)
                    MotherR = "ไทย";
                else MotherR = motherChurChadtxt.Text;
                if (MotherNationThai == true)
                    MotherN = "ไทย";
                else MotherN = motherSunChadtxt.Text;
                if (MotherBuddist == true)
                    MotherRLG = "พุทธ";
                else if (MotherChrist == true)
                    MotherRLG = "คริสต์";
                else if (MotherIslam == true)
                    MotherRLG = "อิสลาม";
                else MotherRLG = motherReligiontxt.Text;

                string GuardianR = "";
                string GuardianN = "";
                string GuardianRLG = "";
                bool GuardianRaceThai = GuardianChurChad1.Checked;
                bool GuardianNationThai = GuardianSunChad1.Checked;
                bool GuardianBuddist = GuardianReligion1.Checked;
                bool GuardianChrist = GuardianReligion2.Checked;
                bool GuardianIslam = GuardianReligion3.Checked;
                if (GuardianRaceThai == true)
                    GuardianR = "ไทย";
                else GuardianR = GuardianChurChadtxt.Text;
                if (GuardianNationThai == true)
                    GuardianN = "ไทย";
                else GuardianN = GuardianSunChadtxt.Text;
                if (GuardianBuddist == true)
                    GuardianRLG = "พุทธ";
                else if (GuardianChrist == true)
                    GuardianRLG = "คริสต์";
                else if (GuardianIslam == true)
                    GuardianRLG = "อิสลาม";
                else GuardianRLG = GuardianReligiontxt.Text;


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

                string provinceHouseHold = ddlprovince2.SelectedValue;
                string aumpherHouseHold = txtAumper2.SelectedValue;
                string tumbonHouseHold = txtTumbon2.SelectedValue;
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
                    oWhyMove = TextBox23.Text;
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

                var count = _db.TPreRegisters.Where(o => o.registerYear == registeryear).Count();
                count = count + 1;
                string str = thisyear.ToString().Remove(0, 2);
                str = str + count.ToString("000000");
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


                int? hometype = null;
                if (DropDownList7.SelectedValue != "0")
                    hometype = int.Parse(DropDownList7.SelectedValue);

                int? familyRequestStudyMonney = null;
                if (DropDownList14.SelectedValue != "-1")
                    familyRequestStudyMonney = int.Parse(DropDownList14.SelectedValue);

                int? province2 = null;
                if (ddlprovince2.SelectedValue != "0")
                    province2 = int.Parse(ddlprovince2.SelectedValue);

                int? aumpher2 = null;
                if (txtAumper2.SelectedValue != "0")
                    aumpher2 = int.Parse(txtAumper2.SelectedValue);

                int? tumbon2 = null;
                if (txtTumbon2.SelectedValue != "0")
                    tumbon2 = int.Parse(txtTumbon2.SelectedValue);

                int? province3 = null;
                if (ddlprovince3.SelectedValue != "0")
                    province3 = int.Parse(ddlprovince3.SelectedValue);

                int? aumpher3 = null;
                if (txtAumper3.SelectedValue != "0")
                    aumpher3 = int.Parse(txtAumper3.SelectedValue);

                int? tumbon3 = null;
                if (txtTumbon3.SelectedValue != "0")
                    tumbon3 = int.Parse(txtTumbon3.SelectedValue);



                double? famincome = null;
                double? faincome = null;
                double? moincome = null;
                if (TextBox28.Text != "")
                {
                    famincome = double.Parse(TextBox28.Text);
                }
                if (TextBox33.Text != "")
                {
                    faincome = double.Parse(TextBox33.Text);
                }
                if (TextBox40.Text != "")
                {
                    moincome = double.Parse(TextBox40.Text);
                }


                _db.TPreRegisters.Add(new TPreRegister
                {
                    preRegisterId = nID,
                    sFamilyAumpher = famAum,
                    sFamilyHomeNumber = famHomenum.Text,
                    sFamilyIdCardNumber = famIdCard.Text,
                    sFamilyLast = famLast.Text,
                    sFamilyMuu = famMuu.Text,
                    sFamilyName = famName.Text,
                    sFamilyNameEN = TextBox24.Text,
                    sFamilyLastEN = TextBox25.Text,
                    dFamilyBirthDay = _dBirthFamily,
                    nFamilyRequestStudyMoney = familyRequestStudyMonney,
                    sFamilyGraduated = int.Parse(DropDownList18.SelectedValue),
                    sFamilyJob = TextBox27.Text,
                    sFamilyWorkPlace = TextBox26.Text,
                    nFamilyIncome = famincome,
                    sPhoneOne = famPhone1.Text,
                    sPhoneThree = famPhone3.Text,
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
                    sFatherNameEN = TextBox29.Text,
                    sFatherLastEN = TextBox30.Text,
                    dFatherBirthDay = _dBirthFather,
                    sFatherGraduated = int.Parse(DropDownList22.SelectedValue),
                    sFatherJob = TextBox31.Text,
                    sFatherWorkPlace = TextBox32.Text,
                    nFatherIncome = faincome,
                    sFatherPhone = TextBox34.Text,
                    sFatherPhone2 = TextBox35.Text,
                    sFatherPhone3 = TextBox37.Text,

                    cDel = null,
                    cSex = sex,
                    cType = "0",
                    dBirth = _dBirth,
                    dPicUpdate = DateTime.Now,
                    MotherTitle = int.Parse(motherTitle.SelectedValue),
                    nHeight = Height,
                    nPicversion = !string.IsNullOrEmpty(link) ? 1 : 0,
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
                    registerCode = count.ToString("0000"),
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
                    sMotherNameEN = TextBox48.Text,
                    sMotherLastEN = TextBox49.Text,
                    dMotherBirthDay = _dBirthMother,
                    sMotherGraduated = int.Parse(DropDownList23.SelectedValue),
                    sMotherJob = TextBox38.Text,
                    sMotherWorkPlace = TextBox39.Text,
                    nMotherIncome = moincome,
                    sMotherPhone = TextBox45.Text,
                    sMotherPhone2 = TextBox46.Text,
                    sMotherPhone3 = TextBox47.Text,
                    sName = sNameTH.Text,
                    sNickName = sNick.Text,
                    sNickNameEN = sNickEN.Text,

                    sPhone = sPhone.Text,

                    sPostalcode = "",
                    sSickDanger = sickdanger,
                    sSickDrug = sickdrug,
                    sSickFood = sickfood,
                    sSickNormal = sicknormal,
                    sSickOther = sickother,
                    sStudentAumpher = userAum,
                    sStudentHomeNumber = stdHomenum.Text,
                    sStudentHomeRegisterCode = stdHomenum2.Text,
                    sStudentID = str,
                    sStudentIdCardNumber = citizenID.Text,
                    sStudentLastEN = sLastEN.Text,
                    sStudentMuu = stdMuu.Text,
                    sStudentNameEN = sNameEN.Text,
                    sStudentNation = sStudentNation,
                    sStudentPicture = link,
                    sStudentPost = txtPost.Text,
                    sStudentProvince = userPro,
                    sStudentRace = sStudentRace,
                    sStudentReligion = sStudentReligion,
                    sStudentRoad = stdRoad.Text,
                    sStudentSoy = stdSoy.Text,
                    sStudentHousePhone = TextBox50.Text,
                    sStudentTumbon = userTum,
                    StudentTitle = StudentTitle,
                    stayWithTitle = stayWithTitle,
                    stayWithName = TextBox1.Text,
                    stayWithLast = TextBox20.Text,
                    stayWithEmergencyCall = TextBox6.Text,
                    stayWithEmail = TextBox51.Text,
                    HomeType = hometype,
                    friendLastName = friendLast.Text,
                    friendName = friendName.Text,
                    friendPhone = friendPhone.Text,
                    friendSubLevel = int.Parse(friendSublevel.SelectedValue),
                    houseRegistrationNumber = TextBox13.Text,
                    houseRegistrationMuu = TextBox15.Text,
                    houseRegistrationSoy = TextBox16.Text,
                    houseRegistrationRoad = TextBox19.Text,
                    houseRegistrationProvince = province2,
                    houseRegistrationAumpher = aumpher2,
                    houseRegistrationTumbon = tumbon2,
                    houseRegistrationPost = txtPost2.Text,
                    houseRegistrationPhone = TextBox21.Text,
                    bornFrom = TextBox22.Text,
                    bornFromProvince = province3,
                    bornFromAumpher = aumpher3,
                    bornFromTumbon = tumbon3,
                    moveOutReason = oWhyMove,
                    optionBranch = opbrach,
                    optionCourse = opCourse,
                    optionLevel = opLevel,
                    optionTime = opTime,
                    addressLat = lat2,
                    addressLng = lng2,
                    //fatherIncome = fatherJob.SelectedValue,
                    //motherIncome = motherJob.SelectedValue,
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
                    SchoolID = userData.CompanyID
                });
                _db.SaveChanges();


                Response.Redirect("registerPaper.aspx?id=" + nID + "&idSchool=" + sEntities2);
            }
        }
        private void OpenData()
        {
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
        class branchSpec
        {

            public int branchSpecId { get; set; }
            public string name { get; set; }
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