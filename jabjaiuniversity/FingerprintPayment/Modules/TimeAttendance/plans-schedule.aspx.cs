using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using Ninject;
using SchoolBright.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class plans_schedule : System.Web.UI.Page
    {
        [Inject]
        public ITimeTableSettingService TimeTableSettingService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            //SiteMap.Provider.CurrentNode.ParentNode.ReadOnly = false;
            //SiteMap.CurrentNode.ParentNode.Url = "/Modules/TimeAttendance/plans-room.aspx?idterm=" + Server.UrlEncode(Request.QueryString["idterm"]);

            if (Session["sEntities"] == null) Response.Redirect("~/Default.aspx");
            JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read));
            if (!IsPostBack)
            {
                OpenData();
                OpenData2();
            }
        }
        private void OpenData()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            int nTermSubLevel2 = int.Parse(Request.QueryString["id"]);
            string idterm = Request.QueryString["idterm"];
            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
                {

                    var _listterm = _db.TTerms.Where(w => w.SchoolID == userData.CompanyID && w.nTerm == idterm).ToList();
                    foreach (var _data in _listterm)
                    {
                        ltrTerm.Text = _data.sTerm;
                        foreach (var _datayear in _db.TYears.Where(w => w.SchoolID == userData.CompanyID && w.nYear == _data.nYear.Value))
                        {
                            ltrYear.Text = _datayear.numberYear.Value.ToString();
                        }
                    }

                    var _listroom = from a in _db.TSubLevels.ToList()
                                    join b in _db.TTermSubLevel2 on a.nTSubLevel equals b.nTSubLevel
                                    where a.SchoolID == userData.CompanyID && b.SchoolID == userData.CompanyID && b.nTermSubLevel2 == nTermSubLevel2
                                    select new { a.SubLevel, b.nTSubLevel2 };
                    foreach (var _data in _listroom)
                    {
                        ltrLevel.Text = _data.SubLevel.Trim();
                        ltrSubLv.Text = _data.nTSubLevel2.ToString();
                    }

                    var homeTeacher = TimeTableSettingService.GetClassHomeTeacherInfo(nTermSubLevel2, idterm, userData.CompanyID);
                    if (homeTeacher != null && homeTeacher.SEmp > 0)
                    {
                        txtaddteacher.Value = homeTeacher.Name + " " + homeTeacher.Lastname;
                        txtaddteacherid.Value = homeTeacher.SEmp.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nTermSubLevel2:{0},idterm{1}", nTermSubLevel2, idterm);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "plans_schedule-OpenData", parameters, "", null);
            }
        }

        [WebMethod(EnableSession = true)]
        public static string getplane(int nTermSubLevel2, string term_Id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var planCourse = ServiceHelper.GetPlanCourses(0, nTermSubLevel2, term_Id, 0, userData.CompanyID, _db);
                    planCourse = planCourse.DistinctBy(d => new { CourseCode = d.CourseCode.Replace(" ", ""), d.CourseName }).OrderBy(x => x.CourseCode.Replace(" ", "")).ToList();
                    //Removed term because this course is based on the term selection only.
                    dynamic rss = new JArray(from a in planCourse
                                             select new JObject {
                                                 new JProperty("value",a.SPlaneId),
                                                 new JProperty("name",a.CourseCode + " " + a.CourseName)
                                             });

                    return rss.ToString();
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nTermSubLevel2:{0},idterm{1}", nTermSubLevel2, term_Id);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "plans_schedule-OpenData", parameters, "", null);
                return string.Empty;
            }

        }

        [WebMethod(EnableSession = true)]
        public static string GetPlanCourse(int nTermSubLevel2, string term_Id)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    var planCourse = ServiceHelper.GetPlanCourses(0, nTermSubLevel2, term_Id, 0, userData.CompanyID, _db);
                    planCourse = planCourse.DistinctBy(d => new { CourseCode = d.CourseCode.Replace(" ", ""), d.CourseName }).OrderBy(x => x.CourseCode.Replace(" ", "")).ToList();
                    //Removed term because this course is based on the term selection only.
                    dynamic rss = new JArray(from a in planCourse
                                             select new JObject {
                                                 new JProperty("value",a.SPlaneId),
                                                 new JProperty("name",a.CourseCode + "--" + a.CourseName)
                                             });

                    return rss.ToString();
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nTermSubLevel2:{0},idterm{1}", nTermSubLevel2, term_Id);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "plans_schedule-OpenData", parameters, "", null);
                return string.Empty;
            }

        }

        [WebMethod(EnableSession = true)]
        public static string GetTeachersByCourseCode(int nTermSubLevel2, string term_Id, string courseCode)
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            try
            {
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    
                    var nTSubLevel = _db.TTermSubLevel2.Where(w => w.SchoolID == userData.CompanyID && w.nTermSubLevel2 == nTermSubLevel2).Select(s => s.nTSubLevel).FirstOrDefault();
                    var sPlaneId = _db.TPlanes.Where(w => w.SchoolID == userData.CompanyID && w.nTSubLevel == nTSubLevel.ToString() && w.courseCode == courseCode).Select(s => s.sPlaneID).FirstOrDefault();
                    var planCourse = ServiceHelper.GetTeachersForAPlanCourse(nTermSubLevel2, term_Id, sPlaneId, userData.CompanyID, userData.UserID);
                   
                    //Removed term because this course is based on the term selection only.
                    dynamic rss = new JArray(from a in planCourse
                                             select new JObject {
                                                 new JProperty("SEmp",a.SEmp),
                                                 new JProperty("TeacherFullName",a.TeacherFullName)
                                             });

                    return rss.ToString();
                }
            }
            catch (Exception ex)
            {
                string parameters = string.Format("nTermSubLevel2:{0},idterm{1}", nTermSubLevel2, term_Id);
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "GetTeachersByCourseCode", parameters, "", null);
                return string.Empty;
            }

        }

        private void OpenData2()
        {
            JWTToken token = new JWTToken();
            var userData = new JWTToken().UserData;
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }
            string id = Request.QueryString["id"];
            string idterm = Request.QueryString["idterm"];
            try
            {
                
                int sEmpID = int.Parse(Session["sEmpID"] + "");

                using (JabJaiMasterEntities _dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities _db = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
                {
                    if (!IsPostBack)
                    {
                        List<TSubLevel> SubLevel = new List<TSubLevel>();
                        TSubLevel sub = new TSubLevel();

                        foreach (var a in _db.TSubLevels.Where(w => w.SchoolID == userData.CompanyID && w.nWorkingStatus == 1))
                        {
                            sub = new TSubLevel();
                            sub = a;
                            sub.SchoolID = userData.CompanyID;
                            SubLevel.Add(sub);
                        }

                        foreach (var t in SubLevel)
                        {
                            var item = new ListItem
                            {
                                Text = t.SubLevel,
                                Value = t.nTSubLevel.ToString()
                            };
                            ddlsublevel.Items.Add(item);
                        }

                        List<TYear> tylist = new List<TYear>();
                        TYear ty = new TYear();
                        foreach (var a in _db.TYears.Where(x => x.SchoolID == userData.CompanyID && x.cDel == false).ToList())
                        {
                            ty = new TYear();
                            ty = a;
                            ty.SchoolID = userData.CompanyID;
                            tylist.Add(ty);
                        }
                        var newList = tylist.OrderByDescending(x => x.numberYear).ToList();

                        DropDownList1.DataSource = newList;
                        DropDownList1.DataTextField = "numberYear";
                        DropDownList1.DataValueField = "numberYear";
                        DropDownList1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                SchoolBright.Business.Helper.Common.CreateExceptionLog("FingerprintPayment", ex, userData.CompanyID, userData.UserID, "plans_schedule-OpenData2", "", "", null);
            }
        }
    }
}