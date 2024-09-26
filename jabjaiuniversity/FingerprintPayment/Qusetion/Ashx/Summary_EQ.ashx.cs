using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Data;
using Newtonsoft.Json.Linq;
using FingerprintPayment.Helper;


namespace FingerprintPayment.Qusetion.Ashx
{
    /// <summary>
    /// Summary description for Summary_EQ
    /// </summary>
    public class Summary_EQ : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;

            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var id = fcommon.ReplaceInjection(context.Request["sID"]);
            var term = fcommon.ReplaceInjection(context.Request["term"]);

            var sID = int.Parse(id);

            dynamic rss = new JObject();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));

            var titleLists = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.deleted != "1" && w.workStatus == "working").AsQueryable().ToList();

            var q = (from a in _db.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                     join b in _db.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                     where a.sID == sID && a.nTerm == term
                     group a by new { a.sID, a.sStudentTitle, a.sName, a.sLastname, a.sStudentID, a.nStudentNumber, a.SubLevel, a.nTSubLevel2, b.sStudentPicture } into gb
                     select new
                     {
                         Title = gb.Key.sStudentTitle,
                         Name = gb.Key.sName + " " + gb.Key.sLastname,
                         StudentID = gb.Key.sStudentID,
                         StudentPicture = gb.Key.sStudentPicture,
                         StudentNumber = gb.Key.nStudentNumber,
                         ClassRoom = gb.Key.SubLevel + " / " + gb.Key.nTSubLevel2,
                         level1 = (from a in _db.TQuestionnaireEQ.Where(w => w.sID == gb.Key.sID)
                                   group a by new { a.QuestionLargeGroup } into gb1
                                   orderby gb1.Key.QuestionLargeGroup ascending
                                   select new
                                   {
                                       lScore = gb1.Sum(s => s.QuestionScore),
                                       lGroup = gb1.Key.QuestionLargeGroup,
                                       level2 = (from a in gb1
                                                 group a by new { a.QuestionSmallGroup } into gb2
                                                 orderby gb2.Key.QuestionSmallGroup ascending
                                                 select new
                                                 {
                                                     sGroup = gb2.Key.QuestionSmallGroup,
                                                     sScore = gb2.Sum(s => s.QuestionScore)
                                                 }).ToList()
                                   }).ToList()
                     }).ToList();

            var value = (from a in q
                         select new
                         {
                             Name = Common.geTitelName(titleLists, a.Title) + a.Name,
                             StudentID = a.StudentID,
                             StudentPicture = a.StudentPicture,
                             StudentNumber = a.StudentNumber,
                             ClassRoom = a.ClassRoom,
                             level1 = (from a1 in a.level1
                                       orderby a1.lGroup ascending
                                       select new
                                       {
                                           lScore = a1.lScore,
                                           lGroup = a1.lGroup,
                                           level2 = (from a2 in a1.level2
                                                     orderby a2.sGroup ascending
                                                     select new
                                                     {
                                                         sGroup = a2.sGroup,
                                                         sScore = a2.sScore
                                                     }).ToList()
                                       }).ToList()
                         }).ToList();

            rss = new JArray(from a in value
                             select new JObject(
                                 new JProperty("Name", a.Name),
                                 new JProperty("StudentID", a.StudentID),
                                 new JProperty("StudentPicture", a.StudentPicture),
                                 new JProperty("StudentNumber", a.StudentNumber),
                                 new JProperty("ClassRoom", a.ClassRoom),
                                 new JProperty("level1",
                                 new JArray(
                                     from b in a.level1
                                     select new JObject(
                                         new JProperty("lScore", b.lScore),
                                         new JProperty("lGroup", b.lGroup),
                                         new JProperty("level2",
                                         new JArray(
                                             from c in b.level2
                                             select new JObject(
                                                 new JProperty("sGroup", c.sGroup),
                                                 new JProperty("sScore", c.sScore)
                                                 )))
                                         )))
                                 ));

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
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