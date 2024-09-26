using JabjaiMainClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.Ashx
{
    /// <summary>
    /// Summary description for KeepProfilePictureInSession
    /// </summary>
    public class KeepProfilePictureInSession : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool isSuccess = true;
            string resultMessage = "File Uploaded Successfully!";

            try
            {
                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        //HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                        // Save to session
                        context.Session["RegisterOnlineProfilePictureObject"] = file;
                    }
                }
            }
            catch (Exception err)
            {
                isSuccess = false;
                resultMessage = err.Message;
            }

            var result = new { success = isSuccess, message = resultMessage };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}