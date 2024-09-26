using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiEntity.DB;
using JabjaiMainClass;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using FingerprintPayment.Qusetion.CsCode;
using FingerprintPayment.Class;


namespace FingerprintPayment.Qusetion
{
    public partial class Question_SDQ : StudentGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string SaveItem(int sID, List<TQuestionnaireSDQ> dSDQs)
        {
            var schoolID = GetUserData().CompanyID;
            var MessageAlert = "";

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                //List<anwerPoint> anwerPoints = new List<anwerPoint>();
                //List<TQuestionnaireSDQ> TQuestionnaireSDQ = new List<TQuestionnaireSDQ>();

                try
                {
                    var cHeck = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolID && w.sID == sID).FirstOrDefault();
                    if (cHeck != null)
                    {
                        MessageAlert = "havedata " + sID.ToString();
                    }
                    else
                    {
                        foreach (var andwer in dSDQs)
                        {
                            TQuestionnaireSDQ questionnaireSDQ = new TQuestionnaireSDQ()
                            {
                                sID = sID,
                                QuestionDes = andwer.QuestionDes,
                                QuestionScore = andwer.QuestionScore,
                                QuestionGroup = andwer.QuestionGroup,
                                SchoolID = schoolID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = sID
                            };
                            en.TQuestionnaireSDQs.Add(questionnaireSDQ);
                        }
                        en.SaveChanges();

                        MessageAlert = "success " + sID.ToString();
                    }
                }
                catch (Exception err)
                {
                    MessageAlert = "error- " + err.Message + " :line " + ComFunction.GetLineNumberError(err);
                }


            }

            return MessageAlert;
        }


    }
}