using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.CsCode
{
    public class EntityQuery
    {
    }

    public class EntityRegisterDistrict
    {
        public int AMPHUR_ID { get; set; }
        public string AMPHUR_NAME { get; set; }
    }

    public class EntityRegisterSubDistrict
    {
        public int DISTRICT_ID { get; set; }
        public string DISTRICT_NAME { get; set; }
    }

    public class EntityStudentTypeSetup
    {
        public string ID { get; set; }
        public string StudentType { get; set; }
    }

    public class EntityPlanSetupClass
    {
        public int nTSubLevel { get; set; }
        public string SubLevel { get; set; }
    }

    public class EntityStudentClass
    {
        public string nTSubLevel { get; set; }
        public string SubLevel { get; set; }
    }

    public class EntityPlanSetupEduProgram
    {
        public int ID { get; set; }
        public string Planname { get; set; }
        public int RegisterMax { get; set; }
        public int RegisterAmount { get; set; }
    }

    public class EntityRegisterStudentTitle
    {
        public int nTitleid { get; set; }
        public string titleDescription { get; set; }
    }

    public class EntityRegularitySubLevel
    {
        public int ID { get; set; }
        public string SubLevel { get; set; }
        public string Filename { get; set; }
    }

    /// <summary>
    /// Dropdown entity query
    /// </summary>
    public class EntityDropdown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class EntityRegisterOnline
    {
        // Page 01
        public bool Page01Saved { get; set; }

        // Page 02
        public string IDCard { get; set; }
        public bool Page02Saved { get; set; }

        // Page 03
        public int Year { get; set; }
        public int YearBE { get; set; }
        public string StudentType { get; set; }
        public int Class { get; set; }
        public int? OptionTime { get; set; }
        public int? OptionBranch { get; set; }
        public int EduProgram { get; set; }
        public int? MainPlan { get; set; }
        public string BackupPlans { get; set; }
        public bool Page03Saved { get; set; }

        // Page 04
        public string ProfilePicture { get; set; }
        public string ProfilePictureName { get; set; }
        public string ProfilePictureContentType { get; set; }
        public string StudentCategory { get; set; }
        public int? StudentTitle { get; set; }
        public string StudentName { get; set; }
        public string StudentLastname { get; set; }
        public string StudentNameEn { get; set; }
        public string StudentLastnameEn { get; set; }
        public string StudentNickName { get; set; }
        public string StudentNickNameEn { get; set; }
        public string StudentSex { get; set; }
        public string StudentBirthday { get; set; } // DateTime?
        public string StudentIDCard { get; set; }
        public string StudentRace { get; set; }
        public string StudentNation { get; set; }
        public string StudentReligion { get; set; }
        public int? StudentSonTotal { get; set; }
        public int? StudentSonNumber { get; set; }
        public bool Page04Saved { get; set; }

        // Page 05
        public string HouseCode { get; set; }
        public string HouseHomeNumber { get; set; }
        public string HouseAlley { get; set; }
        public string HouseVillageNo { get; set; }
        public string HouseRoad { get; set; }
        public int? HouseProvince { get; set; }
        public int? HouseDistrict { get; set; }
        public int? HouseSubDistrict { get; set; }
        public string HousePostalCode { get; set; }
        public string HousePhone { get; set; }
        public string HouseEmail { get; set; }
        public bool Page05Saved { get; set; }

        // Page 06
        // SameHouseAddress= ['same']
        public string SameHouseAddress { get; set; }
        public string StudentHomeNumber { get; set; }
        public string StudentAlley { get; set; }
        public string StudentVillageNo { get; set; }
        public string StudentRoad { get; set; }
        public string StudentProvince { get; set; }
        public string StudentDistrict { get; set; }
        public string StudentSubDistrict { get; set; }
        public string StudentPostalCode { get; set; }
        public string StudentHousePhone { get; set; }
        public string StudentHouseEmail { get; set; }
        public int? StudentStayWithTitle { get; set; }
        public string StudentStayWithName { get; set; }
        public string StudentStayWithLast { get; set; }
        public string StudentStayWithEmergencyCall { get; set; }
        public int? StudentHomeType { get; set; }
        public string StudentNeighborName { get; set; }
        public string StudentNeighborLastName { get; set; }
        public int? StudentNeighborSubLevel { get; set; }
        public string StudentNeighborPhone { get; set; }

        public bool Page06Saved { get; set; }

        // Page 07
        public int? FatherTitle { get; set; }
        public string FatherName { get; set; }
        public string FatherLastname { get; set; }
        public string FatherNameEn { get; set; }
        public string FatherLastnameEn { get; set; }
        public string FatherBirthday { get; set; } // DateTime?
        public string FatherIDCard { get; set; }
        public string FatherRace { get; set; }
        public string FatherNation { get; set; }
        public string FatherReligion { get; set; }
        public int? FatherEducational { get; set; }
        public string FatherHomeNumber { get; set; }
        public string FatherAlley { get; set; }
        public string FatherVillageNo { get; set; }
        public string FatherRoad { get; set; }
        public string FatherProvince { get; set; }
        public string FatherDistrict { get; set; }
        public string FatherSubDistrict { get; set; }
        public string FatherPostalCode { get; set; }
        public string FatherCareer { get; set; }
        public string FatherWorkplace { get; set; }
        public double? FatherMonthIncome { get; set; }
        public string FatherYearIncome { get; set; }
        public string FatherHousePhone { get; set; }
        public string FatherMobilePhone { get; set; }
        public string FatherWorkPhone { get; set; }
        public string FatherEmail { get; set; }
        public bool Page07Saved { get; set; }

        // Page 08
        public int? MotherTitle { get; set; }
        public string MotherName { get; set; }
        public string MotherLastname { get; set; }
        public string MotherNameEn { get; set; }
        public string MotherLastnameEn { get; set; }
        public string MotherBirthday { get; set; } // DateTime?
        public string MotherIDCard { get; set; }
        public string MotherRace { get; set; }
        public string MotherNation { get; set; }
        public string MotherReligion { get; set; }
        public int? MotherEducational { get; set; }
        public string MotherHomeNumber { get; set; }
        public string MotherAlley { get; set; }
        public string MotherVillageNo { get; set; }
        public string MotherRoad { get; set; }
        public string MotherProvince { get; set; }
        public string MotherDistrict { get; set; }
        public string MotherSubDistrict { get; set; }
        public string MotherPostalCode { get; set; }
        public string MotherCareer { get; set; }
        public string MotherWorkplace { get; set; }
        public double? MotherMonthIncome { get; set; }
        public string MotherYearIncome { get; set; }
        public string MotherHousePhone { get; set; }
        public string MotherMobilePhone { get; set; }
        public string MotherWorkPhone { get; set; }
        public string MotherEmail { get; set; }
        public bool Page08Saved { get; set; }

        // Page 09
        public string ParentRelation { get; set; }
        public int? ParentTitle { get; set; }
        public string ParentName { get; set; }
        public string ParentLastname { get; set; }
        public string ParentNameEn { get; set; }
        public string ParentLastnameEn { get; set; }
        public string ParentBirthday { get; set; } // DateTime?
        public string ParentIDCard { get; set; }
        public string ParentRace { get; set; }
        public string ParentNation { get; set; }
        public string ParentReligion { get; set; }
        public int? ParentEducational { get; set; }
        public int? ParentStatus { get; set; }
        public string ParentHomeNumber { get; set; }
        public string ParentAlley { get; set; }
        public string ParentVillageNo { get; set; }
        public string ParentRoad { get; set; }
        public string ParentProvince { get; set; }
        public string ParentDistrict { get; set; }
        public string ParentSubDistrict { get; set; }
        public string ParentPostalCode { get; set; }
        public string ParentCareer { get; set; }
        public string ParentWorkplace { get; set; }
        public double? ParentMonthIncome { get; set; }
        public string ParentYearIncome { get; set; }
        public string ParentHousePhone { get; set; }
        public string ParentMobilePhone { get; set; }
        public string ParentWorkPhone { get; set; }
        public string ParentEmail { get; set; }
        public bool Page09Saved { get; set; }

        // Page 10
        // NoInstitution= ['no']
        public string NoInstitution { get; set; }
        public string OldSchoolName { get; set; }
        public string OldSchoolProvince { get; set; }
        public string OldSchoolDistrict { get; set; }
        public string OldSchoolSubDistrict { get; set; }
        public string OldSchoolEducational { get; set; }
        public double? GPA { get; set; }
        public bool Page10Saved { get; set; }

        // Page 11
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string Blood { get; set; }
        public string AllergySymptoms { get; set; }
        public string FoodAllergy { get; set; }
        public string BeAllergic { get; set; }
        public string OtherAllergic { get; set; }
        public string CongenitalDisease { get; set; }
        public string SeriousDisease { get; set; }
        public bool Page11Saved { get; set; }

        // Page 12
        public List<DocumentFile> Files { get; set; }
        public bool Page12Saved { get; set; }
    }

    public class DocumentFile
    {
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "fileName")]
        public string FileName { get; set; }

        [JsonProperty(PropertyName = "docId")]
        public int DocID { get; set; }

        [JsonProperty(PropertyName = "typeId")]
        public int TypeID { get; set; }

        [JsonProperty(PropertyName = "vfiId")]
        public int VFIID { get; set; }

        [JsonProperty(PropertyName = "byteData")]
        public string ByteData { get; set; }
    }

    public class EntityRegisterPrint
    {
        public string RegisterYear { get; set; }
        public string RegisterCode { get; set; }
        public string ExamCode { get; set; }
        public string FullDate { get; set; }
        public string Date { get; set; }
        public string Class { get; set; }
        public string ClassLongName { get; set; }
        public string StudentCategory { get; set; }
        public string PlanName { get; set; }
        public string StudentFullName { get; set; }
        public string StudentName { get; set; }
        public string StudentBirthday { get; set; }
        public string StudentAge { get; set; }
        public string IDCard { get; set; }
        public string StudentRace { get; set; }
        public string StudentNation { get; set; }
        public string StudentReligion { get; set; }
        public string StudentHomeNumber { get; set; }
        public string StudentAlley { get; set; }
        public string StudentRoad { get; set; }
        public string StudentSubDistrict { get; set; }
        public string StudentDistrict { get; set; }
        public string StudentProvince { get; set; }
        public string StudentPostalCode { get; set; }
        public string StudentHousePhone { get; set; }
        public string StudentHouseEmail { get; set; }

        public string FatherFullName { get; set; }
        public string FatherName { get; set; }
        public string FatherPhone { get; set; }
        public string FatherPhone2 { get; set; }
        public string FatherWorkPlace { get; set; }
        public string FatherJob { get; set; }
        public string FatherIncome { get; set; }

        public string MotherFullName { get; set; }
        public string MotherName { get; set; }
        public string MotherPhone { get; set; }
        public string MotherPhone2 { get; set; }
        public string MotherWorkPlace { get; set; }
        public string MotherJob { get; set; }
        public string MotherIncome { get; set; }

        public string ParentFullName { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string ParentPhone2 { get; set; }
        public string ParentWorkPlace { get; set; }
        public string ParentJob { get; set; }
        public string ParentIncome { get; set; }

        public string ChronicDisease { get; set; }
        public string OtherAllergy { get; set; }
        public string FoodAllergy { get; set; }
        public string DrugAllergy { get; set; }

        public string OldSchoolName { get; set; }
        public string OldSchoolProvince { get; set; }

        public string Fee { get; set; }

    }

    /// <summary>
    /// Register entity query
    /// </summary>
    public class EntityRegisterList
    {
        public int rID { get; set; }
        public string StudentName { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string StudentCode { get; set; }
        public int? LevelID { get; set; }
        public string LevelName { get; set; }
        public int? RoomID { get; set; }
        public string Room { get; set; }
        public int? StatusID { get; set; }
        public string Status { get; set; }
        public string MoveStatus { get; set; }
        public string Branch { get; set; }
        public string ExamRoom { get; set; }
        public string ExamSeatNo { get; set; }
        public string ExamResults { get; set; }
        public string ExamResultsUpdateBy { get; set; }
        public DateTime? ExamResultsUpdateDate { get; set; }
        public string PlanName { get; set; }
        public string CompleteDocuments { get; set; }
        public string CompleteDocumentsUpdateBy { get; set; }
        public DateTime? CompleteDocumentsUpdateDate { get; set; }
        public string CompleteDocumentsInfo { get; set; }
    }

    /// <summary>
    /// Register Plan entity query
    /// </summary>
    public class EntityRegisterPlanList
    {
        public int rID { get; set; }
        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public string PlanName { get; set; }
        public string PlanCode { get; set; }
        public string CanRemove { get; set; }
    }

    /// <summary>
    /// Register Recruitment entity query
    /// </summary>
    public class EntityRegisterRecruitmentList
    {
        public int ID { get; set; }
        public int YearID { get; set; }
        public int Year { get; set; }
        public string StudentTypeID { get; set; }
        public string StudentTypeName { get; set; }
        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// Register Exam Room entity query
    /// </summary>
    public class EntityRegisterExamRoomList
    {
        public int RegisterExamRoomID { get; set; }
        public string PlanName { get; set; }
        public string ExamRoomName { get; set; }
        public int Seats { get; set; }
    }

    /// <summary>
    /// Report PreRegister entity query
    /// </summary>
    public class EntityReportPreRegister
    {
        public int preRegisterId { get; set; }

        public DateTime? RegisterDate { get; set; }
        public int Year { get; set; }
        public string StudentCategory { get; set; }
        public string BranchName { get; set; }
        public string Time { get; set; }
        public string Course { get; set; }
        public string PlanName { get; set; }
        public string SubLevel { get; set; }
        public string StudentID { get; set; }
        public string ExamCode { get; set; }
        public string ExamResults { get; set; }
        public string CompleteDocuments { get; set; }
        public string CompleteDocumentsInfo { get; set; }
        public string Sex { get; set; }
        public string StudentTitle { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentName { get; set; }
        public string StudentFirstNameEn { get; set; }
        public string StudentLastNameEn { get; set; }
        public string StudentNameEn { get; set; }
        public string NickName { get; set; }
        public string NickNameEn { get; set; }
        public string StudentIdentityCard { get; set; }
        public DateTime? StudentBirth { get; set; }
        public string StudentRace { get; set; }
        public string StudentNation { get; set; }
        public string StudentReligion { get; set; }
        public int? SonNumber { get; set; }
        public string StudentHomeNumber { get; set; }
        public string StudentSoy { get; set; }
        public string StudentMuu { get; set; }
        public string StudentRoad { get; set; }
        public string StudentProvince { get; set; }
        public string StudentAumpher { get; set; }
        public string StudentTumbon { get; set; }
        public string StudentPost { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string HouseRegistrationHomeNumber { get; set; }
        public string HouseRegistrationSoy { get; set; }
        public string HouseRegistrationMuu { get; set; }
        public string HouseRegistrationRoad { get; set; }
        public string HouseRegistrationProvince { get; set; }
        public string HouseRegistrationAumpher { get; set; }
        public string HouseRegistrationTumbon { get; set; }
        public string HouseRegistrationPost { get; set; }
        public string HouseRegistrationPhone { get; set; }

        public double? Weight { get; set; }
        public double? Height { get; set; }
        public string Blood { get; set; }
        public string SickFood { get; set; }
        public string SickDrug { get; set; }
        public string SickOther { get; set; }
        public string SickNormal { get; set; }
        public string SickDanger { get; set; }
        public DateTime? AddDate { get; set; }
        public DateTime? MoveInDate { get; set; }
        public int? SaveAsSID { get; set; }
        public string ClassRoom { get; set; }
        public string PaymentStatus { get; set; }

        public string OldSchoolName { get; set; }
        public string OldSchoolProvince { get; set; }
        public string OldSchoolAumpher { get; set; }
        public string OldSchoolTumbon { get; set; }
        public double? OldSchoolGPA { get; set; }
        public string OldSchoolGraduated { get; set; }

        public string FamilyTitle { get; set; }
        public string FamilyFirstName { get; set; }
        public string FamilyLastName { get; set; }
        public string FamilyName { get; set; }
        public string FamilyIdentityCard { get; set; }
        public string FamilyRace { get; set; }
        public string FamilyNation { get; set; }
        public string FamilyReligion { get; set; }
        public string FamilyHomeNumber { get; set; }
        public string FamilySoy { get; set; }
        public string FamilyMuu { get; set; }
        public string FamilyRoad { get; set; }
        public string FamilyProvince { get; set; }
        public string FamilyAumpher { get; set; }
        public string FamilyTumbon { get; set; }
        public string FamilyPost { get; set; }
        public string PhoneOne { get; set; }
        public string PhoneTwo { get; set; }
        public string PhoneThree { get; set; }
        public string ParentEmail { get; set; }//
        public double? FamilyIncome { get; set; }
        public string FamilyGraduated { get; set; }//
        public string FamilyWorkPlace { get; set; }//
        public DateTime? FamilyBirthDay { get; set; }//
        public int? FamilyAge { get; set; }//
        public string FamilyJob { get; set; }
        public string FamilyRelation { get; set; }

        public string FatherTitle { get; set; }
        public string FatherFirstName { get; set; }
        public string FatherLastName { get; set; }
        public string FatherName { get; set; }
        public string FatherIdentityCard { get; set; }
        public string FatherRace { get; set; }
        public string FatherNation { get; set; }
        public string FatherReligion { get; set; }
        public string FatherHomeNumber { get; set; }
        public string FatherSoy { get; set; }
        public string FatherMuu { get; set; }
        public string FatherRoad { get; set; }
        public string FatherProvince { get; set; }
        public string FatherAumpher { get; set; }
        public string FatherTumbon { get; set; }
        public string FatherPost { get; set; }
        public string FatherPhone { get; set; }
        public string FatherEmail { get; set; }//
        public double? FatherIncome { get; set; }
        public string FatherGraduated { get; set; }//
        public string FatherWorkPlace { get; set; }//
        public DateTime? FatherBirthDay { get; set; }//
        public int? FatherAge { get; set; }//
        public string FatherJob { get; set; }

        public string MotherTitle { get; set; }
        public string MotherFirstName { get; set; }
        public string MotherLastName { get; set; }
        public string MotherName { get; set; }
        public string MotherIdentityCard { get; set; }
        public string MotherRace { get; set; }
        public string MotherNation { get; set; }
        public string MotherReligion { get; set; }
        public string MotherHomeNumber { get; set; }
        public string MotherSoy { get; set; }
        public string MotherMuu { get; set; }
        public string MotherRoad { get; set; }
        public string MotherProvince { get; set; }
        public string MotherAumpher { get; set; }
        public string MotherTumbon { get; set; }
        public string MotherPost { get; set; }
        public string MotherPhone { get; set; }
        public string MotherEmail { get; set; }//
        public double? MotherIncome { get; set; }
        public string MotherGraduated { get; set; }//
        public string MotherWorkPlace { get; set; }//
        public DateTime? MotherBirthDay { get; set; }//
        public int? MotherAge { get; set; }//
        public string MotherJob { get; set; }

        public string KnowFrom1 { get; set; }
        public string KnowFrom2 { get; set; }
        public string KnowFrom3 { get; set; }
        public string KnowFrom4 { get; set; }
        public string KnowFrom5 { get; set; }
        public string KnowFrom6 { get; set; }
        public string KnowFrom7 { get; set; }
        public string KnowFrom8 { get; set; }
        public string KnowFrom9 { get; set; }
        public string KnowFrom10 { get; set; }
        public string KnowFrom11 { get; set; }

        public int? OrderPlans { get; set; }
        public string BackupPlans { get; set; }
    }

    /// <summary>
    /// Report PreRegister Student Amount entity query
    /// </summary>
    public class EntityReportPreRegisterStudentAmount
    {
        public DateTime RegisterDate { get; set; }
        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public string PlanName { get; set; }
        public int SumMale { get; set; }
        public int SumFemale { get; set; }
        public int CountAll { get; set; }
    }

}