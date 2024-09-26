using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models.BehaviorsReports
{
    public class DetailModels
    {
        public string student_Code { get; set; }
        public string roomName { get; set; }

        public string student_name { get; set; }
        public string year { get; set; }
        public string term { get; set; }
        public List<Detail> Details { get; set; }

        public class Detail
        {
            public DateTime? dateTime { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Score { get; set; }
            public string residualScore { get; set; }
            public string teacherName { get; set; }
            public string Note { get; set; }
            public string Status { get; set; }
            public int ID { get; set; }
            public DateTime? cancleDate { get; internal set; }
            public string cancleBy { get; internal set; }
        }
    }
}