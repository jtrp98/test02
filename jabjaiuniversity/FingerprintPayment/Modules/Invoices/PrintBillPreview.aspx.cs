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
    public partial class PrintBillPreview : System.Web.UI.Page
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
        public string ClassLabel { get; set; }

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

                using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
                {

                    if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                    {
                        Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
                        string entities = Session["sEntities"].ToString();

                        schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == entities);

                        BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));
                        Model = invoice.Result;
                        Model.InvoiceItems.RemoveAll(w => w.IsDelete);

                        var term = termLogic.GetById(invoice.Result.TermId);


                        var studentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany && w.sID == invoice.Result.StudentId && w.nTerm == invoice.Result.TermId).AsQueryable().FirstOrDefault();
                        var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == schoolData.nCompany).ToList();
                        var user = userLogic.GetById(invoice.Result.StudentId);
                        if (string.IsNullOrEmpty(invoice.Result.TermId) || studentViews == null)
                        {
                            invoice.Result.StudentIdCard = user.Result.Identification;
                            invoice.Result.StudentCode = user.Result.StudentID;
                            if (user.Result.TTermSubLevel2 != null && invoice.Result.SubLevel2 == user.Result.TTermSubLevel2.TSubLevel2)
                            {
                                invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                                invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                            }

                            var f_user = dbschool.TUser.Find(invoice.Result.StudentId, schoolData.nCompany);
                            Model.StudentName = getTitlte(q_titles, f_user.sStudentTitle) + " " + f_user.sName + " " + f_user.sLastname;
                        }
                        else
                        {
                            invoice.Result.StudentIdCard = user.Result.Identification;
                            invoice.Result.StudentCode = studentViews.sStudentID;
                            Model.StudentName = getTitlte(q_titles, studentViews.sStudentTitle) + " " + studentViews.sName + " " + studentViews.sLastname;
                            Model.SubLevel = studentViews.SubLevel;
                            Model.SubLevel2 = studentViews.nTSubLevel2;
                            Model.Term = studentViews.sTerm;
                            Model.TermYear = studentViews.numberYear.Value.ToString();
                        }

                        if (term.Result.sTerm != null)
                        {
                            invoice.Result.Term = term.Result.sTerm;
                            invoice.Result.nYear = term.Result.Year.Number.Value;
                        }

                        string SQL = string.Format(@"DECLARE @nTermSubLevel2 INT = {0};
DECLARE @nTerm NVARCHAR(20) = '{1}'

SELECT TOP 1 D1.* 
FROM TPlan AS A
INNER JOIN TPlanCourse AS B
ON A.SchoolID = B.SchoolID AND A.PlanId = B.PlanId 
INNER JOIN TPlanCourseTerm AS C
ON B.SchoolID = C.SchoolID AND B.PlanCourseId = C.PlanCourseId 
INNER JOIN TPlanTermSubLevel2 AS D
ON A.PlanId = D.PlanId AND A.SchoolID = D.SchoolID
INNER JOIN TCurriculum AS D1
ON D1.CurriculumId = A.CurriculumId AND A.SchoolID = D1.SchoolId
WHERE C.nTerm = @nTerm AND D.nTermSubLevel2 = @nTermSubLevel2 
AND D.IsActive = 1 AND A.IsActive = 1", (Model.Fd_NewTermClass_id ?? Model.SubLevel2Id), Model.TermId);

                        var q1 = dbschool.Database.SqlQuery<TCurriculum>(SQL).FirstOrDefault();
                        if (q1 != null)
                        {
                            ClassLabel = Model.SubLevel + " หลักสูตร " + q1.CurriculumName;
                        }
                        else
                        {
                            ClassLabel = Model.SubLevel;
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

                            foreach (var PaidPaymentsData in Model.PaidPayments)
                            {
                                PaidPaymentsData.Payments.RemoveAll(r => (r.isDel ?? false) == true);
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

                                if (invoice.Result.StudentId == 0)
                                {
                                    if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                                    {
                                        invoice.Result.SubLevel2 = invoice.Result.Fd_NewTermSubLevel2;
                                        invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                                    }
                                    else if (!string.IsNullOrEmpty(invoice.Result.Fd_NewTermSubLevel2))
                                    {
                                        invoice.Result.SubLevel2 = string.Empty;
                                        invoice.Result.SubLevel = invoice.Result.Fd_NewTermSubLevel;
                                    }
                                }

                                Model.ReceiptNo = paidPayment.ReceiptNo;
                                Model.paid_Discount = paidPayment.paid_Discount ?? 0;
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