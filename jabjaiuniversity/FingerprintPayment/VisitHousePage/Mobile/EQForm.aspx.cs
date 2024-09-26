using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Mobile
{
    public partial class EQForm : System.Web.UI.Page
    {
        protected string StudentPicture = "/Content/VisitHouse/assets/img/user.png";
        protected string StudentCode = "";
        protected string StudentName = "";
        protected string StudentClass = "";

        protected string EQChooseData = "";
        protected string DisabledSaveButton = "disabled";
        protected string StyleSaveButton = "btn-default";

        protected bool VisibleSaveButton = true;
        protected bool QuestionsForStudent = true;
        protected int UserType = 1;
        protected string DisableNavigator = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            int.TryParse((string)Request.QueryString["schoolid"], out int schoolID);
            int.TryParse((string)Request.QueryString["sid"], out int studentID);
            int.TryParse((string)Request.QueryString["type"], out int typeID);

            UserType = typeID;
            string client = (string)Request.QueryString["client"];

            // Show / Hide save button
            if (client == "teacher")
            {
                VisibleSaveButton = false;
            }

            // Questions for student(1) or teacher(2) and parents(3)
            if (typeID == 2 || typeID == 3)
            {
                QuestionsForStudent = false;
            }

            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                StudentLogic studentLogic = new StudentLogic(dbSchool);
                string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                string query = string.Format(@"
SELECT u.sStudentID 'StudentID', u.sName 'Name', u.sLastname 'Lastname', u.sStudentPicture 'StudentPicture', sv.SubLevel 'Level', sv.nTSubLevel2 'Classroom'
FROM TUser u LEFT JOIN TB_StudentViews sv ON u.SchoolID = sv.SchoolID AND u.sID = sv.sID 
WHERE u.SchoolID = {0} AND u.sID = {1} AND sv.nTerm = '{2}'", schoolID, studentID, currentTermID);
                var studentData = dbSchool.Database.SqlQuery<StudentData>(query).FirstOrDefault();
                if (studentData != null)
                {
                    if (!string.IsNullOrEmpty(studentData.StudentPicture)) StudentPicture = studentData.StudentPicture;

                    StudentCode = studentData.StudentID;
                    StudentName = studentData.Name + " " + studentData.Lastname;
                    var schoolData = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                    if (schoolData.ClassNameDisable ?? false)
                    {
                        // Hide room
                        StudentClass = studentData.Level;
                    }
                    else
                    {
                        StudentClass = studentData.Level + " / " + studentData.Classroom;
                    }
                }

                GetEQData(schoolID, studentID, typeID);
            }
        }

        public void GetEQData(int schoolID, int studentID, int typeID)
        {
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                int? currentYearID = null;
                var termObj = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                if (termObj != null)
                {
                    currentYearID = termObj.nYear;
                }

                List<TQuestionnaireEQ> questionnaireEQs = en.TQuestionnaireEQ.Where(w => w.SchoolID == schoolID && w.sID == studentID && w.YearID == currentYearID && w.TermID == currentTermID && w.EQType == typeID && w.cDel == false).ToList();
                foreach (var r in questionnaireEQs)
                {
                    EQChooseData += string.Format(@", {{ questionId: {0}, questionName: ""{1}"", choiceNo: {2}, choiceValue: {3}, choiceSmallGroup: {4}, choiceLargeGroup: {5} }}", r.QuestionDes.Replace("Question", ""), r.QuestionDes, r.ChoiceNo ?? 0, r.QuestionScore, r.QuestionSmallGroup, r.QuestionLargeGroup);
                }
                if (!string.IsNullOrEmpty(EQChooseData))
                {
                    EQChooseData = EQChooseData.Remove(0, 2);
                }

                if (questionnaireEQs.Count == 52)
                {
                    DisabledSaveButton = "";
                    StyleSaveButton = "btn-success";
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SaveEQData(int schoolId, int studentId, int typeId, List<EQData> eqDatas)
        {
            bool success = true;
            string message = "บันทึกข้อมูลสำเร็จ";

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                try
                {
                    StudentLogic studentLogic = new StudentLogic(en);
                    string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolId });

                    int? currentYearID = null;
                    var termObj = en.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                    if (termObj != null)
                    {
                        currentYearID = termObj.nYear;
                    }


                    List<TQuestionnaireEQ> questionnaireEQs = en.TQuestionnaireEQ.Where(w => w.SchoolID == schoolId && w.sID == studentId && w.YearID == currentYearID && w.TermID == currentTermID && w.EQType == typeId && w.cDel == false).ToList();

                    foreach (var eqData in eqDatas)
                    {
                        TQuestionnaireEQ questionnaireEQ = questionnaireEQs.Where(w => w.QuestionDes == eqData.QuestionName).FirstOrDefault();
                        if (questionnaireEQ == null)
                        {
                            questionnaireEQ = new TQuestionnaireEQ()
                            {
                                sID = studentId,
                                QuestionDes = eqData.QuestionName,
                                ChoiceNo = eqData.ChoiceNo,
                                QuestionScore = eqData.ChoiceValue,
                                QuestionSmallGroup = eqData.ChoiceSmallGroup,
                                QuestionLargeGroup = eqData.ChoiceLargeGroup,
                                EQType = typeId,
                                SchoolID = schoolId,
                                YearID = currentYearID,
                                TermID = currentTermID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = studentId
                            };
                            en.TQuestionnaireEQ.Add(questionnaireEQ);
                        }
                        else
                        {
                            questionnaireEQ.ChoiceNo = eqData.ChoiceNo;
                            questionnaireEQ.QuestionScore = eqData.ChoiceValue;
                            questionnaireEQ.QuestionSmallGroup = eqData.ChoiceSmallGroup;
                            questionnaireEQ.QuestionSmallGroup = eqData.ChoiceSmallGroup;
                            questionnaireEQ.UpdatedDate = DateTime.Now;
                            questionnaireEQ.UpdatedBy = studentId;
                        }
                        en.SaveChanges();
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;
                }
            }

            return JsonConvert.SerializeObject(new { success, message });
        }

        public class StudentData
        {
            public string StudentID { get; set; }
            public string Name { get; set; }
            public string Lastname { get; set; }
            public string StudentPicture { get; set; }
            public string Level { get; set; }
            public string Classroom { get; set; }
        }

        public class EQData
        {
            [JsonProperty(PropertyName = "questionId")]
            public int QuestionID { get; set; }

            [JsonProperty(PropertyName = "questionName")]
            public string QuestionName { get; set; }

            [JsonProperty(PropertyName = "choiceNo")]
            public int ChoiceNo { get; set; }

            [JsonProperty(PropertyName = "choiceValue")]
            public int ChoiceValue { get; set; }

            [JsonProperty(PropertyName = "choiceSmallGroup")]
            public int ChoiceSmallGroup { get; set; }

            [JsonProperty(PropertyName = "choiceLargeGroup")]
            public int ChoiceLargeGroup { get; set; }
        }
    }
}