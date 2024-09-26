using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class StdFamily : StudentGateway
    {
        private static string SessionPrimaryKey = "STUDENTID";
        private static string SessionFamilyPrimaryKey = "FAMILYID";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];
            switch (v)
            {
                case "list":
                    MvContent.ActiveViewIndex = 0; break;
                case "form":
                    MvContent.ActiveViewIndex = 1;

                   
                    int schoolID = UserData.CompanyID;

                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {

                        // Title
                        var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                        foreach (var r in titleLists)
                        {
                            this.ltrFatherTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                        }
                        this.ltrParentTitle.Text = this.ltrMotherTitle.Text = this.ltrFatherTitle.Text;

                        // Province
                        var provinces = dbMaster.provinces.ToList();
                        foreach (var r in provinces)
                        {
                            this.ltrFatherProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                        }
                        this.ltrParentProvince.Text = this.ltrMotherProvince.Text = this.ltrFatherProvince.Text;

                        // Nation
                        var listNation = en.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                        foreach (var l in listNation)
                        {
                            this.ltrFatherNation.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }
                        this.ltrParentNation.Text = this.ltrMotherNation.Text = this.ltrFatherNation.Text;

                        // Religion
                        var listReligion = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                        foreach (var l in listReligion)
                        {
                            this.ltrFatherReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }
                        this.ltrParentReligion.Text = this.ltrMotherReligion.Text = this.ltrFatherReligion.Text;

                        // Race
                        var listRace = en.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                        foreach (var l in listRace)
                        {
                            this.ltrFatherRace.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }
                        this.ltrParentRace.Text = this.ltrMotherRace.Text = this.ltrFatherRace.Text;
                    }
                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod]
        public static string GetFatherItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 25; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = p.sFatherTitle;
                        dt.Rows[0]["F2"] = p.sFatherFirstName;
                        dt.Rows[0]["F3"] = p.sFatherLastName;
                        dt.Rows[0]["F4"] = p.sFatherNameEN;
                        dt.Rows[0]["F5"] = p.sFatherLastEN;
                        dt.Rows[0]["F6"] = p.dFatherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sFatherIdCardNumber;
                        dt.Rows[0]["F8"] = p.sFatherRace;
                        dt.Rows[0]["F9"] = p.sFatherNation;
                        dt.Rows[0]["F10"] = p.sFatherReligion;
                        dt.Rows[0]["F11"] = p.sFatherGraduated == null ? "0" : p.sFatherGraduated.ToString();
                        dt.Rows[0]["F12"] = p.sFatherHomeNumber;
                        dt.Rows[0]["F13"] = p.sFatherSoy;
                        dt.Rows[0]["F14"] = p.sFatherMuu;
                        dt.Rows[0]["F15"] = p.sFatherRoad;
                        dt.Rows[0]["F16"] = p.sFatherProvince;
                        dt.Rows[0]["F17"] = p.sFatherAumpher;
                        dt.Rows[0]["F18"] = p.sFatherTumbon;
                        dt.Rows[0]["F19"] = p.sFatherPost;
                        dt.Rows[0]["F20"] = p.sFatherJob;
                        dt.Rows[0]["F21"] = p.nFatherIncome?.ToString();
                        dt.Rows[0]["F22"] = p.sFatherWorkPlace;
                        dt.Rows[0]["F23"] = p.sFatherPhone;
                        dt.Rows[0]["F24"] = p.sFatherPhone2;
                        dt.Rows[0]["F25"] = p.sFatherPhone3;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        [WebMethod]
        public static string GetMotherItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 25; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = p.sMotherTitle;
                        dt.Rows[0]["F2"] = p.sMotherFirstName;
                        dt.Rows[0]["F3"] = p.sMotherLastName;
                        dt.Rows[0]["F4"] = p.sMotherNameEN;
                        dt.Rows[0]["F5"] = p.sMotherLastEN;
                        dt.Rows[0]["F6"] = p.dMotherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sMotherIdCardNumber;
                        dt.Rows[0]["F8"] = p.sMotherRace;
                        dt.Rows[0]["F9"] = p.sMotherNation;
                        dt.Rows[0]["F10"] = p.sMotherReligion;
                        dt.Rows[0]["F11"] = p.sMotherGraduated == null ? "0" : p.sMotherGraduated.ToString();
                        dt.Rows[0]["F12"] = p.sMotherHomeNumber;
                        dt.Rows[0]["F13"] = p.sMotherSoy;
                        dt.Rows[0]["F14"] = p.sMotherMuu;
                        dt.Rows[0]["F15"] = p.sMotherRoad;
                        dt.Rows[0]["F16"] = p.sMotherProvince;
                        dt.Rows[0]["F17"] = p.sMotherAumpher;
                        dt.Rows[0]["F18"] = p.sMotherTumbon;
                        dt.Rows[0]["F19"] = p.sMotherPost;
                        dt.Rows[0]["F20"] = p.sMotherJob;
                        dt.Rows[0]["F21"] = p.nMotherIncome?.ToString();
                        dt.Rows[0]["F22"] = p.sMotherWorkPlace;
                        dt.Rows[0]["F23"] = p.sMotherPhone;
                        dt.Rows[0]["F24"] = p.sMotherPhone2;
                        dt.Rows[0]["F25"] = p.sMotherPhone3;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        [WebMethod]
        public static string GetParentItem(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 28; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();
                        dt.Rows[0]["F1"] = p.sFamilyTitle;
                        dt.Rows[0]["F2"] = p.sFamilyName;
                        dt.Rows[0]["F3"] = p.sFamilyLast;
                        dt.Rows[0]["F4"] = p.sFamilyNameEN;
                        dt.Rows[0]["F5"] = p.sFamilyLastEN;
                        dt.Rows[0]["F6"] = p.dFamilyBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sFamilyIdCardNumber;
                        dt.Rows[0]["F8"] = p.sFamilyRace;
                        dt.Rows[0]["F9"] = p.sFamilyNation;
                        dt.Rows[0]["F10"] = p.sFamilyReligion;
                        dt.Rows[0]["F11"] = p.sFamilyGraduated == null ? "0" : p.sFamilyGraduated.ToString();
                        dt.Rows[0]["F12"] = p.sFamilyHomeNumber;
                        dt.Rows[0]["F13"] = p.sFamilySoy;
                        dt.Rows[0]["F14"] = p.sFamilyMuu;
                        dt.Rows[0]["F15"] = p.sFamilyRoad;
                        dt.Rows[0]["F16"] = p.sFamilyProvince;
                        dt.Rows[0]["F17"] = p.sFamilyAumpher;
                        dt.Rows[0]["F18"] = p.sFamilyTumbon;
                        dt.Rows[0]["F19"] = p.sFamilyPost;

                        dt.Rows[0]["F20"] = p.sFamilyRelate;
                        dt.Rows[0]["F21"] = p.nFamilyRequestStudyMoney?.ToString();
                        dt.Rows[0]["F22"] = p.familyStatus == null ? "0" : p.familyStatus.ToString();

                        dt.Rows[0]["F23"] = p.sFamilyJob;
                        dt.Rows[0]["F24"] = p.nFamilyIncome?.ToString();
                        dt.Rows[0]["F25"] = p.sFamilyWorkPlace;
                        dt.Rows[0]["F26"] = p.sPhoneOne;
                        dt.Rows[0]["F27"] = p.sPhoneTwo;
                        dt.Rows[0]["F28"] = p.sPhoneThree;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        [WebMethod]
        public static string SaveFatherItem(List<string> data)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                bool success = true;
                int fmlID = 0;
                string message = "Save Successfully";

                try
                {
                    string[] key = data[0].Split('-');
                    int stdID = int.Parse(key[0]); // sid
                    fmlID = int.Parse(key[1]);

                    string FatherTitle = string.IsNullOrEmpty(data[1]) ? null : data[1];
                    string FatherName = data[2];
                    string FatherLastName = data[3];
                    string FatherNameEn = data[4];
                    string FatherNameLastEn = data[5];
                    DateTime? FatherBirthday = string.IsNullOrEmpty(data[6]) ? (DateTime?)null : DateTime.Parse(data[6], new CultureInfo("th-TH"));
                    string FatherIdentification = data[7];
                    string FatherRace = data[8];
                    string FatherNation = data[9];
                    string FatherReligion = data[10];
                    int? FatherGraduated = string.IsNullOrEmpty(data[11]) ? (int?)null : int.Parse(data[11]);
                    string FatherHomeNo = data[12];
                    string FatherSoi = data[13];
                    string FatherMoo = data[14];
                    string FatherRoad = data[15];
                    string FatherProvince = data[16];
                    string FatherAmphoe = data[17];
                    string FatherTombon = data[18];
                    string FatherPostalCode = data[19];
                    string FatherJob = data[20];
                    double? FatherIncome = string.IsNullOrEmpty(data[21]) ? (double?)null : double.Parse(data[21]);
                    string FatherWorkPlace = data[22];
                    string FatherPhone = data[23];
                    string FatherPhone2 = data[24];
                    string FatherPhone3 = data[25];

                    if (fmlID != 0)
                    {
                        // Modify Section
                        TFamilyProfile pi = en.TFamilyProfiles.First(f => f.nFamilyID == fmlID);

                        pi.sFatherTitle = FatherTitle;
                        pi.sFatherFirstName = FatherName;
                        pi.sFatherLastName = FatherLastName;
                        pi.sFatherNameEN = FatherNameEn;
                        pi.sFatherLastEN = FatherNameLastEn;
                        pi.dFatherBirthDay = FatherBirthday;
                        pi.sFatherIdCardNumber = FatherIdentification;
                        pi.sFatherRace = FatherRace;
                        pi.sFatherNation = FatherNation;
                        pi.sFatherReligion = FatherReligion;
                        pi.sFatherGraduated = FatherGraduated;
                        pi.sFatherHomeNumber = FatherHomeNo;
                        pi.sFatherSoy = FatherSoi;
                        pi.sFatherMuu = FatherMoo;
                        pi.sFatherRoad = FatherRoad;
                        pi.sFatherProvince = FatherProvince;
                        pi.sFatherAumpher = FatherAmphoe;
                        pi.sFatherTumbon = FatherTombon;
                        pi.sFatherPost = FatherPostalCode;
                        pi.sFatherJob = FatherJob;
                        pi.nFatherIncome = FatherIncome;
                        pi.sFatherWorkPlace = FatherWorkPlace;
                        pi.sFatherPhone = FatherPhone;
                        pi.sFatherPhone2 = FatherPhone2;
                        pi.sFatherPhone3 = FatherPhone3;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลนักเรียน(ข้อมูลบิดา) รหัสนักเรียน:" + pi.sID, HttpContext.Current.Request, 14, 3, 0, schoolID);
                    }
                    else
                    {
                        // Insert Section
                        TFamilyProfile f = new TFamilyProfile
                        {
                            sID = stdID,
                            sFatherTitle = FatherTitle,
                            sFatherFirstName = FatherName,
                            sFatherLastName = FatherLastName,
                            sFatherNameEN = FatherNameEn,
                            sFatherLastEN = FatherNameLastEn,
                            dFatherBirthDay = FatherBirthday,
                            sFatherIdCardNumber = FatherIdentification,
                            sFatherRace = FatherRace,
                            sFatherNation = FatherNation,
                            sFatherReligion = FatherReligion,
                            sFatherGraduated = FatherGraduated,
                            sFatherHomeNumber = FatherHomeNo,
                            sFatherSoy = FatherSoi,
                            sFatherMuu = FatherMoo,
                            sFatherRoad = FatherRoad,
                            sFatherProvince = FatherProvince,
                            sFatherAumpher = FatherAmphoe,
                            sFatherTumbon = FatherTombon,
                            sFatherPost = FatherPostalCode,
                            sFatherJob = FatherJob,
                            nFatherIncome = FatherIncome,
                            sFatherWorkPlace = FatherWorkPlace,
                            sFatherPhone = FatherPhone,
                            sFatherPhone2 = FatherPhone2,
                            sFatherPhone3 = FatherPhone3,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now,

                            sDeleted = "false",
                            SchoolID = schoolID
                        };

                        en.TFamilyProfiles.Add(f);

                        en.SaveChanges();

                        fmlID = f.nFamilyID;

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลนักเรียน(ข้อมูลบิดา) รหัสนักเรียน:" + f.sID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                var result = new { success, fmlID, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static string SaveMotherItem(List<string> data)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                bool success = true;
                int fmlID = 0;
                string message = "Save Successfully";

                try
                {
                    string[] key = data[0].Split('-');
                    int stdID = int.Parse(key[0]); // sid
                    fmlID = int.Parse(key[1]);

                    string MotherTitle = string.IsNullOrEmpty(data[1]) ? null : data[1];
                    string MotherName = data[2];
                    string MotherLastName = data[3];
                    string MotherNameEn = data[4];
                    string MotherNameLastEn = data[5];
                    DateTime? MotherBirthday = string.IsNullOrEmpty(data[6]) ? (DateTime?)null : DateTime.Parse(data[6], new CultureInfo("th-TH"));
                    string MotherIdentification = data[7];
                    string MotherRace = data[8];
                    string MotherNation = data[9];
                    string MotherReligion = data[10];
                    int? MotherGraduated = string.IsNullOrEmpty(data[11]) ? (int?)null : int.Parse(data[11]);
                    string MotherHomeNo = data[12];
                    string MotherSoi = data[13];
                    string MotherMoo = data[14];
                    string MotherRoad = data[15];
                    string MotherProvince = data[16];
                    string MotherAmphoe = data[17];
                    string MotherTombon = data[18];
                    string MotherPostalCode = data[19];
                    string MotherJob = data[20];
                    double? MotherIncome = string.IsNullOrEmpty(data[21]) ? (double?)null : double.Parse(data[21]);
                    string MotherWorkPlace = data[22];
                    string MotherPhone = data[23];
                    string MotherPhone2 = data[24];
                    string MotherPhone3 = data[25];

                    if (fmlID != 0)
                    {
                        // Modify Section
                        TFamilyProfile pi = en.TFamilyProfiles.First(f => f.nFamilyID == fmlID);

                        pi.sMotherTitle = MotherTitle;
                        pi.sMotherFirstName = MotherName;
                        pi.sMotherLastName = MotherLastName;
                        pi.sMotherNameEN = MotherNameEn;
                        pi.sMotherLastEN = MotherNameLastEn;
                        pi.dMotherBirthDay = MotherBirthday;
                        pi.sMotherIdCardNumber = MotherIdentification;
                        pi.sMotherRace = MotherRace;
                        pi.sMotherNation = MotherNation;
                        pi.sMotherReligion = MotherReligion;
                        pi.sMotherGraduated = MotherGraduated;
                        pi.sMotherHomeNumber = MotherHomeNo;
                        pi.sMotherSoy = MotherSoi;
                        pi.sMotherMuu = MotherMoo;
                        pi.sMotherRoad = MotherRoad;
                        pi.sMotherProvince = MotherProvince;
                        pi.sMotherAumpher = MotherAmphoe;
                        pi.sMotherTumbon = MotherTombon;
                        pi.sMotherPost = MotherPostalCode;
                        pi.sMotherJob = MotherJob;
                        pi.nMotherIncome = MotherIncome;
                        pi.sMotherWorkPlace = MotherWorkPlace;
                        pi.sMotherPhone = MotherPhone;
                        pi.sMotherPhone2 = MotherPhone2;
                        pi.sMotherPhone3 = MotherPhone3;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลนักเรียน(ข้อมูลมารดา) รหัสนักเรียน:" + pi.sID, HttpContext.Current.Request, 14, 3, 0, schoolID);
                    }
                    else
                    {
                        // Insert Section
                        TFamilyProfile f = new TFamilyProfile
                        {
                            sID = stdID,
                            sMotherTitle = MotherTitle,
                            sMotherFirstName = MotherName,
                            sMotherLastName = MotherLastName,
                            sMotherNameEN = MotherNameEn,
                            sMotherLastEN = MotherNameLastEn,
                            dMotherBirthDay = MotherBirthday,
                            sMotherIdCardNumber = MotherIdentification,
                            sMotherRace = MotherRace,
                            sMotherNation = MotherNation,
                            sMotherReligion = MotherReligion,
                            sMotherGraduated = MotherGraduated,
                            sMotherHomeNumber = MotherHomeNo,
                            sMotherSoy = MotherSoi,
                            sMotherMuu = MotherMoo,
                            sMotherRoad = MotherRoad,
                            sMotherProvince = MotherProvince,
                            sMotherAumpher = MotherAmphoe,
                            sMotherTumbon = MotherTombon,
                            sMotherPost = MotherPostalCode,
                            sMotherJob = MotherJob,
                            nMotherIncome = MotherIncome,
                            sMotherWorkPlace = MotherWorkPlace,
                            sMotherPhone = MotherPhone,
                            sMotherPhone2 = MotherPhone2,
                            sMotherPhone3 = MotherPhone3,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now,

                            sDeleted = "false",
                            SchoolID = schoolID
                        };

                        en.TFamilyProfiles.Add(f);

                        en.SaveChanges();

                        fmlID = f.nFamilyID;

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลนักเรียน(ข้อมูลมารดา) รหัสนักเรียน:" + f.sID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                var result = new { success, fmlID, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static string SaveParentItem(List<string> data)
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                bool success = true;
                int fmlID = 0;
                string message = "Save Successfully";

                try
                {
                    string[] key = data[0].Split('-');
                    int stdID = int.Parse(key[0]); // sid
                    fmlID = int.Parse(key[1]);

                    string ParentTitle = string.IsNullOrEmpty(data[1]) ? null : data[1];
                    string ParentName = data[2];
                    string ParentLastName = data[3];
                    string ParentNameEn = data[4];
                    string ParentNameLastEn = data[5];
                    DateTime? ParentBirthday = string.IsNullOrEmpty(data[6]) ? (DateTime?)null : DateTime.Parse(data[6], new CultureInfo("th-TH"));
                    string ParentIdentification = data[7];
                    string ParentRace = data[8];
                    string ParentNation = data[9];
                    string ParentReligion = data[10];
                    int? ParentGraduated = string.IsNullOrEmpty(data[11]) ? (int?)null : int.Parse(data[11]);
                    string ParentHomeNo = data[12];
                    string ParentSoi = data[13];
                    string ParentMoo = data[14];
                    string ParentRoad = data[15];
                    string ParentProvince = data[16];
                    string ParentAmphoe = data[17];
                    string ParentTombon = data[18];
                    string ParentPostalCode = data[19];

                    string ParentRelate = data[20];
                    int? ParentRequestStudyMoney = string.IsNullOrEmpty(data[21]) ? (int?)null : int.Parse(data[21]);
                    int? ParentStatus = string.IsNullOrEmpty(data[22]) ? (int?)null : int.Parse(data[22]);

                    string ParentJob = data[23];
                    double? ParentIncome = string.IsNullOrEmpty(data[24]) ? (double?)null : double.Parse(data[24]);
                    string ParentWorkPlace = data[25];
                    string ParentPhone = data[26];
                    string ParentPhone2 = data[27];
                    string ParentPhone3 = data[28];

                    if (fmlID != 0)
                    {
                        // Modify Section
                        TFamilyProfile pi = en.TFamilyProfiles.First(f => f.nFamilyID == fmlID);

                        pi.sFamilyTitle = ParentTitle;
                        pi.sFamilyName = ParentName;
                        pi.sFamilyLast = ParentLastName;
                        pi.sFamilyNameEN = ParentNameEn;
                        pi.sFamilyLastEN = ParentNameLastEn;
                        pi.dFamilyBirthDay = ParentBirthday;
                        pi.sFamilyIdCardNumber = ParentIdentification;
                        pi.sFamilyRace = ParentRace;
                        pi.sFamilyNation = ParentNation;
                        pi.sFamilyReligion = ParentReligion;
                        pi.sFamilyGraduated = ParentGraduated;
                        pi.sFamilyHomeNumber = ParentHomeNo;
                        pi.sFamilySoy = ParentSoi;
                        pi.sFamilyMuu = ParentMoo;
                        pi.sFamilyRoad = ParentRoad;
                        pi.sFamilyProvince = ParentProvince;
                        pi.sFamilyAumpher = ParentAmphoe;
                        pi.sFamilyTumbon = ParentTombon;
                        pi.sFamilyPost = ParentPostalCode;

                        pi.sFamilyRelate = ParentRelate;
                        pi.nFamilyRequestStudyMoney = ParentRequestStudyMoney;
                        pi.familyStatus = ParentStatus;

                        pi.sFamilyJob = ParentJob;
                        pi.nFamilyIncome = ParentIncome;
                        pi.sFamilyWorkPlace = ParentWorkPlace;
                        pi.sPhoneOne = ParentPhone;
                        pi.sPhoneTwo = ParentPhone2;
                        pi.sPhoneThree = ParentPhone3;
                        pi.UpdatedBy = userData.UserID;
                        pi.UpdatedDate = DateTime.Now;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลนักเรียน(ข้อมูลผู้ปกครอง) รหัสนักเรียน:" + pi.sID, HttpContext.Current.Request, 14, 3, 0, schoolID);
                    }
                    else
                    {
                        // Insert Section
                        TFamilyProfile f = new TFamilyProfile
                        {
                            sID = stdID,
                            sFamilyTitle = ParentTitle,
                            sFamilyName = ParentName,
                            sFamilyLast = ParentLastName,
                            sFamilyNameEN = ParentNameEn,
                            sFamilyLastEN = ParentNameLastEn,
                            dFamilyBirthDay = ParentBirthday,
                            sFamilyIdCardNumber = ParentIdentification,
                            sFamilyRace = ParentRace,
                            sFamilyNation = ParentNation,
                            sFamilyReligion = ParentReligion,
                            sFamilyGraduated = ParentGraduated,
                            sFamilyHomeNumber = ParentHomeNo,
                            sFamilySoy = ParentSoi,
                            sFamilyMuu = ParentMoo,
                            sFamilyRoad = ParentRoad,
                            sFamilyProvince = ParentProvince,
                            sFamilyAumpher = ParentAmphoe,
                            sFamilyTumbon = ParentTombon,
                            sFamilyPost = ParentPostalCode,

                            sFamilyRelate = ParentRelate,
                            nFamilyRequestStudyMoney = ParentRequestStudyMoney,
                            familyStatus = ParentStatus,

                            sFamilyJob = ParentJob,
                            nFamilyIncome = ParentIncome,
                            sFamilyWorkPlace = ParentWorkPlace,
                            sPhoneOne = ParentPhone,
                            sPhoneTwo = ParentPhone2,
                            sPhoneThree = ParentPhone3,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now,

                            sDeleted = "false",
                            SchoolID = schoolID
                        };

                        en.TFamilyProfiles.Add(f);

                        en.SaveChanges();

                        fmlID = f.nFamilyID;

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลนักเรียน(ข้อมูลผู้ปกครอง) รหัสนักเรียน:" + f.sID, HttpContext.Current.Request, 14, 2, 0, schoolID);
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }

                var result = new { success, fmlID, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionFamilyPrimaryKey] = null;

            return "";
        }


        [WebMethod]
        public static string GetFatherItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                
                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 25; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();

                        var fatherTitleID = string.IsNullOrEmpty(p.sFatherTitle) ? 0 : int.Parse(p.sFatherTitle);
                        var fatherTitle = "";
                        var fatherTitleObj = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == fatherTitleID).FirstOrDefault();
                        if (fatherTitleObj != null)
                        {
                            fatherTitle = fatherTitleObj.titleDescription;
                        }
                        dt.Rows[0]["F1"] = fatherTitle;
                        dt.Rows[0]["F2"] = p.sFatherFirstName;
                        dt.Rows[0]["F3"] = p.sFatherLastName;
                        dt.Rows[0]["F4"] = p.sFatherNameEN;
                        dt.Rows[0]["F5"] = p.sFatherLastEN;
                        dt.Rows[0]["F6"] = p.dFatherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sFatherIdCardNumber;

                        var fatherRace = "";
                        var fatherRaceObj = en.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == p.sFatherRace).FirstOrDefault();
                        if (fatherRaceObj != null)
                        {
                            fatherRace = fatherRaceObj.MasterDes;
                        }
                        dt.Rows[0]["F8"] = fatherRace;

                        var fatherNation = "";
                        var fatherNationObj = en.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == p.sFatherNation).FirstOrDefault();
                        if (fatherNationObj != null)
                        {
                            fatherNation = fatherNationObj.MasterDes;
                        }
                        dt.Rows[0]["F9"] = fatherNation;

                        var fatherReligion = "";
                        var fatherReligionObj = en.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == p.sFatherReligion).FirstOrDefault();
                        if (fatherReligionObj != null)
                        {
                            fatherReligion = fatherReligionObj.MasterDes;
                        }
                        dt.Rows[0]["F10"] = fatherReligion;

                        var fatherGraduated = "";
                        switch (p.sFatherGraduated)
                        {
                            case 1: fatherGraduated = "ต่ำกว่าประถม"; break;
                            case 2: fatherGraduated = "ประถมศึกษา"; break;
                            case 7: fatherGraduated = "มัธยมต้น"; break;
                            case 3: fatherGraduated = "มัธยมศึกษาหรือเทียบเท่า"; break;
                            case 9: fatherGraduated = "ประกาศนียบัตรวิชาชีพ"; break;
                            case 10: fatherGraduated = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
                            case 8: fatherGraduated = "อนุปริญญา"; break;
                            case 4: fatherGraduated = "ปริญญาตรี"; break;
                            case 5: fatherGraduated = "ปริญญาโท"; break;
                            case 6: fatherGraduated = "ปริญญาเอก"; break;
                            default: fatherGraduated = ""; break;
                        }
                        dt.Rows[0]["F11"] = fatherGraduated;
                        dt.Rows[0]["F12"] = p.sFatherHomeNumber;
                        dt.Rows[0]["F13"] = p.sFatherSoy;
                        dt.Rows[0]["F14"] = p.sFatherMuu;
                        dt.Rows[0]["F15"] = p.sFatherRoad;

                        var provinceID = string.IsNullOrEmpty(p.sFatherProvince) ? 0 : int.Parse(p.sFatherProvince);
                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F16"] = province;

                        var aumpherID = string.IsNullOrEmpty(p.sFatherAumpher) ? 0 : int.Parse(p.sFatherAumpher);
                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F17"] = aumpher;

                        var tumbonID = string.IsNullOrEmpty(p.sFatherTumbon) ? 0 : int.Parse(p.sFatherTumbon);
                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F18"] = tumbon;
                        dt.Rows[0]["F19"] = p.sFatherPost;
                        dt.Rows[0]["F20"] = p.sFatherJob;
                        dt.Rows[0]["F21"] = p.nFatherIncome?.ToString();
                        dt.Rows[0]["F22"] = p.sFatherWorkPlace;
                        dt.Rows[0]["F23"] = p.sFatherPhone;
                        dt.Rows[0]["F24"] = p.sFatherPhone2;
                        dt.Rows[0]["F25"] = p.sFatherPhone3;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        [WebMethod]
        public static string GetMotherItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

               
                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 25; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();

                        var motherTitleID = string.IsNullOrEmpty(p.sMotherTitle) ? 0 : int.Parse(p.sMotherTitle);
                        var motherTitle = "";
                        var motherTitleObj = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == motherTitleID).FirstOrDefault();
                        if (motherTitleObj != null)
                        {
                            motherTitle = motherTitleObj.titleDescription;
                        }
                        dt.Rows[0]["F1"] = motherTitle;
                        dt.Rows[0]["F2"] = p.sMotherFirstName;
                        dt.Rows[0]["F3"] = p.sMotherLastName;
                        dt.Rows[0]["F4"] = p.sMotherNameEN;
                        dt.Rows[0]["F5"] = p.sMotherLastEN;
                        dt.Rows[0]["F6"] = p.dMotherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sMotherIdCardNumber;

                        var motherRace = "";
                        var motherRaceObj = en.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == p.sMotherRace).FirstOrDefault();
                        if (motherRaceObj != null)
                        {
                            motherRace = motherRaceObj.MasterDes;
                        }
                        dt.Rows[0]["F8"] = motherRace;

                        var motherNation = "";
                        var motherNationObj = en.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == p.sMotherNation).FirstOrDefault();
                        if (motherNationObj != null)
                        {
                            motherNation = motherNationObj.MasterDes;
                        }
                        dt.Rows[0]["F9"] = motherNation;

                        var motherReligion = "";
                        var motherReligionObj = en.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == p.sMotherReligion).FirstOrDefault();
                        if (motherReligionObj != null)
                        {
                            motherReligion = motherReligionObj.MasterDes;
                        }
                        dt.Rows[0]["F10"] = motherReligion;

                        var motherGraduated = "";
                        switch (p.sMotherGraduated)
                        {
                            case 1: motherGraduated = "ต่ำกว่าประถม"; break;
                            case 2: motherGraduated = "ประถมศึกษา"; break;
                            case 7: motherGraduated = "มัธยมต้น"; break;
                            case 3: motherGraduated = "มัธยมศึกษาหรือเทียบเท่า"; break;
                            case 9: motherGraduated = "ประกาศนียบัตรวิชาชีพ"; break;
                            case 10: motherGraduated = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
                            case 8: motherGraduated = "อนุปริญญา"; break;
                            case 4: motherGraduated = "ปริญญาตรี"; break;
                            case 5: motherGraduated = "ปริญญาโท"; break;
                            case 6: motherGraduated = "ปริญญาเอก"; break;
                            default: motherGraduated = ""; break;
                        }
                        dt.Rows[0]["F11"] = motherGraduated;
                        dt.Rows[0]["F12"] = p.sMotherHomeNumber;
                        dt.Rows[0]["F13"] = p.sMotherSoy;
                        dt.Rows[0]["F14"] = p.sMotherMuu;
                        dt.Rows[0]["F15"] = p.sMotherRoad;

                        var provinceID = string.IsNullOrEmpty(p.sMotherProvince) ? 0 : int.Parse(p.sMotherProvince);
                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F16"] = province;

                        var aumpherID = string.IsNullOrEmpty(p.sMotherAumpher) ? 0 : int.Parse(p.sMotherAumpher);
                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F17"] = aumpher;

                        var tumbonID = string.IsNullOrEmpty(p.sMotherTumbon) ? 0 : int.Parse(p.sMotherTumbon);
                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F18"] = tumbon;
                        dt.Rows[0]["F19"] = p.sMotherPost;
                        dt.Rows[0]["F20"] = p.sMotherJob;
                        dt.Rows[0]["F21"] = p.nMotherIncome?.ToString();
                        dt.Rows[0]["F22"] = p.sMotherWorkPlace;
                        dt.Rows[0]["F23"] = p.sMotherPhone;
                        dt.Rows[0]["F24"] = p.sMotherPhone2;
                        dt.Rows[0]["F25"] = p.sMotherPhone3;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        [WebMethod]
        public static string GetParentItemView(string stdID)
        {
            int schoolID = GetUserData().CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                

                string infor = "new";

                try
                {
                    int iStdID = 0;
                    if (!int.TryParse(stdID, out iStdID)) { iStdID = 0; }

                    TFamilyProfile p = en.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == iStdID && w.sDeleted == "false").FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 0; i <= 28; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        dt.Rows[0]["F0"] = p.nFamilyID.ToString();

                        var familyTitleID = string.IsNullOrEmpty(p.sFamilyTitle) ? 0 : int.Parse(p.sFamilyTitle);
                        var familyTitle = "";
                        var familyTitleObj = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == familyTitleID).FirstOrDefault();
                        if (familyTitleObj != null)
                        {
                            familyTitle = familyTitleObj.titleDescription;
                        }
                        dt.Rows[0]["F1"] = familyTitle;
                        dt.Rows[0]["F2"] = p.sFamilyName;
                        dt.Rows[0]["F3"] = p.sFamilyLast;
                        dt.Rows[0]["F4"] = p.sFamilyNameEN;
                        dt.Rows[0]["F5"] = p.sFamilyLastEN;
                        dt.Rows[0]["F6"] = p.dFamilyBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F7"] = p.sFamilyIdCardNumber;

                        var familyRace = "";
                        var familyRaceObj = en.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == p.sFamilyRace).FirstOrDefault();
                        if (familyRaceObj != null)
                        {
                            familyRace = familyRaceObj.MasterDes;
                        }
                        dt.Rows[0]["F8"] = familyRace;

                        var familyNation = "";
                        var familyNationObj = en.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == p.sFamilyNation).FirstOrDefault();
                        if (familyNationObj != null)
                        {
                            familyNation = familyNationObj.MasterDes;
                        }
                        dt.Rows[0]["F9"] = familyNation;

                        var familyReligion = "";
                        var familyReligionObj = en.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == p.sFamilyReligion).FirstOrDefault();
                        if (familyReligionObj != null)
                        {
                            familyReligion = familyReligionObj.MasterDes;
                        }
                        dt.Rows[0]["F10"] = familyReligion;

                        var familyGraduated = "";
                        switch (p.sFamilyGraduated)
                        {
                            case 1: familyGraduated = "ต่ำกว่าประถม"; break;
                            case 2: familyGraduated = "ประถมศึกษา"; break;
                            case 7: familyGraduated = "มัธยมต้น"; break;
                            case 3: familyGraduated = "มัธยมศึกษาหรือเทียบเท่า"; break;
                            case 9: familyGraduated = "ประกาศนียบัตรวิชาชีพ"; break;
                            case 10: familyGraduated = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
                            case 8: familyGraduated = "อนุปริญญา"; break;
                            case 4: familyGraduated = "ปริญญาตรี"; break;
                            case 5: familyGraduated = "ปริญญาโท"; break;
                            case 6: familyGraduated = "ปริญญาเอก"; break;
                            default: familyGraduated = ""; break;
                        }
                        dt.Rows[0]["F11"] = familyGraduated;
                        dt.Rows[0]["F12"] = p.sFamilyHomeNumber;
                        dt.Rows[0]["F13"] = p.sFamilySoy;
                        dt.Rows[0]["F14"] = p.sFamilyMuu;
                        dt.Rows[0]["F15"] = p.sFamilyRoad;

                        var provinceID = string.IsNullOrEmpty(p.sFamilyProvince) ? 0 : int.Parse(p.sFamilyProvince);
                        var province = "";
                        var provinceObj = dbMaster.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            province = provinceObj.PROVINCE_NAME;
                        }
                        dt.Rows[0]["F16"] = province;

                        var aumpherID = string.IsNullOrEmpty(p.sFamilyAumpher) ? 0 : int.Parse(p.sFamilyAumpher);
                        var aumpher = "";
                        var aumpherObj = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            aumpher = aumpherObj.AMPHUR_NAME;
                        }
                        dt.Rows[0]["F17"] = aumpher;

                        var tumbonID = string.IsNullOrEmpty(p.sFamilyTumbon) ? 0 : int.Parse(p.sFamilyTumbon);
                        var tumbon = "";
                        var tumbonObj = dbMaster.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            tumbon = tumbonObj.DISTRICT_NAME;
                        }
                        dt.Rows[0]["F18"] = tumbon;
                        dt.Rows[0]["F19"] = p.sFamilyPost;

                        dt.Rows[0]["F20"] = p.sFamilyRelate;
                        dt.Rows[0]["F21"] = p.nFamilyRequestStudyMoney == null ? "" : (p.nFamilyRequestStudyMoney.ToString() == "1" ? "เบิกได้" : "เบิกไม่ได้");

                        var familyStatus = "";
                        switch (p.familyStatus)
                        {
                            case 1: familyStatus = "บิดามารดาอยู่ด้วยกัน"; break;
                            case 2: familyStatus = "บิดามารดาแยกกันอยู่"; break;
                            case 3: familyStatus = "บิดามารดาหย่าร้าง"; break;
                            case 4: familyStatus = "บิดาถึงแก่กรรม"; break;
                            case 5: familyStatus = "มารดาถึงแก่กรรม"; break;
                            case 6: familyStatus = "บิดามารดาถึงแก่กรรม"; break;
                            case 7: familyStatus = "บิดามารดาแต่งงานใหม่"; break;
                            default: familyStatus = ""; break;
                        }
                        dt.Rows[0]["F22"] = familyStatus;

                        dt.Rows[0]["F23"] = p.sFamilyJob;
                        dt.Rows[0]["F24"] = p.nFamilyIncome?.ToString();
                        dt.Rows[0]["F25"] = p.sFamilyWorkPlace;
                        dt.Rows[0]["F26"] = p.sPhoneOne;
                        dt.Rows[0]["F27"] = p.sPhoneTwo;
                        dt.Rows[0]["F28"] = p.sPhoneThree;

                        ds.Tables.Add(dt);

                        infor = ds.GetXml();
                    }
                    else
                    {
                        infor = "new";
                    }
                }
                catch
                {
                    infor = "error";
                }

                return infor;
            }
        }

        #endregion
    }
}