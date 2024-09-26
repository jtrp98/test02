using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Report.Models
{
    public class ReportsData_06Models
    {
        public string roomName { get; set; }
        public int? roomId { get; set; }
        public string teacherHead { get; set; }
        public string teacherAssistOne { get; set; }
        public string teacherAssistTwo { get; set; }
        public int dayCount { get; set; }
        public List<string> d { get; set; }
    }
}