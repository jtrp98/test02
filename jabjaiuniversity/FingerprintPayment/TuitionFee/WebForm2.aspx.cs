using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using Accounting;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace FingerprintPayment.TuitionFee
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Click += Button1_Click;
            Button2.Click += Button2_Click;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities entity = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    int registerYear = 0;
                    int.TryParse(TextBox2.Text, out registerYear);
                    foreach (var f_preRegister in entity.TPreRegisters.Where(w => w.registerYear == registerYear && w.SchoolID == qcompany.nCompany))
                    {
                        Accounting.Tuitionfee.Setting.SaveInvoiceData(f_preRegister);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities entity = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    int preRegisterId = 0;
                    int.TryParse(TextBox1.Text, out preRegisterId);
                    var f_preRegister = entity.TPreRegisters.FirstOrDefault(f => f.preRegisterId == preRegisterId);
                    if (f_preRegister != null)
                    {
                        Accounting.Tuitionfee.Setting.SaveInvoiceData(f_preRegister);
                    }
                }
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static Object UpdateInvoice(int invoices_Id)
        {
            dynamic rss = new JObject();
            using (PeakengineEntities peakengineEntities = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                using (JabJaiEntities entities = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
                {
                    var f_invoice = peakengineEntities.TInvoices.FirstOrDefault(f => f.invoices_Id == invoices_Id);

                    var q_Detail = peakengineEntities.TInvoices_Detail.Where(w => w.invoices_Id == invoices_Id && w.IsDelete == false).ToList();
                    var q_Paid_Payment = peakengineEntities.Paid_Payment.Where(w => w.InvoiceId == invoices_Id && (w.isDel ?? false) == false && string.IsNullOrEmpty(w.status)).ToList();

                    int? student_id = null;

                    if (f_invoice.student_id == 0)
                    {
                        var perResigter = entities.TPreRegisters.FirstOrDefault(f => f.preRegisterId == f_invoice.preRegisterId);
                        if (perResigter != null && perResigter.saveAsSID.HasValue) student_id = perResigter.saveAsSID;
                    }
                    else
                    {
                        student_id = f_invoice.student_id;
                    }

                    if (student_id == 0)
                    {
                        return new { Status = "Not Data" };
                    }

                    var studentViews = entities.TB_StudentViews.Where(f => f.sID == student_id && f.nTerm == f_invoice.nTerm).AsQueryable().FirstOrDefault();

                    if (studentViews != null)
                    {
                        f_invoice.nTermSubLevel2 = studentViews.nTermSubLevel2;
                        f_invoice.nTSubLevel = studentViews.nTSubLevel;
                        f_invoice.LabelSubLevel = studentViews.SubLevel;
                        f_invoice.SubLevel = studentViews.SubLevel;
                        f_invoice.LabelSubLevel2 = studentViews.nTSubLevel2;
                        f_invoice.SubLevel2 = studentViews.nTSubLevel2;
                        f_invoice.StudentName = studentViews.sName + " " + studentViews.sLastname;
                    }

                    decimal? OutstandingAmount = 0;
                    OutstandingAmount += q_Detail.Sum(s => (s.price * (decimal)(s.quantity ?? 1)) - (s.Discount ?? 0));

                    OutstandingAmount -= q_Paid_Payment.Sum(s => s.Amount - (s.paid_Discount ?? 0));
                    OutstandingAmount -= f_invoice.ManualDiscount ?? 0;

                    f_invoice.OutstandingAmount = OutstandingAmount <= 0 ? 0 : OutstandingAmount;

                    if (OutstandingAmount == 0)
                    {
                        f_invoice.invoices_status = "Paid";
                    }
                    else
                    {
                        f_invoice.invoices_status = "Approve";
                    }

                    f_invoice.isDel = false;
                    peakengineEntities.SaveChanges();
                    //string data = new JavaScriptSerializer().Serialize(f_invoice);

                    return new { Status = "Success" };
                }
            }

        }


    }
}