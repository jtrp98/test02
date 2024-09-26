using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PaymentGateway
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string pt = Request.QueryString["pt"]; // pt: payment type - 1: Topup, 2: Tuition Fees(qr code), 3: Tuition Fees(credit card)
                string studentID = Request.QueryString["studentID"];
                string amount = Request.QueryString["amount"];
                string invoiceID = "";
                string lang = Request.QueryString["lang"]; // lang: th, en

                string urlPath = "";
                int.TryParse(studentID, out int sID);

                using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                {
                    var userData = mctx.TUsers.FirstOrDefault(f => f.sID == sID);
                    var schoolData = mctx.TCompanies.FirstOrDefault(f => f.nCompany == userData.nCompany);
                    var paymentGatewayObj = mctx.TB_PaymentGateway.Where(w => w.Fd_SchoolID == userData.nCompany).FirstOrDefault();
                    if (paymentGatewayObj != null)
                    {
                        if (paymentGatewayObj.Fd_KTBPayment ?? false)
                        {
                            // KTB
                            switch (pt)
                            {
                                case "1": urlPath = "KTB/Topup.aspx"; break;
                                case "2": 
                                case "3": urlPath = "KTB/TuitionFees/Billing.aspx"; break;
                            }
                        }
                        else
                        {
                            // KBank
                            switch (pt)
                            {
                                case "1": urlPath = "KBank/Checkout.aspx"; break;
                                case "2": urlPath = "KBank/TuitionFees/CheckoutQR.aspx"; break;
                                case "3": urlPath = "KBank/TuitionFees/CheckoutCard.aspx"; break;
                            }
                        }
                    }
                    else
                    {
                        //throw new Exception("The system has not been activated.");
                        ltrMessage.Text = "The system has not been activated.";
                    }
                }

                switch (pt)
                {
                    case "1":
                        Response.Redirect($"{urlPath}?studentID={studentID}&amount={amount}&lang={lang}");
                        break;
                    case "2":
                    case "3":
                        invoiceID = Request.QueryString["invoiceID"];
                        Response.Redirect($"{urlPath}?studentID={studentID}&amount={amount}&invoiceID={invoiceID}&lang={lang}");
                        break;
                }
            }
        }
    }
}