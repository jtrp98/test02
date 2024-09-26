using FingerprintPayment.ClassOnline.Helper;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.StudentCall
{
    /// <summary>
    /// Summary description for UploadFileBG
    /// </summary>
    public class RemoveFileBG : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var c = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == userData.CompanyID);

                if (c != null)
                {
                    c.BgCard = "";
                }

                dbmaster.SaveChanges();
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                d = userData.CompanyID
            }));
                      
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
//if (context.Request.Files.Count > 0)
//{
//    HttpFileCollection files = context.Request.Files;
//    for (int i = 0; i < files.Count; i++)
//    {
//        HttpPostedFile file = files[i];
//        string fname = context.Server.MapPath("~/uploads/" + file.FileName);
//        file.SaveAs(fname);
//    }
//}
