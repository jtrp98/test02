using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using Ninject;
using SchoolBright.Business.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    /// <summary>
    /// Summary description for checkGradeDetail
    /// </summary>
    public class checkGradeDetail : IHttpHandler, IRequiresSessionState
    {
        //[Inject]
        //public ICommonService CommonService { get; set; }
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            dynamic rss = new JObject();

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();

            string id = fcommon.ReplaceInjection(context.Request["id"]);

            List<stdDetail> stdDetailList = new List<stdDetail>();
            stdDetail stdDetail = new stdDetail();
            int idn = int.Parse(id);

            foreach(var a in _dbGrade.GetGradeDetailView(userData.CompanyID, 0, idn))
            {
                var b = _dbGrade.TGrades.Where(w => w.nGradeId == a.nGradeId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                
                var d = _db.TTerms.Where(w => w.nTerm == b.nTerm && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var e = _db.TYears.Where(w => w.nYear == d.nYear && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var room = _db.TTermSubLevel2.Where(w => w.nTermSubLevel2 == b.nTermSubLevel2 && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var sub = _db.TSubLevels.Where(w => w.nTSubLevel == room.nTSubLevel && w.SchoolID == userData.CompanyID).FirstOrDefault();

                var planCourseDTOs = ServiceHelper.GetPlanCourses(sub.nTSubLevel, ((b.nTermSubLevel2 != null) ? (int)b.nTermSubLevel2 : 0), d.nTerm, e.nYear, userData.CompanyID, _db);
                var planCourseDTO = planCourseDTOs.Where(w => w.SPlaneId == b.sPlaneID).FirstOrDefault();

                stdDetail = new stdDetail
                {
                    term = d.sTerm,
                    year = e.numberYear,
                    classroom = sub.SubLevel + " / " + room.nTSubLevel2,
                    planname = planCourseDTO.CourseName,
                    planid = b.sPlaneID.ToString(),
                    gradeid = b.nGradeId,
                    getScore100 = a.getScore100,
                    getSpecial = a.getSpecial,
                    scoreGrade1 = a.scoreGrade1,
                    scoreGrade2 = a.scoreGrade2,
                    scoreGrade3 = a.scoreGrade3,
                    scoreGrade4 = a.scoreGrade4,
                    scoreCheewat1 = a.scoreCheewat1,
                    scoreMidTerm = a.scoreMidTerm,
                    scoreFinalTerm = a.scoreFinalTerm,
                    getMid100 = a.getMid100,
                    getQuiz100 = a.getQuiz100,
                    getFinal100 = a.getFinal100,
                    getgrade = a.getGradeLabel,
                    planStatus = planCourseDTO.CourseStatus
                };
                stdDetailList.Add(stdDetail);
            }

            rss = new JArray(from a in stdDetailList
                             select new JObject(

                   new JProperty("term", a.term),
                   new JProperty("year", a.year),
                   new JProperty("planname", a.planname),
                   new JProperty("classroom", a.classroom),
                   new JProperty("planid", a.planid),
                   new JProperty("gradeid", a.gradeid),
                   new JProperty("getScore100", a.getScore100),
                   new JProperty("getSpecial", a.getSpecial),
                   new JProperty("scoreGrade1", a.scoreGrade1),
                   new JProperty("scoreGrade2", a.scoreGrade2),
                   new JProperty("scoreGrade3", a.scoreGrade3),
                   new JProperty("scoreGrade4", a.scoreGrade4),
                   new JProperty("scoreCheewat1", a.scoreCheewat1),
                   new JProperty("scoreMidTerm", a.scoreMidTerm),
                   new JProperty("scoreFinalTerm", a.scoreFinalTerm),
                   new JProperty("getMid100", a.getMid100),
                   new JProperty("getQuiz100", a.getQuiz100),
                   new JProperty("getFinal100", a.getFinal100),
                   new JProperty("getgrade", a.getgrade),
                   new JProperty("planStatus", a.planStatus)

                ));

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

        //protected class stdDetail
        //{
        //    public string planid { get; set; }
        //    public string planname { get; set; }
        //    public string classroom { get; set; }
        //    public string term { get; set; }
        //    public int? year { get; set; }
        //    public int? ntermidlv2 { get; set; }
        //    public string getScore100 { get; set; }
        //    public string getSpecial { get; set; }
        //    public string scoreGrade1 { get; set; }
        //    public string scoreGrade2 { get; set; }
        //    public string scoreGrade3 { get; set; }
        //    public string scoreGrade4 { get; set; }
        //    public string scoreCheewat1 { get; set; }
        //    public string scoreMidTerm { get; set; }
        //    public string scoreFinalTerm { get; set; }
        //    public string getMid100 { get; set; }
        //    public string getQuiz100 { get; set; }
        //    public string getFinal100 { get; set; }
        //    public string getgrade { get; set; }
        //    public int? gradeid { get; set; }
        //    public int? planStatus { get; set; }
        //}
    }


}