using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PaymentGateway
{
    public partial class PaymentTransaction : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string GetPaymentGatewayTx(string search, string startDate, string endDate)
        {
            bool success = true;
            string message = "Success";

            List<PaymentGatewayModel> transactionModels = new List<PaymentGatewayModel>();

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var schoolObj = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolObj.nCompany, ConnectionDB.Read)))
                        {
                            DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime start);
                            DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime end);

                            string condition = "";
                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                condition += string.Format(@" AND k.CreateDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", start.ToString("yyyy-MM-dd", new CultureInfo("en-US")), end.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                            }
                            if (!string.IsNullOrEmpty(search))
                            {
                                condition += string.Format(@" AND (OrderID LIKE '%{0}%' OR ChargeID LIKE '%{0}%' OR ReferenceNo LIKE '%{0}%' OR u2.sStudentID LIKE '%{0}%' OR e.Code LIKE '%{0}%' OR u.sName LIKE '%{0}%' OR u.sLastname LIKE '%{0}%' OR u.sName+' '+u.sLastname = N'{0}')", search);
                            }

                            string query = string.Format(@"SELECT [Year], TransactionID, OrderID, QRID, ChargeID, ReferenceNo, Amount, CreateDate, k.UpdatedDate, UserID, (CASE WHEN u.cType = '0' THEN u2.sStudentID ELSE e.Code END) 'Code', u.sName 'Name', u.sLastname 'Lastname', u.cType 'UserType', InvoiceID, PaidPaymentID, ErrorCode, ErrorMessage
, (CASE WHEN InvoiceID IS NULL THEN 'topup' ELSE 'invoice' END) 'Payment'
, (CASE WHEN InvoiceID IS NULL OR ReferenceNo LIKE 'Q%' THEN 'qr' ELSE 'card' END) 'Method'
, (CASE WHEN InvoiceID IS NOT NULL THEN 1 ELSE 0 END) 'Invoice'
, (CASE WHEN (InvoiceID IS NOT NULL AND PaidPaymentID IS NOT NULL) OR (OrderID IS NOT NULL AND ChargeID IS NOT NULL) THEN 1 ELSE 0 END) 'Success'
, (CASE WHEN DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 10 AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) < 60 THEN 1 ELSE 0 END) 'BOT15MIN' 
, (CASE WHEN CAST(k.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 1 ELSE 0 END) 'BOT3AM' 
, (CASE WHEN CAST(k.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, CreateDate, k.UpdatedDate) > 60 THEN 1 ELSE 0 END) 'BOTMANUAL' 
FROM KTransaction k LEFT JOIN JabjaiMasterSingleDB.dbo.TUser u ON k.SchoolID = u.nCompany AND k.UserID = u.sID
LEFT JOIN TUser u2 ON k.SchoolID = u2.SchoolID AND k.UserID = u2.sID
LEFT JOIN TEmployeeInfo e ON k.SchoolID = e.SchoolID AND k.UserID = e.sEmp
WHERE k.SchoolID={0}{1}
ORDER BY k.CreateDate DESC", schoolObj.nCompany, condition);
                            transactionModels = ctx.Database.SqlQuery<PaymentGatewayModel>(query).ToList();
                        }
                    }
                    else
                    {
                        success = false;
                        message = string.Format(@"[NOTFOUND] - {0}", schoolID);
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = string.Format(@"[Exception] - {0}", err.Message);
            }

            object result = new { success, message, data = transactionModels };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string GetPromptpayQRTx(string search, string startDate, string endDate)
        {
            bool success = true;
            string message = "Success";

            List<PromptpayQRModel> transactionModels = new List<PromptpayQRModel>();

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var schoolObj = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolObj.nCompany, ConnectionDB.Read)))
                        {
                            DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime start);
                            DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime end);

                            string condition = "";
                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                condition += string.Format(@" AND tx.CreateDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", start.ToString("yyyy-MM-dd", new CultureInfo("en-US")), end.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                            }
                            if (!string.IsNullOrEmpty(search))
                            {
                                condition += string.Format(@" AND (s.shop_name LIKE '%{0}%' OR tx.TxnNo LIKE '%{0}%')", search);
                            }

                            string query = string.Format(@"SELECT tx.TransactionID, tx.SchoolID, tx.ShopID, tx.IMEI, tx.PartnerTxnUID, tx.TxnNo, tx.QRType, tx.TxnAmount, tx.TxnCurrencyCode, tx.CreateDate, tx.StatusCode, tx.ErrorCode, tx.SellGuID
, txi.OrigPartnerTxnUID, txi.REQUESTED, txi.PAID, txi.EXPIRED, txi.PAID8AM, txi.CreateDate 'InquiryCreateDate'
, (CASE WHEN DATEDIFF(MINUTE, tx.CreateDate, txi.CreateDate) > 60 THEN 1 ELSE 0 END) 'BOT8AM'
, s.shop_name 'Shop'
FROM KPromptPayTransaction tx 
LEFT JOIN 
(
	SELECT OrigPartnerTxnUID
	, SUM(CASE WHEN TxnStatus = 'REQUESTED' THEN 1 ELSE 0 END) 'REQUESTED'
	, SUM(CASE WHEN TxnStatus = 'PAID' THEN 1 ELSE 0 END) 'PAID'
	, SUM(CASE WHEN TxnStatus = 'EXPIRED' THEN 1 ELSE 0 END) 'EXPIRED'
	, SUM(CASE WHEN TxnStatus = 'PAID' AND CAST(CreateDate AS TIME) BETWEEN '08:00:00' AND '08:30:00' THEN 1 ELSE 0 END) 'PAID8AM'
	, MAX(CreateDate) 'CreateDate'
	FROM KPromptPayTransactionInquiry 
	WHERE SchoolID={0}
	GROUP BY OrigPartnerTxnUID
) txi ON tx.PartnerTxnUID = txi.OrigPartnerTxnUID
LEFT JOIN TShop s ON tx.SchoolID = s.SchoolID AND tx.ShopID = s.shop_id
WHERE tx.SchoolID={0}{1}
ORDER BY tx.CreateDate DESC", schoolObj.nCompany, condition);
                            transactionModels = ctx.Database.SqlQuery<PromptpayQRModel>(query).ToList();
                        }
                    }
                    else
                    {
                        success = false;
                        message = string.Format(@"[NOTFOUND] - {0}", schoolID);
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = string.Format(@"[Exception] - {0}", err.Message);
            }

            object result = new { success, message, data = transactionModels };

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static string GetPaymentGatewayKTBTx(string search, string startDate, string endDate)
        {
            bool success = true;
            string message = "Success";

            List<PaymentGatewayKTBModel> transactionModels = new List<PaymentGatewayKTBModel>();

            try
            {
                JWTToken.userData userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var schoolObj = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolObj != null)
                    {
                        using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolObj.nCompany, ConnectionDB.Read)))
                        {
                            DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime start);
                            DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime end);

                            string condition = "";
                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                condition += string.Format(@" AND tx.CreateDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", start.ToString("yyyy-MM-dd", new CultureInfo("en-US")), end.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                            }
                            if (!string.IsNullOrEmpty(search))
                            {
                                condition += string.Format(@" AND (tx.Ref1 LIKE '%{0}%' OR tx.Ref2 LIKE '%{0}%' OR u2.sStudentID LIKE '%{0}%' OR e.Code LIKE '%{0}%' OR u.sName LIKE '%{0}%' OR u.sLastname LIKE '%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR s.shop_name LIKE '%{0}%' OR i.InvoiceCode LIKE '%{0}%')", search);
                            }

                            string query = string.Format(@"SELECT tx.TransactionID, tx.CreateDate, tx.UpdatedDate, tx.SchoolID, tx.ShopID, tx.InvoiceID
, (CASE WHEN tx.Ref1 LIKE '01%' THEN 'TOPUP' WHEN tx.Ref1 LIKE '02%' THEN 'SHOP' WHEN tx.Ref1 LIKE '03%' THEN 'INVOICE' ELSE '?' END) 'TransType'
, tx.Ref1, tx.Ref2, tx.UserID, tx.Amount
, (CASE WHEN u.cType = '0' THEN u2.sStudentID ELSE e.Code END) 'Code', u.sName 'Name', u.sLastname 'Lastname', u.cType 'UserType'
, s.shop_name 'Shop', i.InvoiceCode, tx.PaymentRespCode 'Success', tx.PaymentRespMsg 'Note'
, (CASE WHEN CAST(tx.UpdatedDate AS TIME) BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 THEN 1 ELSE 0 END) 'BOT3AM' 
, (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) <= 2 AND DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) >= -2) THEN 1 ELSE 0 END) 'BANKDELAY' 
, (CASE WHEN CAST(tx.UpdatedDate AS TIME) NOT BETWEEN '03:00:00' AND '03:30:00' AND DATEDIFF(MINUTE, tx.CreateDate, tx.UpdatedDate) > 5 AND (DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) > 2 OR DATEDIFF(SECOND, ptx.CreateDate, tx.UpdatedDate) < -2) THEN 1 ELSE 0 END) 'BOTMANUAL' 
, tx.ApprovalRespCode, ptx.PaymentTransID
FROM KTBTransaction tx
LEFT JOIN [JabjaiMasterSingleDB].[dbo].[TUser] u ON tx.SchoolID = u.nCompany AND tx.UserID = u.sID
LEFT JOIN TUser u2 ON tx.SchoolID = u2.SchoolID AND tx.UserID = u2.sID
LEFT JOIN TEmployeeInfo e ON tx.SchoolID = e.SchoolID AND tx.UserID = e.sEmp
LEFT JOIN TShop s ON tx.SchoolID = s.SchoolID AND tx.ShopID = s.shop_id
LEFT JOIN [AccountingDB].[dbo].[AccountInvoiceStudent] i ON tx.InvoiceID = i.AccountInvoiceId
LEFT JOIN KTBPaymentTransaction ptx ON tx.SchoolID = ptx.SchoolID AND tx.Ref2 = ptx.Ref2 AND ptx.RespCode = 0
WHERE tx.SchoolID={0}{1}
ORDER BY tx.CreateDate DESC", schoolObj.nCompany, condition);
                            transactionModels = ctx.Database.SqlQuery<PaymentGatewayKTBModel>(query).ToList();
                        }
                    }
                    else
                    {
                        success = false;
                        message = string.Format(@"[NOTFOUND] - {0}", schoolID);
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = string.Format(@"[Exception] - {0}", err.Message);
            }

            object result = new { success, message, data = transactionModels };

            return JsonConvert.SerializeObject(result);
        }

        public class PaymentGatewayModel
        {
            [JsonProperty(PropertyName = "year")]
            public int Year { get; set; }

            [JsonProperty(PropertyName = "transactionId")]
            public int TransactionID { get; set; }

            [JsonProperty(PropertyName = "orderId")]
            public string OrderID { get; set; }

            [JsonProperty(PropertyName = "qrId")]
            public string QRID { get; set; }

            [JsonProperty(PropertyName = "chargeId")]
            public string ChargeID { get; set; }

            [JsonProperty(PropertyName = "referenceNo")]
            public string ReferenceNo { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }

            [JsonProperty(PropertyName = "createDate")]
            public DateTime CreateDate { get; set; }

            [JsonProperty(PropertyName = "updatedDate")]
            public DateTime? UpdatedDate { get; set; }

            [JsonProperty(PropertyName = "userId")]
            public int UserID { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "userType")]
            public string UserType { get; set; }

            [JsonProperty(PropertyName = "invoiceId")]
            public int? InvoiceID { get; set; }

            [JsonProperty(PropertyName = "paidPaymentId")]
            public int? PaidPaymentID { get; set; }

            [JsonProperty(PropertyName = "errorCode")]
            public string ErrorCode { get; set; }

            [JsonProperty(PropertyName = "errorMessage")]
            public string ErrorMessage { get; set; }

            [JsonProperty(PropertyName = "payment")]
            public string Payment { get; set; }

            [JsonProperty(PropertyName = "method")]
            public string Method { get; set; }

            [JsonProperty(PropertyName = "invoice")]
            public int Invoice { get; set; }

            [JsonProperty(PropertyName = "success")]
            public int Success { get; set; }

            [JsonProperty(PropertyName = "bot15MIN")]
            public int BOT15MIN { get; set; }

            [JsonProperty(PropertyName = "bot3AM")]
            public int BOT3AM { get; set; }

            [JsonProperty(PropertyName = "botMANUAL")]
            public int BOTMANUAL { get; set; }

        }

        public class PromptpayQRModel
        {
            [JsonProperty(PropertyName = "transactionId")]
            public int TransactionID { get; set; }

            [JsonProperty(PropertyName = "schoolId")]
            public int SchoolID { get; set; }

            [JsonProperty(PropertyName = "shopId")]
            public int ShopID { get; set; }

            [JsonProperty(PropertyName = "imei")]
            public string IMEI { get; set; }

            [JsonProperty(PropertyName = "partnerTxnUID")]
            public string PartnerTxnUID { get; set; }

            [JsonProperty(PropertyName = "txnNo")]
            public string TxnNo { get; set; }

            [JsonProperty(PropertyName = "qrType")]
            public string QRType { get; set; }

            [JsonProperty(PropertyName = "txnAmount")]
            public decimal TxnAmount { get; set; }

            [JsonProperty(PropertyName = "txnCurrencyCode")]
            public string TxnCurrencyCode { get; set; }

            [JsonProperty(PropertyName = "createDate")]
            public DateTime CreateDate { get; set; }

            [JsonProperty(PropertyName = "statusCode")]
            public string StatusCode { get; set; }

            [JsonProperty(PropertyName = "errorCode")]
            public string ErrorCode { get; set; }

            [JsonProperty(PropertyName = "sellGuID")]
            public string SellGuID { get; set; }

            [JsonProperty(PropertyName = "origPartnerTxnUID")]
            public string OrigPartnerTxnUID { get; set; }

            [JsonProperty(PropertyName = "requested")]
            public int? REQUESTED { get; set; }

            [JsonProperty(PropertyName = "paid")]
            public int? PAID { get; set; }

            [JsonProperty(PropertyName = "expired")]
            public int? EXPIRED { get; set; }

            [JsonProperty(PropertyName = "paid8AM")]
            public int? PAID8AM { get; set; }

            [JsonProperty(PropertyName = "inquiryCreateDate")]
            public DateTime? InquiryCreateDate { get; set; }

            [JsonProperty(PropertyName = "bot8AM")]
            public int BOT8AM { get; set; }

            [JsonProperty(PropertyName = "shop")]
            public string Shop { get; set; }

        }

        public class PaymentGatewayKTBModel
        {
            [JsonProperty(PropertyName = "transactionId")]
            public int TransactionID { get; set; }

            [JsonProperty(PropertyName = "createDate")]
            public DateTime CreateDate { get; set; }

            [JsonProperty(PropertyName = "updatedDate")]
            public DateTime? UpdatedDate { get; set; }

            [JsonProperty(PropertyName = "schoolId")]
            public int SchoolID { get; set; }

            [JsonProperty(PropertyName = "shopId")]
            public int? ShopID { get; set; }

            [JsonProperty(PropertyName = "invoiceId")]
            public int? InvoiceID { get; set; }

            [JsonProperty(PropertyName = "transType")]
            public string TransType { get; set; }

            [JsonProperty(PropertyName = "ref1")]
            public string Ref1 { get; set; }

            [JsonProperty(PropertyName = "ref2")]
            public string Ref2 { get; set; }

            [JsonProperty(PropertyName = "userId")]
            public int UserID { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "userType")]
            public string UserType { get; set; }

            [JsonProperty(PropertyName = "shop")]
            public string Shop { get; set; }

            [JsonProperty(PropertyName = "invoiceCode")]
            public string InvoiceCode { get; set; }

            [JsonProperty(PropertyName = "success")]
            public int? Success { get; set; }

            [JsonProperty(PropertyName = "note")]
            public string Note { get; set; }

            [JsonProperty(PropertyName = "bot3AM")]
            public int BOT3AM { get; set; }

            [JsonProperty(PropertyName = "bankDELAY")]
            public int BANKDELAY { get; set; }

            [JsonProperty(PropertyName = "botMANUAL")]
            public int BOTMANUAL { get; set; }

            [JsonProperty(PropertyName = "approvalRespCode")]
            public int? ApprovalRespCode { get; set; }

            [JsonProperty(PropertyName = "paymentTransID")]
            public int? PaymentTransID { get; set; }

        }
    }
}