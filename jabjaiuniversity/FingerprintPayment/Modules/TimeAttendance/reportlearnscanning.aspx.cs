using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System.Data.SqlClient;
using MasterEntity;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class reportlearnscanning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                DataTable dtYear = fcommon.LinqToDataTable(dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList());

                DataTable dttSubLevel = fcommon.LinqToDataTable(dbschool.TSubLevels.Where(w=>w.SchoolID == userData.CompanyID).ToList());
                fcommon.ListDataTableToDropDownList(dtYear, ddlyear, "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)),userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;
                }
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> SearchCustomers(string prefixText, int count)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db2 = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            DataTable dtName = fcommon.LinqToDataTable(_db2.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null && (w.sName.Contains(prefixText) || w.sLastname.Contains(prefixText) || w.sIdentification.Contains(prefixText))));
            List<string> customers = new List<string>();
            if (dtName != null)
            {
                foreach (DataRow dr in dtName.Rows)
                {
                    customers.Add(dr["sName"] + " " + dr["sLastname"]);
                }
            }
            return customers;
        }
    }
}