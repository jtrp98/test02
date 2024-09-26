using FingerprintPayment.Report.Models;
using ReportModels = FingerprintPayment.Report.Models.BehaviorsReports;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JabjaiMainClass;
using JabjaiMainClass.Models;

namespace FingerprintPayment.Report.Functions.BehaviorsReports
{
    public class reportsType_01
    {
        public static List<ReportModels.ListModels> GetReports(JabJaiEntities dbschool, Search search, JWTToken.userData userData)
        {
            List<ReportModels.ListModels> models = new List<ReportModels.ListModels>();
            if (search.student_id.HasValue)
            {
                search.level2_id = null;
                search.level_id = null;
            }
            var setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();

            decimal maxScore = setting.MaxScore ?? 100;
            StudentLogic logic = new StudentLogic(dbschool);
            string DBName = "";

            var q1 = new List<StudentModel>();
            if (search.level2_id.HasValue) q1 = logic.getByRooms(search.level2_id.Value, search.term_id, userData);
            else if (search.level_id.HasValue) q1 = logic.getByClass(search.level_id.Value, search.term_id, userData);
            else if (search.student_id.HasValue) q1.Add(logic.getById(search.student_id.Value, search.term_id, userData));
            else q1 = logic.getAll(search.term_id, userData);

            var f_setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();
            int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);

            if (f_setting.Type != 1)
            {
                var f1 = dbschool.TTerms.FirstOrDefault(f => f.nTerm == search.term_id);
                var q2 = dbschool.TTerms.Where(w => w.nYear == f1.nYear && (w.cDel ?? "0") == "0").ToList();
                search.dStart = q2.Select(s => s.dStart).Min();
                search.dEnd = q2.Select(s => s.dEnd).Max();
            }

            var f_term = logic.GetTermDATA(search.dStart.Value, userData);

            var f_termCurrent = logic.GetTermDATA(DateTime.Today, userData);
            //if (f_termCurrent.nYear != f_term.nYear)
            //{
            //    DBName = "JabjaiSchoolHistory";
            //}
            //else
            //{
            //    DBName = "JabjaiSchoolSingleDB";
            //}

            decimal ScoreAlert = ShowMinusSign * (f_setting.Alert ?? 0);

            var q_Id = q1.Select(s => s.student_Id).ToList();

            string StudentId = "";
            foreach (var id in q_Id)
            {
                StudentId += (string.IsNullOrEmpty(StudentId) ? "" : ",") + id;
            }

            if (!string.IsNullOrEmpty(StudentId))
            {
                StudentId = " AND  StudentId IN ( " + StudentId + " ) ";
            }

            List<TBehaviorHistory> q_HistoryData = new List<TBehaviorHistory>();

            string SQL = string.Format(@"SELECT * FROM [{0}].[dbo].[TBehaviorHistory] 
WHERE dCanCel IS NULL AND dAdd BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  AND cDel = 0 {3}",
                "JabjaiSchoolSingleDB", search.dStart, search.dEnd, StudentId);

            q_HistoryData = dbschool.Database.SqlQuery<TBehaviorHistory>(SQL).ToList();

            SQL = string.Format(@"SELECT * FROM [{0}].[dbo].[TBehaviorHistory] 
WHERE dCanCel IS NULL AND dAdd BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}'  AND cDel = 0 {3}",
   "JabjaiSchoolHistory", search.dStart, search.dEnd, StudentId);

            q_HistoryData.AddRange(dbschool.Database.SqlQuery<TBehaviorHistory>(SQL).ToList());

            //var q_HistoryData = (from a in dbschool.TBehaviorHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
            //                     where q_Id.Contains(a.StudentId.Value) &&
            //                     search.dStart <= a.dAdd && search.dEnd >= a.dAdd
            //                     && a.dCanCel == null
            //                     select a).ToList();
            BehaviorHistoryClass behaviorHistoryClass = new BehaviorHistoryClass();

            models = (from a1 in (from a in q1
                                  join b in q_HistoryData on a.student_Id equals b.StudentId into jab
                                  from jb in jab.DefaultIfEmpty()
                                  select new
                                  {
                                      a.student_Code,
                                      a.student_Number,
                                      student_Name = a.student_Title + " " + a.student_Name,
                                      a.student_Id,
                                      Score = jb == null ? (setting.Type == 3 ? behaviorHistoryClass.getResidualScore(dbschool, a.student_Id, f_term.nTerm, maxScore) : maxScore) : jb.ResidualScore,
                                      DayAdd = jb == null ? search.dEnd : jb.dAdd,
                                      BehaviorHistoryId = jb == null ? 1 : jb.BehaviorHistoryId
                                  }).ToList()

                      group a1 by new
                      {
                          a1.student_Code,
                          a1.student_Number,
                          a1.student_Name,
                          a1.student_Id,
                      } into gb
                      select new ReportModels.ListModels
                      {
                          userId = gb.Key.student_Id,
                          studentName = gb.Key.student_Name,
                          studentId = gb.Key.student_Code,
                          studentNumber = gb.Key.student_Number,
                          Score = gb.Where(w => w.BehaviorHistoryId == gb.Where(w1 => w1.student_Id == gb.Key.student_Id).Max(max => max.BehaviorHistoryId)).Max(max => max.Score) * ShowMinusSign,
                          ScoreAlert = ScoreAlert
                      }).ToList();

            return models;

        }
    }
}