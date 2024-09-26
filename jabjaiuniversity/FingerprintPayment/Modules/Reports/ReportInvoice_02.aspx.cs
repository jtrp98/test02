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
    public partial class ReportInvoice_02 : System.Web.UI.Page
    {
        public string YearsJson { get; set; }
        public string ProductJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            Jabjai.DataAccess.JabjaiContext.ConnectionString = database.stringConntionDatabase(Connection.MasterEntities(ConnectionDB.Read), Session["sEntities"].ToString());
            var _yearLogic = new YearLogic();
            BaseResult<List<TYear>> resultYears = _yearLogic.GetSchoolId(userData.CompanyID);
            YearsJson = new JavaScriptSerializer().Serialize(resultYears.Result.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.YearId));
            using (PeakengineEntities entities = Connection.PeakengineEntities(ConnectionDB.Read))
            {
                var q = (from a in entities.Products.Where(w => w.school_id == userData.CompanyID)
                         where (a.Del ?? false) == false
                         orderby a.nPaymentID
                         select new
                         {
                             a.sPayment,
                             a.nPaymentID
                         }).ToList();

                ProductJson = new JavaScriptSerializer().Serialize(q);
            }
        }


    }
}