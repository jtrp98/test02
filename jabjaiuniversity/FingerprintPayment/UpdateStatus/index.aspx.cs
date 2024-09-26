using JabjaiMainClass;
using MasterEntity;
using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Web.Services;
using JabjaiMainClass.Models;
using FingerprintPayment.UpdateStatus.Models;
using FingerprintPayment.Report.Models;
using System.Globalization;
using System.Data.Entity;

namespace FingerprintPayment.UpdateStatus
{
    public partial class index : System.Web.UI.Page
    {
        private JWTToken.userData userData;
        protected JWTToken.userData UserData { get { return userData; } }

        protected override void OnLoad(EventArgs e)
        {
            JWTToken token = new JWTToken();
            userData = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        public string termName { get; set; }
        public string yearName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            if (!this.IsPostBack)
            {
                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                {
                    string sEntities = Session["sEntities"].ToString();
                    var tCompany = db.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();
                    using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                    {
                        //var f_terms = entities.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today);
                        //StudentLogic logic = new StudentLogic(entities);
                        //var f_terms = logic.GetTermDATA(DateTime.Today, userData);

                        //if (f_terms != null)
                        //{
                        //    termName = f_terms.sTerm;
                        //    var f_year = entities.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).FirstOrDefault(f => f.nYear == f_terms.nYear);
                        //    yearName = f_year.numberYear + "";
                        //}

                        var lvlList = entities.TSubLevels
                            .Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1)
                            .OrderBy(o => o.MasterCode)
                            .Select(o => new ListItem
                            {
                                Text = o.SubLevel,
                                Value = o.nTSubLevel + "",
                            })
                            .ToList();

                        //lvlList.Insert(0, new ListItem { Text = "ทั้งหมด", Value = "" });
                        ddlsublevel.DataSource = lvlList;
                        ddlsublevel.DataBind();
                    }
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object GetAllLevelClass()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (var _db = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
            {
                var q = from a in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2Status == "1" && w.nWorkingStatus == 1)
                        from b in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == a.nTSubLevel && w.nWorkingStatus == 1)
                        .OrderBy(o => o.MasterCode)

                        select new
                        {
                            levelId = b.nTSubLevel,
                            levelName = b.SubLevel,
                            classId = a.nTermSubLevel2,
                            className = b.SubLevel + "/" + a.nTSubLevel2,
                        };

                var d = q.ToList()
                    .GroupBy(o => new { o.levelId, o.levelName })
                    .Select(o => new
                    {
                        levelId = o.Key.levelId,
                        levelName = o.Key.levelName,
                        classList = o.Select(i => new
                        {
                            classId = i.classId,
                            className = i.className,
                        })
                    })

                    .ToList();

                return d;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getStudentData(int student_Id, string term)
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
                    StudentLogTime studentLogTime = new StudentLogTime(dbschool, userData);
                    StudentLogic studentLogic = new StudentLogic(dbschool);

                    //var f_terms = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today);
                    var f_terms = dbschool.TTerms
                        .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == term)
                        .FirstOrDefault();
                    //studentLogic.GetTermDATA(DateTime.Today, new JWTToken.userData { CompanyID = userData.CompanyID });
                    DateTime dateStart = f_terms.dStart.Value;
                    DateTime dateEnd = f_terms.dEnd.Value > DateTime.Today ? DateTime.Today : f_terms.dEnd.Value;
                    var logTime = studentLogTime.GetLog4Student(student_Id, dateStart, dateEnd);

                    var studentDatat = (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                        from c in dbschool.TTermSubLevel2.Where(o => o.SchoolID == userData.CompanyID && o.nTermSubLevel2 == a.nTermSubLevel2)
                                        from e in dbschool.TSubLevels.Where(o => o.SchoolID == userData.CompanyID && o.nTSubLevel == c.nTSubLevel)
                                        from b in dbschool.TFamilyProfiles.Where(w => w.SchoolID == userData.CompanyID && w.sID == a.sID).DefaultIfEmpty()
                                        where a.sID == student_Id && a.SchoolID == userData.CompanyID
                                        select new StudentProfileData
                                        {
                                            SchoolID = userData.CompanyID,
                                            Identification = a.sIdentification,
                                            Level = e.SubLevel + "/" + c.nTSubLevel2,
                                            student_Code = a.sStudentID,
                                            student_Name = a.sName + " " + a.sLastname,
                                            dBirth = a.dBirth,
                                            father_Name = b == null ? "" : b.sFatherFirstName + " " + b.sFatherLastName,
                                            mother_Name = b == null ? "" : b.sMotherFirstName + " " + b.sMotherLastName,
                                            Phone = a.sPhone,// b == null ? "" : b.sPhoneOne,
                                                             // Mobile = b == null ? "" : b.sPhoneThree,
                                            Picture = a.sStudentPicture
                                        }).FirstOrDefault();

                    var f_log = (from g in logTime
                                 group g by g.student_Id into gb
                                 select new StudentProfileData.LogTime
                                 {
                                     Status_0 = gb.Count(c => c.LogStatus == "0"),
                                     Status_1 = gb.Count(c => c.LogStatus == "1"),
                                     Status_2 = gb.Count(c => c.LogStatus == "3"),
                                     Status_3 = gb.Count(c => c.LogStatus == "12"),
                                     Status_4 = gb.Count(c => c.LogStatus == "10"),
                                     Status_5 = gb.Count(c => c.LogStatus == "11"),
                                     Status_6 = gb.Count(c => c.LogStatus == "99"),
                                 }).FirstOrDefault();

                    if (f_log == null)
                    {
                        f_log = new StudentProfileData.LogTime
                        {
                            Status_0 = 0,
                            Status_1 = 0,
                            Status_2 = 0,
                            Status_3 = 0,
                            Status_4 = 0,
                            Status_5 = 0,
                            Status_6 = 0,
                        };
                    }

                    studentDatat.logTime = f_log;

                    return studentDatat;

                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object UpdateStudentStatus(Search search)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                //string entities = "JabJaiEntities";//HttpContext.Current.Session["sEntities"].ToString();
                //string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.nCompany == userData.CompanyID);
                var nTeacherId = userData.UserID;//HttpContext.Current.Session["sEmpID"].ToString();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    //StudentLogic logic = new StudentLogic(dbschool);
                    var f_terms = dbschool.TTerms
                        .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == search.term_id)
                        .FirstOrDefault();
                    //var f_terms = logic.GetTermDATA(DateTime.Today, userData);
                    //var f_terms = dbschool.TTerms.FirstOrDefault(f => f.dStart <= DateTime.Today && f.dEnd >= DateTime.Today && f.SchoolID == tCompany.nCompany);
                    if (!search.dStart.HasValue)
                    {
                        search.dStart = DateTime.Today;
                    }
                    if (!search.dEnd.HasValue)
                    {
                        search.dEnd = search.dStart;
                    }
                    //int nTeacherId = int.Parse(user_id);
                    if (search.dStart < f_terms.dStart)
                        search.dStart = f_terms.dStart;
                    if (search.dEnd > f_terms.dEnd)
                        search.dEnd = f_terms.dEnd;
                    //if (f_terms.dStart > search.dStart || f_terms.dEnd < search.dStart) return "Out off day";
                    //if (f_terms.dStart > search.dEnd || f_terms.dEnd < search.dEnd) return "Out off day";

