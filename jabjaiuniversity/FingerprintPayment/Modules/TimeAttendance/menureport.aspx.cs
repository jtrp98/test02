using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class menureport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("./Default.aspx");
            string checkpermission = HttpContext.Current.Session["sEmpID"].ToString().Substring(14, 1);

            if (!String.IsNullOrEmpty(checkpermission))
            {
                if (checkpermission == "0")
                {
                }
            }
        }
    }
}