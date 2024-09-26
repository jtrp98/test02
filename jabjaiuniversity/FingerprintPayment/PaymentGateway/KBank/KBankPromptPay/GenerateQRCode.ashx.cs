using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using QRCoder;
using RestSharp;
using Sentry;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FingerprintPayment.PaymentGateway.KBank.KBankPromptPay
{
    /// <summary>
    /// Summary description for GenerateQRCode
    /// </summary>
    public class GenerateQRCode : IHttpHandler
    {
        string kbankUrl = ConfigurationManager.AppSettings["KBankUrl"].ToString();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool isSuccess = true;
            string message = "Generate QRCode Successfully!";
            string resDescTH = "", resDescEN = ""; 



            GenPara genPara = new GenPara();

            QRCodeModel data = new QRCodeModel();

            ResponseOAuth20 responseOAuth20 = new ResponseOAuth20();
            IRestResponse responseD = null;
            ResponseGenerateThaiQRCode responseGenerateThaiQRCode = null;

            try
            {
                string jsonString = string.Empty;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonString = reader.ReadToEnd();
                }

                genPara = JsonConvert.DeserializeObject<GenPara>(jsonString);

                if (genPara.appName == "win_desktop(1.0.6)")
                {
                    throw new Exception("win_desktop(1.0.6) not available");
                }

                //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);
                //SqlConnection _conn = new SqlConnection(strconn);
                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Data[PaymentGateway/KBank/KBankPromptPay/GenerateQRCode.ashx]{0}')", JsonConvert.SerializeObject(genPara)));




                // Get school data 
                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {

                    var schoolPaymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == genPara.schoolID && f.Fd_PromptPayActive == 1);

                    string Fd_ConsumerID = schoolPaymentGateway.Fd_ConsumerID;
                    string Fd_ConsumerSecret = schoolPaymentGateway.Fd_ConsumerSecret;

                    string authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(@"{0}:{1}", Fd_ConsumerID, Fd_ConsumerSecret)));

                    if (!fcommon.PaymentSetting(masterEntities, "Canteen QRCode", "KBank"))
                    {
                        isSuccess = false;
                        message = "Server Maintenance";
                        data = null;
                        resDescTH = "ปิดระบบพร้อมเพย์ชั่วคราว เนื่องจากระบบขัดข้อง\r\nเจ้าหน้าที่กำลังดำเนินการแก้ไขอย่างเร่งด่วน";
                        resDescEN = "PromptPay system is temporarily closed due to a system failure.\r\nThe officials is working to fix it urgently.";
                    }
                    else
                    {
                        KPromptPayAccessToken kPromptPayAccessToken = null;
                        using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(genPara.schoolID, ConnectionDB.Read)))
                        {
                            kPromptPayAccessToken = schoolEntities.KPromptPayAccessToken.Where(w => w.AccessToken != null).OrderByDescending(o => o.CreateDate).FirstOrDefault();

                            bool getNewAccessToken = true;
                            if (kPromptPayAccessToken != null)
                            {
                                // Check flag to [getNewAccessToken]
                                getNewAccessToken = kPromptPayAccessToken.CreateDate?.AddSeconds(int.Parse(kPromptPayAccessToken.ExpiresIn)).AddMinutes(-2) < DateTime.Now;

                                // Set data to [responseOAuth20], [responseD]
                                responseOAuth20.AccessToken = kPromptPayAccessToken.AccessToken;
                                responseOAuth20.ClientID = kPromptPayAccessToken.ClientID;
                                responseOAuth20.DeveloperEmail = kPromptPayAccessToken.DeveloperEmail;
                                responseOAuth20.ExpiresIn = kPromptPayAccessToken.ExpiresIn;
                                responseOAuth20.Scope = kPromptPayAccessToken.Scope;
                                responseOAuth20.Status = kPromptPayAccessToken.Status;
                                responseOAuth20.TokenType = kPromptPayAccessToken.TokenType;

                                responseD = new RestResponse
                                {
                                    ResponseStatus = ResponseStatus.None,
                                    StatusCode = HttpStatusCode.NoContent,
                                    StatusDescription = "OK",
                                    ErrorMessage = null
                                };
                            }

                            if (getNewAccessToken)
                            {
                                // Get Access Token
                                //ServicePointManager.Expect100Continue = true;
                                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
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

                                responseOAuth20 = JsonConvert.DeserializeObject<ResponseOAuth20>(response.Content);
                                responseD = response;

                                // Save access token to db
                                var accessTokenObj = new KPromptPayAccessToken
                                {
                                    AccessToken = responseOAuth20.AccessToken,
                                    ClientID = responseOAuth20.ClientID,
                                    DeveloperEmail = responseOAuth20.DeveloperEmail,
                                    ExpiresIn = responseOAuth20.ExpiresIn,
                                    Scope = responseOAuth20.Scope,
                                    Status = responseOAuth20.Status,
                                    TokenType = responseOAuth20.TokenType,
                                    CreateDate = DateTime.Now
                                };
                                schoolEntities.KPromptPayAccessToken.Add(accessTokenObj);
                                schoolEntities.SaveChanges();
                            }
                        }


                        // Generate PartnerTxnUID
                        RequestBodyGenerateThaiQRCode requestBodyGenerateThaiQRCode = null;
                        using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(genPara.schoolID, ConnectionDB.Read)))
                        {
                            // 03070013300123464052500001Q // [schoolID:4][shopID:5][imei:6][date:6][runNo:5][Q|C]
                            DateTime currentDate = DateTime.Now.Date;
                            int runNumber = schoolEntities.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(*)+1 FROM KPromptPayTransaction WHERE ShopID = {0} AND CreateDate BETWEEN '{1:yyyy-MM-dd} 00:00:00' AND '{1:yyyy-MM-dd} 23:59:59'", genPara.shopID, currentDate)).FirstOrDefault();
                            string refNo = string.Format(@"{0:D4}{1:D5}{2}{3}{4:D5}{5}", genPara.schoolID
                                , genPara.shopID
                                , (genPara.imei.Length >= 6 ? genPara.imei.Substring(genPara.imei.Length - 6) : genPara.imei.PadLeft(6, '0'))
                                , DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH"))
                                , runNumber
                                , (genPara.type == "3" ? "Q" : (genPara.type == "4" ? "C" : "N")));

                            requestBodyGenerateThaiQRCode = new RequestBodyGenerateThaiQRCode
                            {
                                MerchantID = schoolPaymentGateway.Fd_MerchantMID,
                                MetaData = "",
                                PartnerID = schoolPaymentGateway.Fd_PartnerID,
                                PartnerSecret = schoolPaymentGateway.Fd_PartnerSecret,
                                PartnerTxnUID = refNo,
                                QRType = genPara.type,
                                Reference1 = "",
                                Reference2 = "",
                                Reference3 = "",
                                Reference4 = "",
                                RequestDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"),
                                TxnAmount = genPara.amount.ToString("0.00"),
                                TxnCurrencyCode = "THB"
                            };

                            // Save to db
                            schoolEntities.KPromptPayTransactions.Add(new KPromptPayTransaction
                            {
                                SchoolID = genPara.schoolID,
                                ShopID = genPara.shopID,
                                IMEI = genPara.imei,
                                PartnerTxnUID = refNo,
                                QRType = genPara.type,
                                TxnAmount = genPara.amount,
                                TxnCurrencyCode = "THB",
                                CreateDate = DateTime.Now,
                                UserID = genPara.userID,
                                Method = "Create",
                                AppName = genPara.appName,
                                SellDetail = genPara.sellDetail
                            });

                            schoolEntities.SaveChanges();
                        }

                        var jsonRequestBody = JsonConvert.SerializeObject(requestBodyGenerateThaiQRCode);


                        // Generate QRCode
                        var client2 = new RestClient(kbankUrl + "/v1/qrpayment/request");
                        client2.Timeout = -1;

                        // Add certificate
                        var certificate2 = new X509Certificate2(@"C:\Certificate\wildcard_schoolbright_co.pfx", ConfigurationManager.AppSettings["KBankSSLPassword"].ToString());
                        client2.ClientCertificates = new X509CertificateCollection() { certificate2 };

                        var request2 = new RestRequest(Method.POST);
                        request2.AddHeader("Content-Type", "application/json");
                        request2.AddHeader("Authorization", "Bearer " + responseOAuth20.AccessToken);
                        request2.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                        IRestResponse response2 = client2.Execute(request2);

                        // TODO: Check return html text and save it to page html
                        responseGenerateThaiQRCode = JsonConvert.DeserializeObject<ResponseGenerateThaiQRCode>(response2.Content);

                        data.qrCode = responseGenerateThaiQRCode.QRCode;
                        data.qrCodeBase64 = QRCodeFunction.Create(responseGenerateThaiQRCode.QRCode, QRCodeGenerator.ECCLevel.H).Replace("data:image/png;base64,", "");
                    }
                }
            }
            catch (Exception err)
            {
                SentrySdk.CaptureException(err);
                
                //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                //SqlConnection _conn = new SqlConnection(strconn);

                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Error Message:[api/KBankPromptPay/KBankPromptPay]:{0}')", err.Message.Replace("'", "")));

                LINENotify notify = new LINENotify();
                if (responseD == null)
                {
                    notify.LineSBErrorSend(new LINENotifyDATA
                    {
                        Parameter = new { genPara.schoolID, genPara.shopID, genPara.amount, responseOAuth20, responseGenerateThaiQRCode },
                        Date_Time = DateTime.Now,
                        URL = "https://system.schoolbright.co/PaymentGateway/KBank/KBankPromptPay/GenerateQRCode.ashx",
                        Error_Method = "KBankPromptPay - Generate QRCode"
                    }, err);
                }
                else
                {
                    notify.LineSBErrorSend(new LINENotifyDATA
                    {
                        //Parameter = new { genPara.schoolID, genPara.shopID, genPara.amount, responseOAuth20, responseD },
                        Parameter = new { genPara.schoolID, genPara.shopID, genPara.amount, responseOAuth20, responseGenerateThaiQRCode, responseD.ResponseStatus, responseD.StatusCode, responseD.StatusDescription, responseD.ErrorMessage },
                        Date_Time = DateTime.Now,
                        URL = "https://system.schoolbright.co/PaymentGateway/KBank/KBankPromptPay/GenerateQRCode.ashx",
                        Error_Method = "KBankPromptPay - Generate QRCode"
                    }, err);
                }

                isSuccess = false;
                message = err.Message;

                resDescTH = "ระบบพร้อมเพย์ขัดข้อง กรุณาลองใหม่ภายหลัง";
                resDescEN = "PromptPay system malfunctions Please try again later.";

            }

            var result = new { success = isSuccess, message, data, resDescTH, resDescEN };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }

    public class GenPara
    {
        public int schoolID { get; set; }
        public int shopID { get; set; }
        public decimal amount { get; set; }
        public string imei { get; set; }
        public string type { get; set; }
        public int userID { get; set; }
        public string appName { get; set; }
        public string sellDetail { get; set; }
    }

    public class QRCodeModel
    {
        public string qrCode { get; set; }
        public string qrCodeBase64 { get; set; }
    }

    public class ResponseOAuth20
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "client_id")]
        public string ClientID { get; set; }

        [JsonProperty("developer.email")]
        public string DeveloperEmail { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public string ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "scope")]
        public string Scope { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
    }

    public class RequestBodyGenerateThaiQRCode
    {
        [JsonProperty(PropertyName = "merchantId")]
        public string MerchantID { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public string MetaData { get; set; }

        [JsonProperty(PropertyName = "partnerId")]
        public string PartnerID { get; set; }

        [JsonProperty(PropertyName = "partnerSecret")]
        public string PartnerSecret { get; set; }

        [JsonProperty(PropertyName = "partnerTxnUid")]
        public string PartnerTxnUID { get; set; }

        [JsonProperty(PropertyName = "qrType")]
        public string QRType { get; set; }

        [JsonProperty(PropertyName = "reference1")]
        public string Reference1 { get; set; }

        [JsonProperty(PropertyName = "reference2")]
        public string Reference2 { get; set; }

        [JsonProperty(PropertyName = "reference3")]
        public string Reference3 { get; set; }

        [JsonProperty(PropertyName = "reference4")]
        public string Reference4 { get; set; }

        [JsonProperty(PropertyName = "requestDt")]
        public string RequestDt { get; set; }

        [JsonProperty(PropertyName = "txnAmount")]
        public string TxnAmount { get; set; }

        [JsonProperty(PropertyName = "txnCurrencyCode")]
        public string TxnCurrencyCode { get; set; }
    }

    public class ResponseGenerateThaiQRCode
    {
        [JsonProperty(PropertyName = "accountName")]
        public string AccountName { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "errorDesc")]
        public string ErrorDesc { get; set; }

        [JsonProperty(PropertyName = "partnerId")]
        public string PartnerID { get; set; }

        [JsonProperty(PropertyName = "partnerTxnUid")]
        public string PartnerTxnUID { get; set; }

        [JsonProperty(PropertyName = "qrCode")]
        public string QRCode { get; set; }

        [JsonProperty(PropertyName = "sof")]
        public List<string> SOF { get; set; }

        [JsonProperty(PropertyName = "statusCode")]
        public string StatusCode { get; set; }
    }

}