using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Message.CsCode
{
    public class EntityQuery
    {

    }

    /// <summary>
    /// Group entity query
    /// </summary>
    public class EntityGroupList
    {
        public int SMSGroupID { get; set; }
        public string SMSGroupName { get; set; }
        public string SMSGroupNameEn { get; set; }
        public int Status { get; set; }
    }

    /// <summary>
    /// News entity query
    /// </summary>
    public class EntityNewsList
    {
        public int SmsID { get; set; }
        public DateTime? SendDate { get; set; }
        public string Recorder { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Receiver { get; set; }
        public string Duration { get; set; }
        public string News { get; set; }
    }

    /// <summary>
    /// Dropdown entity query
    /// </summary>
    public class EntityDropdown
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}