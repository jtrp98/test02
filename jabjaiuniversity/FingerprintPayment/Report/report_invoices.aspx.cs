using FingerprintPayment.Report.Functions;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Report
{
    public partial class report_invoices : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where schoolid = " + userData.CompanyID + " order by numberYear desc", "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static Reports_data returnlist(ReportInvoice_Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                ReportInvoicesFunctions reportInvoices = new ReportInvoicesFunctions();
                Reports_data reports_Data = reportInvoices.Init(search, userData.CompanyID);
                return reports_Data;
            }
            catch (Exception ex)
            {
                var q_reports = new Reports_data();
                q_reports.errorMessage = fcommon.WriteErrorLog(ex);
                return q_reports;
            }
        }
    }
}