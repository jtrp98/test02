using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolGradeEntity;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for gradeTranscript
    /// </summary>
    public class gradeTranscript : IHttpHandler, IRequiresSessionState
    {

        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();            
            string id = fcommon.ReplaceInjection(context.Request["id"]);
            int idn = int.Parse(id);

            JabJaiMasterEntities _dbMaster = Connection.MasterEntities();
            JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade());
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString()));
            string sEntities = HttpContext.Current.Session["sEntities"].ToString();//"JabJaiEntities";
            
            List<string> planIdlist = new List<string>();


            List<PlanDetail> unique = new List<PlanDetail>();
            PlanDetail std = new PlanDetail();

            double? allRegisterCredit = 0;
            foreach (var allData1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.sID == idn && w.cDel == false))
            {
                var allData2 = _dbGrade.TGrades.Where(w => w.nGradeId == allData1.nGradeId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                if(allData2 != null)
                {
                        var allData3 = _db.TPlanes.Where(w => w.sPlaneID == allData2.sPlaneID && w.SchoolID == userData.CompanyID).FirstOrDefault();

                    int check1 = 0;
                    foreach (var check3 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial != "-1" && w.cDel == false))
                    {
                        if (check3 != null)
                        {
                            var a = _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.sID == idn).FirstOrDefault();
                            var b = _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.sID == check3.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null)).FirstOrDefault();
                            if (a != null && b != null)
                                if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                    check1 = 1;
                        }
                        if (check1 == 1)
                            break;
                    }
                    int check2 = 0;
                    foreach (var check4 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.nGradeId == allData1.nGradeId && w.getSpecial == "-1" && w.getScore100 != "0" && w.cDel == false))
                    {
                        if (check4 != null)
                        {
                            var a = _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.sID == idn).FirstOrDefault();
                            var b = _db.TUsers.Where(w => w.SchoolID == userData.CompanyID && w.sID == check4.sID && (w.nStudentStatus == 0 || w.nStudentStatus == null)).FirstOrDefault();
                            if (a != null && b != null)
                                if (a.nTermSubLevel2 == b.nTermSubLevel2)
                                    check2 = 1;
                        }
                        if (check2 == 1)
                            break;
                    }

                    if (check1 == 1 || check2 == 1)
                    {
                        allRegisterCredit = allRegisterCredit + allData3.nCredit;

                        if (allData1.getScore100 != null)
                        {
                            string get100 = allData1.getScore100; //Helper.Common.getScore100(allData2, allData1);
                            double score100 = 0;
                            double d;
                            bool isDouble = double.TryParse(get100, out d);
                            if (isDouble == true)
                            {
                                score100 = d;
                            }

                            double? nCredit = allData3.nCredit;
                            string label = "";
                            int creditPass = 1;

                            if (allData1.getSpecial == "-1")
                            {

                                if (score100 < 50)
                                {
                                    label = "0";
                                    creditPass = 0;
                                }
                                else if (score100 < 55)
                                    label = "1.0";
                                else if (score100 < 60)
                                    label = "1.5";
                                else if (score100 < 65)
                                    label = "2.0";
                                else if (score100 < 70)
                                    label = "2.5";
                                else if (score100 < 75)
                                    label = "3.0";
                                else if (score100 < 80)
                                    label = "3.5";
                                else if (score100 >= 80)
                                    label = "4.0";
                            }
                            else if (allData1.getSpecial == "1")
                            {
                                label = "ร";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "2")
                            {
                                label = "มส";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "3")
                            {
                                label = "มก";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "4")
                                label = "ผ";
                            else if (allData1.getSpecial == "5")
                            {
                                label = "มผ";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "6")
                            {
                                label = "อื่นๆ";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "7")
                            {
                                label = "ขร";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "8")
                            {
                                label = "ขส";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "9")
                            {
                                label = "ท";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "10")
                            {
                                label = "ดีเยี่ยม";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "11")
                            {
                                label = "ดี";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "12")
                            {
                                label = "พอใช้";
                                creditPass = 0;
                            }
                            else if (allData1.getSpecial == "13")
                            {
                                label = "ปรับปรุง";
                                creditPass = 0;
                            }


                            std = new PlanDetail();
                            std.planCode = allData3.courseCode;
                            std.score100 = score100.ToString();
                            std.planCredit = allData3.nCredit.ToString();
                            std.getGrade = label;
                            std.planGroup = allData3.courseGroup;
                            std.planName = allData3.sPlaneName;
                            std.planType = allData3.courseType;
                            var e = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == allData2.nTerm).FirstOrDefault();
                            std.term = e.sTerm;
                            var f = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.nYear == e.nYear).FirstOrDefault();
                            std.year = f.numberYear;
                            std.getSpecial = allData1.getSpecial;
                            

                            if (allData1.getSpecial == "-1")
                            {
                                std.planCreditDisplay = allData3.nCredit.ToString();
                                std.gradexcredit = (allData3.nCredit * double.Parse(label)).ToString();
                                std.gradexcreditDisplay = (allData3.nCredit * double.Parse(label)).ToString();
                            }

                            else if (allData1.getSpecial == "4")
                            {
                                std.planCreditDisplay = "-";
                                std.gradexcredit = (allData3.nCredit * 4).ToString();
                                std.gradexcreditDisplay = "-";
                            }
                            else
                            {
                                std.planCreditDisplay = "-";
                                std.gradexcredit = "0";
                                std.gradexcreditDisplay = "-";
                            }


                            string yy = f.numberYear.ToString();
                            yy = yy.Remove(0, 2);
                            std.termYear2 = std.term + "/" + yy;
                            std.termYear = std.term + "/" + std.year;
                            std.creditStatus = creditPass.ToString();
                            std.sPlaneId = allData3.sPlaneID;
                            std.nGradeId = allData1.nGradeId;
                            std.CourseTypeOrder = _db.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == allData3.courseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                            unique.Add(std);
                        }
                        else
                        {
                            std = new PlanDetail();
                            std.planCode = allData3.courseCode;
                            std.score100 = "";
                            std.planCredit = allData3.nCredit.ToString();
                            int creditPass = 1;

                            if (allData1.getSpecial == "-1")
                            {
                                std.planCreditDisplay = allData3.nCredit.ToString();
                                double gradelabel = double.Parse(allData1.getGradeLabel);
                                std.gradexcredit = (allData3.nCredit * gradelabel).ToString();
                                std.gradexcreditDisplay = std.gradexcredit;
                                if (gradelabel == 0)
                                    creditPass = 0;
                            }
                            else if (allData1.getSpecial == "4")
                            {
                                std.planCreditDisplay = "-";
                                std.gradexcredit = (allData3.nCredit * 4).ToString();
                                std.gradexcreditDisplay = "-";
                            }
                            else
                            {
                                creditPass = 0;
                                std.planCreditDisplay = "-";
                                std.gradexcredit = "0";
                                std.gradexcreditDisplay = "-";
                            }

                            std.getGrade = allData1.getGradeLabel;
                            std.planGroup = allData3.courseGroup;
                            std.planName = allData3.sPlaneName;
                            std.planType = allData3.courseType;
                            var e = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == allData2.nTerm).FirstOrDefault();
                            std.term = e.sTerm;
                            var f = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.nYear == e.nYear).FirstOrDefault();
                            std.year = f.numberYear;

                            string yy = f.numberYear.ToString();
                            yy = yy.Remove(0, 2);
                            std.termYear2 = std.term + "/" + yy;
                            std.termYear = std.term + "/" + std.year;

                            std.getSpecial = allData1.getSpecial;

                            std.creditStatus = creditPass.ToString();
                            std.sPlaneId = allData3.sPlaneID;
                            std.nGradeId = allData1.nGradeId;
                            std.CourseTypeOrder = _db.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID && w.courseTypeId == allData3.courseType).Select(s => s.nOrder ?? 99).FirstOrDefault();
                            unique.Add(std);
                        }

                    }
                }
                

            }
            

            
            var newSortList4 = unique;
            newSortList4 = unique.OrderBy(x => x.year).ThenBy(x => x.term).ThenBy(x => x.planGroup).ThenBy(x => x.CourseTypeOrder).ThenBy(x => x.planCode).ToList();
            

            rss = new JArray(from a in newSortList4
                             select new JObject(

                   new JProperty("planCode", a.planCode),
                   new JProperty("planCredit", a.planCredit),
                   new JProperty("getGrade", a.getGrade),
                   new JProperty("planGroup", a.planGroup),
                   new JProperty("planName", a.planName),
                   new JProperty("planType", a.planType),
                   new JProperty("term", a.term),
                   new JProperty("getSpecial", a.getSpecial),
                   new JProperty("getScore", a.getScore),
                   new JProperty("termYear", a.termYear),
                   new JProperty("year", a.year),
                   
                   new JProperty("planCreditDisplay", a.planCreditDisplay),                   
                   new JProperty("termYear2", a.termYear2),                   
                   new JProperty("gradexcredit", a.gradexcredit),
                   new JProperty("gradexcreditDisplay", a.gradexcreditDisplay),
                   new JProperty("creditStatus", a.creditStatus),
                    new JProperty("sPlaneId", a.sPlaneId),
                     new JProperty("nGradeId", a.nGradeId)

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
       

       
        protected class PlanDetail
        {
            public int? year { get; set; }
            public string term { get; set; }
            public string planName { get; set; }
            public string planCode { get; set; }
            public string planCredit { get; set; }
            public string planCreditDisplay { get; set; }
            public string getGrade { get; set; }
            public string getSpecial { get; set; }
            public double? getScore { get; set; }
            public int? planGroup { get; set; }
            public int? planType { get; set; }
            public string termYear { get; set; }
            public string termYear2 { get; set; }
            public string score100 { get; set; }
            public string gradexcredit { get; set; }
            public string gradexcreditDisplay { get; set; }
            public string creditStatus { get; set; }
            public int sPlaneId { get; set; }
            public int nGradeId { get; set; }
            public int CourseTypeOrder { get; set; }
        }        
    }


}