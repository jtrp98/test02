using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;
using FingerprintPayment.StudentInfo.CsCode;
using System.Web.Script.Services;
using System.Web.Services;

namespace FingerprintPayment.PaymentGateway
{
    public partial class Setting : StudentGateway
    {
        public TB_PaymentGateway paymentGateway = new TB_PaymentGateway();
        public TCompany schoolInfo = new TCompany();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                paymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == userData.CompanyID);
                if (paymentGateway == null) paymentGateway = new TB_PaymentGateway();

                schoolInfo = masterEntities.TCompanies.Where(w => w.nCompany == userData.CompanyID).FirstOrDefault();
                if (schoolInfo == null) schoolInfo = new TCompany();
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod]
        public static object updatePaymentGetway(PaymentSetting paymentSetting)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            string Status = "success";

            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            {
                var _paymentGateway = masterEntities.TB_PaymentGateway.FirstOrDefault(f => f.Fd_SchoolID == userData.CompanyID);
                if (_paymentGateway == null) _paymentGateway = new TB_PaymentGateway();

                var schoolInfo = masterEntities.TCompanies.Where(w => w.nCompany == userData.CompanyID).FirstOrDefault();

                switch (paymentSetting.paymentType.ToLower())
                {
                    case "wallet":
                        _paymentGateway.Fd_SecretKey = paymentSetting.SecretKey?.Trim();
                        _paymentGateway.Fd_PublicKey = paymentSetting.PublicKey?.Trim();
                        _paymentGateway.Fd_Active = paymentSetting.Active ?? false;
                        _paymentGateway.Fd_FeePayment = paymentSetting.Fee;
                        break;
                    case "invoice":
                        _paymentGateway.Fd_SecretKeyInvoice = paymentSetting.SecretKey?.Trim();
                        _paymentGateway.Fd_PublicKeyInvoice = paymentSetting.PublicKey?.Trim();
                        _paymentGateway.Fd_ActiveInvoice = paymentSetting.Active ?? false;
                        _paymentGateway.Fd_FeeInvoice = paymentSetting.Fee;
                        break;
                    case "qr_code":
                        _paymentGateway.Fd_PromptPayActive = paymentSetting.Active ?? false ? 1 : 0;
                        _paymentGateway.Fd_MerchantMID = paymentSetting.MerchantMID?.Trim();
                        if (string.IsNullOrEmpty(_paymentGateway.Fd_PartnerID))
                        {
                            //_paymentGateway.Fd_PromptPayActive = 1;
                            _paymentGateway.Fd_PartnerID = "PTR0000028";
                            _paymentGateway.Fd_PartnerSecret = "v3RUUGnbVBmOmAoaV7vGO3S0rnDCkJlM4OsXiaWsVtroh884GkJjjlVraNZPWhmE";
                            _paymentGateway.Fd_ConsumerID = "nbJQTuOFdxcJTQR8nySSc40De4xlNd4n";
                            _paymentGateway.Fd_ConsumerSecret = "Y4PpYA0WVkSCQctY";
                        }
                        break;
                    case "ktb_qr_code":
                        _paymentGateway.Fd_KTBPayment = paymentSetting.Active ?? false;
                        schoolInfo.TaxId = paymentSetting.TaxID?.Trim();
                        break;
                }

                if (_paymentGateway.Fd_PaymentGatewayID == 0)
                {
                    _paymentGateway.Fd_PaymentGatewayID = masterEntities.TB_PaymentGateway.Max(max => max.Fd_PaymentGatewayID) + 1;
                    _paymentGateway.Fd_SchoolID = userData.CompanyID;
                    masterEntities.TB_PaymentGateway.Add(_paymentGateway);
                }

                masterEntities.SaveChanges();
            }

            return new { Status = Status, data = paymentSetting };
        }

        public class PaymentSetting
        {
            public string paymentType { get; set; }
            public string PublicKey { get; set; }
            public string SecretKey { get; set; }
            public string MerchantMID { get; set; }
            public bool? Active { get; set; }
            public decimal? Fee { get; set; }
            public string TaxID { get; set; }
        }
    }

}