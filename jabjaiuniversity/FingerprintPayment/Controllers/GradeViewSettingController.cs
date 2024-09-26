using FingerprintPayment.Helper;
using JabjaiMainClass;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/GradeViewSettings")]
    public class GradeViewSettingController : ApiController
    {
        [Route("GetGradeViewSettingDetails")]
        public IHttpActionResult GetGradeViewSettingDetails()
        {
            int schoolId = Utils.GetSchoolId();

            var columns = new[] {
                                    new { data = "RowNumber", className = "RowNumber text-center", visible=true},
                                    new { data = "GradeViewSettingId", className = "hidden", visible=false},
                                    new { data = "nTerm", className = "hidden",visible=false},
                                    new { data = "nYear", className = "hidden",visible=false},
                                    new { data = "numberYear", className = "NumberYear text-center", visible=true },
                                    new { data = "sTerm", className = "sTerm text-center", visible=true },
                                    new { data = "IsMidTermApproved", className = "IsMidTermApproved text-center", visible=true },
                                    new { data = "IsFinalTermApproved", className = "IsFinalTermApproved text-center", visible=true },
                                    new { data = "IsTermApproved", className = "IsTermApproved text-center", visible=true },
                                 }.ToList();

            var gradeViewSettingDTOs = GradeViewSettingService.GetGradeViewSettingDetails(schoolId);
            var getGradeViewCommonSetting = GradeViewSettingService.GetGradeViewFor100(schoolId);
            return Json(new
            {
                column = columns.ToArray(),
                data = (gradeViewSettingDTOs != null) ? gradeViewSettingDTOs.ToArray() : null,
                GradeViewAutoBlock = getGradeViewCommonSetting.GradeViewAutoBlock
            });
        }

        [Route("GetRoomListForGradeViewBlock")]
        public IHttpActionResult GetRoomListForGradeViewBlock(int gradeViewSettingId, int NTSubLevel)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var columns = new[] {
                                    new { data = "RowNumber", className = "RowNumber text-center", visible=true}, //0
                                    new { data = "GradeViewSettingId", className = "hidden", visible=false}, //1
                                    new { data = "RoomListSettingId", className = "hidden",visible=false}, //2
                                    new { data = "NTermSubLevel2", className = "hidden", visible=false }, //3
                                    new { data = "IsRoomBlocked", className = "hidden",visible=false}, //4
                                    new { data = "RoomFullName", className = "RoomFullName",visible=true}, //5
                                   
                                 }.ToList();

            var gradeViewRoomListSettingDTOs = GradeViewSettingService.GetGradeViewRoomListSettingDetails(userData.CompanyID, gradeViewSettingId, NTSubLevel);

            

            return Json(new
            {
                column = columns.ToArray(),
                data = (gradeViewRoomListSettingDTOs != null) ? gradeViewRoomListSettingDTOs.ToArray() : null,
               
            });
        }

        [Route("GetGradeViewFor100")]
        public IHttpActionResult GetGradeViewFor100()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            return Json(GradeViewSettingService.GetGradeViewFor100(userData.CompanyID));
        }


        [Route("GetStudentsForGradeViewBlock")]
        public IHttpActionResult GetStudentsForGradeViewBlock(int gradeViewSettingId, int NTSubLevel, int nTermSubLevel2, string nTerm)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            var columns = new[] {
                                    new { data = "RowNumber", className = "RowNumber text-center", visible=true}, //0
                                    new { data = "GradeViewSettingId", className = "hidden", visible=false}, //1
                                    new { data = "StudentBlockListSettingId", className = "hidden",visible=false}, //2
                                    new { data = "IsStudentBlocked", className = "hidden", visible=false }, //3
                                    new { data = "SId", className = "hidden",visible=false}, //4
                                    new { data = "SStudentId", className = "SStudentId",visible=true}, //5
                                    new { data = "FullName", className = "FullName",visible=true}, //6
                                 }.ToList();

            var gradeViewStudentBlockSettingDTOs = GradeViewSettingService.GetStudentsForGradeViewBlock(userData.CompanyID, gradeViewSettingId, NTSubLevel, nTermSubLevel2, nTerm);

            return Json(new
            {
                column = columns.ToArray(),
                data = (gradeViewStudentBlockSettingDTOs != null) ? gradeViewStudentBlockSettingDTOs.ToArray() : null,
            });
        }

        [Route("UpdateFinalTermApprovedStatus")]
        public IHttpActionResult UpdateFinalTermApprovedStatus(GradeViewSettingRequest gradeViewSettingRequest)
        {
            GradeViewSettingService.UpdateFinalTermApprovedStatus(gradeViewSettingRequest);

            return Json(new {
                Status = true,
                ErrorMessage = "",
            });
        }

        [Route("UpdateMidTermApprovedStatus")]
        public IHttpActionResult UpdateMidTermApprovedStatus(GradeViewSettingRequest gradeViewSettingRequest)
        {
            GradeViewSettingService.UpdateMidTermApprovedStatus(gradeViewSettingRequest);

            return Json(new
            {
                Status = true,
                ErrorMessage = "",
            });
        }

        [HttpPost]
        [Route("UpdateGradeViewFor100")]
        public IHttpActionResult UpdateGradeViewFor100(GradeViewSettingRequest gradeViewSettingRequest)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            GradeViewSettingService.UpdateGradeViewFor100(gradeViewSettingRequest.GradeViewFor100, userData.CompanyID);
            return Json("");
        }

        [HttpPost]
        [Route("UpdateGradeViewAutoBlock")]
        public IHttpActionResult UpdateGradeViewAutoBlock(GradeViewSettingRequest gradeViewSettingRequest)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            GradeViewSettingService.UpdateGradeViewAutoBlock(gradeViewSettingRequest.GradeViewAutoBlock, userData.CompanyID);
            return Json("");
        }

        [HttpPost]
        [Route("GradeViewRoomListBlock")]
        [SessionTimeout]
        public IHttpActionResult GradeViewRoomListBlock([FromBody] List<GradeViewRoomListBlockRequest> requests)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                if (requests != null)
                {
                    var dateTime = DateTime.Now;
                    requests.ForEach(f => { f.SchoolId = userData.CompanyID; f.CreatedBy = userData.UserID;
                        f.UpdatedBy = userData.UserID; f.UpdatedDate = dateTime; f.CreatedDate = dateTime;
                    });

                    GradeViewSettingService.GradeViewRoomListBlock(requests);

                    return Json("Success");
                }
                return Json("");
            }
            catch (Exception ex)
            {
                

                throw;
            }
        }

        [HttpPost]
        [Route("GradeViewStudentBlock")]
        [SessionTimeout]
        public IHttpActionResult GradeViewStudentBlock([FromBody] List<GradeViewStudentBlockRequest> requests)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {
                if (requests != null)
                {
                    var dateTime = DateTime.Now;
                    requests.ForEach(f => {
                        f.SchoolId = userData.CompanyID; f.CreatedBy = userData.UserID;
                        f.UpdatedBy = userData.UserID; f.UpdatedDate = dateTime; f.CreatedDate = dateTime;
                    });

                    GradeViewSettingService.GradeViewStudentBlock(requests);

                    return Json("Success");
                }
                return Json("");
            }
            catch (Exception ex)
            {


                throw;
            }
        }
    }
}
