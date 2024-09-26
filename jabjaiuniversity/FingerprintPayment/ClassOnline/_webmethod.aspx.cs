using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
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
    public partial class _webmethod : BaseOnlinePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Session["sEmpID"] + "")) Response.Redirect("/Default.aspx");

            if (!this.IsPostBack)
            {
                var schoolId = Request.QueryString["school"] + "";

                //if (!string.IsNullOrEmpty(schoolId))
                //{
                //    var id = Convert.ToInt32(schoolId);
                //    var aws = new AWSFunction();
                //    aws.UpdateAllTUserBySchool(id);
                //}

                //RemoveCollection();
            }
        }

        private void RemoveCollection()
        {
            var aws = new AWSFunction();

            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                var lst = dbmaster.TCompanies.ToList();

                foreach (var item in lst)
                {
                    _ = aws.DeleteCollection(item.nCompany);
                }
            }
        }

        private void ImportOldDataNFC()
        {
            using (JabJaiMasterEntities dbmaster = Connection.MasterEntities(ConnectionDB.Read))
            {
                JWTToken token = new JWTToken();
                var userData = new JWTToken().UserData;
                if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

                //var lst = dbmaster.TUser_Card
                //    .Where(o => o.NFC == "")
                //    .ToList();

                //foreach (var i in lst)
                //{
                //    try
                //    {
                //        i.NFC = fcommon.DeFormatStudentCardNumber(i.NFCEncrypt);
                //    }
                //    catch { }
                //}

                //dbmaster.SaveChanges();

                var lst = dbmaster.TUsers
                    .Where(o => o.NFC != null)
                    .ToList();

                var listCard = new List<TUser_Card>();

                foreach (var item in lst)
                {
                    var card = new TUser_Card()
                    {
                        sID = item.sID,
                        SchoolID = item.nCompany,
                        No = 1,
                        NFC = fcommon.DeFormatStudentCardNumber(item.NFC),
                        NFCEncrypt = item.NFC,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreateBy = userData.UserID,
                        ModifyBy = userData.UserID,
                        IsDel = false,
                        IsActive = true,
                    };

                    listCard.Add(card);
                    //dbmaster.TUser_Card.Add(card);
                }

                dbmaster.TUser_Card.AddRange(listCard);

                dbmaster.SaveChanges();
            }
        }

        //private void ImportTUserCard()
        //{
        //    var tusercard = new List<TUser_Card>();
        //    var tuser = new List<JabjaiEntity.DB.TUser>();
        //    var ttemp = new List<TImportTemp>();

        //    JWTToken token = new JWTToken();
        //    var userData = new JWTToken().UserData;
        //    if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }


        //    using (JabJaiEntities db = new JabJaiEntities(Connection.StringConnectionSchool(Session["sEntities"].ToString(), ConnectionDB.Read)))
        //    {
        //        tuser = db.TUsers.Where(o => o.SchoolID == 421).ToList();
        //        ttemp = db.TImportTemps.ToList();
        //    }

        //    using (JabJaiMasterEntities db = Connection.MasterEntities(ConnectionDB.Read))
        //    {
        //        tusercard = db.TUser_Card.Where(o => o.SchoolID == 421).ToList();

        //        int count1 = 0;
        //        int count2 = 0;
        //        foreach (var temp in ttemp)
        //        {
        //            var user = tuser.FirstOrDefault(o => o.sStudentID == temp.F4 + "");

        //            if (user != null)
        //            {
        //                var card = tusercard.FirstOrDefault(o => o.NFCEncrypt == temp.F3);

        //                if (card == null)
        //                {
        //                    count1++;

        //                    db.TUser_Card.Add(new TUser_Card()
        //                    {
        //                        sID = user.sID,
        //                        SchoolID = user.SchoolID,
        //                        No = 1,
        //                        NFC = fcommon.DeFormatStudentCardNumber(temp.F3),
        //                        NFCEncrypt = temp.F3,
        //                        Created = DateTime.Now,
        //                        Modified = DateTime.Now,
        //                        CreateBy = 333382,
        //                        ModifyBy = 333382,
        //                        IsDel = false,
        //                        IsActive = true,
        //                    });

        //                }
        //                else//update
        //                {
        //                    count2++;
        //                }
        //            }
        //        }

        //        db.SaveChanges();
        //    }
        //}

        [WebMethod()]
        [ScriptMethod(UseHttpGet = true)]
        public static string PageOneMethodOne()
        {
            return "hello world";
        }

        // [WebMethod(EnableSession = true)]
        public static string UploadWorkfile()
        {
            HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["FileUpload1"];

            //    if (file.FileName == "") continue;
            //    string linkfiles = UploadFileHttpRequestBase(file
            //        , "learningonline/work"
            //        , SchoolId
            //        , hw.nHomeWork);//await AzureStorage.UploadFile(files, "online_homework", schoolID);

            //    if (!string.IsNullOrEmpty(linkfiles))
            //    {
            //        ctx.THomeWorkFiles.Add(new THomeWorkFile
            //        {
            //            ContentType = file.ContentType,
            //            nHomeWorkId = hw.nHomeWork,
            //            sFileName = linkfiles,
            //            Title = file.FileName,
            //            SchoolId = SchoolId,
            //        });
            //    }

            return JsonConvert.SerializeObject(new { title = "", url = "", ctype = "" });
        }

        [WebMethod]
        public static string DeleteFile(HttpPostedFile FileUpload2)
        {
            HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["FileUpload2"];
            return "uploaded";
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public static string GetNotification()
        {
            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(HttpContext.Current.Session["sEntities"].ToString(), ConnectionDB.Read)))
            {
                var teacherId = Convert.ToInt32(HttpContext.Current.Session["sEmpID"] + "");

                DateTime? dateLastNotification = null;

                if (ComFunction.CookieExist("ClassOnline", "LastNotification"))
                {
                    string cookieLastNotification = ComFunction.GetFromCookie("ClassOnline", "LastNotification");

                    dateLastNotification = DateTime.ParseExact(cookieLastNotification, "dd/MM/yyyy HH:mm:ss.fff", new CultureInfo("en-US"));
                }
                else
                {
                    dateLastNotification = DateTime.Now;
                }

                var qry1 = from a in ctx.TClassOnlines.Where(o => o.cDel != true && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                           from b in ctx.THomeworks.Where(o => o.OnlineId == a.OnlineId && o.cDel != true)
                           from c in ctx.THomework_User.Where(o => o.UpdatedDate != null && o.IsSend == true && o.nHomeWork == b.nHomeWork)
                           from d in ctx.TPlanes.Where(o => o.sPlaneID == a.PlanId && o.cDel == null)
                               //from e in ctx.TSubLevels.Where( o => o.nTSubLevel == a.LevelId && o.cDel != true)

                           select new
                           {
                               Group = a.TitleName,
                               HomeWork = b.TitleName,
                               SendDate = c.UpdatedDate,

                               Subject = d.sPlaneName,
                               SubjectCode = d.courseCode,
                           };

                var data1 = qry1.GroupBy(o => new { o.Group, o.HomeWork, o.Subject, o.SubjectCode })
                    .AsEnumerable()
                    .Select(o => new
                    {
                        LastDate = o.Max(i => i.SendDate),
                        NewCount = o.Count(i => i.SendDate > dateLastNotification),
                        o.Key.Group,
                        o.Key.HomeWork,
                        o.Key.Subject,
                        o.Key.SubjectCode,
                        type = 1,
                    })
                    .OrderByDescending(o => o.LastDate)
                    .Take(10);


                var qry2 = from a in ctx.TClassOnlines.Where(o => o.cDel != true && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                           from b in ctx.THomeworks.Where(o => o.OnlineId == a.OnlineId && o.cDel != true)
                           from c in ctx.THomeWorkReplies.Where(o => o.HomeWorkId == b.nHomeWork)
                           from d in ctx.TPlanes.Where(o => o.sPlaneID == a.PlanId && o.cDel == null)

                           select new
                           {
                               Group = a.TitleName,
                               HomeWork = b.TitleName,
                               SendDate = c.Created,

                               Subject = d.sPlaneName,
                               SubjectCode = d.courseCode,
                           };


                var data2 = qry2.GroupBy(o => new { o.Group, o.HomeWork, o.Subject, o.SubjectCode })
                    .AsEnumerable()
                    .Select(o => new
                    {
                        LastDate = o.Max(i => i.SendDate),
                        NewCount = o.Count(i => i.SendDate > dateLastNotification),
                        o.Key.Group,
                        o.Key.HomeWork,

                        o.Key.Subject,
                        o.Key.SubjectCode,
                        type = 2,
                    })
                    .OrderByDescending(o => o.LastDate)
                    .Take(10);

                var lst = data1.Concat(data2).OrderByDescending(o => o.LastDate).Take(10).ToList();

                ComFunction.StoreInCookie("ClassOnline", null, "LastNotification", dateLastNotification.Value.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);

                return JsonConvert.SerializeObject(lst);
            }
        }
    }
}

