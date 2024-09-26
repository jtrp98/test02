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
    public partial class RegisterPrint00258 : RegisterGateway
    {
        protected string schoolLogo = "";
        protected string schoolName = "";
        protected EntityRegisterPrint entityRegisterPrint;
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

            if (Session["RegisterOnlineEntities"] != null && Session["RegisterID"] != null)
            {
                int schoolID = Convert.ToInt32(Session["RegisterOnlineSchoolID"]);

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int registerID = Convert.ToInt32(Session["RegisterID"]);
                    var preRegister = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();
                    if (preRegister != null)
                    {
                       
                        var schoolData = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                        if (schoolData != null)
                        {
                            imgPage8Logo.Src = schoolData.sImage;
                        }

                        string className = "";
                        string classLongName = "";
                        var subLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nTSubLevel == preRegister.optionLevel).FirstOrDefault();
                        if (subLevel != null)
                        {
                            className = subLevel.SubLevel;
                            classLongName = subLevel.fullName;
                        }

                        string planName = "";
                        var planSetup = en.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.RegisterPlanSetupID == preRegister.RegisterPlanSetupID && w.nTSubLevel == preRegister.optionLevel).FirstOrDefault();
                        if (planSetup != null)
                        {
                            planName = planSetup.PlanName;
                        }

                        var titleList = en.TTitleLists.Where(w => w.SchoolID == schoolID).ToList();
                        var tumbonList = dbMaster.districts.ToList();
                        var aumpherList = dbMaster.amphurs.ToList();
                        var provinceList = dbMaster.provinces.ToList();
                        var raceList = en.TMasterDatas.Where(w => w.MasterType == "9").ToList();
                        var nationList = en.TMasterDatas.Where(w => w.MasterType == "3").ToList();
                        var religionList = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();

                        var titleObj = titleList.Where(w => w.nTitleid == preRegister.StudentTitle).FirstOrDefault();
                        string userTitle = "";
                        if (titleObj != null)
                        {
                            userTitle = titleObj.titleDescription;
                        }

                        string stdTumbon = "";
                        int.TryParse(preRegister.sStudentTumbon, out int tumbonID);
                        var tumbonObj = tumbonList.Where(w => w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            stdTumbon = tumbonObj.DISTRICT_NAME;
                        }

                        string stdAumpher = "";
                        int.TryParse(preRegister.sStudentAumpher, out int aumpherID);
                        var aumpherObj = aumpherList.Where(w => w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            stdAumpher = aumpherObj.AMPHUR_NAME;
                        }

                        string stdProvince = "";
                        int.TryParse(preRegister.sStudentProvince, out int provinceID);
                        var provinceObj = provinceList.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            stdProvince = provinceObj.PROVINCE_NAME;
                        }

                        string houseRegistrationTumbon = "";
                        tumbonObj = tumbonList.Where(w => w.DISTRICT_ID == preRegister.houseRegistrationTumbon).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            houseRegistrationTumbon = tumbonObj.DISTRICT_NAME;
                        }

                        string houseRegistrationAumpher = "";
                        aumpherObj = aumpherList.Where(w => w.AMPHUR_ID == preRegister.houseRegistrationTumbon).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            houseRegistrationAumpher = aumpherObj.AMPHUR_NAME;
                        }

                        string houseRegistrationProvince = "";
                        provinceObj = provinceList.Where(w => w.PROVINCE_ID == preRegister.houseRegistrationProvince).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            houseRegistrationProvince = provinceObj.PROVINCE_NAME;
                        }

                        string oldSchoolTumbon = "";
                        int.TryParse(preRegister.oldSchoolTumbon, out tumbonID);
                        tumbonObj = tumbonList.Where(w => w.DISTRICT_ID == tumbonID).FirstOrDefault();
                        if (tumbonObj != null)
                        {
                            oldSchoolTumbon = tumbonObj.DISTRICT_NAME;
                        }

                        string oldSchoolAumpher = "";
                        int.TryParse(preRegister.oldSchoolAumpher, out aumpherID);
                        aumpherObj = aumpherList.Where(w => w.AMPHUR_ID == aumpherID).FirstOrDefault();
                        if (aumpherObj != null)
                        {
                            oldSchoolAumpher = aumpherObj.AMPHUR_NAME;
                        }

                        string oldSchoolProvince = "";
                        int.TryParse(preRegister.oldSchoolProvince, out provinceID);
                        provinceObj = provinceList.Where(w => w.PROVINCE_ID == provinceID).FirstOrDefault();
                        if (provinceObj != null)
                        {
                            oldSchoolProvince = provinceObj.PROVINCE_NAME;
                        }

                        string fatherTitle = "";
                        titleObj = titleList.Where(w => w.nTitleid == preRegister.FatherTitle).FirstOrDefault();
                        if (titleObj != null)
                        {
                            fatherTitle = titleObj.titleDescription;
                        }

                        string motherTitle = "";
                        titleObj = titleList.Where(w => w.nTitleid == preRegister.MotherTitle).FirstOrDefault();
                        if (titleObj != null)
                        {
                            motherTitle = titleObj.titleDescription;
                        }

                        string familyTitle = "";
                        titleObj = titleList.Where(w => w.nTitleid == preRegister.nFamilyTitle).FirstOrDefault();
                        if (titleObj != null)
                        {
                            familyTitle = titleObj.titleDescription;
                        }

                        string fatherRace = "";
                        var raceObj = raceList.Where(w => w.MasterCode == preRegister.sFatherRace).FirstOrDefault();
                        if (raceObj != null)
                        {
                            fatherRace = raceObj.MasterDes;
                        }

                        string fatherNation = "";
                        var nationObj = nationList.Where(w => w.MasterCode == preRegister.sFatherNation).FirstOrDefault();
                        if (nationObj != null)
                        {
                            fatherNation = nationObj.MasterDes;
                        }

                        string fatherReligion = "";
                        var religionObj = religionList.Where(w => w.MasterCode == preRegister.sFatherReligion).FirstOrDefault();
                        if (religionObj != null)
                        {
                            fatherReligion = religionObj.MasterDes;
                        }

                        string motherRace = "";
                        raceObj = raceList.Where(w => w.MasterCode == preRegister.sMotherRace).FirstOrDefault();
                        if (raceObj != null)
                        {
                            motherRace = raceObj.MasterDes;
                        }

                        string motherNation = "";
                        nationObj = nationList.Where(w => w.MasterCode == preRegister.sMotherNation).FirstOrDefault();
                        if (nationObj != null)
                        {
                            motherNation = nationObj.MasterDes;
                        }

                        string motherReligion = "";
                        religionObj = religionList.Where(w => w.MasterCode == preRegister.sMotherReligion).FirstOrDefault();
                        if (religionObj != null)
                        {
                            motherReligion = religionObj.MasterDes;
                        }

                        string familyRace = "";
                        raceObj = raceList.Where(w => w.MasterCode == preRegister.sFamilyRace).FirstOrDefault();
                        if (raceObj != null)
                        {
                            familyRace = raceObj.MasterDes;
                        }

                        string familyNation = "";
                        nationObj = nationList.Where(w => w.MasterCode == preRegister.sFamilyNation).FirstOrDefault();
                        if (nationObj != null)
                        {
                            familyNation = nationObj.MasterDes;
                        }

                        string familyReligion = "";
                        religionObj = religionList.Where(w => w.MasterCode == preRegister.sFamilyReligion).FirstOrDefault();
                        if (religionObj != null)
                        {
                            familyReligion = religionObj.MasterDes;
                        }


                        ltrSchoolName.Text = schoolData.sCompany;
                        ltrRegisterYear.Text = (preRegister.registerYear + 543).ToString();
                        ltrStudentID.Text = preRegister.sStudentID?.ToString();
                        ltrRegisterLevel.Text = classLongName;
                        ltrPlan.Text = planName;
                        ltrRegisterDate.Text = preRegister.addDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                        switch (preRegister.sStudentReligion)
                        {
                            case "101": ltrReligion101.Text = "-check"; break;
                            case "102": ltrReligion102.Text = "-check"; break;
                            case "103": ltrReligion103.Text = "-check"; break;
                            default: ltrReligion999.Text = "-check"; break;
                        }
                        ltrStudentName.Text = userTitle + preRegister.sName + " " + preRegister.sLastname;
                        ltrStudentBirthday.Text = preRegister.dBirth?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                        ltrStudentAgeYear.Text = preRegister.dBirth != null ? ComFunction.CalculateAge(preRegister.dBirth.Value, "year") : "";
                        ltrStudentAgeMonth.Text = preRegister.dBirth != null ? ComFunction.CalculateAge(preRegister.dBirth.Value, "month") : "";

                        string sickFood = preRegister.sSickFood.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                        string sickDrug = preRegister.sSickDrug.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                        string sickOther = preRegister.sSickOther.Replace("-", "").Replace("ไม่แพ้", "").Replace("ไม่มี", "").Trim();
                        string sickNormal = preRegister.sSickNormal.Replace("-", "").Replace("ไม่มีโรค", "").Replace("ไม่มี", "").Trim();
                        string sickDanger = preRegister.sSickDanger.Replace("-", "").Replace("ไม่มีโรค", "").Replace("ไม่มี", "").Trim();
                        string symptom = "";
                        symptom += !string.IsNullOrEmpty(sickFood) ? ", " + sickFood : "";
                        symptom += !string.IsNullOrEmpty(sickDrug) ? ", " + sickDrug : "";
                        symptom += !string.IsNullOrEmpty(sickOther) ? ", " + sickOther : "";
                        symptom += !string.IsNullOrEmpty(sickNormal) ? ", " + sickNormal : "";
                        symptom += !string.IsNullOrEmpty(sickDanger) ? ", " + sickDanger : "";

                        ltrSymptoms.Text = !string.IsNullOrEmpty(symptom) ? symptom.Remove(0, 2) : "-";
                        ltrStudentHomeNumber.Text = preRegister.sStudentHomeNumber;
                        ltrStudentMuu.Text = preRegister.sStudentMuu;
                        ltrStudentSoy.Text = preRegister.sStudentSoy;
                        ltrStudentRoad.Text = preRegister.sStudentRoad;
                        ltrStudentTumbon.Text = stdTumbon;
                        ltrStudentAumpher.Text = stdAumpher;
                        ltrStudentProvince.Text = stdProvince;
                        ltrStudentPost.Text = preRegister.sStudentPost;
                        ltrPhone.Text = preRegister.sPhone;

                        ltrHouseRegistrationNumber.Text = preRegister.houseRegistrationNumber;
                        ltrHouseRegistrationMuu.Text = preRegister.houseRegistrationMuu;
                        ltrHouseRegistrationSoy.Text = preRegister.houseRegistrationSoy;
                        ltrHouseRegistrationRoad.Text = preRegister.houseRegistrationRoad;
                        ltrHouseRegistrationTumbon.Text = houseRegistrationTumbon;
                        ltrHouseRegistrationAumpher.Text = houseRegistrationAumpher;
                        ltrHouseRegistrationProvince.Text = houseRegistrationProvince;
                        ltrIdentification.Text = preRegister.sIdentification;

                        ltrFatherName.Text = fatherTitle + preRegister.sFatherFirstName + " " + preRegister.sFatherLastName;
                        ltrFatherIdentification.Text = preRegister.sFatherIdCardNumber;
                        ltrFatherAge.Text = preRegister.dFatherBirthDay != null ? ComFunction.CalculateAge(preRegister.dFatherBirthDay.Value, "year") : "";
                        ltrFatherRace.Text = fatherRace;
                        ltrFatherNation.Text = fatherNation;
                        ltrFatherReligion.Text = fatherReligion;
                        ltrFatherGraduated.Text = GraduatedName(preRegister.sFatherGraduated);
                        ltrFatherJob.Text = preRegister.sFatherJob;
                        ltrFatherIncome.Text = preRegister.nFatherIncome?.ToString("#,0");
                        ltrFatherWorkPlace.Text = preRegister.sFatherWorkPlace;
                        ltrFatherPhone1.Text = preRegister.sFatherPhone;
                        ltrFatherPhone2.Text = preRegister.sFatherPhone2;

                        ltrMotherName.Text = motherTitle + preRegister.sMotherFirstName + " " + preRegister.sMotherLastName;
                        ltrMotherIdentification.Text = preRegister.sMotherIdCardNumber;
                        ltrMotherAge.Text = preRegister.dMotherBirthDay != null ? ComFunction.CalculateAge(preRegister.dMotherBirthDay.Value, "year") : "";
                        ltrMotherRace.Text = motherRace;
                        ltrMotherNation.Text = motherNation;
                        ltrMotherReligion.Text = motherReligion;
                        ltrMotherGraduated.Text = GraduatedName(preRegister.sMotherGraduated);
                        ltrMotherJob.Text = preRegister.sMotherJob;
                        ltrMotherIncome.Text = preRegister.nMotherIncome?.ToString("#,0");
                        ltrMotherWorkPlace.Text = preRegister.sMotherWorkPlace;
                        ltrMotherPhone1.Text = preRegister.sMotherPhone;
                        ltrMotherPhone2.Text = preRegister.sMotherPhone2;

                        switch (preRegister.familyStatus)
                        {
                            case 1: ltrFamilyStatus01.Text = "-check"; break;
                            case 2: break;
                            case 3: ltrFamilyStatus03.Text = "-check"; break;
                            case 4: ltrFamilyStatus04.Text = "-check"; break;
                            case 5: ltrFamilyStatus05.Text = "-check"; break;
                            case 6: ltrFamilyStatus06.Text = "-check"; break;
                            case 7: ltrFamilyStatus07.Text = "-check"; break;
                            default: ltrFamilyStatus99.Text = "-check"; break;
                        }

                        ltrParentName.Text = familyTitle + preRegister.sFamilyName + " " + preRegister.sFamilyLast;
                        ltrParentIdentification.Text = preRegister.sFamilyIdCardNumber;
                        ltrParentAge.Text = preRegister.dFamilyBirthDay != null ? ComFunction.CalculateAge(preRegister.dFamilyBirthDay.Value, "year") : "";
                        ltrParentRace.Text = familyRace;
                        ltrParentNation.Text = familyNation;
                        ltrParentReligion.Text = familyReligion;
                        ltrParentRelate.Text = preRegister.sFamilyRelate;
                        ltrParentGraduated.Text = GraduatedName(preRegister.sFamilyGraduated);
                        ltrParentJob.Text = preRegister.sFamilyJob;
                        ltrParentIncome.Text = preRegister.nFamilyIncome?.ToString("#,0");
                        ltrParentWorkPlace.Text = preRegister.sFamilyWorkPlace;
                        ltrParentPhone1.Text = preRegister.sPhoneOne;
                        ltrParentPhone2.Text = preRegister.sPhoneTwo;

                        ltrOldSchoolName.Text = preRegister.oldSchoolName;
                        ltrOldSchoolTumbon.Text = oldSchoolTumbon;
                        ltrOldSchoolAumpher.Text = oldSchoolAumpher;
                        ltrOldSchoolProvince.Text = oldSchoolProvince;
                        ltrOldSchoolGraduated.Text = OldSchoolGraduatedName(preRegister.oldSchoolGraduated);
                        ltrOldSchoolGPA.Text = preRegister.oldSchoolGPA.ToString();

                    }
                }
            }
        }

        string GraduatedName(int? graduatedID)
        {
            string graduatedName = "";
            switch (graduatedID)
            {
                case 1: graduatedName = "ต่ำกว่าประถม"; break;
                case 2: graduatedName = "ประถมศึกษา"; break;
                case 3: graduatedName = "มัธยมศึกษาหรือเทียบเท่า"; break;
                case 4: graduatedName = "ปริญญาตรี"; break;
                case 5: graduatedName = "ปริญญาโท"; break;
                case 6: graduatedName = "ปริญญาเอก"; break;
                case 7: graduatedName = "มัธยมต้น"; break;
                case 8: graduatedName = "อนุปริญญา"; break;
                case 9: graduatedName = "ประกาศนียบัตรวิชาชีพ"; break;
                case 10: graduatedName = "ประกาศนียบัตรวิชาชีพชั้นสูง"; break;
            }
            return graduatedName;
        }

        string OldSchoolGraduatedName(string oldGraduated)
        {
            int.TryParse(oldGraduated, out int oldGraduatedID);
            string graduatedName = "";
            switch (oldGraduatedID)
            {
                case 11: graduatedName = "เตรียมอนุบาลศึกษา"; break;
                case 12: graduatedName = "อนุบาลศึกษา 1"; break;
                case 13: graduatedName = "อนุบาลศึกษา 2"; break;
                case 14: graduatedName = "อนุบาลศึกษา 3"; break;
                case 1: graduatedName = "ประถมศึกษาปีที่ 1"; break;
                case 2: graduatedName = "ประถมศึกษาปีที่ 2"; break;
                case 3: graduatedName = "ประถมศึกษาปีที่ 3"; break;
                case 4: graduatedName = "ประถมศึกษาปีที่ 4"; break;
                case 5: graduatedName = "ประถมศึกษาปีที่ 5"; break;
                case 6: graduatedName = "ประถมศึกษาปีที่ 6"; break;
                case 7: graduatedName = "มัธยมศึกษาตอนต้น"; break;
                case 8: graduatedName = "มัธยมศึกษาตอนปลาย"; break;
                case 9: graduatedName = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1"; break;
                case 15: graduatedName = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2"; break;
                case 16: graduatedName = "ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3"; break;
                case 10: graduatedName = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1"; break;
                case 17: graduatedName = "ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2"; break;
                case 18: graduatedName = "มัธยมศึกษาปีที่ 1"; break;
                case 19: graduatedName = "มัธยมศึกษาปีที่ 2"; break;
                case 20: graduatedName = "มัธยมศึกษาปีที่ 3"; break;
                case 21: graduatedName = "มัธยมศึกษาปีที่ 4"; break;
                case 22: graduatedName = "มัธยมศึกษาปีที่ 5"; break;
                case 23: graduatedName = "มัธยมศึกษาปีที่ 6"; break;
                default: graduatedName = oldGraduated; break;
            }

            return graduatedName;
        }
    }
}