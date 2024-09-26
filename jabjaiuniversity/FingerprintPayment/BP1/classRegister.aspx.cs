using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Modules.TimeAttendance;
using JabjaiEntity.DB;
using MasterEntity;

namespace FingerprintPayment.BP1
{
    public partial class classRegister : System.Web.UI.Page
    {
        public static string GetPermission()
        {
            return (String)HttpContext.Current.Session["sEntities"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                if (!IsPostBack)
                {
                    Opendata();

                }
            }
        }



        private void Opendata()
        {
            using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

            }



        }
    }
}