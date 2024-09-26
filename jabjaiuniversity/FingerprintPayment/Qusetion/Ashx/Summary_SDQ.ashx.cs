using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System.Data;
using System.Web.SessionState;
using FingerprintPayment.Helper;
using SchoolBright.DTO.Parameters;
using Newtonsoft.Json.Linq;

namespace FingerprintPayment.Qusetion.Ashx
{
    /// <summary>
    /// Summary description for Summary_SDQ
    /// </summary>
    public class Summary_SDQ : IHttpHandler
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

            JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read));
            //StudentLogic studentLogic = new StudentLogic(en);
            //string currentTerm = studentLogic.GetTermId(userData);

            var titleLists = en.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.deleted != "1" && w.workStatus == "working").ToList();

            var q = (from a in en.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID)
                     join b in en.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
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
                         Results = (from a in en.TQuestionnaireSDQs.Where(w => w.sID == gb.Key.sID)
                                    group a by new { a.QuestionGroup } into gb2
                                    select new
                                    {
                                        nScore = gb2.Sum(s => s.QuestionScore),
                                        sScore = gb2.Sum(s => s.QuestionScore),
                                        sGroup = gb2.Key.QuestionGroup
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
                             Results = (from a2 in a.Results
                                        orderby a2.sGroup ascending
                                        select new
                                        {
                                            Score = a2.nScore,
                                            Result = ScoreName(a2.nScore, a2.sGroup),
                                            GroupName = GroupName(a2.sGroup),
                                            GroupNum = a2.sGroup
                                        }).ToList()
                         }).ToList();

            rss = new JArray(from a in value
                             select new JObject(
                                 new JProperty("Name", a.Name),
                                 new JProperty("StudentID", a.StudentID),
                                 new JProperty("StudentPicture", a.StudentPicture),
                                 new JProperty("StudentNumber", a.StudentNumber),
                                 new JProperty("ClassRoom", a.ClassRoom),
                                 new JProperty("Results",
                                 new JArray(
                                     from c in a.Results
                                     select new JObject(
                                         new JProperty("Score", c.Score),
                                         new JProperty("Result", c.Result),
                                         new JProperty("GroupName", c.GroupName),
                                         new JProperty("GroupNum", c.GroupNum)
                                         )))));

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            context.Response.Write(rss);
            context.Response.End();
        }


        private string ScoreName(int? val, int? group)
        {
            var result = "";
            switch (group)
            {
                case 1:
                    if (val <= 5) result = "ปกติ";
                    else if (val == 6) result = "เสี่ยง";
                    else result = "มีปัญหา";
                    break;
                case 2:
                    if (val <= 4) result = "ปกติ";
                    else if (val == 5) result = "เสี่ยง";
                    else result = "มีปัญหา";
                    break;
                case 3:
                    if (val <= 5) result = "ปกติ";
                    else if (val == 6) result = "เสี่ยง";
                    else result = "มีปัญหา";
                    break;
                case 4:
                    if (val <= 3) result = "ปกติ";
                    else if (val == 4) result = "เสี่ยง";
                    else result = "มีปัญหา";
                    break;
                case 5:
                    if (val <= 2) result = "มีปัญหา";
                    else if (val == 3) result = "เสี่ยง";
                    else result = "ปกติ";
                    break;
                case 6:
                    if (val == 0) result = "ปกติ";
                    else if (val <= 2) result = "เสี่ยง";
                    else result = "มีปัญหา";
                    break;
            }
            return result;
        }

        public string GroupName(int? val)
        {
            var result = "";

            switch (val)
            {
                case 1:
                    result = "1. คะแนนพฤติกรรมด้านอารมณ์";
                    break;
                case 2:
                    result = "2. คะแนนพฤติกรรมความประพฤติ";
                    break;
                case 3:
                    result = "3. คะแนนพฤติกรรมความมีสมาธิ/สมาธิสั้น";
                    break;
                case 4:
                    result = "4. คะแนนพฤติกรรมด้านความสัมพันธ์กับเพื่อน";
                    break;
                case 5:
                    result = "5. คะแนนพฤติกรรมสัมพันธภาพทางสังคม";
                    break;
                case 6:
                    result = "6. คะแนนรวมการประเมินส่วนที่ 2";
                    break;
            }

            return result;
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