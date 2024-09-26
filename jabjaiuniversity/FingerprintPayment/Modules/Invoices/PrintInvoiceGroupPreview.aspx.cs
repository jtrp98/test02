using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using JabjaiEntity.DB;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class PrintInvoiceGroupPreview : System.Web.UI.Page
    {
        private InvoiceLogic invoiceLogic;
        private UserLogic userLogic;
        public TermLogic termLogic;
        public List<InvoiceDTO> Model { get; set; }
        public Jabjai.Object.Entity.Jabjai.TUser ModelUser { get; set; }
        public string REF { get; set; }
        public string ModelJson { get; set; }
        public string DueDate { get; set; }
        public decimal FirstPaid { get; set; }
        public decimal SecondPaid { get; set; }
        public TCompany schoolData { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal OutStadingAmount { get; set; }
        public List<PaymentMethodDTO> paymentMethod { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            invoiceLogic = new InvoiceLogic();
            userLogic = new UserLogic();
            termLogic = new TermLogic();
            BankLogic bankLogic = new BankLogic();
            PaymentMethodLogic paymentMethodLogic = new PaymentMethodLogic();
            using (JabJaiMasterEntities masterEntities = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolData, ConnectionDB.Read)))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["TermSubLevel2Id"]))
                {
                    Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
                    string entities = Session["sEntities"].ToString();

                    schoolData = masterEntities.TCompanies.FirstOrDefault(f => f.sEntities == entities);


                    string TermId = "";
                    if (string.IsNullOrEmpty(Request.QueryString["TermId"] + ""))
                    {
                        StudentLogic logic = new StudentLogic(dbschool);
                        TermId = logic.GetTermId(new JWTToken.userData { CompanyID = schoolData.nCompany }).Trim();
                    }
                    else
                    {
                        TermId = Request.QueryString["TermId"] + "";
                    }

                    int TermSubLevel2Id = Convert.ToInt32(Request.QueryString["TermSubLevel2Id"].ToString());
                    var q_studentId = (from a in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany)
                                       where a.nTermSubLevel2 == TermSubLevel2Id && a.nTerm == TermId
                                       select a.sID).AsQueryable().ToList();

                    BaseResult<List<InvoiceDTO>> invoice = invoiceLogic.GetBySubLevel2Id(Convert.ToInt32(Request.QueryString["TermSubLevel2Id"].ToString()), schoolData.nCompany, TermId);
                    //BaseResult<List<InvoiceDTO>> invoice = invoiceLogic.GetBySubLevel2Id(q_studentId, TermId, schoolData.nCompany);
                    Model = invoice.Result;

                    //Model.InvoiceItems.RemoveAll(w => w.IsDelete);
                    var term = termLogic.GetAll();
                    //var user = userLogic.GetById(invoice.Result.StudentId);
                    //invoice.Result.StudentIdCard = user.Result.StudentIdCardNumber;
                    //invoice.Result.StudentCode = user.Result.StudentID;
                    //if (user.Result.TTermSubLevel2 != null)
                    //{
                    //    invoice.Result.SubLevel = user.Result.TTermSubLevel2.TSubLevel.SubLevel;
                    //    invoice.Result.SubLevel2 = user.Result.TTermSubLevel2.TSubLevel2;
                    //}

                    //List<int> q_studentId = new List<int>();
                    //Model.ForEach(f => q_studentId.Add(f.StudentId));

                    var q_titles = dbschool.TTitleLists.Where(w => w.SchoolID == schoolData.nCompany).ToList();
                    var studentData = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolData.nCompany && w.nTermSubLevel2 == TermSubLevel2Id && w.nTerm == TermId).AsQueryable().ToList();
                    Model.ForEach(f =>
                    {
                        var data = studentData.FirstOrDefault(f1 => f1.sID == f.StudentId && f1.nTerm.Trim() == f.TermId.Trim());

                        if (!string.IsNullOrEmpty(f.LabelSubLevel)) f.SubLevel = f.LabelSubLevel;
                        if (!string.IsNullOrEmpty(f.LabelSubLevel2)) f.SubLevel2 = f.LabelSubLevel2;
                        if (data != null)
                        {
                            f.StudentName = getTitlte(q_titles, data.sStudentTitle) + " " + data.sName + " " + data.sLastname;
                            f.SubLevel = data.SubLevel;
                            f.SubLevel2 = data.nTSubLevel2;
                            f.TermYear = data.numberYear.ToString();
                            f.Term = data.sTerm;

                            if (data.nStudentStatus == 2)
                            {
                                f.IsDelete = true;
                            }
                        }
                        else
                        {
                            var f2 = dbschool.TUser.FirstOrDefault(f1 => f1.sID == f.StudentId);
                            if (f2 != null)
                            {
                                f.StudentName = getTitlte(q_titles, f2.sStudentTitle) + " " + f2.sName + " " + f2.sLastname;
                            }
                        }

                        if (!string.IsNullOrEmpty(f.Fd_NewTermSubLevel2))
                        {
                            f.SubLevel2 = f.Fd_NewTermSubLevel2;
                            f.SubLevel = f.Fd_NewTermSubLevel;
                        }
                        else if (!string.IsNullOrEmpty(f.Fd_NewTermSubLevel2))
                        {
                            f.SubLevel2 = string.Empty;
                            f.SubLevel = f.Fd_NewTermSubLevel;
                        }

                        foreach (var PaidPaymentsData in f.PaidPayments)
                        {
                            PaidPaymentsData.Payments.RemoveAll(r => (r.isDel ?? false) == true);
                        }
                    });

                    Model = Model.Where(w => w.IsDelete == false).ToList();

                    var q_banks = bankLogic.GetAll().Result;
                    paymentMethod = paymentMethodLogic.GetBySchoolId(schoolData.nCompany).Result;
                    paymentMethod.ForEach(f =>
                    {
                        if (f.BankId.HasValue)
                        {
                            var f_banks = q_banks.FirstOrDefault(f1 => f1.BankId == f.BankId);
                            if (f_banks != null) f.BankName = f_banks.BankName;
                        }
                    });

                    //if (term.Result != null)
                    //{
                    //    invoice.Result.Term = term.Result.sTerm;
                    //    invoice.Result.nYear = term.Result.Year.Number;
                    //}
                    //DueDate = invoice.Result.DueDate.ToBuddhishDateString();

                    //var paidPayments = invoice.Result.PaidPayments.Where(w => string.IsNullOrEmpty(w.Status)).Sum(s => s.Payments.Sum(s1 => s1.Amount)) ?? 0;

                    //FirstPaid = (decimal)invoice.Result.TotalAmount * 60 / 100;
                    //SecondPaid = (decimal)invoice.Result.TotalAmount * 40 / 100;

                    //if (FirstPaid >= paidPayments) FirstPaid -= paidPayments;
                    //else
                    //{
                    //    SecondPaid -= (paidPayments - FirstPaid);
                    //    FirstPaid = 0;
                    //}

                    //TotalPaid = invoice.Result.GrandTotal.Value - invoice.Result.OutstandingAmount.Value;
                    //OutStadingAmount = invoice.Result.OutstandingAmount.Value;

                    //REF = invoice.Result.StudentCode + "-" + invoice.Result.nYear + "-" + invoice.Result.Term;
                }

                ModelJson = new JavaScriptSerializer().Serialize(Model);
            }
        }

        public string dateTimeFormat(DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy", new CultureInfo("th-th"));
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