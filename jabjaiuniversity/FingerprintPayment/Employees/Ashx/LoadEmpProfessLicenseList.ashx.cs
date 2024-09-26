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
    /// Summary description for LoadEmpProfessLicenseList
    /// </summary>
    public class LoadEmpProfessLicenseList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                case "1": sortBy = "LicenseType"; break;
                case "2": sortBy = "LicenseNo"; break;
                case "3": sortBy = "LicenseName"; break;
                case "4": sortBy = "IssuedDate"; break;
                case "5": sortBy = "ExpireDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = Convert.ToString(jsonObject["search"]);

            var json = QueryEngine.LoadEmpProfessLicenseJsonData(draw, pageIndex, pageSize, sortBy, EmployeeGateway.GetUserData().CompanyID, eid, search);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}