using FingerprintPayment.Helper;
using JabjaiMainClass;
using Ninject;
using SchoolBright.Business.Interfaces;
using System;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/Plan")]
    public class PlanController : ApiController
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        [Inject]
        public ICommonService CommonService { get; set; }

        [HttpGet]
        [ActionName("GetPlan")]
        [SessionTimeout]
        public IHttpActionResult GetPlanClassInformation(int nYear, int nTSublevel, string levelName)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var planClass = PlanService.GetPlanNameByYear(nYear, userData.CompanyID, nTSublevel, levelName);
                return Json(planClass);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        

        [HttpGet]
        [ActionName("GetSubjectByPlanId")]
        [SessionTimeout]
        public IHttpActionResult GetFirstRoomSubjectsByPlanId(int planId)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var subjects = PlanService.GetSubjectsByPlanId(planId, userData.CompanyID);
                return Json(subjects);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetPlanCourseDetails")]
        [SessionTimeout]
        public IHttpActionResult GetPlanCourseDetails(int planCoureId)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var planCourse = PlanService.GetPlanCourseDetails(planCoureId, userData.CompanyID);
                return Json(planCourse);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetSubjectsByTeacherAndRoom")]
        [SessionTimeout]
        public IHttpActionResult GetSubjectsByTeacherAndRoom(int nYear, string nTerm, int nTSublevel, int nTermSubLevel2, int sEmpId)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                return Json(ServiceHelper.GetSubjectsByTeacherAndRoom(nYear, nTerm, nTSublevel, nTermSubLevel2, sEmpId, userData.CompanyID));
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        [HttpGet]
        [ActionName("GetTeachersFromPlan")]
        [SessionTimeout]
        public IHttpActionResult GetTeachersFromPlan(int nYear, string nTerm, int nTSublevel, int nTermSubLevel2)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                
                return Json(ServiceHelper.GetTeachersFromPlan(nYear, nTerm, nTSublevel, nTermSubLevel2, userData.CompanyID));
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        //[HttpGet]
        //[ActionName("GetTermsByPlanCourseId")]
        //[SessionTimeout]
        //public IHttpActionResult GetTermsByPlanCourseId(int planCourseId)
        //{
        //    try
        //    {
        //        JWTToken token = new JWTToken();
        //        var userData = new JWTToken().UserData;
        //        if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //        var planClass = PlanService.GetTermsByPlanCourseId(planCourseId, userData.CompanyID);
        //        return Json(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        //TO DO: Capture and Throw Custom Exception
        //        return null;
        //    }
        //}
    }
}