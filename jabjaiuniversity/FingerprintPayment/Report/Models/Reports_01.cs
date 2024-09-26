using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class Reports_01
    {
        public List<HeaderReports> headerReports { get; set; }
        public List<ReportsData> reportsDatas { get; set; }

        public class HeaderReports
        {
            public string Month_Name { get; set; }
            public int Month_Id { get; set; }
            public List<Weeks> weeks { get; set; }
        }

        public class Weeks
        {
            public string Weeks_Name { get; set; }
            public int Weeks_Id { get; set; }
            public List<Days> days { get; set; }
        }

        public class Days
        {
            public string Days_Name { get; set; }
            public int Days_Id { get; set; }
            public string Days_Status { get; set; }
            public string HoliDay_Name { get; internal set; }
            public bool HoliDay_Status { get; internal set; }
            public string Days_string { get; set; }
        }

        public class ReportsData
        {
            public int RowsIndex { get; set; }
            public int User_Id { get; set; }
            public string Student_Id { get; set; }
            public string Student_Name { get; set; }
            public int? Student_Number { get; set; }
            public List<ScanData> scanDatas { get; set; }
            public int Sum_Status_0 { get; set; }
            public int Sum_Status_1 { get; set; }
            public int Sum_Status_2 { get; set; }
            public int Sum_Status_3 { get; set; }
            public int Sum_Status_4 { get; set; }
            public int Sum_Status_5 { get; set; }
            public int Sum_Status_6 { get; set; }
            public string Student_Status { get; set; }
            public int DayStart { get; set; }
            public int DayEnd { get; set; }
            public string NoteIn { get; set; }
            public string NoteOut { get; set; }
            public DateTime? moveInDate { get; set; }
            public DateTime? MoveOutDate { get; set; }
            public int Student_Status2 { get; internal set; }
        }

        public class ScanData
        {
            public string Scan_Status { get; set; }
            public string Scan_Time { get; set; }
            public string Scan_Date { get; set; }
            public string Days_Status { get; set; }
            public int DayOfYear { get; set; }
            public int Year { get; set; }
            public int Scan_Status2 { get; internal set; }
        }
    }
}