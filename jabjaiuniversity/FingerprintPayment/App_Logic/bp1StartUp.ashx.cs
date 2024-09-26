using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for bp1StartUp
    /// </summary>
    public class bp1StartUp : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            dynamic rss = new JObject();
            string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
            string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
            switch (sMode)
            {
                case "bp1":
                    var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    int? idlvn = Int32.Parse(idlv);
                    
                    TPlane course = new TPlane();
                    List<TPlane> courseList = new List<TPlane>();
                     foreach (var data in _db.TPlanes.Where(w => w.cDel == null))
                    {
                        course = new TPlane();
                        //course.courseStatus = data.courseStatus;
                        course.sPlaneID = data.sPlaneID;
                        courseList.Add(course);
                    }
                    
                      rss = new JArray(from a in courseList
                                       select new JObject(
                             //new JProperty("courseStatus", a.courseStatus),
                            new JProperty("sPlaneID", a.sPlaneID)
                          ));

                    break;



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
    }

   
}