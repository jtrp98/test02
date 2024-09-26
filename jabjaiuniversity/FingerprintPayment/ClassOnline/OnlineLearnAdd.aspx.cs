﻿using JabjaiEntity.DB;
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
using FingerprintPayment.ClassOnline.Helper;
using System.Globalization;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineLearnAdd : BaseOnlinePage
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

            //if (PermissionType.Modify != Permission)
            //{
            //    Response.Redirect("~/NoPermission.aspx");
            //}

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

        //protected int? ToNullableInt(string s)
        //{
        //    int i;
        //    if (int.TryParse(s, out i)) return i;
        //    return null;
        //}

        //private void SetListTopic(List<TClassOnline> lst)
        //{
        //    var _lst = lst
        //       .Select(o => new ListItem { Value = o.OnlineId + "", Text = o.TitleName + "" })
        //       .Distinct()
        //       .OrderBy(o => o.Text)
        //       .ToList();
        //    _lst.Insert(0, new ListItem { Value = "", Text = "เลือกหัวข้อใหญ่" });
        //    ddlGroup.DataSource = _lst;
        //    ddlGroup.DataBind();
        //}

        //private void SetListLevel(List<TTermSubLevel2> lst)
        //{
        //    var _lst = lst
        //       .Select(o => new ListItem { Value = o.nTermSubLevel2 + "", Text = o.nTSubLevel2 + "" })
        //       .Distinct()
        //       .OrderBy(o => o.Text)
        //       .ToList();
        //    _lst.Insert(0, new ListItem { Value = "", Text = "เลือกหัวข้อใหญ่" });
        //    ddlGroup.DataSource = _lst;
        //    ddlGroup.DataBind();
        //}


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

                    //var lst2 = ctx.TClassOnlines
                    //    .Where(o => o.PlanId == planId && o.TermId == termId && o.LevelId == levelId)
                    //    .ToList();
                    var teacherId = UserData.UserID;// Convert.ToInt32(Session["sEmpID"] + "");
                    var SchoolId = UserData.CompanyID;// Convert.ToInt32(Session["nCompany"] + "");
                    var isSuperUser = IsSuperUser();

                    //var _class = ctx.TClassOnlines
                    // .Where(o => o.PlanId == planId && o.TermId == termId && o.LevelId == levelId && o.TeacherId == teacherId)
                    // .FirstOrDefault();

                    //var arrRoom = (_class.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();

                    var qryGroup = ctx.TClassOnlines
                     .Where(o => o.PlanId == planId && o.TermId == termId && o.LevelId == levelId && o.SchoolId == SchoolId && (o.cDel ?? false) == false);

                    if (!isSuperUser)
                    {
                        qryGroup = qryGroup.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                    }

                    var g = qryGroup.ToList();

                    //if (g.Count == 0)
                    {
                        var q = his.TClassOnlines
                            .Where(o => o.PlanId == planId && o.TermId == termId && o.LevelId == levelId && o.SchoolId == SchoolId && (o.cDel ?? false) == false);

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

                var hw = new JabjaiEntity.DB.THomeWorkLearning();
                // hw.nHomeWork = ctx.THomeworks.Max(i => i.nHomeWork) + 1;
                hw.TitleName = txtTitle.Text;
                hw.Description = txtDetail.Text;
                hw.LinkYT = txtLink.Text;
                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                hw.sPlaneID = planId;
                hw.sEmp = teacherId;
                hw.Created = DateTime.Now;
                hw.Modified = DateTime.Now;
                hw.SchoolId = SchoolId;
                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);
                hw.cDel = false;

                var lstAssign = new List<string>();
                if (hw.AssignType == 1)
                {
                    var _lst = from ListItem li in ddlLevel.Items
                               where li.Selected
                               select li.Value;

                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                    hw.SelectedStudent = "";

                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == termId )
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


                ctx.THomeWorkLearnings.Add(hw);

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
                            , hw.LearnId);

                        if (!string.IsNullOrEmpty(linkfiles))
                        {
                            ctx.THomeWorkLearningFiles.Add(new JabjaiEntity.DB.THomeWorkLearningFile
                            {
                                ContentType = type,
                                LearnId = hw.LearnId,
                                sFileName = linkfiles,
                                Title = title,
                                SchoolId = SchoolId,
                                cDel = false,
                            });
                        }
                    }

                    ctx.SaveChanges();
                }

                var _lstRoom = from ListItem li in ddlLevel.Items
                               where li.Selected
                               select li.Text;

                database.InsertLog(teacherId + "", $"สร้าง คลังบทเรียน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);


                ctx.SaveChanges();

                Response.Redirect("OnlineManage.aspx?" + Request.QueryString);
            }
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

                        var id = Convert.ToInt32(ddlGroup.SelectedValue);
                        var _class = ctx.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID);

                        if (_class == null)
                        {
                            var c = his.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID);

                            _class = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                        }

                        var arrRoom = (_class.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();

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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("OnlineManage.aspx?" + Request.QueryString);
        }
    }
}