using JabjaiMainClass;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class bypassgarde : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx?returnUrl=" + Request.Url.PathAndQuery);
            }
            var hostURL = Request.Url.Scheme + "://" + Request.Url.Authority + "/";
            var payload = new Dictionary<string, object>
                                    {
                                        { "user_id", userData.UserID },
                                        { "admin_id", userData.AdminID  },
                                        { "entities", userData.Entities },
                                        { "user_name" ,userData.Name},
                                        { "user_type", "1" },
                                        { "SchoolDBConnectionString",  "" },
                                        { "NCompany", userData.CompanyID},
                                        { "IsAdmin", (userData.UserID == 0 && userData.AdminID != null) ? 1 : 0 },
                                        { "HostURL", hostURL }
                                    };

            string secret = ConfigurationManager.AppSettings["token_secret"].ToString();

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var Token = encoder.Encode(payload, secret);

            string page = (string)Request.QueryString["page"];
            string url = (string)Request.QueryString["url"];
            url = string.IsNullOrEmpty(url) ? "" : "&page=" + url;
            switch (page)
            {
                case "exam":
                    Response.Redirect(ConfigurationManager.AppSettings["ExamURL"].ToString() + "/home/getToken?token=" + Token + url);
                    break;
                case "library":
                    Response.Redirect(ConfigurationManager.AppSettings["LibraryURL"].ToString() + "/home/getToken?token=" + Token + url);
                    break;
                case "alphatutor":
                    Response.Redirect(ConfigurationManager.AppSettings["AlphaTutorURL"].ToString() + "/home/getToken?token=" + Token + url);
                    break;
                case "kindergarden":
                    Response.Redirect(ConfigurationManager.AppSettings["KinderGardenURL"].ToString() + "/home/getToken?token=" + Token + url);
                    break;
                case "accounting":
                    Response.Redirect(ConfigurationManager.AppSettings["AccountingURL"].ToString() + "/Home/getToken?token=" + Token + url);
                    break;
                case "academic":
                    url = Request.Url.Query.Replace("?page=academic&url=","");
                    url = string.IsNullOrEmpty(url) ? "" : "&page=" + url;
                    Response.Redirect(ConfigurationManager.AppSettings["AcademicURL"].ToString() + "/BypassAcademic.aspx?token=" + Token + url);
                    break;
                case "canteen":
                    Response.Redirect(ConfigurationManager.AppSettings["CanteenURL"].ToString() + "/GetToken.aspx?token=" + Token + url);
                    break;
                default:
                    Response.Redirect(ConfigurationManager.AppSettings["GradeURL"].ToString() + "/home/getToken?token=" + Token + url);
                    //Response.Redirect("https://grade-dev.schoolbright.co/home/getToken?token=" + Token);
                    break;
            }
        }
    }
}