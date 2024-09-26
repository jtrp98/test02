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
using MasterEntity;

namespace FingerprintPayment.Report
{
    public partial class Reportmobile02 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            {
                string sEntities = Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                hdfschoolname.Value = tCompany.sCompany;
            }
        }
    }
}