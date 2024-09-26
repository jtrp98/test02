using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Qusetion.CsCode
{
    public class ListData
    {
    }

    public class ListDataStudent
    {
        public string no { get; set; }
        public int No { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public int sID { get; set; }
        public string TermID { get; set; }
        public string ValueSDQ { get; set; }
        public string ValueEQ { get; set; }
    }

    /// <summary>
    /// Report EQ display columm list
    /// </summary>
    public class ListDataReportEQ
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        //ห้อง
        public string ClassName { get; set; }
        //ความดี
        public string EQ11T { get; set; }
        //ความเก่ง
        public string EQ21T { get; set; }
        //ความสุข
        public string EQ31T { get; set; }
        //ผลประเมิน
        public string EQSUMT { get; set; }

    }

    /// <summary>
    /// Report SDQ display columm list
    /// </summary>
    public class ListDataReportSDQ
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //ชื่อ
        public string Name { get; set; }
        //นามสกุล
        public string Lastname { get; set; }
        //ห้อง
        public string ClassName { get; set; }
        //อารมณ์
        public string SDQ1T { get; set; }
        //ความพฤติ
        public string SDQ2T { get; set; }
        //สมาธิ
        public string SDQ3T { get; set; }
        //ความสัมพันธ์กับเพื่อน
        public string SDQ4T { get; set; }
        //สังคม
        public string SDQ5T { get; set; }
        //ปัญหาโดยรวม
        public string SDQ14T { get; set; }
        //ผลประเมิน
        public string SDQRT { get; set; }

    }

}