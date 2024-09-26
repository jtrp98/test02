using JabjaiEntity.DB;
using KBankAPI;
using MasterEntity;
using AccountingEntity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiMainClass;
using System.Data.Entity;
using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using JabjaiTopup;
using Amazon.XRay.Recorder.Handlers.AspNet;
using FingerprintPayment.Class;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Collections;
using System.Threading;
using System.Configuration;

namespace FingerprintPayment.PaymentGateway.KBank.TuitionFees
{
    public partial class CheckoutQR : System.Web.UI.Page
    {
        private int schoolID=0;
        private string source = "";
        private string SystemName = "";
        private string Bank = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                source = (string)Request["source"];

                // Suspend payments for a period of time 23.00-01.00 น
                DateTime start0001 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime end0001 = start0001.AddHours(1);
                DateTime start2300 = start0001.AddHours(23);
                DateTime end2300 = start0001.AddDays(1);
                //bool _demo = (ConfigurationManager.AppSettings["demo"] == "false").ToString() == "true";
                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["demo"]))
                //{
                //    if (ConfigurationManager.AppSettings["demo"] == "true")
                //    {
                //        _demo = true;
                //    }
                //}

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
    </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message04"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Contact01"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Contact02"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));

                    return;
                }

                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {

                    if (!fcommon.PaymentSetting(masterEntities, "Payment Gateway", "KBank"))
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
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message05"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                        return;
                    }

                    if (!string.IsNullOrEmpty(Request["chargeId"]))
                    {
                        // After scan qr code
                        string chargeID = (string)Request["chargeId"];


                        string lang = Request.QueryString["lang"];
                        if (string.IsNullOrEmpty(lang)) lang = "th";
                        switch (lang)
                        {
                            case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                            case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                        }

                        // Check comma in  ChargeID
                        string[] chargeIDs = chargeID.Split(',');
                        if (chargeIDs.Length > 1) chargeID = chargeIDs[0];

                        int schoolID = Convert.ToInt32(Session["K_SchoolID"]);
                        if (source == "newregister")
                        {
                            int.TryParse(Request["schoolID"], out schoolID);
                        }
                        using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                        {
                            //using (var transactionSchool = en.Database.BeginTransaction())
                            //{
                            int yearID = 0;
                            int? transID = 0;
                            decimal? amount = 0m;
                            decimal fee = 0m;

                            int? userID = 0;

                            try
                            {
                                int existResInquiryQR = en.KResInquiryQRs.Where(w => w.ChargeID == chargeID && w.SchoolID == schoolID).Count();
                                if (existResInquiryQR == 0)
                                {
                                    // KResInquiryQR
                                    Config config = new Config();
                                    config.Method = "GET";

                                    // Call Inquiry QR API
                                    InquiryQRAPI inquiryQRAPI = new InquiryQRAPI(config);

                                    var f_PaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolID && f.Fd_ActiveInvoice == true);

                                    inquiryQRAPI.InquiryQR(chargeID, f_PaymentGateway.Fd_SecretKeyInvoice);

                                    // Set fee
                                    //fee = f_PaymentGateway.Fd_FeeInvoice ?? 0;

                                    if (inquiryQRAPI.QR.Status != "Error")
                                    {
                                        // Find OrderID
                                        KResOrder kResOrder = en.KResOrders.Where(w => w.SchoolID == schoolID && w.OrderID == inquiryQRAPI.QR.OrderId).FirstOrDefault();
                                        if (kResOrder != null)
                                        {
                                            yearID = kResOrder.Year;
                                            transID = kResOrder.TransID;
                                            amount = kResOrder.Amount - fee;
                                        }

                                        //KResInquiryQR q = new KResInquiryQR
                                        //{
                                        //    Year = yearID,
                                        //    TransID = transID,
                                        //    ChargeID = chargeID,
                                        //    Object = inquiryQRAPI.QR.Object,
                                        //    Amount = inquiryQRAPI.QR.Amount,
                                        //    Currency = inquiryQRAPI.QR.Currency,
                                        //    Description = inquiryQRAPI.QR.Description,
                                        //    TransactionState = inquiryQRAPI.QR.TransactionState,
                                        //    ReferenceOrder = inquiryQRAPI.QR.ReferenceOrder,
                                        //    Created = inquiryQRAPI.QR.Created,
                                        //    OrderID = inquiryQRAPI.QR.OrderId,
                                        //    Status = inquiryQRAPI.QR.Status,
                                        //    LiveMode = inquiryQRAPI.QR.LiveMode.ToString(),
                                        //    MetaData = inquiryQRAPI.QR.MetaData,
                                        //    FailureCode = inquiryQRAPI.QR.FailureCode,
                                        //    FailureMessage = inquiryQRAPI.QR.FailureMessage,
                                        //    SourceID = inquiryQRAPI.QR.Source.CardObjectId,
                                        //    SourceObject = inquiryQRAPI.QR.Source.Object,
                                        //    SourceBrand = inquiryQRAPI.QR.Source.Brand,
                                        //    SourceCardMasking = inquiryQRAPI.QR.Source.CardMasking,
                                        //    SourceIssuerBank = inquiryQRAPI.QR.Source.IssuerBank,
                                        //    CreateDate = DateTime.Now,
                                        //    SchoolID = schoolID
                                        //};
                                        //en.KResInquiryQRs.Add(q);
                                        //en.SaveChanges();

                                        // Update Transaction (KTransaction)
                                        KTransaction transaction = en.KTransactions.FirstOrDefault(w => w.Year == yearID && w.TransactionID == transID && w.SchoolID == schoolID);
                                        if (transaction != null)
                                        {
                                            //transaction.ChargeID = chargeID;
                                            //transaction.QRID = inquiryQRAPI.QR.Source.CardObjectId;
                                            //transaction.UpdatedDate = DateTime.Now;

                                            //en.SaveChanges();

                                            userID = transaction.UserID;
                                        }

                                        // Call TopupMoney function

                                        // Get school data 
                                        var userData = masterEntities.TUsers.FirstOrDefault(f => f.sID == userID);
                                        var schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.nCompany == userData.nCompany);
                                        // Get student data

                                        // Render html
                                        if (inquiryQRAPI.QR.TransactionState == "Authorized" && inquiryQRAPI.QR.Status == "success")
                                        {
                                            //AWSXRay xray = new AWSXRay();

                                            //AWSXRayRecorder recorder = xray.Register();

                                            //recorder.AddAnnotation("Method", "PaymentGateway - Pay Tuition Fees(QR Code)");
                                            //recorder.AddAnnotation("Entities", schoolData.sEntities);
                                            //recorder.AddAnnotation("UserId", userData.sID);

                                            try
                                            {
                                                // Call Invoice Payment function
                                                KInvoicePaymentV2 kInvoicePayment = new KInvoicePaymentV2();
                                                var result = kInvoicePayment.PaymentV2((int)transaction.InvoiceID, (decimal)transaction.Amount, "K-Payment Gateway(QR Code)", schoolID);
                                                if (result.Status != "Error")
                                                {

                                                    try
                                                    {
                                                        transaction.ChargeID = chargeID;
                                                        transaction.QRID = inquiryQRAPI.QR.Source.CardObjectId;
                                                        transaction.UpdatedDate = DateTime.Now;

                                                        transaction.PaidPaymentID = result.PaidPaymentId;

                                                        en.SaveChanges();

                                                        KResInquiryQR q = new KResInquiryQR
                                                        {
                                                            Year = yearID,
                                                            TransID = transID,
                                                            ChargeID = chargeID,
                                                            Object = inquiryQRAPI.QR.Object,
                                                            Amount = inquiryQRAPI.QR.Amount,
                                                            Currency = inquiryQRAPI.QR.Currency,
                                                            Description = inquiryQRAPI.QR.Description,
                                                            TransactionState = inquiryQRAPI.QR.TransactionState,
                                                            ReferenceOrder = inquiryQRAPI.QR.ReferenceOrder,
                                                            Created = inquiryQRAPI.QR.Created,
                                                            OrderID = inquiryQRAPI.QR.OrderId,
                                                            Status = inquiryQRAPI.QR.Status,
                                                            LiveMode = inquiryQRAPI.QR.LiveMode.ToString(),
                                                            MetaData = inquiryQRAPI.QR.MetaData,
                                                            FailureCode = inquiryQRAPI.QR.FailureCode,
                                                            FailureMessage = inquiryQRAPI.QR.FailureMessage,
                                                            SourceID = inquiryQRAPI.QR.Source.CardObjectId,
                                                            SourceObject = inquiryQRAPI.QR.Source.Object,
                                                            SourceBrand = inquiryQRAPI.QR.Source.Brand,
                                                            SourceCardMasking = inquiryQRAPI.QR.Source.CardMasking,
                                                            SourceIssuerBank = inquiryQRAPI.QR.Source.IssuerBank,
                                                            CreateDate = DateTime.Now,
                                                            SchoolID = schoolID
                                                        };
                                                        en.KResInquiryQRs.Add(q);
                                                        en.SaveChanges();



                                                        #region Send email reciept
                                                        if (result.Source == "newregister")
                                                        {
                                                            using (AccountingDBEntities accountingEntity = Connection.AccountingDBEntities(ConnectionDB.Read))
                                                            {
                                                                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                                                                {
                                                                    var school = dbMaster.TCompanies.FirstOrDefault(m => m.nCompany == schoolID);
                                                                    var invoice = (from a in accountingEntity.AccountInvoiceStudent where a.AccountInvoiceStudentId == transaction.InvoiceID select a).FirstOrDefault();
                                                                    var reciept = (from a in accountingEntity.AccountReceive where a.AccountInvoiceStudentId == transaction.InvoiceID select a).FirstOrDefault();

                                                                    if (invoice != null && reciept != null)
                                                                    {
                                                                        var preRegister = en.TPreRegisters.Where(m => m.SchoolID == schoolID && m.preRegisterId == invoice.PreRegisterId).FirstOrDefault();

                                                                        if (preRegister != null)
                                                                        {
                                                                            var baseUrl = "https://accounting.schoolbright.co";

                                                                            var emailFrom = "noreply@schoolbright.co";
                                                                            var mailMessageHtml = ComFunction.GenerateEmailContent("receipt", schoolID
                                                                                , invoice.StudentName
                                                                                , invoice.StudentName
                                                                                , reciept.ReceiveCode
                                                                                , invoice.Year.ToString()
                                                                                , $"{baseUrl}/Payment/PrintReciept?id={transaction.InvoiceID}&code={reciept.ReceiveCode}&session={reciept.Session}"
                                                                            );
                                                                            var mailSubject = $@"{school.sCompany} : นำส่งใบเสร็จค่าธรรมเนียมการศึกษา {invoice.Year} {invoice.StudentName}";

                                                                            if (!string.IsNullOrEmpty(preRegister.sEmail))
                                                                            {
                                                                                ComFunction.SendMail(subject: mailSubject,
                                                                                                    body: mailMessageHtml,
                                                                                                    toEmail: preRegister.sEmail,
                                                                                                    toName: invoice.StudentName);

                                                                                // Save mail log
                                                                                TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                                                                {
                                                                                    preRegisterId = preRegister.preRegisterId,
                                                                                    SendTo = preRegister.sEmail,
                                                                                    SendFrom = emailFrom,
                                                                                    Title = mailSubject,
                                                                                    Message = mailMessageHtml,
                                                                                    SendDate = DateTime.Now,
                                                                                    SendBy = 0
                                                                                };
                                                                                en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                                                                en.SaveChanges();
                                                                            }
                                                                            if (!string.IsNullOrEmpty(preRegister.stayWithEmail))
                                                                            {
                                                                                ComFunction.SendMail(subject: mailSubject,
                                                                                                    body: mailMessageHtml,
                                                                                                    toEmail: preRegister.stayWithEmail,
                                                                                                    toName: invoice.StudentName);

                                                                                // Save mail log
                                                                                TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                                                                {
                                                                                    preRegisterId = preRegister.preRegisterId,
                                                                                    SendTo = preRegister.stayWithEmail,
                                                                                    SendFrom = emailFrom,
                                                                                    Title = mailSubject,
                                                                                    Message = mailMessageHtml,
                                                                                    SendDate = DateTime.Now,
                                                                                    SendBy = 0
                                                                                };
                                                                                en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                                                                en.SaveChanges();
                                                                            }
                                                                            if (!string.IsNullOrEmpty(preRegister.FatherEmail))
                                                                            {
                                                                                ComFunction.SendMail(subject: mailSubject,
                                                                                                    body: mailMessageHtml,
                                                                                                    toEmail: preRegister.FatherEmail,
                                                                                                    toName: invoice.StudentName);

                                                                                // Save mail log
                                                                                TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                                                                {
                                                                                    preRegisterId = preRegister.preRegisterId,
                                                                                    SendTo = preRegister.FatherEmail,
                                                                                    SendFrom = emailFrom,
                                                                                    Title = mailSubject,
                                                                                    Message = mailMessageHtml,
                                                                                    SendDate = DateTime.Now,
                                                                                    SendBy = 0
                                                                                };
                                                                                en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                                                                en.SaveChanges();
                                                                            }
                                                                            if (!string.IsNullOrEmpty(preRegister.MotherEmail))
                                                                            {
                                                                                ComFunction.SendMail(subject: mailSubject,
                                                                                                    body: mailMessageHtml,
                                                                                                    toEmail: preRegister.MotherEmail,
                                                                                                    toName: invoice.StudentName);

                                                                                // Save mail log
                                                                                TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                                                                {
                                                                                    preRegisterId = preRegister.preRegisterId,
                                                                                    SendTo = preRegister.MotherEmail,
                                                                                    SendFrom = emailFrom,
                                                                                    Title = mailSubject,
                                                                                    Message = mailMessageHtml,
                                                                                    SendDate = DateTime.Now,
                                                                                    SendBy = 0
                                                                                };
                                                                                en.TPreRegisterSendMails.Add(preRegisterSendMail);
                                                                                en.SaveChanges();
                                                                            }
                                                                            if (!string.IsNullOrEmpty(preRegister.ParentEmail))
                                                                            {
                                                                                ComFunction.SendMail(subject: mailSubject,
                                                                                                    body: mailMessageHtml,
                                                                                                    toEmail: preRegister.ParentEmail,
                                                                                                    toName: invoice.StudentName);

                                                                                // Save mail log
                                                                                TPreRegisterSendMail preRegisterSendMail = new TPreRegisterSendMail
                                                                                {
                                                                                    preRegisterId = preRegister.preRegisterId,
                                                                                    SendTo = preRegister.ParentEmail,
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
                                                        }
                                                        #endregion                                                        
                                                    }
                                                    catch (Exception ext)
                                                    {


                                                        LINENotify notify = new LINENotify();
                                                        notify.LineSBErrorSend(new LINENotifyDATA
                                                        {
                                                            Parameter = new { schoolID, transID, transaction.InvoiceID, chargeID },
                                                            Date_Time = DateTime.Now,
                                                            URL = HttpContext.Current.Request.RawUrl,
                                                            Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)[Transaction]"
                                                        }, ext);
                                                    }

                                                }
                                                else
                                                {
                                                    LINENotify notify = new LINENotify();
                                                    notify.LineSBErrorSend(new LINENotifyDATA
                                                    {
                                                        Parameter = new { schoolID, transID, transaction.InvoiceID, chargeID },
                                                        Date_Time = DateTime.Now,
                                                        URL = HttpContext.Current.Request.RawUrl,
                                                        Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)[Invoice Error]",
                                                        Error_Message = result.JsonError
                                                    });
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                //transactionSchool.Rollback();

                                                //recorder.AddException(ex);
                                                //recorder.EndSegment();

                                                LINENotify notify = new LINENotify();
                                                notify.LineSBErrorSend(new LINENotifyDATA
                                                {
                                                    Parameter = new { schoolID, transID, transaction.InvoiceID, chargeID },
                                                    Date_Time = DateTime.Now,
                                                    URL = HttpContext.Current.Request.RawUrl,
                                                    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)"
                                                }, ex);
                                            }

                                            //transactionSchool.Commit();

                                            //this.ltrPay.Text = string.Format(@"Operating: {0}", inquiryQRAPI.QR.Status);
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                                        }
                                        else
                                        {
                                            //transactionSchool.Rollback();

                                            //this.ltrPay.Text = string.Format(@"Operating: {0}, Reference Number: {1}", inquiryQRAPI.QR.Status, inquiryQRAPI.QR.ReferenceOrder);
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-ProcessFail"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));

                                            LINENotify notify = new LINENotify();
                                            notify.LineSBErrorSend(new LINENotifyDATA
                                            {
                                                Parameter = new { chargeID, schoolID },
                                                Date_Time = DateTime.Now,
                                                URL = HttpContext.Current.Request.RawUrl,
                                                Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)",
                                                Error_Message = "ทำรายการไม่สำเร็จ/" + inquiryQRAPI.QR.TransactionState + "/" + inquiryQRAPI.QR.Status
                                            });
                                        }
                                    }
                                    else
                                    {
                                        LINENotify notify = new LINENotify();
                                        notify.LineSBErrorSend(new LINENotifyDATA
                                        {
                                            Parameter = new { chargeID, schoolID },
                                            Date_Time = DateTime.Now,
                                            URL = HttpContext.Current.Request.RawUrl,
                                            Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)",
                                            Error_Message = inquiryQRAPI.QR.JsonError
                                        });
                                    }
                                }
                                else
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                                }
                            }
                            catch (Exception err)
                            {
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

                                //transactionSchool.Rollback();

                                LINENotify notify = new LINENotify();
                                notify.LineSBErrorSend(new LINENotifyDATA
                                {
                                    Parameter = new { chargeID, schoolID, transID, amount, fee, userID },
                                    Date_Time = DateTime.Now,
                                    URL = HttpContext.Current.Request.RawUrl,
                                    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(QR CODE)"
                                }, err);
                            }
                            // try catch
                            //}
                            // using transactionSchool 
                        }
                        // using JabJaiEntities
                    }
                    else
                    {
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

                        try
                        {
                            // Check Invoice Paid status
                            int invID = int.Parse(invoiceID);
                            using (PeakengineEntities peakEngine = Connection.PeakengineEntities(ConnectionDB.Read))
                            {
                                schoolID = Convert.ToInt32(Session["K_SchoolID"]);
                                if (source == "newregister")
                                {
                                    int.TryParse(Request["schoolID"], out schoolID);
                                }
                                var jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read));
                                int c = 0;
                                //if (_demo)
                                //{
                                var sql = string.Format("select count(1) from [AccountingDB].[dbo].AccountInvoiceStudent where AccountInvoiceStudentId = {0} and Status='Success' ", invID);
                                c = jabJaiEntities.Database.SqlQuery<int>(sql).FirstOrDefault();
                                //}
                                //else
                                //{
                                //    c = peakEngine.TInvoices.Where(w => w.invoices_Id == invID && w.invoices_status == "Paid").Count();
                                //}

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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                                    return;
                                }

                                //if (_demo)
                                //{
                                //invoice v2
                                var sql2 = string.Format("select count(1) from [AccountingDB].[dbo].AccountInvoiceStudent where AccountInvoiceStudentId = {0}", invID);
                                c = jabJaiEntities.Database.SqlQuery<int>(sql2).FirstOrDefault();
                                //}
                                //else
                                //{
                                //    c = peakEngine.TInvoices.Where(w => w.invoices_Id == invID).Count();                               
                                //}

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
	                                                </div>", invID, GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message03"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                                    return;
                                }
                            }
                            //

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
                                            if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Class"), classLanguage.ClassTH);
                                            studentName = inv.StudentName;
                                            break;
                                        case "en":
                                            if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Class"), classLanguage.ClassEN);
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


                            var f_PaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolData.nCompany && f.Fd_ActiveInvoice == true);
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message01"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));
                                return;
                            }

                            // Set fee
                            //fee = f_PaymentGateway.Fd_FeeInvoice ?? 0;

                            // Get student data
                            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read));

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
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Class"), classLanguage.ClassTH);
                                        studentName = userData.sName + " " + userData.sLastname;
                                        break;
                                    case "en":
                                        if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Class"), classLanguage.ClassEN);
                                        studentName = f_student.sStudentNameEN + " " + f_student.sStudentLastEN;
                                        if (string.IsNullOrEmpty(studentName.Replace(" ", "")))
                                        {
                                            studentName = userData.sName + " " + userData.sLastname;
                                        }
                                        break;
                                }
                            }


                            DateTime currentDate = DateTime.Now.Date;
                            int runNumber = en.KTransactions.Count(w => DbFunctions.TruncateTime(w.CreateDate) == currentDate) + 1;
                            string userId = userData.username.Length > 5 ? userData.username.Substring(userData.username.Length - 5, 5) : userData.username;
                            string refNo = string.Format(@"Q{0:D4}{1}{2:D5}{3}{4}IV", schoolData.nCompany, DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH")), runNumber, userData.cType == "0" ? "S" : "T", userId);  //Request.QueryString["refno"];

                            string shopName = "School Bright";

                            Config config = new Config();

                            OrderAPI orderAPI = new OrderAPI();
                            string headerData = string.Format(@"{{""amount"":{0:0.00}, ""currency"": ""THB"", ""description"": ""PAY TUITION FEES(QR CODE)"", ""source_type"": ""qr"", ""reference_order"": ""{1}"", ""metadata"": [{{""item"": ""top-up"", ""qty"": 1, ""amount"": {0}}}]}}", decimal.Parse(amount) + fee, refNo);
                            orderAPI.CreateOrder(headerData, f_PaymentGateway.Fd_SecretKeyInvoice);


                            //
                            Session["K_SchoolID"] = schoolData.nCompany;

                            int yearID = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));

                            // KTransaction
                            string newGuid = Guid.NewGuid().ToString();

                            KTransaction t = new KTransaction
                            {
                                Year = yearID,
                                GUID = newGuid,
                                OrderID = orderAPI.Order.OrderId,
                                UserID = int.Parse(studentID),
                                ReferenceNo = refNo,
                                Amount = decimal.Parse(amount),
                                CreateDate = DateTime.Now,
                                SchoolID = schoolData.nCompany,
                                InvoiceID = invID
                            };
                            en.KTransactions.Add(t);
                            en.SaveChanges();

                            int transID = t.TransactionID;

                            if (orderAPI.Order.Status != "Error")
                            {
                                // KResOrder
                                string customerID = (orderAPI.Order.Customer == null ? null : orderAPI.Order.Customer.CustomerId);
                                KResOrder o = new KResOrder
                                {
                                    Year = yearID,
                                    TransID = transID,
                                    OrderID = orderAPI.Order.OrderId,
                                    Object = orderAPI.Order.Object,
                                    Amount = orderAPI.Order.Amount,
                                    Currency = orderAPI.Order.Currency,
                                    Description = orderAPI.Order.Description,
                                    SourceType = orderAPI.Order.SourceType,
                                    AdditionalDataMID = orderAPI.Order.AdditionalData.MId,
                                    AdditionalDataTID = orderAPI.Order.AdditionalData.TId,
                                    AdditionalDataSmartpayID = orderAPI.Order.AdditionalData.SmartpayId,
                                    AdditionalDataTerm = orderAPI.Order.AdditionalData.Term,
                                    CustomerID = customerID,
                                    MetaDataItem = orderAPI.Order.MetaData[0].Item,
                                    MetaDataQty = orderAPI.Order.MetaData[0].Qty,
                                    MetaDataAmount = orderAPI.Order.MetaData[0].Amount,
                                    ExpireTimeSeconds = orderAPI.Order.ExpireTimeSeconds,
                                    Created = orderAPI.Order.Created,
                                    Status = orderAPI.Order.Status,
                                    ReferenceOrder = orderAPI.Order.ReferenceOrder,
                                    LiveMode = orderAPI.Order.LiveMode.ToString(),
                                    FailureCode = orderAPI.Order.FailureCode,
                                    FailureMessage = orderAPI.Order.FailureMessage,
                                    CreateDate = DateTime.Now,
                                    SchoolID = schoolData.nCompany
                                };
                                en.KResOrders.Add(o);
                                en.SaveChanges();

                                var stampDate = "";
                                switch (lang)
                                {
                                    case "th": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm น.", new CultureInfo("th-TH")); break;
                                    case "en": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm", new CultureInfo("en-US")); break;
                                }

                                // Render html
                                this.ltrPay.Text = string.Format(@"
	<div class=""section-top"">
        <div class=""card"" style=""border-radius: 25px; margin: 11px; padding: 5px;"">
			<div class=""container"">
				<div class=""section-top"" style=""padding: 0 15px 0 15px;"">
					<div class=""row-topic"">
						<img src=""../../../images/SchoolBrightLogo.png"" alt="""" class=""logo"" />
						<blockquote class=""topic"">
							<p class=""font-2"">{21}</p>
							<p class=""font-1"">{0}</p>
						</blockquote>
					</div>
					<p class=""topic-detail font-2"">{11}</p>
					<div class=""payer"">
                        <p class=""row-label font-2"">{1}</p>
                        <p class=""row-input font-2"">{2}</p>
					</div>
					<div class=""row-highlight"">
						<div class=""row"">
							<p class=""row-label font-2"">{12}</p>
							<p class=""row-input font-2"">{9}</p>
						</div>
						<div class=""row"">
							<p class=""row-label font-2"">{13}</p>
							<p class=""row-input font-2"">{3}</p>
						</div>
						<div class=""row"">
							<p class=""row-label font-2"">{14}</p>
							<p class=""row-input font-2"">{4:#,0.00} <!--{15}--></p>
						</div>
						<div class=""row"" style=""display: none;"">
							<p class=""row-label font-2"">{18}</p>
							<p class=""row-input font-2"">{19:#,0.00} <!--{15}--></p>
						</div>
						<div class=""row last"">
						</div>
					</div>
				</div>
				<div class=""section-bottom"" style=""padding: 0 15px 0 15px;"">
					<div class=""row-summary"">
                        <p class=""row-label font-3"">{16}</p>
						<p class=""row-input font-3"">{20:#,0.00} <!--{15}--></p>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class=""section-bottom"">
		<div class=""confirm-btn"">
            <form method=""post"" action=""CheckoutQR.aspx?lang={10}"">
                <script type=""text/javascript""
                    src=""{8}""
                    data-apikey=""{5}""
                    data-order-id=""{6}""
                    data-name=""{7}""
                    data-currency=""THB""
                    data-amount=""{20:0.00}""
                    data-ref-number=""{3}""
                    data-payment-methods=""qr"">
                </script>
            </form>
		</div>
	</div>
	<script>
		setTimeout(function(){{ 
			$('.confirm-btn button').css({{'width': '92%'}}).css({{'height': '50px'}}).css({{'border-radius': '25px'}}).css({{'margin-bottom': '5px'}});
            $('head').append(""<style>.pay-button[_kpayment] span::after {{content: '{17}'; font-family: THSarabun; font-size: 25px !important;}} .pay-button[_kpayment] span::before {{margin-top: -5px;}}</style>"");
        }}, 2000);
	</script>", stampDate, studentName, studentLevel, refNo, decimal.Parse(amount), f_PaymentGateway.Fd_PublicKeyInvoice, orderAPI.Order.OrderId, shopName, config.UrlJavaScriptAPI, invoiceID, lang,
        /*11*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Title"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Invoice"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-OrderNumber"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Amount"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-BahtUnit"),
        /*16*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Total"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-Confirm"),
        /*18*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Fee"), fee, decimal.Parse(amount) + fee, GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Form-Topic"));

                            }
                            else
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
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message02"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));

                                LINENotify notify = new LINENotify();
                                notify.LineSBErrorSend(new LINENotifyDATA
                                {
                                    Parameter = new { studentID, amount, fee, invoiceID },
                                    Date_Time = DateTime.Now,
                                    URL = HttpContext.Current.Request.RawUrl,
                                    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(GENERATE QR CODE)",
                                    Error_Message = orderAPI.Order.JsonError
                                });
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
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "System-Message02"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutQR.aspx", "Button-BackToMain"));

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
                                Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(GENERATE QR CODE)"
                            }, err);
                        }
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