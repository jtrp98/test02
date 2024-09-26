using FingerprintPayment.Helper;
using FingerprintPayment.Models;
using FingerprintPayment.studentclass.Models;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;
using static Accounting.Invoices;

namespace FingerprintPayment.studentclass
{
    public partial class index : BehaviorGateway
    {
        public List<QueryDataBases.SubLevel_Query.Class> classesData = new List<QueryDataBases.SubLevel_Query.Class>();
        public string strYearOption = "<option value=\"\" selected>เลือกปีการศึกษา</option>";
        private static BaseResultModel baseResult = new BaseResultModel();
        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiEntities entities = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                classesData = QueryDataBases.SubLevel_Query.GetData(entities, userData);
                StudentLogic logic = new StudentLogic(entities);
                var termId = logic.GetTermId(DateTime.Today, UserData);
                var f_term = (from a in entities.TTerms
                              join b in entities.TYears.Where(w => w.cDel == false) on a.nYear equals b.nYear
                              where a.nTerm == termId
                              select b).FirstOrDefault();
                int yearsBack = -5;
                int nYear = DateTime.Today.AddYears(yearsBack).Year + 543;
                if (f_term != null && f_term.numberYear.HasValue) nYear = f_term.numberYear.Value + yearsBack;

                foreach (var yearData in (from a in entities.TYears.Where(w => w.SchoolID == UserData.CompanyID && w.cDel == false)
                                              //join b in entities.TTerms on a.nYear equals b.nYear
                                          where a.numberYear >= nYear
                                          orderby a.numberYear descending
                                          select a))
                {
                    strYearOption += "<option value=\"" + yearData.nYear + "\" >" + yearData.numberYear + "</option>";
                }
            }
        }

        //CHECK SCOER ENTERED
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object CheckScoreEntered(StudentClassHistoryDTO leftData, StudentClassHistoryDTO rightData, DateTime dateChange)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            List<string> courseCode = new List<string>();
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);

                using (var _dbGrade = new PostgreSQL.PGGradeDBEntities("PGGradeDBReadReplicaEntities"))
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    var rightRoomNewStudent = dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && !leftData.StudentId.Any(a => a == w.sID) && w.nTerm == leftData.TermId.Trim() && w.nTermSubLevel2 == leftData.ClassroomId && w.cDel == null && (w.nStudentStatus ?? 0) == 0).Select(s => s.sID).AsQueryable().ToList();
                    var leftRoomNewStudent = dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && !rightData.StudentId.Any(a => a == w.sID) && w.nTerm == rightData.TermId.Trim() && w.nTermSubLevel2 == rightData.ClassroomId && w.cDel == null && (w.nStudentStatus ?? 0) == 0).Select(s => s.sID).AsQueryable().ToList();

                    var oldClass = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == leftData.ClassroomId).AsQueryable().FirstOrDefault();
                    var newClass = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == rightData.ClassroomId).AsQueryable().FirstOrDefault();

                    if (oldClass.nTSubLevel == newClass.nTSubLevel)
                    {
                        if (leftRoomNewStudent != null && leftRoomNewStudent.ToList().Count > 0)
                        {
                            var leftRoomNewStudentGrade = (from g in _dbGrade.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == leftData.TermId.Trim() && w.nTermSubLevel2 == rightData.ClassroomId)
                                                           join gd in _dbGrade.PGTGradeDetails.Where(w => w.SchoolID == userData.CompanyID && leftRoomNewStudent.Contains(w.sID)) on g.nGradeId equals gd.nGradeId
                                                           where !((string.IsNullOrEmpty(gd.getScore100) || gd.getScore100 == "0") && gd.getSpecial == "-1" && string.IsNullOrEmpty(gd.scoreFinalTerm) && string.IsNullOrEmpty(gd.scoreMidTerm) && string.IsNullOrEmpty(gd.ScoreBeforeMidTerm))
                                                           select g.sPlaneID).ToList();

                            //leftRoomNewStudentGrade = leftRoomNewStudentGrade.Where(w => !string.IsNullOrEmpty(w.ScoreBeforeMidTerm) || !string.IsNullOrEmpty(w.ScoreAfterMidTerm) || !string.IsNullOrEmpty(w.scoreMidTerm) || !string.IsNullOrEmpty(w.scoreFinalTerm)).ToList();

                            if (leftRoomNewStudentGrade != null && leftRoomNewStudentGrade.Count > 0)
                            {
                                courseCode = (from g in leftRoomNewStudentGrade
                                              join p in dbschool.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on g equals p.sPlaneID
                                              select p.courseCode).Distinct().ToList();



                                baseResult.StatusCode = "401";
                                baseResult.Result = courseCode;
                            }

                        }
                        else
                        {
                            baseResult.StatusCode = "200";
                            baseResult.Result = courseCode;
                        }

                        if (baseResult.StatusCode != "401" && rightRoomNewStudent != null && rightRoomNewStudent.ToList().Count > 0)
                        {
                            var rightRoomNewStudentGrade = (from g in _dbGrade.PGTGrades.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == rightData.TermId.Trim() && w.nTermSubLevel2 == leftData.ClassroomId)
                                                            join gd in _dbGrade.PGTGradeDetails.Where(w => w.SchoolID == userData.CompanyID && rightRoomNewStudent.Contains(w.sID)) on g.nGradeId equals gd.nGradeId
                                                            where !((string.IsNullOrEmpty(gd.getScore100) || gd.getScore100 == "0") && gd.getSpecial == "-1" && string.IsNullOrEmpty(gd.scoreFinalTerm) && string.IsNullOrEmpty(gd.scoreMidTerm) && string.IsNullOrEmpty(gd.ScoreBeforeMidTerm))
                                                            select g.sPlaneID).ToList();

                            //rightRoomNewStudentGrade = rightRoomNewStudentGrade.Where(w => !string.IsNullOrEmpty(w.ScoreBeforeMidTerm) || !string.IsNullOrEmpty(w.ScoreAfterMidTerm) || !string.IsNullOrEmpty(w.scoreMidTerm) || !string.IsNullOrEmpty(w.scoreFinalTerm)).ToList();

                            if (rightRoomNewStudentGrade != null && rightRoomNewStudentGrade.Count > 0)
                            {
                                courseCode = (from g in rightRoomNewStudentGrade
                                              join p in dbschool.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on g equals p.sPlaneID
                                              select p.courseCode).Distinct().ToList();

                                baseResult.StatusCode = "401";
                                baseResult.Result = courseCode;
                            }
                            else
                            {
                                baseResult.StatusCode = "200";
                                baseResult.Result = courseCode;
                            }
                        }
                    }
                    else
                    {
                        baseResult.StatusCode = "200";
                        baseResult.Result = courseCode;
                    }
                }
                return baseResult;
            }

        }

        //CHANGE CLASS STUDENT
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object updateDataHistory(StudentClassHistoryDTO leftData, StudentClassHistoryDTO rightData, DateTime dateChange)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                int ValidateCount = 0;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //leftData.TermId = string.Format("TS{0:0000000}", int.Parse(leftData.TermId));
                    //rightData.TermId = string.Format("TS{0:0000000}", int.Parse(rightData.TermId));
                    var f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm == leftData.TermId);

                    StudentLogic logic = new StudentLogic(dbschool);
                    var f_term_Current = logic.GetTermDATA(DateTime.Today, new JWTToken.userData { CompanyID = userData.CompanyID });
                    f_term_Current = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nYear == f_term_Current.nYear);
                    List<int?> UserID = new List<int?>();
                    //if (f_term_Current.dStart > dateChange) ValidateCount = 1;
                    //if (f_term_Current == null)
                    //{
                    //    f_term_Current = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => DateTime.Today <= f.dStart);
                    //}


                    /// Checked Student Right Data
                    var q_count_1 = (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                     join b in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals b.nTerm
                                     where b.nYear == f_term.nYear && b.dStart >= f_term.dEnd
                                     //&& rightData.ClassroomId == a.nTermSubLevel2
                                     select a).ToList();

                    foreach (var data in (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                          where a.nTerm == rightData.TermId && rightData.StudentId.Contains(a.sID ?? 0)
                                          select a))
                    {
                        int _Count = q_count_1.Count(c => c.sID == data.sID && c.nTermSubLevel2 != data.nTermSubLevel2);
                        ValidateCount += _Count;
                    }

                    /// Checked Student Left Data
                    var q_count_2 = (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                     join b in dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals b.nTerm
                                     where b.nYear == f_term.nYear && b.dStart >= f_term.dEnd
                                     //&& leftData.ClassroomId == a.nTermSubLevel2
                                     select a).ToList();

                    foreach (var data in (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                          where a.nTerm.Trim() == leftData.TermId && leftData.StudentId.Contains(a.sID ?? 0)
                                          select a))
                    {
                        int _Count = q_count_2.Count(c => c.sID == data.sID && c.nTermSubLevel2 != data.nTermSubLevel2);
                        ValidateCount += _Count;
                    }

                    if (dbschool.TTerms.Count(c => c.nYear == f_term.nYear && c.cDel == null) < 2)
                    {
                        baseResult.StatusCode = "401";
                        baseResult.Message = @"ท่านต้องมีอย่างน้อย 2 เทอม จึงจะสามารถทำการเลื่อนชั้นได้
สามารถตรวจสอบได้ที่เมนูตั้งค่าปีการศึกษา";
                    }
                    else if (!leftData.ClassroomId.HasValue || !rightData.ClassroomId.HasValue)
                    {
                        baseResult.StatusCode = "401";
                        baseResult.Message = "กรุณาเลือกห้องเรียนที่ต้องการย้าย";
                    }
                    else if (f_term.nYear != f_term_Current.nYear && f_term.dStart < f_term_Current.dStart)
                    //else if (ValidateCount > 0)
                    {
                        baseResult.StatusCode = "401";
                        baseResult.Message = "ไม่สามารถทำการย้ายชั้นเรียนย้อนหลังได้";
                    }
                    else
                    {
                        var roomChange = new List<TRoomChange>();
                        var f_leftData = dbschool.TStudentClassroomHistories.Where(w => w.nTerm.Trim() == leftData.TermId.Trim() && w.nTermSubLevel2 == leftData.ClassroomId && w.cDel == false).ToList();
                        var f_rightData = dbschool.TStudentClassroomHistories.Where(w => w.nTerm.Trim() == rightData.TermId.Trim() && w.nTermSubLevel2 == rightData.ClassroomId && w.cDel == false).ToList();
                        var q_student = dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null && (w.nStudentStatus ?? 0) == 0).ToList();

                        var q_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nYear == f_term.nYear && w.dStart >= f_term.dStart).Select(s => s.nTerm);
                        var q_history = dbschool.TStudentClassroomHistories.Where(w => q_term.Contains(w.nTerm) && w.cDel == false).ToList();

                        foreach (var StudentId in leftData.StudentId)
                        {
                            var f_DTO = f_rightData.FirstOrDefault(f => f.sID == StudentId);
                            if (f_DTO != null)
                            {
                                foreach (var Trem_ID in q_term)
                                {
                                    var f_history = q_history.FirstOrDefault(f => f.sID == StudentId && f.nTerm == Trem_ID);
                                    if (f_history != null)
                                    {
                                        f_history.nTermSubLevel2 = leftData.ClassroomId;
                                    }

                                    if (f_term_Current.nTerm.Trim() == Trem_ID)
                                    {
                                        var f_student = q_student.FirstOrDefault(f => f.sID == StudentId);
                                        f_student.nTermSubLevel2 = leftData.ClassroomId;
                                    }
                                }

                                var newId = Guid.NewGuid().ToString();
                                roomChange.Add(new TRoomChange
                                {
                                    DayChange = dateChange,
                                    Level2New = leftData.ClassroomId,
                                    Level2Old = rightData.ClassroomId,
                                    sID = StudentId,
                                    RoomChangeID = newId,
                                    SchoolID = userData.CompanyID,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = userData.UserID
                                });

                                UserID.Add(StudentId);
                                f_DTO.nTermSubLevel2 = leftData.ClassroomId;
                            }
                        }

                        foreach (var StudentId in rightData.StudentId)
                        {
                            var f_DTO = f_leftData.FirstOrDefault(f => f.sID == StudentId);
                            if (f_DTO != null)
                            {
                                foreach (var Trem_ID in q_term)
                                {
                                    var f_history = q_history.FirstOrDefault(f => f.sID == StudentId && f.nTerm == Trem_ID);
                                    if (f_history != null)
                                    {
                                        f_history.nTermSubLevel2 = rightData.ClassroomId;
                                    }
                                    if (f_term_Current.nTerm.Trim() == Trem_ID)
                                    {
                                        var f_student = q_student.FirstOrDefault(f => f.sID == StudentId);
                                        f_student.nTermSubLevel2 = rightData.ClassroomId;
                                    }
                                }

                                var newId = Guid.NewGuid().ToString();
                                roomChange.Add(new TRoomChange
                                {
                                    DayChange = dateChange,
                                    Level2New = rightData.ClassroomId,
                                    Level2Old = leftData.ClassroomId,
                                    sID = StudentId,
                                    RoomChangeID = newId,
                                    SchoolID = userData.CompanyID,
                                    CreatedDate = DateTime.Now,
                                    CreatedBy = userData.UserID
                                });

                                UserID.Add(StudentId);
                                f_DTO.nTermSubLevel2 = rightData.ClassroomId;
                            }
                        }

                        if (roomChange.Count > 0) dbschool.TRoomChanges.AddRange(roomChange);
                        dbschool.SaveChanges();

                        baseResult.StatusCode = "200";
                        baseResult.Result = rightData;

                        Accounting.Tuitionfee.Setting.UpdateInvoices(UserID, f_term_Current.nTerm, userData.CompanyID);
                    }
                    return baseResult;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getDataHistory(StudentClassHistoryDTO historyDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    var q_data = (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                  join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                  where a.nTerm == historyDTO.TermId && a.nTermSubLevel2 == historyDTO.ClassroomId && (b.nStudentStatus ?? 0) == 0 && b.cDel == null
                                  orderby b.sStudentID
                                  select new
                                  {
                                      studentId = a.sID.Value,
                                      studentName = b.sStudentID + " " + b.sName + " " + b.sLastname
                                  }).ToList();

                    return new JavaScriptSerializer().Serialize(q_data);
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getStudentData(StudentClassHistoryDTO historyDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    var q_data = (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                                  join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                                  where a.nTerm == historyDTO.TermId && a.nTermSubLevel2 == historyDTO.ClassroomId
                                  && !historyDTO.StudentId.Contains(a.sID.Value) && (b.nStudentStatus ?? 0) == 0 && b.cDel == null
                                  orderby b.sStudentID
                                  select new
                                  {
                                      studentId = a.sID.Value,
                                      studentName = b.sStudentID + " " + b.sName + " " + b.sLastname
                                  }).ToList();

                    return new JavaScriptSerializer().Serialize(q_data);
                }
            }
        }

        //UP CLASS STUDENT
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object updateTerm(StudentClassHistoryDTO historyDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                //try
                //{
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                if (!historyDTO.ClassroomId.HasValue) return null;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    var f_term = new TTerm();
                    //string TermId = "";
                    var f_term_Current = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm == historyDTO.TermId);
                    if (f_term_Current == null)
                    {
                        f_term_Current = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => DateTime.Today <= f.dStart);
                    }

                    if (!string.IsNullOrEmpty(historyDTO.TermId))
                    {
                        //TermId = f_term.nTerm.Trim();
                        f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm == historyDTO.TermId);
                    }
                    else
                    {
                        f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart >= DateTime.Today);
                    }
                    if (dbschool.TTerms.Count(c => c.nYear == f_term.nYear && c.cDel == null) < 2)
                    {
                        baseResult.StatusCode = "401";
                        baseResult.Message = @"ท่านต้องมีอย่างน้อย 2 เทอม จึงจะสามารถทำการเลื่อนชั้นได้
สามารถตรวจสอบได้ที่เมนูตั้งค่าปีการศึกษา";
                    }
                    else if (!historyDTO.ClassroomId.HasValue)
                    {
                        baseResult.StatusCode = "401";
                        baseResult.Message = "กรุณาเลือกห้องเรียนที่ต้องการย้าย";
                    }
                    else
                    {
                        //if (f_term != null && historyDTO.TermId == f_term.nTerm.Trim())
                        //{
                        foreach (var studentData in (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                                     where historyDTO.StudentId.Contains(a.sID) && a.cDel == null && (a.nStudentStatus ?? 0) == 0 && a.cDel != "1"
                                                     select a))
                        {
                            studentData.nTermSubLevel2 = historyDTO.ClassroomId;
                        }
                        //}

                        ///GET TREM DATA
                        var q_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nYear == f_term.nYear).Select(s => s.nTerm);
                        var q_history = dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => q_term.Contains(w.nTerm)).ToList();
                        DateTime? MoveInDate = null, MoveOutDate = null;

                        var q_historyOld = dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false && historyDTO.StudentId.Contains(w.sID ?? 0)).ToList();

                        foreach (var sID in historyDTO.StudentId)
                        {
                            if (q_historyOld.Count(c => c.MoveInDate.HasValue) > 0)
                            {
                                var f1 = q_historyOld.OrderByDescending(o => o.MoveInDate).FirstOrDefault(f => f.sID == sID);
                                if (f1 != null)
                                {
                                    MoveInDate = f1.MoveInDate;
                                }
                            }

                            if (q_historyOld.Count(c => c.MoveOutDate.HasValue) > 0)
                            {
                                var f1 = q_historyOld.OrderByDescending(o => o.MoveOutDate).FirstOrDefault(f => f.sID == sID);
                                if (f1 != null)
                                {
                                    MoveOutDate = f1.MoveOutDate;
                                }
                            }

                            foreach (var trem_ID in q_term)
                            {
                                var f_history = q_history.FirstOrDefault(f => f.sID == sID && f.nTerm == trem_ID);
                                if (f_history == null)
                                {
                                    dbschool.TStudentClassroomHistories.Add(new TStudentClassroomHistory
                                    {
                                        sID = sID,
                                        nTerm = trem_ID,
                                        nTermSubLevel2 = historyDTO.ClassroomId,
                                        SchoolID = userData.CompanyID,
                                        MoveInDate = MoveInDate,
                                        MoveOutDate = MoveOutDate,
                                        CreatedDate = DateTime.Now.FixSecondAndMillisecond(6, 10),
                                        CreatedBy = userData.UserID,
                                        cDel = false
                                    });
                                }
                                else
                                {
                                    f_history.sID = sID;
                                    f_history.nTermSubLevel2 = historyDTO.ClassroomId;
                                    f_history.UpdatedDate = DateTime.Now.FixSecondAndMillisecond(6, 20);
                                    f_history.UpdatedBy = userData.UserID;
                                }
                            }

                            database.InsertLog(userData.UserID.ToString(), "เลื่อนระดับชั้นเรียน [รหัสนักเรียน: " + sID + ", ห้อง: " + historyDTO.ClassroomId + ", เทอม: " + historyDTO.TermId + "]", HttpContext.Current.Request, 133, 3, 0, userData.CompanyID);
                        }

                        using (PeakengineEntities peakengine = Connection.PeakengineEntities(ConnectionDB.Read))
                        {
                            var q_room = dbschool.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList();
                            var q_class = dbschool.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList();

                            string SubLevel2 = null, SubLevel = null;
                            int? nTermSubLevel2 = null, nTSubLevel = null;

                            var f_Room = dbschool.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == historyDTO.ClassroomId);
                            nTermSubLevel2 = f_Room.nTermSubLevel2;
                            SubLevel2 = f_Room.nTSubLevel2;

                            var f_Class = dbschool.TSubLevels.FirstOrDefault(f => f.nTSubLevel == f_Room.nTSubLevel);
                            nTSubLevel = f_Class.nTSubLevel;
                            SubLevel = f_Class.SubLevel;

                            //foreach (var sID in historyDTO.StudentId)
                            //{
                            //    //var qt = q_term.ToList();
                            //    //var q_invoice = peakengine.TInvoices.Where(w => w.student_id == sID && qt.Contains(w.nTerm)).ToList();

                            //    //foreach (var invoiceData in q_invoice)
                            //    //{
                            //    //    invoiceData.nTSubLevel = nTSubLevel;
                            //    //    invoiceData.SubLevel = SubLevel;

                            //    //    invoiceData.SubLevel2 = SubLevel2;
                            //    //    invoiceData.nTermSubLevel2 = nTermSubLevel2;

                            //    //    invoiceData.Fd_NewTermSubLevel = null;
                            //    //    invoiceData.Fd_NewTermSubLevel2 = null;
                            //    //    invoiceData.Fd_NewTermClass_id = null;
                            //    //    invoiceData.Fd_NewTermLevel_id = null;
                            //    //}
                            //}

                            //peakengine.SaveChanges();


                            //new accounting 
                            foreach (var sID in historyDTO.StudentId)
                            {
                                foreach (var trem_ID in q_term)
                                {
                                    var sql_invoices = $@"select b.AccountInvoiceStudentId,b.AccountInvoiceId,b.Year,b.Term,a.TermId, b.StudentId  from 
                                    AccountingDB.dbo.AccountInvoice a inner join AccountingDB.dbo.AccountInvoiceStudent b on a.AccountInvoiceId = b.AccountInvoiceId
                                    where a.schoolId={userData.CompanyID}  and b.StudentId={sID} and isnull(b.DeleteDate,'')='' and a.TermId='{trem_ID}'";
                                    var q_invoices = peakengine.Database.SqlQuery<AccountingInvoice>(sql_invoices).ToList();

                                    foreach (var f_invoices in q_invoices)
                                    {
                                        var sql_update_student = $@"update AccountingDB.dbo.AccountInvoiceStudent set                               
                                        LevelName='{SubLevel}',
                                        ClassName='{SubLevel} / {SubLevel2}',
                                        ClassId={nTermSubLevel2},
                                        UpdateDate=getdate(),
                                        UpdateBy='SB move class room' where AccountInvoiceStudentId={f_invoices.AccountInvoiceStudentId}";
                                        peakengine.Database.ExecuteSqlCommand(sql_update_student);

                                        var sql_update_invoice = $@"update AccountingDB.dbo.AccountInvoice set                                       
                                        ClassName='{SubLevel} / {SubLevel2}',
                                        LevelName='{SubLevel}',
                                        LevelId={nTSubLevel} where AccountInvoiceId={f_invoices.AccountInvoiceId}";
                                        peakengine.Database.ExecuteSqlCommand(sql_update_invoice);
                                    }
                                }
                            }
                        }

                        //Remove Data TStudentClassroomHistories
                        //foreach (var data in q_term)
                        //{
                        //    var q_Remove = dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID)
                        //        .Where(w => w.nTerm.Trim() == data.Trim() && w.nTermSubLevel2 == historyDTO.ClassroomId && !historyDTO
                        //    .StudentId.Contains(w.sID ?? 0)).ToList();

                        //    dbschool.TStudentClassroomHistories.RemoveRange(q_Remove);
                        //}

                        dbschool.SaveChanges();
                        //return historyDTO;
                        baseResult.StatusCode = "200";
                        baseResult.Result = historyDTO;
                    }
                }
                //}
                //catch (Exception e)
                //{
                //    baseResult.StatusCode = "500";
                //    baseResult.SystemErrorMessage = e;
                //}

                return baseResult;
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object getNextTerm(StudentClassHistoryDTO historyDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    var f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm.Trim() == historyDTO.TermId);
                    //var nextTerm = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart >= f_term.dEnd && f.cDel == null);
                    TM_Term nextTerm = dbschool.Database.SqlQuery<TM_Term>(String.Format("SELECT TOP 1 *  FROM TTerm WHERE  SchoolID = {0} AND dStart >= '{1:d}' AND cDel IS NULL ORDER BY dStart ", userData.CompanyID, f_term.dEnd)).FirstOrDefault();

                    if (nextTerm != null)
                    {
                        var f_year = dbschool.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).FirstOrDefault(f => f.nYear == nextTerm.nYear);
                        nextTerm.nTerm = nextTerm.nTerm;
                        nextTerm.nYear = f_year.numberYear;
                    }

                    return nextTerm;
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string GetHistoryUpClass(StudentClassHistoryDTO leftData, StudentClassHistoryDTO rightData)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //leftData.TermId = string.Format("TS{0:0000000}", int.Parse(leftData.TermId));
                    //rightData.TermId = string.Format("TS{0:0000000}", int.Parse(rightData.TermId));
                    var f_term = dbschool.TTerms.Where(w => w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.nTerm.Trim() == leftData.TermId);
                    //var nextTerm = dbschool.TTerms.Where(w=>w.SchoolID == userData.CompanyID).FirstOrDefault(f => f.dStart >= f_term.dEnd);

                    dynamic rss = new JObject();
                    var q_studentId = dbschool.TStudentClassroomHistories.Where(w => w.nTerm == rightData.TermId && w.cDel == false).Select(s => s.sID).ToList();

                    string SQL = String.Format(@"SELECT * FROM TB_StudentViews WHERE SchoolID = {0} AND nTerm = '{1}' AND nTermSubLevel2 = {2} 
AND sID NOT IN (SELECT sID FROM TB_StudentViews WHERE SchoolID = {0} AND nTerm = '{3}')  "
                        , userData.CompanyID, leftData.TermId, leftData.ClassroomId ?? 0, rightData.TermId, rightData.ClassroomId ?? 0);

                    var q_student_leftData = dbschool.Database.SqlQuery<TB_StudentViews>(SQL).ToList();

                    rss.leftData = new JArray(from a1 in (from a in q_student_leftData
                                                          where (a.nStudentStatus ?? 0) == 0 && a.cDel == null
                                                          orderby a.nStudentNumber, a.sStudentID
                                                          select new { a.sID, a.sName, a.sLastname, a.sStudentID }).ToList()
                                              select new JObject
                                              {
                                                  new JProperty("studentId",a1.sID),
                                                  new JProperty("studentName",a1.sStudentID + " " + a1.sName + " " + a1.sLastname)
                                              });

                    var q_student_rightData = dbschool.Database.SqlQuery<TB_StudentViews>(String.Format("SELECT * FROM TB_StudentViews WHERE SchoolID = {0} AND nTerm = '{1}' AND nTermSubLevel2 = {2}  ", userData.CompanyID, rightData.TermId, rightData.ClassroomId ?? 0)).ToList();
                    rss.rightData = new JArray(from a1 in (from a in q_student_rightData
                                                           where (a.nStudentStatus ?? 0) == 0 && a.cDel == null
                                                           orderby a.nStudentNumber, a.sStudentID
                                                           select new { a.sID, a.sName, a.sLastname, a.sStudentID }).ToList()
                                               select new JObject
                                              {
                                                  new JProperty("studentId",a1.sID),
                                                  new JProperty("studentName",a1.sStudentID + " " + a1.sName + " " + a1.sLastname)
                                              });

                    return rss.ToString();
                }
            }
        }

        //GRADUATE STUDENT
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static object updateGraduate(StudentClassHistoryDTO historyDTO, string dayGraduate, DateTime? dayProfessionalStandard)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                var _userData = dbmaster.TUsers.Where(w => w.nCompany == tCompany.nCompany && w.cType == "0").ToList();
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    DateTime graduateDate = DateTime.ParseExact(dayGraduate, "dd/MM/yyyy", new CultureInfo("th-TH"));
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    List<TStudentHIstory> studentHIstories = new List<TStudentHIstory>();
                    var f_term = dbschool.TTerms.FirstOrDefault(f => f.nTerm == historyDTO.TermId);
                    var q_history = dbschool.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == historyDTO.TermId && w.nTermSubLevel2_OLD == historyDTO.ClassroomId).AsQueryable().ToList();
                    var q_studentClassroomHistories = dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.nTerm == historyDTO.TermId && w.nTermSubLevel2 == historyDTO.ClassroomId).AsQueryable().ToList();
                    var q_historyNextTerm = dbschool.TB_StudentViews.Where(w => w.SchoolID == userData.CompanyID && w.dStart > f_term.dEnd && w.cDel == null).AsQueryable().ToList();

                    string StudentStatus = null;
                    foreach (var sID in historyDTO.StudentId)
                    {
                        var f_history = q_history.FirstOrDefault(f => f.sID == sID);
                        var f_studentClassroomHistories = q_studentClassroomHistories.FirstOrDefault(f => f.sID == sID);
                        if (q_historyNextTerm.Count(c => c.sID == sID) == 0) StudentStatus = "G";
                        else StudentStatus = null;

                        if (f_history == null)
                        {
                            var id = Guid.NewGuid().ToString();
                            studentHIstories.Add(new TStudentHIstory
                            {
                                DayAdd = graduateDate,
                                nTerm = historyDTO.TermId,
                                StudentStatus = StudentStatus,
                                nTermSubLevel2_OLD = historyDTO.ClassroomId,
                                sID = sID,
                                StudentHistory_ID = id,
                                dProfessionalStandard = dayProfessionalStandard,
                                SchoolID = userData.CompanyID,
                                CreatedDate = DateTime.Now,
                                CreatedBy = userData.UserID,
                                cDel = false
                            });

                            //if (f_studentClassroomHistories != null)
                            //{
                            //    f_studentClassroomHistories.nStudentStatus = 4;
                            //}
                        }
                        //else
                        //{

                        //}

                        if (f_studentClassroomHistories != null)
                        {
                            f_studentClassroomHistories.nStudentStatus = 4;
                        }
                    }

                    dbschool.TStudentHIstories.AddRange(studentHIstories);

                    foreach (var studentData in (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                                 where historyDTO.StudentId.Contains(a.sID) && a.cDel != "1"
                                                 select a))
                    {
                        if (q_historyNextTerm.Count(c => c.sID == studentData.sID) == 0) StudentStatus = "G";
                        else StudentStatus = null;

                        studentData.cDel = StudentStatus;
                    }

                    dbschool.SaveChanges();
                    foreach (var studentData in _userData.Where(w => historyDTO.StudentId.Contains(w.nSystemID ?? 0)))
                    {
                        if (q_historyNextTerm.Count(c => c.sID == studentData.sID) == 0) StudentStatus = "G";
                        else StudentStatus = null;

                        studentData.cDel = StudentStatus;
                    }

                    dbmaster.SaveChanges();

                    //Remove Data
                    q_history = dbschool.TStudentHIstories.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTerm == historyDTO.TermId && w.nTermSubLevel2_OLD == historyDTO.ClassroomId && !historyDTO.StudentId.Contains(w.sID ?? 0)).ToList();

                    var q_remove = q_history.Select(s => s.sID).ToList();

                    foreach (var studentData in (from a in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID)
                                                 where q_remove.Contains(a.sID) && a.cDel != "1"
                                                 select a))
                    {
                        studentData.cDel = null;
                    }

                    foreach (var studentData in (from a in dbmaster.TUsers.Where(w => w.nCompany == userData.CompanyID)
                                                 where q_remove.Contains(a.sID) && a.cDel != "1"
                                                 select a))
                    {
                        studentData.cDel = null;
                    }

                    foreach (var studentData in _userData.Where(w => q_remove.Contains(w.nSystemID ?? 0)))
                    {
                        studentData.cDel = null;
                    }

                    foreach (var data in dbschool.TStudentClassroomHistories.Where(w => q_remove.Contains(w.sID ?? 0) && w.cDel == false))
                    {
                        data.nStudentStatus = 0;
                        data.UpdatedDate = DateTime.Now;
                        data.UpdatedBy = userData.UserID;
                    }

                    dbschool.TStudentHIstories.RemoveRange(q_history);

                    dbschool.SaveChanges();
                    dbmaster.SaveChanges();

                    return new JavaScriptSerializer().Serialize(historyDTO);
                }
            }
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public static string historyGraduate(StudentClassHistoryDTO historyDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                string entities = HttpContext.Current.Session["sEntities"].ToString();
                var tCompany = dbmaster.TCompanies.FirstOrDefault(w => w.sEntities == entities);
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(tCompany,ConnectionDB.Read)))
                {
                    //historyDTO.TermId = string.Format("TS{0:0000000}", int.Parse(historyDTO.TermId));
                    dynamic rss = new JObject();

                    var q = (from a in dbschool.TStudentClassroomHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
                             join b in dbschool.TUser.Where(w => w.SchoolID == userData.CompanyID) on a.sID equals b.sID
                             where a.nTerm == historyDTO.TermId && a.nTermSubLevel2 == historyDTO.ClassroomId && b.cDel != "1"
                             orderby b.sStudentID
                             select new
                             {
                                 studentId = a.sID.Value,
                                 studentName = b.sStudentID + " " + b.sName + " " + b.sLastname,
                                 b.cDel,
                                 Student_Stats = a.nStudentStatus
                             }).ToList();

                    rss.leftData = new JArray(from a in q
                                              where (a.Student_Stats ?? 0) == 0
                                              select new JObject
                                              {
                                                  new JProperty("studentId", a.studentId),
                                                  new JProperty("studentName", a.studentName)
                                              });

                    rss.rightData = new JArray(from a in q
                                               where a.Student_Stats == 4
                                               select new JObject
                                              {
                                                  new JProperty("studentId", a.studentId),
                                                  new JProperty("studentName", a.studentName)
                                              });

                    return rss.ToString();
                }
            }
        }

        public class TM_Term
        {
            public string nTerm { get; set; }
            public Nullable<int> nYear { get; set; }
            public Nullable<int> numberTerm { get; set; }
            public string TermStatus { get; set; }
            public string cDel { get; set; }
            public string sTerm { get; set; }
            public Nullable<System.DateTime> dStart { get; set; }
            public Nullable<System.DateTime> dEnd { get; set; }
            public int SchoolID { get; set; }
            public Nullable<int> CreatedBy { get; set; }
            public Nullable<int> UpdatedBy { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<System.DateTime> UpdatedDate { get; set; }
        }
    }
}