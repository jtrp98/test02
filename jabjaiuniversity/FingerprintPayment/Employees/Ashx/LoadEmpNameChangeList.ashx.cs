using FingerprintPayment.Employees.CsCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.Employees.Ashx
{
    /// <summary>
    /// Summary description for LoadEmpNameChangeList
    /// </summary>
    public class LoadEmpNameChangeList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string jsonStream = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int eid = Convert.ToInt32(jsonObject["eid"]);

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "OldFirstName"; break;
                case "2": sortBy = "OldLastName"; break;
                case "3": sortBy = "NewFirstName"; break;
                case "4": sortBy = "NewLastName"; break;
                case "5": sortBy = "ChangeDate"; break;
                case "6": sortBy = "ChangePlace"; break;
                case "7": sortBy = "UpdateDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = Convert.ToString(jsonObject["search"]);

            var json = QueryEngine.LoadEmpNameChangeJsonData(draw, pageIndex, pageSize, sortBy, EmployeeGateway.GetUserData().CompanyID, eid, search);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}