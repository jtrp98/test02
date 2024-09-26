using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using JabjaiMainClass;
using Newtonsoft.Json.Linq;
using System.Data;
using JabjaiEntity.DB;
using MasterEntity;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for dataJSON1
    /// </summary>
    public class dataJSONArray : IHttpHandler, IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        //public static SqlConnection connMaster = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionSQLMaster"]);
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current))
            {
                userData = token.getTokenValues(HttpContext.Current);
            }
            else
            {
                throw new Exception();
            }

            dynamic rss = new JObject();
            //string sEntities = HttpContext.Current.Session["sEntities"].ToString();
            //string sEntities = "JabJaiEntities";
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                int schoolID = userData.CompanyID;
                using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string sMode = fcommon.ReplaceInjection(context.Request["mode"]);
                    string SubLevel2 = fcommon.ReplaceInjection(context.Request["sublevel2"]);
                    string SubLevel = fcommon.ReplaceInjection(context.Request["sublevel"]);
                    string nTerm = fcommon.ReplaceInjection(context.Request["nTerm"]);
                    string Plane = fcommon.ReplaceInjection(context.Request["plane"]);
                    int sid = int.Parse(string.IsNullOrEmpty(context.Request["sid"]) ? "0" : fcommon.ReplaceInjection(context.Request["sid"]));
                    int nTermSubLevel2 = 0;
                    try
                    {
                        switch (sMode)
                        {
                            case "listplane":
                                #region
                                nTermSubLevel2 = int.Parse(SubLevel2);
                                var lPlane = (from a in db.TTermTimeTables.Where(w => w.SchoolID == schoolID)
                                              join b in db.TSchedules.Where(w => w.SchoolID == schoolID) on a.nTermTable equals b.nTermTable
                                              join c in db.TPlanes.Where(w => w.SchoolID == schoolID && w.cDel == null) on b.sPlaneID equals c.sPlaneID
                                              where a.nTermSubLevel2 == nTermSubLevel2 && a.nTerm == nTerm
                                              select new { c.sPlaneID, c.sPlaneName }).ToList();
                                #endregion
                                break;
                            case "listsublevel":
                                #region
                                rss = new JArray(from a in QueryDataBases.SubLevel_Query.GetData(db, userData)
                                                 select new JObject(
                                                     new JProperty("text", a.class_name),
                                                     new JProperty("values", a.class_id.ToString())
                                                     ));
                                #endregion
                                break;
                            case "listsublevel2":
                                #region 
                                int nTSubLevel = int.Parse(fcommon.ReplaceInjection(context.Request["SubLevel"]));
                                var lSubLevel2 = (from a in db.TSubLevels.Where(w => w.SchoolID == schoolID)
                                                  join b in db.TTermSubLevel2.Where(w => w.SchoolID == schoolID) on a.nTSubLevel equals b.nTSubLevel
                                                  where b.nTSubLevel == nTSubLevel
                                                  select new
                                                  {
                                                      a.nTLevel,
                                                      NameSubLevel2 = a.SubLevel.Trim() + " / " + b.nTSubLevel2.ToString(),
                                                      b.nTermSubLevel2
                                                  }).ToList();

                                rss = new JArray(from a in lSubLevel2
                                                 select new JObject(
                                                     new JProperty("text", a.NameSubLevel2),
                                                     new JProperty("values", a.nTermSubLevel2)
                                                     ));
                                #endregion
                                break;
                            case "listschedule":
                                #region
                                //nTerm = "TS0000004";
                                //nTerm = string.Format("TS{0:0000000}", 4);
                                nTermSubLevel2 = int.Parse(SubLevel2);

                                var listschedule = (from a in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID)
                                                    join c in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                                                    join b in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals b.sPlaneID
                                                    where c.nTerm.Trim() == nTerm.Trim() && c.nTermSubLevel2 == nTermSubLevel2 && a.cDel == null && b.cDel == null
                                                    select new
                                                    {
                                                        a.sScheduleID,
                                                        a.sPlaneID,
                                                        b.sPlaneName,
                                                        a.tEnd,
                                                        a.tStart,
                                                        a.dTimeStart_IN,
                                                        a.dTimeStart_OUT,
                                                        a.dTimeEnd_IN,
                                                        a.dTimeEnd_OUT,
                                                        a.nPlaneDay,
                                                        b.courseCode
                                                    }).ToList();
                                if (listschedule != null && listschedule.Count > 0)
                                {
                                    if (listschedule.Where(w=> w.tStart == TimeSpan.Parse("00:00:00.0000000") || w.tStart < TimeSpan.Parse("05:00:00.0000000")).Count() > 0)
                                    {
                                        foreach (var s in listschedule.Where(w => w.tStart == TimeSpan.Parse("00:00:00.0000000") || w.tStart < TimeSpan.Parse("05:00:00.0000000")).ToList())
                                        {
                                            TSchedule tSchedule = db.TSchedules.FirstOrDefault(f => f.sScheduleID == s.sScheduleID);
                                            tSchedule.cDel = true;
                                            db.SaveChanges();
                                        }

                                        listschedule = listschedule.Where(w => w.tStart != TimeSpan.Parse("00:00:00.0000000") || w.tStart > TimeSpan.Parse("05:00:00.0000000")).ToList();
                                    }
                                }
                                for (int i = 0; i <= 6; i++)
                                {
                                    rss[GetDayName(i)] = new JArray(
                                        from a in listschedule
                                        where i == a.nPlaneDay
                                        select new JObject(
                                            new JProperty("id", a.tStart.Value.ToString().Split('.')[0].Replace(":", ".").Substring(0, 5)),
                                            new JProperty("name", GetDayName(i)),
                                            new JProperty("timestart", a.tStart.Value.ToString().Substring(0, 5)),
                                            new JProperty("timeend", a.tEnd.Value.ToString().Substring(0, 5)),
                                            new JProperty("planename", a.sPlaneName),
                                            new JProperty("scheduleid", a.sScheduleID),
                                            new JProperty("planeid", a.sPlaneID),
                                            new JProperty("course_code", a.courseCode)
                                            ));
                                    //rss[GetDayName(i)].Add(JSonData);
                                }
                                #endregion
                                break;
                            case "grouplistschedule":
                                #region
                                //if (!string.IsNullOrEmpty(nTerm)) nTerm = string.Format("TS{0:0000000}", int.Parse(nTerm));
                                nTermSubLevel2 = int.Parse(SubLevel2);
                                var grouplistschedule = (from a in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID)
                                                         join c in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                                                         join b in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals b.sPlaneID
                                                         where c.nTerm == nTerm && c.nTermSubLevel2 == nTermSubLevel2 && b.cDel == null
                                                         select new { a.sPlaneID, b.sPlaneName, b.courseCode }
                                                    ).ToList();

                                grouplistschedule = grouplistschedule.GroupBy(gr => gr.sPlaneID).Select(se => se.First()).ToList();

                                rss = new JArray(
                                    from a in grouplistschedule
                                    select new JObject(
                                        new JProperty("id", a.sPlaneID),
                                        new JProperty("name", a.courseCode + " - " + a.sPlaneName)
                                        ));
                                #endregion
                                break;
                            case "schedule":
                                #region 
                                int sScheduleID = int.Parse(context.Request["scheduleid"]);
                                var schedule = (from a in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID)
                                                join c in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals c.nTermTable
                                                join b in db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.sPlaneID equals b.sPlaneID
                                                join d in db.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on a.sEmp equals d.sEmp into dlj
                                                from emp in dlj.DefaultIfEmpty()
                                                join e in db.TClasses.Where(w => w.SchoolID == userData.CompanyID) on a.sClassID equals e.sClassID into elj
                                                from tclass in elj.DefaultIfEmpty()
                                                where a.sScheduleID == sScheduleID
                                                select new
                                                {
                                                    a.sScheduleID,
                                                    a.sPlaneID,
                                                    b.sPlaneName,
                                                    a.tEnd,
                                                    a.tStart,
                                                    a.dTimeStart_IN,
                                                    a.dTimeStart_OUT,
                                                    a.dTimeEnd_IN,
                                                    a.dTimeEnd_OUT,
                                                    a.dTimeHalf,
                                                    a.nTimeLate,
                                                    a.nPlaneDay,
                                                    a.sEmp,
                                                    TeacherName = emp == null ? "" : emp.sName + " " + emp.sLastname,
                                                    TeacherID = emp == null ? null : a.sEmp,
                                                    ClassId = a.sClassID ?? null,
                                                    ClassName = a.sClassID ?? null,
                                                    active = a.cActive,
                                                    calculate = a.calculate
                                                }).ToList();
                                foreach (var data in schedule)
                                {
                                    rss = new JObject(
                                        new JProperty("roomid", data.ClassId),
                                        new JProperty("rooms", data.ClassName),
                                        new JProperty("planeid", data.sPlaneID),
                                        new JProperty("planename", data.sPlaneName),
                                        new JProperty("scheduleid", data.sScheduleID),
                                        new JProperty("teacherid", data.TeacherID),
                                        new JProperty("teachername", data.TeacherName),
                                        new JProperty("day", data.nPlaneDay),
                                        new JProperty("timelate", data.nTimeLate),
                                        new JProperty("timehalf", data.dTimeHalf.ToString().Substring(0, 5)),
                                        new JProperty("timeend_in", data.dTimeEnd_IN.ToString().Substring(0, 5)),
                                        new JProperty("timeend_out", data.dTimeEnd_OUT.ToString().Substring(0, 5)),
                                        new JProperty("timestart_in", data.dTimeStart_IN.ToString().Substring(0, 5)),
                                        new JProperty("timestart_out", data.dTimeStart_OUT.ToString().Substring(0, 5)),
                                        new JProperty("tstart", data.tStart.ToString().Substring(0, 5)),
                                        new JProperty("tend", data.tEnd.ToString().Substring(0, 5)),
                                        new JProperty("active", data.active),
                                        new JProperty("calculate", data.calculate));
                                }
                                #endregion
                                break;
                            case "smddetail":
                                #region
                                int SMSId = int.Parse(fcommon.ReplaceInjection(context.Request["smsid"]));
                                var lSMS = db.TSMS.FirstOrDefault(w => w.SchoolID == schoolID && w.nSMS == SMSId);

                                TMessageBox messageBox = new TMessageBox();
                                messageBox = db.Database.SqlQuery<TMessageBox>($"SELECT * FROM [TMessageBox] WHERE SchoolID = {schoolID} AND push_id = {SMSId}").FirstOrDefault();
                                if (messageBox == null) messageBox = db.Database.SqlQuery<TMessageBox>($"SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox] WHERE SchoolID = {schoolID} AND push_id = {SMSId}").FirstOrDefault();
                                if (messageBox == null) messageBox = db.Database.SqlQuery<TMessageBox>($"SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox.old] WHERE SchoolID = {schoolID} AND push_id = {SMSId}").FirstOrDefault();

                                MessageBoxeLogic messageBoxeLogic = new MessageBoxeLogic(db, userData);
                                var messageBoxUser = messageBoxeLogic.GetMessageUser(messageBox.nMessageID);

                                var qmessage = messageBoxUser.Select(s => new { s.UserID, s.read_status, messageBox.push_id, user_type = s.user_type.ToString() }).ToList();

                                //var quser = db.TUsers.Where(w => w.SchoolID == schoolID && (w.nStudentStatus ?? 0) != 2)
                                //    .Select(a => new
                                //    {
                                //        a.sID,
                                //        a.sName,
                                //        a.sLastname,
                                //        a.cType,
                                //        a.SchoolID,
                                //    }).ToList();

                                var qfiles = db.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == SMSId).ToList();
                                rss.files = new JArray(from a in qfiles
                                                       select new JObject {
                                                       new JProperty("filesname", a.sFileName),
                                                       new JProperty("contenttype", a.ContentType) });

                                rss.type = lSMS.SMSType.Value == 0 ? "ส่งทันที" : "ตั้งเวลาการส่ง";
                                rss.daysend = lSMS.dSend.Value.ToString("dd/MM/yyyy");
                                rss.timesend = lSMS.dSend.Value.ToString("HH:mm");
                                rss.texttype = lSMS.SMSDuration.Value == 0 ? "แจ้งประกาศกิจกรรม" : "แจ้งประกาศข่าวสาร";
                                rss.useradd = new JObject();
                                if (lSMS.useradd.HasValue)
                                {
                                    var quseradd = db.TEmployees.FirstOrDefault(f => f.SchoolID == schoolID && f.sEmp == lSMS.useradd.Value);
                                    if (quseradd != null) rss.useradd = quseradd.sName + " " + quseradd.sLastname;
                                }
                                rss.text = lSMS.SMSDesp;

                                rss.user = new JArray();
                                rss.user = new JArray(from a in (from a in qmessage
                                                                 join b in db.TEmployees.Where(w => w.SchoolID == schoolID) on a.UserID equals b.sEmp
                                                                 where a.user_type == "1"
                                                                 select new
                                                                 {
                                                                     name = b.sName + " " + b.sLastname,
                                                                     status = a.read_status,
                                                                     type = b.cType == "1" ? "พนักงาน" : "อาจารย์"
                                                                 })
                                                                 .Union
                                                                 (from a in qmessage
                                                                  join b in db.TUser.Where(w => w.SchoolID == schoolID && (w.nStudentStatus ?? 0) != 2) on a.UserID equals b.sID
                                                                  where a.user_type == "0"
                                                                  select new
                                                                  {
                                                                      name = b.sName + " " + b.sLastname,
                                                                      status = a.read_status,
                                                                      type = "นักเรียน"
                                                                  }).ToList()
                                                      select new JObject{
                                              new JProperty("name", a.name),
                                              new JProperty("type",a.type),
                                              new JProperty("status",a.status),
                                          });

                                #endregion
                                break;
                            case "listplane4homework":
                                #region
                                if (!string.IsNullOrEmpty(GetTermId(db.TTerms.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList(), ref nTerm)))
                                {
                                    var tTermTimeTable = db.TTermTimeTables.Where(w => w.SchoolID == schoolID);//.Where(w => w.nTerm == nTerm).ToList();
                                    var listplane4homework = (from a in tTermTimeTable
                                                              join b in db.TSchedules.Where(w => w.SchoolID == schoolID) on a.nTermTable equals b.nTermTable
                                                              join c in db.TPlanes.Where(w => w.SchoolID == schoolID && w.cDel == null) on b.sPlaneID equals c.sPlaneID
                                                              group c.sPlaneID by new { c.sPlaneID, c.sPlaneName, c.courseCode } into gb
                                                              select new { gb.Key.sPlaneID, gb.Key.sPlaneName, gb.Key.courseCode }).ToList();

                                    rss = new JArray(from a in listplane4homework
                                                     select new JObject(
                                                         new JProperty("planeid", a.sPlaneID),
                                                         new JProperty("planename", a.courseCode + " - " + a.sPlaneName)));
                                }
                                #endregion
                                break;
                            case "tablelevel":
                                #region
                                List<TTimetype> lTimetype = db.TTimetypes.Where(w => w.SchoolID == userData.CompanyID).ToList();
                                List<TSubLevel> lSubLevel = db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();
                                List<TTermSubLevel2> lTermSubLevel2 = db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();
                                rss = new JArray();
                                foreach (var dSublevel in lSubLevel)
                                {
                                    dynamic ArrySublevel = new JObject();
                                    ArrySublevel.SublevelName = dSublevel.SubLevel.Trim();
                                    ArrySublevel.SublevelId = dSublevel.nTSubLevel;
                                    ArrySublevel.Sublevel2 = new JArray();
                                    foreach (var dSublevel2 in lTermSubLevel2.Where(w => w.nTSubLevel == dSublevel.nTSubLevel))
                                    {
                                        dynamic dySublevel2 = new JObject();
                                        dySublevel2.level2Name = dSublevel.SubLevel.Trim() + " / " + dSublevel2.nTSubLevel2;
                                        dySublevel2.level2Id = dSublevel2.nTermSubLevel2;
                                        ArrySublevel.Sublevel2.Add(dySublevel2);
                                    }
                                    rss.Add(ArrySublevel);
                                }
                                #endregion
                                break;
                            case "tablelevel4homework":
                                #region
                                if (!string.IsNullOrEmpty(GetTermId(db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList(), ref nTerm)))
                                {
                                    List<TTermTimeTable> lLevel4Trem = db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm.Trim() == nTerm.Trim()).ToList();
                                    var lLevel4homework = (from a in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
                                                           join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTSubLevel equals b.nTSubLevel
                                                           join c in lLevel4Trem on b.nTermSubLevel2 equals c.nTermSubLevel2.Value
                                                           join d in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on c.nTermTable equals d.nTermTable
                                                           where d.sPlaneID.ToString() == Plane
                                                           select new { a.nTSubLevel, a.SubLevel, b.nTermSubLevel2, b.nTSubLevel2 }).ToList();

                                    rss = new JArray();
                                    foreach (var dSublevel in lLevel4homework.GroupBy(gb => new { gb.nTSubLevel, gb.SubLevel }).Select(s => new { s.Key.nTSubLevel, s.Key.SubLevel }))
                                    {
                                        dynamic ArrySublevel = new JObject();
                                        ArrySublevel.SublevelName = dSublevel.SubLevel.Trim();
                                        ArrySublevel.SublevelId = dSublevel.nTSubLevel;
                                        ArrySublevel.Sublevel2 = new JArray();
                                        foreach (var dSublevel2 in lLevel4homework.Where(w => w.nTSubLevel == dSublevel.nTSubLevel)
                                            .GroupBy(gb => new { gb.nTermSubLevel2, gb.nTSubLevel2 })
                                            .Select(s => new { s.Key.nTermSubLevel2, s.Key.nTSubLevel2 }))
                                        {
                                            dynamic dySublevel2 = new JObject();
                                            dySublevel2.level2Name = dSublevel.SubLevel.Trim() + " / " + dSublevel2.nTSubLevel2;
                                            dySublevel2.level2Id = dSublevel2.nTermSubLevel2;
                                            ArrySublevel.Sublevel2.Add(dySublevel2);
                                        }
                                        rss.Add(ArrySublevel);
                                    }
                                }
                                #endregion
                                break;
                            case "student4homework":
                                #region
                                //var lStudent = (from a in db.TTermTimeTables.ToList()
                                //                join b in db.TUsers.ToList() on a.nTermSubLevel2 equals b.nTermSubLevel2
                                //                where a.nTerm == "TS0000004"
                                //                select new { a.nTermSubLevel2, a.nTermTable });
                                #endregion
                                break;
                            case "sublevel4homework":
                                #region
                                if (!string.IsNullOrEmpty(GetTermId(db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null).ToList(), ref nTerm)))
                                {
                                    var lSublevel4Homework = (from a in db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID)
                                                              join b in db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.nTermSubLevel2 equals b.nTermSubLevel2
                                                              join c in db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on b.nTSubLevel equals c.nTSubLevel
                                                              join d in db.TSchedules.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals d.nTermTable
                                                              where a.nTerm == nTerm && d.sPlaneID.ToString() == Plane
                                                              group b.nTermSubLevel2 by new { c.nTSubLevel, c.SubLevel, b.nTermSubLevel2, b.nTSubLevel2 } into gb
                                                              select new { gb.Key.nTermSubLevel2, gb.Key.nTSubLevel2, gb.Key.SubLevel, gb.Key.nTSubLevel }).ToList();

                                    rss = new JArray();
                                    foreach (var dataLevel in (from a in lSublevel4Homework
                                                               group a by new { a.nTSubLevel, a.SubLevel } into gb
                                                               select new { gb.Key.SubLevel, gb.Key.nTSubLevel }))
                                    {
                                        dynamic ArrayLevel = new JObject();
                                        ArrayLevel.SublevelName = dataLevel.nTSubLevel;
                                        ArrayLevel.SublevelId = dataLevel.SubLevel.Trim();
                                        ArrayLevel.Level2 = new JArray(from a in lSublevel4Homework
                                                                       where a.SubLevel == dataLevel.SubLevel
                                                                       select new JObject(
                                                                       new JProperty("Sublevel2Id", a.nTermSubLevel2),
                                                                       new JProperty("Sublevel2Name", a.SubLevel.Trim() + " / " + a.nTSubLevel2)));
                                        rss.Add(ArrayLevel);
                                    }
                                }
                                #endregion
                                break;
                            case "sublevel":
                                #region


                                var q1 = QueryDataBases.SubLevel_Query.GetData(db, userData);
                                var nTSubLevels = (q1 != null) ? q1.Select(s => s.class_id).ToList() : new List<int>();
                                var q2 = db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && nTSubLevels.Contains(w.nTSubLevel)).ToList();
                                rss = new JArray(from a in q1
                                                 select new JObject
                                             {
                                                 new JProperty("SublevelId",a.class_id),
                                                 new JProperty("SublevelName",a.class_name),
                                                 new JProperty("Level2",(from b in q2
                                                                       where b.nTSubLevel == a.class_id
                                                                       orderby b.nTSubLevel2
                                                                       select new JObject
                                                                       {
                                                                           new JProperty("Sublevel2Id", b.nTermSubLevel2),
                                                                           new JProperty("Sublevel2Name", a.class_name + " / " + b.nTSubLevel2)
                                                                       })
                                             )});
                                #endregion
                                break;
                            case "year":
                                #region
                                rss = new JArray(from a in db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).OrderByDescending(o => o.numberYear).ToList()
                                                 select new JObject(
                                                     new JProperty("text", a.numberYear),
                                                     new JProperty("values", a.nYear)
                                                     ));
                                #endregion
                                break;
                            case "term":
                                #region
                                int nYear = int.Parse(fcommon.ReplaceInjection(context.Request["year"]));
                                rss = new JArray(from a in db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nYear == nYear && w.cDel == null).ToList()
                                                 select new JObject {
                                                 new JProperty("text", a.nTerm),
                                                 new JProperty("values", a.sTerm),
                                                 new JProperty("minDate", a.dStart),
                                                 new JProperty("maxDate", a.dEnd)
                                             });
                                #endregion
                                break;
                            case "listroom":
                                #region
                                int Level = int.Parse(SubLevel);
                                rss = new JArray(from a in QueryDataBases.SubLevel2_Query.GetData(db, Level, userData)
                                                 select new JObject(
                                                     new JProperty("lvname", a.classRoom_name),
                                                     new JProperty("lv2id", a.classRoom_id)
                                                     ));
                                #endregion
                                break;
                            case "getschedule":
                                #region
                                rss = GetSchedule(context, db);
                                #endregion
                                break;
                            case "getprovince":
                                #region
                                var qprovinces = dbmaster.provinces.ToList();
                                rss = new JArray(from a in qprovinces
                                                 select new JObject(
                                                    new JProperty("PROVINCE_ID", a.PROVINCE_ID),
                                                    new JProperty("PROVINCE_NAME", a.PROVINCE_NAME))
                                                    );
                                break;
                            #endregion
                            case "gettumbon":
                                #region
                                rss = new JArray(from a in dbmaster.districts.Where(w => w.AMPHUR_ID == sid).ToList()
                                                 select new JObject(
                                                    new JProperty("DISTRICT_ID", a.DISTRICT_ID),
                                                    new JProperty("DISTRICT_NAME", a.DISTRICT_NAME))
                                                  );
                                break;
                            #endregion
                            case "getaumper":
                                #region
                                rss = new JArray(from a in dbmaster.amphurs.Where(w => w.PROVINCE_ID == sid).ToList()
                                                 select new JObject(
                                                    new JProperty("AMPHUR_ID", a.AMPHUR_ID),
                                                    new JProperty("AMPHUR_NAME", a.AMPHUR_NAME))
                                               );
                                break;
                            #endregion
                            case "liststudent":
                                nTermSubLevel2 = int.Parse(SubLevel2);
                                rss = new JArray(from a in db.TUser.Where(w => w.SchoolID == schoolID)
                                                 where a.nTermSubLevel2 == nTermSubLevel2 && a.cDel == null
                                                 select new JObject(
                                                     new JProperty("studentid", a.sID),
                                                     new JProperty("studentname", a.sName + " " + a.sLastname)
                                                     ));
                                break;
                            case "listpermissionssetting":
                                #region listpermissionssetting
                                var q_comapny = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);
                                var qemployees_listmenu = dbmaster.TUsers.FirstOrDefault(f => f.nSystemID == sid && f.cType == "1" && f.nCompany == q_comapny.nCompany);
                                var qmenu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
                                var qgroupmenu = dbmaster.TGroupMenus.ToList();
                                var q_permission = dbmaster.permissions.Where(w => w.user_id == qemployees_listmenu.sID);
                                //rss.groupmenu = ();
                                rss.employessname = qemployees_listmenu.sName + " " + qemployees_listmenu.sLastname;
                                rss.groupmenu = new JArray(from a in qgroupmenu
                                                           select new JObject
                                               {
                                                   new JProperty("groupmenuid", a.groupmenuid),
                                                   new JProperty("groupmenu", a.groupmenu),
                                                   new JProperty("menu", (from b in qmenu
                                                                          join d in q_permission on b.MenuId equals d.menu_id into jbd
                                                                          from jd in jbd.DefaultIfEmpty()
                                                                          where b.groupmenuid == a.groupmenuid
                                                                          select new JObject
                                                                          {
                                                                              new JProperty("menuid", b.MenuId),
                                                                              new JProperty("menuname", b.MenuName),
                                                                              new JProperty("option",
                                                                                  (from c in IOpion(b.MenuMode)
                                                                                   select new JObject
                                                                                   {
                                                                                       new JProperty("text", c.text),
                                                                                       new JProperty("value", c.value),
                                                                                       new JProperty("selected", getpermission(jd) == c.value ? "selected" : "")
                                                                                   }))
                                                                          }))
                                               });
                                //rss.Data.Add(groupmenu);
                                #endregion
                                break;
                            case "listmenu":
                                #region listmenu
                                int UserID = int.Parse(HttpContext.Current.Session["sEmpID"].ToString());
                                rss = listmenu(UserID, schoolID);
                                #endregion
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        string parameters = string.Format("nTermSubLevel2:{0},nTSubLevel:{1}", SubLevel2, SubLevel);
                        SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "/App_Logic/dataJSONArray.ashx?mode=" + sMode, parameters, "", null);
                        rss = new JObject();
                    }

                }
            }

            context.Response.Expires = -1;
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.ContentType = "application/json";
            //context.Response.ContentEncoding = Encoding.UTF8;
            context.Response.Write(rss);
            context.Response.Flush(); // Sends all currently buffered output to the client.
            context.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            context.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline**
        }

        private dynamic listmenu(int UserID, int schoolID)
        {
            dynamic rss = new JObject();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    var qemployees_listmenu = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == UserID);
                    var qmenu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
                    var qgroupmenu = dbmaster.TGroupMenus.ToList();
                    bool menudemo = bool.Parse(ConfigurationManager.AppSettings["demo"].ToString());
                    if (menudemo == false) qgroupmenu = qgroupmenu.Where(w => w.actvice == true).ToList();
                    rss.employessname = qemployees_listmenu.sName + " " + qemployees_listmenu.sLastname;
                    rss.groupmenu = new JArray(from a in qgroupmenu
                                               select new JObject(
                                                   new JProperty("groupmenuid", a.groupmenuid),
                                                   new JProperty("groupmenu_name", a.groupmenu),
                                                   new JProperty("groupmenu_class", a.@class),
                                                   new JProperty("groupmenu_title", a.title),
                                                   new JProperty("groupmenu_permissions", getpermissions(a.groupmenuid, qmenu, qemployees_listmenu.sClaim)),
                                                   new JProperty("submenu", qmenu.Where(w => w.groupmenuid == a.groupmenuid).Count() == 1 ? false : true),
                                                   new JProperty("groupmenu_url", groupmenu_url(qmenu, a.groupmenuid)),
                                                   new JProperty("menu", menu(qmenu, a.groupmenuid, qemployees_listmenu.sClaim, qcompany))
                                                   ));
                }
            }
            return rss;
        }

        private dynamic listmenu(int UserID, string sEntities)
        {
            dynamic rss = new JObject();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var qcompany = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == sEntities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
                {
                    var qemployees_listmenu = dbschool.TEmployees.FirstOrDefault(f => f.sEmp == UserID);
                    var qmenu = dbmaster.TMenus.Where(w => w.showmenu == true).ToList();
                    var qgroupmenu = dbmaster.TGroupMenus.ToList();
                    bool menudemo = bool.Parse(ConfigurationManager.AppSettings["demo"].ToString());
                    if (menudemo == false) qgroupmenu = qgroupmenu.Where(w => w.actvice == true).ToList();
                    rss.employessname = qemployees_listmenu.sName + " " + qemployees_listmenu.sLastname;
                    rss.groupmenu = new JArray(from a in qgroupmenu
                                               select new JObject(
                                                   new JProperty("groupmenuid", a.groupmenuid),
                                                   new JProperty("groupmenu_name", a.groupmenu),
                                                   new JProperty("groupmenu_class", a.@class),
                                                   new JProperty("groupmenu_title", a.title),
                                                   new JProperty("groupmenu_permissions", getpermissions(a.groupmenuid, qmenu, qemployees_listmenu.sClaim)),
                                                   new JProperty("submenu", qmenu.Where(w => w.groupmenuid == a.groupmenuid).Count() == 1 ? false : true),
                                                   new JProperty("groupmenu_url", groupmenu_url(qmenu, a.groupmenuid)),
                                                   new JProperty("menu", menu(qmenu, a.groupmenuid, qemployees_listmenu.sClaim, qcompany))
                                                   ));
                }
            }
            return rss;
        }

        private string groupmenu_url(List<TMenu> qmenu, int groupmenuid)
        {
            bool menudemo = bool.Parse(ConfigurationManager.AppSettings["demo"].ToString());
            if (qmenu.Where(w => w.groupmenuid == groupmenuid).Count() == 1)
            {
                if (qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().demo == false)
                {
                    return qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().url;
                }
                else
                {
                    if (menudemo) return qmenu.Where(w => w.groupmenuid == groupmenuid).FirstOrDefault().url;
                    else return "#";
                }
            }
            else
            {
                return "#";
            }
        }

        private JArray menu(List<TMenu> qmenu, int groupmenuid, string sClaim, TCompany qcompany)
        {
            bool menudemo = bool.Parse(ConfigurationManager.AppSettings["demo"].ToString());
            dynamic rss = new JArray();
            if (qmenu.Where(w => w.groupmenuid == groupmenuid).Count() > 1)
            {
                if (menudemo == false)
                {
                    rss = new JArray(from b in qmenu
                                     where b.groupmenuid == groupmenuid
                                     && getpermission(sClaim, b.MenuIndex.Value) != "2" && b.demo == false
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", b.demo == false ?  b.url:(menudemo == true ? b.url : "#")),
                                     new JProperty("menu_title", b.title),
                                     new JProperty("target",target(b.target))
                                 });
                }
                else
                {
                    rss = new JArray(from b in qmenu
                                     where b.groupmenuid == groupmenuid
                                     && getpermission(sClaim, b.MenuIndex.Value) != "2"
                                     orderby b.nMenuOrder
                                     select new JObject {
                                     new JProperty("menuid", b.MenuId),
                                     new JProperty("menu_name", (qcompany.sotfware == true && b.MenuName == "รายงานการมาโรงเรียน") ?"รายงานกิจกรรมหน้าเสาธง":b.MenuName),
                                     new JProperty("menu_class", b.@class),
                                     new JProperty("menu_url", b.url),
                                     new JProperty("menu_title", b.title),
                                     new JProperty("target",target(b.target))
                                 });
                }
            }
            return rss;
        }

        private string target(int? target_id)
        {
            switch (target_id)
            {
                case 0: return "_self";
                case 1: return "_blank";
                case 2: return "_top";
                case 3: return "_parent";
                default: return "";
            }
        }

        private bool getpermissions(int groupmenu_id, List<TMenu> qmenu, string user_permissions)
        {
            bool menudemo = bool.Parse(ConfigurationManager.AppSettings["demo"].ToString());
            bool permissions = false;
            foreach (var menu_data in qmenu.Where(w => w.groupmenuid == groupmenu_id))
            {
                if (permissions == false)
                {
                    if (user_permissions.Length > menu_data.MenuIndex.Value)
                        permissions = user_permissions.Substring(menu_data.MenuIndex.Value, 1) != "2";
                    else
                        permissions = menudemo == true;
                }
            }
            return permissions;
        }

        private List<option> IOpion(string menuname)
        {
            List<option> _option = new List<option>();
            if (menuname != "reports") _option.Add(new option { text = "เพิ่ม/ลบ/แก้ไข", value = "0" });
            if (menuname != "permission") _option.Add(new option { text = "ดู", value = "1" });
            _option.Add(new option { text = "ไม่มีสิทธิ์", value = "2" });

            return _option;
        }

        private JArray GetSchedule(HttpContext context, JabJaiEntities dbschool)
        {
            dynamic rss = new JObject();
            TimeSpan tStart = TimeSpan.Parse(context.Request["tStart"]);
            TimeSpan tEnd = TimeSpan.Parse(context.Request["tEnd"]);
            int SubLevel2Id = int.Parse(context.Request["id"]);
            string TremId = context.Request["idterm"];
            int scheduleid = int.Parse(context.Request["scheduleid"]);
            int nPlaneDay = int.Parse(context.Request["nPlaneDay"]);
            int sPlaneID = int.Parse(context.Request["sPlaneID"]);

            var tTermTimeTable = dbschool.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == SubLevel2Id && w.nTerm == TremId).FirstOrDefault();
            if (scheduleid > 0) return new JArray();
            if (tTermTimeTable == null) return new JArray();

            var tSchedule = (from a in dbschool.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
                             join p in dbschool.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.sPlaneID equals p.sPlaneID
                             join b in dbschool.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on a.nTermTable equals b.nTermTable
                             where ((a.tStart < tStart && a.tEnd > tStart) || (a.tStart < tEnd && a.tEnd > tEnd))
                             && a.nPlaneDay == nPlaneDay && b.nTermSubLevel2 == SubLevel2Id && b.nTerm == TremId
                             && a.sPlaneID == sPlaneID
                             select a).ToList();

            if (tSchedule != null && tSchedule.Count > 0)
            {
                if (tSchedule.Where(w => w.tStart == TimeSpan.Parse("00:00:00.0000000")).Count() > 0)
                {
                    tSchedule = tSchedule.Where(w => w.tStart != TimeSpan.Parse("00:00:00.0000000")).ToList();
                }
            }
            rss = new JArray(from a in tSchedule
                             select new JObject(
                                 new JProperty("sPlaneID", a.sPlaneID),
                                 new JProperty("tStart", a.tStart),
                                 new JProperty("tEnd", a.tEnd)
                                 ));

            return rss;
        }

        private string getpermission(string permission, int index)
        {
            if (string.IsNullOrEmpty(permission)) return "2";
            else if (string.IsNullOrEmpty(permission)) return "2";
            else if (permission.Length - 1 >= index) return permission.Substring(index, 1);
            else return "2";
        }

        private string getpermission(permission permission)
        {
            if (permission == null) return "2";
            else return permission.actvice.ToString();
        }

        private string GetDayName(int nPlaneDay)
        {
            switch (nPlaneDay)
            {
                case 0: return "su";
                case 1: return "mo";
                case 2: return "tu";
                case 3: return "we";
                case 4: return "th";
                case 5: return "fr";
                case 6: return "sa";
                default: return "";
            }
        }

        private string GetTermId(List<TTerm> lTerm, ref string TermId)
        {
            foreach (var data in lTerm.Where(w => DateTime.Today >= w.dStart && DateTime.Today <= w.dEnd))
            {
                TermId = data.nTerm;
            }
            return TermId;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        class option
        {
            public string text { get; set; }
            public string value { get; set; }
        }
    }
}