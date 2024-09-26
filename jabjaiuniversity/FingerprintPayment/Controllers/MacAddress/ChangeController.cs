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
using MasterEntity;

namespace FingerprintPayment.Controllers.MacAddress
{
    public class ChangeController : ApiController
    {
        string sAverment = "";
        // GET api/<controller>
        public IHttpActionResult Get()
        {

            return Ok("");
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "";
        }

        public IHttpActionResult Get(int userid, string type)
        {
            var session = HttpContext.Current.Session;
            string sEntities = session["sEntities"] + "";
            JabJaiMasterEntities dbMaster = Connection.MasterEntities();
            int nCompany = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault().nCompany;
            string sPassword = RandomNumber();
            if (type == "0")
            {
                foreach (var data in dbMaster.TUsers.Where(w => w.cType == "0" && w.nSystemID == userid && w.nCompany == nCompany))
                {
                    if (data.sFinger == null && data.sFinger == null && data.sFinger == null)
                    {
                        sPassword = data.sPassword;
                    }
                    else
                    {
                        data.sFinger = null;
                        data.sFinger2 = null;
                        data.sFinger3 = null;
                        data.sPassword = sPassword;
                    }
                }
            }
            else
            {
                foreach (var data in dbMaster.TUsers.Where(w => w.cType != "0" && w.nSystemID == userid && w.nCompany == nCompany))
                {
                    if (data.sFinger == null && data.sFinger == null && data.sFinger == null)
                    {
                        sPassword = data.sPassword;
                    }
                    else
                    {
                        data.sFinger = null;
                        data.sFinger2 = null;
                        data.sFinger3 = null;
                        data.sPassword = sPassword;
                    }
                }
            }
            dbMaster.SaveChanges();
            return Ok(sPassword);
        }

        [Route("~/api/getpassword")]
        public string getpassword(int userid, string type)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities())
            {
                var session = HttpContext.Current.Session;
                string sEntities = session["sEntities"] + "";
                var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var tUser = dbmaster.TUsers.Where(w => w.nSystemID == userid && w.cType == type && w.nCompany == tCompany.nCompany).FirstOrDefault();
                return tUser.sPassword;
            }
        }
        private string RandomNumber()
        {
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            Random rand = new Random((int)DateTime.Now.Ticks);
            int numIterations = 0;
            do
            {
                numIterations = rand.Next(100000, 999999);

            } while (_dbMaster.TUsers.Where(w => w.sPassword == numIterations.ToString() && string.IsNullOrEmpty(w.sFinger)).ToList().Count > 0);

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