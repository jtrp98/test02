using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.VisitHousePage.Mobile
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<StudentModel> studentModels = new List<StudentModel>();
        protected void Page_Load(object sender, EventArgs e)
        {
            int.TryParse(Request.QueryString["schoolid"], out int schoolID);
            int.TryParse(Request.QueryString["roomid"], out int roomID);
            if (schoolID != 0 && roomID != 0)
            {
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    StudentLogic studentLogic = new StudentLogic(ctx);
                    string currentTermID = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                    int? currentYearID = null;
                    var termObj = ctx.TTerms.Where(w => w.nTerm == currentTermID).FirstOrDefault();
                    if (termObj != null)
                    {
                        currentYearID = termObj.nYear;
                    }

                    string query = string.Format(@"
SELECT sv.sID 'ID', sv.sStudentID 'StudentID', sv.sName 'Name', sv.sLastname 'Lastname', CAST((CASE WHEN hv.ID IS NOT NULL THEN 1 ELSE 0 END) AS BIT) 'HomeVisitRecorded', u.sStudentPicture 'StudentPicture'
, ISNULL(A1.SDQRecorded, 0) 'SDQStudentRecorded'
, ISNULL(A2.SDQRecorded, 0) 'SDQTeacherRecorded'
, ISNULL(A3.SDQRecorded, 0) 'SDQParentsRecorded'
, ISNULL(A1.AllQuestion, 0) 'SDQStudentAllQuestion'
, ISNULL(A2.AllQuestion, 0) 'SDQTeacherAllQuestion'
, ISNULL(A3.AllQuestion, 0) 'SDQParentsAllQuestion'
, ISNULL(Q261.Question26, 0) 'SDQStudentQuestion26'
, ISNULL(Q262.Question26, 0) 'SDQTeacherQuestion26'
, ISNULL(Q263.Question26, 0) 'SDQParentsQuestion26'
, ISNULL(E2.EQRecorded, 0) 'EQStudentRecorded'
, ISNULL(E2.AllQuestion, 0) 'EQStudentAllQuestion'
, CAST((CASE WHEN qs.ID IS NOT NULL THEN 1 ELSE 0 END) AS BIT) 'ScreeningStudentRecorded'
FROM TB_StudentViews sv 
LEFT JOIN THomeVisit hv ON sv.SchoolID = hv.SchoolID AND sv.sID = hv.StudentID AND hv.YearID = {3} AND hv.TermID = '{2}' AND ISNULL(hv.IsDel, 0) = 0
LEFT JOIN TUser u ON sv.SchoolID = u.SchoolID AND sv.sID = u.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'SDQRecorded', 33 'AllQuestion' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND SDQType=1 GROUP BY SchoolID, sID) A1 ON sv.SchoolID = A1.SchoolID AND sv.sID = A1.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'SDQRecorded', 31 'AllQuestion' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND SDQType=2 GROUP BY SchoolID, sID) A2 ON sv.SchoolID = A2.SchoolID AND sv.sID = A2.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'SDQRecorded', 31 'AllQuestion' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND SDQType=3 GROUP BY SchoolID, sID) A3 ON sv.SchoolID = A3.SchoolID AND sv.sID = A3.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'Question26' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND QuestionDes='Question26' AND SDQType=1 AND ChoiceNo=1 GROUP BY SchoolID, sID) Q261 ON sv.SchoolID = Q261.SchoolID AND sv.sID = Q261.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'Question26' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND QuestionDes='Question26' AND SDQType=2 AND ChoiceNo=1 GROUP BY SchoolID, sID) Q262 ON sv.SchoolID = Q262.SchoolID AND sv.sID = Q262.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'Question26' FROM TQuestionnaireSDQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND QuestionDes='Question26' AND SDQType=3 AND ChoiceNo=1 GROUP BY SchoolID, sID) Q263 ON sv.SchoolID = Q263.SchoolID AND sv.sID = Q263.sID
LEFT JOIN (SELECT SchoolID, sID, COUNT(*) 'EQRecorded', 52 'AllQuestion' FROM TQuestionnaireEQ WHERE SchoolID={0} AND TermID='{2}' AND cDel=0 AND EQType=1 GROUP BY SchoolID, sID) E2 ON sv.SchoolID = E2.SchoolID AND sv.sID = E2.sID
LEFT JOIN TQuestionnaireScreening qs ON sv.SchoolID = qs.SchoolID AND sv.sID = qs.StudentID AND qs.Status = 1
WHERE sv.SchoolID={0} AND sv.nTermSubLevel2={1} AND sv.nTerm='{2}' AND ISNULL(sv.cDel, '0')='0' AND ISNULL(sv.nStudentStatus, 0) = 0
ORDER BY sv.nStudentNumber", schoolID, roomID, currentTermID, currentYearID);
                    studentModels = ctx.Database.SqlQuery<StudentModel>(query).ToList();
                }
            }
        }

        public class StudentModel
        {
            [JsonProperty(PropertyName = "Id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "studentId")]
            public string StudentID { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "lastname")]
            public string Lastname { get; set; }

            [JsonProperty(PropertyName = "homeVisitRecorded")]
            public bool HomeVisitRecorded { get; set; }

            [JsonProperty(PropertyName = "studentPicture")]
            public string StudentPicture { get; set; }

            [JsonProperty(PropertyName = "sdqStudentRecorded")]
            public int SDQStudentRecorded { get; set; }

            [JsonProperty(PropertyName = "sdqTeacherRecorded")]
            public int SDQTeacherRecorded { get; set; }

            [JsonProperty(PropertyName = "sdqParentsRecorded")]
            public int SDQParentsRecorded { get; set; }

            [JsonProperty(PropertyName = "sdqStudentAllQuestion")]
            public int SDQStudentAllQuestion { get; set; }

            [JsonProperty(PropertyName = "sdqTeacherAllQuestion")]
            public int SDQTeacherAllQuestion { get; set; }

            [JsonProperty(PropertyName = "sdqParentsAllQuestion")]
            public int SDQParentsAllQuestion { get; set; }

            [JsonProperty(PropertyName = "sdqStudentQuestion26")]
            public int SDQStudentQuestion26 { get; set; }

            [JsonProperty(PropertyName = "sdqTeacherQuestion26")]
            public int SDQTeacherQuestion26 { get; set; }

            [JsonProperty(PropertyName = "sdqParentsQuestion26")]
            public int SDQParentsQuestion26 { get; set; }

            [JsonProperty(PropertyName = "eqStudentRecorded")]
            public int EQStudentRecorded { get; set; }

            [JsonProperty(PropertyName = "eqStudentAllQuestion")]
            public int EQStudentAllQuestion { get; set; }

            [JsonProperty(PropertyName = "screeningStudentRecorded")]
            public bool ScreeningStudentRecorded { get; set; }

        }

    }
}