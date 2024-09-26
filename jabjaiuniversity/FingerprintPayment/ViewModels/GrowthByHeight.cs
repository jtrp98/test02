using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ViewModels
{
    public class GrowthByHeight
    {
        public int Age { get; set; }
        public int Month { get; set; }
        public LookUpHeight lookUpHeight { get; set; }
    }
}