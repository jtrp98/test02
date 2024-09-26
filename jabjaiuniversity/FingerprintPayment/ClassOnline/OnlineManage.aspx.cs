using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using Ninject.Planning;
using System.Security.Cryptography;
using JabjaiSchoolHistoryEntity;
using System.Data.Entity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineManage : BaseOnlinePage
    {
        public class SelectRoom
        {
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int nTermSubLevel2 { get; set; }
        }
        protected class WorkList
        {
            public int SchooldID { get; set; }
            public int Id { get; set; }
            public int OnlineId { get; set; }
            public string Group { get; set; }
            public string Title { get; set; }
            public string Recieve { get; set; }
            public string Send { get; set; }
            public DateTime? SendDate { get; set; }
            public DateTime? DisplayDate { get; set; }
            public DateTime? Created { get; internal set; }
            public int Type { get; internal set; }
            public bool IsDel { get; internal set; }
            public int? TeacherId { get; internal set; }
            public string ShareId { get; internal set; }
            public DateTime? OrderDate { get; internal set; }
            public DateTime? GroupDate { get; internal set; }
        }

        protected List<JabjaiEntity.DB.TClassOnline> _lstTopic = new List<JabjaiEntity.DB.TClassOnline>();
        protected List<WorkList> _lstWork = new List<WorkList>();

        //protected string termId = "";
        //protected int? planId = null;
        protected TPlane _plan = new TPlane();
        protected TSubLevel _level = new TSubLevel();



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
                else
                {
                    Response.Redirect("OnlineMain.aspx");
                }
            }
        }

        private void LoadData(string termId, int? planId, int? levelId)
        {

            using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                //ctx.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var repo = new ClassroomRepository(ctx, db);

                //using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {

                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;

                    _plan = ctx.TPlanes.Where(o => o.sPlaneID == planId && o.SchoolID == SchoolId).FirstOrDefault();
                    _level = ctx.TSubLevels.Where(o => o.nTSubLevel == levelId && o.SchoolID == SchoolId).FirstOrDefault();

                    _lstTopic = repo.GetClassOnineGroupList(SchoolId, termId, levelId, planId, teacherId);

                    var qr1 = ctx.Database.SqlQuery<SelectRoom>(
    $@"select        
    TL1.SubLevel ,  TL2.nTSubLevel2 , TL2.nTermSubLevel2
    from TPlanCourse C
    join TPlane P ON C.sPlaneID = P.sPlaneID and C.SchoolID = P.SchoolID
    join TPlanTermSubLevel2 TS2 ON C.PlanId = TS2.PlanId and TS2.SchoolID = C.SchoolID
    join TPlan ON C.PlanId = TPlan.PlanId and C.SchoolID = TPlan.SchoolID
    join TPlanCourseTerm CT ON CT.PlanCourseId = C.PlanCourseId and CT.IsActive = 1 and CT.SchoolID = C.SchoolID
    join TPlanCourseTeacher TT on c.PlanCourseId = TT.PlanCourseId  and c.SchoolID = TT.SchoolID
    join TTermSubLevel2 TL2 on  TL2.nTermSubLevel2 = TS2.nTermSubLevel2  and TL2.SchoolID = TS2.SchoolID
    join TSubLevel TL1 ON TL2.nTSubLevel = TL1.nTSubLevel   and TL2.SchoolID = TL1.SchoolID

where 
    C.CourseStatus = 1 and C.IsActive = 1
    and C.sPlaneID = {planId}
    and P.cDel IS NULL and(P.nTSubLevel = {levelId})
    and TPlan.IsActive = 1 and(TPlan.nTSubLevel = {levelId})
    --and(TS2.nTermSubLevel2 = @nTermSubLevel2 or @nTermSubLevel2 = 0)
    and TS2.IsActive = 1
    --and Cu.IsActive = 1  and Cu.SchoolId = 
    and(CT.nTerm = '{termId}' )--and T.cDel IS NULL
    and C.SchoolID = {SchoolId}  {(IsSuperUser() ? "" : "and TT.sEmp = " + teacherId)} 
    and TT.IsActive = 1 
    and ISNULL(TT.cDel,0) = 0
"
        );

                    var lst1 = qr1.AsEnumerable()
                        .OrderBy(o => o.nTSubLevel2)
                        .Select(o => new ListItem
                        {
                            Text = o.SubLevel + "/" + o.nTSubLevel2,
                            Value = o.nTermSubLevel2 + "",
                        });

                    ddlLevel.DataSource = lst1.Distinct();
                    ddlLevel.DataBind();

                    var _studentView = ctx.TB_StudentViews
                        .Where(o => o.SchoolID == SchoolId && o.cDel == null && o.nTerm == termId && o.nTSubLevel == levelId)
                        .Select(o => new
                        {
                            o.nTermSubLevel2,
                            o.sID,
                        })

                        .ToList();

                    var onlineids = _lstTopic.Select(o => o.OnlineId).ToArray();

                    var _homework = repo.GetHomeWorkListByOnlineIds(SchoolId, onlineids);
                    var _learn = repo.GetLearningListByOnlineIds(SchoolId, onlineids);

                    var qry1 = from a in _lstTopic
                               from b in _homework.Where(o => o.OnlineId == a.OnlineId)

                               select new
                               {
                                   Id = b.nHomeWork,
                                   OnlineId = a.OnlineId,
                                   Group = a.TitleName,
                                   Title = b.TitleName,
                                   SendDate = b.dStart,
                                   b.AssignType,
                                   b.SelectedRoom,
                                   b.SelectedStudent,

                                   Created = b.Created,
                                   Type = 1,
                                   SchooldID = a.SchoolId.Value,
                                   TeacherId = a.TeacherId,
                                   ShareId = a.ShareId,
                                   OrderDate = a.Created,
                                   DisplayDate = b.DisplayDate,
                                   GroupDate = a.Created,
                                   //IsDel = ctx.THomework_User.Where( o => o.nHomeWork == b.nHomeWork && )
                               };

                    var _lst1 = qry1.ToList();

                    var homeworkIds = _lst1.Select(o => o.Id).Distinct().ToArray();
                    var studentList = repo.GetStudentListByHomeWorkIds(SchoolId, homeworkIds);

                    var d1 = from a in _lst1

                             select new WorkList
                             {
                                 Id = a.Id,
                                 OnlineId = a.OnlineId,
                                 Group = a.Group,
                                 Title = a.Title,
                                 SendDate = a.SendDate,
                                 Created = a.Created,
                                 Type = 1,
                                 SchooldID = a.SchooldID,
                                 TeacherId = a.TeacherId,
                                 ShareId = a.ShareId,
                                 OrderDate = a.Created,
                                 GroupDate = a.GroupDate,
                                 DisplayDate = a.DisplayDate,
                                 Recieve = (
                                             from b in _studentView.Where(o => (a.AssignType == 1 && (a.SelectedRoom).Contains("|" + o.nTermSubLevel2 + "|")
                                                || (a.AssignType == 2 && (a.SelectedStudent + "").Contains("|" + o.sID + "|")))
                                             )
                                             select b.sID
                                             ).Distinct().Count() + "",

                                 Send = (
                                             from x in studentList.Where(o => o.nHomeWork == a.Id && o.IsSend == true)
                                             from b in _studentView.Where(o => o.sID == x.sID &&
                                                 (a.AssignType == 1 && (a.SelectedRoom).Contains("|" + o.nTermSubLevel2 + "|")
                                                     || (a.AssignType == 2 && (a.SelectedStudent + "").Contains("|" + o.sID + "|"))
                                                  )
                                             )
                                             select b.sID
                                             ).Distinct().Count() + "",
                             };

                    var qry2 = from a in _lstTopic
                               from b in _learn.Where(o => o.OnlineId == a.OnlineId)

                               select new
                               {
                                   Id = b.LearnId,
                                   OnlineId = a.OnlineId,
                                   Group = a.TitleName,
                                   Title = b.TitleName,
                                   Created = b.Created,

                                   b.AssignType,
                                   b.SelectedRoom,
                                   b.SelectedStudent,


                                   Send = "-",
                                   Type = 2,
                                   SchooldID = a.SchoolId.Value,
                                   TeacherId = a.TeacherId,
                                   ShareId = a.ShareId,
                                   OrderDate = a.Created,
                                   DisplayDate = b.DisplayDate,
                                   GroupDate = a.Created,
                               };

                    var _lst2 = qry2.ToList();

                    var d2 = from a in _lst2

                             select new WorkList
                             {
                                 Id = a.Id,
                                 OnlineId = a.OnlineId,
                                 Group = a.Group,
                                 Title = a.Title,

                                 Created = a.Created,
                                 Type = 2,
                                 SchooldID = a.SchooldID,
                                 TeacherId = a.TeacherId,
                                 ShareId = a.ShareId,
                                 OrderDate = a.Created,
                                 DisplayDate = a.DisplayDate,
                                 GroupDate = a.GroupDate,
                                 Recieve = (
                                             from b in _studentView.Where(o => (a.AssignType == 1 && (a.SelectedRoom).Contains("|" + o.nTermSubLevel2 + "|")
                                                || (a.AssignType == 2 && (a.SelectedStudent + "").Contains("|" + o.sID + "|")))
                                             )
                                             select b.sID
                                             ).Distinct().Count() + "",

                             };

                    _lstWork = d1.Concat(d2).OrderBy(o => o.GroupDate).ThenBy(o => o.OrderDate).ToList();

                    var qry3 = from a in ctx.TPlanCourses.Where(o => o.sPlaneID == planId && o.SchoolID == SchoolId && o.IsActive == true && o.cDel == false && o.CourseStatus == 1)
                               from z in ctx.TPlans.Where(o => o.PlanId == a.PlanId && o.nTSubLevel == levelId && o.IsActive == true && o.cDel == false && o.SchoolID == a.SchoolID)
                               from b in ctx.TPlanCourseTeachers.Where(o => o.PlanCourseId == a.PlanCourseId && o.IsActive == true && o.cDel == false && o.SchoolID == a.SchoolID)
                               from c in ctx.TPlanCourseTerms.Where(o => o.PlanCourseId == a.PlanCourseId && o.IsActive == true && o.nTerm == termId && o.SchoolID == a.SchoolID)
                               from d in ctx.TEmployees.Where(o => o.sEmp == b.sEmp && o.cDel != "1" && o.SchoolID == b.SchoolID)
                               from e in ctx.TTitleLists.Where(o => o.nTitleid + "" == d.sTitle && o.SchoolID == d.SchoolID).DefaultIfEmpty()

                               select new
                               {
                                   Teacher = (e == null ? d.sTitle : e.titleDescription) + " " + d.sName + " " + d.sLastname,
                                   b.sEmp,
                                   a.PlanId,
                               };

                    var lst3 = qry3.Distinct().ToList();

                    var planList = lst3.Where(o => o.sEmp == UserData.UserID).Select(o => o.PlanId).ToList();

                    //var q3 = lst3.Where(o => planList.Contains(o.PlanId))
                    var teacherList = lst3.Where(o => planList.Contains(o.PlanId) /*&& o.sEmp != teacherId*/)
                        .OrderBy(o => o.sEmp)
                        .Select(o => new ListItem
                        {
                            Text = o.Teacher,
                            Value = o.sEmp + "",
                        }).ToList();


                    if (teacherList.Count == 0)
                    {
                        teacherList.Add(new ListItem()
                        {
                            Text = "ท่านไม่สามารถแชร์ร่วมกับผู้อื่นได้ เนื่องจากวิชาและแผนที่ท่านสอนไม่ได้มีการตั้งค่าชื่อครูผู้สอนท่านอื่นร่วม",
                            Value = "",
                            Enabled = false,
                        });

                        ddlShare.Attributes.Add("data-isempty", "1");
                    }
                    ddlShare.DataSource = teacherList.Distinct();
                    ddlShare.DataBind();
                }
            }
        }

        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var termId = Request.QueryString["term"] + "";
                    var planId = ToNullableInt(Request.QueryString["plan"] + "");
                    var levelId = ToNullableInt(Request.QueryString["level"] + "");
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;

                    var _lst1 = from ListItem li in ddlLevel.Items
                                where li.Selected
                                select li.Value;

                    var _lst2 = from ListItem li in ddlShare.Items
                                where li.Selected
                                select li.Value;

                    if (!string.IsNullOrEmpty(txtOnlineID.Text))
                    {
                        var id = Convert.ToInt32(txtOnlineID.Text);
                        var item = ctx.TClassOnlines.Where(o => o.OnlineId == id).FirstOrDefault();

                        if (item == null)
                        {
                            var itemHis = his.TClassOnlines.Where(o => o.OnlineId == id).FirstOrDefault();
                            itemHis.TitleName = txtTitleGroup.Text;
                            itemHis.Modfied = DateTime.Now;
                            itemHis.SelectedRoom = "|" + string.Join("|", _lst1) + "|";
                            itemHis.ShareId = "|" + string.Join("|", _lst2) + "|";
                            his.SaveChanges();
                        }
                        else
                        {
                            item.TitleName = txtTitleGroup.Text;
                            item.Modfied = DateTime.Now;
                            item.SelectedRoom = "|" + string.Join("|", _lst1) + "|";
                            item.ShareId = "|" + string.Join("|", _lst2) + "|";
                            ctx.SaveChanges();
                        }

                        database.InsertLog(teacherId + "", $"สร้าง หัวข้อใหญ่ {txtTitleGroup.Text}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                    }
                    else
                    {
                        var item = new JabjaiEntity.DB.TClassOnline();
                        item.TermId = termId;
                        item.PlanId = planId;
                        item.LevelId = levelId;
                        item.TitleName = txtTitleGroup.Text;
                        item.Created = DateTime.Now;
                        item.Modfied = DateTime.Now;
                        item.SelectedRoom = "|" + string.Join("|", _lst1) + "|";
                        item.ShareId = "|" + string.Join("|", _lst2) + "|";
                        item.TeacherId = UserData.UserID;
                        item.SchoolId = UserData.CompanyID;
                        item.cDel = false;

                        ctx.TClassOnlines.Add(item);

                        ctx.SaveChanges();

                        database.InsertLog(teacherId + "", $"แก้ไข หัวข้อใหญ่ {txtTitleGroup.Text}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                    }

                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOnlineID.Text))
            {
                var id = Convert.ToInt32(txtOnlineID.Text);

                using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    var repo = new ClassroomRepository(ctx, db);

                    //using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                    {
                       
                        var schoolId = UserData.CompanyID;
                        var online = repo.GetClassOnineGroupById(schoolId, id);

                        var room1 = repo.GetHomeWorkListByOnlineIds(schoolId, new int[] { id })
                            .Select(o => o.SelectedRoom)
                            .Distinct()
                            .AsEnumerable()
                            .Select(o => new
                            {
                                a = o.Split('|')
                            });

                      

                        var room2 = repo.GetLearningListByOnlineIds(schoolId, new int[] { id })
                            .Select(o => o.SelectedRoom)
                            .Distinct()
                            .AsEnumerable()
                            .Select(o => new
                            {
                                a = o.Split('|')
                            })
                            .ToList();
                     

                        var selectedRoom = new List<string>();

                        foreach (var h in room1)
                        {
                            foreach (var i in h.a)
                            {
                                selectedRoom.Add(i);
                            }
                        }
                   
                        foreach (var h in room2)
                        {
                            foreach (var i in h.a)
                            {
                                selectedRoom.Add(i);
                            }
                        }

                        ddlLevel.ClearSelection();
                        ddlShare.ClearSelection();

                        foreach (var item in (online.SelectedRoom + "").Split('|'))
                        {
                            var l = ddlLevel.Items.FindByValue(item);
                            if (l != null)
                                l.Selected = true;
                        }

                        foreach (var item in (online.ShareId + "").Split('|'))
                        {
                            var l = ddlShare.Items.FindByValue(item);
                            if (l != null)
                                l.Selected = true;
                        }

                        txtTitleGroup.Text = online.TitleName;

                        txtSelectedRoom.Text = string.Join("|", selectedRoom.Where(o => o != "").Distinct().ToList());
                        //UpdatePanel1.Update();
                    }
                }
            }
        }

        protected void UpdatePanel1_Load(object sender, EventArgs e)
        {


        }
      
        protected void ButtonDelWork_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(hfDelWork.Value))
            {
                var teacherId = UserData.UserID;
                var SchoolId = UserData.CompanyID;
                var id = Convert.ToInt32(hfDelWork.Value);

                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    var home = ctx.THomeworks.Where(o => o.nHomeWork == id).FirstOrDefault();

                    home.cDel = true;

                    ctx.SaveChanges();

                    database.InsertLog(teacherId + "", $"ลบ งาน/การบ้าน {home.TitleName}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                }
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void ButtonDelLearn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfDelLearn.Value))
            {
                var teacherId = UserData.UserID;
                var SchoolId = UserData.CompanyID;
                var id = Convert.ToInt32(hfDelLearn.Value);

                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    var learn = ctx.THomeWorkLearnings.Where(o => o.LearnId == id).FirstOrDefault();

                    learn.cDel = true;

                    ctx.SaveChanges();

                    database.InsertLog(teacherId + "", $"ลบ คลังบทเรียน {learn.TitleName}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var teacherId = UserData.UserID;
                var SchoolId = UserData.CompanyID;

                if (!string.IsNullOrEmpty(txtOnlineID.Text))
                {
                    var id = Convert.ToInt32(txtOnlineID.Text);
                    var item = ctx.TClassOnlines.Where(o => o.OnlineId == id).FirstOrDefault();
                    item.cDel = true;

                    ctx.SaveChanges();

                    database.InsertLog(teacherId + "", $"ลบ หัวข้อใหญ่ {txtTitleGroup.Text}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                }

                Response.Redirect(Request.RawUrl);
            }
        }
    }
}