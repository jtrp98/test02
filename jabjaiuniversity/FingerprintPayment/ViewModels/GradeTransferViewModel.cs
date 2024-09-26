using System.Collections.Generic;

namespace FingerprintPayment.ViewModels
{
    public class GradeTransferViewModel
    {
        public int RowNumber { get; set; }
        public int nGradeId { get; set; }
        public int nYear { get; set; }
        public string nTerm { get; set; }
        public int PlanId { get; set; }
        public int sPlaneId { get; set; }
        public string NumberYear { get; set; }
        public string STerm { get; set; }
        public string PlanName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string NCredit { get; set; }
        public string CourseTotalHour { get; set; }
       
        public string GetScore100 { get; set; }

        public string GetGradeLabel { get; set; }
        public bool IsScoreFromScoreEntryPage { get; set; }

        public bool DeleteScoreForAllStudent { get; set; }

        public bool IsSavedData { get; set; }

        public string GetBehaviorLabel { get; set; }
        public string GetReadWrite { get; set; }
        public string GetSamattana { get; set; }

        public List<ddlterm> OptYear { get; set; }  //Datatable Maintain the Year dropdown value

        public List<ddlterm> OptPlanName { get; set; } //Datatable Maintain the Plan Details  based on OptYear value

        public List<ddlterm> OptCourseCode { get; set; } //Datatable Maintain the Course Details  based on OptPlanName value
        public List<ddlterm> OptTerm { get; set; } //Datatable Maintain the Term Details  based on OptCourseCode value
    }
}