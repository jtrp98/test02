using FingerprintPayment.Employees.CsCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.Ashx
{
    /// <summary>
    /// Summary description for LoadEmpHonorList
    /// </summary>
    public class LoadEmpHonorList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            int eid = Convert.ToInt32(HttpContext.Current.Request.QueryString["eid"]);

            int draw = Convert.ToInt32(HttpContext.Current.Request.QueryString["draw"]);
            int startIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.QueryString["length"]);
            string sortIndex = HttpContext.Current.Request.QueryString["order[0][column]"];
            string orderDir = HttpContext.Current.Request.QueryString["order[0][dir]"];

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "Year"; break;
                case "2": sortBy = "Image"; break;
                case "3": sortBy = "UpdateDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = HttpContext.Current.Request.QueryString["search[value]"];

            var json = QueryEngine.LoadEmpHonorJsonData(draw, startIndex, pageSize, sortBy, EmployeeGateway.GetUserData().CompanyID, eid, search);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}