using FingerprintPayment.Helper;
using JabjaiMainClass;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using System.Linq;
using JabjaiMainClass.Autocompletes;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        [Inject]
        public ICommonService CommonService { get; set; }

        [HttpGet]
        [ActionName("GetStudyStudentsForSpecificSubject")]
        [SessionTimeout]
        public IHttpActionResult GetStudyStudentsForSpecificSubject(int year, string term, int idlv, int planCourseId, int sPlaneId)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var students = ServiceHelper.GetStudyStudentsForSpecificSubject(year, term, idlv, planCourseId, sPlaneId, userData.CompanyID);
                return Json(students);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("TermByYear")]
        [SessionTimeout]
        public IHttpActionResult GetTermByYear(int numberYear)
        {
            try
            {
                var tTerms = CommonService.GetTermsByNumberYear(numberYear, true, Utils.GetSchoolId(), Utils.GetUserId());
                return Json(tTerms);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetTerms")]
        [SessionTimeout]
        public IHttpActionResult GetTerms(int nYear)
        {
            try
            {
                var tTerms = CommonService.GetTerms(nYear, Utils.GetSchoolId(), Utils.GetUserId());
                return Json(tTerms);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetCompanyInfo")]
        [SessionTimeout]
        public IHttpActionResult GetCompanyInfo(int schoolId)
        {
            try
            {
                //var tTerms = CommonService.GetTerms(nYear, Utils.GetSchoolId(), Utils.GetUserId());

                return Json(ServiceHelper.GetCompanyInfo(schoolId));
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetCumulativeTermsByYear")]
        [SessionTimeout]
        public IHttpActionResult GetCumulativeTermsByYear(int numberYear)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                var tTerms = CommonService.GetCumulativeTermsByYear(numberYear, userData.CompanyID, userData.UserID);
                return Json(tTerms);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("TermByYearSeqOrder")]
        [SessionTimeout]
        public IHttpActionResult GetTermByYearSeqOrder(int numberYear)
        {
            try
            {
                var tTerms = CommonService.GetTermsByNumberYear(numberYear, false, Utils.GetSchoolId(), Utils.GetUserId());
                return Json(tTerms);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        [HttpGet]
        [ActionName("GetYears")]
        [SessionTimeout]
        public IHttpActionResult GetYears()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                var tTerms = CommonService.GetYears(userData.CompanyID, userData.UserID);
                return Json(tTerms);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetStudentInfo")]
        [SessionTimeout]
        public IHttpActionResult GetStudentInfo(string studentCode)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var studentInfo = CommonService.GetStudentInfo(studentCode, userData.CompanyID);
                return Json(studentInfo);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        [HttpGet]
        [ActionName("GetPrimaryLevelEducations")]
        [SessionTimeout]
        public IHttpActionResult GetPrimaryLevelEducations()
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                return Json(CommonService.GetPrimaryLevelEducations(userData.CompanyID, userData.UserID));
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        [HttpGet]
        [ActionName("GetClassRoomDTOs")]
        [SessionTimeout]
        public IHttpActionResult GetClassRoomDTOs(int nTSubLevel)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                return Json(CommonService.GetClassRoomDTOs(userData.CompanyID, nTSubLevel, userData.UserID));
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetClassRoomByPlanId")]
        [SessionTimeout]
        public IHttpActionResult GetClassRoomByPlanId(int nTSublevel, int planId)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                var roomInfo = CommonService.GetClassRoomDTOsByPlan(userData.CompanyID, nTSublevel, planId, userData.UserID);
                return Json(roomInfo);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        //[HttpPost]
        //[Route("configuration/refresh")]
        //public async Task<IHttpActionResult> RefreshConfiguration()
        //{

        //    RouteConfig.RefreshConfiguration();

        //    dynamic rss = new JObject();
        //    rss.status = "Configuration Refreshed";
        //    rss.data = new JObject();
        //    return Ok(rss);
        //}

        [HttpGet]
        [ActionName("GetStudyLevelInfo")]
        [SessionTimeout]
        public IHttpActionResult GetStudyLevelInfo(int numberYear, int nTSublevel, string subLevel)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                List<StudyLevelInfo> currentRange = new List<StudyLevelInfo>();

                List<StudyLevelInfo> vacationalCertificate = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "ปวช. 1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ปวช. 2", Index = 1 },
                    new StudyLevelInfo { SubLevel = "ปวช. 3", Index = 2 },
                   };

                List<StudyLevelInfo> diploma = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "ปวส. 1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ปวส. 2", Index = 1 },

                   };

                List<StudyLevelInfo> rangePrimary = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "ป.1", Index = 0, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "ป.2", Index = 1, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "ป.3", Index = 2, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "ป.4", Index = 3, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "ป.5", Index = 4, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "ป.6", Index = 5, IsPrimary = true }};

                List<StudyLevelInfo> rangePrimaryEng = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "P.1", Index = 0, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "P.2", Index = 1, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "P.3", Index = 2, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "P.4", Index = 3, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "P.5", Index = 4, IsPrimary = true },
                    new StudyLevelInfo { SubLevel = "P.6", Index = 5, IsPrimary = true } };

                List<StudyLevelInfo> rangeMathayomL1 = new List<StudyLevelInfo> { new StudyLevelInfo { SubLevel = "ม.1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ม.2", Index = 1 },
                    new StudyLevelInfo { SubLevel = "ม.3", Index = 2 },
                   };

                List<StudyLevelInfo> rangeMathayomL1Eng = new List<StudyLevelInfo> { new StudyLevelInfo { SubLevel = "M.1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "M.2", Index = 1 },
                    new StudyLevelInfo { SubLevel = "M.3", Index = 2 },
                   };

                List<StudyLevelInfo> rangeMathayomL2 = new List<StudyLevelInfo> { new StudyLevelInfo { SubLevel = "ม.4", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ม.5", Index = 1 },
                    new StudyLevelInfo { SubLevel = "ม.6", Index = 2 },
                   };

                List<StudyLevelInfo> rangeMathayomL2Eng = new List<StudyLevelInfo> { new StudyLevelInfo { SubLevel = "M.4", Index = 0 },
                    new StudyLevelInfo { SubLevel = "M.5", Index = 1 },
                    new StudyLevelInfo { SubLevel = "M.6", Index = 2 },
                   };

                if (vacationalCertificate.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = vacationalCertificate;
                }
                else if (diploma.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = diploma;
                }
                else if (rangePrimary.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangePrimary;
                }
                else if (rangePrimaryEng.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangePrimaryEng;
                }
                else if (rangeMathayomL1.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangeMathayomL1;
                }
                else if (rangeMathayomL1Eng.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangeMathayomL1Eng;
                }
                else if (rangeMathayomL2.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangeMathayomL2;
                }
                else if (rangeMathayomL2Eng.Where(w => w.SubLevel == subLevel).Count() > 0)
                {
                    currentRange = rangeMathayomL2Eng;
                }

                currentRange.Where(w => w.SubLevel == subLevel).ToList().ForEach(f => f.NumberYear = numberYear);
                var index = currentRange.FindIndex(w => w.SubLevel == subLevel);
                currentRange = currentRange.Take(index + 1).ToList();
                var currentYear = numberYear;
                foreach (var r in currentRange.OrderByDescending(o => o.Index))
                {
                    r.NumberYear = currentYear;
                    currentYear--;
                }


                var roomInfo = CommonService.GetStudyLevelInfo(numberYear, userData.CompanyID, nTSublevel, currentRange);
                return Json(roomInfo);
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        [HttpPost]
        [Route("StudentProfileUpdateToMemory")]
        public IHttpActionResult StudentProfileUpdateToMemory([FromBody] List<ProfileImage> profileImages)
        {

            if (profileImages != null)
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                ServiceHelper.StudentProfileUpdateToMemory(profileImages, userData.AuthKey, userData.AuthValue);

            }

            return Ok(new List<ProfileImage>());
        }
    }
}
