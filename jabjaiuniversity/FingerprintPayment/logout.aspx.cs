using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //HttpContext.Current.Response.Cookies["token"].Value = "";

                database.InsertLog(Session["sEmpID"] + "", "ออกจากระบบ",
                    HttpContext.Current.Session["sEntities"].ToString(), Request, 999, 1, 0);
                //fcommon.LED8("0", 2);

                //Session["sEmpID"] = "";
                //Session["permissionMenu"] = "";
                //Session["sEntities"] = "";
                //Session["SchoolDBConnectionString"] = ""; 
                //Session["token"] = "";
                //HttpContext.Current.Request.Cookies.Clear();
                //HttpContext.Current.Request.Cookies["token"].Value = null;
                HttpContext.Current.Session.Clear();
            }
            catch
            {
            }
            Response.Redirect("Default.aspx");

        }
    }
}