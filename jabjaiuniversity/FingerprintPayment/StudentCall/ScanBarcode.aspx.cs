using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public partial class ScanBarcode : BaseStudentCall
    {
        // protected override int MenuID => PermissionHelper.MENUID_STUDENTCALL_MAIN;
        protected string TOKEN { get; set; }
        protected TCompany Company = new TCompany();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                TOKEN = "Default-" + UserData.CompanyID;

                Company = dbmaster.TCompanies.Where(o => o.nCompany == UserData.CompanyID)
                    .Select(o => new
                    {
                        o.sImage,
                        o.sCompany,
                    })
                    .AsEnumerable()
                    .Select(o => new TCompany
                    {
                        sImage = o.sImage,
                        sCompany = o.sCompany,
                    })
                    .FirstOrDefault();
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod()]
        public static object ScanInput(string barcode)
        {
            var userData = GetUserData();

            var schoolId = userData.CompanyID;
            //var schoolId = UserData.CompanyID;

            using (var dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                var q = dbmaster.TParent_Card
                    .Where(o => o.SchoolID == schoolId
                        && o.Barcode == barcode.Trim()
                        && o.IsDel == false
                        && o.IsActive == true)
                    .FirstOrDefault();

                if (q != null)
                {
                    var today = DateTime.Now.Date;

                    var callToday = dbmaster.TStudentCall
                        .Where(o => o.SchoolID == schoolId && o.sID == q.sID && o.CallDate == today)
                        .Count();

                    if (callToday == 0)
                    {
                        var url = ConfigurationManager.AppSettings["StudentCallHubUrl"] + "";

                        if (url == "")
                            url = "https://signalr-studentcall.schoolbright.co/";

                        var connection = new HubConnection(url);
                        var hub = connection.CreateHubProxy("StdCallingHub");

                        connection.Start().Wait();
                        hub.Invoke("SendAnnouncement", schoolId, q.sID, q.No, "BCOD").Wait();
                        connection.Stop();

                        //var sc = new TStudentCall
                        //{
                        //    CardNo = q.No,
                        //    CallDate = today,
                        //    sID = q.sID,
                        //    SchoolID = schoolId,
                        //    Created = DateTime.Now.TimeOfDay,
                        //    Status = 1,
                        //    ScanType = "BCOD",
                        //};

                        //dbmaster.TStudentCall.Add(sc);

                        //dbmaster.SaveChanges();

                        //using (var ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                        //{
                        //    var sl = new StudentLogic(ctx);

                        //    var thisTerm = sl.GetTermId(UserData);

                        //    var s = ctx.TB_StudentViews
                        //        .Where(o => o.sID == sc.sID && o.nTerm == thisTerm && o.cDel == null)
                        //        .FirstOrDefault();

                        //    var u = ctx.TUsers.Where(o => o.sID == sc.sID).FirstOrDefault();

                        //    return new
                        //    {
                        //        status = "2",
                        //        data = new
                        //        {
                        //            sID = s.sID,
                        //            imgProfile = (u.sStudentPicture+"").Trim(),
                        //            FullName = $"{s.titleDescription} {s.sName} {s.sLastname}",
                        //            Level = $"{s.SubLevel}/{s.nTSubLevel2}",
                        //            Code = s.sStudentID,
                        //            Time = sc.Created
                        //        }
                        //    };
                        //}

                        return new { status = "2", desc = "waiting", data = new { } };
                    }
                    else
                    {
                        return new { status = "1", desc = "announced", data = new { } };
                    }
                }
                else
                {
                    return new { status = "0", desc = "not found", data = new { } };
                }
            }

        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static object InitialPage(int schoolId)
        {
            //var schoolId = UserData.CompanyID;
            var toDay = DateTime.Now.Date;

            string sql = $@"-- StudentCall/Scanbarcode
SELECT A.SchoolID,A.sID
, A.sNickName 'NickName' , ISNULL(F.titleDescription,A.sStudentTitle) 'Title', A.sName 'FirstName' , A.sLastname  'LastName'
, E.fullName 'Level1', D.nTSubLevel2 'Level2' 
, A.sStudentID 'Code' , A.sStudentPicture 'Img'
, CASE WHEN B.Status = 1 THEN B.Created WHEN B.Status = 2 THEN B.Announced WHEN B.Status = 3 THEN B.Completed END 'tTime'

FROM JabjaiSchoolSingleDB.dbo.TUser A
INNER JOIN JabjaiMasterSingleDB.dbo.TStudentCall B on A.SchoolID = B.SchoolID and A.sID = B.sID 
INNER JOIN JabjaiSchoolSingleDB.dbo.TTermSubLevel2 D on A.SchoolID = D.SchoolID and A.nTermSubLevel2 = D.nTermSubLevel2
INNER JOIN JabjaiSchoolSingleDB.dbo.TSubLevel E on D.SchoolID = E.SchoolID and D.nTSubLevel = E.nTSubLevel
LEFT JOIN  JabjaiSchoolSingleDB.dbo.TTitleList F on A.SchoolID = F.SchoolID and A.sStudentTitle   = CAST( F.nTitleid as nvarchar)

WHERE A.SchoolID = {schoolId} and  B.CallDate = '{toDay.ToString("yyyyMMdd")}'  and B.ScanType = 'BCOD'";

            using (JabJaiMasterEntities dbmaster = MasterEntity.Connection.MasterEntities(ConnectionDB.Read))
            {
                var lst = dbmaster.Database.SqlQuery<ModelStudent>(sql).ToList();

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
                    NickName = o.NickName + "",
                    FullName = $"{o.Title} {o.FirstName} {o.LastName} ",
                    Level = $"{o.Level1}/{o.Level2}",
                    Code = o.Code,
                    imgProfile = (o.Img + "").Trim(),
                    Time = o.Time,
                    o.tTime,
                    //  Time = DateTime.Now.ToString("HH:mm:ss"),
                    //Reciever = o.Receiver,
                });

                return new
                {
                    lstdata = d.OrderBy(o => o.tTime),
                };
            }

        }
    }
}