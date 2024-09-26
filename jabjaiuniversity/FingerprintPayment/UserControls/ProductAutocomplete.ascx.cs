using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.UserControls
{
    public partial class ProductAutocomplete : System.Web.UI.UserControl
    {
        public bool IsRequired { get; set; }
        public string ShopID { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtProductAutocomplete.Attributes["data-shopid"] = ShopID + "";

            if (IsRequired)
            {
                txtProductAutocomplete.Attributes["required"] = "true";
            }
        }

        public string GetText()
        {
            return txtProductAutocomplete.Text;
        }

        public void SetText(string value)
        {
            txtProductAutocomplete.Text = value;
        }
    }
}