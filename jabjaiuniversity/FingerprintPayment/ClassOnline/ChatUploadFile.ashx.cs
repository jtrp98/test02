using FingerprintPayment.ClassOnline.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.ClassOnline
{
    /// <summary>
    /// Summary description for UploadFileBG
    /// </summary>
    public class ChatUploadFile : IHttpHandler , IRequiresSessionState
    {


        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            var schoolId = userData.CompanyID;
            var wid = int.Parse(context.Request.QueryString["wid"] + "");
            var uid = int.Parse(context.Request.QueryString["uid"] + "");

            //var file_data = context.Request.Files;
            if (context.Request.Files.Count > 0)
            {
                using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
                {
                   
                        try
                        {
                            var comment = new THomeWorkReply();
                            comment.HomeWorkId = wid;
                            comment.sID = uid;
                            comment.tID = userData.UserID;
                            comment.Comment = null;
                            comment.Created = DateTime.Now;
                            comment.Modified = DateTime.Now;
                            comment.Type = 2;
                            comment.SchoolId = userData.CompanyID;

                            ctx.THomeWorkReplies.Add(comment);

                            ctx.SaveChanges();

                            //foreach (var fx in file_data)
                            for (int i = 0; i < context.Request.Files.Count; i++)
                            {
                                var f = context.Request.Files[i];//fx as HttpPostedFile;
                                if (f.ContentLength == 0) continue;

                                string linkfiles = UploadFile.UploadFileHttpRequestBase(f
                                    , "learningonline/work/" + userData.UserID
                                    , userData.CompanyID
                                    , wid);

                                if (!string.IsNullOrEmpty(linkfiles))
                                {
                                    ctx.THomeWorkReply_File.Add(new THomeWorkReply_File
                                    {
                                        ReplyID = comment.ReplyId,
                                        ContentType = f.ContentType,
                                        HomeWorkID = wid,
                                        FileUrl = linkfiles,
                                        FileTitle = f.FileName.SubstringFileName(20),
                                        SchoolID = userData.CompanyID,
                                        cDel = false,
                                    });
                                }
                            }

                            ctx.SaveChanges();

                           
                        }
                        catch
                        {
                            
                        }
                                      
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                status = "ok",
                files = context.Request.Files.Count
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
