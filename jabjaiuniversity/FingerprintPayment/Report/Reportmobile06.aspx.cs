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
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.Reports_06;

namespace FingerprintPayment.Report
{
    public partial class Reportmobile06 : BehaviorGateway
    {
        //internal static JWTToken.userData userData = GetUserData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            var userData = GetUserData();
            SqlConnection _conn = fcommon.ConfigSqlConnection(Session["sEntities"].ToString());
            if (!this.IsPostBack)
            {
                //fcommon.ListDBToDropDownList(_conn, ddlyear, "select * from TYear where SchoolID = " + userData.CompanyID + " order by numberYear desc", "", "nYear", "numberYear");
                //ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                //using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                //{
                //    string sEntities = Session["sEntities"].ToString();
                //    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                //    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                //    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                //    hdfschoolname.Value = tCompany.sCompany;
                //}
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            var userData = GetUserData();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    if (search.sort_type == 0)
                    {
                        search.dEnd = search.dStart.Value;
                    }
                    else if (search.sort_type == 1)
                    {
                        search.dEnd = search.dStart.Value.AddMonths(1).AddDays(-1);
                        if (search.dEnd >= DateTime.Now) search.dEnd = DateTime.Now;
                    }
                    else if (search.sort_type == 2)
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        search.dStart = f_term?.dStart.Value;
                        search.dEnd = f_term?.dEnd.Value;
                        if (search.dEnd >= DateTime.Now) search.dEnd = DateTime.Now;
                    }

                    return ReportsType_01.GetReports(dbschool, search, userData);
                }
            }
        }
    }
}