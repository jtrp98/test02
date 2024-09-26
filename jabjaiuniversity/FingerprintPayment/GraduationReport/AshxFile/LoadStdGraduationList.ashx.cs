using FingerprintPayment.GraduationReport.CsFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.GraduationReport.AshxFile
{
    /// <summary>
    /// Summary description for LoadStdGraduationList
    /// </summary>
    public class LoadStdGraduationList : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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

            string sortBy = "Identification";
            switch (sortIndex)
            {
                case "1": sortBy = "StudentId"; break;
                case "2": sortBy = "Identification"; break;
                case "3": sortBy = "Title"; break;
                case "4": sortBy = "Name"; break;
                case "5": sortBy = "Lastname"; break;
                case "6": sortBy = "ClassDisplay"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            string ddlYear = Convert.ToString(jsonObject["ddlYear"]);
            string ddlTerm = Convert.ToString(jsonObject["ddlTerm"]);
            string ddlSubLV = Convert.ToString(jsonObject["ddlSubLV"]);

            var json = QueryEngine.LoadGraduationReportJsonData(draw, pageIndex, pageSize, sortBy, ddlYear, ddlTerm, ddlSubLV);

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}