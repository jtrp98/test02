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
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineReport : BaseOnlinePage
    {
        protected TPlane _plan = new TPlane();
        protected TSubLevel _level = new TSubLevel();
        protected JabjaiEntity.DB.TClassOnline _class = new JabjaiEntity.DB.TClassOnline();
        protected JabjaiEntity.DB.THomework _homework = new JabjaiEntity.DB.THomework();
        protected StatTop _stat = new StatTop();
        protected List<ReportList> _reportList = new List<ReportList>();

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            if (!this.IsPostBack)
            {
                var wid = Request.QueryString["id"] + "";

                if (!string.IsNullOrEmpty(wid))
                {
                    //ValidateThisHomeWork(Convert.ToInt32(wid));

                    LoadData(Convert.ToInt32(wid));

                    var status = HttpContext.Current.Request.Cookies["online_report_status"]?.Value;

                    if (!string.IsNullOrEmpty(status))
                    {
                        ddlFilter.SelectedValue = status;
                    }
                }

            }
        }

        private void ValidateThisHomeWork(int wid)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                var SchoolId = UserData.CompanyID;

                //var hw = ctx.THomeworks.FirstOrDefault(o => o.nHomeWork == wid );

                var homework = (from a in ctx.THomeworks.Where(o => o.SchoolID == SchoolId && o.nHomeWork == wid )
                                from b in ctx.TClassOnlines.Where(o => o.OnlineId == a.OnlineId && o.SchoolId == a.SchoolID)
                                select new
                                {
                                    a,
                                    b
                                }).FirstOrDefault();

                if (homework == null) return;

                var studentHW = ctx.THomework_User.Where(o => o.SchoolID == SchoolId && o.nHomeWork == wid  && o.cDel != true)
                    .Select(o => o.sID)
                    .ToList();

                var studentView = new List<int>();

                if (homework.a.AssignType == 1)
                {
                    var rooms = homework.a.SelectedRoom.Split('|')
                      .Where(o => !string.IsNullOrEmpty(o))
                      .Select(o => o)
                      .ToList();

                    studentView = ctx.TB_StudentViews
                     .Where(o => o.SchoolID == SchoolId && o.nTerm == homework.b.TermId && rooms.Contains(o.nTermSubLevel2 + "") && o.cDel == null )
                     .Select(o => o.sID)
                     .ToList();
                }
                else if (homework.a.AssignType == 2)
                {
                    var student = homework.a.SelectedStudent.Split('|')
                        .Where(o => !string.IsNullOrEmpty(o))
                        .Select(o => o)
                        .ToList();

                    studentView = ctx.TB_StudentViews
                       .Where(o => o.SchoolID == SchoolId && o.nTerm == homework.b.TermId && student.Contains(o.sID + "") && o.cDel == null )
                       .Select(o => o.sID)
                       .ToList();
                }

                if (studentHW.Count != studentView.Count)
                {
                    var toAdd = new List<JabjaiEntity.DB.THomework_User>();
                    foreach (var userId in studentView.Distinct().Except(studentHW))
                    {
                        toAdd.Add(new JabjaiEntity.DB.THomework_User
                        {
                            nHomeWork = wid,
                            sID = Convert.ToInt32(userId),
                            SchoolID = SchoolId,
                            CreatedDate = DateTime.Now,
                        });
                    }

                    if (toAdd.Count > 0)
                    {
                        ctx.THomework_User.AddRange(toAdd);
                        ctx.SaveChanges();
                    }
                }
            }
        }

        private void LoadData(int wid)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;

                    //var homework = (from a in ctx.THomeworks.Where(o => o.nHomeWork == wid )
                    //                from b in ctx.TClassOnlines.Where(o => o.OnlineId == a.OnlineId && o.SchoolId == a.SchoolID)
                    //                select new
                    //                {
                    //                    a,
                    //                    b
                    //                }).FirstOrDefault();

                    //_class = homework.b;
                    //_homework = homework.a;

                    var isHis = false;
                    _homework = ctx.THomeworks.FirstOrDefault(o => o.SchoolID == SchoolId && o.nHomeWork == wid );

                    if (_homework == null)
                    {
                        isHis = true;
                        var c = his.THomeworks.FirstOrDefault(o => o.nHomeWork == wid && o.SchoolID == UserData.CompanyID);

                        _homework = CloneObject<JabjaiEntity.DB.THomework, JabjaiSchoolHistoryEntity.THomework>(c);
                    }

                    _class = ctx.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == _homework.OnlineId  && (o.cDel ?? false) == false);

                    if (_class == null)
                    {
                        var c = his.TClassOnlines.FirstOrDefault(o => o.SchoolId == SchoolId && o.OnlineId == _homework.OnlineId  && (o.cDel ?? false) == false);

                        _class = CloneObject<JabjaiEntity.DB.TClassOnline, JabjaiSchoolHistoryEntity.TClassOnline>(c);
                    }

                    _plan = ctx.TPlanes.Where(o => o.SchoolID == SchoolId && o.sPlaneID == _class.PlanId ).FirstOrDefault();
                    _level = ctx.TSubLevels.Where(o => o.SchoolID == SchoolId && o.nTSubLevel == _class.LevelId ).FirstOrDefault();

                    var qry1 = ctx.TB_StudentViews.Where(o => o.SchoolID == SchoolId && o.nTerm == _class.TermId && o.cDel == null && o.nTSubLevel == _class.LevelId );
                    switch (_homework.AssignType)
                    {
                        case 1:
                            var _arr1 = _homework.SelectedRoom.Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();
                            qry1 = qry1.Where(o => _arr1.Contains(o.nTermSubLevel2 + ""));
                            break;

                        case 2:
                            var _arr2 = _homework.SelectedStudent.Split('|').Where(o => !string.IsNullOrEmpty(o)).Select(o => o).ToList();
                            qry1 = qry1.Where(o => _arr2.Contains(o.sID + ""));
                            break;
                    }



                    if (isHis == false)
                    {
                        var qry = (from a in ctx.THomework_User.Where(o => o.SchoolID == SchoolId && o.nHomeWork == wid  && o.cDel != true)
                                   from b in qry1.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID)

                                   select new
                                   {
                                       a.sID,
                                       a.nHomeWork,

                                       //studentTitle = (f == null ? b.sStudentTitle : f.titleDescription),
                                       studentTitle = b.titleDescription,
                                       nStudentNumber = b.nStudentNumber,
                                       b.sStudentID,
                                       b.sName,
                                       b.sLastname,
                                       a.UpdatedDate,
                                       a.IsLate,
                                       a.IsRead,
                                       a.IsSend,
                                       a.IsManual,
                                       a.Score,
                                       a.LinkUrl,
                                       a.AttachFile,

                                       b.nTermSubLevel2,
                                       b.nTSubLevel2,
                                       b.SubLevel,
                                   });

                        _reportList = qry
                           .OrderBy(o => o.nTSubLevel2)
                           .ThenBy(o => o.nStudentNumber)
                           .ThenBy(o => o.sStudentID)
                           .AsEnumerable()
                           .Select(o => new ReportList
                           {
                               UserId = o.sID,
                               WorkId = o.nHomeWork,
                               Code = o?.sStudentID,
                               Number = o.nStudentNumber,
                               FullName = o?.studentTitle + " " + o?.sName + " " + o.sLastname,

                               IsRead = o.IsSend == true ? true : o.IsRead,
                               IsSend = o.IsSend,
                               IsLate = o.IsLate,
                               IsManual = o.IsManual,
                               Score = (o.Score.HasValue ? o.Score + "" : "-") + "/" + (_homework.MaxScore.HasValue ? _homework.MaxScore + "" : "-"),
                               Link = o.LinkUrl,
                               File = o.AttachFile,
                               Level = o.SubLevel + "/" + o.nTSubLevel2,
                               LevelID = o.nTermSubLevel2,
                               SendDate = o.UpdatedDate, //?? DateTime.Now,
                           })
                           .ToList();
                    }
                    else
                    {
                        var qry = (from a in his.THomework_User.Where(o => o.SchoolID == SchoolId && o.nHomeWork == wid  && o.cDel != true).AsEnumerable()
                                   from b in qry1.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).AsEnumerable()

                                   select new
                                   {
                                       a.sID,
                                       a.nHomeWork,

                                       //studentTitle = (f == null ? b.sStudentTitle : f.titleDescription),
                                       studentTitle = b.titleDescription,
                                       nStudentNumber = b.nStudentNumber,
                                       b.sStudentID,
                                       b.sName,
                                       b.sLastname,
                                       a.UpdatedDate,
                                       a.IsLate,
                                       a.IsRead,
                                       a.IsSend,
                                       a.IsManual,
                                       a.Score,
                                       a.LinkUrl,
                                       a.AttachFile,

                                       b.nTermSubLevel2,
                                       b.nTSubLevel2,
                                       b.SubLevel,
                                   });

                        _reportList = qry
                       .OrderBy(o => o.nTSubLevel2)
                       .ThenBy(o => o.nStudentNumber)
                       .ThenBy(o => o.sStudentID)
                       .AsEnumerable()
                       .Select(o => new ReportList
                       {
                           UserId = o.sID,
                           WorkId = o.nHomeWork,
                           Code = o?.sStudentID,
                           Number = o.nStudentNumber,
                           FullName = o?.studentTitle + " " + o?.sName + " " + o.sLastname,

                           IsRead = o.IsSend == true ? true : o.IsRead,
                           IsSend = o.IsSend,
                           IsLate = o.IsLate,
                           IsManual = o.IsManual,
                           Score = (o.Score.HasValue ? o.Score + "" : "-") + "/" + (_homework.MaxScore.HasValue ? _homework.MaxScore + "" : "-"),
                           Link = o.LinkUrl,
                           File = o.AttachFile,
                           Level = o.SubLevel + "/" + o.nTSubLevel2,
                           LevelID = o.nTermSubLevel2,
                           SendDate = o.UpdatedDate, //?? DateTime.Now,
                       })
                       .ToList();
                    }
                    //var _filter = ddlFilter.SelectedValue;

                    ////_stat.Send = qry.Count(o => o.IsSend == true);
                    ////_stat.NotSend = qry.Count(o => o.IsSend != true);
                    ////_stat.Late  = qry.Count(o => o.IsLate == true || (o.UpdatedDate == null && DateTime.Now > _homework.dStart) );
                    //if (_filter != "")
                    //{
                    //    switch (_filter)
                    //    {
                    //        case "1":
                    //            qry = qry.Where(o => o.IsSend == true);
                    //            break;
                    //        case "2":
                    //            qry = qry.Where(o => o.IsSend != true);
                    //            break;
                    //        case "3":
                    //            qry = qry.Where(o => o.IsLate == true || (o.UpdatedDate == null && DateTime.Now > _homework.dStart));
                    //            break;
                    //        default:
                    //            break;
                    //    }
                    //}


                }
            }
        }

        protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wid = Request.QueryString["id"] + "";

            if (!string.IsNullOrEmpty(wid))
            {
                LoadData(Convert.ToInt32(wid));
            }
        }

        protected void btnRefreshTable_Click(object sender, EventArgs e)
        {
            var wid = Request.QueryString["id"] + "";

            if (!string.IsNullOrEmpty(wid))
            {
                LoadData(Convert.ToInt32(wid));
            }
        }

        public class ReportList
        {
            public int UserId { get; set; }
            public int WorkId { get; set; }
            public string FullName { get; set; }
            public bool? IsRead { get; set; }
            public bool? IsSend { get; set; }
            public bool? IsLate { get; set; }
            public string Score { get; set; }
            public string Link { get; set; }
            public string File { get; set; }

            public List<Chat> Comment { get; set; }
            public string Code { get; set; }
            public string Level { get; set; }
            public DateTime? SendDate { get; set; }
            public int? Number { get; internal set; }
            public int LevelID { get; internal set; }
            public bool? IsManual { get; internal set; }
        }

        public class Chat
        {
            public int Id { get; set; }
            public DateTime? Date { get; set; }
            public string Comment { get; set; }
            public string By { get; set; }
        }

        public class StatTop
        {
            public int Send { get; set; }
            public int NotSend { get; set; }
            public int Late { get; set; }
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUID.Text) && !string.IsNullOrEmpty(txtHID.Text))
            {
                var uid = Convert.ToInt32(txtUID.Text);
                var hid = Convert.ToInt32(txtHID.Text);

                using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                    {
                        var hw = ctx.THomeworks.Where(o => o.nHomeWork == hid && o.SchoolID == UserData.CompanyID).FirstOrDefault();

                        if (hw == null)
                        {
                            var c = his.THomeworks.Where(o => o.nHomeWork == hid && o.SchoolID == UserData.CompanyID).FirstOrDefault();

                            hw = CloneObject<JabjaiEntity.DB.THomework, JabjaiSchoolHistoryEntity.THomework>(c);
                        }

                        var online = ctx.THomework_User.Where(o => o.nHomeWork == hid && o.sID == uid && o.SchoolID == UserData.CompanyID).FirstOrDefault();

                        if (online == null)
                        {
                            var c = his.THomework_User.Where(o => o.nHomeWork == hid && o.sID == uid && o.SchoolID == UserData.CompanyID).FirstOrDefault();

                            online = CloneObject<JabjaiEntity.DB.THomework_User, JabjaiSchoolHistoryEntity.THomework_User>(c);
                        }

                        LiteralMax.Text = hw.MaxScore + "";
                        txtScore.Text = online.Score + "";

                        if (online.IsSend == true)
                        {
                            ddlStatus.SelectedValue = "1";
                        }
                        else if (online.IsSend == false)
                        {
                            ddlStatus.SelectedValue = "2";
                        }
                        else
                        {
                            ddlStatus.SelectedValue = "";
                        }

                        if (online.IsLate == true)
                        {
                            ddlTime.SelectedValue = "2";
                        }
                        else if (online.IsLate == false)
                        {
                            ddlTime.SelectedValue = "1";
                        }
                        else
                        {
                            ddlTime.SelectedValue = "";
                        }

                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "script1", " showModal(); ", true);
                }
            }
        }

        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {
            var uid = Convert.ToInt32(txtUID.Text);
            var hid = Convert.ToInt32(txtHID.Text);
            //var score = Convert.ToInt32(txtScore.Text);

            using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;

                    var user = ctx.TUser.Where(o => o.SchoolID == SchoolId && o.sID == uid ).FirstOrDefault();
                    var q = ctx.THomeworks.Where(o => o.SchoolID == SchoolId && o.nHomeWork == hid ).Count();

                    if (q > 0)
                    {
                        var home = ctx.THomeworks.FirstOrDefault(o => o.SchoolID == SchoolId && o.nHomeWork == hid );
                        var online = ctx.THomework_User.Where(o => o.SchoolID == SchoolId && o.nHomeWork == hid && o.sID == uid ).FirstOrDefault();

                        online.IsManual = true;

                        if (ddlStatus.SelectedValue == "1")
                        {
                            online.IsSend = true;
                        }
                        else
                        {
                            online.IsSend = false;
                        }

                        if (ddlTime.SelectedValue == "1")
                        {
                            online.IsLate = false;
                        }
                        else
                        {
                            online.IsLate = true;
                        }

                        online.UpdatedDate = DateTime.Now;

                        if (!string.IsNullOrEmpty(txtScore.Text))
                            online.Score = Convert.ToDouble(txtScore.Text);
                        else
                            online.Score = null;

                        ctx.SaveChanges();

                        database.InsertLog(teacherId + "", $"ปรับสถานะ นักเรียน {user.sName} {user.sLastname} งาน/การบ้าน {home.TitleName} เป็น {ddlStatus.SelectedItem.Text} {ddlTime.SelectedItem.Text}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                    }
                    else
                    {
                        var home = his.THomeworks.FirstOrDefault(o => o.SchoolID == SchoolId && o.nHomeWork == hid );
                        var online = his.THomework_User.Where(o => o.SchoolID == SchoolId && o.nHomeWork == hid && o.sID == uid ).FirstOrDefault();

                        online.IsManual = true;

                        if (ddlStatus.SelectedValue == "1")
                        {
                            online.IsSend = true;
                        }
                        else
                        {
                            online.IsSend = false;
                        }

                        if (ddlTime.SelectedValue == "1")
                        {
                            online.IsLate = false;
                        }
                        else
                        {
                            online.IsLate = true;
                        }

                        online.UpdatedDate = DateTime.Now;

                        if (!string.IsNullOrEmpty(txtScore.Text))
                            online.Score = Convert.ToDouble(txtScore.Text);
                        else
                            online.Score = null;

                        his.SaveChanges();

                        database.InsertLog(teacherId + "", $"ปรับสถานะ นักเรียน {user.sName} {user.sLastname} งาน/การบ้าน {home.TitleName} เป็น {ddlStatus.SelectedItem.Text} {ddlTime.SelectedItem.Text}", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "script2", " reloadAfterSave(" + uid + "); ", true);
                    //Response.Redirect(Request.RawUrl);
                }
            }
        }


    }
}
//[WebMethod(EnableSession = true)]
//public static ReportList GetData(int wid, int uid)
//{
//    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
//    {
//        var SchoolId = Convert.ToInt32(HttpContext.Current.Session["nCompany"] + "");
//        var obj = (from a in ctx.THomeworks.Where(o => o.nHomeWork == wid)
//                   from b in ctx.TClassOnlines.Where(o => o.OnlineId == a.OnlineId)
//                   select new { a, b }).FirstOrDefault();

