using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Qusetion.CsCode
{
    public class EntityQuery
    {
    }

    public class EntityDropdown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class EntityStudentList
    {
        public int sID { get; set; }
        public int No { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public string Status { get; set; }
        public string TermID { get; set; }
    }

    /// <summary>
    /// Report EQ entity query
    /// </summary>
    public class EntityReportEQ
    {
        public int SchoolID { get; set; }
        public int sID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public int? EQ11 { get; set; }
        public string EQ11T { get; set; }
        public int? EQ12 { get; set; }
        public string EQ12T { get; set; }
        public int? EQ13 { get; set; }
        public string EQ13T { get; set; }
        public int? EQ21 { get; set; }
        public string EQ21T { get; set; }
        public int? EQ22 { get; set; }
        public string EQ22T { get; set; }
        public int? EQ23 { get; set; }
        public string EQ23T { get; set; }
        public int? EQ31 { get; set; }
        public string EQ31T { get; set; }
        public int? EQ32 { get; set; }
        public string EQ32T { get; set; }
        public int? EQ33 { get; set; }
        public string EQ33T { get; set; }
        public int? EQSUM { get; set; }
        public string EQSUMT { get; set; }
    }

    /// <summary>
    /// Report SDQ entity query
    /// </summary>
    public class EntityReportSDQ
    {
        public int SchoolID { get; set; }
        public int sID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ClassName { get; set; }
        public int? SDQ1 { get; set; }
        public string SDQ1T { get; set; }
        public int? SDQ2 { get; set; }
        public string SDQ2T { get; set; }
        public int? SDQ3 { get; set; }
        public string SDQ3T { get; set; }
        public int? SDQ4 { get; set; }
        public string SDQ4T { get; set; }
        public int? SDQ5 { get; set; }
        public string SDQ5T { get; set; }
        public int? SDQ14 { get; set; }
        public string SDQ14T { get; set; }
        public string SDQRT { get; set; }
    }

}