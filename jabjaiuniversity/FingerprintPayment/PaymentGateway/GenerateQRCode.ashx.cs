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
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FingerprintPayment.PaymentGateway
{
    /// <summary>
    /// Summary description for GenerateQRCode
    /// </summary>
    public class GenerateQRCode : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            Result result = new Result();

            try
            {
                string jsonRequestBody = string.Empty;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonRequestBody = reader.ReadToEnd();
                }

                RequestPara requestPara = JsonConvert.DeserializeObject<RequestPara>(jsonRequestBody);

                string urlGenerateQRCode = string.Empty;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var paymentGatewayObj = mctx.TB_PaymentGateway.Where(w => w.Fd_SchoolID == requestPara.schoolID).FirstOrDefault();

                    string ServerIP = ConfigurationManager.AppSettings["PaymentGatewayIP"].ToString();

                    if (paymentGatewayObj != null)
                    {
                        result = GenerateQRCodePayment(paymentGatewayObj, requestPara);
                    }
                    else
                    {
                        throw new Exception("The system has not been activated.");
                    }
                }
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                result.success = false;
                result.message = ex.Message;
            }

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public Result GenerateQRCodePayment(TB_PaymentGateway paymentGateway, RequestPara requestPara)
        {
            Result result = new Result
            {
                success = true,
                message = "Generate QRCode/Barcode Successfully!",
                data = new QRCodeModel(),
                resDescTH = "",
                resDescEN = ""
            };

            // Log request generate qr code / barcode
            //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
            //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
            //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

            //string connectionString = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);
            //SqlConnection sqlConnection = new SqlConnection(connectionString);

            string method = "Init - Generate QRCode/Barcode";

            // Only KBank
            ResponseOAuth20 responseOAuth20 = new ResponseOAuth20();
            IRestResponse responseD = null;
            ResponseGenerateThaiQRCode responseGenerateThaiQRCode = null;

            try
            {
                // Log request data
                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Data[PaymentGateway/GenerateQRCode.ashx]{0}')", JsonConvert.SerializeObject(requestPara)));

                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(requestPara.schoolID, ConnectionDB.Read)))
                {
                    if (paymentGateway.Fd_KTBPayment ?? false)
                    {
                        #region KTB

                        method = "KTB - Generate QRCode/Barcode";

                        if (!fcommon.PaymentSetting(mctx, "Canteen QRCode", "KTB"))
                        {
                            result.success = false;
                            result.message = "Server Maintenance";
                            result.data = null;
                            result.resDescTH = "ปิดระบบพร้อมเพย์ชั่วคราว เนื่องจากระบบขัดข้อง\r\nเจ้าหน้าที่กำลังดำเนินการแก้ไขอย่างเร่งด่วน";
                            result.resDescEN = "PromptPay system is temporarily closed due to a system failure.\r\nThe officials is working to fix it urgently.";
                        }
                        else
                        {
                            string ref1Type = "01";
                            string suffix = "31"; // 30: ค่าเทอม, 31: เติมเงิน, 32: ร้านค้า
                            if (requestPara.shopID != null)
                            {
                                ref1Type = "02";
                                suffix = "32";
                            }
                            else if (requestPara.invoiceID != null)
                            {
                                ref1Type = "03";
                                suffix = "30";
                            }

                            // Ref1: 011093670214032330 - [01][1093][670214][032330] - [trans type][school id][yymmdd][run no of day] - 01: topup, 02: shop, 03: invoice
                            DateTime currentDate = DateTime.Now.Date;
                            int runNumber = ctx.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(*)+1 FROM KTBTransaction WHERE SchoolID = {0} AND CreateDate BETWEEN '{1:yyyy-MM-dd} 00:00:00' AND '{1:yyyy-MM-dd} 23:59:59'", requestPara.schoolID, currentDate)).FirstOrDefault();
                            string ref1 = string.Format(@"{0}{1:D4}{2}{3:D6}", ref1Type
                                , requestPara.schoolID
                                , DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH"))
                                , runNumber);

                            int yearID = int.Parse(DateTime.Now.ToString("yyyy", new CultureInfo("th-TH")));
                            string newGuid = Guid.NewGuid().ToString();

                            // Save & Get Transaction
                            KTBTransaction ktbTransaction = new KTBTransaction
                            {
                                SchoolID = requestPara.schoolID,
                                Year = yearID,
                                GUID = newGuid,
                                Ref1 = ref1,
                                IMEI = requestPara.imei,
                                UserID = requestPara.userID,
                                Amount = requestPara.amount,
                                InvoiceID = requestPara.invoiceID,
                                ShopID = requestPara.shopID,
                                AppName = requestPara.appName,
                                SellDetail = requestPara.sellDetail,
                                CreateDate = DateTime.Now,
                                cDel = false
                            };
                            ctx.KTBTransaction.Add(ktbTransaction);
                            ctx.SaveChanges();

                            int transID = ktbTransaction.TransactionID;

                            string ref2Type = "00"; // 01: student, 02: teacher, 03: shop, 04: invoice
                            if (requestPara.shopID != null)
                            {
                                ref2Type = "03";
                            }
                            else if (requestPara.invoiceID != null)
                            {
                                ref2Type = "04";
                            }
                            else
                            {
                                MasterEntity.TUser userObj = mctx.TUsers.Where(w => w.sID == requestPara.userID).FirstOrDefault();
                                if (userObj != null)
                                {
                                    switch (userObj.cType)
                                    {
                                        case "0": ref2Type = "01"; break;
                                        case "1": ref2Type = "02"; break;
                                    }
                                }
                            }

                            // Ref2: 010070012400000001 - [01][00700124][00000001] - [user type][user id or shop id or invoice id][trans id]
                            string ref2 = string.Format(@"{0}{1:D8}{2:D8}", ref2Type
                                , (ref2Type == "01" || ref2Type == "02" ? requestPara.userID : (ref2Type == "03" ? requestPara.shopID : (ref2Type == "04" ? requestPara.invoiceID : 0)))
                                , transID);

                            ktbTransaction.Ref2 = ref2;
                            ctx.SaveChanges();

                            string amount = requestPara.amount.ToString("#0.00").Replace(".", ""); ;

                            string taxID = "0105559067813" + suffix;
                            TCompany companyObj = mctx.TCompanies.Where(w => w.nCompany == requestPara.schoolID).FirstOrDefault();
                            if (companyObj != null && !string.IsNullOrEmpty(companyObj.TaxId))
                            {
                                if (companyObj.TaxId == "0105559067813")
                                {
                                    taxID = companyObj.TaxId + "00"; // Tax of Jabjai Corporation Co., Ltd.
                                }
                                else
                                {
                                    taxID = companyObj.TaxId + suffix;
                                }
                            }
                            else
                            {
                                throw new Exception("Tax id not found.");
                            }

                            string code = "|" + taxID + "\r\n" + ref1 + "\r\n" + ref2 + "\r\n" + amount;

                            result.data.qrCode = code;
                            result.data.qrCodeBase64 = QRCodeFunction.Create(result.data.qrCode, QRCodeGenerator.ECCLevel.H, false, false).Replace("data:image/png;base64,", "");

                            result.data.barCode = code;
                            result.data.barCodeBase64 = BarCodeFunction.Create(result.data.barCode, BarCodeFunction.BarcodeTYPE.CODE128, false, BarCodeFunction.Alignment.CENTER, 550, 100);
                        }

                        #endregion
                    }
                    else
                    {
                        #region KBank

                        method = "KBankPromptPay - Generate QRCode/Barcode";

                        if (requestPara.appName == "win_desktop(1.0.6)")
                        {
                            throw new Exception("win_desktop(1.0.6) not available");
                        }

                        if (paymentGateway.Fd_PromptPayActive != 1)
                        {
                            throw new Exception("Please set up bank connection.");
                        }

                        string Fd_ConsumerID = paymentGateway.Fd_ConsumerID;
                        string Fd_ConsumerSecret = paymentGateway.Fd_ConsumerSecret;
                        string authBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(@"{0}:{1}", Fd_ConsumerID, Fd_ConsumerSecret)));

                        if (!fcommon.PaymentSetting(mctx, "Canteen QRCode", "KBank"))
                        {
                            result.success = false;
                            result.message = "Server Maintenance";
                            result.data = null;
                            result.resDescTH = "ปิดระบบพร้อมเพย์ชั่วคราว เนื่องจากระบบขัดข้อง\r\nเจ้าหน้าที่กำลังดำเนินการแก้ไขอย่างเร่งด่วน";
                            result.resDescEN = "PromptPay system is temporarily closed due to a system failure.\r\nThe officials is working to fix it urgently.";
                        }
                        else
                        {
                            string kbankUrl = ConfigurationManager.AppSettings["KBankUrl"].ToString();

                            KPromptPayAccessToken kPromptPayAccessToken = null;
                            using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(requestPara.schoolID, ConnectionDB.Read)))
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
                            using (JabJaiEntities schoolEntities = new JabJaiEntities(Connection.StringConnectionSchool(requestPara.schoolID, ConnectionDB.Read)))
                            {
                                // 03070013300123464052500001Q // [schoolID:4][shopID:5][imei:6][date:6][runNo:5][Q|C]
                                DateTime currentDate = DateTime.Now.Date;
                                int runNumber = schoolEntities.Database.SqlQuery<int>(string.Format(@"SELECT COUNT(*)+1 FROM KPromptPayTransaction WHERE ShopID = {0} AND CreateDate BETWEEN '{1:yyyy-MM-dd} 00:00:00' AND '{1:yyyy-MM-dd} 23:59:59'", requestPara.shopID, currentDate)).FirstOrDefault();
                                string refNo = string.Format(@"{0:D4}{1:D5}{2}{3}{4:D5}{5}", requestPara.schoolID
                                    , requestPara.shopID
                                    , (requestPara.imei.Length >= 6 ? requestPara.imei.Substring(requestPara.imei.Length - 6) : requestPara.imei.PadLeft(6, '0'))
                                    , DateTime.Now.ToString("yyMMdd", new CultureInfo("th-TH"))
                                    , runNumber
                                    , (requestPara.type == "3" ? "Q" : (requestPara.type == "4" ? "C" : "N")));

                                requestBodyGenerateThaiQRCode = new RequestBodyGenerateThaiQRCode
                                {
                                    MerchantID = paymentGateway.Fd_MerchantMID,
                                    MetaData = "",
                                    PartnerID = paymentGateway.Fd_PartnerID,
                                    PartnerSecret = paymentGateway.Fd_PartnerSecret,
                                    PartnerTxnUID = refNo,
                                    QRType = requestPara.type,
                                    Reference1 = "",
                                    Reference2 = "",
                                    Reference3 = "",
                                    Reference4 = "",
                                    RequestDt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"),
                                    TxnAmount = requestPara.amount.ToString("0.00"),
                                    TxnCurrencyCode = "THB"
                                };

                                // Save to db
                                schoolEntities.KPromptPayTransactions.Add(new KPromptPayTransaction
                                {
                                    SchoolID = requestPara.schoolID,
                                    ShopID = requestPara.shopID,
                                    IMEI = requestPara.imei,
                                    PartnerTxnUID = refNo,
                                    QRType = requestPara.type,
                                    TxnAmount = requestPara.amount,
                                    TxnCurrencyCode = "THB",
                                    CreateDate = DateTime.Now,
                                    UserID = requestPara.userID,
                                    Method = "Create",
                                    AppName = requestPara.appName,
                                    SellDetail = requestPara.sellDetail
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

                            result.data.qrCode = responseGenerateThaiQRCode.QRCode;
                            result.data.qrCodeBase64 = QRCodeFunction.Create(responseGenerateThaiQRCode.QRCode).Replace("data:image/png;base64,", "");
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                SentrySdk.CaptureException(ex);

                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Error Message:[PaymentGateway/GenerateQRCode]:{0}')", ex.Message.Replace("'", "")));

                string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

                if (paymentGateway.Fd_KTBPayment ?? false)
                {
                    // KTB
                    LINENotify notify = new LINENotify();
                    notify.LineSBErrorSend(new LINENotifyDATA
                    {
                        Parameter = new { requestPara },
                        Date_Time = DateTime.Now,
                        URL = $@"{domainName}/PaymentGateway/GenerateQRCode.ashx",
                        Error_Method = method
                    }, ex);
                }
                else
                {
                    // KBank
                    LINENotify notify = new LINENotify();
                    if (responseD == null)
                    {
                        notify.LineSBErrorSend(new LINENotifyDATA
                        {
                            Parameter = new { requestPara, responseOAuth20, responseGenerateThaiQRCode },
                            Date_Time = DateTime.Now,
                            URL = $@"{domainName}/PaymentGateway/GenerateQRCode.ashx",
                            Error_Method = method + "(1)"
                        }, ex);
                    }
                    else
                    {
                        notify.LineSBErrorSend(new LINENotifyDATA
                        {
                            Parameter = new { requestPara, responseOAuth20, responseGenerateThaiQRCode, responseD.ResponseStatus, responseD.StatusCode, responseD.StatusDescription, responseD.ErrorMessage },
                            Date_Time = DateTime.Now,
                            URL = $@"{domainName}/PaymentGateway/GenerateQRCode.ashx",
                            Error_Method = method + "(2)"
                        }, ex);
                    }
                }

                result.success = false;
                result.message = ex.Message;

                result.resDescTH = "ระบบพร้อมเพย์ขัดข้อง กรุณาลองใหม่ภายหลัง";
                result.resDescEN = "PromptPay system malfunctions Please try again later.";
            }

            return result;
        }

        public bool IsReusable { get { return false; } }
    }

    public class RequestPara
    {
        public int schoolID { get; set; }
        public int userID { get; set; }
        public int? shopID { get; set; }
        public int? invoiceID { get; set; }
        public decimal amount { get; set; }
        public string imei { get; set; }
        public string type { get; set; }
        public string appName { get; set; }
        public string sellDetail { get; set; }
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

        public string resDescTH { get; set; }
        public string resDescEN { get; set; }
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