using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    public class TempScanLastVersion
    {
        public TempScanLastVersion() { }

        public string version { get; set; }
        public Setup setupx64 { get; set; }
        public Setup setupx86 { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public class Setup
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}