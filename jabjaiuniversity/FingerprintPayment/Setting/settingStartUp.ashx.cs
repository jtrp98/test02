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
    /// Summary description for settingStartUp
    /// </summary>
    public class settingStartUp : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                TCompany TCompany = new TCompany();
                List<TCompany> TCompanyList = new List<TCompany>();
                TCompany = new TCompany();
                if (tCompany.settingGradeAdmin == null)
                    TCompany.settingGradeAdmin = 0;
                else TCompany.settingGradeAdmin = tCompany.settingGradeAdmin;

                if (tCompany.settingPlanTeacher == null)
                    TCompany.settingPlanTeacher = 1;
                else TCompany.settingPlanTeacher = tCompany.settingPlanTeacher;

                if (tCompany.settingReportView == null)
                    TCompany.settingReportView = 0;
                else TCompany.settingReportView = tCompany.settingReportView;

                if (tCompany.settingTimePeriod == null)
                    TCompany.settingTimePeriod = 0;
                else TCompany.settingTimePeriod = tCompany.settingTimePeriod;

                TCompanyList.Add(TCompany);

                rss = new JArray(from a in TCompanyList
                                 select new JObject(
                       new JProperty("settingGradeAdmin", a.settingGradeAdmin),
                       new JProperty("settingPlanTeacher", a.settingPlanTeacher),
                      new JProperty("settingReportView", a.settingReportView),
                      new JProperty("settingTimePeriod", a.settingTimePeriod)

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


    }


}