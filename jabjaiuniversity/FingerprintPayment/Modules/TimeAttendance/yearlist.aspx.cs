using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class yearlist : BehaviorGateway
    {
        [WebMethod]
        public static string GetPermission()
        {
            return mp.Permission_Page.permission;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("~/Default.aspx");
            //using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    if (!Page.IsPostBack)
            //    {
            //        //var q = _db.TTerms.Where(w => w.cDel == null && w.SchoolID == UserData.CompanyID).ToList();
            //        //lvYear.DataSource = (from a in _db.TYears.Where(w => w.SchoolID == UserData.CompanyID).ToList()
            //        //                     orderby a.numberYear descending
            //        //                     select new
            //        //                     {
            //        //                         a.SchoolID,
            //        //                         a.numberYear,
            //        //                         a.nYear,
            //        //                         count = q.Where(w => w.nYear == a.nYear).Count()
            //        //                     }).ToList();
            //        //lvYear.DataBind();
            //    }
            //}
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static object GetYearData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                {
                    var q1 = (from a in dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                              join b in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.nYear equals b.nYear into rj
                              from ab in rj.DefaultIfEmpty()
                              group ab by new { a.numberYear, a.SchoolID, a.nYear } into gb
                              orderby gb.Key.numberYear descending
                              select new
                              {
                                  gb.Key.SchoolID,
                                  gb.Key.numberYear,
                                  gb.Key.nYear,
                                  count = gb.Count(c => c != null)
                              }).ToList();

                    return q1;
                }
            }
        }

        [ScriptMethod]
        [WebMethod(EnableSession = true)]
        public static string UpdateData(int? YearId, int numberYear, List<TTermData> _termData
            , bool isCheck1
            , bool isCheck2
            , bool isCheck3)
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
                var isAdd = YearId == null ? true : false;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                {

                    try
                    {

                        if (isAdd)
                        {
                            //YearId = dbschool.TYears.Select(s => s.nYear).DefaultIfEmpty(0).Max(max => max + 1);
                            if (dbschool.TYears.FirstOrDefault(f => f.numberYear == numberYear && f.SchoolID == userData.CompanyID && !f.cDel) != null)
                            {
                                rss.Des = "ช่วงเวลานี้มีการบันทึกไปแล้ว";
                                rss.Status = 400;
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

                        rss.Data = new JObject();
                        rss.Data.Year = numberYear;
                        rss.Data.YearId = YearId;

                        foreach (var _t in dbschool.TTerms.Where(w => w.nYear == YearId && w.cDel == null).ToList())
                        {
                            if (_termData.Count(c => c.nTerm == _t.nTerm) == 0)
                            {
                                if (dbschool.TStudentClassroomHistories.Count(f => f.nTerm.Trim() == _t.nTerm.Trim() && f.SchoolID == userData.CompanyID && f.cDel == false) > 0)
                                {
                                    rss.Des = $"ไม่สามารถลบเทอมที่ {_t.sTerm} เนื่องจากมีการนำนักเรียนย้ายเข้าไปในเทอมนี้แล้ว";
                                    rss.Status = 401;
                                    return rss.ToString();
                                }
                                else
                                {
                                    _t.UpdatedBy = userData.UserID;
                                    _t.UpdatedDate = DateTime.Now;
                                    _t.cDel = "1";
                                    dbschool.SaveChanges();
                                }
                            }
                        }

                        foreach (var termData in _termData)
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

                            #region Add Trem
                            var q_trem = (from a in dbschool.TYears.Where(x => x.SchoolID == schoolid && x.cDel == false)
                                          from b in dbschool.TTerms.Where(o => o.SchoolID == schoolid && o.nYear == a.nYear && o.cDel != "1")
                                          select b).ToList();

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
                            f_data.cDel = null;
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

                            if (rss.Status == 200)
                            {
                                dbschool.SaveChanges();
                            }

                            #endregion
                        }


                    }
                    catch (Exception ex)
                    {
                        rss.Status = 500;


                    }


                    if (isAdd)//copy data only insert
                    {

                        if (isCheck1)
                        {
                            try
                            {
                                var selectedYear = numberYear - 543;
                                //var listDate = CalendarHelper.GetDefaultHoliday(selectedYear);

                                var sl = new StudentLogic(dbschool);
                                var currentTerm = sl.GetTermId(DateTime.Now.Date, userData);

                                var fromYaer = (from a in dbschool.TTerms.Where(o => o.nTerm == currentTerm && o.SchoolID == userData.CompanyID)
                                                from b in dbschool.TYears.Where(o => o.SchoolID == a.SchoolID && o.nYear == a.nYear)
                                                select b.numberYear - 543
                                                ).FirstOrDefault();

                                var d1 = new DateTime(fromYaer.Value, 1, 1);
                                var d2 = new DateTime(fromYaer.Value, 12, 31);

                                var listDate = dbschool.THolidays
                                  .Where(o => o.SchoolID == userData.CompanyID && o.cDel != "1"
                                      && (o.dHolidayStart.Value.Year == fromYaer || o.dHolidayEnd.Value.Year == fromYaer)
                                      && (d1 <= o.dHolidayEnd || o.dHolidayStart <= d2))
                                  .ToList();

                                var holidayList = dbschool.THolidays
                                    .Where(o => o.SchoolID == userData.CompanyID && o.cDel != "1"
                                        && (o.dHolidayStart.Value.Year == selectedYear || o.dHolidayEnd.Value.Year == selectedYear))
                                    .ToList();

                                int countRecord = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID).Count();

                                foreach (var d in listDate)
                                {
                                    var isExist = holidayList.Where(o => o.dHolidayStart == d.dHolidayStart && o.dHolidayEnd == d.dHolidayEnd).Count() > 0;

                                    if (!isExist)
                                    {
                                        var now = DateTime.Now;
                                        now = now.AddMilliseconds(-now.Millisecond + 666);

                                        //int countRecord = dbschool.THolidays.Where(w => w.SchoolID == userData.CompanyID).Count();
                                        string holidayID = string.Format(@"{0:#000}{1:000000}", userData.CompanyID, ++countRecord);

                                        var diff1 = Math.Abs(selectedYear - d.dHolidayStart.Value.Year);
                                        var diff2 = Math.Abs(selectedYear - d.dHolidayEnd.Value.Year);

                                        if (d.dHolidayStart.Value.Year < d.dHolidayEnd.Value.Year)
                                        {
                                            var v = (d.dHolidayEnd.Value.Year - d.dHolidayStart.Value.Year); ;
                                            if (fromYaer == d.dHolidayEnd.Value.Year)
                                            {
                                                diff1 = diff1 - v;
                                            }
                                            else
                                            {
                                                diff2 = diff2 + v;
                                            }
                                        }

                                        var start = d.dHolidayStart.Value.AddYears(diff1);
                                        var end = d.dHolidayEnd.Value.AddYears(diff2);

                                        var h = new THoliday()
                                        {
                                            nHoliday = holidayID,
                                            sHoliday = d.sHoliday,
                                            sHolidayType = d.sHolidayType,
                                            dHolidayStart = start,
                                            dHolidayEnd = end,
                                            sHolidayAll = d.sHolidayAll,
                                            sWhoSeeThis = d.sWhoSeeThis,
                                            sColor = d.sColor,
                                            cStatusActive = d.cStatusActive,
                                            SchoolID = userData.CompanyID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = now,
                                            UpdatedBy = userData.UserID,
                                            UpdatedDate = now,
                                        };


                                        dbschool.THolidays.Add(h);

                                        var holidaySome = dbschool.THolidaySomes
                                           .Where(w => w.SchoolID == userData.CompanyID && w.nHoliday == d.nHoliday && w.cDel != true && w.Deleted != 1)
                                           .ToList();

                                        int runID = 1;

                                        foreach (var some in holidaySome)
                                        {
                                            string holidaySomeID = string.Format(@"{0}{1:000}", holidayID, runID++);
                                            var s = new THolidaySome()
                                            {
                                                nHolidaySomeID = holidaySomeID,
                                                nHoliday = holidayID,
                                                nTSubLevel = some.nTSubLevel,

                                                SchoolID = userData.CompanyID,
                                                CreatedBy = userData.UserID,
                                                CreatedDate = now,
                                                UpdatedBy = userData.UserID,
                                                UpdatedDate = now,
                                                cDel = some.cDel,
                                                Deleted = some.Deleted,
                                            };
                                            dbschool.THolidaySomes.Add(s);
                                        }

                                    }
                                }

                                dbschool.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        if (isCheck2)
                        {
                            try
                            {
                                var yearFrom = numberYear - 1;
                                var yearTo = numberYear;
                                var objYearFrom = dbschool.TYears.FirstOrDefault(f => f.numberYear == yearFrom && f.SchoolID == userData.CompanyID);
                                var objYearTo = dbschool.TYears.FirstOrDefault(f => f.numberYear == yearTo && f.SchoolID == userData.CompanyID);

                                ServiceHelper.CopyPlanFromAnotherYear(objYearFrom.nYear
                                    , objYearTo.nYear
                                    , userData.CompanyID
                                    , userData.UserID);
                            }
                            catch (Exception ex)
                            {
                            }
                        }

                        if (isCheck3)
                        {
                            try
                            {
                                var yearFrom = numberYear - 1;
                                var yearTo = numberYear;
                                var objYearFrom = dbschool.TYears.FirstOrDefault(f => f.numberYear == yearFrom && f.SchoolID == userData.CompanyID);
                                var objYearTo = dbschool.TYears.FirstOrDefault(f => f.numberYear == yearTo && f.SchoolID == userData.CompanyID);

                                var dataTermFrom = dbschool.TTerms
                                   .Where(o => o.SchoolID == userData.CompanyID && o.nYear == objYearFrom.nYear)
                                   .Select(o => new { o.nTerm, o.sTerm })
                                   .OrderBy(o => o.sTerm)
                                   .ToList();

                                var dataTermTo = dbschool.TTerms
                                    .Where(o => o.SchoolID == userData.CompanyID && o.nYear == objYearTo.nYear)
                                    .Select(o => new { o.nTerm, o.sTerm })
                                    .OrderBy(o => o.sTerm)
                                    .ToList();

                                var lst2Add = new List<TClassMember>();

                                dbschool.Configuration.ProxyCreationEnabled = false;

                                foreach (var termFrom in dataTermFrom)
                                {
                                    var termTo = dataTermTo.Where(o => o.sTerm == termFrom.sTerm).FirstOrDefault();

                                    var classToRemove = dbschool.TClassMembers.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == termTo.nTerm).ToList();
                                    dbschool.TClassMembers.RemoveRange(classToRemove);

                                    var classFromAdd = dbschool.TClassMembers.Where(o => o.SchoolID == userData.CompanyID && o.nTerm == termFrom.nTerm).ToList();

                                    foreach (var copyTo in classFromAdd)
                                    {
                                        var obj = CloneObject<TClassMember, TClassMember>(copyTo);
                                        obj.nTerm = termTo.nTerm;
                                        lst2Add.Add(obj);
                                    }
                                }

                                dbschool.TClassMembers.AddRange(lst2Add);
                                dbschool.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    //rss.Data = new JObject();
                    //rss.Data.Year = numberYear;
                    //rss.Data.YearId = YearId;
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                {
                    dynamic rss = new JObject();
                    rss.StatusCode = 200;
                    if (dbschool.TTermTimeTables.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else if (_dbGrade.PGTGrades.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else if (dbschool.TStudentClassroomHistories.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else if (dbschool.TPlanCourseTerms.Count(f => f.nTerm.Trim() == termId.Trim() && f.SchoolID == userData.CompanyID) > 0) rss.StatusCode = 401;
                    else
                    {
                        var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm == termId && f.SchoolID == userData.CompanyID);
                        if (f_term != null)
                        {
                            f_term.cDel = "1";
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
                {
                    var q_year = dbschool.TYears.FirstOrDefault(f => f.nYear == YearId);
                    dynamic rss = new JObject();

                    rss.Year = q_year.nYear;
                    rss.YearName = q_year.numberYear;

                    rss.Data = new JArray(from a in dbschool.TTerms.Where(w => w.cDel == null && w.SchoolID == userData.CompanyID).ToList()
                                          where a.nYear == YearId
                                          orderby a.dStart
                                          select new JObject
                                             {
                                                 new JProperty("sTerm",a.sTerm),
                                                 new JProperty("nTerm",a.nTerm),
                                                 new JProperty("dStart",a.dStart.Value.ToString("dd/MM/yyyy",new CultureInfo("th-th"))),
                                                 new JProperty("dEnd",a.dEnd.Value.ToString("dd/MM/yyyy",new CultureInfo("th-th"))),
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany, ConnectionDB.Read)))
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

        private static T1 CloneObject<T1, T2>(T2 source)
        {
            // Don't serialize a null object, simply return the default for that object
            if (ReferenceEquals(source, null)) return default;

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var deserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };

            return JsonConvert.DeserializeObject<T1>(JsonConvert.SerializeObject(source), deserializeSettings);
        }

        public class TTermData
        {
            public Nullable<System.DateTime> dStart { get; set; }
            public Nullable<System.DateTime> dEnd { get; set; }
            public string sTerm { get; set; }
            public string nTerm { get; set; }
        }
    }
}