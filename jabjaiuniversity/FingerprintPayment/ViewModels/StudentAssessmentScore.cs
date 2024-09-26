using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ViewModels
{
    public class StudentAssessmentScore
    {
        public int StudentAssessmentScoreId { get; set; }
        public int sID { get; set; }
        public Nullable<int> AssessmentId { get; set; }
        public Nullable<double> Score { get; set; }
        public string ScoreIdentifier { get; set; }
        public Nullable<int> SchoolId { get; set; }
        public Nullable<int> nGradeId { get; set; }
        public Nullable<int> nYear { get; set; }
        public string nTerm { get; set; }
        public Nullable<int> nTSubLevel { get; set; }
        public Nullable<int> nTermSubLevel2 { get; set; }
        public string sPlaneID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string NameIdentifier { get; set; }
    }
}