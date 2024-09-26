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
    /// Summary description for LoadEmployeeList
    /// </summary>
    public class LoadEmployeeList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                case "1": sortBy = "Type"; break;
                case "2": sortBy = "RIGHT('0000000000' + Code, 10)"; break;
                case "3": sortBy = "Title"; break;
                case "4": sortBy = "FirstName"; break;
                case "5": sortBy = "LastName"; break;
                case "6": sortBy = "PhoneNumber"; break;
                case "7": sortBy = "Birthday"; break;
                case "8": sortBy = "WorkStatus"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string empType = Convert.ToString(jsonObject["empType"]);
            string studentName = Convert.ToString(jsonObject["studentName"]);

            var json = QueryEngine.LoadEmployeeJsonData(draw, pageIndex, pageSize, sortBy, EmployeeGateway.GetUserData().CompanyID, empType, studentName);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}