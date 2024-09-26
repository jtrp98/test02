using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalR = Microsoft.AspNet.SignalR.Client;

namespace FingerprintPayment.PaymentGateway.KBank.KBankPromptPay
{
    public partial class CallbackResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CallbackData callbackData = new CallbackData();
            callbackData.sellId = 107166892;
            callbackData.statusCode = "00";
            callbackData.errorCode = "00";
            callbackData.errorDesc = "";
            callbackData.partnerTxnUid = "010849670426000016";
            callbackData.txnAmount = 50.00M;
            callbackData.txnNo = "020042338100001201";
            var s = JsonConvert.SerializeObject(callbackData);
        }

        [WebMethod(EnableSession = true)]
        public static object TestCallbackResponse(string imei, string callbackData)
        {
            bool success = true;
            string code = "200";
            string message = "Success.";

            try
            {
                CallbackData callbackDataObj = JsonConvert.DeserializeObject<CallbackData>(callbackData);

                // Send callback data to shop mobile
                SignalR.HubConnection hubConnection = new SignalR.HubConnection("https://signalrpos.schoolbright.co/signalr/hubs");
                SignalR.IHubProxy hubProxy = hubConnection.CreateHubProxy("KBankHub");
                hubConnection.Start().Wait();
                hubProxy.Invoke("CallbackToClient", imei, JsonConvert.SerializeObject(callbackDataObj)).Wait();
                hubConnection.Stop();
            }
            catch (Exception err)
            {
                success = false;
                code = "500";
                message = err.Message;
            }

            return JsonConvert.SerializeObject(new { success, code, message });
        }

        public class CallbackData
        {
            public string statusCode { get; set; }
            public string errorCode { get; set; }
            public string errorDesc { get; set; }
            public string partnerTxnUid { get; set; }
            public string partnerId { get; set; }
            public string merchantId { get; set; }
            public decimal txnAmount { get; set; }
            public string txnCurrencyCode { get; set; }
            public string txnNo { get; set; }
            public int sellId { get; set; }
        }
    }
}