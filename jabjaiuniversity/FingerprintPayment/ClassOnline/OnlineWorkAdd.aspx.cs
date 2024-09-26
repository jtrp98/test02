using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;

using System.Data.Entity;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Globalization;
using FingerprintPayment.ClassOnline.Helper;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineWorkAdd : BaseOnlinePage
    {

        protected List<SelectRoom> ListSelectRoom
        {
            get
            {
                object o = ViewState["ListSelectRoom"];
                return o as List<SelectRoom>;
            }

            set
            {
                ViewState["ListSelectRoom"] = value;
            }
        }

        [Serializable]
        public class SelectRoom
        {
            public string SubLevel { get; set; }
            public string nTSubLevel2 { get; set; }
            public int nTermSubLevel2 { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            if (!this.IsPostBack)
            {

                //if (PermissionType.Modify != Permission)
                //{
                //    Response.Redirect("~/NoPermission.aspx");
                //}

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
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    //var d2 = from a in ctx.TSubLevels.Where(o => o.nTSubLevel == levelId)
                    //         join b in ctx.TTermSubLevel2 on a.nTSubLevel equals b.nTSubLevel
                    //         select new { }

                    //SetListLevel(d1);
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;
                    var isSuperUser = IsSuperUser();


                    var qryGroup = ctx.TClassOnlines
                      .Where(o => o.SchoolId == SchoolId && o.PlanId == planId && o.TermId == termId && o.LevelId == levelId  && (o.cDel ?? false) == false);

                    if (!isSuperUser)
                    {
                        qryGroup = qryGroup.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                    }

                    var g = qryGroup.ToList();

                    //if (g.Count == 0)
                    {
                        var q = his.TClassOnlines
                            .Where(o => o.SchoolId == SchoolId && o.PlanId == planId && o.TermId == termId && o.LevelId == levelId  && (o.cDel ?? false) == false);

                        if (!isSuperUser)
                        {
                            q = q.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                        }

                        var d = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(q.ToList());

                        g = g.Concat(d).ToList();
                    }

                    var lstGroup = g
                        .AsEnumerable()
                        .Select(o => new ListItem { Value = o.OnlineId + "", Text = o.TitleName + "" })
                        .Distinct()
                        .OrderBy(o => o.Text)
                        .ToList();

                    lstGroup.Insert(0, new ListItem { Value = "", Text = "- เลือก -" });
                    ddlGroup.DataSource = lstGroup;
                    ddlGroup.DataBind();

                    //var _class = ctx.TClassOnlines
                    //    .Where(o => o.SchoolId == SchoolId && o.PlanId == planId && o.TermId == termId && o.LevelId == levelId && o.cDel != true
                    //      //&&  ( o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")) 
                    //      )
                    //    .FirstOrDefault();

                    //var arrRoom = (_class?.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();

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
    --and Cu.IsActive = 1  and Cu.SchoolId = 247
    and(CT.nTerm = '{termId}' )--and T.cDel IS NULL
    and C.SchoolID = {SchoolId} { (isSuperUser ? "" : "and TT.sEmp = " + teacherId)} 
    and TT.IsActive = 1 
    and ISNULL(TT.cDel,0) = 0
"
                        );

                    ListSelectRoom = qr1.ToList();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {

                var termId = Request.QueryString["term"] + "";
                var planId = ToNullableInt(Request.QueryString["plan"] + "");
                var levelId = ToNullableInt(Request.QueryString["level"] + "");
                var teacherId = UserData.UserID;
                var SchoolId = UserData.CompanyID;

                var hw = new JabjaiEntity.DB.THomework();
                // hw.nHomeWork = ctx.THomeworks.Max(i => i.nHomeWork) + 1;
                hw.TitleName = txtTitle.Text;
                hw.sHomeworkDetail = txtDetail.Text;
                //hw.sHomeworkDetail = tbFiles.Text;
                hw.LinkYT = txtLink.Text;
                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);
                hw.dNotification = DateTime.Now.AddMinutes(3);
                hw.dStart = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", new CultureInfo("th-TH"));
                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                if (!string.IsNullOrEmpty(txtScore.Text))
                {
                    hw.MaxScore = Convert.ToDouble(txtScore.Text);
                }
                hw.sPlaneID = planId;
                hw.sEmp = teacherId;
                hw.Created = DateTime.Now;
                hw.Modified = DateTime.Now;
                hw.SchoolID = SchoolId;
                hw.cDel = false;

                var lstAssign = new List<string>();
                if (hw.AssignType == 1)
                {
                    var _lst = from ListItem li in ddlLevel.Items
                               where li.Selected
                               select li.Value;

                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                    hw.SelectedStudent = "";
                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == termId  && o.cDel == null)
                               .Select(o => o.sID + "")
                               .AsQueryable()
                               .ToList();
                }
                else if (hw.AssignType == 2)
                {
                    var _lst = from ListItem li in ddlStudent.Items
                               where li.Selected
                               select li.Value;

                    hw.SelectedStudent = "|" + string.Join("|", _lst) + "|";
                    hw.SelectedRoom = "";
                    lstAssign = _lst.ToList();
                }

                hw.DisplayType = Convert.ToByte(ddlDisplay.SelectedValue);
                if (ddlDisplay.SelectedValue == "1")
                {
                    hw.DisplayDate = DateTime.Now;
                }
                else
                {
                    hw.DisplayDate = DateTime.ParseExact(txtDisplayDate.Text + " " + txtDisplayTime.Text, "dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                }

                ctx.THomeworks.Add(hw);
                ctx.SaveChanges();

                var files = tbFiles.Text.Split('?');

                if (files.Length > 0)
                {
                    foreach (var f in files)
                    {
                        if (f == "") continue;

                        var arr = f.Split('|');

                        var title = arr[0];
                        var type = arr[1];
                        var data = arr[2];

                        if (title == "") continue;

                        string linkfiles = UploadFile.UploadFileFromBase64(title, data
                            , "learningonline/work"
                            , SchoolId
                            , hw.nHomeWork);

                        if (!string.IsNullOrEmpty(linkfiles))
                        {
                            ctx.THomeWorkFiles.Add(new JabjaiEntity.DB.THomeWorkFile
                            {
                                ContentType = type,
                                nHomeWorkId = hw.nHomeWork,
                                sFileName = linkfiles,
                                Title = title,
                                SchoolID = SchoolId,
                                cDel = false,
                            });
                        }
                    }

                    ctx.SaveChanges();
                }

                foreach (var userId in lstAssign.Distinct())
                {
                    ctx.THomework_User.Add(new JabjaiEntity.DB.THomework_User
                    {
                        nHomeWork = hw.nHomeWork,
                        sID = Convert.ToInt32(userId),
                        SchoolID = SchoolId,
                        CreatedDate = DateTime.Now,
                        cDel = false
                    });
                }

                ctx.SaveChanges();

                var _lstRoom = from ListItem li in ddlLevel.Items
                               where li.Selected
                               select li.Text;

                database.InsertLog(teacherId + "", $"สร้าง งาน/การบ้าน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                Response.Redirect("OnlineManage.aspx?" + Request.QueryString);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("OnlineManage.aspx?" + Request.QueryString);
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue != "")
            {
                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                    {
                        //var termId = Request.QueryString["term"] + "";
                        //var planId = ToNullableInt(Request.QueryString["plan"] + "");
                        //var levelId = ToNullableInt(Request.QueryString["level"] + "");
                        //var teacherId = Convert.ToInt32(Session["sEmpID"] + "");
                        //var SchoolId = Convert.ToInt32(Session["nCompany"] + "");

                        var id = ToNullableInt(ddlGroup.SelectedValue);
                        var _class = ctx.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID);

                        if (_class == null)
                        {
                            var c = his.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID);

                            _class = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                        }

                        var arrRoom = (_class?.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();

                        var lstLevel = ListSelectRoom.Where(o => arrRoom.Contains(o.nTermSubLevel2 + ""))
                          .OrderBy(o => o.nTSubLevel2)
                          .Select(o => new ListItem
                          {
                              Text = o.SubLevel + "/" + o.nTSubLevel2,
                              Value = o.nTermSubLevel2 + "",
                          });
                        ddlLevel.DataSource = lstLevel.Distinct();
                        ddlLevel.DataBind();

                        var _arr = lstLevel.Select(o => o.Value).ToList();
                        var lstStudent = ctx.TB_StudentViews
                            .Where(o => o.nTerm == _class.TermId && o.nTSubLevel == _class.LevelId && _arr.Contains(o.nTermSubLevel2 + "") && o.SchoolID == UserData.CompanyID && o.cDel == null)
                            .OrderBy(o => o.nTermSubLevel2)
                            .ThenBy(o => o.sStudentID)
                            .AsEnumerable()
                            .Select(o => new ListItem
                            {
                                Value = o.sID + "",
                                Text = $"{o.titleDescription} {o.sName} {o.sLastname} ({o.SubLevel}/{o.nTSubLevel2})",
                            })
                            .ToList();
                        //lst3.Insert(0, new ListItem { Value = "", Text = "เลือกนักเรียน" });
                        ddlStudent.DataSource = lstStudent;
                        ddlStudent.DataBind();
                    }
                }

            }
        }
    }
}