using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for WSDataService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    [System.Web.Script.Services.ScriptService()]
    public class WSDataService : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private JWTToken.userData userData = new JWTToken.userData();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<State> GetLsitPlane()
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            List<State> sbStates = new List<State>();

            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    foreach (var xnl in _db.TPlanes.Where(w => w.SchoolID == userData.CompanyID).ToList())
                    {
                        State st = new State();
                        st.Name = xnl.sPlaneName;
                        st.Abbreviation = xnl.sPlaneID.ToString();
                        st.value = xnl.sPlaneID.ToString();
                        sbStates.Add(st);
                    }
                }
            }
            catch (Exception ex)
            {
                string exp = ex.ToString();	//Setup a breakpoint here 
                //to verify any exceptions raised.
            }
            return sbStates;
        }
        public class State
        {
            string _name;
            string _value;

            public string value
            {
                get { return _value; }
                set { _value = value; }
            }

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }
            string _abbr;

            public string Abbreviation
            {
                get { return _abbr; }
                set { _abbr = value; }
            }
        }

    }
}
