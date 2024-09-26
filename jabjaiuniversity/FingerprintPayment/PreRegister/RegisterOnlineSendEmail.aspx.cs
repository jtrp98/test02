using FingerprintPayment.Class;
using FingerprintPayment.PreRegister.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PreRegister
{
    public partial class RegisterOnlineSendEmail : PreRegisterGateway
    {
        protected bool DISABLEDSENDEMAIL = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                ltrMailDate.Text = DateTime.Now.ToString("ddd dd MMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));

                var registerId = Convert.ToInt32(Request.QueryString["id"]);
                var registerObj = ctx.TPreRegisters.Where(w => w.preRegisterId == registerId).FirstOrDefault();
                if (registerObj != null)
                {
                    var titleObj = ctx.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == registerObj.StudentTitle).FirstOrDefault();

                    ltrTo.Text = registerObj.sEmail;
                    ltrTitle.Text = $@"ระบบรับสมัครนักเรียนออนไลน์ ""{titleObj?.titleDescription}{registerObj.sName} {registerObj.sLastname}""";

                    // Get Request missing files
                    var query = $@"
SELECT rfi.FieldName 
FROM TPreRegisterRequiredField rf LEFT JOIN TPreRegisterRequiredFieldInitiate rfi ON rf.CategoryID = rfi.CategoryID AND rf.VFIID = rfi.VFIID
WHERE rf.SchoolID={schoolID} AND rf.CategoryID=9 AND rf.Status=1 AND rf.VFIID NOT IN (SELECT VFIID FROM TPreRegisterDocument WHERE SchoolID={schoolID} AND preRegisterId={registerId})";
                    var documentRequiredField = ctx.Database.SqlQuery<string>(query).ToList();

                    string code = string.Format(@"{0}{1}", LettersShuffle, schoolID.ToString().PadLeft(4, '0'));
                    string registerLink = string.Format(@"https://{0}/PreRegister/RegisterStart.aspx?id={1}&f=sendmail&rid={2}", HttpContext.Current.Request.Url.Host, ComFunction.Rot13Transform(code), registerId);

                    var idx = 1;
                    var documents = "";
                    foreach (var d in documentRequiredField)
                    {
                        documents += string.Format(@"{0}{0}{2}. {3}{1}", "\t", Environment.NewLine, idx++, d);
                    }
                    if (!string.IsNullOrEmpty(documents))
                    {
                        //DISABLEDSENDEMAIL = true;
                        //documents += string.Format(@"{0}{0}{2}{1}", "\t", Environment.NewLine, "** นักเรียนรายนี้ส่งไฟล์เอกสารที่จำเป็นครบแล้ว **");

                        ltrMessage.Text = string.Format(@"{0}{1}เรียน {2}{0}{0}{1}{1}ข้อเอกสารเพิ่มเติม ดังนี้{0}{3}{0}{0}{1}{1}กรุณากรอกข้อมูลเพิ่มเติม {4}", Environment.NewLine, "\t", registerObj.sName + " " + registerObj.sLastname, documents, registerLink);
                    }
                    else
                    {
                        ltrMessage.Text = string.Format(@"{0}{1}เรียน {2}{0}{0}{1}{1}กรุณากรอกข้อมูลเพิ่มเติม {3}", Environment.NewLine, "\t", registerObj.sName + " " + registerObj.sLastname, registerLink);
                    }

                    if (registerObj.CompleteDocuments != "1")
                    {
                        DISABLEDSENDEMAIL = false;
                    }
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SaveMail(int registerId, string mailFrom, string mailMessage)
        {
            bool success = true;
            string code = "200";
            string message = "Success to send email.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int progressStep = 0; // 1: Pre send mail, 2: Complete send mail, 3: Complete save mail log
                try
                {
                    // Get data register online
                    var registerObj = ctx.TPreRegisters.Where(w => w.preRegisterId == registerId).FirstOrDefault();
                    if (registerObj != null)
                    {
                        string email = registerObj.sEmail.Trim(); // nuriyanee.codex@gmail.com // meen21@gmail.com
                        if (!string.IsNullOrEmpty(email))
                        {
                            var titleObj = ctx.TTitleLists.Where(w => w.SchoolID == schoolID && w.nTitleid == registerObj.StudentTitle).FirstOrDefault();
                            progressStep++;

                            // Send mail
                            var mailMessageHtml = HttpUtility.HtmlAttributeEncode(mailMessage);
                            mailMessageHtml = mailMessageHtml.Replace("\n", "<br/>");
                            mailMessageHtml = mailMessageHtml.Replace("\t", "&emsp;");

                            // Replace hyperlink
                            mailMessageHtml = Regex.Replace(mailMessageHtml, @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)", "<a target='_blank' href='$1'>$1</a>");

                            ComFunction.SendMail(subject: $@"ระบบรับสมัครนักเรียนออนไลน์ ""{titleObj?.titleDescription}{registerObj.sName} {registerObj.sLastname}""", body: mailMessageHtml, fromName: mailFrom, toEmail: email, toName: $@"{titleObj?.titleDescription}{registerObj.sName} {registerObj.sLastname}");
                            progressStep++;

                            // Save mail log
                            TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                            {
                                preRegisterId = registerId,
                                SendTo = email,
                                SendFrom = mailFrom,
                                Title = $@"ระบบรับสมัครนักเรียนออนไลน์ ""{titleObj?.titleDescription}{registerObj.sName} {registerObj.sLastname}""",
                                Message = mailMessage,
                                SendDate = DateTime.Now,
                                SendBy = userData.UserID
                            };
                            ctx.TPreRegisterSendMails.Add(preRegisterSendMail);
                            ctx.SaveChanges();

                            progressStep++;
                        }
                        else
                        {
                            success = false;
                            code = "102";
                            message = "Email not found in online registration data";
                        }
                    }
                    else
                    {
                        success = false;
                        code = "101";
                        message = "Online registration data not found.";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    code = "500";
                    switch (progressStep)
                    {
                        case 1: message = $"Failed to send email. [{err.Message}]"; break;
                        case 2: message = $"Unable to record mailing log. [{err.Message}]"; break;
                        default: message = err.Message; break;
                    }
                }
            }

            return JsonConvert.SerializeObject(new { success, code, message });
        }
    }
}
