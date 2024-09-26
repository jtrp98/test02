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
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for ddlClassroom
    /// </summary>
    public class ddlClassroom : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
            int nidlv = !string.IsNullOrEmpty(idlv) ? int.Parse(idlv) : 0;
            dynamic rss = new JObject();

            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    ddl ddl = new ddl();
                    List<ddl> ddlList = new List<ddl>();

                    foreach (var lv1 in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == nidlv && w.nTermSubLevel2Status == "1" && w.nWorkingStatus == 1))
                    {
                        var lv2 = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == nidlv).FirstOrDefault();
                        ddl = new ddl();
                        ddl.name = lv2.SubLevel.TrimEnd() + " / " + lv1.nTSubLevel2;
                        ddl.value = lv1.nTermSubLevel2.ToString();
                        int n;
                        bool isNumeric = int.TryParse(lv1.nTSubLevel2, out n);
                        if (isNumeric == true)
                            ddl.sort = Int32.Parse(lv1.nTSubLevel2);
                        else ddl.sort = 500;

                        ddlList.Add(ddl);
                    }

                    var newList3 = ddlList.OrderBy(x => x.sort).ThenBy(x => x.name).ToList();


                    rss = new JArray(from a in newList3
                                     select new JObject(
                           new JProperty("name", a.name),
                         new JProperty("value", a.value)
                        ));
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nTSubLevel:{0}", idlv);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "ddlClassroom.ashx", parameters, "", null);
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
            public int sort { get; set; }
        }

    }


}