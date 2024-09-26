using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using Ninject;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/AssessmentScore")]
    public class AssessmentScoreController : ApiController
    {

        [Route("configuration/refresh")]
        public IHttpActionResult RefreshConfiguration()
        {

            Global.RefreshConfiguration();

            return Json(new { });
        }

        //// GET api/<controller>
        [Inject]
        public IGradeService GradeService { get; set; }

        [Inject]
        public ICommonService CommonService { get; set; }

        [Inject]
        public IPlanService PlanService { get; set; }

        //[HttpPost]
        //[Route("GetStudentAssessmentScore")]
        //[SessionTimeout]
        //public IHttpActionResult GetStudentAssessmentScore([FromBody]CommonRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
                    
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GetStudentAssessmentScore Called", null, string.Empty);
        //            request.SchoolId = userData.CompanyID;
        //            request.SEmp = userData.UserID;
        //            request.IsSuperAdmin = (userData.UserID == 0 && userData.IsAdmin);
        //            request.IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(request.NumberYear, null, userData.CompanyID);

        //            var yearDTO = CommonService.GetYearByYearNumber(request.NumberYear, request.SchoolId, userData.UserID);
        //            request.nYear = yearDTO.NYear;

        //            var term = CommonService.GetTerm(request.sTerm, (int)request.nYear, request.SchoolId, userData.UserID);
        //            request.nTerm = term.nTerm;
        //            var otherTerm = string.Empty;
        //            otherTerm = (request.sTerm == "1") ? "2" : ((request.sTerm == "2") ? "1" : "");
                    
        //            if (!string.IsNullOrEmpty(otherTerm))
        //            {
        //                var otherTterm = CommonService.GetTerm(otherTerm, (int)request.nYear, request.SchoolId, request.SEmp);
        //                request.OthernTerm = (otherTterm != null)?otherTterm.nTerm : string.Empty;
        //            }

        //            var gradeDTOs = GradeService.GetStudentAssessmentScore(request);
        //            if (gradeDTOs != null && gradeDTOs.AssessmentDTOs != null && gradeDTOs.StudentAssessmentScoreDTOs != null)
        //            {
        //                var AssessmentTypeHeaders = gradeDTOs.AssessmentDTOs.GroupBy(g => new { g.AssessmentTypeDescription, g.AssessmentTypeId }).Select(s => new
        //                {
        //                    AssessmentTypeName = s.Key.AssessmentTypeDescription,
        //                    AssessmentTypeId = s.Key.AssessmentTypeId,
        //                    ToTalAssessment = s.Count()
        //                });

        //                //Define the column list for DataTable
        //                var columns = new[] {
        //                                    //new { data = "sID"},
        //                                    new { name = "RowNumber", data = "RowNumber", className = "RowNumber", visible=true},
        //                                    new { name = "sID", data = "sID", className = "hidden", visible=false},
        //                                    new { name = "nGradeId", data = "nGradeId", className = "hidden",visible=false},
        //                                    new { name = "sStudentId", data = "sStudentId", className = "sStudentId", visible=true },
        //                                    new { name = "StudentName", data = "StudentName", className = "StudentName", visible=true },
        //                                }.ToList();
        //                //Object Used for Show/Hide the Grid Column  : AssessmentType = 0 is Dummy Array Object. 
        //                var toggleGroupColumns = new[] { new { AssessmentTypeName = "", Columns = new[] { 1, 2, 3 }.ToList() } }.ToList();
        //                var assessmentColumnStarting = 5;
        //                var AssessmentCount = 1;
        //                var LockedAssessmentColumns = new List<int>();

        //                decimal maxMidTermTotal = 0;
        //                decimal maxFinalTermTotal = 0;
        //                decimal maxBeforeMidTermTotal = 0;
        //                decimal maxAfterMidTermTotal = 0;

        //                foreach (var assessment in gradeDTOs.AssessmentDTOs)
        //                {
        //                    bool defaultVisible = false;
        //                    if (assessment.AssessmentTypeDescription == "Before Mid Term" && AssessmentCount <= 10)
        //                    {
        //                        defaultVisible = true;
        //                        AssessmentCount++;
        //                    }

        //                    if (assessment.AssessmentTypeDescription  == "Before Mid Term" && !string.IsNullOrEmpty(assessment.AssessmentMaxScore?.Trim()))
        //                    {
        //                        maxBeforeMidTermTotal += decimal.Parse(assessment.AssessmentMaxScore.Trim());
        //                    }
        //                    else if (assessment.AssessmentTypeDescription == "Mid Term" && !string.IsNullOrEmpty(assessment.AssessmentMaxScore?.Trim()))
        //                    {
        //                        maxMidTermTotal += decimal.Parse(assessment.AssessmentMaxScore);
        //                    }
        //                    else if (assessment.AssessmentTypeDescription == "After Mid Term" && !string.IsNullOrEmpty(assessment.AssessmentMaxScore?.Trim()))
        //                    {
        //                        maxAfterMidTermTotal += decimal.Parse(assessment.AssessmentMaxScore);
        //                    }
        //                    else if (assessment.AssessmentTypeDescription == "Final" && !string.IsNullOrEmpty(assessment.AssessmentMaxScore?.Trim()))
        //                    {
        //                        maxFinalTermTotal += decimal.Parse(assessment.AssessmentMaxScore);
        //                    }

        //                    if (assessment.IsLocked)
        //                        LockedAssessmentColumns.Add(assessmentColumnStarting);

        //                    columns.Add(new { name = assessment.AssessmentId.ToString(), data = assessment.AssessmentId.ToString(), className = "Assessment", visible = defaultVisible });
        //                    if (toggleGroupColumns.Where(c => c.AssessmentTypeName == assessment.AssessmentTypeDescription).Count() > 0)
        //                    {
        //                        toggleGroupColumns.Where(c => c.AssessmentTypeName == assessment.AssessmentTypeDescription).ToList().ForEach(f => f.Columns.Add(assessmentColumnStarting));
        //                    }
        //                    else
        //                    {
        //                        toggleGroupColumns.Add(new { AssessmentTypeName = assessment.AssessmentTypeDescription, Columns = new[] { assessmentColumnStarting }.ToList() });
        //                    }
        //                    assessmentColumnStarting++;
        //                }

        //                var IsMaxBeforeMidTermTotalIssue = false;
        //                var IsMaxAfterMidTermTotalIssue = false;
        //                var IsMaxMidTermTotalIssue = false;
        //                var IsMaxFinalTermTotalIssue = false;

        //                //For Verify Total Is Correct
        //                if (!string.IsNullOrEmpty(gradeDTOs.MaxBeforeMidTermTotal) && decimal.Parse(gradeDTOs.MaxBeforeMidTermTotal) != maxBeforeMidTermTotal)
        //                {
        //                    IsMaxBeforeMidTermTotalIssue = true;
        //                }
        //                if (!string.IsNullOrEmpty(gradeDTOs.MaxAfterMidTermTotal) && decimal.Parse(gradeDTOs.MaxAfterMidTermTotal) != maxAfterMidTermTotal)
        //                {
        //                    IsMaxAfterMidTermTotalIssue = true;
        //                }
        //                if (!string.IsNullOrEmpty(gradeDTOs.MaxMidTerm) && decimal.Parse(gradeDTOs.MaxMidTerm) != maxMidTermTotal)
        //                {
        //                    IsMaxMidTermTotalIssue = true;
        //                }
        //                if (!string.IsNullOrEmpty(gradeDTOs.MaxFinalTerm) && decimal.Parse(gradeDTOs.MaxFinalTerm) != maxFinalTermTotal)
        //                {
        //                    IsMaxFinalTermTotalIssue = true;
        //                }


        //                columns.Add(new { name = "BeforeAfterMidTermTotalScore", data = "BeforeAfterMidTermTotalScore", className = "BeforeAfterMidTermTotalScore", visible = true });
        //                columns.Add(new { name = "ScoreMidTerm", data = "ScoreMidTerm", className = "ScoreMidTerm", visible = true });
        //                columns.Add(new { name = "ScoreFinalTerm", data = "ScoreFinalTerm", className = "ScoreFinalTerm", visible = true });
        //                columns.Add(new { name = "TotalScore", data = "TotalScore", className = "TotalScore", visible = true });
                        

        //                //For Don't show Term 2,Average and Grade for Mathayom
        //                var levelName = ServiceHelper.GetClassLevel(request.nTermSubLevel2);
        //                if (!string.IsNullOrEmpty(levelName) && levelName != "ปวช." && levelName != "ปวส." && levelName != "มัธยมศึกษา")
        //                {
        //                    //Primary
        //                    if (gradeDTOs.GradeShowFullScore)
        //                    {
        //                        columns.Add(new { name = "TotalFor100Percentage", data = "TotalFor100Percentage", className = "TotalFor100Percentage", visible = true });
        //                        columns.Add(new { name = "Term2TotalFor100Percentage", data = "Term2TotalFor100Percentage", className = "Term2TotalFor100Percentage", visible = true });
        //                        columns.Add(new { name = "TotalFor50Percentage", data = "TotalFor50Percentage", className = "TotalFor100Percentage", visible = false });
        //                        columns.Add(new { name = "Term2TotalFor50Percentage", data = "Term2TotalFor50Percentage", className = "Term2TotalFor100Percentage", visible = false });
        //                    }
        //                    else
        //                    {
        //                        columns.Add(new { name = "TotalFor100Percentage", data = "TotalFor100Percentage", className = "TotalFor100Percentage", visible = false });
        //                        columns.Add(new { name = "Term2TotalFor100Percentage", data = "Term2TotalFor100Percentage", className = "Term2TotalFor100Percentage", visible = false });
        //                        columns.Add(new { name = "TotalFor50Percentage", data = "TotalFor50Percentage", className = "TotalFor100Percentage", visible = true });
        //                        columns.Add(new { name = "Term2TotalFor50Percentage", data = "Term2TotalFor50Percentage", className = "Term2TotalFor100Percentage", visible = true });
        //                    }
        //                    columns.Add(new { name = "Term1AndTerm2ScoreTotal", data = "Term1AndTerm2ScoreTotal", className = "Term1AndTerm2ScoreTotal", visible = true });
        //                    columns.Add(new { name = "Term1AndTerm2GradeLabel", data = "Term1AndTerm2GradeLabel", className = "Term1AndTerm2GradeLabel", visible = true });
        //                    columns.Add(new { name = "GetGradeLabel", data = "GetGradeLabel", className = "hidden", visible = false });
        //                }
        //                else
        //                {
        //                    if (term.sTerm == "1")
        //                    {
        //                        columns.Add(new { name= "TotalFor100Percentage", data = "TotalFor100Percentage", className = "TotalFor100Percentage", visible = true });
        //                        columns.Add(new { name = "Term2TotalFor100Percentage", data = "Term2TotalFor100Percentage", className = "hidden", visible = false });
        //                    }
        //                    else
        //                    {
        //                        columns.Add(new { name = "TotalFor100Percentage", data = "TotalFor100Percentage", className = "hidden", visible = false });
        //                        columns.Add(new { name = "Term2TotalFor100Percentage", data = "Term2TotalFor100Percentage", className = "Term2TotalFor100Percentage", visible = true });
        //                    }
        //                    columns.Add(new { name = "TotalFor50Percentage", data = "TotalFor50Percentage", className = "TotalFor100Percentage", visible = false });
        //                    columns.Add(new { name = "Term2TotalFor50Percentage", data = "Term2TotalFor50Percentage", className = "Term2TotalFor100Percentage", visible = false });

        //                    columns.Add(new { name = "Term1AndTerm2ScoreTotal", data = "Term1AndTerm2ScoreTotal", className = "hidden", visible = false });
        //                    columns.Add(new { name = "Term1AndTerm2GradeLabel", data = "Term1AndTerm2GradeLabel", className = "hidden", visible = false });
        //                    columns.Add(new { name = "GetGradeLabel", data = "GetGradeLabel", className = "GetGradeLabel", visible = true });
        //                }
                        
        //                columns.Add(new { name = "GetBehaviorLabel", data = "GetBehaviorLabel", className = "GetBehaviorLabel", visible = true });
        //                columns.Add(new { name = "GetReadWrite", data = "GetReadWrite", className = "GetReadWrite", visible = true });
        //                columns.Add(new { name = "GetSamattana", data = "GetSamattana", className = "GetSamattana", visible = true });
        //                columns.Add(new { name = "GetSpecial", data = "GetSpecial", className = "GetSpecial", visible = true });
        //                columns.Add(new { name = "GetBehaviorTotal", data = "GetBehaviorTotal", className = "GetBehaviorTotal", visible = false });
        //                columns.Add(new { name = "GetReadWriteTotal", data = "GetReadWriteTotal", className = "GetReadWriteTotal", visible = false });
        //                columns.Add(new { name = "GetSamattanaTotal", data = "GetSamattanaTotal", className = "GetSamattanaTotal", visible = false });
        //                columns.Add(new { name = "sPlaneID", data = "sPlaneID", className = "sPlaneID", visible = false });
        //                columns.Add(new { name = "nStudentStatus", data = "nStudentStatus", className = "nStudentStatus", visible = false });
        //                columns.Add(new { name = "ScoreBeforeMidTerm", data = "ScoreBeforeMidTerm", className = "hidden", visible = false });
        //                columns.Add(new { name = "ScoreAfterMidTerm", data = "ScoreAfterMidTerm", className = "hidden", visible = false });

        //                var IsGetBehaviorLabelLocked = false;
        //                var IsGetReadWriteLocked = false;
        //                var IsGetSamattanaLocked = false;
        //                var IsGetSpecialLocked = false;
        //                //if (gradeDTOs.AssessmentDTOs.Where(c => c.IsLocked == true).Count() > 0)
        //                //{
        //                //    IsGetBehaviorLabelLocked = (gradeDTOs.StudentAssessmentScoreDTOs.ToList().Where(c => !string.IsNullOrEmpty(c.GetBehaviorLabel)).Count() > 0) ? true : false;
        //                //    IsGetReadWriteLocked = (gradeDTOs.StudentAssessmentScoreDTOs.ToList().Where(c => !string.IsNullOrEmpty(c.GetReadWrite)).Count() > 0) ? true : false;
        //                //    IsGetSamattanaLocked = (gradeDTOs.StudentAssessmentScoreDTOs.ToList().Where(c => !string.IsNullOrEmpty(c.GetSamattana)).Count() > 0) ? true : false;
        //                //    IsGetSpecialLocked = (gradeDTOs.StudentAssessmentScoreDTOs.ToList().Where(c => !string.IsNullOrEmpty(c.GetSpecial) && c.GetSpecial != "-1").Count() > 0) ? true : false;
        //                //}

        //                if (gradeDTOs.AssessmentDTOs.Where(c => c.IsLocked == false && (c.AssessmentTypeDescription == "Before Mid Term" || c.AssessmentTypeDescription == "Mid Term" || c.AssessmentTypeDescription == "After Mid Term" || c.AssessmentTypeDescription == "Final")).Count() == 0)
        //                {
        //                    IsGetBehaviorLabelLocked = true;
        //                    IsGetReadWriteLocked = true;
        //                    IsGetSamattanaLocked = true;
        //                    IsGetSpecialLocked = true;
        //                }

        //                return Json(new
        //                {
        //                    recordsTotal = gradeDTOs.StudentAssessmentScoreDTOs.Count(),
        //                    recordsFiltered = 10,
        //                    data = gradeDTOs.StudentAssessmentScoreDTOs.ToArray(),
        //                    column = columns.ToArray(),
        //                    AssessmentTypeHeaders = AssessmentTypeHeaders,
        //                    Assessments = gradeDTOs.AssessmentDTOs,
        //                    ToggleGroupColumns = toggleGroupColumns,
        //                    GradeDTO = gradeDTOs,
        //                    LockedAssessmentColumns = LockedAssessmentColumns,
        //                    IsGetBehaviorLabelLocked = IsGetBehaviorLabelLocked,
        //                    IsGetReadWriteLocked = IsGetReadWriteLocked,
        //                    IsGetSamattanaLocked = IsGetSamattanaLocked,
        //                    IsGetSpecialLocked = IsGetSpecialLocked,
        //                    LevelName = levelName,
        //                    IsMaxBeforeMidTermTotalIssue = IsMaxBeforeMidTermTotalIssue,
        //                    IsMaxAfterMidTermTotalIssue = IsMaxAfterMidTermTotalIssue,
        //                    IsMaxMidTermTotalIssue = IsMaxMidTermTotalIssue,
        //                    IsMaxFinalTermTotalIssue = IsMaxFinalTermTotalIssue,
        //                    IsRequestForCurrentAcademicYear = gradeDTOs.IsRequestForCurrentAcademicYear,
        //                    IsNewAssessmentCreated = gradeDTOs.IsNewAssessmentCreated,
        //                });
        //            }
        //            return Json(new { });
        //        }
        //        return Json(new { });
        //    }
        //    catch(Exception ex)
        //    {
        //        //var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        //string requestJson = (request != null) ? json.Serialize(request) : "null";
        //        //string parameters = string.Format("request:{0}", requestJson);
        //        //SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "GetStudentAssessmentScore", parameters, "", null);

        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GetStudentAssessmentScore", ex, string.Empty);

        //        throw;
        //    }
        //}


        //[HttpPost]
        //[Route("GetRoomListForSharingAssessments")]
        //[SessionTimeout]
        //public IHttpActionResult GetRoomListForSharingAssessments([FromBody]CommonRequest request)
        //{


        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //        {
        //            if (request != null)
        //            {
        //                CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GetRoomListForSharingAssessments Called", null, string.Empty);



        //                var yearDTO = (from y in schoolDbContext.TYears
        //                               where y.numberYear == request.NumberYear && y.SchoolID == userData.CompanyID
        //                               select new YearDTO
        //                               {
        //                                   NumberYear = y.numberYear,
        //                                   NYear = y.nYear,
        //                                   YearStatus = y.YearStatus
        //                               }).FirstOrDefault();

        //                request.nYear = yearDTO.NYear;
        //                request.SchoolId = userData.CompanyID;
        //                var rooms = GradeService.GetRoomListForSharingAssessments(request);

        //                return Json(rooms);

        //            }
        //        }
        //        return Json(new List<SchoolBright.DTO.DTO.TermSubLevel2DTO>());
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GetRoomListForSharingAssessments", ex, string.Empty);
        //        throw;
        //    }
        //}


        //[HttpPost]
        //[Route("ShareAssessmentToOtherRooms")]
        //[SessionTimeout]
        //public IHttpActionResult ShareAssessmentToOtherRooms([FromBody]CommonRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //        {
        //            if (request != null)
        //            {
        //                CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "ShareAssessmentToOtherRooms Called", null, string.Empty);


        //                var yearDTO = CommonService.GetYearByYearNumber(request.NumberYear, userData.CompanyID, userData.UserID);
        //                request.nYear = yearDTO.NYear;


                       
        //                request.UpdatedBy = userData.UserID;
        //                request.UpdatedDate = DateTime.Now;
        //                request.SchoolId = userData.CompanyID;
        //                GradeService.ShareAssessmentToOtherRooms(request);


        //                return Json(new List<SchoolBright.DTO.DTO.TermSubLevel2DTO>());


        //            }
        //        }
        //        return Json(new List<SchoolBright.DTO.DTO.TermSubLevel2DTO>());
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "ShareAssessmentToOtherRooms", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateAssessmentName")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateAssessmentName([FromBody]AssessmentUpdateRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            request.SchoolId = userData.CompanyID;
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentName Called", null, string.Empty);
        //            GradeService.UpdateAssessmentName(request);
        //        }

        //        return Ok("Success");
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentName", ex, string.Empty);
        //        throw; 
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateAssessmentDefaultValue")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateAssessmentDefaultValue([FromBody]AssessmentDefaultValueUpdateRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            request.SchoolId = userData.CompanyID;
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentDefaultValue Called", null, string.Empty);
        //            GradeService.UpdateAssessmentDefaultValue(request);
        //        }

        //        return Ok("Success");
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentDefaultValue", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateAssessmentMaxScore")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateAssessmentMaxScore([FromBody]AssessmentUpdateRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {

        //        if (request != null)
        //        {
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            request.SchoolId = userData.CompanyID;
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentMaxScore Called", null, string.Empty);

        //            //request.AssessmentMaxScore = "A";
        //            var isValidScore = false;
        //            if (!string.IsNullOrEmpty(request.AssessmentMaxScore) && Int32.TryParse(request.AssessmentMaxScore, out int value))
        //            {
        //                isValidScore = true;
        //            }

        //            if (!string.IsNullOrEmpty(request.AssessmentMaxScore) && isValidScore == false) return Ok("InValidScore");
                    
        //            GradeService.UpdateAssessmentMaxScore(request);
        //        }

        //        return Ok("Success");
        //    }
        //    catch(Exception ex)
        //    {
               
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateAssessmentMaxScore", ex, string.Empty);

        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateScoreProportion")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateScoreProportion([FromBody]AssessmentUpdateRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
                   
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            request.SchoolId = userData.CompanyID;
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateScoreProportion Called", null, string.Empty);
        //            GradeService.UpdateScoreProportion(request);
        //        }
                
        //        return Ok("Success");
        //    }
        //    catch(Exception  ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateScoreProportion", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("UpdateStudentAssessmentScore")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateStudentAssessmentScore([FromBody]List<StudentAssessmentScoreUpdateRequest> request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    var checkDBConnection = false;
        //    try
        //    {
        //        checkDBConnection = ServiceHelper.CheckConnection();
        //        if (request != null && checkDBConnection)
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateStudentAssessmentScore Called", null, string.Empty);
        //            if (request.Count == 1)
        //            {
        //                //request[0].Score = "A";
        //                var isValidScore = false;
        //                if (!string.IsNullOrEmpty(request[0].Score) && Double.TryParse(request[0].Score, out double value))
        //                {
        //                    isValidScore = true;
        //                }

        //                if (!string.IsNullOrEmpty(request[0].Score) && isValidScore == false) return Ok("InValidScore");
        //            }
                   

        //            var requestAsList = request.ToList();
        //            var dateTime = DateTime.Now;
        //            requestAsList.ForEach(f => { f.UpdatedDate = dateTime; f.UpdatedBy = Utils.GetUserId(); f.SchoolId = userData.CompanyID; f.CreatedDate = dateTime; f.CreatedBy = userData.UserID; });
        //            GradeService.UpdateStudentAssessmentScore(requestAsList);
        //            return Ok("Success");
        //        }
        //        else
        //        {
        //            throw new Exception();
        //        }
                
        //    }
        //    catch(Exception ex)
        //    {
        //        if (!checkDBConnection) throw;
        //        try
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateStudentAssessmentScore", ex, string.Empty);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("GradeImportFromExcel")]
        //[SessionTimeout]
        //public IHttpActionResult GradeImportFromExcel([FromBody]GradeImportFromExcelRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GradeImportFromExcel Called", null, string.Empty);
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            request.SchoolId = userData.CompanyID;
        //            GradeService.GradeImportFromExcel(request);
        //        }

        //        return Json(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GradeImportFromExcel", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("GradeDetailsImportFromExcel")]
        //[SessionTimeout]
        //public IHttpActionResult GradeDetailsImportFromExcel([FromBody]List<GradeDetailsImportFromExcelRequest> request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null && request.Count > 0)
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GradeDetailsImportFromExcel Called", null, string.Empty);
        //            request.ForEach(f => { f.UpdatedDate = DateTime.Now; f.UpdatedBy = userData.UserID; f.SchoolId = userData.CompanyID; });
        //            GradeService.GradeDetailsImportFromExcel(request);
        //        }

        //        return Json(string.Empty);
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "GradeDetailsImportFromExcel", ex, string.Empty);

        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("ImportStudentCurricularMarks")]
        //[SessionTimeout]
        //public IHttpActionResult ImportStudentCurricularMarks([FromBody]CommonRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "ImportStudentCurricularMarks Called", null, string.Empty);

        //            request.SchoolId = userData.CompanyID;
        //            request.UpdatedDate = DateTime.Now;
        //            request.UpdatedBy = userData.UserID;
        //            GradeService.ImportStudentCurricularMarks(request);
        //        }
        //        return Ok("Success");
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "ImportStudentCurricularMarks", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("LockStudentScore")]
        //[SessionTimeout]
        //public IHttpActionResult LockStudentScore([FromBody]GradeLockRequest gradeLockRequest)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (gradeLockRequest != null)
        //        {
        //            CreateLogOnMethodCall(gradeLockRequest, userData.CompanyID, userData.UserID, "LockStudentScore Called", null, string.Empty);

        //            gradeLockRequest.UpdatedDate = DateTime.Now;
        //            gradeLockRequest.UpdatedBy = userData.UserID;
        //            gradeLockRequest.SchoolId = userData.CompanyID;
        //            GradeService.LockStudentScore(gradeLockRequest);
        //        }
        //        return Ok("Success");
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(gradeLockRequest, userData.CompanyID, userData.UserID, "LockStudentScore", ex, string.Empty);

        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("CreateGradeLog")]
        //[SessionTimeout]
        //public IHttpActionResult CreateGradeLog([FromBody]GradeLogRequest gradeLogRequest)
        //{
        //    try
        //    {
        //        if (gradeLogRequest != null && !string.IsNullOrEmpty(gradeLogRequest.Action))
        //        {
        //            if (ServiceHelper.CheckConnection())
        //            {
        //                database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", gradeLogRequest.GetLogMessage(), HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 109, 2, 1);
        //                return Ok("Success");
        //            }
        //            else
        //            {
        //                throw new Exception(); //DB Connection failed;
        //            }
        //        }
        //        throw new Exception(); // If Action is empty
        //    }
        //    catch(Exception ex)
        //    {
        //        throw;
        //    }
        //}

        [Route("GetCoursesForImportGrade")]
        [SessionTimeout]
        public IHttpActionResult GetCoursesForImportGrade([FromUri]int nTermSubLevel2, string nTerm, int nTSubLevel, int sPlaneID)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string parameters = string.Format("nTermSubLevel2:{0},nTerm:{1},nTSubLevel{2},sPlaneID:{3}", nTermSubLevel2, nTerm, nTSubLevel, sPlaneID);
            try
            {
               
                CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetCoursesForImportGrade Called", null, parameters);

                return Json(PlanService.GetCoursesForImportGrade(nTermSubLevel2, nTerm, nTSubLevel, sPlaneID, userData.CompanyID, userData.UserID));
            }
            catch(Exception ex)
            {

                CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetCoursesForImportGrade Called", ex, parameters);
                throw;
            }
        }

        //[Route("GetAssessmentDetailsForImportScore")]
        //[SessionTimeout]
        //public IHttpActionResult GetAssessmentDetailsForImportScore([FromUri]int nGradeId, int assessmentTypeId)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    string parameters = string.Format("nGradeId:{0},assessmentTypeId:{1}", nGradeId, assessmentTypeId);
        //    try
        //    {

        //        CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetAssessmentDetailsForImportScore Called", null, parameters);
        //        var assessments = GradeService.GetAssessmentDetailsForImportScore(nGradeId, assessmentTypeId, userData.CompanyID, userData.UserID);
        //        return Json(assessments);
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetAssessmentDetailsForImportScore", ex, parameters);

        //        throw;
        //    }
        //}


        //[HttpPost]
        //[Route("CheckGradeScoreRatioUpdated")]
        //[SessionTimeout]
        //public IHttpActionResult CheckGradeScoreRatioUpdated([FromBody]GradeScoreRatioUpdatedRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            
        //    try
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "CheckGradeScoreRatioUpdated Called", null, string.Empty);
        //        request.SchoolId = userData.CompanyID;
        //        request.sEmp = userData.UserID;
        //        //GradeService.CheckGradeScoreRatioUpdated(request);
        //        return Json(GradeService.CheckGradeScoreRatioUpdated(request));

        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "CheckGradeScoreRatioUpdated", ex, string.Empty);

        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("DeleteBehaviourScores")]
        //[SessionTimeout]
        //public IHttpActionResult DeleteBehaviourScores([FromBody]CommonRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        request.UpdatedDate = DateTime.Now;
        //        request.UpdatedBy = userData.UserID;
        //        request.SchoolId = userData.CompanyID;
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "DeleteBehaviourScores Called", null, string.Empty);

        //        GradeService.DeleteBehaviourScores(request);
        //        return Json(new List<dynamic>());
        //    }
        //    catch(Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "DeleteBehaviourScores", ex, string.Empty);
        //        throw;
        //    }
        //}

        [HttpPost]
        [Route("ConvertDateTimeToThai")]
        [SessionTimeout]
        public IHttpActionResult ConvertDateTimeToThai([FromBody]CommonRequest request)
        {
            try
            {
                var updatedDate = (request.UpdatedDate == null) ? DateTime.Now : (DateTime)request.UpdatedDate;
                ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();

                int thaiYear = cal.GetYear(updatedDate);
                int thaiMonth = cal.GetMonth(updatedDate);
                int thaiDay = cal.GetDayOfMonth(updatedDate);
                int thaiHour = cal.GetHour(updatedDate);
                int thaiMinute = cal.GetMinute(updatedDate);
                int thaiSecond = cal.GetSecond(updatedDate);

                return Json(String.Format("{0:dd/MM/yyyy HH:mm:ss}", new DateTime(thaiYear, thaiMonth, thaiDay, thaiHour, thaiMinute, thaiSecond)));
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void CreateLogOnMethodCall(object request, int schoolId, int sEmp, string methodName, Exception ex, string parameters)
        {
            try
            {
                string requestJson = string.Empty;

                if (request != null)
                {
                    requestJson = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });
                }
                parameters = string.Format("{0}-request:{1}", parameters ?? string.Empty, requestJson);
                if (ex != null)
                {
                    var exceptionRequest = new ExceptionLogRequest()
                    {
                        Application = "FingerPrintPayment",
                        ExceptionMsg = (ex != null) ? string.Format("Message {0} InnerException : {1} InnerException Stack Trace : {2}", ex.Message ?? string.Empty, (ex.InnerException != null) ? ex.InnerException.Message.ToString() : string.Empty, (ex.InnerException != null) ? ex.InnerException.StackTrace.ToString() : string.Empty) : string.Empty,
                        ExceptionSource = (ex != null) ? ex.Source : string.Empty,
                        ExceptionType = (ex != null && ex.GetType() != null) ? ex.GetType().ToString() : string.Empty,
                        SchoolId = schoolId,
                        SEmp = sEmp,
                        MethodName = methodName,
                        Logdate = DateTime.Now,
                        Parameter = parameters,
                        ExceptionUrl = "AssessmentScoreController",
                    };

                    CommonService.CreateExceptionLog(exceptionRequest);
                    //SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", null, schoolId, sEmp, methodName, parameters, "", null);
                }
                else
                {
                    var gradeAuditLogRequest = new GradeAuditLogRequest()
                    {
                        Application = "FingerPrintPayment",
                        SchoolId = schoolId,
                        SEmp = sEmp,
                        MethodName = methodName,
                        Logdate = DateTime.Now,
                        Parameter = parameters,
                    };

                    CommonService.CreateGradeAuditLog(gradeAuditLogRequest);
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        //[Route("GetStudentRoomAndCourse")]
        //[SessionTimeout]
        //public IHttpActionResult GetStudentRoomAndCourseListByClass([FromUri] string nTerm, int nTSubLevel, int sPlaneID, int SchoolId)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            
        //    try
        //    {
        //        var student = ServiceHelper.GetAllStudentAndSubjectInfoByClassAndTerm(nTSubLevel, nTerm, SchoolId, sPlaneID);

        //        return Json(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {

              
        //        throw;
        //    }
        //}


        //[HttpPost]
        //[Route("DeleteStudentScore")]
        //[SessionTimeout]
        //public IHttpActionResult DeleteStudentScore([FromBody] CommonRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {
        //        if (request != null)
        //        {
                   
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "DeleteStudentScore Called", null, string.Empty);
        //            request.SchoolId = userData.CompanyID;
        //            request.SEmp = userData.UserID;
        //            request.UpdatedBy = userData.UserID;
        //            request.UpdatedDate = DateTime.Now;
        //            if (!request.IsRequestForCurrentAcademicYear && request.NumberYear > 0)
        //            {
        //                request.IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(request.NumberYear, null, userData.CompanyID);
        //            }
        //            GradeService.DeleteStudentScore(request);


        //            return Json("Success");
                   
                   
        //        }
        //        return Json("");
        //    }
        //    catch (Exception ex)
        //    {
        //        //var json = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        //string requestJson = (request != null) ? json.Serialize(request) : "null";
        //        //string parameters = string.Format("request:{0}", requestJson);
        //        //SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "GetStudentAssessmentScore", parameters, "", null);

        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "DeleteStudentScore", ex, string.Empty);

        //        throw;
        //    }
        //}


        //[Route("GetStudentGradeInfo")]
        //[SessionTimeout]
        //public IHttpActionResult GetStudentGradeInfo([FromUri] int sId)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    //string parameters = string.Format("nTermSubLevel2:{0},nTerm:{1},nTSubLevel{2},sPlaneID:{3}", nTermSubLevel2, nTerm, nTSubLevel, sPlaneID);
        //    try
        //    {

        //        //CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetStudentGradeInfo Called", null, parameters);

        //        var gradeDTO = GradeService.GetStudentGradeInfo(sId, userData.CompanyID).ToList();
        //        if (gradeDTO != null)
        //        {
        //            int Srno = 0;

        //            //Define the column list for DataTable
        //            var columns = new[] {
        //                                    //new { data = "sID"},
        //                                    new { name = "RowNumber", data = "RowNumber", className = "RowNumber", visible=true}, //0

        //                                    new { name = "nGradeId", data = "nGradeId", className = "hidden",visible=false},  //1
        //                                    new { name = "nYear", data = "nYear", className = "nYear", visible=false}, //2
        //                                    new { name = "PlanId", data = "PlanId", className = "PlanId", visible=false},   //3
        //                                    new { name = "sPlaneId", data = "sPlaneId", className = "sPlaneId", visible=false},  //4
        //                                    new { name = "nTerm", data = "nTerm", className = "nTerm", visible=false},   //5

        //                                    new { name = "NumberYear", data = "NumberYear", className = "NumberYear", visible=true}, //6
        //                                    new { name = "PlanName", data = "PlanName", className = "PlanName", visible=true}, //7
        //                                    new { name = "CourseCode", data = "CourseCode", className = "CourseCode", visible=true}, //8

        //                                    //new { name = "CourseName", data = "CourseName", className = "CourseName", visible=true},
        //                                    new { name = "STerm", data = "STerm", className = "STerm", visible=true},  //9
        //                                    new { name = "NCredit", data = "NCredit", className = "NCredit", visible=true},  //10
        //                                    new { name = "CourseTotalHour", data = "CourseTotalHour", className = "CourseTotalHour", visible=true},//11
        //                                    new { name = "GetScore100", data = "GetScore100", className = "GetScore100", visible=true }, //12
        //                                    new { name = "GetGradeLabel", data = "GetGradeLabel", className = "GetGradeLabel", visible=true}, //13
        //                                    new { name = "GetBehaviorLabel", data = "GetBehaviorLabel", className = "GetBehaviorLabel", visible=true}, //14
        //                                    new { name = "GetReadWrite", data = "GetReadWrite", className = "GetReadWrite", visible=true},  //15
        //                                    new { name = "GetSamattana", data = "GetSamattana", className = "GetSamattana", visible=true}, //16
        //                                    new { name = "IsScoreFromScoreEntryPage", data = "IsScoreFromScoreEntryPage", className = "IsScoreFromScoreEntryPage", visible = true }, //17

        //                                    new { name = "OptYear", data = "OptYear", className = "OptYear",visible=false}, //18
        //                                    new { name = "OptPlanName", data = "OptPlanName", className = "OptPlanName", visible=false}, //19
        //                                    new { name = "OptCourseCode", data = "OptCourseCode", className = "OptCourseCode", visible=false}, //20
        //                                    new { name = "OptTerm", data = "OptTerm", className = "OptTerm", visible=false}, //21
        //                                    new { name = "IsSavedData", data = "IsSavedData", className = "IsSavedData", visible=false}, //22
        //                                    new { name = "DeleteScoreForAllStudent", data = "DeleteScoreForAllStudent", className = "DeleteScoreForAllStudent", visible = true }, //23

        //                                }.ToList();



        //            var gradeInfo = gradeDTO.Select(s => new GradeTransferViewModel {
        //                RowNumber = ++Srno, nGradeId = s.NGradeId,
        //                nYear = s.nYear ?? 0,
        //                PlanId = s.PlanId,
        //                nTerm = s.NTerm,
        //                sPlaneId = s.SPlaneID,
        //                NumberYear = s.NumberYear.ToString(),
        //                PlanName = s.PlanName,
        //                CourseCode = s.CourseCode,
        //                STerm = s.STerm,
        //                CourseName = s.CourseName,
        //                NCredit = (s.nCredit != null) ? s.nCredit.ToString() : string.Empty,
        //                CourseTotalHour = (s.CourseTotalHour != null) ? s.CourseTotalHour.ToString() : string.Empty,
        //                GetScore100 = s.getScore100,
        //                GetGradeLabel = s.getGradeLabel,
        //                IsScoreFromScoreEntryPage = ((s.nTermSubLevel2 == null) ? false : true),
        //                GetBehaviorLabel = s.GetBehaviorLabel,
        //                GetReadWrite = s.GetReadWrite,
        //                GetSamattana = s.GetSamattana,
        //                IsSavedData = true,
        //                DeleteScoreForAllStudent = ((s.nTermSubLevel2 == null) ? false : true),
        //            }) ;


        //            return Json(new
        //            {
                       
        //                data = gradeInfo.ToArray(),
        //                column = columns.ToArray(),
                        
        //            });
        //        }

        //        return Json(string.Empty);
        //    }
        //    catch (Exception ex)
        //    {

        //        //CreateLogOnMethodCall(null, userData.CompanyID, userData.UserID, "GetCoursesForImportGrade Called", ex, parameters);
        //        throw;
        //    }
        //}

        
        //[Route("GetStudentGradeInfoByYearly")]
        //[SessionTimeout]
        //public IHttpActionResult GetStudentGradeInfoByYearly([FromUri] int nTSubLevel, int nTermSubLevel2, int numberYear, string sTerm, int sId, bool isRankingRequired)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

           
        //        var gradeInfo = ServiceHelper.GetStudentGradeInfoByYearly(userData.CompanyID, nTSubLevel, nTermSubLevel2, numberYear, sTerm, sId, isRankingRequired);

        //    if (gradeInfo != null)
        //    {
        //        gradeInfo = gradeInfo.Where(w => w.sID == sId).ToList();
        //    }
        //    //if (studentRanking.RankingInfoColumn != null)
        //    //{
        //    //    var columnsDataTable = new[] {
        //    //                            new { name = "RowNumber", title="ลำดับ", data = "RowNumber", className = "RowNumber", visible=true}, //0
        //    //                            new { name = "Ranking", title="อันดับ", data = "Ranking", className = "Ranking",visible=true},  //1
        //    //                        }.ToList();

        //    //    var columnName = new List<string>()
        //    //    {
        //    //        "ลำดับ","อันดับ"
        //    //    };
        //    //    foreach (var c in studentRanking.RankingInfoColumn)
        //    //    {
        //    //        var header = c;
        //    //        if (c == "StudentId") header = "รหัสนักเรียน";
        //    //        if (c == "Name") header = "ชื่อ";
        //    //        if (c == "FinalGPA") header = "ผลการเรียนสะสม";
        //    //        if (c == "SubLevel2") header = "ชั้นเรียน";
        //    //        columnsDataTable.Add(new { name = c, title = header, data = c, className = "", visible = true });
        //    //        columnName.Add(header);
        //    //    }


        //    //    return Json(new
        //    //    {

        //    //        data = studentRanking.RankingInfo,
        //    //        column = columnsDataTable,
        //    //        columnNames = columnName
        //    //    });
        //    //}

        //    return Json(new
        //    {

        //        data = gradeInfo,
               
        //    });
        //}

        //[HttpPost]
        //[Route("UpdateStudentsGradeTransferDetails")]
        //[SessionTimeout]
        //public IHttpActionResult UpdateStudentsGradeTransferDetails([FromBody] StudentAssessmentScoreUpdateRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {

        //        if (request != null)
        //        {
        //            CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateStudentsGradeTransferDetails Called", null, string.Empty);
        //            //var requestAsList = request.ToList();
        //            var dateTime = DateTime.Now;
        //            request.SchoolId = userData.CompanyID;
        //            request.UpdatedBy = userData.UserID;
        //            request.UpdatedDate = dateTime;
        //            request.CreatedBy = userData.UserID;
        //            request.CreatedDate = dateTime;
        //            if (request.nTerm != "รายปี")
        //            {
        //                return Ok(GradeService.UpdateStudentsGradeTransferDetails(request));
        //            }
        //            else if (request.nYear > 0)
        //            {
        //                //var terms = CommonService.GetTerms(request.nYear, userData.CompanyID, userData.UserID);
        //                //foreach(var t in terms)
        //                //{
        //                //    request.nTerm = t.nTerm;
        //                //    GradeService.UpdateStudentsGradeTransferDetails(request);
                           
        //                //}
        //                return Ok(0);
        //            }
        //        }

        //        return Ok(0);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateStudentsGradeTransferDetails", ex, string.Empty);
        //        throw;
        //    }
        //}

        //[HttpPost]
        //[Route("GetGradeLog")]
        //[SessionTimeout]
        //public IHttpActionResult GetGradeLog([FromBody] GradeLogRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    try
        //    {

        //        if (request != null)
        //        {
        //            request.SchoolId = userData.CompanyID;
        //            request.UserID = userData.UserID;
        //            var logs = ServiceHelper.GetGradeLog(request);

        //            var columns = new[] {
        //                                    new { name = "LogDate", data = "LogDate", className = "LogDate", visible=true}, //0
        //                                    new { name = "LogDetails", data = "LogDetails", className = "LogDetails",visible=true},  //1
        //                                    new { name = "TeacherName", data = "TeacherName", className = "TeacherName", visible=true}, //2
        //                                }.ToList();
        //            return Json(new
        //            {

        //                data = (logs != null)?logs.ToArray(): null,
        //                column = columns.ToArray(),

        //            });
        //        }

        //        return Ok(0);
        //    }
        //    catch (Exception ex)
        //    {
        //        CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateStudentsGradeTransferDetails", ex, string.Empty);
        //        throw;
        //    }
        //}


        [HttpPost]
        [Route("UpdateSettingExtraTime")]
        [SessionTimeout]
        public IHttpActionResult UpdateSettingExtraTime([FromBody] SettingExtraTimeRequest request)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            try
            {

                if (request != null)
                {
                    request.SchoolID = userData.CompanyID;
                    ServiceHelper.UpdateSettingExtraTime(request, userData.UserID);
                }

                return Ok(0);
            }
            catch (Exception ex)
            {
                CreateLogOnMethodCall(request, userData.CompanyID, userData.UserID, "UpdateSettingExtraTime", ex, string.Empty);
                throw;
            }
        }

        //[HttpPost]
        //[Route("GetStudentRankingByGPA")]
        //[SessionTimeout]
        //public IHttpActionResult GetStudentRankingByGPA([FromBody] GradeRequest request)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    if (request != null)
        //    {
        //        request.SchoolId = userData.CompanyID;
        //        var studentRanking = ServiceHelper.GetStudentRankingByGPA(request);
                
        //         if (studentRanking.RankingInfoColumn != null)
        //         {
        //            var columnsDataTable = new[] {
        //                                    new { name = "RowNumber", title="ลำดับ", data = "RowNumber", className = "RowNumber", visible=true}, //0
        //                                    new { name = "Ranking", title="อันดับ", data = "Ranking", className = "Ranking",visible=true},  //1
        //                                }.ToList();

        //            var columnName = new List<string>()
        //            {
        //                "ลำดับ","อันดับ"
        //            };
        //            foreach(var c in studentRanking.RankingInfoColumn)
        //            {
        //                var header = c;
        //                if (c == "StudentId") header = "รหัสนักเรียน";
        //                if (c == "Name") header = "ชื่อ";
        //                if (c == "FinalGPA") header = "ผลการเรียนสะสม";
        //                if (c == "SubLevel2") header = "ชั้นเรียน";
        //                columnsDataTable.Add(new { name = c, title = header, data = c, className = "", visible = true });
        //                columnName.Add(header);
        //            }
                 
               
        //            return Json(new
        //            {

        //                data = studentRanking.RankingInfo,
        //                column = columnsDataTable,
        //                columnNames = columnName
        //            });
        //        }
        //    }
        //    return null;
        //}


       

        [HttpPost]
        [Route("StudentRankingExport")]
        [SessionTimeout]
        public byte[] StudentRankingExport([FromBody] GradeRankingRequest request)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            var response = new HttpResponseMessage();
            try
            {
                request.SchoolId = userData.CompanyID;
                byte[] buffer = ServiceHelper.GetGradeRankingPdfContent(request);


                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(new MemoryStream(buffer));

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = buffer.Length;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "myFirstPDF.pdf"
                };
                //return System.IO.FileInfo(buffer, "application/pdf", "Weeeeeee_" + DateTime.Now.ToString("_MM-dd-yyyy-mm-ss-tt") + ".pdf");
                //return Json(new { filename = string.Format("{0}-{1}-{2}-{3}", request.NumberYear,request.SubLevel, request.SubLevel2), message = Convert.ToBase64String(buffer) });
                return buffer;
            }
            catch (Exception e)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
            //response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            //return response;
            return null;
        }

        [Route("GetPdf"), HttpGet]
        public HttpResponseMessage GetPdf()
        {
            var response = new HttpResponseMessage();
            try
            {
                byte[] buffer = createPDF();


                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StreamContent(new MemoryStream(buffer));

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
                response.Content.Headers.ContentLength = buffer.Length;
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "myFirstPDF.pdf"
                };

            }
            catch (Exception e)
            {
                response = Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }

            return response;
        }

        private byte[] createPDF()
        {
            MemoryStream memStream = new MemoryStream();
            byte[] pdfBytes;

            Document doc = new Document(iTextSharp.text.PageSize.LETTER);
            PdfWriter wri = PdfWriter.GetInstance(doc, memStream);
            doc.AddTitle("test");
            doc.AddCreator("I am");

            doc.Open();//Open Document to write
            Paragraph paragraph = new Paragraph("This is my first line using Paragraph.");
            Phrase pharse = new Phrase("This is my second line using Pharse.");
            Chunk chunk = new Chunk(" This is my third line using Chunk.");
            doc.Add(paragraph);
            doc.Add(pharse);
            doc.Add(chunk);
            doc.Close(); //Close 
            pdfBytes = memStream.ToArray();

            return pdfBytes;
        }
    }
}
