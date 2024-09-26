using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class perRegisterPrintBillPreview : System.Web.UI.Page
    {
        private InvoiceLogic invoiceLogic;
        private PaymentLogic paymentLogic;
        private UserLogic userLogic;
        private TermLogic termLogic;
        public InvoiceDTO Model { get; set; }
        public Jabjai.Object.Entity.Jabjai.TUser ModelUser { get; set; }
        public string ModelJson { get; set; }
        public bool HideCheckBox { get; set; }
        public int PaidPeriod { get; set; }
        public decimal TotalAllPaid { get; set; }
        public TCompany schoolData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            invoiceLogic = new InvoiceLogic();
            paymentLogic = new PaymentLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();
            HideCheckBox = false;
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                {
                    Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
                    string entities = Session["sEntities"].ToString();
                    
                    using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                    {

                    
                        schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == entities);

                    BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));
                    Model = invoice.Result;
                    Model.InvoiceItems.RemoveAll(w => w.IsDelete);

                    var term = termLogic.GetById(invoice.Result.TermId);
                        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                        {

                            var studentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany && w.sID == invoice.Result.StudentId && w.nTerm == invoice.Result.TermId).AsQueryable().FirstOrDefault();
                            var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == schoolData.nCompany).AsQueryable().ToList();
                            var user = userLogic.GetById(invoice.Result.StudentId);
                            //if (string.IsNullOrEmpty(invoice.Result.TermId) || studentViews == null)
                            //{
                            //    if (user.Result != null)
                            //    {
                            //        invoice.Result.StudentIdCard = user.Result.Identification;
                            //        invoice.Result.StudentCode = user.Result.StudentID;
                            //        if (user.Result.TTermSubLevel2 != null && invoice.Result.SubLevel2 == user.Result.TTermSubLevel2.TSubLevel2)
                            //        {
                            //            invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                            //            invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                            //        }
                            //    }

                            var f_user = dbschool.TPreRegisters.Find(invoice.Result.preRegisterId, schoolData.nCompany);
                            Model.StudentName = getTitlte(q_titles, f_user.StudentTitle.ToString()) + " " + f_user.sName + " " + f_user.sLastname;
                            Model.StudentIdCard = f_user.sIdentification;
                            Model.StudentCode = f_user.sStudentID;
                            Model.nYear = f_user.registerYear + 543;

                            //}
                            //else
                            //{
                            //    invoice.Result.StudentIdCard = user.Result.Identification;
                            //    invoice.Result.StudentCode = studentViews.sStudentID;
                            //    Model.StudentName = getTitlte(q_titles, studentViews.sStudentTitle) + " " + studentViews.sName + " " + studentViews.sLastname;
                            //    Model.SubLevel = studentViews.SubLevel;
                            //    Model.SubLevel2 = studentViews.nTSubLevel2;
                            //    Model.Term = studentViews.sTerm;
                            //    Model.TermYear = studentViews.numberYear.Value.ToString();
                            //}

                            if (user.Result != null && term.Result.sTerm != null)
                            {
                                invoice.Result.Term = term.Result.sTerm;
                                invoice.Result.nYear = term.Result.Year.Number.Value;
                            }

                            if (Request.QueryString["paidPaymentId"] != null)
                            {
                                PaidPaymentDTO paidPayment = invoice.Result.PaidPayments
                                     .OrderBy(o => o.PaidPaymentId)
                                     .Where(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]))
                                     .FirstOrDefault();

                                if (paidPayment.PaymentDate.HasValue)
                                {
                                    Model.Date = paidPayment.PaymentDate.Value;
                                }

                                TotalAllPaid = invoice.Result.PaidPayments.Where(w => w.Status != EnumPaymentStatus.Void.ToString()).Sum(s => s.Amount).Value;

                                if (paidPayment != null)
                                {
                                    //HideCheckBox = true;
                                    int periodIndex = invoice.Result.PaidPayments
                                        .OrderBy(o => o.PaidPaymentId)
                                        .ToList()
                                        .FindIndex(w => w.Status != EnumPaymentStatus.Void.ToString() && w.PaidPaymentId == Convert.ToInt32(Request.QueryString["paidPaymentId"]));
                                    PaidPeriod = periodIndex += 1;

                                    List<int> paidPaymentItem = new List<int>();
                                    foreach (PaymentDTO payment in paidPayment.Payments)
                                    {
                                        paidPaymentItem.Add(payment.InvoiceItemId.Value);
                                        invoice.Result.InvoiceItems.Where(w => w.InvoiceItemId == payment.InvoiceItemId)
                                            .ToList().ForEach(w =>
                                            {
                                                w.OutstandingAmount = payment.Amount;
                                            });
                                    }
                                    invoice.Result.InvoiceItems.RemoveAll(w => !paidPaymentItem.Contains(w.InvoiceItemId));

                                    Model.ReceiptNo = paidPayment.ReceiptNo;
                                    Model.paid_Discount = paidPayment.paid_Discount ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Model = new InvoiceDTO();
            }
            ModelJson = new JavaScriptSerializer().Serialize(Model);
        }
        private string getTitlte(List<TTitleList> titles, string titlesId)
        {
            int nTitleid = 0;
            int.TryParse((titlesId ?? "0"), out nTitleid);
            var f_titles = titles.FirstOrDefault(f => f.nTitleid == nTitleid);
            if (f_titles == null) return titlesId;
            else return f_titles.titleDescription;
        }
    }
}