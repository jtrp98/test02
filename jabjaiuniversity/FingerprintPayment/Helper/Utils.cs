using JabjaiMainClass;
using System;
using System.Web;

namespace FingerprintPayment.Helper
{
    public static class Utils
    {
        public static int GetSchoolId()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["nCompany"] != null)
            {

                return Convert.ToInt32(HttpContext.Current.Session["nCompany"].ToString());
            }
            else if (HttpContext.Current != null)
            {
                JWTToken token = new JWTToken();
                return (token.CheckToken(HttpContext.Current)) ? Convert.ToInt32(HttpContext.Current.Session["nCompany"].ToString()) : 0;
            }
            else
            {
                return 0;
            }
        }

        public static int GetUserId()
        {

            if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["sEmpID"] != null)
            {

                return Convert.ToInt32(HttpContext.Current.Session["sEmpID"].ToString());
            }
            else if (HttpContext.Current != null)
            {
                JWTToken token = new JWTToken();
                return (token.CheckToken(HttpContext.Current)) ? Convert.ToInt32(HttpContext.Current.Session["sEmpID"].ToString()) : 0;
            }
            else
            {
                return 0;
            }
        }


    }


}