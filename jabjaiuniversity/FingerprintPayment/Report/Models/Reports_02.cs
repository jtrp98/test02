using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class Reports_02
    {
        public string Student_Name { get; set; }
        public string Student_Id { get; set; }
        public string ScanIn_Time { get; set; }
        public string ScanOut_Time { get; set; }
        public string ScanIn_Status { get; set; }
        public string ScanOut_Status { get; set; }
        public string TeacherName { get; set; }
        public int nStudentStatus { get; set; }
        public string Temperature { get; internal set; }
        public string FaceScanUrl { get; internal set; }
        public string DeviceType { get; internal set; }
    }
}