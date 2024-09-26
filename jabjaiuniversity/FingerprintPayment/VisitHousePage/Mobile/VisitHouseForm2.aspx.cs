using JabjaiEntity.DB;
using JabjaiMainClass;
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

namespace FingerprintPayment.VisitHousePage.Mobile
{
    public partial class VisitHouseForm2 : System.Web.UI.Page
    {
        protected string HousePictureOutSideDB = "";
        protected string HousePictureInSideDB = "";

        protected string HousePictureOutSidePreloaded = "";
        protected string HousePictureInSidePreloaded = "";

        protected string StudentPicture = "/Content/VisitHouse/assets/img/user.png";
        protected string StudentCode = "";
        protected string StudentName = "";
        protected string StudentClass = "";
        protected int StudentRoomID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            int.TryParse((string)Request.QueryString["schoolid"], out int schoolID);
            int.TryParse((string)Request.QueryString["sid"], out int studentID);

            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                StudentLogic studentLogic = new StudentLogic(dbSchool);
                string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                int? currentYearID = null;
                var termObj = dbSchool.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                if (termObj != null)
                {
                    currentYearID = termObj.nYear;
                }

                string query = string.Format(@"
SELECT u.sStudentID 'StudentID', u.sName 'Name', u.sLastname 'Lastname', u.sStudentPicture 'StudentPicture', sv.SubLevel 'Level', sv.nTSubLevel2 'Classroom', sv.nTermSubLevel2 'RoomID'
FROM TUser u LEFT JOIN TB_StudentViews sv ON u.SchoolID = sv.SchoolID AND u.sID = sv.sID 
WHERE u.SchoolID = {0} AND u.sID = {1} AND sv.nTerm = '{2}'", schoolID, studentID, currentTermID);
                var studentData = dbSchool.Database.SqlQuery<StudentData>(query).FirstOrDefault();
                if (studentData != null)
                {
                    if (!string.IsNullOrEmpty(studentData.StudentPicture)) StudentPicture = studentData.StudentPicture;

                    StudentCode = studentData.StudentID;
                    StudentName = studentData.Name + " " + studentData.Lastname;
                    var schoolData = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolData.ClassNameDisable ?? false)
                    {
                        // Hide room
                        StudentClass = studentData.Level;
                    }
                    else
                    {
                        StudentClass = studentData.Level + " / " + studentData.Classroom;
                    }
                    StudentRoomID = studentData.RoomID ?? 0;
                }

