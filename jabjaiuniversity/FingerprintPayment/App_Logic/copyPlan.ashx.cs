using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
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
    /// Summary description for copyPlan
    /// </summary>
    public class copyPlan : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                string returnMessage = string.Empty;

                dynamic rss = new JObject();
                string id = fcommon.ReplaceInjection(context.Request["id"]);
                string termtxtCopyFrom = fcommon.ReplaceInjection(context.Request["termtxt"]);
                string yeartxtCopyFrom = fcommon.ReplaceInjection(context.Request["yeartxt"]);
                string roomtxtCopyFrom = fcommon.ReplaceInjection(context.Request["roomtxt"]);

                //Copy To
                string nTermDestination = fcommon.ReplaceInjection(context.Request["idterm"]);
                int nTermSubLevel2Destination = int.Parse(id);

                try
                {
                    var yearDestination = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == nTermDestination).Select(s => s.nYear).FirstOrDefault();
                    var subLevelDestination = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTermSubLevel2 == nTermSubLevel2Destination).Select(s => s.nTSubLevel).FirstOrDefault();
                    var planCourseDTOsDestination = ServiceHelper.GetPlanCourses(subLevelDestination, nTermSubLevel2Destination, nTermDestination, (yearDestination != null) ? (int)yearDestination : 0, Utils.GetSchoolId(), _db);

                    planCourseDTOsDestination.ToList().ForEach(f => f.PlanCourseTeacherDTOs = (_db.TPlanCourseTeachers.Where(w => w.SchoolID == userData.CompanyID && w.PlanCourseId == f.PlanCourseId && w.IsActive == true && w.cDel == false).Select(s => new SchoolBright.DTO.DTO.PlanCourseTeacherDTO { SEmp = s.sEmp }).ToList()));

                    var planCourseIdDestination = (planCourseDTOsDestination != null) ? planCourseDTOsDestination.Select(s => s.SPlaneId).Distinct().ToList() : new List<int>();

                    //Copy From
                    string ntermCopyFrom = "";
                    string usertermCopyFrom = termtxtCopyFrom;
                    string yearCopyFrom = yeartxtCopyFrom;
                    string roomCopyFrom = roomtxtCopyFrom;
                    int idlv2CopyFrom = int.Parse(roomCopyFrom);
                    int? useryearCopyFrom = int.Parse(yearCopyFrom);
                    int nyearCopyFrom = 0;

                    foreach (var ff in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.numberYear == useryearCopyFrom && w.cDel == false))
                    {
                        nyearCopyFrom = ff.nYear;
                    }

                    foreach (var ee in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.sTerm == usertermCopyFrom && w.nYear == nyearCopyFrom))
                    {
                        ntermCopyFrom = ee.nTerm;
                    }

                    var termTimeTablesCopyFrom = _db.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == idlv2CopyFrom && w.nTerm == ntermCopyFrom).FirstOrDefault();

                    //Create TermTimeTables
                    if (termTimeTablesCopyFrom != null)
                    {
                        var plancopyList = (from s in _db.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.nTermTable == termTimeTablesCopyFrom.nTermTable && w.cDel == null)
                                            join p in _db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on s.sPlaneID equals p.sPlaneID
                                            select s).Distinct().ToList();

                        var plancopyListCourseId = (plancopyList != null) ? plancopyList.Select(s => s.sPlaneID ?? 0).Distinct().ToList() : null;
                        var courseNotExistinDestinationPlanCourses = plancopyListCourseId.Except(planCourseIdDestination);

                        if (courseNotExistinDestinationPlanCourses != null && courseNotExistinDestinationPlanCourses.Count() > 0)
                        {
                            var subjectNotExistInDestination = from p in _db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null && courseNotExistinDestinationPlanCourses.Contains(w.sPlaneID))
                                                               select new { p.courseCode, p.sPlaneName };

                            returnMessage = "การพยายามคัดลอกหลักสูตรของคุณไม่มีอยู่ในแผนห้องพักปัจจุบัน กรุณาตรวจสอบแผน";
                            var subjectMessage = string.Empty;
                            if (subjectNotExistInDestination != null)
                            {
                                foreach (var p in subjectNotExistInDestination)
                                {
                                    returnMessage += string.Format("</br>{0}-{1}", p.courseCode, p.sPlaneName);
                                }
                            }
                        }
                        else if (plancopyList != null && plancopyList.Count > 0)
                        {

                            var nTermTable = 0;
                            var checktable = _db.TTermTimeTables.Where(w => w.nTermSubLevel2 == nTermSubLevel2Destination && w.nTerm == nTermDestination).FirstOrDefault();

                            if (checktable == null)
                            {
                                var tTermTimeTable = new TTermTimeTable
                                {
                                    nTeacher = termTimeTablesCopyFrom.nTeacher,
                                    nTerm = nTermDestination,
                                    nTermSubLevel2 = nTermSubLevel2Destination,
                                    SchoolID = userData.CompanyID
                                };
                                _db.TTermTimeTables.Add(tTermTimeTable);
                                _db.SaveChanges();
                                nTermTable = tTermTimeTable.nTermTable;

                            }
                            else nTermTable = checktable.nTermTable;

                            var destinationTSchedule = _db.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.nTermTable == nTermTable && w.cDel == null).ToList();

                            //int countSchedule = _db.TSchedules.Select(s => s.sScheduleID).DefaultIfEmpty(0).Max() + 1;
                            foreach (var scheduleFrom in _db.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.nTermTable == termTimeTablesCopyFrom.nTermTable && w.cDel == null))
                            {
                                var newplanid = destinationTSchedule.Where(w => w.sPlaneID == scheduleFrom.sPlaneID && w.nPlaneDay == scheduleFrom.nPlaneDay && w.tStart == scheduleFrom.tStart && w.tEnd == scheduleFrom.tEnd).FirstOrDefault();
                                if (newplanid == null && scheduleFrom.sPlaneID != 0)
                                {

                                    _db.TSchedules.Add(new TSchedule
                                    {
                                        cActive = scheduleFrom.cActive,
                                        calculate = scheduleFrom.calculate,
                                        //sScheduleID = countSchedule,
                                        sPlaneID = scheduleFrom.sPlaneID,
                                        nTermTable = nTermTable,
                                        cDel = scheduleFrom.cDel,
                                        dTimeEnd_IN = scheduleFrom.dTimeEnd_IN,
                                        dTimeEnd_OUT = scheduleFrom.dTimeEnd_OUT,
                                        dTimeHalf = scheduleFrom.dTimeHalf,
                                        dTimeStart_IN = scheduleFrom.dTimeStart_IN,
                                        dTimeStart_OUT = scheduleFrom.dTimeStart_OUT,
                                        nPlaneDay = scheduleFrom.nPlaneDay,
                                        nTimeLate = scheduleFrom.nTimeLate,
                                        sClassID = scheduleFrom.sClassID,
                                        sEmp = (planCourseDTOsDestination.Where(p => p.SPlaneId == scheduleFrom.sPlaneID && p.PlanCourseTeacherDTOs.Where(t => t.SEmp == scheduleFrom.sEmp).Count() > 0).FirstOrDefault() != null) ? scheduleFrom.sEmp : null,
                                        tEnd = scheduleFrom.tEnd,
                                        tStart = scheduleFrom.tStart,
                                        SchoolID = userData.CompanyID
                                    });
                                    //countSchedule = countSchedule + 1;
                                }
                            }
                            _db.SaveChanges();

                            returnMessage = string.Empty;
                        }
                        else
                        {
                            returnMessage = "ไม่มีกำหนดเวลาในการคัดลอก กรุณาเลือกห้องอื่น";
                        }


                    }
                    else
                    {
                        returnMessage = "ความพยายามคัดลอกชั้นเรียนของคุณไม่มีตารางเวลา กรุณาเลือกชั้นเรียนอื่น";
                    }
                }
                catch (Exception ex)
                {
                    string parameters = string.Format("id:{0},termtxtCopyFrom:{1},yeartxtCopyFrom:{2},roomtxtCopyFrom:{3}", id, termtxtCopyFrom, yeartxtCopyFrom, roomtxtCopyFrom);
                    SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "copyPlan.ashx", parameters, "", null);
                }

                context.Response.Expires = -1;
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "text/plain";
                context.Response.Write(returnMessage);
                context.Response.End();
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //protected class planid
        //{
        //    public string oldId { get; set; }
        //    public string newId { get; set; }
        //}
    }


}