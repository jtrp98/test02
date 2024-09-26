using FingerprintPayment.Class;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Handles
{
    /// <summary>
    /// Summary description for GetImageHandler
    /// </summary>
    public class GetImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/png";

            string collection = context.Request["cl"]; // tmp-emp = Temp Employee, emp = Employee, tmp-emp-honor = Temp Employee Honor, emp-honor = Employee Honor
            string imageName = context.Request["im"];
            string year = context.Request["year"];
            string id = context.Request["id"];

            string collectionPath = "";
            switch (collection)
            {
                case "tmp-emp": collectionPath = string.Format(GlobalVariable.EMPLOYEE_INFO_TMEP_FILE, HttpContext.Current.Session["sEmpID"], imageName); break;
                case "emp": collectionPath = string.Format(GlobalVariable.EMPLOYEE_INFO_FILE, id, imageName); break;

                case "tmp-emp-honor": collectionPath = string.Format(GlobalVariable.EMPLOYEE_HONOR_TMEP_FILE, HttpContext.Current.Session["sEmpID"], imageName); break;
                case "emp-honor": collectionPath = string.Format(GlobalVariable.EMPLOYEE_HONOR_FILE, id, imageName); break;

                case "tmp-visit-out": collectionPath = string.Format(GlobalVariable.VISITHOUSE_TMEP_OUT_FILE, HttpContext.Current.Session["sEmpID"], imageName); break;
                case "tmp-visit-in": collectionPath = string.Format(GlobalVariable.VISITHOUSE_TMEP_IN_FILE, HttpContext.Current.Session["sEmpID"], imageName); break;
                case "visit-out": collectionPath = string.Format(GlobalVariable.VISITHOUSE_OUT_FILE, year, id, imageName); break;
                case "visit-in": collectionPath = string.Format(GlobalVariable.VISITHOUSE_IN_FILE, year, id, imageName); break;
            }

            string path = HttpContext.Current.Server.MapPath(collectionPath);

            ComFunction.RenderStream(context, path);
        }

        public bool IsReusable { get { return false; } }
    }
}