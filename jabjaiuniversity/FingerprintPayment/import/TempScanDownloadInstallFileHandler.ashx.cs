using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using FingerprintPayment.Class;
using JabjaiMainClass;
using Newtonsoft.Json;

namespace FingerprintPayment.import
{
    /// <summary>
    /// Summary description for TempScanDownloadInstallFileHandler
    /// </summary>
    public class TempScanDownloadInstallFileHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool success = true;
            string message = "Load Successfully";

            TempScanLastVersion lastVersion = new TempScanLastVersion();

            string logMessagePattern = @"[FingerprintPayment.import.TempScanDownloadInstallFileHandler.ashx], [ErrorMessage:{0}], [StackTrace:{1}]";

            int schoolID = 0;

            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken.userData();
                if (token.CheckToken(HttpContext.Current))
                {
                    userData = token.getTokenValues(HttpContext.Current);

                    schoolID = userData.CompanyID;
                }
                else
                {
                    throw new Exception();
                }

                AWSTempScanFunction awsTempScanFunction = new AWSTempScanFunction(schoolID);
                lastVersion = awsTempScanFunction.ReadTempScanVersionS3Object();
                if (lastVersion.IsSuccess)
                {

                }
                else
                {
                    success = false;
                    message = lastVersion.Message;

                    // Log
                    string logMessage = string.Format(logMessagePattern, lastVersion.Message, lastVersion.StackTrace);

                    ComFunction.InsertLogDebug(schoolID, null, null, logMessage);
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;

                // Log
                string logMessage = string.Format(logMessagePattern, lastVersion.Message, lastVersion.StackTrace);

                ComFunction.InsertLogDebug(schoolID, null, null, logMessage);
            }

            var result = new
            {
                success,
                message,
                versionData = lastVersion
            };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }

    }
}