using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for bp1CourseRegister
    /// </summary>
    public class bp1CourseRegister : IHttpHandler, IRequiresSessionState
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
            string id = fcommon.ReplaceInjection(context.Request["id"]);
            string status = fcommon.ReplaceInjection(context.Request["status"]);
            int nTermSubLevel2 = 0;
            switch (sMode)
            {
                case "bp1":
                    var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    int? idlvn = Int32.Parse(idlv);
                    coursedetail coursedetail = new coursedetail();
                    List<coursedetail> coursedetailList = new List<coursedetail>();

                    

                    var data1 = _db.TPlanes.Where(w => w.sPlaneID == id).FirstOrDefault();
                    if (status == "0")
                    {
                        data1.courseStatus = 0;
                    }
                    else
                    {
                        data1.courseStatus = 1;
                    }

                    _db.SaveChanges();

                    //  rss = new JArray(from a in coursedetailList
                    //                  select new JObject(
                    //         new JProperty("week1", a.week1),
                    //        new JProperty("week2", a.week2),                          
                    //       new JProperty("week20", a.week20)
                    //      ));

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

    class coursedetail
    {
        public string courseCode { get; set; }
        public string courseName { get; set; }
        public string courseType { get; set; }
        public string courseCredit { get; set; }
        public string courseTotalHour { get; set; }
    }
}