                THomeVisit homeVisit = dbSchool.THomeVisit.Where(w => w.SchoolID == schoolID && w.StudentID == studentID && w.YearID == currentYearID && w.TermID == currentTermID && w.IsDel == false).FirstOrDefault();
                if (homeVisit != null)
                {
                    List<THomeVisitFile> homeVisitFiles = dbSchool.THomeVisitFiles.Where(w => w.SchoolID == schoolID && w.HomeVisitID == homeVisit.ID && w.IsDel == false).ToList();

                    var housePictureOutSides = homeVisitFiles.Where(w => w.Type == 1).ToList();
                    foreach (var file in housePictureOutSides)
                    {
                        HousePictureOutSideDB += string.Format(@", {{id: {0}, type: 1, contentType: '{1}', fileName: '{2}', indb: true, status: 'modify'}}", file.ID, file.ContentType, file.FileName);
                        HousePictureOutSidePreloaded += string.Format(@", {{id: {0}, src: '{1}'}}", file.ID, file.Path);
                    }
                    if (!string.IsNullOrEmpty(HousePictureOutSideDB)) HousePictureOutSideDB = HousePictureOutSideDB.Remove(0, 2);
                    if (!string.IsNullOrEmpty(HousePictureOutSidePreloaded)) HousePictureOutSidePreloaded = HousePictureOutSidePreloaded.Remove(0, 2);

                    var housePictureInSides = homeVisitFiles.Where(w => w.Type == 2).ToList();
                    foreach (var file in housePictureInSides)
                    {
                        HousePictureInSideDB += string.Format(@", {{id: {0}, type: 2, contentType: '{1}', fileName: '{2}', indb: true, status: 'modify'}}", file.ID, file.ContentType, file.FileName);
                        HousePictureInSidePreloaded += string.Format(@", {{id: {0}, src: '{1}'}}", file.ID, file.Path);
                    }
                    if (!string.IsNullOrEmpty(HousePictureInSideDB)) HousePictureInSideDB = HousePictureInSideDB.Remove(0, 2);
                    if (!string.IsNullOrEmpty(HousePictureInSidePreloaded)) HousePictureInSidePreloaded = HousePictureInSidePreloaded.Remove(0, 2);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetVisitHouseData(int schoolID, int studentID)
        {
            bool success = true;
            string message = "Save Successfully";

            HomeVisitData data = new HomeVisitData();

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(dbSchool);
                        string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                        int? currentYearID = null;
                        var termObj = dbSchool.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        if (termObj != null)
                        {
                            currentYearID = termObj.nYear;
                        }

                        data = dbSchool.THomeVisit.Where(w => w.SchoolID == schoolID && w.StudentID == studentID && w.YearID == currentYearID && w.TermID == currentTermID && w.IsDel == false)
                            .Select(s => new HomeVisitData
                            {
                                Status = s.Status,
                                LINE = s.LINE,
                                Facebook = s.Facebook,
                                CreatedDate = s.CreatedDate,
                                UpdatedDate = s.UpdatedDate,
                                // 1. ข้อมูลผู้ปกครอง
                                HaveParents = s.HaveParents,
                                Relationship = s.Relationship,
                                RelationshipOther = s.RelationshipOther,
                                Fullname = s.Fullname,
                                PhoneNumber = s.PhoneNumber,
                                IDCardNumber = s.IDCardNumber,
                                Occupation = s.Occupation,
                                OccupationOther = s.OccupationOther,
                                HighestEducation = s.HighestEducation,
                                WelfareRegistersPoor = s.WelfareRegistersPoor,
                                // 2. ความสัมพันธ์ในครอบครัว
                                ResidentialHouse = s.ResidentialHouse,
                                DormitoryLivingWith = s.DormitoryLivingWith,
                                ResidentialHouseOther = s.ResidentialHouseOther,
                                OwnHome = s.OwnHome,
                                Cleanliness = s.Cleanliness,
                                CleanlinessOther = s.CleanlinessOther,
                                UtilitiesElectricity = s.UtilitiesElectricity,
                                WaterForConsumption = s.WaterForConsumption,
                                Toilet = s.Toilet,
                                LivingEnvironment = s.LivingEnvironment,
                                StudentFamilyMembersAmount = s.StudentFamilyMembersAmount,
                                StudentFamilyMembersMale = s.StudentFamilyMembersMale,
                                StudentFamilyMembersFemale = s.StudentFamilyMembersFemale,
                                SiblingsBornSameParentsAmount = s.SiblingsBornSameParentsAmount,
                                SiblingsBornSameParentsMale = s.SiblingsBornSameParentsMale,
                                SiblingsBornSameParentsFemale = s.SiblingsBornSameParentsFemale,
                                SiblingsBornDifferentParentsAmount = s.SiblingsBornDifferentParentsAmount,
                                SiblingsBornDifferentParentsMale = s.SiblingsBornDifferentParentsMale,
                                SiblingsBornDifferentParentsFemale = s.SiblingsBornDifferentParentsFemale,
                                FamiliesNeedSpecialAssistance = s.FamiliesNeedSpecialAssistance,
                                FamiliesNeedSpecialAssistanceTotal = s.FamiliesNeedSpecialAssistanceTotal,
                                FamilyRelationship = s.FamilyRelationship,
                                FamilyRelationshipOther = s.FamilyRelationshipOther,
                                RelationshipMember = s.RelationshipMember,
                                SpendTimeWithFamily = s.SpendTimeWithFamily,
                                WorkloadTheirFamilies = s.WorkloadTheirFamilies,
                                LeisureActivities = s.LeisureActivities,
                                LeaveStudent = s.LeaveStudent,
                                LeaveStudentOther = s.LeaveStudentOther,
                                MedianHouseholdIncome = s.MedianHouseholdIncome,
                                ReceiveExpensesFrom = s.ReceiveExpensesFrom,
                                ReceiveExpensesFromOther = s.ReceiveExpensesFromOther,
                                StudentWorkIncome = s.StudentWorkIncome,
                                StudentWorkIncomeOther = s.StudentWorkIncomeOther,
                                DailyIncome = s.DailyIncome,
                                PaidComeDay = s.PaidComeDay,
                                ParentWantAgencyHelp = s.ParentWantAgencyHelp2,
                                ParentWantAgencyHelpOther = s.ParentWantAgencyHelp2Other,
                                ParentConcerns = s.ParentConcerns,
                                ParentWantSchoolsHelp = s.ParentWantSchoolsHelp,
                                ParentWantSchoolsHelpOther = s.ParentWantSchoolsHelpOther,
                                // 3. พฤติกรรมและความเสี่ยง
                                Health = s.Health,
                                WelfareSafety = s.WelfareSafety,
                                DistanceHomeToSchool = s.DistanceHomeToSchool,
                                TravelTime = s.TravelTime,
                                StudentTravel = s.StudentTravel,
                                StudentTravelOther = s.StudentTravelOther,
                                LivingConditions = s.LivingConditions,
                                LivingConditionsOther = s.LivingConditionsOther,
                                StudentResponsibilities = s.StudentResponsibilities,
                                StudentResponsibilitiesOther = s.StudentResponsibilitiesOther,
                                Hobbies = s.Hobbies,
                                HobbiesOther = s.HobbiesOther,
                                SubstanceAbuseBehavior = s.SubstanceAbuseBehavior,
                                ViolentBehavior = s.ViolentBehavior,
                                ViolentBehaviorOther = s.ViolentBehaviorOther,
                                SexualBehavior = s.SexualBehavior,
                                GameAddiction = s.GameAddiction,
                                GameAddictionOther = s.GameAddictionOther,
                                AccessComputerInternet = s.AccessComputerInternet,
                                UseElectronicTools = s.UseElectronicTools,
                                StudentInfoProvider = s.StudentInfoProvider,
                                // 4. บันทึกเยี่ยมบ้าน
                                Note = s.Note,
                                HouseStyle = s.HouseStyle,
                                ParentSignature = s.ParentSignature,
                                StudentSignature = s.StudentSignature,
                                //TeacherSignature = s.TeacherSignature
                            })
                            .FirstOrDefault();

                        if (data != null)
                        {
                            data.SaveDate = data.CreatedDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                            data.UpdateDate = data.UpdatedDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            return JsonConvert.SerializeObject(new { success, message, data });
        }

        public class StudentData
        {
            public string StudentID { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string StudentPicture { get; set; }
            public string Level { get; set; }
            public string Classroom { get; set; }
            public int? RoomID { get; set; }
        }

        public class HomeVisitData
        {
            [JsonProperty(PropertyName = "Id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "schoolId")]
            public int SchoolID { get; set; }

            [JsonProperty(PropertyName = "roomId")]
            public int RoomID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "termId")]
            public string TermID { get; set; }

            [JsonProperty(PropertyName = "status")]
            public int? Status { get; set; }

            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }

            [JsonProperty(PropertyName = "saveDate")]
            public string SaveDate { get; set; }

            [JsonProperty(PropertyName = "updateDate")]
            public string UpdateDate { get; set; }

            [JsonProperty(PropertyName = "line")]
            public string LINE { get; set; }

            [JsonProperty(PropertyName = "facebook")]
            public string Facebook { get; set; }

            // 1. ข้อมูลผู้ปกครอง
            [JsonProperty(PropertyName = "haveParents")]
            public bool? HaveParents { get; set; }

            [JsonProperty(PropertyName = "relationship")]
            public int? Relationship { get; set; }

            [JsonProperty(PropertyName = "relationshipOther")]
            public string RelationshipOther { get; set; }

            [JsonProperty(PropertyName = "fullname")]
            public string Fullname { get; set; }

            [JsonProperty(PropertyName = "phoneNumber")]
            public string PhoneNumber { get; set; }

            [JsonProperty(PropertyName = "idCardNumber")]
            public string IDCardNumber { get; set; }

            [JsonProperty(PropertyName = "occupation")]
            public int? Occupation { get; set; }

            [JsonProperty(PropertyName = "occupationOther")]
            public string OccupationOther { get; set; }

            [JsonProperty(PropertyName = "highestEducation")]
            public int? HighestEducation { get; set; }

            [JsonProperty(PropertyName = "welfareRegistersPoor")]
            public bool? WelfareRegistersPoor { get; set; }

            // 2. บ้านที่พักอาศัย/ความสัมพันธ์ในครอบครัว
            [JsonProperty(PropertyName = "residentialHouse")]
            public int? ResidentialHouse { get; set; }

            [JsonProperty(PropertyName = "dormitoryLivingWith")]
            public string DormitoryLivingWith { get; set; }

            [JsonProperty(PropertyName = "residentialHouseOther")]
            public string ResidentialHouseOther { get; set; }

            [JsonProperty(PropertyName = "ownHome")]
            public int? OwnHome { get; set; }

            [JsonProperty(PropertyName = "cleanliness")]
            public int? Cleanliness { get; set; }

            [JsonProperty(PropertyName = "cleanlinessOther")]
            public string CleanlinessOther { get; set; }

            [JsonProperty(PropertyName = "utilitiesElectricity")]
            public int? UtilitiesElectricity { get; set; }

            [JsonProperty(PropertyName = "waterForConsumption")]
            public int? WaterForConsumption { get; set; }

            [JsonProperty(PropertyName = "toilet")]
            public int? Toilet { get; set; }

            [JsonProperty(PropertyName = "livingEnvironment")]
            public string LivingEnvironment { get; set; }

            [JsonProperty(PropertyName = "studentFamilyMembersAmount")]
            public int? StudentFamilyMembersAmount { get; set; }

            [JsonProperty(PropertyName = "studentFamilyMembersMale")]
            public int? StudentFamilyMembersMale { get; set; }

            [JsonProperty(PropertyName = "studentFamilyMembersFemale")]
            public int? StudentFamilyMembersFemale { get; set; }

            [JsonProperty(PropertyName = "siblingsBornSameParentsAmount")]
            public int? SiblingsBornSameParentsAmount { get; set; }

            [JsonProperty(PropertyName = "siblingsBornSameParentsMale")]
            public int? SiblingsBornSameParentsMale { get; set; }

            [JsonProperty(PropertyName = "siblingsBornSameParentsFemale")]
            public int? SiblingsBornSameParentsFemale { get; set; }

            [JsonProperty(PropertyName = "siblingsBornDifferentParentsAmount")]
            public int? SiblingsBornDifferentParentsAmount { get; set; }

            [JsonProperty(PropertyName = "siblingsBornDifferentParentsMale")]
            public int? SiblingsBornDifferentParentsMale { get; set; }

            [JsonProperty(PropertyName = "siblingsBornDifferentParentsFemale")]
            public int? SiblingsBornDifferentParentsFemale { get; set; }

            [JsonProperty(PropertyName = "familiesNeedSpecialAssistance")]
            public int? FamiliesNeedSpecialAssistance { get; set; }

            [JsonProperty(PropertyName = "familiesNeedSpecialAssistanceTotal")]
            public int? FamiliesNeedSpecialAssistanceTotal { get; set; }

            [JsonProperty(PropertyName = "familyRelationship")]
            public int? FamilyRelationship { get; set; }

            [JsonProperty(PropertyName = "familyRelationshipOther")]
            public string FamilyRelationshipOther { get; set; }

            [JsonProperty(PropertyName = "relationshipMember")]
            public string RelationshipMember { get; set; }

            [JsonProperty(PropertyName = "spendTimeWithFamily")]
            public decimal? SpendTimeWithFamily { get; set; }

            [JsonProperty(PropertyName = "workloadTheirFamilies")]
            public string WorkloadTheirFamilies { get; set; }

            [JsonProperty(PropertyName = "leisureActivities")]
            public string LeisureActivities { get; set; }

            [JsonProperty(PropertyName = "leaveStudent")]
            public int? LeaveStudent { get; set; }

            [JsonProperty(PropertyName = "leaveStudentOther")]
            public string LeaveStudentOther { get; set; }

            [JsonProperty(PropertyName = "medianHouseholdIncome")]
            public int? MedianHouseholdIncome { get; set; }

            [JsonProperty(PropertyName = "receiveExpensesFrom")]
            public int? ReceiveExpensesFrom { get; set; }

            [JsonProperty(PropertyName = "receiveExpensesFromOther")]
            public string ReceiveExpensesFromOther { get; set; }

            [JsonProperty(PropertyName = "studentWorkIncome")]
            public int? StudentWorkIncome { get; set; }

            [JsonProperty(PropertyName = "studentWorkIncomeOther")]
            public string StudentWorkIncomeOther { get; set; }

            [JsonProperty(PropertyName = "dailyIncome")]
            public int? DailyIncome { get; set; }

            [JsonProperty(PropertyName = "paidComeDay")]
            public int? PaidComeDay { get; set; }

            [JsonProperty(PropertyName = "parentWantAgencyHelp")]
            public string ParentWantAgencyHelp { get; set; }

            [JsonProperty(PropertyName = "parentWantAgencyHelpOther")]
            public string ParentWantAgencyHelpOther { get; set; }

            [JsonProperty(PropertyName = "parentConcerns")]
            public string ParentConcerns { get; set; }

            [JsonProperty(PropertyName = "parentWantSchoolsHelp")]
            public string ParentWantSchoolsHelp { get; set; }

            [JsonProperty(PropertyName = "parentWantSchoolsHelpOther")]
            public string ParentWantSchoolsHelpOther { get; set; }

            // 3. พฤติกรรมและความเสี่ยง
            [JsonProperty(PropertyName = "health")]
            public string Health { get; set; }

            [JsonProperty(PropertyName = "welfareSafety")]
            public string WelfareSafety { get; set; }

            [JsonProperty(PropertyName = "distanceHomeToSchool")]
            public int? DistanceHomeToSchool { get; set; }

            [JsonProperty(PropertyName = "travelTime")]
            public int? TravelTime { get; set; }

            [JsonProperty(PropertyName = "studentTravel")]
            public int? StudentTravel { get; set; }

            [JsonProperty(PropertyName = "studentTravelOther")]
            public string StudentTravelOther { get; set; }

            [JsonProperty(PropertyName = "livingConditions")]
            public string LivingConditions { get; set; }

            [JsonProperty(PropertyName = "livingConditionsOther")]
            public string LivingConditionsOther { get; set; }

            [JsonProperty(PropertyName = "studentResponsibilities")]
            public string StudentResponsibilities { get; set; }

            [JsonProperty(PropertyName = "studentResponsibilitiesOther")]
            public string StudentResponsibilitiesOther { get; set; }

            [JsonProperty(PropertyName = "hobbies")]
            public string Hobbies { get; set; }

            [JsonProperty(PropertyName = "hobbiesOther")]
            public string HobbiesOther { get; set; }

            [JsonProperty(PropertyName = "substanceAbuseBehavior")]
            public string SubstanceAbuseBehavior { get; set; }

            [JsonProperty(PropertyName = "violentBehavior")]
            public string ViolentBehavior { get; set; }

            [JsonProperty(PropertyName = "violentBehaviorOther")]
            public string ViolentBehaviorOther { get; set; }

            [JsonProperty(PropertyName = "sexualBehavior")]
            public string SexualBehavior { get; set; }

            [JsonProperty(PropertyName = "gameAddiction")]
            public string GameAddiction { get; set; }

            [JsonProperty(PropertyName = "gameAddictionOther")]
            public string GameAddictionOther { get; set; }

            [JsonProperty(PropertyName = "accessComputerInternet")]
            public int? AccessComputerInternet { get; set; }

            [JsonProperty(PropertyName = "useElectronicTools")]
            public string UseElectronicTools { get; set; }

            [JsonProperty(PropertyName = "studentInfoProvider")]
            public int? StudentInfoProvider { get; set; }

            // 4. บันทึกเยี่ยมบ้าน
            [JsonProperty(PropertyName = "note")]
            public string Note { get; set; }

            [JsonProperty(PropertyName = "houseStyle")]
            public int? HouseStyle { get; set; }

            [JsonProperty(PropertyName = "housePictureOutSide")]
            public List<HomeVisitFileData> HousePictureOutSide { get; set; }

            [JsonProperty(PropertyName = "housePictureInSide")]
            public List<HomeVisitFileData> HousePictureInSide { get; set; }

            [JsonProperty(PropertyName = "parentSignature")]
            public string ParentSignature { get; set; }

            [JsonProperty(PropertyName = "studentSignature")]
            public string StudentSignature { get; set; }

            [JsonProperty(PropertyName = "teacherSignature")]
            public string TeacherSignature { get; set; }
        }

        public class HomeVisitFileData
        {
            [JsonProperty(PropertyName = "Id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "type")]
            public int Type { get; set; }

            [JsonProperty(PropertyName = "contentType")]
            public string ContentType { get; set; }

            [JsonProperty(PropertyName = "fileName")]
            public string FileName { get; set; }

            [JsonProperty(PropertyName = "path")]
            public string Path { get; set; }

            // indb: true, false  
            [JsonProperty(PropertyName = "indb")]
            public bool InDB { get; set; }

            // status: new, modify, delete
            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
        }

    }
}