using FingerprintPayment.AssetManagement.CsCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.AssetManagement.Ashx
{
    /// <summary>
    /// Summary description for LoadReport01List
    /// </summary>
    public class LoadReport01List : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string jsonStream = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "ProdID";
            switch (sortIndex)
            {
                case "1": sortBy = "Code"; break;
                case "2": sortBy = "Category"; break;
                case "3": sortBy = "Product"; break;
                case "4": sortBy = "Amount"; break;
                case "5": sortBy = "Unit"; break;
                case "6": sortBy = "Department"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string searchYear = Convert.ToString(jsonObject["searchYear"]);
            string searchCategory = Convert.ToString(jsonObject["searchCategory"]);

            var json = QueryEngine.LoadReport01JsonData(draw, pageIndex, pageSize, sortBy, searchYear, searchCategory);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}