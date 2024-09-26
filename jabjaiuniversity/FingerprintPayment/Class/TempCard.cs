using JabjaiEntity.DB;
using JabjaiMainClass;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Memory
{
    public class TempCard
    {
        public int SchoolID { get; set; }
        public System.Guid CardID { get; set; }
        public int UserType { get; set; }
        public int UserID { get; set; }
        public string CardName { get; set; }
        public string BarCode { get; set; }
        public string NFC { get; set; }
        public string NFCEncrypt { get; set; }
        public decimal Money { get; set; }
        public Nullable<bool> cDel { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        string PaymentApi = ConfigurationManager.AppSettings["PaymentApi"].ToString();

        public void AddOrModifyTempCard(TempCard tempCard, string AuthKey, string AuthValue)
        {
            string json = fcommon.EntityToJson(tempCard);
            var client = new RestClient(PaymentApi + "/api/shop/payment/addmodifytempcard");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader(AuthKey, AuthValue);

            IRestResponse response = client.Execute(request);

            string result = fcommon.EntityToJson(response.Content);

            InsertLogAPI.PaymentLog("AddOrModifyTempCard", json, result, tempCard.SchoolID);
        }

        public void AddOrModifyShop(TShop shop, string AuthKey, string AuthValue)
        {
            string json = fcommon.EntityToJson(shop);
            var client = new RestClient(PaymentApi + "/api/shop/payment/addormodifyshop");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader(AuthKey, AuthValue);

            IRestResponse response = client.Execute(request);

            string result = fcommon.EntityToJson(response.Content);


            InsertLogAPI.PaymentLog("AddOrModifyShop", json, result, shop.SchoolID);
        }
    }
}