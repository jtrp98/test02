using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Employees.Ashx
{
    /// <summary>
    /// Summary description for UploadImageProfileHandler
    /// </summary>
    public class UploadImageProfileHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            int empID = Convert.ToInt32(context.Request["empID"]);

            bool isSuccess = true;
            string message = "File Uploaded Successfully!";

            try
            {
                var formData = context.Request.Form; // Get the form object from the current HTTP request.
                var jsonCrop = formData["_crop_"];
                CropData cropData = JsonConvert.DeserializeObject<CropData>(jsonCrop);

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

                int schoolID = userData.CompanyID;

                if (context.Request.Files.Count > 0)
                {
                    HttpFileCollection files = context.Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = files[i];
                        HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                        using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                        using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                        {
                            var userMasterObj = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.sID == empID && w.cType == "1").FirstOrDefault();
                            var userSchoolObj = dbSchool.TEmployees.Where(w => w.SchoolID == schoolID && w.sEmp == empID).FirstOrDefault();

                            if (userMasterObj != null)
                            {
                                string linkOriginalFiles = ComFunction.UploadFileOriginalHttpRequestBase(fileBase, "sb_userprofile", schoolID, userMasterObj.sID);
                                if (!string.IsNullOrEmpty(linkOriginalFiles))
                                {
                                    userMasterObj.sPicture = linkOriginalFiles;

                                    dbMaster.SaveChanges();
                                }
                            }

                            if (userSchoolObj != null)
                            {
                                string linkfiles = "";
                                if (!string.IsNullOrEmpty(cropData.base64))
                                {
                                    // Crop picture
                                    string extraInfo = JsonConvert.SerializeObject(new { schoolId = schoolID, userId = userSchoolObj.sEmp, cropped = new { coordinates = cropData.coordinates }, originalSize = cropData.originalSize });
                                    var byteData = cropData.base64.Split(';')[1].Replace("base64,", "");
                                    linkfiles = ComFunction.UploadProfilePictureFromByteData(byteData, "sb_userprofile", Convert.ToInt32(cropData.coordinates.w * 0.21), schoolID, userSchoolObj.sEmp, extraInfo);
                                }
                                else
                                {
                                    linkfiles = ComFunction.UploadFileHttpRequestBase(fileBase, "sb_userprofile", 150, schoolID, userSchoolObj.sEmp);
                                }

                                if (!string.IsNullOrEmpty(linkfiles))
                                {
                                    userSchoolObj.sPicture = linkfiles;
                                    userSchoolObj.nPicversion = (userSchoolObj.nPicversion ?? 1) + 1;
                                    userSchoolObj.dPicUpdate = DateTime.Now;

                                    dbSchool.SaveChanges();
                                }
                            }

                            // Update memory
                            if (userMasterObj != null && userSchoolObj != null)
                            {
                                UpdateMemory memory = new UpdateMemory(userData.AuthKey,userData.AuthValue);
                                memory.Employee(userSchoolObj, userMasterObj);
                            }
                        }
                    }
                }

                // Crop only no update picture
                if (context.Request.Files.Count == 0 && !string.IsNullOrEmpty(cropData.base64))
                {
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        var userMasterObj = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.sID == empID && w.cType == "1").FirstOrDefault();
                        var userSchoolObj = dbSchool.TEmployees.Where(w => w.SchoolID == schoolID && w.sEmp == empID).FirstOrDefault();
                        if (userSchoolObj != null)
                        {
                            // Crop picture
                            string extraInfo = JsonConvert.SerializeObject(new { schoolId = schoolID, userId = userSchoolObj.sEmp, cropped = new { coordinates = cropData.coordinates }, originalSize = cropData.originalSize });
                            var byteData = cropData.base64.Split(';')[1].Replace("base64,", "");
                            string linkfiles = ComFunction.UploadProfilePictureFromByteData(byteData, "sb_userprofile", Convert.ToInt32(cropData.coordinates.w * 0.21), schoolID, userSchoolObj.sEmp, extraInfo);

                            if (!string.IsNullOrEmpty(linkfiles))
                            {
                                userSchoolObj.sPicture = linkfiles;
                                userSchoolObj.nPicversion = (userSchoolObj.nPicversion ?? 1) + 1;
                                userSchoolObj.dPicUpdate = DateTime.Now;

                                dbSchool.SaveChanges();
                            }
                        }

                        // Update memory
                        if (userMasterObj != null && userSchoolObj != null)
                        {
                            UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                            memory.Employee(userSchoolObj, userMasterObj);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                isSuccess = false;
                message = err.Message;
            }

            var result = new { success = isSuccess, message };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }

        public class Coordinates
        {
            public double x { get; set; }
            public double y { get; set; }
            public double x2 { get; set; }
            public double y2 { get; set; }
            public double w { get; set; }
            public double h { get; set; }
        }
        public class OriginalSize
        {
            public double w { get; set; }
            public double h { get; set; }
        }
        public class CropData
        {
            public Coordinates coordinates { get; set; }
            public OriginalSize originalSize { get; set; }
            public string base64 { get; set; }
        }
    }
}