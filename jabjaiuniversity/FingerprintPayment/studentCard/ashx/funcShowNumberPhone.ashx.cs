using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;

namespace FingerprintPayment.studentCard.ashx
{
    /// <summary>
    /// Summary description for funcShowNumberPhone
    /// </summary>
    public class funcShowNumberPhone : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            var Value = fcommon.ReplaceInjection(context.Request["NumberPhone"]);
            var Name = fcommon.ReplaceInjection(context.Request.QueryString.AllKeys[0]);

            JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));
            TStudentCardInfo card = new TStudentCardInfo();

            var check = db.TStudentCardInfoes.Where(w => w.SchoolID == userData.CompanyID && w.elementName == Name).FirstOrDefault();
            if (check == null)
            {
                card = new TStudentCardInfo();

                card.elementValue = Value;
                card.elementName = Name;
                card.CreatedBy = userData.UserID;
                card.CreatedDate = DateTime.Now;
                card.SchoolID = userData.CompanyID;

                db.TStudentCardInfoes.Add(card);
            }
            else
            {
                check.elementValue = Value;
                check.UpdatedBy = userData.UserID;
                check.UpdatedDate = DateTime.Now;
            }
            db.SaveChanges();
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