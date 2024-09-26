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
    /// Summary description for holidayEditPillbox
    /// </summary>
    public class holidayEditPillbox : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using(JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                

                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                dynamic rss = new JObject();
                string id = fcommon.ReplaceInjection(context.Request["id"]);

                var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                holidayPillbox edit = new holidayPillbox();
                List<holidayPillbox> editList = new List<holidayPillbox>();

                foreach (var data in _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == id && w.Deleted != 1))
                {
                    edit = new holidayPillbox();
                    edit.nTSubLevel = data.nTSubLevel.ToString();
                    editList.Add(edit);
                }

                rss = new JArray(from a in editList
                                 select new JObject(
                     new JProperty("nTSubLevel", a.nTSubLevel)
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

        protected class holidayPillbox
        {
            public string nTSubLevel { get; set; }
        }


    }


}