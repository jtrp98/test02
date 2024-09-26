using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.StudentInfo
{
    public partial class ProfileImage : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            schoolid.Value = userData.CompanyID.ToString();
        }
    }
}