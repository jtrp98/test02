using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class yearsettings1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string UpdateData(int? YearId, int numberYear, TTerm termData)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            dynamic rss = new JObject();
            rss.Status = 200;
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolid = userData.CompanyID;
                TCompany tCompany = dbmaster.TCompanies.Where(w => w.nCompany == schoolid).FirstOrDefault();

                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                // var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);


                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    if (termData.dEnd <= termData.dStart)
                    {
                        rss.Des = "ไม่สามารถตั้งค่าวันที่สิ้นสุดน้อยกว่าวันเริ่มต้นได้";
                        rss.Status = 401;
                        return rss.ToString();
                    }
                    else if (termData.dEnd.Value.Year - DateTime.Today.Year > 20 || termData.dStart.Value.Year - DateTime.Today.Year > 20)
                    {
                        rss.Des = "รูปแบบวันที่ไม่ถูกต้อง";
                        rss.Status = 401;
                        return rss.ToString();
                    }
                    if (YearId == 0)
                    {
                        //YearId = dbschool.TYears.Select(s => s.nYear).DefaultIfEmpty(0).Max(max => max + 1);
                        if (dbschool.TYears.FirstOrDefault(f => f.numberYear == numberYear && f.SchoolID == userData.CompanyID && f.cDel == false) != null)
                        {
                            rss.Des = "ช่วงเวลานี้มีการบันทึกไปแล้ว";
                            rss.Status = 401;
                            return rss.ToString();
                        }
                        else
                        {
                            TYear year = new TYear
                            {
                                //nYear = YearId.Value,
                                numberYear = numberYear,
                                SchoolID = userData.CompanyID
                            };
                            dbschool.TYears.Add(year);
                            dbschool.SaveChanges();
                            YearId = year.nYear;
                        }
                    }

                    #region Add Trem
                    var q_trem = dbschool.TTerms.Where(x => x.SchoolID == schoolid).ToList();
                    ///Check Day Term
                    //termData.nTerm = string.Format("TS{0:0000000}", int.Parse(termData.nTerm.Replace("TS", "")) + 1);
                    var f_term = q_trem.FirstOrDefault(f => ((termData.dStart <= f.dStart && termData.dEnd >= f.dStart)
                    || (termData.dStart <= f.dEnd && termData.dEnd >= f.dEnd)
                    || (f.dStart <= termData.dStart && f.dEnd >= termData.dStart)
                    || (f.dStart <= termData.dEnd && f.dEnd >= termData.dEnd))
                    && f.nTerm.Trim() != termData.nTerm.Trim() && f.cDel == null);
                    if (f_term != null)
                    {
                        rss.Des = "ช่วงเวลานี้มีการบันทึกไปแล้ว";
                        rss.Status = 401;
                        return rss.ToString();
                    }

                    var f_data = dbschool.TTerms.Where(x => x.SchoolID == schoolid).FirstOrDefault(f => f.nTerm == termData.nTerm);
                    if (f_data == null) f_data = new TTerm();

                    f_data.dEnd = termData.dEnd;
                    f_data.dStart = termData.dStart;
                    f_data.sTerm = termData.sTerm;
                    f_data.SchoolID = schoolid;
                    if (string.IsNullOrEmpty(f_data.nTerm))
                    {
                        var max_Id = dbschool.TTerms.Where(w => w.SchoolID == schoolid).Select(s => s.nTerm).DefaultIfEmpty("0").Max();
                        if (max_Id.EndsWith(string.Format("{0:0000}", schoolid)))
                        {
                            max_Id = max_Id.Replace("TS", "");
                            max_Id = max_Id.Substring(0, max_Id.Length - 4);
                        }

                        max_Id = string.Format("TS{0:0000000}{1:0000}", int.Parse(max_Id) + 1, schoolid);
                        f_data.nYear = YearId;
                        f_data.nTerm = max_Id;
                        f_data.CreatedBy = userData.UserID;
                        f_data.CreatedDate = DateTime.Now;

                        dbschool.TTerms.Add(f_data);
                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการเพิ่มปีการศึกษา : " + numberYear + " เทอม : " + f_data.sTerm + " รหัสเทอม : " + f_data.sTerm,
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                    }
                    else
                    {
                        f_data.UpdatedBy = userData.UserID;
                        f_data.UpdatedDate = DateTime.Now;

                        database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",
                            "ทำการแก้ปีการศึกษา : " + numberYear + " เทอม : " + f_data.sTerm + " รหัสเทอม : " + f_data.sTerm,
                            HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 1, 2, 0);
                    }
                    if (rss.Status == 200) dbschool.SaveChanges();
                    #endregion
                    rss.Data = new JObject();
                    rss.Data.Year = numberYear;
                    rss.Data.YearId = f_data.nYear;
                    return rss.ToString();
                }
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string DeleteData(string termId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (var _dbGrade = new PostgreSQL.PGGradeDBEntities("PGGradeDBReadReplicaEntities"))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    rss.StatusCode = 200;
                    if (dbschool.TTermTimeTables.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else if (_dbGrade.PGTGrades.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else if (dbschool.TStudentClassroomHistories.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID && f.cDel == false) > 0) rss.StatusCode = 401;
                    else if (dbschool.TPlanCourseTerms.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm == termId && f.SchoolID == userData.CompanyID);
                        if (f_term != null)
                        {
                            f_term.cDel = "1";
                            f_term.UpdatedDate = DateTime.Now;
                            f_term.UpdatedBy = userData.UserID;
                        }
                        else rss.StatusCode = 402;
                        dbschool.SaveChanges();
                    }
                    return rss.ToString();
                }
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string ListData(int YearId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    dynamic rss = new JArray(from a in dbschool.TTerms.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList()
                                             where a.nYear == YearId
                                             orderby a.dStart
                                             select new JObject
                                             {
                                                 new JProperty("sTerm",a.sTerm),
                                                 new JProperty("nTerm",a.nTerm),
                                                 new JProperty("dStart",a.dStart.Value.ToString("dd/MM/yyyy")),
                                                 new JProperty("dEnd",a.dEnd.Value.ToString("dd/MM/yyyy")),
                                             });

                    return rss.ToString();
                }
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string GetData(string tremId)
        {
            JWTToken token = new JWTToken();
            if (!token.CheckToken(HttpContext.Current)) { }
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm.Trim() == tremId);
                    dynamic rss = new JObject();
                    rss.nTerm = f_term.nTerm;
                    rss.sTerm = f_term.sTerm;
                    rss.dStart = f_term.dStart.Value.ToString("dd/MM/yyyy");
                    rss.dEnd = f_term.dEnd.Value.ToString("dd/MM/yyyy");

                    return rss.ToString();
                }
            }
        }
    }
}