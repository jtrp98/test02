using JabjaiMainClass;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FingerprintPayment.Helper
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public string ErrorMessage;
        private const string DefaultErrorMessage = "Automatic error generated. This is caused by the FailRequest ActionFilter. To stop this error, remove the attribute from the class or method.";

        public HttpStatusCode StatusCode = HttpStatusCode.RequestTimeout;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            
            HttpContext ctx = HttpContext.Current;
            JWTToken token = new JWTToken();
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["nCompany"] == null)
            {
                //token.CheckToken(HttpContext.Current);
                if (!token.CheckToken(HttpContext.Current))
                {
                    var queryString = actionContext.Request.RequestUri.OriginalString.Replace(actionContext.Request.RequestUri.PathAndQuery, "");
                    var response = actionContext.Request.CreateResponse(StatusCode, ErrorMessage);
                    response.Headers.Location = new Uri(queryString);
                    response.StatusCode = StatusCode;
                    actionContext.Response = response;
                }

                //throw new HttpResponseException(actionContext.Request.CreateErrorResponse(StatusCode, ErrorMessage));
                return;
            }
            else
            {
                //token.CheckToken(HttpContext.Current);
            }
            base.OnActionExecuting(actionContext);
        }


       
    }
}