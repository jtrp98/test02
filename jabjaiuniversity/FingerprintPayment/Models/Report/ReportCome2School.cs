using JabjaiMainClass;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Models.Report
{
    public class ReportCome2School
    {

    }

    public class Report01User
    {
        public string day { get; set; }
        public int studentnumber { get; set; }
        public int status_0 { get; set; }
        public int status_1 { get; set; }
        public int status_2 { get; set; }
    }

    public class Report02User
    {
        public string day { get; set; }
        public string levelname { get; set; }
        public int levelid { get; set; }
        public string level2name { get; set; }
        public int level2id { get; set; }
        public int studentnumber { get; set; }
        public int status_0 { get; set; }
        public int status_1 { get; set; }
        public int status_2 { get; set; }
    }

    public class Report03User
    {
        public string day { get; set; }
        public string levelname { get; set; }
        public string level2name { get; set; }
        public int studentid { get; set; }
        public string studentname { get; set; }
        public string studentlastname { get; set; }
        public string timein { get; set; }
        public int statusin_0 { get; set; }
        public int statusin_1 { get; set; }
        public int statusin_2 { get; set; }
        public string timeout { get; set; }
        public int statusout_0 { get; set; }
        public int statusout_1 { get; set; }
        public int statusout_2 { get; set; }
        public int levelid { get; set; }
        public int level2id { get; set; }
    }

    public class Report01Employess
    {
        public string day { get; set; }
        public int employessnumber { get; set; }
        public int status_0 { get; set; }
        public int status_1 { get; set; }
        public int status_2 { get; set; }
    }

    public class Report02Employess
    {
        public string day { get; set; }
        public int employessid { get; set; }
        public string employessname { get; set; }
        public string employesstype { get; set; }
        public int statusIn_0 { get; set; }
        public int statusIn_1 { get; set; }
        public int statusIn_2 { get; set; }
        public string timeinscan { get; set; }
        public int statusOut_0 { get; set; }
        public int statusOut_1 { get; set; }
        public int statusOut_2 { get; set; }
        public string timeoutscan { get; set; }
    }
}