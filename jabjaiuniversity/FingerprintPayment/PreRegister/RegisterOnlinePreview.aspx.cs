using AccountingEntity;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnlinePreview : System.Web.UI.Page
    {
        protected string NoAssumptionSriracha = "";
        protected string NoSuankularbNonthaburi = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            string specialScript = "";
            int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
            if (schoolID != 229)
            {
                NoAssumptionSriracha = "no-assumption-sriracha";
            }
            if (schoolID != 1043)
            {
                NoSuankularbNonthaburi = "no-suankularb-nonthaburi";
            }
            else
            {
                specialScript = string.Format(@"$('.re-order').each(function(i){{$(this).text(parseInt($(this).text())+3);}});");
            }

            //Script Required Field
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = string.Format(@"SELECT rfi.InputFieldName, rfi.CategoryID
FROM TPreRegisterRequiredFieldInitiate rfi 
LEFT JOIN TPreRegisterRequiredField rf ON rfi.VFIID = rf.VFIID AND rfi.CategoryID = rf.CategoryID AND rf.SchoolID = {0}
WHERE rfi.IsDel = 0 AND ISNULL(rf.Status, rfi.DefaultStatus) = 1 {1}{2}", schoolID, (schoolID != 229 ? " AND rfi.VFIID NOT IN (18, 19)" : ""), (schoolID != 1043 ? " AND rfi.VFIID NOT IN (166, 167, 168)" : ""));
                List<RequiredFieldData> listElement = en.Database.SqlQuery<RequiredFieldData>(query).ToList();

                string script = "";
                foreach (var e in listElement)
                {
                    switch (e.CategoryID)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            script += string.Format(@"AddRequiredRulesvalidation('{0}', {1});", e.InputFieldName, e.CategoryID);
                            break;
                        case 9:
                            // Exception
                            if (schoolID == 1043 && (e.InputFieldName == "fileDocument06" || e.InputFieldName == "fileDocument13" || e.InputFieldName == "fileDocument024")) { script += string.Format(@"EnableFileUploadDocument('#{0}');", e.InputFieldName); continue; }

                            script += string.Format(@"AddRequiredRulesvalidation('#{0}', {1}); EnableFileUploadDocument('#{0}');", e.InputFieldName, e.CategoryID);
                            break;
                    }
                }

                ltrScriptRequiredField.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0} {1} ReNoFileUploadDocument(); ReapplyTableStriping();
        }});
    </script>", script, specialScript);
            }

        }

        [WebMethod(EnableSession = true)]
        public static string SaveData(EntityRegisterOnline register)
        {
            bool success = true;
            string message = "Save Successfully";

            string logMessagePattern = @"[SchoolEntities:{0}], [RegisterData:{1}], [ErrorLine:{2}], [ErrorMessage:{3}], [StackTrace:{4}]";

            try
            {
                string registerCode = "";
                string examCode = "";
                int registerID = 0;

                if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
                {
                    try
                    {
                        int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);

                        using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                        {
                            int registerYear = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineYear"]);
                            int registerYearBE = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineYearBE"]);

                            string rf = (string)HttpContext.Current.Session["RegisterF"];

                            if (string.IsNullOrEmpty(rf))
                            {
                                int existIDCard = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registerYear && w.sIdentification == register.StudentIDCard && w.cDel != 1).Count();
                                if (existIDCard > 0)
                                {
                                    success = false;
                                    message = "รหัสบัตรประชาชนนี้มีการใช้งานไปแล้ว";
                                }

                                if (!register.Page01Saved || !register.Page02Saved || !register.Page03Saved || !register.Page04Saved || !register.Page05Saved || !register.Page06Saved || !register.Page07Saved || !register.Page08Saved || !register.Page09Saved || !register.Page10Saved || !register.Page11Saved || !register.Page12Saved)
                                {
                                    success = false;
                                    message = "กรุณากรอกให้ครบทุกหน้า";
                                }

                                if (success)
                                {
                                    //int registerID = 0; // (int)(en.TPreRegisters.Count() == 0 ? 1 : en.TPreRegisters.Max(m => m.preRegisterId) + 1);

                                    var subLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == register.Class).FirstOrDefault();
                                    string levelName = en.TLevels.Where(w => w.SchoolID == schoolID && w.LevelID == subLevel.nTLevel).FirstOrDefault().LevelName;

                                    int yearRunID = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registerYear).Count() + 1;
                                    registerCode = string.Format(@"{0}{1:D4}", registerYearBE.ToString().Substring(2, 2), yearRunID); // [year{2}][runID] = 630001

                                    int classRunID = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registerYear && w.optionLevel == register.Class && w.RegisterPlanSetupID == register.EduProgram).Count() + 1;
                                    examCode = ComFunction.GenerateExamCode2(registerYearBE, levelName, subLevel.SubLevel, register.EduProgram, classRunID); // [year{2}]-[levelID][subLevelID]-[planID]-[runID] = 63-11-01-001

                                    TPreRegister preRegister = new TPreRegister
                                    {
                                        //preRegisterId = registerID,
                                        registerYear = registerYear,
                                        sStudentID = registerCode,
                                        registerCode = yearRunID.ToString(),
                                        ExamCode = examCode,

                                        // Page 03
                                        StudentType = register.StudentType,
                                        optionCourse = null, // 1 = ปกติ, 3 = ทวิภาคี
                                        optionLevel = register.Class, // nTSubLevel
                                        RegisterPlanSetupID = register.EduProgram,
                                        MainPlan = register.MainPlan,
                                        BackupPlans = register.BackupPlans,
                                        optionTime = register.OptionTime, // 1 = เช้า, 2 = บ่าย 1, 3 = บ่าย 2
                                        optionBranch = register.OptionBranch,

                                        // Page 04
                                        StudentCategory = register.StudentCategory.NullIfWhiteSpace(),
                                        StudentTitle = register.StudentTitle,
                                        sName = register.StudentName.NullIfWhiteSpace(),
                                        sLastname = register.StudentLastname.NullIfWhiteSpace(),
                                        sStudentNameEN = register.StudentNameEn.NullIfWhiteSpace(),
                                        sStudentLastEN = register.StudentLastnameEn.NullIfWhiteSpace(),
                                        sNickName = register.StudentNickName.NullIfWhiteSpace(),
                                        sNickNameEN = register.StudentNickNameEn.NullIfWhiteSpace(),
                                        cSex = register.StudentSex.NullIfWhiteSpace(),
                                        dBirth = string.IsNullOrEmpty(register.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(register.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH")),
                                        sIdentification = register.StudentIDCard.NullIfWhiteSpace(),
                                        sStudentIdCardNumber = register.StudentIDCard.NullIfWhiteSpace(),
                                        sStudentRace = register.StudentRace.NullIfWhiteSpace(),
                                        sStudentNation = register.StudentNation.NullIfWhiteSpace(),
                                        sStudentReligion = register.StudentReligion.NullIfWhiteSpace(),
                                        nSonTotal = register.StudentSonTotal,
                                        nSonNumber = register.StudentSonNumber,

                                        // Page 05
                                        sStudentHomeRegisterCode = register.HouseCode.NullIfWhiteSpace(),
                                        houseRegistrationNumber = register.HouseHomeNumber.NullIfWhiteSpace(),
                                        houseRegistrationSoy = register.HouseAlley.NullIfWhiteSpace(),
                                        houseRegistrationMuu = register.HouseVillageNo.NullIfWhiteSpace(),
                                        houseRegistrationRoad = register.HouseRoad.NullIfWhiteSpace(),
                                        houseRegistrationProvince = register.HouseProvince,
                                        houseRegistrationAumpher = register.HouseDistrict,
                                        houseRegistrationTumbon = register.HouseSubDistrict,
                                        houseRegistrationPost = register.HousePostalCode.NullIfWhiteSpace(),
                                        houseRegistrationPhone = register.HousePhone.NullIfWhiteSpace(),
                                        sPhone = register.HousePhone.NullIfWhiteSpace(),
                                        sEmail = register.HouseEmail.NullIfWhiteSpace(),

                                        // Page 06
                                        sStudentHomeNumber = register.StudentHomeNumber.NullIfWhiteSpace(),
                                        sStudentSoy = register.StudentAlley.NullIfWhiteSpace(),
                                        sStudentMuu = register.StudentVillageNo.NullIfWhiteSpace(),
                                        sStudentRoad = register.StudentRoad.NullIfWhiteSpace(),
                                        sStudentProvince = register.StudentProvince.NullIfWhiteSpace(),
                                        sStudentAumpher = register.StudentDistrict.NullIfWhiteSpace(),
                                        sStudentTumbon = register.StudentSubDistrict.NullIfWhiteSpace(),
                                        sStudentPost = register.StudentPostalCode.NullIfWhiteSpace(),
                                        sStudentHousePhone = register.StudentHousePhone.NullIfWhiteSpace(),
                                        stayWithEmail = register.StudentHouseEmail.NullIfWhiteSpace(),
                                        stayWithTitle = register.StudentStayWithTitle,
                                        stayWithName = register.StudentStayWithName.NullIfWhiteSpace(),
                                        stayWithLast = register.StudentStayWithLast.NullIfWhiteSpace(),
                                        stayWithEmergencyCall = register.StudentStayWithEmergencyCall.NullIfWhiteSpace(),
                                        HomeType = register.StudentHomeType == 0 ? null : register.StudentHomeType,
                                        friendName = register.StudentNeighborName.NullIfWhiteSpace(),
                                        friendLastName = register.StudentNeighborLastName.NullIfWhiteSpace(),
                                        friendSubLevel = register.StudentNeighborSubLevel,
                                        friendPhone = register.StudentNeighborPhone.NullIfWhiteSpace(),

                                        // Page 07
                                        FatherTitle = register.FatherTitle,
                                        sFatherFirstName = register.FatherName.NullIfWhiteSpace(),
                                        sFatherLastName = register.FatherLastname.NullIfWhiteSpace(),
                                        sFatherNameEN = register.FatherNameEn.NullIfWhiteSpace(),
                                        sFatherLastEN = register.FatherLastnameEn.NullIfWhiteSpace(),
                                        dFatherBirthDay = string.IsNullOrEmpty(register.FatherBirthday) ? (DateTime?)null : DateTime.ParseExact(register.FatherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH")),
                                        sFatherIdCardNumber = register.FatherIDCard.NullIfWhiteSpace(),
                                        sFatherRace = register.FatherRace.NullIfWhiteSpace(),
                                        sFatherNation = register.FatherNation.NullIfWhiteSpace(),
                                        sFatherReligion = register.FatherReligion.NullIfWhiteSpace(),
                                        sFatherGraduated = register.FatherEducational,
                                        sFatherHomeNumber = register.FatherHomeNumber.NullIfWhiteSpace(),
                                        sFatherSoy = register.FatherAlley.NullIfWhiteSpace(),
                                        sFatherMuu = register.FatherVillageNo.NullIfWhiteSpace(),
                                        sFatherRoad = register.FatherRoad.NullIfWhiteSpace(),
                                        sFatherProvince = register.FatherProvince.NullIfWhiteSpace(),
                                        sFatherAumpher = register.FatherDistrict.NullIfWhiteSpace(),
                                        sFatherTumbon = register.FatherSubDistrict.NullIfWhiteSpace(),
                                        sFatherPost = register.FatherPostalCode.NullIfWhiteSpace(),
                                        sFatherJob = register.FatherCareer.NullIfWhiteSpace(),
                                        sFatherWorkPlace = register.FatherWorkplace.NullIfWhiteSpace(),
                                        nFatherIncome = register.FatherMonthIncome, //
                                        FatherAnnualIncome = register.FatherYearIncome.NullIfWhiteSpace(),
                                        sFatherPhone = register.FatherHousePhone.NullIfWhiteSpace(),
                                        sFatherPhone2 = register.FatherMobilePhone.NullIfWhiteSpace(),
                                        sFatherPhone3 = register.FatherWorkPhone.NullIfWhiteSpace(),
                                        FatherEmail = register.FatherEmail.NullIfWhiteSpace(),

                                        // Page 08
                                        MotherTitle = register.MotherTitle,
                                        sMotherFirstName = register.MotherName.NullIfWhiteSpace(),
                                        sMotherLastName = register.MotherLastname.NullIfWhiteSpace(),
                                        sMotherNameEN = register.MotherNameEn.NullIfWhiteSpace(),
                                        sMotherLastEN = register.MotherLastnameEn.NullIfWhiteSpace(),
                                        dMotherBirthDay = string.IsNullOrEmpty(register.MotherBirthday) ? (DateTime?)null : DateTime.ParseExact(register.MotherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH")),
                                        sMotherIdCardNumber = register.MotherIDCard.NullIfWhiteSpace(),
                                        sMotherRace = register.MotherRace.NullIfWhiteSpace(),
                                        sMotherNation = register.MotherNation.NullIfWhiteSpace(),
                                        sMotherReligion = register.MotherReligion.NullIfWhiteSpace(),
                                        sMotherGraduated = register.MotherEducational,
                                        sMotherHomeNumber = register.MotherHomeNumber.NullIfWhiteSpace(),
                                        sMotherSoy = register.MotherAlley.NullIfWhiteSpace(),
                                        sMotherMuu = register.MotherVillageNo.NullIfWhiteSpace(),
                                        sMotherRoad = register.MotherRoad.NullIfWhiteSpace(),
                                        sMotherProvince = register.MotherProvince.NullIfWhiteSpace(),
                                        sMotherAumpher = register.MotherDistrict.NullIfWhiteSpace(),
                                        sMotherTumbon = register.MotherSubDistrict.NullIfWhiteSpace(),
                                        sMotherPost = register.MotherPostalCode.NullIfWhiteSpace(),
                                        sMotherJob = register.MotherCareer.NullIfWhiteSpace(),
                                        sMotherWorkPlace = register.MotherWorkplace.NullIfWhiteSpace(),
                                        nMotherIncome = register.MotherMonthIncome, //
                                        MotherAnnualIncome = register.MotherYearIncome.NullIfWhiteSpace(),
                                        sMotherPhone = register.MotherHousePhone.NullIfWhiteSpace(),
                                        sMotherPhone2 = register.MotherMobilePhone.NullIfWhiteSpace(),
                                        sMotherPhone3 = register.MotherWorkPhone.NullIfWhiteSpace(),
                                        MotherEmail = register.MotherEmail.NullIfWhiteSpace(),

                                        // Page 09
                                        sFamilyRelate = register.ParentRelation.NullIfWhiteSpace(),
                                        nFamilyTitle = register.ParentTitle,
                                        sFamilyName = register.ParentName.NullIfWhiteSpace(),
                                        sFamilyLast = register.ParentLastname.NullIfWhiteSpace(),
                                        sFamilyNameEN = register.ParentNameEn.NullIfWhiteSpace(),
                                        sFamilyLastEN = register.ParentLastnameEn.NullIfWhiteSpace(),
                                        dFamilyBirthDay = string.IsNullOrEmpty(register.ParentBirthday) ? (DateTime?)null : DateTime.ParseExact(register.ParentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH")),
                                        sFamilyIdCardNumber = register.ParentIDCard.NullIfWhiteSpace(),
                                        sFamilyRace = register.ParentRace.NullIfWhiteSpace(),
                                        sFamilyNation = register.ParentNation.NullIfWhiteSpace(),
                                        sFamilyReligion = register.ParentReligion.NullIfWhiteSpace(),
                                        sFamilyGraduated = register.ParentEducational,
                                        familyStatus = register.ParentStatus,
                                        sFamilyHomeNumber = register.ParentHomeNumber.NullIfWhiteSpace(),
                                        sFamilySoy = register.ParentAlley.NullIfWhiteSpace(),
                                        sFamilyMuu = register.ParentVillageNo.NullIfWhiteSpace(),
                                        sFamilyRoad = register.ParentRoad.NullIfWhiteSpace(),
                                        sFamilyProvince = register.ParentProvince.NullIfWhiteSpace(),
                                        sFamilyAumpher = register.ParentDistrict.NullIfWhiteSpace(),
                                        sFamilyTumbon = register.ParentSubDistrict.NullIfWhiteSpace(),
                                        sFamilyPost = register.ParentPostalCode.NullIfWhiteSpace(),
                                        sFamilyJob = register.ParentCareer.NullIfWhiteSpace(),
                                        sFamilyWorkPlace = register.ParentWorkplace.NullIfWhiteSpace(),
                                        nFamilyIncome = register.ParentMonthIncome, //
                                        ParentAnnualIncome = register.ParentYearIncome.NullIfWhiteSpace(),
                                        sPhoneOne = register.ParentHousePhone.NullIfWhiteSpace(),
                                        sPhoneTwo = register.ParentMobilePhone.NullIfWhiteSpace(),
                                        sPhoneThree = register.ParentWorkPhone.NullIfWhiteSpace(),
                                        ParentEmail = register.ParentEmail.NullIfWhiteSpace(),

                                        // Page 10
                                        oldSchoolName = register.OldSchoolName.NullIfWhiteSpace(),
                                        oldSchoolProvince = register.OldSchoolProvince.NullIfWhiteSpace(),
                                        oldSchoolAumpher = register.OldSchoolDistrict.NullIfWhiteSpace(),
                                        oldSchoolTumbon = register.OldSchoolSubDistrict.NullIfWhiteSpace(),
                                        oldSchoolGraduated = register.OldSchoolEducational.NullIfWhiteSpace(),
                                        oldSchoolGPA = register.GPA,

                                        // Page 11
                                        nWeight = register.Weight,
                                        nHeight = register.Height,
                                        sBlood = register.Blood.NullIfWhiteSpace(),
                                        sSickFood = register.FoodAllergy.NullIfWhiteSpace(),
                                        sSickDrug = register.BeAllergic.NullIfWhiteSpace(),
                                        sSickOther = register.OtherAllergic.NullIfWhiteSpace(),
                                        sSickNormal = register.CongenitalDisease.NullIfWhiteSpace(),
                                        sSickDanger = register.SeriousDisease.NullIfWhiteSpace(),

                                        // Default set
                                        cType = "0",
                                        cDel = null,
                                        nStudentNumber = null,
                                        nStudentStatus = 0,
                                        nTermSubLevel2 = null,
                                        paymentStatus = 0,
                                        registerStatus = 0,
                                        sCity = "",
                                        sCountry = "",
                                        sPostalcode = "",
                                        knowFrom1 = 0,
                                        knowFrom2 = 0,
                                        knowFrom3 = 0,
                                        knowFrom4 = 0,
                                        knowFrom5 = 0,
                                        knowFrom6 = 0,
                                        knowFrom7 = 0,
                                        knowFrom8 = 0,
                                        knowFrom9 = 0,
                                        knowFrom10 = 0,
                                        knowFrom11 = 0,
                                        addDate = DateTime.Now,

                                        SchoolID = schoolID,
                                        CreatedDate = DateTime.Now
                                    };
                                    en.TPreRegisters.Add(preRegister);
                                    en.SaveChanges();

                                    registerID = preRegister.preRegisterId;


                                    // Prepare object on session
                                    List<DocumentFile> DocumentFiles = null;
                                    if (HttpContext.Current.Session["RegisterOnlineFileBase64"] != null)
                                    {
                                        DocumentFiles = (List<DocumentFile>)HttpContext.Current.Session["RegisterOnlineFileBase64"];
                                    }

                                    if (DocumentFiles != null)
                                    {
                                        // Upload profile picture to storage
                                        var profilePicture = DocumentFiles.Where(w => w.VFIID == 0).FirstOrDefault();
                                        if (profilePicture != null)
                                        {
                                            profilePicture.ByteData = profilePicture.ByteData.Split(';')[1].Replace("base64,", "");

                                            string linkfiles = ComFunction.UploadFileFromByteData(profilePicture.ByteData, "preregister", 150, schoolID, registerID);
                                            if (!string.IsNullOrEmpty(linkfiles))
                                            {
                                                TPreRegister p = en.TPreRegisters.First(f => f.SchoolID == schoolID && f.preRegisterId == registerID);

                                                p.sStudentPicture = linkfiles;
                                                p.dPicUpdate = DateTime.Now;
                                                p.nPicversion = !string.IsNullOrEmpty(linkfiles) ? 1 : 0;

                                                en.SaveChanges();
                                            }

                                            var registerFileObj = register.Files.Where(w => w.VFIID == 0).FirstOrDefault();
                                            if (registerFileObj != null)
                                            {
                                                registerFileObj.ByteData = profilePicture.ByteData;
                                            }
                                        }

                                        // Upload document file
                                        var documentFiles = DocumentFiles.Where(w => w.VFIID != 0).ToList();
                                        foreach (var df in documentFiles)
                                        {
                                            string linkfiles = ComFunction.UploadFileFromByteData(df.DocID + "_" + df.TypeID + "_" + df.FileName, df.ByteData, "preregister/document", schoolID, registerID);
                                            TPreRegisterDocument preRegisterDocument = new TPreRegisterDocument
                                            {
                                                preRegisterId = preRegister.preRegisterId,
                                                DocumentID = df.DocID,
                                                Type = df.TypeID,
                                                VFIID = df.VFIID,
                                                FileName = df.FileName,
                                                ContentType = df.ContentType,
                                                FilePath = linkfiles,
                                                SchoolID = schoolID,
                                                UpdateDate = DateTime.Now
                                            };
                                            en.TPreRegisterDocument.Add(preRegisterDocument);

                                            var registerFileObj = register.Files.Where(w => w.VFIID == df.VFIID).FirstOrDefault();
                                            if (registerFileObj != null)
                                            {
                                                registerFileObj.ByteData = df.ByteData;
                                            }
                                        }
                                        en.SaveChanges();

                                        // Clear session
                                        HttpContext.Current.Session["RegisterOnlineFileBase64"] = null;
                                    }


                                    HttpContext.Current.Session["RegisterID"] = registerID;
                                    HttpContext.Current.Session["RegisterPrintID"] = registerID;

                                    TRegisterSetup registerSetup = en.TRegisterSetups.FirstOrDefault(f => f.RegisterPlanSetupID == preRegister.RegisterPlanSetupID && f.Year == registerYearBE && f.nTSubLevel == preRegister.optionLevel);
                                    if (registerSetup != null && registerSetup.PaymentGroupID != null)
                                    {
                                        bool statusSaveInvoice = Accounting.Tuitionfee.Setting.SaveInvoiceData(preRegister);
                                        if (!statusSaveInvoice)
                                        {
                                            success = false;
                                            message = "บันทึกใบแจ้งหนี้ไม่สำเร็จ";
                                        }                                        
                                    }

                                    // Text Log
                                    try
                                    {
                                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/upload/registeronline")))
                                        {
                                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/upload/registeronline"));
                                        }

                                        System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/upload/registeronline/" + schoolID + "-" + registerID + ".txt"), EntityToJson(register));
                                    }
                                    catch { }
                                }
                            }
                            else if (rf == "sendmail")
                            {
                                registerID = (int)HttpContext.Current.Session["RegisterID"];

                                int existIDCard = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId != registerID && w.registerYear == registerYear && w.sIdentification == register.StudentIDCard && w.cDel != 1).Count();
                                if (existIDCard > 0)
                                {
                                    success = false;
                                    message = "รหัสบัตรประชาชนนี้มีการใช้งานไปแล้ว";
                                }

                                if (!register.Page01Saved || !register.Page02Saved || !register.Page03Saved || !register.Page04Saved || !register.Page05Saved || !register.Page06Saved || !register.Page07Saved || !register.Page08Saved || !register.Page09Saved || !register.Page10Saved || !register.Page11Saved || !register.Page12Saved)
                                {
                                    success = false;
                                    message = "กรุณากรอกให้ครบทุกหน้า";
                                }

                                if (success)
                                {
                                    TPreRegister preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();
                                    if (preRegister != null)
                                    {
                                        // Page 03
                                        preRegister.StudentType = register.StudentType;
                                        preRegister.optionCourse = null; // 1 = ปกติ, 3 = ทวิภาคี
                                        preRegister.optionLevel = register.Class; // nTSubLevel
                                        preRegister.RegisterPlanSetupID = register.EduProgram;
                                        preRegister.MainPlan = register.MainPlan;
                                        preRegister.BackupPlans = register.BackupPlans;
                                        preRegister.optionTime = register.OptionTime; // 1 = เช้า, 2 = บ่าย 1, 3 = บ่าย 2
                                        preRegister.optionBranch = register.OptionBranch;

                                        // Page 04
                                        preRegister.StudentCategory = register.StudentCategory.NullIfWhiteSpace();
                                        preRegister.StudentTitle = register.StudentTitle;
                                        preRegister.sName = register.StudentName.NullIfWhiteSpace();
                                        preRegister.sLastname = register.StudentLastname.NullIfWhiteSpace();
                                        preRegister.sStudentNameEN = register.StudentNameEn.NullIfWhiteSpace();
                                        preRegister.sStudentLastEN = register.StudentLastnameEn.NullIfWhiteSpace();
                                        preRegister.sNickName = register.StudentNickName.NullIfWhiteSpace();
                                        preRegister.sNickNameEN = register.StudentNickNameEn.NullIfWhiteSpace();
                                        preRegister.cSex = register.StudentSex.NullIfWhiteSpace();
                                        preRegister.dBirth = string.IsNullOrEmpty(register.StudentBirthday) ? (DateTime?)null : DateTime.ParseExact(register.StudentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                        preRegister.sIdentification = register.StudentIDCard.NullIfWhiteSpace();
                                        preRegister.sStudentIdCardNumber = register.StudentIDCard.NullIfWhiteSpace();
                                        preRegister.sStudentRace = register.StudentRace.NullIfWhiteSpace();
                                        preRegister.sStudentNation = register.StudentNation.NullIfWhiteSpace();
                                        preRegister.sStudentReligion = register.StudentReligion.NullIfWhiteSpace();
                                        preRegister.nSonTotal = register.StudentSonTotal;
                                        preRegister.nSonNumber = register.StudentSonNumber;

                                        // Page 05
                                        preRegister.sStudentHomeRegisterCode = register.HouseCode.NullIfWhiteSpace();
                                        preRegister.houseRegistrationNumber = register.HouseHomeNumber.NullIfWhiteSpace();
                                        preRegister.houseRegistrationSoy = register.HouseAlley.NullIfWhiteSpace();
                                        preRegister.houseRegistrationMuu = register.HouseVillageNo.NullIfWhiteSpace();
                                        preRegister.houseRegistrationRoad = register.HouseRoad.NullIfWhiteSpace();
                                        preRegister.houseRegistrationProvince = register.HouseProvince;
                                        preRegister.houseRegistrationAumpher = register.HouseDistrict;
                                        preRegister.houseRegistrationTumbon = register.HouseSubDistrict;
                                        preRegister.houseRegistrationPost = register.HousePostalCode.NullIfWhiteSpace();
                                        preRegister.houseRegistrationPhone = register.HousePhone.NullIfWhiteSpace();
                                        preRegister.sPhone = register.HousePhone.NullIfWhiteSpace();
                                        preRegister.sEmail = register.HouseEmail.NullIfWhiteSpace();

                                        // Page 06
                                        preRegister.sStudentHomeNumber = register.StudentHomeNumber.NullIfWhiteSpace();
                                        preRegister.sStudentSoy = register.StudentAlley.NullIfWhiteSpace();
                                        preRegister.sStudentMuu = register.StudentVillageNo.NullIfWhiteSpace();
                                        preRegister.sStudentRoad = register.StudentRoad.NullIfWhiteSpace();
                                        preRegister.sStudentProvince = register.StudentProvince.NullIfWhiteSpace();
                                        preRegister.sStudentAumpher = register.StudentDistrict.NullIfWhiteSpace();
                                        preRegister.sStudentTumbon = register.StudentSubDistrict.NullIfWhiteSpace();
                                        preRegister.sStudentPost = register.StudentPostalCode.NullIfWhiteSpace();
                                        preRegister.sStudentHousePhone = register.StudentHousePhone.NullIfWhiteSpace();
                                        preRegister.stayWithEmail = register.StudentHouseEmail.NullIfWhiteSpace();
                                        preRegister.stayWithTitle = register.StudentStayWithTitle;
                                        preRegister.stayWithName = register.StudentStayWithName.NullIfWhiteSpace();
                                        preRegister.stayWithLast = register.StudentStayWithLast.NullIfWhiteSpace();
                                        preRegister.stayWithEmergencyCall = register.StudentStayWithEmergencyCall.NullIfWhiteSpace();
                                        preRegister.HomeType = register.StudentHomeType == 0 ? null : register.StudentHomeType;
                                        preRegister.friendName = register.StudentNeighborName.NullIfWhiteSpace();
                                        preRegister.friendLastName = register.StudentNeighborLastName.NullIfWhiteSpace();
                                        preRegister.friendSubLevel = register.StudentNeighborSubLevel;
                                        preRegister.friendPhone = register.StudentNeighborPhone.NullIfWhiteSpace();

                                        // Page 07
                                        preRegister.FatherTitle = register.FatherTitle;
                                        preRegister.sFatherFirstName = register.FatherName.NullIfWhiteSpace();
                                        preRegister.sFatherLastName = register.FatherLastname.NullIfWhiteSpace();
                                        preRegister.sFatherNameEN = register.FatherNameEn.NullIfWhiteSpace();
                                        preRegister.sFatherLastEN = register.FatherLastnameEn.NullIfWhiteSpace();
                                        preRegister.dFatherBirthDay = string.IsNullOrEmpty(register.FatherBirthday) ? (DateTime?)null : DateTime.ParseExact(register.FatherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                        preRegister.sFatherIdCardNumber = register.FatherIDCard.NullIfWhiteSpace();
                                        preRegister.sFatherRace = register.FatherRace.NullIfWhiteSpace();
                                        preRegister.sFatherNation = register.FatherNation.NullIfWhiteSpace();
                                        preRegister.sFatherReligion = register.FatherReligion.NullIfWhiteSpace();
                                        preRegister.sFatherGraduated = register.FatherEducational;
                                        preRegister.sFatherHomeNumber = register.FatherHomeNumber.NullIfWhiteSpace();
                                        preRegister.sFatherSoy = register.FatherAlley.NullIfWhiteSpace();
                                        preRegister.sFatherMuu = register.FatherVillageNo.NullIfWhiteSpace();
                                        preRegister.sFatherRoad = register.FatherRoad.NullIfWhiteSpace();
                                        preRegister.sFatherProvince = register.FatherProvince.NullIfWhiteSpace();
                                        preRegister.sFatherAumpher = register.FatherDistrict.NullIfWhiteSpace();
                                        preRegister.sFatherTumbon = register.FatherSubDistrict.NullIfWhiteSpace();
                                        preRegister.sFatherPost = register.FatherPostalCode.NullIfWhiteSpace();
                                        preRegister.sFatherJob = register.FatherCareer.NullIfWhiteSpace();
                                        preRegister.sFatherWorkPlace = register.FatherWorkplace.NullIfWhiteSpace();
                                        preRegister.nFatherIncome = register.FatherMonthIncome; //
                                        preRegister.FatherAnnualIncome = register.FatherYearIncome.NullIfWhiteSpace();
                                        preRegister.sFatherPhone = register.FatherHousePhone.NullIfWhiteSpace();
                                        preRegister.sFatherPhone2 = register.FatherMobilePhone.NullIfWhiteSpace();
                                        preRegister.sFatherPhone3 = register.FatherWorkPhone.NullIfWhiteSpace();
                                        preRegister.FatherEmail = register.FatherEmail.NullIfWhiteSpace();

                                        // Page 08
                                        preRegister.MotherTitle = register.MotherTitle;
                                        preRegister.sMotherFirstName = register.MotherName.NullIfWhiteSpace();
                                        preRegister.sMotherLastName = register.MotherLastname.NullIfWhiteSpace();
                                        preRegister.sMotherNameEN = register.MotherNameEn.NullIfWhiteSpace();
                                        preRegister.sMotherLastEN = register.MotherLastnameEn.NullIfWhiteSpace();
                                        preRegister.dMotherBirthDay = string.IsNullOrEmpty(register.MotherBirthday) ? (DateTime?)null : DateTime.ParseExact(register.MotherBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                        preRegister.sMotherIdCardNumber = register.MotherIDCard.NullIfWhiteSpace();
                                        preRegister.sMotherRace = register.MotherRace.NullIfWhiteSpace();
                                        preRegister.sMotherNation = register.MotherNation.NullIfWhiteSpace();
                                        preRegister.sMotherReligion = register.MotherReligion.NullIfWhiteSpace();
                                        preRegister.sMotherGraduated = register.MotherEducational;
                                        preRegister.sMotherHomeNumber = register.MotherHomeNumber.NullIfWhiteSpace();
                                        preRegister.sMotherSoy = register.MotherAlley.NullIfWhiteSpace();
                                        preRegister.sMotherMuu = register.MotherVillageNo.NullIfWhiteSpace();
                                        preRegister.sMotherRoad = register.MotherRoad.NullIfWhiteSpace();
                                        preRegister.sMotherProvince = register.MotherProvince.NullIfWhiteSpace();
                                        preRegister.sMotherAumpher = register.MotherDistrict.NullIfWhiteSpace();
                                        preRegister.sMotherTumbon = register.MotherSubDistrict.NullIfWhiteSpace();
                                        preRegister.sMotherPost = register.MotherPostalCode.NullIfWhiteSpace();
                                        preRegister.sMotherJob = register.MotherCareer.NullIfWhiteSpace();
                                        preRegister.sMotherWorkPlace = register.MotherWorkplace.NullIfWhiteSpace();
                                        preRegister.nMotherIncome = register.MotherMonthIncome; //
                                        preRegister.MotherAnnualIncome = register.MotherYearIncome.NullIfWhiteSpace();
                                        preRegister.sMotherPhone = register.MotherHousePhone.NullIfWhiteSpace();
                                        preRegister.sMotherPhone2 = register.MotherMobilePhone.NullIfWhiteSpace();
                                        preRegister.sMotherPhone3 = register.MotherWorkPhone.NullIfWhiteSpace();
                                        preRegister.MotherEmail = register.MotherEmail.NullIfWhiteSpace();

                                        // Page 09
                                        preRegister.sFamilyRelate = register.ParentRelation.NullIfWhiteSpace();
                                        preRegister.nFamilyTitle = register.ParentTitle;
                                        preRegister.sFamilyName = register.ParentName.NullIfWhiteSpace();
                                        preRegister.sFamilyLast = register.ParentLastname.NullIfWhiteSpace();
                                        preRegister.sFamilyNameEN = register.ParentNameEn.NullIfWhiteSpace();
                                        preRegister.sFamilyLastEN = register.ParentLastnameEn.NullIfWhiteSpace();
                                        preRegister.dFamilyBirthDay = string.IsNullOrEmpty(register.ParentBirthday) ? (DateTime?)null : DateTime.ParseExact(register.ParentBirthday, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                        preRegister.sFamilyIdCardNumber = register.ParentIDCard.NullIfWhiteSpace();
                                        preRegister.sFamilyRace = register.ParentRace.NullIfWhiteSpace();
                                        preRegister.sFamilyNation = register.ParentNation.NullIfWhiteSpace();
                                        preRegister.sFamilyReligion = register.ParentReligion.NullIfWhiteSpace();
                                        preRegister.sFamilyGraduated = register.ParentEducational;
                                        preRegister.familyStatus = register.ParentStatus;
                                        preRegister.sFamilyHomeNumber = register.ParentHomeNumber.NullIfWhiteSpace();
                                        preRegister.sFamilySoy = register.ParentAlley.NullIfWhiteSpace();
                                        preRegister.sFamilyMuu = register.ParentVillageNo.NullIfWhiteSpace();
                                        preRegister.sFamilyRoad = register.ParentRoad.NullIfWhiteSpace();
                                        preRegister.sFamilyProvince = register.ParentProvince.NullIfWhiteSpace();
                                        preRegister.sFamilyAumpher = register.ParentDistrict.NullIfWhiteSpace();
                                        preRegister.sFamilyTumbon = register.ParentSubDistrict.NullIfWhiteSpace();
                                        preRegister.sFamilyPost = register.ParentPostalCode.NullIfWhiteSpace();
                                        preRegister.sFamilyJob = register.ParentCareer.NullIfWhiteSpace();
                                        preRegister.sFamilyWorkPlace = register.ParentWorkplace.NullIfWhiteSpace();
                                        preRegister.nFamilyIncome = register.ParentMonthIncome; //
                                        preRegister.ParentAnnualIncome = register.ParentYearIncome.NullIfWhiteSpace();
                                        preRegister.sPhoneOne = register.ParentHousePhone.NullIfWhiteSpace();
                                        preRegister.sPhoneTwo = register.ParentMobilePhone.NullIfWhiteSpace();
                                        preRegister.sPhoneThree = register.ParentWorkPhone.NullIfWhiteSpace();
                                        preRegister.ParentEmail = register.ParentEmail.NullIfWhiteSpace();

                                        // Page 10
                                        preRegister.oldSchoolName = register.OldSchoolName.NullIfWhiteSpace();
                                        preRegister.oldSchoolProvince = register.OldSchoolProvince.NullIfWhiteSpace();
                                        preRegister.oldSchoolAumpher = register.OldSchoolDistrict.NullIfWhiteSpace();
                                        preRegister.oldSchoolTumbon = register.OldSchoolSubDistrict.NullIfWhiteSpace();
                                        preRegister.oldSchoolGraduated = register.OldSchoolEducational.NullIfWhiteSpace();
                                        preRegister.oldSchoolGPA = register.GPA;

                                        // Page 11
                                        preRegister.nWeight = register.Weight;
                                        preRegister.nHeight = register.Height;
                                        preRegister.sBlood = register.Blood.NullIfWhiteSpace();
                                        preRegister.sSickFood = register.FoodAllergy.NullIfWhiteSpace();
                                        preRegister.sSickDrug = register.BeAllergic.NullIfWhiteSpace();
                                        preRegister.sSickOther = register.OtherAllergic.NullIfWhiteSpace();
                                        preRegister.sSickNormal = register.CongenitalDisease.NullIfWhiteSpace();
                                        preRegister.sSickDanger = register.SeriousDisease.NullIfWhiteSpace();

                                        preRegister.UpdatedDate = DateTime.Now.FixSecond(1).FixMillisecond(10);

                                        en.SaveChanges();


                                        // Prepare object on session
                                        List<DocumentFile> DocumentFiles = null;
                                        if (HttpContext.Current.Session["RegisterOnlineFileBase64"] != null)
                                        {
                                            DocumentFiles = (List<DocumentFile>)HttpContext.Current.Session["RegisterOnlineFileBase64"];
                                        }

                                        if (DocumentFiles != null)
                                        {
                                            // Upload profile picture to storage
                                            var profilePicture = DocumentFiles.Where(w => w.VFIID == 0).FirstOrDefault();
                                            if (profilePicture != null && !string.IsNullOrEmpty(profilePicture.ByteData))
                                            {
                                                profilePicture.ByteData = profilePicture.ByteData.Split(';')[1].Replace("base64,", "");

                                                string linkfiles = ComFunction.UploadFileFromByteData(profilePicture.ByteData, "preregister", 150, schoolID, registerID);
                                                if (!string.IsNullOrEmpty(linkfiles))
                                                {
                                                    TPreRegister p = en.TPreRegisters.First(f => f.SchoolID == schoolID && f.preRegisterId == registerID);

                                                    p.sStudentPicture = linkfiles;
                                                    p.dPicUpdate = DateTime.Now;
                                                    p.nPicversion = !string.IsNullOrEmpty(linkfiles) ? 1 : 0;

                                                    en.SaveChanges();
                                                }

                                                var registerFileObj = register.Files.Where(w => w.VFIID == 0).FirstOrDefault();
                                                if (registerFileObj != null)
                                                {
                                                    registerFileObj.ByteData = profilePicture.ByteData;
                                                }
                                            }

                                            // Upload document file
                                            var documentFiles = DocumentFiles.Where(w => w.VFIID != 0).ToList();
                                            foreach (var df in documentFiles)
                                            {
                                                if (string.IsNullOrEmpty(df.ByteData)) continue;

                                                string linkfiles = ComFunction.UploadFileFromByteData(df.DocID + "_" + df.TypeID + "_" + df.FileName, df.ByteData, "preregister/document", schoolID, registerID);

                                                TPreRegisterDocument preRegisterDocument = en.TPreRegisterDocument.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID && w.DocumentID == df.DocID && w.Type == df.TypeID && w.VFIID == df.VFIID).FirstOrDefault();
                                                if (preRegisterDocument == null)
                                                {
                                                    preRegisterDocument = new TPreRegisterDocument
                                                    {
                                                        preRegisterId = preRegister.preRegisterId,
                                                        DocumentID = df.DocID,
                                                        Type = df.TypeID,
                                                        VFIID = df.VFIID,
                                                        FileName = df.FileName,
                                                        ContentType = df.ContentType,
                                                        FilePath = linkfiles,
                                                        SchoolID = schoolID,
                                                        UpdateDate = DateTime.Now
                                                    };
                                                    en.TPreRegisterDocument.Add(preRegisterDocument);
                                                }
                                                else
                                                {
                                                    preRegisterDocument.FileName = df.FileName;
                                                    preRegisterDocument.ContentType = df.ContentType;
                                                    preRegisterDocument.FilePath = linkfiles;
                                                    preRegisterDocument.UpdateDate = DateTime.Now.FixSecond(1).FixMillisecond(10);
                                                }

                                                var registerFileObj = register.Files.Where(w => w.VFIID == df.VFIID).FirstOrDefault();
                                                if (registerFileObj != null)
                                                {
                                                    registerFileObj.ByteData = df.ByteData;
                                                }
                                            }
                                            en.SaveChanges();

                                            // Clear session
                                            HttpContext.Current.Session["RegisterOnlineFileBase64"] = null;
                                        }


                                        HttpContext.Current.Session["RegisterID"] = registerID;
                                        HttpContext.Current.Session["RegisterPrintID"] = registerID;

                                        //TRegisterSetup registerSetup = en.TRegisterSetups.FirstOrDefault(f => f.RegisterPlanSetupID == preRegister.RegisterPlanSetupID && f.Year == registerYearBE && f.nTSubLevel == preRegister.optionLevel);
                                        //if (registerSetup != null && registerSetup.PaymentGroupID != null)
                                        //{
                                        //    bool statusSaveInvoice = Accounting.Tuitionfee.Setting.SaveInvoiceData(preRegister);
                                        //    if (!statusSaveInvoice)
                                        //    {
                                        //        success = false;
                                        //        message = "บันทึกใบแจ้งหนี้ไม่สำเร็จ";
                                        //    }
                                        //}

                                        // Text Log
                                        try
                                        {
                                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/upload/registeronline")))
                                            {
                                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/upload/registeronline"));
                                            }

                                            System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/upload/registeronline/" + schoolID + "-" + registerID + "-edit.txt"), EntityToJson(register));
                                        }
                                        catch { }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        success = false;
                        message = error.Message;

                        string logMessage = string.Format(logMessagePattern,
                            HttpContext.Current.Session["RegisterOnlineEntities"].ToString(),
                            EntityToJson(register),
                            ComFunction.GetLineNumberError(error),
                            error.Message, error.StackTrace);

                        ComFunction.InsertLogDebug(null, null, null, logMessage);
                    }
                }
                else
                {
                    success = false;
                    message = "Session หมดเวลา กรุณาเข้าทำรายการใหม่อีกครั้ง";
                }

                var result = new { success, message, studentID = registerCode, examCode, registerID };

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception error)
            {
                success = false;
                message = error.Message;

                string logMessage = string.Format(logMessagePattern,
                    HttpContext.Current.Session["RegisterOnlineEntities"].ToString(),
                    EntityToJson(register),
                    ComFunction.GetLineNumberError(error),
                    error.Message, error.StackTrace);

                ComFunction.InsertLogDebug(null, null, null, logMessage);

                var result = new { success, message };

                return JsonConvert.SerializeObject(result);
            }
        }

        private static string EntityToJson(object data)
        {
            return new JavaScriptSerializer().Serialize(data);
        }

        public class RequiredFieldData
        {
            [JsonProperty(PropertyName = "inputFieldName")]
            public string InputFieldName { get; set; }

            [JsonProperty(PropertyName = "categoryId")]
            public int CategoryID { get; set; }
        }

    }
}