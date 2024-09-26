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
    /// Summary description for holidayConfig
    /// </summary>
    public class holidayConfig : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using(JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string start = fcommon.ReplaceInjection(context.Request["start"]);
                string end = fcommon.ReplaceInjection(context.Request["end"]);
                string type = fcommon.ReplaceInjection(context.Request["type"]);
                string id = fcommon.ReplaceInjection(context.Request["id"]);
                string color = fcommon.ReplaceInjection(context.Request["color"]);
                string who = fcommon.ReplaceInjection(context.Request["who"]);
                string name = fcommon.ReplaceInjection(context.Request["name"]);
                string group = fcommon.ReplaceInjection(context.Request["group"]);


                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                DateTime endDate = new DateTime();
                DateTime startDate = new DateTime();


                if (DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    endDate = DateTime.ParseExact(end, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                if (DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                    startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us"));
                else
                    startDate = DateTime.ParseExact(start, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

                int counter = 0;
                foreach (var a in _db.THolidays)
                {
                    counter = counter + 1;
                }

                string type2 = "";
                if (type == "0")
                    type2 = "0";
                else type2 = "3";

                if (group != "1")
                {
                    THoliday holiday = new THoliday();
                    holiday.dHolidayEnd = endDate;
                    holiday.dHolidayStart = startDate;
                    holiday.nHoliday = counter.ToString();
                    holiday.sHoliday = name;
                    holiday.sHolidayType = type2;
                    holiday.sWhoSeeThis = who;
                    holiday.sGroupTarget = group;
                    holiday.sColor = Int32.Parse(color);
                    _db.THolidays.Add(holiday);
                }
                else
                {
                    var check = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.dHolidayEnd == endDate
                    && w.dHolidayStart == startDate && w.sHoliday == name &&
                    w.sHolidayType == type2 && w.sWhoSeeThis == who).FirstOrDefault();
                    if (check == null)
                    {
                        THoliday holiday = new THoliday();
                        holiday.dHolidayEnd = endDate;
                        holiday.dHolidayStart = startDate;
                        holiday.nHoliday = counter.ToString();
                        holiday.sHoliday = name;
                        holiday.sHolidayType = type2;
                        holiday.sWhoSeeThis = who;
                        holiday.sColor = Int32.Parse(color);
                        holiday.sGroupTarget = group;
                        holiday.SchoolID = userData.CompanyID;
                        _db.THolidays.Add(holiday);

                        //int count = 1;
                        //foreach (var a in _db.THolidaySomes)
                        //{
                        //    count = count + 1;
                        //}

                        THolidaySome some = new THolidaySome();
                        //some.nHolidaySomeID = count.ToString();
                        some.nHoliday = counter.ToString();
                        some.nTSubLevel = Int32.Parse(id);
                        some.SchoolID = userData.CompanyID;
                        _db.THolidaySomes.Add(some);
                    }
                    else
                    {
                        //int count = 1;
                        //foreach (var a in _db.THolidaySomes)
                        //{
                        //    count = count + 1;
                        //}

                        THolidaySome some = new THolidaySome();
                        //some.nHolidaySomeID = count.ToString();
                        some.nHoliday = check.nHoliday;
                        some.nTSubLevel = Int32.Parse(id);
                        some.SchoolID = userData.CompanyID;
                        _db.THolidaySomes.Add(some);
                    }


                }


                _db.SaveChanges();

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }


}