//        var qry = from a in ctx.THomework_User
//                  .Where(o => o.nHomeWork == wid && o.sID == uid )
//                  from b in ctx.TUsers
//                  .Where(o => o.sID == a.sID)

//                      //.DefaultIfEmpty()
//                      //from c in ctx.TStudentClassroomHistories.Where(o => o.nTerm == hw.)
//                      //from c in ctx.THomeWorkReplies.Where(o => o.HomeWorkId == id && o.UserId == a.nUserID && o.Type == 1 && ).DefaultIfEmpty()

//                  select new
//                  {
//                      a.sID,
//                      a.nHomeWork,

//                      b.sName,
//                      b.sLastname,

//                      a.IsRead,
//                      a.IsSend,
//                      a.Score,
//                      a.LinkUrl,
//                      a.AttachFile,
//                  };

//        var item = qry.AsEnumerable()
//            .Select(o => new ReportList
//            {
//                UserId = o.sID,
//                WorkId = o.nHomeWork,
//                FullName = o?.sName + " " + o.sLastname,

//                IsRead = o.IsRead,
//                IsSend = o.IsSend,
//                Score = (o.Score ?? 0) + "",
//                Link = o.LinkUrl,
//                File = o.AttachFile,
//            })
//            .FirstOrDefault();

//        var comment = (from a in ctx.THomeWorkReplies.Where(o => o.UserId == uid && o.HomeWorkId == wid && o.Type == 1 && o.ReplyRefId == null )
//                       join b in ctx.TUsers on a.UserId equals b.sID //.Where(o => o.sID == uid).DefaultIfEmpty()
//                       select new Chat
//                       {
//                           Id = a.ReplyId,
//                           By = b.sName + " " + b.sLastname,
//                           Date = a.Created,
//                           Comment = a.Comment,
//                       }).FirstOrDefault();

//        var comments = (from a in ctx.THomeWorkReplies.Where(o => o.ReplyRefId == comment.Id )
//                        join b in ctx.TUsers on a.UserId equals b.sID
//                        select new Chat
//                        {
//                            Id = a.ReplyId,
//                            By = b.sName + " " + b.sLastname,
//                            Date = a.Created,
//                            Comment = a.Comment,
//                        }).ToList();

//        comments.Add(comment);

//        item.Comment = comments.OrderBy(o => o.Date).ToList();

//        return item;
//    }
//}