                    var logTime = dbschool.TLogUserTimeScans
                        .Where(w => w.SchoolID == tCompany.nCompany && search.dStart <= w.LogDate && search.dEnd >= w.LogDate && w.sID == search.student_id)
                        .ToList();

                    var times = dbschool.TTimes.Select(s => new TimeTableScan
                    {
                        nDay = s.nDay,
                        nTimeType = s.nTimeType,
                        cDel = s.cDel
                    }).ToList();
                    var holidays = dbschool.THolidays.Where(w => w.sHolidayType == "0" && w.SchoolID == tCompany.nCompany).ToList();
                    var terms = dbschool.TTerms.Where(w => w.SchoolID == tCompany.nCompany && w.nTerm == search.term_id).ToList();
                    var holidaySomes = dbschool.THolidaySomes.Where(w => w.SchoolID == tCompany.nCompany).ToList();
                    var titles = dbschool.TTitleLists.Where(w => w.SchoolID == tCompany.nCompany).ToList();

                    times.ForEach(f =>
                    {
                        switch (f.nDay)
                        {
                            case 0: f.nDay = 1; break;
                            case 1: f.nDay = 2; break;
                            case 2: f.nDay = 3; break;
                            case 3: f.nDay = 4; break;
                            case 4: f.nDay = 5; break;
                            case 5: f.nDay = 6; break;
                            case 6: f.nDay = 0; break;
                        }
                    });

