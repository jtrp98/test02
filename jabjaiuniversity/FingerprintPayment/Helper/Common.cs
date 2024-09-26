using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json.Linq;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace FingerprintPayment.Helper
{
    public static class Common
    {
        public static PlanList2 GetPlanList2(JabjaiEntity.DB.TB_StudentViews data)
        {
            PlanList2 plan = new PlanList2();
            int nTitleid;
            int.TryParse(data.sStudentTitle, out nTitleid);
            //var f_title = q_title.FirstOrDefault(f => f.titleDescription == data.sStudentTitle || f.nTitleid == nTitleid);
            plan.studentcode = data.sStudentID;
            plan.studentnumber = data.nStudentNumber ?? 0;
            plan.sId = data.sID;
            plan.name = (data.titleDescription == null ? "" : data.titleDescription) + "" + data.sName + " " + data.sLastname;

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
            plan.nStudentStatus = data.nStudentStatus ?? 0;
            plan.DayQuit = data.MoveOutDate;
            return plan;
        }
        //public static string[] GetPlanLists(List<string> unique, List<PlanCourseDTO> planCourseDTOs, List<TGrade> q_grades, List<TGradeDetail> q_gradeDetails, bool columns_all, bool isRequestForCurrentAcademicYear, IMapper mapper, int schoolId)
        //{
        //    string[] planid = new string[26];
        //    sortList sort = new sortList();

        //    q_gradeDetails = new List<TGradeDetail>();

        //    List<sortList> sortList = (from a in unique
        //                               join b in planCourseDTOs on a equals b.SPlaneId.ToString() into jab
        //                               from jb in jab.DefaultIfEmpty()

        //                               select new sortList
        //                               {
        //                                   sortnumberType = jb == null ? 10 : jb.NOrder,
        //                                   sortnumberGroup = jb == null ? 10 : jb.CourseGroup ?? 10,
        //                                   planCode = jb == null ? "zzzz" : jb.CourseCode ?? "zzzz",
        //                                   planId = jb == null ? "" : jb.SPlaneId.ToString(),
        //                                   planName = jb == null ? "" : jb.CourseName,
        //                               }).ToList();

        //    sortList = sortList.OrderBy(x => x.sortnumberGroup).ThenBy(x => x.sortnumberType).ThenBy(x => x.planCode).ToList();
        //    int count = 0;
        //    int register = 0;
        //    //using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    //{


        //    foreach (var ii in sortList)
        //    {
        //        var f_planes = planCourseDTOs.FirstOrDefault(w => w.SPlaneId.ToString() == ii.planId);
        //        if (f_planes != null)
        //        {
        //            var data2 = q_grades.FirstOrDefault(w => w.sPlaneID.ToString() == ii.planId);
        //            if (data2 != null)
        //            {
        //                q_gradeDetails = ServiceHelper.GetTGradeDetailInfo(schoolId, data2.nGradeId, isRequestForCurrentAcademicYear, mapper);
        //                q_gradeDetails = q_gradeDetails.Where(w => (data2.nTermSubLevel2 == null && (!string.IsNullOrEmpty(w.getGradeLabel) || !string.IsNullOrEmpty(w.getScore100))) || !((string.IsNullOrEmpty(w.getScore100) || w.getScore100 == "0") && w.getSpecial == "-1" && string.IsNullOrEmpty(w.scoreFinalTerm) && string.IsNullOrEmpty(w.scoreMidTerm))).ToList();

        //                if (q_gradeDetails != null && q_gradeDetails.Count == 0 && q_grades.Where(g => g.nGradeId != data2.nGradeId && g.sPlaneID.ToString() == ii.planId).Count() > 0)
        //                {
        //                    data2 = q_grades.FirstOrDefault(g => g.nGradeId != data2.nGradeId && g.sPlaneID.ToString() == ii.planId);
        //                    q_gradeDetails = ServiceHelper.GetTGradeDetailInfo(schoolId, data2.nGradeId, isRequestForCurrentAcademicYear, mapper);
        //                    q_gradeDetails = q_gradeDetails.Where(w => (data2.nTermSubLevel2 == null && (!string.IsNullOrEmpty(w.getGradeLabel) || !string.IsNullOrEmpty(w.getScore100))) || !((string.IsNullOrEmpty(w.getScore100) || w.getScore100 == "0") && w.getSpecial == "-1" && string.IsNullOrEmpty(w.scoreFinalTerm) && string.IsNullOrEmpty(w.scoreMidTerm))).ToList();
        //                }
        //                //q_gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data2.nGradeId && !((string.IsNullOrEmpty(w.getScore100) || w.getScore100 == "0") && w.getSpecial == "-1" && string.IsNullOrEmpty(w.scoreFinalTerm) && string.IsNullOrEmpty(w.scoreMidTerm))).ToList();
        //                if (!columns_all && q_gradeDetails.Count(c => ((string.IsNullOrEmpty(c.getScore100) && data2.nTermSubLevel2 != null) || (data2.nTermSubLevel2 == null && (string.IsNullOrEmpty(c.getScore100) && string.IsNullOrEmpty(c.getGradeLabel)))) && c.nGradeId == data2.nGradeId) == q_gradeDetails.Count(c => c.nGradeId == data2.nGradeId)) continue;
        //                register += 1;
        //            }
        //            else if (!columns_all) continue;

        //            //ContentPlaceHolder cph = (ContentPlaceHolder)this.Master.FindControl("MainContent");

        //            if (Array.IndexOf(planid, f_planes.SPlaneId.ToString()) == -1)
        //            {
        //                count += 1;
        //                if (count <= 26)
        //                    planid[count - 1] = f_planes.SPlaneId.ToString();
        //            }
        //        }
        //    }

        //    //}
        //    planid = planid.Distinct().ToArray();

        //    return planid;
        //}

        //public static Grades_Result GetGradeWeight(TGradeEdit tgradeEdit, double scoreTerm1,
        //    double scoreTerm2, string getSpecialTerm1, string getSpecialTerm2, double? NCredit, string gradeDecimal, string getGradeLabel = "")
        //{


        //    double sum = 0;
        //    if (!string.IsNullOrEmpty(getSpecialTerm1) && !string.IsNullOrEmpty(getSpecialTerm2))
        //    {
        //        sum = scoreTerm1 + scoreTerm2;
        //    }
        //    else if (string.IsNullOrEmpty(getSpecialTerm1))
        //    {
        //        sum = scoreTerm2;
        //    }
        //    else if (string.IsNullOrEmpty(getSpecialTerm2))
        //    {
        //        sum = scoreTerm1;
        //    }


        //    //if (scoreTerm1 != 0 && scoreTerm2 != 0) //SB-6320
        //    if (!string.IsNullOrEmpty(getSpecialTerm1) && !string.IsNullOrEmpty(getSpecialTerm2))
        //    {
        //        sum = sum / 2;
        //    }


        //    double sumint = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);

        //    //double sumweight = 0;
        //    Grades_Result gradeResult = new Grades_Result();

        //    if (tgradeEdit != null)
        //    {
        //        if (tgradeEdit.GradeSet.ToString() == "4.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 4);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "3.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 3.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "3.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 3);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "2.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 2.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "2.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 2);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "1.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 1.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "1.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 1);
        //        }
        //        else
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 0);
        //        }
        //    }
        //    else if ((scoreTerm1 != 0 && getSpecialTerm1 == "-1") || (scoreTerm2 != 0 && getSpecialTerm2 == "-1") || (scoreTerm1 == 0 && getSpecialTerm1 == "-1" && scoreTerm2 == 0 && getSpecialTerm2 == "-1" && !string.IsNullOrEmpty(getGradeLabel)))
        //    {
        //        if (sumint > 79.99 || (sumint == 0 && getGradeLabel == "4.0"))
        //        {
        //            gradeResult.getGradeLabel = "4.0";
        //            gradeResult.sumweight = ((double)NCredit * 4);
        //        }
        //        else if (sumint > 74.99 || (sumint == 0 && getGradeLabel == "3.5"))
        //        {
        //            gradeResult.getGradeLabel = "3.5";
        //            gradeResult.sumweight = ((double)NCredit * 3.5);
        //        }
        //        else if (sumint > 69.99 || (sumint == 0 && getGradeLabel == "3.0"))
        //        {
        //            gradeResult.getGradeLabel = "3.0";
        //            gradeResult.sumweight = ((double)NCredit * 3);
        //        }
        //        else if (sumint > 64.99 || (sumint == 0 && getGradeLabel == "2.5"))
        //        {
        //            gradeResult.getGradeLabel = "2.5";
        //            gradeResult.sumweight = ((double)NCredit * 2.5);
        //        }
        //        else if (sumint > 59.99 || (sumint == 0 && getGradeLabel == "2.0"))
        //        {
        //            gradeResult.getGradeLabel = "2.0";
        //            gradeResult.sumweight = ((double)NCredit * 2);
        //        }
        //        else if (sumint > 54.99 || (sumint == 0 && getGradeLabel == "1.5"))
        //        {
        //            gradeResult.getGradeLabel = "1.5";
        //            gradeResult.sumweight = ((double)NCredit * 1.5);
        //        }
        //        else if (sumint > 49.99 || (sumint == 0 && getGradeLabel == "1.0"))
        //        {
        //            gradeResult.getGradeLabel = "1.0";
        //            gradeResult.sumweight = ((double)NCredit * 1);
        //        }
        //        else
        //        {
        //            gradeResult.getGradeLabel = "0";
        //        }
        //    }
        //    else if (getSpecialTerm1 == "4" || getSpecialTerm1 == "10" || getSpecialTerm2 == "4" || getSpecialTerm2 == "10")
        //    {
        //        gradeResult.sumweight = ((double)NCredit * 4);
        //        gradeResult.getGradeLabel = GetGradeLabel(sumint);
        //    }
        //    else if (getSpecialTerm1 == "11" || getSpecialTerm2 == "11")
        //    {
        //        gradeResult.sumweight = ((double)NCredit * 3);
        //        gradeResult.getGradeLabel = GetGradeLabel(sumint);
        //    }
        //    else if (getSpecialTerm1 == "12" || getSpecialTerm2 == "12")
        //    {
        //        gradeResult.sumweight = ((double)NCredit * 2);
        //        gradeResult.getGradeLabel = GetGradeLabel(sumint);
        //    }
        //    else if (getSpecialTerm1 == "13" || getSpecialTerm2 == "13")
        //    {
        //        gradeResult.sumweight = ((double)NCredit * 1);
        //        gradeResult.getGradeLabel = GetGradeLabel(sumint);
        //    }

        //    return gradeResult;
        //}
        public static string GetGradeLabel(double sumint)
        {
            string gradeLabel = string.Empty;
            if (sumint > 79.99)
            {
                gradeLabel = "4.0";
            }
            else if (sumint > 74.99)
            {
                gradeLabel = "3.5";
            }
            else if (sumint > 69.99)
            {
                gradeLabel = "3.0";
            }
            else if (sumint > 64.99)
            {
                gradeLabel = "2.5";
            }
            else if (sumint > 59.99)
            {
                gradeLabel = "2.0";
            }
            else if (sumint > 54.99)
            {
                gradeLabel = "1.5";
            }
            else if (sumint > 49.99)
            {
                gradeLabel = "1.0";
            }
            else
            {
                gradeLabel = "0";
            }

            return gradeLabel;
        }
        //public static Grades_Result GetGradeWeightHistory(GradeHistoryEntity.TGradeEditsHistory tgradeEdit, double scoreTerm1,
        //   double scoreTerm2, string getSpecialTerm1, string getSpecialTerm2, double? NCredit)
        //{
        //    double sum = scoreTerm1 + scoreTerm2;
        //    if (scoreTerm1 != 0 && scoreTerm2 != 0)
        //        sum = sum / 2;
        //    int sumint = (int)Math.Round(sum, MidpointRounding.AwayFromZero);

        //    //double sumweight = 0;
        //    Grades_Result gradeResult = new Grades_Result();

        //    if (tgradeEdit != null)
        //    {
        //        if (tgradeEdit.GradeSet.ToString() == "4.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 4);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "3.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 3.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "3.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 3);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "2.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 2.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "2.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 2);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "1.50")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 1.5);
        //        }
        //        else if (tgradeEdit.GradeSet.ToString() == "1.00")
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 1);
        //        }
        //        else
        //        {
        //            gradeResult.sumweight = ((double)NCredit * 0);
        //        }
        //    }
        //    else if ((scoreTerm1 != 0 && getSpecialTerm1 == "-1") || (scoreTerm2 != 0 && getSpecialTerm2 == "-1"))
        //    {
        //        if (sumint > 79.99)
        //        {
        //            gradeResult.getGradeLabel = "4.0";
        //            gradeResult.sumweight = ((double)NCredit * 4);
        //        }
        //        else if (sumint > 74.99)
        //        {
        //            gradeResult.getGradeLabel = "3.5";
        //            gradeResult.sumweight = ((double)NCredit * 3.5);
        //        }
        //        else if (sumint > 69.99)
        //        {
        //            gradeResult.getGradeLabel = "3.0";
        //            gradeResult.sumweight = ((double)NCredit * 3);
        //        }
        //        else if (sumint > 64.99)
        //        {
        //            gradeResult.getGradeLabel = "2.5";
        //            gradeResult.sumweight = ((double)NCredit * 2.5);
        //        }
        //        else if (sumint > 59.99)
        //        {
        //            gradeResult.getGradeLabel = "2.0";
        //            gradeResult.sumweight = ((double)NCredit * 2);
        //        }
        //        else if (sumint > 54.99)
        //        {
        //            gradeResult.getGradeLabel = "1.5";
        //            gradeResult.sumweight = ((double)NCredit * 1.5);
        //        }
        //        else if (sumint > 49.99)
        //        {
        //            gradeResult.getGradeLabel = "1.0";
        //            gradeResult.sumweight = ((double)NCredit * 1);
        //        }
        //        else
        //        {
        //            gradeResult.getGradeLabel = "0";
        //        }
        //    }
        //    else if (getSpecialTerm1 == "4" || getSpecialTerm1 == "10" || getSpecialTerm2 == "4" || getSpecialTerm2 == "10")
        //        gradeResult.sumweight = ((double)NCredit * 4);
        //    else if (getSpecialTerm1 == "11" || getSpecialTerm2 == "11")
        //        gradeResult.sumweight = ((double)NCredit * 3);
        //    else if (getSpecialTerm1 == "12" || getSpecialTerm2 == "12")
        //        gradeResult.sumweight = ((double)NCredit * 2);
        //    else if (getSpecialTerm1 == "13" || getSpecialTerm2 == "13")
        //        gradeResult.sumweight = ((double)NCredit * 1);

        //    return gradeResult;
        //}
        //public static Grades_Result getGradeNew(List<TGradeDetail> q_gradedetail, List<PlanCourseDTO> planCourseDTOs, int sPlaneID, int student_id)
        //{
        //    Grades_Result grades_Result = new Grades_Result();
        //    var f_gradedetail = new TGradeDetail();
        //    f_gradedetail = (q_gradedetail != null) ? q_gradedetail.FirstOrDefault(f => f.sID == student_id) : null;

        //    if (f_gradedetail != null && f_gradedetail.getScore100 != null)
        //    {
        //        var f_plane = planCourseDTOs.FirstOrDefault(w => w.SPlaneId == sPlaneID);
        //        string get100 = f_gradedetail.getScore100;
        //        if (f_plane.NCredit != null && (f_plane.CourseGroupName == "รายวิชาเพิ่มเติม" || f_plane.CourseGroupName == "รายวิชาพื้นฐาน"))
        //        {
        //            double get100n = Double.Parse(get100);
        //            grades_Result.sumall = get100n;
        //            grades_Result.maxweight = (f_plane.NCredit ?? 0);

        //            if (f_gradedetail.getSpecial == "-1")
        //            {
        //                //if (gradeEdit != null && gradeEdit.UseGradeSet == 1)
        //                //{

        //                //    if (gradeEdit.GradeSet == decimal.Parse("4.00"))
        //                //        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.50"))
        //                //        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.00"))
        //                //        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.50"))
        //                //        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.00"))
        //                //        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.50"))
        //                //        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.00"))
        //                //        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                //    else
        //                //        grades_Result.sumweight = 0;
        //                //}
        //                //else
        //                //{
        //                if (get100n >= 80)
        //                    grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 75)
        //                    grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 70)
        //                    grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 65)
        //                    grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 60)
        //                    grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 55)
        //                    grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 50)
        //                    grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                else
        //                    grades_Result.sumweight = 0;
        //                //}
        //            }
        //            else if (f_gradedetail.getSpecial == "4")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "10")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "11")
        //            {
        //                grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "12")
        //            {
        //                grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "13")
        //            {
        //                grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //            }
        //            else grades_Result.sumweight = 0;

        //        }
        //        grades_Result.planId =
        //        grades_Result.score = get100;

        //        grades_Result.getSpecial = f_gradedetail.getSpecial;
        //        grades_Result.getGradeLabel = f_gradedetail.getGradeLabel;

        //        grades_Result.label = get100 + "\\" + getSpecial2(f_gradedetail.getSpecial, get100, null);
        //        grades_Result.grade = getSpecial2(f_gradedetail.getSpecial, get100, null);
        //    }

        //    return grades_Result;
        //}

        public static Grades_Result getGrade(StudentGradeInfoDTO f_gradedetail, PlanCourseDTO f_plane, int student_id)
        {
            Grades_Result grades_Result = new Grades_Result();
            //var f_gradedetail = new TGradeDetail();
            //f_gradedetail = (q_gradedetail != null) ? q_gradedetail.FirstOrDefault(f => f.nGradeId == f_grades.nGradeId && f.sID == student_id) : null;

            if (f_gradedetail != null && f_gradedetail.getScore100 != null)
            {
                //var f_plane = planCourseDTOs.FirstOrDefault(w => w.SPlaneId == f_grades.sPlaneID);
                string get100 = f_gradedetail.getScore100;
                if (f_plane.NCredit != null && (f_plane.CourseGroupName == "รายวิชาเพิ่มเติม" || f_plane.CourseGroupName == "รายวิชาพื้นฐาน"))
                {
                    double get100n = Double.Parse(get100);
                    grades_Result.sumall = get100n;
                    grades_Result.maxweight = (f_plane.NCredit ?? 0);

                    if (f_gradedetail.getSpecial == "-1")
                    {
                        //if (gradeEdit != null && gradeEdit.UseGradeSet == 1)
                        //{

                        //    if (gradeEdit.GradeSet == decimal.Parse("4.00"))
                        //        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("3.50"))
                        //        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("3.00"))
                        //        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("2.50"))
                        //        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("2.00"))
                        //        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("1.50"))
                        //        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
                        //    else if (gradeEdit.GradeSet == decimal.Parse("1.00"))
                        //        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
                        //    else
                        //        grades_Result.sumweight = 0;
                        //}
                        //else
                        //{
                        if (get100n >= 80)
                            grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 75)
                            grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 70)
                            grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 65)
                            grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 60)
                            grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 55)
                            grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
                        else if (get100n >= 50)
                            grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
                        else
                            grades_Result.sumweight = 0;
                        //}
                    }
                    else if (f_gradedetail.getSpecial == "4")
                    {
                        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
                    }
                    else if (f_gradedetail.getSpecial == "10")
                    {
                        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
                    }
                    else if (f_gradedetail.getSpecial == "11")
                    {
                        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
                    }
                    else if (f_gradedetail.getSpecial == "12")
                    {
                        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
                    }
                    else if (f_gradedetail.getSpecial == "13")
                    {
                        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
                    }
                    else grades_Result.sumweight = 0;

                }
                grades_Result.planId =
                grades_Result.score = get100;

                grades_Result.getSpecial = f_gradedetail.getSpecial;
                grades_Result.getGradeLabel = f_gradedetail.getGradeLabel;

                grades_Result.label = get100 + "\\" + getSpecial2(f_gradedetail.getSpecial, get100, null);
                grades_Result.grade = getSpecial2(f_gradedetail.getSpecial, get100, null);
            }

            return grades_Result;
        }
        //public static Grades_Result getGrade(List<TGradeDetail> q_gradedetail, List<PlanCourseDTO> planCourseDTOs, TGrade f_grades, int student_id)
        //{
        //    Grades_Result grades_Result = new Grades_Result();
        //    var f_gradedetail = new TGradeDetail();
        //    f_gradedetail = (q_gradedetail != null) ? q_gradedetail.FirstOrDefault(f => f.nGradeId == f_grades.nGradeId && f.sID == student_id) : null;

        //    if (f_gradedetail != null && f_gradedetail.getScore100 != null)
        //    {
        //        var f_plane = planCourseDTOs.FirstOrDefault(w => w.SPlaneId == f_grades.sPlaneID);
        //        string get100 = f_gradedetail.getScore100;
        //        if (f_plane.NCredit != null && (f_plane.CourseGroupName == "รายวิชาเพิ่มเติม" || f_plane.CourseGroupName == "รายวิชาพื้นฐาน"))
        //        {
        //            double get100n = Double.Parse(get100);
        //            grades_Result.sumall = get100n;
        //            grades_Result.maxweight = (f_plane.NCredit ?? 0);

        //            if (f_gradedetail.getSpecial == "-1")
        //            {
        //                //if (gradeEdit != null && gradeEdit.UseGradeSet == 1)
        //                //{

        //                //    if (gradeEdit.GradeSet == decimal.Parse("4.00"))
        //                //        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.50"))
        //                //        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.00"))
        //                //        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.50"))
        //                //        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.00"))
        //                //        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.50"))
        //                //        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.00"))
        //                //        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                //    else
        //                //        grades_Result.sumweight = 0;
        //                //}
        //                //else
        //                //{
        //                if (get100n >= 80)
        //                    grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 75)
        //                    grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 70)
        //                    grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 65)
        //                    grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 60)
        //                    grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 55)
        //                    grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 50)
        //                    grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                else
        //                    grades_Result.sumweight = 0;
        //                //}
        //            }
        //            else if (f_gradedetail.getSpecial == "4")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "10")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "11")
        //            {
        //                grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "12")
        //            {
        //                grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "13")
        //            {
        //                grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //            }
        //            else grades_Result.sumweight = 0;

        //        }
        //        grades_Result.planId =
        //        grades_Result.score = get100;

        //        grades_Result.getSpecial = f_gradedetail.getSpecial;
        //        grades_Result.getGradeLabel = f_gradedetail.getGradeLabel;

        //        grades_Result.label = get100 + "\\" + getSpecial2(f_gradedetail.getSpecial, get100, null);
        //        grades_Result.grade = getSpecial2(f_gradedetail.getSpecial, get100, null);
        //    }

        //    return grades_Result;
        //}

        //public static Grades_Result getGradeHistory(List<GradeHistoryEntity.TGradeDetailHistory> q_gradedetail, List<PlanCourseDTO> planCourseDTOs, GradeHistoryEntity.TGradeHistory f_grades, int student_id)
        //{
        //    Grades_Result grades_Result = new Grades_Result();
        //    var f_gradedetail = new GradeHistoryEntity.TGradeDetailHistory();
        //    f_gradedetail = (q_gradedetail != null) ? q_gradedetail.FirstOrDefault(f => f.nGradeId == f_grades.nGradeId && f.sID == student_id) : null;

        //    if (f_gradedetail != null && f_gradedetail.getScore100 != null)
        //    {
        //        var f_plane = planCourseDTOs.FirstOrDefault(w => w.SPlaneId == f_grades.sPlaneID);
        //        string get100 = f_gradedetail.getScore100;
        //        if (f_plane.NCredit != null && (f_plane.CourseGroupName == "รายวิชาเพิ่มเติม" || f_plane.CourseGroupName == "รายวิชาพื้นฐาน"))
        //        {
        //            double get100n = Double.Parse(get100);
        //            grades_Result.sumall = get100n;
        //            grades_Result.maxweight = (f_plane.NCredit ?? 0);
        //            if (f_gradedetail.getSpecial == "-1")
        //            {
        //                //if (gradeEdit != null && gradeEdit.UseGradeSet == 1)
        //                //{

        //                //    if (gradeEdit.GradeSet == decimal.Parse("4.00"))
        //                //        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.50"))
        //                //        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("3.00"))
        //                //        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.50"))
        //                //        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("2.00"))
        //                //        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.50"))
        //                //        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                //    else if (gradeEdit.GradeSet == decimal.Parse("1.00"))
        //                //        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                //    else
        //                //        grades_Result.sumweight = 0;
        //                //}
        //                //else
        //                //{
        //                if (get100n >= 80)
        //                    grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 75)
        //                    grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 70)
        //                    grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 65)
        //                    grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 60)
        //                    grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 55)
        //                    grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                else if (get100n >= 50)
        //                    grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                else
        //                    grades_Result.sumweight = 0;
        //                //}
        //            }
        //            else if (f_gradedetail.getSpecial == "4")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "10")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "11")
        //            {
        //                grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "12")
        //            {
        //                grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "13")
        //            {
        //                grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //            }
        //            else grades_Result.sumweight = 0;

        //        }
        //        grades_Result.planId =
        //        grades_Result.score = get100;
        //        grades_Result.label = get100 + "\\" + getSpecial2(f_gradedetail.getSpecial, get100, null);
        //        grades_Result.grade = getSpecial2(f_gradedetail.getSpecial, get100, null);
        //        grades_Result.getSpecial = f_gradedetail.getSpecial;
        //        grades_Result.getGradeLabel = f_gradedetail.getGradeLabel;
        //    }

        //    return grades_Result;
        //}

        //public static Grades_Result getGradeWithEditGrade(List<TGradeDetail> q_gradedetail, List<TGradeEdit> gradeEdits, List<PlanCourseDTO> planCourseDTOs, TGrade f_grades, int student_id)
        //{
        //    Grades_Result grades_Result = new Grades_Result();
        //    var f_gradedetail = new TGradeDetail();
        //    var gradeEdit = new TGradeEdit();
        //    f_gradedetail = (q_gradedetail != null) ? q_gradedetail.FirstOrDefault(f => f.nGradeId == f_grades.nGradeId && f.sID == student_id) : null;

        //    if (gradeEdits != null && f_gradedetail != null)
        //    {
        //        gradeEdit = gradeEdits.Where(w => w.GradeDetailID == f_gradedetail.nGradeDetailId && w.UseGradeSet == 1).FirstOrDefault();
        //    }

        //    if ((f_gradedetail != null && f_gradedetail.getScore100 != null) || (f_gradedetail != null && gradeEdit != null))
        //    {

        //        var f_plane = planCourseDTOs.FirstOrDefault(w => w.SPlaneId == f_grades.sPlaneID);
        //        string get100 = (gradeEdit != null && !string.IsNullOrEmpty(gradeEdit.getScore100)) ? gradeEdit.getScore100 : f_gradedetail.getScore100;
        //        if (f_plane.NCredit != null && (f_plane.CourseGroupName == "รายวิชาเพิ่มเติม" || f_plane.CourseGroupName == "รายวิชาพื้นฐาน"))
        //        {
        //            double get100n = Double.Parse(get100);
        //            grades_Result.sumall = get100n;
        //            grades_Result.maxweight = (f_plane.NCredit ?? 0);

        //            if (f_gradedetail.getSpecial == "-1")
        //            {
        //                if (gradeEdit != null && gradeEdit.UseGradeSet == 1)
        //                {

        //                    if (gradeEdit.GradeSet == decimal.Parse("4.00"))
        //                    {
        //                        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "4.0";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("3.50"))
        //                    {
        //                        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "3.5";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("3.00"))
        //                    {
        //                        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "3.0";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("2.50"))
        //                    {
        //                        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "2.5";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("2.00"))
        //                    {
        //                        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "2.0";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("1.50"))
        //                    {
        //                        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "1.5";
        //                    }
        //                    else if (gradeEdit.GradeSet == decimal.Parse("1.00"))
        //                    {
        //                        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                        grades_Result.getGradeLabel = "1.0";
        //                    }
        //                    else
        //                    {
        //                        grades_Result.sumweight = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    if (get100n >= 80)
        //                        grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 75)
        //                        grades_Result.sumweight = 3.5 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 70)
        //                        grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 65)
        //                        grades_Result.sumweight = 2.5 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 60)
        //                        grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 55)
        //                        grades_Result.sumweight = 1.5 * (f_plane.NCredit ?? 0);
        //                    else if (get100n >= 50)
        //                        grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //                    else
        //                        grades_Result.sumweight = 0;
        //                }
        //            }
        //            else if (f_gradedetail.getSpecial == "4")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "10")
        //            {
        //                grades_Result.sumweight = 4 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "11")
        //            {
        //                grades_Result.sumweight = 3 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "12")
        //            {
        //                grades_Result.sumweight = 2 * (f_plane.NCredit ?? 0);
        //            }
        //            else if (f_gradedetail.getSpecial == "13")
        //            {
        //                grades_Result.sumweight = 1 * (f_plane.NCredit ?? 0);
        //            }
        //            else grades_Result.sumweight = 0;

        //        }

        //        grades_Result.score = get100;

        //        grades_Result.getSpecial = f_gradedetail.getSpecial;

        //        if (gradeEdit == null)
        //        {
        //            grades_Result.getGradeLabel = f_gradedetail.getGradeLabel;
        //        }

        //        grades_Result.label = get100 + "\\" + getSpecial2(f_gradedetail.getSpecial, get100, null);
        //        grades_Result.grade = getSpecial2(f_gradedetail.getSpecial, get100, null);
        //    }

        //    return grades_Result;
        //}
        public static string SearchPlane(string term_id, string level_id)
        {
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var planCourseWithTerm = ServiceHelper.GetPlanCoursesWithTerm((!string.IsNullOrEmpty(level_id)) ? Convert.ToInt32(level_id) : 0, 0, 0, Utils.GetSchoolId(), db);
                //if (!string.IsNullOrEmpty(term_id))
                //{
                //    term_id = string.Format("TS{0:0000000}", int.Parse(term_id));
                //}


                planCourseWithTerm = planCourseWithTerm.Where(c => c.NTerm.Trim() == term_id).Distinct().ToList();

                dynamic rss = new JArray(from a in planCourseWithTerm
                                         select new JObject {
                                             new JProperty("value",a.SPlaneId),
                                             new JProperty("text",a.CourseCode + " - " + a.CourseName +" ( เทอม " + a.NTerm + " )"),
                                         });

                return rss.ToString();
            }
        }


        public static string getSpecial2(string special, string score100, decimal? newGrade)
        {
            string txt = "";

            if (special != null && special != "")
            {
                if (special == "1")
                    txt = "ร";
                else if (special == "2")
                    txt = "มส";
                else if (special == "3")
                    txt = "มก";
                else if (special == "4")
                    txt = "ผ";
                else if (special == "5")
                    txt = "มผ";
                else if (special == "6")
                    txt = "อื่นๆ";
                else if (special == "7")
                    txt = "ขร";
                else if (special == "8")
                    txt = "ขส";
                else if (special == "9")
                    txt = "ท";
                else if (special == "10")
                    txt = "ดีเยี่ยม";
                else if (special == "11")
                    txt = "ดี";
                else if (special == "12")
                    txt = "พอใช้";
                else if (special == "13")
                    txt = "ปรับปรุง";
                else txt = "";
            }

            if (special == "-1")
            {
                double score = 0;
                if (score100 != null && score100 != "")
                    score = double.Parse(score100);


                if (txt == "")
                {
                    if (score >= 80)
                        txt = "4.0";
                    else if (score >= 75)
                        txt = "3.5";
                    else if (score >= 70)
                        txt = "3.0";
                    else if (score >= 65)
                        txt = "2.5";
                    else if (score >= 60)
                        txt = "2.0";
                    else if (score >= 55)
                        txt = "1.5";
                    else if (score >= 50)
                        txt = "1.0";
                    else
                        txt = "0";

                    if (newGrade != null)
                    {
                        if (newGrade == decimal.Parse("4.00"))
                            txt = string.Format("{0}/{1}", txt, "4");
                        else if (newGrade == decimal.Parse("3.50"))
                            txt = string.Format("{0}/{1}", txt, "3.5");
                        else if (newGrade == decimal.Parse("3.00"))
                            txt = string.Format("{0}/{1}", txt, "3");
                        else if (newGrade == decimal.Parse("2.50"))
                            txt = string.Format("{0}/{1}", txt, "2.5");
                        else if (newGrade == decimal.Parse("2.00"))
                            txt = string.Format("{0}/{1}", txt, "2");
                        else if (newGrade == decimal.Parse("1.50"))
                            txt = string.Format("{0}/{1}", txt, "1.5");
                        else if (newGrade == decimal.Parse("1.00"))
                            txt = string.Format("{0}/{1}", txt, "1.0");
                        else
                            txt = "0";
                    }

                }
            }

            return txt;
        }

        private static DataTable Pivot(DataTable dt, DataColumn pivotColumn, DataColumn pivotValue)
        {
            // find primary key columns 
            //(i.e. everything but pivot column and pivot value)
            DataTable temp = dt.Copy();
            temp.Columns.Remove(pivotColumn.ColumnName);
            temp.Columns.Remove(pivotValue.ColumnName);
            string[] pkColumnNames = temp.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToArray();

            // prep results table
            DataTable result = temp.DefaultView.ToTable(true, pkColumnNames).Copy();
            result.PrimaryKey = result.Columns.Cast<DataColumn>().ToArray();
            dt.AsEnumerable()
                .Select(r => r[pivotColumn.ColumnName].ToString())
                .Distinct().ToList()
                .ForEach(c => result.Columns.Add(c, pivotValue.DataType));

            // load it
            foreach (DataRow row in dt.Rows)
            {
                // find row to update
                DataRow aggRow = result.Rows.Find(
                    pkColumnNames
                        .Select(c => row[c])
                        .ToArray());
                // the aggregate used here is LATEST 
                // adjust the next line if you want (SUM, MAX, etc...)
                aggRow[row[pivotColumn.ColumnName].ToString()] = row[pivotValue.ColumnName];
            }

            return result;
        }

        public static string geTitelName(List<TTitleList> titleLists, string titelId)
        {
            int title_Id = 0;
            int.TryParse(titelId, out title_Id);
            if (title_Id == 0) return titelId;
            else
            {
                var f_title = titleLists.FirstOrDefault(f => f.nTitleid == title_Id);
                return f_title == null ? titelId : f_title.titleDescription;
            }
        }

        public static string geTitelNameEN(List<TTitleList> titleLists, string titelId)
        {
            int title_Id = 0;
            int.TryParse(titelId, out title_Id);
            if (title_Id == 0) return titelId;
            else
            {
                var f_title = titleLists.FirstOrDefault(f => f.nTitleid == title_Id);
                return f_title == null ? titelId : f_title.titleDescriptionEn;
            }
        }

        public static string geTNation(List<TMasterData> masterDatas, string nationId)
        {
            var f_nation = masterDatas.FirstOrDefault(f => f.MasterCode == nationId);
            return f_nation == null ? nationId : f_nation.MasterDes;
        }
        public static string geTReligion(List<TMasterData> masterDatas, string religionId)
        {
            var f_religion = masterDatas.FirstOrDefault(f => f.MasterCode == religionId);
            return f_religion == null ? religionId : f_religion.MasterDes;
        }
        public static string geTRace(List<TMasterData> masterDatas, string raceId)
        {
            var f_race = masterDatas.FirstOrDefault(f => f.MasterCode == raceId);
            return f_race == null ? raceId : f_race.MasterDes;
        }


        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }

        public static List<System.Web.UI.WebControls.ListItem> GetEmployeeTypeToDDL(int schoolId)
        {
            using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                var d = db.TEmployeeTypes
                    .Where(o => o.SchoolID == schoolId && o.IsActive == true && o.IsDel == false)
                    .Select(o => new System.Web.UI.WebControls.ListItem
                    {
                        Value = (o.nTypeId2 ?? o.nTypeId) + "",
                        Text = o.Title,
                    })
                    .OrderBy(o => o.Value)
                    .ToList();

                d.Insert(0, new System.Web.UI.WebControls.ListItem { Value = "", Text = "ทั้งหมด" });
                return d;
            }
        }

        //public static bool CheckScoreExist(TGradeDetail gradeDetail)
        //{
        //    var isScoreEntered = false;

        //    if (gradeDetail != null && !string.IsNullOrEmpty(gradeDetail.ScoreData))
        //    {
        //        var scoredatas = new JavaScriptSerializer().Deserialize<List<StudentScoreData>>(gradeDetail.ScoreData);
        //        if (scoredatas != null)
        //        {
        //            var objScore = scoredatas.Where(w => !w.ScoreIdentifier.Contains("scoreBehavior") && !w.ScoreIdentifier.Contains("scoreSamattana") && !w.ScoreIdentifier.Contains("scoreReadWrite") && !string.IsNullOrEmpty(w.Score)).ToList();
        //            if (objScore != null && objScore.Count > 0)
        //            {
        //                isScoreEntered = true;
        //            }
        //        }
        //    }

        //    return (gradeDetail != null && !string.IsNullOrEmpty(gradeDetail.getScore100) && gradeDetail.getSpecial == "-1" && isScoreEntered) ? true : false;
        //}

        //public static bool CheckScoreExistHistory(GradeHistoryEntity.TGradeDetailHistory gradeDetail)
        //{
        //    var isScoreEntered = false;

        //    if (gradeDetail != null && !string.IsNullOrEmpty(gradeDetail.ScoreData))
        //    {
        //        var scoredatas = new JavaScriptSerializer().Deserialize<List<StudentScoreData>>(gradeDetail.ScoreData);
        //        if (scoredatas != null)
        //        {
        //            var objScore = scoredatas.Where(w => !w.ScoreIdentifier.Contains("scoreBehavior") && !w.ScoreIdentifier.Contains("scoreSamattana") && !w.ScoreIdentifier.Contains("scoreReadWrite") && !string.IsNullOrEmpty(w.Score)).ToList();
        //            if (objScore != null && objScore.Count > 0)
        //            {
        //                isScoreEntered = true;
        //            }
        //        }
        //    }

        //    return (gradeDetail != null && !string.IsNullOrEmpty(gradeDetail.getScore100) && gradeDetail.getSpecial == "-1" && isScoreEntered) ? true : false;
        //}

        //public static List<string> GetPlane(string nterm1, string nterm2, int? idlv2n, int idlvn, int schoolId)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    {

        //        var planeIds = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nterm1 || w.nTerm == nterm2) && w.nTermSubLevel2 == idlv2n).Select(s => s.sPlaneID).Distinct().ToList();


        //        return (from g in planeIds
        //                join p in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.nTSubLevel == idlvn.ToString() && w.cDel == null).Select(s => new { s.sPlaneID, s.courseGroup }).AsEnumerable() on g equals p.sPlaneID
        //                join cg in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId).AsEnumerable() on p.courseGroup equals cg.courseGroupId
        //                where cg.Description != "รายวิชาเสริมไม่คิดหน่วยกิต" && cg.Description != "กิจกรรมพัฒนาผู้เรียน"
        //                select g.ToString()).Distinct().ToList();
        //    }
        //}

        //public static List<string> GetPlaneHistory(string nterm1, string nterm2, int? idlv2n, int idlvn, int schoolId)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {

        //        var planeIds = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && (w.nTerm == nterm1 || w.nTerm == nterm2) && w.nTermSubLevel2 == idlv2n).Select(s => s.sPlaneID).Distinct().ToList();


        //        return (from g in planeIds
        //                join p in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.nTSubLevel == idlvn.ToString() && w.cDel == null).Select(s => new { s.sPlaneID, s.courseGroup }).AsEnumerable() on g equals p.sPlaneID
        //                join cg in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId).AsEnumerable() on p.courseGroup equals cg.courseGroupId
        //                where cg.Description != "รายวิชาเสริมไม่คิดหน่วยกิต" && cg.Description != "กิจกรรมพัฒนาผู้เรียน"
        //                select g.ToString()).Distinct().ToList();
        //    }
        //}

        /// <summary>
        /// Get Datetime.Now and config Second
        /// </summary>
        /// <param name="nd">Datetime.Now</param>
        /// <param name="second">config second</param>
        /// <returns>Fixed datetime</returns>
        public static DateTime FixSecond(this DateTime nd, int second)
        {
            DateTime rd = new DateTime(nd.Year, nd.Month, nd.Day, nd.Hour, nd.Minute, second, nd.Millisecond);

            return rd;
        }

        /// <summary>
        /// Get Datetime.Now and config millisecond
        /// </summary>
        /// <param name="nd">Datetime.Now</param>
        /// <param name="millisecond">config millisecond</param>
        /// <returns>Fixed datetime</returns>
        public static DateTime FixMillisecond(this DateTime nd, int millisecond)
        {
            DateTime rd = new DateTime(nd.Year, nd.Month, nd.Day, nd.Hour, nd.Minute, nd.Second, millisecond);

            return rd;
        }

        /// <summary>
        /// Get Datetime.Now and config second & millisecond
        /// </summary>
        /// <param name="nd">Datetime.Now</param>
        /// <param name="second">config second</param>
        /// <param name="millisecond">config millisecond</param>
        /// <returns>Fixed datetime</returns>
        public static DateTime FixSecondAndMillisecond(this DateTime nd, int second, int millisecond)
        {
            DateTime rd = new DateTime(nd.Year, nd.Month, nd.Day, nd.Hour, nd.Minute, second, millisecond);

            return rd;
        }

        public static string GetClassTeacherName(int schoolId, int nTermSubLevel2, string nTerm, bool thaiLanguage = true)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                var sign1 = schoolDbContext.TClassMembers.Where(w => w.SchoolID == schoolId && w.nTermSubLevel2 == nTermSubLevel2 && w.nTerm == nTerm).FirstOrDefault();
                if (sign1 != null)
                {
                    if (sign1.nTeacherHeadid != null)
                    {
                        string teacherName = "";

                        try
                        {
                            var employeeObj = schoolDbContext.TEmployees.Where(w => w.SchoolID == schoolId && w.sEmp == sign1.nTeacherHeadid).FirstOrDefault();
                            int titleID = 0;
                            bool canConvertTitleID = int.TryParse(employeeObj.sTitle, out titleID);

                            var titleObj = schoolDbContext.TTitleLists.Where(w => w.SchoolID == schoolId && w.nTitleid == titleID).FirstOrDefault();

                            if (thaiLanguage)
                            {
                                if (canConvertTitleID)
                                {
                                    teacherName = titleObj.titleDescription + employeeObj.sName + " " + employeeObj.sLastname;
                                }
                                else
                                {
                                    teacherName = employeeObj.sTitle + employeeObj.sName + " " + employeeObj.sLastname;
                                }
                            }
                            else
                            {
                                var employeeInfoObj = schoolDbContext.TEmployeeInfoes.Where(w => w.SchoolID == schoolId && w.sEmp == sign1.nTeacherHeadid).FirstOrDefault();
                                var firstNameEn = !string.IsNullOrEmpty(employeeInfoObj.FirstNameEn) ? employeeInfoObj.FirstNameEn : "";
                                var lastNameEn = !string.IsNullOrEmpty(employeeInfoObj.LastNameEn) ? employeeInfoObj.LastNameEn : "";

                                if (canConvertTitleID)
                                {
                                    teacherName = titleObj.titleDescriptionEn + " " + firstNameEn + " " + lastNameEn;
                                }
                                else
                                {
                                    teacherName = firstNameEn + " " + lastNameEn;
                                }
                            }
                        }
                        catch
                        {

                        }

                        return teacherName;
                    }
                }

                return string.Empty;
            }
        }

        public static string GetSchoolHeadName(int schoolId, TCompany tCompany, bool thaiLanguage = true)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                if (tCompany.nSchoolHeadid != null)
                {
                    string teacherName = "";

                    try
                    {
                        var employeeObj = schoolDbContext.TEmployees.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nSchoolHeadid).FirstOrDefault();
                        int titleID = 0;
                        bool canConvertTitleID = int.TryParse(employeeObj.sTitle, out titleID);

                        var titleObj = schoolDbContext.TTitleLists.Where(w => w.SchoolID == schoolId && w.nTitleid == titleID).FirstOrDefault();

                        if (thaiLanguage)
                        {
                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescription + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                            else
                            {
                                teacherName = employeeObj.sTitle + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                        }
                        else
                        {
                            var employeeInfoObj = schoolDbContext.TEmployeeInfoes.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nSchoolHeadid).FirstOrDefault();
                            var firstNameEn = !string.IsNullOrEmpty(employeeInfoObj.FirstNameEn) ? employeeInfoObj.FirstNameEn : "";
                            var lastNameEn = !string.IsNullOrEmpty(employeeInfoObj.LastNameEn) ? employeeInfoObj.LastNameEn : "";

                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescriptionEn + " " + firstNameEn + " " + lastNameEn;
                            }
                            else
                            {
                                teacherName = firstNameEn + " " + lastNameEn;
                            }
                        }
                    }
                    catch
                    {

                    }

                    return teacherName;
                }
                else
                {
                    return tCompany.SchoolHeadName + " " + tCompany.SchoolHeadLastname;
                }
            }
        }

        public static string GetAcademicDepartmentHead(int schoolId, TCompany tCompany, bool thaiLanguage = true)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                if (tCompany.nRegistraDirectorid != null)
                {
                    string teacherName = "";

                    try
                    {
                        var employeeObj = schoolDbContext.TEmployees.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nRegistraDirectorid).FirstOrDefault();
                        int titleID = 0;
                        bool canConvertTitleID = int.TryParse(employeeObj.sTitle, out titleID);

                        var titleObj = schoolDbContext.TTitleLists.Where(w => w.SchoolID == schoolId && w.nTitleid == titleID).FirstOrDefault();

                        if (thaiLanguage)
                        {
                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescription + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                            else
                            {
                                teacherName = employeeObj.sTitle + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                        }
                        else
                        {
                            var employeeInfoObj = schoolDbContext.TEmployeeInfoes.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nRegistraDirectorid).FirstOrDefault();
                            var firstNameEn = !string.IsNullOrEmpty(employeeInfoObj.FirstNameEn) ? employeeInfoObj.FirstNameEn : "";
                            var lastNameEn = !string.IsNullOrEmpty(employeeInfoObj.LastNameEn) ? employeeInfoObj.LastNameEn : "";

                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescriptionEn + " " + firstNameEn + " " + lastNameEn;
                            }
                            else
                            {
                                teacherName = firstNameEn + " " + lastNameEn;
                            }
                        }
                    }
                    catch
                    {

                    }

                    return teacherName;
                }

                return string.Empty;
            }
        }

        public static string GetSchoolDirectorName(int schoolId, TCompany tCompany, bool thaiLanguage = true)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                if (tCompany.nAcademicDirectorid != null)
                {
                    string teacherName = "";

                    try
                    {
                        var employeeObj = schoolDbContext.TEmployees.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nAcademicDirectorid).FirstOrDefault();
                        int titleID = 0;
                        bool canConvertTitleID = int.TryParse(employeeObj.sTitle, out titleID);

                        var titleObj = schoolDbContext.TTitleLists.Where(w => w.SchoolID == schoolId && w.nTitleid == titleID).FirstOrDefault();

                        if (thaiLanguage)
                        {
                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescription + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                            else
                            {
                                teacherName = employeeObj.sTitle + employeeObj.sName + " " + employeeObj.sLastname;
                            }
                        }
                        else
                        {
                            var employeeInfoObj = schoolDbContext.TEmployeeInfoes.Where(w => w.SchoolID == schoolId && w.sEmp == tCompany.nAcademicDirectorid).FirstOrDefault();
                            var firstNameEn = !string.IsNullOrEmpty(employeeInfoObj.FirstNameEn) ? employeeInfoObj.FirstNameEn : "";
                            var lastNameEn = !string.IsNullOrEmpty(employeeInfoObj.LastNameEn) ? employeeInfoObj.LastNameEn : "";

                            if (canConvertTitleID)
                            {
                                teacherName = titleObj.titleDescriptionEn + " " + firstNameEn + " " + lastNameEn;
                            }
                            else
                            {
                                teacherName = firstNameEn + " " + lastNameEn;
                            }
                        }
                    }
                    catch
                    {

                    }

                    return teacherName;
                }

                return string.Empty;
            }
        }

        public static string CalculateGPA(double? gradexcredit, double? registerCredit)
        {
            double grade = (registerCredit == 0 || gradexcredit == 0 || gradexcredit == null || registerCredit == null) ? default(double) : (((gradexcredit * 4) / (registerCredit * 4)) != null) ? (double)((gradexcredit * 4) / (registerCredit * 4)) : default(double);
            return Truncate(grade, 2).ToString("0.00");
        }
        public static double Truncate(double d, byte decimals)
        {
            double r = Math.Round(d, decimals);

            if (d > 0 && r > d)
            {
                return r - (double)(new decimal(1, 0, 0, false, decimals));
            }
            else if (d < 0 && r < d)
            {
                return r + (double)(new decimal(1, 0, 0, false, decimals));
            }

            return r;
        }
    }
}