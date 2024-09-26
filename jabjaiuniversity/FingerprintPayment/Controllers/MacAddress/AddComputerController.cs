using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using JabjaiMainClass;

namespace FingerprintPayment.Controllers.MacAddress
{
    public class AddComputerController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        string sAverment = "";
        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {

            var session = HttpContext.Current.Session;
            if (session["sAverment"] != null) sAverment = session["sAverment"] + "";
            List<Models.MacAddress> _MacAddress = new List<Models.MacAddress>();
            if (string.IsNullOrEmpty(sAverment))
            {
                CheckNumber(RandomNumber(), id);
                session["sTime"] = DateTime.Now;
            }
            else
            {
                if (CheckNumber(sAverment, id))
                {
                    CheckNumber(RandomNumber(), id);
                    session["sTime"] = DateTime.Now;
                }
            }

            DateTime _dateStart = DateTime.Parse(HttpContext.Current.Session["sTime"] + "");
            int _time = int.Parse((_dateStart.AddMinutes(5) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
            string sTime = string.Format("{0:00}:{1:00}", _time / 60, (_time % 60) == 60 ? 0 : (_time % 60), _time);

            sAverment = session["sAverment"] + "";
            _MacAddress.Add(new Models.MacAddress
            {
                sAverment = sAverment,
                sTime = sTime
            });

            return Ok(_MacAddress);
        }

        private bool CheckNumber(string sAverment, string smac)
        {
            var session = HttpContext.Current.Session;
            string sEntities = session["sEntities"] + "";
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TAverment where sAverment = '" + sAverment + "' AND DATEADD(MINUTE,-5,GETDATE()) < dAdd AND cStatus IS NULL ");

            if (_dt.Rows.Count == 0)
            {
                int nAvermentID = 1;
                if (fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TAverment").Rows.Count > 0) nAvermentID = int.Parse(fcommon.Get_Value(fcommon.connMaster, "SELECT top 1 nAvermentID FROM TAverment order by nAvermentID desc")) + 1;
                string SQL = "INSERT INTO [TAverment] ([nAvermentID],[sAverment],[nCompany],[dAdd],cType,sMac)VALUES (" + nAvermentID + ",'" + sAverment + "','" + sEntities + "',GETDATE(),2,'" + smac + "')";
                fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
                session["nAvermentID"] = nAvermentID;
            }
            else {
                 _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TComputer where sMac = '"+ smac +"' ");
                foreach (DataRow _dr in _dt.Rows)
                {
                    ////create a cookie
                    //HttpCookie myCookie = new HttpCookie("myCookie");

                    ////Add key-values in the cookie
                    //myCookie.Values.Add("sMac", "c4:6a:b7:20:00:52");

                    ////set cookie expiry date-time. Made it to last for next 12 hours.
                    //myCookie.Expires = DateTime.Now.AddDays(9999);

                    ////Most important, write the cookie to client.
                    //Response.Cookies.Add(myCookie);
                }

            }

            return _dt.Rows.Count == 0;
        }

        private string RandomNumber()
        {
            var session = HttpContext.Current.Session;
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(1000, 999999);
            session["sAverment"] = numIterations.ToString();

            return numIterations.ToString();
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
    }
}