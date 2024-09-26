using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PaymentGateway.KBank
{
    public partial class ManualCallbackQR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class ResponseQR
        {
            [JsonProperty(PropertyName = "id")]
            public string ChargeId { get; set; }

            [JsonProperty(PropertyName = "object")]
            public string Object { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }

            [JsonProperty(PropertyName = "currency")]
            public string Currency { get; set; }

            [JsonProperty(PropertyName = "description")]
            public string Description { get; set; }

            [JsonProperty(PropertyName = "transaction_state")]
            public string TransactionState { get; set; }

            [JsonProperty(PropertyName = "reference_order")]
            public string ReferenceOrder { get; set; }

            [JsonProperty(PropertyName = "created")]
            public string Created { get; set; }

            [JsonProperty(PropertyName = "order_id")]
            public string OrderId { get; set; }

            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }

            [JsonProperty(PropertyName = "livemode")]
            public bool LiveMode { get; set; }

            [JsonProperty(PropertyName = "metadata")]
            public string MetaData { get; set; }

            [JsonProperty(PropertyName = "failure_code")]
            public string FailureCode { get; set; }

            [JsonProperty(PropertyName = "failure_message")]
            public string FailureMessage { get; set; }

            [JsonProperty(PropertyName = "source")]
            public Source Source { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }

            [JsonProperty(PropertyName = "checksum")]
            public string Checksum { get; set; }
        }

        public class Source
        {
            [JsonProperty(PropertyName = "id")]
            public string CardObjectId { get; set; }

            [JsonProperty(PropertyName = "object")]
            public string Object { get; set; }

            [JsonProperty(PropertyName = "brand")]
            public string Brand { get; set; }

            [JsonProperty(PropertyName = "card_masking")]
            public string CardMasking { get; set; }

            [JsonProperty(PropertyName = "issuer_bank")]
            public string IssuerBank { get; set; }
        }

        protected void btnTopup_Click(object sender, EventArgs e)
        {
            string[] orders = this.txtOrderID.Text.Split(',');

            foreach (var ord in orders)
            {
                var client = new RestClient("https://kpaymentgateway-services.kasikornbank.com/qr/v2/qr/" + ord);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("x-api-key", txtsKey.Text);
                IRestResponse response = client.Execute(request);

                var responseQR = JsonConvert.DeserializeObject<ResponseQR>(response.Content);
                string joinInit = string.Join("", responseQR.ChargeId, Convert.ToDecimal(responseQR.Amount).ToString("0.0000"), responseQR.Currency, responseQR.Status, "Authorized", txtsKey.Text);
                responseQR.Checksum = ComputeSha256Hash(joinInit);
                responseQR.TransactionState = "Authorized";

                client = new RestClient("https://api.schoolbright.co/api/PaymentGateway/KBank");
                request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");

                var jsonRequestBody = JsonConvert.SerializeObject(responseQR);

                request.AddParameter("application/json", jsonRequestBody, ParameterType.RequestBody);
                response = client.Execute(request);
                //Console.WriteLine(response.Content);

                lblResult.Text += string.Format(@"{0} - {1}", ord, response.Content);
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}