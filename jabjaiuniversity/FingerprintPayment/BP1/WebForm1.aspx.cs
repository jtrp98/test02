using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.BP1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {
                    var q_Year = (from a in entities.TTermTimeTables
                                  join b in entities.TTerms on a.nTerm equals b.nTerm
                                  join c in entities.TYears on b.nYear equals c.nYear
                                  group a by new { c.nYear, c.numberYear } into gb
                                  select new
                                  {
                                      numberYear = gb.Key.numberYear,
                                      nYear = gb.Key.nYear,
                                      TTermTimeTables = gb
                                  }).ToList();

                    var q_2 = (from a in entities.TPlanes select a).ToList();
                    var q_3 = (from a in entities.TSubLevels
                               join b in entities.TTermSubLevel2 on a.nTSubLevel equals b.nTSubLevel
                               where (b.nWorkingStatus ?? 0) == 1
                               select new
                               {
                                   a.nTSubLevel,
                                   a.SubLevel,
                                   b.nTermSubLevel2,
                                   b.nTSubLevel2
                               }).ToList();
                    var q_4 = entities.TTerms.ToList();
                    var q_5 = entities.TB_Subject.ToList();
                    var q_6 = entities.TPlanOwners.ToList();

                    List<TB_Course> courses = new List<TB_Course>();
                    int nCoursePlanID = 0, nCourseID = 0, nPlanId = 0;
                    foreach (var data in q_Year)
                    {
                        entities.TB_Course.Add(new TB_Course
                        {
                            sCourseName = "หลักสูตรปี " + data.numberYear,
                            dCourse_Create = DateTime.Now,
                            nYear = data.nYear,
                            nCourseID = ++nCourseID
                        });

                        foreach (var dataTerm in (from a in q_3
                                                      //join b in data.TTermTimeTables on a.nTermSubLevel2 equals b.nTermSubLevel2
                                                  group a by new { a.nTSubLevel, a.SubLevel } into gb
                                                  select new
                                                  {
                                                      nTSubLevel = gb.Key.nTSubLevel,
                                                      SubLevel = gb.Key.SubLevel,
                                                      TermSubLevel2 = gb
                                                  }))
                        {
                            entities.TPlans.Add(new TPlan
                            {
                                
                                PlanName = string.Format("หลักสูตรปี {0} แผน {1} ", data.numberYear, dataTerm.SubLevel),
                                nTSubLevel = dataTerm.nTSubLevel,
                                CreatedDate = DateTime.Now,
                                
                                //nCourseID = nCourseID,
                                //nPlanId = ++nPlanId
                            });

                            foreach (var dataLevel2 in dataTerm.TermSubLevel2)
                            {
                                entities.TB_CoursePlan_TTermSubLevel2.Add(new TB_CoursePlan_TTermSubLevel2
                                {
                                    dCourse_Create = DateTime.Now,
                                    nCourseID = nPlanId,
                                    nTermSubLevel2 = dataLevel2.nTermSubLevel2,
                                });
                            }
                            entities.SaveChanges();

                            foreach (var dataPlane in (from a in q_2
                                                       where a.nTSubLevel == dataTerm.nTSubLevel.ToString()
                                                       group a by new
                                                       {
                                                           a.nCredit,
                                                           a.sortNumber,
                                                           //a.sPlaneID,
                                                           a.sPlaneName,
                                                           a.cDel,
                                                           a.courseCode,
                                                           a.courseGroup,
                                                           a.courseHour,
                                                           //a.courseStatus,
                                                           a.courseTotalHour,
                                                           a.courseType
                                                       } into gb1
                                                       select new
                                                       {
                                                           gb1.Key.nCredit,
                                                           gb1.Key.sortNumber,
                                                           //gb1.Key.sPlaneID,
                                                           gb1.Key.sPlaneName,
                                                           gb1.Key.cDel,
                                                           gb1.Key.courseCode,
                                                           gb1.Key.courseGroup,
                                                           gb1.Key.courseHour,
                                                           //gb1.Key.courseStatus,
                                                           gb1.Key.courseTotalHour,
                                                           gb1.Key.courseType,
                                                           termId = gb1.Where(w => w.courseCode == gb1.Key.courseCode).ToList()
                                                       }))
                            {
                                var f_plan = q_5.FirstOrDefault(f => f.courseCode == dataPlane.courseCode && f.SubjectName == dataPlane.sPlaneName
                                && f.courseGroup == dataPlane.courseGroup && f.courseType == dataPlane.courseType);
                                string GuId = Guid.NewGuid().ToString();
                                entities.TB_CoursePlan.Add(new TB_CoursePlan
                                {
                                    cDel = dataPlane.cDel,
                                    courseCode = dataPlane.courseCode,
                                    courseGroup = dataPlane.courseGroup,
                                    courseHour = dataPlane.courseHour,
                                    //courseStatus = dataPlane.courseStatus,
                                    courseTotalHour = dataPlane.courseTotalHour,
                                    courseType = dataPlane.courseType,
                                    nCredit = dataPlane.nCredit,
                                    sortNumber = dataPlane.sortNumber,
                                    sPlaneName = dataPlane.sPlaneName,
                                    nPlanId = nPlanId,
                                    sPlaneID = f_plan.SubjectID.ToString(),
                                    id = GuId
                                });
                                entities.SaveChanges();

                                var f_CoursePlan = entities.TB_CoursePlan.FirstOrDefault(f => f.id == GuId);

                                var IPlaneID = dataPlane.termId.Select(s => s.sPlaneID).ToList();
                                foreach (var dataTeacher in q_6.Where(w => IPlaneID.Contains(w.sPlaneID)))
                                {
                                    entities.TB_CoursePlan_Teacher.Add(new TB_CoursePlan_Teacher
                                    {
                                        changeScoreAnytime = 0,
                                        nCourseID = f_CoursePlan.nCoursePlanID,
                                        TeacherId = dataTeacher.sEMP
                                    });
                                }

                                foreach (var dataTermId in dataPlane.termId)
                                {
                                    var f_Term = q_4.FirstOrDefault(f => f.sTerm == null && f.nYear == data.nYear);
                                    if (f_Term != null)
                                    {
                                        entities.TB_CoursePlan_Term.Add(new TB_CoursePlan_Term
                                        {
                                            Active = true,
                                            nCoursePlanID = f_CoursePlan.nCoursePlanID,
                                            nTerm = f_Term.nTerm
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities db = Connection.MasterEntities())
            {
                string sEntities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                var user_id = HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany)))
                {

                    var q_subject = entities.TB_Subject.ToList();
                    var max_Id = entities.TB_Subject.Select(s => s.SubjectID).DefaultIfEmpty(0).Max() + 1;
                    List<TB_Subject> tB_Subjects = new List<TB_Subject>();
                    foreach (var data in (from a in entities.TPlanes
                                          group a by new
                                          {
                                              a.sPlaneName,
                                              a.nCredit,
                                              a.courseCode,
                                              a.courseGroup,
                                              a.courseType,
                                              a.courseHour,
                                              a.courseTotalHour,
                                              a.nTSubLevel,
                                          } into gb
                                          select new
                                          {
                                              SubjectName = gb.Key.sPlaneName,
                                              nCredit = gb.Key.nCredit,
                                              courseCode = gb.Key.courseCode,
                                              courseGroup = gb.Key.courseGroup,
                                              courseType = gb.Key.courseType,
                                              courseHour = gb.Key.courseHour,
                                              courseTotalHour = gb.Key.courseTotalHour,
                                              nTSubLevel = gb.Key.nTSubLevel,
                                          }))
                    {
                        tB_Subjects.Add(new TB_Subject
                        {
                            SubjectName = data.SubjectName,
                            nCredit = data.nCredit,
                            courseCode = data.courseCode,
                            courseGroup = data.courseGroup,
                            courseType = data.courseType,
                            courseHour = data.courseHour,
                            courseTotalHour = data.courseTotalHour,
                            nTSubLevel = int.Parse(data.nTSubLevel),
                            Delete = false,
                            SubjectID = max_Id++
                        });

                    }

                    entities.TB_Subject.AddRange(tB_Subjects);
                    entities.SaveChanges();
                }
            }
        }
    }
}