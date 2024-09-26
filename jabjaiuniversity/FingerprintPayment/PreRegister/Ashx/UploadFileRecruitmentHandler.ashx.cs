using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace FingerprintPayment.PreRegister.Ashx
{
    /// <summary>
    /// Summary description for UploadFileRecruitmentHandler
    /// </summary>
    public class UploadFileRecruitmentHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        string SessionPrimaryKey = "REGISTERRECRUITMENT_ID";

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

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

            bool success = true;
            string message = "File Uploaded Successfully!";

            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    var formData = context.Request.Form; // Get the form object from the current HTTP request.

                    int? YearID = string.IsNullOrEmpty(formData["yearID"]) ? (int?)null : int.Parse(formData["yearID"]);
                    int? Year = string.IsNullOrEmpty(formData["year"]) ? (int?)null : int.Parse(formData["year"]);
                    string StudentType = formData["studentType"];
                    int? LevelID = string.IsNullOrEmpty(formData["levelID"]) ? (int?)null : int.Parse(formData["levelID"]);
                    int? PlanID = string.IsNullOrEmpty(formData["planID"]) ? (int?)null : int.Parse(formData["planID"]);
                    int? PaymentGroupID = string.IsNullOrEmpty(formData["paymentGroupID"]) ? (int?)null : int.Parse(formData["paymentGroupID"]);
                    int? StudentMax = string.IsNullOrEmpty(formData["studentMax"]) ? (int?)null : int.Parse(formData["studentMax"]);
                    DateTime? DocumentDate = string.IsNullOrEmpty(formData["documentDate"]) ? (DateTime?)null : DateTime.Parse(formData["documentDate"], new CultureInfo("th-TH"));
                    DateTime? EndDate = string.IsNullOrEmpty(formData["endDate"]) ? (DateTime?)null : DateTime.Parse(formData["endDate"], new CultureInfo("th-TH"));
                    int? Fee = string.IsNullOrEmpty(formData["fee"]) ? (int?)null : int.Parse(formData["fee"]);

                    int? ExamAnnounce = string.IsNullOrEmpty(formData["examAnnounce"]) ? (int?)null : int.Parse(formData["examAnnounce"]);
                    DateTime? MeetingDate = string.IsNullOrEmpty(formData["meetingDate"]) ? (DateTime?)null : DateTime.Parse(formData["meetingDate"], new CultureInfo("th-TH"));
                    string MeetingTime = formData["meetingTime"];
                    string MeetingPlace = formData["meetingPlace"];

                    bool? ActiveBackupPlan = string.IsNullOrEmpty(formData["activeBackupPlan"]) ? (bool?)null : bool.Parse(formData["activeBackupPlan"]);
                    int? OrderPlans = string.IsNullOrEmpty(formData["orderPlans"]) ? (int?)null : int.Parse(formData["orderPlans"]);
                    string BackupPlans = formData["backupPlans"];

                    //int schoolID = Convert.ToInt32(HttpContext.Current.Session["nCompany"]);
                    string linkFilePassExam = null;
                    string linkFileFailExam = null;

                    int ID = 0;

                    if (HttpContext.Current.Session[SessionPrimaryKey] == null)
                    {
                        // Insert Section
                        // int ID = (int)(en.TRegisterSetups.Where(w => w.SchoolID == schoolID).Count() == 0 ? 1 : en.TRegisterSetups.Where(w => w.SchoolID == schoolID).Max(m => m.ID) + 1);

                        // Get Item
                        TRegisterSetup p = new TRegisterSetup
                        {
                            //ID = ID,
                            nYear = YearID,
                            Year = Year,
                            StudentType = StudentType,
                            nTSubLevel = LevelID,
                            RegisterPlanSetupID = PlanID,
                            PaymentGroupID = PaymentGroupID,
                            StudentMax = StudentMax,
                            SubmitDocumentDate = DocumentDate,
                            EndDate = EndDate,
                            Fee = Fee,

                            ExamAnnounce = ExamAnnounce,
                            MeetingDate = MeetingDate,
                            MeetingTime = MeetingTime,
                            MeetingPlace = MeetingPlace,
                            AttachmentsPassExam = linkFilePassExam,
                            AttachmentsFailExam = linkFileFailExam,

                            IsActiveBackupPlan = ActiveBackupPlan,
                            OrderPlans = OrderPlans,
                            BackupPlans = BackupPlans,

                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now
                        };

                        en.TRegisterSetups.Add(p);

                        en.SaveChanges();

                        ID = p.RegisterSetupID;

                        database.InsertLog(userData.UserID.ToString(), "เพิ่มข้อมูลตั้งค่าการรับสมัคร รหัส:" + p.RegisterSetupID, HttpContext.Current.Request, 165, 2, 0, schoolID);
                    }
                    else
                    {
                        // Modify Section
                        ID = Convert.ToInt32(HttpContext.Current.Session[SessionPrimaryKey]);

                        if (HttpContext.Current.Request.Files.Count > 0)
                        {
                            HttpFileCollection files = HttpContext.Current.Request.Files;

                            if (files["filePassExam"] != null)
                            {
                                HttpPostedFile file = files["filePassExam"];
                                HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                                linkFilePassExam = ComFunction.UploadFileHttpRequestBase(fileBase, "preregister/recruitment", schoolID, ID);
                            }

                            if (files["fileFailExam"] != null)
                            {
                                HttpPostedFile file = files["fileFailExam"];
                                HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                                linkFileFailExam = ComFunction.UploadFileHttpRequestBase(fileBase, "preregister/recruitment", schoolID, ID);
                            }
                        }

                        // Get Item
                        TRegisterSetup pi = en.TRegisterSetups.First(f => f.SchoolID == schoolID && f.RegisterSetupID == ID);

                        pi.nYear = YearID;
                        pi.Year = Year;
                        pi.StudentType = StudentType;
                        pi.nTSubLevel = LevelID;
                        pi.RegisterPlanSetupID = PlanID;
                        pi.PaymentGroupID = PaymentGroupID;
                        pi.StudentMax = StudentMax;
                        pi.SubmitDocumentDate = DocumentDate;
                        pi.EndDate = EndDate;
                        pi.Fee = Fee;

                        pi.ExamAnnounce = ExamAnnounce;
                        pi.MeetingDate = MeetingDate;
                        pi.MeetingTime = MeetingTime;
                        pi.MeetingPlace = MeetingPlace;

                        if (!string.IsNullOrEmpty(linkFilePassExam))
                        {
                            pi.AttachmentsPassExam = linkFilePassExam;
                        }
                        if (!string.IsNullOrEmpty(linkFileFailExam))
                        {
                            pi.AttachmentsFailExam = linkFileFailExam;
                        }

                        pi.IsActiveBackupPlan = ActiveBackupPlan;
                        pi.OrderPlans = OrderPlans;
                        pi.BackupPlans = BackupPlans;

                        pi.UpdateDate = DateTime.Now;
                        pi.UpdateBy = userData.UserID;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "อัปเดตข้อมูลตั้งค่าการรับสมัคร รหัส:" + ID, HttpContext.Current.Request, 165, 3, 0, schoolID);
                    }

                    if (HttpContext.Current.Request.Files.Count > 0)
                    {
                        HttpFileCollection files = HttpContext.Current.Request.Files;

                        if (files["filePassExam"] != null)
                        {
                            HttpPostedFile file = files["filePassExam"];
                            HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                            linkFilePassExam = ComFunction.UploadFileHttpRequestBase(fileBase, "preregister/recruitment", schoolID, ID);
                        }

                        if (files["fileFailExam"] != null)
                        {
                            HttpPostedFile file = files["fileFailExam"];
                            HttpPostedFileBase fileBase = new HttpPostedFileWrapper(file);

                            linkFileFailExam = ComFunction.UploadFileHttpRequestBase(fileBase, "preregister/recruitment", schoolID, ID);
                        }
                    }

                }
                catch (Exception error)
                {
                    success = false;
                    message = error.Message;
                }
            }

            var result = new { success, message };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}