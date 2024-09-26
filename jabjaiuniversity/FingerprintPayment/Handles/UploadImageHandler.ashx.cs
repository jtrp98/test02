using FingerprintPayment.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Handles
{
    /// <summary>
    /// Summary description for UploadImageHandler
    /// </summary>
    public class UploadImageHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            string collection = context.Request["cl"]; // tmp-emp = Temp Employee, tmp-emp-honor = Temp Employee Honor

            bool isSuccess = true;
            string fileName = "";
            try
            {
                if (context.Request.Files.Count > 0)
                {
                    string collectionPath = "";
                    string prefixFile = "";
                    switch (collection)
                    {
                        case "tmp-emp":

                            collectionPath = string.Format(GlobalVariable.EMPLOYEE_INFO_TMEP_FOLDER, HttpContext.Current.Session["sEmpID"]);

                            break;
                        case "tmp-emp-honor":

                            collectionPath = string.Format(GlobalVariable.EMPLOYEE_HONOR_TMEP_FOLDER, HttpContext.Current.Session["sEmpID"]);

                            break;
                        case "tmp-visit-out":

                            collectionPath = string.Format(GlobalVariable.VISITHOUSE_TMEP_FOLDER, HttpContext.Current.Session["sEmpID"]);

                            prefixFile = "OUT_";

                            break;
                        case "tmp-visit-in":

                            collectionPath = string.Format(GlobalVariable.VISITHOUSE_TMEP_FOLDER, HttpContext.Current.Session["sEmpID"]);

                            prefixFile = "IN_";

                            break;
                    }

                    // Check Directory
                    string tempPath = context.Server.MapPath(collectionPath);
                    if (!Directory.Exists(tempPath))
                    {
                        Directory.CreateDirectory(tempPath);
                    }

                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];

                        fileName = ComFunction.GetFileNameFromBrowser(file);

                        // Check File
                        string fullPathFile = Path.Combine(tempPath, prefixFile + fileName);
                        if (File.Exists(fullPathFile))
                        {
                            File.Delete(fullPathFile);
                        }

                        file.SaveAs(fullPathFile);
                    }
                }
            }
            catch
            {
                isSuccess = false;
            }

            var result = new { success = isSuccess, file = fileName, message = "File Uploaded Successfully!" };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}