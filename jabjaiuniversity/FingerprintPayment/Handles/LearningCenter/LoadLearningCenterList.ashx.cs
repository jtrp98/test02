using FingerprintPayment.Class;
using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.Handles.LearningCenter
{
    /// <summary>
    /// Summary description for LoadLearningCenterList
    /// </summary>
    public class LoadLearningCenterList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
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

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "[Type]"; break;
                case "2": sortBy = "Name"; break;
                case "3": sortBy = "Detail"; break;
                case "4": sortBy = "[Admin]"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = Convert.ToString(jsonObject["search"]);
            string learningType = Convert.ToString(jsonObject["learningType"]);

            var json = QueryEngine.LoadLearningCenterJsonData(draw, pageIndex, pageSize, sortBy, search, learningType,userData.CompanyID);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}