using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class leaveSetting_checker : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
          

            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                SubmitButton.Click += new EventHandler(Button1_Click);
                BackButton.Click += new EventHandler(Button2_Click);
                string sEntities = Session["sEntities"] + "";
                var companydata = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                if (!IsPostBack)
                {
                    int sEmpID = int.Parse(Session["sEmpID"] + "");
                    int? index = 1;
                    var company = _dbMaster.TCompanies.Where(w => w.nCompany == companydata.nCompany).FirstOrDefault();
                    index = company.checker;


                    if (index == 1)
                        ddlAmount.SelectedIndex = 0;
                    else if (index == 2)
                        ddlAmount.SelectedIndex = 1;
                    else ddlAmount.SelectedIndex = 2;
                }

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string sEntities = Session["sEntities"] + "";
                var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                var companydata = _dbMaster.TCompanies.Where(w => w.nCompany == nCompany.nCompany).FirstOrDefault();
                int y = 0;
                Int32.TryParse(ddlAmount.SelectedValue, out y);
                companydata.checker = y;

                _dbMaster.SaveChanges();
                Response.Redirect("leaveSetting.aspx");
            }

        }
        private void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("leaveSetting.aspx");
        }
    }
}