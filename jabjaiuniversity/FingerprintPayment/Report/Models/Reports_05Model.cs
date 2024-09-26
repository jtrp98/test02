using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class Reports_05Model
    {
        public List<HeaderReports> headerReports { get; set; }
        public List<ReportsData> reportsDatas { get; set; }

        public class HeaderReports
        {
            public string Month_Name { get; set; }
            public int Month_Id { get; set; }
            public List<Weeks> weeks { get; set; }

            public class Weeks
            {
                public string Weeks_Name { get; set; }
                public int Weeks_Id { get; set; }
                public List<Days> days { get; set; }

                public class Days
                {
                    public string Days_Name { get; set; }
                    public int Days_Id { get; set; }
                    public string Days_Status { get; set; }
                }
            }

        }

        public class ReportsData
        {
            public int RowsIndex { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            public List<ScanData> scanDatas { get; set; }
            public float Sum_Status_0 { get; set; }
            public float Sum_Status_1 { get; set; }
            public float Sum_Status_2 { get; set; }
            public float Sum_Status_3 { get; set; }
            public float Sum_Status_4 { get; set; }
            public float Sum_Status_5 { get; set; }
            public float Sum_Status_6 { get; set; }

            public float Sum_Status_11 { get; set; }
            public float Sum_Status_10 { get; set; }
            public float Sum_Status_21 { get; set; }
            public float Sum_Status_22 { get; set; }
            public float Sum_Status_23 { get; set; }
            public float Sum_Status_24 { get; set; }
            public float Sum_Status_25 { get; set; }
            public float Sum_Status_26 { get; set; }

            public int? Time_Id { get; set; }
            public string Time_Late
            {
                get
                {
                    double TotalMilliseconds = this.TotalMilliseconds;
                    TimeSpan time = TimeSpan.FromMilliseconds(TotalMilliseconds);
                    return time.ToString(@"hh\:mm\:ss");
                }
            }

            public string Code { get; internal set; }
            public string EmpType { get; internal set; }
            public int? WorkStatus { get; internal set; }
            public DateTime? DayQuit { get; internal set; }
            internal double TotalMilliseconds { get; set; }

            public class ScanData
            {
                public string Scan_StatusIn { get; set; }
                public string Scan_TimeIn { get; set; }
                public string Scan_StatusOut { get; set; }
                public string Scan_TimeOut { get; set; }
                public string Scan_Date { get; set; }
                public string Days_Status { get; set; }
                public int DayOfYear { get; set; }
            }

        }

    }
}