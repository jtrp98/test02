using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FingerprintPayment.PreRegister.Ashx
{
    /// <summary>
    /// Summary description for UploadFileRegularityHandler
    /// </summary>
    public class UploadFileRegularityHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            int lid = Convert.ToInt32(context.Request["lid"]);

            bool success = true;
            string message = "File Uploaded Successfully!";
            try
            {
                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    JWTToken token = new JWTToken();
                    var userData = new JWTToken.userData();
                    if (token.CheckToken(HttpContext.Current))
                    {
                        userData = token.getTokenValues(HttpContext.Current);
                    }
                    else
                    {
                        throw new Exception();
                    }

                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                        int schoolID = userData.CompanyID;
                        string linkfiles = ComFunction.UploadFileHttpRequestBase(fileBase, "preregister/regularity", schoolID, lid);

                        if (!string.IsNullOrEmpty(linkfiles))
                        {
                            // Save linkfiles
                            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                            {
                                TRegisterRegularity registerRegularity = en.TRegisterRegularities.Where(w => w.SchoolID == schoolID && w.nTSubLevel == lid).FirstOrDefault();
                                if (registerRegularity == null)
                                {
                                    registerRegularity = new TRegisterRegularity
                                    {
                                        nTSubLevel = lid,
                                        Filename = linkfiles,
                                        UpdateDate = DateTime.Now,
                                        UpdateBy = userData.UserID,
                                        SchoolID = schoolID,
                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now,
                                        cDel = false
                                    };

                                    en.TRegisterRegularities.Add(registerRegularity);
                                }
                                else
                                {
                                    registerRegularity.Filename = linkfiles;
                                    registerRegularity.UpdateDate = DateTime.Now;
                                    registerRegularity.UpdateBy = userData.UserID;
                                    registerRegularity.cDel = false;
                                }

                                en.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                message = error.Message;
            }

            var result = new { success, message };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}