using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.StudentCall
{
    public class BaseStudentCall : System.Web.UI.Page
    {
        //protected override int MenuID => throw new NotImplementedException();

        //protected override int MenuID => throw new NotImplementedException();
           
        private JWTToken.userData userData;
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

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        //protected bool IsSuperUser()
        //{
        //    using (var ctx = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        var company = ctx.TCompanies
        //            .Where(o => o.nCompany == userData.CompanyID)
        //            .FirstOrDefault();

        //        var userId = userData.UserID;

        //        return (
        //            company.nSchoolHeadid == userId ||
        //            company.nAcademicDirectorid == userId ||
        //            company.nRegistraDirectorid == userId ||
        //            company.nAcademicSubDirectorid == userId ||
        //            company.nAccountingDirectorid == userId ||
        //            company.nStudentDevelopmentDirectorid == userId ||
        //            company.nWebAdminid == userId ||
        //            company.nGM == userId ||
        //            company.nPersonnel == userId
        //            );
        //        //if(company.nAcademicDirectorid == )
        //    }
        //}

        protected int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        public static JWTToken.userData GetUserData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Default.aspx");
            }

            return userData;
        }
    }


}