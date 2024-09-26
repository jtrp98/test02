using FingerprintPayment.Class;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterPrint : RegisterGateway
    {
        protected string schoolLogo = "";
        protected string schoolName = "";
        protected string registerYearBE = "";
        protected EntityRegisterPrint entityRegisterPrint;

        protected bool HaveProfileImage = false;
        protected string ProfileImageUrl = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterOnlineSchoolLogo"] != null)
            {
                schoolLogo = (string)Session["RegisterOnlineSchoolLogo"];
            }
            if (Session["RegisterOnlineSchoolName"] != null)
            {
                schoolName = (string)Session["RegisterOnlineSchoolName"];
            }

            if (Session["RegisterOnlineEntities"] != null && Session["RegisterPrintID"] != null)
            {
                int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int registerID = Convert.ToInt32(Session["RegisterPrintID"]);
                    var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();
                    if (preRegister != null)
                    {
                        registerYearBE = (preRegister.registerYear + 543).ToString();

                        string className = "";
                        string classLongName = "";
                        var subLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == preRegister.optionLevel).FirstOrDefault();
                        if (subLevel != null)
                        {
                            className = subLevel.SubLevel;
                            classLongName = subLevel.fullName;
                        }

                        string planName = "";
                        if (preRegister.RegisterPlanSetupID != 0)
                        {
                            var planSetup = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.RegisterPlanSetupID == preRegister.RegisterPlanSetupID && w.nTSubLevel == preRegister.optionLevel).FirstOrDefault();
                            if (planSetup != null)
                            {
                                planName = planSetup.PlanName;
                            }
                        }
                        else
                        {
                            planName = "ทั้งหมด";
                        }

                        string studentTitleName = "";
                        var studentTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == preRegister.StudentTitle).FirstOrDefault();
                        if (studentTitle != null)
                        {
                            studentTitleName = studentTitle.titleDescription;
                        }

                        string fatherTitleName = "";
                        var fatherTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == preRegister.FatherTitle).FirstOrDefault();
                        if (fatherTitle != null)
                        {
                            fatherTitleName = fatherTitle.titleDescription;
                        }

                        string motherTitleName = "";
                        var motherTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == preRegister.MotherTitle).FirstOrDefault();
                        if (motherTitle != null)
                        {
                            motherTitleName = motherTitle.titleDescription;
                        }

                        string parentTitleName = "";
                        var parentTitle = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == preRegister.nFamilyTitle).FirstOrDefault();
                        if (parentTitle != null)
                        {
                            parentTitleName = parentTitle.titleDescription;
                        }

                       

                        string provinceName = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentProvince))
                        {
                            int studentProvinceID = Convert.ToInt32(preRegister.sStudentProvince);
                            provinceName = dbMaster.provinces.Where(w => w.PROVINCE_ID == studentProvinceID).FirstOrDefault().PROVINCE_NAME;
                        }

                        string districtName = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentAumpher))
                        {
                            int studentDistrictID = Convert.ToInt32(preRegister.sStudentAumpher);
                            districtName = dbMaster.amphurs.Where(w => w.AMPHUR_ID == studentDistrictID).FirstOrDefault().AMPHUR_NAME;
                        }

                        string subDistrictName = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentTumbon))
                        {
                            int studentSubDistrictID = Convert.ToInt32(preRegister.sStudentTumbon);
                            subDistrictName = dbMaster.districts.Where(w => w.DISTRICT_ID == studentSubDistrictID).FirstOrDefault().DISTRICT_NAME;
                        }

                        string studentNation = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentNation))
                        {
                            studentNation = en.TMasterDatas.Where(w => w.MasterType == "3" && w.MasterCode == preRegister.sStudentNation).FirstOrDefault().MasterDes;
                        }

                        string studentReligion = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentReligion))
                        {
                            studentReligion = en.TMasterDatas.Where(w => w.MasterType == "6" && w.MasterCode == preRegister.sStudentReligion).FirstOrDefault().MasterDes;
                        }

                        string studentRace = "";
                        if (!string.IsNullOrEmpty(preRegister.sStudentRace))
                        {
                            studentRace = en.TMasterDatas.Where(w => w.MasterType == "9" && w.MasterCode == preRegister.sStudentRace).FirstOrDefault().MasterDes;
                        }

                        entityRegisterPrint = new EntityRegisterPrint();

                        entityRegisterPrint.RegisterYear = preRegister.registerYear.ToString();
                        entityRegisterPrint.RegisterCode = preRegister.registerCode;
                        entityRegisterPrint.ExamCode = preRegister.ExamCode;
                        entityRegisterPrint.FullDate = preRegister.addDate.Value.ToString("d MMMMM yyyy", new CultureInfo("th-TH"));
                        entityRegisterPrint.Date = preRegister.addDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        entityRegisterPrint.Class = className;
                        entityRegisterPrint.ClassLongName = classLongName;
                        entityRegisterPrint.StudentCategory = (preRegister.StudentCategory == "1" ? "ไป-กลับ" : "ประจำ");
                        entityRegisterPrint.PlanName = planName;
                        entityRegisterPrint.StudentFullName = studentTitleName + preRegister.sName + " " + preRegister.sLastname;
                        entityRegisterPrint.StudentName = preRegister.sName + " " + preRegister.sLastname;
                        entityRegisterPrint.StudentBirthday = (preRegister.dBirth != null ? preRegister.dBirth.Value.ToString("d MMMMM yyyy", new CultureInfo("th-TH")) : "");
                        entityRegisterPrint.StudentAge = (preRegister.dBirth != null ? ComFunction.CalculateAge(preRegister.dBirth.Value).ToString() : "");
                        entityRegisterPrint.IDCard = preRegister.sIdentification;
                        entityRegisterPrint.StudentRace = studentRace;
                        entityRegisterPrint.StudentNation = studentNation;
                        entityRegisterPrint.StudentReligion = studentReligion;
                        entityRegisterPrint.StudentHomeNumber = preRegister.sStudentHomeNumber;
                        entityRegisterPrint.StudentAlley = preRegister.sStudentSoy;
                        entityRegisterPrint.StudentRoad = preRegister.sStudentRoad;
                        entityRegisterPrint.StudentSubDistrict = subDistrictName;
                        entityRegisterPrint.StudentDistrict = districtName;
                        entityRegisterPrint.StudentProvince = provinceName;
                        entityRegisterPrint.StudentPostalCode = preRegister.sStudentPost;
                        entityRegisterPrint.StudentHousePhone = preRegister.sStudentHousePhone;
                        entityRegisterPrint.StudentHouseEmail = preRegister.stayWithEmail;
                        entityRegisterPrint.FatherFullName = fatherTitleName + preRegister.sFatherFirstName + " " + preRegister.sFatherLastName;
                        entityRegisterPrint.FatherName = preRegister.sFatherFirstName + " " + preRegister.sFatherLastName;
                        entityRegisterPrint.FatherPhone = preRegister.sFatherPhone;
                        entityRegisterPrint.FatherPhone2 = preRegister.sFatherPhone2;
                        entityRegisterPrint.FatherWorkPlace = preRegister.sFatherWorkPlace;
                        entityRegisterPrint.FatherJob = preRegister.sFatherJob;
                        entityRegisterPrint.FatherIncome = (preRegister.nFatherIncome != null ? preRegister.nFatherIncome.Value.ToString("#,0.00") : "-");

                        entityRegisterPrint.MotherFullName = motherTitleName + preRegister.sMotherFirstName + " " + preRegister.sMotherLastName;
                        entityRegisterPrint.MotherName = preRegister.sMotherFirstName + " " + preRegister.sMotherLastName;
                        entityRegisterPrint.MotherPhone = preRegister.sMotherPhone;
                        entityRegisterPrint.MotherPhone2 = preRegister.sMotherPhone2;
                        entityRegisterPrint.MotherWorkPlace = preRegister.sMotherWorkPlace;
                        entityRegisterPrint.MotherJob = preRegister.sMotherJob;
                        entityRegisterPrint.MotherIncome = (preRegister.nMotherIncome != null ? preRegister.nMotherIncome.Value.ToString("#,0.00") : "-");

                        entityRegisterPrint.ParentFullName = parentTitleName + preRegister.sFamilyName + " " + preRegister.sFamilyLast;
                        entityRegisterPrint.ParentName = preRegister.sFamilyName + " " + preRegister.sFamilyLast;
                        entityRegisterPrint.ParentPhone = preRegister.sPhoneOne;
                        entityRegisterPrint.ParentPhone2 = preRegister.sPhoneTwo;
                        entityRegisterPrint.ParentWorkPlace = preRegister.sFamilyWorkPlace;
                        entityRegisterPrint.ParentJob = preRegister.sFamilyJob;
                        entityRegisterPrint.ParentIncome = (preRegister.nFamilyIncome != null ? preRegister.nFamilyIncome.Value.ToString("#,0.00") : "-");

                        entityRegisterPrint.ChronicDisease = preRegister.sSickNormal;
                        entityRegisterPrint.OtherAllergy = preRegister.sSickOther;
                        entityRegisterPrint.FoodAllergy = preRegister.sSickFood;
                        entityRegisterPrint.DrugAllergy = preRegister.sSickDrug;

                        string oldSchoolProvince = "";
                        if (!string.IsNullOrEmpty(preRegister.oldSchoolProvince))
                        {
                            int oldSchoolProvinceID = Convert.ToInt32(preRegister.oldSchoolProvince);
                            oldSchoolProvince = dbMaster.provinces.Where(w => w.PROVINCE_ID == oldSchoolProvinceID).FirstOrDefault().PROVINCE_NAME;
                        }
                        entityRegisterPrint.OldSchoolName = preRegister.oldSchoolName;
                        entityRegisterPrint.OldSchoolProvince = oldSchoolProvince;

                        var feeObject = en.TRegisterSetups.Where(w => w.SchoolID == schoolID && w.Year == (preRegister.registerYear + 543) && w.StudentType == preRegister.StudentType && w.nTSubLevel == preRegister.optionLevel && w.RegisterPlanSetupID == preRegister.RegisterPlanSetupID).FirstOrDefault();
                        if (feeObject != null && feeObject.Fee != null)
                        {
                            entityRegisterPrint.Fee = feeObject.Fee.Value.ToString("#,0");
                        }

                        // Check profile image
                        if (!string.IsNullOrEmpty(preRegister.sStudentPicture))
                        {
                            HaveProfileImage = true;
                            ProfileImageUrl = preRegister.sStudentPicture + "?" + DateTime.Now.Ticks;
                        }
                    }
                }
            }
        }
    }
}