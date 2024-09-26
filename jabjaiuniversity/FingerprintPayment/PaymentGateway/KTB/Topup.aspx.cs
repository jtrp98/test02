using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using PagedList;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FingerprintPayment.PaymentGateway.KBank.Checkout;

namespace FingerprintPayment.PaymentGateway.KTB
{
    public partial class Topup : System.Web.UI.Page
    {
        protected string REF1 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Suspend payments for a period of time 23.00-01.00 น
                DateTime start0001 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime end0001 = start0001.AddHours(1);
                DateTime start2300 = start0001.AddHours(23);
                DateTime end2300 = start0001.AddDays(1);

                if ((start0001 <= DateTime.Now && DateTime.Now <= end0001) || (start2300 <= DateTime.Now && DateTime.Now <= end2300))
                {
                    string lang = Request.QueryString["lang"];
                    if (string.IsNullOrEmpty(lang)) lang = "th";
                    switch (lang)
                    {
                        case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                        case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                    }

                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
        <div class="""">
            <div class=""container"">
                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                    <img src=""../../images/School Bright logo 1 storke222.png"" style=""width: 82%;"" />
                    <img src=""../../images/ic_alert.png"" style=""width: 30%;"" />
                    <div class=""improve-system"">
                        <p class=""message"">
                            {0}
                        </p>
                    </div>
                    <div class=""improve-system"">
                        <img class=""contact"" src=""../../images/ic_contact.png"" />
                        <p class=""contact"" style=""padding-top: 12px;"">
                            {1}
                        </p>
                        <p class=""contact"">
                            {2}
                        </p>
                        <p class=""contact"">
                            LINE Offcial : @jabjai
                        </p>
                    </div>
                </div>
                <hr class=""improve-system"">
                <div class=""section-bottom improve-system"">
                    <a href=""../../closepage.html"" class=""confirm-btn btn btn-success"">{3}
                    </a>
                </div>
            </div>
        </div>
    </div>", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "System-Message04"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Contact01"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Contact02"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Button-BackToMain"));

                    return;
                }


                // Check Server Status
                var serverStatus = CheckServerStatus();
                if (serverStatus.StatusCode != 200)
                {
                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../images/School Bright logo only.png"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{0}</h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                       </div>
                                                       <div class=""section-bottom"">
		                                                <a href=""../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                            {1}
		                                                </a>
	                                                   </div>", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "System-Message03"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Button-BackToMain"));
                    return;
                }


                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    if (!fcommon.PaymentSetting(masterEntities, "Topup QRCode", "KTB"))
                    {
                        this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                    <div class=""card"">
                                                        <div class=""container"">
                                                            <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                <img src=""../../../images/exclamation-256.jpg"" style=""height: 248px;"" />
                                                                <h2>{0}</h2>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 </div>
                                                <div class=""section-bottom"">
		                                            <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                        {1}
		                                            </a>
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "System-Message05"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Button-BackToMain"));
                        return;
                    }

                    // Init qr code
                    string studentID = Request.QueryString["studentID"];
                    string amount = Request.QueryString["amount"];
                    decimal fee = 0;

                    string lang = Request.QueryString["lang"]; // th, en
                    if (string.IsNullOrEmpty(lang)) lang = "th";
                    switch (lang)
                    {
                        case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                        case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                    }

                    // Render html
                    try
                    {
                        int sID = 0;
                        int.TryParse(studentID, out sID);

                        // Get school data 
                        string studentLevel = "", userName = "";
                        var userData = masterEntities.TUsers.FirstOrDefault(f => f.sID == sID);
                        var schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == userData.nCompany);
                        var f_PaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany && f.Fd_KTBPayment == true);
                        if (f_PaymentGateway == null)
                        {
                            this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../images/School Bright logo only.png"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{0}</h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                   <div class=""section-bottom"">
		                                                <a href=""../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                           {1}
		                                                </a>
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "System-Message01"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Button-BackToMain"));
                            return;
                        }

                        // Get student data
                        using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                        {
                            if (userData.cType == "0")
                            {
                                // Student
                                StudentLogic studentLogic = new StudentLogic(en);
                                string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolData.nCompany });

                                int classroomID = 0;
                                var f_studentClassroomHistory = en.TStudentClassroomHistories.FirstOrDefault(f => f.SchoolID == schoolData.nCompany && f.sID == userData.nSystemID && f.cDel == false && f.nTerm == currentTermID);
                                if (f_studentClassroomHistory != null)
                                {
                                    classroomID = f_studentClassroomHistory.nTermSubLevel2.Value;
                                }

                                // Get class 2 lang
                                string query = string.Empty;
                                if (schoolData.ClassNameDisable ?? false)
                                {
                                    // Hide room
                                    query = string.Format(@"
SELECT sl.SubLevel 'ClassTH', sl.SubLevelEN 'ClassEN'
FROM TTermSubLevel2 tsl LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
WHERE tsl.SchoolID={0} AND tsl.nTermSubLevel2={1}", schoolData.nCompany, classroomID);
                                }
                                else
                                {
                                    query = string.Format(@"
SELECT sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassTH', sl.SubLevelEN + ' / ' + tsl.nTSubLevel2 'ClassEN'
FROM TTermSubLevel2 tsl LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
WHERE tsl.SchoolID={0} AND tsl.nTermSubLevel2={1}", schoolData.nCompany, classroomID);
                                }
                                ClassLanguage classLanguage = en.Database.SqlQuery<ClassLanguage>(query).FirstOrDefault();

                                switch (lang)
                                {
                                    case "th":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Class"), classLanguage.ClassTH);
                                        userName = userData.sName + " " + userData.sLastname;
                                        break;
                                    case "en":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Class"), classLanguage.ClassEN);
                                        var f_student = en.TUser.FirstOrDefault(f => f.sID == userData.nSystemID);
                                        userName = f_student.sStudentNameEN + " " + f_student.sStudentLastEN;
                                        if (string.IsNullOrEmpty(userName.Replace(" ", "")))
                                        {
                                            userName = userData.sName + " " + userData.sLastname;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                // Employee
                                userName = userData.sName + " " + userData.sLastname;
                                if (lang == "en")
                                {
                                    var empInfo = en.TEmployeeInfoes.Where(w => w.sEmp == sID).OrderByDescending(o => o.ID).FirstOrDefault();
                                    if (empInfo != null && (!string.IsNullOrEmpty(empInfo.FirstNameEn) || !string.IsNullOrEmpty(empInfo.LastNameEn)))
                                    {
                                        userName = empInfo.FirstNameEn + " " + empInfo.LastNameEn;
                                    }
                                }
                            }

                            // Generate KTB QR Code
                            string jsonRequestBody = string.Format(@"{{""schoolID"": {0}, ""userID"": {1}, ""amount"": {2:0.00}, ""appName"": ""System""}}", schoolData.nCompany, sID, decimal.Parse(amount));

                            Result result = new Result();

                            var client = new RestClient("https://system.schoolbright.co/PaymentGateway/KTB/GenerateQRCode.ashx");
                            client.Timeout = -1;

                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            result = JsonConvert.DeserializeObject<Result>(response.Content);

                            // Get ref1 from qr code data
                            string ref1 = "";
                            string qrCodeBase64 = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
                            if (result != null && result.success)
                            {
                                var qrCode = result.data.qrCode.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                REF1 = ref1 = qrCode[1];

                                qrCodeBase64 = "data:image/png;base64," + result.data.qrCodeBase64;
                            }

                            // Stamp date
                            var stampDate = "";
                            switch (lang)
                            {
                                case "th": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm น.", new CultureInfo("th-TH")); break;
                                case "en": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm", new CultureInfo("en-US")); break;
                            }

                            this.ltrPay.Text = string.Format(@"
    <div class=""section-top"">
        <div class=""card"" style=""border-radius: 25px; margin: 11px; padding: 5px;"">
            <div class=""container"">
                <div class=""section-top"" style=""padding: 0 15px 0 15px;"">
                    <div class=""row-topic"">
                        <img src=""../../images/SchoolBrightLogo.png"" alt="""" class=""logo"" />
                        <blockquote class=""topic"">
                            <p class=""font-2"">{0}</p>
                            <p class=""font-1"">{1}</p>
                        </blockquote>
                    </div>
                    <p class=""topic-detail font-2"">{2}</p>
                    <div class=""payer"">
                        <p class=""row-label font-2"">{3}</p>
                        <p class=""row-input font-2"">{4}</p>
                    </div>
                    <div class=""row-highlight"">
                        <div class=""row"">
                            <p class=""row-label font-2"">{5}</p>
                            <p class=""row-input font-2"">{6}</p>
                        </div>
                        <div class=""row"">
                            <p class=""row-label font-2"">{7}</p>
                            <p class=""row-input font-2"">
                                {8:#,0.00}
                                <!--บาท-->
                            </p>
                        </div>
                        <div class=""row"" style=""display: none;"">
                            <p class=""row-label font-2"">{9}</p>
                            <p class=""row-input font-2"">
                                {10:#,0.00}
                                <!--บาท-->
                            </p>
                        </div>
                        <div class=""row last"">
                        </div>
                    </div>
                </div>
                <div class=""section-bottom"" style=""padding: 0 15px 0 15px;"">
                    <div class=""row-summary"">
                        <p class=""row-label font-3"">{11}</p>
                        <p class=""row-input font-3"">
                            {12:#,0.00}
                            <!--บาท-->
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""section-bottom"">
        <div class=""confirm-btn"">
            <button _kpayment="""" type=""button"" class=""pay-button"" style=""width: 92%; height: 50px; border-radius: 25px;""><span style=""transition: all 0.5s ease 0s;""></span></button>
        </div>
    </div>
    <div id=""modal-background""></div>
    <div id=""modal-content"">
        <div id=""card-view"">
            <span class=""modal-close"" style=""width: 2.5rem; height: 2.5rem; float: right;""></span>
            <p class=""modal-title"">QR Code สำหรับการเติมเงิน</p>
            <div id=""card-qr"" style=""height: 330px; text-align: center; padding: 3px 10px;"">
                <img src=""/images/PromptPay-logo.png"" style=""width: 92px;"" />
                <div style=""display: flex; justify-content: center;"">
                    <img src=""/images/ThaiQR.png"" style=""width: 60px; position: absolute; padding: 95px;"" />
                    <img src=""{13}"" style=""width: 250px;"" />
                </div>
            </div>
            <div id=""card-qr-detail"" style=""min-height: 85px; margin-top: 19px; border-radius: 10px; font-weight: bold;"">
                <p class=""school""><span>{14}</span><span class=""right""><span class=""amount"">{8:#,0.00}</span> THB</span></p>
                <hr />
                <p class=""ref-no""><span>หมายเลขอ้างอิง:</span><span class=""right"">{6}</span></p>
                <p class=""time-left""><span>เวลาที่เหลือ:</span><span class=""right"" id=""countdown"">600</span></p>
            </div>
            <p style=""text-align: justify; font-size: large; font-weight: bold; text-indent: 25px; margin-top: 25px; padding: 0 17px;"">
                อยู่ระหว่างทำรายการ กรุณาอย่าปิดหรือรีเฟรชหน้าจอ
                หากไม่พบข้อความแสดงผลการชำระเงินภายใน 30 วินาที
                <!--กรุณากด <span style=""color: #ff9b2b; text-decoration: underline;"">""ตรวจสอบสถานะ""</span>-->
            </p>
        </div>
    </div>
", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Topic"), stampDate, GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Title"), userName, studentLevel, GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-OrderNumber"), ref1, GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Amount"), decimal.Parse(amount), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Fee"), fee
, GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Form-Total"), decimal.Parse(amount) + fee
, qrCodeBase64, schoolData.sCompany);
                        }
                    }
                    catch (Exception err)
                    {
                        this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                    <div class=""card"">
                                                        <div class=""container"">
                                                            <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                <img src=""../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                <h1>{0}</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 </div>
                                                <div class=""section-bottom"">
		                                            <a href=""../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                        {1}
		                                            </a>
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "System-Message02"), GetGlobalResourceObject("PaymentGateway_KTB_Topup.aspx", "Button-BackToMain"));

                        //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                        //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                        //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                        //string connectionString = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                        var error = new Dictionary<string, string>
                                    {
                                        {"Type", err.GetType().ToString()},
                                        {"Message", err.Message},
                                        {"StackTrace", err.StackTrace}
                                    };

                        foreach (DictionaryEntry data in err.Data)
                        {
                            error.Add(data.Key.ToString(), data.Value.ToString());
                        }

                        string jsonString = JsonConvert.SerializeObject(error, Formatting.Indented);

                        //SqlConnection _conn = new SqlConnection(connectionString);
                        fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Json Data:{0}')", jsonString));

                        LINENotify notify = new LINENotify();
                        notify.LineSBErrorSend(new LINENotifyDATA
                        {
                            Parameter = new { studentID, amount, fee },
                            Date_Time = DateTime.Now,
                            URL = HttpContext.Current.Request.RawUrl,
                            Error_Method = "PAYMENTGATEWAY(KTB) - TOPUP MONEY(GENERATE QR CODE)"
                        }, err);
                    }
                }
            }
        }

        private static ResponseServerStatus CheckServerStatus()
        {
            ResponseServerStatus responseServerStatus = new ResponseServerStatus();
            string responseFromServer = "";

            try
            {
                //https://paymentapi.schoolbright.co/
                //https://payment-api-test-hwc.schoolbright.co/
                string HostURL = ConfigurationManager.AppSettings["PaymentApi"].ToString();
                var client = new RestClient(HostURL + "/api/offline/school/checktemptop");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                IRestResponse response = client.Execute(request);

                if (!string.IsNullOrEmpty(response.Content))
                {
                    responseFromServer = response.Content;
                    responseServerStatus = JsonConvert.DeserializeObject<ResponseServerStatus>(responseFromServer);
                }
                else
                {
                    throw new Exception(string.Format(@"Empty response."));
                }
            }
            catch (Exception ex)
            {
                responseServerStatus.Status = "Error";
                responseServerStatus.JsonError = string.Format(@"{{""Message"" : ""{0}"", ""responseData"" : {1}}}", ex.Message, responseFromServer);
            }

            return responseServerStatus;
        }

        public class QRCodeModel
        {
            public string qrCode { get; set; }
            public string qrCodeBase64 { get; set; }
            public string barCode { get; set; }
            public string barCodeBase64 { get; set; }
        }

        public class Result
        {
            public bool success { get; set; }
            public string message { get; set; }
            public QRCodeModel data { get; set; }
        }

        public class PaymentData
        {
            [JsonProperty(PropertyName = "date")]
            public DateTime Date { get; set; }

            [JsonProperty(PropertyName = "fullname")]
            public string Fullname { get; set; }

            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }

            [JsonProperty(PropertyName = "ref1")]
            public string Ref1 { get; set; }

            [JsonProperty(PropertyName = "ref2")]
            public string Ref2 { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }

            [JsonProperty(PropertyName = "total")]
            public decimal Total { get; set; }
        }

    }
}