using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Helper
{
    public class GradeViewSettingService
    {
        public static List<GradeViewSettingDTO> GetGradeViewSettingDetails(int schoolId)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var gradeViewSettingDetails = schoolDbContext.GetGradeViewSettingDetails(schoolId);
                if (gradeViewSettingDetails != null)
                {
                    var gradeViewSettingDTOs = (from v in gradeViewSettingDetails
                                                select new GradeViewSettingDTO
                                                {
                                                    GradeViewSettingId = v.GradeViewSettingId,
                                                    sTerm = v.sTerm,
                                                    IsAllRoomApproved = v.IsAllRoomApproved,
                                                    IsTermApproved = v.IsTermApproved,
                                                    nTerm = v.nTerm,
                                                    numberYear = v.numberYear,
                                                    nYear = v.nYear,
                                                    RowNumber = (int)v.RowNumber,
                                                    IsFinalTermApproved = v.IsFinalTermApproved,
                                                    IsMidTermApproved = v.IsMidTermApproved

                                                }).ToList();
                    return gradeViewSettingDTOs;
                }
                return null;
            }


        }

        public static CompanyDTO GetGradeViewFor100(int schoolId)
        {
            using (var masterDB = Connection.MasterEntities(ConnectionDB.Read))
            {
                return masterDB.TCompanies.Where(w => w.nCompany == schoolId).Select(s => new CompanyDTO { GradeViewFor100= s.GradeViewFor100,GradeViewAutoBlock= s.GradeViewAutoBlock }).FirstOrDefault();
            }
        }

        public static void UpdateGradeViewFor100(bool gradeViewFor100, int schoolId)
        {
            using (var masterDB = Connection.MasterEntities(ConnectionDB.Read))
            {
                var tcompany =  masterDB.TCompanies.Where(w => w.nCompany == schoolId).FirstOrDefault();
                tcompany.GradeViewFor100 = gradeViewFor100;
                masterDB.SaveChanges();
            }
        }

        public static void UpdateGradeViewAutoBlock(bool gradeViewAutoBlock, int schoolId)
        {
            using (var masterDB = Connection.MasterEntities(ConnectionDB.Read))
            {
                var tcompany = masterDB.TCompanies.Where(w => w.nCompany == schoolId).FirstOrDefault();
                tcompany.GradeViewAutoBlock = gradeViewAutoBlock;
                masterDB.SaveChanges();
            }
        }

        public static List<GradeViewRoomListSettingDTO> GetGradeViewRoomListSettingDetails(int schoolId, int gradeViewSettingId, int NTSubLevel)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var gradeViewRoomListSettingDetails = schoolDbContext.GetGradeViewRoomListSettingDetails(schoolId, gradeViewSettingId, NTSubLevel);
                if (gradeViewRoomListSettingDetails != null)
                {
                    int Srno = 0;
                    var gradeViewRoomListSettingDTOs = (from v in gradeViewRoomListSettingDetails
                                                        orderby v.nTSubLevel2
                                                        select new GradeViewRoomListSettingDTO
                                                        {
                                                            RowNumber = ++Srno,
                                                            GradeViewSettingId = v.GradeViewSettingId,
                                                            IsRoomBlocked = (v.IsRoomBlocked == 1) ? true : false,
                                                            SubLevel = v.SubLevel,
                                                            NTSubLevel = v.nTSubLevel,
                                                            NTermSubLevel2 = v.nTermSubLevel2,
                                                            nTSubLevel2 = v.nTSubLevel2,
                                                            RoomListSettingId = v.RoomListSettingId ?? 0,
                                                            
                                                        }).ToList();

                    return gradeViewRoomListSettingDTOs;
                }
                return null;
            }

        }

        public static void UpdateFinalTermApprovedStatus(GradeViewSettingRequest gradeViewSettingRequest)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (gradeViewSettingRequest != null)
                {
                    var sEmp = Utils.GetUserId();
                    var dateTime = System.DateTime.Now;
                    if (gradeViewSettingRequest.GradeViewSettingId != null && gradeViewSettingRequest.GradeViewSettingId > 0)
                    {
                        var tGradeViewSetting = schoolDbContext.TGradeViewSettings.FirstOrDefault(w => w.GradeViewSettingId == gradeViewSettingRequest.GradeViewSettingId);
                        if (tGradeViewSetting != null)
                        {
                            tGradeViewSetting.IsTermApproved = gradeViewSettingRequest.IsTermApproved;
                            tGradeViewSetting.IsFinalTermApproved = gradeViewSettingRequest.IsFinalTermApproved;
                            if (tGradeViewSetting.IsMidTermApproved == false)
                            {
                                tGradeViewSetting.IsMidTermApproved = (gradeViewSettingRequest.IsFinalTermApproved) ? true : false;
                            }
                            tGradeViewSetting.UpdatedBy = sEmp;
                            tGradeViewSetting.UpdatedDate = dateTime;
                            schoolDbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        var schoolId = Utils.GetSchoolId();
                        if (schoolDbContext.TGradeViewSettings.Where(w => w.nTerm == gradeViewSettingRequest.nTerm && w.SchoolId == schoolId).FirstOrDefault() == null)
                        {
                            schoolDbContext.TGradeViewSettings.Add(new TGradeViewSetting()
                            {
                                ApprovedBy = sEmp,
                                ApprovedDate = dateTime,
                                IsAllRoomApproved = false,
                                IsTermApproved = gradeViewSettingRequest.IsTermApproved,
                                IsFinalTermApproved = gradeViewSettingRequest.IsFinalTermApproved,
                                IsMidTermApproved = (gradeViewSettingRequest.IsFinalTermApproved) ? true : false,
                                nTerm = gradeViewSettingRequest.nTerm,
                                SchoolId = Utils.GetSchoolId(),
                                UpdatedBy = sEmp,
                                UpdatedDate = dateTime,
                                CreatedBy = sEmp,
                                CreatedDate = dateTime

                            }); ;
                            schoolDbContext.SaveChanges();
                        }
                    }

                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "",string.Format("ApprovedStatus:{0},nTerm:{1}, EmpId {2}", gradeViewSettingRequest.IsTermApproved, gradeViewSettingRequest.nTerm, HttpContext.Current.Session["sEmpID"]), HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 25, 2, 0);
                }
            }
        }

        public static void UpdateMidTermApprovedStatus(GradeViewSettingRequest gradeViewSettingRequest)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (gradeViewSettingRequest != null)
                {
                    var sEmp = Utils.GetUserId();
                    var dateTime = System.DateTime.Now;
                    if (gradeViewSettingRequest.GradeViewSettingId != null && gradeViewSettingRequest.GradeViewSettingId > 0)
                    {
                        var tGradeViewSetting = schoolDbContext.TGradeViewSettings.FirstOrDefault(w => w.GradeViewSettingId == gradeViewSettingRequest.GradeViewSettingId);
                        if (tGradeViewSetting != null)
                        {
                            tGradeViewSetting.IsMidTermApproved = gradeViewSettingRequest.IsMidTermApproved;
                            tGradeViewSetting.UpdatedBy = sEmp;
                            tGradeViewSetting.UpdatedDate = dateTime;
                            schoolDbContext.SaveChanges();
                        }
                    }
                    else
                    {
                        var schoolId = Utils.GetSchoolId();
                        if (schoolDbContext.TGradeViewSettings.Where(w => w.nTerm == gradeViewSettingRequest.nTerm && w.SchoolId == schoolId).FirstOrDefault() == null)
                        {
                            schoolDbContext.TGradeViewSettings.Add(new TGradeViewSetting()
                            {
                                ApprovedBy = sEmp,
                                ApprovedDate = dateTime,
                                IsAllRoomApproved = false,
                                IsTermApproved = gradeViewSettingRequest.IsTermApproved,
                                nTerm = gradeViewSettingRequest.nTerm,
                                SchoolId = Utils.GetSchoolId(),
                                UpdatedBy = sEmp,
                                UpdatedDate = dateTime,
                                CreatedBy = sEmp,
                                CreatedDate = dateTime,
                                IsMidTermApproved = gradeViewSettingRequest.IsMidTermApproved

                            });
                            schoolDbContext.SaveChanges();
                        }
                    }

                    database.InsertLog(HttpContext.Current.Session["sEmpID"] + "", string.Format("Mid Term ApprovedStatus:{0},nTerm:{1}, EmpId {2}", gradeViewSettingRequest.IsMidTermApproved, gradeViewSettingRequest.nTerm, HttpContext.Current.Session["sEmpID"]), HttpContext.Current.Session["sEntities"].ToString(), HttpContext.Current.Request, 25, 2, 0);
                }
            }
        }

        public static void GradeViewRoomListBlock(List<GradeViewRoomListBlockRequest> requests)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
               
                foreach (var request in requests.Where(w => w.RoomListSettingId > 0))
                {
                    var roomlist = schoolDbContext.TGradeViewRoomListSettings.Where(w => w.GradeViewSettingId == request.GradeViewSettingId && w.RoomListSettingId == request.RoomListSettingId && w.SchoolID == request.SchoolId).FirstOrDefault();
                    if (roomlist != null)
                    {
                        roomlist.IsRoomBlocked = request.IsRoomBlocked;
                        roomlist.UpdatedBy = request.UpdatedBy;
                        roomlist.UpdatedDate = request.UpdatedDate;

                    }
                }
                var roomsList = (from r in requests.Where(w => w.RoomListSettingId == 0 && w.IsRoomBlocked == true)
                                 select new TGradeViewRoomListSetting
                                 {
                                     GradeViewSettingId = r.GradeViewSettingId ?? 0,
                                     SchoolID = r.SchoolId,
                                     nTermSubLevel2 = r.NTermSubLevel2,
                                     IsRoomBlocked = r.IsRoomBlocked,
                                     CreatedBy = r.CreatedBy,
                                     CreatedDate = r.CreatedDate,
                                 }).ToList();

                if (roomsList != null && roomsList.Count > 0 && requests != null)
                {
                    var gradeViewSetting = new TGradeViewSetting();
                    var schoolId = Utils.GetSchoolId();
                    if (roomsList.Where(w => w.GradeViewSettingId == 0).Count() > 0 )
                    {
                        var sEmp = Utils.GetUserId();
                        var dateTime = System.DateTime.Now;
                        gradeViewSetting = schoolDbContext.TGradeViewSettings.Where(w => w.SchoolId == schoolId && w.nTerm == requests[0].NTerm).FirstOrDefault();

                        if (gradeViewSetting == null)
                        {
                            gradeViewSetting = new TGradeViewSetting()
                            {
                                ApprovedBy = sEmp,
                                ApprovedDate = dateTime,
                                IsAllRoomApproved = false,
                                IsTermApproved = false,
                                nTerm = requests[0].NTerm,
                                SchoolId = schoolId,
                                UpdatedBy = sEmp,
                                UpdatedDate = dateTime,
                                CreatedBy = sEmp,
                                CreatedDate = dateTime,
                                IsMidTermApproved = false

                            };
                            schoolDbContext.TGradeViewSettings.Add(gradeViewSetting);
                        }
                        schoolDbContext.SaveChanges();

                        if (gradeViewSetting.GradeViewSettingId > 0)
                        {
                            roomsList.ForEach(f => f.GradeViewSettingId = gradeViewSetting.GradeViewSettingId);
                            schoolDbContext.TGradeViewRoomListSettings.AddRange(roomsList);
                        }
                    }
                    else
                    {
                        schoolDbContext.TGradeViewRoomListSettings.AddRange(roomsList);
                    }
                    
                }

                schoolDbContext.SaveChanges();


            }
        }

        public static List<GradeViewStudentBlockSettingDTO> GetStudentsForGradeViewBlock(int schoolId, int gradeViewSettingId, int NTSubLevel, int nTermSubLevel2, string nTerm)
        {
            try
            {
                using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var students = (from s in schoolDbContext.TB_StudentViews.Where(c => c.SchoolID == schoolId  && c.nTerm == nTerm && c.nTermSubLevel2 == nTermSubLevel2 && (c.cDel ?? "0") != "1")
                                    join u in schoolDbContext.TUser.Where(w => w.SchoolID == schoolId) on s.sID equals u.sID
                                    orderby s.nStudentNumber ?? 9999
                                    select new StudentDTO
                                    {
                                        SId = s.sID,
                                        SName = s.sName,
                                        SLastname = s.sLastname,
                                        SStudentTitle = s.titleDescription,
                                        STerm = s.sTerm,
                                        SStudentId = s.sStudentID,
                                        NStudentNumber = s.nStudentNumber,
                                        SStudentNameEn = u.sStudentNameEN,
                                        SStudentLastEn = u.sStudentLastEN,
                                        NStudentStatus = s.nStudentStatus,
                                        NTerm = s.nTerm,
                                    }).Distinct().AsQueryable().ToList();

                    var blockedStudents = schoolDbContext.TGradeViewStudentBlockListSettings.Where(w => w.SchoolID == schoolId && w.GradeViewSettingId == gradeViewSettingId).DistinctBy(d => d.sID).ToList();
                    int Srno = 0;
                    var gradeviewblockstudents = (from s in students
                                                  join bs in blockedStudents on s.SId equals bs.sID into bl
                                                  orderby s.NStudentNumber ?? 9999
                                                  from bls in bl.DefaultIfEmpty()
                                                  select new GradeViewStudentBlockSettingDTO
                                                  {
                                                      GradeViewSettingId = bls?.GradeViewSettingId ?? 0,
                                                      SId = s.SId,
                                                      SLastname = s.SLastname,
                                                      SName = s.SName,
                                                      SStudentId = s.SStudentId,
                                                      SStudentTitle = s.SStudentTitle,
                                                      StudentBlockListSettingId = bls?.StudentBlockListSettingId ?? 0,
                                                      IsStudentBlocked = bls?.IsStudentBlocked ?? false,
                                                      RowNumber = ++Srno,
                                                      NStudentNumber = s.NStudentNumber ?? 9999
                                                  }).ToList();
                    return gradeviewblockstudents.OrderBy(o => o.NStudentNumber).ToList();


                }
            }
            catch(Exception ex)
            {
                return new List<GradeViewStudentBlockSettingDTO>();
            }
        }

        public static void GradeViewStudentBlock(List<GradeViewStudentBlockRequest> requests)
        {
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                foreach (var request in requests.Where(w => w.StudentBlockListSettingId > 0))
                {
                    var studentlist = schoolDbContext.TGradeViewStudentBlockListSettings.Where(w => w.GradeViewSettingId == request.GradeViewSettingId && w.StudentBlockListSettingId == request.StudentBlockListSettingId && w.SchoolID == request.SchoolId && w.sID == request.SId).FirstOrDefault();
                    if (studentlist != null)
                    {
                        studentlist.IsStudentBlocked = request.IsStudentBlocked;
                        studentlist.UpdatedBy = request.UpdatedBy;
                        studentlist.UpdatedDate = request.UpdatedDate;

                    }
                }
                var studentsList = (from r in requests.Where(w => w.StudentBlockListSettingId == 0 && w.IsStudentBlocked == true)
                                 select new TGradeViewStudentBlockListSetting
                                 {
                                     GradeViewSettingId = r.GradeViewSettingId ?? 0,
                                     SchoolID = r.SchoolId,
                                     //nTermSubLevel2 = r.NTermSubLevel2,
                                     sID = r.SId,
                                     
                                     IsStudentBlocked = r.IsStudentBlocked,
                                     CreatedBy = r.CreatedBy,
                                     CreatedDate = r.CreatedDate,
                                     UpdatedBy = r.UpdatedBy,
                                     UpdatedDate = r.UpdatedDate,
                                 }).ToList();

                if (studentsList != null && studentsList.Count > 0)
                {

                    var gradeViewSetting = new TGradeViewSetting();
                    var schoolId = Utils.GetSchoolId();
                    if (studentsList.Where(w => w.GradeViewSettingId == 0).Count() > 0)
                    {
                        var sEmp = Utils.GetUserId();
                        var dateTime = System.DateTime.Now;
                        //gradeViewSetting = schoolDbContext.TGradeViewSettings.Where(w => w.nTerm == requests[0].NTerm && w.SchoolId == schoolId).FirstOrDefault();

                        var sqlQuery = string.Format("select * from TGradeViewSetting where nTerm = '{0}' and SchoolId = {1}", requests[0].NTerm, schoolId);
                        gradeViewSetting = schoolDbContext.Database.SqlQuery<TGradeViewSetting>(sqlQuery).FirstOrDefault();

                        

                        if (gradeViewSetting == null)
                        {
                            gradeViewSetting = new TGradeViewSetting()
                            {
                                ApprovedBy = sEmp,
                                ApprovedDate = dateTime,
                                IsAllRoomApproved = false,
                                IsTermApproved = false,
                                nTerm = requests[0].NTerm,
                                SchoolId = schoolId,
                                UpdatedBy = sEmp,
                                UpdatedDate = dateTime,
                                CreatedBy = sEmp,
                                CreatedDate = dateTime,
                                IsMidTermApproved = false

                            };
                            schoolDbContext.TGradeViewSettings.Add(gradeViewSetting);
                            schoolDbContext.SaveChanges();
                        }

                        if (gradeViewSetting.GradeViewSettingId > 0)
                        {
                            studentsList.ForEach(f => f.GradeViewSettingId = gradeViewSetting.GradeViewSettingId);
                            schoolDbContext.TGradeViewStudentBlockListSettings.AddRange(studentsList);
                        }
                    }
                    else
                    {
                        schoolDbContext.TGradeViewStudentBlockListSettings.AddRange(studentsList);
                    }
                                       
                }

                schoolDbContext.SaveChanges();


            }
        }
    }
}