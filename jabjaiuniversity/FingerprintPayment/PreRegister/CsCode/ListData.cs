using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.CsCode
{
    public class ListData    {    }

    /// <summary>
    /// Register display columm list
    /// </summary>
    public class ListDataRegister
    {
        public string check { get; set; }
        public string no { get; set; }
        //ชื่อ-นามสกุล
        public string StudentName { get; set; }
        //วันที่สมัคร
        public string RegisterDate { get; set; }
        //รหัสนักเรียน
        public string StudentCode { get; set; }
        //ระดับชั้น
        public string LevelName { get; set; }
        //สถานะ
        public string Status { get; set; }
        //สถานะการย้ายห้อง
        public string MoveStatus { get; set; }
        //ห้องสอบ
        public string ExamRoom { get; set; }
        //เลขที่นั่งสอบ
        public string ExamSeatNo { get; set; }
        //ผลสอบ
        public string ExamResults { get; set; }
        //ผู้บันทึกผลสอบ
        public string ExamResultsUpdateBy { get; set; }
        //วันที่บันทึกผลสอบ
        public string ExamResultsUpdateDate { get; set; }
        //แผน
        public string PlanName { get; set; }
        //สถานะข้อมูลเอกสาร
        public string CompleteDocuments { get; set; }
        //ผู้บันทึกข้อมูลเอกสาร(ครบ/ไม่ครบ)
        public string CompleteDocumentsUpdateBy { get; set; }
        //วันที่บันทึกข้อมูลเอกสาร
        public string CompleteDocumentsUpdateDate { get; set; }
        //รายละเอียดสำหรับกรณีเอกสารไม่ครบ CompleteDocuments = 0
        public string CompleteDocumentsInfo { get; set; }
        public string action { get; set; }
        public string rid { get; set; }
        public string schid { get; set; }
    }

    /// <summary>
    /// Register Plan display columm list
    /// </summary>
    public class ListDataRegisterPlan
    {
        public string no { get; set; }
        //ชั้น
        public string LevelName { get; set; }
        //แผน
        public string PlanName { get; set; }
        //รหัสแผน
        public string PlanCode { get; set; }
        public string action { get; set; }
        public string rid { get; set; }
        public string lid { get; set; }
        public string CanRemove { get; set; }
    }

    /// <summary>
    /// Register Recruitment display columm list
    /// </summary>
    public class ListDataRegisterRecruitment
    {
        public string no { get; set; }
        //ปีการศึกษา
        public string Year { get; set; }
        //ประเภทนักเรียน
        public string StudentType { get; set; }
        //ระดับชั้น
        public string LevelName { get; set; }
        //แผน
        public string PlanName { get; set; }
        //วันสิ้นสุดสมัครเรียนออนไลน์
        public string EndDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Register Exam Room display columm list
    /// </summary>
    public class ListDataRegisterExamRoom
    {
        public string no { get; set; }
        //แผนการเรียน
        public string PlanName { get; set; }
        //ชื่อห้องสอบ
        public string ExamRoomName { get; set; }
        //จำนวนที่นั่งสอบ
        public string Seats { get; set; }
        public string action { get; set; }
        public string erid { get; set; }
    }

}