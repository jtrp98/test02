using AccountingEntity;
using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Org.BouncyCastle.Crypto.Engines;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Accounting.Invoices;
using QueryEngine = FingerprintPayment.PreRegister.CsCode.QueryEngine;

namespace FingerprintPayment.PreRegister
{
    public partial class preRegisterList2 : PreRegisterGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                // Get current year
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(UserData);
                int currentYearID = 0;
                var term = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm && w.cDel == null).FirstOrDefault();
                if (term != null)
                {
                    currentYearID = term.nYear.Value;
                }

                var listYear = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).OrderByDescending(x => x.numberYear).ToList();
                foreach (var l in listYear)
                {
                    if (l.nYear == currentYearID)
                    {
                        this.ltrYear.Text += string.Format(@"<option selected=""selected"" value=""{0}"">{0}</option>", l.numberYear);
                    }
                    else
                    {
                        this.ltrYear.Text += string.Format(@"<option value=""{0}"">{0}</option>", l.numberYear);
                    }

                    if (currentYearID == 0) currentYearID = l.nYear;
                }

                var listOptionLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listOptionLevel)
                {
                    //this.ltrOptionLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                    string levelName = "";
                    if (l.SubLevel.Contains("ปวช."))
                    {
                        levelName = "ปวช.";
                    }
                    else if (l.SubLevel.Contains("ปวส."))
                    {
                        levelName = "ปวส.";
                    }
                    this.ltrOptionLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"" data-level-name=""{3}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel, levelName);
                }
                this.ltrOptionLevelPaymentStatus.Text = this.ltrOptionLevelMoveMoreStudent.Text = this.ltrOptionLevel.Text;

                string code = string.Format(@"{0}{1}", LettersShuffle, schoolID.ToString().PadLeft(4, '0'));
                lblRegisterLink.Text = string.Format(@"https://{0}/PreRegister/RegisterStart.aspx?id={1}", HttpContext.Current.Request.Url.Host, Class.ComFunction.Rot13Transform(code));
                lblExamResultLink.Text = string.Format(@"https://{0}/PreRegister/RegisterExamResult.aspx?id={1}", HttpContext.Current.Request.Url.Host, Class.ComFunction.Rot13Transform(code));
                lblDocumentResultLink.Text = string.Format(@"https://{0}/PreRegister/RegisterQualifyResult.aspx?id={1}", HttpContext.Current.Request.Url.Host, Class.ComFunction.Rot13Transform(code));
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadRegister()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "rID";
            switch (sortIndex)
            {
                case "2": sortBy = "StudentName"; break;
                case "3": sortBy = "RegisterDate"; break;
                case "4": sortBy = "StudentCode"; break;
                case "5": sortBy = "LevelName"; break;
                case "6": sortBy = "PlanName"; break;
                case "7": sortBy = "ExamRoom"; break;
                case "8": sortBy = "ExamSeatNo"; break;
                case "9": sortBy = "Status"; break;
                case "10": sortBy = "MoveStatus"; break;
                case "11": sortBy = "ExamResults"; break;
                case "12": sortBy = "CompleteDocuments"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string year = Convert.ToString(jsonObject["year"]);
            string regStatus = Convert.ToString(jsonObject["regStatus"]);
            string optLevel = Convert.ToString(jsonObject["optLevel"]);
            string couType = Convert.ToString(jsonObject["couType"]);
            string couTime = Convert.ToString(jsonObject["couTime"]);
            string branch = Convert.ToString(jsonObject["branch"]);
            string stdName = Convert.ToString(jsonObject["stdName"]);
            string plan = Convert.ToString(jsonObject["plan"]);

            var json = QueryEngine.LoadRegisterJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, year, regStatus, optLevel, couType, couTime, branch, stdName, plan);

            return json;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityPlanSetupEduProgram> LoadPlan(string subLevelID)
        {
            List<EntityPlanSetupEduProgram> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

                int slID = Convert.ToInt32(subLevelID);
                result = dbschool.TRegisterPlanSetups.Where(w => w.SchoolID == schoolID && w.nTSubLevel == slID && w.cDel == false).Select(s => new EntityPlanSetupEduProgram { ID = s.RegisterPlanSetupID, Planname = s.PlanName }).ToList();
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

                int slID = Convert.ToInt32(subLevelID);

                result = (from l2 in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolID)
                          join l1 in dbschool.TSubLevels.Where(w => w.SchoolID == schoolID) on l2.nTSubLevel equals l1.nTSubLevel into l1_join
                          from l1 in l1_join.DefaultIfEmpty()
                          where
                            l2.nTSubLevel == slID &&
                            l2.nTermSubLevel2Status == "1" &&
                            l2.nWorkingStatus == 1
                          orderby
                            l2.nTSubLevel2
                          select new EntityDropdown
                          {
                              id = l2.nTermSubLevel2 + "",
                              name = (l1.SubLevel + " / " + l2.nTSubLevel2)
                          }).ToList();
            }

            return result;
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteStudentRegister(int registerID)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TPreRegister p = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();

                    p.cDel = 1;
                    p.UpdatedBy = userData.UserID;
                    p.UpdatedDate = DateTime.Now;

                    en.SaveChanges();
                    List<TPreRegister> registerData = new List<TPreRegister>();
                    registerData.Add(p);

                    Accounting.Tuitionfee.Setting.DeleteInvoicesPreRegister(registerData, userData.UserID);

                    database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลนักเรียน " + p.sName + " " + p.sLastname, HttpContext.Current.Request, 166, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string DeleteMoreStudentRegister(int[] registerIDs)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    var registerData = en.TPreRegisters.Where(w => w.SchoolID == schoolID && registerIDs.Contains(w.preRegisterId)).ToList();
                    foreach (var p in registerData)
                    {
                        p.cDel = 1;
                        p.UpdatedBy = userData.UserID;
                        p.UpdatedDate = DateTime.Now;
                    }

                    en.SaveChanges();

                    Accounting.Tuitionfee.Setting.DeleteInvoicesPreRegister(registerData, userData.UserID);

                    string rIDs = string.Join(",", registerIDs);

                    database.InsertLog(userData.UserID.ToString(), "ลบข้อมูลนักเรียน รหัสลงทะเบียน:" + rIDs, HttpContext.Current.Request, 166, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string GenerateExamSeatNo(int[] registerIDs)
        {
            bool success = true;
            string message = "";
            string errorMessage = "";

            int successCount = 0, failCount = 0;

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    // Generate SeatNo
                    // กรณีตั้งค่าแผนสำรอง รหัสแผนจะไปเอาจาก MainPlan column
                    string query = string.Format(@"
SELECT r.preRegisterId, r.optionLevel 'LevelID', IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID) 'RegisterPlanSetupID', t.titleDescription 'Title', r.sName 'Name', r.sLastname 'Lastname', level.SubLevel 'LevelName', rps.PlanCode, rps.PlanName, rer.ExamRoomName, rps.PlanCode + RIGHT('000' + CAST(ROW_NUMBER() OVER(PARTITION BY r.optionLevel, r.RegisterPlanSetupID ORDER BY r.preRegisterId ASC) + CAST(A.LastRunNoOfGroup AS INT) AS VARCHAR(10)), 3) 'GenerateSeatNo'
FROM TPreRegister r 
LEFT JOIN TRegisterPlanSetup rps ON r.SchoolID=rps.SchoolID AND r.optionLevel=rps.nTSubLevel AND IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID)=rps.RegisterPlanSetupID AND rps.cDel=0
LEFT JOIN 
(
	SELECT r.optionLevel, IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID) 'RegisterPlanSetupID', MAX(CASE WHEN LEFT(r.ExamSeatNo, LEN(rps.PlanCode))=rps.PlanCode THEN CONVERT(INT, SUBSTRING(r.ExamSeatNo, LEN(rps.PlanCode)+1, LEN(r.ExamSeatNo)-LEN(rps.PlanCode))) ELSE 0 END) 'LastRunNoOfGroup'
	FROM TPreRegister r 
    LEFT JOIN TRegisterPlanSetup rps ON r.SchoolID=rps.SchoolID AND r.optionLevel=rps.nTSubLevel AND IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID)=rps.RegisterPlanSetupID AND rps.cDel=0
	WHERE r.SchoolID={0}
	GROUP BY r.optionLevel, IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID)
) A ON A.optionLevel = r.optionLevel AND A.RegisterPlanSetupID = IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID)
LEFT JOIN TTitleList t ON r.SchoolID = t.SchoolID AND r.StudentTitle = t.nTitleid
LEFT JOIN TSubLevel level ON r.optionLevel = level.nTSubLevel AND r.SchoolID = level.SchoolID
LEFT JOIN TRegisterExamRoom rer ON r.SchoolID = rer.SchoolID AND r.RegisterExamRoomID = rer.RegisterExamRoomID
WHERE r.SchoolID={0} AND r.ExamSeatNo IS NULL AND r.preRegisterId IN ({1})
ORDER BY r.optionLevel, IIF(r.RegisterPlanSetupID=0, r.MainPlan, r.RegisterPlanSetupID), r.preRegisterId", schoolID, string.Join(",", registerIDs));
                    List<GenerateSeatNoModel> generateSeatNoModels = en.Database.SqlQuery<GenerateSeatNoModel>(query).ToList();

                    query = string.Format(@"
SELECT rer.RegisterExamRoomID, rer.nTSubLevel 'LevelID', rer.RegisterPlanSetupID, rer.ExamRoomName, rer.Seats, COUNT(r.preRegisterId) 'CountCreatedSeatNo'
FROM TRegisterExamRoom rer 
LEFT JOIN TPreRegister r ON rer.SchoolID = r.SchoolID AND rer.RegisterExamRoomID = r.RegisterExamRoomID
WHERE rer.SchoolID={0} AND rer.IsDel=0
GROUP BY rer.RegisterExamRoomID, rer.nTSubLevel, rer.RegisterPlanSetupID, rer.ExamRoomName, rer.Seats", schoolID);
                    List<CountSeatNoInExamRoomModel> countSeatNoInExamRoomModels = en.Database.SqlQuery<CountSeatNoInExamRoomModel>(query).ToList();

                    // Check & set exam room
                    foreach (var seatNo in generateSeatNoModels)
                    {
                        var examRoomData = countSeatNoInExamRoomModels.Where(w => w.LevelID == seatNo.LevelID && w.RegisterPlanSetupID == seatNo.RegisterPlanSetupID).FirstOrDefault();
                        if (examRoomData != null)
                        {
                            seatNo.ExamRoomName = examRoomData.ExamRoomName;
                        }
                    }

                    string brTag = "";
                    foreach (var seatNo in generateSeatNoModels)
                    {
                        if (string.IsNullOrEmpty(seatNo.PlanCode))
                        {
                            failCount++;

                            errorMessage += string.Format(@"{5}{6}. นักเรียนชื่อ: <b>{0}{1} {2}</b>, สมัครชั้น: <b>{3}</b>, แผน: <b>{4}</b> ไม่สามารถสร้างเลขที่นั่งสอบได้เนื่องจาก<u>ยังไม่ระบุรหัสแผน</u>", seatNo.Title, seatNo.Name, seatNo.Lastname, seatNo.LevelName, seatNo.PlanName, brTag, failCount);

                            brTag = "<br/>";
                        }
                        else if (string.IsNullOrEmpty(seatNo.ExamRoomName))
                        {
                            failCount++;

                            errorMessage += string.Format(@"{5}{6}. นักเรียนชื่อ: <b>{0}{1} {2}</b>, สมัครชั้น: <b>{3}</b>, แผน: <b>{4}</b> ไม่สามารถสร้างเลขที่นั่งสอบได้เนื่องจาก<u>ยังไม่ได้สร้างห้องสอบ</u>", seatNo.Title, seatNo.Name, seatNo.Lastname, seatNo.LevelName, seatNo.PlanName, brTag, failCount);

                            brTag = "<br/>";
                        }
                        else
                        {
                            var seatAvailable = countSeatNoInExamRoomModels.Where(w => w.LevelID == seatNo.LevelID && w.RegisterPlanSetupID == seatNo.RegisterPlanSetupID && w.Seats > w.CountCreatedSeatNo).OrderBy(o => o.RegisterExamRoomID).FirstOrDefault();

                            if (seatAvailable != null)
                            {
                                TPreRegister r = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == seatNo.preRegisterId).FirstOrDefault();

                                r.ExamSeatNo = seatNo.GenerateSeatNo;
                                r.RegisterExamRoomID = seatAvailable.RegisterExamRoomID;

                                r.UpdatedBy = userData.UserID;
                                r.UpdatedDate = DateTime.Now;

                                en.SaveChanges();

                                successCount++;

                                seatAvailable.CountCreatedSeatNo++;
                            }
                            else
                            {
                                failCount++;

                                errorMessage += string.Format(@"{5}{6}. นักเรียนชื่อ: <b>{0}{1} {2}</b>, สมัครชั้น: <b>{3}</b>, แผน: <b>{4}</b> ไม่สามารถสร้างเลขที่นั่งสอบได้เนื่องจาก<u>จำนวนที่นั่งสอบในห้องสอบไม่พอ</u>", seatNo.Title, seatNo.Name, seatNo.Lastname, seatNo.LevelName, seatNo.PlanName, brTag, failCount);

                                brTag = "<br/>";
                            }
                        }
                    }

                    string rIDs = string.Join(",", registerIDs);

                    database.InsertLog(userData.UserID.ToString(), "สร้างเลขที่นั่งสอบ รหัสลงทะเบียน:" + rIDs, HttpContext.Current.Request, 166, 4, 0, schoolID);
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, successCount, failCount, errorMessage };
            return JsonConvert.SerializeObject(result);
        }


        [WebMethod(EnableSession = true)]
        public static string SaveExamResultStudent(int[] registerIDs, string examResult)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    foreach (var registerID in registerIDs)
                    {
                        TPreRegister p = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();

                        var tempexamResult = examResult;
                        if (examResult == "3")
                        {
                            examResult = "1";
                        }
                        p.ExamResults = examResult;
                        p.ExamResultsUpdateBy = userData.UserID;
                        p.ExamResultsUpdateDate = DateTime.Now;


                        #region send email invoice
                        if (tempexamResult == "3") 
                        {
                            using (AccountingDBEntities accountingEntity = Connection.AccountingDBEntities(ConnectionDB.Read))
                            {
                                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                                {
                                    var school = dbMaster.TCompanies.FirstOrDefault(m => m.nCompany == schoolID);
                                    var invoice = (from a in accountingEntity.AccountInvoiceStudent where a.PreRegisterId == p.preRegisterId select a).FirstOrDefault();
                                    if (invoice != null)
                                    {
                                        var baseUrl = "https://accounting.schoolbright.co";
                                       
                                        var emailFrom = "noreply@schoolbright.co";
                                        var mailMessageHtml = ComFunction.GenerateEmailContent("invoice", schoolID
                                            , invoice.StudentName
                                            , invoice.StudentName
                                            , invoice.InvoiceCode
                                            , invoice.Year.ToString()
                                            , $"{baseUrl}/Payment/PrintInvoiceAndPay?schoolId={schoolID}&id={invoice.AccountInvoiceStudentId}"
                                        );
                                        var mailSubject = $@"{school.sCompany} : นำส่งใบเเจ้งค่าธรรมเนียมการศึกษา {invoice.Year} {invoice.StudentName}";

                                        if (!string.IsNullOrEmpty(p.sEmail))
                                        {
                                            ComFunction.SendMail(subject: mailSubject,
                                                                body: mailMessageHtml,
                                                                toEmail: p.sEmail,
                                                                toName: invoice.StudentName);

                                            // Save mail log
                                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                            {
                                                preRegisterId = p.preRegisterId,
                                                SendTo = p.sEmail,
                                                SendFrom = emailFrom,
                                                Title = mailSubject,
                                                Message = mailMessageHtml,
                                                SendDate = DateTime.Now,
                                                SendBy = 0
                                            };
                                            en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                            en.SaveChanges();
                                        }
                                        if (!string.IsNullOrEmpty(p.stayWithEmail))
                                        {
                                            ComFunction.SendMail(subject: mailSubject,
                                                                body: mailMessageHtml,
                                                                toEmail: p.stayWithEmail,
                                                                toName: invoice.StudentName);

                                            // Save mail log
                                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                            {
                                                preRegisterId = p.preRegisterId,
                                                SendTo = p.stayWithEmail,
                                                SendFrom = emailFrom,
                                                Title = mailSubject,
                                                Message = mailMessageHtml,
                                                SendDate = DateTime.Now,
                                                SendBy = 0
                                            };
                                            en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                            en.SaveChanges();
                                        }
                                        if (!string.IsNullOrEmpty(p.FatherEmail))
                                        {
                                            ComFunction.SendMail(subject: mailSubject,
                                                                body: mailMessageHtml,
                                                                toEmail: p.FatherEmail,
                                                                toName: invoice.StudentName);

                                            // Save mail log
                                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                            {
                                                preRegisterId = p.preRegisterId,
                                                SendTo = p.FatherEmail,
                                                SendFrom = emailFrom,
                                                Title = mailSubject,
                                                Message = mailMessageHtml,
                                                SendDate = DateTime.Now,
                                                SendBy = 0
                                            };
                                            en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                            en.SaveChanges();
                                        }
                                        if (!string.IsNullOrEmpty(p.MotherEmail))
                                        {
                                            ComFunction.SendMail(subject: mailSubject,
                                                                body: mailMessageHtml,
                                                                toEmail: p.MotherEmail,
                                                                toName: invoice.StudentName);

                                            // Save mail log
                                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                            {
                                                preRegisterId = p.preRegisterId,
                                                SendTo = p.MotherEmail,
                                                SendFrom = emailFrom,
                                                Title = mailSubject,
                                                Message = mailMessageHtml,
                                                SendDate = DateTime.Now,
                                                SendBy = 0
                                            };
                                            en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                            en.SaveChanges();
                                        }
                                        if (!string.IsNullOrEmpty(p.ParentEmail))
                                        {
                                            ComFunction.SendMail(subject: mailSubject,
                                                                body: mailMessageHtml,
                                                                toEmail: p.ParentEmail,
                                                                toName: invoice.StudentName);

                                            // Save mail log
                                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                            {
                                                preRegisterId = p.preRegisterId,
                                                SendTo = p.ParentEmail,
                                                SendFrom = emailFrom,
                                                Title = mailSubject,
                                                Message = mailMessageHtml,
                                                SendDate = DateTime.Now,
                                                SendBy = 0
                                            };
                                            en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                            en.SaveChanges();
                                        }
                                    }
                                }

                            }
                        }
                        #endregion
                    }

                    en.SaveChanges();

                    string rIDs = string.Join(",", registerIDs);

                    database.InsertLog(userData.UserID.ToString(), "อัพเดทผลสอบ รหัสลงทะเบียน:" + rIDs + ", ผลสอบ:" + examResult, HttpContext.Current.Request, 166, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SaveCompleteDocuments(int[] registerIDs, string completeResult, string notCompleteInfo)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    foreach (var registerID in registerIDs)
                    {
                        TPreRegister p = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();

                        p.CompleteDocuments = completeResult;
                        p.CompleteDocumentsUpdateBy = userData.UserID;
                        p.CompleteDocumentsUpdateDate = DateTime.Now;
                        if (completeResult == "0")
                        {
                            p.CompleteDocumentsInfo = notCompleteInfo;
                        }
                    }

                    en.SaveChanges();

                    string rIDs = string.Join(",", registerIDs);

                    database.InsertLog(userData.UserID.ToString(), "อัพเดทสถานะข้อมูล/เอกสาร รหัสลงทะเบียน:" + rIDs + ", ผลสอบ:" + completeResult + ", หมายเหตุ:" + (string.IsNullOrEmpty(notCompleteInfo) ? "-" : notCompleteInfo), HttpContext.Current.Request, 166, 4, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static string SavePaymentStatus(int registerID, int paymentStatusID)
        {
            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string isComplete = "complete";

                try
                {
                    TPreRegister p = en.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID).FirstOrDefault();

                    p.paymentStatus = paymentStatusID;
                    p.UpdatedBy = userData.UserID;
                    p.UpdatedDate = DateTime.Now;

                    en.SaveChanges();

                    string paymentStatus = "";
                    switch (paymentStatusID)
                    {
                        case 0: paymentStatus = "ยังไม่ชำระค่าสมัคร"; break;
                        case 1: paymentStatus = "ชำระค่าสมัครไม่ครบ"; break;
                        case 2: paymentStatus = "ชำระค่าสมัครแล้ว"; break;
                        case 3: paymentStatus = "ย้ายเข้าห้องเรียน"; break;
                    }

                    database.InsertLog(userData.UserID.ToString(), "อัปเดตสถานะการจ่ายเงิน นักเรียน: " + p.sName + " " + p.sLastname + ", สถานะ: " + paymentStatus, HttpContext.Current.Request, 166, 3, 0, schoolID);
                }
                catch
                {
                    isComplete = "error";
                }

                return isComplete;
            }
        }

        [WebMethod]
        public static object MoveStudent(int registerID, int level2ID, string dateMove, int schoolYear, string studentID, string termID)
        {
            bool success = true;
            string message = "";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;

                DateTime dateMoveObject = DateTime.ParseExact(dateMove, "dd/MM/yyyy", new CultureInfo("th-TH"));

                string res = MoveStudentToRoom(registerID, level2ID, dateMoveObject, schoolYear, studentID, schoolID, userData, termID);
                if (!string.IsNullOrEmpty(res))
                {
                    success = false;
                    message = res;
                }
                else
                {
                    database.InsertLog(userData.UserID.ToString(), "ย้ายเข้าห้องเรียนของนักเรียน รหัสลงทะเบียน:" + registerID, HttpContext.Current.Request, 166, 3, 0, schoolID);
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message };
            return JsonConvert.SerializeObject(result);
        }

        [WebMethod]
        public static object MoveMoreStudent(int[] registerIDs, int level2ID, string dateMove, int schoolYear, string termID)
        {
            bool success = true;
            string message = "";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;

                DateTime dateMoveObject = DateTime.ParseExact(dateMove, "dd/MM/yyyy", new CultureInfo("th-TH"));

                foreach (var registerID in registerIDs)
                {
                    string res = MoveStudentToRoom(registerID, level2ID, dateMoveObject, schoolYear, "", schoolID, userData, termID);
                    if (!string.IsNullOrEmpty(res))
                    {
                        success = false;
                        message += string.Format(@", {0} [{1}]<br/>", registerID, res);
                    }
                }

                string rIDs = string.Join(",", registerIDs);

                database.InsertLog(userData.UserID.ToString(), "ย้ายเข้าห้องเรียนของนักเรียน รหัสลงทะเบียน:" + rIDs, HttpContext.Current.Request, 166, 3, 0, schoolID);
            }
            catch
            {
                success = false;
            }

            if (!string.IsNullOrEmpty(message))
            {
                message = message.Remove(0, 2);
            }

            var result = new { success, message };
            return JsonConvert.SerializeObject(result);
        }

        private static string MoveStudentToRoom(int registerID, int level2ID, DateTime dateMove, int schoolYear, string studentID, int schoolID, JWTToken.userData userData, string termID)
        {
            string res = "";
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                

                    JabjaiEntity.DB.TUser user = null;
            try
            {
                var preRegister = dbSchool.TPreRegisters.Where(w => w.SchoolID == schoolID && w.preRegisterId == registerID && w.cDel == null).FirstOrDefault(); // && w.saveAsSID == null
                if (preRegister != null && preRegister.saveAsSID != null)
                {
                    user = dbSchool.TUser.Where(w => w.sID == preRegister.saveAsSID && w.cDel == null).FirstOrDefault();
                }

                if (preRegister != null && user == null)
                {
                    // Check data
                    if (preRegister.dBirth == null)
                    {
                        throw new Exception("กรุณาระบุข้อมูลวันเกิด!");
                    }

                    int sIDMaster = 0;

                    string sStudentID = (string.IsNullOrEmpty(studentID) ? preRegister.sStudentID : studentID);
                    int existIdentify = dbSchool.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null && (((w.sIdentification ?? "") != "" && (w.sIdentification != "-") && w.sIdentification == preRegister.sIdentification) || ((w.sStudentID ?? "") != "" && (w.sStudentID != "-") && w.sStudentID == sStudentID))).Count();
                    int existBackupCardBarCode = dbSchool.TBackupCards.Where(w => w.SchoolID == schoolID && w.BarCode == sStudentID && w.cDel == false).Count();

                    if (existIdentify == 0 && existBackupCardBarCode == 0)
                    {
                        // Insert new data to TUser(master)
                        var userMaster = new MasterEntity.TUser()
                        {
                            //sID = sIDMaster,
                            sName = preRegister.sName.Trim(),
                            sLastname = preRegister.sLastname.Trim(),
                            sIdentification = preRegister.sIdentification.Replace("-", "").Trim(),
                            cSex = preRegister.cSex,
                            sPhone = preRegister.sPhone,
                            sEmail = preRegister.sEmail,
                            userpassword = preRegister.dBirth.Value.ToString("ddMMyyyy"),
                            PasswordHash = ComFunction.HashSHA1(preRegister.dBirth.Value.ToString("ddMMyyyy")),
                            UseEncryptPassword = false,
                            dCreate = DateTime.Now.FixSecondAndMillisecond(1, 166),
                            cType = "0", // 0 = Student, 1 = Employee
                            nCompany = schoolID,
                            //nSystemID = preRegister.saveAsSID,
                            dBirth = preRegister.dBirth,
                            username = sStudentID
                        };
                        dbMaster.TUsers.Add(userMaster);
                        dbMaster.SaveChanges();

                        sIDMaster = userMaster.sID;
                        userMaster.nSystemID = sIDMaster;
                        dbMaster.SaveChanges();


                        // Update TPreRegister Table
                        preRegister.moveInDate = dateMove;
                        preRegister.nTermSubLevel2 = level2ID;
                        preRegister.saveAsSID = sIDMaster;
                        dbSchool.SaveChanges();

                        // Insert new data to TUser(school)
                        var userSchool = new JabjaiEntity.DB.TUser()
                        {
                            moveInDate = dateMove,
                            baseSalary = 0,
                            cDel = null,
                            cSex = preRegister.cSex,
                            cSMS = "1",
                            cTelSMS = preRegister.sPhone,
                            cType = "0",
                            DayQuit = null,
                            dBirth = preRegister.dBirth,
                            dPicUpdate = DateTime.Now.FixSecondAndMillisecond(1, 166),
                            dUpdate = null,
                            nMax = 0,
                            JourneyType = string.IsNullOrEmpty(preRegister.StudentCategory) ? (int?)null : int.Parse(preRegister.StudentCategory),
                            nMoney = 0,
                            Note = null,
                            nPicversion = preRegister.nPicversion,
                            nSonNumber = preRegister.nSonNumber,
                            nStudentNumber = null,
                            nStudentStatus = null,
                            nTermSubLevel2 = level2ID,
                            oldSchoolAumpher = preRegister.oldSchoolAumpher,
                            oldSchoolGraduated = preRegister.oldSchoolGraduated,
                            oldSchoolName = preRegister.oldSchoolName,
                            oldSchoolProvince = preRegister.oldSchoolProvince,
                            oldSchoolTumbon = preRegister.oldSchoolTumbon,
                            oldSchoolGPA2 = preRegister.oldSchoolGPA?.ToString(),
                            sAddress = preRegister.sAddress,
                            sCity = null,
                            sCountry = null,
                            sEmail = preRegister.sEmail,
                            sFinger = null,
                            sFinger2 = null,
                            sID = sIDMaster,
                            sIdentification = preRegister.sIdentification.Replace("-", "").Trim(),
                            sLastname = preRegister.sLastname.Trim(),
                            sName = preRegister.sName.Trim(),
                            sNickName = preRegister.sNickName?.Trim(),
                            sPassword = preRegister.dBirth.Value.ToString("ddMMyyyy"),
                            sPhone = preRegister.sPhone,
                            sPostalcode = null,
                            sStudentAumpher = preRegister.sStudentAumpher,
                            sStudentHomeNumber = preRegister.sStudentHomeNumber,
                            sStudentID = sStudentID,
                            sStudentIdCardNumber = preRegister.sStudentIdCardNumber,
                            sStudentLastEN = preRegister.sStudentLastEN,
                            sStudentMuu = preRegister.sStudentMuu,
                            sStudentNameEN = preRegister.sStudentNameEN,
                            sStudentNation = preRegister.sStudentNation,
                            sStudentPicture = preRegister.sStudentPicture,
                            sStudentPost = preRegister.sStudentPost,
                            sStudentProvince = preRegister.sStudentProvince,
                            sStudentRace = preRegister.sStudentRace,
                            sStudentReligion = preRegister.sStudentReligion,
                            sStudentRoad = preRegister.sStudentRoad,
                            sStudentSoy = preRegister.sStudentSoy,
                            sStudentTitle = preRegister.StudentTitle?.ToString(),
                            sStudentTumbon = preRegister.sStudentTumbon,
                            sSubtopic = null,
                            sToken = null,
                            addressLat = preRegister.addressLat,
                            addressLng = preRegister.addressLng,
                            moveOutReason = preRegister.moveOutReason,
                            sNickNameEN = preRegister.sNickNameEN,
                            sStudentHomeRegisterCode = preRegister.sStudentHomeRegisterCode,
                            sStudentHousePhone = preRegister.sStudentHousePhone,
                            sStudentLastOther = preRegister.sStudentLastOther,
                            sStudentNameOther = preRegister.sStudentNameOther,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                        };

                        dbSchool.TUser.Add(userSchool);
                        dbSchool.SaveChanges();

                        // Insert data to TStudentClassroomHistory
                        //if (schoolYear == 0)
                        //{
                        //    StudentLogic studentLogic = new StudentLogic(dbSchool);

                        //    string termID = studentLogic.GetTermId(dateMove, userData);

                        //    var termData = dbSchool.TTerms.Where(w => w.nTerm == termID && w.SchoolID == schoolID).FirstOrDefault();
                        //    if (termData != null)
                        //    {
                        //        schoolYear = termData.nYear == null ? 0 : termData.nYear.Value;
                        //    }
                        //    else
                        //    {
                        //        var nextTermList = dbSchool.TTerms.Where(w => w.SchoolID == schoolID && w.dStart > dateMove).ToList();
                        //        if (nextTermList.Count() > 0)
                        //        {
                        //            var nextTermList2 = nextTermList.OrderBy(x => x.dStart).ToList();
                        //            var nextTermData = nextTermList2.First();
                        //            schoolYear = nextTermData.nYear == null ? 0 : nextTermData.nYear.Value;
                        //        }
                        //    }
                        //}

                        // Manage Term
                        if (termID == "0" || termID == "null")
                        {
                            // Insert Every Term
                            foreach (var termData in dbSchool.TTerms.Where(w => w.SchoolID == schoolID && w.nYear == schoolYear && w.cDel == null).ToList())
                            {
                                var studentClassRoom = new TStudentClassroomHistory
                                {
                                    sID = preRegister.saveAsSID,
                                    nTerm = termData.nTerm,
                                    nTermSubLevel2 = level2ID,
                                    SchoolID = schoolID,
                                    CreatedBy = userData.UserID,
                                    CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                                };

                                dbSchool.TStudentClassroomHistories.Add(studentClassRoom);
                                dbSchool.SaveChanges();
                            }
                        }
                        else
                        {
                            // Insert Select Term
                            var studentClassRoom = new TStudentClassroomHistory
                            {
                                sID = preRegister.saveAsSID,
                                nTerm = termID,
                                nTermSubLevel2 = level2ID,
                                SchoolID = schoolID,
                                CreatedBy = userData.UserID,
                                CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                            };

                            dbSchool.TStudentClassroomHistories.Add(studentClassRoom);
                            dbSchool.SaveChanges();
                        }

                        // Insert data to FamilyProfile
                        //int familyID = (int)(dbSchool.TFamilyProfiles.Count() == 0 ? 1 : dbSchool.TFamilyProfiles.Max(m => m.nFamilyID) + 1);

                        double? fatherIncome = null;
                        if (preRegister.nFatherIncome == null)
                        {
                            switch (preRegister.fatherIncome)
                            {
                                case "ต่ำกว่า 9,000": fatherIncome = 9000; break;
                                case "9,000 - 12,000": fatherIncome = 12000; break;
                                case "12,000 - 15,000": fatherIncome = 15000; break;
                                case "15,000 - 18,000": fatherIncome = 18000; break;
                                case "18,000 - 21,000": fatherIncome = 21000; break;
                                case "มากกว่า 21,000": fatherIncome = 24000; break;
                            }
                        }
                        else fatherIncome = preRegister.nFatherIncome;

                        double? motherIncome = null;
                        if (preRegister.nMotherIncome == null)
                        {
                            switch (preRegister.fatherIncome)
                            {
                                case "ต่ำกว่า 9,000": motherIncome = 9000; break;
                                case "9,000 - 12,000": motherIncome = 12000; break;
                                case "12,000 - 15,000": motherIncome = 15000; break;
                                case "15,000 - 18,000": motherIncome = 18000; break;
                                case "18,000 - 21,000": motherIncome = 21000; break;
                                case "มากกว่า 21,000": motherIncome = 24000; break;
                            }
                        }
                        else motherIncome = preRegister.nMotherIncome;

                        var familyProfile = new TFamilyProfile
                        {
                            //nFamilyID = familyID,
                            sID = sIDMaster,
                            sDeleted = "false",
                            sFamilyTitle = preRegister.nFamilyTitle?.ToString(),
                            sFamilyName = preRegister.sFamilyName,
                            sFamilyLast = preRegister.sFamilyLast,
                            sFamilyRace = preRegister.sFamilyRace,
                            sFamilyNation = preRegister.sFamilyNation,
                            sFamilyReligion = preRegister.sFamilyReligion,
                            sFamilyIdCardNumber = preRegister.sFamilyIdCardNumber,
                            sFamilyRelate = preRegister.sFamilyRelate,
                            sFamilyHomeNumber = preRegister.sFamilyHomeNumber,
                            sFamilySoy = preRegister.sFamilySoy,
                            sFamilyTumbon = preRegister.sFamilyTumbon,
                            sFamilyProvince = preRegister.sFamilyProvince,
                            sPhoneOne = preRegister.sPhoneOne,
                            sPhoneTwo = preRegister.sPhoneTwo,
                            sPhoneThree = preRegister.sPhoneThree,
                            sPhoneMail = preRegister.sPhoneMail,
                            sFamilyMuu = preRegister.sFamilyMuu,
                            sFamilyRoad = preRegister.sFamilyRoad,
                            sFamilyAumpher = preRegister.sFamilyAumpher,
                            sFamilyPost = preRegister.sFamilyPost,
                            sFatherTitle = preRegister.FatherTitle?.ToString(),
                            sFatherFirstName = preRegister.sFatherFirstName,
                            sFatherLastName = preRegister.sFatherLastName,
                            sFatherIdCardNumber = preRegister.sFatherIdCardNumber,
                            sFatherNation = preRegister.sFatherNation,
                            sFatherRace = preRegister.sFatherRace,
                            sFatherReligion = preRegister.sFatherReligion,
                            sMotherTitle = preRegister.MotherTitle?.ToString(),
                            sMotherFirstName = preRegister.sMotherFirstName,
                            sMotherIdCardNumber = preRegister.sMotherIdCardNumber,
                            sMotherLastName = preRegister.sMotherLastName,
                            sMotherNation = preRegister.sMotherNation,
                            sMotherRace = preRegister.sMotherRace,
                            sMotherReligion = preRegister.sMotherReligion,
                            sFatherAumpher = preRegister.sFatherAumpher,
                            sFatherHomeNumber = preRegister.sFatherHomeNumber,
                            sFatherMuu = preRegister.sFatherMuu,
                            sFatherPhone = preRegister.sFatherPhone,
                            sFatherPost = preRegister.sFatherPost,
                            sFatherProvince = preRegister.sFatherProvince,
                            sFatherRoad = preRegister.sFatherRoad,
                            sFatherSoy = preRegister.sFatherSoy,
                            sFatherTumbon = preRegister.sFatherTumbon,
                            sMotherAumpher = preRegister.sMotherAumpher,
                            sMotherHomeNumber = preRegister.sMotherHomeNumber,
                            sMotherMuu = preRegister.sMotherMuu,
                            sMotherPhone = preRegister.sMotherPhone,
                            sMotherPost = preRegister.sMotherPost,
                            sMotherProvince = preRegister.sMotherProvince,
                            sMotherRoad = preRegister.sMotherRoad,
                            sMotherSoy = preRegister.sMotherSoy,
                            sMotherTumbon = preRegister.sMotherTumbon,
                            bornFrom = preRegister.bornFrom,
                            bornFromAumpher = preRegister.bornFromAumpher,
                            bornFromProvince = preRegister.bornFromProvince,
                            bornFromTumbon = preRegister.bornFromTumbon,
                            dFamilyBirthDay = preRegister.dFamilyBirthDay,
                            dFatherBirthDay = preRegister.dFatherBirthDay,
                            dMotherBirthDay = preRegister.dMotherBirthDay,
                            friendSID = preRegister.friendSID,
                            HomeType = preRegister.HomeType,
                            houseRegistrationAumpher = preRegister.houseRegistrationAumpher,
                            houseRegistrationMuu = preRegister.houseRegistrationMuu,
                            houseRegistrationNumber = preRegister.houseRegistrationNumber,
                            houseRegistrationPhone = preRegister.houseRegistrationPhone,
                            houseRegistrationPost = preRegister.houseRegistrationPost,
                            houseRegistrationProvince = preRegister.houseRegistrationProvince,
                            houseRegistrationRoad = preRegister.houseRegistrationRoad,
                            houseRegistrationSoy = preRegister.houseRegistrationSoy,
                            houseRegistrationTumbon = preRegister.houseRegistrationTumbon,
                            nFamilyIncome = preRegister.nFamilyIncome,
                            nFamilyRequestStudyMoney = preRegister.nFamilyRequestStudyMoney,
                            nFatherIncome = fatherIncome,
                            nMotherIncome = motherIncome,
                            nRelativeStudyHere = preRegister.nRelativeStudyHere,
                            nSonTotal = preRegister.nSonTotal,
                            sFamilyGraduated = preRegister.sFamilyGraduated,
                            sFamilyJob = preRegister.sFamilyJob,
                            sFamilyLastEN = preRegister.sFamilyLastEN,
                            sFamilyNameEN = preRegister.sFamilyNameEN,
                            sFamilyWorkPlace = preRegister.sFamilyWorkPlace,
                            sFatherGraduated = preRegister.sFatherGraduated,
                            sFatherJob = preRegister.sFatherJob,
                            sFatherLastEN = preRegister.sFatherLastEN,
                            sFatherNameEN = preRegister.sFatherNameEN,
                            sFatherPhone2 = preRegister.sFatherPhone2,
                            sFatherPhone3 = preRegister.sFatherPhone3,
                            sFatherWorkPlace = preRegister.sFatherWorkPlace,
                            sMotherGraduated = preRegister.sMotherGraduated,
                            sMotherJob = preRegister.sMotherJob,
                            sMotherLastEN = preRegister.sMotherLastEN,
                            sMotherNameEN = preRegister.sMotherNameEN,
                            sMotherPhone2 = preRegister.sMotherPhone2,
                            sMotherPhone3 = preRegister.sMotherPhone3,
                            sMotherWorkPlace = preRegister.sMotherWorkPlace,
                            stayWithEmail = preRegister.stayWithEmail,
                            stayWithEmergencyCall = preRegister.stayWithEmergencyCall,
                            stayWithLast = preRegister.stayWithLast,
                            stayWithName = preRegister.stayWithName,
                            stayWithTitle = preRegister.stayWithTitle,
                            familyStatus = preRegister.familyStatus,
                            friendLastName = preRegister.friendLastName,
                            friendName = preRegister.friendName,
                            friendPhone = preRegister.friendPhone,
                            friendSubLevel = preRegister.friendSubLevel,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                        };

                        dbSchool.TFamilyProfiles.Add(familyProfile);
                        dbSchool.SaveChanges();

                        // Insert data to TStudentHealthInfo
                        int healthID = (int)(dbSchool.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID).Count() == 0 ? 1 : dbSchool.TStudentHealthInfoes.Where(w => w.SchoolID == schoolID).Max(m => m.nHealthID) + 1);

                        TStudentHealthInfo health = new TStudentHealthInfo
                        {
                            nHealthID = healthID,
                            sID = sIDMaster,
                            sDeleted = "false",
                            sBlood = preRegister.sBlood,
                            sSickFood = preRegister.sSickFood,
                            sSickDrug = preRegister.sSickDrug,
                            sSickOther = preRegister.sSickOther,
                            sSickNormal = preRegister.sSickNormal,
                            sSickDanger = preRegister.sSickDanger,
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                        };
                        dbSchool.TStudentHealthInfoes.Add(health);
                        dbSchool.SaveChanges();

                        //healthID = health.StudentHealthID;

                        // Insert data to TStudentHealthGrowth
                        var nTSubLevel = 0;
                        var termSubLevel2 = dbSchool.TTermSubLevel2.Where(w => w.SchoolID == schoolID && w.nTermSubLevel2 == preRegister.nTermSubLevel2).FirstOrDefault();
                        if (termSubLevel2 != null)
                        {
                            nTSubLevel = termSubLevel2.nTSubLevel;
                        }

                        var nMonth = DateTime.Now.Month;
                        if (5 <= nMonth && nMonth < 8) nMonth = 5;
                        else if (8 <= nMonth && nMonth < 11) nMonth = 8;
                        else if ((11 <= nMonth && nMonth <= 12) && (1 <= nMonth && nMonth < 2)) nMonth = 8;
                        else nMonth = 2;

                        TStudentHealthGrowth healthGrowth = new TStudentHealthGrowth
                        {
                            nHealthID = healthID,
                            nTSubLevel = nTSubLevel,
                            nMonth = nMonth,
                            Weight = Convert.ToDecimal(preRegister.nWeight == null ? 0 : preRegister.nWeight),
                            Height = Convert.ToDecimal(preRegister.nHeight == null ? 0 : preRegister.nHeight),
                            SchoolID = schoolID,
                            CreatedBy = userData.UserID,
                            CreatedDate = DateTime.Now.FixSecondAndMillisecond(1, 166)
                        };
                        dbSchool.TStudentHealthGrowths.Add(healthGrowth);
                        dbSchool.SaveChanges();



                        try
                        {
                            JabjaiMainClass.Autocompletes.TopupMoney.AddOrModify(schoolID, sIDMaster + "", "0", "");
                            UpdateMemory memory = new UpdateMemory(userData.AuthKey, userData.AuthValue);
                            memory.Student(userSchool, userMaster);
                        }
                        catch { }

                        #region Update Invoice 
                        using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities(ConnectionDB.Read))
                        {


                            var sql_invoices = $@"select AccountInvoiceStudentId, a.AccountInvoiceId, b.Year, b.Term, a.TermId, b.StudentId 
                                    from AccountingDB.dbo.AccountInvoice a 
                                    inner join AccountingDB.dbo.AccountInvoiceStudent b on a.AccountInvoiceId = b.AccountInvoiceId
                                    where a.schoolId={userData.CompanyID} and b.PreRegisterId={registerID}";
                            var q_update = peakengineEntities.Database.SqlQuery<AccountingInvoice>(sql_invoices).AsQueryable().ToList();
                            if (q_update.Any())
                            {
                                var f_student = dbSchool.TB_StudentViews.Where(x => x.sID == preRegister.saveAsSID).AsQueryable().FirstOrDefault();
                                if (f_student != null)
                                {
                                    foreach (var invoiceData in q_update)
                                    {
                                        var sql_update_student = $@"update AccountingDB.dbo.AccountInvoiceStudent set                               
                                        LevelName='{f_student.SubLevel}',
                                        ClassName='{f_student.SubLevel} / {f_student.nTSubLevel2}',
                                        ClassId={f_student.nTermSubLevel2},
                                        StudentCode='{f_student.sStudentID}',
                                        StudentId={f_student.sID},
                                        Term='{f_student.sTerm}',                                      
                                        Year={preRegister.registerYear + 543},
                                        NewRegisterInProgress=null
                                        where AccountInvoiceStudentId={invoiceData.AccountInvoiceStudentId}";
                                        peakengineEntities.Database.ExecuteSqlCommand(sql_update_student);


                                        var sql_update_invoice = $@"update AccountingDB.dbo.AccountInvoice set                                          
                                        Term='{f_student.sTerm}',
                                        TermId='{f_student.nTerm}',
                                        Year={preRegister.registerYear + 543},
                                        LevelName='{f_student.SubLevel}',
                                        LevelId={f_student.nTSubLevel}
                                        where AccountInvoiceId={invoiceData.AccountInvoiceId}";
                                        peakengineEntities.Database.ExecuteSqlCommand(sql_update_invoice);
                                    }
                                }
                            }
                        }
                        #endregion

                    } // if (existIdentify == 0 && existBackupCardBarCode == 0)
                    else
                    {
                        res = "รหัสบัตรประชาชนหรือรหัสนักเรียนซ้ำ (Duplicate ID Card Number or duplicate Student ID).";
                    }
                } // if (preRegister != null)
                else
                {
                    if (preRegister == null)
                    {
                        res = "ไม่พบข้อมูลนักเรียน (No data found).";
                    }
                    else if (user != null)
                    {
                        res = string.Format(@"ไม่สามารถย้ายได้ เนื่องจากพบข้อมูลนักเรียนรายนี้ในระบบแล้ว[รหัสนักเรียน: {0}, รหัสบัตรประชาชน: {1}].", user.sStudentID, user.sIdentification);
                    }
                }
            }
            catch (Exception err)
            {

                res = err.Message;
            }

            // End Transaction

            return res;
        }
        }

        [WebMethod]
        public static string[] GetStudentName(string keyword)
        {
            int schoolID = GetUserData().CompanyID;
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));

            string sqlQuery = string.Format(@"
SELECT TOP 20 sName+' '+sLastname
FROM TPreRegister
WHERE cDel IS NULL AND (sName <> '' OR sLastname <> '') AND (sName LIKE N'%{0}%' OR sLastname LIKE N'%{0}%') AND SchoolID = {1}
GROUP BY sName, sLastname
ORDER BY sName, sLastname", keyword, schoolID);
            List<string> result = dbschool.Database.SqlQuery<string>(sqlQuery).ToList();

            return result.ToArray();
        }

        [WebMethod]
        public static object GetSchoolYear(string dateMove)
        {
            string result = "";
            string message = "";
            object data = null;

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;

                DateTime dateMoveObject = DateTime.ParseExact(dateMove, "dd/MM/yyyy", new CultureInfo("th-TH"));
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int? yearID = 0;
                    int? year = 0;
                    string currentTermID = "";
                    List<object> listTerm = new List<object>();

                    var termData = dbschool.TTerms.Where(w => w.dStart <= dateMoveObject && w.dEnd >= dateMoveObject && w.SchoolID == schoolID && w.cDel == null).FirstOrDefault();
                    if (termData != null)
                    {
                        yearID = termData.nYear;
                        currentTermID = termData.nTerm;

                        // List all term in year
                        var lt = dbschool.TTerms.Where(w => w.nYear == yearID && w.SchoolID == schoolID && w.cDel == null).ToList();
                        foreach (var t in lt)
                        {
                            listTerm.Add(new { termID = t.nTerm, term = t.sTerm });
                        }

                        // Year data
                        var yearData = dbschool.TYears.Where(w => w.nYear == yearID && w.SchoolID == schoolID && w.cDel == false).FirstOrDefault();
                        if (yearData != null)
                        {
                            year = yearData.numberYear;
                        }

                        data = new { yearID, year, cTermID = currentTermID, terms = listTerm };

                        result = "success";
                    }
                    else
                    {
                        result = "warning";
                        message = "ไม่พบข้อมูลเทอมในช่วงเวลาที่ระบุ";
                    }
                }
            }
            catch (Exception error)
            {
                result = "error";
                message = error.Message;
            }

            return JsonConvert.SerializeObject(new { result, message, data });
        }

        [WebMethod]
        public static object ListAttachFile(int registerID)
        {
            bool success = true;
            string message = "";

            List<AttachFileData> data = new List<AttachFileData>();

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string query = string.Format(@"SELECT rfi.VFIID, rfi.No, rfi.FieldName, rfi.FieldNameEn, rd.FileName, rd.ContentType, rd.FilePath
FROM TPreRegisterRequiredFieldInitiate rfi LEFT JOIN TPreRegisterDocument rd ON rfi.VFIID = rd.VFIID AND rd.preRegisterId = {0} AND rd.FilePath <> ''
WHERE rfi.CategoryID = 9 AND rfi.IsDel = 0 {1}{2}
ORDER BY rfi.[Order]", registerID, (schoolID != 229 ? " AND rfi.VFIID NOT IN (18, 19)" : ""), (schoolID != 1043 ? " AND rfi.VFIID NOT IN (166, 167, 168)" : ""));
                    data = en.Database.SqlQuery<AttachFileData>(query).ToList();
                }

                // Only SchoolID = 1043
                if (schoolID == 1043)
                {
                    var onlyVFI1043 = new int[] { 14, 15, 16, 17 };
                    var onlyList1043 = data.Where(w => onlyVFI1043.Contains(w.VFIID)).ToList();
                    foreach (var l in onlyList1043)
                    {
                        l.No = (Convert.ToInt32(l.No) + 3).ToString();
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, data };
            return JsonConvert.SerializeObject(result);
        }

        public class AttachFileData
        {
            [JsonProperty(PropertyName = "vfiId")]
            public int VFIID { get; set; }

            [JsonProperty(PropertyName = "no")]
            public string No { get; set; }

            [JsonProperty(PropertyName = "fieldName")]
            public string FieldName { get; set; }

            [JsonProperty(PropertyName = "fieldNameEn")]
            public string FieldNameEn { get; set; }

            [JsonProperty(PropertyName = "fileName")]
            public string FileName { get; set; }

            [JsonProperty(PropertyName = "contentType")]
            public string ContentType { get; set; }

            [JsonProperty(PropertyName = "filePath")]
            public string FilePath { get; set; }
        }

        public class GenerateSeatNoModel
        {
            [JsonProperty(PropertyName = "preRegisterId")]
            public int preRegisterId { get; set; }

            [JsonProperty(PropertyName = "levelId")]
            public int LevelID { get; set; }

            [JsonProperty(PropertyName = "registerPlanSetupID")]
            public int RegisterPlanSetupID { get; set; }

            [JsonProperty(PropertyName = "generateSeatNo")]
            public string GenerateSeatNo { get; set; }

            [JsonProperty(PropertyName = "planCode")]
            public string PlanCode { get; set; }

            [JsonProperty(PropertyName = "planName")]
            public string PlanName { get; set; }

            [JsonProperty(PropertyName = "examRoomName")]
            public string ExamRoomName { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "levelName")]
            public string LevelName { get; set; }
        }

        public class CountSeatNoInExamRoomModel
        {
            [JsonProperty(PropertyName = "registerExamRoomId")]
            public int RegisterExamRoomID { get; set; }

            [JsonProperty(PropertyName = "levelId")]
            public int LevelID { get; set; }

            [JsonProperty(PropertyName = "registerPlanSetupId")]
            public int RegisterPlanSetupID { get; set; }

            [JsonProperty(PropertyName = "examRoomName")]
            public string ExamRoomName { get; set; }

            [JsonProperty(PropertyName = "seats")]
            public int Seats { get; set; }

            [JsonProperty(PropertyName = "countCreatedSeatNo")]
            public int CountCreatedSeatNo { get; set; }
        }

    }
}