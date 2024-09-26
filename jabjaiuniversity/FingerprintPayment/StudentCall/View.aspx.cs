using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static FingerprintPayment.App_Code.StdCallingHub;

namespace FingerprintPayment.StudentCall
{
    public partial class View : Page
    {
        //protected override int MenuID => PermissionHelper.MENUID_STUDENTCALL_MAIN;
        protected Guid _token;
        protected string TOKEN { get { return _token.ToString(); } }
        protected TCompany Company { get; set; } = new TCompany();
        protected TStudentCall_Gate Gate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                var token = Request.QueryString["token"] + "";

                try
                {
                    _token = Guid.Parse(token);

                    Gate = dbmaster.TStudentCall_Gate.FirstOrDefault(o => o.Token == _token && o.IsDel == false);

                    if (Gate != null)
                    {
                        Company = dbmaster.TCompanies.Where(o => o.nCompany == Gate.SchoolID)
                           .Select(o => new
                           {
                               o.nCompany,
                               o.sImage,
                               o.sCompany,
                           })
                           .AsEnumerable()
                           .Select(o => new TCompany
                           {
                               nCompany = o.nCompany,
                               sImage = o.sImage,
                               sCompany = o.sCompany,
                           })
                           .FirstOrDefault();
                    }
                }
                catch
                {
                    return;
                }

            }
        }

        [System.Web.Script.Services.ScriptMethod(UseHttpGet = true)]
        [System.Web.Services.WebMethod()]
        public static object RefreshConnection()
        {
            return DateTime.Now.Ticks;
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object SetAnnouced(int sID, int schoolID)
        {
            Task.Run(() => UpdateStatusAnnounced(sID, schoolID));

            return true;
        }

        private static async void UpdateStatusAnnounced(int sID, int schoolID)
        {
            using (JabJaiMasterEntities dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                var toDay = DateTime.Now.Date;
                var t = await dbmaster.TStudentCall
                    .Where(o => o.sID == sID && o.SchoolID == schoolID && o.CallDate == toDay)
                    .FirstOrDefaultAsync();

                if (t != null && t.Status < 3)
                {
                    t.Status = 2;
                    t.Announced = DateTime.Now.TimeOfDay;

                    await dbmaster.SaveChangesAsync();
                }
            }
        }

        protected static TTerm GetCurrentTerm(JabJaiEntities _ctx, int schoolID)
        {
            var dateNow = DateTime.Now;

            var termList = _ctx.TTerms
                .Where(o => o.SchoolID == schoolID && o.cDel != "1")
                .AsNoTracking()
                .ToList();

            var current = termList
                    .Where(o => o.dStart <= dateNow && dateNow <= o.dEnd)
                    .FirstOrDefault();

            if (current == null)
                current = termList
                    .OrderByDescending(o => o.dEnd)
                    .FirstOrDefault(f => f.dEnd <= dateNow);

            if (current == null)
                current = termList
                    .OrderBy(o => o.dEnd)
                    .FirstOrDefault(f => f.dStart >= dateNow);

            return current;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object InitialPage(int schoolId, Guid token)
        {
            //var schoolId = UserData.CompanyID;
            var toDay = DateTime.Now.Date;

            using (var _ctx = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(schoolId,ConnectionDB.Read)))
            {
                //var logic = new StudentLogic(_ctx);
                //var termId = logic.GetTermId(new JWTToken.userData { CompanyID = schoolId });
                var termId = GetCurrentTerm(_ctx, schoolId);

                string sql = $@"-- StudentCall/Main
SELECT A.SchoolID,A.sID,C.Base64Sound 'Sound'
, A.sNickName 'NickName' , ISNULL(A2.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
, A2.ClassFullName 'Level1', A2.nTSubLevel2 'Level2'  , A2.nTermSubLevel2
, A.sStudentID 'Code' , A.sStudentPicture 'Img' 
, CASE WHEN B.Status = 1 THEN B.Created WHEN B.Status = 2 THEN B.Announced WHEN B.Status = 3 THEN B.Completed END 'tTime'
, B.Status
--, G.ParentName 'Receiver' 
, (CASE WHEN G.Type = 1 THEN H.sFatherFirstName + ' ' + H.sFatherLastName WHEN G.Type = 2 THEN H.sMotherFirstName + ' ' + H.sMotherLastName WHEN G.Type = 3 THEN H.sFamilyName + ' ' + H.sFamilyLast END )  'Receiver'
, C.Base64Sound 'Sound'
FROM JabjaiSchoolSingleDB.dbo.TUser A
INNER JOIN JabjaiMasterSingleDB.dbo.TStudentCall B on A.SchoolID = B.SchoolID and A.sID = B.sID 
INNER JOIN JabjaiSchoolSingleDB.dbo.TB_StudentViews A2 on A.SchoolID = A2.SchoolID and A.sID = A2.sID
LEFT JOIN JabjaiMasterSingleDB.dbo.TSound_Student C on A2.SchoolID = C.SchoolID and A2.sID = C.sID and A2.nTermSubLevel2 = C.nTermSubLevel2

LEFT JOIN JabjaiMasterSingleDB.dbo.TParent_Card G on A.SchoolID = G.SchoolID and A.sID = G.sID  and G.IsDel = 0 and G.IsActive = 1 AND G.No = ISNULL(B.CardNo,1)
LEFT JOIN [JabjaiSchoolSingleDB].[dbo].[TFamilyProfile] H on G.SchoolID = H.SchoolID and G.sID = H.sID

WHERE A.SchoolID = {schoolId} and  B.CallDate = '{toDay.ToString("yyyyMMdd")}' and A2.nTerm = '{termId.nTerm}' ";

                using (JabJaiMasterEntities dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
                {
                    var gate = dbmaster.TStudentCall_Gate
                        .Where(o => o.Token == token && o.SchoolID == schoolId)
                        .AsQueryable().FirstOrDefault();

                    if (gate != null)
                    {
                        var rooms = JsonConvert.DeserializeObject<List<int>>(gate.SelectedRoom);

                        sql += $" AND A2.nTermSubLevel2 in ({string.Join(",", rooms)})";
                    }

                    var lst = dbmaster.Database.SqlQuery<ModelStudent>(sql).AsQueryable().ToList();

                    foreach (var i in lst)
                    {
                        i.Level1 = i.Level1.Replace("ศึกษาปีที่", "")
                         .Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส")
                         .Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช");
                        i.FullName = $"{i.Title} {i.FirstName} {i.LastName} ";
                        i.Level = $"{i.Level1}/{i.Level2}";
                        i.Time = i.tTime.ToString(@"hh\:mm\:ss");

                    }

                    var d = lst.Select(o => new
                    {
                        Status = o.Status,
                        SchoolID = schoolId,
                        sID = o.sID,
                        Sound = o.Sound,
                        NickName = o.NickName + "",
                        FullName = $"{o.Title} {o.FirstName} {o.LastName} ",
                        Level = $"{o.Level1}/{o.Level2}",
                        Code = o.Code,
                        Img = o.Img,
                        Time = o.Time,
                        o.tTime,
                        //  Time = DateTime.Now.ToString("HH:mm:ss"),
                        Reciever = o.Receiver,
                    });

                    return new
                    {
                        status1 = d.Where(o => o.Status == 1).OrderBy(o => o.tTime),
                        status2 = d.Where(o => o.Status == 2).OrderBy(o => o.tTime),
                        status3 = d.Where(o => o.Status == 3).OrderBy(o => o.tTime),
                    };
                }

            }
            //var stdList = new List<ModelStudent>();
            //var callList = new List<TStudentCalling>();

            //using (var _ctx = new JabJaiEntities(MasterEntity.Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            //{
            //    stdList = (from a in _ctx.TUsers.Where(o => o.SchoolID == schoolId)
            //               from b in _ctx.TTermSubLevel2.Where(o => o.SchoolID == schoolId && o.nTermSubLevel2 == a.nTermSubLevel2)
            //               from c in _ctx.TSubLevels.Where(o => o.SchoolID == schoolId && o.nTSubLevel == b.nTSubLevel)
            //               from d in _ctx.TTitleLists.Where(o => o.SchoolID == schoolId && o.nTitleid + "" == a.sStudentTitle).DefaultIfEmpty()

            //               select new ModelStudent
            //               {
            //                   sID = a.sID,
            //                   nTermSubLevel2 = a.nTermSubLevel2,
            //                   NickName = a.sNickName,
            //                   FirstName = a.sName,
            //                   LastName = a.sLastname,
            //                   Level1 = c.fullName,
            //                   Level2 = b.nTSubLevel2,
            //                   Code = a.sStudentID,                              
            //                   Img = a.sStudentPicture,
            //                   Title = d != null ? d.titleDescription : a.sStudentTitle,
            //               })
            //         .ToList();

            //    //std.Level1 = std.Level1.Replace("ศึกษาปีที่", "")
            //    //       .Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส")
            //    //       .Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช");
            //}

            //using (JabJaiMasterEntities dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            //{
            //    var toDay = DateTime.Now.Date;
            //    callList = dbmaster.TStudentCalling
            //        .Where(o => o.SchoolID == schoolId && o.CallDate == toDay)
            //        .ToList();
            //}

            //var data = from a in stdList
            //           from b in callList.Where(o => o.sID == a.sID)//.DefaultIfEmpty()
            //           select new
            //           {
            //               //Status = b?.Status,
            //               a,
            //               b,
            //               //sID = a.sID,
            //               //nTermSubLevel2 = a.nTermSubLevel2,
            //               //NickName = a.NickName,
            //               //FirstName = a.FirstName,
            //               //LastName = a.LastName,
            //               //Level1 = a.Level1.Replace("ศึกษาปีที่", "").Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส").Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช"),
            //               //Level2 = a.Level2,
            //               //Code = a.Code,
            //               //Receiver = "",
            //               //Img = a.Img,
            //               //Title =  a.Title,
            //               //status = b?.Status,

            //           };
            //return new
            //{
            //    Status1 = data.Where(o => o.b.Status == 1)
            //    .OrderByDescending(o => o.b.Created)
            //    .Select( o => new {

            //        schoolID = schoolId,
            //        sID = o.a.sID,
            //        Sound = sound.Base64Sound,
            //        NickName = std.NickName + "",
            //        FullName = $"{std.Title} {std.FirstName} {std.LastName} ",
            //        Level = $"{std.Level1}/{std.Level2}",
            //        Code = std.Code,
            //        Img = std.Img,
            //        Time = DateTime.Now.ToString("HH:mm:ss"),
            //        Reciever = "",

            //        sID = o.a.sID,
            //        nTermSubLevel2 = o.a.nTermSubLevel2,
            //        NickName = o.a.NickName,
            //        FirstName = o.a.FirstName,
            //        LastName = o.a.LastName,
            //        Level1 = o.a.Level1.Replace("ศึกษาปีที่", "").Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส").Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช"),
            //        Level2 = o.a.Level2,
            //        Code = o.a.Code,
            //        Receiver = "",
            //        Img = o.a.Img,
            //        Title = o.a.Title,
            //        Time = o.b.Created?.ToString("HH:mm:ss"),
            //    }),

            //    Status2 = data.Where(o => o.b.Status == 2)
            //    .OrderByDescending(o => o.b.Created)
            //    .Select(o => new {
            //        sID = o.a.sID,
            //        nTermSubLevel2 = o.a.nTermSubLevel2,
            //        NickName = o.a.NickName,
            //        FirstName = o.a.FirstName,
            //        LastName = o.a.LastName,
            //        Level1 = o.a.Level1.Replace("ศึกษาปีที่", "").Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส").Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช"),
            //        Level2 = o.a.Level2,
            //        Code = o.a.Code,
            //        Receiver = "",
            //        Img = o.a.Img,
            //        Title = o.a.Title,
            //        Time = o.b.Announced?.ToString("HH:mm:ss"),
            //    }),

            //    Status3 = data.Where(o => o.b.Status == 3)
            //    .OrderByDescending(o => o.b.Created)
            //    .Select(o => new {
            //        sID = o.a.sID,
            //        nTermSubLevel2 = o.a.nTermSubLevel2,
            //        NickName = o.a.NickName,
            //        FirstName = o.a.FirstName,
            //        LastName = o.a.LastName,
            //        Level1 = o.a.Level1.Replace("ศึกษาปีที่", "").Replace("ประกาศนียบัตรวิชาชีพชั้นสูง", "ป ว ส").Replace("ประกาศนียบัตรวิชาชีพ", "ป ว ช"),
            //        Level2 = o.a.Level2,
            //        Code = o.a.Code,
            //        Receiver = "",
            //        Img = o.a.Img,
            //        Title = o.a.Title,
            //        Time = o.b.Completed?.ToString("HH:mm:ss"),
            //    })
            //};
        }
    }
}