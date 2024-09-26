using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ViewModels
{
    public class TeacherViewModel
    {
        public int SEmp { get; set; } // sEmp
        public string SName { get; set; } // sName (length: 256)
        public string SLastname { get; set; } // sLastname (length: 256)

        public string FullName => string.Format("{0} {1}", SName, SLastname);
        public bool IsTimeTableScheduled { get; set; }
    }
}