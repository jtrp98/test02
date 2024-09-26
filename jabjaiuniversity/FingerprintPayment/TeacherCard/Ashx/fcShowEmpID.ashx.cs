using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;


namespace FingerprintPayment.TeacherCard.Ashx
{
    /// <summary>
    /// Summary description for fcShowEmpID
    /// </summary>
    public class fcShowEmpID : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var Value = fcommon.ReplaceInjection(context.Request["ShowEmpID"]);
            var ShowEmpID = fcommon.ReplaceInjection(context.Request.QueryString.AllKeys[0]);

            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                TTeacherCardInfo card = new TTeacherCardInfo();

                var check = db.TTeacherCardInfoes.Where(w => w.SchoolID == userData.CompanyID && w.elementName == ShowEmpID).FirstOrDefault();
                if (check == null)
                {
                    card = new TTeacherCardInfo();

                    card.elementValue = Value;
                    card.elementName = ShowEmpID;
                    card.CreatedBy = userData.UserID;
                    card.CreatedDate = DateTime.Now;
                    card.SchoolID = userData.CompanyID;

                    db.TTeacherCardInfoes.Add(card);
                }
                else
                {
                    check.elementValue = Value;
                    check.UpdatedBy = userData.UserID;
                    check.UpdatedDate = DateTime.Now;
                }
                db.SaveChanges();
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