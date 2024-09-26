using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ViewModels
{
    public class LookUpHeight
    {
        public string Sex { get; set; }
        public Measurement Short { get; set; }
        public Measurement MediumShort { get; set; }
        public Measurement Normal { get; set; }
        public Measurement MediumTall { get; set; }
        public Measurement Tall { get; set; }

        public LookUpHeight(string sex, Measurement growthShort, Measurement growthMediumShort, Measurement growthNormal, Measurement growthMediumTall, Measurement growthTall)
        {
            Sex = sex;
            Short = growthShort;
            MediumShort = growthMediumShort;
            Normal = growthNormal;
            MediumTall = growthMediumTall;
            Tall = growthTall;
        }
    }
}