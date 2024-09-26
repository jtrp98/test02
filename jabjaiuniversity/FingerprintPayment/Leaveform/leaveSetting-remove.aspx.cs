using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment
{
    public partial class leaveSetting_remove : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["permission"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            int id = 0;
            Int32.TryParse(Request.QueryString["id"], out id);
            string sEntities = Session["sEntities"] + "";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            var userdata = _db.TEmployees.Where(w => w.sEmp == id).FirstOrDefault();
            userdata.leavecheck = 0;
            
            _db.SaveChanges();
            Response.Redirect("leaveSetting.aspx");
        }
    }
}