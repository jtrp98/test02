using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ViewModels
{
    public class Lookup
    {
        public string Sex { get; set; }
        public Measurement Thin { get; set; }
        public Measurement Skinny { get; set; }
        public Measurement Shapely { get; set; }
        public Measurement Plump { get; set; }
        public Measurement Chubby { get; set; }
        public Measurement Fat { get; set; }
        public Lookup(string sex, Measurement thin, Measurement skinny, Measurement shapely, Measurement plump, Measurement chubby, Measurement fat)
        {
            Sex = sex;
            Thin = thin;
            Skinny = skinny;
            Shapely = shapely;
            Plump = plump;
            Chubby = chubby;
            Fat = fat;
        }
    }
}