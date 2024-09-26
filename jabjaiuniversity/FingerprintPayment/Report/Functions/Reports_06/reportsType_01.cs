using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FingerprintPayment.Report.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiMainClass.Models;

namespace FingerprintPayment.Report.Functions.Reports_06
{
    public class ReportsType_01
    {
        public static List<ReportsData_06Models> GetReports(JabJaiEntities dbschool, Search search, JWTToken.userData userData)
        {
            List<ReportsData_06Models> reports = new List<ReportsData_06Models>();

            List<StudentLogTimeModel> models = new List<StudentLogTimeModel>();
            var q_trem = dbschool.TTerms.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList();
            StudentLogTime studentLogTime = new StudentLogTime(dbschool, userData);
            TimeSpan LogTimes = new TimeSpan(0, 0, 0);
            //studentLogTime = StudentLogTime(dbschool)
            StudentLogic studentLogic = new StudentLogic(dbschool);

            if (search.level2_id.HasValue) models = studentLogTime.GetLog4Rooms(search.level2_id.Value, search.dStart.Value, search.dEnd.Value);
            else if (search.level_id.HasValue) models = studentLogTime.GetLog4Class(search.level_id.Value, search.dStart.Value, search.dEnd.Value);
            else if (search.student_id.HasValue) models = studentLogTime.GetLog4Student(search.student_id.Value, search.dStart.Value, search.dEnd.Value);
            else models = studentLogTime.GetLogAll(search.dStart.Value, search.dEnd.Value);

            var q = (from a in models
                     join b in dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID)
                     on a.Room_Id equals b.nTermSubLevel2

                     where a != null && (b.nWorkingStatus ?? 0) == 1
                     group a by new
                     {
                         a.Room_Id,
                         a.Room_Name,
                         a.LogDate,
                         a.Class_Id,
                         a.Class_Name,
                     } into gb
                     select new
                     {
                         level_Id = gb.Key.Class_Id,
                         level2_Id = gb.Key.Room_Id,
                         level_name = gb.Key.Class_Name,
                         level2_name = gb.Key.Room_Name,
                         LogDate = gb.Key.LogDate,
                         count_student = gb.Count(),
                         //count_0 = gb.Count(c => c.LogTimes.HasValue && c.LogTimes != LogTimes),
                         //count_1 = gb.Count(c => c.LogStatus == "99" || c.LogTimes.Equals(LogTimes)),
                         count_0 = gb.Count(c => c.LogStatus != "99"),
                         count_1 = gb.Count(c => c.LogStatus == "99"),
                     }).ToList();

            var q_1 = (from a in q
                       select new
                       {
                           a.level2_Id,
                           level2_order = setValuesOrder(a.level2_name),
                           a.level_Id,
                           a.level_name,
                           a.level2_name,
                           a.LogDate,
                           a.count_student,
                           a.count_0,
                           a.count_1,
                           trem_Id = studentLogic.GetTermDATA(a.LogDate ?? DateTime.Now, q_trem).nTerm
                       }).ToList();

            var q_title = dbschool.TTitleLists.Where(w => w.SchoolID == userData.CompanyID).Select(s => new
            {
                nTitleid = s.nTitleid + "",
                s.titleDescription
            }).ToList();

            var q_employees = (from a in dbschool.TEmployees.Where(w => w.SchoolID == userData.CompanyID)
                               .Select(s => new
                               {
                                   s.sName,
                                   s.sLastname,
                                   s.sTitle,
                                   s.sEmp,
                               }).ToList()
                               join b in q_title on a.sTitle equals b.nTitleid into jab

                               from jb in jab.DefaultIfEmpty()

                               select new
                               {
                                   a.sName,
                                   a.sLastname,
                                   Description = jb == null ? a.sTitle : jb.titleDescription,
                                   a.sEmp
                               }).ToList();

            var q_classMember = (from a in dbschool.TClassMembers.Where(w => w.SchoolID == userData.CompanyID)
                                 join e in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals e.nTerm
                                 select new
                                 {
                                     a.nTeacherAssistOne,
                                     a.nTeacherAssistTwo,
                                     a.nTeacherHeadid,
                                     e.dEnd,
                                     e.dStart,
                                     a.nTermSubLevel2,
                                     a.nTerm
                                 }).ToList();

            var q_theacher = (from a in q_classMember

                              join b in q_employees on a.nTeacherHeadid equals b.sEmp into jab
                              from jb in jab.DefaultIfEmpty()

                              join c in q_employees on a.nTeacherAssistOne equals c.sEmp into jac
                              from jc in jac.DefaultIfEmpty()

                              join d in q_employees on a.nTeacherAssistTwo equals d.sEmp into jad
                              from jd in jad.DefaultIfEmpty()

                              where a.dStart <= search.dStart && a.dEnd >= search.dEnd
                              select new
                              {
                                  a.nTermSubLevel2,
                                  a.nTerm,
                                  head = jb != null ? jb.Description + " " + jb.sName + "  " + jb.sLastname : "",
                                  assistOne = jc != null ? jc.Description + " " + jc.sName + "  " + jc.sLastname : "",
                                  assistTwo = jd != null ? jd.Description + " " + jd.sName + "  " + jd.sLastname : "",
                                  a.dStart,
                                  a.dEnd
                              }).ToList();

            reports = (from a in q_1
                       join b in q_theacher on new { a.level2_Id, a.trem_Id }
                       equals new { level2_Id = b.nTermSubLevel2, trem_Id = b.nTerm } into jab

                       from jb in jab.DefaultIfEmpty()

                       where a.count_student <= a.count_1
                       orderby a.level_name, a.level2_name
                       group a by new
                       {
                           assistOne = jb == null ? "" : jb.assistOne,
                           assistTwo = jb == null ? "" : jb.assistTwo,
                           head = jb == null ? "" : jb.head,
                           a.level2_name,
                           a.level_name,
                           a.level2_Id,
                           a.level2_order,
                           a.level_Id
                       } into gbb
                       orderby gbb.Key.level_Id, gbb.Key.level2_order
                       select new ReportsData_06Models
                       {
                           dayCount = gbb.Count(),
                           roomName = gbb.Key.level_name + " / " + gbb.Key.level2_name,
                           roomId = gbb.Key.level2_Id,
                           teacherAssistOne = gbb.Key.assistOne,
                           teacherAssistTwo = gbb.Key.assistTwo,
                           teacherHead = gbb.Key.head,
                           d = gbb.Select(s => s.LogDate.Value.ToString("dd/MM/yyyy")).ToList()
                       }).ToList();

            return reports;
        }

        private static string getTrem(List<TTerm> q_trem, DateTime? LogDate)
        {
            var f = q_trem.FirstOrDefault(f1 => f1.dStart <= LogDate && f1.dEnd >= LogDate);

            if (f == null) return "";
            else return f.nTerm;
        }

        private static string setValuesOrder(string level_name)
        {
            int level = 0;
            if (int.TryParse(level_name, out level)) return string.Format("{0:000}", level);
            else return level_name;
        }
    }
}