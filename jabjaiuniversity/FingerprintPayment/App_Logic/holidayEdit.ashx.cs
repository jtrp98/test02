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
    /// Summary description for holidayEdit
    /// </summary>
    public class holidayEdit : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string id = fcommon.ReplaceInjection(context.Request["id"]);

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var data = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == id).FirstOrDefault();

                holidayedit edit = new holidayedit();
                List<holidayedit> editList = new List<holidayedit>();
                //string dayEnd = "";
                //string monthEnd = "";
                //string dayStart = "";
                //string monthStart = "";
                edit.color = data.sColor;
                //if (data.dHolidayEnd.Value.Day < 10)
                //    dayEnd = "0" + data.dHolidayEnd.Value.Day;
                //else dayEnd = data.dHolidayEnd.Value.Day.ToString();

                //if (data.dHolidayEnd.Value.Month < 10)
                //    monthEnd = "0" + data.dHolidayEnd.Value.Month;
                //else monthEnd = data.dHolidayEnd.Value.Month.ToString();

                //if (data.dHolidayStart.Value.Day < 10)
                //    dayStart = "0" + data.dHolidayStart.Value.Day;
                //else dayStart = data.dHolidayStart.Value.Day.ToString();

                //if (data.dHolidayStart.Value.Month < 10)
                //    monthStart = "0" + data.dHolidayStart.Value.Month;
                //else monthStart = data.dHolidayStart.Value.Month.ToString();

                //edit.End = dayEnd + "/" + monthEnd + "/" + data.dHolidayEnd.Value.Year;
                edit.holidayGroup = data.sGroupTarget;
                edit.TimeType = data.TimeType; //(data.TimeType == 2 ? "ประจำปี" : "ประจำวัน");
                edit.name = data.sHoliday;
                //edit.Start = dayStart + "/" + monthStart + "/" + data.dHolidayStart.Value.Year;
                edit.Start = data.dHolidayStart?.ToString("dd/MM/yyyy", new CultureInfo("th"));
                edit.End = data.dHolidayEnd?.ToString("dd/MM/yyyy", new CultureInfo("th"));
                if (data.sHolidayType == "1" || data.sHolidayType == "0")
                    edit.Type = "0";
                if (data.sHolidayType == "3" || data.sHolidayType == "4")
                    edit.Type = "1";
                edit.whoSee = data.sWhoSeeThis;
                edit.actvice = data.cStatusActive ?? false ? 1 : 0;
                editList.Add(edit);


                rss = new JArray(from a in editList
                                 select new JObject(
                                new JProperty("color", a.color),
                               new JProperty("End", a.End),
                              new JProperty("group", a.holidayGroup),
                              new JProperty("name", a.name),
                              new JProperty("TimeType", a.TimeType),
                              new JProperty("Start", a.Start),
                              new JProperty("Type", a.Type),
                              new JProperty("whoSee", a.whoSee),
                              new JProperty("actvice", a.actvice),
                                               new JProperty("LevelId", (from a1 in _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID)
                                                                         where a1.nHoliday == id && a1.Deleted == null
                                                                         select a1.nTSubLevel))

                             ));
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

        protected class holidayedit
        {
            public byte? TimeType { get; set; }

            public string Start { get; set; }
            public string End { get; set; }
            public string Type { get; set; }
            public string name { get; set; }
            public string holidayGroup { get; set; }
            public string whoSee { get; set; }
            public int? color { get; set; }
            public int? actvice { get; set; }
        }


    }


}