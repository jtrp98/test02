using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using JabjaiMainClass;

namespace FingerprintPayment.Controllers.MacAddress
{
    public class settingcookiesController : ApiController
    {
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var session = HttpContext.Current.Session;
            HttpResponseMessage respMessage = new HttpResponseMessage();
            respMessage.Content = new ObjectContent<string[]>
               (new string[] { "value1", "value2" }, new JsonMediaTypeFormatter());
            string nAvermentID = "115998";// session["nAvermentID"] + "";
            string sToken = fcommon.Get_Value(fcommon.connMaster, @"SELECT sToken FROM TComputer INNER JOIN TAverment ON TComputer.sMac = TAverment.SMAC
            WHERE sAverment = '" + nAvermentID + "' oreder by nAvermentID desc");

            CookieHeaderValue cookie = new CookieHeaderValue("token", sToken);
            cookie.Expires = DateTimeOffset.Now.AddDays(1);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            respMessage.Headers.AddCookies(new CookieHeaderValue[] { cookie });
            return respMessage;
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
    }
}