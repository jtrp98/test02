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
    /// Summary description for ddlTerm
    /// </summary>
    public class ddlTerm : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();

            string year = fcommon.ReplaceInjection(context.Request["year"]);
            int.TryParse(year, out int nYear);

            try
            {
                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    
                        string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
                    var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();


                    var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                    ddl ddl = new ddl();
                    List<ddl> ddlList = new List<ddl>();

                    var yeardata = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.numberYear == nYear).FirstOrDefault();
                    if (yeardata != null)
                    {
                        var terms = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nYear == yeardata.nYear && w.cDel == null).ToList();
                        foreach (var a in terms)
                        {
                            ddl = new ddl();
                            ddl.name = a.sTerm;
                            ddl.value = a.sTerm;
                            ddlList.Add(ddl);
                        }
                    }

                    rss = new JArray(from a in ddlList
                                     select new JObject(
                           new JProperty("name", a.name),
                           new JProperty("value", a.value)
                        ));
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nYear:{0}", nYear);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "ddlTerm.ashx", parameters, "", null);
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

        protected class ddl
        {
            public string name { get; set; }
            public string value { get; set; }
        }

    }
}

