using FingerprintPayment.Class;
using FingerprintPayment.Helper;
using JabjaiMainClass;
using Ninject;
using OfficeOpenXml;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/TimeTable")]
    public class TimeTableController : ApiController
    {
        [Inject]
        public ITimeTableSettingService TimeTableSettingService { get; set; }

        [HttpGet]
        [ActionName("CheckTimeTableExist")]
        [SessionTimeout]
        public IHttpActionResult CheckTimeTableExist([FromUri] int nTermSubLevel2, string nTerm)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                var isTimeTableExist = TimeTableSettingService.CheckTimeTableExist(nTermSubLevel2, nTerm, userData.CompanyID);
                if (isTimeTableExist)
                {
                    return Json(0);
                }
                else
                {
                    return Json(userData.CompanyID);
                }
                
            }
            catch (Exception ex)
            {
                return Json(true);
            }
        }


        //[HttpPost]
        //[ActionName("ValidateImportTimeTableDataFromPdf")]
        //public IHttpActionResult ValidateImportTimeTableDataFromPdf([FromBody] ImportTimeTableRequest request)
        //{

        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    List<ScheduleDTO> classSchedule = new List<ScheduleDTO>();
        //    List<ScheduleDTO> timetable = new List<ScheduleDTO>();
        //    var planCourse = ServiceHelper.GetPlanCoursesWithTeachers(0, request.nTermSubLevel2, request.nTerm, 0, userData.CompanyID, null);
        //    //Call pdf logic and convert to ScheduleDTO
        //    timetable = ServiceHelper.CheckTimeTableImportDataFromPdf(request.TimeTable, planCourse, userData.CompanyID);


        //    var inValidData = (timetable != null) ? timetable.Where(w => w.IsValidData == "โมฆะ").ToList() : null;

        //    if (inValidData != null && inValidData.Count > 0)
        //    {
        //        return Json(inValidData);
        //    }
        //    else
        //    {
        //        return Json(string.Empty);
        //        //Save to DB
        //        //var dateTime = DateTime.Now;
        //        //var nTermTable = TimeTableSettingService.AddOrUpdateTermTimeTable(new CommonRequest
        //        //{
        //        //    nTerm = nTerm,
        //        //    nTermSubLevel2 = nTermSubLevel2,
        //        //    SchoolId = userData.CompanyID,
        //        //    UpdatedBy = userData.UserID,
        //        //    UpdatedDate = DateTime.Now
        //        //});

        //        //if (nTermTable > 0)
        //        //{
        //        //    timetable.ForEach(f => { f.nTermTable = nTermTable; f.UpdatedBy = userData.UserID; f.UpdatedDate = dateTime; f.CreatedBy = userData.UserID; f.CreatedDate = dateTime; });
        //        //    TimeTableSettingService.AddOrUpdateSchedulesForImport(timetable);
        //        //    return Json("");
        //        //}
        //    }
        //    //return Json("Error");

        //}

        [HttpPost]
        [ActionName("ImportTimeTableFromPdf")]
        public IHttpActionResult ImportTimeTableFromPdf([FromBody] ImportTimeTableRequest request)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
           
            List<ScheduleDTO> classSchedule = new List<ScheduleDTO>();
            List<ScheduleDTO> timetable = new List<ScheduleDTO>();
            var planCourse = ServiceHelper.GetPlanCoursesWithTeachers(0, request.nTermSubLevel2, request.nTerm, 0, userData.CompanyID, null);
            //Call pdf logic and convert to ScheduleDTO
            timetable = ServiceHelper.CheckTimeTableImportDataFromPdf(request.TimeTable, planCourse, userData.CompanyID);


            var inValidData = (timetable != null) ? timetable.Where(w => w.IsValidData == "โมฆะ").ToList() : null;

            if (inValidData != null && inValidData.Count > 0)
            {
                return Json(inValidData);
            }
            else
            {
                //Save to DB
                var dateTime = DateTime.Now;
                var nTermTable = TimeTableSettingService.AddOrUpdateTermTimeTable(new CommonRequest
                {
                    nTerm = request.nTerm,
                    nTermSubLevel2 = request.nTermSubLevel2,
                    SchoolId = userData.CompanyID,
                    UpdatedBy = userData.UserID,
                    UpdatedDate = DateTime.Now
                });

                if (nTermTable > 0)
                {
                    timetable.ForEach(f => { f.nTermTable = nTermTable; f.UpdatedBy = userData.UserID; f.UpdatedDate = dateTime; f.CreatedBy = userData.UserID; f.CreatedDate = dateTime; });
                    TimeTableSettingService.AddOrUpdateSchedulesForImport(timetable);
                    return Json("");
                }
            }
            return Json("Error");

        }


        [HttpPost]
        [ActionName("ImportTimeTable")]
        public IHttpActionResult ImportTimeTable(int nTermSubLevel2, string nTerm)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            // Checking no of files injected in Request object  
            var req = Request;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string extension = System.IO.Path.GetExtension(postedFile.FileName);
                        List<ScheduleDTO> classSchedule = new List<ScheduleDTO>();
                        List<ScheduleDTO> timetable = new List<ScheduleDTO>();
                        var planCourse = ServiceHelper.GetPlanCourses(nTermSubLevel2, nTerm, userData.CompanyID);
                        //if (extension == ".pdf")
                        //{
                        //    //Call pdf logic and convert to ScheduleDTO
                        //    timetable = ServiceHelper.CheckTimeTableImportDataFromPdf(classSchedule, planCourse, userData.CompanyID);
                        //}
                        //else
                        //{
                        byte[] fileBytes = new byte[postedFile.ContentLength];
                            var data = postedFile.InputStream.Read(fileBytes, 0, Convert.ToInt32(postedFile.ContentLength));
                            using (var xlPackage = new ExcelPackage(postedFile.InputStream))
                            {
                                var ds_package = xlPackage.ToDataSet(1);
                                ds_package.Tables[0].Columns["วัน"].ColumnName = "Day";
                                ds_package.Tables[0].Columns["รหัสวิชา"].ColumnName = "CourseCode";
                                ds_package.Tables[0].Columns["เริ่มเวลา"].ColumnName = "time_start";
                                ds_package.Tables[0].Columns["หมดเวลา"].ColumnName = "time_end";
                                ds_package.Tables[0].Columns["รหัสผู้ใช้งาน(ครูผู้สอน)"].ColumnName = "username";

                                classSchedule = ds_package.Tables[0].DataTableToList<ScheduleDTO>().Distinct().Where(w => !string.IsNullOrEmpty(w.CourseCode)).ToList();
                                timetable = ServiceHelper.CheckTimeTableImportData(classSchedule, planCourse, userData.CompanyID);
                            }
                        //}
                            

                        var inValidData = (timetable != null)? timetable.Where(w => w.IsValidData == "โมฆะ").ToList() : null;

                        if (inValidData != null && inValidData.Count > 0)
                        {
                            return Json(inValidData);
                        }
                        else
                        {
                            //Save to DB
                            var dateTime = DateTime.Now;
                            var nTermTable = TimeTableSettingService.AddOrUpdateTermTimeTable(new CommonRequest
                            {
                                nTerm = nTerm,
                                nTermSubLevel2 = nTermSubLevel2,
                                SchoolId = userData.CompanyID,
                                UpdatedBy = userData.UserID,
                                UpdatedDate = DateTime.Now
                            });

                            if (nTermTable > 0)
                            {
                                timetable.ForEach(f => { f.nTermTable = nTermTable; f.UpdatedBy = userData.UserID; f.UpdatedDate = dateTime; f.CreatedBy = userData.UserID; f.CreatedDate = dateTime; });
                                TimeTableSettingService.AddOrUpdateSchedulesForImport(timetable);
                                return Json("");
                            }
                        }
                       
                    }
                }
                else
                {
                    return Json("Error");
                }
            }
            catch (System.Exception e)
            {
                return Json("Error");
            }
            return Json("Error");

        }

        //private async Task<List<class_schedule>> readexcel_plannedata(HttpPostedFileBase fileupload, string entities, int nTermSubLevel2, string nTerm)
        //{
        //    dynamic data = new JArray();
        //    //var excel_plane = new List<excel_plane>();
        //    var class_schedule = new List<class_schedule>();
        //    if (fileupload != null)
        //    {
        //        // tdata.executecommand("truncate table othercompanyassets");  
        //        if (fileupload.ContentType == "application/vnd.ms-excel" || fileupload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        //        {
        //            string filename = fileupload.FileName;
        //            string targetpath = Server.MapPath("~/Files/uploads/");
        //            fileupload.SaveAs(targetpath + filename);
        //            string pathtoexcelfile = targetpath + filename;
        //            FileInfo newFile = new FileInfo(pathtoexcelfile);
        //            ExcelPackage xlPackage = new ExcelPackage(newFile);
        //            var ds_package = xlPackage.ToDataSet(1);

        //            #region Set Columns Name


        //            ds_package.Tables[0].Columns["วัน"].ColumnName = "nday";
        //            ds_package.Tables[0].Columns["รหัสวิชา"].ColumnName = "plane_id";
        //            ds_package.Tables[0].Columns["เริ่มเวลา"].ColumnName = "time_start";
        //            ds_package.Tables[0].Columns["หมดเวลา"].ColumnName = "time_end";
        //            ds_package.Tables[0].Columns["รหัสผู้ใช้งาน(ครูผู้สอน)"].ColumnName = "username";
        //            #endregion



        //            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
        //            {
        //                var q_company = dbmaster.TCompanies.FirstOrDefault(f => f.sEntities == entities);
        //                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(q_company)))
        //                {

        //                    var f_level2 = dbschool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2 && f.SchoolID == q_company.nCompany && f.nWorkingStatus == 1);
        //                    string nTSubLevel = f_level2.nTSubLevel.ToString();



        //                    var checkTimeTableHaveData = (from t in dbschool.TTermTimeTables.Where(w => w.nTermSubLevel2 == nTermSubLevel2 && w.nTerm.Trim() == nTerm && w.SchoolID == q_company.nCompany)
        //                                                  join s in dbschool.TSchedules.Where(w => w.SchoolID == q_company.nCompany && w.cDel == null) on t.nTermTable equals s.nTermTable
        //                                                  select s).FirstOrDefault();

        //                    if (checkTimeTableHaveData == null)
        //                    {
        //                        var getPlanCourses = dbschool.GetPlanCourse(f_level2.nTSubLevel, nTermSubLevel2, nTerm, 0, q_company.nCompany).ToList();
        //                        if (getPlanCourses != null)
        //                        {
        //                            //var getPlanCourseTeachers = (from c in getPlanCourses
        //                            //                             join t in dbschool.TPlanCourseTeachers.Where(w => w.SchoolID == q_company.nCompany) on c.PlanCourseId equals t.PlanCourseId
        //                            //                             where t.IsActive == true
        //                            //                             select new class_schedule {
        //                            //                                 sEmp = t.sEmp,
        //                            //                                 plane_id = c.courseCode
        //                            //                             }).ToList();
        //                            //var planCodes = (getPlanCourses != null) ? getPlanCourses.Select(s => s.courseCode).Distinct().ToList() : null;

        //                            class_schedule = ds_package.Tables[0].DataTableToList<class_schedule>().Distinct().Where(w => !string.IsNullOrEmpty(w.plane_id)).ToList();

        //                            //var class_schedule = ds_package.Tables[0].DataTableToList<class_schedule>().ToList();
        //                            class_schedule = (from a in class_schedule
        //                                              join b in dbmaster.TUsers.Where(w => w.nCompany == q_company.nCompany && w.cType == "1" && !string.IsNullOrEmpty(w.username)) on a.username equals b.username into jab
        //                                              from jb in jab.DefaultIfEmpty()
        //                                              where !string.IsNullOrEmpty(a.plane_id)
        //                                              select new class_schedule
        //                                              {
        //                                                  nday = a.nday,
        //                                                  plane_id = a.plane_id,
        //                                                  sEmp = jb?.sID,
        //                                                  teacher_name = jb == null ? "" : jb.sName + " " + jb.sLastname,
        //                                                  username = a.username,
        //                                                  time_end = string.Format("{0:00.00}", decimal.Parse(a.time_end.Replace(":", "."))),
        //                                                  time_start = string.Format("{0:00.00}", decimal.Parse(a.time_start.Replace(":", "."))),
        //                                                  IsValidData = (getPlanCourses != null && getPlanCourses.Where(c => c.courseCode == a.plane_id).Count() > 0 && !string.IsNullOrEmpty(a.nday) && (a.nday == "จันทร์" || a.nday == "อังคาร" || a.nday == "พุธ" || a.nday == "พฤหัสบดี" || a.nday == "ศุกร์" || a.nday == "เสาร์" || a.nday == "อาทิตย์")) ? "ถูกต้อง" : "โมฆะ",
        //                                              }).ToList();

        //                        }
        //                    }
        //                }
        //            }

        //            if (!System.IO.File.Exists(pathtoexcelfile))
        //            {
        //                System.IO.File.Delete(pathtoexcelfile);
        //            }
        //            Session["class_schedule"] = class_schedule;
        //            return class_schedule;
        //        }
        //        else
        //        {
        //            Session["class_schedule"] = class_schedule;
        //            return class_schedule;
        //        }
        //    }
        //    else
        //    {
        //        Session["class_schedule"] = class_schedule;
        //        return class_schedule;
        //    }
        //}
    }
}
