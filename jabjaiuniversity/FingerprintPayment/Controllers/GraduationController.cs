using Ninject;
using SchoolBright.Business.Interfaces;
using System.Web.Http;

namespace FingerprintPayment.Controllers
{
    [RoutePrefix("api/Graduation")]
    public class GraduationController : ApiController
    {

        [Inject]
        public IGradeService GradeService { get; set; }

        [Inject]
        public IGraduationService GraduationService { get; set; }


        //[Route("GetStudentGraduation")]
        //public IHttpActionResult GetStudentGraduation([FromUri]int nGradeId, int assessmentTypeId)
        //{
        //    int schoolId = Utils.GetSchoolId();
        //    CommonRequest commonRequest = new CommonRequest();
        //    commonRequest.SchoolId = Utils.GetSchoolId();
        //    var StudentGraduation = GraduationService.GetStudentGraduation(commonRequest);
        //    return Json(StudentGraduation);
        //}

    }
}
