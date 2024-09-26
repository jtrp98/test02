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
using System.Globalization;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using FingerprintPayment.ClassOnline.Helper;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineWorkEdit : BaseOnlinePage
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

        protected JabjaiEntity.DB.THomework _homework = null;
        protected JabjaiEntity.DB.TClassOnline _class = null;
        protected List<Files> files = new List<Files>();

        public class Files
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string File { get; set; }
            public string TitleFile { get; set; }
            public string ContentType { get; set; }
        }


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

                    _homework = ctx.THomeworks.FirstOrDefault(o => o.SchoolID == SchoolId && o.nHomeWork == id );

                    if (_homework == null)
                    {
                        var h = his.THomeworks.FirstOrDefault(o => o.SchoolID == SchoolId && o.nHomeWork == id );
                        _homework = CloneObject<JabjaiEntity.DB.THomework, JabjaiSchoolHistoryEntity.THomework>(h);
                    }

                    txtTitle.Text = _homework.TitleName;
                    txtDetail.Text = _homework.sHomeworkDetail;
                    txtLink.Text = _homework.LinkYT;
                    txtScore.Text = _homework.MaxScore + "";
                    txtStart.Text = _homework.dStart?.ToString(@"dd/MM/yyyy", new CultureInfo("th-TH"));
                    ddlType.SelectedValue = _homework.AssignType + "";
                    txtDisplayDate.Text = _homework.DisplayDate?.ToString(@"dd/MM/yyyy", new CultureInfo("th-TH"));
                    txtDisplayTime.Text = _homework.DisplayDate?.ToString(@"HH:mm", new CultureInfo("th-TH"));
                    ddlDisplay.SelectedValue = _homework.DisplayType + "";
                    files = ctx.THomeWorkFiles
                        .Where(o => o.SchoolID == SchoolId && o.nHomeWorkId == id  && o.cDel != true)
                        .AsEnumerable()
                        .Select(o => new Files
                        {
                            Id = o.nFileId,
                            Type = CommonHelper.GetType(o.ContentType),
                            File = o.sFileName,
                            TitleFile = o.Title,
                            ContentType = o.ContentType,
                        })
                        .ToList();

                    //if (files.Count == 0)
                    {
                        var f = his.THomeWorkFiles
                          .Where(o => o.SchoolID == SchoolId && o.nHomeWorkId == id  && o.cDel != true)
                          .AsEnumerable()
                          .Select(o => new Files
                          {
                              Id = o.nFileId,
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
    and C.SchoolID = {SchoolId}  { (isSuperUser ? "" : "and TT.sEmp = " + teacherId)} 
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
                    var lstStudent = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && o.nTerm == _class.TermId && o.nTSubLevel == _class.LevelId && _arr.Contains(o.nTermSubLevel2 + "")  && o.cDel == null)
                        .OrderBy(o => o.nTermSubLevel2)
                        .ThenBy(o => o.sStudentID)
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
                    var onlineID = ctx.THomeworks.Where(o => o.SchoolID == SchoolId && o.nHomeWork == id )
                        .Select(o => o.OnlineId)
                        .FirstOrDefault();

                    if (onlineID == null)
                    {
                        isHis = true;

                        onlineID = his.THomeworks.Where(o => o.SchoolID == SchoolId && o.nHomeWork == id )
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
                                var hw = ctx.THomeworks.FirstOrDefault(o => o.nHomeWork == id );

                                hw.TitleName = txtTitle.Text;
                                hw.sHomeworkDetail = txtDetail.Text;
                                //hw.sHomeworkDetail = tbFiles.Text;
                                hw.LinkYT = txtLink.Text;
                                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);
                                //hw.dNotification = DateTime.Now.AddMinutes(3);
                                hw.dStart = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                                if (!string.IsNullOrEmpty(txtScore.Text))
                                {
                                    hw.MaxScore = Convert.ToDouble(txtScore.Text);
                                }
                                hw.Modified = DateTime.Now;

                                var lstAssign = new List<string>();
                                if (hw.AssignType == 1)
                                {
                                    var _lst = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Value;

                                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                                    hw.SelectedStudent = "";

                                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId )
                                        .Select(o => o.sID + "")
                                        .AsQueryable()
                                        .ToList();

                                    //var q = ctx.TB_StudentViews.Where(o => _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId)
                                    //       .Select(o => o.sID + "")
                                    //       .ToString());
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

                                //fix update student status
                                var toUpdateStatus = ctx.THomework_User
                                    .Where(o => o.SchoolID == SchoolId && o.nHomeWork == id && o.IsSend == true && o.IsLate == true  && o.IsManual == false)
                                    .ToList();

                                foreach (var item in toUpdateStatus)
                                {
                                    if (item.UpdatedDate > hw.dStart?.AddDays(1).AddSeconds(-1))
                                    {
                                        item.IsLate = true;
                                    }
                                    else
                                    {
                                        item.IsLate = false;
                                    }
                                }

                                //ctx.SaveChanges();

                                var lstDone = ctx.THomework_User
                                    .Where(o => o.SchoolID == SchoolId && o.nHomeWork == id )
                                     .ToList();

                                //.Select(o => new
                                //{
                                //    sID = o.sID + "",
                                //    o.cDel,
                                //})

                                var lstActive = lstDone.Where(o => o.cDel != true).Select(o => o.sID + "").ToList();
                                //var lstDel = lstDone.Where(o => o.cDel == true).Select(o => o.sID).ToList();

                                var lstAddUser = lstAssign.Distinct().Except(lstActive);
                                var lstRemoveUser = lstActive.Distinct().Except(lstAssign);

                                foreach (var userId in lstAddUser)
                                {
                                    var u = lstDone.FirstOrDefault(o => o.sID + "" == userId);

                                    if (u == null)
                                    {
                                        ctx.THomework_User.Add(new JabjaiEntity.DB.THomework_User
                                        {
                                            nHomeWork = hw.nHomeWork,
                                            sID = Convert.ToInt32(userId),
                                            SchoolID = SchoolId,
                                            CreatedDate = DateTime.Now,
                                            cDel = false,
                                        });
                                    }
                                    else
                                    {
                                        u.cDel = false;
                                    }
                                }

                                if (lstRemoveUser.Count() > 0)
                                {
                                    var del1 = lstDone
                                        .Where(o => lstRemoveUser.Contains(o.sID + ""))
                                        .ToList();

                                    foreach (var i in del1)
                                    {
                                        i.cDel = true;
                                    }
                                }

                                ctx.SaveChanges();

                                var dellst = tbFilesDel.Text.Split(',');
                                if (dellst.Length > 0)
                                {
                                    foreach (var d in dellst)
                                    {
                                        if (d == "") continue;

                                        var _id = ToNullableInt(d);
                                        var del = ctx.THomeWorkFiles.Where(o => o.SchoolID == SchoolId && o.nHomeWorkId == id  && o.nFileId == _id).FirstOrDefault();
                                        del.cDel = true;
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
                                }

                                ctx.SaveChanges();

                                var _lstRoom = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Text;



                                database.InsertLog(teacherId + "", $"แก้ไข งาน/การบ้าน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                              


                            }
                            catch (Exception ex)
                            {
                                
                            }
                        
                    }
                    else
                    {
                       
                            try
                            {
                                // var hw = his.THomeworks.FirstOrDefault(o => o.nHomeWork == id);

                                var hw = his.THomeworks.FirstOrDefault(o => o.nHomeWork == id && o.SchoolID == UserData.CompanyID);

                                hw.TitleName = txtTitle.Text;
                                hw.sHomeworkDetail = txtDetail.Text;
                                //hw.sHomeworkDetail = tbFiles.Text;
                                hw.LinkYT = txtLink.Text;
                                hw.AssignType = Convert.ToByte(ddlType.SelectedValue);
                                //hw.dNotification = DateTime.Now.AddMinutes(3);
                                hw.dStart = DateTime.ParseExact(txtStart.Text, "dd/MM/yyyy", new CultureInfo("th-TH"));
                                hw.OnlineId = Convert.ToInt32(ddlGroup.SelectedValue);
                                if (!string.IsNullOrEmpty(txtScore.Text))
                                {
                                    hw.MaxScore = Convert.ToDouble(txtScore.Text);
                                }
                                hw.Modified = DateTime.Now;

                                var lstAssign = new List<string>();
                                if (hw.AssignType == 1)
                                {
                                    var _lst = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Value;

                                    hw.SelectedRoom = "|" + string.Join("|", _lst) + "|";
                                    hw.SelectedStudent = "";

                                    lstAssign = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId )
                                        .Select(o => o.sID + "")
                                        .AsQueryable()
                                        .ToList();

                                    //var q = ctx.TB_StudentViews.Where(o => _lst.Contains(o.nTermSubLevel2 + "") && o.cDel == null && o.nTerm == online.TermId)
                                    //       .Select(o => o.sID + "")
                                    //       .ToString());
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

                                //fix update student status
                                var toUpdateStatus = his.THomework_User
                                    .Where(o => o.SchoolID == SchoolId && o.nHomeWork == id && o.IsSend == true && o.IsLate == true  && o.IsManual == false)
                                    .ToList();

                                foreach (var item in toUpdateStatus)
                                {
                                    if (item.UpdatedDate > hw.dStart?.AddDays(1).AddSeconds(-1))
                                    {
                                        item.IsLate = true;
                                    }
                                    else
                                    {
                                        item.IsLate = false;
                                    }
                                }

                                //his.SaveChanges();

                                var lstDone = his.THomework_User
                                    .Where(o => o.SchoolID == SchoolId && o.nHomeWork == id )
                                     .ToList();

                                //.Select(o => new
                                //{
                                //    sID = o.sID + "",
                                //    o.cDel,
                                //})

                                var lstActive = lstDone.Where(o => o.cDel != true).Select(o => o.sID + "").ToList();
                                //var lstDel = lstDone.Where(o => o.cDel == true).Select(o => o.sID).ToList();

                                var lstAddUser = lstAssign.Distinct().Except(lstActive);
                                var lstRemoveUser = lstActive.Distinct().Except(lstAssign);

                                foreach (var userId in lstAddUser)
                                {
                                    var u = lstDone.FirstOrDefault(o => o.sID + "" == userId);

                                    if (u == null)
                                    {
                                        his.THomework_User.Add(new JabjaiSchoolHistoryEntity.THomework_User
                                        {
                                            nHomeWork = hw.nHomeWork,
                                            sID = Convert.ToInt32(userId),
                                            SchoolID = SchoolId,
                                            CreatedDate = DateTime.Now,
                                            cDel = false,
                                        });
                                    }
                                    else
                                    {
                                        u.cDel = false;
                                    }
                                }

                                if (lstRemoveUser.Count() > 0)
                                {
                                    var del1 = lstDone
                                        .Where(o => lstRemoveUser.Contains(o.sID + ""))
                                        .ToList();

                                    foreach (var i in del1)
                                    {
                                        i.cDel = true;
                                    }
                                }

                                his.SaveChanges();

                                var dellst = tbFilesDel.Text.Split(',');
                                if (dellst.Length > 0)
                                {
                                    foreach (var d in dellst)
                                    {
                                        if (d == "") continue;

                                        var _id = ToNullableInt(d);
                                        var del = his.THomeWorkFiles.Where(o => o.SchoolID == SchoolId && o.nHomeWorkId == id  && o.nFileId == _id).FirstOrDefault();
                                        del.cDel = true;
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
                                            , "learningonline/work"
                                            , SchoolId
                                            , hw.nHomeWork);

                                        if (!string.IsNullOrEmpty(linkfiles))
                                        {
                                            his.THomeWorkFiles.Add(new JabjaiSchoolHistoryEntity.THomeWorkFile
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
                                }

                                his.SaveChanges();

                                var _lstRoom = from ListItem li in ddlLevel.Items
                                               where li.Selected
                                               select li.Text;



                                database.InsertLog(teacherId + "", $"แก้ไข งาน/การบ้าน {txtTitle.Text} ห้องเรียน {string.Join(",", _lstRoom) }", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                             


                            }
                            catch (Exception ex)
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