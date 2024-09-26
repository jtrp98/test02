using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.CsCode
{
    public class ListData { }

    /// <summary>
    /// Employee display columm list
    /// </summary>
    public class ListDataEmployee
    {
        public string no { get; set; }
        //ประเภทบุคลากร
        public string Type { get; set; }
        //รหัสพนักงาน
        public string Code { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string FirstName { get; set; }
        //นามสกุล
        public string LastName { get; set; }
        //เบอร์โทรศัพท์
        public string PhoneNumber { get; set; }
        //วันเกิด
        public string Birthday { get; set; }
        //วันที่แก้ไขข้อมูล
        public string UpdateDate { get; set; }
        //สถานะการทำงาน
        public string WorkStatus { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Education display columm list
    /// </summary>
    public class ListDataEmpEducation
    {
        public string no { get; set; }
        //ปีที่เริ่มการศึกษา
        public string StudyYear { get; set; }
        //ปีที่สำเร็จการศึกษา
        public string GraduationYear { get; set; }
        //ระดับการศึกษา - TLevel.nTLevel
        public string Level { get; set; }
        //สถาบันการศึกษา
        public string Institution { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Fame display columm list
    /// </summary>
    public class ListDataEmpFame
    {
        public string no { get; set; }
        //ประเภทเกียรติคุณ
        public string Type { get; set; }
        //หน่วยงานที่มอบ
        public string Department { get; set; }
        //ปีที่ได้รับ
        public string Year { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Training display columm list
    /// </summary>
    public class ListDataEmpTraining
    {
        public string no { get; set; }
        //ชื่อโครงการ
        public string ProjectName { get; set; }
        //ชื่อหลักสูตรอบรม
        public string TrainingName { get; set; }
        //วันที่เริ่มอบรม
        public string StartDate { get; set; }
        //วันที่สิ้นสุดอบรม
        public string EndDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee TOEIC display columm list
    /// </summary>
    public class ListDataEmpTOEIC
    {
        public string no { get; set; }
        //คะแนน TOEIC
        public string TOEICScore { get; set; }
        //สถาบันที่ได้รับใบประกาศ
        public string InstitutionAnnouncement { get; set; }
        //วันหมดอายุ
        public string ExpirationDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Advancement display columm list
    /// </summary>
    public class ListDataEmpAdvancement
    {
        public string no { get; set; }
        //ระดับขั้น - TRank.RANK_ID
        public string Rank { get; set; }
        //ตำแหน่ง - TJobList.nJobid
        public string Job { get; set; }
        //หน่วยงาน - TDepartment.departmentId
        public string Department { get; set; }
        //จังหวัด - master.province.PROVINCE_ID
        public string Province { get; set; }
        //วันที่เลื่อนตำแหน่ง
        public string AdvancementDate { get; set; }
        //วันที่แก้ไขข้อมูล
        public string UpdateDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Teaching display columm list
    /// </summary>
    public class ListDataEmpTeaching
    {
        public string no { get; set; }
        //ปีการศึกษา - TYear.nYear
        public string Year { get; set; }
        //ภาคเรียน - TTerm.nTerm
        public string Term { get; set; }
        //กลุ่มสาระการเรียนรู้ - TCourseType.courseTypeId
        public string GroupLearning { get; set; }
        //วิชาที่สอน - TSubject.SUBJECT_ID
        public string SubjectCode { get; set; }
        //ชั้นเรียน - TClass.sClassID
        public string Class { get; set; }
        //ชั่วโมงสอน/สัปดาร์
        public string HourWeek { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Professional License display columm list
    /// </summary>
    public class ListDataEmpProfessLicense
    {
        public string no { get; set; }
        //ประเภทใบประกอบวิชาชีพ
        public string LicenseType { get; set; }
        //เลขที่ใบประกอบวิชาชีพ
        public string LicenseNo { get; set; }
        //ชื่อใบประกอบวิชาชีพ
        public string LicenseName { get; set; }
        //วันที่ออกใบประกอบ
        public string IssuedDate { get; set; }
        //วันที่หมดอายุใบประกอบ
        public string ExpireDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Insignia display columm list
    /// </summary>
    public class ListDataEmpInsignia
    {
        public string no { get; set; }
        //ปีที่ได้รับ
        public string Year { get; set; }
        //ชั้นเครื่องราชฯ
        public string Grade { get; set; }
        //ตำแหน่ง
        public string Position { get; set; }
        //ลงวันที่
        public string Date { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Family display columm list
    /// </summary>
    public class ListDataEmpFamily
    {
        public string no { get; set; }
        //ความสัมพันธ์ของคนในครอบครัว
        public string FamilyRelation { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string FirstName { get; set; }
        //นามสกุล
        public string LastName { get; set; }
        //วันเกิด
        public string Birthday { get; set; }
        //สถานภาพสมรส
        public string PersonalStatus { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Leave display columm list
    /// </summary>
    public class ListDataEmpLeave
    {
        public string no { get; set; }
        //ปี
        public string Year { get; set; }
        //มาสาย
        public string Late { get; set; }
        //ลาป่วย
        public string Sick { get; set; }
        //ลากิจ
        public string Errand { get; set; }
        //ขาด
        public string Lacking { get; set; }
        //ไปราชการ
        public string ToGovernor { get; set; }
        //ลาพักผ่อน
        public string Holiday { get; set; }
        //ลาคลอด
        public string Maternity { get; set; }
        //ลาบวช/ฮัจช์
        public string Ordain { get; set; }
        //ลาศึกษาต่อ
        public string ContinueStudy { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Name Change display columm list
    /// </summary>
    public class ListDataEmpNameChange
    {
        public string no { get; set; }
        //ชื่อเก่า
        public string OldFirstName { get; set; }
        //นามสกุลเก่า
        public string OldLastName { get; set; }
        //ชื่อใหม่
        public string NewFirstName { get; set; }
        //นามสกุลใหม่
        public string NewLastName { get; set; }
        //วันที่เปลี่ยนชื่อ
        public string ChangeDate { get; set; }
        //สถานที่ทำการเปลี่ยนชื่อ
        public string ChangePlace { get; set; }
        //วันที่อัปเดท
        public string UpdateDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Employee Honor display columm list
    /// </summary>
    public class ListDataEmpHonor
    {
        public string no { get; set; }
        //ปีที่รับ
        public string Year { get; set; }
        //รูปใบประกาศ
        public string Image { get; set; }
        //วันที่อัปเดท
        public string UpdateDate { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }


}