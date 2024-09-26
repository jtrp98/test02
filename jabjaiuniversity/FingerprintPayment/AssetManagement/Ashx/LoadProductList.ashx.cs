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
    /// Summary description for LoadProductList
    /// </summary>
    public class LoadProductList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "Code"; break;
                case "2": sortBy = "Category"; break;
                case "3": sortBy = "[Type]"; break;
                case "4": sortBy = "Product"; break;
                case "5": sortBy = "Unit"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string searchCat = Convert.ToString(jsonObject["searchCat"]);
            string searchProd = Convert.ToString(jsonObject["searchProd"]);

            var json = QueryEngine.LoadProductJsonData(draw, pageIndex, pageSize, sortBy, searchCat, searchProd);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}