                    //var studentData = (from a in dbschool.TUsers.Where(w => w.SchoolID == tCompany.nCompany)
                    //                   join b in dbschool.TTermSubLevel2.Where(w => w.SchoolID == tCompany.nCompany) on a.nTermSubLevel2 equals b.nTermSubLevel2
                    //                   where a.sID == (search.student_id ?? 0)
                    //                   select new
                    //                   {
                    //                       a.SchoolID,
                    //                       a.sID,
                    //                       b.nTimeType,
                    //                       a.nTermSubLevel2,
                    //                       fullname = a.sName + " " +a.sLastname,                                           
                    //                   }).FirstOrDefault();

                    var studentData = (from a in dbschool.TB_StudentViews.Where(w => w.SchoolID == tCompany.nCompany && w.sID == search.student_id && w.nTerm == f_terms.nTerm )
                                       select new
                                       {
                                           a.SchoolID,
                                           a.sID,
                                           a.nTimeType,
                                           a.nTermSubLevel2,
                                           fullname = a.sName + " " + a.sLastname,
                                           level = a.SubLevel + "/" + a.nTSubLevel2
                                       }).AsQueryable().FirstOrDefault();

                    for (int index = 0; search.dStart.Value.AddDays(index) <= search.dEnd.Value; index++)
                    {
                        DateTime LogDate = search.dStart.Value.AddDays(index);
                        int nDay = (int)LogDate.DayOfWeek;

                        //var q_logday = logTime.Where(f => f.LogDate == LogDate).ToList();
                        var f_logTimeAM = logTime.Where(f => f.LogDate == LogDate && f.LogType == "0").FirstOrDefault();
                        var f_logTimePM = logTime.Where(f => f.LogDate == LogDate && f.LogType == "1").FirstOrDefault();

                        var q_times = times.Where(w => w.nDay == nDay);
                        var f_trems = terms.FirstOrDefault(f => f.dStart <= LogDate && f.dEnd >= LogDate);

                        var f_holidays = holidays.FirstOrDefault(f => f.dHolidayStart <= LogDate && f.dHolidayEnd >= LogDate && string.IsNullOrEmpty(f.cDel));
                        var f_times = q_times.FirstOrDefault(f => f.nTimeType == studentData.nTimeType);

                        if (f_trems == null) continue;
                        else if (f_times == null || f_times.cDel == "0") continue;
                        else if (f_holidays != null && f_holidays.sWhoSeeThis == "0") continue;
                        else if (f_holidays != null && f_holidays.sWhoSeeThis == "3")
                        {
                            var f_holidaySomes = holidaySomes.FirstOrDefault(f => f.nHoliday == f_holidays.nHoliday && f.nTSubLevel == studentData.nTermSubLevel2);
                            if (f_holidaySomes != null) continue;
                        }

                        switch (search.logStatus)
                        {

                            case "3":
                            case "10":
                            case "11":
                                {
                                    if (search.tType == null || search.tType == 0)
                                    {
                                        if (f_logTimeAM == null)
                                        {
                                            dbschool.TLogUserTimeScans.Add(new TLogUserTimeScan
                                            {
                                                SchoolID = userData.CompanyID,
                                                LogScanStatus = search.logStatus,
                                                //LogTime = DateTime.Now.TimeOfDay,
                                                TeacherId = nTeacherId,
                                                LogDate = LogDate,
                                                LognDay = nDay,
                                                LogType = "0",
                                                sID = search.student_id,
                                                nTermSubLevel2 = studentData.nTermSubLevel2,
                                                CreatedDate = DateTime.Now,
                                                UpdatedDate = DateTime.Now,
                                                CreatedBy = userData.UserID,
                                                deviceType = 3
                                            });
                                        }
                                        else
                                        {
                                            if (!(f_logTimeAM.bLockStatus ?? false))
                                            {
                                                f_logTimeAM.LogScanStatus = search.logStatus;
                                                //f_logTimeAM.LogTime = f_logTimeAM.LogTime ?? DateTime.Now.TimeOfDay;
                                                f_logTimeAM.TeacherId = nTeacherId;
                                                f_logTimeAM.SchoolID = userData.CompanyID;
                                                f_logTimeAM.UpdatedDate = DateTime.Now;
                                                f_logTimeAM.UpdatedBy = userData.UserID;
                                            }
                                        }

                                        //if (search.tType == 0)
                                        //{                                
                                        //    if (f_logTimePM != null)
                                        //        switch (f_logTimePM.LogScanStatus)
                                        //        {
                                        //            case "10":
                                        //            case "11":
                                        //                f_logTimeAM.LogScanStatus = "99";
                                        //                break;
                                        //            default:
                                        //                break;
                                        //        }
                                        //}
                                    }

                                    if (search.tType == null || search.tType == 1)
                                    {
                                        if (f_logTimePM == null)
                                        {
                                            dbschool.TLogUserTimeScans.Add(new TLogUserTimeScan
                                            {
                                                SchoolID = userData.CompanyID,
                                                LogScanStatus = search.logStatus,
                                                //LogTime = DateTime.Now.TimeOfDay,
                                                TeacherId = nTeacherId,
                                                LogDate = LogDate,
                                                LognDay = nDay,
                                                LogType = "1",
                                                sID = search.student_id,
                                                nTermSubLevel2 = studentData.nTermSubLevel2,
                                                CreatedDate = DateTime.Now,
                                                UpdatedDate = DateTime.Now,
                                                CreatedBy = userData.UserID,
                                                deviceType = 3
                                            });
                                        }
                                        else
                                        {
                                            if (!(f_logTimePM.bLockStatus ?? false))
                                            {
                                                f_logTimePM.LogScanStatus = search.logStatus;
                                                //f_logTimePM.LogTime = f_logTimePM.LogTime ?? DateTime.Now.TimeOfDay;
                                                f_logTimePM.TeacherId = nTeacherId;
                                                f_logTimePM.SchoolID = userData.CompanyID;
                                                f_logTimePM.UpdatedDate = DateTime.Now;
                                                f_logTimePM.UpdatedBy = userData.UserID;
                                            }
                                        }

                                        //if (search.tType == 1)
                                        //{
                                        //    if (f_logTimeAM != null )
                                        //        switch (f_logTimeAM.LogScanStatus)
                                        //        {
                                        //            case "10":
                                        //            case "11":
                                        //                f_logTimeAM.LogScanStatus = "99";
                                        //                break;
                                        //            default:
                                        //                break;
                                        //        }

                                        //}
                                    }



                                }

                                break;

                            default:
                                {
                                    if (f_logTimeAM == null)
                                    {
                                        dbschool.TLogUserTimeScans.Add(new TLogUserTimeScan
                                        {
                                            SchoolID = userData.CompanyID,
                                            LogScanStatus = search.logStatus,
                                            //LogTime = DateTime.Now.TimeOfDay,
                                            TeacherId = nTeacherId,
                                            LogDate = LogDate,
                                            LognDay = nDay,
                                            LogType = "0",
                                            sID = search.student_id,
                                            CreatedDate = DateTime.Now,
                                            UpdatedDate = DateTime.Now,
                                            CreatedBy = userData.UserID,
                                            deviceType = 3
                                        });
                                    }
                                    else
                                    {
                                        if (!(f_logTimeAM.bLockStatus ?? false))
                                        {
                                            f_logTimeAM.LogScanStatus = search.logStatus;
                                            //f_logTimeAM.LogTime = f_logTimeAM.LogTime ?? DateTime.Now.TimeOfDay;
                                            f_logTimeAM.TeacherId = nTeacherId;
                                            f_logTimeAM.SchoolID = userData.CompanyID;
                                            f_logTimeAM.UpdatedBy = userData.UserID;
                                            f_logTimeAM.UpdatedDate = DateTime.Now;
                                        }
                                    }

                                    if (search.logStatus == "99")
                                    {
                                        var face = dbschool.TFaceScanLogs
                                            .Where(o => o.SchoolID == userData.CompanyID && o.sID == search.student_id && DbFunctions.TruncateTime(o.LogTime) == LogDate)
                                            .FirstOrDefault();

                                        if (face != null)
                                        {
                                            face.Temperature = 0;
                                            face.FaceScanUrl = null;
                                        }
                                    }


                                }

                                break;
                        }

                        database.InsertLog(userData.UserID + "", $"แก้ไขสถานะเป็น {GetStatusName(search.logStatus)} วันที่ {LogDate.ToString("dd/MM/yyyy")}-{LogDate.ToString("dd/MM/yyyy")}  [ชื่อ : {studentData.fullname} ,ชั้น : {studentData.level}]"
                                     , HttpContext.Current.Request, 22, 0, 0, userData.CompanyID);



                    }

