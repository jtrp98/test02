using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.CsCode
{
    public class EntityQuery { }

    /// <summary>
    /// Employee entity query
    /// </summary>
    public class EntityEmployee
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string WorkStatus { get; set; }
    }

    /// <summary>
    /// Employee Education entity query
    /// </summary>
    public class EntityEmpEducation
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? StudyYear { get; set; }
        public int? GraduationYear { get; set; }
        public int? LevelID { get; set; }
        public string Institution { get; set; }
    }

    /// <summary>
    /// Employee Fame entity query
    /// </summary>
    public class EntityEmpFame
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public string Department { get; set; }
        public int? Year { get; set; }
    }

    /// <summary>
    /// Employee Training entity query
    /// </summary>
    public class EntityEmpTraining
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string TrainingName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// Employee TOEIC entity query
    /// </summary>
    public class EntityEmpTOEIC
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? TOEICScore { get; set; }
        public string InstitutionAnnouncement { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    /// <summary>
    /// Employee Advancement entity query
    /// </summary>
    public class EntityEmpAdvancement
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public string Rank { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public string Province { get; set; }
        public DateTime? AdvancementDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    /// <summary>
    /// Employee Teaching entity query
    /// </summary>
    public class EntityEmpTeaching
    {
        public int Year { get; set; }
        public int Term { get; set; }
        public string GroupLearning { get; set; }
        public string SubjectCode { get; set; }
        public string Class { get; set; }
        public decimal? HourWeek { get; set; }
    }

    /// <summary>
    /// Employee Professional License entity query
    /// </summary>
    public class EntityEmpProfessLicense
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? LicenseType { get; set; }
        public string LicenseNo { get; set; }
        public string LicenseName { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }

    /// <summary>
    /// Employee Insignia entity query
    /// </summary>
    public class EntityEmpInsignia
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? Year { get; set; }
        public string Grade { get; set; }
        public string Position { get; set; }
        public DateTime? Date { get; set; }
    }

    /// <summary>
    /// Employee Family entity query
    /// </summary>
    public class EntityEmpFamily
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public string FamilyRelation { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public int? PersonalStatus { get; set; }
    }

    /// <summary>
    /// Employee Leave entity query
    /// </summary>
    public class EntityEmpLeave
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? Year { get; set; }
        public int? Late { get; set; }
        public int? Sick { get; set; }
        public int? Errand { get; set; }
        public int? Lacking { get; set; }
        public int? ToGovernor { get; set; }
        public int? Holiday { get; set; }
        public int? Maternity { get; set; }
        public int? Ordain { get; set; }
        public int? ContinueStudy { get; set; }
    }

    /// <summary>
    /// Employee Name Change entity query
    /// </summary>
    public class EntityEmpNameChange
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public string OldFirstName { get; set; }
        public string OldLastName { get; set; }
        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string ChangePlace { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    /// <summary>
    /// Employee Honor entity query
    /// </summary>
    public class EntityEmpHonor
    {
        public int EmpID { get; set; }
        public int ID { get; set; }
        public int? Year { get; set; }
        public string Image { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    /// <summary>
    /// Dropdown entity query
    /// </summary>
    public class EntityDropdown
    {
        public string id { get; set; }
        public string value { get; set; }
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


}