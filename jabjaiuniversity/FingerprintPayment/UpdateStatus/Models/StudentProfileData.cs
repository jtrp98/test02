using System;
using System.Globalization;

namespace FingerprintPayment.UpdateStatus.Models
{
    public class StudentProfileData
    {
        public string Identification { get; set; }
        public string student_Code { get; set; }
        public string student_Name { get; set; }
        public string father_Name { get; set; }
        public string mother_Name { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public LogTime logTime { get; set; }
        public string birthDay { get { return dBirth.HasValue ? dBirth.Value.ToString("dd/MM/yyyy", new CultureInfo("th-th")) : ""; } }
        internal DateTime? dBirth { get; set; }
        public string Picture { get; set; }
        public int SchoolID { get; set; }
        public string Level { get; set; }
        public class LogTime
        {
            public int Status_0 { get; set; }
            public int Status_1 { get; set; }
            public int Status_2 { get; set; }
            public int Status_3 { get; set; }
            public int Status_4 { get; set; }
            public int Status_5 { get; set; }
            public int Status_6 { get; set; }
        }
    }
}