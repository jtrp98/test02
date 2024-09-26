using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentInfo.CsCode
{
    public class ListData    {    }

    /// <summary>
    /// Student display columm list
    /// </summary>
    public class ListDataStudent
    {
        public string no { get; set; }
        //เลขที่
        public string No { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        //ห้อง
        public string ClassName { get; set; }
        //ชั้น
        public string LevelName { get; set; }
        //สถานะ
        public string Status { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string fingerStatus { get; set; }
        public string tid { get; set; }
        //ปีการศึกษา
        public string Year { get; set; }
        //note
        public string Note { get; set; }
    }

    /// <summary>
    /// Student Print QRCode display columm list
    /// </summary>
    public class ListDataStudentPrintQRCode
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
    }

    /// <summary>
    /// Student display columm list
    /// </summary>
    public class ListDataStudentForAPI
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        //ชั้น
        public string ClassName { get; set; }
        //สถานะ
        public string Status { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string yid { get; set; }
        public string tid { get; set; }
        //วันที่ส่งข้อมูลนร.เข้าศูนย์กลาง
        public string SendDate { get; set; }
        public string StatusCode { get; set; }
        public string ResponseContent { get; set; }
        public string SendDate2 { get; set; }
        public string StatusCode2 { get; set; }
        public string ResponseContent2 { get; set; }

        public string LastSendDate { get; set; }
        public string SubjectCount { get; set; }
        public string Success { get; set; }
        public string JsonPlane { get; set; }
    }

    /// <summary>
    /// Retest Student List By Subject display columm list
    /// </summary>
    public class ListDataRetestStudentListBySubject
    {
        // ลำดับ(เรียงตามห้อง)
        public string GroupNo { get; set; }
        // ลำดับ
        public string No { get; set; }
        // ชั้น/ห้อง
        public string ClassRoom { get; set; }
        // เลขประจำตัว
        public string StudentID { get; set; }
        // ชื่อ-นามสกุล
        public string Name { get; set; }
        // ภาค/ปีการศึกษา
        public string TermYear { get; set; }
        // เกรด
        public string Grade { get; set; }
        // หมายเหตุ
        public string Note { get; set; }
    }

    /// <summary>
    /// Retest Student List By Class Room display columm list
    /// </summary>
    public class ListDataRetestStudentListByClassRoom
    {
        public string no { get; set; }
        // ลำดับ
        public string No { get; set; }
        // เลขประจำตัว
        public string StudentID { get; set; }
        // ชื่อ-นามสกุล
        public string Name { get; set; }
        // ชั้น/ห้อง
        public string ClassRoom { get; set; }
        // วิชา
        public string Subject { get; set; }
        // เกรด
        public string Grade { get; set; }
        // คะแนนกลางภาค
        public string ScoreMid { get; set; }
        // ภาค/ปีการศึกษา
        public string TermYear { get; set; }
        // หมายเหตุ
        public string Note { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string tid { get; set; }

        public int termSublevel2 { get; set; }
    }

    /// <summary>
    /// Student Report Fail Exam 01 display columm list
    /// </summary>
    public class ListDataStudentReportFailExam01
    {
        // ลำดับ
        public string No { get; set; }
        // ชั้น
        public string Level { get; set; }
        // ห้อง
        public string ClassRoom { get; set; }
        // ชื่อ - นามสกุล
        public string Name { get; set; }
        // วิชาที่ขาดสอบ
        public string SubjectResultGrade { get; set; }
        public string sid { get; set; }
        public string rn { get; set; }
    }

    /// <summary>
    /// Student Resignation Report columm list
    /// </summary>
    public class ListDataStudentResignationReport
    {
        // ชั้น/ห้อง
        public string ClassRoom { get; set; }
        // ลำดับ
        public string No { get; set; }
        // เลขประจำตัว
        public string StudentID { get; set; }
        // ชื่อ-นามสกุล
        public string Fullname { get; set; }
        // ปีการศึกษา / ภาคเรียน / แผนก / แผนที่ลาออก
        public string YearTermCurriculumPlan { get; set; }
        // วันที่ลาออก
        public string DropOutDate { get; set; }
        // สาเหตุที่ลาออก
        public string Note { get; set; }
    }

    /// <summary>
    /// Request Update Student display columm list
    /// </summary>
    public class ListDataRequestUpdateStudent
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //ชั้นเรียน
        public string Classroom { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        //วันที่ส่งแก้ไข
        public string RequestDate { get; set; }
        //วันที่ปรับสถานะ
        public string ApproveDate { get; set; }
        //สถานะ
        public string Status { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string requestDate { get; set; }
    }

    public class RegisResponse
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<RegisErrors> Errors { get; set; }
    }

    public class RegisErrors
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "recordOrder")]
        public string RecordOrder { get; set; }
    }
}