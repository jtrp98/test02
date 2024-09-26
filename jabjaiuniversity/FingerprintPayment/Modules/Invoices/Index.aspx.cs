using Jabjai.BusinessLogic;
using Jabjai.Common;
using Jabjai.Object;
using Jabjai.Object.DTO.Transaction;
using Jabjai.Object.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiEntity.DB;
using System.Linq;
using JabjaiMainClass;
using System.Web;
using MasterEntity;
using Newtonsoft.Json.Linq;

namespace FingerprintPayment.Modules.Invoices
{
    public partial class Index : Page
    {
        public string UserName { get; set; }

        public string DraftStatus { get; }
        public string ApproveStatus { get; }
        public string PaidStatus { get; }
        public string VoidStatus { get; }
        public string OverDueDate
        {
            get
            {
                return DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public Index()
        {
            DraftStatus = Constants.Invoice.Status.Draft;
            ApproveStatus = Constants.Invoice.Status.Approve;
            PaidStatus = Constants.Invoice.Status.Paid;
            VoidStatus = Constants.Invoice.Status.Void;
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string InvoiceVoid(List<int> InvoiceId)
        {
            dynamic result = new JObject();
            try
            {
                using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                {
                    JWTToken token = new JWTToken();
                    var userData = new JWTToken().UserData;
                    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                    var q_Invoice = peakengine.TInvoices.Where(w => w.school_id == userData.CompanyID && InvoiceId.Contains(w.invoices_Id)).ToList();

                    foreach (var data in q_Invoice)
                    {
                        data.invoices_status = "void";
                        data.UpdatedDate = DateTime.Now;
                        data.UpdatedBy = userData.UserID;
                    }

                    peakengine.SaveChanges();

                    result.Result = true;
                }
                //result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.Message = "พบข้อผิดพลาดกรุณาติดต่อผู้ดูแลระบบ";
            }

            return result.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            UserName = Session["Emp_Name"].ToString();
        }
    }
}