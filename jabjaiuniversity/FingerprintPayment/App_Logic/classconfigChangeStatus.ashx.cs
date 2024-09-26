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
    /// Summary description for classconfigChangeStatus
    /// </summary>
    public class classconfigChangeStatus : IHttpHandler, IRequiresSessionState
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
                string id = fcommon.ReplaceInjection(context.Request["id"]);
                string status = fcommon.ReplaceInjection(context.Request["status"]);
                string type = fcommon.ReplaceInjection(context.Request["type"]);

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                int? idn = Int32.Parse(id);

                if (type == "tterm")
                {
                    var data1 = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idn).FirstOrDefault();
                    if (data1 != null)
                    {
                        if (status == "0")
                        {
                            data1.nWorkingStatus = 0;
                        }
                        else
                        {
                            data1.nWorkingStatus = 1;
                        }

                    }
                }
                else
                {
                    var data2 = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == idn).FirstOrDefault();
                    if (data2 != null)
                    {
                        if (status == "0")
                        {
                            data2.nWorkingStatus = 0;
                        }
                        else
                        {
                            data2.nWorkingStatus = 1;
                        }

                    }
                }


                _db.SaveChanges();

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
    }

    
}