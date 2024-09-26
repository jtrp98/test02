using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JabjaiEntity.DB;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.Controllers
{
    public class schoolCalendarController : ApiController
    {
        // GET api/<controller>
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<schoolCalendar> GetschoolCalendar([FromUri]int userid, string day)
        {

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();

            var quser = _dbMaster.TUsers.Where(w => w.sID == userid).FirstOrDefault();
            var qcompany = _dbMaster.TCompanies.Find(quser.nCompany);

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(qcompany.sEntities));
            var qstudent = _db.TUsers.Where(w => w.sID == quser.nSystemID).FirstOrDefault();

            List<schoolCalendar> eventList = new List<schoolCalendar>();
            schoolCalendar aa = new schoolCalendar();

            string stDate = day.Substring(3, 2);
            string stMonth = day.Substring(0, 2);
            string stYear = day.Substring(6, 4);
            string combineStart = stDate + "/" + stMonth + "/" + stYear;
            DateTime? dt1;
            if (DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);


            foreach (var holiday in _db.THolidays.Where(w => w.cDel != "1" && (w.dHolidayEnd == dt1 || w.dHolidayEnd > dt1) && (w.dHolidayStart < dt1 || w.dHolidayStart == dt1)))
            {
                aa = new schoolCalendar();

                aa.title = holiday.sHoliday;

                eventList.Add(aa);
            }

            return eventList;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public class schoolCalendar
        {

            public String title { get; set; }

        }

    }
}
