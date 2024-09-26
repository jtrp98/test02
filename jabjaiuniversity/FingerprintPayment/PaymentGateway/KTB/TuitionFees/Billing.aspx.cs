using AccountingEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
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

namespace FingerprintPayment.PaymentGateway.KTB.TuitionFees
{
    public partial class Billing : System.Web.UI.Page
    {
        protected string REF1 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string source = (string)Request["source"];

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
                    <img src=""../../../images/School Bright logo 1 storke222.png"" style=""width: 82%;"" />
                    <img src=""../../../images/ic_alert.png"" style=""width: 30%;"" />
                    <div class=""improve-system"">
                        <p class=""message"">
                            {0}
                        </p>
                    </div>
                    <div class=""improve-system"">
                        <img class=""contact"" src=""../../../images/ic_contact.png"" />
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
                    <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">{3}
                    </a>
                </div>
            </div>
        </div>
    </div>", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-Message04"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Contact01"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Contact02"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));

                    return;
                }


                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    if (!fcommon.PaymentSetting(masterEntities, "Payment Gateway", "KTB"))
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
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-Message05"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));
                        return;
                    }

                    // Init qr code
                    string studentID = Request.QueryString["studentID"];
                    string amount = Request.QueryString["amount"];
                    string invoiceID = Request.QueryString["invoiceID"];
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
                        int schoolID = Convert.ToInt32(Session["K_SchoolID"]);

                        // Check Invoice Paid status
                        int invID = int.Parse(invoiceID);
                        using (PeakengineEntities peakEngine = Connection.PeakengineEntities(ConnectionDB.Read))
                        {
                            if (source == "newregister")
                            {
                                int.TryParse(Request["schoolID"], out schoolID);
                            }

                            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                            {
                                var sql = string.Format("select count(1) from [AccountingDB].[dbo].AccountInvoiceStudent where AccountInvoiceStudentId = {0} and Status='Success' ", invID);
                                int c = en.Database.SqlQuery<int>(sql).FirstOrDefault();
                                if (c != 0)
                                {
                                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../../images/check-circle.gif"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{0}</h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>	
                                                    <div class=""section-bottom"">
		                                                <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                            {1}
		                                                </a>
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));
                                    return;
                                }

                                sql = string.Format("select count(1) from [AccountingDB].[dbo].AccountInvoiceStudent where AccountInvoiceStudentId = {0}", invID);
                                c = en.Database.SqlQuery<int>(sql).FirstOrDefault();
                                if (c == 0)
                                {
                                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{1} [Invoice ID: {0}]</h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                     </div>
                                                    <div class=""section-bottom"">
		                                                <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                            {2}
		                                                </a>
	                                                </div>", invID, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-Message03"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));
                                    return;
                                }
                            }
                        }

                        int sID = 0;
                        int.TryParse(studentID, out sID);

                        // Get school data 
                        string studentLevel = "", studentName = "";
                        MasterEntity.TUser userData = null;
                        MasterEntity.TCompany schoolData = null;

                        if (source == "newregister")
                        {
                            studentID = Request["studentCode"];

                            var invoice_id = int.Parse(invoiceID);
                            schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);
                            using (AccountingDBEntities accountingEntity = Connection.AccountingDBEntities(ConnectionDB.Read))
                            {
                                var inv = (from a in accountingEntity.AccountInvoiceStudent
                                           where a.AccountInvoiceStudentId == invoice_id
                                           select a).FirstOrDefault();


                                userData = new MasterEntity.TUser
                                {
                                    sName = inv.StudentName,
                                    username = inv.StudentCode,
                                    cType = "newregister",
                                    nCompany = schoolID,
                                };

                                ClassLanguage classLanguage = new ClassLanguage
                                {
                                    ClassTH = inv.LevelName,
                                    ClassEN = inv.LevelName,
                                };

                                switch (lang)
                                {
                                    case "th":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Class"), classLanguage.ClassTH);
                                        studentName = inv.StudentName;
                                        break;
                                    case "en":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Class"), classLanguage.ClassEN);
                                        studentName = inv.StudentName;
                                        break;
                                }
                            }
                        }
                        else
                        {
                            userData = masterEntities.TUsers.FirstOrDefault(f => f.sID == sID);
                            schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == userData.nCompany);
                        }

                        var f_PaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany && f.Fd_KTBPayment == true);
                        if (f_PaymentGateway == null)
                        {
                            this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../../images/School Bright logo only.png"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{0}</h1>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                   <div class=""section-bottom"">
		                                                <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                           {1}
		                                                </a>
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-Message01"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));
                            return;
                        }

                        // Get student data
                        using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                        {
                            if (userData.cType == "0")
                            {
                                var f_student = en.TUser.FirstOrDefault(f => f.sID == userData.nSystemID);

                                // Get class 2 lang
                                string query = string.Empty;
                                if (schoolData.ClassNameDisable ?? false)
                                {
                                    // Hide room
                                    query = string.Format(@"
SELECT sl.SubLevel 'ClassTH', sl.SubLevelEN 'ClassEN'
FROM TTermSubLevel2 tsl LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
WHERE tsl.SchoolID={0} AND tsl.nTermSubLevel2={1}", schoolData.nCompany, f_student.nTermSubLevel2.Value);
                                }
                                else
                                {
                                    query = string.Format(@"
SELECT sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassTH', sl.SubLevelEN + ' / ' + tsl.nTSubLevel2 'ClassEN'
FROM TTermSubLevel2 tsl LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
WHERE tsl.SchoolID={0} AND tsl.nTermSubLevel2={1}", schoolData.nCompany, f_student.nTermSubLevel2.Value);
                                }
                                ClassLanguage classLanguage = en.Database.SqlQuery<ClassLanguage>(query).FirstOrDefault();

                                switch (lang)
                                {
                                    case "th":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Class"), classLanguage.ClassTH);
                                        studentName = userData.sName + " " + userData.sLastname;
                                        break;
                                    case "en":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Class"), classLanguage.ClassEN);
                                        studentName = f_student.sStudentNameEN + " " + f_student.sStudentLastEN;
                                        if (string.IsNullOrEmpty(studentName.Replace(" ", "")))
                                        {
                                            studentName = userData.sName + " " + userData.sLastname;
                                        }
                                        break;
                                }
                            }

                            // Generate KTB QR Code & Barcode
                            string jsonRequestBody = string.Format(@"{{""schoolID"": {0}, ""userID"": {1}, ""invoiceID"": {2}, ""amount"": {3:0.00}, ""appName"": ""System""}}", schoolData.nCompany, sID, invID, decimal.Parse(amount));

                            Result result = new Result();

                            var client = new RestClient("https://system.schoolbright.co/PaymentGateway/KTB/GenerateQRCode.ashx"); // http://localhost:59443/PaymentGateway/KTB/GenerateQRCode.ashx
                            client.Timeout = -1;

                            var request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                            IRestResponse response = client.Execute(request);

                            result = JsonConvert.DeserializeObject<Result>(response.Content);

                            // Get ref1 from qr code data
                            string ref1 = "";
                            string ref2 = "";
                            string qrCodeBase64 = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
                            string barCodeBase64 = "data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==";
                            if (result != null && result.success)
                            {
                                var qrCode = result.data.qrCode.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                REF1 = ref1 = qrCode[1];
                                ref2 = qrCode[2];

                                qrCodeBase64 = "data:image/png;base64," + result.data.qrCodeBase64;
                                barCodeBase64 = result.data.barCodeBase64;
                            }

                            // Stamp date
                            var stampDate = "";
                            var stampDate2 = "";
                            switch (lang)
                            {
                                case "th":
                                    stampDate = DateTime.Now.ToString("dd MMM yy HH:mm น.", new CultureInfo("th-TH"));
                                    stampDate2 = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                                    break;
                                case "en":
                                    stampDate = DateTime.Now.ToString("dd MMM yy HH:mm", new CultureInfo("en-US"));
                                    stampDate2 = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-US"));
                                    break;
                            }

                            // Get address
                            string provinceName = "";
                            string aumpherName = "";
                            string tumbonName = "";
                            if (schoolData.ProvinceID != null)
                            {
                                var provinceObj = masterEntities.provinces.Where(w => w.PROVINCE_ID == schoolData.ProvinceID).FirstOrDefault();
                                if (provinceObj != null)
                                {
                                    provinceName = provinceObj.PROVINCE_NAME;
                                }

                                if (schoolData.AumpherID != null)
                                {
                                    var aumpherObj = masterEntities.amphurs.Where(w => w.PROVINCE_ID == schoolData.ProvinceID && w.AMPHUR_ID == schoolData.AumpherID).FirstOrDefault();
                                    if (aumpherObj != null)
                                    {
                                        aumpherName = aumpherObj.AMPHUR_NAME;
                                    }

                                    if (schoolData.TumbonID != null)
                                    {
                                        var tumbonObj = masterEntities.districts.Where(w => w.PROVINCE_ID == schoolData.ProvinceID && w.AMPHUR_ID == schoolData.AumpherID && w.DISTRICT_ID == schoolData.TumbonID).FirstOrDefault();
                                        if (tumbonObj != null)
                                        {
                                            tumbonName = tumbonObj.DISTRICT_NAME;
                                        }
                                    }
                                    else
                                    {
                                        tumbonName = schoolData.sTumbon;
                                    }
                                }
                                else
                                {
                                    aumpherName = schoolData.sAumpher;
                                }
                            }
                            else
                            {
                                provinceName = schoolData.sProvince;
                            }
                            if (schoolData.AumpherID == null)
                            {
                                aumpherName = schoolData.sAumpher;
                            }
                            if (schoolData.TumbonID == null)
                            {
                                tumbonName = schoolData.sTumbon;
                            }
                            string address1 = string.Format(@"{0} {1} {2} {3}", string.IsNullOrEmpty(schoolData.sHomeNumber) ? "" : schoolData.sHomeNumber, string.IsNullOrEmpty(schoolData.sMuu) ? "" : "หมู่ที่ " + schoolData.sMuu, string.IsNullOrEmpty(schoolData.sRoad) ? "" : "ถนน" + schoolData.sRoad, string.IsNullOrEmpty(schoolData.sSoy) ? "" : "ซอย" + schoolData.sSoy);
                            string address2 = string.Format(@"{0} {1} {2} {3}", string.IsNullOrEmpty(tumbonName) ? "" : (schoolData.ProvinceID == 1? "แขวง" : "ตำบล") + tumbonName, string.IsNullOrEmpty(aumpherName) ? "" : (schoolData.ProvinceID == 1 ? "เขต" : "อำเภอ") + aumpherName, string.IsNullOrEmpty(provinceName) ? "" : provinceName, string.IsNullOrEmpty(schoolData.sPost) ? "" : schoolData.sPost);

                            this.ltrPay.Text = string.Format(@"
    <div class=""section-top"">
        <div class=""card"" style=""border-radius: 25px; margin: 11px; padding: 5px;"">
            <div class=""container"">
                <div class=""section-top"" style=""padding: 0 15px 0 15px;"">
                    <div class=""row-topic"">
                        <img src=""../../../images/SchoolBrightLogo.png"" alt="""" class=""logo"" />
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
                            <p class=""row-input font-2"">{8}</p>
                        </div>
                        <div class=""row"">
                            <p class=""row-label font-2"">{9}</p>
                            <p class=""row-input font-2"">
                                {10:#,0.00}
                                <!--บาท-->
                            </p>
                        </div>
                        <div class=""row"" style=""display: none;"">
                            <p class=""row-label font-2"">{11}</p>
                            <p class=""row-input font-2"">
                                {12:#,0.00}
                                <!--บาท-->
                            </p>
                        </div>
                        <div class=""row last"">
                        </div>
                    </div>
                </div>
                <div class=""section-bottom"" style=""padding: 0 15px 0 15px;"">
                    <div class=""row-summary"">
                        <p class=""row-label font-3"">{13}</p>
                        <p class=""row-input font-3"">
                            {14:#,0.00}
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
            <span class=""modal-close"" style=""width: 2rem; height: 2rem; float: right;""></span>
            <div class=""row-topic modal"">
                <img src=""../../../images/SchoolBrightLogo.png"" alt="""" class=""logo modal"" />
                <blockquote class=""topic modal"">
                    <p class=""font-2"">{15}</p>
                    <p class=""font-1"">{16}</p>
                </blockquote>
            </div>
            <p class=""modal-address"">{17}</p>
            <p class=""modal-address"">{18}</p>
            <p class=""modal-tax"">เลขประจำตัวผู้เสียภาษีอากร: {19}</p>
            <hr class=""modal"" />
            <div id=""card-detail"" style="""">
                <p class=""date""><span class=""left"">&nbsp;</span><span class=""right"">{20}</span></p>
                <p class=""fullname""><span class=""left"">ชื่อ-สกุล:</span><span class=""right"">{3}</span></p>
                <p class=""level""><span class=""left"">ชั้นเรียน:</span><span class=""right"">{4}</span></p>
                <p class=""student-code""><span class=""left"">รหัสนักเรียน:</span><span class=""right"">{21}</span></p>
                <p class=""ref-1""><span class=""left"">REF.1</span><span class=""right"">{8}</span></p>
                <p class=""ref-2""><span class=""left"">REF.2</span><span class=""right"">{22}</span></p>
                <p class=""amount""><span class=""left"">ยอดเงิน</span><span class=""right""><span style=""color: green;"">{10:#,0.00}</span> บาท</span></p>
            </div>
            <hr class=""modal dash"" />
            <div id=""card-scan"" style=""margin-top: 10px; text-align: center;"">
                <div style=""display: flex; justify-content: center;"">
                    <img src=""/images/ThaiQR.png"" style=""width: 57px; position: absolute; padding: 78px;"" />
                    <img src=""{23}"" style=""width: 210px;"" />
                </div>
                <img src=""{24}"" style=""width: 215px; height: 60px; margin-top: 5px; display: none;"" />
            </div>
            <p class=""modal-message"">กรุณาบันทึกรูป หรือแคปหน้าจอ</p>
            <p class=""modal-message"">เพื่อชำระเงินที่ Application ของธนาคาร</p>
            <div class=""save-bill"">
                <button type=""button"">บันทึกรูปภาพ</button>
            </div>
        </div>
    </div>
", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Topic"), stampDate, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Title"), studentName, studentLevel, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Invoice"), invoiceID, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-OrderNumber"), ref1, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Amount"), decimal.Parse(amount)
, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Fee"), fee, GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Form-Total"), decimal.Parse(amount) + fee
, schoolData.sCompany, schoolData.sNameEN, address1, address2, schoolData.TaxId, stampDate2, userData.username, ref2, qrCodeBase64, barCodeBase64);
                        }
                    }
                    catch (Exception err)
                    {
                        this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                    <div class=""card"">
                                                        <div class=""container"">
                                                            <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                <img src=""../../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                <h1>{0}</h1>
                                                            </div>
                                                        </div>
                                                    </div>
                                                 </div>
                                                <div class=""section-bottom"">
		                                            <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                        {1}
		                                            </a>
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "System-Message02"), GetGlobalResourceObject("PaymentGateway_KTB_TuitionFees_Billing.aspx", "Button-BackToMain"));

                        //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                        //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                        //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                        //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

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

                        //SqlConnection _conn = new SqlConnection(strconn);
                        fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Json Data:{0}')", jsonString));

                        LINENotify notify = new LINENotify();
                        notify.LineSBErrorSend(new LINENotifyDATA
                        {
                            Parameter = new { studentID, amount, fee, invoiceID },
                            Date_Time = DateTime.Now,
                            URL = HttpContext.Current.Request.RawUrl,
                            Error_Method = "PAYMENTGATEWAY(KTB) - PAY TUITION FEES(GENERATE QR CODE)"
                        }, err);
                    }
                }
            }
        }

        public class ClassLanguage
        {
            public string ClassTH { get; set; }
            public string ClassEN { get; set; }
        }
    }
}