using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using Ninject;
using SchoolBright.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for SchoolReportStartup
    /// </summary>
    public class SchoolReportStartup : IHttpHandler, IRequiresSessionState
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
            string id = fcommon.ReplaceInjection(context.Request["id"]);


            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));

            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

            int? idlv2n = Int32.Parse(idlv2);
            int idlvn = int.Parse(idlv);
            int? useryear = Int32.Parse(year);
            int nyear = 0;
            string nterm = "";
            List<string> planIdlist = new List<string>();


            foreach (var ff in _db.TYears.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.numberYear == useryear))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.sTerm == term && w.nYear == nyear && w.cDel == null))
            {
                nterm = ee.nTerm;
            }

            var planCourseDTO = ServiceHelper.GetPlanCourses(idlvn, ((idlv2n != null) ? (int)idlv2n : 0), nterm, nyear, userData.CompanyID, _db);

            List<string> unique = new List<string>();

            unique.AddRange((from a in _db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                             join b in _db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals b.nTermTable
                             where b.cDel == null && a.nTerm == nterm && a.nTermSubLevel2 == idlv2n
                             select
                                 b.sPlaneID.ToString()
                             ).Distinct().ToList());

            unique.AddRange((from a in _dbGrade.TGrades
                             where !unique.Contains(a.sPlaneID.ToString()) && a.nTermSubLevel2 == idlv2n && a.nTerm.Trim() == nterm && a.SchoolID == userData.CompanyID
                             select a.sPlaneID.ToString()));

            List<sortList> sortList = new List<sortList>();
            sortList sort = new sortList();

            foreach (var ii in unique)
            {
                    var kk = planCourseDTO.Where(w => w.SPlaneId.ToString() == ii).FirstOrDefault();
                if (kk != null)
                {
                    sort = new sortList();
                    sort.planId = kk.SPlaneId.ToString();
                    sort.planName = kk.CourseName;

                    if (kk.CourseType != null)
                    {
                        sort.sortnumberType = kk.CourseType;
                    }
                    else sort.sortnumberType = 10;

                    if (kk.CourseGroup != null)
                    {
                        sort.sortnumberGroup = kk.CourseGroup;
                    }
                    else sort.sortnumberGroup = 10;

                    if (kk.CourseCode != null)
                    {
                        sort.planCode = kk.CourseCode;
                    }
                    else sort.planCode = "zzzz";

                    sortList.Add(sort);
                }
                else
                {
                    sort.sortnumberType = 10;
                    sort.sortnumberGroup = 10;
                    sort.planCode = "zzzz";
                    sortList.Add(sort);
                }
            }


            var newSortList2 = sortList;
            newSortList2 = sortList.OrderBy(x => x.sortnumberGroup).ThenBy(x => x.sortnumberType).ThenBy(x => x.planCode).ToList();


            string plan1 = "";
            string tName = "";
            var mem = _db.TClassMembers.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n).FirstOrDefault();
            if(mem != null)
            {
                if (mem.nTeacherHeadid != null)
                {
                    var teacher = _db.TEmployees.Where(w => w.SchoolID == userData.CompanyID && w.sEmp == mem.nTeacherHeadid).FirstOrDefault();
                    
                    string stdTitle = "";
                    if(teacher != null)
                    {
                        if (teacher.sTitle != null)
                        {
                            int n;
                            bool isNumeric = int.TryParse(teacher.sTitle, out n);
                            if (isNumeric == true)
                            {
                                var title = _db.TTitleLists.Where(w => w.SchoolID == userData.CompanyID && w.nTitleid == n).FirstOrDefault();
                                stdTitle = title.titleDescription;
                            }
                            else
                            {
                                stdTitle = teacher.sTitle;
                            }

                            tName = stdTitle + " " + teacher.sName + " " + teacher.sLastname;
                        }
                    }
                                        
                }
            }
            
            
            PlanDetail Detail = new PlanDetail();
            List<PlanDetail> DetailList = new List<PlanDetail>();
            Detail.schoolHeadName = nCompany.SchoolHeadName +" "+ nCompany.SchoolHeadLastname;
            Detail.schoolHeadJob = "ผู้อำนวยการ";
            Detail.teacherName = tName;
            Detail.teacherJob = "ครูประจำชั้น";
            Detail.name1 = "";
            Detail.name2 = "";
            Detail.name3 = "";
            Detail.name4 = "";
            Detail.name5 = "";
            Detail.name6 = "";
            Detail.name7 = "";
            Detail.name8 = "";
            Detail.name9 = "";
            Detail.name10 = "";
            Detail.name11 = "";
            Detail.name12 = "";
            Detail.name13 = "";
            Detail.name14 = "";
            Detail.name15 = "";
            Detail.name16 = "";
            Detail.name17 = "";
            Detail.name18 = "";
            Detail.name19 = "";
            Detail.name20 = "";
            Detail.name21 = "";
            Detail.name22 = "";
            Detail.name23 = "";
            Detail.name24 = "";
            Detail.name25 = "";
            Detail.name26 = "";
            Detail.code1 = "";
            Detail.code2 = "";
            Detail.code3 = "";
            Detail.code4 = "";
            Detail.code5 = "";
            Detail.code6 = "";
            Detail.code7 = "";
            Detail.code8 = "";
            Detail.code9 = "";
            Detail.code10 = "";
            Detail.code11 = "";
            Detail.code12 = "";
            Detail.code13 = "";
            Detail.code14 = "";
            Detail.code15 = "";
            Detail.code16 = "";
            Detail.code17 = "";
            Detail.code18 = "";
            Detail.code19 = "";
            Detail.code20 = "";
            Detail.code21 = "";
            Detail.code22 = "";
            Detail.code23 = "";
            Detail.code24 = "";
            Detail.code25 = "";
            Detail.code26 = "";

            int register = 0;
            double? creditsum = 0;

            int maxstd = 0;
            foreach (var check in _db.TUsers.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == idlv2n && w.cDel == null && (w.nStudentStatus == null || w.nStudentStatus == 0)))
            {
                var TUserMaster = _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany && w.cType == "0" && w.nSystemID == check.sID).FirstOrDefault();
                if (TUserMaster != null) 
                {
                    maxstd = maxstd + 1;
                }
                    
            }

            foreach (var ii in newSortList2)
            {
                    var kk = planCourseDTO.Where(w => w.SPlaneId.ToString() == ii.planId).FirstOrDefault();
                if (kk != null)
                {
                    var data2 = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == ii.planId && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (data2 != null)
                    {
                        int check1 = 0;

                        foreach (var check in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data2.nGradeId && w.getSpecial == "-1" && w.getScore100 == "0" && w.cDel == false))
                        {
                            var check3 = _db.TUsers.Where(w => w.sID == check.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                            if(check3!=null)
                            {
                                if (check3.nTermSubLevel2 == data2.nTermSubLevel2 && (check3.nStudentStatus == 0 || check3.nStudentStatus == null))
                                    check1 = check1 + 1;
                            }                            
                        }

                        if (register == 0 && check1 < maxstd) { Detail.name1 = kk.CourseName; Detail.code1 = kk.CourseCode; Detail.max1 = kk.NCredit.ToString(); plan1 = data2.sPlaneID.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 1 && check1 < maxstd) { Detail.name2 = kk.CourseName; Detail.code2 = kk.CourseCode; Detail.max2 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 2 && check1 < maxstd) { Detail.name3 = kk.CourseName; Detail.code3 = kk.CourseCode; Detail.max3 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 3 && check1 < maxstd) { Detail.name4 = kk.CourseName; Detail.code4 = kk.CourseCode; Detail.max4 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 4 && check1 < maxstd) { Detail.name5 = kk.CourseName; Detail.code5 = kk.CourseCode; Detail.max5 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 5 && check1 < maxstd) { Detail.name6 = kk.CourseName; Detail.code6 = kk.CourseCode; Detail.max6 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 6 && check1 < maxstd) { Detail.name7 = kk.CourseName; Detail.code7 = kk.CourseCode; Detail.max7 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 7 && check1 < maxstd) { Detail.name8 = kk.CourseName; Detail.code8 = kk.CourseCode; Detail.max8 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 8 && check1 < maxstd) { Detail.name9 = kk.CourseName; Detail.code9 = kk.CourseCode; Detail.max9 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 9 && check1 < maxstd) { Detail.name10 = kk.CourseName; Detail.code10 = kk.CourseCode; Detail.max10 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 10 && check1 < maxstd) { Detail.name11 = kk.CourseName; Detail.code11 = kk.CourseCode; Detail.max11 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 11 && check1 < maxstd) { Detail.name12 = kk.CourseName; Detail.code12 = kk.CourseCode; Detail.max12 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 12 && check1 < maxstd) { Detail.name13 = kk.CourseName; Detail.code13 = kk.CourseCode; Detail.max13 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 13 && check1 < maxstd) { Detail.name14 = kk.CourseName; Detail.code14 = kk.CourseCode; Detail.max14 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 14 && check1 < maxstd) { Detail.name15 = kk.CourseName; Detail.code15 = kk.CourseCode; Detail.max15 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 15 && check1 < maxstd) { Detail.name16 = kk.CourseName; Detail.code16 = kk.CourseCode; Detail.max16 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 16 && check1 < maxstd) { Detail.name17 = kk.CourseName; Detail.code17 = kk.CourseCode; Detail.max17 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 17 && check1 < maxstd) { Detail.name18 = kk.CourseName; Detail.code18 = kk.CourseCode; Detail.max18 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 18 && check1 < maxstd) { Detail.name19 = kk.CourseName; Detail.code19 = kk.CourseCode; Detail.max19 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 19 && check1 < maxstd) { Detail.name20 = kk.CourseName; Detail.code20 = kk.CourseCode; Detail.max20 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 20 && check1 < maxstd) { Detail.name21 = kk.CourseName; Detail.code21 = kk.CourseCode; Detail.max21 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 21 && check1 < maxstd) { Detail.name22 = kk.CourseName; Detail.code22 = kk.CourseCode; Detail.max22 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 22 && check1 < maxstd) { Detail.name23 = kk.CourseName; Detail.code23 = kk.CourseCode; Detail.max23 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 23 && check1 < maxstd) { Detail.name24 = kk.CourseName; Detail.code24 = kk.CourseCode; Detail.max24 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 24 && check1 < maxstd) { Detail.name25 = kk.CourseName; Detail.code25 = kk.CourseCode; Detail.max25 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (register == 25 && check1 < maxstd) { Detail.name26 = kk.CourseName; Detail.code26 = kk.CourseCode; Detail.max26 = kk.NCredit.ToString(); creditsum = creditsum + kk.NCredit; }
                        if (check1 < maxstd)
                            register = register + 1;
                    }                    
                }
            }

            Detail.maxtotal = creditsum.ToString();
            var grade1 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan1 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            int number = 0;
            if (id == "" || id == "99998")
            {
                if (grade1 != null)
                {
                    foreach (var detail1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade1.nGradeId && w.cDel == false))
                    {
                        number = number + 1;
                    }
                }
            }
            else number = 1;
            
            Detail.totalstudent = number.ToString();
            Detail.registerPlan = register;
            DetailList.Add(Detail);
            

            rss = new JArray(from a in DetailList
                             select new JObject(

                   new JProperty("name1", a.name1),
                   new JProperty("name2", a.name2),
                   new JProperty("name3", a.name3),
                   new JProperty("name4", a.name4),
                   new JProperty("name5", a.name5),
                   new JProperty("name6", a.name6),
                   new JProperty("name7", a.name7),
                   new JProperty("name8", a.name8),
                   new JProperty("name9", a.name9),
                   new JProperty("name10", a.name10),
                   new JProperty("name11", a.name11),
                   new JProperty("name12", a.name12),
                   new JProperty("name13", a.name13),
                   new JProperty("name14", a.name14),
                   new JProperty("name15", a.name15),
                   new JProperty("name16", a.name16),
                   new JProperty("name17", a.name17),
                   new JProperty("name18", a.name18),
                   new JProperty("name19", a.name19),
                   new JProperty("name20", a.name20),
                   new JProperty("name21", a.name21),
                   new JProperty("name22", a.name22),
                   new JProperty("name23", a.name23),
                   new JProperty("name24", a.name24),
                   new JProperty("name25", a.name25),
                   new JProperty("name26", a.name26),
                   new JProperty("code1", a.code1),
                   new JProperty("code2", a.code2),
                   new JProperty("code3", a.code3),
                   new JProperty("code4", a.code4),
                   new JProperty("code5", a.code5),
                   new JProperty("code6", a.code6),
                   new JProperty("code7", a.code7),
                   new JProperty("code8", a.code8),
                   new JProperty("code9", a.code9),
                   new JProperty("code10", a.code10),
                   new JProperty("code11", a.code11),
                   new JProperty("code12", a.code12),
                   new JProperty("code13", a.code13),
                   new JProperty("code14", a.code14),
                   new JProperty("code15", a.code15),
                   new JProperty("code16", a.code16),
                   new JProperty("code17", a.code17),
                   new JProperty("code18", a.code18),
                   new JProperty("code19", a.code19),
                   new JProperty("code20", a.code20),
                   new JProperty("code21", a.code21),
                   new JProperty("code22", a.code22),
                   new JProperty("code23", a.code23),
                   new JProperty("code24", a.code24),
                   new JProperty("code25", a.code25),
                   new JProperty("code26", a.code26),
                   new JProperty("max1", a.max1),
                   new JProperty("max2", a.max2),
                   new JProperty("max3", a.max3),
                   new JProperty("max4", a.max4),
                   new JProperty("max5", a.max5),
                   new JProperty("max6", a.max6),
                   new JProperty("max7", a.max7),
                   new JProperty("max8", a.max8),
                   new JProperty("max9", a.max9),
                   new JProperty("max10", a.max10),
                   new JProperty("max11", a.max11),
                   new JProperty("max12", a.max12),
                   new JProperty("max13", a.max13),
                   new JProperty("max14", a.max14),
                   new JProperty("max15", a.max15),
                   new JProperty("max16", a.max16),
                   new JProperty("max17", a.max17),
                   new JProperty("max18", a.max18),
                   new JProperty("max19", a.max19),
                   new JProperty("max20", a.max20),
                   new JProperty("max21", a.max21),
                   new JProperty("max22", a.max22),
                   new JProperty("max23", a.max23),
                   new JProperty("max24", a.max24),
                   new JProperty("max25", a.max25),
                   new JProperty("max26", a.max26),
                   new JProperty("maxtotal", a.maxtotal),
                   new JProperty("totalstudent", a.totalstudent),
                   new JProperty("teacherName", a.teacherName),
                   new JProperty("teacherJob", a.teacherJob),
                   new JProperty("schoolHeadName", a.schoolHeadName),
                   new JProperty("schoolHeadJob", a.schoolHeadJob),
                   new JProperty("registerPlan", a.registerPlan)

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


        //protected class PlanDetail
        //{
        //    public string code1 { get; set; }
        //    public string code2 { get; set; }
        //    public string code3 { get; set; }
        //    public string code4 { get; set; }
        //    public string code5 { get; set; }
        //    public string code6 { get; set; }
        //    public string code7 { get; set; }
        //    public string code8 { get; set; }
        //    public string code9 { get; set; }
        //    public string code10 { get; set; }
        //    public string code11 { get; set; }
        //    public string code12 { get; set; }
        //    public string code13 { get; set; }
        //    public string code14 { get; set; }
        //    public string code15 { get; set; }
        //    public string code16 { get; set; }
        //    public string code17 { get; set; }
        //    public string code18 { get; set; }
        //    public string code19 { get; set; }
        //    public string code20 { get; set; }
        //    public string code21 { get; set; }
        //    public string code22 { get; set; }
        //    public string code23 { get; set; }
        //    public string code24 { get; set; }
        //    public string code25 { get; set; }
        //    public string code26 { get; set; }
        //    public string name1 { get; set; }
        //    public string name2 { get; set; }
        //    public string name3 { get; set; }
        //    public string name4 { get; set; }
        //    public string name5 { get; set; }
        //    public string name6 { get; set; }
        //    public string name7 { get; set; }
        //    public string name8 { get; set; }
        //    public string name9 { get; set; }
        //    public string name10 { get; set; }
        //    public string name11 { get; set; }
        //    public string name12 { get; set; }
        //    public string name13 { get; set; }
        //    public string name14 { get; set; }
        //    public string name15 { get; set; }
        //    public string name16 { get; set; }
        //    public string name17 { get; set; }
        //    public string name18 { get; set; }
        //    public string name19 { get; set; }
        //    public string name20 { get; set; }
        //    public string name21 { get; set; }
        //    public string name22 { get; set; }
        //    public string name23 { get; set; }
        //    public string name24 { get; set; }
        //    public string name25 { get; set; }
        //    public string name26 { get; set; }
        //    public string max1 { get; set; }
        //    public string max2 { get; set; }
        //    public string max3 { get; set; }
        //    public string max4 { get; set; }
        //    public string max5 { get; set; }
        //    public string max6 { get; set; }
        //    public string max7 { get; set; }
        //    public string max8 { get; set; }
        //    public string max9 { get; set; }
        //    public string max10 { get; set; }
        //    public string max11 { get; set; }
        //    public string max12 { get; set; }
        //    public string max13 { get; set; }
        //    public string max14 { get; set; }
        //    public string max15 { get; set; }
        //    public string max16 { get; set; }
        //    public string max17 { get; set; }
        //    public string max18 { get; set; }
        //    public string max19 { get; set; }
        //    public string max20 { get; set; }
        //    public string max21 { get; set; }
        //    public string max22 { get; set; }
        //    public string max23 { get; set; }
        //    public string max24 { get; set; }
        //    public string max25 { get; set; }
        //    public string max26 { get; set; }
        //    public string maxtotal { get; set; }
        //    public string totalstudent { get; set; }
        //    public string teacherName { get; set; }
        //    public string teacherJob { get; set; }
        //    public string schoolHeadName { get; set; }
        //    public string schoolHeadJob { get; set; }
        //    public int registerPlan { get; set; }
        //}
        //protected class sortList
        //{
        //    public string planId { get; set; }
        //    public int? sortnumberType { get; set; }
        //    public int? sortnumberGroup { get; set; }
        //    public string planName { get; set; }
        //    public string planCode { get; set; }
        //}
    }


}