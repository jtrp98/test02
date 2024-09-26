using JabjaiMainClass;
using MasterEntity;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Memory
{
    public class TopupTempCard
    {
        public int SchoolID { get; set; }
        public System.Guid CardID { get; set; }
        public int UserType { get; set; }
        public int UserID { get; set; }
        public string CardName { get; set; }
        public string BarCode { get; set; }
        public string NFC { get; set; }
        public decimal Money { get; set; }
        public string NFCEncrypt { get; set; }
        public Nullable<bool> cDel { get; set; }
        string PaymentApi = ConfigurationManager.AppSettings["PaymentApi"].ToString();

        public void init(TopupTempCard topupTempCard, string AuthKey, string AuthValue)
        {
            string json = fcommon.EntityToJson(topupTempCard);
            var client = new RestClient(PaymentApi + "/api/shop/payment/TopupTempCard");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader(AuthKey, AuthValue);
            IRestResponse response = client.Execute(request);

            string result = fcommon.EntityToJson(response.Content);

            InsertLogAPI.PaymentLog("TopupTempCard", json, result, topupTempCard.SchoolID);
        }

        public void AddOrModifyUserCard(List<TUser_Card> user_Card, int SchoolId, string AuthKey, string AuthValue)
        {
            string json = fcommon.EntityToJson(user_Card);
            var client = new RestClient(PaymentApi + "/api/shop/payment/addmodifyusercard");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader(AuthKey, AuthValue);
            IRestResponse response = client.Execute(request);

            string result = fcommon.EntityToJson(response.Content);

            InsertLogAPI.PaymentLog("AddOrModifyUserCard", json, result, SchoolId);
        }
    }
}