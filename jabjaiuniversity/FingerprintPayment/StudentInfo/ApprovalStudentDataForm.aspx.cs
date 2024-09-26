using FingerprintPayment.Helper;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class ApprovalStudentDataForm : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            string id = Request.QueryString["id"];
            int.TryParse(Request.QueryString["sid"], out int sid);
            string section = Request.QueryString["section"];

            int schoolID = UserData.CompanyID;
            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                // Title
                var titleLists = ctx.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                foreach (var r in titleLists)
                {
                    this.ltrStudentTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                }
                this.ltrHomeStayWithTitle.Text = this.ltrFatherTitle.Text = this.ltrMotherTitle.Text = this.ltrParentTitle.Text = this.ltrStudentTitle.Text;

                // Race
                var listRace = ctx.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                foreach (var l in listRace)
                {
                    this.ltrStudentRace.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }
                this.ltrFatherRace.Text = this.ltrMotherRace.Text = this.ltrParentRace.Text = this.ltrStudentRace.Text;

                // Nation
                var listNation = ctx.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                foreach (var l in listNation)
                {
                    this.ltrStudentNation.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }
                this.ltrFatherNation.Text = this.ltrMotherNation.Text = this.ltrParentNation.Text = this.ltrStudentNation.Text;

                // Religion
                var listReligion = ctx.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                foreach (var l in listReligion)
                {
                    this.ltrStudentReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                }
                this.ltrFatherReligion.Text = this.ltrMotherReligion.Text = this.ltrParentReligion.Text = this.ltrStudentReligion.Text;

                // Disability - ความพิการ
                var listDisability = ctx.TMasterDatas.Where(w => w.MasterType == "7").ToList();
                foreach (var l in listDisability)
                {
                    this.ltrDisabilityCode.Text += string.Format(@"<option {2} value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes, (l.MasterCode == "99" ? "selected=\"selected\"" : ""));
                }

                // Disadvantage - ความด้อยโอกาส
                var listDisadvantage = ctx.TMasterDatas.Where(w => w.MasterType == "8").ToList();
                foreach (var l in listDisadvantage)
                {
                    this.ltrDisadvantageCode.Text += string.Format(@"<option {2} value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes, (l.MasterCode == "99" ? "selected=\"selected\"" : ""));
                }

                // Province
                var provinces = mctx.provinces.ToList();
                foreach (var r in provinces)
                {
                    this.ltrRegisterHomeProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                }
                this.ltrBornFromProvince.Text = this.ltrHomeProvince.Text = this.ltrFatherProvince.Text = this.ltrMotherProvince.Text = this.ltrParentProvince.Text = this.ltrRegisterHomeProvince.Text;


            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadDistrict(int provinceID)
        {
            List<EntityDropdown> result = null;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                result = dbMaster.amphurs.Where(w => w.PROVINCE_ID == provinceID).Select(s => new EntityDropdown { id = s.AMPHUR_ID.ToString(), name = s.AMPHUR_NAME }).ToList();

                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadSubDistrict(int districtID)
        {
            List<EntityDropdown> result = null;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            {
               
                result = dbMaster.districts.Where(w => w.AMPHUR_ID == districtID).Select(s => new EntityDropdown { id = s.DISTRICT_ID.ToString(), name = s.DISTRICT_NAME }).ToList();

                return result;
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetRequestApprovalStudentInfo(int sid, string requestDate)
        {
            bool success = true;
            string code = "200";
            string message = "Get data success.";

            var data = new StudentData();

            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    // Get current year
                    StudentLogic studentLogic = new StudentLogic(ctx);
                    string currentTerm = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                    var levels = ctx.TSubLevels.Where(w => w.SchoolID == schoolID).ToList(); // && w.nWorkingStatus == 1
                    var classrooms = ctx.TTermSubLevel2.Where(w => w.SchoolID == schoolID).ToList(); // && w.nTermSubLevel2Status == "1" && w.nWorkingStatus == 1

                    var titles = ctx.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();

                    // Get data in db.
                    JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == sid).FirstOrDefault();
                    if (userObj != null)
                    {
                        // ประวัติส่วนตัว
                        data.Profile.StudentID = new FieldData<string> { Value = userObj.sStudentID };

                        TStudentClassroomHistory schObj = ctx.TStudentClassroomHistories.Where(w => w.SchoolID == schoolID && w.sID == sid && w.nTerm == currentTerm && w.cDel == false).FirstOrDefault();
                        if (schObj != null)
                        {
                            data.Profile.StudentStatus = new FieldData<string> { Value = GetStudentStatus(schObj.nStudentStatus) };
                            data.Profile.StudentMoveInDate = new FieldData<string> { Value = schObj.MoveInDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) };
                            data.Profile.StudentNumber = new FieldData<string> { Value = schObj.nStudentNumber?.ToString() };

                            var classroomObj = classrooms.Where(w => w.nTermSubLevel2 == schObj.nTermSubLevel2).FirstOrDefault();
                            if (classroomObj != null)
                            {
                                var levelObj = levels.Where(w => w.nTSubLevel == classroomObj.nTSubLevel).FirstOrDefault();
                                if (levelObj != null)
                                {
                                    data.Profile.StudentClass = new FieldData<string> { Value = levelObj.SubLevel };
                                    data.Profile.StudentClassRoom = new FieldData<string> { Value = levelObj.SubLevel + " / " + classroomObj.nTSubLevel2 };
                                }
                            }
                        }

                        data.Profile.StudentGender = new FieldData<string> { Value = userObj.cSex };

                        int? studentTitleID = null;
                        var titleObj = titles.Where(w => w.nTitleid.ToString() == userObj.sStudentTitle || w.titleDescription == userObj.sStudentTitle).FirstOrDefault();
                        if (titleObj != null)
                        {
                            studentTitleID = titleObj.nTitleid;
                        }
                        data.Profile.StudentTitle = new FieldData<int?> { Value = studentTitleID };

                        data.Profile.StudentFirstNameTh = new FieldData<string> { Value = userObj.sName };
                        data.Profile.StudentLastNameTh = new FieldData<string> { Value = userObj.sLastname };
                        data.Profile.StudentFirstNameEn = new FieldData<string> { Value = userObj.sStudentNameEN };
                        data.Profile.StudentLastNameEn = new FieldData<string> { Value = userObj.sStudentLastEN };
                        data.Profile.StudentFirstNameOther = new FieldData<string> { Value = userObj.sStudentNameOther };
                        data.Profile.StudentLastNameOther = new FieldData<string> { Value = userObj.sStudentLastOther };
                        data.Profile.StudentNickNameTh = new FieldData<string> { Value = userObj.sNickName };
                        data.Profile.StudentNickNameEn = new FieldData<string> { Value = userObj.sNickNameEN };
                        data.Profile.StudentBirthday = new FieldData<string> { Value = userObj.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) };
                        data.Profile.StudentRace = new FieldData<string> { Value = userObj.sStudentRace };
                        data.Profile.StudentNation = new FieldData<string> { Value = userObj.sStudentNation };
                        data.Profile.StudentReligion = new FieldData<string> { Value = userObj.sStudentReligion };
                        data.Profile.DisabilityCode = new FieldData<string> { Value = string.IsNullOrEmpty(userObj.DisabilityCode) ? "99" : userObj.DisabilityCode };
                        data.Profile.DisadvantageCode = new FieldData<string> { Value = string.IsNullOrEmpty(userObj.DisadvantageCode) ? "99" : userObj.DisadvantageCode };
                        data.Profile.StudentPhone = new FieldData<string> { Value = userObj.sPhone };
                        data.Profile.StudentEmail = new FieldData<string> { Value = userObj.sEmail };

                        data.Profile.StudentSonNumber = new FieldData<int?> { Value = userObj.nSonNumber };
                        data.Profile.Note2 = new FieldData<string> { Value = userObj.Note2 };

                        TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == sid && w.sDeleted == "false").FirstOrDefault();
                        if (familyObj != null)
                        {
                            data.Profile.StudentSonTotal = new FieldData<int?> { Value = familyObj.nSonTotal ?? 0 };
                            data.Profile.StudentBrethrenStudyHere = new FieldData<int?> { Value = familyObj.nRelativeStudyHere };

                            // ที่อยู่ตามทะเบียนบ้าน
                            data.PermanentAddress.RegisterHomeCode = new FieldData<string> { Value = userObj.sStudentHomeRegisterCode };
                            data.PermanentAddress.RegisterHomeNo = new FieldData<string> { Value = familyObj.houseRegistrationNumber };
                            data.PermanentAddress.RegisterHomeSoi = new FieldData<string> { Value = familyObj.houseRegistrationSoy };
                            data.PermanentAddress.RegisterHomeMoo = new FieldData<string> { Value = familyObj.houseRegistrationMuu };
                            data.PermanentAddress.RegisterHomeRoad = new FieldData<string> { Value = familyObj.houseRegistrationRoad };
                            data.PermanentAddress.RegisterHomeProvince = new FieldData<int?> { Value = familyObj.houseRegistrationProvince };
                            data.PermanentAddress.RegisterHomeAmphoe = new FieldData<int?> { Value = familyObj.houseRegistrationAumpher };
                            data.PermanentAddress.RegisterHomeTombon = new FieldData<int?> { Value = familyObj.houseRegistrationTumbon };
                            data.PermanentAddress.RegisterHomePostalCode = new FieldData<string> { Value = familyObj.houseRegistrationPost };
                            data.PermanentAddress.RegisterHomePhone = new FieldData<string> { Value = familyObj.houseRegistrationPhone };
                            data.PermanentAddress.BornFrom = new FieldData<string> { Value = familyObj.bornFrom };
                            data.PermanentAddress.BornFromProvince = new FieldData<int?> { Value = familyObj.bornFromProvince };
                            data.PermanentAddress.BornFromAmphoe = new FieldData<int?> { Value = familyObj.bornFromAumpher };
                            data.PermanentAddress.BornFromTombon = new FieldData<int?> { Value = familyObj.bornFromTumbon };

                            // ที่อยู่ปัจจุบัน
                            data.ContactAddress.UseHouseAddress = new FieldData<bool> { Value = false };
                            data.ContactAddress.HomeNo = new FieldData<string> { Value = userObj.sStudentHomeNumber };
                            data.ContactAddress.HomeSoi = new FieldData<string> { Value = userObj.sStudentSoy };
                            data.ContactAddress.HomeMoo = new FieldData<string> { Value = userObj.sStudentMuu };
                            data.ContactAddress.HomeRoad = new FieldData<string> { Value = userObj.sStudentRoad };
                            data.ContactAddress.HomeProvince = new FieldData<string> { Value = userObj.sStudentProvince };
                            data.ContactAddress.HomeAmphoe = new FieldData<string> { Value = userObj.sStudentAumpher };
                            data.ContactAddress.HomeTombon = new FieldData<string> { Value = userObj.sStudentTumbon };
                            data.ContactAddress.HomePostalCode = new FieldData<string> { Value = userObj.sStudentPost };
                            data.ContactAddress.HomePhone = new FieldData<string> { Value = userObj.sStudentHousePhone };

                            data.ContactAddress.HomeStayWithTitle = new FieldData<int?> { Value = familyObj.stayWithTitle };
                            data.ContactAddress.HomeStayWithName = new FieldData<string> { Value = familyObj.stayWithName };
                            data.ContactAddress.HomeStayWithLast = new FieldData<string> { Value = familyObj.stayWithLast };
                            data.ContactAddress.HomeStayWithEmergencyCall = new FieldData<string> { Value = familyObj.stayWithEmergencyCall };
                            data.ContactAddress.HomeStayWithEmergencyEmail = new FieldData<string> { Value = familyObj.stayWithEmail };
                            data.ContactAddress.HomeFriendName = new FieldData<string> { Value = familyObj.friendName };
                            data.ContactAddress.HomeFriendLastName = new FieldData<string> { Value = familyObj.friendLastName };
                            data.ContactAddress.HomeFriendPhone = new FieldData<string> { Value = familyObj.friendPhone };
                            data.ContactAddress.HomeHomeType = new FieldData<int?> { Value = familyObj.HomeType ?? 0 };

                            // ข้อมูลบิดา
                            data.FatherInfo.FatherTitle = new FieldData<string> { Value = familyObj.sFatherTitle };
                            data.FatherInfo.FatherName = new FieldData<string> { Value = familyObj.sFatherFirstName };
                            data.FatherInfo.FatherLastName = new FieldData<string> { Value = familyObj.sFatherLastName };
                            data.FatherInfo.FatherNameEn = new FieldData<string> { Value = familyObj.sFatherNameEN };
                            data.FatherInfo.FatherNameLastEn = new FieldData<string> { Value = familyObj.sFatherLastEN };
                            data.FatherInfo.FatherBirthday = new FieldData<string> { Value = familyObj.dFatherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) };
                            data.FatherInfo.FatherIdentification = new FieldData<string> { Value = familyObj.sFatherIdCardNumber };
                            data.FatherInfo.FatherRace = new FieldData<string> { Value = familyObj.sFatherRace };
                            data.FatherInfo.FatherNation = new FieldData<string> { Value = familyObj.sFatherNation };
                            data.FatherInfo.FatherReligion = new FieldData<string> { Value = familyObj.sFatherReligion };
                            data.FatherInfo.FatherGraduated = new FieldData<int?> { Value = familyObj.sFatherGraduated ?? 0 };
                            data.FatherInfo.FatherHomeNo = new FieldData<string> { Value = familyObj.sFatherHomeNumber };
                            data.FatherInfo.FatherSoi = new FieldData<string> { Value = familyObj.sFatherSoy };
                            data.FatherInfo.FatherMoo = new FieldData<string> { Value = familyObj.sFatherMuu };
                            data.FatherInfo.FatherRoad = new FieldData<string> { Value = familyObj.sFatherRoad };
                            data.FatherInfo.FatherProvince = new FieldData<string> { Value = familyObj.sFatherProvince };
                            data.FatherInfo.FatherAmphoe = new FieldData<string> { Value = familyObj.sFatherAumpher };
                            data.FatherInfo.FatherTombon = new FieldData<string> { Value = familyObj.sFatherTumbon };
                            data.FatherInfo.FatherPostalCode = new FieldData<string> { Value = familyObj.sFatherPost };
                            data.FatherInfo.FatherJob = new FieldData<string> { Value = familyObj.sFatherJob };
                            data.FatherInfo.FatherIncome = new FieldData<double?> { Value = familyObj.nFatherIncome };
                            data.FatherInfo.FatherWorkPlace = new FieldData<string> { Value = familyObj.sFatherWorkPlace };
                            data.FatherInfo.FatherPhone = new FieldData<string> { Value = familyObj.sFatherPhone };
                            data.FatherInfo.FatherPhone2 = new FieldData<string> { Value = familyObj.sFatherPhone2 };
                            data.FatherInfo.FatherPhone3 = new FieldData<string> { Value = familyObj.sFatherPhone3 };

                            // ข้อมูลมารดา
                            data.MotherInfo.MotherTitle = new FieldData<string> { Value = familyObj.sMotherTitle };
                            data.MotherInfo.MotherName = new FieldData<string> { Value = familyObj.sMotherFirstName };
                            data.MotherInfo.MotherLastName = new FieldData<string> { Value = familyObj.sMotherLastName };
                            data.MotherInfo.MotherNameEn = new FieldData<string> { Value = familyObj.sMotherNameEN };
                            data.MotherInfo.MotherNameLastEn = new FieldData<string> { Value = familyObj.sMotherLastEN };
                            data.MotherInfo.MotherBirthday = new FieldData<string> { Value = familyObj.dMotherBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) };
                            data.MotherInfo.MotherIdentification = new FieldData<string> { Value = familyObj.sMotherIdCardNumber };
                            data.MotherInfo.MotherRace = new FieldData<string> { Value = familyObj.sMotherRace };
                            data.MotherInfo.MotherNation = new FieldData<string> { Value = familyObj.sMotherNation };
                            data.MotherInfo.MotherReligion = new FieldData<string> { Value = familyObj.sMotherReligion };
                            data.MotherInfo.MotherGraduated = new FieldData<int?> { Value = familyObj.sMotherGraduated ?? 0 };
                            data.MotherInfo.IsFatherAddress = new FieldData<bool?> { Value = null };
                            data.MotherInfo.MotherHomeNo = new FieldData<string> { Value = familyObj.sMotherHomeNumber };
                            data.MotherInfo.MotherSoi = new FieldData<string> { Value = familyObj.sMotherSoy };
                            data.MotherInfo.MotherMoo = new FieldData<string> { Value = familyObj.sMotherMuu };
                            data.MotherInfo.MotherRoad = new FieldData<string> { Value = familyObj.sMotherRoad };
                            data.MotherInfo.MotherProvince = new FieldData<string> { Value = familyObj.sMotherProvince };
                            data.MotherInfo.MotherAmphoe = new FieldData<string> { Value = familyObj.sMotherAumpher };
                            data.MotherInfo.MotherTombon = new FieldData<string> { Value = familyObj.sMotherTumbon };
                            data.MotherInfo.MotherPostalCode = new FieldData<string> { Value = familyObj.sMotherPost };
                            data.MotherInfo.MotherJob = new FieldData<string> { Value = familyObj.sMotherJob };
                            data.MotherInfo.MotherIncome = new FieldData<double?> { Value = familyObj.nMotherIncome };
                            data.MotherInfo.MotherWorkPlace = new FieldData<string> { Value = familyObj.sMotherWorkPlace };
                            data.MotherInfo.MotherPhone = new FieldData<string> { Value = familyObj.sMotherPhone };
                            data.MotherInfo.MotherPhone2 = new FieldData<string> { Value = familyObj.sMotherPhone2 };
                            data.MotherInfo.MotherPhone3 = new FieldData<string> { Value = familyObj.sMotherPhone3 };

                            // ข้อมูลผู้ปกครอง
                            data.ParentInfo.CopyDataFrom = new FieldData<int?> { Value = null };
                            data.ParentInfo.ParentRelate = new FieldData<string> { Value = familyObj.sFamilyRelate };
                            data.ParentInfo.ParentTitle = new FieldData<string> { Value = familyObj.sFamilyTitle };
                            data.ParentInfo.ParentName = new FieldData<string> { Value = familyObj.sFamilyName };
                            data.ParentInfo.ParentLastName = new FieldData<string> { Value = familyObj.sFamilyLast };
                            data.ParentInfo.ParentNameEn = new FieldData<string> { Value = familyObj.sFamilyNameEN };
                            data.ParentInfo.ParentNameLastEn = new FieldData<string> { Value = familyObj.sFamilyLastEN };
                            data.ParentInfo.ParentBirthday = new FieldData<string> { Value = familyObj.dFamilyBirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH")) };
                            data.ParentInfo.ParentIdentification = new FieldData<string> { Value = familyObj.sFamilyIdCardNumber };
                            data.ParentInfo.ParentRace = new FieldData<string> { Value = familyObj.sFamilyRace };
                            data.ParentInfo.ParentNation = new FieldData<string> { Value = familyObj.sFamilyNation };
                            data.ParentInfo.ParentReligion = new FieldData<string> { Value = familyObj.sFamilyReligion };
                            data.ParentInfo.ParentGraduated = new FieldData<int?> { Value = familyObj.sFamilyGraduated ?? 0 };
                            data.ParentInfo.ParentHomeNo = new FieldData<string> { Value = familyObj.sFamilyHomeNumber };
                            data.ParentInfo.ParentSoi = new FieldData<string> { Value = familyObj.sFamilySoy };
                            data.ParentInfo.ParentMoo = new FieldData<string> { Value = familyObj.sFamilyMuu };
                            data.ParentInfo.ParentRoad = new FieldData<string> { Value = familyObj.sFamilyRoad };
                            data.ParentInfo.ParentProvince = new FieldData<string> { Value = familyObj.sFamilyProvince };
                            data.ParentInfo.ParentAmphoe = new FieldData<string> { Value = familyObj.sFamilyAumpher };
                            data.ParentInfo.ParentTombon = new FieldData<string> { Value = familyObj.sFamilyTumbon };
                            data.ParentInfo.ParentPostalCode = new FieldData<string> { Value = familyObj.sFamilyPost };
                            data.ParentInfo.TuitionReimbursement = new FieldData<int?> { Value = familyObj.nFamilyRequestStudyMoney };
                            data.ParentInfo.ParentStatus = new FieldData<int?> { Value = familyObj.familyStatus };
                            data.ParentInfo.ParentJob = new FieldData<string> { Value = familyObj.sFamilyJob };
                            data.ParentInfo.ParentIncome = new FieldData<double?> { Value = familyObj.nFamilyIncome };
                            data.ParentInfo.ParentWorkPlace = new FieldData<string> { Value = familyObj.sFamilyWorkPlace };
                            data.ParentInfo.ParentPhone = new FieldData<string> { Value = familyObj.sPhoneOne };
                            data.ParentInfo.ParentPhone2 = new FieldData<string> { Value = familyObj.sPhoneTwo };
                            data.ParentInfo.ParentPhone3 = new FieldData<string> { Value = familyObj.sPhoneThree };
                        }
                        else
                        {
                            data.Profile.StudentSonTotal = new FieldData<int?>();
                            data.Profile.StudentBrethrenStudyHere = new FieldData<int?>();

                            // ที่อยู่ตามทะเบียนบ้าน
                            data.PermanentAddress.RegisterHomeCode = new FieldData<string>();
                            data.PermanentAddress.RegisterHomeNo = new FieldData<string>();
                            data.PermanentAddress.RegisterHomeSoi = new FieldData<string>();
                            data.PermanentAddress.RegisterHomeMoo = new FieldData<string>();
                            data.PermanentAddress.RegisterHomeRoad = new FieldData<string>();
                            data.PermanentAddress.RegisterHomeProvince = new FieldData<int?>();
                            data.PermanentAddress.RegisterHomeAmphoe = new FieldData<int?>();
                            data.PermanentAddress.RegisterHomeTombon = new FieldData<int?>();
                            data.PermanentAddress.RegisterHomePostalCode = new FieldData<string>();
                            data.PermanentAddress.RegisterHomePhone = new FieldData<string>();
                            data.PermanentAddress.BornFrom = new FieldData<string>();
                            data.PermanentAddress.BornFromProvince = new FieldData<int?>();
                            data.PermanentAddress.BornFromAmphoe = new FieldData<int?>();
                            data.PermanentAddress.BornFromTombon = new FieldData<int?>();

                            // ที่อยู่ปัจจุบัน
                            data.ContactAddress.UseHouseAddress = new FieldData<bool> { Value = false };
                            data.ContactAddress.HomeNo = new FieldData<string>();
                            data.ContactAddress.HomeSoi = new FieldData<string>();
                            data.ContactAddress.HomeMoo = new FieldData<string>();
                            data.ContactAddress.HomeRoad = new FieldData<string>();
                            data.ContactAddress.HomeProvince = new FieldData<string>();
                            data.ContactAddress.HomeAmphoe = new FieldData<string>();
                            data.ContactAddress.HomeTombon = new FieldData<string>();
                            data.ContactAddress.HomePostalCode = new FieldData<string>();
                            data.ContactAddress.HomePhone = new FieldData<string>();

                            data.ContactAddress.HomeStayWithTitle = new FieldData<int?>();
                            data.ContactAddress.HomeStayWithName = new FieldData<string>();
                            data.ContactAddress.HomeStayWithLast = new FieldData<string>();
                            data.ContactAddress.HomeStayWithEmergencyCall = new FieldData<string>();
                            data.ContactAddress.HomeStayWithEmergencyEmail = new FieldData<string>();
                            data.ContactAddress.HomeFriendName = new FieldData<string>();
                            data.ContactAddress.HomeFriendLastName = new FieldData<string>();
                            data.ContactAddress.HomeFriendPhone = new FieldData<string>();
                            data.ContactAddress.HomeHomeType = new FieldData<int?>();

                            // ข้อมูลบิดา
                            data.FatherInfo.FatherTitle = new FieldData<string>();
                            data.FatherInfo.FatherName = new FieldData<string>();
                            data.FatherInfo.FatherLastName = new FieldData<string>();
                            data.FatherInfo.FatherNameEn = new FieldData<string>();
                            data.FatherInfo.FatherNameLastEn = new FieldData<string>();
                            data.FatherInfo.FatherBirthday = new FieldData<string>();
                            data.FatherInfo.FatherIdentification = new FieldData<string>();
                            data.FatherInfo.FatherRace = new FieldData<string>();
                            data.FatherInfo.FatherNation = new FieldData<string>();
                            data.FatherInfo.FatherReligion = new FieldData<string>();
                            data.FatherInfo.FatherGraduated = new FieldData<int?>();
                            data.FatherInfo.FatherHomeNo = new FieldData<string>();
                            data.FatherInfo.FatherSoi = new FieldData<string>();
                            data.FatherInfo.FatherMoo = new FieldData<string>();
                            data.FatherInfo.FatherRoad = new FieldData<string>();
                            data.FatherInfo.FatherProvince = new FieldData<string>();
                            data.FatherInfo.FatherAmphoe = new FieldData<string>();
                            data.FatherInfo.FatherTombon = new FieldData<string>();
                            data.FatherInfo.FatherPostalCode = new FieldData<string>();
                            data.FatherInfo.FatherJob = new FieldData<string>();
                            data.FatherInfo.FatherIncome = new FieldData<double?>();
                            data.FatherInfo.FatherWorkPlace = new FieldData<string>();
                            data.FatherInfo.FatherPhone = new FieldData<string>();
                            data.FatherInfo.FatherPhone2 = new FieldData<string>();
                            data.FatherInfo.FatherPhone3 = new FieldData<string>();

                            // ข้อมูลมารดา
                            data.MotherInfo.MotherTitle = new FieldData<string>();
                            data.MotherInfo.MotherName = new FieldData<string>();
                            data.MotherInfo.MotherLastName = new FieldData<string>();
                            data.MotherInfo.MotherNameEn = new FieldData<string>();
                            data.MotherInfo.MotherNameLastEn = new FieldData<string>();
                            data.MotherInfo.MotherBirthday = new FieldData<string>();
                            data.MotherInfo.MotherIdentification = new FieldData<string>();
                            data.MotherInfo.MotherRace = new FieldData<string>();
                            data.MotherInfo.MotherNation = new FieldData<string>();
                            data.MotherInfo.MotherReligion = new FieldData<string>();
                            data.MotherInfo.MotherGraduated = new FieldData<int?>();
                            data.MotherInfo.IsFatherAddress = new FieldData<bool?>();
                            data.MotherInfo.MotherHomeNo = new FieldData<string>();
                            data.MotherInfo.MotherSoi = new FieldData<string>();
                            data.MotherInfo.MotherMoo = new FieldData<string>();
                            data.MotherInfo.MotherRoad = new FieldData<string>();
                            data.MotherInfo.MotherProvince = new FieldData<string>();
                            data.MotherInfo.MotherAmphoe = new FieldData<string>();
                            data.MotherInfo.MotherTombon = new FieldData<string>();
                            data.MotherInfo.MotherPostalCode = new FieldData<string>();
                            data.MotherInfo.MotherJob = new FieldData<string>();
                            data.MotherInfo.MotherIncome = new FieldData<double?>();
                            data.MotherInfo.MotherWorkPlace = new FieldData<string>();
                            data.MotherInfo.MotherPhone = new FieldData<string>();
                            data.MotherInfo.MotherPhone2 = new FieldData<string>();
                            data.MotherInfo.MotherPhone3 = new FieldData<string>();

                            // ข้อมูลผู้ปกครอง
                            data.ParentInfo.CopyDataFrom = new FieldData<int?>();
                            data.ParentInfo.ParentRelate = new FieldData<string>();
                            data.ParentInfo.ParentTitle = new FieldData<string>();
                            data.ParentInfo.ParentName = new FieldData<string>();
                            data.ParentInfo.ParentLastName = new FieldData<string>();
                            data.ParentInfo.ParentNameEn = new FieldData<string>();
                            data.ParentInfo.ParentNameLastEn = new FieldData<string>();
                            data.ParentInfo.ParentBirthday = new FieldData<string>();
                            data.ParentInfo.ParentIdentification = new FieldData<string>();
                            data.ParentInfo.ParentRace = new FieldData<string>();
                            data.ParentInfo.ParentNation = new FieldData<string>();
                            data.ParentInfo.ParentReligion = new FieldData<string>();
                            data.ParentInfo.ParentGraduated = new FieldData<int?>();
                            data.ParentInfo.ParentHomeNo = new FieldData<string>();
                            data.ParentInfo.ParentSoi = new FieldData<string>();
                            data.ParentInfo.ParentMoo = new FieldData<string>();
                            data.ParentInfo.ParentRoad = new FieldData<string>();
                            data.ParentInfo.ParentProvince = new FieldData<string>();
                            data.ParentInfo.ParentAmphoe = new FieldData<string>();
                            data.ParentInfo.ParentTombon = new FieldData<string>();
                            data.ParentInfo.ParentPostalCode = new FieldData<string>();
                            data.ParentInfo.TuitionReimbursement = new FieldData<int?>();
                            data.ParentInfo.ParentStatus = new FieldData<int?>();
                            data.ParentInfo.ParentJob = new FieldData<string>();
                            data.ParentInfo.ParentIncome = new FieldData<double?>();
                            data.ParentInfo.ParentWorkPlace = new FieldData<string>();
                            data.ParentInfo.ParentPhone = new FieldData<string>();
                            data.ParentInfo.ParentPhone2 = new FieldData<string>();
                            data.ParentInfo.ParentPhone3 = new FieldData<string>();
                        }

                        // ประวัติการศึกษา
                        data.Education.OldSchoolName = new FieldData<string> { Value = userObj.oldSchoolName };

                        var oldSchoolLocation = "";

                        var provinceID = string.IsNullOrEmpty(userObj.oldSchoolProvince) ? 0 : int.Parse(userObj.oldSchoolProvince);
                        var provinceObj = mctx.provinces.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            oldSchoolLocation = " จังหวัด " + provinceObj.PROVINCE_NAME;
                        }

                        var aumpherID = string.IsNullOrEmpty(userObj.oldSchoolAumpher) ? 0 : int.Parse(userObj.oldSchoolAumpher);
                        var aumpherObj = mctx.amphurs.Where(w => w.PROVINCE_ID == provinceID && w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            oldSchoolLocation = " เขต/อำเภอ " + aumpherObj.AMPHUR_NAME + oldSchoolLocation;
                        }

                        var tumbonID = string.IsNullOrEmpty(userObj.oldSchoolTumbon) ? 0 : int.Parse(userObj.oldSchoolTumbon);
                        var tumbonObj = mctx.districts.Where(w => w.AMPHUR_ID == aumpherID && w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            oldSchoolLocation = "แขวง/ตำบล " + tumbonObj.DISTRICT_NAME + oldSchoolLocation;
                        }

                        data.Education.OldSchoolLocation = new FieldData<string> { Value = oldSchoolLocation };
                        data.Education.OldSchoolGPA = new FieldData<string> { Value = userObj.oldSchoolGPA2 };
                        data.Education.Credit = new FieldData<string> { Value = userObj.Credit?.ToString("0.00") };
                        data.Education.OldSchoolGraduated = new FieldData<string> { Value = StdEducation.GetNameOldSchoolGraduated(userObj.oldSchoolGraduated) };
                        data.Education.MoveOutReason = new FieldData<string> { Value = userObj.moveOutReason };
                    }

                    var bRequestDate = DateTime.TryParseExact(requestDate, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out DateTime dRequestDate);
                    if (bRequestDate)
                    {
                        // Prepare on section: [profile, permanent-address, contact-address, father-info, mother-info, parent-info]
                        // ประวัติส่วนตัว
                        var profileObj = ctx.TApproveStudentProfiles.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (profileObj != null)
                        {
                            data.Profile.ID = profileObj.ID;
                            data.Profile.SID = profileObj.StudentID;
                            if (!string.IsNullOrEmpty(profileObj.Gender)) data.Profile.StudentGender.NewValue = profileObj.Gender;
                            if (profileObj.Title != null) data.Profile.StudentTitle.NewValue = profileObj.Title;
                            if (!string.IsNullOrEmpty(profileObj.Name)) data.Profile.StudentFirstNameTh.NewValue = profileObj.Name;
                            if (!string.IsNullOrEmpty(profileObj.Surname)) data.Profile.StudentLastNameTh.NewValue = profileObj.Surname;
                            if (!string.IsNullOrEmpty(profileObj.NameEn)) data.Profile.StudentFirstNameEn.NewValue = profileObj.NameEn;
                            if (!string.IsNullOrEmpty(profileObj.SurnameEn)) data.Profile.StudentLastNameEn.NewValue = profileObj.SurnameEn;
                            if (!string.IsNullOrEmpty(profileObj.NameOther)) data.Profile.StudentFirstNameOther.NewValue = profileObj.NameOther;
                            if (!string.IsNullOrEmpty(profileObj.SurnameOther)) data.Profile.StudentLastNameOther.NewValue = profileObj.SurnameOther;
                            if (!string.IsNullOrEmpty(profileObj.Nickname)) data.Profile.StudentNickNameTh.NewValue = profileObj.Nickname;
                            if (!string.IsNullOrEmpty(profileObj.NicknameEn)) data.Profile.StudentNickNameEn.NewValue = profileObj.NicknameEn;
                            if (profileObj.BirthDay != null) data.Profile.StudentBirthday.NewValue = profileObj.BirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            if (!string.IsNullOrEmpty(profileObj.Race)) data.Profile.StudentRace.NewValue = profileObj.Race;
                            if (!string.IsNullOrEmpty(profileObj.Nationality)) data.Profile.StudentNation.NewValue = profileObj.Nationality;
                            if (!string.IsNullOrEmpty(profileObj.Religion)) data.Profile.StudentReligion.NewValue = profileObj.Religion;
                            if (!string.IsNullOrEmpty(profileObj.Disability)) data.Profile.DisabilityCode.NewValue = profileObj.Disability;
                            if (!string.IsNullOrEmpty(profileObj.Disadvantaged)) data.Profile.DisadvantageCode.NewValue = profileObj.Disadvantaged;
                            if (!string.IsNullOrEmpty(profileObj.PhoneNumber)) data.Profile.StudentPhone.NewValue = profileObj.PhoneNumber;
                            if (!string.IsNullOrEmpty(profileObj.Email)) data.Profile.StudentEmail.NewValue = profileObj.Email;
                            if (profileObj.NumberMemberInFamily != null) data.Profile.StudentSonTotal.NewValue = profileObj.NumberMemberInFamily;
                            if (profileObj.YouAreChildOfFamily != null) data.Profile.StudentSonNumber.NewValue = profileObj.YouAreChildOfFamily;
                            if (profileObj.HaveBrotherStudyInSchool != null) data.Profile.StudentBrethrenStudyHere.NewValue = profileObj.HaveBrotherStudyInSchool;
                            if (!string.IsNullOrEmpty(profileObj.Other)) data.Profile.Note2.NewValue = profileObj.Other;

                            data.Profile.ApproveStatus = profileObj.ApproveStatus;
                            data.Profile.RequestUpdateData = true;
                            data.Profile.DataChanged = GetDataChanged<Profile>(data.Profile);
                        }

                        // ที่อยู่ตามทะเบียนบ้าน
                        var permanentObj = ctx.TApproveStudentPermanentAddresses.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (permanentObj != null)
                        {
                            data.PermanentAddress.ID = permanentObj.ID;
                            data.PermanentAddress.SID = permanentObj.StudentID;
                            if (!string.IsNullOrEmpty(permanentObj.HouseCode)) data.PermanentAddress.RegisterHomeCode.NewValue = permanentObj.HouseCode;
                            if (!string.IsNullOrEmpty(permanentObj.HouseNo)) data.PermanentAddress.RegisterHomeNo.NewValue = permanentObj.HouseNo;
                            if (!string.IsNullOrEmpty(permanentObj.Soi)) data.PermanentAddress.RegisterHomeSoi.NewValue = permanentObj.Soi;
                            if (!string.IsNullOrEmpty(permanentObj.Moo)) data.PermanentAddress.RegisterHomeMoo.NewValue = permanentObj.Moo;
                            if (!string.IsNullOrEmpty(permanentObj.Road)) data.PermanentAddress.RegisterHomeRoad.NewValue = permanentObj.Road;
                            if (permanentObj.Province != null) data.PermanentAddress.RegisterHomeProvince.NewValue = permanentObj.Province;
                            if (permanentObj.District != null) data.PermanentAddress.RegisterHomeAmphoe.NewValue = permanentObj.District;
                            if (permanentObj.SubDistrict != null) data.PermanentAddress.RegisterHomeTombon.NewValue = permanentObj.SubDistrict;
                            if (!string.IsNullOrEmpty(permanentObj.PostalCode)) data.PermanentAddress.RegisterHomePostalCode.NewValue = permanentObj.PostalCode;
                            if (!string.IsNullOrEmpty(permanentObj.HomePhoneNumber)) data.PermanentAddress.RegisterHomePhone.NewValue = permanentObj.HomePhoneNumber;
                            if (!string.IsNullOrEmpty(permanentObj.BirthPlace)) data.PermanentAddress.BornFrom.NewValue = permanentObj.BirthPlace;
                            if (permanentObj.BirthPlaceProvince != null) data.PermanentAddress.BornFromProvince.NewValue = permanentObj.BirthPlaceProvince;
                            if (permanentObj.BirthPlaceDistrict != null) data.PermanentAddress.BornFromAmphoe.NewValue = permanentObj.BirthPlaceDistrict;
                            if (permanentObj.BirthPlaceSubDistrict != null) data.PermanentAddress.BornFromTombon.NewValue = permanentObj.BirthPlaceSubDistrict;

                            data.PermanentAddress.ApproveStatus = permanentObj.ApproveStatus;
                            data.PermanentAddress.RequestUpdateData = true;
                            data.PermanentAddress.DataChanged = GetDataChanged<PermanentAddress>(data.PermanentAddress);
                        }

                        // ที่อยู่ปัจจุบัน
                        var contactObj = ctx.TApproveStudentContactAddresses.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (contactObj != null)
                        {
                            data.ContactAddress.ID = contactObj.ID;
                            data.ContactAddress.SID = contactObj.StudentID;
                            if (contactObj.IsPermanentAddress != null) data.ContactAddress.UseHouseAddress.NewValue = (bool)contactObj.IsPermanentAddress;
                            if (!string.IsNullOrEmpty(contactObj.HouseNo)) data.ContactAddress.HomeNo.NewValue = contactObj.HouseNo;
                            if (!string.IsNullOrEmpty(contactObj.Soi)) data.ContactAddress.HomeSoi.NewValue = contactObj.Soi;
                            if (!string.IsNullOrEmpty(contactObj.Moo)) data.ContactAddress.HomeMoo.NewValue = contactObj.Moo;
                            if (!string.IsNullOrEmpty(contactObj.Road)) data.ContactAddress.HomeRoad.NewValue = contactObj.Road;
                            if (contactObj.Province != null) data.ContactAddress.HomeProvince.NewValue = contactObj.Province?.ToString();
                            if (contactObj.District != null) data.ContactAddress.HomeAmphoe.NewValue = contactObj.District?.ToString();
                            if (contactObj.SubDistrict != null) data.ContactAddress.HomeTombon.NewValue = contactObj.SubDistrict?.ToString();
                            if (!string.IsNullOrEmpty(contactObj.PostalCode)) data.ContactAddress.HomePostalCode.NewValue = contactObj.PostalCode;
                            if (!string.IsNullOrEmpty(contactObj.HousePhone)) data.ContactAddress.HomePhone.NewValue = contactObj.HousePhone;

                            if (contactObj.LiveWithTitle != null) data.ContactAddress.HomeStayWithTitle.NewValue = contactObj.LiveWithTitle;
                            if (!string.IsNullOrEmpty(contactObj.LiveWithName)) data.ContactAddress.HomeStayWithName.NewValue = contactObj.LiveWithName;
                            if (!string.IsNullOrEmpty(contactObj.LiveWithSurname)) data.ContactAddress.HomeStayWithLast.NewValue = contactObj.LiveWithSurname;
                            if (!string.IsNullOrEmpty(contactObj.EmergencyPhone)) data.ContactAddress.HomeStayWithEmergencyCall.NewValue = contactObj.EmergencyPhone;
                            if (!string.IsNullOrEmpty(contactObj.LiveWithEmail)) data.ContactAddress.HomeStayWithEmergencyEmail.NewValue = contactObj.LiveWithEmail;
                            if (!string.IsNullOrEmpty(contactObj.NeighborName)) data.ContactAddress.HomeFriendName.NewValue = contactObj.NeighborName;
                            if (!string.IsNullOrEmpty(contactObj.NeighborSurname)) data.ContactAddress.HomeFriendLastName.NewValue = contactObj.NeighborSurname;
                            if (!string.IsNullOrEmpty(contactObj.NeighborPhone)) data.ContactAddress.HomeFriendPhone.NewValue = contactObj.NeighborPhone;
                            if (contactObj.HouseStyle != null) data.ContactAddress.HomeHomeType.NewValue = contactObj.HouseStyle;

                            data.ContactAddress.ApproveStatus = contactObj.ApproveStatus;
                            data.ContactAddress.RequestUpdateData = true;
                            data.ContactAddress.DataChanged = GetDataChanged<ContactAddress>(data.ContactAddress);
                        }

                        // ประวัติการศึกษา

                        // ข้อมูลบิดา
                        var fatherObj = ctx.TApproveStudentFatherInfoes.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (fatherObj != null)
                        {
                            data.FatherInfo.ID = fatherObj.ID;
                            data.FatherInfo.SID = fatherObj.StudentID;
                            if (fatherObj.Title != null) data.FatherInfo.FatherTitle.NewValue = fatherObj.Title?.ToString();
                            if (!string.IsNullOrEmpty(fatherObj.Name)) data.FatherInfo.FatherName.NewValue = fatherObj.Name;
                            if (!string.IsNullOrEmpty(fatherObj.Surname)) data.FatherInfo.FatherLastName.NewValue = fatherObj.Surname;
                            if (!string.IsNullOrEmpty(fatherObj.NameEn)) data.FatherInfo.FatherNameEn.NewValue = fatherObj.NameEn;
                            if (!string.IsNullOrEmpty(fatherObj.SurnameEn)) data.FatherInfo.FatherNameLastEn.NewValue = fatherObj.SurnameEn;
                            if (fatherObj.BirthDay != null) data.FatherInfo.FatherBirthday.NewValue = fatherObj.BirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            if (!string.IsNullOrEmpty(fatherObj.IDCardNumber)) data.FatherInfo.FatherIdentification.NewValue = fatherObj.IDCardNumber;
                            if (!string.IsNullOrEmpty(fatherObj.Race)) data.FatherInfo.FatherRace.NewValue = fatherObj.Race;
                            if (!string.IsNullOrEmpty(fatherObj.Nationality)) data.FatherInfo.FatherNation.NewValue = fatherObj.Nationality;
                            if (!string.IsNullOrEmpty(fatherObj.Religion)) data.FatherInfo.FatherReligion.NewValue = fatherObj.Religion;
                            if (fatherObj.Education != null) data.FatherInfo.FatherGraduated.NewValue = fatherObj.Education;
                            if (!string.IsNullOrEmpty(fatherObj.HouseNo)) data.FatherInfo.FatherHomeNo.NewValue = fatherObj.HouseNo;
                            if (!string.IsNullOrEmpty(fatherObj.Soi)) data.FatherInfo.FatherSoi.NewValue = fatherObj.Soi;
                            if (!string.IsNullOrEmpty(fatherObj.Moo)) data.FatherInfo.FatherMoo.NewValue = fatherObj.Moo;
                            if (!string.IsNullOrEmpty(fatherObj.Road)) data.FatherInfo.FatherRoad.NewValue = fatherObj.Road;
                            if (fatherObj.Province != null) data.FatherInfo.FatherProvince.NewValue = fatherObj.Province?.ToString();
                            if (fatherObj.District != null) data.FatherInfo.FatherAmphoe.NewValue = fatherObj.District?.ToString();
                            if (fatherObj.SubDistrict != null) data.FatherInfo.FatherTombon.NewValue = fatherObj.SubDistrict?.ToString();
                            if (!string.IsNullOrEmpty(fatherObj.PostalCode)) data.FatherInfo.FatherPostalCode.NewValue = fatherObj.PostalCode;
                            if (!string.IsNullOrEmpty(fatherObj.Career)) data.FatherInfo.FatherJob.NewValue = fatherObj.Career;
                            if (fatherObj.MonthlyIncome != null) data.FatherInfo.FatherIncome.NewValue = fatherObj.MonthlyIncome;
                            if (!string.IsNullOrEmpty(fatherObj.WorkPlaces)) data.FatherInfo.FatherWorkPlace.NewValue = fatherObj.WorkPlaces;
                            if (!string.IsNullOrEmpty(fatherObj.PhoneNumberHouse)) data.FatherInfo.FatherPhone.NewValue = fatherObj.PhoneNumberHouse;
                            if (!string.IsNullOrEmpty(fatherObj.PhoneNumberMobile)) data.FatherInfo.FatherPhone2.NewValue = fatherObj.PhoneNumberMobile;
                            if (!string.IsNullOrEmpty(fatherObj.PhoneNumberWorkPlace)) data.FatherInfo.FatherPhone3.NewValue = fatherObj.PhoneNumberWorkPlace;

                            data.FatherInfo.ApproveStatus = fatherObj.ApproveStatus;
                            data.FatherInfo.RequestUpdateData = true;
                            data.FatherInfo.DataChanged = GetDataChanged<FatherInfo>(data.FatherInfo);
                        }

                        // ข้อมูลมารดา
                        var motherObj = ctx.TApproveStudentMotherInfoes.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (motherObj != null)
                        {
                            data.MotherInfo.ID = motherObj.ID;
                            data.MotherInfo.SID = motherObj.StudentID;
                            if (motherObj.Title != null) data.MotherInfo.MotherTitle.NewValue = motherObj.Title?.ToString();
                            if (!string.IsNullOrEmpty(motherObj.Name)) data.MotherInfo.MotherName.NewValue = motherObj.Name;
                            if (!string.IsNullOrEmpty(motherObj.Surname)) data.MotherInfo.MotherLastName.NewValue = motherObj.Surname;
                            if (!string.IsNullOrEmpty(motherObj.NameEn)) data.MotherInfo.MotherNameEn.NewValue = motherObj.NameEn;
                            if (!string.IsNullOrEmpty(motherObj.SurnameEn)) data.MotherInfo.MotherNameLastEn.NewValue = motherObj.SurnameEn;
                            if (motherObj.BirthDay != null) data.MotherInfo.MotherBirthday.NewValue = motherObj.BirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            if (!string.IsNullOrEmpty(motherObj.IDCardNumber)) data.MotherInfo.MotherIdentification.NewValue = motherObj.IDCardNumber;
                            if (!string.IsNullOrEmpty(motherObj.Race)) data.MotherInfo.MotherRace.NewValue = motherObj.Race;
                            if (!string.IsNullOrEmpty(motherObj.Nationality)) data.MotherInfo.MotherNation.NewValue = motherObj.Nationality;
                            if (!string.IsNullOrEmpty(motherObj.Religion)) data.MotherInfo.MotherReligion.NewValue = motherObj.Religion;
                            if (motherObj.Education != null) data.MotherInfo.MotherGraduated.NewValue = motherObj.Education;
                            if (motherObj.IsFatherAddress != null) data.MotherInfo.IsFatherAddress.NewValue = motherObj.IsFatherAddress;
                            if (!string.IsNullOrEmpty(motherObj.HouseNo)) data.MotherInfo.MotherHomeNo.NewValue = motherObj.HouseNo;
                            if (!string.IsNullOrEmpty(motherObj.Soi)) data.MotherInfo.MotherSoi.NewValue = motherObj.Soi;
                            if (!string.IsNullOrEmpty(motherObj.Moo)) data.MotherInfo.MotherMoo.NewValue = motherObj.Moo;
                            if (!string.IsNullOrEmpty(motherObj.Road)) data.MotherInfo.MotherRoad.NewValue = motherObj.Road;
                            if (motherObj.Province != null) data.MotherInfo.MotherProvince.NewValue = motherObj.Province?.ToString();
                            if (motherObj.District != null) data.MotherInfo.MotherAmphoe.NewValue = motherObj.District?.ToString();
                            if (motherObj.SubDistrict != null) data.MotherInfo.MotherTombon.NewValue = motherObj.SubDistrict?.ToString();
                            if (!string.IsNullOrEmpty(motherObj.PostalCode)) data.MotherInfo.MotherPostalCode.NewValue = motherObj.PostalCode;
                            if (!string.IsNullOrEmpty(motherObj.Career)) data.MotherInfo.MotherJob.NewValue = motherObj.Career;
                            if (motherObj.MonthlyIncome != null) data.MotherInfo.MotherIncome.NewValue = motherObj.MonthlyIncome;
                            if (!string.IsNullOrEmpty(motherObj.WorkPlaces)) data.MotherInfo.MotherWorkPlace.NewValue = motherObj.WorkPlaces;
                            if (!string.IsNullOrEmpty(motherObj.PhoneNumberHouse)) data.MotherInfo.MotherPhone.NewValue = motherObj.PhoneNumberHouse;
                            if (!string.IsNullOrEmpty(motherObj.PhoneNumberMobile)) data.MotherInfo.MotherPhone2.NewValue = motherObj.PhoneNumberMobile;
                            if (!string.IsNullOrEmpty(motherObj.PhoneNumberWorkPlace)) data.MotherInfo.MotherPhone3.NewValue = motherObj.PhoneNumberWorkPlace;

                            data.MotherInfo.ApproveStatus = motherObj.ApproveStatus;
                            data.MotherInfo.RequestUpdateData = true;
                            data.MotherInfo.DataChanged = GetDataChanged<MotherInfo>(data.MotherInfo);
                        }

                        // ข้อมูลผู้ปกครอง
                        var parentObj = ctx.TApproveStudentParentInfoes.Where(w => w.StudentID == sid && w.SchoolID == schoolID && EntityFunctions.TruncateTime(w.RequestApproveDate) == EntityFunctions.TruncateTime(dRequestDate)).OrderByDescending(o => o.RequestApproveDate).FirstOrDefault();
                        if (parentObj != null)
                        {
                            data.ParentInfo.ID = parentObj.ID;
                            data.ParentInfo.SID = parentObj.StudentID;
                            if (parentObj.CopyFrom != null) data.ParentInfo.CopyDataFrom.NewValue = parentObj.CopyFrom;
                            if (!string.IsNullOrEmpty(parentObj.Relationship)) data.ParentInfo.ParentRelate.NewValue = parentObj.Relationship;
                            if (parentObj.Title != null) data.ParentInfo.ParentTitle.NewValue = parentObj.Title?.ToString();
                            if (!string.IsNullOrEmpty(parentObj.Name)) data.ParentInfo.ParentName.NewValue = parentObj.Name;
                            if (!string.IsNullOrEmpty(parentObj.Surname)) data.ParentInfo.ParentLastName.NewValue = parentObj.Surname;
                            if (!string.IsNullOrEmpty(parentObj.NameEn)) data.ParentInfo.ParentNameEn.NewValue = parentObj.NameEn;
                            if (!string.IsNullOrEmpty(parentObj.SurnameEn)) data.ParentInfo.ParentNameLastEn.NewValue = parentObj.SurnameEn;
                            if (parentObj.BirthDay != null) data.ParentInfo.ParentBirthday.NewValue = parentObj.BirthDay?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            if (!string.IsNullOrEmpty(parentObj.IDCardNumber)) data.ParentInfo.ParentIdentification.NewValue = parentObj.IDCardNumber;
                            if (!string.IsNullOrEmpty(parentObj.Race)) data.ParentInfo.ParentRace.NewValue = parentObj.Race;
                            if (!string.IsNullOrEmpty(parentObj.Nationality)) data.ParentInfo.ParentNation.NewValue = parentObj.Nationality;
                            if (!string.IsNullOrEmpty(parentObj.Religion)) data.ParentInfo.ParentReligion.NewValue = parentObj.Religion;
                            if (parentObj.Education != null) data.ParentInfo.ParentGraduated.NewValue = parentObj.Education;
                            if (!string.IsNullOrEmpty(parentObj.HouseNo)) data.ParentInfo.ParentHomeNo.NewValue = parentObj.HouseNo;
                            if (!string.IsNullOrEmpty(parentObj.Soi)) data.ParentInfo.ParentSoi.NewValue = parentObj.Soi;
                            if (!string.IsNullOrEmpty(parentObj.Moo)) data.ParentInfo.ParentMoo.NewValue = parentObj.Moo;
                            if (!string.IsNullOrEmpty(parentObj.Road)) data.ParentInfo.ParentRoad.NewValue = parentObj.Road;
                            if (parentObj.Province != null) data.ParentInfo.ParentProvince.NewValue = parentObj.Province?.ToString();
                            if (parentObj.District != null) data.ParentInfo.ParentAmphoe.NewValue = parentObj.District?.ToString();
                            if (parentObj.SubDistrict != null) data.ParentInfo.ParentTombon.NewValue = parentObj.SubDistrict?.ToString();
                            if (!string.IsNullOrEmpty(parentObj.PostalCode)) data.ParentInfo.ParentPostalCode.NewValue = parentObj.PostalCode;
                            if (parentObj.TuitionFee != null) data.ParentInfo.TuitionReimbursement.NewValue = parentObj.TuitionFee;
                            if (parentObj.FamilyStatus != null) data.ParentInfo.ParentStatus.NewValue = parentObj.FamilyStatus;
                            if (!string.IsNullOrEmpty(parentObj.Career)) data.ParentInfo.ParentJob.NewValue = parentObj.Career;
                            if (parentObj.MonthlyIncome != null) data.ParentInfo.ParentIncome.NewValue = parentObj.MonthlyIncome;
                            if (!string.IsNullOrEmpty(parentObj.WorkPlaces)) data.ParentInfo.ParentWorkPlace.NewValue = parentObj.WorkPlaces;
                            if (!string.IsNullOrEmpty(parentObj.PhoneNumberHouse)) data.ParentInfo.ParentPhone.NewValue = parentObj.PhoneNumberHouse;
                            if (!string.IsNullOrEmpty(parentObj.PhoneNumberMobile)) data.ParentInfo.ParentPhone2.NewValue = parentObj.PhoneNumberMobile;
                            if (!string.IsNullOrEmpty(parentObj.PhoneNumberWorkPlace)) data.ParentInfo.ParentPhone3.NewValue = parentObj.PhoneNumberWorkPlace;

                            data.ParentInfo.ApproveStatus = parentObj.ApproveStatus;
                            data.ParentInfo.RequestUpdateData = true;
                            data.ParentInfo.DataChanged = GetDataChanged<ParentInfo>(data.ParentInfo);
                        }
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, code, message, data });
        }

        [WebMethod]
        public static object SaveApproveAll(ProfileEdited profileEdited, PermanentAddressEdited permanentAddressEdited, ContactAddressEdited contactAddressEdited, FatherInfoEdited fatherInfoEdited, MotherInfoEdited motherInfoEdited, ParentInfoEdited parentInfoEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string section = "";
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                   
                        try
                        {
                            if (profileEdited != null)
                            {
                                section = "ประวัติส่วนตัว";

                                string recoveryScript = "";

                                // Approve Data
                                MasterEntity.TUser mUserObj = mctx.TUsers.Where(w => w.nCompany == schoolID && w.sID == profileEdited.StudentID).FirstOrDefault();
                                if (mUserObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiMasterSingleDB.dbo.TUser SET cSex='{mUserObj.cSex}', sName='{mUserObj.sName}', sLastname='{mUserObj.sLastname}', dBirth='{mUserObj.dBirth?.ToString("yyyy-MM-dd HH:mm:ss.fff")}', sPhone='{mUserObj.sPhone}', sEmail='{mUserObj.sEmail}' WHERE nCompany={mUserObj.nCompany} AND sID={mUserObj.sID};") + Environment.NewLine;

                                    mUserObj.cSex = profileEdited.StudentGender;
                                    mUserObj.sName = profileEdited.StudentFirstNameTh;
                                    mUserObj.sLastname = profileEdited.StudentLastNameTh;
                                    mUserObj.dBirth = string.IsNullOrEmpty(profileEdited.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(profileEdited.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                    mUserObj.sPhone = profileEdited.StudentPhone;
                                    mUserObj.sEmail = profileEdited.StudentEmail;

                                    mUserObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                                }

                                mctx.SaveChanges();

                                JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == profileEdited.StudentID).FirstOrDefault();
                                if (userObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET cSex='{userObj.cSex}', sStudentTitle='{userObj.sStudentTitle}', sName='{userObj.sName}', sLastname='{userObj.sLastname}', sStudentNameEN='{userObj.sStudentNameEN}', sStudentLastEN='{userObj.sStudentLastEN}', sStudentNameOther='{userObj.sStudentNameOther}', sStudentLastOther='{userObj.sStudentLastOther}', sNickName='{userObj.sNickName}', sNickNameEN='{userObj.sNickNameEN}', dBirth='{userObj.dBirth?.ToString("yyyy-MM-dd HH:mm:ss.fff")}', sStudentRace='{userObj.sStudentRace}', sStudentNation='{userObj.sStudentNation}', sStudentReligion='{userObj.sStudentReligion}', DisabilityCode='{userObj.DisabilityCode}', DisadvantageCode='{userObj.DisadvantageCode}', sPhone='{userObj.sPhone}', sEmail='{userObj.sEmail}', nSonNumber={(userObj.nSonNumber == null ? "NULL" : userObj.nSonNumber.ToString())}, Note2='{userObj.Note2}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                    userObj.cSex = profileEdited.StudentGender;
                                    userObj.sStudentTitle = profileEdited.StudentTitle?.ToString();
                                    userObj.sName = profileEdited.StudentFirstNameTh;
                                    userObj.sLastname = profileEdited.StudentLastNameTh;
                                    userObj.sStudentNameEN = profileEdited.StudentFirstNameEn;
                                    userObj.sStudentLastEN = profileEdited.StudentLastNameEn;
                                    userObj.sStudentNameOther = profileEdited.StudentFirstNameOther;
                                    userObj.sStudentLastOther = profileEdited.StudentLastNameOther;
                                    userObj.sNickName = profileEdited.StudentNickNameTh;
                                    userObj.sNickNameEN = profileEdited.StudentNickNameEn;
                                    userObj.dBirth = string.IsNullOrEmpty(profileEdited.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(profileEdited.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                    userObj.sStudentRace = profileEdited.StudentRace;
                                    userObj.sStudentNation = profileEdited.StudentNation;
                                    userObj.sStudentReligion = profileEdited.StudentReligion;
                                    userObj.DisabilityCode = profileEdited.DisabilityCode;
                                    userObj.DisadvantageCode = profileEdited.DisadvantageCode;
                                    userObj.sPhone = profileEdited.StudentPhone;
                                    userObj.sEmail = profileEdited.StudentEmail;
                                    userObj.nSonNumber = profileEdited.StudentSonNumber;
                                    userObj.Note2 = profileEdited.Note2;

                                    userObj.UpdatedBy = userData.UserID;
                                    userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                                }

                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == profileEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET nSonTotal={(familyObj.nSonTotal == null ? "NULL" : familyObj.nSonTotal.ToString())}, nRelativeStudyHere={(familyObj.nRelativeStudyHere == null ? "NULL" : familyObj.nRelativeStudyHere.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.nSonTotal = profileEdited.StudentSonTotal;
                                    familyObj.nRelativeStudyHere = profileEdited.StudentBrethrenStudyHere;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                                }

                                TApproveStudentProfile approveStudentProfilesObj = ctx.TApproveStudentProfiles.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == profileEdited.ID && f.StudentID == profileEdited.StudentID);
                                if (approveStudentProfilesObj != null)
                                {
                                    approveStudentProfilesObj.ApproveStatus = "approve";
                                    approveStudentProfilesObj.ApproveDate = DateTime.Now;
                                    approveStudentProfilesObj.ApproveBy = userData.UserID;
                                    approveStudentProfilesObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                            if (permanentAddressEdited != null)
                            {
                                section = "ที่อยู่ตามทะเบียนบ้าน";

                                string recoveryScript = "";

                                // Approve Data
                                JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == permanentAddressEdited.StudentID).FirstOrDefault();
                                if (userObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET sStudentHomeRegisterCode='{userObj.sStudentHomeRegisterCode}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                    userObj.sStudentHomeRegisterCode = permanentAddressEdited.RegisterHomeCode;

                                    userObj.UpdatedBy = userData.UserID;
                                    userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 5);
                                }

                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == permanentAddressEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET houseRegistrationNumber='{familyObj.houseRegistrationNumber}', houseRegistrationSoy='{familyObj.houseRegistrationSoy}', houseRegistrationMuu='{familyObj.houseRegistrationMuu}', houseRegistrationRoad='{familyObj.houseRegistrationRoad}', houseRegistrationProvince={(familyObj.houseRegistrationProvince == null ? "NULL" : familyObj.houseRegistrationProvince?.ToString())}, houseRegistrationAumpher={(familyObj.houseRegistrationAumpher == null ? "NULL" : familyObj.houseRegistrationAumpher?.ToString())}, houseRegistrationTumbon={(familyObj.houseRegistrationTumbon == null ? "NULL" : familyObj.houseRegistrationTumbon?.ToString())}, houseRegistrationPost='{familyObj.houseRegistrationPost}', houseRegistrationPhone='{familyObj.houseRegistrationPhone}', bornFrom='{familyObj.bornFrom}', bornFromProvince={(familyObj.bornFromProvince == null ? "NULL" : familyObj.bornFromProvince?.ToString())}, bornFromAumpher={(familyObj.bornFromAumpher == null ? "NULL" : familyObj.bornFromAumpher?.ToString())}, bornFromTumbon={(familyObj.bornFromTumbon == null ? "NULL" : familyObj.bornFromTumbon?.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.houseRegistrationNumber = permanentAddressEdited.RegisterHomeNo;
                                    familyObj.houseRegistrationSoy = permanentAddressEdited.RegisterHomeSoi;
                                    familyObj.houseRegistrationMuu = permanentAddressEdited.RegisterHomeMoo;
                                    familyObj.houseRegistrationRoad = permanentAddressEdited.RegisterHomeRoad;
                                    familyObj.houseRegistrationProvince = permanentAddressEdited.RegisterHomeProvince;
                                    familyObj.houseRegistrationAumpher = permanentAddressEdited.RegisterHomeAmphoe;
                                    familyObj.houseRegistrationTumbon = permanentAddressEdited.RegisterHomeTombon;
                                    familyObj.houseRegistrationPost = permanentAddressEdited.RegisterHomePostalCode;
                                    familyObj.houseRegistrationPhone = permanentAddressEdited.RegisterHomePhone;
                                    familyObj.bornFrom = permanentAddressEdited.BornFrom;
                                    familyObj.bornFromProvince = permanentAddressEdited.BornFromProvince;
                                    familyObj.bornFromAumpher = permanentAddressEdited.BornFromAmphoe;
                                    familyObj.bornFromTumbon = permanentAddressEdited.BornFromTombon;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 5);
                                }

                                TApproveStudentPermanentAddress approveStudentPermanentAddressObj = ctx.TApproveStudentPermanentAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == permanentAddressEdited.ID && f.StudentID == permanentAddressEdited.StudentID);
                                if (approveStudentPermanentAddressObj != null)
                                {
                                    approveStudentPermanentAddressObj.ApproveStatus = "approve";
                                    approveStudentPermanentAddressObj.ApproveDate = DateTime.Now;
                                    approveStudentPermanentAddressObj.ApproveBy = userData.UserID;
                                    approveStudentPermanentAddressObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                            if (contactAddressEdited != null)
                            {
                                section = "ที่อยู่ปัจจุบัน";

                                string recoveryScript = "";

                                // Approve Data
                                JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == contactAddressEdited.StudentID).FirstOrDefault();
                                if (userObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET sStudentHomeNumber='{userObj.sStudentHomeNumber}', sStudentSoy='{userObj.sStudentSoy}', sStudentMuu='{userObj.sStudentMuu}', sStudentRoad='{userObj.sStudentRoad}', sStudentProvince='{userObj.sStudentProvince}', sStudentAumpher='{userObj.sStudentAumpher}', sStudentTumbon='{userObj.sStudentTumbon}', sStudentPost='{userObj.sStudentPost}', sStudentHousePhone='{userObj.sStudentHousePhone}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                    userObj.sStudentHomeNumber = contactAddressEdited.HomeNo;
                                    userObj.sStudentSoy = contactAddressEdited.HomeSoi;
                                    userObj.sStudentMuu = contactAddressEdited.HomeMoo;
                                    userObj.sStudentRoad = contactAddressEdited.HomeRoad;
                                    userObj.sStudentProvince = contactAddressEdited.HomeProvince;
                                    userObj.sStudentAumpher = contactAddressEdited.HomeAmphoe;
                                    userObj.sStudentTumbon = contactAddressEdited.HomeTombon;
                                    userObj.sStudentPost = contactAddressEdited.HomePostalCode;
                                    userObj.sStudentHousePhone = contactAddressEdited.HomePhone;

                                    userObj.UpdatedBy = userData.UserID;
                                    userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 10);
                                }

                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == contactAddressEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET stayWithTitle={(familyObj.stayWithTitle == null ? "NULL" : familyObj.stayWithTitle?.ToString())}, stayWithName='{familyObj.stayWithName}', stayWithLast='{familyObj.stayWithLast}', stayWithEmergencyCall='{familyObj.stayWithEmergencyCall}', stayWithEmail='{familyObj.stayWithEmail}', friendName='{familyObj.friendName}', friendLastName='{familyObj.friendLastName}', friendPhone='{familyObj.friendPhone}', HomeType={(familyObj.HomeType == null ? "NULL" : familyObj.HomeType?.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.stayWithTitle = contactAddressEdited.HomeStayWithTitle;
                                    familyObj.stayWithName = contactAddressEdited.HomeStayWithName;
                                    familyObj.stayWithLast = contactAddressEdited.HomeStayWithLast;
                                    familyObj.stayWithEmergencyCall = contactAddressEdited.HomeStayWithEmergencyCall;
                                    familyObj.stayWithEmail = contactAddressEdited.HomeStayWithEmergencyEmail;
                                    familyObj.friendName = contactAddressEdited.HomeFriendName;
                                    familyObj.friendLastName = contactAddressEdited.HomeFriendLastName;
                                    familyObj.friendPhone = contactAddressEdited.HomeFriendPhone;
                                    familyObj.HomeType = contactAddressEdited.HomeHomeType;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 10);
                                }

                                TApproveStudentContactAddress approveStudentContactAddressObj = ctx.TApproveStudentContactAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == contactAddressEdited.ID && f.StudentID == contactAddressEdited.StudentID);
                                if (approveStudentContactAddressObj != null)
                                {
                                    approveStudentContactAddressObj.ApproveStatus = "approve";
                                    approveStudentContactAddressObj.ApproveDate = DateTime.Now;
                                    approveStudentContactAddressObj.ApproveBy = userData.UserID;
                                    approveStudentContactAddressObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                            if (fatherInfoEdited != null)
                            {
                                section = "ข้อมูลบิดา";

                                string recoveryScript = "";

                                // Approve Data
                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == fatherInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sFatherTitle='{familyObj.sFatherTitle}', sFatherFirstName='{familyObj.sFatherFirstName}', sFatherLastName='{familyObj.sFatherLastName}', sFatherNameEN='{familyObj.sFatherNameEN}', sFatherLastEN='{familyObj.sFatherLastEN}', dFatherBirthDay={(familyObj.dFatherBirthDay == null ? "NULL" : "'" + familyObj.dFatherBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sFatherIdCardNumber='{familyObj.sFatherIdCardNumber}', sFatherRace='{familyObj.sFatherRace}', sFatherNation='{familyObj.sFatherNation}', sFatherReligion='{familyObj.sFatherReligion}', sFatherGraduated={(familyObj.sFatherGraduated == null ? "NULL" : familyObj.sFatherGraduated?.ToString())}, sFatherHomeNumber='{familyObj.sFatherHomeNumber}', sFatherSoy='{familyObj.sFatherSoy}', sFatherMuu='{familyObj.sFatherMuu}', sFatherRoad='{familyObj.sFatherRoad}', sFatherProvince='{familyObj.sFatherProvince}', sFatherAumpher='{familyObj.sFatherAumpher}', sFatherTumbon='{familyObj.sFatherTumbon}', sFatherPost='{familyObj.sFatherPost}', sFatherJob='{familyObj.sFatherJob}', nFatherIncome={(familyObj.nFatherIncome == null ? "NULL" : familyObj.nFatherIncome?.ToString())}, sFatherWorkPlace='{familyObj.sFatherWorkPlace}', sFatherPhone='{familyObj.sFatherPhone}', sFatherPhone2='{familyObj.sFatherPhone2}', sFatherPhone3='{familyObj.sFatherPhone3}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.sFatherTitle = fatherInfoEdited.FatherTitle;
                                    familyObj.sFatherFirstName = fatherInfoEdited.FatherName;
                                    familyObj.sFatherLastName = fatherInfoEdited.FatherLastName;
                                    familyObj.sFatherNameEN = fatherInfoEdited.FatherNameEn;
                                    familyObj.sFatherLastEN = fatherInfoEdited.FatherNameLastEn;
                                    familyObj.dFatherBirthDay = string.IsNullOrEmpty(fatherInfoEdited.FatherBirthday) ? (DateTime?)null : DateTime.ParseExact(fatherInfoEdited.FatherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                    familyObj.sFatherIdCardNumber = fatherInfoEdited.FatherIdentification;
                                    familyObj.sFatherRace = fatherInfoEdited.FatherRace;
                                    familyObj.sFatherNation = fatherInfoEdited.FatherNation;
                                    familyObj.sFatherReligion = fatherInfoEdited.FatherReligion;
                                    familyObj.sFatherGraduated = fatherInfoEdited.FatherGraduated;
                                    familyObj.sFatherHomeNumber = fatherInfoEdited.FatherHomeNo;
                                    familyObj.sFatherSoy = fatherInfoEdited.FatherSoi;
                                    familyObj.sFatherMuu = fatherInfoEdited.FatherMoo;
                                    familyObj.sFatherRoad = fatherInfoEdited.FatherRoad;
                                    familyObj.sFatherProvince = fatherInfoEdited.FatherProvince;
                                    familyObj.sFatherAumpher = fatherInfoEdited.FatherAmphoe;
                                    familyObj.sFatherTumbon = fatherInfoEdited.FatherTombon;
                                    familyObj.sFatherPost = fatherInfoEdited.FatherPostalCode;
                                    familyObj.sFatherJob = fatherInfoEdited.FatherJob;
                                    familyObj.nFatherIncome = fatherInfoEdited.FatherIncome;
                                    familyObj.sFatherWorkPlace = fatherInfoEdited.FatherWorkPlace;
                                    familyObj.sFatherPhone = fatherInfoEdited.FatherPhone;
                                    familyObj.sFatherPhone2 = fatherInfoEdited.FatherPhone2;
                                    familyObj.sFatherPhone3 = fatherInfoEdited.FatherPhone3;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 15);
                                }

                                TApproveStudentFatherInfo approveStudentFatherInfoObj = ctx.TApproveStudentFatherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == fatherInfoEdited.ID && f.StudentID == fatherInfoEdited.StudentID);
                                if (approveStudentFatherInfoObj != null)
                                {
                                    approveStudentFatherInfoObj.ApproveStatus = "approve";
                                    approveStudentFatherInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentFatherInfoObj.ApproveBy = userData.UserID;
                                    approveStudentFatherInfoObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                            if (motherInfoEdited != null)
                            {
                                section = "ข้อมูลมารดา";

                                string recoveryScript = "";

                                // Approve Data
                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == motherInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sMotherTitle='{familyObj.sMotherTitle}', sMotherFirstName='{familyObj.sMotherFirstName}', sMotherLastName='{familyObj.sMotherLastName}', sMotherNameEN='{familyObj.sMotherNameEN}', sMotherLastEN='{familyObj.sMotherLastEN}', dMotherBirthDay={(familyObj.dMotherBirthDay == null ? "NULL" : "'" + familyObj.dMotherBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sMotherIdCardNumber='{familyObj.sMotherIdCardNumber}', sMotherRace='{familyObj.sMotherRace}', sMotherNation='{familyObj.sMotherNation}', sMotherReligion='{familyObj.sMotherReligion}', sMotherGraduated={(familyObj.sMotherGraduated == null ? "NULL" : familyObj.sMotherGraduated?.ToString())}, sMotherHomeNumber='{familyObj.sMotherHomeNumber}', sMotherSoy='{familyObj.sMotherSoy}', sMotherMuu='{familyObj.sMotherMuu}', sMotherRoad='{familyObj.sMotherRoad}', sMotherProvince='{familyObj.sMotherProvince}', sMotherAumpher='{familyObj.sMotherAumpher}', sMotherTumbon='{familyObj.sMotherTumbon}', sMotherPost='{familyObj.sMotherPost}', sMotherJob='{familyObj.sMotherJob}', nMotherIncome={(familyObj.nMotherIncome == null ? "NULL" : familyObj.nMotherIncome?.ToString())}, sMotherWorkPlace='{familyObj.sMotherWorkPlace}', sMotherPhone='{familyObj.sMotherPhone}', sMotherPhone2='{familyObj.sMotherPhone2}', sMotherPhone3='{familyObj.sMotherPhone3}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.sMotherTitle = motherInfoEdited.MotherTitle;
                                    familyObj.sMotherFirstName = motherInfoEdited.MotherName;
                                    familyObj.sMotherLastName = motherInfoEdited.MotherLastName;
                                    familyObj.sMotherNameEN = motherInfoEdited.MotherNameEn;
                                    familyObj.sMotherLastEN = motherInfoEdited.MotherNameLastEn;
                                    familyObj.dMotherBirthDay = string.IsNullOrEmpty(motherInfoEdited.MotherBirthday) ? (DateTime?)null : DateTime.ParseExact(motherInfoEdited.MotherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                    familyObj.sMotherIdCardNumber = motherInfoEdited.MotherIdentification;
                                    familyObj.sMotherRace = motherInfoEdited.MotherRace;
                                    familyObj.sMotherNation = motherInfoEdited.MotherNation;
                                    familyObj.sMotherReligion = motherInfoEdited.MotherReligion;
                                    familyObj.sMotherGraduated = motherInfoEdited.MotherGraduated;
                                    familyObj.sMotherHomeNumber = motherInfoEdited.MotherHomeNo;
                                    familyObj.sMotherSoy = motherInfoEdited.MotherSoi;
                                    familyObj.sMotherMuu = motherInfoEdited.MotherMoo;
                                    familyObj.sMotherRoad = motherInfoEdited.MotherRoad;
                                    familyObj.sMotherProvince = motherInfoEdited.MotherProvince;
                                    familyObj.sMotherAumpher = motherInfoEdited.MotherAmphoe;
                                    familyObj.sMotherTumbon = motherInfoEdited.MotherTombon;
                                    familyObj.sMotherPost = motherInfoEdited.MotherPostalCode;
                                    familyObj.sMotherJob = motherInfoEdited.MotherJob;
                                    familyObj.nMotherIncome = motherInfoEdited.MotherIncome;
                                    familyObj.sMotherWorkPlace = motherInfoEdited.MotherWorkPlace;
                                    familyObj.sMotherPhone = motherInfoEdited.MotherPhone;
                                    familyObj.sMotherPhone2 = motherInfoEdited.MotherPhone2;
                                    familyObj.sMotherPhone3 = motherInfoEdited.MotherPhone3;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 20);
                                }

                                TApproveStudentMotherInfo approveStudentMotherInfoObj = ctx.TApproveStudentMotherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == motherInfoEdited.ID && f.StudentID == motherInfoEdited.StudentID);
                                if (approveStudentMotherInfoObj != null)
                                {
                                    approveStudentMotherInfoObj.ApproveStatus = "approve";
                                    approveStudentMotherInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentMotherInfoObj.ApproveBy = userData.UserID;
                                    approveStudentMotherInfoObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                            if (parentInfoEdited != null)
                            {
                                section = "ข้อมูลผู้ปกครอง";

                                string recoveryScript = "";

                                // Approve Data
                                TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == parentInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                                if (familyObj != null)
                                {
                                    recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sFamilyRelate='{familyObj.sFamilyRelate}', sFamilyTitle='{familyObj.sFamilyTitle}', sFamilyName='{familyObj.sFamilyName}', sFamilyLast='{familyObj.sFamilyLast}', sFamilyNameEN='{familyObj.sFamilyNameEN}', sFamilyLastEN='{familyObj.sFamilyLastEN}', dFamilyBirthDay={(familyObj.dFamilyBirthDay == null ? "NULL" : "'" + familyObj.dFamilyBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sFamilyIdCardNumber='{familyObj.sFamilyIdCardNumber}', sFamilyRace='{familyObj.sFamilyRace}', sFamilyNation='{familyObj.sFamilyNation}', sFamilyReligion='{familyObj.sFamilyReligion}', sFamilyGraduated={(familyObj.sFamilyGraduated == null ? "NULL" : familyObj.sFamilyGraduated?.ToString())}, sFamilyHomeNumber='{familyObj.sFamilyHomeNumber}', sFamilySoy='{familyObj.sFamilySoy}', sFamilyMuu='{familyObj.sFamilyMuu}', sFamilyRoad='{familyObj.sFamilyRoad}', sFamilyProvince='{familyObj.sFamilyProvince}', sFamilyAumpher='{familyObj.sFamilyAumpher}', sFamilyTumbon='{familyObj.sFamilyTumbon}', sFamilyPost='{familyObj.sFamilyPost}', nFamilyRequestStudyMoney={(familyObj.nFamilyRequestStudyMoney == null ? "NULL" : familyObj.nFamilyRequestStudyMoney?.ToString())}, familyStatus={(familyObj.familyStatus == null ? "NULL" : familyObj.familyStatus?.ToString())}, sFamilyJob='{familyObj.sFamilyJob}', nFamilyIncome={(familyObj.nFamilyIncome == null ? "NULL" : familyObj.nFamilyIncome?.ToString())}, sFamilyWorkPlace='{familyObj.sFamilyWorkPlace}', sPhoneOne='{familyObj.sPhoneOne}', sPhoneTwo='{familyObj.sPhoneTwo}', sPhoneThree='{familyObj.sPhoneThree}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                    familyObj.sFamilyRelate = parentInfoEdited.ParentRelate;
                                    familyObj.sFamilyTitle = parentInfoEdited.ParentTitle;
                                    familyObj.sFamilyName = parentInfoEdited.ParentName;
                                    familyObj.sFamilyLast = parentInfoEdited.ParentLastName;
                                    familyObj.sFamilyNameEN = parentInfoEdited.ParentNameEn;
                                    familyObj.sFamilyLastEN = parentInfoEdited.ParentNameLastEn;
                                    familyObj.dFamilyBirthDay = string.IsNullOrEmpty(parentInfoEdited.ParentBirthday) ? (DateTime?)null : DateTime.ParseExact(parentInfoEdited.ParentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                    familyObj.sFamilyIdCardNumber = parentInfoEdited.ParentIdentification;
                                    familyObj.sFamilyRace = parentInfoEdited.ParentRace;
                                    familyObj.sFamilyNation = parentInfoEdited.ParentNation;
                                    familyObj.sFamilyReligion = parentInfoEdited.ParentReligion;
                                    familyObj.sFamilyGraduated = parentInfoEdited.ParentGraduated;
                                    familyObj.sFamilyHomeNumber = parentInfoEdited.ParentHomeNo;
                                    familyObj.sFamilySoy = parentInfoEdited.ParentSoi;
                                    familyObj.sFamilyMuu = parentInfoEdited.ParentMoo;
                                    familyObj.sFamilyRoad = parentInfoEdited.ParentRoad;
                                    familyObj.sFamilyProvince = parentInfoEdited.ParentProvince;
                                    familyObj.sFamilyAumpher = parentInfoEdited.ParentAmphoe;
                                    familyObj.sFamilyTumbon = parentInfoEdited.ParentTombon;
                                    familyObj.sFamilyPost = parentInfoEdited.ParentPostalCode;
                                    familyObj.nFamilyRequestStudyMoney = parentInfoEdited.TuitionReimbursement;
                                    familyObj.familyStatus = parentInfoEdited.ParentStatus;
                                    familyObj.sFamilyJob = parentInfoEdited.ParentJob;
                                    familyObj.nFamilyIncome = parentInfoEdited.ParentIncome;
                                    familyObj.sFamilyWorkPlace = parentInfoEdited.ParentWorkPlace;
                                    familyObj.sPhoneOne = parentInfoEdited.ParentPhone;
                                    familyObj.sPhoneTwo = parentInfoEdited.ParentPhone2;
                                    familyObj.sPhoneThree = parentInfoEdited.ParentPhone3;

                                    familyObj.UpdatedBy = userData.UserID;
                                    familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 25);
                                }

                                TApproveStudentParentInfo approveStudentParentInfoObj = ctx.TApproveStudentParentInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == parentInfoEdited.ID && f.StudentID == parentInfoEdited.StudentID);
                                if (approveStudentParentInfoObj != null)
                                {
                                    approveStudentParentInfoObj.ApproveStatus = "approve";
                                    approveStudentParentInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentParentInfoObj.ApproveBy = userData.UserID;
                                    approveStudentParentInfoObj.RecoveryScript = recoveryScript;
                                }

                                ctx.SaveChanges();
                            }

                        }
                        catch (Exception err2)
                        {
                          

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, section, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApproveProfile(ProfileEdited profileEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                   
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            MasterEntity.TUser mUserObj = mctx.TUsers.Where(w => w.nCompany == schoolID && w.sID == profileEdited.StudentID).FirstOrDefault();
                            if (mUserObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiMasterSingleDB.dbo.TUser SET cSex='{mUserObj.cSex}', sName='{mUserObj.sName}', sLastname='{mUserObj.sLastname}', dBirth='{mUserObj.dBirth?.ToString("yyyy-MM-dd HH:mm:ss.fff")}', sPhone='{mUserObj.sPhone}', sEmail='{mUserObj.sEmail}' WHERE nCompany={mUserObj.nCompany} AND sID={mUserObj.sID};") + Environment.NewLine;

                                mUserObj.cSex = profileEdited.StudentGender;
                                mUserObj.sName = profileEdited.StudentFirstNameTh;
                                mUserObj.sLastname = profileEdited.StudentLastNameTh;
                                mUserObj.dBirth = string.IsNullOrEmpty(profileEdited.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(profileEdited.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                mUserObj.sPhone = profileEdited.StudentPhone;
                                mUserObj.sEmail = profileEdited.StudentEmail;

                                mUserObj.dUpdate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                            }

                            mctx.SaveChanges();

                            JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == profileEdited.StudentID).FirstOrDefault();
                            if (userObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET cSex='{userObj.cSex}', sStudentTitle='{userObj.sStudentTitle}', sName='{userObj.sName}', sLastname='{userObj.sLastname}', sStudentNameEN='{userObj.sStudentNameEN}', sStudentLastEN='{userObj.sStudentLastEN}', sStudentNameOther='{userObj.sStudentNameOther}', sStudentLastOther='{userObj.sStudentLastOther}', sNickName='{userObj.sNickName}', sNickNameEN='{userObj.sNickNameEN}', dBirth='{userObj.dBirth?.ToString("yyyy-MM-dd HH:mm:ss.fff")}', sStudentRace='{userObj.sStudentRace}', sStudentNation='{userObj.sStudentNation}', sStudentReligion='{userObj.sStudentReligion}', DisabilityCode='{userObj.DisabilityCode}', DisadvantageCode='{userObj.DisadvantageCode}', sPhone='{userObj.sPhone}', sEmail='{userObj.sEmail}', nSonNumber={(userObj.nSonNumber == null ? "NULL" : userObj.nSonNumber.ToString())}, Note2='{userObj.Note2}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                userObj.cSex = profileEdited.StudentGender;
                                userObj.sStudentTitle = profileEdited.StudentTitle?.ToString();
                                userObj.sName = profileEdited.StudentFirstNameTh;
                                userObj.sLastname = profileEdited.StudentLastNameTh;
                                userObj.sStudentNameEN = profileEdited.StudentFirstNameEn;
                                userObj.sStudentLastEN = profileEdited.StudentLastNameEn;
                                userObj.sStudentNameOther = profileEdited.StudentFirstNameOther;
                                userObj.sStudentLastOther = profileEdited.StudentLastNameOther;
                                userObj.sNickName = profileEdited.StudentNickNameTh;
                                userObj.sNickNameEN = profileEdited.StudentNickNameEn;
                                userObj.dBirth = string.IsNullOrEmpty(profileEdited.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(profileEdited.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                userObj.sStudentRace = profileEdited.StudentRace;
                                userObj.sStudentNation = profileEdited.StudentNation;
                                userObj.sStudentReligion = profileEdited.StudentReligion;
                                userObj.DisabilityCode = profileEdited.DisabilityCode;
                                userObj.DisadvantageCode = profileEdited.DisadvantageCode;
                                userObj.sPhone = profileEdited.StudentPhone;
                                userObj.sEmail = profileEdited.StudentEmail;
                                userObj.nSonNumber = profileEdited.StudentSonNumber;
                                userObj.Note2 = profileEdited.Note2;

                                userObj.UpdatedBy = userData.UserID;
                                userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                            }

                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == profileEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET nSonTotal={(familyObj.nSonTotal == null ? "NULL" : familyObj.nSonTotal.ToString())}, nRelativeStudyHere={(familyObj.nRelativeStudyHere == null ? "NULL" : familyObj.nRelativeStudyHere.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.nSonTotal = profileEdited.StudentSonTotal;
                                familyObj.nRelativeStudyHere = profileEdited.StudentBrethrenStudyHere;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 1);
                            }

                            TApproveStudentProfile approveStudentProfilesObj = ctx.TApproveStudentProfiles.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == profileEdited.ID && f.StudentID == profileEdited.StudentID);
                            if (approveStudentProfilesObj != null)
                            {
                                approveStudentProfilesObj.ApproveStatus = "approve";
                                approveStudentProfilesObj.ApproveDate = DateTime.Now;
                                approveStudentProfilesObj.ApproveBy = userData.UserID;
                                approveStudentProfilesObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                    
                        }
                        catch (Exception err2)
                        {
                          

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApprovePermanentAddress(PermanentAddressEdited permanentAddressEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == permanentAddressEdited.StudentID).FirstOrDefault();
                            if (userObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET sStudentHomeRegisterCode='{userObj.sStudentHomeRegisterCode}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                userObj.sStudentHomeRegisterCode = permanentAddressEdited.RegisterHomeCode;

                                userObj.UpdatedBy = userData.UserID;
                                userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 5);
                            }

                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == permanentAddressEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET houseRegistrationNumber='{familyObj.houseRegistrationNumber}', houseRegistrationSoy='{familyObj.houseRegistrationSoy}', houseRegistrationMuu='{familyObj.houseRegistrationMuu}', houseRegistrationRoad='{familyObj.houseRegistrationRoad}', houseRegistrationProvince={(familyObj.houseRegistrationProvince == null ? "NULL" : familyObj.houseRegistrationProvince?.ToString())}, houseRegistrationAumpher={(familyObj.houseRegistrationAumpher == null ? "NULL" : familyObj.houseRegistrationAumpher?.ToString())}, houseRegistrationTumbon={(familyObj.houseRegistrationTumbon == null ? "NULL" : familyObj.houseRegistrationTumbon?.ToString())}, houseRegistrationPost='{familyObj.houseRegistrationPost}', houseRegistrationPhone='{familyObj.houseRegistrationPhone}', bornFrom='{familyObj.bornFrom}', bornFromProvince={(familyObj.bornFromProvince == null ? "NULL" : familyObj.bornFromProvince?.ToString())}, bornFromAumpher={(familyObj.bornFromAumpher == null ? "NULL" : familyObj.bornFromAumpher?.ToString())}, bornFromTumbon={(familyObj.bornFromTumbon == null ? "NULL" : familyObj.bornFromTumbon?.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.houseRegistrationNumber = permanentAddressEdited.RegisterHomeNo;
                                familyObj.houseRegistrationSoy = permanentAddressEdited.RegisterHomeSoi;
                                familyObj.houseRegistrationMuu = permanentAddressEdited.RegisterHomeMoo;
                                familyObj.houseRegistrationRoad = permanentAddressEdited.RegisterHomeRoad;
                                familyObj.houseRegistrationProvince = permanentAddressEdited.RegisterHomeProvince;
                                familyObj.houseRegistrationAumpher = permanentAddressEdited.RegisterHomeAmphoe;
                                familyObj.houseRegistrationTumbon = permanentAddressEdited.RegisterHomeTombon;
                                familyObj.houseRegistrationPost = permanentAddressEdited.RegisterHomePostalCode;
                                familyObj.houseRegistrationPhone = permanentAddressEdited.RegisterHomePhone;
                                familyObj.bornFrom = permanentAddressEdited.BornFrom;
                                familyObj.bornFromProvince = permanentAddressEdited.BornFromProvince;
                                familyObj.bornFromAumpher = permanentAddressEdited.BornFromAmphoe;
                                familyObj.bornFromTumbon = permanentAddressEdited.BornFromTombon;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 5);
                            }

                            TApproveStudentPermanentAddress approveStudentPermanentAddressObj = ctx.TApproveStudentPermanentAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == permanentAddressEdited.ID && f.StudentID == permanentAddressEdited.StudentID);
                            if (approveStudentPermanentAddressObj != null)
                            {
                                approveStudentPermanentAddressObj.ApproveStatus = "approve";
                                approveStudentPermanentAddressObj.ApproveDate = DateTime.Now;
                                approveStudentPermanentAddressObj.ApproveBy = userData.UserID;
                                approveStudentPermanentAddressObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                        }
                        catch (Exception err2)
                        {
                           

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApproveContactAddress(ContactAddressEdited contactAddressEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                   
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            JabjaiEntity.DB.TUser userObj = ctx.TUser.Where(w => w.SchoolID == schoolID && w.sID == contactAddressEdited.StudentID).FirstOrDefault();
                            if (userObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TUser SET sStudentHomeNumber='{userObj.sStudentHomeNumber}', sStudentSoy='{userObj.sStudentSoy}', sStudentMuu='{userObj.sStudentMuu}', sStudentRoad='{userObj.sStudentRoad}', sStudentProvince='{userObj.sStudentProvince}', sStudentAumpher='{userObj.sStudentAumpher}', sStudentTumbon='{userObj.sStudentTumbon}', sStudentPost='{userObj.sStudentPost}', sStudentHousePhone='{userObj.sStudentHousePhone}' WHERE SchoolID={userObj.SchoolID} AND sID={userObj.sID};") + Environment.NewLine;

                                userObj.sStudentHomeNumber = contactAddressEdited.HomeNo;
                                userObj.sStudentSoy = contactAddressEdited.HomeSoi;
                                userObj.sStudentMuu = contactAddressEdited.HomeMoo;
                                userObj.sStudentRoad = contactAddressEdited.HomeRoad;
                                userObj.sStudentProvince = contactAddressEdited.HomeProvince;
                                userObj.sStudentAumpher = contactAddressEdited.HomeAmphoe;
                                userObj.sStudentTumbon = contactAddressEdited.HomeTombon;
                                userObj.sStudentPost = contactAddressEdited.HomePostalCode;
                                userObj.sStudentHousePhone = contactAddressEdited.HomePhone;

                                userObj.UpdatedBy = userData.UserID;
                                userObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 10);
                            }

                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == contactAddressEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET stayWithTitle={(familyObj.stayWithTitle == null ? "NULL" : familyObj.stayWithTitle?.ToString())}, stayWithName='{familyObj.stayWithName}', stayWithLast='{familyObj.stayWithLast}', stayWithEmergencyCall='{familyObj.stayWithEmergencyCall}', stayWithEmail='{familyObj.stayWithEmail}', friendName='{familyObj.friendName}', friendLastName='{familyObj.friendLastName}', friendPhone='{familyObj.friendPhone}', HomeType={(familyObj.HomeType == null ? "NULL" : familyObj.HomeType?.ToString())} WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.stayWithTitle = contactAddressEdited.HomeStayWithTitle;
                                familyObj.stayWithName = contactAddressEdited.HomeStayWithName;
                                familyObj.stayWithLast = contactAddressEdited.HomeStayWithLast;
                                familyObj.stayWithEmergencyCall = contactAddressEdited.HomeStayWithEmergencyCall;
                                familyObj.stayWithEmail = contactAddressEdited.HomeStayWithEmergencyEmail;
                                familyObj.friendName = contactAddressEdited.HomeFriendName;
                                familyObj.friendLastName = contactAddressEdited.HomeFriendLastName;
                                familyObj.friendPhone = contactAddressEdited.HomeFriendPhone;
                                familyObj.HomeType = contactAddressEdited.HomeHomeType;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 10);
                            }

                            TApproveStudentContactAddress approveStudentContactAddressObj = ctx.TApproveStudentContactAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == contactAddressEdited.ID && f.StudentID == contactAddressEdited.StudentID);
                            if (approveStudentContactAddressObj != null)
                            {
                                approveStudentContactAddressObj.ApproveStatus = "approve";
                                approveStudentContactAddressObj.ApproveDate = DateTime.Now;
                                approveStudentContactAddressObj.ApproveBy = userData.UserID;
                                approveStudentContactAddressObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                            
                        }
                        catch (Exception err2)
                        {
                           

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApproveFatherInfo(FatherInfoEdited fatherInfoEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                   
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == fatherInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sFatherTitle='{familyObj.sFatherTitle}', sFatherFirstName='{familyObj.sFatherFirstName}', sFatherLastName='{familyObj.sFatherLastName}', sFatherNameEN='{familyObj.sFatherNameEN}', sFatherLastEN='{familyObj.sFatherLastEN}', dFatherBirthDay={(familyObj.dFatherBirthDay == null ? "NULL" : "'" + familyObj.dFatherBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sFatherIdCardNumber='{familyObj.sFatherIdCardNumber}', sFatherRace='{familyObj.sFatherRace}', sFatherNation='{familyObj.sFatherNation}', sFatherReligion='{familyObj.sFatherReligion}', sFatherGraduated={(familyObj.sFatherGraduated == null ? "NULL" : familyObj.sFatherGraduated?.ToString())}, sFatherHomeNumber='{familyObj.sFatherHomeNumber}', sFatherSoy='{familyObj.sFatherSoy}', sFatherMuu='{familyObj.sFatherMuu}', sFatherRoad='{familyObj.sFatherRoad}', sFatherProvince='{familyObj.sFatherProvince}', sFatherAumpher='{familyObj.sFatherAumpher}', sFatherTumbon='{familyObj.sFatherTumbon}', sFatherPost='{familyObj.sFatherPost}', sFatherJob='{familyObj.sFatherJob}', nFatherIncome={(familyObj.nFatherIncome == null ? "NULL" : familyObj.nFatherIncome?.ToString())}, sFatherWorkPlace='{familyObj.sFatherWorkPlace}', sFatherPhone='{familyObj.sFatherPhone}', sFatherPhone2='{familyObj.sFatherPhone2}', sFatherPhone3='{familyObj.sFatherPhone3}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.sFatherTitle = fatherInfoEdited.FatherTitle;
                                familyObj.sFatherFirstName = fatherInfoEdited.FatherName;
                                familyObj.sFatherLastName = fatherInfoEdited.FatherLastName;
                                familyObj.sFatherNameEN = fatherInfoEdited.FatherNameEn;
                                familyObj.sFatherLastEN = fatherInfoEdited.FatherNameLastEn;
                                familyObj.dFatherBirthDay = string.IsNullOrEmpty(fatherInfoEdited.FatherBirthday) ? (DateTime?)null : DateTime.ParseExact(fatherInfoEdited.FatherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                familyObj.sFatherIdCardNumber = fatherInfoEdited.FatherIdentification;
                                familyObj.sFatherRace = fatherInfoEdited.FatherRace;
                                familyObj.sFatherNation = fatherInfoEdited.FatherNation;
                                familyObj.sFatherReligion = fatherInfoEdited.FatherReligion;
                                familyObj.sFatherGraduated = fatherInfoEdited.FatherGraduated;
                                familyObj.sFatherHomeNumber = fatherInfoEdited.FatherHomeNo;
                                familyObj.sFatherSoy = fatherInfoEdited.FatherSoi;
                                familyObj.sFatherMuu = fatherInfoEdited.FatherMoo;
                                familyObj.sFatherRoad = fatherInfoEdited.FatherRoad;
                                familyObj.sFatherProvince = fatherInfoEdited.FatherProvince;
                                familyObj.sFatherAumpher = fatherInfoEdited.FatherAmphoe;
                                familyObj.sFatherTumbon = fatherInfoEdited.FatherTombon;
                                familyObj.sFatherPost = fatherInfoEdited.FatherPostalCode;
                                familyObj.sFatherJob = fatherInfoEdited.FatherJob;
                                familyObj.nFatherIncome = fatherInfoEdited.FatherIncome;
                                familyObj.sFatherWorkPlace = fatherInfoEdited.FatherWorkPlace;
                                familyObj.sFatherPhone = fatherInfoEdited.FatherPhone;
                                familyObj.sFatherPhone2 = fatherInfoEdited.FatherPhone2;
                                familyObj.sFatherPhone3 = fatherInfoEdited.FatherPhone3;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 15);
                            }

                            TApproveStudentFatherInfo approveStudentFatherInfoObj = ctx.TApproveStudentFatherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == fatherInfoEdited.ID && f.StudentID == fatherInfoEdited.StudentID);
                            if (approveStudentFatherInfoObj != null)
                            {
                                approveStudentFatherInfoObj.ApproveStatus = "approve";
                                approveStudentFatherInfoObj.ApproveDate = DateTime.Now;
                                approveStudentFatherInfoObj.ApproveBy = userData.UserID;
                                approveStudentFatherInfoObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                            
                        }
                        catch (Exception err2)
                        {
                          

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApproveMotherInfo(MotherInfoEdited motherInfoEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == motherInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sMotherTitle='{familyObj.sMotherTitle}', sMotherFirstName='{familyObj.sMotherFirstName}', sMotherLastName='{familyObj.sMotherLastName}', sMotherNameEN='{familyObj.sMotherNameEN}', sMotherLastEN='{familyObj.sMotherLastEN}', dMotherBirthDay={(familyObj.dMotherBirthDay == null ? "NULL" : "'" + familyObj.dMotherBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sMotherIdCardNumber='{familyObj.sMotherIdCardNumber}', sMotherRace='{familyObj.sMotherRace}', sMotherNation='{familyObj.sMotherNation}', sMotherReligion='{familyObj.sMotherReligion}', sMotherGraduated={(familyObj.sMotherGraduated == null ? "NULL" : familyObj.sMotherGraduated?.ToString())}, sMotherHomeNumber='{familyObj.sMotherHomeNumber}', sMotherSoy='{familyObj.sMotherSoy}', sMotherMuu='{familyObj.sMotherMuu}', sMotherRoad='{familyObj.sMotherRoad}', sMotherProvince='{familyObj.sMotherProvince}', sMotherAumpher='{familyObj.sMotherAumpher}', sMotherTumbon='{familyObj.sMotherTumbon}', sMotherPost='{familyObj.sMotherPost}', sMotherJob='{familyObj.sMotherJob}', nMotherIncome={(familyObj.nMotherIncome == null ? "NULL" : familyObj.nMotherIncome?.ToString())}, sMotherWorkPlace='{familyObj.sMotherWorkPlace}', sMotherPhone='{familyObj.sMotherPhone}', sMotherPhone2='{familyObj.sMotherPhone2}', sMotherPhone3='{familyObj.sMotherPhone3}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.sMotherTitle = motherInfoEdited.MotherTitle;
                                familyObj.sMotherFirstName = motherInfoEdited.MotherName;
                                familyObj.sMotherLastName = motherInfoEdited.MotherLastName;
                                familyObj.sMotherNameEN = motherInfoEdited.MotherNameEn;
                                familyObj.sMotherLastEN = motherInfoEdited.MotherNameLastEn;
                                familyObj.dMotherBirthDay = string.IsNullOrEmpty(motherInfoEdited.MotherBirthday) ? (DateTime?)null : DateTime.ParseExact(motherInfoEdited.MotherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                familyObj.sMotherIdCardNumber = motherInfoEdited.MotherIdentification;
                                familyObj.sMotherRace = motherInfoEdited.MotherRace;
                                familyObj.sMotherNation = motherInfoEdited.MotherNation;
                                familyObj.sMotherReligion = motherInfoEdited.MotherReligion;
                                familyObj.sMotherGraduated = motherInfoEdited.MotherGraduated;
                                familyObj.sMotherHomeNumber = motherInfoEdited.MotherHomeNo;
                                familyObj.sMotherSoy = motherInfoEdited.MotherSoi;
                                familyObj.sMotherMuu = motherInfoEdited.MotherMoo;
                                familyObj.sMotherRoad = motherInfoEdited.MotherRoad;
                                familyObj.sMotherProvince = motherInfoEdited.MotherProvince;
                                familyObj.sMotherAumpher = motherInfoEdited.MotherAmphoe;
                                familyObj.sMotherTumbon = motherInfoEdited.MotherTombon;
                                familyObj.sMotherPost = motherInfoEdited.MotherPostalCode;
                                familyObj.sMotherJob = motherInfoEdited.MotherJob;
                                familyObj.nMotherIncome = motherInfoEdited.MotherIncome;
                                familyObj.sMotherWorkPlace = motherInfoEdited.MotherWorkPlace;
                                familyObj.sMotherPhone = motherInfoEdited.MotherPhone;
                                familyObj.sMotherPhone2 = motherInfoEdited.MotherPhone2;
                                familyObj.sMotherPhone3 = motherInfoEdited.MotherPhone3;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 20);
                            }

                            TApproveStudentMotherInfo approveStudentMotherInfoObj = ctx.TApproveStudentMotherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == motherInfoEdited.ID && f.StudentID == motherInfoEdited.StudentID);
                            if (approveStudentMotherInfoObj != null)
                            {
                                approveStudentMotherInfoObj.ApproveStatus = "approve";
                                approveStudentMotherInfoObj.ApproveDate = DateTime.Now;
                                approveStudentMotherInfoObj.ApproveBy = userData.UserID;
                                approveStudentMotherInfoObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                           
                        }
                        catch (Exception err2)
                        {
                           

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveApproveParentInfo(ParentInfoEdited parentInfoEdited)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                  
                        try
                        {
                            string recoveryScript = "";

                            // Approve Data
                            TFamilyProfile familyObj = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == parentInfoEdited.StudentID && w.sDeleted == "false").FirstOrDefault();
                            if (familyObj != null)
                            {
                                recoveryScript += string.Format($@"UPDATE JabjaiSchoolSingleDB.dbo.TFamilyProfile SET sFamilyRelate='{familyObj.sFamilyRelate}', sFamilyTitle='{familyObj.sFamilyTitle}', sFamilyName='{familyObj.sFamilyName}', sFamilyLast='{familyObj.sFamilyLast}', sFamilyNameEN='{familyObj.sFamilyNameEN}', sFamilyLastEN='{familyObj.sFamilyLastEN}', dFamilyBirthDay={(familyObj.dFamilyBirthDay == null ? "NULL" : "'" + familyObj.dFamilyBirthDay?.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'")}, sFamilyIdCardNumber='{familyObj.sFamilyIdCardNumber}', sFamilyRace='{familyObj.sFamilyRace}', sFamilyNation='{familyObj.sFamilyNation}', sFamilyReligion='{familyObj.sFamilyReligion}', sFamilyGraduated={(familyObj.sFamilyGraduated == null ? "NULL" : familyObj.sFamilyGraduated?.ToString())}, sFamilyHomeNumber='{familyObj.sFamilyHomeNumber}', sFamilySoy='{familyObj.sFamilySoy}', sFamilyMuu='{familyObj.sFamilyMuu}', sFamilyRoad='{familyObj.sFamilyRoad}', sFamilyProvince='{familyObj.sFamilyProvince}', sFamilyAumpher='{familyObj.sFamilyAumpher}', sFamilyTumbon='{familyObj.sFamilyTumbon}', sFamilyPost='{familyObj.sFamilyPost}', nFamilyRequestStudyMoney={(familyObj.nFamilyRequestStudyMoney == null ? "NULL" : familyObj.nFamilyRequestStudyMoney?.ToString())}, familyStatus={(familyObj.familyStatus == null ? "NULL" : familyObj.familyStatus?.ToString())}, sFamilyJob='{familyObj.sFamilyJob}', nFamilyIncome={(familyObj.nFamilyIncome == null ? "NULL" : familyObj.nFamilyIncome?.ToString())}, sFamilyWorkPlace='{familyObj.sFamilyWorkPlace}', sPhoneOne='{familyObj.sPhoneOne}', sPhoneTwo='{familyObj.sPhoneTwo}', sPhoneThree='{familyObj.sPhoneThree}' WHERE nFamilyID={familyObj.nFamilyID};") + Environment.NewLine;

                                familyObj.sFamilyRelate = parentInfoEdited.ParentRelate;
                                familyObj.sFamilyTitle = parentInfoEdited.ParentTitle;
                                familyObj.sFamilyName = parentInfoEdited.ParentName;
                                familyObj.sFamilyLast = parentInfoEdited.ParentLastName;
                                familyObj.sFamilyNameEN = parentInfoEdited.ParentNameEn;
                                familyObj.sFamilyLastEN = parentInfoEdited.ParentNameLastEn;
                                familyObj.dFamilyBirthDay = string.IsNullOrEmpty(parentInfoEdited.ParentBirthday) ? (DateTime?)null : DateTime.ParseExact(parentInfoEdited.ParentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                familyObj.sFamilyIdCardNumber = parentInfoEdited.ParentIdentification;
                                familyObj.sFamilyRace = parentInfoEdited.ParentRace;
                                familyObj.sFamilyNation = parentInfoEdited.ParentNation;
                                familyObj.sFamilyReligion = parentInfoEdited.ParentReligion;
                                familyObj.sFamilyGraduated = parentInfoEdited.ParentGraduated;
                                familyObj.sFamilyHomeNumber = parentInfoEdited.ParentHomeNo;
                                familyObj.sFamilySoy = parentInfoEdited.ParentSoi;
                                familyObj.sFamilyMuu = parentInfoEdited.ParentMoo;
                                familyObj.sFamilyRoad = parentInfoEdited.ParentRoad;
                                familyObj.sFamilyProvince = parentInfoEdited.ParentProvince;
                                familyObj.sFamilyAumpher = parentInfoEdited.ParentAmphoe;
                                familyObj.sFamilyTumbon = parentInfoEdited.ParentTombon;
                                familyObj.sFamilyPost = parentInfoEdited.ParentPostalCode;
                                familyObj.nFamilyRequestStudyMoney = parentInfoEdited.TuitionReimbursement;
                                familyObj.familyStatus = parentInfoEdited.ParentStatus;
                                familyObj.sFamilyJob = parentInfoEdited.ParentJob;
                                familyObj.nFamilyIncome = parentInfoEdited.ParentIncome;
                                familyObj.sFamilyWorkPlace = parentInfoEdited.ParentWorkPlace;
                                familyObj.sPhoneOne = parentInfoEdited.ParentPhone;
                                familyObj.sPhoneTwo = parentInfoEdited.ParentPhone2;
                                familyObj.sPhoneThree = parentInfoEdited.ParentPhone3;

                                familyObj.UpdatedBy = userData.UserID;
                                familyObj.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(7, 25);
                            }

                            TApproveStudentParentInfo approveStudentParentInfoObj = ctx.TApproveStudentParentInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == parentInfoEdited.ID && f.StudentID == parentInfoEdited.StudentID);
                            if (approveStudentParentInfoObj != null)
                            {
                                approveStudentParentInfoObj.ApproveStatus = "approve";
                                approveStudentParentInfoObj.ApproveDate = DateTime.Now;
                                approveStudentParentInfoObj.ApproveBy = userData.UserID;
                                approveStudentParentInfoObj.RecoveryScript = recoveryScript;
                            }

                            ctx.SaveChanges();

                            
                        }
                        catch (Exception err2)
                        {
                           

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryProfile(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentProfile approveStudentProfilesObj = ctx.TApproveStudentProfiles.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentProfilesObj != null)
                    {
                        // Execute command
                        var query = approveStudentProfilesObj.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentProfilesObj.RecoveryDate = DateTime.Now;
                            approveStudentProfilesObj.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryPermanentAddress(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentPermanentAddress approveStudentPermanentAddress = ctx.TApproveStudentPermanentAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentPermanentAddress != null)
                    {
                        // Execute command
                        var query = approveStudentPermanentAddress.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentPermanentAddress.RecoveryDate = DateTime.Now;
                            approveStudentPermanentAddress.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryContactAddress(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentContactAddress approveStudentContactAddress = ctx.TApproveStudentContactAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentContactAddress != null)
                    {
                        // Execute command
                        var query = approveStudentContactAddress.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentContactAddress.RecoveryDate = DateTime.Now;
                            approveStudentContactAddress.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryFatherInfo(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentFatherInfo approveStudentFatherInfo = ctx.TApproveStudentFatherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentFatherInfo != null)
                    {
                        // Execute command
                        var query = approveStudentFatherInfo.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentFatherInfo.RecoveryDate = DateTime.Now;
                            approveStudentFatherInfo.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryMotherInfo(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentMotherInfo approveStudentMotherInfo = ctx.TApproveStudentMotherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentMotherInfo != null)
                    {
                        // Execute command
                        var query = approveStudentMotherInfo.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentMotherInfo.RecoveryDate = DateTime.Now;
                            approveStudentMotherInfo.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object RecoveryParentInfo(int id, int sid)
        {
            return JsonConvert.SerializeObject(new { success = false, message = "Under development" });

            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentParentInfo approveStudentParentInfo = ctx.TApproveStudentParentInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == id && f.StudentID == sid);
                    if (approveStudentParentInfo != null)
                    {
                        // Execute command
                        var query = approveStudentParentInfo.RecoveryScript;
                        int noOfRowRecovery = ctx.Database.ExecuteSqlCommand(query);
                        if (noOfRowRecovery > 0)
                        {
                            approveStudentParentInfo.RecoveryDate = DateTime.Now;
                            approveStudentParentInfo.RecoveryBy = userData.UserID;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectAll(RejectData profileReject, RejectData permanentAddressReject, RejectData contactAddressReject, RejectData fatherInfoReject, RejectData motherInfoReject, RejectData parentInfoReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string section = "";
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    
                        try
                        {
                            if (profileReject != null)
                            {
                                section = "ประวัติส่วนตัว";

                                TApproveStudentProfile approveStudentProfilesObj = ctx.TApproveStudentProfiles.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == profileReject.ID && f.StudentID == profileReject.StudentID);
                                if (approveStudentProfilesObj != null)
                                {
                                    approveStudentProfilesObj.ApproveStatus = "reject";
                                    approveStudentProfilesObj.ApproveComment = profileReject.Comment;
                                    approveStudentProfilesObj.ApproveDate = DateTime.Now;
                                    approveStudentProfilesObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                            if (permanentAddressReject != null)
                            {
                                section = "ที่อยู่ตามทะเบียนบ้าน";

                                TApproveStudentPermanentAddress approveStudentPermanentAddressObj = ctx.TApproveStudentPermanentAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == permanentAddressReject.ID && f.StudentID == permanentAddressReject.StudentID);
                                if (approveStudentPermanentAddressObj != null)
                                {
                                    approveStudentPermanentAddressObj.ApproveStatus = "reject";
                                    approveStudentPermanentAddressObj.ApproveComment = permanentAddressReject.Comment;
                                    approveStudentPermanentAddressObj.ApproveDate = DateTime.Now;
                                    approveStudentPermanentAddressObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                            if (contactAddressReject != null)
                            {
                                section = "ที่อยู่ปัจจุบัน";

                                TApproveStudentContactAddress approveStudentContactAddressObj = ctx.TApproveStudentContactAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == contactAddressReject.ID && f.StudentID == contactAddressReject.StudentID);
                                if (approveStudentContactAddressObj != null)
                                {
                                    approveStudentContactAddressObj.ApproveStatus = "reject";
                                    approveStudentContactAddressObj.ApproveComment = contactAddressReject.Comment;
                                    approveStudentContactAddressObj.ApproveDate = DateTime.Now;
                                    approveStudentContactAddressObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                            if (fatherInfoReject != null)
                            {
                                section = "ข้อมูลบิดา";

                                TApproveStudentFatherInfo approveStudentFatherInfoObj = ctx.TApproveStudentFatherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == fatherInfoReject.ID && f.StudentID == fatherInfoReject.StudentID);
                                if (approveStudentFatherInfoObj != null)
                                {
                                    approveStudentFatherInfoObj.ApproveStatus = "reject";
                                    approveStudentFatherInfoObj.ApproveComment = fatherInfoReject.Comment;
                                    approveStudentFatherInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentFatherInfoObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                            if (motherInfoReject != null)
                            {
                                section = "ข้อมูลมารดา";

                                TApproveStudentMotherInfo approveStudentMotherInfoObj = ctx.TApproveStudentMotherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == motherInfoReject.ID && f.StudentID == motherInfoReject.StudentID);
                                if (approveStudentMotherInfoObj != null)
                                {
                                    approveStudentMotherInfoObj.ApproveStatus = "reject";
                                    approveStudentMotherInfoObj.ApproveComment = motherInfoReject.Comment;
                                    approveStudentMotherInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentMotherInfoObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                            if (parentInfoReject != null)
                            {
                                section = "ข้อมูลผู้ปกครอง";

                                TApproveStudentParentInfo approveStudentParentInfoObj = ctx.TApproveStudentParentInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == parentInfoReject.ID && f.StudentID == parentInfoReject.StudentID);
                                if (approveStudentParentInfoObj != null)
                                {
                                    approveStudentParentInfoObj.ApproveStatus = "reject";
                                    approveStudentParentInfoObj.ApproveComment = parentInfoReject.Comment;
                                    approveStudentParentInfoObj.ApproveDate = DateTime.Now;
                                    approveStudentParentInfoObj.ApproveBy = userData.UserID;
                                }

                                ctx.SaveChanges();
                            }

                           
                        }
                        catch (Exception err2)
                        {
                           

                            success = false;
                            message = err2.Message;
                        }
                    
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, section, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectProfile(RejectData profileReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentProfile approveStudentProfilesObj = ctx.TApproveStudentProfiles.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == profileReject.ID && f.StudentID == profileReject.StudentID);
                    if (approveStudentProfilesObj != null)
                    {
                        approveStudentProfilesObj.ApproveStatus = "reject";
                        approveStudentProfilesObj.ApproveComment = profileReject.Comment;
                        approveStudentProfilesObj.ApproveDate = DateTime.Now;
                        approveStudentProfilesObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectPermanentAddress(RejectData permanentAddressReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentPermanentAddress approveStudentPermanentAddressObj = ctx.TApproveStudentPermanentAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == permanentAddressReject.ID && f.StudentID == permanentAddressReject.StudentID);
                    if (approveStudentPermanentAddressObj != null)
                    {
                        approveStudentPermanentAddressObj.ApproveStatus = "reject";
                        approveStudentPermanentAddressObj.ApproveComment = permanentAddressReject.Comment;
                        approveStudentPermanentAddressObj.ApproveDate = DateTime.Now;
                        approveStudentPermanentAddressObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectContactAddress(RejectData contactAddressReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentContactAddress approveStudentContactAddressObj = ctx.TApproveStudentContactAddresses.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == contactAddressReject.ID && f.StudentID == contactAddressReject.StudentID);
                    if (approveStudentContactAddressObj != null)
                    {
                        approveStudentContactAddressObj.ApproveStatus = "reject";
                        approveStudentContactAddressObj.ApproveComment = contactAddressReject.Comment;
                        approveStudentContactAddressObj.ApproveDate = DateTime.Now;
                        approveStudentContactAddressObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectFatherInfo(RejectData fatherInfoReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentFatherInfo approveStudentFatherInfoObj = ctx.TApproveStudentFatherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == fatherInfoReject.ID && f.StudentID == fatherInfoReject.StudentID);
                    if (approveStudentFatherInfoObj != null)
                    {
                        approveStudentFatherInfoObj.ApproveStatus = "reject";
                        approveStudentFatherInfoObj.ApproveComment = fatherInfoReject.Comment;
                        approveStudentFatherInfoObj.ApproveDate = DateTime.Now;
                        approveStudentFatherInfoObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectMotherInfo(RejectData motherInfoReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentMotherInfo approveStudentMotherInfoObj = ctx.TApproveStudentMotherInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == motherInfoReject.ID && f.StudentID == motherInfoReject.StudentID);
                    if (approveStudentMotherInfoObj != null)
                    {
                        approveStudentMotherInfoObj.ApproveStatus = "reject";
                        approveStudentMotherInfoObj.ApproveComment = motherInfoReject.Comment;
                        approveStudentMotherInfoObj.ApproveDate = DateTime.Now;
                        approveStudentMotherInfoObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object SaveRejectParentInfo(RejectData parentInfoReject)
        {
            JWTToken token = new JWTToken();
            JWTToken.userData userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            bool success = true;
            string message = "Save Successfully";

            try
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    TApproveStudentParentInfo approveStudentParentInfoObj = ctx.TApproveStudentParentInfoes.FirstOrDefault(f => f.SchoolID == schoolID && f.ID == parentInfoReject.ID && f.StudentID == parentInfoReject.StudentID);
                    if (approveStudentParentInfoObj != null)
                    {
                        approveStudentParentInfoObj.ApproveStatus = "reject";
                        approveStudentParentInfoObj.ApproveComment = parentInfoReject.Comment;
                        approveStudentParentInfoObj.ApproveDate = DateTime.Now;
                        approveStudentParentInfoObj.ApproveBy = userData.UserID;
                    }

                    ctx.SaveChanges();
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        public static string GetStudentStatus(int? studentStatusID)
        {
            string studentStatusName;
            switch (studentStatusID)
            {
                case 0: studentStatusName = "กำลังศึกษา"; break;
                case 1: studentStatusName = "จำหน่าย"; break;
                case 2: studentStatusName = "ลาออก"; break;
                case 3: studentStatusName = "พักการเรียน"; break;
                case 4: studentStatusName = "สำเร็จการศึกษา"; break;
                case 5: studentStatusName = "ขาดการติดต่อ"; break;
                case 6: studentStatusName = "พ้นสภาพ"; break;
                case 7: studentStatusName = "นักเรียนไปโครงการ"; break;
                default: studentStatusName = "กำลังศึกษา"; break;
            }

            return studentStatusName;
        }

        private static bool GetDataChanged<T>(T data)
        {
            bool dataChanged = false;
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var s = property.GetValue(data);

                if (s == null || s.GetType() == typeof(int) || s.GetType() == typeof(string) || s.GetType() == typeof(bool)) continue;

                if (s.GetType().GetGenericTypeDefinition() == typeof(FieldData<>))
                {
                    dataChanged = ((IFieldData)s).DataChanged;
                    if (dataChanged) break;
                }
            }

            return dataChanged;
        }

        // Conclusion Class
        interface IFieldData
        {
            bool DataChanged { get; set; }
        }

        public class FieldData<T> : IFieldData
        {
            public T _NewValue;

            [JsonProperty(PropertyName = "value")]
            public T Value { get; set; }

            [JsonProperty(PropertyName = "newValue")]
            public T NewValue
            {
                get { return _NewValue; }
                set
                {
                    _NewValue = value;
                    if (!Compare(Value, NewValue))
                    {
                        (DBValue, Value) = (Value, NewValue);
                        IsNewValue = true;
                        DataChanged = true;
                    }
                }
            }

            [JsonProperty(PropertyName = "isNewValue")]
            public bool IsNewValue { get; set; }

            [JsonProperty(PropertyName = "dbValue")]
            public T DBValue { get; set; }

            public bool DataChanged { get; set; }

            public bool Compare<T>(T x, T y)
            {
                return EqualityComparer<T>.Default.Equals(x, y);
            }
        }

        // profile, permanent-address, contact-address, education, father-info, mother-info, parent-info
        public class Profile
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public FieldData<string> StudentID { get; set; }

            [JsonProperty(PropertyName = "studentStatus")]
            public FieldData<string> StudentStatus { get; set; }

            [JsonProperty(PropertyName = "studentMoveInDate")]
            public FieldData<string> StudentMoveInDate { get; set; }

            [JsonProperty(PropertyName = "studentNumber")]
            public FieldData<string> StudentNumber { get; set; }

            [JsonProperty(PropertyName = "studentClass")]
            public FieldData<string> StudentClass { get; set; }

            [JsonProperty(PropertyName = "studentClassRoom")]
            public FieldData<string> StudentClassRoom { get; set; }


            [JsonProperty(PropertyName = "studentGender")]
            public FieldData<string> StudentGender { get; set; }

            [JsonProperty(PropertyName = "studentTitle")]
            public FieldData<int?> StudentTitle { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameTh")]
            public FieldData<string> StudentFirstNameTh { get; set; }

            [JsonProperty(PropertyName = "studentLastNameTh")]
            public FieldData<string> StudentLastNameTh { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameEn")]
            public FieldData<string> StudentFirstNameEn { get; set; }

            [JsonProperty(PropertyName = "studentLastNameEn")]
            public FieldData<string> StudentLastNameEn { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameOther")]
            public FieldData<string> StudentFirstNameOther { get; set; }

            [JsonProperty(PropertyName = "studentLastNameOther")]
            public FieldData<string> StudentLastNameOther { get; set; }

            [JsonProperty(PropertyName = "studentNickNameTh")]
            public FieldData<string> StudentNickNameTh { get; set; }

            [JsonProperty(PropertyName = "studentNickNameEn")]
            public FieldData<string> StudentNickNameEn { get; set; }

            [JsonProperty(PropertyName = "studentBirthday")]
            public FieldData<string> StudentBirthday { get; set; }

            [JsonProperty(PropertyName = "studentRace")]
            public FieldData<string> StudentRace { get; set; }

            [JsonProperty(PropertyName = "studentNation")]
            public FieldData<string> StudentNation { get; set; }

            [JsonProperty(PropertyName = "studentReligion")]
            public FieldData<string> StudentReligion { get; set; }

            [JsonProperty(PropertyName = "disabilityCode")]
            public FieldData<string> DisabilityCode { get; set; }

            [JsonProperty(PropertyName = "disadvantageCode")]
            public FieldData<string> DisadvantageCode { get; set; }

            [JsonProperty(PropertyName = "studentPhone")]
            public FieldData<string> StudentPhone { get; set; }

            [JsonProperty(PropertyName = "studentEmail")]
            public FieldData<string> StudentEmail { get; set; }

            [JsonProperty(PropertyName = "studentSonTotal")]
            public FieldData<int?> StudentSonTotal { get; set; }

            [JsonProperty(PropertyName = "studentSonNumber")]
            public FieldData<int?> StudentSonNumber { get; set; }

            [JsonProperty(PropertyName = "studentBrethrenStudyHere")]
            public FieldData<int?> StudentBrethrenStudyHere { get; set; }

            [JsonProperty(PropertyName = "note2")]
            public FieldData<string> Note2 { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class PermanentAddress
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "registerHomeCode")]
            public FieldData<string> RegisterHomeCode { get; set; }

            [JsonProperty(PropertyName = "registerHomeNo")]
            public FieldData<string> RegisterHomeNo { get; set; }

            [JsonProperty(PropertyName = "registerHomeSoi")]
            public FieldData<string> RegisterHomeSoi { get; set; }

            [JsonProperty(PropertyName = "registerHomeMoo")]
            public FieldData<string> RegisterHomeMoo { get; set; }

            [JsonProperty(PropertyName = "registerHomeRoad")]
            public FieldData<string> RegisterHomeRoad { get; set; }

            [JsonProperty(PropertyName = "registerHomeProvince")]
            public FieldData<int?> RegisterHomeProvince { get; set; }

            [JsonProperty(PropertyName = "registerHomeAmphoe")]
            public FieldData<int?> RegisterHomeAmphoe { get; set; }

            [JsonProperty(PropertyName = "registerHomeTombon")]
            public FieldData<int?> RegisterHomeTombon { get; set; }

            [JsonProperty(PropertyName = "registerHomePostalCode")]
            public FieldData<string> RegisterHomePostalCode { get; set; }

            [JsonProperty(PropertyName = "registerHomePhone")]
            public FieldData<string> RegisterHomePhone { get; set; }

            [JsonProperty(PropertyName = "bornFrom")]
            public FieldData<string> BornFrom { get; set; }

            [JsonProperty(PropertyName = "bornFromProvince")]
            public FieldData<int?> BornFromProvince { get; set; }

            [JsonProperty(PropertyName = "bornFromAmphoe")]
            public FieldData<int?> BornFromAmphoe { get; set; }

            [JsonProperty(PropertyName = "bornFromTombon")]
            public FieldData<int?> BornFromTombon { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class ContactAddress
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "useHouseAddress")]
            public FieldData<bool> UseHouseAddress { get; set; }

            [JsonProperty(PropertyName = "homeNo")]
            public FieldData<string> HomeNo { get; set; }

            [JsonProperty(PropertyName = "homeSoi")]
            public FieldData<string> HomeSoi { get; set; }

            [JsonProperty(PropertyName = "homeMoo")]
            public FieldData<string> HomeMoo { get; set; }

            [JsonProperty(PropertyName = "homeRoad")]
            public FieldData<string> HomeRoad { get; set; }

            [JsonProperty(PropertyName = "homeProvince")]
            public FieldData<string> HomeProvince { get; set; }

            [JsonProperty(PropertyName = "homeAmphoe")]
            public FieldData<string> HomeAmphoe { get; set; }

            [JsonProperty(PropertyName = "homeTombon")]
            public FieldData<string> HomeTombon { get; set; }

            [JsonProperty(PropertyName = "homePostalCode")]
            public FieldData<string> HomePostalCode { get; set; }

            [JsonProperty(PropertyName = "homePhone")]
            public FieldData<string> HomePhone { get; set; }

            [JsonProperty(PropertyName = "homeStayWithTitle")]
            public FieldData<int?> HomeStayWithTitle { get; set; }

            [JsonProperty(PropertyName = "homeStayWithName")]
            public FieldData<string> HomeStayWithName { get; set; }

            [JsonProperty(PropertyName = "homeStayWithLast")]
            public FieldData<string> HomeStayWithLast { get; set; }

            [JsonProperty(PropertyName = "homeStayWithEmergencyCall")]
            public FieldData<string> HomeStayWithEmergencyCall { get; set; }

            [JsonProperty(PropertyName = "homeStayWithEmergencyEmail")]
            public FieldData<string> HomeStayWithEmergencyEmail { get; set; }

            [JsonProperty(PropertyName = "homeFriendName")]
            public FieldData<string> HomeFriendName { get; set; }

            [JsonProperty(PropertyName = "homeFriendLastName")]
            public FieldData<string> HomeFriendLastName { get; set; }

            [JsonProperty(PropertyName = "homeFriendPhone")]
            public FieldData<string> HomeFriendPhone { get; set; }

            [JsonProperty(PropertyName = "homeHomeType")]
            public FieldData<int?> HomeHomeType { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class Education
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "oldSchoolName")]
            public FieldData<string> OldSchoolName { get; set; }

            [JsonProperty(PropertyName = "oldSchoolLocation")]
            public FieldData<string> OldSchoolLocation { get; set; }

            [JsonProperty(PropertyName = "oldSchoolGPA")]
            public FieldData<string> OldSchoolGPA { get; set; }

            [JsonProperty(PropertyName = "credit")]
            public FieldData<string> Credit { get; set; }

            [JsonProperty(PropertyName = "oldSchoolGraduated")]
            public FieldData<string> OldSchoolGraduated { get; set; }

            [JsonProperty(PropertyName = "moveOutReason")]
            public FieldData<string> MoveOutReason { get; set; }
        }

        public class FatherInfo
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "fatherTitle")]
            public FieldData<string> FatherTitle { get; set; }

            [JsonProperty(PropertyName = "fatherName")]
            public FieldData<string> FatherName { get; set; }

            [JsonProperty(PropertyName = "fatherLastName")]
            public FieldData<string> FatherLastName { get; set; }

            [JsonProperty(PropertyName = "fatherNameEn")]
            public FieldData<string> FatherNameEn { get; set; }

            [JsonProperty(PropertyName = "fatherNameLastEn")]
            public FieldData<string> FatherNameLastEn { get; set; }

            [JsonProperty(PropertyName = "fatherBirthday")]
            public FieldData<string> FatherBirthday { get; set; }

            [JsonProperty(PropertyName = "fatherIdentification")]
            public FieldData<string> FatherIdentification { get; set; }

            [JsonProperty(PropertyName = "fatherRace")]
            public FieldData<string> FatherRace { get; set; }

            [JsonProperty(PropertyName = "fatherNation")]
            public FieldData<string> FatherNation { get; set; }

            [JsonProperty(PropertyName = "fatherReligion")]
            public FieldData<string> FatherReligion { get; set; }

            [JsonProperty(PropertyName = "fatherGraduated")]
            public FieldData<int?> FatherGraduated { get; set; }

            [JsonProperty(PropertyName = "fatherHomeNo")]
            public FieldData<string> FatherHomeNo { get; set; }

            [JsonProperty(PropertyName = "fatherSoi")]
            public FieldData<string> FatherSoi { get; set; }

            [JsonProperty(PropertyName = "fatherMoo")]
            public FieldData<string> FatherMoo { get; set; }

            [JsonProperty(PropertyName = "fatherRoad")]
            public FieldData<string> FatherRoad { get; set; }

            [JsonProperty(PropertyName = "fatherProvince")]
            public FieldData<string> FatherProvince { get; set; }

            [JsonProperty(PropertyName = "fatherAmphoe")]
            public FieldData<string> FatherAmphoe { get; set; }

            [JsonProperty(PropertyName = "fatherTombon")]
            public FieldData<string> FatherTombon { get; set; }

            [JsonProperty(PropertyName = "fatherPostalCode")]
            public FieldData<string> FatherPostalCode { get; set; }

            [JsonProperty(PropertyName = "fatherJob")]
            public FieldData<string> FatherJob { get; set; }

            [JsonProperty(PropertyName = "fatherIncome")]
            public FieldData<double?> FatherIncome { get; set; }

            [JsonProperty(PropertyName = "fatherWorkPlace")]
            public FieldData<string> FatherWorkPlace { get; set; }

            [JsonProperty(PropertyName = "fatherPhone")]
            public FieldData<string> FatherPhone { get; set; }

            [JsonProperty(PropertyName = "fatherPhone2")]
            public FieldData<string> FatherPhone2 { get; set; }

            [JsonProperty(PropertyName = "fatherPhone3")]
            public FieldData<string> FatherPhone3 { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class MotherInfo
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "motherTitle")]
            public FieldData<string> MotherTitle { get; set; }

            [JsonProperty(PropertyName = "motherName")]
            public FieldData<string> MotherName { get; set; }

            [JsonProperty(PropertyName = "motherLastName")]
            public FieldData<string> MotherLastName { get; set; }

            [JsonProperty(PropertyName = "motherNameEn")]
            public FieldData<string> MotherNameEn { get; set; }

            [JsonProperty(PropertyName = "motherNameLastEn")]
            public FieldData<string> MotherNameLastEn { get; set; }

            [JsonProperty(PropertyName = "motherBirthday")]
            public FieldData<string> MotherBirthday { get; set; }

            [JsonProperty(PropertyName = "motherIdentification")]
            public FieldData<string> MotherIdentification { get; set; }

            [JsonProperty(PropertyName = "motherRace")]
            public FieldData<string> MotherRace { get; set; }

            [JsonProperty(PropertyName = "motherNation")]
            public FieldData<string> MotherNation { get; set; }

            [JsonProperty(PropertyName = "motherReligion")]
            public FieldData<string> MotherReligion { get; set; }

            [JsonProperty(PropertyName = "motherGraduated")]
            public FieldData<int?> MotherGraduated { get; set; }

            [JsonProperty(PropertyName = "isFatherAddress")]
            public FieldData<bool?> IsFatherAddress { get; set; }

            [JsonProperty(PropertyName = "motherHomeNo")]
            public FieldData<string> MotherHomeNo { get; set; }

            [JsonProperty(PropertyName = "motherSoi")]
            public FieldData<string> MotherSoi { get; set; }

            [JsonProperty(PropertyName = "motherMoo")]
            public FieldData<string> MotherMoo { get; set; }

            [JsonProperty(PropertyName = "motherRoad")]
            public FieldData<string> MotherRoad { get; set; }

            [JsonProperty(PropertyName = "motherProvince")]
            public FieldData<string> MotherProvince { get; set; }

            [JsonProperty(PropertyName = "motherAmphoe")]
            public FieldData<string> MotherAmphoe { get; set; }

            [JsonProperty(PropertyName = "motherTombon")]
            public FieldData<string> MotherTombon { get; set; }

            [JsonProperty(PropertyName = "motherPostalCode")]
            public FieldData<string> MotherPostalCode { get; set; }

            [JsonProperty(PropertyName = "motherJob")]
            public FieldData<string> MotherJob { get; set; }

            [JsonProperty(PropertyName = "motherIncome")]
            public FieldData<double?> MotherIncome { get; set; }

            [JsonProperty(PropertyName = "motherWorkPlace")]
            public FieldData<string> MotherWorkPlace { get; set; }

            [JsonProperty(PropertyName = "motherPhone")]
            public FieldData<string> MotherPhone { get; set; }

            [JsonProperty(PropertyName = "motherPhone2")]
            public FieldData<string> MotherPhone2 { get; set; }

            [JsonProperty(PropertyName = "motherPhone3")]
            public FieldData<string> MotherPhone3 { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class ParentInfo
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "sId")]
            public int SID { get; set; }

            [JsonProperty(PropertyName = "copyDataFrom")]
            public FieldData<int?> CopyDataFrom { get; set; }

            [JsonProperty(PropertyName = "parentRelate")]
            public FieldData<string> ParentRelate { get; set; }

            [JsonProperty(PropertyName = "parentTitle")]
            public FieldData<string> ParentTitle { get; set; }

            [JsonProperty(PropertyName = "parentName")]
            public FieldData<string> ParentName { get; set; }

            [JsonProperty(PropertyName = "parentLastName")]
            public FieldData<string> ParentLastName { get; set; }

            [JsonProperty(PropertyName = "parentNameEn")]
            public FieldData<string> ParentNameEn { get; set; }

            [JsonProperty(PropertyName = "parentNameLastEn")]
            public FieldData<string> ParentNameLastEn { get; set; }

            [JsonProperty(PropertyName = "parentBirthday")]
            public FieldData<string> ParentBirthday { get; set; }

            [JsonProperty(PropertyName = "parentIdentification")]
            public FieldData<string> ParentIdentification { get; set; }

            [JsonProperty(PropertyName = "parentRace")]
            public FieldData<string> ParentRace { get; set; }

            [JsonProperty(PropertyName = "parentNation")]
            public FieldData<string> ParentNation { get; set; }

            [JsonProperty(PropertyName = "parentReligion")]
            public FieldData<string> ParentReligion { get; set; }

            [JsonProperty(PropertyName = "parentGraduated")]
            public FieldData<int?> ParentGraduated { get; set; }

            [JsonProperty(PropertyName = "parentHomeNo")]
            public FieldData<string> ParentHomeNo { get; set; }

            [JsonProperty(PropertyName = "parentSoi")]
            public FieldData<string> ParentSoi { get; set; }

            [JsonProperty(PropertyName = "parentMoo")]
            public FieldData<string> ParentMoo { get; set; }

            [JsonProperty(PropertyName = "parentRoad")]
            public FieldData<string> ParentRoad { get; set; }

            [JsonProperty(PropertyName = "parentProvince")]
            public FieldData<string> ParentProvince { get; set; }

            [JsonProperty(PropertyName = "parentAmphoe")]
            public FieldData<string> ParentAmphoe { get; set; }

            [JsonProperty(PropertyName = "parentTombon")]
            public FieldData<string> ParentTombon { get; set; }

            [JsonProperty(PropertyName = "parentPostalCode")]
            public FieldData<string> ParentPostalCode { get; set; }

            [JsonProperty(PropertyName = "tuitionReimbursement")]
            public FieldData<int?> TuitionReimbursement { get; set; }

            [JsonProperty(PropertyName = "parentStatus")]
            public FieldData<int?> ParentStatus { get; set; }

            [JsonProperty(PropertyName = "parentJob")]
            public FieldData<string> ParentJob { get; set; }

            [JsonProperty(PropertyName = "parentIncome")]
            public FieldData<double?> ParentIncome { get; set; }

            [JsonProperty(PropertyName = "parentWorkPlace")]
            public FieldData<string> ParentWorkPlace { get; set; }

            [JsonProperty(PropertyName = "parentPhone")]
            public FieldData<string> ParentPhone { get; set; }

            [JsonProperty(PropertyName = "parentPhone2")]
            public FieldData<string> ParentPhone2 { get; set; }

            [JsonProperty(PropertyName = "parentPhone3")]
            public FieldData<string> ParentPhone3 { get; set; }

            [JsonProperty(PropertyName = "approveStatus")]
            public string ApproveStatus { get; set; }

            [JsonProperty(PropertyName = "requestUpdateData")]
            public bool? RequestUpdateData { get; set; }

            [JsonProperty(PropertyName = "dataChanged")]
            public bool? DataChanged { get; set; }
        }

        public class StudentData
        {
            public StudentData()
            {
                Profile = new Profile();
                PermanentAddress = new PermanentAddress();
                ContactAddress = new ContactAddress();
                Education = new Education();
                FatherInfo = new FatherInfo();
                MotherInfo = new MotherInfo();
                ParentInfo = new ParentInfo();
            }

            // ประวัติส่วนตัว
            [JsonProperty(PropertyName = "profile")]
            public Profile Profile { get; set; }

            // ที่อยู่ตามทะเบียนบ้าน
            [JsonProperty(PropertyName = "permanentAddress")]
            public PermanentAddress PermanentAddress { get; set; }

            // ที่อยู่ปัจจุบัน
            [JsonProperty(PropertyName = "contactAddress")]
            public ContactAddress ContactAddress { get; set; }

            // ประวัติการศึกษา
            [JsonProperty(PropertyName = "education")]
            public Education Education { get; set; }

            // ข้อมูลบิดา
            [JsonProperty(PropertyName = "fatherInfo")]
            public FatherInfo FatherInfo { get; set; }

            // ข้อมูลมารดา
            [JsonProperty(PropertyName = "motherInfo")]
            public MotherInfo MotherInfo { get; set; }

            // ข้อมูลผู้ปกครอง
            [JsonProperty(PropertyName = "parentInfo")]
            public ParentInfo ParentInfo { get; set; }
        }

        public class ProfileEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "studentGender")]
            public string StudentGender { get; set; }

            [JsonProperty(PropertyName = "studentTitle")]
            public int? StudentTitle { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameTh")]
            public string StudentFirstNameTh { get; set; }

            [JsonProperty(PropertyName = "studentLastNameTh")]
            public string StudentLastNameTh { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameEn")]
            public string StudentFirstNameEn { get; set; }

            [JsonProperty(PropertyName = "studentLastNameEn")]
            public string StudentLastNameEn { get; set; }

            [JsonProperty(PropertyName = "studentFirstNameOther")]
            public string StudentFirstNameOther { get; set; }

            [JsonProperty(PropertyName = "studentLastNameOther")]
            public string StudentLastNameOther { get; set; }

            [JsonProperty(PropertyName = "studentNickNameTh")]
            public string StudentNickNameTh { get; set; }

            [JsonProperty(PropertyName = "studentNickNameEn")]
            public string StudentNickNameEn { get; set; }

            [JsonProperty(PropertyName = "studentBirthday")]
            public string StudentBirthday { get; set; }

            [JsonProperty(PropertyName = "studentRace")]
            public string StudentRace { get; set; }

            [JsonProperty(PropertyName = "studentNation")]
            public string StudentNation { get; set; }

            [JsonProperty(PropertyName = "studentReligion")]
            public string StudentReligion { get; set; }

            [JsonProperty(PropertyName = "disabilityCode")]
            public string DisabilityCode { get; set; }

            [JsonProperty(PropertyName = "disadvantageCode")]
            public string DisadvantageCode { get; set; }

            [JsonProperty(PropertyName = "studentPhone")]
            public string StudentPhone { get; set; }

            [JsonProperty(PropertyName = "studentEmail")]
            public string StudentEmail { get; set; }

            [JsonProperty(PropertyName = "studentSonTotal")]
            public int? StudentSonTotal { get; set; }

            [JsonProperty(PropertyName = "studentSonNumber")]
            public int? StudentSonNumber { get; set; }

            [JsonProperty(PropertyName = "studentBrethrenStudyHere")]
            public int? StudentBrethrenStudyHere { get; set; }

            [JsonProperty(PropertyName = "note2")]
            public string Note2 { get; set; }
        }

        public class PermanentAddressEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "registerHomeCode")]
            public string RegisterHomeCode { get; set; }

            [JsonProperty(PropertyName = "registerHomeNo")]
            public string RegisterHomeNo { get; set; }

            [JsonProperty(PropertyName = "registerHomeSoi")]
            public string RegisterHomeSoi { get; set; }

            [JsonProperty(PropertyName = "registerHomeMoo")]
            public string RegisterHomeMoo { get; set; }

            [JsonProperty(PropertyName = "registerHomeRoad")]
            public string RegisterHomeRoad { get; set; }

            [JsonProperty(PropertyName = "registerHomeProvince")]
            public int? RegisterHomeProvince { get; set; }

            [JsonProperty(PropertyName = "registerHomeAmphoe")]
            public int? RegisterHomeAmphoe { get; set; }

            [JsonProperty(PropertyName = "registerHomeTombon")]
            public int? RegisterHomeTombon { get; set; }

            [JsonProperty(PropertyName = "registerHomePostalCode")]
            public string RegisterHomePostalCode { get; set; }

            [JsonProperty(PropertyName = "registerHomePhone")]
            public string RegisterHomePhone { get; set; }

            [JsonProperty(PropertyName = "bornFrom")]
            public string BornFrom { get; set; }

            [JsonProperty(PropertyName = "bornFromProvince")]
            public int? BornFromProvince { get; set; }

            [JsonProperty(PropertyName = "bornFromAmphoe")]
            public int? BornFromAmphoe { get; set; }

            [JsonProperty(PropertyName = "bornFromTombon")]
            public int? BornFromTombon { get; set; }
        }

        public class ContactAddressEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "homeNo")]
            public string HomeNo { get; set; }

            [JsonProperty(PropertyName = "homeSoi")]
            public string HomeSoi { get; set; }

            [JsonProperty(PropertyName = "homeMoo")]
            public string HomeMoo { get; set; }

            [JsonProperty(PropertyName = "homeRoad")]
            public string HomeRoad { get; set; }

            [JsonProperty(PropertyName = "homeProvince")]
            public string HomeProvince { get; set; }

            [JsonProperty(PropertyName = "homeAmphoe")]
            public string HomeAmphoe { get; set; }

            [JsonProperty(PropertyName = "homeTombon")]
            public string HomeTombon { get; set; }

            [JsonProperty(PropertyName = "homePostalCode")]
            public string HomePostalCode { get; set; }

            [JsonProperty(PropertyName = "homePhone")]
            public string HomePhone { get; set; }

            [JsonProperty(PropertyName = "homeStayWithTitle")]
            public int? HomeStayWithTitle { get; set; }

            [JsonProperty(PropertyName = "homeStayWithName")]
            public string HomeStayWithName { get; set; }

            [JsonProperty(PropertyName = "homeStayWithLast")]
            public string HomeStayWithLast { get; set; }

            [JsonProperty(PropertyName = "homeStayWithEmergencyCall")]
            public string HomeStayWithEmergencyCall { get; set; }

            [JsonProperty(PropertyName = "homeStayWithEmergencyEmail")]
            public string HomeStayWithEmergencyEmail { get; set; }

            [JsonProperty(PropertyName = "homeFriendName")]
            public string HomeFriendName { get; set; }

            [JsonProperty(PropertyName = "homeFriendLastName")]
            public string HomeFriendLastName { get; set; }

            [JsonProperty(PropertyName = "homeFriendPhone")]
            public string HomeFriendPhone { get; set; }

            [JsonProperty(PropertyName = "homeHomeType")]
            public int? HomeHomeType { get; set; }
        }

        public class FatherInfoEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "fatherTitle")]
            public string FatherTitle { get; set; }

            [JsonProperty(PropertyName = "fatherName")]
            public string FatherName { get; set; }

            [JsonProperty(PropertyName = "fatherLastName")]
            public string FatherLastName { get; set; }

            [JsonProperty(PropertyName = "fatherNameEn")]
            public string FatherNameEn { get; set; }

            [JsonProperty(PropertyName = "fatherNameLastEn")]
            public string FatherNameLastEn { get; set; }

            [JsonProperty(PropertyName = "fatherBirthday")]
            public string FatherBirthday { get; set; }

            [JsonProperty(PropertyName = "fatherIdentification")]
            public string FatherIdentification { get; set; }

            [JsonProperty(PropertyName = "fatherRace")]
            public string FatherRace { get; set; }

            [JsonProperty(PropertyName = "fatherNation")]
            public string FatherNation { get; set; }

            [JsonProperty(PropertyName = "fatherReligion")]
            public string FatherReligion { get; set; }

            [JsonProperty(PropertyName = "fatherGraduated")]
            public int? FatherGraduated { get; set; }

            [JsonProperty(PropertyName = "fatherHomeNo")]
            public string FatherHomeNo { get; set; }

            [JsonProperty(PropertyName = "fatherSoi")]
            public string FatherSoi { get; set; }

            [JsonProperty(PropertyName = "fatherMoo")]
            public string FatherMoo { get; set; }

            [JsonProperty(PropertyName = "fatherRoad")]
            public string FatherRoad { get; set; }

            [JsonProperty(PropertyName = "fatherProvince")]
            public string FatherProvince { get; set; }

            [JsonProperty(PropertyName = "fatherAmphoe")]
            public string FatherAmphoe { get; set; }

            [JsonProperty(PropertyName = "fatherTombon")]
            public string FatherTombon { get; set; }

            [JsonProperty(PropertyName = "fatherPostalCode")]
            public string FatherPostalCode { get; set; }

            [JsonProperty(PropertyName = "fatherJob")]
            public string FatherJob { get; set; }

            [JsonProperty(PropertyName = "fatherIncome")]
            public double? FatherIncome { get; set; }

            [JsonProperty(PropertyName = "fatherWorkPlace")]
            public string FatherWorkPlace { get; set; }

            [JsonProperty(PropertyName = "fatherPhone")]
            public string FatherPhone { get; set; }

            [JsonProperty(PropertyName = "fatherPhone2")]
            public string FatherPhone2 { get; set; }

            [JsonProperty(PropertyName = "fatherPhone3")]
            public string FatherPhone3 { get; set; }
        }

        public class MotherInfoEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "motherTitle")]
            public string MotherTitle { get; set; }

            [JsonProperty(PropertyName = "motherName")]
            public string MotherName { get; set; }

            [JsonProperty(PropertyName = "motherLastName")]
            public string MotherLastName { get; set; }

            [JsonProperty(PropertyName = "motherNameEn")]
            public string MotherNameEn { get; set; }

            [JsonProperty(PropertyName = "motherNameLastEn")]
            public string MotherNameLastEn { get; set; }

            [JsonProperty(PropertyName = "motherBirthday")]
            public string MotherBirthday { get; set; }

            [JsonProperty(PropertyName = "motherIdentification")]
            public string MotherIdentification { get; set; }

            [JsonProperty(PropertyName = "motherRace")]
            public string MotherRace { get; set; }

            [JsonProperty(PropertyName = "motherNation")]
            public string MotherNation { get; set; }

            [JsonProperty(PropertyName = "motherReligion")]
            public string MotherReligion { get; set; }

            [JsonProperty(PropertyName = "motherGraduated")]
            public int? MotherGraduated { get; set; }

            [JsonProperty(PropertyName = "motherHomeNo")]
            public string MotherHomeNo { get; set; }

            [JsonProperty(PropertyName = "motherSoi")]
            public string MotherSoi { get; set; }

            [JsonProperty(PropertyName = "motherMoo")]
            public string MotherMoo { get; set; }

            [JsonProperty(PropertyName = "motherRoad")]
            public string MotherRoad { get; set; }

            [JsonProperty(PropertyName = "motherProvince")]
            public string MotherProvince { get; set; }

            [JsonProperty(PropertyName = "motherAmphoe")]
            public string MotherAmphoe { get; set; }

            [JsonProperty(PropertyName = "motherTombon")]
            public string MotherTombon { get; set; }

            [JsonProperty(PropertyName = "motherPostalCode")]
            public string MotherPostalCode { get; set; }

            [JsonProperty(PropertyName = "motherJob")]
            public string MotherJob { get; set; }

            [JsonProperty(PropertyName = "motherIncome")]
            public double? MotherIncome { get; set; }

            [JsonProperty(PropertyName = "motherWorkPlace")]
            public string MotherWorkPlace { get; set; }

            [JsonProperty(PropertyName = "motherPhone")]
            public string MotherPhone { get; set; }

            [JsonProperty(PropertyName = "motherPhone2")]
            public string MotherPhone2 { get; set; }

            [JsonProperty(PropertyName = "motherPhone3")]
            public string MotherPhone3 { get; set; }
        }

        public class ParentInfoEdited
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "parentRelate")]
            public string ParentRelate { get; set; }

            [JsonProperty(PropertyName = "parentTitle")]
            public string ParentTitle { get; set; }

            [JsonProperty(PropertyName = "parentName")]
            public string ParentName { get; set; }

            [JsonProperty(PropertyName = "parentLastName")]
            public string ParentLastName { get; set; }

            [JsonProperty(PropertyName = "parentNameEn")]
            public string ParentNameEn { get; set; }

            [JsonProperty(PropertyName = "parentNameLastEn")]
            public string ParentNameLastEn { get; set; }

            [JsonProperty(PropertyName = "parentBirthday")]
            public string ParentBirthday { get; set; }

            [JsonProperty(PropertyName = "parentIdentification")]
            public string ParentIdentification { get; set; }

            [JsonProperty(PropertyName = "parentRace")]
            public string ParentRace { get; set; }

            [JsonProperty(PropertyName = "parentNation")]
            public string ParentNation { get; set; }

            [JsonProperty(PropertyName = "parentReligion")]
            public string ParentReligion { get; set; }

            [JsonProperty(PropertyName = "parentGraduated")]
            public int? ParentGraduated { get; set; }

            [JsonProperty(PropertyName = "parentHomeNo")]
            public string ParentHomeNo { get; set; }

            [JsonProperty(PropertyName = "parentSoi")]
            public string ParentSoi { get; set; }

            [JsonProperty(PropertyName = "parentMoo")]
            public string ParentMoo { get; set; }

            [JsonProperty(PropertyName = "parentRoad")]
            public string ParentRoad { get; set; }

            [JsonProperty(PropertyName = "parentProvince")]
            public string ParentProvince { get; set; }

            [JsonProperty(PropertyName = "parentAmphoe")]
            public string ParentAmphoe { get; set; }

            [JsonProperty(PropertyName = "parentTombon")]
            public string ParentTombon { get; set; }

            [JsonProperty(PropertyName = "parentPostalCode")]
            public string ParentPostalCode { get; set; }

            [JsonProperty(PropertyName = "tuitionReimbursement")]
            public int? TuitionReimbursement { get; set; }

            [JsonProperty(PropertyName = "parentStatus")]
            public int? ParentStatus { get; set; }

            [JsonProperty(PropertyName = "parentJob")]
            public string ParentJob { get; set; }

            [JsonProperty(PropertyName = "parentIncome")]
            public double? ParentIncome { get; set; }

            [JsonProperty(PropertyName = "parentWorkPlace")]
            public string ParentWorkPlace { get; set; }

            [JsonProperty(PropertyName = "parentPhone")]
            public string ParentPhone { get; set; }

            [JsonProperty(PropertyName = "parentPhone2")]
            public string ParentPhone2 { get; set; }

            [JsonProperty(PropertyName = "parentPhone3")]
            public string ParentPhone3 { get; set; }
        }

        public class RejectData
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "comment")]
            public string Comment { get; set; }
        }
    }
}