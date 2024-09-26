using FingerprintPayment.Employees.CsCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.Ashx
{
    /// <summary>
    /// Summary description for LoadEmpAddressList
    /// </summary>
    public class LoadEmpAddressList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            int draw = Convert.ToInt32(HttpContext.Current.Request.QueryString["draw"]);
            int startIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["start"]);
            int pageSize = Convert.ToInt32(HttpContext.Current.Request.QueryString["length"]);
            string sortIndex = HttpContext.Current.Request.QueryString["order[0][column]"];
            string orderDir = HttpContext.Current.Request.QueryString["order[0][dir]"];

            string sortBy = "ID";
            switch (sortIndex)
            {
                case "1": sortBy = "FirstName"; break;
                case "2": sortBy = "LastName"; break;
                case "3": sortBy = "PhoneNumber"; break;
                case "4": sortBy = "Birthday"; break;
                case "5": sortBy = "UpdateDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string search = HttpContext.Current.Request.QueryString["search[value]"];

            //var json = QueryEngine.LoadEmployeeJsonData(draw, startIndex, pageSize, sortBy, search);

            context.Response.Write("");
        }

        public bool IsReusable { get { return false; } }
    }
}