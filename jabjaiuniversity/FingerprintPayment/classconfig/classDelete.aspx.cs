using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using System.Globalization;
using System.Web.DynamicData;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;

namespace FingerprintPayment.classconfig
{
    public partial class classDelete : System.Web.UI.Page
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
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                
                string type = Request.QueryString["type"];
                string id = Request.QueryString["id"];
                string sEntities = Session["sEntities"] + "";
                int idn = int.Parse(id);

                if (type == "sublevel")
                {
                    var data1 = _db.TSubLevels.Where(w => w.nTSubLevel == idn).FirstOrDefault();
                    data1.nDeleted = 1;
                }
                if (type == "termsublevel")
                {
                    var data2 = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == idn).FirstOrDefault();
                    data2.nTermSubLevel2Status = "0";
                }


                _db.SaveChanges();
                Response.Redirect("classSetting.aspx");
            }
        }
    }
}