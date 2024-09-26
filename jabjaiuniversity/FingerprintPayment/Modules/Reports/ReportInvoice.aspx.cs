using Jabjai.BusinessLogic;
using Jabjai.BusinessLogic.Master;
using Jabjai.Object;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.Reports
{
    public partial class ReportInvoice : System.Web.UI.Page
    {
        public string YearsJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
            var _yearLogic = new YearLogic();
            BaseResult<List<TYear>> resultYears = _yearLogic.GetSchoolId(userData.CompanyID);
            YearsJson = new JavaScriptSerializer().Serialize(resultYears.Result.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.YearId));
        }
    }
}