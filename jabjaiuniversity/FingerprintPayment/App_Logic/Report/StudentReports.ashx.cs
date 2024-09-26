using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for StudentReports
    /// </summary>
    public class StudentReports : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"sbactestEntities";//
            string level = context.Request["level"];
            string level2 = context.Request["level2"];
            string studentname = context.Request["studentname"];
            int level2Id, levelId;
            dynamic rss = new JObject();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var tstudent = dbschool.TUser.Where(w => w.cDel == null && userData.CompanyID == w.SchoolID).ToList();
                    var tuser = dbmaster.TUsers.Where(w => w.nCompany == qcompany.nCompany && w.cType == "0").ToList();
                    var tlevel2 = dbschool.TTermSubLevel2.Where(w=>w.SchoolID == userData.CompanyID).ToList();
                    var tlevel = dbschool.TSubLevels.Where(w=>w.SchoolID == userData.CompanyID).ToList();                    

                    var q1 = (from a in tstudent
                              join d in tuser on a.sID equals d.nSystemID
                              join b in tlevel2 on a.nTermSubLevel2 equals b.nTermSubLevel2
                              join c in tlevel on b.nTSubLevel equals c.nTSubLevel
                              
                              select new stdReport
                              {
                                  studentname = a.sName,
                                  studentlastname = a.sLastname,
                                  levelname = c.SubLevel,
                                  level2name = c.SubLevel.Trim() + "/" + b.nTSubLevel2,
                                  levelId = c.nTSubLevel,
                                  level2Id = b.nTermSubLevel2,
                                  studentId = a.sIdentification,
                                  password = d.sFinger != null ? "" : d.sPassword,
                                  phone = string.IsNullOrEmpty(a.sPhone) ? "" : a.sPhone,
                                  birth = a.dBirth.Value.ToString("dd/MM/yyyy"),
                                  money = string.Format("{0:#,#0}", a.nMoney),
                                  homeNumber = string.IsNullOrEmpty(a.sStudentHomeNumber) ? "" : a.sStudentHomeNumber,
                                  muu = string.IsNullOrEmpty(a.sStudentMuu) ? "" : a.sStudentMuu,
                                  soy = string.IsNullOrEmpty(a.sStudentSoy) ? "" : a.sStudentSoy,
                                  road = string.IsNullOrEmpty(a.sStudentRoad) ? "" : a.sStudentRoad,
                                  tumbon = string.IsNullOrEmpty(a.sStudentTumbon) ? "" : a.sStudentTumbon,
                                  aumpher = string.IsNullOrEmpty(a.sStudentAumpher) ? "" : a.sStudentAumpher,
                                  provin = string.IsNullOrEmpty(a.sStudentProvince) ? "" : a.sStudentProvince,
                                  post = string.IsNullOrEmpty(a.sStudentPost) ? "" : a.sStudentPost
                              }).ToList();

                    foreach(var a in q1)
                    {
                        int ntum;
                        int naum;
                        int npro;
                        
                        bool isNumeric = int.TryParse(a.tumbon, out ntum);
                        if (isNumeric == true)
                        {
                            var tumbon = dbmaster.districts.Where(w => w.DISTRICT_ID == ntum).FirstOrDefault();
                            if (tumbon != null)
                                a.tumbon = tumbon.DISTRICT_NAME;
                            else a.tumbon = "";
                        }
                        else a.tumbon = "";

                        bool isNumeric2 = int.TryParse(a.aumpher, out naum);
                        if (isNumeric2 == true)
                        {
                            var aumper = dbmaster.amphurs.Where(w => w.AMPHUR_ID == naum).FirstOrDefault();
                            if (aumper != null)
                                a.aumpher = aumper.AMPHUR_NAME;
                            else a.aumpher = "";
                        }
                        else a.aumpher = "";

                        bool isNumeric3 = int.TryParse(a.provin, out npro);
                        if (isNumeric3 == true)
                        {
                            var provin = dbmaster.provinces.Where(w => w.PROVINCE_ID == npro).FirstOrDefault();
                            if (provin != null)
                                a.provin = provin.PROVINCE_NAME;
                            else a.provin = "";
                        }
                        else a.provin = "";

                    }

                    if (!string.IsNullOrEmpty(studentname))
                    {
                        q1 = q1.Where(w => w.studentname.Contains(studentname)).ToList();
                    }
                    else if (!string.IsNullOrEmpty(level2))
                    {
                        level2Id = int.Parse(level2);
                        q1 = q1.Where(w => w.level2Id.Equals(level2Id)).ToList();
                    }
                    else if (!string.IsNullOrEmpty(level))
                    {
                        levelId = int.Parse(level);
                        q1 = q1.Where(w => w.levelId.Equals(levelId)).ToList();
                    }

                    var q2 = (from b in q1
                              group b by new { b.levelId, b.levelname } into bg
                              select new
                              {
                                  bg.Key.levelId,
                                  bg.Key.levelname
                              }).ToList();

                    var q3 = (from b in q1
                              group b by new { b.levelId, b.level2name, b.level2Id } into bg
                              select new
                              {
                                  bg.Key.level2Id,
                                  bg.Key.level2name,
                                  bg.Key.levelId
                              }).ToList();

                    rss = new JArray();
                    foreach (var glevel in q2)
                    {
                        dynamic jlevel = new JObject();
                        jlevel.levelname = glevel.levelname;
                        jlevel.level2 = new JArray();
                        foreach (var glevel2 in q3.Where(w => w.levelId == glevel.levelId))
                        {
                            dynamic jlevel2 = new JObject();
                            jlevel2.level2name = glevel2.level2name;
                            jlevel2.student = new JArray();
                            jlevel2.student = new JArray(from a in q1
                                                         where a.level2Id == glevel2.level2Id
                                                         select new JObject
                                                     {
                                                         new JProperty("studentname",a.studentname),
                                                         new JProperty("studentlastname",a.studentlastname),
                                                         new JProperty("studentId",a.studentId),
                                                         new JProperty("password",a.password),
                                                         new JProperty("phone",a.phone),
                                                         new JProperty("money",a.money),
                                                         new JProperty("birth",a.birth),
                                                         new JProperty("homeNumber",a.homeNumber),
                                                         new JProperty("muu",a.muu),
                                                         new JProperty("soy",a.soy),
                                                         new JProperty("tumbon",a.tumbon),
                                                         new JProperty("road",a.road),
                                                         new JProperty("aumpher",a.aumpher),
                                                         new JProperty("provin",a.provin),
                                                         new JProperty("post",a.post),
                                                     });
                            jlevel.level2.Add(jlevel2);
                        }
                        rss.Add(jlevel);
                    }
                }
            }

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class stdReport
        {
            public int levelId { get; set; }
            public int level2Id { get; set; }
            public string studentname { get; set; }
            public string studentlastname { get; set; }
            public string levelname { get; set; }
            public string level2name { get; set; }
            public string studentId { get; set; }
            public string password { get; set; }
            public string phone { get; set; }
            public string birth { get; set; }
            public string money { get; set; }
            public string homeNumber { get; set; }
            public string post { get; set; }
            public string provin { get; set; }
            public string aumpher { get; set; }
            public string tumbon { get; set; }
            public string road { get; set; }
            public string soy { get; set; }
            public string muu { get; set; }
            
        }
    }
}