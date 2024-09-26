using System;

namespace FingerprintPayment.ViewModels
{
    public class StudentGradeSplitUpDetailViewModel
    {
        public string sPlaneName { get; set; }
        public Nullable<int> courseGroup { get; set; }
        public Nullable<int> courseType { get; set; }
        public Nullable<int> StudentAmount { get; set; }
        public Nullable<int> Grade40 { get; set; }
        public Nullable<int> Grade35 { get; set; }
        public Nullable<int> Grade30 { get; set; }
        public Nullable<int> Grade25 { get; set; }
        public Nullable<int> Grade20 { get; set; }
        public Nullable<int> Grade15 { get; set; }
        public Nullable<int> Grade10 { get; set; }
        public Nullable<int> Grade00 { get; set; }
        public Nullable<int> Grade30Up { get; set; }
        public Nullable<decimal> Grade30UpPercent { get; set; }
        public string courseCode { get; set; }

        public int nTSubLevel { get; set; }
    }
}