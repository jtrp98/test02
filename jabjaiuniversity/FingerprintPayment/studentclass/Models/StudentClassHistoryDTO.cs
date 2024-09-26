using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.studentclass.Models
{
    public class StudentClassHistoryDTO
    {
        public int? ClassroomId { get; set; }
        public string TermId { get; set; }
        public List<int> StudentId { get; set; }
    }
}