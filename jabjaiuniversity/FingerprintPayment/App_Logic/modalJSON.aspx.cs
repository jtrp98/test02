using FingerprintPayment.Helper;
using FingerprintPayment.ViewModels;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Ninject;
using SchoolBright.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    public partial class modalJSON2 : System.Web.UI.Page
    {
        [Inject]
        public IPlanService PlanService { get; set; }

        [Inject]
        public ITimeTableSettingService TimeTableSettingService { get; set; }

        System.Web.Script.Serialization.JavaScriptSerializer serializerObject = new System.Web.Script.Serialization.JavaScriptSerializer();
        private JWTToken.userData userData = new JWTToken.userData();

        protected void Page_Load(object sender, EventArgs e)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            var mode = Request.QueryString["mode"];
            try
            {
                using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
                {
                    //string entities = Session["sEntities"].ToString();
                    int schoolID = userData.CompanyID;
                    var f_company = dbmaster.TCompanies.FirstOrDefault(f => f.nCompany == schoolID);
                    using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                    {

                        switch (mode)
                        {
                            case "planelist":

                                int termlv = Convert.ToInt32(Request.QueryString["sublv"]);
                                System.Web.Script.Serialization.JavaScriptSerializer serializerPlaneList = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtplanelist = fcommon.LinqToDataTable(_db.TPlanLists.Where(w => w.SchoolID == schoolID).ToList());

                                var queryplanelist = from TPlanLists in _db.TPlanLists.Where(w => w.SchoolID == schoolID)
                                                     join TPlanes in _db.TPlanes.Where(w => w.SchoolID == schoolID) on TPlanLists.sPlaneID equals TPlanes.sPlaneID
                                                     //   .Where(hpl => hpl.TTermSublv == termlv).DefaultIfEmpty()
                                                     //from TPlanes in _db.TPlane
                                                     where TPlanLists.TTermSublv == termlv && TPlanLists.cDel == null
                                                     select new
                                                     {
                                                         sNo = TPlanLists.PlanListID,
                                                         sPlaneName = TPlanes.sPlaneName,
                                                         sPlaneID = TPlanLists.sPlaneID
                                                     };
                                if (queryplanelist.ToList().Count > 0)
                                {
                                    dtplanelist = fcommon.LinqToDataTable(queryplanelist);

                                    List<Dictionary<string, object>> rowsplanelist = new List<Dictionary<string, object>>();
                                    Dictionary<string, object> rowplanelist;
                                    foreach (DataRow dr in dtplanelist.Rows)
                                    {
                                        rowplanelist = new Dictionary<string, object>();
                                        foreach (DataColumn col in dtplanelist.Columns)
                                        {
                                            rowplanelist.Add(col.ColumnName, dr[col]);
                                        }
                                        rowsplanelist.Add(rowplanelist);
                                    }
                                    // return ;
                                    Response.Write(serializerPlaneList.Serialize(rowsplanelist).ToString());
                                    Response.End();
                                }
                                else
                                {
                                    Response.Write("[]");
                                    Response.End();
                                }

                                break;
                            case "plane":
                                System.Web.Script.Serialization.JavaScriptSerializer serializerPlane = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtplane = fcommon.LinqToDataTable(_db.TPlanes.Where(w => w.SchoolID == schoolID).ToList());
                                List<Dictionary<string, object>> rowsplane = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowplane;
                                foreach (DataRow dr in dtplane.Rows)
                                {
                                    rowplane = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtplane.Columns)
                                    {
                                        rowplane.Add(col.ColumnName, dr[col]);
                                    }
                                    rowsplane.Add(rowplane);
                                }
                                // return ;
                                Response.Write(serializerPlane.Serialize(rowsplane).ToString());
                                Response.End();
                                break;

                            case "schegen":
                                //System.Web.Script.Serialization.JavaScriptSerializer serializerScheGenxx = new System.Web.Script.Serialization.JavaScriptSerializer();
                                //int sTermGen = Convert.ToInt32(Request.QueryString["term"]);
                                //int sdayGen = Convert.ToInt32(Request.QueryString["days"]);
                                //DataTable dtScheGen;



                                //var queryScheGen = from sche in _db.TSchedule
                                //                   .Where(w => w.nPlaneDay == sdayGen && w.TermSublv == sTermGen && w.cDel == null)
                                //                   from Teacher in _db.TUser
                                //                   .Where(t => t.sID == sche.sID).DefaultIfEmpty()
                                //                   from Plane in _db.TPlane
                                //                   .Where(p => p.sPlaneID == sche.sPlaneID).DefaultIfEmpty()
                                //                   select new
                                //                   {
                                //                       nDay = sdayGen,
                                //                       teacher = Teacher.sName + " " + Teacher.sLastname,
                                //                       plane = Plane.sPlaneName,
                                //                       time1H = sche.dTimeStart_IN.Value.Hours,
                                //                       time1M = sche.dTimeStart_IN.Value.Minutes,
                                //                       time2H = sche.dTimeEnd_IN.Value.Hours,
                                //                       time2M = sche.dTimeEnd_IN.Value.Minutes
                                //                   };//

                                //if (queryScheGen.ToList().Count > 0)
                                //{
                                //    dtScheGen = fcommon.LinqToDataTable(queryScheGen);

                                //    dtScheGen = fcommon.LinqToDataTable(queryScheGen);

                                //    List<Dictionary<string, object>> rowsScheGen = new List<Dictionary<string, object>>();
                                //    Dictionary<string, object> rowScheGen;
                                //    foreach (DataRow dr in dtScheGen.Rows)
                                //    {
                                //        rowScheGen = new Dictionary<string, object>();
                                //        foreach (DataColumn col in dtScheGen.Columns)
                                //        {
                                //            rowScheGen.Add(col.ColumnName, dr[col]);
                                //        }
                                //        rowsScheGen.Add(rowScheGen);
                                //    }
                                //    // return ;
                                //    Response.Write(serializerScheGenxx.Serialize(rowsScheGen).ToString());
                                //    Response.End();
                                //}
                                //Response.Write("[]");
                                //Response.End();

                                break;
                            case "teacher":
                                System.Web.Script.Serialization.JavaScriptSerializer serializerTeacher = new System.Web.Script.Serialization.JavaScriptSerializer();
                                var q_teacher = (from a in dbmaster.TUsers
                                                 where a.cDel == null && a.cType == "1" && a.nCompany == f_company.nCompany
                                                 select new
                                                 {
                                                     a.sName,
                                                     a.sLastname,
                                                     a.nSystemID,
                                                     a.sPhone,
                                                     a.username
                                                 }).ToList();

                                var q_employees = (from a in _db.TEmployees
                                                   from b in _db.TEmployeeInfoes.Where(o => o.SchoolID == a.SchoolID && o.sEmp == a.sEmp).DefaultIfEmpty()
                                                   where a.SchoolID == schoolID
                                                   where a.cDel == null
                                                   select new
                                                   {
                                                       b.Code,
                                                       a.sName,
                                                       a.sLastname,
                                                       a.sEmp,
                                                       a.nDepartmentId,
                                                       a.cType
                                                   }).ToList();

                                //DataTable dteacher = fcommon.LinqToDataTable(from a in dbmaster.TUsers.Where(c => c.cDel == null && c.cType == "1" && c.nCompany == f_company.nCompany)
                                //                                             join b in _db.TEmployees.Where(emp => emp.SchoolID == schoolID && emp.cDel == null) on a.nSystemID equals b.sEmp
                                //                                             //where b.cDel == null && a.cDel == null && a.cType == "1" && a.nCompany == f_company.nCompany
                                //                                             select new
                                //                                             {
                                //                                                 a.sName,
                                //                                                 a.sLastname,
                                //                                                 b.sEmp,
                                //                                                 a.sPhone,
                                //                                                 a.username,
                                //                                                 b.nDepartmentId,
                                //                                                 b.cType
                                //                                             });

                                DataTable dteacher = fcommon.LinqToDataTable(from a in q_teacher
                                                                             join b in q_employees on a.nSystemID equals b.sEmp
                                                                             select new
                                                                             {
                                                                                 b.Code,
                                                                                 b.sName,
                                                                                 b.sLastname,
                                                                                 b.sEmp,
                                                                                 a.sPhone,
                                                                                 a.username,
                                                                                 b.nDepartmentId,
                                                                                 b.cType
                                                                             });

                                List<Dictionary<string, object>> rowsteacher = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowteacher;
                                if (dteacher != null)
                                {
                                    foreach (DataRow dr in dteacher.Rows)
                                    {
                                        rowteacher = new Dictionary<string, object>();
                                        foreach (DataColumn col in dteacher.Columns)
                                        {
                                            rowteacher.Add(col.ColumnName, dr[col]);
                                        }
                                        rowsteacher.Add(rowteacher);
                                    }
                                }
                                // return ;
                                Response.Write(serializerTeacher.Serialize(rowsteacher).ToString());
                                Response.End();
                                break;
                            case "backupcard":


                                var dataCards = (from a in _db.TBackupCards.Where(o => o.SchoolID == schoolID && o.cDel == false)
                                                 select new
                                                 {
                                                     id = a.CardID,
                                                     name = a.CardName,
                                                     money = a.Money,
                                                     insurance = a.Insurance,
                                                 }).ToList();

                                Response.Write(
                                    serializerObject.Serialize(
                                        dataCards
                                    )
                                );
                                Response.End();
                                break;
                            case "PlanCourseTeacher":
                                var nTermSubLevel2 = Request.QueryString["nTermSubLevel2"];
                                var nTerm = Request.QueryString["nTerm"];
                                var nPlaneDay = String.IsNullOrEmpty(Request.QueryString["nPlaneDay"]) ? 0 : int.Parse(Request.QueryString["nPlaneDay"]);
                                var sPlaneId = String.IsNullOrEmpty(Request.QueryString["sPlaneId"].ToString()) ? 0 : int.Parse(Request.QueryString["sPlaneId"]);
                                var scheduleid = String.IsNullOrEmpty(Request.QueryString["scheduleid"].ToString()) ? 0 : int.Parse(Request.QueryString["scheduleid"]);
                                var planCourseTeachers = ServiceHelper.GetTeachersForAPlanCourse(Convert.ToInt32(nTermSubLevel2), nTerm, sPlaneId, _db, nPlaneDay, scheduleid);
                                var teachers = (from pc in planCourseTeachers
                                                orderby pc.SName, pc.SLastname
                                                select new TeacherViewModel
                                                {
                                                    SEmp = pc.SEmp,
                                                    SLastname = pc.SLastname,
                                                    SName = pc.SName,
                                                    IsTimeTableScheduled = pc.IsTimeTableScheduled

                                                }).ToList().DistinctBy(d => d.SEmp).ToList();
                                teachers = (teachers != null) ? teachers.OrderBy(o => o.FullName).ToList() : teachers;

                                Response.Write(serializerObject.Serialize(teachers).ToString());
                                Response.End();
                                break;
                            case "HomeTeacher":
                                nTermSubLevel2 = Request.QueryString["nTermSubLevel2"];
                                nTerm = Request.QueryString["nTerm"];
                                var classTeacherDTO = TimeTableSettingService.GetClassHomeTeacherInfo(Convert.ToInt32(nTermSubLevel2), nTerm, userData.CompanyID);
                                var homeTeachers = new List<TeacherViewModel>();

                                if (classTeacherDTO != null)
                                {
                                    homeTeachers.Add(new TeacherViewModel
                                    {
                                        SEmp = classTeacherDTO.SEmp,
                                        SLastname = classTeacherDTO.Lastname,
                                        SName = classTeacherDTO.Name
                                    });
                                }

                                Response.Write(serializerObject.Serialize(homeTeachers).ToString());
                                Response.End();
                                break;
                            case "termyear":
                                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dt = fcommon.LinqToDataTable(_db.TSubLevels.Where(w => w.SchoolID == schoolID).ToList());
                                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                                Dictionary<string, object> row;
                                foreach (DataRow dr in dt.Rows)
                                {
                                    row = new Dictionary<string, object>();
                                    foreach (DataColumn col in dt.Columns)
                                    {
                                        row.Add(col.ColumnName, dr[col]);
                                    }
                                    rows.Add(row);
                                }
                                // return ;
                                Response.Write(serializer.Serialize(rows).ToString());
                                Response.End();
                                break;
                            case "learnlist":
                                //System.Web.Script.Serialization.JavaScriptSerializer serializerLearnList = new System.Web.Script.Serialization.JavaScriptSerializer();
                                //int sNoList = Convert.ToInt32(Request.QueryString["sNo"]);
                                //string splaneList = Request.QueryString["plane"];
                                //int stermList = Convert.ToInt32(Request.QueryString["term"]);
                                //DataTable dtLearnList = fcommon.LinqToDataTable(_db.TSchedules.Where(w => w.sNo == sNoList));
                                //var queryLearnList = from Sche in _db.TSchedule
                                //                     .Where(w => w.sPlaneID == splaneList && w.TermSublv == stermList)
                                //                     from Class in _db.TClass
                                //                     .Where(w => w.sClassID == Sche.sClassID)
                                //                     from Teacher in _db.TUser
                                //                     .Where(t => t.sID == Sche.sID).DefaultIfEmpty()
                                //                     select new
                                //                     {
                                //                         nPlaneDay = Sche.nPlaneDay,
                                //                         nName = Teacher.sName + " " + Teacher.sLastname,
                                //                         learnID = Sche.sScheduleID,
                                //                         Class = Class.sClass,
                                //                         dTimeStart_INH = Sche.dTimeStart_IN.Value.Hours,
                                //                         dTimeStart_INM = Sche.dTimeStart_IN.Value.Minutes,
                                //                         dTimeStart_OUTH = Sche.dTimeStart_OUT.Value.Hours,
                                //                         dTimeStart_OUTM = Sche.dTimeStart_OUT.Value.Minutes,
                                //                         dTimeEnd_INH = Sche.dTimeEnd_IN.Value.Hours,
                                //                         dTimeEnd_INM = Sche.dTimeEnd_IN.Value.Minutes,
                                //                         dTimeEnd_OUTH = Sche.dTimeEnd_OUT.Value.Hours,
                                //                         dTimeEnd_OUTM = Sche.dTimeEnd_OUT.Value.Minutes,
                                //                     };
                                //if (queryLearnList.ToList().Count > 0)
                                //{
                                //    dtLearnList = fcommon.LinqToDataTable(queryLearnList);
                                //    //
                                //    List<Dictionary<string, object>> rowsLearnList = new List<Dictionary<string, object>>();
                                //    Dictionary<string, object> rowLearnList;
                                //    foreach (DataRow dr in dtLearnList.Rows)
                                //    {
                                //        rowLearnList = new Dictionary<string, object>();
                                //        foreach (DataColumn col in dtLearnList.Columns)
                                //        {
                                //            rowLearnList.Add(col.ColumnName, dr[col]);
                                //        }
                                //        rowsLearnList.Add(rowLearnList);
                                //    }
                                //    // return ;
                                //    Response.Write(serializerLearnList.Serialize(rowsLearnList).ToString());
                                //    Response.End();
                                //}
                                //Response.Write("[]");
                                //Response.End();
                                break;
                            #region holidayset
                            case "holidayset":
                                System.Web.Script.Serialization.JavaScriptSerializer serializer2 = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dt2 = fcommon.LinqToDataTable(_db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList());
                                List<Dictionary<string, object>> rows2 = new List<Dictionary<string, object>>();
                                Dictionary<string, object> row2;

                                string nHol = Request.QueryString["nHol"];
                                List<THoliday> _hol2 = _db.THolidays.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == nHol && w.sHolidayAll == "1").ToList();
                                if (_hol2.Count > 0)
                                {
                                    var query = from SubLevels in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).DefaultIfEmpty()
                                                select new
                                                {
                                                    nHoliday = nHol,
                                                    nTSubLevel = SubLevels.nTSubLevel,
                                                    SubLevel = SubLevels.SubLevel,
                                                    nAll = "1"
                                                };
                                    dt2 = fcommon.LinqToDataTable(query);
                                }
                                else
                                {
                                    var query = from TB1 in
                                                    (from A in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                     join B in _db.THolidaySomes.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nHoliday == nHol) on A.nTSubLevel equals B.nTSubLevel into AB
                                                     from subpet in AB.DefaultIfEmpty()
                                                     select new
                                                     {
                                                         nHoliday = subpet.nHoliday,
                                                         nTSubLevel = A.nTSubLevel,
                                                         SubLevel = A.SubLevel,
                                                     })
                                                join C in _db.THolidays.Where(w => w.SchoolID == userData.CompanyID) on TB1.nHoliday equals C.nHoliday into AB
                                                from subpet in AB.DefaultIfEmpty()
                                                select new
                                                {
                                                    nHoliday = TB1.nHoliday,
                                                    nTSubLevel = TB1.nTSubLevel,
                                                    SubLevel = TB1.SubLevel,
                                                    nAll = (TB1.nHoliday == null ? "0" : "1")
                                                };

                                    if (query.ToList().Count > 0)
                                    {
                                        dt2 = fcommon.LinqToDataTable(query);
                                    }
                                }
                                //
                                foreach (DataRow dr2 in dt2.Rows)
                                {
                                    row2 = new Dictionary<string, object>();
                                    foreach (DataColumn col in dt2.Columns)
                                    {
                                        row2.Add(col.ColumnName, dr2[col]);
                                    }
                                    rows2.Add(row2);
                                }
                                // return ;
                                Response.Write(serializer2.Serialize(rows2).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region btnStdManage
                            case "btnStdManage":
                                //System.Web.Script.Serialization.JavaScriptSerializer serializerStudent = new System.Web.Script.Serialization.JavaScriptSerializer();
                                //DataTable dtStudent = new DataTable();
                                //List<Dictionary<string, object>> rowsStd = new List<Dictionary<string, object>>();
                                //Dictionary<string, object> rowStd;
                                //int LevelData = Int32.Parse(Request.QueryString["termsub"]);

                                //if (LevelData == -1)
                                //{
                                //    var queryStd = from Student in _db.TUsers.Where(w => w.cType == "0" && w.cDel == null)
                                //                   join SubLevel in _db.TStudentLevels.ToList() on Student.sID equals SubLevel.sID
                                //                   where SubLevel.nTermSubLevel2 == null || SubLevel.nTermSubLevel2 == 0// into temp
                                //                    && SubLevel.nTSubLevel != -2 && SubLevel.nTSubLevel != -3
                                //                   // from StdData in temp.Where(d => d.nTermSubLevel2 == null).DefaultIfEmpty()
                                //                   select new
                                //                   {
                                //                       sID = Student.sID,
                                //                       Name = Student.sName,
                                //                       LastName = Student.sLastname,
                                //                       LevelData = (SubLevel.nTermSubLevel2 == null ? -1 : SubLevel.nTermSubLevel2),
                                //                       Lv2Data = (SubLevel.nTSubLevel == null ? 0 : SubLevel.nTSubLevel),
                                //                       TSubLevel = (SubLevel.nTSubLevel == null ? 0 : SubLevel.nTSubLevel)

                                //                   };
                                //    if (queryStd.ToList().Count > 0)
                                //    {
                                //        dtStudent = fcommon.LinqToDataTable(queryStd);
                                //    }
                                //}
                                //else
                                //{

                                //    var queryStd = from Student in _db.TUsers.Where(w => w.cType == "0" && w.cDel == null)
                                //                   join SubLevel in _db.TStudentLevels.ToList() on Student.sID equals SubLevel.sID
                                //                   where SubLevel.nTermSubLevel2 == LevelData && SubLevel.nTSubLevel != -2 && SubLevel.nTSubLevel != -3
                                //                   select new
                                //                   {
                                //                       sID = Student.sID,
                                //                       Name = Student.sName,
                                //                       LastName = Student.sLastname,
                                //                       LevelData = (SubLevel.nTermSubLevel2 == null ? -1 : SubLevel.nTermSubLevel2),
                                //                       TSubLevel = (SubLevel.nTSubLevel == null ? 0 : SubLevel.nTSubLevel)
                                //                   };
                                //    if (queryStd.ToList().Count > 0)
                                //    {
                                //        dtStudent = fcommon.LinqToDataTable(queryStd);
                                //    }

                                //}

                                //foreach (DataRow drStd in dtStudent.Rows)
                                //{
                                //    rowStd = new Dictionary<string, object>();
                                //    foreach (DataColumn col in dtStudent.Columns)
                                //    {
                                //        rowStd.Add(col.ColumnName, drStd[col]);
                                //    }
                                //    rowsStd.Add(rowStd);
                                //}
                                //// return ;
                                //Response.Write(serializerStudent.Serialize(rowsStd).ToString());
                                //Response.End();

                                break;
                            #endregion
                            #region yeartermset
                            case "yeartermset":
                                System.Web.Script.Serialization.JavaScriptSerializer serializer3 = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dt3 = new DataTable();//= fcommon.LinqToDataTable(_db.TSubLevels);
                                List<Dictionary<string, object>> rows3 = new List<Dictionary<string, object>>();
                                Dictionary<string, object> row3;

                                // string nHol3 = Request.QueryString["nHol"];

                                var query3 = from Year in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).ToList()
                                             .Where(years => years.YearStatus != "0")
                                                 //.Where(years => years.numberYear == (DateTime.Now.Year+543))
                                             from Term in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                             .Where(term => term.nYear == Year.nYear && term.cDel != "1").DefaultIfEmpty()
                                             from SubLv2 in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID)
                                             .Where(slv2 => slv2.nTerm == Term.nTerm).DefaultIfEmpty()
                                             from SubLv in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                             .Where(slv => slv.nTSubLevel == SubLv2.nTSubLevel).DefaultIfEmpty()
                                             from lv in _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                             .Where(lv => lv.LevelID == SubLv.nTLevel).DefaultIfEmpty()
                                             select new
                                             {
                                                 nYear = Year.nYear,
                                                 Level = lv.LevelName,
                                                 SubLevel = SubLv.SubLevel,
                                                 SubLevel2 = SubLv2.nTSubLevel2,
                                                 Term = SubLv2.nTermSubLevel2,
                                             };
                                if (query3.ToList().Count > 0)
                                    dt3 = fcommon.LinqToDataTable(query3);

                                foreach (DataRow dr3 in dt3.Rows)
                                {
                                    row3 = new Dictionary<string, object>();
                                    foreach (DataColumn col in dt3.Columns)
                                    {
                                        row3.Add(col.ColumnName, dr3[col]);
                                    }
                                    rows3.Add(row3);
                                }
                                // return ;
                                Response.Write(serializer3.Serialize(rows3).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region itemlv
                            case "itemlv":
                                System.Web.Script.Serialization.JavaScriptSerializer serializeItemLv = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtItemLv = new DataTable();
                                List<Dictionary<string, object>> rowsLv = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowLv;
                                var queryItemlv = from Level in _db.TLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                  select new
                                                  {
                                                      LevelValue = Level.LevelID,
                                                      LevelName = Level.LevelName
                                                  };
                                if (queryItemlv.ToList().Count > 0)
                                {
                                    dtItemLv = fcommon.LinqToDataTable(queryItemlv);
                                }

                                foreach (DataRow drItemLv in dtItemLv.Rows)
                                {
                                    rowLv = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtItemLv.Columns)
                                    {
                                        rowLv.Add(col.ColumnName, drItemLv[col]);
                                    }
                                    rowsLv.Add(rowLv);
                                }
                                // return ;
                                Response.Write(serializeItemLv.Serialize(rowsLv).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region thisTSub
                            case "thisTSub":
                                System.Web.Script.Serialization.JavaScriptSerializer serializeThisTSub = new System.Web.Script.Serialization.JavaScriptSerializer();
                                List<Dictionary<string, object>> rowsTSUB = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowTSUB;
                                int thisnTSUB = Int32.Parse(Request.QueryString["sublv"]);
                                DataTable dtItemTSUB = fcommon.LinqToDataTable(_db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.nTSubLevel == thisnTSUB));

                                foreach (DataRow drItemTSUBDR in dtItemTSUB.Rows)
                                {
                                    rowTSUB = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtItemTSUB.Columns)
                                    {
                                        rowTSUB.Add(col.ColumnName, drItemTSUBDR[col]);
                                    }
                                    rowsTSUB.Add(rowTSUB);
                                }
                                // return ;
                                Response.Write(serializeThisTSub.Serialize(rowsTSUB).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region itemsublv1
                            case "itemsublv1":
                                System.Web.Script.Serialization.JavaScriptSerializer serializeItemSub = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtItemSubLv1 = new DataTable();
                                List<Dictionary<string, object>> rowsLv1 = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowLv1;
                                int lv1 = Int32.Parse(Request.QueryString["lv1"]);
                                var queryItemSublv1 = from SubLevel in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                      where SubLevel.nTLevel == lv1
                                                      select new
                                                      {
                                                          SubLevelValue = SubLevel.nTSubLevel,
                                                          SubLevelName = SubLevel.SubLevel
                                                      };
                                if (queryItemSublv1.ToList().Count > 0)
                                {
                                    dtItemSubLv1 = fcommon.LinqToDataTable(queryItemSublv1);
                                }

                                foreach (DataRow drItemSubLv1 in dtItemSubLv1.Rows)
                                {
                                    rowLv1 = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtItemSubLv1.Columns)
                                    {
                                        rowLv1.Add(col.ColumnName, drItemSubLv1[col]);
                                    }
                                    rowsLv1.Add(rowLv1);
                                }
                                // return ;
                                Response.Write(serializeItemSub.Serialize(rowsLv1).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region itemsublv2
                            case "itemsublv2":
                                System.Web.Script.Serialization.JavaScriptSerializer serializeItemSublv2 = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtItemSubLv2 = new DataTable();
                                List<Dictionary<string, object>> rowsLv2 = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowLv2;
                                int sublv1 = Int32.Parse(Request.QueryString["sublv1"]);
                                var queryItemSublv2 = from SubLevel2 in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                      where SubLevel2.nTSubLevel == sublv1
                                                      select new
                                                      {
                                                          SubLevel2Value = SubLevel2.nTermSubLevel2,
                                                          SubLevel2Name = SubLevel2.nTSubLevel2
                                                      };
                                if (queryItemSublv2.ToList().Count > 0)
                                {
                                    dtItemSubLv2 = fcommon.LinqToDataTable(queryItemSublv2);
                                }

                                foreach (DataRow drItemSubLv2 in dtItemSubLv2.Rows)
                                {
                                    rowLv2 = new Dictionary<string, object>();
                                    foreach (DataColumn col in dtItemSubLv2.Columns)
                                    {
                                        rowLv2.Add(col.ColumnName, drItemSubLv2[col]);
                                    }
                                    rowsLv2.Add(rowLv2);
                                }
                                // return ;
                                Response.Write(serializeItemSublv2.Serialize(rowsLv2).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region itemclass
                            case "itemclass":
                                System.Web.Script.Serialization.JavaScriptSerializer serializerItemclass = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtClass = fcommon.LinqToDataTable(_db.TClasses.Where(w => w.SchoolID == userData.CompanyID).Where(w => w.cDel == null).ToList());
                                List<Dictionary<string, object>> rowsClass = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowClass;
                                if (dtClass != null)
                                {
                                    foreach (DataRow dr in dtClass.Rows)
                                    {
                                        rowClass = new Dictionary<string, object>();
                                        foreach (DataColumn col in dtClass.Columns)
                                        {
                                            rowClass.Add(col.ColumnName, dr[col]);
                                        }
                                        rowsClass.Add(rowClass);
                                    }
                                }
                                // return ;
                                Response.Write(serializerItemclass.Serialize(rowsClass).ToString());
                                Response.End();
                                break;
                            #endregion
                            #region leaverlist
                            case "leaverlist":

                                string _id = Request.QueryString["stdID"].ToString();
                                string _typeid = "";
                                if (_id.IndexOf("E") != -1)
                                {
                                    _id = _id.Replace("E", "");
                                    _typeid = "E";
                                }
                                else
                                {
                                    _id = _id.Replace("U", "");
                                    _typeid = "U";
                                }
                                int sIDLeave = Int32.Parse(_id);
                                System.Web.Script.Serialization.JavaScriptSerializer serializerLeave = new System.Web.Script.Serialization.JavaScriptSerializer();
                                string SQL = "SELECT * FROM TLeave WHERE sID = " + sIDLeave + " AND cDel IS NULL AND cTypeID = '" + _typeid + "' AND SchoolID = " + userData.CompanyID;
                                //DataTable dtLeave = fcommon.LinqToDataTable();
                                //List<Dictionary<string, object>> rowsLeave = new List<Dictionary<string, object>>();
                                //Dictionary<string, object> rowLeave;
                                //if (dtLeave != null)
                                //{
                                //    foreach (DataRow dr in dtLeave.Rows)
                                //    {
                                //        rowLeave = new Dictionary<string, object>();
                                //        foreach (DataColumn col in dtLeave.Columns)
                                //        {
                                //            rowLeave.Add(col.ColumnName, dr[col]);
                                //        }
                                //        rowsLeave.Add(rowLeave);
                                //    }
                                //}
                                // return ;
                                //Response.Write(serializerLeave.Serialize(rowsLeave).ToString());
                                //Response.End();
                                break;
                            #endregion
                            #region allyear
                            case "allyear":
                                System.Web.Script.Serialization.JavaScriptSerializer serializerAllYear = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtAllYear = fcommon.LinqToDataTable(_db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.cDel == false).Where(w => w.YearStatus == "1"));
                                List<Dictionary<string, object>> rowSYear = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowYear;
                                if (dtAllYear != null && dtAllYear.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtAllYear.Rows)
                                    {
                                        rowYear = new Dictionary<string, object>();
                                        foreach (DataColumn col in dtAllYear.Columns)
                                        {
                                            rowYear.Add(col.ColumnName, dr[col]);
                                        }
                                        rowSYear.Add(rowYear);
                                    }
                                }
                                else
                                {
                                    var resultTempYear = from years in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                                         group years by years.LogDate.Value.Year into g
                                                         select new { Year = g.Key };

                                    foreach (var fetchResult in resultTempYear)
                                    {
                                        rowYear = new Dictionary<string, object>();
                                        rowYear.Add("nYear", fetchResult.Year);
                                        rowYear.Add("numberYear", fetchResult.Year + 543);
                                        rowSYear.Add(rowYear);
                                    }
                                }
                                // return ;
                                Response.Write(serializerAllYear.Serialize(rowSYear).ToString());
                                Response.End();

                                break;
                            #endregion
                            #region allsublv2
                            case "allsublv2":
                                int allnYear = Int32.Parse(Request.QueryString["nYear"].ToString());
                                System.Web.Script.Serialization.JavaScriptSerializer serializerAllSubLV2 = new System.Web.Script.Serialization.JavaScriptSerializer();
                                DataTable dtAllSUbLV2 = new DataTable();

                                if (allnYear != 0)
                                {
                                    var queryAllSubLV2 = from Term in _db.TTerms.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                        .Where(term => term.nYear == allnYear).DefaultIfEmpty()
                                                         from TermSUBLV2 in _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID)
                                                         .Where(sublv2 => sublv2.nTerm == Term.nTerm).DefaultIfEmpty()
                                                         from TermSub in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                                         .Where(sublv => sublv.nTSubLevel == TermSUBLV2.nTSubLevel).DefaultIfEmpty()
                                                         select new
                                                         {
                                                             SubLevelID = TermSUBLV2.nTermSubLevel2,
                                                             SubLevelName = TermSub.SubLevel,
                                                             SubLevelNameLV2 = TermSUBLV2.nTSubLevel2
                                                         };
                                    dtAllSUbLV2 = fcommon.LinqToDataTable(queryAllSubLV2);
                                }

                                List<Dictionary<string, object>> rowSSubLV2 = new List<Dictionary<string, object>>();
                                Dictionary<string, object> rowColSubLv2;
                                if (dtAllSUbLV2 != null)
                                {
                                    foreach (DataRow dr in dtAllSUbLV2.Rows)
                                    {
                                        rowColSubLv2 = new Dictionary<string, object>();
                                        foreach (DataColumn col in dtAllSUbLV2.Columns)
                                        {
                                            rowColSubLv2.Add(col.ColumnName, dr[col]);
                                        }
                                        rowSSubLV2.Add(rowColSubLv2);
                                    }
                                }
                                // return ;
                                Response.Write(serializerAllSubLV2.Serialize(rowSSubLV2).ToString());
                                Response.End();

                                break;
                            #endregion
                            #region reportteacher
                            case "reportteacher":
                                string strResponse;
                                int reportTeacherYear = Int32.Parse(Request.QueryString["years"].ToString());
                                DataTable dtReportTeacher = new DataTable();// fcommon.LinqToDataTable(_db.TLogUserTimeScans.Where(w=> w.nYear==reportTeacherYear));
                                DataTable dtReportTeacher2 = new DataTable();
                                DataTable dtReportTeacher3 = new DataTable();

                                try
                                {
                                    var queryReportTeacher = from Logs in _db.TLogUserTimeScans
                                                             join user in _db.TUser
                                                             on Logs.sID equals user.sID
                                                             where Logs.SchoolID == userData.CompanyID && Logs.nYear == reportTeacherYear && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                                             && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                             orderby Logs.nLogScanID
                                                             select Logs
                                                             ;
                                    if (queryReportTeacher.ToList().Count > 0)
                                    {
                                        dtReportTeacher = fcommon.LinqToDataTable(queryReportTeacher);
                                    }

                                    var queryReportTeacher2 = from Logs in _db.TLogUserTimeScans
                                                              join user in _db.TUser
                                                              on Logs.sID equals user.sID
                                                              where Logs.SchoolID == userData.CompanyID &&
                                                              Logs.nYear == reportTeacherYear && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                                              && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                              orderby Logs.nLogScanID
                                                              select Logs;
                                    if (queryReportTeacher2.ToList().Count > 0)
                                    {
                                        dtReportTeacher2 = fcommon.LinqToDataTable(queryReportTeacher2);
                                    }

                                    var queryReportTeacher3 = from Logs in _db.TLogUserTimeScans
                                                              join user in _db.TUser
                                                              on Logs.sID equals user.sID
                                                              where Logs.SchoolID == userData.CompanyID &&
                                                              Logs.nYear == reportTeacherYear && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                                              && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                              orderby Logs.nLogScanID
                                                              select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                    //                         w.nYear == reportTeacherYear && w.LogType == "0" && w.LogScanStatus == "5").AsQueryable().ToList()
                                    //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "1").AsQueryable().ToList()
                                    //                          on Logs.sID equals user.sID
                                    //                          orderby Logs.nLogScanID
                                    //                          select Logs;


                                    if (queryReportTeacher3.ToList().Count > 0)
                                    {
                                        dtReportTeacher3 = fcommon.LinqToDataTable(queryReportTeacher3);
                                    }



                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr1Month = new List<string>();
                                    List<string> arr1Count = new List<string>();
                                    List<string> arr1Type = new List<string>();

                                    List<string> arr2Month = new List<string>();
                                    List<string> arr2Count = new List<string>();
                                    List<string> arr2Type = new List<string>();

                                    List<string> arr3Month = new List<string>();
                                    List<string> arr3Count = new List<string>();
                                    List<string> arr3Type = new List<string>();
                                    int rowsMonth = 0;

                                    foreach (DataRow DR in dtReportTeacher.Rows)
                                    {
                                        List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                arr2Count[rowsMonth] = "0";
                                                arr3Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr1Month.Add(dateLogs[0]);
                                                arr1Count.Add("1");
                                                arr1Type.Add("0");
                                                arr2Month.Add(dateLogs[0]);
                                                arr2Count.Add("0");
                                                arr2Type.Add("1");
                                                arr3Month.Add(dateLogs[0]);
                                                arr3Count.Add("0");
                                                arr3Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr1Month.Add(dateLogs[0]);
                                            arr1Count.Add("1");
                                            arr1Type.Add("1");
                                            arr2Month.Add(dateLogs[0]);
                                            arr2Count.Add("0");
                                            arr2Type.Add("1");
                                            arr3Month.Add(dateLogs[0]);
                                            arr3Count.Add("0");
                                            arr3Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportTeacher2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr2Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr2Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportTeacher3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr3Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr3Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType1 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "[{\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr1Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr1Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType1 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType2 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr2Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr2Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType2 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType3 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr3Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr3Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType3 = strCount3 + "]}]";
                                    strResponse = strType1 + strType2 + strType3;

                                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                                {
                                    strResponse = "error";
                                }



                                try
                                {
                                    dtReportTeacher = new DataTable();
                                    dtReportTeacher2 = new DataTable();
                                    dtReportTeacher3 = new DataTable();
                                    var queryReportTeacher = from Logs in _db.TLogUserTimeScans
                                                             join user in _db.TUser
                                                             on Logs.sID equals user.sID
                                                             where Logs.SchoolID == userData.CompanyID &&
                                                             Logs.nYear == reportTeacherYear && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                                             && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                             orderby Logs.nLogScanID
                                                             select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                    //                         w.nYear == reportTeacherYear && w.LogType == "1" && w.LogScanStatus == "0").AsQueryable().ToList()
                                    //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "1").AsQueryable().ToList()
                                    //                         on Logs.sID equals user.sID
                                    //                         orderby Logs.nLogScanID
                                    //                         select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                         where Logs.nYear == reportTeacherYear
                                    //                        && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                    //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                         on Logs.sID equals user.sID
                                    //                         where user.cType == "1"
                                    //                         orderby Logs.nLogScanID
                                    //                         select Logs;

                                    if (queryReportTeacher.ToList().Count > 0)
                                    {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        dtReportTeacher = fcommon.LinqToDataTable(queryReportTeacher);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                    }

                                    var queryReportTeacher2 = from Logs in _db.TLogUserTimeScans
                                                              join user in _db.TUser
                                                              on Logs.sID equals user.sID
                                                              where Logs.SchoolID == userData.CompanyID &&
                                                              Logs.nYear == reportTeacherYear && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                                              && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                              orderby Logs.nLogScanID
                                                              select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                    //                     w.nYear == reportTeacherYear && w.LogType == "1" && w.LogScanStatus == "2").AsQueryable().ToList()
                                    //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "1").AsQueryable().ToList()
                                    //                      on Logs.sID equals user.sID
                                    //                      orderby Logs.nLogScanID
                                    //                      select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                      where Logs.nYear == reportTeacherYear
                                    //                     && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                    //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                      on Logs.sID equals user.sID
                                    //                      where user.cType == "1"
                                    //                      orderby Logs.nLogScanID
                                    //                      select Logs;

                                    if (queryReportTeacher2.ToList().Count > 0)
                                    {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        dtReportTeacher2 = fcommon.LinqToDataTable(queryReportTeacher2);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                    }

                                    var queryReportTeacher3 = from Logs in _db.TLogUserTimeScans
                                                              join user in _db.TUser
                                                              on Logs.sID equals user.sID
                                                              where Logs.SchoolID == userData.CompanyID &&
                                                              Logs.nYear == reportTeacherYear && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                                              && user.SchoolID == userData.CompanyID && user.cType == "1"
                                                              orderby Logs.nLogScanID
                                                              select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                    //                         w.nYear == reportTeacherYear && w.LogType == "1" && w.LogScanStatus == "3").AsQueryable().ToList()
                                    //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "1").AsQueryable().ToList()
                                    //                          on Logs.sID equals user.sID
                                    //                          orderby Logs.nLogScanID
                                    //                          select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                          where Logs.nYear == reportTeacherYear
                                    //                         && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                    //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                    //                          on Logs.sID equals user.sID
                                    //                          where user.cType == "1"
                                    //                          orderby Logs.nLogScanID
                                    //                          select Logs;

                                    if (queryReportTeacher3.ToList().Count > 0)
                                    {
                                        dtReportTeacher3 = fcommon.LinqToDataTable(queryReportTeacher3);
                                    }



                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr11Month = new List<string>();
                                    List<string> arr11Count = new List<string>();
                                    List<string> arr11Type = new List<string>();

                                    List<string> arr22Month = new List<string>();
                                    List<string> arr22Count = new List<string>();
                                    List<string> arr22Type = new List<string>();

                                    List<string> arr33Month = new List<string>();
                                    List<string> arr33Count = new List<string>();
                                    List<string> arr33Type = new List<string>();
                                    int rowsMonth = 0;

                                    foreach (DataRow DR in dtReportTeacher.Rows)
                                    {
                                        List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                arr22Count[rowsMonth] = "0";
                                                arr33Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr11Month.Add(dateLogs[0]);
                                                arr11Count.Add("1");
                                                arr11Type.Add("0");
                                                arr22Month.Add(dateLogs[0]);
                                                arr22Count.Add("0");
                                                arr22Type.Add("1");
                                                arr33Month.Add(dateLogs[0]);
                                                arr33Count.Add("0");
                                                arr33Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr11Month.Add(dateLogs[0]);
                                            arr11Count.Add("1");
                                            arr11Type.Add("1");
                                            arr22Month.Add(dateLogs[0]);
                                            arr22Count.Add("0");
                                            arr22Type.Add("1");
                                            arr33Month.Add(dateLogs[0]);
                                            arr33Count.Add("0");
                                            arr33Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportTeacher2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr22Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr22Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportTeacher3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr33Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr33Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType11 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "split[{\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr11Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr11Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType11 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType22 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr22Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr22Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType22 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType33 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr33Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr33Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType33 = strCount3 + "]}]";
                                    strResponse += strType11 + strType22 + strType33;

                                }
                                catch
                                {
                                    strResponse = "error";
                                }
                                Response.Write(strResponse);
                                Response.End();



                                break;
                            #endregion
                            #region reportstudent
                            case "reportstudent":
                                string strResponseStd;
                                int reportStudentYear = Int32.Parse(Request.QueryString["years"].ToString());
                                int reportStudentSub = Int32.Parse(Request.QueryString["sublv2"].ToString());
                                DataTable dtReportStudent = new DataTable();
                                DataTable dtReportStudent2 = new DataTable();
                                DataTable dtReportStudent3 = new DataTable();
                                if (reportStudentSub != 0)
                                {
                                    try
                                    {
                                        var queryReportStudent = from Logs in _db.TLogUserTimeScans
                                                                 join user in _db.TUser on Logs.sID equals user.sID
                                                                 join sub in _db.TStudentLevels
                                                                  on user.sID equals sub.sID
                                                                 where Logs.SchoolID == userData.CompanyID &&
                                                                 Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                                                 && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                 && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                         w.nYear == reportStudentYear && w.LogType == "0" && w.LogScanStatus == "0").AsQueryable().ToList()
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                          on user.sID equals sub.sID
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         where Logs.nYear == reportStudentYear
                                        //                        && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         where user.cType == "0"
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         on user.sID equals sub.sID
                                        //                         where sub.nTermSubLevel2 == reportStudentSub
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs;

                                        if (queryReportStudent.ToList().Count > 0)
                                        {
                                            dtReportStudent = fcommon.LinqToDataTable(queryReportStudent);
                                        }

                                        var queryReportStudent2 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                        w.nYear == reportStudentYear && w.LogType == "0" && w.LogScanStatus == "1").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on user.sID equals sub.sID
                                        //                      where sub.nTermSubLevel2 == reportStudentSub
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;

                                        if (queryReportStudent.ToList().Count > 0)
                                        {
                                            dtReportStudent2 = fcommon.LinqToDataTable(queryReportStudent);
                                        }

                                        var queryReportStudent3 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                       w.nYear == reportStudentYear && w.LogType == "0" && w.LogScanStatus == "5").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                          where Logs.nYear == reportStudentYear
                                        //                         && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          where user.cType == "0"
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         on user.sID equals sub.sID
                                        //                          where sub.nTermSubLevel2 == reportStudentSub
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        if (queryReportStudent3.ToList().Count > 0)
                                        {
                                            dtReportStudent3 = fcommon.LinqToDataTable(queryReportStudent3);
                                        }



                                        string tempMonth = String.Empty;
                                        int currentMonthCount = 0;

                                        List<string> arr1Month = new List<string>();
                                        List<string> arr1Count = new List<string>();
                                        List<string> arr1Type = new List<string>();

                                        List<string> arr2Month = new List<string>();
                                        List<string> arr2Count = new List<string>();
                                        List<string> arr2Type = new List<string>();

                                        List<string> arr3Month = new List<string>();
                                        List<string> arr3Count = new List<string>();
                                        List<string> arr3Type = new List<string>();
                                        int rowsMonth = 0;

                                        foreach (DataRow DR in dtReportStudent.Rows)
                                        {
                                            List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs[0])
                                                {
                                                    currentMonthCount++;
                                                    arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                    arr2Count[rowsMonth] = "0";
                                                    arr3Count[rowsMonth] = "0";
                                                }
                                                else
                                                {
                                                    currentMonthCount = 1;
                                                    rowsMonth++;
                                                    arr1Month.Add(dateLogs[0]);
                                                    arr1Count.Add("1");
                                                    arr1Type.Add("0");
                                                    arr2Month.Add(dateLogs[0]);
                                                    arr2Count.Add("0");
                                                    arr2Type.Add("1");
                                                    arr3Month.Add(dateLogs[0]);
                                                    arr3Count.Add("0");
                                                    arr3Type.Add("1");
                                                }
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs[0];
                                                arr1Month.Add(dateLogs[0]);
                                                arr1Count.Add("1");
                                                arr1Type.Add("1");
                                                arr2Month.Add(dateLogs[0]);
                                                arr2Count.Add("0");
                                                arr2Type.Add("1");
                                                arr3Month.Add(dateLogs[0]);
                                                arr3Count.Add("0");
                                                arr3Type.Add("1");
                                            }
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR2 in dtReportStudent2.Rows)
                                        {
                                            List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr2Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr2Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr2Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }


                                            tempMonth = dateLogs2[0];
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR3 in dtReportStudent3.Rows)
                                        {
                                            List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr3Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr3Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr3Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }
                                            tempMonth = dateLogs2[0];
                                        }



                                        rowsMonth = 0;
                                        string strType1 = "";


                                        //1
                                        string strMonth = "";
                                        string strCount = "";
                                        strMonth = "[{\"Month\":[";
                                        strCount = ",\"Count\":[";
                                        int roundLoop = 1;

                                        foreach (string val in arr1Month)
                                        {
                                            if (roundLoop != 1)
                                            {
                                                strMonth += ",";
                                                strCount += ",";
                                            }

                                            strMonth += "\"" + monthConv(val) + "\"";
                                            strCount += arr1Count[rowsMonth];
                                            roundLoop++;
                                            rowsMonth++;
                                        }

                                        strType1 = strMonth + "]" + strCount + "]";//}]";

                                        rowsMonth = 0;
                                        string strType2 = "";
                                        //2
                                        string strCount2 = "";
                                        strCount2 = ",\"Count2\":[";
                                        int roundLoop2 = 1;

                                        foreach (string val in arr2Month)
                                        {
                                            if (roundLoop2 != 1)
                                            {
                                                strCount2 += ",";
                                            }

                                            strCount2 += arr2Count[rowsMonth];
                                            roundLoop2++;
                                            rowsMonth++;
                                        }

                                        strType2 = strCount2 + "]";//}]";

                                        rowsMonth = 0;
                                        string strType3 = "";
                                        //2
                                        string strCount3 = "";
                                        strCount3 = ",\"Count3\":[";
                                        int roundLoop3 = 1;

                                        foreach (string val in arr3Month)
                                        {
                                            if (roundLoop3 != 1)
                                            {
                                                strCount3 += ",";
                                            }

                                            strCount3 += arr3Count[rowsMonth];
                                            roundLoop3++;
                                            rowsMonth++;
                                        }

                                        strType3 = strCount3 + "]}]";
                                        strResponseStd = strType1 + strType2 + strType3;

                                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                                    catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                                    {
                                        strResponseStd = "error";
                                    }



                                    try
                                    {
                                        dtReportStudent = new DataTable();
                                        dtReportStudent2 = new DataTable();
                                        dtReportStudent3 = new DataTable();
                                        var queryReportStudent = from Logs in _db.TLogUserTimeScans
                                                                 join user in _db.TUser on Logs.sID equals user.sID
                                                                 join sub in _db.TStudentLevels
                                                                  on user.sID equals sub.sID
                                                                 where Logs.SchoolID == userData.CompanyID &&
                                                                 Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                                                 && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                 && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                       w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "0").AsQueryable().ToList()
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                          on user.sID equals sub.sID
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         where Logs.nYear == reportStudentYear
                                        //                        && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         where user.cType == "0"
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                         on user.sID equals sub.sID
                                        //                         where sub.nTermSubLevel2 == reportStudentSub
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs
                                        //                         ;
                                        if (queryReportStudent.ToList().Count > 0)
                                        {
                                            dtReportStudent = fcommon.LinqToDataTable(queryReportStudent);
                                        }

                                        var queryReportStudent2 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "2").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on user.sID equals sub.sID
                                        //                      where sub.nTermSubLevel2 == reportStudentSub
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;
                                        if (queryReportStudent2.ToList().Count > 0)
                                        {
                                            dtReportStudent2 = fcommon.LinqToDataTable(queryReportStudent2);
                                        }

                                        var queryReportStudent3 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "3").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == reportStudentSub).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                          where Logs.nYear == reportStudentYear
                                        //                         && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          where user.cType == "0"
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                          on user.sID equals sub.sID
                                        //                          where sub.nTermSubLevel2 == reportStudentSub
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        if (queryReportStudent3.ToList().Count > 0)
                                        {
                                            dtReportStudent3 = fcommon.LinqToDataTable(queryReportStudent3);
                                        }



                                        string tempMonth = String.Empty;
                                        int currentMonthCount = 0;

                                        List<string> arr11Month = new List<string>();
                                        List<string> arr11Count = new List<string>();
                                        List<string> arr11Type = new List<string>();

                                        List<string> arr22Month = new List<string>();
                                        List<string> arr22Count = new List<string>();
                                        List<string> arr22Type = new List<string>();

                                        List<string> arr33Month = new List<string>();
                                        List<string> arr33Count = new List<string>();
                                        List<string> arr33Type = new List<string>();
                                        int rowsMonth = 0;

                                        foreach (DataRow DR in dtReportStudent.Rows)
                                        {
                                            List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs[0])
                                                {
                                                    currentMonthCount++;
                                                    arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                    arr22Count[rowsMonth] = "0";
                                                    arr33Count[rowsMonth] = "0";
                                                }
                                                else
                                                {
                                                    currentMonthCount = 1;
                                                    rowsMonth++;
                                                    arr11Month.Add(dateLogs[0]);
                                                    arr11Count.Add("1");
                                                    arr11Type.Add("0");
                                                    arr22Month.Add(dateLogs[0]);
                                                    arr22Count.Add("0");
                                                    arr22Type.Add("1");
                                                    arr33Month.Add(dateLogs[0]);
                                                    arr33Count.Add("0");
                                                    arr33Type.Add("1");
                                                }
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs[0];
                                                arr11Month.Add(dateLogs[0]);
                                                arr11Count.Add("1");
                                                arr11Type.Add("1");
                                                arr22Month.Add(dateLogs[0]);
                                                arr22Count.Add("0");
                                                arr22Type.Add("1");
                                                arr33Month.Add(dateLogs[0]);
                                                arr33Count.Add("0");
                                                arr33Type.Add("1");
                                            }
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR2 in dtReportStudent2.Rows)
                                        {
                                            List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr22Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr22Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr22Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }


                                            tempMonth = dateLogs2[0];
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR3 in dtReportStudent3.Rows)
                                        {
                                            List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr33Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr33Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr33Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }
                                            tempMonth = dateLogs2[0];
                                        }



                                        rowsMonth = 0;
                                        string strType11 = "";


                                        //1
                                        string strMonth = "";
                                        string strCount = "";
                                        strMonth = "split[{\"Month\":[";
                                        strCount = ",\"Count\":[";
                                        int roundLoop = 1;

                                        foreach (string val in arr11Month)
                                        {
                                            if (roundLoop != 1)
                                            {
                                                strMonth += ",";
                                                strCount += ",";
                                            }

                                            strMonth += "\"" + monthConv(val) + "\"";
                                            strCount += arr11Count[rowsMonth];
                                            roundLoop++;
                                            rowsMonth++;
                                        }

                                        strType11 = strMonth + "]" + strCount + "]";//}]";

                                        rowsMonth = 0;
                                        string strType22 = "";
                                        //2
                                        string strCount2 = "";
                                        strCount2 = ",\"Count2\":[";
                                        int roundLoop2 = 1;

                                        foreach (string val in arr22Month)
                                        {
                                            if (roundLoop2 != 1)
                                            {
                                                strCount2 += ",";
                                            }

                                            strCount2 += arr22Count[rowsMonth];
                                            roundLoop2++;
                                            rowsMonth++;
                                        }

                                        strType22 = strCount2 + "]";//}]";

                                        rowsMonth = 0;
                                        string strType33 = "";
                                        //2
                                        string strCount3 = "";
                                        strCount3 = ",\"Count3\":[";
                                        int roundLoop3 = 1;

                                        foreach (string val in arr33Month)
                                        {
                                            if (roundLoop3 != 1)
                                            {
                                                strCount3 += ",";
                                            }

                                            strCount3 += arr33Count[rowsMonth];
                                            roundLoop3++;
                                            rowsMonth++;
                                        }

                                        strType33 = strCount3 + "]}]";
                                        strResponseStd += strType11 + strType22 + strType33;

                                    }
                                    catch
                                    {
                                        strResponse = "error";
                                    }
                                    Response.Write(strResponseStd);
                                }
                                else
                                {
                                    //all sub
                                    try
                                    {
                                        var queryReportStudent = from Logs in _db.TLogUserTimeScans
                                                                 join user in _db.TUser on Logs.sID equals user.sID
                                                                 join sub in _db.TStudentLevels
                                                                  on user.sID equals sub.sID
                                                                 where Logs.SchoolID == userData.CompanyID &&
                                                                 Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                                                 && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                 && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "0" && w.LogScanStatus == "0").AsQueryable().ToList()
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID ).AsQueryable().ToList()
                                        //                          on user.sID equals sub.sID
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     where Logs.nYear == reportStudentYear
                                        //                    && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                        //                     join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on Logs.sID equals user.sID
                                        //                     where user.cType == "0"
                                        //                     join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on user.sID equals sub.sID
                                        //                     orderby Logs.nLogScanID
                                        //                     select Logs
                                        //                     ;
                                        if (queryReportStudent.ToList().Count > 0)
                                        {
                                            dtReportStudent = fcommon.LinqToDataTable(queryReportStudent);
                                        }

                                        var queryReportStudent2 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "0" && w.LogScanStatus == "1").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID ).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on user.sID equals sub.sID
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;
                                        if (queryReportStudent.ToList().Count > 0)
                                        {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                            dtReportStudent2 = fcommon.LinqToDataTable(queryReportStudent);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        }

                                        var queryReportStudent3 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;



                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on user.sID equals sub.sID
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;

                                        if (queryReportStudent3.ToList().Count > 0)
                                        {
                                            dtReportStudent3 = fcommon.LinqToDataTable(queryReportStudent3);
                                        }



                                        string tempMonth = String.Empty;
                                        int currentMonthCount = 0;

                                        List<string> arr1Month = new List<string>();
                                        List<string> arr1Count = new List<string>();
                                        List<string> arr1Type = new List<string>();

                                        List<string> arr2Month = new List<string>();
                                        List<string> arr2Count = new List<string>();
                                        List<string> arr2Type = new List<string>();

                                        List<string> arr3Month = new List<string>();
                                        List<string> arr3Count = new List<string>();
                                        List<string> arr3Type = new List<string>();
                                        int rowsMonth = 0;

                                        foreach (DataRow DR in dtReportStudent.Rows)
                                        {
                                            List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs[0])
                                                {
                                                    currentMonthCount++;
                                                    arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                    arr2Count[rowsMonth] = "0";
                                                    arr3Count[rowsMonth] = "0";
                                                }
                                                else
                                                {
                                                    currentMonthCount = 1;
                                                    rowsMonth++;
                                                    arr1Month.Add(dateLogs[0]);
                                                    arr1Count.Add("1");
                                                    arr1Type.Add("0");
                                                    arr2Month.Add(dateLogs[0]);
                                                    arr2Count.Add("0");
                                                    arr2Type.Add("1");
                                                    arr3Month.Add(dateLogs[0]);
                                                    arr3Count.Add("0");
                                                    arr3Type.Add("1");
                                                }
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs[0];
                                                arr1Month.Add(dateLogs[0]);
                                                arr1Count.Add("1");
                                                arr1Type.Add("1");
                                                arr2Month.Add(dateLogs[0]);
                                                arr2Count.Add("0");
                                                arr2Type.Add("1");
                                                arr3Month.Add(dateLogs[0]);
                                                arr3Count.Add("0");
                                                arr3Type.Add("1");
                                            }
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR2 in dtReportStudent2.Rows)
                                        {
                                            List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr2Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr2Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr2Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }


                                            tempMonth = dateLogs2[0];
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR3 in dtReportStudent3.Rows)
                                        {
                                            List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr3Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr3Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr3Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }
                                            tempMonth = dateLogs2[0];
                                        }



                                        rowsMonth = 0;
                                        string strType1 = "";


                                        //1
                                        string strMonth = "";
                                        string strCount = "";
                                        strMonth = "[{\"Month\":[";
                                        strCount = ",\"Count\":[";
                                        int roundLoop = 1;

                                        foreach (string val in arr1Month)
                                        {
                                            if (roundLoop != 1)
                                            {
                                                strMonth += ",";
                                                strCount += ",";
                                            }

                                            strMonth += "\"" + monthConv(val) + "\"";
                                            strCount += arr1Count[rowsMonth];
                                            roundLoop++;
                                            rowsMonth++;
                                        }

                                        strType1 = strMonth + "]" + strCount + "]";//}]";

                                        rowsMonth = 0;
                                        string strType2 = "";
                                        //2
                                        string strCount2 = "";
                                        strCount2 = ",\"Count2\":[";
                                        int roundLoop2 = 1;

                                        foreach (string val in arr2Month)
                                        {
                                            if (roundLoop2 != 1)
                                            {
                                                strCount2 += ",";
                                            }

                                            strCount2 += arr2Count[rowsMonth];
                                            roundLoop2++;
                                            rowsMonth++;
                                        }

                                        strType2 = strCount2 + "]";//}]";

                                        rowsMonth = 0;
                                        string strType3 = "";
                                        //2
                                        string strCount3 = "";
                                        strCount3 = ",\"Count3\":[";
                                        int roundLoop3 = 1;

                                        foreach (string val in arr3Month)
                                        {
                                            if (roundLoop3 != 1)
                                            {
                                                strCount3 += ",";
                                            }

                                            strCount3 += arr3Count[rowsMonth];
                                            roundLoop3++;
                                            rowsMonth++;
                                        }

                                        strType3 = strCount3 + "]}]";
                                        strResponseStd = strType1 + strType2 + strType3;

                                    }
                                    catch
                                    {
                                        strResponseStd = "error";
                                    }



                                    try
                                    {
                                        dtReportStudent = new DataTable();
                                        dtReportStudent2 = new DataTable();
                                        dtReportStudent3 = new DataTable();
                                        var queryReportStudent = from Logs in _db.TLogUserTimeScans
                                                                 join user in _db.TUser on Logs.sID equals user.sID
                                                                 join sub in _db.TStudentLevels
                                                                  on user.sID equals sub.sID
                                                                 where Logs.SchoolID == userData.CompanyID &&
                                                                 Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                                                 && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                 && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;



                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "0").AsQueryable().ToList()
                                        //                         join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                         on Logs.sID equals user.sID
                                        //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                        //                          on user.sID equals sub.sID
                                        //                         orderby Logs.nLogScanID
                                        //                         select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     where Logs.nYear == reportStudentYear
                                        //                    && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                        //                     join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on Logs.sID equals user.sID
                                        //                     where user.cType == "0"
                                        //                     join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                     on user.sID equals sub.sID
                                        //                     orderby Logs.nLogScanID
                                        //                     select Logs ;
                                        if (queryReportStudent.ToList().Count > 0)
                                        {
                                            dtReportStudent = fcommon.LinqToDataTable(queryReportStudent);
                                        }

                                        var queryReportStudent2 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "2").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on user.sID equals sub.sID
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;
                                        if (queryReportStudent2.ToList().Count > 0)
                                        {
                                            dtReportStudent2 = fcommon.LinqToDataTable(queryReportStudent2);
                                        }

                                        var queryReportStudent3 = from Logs in _db.TLogUserTimeScans
                                                                  join user in _db.TUser on Logs.sID equals user.sID
                                                                  join sub in _db.TStudentLevels
                                                                   on user.sID equals sub.sID
                                                                  where Logs.SchoolID == userData.CompanyID &&
                                                                  Logs.nYear == reportStudentYear && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                                                  && user.SchoolID == userData.CompanyID && user.cType == "0"
                                                                  && sub.SchoolID == userData.CompanyID && sub.nTermSubLevel2 == reportStudentSub
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;


                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID &&
                                        //                      w.nYear == reportStudentYear && w.LogType == "1" && w.LogScanStatus == "3").AsQueryable().ToList()
                                        //                          join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID && w.cType == "0").AsQueryable().ToList()
                                        //                          on Logs.sID equals user.sID
                                        //                          join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).AsQueryable().ToList()
                                        //                           on user.sID equals sub.sID
                                        //                          orderby Logs.nLogScanID
                                        //                          select Logs;

                                        //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      where Logs.nYear == reportStudentYear
                                        //                     && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                        //                      join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on Logs.sID equals user.sID
                                        //                      where user.cType == "0"
                                        //                      join sub in _db.TStudentLevels.Where(w => w.SchoolID == userData.CompanyID).ToList()
                                        //                      on user.sID equals sub.sID
                                        //                      orderby Logs.nLogScanID
                                        //                      select Logs;

                                        if (queryReportStudent3.ToList().Count > 0)
                                        {
                                            dtReportStudent3 = fcommon.LinqToDataTable(queryReportStudent3);
                                        }



                                        string tempMonth = String.Empty;
                                        int currentMonthCount = 0;

                                        List<string> arr11Month = new List<string>();
                                        List<string> arr11Count = new List<string>();
                                        List<string> arr11Type = new List<string>();

                                        List<string> arr22Month = new List<string>();
                                        List<string> arr22Count = new List<string>();
                                        List<string> arr22Type = new List<string>();

                                        List<string> arr33Month = new List<string>();
                                        List<string> arr33Count = new List<string>();
                                        List<string> arr33Type = new List<string>();
                                        int rowsMonth = 0;

                                        foreach (DataRow DR in dtReportStudent.Rows)
                                        {
                                            List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs[0])
                                                {
                                                    currentMonthCount++;
                                                    arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                    arr22Count[rowsMonth] = "0";
                                                    arr33Count[rowsMonth] = "0";
                                                }
                                                else
                                                {
                                                    currentMonthCount = 1;
                                                    rowsMonth++;
                                                    arr11Month.Add(dateLogs[0]);
                                                    arr11Count.Add("1");
                                                    arr11Type.Add("0");
                                                    arr22Month.Add(dateLogs[0]);
                                                    arr22Count.Add("0");
                                                    arr22Type.Add("1");
                                                    arr33Month.Add(dateLogs[0]);
                                                    arr33Count.Add("0");
                                                    arr33Type.Add("1");
                                                }
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs[0];
                                                arr11Month.Add(dateLogs[0]);
                                                arr11Count.Add("1");
                                                arr11Type.Add("1");
                                                arr22Month.Add(dateLogs[0]);
                                                arr22Count.Add("0");
                                                arr22Type.Add("1");
                                                arr33Month.Add(dateLogs[0]);
                                                arr33Count.Add("0");
                                                arr33Type.Add("1");
                                            }
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR2 in dtReportStudent2.Rows)
                                        {
                                            List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr22Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr22Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr22Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }


                                            tempMonth = dateLogs2[0];
                                        }

                                        tempMonth = String.Empty;
                                        rowsMonth = 0;
                                        currentMonthCount = 0;
                                        foreach (DataRow DR3 in dtReportStudent3.Rows)
                                        {
                                            List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                            if (!String.IsNullOrEmpty(tempMonth))
                                            {
                                                if (tempMonth == dateLogs2[0])
                                                {
                                                    currentMonthCount++;
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr33Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    //
                                                    int forcheckMonth = 0;

                                                    foreach (var valMonthCheck in arr33Month)
                                                    {
                                                        if (valMonthCheck == dateLogs2[0])
                                                        {
                                                            arr33Count[forcheckMonth] = "1";
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            forcheckMonth++;
                                                        }
                                                    }

                                                    //
                                                    currentMonthCount = 1;
                                                    tempMonth = dateLogs2[0];
                                                }
                                            }
                                            else
                                            {

                                                currentMonthCount = 1;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                            }
                                            tempMonth = dateLogs2[0];
                                        }



                                        rowsMonth = 0;
                                        string strType11 = "";


                                        //1
                                        string strMonth = "";
                                        string strCount = "";
                                        strMonth = "split[{\"Month\":[";
                                        strCount = ",\"Count\":[";
                                        int roundLoop = 1;

                                        foreach (string val in arr11Month)
                                        {
                                            if (roundLoop != 1)
                                            {
                                                strMonth += ",";
                                                strCount += ",";
                                            }

                                            strMonth += "\"" + monthConv(val) + "\"";
                                            strCount += arr11Count[rowsMonth];
                                            roundLoop++;
                                            rowsMonth++;
                                        }

                                        strType11 = strMonth + "]" + strCount + "]";//}]";

                                        rowsMonth = 0;
                                        string strType22 = "";
                                        //2
                                        string strCount2 = "";
                                        strCount2 = ",\"Count2\":[";
                                        int roundLoop2 = 1;

                                        foreach (string val in arr22Month)
                                        {
                                            if (roundLoop2 != 1)
                                            {
                                                strCount2 += ",";
                                            }

                                            strCount2 += arr22Count[rowsMonth];
                                            roundLoop2++;
                                            rowsMonth++;
                                        }

                                        strType22 = strCount2 + "]";//}]";

                                        rowsMonth = 0;
                                        string strType33 = "";
                                        //2
                                        string strCount3 = "";
                                        strCount3 = ",\"Count3\":[";
                                        int roundLoop3 = 1;

                                        foreach (string val in arr33Month)
                                        {
                                            if (roundLoop3 != 1)
                                            {
                                                strCount3 += ",";
                                            }

                                            strCount3 += arr33Count[rowsMonth];
                                            roundLoop3++;
                                            rowsMonth++;
                                        }

                                        strType33 = strCount3 + "]}]";
                                        strResponseStd += strType11 + strType22 + strType33;

                                    }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                                    catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                                    {
                                        strResponse = "error";
                                    }
                                    Response.Write(strResponseStd);
                                }

                                Response.End();
                                break;
                            #endregion

                            case "reportlearnstudent":
                                //string strResponseLearnStd;
                                //int reportLearnStudentYear = Int32.Parse(Request.QueryString["years"].ToString());
                                //int reportLearnStudentSub = Int32.Parse(Request.QueryString["sublv2"].ToString());
                                //DataTable dtReportLearnStudent = new DataTable();
                                //DataTable dtReportLearnStudent2 = new DataTable();
                                //DataTable dtReportLearnStudent3 = new DataTable();
                                //if (reportLearnStudentSub != 0)
                                //{
                                //    try
                                //    {
                                //        var queryReportLearnStudent = from Logs in _db.TLogLearnTimeScan
                                //                                      where Logs.nYear == reportLearnStudentYear
                                //                                     && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "0"
                                //                                      join user in _db.TUser
                                //                                      on Logs.sID equals user.sID
                                //                                      where user.cType == "0"
                                //                                      join sub in _db.TStudentLevel
                                //                                      on user.sID equals sub.sID
                                //                                      where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                      orderby Logs.nLogLearnScanID
                                //                                      select Logs
                                //                                 ;
                                //        if (queryReportLearnStudent.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent = fcommon.LinqToDataTable(queryReportLearnStudent);
                                //        }

                                //        var queryReportLearnStudent2 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "1"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       join sub in _db.TStudentLevel
                                //                                      on user.sID equals sub.sID
                                //                                       where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;
                                //        if (queryReportLearnStudent2.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent2 = fcommon.LinqToDataTable(queryReportLearnStudent2);
                                //        }

                                //        var queryReportLearnStudent3 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "5"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       join sub in _db.TStudentLevel
                                //                                      on user.sID equals sub.sID
                                //                                       where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;

                                //        if (queryReportLearnStudent3.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent3 = fcommon.LinqToDataTable(queryReportLearnStudent3);
                                //        }



                                //        string tempMonth = String.Empty;
                                //        int currentMonthCount = 0;

                                //        List<string> arr1Month = new List<string>();
                                //        List<string> arr1Count = new List<string>();
                                //        List<string> arr1Type = new List<string>();

                                //        List<string> arr2Month = new List<string>();
                                //        List<string> arr2Count = new List<string>();
                                //        List<string> arr2Type = new List<string>();

                                //        List<string> arr3Month = new List<string>();
                                //        List<string> arr3Count = new List<string>();
                                //        List<string> arr3Type = new List<string>();
                                //        int rowsMonth = 0;

                                //        foreach (DataRow DR in dtReportLearnStudent.Rows)
                                //        {
                                //            List<string> dateLogs = DR["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    arr1Count[rowsMonth] = currentMonthCount.ToString();
                                //                    arr2Count[rowsMonth] = "0";
                                //                    arr3Count[rowsMonth] = "0";
                                //                }
                                //                else
                                //                {
                                //                    currentMonthCount = 1;
                                //                    rowsMonth++;
                                //                    arr1Month.Add(dateLogs[0]);
                                //                    arr1Count.Add("1");
                                //                    arr1Type.Add("0");
                                //                    arr2Month.Add(dateLogs[0]);
                                //                    arr2Count.Add("0");
                                //                    arr2Type.Add("1");
                                //                    arr3Month.Add(dateLogs[0]);
                                //                    arr3Count.Add("0");
                                //                    arr3Type.Add("1");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                currentMonthCount = 1;
                                //                tempMonth = dateLogs[0];
                                //                arr1Month.Add(dateLogs[0]);
                                //                arr1Count.Add("1");
                                //                arr1Type.Add("1");
                                //                arr2Month.Add(dateLogs[0]);
                                //                arr2Count.Add("0");
                                //                arr2Type.Add("1");
                                //                arr3Month.Add(dateLogs[0]);
                                //                arr3Count.Add("0");
                                //                arr3Type.Add("1");
                                //            }
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR2 in dtReportLearnStudent2.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR2["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr2Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr2Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr2Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr2Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr2Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }


                                //            tempMonth = dateLogs2[0];
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR3 in dtReportLearnStudent3.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR3["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr3Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr3Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr3Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr3Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr3Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }
                                //            tempMonth = dateLogs2[0];
                                //        }



                                //        rowsMonth = 0;
                                //        string strType1 = "";


                                //        //1
                                //        string strMonth = "";
                                //        string strCount = "";
                                //        strMonth = "[{\"Month\":[";
                                //        strCount = ",\"Count\":[";
                                //        int roundLoop = 1;

                                //        foreach (string val in arr1Month)
                                //        {
                                //            if (roundLoop != 1)
                                //            {
                                //                strMonth += ",";
                                //                strCount += ",";
                                //            }

                                //            strMonth += "\"" + monthConv(val) + "\"";
                                //            strCount += arr1Count[rowsMonth];
                                //            roundLoop++;
                                //            rowsMonth++;
                                //        }

                                //        strType1 = strMonth + "]" + strCount + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType2 = "";
                                //        //2
                                //        string strCount2 = "";
                                //        strCount2 = ",\"Count2\":[";
                                //        int roundLoop2 = 1;

                                //        foreach (string val in arr2Month)
                                //        {
                                //            if (roundLoop2 != 1)
                                //            {
                                //                strCount2 += ",";
                                //            }

                                //            strCount2 += arr2Count[rowsMonth];
                                //            roundLoop2++;
                                //            rowsMonth++;
                                //        }

                                //        strType2 = strCount2 + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType3 = "";
                                //        //2
                                //        string strCount3 = "";
                                //        strCount3 = ",\"Count3\":[";
                                //        int roundLoop3 = 1;

                                //        foreach (string val in arr3Month)
                                //        {
                                //            if (roundLoop3 != 1)
                                //            {
                                //                strCount3 += ",";
                                //            }

                                //            strCount3 += arr3Count[rowsMonth];
                                //            roundLoop3++;
                                //            rowsMonth++;
                                //        }

                                //        strType3 = strCount3 + "]}]";
                                //        strResponseLearnStd = strType1 + strType2 + strType3;

                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        strResponseLearnStd = "error";
                                //    }



                                //    try
                                //    {
                                //        dtReportLearnStudent = new DataTable();
                                //        dtReportLearnStudent2 = new DataTable();
                                //        dtReportLearnStudent3 = new DataTable();
                                //        var queryReportLearnStudent = from Logs in _db.TLogLearnTimeScan
                                //                                      where Logs.nYear == reportLearnStudentYear
                                //                                && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "0"
                                //                                      join user in _db.TUser
                                //                                      on Logs.sID equals user.sID
                                //                                      where user.cType == "0"
                                //                                      join sub in _db.TStudentLevel
                                //                                      on user.sID equals sub.sID
                                //                                      where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                      orderby Logs.nLogLearnScanID
                                //                                      select Logs
                                //                                 ;
                                //        if (queryReportLearnStudent.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent = fcommon.LinqToDataTable(queryReportLearnStudent);
                                //        }

                                //        var queryReportLearnStudent2 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "2"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       join sub in _db.TStudentLevel
                                //                                       on user.sID equals sub.sID
                                //                                       where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;
                                //        if (queryReportLearnStudent2.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent2 = fcommon.LinqToDataTable(queryReportLearnStudent2);
                                //        }

                                //        var queryReportLearnStudent3 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "3"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       join sub in _db.TStudentLevel
                                //                                       on user.sID equals sub.sID
                                //                                       where sub.nTermSubLevel2 == reportLearnStudentSub
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;

                                //        if (queryReportLearnStudent3.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent3 = fcommon.LinqToDataTable(queryReportLearnStudent3);
                                //        }



                                //        string tempMonth = String.Empty;
                                //        int currentMonthCount = 0;

                                //        List<string> arr11Month = new List<string>();
                                //        List<string> arr11Count = new List<string>();
                                //        List<string> arr11Type = new List<string>();

                                //        List<string> arr22Month = new List<string>();
                                //        List<string> arr22Count = new List<string>();
                                //        List<string> arr22Type = new List<string>();

                                //        List<string> arr33Month = new List<string>();
                                //        List<string> arr33Count = new List<string>();
                                //        List<string> arr33Type = new List<string>();
                                //        int rowsMonth = 0;

                                //        foreach (DataRow DR in dtReportLearnStudent.Rows)
                                //        {
                                //            List<string> dateLogs = DR["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    arr11Count[rowsMonth] = currentMonthCount.ToString();
                                //                    arr22Count[rowsMonth] = "0";
                                //                    arr33Count[rowsMonth] = "0";
                                //                }
                                //                else
                                //                {
                                //                    currentMonthCount = 1;
                                //                    rowsMonth++;
                                //                    arr11Month.Add(dateLogs[0]);
                                //                    arr11Count.Add("1");
                                //                    arr11Type.Add("0");
                                //                    arr22Month.Add(dateLogs[0]);
                                //                    arr22Count.Add("0");
                                //                    arr22Type.Add("1");
                                //                    arr33Month.Add(dateLogs[0]);
                                //                    arr33Count.Add("0");
                                //                    arr33Type.Add("1");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                currentMonthCount = 1;
                                //                tempMonth = dateLogs[0];
                                //                arr11Month.Add(dateLogs[0]);
                                //                arr11Count.Add("1");
                                //                arr11Type.Add("1");
                                //                arr22Month.Add(dateLogs[0]);
                                //                arr22Count.Add("0");
                                //                arr22Type.Add("1");
                                //                arr33Month.Add(dateLogs[0]);
                                //                arr33Count.Add("0");
                                //                arr33Type.Add("1");
                                //            }
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR2 in dtReportLearnStudent2.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR2["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr22Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr22Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr22Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr22Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr22Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }


                                //            tempMonth = dateLogs2[0];
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR3 in dtReportLearnStudent3.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR3["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr33Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr33Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr33Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr33Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr33Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }
                                //            tempMonth = dateLogs2[0];
                                //        }



                                //        rowsMonth = 0;
                                //        string strType11 = "";


                                //        //1
                                //        string strMonth = "";
                                //        string strCount = "";
                                //        strMonth = "split[{\"Month\":[";
                                //        strCount = ",\"Count\":[";
                                //        int roundLoop = 1;

                                //        foreach (string val in arr11Month)
                                //        {
                                //            if (roundLoop != 1)
                                //            {
                                //                strMonth += ",";
                                //                strCount += ",";
                                //            }

                                //            strMonth += "\"" + monthConv(val) + "\"";
                                //            strCount += arr11Count[rowsMonth];
                                //            roundLoop++;
                                //            rowsMonth++;
                                //        }

                                //        strType11 = strMonth + "]" + strCount + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType22 = "";
                                //        //2
                                //        string strCount2 = "";
                                //        strCount2 = ",\"Count2\":[";
                                //        int roundLoop2 = 1;

                                //        foreach (string val in arr22Month)
                                //        {
                                //            if (roundLoop2 != 1)
                                //            {
                                //                strCount2 += ",";
                                //            }

                                //            strCount2 += arr22Count[rowsMonth];
                                //            roundLoop2++;
                                //            rowsMonth++;
                                //        }

                                //        strType22 = strCount2 + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType33 = "";
                                //        //2
                                //        string strCount3 = "";
                                //        strCount3 = ",\"Count3\":[";
                                //        int roundLoop3 = 1;

                                //        foreach (string val in arr33Month)
                                //        {
                                //            if (roundLoop3 != 1)
                                //            {
                                //                strCount3 += ",";
                                //            }

                                //            strCount3 += arr33Count[rowsMonth];
                                //            roundLoop3++;
                                //            rowsMonth++;
                                //        }

                                //        strType33 = strCount3 + "]}]";
                                //        strResponseLearnStd += strType11 + strType22 + strType33;

                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        strResponseLearnStd = "error";
                                //    }
                                //    Response.Write(strResponseLearnStd);
                                //}
                                //else
                                //{
                                //    //all sub
                                //    try
                                //    {
                                //        var queryReportLearnStudent = from Logs in _db.TLogLearnTimeScan
                                //                                      where Logs.nYear == reportLearnStudentYear
                                //                                && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "0"
                                //                                      join user in _db.TUser
                                //                                      on Logs.sID equals user.sID
                                //                                      where user.cType == "0"
                                //                                      orderby Logs.nLogLearnScanID
                                //                                      select Logs
                                //                                 ;
                                //        if (queryReportLearnStudent.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent = fcommon.LinqToDataTable(queryReportLearnStudent);
                                //        }

                                //        var queryReportLearnStudent2 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "1"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;
                                //        if (queryReportLearnStudent2.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent2 = fcommon.LinqToDataTable(queryReportLearnStudent2);
                                //        }

                                //        var queryReportLearnStudent3 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "0" && Logs.LogLearnScanStatus == "5"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;

                                //        if (queryReportLearnStudent3.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent3 = fcommon.LinqToDataTable(queryReportLearnStudent3);
                                //        }



                                //        string tempMonth = String.Empty;
                                //        int currentMonthCount = 0;

                                //        List<string> arr1Month = new List<string>();
                                //        List<string> arr1Count = new List<string>();
                                //        List<string> arr1Type = new List<string>();

                                //        List<string> arr2Month = new List<string>();
                                //        List<string> arr2Count = new List<string>();
                                //        List<string> arr2Type = new List<string>();

                                //        List<string> arr3Month = new List<string>();
                                //        List<string> arr3Count = new List<string>();
                                //        List<string> arr3Type = new List<string>();
                                //        int rowsMonth = 0;

                                //        foreach (DataRow DR in dtReportLearnStudent.Rows)
                                //        {
                                //            List<string> dateLogs = DR["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    arr1Count[rowsMonth] = currentMonthCount.ToString();
                                //                    arr2Count[rowsMonth] = "0";
                                //                    arr3Count[rowsMonth] = "0";
                                //                }
                                //                else
                                //                {
                                //                    currentMonthCount = 1;
                                //                    rowsMonth++;
                                //                    arr1Month.Add(dateLogs[0]);
                                //                    arr1Count.Add("1");
                                //                    arr1Type.Add("0");
                                //                    arr2Month.Add(dateLogs[0]);
                                //                    arr2Count.Add("0");
                                //                    arr2Type.Add("1");
                                //                    arr3Month.Add(dateLogs[0]);
                                //                    arr3Count.Add("0");
                                //                    arr3Type.Add("1");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                currentMonthCount = 1;
                                //                tempMonth = dateLogs[0];
                                //                arr1Month.Add(dateLogs[0]);
                                //                arr1Count.Add("1");
                                //                arr1Type.Add("1");
                                //                arr2Month.Add(dateLogs[0]);
                                //                arr2Count.Add("0");
                                //                arr2Type.Add("1");
                                //                arr3Month.Add(dateLogs[0]);
                                //                arr3Count.Add("0");
                                //                arr3Type.Add("1");
                                //            }
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR2 in dtReportLearnStudent2.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR2["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr2Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr2Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr2Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr2Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr2Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }


                                //            tempMonth = dateLogs2[0];
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR3 in dtReportLearnStudent3.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR3["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr3Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr3Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr3Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr3Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr3Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }
                                //            tempMonth = dateLogs2[0];
                                //        }



                                //        rowsMonth = 0;
                                //        string strType1 = "";


                                //        //1
                                //        string strMonth = "";
                                //        string strCount = "";
                                //        strMonth = "[{\"Month\":[";
                                //        strCount = ",\"Count\":[";
                                //        int roundLoop = 1;

                                //        foreach (string val in arr1Month)
                                //        {
                                //            if (roundLoop != 1)
                                //            {
                                //                strMonth += ",";
                                //                strCount += ",";
                                //            }

                                //            strMonth += "\"" + monthConv(val) + "\"";
                                //            strCount += arr1Count[rowsMonth];
                                //            roundLoop++;
                                //            rowsMonth++;
                                //        }

                                //        strType1 = strMonth + "]" + strCount + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType2 = "";
                                //        //2
                                //        string strCount2 = "";
                                //        strCount2 = ",\"Count2\":[";
                                //        int roundLoop2 = 1;

                                //        foreach (string val in arr2Month)
                                //        {
                                //            if (roundLoop2 != 1)
                                //            {
                                //                strCount2 += ",";
                                //            }

                                //            strCount2 += arr2Count[rowsMonth];
                                //            roundLoop2++;
                                //            rowsMonth++;
                                //        }

                                //        strType2 = strCount2 + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType3 = "";
                                //        //2
                                //        string strCount3 = "";
                                //        strCount3 = ",\"Count3\":[";
                                //        int roundLoop3 = 1;

                                //        foreach (string val in arr3Month)
                                //        {
                                //            if (roundLoop3 != 1)
                                //            {
                                //                strCount3 += ",";
                                //            }

                                //            strCount3 += arr3Count[rowsMonth];
                                //            roundLoop3++;
                                //            rowsMonth++;
                                //        }

                                //        strType3 = strCount3 + "]}]";
                                //        strResponseLearnStd = strType1 + strType2 + strType3;

                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        strResponseLearnStd = "error";
                                //    }



                                //    try
                                //    {
                                //        dtReportLearnStudent = new DataTable();
                                //        dtReportLearnStudent2 = new DataTable();
                                //        dtReportLearnStudent3 = new DataTable();
                                //        var queryReportLearnStudent = from Logs in _db.TLogLearnTimeScan
                                //                                      where Logs.nYear == reportLearnStudentYear
                                //                                     && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "0"
                                //                                      join user in _db.TUser
                                //                                      on Logs.sID equals user.sID
                                //                                      where user.cType == "0"
                                //                                      orderby Logs.nLogLearnScanID
                                //                                      select Logs
                                //                                 ;
                                //        if (queryReportLearnStudent.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent = fcommon.LinqToDataTable(queryReportLearnStudent);
                                //        }

                                //        var queryReportLearnStudent2 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "2"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       join sub in _db.TStudentLevel
                                //                                       on user.sID equals sub.sID
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;
                                //        if (queryReportLearnStudent2.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent2 = fcommon.LinqToDataTable(queryReportLearnStudent2);
                                //        }

                                //        var queryReportLearnStudent3 = from Logs in _db.TLogLearnTimeScan
                                //                                       where Logs.nYear == reportLearnStudentYear
                                //                                 && Logs.LogLearnType == "1" && Logs.LogLearnScanStatus == "3"
                                //                                       join user in _db.TUser
                                //                                       on Logs.sID equals user.sID
                                //                                       where user.cType == "0"
                                //                                       orderby Logs.nLogLearnScanID
                                //                                       select Logs;

                                //        if (queryReportLearnStudent3.ToList().Count > 0)
                                //        {
                                //            dtReportLearnStudent3 = fcommon.LinqToDataTable(queryReportLearnStudent3);
                                //        }



                                //        string tempMonth = String.Empty;
                                //        int currentMonthCount = 0;

                                //        List<string> arr11Month = new List<string>();
                                //        List<string> arr11Count = new List<string>();
                                //        List<string> arr11Type = new List<string>();

                                //        List<string> arr22Month = new List<string>();
                                //        List<string> arr22Count = new List<string>();
                                //        List<string> arr22Type = new List<string>();

                                //        List<string> arr33Month = new List<string>();
                                //        List<string> arr33Count = new List<string>();
                                //        List<string> arr33Type = new List<string>();
                                //        int rowsMonth = 0;

                                //        foreach (DataRow DR in dtReportLearnStudent.Rows)
                                //        {
                                //            List<string> dateLogs = DR["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    arr11Count[rowsMonth] = currentMonthCount.ToString();
                                //                    arr22Count[rowsMonth] = "0";
                                //                    arr33Count[rowsMonth] = "0";
                                //                }
                                //                else
                                //                {
                                //                    currentMonthCount = 1;
                                //                    rowsMonth++;
                                //                    arr11Month.Add(dateLogs[0]);
                                //                    arr11Count.Add("1");
                                //                    arr11Type.Add("0");
                                //                    arr22Month.Add(dateLogs[0]);
                                //                    arr22Count.Add("0");
                                //                    arr22Type.Add("1");
                                //                    arr33Month.Add(dateLogs[0]);
                                //                    arr33Count.Add("0");
                                //                    arr33Type.Add("1");
                                //                }
                                //            }
                                //            else
                                //            {
                                //                currentMonthCount = 1;
                                //                tempMonth = dateLogs[0];
                                //                arr11Month.Add(dateLogs[0]);
                                //                arr11Count.Add("1");
                                //                arr11Type.Add("1");
                                //                arr22Month.Add(dateLogs[0]);
                                //                arr22Count.Add("0");
                                //                arr22Type.Add("1");
                                //                arr33Month.Add(dateLogs[0]);
                                //                arr33Count.Add("0");
                                //                arr33Type.Add("1");
                                //            }
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR2 in dtReportLearnStudent2.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR2["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr22Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr22Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr22Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr22Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr22Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }


                                //            tempMonth = dateLogs2[0];
                                //        }

                                //        tempMonth = String.Empty;
                                //        rowsMonth = 0;
                                //        currentMonthCount = 0;
                                //        foreach (DataRow DR3 in dtReportLearnStudent3.Rows)
                                //        {
                                //            List<string> dateLogs2 = DR3["LogLearnDate"].ToString().Split('/').ToList();
                                //            if (!String.IsNullOrEmpty(tempMonth))
                                //            {
                                //                if (tempMonth == dateLogs2[0])
                                //                {
                                //                    currentMonthCount++;
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr33Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }
                                //                }
                                //                else
                                //                {
                                //                    //
                                //                    int forcheckMonth = 0;

                                //                    foreach (var valMonthCheck in arr33Month)
                                //                    {
                                //                        if (valMonthCheck == dateLogs2[0])
                                //                        {
                                //                            arr33Count[forcheckMonth] = "1";
                                //                            break;
                                //                        }
                                //                        else
                                //                        {
                                //                            forcheckMonth++;
                                //                        }
                                //                    }

                                //                    //
                                //                    currentMonthCount = 1;
                                //                    tempMonth = dateLogs2[0];
                                //                }
                                //            }
                                //            else
                                //            {

                                //                currentMonthCount = 1;
                                //                int forcheckMonth = 0;

                                //                foreach (var valMonthCheck in arr33Month)
                                //                {
                                //                    if (valMonthCheck == dateLogs2[0])
                                //                    {
                                //                        arr33Count[forcheckMonth] = "1";
                                //                        break;
                                //                    }
                                //                    else
                                //                    {
                                //                        forcheckMonth++;
                                //                    }
                                //                }

                                //            }
                                //            tempMonth = dateLogs2[0];
                                //        }



                                //        rowsMonth = 0;
                                //        string strType11 = "";


                                //        //1
                                //        string strMonth = "";
                                //        string strCount = "";
                                //        strMonth = "split[{\"Month\":[";
                                //        strCount = ",\"Count\":[";
                                //        int roundLoop = 1;

                                //        foreach (string val in arr11Month)
                                //        {
                                //            if (roundLoop != 1)
                                //            {
                                //                strMonth += ",";
                                //                strCount += ",";
                                //            }

                                //            strMonth += "\"" + monthConv(val) + "\"";
                                //            strCount += arr11Count[rowsMonth];
                                //            roundLoop++;
                                //            rowsMonth++;
                                //        }

                                //        strType11 = strMonth + "]" + strCount + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType22 = "";
                                //        //2
                                //        string strCount2 = "";
                                //        strCount2 = ",\"Count2\":[";
                                //        int roundLoop2 = 1;

                                //        foreach (string val in arr22Month)
                                //        {
                                //            if (roundLoop2 != 1)
                                //            {
                                //                strCount2 += ",";
                                //            }

                                //            strCount2 += arr22Count[rowsMonth];
                                //            roundLoop2++;
                                //            rowsMonth++;
                                //        }

                                //        strType22 = strCount2 + "]";//}]";

                                //        rowsMonth = 0;
                                //        string strType33 = "";
                                //        //2
                                //        string strCount3 = "";
                                //        strCount3 = ",\"Count3\":[";
                                //        int roundLoop3 = 1;

                                //        foreach (string val in arr33Month)
                                //        {
                                //            if (roundLoop3 != 1)
                                //            {
                                //                strCount3 += ",";
                                //            }

                                //            strCount3 += arr33Count[rowsMonth];
                                //            roundLoop3++;
                                //            rowsMonth++;
                                //        }

                                //        strType33 = strCount3 + "]}]";
                                //        strResponseLearnStd += strType11 + strType22 + strType33;

                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        strResponseLearnStd = "error";
                                //    }
                                //    Response.Write(strResponseLearnStd);
                                //}

                                //Response.End();
                                break;

                            case "reportunden":
                                string strResponseUnden;
                                int reportUndenYear = Int32.Parse(Request.QueryString["years"].ToString());
                                string reportUndenSearch = Request.QueryString["txtsearch"].ToString();
                                DataTable dtReportUnden = new DataTable();
                                DataTable dtReportUnden2 = new DataTable();
                                DataTable dtReportUnden3 = new DataTable();
                                DataTable dtReportUnden4 = fcommon.LinqToDataTable(_db.TUser.Where(w => w.SchoolID == schoolID && w.sName + " " + w.sLastname == reportUndenSearch
                        || w.sIdentification == reportUndenSearch));

                                //if (reportUndenYear != 0)
                                // {
                                string sUserName = "";

                                try
                                {
                                    var queryReportUnden = from Logs in _db.TLogUserTimeScans
                                                           join user in _db.TUser on Logs.sID equals user.sID
                                                           where Logs.SchoolID == userData.CompanyID &&
                                                           Logs.nYear == reportUndenYear && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                                           && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                           orderby Logs.nLogScanID
                                                           select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID &&
                                    //                           w.nYear == reportUndenYear && w.LogType == "0" && w.LogScanStatus == "0").AsQueryable().ToList()
                                    //                       join user in _db.TUser.Where(w => w.SchoolID == userData.CompanyID &&
                                    //                       (w.sName + " " + w.sLastname == reportUndenSearch || w.sIdentification == reportUndenSearch)
                                    //                       ).AsQueryable().ToList()
                                    //                       on Logs.sID equals user.sID
                                    //                       orderby Logs.nLogScanID
                                    //                       select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                       where Logs.nYear == reportUndenYear
                                    //                      && Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                    //                       join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                       on Logs.sID equals user.sID
                                    //                       where user.sName + " " + user.sLastname == reportUndenSearch
                                    //                        || user.sIdentification == reportUndenSearch
                                    //                       orderby Logs.nLogScanID
                                    //                       select Logs ;
                                    if (queryReportUnden.ToList().Count > 0)
                                    {
                                        dtReportUnden = fcommon.LinqToDataTable(queryReportUnden);
                                    }

                                    var queryReportUnden2 = from Logs in _db.TLogUserTimeScans
                                                            join user in _db.TUser on Logs.sID equals user.sID
                                                            where Logs.SchoolID == userData.CompanyID &&
                                                            Logs.nYear == reportUndenYear && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                                            && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                            orderby Logs.nLogScanID
                                                            select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                        where Logs.nYear == reportUndenYear
                                    //                         && Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                    //                        join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                        on Logs.sID equals user.sID
                                    //                        where user.sName + " " + user.sLastname == reportUndenSearch
                                    //                        || user.sIdentification == reportUndenSearch
                                    //                        orderby Logs.nLogScanID
                                    //                        select Logs;
                                    if (queryReportUnden2.ToList().Count > 0)
                                    {
                                        dtReportUnden2 = fcommon.LinqToDataTable(queryReportUnden2);
                                    }

                                    var queryReportUnden3 = from Logs in _db.TLogUserTimeScans
                                                            join user in _db.TUser on Logs.sID equals user.sID
                                                            where Logs.SchoolID == userData.CompanyID &&
                                                            Logs.nYear == reportUndenYear && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                                            && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                            orderby Logs.nLogScanID
                                                            select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                    where Logs.nYear == reportUndenYear
                                    //                     && Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                    //                    join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                    on Logs.sID equals user.sID
                                    //                    where user.sName + " " + user.sLastname == reportUndenSearch
                                    //                  || user.sIdentification == reportUndenSearch
                                    //                    orderby Logs.nLogScanID
                                    //                    select Logs;

                                    if (queryReportUnden3.ToList().Count > 0)
                                    {
                                        dtReportUnden3 = fcommon.LinqToDataTable(queryReportUnden3);
                                    }



                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr1Month = new List<string>();
                                    List<string> arr1Count = new List<string>();
                                    List<string> arr1Type = new List<string>();

                                    List<string> arr2Month = new List<string>();
                                    List<string> arr2Count = new List<string>();
                                    List<string> arr2Type = new List<string>();

                                    List<string> arr3Month = new List<string>();
                                    List<string> arr3Count = new List<string>();
                                    List<string> arr3Type = new List<string>();
                                    int rowsMonth = 0;
                                    foreach (DataRow DR in dtReportUnden.Rows)
                                    {
                                        List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                        sUserName = dtReportUnden4.Rows[0]["sName"].ToString() + " " + dtReportUnden4.Rows[0]["sLastname"].ToString();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                arr2Count[rowsMonth] = "0";
                                                arr3Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr1Month.Add(dateLogs[0]);
                                                arr1Count.Add("1");
                                                arr1Type.Add("0");
                                                arr2Month.Add(dateLogs[0]);
                                                arr2Count.Add("0");
                                                arr2Type.Add("1");
                                                arr3Month.Add(dateLogs[0]);
                                                arr3Count.Add("0");
                                                arr3Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr1Month.Add(dateLogs[0]);
                                            arr1Count.Add("1");
                                            arr1Type.Add("1");
                                            arr2Month.Add(dateLogs[0]);
                                            arr2Count.Add("0");
                                            arr2Type.Add("1");
                                            arr3Month.Add(dateLogs[0]);
                                            arr3Count.Add("0");
                                            arr3Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportUnden2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr2Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr2Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportUnden3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr3Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr3Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType1 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "[{\"Name\":\"" + sUserName + "\",\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr1Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr1Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType1 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType2 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr2Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr2Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType2 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType3 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr3Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr3Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType3 = strCount3 + "]}]";
                                    strResponseUnden = strType1 + strType2 + strType3;

                                }
                                catch
                                {
                                    strResponseUnden = "error";
                                }



                                try
                                {
                                    dtReportUnden = new DataTable();
                                    dtReportUnden2 = new DataTable();
                                    dtReportUnden3 = new DataTable();
                                    var queryReportsUnden = from Logs in _db.TLogUserTimeScans
                                                            join user in _db.TUser on Logs.sID equals user.sID
                                                            join sub in _db.TStudentLevels on user.sID equals sub.sID
                                                            where Logs.SchoolID == userData.CompanyID &&
                                                            Logs.nYear == reportUndenYear && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                                            && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                            && sub.SchoolID == userData.CompanyID
                                                            orderby Logs.nLogScanID
                                                            select Logs;

                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                    where Logs.nYear == reportUndenYear
                                    //                   && Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                    //                    join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                    on Logs.sID equals user.sID
                                    //                    where user.cType == "0"
                                    //                    join sub in _db.TStudentLevels.Where(w => w.SchoolID == schoolID)
                                    //                    on user.sID equals sub.sID
                                    //                    where user.sName + " " + user.sLastname == reportUndenSearch
                                    //               || user.sIdentification == reportUndenSearch
                                    //                    orderby Logs.nLogScanID
                                    //                    select Logs
                                    //                     ;
                                    if (queryReportsUnden.ToList().Count > 0)
                                    {
                                        dtReportUnden = fcommon.LinqToDataTable(queryReportsUnden);
                                    }

                                    var queryReportsUnden2 = from Logs in _db.TLogUserTimeScans
                                                             join user in _db.TUser on Logs.sID equals user.sID
                                                             join sub in _db.TStudentLevels on user.sID equals sub.sID
                                                             where Logs.SchoolID == userData.CompanyID &&
                                                             Logs.nYear == reportUndenYear && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                                             && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                             && sub.SchoolID == userData.CompanyID
                                                             orderby Logs.nLogScanID
                                                             select Logs;


                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                         where Logs.nYear == reportUndenYear
                                    //                        && Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                    //                         join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                         on Logs.sID equals user.sID
                                    //                         where user.cType == "0"
                                    //                         join sub in _db.TStudentLevels.Where(w => w.SchoolID == schoolID)
                                    //                         on user.sID equals sub.sID
                                    //                         where user.sName + " " + user.sLastname == reportUndenSearch
                                    //                          || user.sIdentification == reportUndenSearch
                                    //                         orderby Logs.nLogScanID
                                    //                         select Logs;
                                    if (queryReportsUnden2.ToList().Count > 0)
                                    {
                                        dtReportUnden2 = fcommon.LinqToDataTable(queryReportsUnden2);
                                    }

                                    var queryReportsUnden3 = from Logs in _db.TLogUserTimeScans
                                                             join user in _db.TUser on Logs.sID equals user.sID
                                                             join sub in _db.TStudentLevels on user.sID equals sub.sID
                                                             where Logs.SchoolID == userData.CompanyID &&
                                                             Logs.nYear == reportUndenYear && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                                             && user.SchoolID == userData.CompanyID && (user.sName + " " + user.sLastname == reportUndenSearch || user.sIdentification == reportUndenSearch)
                                                             && sub.SchoolID == userData.CompanyID
                                                             orderby Logs.nLogScanID
                                                             select Logs;
                                    //from Logs in _db.TLogUserTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                     where Logs.nYear == reportUndenYear
                                    //                    && Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                    //                     join user in _db.TUser.Where(w => w.SchoolID == schoolID)
                                    //                     on Logs.sID equals user.sID
                                    //                     where user.cType == "0"
                                    //                     join sub in _db.TStudentLevels.Where(w => w.SchoolID == schoolID)
                                    //                     on user.sID equals sub.sID
                                    //                     where user.sName + " " + user.sLastname == reportUndenSearch
                                    //                      || user.sIdentification == reportUndenSearch
                                    //                     orderby Logs.nLogScanID
                                    //                     select Logs;

                                    if (queryReportsUnden3.ToList().Count > 0)
                                    {
                                        dtReportUnden3 = fcommon.LinqToDataTable(queryReportsUnden3);
                                    }



                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr11Month = new List<string>();
                                    List<string> arr11Count = new List<string>();
                                    List<string> arr11Type = new List<string>();

                                    List<string> arr22Month = new List<string>();
                                    List<string> arr22Count = new List<string>();
                                    List<string> arr22Type = new List<string>();

                                    List<string> arr33Month = new List<string>();
                                    List<string> arr33Count = new List<string>();
                                    List<string> arr33Type = new List<string>();
                                    int rowsMonth = 0;


                                    foreach (DataRow DR in dtReportUnden.Rows)
                                    {
                                        List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                        sUserName = dtReportUnden4.Rows[0]["sName"].ToString() + " " + dtReportUnden4.Rows[0]["sLastname"].ToString();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                arr22Count[rowsMonth] = "0";
                                                arr33Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr11Month.Add(dateLogs[0]);
                                                arr11Count.Add("1");
                                                arr11Type.Add("0");
                                                arr22Month.Add(dateLogs[0]);
                                                arr22Count.Add("0");
                                                arr22Type.Add("1");
                                                arr33Month.Add(dateLogs[0]);
                                                arr33Count.Add("0");
                                                arr33Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr11Month.Add(dateLogs[0]);
                                            arr11Count.Add("1");
                                            arr11Type.Add("1");
                                            arr22Month.Add(dateLogs[0]);
                                            arr22Count.Add("0");
                                            arr22Type.Add("1");
                                            arr33Month.Add(dateLogs[0]);
                                            arr33Count.Add("0");
                                            arr33Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportUnden2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr22Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr22Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportUnden3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr33Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr33Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType11 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "split[{\"Name\":\"" + sUserName + "\",\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr11Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr11Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType11 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType22 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr22Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr22Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType22 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType33 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr33Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr33Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType33 = strCount3 + "]}]";
                                    strResponseUnden += strType11 + strType22 + strType33;

                                }
                                catch
                                {
                                    strResponseUnden = "error";
                                }
                                Response.Write(strResponseUnden);
                                // }
                                /* else {
                                 //all sub
                                     string sUserName = "";
                                     try
                                     {
                                         var queryReportUnden = from Logs in _db.TLogUserTimeScan
                                                                where Logs.LogType == "0" && Logs.LogScanStatus == "0"
                                                                join user in _db.TUser
                                                                on Logs.sID equals user.sID
                                                                where user.sName + " " + user.sLastname == reportUndenSearch
                                                                 || user.sIdentification == reportUndenSearch
                                                                orderby Logs.nLogScanID
                                                                select Logs
                                                                  ;
                                         if (queryReportUnden.ToList().Count > 0)
                                         {
                                             dtReportUnden = fcommon.LinqToDataTable(queryReportUnden);
                                         }

                                         var queryReportUnden2 = from Logs in _db.TLogUserTimeScan
                                                                 where Logs.LogType == "0" && Logs.LogScanStatus == "1"
                                                                 join user in _db.TUser
                                                                 on Logs.sID equals user.sID
                                                                 where user.sName + " " + user.sLastname == reportUndenSearch
                                                                 || user.sIdentification == reportUndenSearch
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;
                                         if (queryReportUnden2.ToList().Count > 0)
                                         {
                                             dtReportUnden2 = fcommon.LinqToDataTable(queryReportUnden2);
                                         }

                                         var queryReportUnden3 = from Logs in _db.TLogUserTimeScan
                                                                 where Logs.LogType == "0" && Logs.LogScanStatus == "5"
                                                                 join user in _db.TUser
                                                                 on Logs.sID equals user.sID
                                                                 where user.sName + " " + user.sLastname == reportUndenSearch
                                                               || user.sIdentification == reportUndenSearch
                                                                 orderby Logs.nLogScanID
                                                                 select Logs;

                                         if (queryReportUnden3.ToList().Count > 0)
                                         {
                                             dtReportUnden3 = fcommon.LinqToDataTable(queryReportUnden3);
                                         }



                                         string tempMonth = String.Empty;
                                         int currentMonthCount = 0;

                                         List<string> arr1Month = new List<string>();
                                         List<string> arr1Count = new List<string>();
                                         List<string> arr1Type = new List<string>();

                                         List<string> arr2Month = new List<string>();
                                         List<string> arr2Count = new List<string>();
                                         List<string> arr2Type = new List<string>();

                                         List<string> arr3Month = new List<string>();
                                         List<string> arr3Count = new List<string>();
                                         List<string> arr3Type = new List<string>();
                                         int rowsMonth = 0;

                                         foreach (DataRow DR in dtReportUnden.Rows)
                                         {
                                             List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                             sUserName = dtReportUnden4.Rows[0]["sName"].ToString() + " " + dtReportUnden4.Rows[0]["sLastname"].ToString();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs[0])
                                                 {
                                                     currentMonthCount++;
                                                     arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                     arr2Count[rowsMonth] = "0";
                                                     arr3Count[rowsMonth] = "0";
                                                 }
                                                 else
                                                 {
                                                     currentMonthCount = 1;
                                                     rowsMonth++;
                                                     arr1Month.Add(dateLogs[0]);
                                                     arr1Count.Add("1");
                                                     arr1Type.Add("0");
                                                     arr2Month.Add(dateLogs[0]);
                                                     arr2Count.Add("0");
                                                     arr2Type.Add("1");
                                                     arr3Month.Add(dateLogs[0]);
                                                     arr3Count.Add("0");
                                                     arr3Type.Add("1");
                                                 }
                                             }
                                             else
                                             {
                                                 currentMonthCount = 1;
                                                 tempMonth = dateLogs[0];
                                                 arr1Month.Add(dateLogs[0]);
                                                 arr1Count.Add("1");
                                                 arr1Type.Add("1");
                                                 arr2Month.Add(dateLogs[0]);
                                                 arr2Count.Add("0");
                                                 arr2Type.Add("1");
                                                 arr3Month.Add(dateLogs[0]);
                                                 arr3Count.Add("0");
                                                 arr3Type.Add("1");
                                             }
                                         }

                                         tempMonth = String.Empty;
                                         rowsMonth = 0;
                                         currentMonthCount = 0;
                                         foreach (DataRow DR2 in dtReportUnden2.Rows)
                                         {
                                             List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs2[0])
                                                 {
                                                     currentMonthCount++;
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr2Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr2Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr2Count[forcheckMonth] = "1";
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }

                                                     //
                                                     currentMonthCount = 1;
                                                     tempMonth = dateLogs2[0];
                                                 }
                                             }
                                             else
                                             {

                                                 currentMonthCount = 1;
                                                 int forcheckMonth = 0;

                                                 foreach (var valMonthCheck in arr2Month)
                                                 {
                                                     if (valMonthCheck == dateLogs2[0])
                                                     {
                                                         arr2Count[forcheckMonth] = "1";
                                                         break;
                                                     }
                                                     else
                                                     {
                                                         forcheckMonth++;
                                                     }
                                                 }

                                             }


                                             tempMonth = dateLogs2[0];
                                         }

                                         tempMonth = String.Empty;
                                         rowsMonth = 0;
                                         currentMonthCount = 0;
                                         foreach (DataRow DR3 in dtReportUnden3.Rows)
                                         {
                                             List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs2[0])
                                                 {
                                                     currentMonthCount++;
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr3Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr3Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr3Count[forcheckMonth] = "1";
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }

                                                     //
                                                     currentMonthCount = 1;
                                                     tempMonth = dateLogs2[0];
                                                 }
                                             }
                                             else
                                             {

                                                 currentMonthCount = 1;
                                                 int forcheckMonth = 0;

                                                 foreach (var valMonthCheck in arr3Month)
                                                 {
                                                     if (valMonthCheck == dateLogs2[0])
                                                     {
                                                         arr3Count[forcheckMonth] = "1";
                                                         break;
                                                     }
                                                     else
                                                     {
                                                         forcheckMonth++;
                                                     }
                                                 }

                                             }
                                             tempMonth = dateLogs2[0];
                                         }



                                         rowsMonth = 0;
                                         string strType1 = "";


                                         //1
                                         string strMonth = "";
                                         string strCount = "";
                                         strMonth = "[{\"Name\":\""+sUserName+"\",\"Month\":[";
                                         strCount = ",\"Count\":[";
                                         int roundLoop = 1;

                                         foreach (string val in arr1Month)
                                         {
                                             if (roundLoop != 1)
                                             {
                                                 strMonth += ",";
                                                 strCount += ",";
                                             }

                                             strMonth += "\"" + monthConv(val) + "\"";
                                             strCount += arr1Count[rowsMonth];
                                             roundLoop++;
                                             rowsMonth++;
                                         }

                                         strType1 = strMonth + "]" + strCount + "]";//}]";

                                         rowsMonth = 0;
                                         string strType2 = "";
                                         //2
                                         string strCount2 = "";
                                         strCount2 = ",\"Count2\":[";
                                         int roundLoop2 = 1;

                                         foreach (string val in arr2Month)
                                         {
                                             if (roundLoop2 != 1)
                                             {
                                                 strCount2 += ",";
                                             }

                                             strCount2 += arr2Count[rowsMonth];
                                             roundLoop2++;
                                             rowsMonth++;
                                         }

                                         strType2 = strCount2 + "]";//}]";

                                         rowsMonth = 0;
                                         string strType3 = "";
                                         //2
                                         string strCount3 = "";
                                         strCount3 = ",\"Count3\":[";
                                         int roundLoop3 = 1;

                                         foreach (string val in arr3Month)
                                         {
                                             if (roundLoop3 != 1)
                                             {
                                                 strCount3 += ",";
                                             }

                                             strCount3 += arr3Count[rowsMonth];
                                             roundLoop3++;
                                             rowsMonth++;
                                         }

                                         strType3 = strCount3 + "]}]";
                                         strResponseUnden = strType1 + strType2 + strType3;

                                     }
                                     catch (Exception ex)
                                     {
                                         strResponseUnden = "error";
                                     }



                                     try
                                     {
                                         dtReportUnden = new DataTable();
                                         dtReportUnden2 = new DataTable();
                                         dtReportUnden3 = new DataTable();
                                         var queryReportsUnden = from Logs in _db.TLogUserTimeScan
                                                                 where  Logs.LogType == "1" && Logs.LogScanStatus == "0"
                                                                 join user in _db.TUser
                                                                 on Logs.sID equals user.sID
                                                                 where user.cType == "0"
                                                                 join sub in _db.TStudentLevel
                                                                 on user.sID equals sub.sID
                                                                 where user.sName + " " + user.sLastname == reportUndenSearch
                                                            || user.sIdentification == reportUndenSearch
                                                                 orderby Logs.nLogScanID
                                                                 select Logs
                                                                  ;
                                         if (queryReportsUnden.ToList().Count > 0)
                                         {
                                             dtReportUnden = fcommon.LinqToDataTable(queryReportsUnden);
                                         }

                                         var queryReportsUnden2 = from Logs in _db.TLogUserTimeScan
                                                                  where  Logs.LogType == "1" && Logs.LogScanStatus == "2"
                                                                  join user in _db.TUser
                                                                  on Logs.sID equals user.sID
                                                                  where user.cType == "0"
                                                                  join sub in _db.TStudentLevel
                                                                  on user.sID equals sub.sID
                                                                  where user.sName + " " + user.sLastname == reportUndenSearch
                                                                   || user.sIdentification == reportUndenSearch
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;
                                         if (queryReportsUnden2.ToList().Count > 0)
                                         {
                                             dtReportUnden2 = fcommon.LinqToDataTable(queryReportsUnden2);
                                         }

                                         var queryReportsUnden3 = from Logs in _db.TLogUserTimeScan
                                                                  where  Logs.LogType == "1" && Logs.LogScanStatus == "3"
                                                                  join user in _db.TUser
                                                                  on Logs.sID equals user.sID
                                                                  where user.cType == "0"
                                                                  join sub in _db.TStudentLevel
                                                                  on user.sID equals sub.sID
                                                                  where user.sName + " " + user.sLastname == reportUndenSearch
                                                                   || user.sIdentification == reportUndenSearch
                                                                  orderby Logs.nLogScanID
                                                                  select Logs;

                                         if (queryReportsUnden3.ToList().Count > 0)
                                         {
                                             dtReportUnden3 = fcommon.LinqToDataTable(queryReportsUnden3);
                                         }



                                         string tempMonth = String.Empty;
                                         int currentMonthCount = 0;

                                         List<string> arr11Month = new List<string>();
                                         List<string> arr11Count = new List<string>();
                                         List<string> arr11Type = new List<string>();

                                         List<string> arr22Month = new List<string>();
                                         List<string> arr22Count = new List<string>();
                                         List<string> arr22Type = new List<string>();

                                         List<string> arr33Month = new List<string>();
                                         List<string> arr33Count = new List<string>();
                                         List<string> arr33Type = new List<string>();
                                         int rowsMonth = 0;

                                         foreach (DataRow DR in dtReportUnden.Rows)
                                         {
                                             List<string> dateLogs = DR["LogDate"].ToString().Split('/').ToList();
                                             sUserName = dtReportUnden4.Rows[0]["sName"].ToString() + " " + dtReportUnden4.Rows[0]["sLastname"].ToString();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs[0])
                                                 {
                                                     currentMonthCount++;
                                                     arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                     arr22Count[rowsMonth] = "0";
                                                     arr33Count[rowsMonth] = "0";
                                                 }
                                                 else
                                                 {
                                                     currentMonthCount = 1;
                                                     rowsMonth++;
                                                     arr11Month.Add(dateLogs[0]);
                                                     arr11Count.Add("1");
                                                     arr11Type.Add("0");
                                                     arr22Month.Add(dateLogs[0]);
                                                     arr22Count.Add("0");
                                                     arr22Type.Add("1");
                                                     arr33Month.Add(dateLogs[0]);
                                                     arr33Count.Add("0");
                                                     arr33Type.Add("1");
                                                 }
                                             }
                                             else
                                             {
                                                 currentMonthCount = 1;
                                                 tempMonth = dateLogs[0];
                                                 arr11Month.Add(dateLogs[0]);
                                                 arr11Count.Add("1");
                                                 arr11Type.Add("1");
                                                 arr22Month.Add(dateLogs[0]);
                                                 arr22Count.Add("0");
                                                 arr22Type.Add("1");
                                                 arr33Month.Add(dateLogs[0]);
                                                 arr33Count.Add("0");
                                                 arr33Type.Add("1");
                                             }
                                         }

                                         tempMonth = String.Empty;
                                         rowsMonth = 0;
                                         currentMonthCount = 0;
                                         foreach (DataRow DR2 in dtReportUnden2.Rows)
                                         {
                                             List<string> dateLogs2 = DR2["LogDate"].ToString().Split('/').ToList();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs2[0])
                                                 {
                                                     currentMonthCount++;
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr22Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr22Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr22Count[forcheckMonth] = "1";
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }

                                                     //
                                                     currentMonthCount = 1;
                                                     tempMonth = dateLogs2[0];
                                                 }
                                             }
                                             else
                                             {

                                                 currentMonthCount = 1;
                                                 int forcheckMonth = 0;

                                                 foreach (var valMonthCheck in arr22Month)
                                                 {
                                                     if (valMonthCheck == dateLogs2[0])
                                                     {
                                                         arr22Count[forcheckMonth] = "1";
                                                         break;
                                                     }
                                                     else
                                                     {
                                                         forcheckMonth++;
                                                     }
                                                 }

                                             }


                                             tempMonth = dateLogs2[0];
                                         }

                                         tempMonth = String.Empty;
                                         rowsMonth = 0;
                                         currentMonthCount = 0;
                                         foreach (DataRow DR3 in dtReportUnden3.Rows)
                                         {
                                             List<string> dateLogs2 = DR3["LogDate"].ToString().Split('/').ToList();
                                             if (!String.IsNullOrEmpty(tempMonth))
                                             {
                                                 if (tempMonth == dateLogs2[0])
                                                 {
                                                     currentMonthCount++;
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr33Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }
                                                 }
                                                 else
                                                 {
                                                     //
                                                     int forcheckMonth = 0;

                                                     foreach (var valMonthCheck in arr33Month)
                                                     {
                                                         if (valMonthCheck == dateLogs2[0])
                                                         {
                                                             arr33Count[forcheckMonth] = "1";
                                                             break;
                                                         }
                                                         else
                                                         {
                                                             forcheckMonth++;
                                                         }
                                                     }

                                                     //
                                                     currentMonthCount = 1;
                                                     tempMonth = dateLogs2[0];
                                                 }
                                             }
                                             else
                                             {

                                                 currentMonthCount = 1;
                                                 int forcheckMonth = 0;

                                                 foreach (var valMonthCheck in arr33Month)
                                                 {
                                                     if (valMonthCheck == dateLogs2[0])
                                                     {
                                                         arr33Count[forcheckMonth] = "1";
                                                         break;
                                                     }
                                                     else
                                                     {
                                                         forcheckMonth++;
                                                     }
                                                 }

                                             }
                                             tempMonth = dateLogs2[0];
                                         }



                                         rowsMonth = 0;
                                         string strType11 = "";


                                         //1
                                         string strMonth = "";
                                         string strCount = "";
                                         strMonth = "split[{\"Name\":\""+sUserName+"\",\"Month\":[";
                                         strCount = ",\"Count\":[";
                                         int roundLoop = 1;

                                         foreach (string val in arr11Month)
                                         {
                                             if (roundLoop != 1)
                                             {
                                                 strMonth += ",";
                                                 strCount += ",";
                                             }

                                             strMonth += "\"" + monthConv(val) + "\"";
                                             strCount += arr11Count[rowsMonth];
                                             roundLoop++;
                                             rowsMonth++;
                                         }

                                         strType11 = strMonth + "]" + strCount + "]";//}]";

                                         rowsMonth = 0;
                                         string strType22 = "";
                                         //2
                                         string strCount2 = "";
                                         strCount2 = ",\"Count2\":[";
                                         int roundLoop2 = 1;

                                         foreach (string val in arr22Month)
                                         {
                                             if (roundLoop2 != 1)
                                             {
                                                 strCount2 += ",";
                                             }

                                             strCount2 += arr22Count[rowsMonth];
                                             roundLoop2++;
                                             rowsMonth++;
                                         }

                                         strType22 = strCount2 + "]";//}]";

                                         rowsMonth = 0;
                                         string strType33 = "";
                                         //2
                                         string strCount3 = "";
                                         strCount3 = ",\"Count3\":[";
                                         int roundLoop3 = 1;

                                         foreach (string val in arr33Month)
                                         {
                                             if (roundLoop3 != 1)
                                             {
                                                 strCount3 += ",";
                                             }

                                             strCount3 += arr33Count[rowsMonth];
                                             roundLoop3++;
                                             rowsMonth++;
                                         }

                                         strType33 = strCount3 + "]}]";
                                         strResponseUnden += strType11 + strType22 + strType33;

                                     }
                                     catch (Exception ex)
                                     {
                                         strResponseUnden = "error";
                                     }
                                     Response.Write(strResponseUnden);
                                 }*/

                                Response.End();
                                break;

                            case "reportlearnunden":
                                break;

                            case "reportempunden":
                                string strResponseEmpUnden;
                                int reportEmpUndenYear = Int32.Parse(Request.QueryString["years"].ToString());
                                string reportEmpUndenSearch = Request.QueryString["txtsearch"].ToString();
                                DataTable dtReportEmpUnden = new DataTable();
                                DataTable dtReportEmpUnden2 = new DataTable();
                                DataTable dtReportEmpUnden3 = new DataTable();
                                DataTable dtReportEmpUnden4 = fcommon.LinqToDataTable(_db.TEmployees.Where(w => w.sName + " " + w.sLastname == reportEmpUndenSearch
                        || w.sIdentification == reportEmpUndenSearch));

                                //if (reportUndenYear != 0)
                                // {
                                string EmpUserName = "";

                                try
                                {
                                    var queryReportEmpUnden = from Logs in _db.TLogEmpTimeScans
                                                              join user in _db.TEmployees
                                                              on Logs.sEmp equals user.sEmp
                                                              where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                              && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "0" && user.SchoolID == schoolID
                                                              && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                              orderby Logs.nLogEmpScanID
                                                              select Logs;

                                    //from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                          where Logs.nYear == reportEmpUndenYear
                                    //                      && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "0"
                                    //                          join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //                          on Logs.sEmp equals user.sEmp
                                    //                          where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //                        || user.sIdentification == reportEmpUndenSearch
                                    //                          orderby Logs.nLogEmpScanID
                                    //                          select Logs
                                    //                         ;
                                    if (queryReportEmpUnden.ToList().Count > 0)
                                    {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        dtReportEmpUnden = fcommon.LinqToDataTable(queryReportEmpUnden);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                    }

                                    var queryReportEmpUnden2 = from Logs in _db.TLogEmpTimeScans
                                                               join user in _db.TEmployees
                                                               on Logs.sEmp equals user.sEmp
                                                               where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                               && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "1" && user.SchoolID == schoolID
                                                               && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                               orderby Logs.nLogEmpScanID
                                                               select Logs;

                                    //from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                           where Logs.nYear == reportEmpUndenYear
                                    //                            && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "1"
                                    //                           join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //                           on Logs.sEmp equals user.sEmp
                                    //                           where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //                      || user.sIdentification == reportEmpUndenSearch
                                    //                           orderby Logs.nLogEmpScanID
                                    //                           select Logs;
                                    if (queryReportEmpUnden2.ToList().Count > 0)
                                    {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        dtReportEmpUnden2 = fcommon.LinqToDataTable(queryReportEmpUnden2);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                    }

                                    var queryReportEmpUnden3 = from Logs in _db.TLogEmpTimeScans
                                                               join user in _db.TEmployees
                                                               on Logs.sEmp equals user.sEmp
                                                               where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                               && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "5" && user.SchoolID == schoolID
                                                               && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                               orderby Logs.nLogEmpScanID
                                                               select Logs;

                                    //from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                       where Logs.nYear == reportEmpUndenYear
                                    //                          && Logs.LogEmpType == "0" && Logs.LogEmpScanStatus == "5"
                                    //                       join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //                       on Logs.sEmp equals user.sEmp
                                    //                       where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //                  || user.sIdentification == reportEmpUndenSearch
                                    //                       orderby Logs.nLogEmpScanID
                                    //                       select Logs;

                                    if (queryReportEmpUnden3.ToList().Count > 0)
                                    {
                                        dtReportEmpUnden3 = fcommon.LinqToDataTable(queryReportEmpUnden3);
                                    }

                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr1Month = new List<string>();
                                    List<string> arr1Count = new List<string>();
                                    List<string> arr1Type = new List<string>();

                                    List<string> arr2Month = new List<string>();
                                    List<string> arr2Count = new List<string>();
                                    List<string> arr2Type = new List<string>();

                                    List<string> arr3Month = new List<string>();
                                    List<string> arr3Count = new List<string>();
                                    List<string> arr3Type = new List<string>();
                                    int rowsMonth = 0;
                                    foreach (DataRow DR in dtReportEmpUnden.Rows)
                                    {
                                        List<string> dateLogs = DR["LogEmpDate"].ToString().Split('/').ToList();
                                        EmpUserName = dtReportEmpUnden4.Rows[0]["sName"].ToString() + " " + dtReportEmpUnden4.Rows[0]["sLastname"].ToString();


                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr1Count[rowsMonth] = currentMonthCount.ToString();
                                                arr2Count[rowsMonth] = "0";
                                                arr3Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr1Month.Add(dateLogs[0]);
                                                arr1Count.Add("1");
                                                arr1Type.Add("0");
                                                arr2Month.Add(dateLogs[0]);
                                                arr2Count.Add("0");
                                                arr2Type.Add("1");
                                                arr3Month.Add(dateLogs[0]);
                                                arr3Count.Add("0");
                                                arr3Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr1Month.Add(dateLogs[0]);
                                            arr1Count.Add("1");
                                            arr1Type.Add("1");
                                            arr2Month.Add(dateLogs[0]);
                                            arr2Count.Add("0");
                                            arr2Type.Add("1");
                                            arr3Month.Add(dateLogs[0]);
                                            arr3Count.Add("0");
                                            arr3Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportEmpUnden2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogEmpDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr2Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr2Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr2Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr2Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportEmpUnden3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogEmpDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr3Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr3Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr3Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr3Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType1 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "[{\"Name\":\"" + EmpUserName + "\",\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr1Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr1Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType1 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType2 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr2Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr2Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType2 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType3 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr3Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr3Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType3 = strCount3 + "]}]";
                                    strResponseEmpUnden = strType1 + strType2 + strType3;

                                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                                {
                                    strResponseEmpUnden = "error";
                                }



                                try
                                {
                                    dtReportEmpUnden = new DataTable();
                                    dtReportEmpUnden2 = new DataTable();
                                    dtReportEmpUnden3 = new DataTable();
                                    var queryReportsEmpUnden = from Logs in _db.TLogEmpTimeScans
                                                               join user in _db.TEmployees
                                                               on Logs.sEmp equals user.sEmp
                                                               where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                               && Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "0" && user.SchoolID == schoolID
                                                               && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                               orderby Logs.nLogEmpScanID
                                                               select Logs;


                                    //from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                           where Logs.nYear == reportEmpUndenYear
                                    //                            && Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "0"
                                    //                           join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //                          on Logs.sEmp equals user.sEmp
                                    //                           where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //                   || user.sIdentification == reportEmpUndenSearch
                                    //                           orderby Logs.nLogEmpScanID
                                    //                           select Logs
                                    //                         ;
                                    if (queryReportsEmpUnden.ToList().Count > 0)
                                    {
                                        dtReportEmpUnden = fcommon.LinqToDataTable(queryReportsEmpUnden);
                                    }

                                    var queryReportsEmpUnden2 = from Logs in _db.TLogEmpTimeScans
                                                                join user in _db.TEmployees
                                                                on Logs.sEmp equals user.sEmp
                                                                where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                                && Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "2" && user.SchoolID == schoolID
                                                                && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                                orderby Logs.nLogEmpScanID
                                                                select Logs;

                                    //from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //                        where Logs.nYear == reportEmpUndenYear
                                    //                         && Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "2"
                                    //                        join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //                       on Logs.sEmp equals user.sEmp
                                    //                        where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //                      || user.sIdentification == reportEmpUndenSearch
                                    //                        orderby Logs.nLogEmpScanID
                                    //                        select Logs;
                                    if (queryReportsEmpUnden2.ToList().Count > 0)
                                    {
#pragma warning disable CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                        dtReportEmpUnden2 = fcommon.LinqToDataTable(queryReportsEmpUnden2);
#pragma warning restore CS0436 // The type 'fcommon' in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs' conflicts with the imported type 'fcommon' in 'FingerprintPayment, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'. Using the type defined in 'G:\GitHub\MyFirstProject\from songkra school\JabJaiWebBackEnd\jabjaiuniversity\FingerprintPayment\Class\fcommon.cs'.
                                    }

                                    var queryReportsEmpUnden3 = from Logs in _db.TLogEmpTimeScans
                                                                join user in _db.TEmployees
                                                                on Logs.sEmp equals user.sEmp
                                                                where Logs.SchoolID == schoolID && Logs.nYear == reportEmpUndenYear
                                                                && Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "3" && user.SchoolID == schoolID
                                                                && (user.sName + " " + user.sLastname == reportEmpUndenSearch || user.sIdentification == reportEmpUndenSearch)
                                                                orderby Logs.nLogEmpScanID
                                                                select Logs;

                                    //      from Logs in _db.TLogEmpTimeScans.Where(w => w.SchoolID == schoolID)
                                    //      where Logs.nYear == reportEmpUndenYear
                                    //&& Logs.LogEmpType == "1" && Logs.LogEmpScanStatus == "3"
                                    //      join user in _db.TEmployees.Where(w => w.SchoolID == schoolID)
                                    //      on Logs.sEmp equals user.sEmp
                                    //      where user.sName + " " + user.sLastname == reportEmpUndenSearch
                                    //      || user.sIdentification == reportEmpUndenSearch
                                    //      orderby Logs.nLogEmpScanID
                                    //      select Logs;

                                    if (queryReportsEmpUnden3.ToList().Count > 0)
                                    {
                                        dtReportEmpUnden3 = fcommon.LinqToDataTable(queryReportsEmpUnden3);
                                    }

                                    string tempMonth = String.Empty;
                                    int currentMonthCount = 0;

                                    List<string> arr11Month = new List<string>();
                                    List<string> arr11Count = new List<string>();
                                    List<string> arr11Type = new List<string>();

                                    List<string> arr22Month = new List<string>();
                                    List<string> arr22Count = new List<string>();
                                    List<string> arr22Type = new List<string>();

                                    List<string> arr33Month = new List<string>();
                                    List<string> arr33Count = new List<string>();
                                    List<string> arr33Type = new List<string>();
                                    int rowsMonth = 0;


                                    foreach (DataRow DR in dtReportEmpUnden.Rows)
                                    {
                                        List<string> dateLogs = DR["LogEmpDate"].ToString().Split('/').ToList();
                                        EmpUserName = dtReportEmpUnden4.Rows[0]["sName"].ToString() + " " + dtReportEmpUnden4.Rows[0]["sLastname"].ToString();

                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs[0])
                                            {
                                                currentMonthCount++;
                                                arr11Count[rowsMonth] = currentMonthCount.ToString();
                                                arr22Count[rowsMonth] = "0";
                                                arr33Count[rowsMonth] = "0";
                                            }
                                            else
                                            {
                                                currentMonthCount = 1;
                                                rowsMonth++;
                                                arr11Month.Add(dateLogs[0]);
                                                arr11Count.Add("1");
                                                arr11Type.Add("0");
                                                arr22Month.Add(dateLogs[0]);
                                                arr22Count.Add("0");
                                                arr22Type.Add("1");
                                                arr33Month.Add(dateLogs[0]);
                                                arr33Count.Add("0");
                                                arr33Type.Add("1");
                                            }
                                        }
                                        else
                                        {
                                            currentMonthCount = 1;
                                            tempMonth = dateLogs[0];
                                            arr11Month.Add(dateLogs[0]);
                                            arr11Count.Add("1");
                                            arr11Type.Add("1");
                                            arr22Month.Add(dateLogs[0]);
                                            arr22Count.Add("0");
                                            arr22Type.Add("1");
                                            arr33Month.Add(dateLogs[0]);
                                            arr33Count.Add("0");
                                            arr33Type.Add("1");
                                        }
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR2 in dtReportEmpUnden2.Rows)
                                    {
                                        List<string> dateLogs2 = DR2["LogEmpDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr22Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr22Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr22Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr22Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }


                                        tempMonth = dateLogs2[0];
                                    }

                                    tempMonth = String.Empty;
                                    rowsMonth = 0;
                                    currentMonthCount = 0;
                                    foreach (DataRow DR3 in dtReportEmpUnden3.Rows)
                                    {
                                        List<string> dateLogs2 = DR3["LogEmpDate"].ToString().Split('/').ToList();
                                        if (!String.IsNullOrEmpty(tempMonth))
                                        {
                                            if (tempMonth == dateLogs2[0])
                                            {
                                                currentMonthCount++;
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = currentMonthCount.ToString();
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //
                                                int forcheckMonth = 0;

                                                foreach (var valMonthCheck in arr33Month)
                                                {
                                                    if (valMonthCheck == dateLogs2[0])
                                                    {
                                                        arr33Count[forcheckMonth] = "1";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        forcheckMonth++;
                                                    }
                                                }

                                                //
                                                currentMonthCount = 1;
                                                tempMonth = dateLogs2[0];
                                            }
                                        }
                                        else
                                        {

                                            currentMonthCount = 1;
                                            int forcheckMonth = 0;

                                            foreach (var valMonthCheck in arr33Month)
                                            {
                                                if (valMonthCheck == dateLogs2[0])
                                                {
                                                    arr33Count[forcheckMonth] = "1";
                                                    break;
                                                }
                                                else
                                                {
                                                    forcheckMonth++;
                                                }
                                            }

                                        }
                                        tempMonth = dateLogs2[0];
                                    }



                                    rowsMonth = 0;
                                    string strType11 = "";


                                    //1
                                    string strMonth = "";
                                    string strCount = "";
                                    strMonth = "split[{\"Name\":\"" + EmpUserName + "\",\"Month\":[";
                                    strCount = ",\"Count\":[";
                                    int roundLoop = 1;

                                    foreach (string val in arr11Month)
                                    {
                                        if (roundLoop != 1)
                                        {
                                            strMonth += ",";
                                            strCount += ",";
                                        }

                                        strMonth += "\"" + monthConv(val) + "\"";
                                        strCount += arr11Count[rowsMonth];
                                        roundLoop++;
                                        rowsMonth++;
                                    }

                                    strType11 = strMonth + "]" + strCount + "]";//}]";

                                    rowsMonth = 0;
                                    string strType22 = "";
                                    //2
                                    string strCount2 = "";
                                    strCount2 = ",\"Count2\":[";
                                    int roundLoop2 = 1;

                                    foreach (string val in arr22Month)
                                    {
                                        if (roundLoop2 != 1)
                                        {
                                            strCount2 += ",";
                                        }

                                        strCount2 += arr22Count[rowsMonth];
                                        roundLoop2++;
                                        rowsMonth++;
                                    }

                                    strType22 = strCount2 + "]";//}]";

                                    rowsMonth = 0;
                                    string strType33 = "";
                                    //2
                                    string strCount3 = "";
                                    strCount3 = ",\"Count3\":[";
                                    int roundLoop3 = 1;

                                    foreach (string val in arr33Month)
                                    {
                                        if (roundLoop3 != 1)
                                        {
                                            strCount3 += ",";
                                        }

                                        strCount3 += arr33Count[rowsMonth];
                                        roundLoop3++;
                                        rowsMonth++;
                                    }

                                    strType33 = strCount3 + "]}]";
                                    strResponseEmpUnden += strType11 + strType22 + strType33;

                                }
                                catch
                                {
                                    strResponseEmpUnden = "error";
                                }
                                Response.Write(strResponseEmpUnden);
                                Response.End();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("mode:{0}", mode);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "modalJSON2.ashx", parameters, "", null);
            }
        }

        public string monthConv(string month)
        {
            string txtMonth = "";
            switch (month)
            {
                case "1": txtMonth = "มกราคม"; break;
                case "2": txtMonth = "กุมภาพันธ์"; break;
                case "3": txtMonth = "มีนาคม"; break;
                case "4": txtMonth = "เมษายน"; break;
                case "5": txtMonth = "พฤษภาคม"; break;
                case "6": txtMonth = "มิถุนายน"; break;
                case "7": txtMonth = "กรกฎาคม"; break;
                case "8": txtMonth = "สิงหาคม"; break;
                case "9": txtMonth = "กันยายน"; break;
                case "10": txtMonth = "ตุลาคม"; break;
                case "11": txtMonth = "พฤศจิกายน"; break;
                case "12": txtMonth = "ธันวาคม"; break;
            }
            return txtMonth;
        }
    }
}