using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class plans_term : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities  _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object ValidatePlanCourseDeActivation(int nTermSubLevel2, string nTerm)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var planCourses = schoolDbContext.GetPlanCourse(0, nTermSubLevel2, nTerm, 0, userData.CompanyID);
                    if (planCourses != null && planCourses.ToList().Count() > 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
               
                string parameters = string.Format("nTermSubLevel2:{0},nTerm:{1}", nTermSubLevel2, nTerm);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "ValidatePlanCourseDeActivation", parameters, "", null);
                return true;
            }
        }
    }
}