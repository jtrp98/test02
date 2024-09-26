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
    /// Summary description for LoadWithdrawList
    /// </summary>
    public class LoadWithdrawList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                case "1": sortBy = "DateStamp"; break;
                case "2": sortBy = "Code"; break;
                case "3": sortBy = "TransType"; break;
                case "4": sortBy = "Product"; break;
                case "5": sortBy = "Amount"; break;
                case "6": sortBy = "Unit"; break;
                case "7": sortBy = "Department"; break;
                case "8": sortBy = "Receiver"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string searchDate = Convert.ToString(jsonObject["searchDate"]);

            var json = QueryEngine.LoadWithdrawJsonData(draw, pageIndex, pageSize, sortBy, searchDate);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}