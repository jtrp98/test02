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
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using FingerprintPayment.ClassOnline.Helper;
using JabjaiSchoolHistoryEntity;

namespace FingerprintPayment.ClassOnline
{
    public partial class OnlineReportEdit : BaseOnlinePage
    {

        public JabjaiEntity.DB.THomework _hw = null;
        public ModelReport _model = new ModelReport();
        public int? wid;
        public int? uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) 
            //    Response.Redirect("/Default.aspx");


            if (!this.IsPostBack)
            {
                wid = ToNullableInt(Request.QueryString["wid"] + "");
                uid = ToNullableInt(Request.QueryString["uid"] + "");

                if (uid.HasValue && wid.HasValue)
                {
                    LoadData(wid, uid);

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

        private void LoadData(int? wid, int? uid)
        {
            // var userData = GetUserData();

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var SchoolId = UserData.CompanyID;//Convert.ToInt32(HttpContext.Current.Session["nCompany"] + "");

                    _hw = ctx.THomeworks.FirstOrDefault(o => o.nHomeWork == wid && o.SchoolID == SchoolId);
                    var isHis = false;

                    if (_hw == null)
                    {
                        isHis = true;
                        var q = his.THomeworks.FirstOrDefault(o => o.nHomeWork == wid && o.SchoolID == SchoolId);

                        _hw = CloneObject<JabjaiEntity.DB.THomework, JabjaiSchoolHistoryEntity.THomework>(q);
                    }

                    var item = new ModelReport();

                    if (isHis)
                    {
                        var qry = from a in his.THomework_User.Where(o => o.nHomeWork == wid && o.sID == uid && o.SchoolID == SchoolId && o.cDel != true).AsEnumerable()
                                  from b in ctx.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                  from d in ctx.TTitleLists.Where(o => o.nTitleid + "" == b.sStudentTitle && o.SchoolID == b.SchoolID).DefaultIfEmpty()// on b.sStudentTitle equals d.nTitleid + "" /

                                  select new
                                  {
                                      a.sID,
                                      a.nHomeWork,
                                      title = (d == null ? b.sStudentTitle : d.titleDescription),
                                      b.sName,
                                      b.sLastname,

                                      a.IsRead,
                                      a.IsSend,
                                      a.Score,
                                      a.LinkUrl,
                                      a.AttachFile,
                                      a.AttachTitle,
                                  };


                        item = qry.AsEnumerable()
                             .Select(o => new ModelReport
                             {
                                 UserId = o.sID,
                                 WorkId = o.nHomeWork,
                                 FullName = o?.title + " " + o?.sName + " " + o.sLastname,

                                 IsRead = o.IsRead,
                                 IsSend = o.IsSend,
                                 Score = (o.Score ?? 0) + "",
                                 Link = o.LinkUrl,
                                 //File = o.AttachFile,
                                 //TitleFile = o.AttachTitle,
                             })
                         .FirstOrDefault();

                        item.Files = his.THomework_User_File
                             .Where(o => (o.cDel ?? false) == false && o.nHomeWorkId == wid && o.SchoolID == SchoolId && o.sID == uid)
                             .AsEnumerable()
                             .Select(o => new Files
                             {
                                 Type = CommonHelper.GetType(o.FileContentType),
                                 File = o.FileUrl,
                                 TitleFile = o.FileTitle,
                                 ContentType = o.FileContentType,
                             })
                             .ToList();
                    }
                    else
                    {
                        var qry = from a in ctx.THomework_User.Where(o => o.nHomeWork == wid && o.sID == uid && o.SchoolID == SchoolId && o.cDel != true)
                                  from b in ctx.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolID).DefaultIfEmpty()
                                  from d in ctx.TTitleLists.Where(o => o.nTitleid + "" == b.sStudentTitle && o.SchoolID == b.SchoolID).DefaultIfEmpty()// on b.sStudentTitle equals d.nTitleid + "" /

                                  select new
                                  {
                                      a.sID,
                                      a.nHomeWork,
                                      title = (d == null ? b.sStudentTitle : d.titleDescription),
                                      b.sName,
                                      b.sLastname,

                                      a.IsRead,
                                      a.IsSend,
                                      a.Score,
                                      a.LinkUrl,
                                      a.AttachFile,
                                      a.AttachTitle,
                                  };


                        item = qry.AsEnumerable()
                             .Select(o => new ModelReport
                             {
                                 UserId = o.sID,
                                 WorkId = o.nHomeWork,
                                 FullName = o?.title + " " + o?.sName + " " + o.sLastname,

                                 IsRead = o.IsRead,
                                 IsSend = o.IsSend,
                                 Score = (o.Score ?? 0) + "",
                                 Link = o.LinkUrl,
                                 //File = o.AttachFile,
                                 //TitleFile = o.AttachTitle,
                             })
                         .FirstOrDefault();

                        item.Files = ctx.THomework_User_File
                             .Where(o => (o.cDel ?? false) == false && o.nHomeWorkId == wid && o.SchoolID == SchoolId && o.sID == uid)
                             .AsEnumerable()
                             .Select(o => new Files
                             {
                                 Type = CommonHelper.GetType(o.FileContentType),
                                 File = o.FileUrl,
                                 TitleFile = o.FileTitle,
                                 ContentType = o.FileContentType,
                             })
                             .ToList();
                    }

                    txtScore.Text = item.Score;

                    _model = item;

                }
            }
        }

        //protected async void btnSave_Click(object sender, EventArgs e)
        //{
        //    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
        //    {
        //        var id = ToNullableInt(Request.QueryString["id"] + "");
        //        var hw = ctx.THomeWorkLearnings.FirstOrDefault(o => o.LearnId == id);
        //        var online = ctx.TClassOnlines.FirstOrDefault(o => o.OnlineId == hw.OnlineId);


        //        Response.Redirect($"OnlineManage.aspx?term={online.TermId.Trim()}&plan={online.PlanId}&level={online.LevelId}");
        //    }

        //}



        //protected void btnSend1_Click(object sender, EventArgs e)
        //{
        //    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
        //    {
        //        var wid = ToNullableInt(Request.QueryString["wid"] + "");
        //        var uid = ToNullableInt(Request.QueryString["uid"] + "");
        //        int SchoolID = UserData.CompanyID;//Convert.ToInt32(Session["nCompany"] + "");

        //        if (!string.IsNullOrEmpty(txtComment.Text))
        //        {
        //            //var head = ctx.THomeWorkReplies
        //            //     .Where(o => o.sID == uid && o.HomeWorkId == wid && o.ReplyRefId == null && o.SchoolId == SchoolID)
        //            //     .FirstOrDefault();

        //            var comment = new THomeWorkReply();
        //            comment.HomeWorkId = wid;
        //            comment.tID = UserData.UserID;
        //            comment.sID = uid;
        //            comment.Comment = txtComment.Text;
        //            //comment.ReplyRefId = head?.ReplyId;
        //            comment.Created = DateTime.Now;
        //            comment.Modified = DateTime.Now;
        //            comment.Type = 2;
        //            comment.SchoolId = SchoolID;

        //            ctx.THomeWorkReplies.Add(comment);
        //        }

        //        ctx.SaveChanges();
        //        Response.Redirect(Request.Url.AbsoluteUri);
        //        //Response.Redirect($"OnlineReport.aspx?id={wid}");
        //    }
        //}

        public class ModelReport
        {
            public int UserId { get; set; }
            public int WorkId { get; set; }
            public string FullName { get; set; }
            public bool? IsRead { get; set; }
            public bool? IsSend { get; set; }
            public string Score { get; set; }
            public string Link { get; set; }
            //public string File { get; set; }
            //public string TitleFile { get; set; }
            //public Chat HeadComment { get; set; }
            // public List<Chat> Comment { get; set; }
            public List<Files> Files { get; set; }

        }
        public class Files
        {
            public string Type { get; set; }
            public string File { get; set; }
            public string TitleFile { get; set; }
            public string ContentType { get; set; }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void SendText(string text, int wid, int uid)
        {
            JWTToken token = new JWTToken();
            var session = new JWTToken.userData();
            if (token.CheckToken(HttpContext.Current))
            {
                session = token.getTokenValues(HttpContext.Current);
            }

            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(session.CompanyID, ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    int SchoolID = session.CompanyID;//Convert.ToInt32(Session["nCompany"] + "");

                    //var q = ctx.THomeworks.Where(o => o.nHomeWork == wid && o.SchoolID == session.CompanyID).Count();

                    //if (q == 0)
                    //{
                    //    if (!string.IsNullOrEmpty(text))
                    //    {
                    //        var comment = new JabjaiSchoolHistoryEntity.THomeWorkReply();
                    //        comment.HomeWorkId = wid;
                    //        comment.tID = session.UserID;
                    //        comment.sID = uid;
                    //        comment.Comment = text;
                    //        comment.Created = DateTime.Now;
                    //        comment.Modified = DateTime.Now;
                    //        comment.Type = 2;
                    //        comment.SchoolId = SchoolID;

                    //        his.THomeWorkReply.Add(comment);
                    //    }

                    //    his.SaveChanges();
                    //}
                    //else
                    //{
                    if (!string.IsNullOrEmpty(text))
                    {
                        var comment = new JabjaiEntity.DB.THomeWorkReply();
                        comment.HomeWorkId = wid;
                        comment.tID = session.UserID;
                        comment.sID = uid;
                        comment.Comment = text;
                        comment.Created = DateTime.Now;
                        comment.Modified = DateTime.Now;
                        comment.Type = 2;
                        comment.SchoolId = SchoolID;

                        ctx.THomeWorkReplies.Add(comment);
                    }

                    ctx.SaveChanges();
                    //}
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
            {
                using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                {
                    var teacherId = UserData.UserID;
                    var SchoolId = UserData.CompanyID;
                    var wid = ToNullableInt(Request.QueryString["wid"] + "");
                    var uid = ToNullableInt(Request.QueryString["uid"] + "");

                    var student = ctx.TUser.Where(o => o.sID == uid && o.SchoolID == SchoolId).FirstOrDefault();
                    _hw = ctx.THomeworks.FirstOrDefault(o => o.nHomeWork == wid && o.SchoolID == SchoolId);


                    //var q = ctx.THomeworks.Where(o => o.nHomeWork == wid && o.SchoolID == SchoolId).Count();

                    if (_hw == null)
                    {
                        var q = his.THomeworks.FirstOrDefault(o => o.nHomeWork == wid && o.SchoolID == SchoolId);
                        _hw = CloneObject<JabjaiEntity.DB.THomework, JabjaiSchoolHistoryEntity.THomework>(q);

                        var user = his.THomework_User
                           .Where(o => o.nHomeWork == wid && o.sID == uid && o.SchoolID == SchoolId && o.cDel != true)
                           .FirstOrDefault();

                        if (!string.IsNullOrEmpty(txtScore.Text))
                            user.Score = Convert.ToDouble(txtScore.Text);

                        his.SaveChanges();
                    }
                    else
                    {
                        var user = ctx.THomework_User
                              .Where(o => o.nHomeWork == wid && o.sID == uid && o.SchoolID == SchoolId && o.cDel != true)
                              .FirstOrDefault();

                        if (!string.IsNullOrEmpty(txtScore.Text))
                            user.Score = Convert.ToDouble(txtScore.Text);

                        ctx.SaveChanges();
                    }

                    database.InsertLog(teacherId + "", $"ให้คะแนนนักเรียน {student.sName} {student.sLastname} งาน/การบ้าน {_hw.TitleName} {txtScore.Text} คะแนน", HttpContext.Current.Request, 26, 0, 0, UserData.CompanyID);

                    ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "savescore",
                        @" alertsuccess(); ", true);

                    //Response.Redirect($"OnlineReport.aspx?id={wid}");
                    // Response.Redirect(Request.Url.AbsoluteUri);
                }
            }
        }
    }
}