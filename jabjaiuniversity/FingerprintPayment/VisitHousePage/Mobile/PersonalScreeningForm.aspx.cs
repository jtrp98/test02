using FingerprintPayment.Helper;
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
    public partial class PersonalScreeningForm : System.Web.UI.Page
    {
        protected string StudentPicture = "/Content/VisitHouse/assets/img/user.png";
        protected string StudentCode = "";
        protected string StudentName = "";
        protected string StudentClass = "";

        protected bool VisibleSaveButton = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            int.TryParse((string)Request.QueryString["schoolid"], out int schoolID);
            int.TryParse((string)Request.QueryString["sid"], out int studentID);

            string client = (string)Request.QueryString["client"];

            // Show / Hide save button
            if (client != "teacher")
            {
                VisibleSaveButton = false;
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
            }
        }

        [WebMethod(EnableSession = true)]
        public static object GetScreeningData(int schoolID, int studentID)
        {
            bool success = true;
            string message = "Save Successfully";

            ScreeningData data = new ScreeningData();

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(dbSchool);
                        string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                        int? currentYearID = null;
                        var termObj = dbSchool.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        if (termObj != null)
                        {
                            currentYearID = termObj.nYear;
                        }

                        data = dbSchool.TQuestionnaireScreenings.Where(w => w.SchoolID == schoolID && w.StudentID == studentID)
                            .Select(s => new ScreeningData
                            {
                                CreatedDate = s.CreatedDate,
                                UpdatedDate = s.UpdatedDate,

                                // 1. ด้านการเรียน
                                Academic = s.Academic,
                                AcademicRisk = s.AcademicRisk,
                                AcademicRiskNote = s.AcademicRiskNote,
                                AcademicProblem = s.AcademicProblem,
                                AcademicProblemNote = s.AcademicProblemNote,

                                // 2. ด้านความสามารถอื่นๆ
                                Abilities = s.Abilities,
                                AbilitiesNote = s.AbilitiesNote,

                                // 3. ด้านสุขภาพ
                                Health = s.Health,
                                HealthRisk = s.HealthRisk,
                                HealthRiskNote = s.HealthRiskNote,
                                HealthProblem = s.HealthProblem,
                                HealthProblemNote = s.HealthProblemNote,

                                // 4. ด้านสุขภาพจิตและพฤติกรรม
                                MentalHealthBehavior1 = s.MentalHealthBehavior1,
                                MentalHealthBehavior2 = s.MentalHealthBehavior2,
                                MentalHealthBehavior3 = s.MentalHealthBehavior3,
                                MentalHealthBehavior4 = s.MentalHealthBehavior4,
                                MentalHealthBehavior5 = s.MentalHealthBehavior5,

                                // 5. ด้านเศรษฐกิจ
                                Economic = s.Economic,
                                EconomicRisk = s.EconomicRisk,
                                EconomicRiskMoney = s.EconomicRiskMoney,
                                EconomicRiskNote = s.EconomicRiskNote,
                                EconomicProblem = s.EconomicProblem,
                                EconomicProblemNote = s.EconomicProblemNote,

                                // 6. ด้านการคุ้มครองนักเรียน
                                Protection = s.Protection,
                                ProtectionRisk = s.ProtectionRisk,
                                ProtectionProblem = s.ProtectionProblem,
                                ProtectionProblemNote = s.ProtectionProblemNote,

                                // 7. ด้านการใช้สารเสพติด
                                SubstanceAbuse = s.SubstanceAbuse,

                                // 8. ด้านเพศสัมพันธ์
                                Sex = s.Sex,

                                // 9. ด้านอื่นๆ
                                OtherSide = s.OtherSide,
                                OtherSideRiskNote = s.OtherSideRiskNote,
                                OtherSideProblemNote = s.OtherSideProblemNote

                            })
                            .FirstOrDefault();

                        // Get result data from SDQ Form
                        var query = $@"SELECT sID
, CASE WHEN CountGroup1 > 0 THEN (CASE WHEN 0 <= ScoreGroup1 AND ScoreGroup1 <=  3 THEN 1 WHEN ScoreGroup1 = 4 THEN 2 ELSE 3 END) ELSE NULL END 'ResultGroup1'
, CASE WHEN CountGroup2 > 0 THEN (CASE WHEN 0 <= ScoreGroup2 AND ScoreGroup2 <=  3 THEN 1 WHEN ScoreGroup2 = 4 THEN 2 ELSE 3 END) ELSE NULL END 'ResultGroup2'
, CASE WHEN CountGroup3 > 0 THEN (CASE WHEN 0 <= ScoreGroup3 AND ScoreGroup3 <=  5 THEN 1 WHEN ScoreGroup3 = 6 THEN 2 ELSE 3 END) ELSE NULL END 'ResultGroup3'
, CASE WHEN CountGroup4 > 0 THEN (CASE WHEN 0 <= ScoreGroup4 AND ScoreGroup4 <=  5 THEN 1 WHEN ScoreGroup4 = 6 THEN 2 ELSE 3 END) ELSE NULL END 'ResultGroup4'
, CASE WHEN CountGroup5 > 0 THEN (CASE WHEN 4 <= ScoreGroup5 AND ScoreGroup5 <= 10 THEN 1 WHEN ScoreGroup5 = 3 THEN 2 ELSE 3 END) ELSE NULL END 'ResultGroup5'
FROM
(
	SELECT sID
	, SUM(CASE WHEN QuestionGroup=1 THEN QuestionScore ELSE 0 END) 'ScoreGroup1'
	, SUM(CASE WHEN QuestionGroup=2 THEN QuestionScore ELSE 0 END) 'ScoreGroup2'
	, SUM(CASE WHEN QuestionGroup=3 THEN QuestionScore ELSE 0 END) 'ScoreGroup3'
	, SUM(CASE WHEN QuestionGroup=4 THEN QuestionScore ELSE 0 END) 'ScoreGroup4'
	, SUM(CASE WHEN QuestionGroup=5 THEN QuestionScore ELSE 0 END) 'ScoreGroup5'
	, SUM(CASE WHEN QuestionGroup=1 THEN 1 ELSE 0 END) 'CountGroup1'
	, SUM(CASE WHEN QuestionGroup=2 THEN 1 ELSE 0 END) 'CountGroup2'
	, SUM(CASE WHEN QuestionGroup=3 THEN 1 ELSE 0 END) 'CountGroup3'
	, SUM(CASE WHEN QuestionGroup=4 THEN 1 ELSE 0 END) 'CountGroup4'
	, SUM(CASE WHEN QuestionGroup=5 THEN 1 ELSE 0 END) 'CountGroup5'
	FROM TQuestionnaireSDQ 
	WHERE SchoolID={schoolID} AND sID={studentID} AND YearID={currentYearID} AND TermID='{currentTermID}' AND SDQType=2
	GROUP BY sID
) A";
                        var sdqResults = dbSchool.Database.SqlQuery<SDQResult>(query).FirstOrDefault();
                        if (sdqResults != null)
                        {
                            if (data == null) data = new ScreeningData();

                            if (sdqResults.ResultGroup1 != null) data.MentalHealthBehavior1 = sdqResults.ResultGroup1;
                            if (sdqResults.ResultGroup2 != null) data.MentalHealthBehavior2 = sdqResults.ResultGroup2;
                            if (sdqResults.ResultGroup3 != null) data.MentalHealthBehavior3 = sdqResults.ResultGroup3;
                            if (sdqResults.ResultGroup4 != null) data.MentalHealthBehavior4 = sdqResults.ResultGroup4;
                            if (sdqResults.ResultGroup5 != null) data.MentalHealthBehavior5 = sdqResults.ResultGroup5;
                        }

                        if (data != null)
                        {
                            data.SaveDate = data.CreatedDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                            data.UpdateDate = data.UpdatedDate?.ToString("d MMMM yyyy", new CultureInfo("th-TH"));
                        }
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            return JsonConvert.SerializeObject(new { success, message, data });
        }

        [WebMethod(EnableSession = true)]
        public static object SaveScreeningData(int schoolId, int studentId, ScreeningData screeningData)
        {
            bool success = true;
            string message = "บันทึกข้อมูลสำเร็จ";

            // Ignore save for student
            string client = (string)HttpContext.Current.Request.QueryString["client"];
            if (client == "student")
            {
                return JsonConvert.SerializeObject(new { success, message });
            }

            try
            {
                using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
                    {
                        StudentLogic studentLogic = new StudentLogic(dbSchool);
                        string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolId });

                        int? currentYearID = null;
                        var termObj = dbSchool.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                        if (termObj != null)
                        {
                            currentYearID = termObj.nYear;
                        }

                        // Double check complete status
                        if ((screeningData.Academic == null || (screeningData.Academic == 2 && screeningData.AcademicRisk == "[]") || (screeningData.Academic == 3 && screeningData.AcademicProblem == "[]"))
                            || screeningData.Abilities == null
                            || (screeningData.Health == null || (screeningData.Health == 2 && screeningData.HealthRisk == "[]") || (screeningData.Health == 3 && screeningData.HealthProblem == "[]"))
                            || (screeningData.MentalHealthBehavior1 == null || screeningData.MentalHealthBehavior2 == null || screeningData.MentalHealthBehavior3 == null || screeningData.MentalHealthBehavior4 == null || screeningData.MentalHealthBehavior5 == null)
                            || (screeningData.Economic == null || (screeningData.Economic == 2 && screeningData.EconomicRisk == "[]") || (screeningData.Economic == 3 && screeningData.EconomicProblem == "[]"))
                            || (screeningData.Protection == null || (screeningData.Protection == 2 && screeningData.ProtectionRisk == "[]") || (screeningData.Protection == 3 && screeningData.ProtectionProblem == "[]"))
                            || screeningData.SubstanceAbuse == null
                            || screeningData.Sex == null
                            || screeningData.OtherSide == null
                            )
                        {
                            screeningData.Status = 0;
                        }

                        TQuestionnaireScreening questionnaireScreening = dbSchool.TQuestionnaireScreenings.Where(w => w.SchoolID == schoolId && w.StudentID == studentId && w.IsDel == false).FirstOrDefault();
                        if (questionnaireScreening == null)
                        {
                            questionnaireScreening = new TQuestionnaireScreening
                            {
                                SchoolID = schoolId,
                                RoomID = screeningData.RoomID,
                                StudentID = studentId,
                                TermID = currentTermID,
                                YearID = currentYearID,

                                // 1. ด้านการเรียน
                                Academic = screeningData.Academic,
                                AcademicRisk = screeningData.AcademicRisk,
                                AcademicRiskNote = screeningData.AcademicRiskNote,
                                AcademicProblem = screeningData.AcademicProblem,
                                AcademicProblemNote = screeningData.AcademicProblemNote,

                                // 2. ด้านความสามารถอื่นๆ
                                Abilities = screeningData.Abilities,
                                AbilitiesNote = screeningData.AbilitiesNote,

                                // 3. ด้านสุขภาพ
                                Health = screeningData.Health,
                                HealthRisk = screeningData.HealthRisk,
                                HealthRiskNote = screeningData.HealthRiskNote,
                                HealthProblem = screeningData.HealthProblem,
                                HealthProblemNote = screeningData.HealthProblemNote,

                                // 4. ด้านสุขภาพจิตและพฤติกรรม
                                MentalHealthBehavior1 = screeningData.MentalHealthBehavior1,
                                MentalHealthBehavior2 = screeningData.MentalHealthBehavior2,
                                MentalHealthBehavior3 = screeningData.MentalHealthBehavior3,
                                MentalHealthBehavior4 = screeningData.MentalHealthBehavior4,
                                MentalHealthBehavior5 = screeningData.MentalHealthBehavior5,

                                // 5. ด้านเศรษฐกิจ
                                Economic = screeningData.Economic,
                                EconomicRisk = screeningData.EconomicRisk,
                                EconomicRiskMoney = screeningData.EconomicRiskMoney,
                                EconomicRiskNote = screeningData.EconomicRiskNote,
                                EconomicProblem = screeningData.EconomicProblem,
                                EconomicProblemNote = screeningData.EconomicProblemNote,

                                // 6. ด้านการคุ้มครองนักเรียน
                                Protection = screeningData.Protection,
                                ProtectionRisk = screeningData.ProtectionRisk,
                                ProtectionProblem = screeningData.ProtectionProblem,
                                ProtectionProblemNote = screeningData.ProtectionProblemNote,

                                // 7. ด้านการใช้สารเสพติด
                                SubstanceAbuse = screeningData.SubstanceAbuse,

                                // 8. ด้านเพศสัมพันธ์
                                Sex = screeningData.Sex,

                                // 9. ด้านอื่นๆ
                                OtherSide = screeningData.OtherSide,
                                OtherSideRiskNote = screeningData.OtherSideRiskNote,
                                OtherSideProblemNote = screeningData.OtherSideProblemNote,

                                Status = screeningData.Status,

                                IsDel = false,
                                CreatedDate = DateTime.Now.FixSecond(15).FixMillisecond(5)
                            };

                            dbSchool.TQuestionnaireScreenings.Add(questionnaireScreening);
                        }
                        else
                        {
                            // 1. ด้านการเรียน
                            questionnaireScreening.Academic = screeningData.Academic;
                            questionnaireScreening.AcademicRisk = screeningData.AcademicRisk;
                            questionnaireScreening.AcademicRiskNote = screeningData.AcademicRiskNote;
                            questionnaireScreening.AcademicProblem = screeningData.AcademicProblem;
                            questionnaireScreening.AcademicProblemNote = screeningData.AcademicProblemNote;

                            // 2. ด้านความสามารถอื่นๆ
                            questionnaireScreening.Abilities = screeningData.Abilities;
                            questionnaireScreening.AbilitiesNote = screeningData.AbilitiesNote;

                            // 3. ด้านสุขภาพ
                            questionnaireScreening.Health = screeningData.Health;
                            questionnaireScreening.HealthRisk = screeningData.HealthRisk;
                            questionnaireScreening.HealthRiskNote = screeningData.HealthRiskNote;
                            questionnaireScreening.HealthProblem = screeningData.HealthProblem;
                            questionnaireScreening.HealthProblemNote = screeningData.HealthProblemNote;

                            // 4. ด้านสุขภาพจิตและพฤติกรรม
                            questionnaireScreening.MentalHealthBehavior1 = screeningData.MentalHealthBehavior1;
                            questionnaireScreening.MentalHealthBehavior2 = screeningData.MentalHealthBehavior2;
                            questionnaireScreening.MentalHealthBehavior3 = screeningData.MentalHealthBehavior3;
                            questionnaireScreening.MentalHealthBehavior4 = screeningData.MentalHealthBehavior4;
                            questionnaireScreening.MentalHealthBehavior5 = screeningData.MentalHealthBehavior5;

                            // 5. ด้านเศรษฐกิจ
                            questionnaireScreening.Economic = screeningData.Economic;
                            questionnaireScreening.EconomicRisk = screeningData.EconomicRisk;
                            questionnaireScreening.EconomicRiskMoney = screeningData.EconomicRiskMoney;
                            questionnaireScreening.EconomicRiskNote = screeningData.EconomicRiskNote;
                            questionnaireScreening.EconomicProblem = screeningData.EconomicProblem;
                            questionnaireScreening.EconomicProblemNote = screeningData.EconomicProblemNote;

                            // 6. ด้านการคุ้มครองนักเรียน
                            questionnaireScreening.Protection = screeningData.Protection;
                            questionnaireScreening.ProtectionRisk = screeningData.ProtectionRisk;
                            questionnaireScreening.ProtectionProblem = screeningData.ProtectionProblem;
                            questionnaireScreening.ProtectionProblemNote = screeningData.ProtectionProblemNote;

                            // 7. ด้านการใช้สารเสพติด
                            questionnaireScreening.SubstanceAbuse = screeningData.SubstanceAbuse;

                            // 8. ด้านเพศสัมพันธ์
                            questionnaireScreening.Sex = screeningData.Sex;

                            // 9. ด้านอื่นๆ
                            questionnaireScreening.OtherSide = screeningData.OtherSide;
                            questionnaireScreening.OtherSideRiskNote = screeningData.OtherSideRiskNote;
                            questionnaireScreening.OtherSideProblemNote = screeningData.OtherSideProblemNote;

                            questionnaireScreening.Status = screeningData.Status;

                            questionnaireScreening.UpdatedDate = DateTime.Now.FixSecond(15).FixMillisecond(15);
                        }

                        dbSchool.SaveChanges();
                    }
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
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

        public class ScreeningData
        {
            [JsonProperty(PropertyName = "Id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "schoolId")]
            public int SchoolID { get; set; }

            [JsonProperty(PropertyName = "roomId")]
            public int RoomID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public int StudentID { get; set; }

            [JsonProperty(PropertyName = "termId")]
            public string TermID { get; set; }

            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }

            [JsonProperty(PropertyName = "saveDate")]
            public string SaveDate { get; set; }

            [JsonProperty(PropertyName = "updateDate")]
            public string UpdateDate { get; set; }

            // 1. ด้านการเรียน
            [JsonProperty(PropertyName = "academic")]
            public int? Academic { get; set; }

            [JsonProperty(PropertyName = "academicRisk")]
            public string AcademicRisk { get; set; }

            [JsonProperty(PropertyName = "academicRiskNote")]
            public string AcademicRiskNote { get; set; }

            [JsonProperty(PropertyName = "academicProblem")]
            public string AcademicProblem { get; set; }

            [JsonProperty(PropertyName = "academicProblemNote")]
            public string AcademicProblemNote { get; set; }

            // 2. ด้านความสามารถอื่นๆ
            [JsonProperty(PropertyName = "abilities")]
            public int? Abilities { get; set; }

            [JsonProperty(PropertyName = "abilitiesNote")]
            public string AbilitiesNote { get; set; }

            // 3. ด้านสุขภาพ
            [JsonProperty(PropertyName = "health")]
            public int? Health { get; set; }

            [JsonProperty(PropertyName = "healthRisk")]
            public string HealthRisk { get; set; }

            [JsonProperty(PropertyName = "healthRiskNote")]
            public string HealthRiskNote { get; set; }

            [JsonProperty(PropertyName = "healthProblem")]
            public string HealthProblem { get; set; }

            [JsonProperty(PropertyName = "healthProblemNote")]
            public string HealthProblemNote { get; set; }

            // 4. ด้านสุขภาพจิตและพฤติกรรม
            [JsonProperty(PropertyName = "mentalHealthBehavior1")]
            public int? MentalHealthBehavior1 { get; set; }

            [JsonProperty(PropertyName = "mentalHealthBehavior2")]
            public int? MentalHealthBehavior2 { get; set; }

            [JsonProperty(PropertyName = "mentalHealthBehavior3")]
            public int? MentalHealthBehavior3 { get; set; }

            [JsonProperty(PropertyName = "mentalHealthBehavior4")]
            public int? MentalHealthBehavior4 { get; set; }

            [JsonProperty(PropertyName = "mentalHealthBehavior5")]
            public int? MentalHealthBehavior5 { get; set; }

            // 5. ด้านเศรษฐกิจ
            [JsonProperty(PropertyName = "economic")]
            public int? Economic { get; set; }

            [JsonProperty(PropertyName = "economicRisk")]
            public string EconomicRisk { get; set; }

            [JsonProperty(PropertyName = "economicRiskMoney")]
            public string EconomicRiskMoney { get; set; }

            [JsonProperty(PropertyName = "economicRiskNote")]
            public string EconomicRiskNote { get; set; }

            [JsonProperty(PropertyName = "economicProblem")]
            public string EconomicProblem { get; set; }

            [JsonProperty(PropertyName = "economicProblemNote")]
            public string EconomicProblemNote { get; set; }

            // 6. ด้านการคุ้มครองนักเรียน
            [JsonProperty(PropertyName = "protection")]
            public int? Protection { get; set; }

            [JsonProperty(PropertyName = "protectionRisk")]
            public string ProtectionRisk { get; set; }

            [JsonProperty(PropertyName = "protectionProblem")]
            public string ProtectionProblem { get; set; }

            [JsonProperty(PropertyName = "protectionProblemNote")]
            public string ProtectionProblemNote { get; set; }

            // 7. ด้านการใช้สารเสพติด
            [JsonProperty(PropertyName = "substanceAbuse")]
            public int? SubstanceAbuse { get; set; }

            // 8. ด้านเพศสัมพันธ์
            [JsonProperty(PropertyName = "sex")]
            public int? Sex { get; set; }

            // 9. ด้านอื่นๆ
            [JsonProperty(PropertyName = "otherSide")]
            public int? OtherSide { get; set; }

            [JsonProperty(PropertyName = "otherSideRiskNote")]
            public string OtherSideRiskNote { get; set; }

            [JsonProperty(PropertyName = "otherSideProblemNote")]
            public string OtherSideProblemNote { get; set; }

            [JsonProperty(PropertyName = "status")]
            public int? Status { get; set; }

        }

        public class SDQResult
        {
            public int sID { get; set; }
            public int? ResultGroup1 { get; set; }
            public int? ResultGroup2 { get; set; }
            public int? ResultGroup3 { get; set; }
            public int? ResultGroup4 { get; set; }
            public int? ResultGroup5 { get; set; }
        }
    }
}