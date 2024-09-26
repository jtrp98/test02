using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReportModels = FingerprintPayment.Report.Models.BehaviorsReports;
using FingerprintPayment.Report.Models;
using JabjaiMainClass;
using JabjaiMainClass.Models;

namespace FingerprintPayment.Report.Functions.BehaviorsReports
{
    public class reportsDetailType_01
    {
        public static ReportModels.DetailModels GetReports(JabJaiEntities dbschool, Search search)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }

            ReportModels.DetailModels models = new ReportModels.DetailModels();
            var setting = dbschool.TBehaviorSettings.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault();

            decimal maxScore = setting.MaxScore ?? 100;
            var studentData = new StudentLogic(dbschool);

            var f_setting = dbschool.TBehaviorSettings.FirstOrDefault(w => w.SchoolID == userData.CompanyID);
            int ShowMinusSign = f_setting == null ? 1 : ((f_setting.ShowMinusSign ?? 0) == 1 ? 1 : -1);

            var q1 = studentData.getById(search.student_id.Value, search.term_id, userData);

            models.student_name = q1.student_Name;
            models.roomName = q1.Class_Name + " / " + q1.Room_Name;
            models.student_Code = q1.student_Code;

            string DBName = "";
            StudentLogic logic = new StudentLogic(dbschool);

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

            List<TBehaviorHistory> q_HistoryData = new List<TBehaviorHistory>();

            string SQL = string.Format(@"SELECT * FROM [{0}].[dbo].[TBehaviorHistory] 
WHERE dAdd BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' AND  StudentId IN ({3}) AND ISNULL(cDel,0) = 0",
            "JabjaiSchoolSingleDB", search.dStart, search.dEnd, search.student_id);

            q_HistoryData = dbschool.Database.SqlQuery<TBehaviorHistory>(SQL).ToList();

            SQL = string.Format(@"SELECT * FROM [{0}].[dbo].[TBehaviorHistory] 
WHERE dAdd BETWEEN '{1:yyyyMMdd}' AND '{2:yyyyMMdd}' AND  StudentId IN ({3}) AND ISNULL(cDel,0) = 0",
             "JabjaiSchoolHistory", search.dStart, search.dEnd, search.student_id);

            q_HistoryData.AddRange(dbschool.Database.SqlQuery<TBehaviorHistory>(SQL).ToList());

            //var q_HistoryData = (from a in dbschool.TBehaviorHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
            //                     where q_Id.Contains(a.StudentId.Value) &&
            //                     search.dStart <= a.dAdd && search.dEnd >= a.dAdd
            //                     && a.dCanCel == null
            //                     select a).ToList();

            models.Details = (from a in q_HistoryData
                              join b in dbschool.TBehaviors.Where(w => w.SchoolID == userData.CompanyID) on a.BehaviorId equals b.BehaviorId into jab
                              from jb in jab.DefaultIfEmpty()

                              join c in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.UserAdd equals c.sEmp into jac
                              from jc in jac.DefaultIfEmpty()

                              from d in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == a.UserCancel).DefaultIfEmpty()

                              where q1.student_Id == a.StudentId.Value &&
                              search.dStart <= a.dAdd && search.dEnd >= a.dAdd
                              select new { a, jc, cancleBy = d?.sName + " " + d?.sLastname }).AsEnumerable().Select(
                              s => new ReportModels.DetailModels.Detail
                              {
                                  dateTime = s.a.dAdd,
                                  cancleDate = s.a.dCanCel,
                                  cancleBy = s.cancleBy,
                                  Name = s.a.BehaviorName,
                                  Note = s.a.Note ?? "",
                                  residualScore = string.Format("{0:0.##}", (s.a.ResidualScore ?? 0) + (s.a.dCanCel.HasValue ? (s.a.Type == "1" ? 1 * s.a.Score : -1 * s.a.Score) : 0) * ShowMinusSign),
                                  //residualScore = string.Format("{0:0.##}", (s.a.ResidualScore ?? 0) * ShowMinusSign),
                                  Score = string.Format("{0:0.##}", (s.a.Score ?? 0)),
                                  teacherName = s.jc == null ? "" : s.jc.sName + " " + s.jc.sLastname,
                                  Type = s.a.Type == "1" ? "ลด" : "เพิ่ม",
                                  Status = s.a.IsHolidayCancel == true ? "holiday" : (s.a.dCanCel.HasValue ? "delete" : ""),
                                  ID = s.a.BehaviorHistoryId
                              }).ToList();

            return models;
        }
    }
}