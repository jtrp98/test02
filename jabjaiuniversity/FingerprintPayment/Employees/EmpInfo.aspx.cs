using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Employees.CsCode;
using JabjaiMainClass;
using System.Diagnostics;
using System.Web.Script.Serialization;
using FingerprintPayment.Helper;
using Newtonsoft.Json;

namespace FingerprintPayment.Employees
{
    [System.Web.Script.Services.ScriptService]
    public partial class EmpInfo : EmployeeGateway
    {
        private static string SessionPrimaryKey = "EMPLOYEEID";

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            //{
            //    sltEmpType.DataSource = Common.GetEmployeeTypeToDDL(UserData.CompanyID);
            //    sltEmpType.DataBind();
            //}

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

                        //sltJob
                        var jobLists = en.TJobLists.Where(w => w.SchoolID == schoolID && w.deleted != "1").ToList();
                        foreach (var r in jobLists)
                        {
                            this.ltrJob.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nJobid, r.jobDescription);
                        }

                        //sltDepartment
                        var departments = en.TDepartments.Where(w => w.SchoolID == schoolID && w.deleted != 1).ToList();
                        foreach (var r in departments)
                        {
                            this.ltrDepartment.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.DepID, r.departmentName);
                        }

                        //sltTitle
                        var titleLists = en.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();
                        foreach (var r in titleLists)
                        {
                            this.ltrTitle.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTitleid, r.titleDescription);
                        }

                        //sltProvince
                        //sltProvince2
                        var provinces = dbMaster.provinces.ToList();
                        foreach (var r in provinces)
                        {
                            this.ltrProvince.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.PROVINCE_ID, r.PROVINCE_NAME);
                        }
                        this.ltrProvince2.Text = this.ltrProvince.Text;

                        //sltTimeType
                        var timetypes = en.TTimetypes.Where(w => w.SchoolID == schoolID && w.cUserType == "2").ToList();
                        foreach (var r in timetypes)
                        {
                            this.ltrTimeType.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.nTimeType, r.sTimeType);
                        }

                        var emptype = en.TEmployeeTypes.Where(w => w.SchoolID == schoolID && w.IsActive == true && w.IsDel == false)
                           .Select(o => new
                           {
                               Value = (o.nTypeId2 ?? o.nTypeId) + "",
                               Text = o.Title,
                           })
                           .OrderBy(o => o.Value)
                           .ToList();
                        foreach (var r in emptype)
                        {
                            this.ltrEmpType.Text += string.Format(@"<option value=""{0}"">{1}</option>", r.Value, r.Text);
                        }

                        // Finger
                        int eid = Convert.ToInt32(Request.QueryString["eid"]);
                        if (eid != 0)
                        {
                            //var company = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                            var masterUser = dbMaster.TUsers.Where(w => w.sID == eid && w.nCompany == schoolID && w.cType == "1").FirstOrDefault();
                            if (masterUser != null)
                            {
                                if (masterUser.sFinger == null && masterUser.sFinger2 == null && masterUser.sPassword == "000000")
                                {
                                    ltrDelFinger.Visible = true;
                                }
                                else if (masterUser.sFinger == null && masterUser.sFinger2 == null)
                                {
                                    ltrFinger.Text = "รหัสลายนิ้วมือ :";
                                    ltrDelFinger.Visible = false;
                                    ltrPassword.Text = masterUser.sPassword;
                                }
                                else
                                {
                                    ltrFinger.Text = "ลายนิ้วมือ :";
                                }
                            }
                        }
                        else
                        {
                            ltrFinger.Text = "";
                            ltrDelFinger.Visible = false;
                        }

                        // Nation
                        var listNation = en.TMasterDatas.Where(w => w.MasterType == "3").OrderBy(o => o.MasterDes).ToList();
                        foreach (var l in listNation)
                        {
                            this.ltrNationality.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }

                        // Religion
                        var listReligion = en.TMasterDatas.Where(w => w.MasterType == "6").ToList();
                        foreach (var l in listReligion)
                        {
                            this.ltrReligion.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }

                        // Race
                        var listRace = en.TMasterDatas.Where(w => w.MasterType == "9").OrderBy(o => o.MasterDes).ToList();
                        foreach (var l in listRace)
                        {
                            this.ltrEthnicity.Text += string.Format(@"<option value=""{0}"">{1}</option>", l.MasterCode, l.MasterDes);
                        }
                    }
                    break;
                case "view":
                    MvContent.ActiveViewIndex = 2; break;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SaveItem(List<string> data)
        {
            string isComplete = "";
            bool flagComplete = true;

            string logMessage = "";
            int logAction = 0;

            var userData = GetUserData();
            int schoolID = userData.CompanyID;


            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
               
                    int empID = 0;
                    TEmployee pi = new TEmployee();
                

                    try
                    {
                        int empIDMaster = 0;

                        //TCompany qCompany;
                        MasterEntity.TUser userMasterData;


                        string[] data0 = data[0].Split('-');
                        string SectionID = data0[0];
                        empID = Convert.ToInt32(data0[1]);

                        switch (SectionID)
                        {
                            case "1":

                                #region Section 1

                                string EmpType = data[1];
                                string Code = data[2].Trim();
                                int? Job = string.IsNullOrEmpty(data[3]) ? (int?)null : int.Parse(data[3]);
                                int? Department = string.IsNullOrEmpty(data[4]) ? (int?)null : int.Parse(data[4]);
                                string Gender = data[5];
                                string Title = data[6];
                                string FirstName = data[7];
                                string LastName = data[8];
                                string FirstNameEn = data[9];
                                string LastNameEn = data[10];
                                string IDCardNumber = data[11];
                                string PassportNumber = data[12];
                                string PassportCountry = data[13];
                                DateTime? PassportExpirationDate = string.IsNullOrEmpty(data[49]) ? (DateTime?)null : DateTime.Parse(data[49], new CultureInfo("th-TH"));
                                string VisaNo = data[50];
                                DateTime? VisaExpirationDate = string.IsNullOrEmpty(data[51]) ? (DateTime?)null : DateTime.Parse(data[51], new CultureInfo("th-TH"));
                                string WorkPermitNo = data[52];
                                DateTime? WorkPermitExpirationDate = string.IsNullOrEmpty(data[53]) ? (DateTime?)null : DateTime.Parse(data[53], new CultureInfo("th-TH"));

                                DateTime? Birthday = string.IsNullOrEmpty(data[14]) ? (DateTime?)null : DateTime.Parse(data[14], new CultureInfo("th-TH"));
                                string BloodType = data[15];
                                string Nationality = data[16];
                                string Ethnicity = data[17];
                                string Religion = data[18];
                                int? PersonalStatus = string.IsNullOrEmpty(data[19]) ? (int?)null : int.Parse(data[19]);
                                string SpouseFirstName = data[20];
                                string SpouseLastName = data[21];
                                string Phone = data[22].Trim();
                                string Email = data[23];
                                int? TimeType = string.IsNullOrEmpty(data[24]) ? (int?)null : int.Parse(data[24]);

                                //string ProfileImage = data[25];
                                bool? RemoveOldPicture = string.IsNullOrEmpty(data[25]) ? (bool?)null : bool.Parse(data[25]); // Use edit mode only

                                bool UseBiometric = bool.Parse(data[47]);

                                if (empID == 0)
                                {
                                    // Check valid data
                                    string message = "";
                                    int checkExistUsername = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == Code)).Count();
                                    int checkExistIdentification = en.TEmployees.Where(w => w.SchoolID == schoolID && w.cDel == null && (((w.sIdentification ?? "") != "" && (w.sIdentification != "-") && w.sIdentification == IDCardNumber))).Count();
                                    int checkExistCodeAndPassportNumber = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.cDel == false && (((w.Code ?? "") != "" && (w.Code != "-") && w.Code == Code) || ((w.PassportNumber ?? "") != "" && w.PassportNumber == PassportNumber))).Count();
                                    if (checkExistUsername != 0)
                                    {
                                        message += ", username ซ้ำ: " + Code;
                                    }
                                    if (checkExistIdentification != 0)
                                    {
                                        message += ", เลขบัตรประชาชนซ้ำ: " + IDCardNumber;
                                    }
                                    if (checkExistCodeAndPassportNumber != 0)
                                    {
                                        message += ", รหัสพนักกงาน/เลขที่หนังสือเดินทางซ้ำ: " + (Code + "/" + PassportNumber);
                                    }

                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        isComplete = "warning-" + message.Remove(0, 2);
                                        flagComplete = false;
                                    }

                                    // Access & Save data to Master Database
                                    if (checkExistUsername == 0 && checkExistIdentification == 0 && checkExistCodeAndPassportNumber == 0)
                                    {
                                        userMasterData = new MasterEntity.TUser
                                        {
                                            sName = FirstName,
                                            sLastname = LastName,
                                            sIdentification = IDCardNumber,
                                            dBirth = Birthday,
                                            //sPassword = ComFunction.RandomNumber(),
                                            sPassword = "999999",
                                            sEmail = Email,
                                            nCompany = schoolID,
                                            cType = "1",
                                            username = Code,
                                            sPhone = Phone,
                                            cSex = Gender,
                                            dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 1),
                                            userpassword = (Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000"),
                                            PasswordHash = ComFunction.HashSHA1((Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000")),
                                            UseEncryptPassword = false,
                                            UseBiometric = UseBiometric
                                        };
                                        dbMaster.TUsers.Add(userMasterData);
                                        dbMaster.SaveChanges();

                                        empIDMaster = userMasterData.sID;
                                        userMasterData.nSystemID = empIDMaster;
                                        dbMaster.SaveChanges();

                                        // Insert Section
                                        empID = empIDMaster;

                                        // Get Item
                                        pi = new TEmployee
                                        {
                                            sEmp = empID,
                                            sName = FirstName,
                                            sLastname = LastName,
                                            sIdentification = IDCardNumber,
                                            dBirth = Birthday,
                                            cSex = Gender,
                                            sPhone = Phone,
                                            sEmail = Email,
                                            nTimeType = TimeType,
                                            cType = EmpType,
                                            sTitle = Title,
                                            nJobid = Job,
                                            nDepartmentId = Department,
                                            SchoolID = schoolID,

                                            dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 1),
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(5, 1)
                                        };

                                        en.TEmployees.Add(pi);

                                        en.SaveChanges();

                                        isComplete = "complete";

                                        logMessage = "เพิ่มข้อมูลบุคลากร(ประวัติส่วนตัว) [รหัส: " + empID + ", ชื่อ-นามสกุล: " + FirstName + " " + LastName + "]";
                                        logAction = 2;
                                    }
                                }
                                else
                                {
                                    // Check valid data
                                    string message = "";
                                    int checkExistUsername = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.sID != empID && w.cDel == null && ((w.username ?? "") != "" && (w.username != "-") && w.username == Code)).Count();
                                    int checkExistIdentification = en.TEmployees.Where(w => w.SchoolID == schoolID && w.sEmp != empID && w.cDel == null && (((w.sIdentification ?? "") != "" && (w.sIdentification != "-") && w.sIdentification == IDCardNumber))).Count();
                                    int checkExistCodeAndPassportNumber = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp != empID && w.cDel == false && (((w.Code ?? "") != "" && (w.Code != "-") && w.Code == Code) || ((w.PassportNumber ?? "") != "" && (w.PassportNumber != "-") && w.PassportNumber == PassportNumber))).Count();
                                    if (checkExistUsername != 0)
                                    {
                                        message += ", username ซ้ำ: " + Code;
                                    }
                                    if (checkExistIdentification != 0)
                                    {
                                        message += ", เลขบัตรประชาชนซ้ำ: " + IDCardNumber;
                                    }
                                    if (checkExistCodeAndPassportNumber != 0)
                                    {
                                        message += ", รหัสพนักกงาน/เลขที่หนังสือเดินทางซ้ำ: " + (Code + "/" + PassportNumber);
                                    }

                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        isComplete = "warning-" + message.Remove(0, 2);
                                        flagComplete = false;
                                    }

                                    // Modify Section
                                    if (checkExistUsername == 0 && checkExistIdentification == 0 && checkExistCodeAndPassportNumber == 0)
                                    {
                                        // Access to Master Database
                                        userMasterData = dbMaster.TUsers.Where(w => w.sID == empID && w.nCompany == schoolID && w.cType == "1").FirstOrDefault();
                                        if (userMasterData != null)
                                        {
                                            userMasterData.username = Code;

                                            userMasterData.sName = FirstName;
                                            userMasterData.sLastname = LastName;
                                            userMasterData.sIdentification = IDCardNumber;

                                            userMasterData.dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 2);

                                            userMasterData.dBirth = Birthday;

                                            userMasterData.sEmail = Email;
                                            userMasterData.UseBiometric = UseBiometric;

                                            if (RemoveOldPicture != null && RemoveOldPicture.Value)
                                            {
                                                userMasterData.sPicture = null;
                                            }
                                        }
                                        else
                                        {
                                            userMasterData = new MasterEntity.TUser
                                            {
                                                sName = FirstName,
                                                sLastname = LastName,
                                                sIdentification = IDCardNumber,
                                                dBirth = Birthday,
                                                //sPassword = ComFunction.RandomNumber(),
                                                sPassword = "999999",
                                                sEmail = Email,
                                                nCompany = schoolID,
                                                cType = "1",
                                                username = Code,
                                                sPhone = Phone,
                                                cSex = Gender,
                                                dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 2),
                                                userpassword = (Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000"),
                                                PasswordHash = ComFunction.HashSHA1((Birthday != null ? Birthday.Value.ToString("ddMMyyyy") : "00000000")),
                                                UseBiometric = UseBiometric
                                            };
                                            dbMaster.TUsers.Add(userMasterData);
                                        }
                                        dbMaster.SaveChanges();

                                        empIDMaster = userMasterData.sID;
                                        userMasterData.nSystemID = empIDMaster;
                                        dbMaster.SaveChanges();


                                        // Get Item
                                        pi = en.TEmployees.First(f => f.SchoolID == schoolID && f.sEmp == empID);

                                        pi.sName = FirstName;
                                        pi.sLastname = LastName;
                                        pi.sIdentification = IDCardNumber;
                                        pi.dBirth = Birthday;
                                        pi.cSex = Gender;
                                        pi.sPhone = Phone;
                                        pi.sEmail = Email;
                                        pi.nTimeType = TimeType;
                                        pi.cType = EmpType;
                                        if (RemoveOldPicture != null && RemoveOldPicture.Value)
                                        {
                                            pi.sPicture = null;
                                        }
                                        pi.sTitle = Title;
                                        pi.nJobid = Job;
                                        pi.nDepartmentId = Department;

                                        pi.dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 2);
                                        pi.UpdatedBy = userData.UserID;
                                        pi.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(5, 2);

                                        en.SaveChanges();

                                        isComplete = "complete";

                                        logMessage = "อัปเดตข้อมูลบุคลากร(ประวัติส่วนตัว) [รหัส: " + empID + ", ชื่อ-นามสกุล: " + FirstName + " " + LastName + "]";
                                        logAction = 3;
                                    }
                                }

                                if (isComplete == "complete")
                                {
                                    // ข้อมูลพนักงาน2 - TEmployeeInfo
                                    var empInfo = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == empID).OrderByDescending(o => o.ID).FirstOrDefault();
                                    if (empInfo == null)
                                    {
                                        // Insert Section
                                        // Get Item
                                        int ID = (int)(en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);
                                        TEmployeeInfo p = new TEmployeeInfo
                                        {
                                            sEmp = empID,
                                            ID = ID,
                                            Code = Code,
                                            FirstNameEn = FirstNameEn,
                                            LastNameEn = LastNameEn,
                                            PassportNumber = PassportNumber,
                                            PassportCountry = PassportCountry,
                                            PassportExpirationDate = PassportExpirationDate,
                                            VisaNo = VisaNo,
                                            VisaExpirationDate = VisaExpirationDate,
                                            WorkPermitNo = WorkPermitNo,
                                            WorkPermitExpirationDate = WorkPermitExpirationDate,
                                            BloodType = BloodType,
                                            Nationality = Nationality,
                                            Ethnicity = Ethnicity,
                                            Religion = Religion,
                                            PersonalStatus = PersonalStatus,
                                            SpouseFirstName = SpouseFirstName,
                                            SpouseLastName = SpouseLastName,
                                            SchoolID = schoolID,

                                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(5, 1),
                                            CreatedBy = userData.UserID
                                        };

                                        en.TEmployeeInfoes.Add(p);

                                        en.SaveChanges();
                                    }
                                    else
                                    {
                                        // Modify Section
                                        empInfo.Code = Code;
                                        empInfo.FirstNameEn = FirstNameEn;
                                        empInfo.LastNameEn = LastNameEn;
                                        empInfo.PassportNumber = PassportNumber;
                                        empInfo.PassportCountry = PassportCountry;
                                        empInfo.PassportExpirationDate = PassportExpirationDate;
                                        empInfo.VisaNo = VisaNo;
                                        empInfo.VisaExpirationDate = VisaExpirationDate;
                                        empInfo.WorkPermitNo = WorkPermitNo;
                                        empInfo.WorkPermitExpirationDate = WorkPermitExpirationDate;
                                        empInfo.BloodType = BloodType;
                                        empInfo.Nationality = Nationality;
                                        empInfo.Ethnicity = Ethnicity;
                                        empInfo.Religion = Religion;
                                        empInfo.PersonalStatus = PersonalStatus;
                                        empInfo.SpouseFirstName = SpouseFirstName;
                                        empInfo.SpouseLastName = SpouseLastName;

                                        empInfo.UpdateDate = DateTime.Now.FixSecondAndMillisecond(5, 2);
                                        empInfo.UpdateBy = userData.UserID;

                                        en.SaveChanges();
                                    }

                                    isComplete = "complete-" + empID;
                                }

                                #endregion            

                                break;
                            case "2":

                                #region Section 2

                                string No = data[26];
                                string VillageNo = data[27];
                                string Village = data[28];
                                string Alley = data[29];
                                string Building = data[30];
                                string Road = data[31];
                                string Province = data[32];
                                string Amphur = data[33];
                                string District = data[34];
                                string PostalCode = data[35];
                                int? ProvinceID = string.IsNullOrEmpty(data[32]) ? (int?)null : int.Parse(data[32]);
                                int? AmphurID = string.IsNullOrEmpty(data[33]) ? (int?)null : int.Parse(data[33]);
                                int? DistrictID = string.IsNullOrEmpty(data[34]) ? (int?)null : int.Parse(data[34]);

                                string DistrictName = "";
                                string AmphurName = "";
                                string ProvinceName = "";
                                var objDistrict = dbMaster.districts.Where(w => w.DISTRICT_ID == DistrictID).FirstOrDefault();
                                if (objDistrict != null)
                                {
                                    DistrictName = objDistrict.DISTRICT_NAME;
                                }
                                var objAmphur = dbMaster.amphurs.Where(w => w.AMPHUR_ID == AmphurID).FirstOrDefault();
                                if (objAmphur != null)
                                {
                                    AmphurName = objAmphur.AMPHUR_NAME;
                                }
                                var objProvince = dbMaster.provinces.Where(w => w.PROVINCE_ID == ProvinceID).FirstOrDefault();
                                if (objProvince != null)
                                {
                                    ProvinceName = objProvince.PROVINCE_NAME;
                                }

                                string Address = "";
                                if (!string.IsNullOrEmpty(Alley)) Address += " ซอย " + Alley;
                                if (!string.IsNullOrEmpty(VillageNo)) Address += " หมู่ " + VillageNo;
                                if (!string.IsNullOrEmpty(Road)) Address += " ถนน " + Road;
                                if (!string.IsNullOrEmpty(DistrictName)) Address += " ตำบล " + DistrictName;
                                if (!string.IsNullOrEmpty(AmphurName)) Address += " อำเภอ " + AmphurName;
                                if (!string.IsNullOrEmpty(ProvinceName)) Address += " จังหวัด " + ProvinceName;
                                if (!string.IsNullOrEmpty(Alley)) Address += " รหัสไปรษณีย์ " + PostalCode;

                                // Modify Section
                                //empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                                // Get Item
                                pi = en.TEmployees.First(f => f.SchoolID == schoolID && f.sEmp == empID);

                                pi.sProvince = Province;
                                pi.sTumbon = District;
                                pi.sSoy = Alley;
                                pi.sHomeNumber = No;
                                pi.sMuu = VillageNo;
                                pi.Village = Village;
                                pi.Building = Building;
                                pi.sRoad = Road;
                                pi.sAumpher = Amphur;
                                pi.sPost = PostalCode;
                                pi.sAddress = Address;

                                pi.dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 2);
                                pi.UpdatedBy = userData.UserID;
                                pi.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(5, 2);

                                en.SaveChanges();

                                // Access to Master Database
                                userMasterData = dbMaster.TUsers.Where(w => w.sID == empID && w.nCompany == schoolID && w.cType == "1").FirstOrDefault();
                                if (userMasterData != null)
                                {
                                    userMasterData.AMPHUR_ID = AmphurID;
                                    userMasterData.DISTRICT_ID = DistrictID;
                                    userMasterData.PROVINCE_ID = ProvinceID;
                                    userMasterData.sPostalcode = PostalCode;
                                    userMasterData.dUpdate = DateTime.Now.FixSecondAndMillisecond(5, 2);

                                    userMasterData.sAddress = Address;
                                }

                                dbMaster.SaveChanges();

                                isComplete = "complete-" + empID;

                                logMessage = "อัปเดตข้อมูลบุคลากร(ที่อยู่ตามทะเบียนบ้าน) [รหัส: " + empID + ", ชื่อ-นามสกุล: " + pi.sName + " " + pi.sLastname + "]";
                                logAction = 3;

                                #endregion

                                break;
                            case "3":

                                #region Section 3

                                int? UseHouseAddress = string.IsNullOrEmpty(data[36]) ? (int?)null : int.Parse(data[36]);
                                string No2 = data[37];
                                string VillageNo2 = data[38];
                                string Village2 = data[39];
                                string Alley2 = data[40];
                                string Building2 = data[41];
                                string Road2 = data[42];
                                int? Province2 = string.IsNullOrEmpty(data[43]) ? (int?)null : int.Parse(data[43]);
                                int? Amphur2 = string.IsNullOrEmpty(data[44]) ? (int?)null : int.Parse(data[44]);
                                int? District2 = string.IsNullOrEmpty(data[45]) ? (int?)null : int.Parse(data[45]);
                                string PostalCode2 = data[46];

                                // Modify Section
                                //empID = Convert.ToInt16(HttpContext.Current.Session[SessionPrimaryKey]);

                                // Address section
                                // ที่อยู่ปัจจุบัน
                                var empAddress = en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).OrderByDescending(o => o.ID).FirstOrDefault();
                                if (empAddress == null)
                                {
                                    // Insert Section
                                    // Get Item
                                    int ID = (int)(en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Count() == 0 ? 1 : en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == empID).Max(m => m.ID) + 1);
                                    TEmpAddress p = new TEmpAddress
                                    {
                                        sEmp = empID,
                                        ID = ID,
                                        Type = UseHouseAddress,
                                        No = No2,
                                        VillageNo = VillageNo2,
                                        Village = Village2,
                                        Building = Building2,
                                        Alley = Alley2,
                                        Road = Road2,
                                        SubdistrictID = District2,
                                        DistrictID = Amphur2,
                                        ProvinceID = Province2,
                                        Postcode = PostalCode2,
                                        SchoolID = schoolID,

                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(5, 1),
                                        CreatedBy = userData.UserID
                                    };

                                    en.TEmpAddresses.Add(p);

                                    en.SaveChanges();

                                    logMessage = "เพิ่มข้อมูลบุคลากร(ที่อยู่ที่ติดต่อได้) [รหัส: " + empID + "]";
                                    logAction = 2;
                                }
                                else
                                {
                                    // Modify Section
                                    empAddress.Type = UseHouseAddress;
                                    empAddress.No = No2;
                                    empAddress.VillageNo = VillageNo2;
                                    empAddress.Village = Village2;
                                    empAddress.Building = Building2;
                                    empAddress.Alley = Alley2;
                                    empAddress.Road = Road2;
                                    empAddress.SubdistrictID = District2;
                                    empAddress.DistrictID = Amphur2;
                                    empAddress.ProvinceID = Province2;
                                    empAddress.Postcode = PostalCode2;

                                    empAddress.UpdateDate = DateTime.Now.FixSecondAndMillisecond(5, 2);
                                    empAddress.UpdateBy = userData.UserID;

                                    en.SaveChanges();

                                    logMessage = "อัปเดตข้อมูลบุคลากร(ที่อยู่ที่ติดต่อได้) [รหัส: " + empID + "]";
                                    logAction = 3;
                                }

                                isComplete = "complete-" + empID;

                                #endregion

                                break;
                        }

                   

                        var f_usermaster = dbMaster.TUsers.Where(w => w.sID == empID).FirstOrDefault();
                        if (f_usermaster != null)
                        {
                            var p = en.TEmployees.FirstOrDefault(f => f.SchoolID == schoolID && f.sEmp == empID);
                            UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                            memory.Employee(p, f_usermaster);

                            JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, empID + "", "1", p.sPhone);
                        }

                    }
                    catch (Exception err)
                    {
                        isComplete = "error-" + err.Message + " :line " + ComFunction.GetLineNumberError(err);

                        flagComplete = false;

                     

                        string logMessagePattern = @"[SchoolEntities:{0}], [EmployeeData:{1}], [ErrorLine:{2}], [ErrorMessage:{3}]";
                        string errorMessage = err.Message;
                        string innerExceptionMessage = "";
                        while (err.InnerException != null) { innerExceptionMessage += ", " + err.InnerException.Message; err = err.InnerException; }
                        string logMessageDebug = string.Format(logMessagePattern, userData.Entities, new JavaScriptSerializer().Serialize(data), ComFunction.GetLineNumberError(err), errorMessage + ", " + innerExceptionMessage);

                        ComFunction.InsertLogDebug(null, null, userData.UserID, logMessageDebug);
                    }

                
            }

            if (flagComplete)
            {
                database.InsertLog(userData.UserID.ToString(), logMessage, HttpContext.Current.Request, 156, logAction, 0, schoolID);
            }

            return isComplete;
        }

        [WebMethod(EnableSession = true)]
        public static string GetItem(string empID)
        {
            int schoolID = GetUserData().CompanyID;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string infor = "new";

                try
                {
                    int iEmpID = 0;
                    if (!int.TryParse(empID, out iEmpID)) { iEmpID = 0; }

                    TEmployee p = en.TEmployees.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID).FirstOrDefault();
                    if (p != null)
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable("Table1");
                        for (int i = 1; i <= 53; i++)
                        {
                            dt.Columns.Add("F" + i);
                        }

                        dt.Rows.Add();

                        //HttpContext.Current.Session[SessionPrimaryKey] = p.sEmp;

                        dt.Rows[0]["F1"] = p.cType;
                        dt.Rows[0]["F3"] = p.nJobid?.ToString();
                        dt.Rows[0]["F4"] = p.nDepartmentId?.ToString();
                        dt.Rows[0]["F5"] = string.IsNullOrEmpty(p.cSex.Replace(" ", "")) ? "0" : p.cSex;
                        dt.Rows[0]["F6"] = p.sTitle;
                        dt.Rows[0]["F7"] = p.sName;
                        dt.Rows[0]["F8"] = p.sLastname;
                        dt.Rows[0]["F11"] = p.sIdentification;

                        dt.Rows[0]["F14"] = p.dBirth?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        dt.Rows[0]["F22"] = p.sPhone;
                        dt.Rows[0]["F23"] = p.sEmail;
                        dt.Rows[0]["F24"] = p.nTimeType?.ToString();

                        dt.Rows[0]["F25"] = p.sPicture;

                        bool useBiometric = false;
                        string originalStudentPicture = "";
                       
                        //var qCompany = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                        var userMaster = dbMaster.TUsers.Where(w => w.sID == p.sEmp && w.nCompany == schoolID && w.cType == "1").FirstOrDefault();
                        if (userMaster != null)
                        {
                            useBiometric = userMaster.UseBiometric;

                            originalStudentPicture = ComFunction.GetImageAsBase64Url(userMaster.sPicture);
                        }
                        dt.Rows[0]["F47"] = useBiometric;
                        dt.Rows[0]["F48"] = originalStudentPicture;

                        // ที่อยู่ตามทะเบียนบ้าน
                        dt.Rows[0]["F26"] = p.sHomeNumber;
                        dt.Rows[0]["F27"] = p.sMuu;
                        dt.Rows[0]["F28"] = p.Village;
                        dt.Rows[0]["F29"] = p.sSoy;
                        dt.Rows[0]["F30"] = p.Building;
                        dt.Rows[0]["F31"] = p.sRoad;
                        dt.Rows[0]["F32"] = p.sProvince;
                        dt.Rows[0]["F33"] = p.sAumpher;
                        dt.Rows[0]["F34"] = p.sTumbon;
                        dt.Rows[0]["F35"] = p.sPost;


                        // ที่อยู่ที่ติดต่อได้
                        TEmpAddress p2 = en.TEmpAddresses.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID).FirstOrDefault();
                        if (p2 != null)
                        {
                            dt.Rows[0]["F36"] = p2.Type?.ToString();
                            dt.Rows[0]["F37"] = p2.No;
                            dt.Rows[0]["F38"] = p2.VillageNo;
                            dt.Rows[0]["F39"] = p2.Village;
                            dt.Rows[0]["F40"] = p2.Alley;
                            dt.Rows[0]["F41"] = p2.Building;
                            dt.Rows[0]["F42"] = p2.Road;
                            dt.Rows[0]["F43"] = p2.ProvinceID?.ToString();
                            dt.Rows[0]["F44"] = p2.DistrictID?.ToString();
                            dt.Rows[0]["F45"] = p2.SubdistrictID?.ToString();
                            dt.Rows[0]["F46"] = p2.Postcode;
                        }

                        // ข้อมูลเพิ่มเติม บุคลากร
                        TEmployeeInfo p3 = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.sEmp == iEmpID).FirstOrDefault();
                        if (p3 != null)
                        {
                            dt.Rows[0]["F2"] = p3.Code;
                            dt.Rows[0]["F9"] = p3.FirstNameEn;
                            dt.Rows[0]["F10"] = p3.LastNameEn;
                            dt.Rows[0]["F12"] = p3.PassportNumber;
                            dt.Rows[0]["F13"] = p3.PassportCountry;
                            dt.Rows[0]["F15"] = p3.BloodType;
                            dt.Rows[0]["F16"] = p3.Nationality;
                            dt.Rows[0]["F17"] = p3.Ethnicity;
                            dt.Rows[0]["F18"] = p3.Religion;
                            dt.Rows[0]["F19"] = p3.PersonalStatus?.ToString();
                            dt.Rows[0]["F20"] = p3.SpouseFirstName;
                            dt.Rows[0]["F21"] = p3.SpouseLastName;

                            dt.Rows[0]["F49"] = p3.PassportExpirationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            dt.Rows[0]["F50"] = p3.VisaNo;
                            dt.Rows[0]["F51"] = p3.VisaExpirationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                            dt.Rows[0]["F52"] = p3.WorkPermitNo;
                            dt.Rows[0]["F53"] = p3.WorkPermitExpirationDate?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                        }

                        // Check verified email & phone number
                        List<TUserVerify> userVerifies = en.TUserVerifies.Where(w => w.SchoolID == schoolID && w.UserID == iEmpID && w.Status == 1).ToList();
                        if (userVerifies.Count > 0)
                        {
                            dt.Rows.Add();

                            if (userVerifies.Count(c => c.Type == 1 && c.Email == p.sEmail?.Trim()) > 0)
                            {
                                dt.Rows[1]["F23"] = true;
                            }
                            if (userVerifies.Count(c => c.Type == 2 && c.PhoneNumber == p.sPhone?.Trim()) > 0)
                            {
                                dt.Rows[1]["F22"] = true;
                            }
                        }
                        //

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

                //if (infor == "new" || infor == "error") HttpContext.Current.Session[SessionPrimaryKey] = null;

                return infor;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string LoadEmpType()
        {
            int schoolID = GetUserData().CompanyID;
            return JsonConvert.SerializeObject(Common.GetEmployeeTypeToDDL(schoolID));

        }

        [WebMethod]
        public static string ClearSessionID()
        {
            //HttpContext.Current.Session[SessionPrimaryKey] = null;

            return "";
        }

        #endregion
    }
}