                    dbschool.SaveChanges();
                    return "Success !!";
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object UpdateStudentStatus2(Search search)
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                {
                    var f_terms = dbschool.TTerms
                       .Where(o => o.SchoolID == userData.CompanyID && o.nTerm == search.term_id)
                       .FirstOrDefault();

                    if (search.dStart < f_terms.dStart)
                        search.dStart = f_terms.dStart;
                    if (search.dEnd > f_terms.dEnd)
                        search.dEnd = f_terms.dEnd;

                    var dateNow = DateTime.Now;
                    var date15Ago = dateNow.AddDays(-16);

                    DateTime? dateSing1 = null, dateSing2 = null;
                    DateTime? dateHis1 = null, dateHis2 = null;

                    if (search.dStart.Value.Date >= date15Ago.Date)
                    {
                        dateSing1 = search.dStart;
                        dateSing2 = search.dEnd;
                    }
                    else if (search.dEnd.Value.Date < date15Ago.Date)
                    {
                        dateHis1 = search.dStart;
                        dateHis2 = search.dEnd;
                    }
                    else
                    {
                        dateSing1 = date15Ago;
                        dateSing2 = search.dEnd;

                        dateHis1 = search.dStart;
                        dateHis2 = date15Ago.AddDays(-1);
                    }

                    var sl = new StudentLogic(dbschool);

                    if (dateSing1.HasValue)
                    {
                        if (search.tType.HasValue)
                        {
                            var qry = GetQueryUpdateStatus2("JabjaiSchoolSingleDB"
                                 , dateSing1
                                 , dateSing2
                                 , userData.CompanyID
                                 , sl.GetTermId(dateSing2.Value, userData)
                                 , search.logStatus
                                 , search.level2str
                                 , search.tType
                                 , userData.UserID);

                            var result = dbschool.Database.SqlQuery<object>(qry).ToList();
                        }
                        else
                        {
                            if (search.logStatus == "0" || search.logStatus == "1")
                            {
                                var qry = GetQueryUpdateStatus2("JabjaiSchoolSingleDB"
                                     , dateSing1
                                     , dateSing2
                                     , userData.CompanyID
                                     , sl.GetTermId(dateSing2.Value, userData)
                                     , search.logStatus
                                     , search.level2str
                                     , 0
                                     , userData.UserID);

                                var result = dbschool.Database.SqlQuery<object>(qry).ToList();

                            }
                            else
                            {
                                foreach (var i in new int[] { 0, 1 })
                                {
                                    var qry = GetQueryUpdateStatus2("JabjaiSchoolSingleDB"
                                       , dateSing1
                                       , dateSing2
                                       , userData.CompanyID
                                       , sl.GetTermId(dateSing2.Value, userData)
                                       , search.logStatus
                                       , search.level2str
                                       , i
                                       , userData.UserID);

                                    var result = dbschool.Database.SqlQuery<object>(qry).ToList();
                                }
                            }
                        }

                        database.InsertLog(userData.UserID + "", $"แก้ไขสถานะเป็น {GetStatusName(search.logStatus)} วันที่ {dateSing1?.ToString("dd/MM/yyyy")}-{dateSing2?.ToString("dd/MM/yyyy")}  [ห้อง : {GetLevelName(dbschool, search.level2str, userData.CompanyID)}]"
                            , HttpContext.Current.Request, 22, 0, 0, userData.CompanyID);
                    }

                    if (dateHis1.HasValue)
                    {
                        if (search.tType.HasValue)
                        {
                            var qry = GetQueryUpdateStatus2("JabjaiSchoolHistory"
                                 , dateHis1
                                 , dateHis2
                                 , userData.CompanyID
                                 , sl.GetTermId(dateHis2.Value, userData)
                                 , search.logStatus
                                 , search.level2str
                                 , search.tType
                                 , userData.UserID);

                            var result = dbschool.Database.SqlQuery<object>(qry).ToList();
                        }
                        else
                        {
                            if (search.logStatus == "0" || search.logStatus == "1")
                            {
                                var qry = GetQueryUpdateStatus2("JabjaiSchoolHistory"
                                     , dateHis1
                                     , dateHis2
                                     , userData.CompanyID
                                     , sl.GetTermId(dateHis2.Value, userData)
                                     , search.logStatus
                                     , search.level2str
                                     , 0
                                     , userData.UserID);

                                var result = dbschool.Database.SqlQuery<object>(qry).ToList();
                            }
                            else
                            {
                                foreach (var i in new int[] { 0, 1 })
                                {
                                    var qry = GetQueryUpdateStatus2("JabjaiSchoolHistory"
                                       , dateHis1
                                       , dateHis2
                                       , userData.CompanyID
                                       , sl.GetTermId(dateHis2.Value, userData)
                                       , search.logStatus
                                       , search.level2str
                                       , i
                                       , userData.UserID);

                                    var result = dbschool.Database.SqlQuery<object>(qry).ToList();
                                }
                            }
                        }

                        database.InsertLog(userData.UserID + "", $"แก้ไขสถานะเป็น {GetStatusName(search.logStatus)} วันที่ {dateHis1?.ToString("dd/MM/yyyy")}-{dateHis2?.ToString("dd/MM/yyyy")} [ห้อง : {GetLevelName(dbschool, search.level2str, userData.CompanyID)}]"
                          , HttpContext.Current.Request, 22, 0, 0, userData.CompanyID);
                    }

                }
            }



