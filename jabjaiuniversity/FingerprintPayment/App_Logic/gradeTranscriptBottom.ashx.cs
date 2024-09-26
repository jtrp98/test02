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
    /// Summary description for gradeTranscriptBottom
    /// </summary>
    public class gradeTranscriptBottom : IHttpHandler, IRequiresSessionState
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

            
            foreach (var allData1 in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.sID == idn && w.cDel == false))
            {
                var allData2 = _dbGrade.TGrades.Where(w => w.nGradeId == allData1.nGradeId && w.SchoolID == userData.CompanyID).FirstOrDefault();
                if(allData2 != null)
                {
                        var allData3 = _db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.sPlaneID == allData2.sPlaneID).FirstOrDefault();

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
                        if (allData1.getSpecial == "-1")
                        {

                            if (score100 < 50)
                                label = "0";
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


                        std = new PlanDetail();
                        std.planCredit = allData3.nCredit;
                        std.getGrade = label;
                        var e = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == allData2.nTerm).FirstOrDefault();
                        std.term = e.sTerm;
                        var f = _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.nYear == e.nYear).FirstOrDefault();
                        std.year = f.numberYear;
                        std.getSpecial = allData1.getSpecial;
                        unique.Add(std);
                    }
                }
                

            }

            

            var newSortList4 = unique;
            newSortList4 = unique.OrderBy(x => x.year).ThenBy(x => x.term).ToList();
            
            string check = "";

            List<PlanDetail2> unique2 = new List<PlanDetail2>();
            PlanDetail2 detail = new PlanDetail2();

            List<checkterm> checktermList = new List<checkterm>();
            checkterm checkterm = new checkterm();

            foreach (var a in newSortList4)
            {                
                string y2 = a.year.ToString();
                y2 = y2.Remove(0, 2);
                a.termYear = a.term + "/" + y2;
                if (a.termYear != check) 
                {
                    checkterm = new checkterm();
                    checkterm.termYear = a.termYear;
                    checktermList.Add(checkterm);
                    check = a.termYear;
                }
            }

            double? sumAllPassedCredit = 0;
            double? sumAllRegisterCredit = 0;
            double? sumAllScoreGet = 0;

            foreach (var b in checktermList)
            {
                double? termCreditGain = 0;
                double? termCreditRegister = 0;
                double? termScoreGain = 0;

                detail = new PlanDetail2();

                foreach (var c in newSortList4.Where(w=>w.termYear == b.termYear))
                {
                    sumAllRegisterCredit = sumAllRegisterCredit + c.planCredit;
                    termCreditRegister = termCreditRegister + c.planCredit;

                    if (c.getSpecial == "-1")
                    {                        
                        double checkgrade = double.Parse(c.getGrade);
                        if(checkgrade != 0)
                        {
                            termCreditGain = termCreditGain + c.planCredit;
                            termScoreGain = termScoreGain + (checkgrade * c.planCredit);
                            sumAllPassedCredit = sumAllPassedCredit + c.planCredit;
                            sumAllScoreGet = sumAllScoreGet + (checkgrade * c.planCredit);
                        }                        
                    }
                    else if (c.getSpecial == "4")
                    {
                        termCreditGain = termCreditGain + c.planCredit;
                        termCreditRegister = termCreditRegister + c.planCredit;
                        sumAllPassedCredit = sumAllPassedCredit + c.planCredit;
                        termScoreGain = termScoreGain + (4 * c.planCredit);
                        sumAllScoreGet = sumAllScoreGet + (4 * c.planCredit);
                    }                            
                }

                detail.termYear = b.termYear;
                detail.sumCredit = sumAllPassedCredit;

                double? calculate = (termScoreGain * 4) / (termCreditRegister * 4);
                double cal = calculate ?? default(double);
                double cal2 = System.Math.Round(cal, 3);
                string ff = cal2.ToString("0.000");
                ff = ff.Remove(ff.Length - 1);
                detail.thisTermGrade = ff;

                double? calculateALL = (sumAllScoreGet * 4) / (sumAllRegisterCredit * 4);
                double calall = calculateALL ?? default(double);
                double cal2all = System.Math.Round(calall, 3);
                string ffall = cal2all.ToString("0.000");
                ffall = ffall.Remove(ffall.Length - 1);
                detail.sumGrade = ffall;

                detail.thisTermCredit = termCreditGain;
                if(termScoreGain==0)
                    detail.thisTermGrade = "0.00";
                unique2.Add(detail);
            }

            rss = new JArray(from a in unique2
                             select new JObject(

                   new JProperty("termYear", a.termYear),
                   new JProperty("sumCredit", a.sumCredit),
                   new JProperty("sumGrade", a.sumGrade),
                   new JProperty("thisTermCredit", a.thisTermCredit),
                   new JProperty("thisTermGrade", a.thisTermGrade)


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
            public double? planCredit { get; set; }
            public string getGrade { get; set; }
            public string getSpecial { get; set; }
            public double? getScore { get; set; }
            public double? thisTermCredit { get; set; }
            public double? sumCredit { get; set; }
            public double? thisTermGrade { get; set; }
            public double? sumGrade { get; set; }
            public string termYear { get; set; }
        }

        protected class PlanDetail2
        {           
            public double? thisTermCredit { get; set; }
            public double? sumCredit { get; set; }
            public string thisTermGrade { get; set; }
            public string sumGrade { get; set; }
            public string termYear { get; set; }
        }

        protected class checkterm
        {
            public string termYear { get; set; }
            
        }
    }


}