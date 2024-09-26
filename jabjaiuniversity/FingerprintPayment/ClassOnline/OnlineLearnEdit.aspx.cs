using JabjaiEntity.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MasterEntity;
using JabjaiMainClass;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Globalization;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using FingerprintPayment.ClassOnline.Helper;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineLearnEdit : BaseOnlinePage
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

        public class Files
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string File { get; set; }
            public string TitleFile { get; set; }
            public string ContentType { get; set; }
        }

        protected JabjaiEntity.DB.THomeWorkLearning _homework = null;
        protected JabjaiEntity.DB.TClassOnline _class = null;
        protected List<Files> files = new List<Files>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");


            if (!this.IsPostBack)
            {
                var id = ToNullableInt(Request.QueryString["id"] + "");

                if (id.HasValue)
                {
                    LoadData(id);

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


        private void LoadData(int? id)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;
                    var isSuperUser = IsSuperUser();

                    _homework = ctx.THomeWorkLearnings.FirstOrDefault(o => o.SchoolId == SchoolId && o.LearnId == id);

                    if (_homework == null)
                    {
                        var h = his.THomeWorkLearnings.FirstOrDefault(o => o.SchoolId == SchoolId && o.LearnId == id );
                        _homework = CloneObject<JabjaiEntity.DB.THomeWorkLearning, JabjaiSchoolHistoryEntity.THomeWorkLearning>(h);
                    }

                    txtTitle.Text = _homework.TitleName;
                    txtDetail.Text = _homework.Description;
                    txtLink.Text = _homework.LinkYT;
                    ddlType.SelectedValue = _homework.AssignType + "";
                    txtDisplayDate.Text = _homework.DisplayDate?.ToString(@"dd/MM/yyyy", new CultureInfo("th-TH"));
                    txtDisplayTime.Text = _homework.DisplayDate?.ToString(@"HH:mm", new CultureInfo("th-TH"));
                    ddlDisplay.SelectedValue = _homework.DisplayType + "";

                    files = ctx.THomeWorkLearningFiles
                        .Where(o => o.SchoolId == SchoolId &&  o.LearnId == id  && (o.cDel ?? false) == false)
                        .AsEnumerable()
                        .Select(o => new Files
                        {
                            Id = o.AttachId,
                            Type = CommonHelper.GetType(o.ContentType),
                            File = o.sFileName,
                            TitleFile = o.Title,
                            ContentType = o.ContentType,
                        })
                        .ToList();

                    //if (files.Count == 0)
                    {
                        var f = his.THomeWorkLearningFiles
                          .Where(o => o.SchoolId == SchoolId && o.LearnId == id  && o.cDel != true)
                          .AsEnumerable()
                          .Select(o => new Files
                          {
                              Id = o.AttachId,
                              Type = CommonHelper.GetType(o.ContentType),
                              File = o.sFileName,
                              TitleFile = o.Title,
                              ContentType = o.ContentType,
                          })
                          .ToList();

                        files = files.Concat(f).ToList();
                    }

                    _class = ctx.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == _homework.OnlineId  && (o.cDel ?? false) == false);

                    if (_class == null)
                    {
                        var c = his.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == _homework.OnlineId  && (o.cDel ?? false) == false);

                        _class = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                    }

                    var qryGroup = ctx.TClassOnlines
                    .Where(o => o.SchoolId == SchoolId && o.PlanId == _class.PlanId && o.TermId == _class.TermId && o.LevelId == _class.LevelId  && (o.cDel ?? false) == false);

                    if (!isSuperUser)
                    {
                        qryGroup = qryGroup.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                    }

                    var g = qryGroup.ToList();

                    //if (g.Count == 0)
                    {
                        var q = his.TClassOnlines
                     .Where(o => o.SchoolId == SchoolId && o.PlanId == _class.PlanId && o.TermId == _class.TermId && o.LevelId == _class.LevelId  && (o.cDel ?? false) == false);

                        if (!isSuperUser)
                        {
                            q = q.Where(o => (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")));
                        }

                        var d = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(q.ToList());

                        g = g.Concat(d).ToList();
                    }

                    var lstGroup = g
                        .Select(o => new ListItem { Value = o.OnlineId + "", Text = o.TitleName + "" })
                        .Distinct()
                        .OrderBy(o => o.Text)
                        .ToList();

                    lstGroup.Insert(0, new ListItem { Value = "", Text = "- เลือก -" });
                    ddlGroup.DataSource = lstGroup;
                    ddlGroup.DataBind();
                    ddlGroup.SelectedValue = _homework.OnlineId + "";

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
    and C.sPlaneID = {_homework.sPlaneID}
    and P.cDel IS NULL and(P.nTSubLevel = {_class.LevelId})
    and TPlan.IsActive = 1 and(TPlan.nTSubLevel = {_class.LevelId})
    --and(TS2.nTermSubLevel2 = @nTermSubLevel2 or @nTermSubLevel2 = 0)
    and TS2.IsActive = 1
    --and Cu.IsActive = 1  and Cu.SchoolId = 247
    and(CT.nTerm = '{_class.TermId}' )--and T.cDel IS NULL
    and C.SchoolID = {SchoolId} { (isSuperUser ? "" : "and TT.sEmp = " + teacherId)} 
    and TT.IsActive = 1 
    and ISNULL(TT.cDel,0) = 0
"
                      );

                    ListSelectRoom = qr1.ToList();

                    var arrRoom = (_class?.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();

                    var lstLevel = ListSelectRoom.Where(o => arrRoom.Contains(o.nTermSubLevel2 + ""))
                        .OrderBy(o => o.nTSubLevel2)
                        .Select(o => new ListItem
                        {
                            Text = o.SubLevel + "/" + o.nTSubLevel2,
                            Value = o.nTermSubLevel2 + "",
                        });

                    //lst1.Insert(0, new ListItem { Value = "", Text = "เลือกห้อง" });
                    ddlLevel.DataSource = lstLevel.Distinct();
                    ddlLevel.DataBind();

                    foreach (var item in (_homework.SelectedRoom + "").Split('|'))
                    {
                        var l = ddlLevel.Items.FindByValue(item);
                        if (l != null)
                            l.Selected = true;
                    }

                    var _arr = lstLevel.Select(o => o.Value).ToList();
                    var lstStudent = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId &&  o.nTerm == _class.TermId && o.nTSubLevel == _class.LevelId && _arr.Contains(o.nTermSubLevel2 + "") &&  o.cDel == null)
                        .AsEnumerable()
                        .Select(o => new ListItem
                        {
                            Value = o.sID + "",
                            Text = $"{o.titleDescription} {o.sName} {o.sLastname} ({o.SubLevel}/{o.nTSubLevel2})",
                        })
                        .AsQueryable().ToList();
                    //lst3.Insert(0, new ListItem { Value = "", Text = "เลือกนักเรียน" });
                    ddlStudent.DataSource = lstStudent;
                    ddlStudent.DataBind();
                    foreach (var item in (_homework.SelectedStudent + "").Split('|'))
                    {
                        var l = ddlStudent.Items.FindByValue(item);
                        if (l != null)
                            l.Selected = true;
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isHis = false;
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;
                    var id = ToNullableInt(Request.QueryString["id"] + "");
                    var url = "";

                    var onlineID = ctx.THomeWorkLearnings.Where(o => o.SchoolId == SchoolId && o.LearnId == id )
                        .Select(o => o.OnlineId)
                        .FirstOrDefault();

                    if (onlineID == null)
                    {
                        isHis = true;

                        onlineID = his.THomeWorkLearnings.Where(o => o.SchoolId == SchoolId && o.LearnId == id )
                           .Select(o => o.OnlineId)
                           .FirstOrDefault();
                    }

                    var online = ctx.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == onlineID  && (o.cDel ?? false) == false);

                    if (online == null)
                    {
                        var c = his.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == onlineID  && (o.cDel ?? false) == false);

                        online = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                    }

                    if (isHis == false)
                    {
                     
                            try
                            {
                                // var hw = ctx.THomeworks.FirstOrDefault(o => o.nHomeWork == id);
                                var hw = ctx.THomeWorkLearnings.FirstOrDefault(o => o.LearnId == id && o.SchoolId == UserData.CompanyID);

                                hw.TitleName = txtTitle.Text;
                                hw.Description = txtDetail.Text;
                                hw.LinkYT = txtLink.Text;
                                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                                hw.Modified = DateTime.Now;
                                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);

                                var lstAssign = new List<string>();
                                if (hw.AssignType == 1)
                                {
                                    var _lst = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Value;

                                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                                    hw.SelectedStudent = "";

                                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId)
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

                                ctx.SaveChanges();

                                var dellst = tbFilesDel.Text.Split(',');
                                if (dellst.Length > 0)
                                {
                                    foreach (var d in dellst)
                                    {
                                        if (d == "") continue;

                                        var _id = ToNullableInt(d);
                                        var del = ctx.THomeWorkLearningFiles
                                            .Where(o => o.SchoolId == SchoolId && o.AttachId == _id)
                                            .FirstOrDefault();
                                        del.cDel = true;

                                        //ctx.THomeWorkLearningFiles.Remove(del);
                                    }
                                }

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
                                            , "learningonline/lesson"
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
                                }

                                ctx.SaveChanges();

                                var _lstRoom = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Text;

                                database.InsertLog(teacherId + "", $"แก้ไข คลังบทเรียน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                              
                            }
                            catch
                            {
                               
                            }
                        
                    }
                    else
                    {
                        
                            try
                            {
                                // var hw = his.THomeworks.FirstOrDefault(o => o.nHomeWork == id);
                                var hw = his.THomeWorkLearnings.FirstOrDefault(o => o.LearnId == id && o.SchoolId == UserData.CompanyID);

                                //url = $"OnlineManage.aspx?term={online.TermId.Trim()}&plan={online.PlanId}&level={online.LevelId}";
                                hw.TitleName = txtTitle.Text;
                                hw.Description = txtDetail.Text;
                                hw.LinkYT = txtLink.Text;
                                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                                hw.Modified = DateTime.Now;
                                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);

                                var lstAssign = new List<string>();
                                if (hw.AssignType == 1)
                                {
                                    var _lst = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Value;

                                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                                    hw.SelectedStudent = "";

                                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId &&  _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId)
                                        .Select(o => o.sID + "")
                                        .AsQueryable().ToList();
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

                                his.SaveChanges();

                                var dellst = tbFilesDel.Text.Split(',');
                                if (dellst.Length > 0)
                                {
                                    foreach (var d in dellst)
                                    {
                                        if (d == "") continue;

                                        var _id = ToNullableInt(d);
                                        var del = his.THomeWorkLearningFiles
                                            .Where(o => o.SchoolId == SchoolId && o.AttachId == _id)
                                            .FirstOrDefault();
                                        del.cDel = true;

                                        //his.THomeWorkLearningFiles.Remove(del);
                                    }
                                }

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
                                            , "learningonline/lesson"
                                            , SchoolId
                                            , hw.LearnId);

                                        if (!string.IsNullOrEmpty(linkfiles))
                                        {
                                            his.THomeWorkLearningFiles.Add(new JabjaiSchoolHistoryEntity.THomeWorkLearningFile
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
                                }

                                his.SaveChanges();

                                var _lstRoom = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Text;

                                database.InsertLog(teacherId + "", $"แก้ไข คลังบทเรียน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                                
                            }
                            catch
                            {
                               
                            }
                        
                    }
                    Response.Redirect($"OnlineManage.aspx?term={online.TermId.Trim()}&plan={online.PlanId}&level={online.LevelId}");
                }
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
                        var id = ToNullableInt(ddlGroup.SelectedValue);
                        var _class = ctx.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID);

                        if (_class == null)
                        {
                            var c = his.TClassOnlines.FirstOrDefault(o => o.OnlineId == id && o.SchoolId == UserData.CompanyID && (o.cDel ?? false) == false);

                            _class = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                        }

                        var arrRoom = (_class.SelectedRoom + "").Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();


                        var lst1 = ListSelectRoom.Where(o => arrRoom.Contains(o.nTermSubLevel2 + ""))
                          .OrderBy(o => o.nTSubLevel2)
                          .Select(o => new ListItem
                          {
                              Text = o.SubLevel + "/" + o.nTSubLevel2,
                              Value = o.nTermSubLevel2 + "",
                          });
                        ddlLevel.DataSource = lst1.Distinct();
                        ddlLevel.DataBind();

                        var _arr = lst1.Select(o => o.Value).ToList();
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
    }
}