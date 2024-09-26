using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.UserControls
{
    public partial class TeacherAutocomplete : System.Web.UI.UserControl
    {
        public bool IsRequired { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsRequired)
            {
                txtStudentAutocomplete.Attributes["required"] = "true";
            }
        }

        public string GetText()
        {
            return txtStudentAutocomplete.Text;
        }

        public void SetText(string value)
        {
            txtStudentAutocomplete.Text = value;
        }
    }
}