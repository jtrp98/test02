using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using MasterEntity;
using JabjaiMainClass;
using Newtonsoft.Json;

namespace FingerprintPayment.App_Logic.Report
{
    /// <summary>
    /// Summary description for employessreports
    /// </summary>
    public class employessreports : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"sbactestEntities";//
            string type = context.Request["type"];
            string employessname = context.Request["employessname"];
            dynamic rss = null;

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);

                    var tuser = dbmaster.TUsers.Where(w => w.cType == "1" && w.nCompany == qcompany.nCompany).ToList();

                    var q1 = (from a in dbschool.TEmployees.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID)
                              from c in dbschool.TTimetypes.Where(w => w.SchoolID == userData.CompanyID && a.nTimeType == w.nTimeType).DefaultIfEmpty()
                              from d in dbschool.TEmployeeTypes.Where(o => o.SchoolID == userData.CompanyID && (o.nTypeId2 ?? o.nTypeId) + "" == a.cType).DefaultIfEmpty()

                              where a.cType.Contains(type) && (a.sName + " " + a.sLastname).Contains(employessname)

                              select new
                              {
                                  a.sEmp,
                                  a.nTimeType,
                                  cType = a.cType ?? "-",
                                  a.sName,
                                  a.sLastname,
                                  a.sPhone,
                                  type = d.Title,//a.cType.Trim() == "2" ? "อาจารย์" : "พนักงาน",
                                  c.sTimeType
                              })
                              .ToList();

                    rss = (from a in q1
                           from b in tuser.Where(w => a.sEmp == w.nSystemID.Value).DefaultIfEmpty()

                           orderby a.cType, a.sName

                           select new
                           {
                               employessname = a.sName + " " + a.sLastname,
                               phone = a.sPhone,
                               type = a.type,
                               sPassword = string.IsNullOrEmpty(b.sFinger) ? b.sPassword : "",
                               sTimeType = a.sTimeType
                           })
                               .GroupBy(o => new
                               {
                                   o.type
                               })
                               .Select(o => new
                               {
                                   type = o.Key.type,
                                   employess = o.Select(i => new
                                   {
                                       employessname = i.employessname,
                                       phone = i.phone,
                                       timetable = i.sTimeType ?? "",
                                       password = i.sPassword ?? "",
                                   })
                               }).ToList();


                    //foreach (var g in (from a in q1
                    //                   group a by a.type into gp
                    //                   select new { gp.Key }))
                    //{
                    //    dynamic emptype = new JObject();
                    //    emptype.type = new JObject();
                    //    emptype.type = g.Key;
                    //    emptype.employess = new JArray(from a in q1
                    //                                   where a.type == g.Key
                    //                                   select new JObject {
                    //                                new JProperty("employessname",a.employessname),
                    //                                new JProperty("phone",a.phone),
                    //                                new JProperty("timetable",a.sTimeType),
                    //                                new JProperty("password",a.sPassword)
                    //                            });
                    //    rss.Add(emptype);
                    //}

                    // rss = d1;
                }
            }

            //context.Response.Expires = -1;
            //context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(JsonConvert.SerializeObject(rss));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}