using MasterEntity;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace FingerprintPayment
{
    public class RouteConfig
    {
        //public static List<TB_Send_API_Log> _Send_API_Logs = new List<TB_Send_API_Log>();
   
     

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region MVC Routes
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            #endregion



            try
            {
                string UseSQLiteDB = ConfigurationManager.AppSettings["UseSQLiteDB"].ToString();



                if (UseSQLiteDB.ToUpper() == "Y")
                    SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade = false;
                else
                    SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade = false;
            }
            catch
            {
                SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade = false; // defaulted to true
            }
        }

    }
}