            return "success";
        }

        private static string GetQueryUpdateStatus2(string dbName
            , DateTime? date1
            , DateTime? date2
            , int schoolId
            , string nTerm
            , string toStatus
            , string level2
            , int? logType
            , int userid)
        {
            var qry1 = $@"
DECLARE @DayOfWeek int
DECLARE @FromStatus int
DECLARE @ToStatus int
DECLARE @StartDate AS DATETIME
DECLARE @EndDate AS DATETIME
DECLARE @CurrentDate AS DATETIME
DECLARE @SchoolID AS int
DECLARE @LogType AS int
DECLARE @TermID AS nvarchar(100)

SET @ToStatus = {toStatus}
SET @StartDate = '{date1?.ToString("yyyy-MM-dd", new CultureInfo("en-US"))}'
SET @EndDate = '{date2?.ToString("yyyy-MM-dd", new CultureInfo("en-US"))}'
SET @CurrentDate = @StartDate
SET @SchoolID = {schoolId}
SET @LogType = {(logType.HasValue ? logType : 0)}
SET @TermID = '{nTerm}' 

SELECT *
INTO #TBStudent
FROM [JabjaiSchoolSingleDB].[dbo].TB_StudentViews
WHERE  SchoolID = @SchoolID
AND nTerm = @TermID
AND ISNULL(nStudentStatus,0) = 0
AND cDel is null
{(string.IsNullOrEmpty(level2) ? "" : $" AND nTermSubLevel2 in ({level2})")}

WHILE (@CurrentDate <= @EndDate)
BEGIN
   IF( LOWER(DATENAME(w,@CurrentDate)) <> 'sunday' and LOWER(DATENAME(w,@CurrentDate)) <> 'saturday')
   --IF( LOWER(DATENAME(w,@CurrentDate)) = 'wednesday' )
   --IF( LOWER(DATENAME(w,@CurrentDate)) <> 'X' )
   BEGIN
		
	SELECT @DayOfWeek = CASE LOWER(DATENAME(w,@CurrentDate))
				WHEN 'monday' THEN 1
				WHEN 'Tuesday' THEN 2
				WHEN 'Wednesday' THEN 3
				WHEN 'Thursday' THEN 4
				WHEN 'Friday' THEN 5
				WHEN 'Saturday' THEN 6
				WHEN 'Sunday' THEN 0
				END

		--select * FROM JabjaiSchoolSingleDB.[dbo].[TLogUserTimeScan]
	    UPDATE {dbName}.[dbo].[TLogUserTimeScan] SET LogScanStatus = @ToStatus ,UpdatedBy = {userid} , UpdatedDate = DATEADD(hh,7,GETUTCDATE()) --, LogTime = null
		WHERE 1=1
		and LogDate = @CurrentDate
		and SchoolID = @SchoolID
		AND LogScanStatus <> @ToStatus
        AND LogType = @LogType
		and sID in (
			SELECT distinct sID	FROM #TBStudent 
		)
		
		INSERT INTO  {dbName}.[dbo].[TLogUserTimeScan]
					([sID]
					,[LogTime]
					,[LogType]
					,[LogScanStatus]
					,[LognDay]
					,[LogDate]
					,[nYear]
					,[nTermSubLevel2]
					,[TeacherId]
					,[bLockStatus]
					,[SchoolID]
					,[CreatedBy]
					,[UpdatedBy]
					,[CreatedDate]
					,[UpdatedDate]
					,[cDel])

		 SELECT sID 
				,null--'00:00' --LogTime
				,@LogType --LogType```````
				,@ToStatus --LogScanStatus
				,@DayOfWeek --LognDay
				,@CurrentDate --LogDate
				,null
				,[nTermSubLevel2] --nTermSubLevel2
				,null
				,null
				,@SchoolID
				,{userid}
				,null
				,DATEADD(hh,7,GETUTCDATE())
				,DATEADD(hh,7,GETUTCDATE())
				,0
		     
			FROM (

				select A.sID , B.nTermSubLevel2
				FROM (
					SELECT distinct sID				
					FROM #TBStudent					
			
					EXCEPT

					select distinct sID 
					from  {dbName}.[dbo].[TLogUserTimeScan] 
					where SchoolID = @SchoolID 
					and LogDate in (@CurrentDate)				
					and LogType = @LogType				

				)A INNER JOIN #TBStudent B ON A.sID = B.sID 
				
			)T
  
   END

   SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate); /*increment current date*/
END

DROP TABLE #TBStudent
";
            return qry1;
        }

        private static string GetStatusName(string status)
        {
            switch (status)
            {
                case "0":
                    return "ตรงเวลา";
                case "1":
                    return "สาย";
                case "3":
                    return "ขาด";
                case "12":
                    return "กิจกรรม";
                case "10":
                    return "ลากิจ";
                case "11":
                    return "ลาป่วย";
                case "8":
                    return "วันหยุด";
                case "99":
                    return "ไม่ระบุ";
                default:
                    return status;
            }

        }

        private static string GetLevelName(JabJaiEntities dbschool, string level, int schoolId)
        {
            var arr = level.Split(',')
                .Where(o => !string.IsNullOrEmpty(o))
                .Select(o => int.Parse(o));
            var q = from a in dbschool.TTermSubLevel2
                    .Where(o => o.SchoolID == schoolId && arr.Contains(o.nTermSubLevel2))
                    from b in dbschool.TSubLevels
                     .Where(o => o.SchoolID == schoolId && o.nTSubLevel == a.nTSubLevel)

                    select new
                    {
                        a.nTSubLevel2,
                        b.SubLevel,
                    };
            var d = q.AsEnumerable()
                .Select(o => $"{o.SubLevel}/{o.nTSubLevel2}")
                .ToList();

            return string.Join(",", d);
        }

        private class TimeTableScan
        {
            public int? nDay { get; set; }
            public int? nTimeType { get; set; }
            public string cDel { get; set; }
            public int SchoolID { get; set; }
        }

        //protected void ddlsublevel_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
        //    {
        //        var id = ddlsublevel.SelectedValue;

        //        if (string.IsNullOrEmpty(id))
        //        {
        //            return;
        //        }
        //        //foreach (var lv1 in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == nidlv && w.nTermSubLevel2Status == "1" && w.nWorkingStatus == 1))
        //        //{
        //        //    var lv2 = _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == nidlv).FirstOrDefault();
        //        //    ddl = new ddl();
        //        //    ddl.name = lv2.SubLevel.TrimEnd() + " / " + lv1.nTSubLevel2;
        //        //    ddl.value = lv1.nTermSubLevel2.ToString();
        //        //    int n;
        //        //    bool isNumeric = int.TryParse(lv1.nTSubLevel2, out n);
        //        //    if (isNumeric == true)
        //        //        ddl.sort = Int32.Parse(lv1.nTSubLevel2);
        //        //    else ddl.sort = 500;

        //        //    ddlList.Add(ddl);
        //        //}
        //        var _id = Convert.ToInt32(id);
        //        var q1 = from a in ctx.TTermSubLevel2.Where(o => o.SchoolID == userData.CompanyID
        //                && o.nTSubLevel == _id
        //                && o.nTermSubLevel2Status == "1"
        //                && o.nWorkingStatus == 1)

        //                 from b in ctx.TSubLevels.Where(o => o.SchoolID == userData.CompanyID
        //                 && o.nTSubLevel == a.nTSubLevel)

        //                 select new ListItem
        //                 {
        //                     Text = b.SubLevel + "/" + a.nTSubLevel2,
        //                     Value = a.nTermSubLevel2 + "",
        //                 };

        //        var d1 = q1.ToList();

        //        d1.Insert(0, new ListItem { Text = "ทั้งหมด", Value = "" });
        //        ddlsublevel2.DataSource = d1;
        //        ddlsublevel2.DataBind();
        //    }
        //}
    }
}