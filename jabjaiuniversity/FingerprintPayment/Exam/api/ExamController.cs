using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FingerprintPayment.Exam.api
{
    [RoutePrefix("api/exam")]
    public class ExamController : ApiController
    {
        protected static JWTToken.userData UserData { get; set; }// { get { return userData; } }
        protected override void Initialize(HttpControllerContext requestContext)
        {
            base.Initialize(requestContext);

            JWTToken token = new JWTToken();
            UserData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                UserData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
            }
        }

        [Route("GetTermByYear")]
        [HttpGet]
        public IHttpActionResult GetTermByYear(int year)
        {
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TTerms.Where(o => o.SchoolID == UserData.CompanyID && o.cDel != "1" && o.nYear == year)
                   .Select(o => new
                   {
                       text = "เทอม " + o.sTerm,
                       value = o.nTerm
                   })
                  .ToList();

                return Json(data);
            }
        }

        [Route("GetSubjectByCoruseType")]
        [HttpGet]
        public IHttpActionResult GetSubjectByCoruseType(int type)
        {
            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var data = db.TPlanes.Where(o => o.SchoolID == UserData.CompanyID && o.cDel != "1" && o.courseType == type)
                   .Select(o => new
                   {
                       text = o.sPlaneName,
                       value = o.sPlaneID
                   })
                  .ToList();

                return Json(data);
            }
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}