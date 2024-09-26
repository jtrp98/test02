using JabjaiMainClass;
using Ninject.Activation;
using RestSharp;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

namespace FingerprintPayment.Class
{
    public class LINENotify
    {
        /// <summary>
        /// Line SB Error Send message to group
        /// </summary>
        /// <param name="message">1000 characters max</param>
        public static void LineSBErrorSend(string message)
        {
            try
            {
                var client = new RestClient("https://notify-api.line.me/api/notify");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Bearer tGtEXKdmfOe6pip8zt2dUYxwl2kcpr9Ses2pKaWWC1e");
                request.AddParameter("message", message);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch { }
        }

        public void LineSBErrorSend(LINENotifyDATA data, Exception ex)
        {
            try
            {

#if DEBUG
                data.Error_Method = "[DEBUG] " + data.Error_Method;
#endif

                //data.Error_Message = fcommon.ExceptionMessage(ex);
                string Message = string.Format(@"Method : {0}{4}
Parameter : {1} {4}
Date & Time  : {2:dd/MM/yyyy HH:mm:ss}{4}
Error StackTrace : {3}
Error Message : {5}
", data.Error_Method,
fcommon.EntityToJson(data.Parameter), data.Date_Time, ex.StackTrace.ToString(), Environment.NewLine, ex.Message);

                var client = new RestClient("https://notify-api.line.me/api/notify");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Bearer nXb9LJaGxUTErhzvjMbAs2sddb2cckjWW5gOCl72RMZ");
                request.AddParameter("message", Message);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch { }
        }

        public void LineSBErrorSend(LINENotifyDATA data)
        {
            try
            {

#if DEBUG
                data.Error_Method = "[DEBUG] " + data.Error_Method;
#endif

                //data.Error_Message = fcommon.ExceptionMessage(ex);
                string Message = string.Format(@"Method : {0}{4}
Parameter : {1} {4}
Date & Time  : {2:dd/MM/yyyy HH:mm:ss}{4}
Error Message : {3}
", data.Error_Method,
fcommon.EntityToJson(data.Parameter), data.Date_Time,data.Error_Message, Environment.NewLine);

                var client = new RestClient("https://notify-api.line.me/api/notify");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", "Bearer nXb9LJaGxUTErhzvjMbAs2sddb2cckjWW5gOCl72RMZ");
                request.AddParameter("message", Message);
                IRestResponse response = client.Execute(request);
                //Console.WriteLine(response.Content);
            }
            catch { }
        }
    }

    public class LINENotifyDATA
    {
        public object Parameter { get; set; }
        public DateTime Date_Time { get; set; }
        public string Error_Message { get; set; }
        public string Error_Method { get; set; }
        public string URL { get; set; }
    }
}