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
    /// Summary description for LoadEmpLeaveList
    /// </summary>
    public class LoadEmpLeaveList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                case "1": sortBy = "Year"; break;
                case "2": sortBy = "Late"; break;
                case "3": sortBy = "Sick"; break;
                case "4": sortBy = "Errand"; break;
                case "5": sortBy = "Lacking"; break;
                case "6": sortBy = "ToGovernor"; break;
                case "7": sortBy = "Holiday"; break;
                case "8": sortBy = "Maternity"; break;
                case "9": sortBy = "Ordain"; break;
                case "10": sortBy = "ContinueStudy"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = Convert.ToString(jsonObject["search"]);

            var json = QueryEngine.LoadEmpLeaveJsonData(draw, pageIndex, pageSize, sortBy, EmployeeGateway.GetUserData().CompanyID, eid, search);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}