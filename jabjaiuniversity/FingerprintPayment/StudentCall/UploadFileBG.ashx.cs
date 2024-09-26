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
    public class UploadFileBG : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            string linkfiles = "";
            if (context.Request.Files.Count > 0)
            {
                HttpFileCollection files = context.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = files[i];

                    if (file.ContentLength == 0)
                        continue;

                    linkfiles = UploadFile.UploadFileParentCardBG(file
                        , "parentcard"
                        , userData.CompanyID
                        );//await AzureStorage.UploadFile(files, "online_homework", schoolID);

                    if (!string.IsNullOrEmpty(linkfiles))
                    {
                        using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            var c = dbmaster.TStudentCall_Config.FirstOrDefault(o => o.SchoolId == userData.CompanyID);

                            if (c != null)
                            {
                                c.BgCard = linkfiles;
                            }
                            else
                            {
                                dbmaster.TStudentCall_Config.Add(new TStudentCall_Config
                                {
                                    SchoolId = userData.CompanyID,
                                    BgCard = linkfiles,
                                });
                            }

                            dbmaster.SaveChanges();
                        }
                    }
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { d = userData.CompanyID, path = linkfiles }));

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


            //context.Response.ContentType = "text/plain";
            //context.Response.Write("File(s) Uploaded Successfully!" + context.Request.Files.Count);
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
