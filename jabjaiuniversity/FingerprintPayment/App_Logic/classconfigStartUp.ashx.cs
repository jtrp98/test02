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
    /// Summary description for classconfigStartUp
    /// </summary>
    public class classconfigStartUp : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                    string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                startUpTTerm tsublv2 = new startUpTTerm();
                List<startUpTTerm> tsublv2List = new List<startUpTTerm>();
                foreach (var data in _db.TTermSubLevel2.Where(w => w.nWorkingStatus == 1))
                {
                    tsublv2 = new startUpTTerm();
                    tsublv2.id = data.nTermSubLevel2;
                    tsublv2.nWorkingStatus = data.nWorkingStatus;
                    tsublv2.type = "TTerm";
                    tsublv2List.Add(tsublv2);
                }

                foreach (var data2 in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1))
                {
                    tsublv2 = new startUpTTerm();
                    tsublv2.id = data2.nTSubLevel;
                    tsublv2.nWorkingStatus = data2.nWorkingStatus;
                    tsublv2.type = "Sublv";
                    tsublv2List.Add(tsublv2);
                }

                rss = new JArray(from a in tsublv2List
                                 select new JObject(
                       new JProperty("id", a.id),
                       new JProperty("type", a.type),
                      new JProperty("nWorkingStatus", a.nWorkingStatus)

                    ));

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";
                //context.Response.ContentEncoding = Encoding.UTF8;
                context.Response.Write(rss);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class startUpTTerm
        {
            public int id { get; set; }
            public int? nWorkingStatus { get; set; }
            public string type { get; set; }            
        }
        

    }


}