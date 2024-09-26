using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Users
{
    /// <summary>
    /// Summary description for CustomerSearchHandler
    /// </summary>
    public class UserSearchHandle : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string json = string.Empty;

            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString());
            dynamic rss = new JObject();
            rss.Result = new JArray(from a in this.GetDataJSON(context.Request.QueryString["term"], userData.CompanyID).Result
                                    select new JObject
                                    {
                                        new JProperty("UserId",a.UserId),
                                        new JProperty("FullName",a.FullName),
                                        new JProperty("IsDeleted",a.IsDeleted),
                                    });
            //json = new JavaScriptSerializer().Serialize(q_data);
            context.Response.ContentType = "text/json";
            context.Response.Write(rss);
            context.Response.End();
        }

        private BaseResult<List<Jabjai.Object.Entity.Jabjai.TUser>> GetDataJSON(string filter, int SchoolId)
        {
            UserLogic userLogic = new UserLogic();
            return userLogic.GetByTextSearch(filter, SchoolId);
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