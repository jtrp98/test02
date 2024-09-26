using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Models.Report
{
    public class reportsmonth
    {
    }

    public class _30DayUserTimeScan
    {
        public string sIdentification { set; get; }
        public string name { set; get; }
        public List<TimeScan> time { set; get; }
        public int LogScanStatus0 { get; set; }
        public int LogScanStatus1 { get; set; }
        public int LogScanStatus3 { get; set; }
        public int LogScanStatus9 { get; set; }
        public int LogScanPercent0 { get; set; }
        public int LogScanPercent1 { get; set; }
        public int LogScanPercent3 { get; set; }
        public int LogScanPercent9 { get; set; }
    }

    public class TimeScan
    {
        public string LogTime { get; set; }
        public int LogDay { get; set; }
        public string LogDate { get; set; }
        public string scanstatus { get; set; }
    }
}