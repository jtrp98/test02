using FingerprintPayment.Employees.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace FingerprintPayment.Handles
{
    /// <summary>
    /// Summary description for InitDropdownData
    /// </summary>
    public class InitDropdownData : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "application/json";

            string table = HttpContext.Current.Request.QueryString["table"];

            var json = "";
            List<object> result = new List<object>();

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                switch (table)
                {
                    case "Year":
                        result = en.TYears.Where(w=>w.SchoolID == userData.CompanyID && w.cDel == false).Select(s => new { id = s.nYear, value = s.numberYear }).ToList<object>();
                        break;
                    case "Term":
                        result = en.TTerms.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.nTerm, value = s.sTerm }).ToList<object>();
                        break;
                    case "SubLevel":
                        result = en.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.nTSubLevel, value = s.fullName }).ToList<object>();
                        break;
                    case "TermSubLevel":
                        result = en.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Select(s => new { id = s.nTermSubLevel2, value = s.nTSubLevel2 }).ToList<object>();
                        break;

                }
            }

            json = JsonConvert.SerializeObject(result);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}