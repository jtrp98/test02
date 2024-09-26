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

namespace FingerprintPayment.Modules.Invoices
{
    public partial class perRegisterIndex : Page
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

        public perRegisterIndex()
        {
            DraftStatus = Constants.Invoice.Status.Draft;
            ApproveStatus = Constants.Invoice.Status.Approve;
            PaidStatus = Constants.Invoice.Status.Paid;
            VoidStatus = Constants.Invoice.Status.Void;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) Response.Redirect("~/Default.aspx");
            UserName = Session["Emp_Name"].ToString();
        }
    }
}