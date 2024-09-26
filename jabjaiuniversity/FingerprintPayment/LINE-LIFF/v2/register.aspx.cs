using FingerprintPayment.Class;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.LINE_LIFF.v2
{
    public partial class register : System.Web.UI.Page
    {
        protected string SSID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SSID = Request.QueryString["ssid"];
            }
            catch (Exception error)
            {
                string logMessagePattern = @"[LINERegisterPageLoad], [ErrorLine:{2}], [ErrorMessage:{3}]";
                string errorMessage = error.Message;
                string innerExceptionMessage = "";
                while (error.InnerException != null) { innerExceptionMessage += ", " + error.InnerException.Message; error = error.InnerException; }
                string logMessageDebug = string.Format(logMessagePattern, ComFunction.GetLineNumberError(error), errorMessage + ", " + innerExceptionMessage);

                InsertLog(logMessageDebug);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string LINERegister(string userId, string os, string ssid)
        {
            string result = "success";
            string code = "200";
            string message = "";

            int schoolID = 0;
            int studentID = 0;

            try
            {
                string decryptedSSID = EncryptionHelper.Decrypt(ssid);
                string[] ssids = decryptedSSID.Split('-');
                schoolID = Convert.ToInt32(ssids[0]);
                studentID = Convert.ToInt32(ssids[1]);

                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {

                    // Check already exist register user id
                    int c = dbMaster.LINEUsers.Where(w => w.StudentID == studentID && w.LINEUserID == userId && (w.Status != "Unfollow" && w.Status != "Leave")).Count();
                    if (c == 0)
                    {
                        // Check user id in system
                        var userObj = dbMaster.TUsers.Where(w => w.cDel == null && w.sID == studentID).FirstOrDefault();
                        if (userObj != null)
                        {
                            // RichMenuID for student or employee
                            var RichMenuID = "richmenu-dc4c0f43fa200ffbfd64142d5705dd02"; // for student
                            if (userObj.cType == "1")
                            {
                                RichMenuID = "richmenu-54a7dc2d4645ccd760c788ef2093d42f"; // for teacher
                            }

                            //// Clear active user in db
                            //dbMaster.LINEUsers.Where(w => w.LINEUserID == userId && w.Status == "Active").ToList().ForEach(a => a.Status = null);

                            var lu = dbMaster.LINEUsers.Where(w => w.StudentID == studentID && w.LINEUserID == userId).FirstOrDefault();
                            if (lu == null)
                            {
                                dbMaster.LINEUsers.Add(new LINEUser
                                {
                                    StudentID = studentID,
                                    LINEUserID = userId,
                                    RichMenuID = RichMenuID,
                                    SchoolID = schoolID,
                                    RegisterDate = DateTime.Now,
                                    Status = "Active",
                                    OS = os
                                });
                            }
                            else
                            {
                                lu.Status = "Active";
                                lu.RegisterDate = DateTime.Now;
                            }

                            dbMaster.SaveChanges();
                        }
                        else
                        {
                            result = "error";
                            code = "201";
                            message = "User id not in system.";
                        }
                    }
                    else
                    {
                        result = "error";
                        code = "202";
                        message = "Already register user id.";

                        //// Clear active student in db
                        //dbMaster.LINEUsers.Where(w => w.LINEUserID == userId && w.Status == "Active").ToList().ForEach(a => a.Status = null);

                        // Update active user
                        var lu = dbMaster.LINEUsers.Where(w => w.StudentID == studentID && w.LINEUserID == userId).FirstOrDefault();
                        lu.Status = "Active";
                        lu.RegisterDate = DateTime.Now;

                        dbMaster.SaveChanges();
                    }
                }
            }
            catch (Exception error)
            {
                result = "error";
                code = "203";
                message = error.Message;

                string logMessagePattern = @"[LINERegister][LINEUserID:{0}], [OS:{1}], [StudentID:{2}], [ErrorLine:{3}], [ErrorMessage:{4}]";
                string errorMessage = error.Message;
                string innerExceptionMessage = "";
                while (error.InnerException != null) { innerExceptionMessage += ", " + error.InnerException.Message; error = error.InnerException; }
                string logMessageDebug = string.Format(logMessagePattern, userId, os, studentID, ComFunction.GetLineNumberError(error), errorMessage + ", " + innerExceptionMessage);

                InsertLog(logMessageDebug);
            }

            var json = JsonConvert.SerializeObject(new { result, code, message });

            return json;
        }

        [WebMethod(EnableSession = true)]
        public static string ErrorMessage(string userId, string os, string ssid, string message)
        {
            string result = "error";

            try
            {
                string logMessagePattern = @"[LINERegisterError][LINEUserID:{0}], [OS:{1}], [SSID:{2}], [ErrorMessage:{3}]";
                string logMessageDebug = string.Format(logMessagePattern, userId, os, ssid, message);

                InsertLog(logMessageDebug);
            }
            catch (Exception error)
            {
                string logMessagePattern = @"[LINERegisterError][LINEUserID:{0}], [OS:{1}], [SSID:{2}], [ErrorMessage:{3}]";
                string logMessageDebug = string.Format(logMessagePattern, userId, os, ssid, error.Message);

                InsertLog(logMessageDebug);
            }

            var json = JsonConvert.SerializeObject(new { result });

            return json;
        }

        private static void InsertLog(string message)
        {
            //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
            //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
            //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

            //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

            //SqlConnection _conn = new SqlConnection(strconn);
            //fcommon.ExecuteNonQuery(_conn, string.Format("insert into [dbo].[tb_apilog] ([info]) values ('{0}')", message));

            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            {
                mctx.Database.ExecuteSqlCommand(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('{0}')", message));
            }
        }
    }
}