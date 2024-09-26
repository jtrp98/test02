using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Peak;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using JabjaiEntity.DB;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoicePaymentHandler
    /// </summary>
    public class InvoicePaymentHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var jsonString = context.Request.Form["invoice"];
            string userName = context.Session["Emp_Name"].ToString();  //#HardcodeuserName
            int sEmpID = int.Parse(context.Session["sEmpID"].ToString());  //#HardcodeuserName
            BaseResult<PaidPayment> result = new BaseResult<PaidPayment>();
            BaseResult<int?> PaidPaymentId = new BaseResult<int?>();

            try
            {
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString());
                InvoicePayment payment = JsonConvert.DeserializeObject<InvoicePayment>(jsonString, new JsonSerializerSettings
                {
                    DateFormatString = "dd/MM/yyyy"
                });

                PaymentLogic paymentLogic = new PaymentLogic();
                InvoiceLogic invoiceLogic = new InvoiceLogic();
                BaseResult<InvoiceDTO> resultInvoice = invoiceLogic.GetById(Convert.ToInt32(payment.InvoiceId));

                if (!resultInvoice.Result.OutstandingAmount.HasValue || ((payment.PaidAmount > resultInvoice.Result.OutstandingAmount.Value && resultInvoice.Result.OutstandingAmount > 0)))
                {
                    result.Result = null;
                    result.Message = "ไม่สามารถชำระเงินเกินยอดค้างชำระได้";
                }
                else
                {

                    InvoiceDTO invoice = resultInvoice.Result;

                    PaidPaymentDTO paidPaymentDTO = new PaidPaymentDTO();
                    PaymentLogic logic = new PaymentLogic();
                    Dictionary<string, string> filters = new Dictionary<string, string>();
                    filters.Add("InvoiceId", payment.InvoiceId.ToString());
                    var res = logic.GetHistory(filters);
                    DateTime currentDateTime = DateTime.Now;
                    paidPaymentDTO.Payee = userName;
                    paidPaymentDTO.PaymentDate = payment.PaymentDate;
                    paidPaymentDTO.Amount = payment.PaidAmount;
                    paidPaymentDTO.InvoicesId = payment.InvoiceCode;
                    paidPaymentDTO.InvoiceId = invoice.InvoiceId;
                    paidPaymentDTO.PaymentGroupId = res.Result.Count() + 1;
                    paidPaymentDTO.SchoolID = userData.CompanyID;
                    paidPaymentDTO.CreatedBy = sEmpID;
                    decimal totalPaidAmount = payment.PaidAmount;
                    paidPaymentDTO.Payments = new List<PaymentDTO>();
                    DeductItemOutstading(ref payment, ref paidPaymentDTO, ref invoice, ref totalPaidAmount, ref userName, ref currentDateTime, true);

                    if (totalPaidAmount > 0)
                        DeductItemOutstading(ref payment, ref paidPaymentDTO, ref invoice, ref totalPaidAmount, ref userName, ref currentDateTime, false);

                    if (paidPaymentDTO.Payments.Count() > 0)
                    {
                        var paidPayment = paymentLogic.CreatePaidPayment(paidPaymentDTO);
                        result.Result = paidPayment.Result;
                        PaidPaymentId.Result = paidPayment.Result.PaidPaymentId;
                    }

                    if (invoice.preRegisterId.HasValue)
                    {
                        using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                        {
                            var f_preRegisters = entities.TPreRegisters.FirstOrDefault(f => f.preRegisterId == invoice.preRegisterId);
                            if (f_preRegisters != null)
                            {
                                if (result.Result.Invoice.InvoiceStatus == "Paid")
                                {
                                    f_preRegisters.paymentStatus = 2;
                                }
                                else
                                {
                                    f_preRegisters.paymentStatus = 1;
                                }

                                entities.SaveChanges();
                            }
                        }
                    }

                    //    using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
                    //    {
                    //        var invoice1 = entities.TInvoices.FirstOrDefault(f => f.invoices_Id == invoice.InvoiceId);
                    //        var f1 = paidPayment.Result;
                    //        if ((invoice1.OutstandingAmount ?? 0) == 0 && (invoice1.ManualDiscount ?? 0) > 0)
                    //        {
                    //            string SQL = @"DECLARE @InvoiceId INT =" + invoice.InvoiceId + @"

                    //    SELECT * FROM TInvoices_Detail WHERE invoicesDetail_Id NOT IN 
                    //    (SELECT B.invoicesDetail_Id 
                    //    FROM Paid_Payment AS A
                    //    INNER JOIN Payment AS B
                    //    ON A.paidpayment_id = B.paidpayment_id
                    //    WHERE A.InvoiceId = @InvoiceId AND A.status IS NULL) AND invoices_Id = @InvoiceId";

                    //            var q1 = entities.Database.SqlQuery<TInvoices_Detail>(SQL).ToList();

                    //            foreach (var item in q1)
                    //            {

                    //                entities.Payments.Add(new MasterEntity.Payment
                    //                {
                    //                    paymentMethodId = payment.PaymentMethodId,
                    //                    paidpayment_id = f1.PaidPaymentId,
                    //                    amount = 0,
                    //                    invoicesDetail_Id = item.invoicesDetail_Id,
                    //                });
                    //            }

                    //            entities.SaveChanges();
                    //        }

                    //        if (entities.TInvoices_Detail.Count(c => c.OutstandingAmount < 0 && c.invoices_Id == invoice1.invoices_Id && c.IsDelete != true) > 0 && invoice1.invoices_status == "Paid")
                    //        {
                    //            foreach (var data in entities.TInvoices_Detail.Where(w => w.OutstandingAmount != 0 && w.invoices_Id == invoice1.invoices_Id && w.IsDelete != true))
                    //            {
                    //                if (data.OutstandingAmount == data.GrandTotal)
                    //                {
                    //                    entities.Payments.Add(new MasterEntity.Payment
                    //                    {
                    //                        paymentMethodId = payment.PaymentMethodId,
                    //                        paidpayment_id = f1.PaidPaymentId,
                    //                        amount = data.OutstandingAmount,
                    //                        invoicesDetail_Id = data.invoicesDetail_Id,
                    //                    });
                    //                }
                    //                else if (data.OutstandingAmount > 0)
                    //                {
                    //                    var p1 = entities.Payments.FirstOrDefault(f => f.invoicesDetail_Id == data.invoicesDetail_Id && f.paidpayment_id == f1.PaidPaymentId);
                    //                    p1.amount = data.OutstandingAmount;
                    //                }

                    //                data.OutstandingAmount = 0;
                    //            }

                    //            entities.SaveChanges();
                    //        }
                    //    }
                    //    result.StatusCode = "200";
                }
            }
            catch (Exception ex)
            {
                result.Result = null;
                result.SystemErrorMessage = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(PaidPaymentId));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void DeductItemOutstading(
            ref InvoicePayment payment,
            ref PaidPaymentDTO paidPaymentDTO,
            ref InvoiceDTO invoice,
            ref decimal totalPaidAmount,
            ref string updateBy,
            ref DateTime updateDate,
            bool isChecked)
        {

            decimal? totalDiscount = (invoice.InvoiceItems.Where(w => w.OutstandingAmount < 0).Sum(s => s.OutstandingAmount) ?? 0) * -1;
            totalDiscount += invoice.ManualDiscount ?? 0;
            decimal? OutstandingAmount = 0;
            foreach (var data in invoice.InvoiceItems.Where(w => w.IsDelete == false))
            {
                decimal? Qty = (decimal)(data.Qty ?? 1);
                OutstandingAmount += (data.ProductAmount * Qty) - data.Discount;
            }

            foreach (var data in invoice.PaidPayments.Where(w => string.IsNullOrEmpty(w.Status) && (w.isDel ?? false) == false))
            {
                OutstandingAmount -= data.Amount;
            }

            OutstandingAmount -= (invoice.ManualDiscount ?? 0);

            if (OutstandingAmount == totalPaidAmount || OutstandingAmount <= 0)
            //if (invoice.OutstandingAmount == totalPaidAmount || invoice.OutstandingAmount  <= 0)
            {
                foreach (InvoiceItemDTO invoiceItem in invoice.InvoiceItems.Where(w => w.OutstandingAmount != 0 && w.IsDelete == false))
                {
                    PaymentDTO itemPayment = new PaymentDTO();

                    itemPayment.PaymentMethodId = payment.PaymentMethodId;
                    itemPayment.Amount = invoiceItem.OutstandingAmount;
                    itemPayment.InvoiceItemId = invoiceItem.InvoiceItemId;
                    itemPayment.CreateBy = updateBy;
                    itemPayment.UpdateBy = updateBy;
                    itemPayment.CreatedDate = updateDate;
                    itemPayment.UpdatedDate = updateDate;
                    paidPaymentDTO.Payments.Add(itemPayment);
                }

                totalPaidAmount = 0;
            }
            else if (invoice.TotalAmount == invoice.ManualDiscount)
            {
                foreach (InvoiceItemDTO invoiceItem in invoice.InvoiceItems.Where(w => w.OutstandingAmount != 0 && w.IsDelete == false))
                {
                    PaymentDTO itemPayment = new PaymentDTO();

                    itemPayment.PaymentMethodId = payment.PaymentMethodId;
                    itemPayment.Amount = invoiceItem.OutstandingAmount;
                    itemPayment.InvoiceItemId = invoiceItem.InvoiceItemId;
                    itemPayment.CreateBy = updateBy;
                    itemPayment.UpdateBy = updateBy;
                    itemPayment.CreatedDate = updateDate;
                    itemPayment.UpdatedDate = updateDate;
                    paidPaymentDTO.Payments.Add(itemPayment);
                }

                totalPaidAmount = 0;
            }
            else
            {
                foreach (InvoiceItemPayment itemPaid in payment.ItemsPaid.Where(w => w.IsChecked == isChecked))
                {
                    InvoiceItemDTO item = invoice.InvoiceItems.FirstOrDefault(w => w.InvoiceItemId == itemPaid.InvoiceItemId);
                    if (totalPaidAmount > 0)
                    {

                        if (item != null && item.OutstandingAmount != 0)
                        {
                            PaymentDTO itemPayment = new PaymentDTO();
                            decimal currentAmount = totalPaidAmount;
                            decimal currentCollected = 0;
                            currentAmount -= item.OutstandingAmount.Value;

                            if (currentAmount > 0)
                            {
                                totalPaidAmount -= item.OutstandingAmount.Value;
                                currentCollected = item.OutstandingAmount.Value;
                            }
                            else
                            {
                                currentCollected = totalPaidAmount;
                                totalPaidAmount -= totalPaidAmount;
                            }

                            itemPayment.PaymentMethodId = payment.PaymentMethodId;
                            itemPayment.Amount = currentCollected;
                            itemPayment.InvoiceItemId = item.InvoiceItemId;
                            itemPayment.CreateBy = updateBy;
                            itemPayment.UpdateBy = updateBy;
                            itemPayment.CreatedDate = DateTime.Now;
                            itemPayment.UpdatedDate = DateTime.Now;
                            paidPaymentDTO.Payments.Add(itemPayment);
                        }
                    }
                    //else if (totalDiscount > 0 && totalPaidAmount == 0)
                    //{
                    //    PaymentDTO itemPayment = new PaymentDTO();
                    //    itemPayment.PaymentMethodId = payment.PaymentMethodId;
                    //    itemPayment.Amount = item.OutstandingAmount.Value;
                    //    itemPayment.InvoiceItemId = item.InvoiceItemId;
                    //    itemPayment.CreateBy = updateBy;
                    //    itemPayment.UpdateBy = updateBy;
                    //    itemPayment.CreateDate = updateDate;
                    //    itemPayment.UpdateDate = updateDate;
                    //    paidPaymentDTO.Payments.Add(itemPayment);
                    //}
                    else
                    {
                        //if (item.OutstandingAmount > 0)
                        //{
                        //PaymentDTO itemPayment = new PaymentDTO();
                        //itemPayment.PaymentMethodId = payment.PaymentMethodId;
                        //itemPayment.Amount = item.OutstandingAmount.Value;
                        //itemPayment.InvoiceItemId = item.InvoiceItemId;
                        //itemPayment.CreateBy = updateBy;
                        //itemPayment.UpdateBy = updateBy;
                        //itemPayment.CreateDate = updateDate;
                        //itemPayment.UpdateDate = updateDate;
                        //paidPaymentDTO.Payments.Add(itemPayment);
                        //}
                        break;
                    }
                }
            }

        }
    }

    public class InvoicePayment
    {
        public int InvoiceId { get; set; }
        public string InvoiceCode { get; set; }
        public decimal PaidAmount { get; set; }
        public string PaymentMethodId { get; set; }
        public string Note { get; set; }
        public DateTime PaymentDate { get; set; }
        public List<InvoiceItemPayment> ItemsPaid { get; set; }
    }

    public class InvoiceItemPayment
    {
        public bool IsChecked { get; set; }
        public int InvoiceItemId { get; set; }
    }
}