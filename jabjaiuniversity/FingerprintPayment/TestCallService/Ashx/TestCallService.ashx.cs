
using FingerprintPayment.ViewModels;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.TestCallService.Ashx
{
    /// <summary>
    /// Summary description for TestCallService
    /// </summary>
    public class TestCallService : IHttpHandler, IRequiresSessionState
    {

        public List<YearData> years = new List<YearData>();

        //[Inject]
        //public IGraduationService CommonService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string jsonStream = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            //int draw = Convert.ToInt32(jsonObject["draw"]);
            //int pageIndex = Convert.ToInt32(jsonObject["page"]);
            //int pageSize = Convert.ToInt32(jsonObject["length"]);
            //string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            //string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            //string sortBy = "ID";
            //switch (sortIndex)
            //{
            //    case "1": sortBy = "Type"; break;
            //    case "2": sortBy = "Code"; break;
            //    case "3": sortBy = "Title"; break;
            //    case "4": sortBy = "FirstName"; break;
            //    case "5": sortBy = "LastName"; break;
            //    case "6": sortBy = "PhoneNumber"; break;
            //    case "7": sortBy = "Birthday"; break;
            //}
            //sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //string empType = Convert.ToString(jsonObject["empType"]);
            //string studentName = Convert.ToString(jsonObject["studentName"]);

            //var json = QueryEngine.LoadEmployeeJsonData(draw, pageIndex, pageSize, sortBy, empType, studentName);


            //JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            //var ss = HttpContext.Current.Session["sEntities"].ToString();
            //if (HttpContext.Current.Session["sEntities"] != null)
            //{
            //    List<YearDTO> yeatDTO = CommonService.GetYears(HttpContext.Current.Session["sEntities"].ToString());
            //}
            var json = "asd";

            context.Response.Write(json);
        }

        public bool IsReusable { get { return false; } }
    }
}