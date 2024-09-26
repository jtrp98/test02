using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using JabjaiMainClass;

namespace FingerprintPayment
{
    public partial class Reportmobile03 : System.Web.UI.Page
    {
        public static SqlConnection _conn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionSQL"]);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear order by numberYear desc", "", "nYear", "numberYear");
                fcommon.ListDBToDropDownList(_conn, ddlsublevel, "select * from TSubLevel", "ทั้งหมด", "nTSubLevel", "SubLevel");
            }
        }
    }
}