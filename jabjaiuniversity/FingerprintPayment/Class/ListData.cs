using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    public class ListData { }

    /// <summary>
    /// Learning Center display columm list
    /// </summary>
    public class ListDataLearningCenter
    {
        public string no { get; set; }
        //ประเภทแหล่งเรียนรู้
        public string Type { get; set; }
        //ฃื่อแหล่งเรียนรู้
        public string Name { get; set; }
        //รายละเอียด
        public string Detail { get; set; }
        //ผู้ดูแล
        public string Admin { get; set; }
        public string action { get; set; }
        public string id { get; set; }
    }

    /// <summary>
    /// Visit House display columm list
    /// </summary>
    public class ListDataVisitHouse
    {
        public string no { get; set; }
        //ชื่อ
        public string FirstName { get; set; }
        //นามสกุล
        public string LastName { get; set; }
        //รหัสนักเรียน
        public string StudentCode { get; set; }
        //บันทึกวัน
        public string StampDate { get; set; }
        //บันทึกเวลา
        public string StampTime { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string year { get; set; }
        public string term { get; set; }
        public string vid { get; set; }
    }

    /// <summary>
    /// Electronic Mail display columm list
    /// </summary>
    public class ListDataElectronicMail
    {
        public string no { get; set; }
        //หัวข้อ
        public string Title { get; set; }
        //เนื้อหาเมล์
        public string MailContent { get; set; }
        //วันที่สร้างหาเมล์
        public string MailDate { get; set; }
        public string mid { get; set; }
        public string aid { get; set; }
        public string files { get; set; }
        public string schools { get; set; }

    }

}