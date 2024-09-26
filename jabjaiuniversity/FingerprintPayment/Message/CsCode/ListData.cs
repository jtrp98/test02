using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Message.CsCode
{
    public class ListData
    {
    }

    /// <summary>
    /// Group display columm list
    /// </summary>
    public class ListDataGroup
    {
        public string no { get; set; }
        //ชื่อกลุ่มผู้รับ
        public string GroupName { get; set; }
        //ชื่อกลุ่มผู้รับ (อังกฤษ)
        public string GroupNameEn { get; set; }
        //สถานะการใช้งาน
        public string status { get; set; }
        public string @switch { get; set; }
        public string action { get; set; }
        public string gid { get; set; }
        public string istatus { get; set; }
    }

    /// <summary>
    /// News display columm list
    /// </summary>
    public class ListDataNews
    {
        public string no { get; set; }
        //วันที่/เวลาที่ส่ง
        public string SendDate { get; set; }
        //ชื่อผู้ส่ง
        public string Recorder { get; set; }
        //ประเภทการส่ง
        public string Type { get; set; }
        //ชื่อเรื่อง
        public string Title { get; set; }
        //ผู้รับ
        public string Receiver { get; set; }
        //ประเภทข้อความ
        public string Duration { get; set; }
        //รายละเอียด
        public string News { get; set; }
        public string action { get; set; }
        public string nid { get; set; }
    }
    
}