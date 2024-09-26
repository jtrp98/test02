using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    public class EntityQuery { }

    /// <summary>
    /// Learning Center entity query
    /// </summary>
    public class EntityLearningCenter
    {
        public int ID { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Admin { get; set; }
    }

    /// <summary>
    /// Visit House entity query
    /// </summary>
    public class EntityVisitHouse
    {
        public int? Year { get; set; }
        public int? vID { get; set; }
        public int sID { get; set; }
        public int Term { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentCode { get; set; }
        public DateTime? StampDate { get; set; }
        public string StampTime { get; set; }
    }

    /// <summary>
    /// Electronic Mail entity query 
    /// </summary>
    public class EntityElectronicMail
    {
        // รหัสจดหมาย
        public int MailID { get; set; }
        // รหัสหน่วยงาน
        public int AgencyID { get; set; }
        // หัวข้อ
        public string Title { get; set; }
        // เนื้อหา
        public string MailContent { get; set; }
        // วันที่สร้างเมล์
        public DateTime? MailDate { get; set; }
        public string Files { get; set; }
        public string Schools { get; set; }

    }

}