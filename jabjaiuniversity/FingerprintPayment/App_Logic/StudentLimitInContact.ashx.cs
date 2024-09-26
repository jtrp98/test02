using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for StudentLimitInContact
    /// </summary>
    public class StudentLimitInContact : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "application/json";

            bool success = true;
            string message = "Load data successfully.";
            var data = new { limitInContact = 0, currentNumber = 0, remainingNumber = 0, excessNumber = 0 };

            try
            {
                int schoolID = userData.CompanyID;

                using (JabJaiMasterEntities em = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    StudentLogic studentLogic = new StudentLogic(en);
                    string currentTermID = studentLogic.GetTermId(userData);

                    // Count all student study current term
                    string query = string.Format(@"
SELECT COUNT(*)
FROM TB_StudentViews 
WHERE SchoolID={0} AND ISNULL(cDel, '0')='0' AND nTerm='{1}' 
AND ISNULL(nStudentStatus, 0)=0 AND ISNULL(moveInDate, CONVERT(DATE, GETDATE())) <= CONVERT(DATE, GETDATE())
AND sID IN (SELECT sID FROM JabjaiMasterSingleDB.dbo.TUser WHERE nCompany={0} AND cType = '0')", schoolID, currentTermID);
                    var currentNumber = en.Database.SqlQuery<int>(query).FirstOrDefault();

                    // Get limit amount student on contact
                    var limitInContact = 0;
                    TContact contact = em.TContact.Where(w => w.SchoolID == schoolID && w.IsDelete == false).FirstOrDefault();
                    if (contact != null && contact.StudentCount != null)
                    {
                        limitInContact = (int)contact.StudentCount;
                    }

                    var remainingNumber = limitInContact - currentNumber;
                    var excessNumber = remainingNumber < 0 ? Math.Abs(remainingNumber) : 0;
                    remainingNumber = remainingNumber < 0 ? 0 : remainingNumber;

                    data = new { limitInContact, currentNumber, remainingNumber, excessNumber };
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, data };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }
}