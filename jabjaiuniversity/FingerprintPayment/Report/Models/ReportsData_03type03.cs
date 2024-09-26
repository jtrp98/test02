using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class ReportsData_03type03
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
                public string timeIn { get; set; }
                public string Scan_Status { get; set; }
                public int Late { get; set; }// 0;
                public int Absence { get; set; }// 1;
                public int Absence_Half { get; set; } // 1;
                public int Errand { get; set; } // 2;
                public int Sick { get; set; } // 3;
                public int UncheckName { get; set; } // 4;

            }
            //public decimal behaviorScore { get; set; }
            public decimal behaviorScore
            {
                get
                {
                    return this.behavAutoTotal + this.behavManualTotal;
                }
            }
            internal decimal ScoreAlert { get; set; }
            public bool Alert
            {
                get
                {
                    decimal Score = this.behaviorScore;
                    return Score <= this.ScoreAlert;
                }
            }
            public decimal behavAuto { get; set; }
            public decimal behavManual { get; set; }
            public decimal behavAutoTotal { get; set; }
            public decimal behavManualTotal { get; set; }
            public int? StudentStatus { get; set; }
            //public class Behavior
            //{
            //    public int? behaviorScore { get; set; }

            //}
        }
    }
}