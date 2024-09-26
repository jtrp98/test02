using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiEntity.DB;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Qusetion.CsCode;
using FingerprintPayment.Class;


namespace FingerprintPayment.Qusetion
{
    public partial class Question_EQ : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string SaveItem(int sID, List<TQuestionnaireEQ> questionnaireEQs)
        {
            var schoolID = GetUserData().CompanyID;
            var MessageAlert = "";

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                var cHeck = _db.TQuestionnaireEQ.Where(w => w.SchoolID == schoolID && w.sID == sID).FirstOrDefault();
                if (cHeck != null)
                {
                    MessageAlert = "Available in the system";
                }
                else
                {
                    foreach (var answer in questionnaireEQs)
                    {
                        TQuestionnaireEQ eQ = new TQuestionnaireEQ
                        {
                            sID = sID,
                            QuestionDes = answer.QuestionDes,
                            QuestionScore = answer.QuestionScore,
                            QuestionSmallGroup = answer.QuestionSmallGroup,
                            QuestionLargeGroup = answer.QuestionLargeGroup,
                            SchoolID = schoolID,
                            CreatedDate = DateTime.Now,
                            CreatedBy = sID
                        };
                        _db.TQuestionnaireEQ.Add(eQ);
                    }
                    _db.SaveChanges();

                    MessageAlert = "Added Successfully";
                }

                return MessageAlert;
            }

        }

    }
}