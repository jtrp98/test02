using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using FingerprintPayment.Models;
using JabjaiMainClass;

namespace FingerprintPayment.Controllers.MacAddress
{
    public class AddmobileController : ApiController
    {
        string sAverment = "";
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var session = HttpContext.Current.Session;
            if (session["sAverment"] != null) sAverment = session["sAverment"] + "";
            List<Models.MacAddress> _MacAddress = new List<Models.MacAddress>();
            if (string.IsNullOrEmpty(sAverment))
            {
                CheckNumber(RandomNumber());
                session["sTime"] = DateTime.Now;
            }
            else
            {
                if (CheckNumber(sAverment))
                {
                    CheckNumber(RandomNumber());
                    session["sTime"] = DateTime.Now;
                }
            }

            DateTime _dateStart = DateTime.Parse(HttpContext.Current.Session["sTime"] + "");
            int _time = int.Parse((_dateStart.AddMinutes(5) - DateTime.Now).TotalSeconds.ToString().Split('.')[0]);
            string sTime = string.Format("{0:00}:{1:00}", _time / 60, (_time % 60) == 60 ? 0 : (_time % 60), _time);

            sAverment = string.Format("{0:000000}", session["sAverment"] + "");
            _MacAddress.Add(new Models.MacAddress
            {
                sAverment = sAverment,
                sTime = sTime
            });

            return Ok(_MacAddress);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "";
        }

        private bool CheckNumber(string sAverment)
        {
            var session = HttpContext.Current.Session;
            string sEntities = session["sEntities"] + "";
            DataTable _dt = fcommon.Get_Data(fcommon.connMaster, @"SELECT * FROM TAverment where sAverment = '" + sAverment + "' AND DATEADD(MINUTE,-5,GETDATE()) < dAdd AND sMac IS NULL ");

            if (_dt.Rows.Count == 0)
            {
                int nAvermentID = 1;
                if (fcommon.Get_Data(fcommon.connMaster, "SELECT * FROM TAverment").Rows.Count > 0) nAvermentID = int.Parse(fcommon.Get_Value(fcommon.connMaster, "SELECT top 1 nAvermentID FROM TAverment order by nAvermentID desc")) + 1;
                string SQL = "INSERT INTO [TAverment] ([nAvermentID],[sAverment],[nCompany],[dAdd],cType)VALUES (" + nAvermentID + ",'" + sAverment + "','" + sEntities + "',GETDATE(),1)";
                fcommon.ExecuteNonQuery(fcommon.connMaster, SQL);
                session["nAvermentID"] = nAvermentID;
            }

            return _dt.Rows.Count == 0;
        }

        private string RandomNumber()
        {
            var session = HttpContext.Current.Session;
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            numIterations = rand.Next(100000, 999999);
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