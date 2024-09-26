using Jabjai.BusinessLogic.Master;
using Jabjai.Object;
using Jabjai.Object.Entity.Jabjai;
using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jabjai_finance.Modules.Reports
{
    public partial class ReportInvoiceToExcel : System.Web.UI.Page
    {
        private MasterDataLogic<TYear> _yearLogic;
        public string YearsJson { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            _yearLogic = new MasterDataLogic<TYear>();
            BaseResult<List<TYear>> resultYears = _yearLogic.GetAll();
            YearsJson = new JavaScriptSerializer().Serialize(resultYears.Result.Where(w=>w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.YearId));

        }
    }
}