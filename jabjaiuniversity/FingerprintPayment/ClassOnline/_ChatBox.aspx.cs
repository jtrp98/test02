using FingerprintPayment.Class;
using FingerprintPayment.ClassOnline.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using JabjaiSchoolHistoryEntity;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.ClassOnline
{
    public partial class _ChatBox : BaseOnlinePage
    {

        public class Chat
        {
            public int Id { get; set; }
            public DateTime? Date { get; set; }
            public string Comment { get; set; }
            public string Teacher { get; set; }
            public string Student { get; set; }
            public bool IsHead { get; set; }
            public byte? Type { get; set; }

            public List<ChatFile> Files { get; set; }

            public class ChatFile
            {
                public string FileUrl { get; set; }
                public string FileTitle { get; set; }
                public string ContentType { get; set; }
                public string Type { get; internal set; }
            }
        }

        public List<Chat> _model = new List<Chat>();
        public int? wid;
        public int? uid;
        protected void Page_Load(object sender, EventArgs ea)
        {
            if (!this.IsPostBack)
            {
                wid = ToNullableInt(Request.QueryString["wid"] + "");
                uid = ToNullableInt(Request.QueryString["uid"] + "");

                var SchoolId = UserData.CompanyID;
                using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(UserData.CompanyID,ConnectionDB.Read)))
                {
                    using (var his = new JabjaiSchoolHistoryEntities(Connection.StringConnectionSchoolHistory(ConnectionDB.Read)))
                    {
                        var a1 = ctx.THomeWorkReplies.Where(o => o.sID == uid && o.HomeWorkId == wid && o.SchoolId == SchoolId).ToList();
                        var a2 = CloneObject<JabjaiEntity.DB.THomeWorkReply, JabjaiSchoolHistoryEntity.THomeWorkReply>(
                            his.THomeWorkReply.Where(o => o.sID == uid && o.HomeWorkId == wid && o.SchoolId == SchoolId).ToList()
                            );
                        var reply = a1.Concat(a2);

                        var b1 = ctx.THomeWorkReply_File.Where(o => o.HomeWorkID == wid && o.SchoolID == UserData.CompanyID).ToList();
                        var b2 = CloneObject<JabjaiEntity.DB.THomeWorkReply_File, JabjaiSchoolHistoryEntity.THomeWorkReply_File>(
                            his.THomeWorkReply_File.Where(o => o.HomeWorkID == wid && o.SchoolID == UserData.CompanyID).ToList()
                            );
                        var replyFile = b1.Concat(b2);

                        var comment1 = (from a in reply
                                    from b in ctx.TUser.Where(o => o.sID == a.sID && o.SchoolID == a.SchoolId)// on a.sID equals b.sID
                                    from c in ctx.TEmployees.Where(o => o.sEmp == a.tID && o.SchoolID == a.SchoolId)// on a.tID equals c.sEmp
                                    from d in ctx.TTitleLists.Where(o => o.nTitleid + "" == b.sStudentTitle && o.SchoolID == b.SchoolID).DefaultIfEmpty()

                                    from e in ctx.TTitleLists.Where(o => o.nTitleid + "" == c.sTitle && o.SchoolID == c.SchoolID).DefaultIfEmpty()

                                    join f in replyFile on a.ReplyId equals f.ReplyID into fileList//.DefaultIfEmpty()
                                    select new
                                    {
                                        Id = a.ReplyId,
                                        Student = (d == null ? b.sStudentTitle : d.titleDescription) + " " + b.sName + " " + b.sLastname,
                                        Teacher = (e == null ? c.sTitle : e.titleDescription) + " " + c.sName + " " + c.sLastname,
                                        Date = a.Created,
                                        Comment = a.Comment,
                                        Type = a.Type,

                                        files = fileList.Select(i => new
                                        {
                                            FileUrl = i.FileUrl,
                                            FileTitle = i.FileTitle,
                                            ContentType = i.ContentType,
                                        }),
                                    })
                                   .AsEnumerable()
                                   .Select(o => new Chat
                                   {
                                       Student = o.Student,
                                       Teacher = o.Teacher,
                                       Comment = o.Comment,
                                       Date = o.Date,
                                       Type = o.Type,
                                       Files = o.files.Select(i => new Chat.ChatFile
                                       {
                                           FileUrl = i.FileUrl,
                                           FileTitle = i.FileTitle,
                                           Type = CommonHelper.GetType(i.ContentType),
                                           ContentType = i.ContentType,
                                       }).ToList(),
                                   })
                                   .OrderBy(o => o.Date)
                                   .ToList();

                        _model = comment1;


                    }

                }


            }
        }


    }
}

