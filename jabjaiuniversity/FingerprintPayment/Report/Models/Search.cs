using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class Search
    {
        public int sort_type { get; set; }
        public string term_id { get; set; }
        public int? level2_id { get; set; }
        public int? level_id { get; set; }
        public Nullable<int> student_id { get; set; }
        public DateTime? dStart { get; set; }
        public DateTime? dEnd { get; set; }
        public int report_type { get; set; }
        public int? year_Id { get; set; }
        public int? tType { get; set; }
        public string logStatus { get; set; }

        public int? selectType { get; set; }
        public string level2str { get; set; }
    }

    public class SearchEmployees
    {
        public int sort_type { get; set; }
        public DateTime dStart { get; set; }
        public DateTime dEnd { get; set; }
        public int? emp_id { get; set; }
        public int? department_id { get; set; }
        public string user_type { get; set; }
        public int status { get; set; }
        public int formtype { get; set; }
        public TimeSpan? time1 { get; set; }
        public TimeSpan? time2 { get; set; }
    }
}