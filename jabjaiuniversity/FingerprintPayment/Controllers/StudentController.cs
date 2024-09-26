using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/Stdeunt/List")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody] Search search)
        {

            if (search == null || string.IsNullOrEmpty(search.Token))
            {
                return Ok();
            }
            else
            {
                var root = JsonConvert.DeserializeObject<RootObject>(search.Token);
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(root.schoolId)))
                {

                    string SQL = string.Format(@"SELECT DISTINCT TOP 10 sName,sLastname,sStudentID,sID 
FROM TB_StudentViews 
WHERE SCHOOLID = {0}  
AND (sName + ' ' + sLastname LIKE '%{1}%' OR sStudentID LIKE '%{1}%' ) ", root.schoolId, search.wording);

                    if (search.RoomID.HasValue)
                    {
                        SQL += " AND nTermSubLevel2 = " + search.RoomID;
                    }
                    else if (search.ClassID.HasValue)
                    {
                        SQL += " AND nTSubLevel = " + search.ClassID;
                    }

                    if (!string.IsNullOrEmpty(search.TremID))
                    {
                        SQL += $" AND nTerm = '{search.TremID}' ";

                    }

                    var q = entities.Database.SqlQuery<IStudent>(SQL).ToList();

                    return Ok(q);

                }

            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        public class IStudent
        {
            public string sName { get; set; }
            public string sLastname { get; set; }
            public string sStudentID { get; set; }
            public string sID { get; set; }
        }

        public class Search
        {
            public string wording { get; set; }
            public string Token { get; set; }
            public int? ClassID { get; set; }
            public int? RoomID { get; set; }
            public string TremID { get; set; }
        }

        public class RootObject
        {
            public string userId { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public int schoolId { get; set; }
        }
    }
}