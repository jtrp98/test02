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
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FingerprintPayment.PaymentGateway.KBank.KBankPromptPay
{
    /// <summary>
    /// Summary description for ManualGenerateQRCode
    /// </summary>
    public class ManualGenerateQRCode : IHttpHandler
    {
        string kbankUrl = ConfigurationManager.AppSettings["KBankUrl"].ToString();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            bool isSuccess = true;
            string message = "Generate QRCode Successfully!";

            RequestBodyGenerateThaiQRCode requestBodyGenerateThaiQRCode = new RequestBodyGenerateThaiQRCode();

            ResponseGenerateThaiQRCode responseData = new ResponseGenerateThaiQRCode();

            int schoolID = 0;

            try
            {
                schoolID = int.Parse(context.Request.QueryString["SchoolID"]);

                string jsonString = string.Empty;
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    jsonString = reader.ReadToEnd();
                }

                requestBodyGenerateThaiQRCode = JsonConvert.DeserializeObject<RequestBodyGenerateThaiQRCode>(jsonString);


                //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                //string strconn = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);
                //SqlConnection _conn = new SqlConnection(strconn);
                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Data[PaymentGateway/KBank/KBankPromptPay/ManualGenerateQRCode.ashx]{0}')", JsonConvert.SerializeObject(requestBodyGenerateThaiQRCode)));


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


                    // Generate PartnerTxnUID

                    var jsonRequestBody = JsonConvert.SerializeObject(requestBodyGenerateThaiQRCode);


                    // Generate QRCode
                    client.BaseUrl = new Uri(kbankUrl + "/v1/qrpayment/request");

                    request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddHeader("Authorization", "Bearer " + responseOAuth20.AccessToken);
                    request.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                    response = client.Execute(request);
                    //Console.WriteLine(response.Content);

                    responseData = JsonConvert.DeserializeObject<ResponseGenerateThaiQRCode>(response.Content);
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

                fcommon.InsertLog(string.Format("insert into [dbo].[tb_apilog] ([info]) values ('Error Message:[PaymentGateway/KBank/KBankPromptPay/ManualGenerateQRCode.ashx]:{0}')", err.Message.Replace("'", "")));

                LINENotify notify = new LINENotify();
                notify.LineSBErrorSend(new LINENotifyDATA
                {
                    Parameter = new { schoolID },
                    Date_Time = DateTime.Now,
                    URL = "https://dev.schoolbright.co/PaymentGateway/KBank/KBankPromptPay/ManualGenerateQRCode.ashx",
                    Error_Method = "KBankPromptPay - Manual Generate QRCode"
                }, err);

                isSuccess = false;
                message = err.Message;
            }

            var result = new { success = isSuccess, message, responseData };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }

}