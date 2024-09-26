using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace FingerprintPayment.App_Logic
{
    /// <summary>
    /// Summary description for MessageSystemHandler
    /// </summary>
    public class MessageSystemHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        private JWTToken.userData userData = new JWTToken.userData();
        public void ProcessRequest(HttpContext context)
        {
            JWTToken token = new JWTToken();
            if (token.CheckToken(HttpContext.Current)) { userData = token.getTokenValues(HttpContext.Current); }

            context.Response.ContentType = "application/json";

            bool success = true;
            string message = "Load data complete.";

            List<object> listData = new List<object>();
            List<object> WorklistData = new List<object>();
            object getData = null;

            try
            {
                string strJson = new StreamReader(context.Request.InputStream).ReadToEnd();

                //deserialize the object
                MessageSystemInfo info = new JavaScriptSerializer().Deserialize<MessageSystemInfo>(strJson);
                if (info != null)
                {
                    using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
                    {
                        switch (info.method)
                        {
                            case "list":
                                {
                                    DateTime? dateLastNotification = null;
                                    if (ComFunction.CookieExist("MessageSystem", "LastNotification"))
                                    {
                                        string cookieLastNotification = ComFunction.GetFromCookie("MessageSystem", "LastNotification");
                                        dateLastNotification = DateTime.ParseExact(cookieLastNotification, "dd/MM/yyyy HH:mm:ss.fff", new CultureInfo("en-US"));
                                    }

                                    DateTime? dateLastNotificationHomework = null;
                                    if (ComFunction.CookieExist("MessageSystem", "HomeworkLastNotification"))
                                    {
                                        string cookieLastNotification = ComFunction.GetFromCookie("MessageSystem", "HomeworkLastNotification");
                                        dateLastNotificationHomework = DateTime.ParseExact(cookieLastNotification, "dd/MM/yyyy HH:mm:ss.fff", new CultureInfo("en-US"));
                                    }

                                    var invoiceSql = "";
                                    var hasPermission = AdminMain.HasInvoicePermission(userData.CompanyID, userData.UserID);
                                    if (hasPermission)//แสดงใบแจ้งหนี้และใบเสร็จให้กับ user ที่มีสิทธิ์
                                    {
                                        invoiceSql= $@"
                                        UNION
	                                    SELECT TOP 10 NewsID 'ID', Created 'AddDate', Title, NULL 'Content', '3' 'Type' FROM TNews WHERE remark in('RemindInvoice') AND ISNULL(ISDone,0)=0 and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate
                                        UNION                                        
                                        SELECT TOP 10 NewsID 'ID', Created 'AddDate', Title, NULL 'Content', '3' 'Type' FROM TNews WHERE remark in('RemindReceipt') AND ISNULL(ISDone,0)=1 and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate";
                                     }

                                    string query = $@"SELECT TOP 10 *
                                                    FROM
                                                    (
	                                                    SELECT TOP 10 ID, AddDate, Title, NULL 'Content', '1' 'Type' FROM TMessageSystem ORDER BY AddDate DESC
	                                                    UNION
	                                                    SELECT TOP 10 NewsID 'ID', Created 'AddDate', Title, NULL 'Content', '2' 'Type' FROM TNews WHERE IsBroadcast = 1 ORDER BY Created DESC      
                                                        UNION 
                                                        SELECT TOP 10 NewsID, Created 'AddDate', Title, NULL 'Content', '3' 'Type' FROM TNews WHERE remark in ('RemindBillSetting','RemindPayment','RemindSchoolInActive') and ISNULL(ISDone,0)=0 and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate
                                                        UNION
                                                        SELECT TOP 10 MailID 'ID', MailDate 'AddDate', Title, CAST(MailContent AS VARCHAR(MAX)) 'Content', '4' 'Type' FROM [JabJai-EduZone].[dbo].[TMailList] WHERE MailID IN (SELECT MailID FROM [JabJai-EduZone].[dbo].[TSchoolMailList] WHERE SchoolID={userData.CompanyID}) ORDER BY MailDate DESC

                                                        {invoiceSql}
                                                    ) A
                                                    ORDER BY AddDate DESC";

                                    var list = dbMaster.Database.SqlQuery<NotificationModel>(query).ToList();

                                    if (list.Count > 0)
                                    {
                                        foreach (var l in list)
                                        {
                                            bool flag = false;
                                            flag = dateLastNotification != null && dateLastNotification < l.AddDate;

                                            string timeAgo = ComFunction.GetTimeSince(l.AddDate);
                                            listData.Add(new
                                            {
                                                id = l.ID,
                                                title = l.Title,
                                                content = Regex.Replace(l.Content, @"<(.|\n)*?>", ""),
                                                timeAgo,
                                                flag,
                                                type = l.Type
                                            });
                                        }
                                    }


                                    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                                    {
                                        var teacherId = userData.UserID;
                                        var schoolId = userData.CompanyID;

                                        var qry1 = from a in ctx.TClassOnlines.Where(o => o.SchoolId == schoolId && (o.cDel ?? false) == false && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                                                   from b in ctx.THomeworks.Where(o => o.SchoolID == a.SchoolId && o.OnlineId == a.OnlineId && o.cDel != true)
                                                   from c in ctx.THomework_User.Where(o => o.SchoolID == b.SchoolID && o.IsSend == true && o.nHomeWork == b.nHomeWork && o.cDel != true)
                                                   from d in ctx.TPlanes.Where(o => o.SchoolID == a.SchoolId && o.sPlaneID == a.PlanId && o.cDel == null)
                                                       //from e in ctx.TSubLevels.Where( o => o.nTSubLevel == a.LevelId && o.cDel != true)

                                                   select new
                                                   {
                                                       Id = b.nHomeWork,
                                                       Group = a.TitleName,
                                                       HomeWork = b.TitleName,
                                                       SendDate = c.UpdatedDate,

                                                       Subject = d.sPlaneName,
                                                       SubjectCode = d.courseCode,
                                                   };

                                        var data1 = qry1.GroupBy(o => new { o.Id, o.Group, o.HomeWork, o.Subject, o.SubjectCode })
                                            .Select(o => new
                                            {
                                                LastDate = o.Max(i => i.SendDate),
                                                NewCount = o.Count(),//o.Count(i => i.SendDate > dateLastNotification),
                                                o.Key.Group,
                                                o.Key.HomeWork,
                                                o.Key.Subject,
                                                o.Key.SubjectCode,
                                                o.Key.Id,

                                                Type = 1,
                                            })
                                            .Where(o => o.NewCount > 0)
                                            .OrderByDescending(o => o.LastDate)
                                            .Take(10)
                                            .ToList();

                                        var qry2 = from a in ctx.TClassOnlines.Where(o => o.SchoolId == schoolId && (o.cDel ?? false) == false && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                                                   from b in ctx.THomeworks.Where(o => o.SchoolID == a.SchoolId && o.OnlineId == a.OnlineId && o.cDel != true)
                                                   from c in ctx.THomeWorkReplies.Where(o => o.SchoolId == b.SchoolID && o.HomeWorkId == b.nHomeWork)
                                                   from d in ctx.TPlanes.Where(o => o.SchoolID == a.SchoolId && o.sPlaneID == a.PlanId && o.cDel == null)

                                                   select new
                                                   {
                                                       Id = b.nHomeWork,
                                                       Group = a.TitleName,
                                                       HomeWork = b.TitleName,
                                                       SendDate = c.Created,

                                                       Subject = d.sPlaneName,
                                                       SubjectCode = d.courseCode,
                                                   };


                                        var data2 = qry2.GroupBy(o => new { o.Id, o.Group, o.HomeWork, o.Subject, o.SubjectCode })
                                            .Select(o => new
                                            {
                                                LastDate = o.Max(i => i.SendDate),
                                                NewCount = o.Count(),// o.Count(i => i.SendDate > dateLastNotification),
                                                o.Key.Group,
                                                o.Key.HomeWork,

                                                o.Key.Subject,
                                                o.Key.SubjectCode,
                                                o.Key.Id,

                                                Type = 2,
                                            })
                                            .Where(o => o.NewCount > 0)
                                            .OrderByDescending(o => o.LastDate)
                                            .Take(10)
                                            .ToList();

                                        var lst = data1.Concat(data2).OrderByDescending(o => o.LastDate).Take(10).ToList();

                                        if (lst.Count > 0)
                                        {
                                            foreach (var l in lst)
                                            {
                                                bool flag = false;
                                                flag = dateLastNotificationHomework != null && dateLastNotificationHomework < l.LastDate;

                                                string timeAgo = ComFunction.GetTimeSince(l.LastDate.Value);

                                                WorklistData.Add(new
                                                {
                                                    id = l.Id,
                                                    group = l.Group,
                                                    homework = l.HomeWork,
                                                    subject = l.Subject,
                                                    code = l.SubjectCode,
                                                    timeAgo,
                                                    flag,
                                                    type = l.Type,
                                                    count = l.NewCount,
                                                });
                                            }
                                        }
                                    }
                                }
                                break;
                            case "get":
                                {
                                    switch (info.type)
                                    {
                                        case "1":
                                            TMessageSystem messageSystem = dbMaster.TMessageSystems.Where(w => w.ID == info.id).FirstOrDefault();
                                            if (messageSystem != null)
                                            {
                                                getData = new
                                                {
                                                    title = messageSystem.Title,
                                                    message = messageSystem.Message,
                                                    date = messageSystem.AddDate.Value.ToString("dd/MM/yyyy HH:mm น.", new CultureInfo("th-TH"))
                                                };
                                            }
                                            else
                                            {
                                                success = false;
                                                message = "No info.";
                                            }
                                            break;
                                        case "2":
                                            var news = dbMaster.TNews2
                                                .Where(w => w.NewsID == info.id && w.IsNoteAppWeb == true && w.IsDelete == false).FirstOrDefault();
                                            if (news != null)
                                            {
                                                getData = new
                                                {
                                                    title = news.Title,
                                                    message = news.NoteAppWeb,
                                                    date = news.Created.Value.ToString("dd/MM/yyyy HH:mm น.", new CultureInfo("th-TH"))
                                                };
                                            }
                                            else
                                            {
                                                success = false;
                                                message = "No info.";
                                            }
                                            break;
                                        case "3":
                                            var msg = dbMaster.TNews
                                                .Where(w => w.NewsID == info.id).FirstOrDefault();
                                            if (msg != null)
                                            {
                                                getData = new
                                                {
                                                    title = msg.Title,
                                                    message = msg.Detail,
                                                    date = msg.Created.Value.ToString("dd/MM/yyyy HH:mm น.", new CultureInfo("th-TH"))
                                                };
                                            }
                                            else
                                            {
                                                success = false;
                                                message = "No info.";
                                            }
                                            break;
                                        case "4":
                                            string query = $@"SELECT Title, MailContent 'Message', MailDate 'Date' FROM [JabJai-EduZone].[dbo].[TMailList] WHERE MailID={info.id}";
                                            var mailObj = dbMaster.Database.SqlQuery<EduZoneMailList>(query).FirstOrDefault();
                                            if (mailObj != null)
                                            {
                                                getData = new
                                                {
                                                    title = mailObj.Title,
                                                    message = mailObj.Message,
                                                    date = mailObj.Date.Value.ToString("dd/MM/yyyy HH:mm น.", new CultureInfo("th-TH"))
                                                };
                                            }
                                            else
                                            {
                                                success = false;
                                                message = "No info.";
                                            }
                                            break;
                                    }
                                }
                                break;
                            case "update-last-noti":
                                {
                                    var invoiceSql = "";
                                    var hasPermission = AdminMain.HasInvoicePermission(userData.CompanyID, userData.UserID);
                                    if (hasPermission)
                                    {
                                        invoiceSql=$@"
                                        UNION
                                        SELECT TOP 1 NewsID 'ID', Created 'AddDate', Title, '3' 'Type' FROM TNews WHERE remark in('RemindInvoice','RemindReceipt') and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate";
                                    }

                                    string query = $@"SELECT TOP 1 *
                                                    FROM
                                                    (
	                                                    SELECT TOP 1 ID, AddDate, Title, '1' 'Type' FROM TMessageSystem ORDER BY AddDate DESC
	                                                    UNION
	                                                    SELECT TOP 1 NewsID 'ID', Created 'AddDate', Title, '2' 'Type' FROM TNews WHERE IsBroadcast = 1 ORDER BY Created DESC
                                                        UNION
                                                        SELECT TOP 1 NewsID, Created 'AddDate', Title, '3' 'Type' FROM TNews WHERE remark in ('RemindBillSetting','RemindPayment','RemindSchoolInActive') and ISNULL(ISDone,0)=0 and SchoolId = {userData.CompanyID} AND GETDATE() BETWEEN StartDate AND EndDate
                                                        UNION
                                                        SELECT TOP 1 MailID 'ID', MailDate 'AddDate', Title, '4' 'Type' FROM [JabJai-EduZone].[dbo].[TMailList] WHERE MailID IN (SELECT MailID FROM [JabJai-EduZone].[dbo].[TSchoolMailList] WHERE SchoolID={userData.CompanyID}) ORDER BY MailDate DESC
                                                        
                                                        {invoiceSql}
                                                    ) A
                                                    ORDER BY AddDate DESC";

                                    var data = dbMaster.Database.SqlQuery<NotificationModel>(query).FirstOrDefault();
                                    if (data != null)
                                    {
                                        // Update cookie
                                        ComFunction.StoreInCookie("MessageSystem", null, "LastNotification", data.AddDate.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
                                    }

                                    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(userData.CompanyID,ConnectionDB.Read)))
                                    {
                                        var teacherId = userData.UserID;
                                        var schoolId = userData.CompanyID;

                                        var qry1 = from a in ctx.TClassOnlines.Where(o => o.SchoolId == schoolId && (o.cDel ?? false) == false && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                                                   from b in ctx.THomeworks.Where(o => o.SchoolID == a.SchoolId && o.OnlineId == a.OnlineId && o.cDel != true)
                                                   from c in ctx.THomework_User.Where(o => o.SchoolID == b.SchoolID && o.IsSend == true && o.nHomeWork == b.nHomeWork && o.cDel != true)

                                                   select new
                                                   {
                                                       SendDate = c.UpdatedDate,
                                                   };

                                        var lastDate1 = qry1.Max(o => o.SendDate);

                                        var qry2 = from a in ctx.TClassOnlines.Where(o => o.SchoolId == schoolId && (o.cDel ?? false) == false && (o.TeacherId == teacherId || o.ShareId.Contains("|" + teacherId + "|")))
                                                   from b in ctx.THomeworks.Where(o => o.SchoolID == a.SchoolId && o.OnlineId == a.OnlineId && o.cDel != true)
                                                   from c in ctx.THomeWorkReplies.Where(o => o.SchoolId == b.SchoolID && o.HomeWorkId == b.nHomeWork)

                                                   select new
                                                   {
                                                       SendDate = c.Created,
                                                   };

                                        var lastDate2 = qry2.Max(o => o.SendDate);

                                        var _max = new List<DateTime?>() {
                                        lastDate1 ?? DateTime.Now,
                                        lastDate2 ?? DateTime.Now,
                                    }.Max();

                                        if (_max.HasValue)
                                        {
                                            ComFunction.StoreInCookie("MessageSystem", null, "HomeworkLastNotification", _max.Value.ToString("dd/MM/yyyy HH:mm:ss.fff"), null);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                else
                {
                    success = false;
                    message = "No info.";
                }
            }
            catch (Exception err)
            {
                success = false;
                message = err.Message;
            }

            var result = new { success, message, listData, getData, WorklistData };

            context.Response.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable { get { return false; } }
    }

    public class MessageSystemInfo
    {
        public string method { get; set; }
        public int id { get; set; }
        public string type { get; set; }
    }

    public class EduZoneMailList
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime? Date { get; set; }
    }

}