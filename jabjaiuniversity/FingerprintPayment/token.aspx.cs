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
    public partial class token : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            var payload = new Dictionary<string, object>
                                    {
                                        { "user_id", userData.UserID },
                                        { "entities", userData.Entities },
                                        { "user_name" ,userData.Name},
                                        { "user_type", "1" },
                                        { "SchoolDBConnectionString",  "" },
                                        { "NCompany", userData.CompanyID}
                                    };

            string secret = ConfigurationManager.AppSettings["token_secret"].ToString();

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            var Token = encoder.Encode(payload, secret);

            //Response.Write(Token);
            Response.Write("https://grade-dev.schoolbright.co/home/getToken?token=" + Token);

            //string page = (string)Request.QueryString["page"];
            //switch (page)
            //{
            //    case "exam":
            //        Response.Redirect(ConfigurationManager.AppSettings["ExamURL"].ToString() + "/home/getToken?token=" + Token);
            //        break;
            //    default:
            //        Response.Redirect(ConfigurationManager.AppSettings["GradeURL"].ToString() + "/home/getToken?token=" + Token);
            //        //Response.Redirect("https://grade-dev.schoolbright.co/home/getToken?token=" + Token);
            //        break;
            //}
        }
    }
}