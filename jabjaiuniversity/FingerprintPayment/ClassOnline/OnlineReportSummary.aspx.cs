using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using Microsoft.Ajax.Utilities;
using System.Web.Services;
using FingerprintPayment.App_Logic;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using iTextSharp.text.log;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineReportSummary : BaseOnlinePage
    {
        protected TPlane _plan = new TPlane();
        protected TSubLevel _level = new TSubLevel();
        //protected TClassOnline _class = new TClassOnline();
        //protected StatTop _stat = new StatTop();
        protected List<SelectedRoom> _roomList = new List<SelectedRoom>();
        protected List<JabjaiEntity.DB.THomework> _homeworkList = new List<JabjaiEntity.DB.THomework>();
        protected List<ReportList> _reportList = new List<ReportList>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            if (!this.IsPostBack)
            {
                var termId = Request.QueryString["term"] + "";
                var planId = ToNullableInt(Request.QueryString["plan"] + "");
                var levelId = ToNullableInt(Request.QueryString["level"] + "");

                if (!string.IsNullOrEmpty(termId) && planId.HasValue && levelId.HasValue)
                {
                    LoadData(termId, planId, levelId);
                }
            }
        }

        private void LoadData(string termId, int? planId, int? levelId)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;

                    _plan = ctx.TPlanes.Where(o => o.sPlaneID == planId && o.SchoolID == SchoolId).FirstOrDefault();
                    _level = ctx.TSubLevels.Where(o => o.nTSubLevel == levelId && o.SchoolID == SchoolId).FirstOrDefault();

                    var q1 = ctx.TClassOnlines.Where(o => o.LevelId == levelId && o.TermId == termId && o.PlanId == planId
                        && o.SchoolId == SchoolId && (o.cDel ?? false) == false);

                    if (!IsSuperUser())
                    {
                        q1 = q1.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                    }

                    var room = (from a in q1
                                from b in ctx.THomeworks.Where(o => o.OnlineId == a.OnlineId && o.SchoolID == a.SchoolId && o.cDel != true)

                                select new
                                {
                                    nHomeWork = b.nHomeWork,
                                    //TitleName = b.TitleName,
                                    //AssignType = b.AssignType,
                                    SelectedRoom = b.SelectedRoom,
                                    SelectedStudent = b.SelectedStudent,
                                })
                             .OrderBy(o => o.nHomeWork)
                             .ToList();

                    if (room.Count == 0)
                    {
                        var q2 = his.TClassOnlines.Where(o => o.LevelId == levelId && o.TermId == termId && o.PlanId == planId
                       && o.SchoolId == SchoolId && (o.cDel ?? false) == false);

                        if (!IsSuperUser())
                        {
                            q2 = q2.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                        }

                        room = (from a in q2
                                from b in his.THomeworks.Where(o => o.OnlineId == a.OnlineId && o.SchoolID == a.SchoolId && o.cDel != true)

                                select new
                                {
                                    nHomeWork = b.nHomeWork,
                                    //TitleName = b.TitleName,
                                    //AssignType = b.AssignType,
                                    SelectedRoom = b.SelectedRoom,
                                    SelectedStudent = b.SelectedStudent,
                                })
                                .OrderBy(o => o.nHomeWork)
                                .ToList();
                    }

                    var arr = new List<int?>();
                    var users = ctx.TUser.Where(o => o.SchoolID == SchoolId).Select(o => new { o.sID, o.nTermSubLevel2 }).ToList();

                    foreach (var i in room)
                    {
                        foreach (var r in i.SelectedRoom.Split('|'))
                        {
                            var n = base.ToNullableInt(r);
                            if (n.HasValue)
                                arr.Add(n.Value);
                        }

                        foreach (var r in i.SelectedStudent.Split('|'))
                        {
                            var n = base.ToNullableInt(r);

                            if (n.HasValue)
                            {
                                var d = users.FirstOrDefault(o => o.sID == n);
                                if (d != null)
                                {
                                    arr.Add(d.nTermSubLevel2);
                                }
                            }
                        }
                    }

                    arr = arr.Where(o => o.HasValue).Distinct().ToList();

                    _roomList = (from a in ctx.TTermSubLevel2.Where(o => arr.Contains(o.nTermSubLevel2) && o.SchoolID == SchoolId)
                                 from b in ctx.TSubLevels.Where(o => o.nTSubLevel == a.nTSubLevel && o.SchoolID == SchoolId)
                                 select new SelectedRoom
                                 {
                                     Id = a.nTermSubLevel2,
                                     Room = b.SubLevel + "/" + a.nTSubLevel2
                                 })
                                 .OrderBy(o => o.Id)
                                 .ToList();
                }
            }
        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {
            var roomId = Request["__EVENTARGUMENT"];

            if (string.IsNullOrEmpty(roomId)) return;

            var teacherId = UserData.UserID;
            var SchoolId = UserData.CompanyID;
            var termId = Request.QueryString["term"] + "";
            var planId = ToNullableInt(Request.QueryString["plan"] + "");
            var levelId = ToNullableInt(Request.QueryString["level"] + "");

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {

                var sql = $@" 

 SELECT *
 FROM (
	Select *
	FROM (
		select B.nHomeWork 'HID', B.TitleName 'HomeWork'  , B.MaxScore
		, C.Score , C.sID	
		, ( CASE WHEN Z.titleDescription IS NULL THEN Z.sStudentTitle ELSE Z.titleDescription END ) +' ' + Z.sName + ' ' + Z.sLastname 'StudentName'		
		, Z.sStudentID , ISNULL(Z.nStudentNumber,9999) 'nStudentNumber' , Z.nTSubLevel2
		
		from JabjaiSchoolSingleDB.dbo.TClassOnline A 
		JOIN JabjaiSchoolSingleDB.dbo.THomework B ON A.OnlineId = B.OnlineId and A.SchoolId = B.SchoolID
		JOIN JabjaiSchoolSingleDB.dbo.THomework_User C ON B.nHomeWork = C.nHomeWork And B.SchoolID  = C.SchoolID 
		JOIN JabjaiSchoolSingleDB.dbo.TB_StudentViews Z ON C.sID = Z.sID And C.SchoolID  = Z.SchoolID and Z.cDel is null  and Z.nTerm = '{termId}'  and Z.nTSubLevel = {levelId} and Z.nTermSubLevel2 = {roomId}
		 
		where A.SchoolId = {SchoolId} 
        { (IsSuperUser() ? "" : " and ( A.TeacherId = " + teacherId + " or A.ShareId like '%|'+CAST(" + teacherId + " as varchar(10))+'|%' ) ")} 
		and A.TermId = '{termId}'
		and A.PlanId = {planId}
		and A.LevelId = {levelId}
		and ISNULL( A.cDel , 0) = 0
		and ISNULL( B.cDel , 0) = 0	
        AND ISNULL(C.cDel,0) = 0
		and Z.cDel is null  
		and ( B.SelectedRoom like '%|'+CAST(Z.nTermSubLevel2 as varchar(10))+'|%' OR B.SelectedStudent like '%|'+CAST(Z.sID as varchar(10))+'|%')
	)T

                
	UNION

	Select *
	FROM (
		select B.nHomeWork 'HID', B.TitleName 'HomeWork'  , B.MaxScore
		, C.Score , C.sID	
		, ( CASE WHEN Z.titleDescription IS NULL THEN Z.sStudentTitle ELSE Z.titleDescription END ) +' ' + Z.sName + ' ' + Z.sLastname 'StudentName'		
		, Z.sStudentID , ISNULL(Z.nStudentNumber,9999) 'nStudentNumber' , Z.nTSubLevel2
		
		from JabjaiSchoolHistory.dbo.TClassOnline A 
		JOIN JabjaiSchoolHistory.dbo.THomework B ON A.OnlineId = B.OnlineId and A.SchoolId = B.SchoolID
		JOIN JabjaiSchoolHistory.dbo.THomework_User C ON B.nHomeWork = C.nHomeWork And B.SchoolID  = C.SchoolID 
		JOIN JabjaiSchoolSingleDB.dbo.TB_StudentViews Z ON C.sID = Z.sID And C.SchoolID  = Z.SchoolID and Z.cDel is null  and Z.nTerm = '{termId}'  and Z.nTSubLevel = {levelId} and Z.nTermSubLevel2 = {roomId}
		 
		where A.SchoolId = {SchoolId} 
        { (IsSuperUser() ? "" : " and ( A.TeacherId = " + teacherId + " or A.ShareId like '%|'+CAST(" + teacherId + " as varchar(10))+'|%' ) ")} 
		and A.TermId = '{termId}'
		and A.PlanId = {planId}
		and A.LevelId = {levelId}
		and ISNULL( A.cDel , 0) = 0
		and ISNULL( B.cDel , 0) = 0	
        AND ISNULL(C.cDel,0) = 0
		and Z.cDel is null  
		and ( B.SelectedRoom like '%|'+CAST(Z.nTermSubLevel2 as varchar(10))+'|%' OR B.SelectedStudent like '%|'+CAST(Z.sID as varchar(10))+'|%')
	)T
)T
order by T.nTSubLevel2 ,T.nStudentNumber , T.sStudentID
                
                ";

                _reportList = ctx.Database.SqlQuery<ReportList>(sql).AsQueryable().ToList();
            }

            System.Threading.Thread.Sleep(1000);
            //UpdatePanel1.Update();
        }


        protected void btnLoadRoom_Click(object sender, EventArgs e)
        {
            var roomId = Request["__EVENTARGUMENT"];
        }
    }

    public class SelectedRoom
    {
        public int Id { get; set; }
        public string Room { get; set; }
    }

    public class ReportList
    {
        public int HID { get; set; }
        public string HomeWork { get; set; }
        public double? MaxScore { get; set; }
        public double? Score { get; set; }
        public int? sID { get; set; }
        public string sStudentID { get; set; }
        public int? nStudentNumber { get; set; }
        public string StudentName { get; set; }
    }
}
