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
    public class schoolCalendarListController : ApiController
    {
        [AcceptVerbs("GET", "POST")]
        public IEnumerable<schoolCalendar> GetschoolListCalendar([FromUri]int userid, string day)
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
            string combineStart = "01" + "/" + stMonth + "/" + stYear;
            DateTime? dt1;
            if (DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt1 = DateTime.ParseExact(combineStart, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

            int m = Int32.Parse(stMonth);
            int y = Int32.Parse(stYear);
            string d = DateTime.DaysInMonth(y, m).ToString();

            DateTime? dt2;
            string combineStart2 = d + "/" + stMonth + "/" + stYear;
            if (DateTime.ParseExact(combineStart2, "dd/MM/yyyy", new CultureInfo("en-us")).Year <= DateTime.Today.Year)
                dt2 = DateTime.ParseExact(combineStart2, "dd/MM/yyyy", new CultureInfo("en-us"));
            else
                dt2 = DateTime.ParseExact(combineStart2, "dd/MM/yyyy", new CultureInfo("en-us")).AddYears(-543);

            DateTime? check = dt1;

            do
            {
                foreach (var holiday in _db.THolidays.Where(w => w.cDel != "1" && (w.dHolidayStart == check || (w.dHolidayStart< check && w.dHolidayEnd> check) || w.dHolidayEnd== check)))
                {
                    aa = new schoolCalendar();

                    aa.date = check;
                    eventList.Add(aa);
                    if (eventList.Count > 1)
                    {
                        if (eventList[eventList.Count - 2].date == eventList[eventList.Count - 1].date)
                        {
                            eventList.RemoveAt(eventList.Count - 1);
                        }
                    }
                    
                    

                }
                check = check.Value.AddDays(1);
            } while (check != dt2);

            List<schoolCalendar> unique = eventList.Distinct().ToList();
            return unique;
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

            public DateTime? date { get; set; }

        }

    }
}
