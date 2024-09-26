using JabjaiEntity.DB;
using JabjaiMainClass;
using KBankAPI;
using MasterEntity;
using AccountingEntity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;

namespace FingerprintPayment.PaymentGateway.KBank.TuitionFees
{
    public partial class CheckoutCard : System.Web.UI.Page
    {
        private int schoolID = 0;
        private string source = "";

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
                //bool _demo = false;
                //if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["demo"]))
                //{
                //    if (ConfigurationManager.AppSettings["demo"] == "true")
                //    {
                //        _demo = true;
                //    }
                //}

                if ((start0001 <= DateTime.Now && DateTime.Now <= end0001) || (start2300 <= DateTime.Now && DateTime.Now <= end2300))
                {
                    string lang = Request.QueryString["lang"]; // th, en
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
    </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-Message04"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Contact01"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Contact02"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));

                    return;
                }


                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    if (!fcommon.PaymentSetting(masterEntities, "Credit Card", "KBank"))
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

                    // Check token kbank
                    string token = Request["token"];
                    string paymentMethods = Request["paymentMethods"];

                    // Check pre init form card
                    string cardType = Request["ctype"]; // Card Type
                    string paymentType = Request["ptype"]; // Payment Type
                    string term = Request["term"]; // Term

                    string studentIDx = Request.QueryString["studentID"];
                    int sIDx = 0;
                    int.TryParse(studentIDx, out sIDx);

