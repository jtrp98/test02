using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace FingerprintPayment.studentCard.ashx
{
    /// <summary>
    /// Summary description for funcShowBrithday
    /// </summary>
    public class studentCardProp : IHttpHandler , IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            var value = fcommon.ReplaceInjection(context.Request["value"]);
            var prop = fcommon.ReplaceInjection(context.Request["prop"]);

            

            using (var db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                //TStudentCardInfo card = new TStudentCardInfo();

                var card = db.TStudentCardInfoes
                    .Where(w => w.SchoolID == userData.CompanyID && w.elementName == prop)
                    .FirstOrDefault();
                DateTime date;

                if (card == null)
                {
                    card = new TStudentCardInfo();

                    if(DateTime.TryParseExact(value, "dd/MM/yyyy", new CultureInfo("th-TH") , DateTimeStyles.None,out date))
                    {
                        card.date = date;
                    }
                    else
                    {
                        card.elementValue = value;
                    }
                                       
                    card.elementName = prop;
                    card.CreatedBy = userData.UserID;
                    card.CreatedDate = DateTime.Now;
                    card.SchoolID = userData.CompanyID;

                    db.TStudentCardInfoes.Add(card);
                }
                else
                {
                    if (DateTime.TryParseExact(value, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out date))
                    {
                        card.date = date;
                    }
                    else
                    {
                        card.elementValue = value;
                    }

                    card.UpdatedBy = userData.UserID;
                    card.UpdatedDate = DateTime.Now;
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