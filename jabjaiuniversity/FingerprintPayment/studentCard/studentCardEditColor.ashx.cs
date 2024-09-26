using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Web.DynamicData;
using System.Drawing;
using System.IO;

namespace FingerprintPayment.studentCard
{
    /// <summary>
    /// Summary description for studentCardEditColor
    /// </summary>
    public class studentCardEditColor : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                string ColorValue = fcommon.ReplaceInjection(context.Request["ColorValue"]);

                TStudentCardInfo card = new TStudentCardInfo();

                var check = _db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "FontColor" && w.cDel == null).FirstOrDefault();
                if (check == null)
                {
                    card = new TStudentCardInfo();
                    card.cDel = null;
                    card.elementName = "FontColor";
                    card.elementValue = ColorValue;
                    card.SchoolID = userData.CompanyID;

                    _db.TStudentCardInfoes.Add(card);
                }
                else
                {
                    check.elementValue = ColorValue;
                }

                _db.SaveChanges();

            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}