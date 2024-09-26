using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Report.Models;
using FingerprintPayment.Report.Functions.BehaviorsReports;

namespace FingerprintPayment.Report
{
    public partial class Behaviorsreports_2 : BehaviorGateway
    {
        public TBehaviorSetting setting = new TBehaviorSetting();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            if (!this.IsPostBack)
            {
                JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
                var dtYear = dbschool.TYears.Where(w => w.SchoolID == UserData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList();
                var userData = GetUserData();

                fcommon.LinqToDropDownList(dtYear, ddlyear, "", "nYear", "numberYear");
                ddlyear.SelectedValue = DateTime.Now.Year.ToString();
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    var q = QueryDataBases.SubLevel_Query.GetData(new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)), userData);
                    fcommon.LinqToDropDownList(q, ddlsublevel, "ทั้งหมด", "class_id", "class_name");
                    hdfschoolname.Value = tCompany.sCompany;

                    setting = dbschool.TBehaviorSettings.FirstOrDefault();
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object returnlist(Search search)
        {
            //JWTToken token = new JWTToken();
            //if (!token.CheckToken(HttpContext.Current)) { }
            var userData = GetUserData();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime dStart = DateTime.Today, dEnd = DateTime.Today;

                    if (!string.IsNullOrEmpty(search.term_id))
                    {
                        //search.term_id = string.Format("TS{0:0000000}", int.Parse(search.term_id));
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == search.term_id.Trim());
                        search.dStart = f_term.dStart.Value;
                        search.dEnd = f_term.dEnd.Value;
                    }
                    else
                    {
                        search.dStart = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && w.SchoolID == userData.CompanyID).Min(min => min.dStart) ?? DateTime.Today;
                        search.dEnd = dbschool.TTerms.Where(w => w.nYear == search.year_Id && w.cDel == null && w.SchoolID == userData.CompanyID).Max(max => max.dEnd) ?? DateTime.Today;
                    }

                    return reportsType_01.GetReports(dbschool, search, userData);

                }
            }
        }
    }
}