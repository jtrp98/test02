using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Transcript.CsCode
{
    public class EntityQuery
    {
    }

    /// <summary>
    /// Dropdown entity query
    /// </summary>
    public class EntityDropdown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    /// <summary>
    /// Transcript entity query
    /// </summary>
    public class EntityTranscriptList
    {
        public int sID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public int Flag { get; set; }
        public int Range { get; set; }
        public string JsonData { get; set; }
        public int Level { get; set; }
    }

    public class EntityJsonQuery
    {
        public int sID { get; set; }
        public List<JsonData> jsonDatas { get; set; }
    }

    public class JsonData
    {
        public int yearID { get; set; }
        public int? year { get; set; }
        public string termID { get; set; }
        public string term { get; set; }
        public string levelID { get; set; }
        
    }

}