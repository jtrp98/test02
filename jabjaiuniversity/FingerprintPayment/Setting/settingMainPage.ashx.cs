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

namespace FingerprintPayment.Setting
{
    /// <summary>
    /// Summary description for settingMainPage
    /// </summary>
    public class settingMainPage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
                switch (sMode)
                {
                    case "1":
                        nCompany.settingTimePeriod = 1;
                        _dbMaster.SaveChanges();
                        break;

                    case "2":
                        nCompany.settingTimePeriod = 0;
                        _dbMaster.SaveChanges();
                        break;

                    case "3":
                        nCompany.settingPlanTeacher = 1;
                        _dbMaster.SaveChanges();
                        break;

                    case "4":
                        nCompany.settingPlanTeacher = 0;
                        _dbMaster.SaveChanges();
                        break;

                    case "5":
                        nCompany.settingGradeAdmin = 1;
                        _dbMaster.SaveChanges();
                        break;

                    case "6":
                        nCompany.settingGradeAdmin = 0;
                        _dbMaster.SaveChanges();
                        break;
                    case "7":
                        nCompany.settingReportView = 1;
                        _dbMaster.SaveChanges();
                        break;

                    case "8":
                        nCompany.settingReportView = 0;
                        _dbMaster.SaveChanges();
                        break;
                }

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