using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.grade
{
    /// <summary>
    /// Summary description for schoolReportExport11
    /// </summary>
    public class schoolReportExport11 : IHttpHandler, IRequiresSessionState
    {
        //[Inject]
        //public ICommonService CommonService { get; set; }
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            dynamic rss = new JObject();
            string idlv = fcommon.ReplaceInjection(context.Request["idlv"]);
            string idlv2 = fcommon.ReplaceInjection(context.Request["idlv2"]);
            string year = fcommon.ReplaceInjection(context.Request["year"]);
            string term = fcommon.ReplaceInjection(context.Request["term"]);
            int? idlv2n = Int32.Parse(idlv2);
            int idlvn = Int32.Parse(idlv);
            int? useryear = Int32.Parse(year);

           
            int? term2;
            if (term == "1")
                term2 = 2;
            else term2 = 1;

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            List<PlanList2> planlist = new List<PlanList2>();
            List<PlanList3> planlist3 = new List<PlanList3>();
            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, userData.CompanyID, userData.UserID);
            var planCourseWithTerm = ServiceHelper.GetPlanCoursesWithTerm(idlvn, (int)idlv2n, ((yearDTO != null) ? yearDTO.NYear : 0), userData.CompanyID, dbschool);

            var f_term = GetTerm(useryear, term, dbschool);
            var q_planes_term1 = planCourseWithTerm;
            var q_grades = _dbGrade.TGrades.Where(w => w.nTerm == f_term.nTerm.Trim() && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).ToList();
           
            var q_gradeId = q_grades.Select(s => s.nGradeId).ToList();
            var q_gradedetail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && q_gradeId.Contains(w.nGradeId)).ToList();
            q_gradedetail = (q_gradedetail != null) ? q_gradedetail.Where(w => w.cDel == false).ToList() : null;
            var f_term_term2 = GetTerm(useryear, term2.ToString(), dbschool);
            var q_grades_term2 = _dbGrade.TGrades.Where(w => w.nTerm == f_term_term2.nTerm.Trim() && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).ToList();
            var q_gradeId_term2 = q_grades_term2.Select(s => s.nGradeId).ToList();
            var q_gradedetail_term2 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && q_gradeId_term2.Contains(w.nGradeId)).ToList();
            q_gradedetail_term2 = (q_gradedetail_term2 != null) ? q_gradedetail_term2.Where(w => w.cDel == false).ToList() : null;
            var q_plane1 = planCourseWithTerm;
            var q_plane2 = planCourseWithTerm;
            var q_plane3 = planCourseWithTerm;

            var tCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            


            var f_term1 = GetTerm(useryear, "1", dbschool);
            var f_term2 = GetTerm(useryear, "2", dbschool);


            List<string> unique = getPlane(f_term1.nTerm, f_term2.nTerm, idlv2n);
            string[] planid = GetPlanLists(unique, q_planes_term1, q_grades, q_gradedetail, dbschool, false);

            var q_usermaster = _dbMaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
            var q_title = dbschool.TTitleLists.Where(w =>  w.SchoolID == userData.CompanyID).ToList();

            int countMaxScore = q_planes_term1.Count(c => planid.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));
            var students = dbschool.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1") && w.SchoolID == userData.CompanyID);

            string _commUser = "";
            if (students != null)
            {
                students.ToList().ForEach(f =>
                {
                    _commUser += (string.IsNullOrEmpty(_commUser) ? "" : ",") + "'" + f.sID + "'";
                });
            }
          
            

            foreach (var data in students)
            {
                var roomChangeCheck = dbschool.TRoomChanges.Where(w => w.sID == data.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
                if (TUserMaster != null && roomChangeCheck == null)
                {
                    
                    int countplan = 0;
                    
                    double g1;

                    foreach (var f_plan in planid)
                    {
                        var f_grades = q_grades.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan);
                        if (f_grades != null)
                        {

                            var detailView = q_gradedetail.Where(c => c.nGradeId == f_grades.nGradeId && c.sID == data.sID).FirstOrDefault();
                            var tgradeEdit = _dbGrade.TGradeEdits.Where(ge => ge.GradeDetailID == detailView.nGradeDetailId && ge.UseGradeSet == 1 && ge.SchoolID == userData.CompanyID).FirstOrDefault();

                            var isScoreExist = Common.CheckScoreExist(detailView);

                            if (isScoreExist || (detailView != null && detailView.getSpecial != "-1") || tgradeEdit != null || f_grades.nTermSubLevel2 == null)
                            {
                                double score_term1 = 0;
                                var result = Common.getGrade(q_gradedetail, q_planes_term1, f_grades, data.sID);
                                //var checkplan = dbschool.TPlanes.Where(w => w.sPlaneID == f_plan).FirstOrDefault();


                                if (result.score != null && result.score != "")
                                    score_term1 = double.Parse(result.score);

                                double score_term2 = 0;
                                var gradeid_term1 = q_plane1.Where(w => w.SPlaneId == f_grades.sPlaneID).FirstOrDefault();
                                var gradeid_term2 = q_plane2.Where(w => w.CourseName == gradeid_term1.CourseName && w.NTerm == f_term2.nTerm).FirstOrDefault();
                                if (gradeid_term2 != null)
                                {
                                    var f_grades_term2 = _dbGrade.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                    if (f_grades_term2 != null)
                                    {
                                        var result2 = Common.getGrade(q_gradedetail_term2, q_plane3, f_grades_term2, data.sID);
                                        bool g1n = double.TryParse(result2.score, out g1);
                                        if (g1n == true)
                                            score_term2 = g1;
                                    }
                                }



                                countplan = countplan + 1;


                                if (term == "1")
                                {
                                    PlanList3 plan3 = new PlanList3();

                                    plan3.planGroupNum = gradeid_term1.CourseGroup;
                                    plan3.planGroup = gradeid_term1.CourseGroupName;
                                    plan3.planName = gradeid_term1.CourseName;
                                    plan3.planType = dbschool.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == gradeid_term1.CourseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                                    plan3.studentcode = data.sStudentID;
                                    plan3.studentName = data.sName + " " + data.sLastname;
                                    plan3.term1score = score_term1.ToString();
                                    plan3.term2score = score_term2.ToString();
                                    planlist3.Add(plan3);
                                }
                                else
                                {
                                    PlanList3 plan3 = new PlanList3();
                                    plan3.planGroupNum = gradeid_term1.CourseGroup;
                                    plan3.planGroup = gradeid_term1.CourseGroupName;
                                    plan3.planName = gradeid_term1.CourseName;
                                    plan3.planType = dbschool.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == gradeid_term1.CourseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                                    plan3.studentcode = data.sStudentID;
                                    plan3.studentName = data.sName + " " + data.sLastname;
                                    plan3.term1score = score_term2.ToString();
                                    plan3.term2score = score_term1.ToString();
                                    planlist3.Add(plan3);

                                }
                            }
                            
                        }
                      
                    }                    
                }
                else if (roomChangeCheck != null)
                {
                    var f_term_alt = GetTerm(useryear, term, dbschool);
                    var q_planes_term1_alt = planCourseWithTerm;

                    var q_grades_alt = _dbGrade.TGrades.Where(w => w.nTerm == f_term_alt.nTerm.Trim() && w.nTermSubLevel2 == roomChangeCheck.Level2Old && w.SchoolID == userData.CompanyID).ToList();
                    var q_gradeId_alt = q_grades_alt.Select(s => s.nGradeId).ToList();
                    var q_gradedetail_alt = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && q_gradeId_alt.Contains(w.nGradeId)).ToList();
                    q_gradedetail_alt = (q_gradedetail_alt != null) ? q_gradedetail_alt.Where(w => w.cDel == false).ToList() : null;
                    var f_term_term2_alt = GetTerm(useryear, term2.ToString(), dbschool);
                    var q_grades_term2_alt = _dbGrade.TGrades.Where(w => w.nTerm == f_term_term2_alt.nTerm.Trim() && w.nTermSubLevel2 == roomChangeCheck.Level2Old && w.SchoolID == userData.CompanyID).ToList();
                    var q_gradeId_term2_alt = q_grades_term2_alt.Select(s => s.nGradeId).ToList();
                    var q_gradedetail_term2_alt = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && q_gradeId_term2_alt.Contains(w.nGradeId)).ToList();
                    q_gradedetail_term2_alt = (q_gradedetail_term2_alt != null) ? q_gradedetail_term2_alt.Where(w => w.cDel == false).ToList() : null;
                    var q_plane1_alt = planCourseWithTerm;
                    var q_plane2_alt = planCourseWithTerm;
                    var q_plane3_alt = planCourseWithTerm;


                    int countplan = 0;

                   
                    double g1;

                    foreach (var f_plan in planid)
                    {
                        var f_grades = q_grades_alt.FirstOrDefault(w => w.sPlaneID.ToString() == f_plan);
                        if (f_grades != null)
                        {
                           

                            var detailView = q_gradedetail.Where(c => c.nGradeId == f_grades.nGradeId && c.sID == data.sID).FirstOrDefault();
                            var tgradeEdit = _dbGrade.TGradeEdits.Where(ge => ge.GradeDetailID == detailView.nGradeDetailId && ge.UseGradeSet == 1 && ge.SchoolID == userData.CompanyID).FirstOrDefault();

                            var isScoreExist = Common.CheckScoreExist(detailView);

                            if (isScoreExist || (detailView != null && detailView.getSpecial != "-1") || tgradeEdit != null || f_grades.nTermSubLevel2 == null)
                            {
                                double score_term1 = 0;

                                var result = Common.getGrade(q_gradedetail, q_planes_term1_alt, f_grades, data.sID);
                                if (result.score != null && result.score != "" && result.score != "0")
                                {
                                    score_term1 = double.Parse(result.score);
                                }
                                else result = Common.getGrade(q_gradedetail_alt, q_planes_term1_alt, f_grades, data.sID);
                                if (result.score != null && result.score != "")
                                    score_term1 = double.Parse(result.score);
                                //var checkplan = dbschool.TPlanes.Where(w => w.sPlaneID == f_plan).FirstOrDefault();


                                double score_term2 = 0;
                                var gradeid_term1 = q_plane1_alt.Where(w => w.SPlaneId == f_grades.sPlaneID).FirstOrDefault();
                                var gradeid_term2 = q_plane2_alt.Where(w => w.SPlaneId == gradeid_term1.SPlaneId && w.NTerm == f_term_term2_alt.nTerm).FirstOrDefault();
                                if (gradeid_term2 != null)
                                {
                                    var f_grades_term2 = _dbGrade.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == roomChangeCheck.Level2Old && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                    if (f_grades_term2 != null)
                                    {
                                        var result2 = Common.getGrade(q_gradedetail_term2_alt, q_plane3_alt, f_grades_term2, data.sID);
                                        bool g1n = double.TryParse(result2.score, out g1);
                                        if (g1n == true)
                                            score_term2 = g1;
                                    }

                                    if (score_term2 == 0)
                                    {
                                        f_grades_term2 = _dbGrade.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == roomChangeCheck.Level2Old && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                        if (f_grades_term2 != null)
                                        {
                                            var result2 = Common.getGrade(q_gradedetail_term2, q_plane3, f_grades_term2, data.sID);
                                            bool g1n = double.TryParse(result2.score, out g1);
                                            if (g1n == true)
                                                score_term2 = g1;
                                        }
                                    }

                                    if (score_term2 == 0)
                                    {
                                        f_grades_term2 = _dbGrade.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == roomChangeCheck.Level2New && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                        if (f_grades_term2 != null)
                                        {
                                            var result2 = Common.getGrade(q_gradedetail_term2, q_plane3, f_grades_term2, data.sID);
                                            bool g1n = double.TryParse(result2.score, out g1);
                                            if (g1n == true)
                                                score_term2 = g1;
                                        }
                                    }

                                    if (score_term2 == 0)
                                    {
                                        f_grades_term2 = _dbGrade.TGrades.Where(w => w.sPlaneID == gradeid_term2.SPlaneId && w.nTermSubLevel2 == roomChangeCheck.Level2New && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                        if (f_grades_term2 != null)
                                        {
                                            var result2 = Common.getGrade(q_gradedetail_term2_alt, q_plane3_alt, f_grades_term2, data.sID);
                                            bool g1n = double.TryParse(result2.score, out g1);
                                            if (g1n == true)
                                                score_term2 = g1;
                                        }
                                    }
                                }

                                countplan = countplan + 1;


                                if (term == "1")
                                {
                                    PlanList3 plan3 = new PlanList3();
                                    plan3.planGroupNum = gradeid_term1.CourseGroup;
                                    plan3.planGroup = gradeid_term1.CourseGroupName;
                                    plan3.planName = gradeid_term1.CourseName;
                                    plan3.studentcode = data.sStudentID;
                                    plan3.studentName = data.sName + " " + data.sLastname;
                                    plan3.term1score = score_term1.ToString();
                                    plan3.term2score = score_term2.ToString();
                                    plan3.planType = dbschool.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == gradeid_term1.CourseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                                    planlist3.Add(plan3);
                                }
                                else
                                {

                                    PlanList3 plan3 = new PlanList3();
                                    plan3.planGroupNum = gradeid_term1.CourseGroup;
                                    plan3.planGroup = gradeid_term1.CourseGroupName;
                                    plan3.planName = gradeid_term1.CourseName;
                                    plan3.studentcode = data.sStudentID;
                                    plan3.studentName = data.sName + " " + data.sLastname;
                                    plan3.term1score = score_term2.ToString();
                                    plan3.term2score = score_term1.ToString();
                                    plan3.planType = dbschool.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == gradeid_term1.CourseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                                    planlist3.Add(plan3);
                                }
                            }
                        }
                    
                    }                    
                }
            }

            var newSortList2 = planlist3;
            newSortList2 = planlist3.OrderBy(x => x.planGroupNum).ThenBy(x => x.planType).ThenBy(x => x.planName).ToList();

            int ranking = -1;
            string planname2 = "";
            

            foreach (var item in newSortList2)
            {
                if (planname2 != item.planName)
                {
                    ranking++;
                    planname2 = item.planName;
                }
                
                item.index = ranking;
            }

            rss = new JArray(from a in newSortList2
                             select new JObject(

                   new JProperty("studentName", a.studentName),
                   new JProperty("planName", a.planName),
                   new JProperty("index", a.index),
                   new JProperty("term1score", a.term1score),
                   new JProperty("term2score", a.term2score),
                   new JProperty("planGroup", a.planGroup),
                   new JProperty("studentcode", a.studentcode)
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

        
        

        private double CalGrade(double? maxweight, double? sumweight)
        {
            double? sumall2 = Math.Round((maxweight == 0 ? 0 : (sumweight / maxweight)) ?? 0, 10, MidpointRounding.ToEven);
            return double.Parse(string.Format("{0:0.0000000000}", sumall2 ?? 0).Substring(0, 4));
        }

        private TTerm GetTerm(int? useryear, string term, JabJaiEntities dbschool)
        {
            return (from a in dbschool.TYears.Where(w =>  w.SchoolID == userData.CompanyID)
                    join b in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nYear equals b.nYear
                    where a.numberYear == useryear && b.sTerm == term && b.cDel == null
                    select b).FirstOrDefault();
        }

        private List<string> getPlane(string nterm1, string nterm2, int? idlv2n)
        {
            List<string> unique = new List<string>();
            //List<TGrade> gradelist = new List<TGrade>();
            //gradelist.AddRange((from a in dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID)
            //                    join b in dbschool.TGrades.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
            //                    where (b.nTerm == nterm1 || b.nTerm == nterm2) && a.nTermSubLevel2 == idlv2n
            //                    select
            //                        b
            //                 ).Distinct().ToList());
            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade()))
            {
                //gradelist.AddRange(();

                //foreach (var a in gradelist)
                //{
                //    if (a.sPlaneID != null)
                //        unique.Add(a.sPlaneID.ToString());
                //}

                //unique = unique.Distinct().ToList();

                unique = (from b in _dbGrade.TGrades
                          where b.SchoolID == userData.CompanyID && (b.nTerm == nterm1 || b.nTerm == nterm2) && b.nTermSubLevel2 == idlv2n
                          select b.sPlaneID.ToString()).Distinct().ToList();
            }

            return unique;
        }

        private string[] GetPlanLists(List<string> unique, List<PlanCourseDTO> q_planes, List<TGrade> q_grades, List<TGradeDetail> q_gradeDetails, JabJaiEntities dbschool, bool columns_all)
        {
            string[] planid = new string[26];
            sortList sort = new sortList();
            List<sortList> sortList = (from a in unique
                                       join b in q_planes on a equals b.SPlaneId.ToString() into jab
                                       from jb in jab.DefaultIfEmpty()

                                       select new sortList
                                       {
                                           sortnumberType = jb == null ? 10 : jb.CourseType ?? 10,
                                           sortnumberGroup = jb == null ? 10 : jb.CourseGroup ?? 10,
                                           planCode = jb == null ? "zzzz" : jb.CourseCode ?? "zzzz",
                                           planId = jb == null ? "" : jb.SPlaneId.ToString(),
                                           planName = jb == null ? "" : jb.CourseName,
                                       }).ToList();

            sortList = sortList.OrderBy(x => x.sortnumberGroup).ThenBy(x => x.sortnumberType).ThenBy(x => x.planCode).ToList();
            int count = 0;
            int register = 0;
            foreach (var ii in sortList)
            {
                
                var f_planes = q_planes.FirstOrDefault(w => w.SPlaneId.ToString() == ii.planId);
                if (f_planes != null)
                {
                    var data2 = q_grades.FirstOrDefault(w => w.sPlaneID.ToString() == ii.planId);
                    if (data2 != null)
                    {
                        if (!columns_all && q_gradeDetails.Count(c => string.IsNullOrEmpty(c.getScore100) && c.nGradeId == data2.nGradeId) == q_gradeDetails.Count(c => c.nGradeId == data2.nGradeId)) continue;
                        register += 1;
                    }
                    else if (!columns_all) continue;

                    if (Array.IndexOf(planid, f_planes.SPlaneId.ToString()) == -1)
                    {
                        count += 1;

                        if (count <= 26)
                            planid[count - 1] = f_planes.SPlaneId.ToString();
                    }
                }
            }



            return planid;
        }

        private PlanList2 getPlanList2(JabjaiEntity.DB.TUser data, List<TTitleList> q_title)
        {
            PlanList2 plan = new PlanList2();
            int nTitleid;
            int.TryParse(data.sStudentTitle, out nTitleid);
            var f_title = q_title.FirstOrDefault(f => f.titleDescription == data.sStudentTitle || f.nTitleid == nTitleid);
            plan.studentcode = data.sStudentID;
            plan.studentnumber = data.nStudentNumber ?? 0;
            plan.sId = data.sID;
            plan.name = (f_title == null ? "" : f_title.titleDescription) + " " + data.sName + " " + data.sLastname;

            int n;
            bool isNumeric = int.TryParse(data.sStudentID, out n);
            if (isNumeric == true)
            {
                plan.sort1int = Int32.Parse(data.sStudentID);
                plan.sort1txt = data.sStudentID;
                plan.sort2 = 999999;
            }
            else if (data.sStudentID == null || data.sStudentID == "")
            {
                plan.sort1int = 0;
                plan.sort1txt = "";
                plan.sort2 = 999999;
            }
            else
            {
                plan.sort1txt = data.sStudentID;
                plan.sort2 = 999999;
            }

            return plan;
        }


        private List<PlanList2> List_Sorting2(List<PlanList2> newSortList)
        {
            int ranking = 1;
            List<PlanList2> lists = new List<PlanList2>();
            var groupRanking = (from a in newSortList
                                group a.grade by a into ga
                                orderby ga.Key.grade descending
                                select new
                                {
                                    Grade = ga.Key.grade,
                                    Count = ga.Count()
                                }).ToList();

            foreach (var ranking_data in groupRanking.OrderByDescending(o => o.Grade))
            {
                foreach (var student_data in newSortList.Where(w => w.grade == ranking_data.Grade))
                {
                    student_data.ranking = groupRanking.Where(w => w.Grade > ranking_data.Grade).Sum(s => s.Count) + 1;
                }
            }
            int number = 1;
            newSortList = newSortList.OrderBy(o => o.studentnumber).ThenBy(o1 => o1.studentcode).ToList();
            newSortList.ForEach(f => f.number = number++);

            return newSortList;
        }

        
        protected class PlanList3
        {
            
            public int studentnumber { get; set; }
            public int sId { get; set; }
            public string studentcode { get; set; }
            public string studentName { get; set; }
            public string term1score { get; set; }
            public string term2score { get; set; }
            public string sum { get; set; }
            public string planName { get; set; }
            public string planGroup { get; set; }
            public int index { get; set; }
            public int? planGroupNum { get; set; }
            public int? planType { get; set; }

            public string PlanCourseCode { get; set; }
        }
        //protected class sortList
        //{
        //    public string planId { get; set; }
        //    public int? sortnumberType { get; set; }
        //    public int? sortnumberGroup { get; set; }
        //    public string planName { get; set; }
        //    public string planCode { get; set; }
        //}
        class Grades_Result
        {
            internal double sumall { get; set; }
            internal double maxweight { get; set; }
            internal double sumweight { get; set; }
            internal string score { get; set; }
            internal string label { get; set; }
            internal string grade { get; set; }
        }
        protected class PlanList2
        {
            public int ranking { get; set; }
            public int scoreRank { get; set; }
            public int studentnumber { get; set; }
            public int sId { get; set; }
            public string studentcode { get; set; }
            public int number { get; set; }
            public string name { get; set; }
            public string totalPlan { get; set; }
            public string registerPlan { get; set; }
            public string term1score1 { get; set; }
            public string term1score2 { get; set; }
            public string term1score3 { get; set; }
            public string term1score4 { get; set; }
            public string term1score5 { get; set; }
            public string term1score6 { get; set; }
            public string term1score7 { get; set; }
            public string term1score8 { get; set; }
            public string term1score9 { get; set; }
            public string term1score10 { get; set; }
            public string term1score11 { get; set; }
            public string term1score12 { get; set; }
            public string term1score13 { get; set; }
            public string term1score14 { get; set; }
            public string term1score15 { get; set; }
            public string term1score16 { get; set; }
            public string term1score17 { get; set; }
            public string term1score18 { get; set; }
            public string term1score19 { get; set; }
            public string term1score20 { get; set; }
            public string term1score21 { get; set; }
            public string term1score22 { get; set; }
            public string term1score23 { get; set; }
            public string term1score24 { get; set; }
            public string term1score25 { get; set; }
            public string term1score26 { get; set; }
            public string term2score1 { get; set; }
            public string term2score2 { get; set; }
            public string term2score3 { get; set; }
            public string term2score4 { get; set; }
            public string term2score5 { get; set; }
            public string term2score6 { get; set; }
            public string term2score7 { get; set; }
            public string term2score8 { get; set; }
            public string term2score9 { get; set; }
            public string term2score10 { get; set; }
            public string term2score11 { get; set; }
            public string term2score12 { get; set; }
            public string term2score13 { get; set; }
            public string term2score14 { get; set; }
            public string term2score15 { get; set; }
            public string term2score16 { get; set; }
            public string term2score17 { get; set; }
            public string term2score18 { get; set; }
            public string term2score19 { get; set; }
            public string term2score20 { get; set; }
            public string term2score21 { get; set; }
            public string term2score22 { get; set; }
            public string term2score23 { get; set; }
            public string term2score24 { get; set; }
            public string term2score25 { get; set; }
            public string term2score26 { get; set; }

            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public int? sort2 { get; set; }

            public int scoreall { get; set; }
            public double scoreget { get; set; }
            public double? maxweight { get; set; }
            public double? sumweight { get; set; }
            public double grade { get; set; }
            public string sum1 { get; set; }
            public string sum2 { get; set; }
            public string sum3 { get; set; }
            public string sum4 { get; set; }
            public string sum5 { get; set; }
            public string sum6 { get; set; }
            public string sum7 { get; set; }
            public string sum8 { get; set; }
            public string sum9 { get; set; }
            public string sum10 { get; set; }
            public string sum11 { get; set; }
            public string sum12 { get; set; }
            public string sum13 { get; set; }
            public string sum14 { get; set; }
            public string sum15 { get; set; }
            public string sum16 { get; set; }
            public string sum17 { get; set; }
            public string sum18 { get; set; }
            public string sum19 { get; set; }
            public string sum20 { get; set; }
            public string sum21 { get; set; }
            public string sum22 { get; set; }
            public string sum23 { get; set; }
            public string sum24 { get; set; }
            public string sum25 { get; set; }
            public string sum26 { get; set; }

            public string max1 { get; set; }
            public string max2 { get; set; }
            public string max3 { get; set; }
            public string max4 { get; set; }
            public string max5 { get; set; }
            public string max6 { get; set; }
            public string max7 { get; set; }
            public string max8 { get; set; }
            public string max9 { get; set; }
            public string max10 { get; set; }
            public string max11 { get; set; }
            public string max12 { get; set; }
            public string max13 { get; set; }
            public string max14 { get; set; }
            public string max15 { get; set; }
            public string max16 { get; set; }
            public string max17 { get; set; }
            public string max18 { get; set; }
            public string max19 { get; set; }
            public string max20 { get; set; }
            public string max21 { get; set; }
            public string max22 { get; set; }
            public string max23 { get; set; }
            public string max24 { get; set; }
            public string max25 { get; set; }
            public string max26 { get; set; }

            public string grade1 { get; set; }
            public string grade2 { get; set; }
            public string grade3 { get; set; }
            public string grade4 { get; set; }
            public string grade5 { get; set; }
            public string grade6 { get; set; }
            public string grade7 { get; set; }
            public string grade8 { get; set; }
            public string grade9 { get; set; }
            public string grade10 { get; set; }
            public string grade11 { get; set; }
            public string grade12 { get; set; }
            public string grade13 { get; set; }
            public string grade14 { get; set; }
            public string grade15 { get; set; }
            public string grade16 { get; set; }
            public string grade17 { get; set; }
            public string grade18 { get; set; }
            public string grade19 { get; set; }
            public string grade20 { get; set; }
            public string grade21 { get; set; }
            public string grade22 { get; set; }
            public string grade23 { get; set; }
            public string grade24 { get; set; }
            public string grade25 { get; set; }
            public string grade26 { get; set; }
            public string sumall { get; set; }
            public string score100get { get; set; }
            public string gradeget { get; set; }
            public double gradegetdouble { get; set; }
            public int? sortnumberType { get; set; }
            public int? sortnumberGroup { get; set; }
        }
    }


}