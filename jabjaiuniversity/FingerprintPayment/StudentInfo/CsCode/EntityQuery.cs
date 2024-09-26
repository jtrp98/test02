using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentInfo.CsCode
{
    public class EntityQuery
    {
    }

    /// <summary>
    /// Dropdown entity query
    /// </summary>
    public class EntityDropdown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Dropdown 2 entity query
    /// </summary>
    public class EntityDropdown2
    {
        public int id { get; set; }
        public string name { get; set; }
        public int lid { get; set; }
    }

    /// <summary>
    /// Student entity query
    /// </summary>
    public class EntityStudentList
    {
        public int sID { get; set; }
        public int No { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public string LevelName { get; set; }
        public string Status { get; set; }
        public bool FingerStatus { get; set; }
        public string TermID { get; set; }
        public string Note { get; set; }
        public int Year { get; set; }
    }

    /// <summary>
    /// Student Print QRCode entity query
    /// </summary>
    public class EntityStudentPrintQRCodeList
    {
        public int sID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }

    /// <summary>
    /// Student Health Growth entity query
    /// </summary>
    public class EntityStudentHealthGrowth
    {
        //nTSubLevel
        public int LevelID { get; set; }
        public string SubLevel { get; set; }
        public string FullName { get; set; }
        public int CurrentYear { get; set; }
        public decimal? Weight5 { get; set; }
        public decimal? Height5 { get; set; }
        public decimal? Weight8 { get; set; }
        public decimal? Height8 { get; set; }
        public decimal? Weight11 { get; set; }
        public decimal? Height11 { get; set; }
        public decimal? Weight2 { get; set; }
        public decimal? Height2 { get; set; }
    }

    /// <summary>
    /// Student Health Growth entity query
    /// </summary>
    public class EntityNewOrderSubLevel
    {
        public int CurrentYear { get; set; }
        //nTSubLevel
        public int LevelID { get; set; }
    }

    /// <summary>
    /// Student entity query
    /// </summary>
    public class EntityStudentListForAPI
    {
        public int sID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public int YearID { get; set; }
        public string TermID { get; set; }
        public string Term { get; set; }
        public DateTime? SendDate { get; set; }
        public int? StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public DateTime? SendDate2 { get; set; }
        public int? StatusCode2 { get; set; }
        public string ResponseContent2 { get; set; }
    }

    /// <summary>
    /// Json Plane entity query
    /// </summary>
    public class EntityJsonPlane
    {
        public int sID { get; set; }
        public DateTime LastSendDate { get; set; }
        public int SubjectCount { get; set; }
        public int Success { get; set; }
        public string JsonPlane { get; set; }
    }

    /// <summary>
    /// Retest Student List By Subject entity query
    /// </summary>
    public class EntityRetestStudentListBySubject
    {
        public long GroupNo { get; set; }
        public long No { get; set; }
        public string ClassRoom { get; set; }
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string TermYear { get; set; }
        public string Grade { get; set; }
    }

    /// <summary>
    /// Retest Student List By Class Room entity query
    /// </summary>
    public class EntityRetestStudentListByClassRoom
    {
        public long No { get; set; }
        public int sID { get; set; }
        public string StudentID { get; set; }
        public string Name { get; set; }
        public string ClassRoom { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public string TermYear { get; set; }
        public string nTerm { get; set; }
        public double MaxScoreMid50Perc { get; set; }
        public string ScoreMid { get; set; }

        public int nTermSubLevel2 { get; set; }

        public int nTSubLevel { get; set; }
        public int sPlaneID { get; set; }

        public bool IsStudentStudyThisSubject { get; set; }

        public int nStudentNumber { get; set; }
    }

    /// <summary>
    /// Student Report Fail Exam 01 entity query
    /// </summary>
    public class EntityStudentReportFailExam01
    {
        public long RowNumber { get; set; }
        public long No { get; set; }
        public int sID { get; set; }
        public string StudentID { get; set; }
        public string Level { get; set; }
        public string ClassRoom { get; set; }
        public string Name { get; set; }
        public string SubjectResultGrade { get; set; }
        public string CourseCode { get; set; }
    }

    /// <summary>
    /// Student Resignation Report entity query
    /// </summary>
    public class EntityStudentResignationReport
    {
        public string Level { get; set; }
        public string ClassRoom { get; set; }
        public string LevelClassRoom { get; set; }
        public long No { get; set; }
        public string StudentID { get; set; }
        public string Fullname { get; set; }
        public string YearTermCurriculumPlan { get; set; }
        public DateTime? DropOutDate { get; set; }
        public string Note { get; set; }
    }

    /// <summary>
    /// Request Update Student entity query
    /// </summary>
    public class EntityRequestUpdateStudentList
    {
        public int SchoolID { get; set; }
        public int StudentID { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string ApproveStatus { get; set; }
        public string StudentCode { get; set; }
        public string Classroom { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }

}