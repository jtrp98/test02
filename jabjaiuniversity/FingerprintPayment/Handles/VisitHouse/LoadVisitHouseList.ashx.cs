using FingerprintPayment.Class;
using Jabjai.Common;
using JabjaiMainClass;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.Handles.VisitHouse
{
    /// <summary>
    /// Summary description for LoadVisitHouseList
    /// </summary>
    public class LoadVisitHouseList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "application/json";

            string jsonStream = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "sID";
            switch (sortIndex)
            {
                case "1": sortBy = "FirstName"; break;
                case "2": sortBy = "LastName"; break;
                case "3": sortBy = "StudentCode"; break;
                case "4": sortBy = "StampDate"; break;
                case "5": sortBy = "StampTime"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = Convert.ToString(jsonObject["search"]);
            string year = Convert.ToString(jsonObject["year"]);
            string term = Convert.ToString(jsonObject["term"]);
            string subLevel = Convert.ToString(jsonObject["subLevel"]);
            string termSubLevel = Convert.ToString(jsonObject["termSubLevel"]);

            var json = QueryEngine.LoadVisitHouseJsonData(draw, pageIndex, pageSize, sortBy, search, year, term, subLevel, termSubLevel,userData.CompanyID);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}