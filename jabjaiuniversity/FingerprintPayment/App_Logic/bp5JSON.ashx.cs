using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for bp5JSON
    /// </summary>
    public class bp5JSON : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    dynamic rss = new JObject();
                    string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
                    string id = fcommon.ReplaceInjection(context.Request["id"]);
                    string userterm = fcommon.ReplaceInjection(context.Request["term"]);
                    string year = fcommon.ReplaceInjection(context.Request["year"]);
                    string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
                    int nTermSubLevel2 = 0;
                    switch (sMode)
                    {


                        case "month":


                            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();



                            int? useryear = Int32.Parse(year);
                            int? idlv2n = Int32.Parse(idlv2);
                            string username = "";
                            int? ntermsublv2 = 0;
                            int nyear = 0;
                            string nterm = "";
                            int ntermtable = 0;


                            foreach (var ff in db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID && w.cDel == false))
                            {
                                nyear = ff.nYear;
                            }

                            DateTime? daystart = DateTime.Now;
                            DateTime? dayend = DateTime.Now;

                            foreach (var ee in db.TTerms.Where(w => w.sTerm == userterm && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
                            {
                                nterm = ee.nTerm;
                                daystart = ee.dStart;
                                dayend = ee.dEnd;
                            }




                            DateTime? day1 = daystart;
                            while ((int)day1.Value.DayOfWeek != 1)
                            {
                                if ((int)day1.Value.DayOfWeek != 1)
                                    day1 = day1.Value.AddDays(-1);
                            }

                            DateTime? week1 = day1.Value.AddDays(4);
                            DateTime? week2 = week1.Value.AddDays(7);
                            DateTime? week3 = week2.Value.AddDays(7);
                            DateTime? week4 = week3.Value.AddDays(7);
                            DateTime? week5 = week4.Value.AddDays(7);
                            DateTime? week6 = week5.Value.AddDays(7);
                            DateTime? week7 = week6.Value.AddDays(7);
                            DateTime? week8 = week7.Value.AddDays(7);
                            DateTime? week9 = week8.Value.AddDays(7);
                            DateTime? week10 = week9.Value.AddDays(7);
                            DateTime? week11 = week10.Value.AddDays(7);
                            DateTime? week12 = week11.Value.AddDays(7);
                            DateTime? week13 = week12.Value.AddDays(7);
                            DateTime? week14 = week13.Value.AddDays(7);
                            DateTime? week15 = week14.Value.AddDays(7);
                            DateTime? week16 = week15.Value.AddDays(7);
                            DateTime? week17 = week16.Value.AddDays(7);
                            DateTime? week18 = week17.Value.AddDays(7);
                            DateTime? week19 = week18.Value.AddDays(7);
                            DateTime? week20 = week19.Value.AddDays(7);
                            DateTime? monday1 = day1;
                            DateTime? monday2 = monday1.Value.AddDays(7);
                            DateTime? monday3 = monday2.Value.AddDays(7);
                            DateTime? monday4 = monday3.Value.AddDays(7);
                            DateTime? monday5 = monday4.Value.AddDays(7);
                            DateTime? monday6 = monday5.Value.AddDays(7);
                            DateTime? monday7 = monday6.Value.AddDays(7);
                            DateTime? monday8 = monday7.Value.AddDays(7);
                            DateTime? monday9 = monday8.Value.AddDays(7);
                            DateTime? monday10 = monday9.Value.AddDays(7);
                            DateTime? monday11 = monday10.Value.AddDays(7);
                            DateTime? monday12 = monday11.Value.AddDays(7);
                            DateTime? monday13 = monday12.Value.AddDays(7);
                            DateTime? monday14 = monday13.Value.AddDays(7);
                            DateTime? monday15 = monday14.Value.AddDays(7);
                            DateTime? monday16 = monday15.Value.AddDays(7);
                            DateTime? monday17 = monday16.Value.AddDays(7);
                            DateTime? monday18 = monday17.Value.AddDays(7);
                            DateTime? monday19 = monday18.Value.AddDays(7);
                            DateTime? monday20 = monday19.Value.AddDays(7);

                            List<bp5month> bp5monthList = new List<bp5month>();
                            bp5month bp5month = new bp5month();
                            bp5month.week1 = monthtxt(week1.Value.Month);
                            bp5month.week2 = monthtxt(week2.Value.Month);
                            bp5month.week3 = monthtxt(week3.Value.Month);
                            bp5month.week4 = monthtxt(week4.Value.Month);
                            bp5month.week5 = monthtxt(week5.Value.Month);
                            bp5month.week6 = monthtxt(week6.Value.Month);
                            bp5month.week7 = monthtxt(week7.Value.Month);
                            bp5month.week8 = monthtxt(week8.Value.Month);
                            bp5month.week9 = monthtxt(week9.Value.Month);
                            bp5month.week10 = monthtxt(week10.Value.Month);
                            bp5month.week11 = monthtxt(week11.Value.Month);
                            bp5month.week12 = monthtxt(week12.Value.Month);
                            bp5month.week13 = monthtxt(week13.Value.Month);
                            bp5month.week14 = monthtxt(week14.Value.Month);
                            bp5month.week15 = monthtxt(week15.Value.Month);
                            bp5month.week16 = monthtxt(week16.Value.Month);
                            bp5month.week17 = monthtxt(week17.Value.Month);
                            bp5month.week18 = monthtxt(week18.Value.Month);
                            bp5month.week19 = monthtxt(week19.Value.Month);
                            bp5month.week20 = monthtxt(week20.Value.Month);
                            bp5monthList.Add(bp5month);

                            rss = new JArray(from a in bp5monthList
                                             select new JObject(
                                    new JProperty("week1", a.week1),
                                    new JProperty("week2", a.week2),
                                    new JProperty("week3", a.week3),
                                    new JProperty("week4", a.week4),
                                    new JProperty("week5", a.week5),
                                    new JProperty("week6", a.week6),
                                    new JProperty("week7", a.week7),
                                    new JProperty("week8", a.week8),
                                    new JProperty("week9", a.week9),
                                    new JProperty("week10", a.week10),
                                    new JProperty("week11", a.week11),
                                    new JProperty("week12", a.week12),
                                    new JProperty("week13", a.week13),
                                    new JProperty("week14", a.week14),
                                    new JProperty("week15", a.week15),
                                    new JProperty("week16", a.week16),
                                    new JProperty("week17", a.week17),
                                    new JProperty("week18", a.week18),
                                    new JProperty("week19", a.week19),
                                  new JProperty("week20", a.week20)
                                ));

                            break;



                    }

                    context.Response.Expires = -1;
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";
                    //context.Response.ContentEncoding = Encoding.UTF8;
                    context.Response.Write(rss);
                    context.Response.End();
                }
            }
        }

        protected string monthtxt(int number)
        {
            string txt = "";
            if (number == 1)
                txt = "ม.ค.";
            else if (number == 2)
                txt = "ก.พ.";
            else if (number == 3)
                txt = "มี.ค.";
            else if (number == 4)
                txt = "เม.ย.";
            else if (number == 5)
                txt = "พ.ค.";
            else if (number == 6)
                txt = "มิ.ย.";
            else if (number == 7)
                txt = "ก.ค.";
            else if (number == 8)
                txt = "ส.ค.";
            else if (number == 9)
                txt = "ก.ย.";
            else if (number == 10)
                txt = "ต.ค.";
            else if (number == 11)
                txt = "พ.ย.";
            else if (number == 12)
                txt = "ธ.ค.";


            return txt;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

    class bp5month
    {
        public string week1 { get; set; }
        public string week2 { get; set; }
        public string week3 { get; set; }
        public string week4 { get; set; }
        public string week5 { get; set; }
        public string week6 { get; set; }
        public string week7 { get; set; }
        public string week8 { get; set; }
        public string week9 { get; set; }
        public string week10 { get; set; }
        public string week11 { get; set; }
        public string week12 { get; set; }
        public string week13 { get; set; }
        public string week14 { get; set; }
        public string week15 { get; set; }
        public string week16 { get; set; }
        public string week17 { get; set; }
        public string week18 { get; set; }
        public string week19 { get; set; }
        public string week20 { get; set; }
    }
}