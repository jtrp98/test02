using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using QRCoder;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FingerprintPayment.PaymentGateway.KBank.KBankPromptPay
{
    /// <summary>
    /// Summary description for InquiryTx
    /// </summary>
    public class InquiryTx : IHttpHandler
    {
        string kbankUrl = ConfigurationManager.AppSettings["KBankUrl"].ToString();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool isSuccess = true;
            string message = "Void QRCode Successfully!";

            int schoolID = 0;
            string PartnerTxnUid = "";
            int userID = 0;

            ResponseInquiryTx responseData = new ResponseInquiryTx();

            try
            {
                schoolID = int.Parse(context.Request.QueryString["SchoolID"]);
                PartnerTxnUid = context.Request.QueryString["PartnerTxnUid"].ToString();
                userID = int.Parse(context.Request.QueryString["UserID"]);


                // Get school data 
                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {

                    var schoolPaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == schoolID && f.Fd_PromptPayActive == 1);

                    string authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(@"{0}:{1}", schoolPaymentGateway.Fd_ConsumerID, schoolPaymentGateway.Fd_ConsumerSecret)));


                    // Get Access Token
                    var client = new RestClient(kbankUrl + "/oauth/token");
                    client.Timeout = -1;


                    // Add certificate
                    var certificate = new X509Certificate2(@"C:\Certificate\wildcard_schoolbright_co.pfx", ConfigurationManager.AppSettings["KBankSSLPassword"].ToString());
                    client.ClientCertificates = new X509CertificateCollection() { certificate };

                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    request.AddHeader("Authorization", "Basic " + authBase64);
                    request.AddParameter("grant_type", "client_credentials");
                    IRestResponse response = client.Execute(request);
                    //Console.WriteLine(response.Content);

                    var responseOAuth20 = JsonConvert.DeserializeObject<ResponseOAuth20>(response.Content);


                    // Inquiry Tx
                    RequestBodyInquiryTx requestBodyInquiryTx = null;

                    using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                    {
                        var kPromptPayTransaction = schoolEntities.KPromptPayTransactions.Where(w => w.PartnerTxnUID == PartnerTxnUid).FirstOrDefault();
                        if (kPromptPayTransaction != null)
                        {
                            // 030700133123464052500001Q // [schoolID:4][shopID:5][imei:4][date:6][runNo:5][Q|C]
                            DateTime currentDate = DateTime.Now.Date;
                            int runNumber = schoolEntities.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(*)+1 FROM KPromptPayTransaction WHERE ShopID = {0} AND CreateDate BETWEEN '{1:yyyy-MM-dd} 00:00:00' AND '{1:yyyy-MM-dd} 23:59:59'", kPromptPayTransaction.ShopID, currentDate)).FirstOrDefault();
                            string refNo = string.Format(@"{0:D4}{1:D5}{2}{3}{4:D5}{5}", schoolID
                                , kPromptPayTransaction.ShopID
                                , (kPromptPayTransaction.IMEI.Length >= 4 ? kPromptPayTransaction.IMEI.Substring(0, 4) : kPromptPayTransaction.IMEI.PadLeft(4, '0'))
                                , DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH"))
                                , runNumber
                                , (kPromptPayTransaction.QRType == "3" ? "Q" : (kPromptPayTransaction.QRType == "4" ? "C" : "N")));

                            requestBodyInquiryTx = new RequestBodyInquiryTx
                            {
                                MerchantID = schoolPaymentGateway.Fd_MerchantMID,
                                OrigPartnerTxnUid = PartnerTxnUid,
                                PartnerID = schoolPaymentGateway.Fd_PartnerID,
                                PartnerSecret = schoolPaymentGateway.Fd_PartnerSecret,
                                PartnerTxnUID = refNo,
                                RequestDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK")
                            };


                            // Save to db
                            schoolEntities.KPromptPayTransactions.Add(new KPromptPayTransaction
                            {
                                SchoolID = schoolID,
                                ShopID = kPromptPayTransaction.ShopID,
                                IMEI = kPromptPayTransaction.IMEI,
                                PartnerTxnUID = refNo,
                                QRType = kPromptPayTransaction.QRType,
                                TxnAmount = kPromptPayTransaction.TxnAmount,
                                TxnCurrencyCode = "THB",
                                CreateDate = DateTime.Now,
                                UserID = userID,
                                OrigPartnerTxnUID = PartnerTxnUid,
                                Method = "Inquiry"
                            });

                            schoolEntities.SaveChanges();

                            var jsonRequestBody = JsonConvert.SerializeObject(requestBodyInquiryTx);


                            // Inquiry QRCode
                            client.BaseUrl = new Uri(kbankUrl + "/v1/qrpayment/inquiry");

                            request = new RestRequest(Method.POST);
                            request.AddHeader("Content-Type", "application/json");
                            request.AddHeader("Authorization", "Bearer " + responseOAuth20.AccessToken);
                            request.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                            response = client.Execute(request);
                            //Console.WriteLine(response.Content);

                            responseData = JsonConvert.DeserializeObject<ResponseInquiryTx>(response.Content);

                            var inquiryTransaction = schoolEntities.KPromptPayTransactions.Where(w => w.PartnerTxnUID == responseData.PartnerTxnUID).FirstOrDefault();
                            if (inquiryTransaction != null)
                            {
                                inquiryTransaction.ErrorCode = responseData.ErrorCode;
                                inquiryTransaction.ErrorDesc = responseData.ErrorDesc;
                                inquiryTransaction.StatusCode = responseData.StatusCode;
                                inquiryTransaction.LoyaltyId = responseData.LoyaltyId;
                                inquiryTransaction.TxnNo = responseData.TxnNo;
                                inquiryTransaction.TxnStatus = responseData.TxnStatus;

                                schoolEntities.SaveChanges();
                            }
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

                //SqlConnection _conn = new SqlConnection(strconn);

                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Error Message:[PaymentGateway/KBank/KBankPromptPay/InquiryTx.ashx]:{0}')", err.Message.Replace("'", "")));

                LINENotify notify = new LINENotify();
                notify.LineSBErrorSend(new LINENotifyDATA
                {
                    Parameter = new { schoolID, PartnerTxnUid },
                    Date_Time = DateTime.Now,
                    URL = "https://dev.schoolbright.co/PaymentGateway/KBank/KBankPromptPay/InquiryTx.ashx",
                    Error_Method = "KBankPromptPay - Inquiry QRCode"
                }, err);

                isSuccess = false;
                message = err.Message;
            }

            var result = new { success = isSuccess, message, responseData };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }

    public class RequestBodyInquiryTx
    {
        [JsonProperty(PropertyName = "merchantId")]
        public string MerchantID { get; set; }

        [JsonProperty(PropertyName = "origPartnerTxnUid")]
        public string OrigPartnerTxnUid { get; set; }

        [JsonProperty(PropertyName = "partnerId")]
        public string PartnerID { get; set; }

        [JsonProperty(PropertyName = "partnerSecret")]
        public string PartnerSecret { get; set; }

        [JsonProperty(PropertyName = "partnerTxnUid")]
        public string PartnerTxnUID { get; set; }

        [JsonProperty(PropertyName = "requestDt")]
        public string RequestDt { get; set; }
    }

    public class ResponseInquiryTx
    {
        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorDesc")]
        public string ErrorDesc { get; set; }

        [JsonProperty(PropertyName = "loyaltyId")]
        public string LoyaltyId { get; set; }

        [JsonProperty(PropertyName = "partnerId")]
        public string PartnerID { get; set; }

        [JsonProperty(PropertyName = "partnerTxnUid")]
        public string PartnerTxnUID { get; set; }

        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty(PropertyName = "txnNo")]
        public string TxnNo { get; set; }

        [JsonProperty(PropertyName = "txnStatus")]
        public string TxnStatus { get; set; }
    }

}