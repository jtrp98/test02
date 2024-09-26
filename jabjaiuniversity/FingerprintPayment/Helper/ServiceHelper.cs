using FingerprintPayment.Class;
using FingerprintPayment.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using PostgreSQL;
using SchoolBright.Business.Interfaces;
using SchoolBright.DTO.DTO;
using SchoolBright.DTO.Parameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

//using SchoolBright.DataAccess;

namespace FingerprintPayment.Helper
{
    public class ServiceHelper
    {
        public static ILoginService LoginService { get; set; }

        public static IPlanService PlanService { get; set; }

        public static ICommonService CommonService { get; set; }

        public static IGraduationService GraduationService { get; set; }

        public static UserDTO ValidateUser(string userId, string passWord)
        {
            return LoginService.ValidateUser(userId, passWord);
        }

        public static string GetAntiXsrfToken()
        {
            JWTToken token = new JWTToken();
            var tokenValue = string.Empty;
            if (token.CheckToken(HttpContext.Current)) { tokenValue = token.GetToken(HttpContext.Current); }

            return tokenValue;
        }
        public static TermDTO GetTerm(string sTerm, int nYear)
        {
            return CommonService.GetTerm(sTerm, nYear, Utils.GetSchoolId(), Utils.GetUserId());
        }

        public static List<YearDTO> GetYears(string entities, int schoolid, int sEmp)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                try
                {
                    return (from y in schoolDbContext.TYears.Where(w => w.SchoolID == userData.CompanyID)
                            select new YearDTO
                            {
                                NumberYear = y.numberYear,
                                NYear = y.nYear,
                                YearStatus = y.YearStatus,
                            }).ToList();
                }
                catch (Exception ex)
                {


                    return new List<YearDTO>();

                }
            }
        }

        public static object GetCompanyInfo(int schoolid)
        {
            //JWTToken token = new JWTToken();
            //var userData = new JWTToken().UserData;
            //if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                return (from y in dbmaster.TCompanies.Where(w => w.nCompany == schoolid)
                        select new
                        {
                            CompanyName = y.sCompany
                        }).FirstOrDefault();
            }

        }

        public static string GetClassLevel(int? nTermSubLevel2)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    return (from t in schoolDbContext.TTermSubLevel2.Where(c => c.nTermSubLevel2 == nTermSubLevel2 && c.SchoolID == userData.CompanyID)
            //            join s in schoolDbContext.TSubLevels on t.nTSubLevel equals s.nTSubLevel
            //            join l in schoolDbContext.TLevels on s.nTLevel equals l.LevelID
            //            select l.LevelName).FirstOrDefault();
            //}
            return CommonService.GetClassLevel(nTermSubLevel2, userData.CompanyID);
        }

        public static int GetMathayomRange(string subLevel)
        {
            var mathayomRange1 = new List<string> { "ม.1", "ม.2", "ม.3" };
            var mathayomRange2 = new List<string> { "ม.4", "ม.5", "ม.6" };
            if (mathayomRange1.Contains(subLevel))
            {
                return 1;
            }
            else if (mathayomRange2.Contains(subLevel))
            {
                return 2;
            }
            return 0;
        }

        public static List<PrimaryLevelEducationDTO> GetPrimaryLevelEducations()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    var primaryLevelEducations = (from a in schoolDbContext.TSubLevels.Where(w => w.SchoolID == userData.CompanyID)
            //                                  join b in schoolDbContext.TLevels.Where(w => w.SchoolID == userData.CompanyID) on a.nTLevel equals b.LevelID
            //                                  where a.nWorkingStatus == 1
            //                                  orderby new { b.sortValue, a.SubLevel }
            //                                  select new PrimaryLevelEducationDTO
            //                                  {
            //                                      NTSubLevel = a.nTSubLevel,
            //                                      SubLevel = a.SubLevel
            //                                  }).ToList();

            //    return primaryLevelEducations;
            //}
            return CommonService.GetPrimaryLevelEducations(userData.CompanyID, userData.UserID);
        }

        public static System.Web.UI.WebControls.ListItem[] GetCourseGroup()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    var courseGroup = (from a in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == userData.CompanyID)
            //                       select new ListItem
            //                       {
            //                           Text = a.Description,
            //                           Value = a.courseGroupId.ToString()
            //                       }).ToArray();

            //    return courseGroup;
            //}

            return CommonService.GetCourseGroup(userData.CompanyID);
        }

        public static System.Web.UI.WebControls.ListItem[] GetCourseType()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            //using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            //{
            //    var courseType = (from a in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID)
            //                      orderby a.nOrder
            //                      select new ListItem
            //                      {
            //                          Text = a.Description,
            //                          Value = a.courseTypeId.ToString()
            //                      }).ToArray();

            //    return courseType;
            //}
            return CommonService.GetCourseType(userData.CompanyID);
        }

        //public static TGrade GetTGradeInfo(string nTerm, int schoolId, int sPlaneId, int? nTermSubLevel2, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    TGrade gradedata = null;


        //    if (isRequestForCurrentAcademicYear)
        //    {

        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneId && w.nTermSubLevel2 == nTermSubLevel2 && w.SchoolID == schoolId).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneId && w.nTermSubLevel2 == nTermSubLevel2 && w.SchoolID == schoolId).FirstOrDefault();
        //            }
        //        }
        //        //Check History table
        //        if (gradedata == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && w.sPlaneID == sPlaneId).FirstOrDefault();
        //                gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && w.sPlaneID == sPlaneId).FirstOrDefault();
        //            gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //        }

        //        //Check Main Table
        //        if (gradedata == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneId && w.nTermSubLevel2 == nTermSubLevel2 && w.SchoolID == schoolId).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneId && w.nTermSubLevel2 == nTermSubLevel2 && w.SchoolID == schoolId).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static TGrade GetTGradeInfo(int nGradeId, int schoolId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    TGrade gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //            }
        //        }

        //        //Check History table
        //        if (gradedata == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //                gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //            gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //        }

        //        //Check main table
        //        if (gradedata == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static List<TGrade> GetTGradeInfos(string nTerm, int schoolId, List<int?> sPlaneId, int? nTermSubLevel2, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGrade> gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //            }
        //        }
        //        else
        //        {


        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();
        //            }
        //        }
        //        //Check history table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();
        //                gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();
        //            gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //        }

        //        //Check main table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && sPlaneId.Contains(w.sPlaneID)).ToList();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static TGrade GetTGradeInfoExistForRoom(string nTerm, int schoolId, int? nTermSubLevel2, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    TGrade gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
        //            }
        //        }

        //        //Check history table
        //        if (gradedata == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
        //                gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
        //            gradedata = iMapper.Map<GradeHistoryEntity.TGradeHistory, TGrade>(gradedataHistory);
        //        }

        //        //Check Main Table
        //        if (gradedata == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGrade(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}



        //public static List<TGrade> GetTGradeInfoByRoom(string nTerm, int schoolId, int? nTermSubLevel2, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGrade> gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();
        //            }
        //        }
        //        //Check History table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();
        //                gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();
        //            gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //        }
        //        //Check Main Table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && (w.nTermSubLevel2 == nTermSubLevel2 || w.nTermSubLevel2 == null)).ToList();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static List<TGrade> GetTGradeInfoByRoomBothTerm(string nTerm, string nTerm2, int schoolId, int? nTermSubLevel2, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGrade> gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();
        //            }
        //        }
        //        //Check History table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();
        //                gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();
        //            gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //        }
        //        //Check Main Table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2) && w.nTermSubLevel2 == nTermSubLevel2).ToList();
        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static List<TGrade> GetTGradeInfoByRoomBothTerm(string nTerm, string nTerm2, int schoolId, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGrade> gradedata = null;

        //    IMapper iMapper = GetMapper();
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2)).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2)).ToList();
        //            }
        //        }

        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && (w.nTerm == nTerm || w.nTerm == nTerm2)).ToList();
        //            gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //        }

        //    }
        //    return gradedata;
        //}

        //public static List<TGrade> GetTGradeInfoBySId(string nTerm, int schoolId, int sId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGrade> gradedata = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {

        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var gradeList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                                 join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                 select g).ToList();

        //                gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradedata = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                             join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                             select g).ToList();

        //            }
        //        }
        //        //Check History table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradedataHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                                        join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                        select g).ToList();
        //                gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradedataHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                                    join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                    select g).ToList();
        //            gradedata = iMapper.Map<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>(gradedataHistory);
        //        }
        //        //Check Main table
        //        if (gradedata == null || (gradedata != null && gradedata.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var gradeList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                                     join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                     select g).ToList();

        //                    gradedata = SchoolBright.DataAccess.DataAccessHelper.GetTGradeFromPGTGradeList(gradeList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradedata = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm)
        //                                 join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                 select g).ToList();

        //                }
        //            }
        //        }
        //    }
        //    return gradedata;
        //}

        //public static List<GetGradeDetailView_Result> GetGradeDetailView(int schoolId, int nGradeId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<GetGradeDetailView_Result> gradeDetail = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                gradeDetail = GetSQGradeDetailView(schoolId, nGradeId, 0);
        //            }
        //            else
        //            {
        //                gradeDetail = _dbGrade.GetGradeDetailView(schoolId, nGradeId, 0).ToList();
        //            }

        //            //Check Any Imported Data available for this student
        //            if (gradeDetail == null)
        //            {
        //                var tgrade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //                if (tgrade != null)
        //                {
        //                    tgrade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == tgrade.nTerm && w.sPlaneID == tgrade.sPlaneID && w.nTermSubLevel2 == null).FirstOrDefault();
        //                    if (tgrade != null)
        //                    {
        //                        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                        {
        //                            gradeDetail = GetSQGradeDetailView(schoolId, tgrade.nGradeId, 0);
        //                        }
        //                        else
        //                        {
        //                            gradeDetail = _dbGrade.GetGradeDetailView(schoolId, tgrade.nGradeId, 0).ToList();
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        //Check History table
        //        if (gradeDetail == null || (gradeDetail != null && gradeDetail.Count() == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, nGradeId, 0).ToList();
        //                gradeDetail = iMapper.Map<List<GradeHistoryEntity.GetGradeDetailViewHistory_Result>, List<GetGradeDetailView_Result>>(gradeDetailHistory);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, nGradeId, 0).ToList();
        //            gradeDetail = iMapper.Map<List<GradeHistoryEntity.GetGradeDetailViewHistory_Result>, List<GetGradeDetailView_Result>>(gradeDetailHistory);

        //            //Check Any Imported Data available for this student
        //            if (gradeDetail == null)
        //            {
        //                var tgrade = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //                if (tgrade != null)
        //                {
        //                    tgrade = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == tgrade.nTerm && w.sPlaneID == tgrade.sPlaneID && w.nTermSubLevel2 == null).FirstOrDefault();
        //                    if (tgrade != null)
        //                    {
        //                        gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, nGradeId, 0).ToList();
        //                        gradeDetail = iMapper.Map<List<GradeHistoryEntity.GetGradeDetailViewHistory_Result>, List<GetGradeDetailView_Result>>(gradeDetailHistory);
        //                    }
        //                }
        //            }
        //        }

        //        // check main table
        //        if (gradeDetail == null || (gradeDetail != null && gradeDetail.Count() == 0))
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    gradeDetail = GetSQGradeDetailView(schoolId, nGradeId, 0);
        //                }
        //                else
        //                {
        //                    gradeDetail = _dbGrade.GetGradeDetailView(schoolId, nGradeId, 0).ToList();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetail;
        //}

        //public static GetGradeDetailView_Result GetGradeDetailViewBySid(int schoolId, string nTerm, int sPlaneId, int sId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    GetGradeDetailView_Result gradeDetail = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {


        //            var tgrade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                          join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                          select g).FirstOrDefault();
        //            if (tgrade != null)
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    gradeDetail = GetSQGradeDetailViewBySid(schoolId, tgrade.nGradeId, sId);
        //                }
        //                else
        //                {
        //                    gradeDetail = _dbGrade.GetGradeDetailView(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //                }
        //            }


        //        }

        //        //Check History Table
        //        if (gradeDetail == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {

        //                //Check Any Imported Data available for this student
        //                var tgrade = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                              join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                              select g).FirstOrDefault();
        //                if (tgrade != null)
        //                {
        //                    var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //                    gradeDetail = iMapper.Map<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>(gradeDetailHistory);
        //                }


        //            }
        //        }


        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgrade = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                          join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                          select g).FirstOrDefault();
        //            if (tgrade != null)
        //            {
        //                var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //                gradeDetail = iMapper.Map<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>(gradeDetailHistory);
        //            }
        //        }

        //        //Check Main Table
        //        if (gradeDetail == null)
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                var tgrade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                              join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                              select g).FirstOrDefault();
        //                if (tgrade != null)
        //                {
        //                    if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                    {
        //                        gradeDetail = GetSQGradeDetailViewBySid(schoolId, tgrade.nGradeId, sId);
        //                    }
        //                    else
        //                    {
        //                        gradeDetail = _dbGrade.GetGradeDetailView(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetail;
        //}



        //public static GetGradeDetailView_Result GetGradeDetailViewBySid(int schoolId, int nGradeId, int sId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    GetGradeDetailView_Result gradeDetail = null;

        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            if (nGradeId > 0)
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    gradeDetail = GetSQGradeDetailViewBySid(schoolId, nGradeId, sId);
        //                }
        //                else
        //                {
        //                    gradeDetail = _dbGrade.GetGradeDetailView(schoolId, nGradeId, sId).FirstOrDefault();
        //                }
        //            }

        //            ////Check Any Imported Data available for this student
        //            //if (gradeDetail == null && nGradeId == 0)
        //            //{
        //            //    var tgrade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //            //    if (tgrade != null)
        //            //    {
        //            //        tgrade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == tgrade.nTerm && w.sPlaneID == tgrade.sPlaneID && w.nTermSubLevel2 == null).FirstOrDefault();
        //            //        if (tgrade != null)
        //            //        {
        //            //            gradeDetail = _dbGrade.GetGradeDetailView(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //            //        }
        //            //    }
        //            //}
        //        }

        //        //Check History Table
        //        if (gradeDetail == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, nGradeId, sId).FirstOrDefault();
        //                gradeDetail = iMapper.Map<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>(gradeDetailHistory);
        //            }
        //        }


        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, nGradeId, sId).FirstOrDefault();
        //            gradeDetail = iMapper.Map<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>(gradeDetailHistory);

        //            ////Check Any Imported Data available for this student
        //            //if (gradeDetail == null && nGradeId == 0)
        //            //{
        //            //    var tgrade = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).FirstOrDefault();
        //            //    if (tgrade != null)
        //            //    {
        //            //        tgrade = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == tgrade.nTerm && w.sPlaneID == tgrade.sPlaneID && w.nTermSubLevel2 == null).FirstOrDefault();
        //            //        if (tgrade != null)
        //            //        {
        //            //            gradeDetailHistory = _dbGradeHistory.GetGradeDetailViewHistory(schoolId, tgrade.nGradeId, sId).FirstOrDefault();
        //            //            gradeDetail = iMapper.Map<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>(gradeDetailHistory);
        //            //        }
        //            //    }
        //            //}
        //        }

        //        //Check Main Table
        //        if (gradeDetail == null)
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    gradeDetail = GetSQGradeDetailViewBySid(schoolId, nGradeId, sId);
        //                }
        //                else
        //                {
        //                    gradeDetail = _dbGrade.GetGradeDetailView(schoolId, nGradeId, sId).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetail;
        //}

        //public static TB_GradeViews GetTBGradeViews(string nTerm, int sPlaneID, int? nTermSubLevel2, int schoolId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{

        //    TB_GradeViews tB_GradeViews = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            tB_GradeViews = GetSQGradeViewsByID(schoolId, nTerm, sPlaneID, nTermSubLevel2);
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                tB_GradeViews = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && (nTermSubLevel2 == 0 || w.nTermSubLevel2 == nTermSubLevel2) && w.SchoolID == schoolId).FirstOrDefault();
        //            }
        //        }

        //        if (tB_GradeViews == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tB_GradeViewsHistory = _dbGradeHistory.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && (nTermSubLevel2 == 0 || w.nTermSubLevel2 == nTermSubLevel2) && w.SchoolID == schoolId).FirstOrDefault(); ;
        //                tB_GradeViews = iMapper.Map<GradeHistoryEntity.TB_GradeViews, TB_GradeViews>(tB_GradeViewsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tB_GradeViewsHistory = _dbGradeHistory.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && (nTermSubLevel2 == 0 || w.nTermSubLevel2 == nTermSubLevel2) && w.SchoolID == schoolId).FirstOrDefault(); ;
        //            tB_GradeViews = iMapper.Map<GradeHistoryEntity.TB_GradeViews, TB_GradeViews>(tB_GradeViewsHistory);
        //        }

        //        if (tB_GradeViews == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                tB_GradeViews = GetSQGradeViewsByID(schoolId, nTerm, sPlaneID, nTermSubLevel2);
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    tB_GradeViews = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && (nTermSubLevel2 == 0 || w.nTermSubLevel2 == nTermSubLevel2) && w.SchoolID == schoolId).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }
        //    return tB_GradeViews;
        //}

        //public static List<TB_GradeViews> GetTBGradeViews(string nTerm, int sPlaneID, int schoolId, IMapper iMapper, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TB_GradeViews> tB_GradeViews = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            tB_GradeViews = GetSQGradeViews(schoolId, nTerm, sPlaneID);
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                tB_GradeViews = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && w.SchoolID == schoolId).ToList();
        //            }
        //        }

        //        //Check history table
        //        if (tB_GradeViews == null || (tB_GradeViews != null && tB_GradeViews.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tB_GradeViewsHistory = _dbGradeHistory.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && w.SchoolID == schoolId).ToList();
        //                tB_GradeViews = iMapper.Map<List<GradeHistoryEntity.TB_GradeViews>, List<TB_GradeViews>>(tB_GradeViewsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tB_GradeViewsHistory = _dbGradeHistory.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && w.SchoolID == schoolId).ToList();
        //            tB_GradeViews = iMapper.Map<List<GradeHistoryEntity.TB_GradeViews>, List<TB_GradeViews>>(tB_GradeViewsHistory);
        //        }

        //        //Check Main table
        //        if (tB_GradeViews == null || (tB_GradeViews != null && tB_GradeViews.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                tB_GradeViews = GetSQGradeViews(schoolId, nTerm, sPlaneID);
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    tB_GradeViews = _dbGrade.TB_GradeViews.Where(w => w.nTerm == nTerm && w.sPlaneID == sPlaneID && w.SchoolID == schoolId).ToList();
        //                }
        //            }
        //        }
        //    }
        //    return tB_GradeViews;
        //}

        //public static List<TGradeDetail> GetTGradeDetailInfo(int schoolId, int nGradeId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {

        //                var gradedetailsList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.cDel == false).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(gradedetailsList);



        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                //gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.cDel == false 
        //                //&& (!(string.IsNullOrEmpty(w.ScoreBeforeMidTerm) && string.IsNullOrEmpty(w.ScoreAfterMidTerm) && string.IsNullOrEmpty(w.scoreMidTerm) && string.IsNullOrEmpty(w.scoreFinalTerm)) || w.getSpecial != "-1")).ToList();

        //                gradeDetails = _dbGrade.TGradeDetails
        //                    .Where(w => w.SchoolID == schoolId &&
        //                                w.nGradeId == nGradeId &&
        //                                w.cDel == false &&
        //                                (!string.IsNullOrEmpty(w.ScoreBeforeMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.ScoreAfterMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreFinalTerm) ||
        //                                 w.getSpecial != "-1"))
        //                    .ToList();

        //            }
        //        }
        //        //Check history table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId &&
        //                                w.nGradeId == nGradeId &&
        //                                w.cDel == false &&
        //                                (!string.IsNullOrEmpty(w.ScoreBeforeMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.ScoreAfterMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreFinalTerm) ||
        //                                 w.getSpecial != "-1"))
        //                    .ToList();
        //                gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId &&
        //                                w.nGradeId == nGradeId &&
        //                                w.cDel == false &&
        //                                (!string.IsNullOrEmpty(w.ScoreBeforeMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.ScoreAfterMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreFinalTerm) ||
        //                                 w.getSpecial != "-1"))
        //                    .ToList();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        }
        //        //Check main table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {



        //                    var gradedetailsList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.cDel == false).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(gradedetailsList);

        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId &&
        //                                w.nGradeId == nGradeId &&
        //                                w.cDel == false &&
        //                                (!string.IsNullOrEmpty(w.ScoreBeforeMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.ScoreAfterMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreMidTerm) ||
        //                                 !string.IsNullOrEmpty(w.scoreFinalTerm) ||
        //                                 w.getSpecial != "-1"))
        //                    .ToList();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static List<TGradeDetail> GetTGradeDetailInfoBySQLQuery(int schoolId, string commUser, string commGrade, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM PGTGradeDetail WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //                gradeDetails = (SQL != null) ? pgLiteDB.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM TGradeDetail WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //                gradeDetails = (SQL != null) ? _dbGrade.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();
        //            }
        //        }
        //        //Chekc history table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                //var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).ToList();
        //                //gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //                var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM TGradeDetailHistory WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //                var gradeDetailsHistory = (SQL != null) ? _dbGradeHistory.Database.SqlQuery<GradeHistoryEntity.TGradeDetailHistory>(SQL).ToList() : new List<GradeHistoryEntity.TGradeDetailHistory>();
        //                gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(gradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            //var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId).ToList();
        //            //gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //            var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM TGradeDetailHistory WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //            var gradeDetailsHistory = (SQL != null) ? _dbGradeHistory.Database.SqlQuery<GradeHistoryEntity.TGradeDetailHistory>(SQL).ToList() : new List<GradeHistoryEntity.TGradeDetailHistory>();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(gradeDetailsHistory);
        //        }

        //        //Check Main table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM PGTGradeDetail WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //                    gradeDetails = (SQL != null) ? pgLiteDB.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    var SQL = (!string.IsNullOrEmpty(commUser) || !string.IsNullOrEmpty(commGrade)) ? string.Format("SELECT * FROM TGradeDetail WHERE 1=1 {0}  {1} and SchoolID={2} and cDel = 0", commUser, commGrade, schoolId) : null;
        //                    gradeDetails = (SQL != null) ? _dbGrade.Database.SqlQuery<TGradeDetail>(SQL).ToList() : new List<TGradeDetail>();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static TGradeDetail GetTGradeDetailInfoBySId(int schoolId, int nGradeId, int sId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    TGradeDetail gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.sID == sId && x.cDel == false).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailFromPGTGradeDetails(sqgradedetail);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).FirstOrDefault();
        //            }
        //        }
        //        //Check history table
        //        if (gradeDetails == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).FirstOrDefault();
        //                gradeDetails = iMapper.Map<GradeHistoryEntity.TGradeDetailHistory, TGradeDetail>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).FirstOrDefault();
        //            gradeDetails = iMapper.Map<GradeHistoryEntity.TGradeDetailHistory, TGradeDetail>(tgradeDetailsHistory);
        //        }

        //        //Check Main Table
        //        if (gradeDetails == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.sID == sId && x.cDel == false).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailFromPGTGradeDetails(sqgradedetail);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static List<TGradeDetail> GetTGradeDetailInfosBySId(int schoolId, int nGradeId, int sId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.sID == sId && x.cDel == false).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetail);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).ToList();
        //            }
        //        }

        //        //Check history table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).ToList();
        //                gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).ToList();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        }

        //        //Check main table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.nGradeId == nGradeId && x.sID == sId && x.cDel == false).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetail);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.nGradeId == nGradeId && w.sID == sId && w.cDel == false).ToList();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static List<TGradeDetail> GetTGradeDetailInfosBySIdTermsPlaneId(int schoolId, string nTerm, int sPlaneId, int sId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {

        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetailsList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                          join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                          select gd).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetailsList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                select gd).ToList();
        //            }
        //        }

        //        //Check history table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                            join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                            select gd).ToList();

        //                gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                        join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                        select gd).ToList();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        }

        //        //Check main table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var sqgradedetailsList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                              join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                              select gd).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetailsList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                    join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                    select gd).ToList();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static TGradeDetail CheckGradeDetailsExistByTermsPlaneId(int schoolId, string nTerm, int sPlaneId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    TGradeDetail gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetailsList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                          join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                          select gd).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailFromPGTGradeDetails(sqgradedetailsList);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                select gd).FirstOrDefault();
        //            }
        //        }

        //        //Check history table
        //        if (gradeDetails == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                            join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                            select gd).FirstOrDefault();

        //                gradeDetails = iMapper.Map<GradeHistoryEntity.TGradeDetailHistory, TGradeDetail>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                        join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                        select gd).FirstOrDefault();
        //            gradeDetails = iMapper.Map<GradeHistoryEntity.TGradeDetailHistory, TGradeDetail>(tgradeDetailsHistory);
        //        }

        //        //Check main table
        //        if (gradeDetails == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var sqgradedetailsList = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                              join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                              select gd).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailFromPGTGradeDetails(sqgradedetailsList);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.sPlaneID == sPlaneId)
        //                                    join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.cDel == false) on g.nGradeId equals gd.nGradeId
        //                                    select gd).FirstOrDefault();
        //                }
        //            }
        //        }
        //    }

        //    if (gradeDetails != null && Common.CheckScoreExist(gradeDetails))
        //    {
        //        return gradeDetails;
        //    }
        //    return null;
        //}
        //public static List<TGradeDetail> GetTGradeDetailInfosBySId(int schoolId, List<int> nGradeId, int sId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetail);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();
        //            }
        //        }
        //        //Check history table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();
        //                gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        }

        //        //Check main table
        //        if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {


        //                    var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();

        //                    gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetail);


        //                }
        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && nGradeId.Contains(w.nGradeId) && w.sID == sId && w.cDel == false).ToList();
        //                }
        //            }
        //        }
        //    }

        //    return gradeDetails;
        //}

        //public static List<TGradeDetail> GetTGradeDetailInfosBySId(int schoolId, int sId, bool isRequestForCurrentAcademicYear)
        //{
        //    List<TGradeDetail> gradeDetails = null;
        //    IMapper iMapper = GetMapper();
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {


        //                var sqgradedetail = pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false).ToList();

        //                gradeDetails = SchoolBright.DataAccess.DataAccessHelper.GetTGradeDetailListFromPGTGradeDetails(sqgradedetail);


        //            }
        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false).ToList();
        //            }
        //        }
        //        //Check history table
        //        //if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        //{
        //        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        //    {
        //        //        var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false).ToList();
        //        //        gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var tgradeDetailsHistory = _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false).ToList();
        //            gradeDetails = iMapper.Map<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>(tgradeDetailsHistory);
        //        }

        //        //Check main table
        //        //if (gradeDetails == null || (gradeDetails != null && gradeDetails.Count == 0))
        //        //{
        //        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        //    {
        //        //        gradeDetails = _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false).ToList();
        //        //    }
        //        //}
        //    }

        //    return gradeDetails;
        //}

        //public static TGradeEdit GetTGradeEdits(int schoolId, int nGradeDetailId, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    TGradeEdit gradeEdit = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {



        //                var PGTgradeEditList = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId && x.GradeDetailID == nGradeDetailId).ToList();

        //                gradeEdit = SchoolBright.DataAccess.DataAccessHelper.GetGradeEditsFromPGTGradeEditList(PGTgradeEditList);

        //            }

        //        }
        //        else
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                gradeEdit = _dbGrade.TGradeEdits.Where(w => w.SchoolID == schoolId && w.GradeDetailID == nGradeDetailId && w.UseGradeSet == 1).FirstOrDefault();
        //            }
        //        }



        //        if (gradeEdit == null)
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var gradeEditHistory = _dbGradeHistory.TGradeEditsHistories.Where(w => w.SchoolID == schoolId && w.GradeDetailID == nGradeDetailId && w.UseGradeSet == 1).FirstOrDefault();
        //                gradeEdit = iMapper.Map<GradeHistoryEntity.TGradeEditsHistory, TGradeEdit>(gradeEditHistory);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var gradeEditHistory = _dbGradeHistory.TGradeEditsHistories.Where(w => w.SchoolID == schoolId && w.GradeDetailID == nGradeDetailId && w.UseGradeSet == 1).FirstOrDefault();
        //            gradeEdit = iMapper.Map<GradeHistoryEntity.TGradeEditsHistory, TGradeEdit>(gradeEditHistory);
        //        }

        //        //If history IS NULL check exist in Main table
        //        if (gradeEdit == null)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {

        //                    var PGTgradeEditList = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId && x.GradeDetailID == nGradeDetailId && x.UseGradeSet == 1).ToList();

        //                    gradeEdit = SchoolBright.DataAccess.DataAccessHelper.GetGradeEditsFromPGTGradeEditList(PGTgradeEditList);

        //                }

        //            }
        //            else
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    gradeEdit = _dbGrade.TGradeEdits.Where(w => w.SchoolID == schoolId && w.GradeDetailID == nGradeDetailId && w.UseGradeSet == 1).FirstOrDefault();
        //                }
        //            }



        //        }
        //    }

        //    return gradeEdit;
        //}

        //public static List<TGradeEdit> GetTGradeEditsBySQLQuery(int schoolId, string commGrade, List<int> iCommGrade, bool isRequestForCurrentAcademicYear, IMapper iMapper)
        //{
        //    List<TGradeEdit> gradeEdit = null;
        //    if (isRequestForCurrentAcademicYear)
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {

        //            if (!string.IsNullOrEmpty(commGrade))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    using (var pgLiteDB = new PGGradeDBEntities())
        //                    {


        //                        var PGTgradeEditList = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId && iCommGrade.Contains(x.GradeDetailID)).ToList();

        //                        gradeEdit = SchoolBright.DataAccess.DataAccessHelper.GetGradeEditsListFromPGTGradeEditList(PGTgradeEditList);


        //                    }
        //                }
        //                else
        //                {


        //                    var SQL = string.Format("SELECT * FROM TGradeEdits AS TGradeEdit  WHERE 1=1 {0} and SchoolID= {1} ", commGrade, schoolId);
        //                    gradeEdit = _dbGrade.Database.SqlQuery<TGradeEdit>(SQL).ToList();
        //                }
        //            }
        //            else
        //            {
        //                gradeEdit = new List<TGradeEdit>();
        //            }
        //        }
        //        //Check history table
        //        if (gradeEdit == null || (gradeEdit != null && gradeEdit.Count == 0))
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                var SQL = string.Format("SELECT * FROM TGradeEditsHistory AS TGradeEditHistory  WHERE 1=1 {0} and SchoolID= {1} ", commGrade, schoolId);
        //                var db_GradeEdits = _dbGradeHistory.Database.SqlQuery<GradeHistoryEntity.TGradeEditsHistory>(SQL).ToList();
        //                gradeEdit = iMapper.Map<List<GradeHistoryEntity.TGradeEditsHistory>, List<TGradeEdit>>(db_GradeEdits);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            var SQL = string.Format("SELECT * FROM TGradeEditsHistory AS TGradeEditHistory  WHERE 1=1 {0} and SchoolID= {1} ", commGrade, schoolId);
        //            var db_GradeEdits = _dbGradeHistory.Database.SqlQuery<GradeHistoryEntity.TGradeEditsHistory>(SQL).ToList();
        //            gradeEdit = iMapper.Map<List<GradeHistoryEntity.TGradeEditsHistory>, List<TGradeEdit>>(db_GradeEdits);
        //        }

        //        //Check main table
        //        if (gradeEdit == null || (gradeEdit != null && gradeEdit.Count == 0))
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                if (!string.IsNullOrEmpty(commGrade))
        //                {
        //                    if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                    {
        //                        using (var pgLiteDB = new PGGradeDBEntities())
        //                        {


        //                            var PGTgradeEditList = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId && iCommGrade.Contains(x.GradeDetailID)).ToList();
        //                            gradeEdit = SchoolBright.DataAccess.DataAccessHelper.GetGradeEditsListFromPGTGradeEditList(PGTgradeEditList);

        //                        }
        //                    }
        //                    else
        //                    {
        //                        var SQL = string.Format("SELECT * FROM TGradeEdits  WHERE 1=1 {0} and SchoolID= {1} ", commGrade, schoolId);
        //                        gradeEdit = _dbGrade.Database.SqlQuery<TGradeEdit>(SQL).ToList();
        //                    }
        //                }
        //                else
        //                {
        //                    gradeEdit = new List<TGradeEdit>();
        //                }
        //            }
        //        }
        //    }

        //    return gradeEdit;
        //}


        //public static List<PlanList2> GetStudentsGradeForYearlyReport(int? useryear, string term, string term2, int idlvn, int idlv2n, int schoolId, int userId, out string[] planid, string selectedTerm = null)
        //{
        //    try
        //    {
        //        //Logic Moved from SchoolReportRankingPerYear and schoolReportExport1.aspx

        //        using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //        {
        //            //Rank Calculation
        //            //Yearly grade calculation
        //            List<PlanList2> planlist = new List<PlanList2>();
        //            var f_term = GetTerm(useryear, term, dbschool, schoolId);
        //            var f_term_term2 = GetTerm(useryear, term2, dbschool, schoolId);

        //            var yearDTO = ServiceHelper.GetYearByYearNumber((int)useryear, schoolId, userId);
        //            var planCourseWithTerm = ServiceHelper.GetPlanCoursesWithTerm(idlvn, (int)idlv2n, ((yearDTO != null) ? yearDTO.NYear : 0), schoolId, dbschool);
        //            var q_planes_term1 = planCourseWithTerm;

        //            var isRequestForCurrentAcademicYear = ServiceHelper.IsRequestForCurrentAcademicYear(useryear ?? 0, null, schoolId);
        //            IMapper mapper = ServiceHelper.GetMapper();

        //            var f_term1 = GetTerm(useryear, "1", dbschool, schoolId);
        //            var f_term2 = GetTerm(useryear, "2", dbschool, schoolId);

        //            List<string> unique = (isRequestForCurrentAcademicYear) ? Common.GetPlane(f_term1.nTerm, f_term2.nTerm, idlv2n, idlvn, schoolId) : Common.GetPlaneHistory(f_term1.nTerm, f_term2.nTerm, idlv2n, idlvn, schoolId);
        //            if (unique.Count == 0 && isRequestForCurrentAcademicYear)
        //            {
        //                unique = Common.GetPlaneHistory(f_term1.nTerm, f_term2.nTerm, idlv2n, idlvn, schoolId);
        //                if (unique.Count > 0 && isRequestForCurrentAcademicYear)
        //                {
        //                    isRequestForCurrentAcademicYear = false;
        //                }
        //            }
        //            var q_grades = ServiceHelper.GetTGradeInfoByRoom(f_term.nTerm, schoolId, idlv2n, mapper, isRequestForCurrentAcademicYear);
        //            var q_gradeId = q_grades.Select(s => s.nGradeId).ToList();

        //            var q_grades_term2 = ServiceHelper.GetTGradeInfoByRoom(f_term_term2.nTerm, schoolId, idlv2n, mapper, isRequestForCurrentAcademicYear);
        //            var q_gradeId_term2 = q_grades_term2.Select(s => s.nGradeId).ToList();

        //            int number = 1;


        //            planid = Common.GetPlanLists(unique, q_planes_term1, q_grades, null, false, isRequestForCurrentAcademicYear, mapper, schoolId);
        //            string[] planidTerm2 = Common.GetPlanLists(unique, q_planes_term1, q_grades_term2, null, false, isRequestForCurrentAcademicYear, mapper, schoolId);

        //            planid = planid.Where(w => w != null).ToArray();

        //            if (planidTerm2.Length != planid.Length)
        //                planid = planid.Union(planidTerm2).ToArray().Where(w => w != null).ToArray();

        //            var subjectsIds = planid;

        //            int countMaxScore = q_planes_term1.Count(c => subjectsIds.Contains(c.SPlaneId.ToString()) && (c.CourseGroupName == "รายวิชาพื้นฐาน" || c.CourseGroupName == "รายวิชาเพิ่มเติม"));

        //            List<TB_StudentViews> StudentViews = null;

        //            if (!string.IsNullOrEmpty(selectedTerm))
        //            {
        //                if (selectedTerm == "1" && f_term1 != null)
        //                {
        //                    StudentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sTerm == selectedTerm && w.nTerm == f_term1.nTerm && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();
        //                }
        //                else if (f_term2 != null && !string.IsNullOrEmpty(f_term2.nTerm))
        //                {
        //                    StudentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sTerm == selectedTerm && w.nTerm == f_term2.nTerm && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();
        //                }
        //            }
        //            else if (f_term1 != null && f_term2 != null && !string.IsNullOrEmpty(f_term2.nTerm))
        //            {
        //                StudentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && (w.nTerm == f_term1.nTerm || w.nTerm == f_term2.nTerm) && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();
        //            }
        //            else if (f_term1 != null)
        //            {
        //                StudentViews = dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTerm == f_term1.nTerm && w.nTermSubLevel2 == idlv2n && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();
        //            }

        //            //var q_user = dbschool.TUsers.Where(w => w.SchoolID == schoolId).ToList();

        //            var xmlNTermParameter = new List<XMLnTermParameter>() {
        //                        new XMLnTermParameter { nTerm = f_term.nTerm, SchoolId = schoolId },
        //                        new XMLnTermParameter { nTerm = f_term_term2.nTerm, SchoolId = schoolId },
        //                    };

        //            var sIds = StudentViews.Select(s => new XMLStudentParameter { SId = s.sID }).ToList();
        //            var gradeInfo = ServiceHelper.GetStudentsGradeByTerms(xmlNTermParameter, sIds, idlv2n, schoolId, isRequestForCurrentAcademicYear);

        //            foreach (var data in StudentViews)
        //            {

        //                //var TUserMaster = q_usermaster.FirstOrDefault(w => w.nSystemID == data.sID);
        //                //if (TUserMaster != null)
        //                //{

        //                double maxweight = 0;
        //                double sumweight = 0;
        //                double sumscoreall = 0;
        //                double sumscoreall2 = 0;
        //                int countplan = 0;
        //                PlanList2 plan = Common.GetPlanList2(data);

        //                int index = 1;
        //                double g1;
        //                var studentGradeInfo = gradeInfo.Where(w => w.sID == data.sID).ToList();
        //                foreach (var f_plan in planid)
        //                {
        //                    var f_grades = studentGradeInfo.FirstOrDefault(w => w.NTerm == f_term.nTerm && w.sPlaneID.ToString() == f_plan);
        //                    var f_grades_term2 = studentGradeInfo.FirstOrDefault(w => w.NTerm == f_term_term2.nTerm && w.sPlaneID.ToString() == f_plan);

        //                    var gradeDecimal = "0";

        //                    if (f_grades != null || f_grades_term2 != null)
        //                    {
        //                        if (f_grades != null)
        //                        {

        //                            var term1Grade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == f_grades.nGradeId).FirstOrDefault();
        //                            if (term1Grade != null && !string.IsNullOrEmpty(term1Grade.GradeDicimal))
        //                            {
        //                                gradeDecimal = term1Grade.GradeDicimal;
        //                            }

        //                            double score_term1 = 0;

        //                            if (f_grades.getScore100 != null && f_grades.getScore100 != "")
        //                                score_term1 = double.Parse(f_grades.getScore100);

        //                            double score_term2 = 0;

        //                            if (f_grades_term2 != null && !string.IsNullOrEmpty(f_grades_term2.getScore100))
        //                            {
        //                                bool g1n = double.TryParse(f_grades_term2.getScore100, out g1);
        //                                if (g1n == true)
        //                                    score_term2 = g1;
        //                            }



        //                            double? weight = 0;
        //                            //if (f_grades != null)
        //                            //{
        //                            if (f_grades.CourseGroupName == "รายวิชาเพิ่มเติม" || f_grades.CourseGroupName == "รายวิชาพื้นฐาน")
        //                                weight = f_grades.NCredit;
        //                            //}

        //                            if (f_grades_term2 != null && f_grades_term2.NCredit != weight)
        //                            {
        //                                if (f_grades_term2.CourseGroupName == "รายวิชาเพิ่มเติม" || f_grades_term2.CourseGroupName == "รายวิชาพื้นฐาน")
        //                                    weight = f_grades_term2.NCredit;
        //                            }


        //                            //var checkplan = q_planes_term1.Where(w => w.SPlaneId.ToString() == f_plan).FirstOrDefault();

        //                            maxweight += (double)weight;

        //                            double sum = score_term1 + score_term2;
        //                            //double sumRoomChange = score_term1 + score_term2;
        //                            //sumRoomChange = sumRoomChange / 2;
        //                            double sum200int = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);
        //                            //if (score_term1 != 0 && score_term2 != 0) //SB-4746 //SB-6320
        //                            if (f_grades_term2 != null)
        //                                sum = sum / 2;

        //                            double sumint = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);

        //                            //int sumint2 = (int)Math.Round(sumRoomChange, MidpointRounding.AwayFromZero);
        //                            if ((f_grades != null && f_grades.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && f_grades.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต")
        //                                || (f_grades_term2 != null && f_grades_term2.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && f_grades_term2.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"))
        //                                sumscoreall2 = sumscoreall2 + sumint;
        //                            sumscoreall = sumscoreall + sum;

        //                            if ((f_grades != null && (f_grades.CourseGroupName == "รายวิชาพื้นฐาน" || f_grades.CourseGroupName == "รายวิชาเพิ่มเติม"))
        //                                || (f_grades_term2 != null && (f_grades_term2.CourseGroupName == "รายวิชาพื้นฐาน" || f_grades_term2.CourseGroupName == "รายวิชาเพิ่มเติม")))
        //                                countplan = countplan + 1;

        //                            var getgradeWeight = Common.GetGradeWeight(null, score_term1, score_term2, f_grades.getSpecial, f_grades_term2?.getSpecial, weight, gradeDecimal, f_grades.getGradeLabel ?? f_grades_term2.getGradeLabel);
        //                            sumweight = getgradeWeight.sumweight + sumweight;
        //                            string gradelabel = getgradeWeight.getGradeLabel ?? string.Empty;




        //                            string term1score = score_term1.ToString();
        //                            if (term1score == "0")
        //                                term1score = "-";
        //                            string term2score = score_term2.ToString();
        //                            if (term2score == "0")
        //                                term2score = "-";

        //                            if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                            {
        //                                if (term == "1")
        //                                {

        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term1score, null);
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term2score, null);

        //                                }
        //                                else
        //                                {
        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term2score, null);
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term1score, null);
        //                                }


        //                                typeof(PlanList2).GetProperty("sum" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sumint) : sumint.ToString(), null);
        //                                typeof(PlanList2).GetProperty("max" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sum200int) : sum200int.ToString(), null);
        //                                typeof(PlanList2).GetProperty("grade" + index).SetValue(plan, gradelabel, null);
        //                            }

        //                            index++;
        //                        }
        //                        else if (f_grades == null && f_grades_term2 != null)
        //                        {

        //                            var term2Grade = _dbGrade.TGrades.Where(w => w.SchoolID == schoolId && w.nGradeId == f_grades_term2.nGradeId).FirstOrDefault();
        //                            if (term2Grade != null && !string.IsNullOrEmpty(term2Grade.GradeDicimal))
        //                            {
        //                                gradeDecimal = term2Grade.GradeDicimal;
        //                            }

        //                            double score_term1 = 0;
        //                            double score_term2 = 0;

        //                            bool g1n = double.TryParse(f_grades_term2.getScore100, out g1);
        //                            if (g1n == true)
        //                                score_term2 = g1;

        //                            double? weight = 0;

        //                            if (f_grades_term2.CourseGroupName == "รายวิชาเพิ่มเติม" || f_grades_term2.CourseGroupName == "รายวิชาพื้นฐาน")
        //                                weight = f_grades_term2.NCredit;

        //                            maxweight += (double)weight;

        //                            double sum = score_term1 + score_term2;
        //                            double sumRoomChange = score_term1 + score_term2;
        //                            sumRoomChange = sumRoomChange / 2;

        //                            double sum200int = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);

        //                            //int sum200int = (int)Math.Round(sum, MidpointRounding.AwayFromZero);

        //                            double sumint = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);
        //                            double sumint2 = (gradeDecimal == "1") ? Math.Round(sumRoomChange, 2) : Math.Round(sumRoomChange, MidpointRounding.AwayFromZero);


        //                            if (f_grades_term2.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && f_grades_term2.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต")
        //                                sumscoreall2 = sumscoreall2 + sumint;
        //                            sumscoreall = sumscoreall + sum;

        //                            if (f_grades_term2 != null && (f_grades_term2.CourseGroupName == "รายวิชาพื้นฐาน" || f_grades_term2.CourseGroupName == "รายวิชาเพิ่มเติม"))
        //                                countplan = countplan + 1;

        //                            var getgradeWeight = Common.GetGradeWeight(null, score_term1, score_term2, "", f_grades_term2.getSpecial, f_grades_term2.NCredit, gradeDecimal, f_grades?.getGradeLabel ?? f_grades_term2?.getGradeLabel);
        //                            sumweight = getgradeWeight.sumweight + sumweight;
        //                            string gradelabel = getgradeWeight.getGradeLabel ?? string.Empty;

        //                            if ((f_grades_term2.getGradeLabel != null && f_grades_term2.getSpecial != "-1"))
        //                            {
        //                                gradelabel = f_grades_term2.getGradeLabel;
        //                            }
        //                            string term1score = score_term1.ToString();
        //                            if (term1score == "0")
        //                                term1score = "-";
        //                            string term2score = score_term2.ToString();
        //                            if (term2score == "0")
        //                                term2score = "-";
        //                            if (term == "1")
        //                            {
        //                                if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term1score, null);

        //                                if (typeof(PlanList2).GetProperty("term2score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term2score, null);
        //                            }
        //                            else
        //                            {
        //                                if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term2score, null);
        //                                if (typeof(PlanList2).GetProperty("term2score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term1score, null);
        //                            }
        //                            if (typeof(PlanList2).GetProperty("sum" + index) != null)
        //                                typeof(PlanList2).GetProperty("sum" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sumint) : sumint.ToString(), null);
        //                            if (typeof(PlanList2).GetProperty("max" + index) != null)
        //                                typeof(PlanList2).GetProperty("max" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sum200int) : sum200int.ToString(), null);
        //                            if (typeof(PlanList2).GetProperty("grade" + index) != null)
        //                                typeof(PlanList2).GetProperty("grade" + index).SetValue(plan, gradelabel, null);

        //                            index++;
        //                        }


        //                        //}
        //                        //else
        //                        //{
        //                        //    index++;
        //                        //}


        //                    }
        //                    else
        //                    {
        //                        double score_term1 = 0;
        //                        double score_term2 = 0;
        //                        double sum = score_term1 + score_term2;
        //                        string term1score = score_term1.ToString();
        //                        if (term1score == "0")
        //                            term1score = "-";
        //                        string term2score = score_term2.ToString();
        //                        if (term2score == "0")
        //                            term2score = "-";
        //                        gradeDecimal = "0";
        //                        //countplan = countplan + 1;
        //                        double sum200int = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);

        //                        //int sum200int = (int)Math.Round(sum, MidpointRounding.AwayFromZero);

        //                        double sumint = (gradeDecimal == "1") ? Math.Round(sum, 2) : Math.Round(sum, MidpointRounding.AwayFromZero);

        //                        if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                        {
        //                            if (term == "1")
        //                            {
        //                                if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term1score, null);

        //                                if (typeof(PlanList2).GetProperty("term2score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term2score, null);
        //                            }
        //                            else
        //                            {
        //                                if (typeof(PlanList2).GetProperty("term1score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term1score" + index).SetValue(plan, term2score, null);
        //                                if (typeof(PlanList2).GetProperty("term2score" + index) != null)
        //                                    typeof(PlanList2).GetProperty("term2score" + index).SetValue(plan, term1score, null);
        //                            }
        //                            if (typeof(PlanList2).GetProperty("sum" + index) != null)
        //                                typeof(PlanList2).GetProperty("sum" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sumint) : sumint.ToString(), null);
        //                            if (typeof(PlanList2).GetProperty("max" + index) != null)
        //                                typeof(PlanList2).GetProperty("max" + index).SetValue(plan, (gradeDecimal == "1") ? string.Format("{0:0.00}", sum200int) : sum200int.ToString(), null);
        //                            if (typeof(PlanList2).GetProperty("grade" + index) != null)
        //                                typeof(PlanList2).GetProperty("grade" + index).SetValue(plan, "-", null);

        //                        }
        //                        index++;

        //                    }

        //                }


        //                var data1 = (from a in dbschool.TUser.Where(w => w.sStudentID == data.sStudentID && w.SchoolID == schoolId && w.cDel == null)
        //                             from b in dbschool.TStudentClassroomHistories.Where(o => o.sID == a.sID && o.nTerm == f_term1.nTerm && o.cDel == false)
        //                             select new
        //                             {
        //                                 a.DayQuit,
        //                                 b.nStudentStatus
        //                             })
        //              .FirstOrDefault();

        //                var data2 = (from a in dbschool.TUser.Where(w => w.sStudentID == data.sStudentID && w.SchoolID == schoolId && w.cDel == null)
        //                             from b in dbschool.TStudentClassroomHistories.Where(o => o.sID == a.sID && o.nTerm == f_term2.nTerm && o.cDel == false)
        //                             select new
        //                             {
        //                                 a.DayQuit,
        //                                 b.nStudentStatus
        //                             })
        //                           .FirstOrDefault();

        //                if ((data1 != null && data1.nStudentStatus == 2) || (data2 != null && data2.nStudentStatus == 2))
        //                {
        //                    plan.IsStudentResigned = true;
        //                }



        //                plan.scoreall = countMaxScore * 100;
        //                plan.sumweight = sumweight;
        //                plan.maxweight = maxweight;
        //                plan.sumall = sumscoreall2.ToString();
        //                double cal100 = (sumscoreall2 * 100) / (countplan * 100);
        //                plan.score100get = cal100.ToString("0.00");

        //                //double cal4 = Math.Round(((sumweight * 4) / (maxweight * 4)), 3);
        //                //plan.gradeget = cal4.ToString("0.000");
        //                //plan.gradeget = plan.gradeget.Remove(plan.gradeget.Length - 1);
        //                plan.gradeget = Common.CalculateGPA(sumweight, maxweight);
        //                plan.gradegetdouble = sumscoreall;
        //                plan.GPAdouble = (!string.IsNullOrEmpty(plan.gradeget) && plan.gradeget != "Na") ? double.Parse(plan.gradeget) : 0;

        //                plan.grade = Convert.ToDouble(Common.CalculateGPA(sumweight, maxweight));//CalGrade(maxweight, sumweight);

        //                number = number + 1;
        //                planlist.Add(plan);
        //            }

        //            return planlist;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        planid = new string[0];
        //        return new List<PlanList2>();
        //    }

        //}



        private static TTerm GetTerm(int? useryear, string term, JabJaiEntities dbschool, int schoolId)
        {
            return (from a in dbschool.TYears.Where(w => w.SchoolID == schoolId)
                    join b in dbschool.TTerms.Where(w => w.SchoolID == schoolId) on a.nYear equals b.nYear
                    where a.numberYear == useryear && b.sTerm == term && b.cDel == null
                    select b).FirstOrDefault();
        }

        //public static StudentRanking GetStudentRankingByGPA(GradeRequest request)
        //{
        //    var studentRanking = new StudentRanking();
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
        //    {
        //        if (request.nTerms != null && request.nTermSubLevel2s != null)
        //        {
        //            var StudentViews = schoolDbContext.TB_StudentViews.Where(w => w.SchoolID == request.SchoolId && w.nYear == request.nYear && request.nTermSubLevel2s.Contains(w.nTermSubLevel2) && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => o.nTSubLevel2).ThenBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();
        //            var terms = (from t in schoolDbContext.TTerms.Where(w => w.SchoolID == request.SchoolId && w.cDel == null && request.nTerms.Contains(w.nTerm)).ToList()
        //                         join y in schoolDbContext.TYears.Where(w => w.SchoolID == request.SchoolId) on t.nYear equals y.nYear
        //                         //where y.numberYear == 2562
        //                         select new { t.nTerm, t.sTerm, y.numberYear, y.nYear }).ToList();
        //            var xmlNTermParameter = new List<XMLnTermParameter>();
        //            foreach (var t in terms)
        //            {
        //                xmlNTermParameter.Add(new XMLnTermParameter { nTerm = t.nTerm, SchoolId = request.SchoolId, sTerm = t.sTerm, NumberYear = t.numberYear ?? 0 });
        //            }

        //            var sIds = StudentViews.Select(s => new XMLStudentParameter { SId = s.sID }).ToList();
        //            //sIds.Add(new XMLStudentParameter { SId = 742347 });
        //            var gradeInfo = new List<StudentGradeInfoDTO>();
        //            var gradeInfoPreviouseTerm = new List<StudentGradeInfoDTO>();
        //            if (request.IsPrimary)
        //            {
        //                gradeInfo = ServiceHelper.GetStudentsGradeByYearly(xmlNTermParameter, sIds, 0, request.SchoolId, true);
        //                gradeInfoPreviouseTerm = ServiceHelper.GetStudentsGradeByYearly(xmlNTermParameter, sIds, 0, request.SchoolId, false);

        //            }
        //            else
        //            {
        //                gradeInfo = ServiceHelper.GetStudentsGradeByTerms(xmlNTermParameter, sIds, 0, request.SchoolId, true);
        //                gradeInfoPreviouseTerm = ServiceHelper.GetStudentsGradeByTerms(xmlNTermParameter, sIds, 0, request.SchoolId, false);
        //            }

        //            if (gradeInfo == null && gradeInfoPreviouseTerm != null)
        //            {
        //                gradeInfo = gradeInfoPreviouseTerm;
        //            }
        //            else if (gradeInfo != null && gradeInfoPreviouseTerm != null)
        //            {
        //                gradeInfo.AddRange(gradeInfoPreviouseTerm);
        //            }
        //            List<string> column = new List<string>();
        //            //column.Add("RowNumber");
        //            //column.Add("Ranking");
        //            column.Add("StudentId");
        //            column.Add("Name");
        //            column.Add("SubLevel2");
        //            if (request.IsPrimary)
        //            {
        //                var columnSelectedYear = gradeInfo
        //                            .Select(s => s.numberYear?.ToString())
        //                            .Distinct()
        //                            .OrderBy(numberYear => numberYear)
        //                            .ToList();

        //                column.AddRange(columnSelectedYear);
        //                //foreach (var g in gradeInfo.Select(s => new { s.numberYear }).Distinct().OrderBy(o => o.numberYear).ToList())
        //                //{
        //                //    column.Add((g.numberYear ?? 0).ToString());
        //                //}
        //            }
        //            else
        //            {
        //                var columnSelectedYears = gradeInfo
        //                                .Select(s => $"{s.numberYear}/{s.sTerm}")
        //                                .Distinct()
        //                                .OrderBy(combination => combination)
        //                                .ToList();
        //                column.AddRange(columnSelectedYears);
        //                //foreach (var g in gradeInfo.Select(s => new { s.numberYear, s.sTerm }).Distinct().OrderBy(o => o.numberYear).ThenBy(o1 => o1.sTerm).ToList())
        //                //{
        //                //    column.Add(string.Format("{0}/{1}", g.numberYear, g.sTerm));
        //                //}
        //            }
        //            column.Add("FinalGPA");
        //            studentRanking.RankingInfoColumn = column;
        //            List<dynamic> gradeRankingInfo = new List<dynamic>();

        //            foreach (var s in StudentViews)
        //            {
        //                dynamic rankingInfo = new ExpandoObject();
        //                ((IDictionary<String, Object>)rankingInfo).Add("RowNumber", 0);
        //                ((IDictionary<String, Object>)rankingInfo).Add("Ranking", 0);
        //                ((IDictionary<String, Object>)rankingInfo).Add("StudentId", s.sStudentID);
        //                ((IDictionary<String, Object>)rankingInfo).Add("Name", $"{s.titleDescription} {s.sName} {s.sLastname}");
        //                ((IDictionary<String, Object>)rankingInfo).Add("SubLevel2", s.nTSubLevel2);


        //                var studentGradeInfo = new List<StudentGradeInfoDTO>();
        //                if (request.IsPrimary)
        //                {
        //                    studentGradeInfo = gradeInfo.Where(w => w.sID == s.sID).Select(s1 => new StudentGradeInfoDTO { sID = s1.sID, numberYear = s1.numberYear, GPA = s1.GPA, TotalScore = s1.TotalScore, AverageGPA = s1.AverageGPA }).DistinctBy(d => new { d.numberYear }).OrderBy(o => o.numberYear).ToList();
        //                }
        //                else
        //                {


        //                    studentGradeInfo = gradeInfo.Where(w => w.sID == s.sID).Select(s1 => new StudentGradeInfoDTO { sID = s1.sID, numberYear = s1.numberYear, NTerm = s1.NTerm, sTerm = s1.sTerm, GPA = s1.GPA, TotalScore = s1.TotalScore, AverageGPA = s1.AverageGPA }).DistinctBy(d => new { d.numberYear, d.sTerm }).OrderBy(o => o.numberYear).ThenBy(t => t.sTerm).ToList();


        //                }
        //                if (gradeInfo != null)
        //                {
        //                    //var averageGPAAndTotalCredit = (from gi in gradeInfo.Where(w => w.sID == s.sID)
        //                    //                                where (gi.CourseGroupName == "รายวิชาพื้นฐาน" || gi.CourseGroupName == "รายวิชาเพิ่มเติม" ||
        //                    //gi.CourseGroupName == "Fundamental Subjects" || gi.CourseGroupName == "Additional Subjects") && gi.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && gi.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
        //                    //                                group gi by new { gi.sID } into sg
        //                    //                                select new { sg.Key.sID, TotalNCredit = sg.Sum(s2 => s2.NCredit), TotalScore = sg.Sum(s2 => !string.IsNullOrEmpty(s2.getScore100) ? double.Parse(s2.getScore100) : 0), TotalGadexNCredit = sg.Sum(s2 => s2.GradeXNCredit), GPA = ((sg.Sum(s2 => s2.GradeXNCredit) * 4) / (sg.Sum(s2 => s2.NCredit) * 4)) });

        //                    //if (studentGradeInfo != null && averageGPAAndTotalCredit != null)
        //                    //{
        //                    //    foreach (var g in averageGPAAndTotalCredit)
        //                    //    {
        //                    //        studentGradeInfo.Where(w => w.sID == g.sID).ToList().ForEach(f => { f.AverageGPA = GetGPA(g.GPA); });
        //                    //    }
        //                    //}

        //                    var filteredCourseGroups = new string[] { "รายวิชาพื้นฐาน", "รายวิชาเพิ่มเติม", "Fundamental Subjects", "Additional Subjects" };
        //                    var ignoredCourseGroups = new string[] { "กิจกรรมพัฒนาผู้เรียน", "รายวิชาเสริมไม่คิดหน่วยกิต" };

        //                    var averageGPAAndTotalCredit = from gi in gradeInfo
        //                                                   where gi.sID == s.sID &&
        //                                                         filteredCourseGroups.Contains(gi.CourseGroupName) &&
        //                                                         !ignoredCourseGroups.Contains(gi.CourseGroupName)
        //                                                   group gi by gi.sID into sg
        //                                                   select new
        //                                                   {
        //                                                       sg.Key,
        //                                                       TotalNCredit = sg.Sum(s2 => s2.NCredit),
        //                                                       TotalScore = sg.Sum(s2 => !string.IsNullOrEmpty(s2.getScore100) ? double.Parse(s2.getScore100) : 0),
        //                                                       TotalGadexNCredit = sg.Sum(s2 => s2.GradeXNCredit),
        //                                                       GPA = ((sg.Sum(s2 => s2.GradeXNCredit) * 4) / (sg.Sum(s2 => s2.NCredit) * 4))
        //                                                   };

        //                    foreach (var g in averageGPAAndTotalCredit)
        //                    {
        //                        var studentToUpdate = studentGradeInfo.FirstOrDefault(w => w.sID == g.Key);
        //                        if (studentToUpdate != null)
        //                        {
        //                            studentToUpdate.AverageGPA = GetGPA(g.GPA);
        //                        }
        //                    }
        //                }

        //                if (studentGradeInfo != null && studentGradeInfo.Count > 0)
        //                {
        //                    if (request.IsPrimary)
        //                    {
        //                        //foreach (var g in studentGradeInfo)
        //                        //{
        //                        //    bool keyExists = ((IDictionary<String, Object>)rankingInfo).ContainsKey((g.numberYear ?? 0).ToString());
        //                        //    if (!keyExists)
        //                        //        ((IDictionary<String, Object>)rankingInfo).Add((g.numberYear ?? 0).ToString(), g.GPA);
        //                        //}
        //                        foreach (var g in studentGradeInfo)
        //                        {
        //                            var key = (g.numberYear ?? 0).ToString();
        //                            if (!((IDictionary<String, Object>)rankingInfo).ContainsKey(key))
        //                            {
        //                                ((IDictionary<String, Object>)rankingInfo).Add(key, g.GPA);
        //                            }
        //                        }
        //                    }
        //                    else
        //                    {
        //                        ////foreach (var g in studentGradeInfo)
        //                        ////{
        //                        ////    bool keyExists = ((IDictionary<String, Object>)rankingInfo).ContainsKey(string.Format("{0}/{1}", g.numberYear, g.sTerm));
        //                        ////    if (!keyExists)
        //                        ////        ((IDictionary<String, Object>)rankingInfo).Add(string.Format("{0}/{1}", g.numberYear, g.sTerm), g.GPA);
        //                        ////    keyExists = ((IDictionary<String, Object>)rankingInfo).ContainsKey("NTerm");
        //                        ////    if (!keyExists)
        //                        ////        ((IDictionary<String, Object>)rankingInfo).Add("NTerm", g.NTerm);

        //                        ////}
        //                        ///
        //                        if (!((IDictionary<String, Object>)rankingInfo).ContainsKey("NTerm"))
        //                        {
        //                            ((IDictionary<String, Object>)rankingInfo).Add("NTerm", studentGradeInfo.FirstOrDefault()?.NTerm);
        //                        }

        //                        foreach (var g in studentGradeInfo)
        //                        {
        //                            var key = $"{g.numberYear}/{g.sTerm}";
        //                            if (!((IDictionary<String, Object>)rankingInfo).ContainsKey(key))
        //                            {
        //                                ((IDictionary<String, Object>)rankingInfo).Add(key, g.GPA);
        //                            }
        //                        }
        //                    }
        //                    if (studentGradeInfo.Count() > 1)
        //                    {
        //                        ////((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", GetGPA(Math.Round(((double?)(studentGradeInfo.Sum(g => decimal.Parse(g.GPA)) / studentGradeInfo.Count())) ?? 0, 2, MidpointRounding.AwayFromZero)));
        //                        ((IDictionary<String, Object>)rankingInfo).Add("TotalScore", studentGradeInfo.Where(w => w.TotalScore != null).Sum(g => g.TotalScore));
        //                        ((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", studentGradeInfo[0].AverageGPA);

        //                        //var totalScore = studentGradeInfo.Where(g => g.TotalScore != null).Sum(g => g.TotalScore);
        //                        //rankingInfo["TotalScore"] = totalScore;

        //                        //if (studentGradeInfo.Count > 0)
        //                        //{
        //                        //    rankingInfo["FinalGPA"] = studentGradeInfo[0].AverageGPA;
        //                        //}
        //                    }
        //                    else if (studentGradeInfo.Count() == 1)
        //                    {
        //                        //((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", studentGradeInfo[0].GPA);
        //                        ((IDictionary<String, Object>)rankingInfo).Add("TotalScore", studentGradeInfo.Where(w => w.TotalScore != null).Sum(g => g.TotalScore));
        //                        ((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", studentGradeInfo[0].AverageGPA);
        //                    }
        //                    else
        //                    {
        //                        ((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", "");
        //                        ((IDictionary<String, Object>)rankingInfo).Add("TotalScore", 0);
        //                        //((IDictionary<String, Object>)rankingInfo).Add("AverageGPA", "");
        //                    }
        //                }
        //                else
        //                {
        //                    ((IDictionary<String, Object>)rankingInfo).Add("FinalGPA", "");
        //                    ((IDictionary<String, Object>)rankingInfo).Add("TotalScore", 0);
        //                    //((IDictionary<String, Object>)rankingInfo).Add("AverageGPA", "");
        //                }
        //                ((IDictionary<String, Object>)rankingInfo).Add("NStudentStatus", s.nStudentStatus);
        //                ((IDictionary<String, Object>)rankingInfo).Add("MoveOutDate", s.MoveOutDate);

        //                var keyCreated = new List<string>();
        //                foreach (var r in rankingInfo)
        //                {
        //                    keyCreated.Add(r.Key);
        //                }
        //                var missingColumn = column.Except(keyCreated);
        //                foreach (var c in missingColumn)
        //                {
        //                    ((IDictionary<String, Object>)rankingInfo).Add(c, "");
        //                }
        //                gradeRankingInfo.Add(rankingInfo);
        //            }

        //            if (gradeRankingInfo != null)
        //            {
        //                var ranking = gradeRankingInfo.OrderByDescending(x => x.FinalGPA);//.ThenByDescending(x => x.TotalScore).ToList();

        //                //int rank2 = 0;
        //                //double lastScore = 0;
        //                //int counter = 1;

        //                //foreach (var item in ranking)
        //                //{
        //                //    if (lastScore != (!string.IsNullOrEmpty(item.FinalGPA) ? Convert.ToDouble(item.FinalGPA) : 0))
        //                //    {

        //                //        if (rank2 != counter)
        //                //        {
        //                //            rank2++;
        //                //        }
        //                //        else
        //                //        {
        //                //            rank2 = counter;
        //                //        }
        //                //    }
        //                //    lastScore = (!string.IsNullOrEmpty(item.FinalGPA) ? Convert.ToDouble(item.FinalGPA) : 0);
        //                //    item.Ranking = rank2;
        //                //    item.RowNumber = counter;
        //                //    counter = counter + 1;
        //                //}
        //                int rank2 = 0;
        //                double lastScore = 0;
        //                double lastScore2 = 0;
        //                int counter = 1;

        //                foreach (var item in ranking)
        //                {
        //                    double finalGPA = 0;
        //                    double.TryParse(item.FinalGPA, out finalGPA);

        //                    double totalScore = 0;
        //                    double.TryParse(item.TotalScore.ToString(), out totalScore);
        //                    //var term = terms.Where(w => w.nTerm == item.NTerm).FirstOrDefault();
        //                    if ((item.NStudentStatus ?? 0) != 0)
        //                    {
        //                        if (lastScore != finalGPA)
        //                            rank2 = counter;
        //                        //else if (lastScore == finalGPA && lastScore2 != totalScore)
        //                        //    rank2 = counter;
        //                        lastScore2 = totalScore;
        //                        lastScore = finalGPA;

        //                        //if (f_term.dEnd <= item.MoveOutDate && f_term_term2.dEnd <= item.MoveOutDate)
        //                        //{
        //                        //    item.scoreRank = rank2.ToString();
        //                        //    counter = counter + 1;
        //                        //}
        //                        //item.Ranking = rank2;
        //                        item.RowNumber = counter;
        //                        counter = counter + 1;
        //                    }
        //                    else
        //                    {
        //                        if (lastScore != finalGPA)
        //                            rank2 = counter;
        //                        //else if (lastScore == finalGPA && lastScore2 != totalScore)
        //                        //    rank2 = counter;
        //                        lastScore2 = totalScore;
        //                        lastScore = finalGPA;
        //                        //item.scoreRank = rank2.ToString();
        //                        item.Ranking = rank2;
        //                        item.RowNumber = counter;
        //                        counter = counter + 1;
        //                    }
        //                }
        //                studentRanking.RankingInfo = gradeRankingInfo.OrderBy(o => o.RowNumber).ToList();
        //            }
        //            return studentRanking;
        //        }
        //        return new StudentRanking();
        //    }
        //}
        //public static List<StudentGradeInfoDTO> GetStudentGradeInfoByYearly(int schoolId, int nTSubLevel, int nTermSubLevel2, int numberYear, string sTerm, int sId, bool isRankingRequired = false)
        //{
        //    var studentRanking = new StudentRanking();
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
        //    {

        //        var studentViews = schoolDbContext.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel
        //        && w.numberYear == numberYear && w.sTerm == sTerm
        //        && ((w.cDel ?? "0") != "1")).DistinctBy(d => d.sID).OrderBy(o => (o.nStudentNumber == null) ? 9999 : o.nStudentNumber).AsQueryable().ToList();

        //        if (!isRankingRequired)
        //        {
        //            studentViews = studentViews.Where(w => w.sID == sId).ToList();
        //        }
        //        var terms = (from t in schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.cDel == null).ToList()
        //                     join y in schoolDbContext.TYears.Where(w => w.SchoolID == schoolId && w.numberYear == numberYear) on t.nYear equals y.nYear
        //                     //where y.numberYear == 2562
        //                     select new { t.nTerm, t.sTerm, y.numberYear, y.nYear }).ToList();

        //        var isRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(0, terms[0].nTerm, schoolId);

        //        var xmlNTermParameter = new List<XMLnTermParameter>();
        //        foreach (var t in terms)
        //        {
        //            xmlNTermParameter.Add(new XMLnTermParameter { nTerm = t.nTerm, SchoolId = schoolId, sTerm = t.sTerm, NumberYear = t.numberYear ?? 0 });
        //        }

        //        var sIds = studentViews.Select(s => new XMLStudentParameter { SId = s.sID }).ToList();
        //        //sIds.Add(new XMLStudentParameter { SId = 742347 });
        //        var gradeInfo = new List<StudentGradeInfoDTO>();
        //        var gradeInfoPreviouseTerm = new List<StudentGradeInfoDTO>();

        //        gradeInfo = ServiceHelper.GetStudentsGradeByYearly(xmlNTermParameter, sIds, 0, schoolId, isRequestForCurrentAcademicYear);
        //        //var groupQuery = gradeInfo
        //        //                   .GroupBy(x => new { x.GPA, x.Term1AndTerm2Score, x.sID, NTerm = x.NTerm ?? x.NTerm2, x.nTermSubLevel2 })
        //        //                   .OrderByDescending(g => g.Key.GPA).ThenByDescending(g => g.Key.Term1AndTerm2Score)
        //        //                   .AsEnumerable()
        //        //                   .Select(g => new { g.Key.GPA, g.Key.Term1AndTerm2Score, g.Key.sID, g.Key.NTerm, g.Key.nTermSubLevel2 }).ToList();
        //        //TotalScore = g.Sum(s => s.Term1AndTerm2Score ?? 0), 
        //        var groupQueryByClass = gradeInfo.Where(w => !string.IsNullOrEmpty(w.GPA))
        //                          .GroupBy(x => new { x.sID, x.RoomId })
        //                          .AsEnumerable()
        //                          .Select(g => new { GPA = g.Select(s => s.GPA).FirstOrDefault(), TotalScore = g.Sum(s => s.Term1AndTerm2Score ?? 0), FirstSubjectScore = g.Select(s => s.Term1AndTerm2Score).FirstOrDefault(), SecondSubjectScore = g.Select(s => s.Term1AndTerm2Score).Skip(1).FirstOrDefault(), g.Key.sID, nTermSubLevel2 = g.Key.RoomId }).ToList();


        //        var groupQueryByLevel = gradeInfo.Where(w => !string.IsNullOrEmpty(w.GPA))
        //                          .GroupBy(x => new { x.sID })
        //                          .AsEnumerable()
        //                          .Select(g => new { GPA = g.Select(s => s.GPA).FirstOrDefault(), TotalScore = g.Sum(s => s.Term1AndTerm2Score ?? 0), FirstSubjectScore = g.Select(s => s.Term1AndTerm2Score).FirstOrDefault(), SecondSubjectScore = g.Select(s => s.Term1AndTerm2Score).Skip(1).FirstOrDefault(), g.Key.sID }).ToList();


        //        var rooms = groupQueryByClass.Select(s => s.nTermSubLevel2).Distinct().ToList();
        //        foreach (var room in rooms)
        //        {
        //            var query = groupQueryByClass.Where(w => w.nTermSubLevel2 == room)
        //                               .GroupBy(x => new { x.sID, x.GPA, x.TotalScore, x.FirstSubjectScore, x.SecondSubjectScore })
        //                               .OrderByDescending(g => g.Key.GPA).ThenByDescending(g => g.Key.TotalScore).ThenByDescending(g => g.Key.FirstSubjectScore).ThenByDescending(g => g.Key.SecondSubjectScore)
        //                               .AsEnumerable()
        //                               .Select((g, i) => new { g, i, Ranking = i + 1 })
        //                               .SelectMany(x =>
        //                                   x.g.Select((y, j) => new
        //                                   {
        //                                       sId = y.sID,
        //                                       DenseRank = x.i,
        //                                       DenseRank1 = j,
        //                                       DenseRank2 = x.Ranking,
        //                                       Ranking = j + x.Ranking
        //                                   }
        //                               )).ToList();

        //            gradeInfo.Where(w => w.RoomId == room).ToList().ForEach(f => f.RankingByClass = (query.Where(w => w.sId == f.sID).Count() > 0) ? query.Where(w => w.sId == f.sID).Select(s => s.DenseRank2).FirstOrDefault() : 0);
        //        }



        //        var queryLevel = groupQueryByLevel
        //                          .GroupBy(x => new { x.GPA, x.TotalScore, x.FirstSubjectScore, x.SecondSubjectScore })
        //                          .OrderByDescending(g => g.Key.GPA).ThenByDescending(g => g.Key.TotalScore).ThenByDescending(g => g.Key.FirstSubjectScore).ThenByDescending(g => g.Key.SecondSubjectScore)
        //                          .AsEnumerable()
        //                            .Select((g, i) => new { g, i, Ranking = i + 1 })
        //                            .SelectMany(x =>
        //                                x.g.Select((y, j) => new
        //                                {
        //                                    sId = y.sID,
        //                                    GPA = y.GPA,
        //                                    FirstSubjectScore = y.FirstSubjectScore,
        //                                    SecondSubjectScore = y.SecondSubjectScore,
        //                                    DenseRank = x.i,
        //                                    DenseRank1 = j,
        //                                    DenseRank2 = x.Ranking,
        //                                    Ranking = j + x.Ranking
        //                                }
        //                            ));


        //        gradeInfo.ForEach(f => f.RankingByLevel = (queryLevel.Where(w => w.sId == f.sID).Count() > 0) ? queryLevel.Where(w => w.sId == f.sID).Select(s => s.DenseRank2).FirstOrDefault() : 0);


        //        return gradeInfo;

        //        //return new StudentRanking();
        //    }
        //}

//        public static List<StudentGradeInfoDTO> GetStudentsGradeByTerms(List<XMLnTermParameter> nTerm, List<XMLStudentParameter> sIds, int nTermSubLevel2, int schoolId, bool IsRequestForCurrentAcademicYear, bool thaiLanguage = true)
//        {
//            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
//            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
//            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
//            {
//                var studentGradeByTerm = new List<GetStudentGradeByTerm_Result>();
//                if (IsRequestForCurrentAcademicYear)
//                {
//                    if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
//                    {
//                        using (var pgLiteDB = new PGGradeDBEntities())
//                        {

//                            List<int> _iSid = sIds.Select(x => x.SId).ToList();

//                            List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
//                            List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
//                            List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();


//                            _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

//                            _PGTGradeList = (from G in pgLiteDB.PGTGrades
//                                             join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
//                                             where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
//                                             select G).ToList();


//                            _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
//                                                 join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
//                                                 where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
//                                                 select GE).ToList();



//                            var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
//                            var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
//                            var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

//                            var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
//                            var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
//                            var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);

//                            var nTermList = Common.GetXMLFromObject(nTerm);
//                            var sIdsList = Common.GetXMLFromObject(sIds);


//                            var dt = new DataTable();
//                            var conn = _dbGrade.Database.Connection;
//                            var connectionState = conn.State;
//                            _dbGrade.Database.CommandTimeout = 16000;


//                            try
//                            {


//                                if (connectionState != ConnectionState.Open) conn.Open();
//                                using (var cmd = conn.CreateCommand())
//                                {
//                                    cmd.CommandTimeout = 16000;
//                                    cmd.CommandText = "SQGetStudentGradeByTerm";
//                                    cmd.CommandType = CommandType.StoredProcedure;
//                                    cmd.Parameters.Add(new SqlParameter("@XMLNTermList", nTermList));
//                                    cmd.Parameters.Add(new SqlParameter("@XMLStudentList", sIdsList));
//                                    cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
//                                    cmd.Parameters.Add(new SqlParameter("@IncludeTerm", true));
//                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
//                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
//                                    cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


//                                    using (var reader = cmd.ExecuteReader())
//                                    {
//                                        dt.Load(reader);
//                                    }

//                                    studentGradeByTerm = ServiceHelper.ConvertDataTableToList<GetStudentGradeByTerm_Result>(dt);


//                                    try
//                                    {
//                                        string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
//                                        pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
//                                    }
//                                    catch (Exception ex)
//                                    {

//                                    }
//                                }
//                            }
//                            catch (Exception ex)
//                            {
//                                // return new GetGradeDetailView_Result();

//                            }
//                            finally
//                            {
//                                //if (connectionState != ConnectionState.Closed) conn.Close();
//                            }

//                        }
//                    }
//                    else
//                    {
//                        studentGradeByTerm = _dbGrade.GetStudentGradeByTerm(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();

//                    }

//                }
//                else
//                {
//                    var studentGradeByTermHistory = _dbGradeHistory.GetStudentGradeByTermHistory(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();
//                    var config = new MapperConfiguration(cfg =>
//                    {

//                        cfg.AllowNullCollections = true;
//                        cfg.CreateMap<GradeHistoryEntity.GetStudentGradeByTermHistory_Result, GetStudentGradeByTerm_Result>();

//                    });
//                    IMapper iMapper = config.CreateMapper();

//                    studentGradeByTerm = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeByTermHistory_Result>, List<GetStudentGradeByTerm_Result>>(studentGradeByTermHistory);

//                    ////if history doesn't exist then check in the main table.
//                    //if (studentGradeByTerm == null || (studentGradeByTerm != null && studentGradeByTerm.Count == 0))
//                    //{
//                    //    studentGradeByTerm = _dbGrade.GetStudentGradeByTerm(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();
//                    //}
//                }
//                //var planCourse = GetPlanCourses(0, nTermSubLevel2, nTerm, nYear, schoolId, schoolDbContext, thaiLanguage);
//                //var studentInfo = schoolDbContext.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).ToList();
//                var studentGradeInfoDTO = new List<StudentGradeInfoDTO>();
//                if (studentGradeByTerm != null)
//                {
//                    var roomInfo = (from sg in studentGradeByTerm.Where(w => w.nTermSubLevel2 != null)
//                                    select new { sg.NTerm, sg.nTermSubLevel2 }).Distinct().ToList();

//                    var roomInfoImported = (from sg in studentGradeByTerm.Where(w => w.nTermSubLevel2 == null && w.PlanId == null)
//                                            select new { sg.NTerm, sg.nTermSubLevel2 }).Distinct().ToList();

//                    var roomInfoImportedWithPlanId = (from sg in studentGradeByTerm.Where(w => w.nTermSubLevel2 == null && w.PlanId != null)
//                                                      select new { sg.NTerm, sg.nTermSubLevel2, sg.PlanId }).Distinct().OrderBy(o => o.PlanId).ToList();


//                    //var PlanCourseDTOsImportedWithPlanid = new List<PlanCourseDTO>();
//                    var PlanCourseDTOsImported = new List<PlanCourseDTO>();
//                    var planCourseDTOs = new List<PlanCourseDTO>();
//                    if (roomInfoImportedWithPlanId != null && roomInfoImportedWithPlanId.Count > 0)
//                    {
//                        var planIds = roomInfoImportedWithPlanId.Where(w => w.PlanId > 0).Select(s => s.PlanId).Distinct().ToList();
//                        if (planIds != null && planIds.Count() > 0)  // If imported data then check filter plan id used to import the old grade
//                        {

//                            foreach (var planId in planIds)
//                            {

//                                var query = string.Format(@"select DISTINCT C.PlanCourseId, P.sPlaneID, P.sPlaneName, P.courseType, P.courseGroup, P.courseCode,  C.nCredit, C.CourseTotalHour, 
//C.CourseHour, CType.nOrder, CType.Description CourseTypeName, CG.Description CourseGroupName , PCT.nTerm from TPlanCourse C 
//join TPlane P  ON C.sPlaneID = P.sPlaneID and C.PlanId = {0} 
//join TCourseType CType ON P.courseType = CType.courseTypeId
//join TCourseGroup CG ON P.courseGroup = CG.courseGroupId
//JOIN TPlanCourseTerm PCT ON C.PlanCourseId = PCT.PlanCourseId where C.SchoolID = {1}  and C.PlanId = {0} and C.CourseStatus = 1 and C.IsActive = 1  and CType.SchoolID = {1} and CG.SchoolID = {1}
//		and P.SchoolID = {1} and P.cDel IS NULL and PCT.IsActive = 1 order by PCT.nTerm, P.courseGroup, CType.nOrder, P.courseCode", planId, schoolId);

//                                var planCourseDTOImportedWithPlanid = schoolDbContext.Database.SqlQuery<PlanCourseDTO>(query).ToList();

//                                //var planCourseDTOImportedWithPlanid = (from p in schoolDbContext.GetPlanCoursesByPlanId(planId, schoolId)
//                                //                                       select new PlanCourseDTO
//                                //                                       {
//                                //                                           PlanCourseId = p.PlanCourseId,
//                                //                                           SPlaneId = p.sPlaneID,
//                                //                                           CourseName = p.sPlaneName,
//                                //                                           CourseType = p.courseType,
//                                //                                           CourseTypeName = p.CourseTypeName,
//                                //                                           CourseGroup = p.courseGroup,
//                                //                                           CourseGroupName = p.CourseGroupName,
//                                //                                           CourseCode = p.courseCode,
//                                //                                           NCredit = p.nCredit,
//                                //                                           CourseTotalHour = p.CourseTotalHour,
//                                //                                           CourseHour = p.CourseHour,
//                                //                                           NOrder = p.nOrder ?? 0,
//                                //                                           PlanId = planId ?? 0

//                                //                                       }).Distinct().ToList();
//                                if (PlanCourseDTOsImported != null && planCourseDTOImportedWithPlanid.Count > 0)
//                                    PlanCourseDTOsImported.AddRange(planCourseDTOImportedWithPlanid);

//                            }

//                        }
//                    }

//                    if (roomInfoImported != null && roomInfoImported.Count > 0)
//                    {
//                        foreach (var r in roomInfoImported)
//                        {
//                            if (PlanCourseDTOsImported.Where(w => w.NTerm == r.NTerm && (w.nTermSubLevel2 == r.nTermSubLevel2 || (w.nTermSubLevel2 == 0 && r.nTermSubLevel2 == null))).Count() == 0)
//                            {
//                                var planCourse = GetPlanCourses(0, r.nTermSubLevel2 ?? 0, r.NTerm, 0, schoolId, schoolDbContext);
//                                if (planCourse != null && planCourse.Count > 0)
//                                {
//                                    PlanCourseDTOsImported.AddRange(planCourse);
//                                }
//                            }
//                        }
//                    }

//                    foreach (var r in roomInfo)
//                    {
//                        if (planCourseDTOs.Where(w => w.NTerm == r.NTerm && w.nTermSubLevel2 == r.nTermSubLevel2).Count() == 0)
//                        {
//                            var planCourse = GetPlanCourses(0, r.nTermSubLevel2 ?? 0, r.NTerm, 0, schoolId, schoolDbContext);
//                            if (planCourse != null && planCourse.Count > 0)
//                            {
//                                planCourseDTOs.AddRange(planCourse);
//                            }
//                        }
//                    }

//                    studentGradeInfoDTO = BindPlanCourse(planCourseDTOs, studentGradeByTerm.Where(w => w.nTermSubLevel2 != null).ToList(), schoolId, schoolDbContext);

//                    //if (PlanCourseDTOsImportedWithOutPlanid != null && PlanCourseDTOsImportedWithOutPlanid.Count > 0)
//                    //{
//                    //    studentGradeInfoDTO.AddRange(BindPlanCourse(PlanCourseDTOsImportedWithOutPlanid, gradeTranscriptInfo.Where(w => w.nTermSubLevel2 == null && w.PlanId == null).ToList(), schoolId, dbschool));
//                    //}

//                    if (PlanCourseDTOsImported != null && PlanCourseDTOsImported.Count > 0)
//                    {
//                        studentGradeInfoDTO.AddRange(BindPlanCourse(PlanCourseDTOsImported, studentGradeByTerm.Where(w => w.nTermSubLevel2 == null).ToList(), schoolId, schoolDbContext));
//                    }

//                    //var gpaandtotalcredit = (from s in studentGradeInfoDTO
//                    //                         where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
//                    //                         group s by new { s.sID, s.NTerm } into sg
//                    //                         select new { sg.Key.sID, TotalNCredit = sg.Sum(s => s.NCredit), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) }).FirstOrDefault();

//                    //if (studentGradeInfoDTO != null && gpaandtotalcredit != null)
//                    //{
//                    //    var gpa = (gpaandtotalcredit?.GPA ?? 0).ToString("0.000");
//                    //    studentGradeInfoDTO.ForEach(f => f.GPA = (f.GradeSet != null) ? f.GradeSet.ToString() : gpa.Remove(gpa.Length - 1));
//                    //}
//                    var gpaandtotalcredit = (from s in studentGradeInfoDTO
//                                             where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
//                                             group s by new { s.sID, s.NTerm } into sg
//                                             select new { sg.Key.sID, sg.Key.NTerm, TotalNCredit = sg.Sum(s => s.NCredit), TotalScore = sg.Sum(s => !string.IsNullOrEmpty(s.getScore100) ? double.Parse(s.getScore100) : 0), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) });

//                    var averageGPAAndTotalCredit = (from s in studentGradeInfoDTO
//                                                    where (s.CourseGroupName == "รายวิชาพื้นฐาน" || s.CourseGroupName == "รายวิชาเพิ่มเติม" ||
//                    s.CourseGroupName == "Fundamental Subjects" || s.CourseGroupName == "Additional Subjects") && s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
//                                                    group s by new { s.sID } into sg
//                                                    select new { sg.Key.sID, TotalNCredit = sg.Sum(s => s.NCredit), TotalScore = sg.Sum(s => !string.IsNullOrEmpty(s.getScore100) ? double.Parse(s.getScore100) : 0), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) });
//                    foreach (var g in gpaandtotalcredit)
//                    {
//                        if (studentGradeInfoDTO != null && gpaandtotalcredit != null)
//                        {
//                            //var gpa = (gpaandtotalcredit?.GPA ?? 0).ToString("0.000");
//                            //studentGradeInfoDTO.ForEach(f => f.GPA = (f.GradeSet != null)? f.GradeSet.ToString() : gpa.Remove(gpa.Length - 1));
//                            studentGradeInfoDTO.Where(w => w.sID == g.sID && w.NTerm == g.NTerm).ToList().ForEach(f => { f.GPA = GetGPA(g.GPA); f.TotalScore = g.TotalScore; });
//                        }


//                    }
//                    if (studentGradeInfoDTO != null && averageGPAAndTotalCredit != null)
//                    {
//                        foreach (var g in averageGPAAndTotalCredit)
//                        {
//                            studentGradeInfoDTO.Where(w => w.sID == g.sID).ToList().ForEach(f => { f.AverageGPA = GetGPA(g.GPA); });
//                        }
//                    }
//                }
//                if (studentGradeInfoDTO != null && studentGradeInfoDTO.Count > 0)
//                {
//                    studentGradeInfoDTO = studentGradeInfoDTO.OrderBy(o => o.CourseGroup).ThenBy(o => o.nOrder).ThenBy(o => o.CourseCode).ToList();
//                }
//                return studentGradeInfoDTO;
//            }
//        }
        public static byte[] GetGradeRankingPdfContent(GradeRankingRequest request)
        {
            MemoryStream memStream = new MemoryStream();
            byte[] pdfBytes;

            // Bold
            BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew_bold-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font h1 = new Font(bf_bold, 14);
            Font bold = new Font(bf_bold, 12);
            Font smallBold = new Font(bf_bold, 10);

            // Normal
            BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/Fonts/thsarabunnew-webfont.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font normal = new Font(bf_normal, 10);
            Font smallNormal = new Font(bf_normal, 8);
            Font smallNormal_Black = new Font(bf_normal, 8, 0, new BaseColor(System.Drawing.Color.Black));
            Font Header_smallNormal = new Font(bf_normal, 8, 0 /*,new BaseColor(255, 255, 255)*/);

            Document pdfDoc = new Document(PageSize.A4, 12, 12, 20, 50);
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, memStream);
            pdfDoc.AddAuthor("School Bright");

            pdfDoc.Open();

            // Set width column
            float[] widthPDFHeaderPercentage = new float[] { 50, 50 };
            float[] widthPDFTablePercentage = new float[] { 3f, 8.25f, 16.175f, 2.75f, 2.75f, 2.75f, 2.75f, 3.2f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f, 2.75f };

            //Paragraph paragraph = new Paragraph("รายงานผลการเรียนระเบียนสะสม", h1);
            //Paragraph blank = new Paragraph(" ", normal);
            //paragraph.Alignment = Element.ALIGN_CENTER;

            Paragraph paragraph = new Paragraph("This is my first line using Paragraph.");

            pdfDoc.Add(paragraph);
            // Generate PDF Header
            //PdfPTable headerTable = new PdfPTable(2);
            //headerTable.WidthPercentage = 100;
            //headerTable.HorizontalAlignment = 0;
            //headerTable.SpacingAfter = 10;

            //headerTable.SetTotalWidth(widthPDFHeaderPercentage);

            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีการศึกษา {0}", request.NumberYear), Font = normal, HorizontalAlignment = Element.ALIGN_LEFT }));
            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ระดับชั้นเรียน {0}", request.SubLevel), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));
            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("แผน {0}", request.PlanName), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));
            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ชั้นเรียน {0}", request.SubLevel2), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));

            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีเริ่มต้น {0} ", request.StartYearTerm), Font = normal, HorizontalAlignment = Element.ALIGN_LEFT }));
            //headerTable.AddCell(SetCell(new CellProperty { Border = Rectangle.NO_BORDER, Text = string.Format("ปีสิ้นสุด {0}", request.EndYearTerm), Font = normal, HorizontalAlignment = Element.ALIGN_RIGHT }));

            foreach (var r in request.RankingInfos)
            {
                //keyCreated.Add(r.Key);
            }

            // Generate PDF Table
            PdfPTable pdfTable = new PdfPTable(29);

            int numberPerPage = 23;
            int rowIdx = 1;
            int rowNumber = 1;

            //foreach (var rankingInfo in request.RankingInfos)
            //{
            //    if (rowIdx == 1)
            //    {
            //        pdfDoc.Add(paragraph);
            //        pdfDoc.Add(blank);

            //        //pdfDoc.Add(headerTable);

            //        // Generate Header Table
            //        //pdfTable = GetHeaderCellTable1(29, widthPDFTablePercentage, request.RankingColumn, smallNormal);
            //    }

            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = rowNumber.ToString(), Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.studentid, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.sName, Font = smallNormal, HorizontalAlignment = Element.ALIGN_LEFT, Height = 22f }));

            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.totalBeforeMidTerm, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.totalAfterMidTerm, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.totalmid, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.totalfinal, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.total100, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.getgrade, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));

            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg1, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg2, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg3, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg4, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg5, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg6, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg7, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg8, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg9, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg10, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg11, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg12, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg13, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg14, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg15, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg16, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg17, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg18, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg19, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));
            //    //pdfTable.AddCell(SetCell(new CellProperty { Text = ss.txtg20, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER, Height = 22f }));

            //    if (rowIdx >= numberPerPage || rowNumber >= request.RankingInfos.Count)
            //    {
            //        pdfDoc.Add(pdfTable);

            //        pdfDoc.NewPage();

            //        rowIdx = 0;
            //    }

            //    rowIdx++;
            //    rowNumber++;
            //}

            pdfDoc.Close();
            pdfBytes = memStream.ToArray();

            return pdfBytes;
        }

        private static PdfPTable GetHeaderCellTable1(int allColumn, float[] widthPDFTablePercentage, List<string> columnNames, Font smallNormal)
        {
            PdfPTable headerCellTable = new PdfPTable(allColumn);
            headerCellTable.WidthPercentage = 100;
            headerCellTable.HorizontalAlignment = 0;
            headerCellTable.SpacingAfter = 10;

            headerCellTable.SetTotalWidth(widthPDFTablePercentage);


            foreach (var name in columnNames)
            {
                headerCellTable.AddCell(SetCell(new CellProperty { Text = name, Font = smallNormal, HorizontalAlignment = Element.ALIGN_CENTER }));
            }



            return headerCellTable;
        }
        private static PdfPCell SetCell(CellProperty property)
        {
            Phrase elements = new Phrase(property.Text, property.Font);

            PdfPCell tableCell = new PdfPCell(elements);
            tableCell.HorizontalAlignment = property.HorizontalAlignment ?? Element.ALIGN_CENTER;
            tableCell.VerticalAlignment = property.VerticalAlignment ?? Element.ALIGN_MIDDLE;
            tableCell.Border = property.Border ?? Rectangle.BOX;
            tableCell.Colspan = property.Colspan ?? 1;
            tableCell.Rowspan = property.Rowspan ?? 1;
            tableCell.Rotation = property.Rotation ?? 0;

            if (property.Height != null) tableCell.FixedHeight = property.Height.Value;
            if (property.PaddingLeft != null) tableCell.PaddingLeft = property.PaddingLeft.Value;
            if (property.PaddingBottom != null) tableCell.PaddingBottom = property.PaddingBottom.Value;
            if (property.PaddingTop != null) tableCell.PaddingTop = property.PaddingTop.Value;

            if (property.BackgroundColor != null) tableCell.BackgroundColor = new BaseColor(property.BackgroundColor.Red, property.BackgroundColor.Green, property.BackgroundColor.Blue);

            return tableCell;
        }


        //public static List<StudentGradeInfoDTO> GetStudentsGradeByYearly(List<XMLnTermParameter> nTerm, List<XMLStudentParameter> sIds, int nTermSubLevel2, int schoolId, bool IsRequestForCurrentAcademicYear, bool thaiLanguage = true)
        //{
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
        //    {
        //        var studentGradeByYearly = new List<GetStudentGradeByYearly_Result>();
        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {

        //                    List<int> _iSid = sIds.Select(x => x.SId).ToList();

        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();


        //                    _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                    _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                     join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select G).ToList();


        //                    _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                         join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select GE).ToList();



        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                    var dt = new DataTable();
        //                    var conn = _dbGrade.Database.Connection;
        //                    var connectionState = conn.State;
        //                    _dbGrade.Database.CommandTimeout = 16000;


        //                    try
        //                    {


        //                        if (connectionState != ConnectionState.Open) conn.Open();
        //                        using (var cmd = conn.CreateCommand())
        //                        {
        //                            cmd.CommandTimeout = 16000;
        //                            cmd.CommandText = "SQGetStudentGradeByYearly";
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            cmd.Parameters.Add(new SqlParameter("@XMLNTermList", Common.GetXMLFromObject(nTerm)));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLStudentList", Common.GetXMLFromObject(sIds)));
        //                            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                            cmd.Parameters.Add(new SqlParameter("@IncludeTerm", true));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                            using (var reader = cmd.ExecuteReader())
        //                            {
        //                                dt.Load(reader);
        //                            }

        //                            studentGradeByYearly = ServiceHelper.ConvertDataTableToList<GetStudentGradeByYearly_Result>(dt);

        //                            try
        //                            {
        //                                string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
        //                            }
        //                            catch (Exception ex)
        //                            {

        //                            }

        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        // return new GetGradeDetailView_Result();

        //                    }
        //                    finally
        //                    {

        //                    }

        //                }
        //            }
        //            else
        //            {
        //                studentGradeByYearly = _dbGrade.GetStudentGradeByYearly(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();
        //            }

        //        }
        //        else
        //        {
        //            var studentGradeByYearlyHistory = _dbGradeHistory.GetStudentGradeByYearlyHistory(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();
        //            var config = new MapperConfiguration(cfg =>
        //            {

        //                cfg.AllowNullCollections = true;
        //                cfg.CreateMap<GradeHistoryEntity.GetStudentGradeByTermHistory_Result, GetStudentGradeByTerm_Result>();

        //            });
        //            IMapper iMapper = config.CreateMapper();

        //            studentGradeByYearly = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeByYearlyHistory_Result>, List<GetStudentGradeByYearly_Result>>(studentGradeByYearlyHistory);

        //            ////if history doesn't exist then check in the main table.
        //            //if (studentGradeByTerm == null || (studentGradeByTerm != null && studentGradeByTerm.Count == 0))
        //            //{
        //            //    studentGradeByTerm = _dbGrade.GetStudentGradeByTerm(Common.GetXMLFromObject(nTerm), Common.GetXMLFromObject(sIds), schoolId, true).ToList();
        //            //}
        //        }


        //        //var planCourse = GetPlanCourses(0, nTermSubLevel2, nTerm, nYear, schoolId, schoolDbContext, thaiLanguage);
        //        //var studentInfo = schoolDbContext.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).ToList();
        //        var studentGradeInfoDTO = new List<StudentGradeInfoDTO>();
        //        if (studentGradeByYearly != null && studentGradeByYearly.Count > 0)
        //        {
        //            var roomInfo = (from sg in studentGradeByYearly.Where(w => w.nTermSubLevel2 != null)
        //                            select new { sg.NTerm, sg.nTermSubLevel2 }).Distinct().ToList();

        //            if (roomInfo != null)
        //            {
        //                var roomInfoTerm2 = (from sg in studentGradeByYearly.Where(w => w.nTermSubLevel2 != null)
        //                                     select new { NTerm = sg.NTerm2, sg.nTermSubLevel2 }).Distinct().ToList();
        //                if (roomInfoTerm2 != null && roomInfoTerm2.Count > 0)
        //                {
        //                    roomInfo.AddRange(roomInfoTerm2);
        //                }

        //                roomInfo = roomInfo.Where(w => w.NTerm != null).Distinct().ToList();
        //            }

        //            var roomInfoImported = (from sg in studentGradeByYearly.Where(w => w.nTermSubLevel2 == null && w.NTerm != null && w.PlanIdTerm1 == null && w.PlanIdTerm2 == null)
        //                                    select new { sg.NTerm, sg.nTermSubLevel2 }).Distinct().ToList();

        //            var roomInfoImportedTerm2 = (from sg in studentGradeByYearly.Where(w => w.nTermSubLevel2 == null && w.NTerm2 != null && w.PlanIdTerm1 == null && w.PlanIdTerm2 == null)
        //                                         select new { sg.NTerm2, sg.nTermSubLevel2 }).Distinct().ToList();


        //            var roomInfoImportedWithPlanId = (from sg in studentGradeByYearly.Where(w => w.nTermSubLevel2 == null && (w.PlanIdTerm1 != null || w.PlanIdTerm2 != null))
        //                                              select new { sg.NTerm, sg.NTerm2, sg.nTermSubLevel2, sg.PlanIdTerm1, sg.PlanIdTerm2 }).Distinct().OrderBy(o => o.PlanIdTerm1).ToList();


        //            //var PlanCourseDTOsImportedWithPlanid = new List<PlanCourseDTO>();
        //            var PlanCourseDTOsImported = new List<PlanCourseDTO>();
        //            var planCourseDTOs = new List<PlanCourseDTO>();
        //            if (roomInfoImportedWithPlanId != null && roomInfoImportedWithPlanId.Count > 0)
        //            {
        //                var planIds = roomInfoImportedWithPlanId.Where(w => w.PlanIdTerm1 > 0 || w.PlanIdTerm2 > 0).Select(s2 => new { s2.PlanIdTerm1, s2.PlanIdTerm2, s2.NTerm, s2.NTerm2 }).Distinct().ToList();

        //                if (planIds != null && planIds.Count() > 0)  // If imported data then check filter plan id used to import the old grade
        //                {

        //                    foreach (var planId in planIds)
        //                    {

        //                        var planCourseDTOImportedWithPlanid = (from p in schoolDbContext.GetPlanCoursesByPlanId(planId.PlanIdTerm1, schoolId)
        //                                                               select new PlanCourseDTO
        //                                                               {
        //                                                                   PlanCourseId = p.PlanCourseId,
        //                                                                   SPlaneId = p.sPlaneID,
        //                                                                   CourseName = p.sPlaneName,
        //                                                                   CourseType = p.courseType,
        //                                                                   CourseTypeName = p.CourseTypeName,
        //                                                                   CourseGroup = p.courseGroup,
        //                                                                   CourseGroupName = p.CourseGroupName,
        //                                                                   CourseCode = p.courseCode,
        //                                                                   NCredit = p.nCredit,
        //                                                                   CourseTotalHour = p.CourseTotalHour,
        //                                                                   CourseHour = p.CourseHour,
        //                                                                   NOrder = p.nOrder ?? 0,
        //                                                                   PlanId = planId.PlanIdTerm1 ?? 0,
        //                                                                   NTerm = planId.NTerm
        //                                                               }).Distinct().ToList();

        //                        var planCourseDTOImportedWithPlanidTerm2 = (from p in schoolDbContext.GetPlanCoursesByPlanId(planId.PlanIdTerm2, schoolId)
        //                                                                    select new PlanCourseDTO
        //                                                                    {
        //                                                                        PlanCourseId = p.PlanCourseId,
        //                                                                        SPlaneId = p.sPlaneID,
        //                                                                        CourseName = p.sPlaneName,
        //                                                                        CourseType = p.courseType,
        //                                                                        CourseTypeName = p.CourseTypeName,
        //                                                                        CourseGroup = p.courseGroup,
        //                                                                        CourseGroupName = p.CourseGroupName,
        //                                                                        CourseCode = p.courseCode,
        //                                                                        NCredit = p.nCredit,
        //                                                                        CourseTotalHour = p.CourseTotalHour,
        //                                                                        CourseHour = p.CourseHour,
        //                                                                        NOrder = p.nOrder ?? 0,
        //                                                                        PlanId = planId.PlanIdTerm1 ?? 0,
        //                                                                        NTerm = planId.NTerm2
        //                                                                    }).Distinct().ToList();


        //                        if (PlanCourseDTOsImported != null && planCourseDTOImportedWithPlanid != null && planCourseDTOImportedWithPlanid.Count > 0)
        //                            PlanCourseDTOsImported.AddRange(planCourseDTOImportedWithPlanid);

        //                        if (PlanCourseDTOsImported != null && planCourseDTOImportedWithPlanidTerm2 != null && planCourseDTOImportedWithPlanidTerm2.Count > 0)
        //                            PlanCourseDTOsImported.AddRange(planCourseDTOImportedWithPlanidTerm2);

        //                    }

        //                }
        //            }

        //            if (roomInfoImported != null && roomInfoImported.Count > 0)
        //            {
        //                foreach (var r in roomInfoImported)
        //                {
        //                    if (PlanCourseDTOsImported.Where(w => w.NTerm == r.NTerm && (w.nTermSubLevel2 == r.nTermSubLevel2 || (w.nTermSubLevel2 == 0 && r.nTermSubLevel2 == null))).Count() == 0)
        //                    {
        //                        var planCourse = GetPlanCourses(0, r.nTermSubLevel2 ?? 0, r.NTerm, 0, schoolId, schoolDbContext);
        //                        if (planCourse != null && planCourse.Count > 0)
        //                        {
        //                            PlanCourseDTOsImported.AddRange(planCourse);
        //                        }
        //                    }
        //                }
        //            }


        //            if (roomInfoImportedTerm2 != null && roomInfoImportedTerm2.Count > 0)
        //            {
        //                foreach (var r in roomInfoImportedTerm2)
        //                {
        //                    if (PlanCourseDTOsImported.Where(w => w.NTerm == r.NTerm2 && (w.nTermSubLevel2 == r.nTermSubLevel2 || (w.nTermSubLevel2 == 0 && r.nTermSubLevel2 == null))).Count() == 0)
        //                    {
        //                        var planCourse = GetPlanCourses(0, r.nTermSubLevel2 ?? 0, r.NTerm2, 0, schoolId, schoolDbContext);
        //                        if (planCourse != null && planCourse.Count > 0)
        //                        {
        //                            PlanCourseDTOsImported.AddRange(planCourse);
        //                        }
        //                    }
        //                }
        //            }

        //            foreach (var r in roomInfo)
        //            {
        //                if (planCourseDTOs.Where(w => w.NTerm == r.NTerm && w.nTermSubLevel2 == r.nTermSubLevel2).Count() == 0)
        //                {
        //                    var planCourse = GetPlanCourses(0, r.nTermSubLevel2 ?? 0, r.NTerm, 0, schoolId, schoolDbContext);
        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                }
        //            }

        //            studentGradeInfoDTO = BindPlanCourseYearly(planCourseDTOs, studentGradeByYearly.Where(w => w.nTermSubLevel2 != null).ToList(), schoolId, schoolDbContext);

        //            //if (PlanCourseDTOsImportedWithOutPlanid != null && PlanCourseDTOsImportedWithOutPlanid.Count > 0)
        //            //{
        //            //    studentGradeInfoDTO.AddRange(BindPlanCourse(PlanCourseDTOsImportedWithOutPlanid, gradeTranscriptInfo.Where(w => w.nTermSubLevel2 == null && w.PlanId == null).ToList(), schoolId, dbschool));
        //            //}

        //            if (PlanCourseDTOsImported != null && PlanCourseDTOsImported.Count > 0)
        //            {
        //                studentGradeInfoDTO.AddRange(BindPlanCourseYearly(PlanCourseDTOsImported, studentGradeByYearly.Where(w => w.nTermSubLevel2 == null).ToList(), schoolId, schoolDbContext));
        //            }

        //            //var gpaandtotalcredit = (from s in studentGradeInfoDTO
        //            //                         where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
        //            //                         group s by new { s.sID, s.NTerm } into sg
        //            //                         select new { sg.Key.sID, TotalNCredit = sg.Sum(s => s.NCredit), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) }).FirstOrDefault();

        //            //if (studentGradeInfoDTO != null && gpaandtotalcredit != null)
        //            //{
        //            //    var gpa = (gpaandtotalcredit?.GPA ?? 0).ToString("0.000");
        //            //    studentGradeInfoDTO.ForEach(f => f.GPA = (f.GradeSet != null) ? f.GradeSet.ToString() : gpa.Remove(gpa.Length - 1));
        //            //}
        //            var gpaandtotalcredit = (from s in studentGradeInfoDTO
        //                                     where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
        //                                     group s by new { s.sID, s.NTerm } into sg
        //                                     select new { sg.Key.sID, sg.Key.NTerm, TotalNCredit = sg.Sum(s => s.NCredit), TotalScore = sg.Sum(s => !string.IsNullOrEmpty(s.getScore100) ? double.Parse(s.getScore100) : 0), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) });

        //            var averageGPAAndTotalCredit = (from s in studentGradeInfoDTO
        //                                            where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
        //                                            group s by new { s.sID } into sg
        //                                            select new { sg.Key.sID, TotalNCredit = sg.Sum(s => s.NCredit), TotalScore = sg.Sum(s => !string.IsNullOrEmpty(s.getScore100) ? double.Parse(s.getScore100) : 0), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) });
        //            foreach (var g in gpaandtotalcredit)
        //            {
        //                if (studentGradeInfoDTO != null && gpaandtotalcredit != null)
        //                {
        //                    //var gpa = (gpaandtotalcredit?.GPA ?? 0).ToString("0.000");
        //                    //studentGradeInfoDTO.ForEach(f => f.GPA = (f.GradeSet != null)? f.GradeSet.ToString() : gpa.Remove(gpa.Length - 1));
        //                    studentGradeInfoDTO.Where(w => w.sID == g.sID && w.NTerm == g.NTerm).ToList().ForEach(f => { f.GPA = GetGPA(g.GPA); f.TotalScore = g.TotalScore; });
        //                }
        //            }
        //            if (studentGradeInfoDTO != null && averageGPAAndTotalCredit != null)
        //            {
        //                foreach (var g in averageGPAAndTotalCredit)
        //                {
        //                    studentGradeInfoDTO.Where(w => w.sID == g.sID).ToList().ForEach(f => { f.AverageGPA = GetGPA(g.GPA); });
        //                }
        //            }

        //        }
        //        if (studentGradeInfoDTO != null && studentGradeInfoDTO.Count > 0)
        //        {
        //            studentGradeInfoDTO = studentGradeInfoDTO.OrderBy(o => o.CourseGroup).ThenBy(o => o.nOrder).ThenBy(o => o.CourseCode).ToList();
        //        }
        //        return studentGradeInfoDTO;
        //    }
        //}
        //public static List<StudentGradeInfoDTO> GetStudentsGradeByTerms(List<StudentGradeInfoDTO> studentGradeInfoDTO)
        //{
        //    var gradeInfoTerm1All = studentGradeInfoDTO.Where(w => w.sTerm == "1").ToList();
        //    var gradeInfoTerm2All = studentGradeInfoDTO.Where(w => w.sTerm == "2").ToList();
        //    var years = studentGradeInfoDTO.Select(s => s.numberYear).ToList();
        //    foreach(var y in years)
        //    {
        //        var gradeInfoTerm1 = gradeInfoTerm1All.Where(w => w.numberYear == y).ToList();

        //    }
        //}
        public static string GetGPA(double? gpa)
        {
            //var gpaNew = (gpa ?? 0).ToString("0.000");
            //return gpaNew.Remove(gpaNew.Length - 1);
            if (double.IsNaN((double)gpa))
                gpa = null;

            return Common.Truncate(gpa ?? 0, 2).ToString("0.00");
        }
        //public static List<StudentGradeInfoDTO> BindPlanCourse(List<PlanCourseDTO> planCourseDTOs,
        //    List<GetStudentGradeByTerm_Result> gradeTranscriptInfo, int schoolId, JabJaiEntities schoolDbContext)
        //{
        //    //return new List<StudentGradeInfoDTO>();
        //    var gradeInfo = (from sg in gradeTranscriptInfo
        //                     join pc in planCourseDTOs on
        //                     new { sPlaneID = sg.sPlaneID ?? 0, sg.NTerm, nTermSubLevel2 = sg.nTermSubLevel2 ?? 0 }
        //                     equals new { sPlaneID = pc.SPlaneId, pc.NTerm, nTermSubLevel2 = pc.nTermSubLevel2 } into grade
        //                     from gf in grade.DefaultIfEmpty()
        //                     join p in schoolDbContext.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null) on new { sPlaneID = sg.sPlaneID ?? 0, pcPlaneId = gf?.SPlaneId ?? 0 } equals new { sPlaneID = p.sPlaneID, pcPlaneId = p.sPlaneID }
        //                     join c in schoolDbContext.TSubLevels.Where(w => w.SchoolID == schoolId) on p.nTSubLevel equals c.nTSubLevel.ToString()
        //                     join t in schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.cDel == null)
        //                      on new { sg.NTerm, pcNTerm = gf.NTerm } equals new { NTerm = t.nTerm, pcNTerm = t.nTerm }
        //                     join y in schoolDbContext.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                     where gf != null


        //                     //from pc in dbschool.TPlanCourses.Where(w => w.SchoolID == schoolId && w.sPlaneID == sg.sPlaneID && w.CourseStatus == 1 && w.IsActive == true).DefaultIfEmpty()
        //                     //from p in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.sPlaneID == pc.sPlaneID)
        //                     //from pl in dbschool.TPlans.Where(w => w.SchoolID == schoolId && w.PlanId == pc.PlanId)
        //                     //from  pct in dbschool.TPlanCourseTerms.Where(w => w.SchoolID == schoolId && w.IsActive == true && w.nTerm == sg.NTerm && w.PlanCourseId == pc.PlanCourseId).DefaultIfEmpty()
        //                     //from pcr in dbschool.TPlanTermSubLevel2.Where(w => w.SchoolID == schoolId && w.PlanId == pc.PlanId && pl.PlanId == w.PlanId && w.IsActive == true && w. nTermSubLevel2 == sg.nTermSubLevel2)
        //                     //from st in students.Where(w => w.SId == sg.sID)
        //                     //from cg in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId && w.courseGroupId == p.courseGroup)
        //                     //from ct in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId && w.courseTypeId == p.courseType)
        //                     select new StudentGradeInfoDTO()
        //                     {
        //                         fRatioAfterQuiz = sg.fRatioAfterQuiz,
        //                         fRatioBeforeQuiz = sg.fRatioBeforeQuiz,
        //                         fRatioLateTerm = sg.fRatioLateTerm,
        //                         fRatioMidTerm = sg.fRatioMidTerm,
        //                         fRatioQuiz = sg.fRatioQuiz,
        //                         //FirstName = st.SName,
        //                         //LastName = st.SLastname,
        //                         //FullClassName = st.ClassFullName,
        //                         scoreBeforeAfterMidTerm = sg.scoreBeforeAfterMidTerm,
        //                         scoreFinalTerm = sg.scoreFinalTerm,
        //                         scoreMidTerm = sg.scoreMidTerm,
        //                         sID = sg.sID,
        //                         //sIdentification = st.SIdentification,
        //                         sPlaneID = sg.sPlaneID,
        //                         //sStudentId = st.SStudentID,
        //                         sTerm = t.sTerm,
        //                         ScoreAfterMidTerm = sg.ScoreAfterMidTerm,
        //                         ScoreBeforeMidTerm = sg.ScoreBeforeMidTerm,
        //                         //CourseStatus = pc.CourseStatus,
        //                         getSamattana = sg.getSamattana,
        //                         getScore100 = sg.getScore100,
        //                         getSpecial = sg.getSpecial,
        //                         GradeSet = sg.GradeSet,
        //                         getAfterQuiz100 = sg.getAfterQuiz100,
        //                         IsScoreExist = sg.IsScoreExist,
        //                         //nStudentNumber = st.NStudentNumber,
        //                         //nStudentStatus = st.nStudentStatus,
        //                         nTermSubLevel2 = sg.nTermSubLevel2,
        //                         nTSubLevel = c.nTSubLevel,
        //                         ClassName = c.SubLevel,
        //                         CourseCode = gf.CourseCode,
        //                         CourseGroup = gf.CourseGroup,
        //                         CourseGroupName = gf.CourseGroupName,
        //                         CourseHour = gf.CourseHour,
        //                         CourseName = gf.CourseName,
        //                         CourseTotalHour = (sg.CourseTotalHour == null) ? gf.CourseTotalHour : sg.CourseTotalHour,
        //                         CourseType = gf.CourseType,
        //                         CourseTypeName = gf.CourseTypeName,
        //                         GradeXNCredit = CalCulateGradeXNCredit(sg.nTermSubLevel2, ((gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit)), (double?)sg.GradeSet, sg.getSpecial, sg.getGradeLabel, sg.getScore100), //((gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit)) * sg.GradeXNCredit,
        //                         NCredit = (gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit),
        //                         getBeforeQuiz100 = sg.getBeforeQuiz100,
        //                         getBehaviorLabel = sg.getBehaviorLabel,
        //                         getBehaviorTotal = sg.getBehaviorTotal,
        //                         getFinal100 = sg.getFinal100,
        //                         getGradeLabel = sg.getGradeLabel,
        //                         getMid100 = sg.getMid100,
        //                         getQuiz100 = sg.getQuiz100,
        //                         getReadWrite = sg.getReadWrite,
        //                         //GPA = sg.GPA,
        //                         GradePass = sg.GradePass ?? 0,

        //                         maxAfterMidTerm = sg.maxAfterMidTerm,
        //                         maxBeforeMidTerm = sg.maxBeforeMidTerm,
        //                         MaxBeforeAfterMidTermTotal = sg.MaxBeforeAfterMidTermTotal,
        //                         maxFinalTerm = sg.maxFinalTerm,
        //                         maxMidTerm = sg.maxMidTerm,
        //                         nGradeId = sg.nGradeId,
        //                         nOrder = gf.NOrder,
        //                         NTerm = sg.NTerm,
        //                         numberYear = y.numberYear,
        //                         nYear = y.nYear,
        //                         //RoomName = st.SubLevel + "/" + st.nTSubLevel2,
        //                         //TitleDesc = st.titleDescription,
        //                         ActualNCredit = ((sg.NCredit == null) ? gf.NCredit : sg.NCredit),
        //                         CourseGroupNameEng = gf.CourseGroupNameEng,
        //                         CourseTypeNameEng = gf.CourseTypeNameEng

        //                     }).DistinctBy(d => new { d.sID, d.nGradeId }).ToList();
        //    return gradeInfo;
        //}
        public static double? CheckTerm1AndTerm2Credit(List<PlanCourseDTO> planCourseDTOs, int? sPlaneID, string NTerm, string NTerm2)
        {
            double? nCredit = 0;
            var term1Credit = planCourseDTOs.Where(w => w.SPlaneId == sPlaneID && w.NTerm == NTerm).Select(s => s.NCredit).FirstOrDefault();
            var term2Credit = planCourseDTOs.Where(w => w.SPlaneId == sPlaneID && w.NTerm == NTerm2).Select(s => s.NCredit).FirstOrDefault();
            if (term2Credit != null && term2Credit != term1Credit)
            {
                nCredit = term2Credit;
            }
            else if (term1Credit != null)
            {
                nCredit = term1Credit;
            }
            else if (term2Credit != null)
            {
                nCredit = term2Credit;
            }
            return nCredit;
        }
        //public static List<StudentGradeInfoDTO> BindPlanCourseYearly(List<PlanCourseDTO> planCourseDTOs,
        //    List<GetStudentGradeByYearly_Result> gradeTranscriptInfo, int schoolId, JabJaiEntities schoolDbContext)
        //{

        //    //return new List<StudentGradeInfoDTO>();
        //    var gradeInfo = (from sg in gradeTranscriptInfo
        //                     join pc in planCourseDTOs on
        //                     new { sPlaneID = sg.sPlaneID ?? 0, NTerm = sg.NTerm ?? sg.NTerm2, nTermSubLevel2 = sg.nTermSubLevel2 ?? 0 }
        //                     equals new { sPlaneID = pc.SPlaneId, pc.NTerm, nTermSubLevel2 = pc.nTermSubLevel2 } into grade
        //                     from gf in grade.DefaultIfEmpty()
        //                     join p in schoolDbContext.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null) on new { sPlaneID = sg.sPlaneID ?? 0, pcPlaneId = gf?.SPlaneId ?? 0 } equals new { sPlaneID = p.sPlaneID, pcPlaneId = p.sPlaneID }
        //                     join c in schoolDbContext.TSubLevels.Where(w => w.SchoolID == schoolId) on p.nTSubLevel equals c.nTSubLevel.ToString()
        //                     join t in schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.cDel == null)
        //                      on new { NTerm = sg.NTerm ?? sg.NTerm2, pcNTerm = gf.NTerm } equals new { NTerm = t.nTerm, pcNTerm = t.nTerm }
        //                     join y in schoolDbContext.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                     where gf != null

        //                     //let NCredit  = CheckTerm1AndTerm2Credit(planCourseDTOs, sg.sPlaneID,sg.NTerm, sg.NTerm2)
        //                     //from pc in dbschool.TPlanCourses.Where(w => w.SchoolID == schoolId && w.sPlaneID == sg.sPlaneID && w.CourseStatus == 1 && w.IsActive == true).DefaultIfEmpty()
        //                     //from p in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.sPlaneID == pc.sPlaneID)
        //                     //from pl in dbschool.TPlans.Where(w => w.SchoolID == schoolId && w.PlanId == pc.PlanId)
        //                     //from  pct in dbschool.TPlanCourseTerms.Where(w => w.SchoolID == schoolId && w.IsActive == true && w.nTerm == sg.NTerm && w.PlanCourseId == pc.PlanCourseId).DefaultIfEmpty()
        //                     //from pcr in dbschool.TPlanTermSubLevel2.Where(w => w.SchoolID == schoolId && w.PlanId == pc.PlanId && pl.PlanId == w.PlanId && w.IsActive == true && w. nTermSubLevel2 == sg.nTermSubLevel2)
        //                     //from st in students.Where(w => w.SId == sg.sID)
        //                     //from cg in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId && w.courseGroupId == p.courseGroup)
        //                     //from ct in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId && w.courseTypeId == p.courseType)
        //                     select new StudentGradeInfoDTO()
        //                     {
        //                         //fRatioAfterQuiz = sg.fRatioAfterQuiz,
        //                         //fRatioBeforeQuiz = sg.fRatioBeforeQuiz,
        //                         //fRatioLateTerm = sg.fRatioLateTerm,
        //                         //fRatioMidTerm = sg.fRatioMidTerm,
        //                         //fRatioQuiz = sg.fRatioQuiz,
        //                         //FirstName = st.SName,
        //                         //LastName = st.SLastname,
        //                         //FullClassName = st.ClassFullName,
        //                         //scoreBeforeAfterMidTerm = sg.scoreBeforeAfterMidTerm,
        //                         //scoreFinalTerm = sg.scoreFinalTerm,
        //                         //scoreMidTerm = sg.scoreMidTerm,
        //                         sID = sg.sID,
        //                         //sIdentification = st.SIdentification,
        //                         sPlaneID = sg.sPlaneID,
        //                         //sStudentId = st.SStudentID,
        //                         sTerm = t.sTerm,
        //                         //ScoreAfterMidTerm = sg.ScoreAfterMidTerm,
        //                         //ScoreBeforeMidTerm = sg.ScoreBeforeMidTerm,
        //                         //CourseStatus = pc.CourseStatus,
        //                         //getSamattana = sg.getSamattana,
        //                         getScore100 = sg.getScore100,
        //                         getSpecial = sg.getSpecial,
        //                         GradeSet = sg.GradeSet,
        //                         //getAfterQuiz100 = sg.getAfterQuiz100,
        //                         IsScoreExist = sg.IsScoreExistTerm1 ?? 0,
        //                         //nStudentNumber = st.NStudentNumber,
        //                         //nStudentStatus = st.nStudentStatus,
        //                         nTermSubLevel2 = sg.nTermSubLevel2 ?? sg.nTermSubLevel2Term2,
        //                         RoomId = sg.RoomId ?? sg.RoomIdTerm2,
        //                         nTSubLevel = c.nTSubLevel,
        //                         ClassName = c.SubLevel,
        //                         CourseCode = gf.CourseCode,
        //                         CourseGroup = gf.CourseGroup,
        //                         CourseGroupName = gf.CourseGroupName,
        //                         CourseHour = gf.CourseHour,
        //                         CourseName = gf.CourseName,
        //                         CourseTotalHour = (sg.CourseTotalHourTerm1 == null) ? gf.CourseTotalHour : sg.CourseTotalHourTerm1,
        //                         CourseType = gf.CourseType,
        //                         CourseTypeName = gf.CourseTypeName,
        //                         GradeXNCredit = CalCulateGradeXNCredit(sg.nTermSubLevel2, ((gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit)), (double?)sg.GradeSet, sg.getSpecial, sg.getGradeLabel, sg.getScore100), //((gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit)) * sg.GradeXNCredit,
        //                         NCredit = (gf.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || gf.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : ((sg.NCredit == null) ? gf.NCredit : sg.NCredit),
        //                         //getBeforeQuiz100 = sg.getBeforeQuiz100,
        //                         //getBehaviorLabel = sg.getBehaviorLabel,
        //                         //getBehaviorTotal = sg.getBehaviorTotal,
        //                         //getFinal100 = sg.getFinal100,
        //                         getGradeLabel = sg.getGradeLabel,
        //                         //getMid100 = sg.getMid100,
        //                         //getQuiz100 = sg.getQuiz100,
        //                         //getReadWrite = sg.getReadWrite,
        //                         //GPA = sg.GPA,
        //                         //GradePass = sg.GradePass ?? 0,

        //                         //maxAfterMidTerm = sg.maxAfterMidTerm,
        //                         //maxBeforeMidTerm = sg.maxBeforeMidTerm,
        //                         //MaxBeforeAfterMidTermTotal = sg.MaxBeforeAfterMidTermTotal,
        //                         //maxFinalTerm = sg.maxFinalTerm,
        //                         //maxMidTerm = sg.maxMidTerm,
        //                         nGradeId = sg.nGradeIdTerm1 ?? sg.nGradeIdTerm2 ?? 0,
        //                         nOrder = gf.NOrder,
        //                         NTerm = sg.NTerm,
        //                         numberYear = y.numberYear,
        //                         nYear = y.nYear,
        //                         Term1Score = sg.Term1Score,
        //                         Term2Score = sg.Term2Score,
        //                         Term1AndTerm2Score = (!string.IsNullOrEmpty(sg.Term1Score) ? Convert.ToDouble(sg.Term1Score) : 0) + (!string.IsNullOrEmpty(sg.Term2Score) ? Convert.ToDouble(sg.Term2Score) : 0),
        //                         getBehaviorLabelTerm1 = sg.getBehaviorLabelTerm1,
        //                         getBehaviorLabelTerm2 = sg.getBehaviorLabelTerm2,
        //                         getReadWriteTerm1 = sg.getReadWriteTerm1,
        //                         getReadWriteTerm2 = sg.getReadWriteTerm2,
        //                         getSamattanaTerm1 = sg.getSamattanaTerm1,
        //                         getSamattanaTerm2 = sg.getSamattanaTerm2,
        //                         //RoomName = st.SubLevel + "/" + st.nTSubLevel2,
        //                         //TitleDesc = st.titleDescription,
        //                         ActualNCredit = ((sg.NCredit == null) ? gf.NCredit : sg.NCredit),
        //                         CourseGroupNameEng = gf.CourseGroupNameEng,
        //                         CourseTypeNameEng = gf.CourseTypeNameEng,


        //                     }).DistinctBy(d => new { d.sID, d.nGradeId }).ToList();

        //    gradeInfo.ForEach(f =>
        //    {
        //        f.HighestScore = GetHighestScoreYearly(gradeInfo, f.sPlaneID); f.LowestScore = GetLowestScoreYearly(gradeInfo, f.sPlaneID);
        //        f.AverageScore = GetAverageScoreYearly(gradeInfo, f.sPlaneID);
        //    });


        //    return gradeInfo;
        //}
        public static string GetHighestScoreYearly(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            var highestScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0).ToList().OrderByDescending(d => d).FirstOrDefault();
            return highestScore.ToString();
        }

        public static string GetLowestScoreYearly(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            var lowestScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0).ToList().OrderBy(d => d).FirstOrDefault();
            return lowestScore.ToString();
        }

        public static string GetAverageScoreYearly(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            //Term 2 score exist
            if (gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && !string.IsNullOrEmpty(w.Term2Score)).Count() > 0)
            {
                var totalScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Sum(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0);
                var totalSubject = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0).Count();
                var averageScore = Math.Round(((totalScore ?? 0) / (totalSubject * 200)) * 100, 2);
                return double.IsNaN(averageScore) ? "" : averageScore.ToString();
            }
            else  //Term 2 Score Doesn't Exist
            {
                var totalScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Sum(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0);
                var totalSubject = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (s.Term1AndTerm2Score != null) ? s.Term1AndTerm2Score : 0).Count();
                var averageScore = Math.Round(((totalScore ?? 0) / ((totalSubject / 2) * 100)) * 100, 2);
                return double.IsNaN(averageScore) ? "" : averageScore.ToString();
            }
        }
        public static string GetHighestScore(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            var highestScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (!string.IsNullOrEmpty(s.getScore100) ? Convert.ToDouble(s.getScore100) : 0)).ToList().OrderByDescending(d => d).FirstOrDefault();
            return highestScore.ToString();
        }

        public static string GetLowestScore(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            var lowestScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (!string.IsNullOrEmpty(s.getScore100) ? Convert.ToDouble(s.getScore100) : 0)).ToList().OrderBy(d => d).FirstOrDefault();
            return lowestScore.ToString();
        }

        public static string GetAverageScore(List<StudentGradeInfoDTO> gradeInfo, int? sPlaneID)
        {
            var totalScore = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Sum(s => (!string.IsNullOrEmpty(s.getScore100) ? Convert.ToDouble(s.getScore100) : 0));
            var totalSubject = gradeInfo.Where(w => w.sPlaneID == sPlaneID && w.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต" && w.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน").Select(s => (!string.IsNullOrEmpty(s.getScore100) ? Convert.ToDouble(s.getScore100) : 0)).Count();
            var averageScore = Math.Round((totalScore / (totalSubject * 100)) * 100, 2);
            return double.IsNaN(averageScore) ? "" : averageScore.ToString();
        }
        public static double? CalCulateGradeXNCredit(int? nTermSubLevel2, double? NCredit, double? GradeSet, string getSpecial, string getGradeLabel, string getScore100)
        {
            double? gradeXNCredit = 0;
            if (GradeSet != null && GradeSet == 4.00)
                gradeXNCredit = 4 * NCredit;
            else if (GradeSet != null && GradeSet == 3.50)
                gradeXNCredit = 3.5 * NCredit;
            else if (GradeSet != null && GradeSet == 3.00)
                gradeXNCredit = 3 * NCredit;
            else if (GradeSet != null && GradeSet == 2.50)
                gradeXNCredit = 2.5 * NCredit;
            else if (GradeSet != null && GradeSet == 2.00)
                gradeXNCredit = 2 * NCredit;
            else if (GradeSet != null && GradeSet == 1.50)
                gradeXNCredit = 1.5 * NCredit;
            else if (GradeSet != null && GradeSet == 1.00)
                gradeXNCredit = 1 * NCredit;
            else if (nTermSubLevel2 == null && !string.IsNullOrEmpty(getGradeLabel) && CheckIsDouble(getGradeLabel))
                gradeXNCredit = Convert.ToDouble(getGradeLabel) * NCredit;
            else if (getScore100 != null && GradeSet == null && getSpecial == "-1" && !string.IsNullOrEmpty(getGradeLabel))
                gradeXNCredit = Convert.ToDouble(getGradeLabel) * NCredit;
            else if (getScore100 == null && GradeSet == null && getSpecial == "-1" && !string.IsNullOrEmpty(getGradeLabel))
                gradeXNCredit = Convert.ToDouble(getGradeLabel) * NCredit;
            else if (getScore100 != null && GradeSet == null && (getSpecial == "4" || getSpecial == "10"))
                gradeXNCredit = 4 * NCredit;
            else if (getScore100 != null && GradeSet == null && getSpecial == "11")
                gradeXNCredit = 3 * NCredit;
            else if (getScore100 != null && GradeSet == null && getSpecial == "12")
                gradeXNCredit = 2 * NCredit;
            else if (getScore100 != null && GradeSet == null && getSpecial == "13")
                gradeXNCredit = 1 * NCredit;
            else if (getScore100 == null && GradeSet == null && (getSpecial == "4" || getSpecial == "10"))
                gradeXNCredit = 4 * NCredit;
            else if (getScore100 == null && GradeSet == null && getSpecial == "11")
                gradeXNCredit = 3 * NCredit;
            else if (getScore100 == null && GradeSet == null && getSpecial == "12")
                gradeXNCredit = 2 * NCredit;
            else if (getScore100 == null && GradeSet == null && getSpecial == "13")
                gradeXNCredit = 1 * NCredit;
            else gradeXNCredit = 0;

            return gradeXNCredit;
        }

        public static double ConvertToDouble(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if (double.IsNaN(OutVal) || double.IsInfinity(OutVal))
                {
                    return 0;
                }
                return OutVal;
            }
        }

        public static bool CheckIsDouble(string Value)
        {
            if (Value == null)
            {
                return false;
            }
            else
            {
                double OutVal;
                double.TryParse(Value, out OutVal);

                if ((Value != "0" && OutVal == 0) || (double.IsNaN(OutVal) || double.IsInfinity(OutVal)))
                {
                    return false;
                }
                return true;
            }
        }
        //public static IMapper GetMapper()
        //{
        //    var config = new MapperConfiguration(cfg =>
        //    {

        //        cfg.AllowNullCollections = true;
        //        cfg.CreateMap<GradeHistoryEntity.TGradeHistory, TGrade>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.TGradeHistory>, List<TGrade>>();

        //        cfg.CreateMap<GradeHistoryEntity.TGradeDetailHistory, TGradeDetail>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.TGradeDetailHistory>, List<TGradeDetail>>();

        //        cfg.CreateMap<GradeHistoryEntity.TGradeEditsHistory, TGradeEdit>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.TGradeEditsHistory>, List<TGradeEdit>>();

        //        cfg.CreateMap<GradeHistoryEntity.GetGradeDetailViewHistory_Result, GetGradeDetailView_Result>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.GetGradeDetailViewHistory_Result>, List<GetGradeDetailView_Result>>();

        //        cfg.CreateMap<GradeHistoryEntity.TB_GradeViews, TB_GradeViews>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.TB_GradeViews>, List<TB_GradeViews>>();

        //        cfg.CreateMap<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailByCourseTypeHistory_Result, GetAllStudentGradeSplitUpDetailByCourseType_Result>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailByCourseTypeHistory_Result>, List<GetAllStudentGradeSplitUpDetailByCourseType_Result>>();

        //        cfg.CreateMap<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailHistory_Result, GetAllStudentGradeSplitUpDetail_Result>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailHistory_Result>, List<GetAllStudentGradeSplitUpDetail_Result>>();

        //        cfg.CreateMap<GradeHistoryEntity.GetStudentEvaluationScoreHistory_Result, GetStudentEvaluationScore_Result>();
        //        //cfg.CreateMap<List<GradeHistoryEntity.GetStudentEvaluationScoreHistory_Result>, List<GetStudentEvaluationScore_Result>>();
        //    });
        //    IMapper iMapper = config.CreateMapper();

        //    return iMapper;
        //}

        public static List<PlanDTO> GetPlansByCurriculumId(int curriculumId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var planDTOs = (from plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
                                where plan.CurriculumId == curriculumId && plan.IsActive == true && plan.SchoolID == userData.CompanyID
                                select new PlanDTO
                                {
                                    PlanId = plan.PlanId,
                                    PlanName = plan.PlanName,
                                    EducationLevels = schoolDbContext.VGetPlanRoomLists.Where(w => w.PlanId == plan.PlanId && w.SchoolID == userData.CompanyID).OrderBy(o => o.SortBy).Select(s => s.SubLevel + "/" + s.nTSubLevel2).ToList()
                                }).ToList();
                return planDTOs;
            }
            //return PlanService.GetPlansByCurriculumId(curriculumId, Utils.GetSchoolId(), Utils.GetUserId());
        }

        //public static PlanDTO GetPlanDetailsByPlanId(int curriculumId)
        //{
        //    return PlanService.GetPlanDetailsByPlanId(curriculumId, Utils.GetSchoolId(), Utils.GetUserId());
        //}

        public static List<CurriculumDTO> GetCurriculumByYear(int nYear, int schoolId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            //using (var schoolDbContext = new JabJaiEntities(connectionString))
            //{
            var curriculumDTOs = (from a in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID)
                                  join b in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID) on a.CurriculumId equals b.CurriculumId into jab
                                  from jb in jab.DefaultIfEmpty()
                                  where a.nYear == nYear && a.SchoolId == schoolId && a.IsActive == true
                                  group jb by new { a.CurriculumId, a.CurriculumName } into gb
                                  select new CurriculumDTO
                                  {
                                      CurriculumId = gb.Key.CurriculumId,
                                      CurriculumName = gb.Key.CurriculumName,
                                      PlanCount = gb.Count(c => c.CurriculumId == gb.Key.CurriculumId && c.IsActive == true)
                                  }).ToList();
            return curriculumDTOs;
            //}

            //return PlanService.GetCurriculumByYear(nYear, schoolId, Utils.GetUserId());
        }

        public static bool CheckYearHaveActiveTerm(int nYear, int schoolId)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            var terms = schoolDbContext.TTerms.Where(c => (string.IsNullOrEmpty(c.cDel) || c.cDel == "0") && c.nYear == nYear && c.SchoolID == userData.CompanyID).FirstOrDefault();
            if (terms != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<PlanCourseDTO> GetCoursesForCreatePlan(int nTSubLevel, int nYear)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            using (var schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var courseTerm = schoolDbContext.TTerms.Where(w => w.nYear == nYear && string.IsNullOrEmpty(w.cDel) && w.SchoolID == userData.CompanyID).Select(s => new PlanCourseTermDTO
                {
                    IsActive = true,
                    NTerm = s.nTerm,
                    TermNumber = s.sTerm
                }).OrderBy(o => o.TermNumber).ToList();

                var planCourseDTO = (from a in schoolDbContext.TPlanes
                                     join cg in schoolDbContext.TCourseGroups on a.courseGroup equals cg.courseGroupId
                                     join ct in schoolDbContext.TCourseTypes on a.courseType equals ct.courseTypeId
                                     where a.nTSubLevel == nTSubLevel.ToString() && string.IsNullOrEmpty(a.cDel) && a.SchoolID == userData.CompanyID
                                     select new PlanCourseDTO
                                     {
                                         CourseName = a.sPlaneName,
                                         CourseType = a.courseType,
                                         CourseGroup = a.courseGroup,
                                         CourseCode = a.courseCode,
                                         SPlaneId = a.sPlaneID,
                                         NCredit = a.nCredit,
                                         CourseTotalHour = a.courseTotalHour,
                                         CourseStatus = 1,
                                         CourseHour = a.courseHour,
                                         IsModified = true,
                                         NOrder = ct.nOrder ?? 0
                                     }).ToList();

                planCourseDTO.ForEach(f => f.PlanCourseTermDTOs = courseTerm);
                planCourseDTO = planCourseDTO.OrderBy(o => o.CourseGroup).ThenBy(o => o.NOrder).ThenBy(o => o.CourseCode).ToList();
                return planCourseDTO;
            }
            //return PlanService.GetCoursesForCreatePlan(nTSubLevel, nYear, Utils.GetSchoolId(), Utils.GetUserId());
        }

        public static List<PlanCourseDTO> GetPlanCourses(int nTSubLevel, int nTermSubLevel2, string nTerm, int nYear, int schoolId, JabJaiEntities schoolDbContext, bool thaiLanguage = true)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            if (nTSubLevel == 0 && nTermSubLevel2 != 0)
            {
                var tTermSubLevel2 = schoolDbContext.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2 && f.SchoolID == userData.CompanyID);
                nTSubLevel = tTermSubLevel2?.nTSubLevel ?? 0;
            }



            var getPlanCourses = schoolDbContext.GetPlanCourse(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId).ToList();


            var planCourseDTO = new List<PlanCourseDTO>();
            if (getPlanCourses != null)

            {
                planCourseDTO = (from a in getPlanCourses.ToList()
                                 join y in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID) on a.courseType equals y.courseTypeId
                                 join g in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == userData.CompanyID) on a.courseGroup equals g.courseGroupId
                                 join t in schoolDbContext.TTerms.Where(w => w.SchoolID == userData.CompanyID) on a.nTerm equals t.nTerm
                                 orderby a.courseCode
                                 select new PlanCourseDTO
                                 {
                                     NYear = nYear,
                                     CourseName = (thaiLanguage) ? a.sPlaneName : (string.IsNullOrEmpty(a.CourseNameEn) ? a.sPlaneName : a.CourseNameEn),
                                     CourseType = a.courseType,
                                     CourseTypeName = (!thaiLanguage && !string.IsNullOrEmpty(y.DescriptionEn)) ? y.DescriptionEn : y.Description,
                                     CourseTypeNameEng = y.DescriptionEn,
                                     CourseGroup = a.courseGroup,
                                     CourseGroupName = (!thaiLanguage && !string.IsNullOrEmpty(g.DescriptionEn)) ? g.DescriptionEn : g.Description,
                                     CourseGroupNameEng = g.DescriptionEn,
                                     CourseCode = (thaiLanguage) ? a.courseCode : (string.IsNullOrEmpty(a.CourseCodeEn) ? a.courseCode : a.CourseCodeEn),
                                     SPlaneId = a.sPlaneID,
                                     NCredit = a.nCredit,
                                     CourseTotalHour = a.CourseTotalHour,
                                     CourseStatus = a.CourseStatus,
                                     CourseHour = a.CourseHour,
                                     NTSubLevel = nTSubLevel.ToString(),
                                     NTerm = a.nTerm,
                                     PlanCourseId = a.PlanCourseId,
                                     nTermSubLevel2 = nTermSubLevel2,
                                     NOrder = y.nOrder ?? 0,
                                     PlanId = a.PlanId,
                                     STerm = t.sTerm
                                 }).ToList();

            }

            if (planCourseDTO != null)
            {
                planCourseDTO = planCourseDTO.DistinctBy(d => d.SPlaneId).OrderBy(w => w.CourseGroup).ThenBy(w => w.NOrder).ThenBy(w => w.CourseCode).ToList();
            }


            //return CommonService.GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId, Utils.GetUserId());
            return planCourseDTO;
        }

        public static List<PlanCourseDTO> GetPlanCourses(int nTermSubLevel2, string nTerm, int schoolId)
        {
            //JWTToken token = new JWTToken();
            //var userData = new JWTToken().UserData;
            //if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {

                var tTermSubLevel2 = schoolDbContext.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2 && f.SchoolID == schoolId);
                var nTSubLevel = tTermSubLevel2?.nTSubLevel ?? 0;

                var nYear = schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm).Select(s => s.nYear).FirstOrDefault();

                var getPlanCourses = schoolDbContext.GetPlanCourse(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId).ToList();

                var planCourseDTO = new List<PlanCourseDTO>();
                if (getPlanCourses != null)

                {
                    planCourseDTO = (from a in getPlanCourses.ToList()
                                     join y in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals y.courseTypeId
                                     join g in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
                                     orderby a.courseCode
                                     select new PlanCourseDTO
                                     {
                                         NYear = nYear,
                                         CourseName = a.sPlaneName,
                                         CourseType = a.courseType,
                                         CourseTypeName = y.Description,
                                         CourseGroup = a.courseGroup,
                                         CourseGroupName = g.Description,
                                         CourseCode = a.courseCode,
                                         SPlaneId = a.sPlaneID,
                                         NCredit = a.nCredit,
                                         CourseTotalHour = a.CourseTotalHour,
                                         CourseStatus = a.CourseStatus,
                                         CourseHour = a.CourseHour,
                                         NTSubLevel = nTSubLevel.ToString(),
                                         NTerm = a.nTerm,
                                         PlanCourseId = a.PlanCourseId,
                                         nTermSubLevel2 = nTermSubLevel2,
                                         NOrder = y.nOrder ?? 0
                                     }).ToList();

                }

                if (planCourseDTO != null)
                {
                    planCourseDTO = planCourseDTO.DistinctBy(d => d.SPlaneId).OrderBy(w => w.CourseGroup).ThenBy(w => w.NOrder).ThenBy(w => w.CourseCode).ToList();
                }


                //return CommonService.GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId, Utils.GetUserId());
                return planCourseDTO;
            }
        }

        public static List<ScheduleDTO> CheckTimeTableImportData(List<ScheduleDTO> scheduleDTOs, List<PlanCourseDTO> planCourseDTOs, int schoolId)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                var schedule = (from a in scheduleDTOs
                                join b in dbmaster.TUsers.Where(w => w.nCompany == schoolId && w.cType == "1" && !string.IsNullOrEmpty(w.username)).ToList() on a.username equals b.username into jab
                                from jb in jab.DefaultIfEmpty()
                                where !string.IsNullOrEmpty(a.CourseCode)
                                select new ScheduleDTO
                                {
                                    Day = a.Day,
                                    CourseCode = a.CourseCode,
                                    sEmp = jb?.sID,
                                    teacher_name = jb == null ? "" : jb.sName + " " + jb.sLastname,
                                    username = a.username,
                                    time_end = string.Format("{0:00.00}", decimal.Parse(a.time_end.Replace(":", "."))),
                                    time_start = string.Format("{0:00.00}", decimal.Parse(a.time_start.Replace(":", "."))),
                                    IsValidData = (planCourseDTOs != null && planCourseDTOs.Where(c => c.CourseCode == a.CourseCode).Count() > 0 && !string.IsNullOrEmpty(a.Day) && (a.Day == "จันทร์" || a.Day == "อังคาร" || a.Day == "พุธ" || a.Day == "พฤหัสบดี" || a.Day == "ศุกร์" || a.Day == "เสาร์" || a.Day == "อาทิตย์")) ? "ถูกต้อง" : "โมฆะ",
                                }).ToList();

                schedule = (from a in schedule
                            join b in planCourseDTOs on a.CourseCode equals b.CourseCode into TT
                            from jb in TT.DefaultIfEmpty()
                            where !string.IsNullOrEmpty(a.CourseCode)
                            select new ScheduleDTO
                            {
                                Day = a.Day,
                                CourseCode = a.CourseCode,
                                sEmp = a.sEmp,
                                teacher_name = a.teacher_name,
                                username = a.username,
                                time_end = a.time_end,
                                time_start = a.time_start,
                                IsValidData = a.IsValidData,
                                sPlaneID = jb == null ? 0 : jb.SPlaneId,
                                SchoolID = schoolId
                            }).DistinctBy(d => new { d.Day, d.CourseCode, d.username, d.time_end, d.time_start, d.IsValidData, d.sPlaneID }).ToList();

                return schedule;
            }
        }

        public static List<ScheduleDTO> CheckTimeTableImportDataFromPdf(List<ImportTimeTable> scheduleDTOs, List<PlanCourseDTO> planCourseDTOs, int schoolId)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                foreach (var s in scheduleDTOs)
                {
                    //var planCourse = planCourseDTOs.Where(w => w.CourseCode == s.course_code).FirstOrDefault();
                    //if (planCourse != null)
                    //{
                    //    var teachersId = planCourse.PlanCourseTeacherDTOs.Select(t => t.SEmp).ToList();
                    //    s.user_name = dbmaster.TUsers.Where(w => w.nCompany == schoolId && teachersId.Contains(w.sID) && w.cType == "1" && s.user_name.Contains(w.sName) && s.user_name.Contains(w.sLastname)).Select(u => u.username).FirstOrDefault();
                    //}
                    if (!string.IsNullOrEmpty(s.teacher_name) && s.sEmp == 0 && string.IsNullOrEmpty(s.user_name) && s.planCourseTeacherDTOs != null)
                    {
                        var teacherInfo = s.planCourseTeacherDTOs.Where(w => w.TeacherFullName == s.teacher_name).FirstOrDefault();
                        if (teacherInfo != null)
                        {
                            s.user_name = dbmaster.TUsers.Where(w => w.nCompany == schoolId && w.sID == teacherInfo.SEmp && w.cType == "1" && !string.IsNullOrEmpty(w.username)).Select(s1 => s1.username).FirstOrDefault();
                        }
                    }

                }

                var schedule = (from a in scheduleDTOs
                                join b in dbmaster.TUsers.Where(w => w.nCompany == schoolId && w.cType == "1" && !string.IsNullOrEmpty(w.username)).ToList() on a.user_name equals b.username into jab
                                from jb in jab.DefaultIfEmpty()
                                where !string.IsNullOrEmpty(a.course_code)
                                select new ScheduleDTO
                                {
                                    Day = a.day,
                                    CourseCode = a.course_code,
                                    sEmp = jb?.sID,
                                    teacher_name = jb == null ? "" : jb.sName + " " + jb.sLastname,
                                    username = a.user_name,
                                    time_end = string.Format("{0:00.00}", decimal.Parse(a.time_end.Replace("..", ".").Replace(":", "."))),
                                    time_start = string.Format("{0:00.00}", decimal.Parse(a.time_start.Replace("..", ".").Replace(":", "."))),
                                    IsValidData = (planCourseDTOs != null && planCourseDTOs.Where(c => c.CourseCode == a.course_code).Count() > 0 && !string.IsNullOrEmpty(a.day) && (a.day == "จันทร์" || a.day == "อังคาร" || a.day == "พุธ" || a.day == "พฤหัสบดี" || a.day == "ศุกร์" || a.day == "เสาร์" || a.day == "อาทิตย์")) ? "ถูกต้อง" : "โมฆะ",
                                }).ToList();



                schedule = (from a in schedule
                            join b in planCourseDTOs on a.CourseCode equals b.CourseCode into TT
                            from jb in TT.DefaultIfEmpty()
                            where !string.IsNullOrEmpty(a.CourseCode)
                            select new ScheduleDTO
                            {
                                Day = a.Day,
                                CourseCode = a.CourseCode,
                                sEmp = a.sEmp,
                                teacher_name = a.teacher_name,
                                username = a.username,
                                time_end = a.time_end,
                                time_start = a.time_start,
                                IsValidData = a.IsValidData,
                                sPlaneID = jb == null ? 0 : jb.SPlaneId,
                                SchoolID = schoolId
                            }).DistinctBy(d => new { d.Day, d.CourseCode, d.username, d.time_end, d.time_start, d.IsValidData, d.sPlaneID }).ToList();

                return schedule;
            }
        }

        public static List<ClassTeacherDTO> GetClassTeachersInfo(int nTermSubLevel2, string nTerm, int sPlaneId, int schoolId, int planCourseId)
        {
            return CommonService.GetClassTeachersInfo((int)nTermSubLevel2, nTerm, sPlaneId, schoolId, planCourseId);
        }

        public static List<PlanCourseTeacherDTO> GetTeachersForAPlanCourse(int nTermSubLevel2, string nTerm, int sPlaneId, int schoolId, int sEmp)
        {
            try
            {
                using (var schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
                {
                    var planCourseTeacherDTO = PlanService.GetTeachersForAPlanCourse(nTermSubLevel2, nTerm, sPlaneId, schoolId, sEmp).ToList();
                    if (planCourseTeacherDTO != null)
                    {
                        foreach (var p in planCourseTeacherDTO)
                        {
                            var query = string.Format(@"SELECT distinct ISNULL(tl.titleDescription, e.sTitle) + ' ' + e.sName + ' ' + e.sLastname 'Name' 
							FROM TEmployees e
							LEFT JOIN TTitleList tl ON e.SchoolID = tl.SchoolID AND e.sTitle = CAST(tl.nTitleid AS VARCHAR(5))
							WHERE e.sEmp = {0}", p.SEmp);

                            var teachers = schoolDbContext.Database.SqlQuery<string>(query).ToArray();
                            p.TeacherFullName = string.Join(", ", teachers);
                        }
                    }
                    return planCourseTeacherDTO;
                }
            }
            catch (Exception ex)
            {

                return new List<PlanCourseTeacherDTO>();
            }
        }
        //public static List<PlanCourseDTO> GetPlanCoursesForMultipleRooms(int sid)
        //{

        //    return CommonService.GetPlanCoursesForMultipleRooms(new List<XMLClassParameter>(), sid, Utils.GetSchoolId(), Utils.GetUserId());
        //}

      //  public static List<PlanCourseAdjustedOrderDTO> GetPlanCoursesWithAdjustedOrder(int schoolId, int sId, int sEmp)
      //  {
      //      using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
      //      using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
      //      using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
      //      {
      //          var gradeDetail = (from gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false)
      //                             join g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId) on gd.nGradeId equals g.nGradeId
      //                             where ((g.nTermSubLevel2 == null) || !((string.IsNullOrEmpty(gd.getScore100) || gd.getScore100 == "0") && gd.getSpecial == "-1" && string.IsNullOrEmpty(gd.scoreFinalTerm) && string.IsNullOrEmpty(gd.scoreMidTerm) && string.IsNullOrEmpty(gd.ScoreBeforeMidTerm)))
      //                             select new XMLGradeParameter
      //                             {
      //                                 SPlaneID = g.sPlaneID ?? 0,
      //                                 NTerm = g.nTerm,
      //                                 NTermSubLevel2 = g.nTermSubLevel2 ?? 0,
      //                                 NGradeId = g.nGradeId,
      //                                 PlanId = g.PlanId

      //                             }).ToList();

      //          if (gradeDetail != null)
      //          {
      //              var gradeDetailHistory = (from gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId && w.sID == sId && w.cDel == false)
      //                                        join g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId) on gd.nGradeId equals g.nGradeId
      //                                        where ((g.nTermSubLevel2 == null) || !((string.IsNullOrEmpty(gd.getScore100) || gd.getScore100 == "0") && gd.getSpecial == "-1" && string.IsNullOrEmpty(gd.scoreFinalTerm) && string.IsNullOrEmpty(gd.scoreMidTerm) && string.IsNullOrEmpty(gd.ScoreBeforeMidTerm)))
      //                                        select new XMLGradeParameter
      //                                        {
      //                                            SPlaneID = g.sPlaneID ?? 0,
      //                                            NTerm = g.nTerm,
      //                                            NTermSubLevel2 = g.nTermSubLevel2 ?? 0,
      //                                            NGradeId = g.nGradeId,
      //                                            PlanId = g.PlanId
      //                                        }).ToList();

      //              gradeDetail.AddRange(gradeDetailHistory);
      //          }

      //          var xmlCourseList = string.Empty;
      //          foreach (var p in gradeDetail)
      //          {
      //              var nTermSubLevel2s = new List<int>();
      //              if (p.NTermSubLevel2 == 0 && p.PlanId != null)
      //              {

      //                  //If data imported and PlanId updated in the TGrade table fetch the room id
      //                  nTermSubLevel2s = schoolDbContext.TPlanTermSubLevel2.Where(w => w.SchoolID == schoolId && w.PlanId == p.PlanId && w.IsActive == true).Select(s => s.nTermSubLevel2).ToList();

      //              }
      //              else if (p.NTermSubLevel2 == 0 && p.PlanId == null)
      //              {
      //                  //If data imported but PlanId not updated in the TGrade Table then fetch the room id from class
      //                  var nTSubLevel = schoolDbContext.TPlanes.Where(w => w.SchoolID == schoolId && w.sPlaneID == p.SPlaneID).Select(s => s.nTSubLevel).FirstOrDefault();
      //                  nTermSubLevel2s = schoolDbContext.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel.ToString() == nTSubLevel && w.nWorkingStatus == 1).Select(s => s.nTermSubLevel2).ToList();
      //              }
      //              else
      //              {
      //                  xmlCourseList += string.Format(@"
						//<XMLGradeParameter>
						//	<NGradeId>{0}</NGradeId>
						//	<NTerm>{1}</NTerm>
						//	<NTermSubLevel2>{2}</NTermSubLevel2>
						//	<SPlaneID>{3}</SPlaneID>
						//	 <PlanId>{4}</PlanId>
						//</XMLGradeParameter>", p.NGradeId, p.NTerm, p.NTermSubLevel2, p.SPlaneID, p.PlanId);
      //              }

      //              if (nTermSubLevel2s.Count > 0)
      //              {
      //                  foreach (var r in nTermSubLevel2s)
      //                  {
      //                      xmlCourseList += string.Format(@"
						//		<XMLGradeParameter>
						//			<NGradeId>{0}</NGradeId>
						//			<NTerm>{1}</NTerm>
						//			<NTermSubLevel2>{2}</NTermSubLevel2>
						//			<SPlaneID>{3}</SPlaneID>
						//			 <PlanId>{4}</PlanId>
						//		</XMLGradeParameter>", p.NGradeId, p.NTerm, r, p.SPlaneID, p.PlanId);
      //                  }
      //              }
      //          }

      //          var query = string.Format(@"EXEC GetPlanCoursesWithAdjustedOrder {0},{1},'<ArrayOfXMLGradeParameter>{2}</ArrayOfXMLGradeParameter>'", schoolId, sId, xmlCourseList);

      //          var planCourseWithAdjustedOrder = schoolDbContext.Database.SqlQuery<GetPlanCoursesWithAdjustedOrder_Result>(query).ToList();
      //          //var planCourseWithAdjustedOrder = schoolDbContext.GetPlanCoursesWithAdjustedOrder(schoolId, sId);


      //          var planCourseAdjustedOrderDTO = (from a in planCourseWithAdjustedOrder
      //                                            join y in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals y.courseTypeId
      //                                            join g in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
      //                                            orderby a.courseCode
      //                                            select new PlanCourseAdjustedOrderDTO
      //                                            {
      //                                                CourseName = a.sPlaneName,
      //                                                CourseType = a.courseType,
      //                                                CourseGroup = a.courseGroup,
      //                                                CourseCode = a.courseCode,
      //                                                SPlaneId = a.sPlaneID.ToString(),
      //                                                NCredit = a.nCredit,
      //                                                CourseTotalHour = a.CourseTotalHour,
      //                                                CourseStatus = a.CourseStatus,
      //                                                CourseHour = a.CourseHour,
      //                                                NTSubLevel = a.nTSubLevel.ToString(),
      //                                                NYear = a.nYear,
      //                                                STerm = a.sTerm,
      //                                                NumberYear = a.numberYear,
      //                                                NTerm = a.nTerm,
      //                                                NGradeId = a.nGradeId,
      //                                                Grade_nTerm = a.Grade_nTerm,
      //                                                RB1DisplayOrder = a.RB1DisplayOrder,
      //                                                CourseTypeName = y.Description,
      //                                                CourseGroupName = g.Description,
      //                                                NTermSubLevel2 = a.nTermSubLevel2
      //                                            }).DistinctBy(d => d.NGradeId).ToList();
      //          return planCourseAdjustedOrderDTO;
      //      }

      //      //return CommonService.GetPlanCoursesWithAdjustedOrder(schoolId, sId, sEmp);
      //  }

        public static List<PlanCourseDTO> GetPlanCoursesWithTeachers(int nTSubLevel, int nTermSubLevel2, string nTerm, int nYear, int schoolId, JabJaiEntities schoolDbContext)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                if (schoolDbContext == null)
                    schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));

                if (nTSubLevel == 0 && nTermSubLevel2 != 0)
                    nTSubLevel = schoolDbContext.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2 && f.SchoolID == userData.CompanyID).nTSubLevel;

                var getPlanCourses = schoolDbContext.GetPlanCourse(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId);

                var planCourseDTO = (from a in getPlanCourses.ToList()
                                     join y in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID) on a.courseType equals y.courseTypeId
                                     join g in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == userData.CompanyID) on a.courseGroup equals g.courseGroupId
                                     orderby a.courseCode
                                     select new PlanCourseDTO
                                     {
                                         NYear = nYear,
                                         CourseName = a.sPlaneName,
                                         CourseType = a.courseType,
                                         CourseGroup = a.courseGroup,
                                         CourseCode = a.courseCode,
                                         SPlaneId = a.sPlaneID,
                                         NCredit = a.nCredit,
                                         CourseTotalHour = a.CourseTotalHour,
                                         CourseStatus = a.CourseStatus,
                                         CourseHour = a.CourseHour,
                                         NTSubLevel = nTSubLevel.ToString(),
                                         NTerm = a.nTerm,
                                         PlanCourseId = a.PlanCourseId,
                                         CourseTypeName = y.Description,
                                         CourseGroupName = g.Description,
                                         PlanCourseTeacherDTOs = (from e in schoolDbContext.TPlanCourseTeachers
                                                                  where e.SchoolID == userData.CompanyID && e.PlanCourseId == a.PlanCourseId && e.IsActive == true
                                                                  select new PlanCourseTeacherDTO
                                                                  {
                                                                      PlanCourseId = e.PlanCourseId,
                                                                      SEmp = e.sEmp
                                                                  }).ToList()
                                     }).ToList();


                //var planCourseDTOs = _unitOfWork.PlanRepository.GetPlanCoursesWithTeachers(nTSubLevel, nTermSubLevel2, nTerm, nYear, schoolId);
                if (planCourseDTO != null)
                    planCourseDTO = planCourseDTO.DistinctBy(d => d.SPlaneId).ToList();

                return planCourseDTO;
            }
            catch (Exception ex)
            {
                //var parameters = string.Format("nTSubLevel: {0} nTermSubLevel2:{1} nTerm:{2} nYear:{3}", nTSubLevel, nTermSubLevel2, nTerm, nYear);
                //Common.CreateExceptionLog("FingerprintPayment", ex, schoolId, sEmp, "CommonService.GetPlanCoursesWithTeachers", parameters, "", _unitOfWork);
                return new List<PlanCourseDTO>();
            }
        }

        public static List<PlanCourseTeacherDTO> GetTeachersForAPlanCourse(int nTermSubLevel2, string nTerm, int sPlaneId, JabJaiEntities schoolDbContext, int nPlaneDay, int scheduleid)
        {
            //using (var schoolDbContext = new JabJaiEntities(connectionString))
            //{
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


            var tSubLevel = schoolDbContext.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2);
            List<PlanCourseTeacherDTO> planCourseTeacherDTOs = new List<PlanCourseTeacherDTO>();
            if (tSubLevel != null)
            {
                var planCourseDTO = (from a in schoolDbContext.TPlanCourses.Where(w => w.SchoolID == userData.CompanyID)
                                     join b in schoolDbContext.TPlanes.Where(w => w.SchoolID == userData.CompanyID) on a.sPlaneID equals b.sPlaneID into planCourse
                                     from pc in planCourse.DefaultIfEmpty()
                                     join ct in schoolDbContext.TPlanCourseTerms.Where(w => w.SchoolID == userData.CompanyID) on a.PlanCourseId equals ct.PlanCourseId
                                     join t in schoolDbContext.TTerms.Where(w => w.SchoolID == userData.CompanyID) on ct.nTerm equals t.nTerm
                                     join curr in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on t.nYear equals curr.nYear
                                     join plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID) on new { curr.CurriculumId, a.PlanId } equals new { plan.CurriculumId, plan.PlanId }
                                     join r in schoolDbContext.TPlanTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on plan.PlanId equals r.PlanId
                                     join cType in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == userData.CompanyID) on pc.courseType equals cType.courseTypeId
                                     where pc.nTSubLevel == tSubLevel.nTSubLevel.ToString() && string.IsNullOrEmpty(pc.cDel) && a.CourseStatus == 1 && t.nTerm == nTerm
                                     && (sPlaneId == 0 || pc.sPlaneID == sPlaneId) && a.IsActive == true && curr.IsActive == true && plan.IsActive == true && a.SchoolID == userData.CompanyID && r.nTermSubLevel2 == nTermSubLevel2
                                     orderby new { pc.courseGroup, cType.courseTypeId, pc.courseCode }
                                     select new PlanCourseDTO
                                     {
                                         PlanCourseId = a.PlanCourseId,
                                         PlanCourseTeacherDTOs = (from pct in schoolDbContext.TPlanCourseTeachers.Where(w => w.SchoolID == userData.CompanyID)
                                                                  join emp in schoolDbContext.TEmployees.Where(w => w.SchoolID == userData.CompanyID) on pct.sEmp equals emp.sEmp
                                                                  where pct.SchoolID == userData.CompanyID && pct.PlanCourseId == a.PlanCourseId && emp.cDel == null && pct.IsActive == true

                                                                  select new PlanCourseTeacherDTO
                                                                  {
                                                                      PlanCourseTeacherId = pct.PlanCourseTeacherId,
                                                                      PlanCourseId = pct.PlanCourseId,
                                                                      SEmp = pct.sEmp,
                                                                      SName = emp.sName,
                                                                      SLastname = emp.sLastname,
                                                                      IsTimeTableScheduled = ((from ts in schoolDbContext.TSchedules.Where(w => w.SchoolID == userData.CompanyID && w.sScheduleID == scheduleid && pct.sEmp == w.sEmp && w.cDel == null)
                                                                                               select ts).ToList().Count() > 0) ? true : false
                                                                  }).ToList(),
                                         PlanId = plan.PlanId
                                     }).ToList();

                //planCourseDTO = (from p in planCourseDTO
                //                   join t in schoolDbContext.TPlanTermSubLevel2 on p.PlanId equals t.PlanId
                //                   where t.nTermSubLevel2 == nTermSubLevel2
                //                   select p).ToList();

                planCourseTeacherDTOs = planCourseDTO.SelectMany(s => s.PlanCourseTeacherDTOs).ToList();
            }

            return planCourseTeacherDTOs;
            //}
        }
        public static List<PlanCourseDTO> GetPlanCoursesWithTerm(int nTSubLevel, int nTermSubLevel2, int nYear, int schoolId, JabJaiEntities schoolDbContext, bool thaiLanguage = true)
        {
            var planCourseDTO = new List<PlanCourseDTO>();

            if (nTSubLevel == 0 && nTermSubLevel2 != 0)
                nTSubLevel = schoolDbContext.TTermSubLevel2.FirstOrDefault(f => f.nTermSubLevel2 == nTermSubLevel2).nTSubLevel;

            var getPlanCourses = schoolDbContext.GetPlanCourse(nTSubLevel, nTermSubLevel2, "", nYear, schoolId);

            if (getPlanCourses != null)
            {
                planCourseDTO = (from a in getPlanCourses.ToList()
                                 join y in schoolDbContext.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals y.courseTypeId
                                 join g in schoolDbContext.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
                                 orderby a.courseCode
                                 select new PlanCourseDTO
                                 {
                                     CourseName = (thaiLanguage) ? a.sPlaneName : (string.IsNullOrEmpty(a.CourseNameEn) ? a.sPlaneName : a.CourseNameEn),
                                     CourseType = a.courseType,
                                     CourseTypeName = (!thaiLanguage && !string.IsNullOrEmpty(y.DescriptionEn)) ? y.DescriptionEn : y.Description,
                                     CourseTypeNameEng = y.DescriptionEn,
                                     CourseGroup = a.courseGroup,
                                     CourseGroupName = (!thaiLanguage && !string.IsNullOrEmpty(g.DescriptionEn)) ? g.DescriptionEn : g.Description,
                                     CourseCode = (thaiLanguage) ? a.courseCode : (string.IsNullOrEmpty(a.CourseCodeEn) ? a.courseCode : a.CourseCodeEn),
                                     CourseGroupNameEng = g.DescriptionEn,
                                     SPlaneId = a.sPlaneID,
                                     NCredit = a.nCredit,
                                     CourseTotalHour = a.CourseTotalHour,
                                     CourseStatus = a.CourseStatus,
                                     CourseHour = a.CourseHour,
                                     NTerm = a.nTerm,
                                     NOrder = y.nOrder ?? 0
                                 }).ToList();

            }
            if (planCourseDTO != null)
                planCourseDTO = planCourseDTO.DistinctBy(d => new { d.SPlaneId, d.NTerm }).ToList();
            return planCourseDTO;
            //return CommonService.GetPlanCoursesWithTerm(nTSubLevel, nTermSubLevel2, nYear, schoolId, Utils.GetUserId());
        }

        public static List<PlanCourseDTO> GetSubjectsByTeacherAndRoom(int nYear, string nTerm, int nTSublevel, int nTermSubLevel2, int sEmpId, int schoolId)
        {
            var teacherSubject = new List<PlanCourseDTO>();
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                //var nTerm = schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.sTerm == sTerm && w.nYear == nYear && w.cDel == null).Select(s => s.nTerm).FirstOrDefault();

                var plancourseDTOs = GetPlanCourses(nTSublevel, nTermSubLevel2, nTerm, nYear, schoolId, schoolDbContext);
                if (plancourseDTOs != null)
                {
                    teacherSubject = (from p in plancourseDTOs
                                      join t in schoolDbContext.TPlanCourseTeachers.Where(w => w.SchoolID == schoolId && w.IsActive == true && w.sEmp == sEmpId).ToList() on p.PlanCourseId equals t.PlanCourseId
                                      select p).OrderBy(o => o.CourseGroup).ThenBy(o => o.NOrder).ThenBy(o => o.CourseCode).ToList();

                }
            }
            return teacherSubject;
        }

        public static List<PlanCourseTeacherDTO> GetTeachersFromPlan(int nYear, string nTerm, int nTSublevel, int nTermSubLevel2, int schoolId)
        {
            var teachers = new List<PlanCourseTeacherDTO>();
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                //var nTerm = schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.sTerm == sTerm && w.nYear == nYear && w.cDel == null).Select(s => s.nTerm).FirstOrDefault();

                var plancourseDTOs = GetPlanCourses(nTSublevel, nTermSubLevel2, nTerm, nYear, schoolId, schoolDbContext);
                if (plancourseDTOs != null)
                {
                    var planTeachers = (from p in plancourseDTOs
                                        join t in schoolDbContext.TPlanCourseTeachers.Where(w => w.SchoolID == schoolId && w.IsActive == true).ToList() on p.PlanCourseId equals t.PlanCourseId
                                        join e in schoolDbContext.TEmployees.Where(w => w.SchoolID == schoolId && w.cDel == null) on t.sEmp equals e.sEmp
                                        //orderby new { p.CourseGroup, p.NOrder, p.CourseCode }
                                        select new { p.CourseGroup, p.NOrder, p.CourseCode, e.sEmp, e.sName, e.sLastname }).DistinctBy(s => s.sEmp).OrderBy(o => o.CourseGroup).ThenBy(o => o.NOrder).ThenBy(o => o.CourseCode).ToList();
                    if (planTeachers != null)
                    {
                        teachers = planTeachers.Select(s => new PlanCourseTeacherDTO { SEmp = s.sEmp, SName = s.sName, SLastname = s.sLastname, TeacherFullName = string.Format("{0} {1}", s.sName ?? string.Empty, s.sLastname ?? string.Empty) }).ToList();
                    }
                }
            }
            return teachers;
        }

        public static CurriculumDTO AddOrUpdateCurriculum(CurriculumDTO curriculumDTO)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var curriculum = new TCurriculum();
                if (curriculumDTO != null)
                {
                    curriculum = schoolDbContext.TCurriculums.FirstOrDefault(f => f.CurriculumId == curriculumDTO.CurriculumId);
                    if (curriculum == null) curriculum = new TCurriculum();
                    curriculum.CurriculumName = curriculumDTO.CurriculumName;

                    curriculum.UpdatedDate = curriculumDTO.UpdatedDate;
                    curriculum.UpdatedBy = curriculumDTO.UpdatedBy;

                    if (curriculum.CurriculumId == 0)
                    {
                        curriculum.nYear = curriculumDTO.NYear;
                        curriculum.CreatedDate = curriculumDTO.CreatedDate;
                        curriculum.CreatedBy = curriculumDTO.CreatedBy;
                        curriculum.SchoolId = curriculumDTO.SchoolId;
                        curriculum.IsActive = true;
                        schoolDbContext.TCurriculums.Add(curriculum);

                    }
                    schoolDbContext.SaveChanges();
                    curriculumDTO.CurriculumId = curriculum.CurriculumId;
                }

                return curriculumDTO;
            }
            //return PlanService.AddOrUpdateCurriculum(curriculumDTO, Utils.GetSchoolId(), Utils.GetUserId());
        }

        public static void DeleteCurriculum(int curriculumId, int schoolId)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (curriculumId > 0 && schoolId > 0)
                {
                   
                        try
                        {
                            var curriculum = schoolDbContext.TCurriculums.FirstOrDefault(f => f.CurriculumId == curriculumId && f.SchoolId == schoolId);
                            if (curriculum != null)
                            {
                                curriculum.IsActive = false;
                                schoolDbContext.Entry(curriculum).State = EntityState.Modified;

                            }
                            schoolDbContext.SaveChanges();
                            
                        }
                        catch (Exception ex)
                        {
                           
                            throw;
                        }
                    
                }
            }
            //PlanService.DeleteCurriculum(curriculumId, schoolId, Utils.GetUserId());
        }

        public static List<PlanTermSubLevel2DTO> CheckTermSubLevel2Exist(PlanDTO planDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            var tPlanTermSubLevel2s = (from a in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
                                       join b in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on a.CurriculumId equals b.CurriculumId
                                       join c in schoolDbContext.TPlanTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.PlanId equals c.PlanId
                                       join sl2 in schoolDbContext.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on c.nTermSubLevel2 equals sl2.nTermSubLevel2
                                       join sl in schoolDbContext.TSubLevels.Where(w => w.SchoolID == userData.CompanyID) on new { sl2.nTSubLevel, planNTSubLevel = a.nTSubLevel } equals new { sl.nTSubLevel, planNTSubLevel = sl.nTSubLevel }
                                       where b.nYear == planDTO.NYear && planDTO.EducationSubLevelIds.Contains(c.nTermSubLevel2) && a.PlanId != planDTO.PlanId && c.IsActive == true && b.IsActive == true && a.IsActive == true
                                       select new PlanTermSubLevel2DTO
                                       {
                                           NTermSubLevel2 = c.nTermSubLevel2,
                                           PlanId = a.PlanId,
                                           TermSubLevel2Name = sl.SubLevel + " / " + sl2.nTSubLevel2
                                       }).ToList();

            return tPlanTermSubLevel2s;
            //return PlanService.CheckTermSubLevel2Exist(planDTO, Utils.GetSchoolId(), Utils.GetUserId());
        }

        public static List<PlanDTO> CheckPlanNameExist(PlanDTO planDTO)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
            var planDTOs = (from a in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
                            join b in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on a.CurriculumId equals b.CurriculumId
                            join c in schoolDbContext.TPlanTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID) on a.PlanId equals c.PlanId
                            where b.nYear == planDTO.NYear && a.PlanName == planDTO.PlanName && a.PlanId != planDTO.PlanId && c.IsActive == true && a.IsActive == true && b.IsActive == true
                            select new PlanDTO
                            {
                                PlanId = a.PlanId,
                                PlanName = planDTO.PlanName
                            }).ToList();
            return planDTOs;
            //return PlanService.CheckPlanNameExist(planDTO, Utils.GetSchoolId(), Utils.GetUserId());
        }

        //public static void AddOrUpdatePlanDetails(PlanDTO planDTO)
        //{
        //    PlanService.AddOrUpdatePlanDetails(planDTO, Utils.GetSchoolId(), Utils.GetUserId());
        //}

        public static YearDTO GetYearByYearNumber(int yearNumber, int schoolId, int sEmp)
        {
            //return (from y in schoolDbContext.TYears
            //        where y.numberYear == yearNumber
            //        select new YearDTO
            //        {
            //            NumberYear = y.numberYear,
            //            NYear = y.nYear,
            //            YearStatus = y.YearStatus
            //        }).FirstOrDefault();
            return CommonService.GetYearByYearNumber(yearNumber, schoolId, sEmp);

        }

        //public static GraduationDTO GetStudentGraduation(CommonRequest commonRequest)
        //{

        //    return GraduationService.GetStudentGraduation(commonRequest);
        //}

        //public static bool ValidatePlanCourseDeActivation(string sPlaneId, int planId, List<PlanCourseTermDTO> planCourseTermDTOs)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {
        //        var planDTO = (from plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
        //                       join curr in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on plan.CurriculumId equals curr.CurriculumId
        //                       where plan.PlanId == planId && plan.IsActive == true && curr.IsActive == true
        //                       select new PlanDTO
        //                       {
        //                           PlanId = plan.PlanId,
        //                           PlanName = plan.PlanName,
        //                           NTSubLevel = plan.nTSubLevel,
        //                           NYear = curr.nYear,
        //                           EducationSubLevelIds = schoolDbContext.VGetPlanRoomLists.Where(c => c.PlanId == plan.PlanId && c.SchoolID == userData.CompanyID).OrderBy(o => o.SortBy).Select(s => s.nTermSubLevel2).ToList(),

        //                       }).FirstOrDefault();

        //        if (planDTO != null && planDTO.EducationSubLevelIds != null && planCourseTermDTOs != null)
        //        {
        //            var nTerms = planCourseTermDTOs.Where(w => w.IsActive == true).Select(s => s.NTerm).ToList();

        //            //Check Exist in Time Table
        //            var tScheduleCheck = (from s in schoolDbContext.TSchedules
        //                                  join t in schoolDbContext.TTermTimeTables on s.nTermTable ?? 0 equals t.nTermTable
        //                                  where s.sPlaneID.ToString() == sPlaneId && s.cDel == null && planDTO.EducationSubLevelIds.Contains(t.nTermSubLevel2 ?? 0) && nTerms.Contains(t.nTerm)
        //                                  select s).FirstOrDefault();
        //            if (tScheduleCheck != null)
        //            {
        //                return true;
        //            }

        //            //Check Grade Exist in Grade Main Table
        //            var tGradeCheck = (from g in _dbGrade.TGrades.Where(w => w.sPlaneID.ToString() == sPlaneId && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0) && w.SchoolID == userData.CompanyID)
        //                               join gd in _dbGrade.TGradeDetails on g.nGradeId equals gd.nGradeId
        //                               where gd.cDel == false && gd.ScoreData != "" && !string.IsNullOrEmpty(gd.getScore100) && gd.getSpecial == "-1" && ((gd.getScore100 != "0" && gd.getScore100 != "0.00") || (!string.IsNullOrEmpty(gd.scoreMidTerm) && gd.scoreMidTerm != "0") || (!string.IsNullOrEmpty(gd.scoreFinalTerm) && gd.scoreFinalTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreBeforeMidTerm) && gd.ScoreBeforeMidTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreAfterMidTerm) && gd.ScoreAfterMidTerm != "0"))
        //                               select new { g.nGradeId, g.sPlaneID }).FirstOrDefault();
        //            if (tGradeCheck != null)
        //            {
        //                return true;
        //            }

        //            //Check Grade Exist In Grade History Table
        //            var tGradeCheckHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0) && w.sPlaneID.ToString() == sPlaneId)
        //                                      join gd in _dbGradeHistory.TGradeDetailHistories on g.nGradeId equals gd.nGradeId
        //                                      where gd.cDel == false && gd.ScoreData != "" && !string.IsNullOrEmpty(gd.getScore100) && gd.getSpecial == "-1" && ((gd.getScore100 != "0" && gd.getScore100 != "0.00") || (!string.IsNullOrEmpty(gd.scoreMidTerm) && gd.scoreMidTerm != "0") || (!string.IsNullOrEmpty(gd.scoreFinalTerm) && gd.scoreFinalTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreBeforeMidTerm) && gd.ScoreBeforeMidTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreAfterMidTerm) && gd.ScoreAfterMidTerm != "0"))
        //                                      select new { g.nGradeId, g.sPlaneID }).FirstOrDefault();
        //            if (tGradeCheckHistory != null)
        //            {
        //                return true;
        //            }

        //        }

        //        return false;
        //    }
        //    //return PlanService.ValidatePlanCourseDeActivation(sPlaneId, planId, planCourseTermDTOs, Utils.GetSchoolId(), Utils.GetUserId());
        //}

        //public static bool DeletePlanByPlanId(int planId, int schoolId)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {
        //        var planDTO = (from plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
        //                       join curr in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on plan.CurriculumId equals curr.CurriculumId
        //                       where plan.PlanId == planId && plan.IsActive == true && curr.IsActive == true
        //                       select new PlanDTO
        //                       {
        //                           PlanId = plan.PlanId,
        //                           PlanName = plan.PlanName,
        //                           NTSubLevel = plan.nTSubLevel,
        //                           NYear = curr.nYear,
        //                           EducationSubLevelIds = schoolDbContext.VGetPlanRoomLists.Where(c => c.PlanId == plan.PlanId && c.SchoolID == userData.CompanyID).OrderBy(o => o.SortBy).Select(s => s.nTermSubLevel2).ToList(),
        //                           PlanCourseDTOs = (from pc in schoolDbContext.TPlanCourses.Where(c => c.PlanId == plan.PlanId && c.IsActive == true && c.CourseStatus == 1 && c.SchoolID == userData.CompanyID)
        //                                             join p in schoolDbContext.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null)
        //                                             on pc.sPlaneID equals p.sPlaneID
        //                                             select new PlanCourseDTO { PlanCourseId = pc.PlanCourseId, SPlaneId = pc.sPlaneID }).ToList()

        //                       }).FirstOrDefault();

        //        var IsDeleted = false;
        //        var IsExist = false;
        //        if (planDTO != null && planDTO.EducationSubLevelIds != null && planDTO.PlanCourseDTOs != null)
        //        {

        //            foreach (var planCourseDTO in planDTO.PlanCourseDTOs)
        //            {
        //                if (!IsExist)
        //                {
        //                    var nTerms = schoolDbContext.TPlanCourseTerms.Where(c => c.PlanCourseId == planCourseDTO.PlanCourseId && c.IsActive == true && c.SchoolID == userData.CompanyID).Select(s => s.nTerm).ToList();

        //                    //Check Time Table Created
        //                    var tScheduleCheck = (from s in schoolDbContext.TSchedules.Where(w => w.SchoolID == userData.CompanyID)
        //                                          join t in schoolDbContext.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on s.nTermTable ?? 0 equals t.nTermTable
        //                                          where s.sPlaneID == planCourseDTO.SPlaneId && s.cDel == null && planDTO.EducationSubLevelIds.Contains(t.nTermSubLevel2 ?? 0) && nTerms.Contains(t.nTerm)
        //                                          select s).FirstOrDefault();
        //                    if (tScheduleCheck != null)
        //                    {
        //                        //Plan Course Exist in Time Table 
        //                        IsExist = true;
        //                    }

        //                    if (!IsExist) // Check Grade Exist in Grade Main Table
        //                    {
        //                        var tGradeCheck = _dbGrade.TGrades.Where(w => w.sPlaneID == planCourseDTO.SPlaneId && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0) && w.SchoolID == userData.CompanyID).Select(s => s).FirstOrDefault();
        //                        if (tGradeCheck != null)
        //                        {
        //                            //Plan Course Exist in Grade main table
        //                            IsExist = true;
        //                        }
        //                    }

        //                    if (!IsExist) //Check Grade Exist in Grade History Table
        //                    {
        //                        var tGradeHistoryCheck = _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0) && w.sPlaneID == planCourseDTO.SPlaneId).Select(s => s).FirstOrDefault();
        //                        if (tGradeHistoryCheck != null)
        //                        {
        //                            //Plan Course Exist in Grade History table
        //                            IsExist = true;
        //                        }
        //                    }
        //                }
        //            }

        //        }

        //        if (!IsExist)
        //        {
        //            var tPlans = schoolDbContext.TPlans.Where(w => w.PlanId == planId && w.SchoolID == userData.CompanyID).FirstOrDefault();
        //            if (tPlans != null)
        //            {
        //                tPlans.IsActive = false;
        //                schoolDbContext.SaveChanges();
        //                IsDeleted = true;
        //            }

        //        }

        //        return IsDeleted;
        //    }
        //    //return PlanService.DeletePlanByPlanId(planId, schoolId, Utils.GetUserId());
        //}

        //public static bool ValidatePlanRoomDeletion(PlanDTO planDTO)
        //{
        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

        //    JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read));
        //    JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read));
        //    GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read));
        //    if (planDTO != null && planDTO.PlanId > 0)
        //    {
        //        var existPlanDTOs = (from plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
        //                             join curr in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on plan.CurriculumId equals curr.CurriculumId
        //                             where plan.PlanId == planDTO.PlanId && plan.IsActive == true && curr.IsActive == true
        //                             select new PlanDTO
        //                             {
        //                                 PlanId = plan.PlanId,
        //                                 PlanName = plan.PlanName,
        //                                 NTSubLevel = plan.nTSubLevel,
        //                                 NYear = curr.nYear,
        //                                 EducationSubLevelIds = schoolDbContext.VGetPlanRoomLists.Where(w => w.PlanId == plan.PlanId && w.SchoolID == userData.CompanyID).OrderBy(o => o.SortBy).Select(s => s.nTermSubLevel2).ToList(),
        //                                 PlanCourseDTOs = (from a in schoolDbContext.TPlanCourses.Where(w => w.PlanId == plan.PlanId && w.CourseStatus == 1 && w.IsActive == true && w.SchoolID == userData.CompanyID)
        //                                                   join p in schoolDbContext.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.cDel == null) on a.sPlaneID equals p.sPlaneID
        //                                                   select new PlanCourseDTO
        //                                                   {
        //                                                       SPlaneId = a.sPlaneID,
        //                                                   }).ToList()

        //                             }).FirstOrDefault();

        //        if (existPlanDTOs != null && existPlanDTOs.EducationSubLevelIds != null && existPlanDTOs.PlanCourseDTOs != null && planDTO != null && planDTO.EducationSubLevelIds != null && planDTO.PlanCourseDTOs != null)
        //        {

        //            var deletedIds = existPlanDTOs.EducationSubLevelIds.Except(planDTO.EducationSubLevelIds).ToList();
        //            var planIds = existPlanDTOs.PlanCourseDTOs.Select(s => s.SPlaneId).ToList();
        //            List<string> tTerms = schoolDbContext.TTerms.Where(w => w.nYear == planDTO.NYear && w.SchoolID == userData.CompanyID && w.cDel == null).Select(s => s.nTerm).ToList();
        //            var tGradeCheck = (from g in _dbGrade.TGrades.Where(w => planIds.Contains(w.sPlaneID ?? 0) && deletedIds.Contains(w.nTermSubLevel2 ?? 0) && tTerms.Contains(w.nTerm) && w.SchoolID == userData.CompanyID)
        //                               join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
        //                               on g.nGradeId equals gd.nGradeId
        //                               select gd).FirstOrDefault();

        //            if (tGradeCheck != null && Common.CheckScoreExist(tGradeCheck))
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                var tGradeCheckHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => planIds.Contains(w.sPlaneID ?? 0) && deletedIds.Contains(w.nTermSubLevel2 ?? 0) && tTerms.Contains(w.nTerm) && w.SchoolID == userData.CompanyID)
        //                                          join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false)
        //                                          on g.nGradeId equals gd.nGradeId
        //                                          select gd).FirstOrDefault();

        //                if (tGradeCheckHistory != null)
        //                {
        //                    if (Common.CheckScoreExistHistory(tGradeCheckHistory))
        //                        return true;
        //                }
        //                else
        //                {
        //                    var tScheduleCheck = (from s in schoolDbContext.TSchedules.Where(w => w.SchoolID == userData.CompanyID)
        //                                          join t in schoolDbContext.TTermTimeTables.Where(w => w.SchoolID == userData.CompanyID) on s.nTermTable ?? 0 equals t.nTermTable
        //                                          join t1 in tTerms on t.nTerm equals t1
        //                                          where planIds.Contains(s.sPlaneID ?? 0) && s.cDel == null && deletedIds.Any(a => a == t.nTermSubLevel2)
        //                                          select s).FirstOrDefault();
        //                    if (tScheduleCheck != null)
        //                    {
        //                        return true;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //    //return PlanService.ValidatePlanRoomDeletion(planDTO, Utils.GetSchoolId(), Utils.GetUserId());
        //}


        public static List<PlanCourseTermDTO> GetPlanCourseTermDTOs(int planCourseId, int nYear)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<PlanCourseTermDTO> planCourseTermDTOs = new List<PlanCourseTermDTO>();
                if (planCourseId == 0)
                {
                    var planCourseTerm = (from ct in schoolDbContext.GetPlanCourseTerms.Where(w => w.nYear == nYear && w.SchoolID == userData.CompanyID).ToList()
                                          orderby ct.TermNumber
                                          select new
                                          {
                                              NTerm = ct.nTerm,
                                              TermNumber = ct.TermNumber

                                          }).Distinct().ToList();

                    planCourseTermDTOs = (from c in planCourseTerm
                                          select new PlanCourseTermDTO
                                          {
                                              NTerm = c.NTerm,
                                              TermNumber = c.TermNumber
                                          }).Take(3).ToList();


                }
                else
                {

                    planCourseTermDTOs = schoolDbContext.GetPlanCourseTerms.Where(w => w.PlanCourseId == planCourseId && w.nYear == nYear && w.SchoolID == userData.CompanyID).Select(s => new PlanCourseTermDTO
                    {
                        PlanCourseId = s.PlanCourseId,
                        IsActive = s.IsActive,
                        NTerm = s.nTerm,
                        TermNumber = s.TermNumber
                    }).Take(3).ToList();

                }

                if (planCourseTermDTOs != null && planCourseTermDTOs.Count < 2)
                {
                    planCourseTermDTOs = GetTerms(planCourseTermDTOs, nYear);
                }
                if (planCourseTermDTOs != null)
                {
                    planCourseTermDTOs = planCourseTermDTOs.OrderBy(o => o.TermNumber).ToList();
                }
                return planCourseTermDTOs;
            }
            //return PlanService.GetPlanCourseTermDTOs(planCourseId, nYear, Utils.GetSchoolId(), Utils.GetUserId());
        }

        public static List<TermDTO> GetTermsByNumberYear(int numberYear, bool isOrderByCurrentTerm, int schoolId)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var yeardata = schoolDbContext.TYears.Where(w => w.numberYear == numberYear && w.SchoolID == schoolId).FirstOrDefault();
                if (yeardata != null)
                {

                    var termDTOs = schoolDbContext.TTerms.Where(c => (string.IsNullOrEmpty(c.cDel) || c.cDel == "0") && c.nYear == yeardata.nYear && c.SchoolID == schoolId).Select(s => new TermDTO { sTerm = s.sTerm, nTerm = s.nTerm, nYear = (int)s.nYear, dStart = s.dStart, dEnd = s.dEnd }).ToList();

                    if (termDTOs != null && isOrderByCurrentTerm && termDTOs.Count() > 1)
                    {
                        var currentDateTime = DateTime.Now;
                        var currentTerm = schoolDbContext.TTerms.Where(c => c.dStart <= DateTime.Now && c.dEnd >= DateTime.Now && c.cDel == null && c.SchoolID == schoolId && c.cDel == null).FirstOrDefault();
                        if (currentTerm != null)
                        {
                            int currentTermNumber = 0;
                            bool successfullyParsed = int.TryParse(currentTerm.sTerm, out currentTermNumber);
                            if (successfullyParsed && currentTermNumber > 1)
                            {
                                termDTOs = termDTOs.OrderByDescending(d => d.nTerm).ToList();
                            }
                        }
                    }

                    return termDTOs;
                }

                return null;
            }
        }

        public static void CreateApplicationExceptionLog(Exception ex, string path, Exception baseException)
        {

            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            if (ex != null)
            {
                var exceptionRequest = new ExceptionLogRequest()
                {
                    Application = "FingerPrintPayment",
                    ExceptionMsg = (ex != null) ? string.Format("Message {0} InnerException : {1} InnerException Stack Trace : {2}", ex.Message ?? string.Empty, (ex.InnerException != null) ? ex.InnerException.Message.ToString() : string.Empty, (ex.InnerException != null) ? ex.InnerException.StackTrace.ToString() : string.Empty) : string.Empty,
                    ExceptionSource = (ex != null) ? ex.Source : string.Empty,
                    ExceptionType = (ex != null && ex.GetType() != null) ? ex.GetType().ToString() : string.Empty,
                    SchoolId = userData.CompanyID,
                    SEmp = userData.UserID,
                    MethodName = "Application_Error",
                    Logdate = DateTime.Now,
                    Parameter = "",
                    ExceptionUrl = path,
                };

                CommonService.CreateExceptionLog(exceptionRequest);
            }

            if (baseException != null)
            {
                var exceptionRequest = new ExceptionLogRequest()
                {
                    Application = "FingerPrintPayment",
                    ExceptionMsg = string.Format("BaseException Message {0} InnerException : {1} InnerException Stack Trace : {2}", baseException.Message ?? string.Empty, (baseException.InnerException != null) ? baseException.InnerException.Message.ToString() : string.Empty, (baseException.InnerException != null) ? baseException.InnerException.StackTrace.ToString() : string.Empty),
                    ExceptionSource = baseException.Source,
                    ExceptionType = (baseException.GetType() != null) ? baseException.GetType().ToString() : string.Empty,
                    SchoolId = userData.CompanyID,
                    SEmp = userData.UserID,
                    MethodName = "Application_Error",
                    Logdate = DateTime.Now,
                    Parameter = "",
                    ExceptionUrl = path,
                };

                CommonService.CreateExceptionLog(exceptionRequest);
            }
        }

        private static List<PlanCourseTermDTO> GetTerms(List<PlanCourseTermDTO> planCourseTermDTOs, int? nYear)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (planCourseTermDTOs != null && planCourseTermDTOs.Count == 1)
                {
                    var termsDTO = planCourseTermDTOs.FirstOrDefault();
                    //var termNumber  = (termsDTO != null && termsDTO.TermNumber == "1") ? 
                    var tterms = schoolDbContext.TTerms.Where(con => con.nYear == nYear && con.nTerm != termsDTO.NTerm && string.IsNullOrEmpty(con.cDel) && con.SchoolID == userData.CompanyID).ToList();

                    if (tterms != null)
                    {
                        foreach (var t in tterms)
                        {
                            planCourseTermDTOs.Add(new PlanCourseTermDTO
                            {
                                NTerm = t.nTerm,
                                TermNumber = t.sTerm

                            });
                        }


                        planCourseTermDTOs = planCourseTermDTOs.OrderBy(o => o.TermNumber).ToList();
                    }
                }
                else if (planCourseTermDTOs != null && planCourseTermDTOs.Count == 0)
                {
                    var terms = (from ct in schoolDbContext.TTerms.Where(con => con.nYear == nYear && string.IsNullOrEmpty(con.cDel)).ToList()
                                 select new PlanCourseTermDTO
                                 {
                                     NTerm = ct.nTerm,
                                     TermNumber = ct.sTerm
                                 });

                    if (terms != null)
                    {
                        planCourseTermDTOs.AddRange(terms);
                        planCourseTermDTOs = planCourseTermDTOs.OrderBy(o => o.TermNumber).ToList();
                    }
                }
                return planCourseTermDTOs;
            }
        }

        public static string CopyPlanFromAnotherYear(int copyPlanFromnYear, int copyPlnaTonYear, int schoolId, int sEmp)
        {
            PlanService.CopyPlanFromAnotherYear(copyPlanFromnYear, copyPlnaTonYear, schoolId, sEmp);
            return string.Empty;

        }

        public static List<TermDTO> GetTerms(int nYear, int schoolId, int userId)
        {
            return CommonService.GetTerms(nYear, schoolId, userId);
        }

        public static List<StudentDTO> GetStudents(CommonRequest request)
        {
            return CommonService.GetStudents(request);
        }

        public static List<StudentDTO> GetStudentsWithGraduatedStatus(CommonRequest request)
        {
            return CommonService.GetStudentsWithGraduatedStatus(request);
        }

        public static void StudentProfileUpdateToMemory(List<ProfileImage> profileImages, string AuthKey, string AuthValue)
        {
            using (var dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                foreach (var profileImage in profileImages)
                {
                    var masterUser = dbmaster.TUsers.FirstOrDefault(w => w.nCompany == profileImage.SchoolId && w.cDel == null && w.username == profileImage.UserName);


                    if (masterUser != null)
                    {
                        var qstudent = schoolDbContext.TUser.FirstOrDefault(w => w.SchoolID == profileImage.SchoolId && w.cDel == null && !string.IsNullOrEmpty(w.sStudentID) && w.sID == masterUser.sID);
                        var q_employess = schoolDbContext.TEmployees.FirstOrDefault(f => f.cDel == null && f.SchoolID == profileImage.SchoolId && f.sEmp == masterUser.sID);

                        if (qstudent != null)
                        {
                            UpdateMemory memory = new UpdateMemory(AuthKey, AuthValue);
                            memory.Student(qstudent, masterUser);

                        }
                        else if (q_employess != null)
                        {
                            UpdateMemory memory = new UpdateMemory(AuthKey, AuthValue);
                            memory.Employee(q_employess, masterUser);
                        }
                    }

                }
            }
        }

        public static bool CheckTermIsActiveInPlan(CommonRequest commonRequest)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var isTermActive = true;
                if (commonRequest.PlanCourseId > 0)
                {
                    var planCourseTerm = (from p in dbschool.TPlanCourses
                                          join t in dbschool.TPlanCourseTerms on p.PlanCourseId equals t.PlanCourseId
                                          where p.SchoolID == commonRequest.SchoolId && p.PlanCourseId == commonRequest.PlanCourseId &&
                                          t.IsActive == true && t.nTerm == commonRequest.nTerm && p.CourseStatus == 1
                                          select t).FirstOrDefault();

                    if (planCourseTerm == null)
                    {
                        isTermActive = false;
                    }
                }
                return isTermActive;
            }
        }

        public static string GetGradeTeacherComments(int schoolId)
        {
            teacherDescribe teacherDescribe = new teacherDescribe();
            List<teacherDescribe> teacherDescribeList = new List<teacherDescribe>();

            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {


                int number = 1;
                foreach (var data in _db.TGradeTeacherDescribes.Where(w => w.SchoolID == schoolId && w.cDel != 1))
                {
                    teacherDescribe = new teacherDescribe();
                    teacherDescribe.id = data.nGradeTeacherDescribe;
                    teacherDescribe.number = number;
                    teacherDescribe.describe = data.Describe;
                    number = number + 1;
                    teacherDescribeList.Add(teacherDescribe);
                }
                if (teacherDescribeList.Count == 0)
                    return string.Empty;
                return String.Join(",", teacherDescribeList.Select(m => m.describe).ToArray());
            }
        }

        #region "Grade"

        //public static List<StudentGradeInfoDTO> GetStudentGradeByTermAndRoom(int sId, int schoolId, int nTermSubLevel2, string nTerm, int nYear, bool IsRequestForCurrentAcademicYear, bool thaiLanguage = true)
        //{
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
        //    {
        //        var studentGradeByTermAndRoom = new List<GetStudentGradeByTermAndRoom_Result>();
        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {
        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();

        //                    _PGTGradeList = pgLiteDB.PGTGrades.Where(x => x.SchoolID == schoolId && x.nTerm == nTerm && (x.nTermSubLevel2 == nTermSubLevel2 || x.nTermSubLevel2 == 0 || x.nTermSubLevel2 == null)).ToList();

        //                    var SQGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.sID == sId && x.cDel == false).ToList();

        //                    var SQGradeEdits = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId).ToList();


        //                    _PGTGradeDetailList = (from g in _PGTGradeList
        //                                           join gd in SQGradeDetails on g.nGradeId equals gd.nGradeId
        //                                           where g.SchoolID == schoolId && gd.SchoolID == schoolId
        //                                           && g.nTerm == nTerm && gd.sID == sId
        //                                           && (g.nTermSubLevel2 == nTermSubLevel2 || g.nTermSubLevel2 == 0 || g.nTermSubLevel2 == null) && gd.cDel == false
        //                                           select gd
        //                                     ).ToList();


        //                    _PGTGradeEditList = (from gd in SQGradeDetails
        //                                         join ge in SQGradeEdits on gd.nGradeDetailId equals ge.GradeDetailID
        //                                         where gd.SchoolID == schoolId && ge.SchoolID == schoolId
        //                                         select ge
        //                                        ).ToList();

        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);

        //                    studentGradeByTermAndRoom = GetSQStudentGradeByTermAndRoom(sId, schoolId, nTermSubLevel2, nTerm);


        //                }
        //            }
        //            else
        //            {
        //                studentGradeByTermAndRoom = _dbGrade.GetStudentGradeByTermAndRoom(sId, nTerm, nTermSubLevel2, schoolId).ToList();
        //            }

        //            //if main table doesn't have value then check in the history
        //            if (studentGradeByTermAndRoom == null || (studentGradeByTermAndRoom != null && studentGradeByTermAndRoom.Count == 0))
        //            {
        //                var studentGradeByTermAndRoomHistory = _dbGradeHistory.GetStudentGradeByTermAndRoomHistory(sId, nTerm, nTermSubLevel2, schoolId).ToList();
        //                var config = new MapperConfiguration(cfg =>
        //                {

        //                    cfg.AllowNullCollections = true;
        //                    cfg.CreateMap<GradeHistoryEntity.GetStudentGradeByTermAndRoomHistory_Result, GetStudentGradeByTermAndRoom_Result>();

        //                });
        //                IMapper iMapper = config.CreateMapper();

        //                studentGradeByTermAndRoom = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeByTermAndRoomHistory_Result>, List<GetStudentGradeByTermAndRoom_Result>>(studentGradeByTermAndRoomHistory);
        //            }
        //        }
        //        else
        //        {
        //            var studentGradeByTermAndRoomHistory = _dbGradeHistory.GetStudentGradeByTermAndRoomHistory(sId, nTerm, nTermSubLevel2, schoolId).ToList();
        //            var config = new MapperConfiguration(cfg =>
        //            {

        //                cfg.AllowNullCollections = true;
        //                cfg.CreateMap<GradeHistoryEntity.GetStudentGradeByTermAndRoomHistory_Result, GetStudentGradeByTermAndRoom_Result>();

        //            });
        //            IMapper iMapper = config.CreateMapper();

        //            studentGradeByTermAndRoom = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeByTermAndRoomHistory_Result>, List<GetStudentGradeByTermAndRoom_Result>>(studentGradeByTermAndRoomHistory);

        //            //if history doesn't exist then check in the main table.
        //            if (studentGradeByTermAndRoom == null || (studentGradeByTermAndRoom != null && studentGradeByTermAndRoom.Count == 0))
        //            {


        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    using (var pgLiteDB = new PGGradeDBEntities())
        //                    {
        //                        List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                        List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                        List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();

        //                        _PGTGradeList = pgLiteDB.PGTGrades.Where(x => x.SchoolID == schoolId && x.nTerm == nTerm && (x.nTermSubLevel2 == nTermSubLevel2 || x.nTermSubLevel2 == 0 || x.nTermSubLevel2 == null)).ToList();

        //                        var SQGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.sID == sId && x.cDel == false).ToList();

        //                        var SQGradeEdits = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId).ToList();


        //                        _PGTGradeDetailList = (from g in _PGTGradeList
        //                                               join gd in SQGradeDetails on g.nGradeId equals gd.nGradeId
        //                                               where g.SchoolID == schoolId && gd.SchoolID == schoolId
        //                                               && g.nTerm == nTerm && gd.sID == sId
        //                                               && (g.nTermSubLevel2 == nTermSubLevel2 || g.nTermSubLevel2 == 0 || g.nTermSubLevel2 == null) && gd.cDel == false
        //                                               select gd
        //                                         ).ToList();


        //                        _PGTGradeEditList = (from gd in SQGradeDetails
        //                                             join ge in SQGradeEdits on gd.nGradeDetailId equals ge.GradeDetailID
        //                                             where gd.SchoolID == schoolId && ge.SchoolID == schoolId
        //                                             select ge
        //                                            ).ToList();

        //                        var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                        var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                        var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                        var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                        var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                        var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);

        //                        studentGradeByTermAndRoom = GetSQStudentGradeByTermAndRoom(sId, schoolId, nTermSubLevel2, nTerm);


        //                    }
        //                }
        //                else
        //                {
        //                    studentGradeByTermAndRoom = _dbGrade.GetStudentGradeByTermAndRoom(sId, nTerm, nTermSubLevel2, schoolId).ToList();

        //                }

        //            }
        //        }
        //        var planCourse = GetPlanCourses(0, nTermSubLevel2, nTerm, nYear, schoolId, schoolDbContext, thaiLanguage);
        //        var studentInfo = schoolDbContext.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2).AsQueryable().ToList();

        //        var studentGradeInfo = (from sg in studentGradeByTermAndRoom
        //                                join pc in planCourse on new { sg.NTerm, sg.sPlaneID } equals new { pc.NTerm, sPlaneID = pc.SPlaneId }
        //                                join st in studentInfo on new { sg.sID, nTerm = sg.NTerm } equals new { st.sID, st.nTerm }
        //                                select new StudentGradeInfoDTO()
        //                                {
        //                                    fRatioAfterQuiz = sg.fRatioAfterQuiz,
        //                                    fRatioBeforeQuiz = sg.fRatioBeforeQuiz,
        //                                    fRatioLateTerm = sg.fRatioLateTerm,
        //                                    fRatioMidTerm = sg.fRatioMidTerm,
        //                                    fRatioQuiz = sg.fRatioQuiz,
        //                                    FirstName = st.sName,
        //                                    FullClassName = st.ClassFullName,
        //                                    scoreBeforeAfterMidTerm = sg.scoreBeforeAfterMidTerm,
        //                                    scoreFinalTerm = sg.scoreFinalTerm,
        //                                    scoreMidTerm = sg.scoreMidTerm,
        //                                    sID = sg.sID,
        //                                    //sIdentification = st.sIdentification,
        //                                    sPlaneID = sg.sPlaneID,
        //                                    sStudentId = st.sStudentID,
        //                                    sTerm = st.sTerm,
        //                                    ScoreAfterMidTerm = sg.ScoreAfterMidTerm,
        //                                    ScoreBeforeMidTerm = sg.ScoreBeforeMidTerm,
        //                                    CourseStatus = pc.CourseStatus,
        //                                    getSamattana = sg.getSamattana,
        //                                    getScore100 = sg.getScore100,
        //                                    getSpecial = sg.getSpecial,
        //                                    GradeSet = sg.GradeSet,
        //                                    getAfterQuiz100 = sg.getAfterQuiz100,
        //                                    IsScoreExist = sg.IsScoreExist,
        //                                    nStudentNumber = st.nStudentNumber ?? 999,
        //                                    nStudentStatus = st.nStudentStatus,
        //                                    nTermSubLevel2 = st.nTermSubLevel2,
        //                                    nTSubLevel = st.nTSubLevel,
        //                                    ClassName = st.SubLevel,
        //                                    CourseCode = pc.CourseCode,
        //                                    CourseGroup = pc.CourseGroup,
        //                                    CourseGroupName = pc.CourseGroupName,
        //                                    CourseHour = pc.CourseHour,
        //                                    CourseName = pc.CourseName,
        //                                    CourseTotalHour = pc.CourseTotalHour,
        //                                    CourseType = pc.CourseType,
        //                                    CourseTypeName = pc.CourseTypeName,
        //                                    GradeXNCredit = ((pc.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || pc.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : pc.NCredit) * sg.GradeXNCredit,
        //                                    NCredit = (pc.CourseGroupName == "รายวิชาเสริมไม่คิดหน่วยกิต" || pc.CourseGroupName == "กิจกรรมพัฒนาผู้เรียน") ? 0 : pc.NCredit,
        //                                    getBeforeQuiz100 = sg.getBeforeQuiz100,
        //                                    getBehaviorLabel = sg.getBehaviorLabel,
        //                                    getBehaviorTotal = sg.getBehaviorTotal,
        //                                    getFinal100 = sg.getFinal100,
        //                                    getGradeLabel = sg.getGradeLabel,
        //                                    getMid100 = sg.getMid100,
        //                                    getQuiz100 = sg.getQuiz100,
        //                                    getReadWrite = sg.getReadWrite,
        //                                    //GPA = sg.GPA,
        //                                    GradePass = sg.GradePass ?? 0,
        //                                    LastName = st.sLastname,
        //                                    maxAfterMidTerm = sg.maxAfterMidTerm,
        //                                    maxBeforeMidTerm = sg.maxBeforeMidTerm,
        //                                    MaxBeforeAfterMidTermTotal = sg.MaxBeforeAfterMidTermTotal,
        //                                    maxFinalTerm = sg.maxFinalTerm,
        //                                    maxMidTerm = sg.maxMidTerm,
        //                                    nGradeId = sg.nGradeId,
        //                                    nOrder = pc.NOrder,
        //                                    NTerm = sg.NTerm,
        //                                    numberYear = st.numberYear,
        //                                    nYear = st.nYear,
        //                                    RoomName = st.SubLevel + "/" + st.nTSubLevel2,
        //                                    TitleDesc = st.titleDescription,
        //                                    ActualNCredit = pc.NCredit,
        //                                    CourseGroupNameEng = pc.CourseGroupNameEng,
        //                                    CourseTypeNameEng = pc.CourseTypeNameEng

        //                                }).ToList();

        //        var gpaandtotalcredit = (from s in studentGradeInfo
        //                                 where s.CourseGroupName != "กิจกรรมพัฒนาผู้เรียน" && s.CourseGroupName != "รายวิชาเสริมไม่คิดหน่วยกิต"
        //                                 group s by new { s.sID, s.NTerm } into sg
        //                                 select new { sg.Key.sID, TotalNCredit = sg.Sum(s => s.NCredit), TotalGadexNCredit = sg.Sum(s => s.GradeXNCredit), GPA = ((sg.Sum(s => s.GradeXNCredit) * 4) / (sg.Sum(s => s.NCredit) * 4)) }).FirstOrDefault();

        //        if (studentGradeInfo != null && gpaandtotalcredit != null)
        //        {
        //            //var gpa = (gpaandtotalcredit?.GPA ?? 0).ToString("0.000");
        //            //studentGradeInfo.ForEach(f => f.GPA = gpa.Remove(gpa.Length - 1));
        //            studentGradeInfo.ForEach(f => f.GPA = Common.Truncate(gpaandtotalcredit?.GPA ?? 0, 2).ToString("0.00"));
        //        }

        //        return studentGradeInfo;
        //    }
        //}
        public static List<XMLStudentParameter> FilterStudentsByPlanCourseId(List<XMLStudentParameter> students, int planCourseId)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                if (students != null)
                {
                    var planCourseStudent = dbschool.TPlanCourseStudents.Where(w => w.PlanCourseId == planCourseId && w.IsActive == true).ToList();
                    if (planCourseStudent != null && planCourseStudent.Count() > 0)
                    {
                        students = (from ps in planCourseStudent
                                    join s in students on ps.sID equals s.SId
                                    select s).ToList();
                    }

                    return students;
                }
                return new List<XMLStudentParameter>();
            }
        }
        public static List<XMLStudentParameter> GetStudentsWithOutResigned(List<string> nTermList, int nTSubLevel, int schoolId, int nTermSubLevel2 = 0)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var tTerm2 = dbschool.TTerms.Where(w => w.SchoolID == schoolId && w.cDel == null && nTermList.Contains(w.nTerm) && w.sTerm == "2").Select(s => new { s.dStart, s.dEnd }).FirstOrDefault();

                var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && (w.nTSubLevel == nTSubLevel || nTSubLevel == 0) && (w.cDel ?? "0") != "1" && w.nStudentStatus != 1 && w.nStudentStatus != 3 && !(w.nStudentStatus == 2 && w.MoveOutDate >= w.dStart && w.MoveOutDate <= w.dEnd) && !(w.nStudentStatus == 2 && w.dStart >= w.MoveOutDate))
                                join t in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1) on s.nTermSubLevel2 equals t.nTermSubLevel2
                                where nTermList.Contains(s.nTerm) && (nTermSubLevel2 == 0 || t.nTermSubLevel2 == nTermSubLevel2)
                                select new XMLStudentParameter
                                {
                                    SId = s.sID,
                                    NTerm = s.nTerm,
                                    NTermSubLevel2 = s.nTermSubLevel2,
                                    STerm = s.sTerm,
                                    NumberYear = s.numberYear ?? 0,
                                    NTSubLevel = s.nTSubLevel,
                                    NStudentStatus = s.nStudentStatus ?? 0,
                                    SName = s.sName,
                                    SLastname = s.sLastname
                                }).AsQueryable().ToList();

                var totalStudents = new List<int>();
                var studentsinAllrooms = new List<StudentDTO>();
                if (nTermList.Count > 1)
                {
                    var resignedStudents = students.Where(w => w.NStudentStatus == 2 && w.STerm == "2").Select(s => s.SId).Distinct().ToList();
                    students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && (w.nTSubLevel == nTSubLevel || nTSubLevel == 0) && (w.cDel ?? "0") != "1" && w.nStudentStatus != 3 && !(w.nStudentStatus == 2 && w.MoveOutDate >= w.dStart && w.MoveOutDate <= w.dEnd) && !(w.nStudentStatus == 2 && w.dStart >= w.MoveOutDate))
                                join t in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1) on s.nTermSubLevel2 equals t.nTermSubLevel2
                                where nTermList.Contains(s.nTerm) && !(s.nStudentStatus == 2 && tTerm2.dEnd > s.MoveOutDate)
                                && !(s.nStudentStatus == 2 && tTerm2.dStart > s.MoveOutDate) && !(s.sTerm == "1" && s.MoveOutDate != null && s.nStudentStatus == 0 && s.MoveOutDate >= tTerm2.dStart && s.MoveOutDate <= tTerm2.dEnd)
                                select new XMLStudentParameter
                                {
                                    SId = s.sID,
                                    NTerm = s.nTerm,
                                    NTermSubLevel2 = s.nTermSubLevel2,
                                    STerm = s.sTerm,
                                    NumberYear = s.numberYear ?? 0,
                                    NTSubLevel = s.nTSubLevel,
                                    NStudentStatus = s.nStudentStatus ?? 0,
                                    SName = s.sName,
                                    SLastname = s.sLastname
                                }).AsQueryable().ToList();


                }
                return students;
            }
        }
        //public static List<GetAllStudentGradeSplitUpDetail_Result> GetAllStudentGradeSplitUpDetail(List<string> nTermList, int nTSubLevel, int schoolId, string sPlaneName, int nYear)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {
        //        List<PlanCourseDTO> planCourses = new List<PlanCourseDTO>();

        //        var IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(0, nTermList[0], schoolId);

        //        var tTerm2 = dbschool.TTerms.Where(w => w.SchoolID == schoolId && w.cDel == null && nTermList.Contains(w.nTerm) && w.sTerm == "2").Select(s => new { s.dStart, s.dEnd }).FirstOrDefault();
        //        //Student resignation must not be include for Yearly

        //        //Resign and suspension should not be included. SB-7026
        //        //Included Resign student for  SB-8510
        //        //SB - 8627 If Student Resigned and within term period then don't include
        //        //var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel && (w.cDel ?? "0") != "1" && w.nStudentStatus != 3 && !(w.nStudentStatus == 2 && w.MoveOutDate >= w.dStart && w.MoveOutDate <= w.dEnd) && !(w.nStudentStatus == 2 && w.dStart >= w.MoveOutDate))
        //        //                join t in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1) on s.nTermSubLevel2 equals t.nTermSubLevel2
        //        //                where nTermList.Contains(s.nTerm) 
        //        //                select new XMLStudentParameter
        //        //                {
        //        //                    SId = s.sID,
        //        //                    NTerm = s.nTerm,
        //        //                    NTermSubLevel2 = s.nTermSubLevel2,
        //        //                    STerm = s.sTerm,
        //        //                    NumberYear = s.numberYear ?? 0,
        //        //                    NTSubLevel = s.nTSubLevel,
        //        //                    NStudentStatus = s.nStudentStatus ?? 0
        //        //                }).ToList();
        //        var students = GetStudentsWithOutResigned(nTermList, nTSubLevel, schoolId);
        //        var totalStudents = new List<int>();
        //        var studentsinAllrooms = new List<StudentDTO>();
        //        //if (nTermList.Count > 1)
        //        //{
        //        //    //var resignedStudents = students.Where(w => w.NStudentStatus == 2 && w.STerm == "2").Select(s => s.SId).Distinct().ToList();
        //        //    ////resignedStudents = null;
        //        //    ////var term2StartDate = dbschool.TTerms.Where(w => w.SchoolID == 258 && w.cDel == null && nTermList.Contains(w.nTerm) && w.sTerm == "2").Select(s => s.dStart).FirstOrDefault();
        //        //    //students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel && (w.cDel ?? "0") != "1" && w.nStudentStatus != 3 && !(w.nStudentStatus == 2 && w.MoveOutDate >= w.dStart && w.MoveOutDate <= w.dEnd) && !(w.nStudentStatus == 2 && w.dStart >= w.MoveOutDate))
        //        //    //            join t in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1) on s.nTermSubLevel2 equals t.nTermSubLevel2
        //        //    //            where nTermList.Contains(s.nTerm) && !(s.nStudentStatus == 2 && tTerm2.dEnd > s.MoveOutDate)
        //        //    //            && !(s.nStudentStatus == 2 && tTerm2.dStart > s.MoveOutDate) && !(s.sTerm == "1" && s.MoveOutDate != null && s.nStudentStatus == 0 && s.MoveOutDate >= tTerm2.dStart && s.MoveOutDate <= tTerm2.dEnd)
        //        //    //            select new XMLStudentParameter
        //        //    //            {
        //        //    //                SId = s.sID,
        //        //    //                NTerm = s.nTerm,
        //        //    //                NTermSubLevel2 = s.nTermSubLevel2,
        //        //    //                STerm = s.sTerm,
        //        //    //                NumberYear = s.numberYear ?? 0,
        //        //    //                NTSubLevel = s.nTSubLevel,
        //        //    //                NStudentStatus = s.nStudentStatus ?? 0
        //        //    //            }).ToList();

        //        //    //if (resignedStudents != null)
        //        //    //{
        //        //    //    students = students.Where(w => !resignedStudents.Contains(w.SId)).ToList();
        //        //    //}
        //        //    //totalStudents = (from t1 in students.Where(w => w.STerm == "1")
        //        //    //                 join t2 in students.Where(w => w.STerm == "2") on t1.SId equals t2.SId
        //        //    //                 select t1.SId).ToList();

        //        //    //students = students.Where(w => totalStudents.Contains(w.SId)).ToList();

        //        //    //studentsinAllrooms = (from t1 in students.Where(w => w.STerm == "1")
        //        //    //                 join t2 in students.Where(w => w.STerm == "2") on t1.SId equals t2.SId
        //        //    //                 select new StudentDTO { SId = t1.SId, NTerm = t1.NTerm, NTermSubLevel2 = t1.NTermSubLevel2 }).ToList();

        //        //    totalStudents = (from t1 in students
        //        //                     select t1.SId).Distinct().ToList();

        //        //    students = students.Where(w => totalStudents.Contains(w.SId)).ToList();

        //        //    studentsinAllrooms = (from t1 in students
        //        //                          select new StudentDTO { SId = t1.SId, NTerm = t1.NTerm, NTermSubLevel2 = t1.NTermSubLevel2 }).ToList();
        //        //}
        //        //else
        //        //{
        //        totalStudents = (from t1 in students
        //                         select t1.SId).Distinct().ToList();

        //        studentsinAllrooms = (from t1 in students
        //                              select new StudentDTO { SId = t1.SId, NTerm = t1.NTerm, NTermSubLevel2 = t1.NTermSubLevel2 }).ToList();
        //        //}

        //        //var totalStudents = students.Select(s => s.SId).Distinct().ToList();

        //        foreach (var t in students.Select(s => new { s.NTerm, s.NTermSubLevel2, s.NTSubLevel }).Distinct().ToList())
        //        {
        //            if (planCourses.Where(w => w.NTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2).Count() == 0)
        //            {
        //                //var getPlanCourses = dbschool.GetPlanCourse(nTSubLevel, t.NTermSubLevel2, t.NTerm, 0, schoolId).ToList();

        //                var getPlanCourses = ServiceHelper.GetPlanCourses(nTSubLevel, t.NTermSubLevel2, t.NTerm, 0, schoolId, dbschool, true);


        //                getPlanCourses = getPlanCourses.Where(w => w.CourseName == (!string.IsNullOrEmpty(sPlaneName) ? sPlaneName : w.CourseName)).ToList();
        //                if (getPlanCourses != null)
        //                {
        //                    //Subject wise student count from plan
        //                    var getStudentsfromPlanCourses = (from s in dbschool.TPlanCourseStudents.Where(w => w.SchoolID == schoolId && w.IsActive == true
        //                                                      && w.nTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2
        //                                                     && w.nTSubLevel == t.NTSubLevel).ToList()
        //                                                      join pc in getPlanCourses on s.PlanCourseId equals pc.PlanCourseId
        //                                                      select s).Distinct().ToList();

        //                    foreach (var s in getPlanCourses)
        //                    {
        //                        var courseStudent = new List<int>();
        //                        if (getStudentsfromPlanCourses != null)
        //                        {

        //                            var getStudentsfromPlanCourse = getStudentsfromPlanCourses.Where(w => w.PlanCourseId == s.PlanCourseId).ToList();
        //                            if (getStudentsfromPlanCourse != null && getStudentsfromPlanCourse.Count > 0)
        //                            {
        //                                courseStudent = (from crs in studentsinAllrooms.Where(w => w.NTermSubLevel2 == t.NTermSubLevel2 && w.NTerm == t.NTerm)
        //                                                 join ps in getStudentsfromPlanCourse on crs.SId equals ps.sID
        //                                                 select crs.SId).Distinct().ToList();
        //                            }
        //                            else
        //                            {
        //                                courseStudent = (from crs in studentsinAllrooms.Where(w => w.NTermSubLevel2 == t.NTermSubLevel2 && w.NTerm == t.NTerm)
        //                                                 select crs.SId).Distinct().ToList();
        //                            }
        //                        }


        //                        planCourses.Add(new PlanCourseDTO
        //                        {
        //                            SPlaneId = s.SPlaneId,
        //                            CourseName = s.CourseName,
        //                            CourseCode = s.CourseCode,
        //                            CourseGroup = s.CourseGroup,
        //                            CourseHour = s.CourseHour,
        //                            CourseStatus = s.CourseStatus,
        //                            CourseType = s.CourseType,
        //                            CourseTotalHour = s.CourseTotalHour,
        //                            NCredit = s.NCredit,
        //                            NTerm = s.NTerm,
        //                            nTermSubLevel2 = t.NTermSubLevel2,
        //                            SIds = courseStudent,
        //                            NOrder = s.NOrder,
        //                        });
        //                    }
        //                }
        //            }

        //        }

        //        if (planCourses != null)
        //        {
        //            planCourses = planCourses.OrderBy(w => w.CourseGroup).ThenBy(w => w.NOrder).ThenBy(w => w.CourseCode).ToList();
        //        }

        //        var planStudents = (from pc in planCourses
        //                            group pc by new { pc.CourseCode, pc.CourseName } into gpc
        //                            select new XMLCourseTypeStudentParameter { CourseCode = gpc.Key.CourseCode, CourseName = gpc.Key.CourseName, StudentCount = gpc.SelectMany(s => s.SIds).Distinct().ToList().Count() }).ToList();


        //        var planCoursesXML = (from s in planCourses
        //                              select new XMLsPlaneIdParameter
        //                              {
        //                                  SPlaneID = s.SPlaneId,
        //                                  SPlaneName = s.CourseName,
        //                                  CourseCode = s.CourseCode,
        //                                  CourseGroup = s.CourseGroup,
        //                                  CourseHour = s.CourseHour,
        //                                  CourseStatus = s.CourseStatus,
        //                                  CourseType = s.CourseType,
        //                                  CourseTotalHour = s.CourseTotalHour,
        //                                  NCredit = s.NCredit,
        //                                  NTerm = s.NTerm,
        //                                  NTermSubLevel2 = s.nTermSubLevel2,
        //                                  NOrder = s.NOrder,
        //                              }).ToList();

        //        var xmlplanIDList = Common.GetXMLFromObject(planCoursesXML);

        //        var xmltermIDList = Common.GetXMLFromObject(nTermList.Select(s => new XMLnTermParameter { nTerm = s }).ToList());

        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            var studentGradeSplitUpDetail = new List<GetAllStudentGradeSplitUpDetail_Result>();
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                studentGradeSplitUpDetail = _dbGrade.GetAllStudentGradeSplitUpDetail(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //            }

        //            //Check History Table
        //            if (studentGradeSplitUpDetail == null || (studentGradeSplitUpDetail != null && studentGradeSplitUpDetail.Count == 0))
        //            {
        //                using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //                {
        //                    IMapper mapper = GetMapper();
        //                    var studentGradeSplitUpDetailHistory = _dbGradeHistory.GetAllStudentGradeSplitUpDetailHistory(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //                    studentGradeSplitUpDetail = mapper.Map<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailHistory_Result>, List<GetAllStudentGradeSplitUpDetail_Result>>(studentGradeSplitUpDetailHistory);
        //                }
        //            }

        //            return studentGradeSplitUpDetail;
        //        }
        //        else
        //        {
        //            var studentGradeSplitUpDetail = new List<GetAllStudentGradeSplitUpDetail_Result>();
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                IMapper mapper = GetMapper();
        //                var studentGradeSplitUpDetailHistory = _dbGradeHistory.GetAllStudentGradeSplitUpDetailHistory(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //                studentGradeSplitUpDetail = mapper.Map<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailHistory_Result>, List<GetAllStudentGradeSplitUpDetail_Result>>(studentGradeSplitUpDetailHistory);

        //            }

        //            //Check Main Table
        //            if (studentGradeSplitUpDetail == null || (studentGradeSplitUpDetail != null && studentGradeSplitUpDetail.Count == 0))
        //            {
        //                using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //                {
        //                    studentGradeSplitUpDetail = _dbGrade.GetAllStudentGradeSplitUpDetail(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //                }
        //            }

        //            return studentGradeSplitUpDetail;
        //        }

        //        //studentGradeSplitUpDetail = (from g in studentGradeSplitUpDetail
        //        //                             join s in planStudents on new { g.sPlaneName, g.courseCode } equals new { sPlaneName = s.CourseName, courseCode = s.CourseCode }
        //        //                             select new GetAllStudentGradeSplitUpDetail_Result
        //        //                             {
        //        //                                 Grade25 = g.Grade25,
        //        //                                 Grade00 = g.Grade00,
        //        //                                 Grade10 = g.Grade10,
        //        //                                 Grade15 = g.Grade15,
        //        //                                 Grade20 = g.Grade20,
        //        //                                 Grade30 = g.Grade30,
        //        //                                 Grade30Up = g.Grade30Up,
        //        //                                 Grade30UpPercent = (Math.Round((decimal)(g.Grade30Up * 100 / s.StudentCount), 2, MidpointRounding.AwayFromZero)),  //g.Grade30UpPercent,
        //        //                                 Grade35 = g.Grade35,
        //        //                                 Grade40 = g.Grade40,
        //        //                                 courseGroup = g.courseGroup,
        //        //                                 courseType = g.courseType,
        //        //                                 sPlaneName = g.sPlaneName,
        //        //                                 StudentAmount = (s.StudentCount > 0) ? s.StudentCount : g.StudentAmount,
        //        //                                 courseCode = g.courseCode

        //        //                             }).ToList();



        //    }
        //}

        //public static List<GetAllStudentGradeSplitUpDetail_Result> GetAllStudentGradeSplitUpDetailByRoom(List<string> nTermList, int nTSubLevel, int nTermSubLevel2, int schoolId, string sPlaneName, int nYear)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {
        //        List<PlanCourseDTO> planCourses = new List<PlanCourseDTO>();

        //        var IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(0, nTermList[0], schoolId);

        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel && (w.cDel ?? "0") != "1" && w.nStudentStatus != 2 && w.nStudentStatus != 3)
        //                        join t in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1) on s.nTermSubLevel2 equals t.nTermSubLevel2
        //                        where nTermList.Contains(s.nTerm) && (t.nTermSubLevel2 == nTermSubLevel2 || nTermSubLevel2 == 0)
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            STerm = s.sTerm,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NTSubLevel = s.nTSubLevel
        //                        }).AsQueryable().ToList();

        //        var totalStudents = new List<int>();
        //        if (nTermList.Count > 1)
        //        {
        //            //totalStudents = (from t1 in students.Where(w => w.STerm == "1")
        //            //                 join t2 in students.Where(w => w.STerm == "2") on t1.SId equals t2.SId
        //            //                 select t1.SId).ToList();

        //            totalStudents = (from t1 in students.Where(w => w.STerm == "2")
        //                             select t1.SId).Distinct().ToList();
        //        }
        //        else
        //        {
        //            totalStudents = (from t1 in students
        //                             select t1.SId).Distinct().ToList();
        //        }

        //        //var totalStudents = students.Select(s => s.SId).Distinct().ToList();

        //        foreach (var t in students)
        //        {
        //            if (planCourses.Where(w => w.NTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2).Count() == 0)
        //            {
        //                var getPlanCourses = dbschool.GetPlanCourse(nTSubLevel, t.NTermSubLevel2, t.NTerm, 0, schoolId).ToList();




        //                getPlanCourses = getPlanCourses.Where(w => w.sPlaneName == (!string.IsNullOrEmpty(sPlaneName) ? sPlaneName : w.sPlaneName)).ToList();
        //                if (getPlanCourses != null)
        //                {
        //                    //Subject wise student count from plan
        //                    var getStudentsfromPlanCourses = (from s in dbschool.TPlanCourseStudents.Where(w => w.SchoolID == schoolId && w.IsActive == true
        //                                                      && w.nTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2
        //                                                     && w.nTSubLevel == t.NTSubLevel).ToList()
        //                                                      join pc in getPlanCourses on s.PlanCourseId equals pc.PlanCourseId
        //                                                      select s).Distinct().ToList();

        //                    foreach (var s in getPlanCourses)
        //                    {
        //                        var courseStudent = new List<int>();
        //                        if (getStudentsfromPlanCourses != null)
        //                        {

        //                            var getStudentsfromPlanCourse = getStudentsfromPlanCourses.Where(w => w.PlanCourseId == s.PlanCourseId).ToList();
        //                            if (getStudentsfromPlanCourse != null && getStudentsfromPlanCourse.Count > 0)
        //                            {
        //                                courseStudent = (from crs in totalStudents
        //                                                 join ps in getStudentsfromPlanCourse on crs equals ps.sID
        //                                                 select crs).ToList();
        //                            }
        //                            else
        //                            {
        //                                courseStudent = (from crs in totalStudents
        //                                                 select crs).ToList();
        //                            }
        //                        }


        //                        planCourses.Add(new PlanCourseDTO
        //                        {
        //                            SPlaneId = s.sPlaneID,
        //                            CourseName = s.sPlaneName,
        //                            CourseCode = s.courseCode,
        //                            CourseGroup = s.courseGroup,
        //                            CourseHour = s.CourseHour,
        //                            CourseStatus = s.CourseStatus,
        //                            CourseType = s.courseType,
        //                            CourseTotalHour = s.CourseTotalHour,
        //                            NCredit = s.nCredit,
        //                            NTerm = s.nTerm,
        //                            nTermSubLevel2 = t.NTermSubLevel2,
        //                            SIds = courseStudent
        //                        });
        //                    }
        //                }
        //            }

        //        }

        //        var planStudents = (from pc in planCourses
        //                            group pc by new { pc.CourseCode, pc.CourseName } into gpc
        //                            select new XMLCourseTypeStudentParameter { CourseCode = gpc.Key.CourseCode, CourseName = gpc.Key.CourseName, StudentCount = gpc.SelectMany(s => s.SIds).Distinct().ToList().Count() }).ToList();


        //        var planCoursesXML = (from s in planCourses
        //                              select new XMLsPlaneIdParameter
        //                              {
        //                                  SPlaneID = s.SPlaneId,
        //                                  SPlaneName = s.CourseName,
        //                                  CourseCode = s.CourseCode,
        //                                  CourseGroup = s.CourseGroup,
        //                                  CourseHour = s.CourseHour,
        //                                  CourseStatus = s.CourseStatus,
        //                                  CourseType = s.CourseType,
        //                                  CourseTotalHour = s.CourseTotalHour,
        //                                  NCredit = s.NCredit,
        //                                  NTerm = s.NTerm,
        //                                  NTermSubLevel2 = s.nTermSubLevel2

        //                              }).ToList();

        //        var xmlplanIDList = Common.GetXMLFromObject(planCoursesXML);

        //        var xmltermIDList = Common.GetXMLFromObject(nTermList.Select(s => new XMLnTermParameter { nTerm = s }).ToList());

        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                var studentGradeSplitUpDetail = _dbGrade.GetAllStudentGradeSplitUpDetail(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //                return studentGradeSplitUpDetail;
        //            }
        //        }
        //        else
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                IMapper mapper = GetMapper();
        //                var studentGradeSplitUpDetail = _dbGradeHistory.GetAllStudentGradeSplitUpDetailHistory(nTSubLevel, nYear, schoolId, 0, xmltermIDList, xmlplanIDList, Common.GetXMLFromObject(students), Common.GetXMLFromObject(planStudents)).ToList();
        //                return mapper.Map<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailHistory_Result>, List<GetAllStudentGradeSplitUpDetail_Result>>(studentGradeSplitUpDetail);
        //            }
        //        }

        //        //studentGradeSplitUpDetail = (from g in studentGradeSplitUpDetail
        //        //                             join s in planStudents on new { g.sPlaneName, g.courseCode } equals new { sPlaneName = s.CourseName, courseCode = s.CourseCode }
        //        //                             select new GetAllStudentGradeSplitUpDetail_Result
        //        //                             {
        //        //                                 Grade25 = g.Grade25,
        //        //                                 Grade00 = g.Grade00,
        //        //                                 Grade10 = g.Grade10,
        //        //                                 Grade15 = g.Grade15,
        //        //                                 Grade20 = g.Grade20,
        //        //                                 Grade30 = g.Grade30,
        //        //                                 Grade30Up = g.Grade30Up,
        //        //                                 Grade30UpPercent = (Math.Round((decimal)(g.Grade30Up * 100 / s.StudentCount), 2, MidpointRounding.AwayFromZero)),  //g.Grade30UpPercent,
        //        //                                 Grade35 = g.Grade35,
        //        //                                 Grade40 = g.Grade40,
        //        //                                 courseGroup = g.courseGroup,
        //        //                                 courseType = g.courseType,
        //        //                                 sPlaneName = g.sPlaneName,
        //        //                                 StudentAmount = (s.StudentCount > 0) ? s.StudentCount : g.StudentAmount,
        //        //                                 courseCode = g.courseCode

        //        //                             }).ToList();



        //    }
        //}
        //public static List<GetAllStudentGradeSplitUpDetailByCourseType_Result> GetAllStudentGradeSplitUpDetailByCourseType(List<string> nTermList, int schoolId, int nYear)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {

        //        var IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(0, nTermList[0], schoolId);
        //        List<PlanCourseDTO> planCourses = new List<PlanCourseDTO>();

        //        //Fetch All students from schools based on Term


        //        var students = GetStudentsWithOutResigned(nTermList, 0, schoolId);

        //        var totalStudents = (from t1 in students
        //                             select new StudentDTO { SId = t1.SId, NTSubLevel = t1.NTSubLevel }).Distinct().ToList();


        //        foreach (var t in students)
        //        {
        //            if (planCourses.Where(w => w.NTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2).Count() == 0)
        //            {
        //                //Get the plan course details by class
        //                var getPlanCourses = dbschool.GetPlanCourse(t.NTSubLevel, t.NTermSubLevel2, t.NTerm, 0, schoolId).ToList();

        //                //getPlanCourses = getPlanCourses.Where(w => w.sPlaneName == (!string.IsNullOrEmpty(sPlaneName) ? sPlaneName : w.sPlaneName)).ToList();
        //                if (getPlanCourses != null && getPlanCourses.Count() > 0)
        //                {
        //                    //Subject wise student count from plan
        //                    var getStudentsfromPlanCourses = (from s in dbschool.TPlanCourseStudents.Where(w => w.SchoolID == schoolId && w.IsActive == true
        //                                                      && w.nTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2
        //                                                     && w.nTSubLevel == t.NTSubLevel).ToList()
        //                                                      join pc in getPlanCourses on s.PlanCourseId equals pc.PlanCourseId
        //                                                      select s).Distinct().ToList();

        //                    foreach (var s in getPlanCourses)
        //                    {
        //                        var courseStudent = new List<int>();
        //                        if (getStudentsfromPlanCourses != null)
        //                        {

        //                            var getStudentsfromPlanCourse = getStudentsfromPlanCourses.Where(w => w.PlanCourseId == s.PlanCourseId).ToList();
        //                            if (getStudentsfromPlanCourse != null && getStudentsfromPlanCourse.Count > 0)
        //                            {
        //                                courseStudent = (from crs in totalStudents
        //                                                 join ps in getStudentsfromPlanCourse on new { sID = crs.SId, nTSubLevel = (crs.NTSubLevel ?? 0) } equals new { ps.sID, ps.nTSubLevel }
        //                                                 where crs.NTSubLevel == t.NTSubLevel
        //                                                 select crs.SId).ToList();
        //                            }
        //                            else
        //                            {
        //                                courseStudent = (from crs in totalStudents
        //                                                 where crs.NTSubLevel == t.NTSubLevel
        //                                                 select crs.SId).ToList();
        //                            }
        //                        }


        //                        planCourses.Add(new PlanCourseDTO
        //                        {
        //                            SPlaneId = s.sPlaneID,
        //                            CourseName = s.sPlaneName,
        //                            CourseCode = s.courseCode,
        //                            CourseGroup = s.courseGroup,
        //                            CourseHour = s.CourseHour,
        //                            CourseStatus = s.CourseStatus,
        //                            CourseType = s.courseType,
        //                            CourseTotalHour = s.CourseTotalHour,
        //                            NCredit = s.nCredit,
        //                            NTerm = s.nTerm,
        //                            nTermSubLevel2 = t.NTermSubLevel2,
        //                            NTSubLevel = t.NTSubLevel.ToString(),
        //                            SIds = courseStudent
        //                        });
        //                    }
        //                }
        //            }

        //        }


        //        var planStudents = (from pc in planCourses
        //                            group pc by new { pc.NTSubLevel, pc.CourseType, pc.CourseCode, pc.CourseName } into gpc
        //                            select new { NTSubLevel = gpc.Key.NTSubLevel, CourseType = gpc.Key.CourseType, CourseCode = gpc.Key.CourseCode, CourseName = gpc.Key.CourseName, StudentCount = gpc.SelectMany(s => s.SIds).Distinct().ToList().Count() }).ToList();

        //        var studentsByCourseType = (from pc in planStudents
        //                                    group pc by new { pc.NTSubLevel, pc.CourseType } into gpc
        //                                    select new XMLCourseTypeStudentParameter { NTSubLevel = Int32.Parse(gpc.Key.NTSubLevel), CourseType = gpc.Key.CourseType ?? 0, StudentCount = gpc.Sum(s => s.StudentCount) }).ToList();

        //        studentsByCourseType = (from s in studentsByCourseType
        //                                join y in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId) on s.CourseType equals y.courseTypeId
        //                                join g in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.NTSubLevel equals g.nTSubLevel
        //                                select new XMLCourseTypeStudentParameter { CourseType = s.CourseType, CourseTypeName = y.Description, NTSubLevel = s.NTSubLevel, StudentCount = s.StudentCount, SubLevel = g.SubLevel }).ToList();

        //        var planCoursesXML = (from s in planCourses
        //                              select new XMLsPlaneIdParameter
        //                              {
        //                                  SPlaneID = s.SPlaneId,
        //                                  SPlaneName = s.CourseName,
        //                                  CourseCode = s.CourseCode,
        //                                  CourseGroup = s.CourseGroup,
        //                                  CourseHour = s.CourseHour,
        //                                  CourseStatus = s.CourseStatus,
        //                                  CourseType = s.CourseType,
        //                                  CourseTotalHour = s.CourseTotalHour,
        //                                  NCredit = s.NCredit,
        //                                  NTerm = s.NTerm,
        //                                  NTermSubLevel2 = s.nTermSubLevel2

        //                              }).ToList();

        //        //var xmlplanIDList = Common.GetXMLFromObject(planCoursesXML);
        //        var xmltermIDList = Common.GetXMLFromObject(nTermList.Select(s => new XMLnTermParameter { nTerm = s }).ToList());
        //        //var xmlstudentByCourseType = Common.GetXMLFromObject(studentsByCourseType);
        //        //var xmlStudentList = Common.GetXMLFromObject(students);

        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //            {
        //                return _dbGrade.GetAllStudentGradeSplitUpDetailByCourseType(schoolId, xmltermIDList, Common.GetXMLFromObject(planCoursesXML), Common.GetXMLFromObject(students), Common.GetXMLFromObject(studentsByCourseType)).ToList();
        //            }
        //        }
        //        else
        //        {
        //            using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //            {
        //                IMapper mapper = GetMapper();
        //                var splitupDetail = _dbGradeHistory.GetAllStudentGradeSplitUpDetailByCourseTypeHistory(schoolId, xmltermIDList, Common.GetXMLFromObject(planCoursesXML), Common.GetXMLFromObject(students), Common.GetXMLFromObject(studentsByCourseType)).ToList();
        //                return mapper.Map<List<GradeHistoryEntity.GetAllStudentGradeSplitUpDetailByCourseTypeHistory_Result>, List<GetAllStudentGradeSplitUpDetailByCourseType_Result>>(splitupDetail);
        //            }
        //        }
        //    }
        //}

        public static bool IsRequestForCurrentAcademicYear(int numberYear, string nTerm, int schoolId)
        {
            return CommonService.IsRequestForCurrentAcademicYear(numberYear, nTerm, schoolId);
        }
      //  public static List<GetStudentGradeInfoCommon_Result> GetStudentGradeInfoCommon(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<PlanCourseDTO> planCourseDTOs, int sid)
      //  {

      //      List<XMLCourseParameter> courseList = new List<XMLCourseParameter>();
      //      List<XMLStudentParameter> students = (sid > 0) ? GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, new List<XMLStudentParameter> { new XMLStudentParameter { SId = sid } })
      //          : GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, null);

      //      var courses = GetXMLCourseParameter(planCourseDTOs);
      //      var xmlCourseList = Common.GetXMLFromObject(courses);
      //      var xmlStudentList = Common.GetXMLFromObject(students);
      //      var studentGradeInfo = new List<GetStudentGradeInfoCommon_Result>();
      //      var IsRequestForCurrentAcademicYear = CommonService.IsRequestForCurrentAcademicYear(0, nTerm, schoolId);
      //      if (IsRequestForCurrentAcademicYear)
      //      {
      //          using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
      //          {
      //              if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
      //              {
      //                  using (var pgLiteDB = new PGGradeDBEntities())
      //                  {
      //                      List<int> _iSid = students.Select(x => x.SId).ToList();

      //                      List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
      //                      List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
      //                      List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



      //                      _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

      //                      _PGTGradeList = (from G in pgLiteDB.PGTGrades
      //                                       join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
      //                                       where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
      //                                       select G).ToList();


      //                      _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
      //                                           join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
      //                                           where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
      //                                           select GE).ToList();




      //                      var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
      //                      var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
      //                      var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

      //                      var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
      //                      var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
      //                      var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



      //                      var dt = new DataTable();
      //                      var conn = _dbGrade.Database.Connection;
      //                      var connectionState = conn.State;
      //                      _dbGrade.Database.CommandTimeout = 16000;


      //                      try
      //                      {


      //                          if (connectionState != ConnectionState.Open) conn.Open();
      //                          using (var cmd = conn.CreateCommand())
      //                          {
      //                              cmd.CommandTimeout = 16000;
      //                              cmd.CommandText = "SQGetStudentGradeInfoCommon";
      //                              cmd.CommandType = CommandType.StoredProcedure;
      //                              cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
      //                              cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
      //                              cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
      //                              cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
      //                              cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
      //                              cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


      //                              using (var reader = cmd.ExecuteReader())
      //                              {
      //                                  dt.Load(reader);
      //                              }

      //                              studentGradeInfo = ServiceHelper.ConvertDataTableToList<GetStudentGradeInfoCommon_Result>(dt);

      //                              try
      //                              {
      //                                  string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
      //                                  pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
      //                              }
      //                              catch (Exception ex)
      //                              {

      //                              }
      //                          }
      //                      }
      //                      catch (Exception ex)
      //                      {
      //                          // return new GetGradeDetailView_Result();

      //                      }
      //                      finally
      //                      {

      //                      }

      //                  }
      //              }
      //              else
      //              {
      //                  studentGradeInfo = _dbGrade.GetStudentGradeInfoCommon(xmlCourseList, xmlStudentList, schoolId).ToList();
      //              }
      //          }

      //          //Check History Table
      //          if (studentGradeInfo == null || (studentGradeInfo != null && studentGradeInfo.Count == 0))
      //          {
      //              using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
      //              {
      //                  var gradeHistory = _dbGradeHistory.GetStudentGradeInfoCommonHistory(xmlCourseList, xmlStudentList, schoolId).ToList();


      //                  var config = new MapperConfiguration(cfg =>
      //                  {

      //                      cfg.AllowNullCollections = true;
      //                      cfg.CreateMap<GradeHistoryEntity.GetStudentGradeInfoCommonHistory_Result, GetStudentGradeInfoCommon_Result>();

      //                  });
      //                  IMapper iMapper = config.CreateMapper();

      //                  studentGradeInfo = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeInfoCommonHistory_Result>, List<GetStudentGradeInfoCommon_Result>>(gradeHistory);
      //              }
      //          }
      //      }
      //      else
      //      {
      //          using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
      //          {
      //              var gradeHistory = _dbGradeHistory.GetStudentGradeInfoCommonHistory(xmlCourseList, xmlStudentList, schoolId).ToList();

      //              var config = new MapperConfiguration(cfg =>
      //              {

      //                  cfg.AllowNullCollections = true;
      //                  cfg.CreateMap<GradeHistoryEntity.GetStudentGradeInfoCommonHistory_Result, GetStudentGradeInfoCommon_Result>();

      //              });
      //              IMapper iMapper = config.CreateMapper();

      //              studentGradeInfo = iMapper.Map<List<GradeHistoryEntity.GetStudentGradeInfoCommonHistory_Result>, List<GetStudentGradeInfoCommon_Result>>(gradeHistory);

      //          }

      //          //Check Main Table
      //          if (studentGradeInfo == null || (studentGradeInfo != null && studentGradeInfo.Count == 0))
      //          {

      //              if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
      //              {
      //                  using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
      //                  {
      //                      using (var pgLiteDB = new PGGradeDBEntities())
      //                      {

      //                          List<int> _iSid = students.Select(x => x.SId).ToList();

      //                          List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
      //                          List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
      //                          List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



      //                          _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

      //                          _PGTGradeList = (from G in pgLiteDB.PGTGrades
      //                                           join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
      //                                           where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
      //                                           select G).ToList();


      //                          _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
      //                                               join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
      //                                               where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
      //                                               select GE).ToList();



      //                          var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
      //                          var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
      //                          var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

      //                          var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
      //                          var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
      //                          var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



      //                          var dt = new DataTable();
      //                          var conn = _dbGrade.Database.Connection;
      //                          var connectionState = conn.State;
      //                          _dbGrade.Database.CommandTimeout = 16000;


      //                          try
      //                          {


      //                              if (connectionState != ConnectionState.Open) conn.Open();
      //                              using (var cmd = conn.CreateCommand())
      //                              {
      //                                  cmd.CommandTimeout = 16000;
      //                                  cmd.CommandText = "SQGetStudentGradeInfoCommon";
      //                                  cmd.CommandType = CommandType.StoredProcedure;
      //                                  cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
      //                                  cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
      //                                  cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
      //                                  cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
      //                                  cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
      //                                  cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


      //                                  using (var reader = cmd.ExecuteReader())
      //                                  {
      //                                      dt.Load(reader);
      //                                  }

      //                                  studentGradeInfo = ServiceHelper.ConvertDataTableToList<GetStudentGradeInfoCommon_Result>(dt);

      //                                  try
      //                                  {
      //                                      string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
      //                                      pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
      //                                  }
      //                                  catch (Exception ex)
      //                                  {

      //                                  }
      //                              }
      //                          }
      //                          catch (Exception ex)
      //                          {
      //                              // return new GetGradeDetailView_Result();

      //                          }
      //                          finally
      //                          {

      //                          }

      //                      }///
						//}
      //              }
      //              else
      //              {
      //                  using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
      //                  {
      //                      studentGradeInfo = _dbGrade.GetStudentGradeInfoCommon(xmlCourseList, xmlStudentList, schoolId).ToList();
      //                  }
      //              }
      //          }
      //      }

      //      return studentGradeInfo;
      //  }

      //  public static List<GetStudentEvaluationScore_Result> GetStudentEvaluationScore(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<XMLStudentParameter> xMLStudents, out int NumberOfStudent, bool thaiLanguage = true, int sid = 0, bool IsRankingReq = false)
      //  {
      //      NumberOfStudent = 0;
      //      using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
      //      using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
      //      {

      //          List<XMLStudentParameter> students = new List<XMLStudentParameter>();
      //          List<PlanCourseDTO> planCourse = new List<PlanCourseDTO>();
      //          if (IsRankingReq)
      //          {
      //              students = GetXMLStudentParameterBynTSubLevel(schoolId, nTerm, nTSubLevel);
      //              foreach (var roomId in students.Select(s => s.NTermSubLevel2).Distinct().ToList())
      //              {
      //                  planCourse.AddRange(GetPlanCourses(nTSubLevel, roomId, nTerm, 0, schoolId, dbschool, thaiLanguage));
      //              }
      //              //planCourse = planCourse.DistinctBy(d => d.PlanCourseId).ToList();
      //          }
      //          else
      //          {
      //              students = GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, xMLStudents);
      //              planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
      //          }
      //          NumberOfStudent = students?.Count ?? 0;
      //          var courses = GetXMLCourseParameter(planCourse.DistinctBy(s => new { s.SPlaneId, s.NTerm, s.nTermSubLevel2 }).ToList());
      //          var xmlCourseList = Common.GetXMLFromObject(courses);
      //          var xmlStudentList = Common.GetXMLFromObject(students);
      //          return _dbGrade.GetStudentEvaluationScore(xmlCourseList, xmlStudentList, schoolId).ToList();
      //      }
      //  }

      //  public static List<GradeHistoryEntity.GetStudentEvaluationScoreHistory_Result> GetStudentEvaluationScoreHistory(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<XMLStudentParameter> xMLStudents, out int NumberOfStudent, bool thaiLanguage = true, int sid = 0, bool IsRankingReq = false)
      //  {

      //      NumberOfStudent = 0;
      //      using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
      //      using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
      //      {
      //          List<XMLStudentParameter> students = new List<XMLStudentParameter>();
      //          List<PlanCourseDTO> planCourse = new List<PlanCourseDTO>();
      //          if (IsRankingReq)
      //          {
      //              students = GetXMLStudentParameterBynTSubLevel(schoolId, nTerm, nTSubLevel);
      //              //planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
      //              foreach (var roomId in students.Select(s => s.NTermSubLevel2).Distinct().ToList())
      //              {
      //                  planCourse.AddRange(GetPlanCourses(nTSubLevel, roomId, nTerm, 0, schoolId, dbschool, thaiLanguage));
      //              }
      //          }
      //          else
      //          {
      //              students = GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, xMLStudents);
      //              planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
      //          }

      //          NumberOfStudent = students?.Count ?? 0;
      //          var courses = GetXMLCourseParameter(planCourse);
      //          var xmlCourseList = Common.GetXMLFromObject(courses);
      //          var xmlStudentList = Common.GetXMLFromObject(students);
      //          return _dbGradeHistory.GetStudentEvaluationScoreHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
      //      }
      //  }

        public static string GetGradeByLanguage(string grade, bool thaiLanguage = true)
        {
            if (!thaiLanguage)
            {
                switch (grade)
                {
                    case "ผ":
                        grade = "Pass";
                        break;
                    case "มผ":
                        grade = "Fail";
                        break;
                    case "ดีเยี่ยม":
                        grade = "Excellent";
                        break;
                    case "ดี":
                        grade = "Good";
                        break;
                    case "พอใช้":
                        grade = "Moderate";
                        break;
                    case "ปรับปรุง":
                        grade = "Improve";
                        break;

                }
                return grade;
            }
            else
            {
                return grade;
            }
        }
        //public static List<GetStudentScoreByBehavior_Result> GetStudentScoreByBehavior(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<XMLStudentParameter> xMLStudents, bool isRequestForCurrentAcademicYear, bool thaiLanguage = true)
        //{
        //    // List<GetStudentScoreByBehavior_Result> behaviour = new List<GetStudentScoreByBehavior_Result>();
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {

        //        List<XMLStudentParameter> students = GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, xMLStudents);
        //        var planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
        //        var courses = GetXMLCourseParameter(planCourse);
        //        var xmlCourseList = Common.GetXMLFromObject(courses);
        //        var xmlStudentList = Common.GetXMLFromObject(students);
        //        if (isRequestForCurrentAcademicYear)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {

        //                    List<int> _iSid = students.Select(x => x.SId).ToList();

        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                    _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                    _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                     join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select G).ToList();


        //                    _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                         join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select GE).ToList();



        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                    var dt = new DataTable();
        //                    var conn = _dbGrade.Database.Connection;
        //                    var connectionState = conn.State;
        //                    _dbGrade.Database.CommandTimeout = 16000;


        //                    try
        //                    {


        //                        if (connectionState != ConnectionState.Open) conn.Open();
        //                        using (var cmd = conn.CreateCommand())
        //                        {
        //                            cmd.CommandTimeout = 16000;
        //                            cmd.CommandText = "SQGetStudentScoreByBehavior";
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                            using (var reader = cmd.ExecuteReader())
        //                            {
        //                                dt.Load(reader);
        //                            }

        //                            var behaviour = ServiceHelper.ConvertDataTableToList<GetStudentScoreByBehavior_Result>(dt);
        //                            return behaviour;

        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        return new List<GetStudentScoreByBehavior_Result>();

        //                    }
        //                    finally
        //                    {

        //                    }

        //                }
        //            }
        //            else
        //            {
        //                var behaviour = _dbGrade.GetStudentScoreByBehavior(xmlCourseList, xmlStudentList, schoolId).ToList();

        //                //Check History Table
        //                if (behaviour == null || (behaviour != null && behaviour.Count == 0))
        //                {
        //                    var behaviourHistory = _dbGradeHistory.GetStudentScoreByBehaviorHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                    var config = new MapperConfiguration(cfg =>
        //                    {

        //                        cfg.AllowNullCollections = true;
        //                        cfg.CreateMap<GradeHistoryEntity.GetStudentScoreByBehaviorHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreByBehavior_Result>();

        //                    });
        //                    IMapper iMapper = config.CreateMapper();
        //                    behaviour = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreByBehaviorHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreByBehavior_Result>>(behaviourHistory);
        //                }
        //                return behaviour;
        //            }
        //        }
        //        else
        //        {
        //            var behaviourHistory = _dbGradeHistory.GetStudentScoreByBehaviorHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //            var config = new MapperConfiguration(cfg =>
        //            {

        //                cfg.AllowNullCollections = true;
        //                cfg.CreateMap<GradeHistoryEntity.GetStudentScoreByBehaviorHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreByBehavior_Result>();

        //            });
        //            IMapper iMapper = config.CreateMapper();
        //            var behaviour = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreByBehaviorHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreByBehavior_Result>>(behaviourHistory);

        //            //Check Main Table
        //            if (behaviour == null || (behaviour != null && behaviour.Count == 0))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    using (var pgLiteDB = new PGGradeDBEntities())
        //                    {

        //                        List<int> _iSid = students.Select(x => x.SId).ToList();

        //                        List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                        List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                        List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                        _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                        _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                         join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select G).ToList();


        //                        _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                             join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                             where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                             select GE).ToList();



        //                        var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                        var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                        var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                        var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                        var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                        var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                        var dt = new DataTable();
        //                        var conn = _dbGrade.Database.Connection;
        //                        var connectionState = conn.State;
        //                        _dbGrade.Database.CommandTimeout = 16000;


        //                        try
        //                        {


        //                            if (connectionState != ConnectionState.Open) conn.Open();
        //                            using (var cmd = conn.CreateCommand())
        //                            {
        //                                cmd.CommandTimeout = 16000;
        //                                cmd.CommandText = "SQGetStudentScoreByBehavior";
        //                                cmd.CommandType = CommandType.StoredProcedure;
        //                                cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                                cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                                using (var reader = cmd.ExecuteReader())
        //                                {
        //                                    dt.Load(reader);
        //                                }

        //                                behaviour = ServiceHelper.ConvertDataTableToList<GetStudentScoreByBehavior_Result>(dt);
        //                                return behaviour;

        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            return new List<GetStudentScoreByBehavior_Result>();

        //                        }
        //                        finally
        //                        {

        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    behaviour = _dbGrade.GetStudentScoreByBehavior(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                }
        //            }
        //            return behaviour;
        //        }
        //    }
        //}

        //public static List<GetStudentScoreByReadWrite_Result> GetStudentScoreByReadWrite(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<XMLStudentParameter> xMLStudents, bool isRequestForCurrentAcademicYear, bool thaiLanguage = true)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {

        //        List<XMLStudentParameter> students = GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, xMLStudents);
        //        var planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
        //        var courses = GetXMLCourseParameter(planCourse);
        //        var xmlCourseList = Common.GetXMLFromObject(courses);
        //        var xmlStudentList = Common.GetXMLFromObject(students);


        //        if (isRequestForCurrentAcademicYear)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {
        //                    List<int> _iSid = students.Select(x => x.SId).ToList();

        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();

        //                    List<PGTAssessment> _PGPGTAssessmentList = new List<PGTAssessment>();


        //                    _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                    _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                     join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select G).ToList();


        //                    _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                         join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select GE).ToList();

        //                    _PGPGTAssessmentList = pgLiteDB.PGTAssessments.Where(x => x.SchoolId == schoolId).ToList();


        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);
        //                    var TAssessmentDatas = ServiceHelper.GetXMLTAssessmentParameter(_PGPGTAssessmentList);


        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);
        //                    var xmlTAssessmentList = Common.GetXMLFromObject(TAssessmentDatas);



        //                    var dt = new DataTable();
        //                    var conn = _dbGrade.Database.Connection;
        //                    var connectionState = conn.State;
        //                    _dbGrade.Database.CommandTimeout = 16000;


        //                    try
        //                    {


        //                        if (connectionState != ConnectionState.Open) conn.Open();
        //                        using (var cmd = conn.CreateCommand())
        //                        {
        //                            cmd.CommandTimeout = 16000;
        //                            cmd.CommandText = "SQGetStudentScoreByReadWrite";
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTAssessmentList", xmlTAssessmentList));


        //                            using (var reader = cmd.ExecuteReader())
        //                            {
        //                                dt.Load(reader);
        //                            }

        //                            var readWrite = ServiceHelper.ConvertDataTableToList<GetStudentScoreByReadWrite_Result>(dt);
        //                            return readWrite;

        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        return new List<GetStudentScoreByReadWrite_Result>();

        //                    }
        //                    finally
        //                    {

        //                    }

        //                }
        //            }
        //            else
        //            {
        //                var readWrite = _dbGrade.GetStudentScoreByReadWrite(xmlCourseList, xmlStudentList, schoolId).ToList();


        //                //Check History Table
        //                if (readWrite == null || (readWrite != null && readWrite.Count == 0))
        //                {
        //                    var readWriteHistory = _dbGradeHistory.GetStudentScoreByReadWriteHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                    var config = new MapperConfiguration(cfg =>
        //                    {

        //                        cfg.AllowNullCollections = true;
        //                        cfg.CreateMap<GradeHistoryEntity.GetStudentScoreByReadWriteHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreByReadWrite_Result>();

        //                    });
        //                    IMapper iMapper = config.CreateMapper();
        //                    readWrite = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreByReadWriteHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreByReadWrite_Result>>(readWriteHistory);

        //                }
        //                return readWrite;

        //            }
        //        }
        //        else
        //        {

        //            var readWriteHistory = _dbGradeHistory.GetStudentScoreByReadWriteHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //            var config = new MapperConfiguration(cfg =>
        //            {

        //                cfg.AllowNullCollections = true;
        //                cfg.CreateMap<GradeHistoryEntity.GetStudentScoreByReadWriteHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreByReadWrite_Result>();

        //            });
        //            IMapper iMapper = config.CreateMapper();
        //            var readWrite = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreByReadWriteHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreByReadWrite_Result>>(readWriteHistory);

        //            //Check Main Table
        //            if (readWrite == null || (readWrite != null && readWrite.Count == 0))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    using (var pgLiteDB = new PGGradeDBEntities())
        //                    {
        //                        List<int> _iSid = students.Select(x => x.SId).ToList();

        //                        List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                        List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                        List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();

        //                        List<PGTAssessment> _PGPGTAssessmentList = new List<PGTAssessment>();


        //                        _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                        _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                         join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select G).ToList();


        //                        _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                             join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                             where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                             select GE).ToList();

        //                        _PGPGTAssessmentList = pgLiteDB.PGTAssessments.Where(x => x.SchoolId == schoolId).ToList();


        //                        var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                        var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                        var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);
        //                        var TAssessmentDatas = ServiceHelper.GetXMLTAssessmentParameter(_PGPGTAssessmentList);


        //                        var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                        var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                        var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);
        //                        var xmlTAssessmentList = Common.GetXMLFromObject(TAssessmentDatas);


        //                        var dt = new DataTable();
        //                        var conn = _dbGrade.Database.Connection;
        //                        var connectionState = conn.State;
        //                        _dbGrade.Database.CommandTimeout = 16000;


        //                        try
        //                        {


        //                            if (connectionState != ConnectionState.Open) conn.Open();
        //                            using (var cmd = conn.CreateCommand())
        //                            {
        //                                cmd.CommandTimeout = 16000;
        //                                cmd.CommandText = "SQGetStudentScoreByReadWrite";
        //                                cmd.CommandType = CommandType.StoredProcedure;
        //                                cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                                cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTAssessmentList", xmlTAssessmentList));


        //                                using (var reader = cmd.ExecuteReader())
        //                                {
        //                                    dt.Load(reader);
        //                                }

        //                                readWrite = ServiceHelper.ConvertDataTableToList<GetStudentScoreByReadWrite_Result>(dt);
        //                                return readWrite;

        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            return new List<GetStudentScoreByReadWrite_Result>();

        //                        }
        //                        finally
        //                        {

        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    readWrite = _dbGrade.GetStudentScoreByReadWrite(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                }
        //            }
        //            return readWrite;
        //        }
        //    }
        //}

        //public static List<GetStudentScoreBySamattana_Result> GetStudentScoreBySamattana(int schoolId, string nTerm, int nTermSubLevel2, int nTSubLevel, List<XMLStudentParameter> xMLStudents, bool IsRequestForCurrentAcademicYear, bool thaiLanguage = true)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {

        //        List<XMLStudentParameter> students = GetXMLStudentParameter(schoolId, nTerm, nTermSubLevel2, xMLStudents);
        //        var planCourse = GetPlanCourses(nTSubLevel, nTermSubLevel2, nTerm, 0, schoolId, dbschool, thaiLanguage);
        //        var courses = GetXMLCourseParameter(planCourse);
        //        var xmlCourseList = Common.GetXMLFromObject(courses);
        //        var xmlStudentList = Common.GetXMLFromObject(students);

        //        if (IsRequestForCurrentAcademicYear)
        //        {
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {

        //                    List<int> _iSid = students.Select(x => x.SId).ToList();

        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                    _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                    _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                     join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select G).ToList();


        //                    _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                         join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select GE).ToList();



        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                    var dt = new DataTable();
        //                    var conn = _dbGrade.Database.Connection;
        //                    var connectionState = conn.State;
        //                    _dbGrade.Database.CommandTimeout = 16000;


        //                    try
        //                    {


        //                        if (connectionState != ConnectionState.Open) conn.Open();
        //                        using (var cmd = conn.CreateCommand())
        //                        {
        //                            cmd.CommandTimeout = 16000;
        //                            cmd.CommandText = "SQGetStudentScoreBySamattana";
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                            using (var reader = cmd.ExecuteReader())
        //                            {
        //                                dt.Load(reader);
        //                            }

        //                            var samattana = ServiceHelper.ConvertDataTableToList<GetStudentScoreBySamattana_Result>(dt);
        //                            return samattana;

        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        return new List<GetStudentScoreBySamattana_Result>();

        //                    }
        //                    finally
        //                    {

        //                    }

        //                }
        //            }
        //            else
        //            {
        //                var samattana = _dbGrade.GetStudentScoreBySamattana(xmlCourseList, xmlStudentList, schoolId).ToList();

        //                //if main table doesn't exist then check in the history
        //                if (samattana == null || (samattana != null && samattana.Count == 0))
        //                {
        //                    var samattanaHistory = _dbGradeHistory.GetStudentScoreBySamattanaHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                    var config = new MapperConfiguration(cfg =>
        //                    {

        //                        cfg.AllowNullCollections = true;
        //                        cfg.CreateMap<GradeHistoryEntity.GetStudentScoreBySamattanaHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreBySamattana_Result>();

        //                    });
        //                    IMapper iMapper = config.CreateMapper();
        //                    samattana = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreBySamattanaHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreBySamattana_Result>>(samattanaHistory);
        //                }
        //                return samattana;
        //            }
        //        }
        //        else
        //        {

        //            var samattanaHistory = _dbGradeHistory.GetStudentScoreBySamattanaHistory(xmlCourseList, xmlStudentList, schoolId).ToList();
        //            var config = new MapperConfiguration(cfg =>
        //            {

        //                cfg.AllowNullCollections = true;
        //                cfg.CreateMap<GradeHistoryEntity.GetStudentScoreBySamattanaHistory_Result, JabjaiSchoolGradeEntity.GetStudentScoreBySamattana_Result>();

        //            });
        //            IMapper iMapper = config.CreateMapper();
        //            var samattana = iMapper.Map<List<GradeHistoryEntity.GetStudentScoreBySamattanaHistory_Result>, List<JabjaiSchoolGradeEntity.GetStudentScoreBySamattana_Result>>(samattanaHistory);

        //            //Check history table doesn't have value then check in the main table
        //            if (samattana == null || (samattana != null && samattana.Count == 0))
        //            {
        //                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //                {
        //                    using (var pgLiteDB = new PGGradeDBEntities())
        //                    {

        //                        List<int> _iSid = students.Select(x => x.SId).ToList();

        //                        List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                        List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                        List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                        _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                        _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                         join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select G).ToList();


        //                        _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                             join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                             where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                             select GE).ToList();



        //                        var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                        var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                        var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                        var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                        var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                        var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                        var dt = new DataTable();
        //                        var conn = _dbGrade.Database.Connection;
        //                        var connectionState = conn.State;
        //                        _dbGrade.Database.CommandTimeout = 16000;


        //                        try
        //                        {


        //                            if (connectionState != ConnectionState.Open) conn.Open();
        //                            using (var cmd = conn.CreateCommand())
        //                            {
        //                                cmd.CommandTimeout = 16000;
        //                                cmd.CommandText = "SQGetStudentScoreBySamattana";
        //                                cmd.CommandType = CommandType.StoredProcedure;
        //                                cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                                cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                                cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                                using (var reader = cmd.ExecuteReader())
        //                                {
        //                                    dt.Load(reader);
        //                                }

        //                                samattana = ServiceHelper.ConvertDataTableToList<GetStudentScoreBySamattana_Result>(dt);
        //                                return samattana;

        //                            }
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            return new List<GetStudentScoreBySamattana_Result>();

        //                        }
        //                        finally
        //                        {

        //                        }

        //                    }
        //                }
        //                else
        //                {
        //                    samattana = _dbGrade.GetStudentScoreBySamattana(xmlCourseList, xmlStudentList, schoolId).ToList();
        //                }
        //            }
        //            return samattana;


        //        }
        //    }
        //}

        //public static List<StudentGrade> GetStudentGrade(int schoolId, string nTerm, int nTSubLevel, List<int> sIds)
        //{
        //    List<StudentGrade> _StudentGradeList = new List<StudentGrade>();

        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    {
        //        List<StudentGrade> studentGrades = new List<StudentGrade>();
        //        List<PlanCourseDTO> planCourseDTOs = new List<PlanCourseDTO>();
        //        //Get Student Information
        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTSubLevel == nTSubLevel)
        //                        where sIds.Contains(s.sID)
        //                        join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                        join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
        //                        join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel) on s.nTSubLevel equals t.nTSubLevel
        //                        join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            SName = s.sName,
        //                            SLastname = s.sLastname,
        //                            SStudentTitle = s.titleDescription,
        //                            STerm = s.sTerm,
        //                            SStudentID = s.sStudentID,
        //                            NStudentNumber = s.nStudentNumber ?? 0,
        //                            SStudentNameEn = u.sStudentNameEN,
        //                            SStudentLastEn = u.sStudentLastEN,
        //                            NStudentStatus = s.nStudentStatus ?? 0,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            NTSubLevel = s.nTSubLevel,
        //                            SIdentification = u.sIdentification,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NYear = y.nYear,
        //                            ClassFullName = t.fullName,
        //                            ClassName = t.SubLevel,
        //                            RoomName = r.nTSubLevel2
        //                        }).Distinct().AsQueryable().ToList();

        //        if (students != null)
        //        {


        //            foreach (var student in students)
        //            {
        //                if (planCourseDTOs.Where(w => w.NTerm == student.NTerm && w.nTermSubLevel2 == student.NTermSubLevel2).Count() == 0)
        //                {
        //                    //Get Course Details from Plan
        //                    var planCourse = GetPlanCourses(nTSubLevel, student.NTermSubLevel2, student.NTerm, 0, schoolId, dbschool);
        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                    else //If No Plan Get the detail from Tplane table
        //                    {
        //                        planCourse = (from a in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null && w.nTSubLevel == student.NTSubLevel.ToString())
        //                                      join y in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals y.courseTypeId
        //                                      join g in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
        //                                      orderby a.courseCode
        //                                      select new PlanCourseDTO
        //                                      {
        //                                          NYear = student.NYear,
        //                                          CourseName = a.sPlaneName,
        //                                          CourseType = a.courseType,
        //                                          CourseTypeName = y.Description,
        //                                          CourseGroup = a.courseGroup,
        //                                          CourseGroupName = g.Description,
        //                                          CourseCode = a.courseCode,
        //                                          SPlaneId = a.sPlaneID,
        //                                          NCredit = a.nCredit,
        //                                          CourseTotalHour = a.courseTotalHour,
        //                                          CourseStatus = 1,
        //                                          CourseHour = a.courseHour,
        //                                          NTSubLevel = nTSubLevel.ToString(),
        //                                          NTerm = student.NTerm,
        //                                          nTermSubLevel2 = student.NTermSubLevel2,
        //                                          NOrder = y.nOrder ?? 0
        //                                      }).ToList();

        //                        if (planCourse != null && planCourse.Count > 0)
        //                            planCourseDTOs.AddRange(planCourse);
        //                    }
        //                }
        //            }
        //        }

        //        var courses = GetXMLCourseParameter(planCourseDTOs);

        //        var xmlCourseList = Common.GetXMLFromObject(courses);
        //        var xmlStudentList = Common.GetXMLFromObject(students);
        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {

        //                List<int> _iSid = students.Select(x => x.SId).ToList();

        //                List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                 join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                 where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                 select G).ToList();


        //                _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                     join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select GE).ToList();



        //                var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;


        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetStudentGradeTranscriptInfo";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLStudentList", xmlStudentList));
        //                        cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        var gradeInfo = ServiceHelper.ConvertDataTableToList<GetStudentGradeTranscriptInfo_Result>(dt);


        //                        try
        //                        {
        //                            string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
        //                            pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
        //                        }
        //                        catch (Exception ex)
        //                        {

        //                        }

        //                        if (gradeInfo != null)
        //                        {
        //                            _StudentGradeList = gradeInfo.Where(w => !string.IsNullOrEmpty(w.getGradeLabel) || (w.GradeSet != null)).Select(s => new StudentGrade
        //                            {
        //                                CourseCode = s.CourseCode,
        //                                sID = s.sID ?? 0,
        //                                Identification = s.sIdentification,
        //                                PlaneID = s.sPlaneID ?? 0,
        //                                Term = s.sTerm,
        //                                Year = s.numberYear ?? 0,
        //                                Grade = (s.GradeSet == null) ? s.getGradeLabel : ((decimal)s.GradeSet).ToString("0.0"),
        //                            }).ToList();

        //                        }

        //                        return _StudentGradeList;
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    return _StudentGradeList;

        //                }
        //                finally
        //                {

        //                }

        //            }
        //        }
        //        else
        //        {
        //            var gradeInfo = new List<GetStudentGradeTranscriptInfo_Result>();
        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {

        //                    List<int> _iSid = students.Select(x => x.SId).ToList();

        //                    List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                    List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                    List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();



        //                    _PGTGradeDetailList = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && _iSid.Contains(x.sID)).ToList();

        //                    _PGTGradeList = (from G in pgLiteDB.PGTGrades
        //                                     join GD in pgLiteDB.PGTGradeDetails on G.nGradeId equals GD.nGradeId
        //                                     where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                     select G).ToList();


        //                    _PGTGradeEditList = (from GE in pgLiteDB.PGTGradeEdits
        //                                         join GD in pgLiteDB.PGTGradeDetails on GE.GradeDetailID equals GD.nGradeDetailId
        //                                         where GD.SchoolID == schoolId && _iSid.Contains(GD.sID)
        //                                         select GE).ToList();



        //                    var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                    var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                    var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                    var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                    var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                    var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);



        //                    var dt = new DataTable();
        //                    var conn = _dbGrade.Database.Connection;
        //                    var connectionState = conn.State;
        //                    _dbGrade.Database.CommandTimeout = 16000;


        //                    try
        //                    {


        //                        if (connectionState != ConnectionState.Open) conn.Open();
        //                        using (var cmd = conn.CreateCommand())
        //                        {
        //                            cmd.CommandTimeout = 16000;
        //                            cmd.CommandText = "SQGetStudentGradeTranscriptInfo";
        //                            cmd.CommandType = CommandType.StoredProcedure;
        //                            cmd.Parameters.Add(new SqlParameter("@XMLCourseList", xmlCourseList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLStudentList", Common.GetXMLFromObject(xmlStudentList)));
        //                            cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                            cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                            using (var reader = cmd.ExecuteReader())
        //                            {
        //                                dt.Load(reader);
        //                            }

        //                            gradeInfo = ServiceHelper.ConvertDataTableToList<GetStudentGradeTranscriptInfo_Result>(dt);


        //                            try
        //                            {
        //                                string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
        //                                pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
        //                            }
        //                            catch (Exception ex)
        //                            {

        //                            }
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        // return new GetGradeDetailView_Result();

        //                    }
        //                    finally
        //                    {
        //                        //if (connectionState != ConnectionState.Closed) conn.Close();
        //                    }

        //                }
        //            }
        //            else
        //            {
        //                gradeInfo = _dbGrade.GetStudentGradeTranscriptInfo(xmlCourseList, xmlStudentList, schoolId).ToList();

        //            }


        //            if (gradeInfo != null)
        //            {
        //                _StudentGradeList = gradeInfo.Where(w => !string.IsNullOrEmpty(w.getGradeLabel) || (w.GradeSet != null)).Select(s => new StudentGrade
        //                {
        //                    CourseCode = s.CourseCode,
        //                    sID = s.sID ?? 0,
        //                    Identification = s.sIdentification,
        //                    PlaneID = s.sPlaneID ?? 0,
        //                    Term = s.sTerm,
        //                    Year = s.numberYear ?? 0,
        //                    Grade = (s.GradeSet == null) ? s.getGradeLabel : ((decimal)s.GradeSet).ToString("0.0"),
        //                }).ToList();

        //            }

        //            return _StudentGradeList;
        //        }
        //    }
        //}

        ////pw1
        //public static List<XMLStudentParameter> GetStudentInfoForTranscript(List<int> nTSubLevels, int schoolId, int sId, out List<PlanCourseDTO> planCourseDTOs)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {
        //        List<StudentGrade> studentGrades = new List<StudentGrade>();
        //        planCourseDTOs = new List<PlanCourseDTO>();
        //        //Get Student Information
        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId)
        //                        where nTSubLevels.Contains(s.nTSubLevel)
        //                        join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                        join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
        //                        join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
        //                        join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            SName = s.sName,
        //                            SLastname = s.sLastname,
        //                            SStudentTitle = s.titleDescription,
        //                            STerm = s.sTerm,
        //                            SStudentID = s.sStudentID,
        //                            NStudentNumber = s.nStudentNumber ?? 0,
        //                            SStudentNameEn = u.sStudentNameEN,
        //                            SStudentLastEn = u.sStudentLastEN,
        //                            NStudentStatus = s.nStudentStatus ?? 0,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            NTSubLevel = s.nTSubLevel,
        //                            SIdentification = u.sIdentification,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NYear = y.nYear,
        //                            ClassFullName = t.fullName,
        //                            ClassName = t.SubLevel,
        //                            RoomName = r.nTSubLevel2
        //                        }).Distinct().AsQueryable().ToList();


        //        if (students != null)
        //        {
        //            var previouseYearGrade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId)
        //                                      join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                                      where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                                      select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, g.PlanId }).Distinct().ToList();

        //            var previouseYearGradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId)
        //                                             join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                                             where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                                             select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, g.PlanId }).Distinct().ToList();

        //            if (previouseYearGrade != null && previouseYearGradeHistory != null)
        //            {
        //                previouseYearGrade.AddRange(previouseYearGradeHistory);
        //            }
        //            else if (previouseYearGrade == null && previouseYearGradeHistory != null)
        //            {
        //                previouseYearGrade = previouseYearGradeHistory;
        //            }


        //            //Previous Year Record to fetch the grade
        //            var year = students.Min(m => m.NumberYear);
        //            if (previouseYearGrade != null && previouseYearGrade.Count > 0)
        //            {

        //                year = year - 1;
        //                var tTerm = (from y in dbschool.TYears.Where(w => w.SchoolID == schoolId).ToList()
        //                             join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && (string.IsNullOrEmpty(w.cDel) || w.cDel == "0")).ToList() on y.nYear equals t.nYear
        //                             join py in previouseYearGrade on t.nTerm equals py.nTerm
        //                             select new { t.nTerm, t.sTerm, y.numberYear, t.nYear }).Distinct().ToList();

        //                if (tTerm != null && students != null)
        //                {
        //                    var student = students.FirstOrDefault();
        //                    foreach (var t in tTerm)
        //                    {
        //                        if (students.Where(w => w.NTerm == t.nTerm).Count() == 0)
        //                        {
        //                            students.Add(new XMLStudentParameter
        //                            {
        //                                SId = student.SId,
        //                                SName = student.SName,
        //                                SLastname = student.SLastname,
        //                                SStudentTitle = student.SStudentTitle,
        //                                STerm = t.sTerm,
        //                                SStudentID = student.SStudentID,
        //                                NStudentNumber = student.NStudentNumber,
        //                                SStudentNameEn = student.SStudentNameEn,
        //                                SStudentLastEn = student.SStudentLastEn,
        //                                NStudentStatus = student.NStudentStatus,
        //                                NTerm = t.nTerm,
        //                                //NTermSubLevel2 = s.nTermSubLevel2,
        //                                //NTSubLevel = student.NTSubLevel,
        //                                SIdentification = student.SIdentification,
        //                                NumberYear = t.numberYear ?? 0,
        //                                NYear = t.nYear ?? 0,
        //                                //ClassFullName = t.fullName,
        //                                //ClassName = t.SubLevel,
        //                                //RoomName = r.nTSubLevel2
        //                            });
        //                        }
        //                    }
        //                }
        //            }



        //            foreach (var student in students)
        //            {
        //                if (planCourseDTOs.Where(w => w.NTerm == student.NTerm && w.nTermSubLevel2 == student.NTermSubLevel2).Count() == 0)
        //                {
        //                    //Get Course Details from Plan
        //                    var planCourse = GetPlanCourses(student.NTSubLevel, student.NTermSubLevel2, student.NTerm, 0, schoolId, dbschool);


        //                    //if (student.NTermSubLevel2 == 0)  // If imported data then check filter plan id used to import the old grade
        //                    //{
        //                    //    var planId = (from p in planCourse
        //                    //                  join pg in previouseYearGrade on new { p.NTerm, p.SPlaneId, p.PlanId } equals new { NTerm = pg.nTerm, SPlaneId = pg.sPlaneID, PlanId = pg.PlanId ?? 0 }
        //                    //                  select pg.PlanId).FirstOrDefault();
        //                    //    if (planId != null)
        //                    //    {
        //                    //        planCourse = planCourse.Where(w => w.PlanId == planId).ToList();
        //                    //    }
        //                    //}


        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                    else if (planCourseDTOs.Where(w => w.NTerm == student.NTerm).Count() == 0) //If No Plan Get the detail from Tplane table
        //                    {

        //                        var previouseYearCourse = (from gd in previouseYearGrade
        //                                                   join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && w.nTerm == student.NTerm) on gd.nTerm equals t.nTerm
        //                                                   join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                                                   join a in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null) on gd.sPlaneID equals a.sPlaneID
        //                                                   join ct in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals ct.courseTypeId
        //                                                   join g in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
        //                                                   orderby a.courseCode
        //                                                   select new PlanCourseDTO
        //                                                   {
        //                                                       NYear = y.nYear,
        //                                                       CourseName = a.sPlaneName,
        //                                                       CourseType = a.courseType,
        //                                                       CourseTypeName = ct.Description,
        //                                                       CourseGroup = a.courseGroup,
        //                                                       CourseGroupName = g.Description,
        //                                                       CourseCode = a.courseCode,
        //                                                       SPlaneId = a.sPlaneID,
        //                                                       NCredit = a.nCredit,
        //                                                       CourseTotalHour = a.courseTotalHour,
        //                                                       CourseStatus = 1,
        //                                                       CourseHour = a.courseHour,
        //                                                       NTSubLevel = a.nTSubLevel,
        //                                                       NTerm = t.nTerm,
        //                                                       //nTermSubLevel2 = student.NTermSubLevel2,
        //                                                       NOrder = ct.nOrder ?? 0
        //                                                   }).ToList();

        //                        //Check plan exist for Class. If plan exist then take the Ncredit and value from Plan.
        //                        if (previouseYearCourse != null && previouseYearCourse.Count > 0)
        //                        {
        //                            var ntSubLevels = previouseYearCourse.Select(s => Int32.Parse(s.NTSubLevel)).Distinct().ToList();
        //                            var plandetails = new List<PlanCourseDTO>();

        //                            //Get Plan Detail for Previouse Year.
        //                            foreach (var c in ntSubLevels)
        //                            {
        //                                var nTermSubLevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c && w.nWorkingStatus == 1 && w.nTermSubLevel2Status == "1").OrderBy(o => o.nTSubLevel2).Select(s => s.nTermSubLevel2).FirstOrDefault();
        //                                var tSubLevel = dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c).FirstOrDefault();
        //                                student.ClassFullName = tSubLevel.fullName;
        //                                student.ClassName = tSubLevel.SubLevel;
        //                                planCourse = GetPlanCourses(c, nTermSubLevel2, student.NTerm, 0, schoolId, dbschool);
        //                                if (planCourse != null && planCourse.Count > 0)
        //                                {
        //                                    plandetails.AddRange(planCourse);
        //                                }
        //                            }
        //                            foreach (var c in previouseYearCourse)
        //                            {
        //                                var course = plandetails.Where(w => w.SPlaneId == c.SPlaneId && w.NTerm == c.NTerm).FirstOrDefault();
        //                                if (course != null)
        //                                {
        //                                    planCourseDTOs.Add(course);
        //                                }
        //                                else
        //                                {
        //                                    planCourseDTOs.Add(c);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return students;
        //    }
        //}

        //public static List<XMLStudentParameter> GetStudentInfoForTranscriptNew(List<int> nTSubLevels, int schoolId, int sId, List<string> nTerm, out List<PlanCourseDTO> planCourseDTOs, string lang = "thai")
        //{

        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {
        //        List<StudentGrade> studentGrades = new List<StudentGrade>();
        //        planCourseDTOs = new List<PlanCourseDTO>();


        //        var grade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId)
        //                     join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                     where gd.sID == sId && gd.cDel == false && nTerm.Contains(g.nTerm) && g.nTermSubLevel2 == null
        //                     select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();


        //        if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //        {
        //            using (var pgLiteDB = new PGGradeDBEntities())
        //            {
        //                grade = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId)
        //                         join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                         where gd.sID == sId && gd.cDel == false && nTerm.Contains(g.nTerm) && g.nTermSubLevel2 == null
        //                         select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();

        //            }
        //        }

        //        var gradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId)
        //                            join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                            where gd.sID == sId && gd.cDel == false && nTerm.Contains(g.nTerm) && g.nTermSubLevel2 == null
        //                            select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();

        //        if (grade != null && grade.Count == 0 && (nTerm == null || (nTerm != null && nTerm.Count == 0)))
        //        {
        //            grade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId)
        //                     join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                     where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                     select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();


        //            if (SchoolBright.DataAccess.DataAccessHelper.bUsePGLiteDBForGrade)
        //            {
        //                using (var pgLiteDB = new PGGradeDBEntities())
        //                {
        //                    grade = (from g in pgLiteDB.PGTGrades.Where(w => w.SchoolID == schoolId)
        //                             join gd in pgLiteDB.PGTGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                             where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                             select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();

        //                }
        //            }
        //        }

        //        if (gradeHistory != null && gradeHistory.Count == 0 && (nTerm == null || (nTerm != null && nTerm.Count == 0)))
        //        {
        //            gradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId)
        //                            join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                            where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                            select new { sPlaneID = (g.sPlaneID ?? 0), g.nTerm, PlanId = gd.PlanId ?? 0, nTermSubLevel2 = g.nTermSubLevel2 }).Distinct().ToList();
        //        }


        //        if (grade != null && grade != null && gradeHistory != null && gradeHistory.Count > 0)
        //        {
        //            grade.AddRange(gradeHistory);
        //        }
        //        else if (grade == null && gradeHistory != null)
        //        {
        //            grade = gradeHistory;
        //        }
        //        if (nTerm == null || (nTerm != null && nTerm.Count == 0))
        //        {
        //            nTerm = grade.Select(s => s.nTerm).Distinct().ToList();
        //        }
        //        //Get Student Information
        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId)
        //                        where nTSubLevels.Contains(s.nTSubLevel) && nTerm.Contains(s.nTerm)
        //                        join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                        //join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
        //                        //join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
        //                        //join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            SName = (lang == "eng") ? s.sStudentNameEN : s.sName,
        //                            SLastname = (lang == "eng") ? s.sStudentLastEN : s.sLastname,
        //                            SStudentTitle = (lang == "eng") ? "" : s.titleDescription,
        //                            STerm = s.sTerm,
        //                            SStudentID = s.sStudentID,
        //                            NStudentNumber = s.nStudentNumber ?? 0,
        //                            SStudentNameEn = u.sStudentNameEN,
        //                            SStudentLastEn = u.sStudentLastEN,
        //                            NStudentStatus = s.nStudentStatus ?? 0,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            NTSubLevel = s.nTSubLevel,
        //                            SIdentification = u.sIdentification,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NYear = s.nYear,
        //                            ClassFullName = (lang == "eng") ? s.ClassFullNameEng : s.ClassFullName,
        //                            ClassName = (lang == "eng") ? s.SubLevelEN : s.SubLevel,
        //                            RoomName = s.nTSubLevel2
        //                        }).Distinct().AsQueryable().ToList();


        //        if (students != null)
        //        {
        //            var studentInfo = students.FirstOrDefault();

        //            if (studentInfo == null)
        //            {
        //                studentInfo = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId)
        //                               where nTSubLevels.Contains(s.nTSubLevel)
        //                               join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                               select new XMLStudentParameter
        //                               {
        //                                   SId = s.sID,
        //                                   SName = (lang == "eng") ? s.sStudentNameEN : s.sName,
        //                                   SLastname = (lang == "eng") ? s.sStudentLastEN : s.sLastname,
        //                                   SStudentTitle = (lang == "eng") ? "" : s.titleDescription,
        //                                   STerm = s.sTerm,
        //                                   SStudentID = s.sStudentID,
        //                                   NStudentNumber = s.nStudentNumber ?? 0,
        //                                   SStudentNameEn = u.sStudentNameEN,
        //                                   SStudentLastEn = u.sStudentLastEN,
        //                                   NStudentStatus = s.nStudentStatus ?? 0,
        //                                   NTerm = s.nTerm,
        //                                   NTermSubLevel2 = s.nTermSubLevel2,
        //                                   NTSubLevel = s.nTSubLevel,
        //                                   SIdentification = u.sIdentification,
        //                                   NumberYear = s.numberYear ?? 0,
        //                                   NYear = s.nYear,
        //                                   ClassFullName = (lang == "eng") ? s.ClassFullNameEng : s.ClassFullName,
        //                                   ClassName = (lang == "eng") ? s.SubLevelEN : s.SubLevel,
        //                                   RoomName = s.nTSubLevel2
        //                               }).Distinct().AsQueryable().FirstOrDefault();

        //                if (studentInfo == null)
        //                {
        //                    studentInfo = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId)
        //                                   join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                                   select new XMLStudentParameter
        //                                   {
        //                                       SId = s.sID,
        //                                       SName = (lang == "eng") ? s.sStudentNameEN : s.sName,
        //                                       SLastname = (lang == "eng") ? s.sStudentLastEN : s.sLastname,
        //                                       SStudentTitle = (lang == "eng") ? "" : s.titleDescription,
        //                                       STerm = s.sTerm,
        //                                       SStudentID = s.sStudentID,
        //                                       NStudentNumber = s.nStudentNumber ?? 0,
        //                                       SStudentNameEn = u.sStudentNameEN,
        //                                       SStudentLastEn = u.sStudentLastEN,
        //                                       NStudentStatus = s.nStudentStatus ?? 0,
        //                                       NTerm = s.nTerm,
        //                                       NTermSubLevel2 = s.nTermSubLevel2,
        //                                       NTSubLevel = s.nTSubLevel,
        //                                       SIdentification = u.sIdentification,
        //                                       NumberYear = s.numberYear ?? 0,
        //                                       NYear = s.nYear,
        //                                       ClassFullName = (lang == "eng") ? s.ClassFullNameEng : s.ClassFullName,
        //                                       ClassName = (lang == "eng") ? s.SubLevelEN : s.SubLevel,
        //                                       RoomName = s.nTSubLevel2
        //                                   }).Distinct().AsQueryable().FirstOrDefault();
        //                }
        //            }

        //            var previouseYearGrade = (from g in grade
        //                                      join p in dbschool.TPlanes.Where(w => w.SchoolID == schoolId) on g.sPlaneID equals p.sPlaneID
        //                                      join c in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on p.nTSubLevel equals c.nTSubLevel.ToString()
        //                                      join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && (string.IsNullOrEmpty(w.cDel) || w.cDel == "0")) on g.nTerm equals t.nTerm
        //                                      join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                                      where g.nTermSubLevel2 == null
        //                                      select new { g.sPlaneID, g.nTerm, g.PlanId, c.nTSubLevel, y.numberYear, fullName = ((lang == "eng") ? c.fullNameEN : c.fullName), c.SubLevel, y.nYear, t.sTerm }).Distinct().ToList();

        //            // fetch student previouse year information
        //            if (studentInfo != null && previouseYearGrade != null && previouseYearGrade.Count > 0)
        //            {
        //                foreach (var g in previouseYearGrade)
        //                {
        //                    if (students.Where(w => w.NTerm == g.nTerm).Count() == 0)
        //                    {
        //                        students.Add(new XMLStudentParameter
        //                        {
        //                            SId = studentInfo.SId,
        //                            SName = studentInfo.SName,
        //                            SLastname = studentInfo.SLastname,
        //                            SStudentTitle = studentInfo.SStudentTitle,
        //                            STerm = g.sTerm,
        //                            SStudentID = studentInfo.SStudentID,
        //                            NStudentNumber = studentInfo.NStudentNumber,
        //                            SStudentNameEn = studentInfo.SStudentNameEn,
        //                            SStudentLastEn = studentInfo.SStudentLastEn,
        //                            NStudentStatus = studentInfo.NStudentStatus,
        //                            NTerm = g.nTerm,
        //                            NTSubLevel = g.nTSubLevel,
        //                            SIdentification = studentInfo.SIdentification,
        //                            NumberYear = g.numberYear ?? 0,
        //                            NYear = g.nYear,
        //                            ClassFullName = g.fullName,
        //                            ClassName = g.SubLevel,
        //                        });
        //                    }
        //                }
        //            }

        //            //fetch the plan for students studied in the schools
        //            foreach (var s in students.Where(w => w.NTermSubLevel2 > 0))
        //            {
        //                if (planCourseDTOs.Where(w => w.NTerm == s.NTerm && w.nTermSubLevel2 == s.NTermSubLevel2).Count() == 0)
        //                {
        //                    var planCourse = GetPlanCourses(s.NTSubLevel, s.NTermSubLevel2, s.NTerm, 0, schoolId, dbschool, (lang == "eng") ? false : true);
        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                }
        //            }

        //            //If already plan course fetched avoid to fetch again.
        //            var nTerms = planCourseDTOs.Select(s => s.NTerm);
        //            var nTermSubLevel2s = planCourseDTOs.Select(s => s.nTermSubLevel2);
        //            var studentNotStudiedYear = grade.Where(w => (!nTerms.Any(t => t == w.nTerm) && !nTermSubLevel2s.Any(t => t == w.nTermSubLevel2)) || w.nTermSubLevel2 == null).Select(s => new { s.nTerm, s.nTermSubLevel2, s.PlanId }).Distinct().ToList();

        //            //fetch the plan for students doesn't studied in the schools
        //            foreach (var g in studentNotStudiedYear)
        //            {
        //                if (g.PlanId > 0 && planCourseDTOs.Where(w => w.NTerm == g.nTerm && w.PlanId == g.PlanId).Count() > 0)
        //                {
        //                    continue;
        //                }
        //                else if (planCourseDTOs.Where(w => w.NTerm == g.nTerm && w.nTermSubLevel2 == g.nTermSubLevel2).Count() == 0)
        //                {

        //                    var planIds = grade.Where(w => w.nTerm == g.nTerm).Select(s => s.PlanId).Distinct().OrderByDescending(d => d).ToList();

        //                    //Get Course Details from Plan
        //                    var planCourse = new List<PlanCourseDTO>();
        //                    //GetPlanCourses(student.NTSubLevel, student.NTermSubLevel2, student.NTerm, 0, schoolId, dbschool);
        //                    var planCourseByPlanIds = new List<PlanCourseDTO>();

        //                    if (planIds != null && planIds.Count() > 0)  // If imported data then check filter plan id used to import the old grade
        //                    {
        //                        foreach (var planId in planIds)
        //                        {
        //                            //var anotherPlanId = grade.Where(gr => gr.nTerm == g.nTerm && gr.PlanId > 0).Select(s => s.PlanId).FirstOrDefault();
        //                            if (planId > 0 && planCourseDTOs.Where(w => w.NTerm == g.nTerm && w.PlanId == planId).Count() > 0)
        //                            {
        //                                continue;
        //                            }
        //                            var nTermSubLevel2 = dbschool.TPlanTermSubLevel2.Where(w => w.SchoolID == schoolId && w.PlanId == planId && w.IsActive == true).Select(s => s.nTermSubLevel2).FirstOrDefault();

        //                            var planCourseByRoom = GetPlanCourses(0, nTermSubLevel2, g.nTerm, 0, schoolId, dbschool, (lang == "eng") ? false : true);
        //                            if (planCourseByRoom != null)
        //                            {
        //                                if (planId == 0)
        //                                {
        //                                    planCourseByRoom = (from pc in planCourseByRoom
        //                                                        join g1 in grade.Where(gr => gr.nTerm == g.nTerm && gr.PlanId == planId) on new { pc.NTerm, pc.SPlaneId } equals new { NTerm = g1.nTerm, SPlaneId = g1.sPlaneID }
        //                                                        select pc).ToList();
        //                                }
        //                                else
        //                                {
        //                                    planCourseByRoom = (from pc in planCourseByRoom
        //                                                        join g1 in grade.Where(gr => gr.nTerm == g.nTerm) on new { pc.NTerm, pc.SPlaneId, pc.PlanId } equals new { NTerm = g1.nTerm, SPlaneId = g1.sPlaneID, g1.PlanId }
        //                                                        select pc).ToList();
        //                                }
        //                            }
        //                            if (planCourseByPlanIds.Count > 0)
        //                            {
        //                                planCourseByRoom = planCourseByRoom.Where(w => !planCourseByPlanIds.Any(p => p.SPlaneId == w.SPlaneId)).ToList();
        //                                planCourseByPlanIds.AddRange(planCourseByRoom);
        //                            }
        //                            else
        //                            {

        //                                planCourseByPlanIds = planCourseByRoom;
        //                            }
        //                        }
        //                        if (planCourseByPlanIds != null && planCourseByPlanIds.Count > 0)
        //                        {
        //                            planCourseByPlanIds = (from pc in planCourseByPlanIds
        //                                                   join g1 in grade.Where(gr => gr.nTerm == g.nTerm) on new { pc.NTerm, pc.SPlaneId } equals new { NTerm = g1.nTerm, SPlaneId = g1.sPlaneID }
        //                                                   select pc).ToList();
        //                            planCourse.RemoveAll(w2 => w2.NTerm == g.nTerm && planCourseByPlanIds.Any(c => c.SPlaneId == w2.SPlaneId));
        //                            planCourse.AddRange(planCourseByPlanIds);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        planCourse = GetPlanCourses(0, g.nTermSubLevel2 ?? 0, g.nTerm, 0, schoolId, dbschool, (lang == "eng") ? false : true);
        //                    }

        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.RemoveAll(w2 => planCourse.Any(c => c.SPlaneId == w2.SPlaneId && c.NTerm == w2.NTerm));
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                    else if (planCourseDTOs.Where(w => w.NTerm == g.nTerm).Count() == 0) //If No Plan Get the detail from Tplane table
        //                    {

        //                        var previouseYearCourse = (from gd in previouseYearGrade
        //                                                   join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && w.nTerm == g.nTerm) on gd.nTerm equals t.nTerm
        //                                                   join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                                                   join a in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null) on gd.sPlaneID equals a.sPlaneID
        //                                                   join ct in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals ct.courseTypeId
        //                                                   join cg in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals cg.courseGroupId
        //                                                   orderby a.courseCode
        //                                                   select new PlanCourseDTO
        //                                                   {
        //                                                       NYear = y.nYear,
        //                                                       CourseName = (lang == "eng") ? a.CourseNameEn : a.sPlaneName,
        //                                                       CourseType = a.courseType,
        //                                                       CourseTypeName = (lang == "eng") ? ct.DescriptionEn : ct.Description,
        //                                                       CourseGroup = a.courseGroup,
        //                                                       CourseGroupName = (lang == "eng") ? cg.DescriptionEn : cg.Description,
        //                                                       CourseCode = (lang == "eng") ? a.CourseCodeEn : a.courseCode,
        //                                                       SPlaneId = a.sPlaneID,
        //                                                       NCredit = a.nCredit,
        //                                                       CourseTotalHour = a.courseTotalHour,
        //                                                       CourseStatus = 1,
        //                                                       CourseHour = a.courseHour,
        //                                                       NTSubLevel = a.nTSubLevel,
        //                                                       NTerm = t.nTerm,
        //                                                       //nTermSubLevel2 = student.NTermSubLevel2,
        //                                                       NOrder = ct.nOrder ?? 0
        //                                                   }).ToList();

        //                        //Check plan exist for Class. If plan exist then take the Ncredit and value from Plan.
        //                        if (previouseYearCourse != null && previouseYearCourse.Count > 0)
        //                        {
        //                            var ntSubLevels = previouseYearCourse.Select(s1 => Int32.Parse(s1.NTSubLevel)).Distinct().ToList();
        //                            var plandetails = new List<PlanCourseDTO>();

        //                            //Get Plan Detail for Previouse Year.
        //                            foreach (var c in ntSubLevels)
        //                            {
        //                                var nTermSubLevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c && w.nWorkingStatus == 1 && w.nTermSubLevel2Status == "1").OrderBy(o => o.nTSubLevel2).Select(s => s.nTermSubLevel2).FirstOrDefault();
        //                                var tSubLevel = dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c).FirstOrDefault();
        //                                //student.ClassFullName = tSubLevel.fullName;
        //                                //student.ClassName = tSubLevel.SubLevel;
        //                                planCourse = GetPlanCourses(c, nTermSubLevel2, g.nTerm, 0, schoolId, dbschool, (lang == "eng") ? false : true);
        //                                if (planCourse != null && planCourse.Count > 0)
        //                                {
        //                                    plandetails.AddRange(planCourse);
        //                                }
        //                            }
        //                            foreach (var c in previouseYearCourse)
        //                            {
        //                                var course = plandetails.Where(w => w.SPlaneId == c.SPlaneId && w.NTerm == c.NTerm).FirstOrDefault();
        //                                if (course != null)
        //                                {
        //                                    planCourseDTOs.Add(course);
        //                                }
        //                                else
        //                                {
        //                                    planCourseDTOs.Add(c);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return students;
        //    }


        //}

        ////public static List<int> ValidatePlanCoursesDeActivation(PlanDTO planDTO)
        ////{
        ////    JWTToken token = new JWTToken();
        ////    var userData = new JWTToken().UserData;
        ////    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
        ////    var planIds = planDTO.PlanCourseDTOs.Select(s => s.PlanId).Distinct().ToList();
        ////    var sPlaneIds = planDTO.PlanCourseDTOs.Select(s => s.SPlaneId).Distinct().ToList();
        ////    var planCourseTermDTOs = planDTO.PlanCourseDTOs.SelectMany(s => s.PlanCourseTermDTOs).Distinct().ToList();
        ////    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        ////    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        ////    using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        ////    {
        ////        var planDTOs = (from plan in schoolDbContext.TPlans.Where(w => w.SchoolID == userData.CompanyID)
        ////                        join curr in schoolDbContext.TCurriculums.Where(w => w.SchoolId == userData.CompanyID) on plan.CurriculumId equals curr.CurriculumId
        ////                        where planIds.Contains(plan.PlanId) && plan.IsActive == true && curr.IsActive == true
        ////                        select new PlanDTO
        ////                        {
        ////                            PlanId = plan.PlanId,
        ////                            PlanName = plan.PlanName,
        ////                            NTSubLevel = plan.nTSubLevel,
        ////                            NYear = curr.nYear,
        ////                            EducationSubLevelIds = schoolDbContext.VGetPlanRoomLists.Where(c => c.PlanId == plan.PlanId && c.SchoolID == userData.CompanyID).OrderBy(o => o.SortBy).Select(s => s.nTermSubLevel2).ToList(),

        ////                        }).FirstOrDefault();

        ////        if (planDTO != null && planDTO.EducationSubLevelIds != null && planCourseTermDTOs != null)
        ////        {
        ////            var nTerms = planCourseTermDTOs.Where(w => w.IsActive == true).Select(s => s.NTerm).ToList();

        ////            //Check Exist in Time Table
        ////            var tScheduleCheck = (from s in schoolDbContext.TSchedules
        ////                                  join t in schoolDbContext.TTermTimeTables on s.nTermTable ?? 0 equals t.nTermTable
        ////                                  where sPlaneIds.Contains(s.sPlaneID ?? 0) && s.cDel == null && planDTO.EducationSubLevelIds.Contains(t.nTermSubLevel2 ?? 0) && nTerms.Contains(t.nTerm)
        ////                                  select s.sPlaneID ?? 0).ToList();
        ////            if (tScheduleCheck != null && tScheduleCheck.Any())
        ////            {
        ////                return tScheduleCheck;
        ////            }

        ////            //Check Grade Exist in Grade Main Table
        ////            var tGradeCheck = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == userData.CompanyID && sPlaneIds.Contains(w.sPlaneID ?? 0) && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0))
        ////                               join gd in _dbGrade.TGradeDetails on g.nGradeId equals gd.nGradeId
        ////                               where gd.SchoolID == userData.CompanyID && gd.cDel == false && gd.ScoreData != "" && !string.IsNullOrEmpty(gd.getScore100) && gd.getSpecial == "-1" && ((gd.getScore100 != "0" && gd.getScore100 != "0.00") || (!string.IsNullOrEmpty(gd.scoreMidTerm) && gd.scoreMidTerm != "0") || (!string.IsNullOrEmpty(gd.scoreFinalTerm) && gd.scoreFinalTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreBeforeMidTerm) && gd.ScoreBeforeMidTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreAfterMidTerm) && gd.ScoreAfterMidTerm != "0"))
        ////                               select g.sPlaneID ?? 0).ToList();
        ////            if (tGradeCheck != null && tGradeCheck.Any())
        ////            {
        ////                return tGradeCheck;
        ////            }

        ////            //Check Grade Exist In Grade History Table
        ////            var tGradeCheckHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == userData.CompanyID && nTerms.Contains(w.nTerm) && planDTO.EducationSubLevelIds.Contains(w.nTermSubLevel2 ?? 0) && planIds.Contains(w.sPlaneID ?? 0))
        ////                                      join gd in _dbGradeHistory.TGradeDetailHistories on g.nGradeId equals gd.nGradeId
        ////                                      where gd.SchoolID == userData.CompanyID && gd.cDel == false && gd.ScoreData != "" && !string.IsNullOrEmpty(gd.getScore100) && gd.getSpecial == "-1" && ((gd.getScore100 != "0" && gd.getScore100 != "0.00") || (!string.IsNullOrEmpty(gd.scoreMidTerm) && gd.scoreMidTerm != "0") || (!string.IsNullOrEmpty(gd.scoreFinalTerm) && gd.scoreFinalTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreBeforeMidTerm) && gd.ScoreBeforeMidTerm != "0") || (!string.IsNullOrEmpty(gd.ScoreAfterMidTerm) && gd.ScoreAfterMidTerm != "0"))
        ////                                      select g.sPlaneID ?? 0).ToList();
        ////            if (tGradeCheckHistory != null && tGradeCheckHistory.Any())
        ////            {
        ////                return tGradeCheckHistory;
        ////            }

        ////        }

        ////        return null;
        ////    }
        ////    //return PlanService.ValidatePlanCourseDeActivation(sPlaneId, planId, planCourseTermDTOs, Utils.GetSchoolId(), Utils.GetUserId());
        ////}
        //Report Type 1
        //public static List<XMLStudentParameter> GetStudentInfoForTranscript(int schoolId, int sId, out List<PlanCourseDTO> planCourseDTOs)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
        //    {
        //        List<StudentGrade> studentGrades = new List<StudentGrade>();
        //        planCourseDTOs = new List<PlanCourseDTO>();
        //        //Get Student Information
        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.sID == sId)
        //                        join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                        join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
        //                        join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
        //                        join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            SName = s.sName,
        //                            SLastname = s.sLastname,
        //                            SStudentTitle = s.titleDescription,
        //                            STerm = s.sTerm,
        //                            SStudentID = s.sStudentID,
        //                            NStudentNumber = s.nStudentNumber ?? 0,
        //                            SStudentNameEn = u.sStudentNameEN,
        //                            SStudentLastEn = u.sStudentLastEN,
        //                            NStudentStatus = s.nStudentStatus ?? 0,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            NTSubLevel = s.nTSubLevel,
        //                            SIdentification = u.sIdentification,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NYear = y.nYear,
        //                            ClassFullName = t.fullName,
        //                            ClassName = t.SubLevel,
        //                            RoomName = r.nTSubLevel2
        //                        }).Distinct().AsQueryable().ToList();


        //        if (students != null)
        //        {
        //            var previouseYearGrade = (from g in _dbGrade.TGrades.Where(w => w.SchoolID == schoolId)
        //                                      join gd in _dbGrade.TGradeDetails.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                                      where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                                      select new { g.sPlaneID, g.nTerm }).Distinct().ToList();

        //            var previouseYearGradeHistory = (from g in _dbGradeHistory.TGradeHistories.Where(w => w.SchoolID == schoolId)
        //                                             join gd in _dbGradeHistory.TGradeDetailHistories.Where(w => w.SchoolID == schoolId) on g.nGradeId equals gd.nGradeId
        //                                             where gd.sID == sId && gd.cDel == false && g.nTermSubLevel2 == null
        //                                             select new { g.sPlaneID, g.nTerm }).Distinct().ToList();

        //            if (previouseYearGrade != null && previouseYearGradeHistory != null)
        //            {
        //                previouseYearGrade.AddRange(previouseYearGradeHistory);
        //            }
        //            else if (previouseYearGrade == null && previouseYearGradeHistory != null)
        //            {
        //                previouseYearGrade = previouseYearGradeHistory;
        //            }


        //            //Previous Year Record to fetch the grade
        //            var year = students.Min(m => m.NumberYear);
        //            if (previouseYearGrade != null && previouseYearGrade.Count > 0)
        //            {

        //                year = year - 1;
        //                var tTerm = (from y in dbschool.TYears.Where(w => w.SchoolID == schoolId).ToList()
        //                             join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && (string.IsNullOrEmpty(w.cDel) || w.cDel == "0")).ToList() on y.nYear equals t.nYear
        //                             join py in previouseYearGrade on t.nTerm equals py.nTerm
        //                             join p in dbschool.TPlanes.Where(p => p.SchoolID == schoolId) on py.sPlaneID equals p.sPlaneID
        //                             select new { t.nTerm, t.sTerm, y.numberYear, t.nYear, p.nTSubLevel }).Distinct().ToList();

        //                if (tTerm != null && students != null)
        //                {
        //                    var student = students.FirstOrDefault();
        //                    foreach (var t in tTerm)
        //                    {
        //                        if (students.Where(w => w.NTerm == t.nTerm).Count() == 0)
        //                        {
        //                            students.Add(new XMLStudentParameter
        //                            {
        //                                SId = student.SId,
        //                                SName = student.SName,
        //                                SLastname = student.SLastname,
        //                                SStudentTitle = student.SStudentTitle,
        //                                STerm = t.sTerm,
        //                                SStudentID = student.SStudentID,
        //                                NStudentNumber = student.NStudentNumber,
        //                                SStudentNameEn = student.SStudentNameEn,
        //                                SStudentLastEn = student.SStudentLastEn,
        //                                NStudentStatus = student.NStudentStatus,
        //                                NTerm = t.nTerm,
        //                                //NTermSubLevel2 = s.nTermSubLevel2,
        //                                NTSubLevel = Convert.ToInt32(t.nTSubLevel),
        //                                SIdentification = student.SIdentification,
        //                                NumberYear = t.numberYear ?? 0,
        //                                NYear = t.nYear ?? 0,
        //                                //ClassFullName = t.fullName,
        //                                //ClassName = t.SubLevel,
        //                                //RoomName = r.nTSubLevel2
        //                            });
        //                        }
        //                    }
        //                }
        //            }



        //            foreach (var student in students)
        //            {
        //                if (planCourseDTOs.Where(w => w.NTerm == student.NTerm && w.nTermSubLevel2 == student.NTermSubLevel2).Count() == 0)
        //                {
        //                    //Get Course Details from Plan
        //                    var planCourse = GetPlanCourses(student.NTSubLevel, student.NTermSubLevel2, student.NTerm, 0, schoolId, dbschool);

        //                    if (planCourse != null && planCourse.Count > 0)
        //                    {
        //                        planCourseDTOs.AddRange(planCourse);
        //                    }
        //                    else if (planCourseDTOs.Where(w => w.NTerm == student.NTerm).Count() == 0) //If No Plan Get the detail from Tplane table
        //                    {

        //                        var previouseYearCourse = (from gd in previouseYearGrade
        //                                                   join t in dbschool.TTerms.Where(w => w.SchoolID == schoolId && w.nTerm == student.NTerm) on gd.nTerm equals t.nTerm
        //                                                   join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on t.nYear equals y.nYear
        //                                                   join a in dbschool.TPlanes.Where(w => w.SchoolID == schoolId && w.cDel == null) on gd.sPlaneID equals a.sPlaneID
        //                                                   join ct in dbschool.TCourseTypes.Where(w => w.SchoolID == schoolId) on a.courseType equals ct.courseTypeId
        //                                                   join g in dbschool.TCourseGroups.Where(w => w.SchoolID == schoolId) on a.courseGroup equals g.courseGroupId
        //                                                   orderby a.courseCode
        //                                                   select new PlanCourseDTO
        //                                                   {
        //                                                       NYear = y.nYear,
        //                                                       CourseName = a.sPlaneName,
        //                                                       CourseType = a.courseType,
        //                                                       CourseTypeName = ct.Description,
        //                                                       CourseGroup = a.courseGroup,
        //                                                       CourseGroupName = g.Description,
        //                                                       CourseCode = a.courseCode,
        //                                                       SPlaneId = a.sPlaneID,
        //                                                       NCredit = a.nCredit,
        //                                                       CourseTotalHour = a.courseTotalHour,
        //                                                       CourseStatus = 1,
        //                                                       CourseHour = a.courseHour,
        //                                                       NTSubLevel = a.nTSubLevel,
        //                                                       NTerm = t.nTerm,
        //                                                       //nTermSubLevel2 = student.NTermSubLevel2,
        //                                                       NOrder = ct.nOrder ?? 0
        //                                                   }).ToList();

        //                        //Check plan exist for Class. If plan exist then take the Ncredit and value from Plan.
        //                        if (previouseYearCourse != null && previouseYearCourse.Count > 0)
        //                        {
        //                            var ntSubLevels = previouseYearCourse.Select(s => Int32.Parse(s.NTSubLevel)).Distinct().ToList();
        //                            var plandetails = new List<PlanCourseDTO>();

        //                            //Get Plan Detail for Previouse Year.
        //                            foreach (var c in ntSubLevels)
        //                            {
        //                                var nTermSubLevel2 = dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c && w.nWorkingStatus == 1 && w.nTermSubLevel2Status == "1").OrderBy(o => o.nTSubLevel2).Select(s => s.nTermSubLevel2).FirstOrDefault();
        //                                var tSubLevel = dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == c).FirstOrDefault();
        //                                student.ClassFullName = tSubLevel.fullName;
        //                                student.ClassName = tSubLevel.SubLevel;
        //                                planCourse = GetPlanCourses(c, nTermSubLevel2, student.NTerm, 0, schoolId, dbschool);
        //                                if (planCourse != null && planCourse.Count > 0)
        //                                {
        //                                    plandetails.AddRange(planCourse);
        //                                }
        //                            }
        //                            foreach (var c in previouseYearCourse)
        //                            {
        //                                var course = plandetails.Where(w => w.SPlaneId == c.SPlaneId && w.NTerm == c.NTerm).FirstOrDefault();
        //                                if (course != null)
        //                                {
        //                                    planCourseDTOs.Add(course);
        //                                }
        //                                else
        //                                {
        //                                    planCourseDTOs.Add(c);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        return students;
        //    }
        //}

        public static List<XMLStudentParameter> GetXMLStudentParameter(int schoolId, int nTermSubLevel2)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<XMLStudentParameter> students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTermSubLevel2 == nTermSubLevel2 && ((w.cDel ?? "0") != "1"))
                                                      join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
                                                      join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
                                                      join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
                                                      join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTermSubLevel2 == nTermSubLevel2) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
                                                      select new XMLStudentParameter
                                                      {
                                                          SId = s.sID,
                                                          SName = s.sName,
                                                          SLastname = s.sLastname,
                                                          SStudentTitle = s.titleDescription,
                                                          STerm = s.sTerm,
                                                          SStudentID = s.sStudentID,
                                                          NStudentNumber = s.nStudentNumber ?? 0,
                                                          SStudentNameEn = u.sStudentNameEN,
                                                          SStudentLastEn = u.sStudentLastEN,
                                                          NStudentStatus = s.nStudentStatus ?? 0,
                                                          NTerm = s.nTerm,
                                                          NTermSubLevel2 = s.nTermSubLevel2,
                                                          NTSubLevel = s.nTSubLevel,
                                                          SIdentification = u.sIdentification,
                                                          NumberYear = s.numberYear ?? 0,
                                                          NYear = y.nYear,
                                                          ClassFullName = t.fullName,
                                                          ClassName = t.SubLevel,
                                                          RoomName = r.nTSubLevel2
                                                      }).Distinct().AsQueryable().ToList();

                return students;
            }
        }

        public static List<XMLStudentParameter> GetXMLStudentParameter(int schoolId, string nTerm, int nTermSubLevel2, List<XMLStudentParameter> xMLStudents)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<XMLStudentParameter> students = new List<XMLStudentParameter>();
                if (xMLStudents == null)
                {
                    students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && ((w.cDel ?? "0") != "1"))
                                join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
                                join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
                                join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
                                join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTermSubLevel2 == nTermSubLevel2) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
                                select new XMLStudentParameter
                                {
                                    SId = s.sID,
                                    SName = s.sName,
                                    SLastname = s.sLastname,
                                    SStudentTitle = s.titleDescription,
                                    STerm = s.sTerm,
                                    SStudentID = s.sStudentID,
                                    NStudentNumber = s.nStudentNumber ?? 9999,
                                    SStudentNameEn = u.sStudentNameEN,
                                    SStudentLastEn = u.sStudentLastEN,
                                    NStudentStatus = s.nStudentStatus ?? 0,
                                    NTerm = s.nTerm,
                                    NTermSubLevel2 = s.nTermSubLevel2,
                                    NTSubLevel = s.nTSubLevel,
                                    SIdentification = u.sIdentification,
                                    NumberYear = s.numberYear ?? 0,
                                    NYear = y.nYear,
                                    ClassFullName = t.fullName,
                                    ClassName = t.SubLevel,
                                    RoomName = r.nTSubLevel2
                                }).Distinct().AsQueryable().ToList();
                }
                else if (xMLStudents != null)
                {

                    students = (from indstudent in xMLStudents
                                join s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTermSubLevel2 == nTermSubLevel2 && ((w.cDel ?? "0") != "1")) on indstudent.SId equals s.sID
                                join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
                                join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
                                join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
                                join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTermSubLevel2 == nTermSubLevel2) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }

                                select new XMLStudentParameter
                                {
                                    SId = s.sID,
                                    SName = s.sName,
                                    SLastname = s.sLastname,
                                    SStudentTitle = s.titleDescription,
                                    STerm = s.sTerm,
                                    SStudentID = s.sStudentID,
                                    NStudentNumber = s.nStudentNumber ?? 9999,
                                    SStudentNameEn = u.sStudentNameEN,
                                    SStudentLastEn = u.sStudentLastEN,
                                    NStudentStatus = s.nStudentStatus ?? 0,
                                    NTerm = s.nTerm,
                                    NTermSubLevel2 = s.nTermSubLevel2,
                                    NTSubLevel = s.nTSubLevel,
                                    SIdentification = u.sIdentification,
                                    NumberYear = s.numberYear ?? 0,
                                    NYear = y.nYear,
                                    ClassFullName = t.fullName,
                                    ClassName = t.SubLevel,
                                    RoomName = r.nTSubLevel2
                                }).Distinct().AsQueryable().ToList();

                }
                return students;
            }
        }

        public static List<XMLStudentParameter> GetXMLStudentParameterBynTSubLevel(int schoolId, string nTerm, int nTSubLevel)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<XMLStudentParameter> students = new List<XMLStudentParameter>();
                students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTerm == nTerm && w.nTSubLevel == nTSubLevel && ((w.cDel ?? "0") != "1"))
                            join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
                            join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
                            join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel) on s.nTSubLevel equals t.nTSubLevel
                            join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nWorkingStatus == 1 && w.cDel == false) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
                            select new XMLStudentParameter
                            {
                                SId = s.sID,
                                SName = s.sName,
                                SLastname = s.sLastname,
                                SStudentTitle = s.titleDescription,
                                STerm = s.sTerm,
                                SStudentID = s.sStudentID,
                                NStudentNumber = s.nStudentNumber ?? 9999,
                                SStudentNameEn = u.sStudentNameEN,
                                SStudentLastEn = u.sStudentLastEN,
                                NStudentStatus = s.nStudentStatus ?? 0,
                                NTerm = s.nTerm,
                                NTermSubLevel2 = s.nTermSubLevel2,
                                NTSubLevel = s.nTSubLevel,
                                SIdentification = u.sIdentification,
                                NumberYear = s.numberYear ?? 0,
                                NYear = y.nYear,
                                ClassFullName = t.fullName,
                                ClassName = t.SubLevel,
                                RoomName = r.nTSubLevel2
                            }).Distinct().AsQueryable().ToList();
                return students;
            }
        }

        public static List<XMLStudentParameter> GetXMLStudentParameter(int schoolId, string nTerm, List<int> sIds, bool thaiLanguage = true)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                List<XMLStudentParameter> students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && sIds.Contains(w.sID) && w.nTerm == nTerm && ((w.cDel ?? "0") != "1"))
                                                      join u in dbschool.TUser.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
                                                      join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
                                                      join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
                                                      join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
                                                      select new XMLStudentParameter
                                                      {
                                                          SId = s.sID,
                                                          SName = (thaiLanguage) ? s.sName : u.sStudentNameEN,
                                                          SLastname = (thaiLanguage) ? s.sLastname : u.sStudentLastEN,
                                                          SStudentTitle = (thaiLanguage) ? s.titleDescription : s.titleDescriptionEn,
                                                          STerm = s.sTerm,
                                                          SStudentID = s.sStudentID,
                                                          NStudentNumber = s.nStudentNumber ?? 0,
                                                          SStudentNameEn = u.sStudentNameEN,
                                                          SStudentLastEn = u.sStudentLastEN,
                                                          NStudentStatus = s.nStudentStatus ?? 0,
                                                          NTerm = s.nTerm,
                                                          NTermSubLevel2 = s.nTermSubLevel2,
                                                          NTSubLevel = s.nTSubLevel,
                                                          SIdentification = u.sIdentification,
                                                          NumberYear = s.numberYear ?? 0,
                                                          NYear = y.nYear,
                                                          ClassFullName = (thaiLanguage) ? t.fullName : t.fullNameEN,
                                                          ClassName = (thaiLanguage) ? t.SubLevel : t.SubLevelEN,
                                                          RoomName = (thaiLanguage) ? r.nTSubLevel2 : r.nTSubLevel2
                                                      }).Distinct().ToList();

                return students;
            }
        }

        public static List<XMLCourseParameter> GetXMLCourseParameter(List<PlanCourseDTO> planCourseDTOs)
        {
            if (planCourseDTOs != null)
            {
                return planCourseDTOs.Select(s => new XMLCourseParameter
                {
                    CourseCode = s.CourseCode,
                    CourseGroup = s.CourseGroup,
                    CourseGroupName = s.CourseGroupName,
                    CourseHour = s.CourseHour,
                    CourseStatus = s.CourseStatus,
                    CourseTotalHour = s.CourseTotalHour,
                    CourseType = s.CourseType,
                    CourseTypeName = s.CourseTypeName,
                    NCredit = s.NCredit,
                    NOrder = s.NOrder,
                    NTerm = s.NTerm,
                    SPlaneID = s.SPlaneId,
                    SPlaneName = s.CourseName,
                    NTermSubLevel2 = s.nTermSubLevel2,
                    CourseGroupNameEng = s.CourseGroupNameEng,
                    CourseTypeNameEng = s.CourseTypeNameEng
                }).ToList();
            }
            else
            {
                return new List<XMLCourseParameter>();
            }
        }




        public static void CreatePlanFromPage(int ntSubLevel, int nTermSubLevel2, string nTerm, int nYear, int schoolId, int sEmp, int numberYear)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {

                var dateTime = DateTime.Now;
                var getCurriculam = GetCurriculumByYear(nYear, schoolId);

                var isCurriculamExist = false;
                var isPlanExist = false;
                var curriculamId = 0;
                var planId = 0;

                if (getCurriculam == null || (getCurriculam != null && getCurriculam.Count > 0))
                {
                    isCurriculamExist = true;
                }

                if (getCurriculam != null && getCurriculam.Count > 0 && getCurriculam.Sum(s => s.PlanCount) > 0)
                {
                    var getPlan = (from p in dbschool.TPlans.Where(w => w.SchoolID == schoolId && w.nTSubLevel == ntSubLevel && w.cDel == false)
                                   join c in dbschool.TCurriculums.Where(w => w.SchoolId == schoolId && w.nYear == nYear) on p.CurriculumId equals c.CurriculumId
                                   select p).FirstOrDefault();
                    if (getPlan != null)
                    {
                        isPlanExist = true;
                        if (getPlan.IsActive == false)
                        {
                            getPlan.IsActive = true;
                            dbschool.SaveChanges();
                        }
                    }
                    else
                    {
                        curriculamId = getCurriculam.FirstOrDefault().CurriculumId;
                    }
                }
                else if (getCurriculam != null && getCurriculam.Count > 0 && getCurriculam.Sum(s => s.PlanCount) == 0)
                {
                    curriculamId = getCurriculam.FirstOrDefault().CurriculumId;
                }

                if (!isCurriculamExist)
                {
                    CurriculumDTO curriculum = new CurriculumDTO { };
                    curriculum.NYear = nYear;
                    curriculum.CurriculumName = String.Format("{0} หลักสูตร", numberYear);

                    curriculum.UpdatedBy = sEmp;
                    curriculum.CreatedBy = sEmp;

                    curriculum.CreatedDate = dateTime;
                    curriculum.UpdatedDate = dateTime;
                    curriculum.SchoolId = Utils.GetSchoolId();

                    var curriculam = new TCurriculum
                    {
                        cDel = false,
                        IsActive = true,
                        CreatedBy = sEmp,
                        CreatedDate = dateTime,
                        UpdatedBy = sEmp,
                        UpdatedDate = dateTime,
                        SchoolId = schoolId,
                        CurriculumName = String.Format("{0} หลักสูตร", numberYear),
                        nYear = nYear
                    };
                    dbschool.TCurriculums.Add(curriculam);
                    dbschool.SaveChanges();
                    curriculamId = curriculam.CurriculumId;
                }
                if (curriculamId > 0 && isPlanExist == false)
                {
                    var tPlan = new TPlan();
                    var subLevel = dbschool.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == ntSubLevel && w.nWorkingStatus == 1).FirstOrDefault();

                    tPlan.PlanName = string.Format("{0}-แผนคู่มือ", subLevel.SubLevel);
                    tPlan.CurriculumId = curriculamId;
                    tPlan.UpdatedBy = sEmp;
                    tPlan.UpdatedDate = dateTime;

                    tPlan.nTSubLevel = ntSubLevel;
                    tPlan.CreatedBy = sEmp;
                    tPlan.CreatedDate = dateTime;
                    tPlan.SchoolID = schoolId;
                    tPlan.IsActive = true;
                    dbschool.TPlans.Add(tPlan);
                    dbschool.SaveChanges();
                    planId = tPlan.PlanId;

                    var nTermSubLevel = (dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == ntSubLevel && w.cDel == false && w.nWorkingStatus == 1).ToList()
                        .Select(s => new TPlanTermSubLevel2
                        {
                            cDel = false,
                            CreatedBy = sEmp,
                            CreatedDate = dateTime,
                            IsActive = true,
                            nTermSubLevel2 = s.nTermSubLevel2,
                            PlanId = planId,
                            SchoolID = schoolId,
                            UpdatedBy = sEmp,
                            UpdatedDate = dateTime
                        }
                        )).ToList();

                    dbschool.TPlanTermSubLevel2.AddRange(nTermSubLevel);
                    dbschool.SaveChanges();

                    var courseDetail = GetCoursesForCreatePlan(ntSubLevel, nYear);
                    if (courseDetail != null && courseDetail.Count > 0)
                    {
                        var planCourses = new List<TPlanCourse>();
                        var planCourse = new TPlanCourse();
                        foreach (var c in courseDetail)
                        {
                            planCourse = new TPlanCourse()
                            {
                                PlanId = planId,
                                sPlaneID = c.SPlaneId,
                                SchoolID = schoolId,
                                CourseStatus = c.CourseStatus,
                                cDel = false,
                                CourseHour = c.CourseHour,
                                CourseTotalHour = c.CourseTotalHour,
                                CreatedBy = sEmp,
                                CreatedDate = dateTime,
                                nCredit = c.NCredit,
                                IsActive = true,
                                UpdatedBy = sEmp,
                                UpdatedDate = dateTime,
                                IsUserAllowedToEditRatio = false,

                            };
                            dbschool.TPlanCourses.Add(planCourse);
                            dbschool.SaveChanges();
                            var planCourseId = planCourse.PlanCourseId;
                            if (planCourseId > 0 && c.PlanCourseTermDTOs != null)
                            {
                                var planCourseterms = new List<TPlanCourseTerm>();
                                var planCourseTerm = new TPlanCourseTerm();
                                foreach (var t in c.PlanCourseTermDTOs)
                                {
                                    var tTerm = dbschool.TPlanCourseTerms.Where(w => w.SchoolID == schoolId && w.PlanCourseId == planCourseId && w.nTerm == t.NTerm).FirstOrDefault();
                                    if (tTerm == null)
                                    {
                                        dbschool.TPlanCourseTerms.Add(new TPlanCourseTerm
                                        {
                                            nTerm = t.NTerm,
                                            IsActive = true,
                                            CreatedBy = sEmp,
                                            CreatedDate = dateTime,
                                            PlanCourseId = planCourseId,
                                            SchoolID = schoolId,
                                            UpdatedBy = sEmp,
                                            UpdatedDate = dateTime
                                        });
                                        dbschool.SaveChanges();
                                    }
                                    else
                                    {
                                        tTerm.IsActive = true;
                                        dbschool.SaveChanges();
                                    }

                                }
                            }
                        }
                    }
                }



            }
        }

        //public static string GetAllStudentAndSubjectInfoByClassAndTerm(int nTSubLevel,string nTerm, int schoolId, int sPlaneId)
        //{
        //    using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    {
        //        List<StudentGrade> studentGrades = new List<StudentGrade>();
        //        var planCourseDTOs = new List<PlanCourseDTO>();
        //        //Get Student Information
        //        var students = (from s in dbschool.TB_StudentViews.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel && w.nTerm == nTerm)
        //                        join u in dbschool.TUsers.Where(w => w.SchoolID == schoolId && (w.cType ?? "0") == "0") on s.sID equals u.sID
        //                        join y in dbschool.TYears.Where(w => w.SchoolID == schoolId) on s.numberYear equals y.numberYear
        //                        join t in dbschool.TSubLevels.Where(w => w.SchoolID == schoolId) on s.nTSubLevel equals t.nTSubLevel
        //                        join r in dbschool.TTermSubLevel2.Where(w => w.SchoolID == schoolId) on new { s.nTermSubLevel2, s.nTSubLevel } equals new { r.nTermSubLevel2, r.nTSubLevel }
        //                        select new XMLStudentParameter
        //                        {
        //                            SId = s.sID,
        //                            SName = s.sName,
        //                            SLastname = s.sLastname,
        //                            SStudentTitle = s.titleDescription,
        //                            STerm = s.sTerm,
        //                            SStudentID = s.sStudentID,
        //                            NStudentNumber = s.nStudentNumber ?? 0,
        //                            SStudentNameEn = u.sStudentNameEN,
        //                            SStudentLastEn = u.sStudentLastEN,
        //                            NStudentStatus = s.nStudentStatus ?? 0,
        //                            NTerm = s.nTerm,
        //                            NTermSubLevel2 = s.nTermSubLevel2,
        //                            NTSubLevel = s.nTSubLevel,
        //                            SIdentification = u.sIdentification,
        //                            NumberYear = s.numberYear ?? 0,
        //                            NYear = y.nYear,
        //                            ClassFullName = t.fullName,
        //                            ClassName = t.SubLevel,
        //                            RoomName = r.nTSubLevel2
        //                        }).Distinct().ToList();


        //        foreach (var t in students)
        //        {
        //            if (planCourseDTOs.Where(w => w.NTerm == t.NTerm && w.nTermSubLevel2 == t.NTermSubLevel2).Count() == 0)
        //            {
        //                var planCourse = GetPlanCourses(nTSubLevel, t.NTermSubLevel2, t.NTerm, 0, schoolId, dbschool);
        //                if (planCourse != null && planCourse.Count > 0)
        //                {
        //                    planCourse = planCourse.Where(w => w.SPlaneId == sPlaneId).ToList();
        //                    planCourseDTOs.AddRange(planCourse);
        //                }
        //            }

        //        }

        //        var xmlStudentList = Common.GetXMLFromObject(students);
        //        var planCourses = GetXMLCourseParameter(planCourseDTOs);
        //        var xmlCourseList = Common.GetXMLFromObject(planCourses);
        //        return string.Empty;
        //    }
        //}

        //public static bool CheckConnection()
        //{
        //    try
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            _dbGrade.Database.Connection.Open();
        //            _dbGrade.Database.Connection.Close();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        try
        //        {
        //            System.Diagnostics.EventLog.WriteEntry("CheckDBConnection", ex.StackTrace,
        //                                   System.Diagnostics.EventLogEntryType.Warning);
        //        }
        //        catch (Exception ex1)
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //    //return true;
        //}

        public static List<GradeLogViewModel> GetGradeLog(GradeLogRequest request)
        {
            using (JabJaiEntities dbschool = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                var terms = (from t in dbschool.TTerms.Where(w => w.SchoolID == request.SchoolId && w.sTerm == request.sTerm && w.cDel == null)
                             join y in dbschool.TYears.Where(w => w.SchoolID == request.SchoolId && w.numberYear == request.NumberYear) on t.nYear equals y.nYear
                             select t).FirstOrDefault();


                if (SchoolBright.DataAccess.DataAccessHelper.bUsePGDBForLogs)
                {
                    List<string> searchOption = new List<string>();
                    searchOption.Add(request.RoomInfo.Replace("'", "''"));

                    DateTime _dtStartDate = (DateTime)terms.dStart;
                    DateTime _dtEndDate = DateTime.Now;


                    using (var PGJabjaiLogs = new PostgreSQLLogs.JabjaiSchoolLogsContainer())
                    {

                        string sql = string.Format("select * from public.{0}TSystemlog{1} where {0}SchoolID{1}= {2} and {0}dLog{1} between '{4}' and '{5}' and {0}sLog{1} like '%{3}%'  ", '"', '"', request.SchoolId, request.RoomInfo.Replace("'", "''"), _dtStartDate.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        List<PostgreSQLLogs.TSystemlog> _SystemLog = PGJabjaiLogs.Database.SqlQuery<PostgreSQLLogs.TSystemlog>(sql).ToList();


                        List<TEmployee> _TEmployee = dbschool.TEmployees.Where(x => x.SchoolID == request.SchoolId).ToList();

                        var query = (from l in _SystemLog
                                     join e in _TEmployee on l.sEmp equals e.sEmp into le
                                     from e in le.DefaultIfEmpty()

                                     select new
                                     {
                                         l.dLog,
                                         sLog = l.sLog.Replace(request.SchoolId.ToString(), ""),
                                         TeacherName = e.sEmp == null ? "สคูลไบร์ท แอดมิน" : e.sName + " " + e.sLastname,
                                     }).ToList();

                        List<GradeLogViewModel> _GradeLogViewModel = (from l in query
                                                                      select new
                                                                      {
                                                                          l.dLog,
                                                                          LogDetails = l.sLog,
                                                                          l.TeacherName

                                                                      }).AsEnumerable().Select(x => new GradeLogViewModel
                                                                      {
                                                                          LogDate = x.dLog,
                                                                          LogDetails = x.LogDetails,
                                                                          TeacherName = x.TeacherName

                                                                      }).OrderByDescending(a => a.LogDate).ToList();


                        return _GradeLogViewModel;

                    }


                }
                else
                {
                    //When we login using admin site then userid is zero.
                    //If request.userid = 0 then 'สคูลไบร์ท แอดมิน' TEmployee table doesn't value. 
                    var sqlQuery = string.Format("select dLog LogDate, sLog LogDetails, TeacherName from (select dLog, Replace(sLog,'{0}','') sLog, CASE WHEN E.sEmp IS NULL THEN 'สคูลไบร์ท แอดมิน' ELSE  CONCAT(E.sName, ' ', E.sLastname) END TeacherName from TSystemlog S " +
                            " LEFT JOIN TEmployees E ON S.sEmp = E.sEmp " +
                            " where S.SchoolID = {0} and dLog between '{1}' and '{2}' and sLog like '%{3}%' " +
                            " UNION " +
                            "select dLog,Replace(sLog, '{0}', '') sLog, CASE WHEN E.sEmp IS NULL THEN 'สคูลไบร์ท แอดมิน' ELSE  CONCAT(E.sName, ' ', E.sLastname) END TeacherName from JabjaiSchoolHistory.dbo.TSystemlog S" +
                            " LEFT JOIN TEmployees E ON S.sEmp = E.sEmp" +
                            " where S.SchoolID = {0} and dLog between '{1}' and '{2}' and sLog like '%{3}%') A" +
                            " order by dLog desc", request.SchoolId, terms.dStart, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), request.RoomInfo.Replace("'", "''"));
                    return dbschool.Database.SqlQuery<GradeLogViewModel>(sqlQuery).ToList();
                }
            }
        }

        public static List<PlanCourseDTO> GetPlanCoursesBySId(List<PlanCourseDTO> planCourseDTOs, int schoolID, int sid)
        {
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                if (planCourseDTOs != null && planCourseDTOs.Count() > 0)
                {
                    var filteredPlanCourseIds = en.TPlanCourseStudents.Where(pcs => pcs.SchoolID == schoolID && pcs.sID == sid && pcs.IsActive)
                               .Select(pcs => pcs.PlanCourseId).ToList();


                    var allPlanCourseIds = planCourseDTOs.Select(s => s.PlanCourseId).ToList();

                    var planCourseSettingDone = en.TPlanCourseStudents
                        .Where(pcs => pcs.SchoolID == schoolID && pcs.IsActive && allPlanCourseIds.Contains(pcs.PlanCourseId))
                        .Select(s => s.PlanCourseId)
                        .ToHashSet();

                    planCourseDTOs = planCourseDTOs
                        .Where(pc => filteredPlanCourseIds.Contains(pc.PlanCourseId) || !planCourseSettingDone.Contains(pc.PlanCourseId))
                        .ToList();
                }

                return planCourseDTOs;
            }
        }
        #endregion

        #region "Common"

        public static List<StudyLevelInfo> GetStudyLevelInfo(int numberYear, int nTSublevel, string subLevel)
        {
            try
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
                List<StudyLevelInfo> currentRange = new List<StudyLevelInfo>();
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

                List<StudyLevelInfo> rangeHighVocationCertificate = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "ปวส.1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ปวส.2", Index = 1 },
                    new StudyLevelInfo { SubLevel = "ปวส.พ.1", Index = 2 },
                    new StudyLevelInfo { SubLevel = "ปวส.พ.2", Index = 3 },
                   };

                List<StudyLevelInfo> rangeVocationCertificate = new List<StudyLevelInfo> {
                    new StudyLevelInfo { SubLevel = "ปวช.1", Index = 0 },
                    new StudyLevelInfo { SubLevel = "ปวช.2", Index = 1 },
                    new StudyLevelInfo { SubLevel = "ปวช.3", Index = 2 },
                   };

                if (rangePrimary.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangePrimary;


                }
                else if (rangePrimaryEng.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangePrimaryEng;
                }
                else if (rangeMathayomL1.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeMathayomL1;
                }
                else if (rangeMathayomL1Eng.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeMathayomL1Eng;
                }
                else if (rangeMathayomL2.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeMathayomL2;
                }
                else if (rangeMathayomL2Eng.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeMathayomL2Eng;
                }
                else if (rangeHighVocationCertificate.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeHighVocationCertificate;
                }
                else if (rangeVocationCertificate.Where(w => w.SubLevel == subLevel.Replace(" ", "")).Count() > 0)
                {
                    currentRange = rangeVocationCertificate;
                }

                currentRange.Where(w => w.SubLevel == subLevel.Replace(" ", "")).ToList().ForEach(f => f.NumberYear = numberYear);
                var index = currentRange.FindIndex(w => w.SubLevel == subLevel.Replace(" ", ""));
                currentRange = currentRange.Take(index + 1).ToList();
                var currentYear = numberYear;
                foreach (var r in currentRange.OrderByDescending(o => o.Index))
                {
                    r.NumberYear = currentYear;
                    currentYear--;
                }


                //var roomInfo = CommonService.GetStudyLevelInfo(numberYear, userData.CompanyID, nTSublevel, currentRange);
                return currentRange;
            }
            catch (Exception ex)
            {
                //TO DO: Capture and Throw Custom Exception
                return null;
            }
        }


        public static int GetEnglishYear(int thaiYear)
        {
            ThaiBuddhistCalendar cal = new ThaiBuddhistCalendar();
            return cal.ToDateTime(thaiYear, 2, 1, 0, 0, 0, 0).Year;
        }

        public static bool CheckIsPrimary(int nTSubLevel, int schoolId)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var levelName = (from s in _db.TSubLevels.Where(w => w.SchoolID == schoolId && w.nTSubLevel == nTSubLevel)
                                 join l in _db.TLevels on s.nTLevel equals l.LevelID
                                 select l.LevelName).FirstOrDefault();

                var isPrimary = false;
                if (!string.IsNullOrEmpty(levelName) && levelName != "ปวช." && levelName != "ปวส." && levelName != "มัธยมศึกษา")
                {
                    isPrimary = true;
                }
                return isPrimary;
            }
        }
        #endregion

        #region "GradeSetting"
        public static void UpdateSettingExtraTime(SettingExtraTimeRequest request, int userId)
        {
            using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                DateTime now = DateTime.Now;
                var tSettingExtraTimes = _db.TSettingExtraTimes.Where(w => w.SchoolID == request.SchoolID && w.nTerm == request.nTerm && w.nTermSubLevel2 == request.nTermSubLevel2 && w.sEMP == request.sEMP && w.sPlaneID == request.sPlaneID && w.cDel == null && w.useToken == null && w.addDate.Day == now.Day && w.addDate.Month == now.Month).FirstOrDefault();

                //int countID = _db.TSettingExtraTimes.Count() == 0 ? 0 : _db.TSettingExtraTimes.Max(max => max.nSettingExtraTime);
                //countID = countID + 1;
                //int sPlaneId = 0;
                //int.TryParse(planlist.SelectedValue, out sPlaneId);
                TSettingExtraTime extra = new TSettingExtraTime();
                extra.addDate = DateTime.Now;
                //extra.nSettingExtraTime = countID;
                extra.nTerm = request.nTerm;
                extra.nTermSubLevel2 = request.nTermSubLevel2;
                extra.sEMP = request.sEMP;
                extra.SchoolID = request.SchoolID;
                extra.sPlaneID = request.sPlaneID;
                extra.UpdatedBy = userId;
                extra.UpdatedDate = DateTime.Now;

                if (tSettingExtraTimes == null)
                {
                    extra.CreatedDate = DateTime.Now;
                    extra.CreatedBy = userId;
                    _db.TSettingExtraTimes.Add(extra);
                }

                _db.SaveChanges();
            }
        }


        public static string GetGradeAverage(double gba)
        {
            string gradeAverage = "0.000";
            bool checkisNaN = Double.IsNaN(gba);
            if (checkisNaN)
            {
                gradeAverage = gradeAverage.Remove(gradeAverage.Length - 1);
            }
            else
            {

                //gradeAverage = gba.ToString("0.000");
                //gradeAverage = gradeAverage.Remove(gradeAverage.Length - 1);
                gradeAverage = Truncate(gba, 2).ToString("0.00");
            }
            return gradeAverage;
        }

        public static double Truncate(double d, byte decimals)
        {
            double r = Math.Round(d, decimals);

            if (d > 0 && r > d)
            {
                return r - (double)(new decimal(1, 0, 0, false, decimals));
            }
            else if (d < 0 && r < d)
            {
                return r + (double)(new decimal(1, 0, 0, false, decimals));
            }

            return r;
        }
        #endregion


        #region SQDetailsFunctions

        public static string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
        public static List<T> ConvertDataTableToList<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (pro.PropertyType.Name == "String")
                        {
                            try
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            catch
                            {
                                pro.SetValue(obj, "", null);
                            }
                        }
                        else
                        {
                            try
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                            }
                            catch
                            {
                                //pro.SetValue(obj, "", null);
                            }
                        }
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        //public static GetGradeDetailView_Result GetSQGradeDetailViewBySid(int SchoolID, int nGradeID, int sID)
        //{
        //    List<GetGradeDetailView_Result> GetGradeDetailView_Result = new List<GetGradeDetailView_Result>();
        //    GetGradeDetailView_Result GetGradeDetailView_ResultObj = null;

        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    {
        //        using (var pgLiteDB = new PGGradeDBEntities())
        //        {
        //            try
        //            {


        //                List<PGTGradeDetail> _PGTGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == SchoolID && x.nGradeId == nGradeID && x.sID == sID).ToList();
        //                var TGradeDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetails);
        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;



        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetGradeDetailView";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@SchoolId", SchoolID));
        //                        cmd.Parameters.Add(new SqlParameter("@nGradeId", nGradeID));
        //                        cmd.Parameters.Add(new SqlParameter("@sID", sID));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeList));


        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        GetGradeDetailView_Result = ServiceHelper.ConvertDataTableToList<GetGradeDetailView_Result>(dt);


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // return new GetGradeDetailView_Result();

        //                }
        //                finally
        //                {
        //                    //if (connectionState != ConnectionState.Closed) conn.Close();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //return new GetGradeDetailView_Result();
        //            }
        //        }
        //    }

        //    if (GetGradeDetailView_Result.Count > 0)
        //        return GetGradeDetailView_Result.FirstOrDefault();
        //    else
        //        return GetGradeDetailView_ResultObj;


        //}

        //public static List<GetGradeDetailView_Result> GetSQGradeDetailView(int SchoolID, int nGradeID, int sID)
        //{
        //    List<GetGradeDetailView_Result> GetGradeDetailView_Result = new List<GetGradeDetailView_Result>();
        //    using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //    {
        //        using (var pgLiteDB = new PGGradeDBEntities())
        //        {
        //            try
        //            {


        //                List<PGTGradeDetail> _PGTGradeDetails = new List<PGTGradeDetail>();

        //                if (sID > 0)
        //                    _PGTGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == SchoolID && x.nGradeId == nGradeID && x.sID == sID).ToList();
        //                else
        //                    _PGTGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == SchoolID && x.nGradeId == nGradeID).ToList();


        //                var TGradeDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetails);
        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;



        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetGradeDetailView";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@SchoolId", SchoolID));
        //                        cmd.Parameters.Add(new SqlParameter("@nGradeId", nGradeID));
        //                        cmd.Parameters.Add(new SqlParameter("@sID", sID));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeList));


        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        GetGradeDetailView_Result = ServiceHelper.ConvertDataTableToList<GetGradeDetailView_Result>(dt);


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // return new GetGradeDetailView_Result();

        //                }
        //                finally
        //                {
        //                    //if (connectionState != ConnectionState.Closed) conn.Close();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                //return new GetGradeDetailView_Result();
        //            }
        //        }
        //    }

        //    return GetGradeDetailView_Result;

        //}

        //public static TB_GradeViews GetSQGradeViewsByID(int SchoolID, string nTerm, int sPlaneID, int? nTermSubLevel2)
        //{

        //    List<TB_GradeViews> tB_GradeViews = new List<TB_GradeViews>();

        //    TB_GradeViews tB_GradeView = null;

        //    using (var pgLiteDB = new PGGradeDBEntities())
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            try
        //            {

        //                List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                List<PGTAssessment> _PGTAssessmentList = new List<PGTAssessment>();


        //                _PGTGradeList = pgLiteDB.PGTGrades.Where(x => x.SchoolID == SchoolID && x.nTerm == nTerm && x.sPlaneID == sPlaneID && (x.nTermSubLevel2 == 0 || x.nTermSubLevel2 == nTermSubLevel2)).ToList();
        //                _PGTAssessmentList = pgLiteDB.PGTAssessments.Where(x => x.SchoolId == SchoolID && x.nTerm == nTerm && x.sPlaneID == sPlaneID && (x.nTermSubLevel2 == 0 || x.nTermSubLevel2 == nTermSubLevel2)).ToList();


        //                var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                var TAssessementDatas = ServiceHelper.GetXMLTAssessmentParameter(_PGTAssessmentList);
        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var xmlTAssessementList = Common.GetXMLFromObject(TAssessementDatas);

        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;


        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetGradeView";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTAssessmentList", xmlTAssessementList));



        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        tB_GradeViews = ServiceHelper.ConvertDataTableToList<TB_GradeViews>(dt);


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // return new GetGradeDetailView_Result();

        //                }
        //                finally
        //                {
        //                    //if (connectionState != ConnectionState.Closed) conn.Close();
        //                }
        //            }
        //            catch
        //            {

        //            }
        //        }
        //    }

        //    if (tB_GradeViews.Count > 0)
        //        return tB_GradeViews.FirstOrDefault();
        //    else
        //        return tB_GradeView;

        //}

        //public static List<TB_GradeViews> GetSQGradeViews(int SchoolID, string nTerm, int sPlaneID)
        //{

        //    List<TB_GradeViews> tB_GradeViews = new List<TB_GradeViews>();



        //    using (var pgLiteDB = new PGGradeDBEntities())
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            try
        //            {

        //                List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                List<PGTAssessment> _PGTAssessmentList = new List<PGTAssessment>();


        //                _PGTGradeList = pgLiteDB.PGTGrades.Where(x => x.SchoolID == SchoolID && x.nTerm == nTerm && x.sPlaneID == sPlaneID).ToList();
        //                _PGTAssessmentList = pgLiteDB.PGTAssessments.Where(x => x.SchoolId == SchoolID && x.nTerm == nTerm && x.sPlaneID == sPlaneID).ToList();


        //                var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                var TAssessementDatas = ServiceHelper.GetXMLTAssessmentParameter(_PGTAssessmentList);
        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var xmlTAssessementList = Common.GetXMLFromObject(TAssessementDatas);

        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;


        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetGradeView";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTAssessmentList", xmlTAssessementList));



        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        tB_GradeViews = ServiceHelper.ConvertDataTableToList<TB_GradeViews>(dt);


        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // return new GetGradeDetailView_Result();

        //                }
        //                finally
        //                {
        //                    //if (connectionState != ConnectionState.Closed) conn.Close();
        //                }
        //            }
        //            catch
        //            {

        //            }
        //        }
        //    }

        //    return tB_GradeViews;

        //}

        //public static List<GetStudentGradeByTermAndRoom_Result> GetSQStudentGradeByTermAndRoom(int sId, int schoolId, int nTermSubLevel2, string nTerm)
        //{

        //    List<GetStudentGradeByTermAndRoom_Result> _GetStudentGradeByTermAndRoom_ResultList = new List<GetStudentGradeByTermAndRoom_Result>();



        //    using (var pgLiteDB = new PGGradeDBEntities())
        //    {
        //        using (JabJaiSchoolGradeEntities _dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
        //        {
        //            try
        //            {

        //                List<PGTGrade> _PGTGradeList = new List<PGTGrade>();
        //                List<PGTGradeDetail> _PGTGradeDetailList = new List<PGTGradeDetail>();
        //                List<PGTGradeEdit> _PGTGradeEditList = new List<PGTGradeEdit>();

        //                _PGTGradeList = pgLiteDB.PGTGrades.Where(x => x.SchoolID == schoolId && x.nTerm == nTerm && (x.nTermSubLevel2 == nTermSubLevel2 || x.nTermSubLevel2 == 0 || x.nTermSubLevel2 == null)).ToList();

        //                var SQGradeDetails = pgLiteDB.PGTGradeDetails.Where(x => x.SchoolID == schoolId && x.sID == sId && x.cDel == false).ToList();

        //                var SQGradeEdits = pgLiteDB.PGTGradeEdits.Where(x => x.SchoolID == schoolId).ToList();


        //                _PGTGradeDetailList = (from g in _PGTGradeList
        //                                       join gd in SQGradeDetails on g.nGradeId equals gd.nGradeId
        //                                       where g.SchoolID == schoolId && gd.SchoolID == schoolId
        //                                       && g.nTerm == nTerm && gd.sID == sId
        //                                       && (g.nTermSubLevel2 == nTermSubLevel2 || g.nTermSubLevel2 == 0 || g.nTermSubLevel2 == null) && gd.cDel == false
        //                                       select gd
        //                                 ).ToList();


        //                _PGTGradeEditList = (from gd in SQGradeDetails
        //                                     join ge in SQGradeEdits on gd.nGradeDetailId equals ge.GradeDetailID
        //                                     where gd.SchoolID == schoolId && ge.SchoolID == schoolId
        //                                     select ge
        //                                    ).ToList();

        //                var TGradeDatas = ServiceHelper.GetXMLTGadeParameter(_PGTGradeList);
        //                var TGradeDetailDatas = ServiceHelper.GetXMLTGadeDetailParameter(_PGTGradeDetailList);
        //                var TGradeEditDatas = ServiceHelper.GetXMLPGTGradeEditsParameter(_PGTGradeEditList);

        //                var xmlTGradeList = Common.GetXMLFromObject(TGradeDatas);
        //                var xmlTGradeDetailList = Common.GetXMLFromObject(TGradeDetailDatas);
        //                var xmlTGradeEditList = Common.GetXMLFromObject(TGradeEditDatas);

        //                var dt = new DataTable();
        //                var conn = _dbGrade.Database.Connection;
        //                var connectionState = conn.State;
        //                _dbGrade.Database.CommandTimeout = 16000;


        //                try
        //                {


        //                    if (connectionState != ConnectionState.Open) conn.Open();
        //                    using (var cmd = conn.CreateCommand())
        //                    {
        //                        cmd.CommandTimeout = 16000;
        //                        cmd.CommandText = "SQGetStudentGradeByTermAndRoom";
        //                        cmd.CommandType = CommandType.StoredProcedure;
        //                        cmd.Parameters.Add(new SqlParameter("@SchoolId", schoolId));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeList", xmlTGradeList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeDetailList", xmlTGradeDetailList));
        //                        cmd.Parameters.Add(new SqlParameter("@XMLTGradeEditsList", xmlTGradeEditList));


        //                        using (var reader = cmd.ExecuteReader())
        //                        {
        //                            dt.Load(reader);
        //                        }

        //                        _GetStudentGradeByTermAndRoom_ResultList = ServiceHelper.ConvertDataTableToList<GetStudentGradeByTermAndRoom_Result>(dt);


        //                        try
        //                        {
        //                            string sqlUpdateScript = string.Format(@"call PGTGradeDetail_GetStudentGradeByTermAndRoom({0});", schoolId);
        //                            pgLiteDB.Database.ExecuteSqlCommand(sqlUpdateScript);
        //                        }
        //                        catch (Exception ex)
        //                        {

        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    // return new GetGradeDetailView_Result();

        //                }
        //                finally
        //                {
        //                    //if (connectionState != ConnectionState.Closed) conn.Close();
        //                }
        //            }
        //            catch
        //            {

        //            }
        //        }
        //    }

        //    return _GetStudentGradeByTermAndRoom_ResultList;

        //}

        public static List<XMLPGTGradeDetail> GetXMLTGadeDetailParameter(List<PGTGradeDetail> _PGTGradeDetails)
        {
            if (_PGTGradeDetails.Count > 0)
            {
                return _PGTGradeDetails.Select(x => new XMLPGTGradeDetail
                {
                    nGradeDetailId = x.nGradeDetailId,
                    nGradeId = x.nGradeId,
                    sID = x.sID,
                    scoreMidTerm = x.scoreMidTerm,
                    scoreFinalTerm = x.scoreFinalTerm,
                    getScore100 = x.getScore100,
                    getGradeLabel = x.getGradeLabel,
                    getBehaviorLabel = x.getBehaviorLabel,
                    getReadWrite = x.getReadWrite,
                    getSpecial = x.getSpecial,
                    getBehaviorTotal = x.getBehaviorTotal,
                    getQuiz100 = x.getQuiz100,
                    getMid100 = x.getMid100,
                    getFinal100 = x.getFinal100,
                    getSamattana = x.getSamattana,
                    SchoolID = x.SchoolID,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    cDel = x.cDel,
                    scoreBeforeAfterMidTerm = x.scoreBeforeAfterMidTerm,
                    ScoreData = x.ScoreData,
                    ScoreBeforeMidTerm = x.ScoreBeforeMidTerm,
                    ScoreAfterMidTerm = x.ScoreAfterMidTerm,
                    getReadWriteTotal = x.getReadWriteTotal,
                    getSamattanaTotal = x.getSamattanaTotal,
                    getBeforeQuiz100 = x.getBeforeQuiz100,
                    getAfterQuiz100 = x.getAfterQuiz100,
                    nCredit = x.nCredit,
                    CourseTotalHour = x.CourseTotalHour,
                    PlanId = x.PlanId,

                }).ToList();
            }
            else
            {
                return new List<XMLPGTGradeDetail>();
            }
        }
        public static List<XMLPGTGrade> GetXMLTGadeParameter(List<PGTGrade> _PGTGrade)
        {
            if (_PGTGrade.Count > 0)
            {
                return _PGTGrade.Select(x => new XMLPGTGrade
                {
                    nGradeId = x.nGradeId,
                    nTerm = x.nTerm,
                    sPlaneID = x.sPlaneID,
                    sNote = x.sNote,
                    dAdd = x.dAdd,
                    dUpdate = x.dUpdate,
                    nUserAdd = x.nUserAdd,
                    nUserUpdate = x.nUserUpdate,
                    fRatioQuiz = x.fRatioQuiz,
                    fRatioMidTerm = x.fRatioMidTerm,
                    fRatioLateTerm = x.fRatioLateTerm,
                    maxMidTerm = x.maxMidTerm,
                    maxFinalTerm = x.maxFinalTerm,
                    maxGradeTotal = x.maxGradeTotal,
                    maxBehaviorTotal = x.maxBehaviorTotal,
                    studyMonday = x.studyMonday,
                    studyTuesday = x.studyTuesday,
                    studyWednesday = x.studyWednesday,
                    studyThursday = x.studyThursday,
                    studyFriday = x.studyFriday,
                    studySaturday = x.studySaturday,
                    studySunday = x.studySunday,
                    GradeDicimal = x.GradeDicimal,
                    GradeAutoReadScore = x.GradeAutoReadScore,
                    GradeAutoBehaviorScore = x.GradeAutoBehaviorScore,
                    GradeAddBehavior = x.GradeAddBehavior,
                    GradeAddCheewat = x.GradeAddCheewat,
                    GradeCloseBehaviorReadWrite = x.GradeCloseBehaviorReadWrite,
                    GradeShareData = x.GradeShareData,
                    nTermSubLevel2 = x.nTermSubLevel2,
                    fRatioQuizPass = x.fRatioQuizPass,
                    GradeCloseGrade = x.GradeCloseGrade,
                    sortNumber = x.sortNumber,
                    reportPrint = x.reportPrint,
                    GradeCloseSamattana = x.GradeCloseSamattana,
                    optionMid = x.optionMid,
                    optionFinal = x.optionFinal,
                    SchoolID = x.SchoolID,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    cDel = x.cDel,
                    GradeAddSamattana = x.GradeAddSamattana,
                    maxReadWriteTotal = x.maxReadWriteTotal,
                    maxSamattanaTotal = x.maxSamattanaTotal,
                    maxBeforeMidTerm = x.maxBeforeMidTerm,
                    maxAfterMidTerm = x.maxAfterMidTerm,
                    GradeShowFullScore = x.GradeShowFullScore,
                    fRatioBeforeMidTerm = x.fRatioBeforeMidTerm,
                    fRatioAfterMidTerm = x.fRatioAfterMidTerm,
                    PlanId = x.PlanId,
                    GradeDicimalForFinal = x.GradeDicimalForFinal,

                }).ToList();
            }
            else
            {
                return new List<XMLPGTGrade>();
            }
        }
        public static List<XMLPGTAssessment> GetXMLTAssessmentParameter(List<PGTAssessment> _PGTAssessment)
        {
            if (_PGTAssessment.Count > 0)
            {
                return _PGTAssessment.Select(x => new XMLPGTAssessment
                {
                    AssessmentId = x.AssessmentId,
                    AssessmentName = x.AssessmentName,
                    AssessmentTypeId = x.AssessmentTypeId,
                    AssessmentMaxScore = x.AssessmentMaxScore,
                    NameIdentifier = x.NameIdentifier,
                    MaxScoreIdentifier = x.MaxScoreIdentifier,
                    SchoolId = x.SchoolId,
                    nGradeId = x.nGradeId,
                    nYear = x.nYear,
                    nTerm = x.nTerm,
                    nTSubLevel = x.nTSubLevel,
                    nTermSubLevel2 = x.nTermSubLevel2,
                    sPlaneID = x.sPlaneID,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    CreatedBy = x.CreatedBy,
                    UpdatedDate = x.UpdatedDate,
                    IsActive = x.IsActive,
                    cDel = x.cDel,
                    SubmitPeriod = x.SubmitPeriod,
                    AssessmentNameEng = x.AssessmentNameEng,

                }).ToList();
            }
            else
            {
                return new List<XMLPGTAssessment>();
            }
        }

        public static List<XMLPGTGradeEdits> GetXMLPGTGradeEditsParameter(List<PGTGradeEdit> _PGTGradeEdits)
        {
            if (_PGTGradeEdits.Count > 0)
            {
                return _PGTGradeEdits.Select(x => new XMLPGTGradeEdits
                {
                    GradeEditsID = x.GradeEditsID,
                    GradeDetailID = x.GradeDetailID,
                    GradeCalculation = x.GradeCalculation,
                    GradeSet = x.GradeSet,
                    UserUpdate = x.UserUpdate,
                    DateUpdate = x.DateUpdate,
                    UseGradeSet = x.UseGradeSet,
                    SchoolID = x.SchoolID,
                    CreatedBy = x.CreatedBy,
                    UpdatedBy = x.UpdatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    cDel = x.cDel,
                    scoreMidTerm = x.scoreMidTerm,
                    scoreFinalTerm = x.scoreFinalTerm,
                    getScore100 = x.getScore100,
                    getMid100 = x.getMid100,
                    getFinal100 = x.getFinal100,
                    getGradeLabel = x.getGradeLabel,

                }).ToList();
            }
            else
            {
                return new List<XMLPGTGradeEdits>();
            }
        }
        #endregion
        public static List<StudentDTO> GetStudyStudentsForSpecificSubject(int year, string term, int idlv, int planCourseId, int sPlaneId, int schoolid)
        {
            var studentList = new List<StudentDTO>();
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                var nTerm = (from t in schoolDbContext.TTerms.Where(w => w.SchoolID == schoolid && string.IsNullOrEmpty(w.cDel) && w.sTerm == term)
                             join y in schoolDbContext.TYears.Where(w => w.SchoolID == schoolid && w.numberYear == year) on t.nYear equals y.nYear
                             select t.nTerm).FirstOrDefault();
                var tTermSubLevel2 = schoolDbContext.TTermSubLevel2.Where(w => w.SchoolID == schoolid && w.nTSubLevel == idlv && w.nWorkingStatus == 1 && w.cDel == false).ToList();
                if (tTermSubLevel2 != null && tTermSubLevel2.Count > 0)
                {
                    foreach (var idlv2n in tTermSubLevel2)
                    {
                        var commonRequest = new CommonRequest()
                        {
                            nTerm = nTerm,
                            nTermSubLevel2 = idlv2n.nTermSubLevel2,
                            nTSubLevel = idlv,
                            PlanCourseId = planCourseId,
                            SchoolId = schoolid,
                        };
                        if (ServiceHelper.CheckTermIsActiveInPlan(commonRequest))
                        {
                            //Fetch selected student from plan
                            var getStudentsfromPlanCourse = ServiceHelper.GetStudentsWithGraduatedStatusforpw5(commonRequest); //CommonService.GetStudents(commonRequest);
                            if (getStudentsfromPlanCourse != null && getStudentsfromPlanCourse.Count > 0)
                            {
                                studentList.AddRange(getStudentsfromPlanCourse);
                            }
                        }
                    }
                }
            }

            return studentList;
        }

        public static List<int> GetRoomListForpw5(int ntSubLevel, int schoolId, int planCourseid, int userYear, string userTerm)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {

                List<int> idlv2ns = new List<int>();
                idlv2ns = (from r in schoolDbContext.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == ntSubLevel && w.nWorkingStatus == 1)
                           select r.nTermSubLevel2).ToList();

                var nTerm = (from y in schoolDbContext.TYears.Where(w => w.SchoolID == schoolId && w.numberYear == userYear)
                             join t in schoolDbContext.TTerms.Where(w => w.SchoolID == schoolId && w.sTerm == userTerm) on y.nYear equals t.nYear
                             select t.nTerm).FirstOrDefault();


                var roomList = (from s in schoolDbContext.TPlanCourseStudents
                                join p in schoolDbContext.TPlanCourses on s.PlanCourseId equals p.PlanCourseId
                                join t in schoolDbContext.TPlanCourseTerms on s.nTerm equals t.nTerm
                                join sl2 in schoolDbContext.TPlanTermSubLevel2 on s.nTermSubLevel2 equals sl2.nTermSubLevel2
                                join r in schoolDbContext.TTermSubLevel2.Where(w => w.SchoolID == schoolId && w.nTSubLevel == ntSubLevel && w.nWorkingStatus == 1) on sl2.nTermSubLevel2 equals r.nTermSubLevel2
                                where s.SchoolID == schoolId && p.SchoolID == schoolId && t.SchoolID == schoolId
                                && sl2.SchoolID == schoolId && s.IsActive == true && s.nTerm == nTerm && idlv2ns.Contains(s.nTermSubLevel2 ?? 0) && s.nTSubLevel == ntSubLevel && p.CourseStatus == 1
                                && t.IsActive == true && t.nTerm == nTerm && idlv2ns.Contains(sl2.nTermSubLevel2) && sl2.IsActive == true && p.PlanCourseId == planCourseid
                                orderby r.nTSubLevel2
                                select s.nTermSubLevel2 ?? 0).Distinct().ToList();

                return roomList;

            }
        }
        public static List<StudentDTO> GetStudentsWithGraduatedStatusforpw5(CommonRequest commonRequest)
        {
            using (JabJaiEntities schoolDbContext = new JabJaiEntities(Connection.SchoolDBConnection(ConnectionDB.Read)))
            {
                var students = (from s in schoolDbContext.TB_StudentViews.Where(c => c.nTerm == commonRequest.nTerm && c.nTermSubLevel2 == commonRequest.nTermSubLevel2 && (c.cDel ?? "0") != "1" && c.SchoolID == commonRequest.SchoolId)
                                join u in schoolDbContext.TUser.Where(w => w.SchoolID == commonRequest.SchoolId) on s.sID equals u.sID
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
                                    NTermSubLevel2 = commonRequest.nTermSubLevel2,
                                    nTSubLevel2 = s.nTSubLevel2
                                }).Distinct().AsQueryable().ToList();

                var getStudentsfromPlanCourse = (from s in schoolDbContext.TPlanCourseStudents
                                                 join p in schoolDbContext.TPlanCourses on s.PlanCourseId equals p.PlanCourseId
                                                 join t in schoolDbContext.TPlanCourseTerms on s.nTerm equals t.nTerm
                                                 join sl2 in schoolDbContext.TPlanTermSubLevel2 on s.nTermSubLevel2 equals sl2.nTermSubLevel2
                                                 where s.SchoolID == commonRequest.SchoolId && p.SchoolID == commonRequest.SchoolId && t.SchoolID == commonRequest.SchoolId
                                                 && sl2.SchoolID == commonRequest.SchoolId && s.IsActive == true && s.nTerm == commonRequest.nTerm && s.nTermSubLevel2 == commonRequest.nTermSubLevel2 && s.nTSubLevel == commonRequest.nTSubLevel && p.CourseStatus == 1 && t.IsActive == true && t.nTerm == commonRequest.nTerm && sl2.nTermSubLevel2 == commonRequest.nTermSubLevel2 && sl2.IsActive == true && p.PlanCourseId == commonRequest.PlanCourseId
                                                 select s.sID).Distinct().ToList();

                if (getStudentsfromPlanCourse != null && getStudentsfromPlanCourse.Count > 0)
                {
                    students = (from s in students
                                join ps in getStudentsfromPlanCourse on s.SId equals ps
                                select s).ToList();
                }
                else
                {
                    students = new List<StudentDTO>();
                }

                return students;
            }
        }
    }
    public class teacherDescribe
    {
        public int number { get; set; }
        public string describe { get; set; }
        public int id { get; set; }

    }
    public class CellProperty
    {
        public string Text { get; set; }
        public Font Font { get; set; }
        public int? Rowspan { get; set; }
        public int? Colspan { get; set; }
        public int? HorizontalAlignment { get; set; }
        public int? VerticalAlignment { get; set; }
        public int? Border { get; set; }
        public int? Rotation { get; set; }
        public float? Height { get; set; }
        public float? PaddingLeft { get; set; }
        public float? PaddingBottom { get; set; }
        public float? PaddingTop { get; set; }
        public Color BackgroundColor { get; set; }
        public class Color
        {
            public int Red { get; set; }
            public int Green { get; set; }
            public int Blue { get; set; }
        }
    }

    public partial class GetStudentGradeInfoCommon_ResultData
    {
        public Nullable<int> sID { get; set; }
        public string sStudentId { get; set; }
        public string sIdentification { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TitleDesc { get; set; }
        public Nullable<int> nTSubLevel { get; set; }
        public Nullable<int> nTermSubLevel2 { get; set; }
        public Nullable<int> nYear { get; set; }
        public Nullable<int> numberYear { get; set; }
        public string sTerm { get; set; }
        public string NTerm { get; set; }
        public string FullClassName { get; set; }
        public string ClassName { get; set; }
        public string RoomName { get; set; }
        public Nullable<int> sPlaneID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public Nullable<int> CourseType { get; set; }
        public string CourseTypeName { get; set; }
        public Nullable<int> CourseGroup { get; set; }
        public string CourseGroupName { get; set; }
        public Nullable<double> CourseTotalHour { get; set; }
        public Nullable<int> CourseStatus { get; set; }
        public Nullable<double> CourseHour { get; set; }
        public int nGradeId { get; set; }
        public Nullable<double> fRatioQuiz { get; set; }
        public Nullable<double> fRatioBeforeQuiz { get; set; }
        public Nullable<double> fRatioAfterQuiz { get; set; }
        public Nullable<double> fRatioMidTerm { get; set; }
        public Nullable<double> fRatioLateTerm { get; set; }
        public string MaxBeforeAfterMidTermTotal { get; set; }
        public string maxMidTerm { get; set; }
        public string maxFinalTerm { get; set; }
        public string maxBeforeMidTerm { get; set; }
        public string maxAfterMidTerm { get; set; }
        public string scoreBeforeAfterMidTerm { get; set; }
        public string ScoreBeforeMidTerm { get; set; }
        public string ScoreAfterMidTerm { get; set; }
        public string scoreMidTerm { get; set; }
        public string scoreFinalTerm { get; set; }
        public string getQuiz100 { get; set; }
        public string getMid100 { get; set; }
        public string getFinal100 { get; set; }
        public string getBeforeQuiz100 { get; set; }
        public string getAfterQuiz100 { get; set; }
        public string getScore100 { get; set; }
        public string getGradeLabel { get; set; }
        public int GradePass { get; set; }
        public Nullable<double> NCredit { get; set; }
        public string getBehaviorLabel { get; set; }
        public string getReadWrite { get; set; }
        public string getSpecial { get; set; }
        public string getBehaviorTotal { get; set; }
        public string getSamattana { get; set; }
        public Nullable<int> nStudentStatus { get; set; }
        public Nullable<int> nStudentNumber { get; set; }
        public int IsScoreExist { get; set; }
        public Nullable<decimal> GradeSet { get; set; }
        public Nullable<double> GradeXNCredit { get; set; }
        public Nullable<int> nOrder { get; set; }
        public Nullable<double> GPA { get; set; }
        public Nullable<double> ActualNCredit { get; set; }
        public string CourseTypeNameEng { get; set; }
        public string CourseGroupNameEng { get; set; }
        public string getScore100New { get; set; }
        public string getScore100Display { get; set; }
        public Nullable<long> RankingByClass { get; set; }
        public Nullable<long> RankingByLevel { get; set; }
    }
}