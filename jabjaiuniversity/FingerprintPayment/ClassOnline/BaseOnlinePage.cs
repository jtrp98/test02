using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace FingerprintPayment.ClassOnline
{
    public class BaseOnlinePage : System.Web.UI.Page
    {
        // protected override int MenuID => PermissionHelper.MENUID_CLASSONLINE;

        //public PermissionType Permission { get; set; }
        //public int MenuID { get { return PermissionHelper.MENUID_CLASSONLINE; } }

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

        protected bool IsSuperUser()
        {
            using (var ctx = Connection.MasterEntities(ConnectionDB.Read))
            {
                var company = ctx.TCompanies
                    .Where(o => o.nCompany == userData.CompanyID)
                    .FirstOrDefault();

                var userId = userData.UserID;

                return (
                    userId == 0 ||
                    company.nSchoolHeadid == userId ||
                    company.nAcademicDirectorid == userId ||
                    company.nRegistraDirectorid == userId ||
                    company.nAcademicSubDirectorid == userId ||
                    company.nAccountingDirectorid == userId ||
                    company.nStudentDevelopmentDirectorid == userId ||
                    company.nWebAdminid == userId ||
                    company.nGM == userId ||
                    company.nPersonnel == userId
                );
                //if(company.nAcademicDirectorid == )
            }
        }

        protected int? ToNullableInt(string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }

        //public static JWTToken.userData GetUserData()
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken.userData();
        //    if (token.CheckToken(HttpContext.Current))
        //    {
        //        userData = token.getTokenValues(HttpContext.Current);
        //    }
        //    else
        //    {
        //        HttpContext.Current.Response.Redirect("~/Default.aspx");
        //    }

        //    return userData;
        //}

        public bool ValidHttpURL(string s, out Uri resultURI)
        {
            //Uri.IsWellFormedUriString
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "http://" + s;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultURI))
                return (resultURI.Scheme == Uri.UriSchemeHttp ||
                        resultURI.Scheme == Uri.UriSchemeHttps);

            return false;
        }

        public T1 CloneObject<T1, T2>(T2 source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public List<T1> CloneObject<T1, T2>(List<T2> source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<List<T1>>(JsonConvert.SerializeObject(source), deserializeSettings);
        }
    }


}