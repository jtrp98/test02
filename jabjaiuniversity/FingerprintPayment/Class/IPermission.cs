using JabjaiMainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
{
    //public interface IPermission
    //{
    //    PermissionType Permission { get; set; }
    //    int MenuID { get; }
    //}

    //public static class PermissionHelper
    //{

    //}

    public abstract class BasePagePermission : System.Web.UI.Page
    {
        //protected PermissionType Permission { get; set; }
        //protected int MenuID { get; }

        protected JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();

            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            //Permission = PermissionHelper.GetPermissionOnWEB(MenuID, UserData.UserID, UserData.CompanyID);

            //if (PermissionType.NoPermission == Permission)
            //{
            //    Response.Redirect("~/NoPermission.aspx");
            //}

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }
    }   
}