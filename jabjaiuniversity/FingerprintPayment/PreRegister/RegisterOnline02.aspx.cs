using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnline02 : RegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string SearchData(string id)
        {
            int code = 200;
            bool success = true;
            string message = "Search Successfully";
            EntityRegisterOnline registerData = null;

            if (HttpContext.Current.Session["RegisterOnlineEntities"] != null)
            {
                try
                {
                    int schoolID = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineSchoolID"]);
                    using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        string f = (string)HttpContext.Current.Session["RegisterF"];
                        if (string.IsNullOrEmpty(f))
                        {
                            int registerYear = Convert.ToInt32(HttpContext.Current.Session["RegisterOnlineYear"]);

                            var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.registerYear == registerYear && w.sIdentification == id && w.cDel != 1).FirstOrDefault();
                            if (preRegister != null)
                            {
                                code = 201;
                                success = false;
                                message = "รหัสบัตรประชาชนนี้ได้ทำการลงทะเบียนเรียบร้อยแล้ว";
                            }
                        }
                        else if (f == "sendmail")
                        {
                            var preRegisterId = Convert.ToInt32(HttpContext.Current.Session["RegisterID"]);
                            var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == preRegisterId && w.sIdentification == id && w.cDel != 1).FirstOrDefault();
                            if (preRegister != null)
                            {
                                switch (preRegister.CompleteDocuments)
                                {
                                    case "0": // เอกสารไม่ครบ
                                        success = true;
                                        message = "Load register data & load register document successfully";

                                        // Load register data & load register document
                                        var documentFiles = new List<DocumentFile>();
                                        var preRegisterDocuments = en.TPreRegisterDocument.Where(w => w.SchoolID == schoolID && w.preRegisterId == preRegisterId).ToList();
                                        foreach (var rd in preRegisterDocuments)
                                        {
                                            documentFiles.Add(new DocumentFile
                                            {
                                                DocID = (int)rd.DocumentID,
                                                TypeID = (int)rd.Type,
                                                VFIID = (int)rd.VFIID,
                                                FileName = rd.FileName,
                                                ContentType = rd.ContentType,
                                            });
                                        }

                                        HttpContext.Current.Session["RegisterOnlineFileBase64"] = documentFiles;

                                        registerData = new EntityRegisterOnline
                                        {
                                            Page01Saved = true,

                                            Page02Saved = true,
                                            IDCard = preRegister.sIdentification,

                                            Page03Saved = true,
                                            Year = preRegister.registerYear,
                                            YearBE = preRegister.registerYear + 543,
                                            StudentType = preRegister.StudentType,
                                            Class = (int)preRegister.optionLevel,
                                            EduProgram = (int)preRegister.RegisterPlanSetupID,
                                            OptionTime = preRegister.optionTime,
                                            OptionBranch = preRegister.optionBranch,

                                            Page04Saved = true,
                                            StudentCategory = preRegister.StudentCategory,
                                            StudentTitle = preRegister.StudentTitle,
                                            StudentName = preRegister.sName,
                                            StudentLastname = preRegister.sLastname,
                                            StudentNameEn = preRegister.sStudentNameEN,
                                            StudentLastnameEn = preRegister.sStudentLastEN,
                                            StudentNickName = preRegister.sNickName,
                                            StudentNickNameEn = preRegister.sNickNameEN,
                                            StudentSex = preRegister.cSex,
                                            StudentBirthday = preRegister.dBirth == null ? "" : preRegister.dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                            StudentIDCard = preRegister.sStudentIdCardNumber,
                                            StudentRace = preRegister.sStudentRace,
                                            StudentNation = preRegister.sStudentNation,
                                            StudentReligion = preRegister.sStudentReligion,
                                            StudentSonTotal = preRegister.nSonTotal,
                                            StudentSonNumber = preRegister.nSonNumber,

                                            Page05Saved = true,
                                            HouseCode = preRegister.sStudentHomeRegisterCode,
                                            HouseHomeNumber = preRegister.houseRegistrationNumber,
                                            HouseAlley = preRegister.houseRegistrationSoy,
                                            HouseVillageNo = preRegister.houseRegistrationMuu,
                                            HouseRoad = preRegister.houseRegistrationRoad,
                                            HouseProvince = preRegister.houseRegistrationProvince,
                                            HouseDistrict = preRegister.houseRegistrationAumpher,
                                            HouseSubDistrict = preRegister.houseRegistrationTumbon,
                                            HousePostalCode = preRegister.houseRegistrationPost,
                                            HousePhone = preRegister.sPhone,
                                            HouseEmail = preRegister.sEmail,

                                            Page06Saved = true,
                                            StudentHomeNumber = preRegister.sStudentHomeNumber,
                                            StudentAlley = preRegister.sStudentSoy,
                                            StudentVillageNo = preRegister.sStudentMuu,
                                            StudentRoad = preRegister.sStudentRoad,
                                            StudentProvince = preRegister.sStudentProvince,
                                            StudentDistrict = preRegister.sStudentAumpher,
                                            StudentSubDistrict = preRegister.sStudentTumbon,
                                            StudentPostalCode = preRegister.sStudentPost,
                                            StudentHousePhone = preRegister.sStudentHousePhone,
                                            StudentHouseEmail = preRegister.stayWithEmail,
                                            StudentStayWithTitle = preRegister.stayWithTitle,
                                            StudentStayWithName = preRegister.stayWithName,
                                            StudentStayWithLast = preRegister.stayWithLast,
                                            StudentStayWithEmergencyCall = preRegister.stayWithEmergencyCall,
                                            StudentHomeType = preRegister.HomeType == null ? 0 : preRegister.HomeType,
                                            StudentNeighborName = preRegister.friendName,
                                            StudentNeighborLastName = preRegister.friendLastName,
                                            StudentNeighborSubLevel = preRegister.friendSubLevel,
                                            StudentNeighborPhone = preRegister.friendPhone,

                                            Page07Saved = true,
                                            FatherTitle = preRegister.FatherTitle,
                                            FatherName = preRegister.sFatherFirstName,
                                            FatherLastname = preRegister.sFatherLastName,
                                            FatherNameEn = preRegister.sFatherNameEN,
                                            FatherLastnameEn = preRegister.sFatherLastEN,
                                            FatherBirthday = preRegister.dFatherBirthDay == null ? "" : preRegister.dFatherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                            FatherIDCard = preRegister.sFatherIdCardNumber,
                                            FatherRace = preRegister.sFatherRace,
                                            FatherNation = preRegister.sFatherNation,
                                            FatherReligion = preRegister.sFatherReligion,
                                            FatherEducational = preRegister.sFatherGraduated,
                                            FatherHomeNumber = preRegister.sFatherHomeNumber,
                                            FatherAlley = preRegister.sFatherSoy,
                                            FatherVillageNo = preRegister.sFatherMuu,
                                            FatherRoad = preRegister.sFatherRoad,
                                            FatherProvince = preRegister.sFatherProvince,
                                            FatherDistrict = preRegister.sFatherAumpher,
                                            FatherSubDistrict = preRegister.sFatherTumbon,
                                            FatherPostalCode = preRegister.sFatherPost,
                                            FatherCareer = preRegister.sFatherJob,
                                            FatherWorkplace = preRegister.sFatherWorkPlace,
                                            FatherMonthIncome = preRegister.nFatherIncome,
                                            FatherYearIncome = preRegister.FatherAnnualIncome,
                                            FatherHousePhone = preRegister.sFatherPhone,
                                            FatherMobilePhone = preRegister.sFatherPhone2,
                                            FatherWorkPhone = preRegister.sFatherPhone3,
                                            FatherEmail = preRegister.FatherEmail,

                                            Page08Saved = true,
                                            MotherTitle = preRegister.MotherTitle,
                                            MotherName = preRegister.sMotherFirstName,
                                            MotherLastname = preRegister.sMotherLastName,
                                            MotherNameEn = preRegister.sMotherNameEN,
                                            MotherLastnameEn = preRegister.sMotherLastEN,
                                            MotherBirthday = preRegister.dMotherBirthDay == null ? "" : preRegister.dMotherBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                            MotherIDCard = preRegister.sMotherIdCardNumber,
                                            MotherRace = preRegister.sMotherRace,
                                            MotherNation = preRegister.sMotherNation,
                                            MotherReligion = preRegister.sMotherReligion,
                                            MotherEducational = preRegister.sMotherGraduated,
                                            MotherHomeNumber = preRegister.sMotherHomeNumber,
                                            MotherAlley = preRegister.sMotherSoy,
                                            MotherVillageNo = preRegister.sMotherMuu,
                                            MotherRoad = preRegister.sMotherRoad,
                                            MotherProvince = preRegister.sMotherProvince,
                                            MotherDistrict = preRegister.sMotherAumpher,
                                            MotherSubDistrict = preRegister.sMotherTumbon,
                                            MotherPostalCode = preRegister.sMotherPost,
                                            MotherCareer = preRegister.sMotherJob,
                                            MotherWorkplace = preRegister.sMotherWorkPlace,
                                            MotherMonthIncome = preRegister.nMotherIncome,
                                            MotherYearIncome = preRegister.MotherAnnualIncome,
                                            MotherHousePhone = preRegister.sMotherPhone,
                                            MotherMobilePhone = preRegister.sMotherPhone2,
                                            MotherWorkPhone = preRegister.sMotherPhone3,
                                            MotherEmail = preRegister.MotherEmail,

                                            Page09Saved = true,
                                            ParentRelation = preRegister.sFamilyRelate,
                                            ParentTitle = preRegister.nFamilyTitle,
                                            ParentName = preRegister.sFamilyName,
                                            ParentLastname = preRegister.sFamilyLast,
                                            ParentNameEn = preRegister.sFamilyNameEN,
                                            ParentLastnameEn = preRegister.sFamilyLastEN,
                                            ParentBirthday = preRegister.dFamilyBirthDay == null ? "" : preRegister.dFamilyBirthDay.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")),
                                            ParentIDCard = preRegister.sFamilyIdCardNumber,
                                            ParentRace = preRegister.sFamilyRace,
                                            ParentNation = preRegister.sFamilyNation,
                                            ParentReligion = preRegister.sFamilyReligion,
                                            ParentEducational = preRegister.sFamilyGraduated,
                                            ParentStatus = preRegister.familyStatus,
                                            ParentHomeNumber = preRegister.sFamilyHomeNumber,
                                            ParentAlley = preRegister.sFamilySoy,
                                            ParentVillageNo = preRegister.sFamilyMuu,
                                            ParentRoad = preRegister.sFamilyRoad,
                                            ParentProvince = preRegister.sFamilyProvince,
                                            ParentDistrict = preRegister.sFamilyAumpher,
                                            ParentSubDistrict = preRegister.sFamilyTumbon,
                                            ParentPostalCode = preRegister.sFamilyPost,
                                            ParentCareer = preRegister.sFamilyJob,
                                            ParentWorkplace = preRegister.sFamilyWorkPlace,
                                            ParentMonthIncome = preRegister.nFamilyIncome, //
                                            ParentYearIncome = preRegister.ParentAnnualIncome,
                                            ParentHousePhone = preRegister.sPhoneOne,
                                            ParentMobilePhone = preRegister.sPhoneTwo,
                                            ParentWorkPhone = preRegister.sPhoneThree,
                                            ParentEmail = preRegister.ParentEmail,

                                            Page10Saved = true,
                                            OldSchoolName = preRegister.oldSchoolName,
                                            OldSchoolProvince = preRegister.oldSchoolProvince,
                                            OldSchoolDistrict = preRegister.oldSchoolAumpher,
                                            OldSchoolSubDistrict = preRegister.oldSchoolTumbon,
                                            OldSchoolEducational = preRegister.oldSchoolGraduated,
                                            GPA = preRegister.oldSchoolGPA,

                                            Page11Saved = true,
                                            Weight = preRegister.nWeight,
                                            Height = preRegister.nHeight,
                                            Blood = preRegister.sBlood,
                                            FoodAllergy = preRegister.sSickFood,
                                            BeAllergic = preRegister.sSickDrug,
                                            OtherAllergic = preRegister.sSickOther,
                                            CongenitalDisease = preRegister.sSickNormal,
                                            SeriousDisease = preRegister.sSickDanger,

                                            Page12Saved = true,
                                            Files = documentFiles
                                        };

                                        break;
                                    case "1": // เอกสารครบ
                                        code = 202;
                                        success = false;
                                        message = "รหัสบัตรประชาชนนี้ได้ทำการลงทะเบียนเรียบร้อยและเอกสารครบแล้ว";
                                        break;
                                    default: // ไม่ระบุสถานะ ข้อมูลเอกสาร
                                        code = 203;
                                        success = false;
                                        message = "รหัสบัตรประชาชนนี้ได้ทำการลงทะเบียนเรียบร้อยแล้ว";
                                        break;
                                }
                            }
                            else
                            {
                                code = 204;
                                success = false;
                                message = "กรุณากรอกรหัสบัตรประชาชนให้ถูกต้อง";
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    code = 500;
                    success = false;
                    message = error.Message;
                }
            }

            var result = new { code, success, message, registerData };

            return JsonConvert.SerializeObject(result);
        }
    }
}