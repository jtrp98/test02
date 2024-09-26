using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class Reportmobile01 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear order by numberYear desc", "", "nYear", "numberYear");
                fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "ทั้งหมด", "nTSubLevel", "SubLevel");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
    }
}