                    bool? disableNormalCard = false;
                    string campaignID = "";
                    var userDatax = masterEntities.TUsers.Where(w => w.sID == sIDx).FirstOrDefault();
                    if (userDatax != null)
                    {
                        var schoolDatax = masterEntities.TCompanies.Where(w => w.nCompany == userDatax.nCompany).FirstOrDefault();
                        var f_PaymentGatewayx = masterEntities.TB_PaymentGateway.Where(w => w.Fd_SchoolID == schoolDatax.nCompany && w.Fd_ActiveInvoice == true).FirstOrDefault();
                        if (f_PaymentGatewayx != null)
                        {
                            campaignID = f_PaymentGatewayx.Fd_Campaign_ID;
                            if (f_PaymentGatewayx.DisableNormalCard != null)
                            {
                                disableNormalCard = f_PaymentGatewayx.DisableNormalCard;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(cardType) && !string.IsNullOrEmpty(campaignID))
                    {
                        string lang = Request.QueryString["lang"]; // th, en
                        if (string.IsNullOrEmpty(lang)) lang = "th";
                        switch (lang)
                        {
                            case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                            case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                        }

                        string normalCardTypeEnable = string.Format(@"
                            <label>
                                <input type=""radio"" name=""rdoCardType"" value=""common"" />&nbsp;&nbsp;{0}
                            </label>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-CardType-Common"));
                        string normalCardTypeDescription = string.Format(@"
                            <p style=""margin: -7px 0px 0px 40px; font-size: 26px; line-height: 0.9; color: #666;"">
                                {0}
                            </p>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Desc-CardType-Common"));

                        string cgaCardTypeEnable = string.Format(@"
                            <label>
                                <input type=""radio"" name=""rdoCardType"" value=""cga"" />&nbsp;&nbsp;{0}
                            </label>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-CardType-CGA"));
                        string cgaCardTypeDescription = string.Format(@"
                            <p style=""margin: -7px 0px 0px 40px; font-size: 26px; line-height: 0.9; color: #666;"">
                                {0}
                            </p>
                            <br />", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Desc-CardType-CGA"));

                        //string initFunction = string.Format(@"$(""input[name='rdoCardType']:checked"").change();");
                        string initFunction = "";
                        if ((bool)disableNormalCard)
                        {
                            normalCardTypeEnable = "";
                            normalCardTypeDescription = "";
                            cgaCardTypeEnable = string.Format(@"
                            <label>
                                <input type=""radio"" name=""rdoCardType"" value=""cga"" />&nbsp;&nbsp;{0}
                            </label>
", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-CardType-CGA"));
                        }

                        this.ltrPay.Text = string.Format(@"
    <div class=""section-top"">
        <div class=""card"" style=""border-radius: 25px; margin: 11px; padding: 5px;"">
            <div class=""container"">
                <div class=""section-top"" style=""padding: 0 15px 0 15px;"">
                    <div style=""border-bottom: 2px solid #ccc; display: block; overflow: hidden; margin-top: 25px; padding-right: 15px; padding-left: 15px;"">
                        <p class=""font-3"" style=""text-align: center; margin: 0px; font-weight: bold; color: #ffb8a9;"">{0}</p>
                        <p class=""font-3"" style=""text-align: left; margin: 0px; padding-bottom: 5px;"">
                            {2}
                        </p>
                        {14}
                        <p class=""font-3"" style=""text-align: left; margin: 0px; padding-bottom: 5px;"">
                            {1}
                        </p>
                        {13}
                    </div>
                    <div id=""divPaymentType"" style=""border-bottom: 2px solid #ccc; display: none; overflow: auto; margin-top: 20px; padding-right: 15px; padding-left: 15px;"">
                        <p class=""font-3"" style=""text-align: center;margin: 0px; font-weight: bold; color: #ffb8a9;"">{3}</p>
                        <p class=""font-3"" style=""text-align: left; margin: 0px; padding-bottom: 5px;"">
                            <label>
                                <input type=""radio"" name=""rdoPaymentType"" value=""fullpayment"" />&nbsp;&nbsp;{4}
                            </label>
                            <br />
                            <label>
                                <input type=""radio"" name=""rdoPaymentType"" value=""smartpay"" />&nbsp;&nbsp;{5}
                            </label>
                        </p>
                    </div>
                    <div id=""divTerm"" style=""border-bottom: 2px solid #ccc; display: none; overflow: auto; margin-top: 20px; padding-right: 15px; padding-left: 15px;"">
                        <p class=""font-3"" style=""text-align: center;margin: 0px; font-weight: bold; color: #ffb8a9;"">{6}</p>
                        <p class=""font-3"" style=""text-align: left; margin: 0px; padding-bottom: 5px;"">
                            <select name=""sltTerm"" id=""sltTerm"" class=""font-2"" style=""width: 100%;"">
                                <option value=""3"">3 {7}</option>
                                <option value=""6"">6 {7}</option>
                            </select>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""section-bottom"">
        <div class=""confirm-btn"">
            <button id=""btnNext"" type=""button"" class=""btn-success font-2 pay-button"">{8}</button>
        </div>
    </div>
    <script>
        $(document).ready(function () {{
            $(""input[name='rdoCardType']"").change(function () {{
                switch ($(this).val()) {{
                    case ""common"":
                        $('#divPaymentType').hide();
                        $('#divTerm').hide();
                        break;
                    case ""cga"":
                        $('#divPaymentType').show();
                        switch ($(""input[name='rdoPaymentType']:checked"").val()) {{
                            case ""fullpayment"":
                                $('#divTerm').hide();
                                break;
                            case ""smartpay"":
                                $('#divTerm').show();
                                break;
                        }}
                        break;
                }}
            }});
            $(""input[name='rdoPaymentType']"").change(function () {{
                switch ($(this).val()) {{
                    case ""fullpayment"":
                        $('#divTerm').hide();
                        break;
                    case ""smartpay"":
                        $('#divTerm').show();
                        break;
                }}
            }});
            $(""#btnNext"").click(function () {{
                if(($(""input[name='rdoCardType']:checked"").val() || '') == ''){{
                    alert('{11}');
                    return false;
                }}
                if(($(""input[name='rdoCardType']:checked"").val() || '') == 'cga' && ($(""input[name='rdoPaymentType']:checked"").val() || '') == ''){{
                    alert('{12}');
                    return false;
                }}

                location.replace(""{9}&ctype="" + ($(""input[name='rdoCardType']:checked"").val() || '') + ""&ptype="" + ($(""input[name='rdoPaymentType']:checked"").val() || '') + ""&term="" + $(""#sltTerm"").val());
            }});
            {10}
        }});
    </script>", /*0*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Label-CardType"), normalCardTypeEnable, cgaCardTypeEnable, GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Label-PaymentType"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-PaymentType-FullPayment")
    , /*5*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-PaymentType-SmartPay"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Label-Term"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Label-Month"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-Next"), Request.Url.AbsoluteUri, initFunction
    , /*11*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Alert-CardType"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Alert-PaymentType"), normalCardTypeDescription, cgaCardTypeDescription);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(token) && token.IndexOf("tokn_") != -1)
                        {
                            string lang = Request.QueryString["lang"];
                            if (string.IsNullOrEmpty(lang))
                            {
                                // Retrieve Cookie
                                System.Web.HttpCookie kCookie = Request.Cookies["KPaymentInfo"];
                                if (kCookie != null)
                                {
                                    lang = kCookie["lang"].ToString();
                                }
                                else
                                {
                                    lang = "th";
                                }
                            }

                            switch (lang)
                            {
                                case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                                case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                            }

                            string objectId = Request["objectId"];
                            if (!string.IsNullOrEmpty(objectId))
                            {
                                // After submit OTP
                                string status = Request["status"];
                                if (status == "true")
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));
                                }
                                else
                                {
                                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                        <div class=""card"">
                                                            <div class=""container"">
                                                                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                    <img src=""../../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                    <h1>{0}<br/><p style=""margin: -14px 0px 0px 0px; font-size: 1.3rem; color: darkgrey;"">(After Submit OTP)</p></h1>
                                                                    <p style=""margin: 0px; font-size: 2em; line-height: 1.0;"">{2}</p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                     </div>
                                                     <div class=""section-bottom"">
		                                                <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                            {1}
		                                                </a>
	                                                 </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessFail"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessFail2"));

                                    //string errorMessage = "ทำรายการไม่สำเร็จ(After Submit OTP)";
                                    //// Inquiry Transaction
                                    //// Get ChargeID
                                    //string chargeID = "";
                                    //using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(307, ConnectionDB.Read)))
                                    //{
                                    //    KTransaction transaction = schoolEntities.KTransactions.Where(w => w.TokenID == token).FirstOrDefault();
                                    //    if (transaction != null && !string.IsNullOrEmpty(transaction.ChargeID))
                                    //    {
                                    //        chargeID = transaction.ChargeID;

                                    //        List<TB_PaymentGateway> listPaymentGateway = masterEntities.TB_PaymentGateway.ToList();
                                    //        TB_PaymentGateway schoolPaymentGateway = null;
                                    //        if (transaction.InvoiceID == null)
                                    //        {
                                    //            // Case TopupMoney
                                    //            schoolPaymentGateway = listPaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == transaction.SchoolID && f.Fd_Active == true);
                                    //        }
                                    //        else
                                    //        {
                                    //            // Case Invoice Payment
                                    //            schoolPaymentGateway = listPaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == transaction.SchoolID && f.Fd_ActiveInvoice == true);
                                    //        }

                                    //        Config config = new Config();
                                    //        config.Method = "GET";
                                    //        config.APISecretKey = transaction.InvoiceID == null ? schoolPaymentGateway.Fd_SecretKey : schoolPaymentGateway.Fd_SecretKeyInvoice;

                                    //        InquiryChargeAPI inquiryChargeAPI = new InquiryChargeAPI(config);
                                    //        inquiryChargeAPI.InquiryCharge(chargeID);

                                    //        if (inquiryChargeAPI.Charge.Status != "Error")
                                    //        {
                                    //            errorMessage = string.Format(@"ทำรายการไม่สำเร็จ(After Submit OTP)[FailureCode: {0}, FailureMessage: {1}]", inquiryChargeAPI.Charge.FailureCode, inquiryChargeAPI.Charge.FailureMessage);
                                    //        }
                                    //    }
                                    //}

                                    //LINENotify notify = new LINENotify();
                                    //notify.LineSBErrorSend(new LINENotifyDATA
                                    //{
                                    //    Parameter = new { token, status, paymentMethods },
                                    //    Date_Time = DateTime.Now,
                                    //    URL = HttpContext.Current.Request.RawUrl,
                                    //    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)",
                                    //    Error_Message = errorMessage
                                    //});
                                }
                            }
                            else
                            {
                                // After submit form (Credit Card)
                                int transID = Convert.ToInt32(Session["K_TransID"]);
                                int schoolID = Convert.ToInt32(Session["K_SchoolID"]);
                                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                                {
                                    //using (var transactionSchool = en.Database.BeginTransaction())
                                    //{
                                    try
                                    {
                                        KTransaction transaction = en.KTransactions.Where(w => w.SchoolID == schoolID && w.TransactionID == transID).FirstOrDefault();
                                        if (transaction != null)
                                        {
                                            var f_PaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolID && f.Fd_ActiveInvoice == true);
                                            //f_PaymentGateway.Fd_SecretKey = "skey_test_228DXmr4Ph3g1LqVo0NuWL7UOOFQ70GULCr";
                                            if (f_PaymentGateway != null)
                                            {
                                                Config config = new Config();
                                                config.APISecretKey = f_PaymentGateway.Fd_SecretKeyInvoice;

                                                // Set fee
                                                decimal feeRate = f_PaymentGateway.Fd_FeeInvoice ?? 0;
                                                decimal fee = (decimal)transaction.Amount * (feeRate / 100);

                                                // CGA Config
                                                string cgaConfig = "";
                                                cardType = Request["ctype"]; // Card Type: common, cga
                                                paymentType = Request["ptype"]; // Payment Type: fullpayment, smartpay
                                                term = Request["term"]; // Term: 3, 6

                                                // CGA Config
                                                if (cardType == "cga" && paymentType == "fullpayment")
                                                {
                                                    //, ""additional_data"": {{""mid"":""401123456789"",""tid"":""12345678"",""smartpay_id"":""888888"",""term"":""10"",""campaign_id"":""00000001""}}
                                                    string mid = "", tid = "";
                                                    if (!string.IsNullOrEmpty(f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Full_ID))
                                                    {
                                                        mid = string.Format(@"""mid"":""{0}"",", f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Full_ID);
                                                    }
                                                    if (!string.IsNullOrEmpty(f_PaymentGateway.Fd_CGA_Terminal_CreditCard_Full_ID))
                                                    {
                                                        tid = string.Format(@"""tid"":""{0}"",", f_PaymentGateway.Fd_CGA_Terminal_CreditCard_Full_ID);
                                                    }

                                                    cgaConfig = string.Format(@", ""additional_data"": {{{0}{1}""campaign_id"":""{2}""}}", mid, tid, f_PaymentGateway.Fd_Campaign_ID);
                                                }
                                                else if (cardType == "cga" && paymentType == "smartpay")
                                                {
                                                    cgaConfig = string.Format(@", ""additional_data"": {{""mid"":""{0}"",""tid"":""{1}"",""smartpay_id"":""{2}"",""term"":""{3}"",""campaign_id"":""{4}""}}", f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Installment_ID, f_PaymentGateway.Fd_CGA_Terminal_CreditCard_Installment_ID, f_PaymentGateway.Fd_CGA_SmartPay_CreditCard_Installment_ID, term, f_PaymentGateway.Fd_Campaign_ID);
                                                }

                                                // No calculate fee on cga type
                                                if (cardType == "cga")
                                                {
                                                    fee = 0;
                                                }

                                                // Call Charge API
                                                ChargeAPI chargeAPI = new ChargeAPI(config);
                                                string headerData = string.Format(@"{{""amount"": {0:0.00}, ""currency"": ""THB"", ""description"": ""PAY TUITION FEES(CREDIT CARD)"", ""source_type"": ""card"", ""mode"": ""token"", ""token"": ""{1}"", ""reference_order"" : ""{2}"", ""ref_1"": ""{3}"", ""ref_2"": ""{4}""{5}}}", transaction.Amount + fee, token, transaction.ReferenceNo, "", "", cgaConfig);
                                                chargeAPI.CreateCharge(headerData);

                                                if (chargeAPI.Charge.Status != "Error")
                                                {
                                                    
                                                        try
                                                        {
                                                            // Update Transaction (KTransaction)
                                                            transaction.ChargeID = chargeAPI.Charge.ChargeID;
                                                            transaction.TokenID = token;
                                                            transaction.CardID = chargeAPI.Charge.Source.CardObjectId;
                                                            transaction.UpdatedDate = DateTime.Now;

                                                            // Save Response Charge
                                                            KResCharge o = new KResCharge
                                                            {
                                                                Year = transaction.Year,
                                                                ResChargeID = chargeAPI.Charge.ChargeID,
                                                                SchoolID = schoolID,
                                                                TransID = transID,
                                                                Data = chargeAPI.JsonCharge,
                                                                Tstamp = DateTime.Now
                                                            };
                                                            en.KResCharges.Add(o);

                                                            en.SaveChanges();

                                                            if (chargeAPI.Charge.TransactionState == "Authorized" && chargeAPI.Charge.Status == "success")
                                                            {
                                                                // Non 3D Secure
                                                                // Call Invoice Payment function
                                                                KInvoicePaymentV2 kInvoicePayment = new KInvoicePaymentV2();
                                                                var result = kInvoicePayment.PaymentV2((int)transaction.InvoiceID, (decimal)transaction.Amount, "K-Payment Gateway(Credit Card)", schoolID);
                                                                if (result.Status != "Error")
                                                                {
                                                                    transaction.PaidPaymentID = result.PaidPaymentId;

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
                                                                else
                                                                {
                                                                    LINENotify notify = new LINENotify();
                                                                    notify.LineSBErrorSend(new LINENotifyDATA
                                                                    {
                                                                        Parameter = new { schoolID, transID, transaction.InvoiceID, token },
                                                                        Date_Time = DateTime.Now,
                                                                        URL = HttpContext.Current.Request.RawUrl,
                                                                        Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)[Invoice Error]",
                                                                        Error_Message = result.JsonError
                                                                    });
                                                                }

                                                                
                                                                

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
	                                                                        </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));  
                                                            }
                                                            else if (chargeAPI.Charge.TransactionState == "Pre-Authorized" && chargeAPI.Charge.Status == "success")
                                                            {
                                                                // 3D Secure
                                                                if (!string.IsNullOrEmpty(chargeAPI.Charge.RedirectUrl))
                                                                {
                                                                    // Add Cookie
                                                                    System.Web.HttpCookie kCookie = new System.Web.HttpCookie("KPaymentInfo");
                                                                    kCookie.Expires = DateTime.Now.AddHours(1);
                                                                    kCookie["lang"] = lang;
                                                                    Response.Cookies.Add(kCookie);

                                                                    Response.Redirect(chargeAPI.Charge.RedirectUrl, false);

                                                                    
                                                                }
                                                                else
                                                                {
                                                                    

                                                                    this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                                                    <div class=""card"">
                                                                                        <div class=""container"">
                                                                                            <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                                                <img src=""../../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                                                <h1>{0}(1)</h1>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                 </div>
                                                                                 <div class=""section-bottom"">
		                                                                            <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                                                        {1}
		                                                                            </a>
	                                                                             </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessFail"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));

                                                                    LINENotify notify = new LINENotify();
                                                                    notify.LineSBErrorSend(new LINENotifyDATA
                                                                    {
                                                                        Parameter = new { schoolID, transID, transaction.InvoiceID, token },
                                                                        Date_Time = DateTime.Now,
                                                                        URL = HttpContext.Current.Request.RawUrl,
                                                                        Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)",
                                                                        Error_Message = "ทำรายการไม่สำเร็จ(1)/" + chargeAPI.Charge.TransactionState + "/" + chargeAPI.Charge.Status
                                                                    });
                                                                }
                                                            }
                                                            else
                                                            {
                                                                

                                                                this.ltrPay.Text = string.Format(@"<div class=""section-top"">
                                                                                <div class=""card"">
                                                                                    <div class=""container"">
                                                                                        <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                                                                                            <img src=""../../../images/exclamation-256.jpg"" style=""width: 284px; height: 278px;"" />
                                                                                            <h1>{0}(2)</h1>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                             </div>
                                                                             <div class=""section-bottom"">
		                                                                        <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">
                                                                                    {1}
		                                                                        </a>
	                                                                         </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessFail"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));

                                                                LINENotify notify = new LINENotify();
                                                                notify.LineSBErrorSend(new LINENotifyDATA
                                                                {
                                                                    Parameter = new { schoolID, transID, transaction.InvoiceID, token, chargeAPI.Charge.ChargeID },
                                                                    Date_Time = DateTime.Now,
                                                                    URL = HttpContext.Current.Request.RawUrl,
                                                                    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)",
                                                                    Error_Message = "ทำรายการไม่สำเร็จ(2)/" + chargeAPI.Charge.TransactionState + "/" + chargeAPI.Charge.Status
                                                                });
                                                            }
                                                        }
                                                        catch (Exception ext)
                                                        {
                                                           

                                                            LINENotify notify = new LINENotify();
                                                            notify.LineSBErrorSend(new LINENotifyDATA
                                                            {
                                                                Parameter = new { schoolID, transID, transaction.InvoiceID, token },
                                                                Date_Time = DateTime.Now,
                                                                URL = HttpContext.Current.Request.RawUrl,
                                                                Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)[Transaction]"
                                                            }, ext);
                                                        }
                                                    
                                                }
                                                else
                                                {
                                                    LINENotify notify = new LINENotify();
                                                    notify.LineSBErrorSend(new LINENotifyDATA
                                                    {
                                                        Parameter = new { schoolID, transID, transaction.InvoiceID, token },
                                                        Date_Time = DateTime.Now,
                                                        URL = HttpContext.Current.Request.RawUrl,
                                                        Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)[API Error]",
                                                        Error_Message = chargeAPI.Charge.JsonError
                                                    });
                                                }
                                            }
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
                                        fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('[CheckoutCard(CREDIT CARD)/Error]Json Data:{0}')", jsonString));

                                        //transactionSchool.Rollback();

                                        LINENotify notify = new LINENotify();
                                        notify.LineSBErrorSend(new LINENotifyDATA
                                        {
                                            Parameter = new { schoolID, transID, token },
                                            Date_Time = DateTime.Now,
                                            URL = HttpContext.Current.Request.RawUrl,
                                            Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(CREDIT CARD)"
                                        }, err);
                                    }
                                    // try catch
                                    //}
                                    // using transactionSchool 
                                }
                                // using JabJaiEntities
                            }
                        }
                        else
                        {
                            // Init form card
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
                                    var jabJaiEntities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read));
                                    int c = 0;
                                    //if (_demo)
                                    //{
                                    //invoice V2
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
	                                                </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-ProcessComplete"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));
                                        return;
                                    }

                                    //if (_demo)
                                    //{
                                    //invoice V2
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
	                                                </div>", invID, GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-Message03"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));
                                        return;
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
                                //f_PaymentGateway.Fd_PublicKey = "pkey_test_228aJWI1jjX3xN5U8xh14kxBSfXtSi5HWdk";
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
	                                             </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-Message01"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));
                                    return;
                                }

                                // Set fee
                                decimal feeRate = f_PaymentGateway.Fd_FeeInvoice ?? 0;
                                fee = decimal.Parse(amount) * (feeRate / 100);

                                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                                {
                                    // Get student data
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
                                                if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Class"), classLanguage.ClassTH);
                                                studentName = userData.sName + " " + userData.sLastname;
                                                break;
                                            case "en":
                                                if (classLanguage != null) studentLevel = string.Format(@"{0} {1}", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Class"), classLanguage.ClassEN);
                                                studentName = f_student.sStudentNameEN + " " + f_student.sStudentLastEN;
                                                if (string.IsNullOrEmpty(studentName.Replace(" ", "")))
                                                {
                                                    studentName = userData.sName + " " + userData.sLastname;
                                                }
                                                break;
                                        }

                                    }

                                    // Check Card Type
                                    string cgaConfig = "";
                                    cardType = Request["ctype"]; // Card Type: common, cga
                                    paymentType = Request["ptype"]; // Payment Type: fullpayment, smartpay
                                    term = Request["term"]; // Term: 3, 6

                                    string cgaRN = "";
                                    if (cardType == "cga" && paymentType == "fullpayment")
                                    {
                                        cgaRN = "GF";
                                    }
                                    else if (cardType == "cga" && paymentType == "smartpay")
                                    {
                                        cgaRN = "GS";
                                    }

                                    // No calculate fee on cga type
                                    if (cardType == "cga")
                                    {
                                        fee = 0;
                                    }

                                    DateTime currentDate = DateTime.Now.Date;
                                    int runNumber = en.KTransactions.Count(w => DbFunctions.TruncateTime(w.CreateDate) == currentDate) + 1;
                                    string username4 = userData.username.Length > 5 ? userData.username.Substring(userData.username.Length - 5, 5) : userData.username;
                                    string refNo = string.Format(@"C{0:D4}{1}{2:D5}{3}{4}IV{5}", schoolData.nCompany, DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH")), runNumber, userData.cType == "0" ? "S" : "T", username4, cgaRN);

                                    string shopName = "School Bright";

                                    Config config = new Config();

                                    // Save Init Transaction

                                    int yearID = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));

                                    // KTransaction
                                    string newGuid = Guid.NewGuid().ToString();

                                    KTransaction t = new KTransaction
                                    {
                                        Year = yearID,
                                        GUID = newGuid,
                                        UserID = int.Parse(studentID),
                                        ReferenceNo = refNo,
                                        Amount = decimal.Parse(amount),
                                        CreateDate = DateTime.Now,
                                        SchoolID = schoolData.nCompany,
                                        InvoiceID = int.Parse(invoiceID)
                                    };
                                    en.KTransactions.Add(t);
                                    en.SaveChanges();

                                    int transID = t.TransactionID;

                                    // Save some data to session
                                    Session["K_TransID"] = transID;
                                    Session["K_SchoolID"] = schoolData.nCompany;

                                    var stampDate = "";
                                    switch (lang)
                                    {
                                        case "th": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm น.", new CultureInfo("th-TH")); break;
                                        case "en": stampDate = DateTime.Now.ToString("dd MMM yy HH:mm", new CultureInfo("en-US")); break;
                                    }

                                    // CGA Config
                                    if (cardType == "cga" && paymentType == "fullpayment")
                                    {
                                        if (!string.IsNullOrEmpty(f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Full_ID))
                                        {
                                            cgaConfig += string.Format(@"
                    data-mid=""{0}""", f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Full_ID);
                                        }

                                        cgaConfig += string.Format(@"
                    data-campaign-id=""{0}""", f_PaymentGateway.Fd_Campaign_ID);
                                    }
                                    else if (cardType == "cga" && paymentType == "smartpay")
                                    {
                                        cgaConfig += string.Format(@"
                    data-mid=""{0}""
                    data-smartpay-id=""{1}""
                    data-campaign-id=""{2}""", f_PaymentGateway.Fd_CGA_Merchant_CreditCard_Installment_ID, f_PaymentGateway.Fd_CGA_SmartPay_CreditCard_Installment_ID, f_PaymentGateway.Fd_Campaign_ID);
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
							<p class=""font-2"">{24}</p>
							<p class=""font-1"">{0}</p>
						</blockquote>
					</div>
					<p class=""topic-detail font-2"">{10}</p>
					<div class=""payer"">
                        <p class=""row-label font-2"">{1}</p>
                        <p class=""row-input font-2"">{2}</p>
					</div>
					<div class=""row-highlight"">
						<div class=""row"">
							<p class=""row-label font-2"">{11}</p>
							<p class=""row-input font-2"">{3}</p>
						</div>
						<div class=""row"">
							<p class=""row-label font-2"">{12}</p>
							<p class=""row-input font-2"">{4}</p>
						</div>
						<div class=""row"">
							<p class=""row-label font-2"">{13}</p>
							<p class=""row-input font-2"">{5:#,0.00} <!--{14}--></p>
						</div>
						<div class=""row"">
							<p class=""row-label font-2"">{21}</p>
							<p class=""row-input font-2"">{22:#,0.00} <!--{14}--></p>
						</div>
						<div class=""row last"">
						</div>
					</div>
				</div>
				<div class=""section-bottom"" style=""padding: 0 15px 0 15px;"">
					<div class=""row-summary"">
                        <p class=""row-label font-3"">{15}</p>
						<p class=""row-input font-3"">{23:#,0.00} <!--{14}--></p>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class=""section-bottom"">
		<div class=""confirm-btn"">
            <form method=""post"" action=""CheckoutCard.aspx?lang={9}&ctype={18}&ptype={19}&term={20}"">
                <script type=""text/javascript""
                    src=""{6}""
                    data-apikey=""{7}""
                    data-amount=""{23:0.00}""
                    data-currency=""THB""
                    data-payment-methods=""card""
                    data-name=""{8}""{17}>
                </script>
            </form>
		</div>
	</div>
	<script>
		setTimeout(function(){{ 
			$('.confirm-btn button').css({{'width': '92%'}}).css({{'height': '50px'}}).css({{'border-radius': '25px'}}).css({{'margin-bottom': '5px'}});
            $('head').append(""<style>.pay-button[_kpayment] span::after {{content: '{16}'; font-family: THSarabun; font-size: 25px !important;}} .pay-button[_kpayment] span::before {{margin-top: -5px;}}</style>"");
        }}, 2000);
	</script>", stampDate, studentName, studentLevel, invoiceID, refNo, decimal.Parse(amount), config.UrlJavaScriptAPI, f_PaymentGateway.Fd_PublicKeyInvoice, shopName, lang,
        /*10*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Title"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Invoice"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-OrderNumber"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Amount"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-BahtUnit"),
        /*15*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Total"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-Confirm"), cgaConfig, cardType, paymentType, term,
        /*21*/GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Fee"), fee, decimal.Parse(amount) + fee, GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Form-Topic"));

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
	                                            </div>", GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "System-Message02"), GetGlobalResourceObject("PaymentGateway_KBank_TuitionFees_CheckoutCard.aspx", "Button-BackToMain"));

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
                                    Error_Method = "PAYMENTGATEWAY - PAY TUITION FEES(GENERATE CREDIT CARD FORM)"
                                }, err);
                            }
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