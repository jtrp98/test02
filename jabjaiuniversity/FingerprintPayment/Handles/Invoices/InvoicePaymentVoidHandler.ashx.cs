using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace FingerprintPayment.Handles.Invoices
{
    /// <summary>
    /// Summary description for InvoicePaymentVoidHandler
    /// </summary>
    public class InvoicePaymentVoidHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) context.Response.Redirect("~/Default.aspx");
            var userData = new JWTToken().UserData;

            BaseResult<bool> result = new BaseResult<bool>();

            try
            {
                int schoolId = 0;
                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), context.Session["sEntities"].ToString(), ref schoolId);
                var invoiceId = context.Request.Form["invoiceId"];
                var paidPaymentId = context.Request.Form["paidPaymentId"];
                int UpdatedBy = int.Parse(context.Session["sEmpID"].ToString());  //#HardcodeuserName
                PaymentLogic paymentLogic = new PaymentLogic();
                if (invoiceId != null && paidPaymentId != null)
                    result = paymentLogic.UpdateStatusVoid(Convert.ToInt32(invoiceId), Convert.ToInt32(paidPaymentId));

                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    int paidpayment_id = int.Parse(paidPaymentId);
                    var f1 = peakengine.Paid_Payment.FirstOrDefault(f => f.paidpayment_id == paidpayment_id);
                    foreach (var data in peakengine.Paid_Payment.Where(w => w.InvoiceId == f1.InvoiceId && string.IsNullOrEmpty(f1.status)))
                    {
                        data.paid_Discount = 0;
                    }

                    f1.UpdatedDate = DateTime.Now;
                    f1.UpdatedBy = UpdatedBy;
                    peakengine.SaveChanges();
                    peakengine.SPInvoicesUpdate(Convert.ToInt32(invoiceId));

                    InvoiceLogic invoiceLogic = new InvoiceLogic();
                    BaseResult<InvoiceDTO> resultInvoice = invoiceLogic.GetById(Convert.ToInt32(invoiceId));
                    InvoiceDTO invoice = resultInvoice.Result;

                    if (invoice.preRegisterId.HasValue)
                    {
                        using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                        {
                            var f_preRegisters = entities.TPreRegisters.FirstOrDefault(f => f.preRegisterId == invoice.preRegisterId);
                            if (f_preRegisters != null)
                            {
                                if (peakengine.Paid_Payment.Count(c => c.InvoiceId == invoice.InvoiceId && string.IsNullOrEmpty(c.status) && (c.isDel ?? false) == false) > 0)
                                {
                                    f_preRegisters.paymentStatus = 1;
                                }
                                else
                                {
                                    f_preRegisters.paymentStatus = 0;
                                }

                                entities.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
                result.SystemErrorMessage = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(new JavaScriptSerializer().Serialize(result));
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}