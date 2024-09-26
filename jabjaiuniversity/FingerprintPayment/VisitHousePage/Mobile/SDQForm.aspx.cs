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
    public partial class SDQForm : System.Web.UI.Page
    {
        protected string StudentPicture = "/Content/VisitHouse/assets/img/user.png";
        protected string StudentCode = "";
        protected string StudentName = "";
        protected string StudentClass = "";

        protected string SDQChooseData1 = "";
        protected string DisabledSaveButton1 = "disabled";
        protected string StyleSaveButton1 = "btn-default";

        protected string SDQChooseData2 = "";
        protected string DisabledSaveButton2 = "disabled";
        protected string StyleSaveButton2 = "btn-default";

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
            if ((client == "teacher" && (typeID == 1 || typeID == 3)) || (client == "student" && typeID == 2))
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

                int.TryParse((string)Request.QueryString["section"], out int sectionID);
                switch (sectionID)
                {
                    case 1:
                        MvContent.ActiveViewIndex = 0;
                        MvScript.ActiveViewIndex = 0;

                        // Get SDQ Data (Section 1)
                        GetSDQDataSection1(schoolID, studentID, typeID);

                        break;
                    case 2:
                        MvContent.ActiveViewIndex = 1;
                        MvScript.ActiveViewIndex = 1;

                        // Get SDQ Data (Section 2)
                        GetSDQDataSection2(schoolID, studentID, typeID);

                        break;
                }
            }
        }

        // Get SDQ Data (Section 1)
        public void GetSDQDataSection1(int schoolID, int studentID, int typeID)
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

                var groupSection1 = new int[] { 1, 2, 3, 4, 5 };
                List<TQuestionnaireSDQ> questionnaireSDQs = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolID && w.sID == studentID && w.YearID == currentYearID && w.TermID == currentTermID && w.SDQType == typeID && w.cDel == false && groupSection1.Contains((int)w.QuestionGroup)).ToList();
                foreach (var r in questionnaireSDQs)
                {
                    SDQChooseData1 += string.Format(@", {{ questionId: {0}, questionName: ""{1}"", choiceNo: {2}, choiceValue: {3}, choiceGroup: {4} }}", r.QuestionDes.Replace("Question", ""), r.QuestionDes, r.ChoiceNo ?? 0, r.QuestionScore, r.QuestionGroup);
                }
                if (!string.IsNullOrEmpty(SDQChooseData1))
                {
                    SDQChooseData1 = SDQChooseData1.Remove(0, 2);
                }

                if (questionnaireSDQs.Count == 25)
                {
                    DisabledSaveButton1 = "";
                    StyleSaveButton1 = "btn-success";
                }
            }
        }

        // Get SDQ Data (Section 2)
        public void GetSDQDataSection2(int schoolID, int studentID, int typeID)
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

                var groupSection1 = new int[] { 6 };
                TQuestionnaireSDQ question26 = null;
                List<TQuestionnaireSDQ> questionnaireSDQs = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolID && w.sID == studentID && w.YearID == currentYearID && w.TermID == currentTermID && w.SDQType == typeID && w.cDel == false && groupSection1.Contains((int)w.QuestionGroup)).ToList();
                foreach (var r in questionnaireSDQs)
                {
                    SDQChooseData2 += string.Format(@", {{ questionId: {0}, questionName: ""{1}"", choiceNo: {2}, choiceValue: {3}, choiceGroup: {4} }}", r.QuestionDes.Replace("Question", ""), r.QuestionDes, r.ChoiceNo ?? 0, r.QuestionScore, r.QuestionGroup);
                    if (r.QuestionDes == "Question26")
                    {
                        question26 = r;
                    }
                }
                if (!string.IsNullOrEmpty(SDQChooseData2))
                {
                    SDQChooseData2 = SDQChooseData2.Remove(0, 2);
                }

                if ((typeID == 1 && questionnaireSDQs.Count == 8) || (((typeID == 2 || typeID == 3) && questionnaireSDQs.Count == 6)) || question26?.ChoiceNo == 1)
                {
                    DisabledSaveButton2 = "";
                    StyleSaveButton2 = "btn-success";
                }

                if (question26?.ChoiceNo == 1 && VisibleSaveButton) {
                    DisableNavigator = "disabled";
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static object SaveSDQDataSection1(int schoolId, int studentId, int typeId, List<SDQData> sdqDatas)
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


                    List<TQuestionnaireSDQ> questionnaireSDQs = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolId && w.sID == studentId && w.YearID == currentYearID && w.TermID == currentTermID && w.SDQType == typeId && w.cDel == false).ToList();

                    foreach (var sdqData in sdqDatas)
                    {
                        TQuestionnaireSDQ questionnaireSDQ = questionnaireSDQs.Where(w => w.QuestionDes == sdqData.QuestionName).FirstOrDefault();
                        if (questionnaireSDQ == null)
                        {
                            questionnaireSDQ = new TQuestionnaireSDQ()
                            {
                                sID = studentId,
                                QuestionDes = sdqData.QuestionName,
                                ChoiceNo = sdqData.ChoiceNo,
                                QuestionScore = sdqData.ChoiceValue,
                                QuestionGroup = sdqData.ChoiceGroup,
                                SDQType = typeId,
                                SchoolID = schoolId,
                                YearID = currentYearID,
                                TermID = currentTermID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = studentId
                            };
                            en.TQuestionnaireSDQs.Add(questionnaireSDQ);
                        }
                        else
                        {
                            questionnaireSDQ.ChoiceNo = sdqData.ChoiceNo;
                            questionnaireSDQ.QuestionScore = sdqData.ChoiceValue;
                            questionnaireSDQ.QuestionGroup = sdqData.ChoiceGroup;
                            questionnaireSDQ.UpdatedDate = DateTime.Now;
                            questionnaireSDQ.UpdatedBy = studentId;
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

        [WebMethod(EnableSession = true)]
        public static object SaveSDQDataSection2(int schoolId, int studentId, int typeId, List<SDQData> sdqDatas)
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


                    List<TQuestionnaireSDQ> questionnaireSDQs = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolId && w.sID == studentId && w.YearID == currentYearID && w.TermID == currentTermID && w.SDQType == typeId && w.cDel == false).ToList();

                    foreach (var sdqData in sdqDatas)
                    {
                        TQuestionnaireSDQ questionnaireSDQ = questionnaireSDQs.Where(w => w.QuestionDes == sdqData.QuestionName).FirstOrDefault();
                        if (questionnaireSDQ == null)
                        {
                            questionnaireSDQ = new TQuestionnaireSDQ()
                            {
                                sID = studentId,
                                QuestionDes = sdqData.QuestionName,
                                ChoiceNo = sdqData.ChoiceNo,
                                QuestionScore = sdqData.ChoiceValue,
                                QuestionGroup = sdqData.ChoiceGroup,
                                SDQType = typeId,
                                SchoolID = schoolId,
                                YearID = currentYearID,
                                TermID = currentTermID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = studentId
                            };
                            en.TQuestionnaireSDQs.Add(questionnaireSDQ);
                        }
                        else
                        {
                            questionnaireSDQ.ChoiceNo = sdqData.ChoiceNo;
                            questionnaireSDQ.QuestionScore = sdqData.ChoiceValue;
                            questionnaireSDQ.QuestionGroup = sdqData.ChoiceGroup;
                            questionnaireSDQ.UpdatedDate = DateTime.Now;
                            questionnaireSDQ.UpdatedBy = studentId;
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

        public class SDQData
        {
            [JsonProperty(PropertyName = "questionId")]
            public int QuestionID { get; set; }

            [JsonProperty(PropertyName = "questionName")]
            public string QuestionName { get; set; }

            [JsonProperty(PropertyName = "choiceNo")]
            public int ChoiceNo { get; set; }

            [JsonProperty(PropertyName = "choiceValue")]
            public int ChoiceValue { get; set; }

            [JsonProperty(PropertyName = "choiceGroup")]
            public int ChoiceGroup { get; set; }
        }
    }
}