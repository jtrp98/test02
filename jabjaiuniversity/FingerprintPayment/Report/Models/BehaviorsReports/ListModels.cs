
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models.BehaviorsReports
{
    public class ListModels
    {
        public string studentName { get; set; }
        public string studentId { get; set; }
        public int? studentNumber { get; set; }
        public decimal? Score { get; set; }
        public int userId { get; set; }
        public string Status { get; set; }
        internal decimal ScoreAlert { get; set; }
        public bool Alert
        {
            get
            {
                decimal Score = this.Score ?? 0;
                return Score <= this.ScoreAlert;
            }
        }
    }
}