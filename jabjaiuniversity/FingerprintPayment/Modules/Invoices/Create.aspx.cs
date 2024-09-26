using Jabjai.BusinessLogic;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using System;
using System.Globalization;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class Create : Page
    {
        public string DraftStatus { get; }
        public string ApproveStatus { get; }
        public string VoidStatus { get; }
        private InvoiceLogic invoiceLogic;
        public InvoiceDTO Model { get; set; }
        public string ModelJson { get; set; }
        public bool IsLocked { get; set; }
        public string nTermID { get; set; }

        public Create()
        {
            DraftStatus = Constants.Invoice.Status.Draft;
            ApproveStatus = Constants.Invoice.Status.Approve;
            VoidStatus = Constants.Invoice.Status.Void;
            IsLocked = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            invoiceLogic = new InvoiceLogic();
            Model = new InvoiceDTO();
            Model.Date = DateTime.Now;
            Model.DueDate = DateTime.Now;
            Model.InvoiceStatus = DraftStatus;

            if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]) || !string.IsNullOrEmpty(Request.QueryString["Mode"]))
            {

                Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
                if (!string.IsNullOrEmpty(Request.QueryString["InvoiceId"]))
                {
                    BaseResult<InvoiceDTO> invoice = invoiceLogic.GetById(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString()));
                    Model = invoice.Result;
                    if (Model != null)
                    {
                        Model.InvoiceItems.RemoveAll(w => w.IsDelete);
                        Model.PaidPayments.RemoveAll(w => w.isDel ?? false);
                        Model.PaidPayments.ForEach(f => f.Payments.RemoveAll(r => (r.isDel ?? false) == true || r.IsDelete == true));
                        Model.PaidPayments = Model.PaidPayments.OrderBy(o => o.PaymentGroupId).ToList();

                        using (JabJaiMasterEntities jabJaiMasterEntities = Connection.MasterEntities(ConnectionDB.Read))
                        {
                            var SchoolId = Model.SchoolId;
                            var f_school = jabJaiMasterEntities.TCompanies.FirstOrDefault(f => f.nCompany == SchoolId);
                            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(f_school, ConnectionDB.Read)))
                            {
                                var f_studet = entities.TUser.FirstOrDefault(f => f.sID == Model.StudentId);
                                var q_titles = entities.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).ToList();
                                Model.StudentName = getTitlte(q_titles, f_studet.sStudentTitle) + " " + f_studet.sName + " " + f_studet.sLastname;
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["Mode"]) && Request.QueryString["Mode"].ToString().ToLower() == "edit")
                {
                    //if ((Model.InvoiceStatus == DraftStatus || Model.InvoiceStatus == ApproveStatus)
                    //    && (Model.PaidStatus.HasValue && !Model.PaidStatus.Value))
                    if (Model.OutstandingAmount > 0)
                    {
                        IsLocked = false;
                        if (Model.OutstandingAmount == Model.GrandTotal)
                        {
                            btnSpan.Visible = true;
                            btnBack.Visible = false;
                            spanBtnVoid.Visible = true;
                            btnDraft.Visible = true;
                            spanBtnDelete.Visible = true;
                            IsLocked = false;
                        }
                        else
                        {
                            btnSpan.Visible = true;
                            btnBack.Visible = true;
                            spanBtnVoid.Visible = false;
                            btnDraft.Visible = false;
                        }
                    }
                    else if (Model.OutstandingAmount < 0)
                    {
                        btnSpan.Visible = true;
                        spanBtnVoid.Visible = true;
                        spanBtnCancel.Visible = true;
                        spanBtnDelete.Visible = true;
                        btnBack.Visible = true;
                        IsLocked = false;
                    }
                    else
                    {
                        spanBtnVoid.Visible = false;
                        btnBack.Visible = true;
                        btnSpan.Visible = false;
                        spanBtnCancel.Visible = false;
                    }
                }
                else if (!string.IsNullOrEmpty(Request.QueryString["Mode"]) && Request.QueryString["Mode"].ToString().ToLower() == "update")
                {
                    //if ((Model.InvoiceStatus == DraftStatus || Model.InvoiceStatus == ApproveStatus)
                    //    && (Model.PaidStatus.HasValue && !Model.PaidStatus.Value))
                    using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                    {
                        peakengine.SPInvoicesUpdate(Convert.ToInt32(Convert.ToInt32(Request.QueryString["InvoiceId"].ToString())));
                    }

                    btnBack.Visible = true;
                    btnSpan.Visible = false;
                    spanBtnVoid.Visible = false;
                    spanBtnDelete.Visible = false;
                    spanBtnCancel.Visible = false;
                }
                else if (string.IsNullOrEmpty(Request.QueryString["InvoiceId"]) && string.IsNullOrEmpty(Request.QueryString["Mode"]))
                {
                    btnSpan.Visible = true;
                    btnBack.Visible = false;
                    spanBtnVoid.Visible = false;
                    spanBtnDelete.Visible = false;
                    spanBtnCancel.Visible = false;
                }
                else
                {
                    btnBack.Visible = true;
                    btnSpan.Visible = false;
                    spanBtnVoid.Visible = false;
                    spanBtnDelete.Visible = false;
                    spanBtnCancel.Visible = false;
                }
            }
            else
            {
                using (JabJaiMasterEntities jabJaiMasterEntities = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var SchoolId = Model.SchoolId;
                    var f_school = jabJaiMasterEntities.TCompanies.FirstOrDefault(f => f.nCompany == SchoolId);
                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(f_school, ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(entities);
                        nTermID = studentLogic.GetTermId(userData);
                    }
                }
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