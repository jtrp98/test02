using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for gradeAutosave
    /// </summary>
    public class gradeAutosave : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";

            JabJaiMasterEntities dbmaster = Connection.MasterEntities();
            var tCompany = dbmaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            dynamic rss = new JObject();
            
            string target = fcommon.ReplaceInjection(context.Request["target"]);
            string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
            int sEmpID = int.Parse(HttpContext.Current.Session["sEmpID"] + "");
            int? idlv2n = Int32.Parse(idlv2);

            string idlv = fcommon.ReplaceInjection(context.Request["idlvn"]);
            int? idlvn = Int32.Parse(idlv);

            string year = fcommon.ReplaceInjection(context.Request["nYear"]);
            int? useryear = Int32.Parse(year);
            string sid = fcommon.ReplaceInjection(context.Request["sid"]);
            string nterm = fcommon.ReplaceInjection(context.Request["nterm"]);
            string planid = fcommon.ReplaceInjection(context.Request["planid"]);
            int sidn = int.Parse(sid);
            string gradeid = fcommon.ReplaceInjection(context.Request["gradeid"]);
            int gradeidn = int.Parse(gradeid);
            string score = fcommon.ReplaceInjection(context.Request["score"]);
            var data1 = _db.TGradeDetails.Where(w => w.nGradeId == gradeidn && w.sID == sidn && w.SchoolID == userData.CompanyID).FirstOrDefault();
            string nameIdentifier = string.Empty;
            switch (target)
            {                
                case "txtGrade1":
                    target = "scoreGrade1";
                    nameIdentifier = "nameGrade1";
                    break;
                case "txtGrade2":
                    target = "scoreGrade2";
                    nameIdentifier = "nameGrade2";
                    break;
                case "txtGrade3":
                    target = "scoreGrade3";
                    nameIdentifier = "nameGrade3";
                    break;
                case "txtGrade4":
                    target = "scoreGrade4";
                    nameIdentifier = "nameGrade4";
                    break;
                case "txtGrade5":
                    target = "scoreGrade5";
                    nameIdentifier = "nameGrade5";
                    break;
                case "txtGrade6":
                    target = "scoreGrade6";
                    nameIdentifier = "nameGrade6";
                    break;
                case "txtGrade7":
                    target = "scoreGrade7";
                    nameIdentifier = "nameGrade7";
                    break;
                case "txtGrade8":
                    target = "scoreGrade8";
                    nameIdentifier = "nameGrade8";
                    break;
                case "txtGrade9":
                    target = "scoreGrade9";
                    nameIdentifier = "nameGrade9";
                    break;
                case "txtGrade10":
                    target = "scoreGrade10";
                    nameIdentifier = "nameGrade10";
                    break;
                case "txtGrade11":
                    target = "scoreGrade11";
                    nameIdentifier = "nameGrade11";
                    break;
                case "txtGrade12":
                    target = "scoreGrade12";
                    nameIdentifier = "nameGrade12";
                    break;
                case "txtGrade13":
                    target = "scoreGrade13";
                    nameIdentifier = "nameGrade13";
                    break;
                case "txtGrade14":
                    target = "scoreGrade14";
                    nameIdentifier = "nameGrade14";
                    break;
                case "txtGrade15":
                    target = "scoreGrade15";
                    nameIdentifier = "nameGrade15";
                    break;
                case "txtGrade16":
                    target = "scoreGrade16";
                    nameIdentifier = "nameGrade16";
                    break;
                case "txtGrade17":
                    target = "scoreGrade17";
                    nameIdentifier = "nameGrade17";
                    break;
                case "txtGrade18":
                    target = "scoreGrade18";
                    nameIdentifier = "nameGrade18";
                    break;
                case "txtGrade19":
                    target = "scoreGrade19";
                    nameIdentifier = "nameGrade19";
                    break;
                case "txtGrade20":
                     target = "scoreGrade20";
                    nameIdentifier = "nameGrade20";
                    break;
                case "chewat1":
                    target = "scoreCheewat1";
                    nameIdentifier = "nameCheewat1";
                    
                    break;
                case "chewat2":
                    target = "scoreCheewat2";
                    nameIdentifier = "nameCheewat2";
                    break;
                case "chewat3":
                    target = "scoreCheewat3";
                    nameIdentifier = "nameCheewat3";
                    break;
                case "chewat4":
                    target = "scoreCheewat4";
                    nameIdentifier = "nameCheewat4";
                    break;
                case "chewat5":
                    target = "scoreCheewat5";
                    nameIdentifier = "nameCheewat5";
                    break;
                case "chewat6":
                    target = "scoreCheewat6";
                    nameIdentifier = "nameCheewat6";
                    break;
                case "chewat7":
                    target = "scoreCheewat7";
                    nameIdentifier = "nameCheewat7";
                    break;
                case "chewat8":
                    target = "scoreCheewat8";
                    nameIdentifier = "nameCheewat8";
                    break;
                case "chewat9":
                    target = "scoreCheewat9";
                    nameIdentifier = "nameCheewat9";
                    break;
                case "chewat10":
                    target = "scoreCheewat10";
                    nameIdentifier = "nameCheewat10";
                    break;
                case "chewat11":
                    target = "scoreCheewat11";
                    nameIdentifier = "nameCheewat11";
                    break;
                case "chewat12":
                    target = "scoreCheewat12";
                    nameIdentifier = "nameCheewat12";
                    break;
                case "chewat13":
                    target = "scoreCheewat13";
                    nameIdentifier = "nameCheewat13";
                    break;
                case "chewat14":
                    target = "scoreCheewat14";
                    nameIdentifier = "nameCheewat14";
                    break;
                case "chewat15":
                    target = "scoreCheewat15";
                    nameIdentifier = "nameCheewat15";
                    break;
                case "chewat16":
                    target = "scoreCheewat16";
                    nameIdentifier = "nameCheewat16";
                    break;
                case "chewat17":
                    target = "scoreCheewat17";
                    nameIdentifier = "nameCheewat17";
                    break;
                case "chewat18":
                    target = "scoreCheewat18";
                    nameIdentifier = "nameCheewat18";
                    break;
                case "chewat19":
                    target = "scoreCheewat19";
                    nameIdentifier = "nameCheewat19";
                    break;
                case "chewat20":
                    target = "scoreCheewat20";
                    nameIdentifier = "nameCheewat20";
                    break;
                case "midscore1":
                    target = "scoreMid1";
                    nameIdentifier = "nameMid1";
                    break;
                case "midscore2":
                    target = "scoreMid2";
                    nameIdentifier = "nameMid2";
                    break;
                case "midscore3":
                    target = "scoreMid3";
                    nameIdentifier = "nameMid3";
                    break;
                case "midscore4":
                    target = "scoreMid4";
                    nameIdentifier = "nameMid4";
                    break;
                case "midscore5":
                    target = "scoreMid5";
                    nameIdentifier = "nameMid5";
                    break;
                case "midscore6":
                    target = "scoreMid6";
                    nameIdentifier = "nameMid6";
                    break;
                case "midscore7":
                    target = "scoreMid7";
                    nameIdentifier = "nameMid7";
                    break;
                case "midscore8":
                    target = "scoreMid8";
                    nameIdentifier = "nameMid8";
                    break;
                case "midscore9":
                    target = "scoreMid9";
                    nameIdentifier = "nameMid9";
                    break;
                case "midscore10":
                    target = "scoreMid10";
                    nameIdentifier = "nameMid10";
                    break;
                case "finalscore1":
                    target = "scoreFinal1";
                    nameIdentifier = "scoreFinal1";
                    break;
                case "finalscore2":
                    target = "scoreFinal2";
                    nameIdentifier = "scoreFinal2";
                    break;
                case "finalscore3":
                    target = "scoreFinal3";
                    nameIdentifier = "scoreFinal3";
                    break;
                case "finalscore4":
                    target = "scoreFinal4";
                    nameIdentifier = "scoreFinal4";
                    break;
                case "finalscore5":
                    target = "scoreFinal5";
                    nameIdentifier = "scoreFinal5";
                    break;
                case "finalscore6":
                    target = "scoreFinal6";
                    nameIdentifier = "scoreFinal6";
                    break;
                case "finalscore7":
                    target = "scoreFinal7";
                    nameIdentifier = "scoreFinal7";
                    break;
                case "finalscore8":
                    target = "scoreFinal8";
                    nameIdentifier = "scoreFinal8";
                    break;
                case "finalscore9":
                    target = "scoreFinal9";
                    nameIdentifier = "scoreFinal9";
                    break;
                case "finalscore10":
                    target = "scoreFinal10";
                    nameIdentifier = "scoreFinal10";
                    break;
                case "behave1":
                    target = "scoreBehavior1";
                    nameIdentifier = "nameBehavior1";
                    break;
                case "behave2":
                    target = "scoreBehavior2";
                    nameIdentifier = "nameBehavior2";
                    break;
                case "behave3":
                    target = "scoreBehavior3";
                    nameIdentifier = "nameBehavior3";
                    break;
                case "behave4":
                    target = "scoreBehavior4";
                    nameIdentifier = "nameBehavior4";
                    break;
                case "behave5":
                    target = "scoreBehavior5";
                    nameIdentifier = "nameBehavior5";
                    break;
                case "behave6":
                    target = "scoreBehavior6";
                    nameIdentifier = "nameBehavior6";
                    break;
                case "behave7":
                    target = "scoreBehavior7";
                    nameIdentifier = "nameBehavior7";
                    break;
                case "behave8":
                    target = "scoreBehavior8";
                    nameIdentifier = "nameBehavior8";
                    break;
                case "behave9":
                    target = "scoreBehavior9";
                    nameIdentifier = "nameBehavior9";
                    break;
                case "behave10":
                    target = "scoreBehavior10";
                    nameIdentifier = "nameBehavior10";
                    break;
                case "txtMidScore":
                    target = string.Empty;
                    data1.scoreMidTerm = score;
                    break;
                case "txtLateScore":
                    target = string.Empty;
                    data1.scoreFinalTerm = score;
                    break;
                case "txtGoodBehavior":
                    target = string.Empty;
                    data1.getBehaviorLabel = score;
                    break;
                case "txtGoodReading":
                    target = string.Empty;
                    data1.getReadWrite = score;
                    break;
                case "txtSamattana":
                    target = string.Empty;
                    data1.getSamattana = score;
                    break;
            }

            if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(nameIdentifier))
            {
                if ( _db.TStudentAssessmentScores.Where(w => w.SchoolId == userData.CompanyID).Where(w => w.nGradeId == gradeidn && w.sID == sidn && w.ScoreIdentifier == target).Count() > 0)
                {
                    var studentScore = _db.TStudentAssessmentScores.Where(w => w.SchoolId == userData.CompanyID).Where(w => w.nGradeId == gradeidn && w.sID == sidn && w.ScoreIdentifier == target).FirstOrDefault();
                    studentScore.Score = !string.IsNullOrEmpty(score) ? score : "";
                    studentScore.UpdatedDate = DateTime.Now;
                    studentScore.UpdatedBy = sEmpID;
                }
                else
                {
                    var assessment = _db.TAssessments.Where(w => w.SchoolId == userData.CompanyID).Where(w => w.nGradeId == gradeidn && w.NameIdentifier == nameIdentifier).FirstOrDefault();
                    var studentAssessmentScore = new TStudentAssessmentScore();
                    int sPlaneId = 0;
                    int.TryParse(planid, out sPlaneId);


                    var tYear = _db.TYears.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.numberYear == useryear).FirstOrDefault();
                    DateTime createdDate = DateTime.Now;
                    studentAssessmentScore.nYear = tYear.nYear;
                    studentAssessmentScore.nTerm = nterm ;
                    studentAssessmentScore.IsActive = true;
                    studentAssessmentScore.nGradeId = gradeidn;
                    studentAssessmentScore.Score = !string.IsNullOrEmpty(score) ? score : "";
                    studentAssessmentScore.ScoreIdentifier = target;
                    studentAssessmentScore.SchoolId = tCompany.nCompany;
                    studentAssessmentScore.nTermSubLevel2 = idlv2n;
                    studentAssessmentScore.nTSubLevel = idlvn ;
                    studentAssessmentScore.sID = sidn;
                    studentAssessmentScore.sPlaneID = sPlaneId;
                    studentAssessmentScore.AssessmentId = assessment.AssessmentId;
                    studentAssessmentScore.UpdatedDate = createdDate;
                    studentAssessmentScore.UpdatedBy = sEmpID;
                    studentAssessmentScore.CreatedDate = createdDate;
                    studentAssessmentScore.CreatedBy = sEmpID;
                    studentAssessmentScore.SchoolId = userData.CompanyID;
                    _db.TStudentAssessmentScores.Add(studentAssessmentScore);
                }
            }
            _db.SaveChanges();

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
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