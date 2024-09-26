using FingerprintPayment.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace FingerprintPayment.PreRegister.Ashx
{
    /// <summary>
    /// Summary description for ActivePageComplete
    /// </summary>
    public class ActivePageComplete : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string jsonStream = new StreamReader(context.Request.InputStream).ReadToEnd();
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int pid = Convert.ToInt32(jsonObject["pid"]);

            bool isSuccess = true;
            string resultMessage = "Active Page Successfully!";

            bool isRedirect = false;
            string redirectUrl = "";

            try
            {
                //int activePage = Convert.ToInt32(context.Session["ActivePage"]);
                //if (activePage + 2 < pid)
                //{
                //    string schoolEntities = (string)context.Session["RegisterOnlineEntities"];

                //    redirectUrl = string.Format(@"RegisterStart.aspx?id={0}", ComFunction.Rot13Transform(schoolEntities));

                //    isRedirect = true;
                //}
                //else
                //{
                //    context.Session["ActivePage"] = pid;
                //}

                // Save active page
                context.Session["ActivePage"] = pid;

            }
            catch (Exception err)
            {
                isSuccess = false;
                resultMessage = err.Message;
            }

            var result = new { success = isSuccess, message = resultMessage, redirect = isRedirect, url = redirectUrl };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}