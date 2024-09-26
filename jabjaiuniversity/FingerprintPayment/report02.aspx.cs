using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using JabjaiMainClass;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class report02 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("Default.aspx");
            //setAutoComplete();
            #region include js
            HtmlGenericControl jsJquery = new HtmlGenericControl("script");

            jsJquery = new HtmlGenericControl("script");
            jsJquery.Attributes["type"] = "text/javascript";
            jsJquery.Attributes["src"] = "javascript/autocomplete.js";
            Page.Header.Controls.Add(jsJquery);
            #endregion
            if (!IsPostBack)
            {
                fcommon.ListMonths(ddlListMonth, "");
                fcommon.ListYears(ddlListYear, "", 2013, "", "");
            }
            lodareport();
        }

        void Button1_Click(object sender, EventArgs e)
        {
            lodareport();
        }

        private void lodareport()
        {
//            Report.cryreport02 reportdocument = new Report.cryreport02();
//            //reportdocument.Load(Server.MapPath("Report/report02.rpt"));
//            string[] settings = ConfigurationManager.ConnectionStrings["FingerPaymentConnectionString"].ToString().Split(';');
//            reportdocument.SetDatabaseLogon(settings[2].Split('=')[1], settings[3].Split('=')[1], settings[0].Split('=')[1], settings[1].Split('=')[1]);
//            string sReport = "รายงานการขายสินค้าในระบบ";
//            string SQL = @"SELECT TB2.SNAME + ' ' + TB2.SLASTNAME AS SNAME,DSELL,SPRODUCT,TB3.NNUMBER
//            ,TB3.NNUMBER * TB4.NPRICE AS NTOTAL,SBARCODE,TB4.NPRICE,'รายงานการขายสินค้าในระบบ' AS SREPORTNAME
//            ,dLog,TB1.nSession			
//            FROM TSELL TB1 INNER JOIN TUSER TB2 ON TB1.SID = TB2.SID
//            INNER JOIN TSELL_DETAIL TB3 ON TB1.SSELLID = TB3.NSELL
//            INNER JOIN TPRODUCT TB4 ON TB3.NPRODUCT = TB4.NPRODUCTID
//            INNER JOIN TEMPLOYEES TB5 ON TB1.NEMP = TB5.SEMP
//            INNER JOIN TSystemlog TB6 ON CONVERT(VARCHAR(10),TB6.dLog,10) = CONVERT(VARCHAR(10),TB1.dSell,10) AND TB6.nSession = TB1.nSession
//            WHERE 1=1  AND Year(dSell) = " + ddlListYear.Text;
//            if (!string.IsNullOrEmpty(txtsunit.Text))
//            {
//                SQL += " AND TB5.sName + ' ' + TB5.sLastName = '" + txtsunit.Text + "' ";
//            }
//            if (ddlListMonth.SelectedIndex != 0)
//            {
//                SQL += " AND Month(dSell) = " + (ddlListMonth.SelectedIndex + 1) + " ";
//            }
//            SQL += " ORDER BY TB1.SID";
//            DataTable _dt = fcommon.Get_Data(_conn, SQL);
//            reportdocument.SetDataSource(_dt);
//            //reportdocument.SetParameterValue("sReportName", sReport);
//            CrystalReportViewer1.ReportSource = reportdocument;
//            CrystalReportViewer1.DataBind();
//            reportdocument.Refresh();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCompletionList(string prefixText)
        {
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            DataTable dt = fcommon.LinqToDataTable(_db.TEmployees.Where(w => (w.sName + " " + w.sLastname).Contains(prefixText)));
            List<string> CityNames = new List<string>();
            foreach (DataRow _dr in dt.Rows)
            {
                CityNames.Add(_dr["sName"] + " " + _dr["sLastname"]);
            }
            return CityNames;
        }

    }
}