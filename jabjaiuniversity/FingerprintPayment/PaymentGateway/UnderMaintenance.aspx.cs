using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.PaymentGateway
{
    public partial class UnderMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string lang = Request.QueryString["lang"];
                if (string.IsNullOrEmpty(lang)) lang = "th";
                switch (lang)
                {
                    case "th": Thread.CurrentThread.CurrentUICulture = new CultureInfo("th-TH"); break;
                    case "en": Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); break;
                }

                this.ltrPay.Text = string.Format(@"<div class=""section-top"">
        <div class="""">
            <div class=""container"">
                <div class=""section-top"" style=""padding: 0 15px 0 15px; height: 100%; text-align: center; margin-top: 18%;"">
                    <img src=""../../../images/School Bright logo 1 storke222.png"" style=""width: 82%;"" />
                    <img src=""../../../images/ic_alert.png"" style=""width: 30%;"" />
                    <div class=""under-maintenance"">
                        <p class=""message"">
                            {0}
                        </p>
                    </div>
                    <div class=""under-maintenance"">
                        <img class=""contact"" src=""../../../images/ic_contact.png"" />
                        <p class=""contact"" style=""padding-top: 12px;"">
                            {1}
                        </p>
                        <p class=""contact"">
                            {2}
                        </p>
                        <p class=""contact"">
                            LINE Offcial : @jabjai
                        </p>
                    </div>
                </div>
                <hr class=""under-maintenance"">
                <div class=""section-bottom under-maintenance"">
                    <a href=""../../../closepage.html"" class=""confirm-btn btn btn-success"">{3}
                    </a>
                </div>
            </div>
        </div>
    </div>", GetGlobalResourceObject("PaymentGateway_UnderMaintenance.aspx", "System-Message01"), GetGlobalResourceObject("PaymentGateway_UnderMaintenance.aspx", "Contact01"), GetGlobalResourceObject("PaymentGateway_UnderMaintenance.aspx", "Contact02"), GetGlobalResourceObject("PaymentGateway_UnderMaintenance.aspx", "Button-BackToMain"));

                return;
            }
        }
    }
}