using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
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
    /// Summary description for SchoolReport
    /// </summary>
    public class SchoolReport : IHttpHandler, IRequiresSessionState
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

            int idn = 99999;
            if (id != "")
                idn = Int32.Parse(id);

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            var nCompany = _dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
            //var gradeDetail = _db.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList();

            int idlvn = int.Parse(idlv);
            int? idlv2n = Int32.Parse(idlv2);
            int? useryear = Int32.Parse(year);
            int nyear = 0;
            string nterm = "";
            List<string> planIdlist = new List<string>();


            foreach (var ff in _db.TYears.Where(w => w.numberYear == useryear && w.SchoolID == userData.CompanyID))
            {
                nyear = ff.nYear;
            }

            foreach (var ee in _db.TTerms.Where(w => w.sTerm == term && w.nYear == nyear && w.SchoolID == userData.CompanyID && w.cDel == null))
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
                             where !unique.Contains((a.sPlaneID != null)?a.sPlaneID.ToString() : string.Empty) && a.nTermSubLevel2 == idlv2n && a.nTerm.Trim() == nterm && a.SchoolID == userData.CompanyID
                             select a.sPlaneID.ToString()));

            List<sortList> sortList = new List<sortList>();
            sortList sort = new sortList();

            foreach (var ii in unique)
            {
                int sPlaneId = 0;
                int.TryParse(ii, out sPlaneId);
                var kk = planCourseDTO.Where(w => w.SPlaneId == sPlaneId).FirstOrDefault();
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


            var newSortList4 = sortList;
            newSortList4 = sortList.OrderBy(x => x.sortnumberGroup).ThenBy(x => x.sortnumberType).ThenBy(x => x.planCode).ToList();


            string plan1 = "";
            string plan2 = "";
            string plan3 = "";
            string plan4 = "";
            string plan5 = "";
            string plan6 = "";
            string plan7 = "";
            string plan8 = "";
            string plan9 = "";
            string plan10 = "";
            string plan11 = "";
            string plan12 = "";
            string plan13 = "";
            string plan14 = "";
            string plan15 = "";
            string plan16 = "";
            string plan17 = "";
            string plan18 = "";
            string plan19 = "";
            string plan20 = "";
            string plan21 = "";
            string plan22 = "";
            string plan23 = "";
            string plan24 = "";
            string plan25 = "";
            string plan26 = "";

            double? credit1 = 0;
            double? credit2 = 0;
            double? credit3 = 0;
            double? credit4 = 0;
            double? credit5 = 0;
            double? credit6 = 0;
            double? credit7 = 0;
            double? credit8 = 0;
            double? credit9 = 0;
            double? credit10 = 0;
            double? credit11 = 0;
            double? credit12 = 0;
            double? credit13 = 0;
            double? credit14 = 0;
            double? credit15 = 0;
            double? credit16 = 0;
            double? credit17 = 0;
            double? credit18 = 0;
            double? credit19 = 0;
            double? credit20 = 0;
            double? credit21 = 0;
            double? credit22 = 0;
            double? credit23 = 0;
            double? credit24 = 0;
            double? credit25 = 0;
            double? credit26 = 0;

            string[] stdId = new string[90];
            string[] label1 = new string[90];
            string[] label2 = new string[90];
            string[] label3 = new string[90];
            string[] label4 = new string[90];
            string[] label5 = new string[90];
            string[] label6 = new string[90];
            string[] label7 = new string[90];
            string[] label8 = new string[90];
            string[] label9 = new string[90];
            string[] label10 = new string[90];
            string[] label11 = new string[90];
            string[] label12 = new string[90];
            string[] label13 = new string[90];
            string[] label14 = new string[90];
            string[] label15 = new string[90];
            string[] label16 = new string[90];
            string[] label17 = new string[90];
            string[] label18 = new string[90];
            string[] label19 = new string[90];
            string[] label20 = new string[90];
            string[] label21 = new string[90];
            string[] label22 = new string[90];
            string[] label23 = new string[90];
            string[] label24 = new string[90];
            string[] label25 = new string[90];
            string[] label26 = new string[90];
            string[] get1 = new string[90];
            string[] get2 = new string[90];
            string[] get3 = new string[90];
            string[] get4 = new string[90];
            string[] get5 = new string[90];
            string[] get6 = new string[90];
            string[] get7 = new string[90];
            string[] get8 = new string[90];
            string[] get9 = new string[90];
            string[] get10 = new string[90];
            string[] get11 = new string[90];
            string[] get12 = new string[90];
            string[] get13 = new string[90];
            string[] get14 = new string[90];
            string[] get15 = new string[90];
            string[] get16 = new string[90];
            string[] get17 = new string[90];
            string[] get18 = new string[90];
            string[] get19 = new string[90];
            string[] get20 = new string[90];
            string[] get21 = new string[90];
            string[] get22 = new string[90];
            string[] get23 = new string[90];
            string[] get24 = new string[90];
            string[] get25 = new string[90];
            string[] get26 = new string[90];
            double?[] scoreSum = new double?[90];
            double?[] scoreRaw = new double?[90];

            double?[] creditGet1 = new double?[90];
            double?[] creditGet2 = new double?[90];
            double?[] creditGet3 = new double?[90];
            double?[] creditGet4 = new double?[90];
            double?[] creditGet5 = new double?[90];
            double?[] creditGet6 = new double?[90];
            double?[] creditGet7 = new double?[90];
            double?[] creditGet8 = new double?[90];
            double?[] creditGet9 = new double?[90];
            double?[] creditGet10 = new double?[90];
            double?[] creditGet11 = new double?[90];
            double?[] creditGet12 = new double?[90];
            double?[] creditGet13 = new double?[90];
            double?[] creditGet14 = new double?[90];
            double?[] creditGet15 = new double?[90];
            double?[] creditGet16 = new double?[90];
            double?[] creditGet17 = new double?[90];
            double?[] creditGet18 = new double?[90];
            double?[] creditGet19 = new double?[90];
            double?[] creditGet20 = new double?[90];
            double?[] creditGet21 = new double?[90];
            double?[] creditGet22 = new double?[90];
            double?[] creditGet23 = new double?[90];
            double?[] creditGet24 = new double?[90];
            double?[] creditGet25 = new double?[90];
            double?[] creditGet26 = new double?[90];

            int register = 0;
            int count = 0;
            int maxscorecount = 0;
            double? totalweight = 0;

            int maxstd = 0;
            foreach (var check in _db.TUsers.Where(w => w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1") && (w.nStudentStatus == null || w.nStudentStatus == 0) && w.SchoolID == userData.CompanyID))
            {
                var TUserMaster = _dbMaster.TUsers.Where(w => w.nCompany == nCompany.nCompany && w.cType == "0" && w.nSystemID == check.sID).FirstOrDefault();
                if (TUserMaster != null)
                {
                    maxstd = maxstd + 1;
                }
            }

            foreach (var ii in newSortList4)
            {
                var kk = planCourseDTO.Where(w => w.SPlaneId.ToString() == ii.planId).FirstOrDefault();
                if (kk != null)
                {
                    var data2 = _dbGrade.TGrades.Where(w => w.nTerm == nterm && w.sPlaneID.ToString() == ii.planId && w.nTermSubLevel2 == idlv2n &&  w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (data2 != null)
                    {
                        int check1 = 0;

                        foreach (var check in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == data2.nGradeId && w.getSpecial == "-1" && w.getScore100 == "0" && w.cDel == false))
                        {
                            var check3 = _db.TUsers.Where(w => w.sID == check.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                            if (check3 != null)
                            {
                                if (check3.nTermSubLevel2 == data2.nTermSubLevel2 && (check3.nStudentStatus == 0 || check3.nStudentStatus == null))
                                    check1 = check1 + 1;
                            }
                        }

                        if (register == 0 && check1 < maxstd) { plan1 = kk.SPlaneId.ToString(); count++; credit1 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 1 && check1 < maxstd) { plan2 = kk.SPlaneId.ToString(); count++; credit2 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 2 && check1 < maxstd) { plan3 = kk.SPlaneId.ToString(); count++; credit3 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 3 && check1 < maxstd) { plan4 = kk.SPlaneId.ToString(); count++; credit4 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 4 && check1 < maxstd) { plan5 = kk.SPlaneId.ToString(); count++; credit5 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 5 && check1 < maxstd) { plan6 = kk.SPlaneId.ToString(); count++; credit6 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 6 && check1 < maxstd) { plan7 = kk.SPlaneId.ToString(); count++; credit7 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 7 && check1 < maxstd) { plan8 = kk.SPlaneId.ToString(); count++; credit8 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 8 && check1 < maxstd) { plan9 = kk.SPlaneId.ToString(); count++; credit9 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 9 && check1 < maxstd) { plan10 = kk.SPlaneId.ToString(); count++; credit10 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 10 && check1 < maxstd) { plan11 = kk.SPlaneId.ToString(); count++; credit11 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 11 && check1 < maxstd) { plan12 = kk.SPlaneId.ToString(); count++; credit12 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 12 && check1 < maxstd) { plan13 = kk.SPlaneId.ToString(); count++; credit13 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 13 && check1 < maxstd) { plan14 = kk.SPlaneId.ToString(); count++; credit14 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 14 && check1 < maxstd) { plan15 = kk.SPlaneId.ToString(); count++; credit15 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 15 && check1 < maxstd) { plan16 = kk.SPlaneId.ToString(); count++; credit16 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 16 && check1 < maxstd) { plan17 = kk.SPlaneId.ToString(); count++; credit17 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 17 && check1 < maxstd) { plan18 = kk.SPlaneId.ToString(); count++; credit18 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 18 && check1 < maxstd) { plan19 = kk.SPlaneId.ToString(); count++; credit19 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 19 && check1 < maxstd) { plan20 = kk.SPlaneId.ToString(); count++; credit20 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 20 && check1 < maxstd) { plan21 = kk.SPlaneId.ToString(); count++; credit21 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 21 && check1 < maxstd) { plan22 = kk.SPlaneId.ToString(); count++; credit22 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 22 && check1 < maxstd) { plan23 = kk.SPlaneId.ToString(); count++; credit23 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 23 && check1 < maxstd) { plan24 = kk.SPlaneId.ToString(); count++; credit24 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 24 && check1 < maxstd) { plan25 = kk.SPlaneId.ToString(); count++; credit25 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }
                        if (register == 25 && check1 < maxstd) { plan26 = kk.SPlaneId.ToString(); count++; credit26 = kk.NCredit; totalweight = totalweight + kk.NCredit; if (kk.CourseGroupName == "รายวิชาพื้นฐาน" || kk.CourseGroupName == "รายวิชาเพิ่มเติม") maxscorecount++; }

                        if (check1 < maxstd)
                            register = register + 1;
                    }
                }
            }


            var grade1 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan1 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade2 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan2 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade3 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan3 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade4 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan4 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade5 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan5 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade6 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan6 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade7 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan7 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade8 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan8 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade9 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan9 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade10 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan10 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade11 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan11 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade12 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan12 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade13 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan13 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade14 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan14 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade15 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan15 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade16 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan16 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade17 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan17 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade18 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan18 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade19 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan19 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade20 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan20 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade21 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan21 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade22 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan22 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade23 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan23 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade24 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan24 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade25 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan25 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();
            var grade26 = _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == plan26 && w.nTerm == nterm && w.nTermSubLevel2 == idlv2n && w.SchoolID == userData.CompanyID).FirstOrDefault();

            var plane1 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan1).FirstOrDefault();
            var plane2 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan2).FirstOrDefault();
            var plane3 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan3).FirstOrDefault();
            var plane4 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan4).FirstOrDefault();
            var plane5 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan5).FirstOrDefault();
            var plane6 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan6).FirstOrDefault();
            var plane7 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan7).FirstOrDefault();
            var plane8 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan8).FirstOrDefault();
            var plane9 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan9).FirstOrDefault();
            var plane10 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan10).FirstOrDefault();
            var plane11 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan11).FirstOrDefault();
            var plane12 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan12).FirstOrDefault();
            var plane13 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan13).FirstOrDefault();
            var plane14 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan14).FirstOrDefault();
            var plane15 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan15).FirstOrDefault();
            var plane16 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan16).FirstOrDefault();
            var plane17 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan17).FirstOrDefault();
            var plane18 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan18).FirstOrDefault();
            var plane19 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan19).FirstOrDefault();
            var plane20 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan20).FirstOrDefault();
            var plane21 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan21).FirstOrDefault();
            var plane22 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan22).FirstOrDefault();
            var plane23 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan23).FirstOrDefault();
            var plane24 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan24).FirstOrDefault();
            var plane25 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan25).FirstOrDefault();
            var plane26 = planCourseDTO.Where(w => w.SPlaneId.ToString() == plan26).FirstOrDefault();

            int grade1score100 = 0;
            int grade2score100 = 0;
            int grade3score100 = 0;
            int grade4score100 = 0;
            int grade5score100 = 0;
            int grade6score100 = 0;
            int grade7score100 = 0;
            int grade8score100 = 0;
            int grade9score100 = 0;
            int grade10score100 = 0;
            int grade11score100 = 0;
            int grade12score100 = 0;
            int grade13score100 = 0;
            int grade14score100 = 0;
            int grade15score100 = 0;
            int grade16score100 = 0;
            int grade17score100 = 0;
            int grade18score100 = 0;
            int grade19score100 = 0;
            int grade20score100 = 0;
            int grade21score100 = 0;
            int grade22score100 = 0;
            int grade23score100 = 0;
            int grade24score100 = 0;
            int grade25score100 = 0;
            int grade26score100 = 0;

            List<stdList> studentList = new List<stdList>();
            stdList student = new stdList();
            int sorttype1txt2 = 0;

            if (id == "" || id == "99998")
            {
                int number = 0;

                if (grade1 != null)
                {
                    foreach (var detail1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade1.nGradeId && w.cDel == false))
                    {

                        var stddetail = _db.TUsers.Where(w => w.sID == detail1.sID && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1") && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (stddetail != null)
                        {
                            student = new stdList();
                            student.sid = detail1.sID;
                            student.sStudent = stddetail.sStudentID;
                            int n;
                            bool isNumeric = int.TryParse(stddetail.sStudentID, out n);
                            if (isNumeric == true)
                            {
                                student.sort1int = Int32.Parse(stddetail.sStudentID);
                                student.sort1txt = stddetail.sStudentID;
                                student.sort2 = 999999;
                            }
                            else if (stddetail.sStudentID == null || stddetail.sStudentID == "")
                            {
                                student.sort1int = 0;
                                student.sort1txt = "";
                                student.sort2 = 999999;
                            }
                            else
                            {
                                sorttype1txt2 = sorttype1txt2 + 1;
                                student.sort1txt = stddetail.sStudentID;
                                student.sort2 = 999999;
                            }
                            studentList.Add(student);
                        }
                    }
                }


                var newSortList2 = studentList;
                newSortList2 = studentList.OrderBy(x => x.sort1int).ToList();
                if (sorttype1txt2 != 0)
                    newSortList2 = studentList.OrderBy(x => x.sort1txt).ToList();

                if (grade1 != null)
                {
                    number = 0;
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade1.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial } ).ToList();
                    foreach (var data in newSortList2)
                    {
                        //var detail1 = grade1Detail.Where(w => w.nGradeId == grade1.nGradeId && w.sID == data.sid && w.SchoolID == userData.CompanyID && w.cDel == false).FirstOrDefault();
                        var detail1 = grade1Detail.Where(w => w.nGradeId == grade1.nGradeId && w.sID == data.sid).FirstOrDefault();
                        scoreSum[number] = 0;
                        scoreRaw[number] = 0;
                        stdId[number] = detail1.sID.ToString();
                        string get100 = (!string.IsNullOrEmpty(detail1.getScore100))?detail1.getScore100 : "0"; //Common.getScore100(grade1, detail1);
                        grade1score100 = int.Parse(get100);
                        label1[number] = getSpecial(detail1.getSpecial, get100, plane1.CourseGroup);

                        if (plane1.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get1[number] = "";
                        else get1[number] = get100;


                        if (get1[number] != "" & get1[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get1[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get1[number]) * credit1);
                        }
                        creditGet1[number] = creditGain(detail1.getSpecial,
                                              get100,
                                              grade1.sPlaneID, plane1);
                        number = number + 1;
                    }
                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet1[x] = 0;
                    stdId[number] = "";
                    label1[number] = "";
                    get1[number] = "";
                }

                if (grade2 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade2.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail2 = grade1Detail.Where(w => w.nGradeId == grade2.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail2.getScore100) ? detail2.getScore100 : "0";  //Common.getScore100(grade2, detail2);
                        grade2score100 = int.Parse(get100);
                        label2[number] = getSpecial(detail2.getSpecial, get100, plane2.CourseGroup);
                        if (plane2.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get2[number] = "";
                        else get2[number] = get100;

                        if (get2[number] != "" & get2[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get2[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get2[number]) * credit2);
                        }
                        creditGet2[number] = creditGain(detail2.getSpecial,
                                              get100,
                                              grade2.sPlaneID, plane2);
                        number = number + 1;
                    }
                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet2[x] = 0;
                }


                if (grade3 != null)
                {
                    number = 0;
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade3.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    foreach (var data in newSortList2)
                    {
                        var detail3 = grade1Detail.Where(w => w.nGradeId == grade3.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail3.getScore100)? detail3.getScore100 : "0"; //Common.getScore100(grade3, detail3);
                        grade3score100 = int.Parse(get100);
                        label3[number] = getSpecial(detail3.getSpecial, get100, plane3.CourseGroup);
                        if (plane3.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get3[number] = "";
                        else get3[number] = get100;
                        if (get3[number] != "" & get3[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get3[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get3[number]) * credit3);
                        }
                        creditGet3[number] = creditGain(detail3.getSpecial,
                                              get100,
                                              grade3.sPlaneID, plane3);
                        number = number + 1;
                    }
                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet3[x] = 0;
                }

                if (grade4 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade4.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail4 = grade1Detail.Where(w => w.nGradeId == grade4.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail4.getScore100) ? detail4.getScore100 : "0"; //Common.getScore100(grade4, detail4);
                        grade4score100 = int.Parse(get100);
                        label4[number] = getSpecial(detail4.getSpecial, get100, plane4.CourseGroup);
                        if (plane4.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get4[number] = "";
                        else get4[number] = get100;
                        if (get4[number] != "" & get4[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get4[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get4[number]) * credit4);
                        }
                        creditGet4[number] = creditGain(detail4.getSpecial,
                                              get100,
                                              grade4.sPlaneID, plane4);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet4[x] = 0;
                }

                if (grade5 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade5.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail5 = grade1Detail.Where(w => w.nGradeId == grade5.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail5.getScore100) ? detail5.getScore100 : "0"; //Common.getScore100(grade5, detail5);
                        grade5score100 = int.Parse(get100);
                        label5[number] = getSpecial(detail5.getSpecial, get100, plane5.CourseGroup);
                        if (plane5.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get5[number] = "";
                        else get5[number] = get100;
                        if (get5[number] != "" & get5[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get5[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get5[number]) * credit5);
                        }
                        creditGet5[number] = creditGain(detail5.getSpecial,
                                              get100,
                                              grade5.sPlaneID, plane5);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet5[x] = 0;
                }

                if (grade6 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade6.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail6 = grade1Detail.Where(w => w.nGradeId == grade6.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail6.getScore100) ? detail6.getScore100 : "0"; //Common.getScore100(grade6, detail6);
                        grade6score100 = int.Parse(get100);
                        label6[number] = getSpecial(detail6.getSpecial, get100, plane6.CourseGroup);
                        if (plane6.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get6[number] = "";
                        else get6[number] = get100;
                        if (get6[number] != "" & get6[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get6[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get6[number]) * credit6);
                        }
                        creditGet6[number] = creditGain(detail6.getSpecial,
                                              get100,
                                              grade6.sPlaneID, plane6);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet6[x] = 0;
                }

                if (grade7 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade7.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail7 = grade1Detail.Where(w => w.nGradeId == grade7.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail7.getScore100) ? detail7.getScore100 : "0";  //Common.getScore100(grade7, detail7);
                        grade7score100 = int.Parse(get100);
                        label7[number] = getSpecial(detail7.getSpecial, get100, plane7.CourseGroup);
                        if (plane7.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get7[number] = "";
                        else get7[number] = get100;
                        if (get7[number] != "" & get7[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get7[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get7[number]) * credit7);
                        }
                        creditGet7[number] = creditGain(detail7.getSpecial,
                                              get100,
                                              grade7.sPlaneID, plane7);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet7[x] = 0;
                }

                if (grade8 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade8.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail8 = grade1Detail.Where(w => w.nGradeId == grade8.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail8.getScore100)? detail8.getScore100 : "0";
                        grade8score100 = int.Parse(get100);
                        label8[number] = getSpecial(detail8.getSpecial, get100, plane8.CourseGroup);
                        if (plane8.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get8[number] = "";
                        else get8[number] = get100;
                        if (get8[number] != "" & get8[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get8[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get8[number]) * credit8);
                        }
                        creditGet8[number] = creditGain(detail8.getSpecial,
                                              get100,
                                              grade8.sPlaneID, plane8);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet8[x] = 0;
                }

                if (grade9 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade9.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail9 = grade1Detail.Where(w => w.nGradeId == grade9.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail9.getScore100) ? detail9.getScore100 : "0";// Common.getScore100(grade9, detail9);
                        grade9score100 = int.Parse(get100);
                        label9[number] = getSpecial(detail9.getSpecial, get100, plane9.CourseGroup);
                        if (plane9.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get9[number] = "";
                        else get9[number] = get100;
                        if (get9[number] != "" & get9[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get9[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get9[number]) * credit9);
                        }
                        creditGet9[number] = creditGain(detail9.getSpecial,
                                              get100,
                                              grade9.sPlaneID, plane9);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet9[x] = 0;
                }

                if (grade10 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade10.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail10 = grade1Detail.Where(w => w.nGradeId == grade10.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail10.getScore100) ? detail10.getScore100 : "0"; //Common.getScore100(grade10, detail10);
                        grade10score100 = int.Parse(get100);
                        label10[number] = getSpecial(detail10.getSpecial, get100, plane10.CourseGroup);
                        if (plane10.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get10[number] = "";
                        else get10[number] = get100;
                        if (get10[number] != "" & get10[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get10[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get10[number]) * credit10);
                        }
                        creditGet10[number] = creditGain(detail10.getSpecial,
                                              get100,
                                              grade10.sPlaneID, plane10);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet10[x] = 0;
                }

                if (grade11 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade11.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail11 = grade1Detail.Where(w => w.nGradeId == grade11.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail11.getScore100) ? detail11.getScore100 : "0"; //Common.getScore100(grade11, detail11);
                        grade11score100 = int.Parse(get100);
                        label11[number] = getSpecial(detail11.getSpecial, get100, plane11.CourseGroup);
                        if (plane11.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get11[number] = "";
                        else get11[number] = get100;
                        if (get11[number] != "" & get11[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get11[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get11[number]) * credit11);
                        }
                        creditGet11[number] = creditGain(detail11.getSpecial,
                                              get100,
                                              grade11.sPlaneID, plane11);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet11[x] = 0;
                }

                if (grade12 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade12.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail12 = grade1Detail.Where(w => w.nGradeId == grade12.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail12.getScore100) ? detail12.getScore100 : "0"; //Common.getScore100(grade12, detail12);
                        grade12score100 = int.Parse(get100);
                        label12[number] = getSpecial(detail12.getSpecial, get100, plane12.CourseGroup);
                        if (plane12.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get12[number] = "";
                        else get12[number] = get100;
                        if (get12[number] != "" & get12[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get12[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get12[number]) * credit12);
                        }
                        creditGet12[number] = creditGain(detail12.getSpecial,
                                              get100,
                                              grade12.sPlaneID, plane12);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet12[x] = 0;
                }

                if (grade13 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade13.nGradeId &&w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail13 = grade1Detail.Where(w => w.nGradeId == grade13.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail13.getScore100) ? detail13.getScore100 : "0";// Common.getScore100(grade13, detail13);
                        grade13score100 = int.Parse(get100);
                        label13[number] = getSpecial(detail13.getSpecial, get100, plane13.CourseGroup);
                        if (plane13.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get13[number] = "";
                        else get13[number] = get100;
                        if (get13[number] != "" & get13[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get13[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get13[number]) * credit13);
                        }
                        creditGet13[number] = creditGain(detail13.getSpecial,
                                              get100,
                                              grade13.sPlaneID, plane13);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet13[x] = 0;
                }

                if (grade14 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade14.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail14 = grade1Detail.Where(w => w.nGradeId == grade14.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail14.getScore100) ? detail14.getScore100 : "0"; //Common.getScore100(grade14, detail14);
                        grade14score100 = int.Parse(get100);
                        label14[number] = getSpecial(detail14.getSpecial, get100, plane14.CourseGroup);
                        if (plane14.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get14[number] = "";
                        else get14[number] = get100;
                        if (get14[number] != "" & get14[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get14[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get14[number]) * credit14);
                        }
                        creditGet14[number] = creditGain(detail14.getSpecial,
                                              get100,
                                              grade14.sPlaneID, plane14);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet14[x] = 0;
                }

                if (grade15 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade15.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail15 = grade1Detail.Where(w => w.nGradeId == grade15.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail15.getScore100) ? detail15.getScore100 : "0"; //Common.getScore100(grade15, detail15);
                        grade15score100 = int.Parse(get100);
                        label15[number] = getSpecial(detail15.getSpecial, get100, plane15.CourseGroup);
                        if (plane15.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get15[number] = "";
                        else get15[number] = get100;
                        if (get15[number] != "" & get15[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get15[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get15[number]) * credit15);
                        }
                        creditGet15[number] = creditGain(detail15.getSpecial,
                                              get100,
                                              grade15.sPlaneID, plane15);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet15[x] = 0;
                }

                if (grade16 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade16.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail16 = grade1Detail.Where(w => w.nGradeId == grade16.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail16.getScore100) ? detail16.getScore100 : "0"; //Common.getScore100(grade16, detail16);
                        grade16score100 = int.Parse(get100);
                        label16[number] = getSpecial(detail16.getSpecial, get100, plane16.CourseGroup);
                        if (plane16.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get16[number] = "";
                        else get16[number] = get100;
                        if (get16[number] != "" & get16[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get16[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get16[number]) * credit16);
                        }
                        creditGet16[number] = creditGain(detail16.getSpecial,
                                              get100,
                                              grade16.sPlaneID, plane16);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet16[x] = 0;
                }

                if (grade17 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade17.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail17 = grade1Detail.Where(w => w.nGradeId == grade17.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail17.getScore100) ? detail17.getScore100 : "0"; //Common.getScore100(grade17, detail17);
                        grade17score100 = int.Parse(get100);
                        label17[number] = getSpecial(detail17.getSpecial, get100, plane17.CourseGroup);
                        if (plane17.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get17[number] = "";
                        else get17[number] = get100;
                        if (get17[number] != "" & get17[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get17[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get17[number]) * credit17);
                        }
                        creditGet17[number] = creditGain(detail17.getSpecial,
                                              get100,
                                              grade17.sPlaneID, plane17);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet17[x] = 0;
                }

                if (grade18 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade18.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail18 = grade1Detail.Where(w => w.nGradeId == grade18.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail18.getScore100) ? detail18.getScore100 : "0"; //Common.getScore100(grade18, detail18);
                        grade18score100 = int.Parse(get100);
                        label18[number] = getSpecial(detail18.getSpecial, get100, plane18.CourseGroup);
                        if (plane18.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get18[number] = "";
                        else get18[number] = get100;
                        if (get18[number] != "" & get18[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get18[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get18[number]) * credit18);
                        }
                        creditGet18[number] = creditGain(detail18.getSpecial,
                                              get100,
                                              grade18.sPlaneID, plane18);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet18[x] = 0;
                }

                if (grade19 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade19.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();
                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail19 = grade1Detail.Where(w => w.nGradeId == grade19.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail19.getScore100) ? detail19.getScore100 : "0"; //Common.getScore100(grade19, detail19);
                        grade19score100 = int.Parse(get100);
                        label19[number] = getSpecial(detail19.getSpecial, get100, plane19.CourseGroup);
                        if (plane19.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get19[number] = "";
                        else get19[number] = get100;
                        if (get19[number] != "" & get19[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get19[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get19[number]) * credit19);
                        }
                        creditGet19[number] = creditGain(detail19.getSpecial,
                                              get100,
                                              grade19.sPlaneID, plane19);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet19[x] = 0;
                }

                if (grade20 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade20.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail20 = grade1Detail.Where(w => w.nGradeId == grade20.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail20.getScore100) ? detail20.getScore100 : "0"; //Common.getScore100(grade20, detail20);
                        grade20score100 = int.Parse(get100);
                        label20[number] = getSpecial(detail20.getSpecial, get100, plane20.CourseGroup);
                        if (plane20.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get20[number] = "";
                        else get20[number] = get100;
                        if (get20[number] != "" & get20[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get20[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get20[number]) * credit20);
                        }
                        creditGet20[number] = creditGain(detail20.getSpecial,
                                              get100,
                                              grade20.sPlaneID, plane20);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet20[x] = 0;
                }

                if (grade21 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade21.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail21 = grade1Detail.Where(w => w.nGradeId == grade21.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail21.getScore100) ? detail21.getScore100 : "0"; //Common.getScore100(grade21, detail21);
                        grade21score100 = int.Parse(get100);
                        label21[number] = getSpecial(detail21.getSpecial, get100, plane21.CourseGroup);
                        if (plane21.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get21[number] = "";
                        else get21[number] = get100;
                        if (get21[number] != "" & get21[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get21[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get21[number]) * credit21);
                        }
                        creditGet21[number] = creditGain(detail21.getSpecial,
                                              get100,
                                              grade21.sPlaneID, plane21);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet21[x] = 0;
                }

                if (grade22 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade22.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail22 = grade1Detail.Where(w => w.nGradeId == grade22.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail22.getScore100) ? detail22.getScore100 : "0"; //Common.getScore100(grade22, detail22);
                        grade22score100 = int.Parse(get100);
                        label22[number] = getSpecial(detail22.getSpecial, get100, plane22.CourseGroup);
                        if (plane22.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get22[number] = "";
                        else get22[number] = get100;
                        if (get22[number] != "" & get22[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get22[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get22[number]) * credit22);
                        }
                        creditGet22[number] = creditGain(detail22.getSpecial,
                                              get100,
                                              grade22.sPlaneID, plane22);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet22[x] = 0;
                }

                if (grade23 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade23.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail23 = grade1Detail.Where(w => w.nGradeId == grade23.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail23.getScore100) ? detail23.getScore100 : "0"; //Common.getScore100(grade23, detail23);
                        grade23score100 = int.Parse(get100);
                        label23[number] = getSpecial(detail23.getSpecial, get100, plane23.CourseGroup);
                        if (plane23.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get23[number] = "";
                        else get23[number] = get100;
                        if (get23[number] != "" & get23[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get23[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get23[number]) * credit23);
                        }
                        creditGet23[number] = creditGain(detail23.getSpecial,
                                              get100,
                                              grade23.sPlaneID, plane23);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet23[x] = 0;
                }

                if (grade24 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade24.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail24 = grade1Detail.Where(w => w.nGradeId == grade24.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail24.getScore100) ? detail24.getScore100 : "0"; //Common.getScore100(grade24, detail24);
                        grade24score100 = int.Parse(get100);
                        label24[number] = getSpecial(detail24.getSpecial, get100, plane24.CourseGroup);
                        if (plane24.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get24[number] = "";
                        else get24[number] = get100;
                        if (get24[number] != "" & get24[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get24[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get24[number]) * credit24);
                        }
                        creditGet24[number] = creditGain(detail24.getSpecial,
                                              get100,
                                              grade24.sPlaneID, plane24);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet24[x] = 0;
                }

                if (grade25 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade25.nGradeId &&  w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail25 = grade1Detail.Where(w => w.nGradeId == grade25.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail25.getScore100) ? detail25.getScore100 : "0"; //Common.getScore100(grade25, detail25);
                        grade25score100 = int.Parse(get100);
                        label25[number] = getSpecial(detail25.getSpecial, get100, plane25.CourseGroup);
                        if (plane25.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get25[number] = "";
                        else get25[number] = get100;
                        if (get25[number] != "" & get25[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get25[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get25[number]) * credit25);
                        }
                        creditGet25[number] = creditGain(detail25.getSpecial,
                                              get100,
                                              grade25.sPlaneID, plane25);
                        number = number + 1;
                    }

                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet25[x] = 0;
                }

                if (grade26 != null)
                {
                    var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade26.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                    number = 0;
                    foreach (var data in newSortList2)
                    {
                        var detail26 = grade1Detail.Where(w => w.nGradeId == grade26.nGradeId && w.sID == data.sid).FirstOrDefault();

                        string get100 = !string.IsNullOrEmpty(detail26.getScore100) ? detail26.getScore100 : "0"; //Common.getScore100(grade26, detail26);
                        grade26score100 = int.Parse(get100);
                        label26[number] = getSpecial(detail26.getSpecial, get100, plane26.CourseGroup);
                        if (plane26.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                            get26[number] = "";
                        else get26[number] = get100;
                        if (get26[number] != "" & get26[number] != null)
                        {
                            scoreRaw[number] = scoreRaw[number] + Double.Parse(get26[number]);
                            scoreSum[number] = scoreSum[number] + (Double.Parse(get26[number]) * credit26);
                        }
                        creditGet26[number] = creditGain(detail26.getSpecial,
                                              get100,
                                              grade26.sPlaneID, plane26);
                        number = number + 1;
                    }
                }
                else
                {
                    for (int x = 0; x < 90; x++)
                        creditGet26[x] = 0;
                }
            }
            else
            {
                var grade1Detail = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade1.nGradeId && w.cDel == false).Select(s => new { s.nGradeId, s.getScore100, s.sID, s.getSpecial }).ToList();

                if (grade1 != null)
                {
                    var detail1 = grade1Detail.Where(w => w.nGradeId == grade1.nGradeId && w.sID == idn).FirstOrDefault();
                    scoreSum[0] = 0;
                    scoreRaw[0] = 0;
                    stdId[0] = detail1.sID.ToString();

                    string get100 = !string.IsNullOrEmpty(detail1.getScore100) ? detail1.getScore100 : "0"; //Common.getScore100(grade1, detail1);
                    grade1score100 = int.Parse(get100);
                    label1[0] = getSpecial(detail1.getSpecial, get100, plane1.CourseGroup);

                    if (plane1.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get1[0] = "";
                    else get1[0] = get100;
                    if (get1[0] != "" & get1[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get1[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get1[0]) * credit1);
                    }
                    creditGet1[0] = creditGain(detail1.getSpecial,
                                              get100,
                                              grade1.sPlaneID, plane1);
                }
                else
                {
                    stdId[0] = "";
                    label1[0] = "";
                    get1[0] = "";
                    creditGet1[0] = 0;
                }

                if (grade2 != null)
                {
                    var detail2 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade2.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail2.getScore100) ? detail2.getScore100 : "0"; //Common.getScore100(grade2, detail2);
                    grade2score100 = int.Parse(get100);
                    label2[0] = getSpecial(detail2.getSpecial, get100, plane2.CourseGroup);
                    if (plane2.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get2[0] = "";
                    else get2[0] = get100;
                    if (get2[0] != "" & get2[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get2[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get2[0]) * credit2);
                    }
                    creditGet2[0] = creditGain(detail2.getSpecial,
                                              get100,
                                              grade2.sPlaneID, plane2);
                }
                else
                {
                    label2[0] = "";
                    get2[0] = "";
                    creditGet2[0] = 0;
                }

                if (grade3 != null)
                {
                    var detail3 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade3.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail3.getScore100) ? detail3.getScore100 : "0"; //Common.getScore100(grade3, detail3);
                    grade3score100 = int.Parse(get100);
                    label3[0] = getSpecial(detail3.getSpecial, get100, plane3.CourseGroup);
                    if (plane3.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get3[0] = "";
                    else get3[0] = get100;
                    if (get3[0] != "" & get3[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get3[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get3[0]) * credit3);
                    }
                    creditGet3[0] = creditGain(detail3.getSpecial,
                                              get100,
                                              grade3.sPlaneID, plane3);
                }
                else
                {
                    creditGet3[0] = 0;
                    label3[0] = "";
                    get3[0] = "";
                }

                if (grade4 != null)
                {

                    var detail4 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade4.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail4.getScore100) ? detail4.getScore100 : "0"; //Common.getScore100(grade4, detail4);
                    grade4score100 = int.Parse(get100);
                    label4[0] = getSpecial(detail4.getSpecial, get100, plane4.CourseGroup);
                    if (plane4.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get4[0] = "";
                    else get4[0] = get100;
                    if (get4[0] != "" & get4[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get4[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get4[0]) * credit4);
                    }
                    creditGet4[0] = creditGain(detail4.getSpecial,
                                              get100,
                                              grade4.sPlaneID, plane4);
                }
                else
                {
                    creditGet4[0] = 0;
                    label4[0] = "";
                    get4[0] = "";
                }

                if (grade5 != null)
                {

                    var detail5 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade5.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail5.getScore100) ? detail5.getScore100 : "0"; //Common.getScore100(grade5, detail5);
                    grade5score100 = int.Parse(get100);
                    label5[0] = getSpecial(detail5.getSpecial, get100, plane5.CourseGroup);
                    if (plane5.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get5[0] = "";
                    else get5[0] = get100;
                    if (get5[0] != "" & get5[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get5[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get5[0]) * credit5);
                    }
                    creditGet5[0] = creditGain(detail5.getSpecial,
                                              get100,
                                              grade5.sPlaneID, plane5);
                }
                else
                {
                    creditGet5[0] = 0;
                    label5[0] = "";
                    get5[0] = "";
                }

                if (grade6 != null)
                {

                    var detail6 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade6.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail6.getScore100) ? detail6.getScore100 : "0"; //Common.getScore100(grade6, detail6);
                    grade6score100 = int.Parse(get100);
                    label6[0] = getSpecial(detail6.getSpecial, get100, plane6.CourseGroup);
                    if (plane6.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get6[0] = "";
                    else get6[0] = get100;
                    if (get6[0] != "" & get6[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get6[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get6[0]) * credit6);

                    }
                    creditGet6[0] = creditGain(detail6.getSpecial,
                                              get100,
                                              grade6.sPlaneID, plane6);
                }
                else
                {
                    creditGet6[0] = 0;
                    label6[0] = "";
                    get6[0] = "";
                }

                if (grade7 != null)
                {

                    var detail7 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade7.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail7.getScore100) ? detail7.getScore100 : "0"; //Common.getScore100(grade7, detail7);
                    grade7score100 = int.Parse(get100);
                    label7[0] = getSpecial(detail7.getSpecial, get100, plane7.CourseGroup);
                    if (plane7.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get7[0] = "";
                    else get7[0] = get100;
                    if (get7[0] != "" & get7[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get7[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get7[0]) * credit7);

                    }
                    creditGet7[0] = creditGain(detail7.getSpecial,
                                              get100,
                                              grade7.sPlaneID, plane7);
                }
                else
                {
                    creditGet7[0] = 0;
                    label7[0] = "";
                    get7[0] = "";
                }

                if (grade8 != null)
                {

                    var detail8 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade8.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail8.getScore100) ? detail8.getScore100 : "0"; //Common.getScore100(grade8, detail8);
                    grade8score100 = int.Parse(get100);
                    label8[0] = getSpecial(detail8.getSpecial, get100, plane8.CourseGroup);
                    if (plane8.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get8[0] = "";
                    else get8[0] = get100;
                    if (get8[0] != "" & get8[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get8[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get8[0]) * credit8);

                    }
                    creditGet8[0] = creditGain(detail8.getSpecial,
                                              get100,
                                              grade8.sPlaneID, plane8);
                }
                else
                {
                    creditGet8[0] = 0;
                    label8[0] = "";
                    get8[0] = "";
                }

                if (grade9 != null)
                {

                    var detail9 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade9.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail9.getScore100) ? detail9.getScore100 : "0"; //Common.getScore100(grade9, detail9);
                    grade9score100 = int.Parse(get100);
                    label9[0] = getSpecial(detail9.getSpecial, get100, plane9.CourseGroup);
                    if (plane9.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get9[0] = "";
                    else get9[0] = get100;
                    if (get9[0] != "" & get9[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get9[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get9[0]) * credit9);
                    }
                    creditGet9[0] = creditGain(detail9.getSpecial,
                                              get100,
                                              grade9.sPlaneID, plane9);
                }
                else
                {
                    creditGet9[0] = 0;
                    label9[0] = "";
                    get9[0] = "";
                }

                if (grade10 != null)
                {

                    var detail10 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade10.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail10.getScore100) ? detail10.getScore100 : "0"; //Common.getScore100(grade10, detail10);
                    grade10score100 = int.Parse(get100);
                    label10[0] = getSpecial(detail10.getSpecial, get100, plane10.CourseGroup);
                    if (plane10.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get10[0] = "";
                    else get10[0] = get100;
                    if (get10[0] != "" & get10[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get10[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get10[0]) * credit10);

                    }
                    creditGet10[0] = creditGain(detail10.getSpecial,
                                              get100,
                                              grade10.sPlaneID, plane10);
                }
                else
                {
                    creditGet10[0] = 0;
                    label10[0] = "";
                    get10[0] = "";
                }

                if (grade11 != null)
                {

                    var detail11 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade11.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail11.getScore100) ? detail11.getScore100 : "0"; //Common.getScore100(grade11, detail11);
                    grade11score100 = int.Parse(get100);
                    label11[0] = getSpecial(detail11.getSpecial, get100, plane11.CourseGroup);
                    if (plane11.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get11[0] = "";
                    else get11[0] = get100;
                    if (get11[0] != "" & get11[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get11[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get11[0]) * credit11);

                    }
                    creditGet11[0] = creditGain(detail11.getSpecial,
                                              get100,
                                              grade11.sPlaneID, plane11);
                }
                else
                {
                    creditGet11[0] = 0;
                    label11[0] = "";
                    get11[0] = "";
                }

                if (grade12 != null)
                {

                    var detail12 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade12.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();


                    string get100 = !string.IsNullOrEmpty(detail12.getScore100) ? detail12.getScore100 : "0"; //Common.getScore100(grade12, detail12);
                    grade12score100 = int.Parse(get100);
                    label12[0] = getSpecial(detail12.getSpecial, get100, plane12.CourseGroup);
                    if (plane12.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get12[0] = "";
                    else get12[0] = get100;
                    if (get12[0] != "" & get12[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get12[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get12[0]) * credit12);

                    }
                    creditGet12[0] = creditGain(detail12.getSpecial,
                                              get100,
                                              grade12.sPlaneID, plane12);
                }
                else
                {
                    creditGet12[0] = 0;
                    label12[0] = "";
                    get12[0] = "";
                }

                if (grade13 != null)
                {
                    var detail13 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade13.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail13.getScore100) ? detail13.getScore100 : "0"; //Common.getScore100(grade13, detail13);
                    grade13score100 = int.Parse(get100);
                    label13[0] = getSpecial(detail13.getSpecial, get100, plane13.CourseGroup);
                    if (plane13.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get13[0] = "";
                    else get13[0] = get100;
                    if (get13[0] != "" & get13[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get13[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get13[0]) * credit13);
                    }
                    creditGet13[0] = creditGain(detail13.getSpecial,
                                              get100,
                                              grade13.sPlaneID, plane13);
                }
                else
                {
                    creditGet13[0] = 0;
                    label13[0] = "";
                    get13[0] = "";
                }

                if (grade14 != null)
                {
                    var detail14 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade14.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail14.getScore100) ? detail14.getScore100 : "0"; //Common.getScore100(grade14, detail14);
                    grade14score100 = int.Parse(get100);
                    label14[0] = getSpecial(detail14.getSpecial, get100, plane14.CourseGroup);
                    if (plane14.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get14[0] = "";
                    else get14[0] = get100;
                    if (get14[0] != "" & get14[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get14[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get14[0]) * credit14);
                    }
                    creditGet14[0] = creditGain(detail14.getSpecial,
                                             get100,
                                              grade14.sPlaneID, plane14);
                }
                else
                {
                    creditGet14[0] = 0;
                    label14[0] = "";
                    get14[0] = "";
                }

                if (grade15 != null)
                {
                    var detail15 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade15.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail15.getScore100) ? detail15.getScore100 : "0"; //Common.getScore100(grade15, detail15);
                    grade15score100 = int.Parse(get100);
                    label15[0] = getSpecial(detail15.getSpecial, get100, plane15.CourseGroup);
                    if (plane15.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get15[0] = "";
                    else get15[0] = get100;
                    if (get15[0] != "" & get15[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get15[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get15[0]) * credit15);
                    }
                    creditGet15[0] = creditGain(detail15.getSpecial,
                                              get100,
                                              grade15.sPlaneID, plane15);
                }
                else
                {
                    creditGet15[0] = 0;
                    label15[0] = "";
                    get15[0] = "";
                }

                if (grade16 != null)
                {
                    var detail16 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade16.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail16.getScore100) ? detail16.getScore100 : "0"; //Common.getScore100(grade16, detail16);
                    grade16score100 = int.Parse(get100);
                    label16[0] = getSpecial(detail16.getSpecial, get100, plane16.CourseGroup);
                    if (plane16.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get16[0] = "";
                    else get16[0] = get100;
                    if (get16[0] != "" & get16[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get16[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get16[0]) * credit16);
                    }
                    creditGet16[0] = creditGain(detail16.getSpecial,
                                              get100,
                                              grade16.sPlaneID, plane16);
                }
                else
                {
                    creditGet16[0] = 0;
                    label16[0] = "";
                    get16[0] = "";
                }

                if (grade17 != null)
                {
                    var detail17 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade17.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail17.getScore100) ? detail17.getScore100 : "0"; //Common.getScore100(grade17, detail17);
                    grade17score100 = int.Parse(get100);
                    label17[0] = getSpecial(detail17.getSpecial, get100, plane17.CourseGroup);
                    if (plane17.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get17[0] = "";
                    else get17[0] = get100;
                    if (get17[0] != "" & get17[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get17[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get17[0]) * credit17);
                    }
                    creditGet17[0] = creditGain(detail17.getSpecial,
                                               get100,
                                               grade17.sPlaneID, plane17);
                }
                else
                {
                    creditGet17[0] = 0;
                    label17[0] = "";
                    get17[0] = "";
                }

                if (grade18 != null)
                {
                    var detail18 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade18.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail18.getScore100) ? detail18.getScore100 : "0"; //Common.getScore100(grade18, detail18);
                    grade18score100 = int.Parse(get100);
                    label18[0] = getSpecial(detail18.getSpecial, get100, plane18.CourseGroup);
                    if (plane18.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get18[0] = "";
                    else get18[0] = get100;
                    if (get18[0] != "" & get18[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get18[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get18[0]) * credit18);
                    }
                    creditGet18[0] = creditGain(detail18.getSpecial,
                                              get100,
                                              grade18.sPlaneID, plane18);
                }
                else
                {
                    creditGet18[0] = 0;
                    label18[0] = "";
                    get18[0] = "";
                }

                if (grade19 != null)
                {
                    var detail19 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade19.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail19.getScore100) ? detail19.getScore100 : "0"; //Common.getScore100(grade19, detail19);
                    grade19score100 = int.Parse(get100);
                    label19[0] = getSpecial(detail19.getSpecial, get100, plane19.CourseGroup);
                    if (plane19.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get19[0] = "";
                    else get19[0] = get100;
                    if (get19[0] != "" & get19[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get19[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get19[0]) * credit19);
                    }
                    creditGet19[0] = creditGain(detail19.getSpecial,
                                              get100,
                                              grade19.sPlaneID, plane19);
                }
                else
                {
                    creditGet19[0] = 0;
                    label19[0] = "";
                    get19[0] = "";
                }

                if (grade20 != null)
                {
                    var detail20 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade20.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail20.getScore100) ? detail20.getScore100 : "0"; //Common.getScore100(grade20, detail20);
                    grade20score100 = int.Parse(get100);
                    label20[0] = getSpecial(detail20.getSpecial, get100, plane20.CourseGroup);
                    if (plane20.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get20[0] = "";
                    else get20[0] = get100;
                    if (get20[0] != "" & get20[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get20[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get20[0]) * credit20);
                    }
                    creditGet20[0] = creditGain(detail20.getSpecial,
                                              get100,
                                              grade20.sPlaneID, plane20);
                }
                else
                {
                    creditGet20[0] = 0;
                    label20[0] = "";
                    get20[0] = "";
                }

                if (grade21 != null)
                {
                    var detail21 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade21.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail21.getScore100) ? detail21.getScore100 : "0"; //Common.getScore100(grade21, detail21);
                    grade21score100 = int.Parse(get100);
                    label21[0] = getSpecial(detail21.getSpecial, get100, plane21.CourseGroup);
                    if (plane21.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get21[0] = "";
                    else get21[0] = get100;
                    if (get21[0] != "" & get21[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get21[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get21[0]) * credit21);
                    }
                    creditGet21[0] = creditGain(detail21.getSpecial,
                                              get100,
                                              grade21.sPlaneID, plane21);
                }
                else
                {
                    creditGet21[0] = 0;
                    label21[0] = "";
                    get21[0] = "";
                }

                if (grade22 != null)
                {
                    var detail22 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade22.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail22.getScore100) ? detail22.getScore100 : "0"; //Common.getScore100(grade22, detail22);
                    grade22score100 = int.Parse(get100);
                    label22[0] = getSpecial(detail22.getSpecial, get100, plane22.CourseGroup);
                    if (plane22.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get22[0] = "";
                    else get22[0] = get100;
                    if (get22[0] != "" & get22[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get22[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get22[0]) * credit22);
                    }
                    creditGet22[0] = creditGain(detail22.getSpecial,
                                              get100,
                                              grade22.sPlaneID, plane22);
                }
                else
                {
                    creditGet22[0] = 0;
                    label22[0] = "";
                    get22[0] = "";
                }

                if (grade23 != null)
                {
                    var detail23 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade23.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail23.getScore100) ? detail23.getScore100 : "0"; //Common.getScore100(grade23, detail23);
                    grade23score100 = int.Parse(get100);
                    label23[0] = getSpecial(detail23.getSpecial, get100, plane23.CourseGroup);
                    if (plane23.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get23[0] = "";
                    else get23[0] = get100;
                    if (get23[0] != "" & get23[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get23[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get23[0]) * credit23);
                    }
                    creditGet23[0] = creditGain(detail23.getSpecial,
                                              get100,
                                              grade23.sPlaneID, plane23);
                }
                else
                {
                    creditGet23[0] = 0;
                    label23[0] = "";
                    get23[0] = "";
                }

                if (grade24 != null)
                {
                    var detail24 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade24.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail24.getScore100) ? detail24.getScore100 : "0"; //Common.getScore100(grade24, detail24);
                    grade24score100 = int.Parse(get100);
                    label24[0] = getSpecial(detail24.getSpecial, get100, plane24.CourseGroup);
                    if (plane24.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get24[0] = "";
                    else get24[0] = get100;
                    if (get24[0] != "" & get24[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get24[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get24[0]) * credit24);
                    }
                    creditGet24[0] = creditGain(detail24.getSpecial,
                                              get100,
                                              grade24.sPlaneID, plane24);
                }
                else
                {
                    creditGet24[0] = 0;
                    label24[0] = "";
                    get24[0] = "";
                }

                if (grade25 != null)
                {
                    var detail25 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade25.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail25.getScore100) ? detail25.getScore100 : "0"; //Common.getScore100(grade25, detail25);
                    grade25score100 = int.Parse(get100);
                    label25[0] = getSpecial(detail25.getSpecial, get100, plane25.CourseGroup);
                    if (plane25.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get25[0] = "";
                    else get25[0] = get100;
                    if (get25[0] != "" & get25[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get25[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get25[0]) * credit25);
                    }
                    creditGet25[0] = creditGain(detail25.getSpecial,
                                               get100,
                                               grade25.sPlaneID, plane25);
                }
                else
                {
                    creditGet25[0] = 0;
                    label25[0] = "";
                    get25[0] = "";
                }

                if (grade26 != null)
                {
                    var detail26 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == grade26.nGradeId && w.sID == idn && w.cDel == false).FirstOrDefault();

                    string get100 = !string.IsNullOrEmpty(detail26.getScore100) ? detail26.getScore100 : "0"; //Common.getScore100(grade26, detail26);
                    grade26score100 = int.Parse(get100);
                    label26[0] = getSpecial(detail26.getSpecial, get100, plane26.CourseGroup);
                    if (plane26.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน")
                        get26[0] = "";
                    else get26[0] = get100;
                    if (get26[0] != "" & get26[0] != null)
                    {
                        scoreRaw[0] = scoreRaw[0] + Double.Parse(get26[0]);
                        scoreSum[0] = scoreSum[0] + (Double.Parse(get26[0]) * credit26);
                    }

                    creditGet26[0] = creditGain(detail26.getSpecial, get100, grade26.sPlaneID, plane26);


                }
                else
                {
                    creditGet26[0] = 0;
                    label26[0] = "";
                    get26[0] = "";
                }
            }

            PlanDetail Detail = new PlanDetail();
            List<PlanDetail> DetailList = new List<PlanDetail>();

            var newSortList3 = studentList;
            newSortList3 = studentList.OrderBy(x => x.sort1int).ToList();
            if (sorttype1txt2 != 0)
                newSortList3 = studentList.OrderBy(x => x.sort1txt).ToList();

            int sorttype1txt = 0;

            if (id == "" || id == "99998")
            {
                var q_studentId = newSortList3.Select(s => s.sid).ToList();
                var q_gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && q_studentId.Contains(w.sID) && w.cDel == false).ToList();
                var q_studentData = _db.TUsers.Where(w => q_studentId.Contains(w.sID) && w.SchoolID == userData.CompanyID).ToList();
                var q_plane = planCourseDTO;
                var q_title = _db.TTitleLists.ToList();
                var q_gradeData = _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID).ToList();
                int x = 0;
                foreach (var stdid in newSortList3)
                {
                    Detail = new PlanDetail();
                    var std = q_studentData.Where(w => w.sID == stdid.sid).FirstOrDefault();
                    string stdtitle = "";
                    if (std.sStudentTitle != null)
                    {
                        int nn;
                        bool isNumeric2 = int.TryParse(std.sStudentTitle, out nn);
                        if (isNumeric2 == true)
                        {
                            var dbtitle = q_title.Where(w => w.nTitleid == nn).FirstOrDefault();
                            if (dbtitle != null)
                                stdtitle = dbtitle.titleDescription;
                        }
                    }
                    if (stdtitle != "")
                        Detail.stdName = stdtitle + " " + std.sName + " " + std.sLastname;
                    else Detail.stdName = std.sName + " " + std.sLastname;

                    Detail.stdNumber = std.sStudentID;


                    int n;
                    bool isNumeric = int.TryParse(std.sStudentID, out n);
                    if (isNumeric == true)
                    {
                        Detail.sort1int = Int32.Parse(std.sStudentID);
                        Detail.sort1txt = std.sStudentID;
                        Detail.sort2 = 999999;
                    }
                    else if (std.sStudentID == null || std.sStudentID == "")
                    {
                        Detail.sort1int = 0;
                        Detail.sort1txt = "";
                        Detail.sort2 = 999999;
                    }
                    else
                    {
                        sorttype1txt = sorttype1txt + 1;
                        Detail.sort1txt = std.sStudentID;
                        Detail.sort2 = 999999;
                    }
                    Detail.creditDetail1 = "หน่วยกิตที่ลงทะเบียนในภาคเรียนนี้ " + totalweight + " หน่วย";
                    double creditsum =
                        creditGet1[x].Value +
                        creditGet2[x].Value +
                        creditGet3[x].Value +
                        creditGet4[x].Value +
                        creditGet5[x].Value +
                        creditGet6[x].Value +
                        creditGet7[x].Value +
                        creditGet8[x].Value +
                        creditGet9[x].Value +
                        creditGet10[x].Value +
                        creditGet11[x].Value +
                        creditGet12[x].Value +
                        creditGet13[x].Value +
                        creditGet14[x].Value +
                        creditGet15[x].Value +
                        creditGet16[x].Value +
                        creditGet17[x].Value +
                        creditGet18[x].Value +
                        creditGet19[x].Value +
                        creditGet20[x].Value +
                        creditGet21[x].Value +
                        creditGet22[x].Value +
                        creditGet23[x].Value +
                        creditGet24[x].Value +
                        creditGet25[x].Value +
                        creditGet26[x].Value;
                    Detail.creditDetail2 = "หน่วยกิตที่สอบได้ภาคเรียนนี้ " + creditsum + " หน่วย";
                    Detail.getscore1 = get1[x];
                    Detail.getscore2 = get2[x];
                    Detail.getscore3 = get3[x];
                    Detail.getscore4 = get4[x];
                    Detail.getscore5 = get5[x];
                    Detail.getscore6 = get6[x];
                    Detail.getscore7 = get7[x];
                    Detail.getscore8 = get8[x];
                    Detail.getscore9 = get9[x];
                    Detail.getscore10 = get10[x];
                    Detail.getscore11 = get11[x];
                    Detail.getscore12 = get12[x];
                    Detail.getscore13 = get13[x];
                    Detail.getscore14 = get14[x];
                    Detail.getscore15 = get15[x];
                    Detail.getscore16 = get16[x];
                    Detail.getscore17 = get17[x];
                    Detail.getscore18 = get18[x];
                    Detail.getscore19 = get19[x];
                    Detail.getscore20 = get20[x];
                    Detail.getscore21 = get21[x];
                    Detail.getscore22 = get22[x];
                    Detail.getscore23 = get23[x];
                    Detail.getscore24 = get24[x];
                    Detail.getscore25 = get25[x];
                    Detail.getscore26 = get26[x];
                    Detail.gradelabel1 = label1[x];
                    Detail.gradelabel2 = label2[x];
                    Detail.gradelabel3 = label3[x];
                    Detail.gradelabel4 = label4[x];
                    Detail.gradelabel5 = label5[x];
                    Detail.gradelabel6 = label6[x];
                    Detail.gradelabel7 = label7[x];
                    Detail.gradelabel8 = label8[x];
                    Detail.gradelabel9 = label9[x];
                    Detail.gradelabel10 = label10[x];
                    Detail.gradelabel11 = label11[x];
                    Detail.gradelabel12 = label12[x];
                    Detail.gradelabel13 = label13[x];
                    Detail.gradelabel14 = label14[x];
                    Detail.gradelabel15 = label15[x];
                    Detail.gradelabel16 = label16[x];
                    Detail.gradelabel17 = label17[x];
                    Detail.gradelabel18 = label18[x];
                    Detail.gradelabel19 = label19[x];
                    Detail.gradelabel20 = label20[x];
                    Detail.gradelabel21 = label21[x];
                    Detail.gradelabel22 = label22[x];
                    Detail.gradelabel23 = label23[x];
                    Detail.gradelabel24 = label24[x];
                    Detail.gradelabel25 = label25[x];
                    Detail.gradelabel26 = label26[x];
                    Detail.scoreSum = scoreRaw[x].ToString();

                    double? thisTermRegisterCredit = 0;
                    double? thisTermPassCredit = 0;
                    double? thisTermPassWeight = 0;


                    foreach (var allData1 in q_gradeDetails.Where(w => w.sID == std.sID))
                    {

                        var allData2 = q_gradeData.Where(w => w.nGradeId == allData1.nGradeId).FirstOrDefault();
                        if (allData2.nTermSubLevel2 == std.nTermSubLevel2 && allData2.nTerm == nterm)
                        {
                            //var allData3 = q_plane.Where(w => w.sPlaneID == allData2.sPlaneID).FirstOrDefault();
                            var planCourseDTOs = ServiceHelper.GetPlanCourses(0, (int)allData2.nTermSubLevel2, allData2.nTerm, 0, userData.CompanyID, _db);
                            var allData3 = planCourseDTOs.Where(c => c.SPlaneId == allData2.sPlaneID).FirstOrDefault();

                            var check1 = q_gradeDetails.Where(w => w.nGradeId == allData1.nGradeId && w.getSpecial != "-1").FirstOrDefault();
                            var check2 = q_gradeDetails.Where(w => w.nGradeId == allData1.nGradeId && w.getSpecial == "-1" && w.getScore100 != "0").FirstOrDefault();

                            if (check1 != null || check2 != null)
                            {
                                thisTermRegisterCredit = thisTermRegisterCredit + allData3.NCredit;

                                string get100 = !string.IsNullOrEmpty(allData1.getScore100) ? allData1.getScore100 : "0"; //Common.getScore100(allData2, allData1);
                                double score100 = 0;
                                double d;
                                bool isDouble = double.TryParse(get100, out d);
                                if (isDouble == true)
                                {
                                    score100 = d;
                                }

                                double? nCredit = allData3.NCredit;
                                if (allData1.getSpecial == "-1")
                                {

                                    if (score100 > 49.99)
                                        thisTermPassCredit = thisTermPassCredit + allData3.NCredit;

                                    if (score100 < 50)
                                        thisTermPassWeight = thisTermPassWeight + 0;
                                    else if (score100 < 55)
                                        thisTermPassWeight = thisTermPassWeight + (1 * nCredit);
                                    else if (score100 < 60)
                                        thisTermPassWeight = thisTermPassWeight + (1.5 * nCredit);
                                    else if (score100 < 65)
                                        thisTermPassWeight = thisTermPassWeight + (2 * nCredit);
                                    else if (score100 < 70)
                                        thisTermPassWeight = thisTermPassWeight + (2.5 * nCredit);
                                    else if (score100 < 75)
                                        thisTermPassWeight = thisTermPassWeight + (3 * nCredit);
                                    else if (score100 < 80)
                                        thisTermPassWeight = thisTermPassWeight + (3.5 * nCredit);
                                    else if (score100 >= 80)
                                        thisTermPassWeight = thisTermPassWeight + (4 * nCredit);
                                }
                                else if (allData1.getSpecial == "4")
                                {
                                    thisTermPassCredit = thisTermPassCredit + allData3.NCredit;
                                    thisTermPassWeight = thisTermPassWeight + (4 * nCredit);
                                }
                            }

                        }

                    }

                    double? calculate = (thisTermPassWeight * 4) / (thisTermRegisterCredit * 4);
                    double cal = calculate ?? default(double);
                    double cal2 = System.Math.Round(cal, 3);

                    string ff = cal2.ToString("0.000");
                    ff = ff.Remove(ff.Length - 1);
                    Detail.gradelabelSum = ff;
                    Detail.creditDetail3 = "คะแนนเฉลี่ยประจำภาค " + ff;

                    double? allRegisterCredit = 0;
                    double? allPassedCredit = 0;
                    double? allPassedWeight = 0;
                    foreach (var allData1 in q_gradeDetails.Where(w => w.sID == std.sID))
                    {
                        var allData2 = q_gradeData.Where(w => w.nGradeId == allData1.nGradeId).FirstOrDefault();
                        //var allData3 = q_plane.Where(w => w.SPlaneId == allData2.sPlaneID).FirstOrDefault();

                        var planCourseDTOs = ServiceHelper.GetPlanCourses(0, (int)allData2.nTermSubLevel2, allData2.nTerm, 0, userData.CompanyID, _db);
                        var allData3 = planCourseDTOs.Where(c => c.SPlaneId == allData2.sPlaneID).FirstOrDefault();

                        int check1 = 0;
                        foreach (var check3 in q_gradeDetails.Where(w => w.nGradeId == allData1.nGradeId && w.getSpecial != "-1"))
                        {
                            if (check3 != null && check1 == 0)
                            {
                                var a = q_studentData.Where(w => w.sID == std.sID).FirstOrDefault();
                                var b = q_studentData.Where(w => w.sID == check3.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null)).FirstOrDefault();
                                if (a != null && b != null)
                                    if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                        check1 = 1;
                            }

                        }
                        int check2 = 0;
                        foreach (var check4 in q_gradeDetails.Where(w => w.nGradeId == allData1.nGradeId && w.getSpecial == "-1" && w.getScore100 != "0"))
                        {
                            if (check4 != null && check2 == 0)
                            {
                                var a = q_studentData.Where(w => w.sID == std.sID).FirstOrDefault();
                                var b = q_studentData.Where(w => w.sID == check4.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null)).FirstOrDefault();
                                if (a != null && b != null)
                                    if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                        check2 = 1;
                            }
                        }

                        if (check1 == 1 || check2 == 1)
                        {
                            allRegisterCredit = allRegisterCredit + allData3.NCredit;

                            string get100 = !string.IsNullOrEmpty(allData1.getScore100) ? allData1.getScore100 : "0"; //Common.getScore100(allData2, allData1);
                            double score100 = 0;
                            double d;
                            bool isDouble = double.TryParse(get100, out d);
                            if (isDouble == true)
                            {
                                score100 = d;
                            }

                            double? nCredit = allData3.NCredit;
                            if (allData1.getSpecial == "-1")
                            {

                                if (score100 > 49.99)
                                    allPassedCredit = allPassedCredit + allData3.NCredit;

                                if (score100 < 50)
                                    allPassedWeight = allPassedWeight + 0;
                                else if (score100 < 55)
                                    allPassedWeight = allPassedWeight + (1 * nCredit);
                                else if (score100 < 60)
                                    allPassedWeight = allPassedWeight + (1.5 * nCredit);
                                else if (score100 < 65)
                                    allPassedWeight = allPassedWeight + (2 * nCredit);
                                else if (score100 < 70)
                                    allPassedWeight = allPassedWeight + (2.5 * nCredit);
                                else if (score100 < 75)
                                    allPassedWeight = allPassedWeight + (3 * nCredit);
                                else if (score100 < 80)
                                    allPassedWeight = allPassedWeight + (3.5 * nCredit);
                                else if (score100 >= 80)
                                    allPassedWeight = allPassedWeight + (4 * nCredit);
                            }
                            else if (allData1.getSpecial == "4")
                            {
                                allPassedCredit = allPassedCredit + allData3.NCredit;
                                allPassedWeight = allPassedWeight + (4 * nCredit);
                            }
                        }
                    }

                    Detail.creditDetail4 = "หน่วยกิตที่ลงทะเบียนรวม " + allRegisterCredit + " หน่วย";
                    Detail.creditDetail5 = "หน่วยกิตที่สอบได้สะสม " + allPassedCredit + " หน่วย";

                    double? calGPA = (allPassedWeight * 4) / (allRegisterCredit * 4);
                    double GPA;
                    GPA = calGPA ?? default(double);
                    double cal3 = System.Math.Round(GPA, 3);

                    string ff2 = cal3.ToString("0.000");
                    ff2 = ff2.Remove(ff2.Length - 1);

                    Detail.creditDetail6 = "คะแนนเฉลี่ยสะสม " + ff2;
                    Detail.registerPlan = count;
                    Detail.maxScore = maxscorecount * 100;
                    DetailList.Add(Detail);
                    x = x + 1;
                }
            }
            else
            {
                int x = 0;
                Detail = new PlanDetail();

                var std = _db.TUsers.Where(w => w.sID == idn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                string stdtitle = "";
                if (std.sStudentTitle != null)
                {
                    int nn;
                    bool isNumeric2 = int.TryParse(std.sStudentTitle, out nn);
                    if (isNumeric2 == true)
                    {
                        var dbtitle = _db.TTitleLists.Where(w => w.nTitleid == nn && w.SchoolID == userData.CompanyID).FirstOrDefault();
                        if (dbtitle != null)
                            stdtitle = dbtitle.titleDescription;
                    }
                }
                if (stdtitle != "")
                    Detail.stdName = stdtitle + " " + std.sName + " " + std.sLastname;
                else Detail.stdName = std.sName + " " + std.sLastname;

                Detail.stdNumber = std.sStudentID;
                Detail.getscore1 = get1[x];
                Detail.getscore2 = get2[x];
                Detail.getscore3 = get3[x];
                Detail.getscore4 = get4[x];
                Detail.getscore5 = get5[x];
                Detail.getscore6 = get6[x];
                Detail.getscore7 = get7[x];
                Detail.getscore8 = get8[x];
                Detail.getscore9 = get9[x];
                Detail.getscore10 = get10[x];
                Detail.getscore11 = get11[x];
                Detail.getscore12 = get12[x];
                Detail.getscore13 = get13[x];
                Detail.getscore14 = get14[x];
                Detail.getscore15 = get15[x];
                Detail.getscore16 = get16[x];
                Detail.getscore17 = get17[x];
                Detail.getscore18 = get18[x];
                Detail.getscore19 = get19[x];
                Detail.getscore20 = get20[x];
                Detail.getscore21 = get21[x];
                Detail.getscore22 = get22[x];
                Detail.getscore23 = get23[x];
                Detail.getscore24 = get24[x];
                Detail.getscore25 = get25[x];
                Detail.getscore26 = get26[x];
                Detail.gradelabel1 = label1[x];
                Detail.gradelabel2 = label2[x];
                Detail.gradelabel3 = label3[x];
                Detail.gradelabel4 = label4[x];
                Detail.gradelabel5 = label5[x];
                Detail.gradelabel6 = label6[x];
                Detail.gradelabel7 = label7[x];
                Detail.gradelabel8 = label8[x];
                Detail.gradelabel9 = label9[x];
                Detail.gradelabel10 = label10[x];
                Detail.gradelabel11 = label11[x];
                Detail.gradelabel12 = label12[x];
                Detail.gradelabel13 = label13[x];
                Detail.gradelabel14 = label14[x];
                Detail.gradelabel15 = label15[x];
                Detail.gradelabel16 = label16[x];
                Detail.gradelabel17 = label17[x];
                Detail.gradelabel18 = label18[x];
                Detail.gradelabel19 = label19[x];
                Detail.gradelabel20 = label20[x];
                Detail.gradelabel21 = label21[x];
                Detail.gradelabel22 = label22[x];
                Detail.gradelabel23 = label23[x];
                Detail.gradelabel24 = label24[x];
                Detail.gradelabel25 = label25[x];
                Detail.gradelabel26 = label26[x];
                Detail.scoreSum = scoreRaw[x].ToString();


                Detail.creditDetail1 = "หน่วยกิตที่ลงทะเบียนในภาคเรียนนี้ " + totalweight + " หน่วย";
                double creditsum =
                    creditGet1[x].Value +
                    creditGet2[x].Value +
                    creditGet3[x].Value +
                    creditGet4[x].Value +
                    creditGet5[x].Value +
                    creditGet6[x].Value +
                    creditGet7[x].Value +
                    creditGet8[x].Value +
                    creditGet9[x].Value +
                    creditGet10[x].Value +
                    creditGet11[x].Value +
                    creditGet12[x].Value +
                    creditGet13[x].Value +
                    creditGet14[x].Value +
                    creditGet15[x].Value +
                    creditGet16[x].Value +
                    creditGet17[x].Value +
                    creditGet18[x].Value +
                    creditGet19[x].Value +
                    creditGet20[x].Value +
                    creditGet21[x].Value +
                    creditGet22[x].Value +
                    creditGet23[x].Value +
                    creditGet24[x].Value +
                    creditGet25[x].Value +
                    creditGet26[x].Value;
                Detail.creditDetail2 = "หน่วยกิตที่สอบได้ภาคเรียนนี้ " + creditsum + " หน่วย";

                double? thisTermRegisterCredit = 0;
                double? thisTermPassCredit = 0;
                double? thisTermPassWeight = 0;

                foreach (var allData1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.sID == std.sID && w.cDel == false))
                {

                    var allData2 = _dbGrade.TGrades.Where(w => w.nGradeId == allData1.nGradeId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (allData2 != null)
                    {
                        if (allData2.nTermSubLevel2 == std.nTermSubLevel2 && allData2.nTerm == nterm)
                        {
                            //var allData3 = _db.TPlanes.Where(w => w.sPlaneID == allData2.sPlaneID).FirstOrDefault();
                            var planCourseDTOs = ServiceHelper.GetPlanCourses(0, (int)allData2.nTermSubLevel2, allData2.nTerm, 0, userData.CompanyID, _db);
                            var allData3 = planCourseDTOs.Where(c => c.SPlaneId == allData2.sPlaneID).FirstOrDefault();

                            var check1 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial != "-1" && w.cDel == false).FirstOrDefault();
                            var check2 = _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial == "-1" && w.getScore100 != "0" && w.cDel == false).FirstOrDefault();

                            if (check1 != null || check2 != null)
                            {
                                thisTermRegisterCredit = thisTermRegisterCredit + allData3.NCredit;

                                string get100 = !string.IsNullOrEmpty(allData1.getScore100) ? allData1.getScore100 : "0"; //Common.getScore100(allData2, allData1);
                                double score100 = 0;
                                double d;
                                bool isDouble = double.TryParse(get100, out d);
                                if (isDouble == true)
                                {
                                    score100 = d;
                                }

                                double? nCredit = allData3.NCredit;
                                if (allData1.getSpecial == "-1")
                                {

                                    if (score100 > 49.99)
                                        thisTermPassCredit = thisTermPassCredit + allData3.NCredit;

                                    if (score100 < 50)
                                        thisTermPassWeight = thisTermPassWeight + 0;
                                    else if (score100 < 55)
                                        thisTermPassWeight = thisTermPassWeight + (1 * nCredit);
                                    else if (score100 < 60)
                                        thisTermPassWeight = thisTermPassWeight + (1.5 * nCredit);
                                    else if (score100 < 65)
                                        thisTermPassWeight = thisTermPassWeight + (2 * nCredit);
                                    else if (score100 < 70)
                                        thisTermPassWeight = thisTermPassWeight + (2.5 * nCredit);
                                    else if (score100 < 75)
                                        thisTermPassWeight = thisTermPassWeight + (3 * nCredit);
                                    else if (score100 < 80)
                                        thisTermPassWeight = thisTermPassWeight + (3.5 * nCredit);
                                    else if (score100 >= 80)
                                        thisTermPassWeight = thisTermPassWeight + (4 * nCredit);
                                }
                                else if (allData1.getSpecial == "4")
                                {
                                    thisTermPassCredit = thisTermPassCredit + allData3.NCredit;
                                    thisTermPassWeight = thisTermPassWeight + (4 * nCredit);
                                }
                            }

                        }
                    }


                }

                double? calculate = (thisTermPassWeight * 4) / (thisTermRegisterCredit * 4);
                double cal = calculate ?? default(double);
                double cal2 = System.Math.Round(cal, 3);

                string ff = cal2.ToString("0.000");
                ff = ff.Remove(ff.Length - 1);

                Detail.gradelabelSum = ff;
                Detail.creditDetail3 = "คะแนนเฉลี่ยประจำภาค " + ff;
                double? allRegisterCredit = 0;
                double? allPassedCredit = 0;
                double? allPassedWeight = 0;
                foreach (var allData1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.sID == std.sID && w.cDel == false))
                {
                    var allData2 = _dbGrade.TGrades.Where(w => w.nGradeId == allData1.nGradeId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                    if (allData2 != null)
                    {


                        //var allData3 = _db.TPlanes.Where(w => w.sPlaneID == allData2.sPlaneID).FirstOrDefault();
                        var planCourseDTOs = ServiceHelper.GetPlanCourses(0, (int)allData2.nTermSubLevel2, allData2.nTerm, 0, userData.CompanyID, _db);
                        var allData3 = planCourseDTOs.Where(c => c.SPlaneId == allData2.sPlaneID).FirstOrDefault();

                        int check1 = 0;
                        foreach (var check3 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial != "-1" && w.cDel == false))
                        {
                            if (check3 != null && check1 == 0)
                            {
                                var a = _db.TUsers.Where(w => w.sID == std.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                var b = _db.TUsers.Where(w => w.sID == check3.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null) && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                if (a != null && b != null)
                                    if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                        check1 = 1;
                            }
                        }
                        int check2 = 0;
                        foreach (var check4 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial == "-1" && w.getScore100 != "0" && w.cDel == false))
                        {
                            if (check4 != null && check2 == 0)
                            {
                                var a = _db.TUsers.Where(w => w.sID == std.sID && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                var b = _db.TUsers.Where(w => w.sID == check4.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null) && w.SchoolID == userData.CompanyID).FirstOrDefault();
                                if (a != null && b != null)
                                    if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                        check2 = 1;
                            }

                        }

                        if (check1 == 1 || check2 == 1)
                        {
                            allRegisterCredit = allRegisterCredit + allData3.NCredit;

                            string get100 = !string.IsNullOrEmpty(allData1.getScore100) ? allData1.getScore100 : "0"; //Common.getScore100(allData2, allData1);
                            double score100 = 0;
                            double d;
                            bool isDouble = double.TryParse(get100, out d);
                            if (isDouble == true)
                            {
                                score100 = d;
                            }

                            double? nCredit = allData3.NCredit;
                            if (allData1.getSpecial == "-1")
                            {

                                if (score100 > 49.99)
                                    allPassedCredit = allPassedCredit + allData3.NCredit;

                                if (score100 < 50)
                                    allPassedWeight = allPassedWeight + 0;
                                else if (score100 < 55)
                                    allPassedWeight = allPassedWeight + (1 * nCredit);
                                else if (score100 < 60)
                                    allPassedWeight = allPassedWeight + (1.5 * nCredit);
                                else if (score100 < 65)
                                    allPassedWeight = allPassedWeight + (2 * nCredit);
                                else if (score100 < 70)
                                    allPassedWeight = allPassedWeight + (2.5 * nCredit);
                                else if (score100 < 75)
                                    allPassedWeight = allPassedWeight + (3 * nCredit);
                                else if (score100 < 80)
                                    allPassedWeight = allPassedWeight + (3.5 * nCredit);
                                else if (score100 >= 80)
                                    allPassedWeight = allPassedWeight + (4 * nCredit);
                            }
                            else if (allData1.getSpecial == "4")
                            {
                                allPassedCredit = allPassedCredit + allData3.NCredit;
                                allPassedWeight = allPassedWeight + (4 * nCredit);
                            }
                        }
                    }


                }
                Detail.creditDetail4 = "หน่วยกิตที่ลงทะเบียนรวม " + allRegisterCredit + " หน่วย";
                Detail.creditDetail5 = "หน่วยกิตที่สอบได้สะสม " + allPassedCredit + " หน่วย";

                double? calGPA = (allPassedWeight * 4) / (allRegisterCredit * 4);
                double GPA;
                GPA = calGPA ?? default(double);
                double cal3 = System.Math.Round(GPA, 3);

                string ff2 = cal3.ToString("0.000");
                ff2 = ff2.Remove(ff2.Length - 1);

                Detail.creditDetail6 = "คะแนนเฉลี่ยสะสม " + ff2;
                Detail.registerPlan = count;
                Detail.maxScore = maxscorecount * 100;
                DetailList.Add(Detail);
            }

            var newSortList = DetailList;

            newSortList = DetailList.OrderBy(x => x.sort1int).ToList();

            if (sorttype1txt != 0)
                newSortList = DetailList.OrderBy(x => x.sort1txt).ToList();



            rss = new JArray(from a in newSortList
                             select new JObject(

                   new JProperty("getscore1", a.getscore1),
                   new JProperty("getscore2", a.getscore2),
                   new JProperty("getscore3", a.getscore3),
                   new JProperty("getscore4", a.getscore4),
                   new JProperty("getscore5", a.getscore5),
                   new JProperty("getscore6", a.getscore6),
                   new JProperty("getscore7", a.getscore7),
                   new JProperty("getscore8", a.getscore8),
                   new JProperty("getscore9", a.getscore9),
                   new JProperty("getscore10", a.getscore10),
                   new JProperty("getscore11", a.getscore11),
                   new JProperty("getscore12", a.getscore12),
                   new JProperty("getscore13", a.getscore13),
                   new JProperty("getscore14", a.getscore14),
                   new JProperty("getscore15", a.getscore15),
                   new JProperty("getscore16", a.getscore16),
                   new JProperty("getscore17", a.getscore17),
                   new JProperty("getscore18", a.getscore18),
                   new JProperty("getscore19", a.getscore19),
                   new JProperty("getscore20", a.getscore20),
                   new JProperty("getscore21", a.getscore21),
                   new JProperty("getscore22", a.getscore22),
                   new JProperty("getscore23", a.getscore23),
                   new JProperty("getscore24", a.getscore24),
                   new JProperty("getscore25", a.getscore25),
                   new JProperty("getscore26", a.getscore26),
                   new JProperty("label1", a.gradelabel1),
                   new JProperty("label2", a.gradelabel2),
                   new JProperty("label3", a.gradelabel3),
                   new JProperty("label4", a.gradelabel4),
                   new JProperty("label5", a.gradelabel5),
                   new JProperty("label6", a.gradelabel6),
                   new JProperty("label7", a.gradelabel7),
                   new JProperty("label8", a.gradelabel8),
                   new JProperty("label9", a.gradelabel9),
                   new JProperty("label10", a.gradelabel10),
                   new JProperty("label11", a.gradelabel11),
                   new JProperty("label12", a.gradelabel12),
                   new JProperty("label13", a.gradelabel13),
                   new JProperty("label14", a.gradelabel14),
                   new JProperty("label15", a.gradelabel15),
                   new JProperty("label16", a.gradelabel16),
                   new JProperty("label17", a.gradelabel17),
                   new JProperty("label18", a.gradelabel18),
                   new JProperty("label19", a.gradelabel19),
                   new JProperty("label20", a.gradelabel20),
                   new JProperty("label21", a.gradelabel21),
                   new JProperty("label22", a.gradelabel22),
                   new JProperty("label23", a.gradelabel23),
                   new JProperty("label24", a.gradelabel24),
                   new JProperty("label25", a.gradelabel25),
                   new JProperty("label26", a.gradelabel26),
                   new JProperty("name", a.stdName),
                   new JProperty("gradelabelSum", a.gradelabelSum),
                   new JProperty("scoreSum", a.scoreSum),
                   new JProperty("number", a.stdNumber),
                   new JProperty("creditDetail1", a.creditDetail1),
                   new JProperty("creditDetail2", a.creditDetail2),
                   new JProperty("creditDetail3", a.creditDetail3),
                   new JProperty("creditDetail4", a.creditDetail4),
                   new JProperty("creditDetail5", a.creditDetail5),
                   new JProperty("creditDetail6", a.creditDetail6),
                   new JProperty("registerPlan", a.registerPlan),
                   new JProperty("maxScore", a.maxScore)


                ));

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.End();
        }

        protected double? creditGain(string getSpecial, string getScore100, int? planId, PlanCourseDTO planCourseDTO)
        {
            double? credit = 0;

            var planCredit = planCourseDTO;


            if (getSpecial == "-1" && getScore100 != "")
            {
                double score = double.Parse(getScore100);
                if (score > 49.99)
                    credit = planCredit.NCredit;
            }
            else if (getSpecial == "4")
            {
                credit = planCredit.NCredit;
            }
            else
            {
                credit = 0;
            }

            return credit;
        }

        protected double? gradeGain(int userId, double? credit, List<TGradeDetail> gradeDetail, int score100, int planid)
        {

            var Gdetail = gradeDetail.Where(w => w.nGradeId == planid && w.sID == userId).FirstOrDefault();
            double? nCredit = credit;

            if (Gdetail.getSpecial == "-1")
            {
                if (score100 < 50)
                    credit = 0;
                else if (score100 < 55)
                    credit = 1 * nCredit;
                else if (score100 < 60)
                    credit = 1.5 * nCredit;
                else if (score100 < 65)
                    credit = 2 * nCredit;
                else if (score100 < 70)
                    credit = 2.5 * nCredit;
                else if (score100 < 75)
                    credit = 3 * nCredit;
                else if (score100 < 80)
                    credit = 3.5 * nCredit;
                else if (score100 >= 80)
                    credit = 4 * nCredit;
            }
            else if (Gdetail.getSpecial == "4")
            {
                credit = 4 * nCredit;
            }
            else
            {
                credit = 0;
            }

            return credit;
        }

        

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public string getSpecial(string special, string score100, int? group)
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
                else txt = "";
            }

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
            }

            if (group == 3 && score >= 50)
                txt = "ผ";

            return txt;
        }
        public string haveSpecial(string gradeLabel, string getSpecial)
        {
            string specialLebel = "";
            if (getSpecial != null)
            {
                if (getSpecial == "1")
                    specialLebel = "ร";
                else if (getSpecial == "2")
                    specialLebel = "มส";
                else if (getSpecial == "3")
                    specialLebel = "มก";
                else if (getSpecial == "4")
                    specialLebel = "ผ";
                else if (getSpecial == "5")
                    specialLebel = "มผ";
                else if (getSpecial == "6")
                    specialLebel = "อื่นๆ";
                else if (getSpecial == "7")
                    specialLebel = "ขร";
                else if (getSpecial == "8")
                    specialLebel = "ขส";
                else if (getSpecial == "9")
                    specialLebel = "ท";
                else specialLebel = "";
            }

            if (specialLebel != "")
                specialLebel = getSpecial;
            else specialLebel = gradeLabel;
            return specialLebel;
        }

        public decimal RoundDown(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }

        protected class stdList
        {
            public int sid { get; set; }
            public string sStudent { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public int? sort2 { get; set; }
        }
        protected class PlanDetail
        {
            public string creditDetail1 { get; set; }
            public string creditDetail2 { get; set; }
            public string creditDetail3 { get; set; }
            public string creditDetail4 { get; set; }
            public string creditDetail5 { get; set; }
            public string creditDetail6 { get; set; }
            public string getscore1 { get; set; }
            public string getscore2 { get; set; }
            public string getscore3 { get; set; }
            public string getscore4 { get; set; }
            public string getscore5 { get; set; }
            public string getscore6 { get; set; }
            public string getscore7 { get; set; }
            public string getscore8 { get; set; }
            public string getscore9 { get; set; }
            public string getscore10 { get; set; }
            public string getscore11 { get; set; }
            public string getscore12 { get; set; }
            public string getscore13 { get; set; }
            public string getscore14 { get; set; }
            public string getscore15 { get; set; }
            public string getscore16 { get; set; }
            public string getscore17 { get; set; }
            public string getscore18 { get; set; }
            public string getscore19 { get; set; }
            public string getscore20 { get; set; }
            public string getscore21 { get; set; }
            public string getscore22 { get; set; }
            public string getscore23 { get; set; }
            public string getscore24 { get; set; }
            public string getscore25 { get; set; }
            public string getscore26 { get; set; }
            public string gradelabel1 { get; set; }
            public string gradelabel2 { get; set; }
            public string gradelabel3 { get; set; }
            public string gradelabel4 { get; set; }
            public string gradelabel5 { get; set; }
            public string gradelabel6 { get; set; }
            public string gradelabel7 { get; set; }
            public string gradelabel8 { get; set; }
            public string gradelabel9 { get; set; }
            public string gradelabel10 { get; set; }
            public string gradelabel11 { get; set; }
            public string gradelabel12 { get; set; }
            public string gradelabel13 { get; set; }
            public string gradelabel15 { get; set; }
            public string gradelabel16 { get; set; }
            public string gradelabel17 { get; set; }
            public string gradelabel18 { get; set; }
            public string gradelabel19 { get; set; }
            public string gradelabel20 { get; set; }
            public string gradelabel14 { get; set; }
            public string gradelabel21 { get; set; }
            public string gradelabel22 { get; set; }
            public string gradelabel23 { get; set; }
            public string gradelabel24 { get; set; }
            public string gradelabel25 { get; set; }
            public string gradelabel26 { get; set; }
            public string stdName { get; set; }
            public string stdNumber { get; set; }
            public string gradelabelSum { get; set; }
            public string scoreSum { get; set; }
            public int sort1int { get; set; }
            public string sort1txt { get; set; }
            public int? sort2 { get; set; }
            public int registerPlan { get; set; }
            public int maxScore { get; set; }
        }

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