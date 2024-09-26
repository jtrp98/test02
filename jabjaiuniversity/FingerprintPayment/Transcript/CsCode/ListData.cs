using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Transcript.CsCode
{
    public class ListData { }

    /// <summary>
    /// Transcript display columm list
    /// </summary>
    public class ListDataTranscript
    {
        public string no { get; set; }
        //รหัสนักเรียน
        public string Code { get; set; }
        //คำนำหน้า
        public string Title { get; set; }
        //ชื่อ
        public string Name { get; set; }
        public string action { get; set; }
        public string sid { get; set; }
        public string flag { get; set; }
        public string range { get; set; }
        public string jsonData { get; set; }
        public string jsonData2 { get; set; }
        public string level { get; set; }
    }

}