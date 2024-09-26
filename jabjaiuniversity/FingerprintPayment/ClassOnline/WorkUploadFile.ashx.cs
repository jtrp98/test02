using FingerprintPayment.ClassOnline.Helper;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.ClassOnline
{
    /// <summary>
    /// Summary description for Handler1
    /// </summary>
    public class Handler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //var SchoolId = Convert.ToInt32(context.Session["nCompany"] + "");
            // HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["fileUpload1"];
            //using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            //{

            //}

            //foreach (string f in context.Request.Files)
            //{
            //    HttpPostedFile file = context.Request.Files[f] as HttpPostedFile;
            //    if (file.ContentLength == 0)
            //        continue;

            //    //string linkfiles = UploadFile.UploadFileHttpRequestBase(file
            //    //    , "learningonline/work"
            //    //    , SchoolId
            //    //    , hw.nHomeWork);//await AzureStorage.UploadFile(files, "online_homework", schoolID);

            //    //if (!string.IsNullOrEmpty(linkfiles))
            //    //{
            //    //    //ctx.THomeWorkFiles.Add(new THomeWorkFile
            //    //    //{
            //    //    //    ContentType = file.ContentType,
            //    //    //    nHomeWorkId = hw.nHomeWork,
            //    //    //    sFileName = linkfiles,
            //    //    //    Title = file.FileName,
            //    //    //    SchoolId = SchoolId,
            //    //    //});
            //    //}
            //    // savedFileName = context.Server.MapPath(Path.GetFileName(hpf.FileName));
            //    // hpf.SaveAs(savedFileName);
            //}
            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { title = "", url = "", ctype = "" }));
           // context.Response.Write("");
            context.Response.End();

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