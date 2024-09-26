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
using FingerprintPayment.Helper;


namespace FingerprintPayment.TeacherCard.Ashx
{
    /// <summary>
    /// Summary description for EditTeacherCardType
    /// </summary>
    public class EditTeacherCardType : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

                dynamic rss = new JObject();

                var CardTypeValue = context.Request.QueryString["CardType"];
                TTeacherCardInfo CardTeacher = new TTeacherCardInfo();


                var check = _db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.elementName == "CardType" && w.cDel == null).FirstOrDefault();

                if (check == null)
                {
                    CardTeacher = new TTeacherCardInfo();
                    CardTeacher.elementValue = CardTypeValue;
                    CardTeacher.elementName = "CardType";
                    CardTeacher.cDel = null;
                    CardTeacher.SchoolID = userData.CompanyID;
                    CardTeacher.CreatedDate = DateTime.Now;

                    _db.TTeacherCardInfoes.Add(CardTeacher);
                    _db.SaveChanges();
                }
                else
                {
                    check.elementValue = CardTypeValue;
                    check.UpdatedDate = DateTime.Now;

                    _db.SaveChanges();
                }
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