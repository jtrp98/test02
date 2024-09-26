using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class ReportsData_03Models
    {
        public string levelname { get; set; }
        public string level2name { get; set; }
        public List<StudentData> studentDatas { get; set; }
        public class StudentData
        {
            public string Student_Name { get; set; }
            public string Student_Id { get; set; }
            public int Id { get; set; }
            public List<ScanData> scanDatas { get; set; }
            public class ScanData
            {
                public string Scan_Date { get; set; }
                public string Scan_Status { get; set; }
                internal DateTime LogDay { get; set; }
                internal TimeSpan? TimeIn { get; set; }
                internal TimeSpan TimeLate { get; set; }
                public int CountTimeLate
                {
                    get
                    {
                        if (Scan_Status.Trim() == "1" && TimeIn.HasValue)
                        {
                            DateTime _d1 = LogDay.Add(TimeLate);
                            DateTime _d2 = LogDay.Add(TimeIn.Value);
                            if (_d1 <= _d2)
                            {
                                int TotalSeconds = (int)(_d2 - _d1).TotalSeconds;
                                return TotalSeconds;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
            }
        }
    }
}