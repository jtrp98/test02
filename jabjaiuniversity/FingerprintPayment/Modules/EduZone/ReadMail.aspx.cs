using FingerprintPayment.Class;
using FingerprintPayment.StudentInfo.CsCode;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Modules.EduZone
{
    public partial class ReadMail : StudentGateway
    {
        protected string AttachedFilesDisplay = "display: none;";

        protected int MailID = 0;
        protected string ShowInputReply = "hide"; // show, hide

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        #region Method

        private void InitialPage()
        {
            string v = Request.QueryString["v"];

            switch (v)
            {
                case "list":
                    MvMainContent.ActiveViewIndex = 0;
                    MvScript.ActiveViewIndex = 0;
                    break;
                case "form":
                    int mid = Convert.ToInt32(Request.QueryString["mid"]);

                    MvMainContent.ActiveViewIndex = 1;
                    MvScript.ActiveViewIndex = 1;

                    ViewMail(mid);

                    break;
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadMail()
        {
            var jsonStream = "";
            HttpContext.Current.Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                jsonStream = inputStream.ReadToEnd();
            }
            var serializer = new JavaScriptSerializer();
            dynamic jsonObject = serializer.Deserialize(jsonStream, typeof(object));

            int draw = Convert.ToInt32(jsonObject["draw"]);
            int pageIndex = Convert.ToInt32(jsonObject["page"]);
            int pageSize = Convert.ToInt32(jsonObject["length"]);
            string sortIndex = Convert.ToString(jsonObject["order"][0]["column"]);
            string orderDir = Convert.ToString(jsonObject["order"][0]["dir"]);

            string sortBy = "MailID";
            switch (sortIndex)
            {
                case "2": sortBy = "Title"; break;
                case "3": sortBy = "MailContent"; break;
                case "5": sortBy = "MailDate"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string search = Convert.ToString(jsonObject["search"]);

            var json = Class.QueryEngine.LoadMailJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, search);

            return json;
        }

        public void ViewMail(int mailID)
        {
            MailID = mailID;
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQuery = string.Format(@"
SELECT ml.MailID, ml.AgencyID, ml.Title, ml.MailContent, ml.MailDate
, (SELECT FileUrl 'fileUrl' FROM [JabJai-EduZone].[dbo].TAttachedFile af WHERE af.AgencyID = ml.AgencyID AND af.MailID = ml.MailID AND af.IsDel = 0 FOR JSON AUTO) 'Files' 
, (SELECT sml.SchoolID 'schoolID', sml.sCompany 'schoolName' FROM (SELECT sml.SchoolID, c.sCompany, sml.AgencyID, sml.MailID, sml.IsDel FROM [JabJai-EduZone].[dbo].TSchoolMailList sml LEFT JOIN JabjaiMasterSingleDB.dbo.TCompany c ON sml.SchoolID = c.nCompany) sml WHERE sml.AgencyID = ml.AgencyID AND sml.MailID = ml.MailID AND sml.IsDel = 0 FOR JSON AUTO) 'Schools'
FROM [JabJai-EduZone].[dbo].TSchoolMailList sml 
LEFT JOIN [JabJai-EduZone].[dbo].TMailList ml ON sml.AgencyID = ml.AgencyID AND sml.MailID = ml.MailID
WHERE sml.SchoolID = {0} AND ml.IsDel = 0 AND ml.MailID = {1}
", schoolID, mailID);
                EntityElectronicMail entityElectronicMail = en.Database.SqlQuery<EntityElectronicMail>(sqlQuery).FirstOrDefault();

                if (entityElectronicMail != null)
                {
                    ltrTitle.Text = entityElectronicMail.Title;
                    ltrMailDate.Text = entityElectronicMail.MailDate?.ToString("ddd dd MMM yyyy เวลา HH:mm", new CultureInfo("th-TH"));

                    if (entityElectronicMail.Files != null)
                    {
                        AttachedFilesDisplay = "";

                        var fileData = JsonConvert.DeserializeObject<List<FileData>>(entityElectronicMail.Files);
                        foreach (var file in fileData)
                        {
                            ltrAttachedFiles.Text += string.Format(@"<a href=""{0}"" target=""_blank"">
    <div class=""attached-file"">
        <div class=""rounded-circle icon-attached-file"">W</div>
        <p>{1}</p>
    </div>
</a>", file.FileUrl, Path.GetFileName(HttpUtility.UrlDecode(file.FileUrl)));
                        }
                    }

                    ltrMailContent.Text = entityElectronicMail.MailContent;

                    // Load Mail Reply
                    sqlQuery = string.Format(@"
SELECT mr.MailReplyID, mr.ReplyContent, mr.ReplyDate, mr.ReplyUserType, mr.ReplyUserID, mr.ReplyRefID, mr.ActiveReply
, (CASE WHEN mr.ReplyUserType = 1 THEN u1.ProfileImage ELSE u2.sPicture END) 'ProfileImage'
, (CASE WHEN mr.ReplyUserType = 1 THEN u1.FirstName+' '+u1.LastName ELSE u2.sName+' '+u2.sLastname END) 'FullName'
FROM [JabJai-EduZone].[dbo].TMailReply mr 
LEFT JOIN [JabJai-EduZone].[dbo].[TUser] u1 ON mr.ReplyUserID = u1.ID
LEFT JOIN TEmployees u2 ON mr.ReplyUserID = u2.sEmp AND u2.SchoolID = {2}
WHERE mr.MailID={0} AND mr.AgencyID={1} AND mr.SchoolID={2}", entityElectronicMail.MailID, entityElectronicMail.AgencyID, schoolID);
                    List<MailReplyData> mailReplyDatas = en.Database.SqlQuery<MailReplyData>(sqlQuery).ToList();
                    if (mailReplyDatas.Count > 0)
                    {
                        int replyNo = 0;
                        string allReplyContent = "";

                        foreach (var r in mailReplyDatas)
                        {
                            string replyType = "";
                            string replyAction = "";
                            switch (r.ReplyUserType)
                            {
                                case 1:
                                    replyType = "office";
                                    break;
                                case 2:
                                    replyType = "school";
                                    break;
                            }

                            // Check last reply & check reply user type
                            if ((replyNo + 1) == mailReplyDatas.Count && r.ReplyUserType == 1)
                            {
                                replyAction = string.Format(@"
            <div class=""reply-action"" data-reply-id=""{0}"" data-mode=""new"">
                <span class=""material-icons"" style=""vertical-align: middle;"">reply</span> ตอบกลับ
            </div>", r.MailReplyID);
                            }
                            else if ((replyNo + 1) == mailReplyDatas.Count && r.ReplyUserID == UserData.UserID)
                            {
                                // Check last reply message & is owner reply message
                                replyAction = string.Format(@"
            <div class=""reply-action"" data-reply-id=""{0}"" data-mode=""edit"">
                <span class=""material-icons"" style=""vertical-align: middle;"">reply</span> แก้ไขข้อความ
            </div>", r.MailReplyID);
                            }

                            allReplyContent += string.Format(@"
<div class=""row {1}-reply title"">
    <div class=""col-md-12"">
        <h4 class=""card-title"" style=""font-weight: bold;"">ความคิดเห็นที่ 1{0}</h4>
    </div>
</div>
<div class=""row {1}-reply"">
    <div class=""col-md-12"">
        <div style=""border: 1px solid #ccc; border-radius: 3px; padding: 17px 17px 15px 17px; min-height: 120px; display: flow-root;"">
            <div class=""div-text-span"" style=""margin-left: 35px; margin-bottom: 45px;"">
                {2}
            </div>
            <div style=""display: flex; position: absolute; bottom: 0px; margin-bottom: 12px;"">
                <img src=""{3}"" alt=""user"" style=""width: 35px; height: 35px; border-radius: 50%; margin-right: 5px;"">
                <p style=""margin: 6px 0px 0px 10px;"">คุณ{4}&nbsp;&nbsp;&nbsp;{5}</p>
            </div>
            {6}
        </div>
    </div>
</div>", (replyNo == 0 ? "" : "-" + replyNo), replyType, r.ReplyContent, (string.IsNullOrEmpty(r.ProfileImage) ? "/Content/VisitHouse/assets/img/user.png" : r.ProfileImage), r.FullName, r.ReplyDate.ToString("ddd dd MMM yyyy เวลา HH:mm", new CultureInfo("th-TH")), replyAction);

                            replyNo++;
                        }

                        ltrReply.Text = allReplyContent;
                    }
                    else
                    {
                        ShowInputReply = "show";
                    }
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static string SaveReplyMessage(int mailId, int replyRefId, string replyMessage, string mode)
        {
            bool success = true;
            string message = "Reply message complete.";

            try
            {
                var userData = GetUserData();
                int schoolID = userData.CompanyID;
                using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    string query = "";
                    if (mode == "new")
                    {
                        // Get mail info
                        query = string.Format(@"SELECT AgencyID FROM [JabJai-EduZone].[dbo].TMailList WHERE MailID = {0}", mailId);
                        int AgencyID = en.Database.SqlQuery<int>(query).FirstOrDefault();

                        en.Database.ExecuteSqlCommand("INSERT INTO [JabJai-EduZone].[dbo].TMailReply (AgencyID, MailID, SchoolID, ReplyContent, ReplyDate, ReplyUserType, ReplyUserID, ReplyRefID, ActiveReply, CreateDate, CreateBy, IsDel) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11})",
                            AgencyID, mailId, schoolID, replyMessage.Trim(), DateTime.Now, 2, userData.UserID, replyRefId, 1, DateTime.Now, userData.UserID, 0);

                        // Update active last reply to false (not see reply action)
                        if (replyRefId != 0)
                        {
                            query = string.Format(@"UPDATE [JabJai-EduZone].[dbo].TMailReply SET ActiveReply = 0 WHERE MailReplyID = {0}", replyRefId);
                            en.Database.ExecuteSqlCommand(query);
                        }
                    }
                    else if (mode == "edit")
                    {
                        query = string.Format(@"UPDATE [JabJai-EduZone].[dbo].TMailReply SET ReplyContent = @ReplyContent, ReplyDate = @ReplyDate, UpdateDate = @UpdateDate, UpdateBy = @UpdateBy WHERE MailReplyID = {0}", replyRefId);
                        en.Database.ExecuteSqlCommand(query
                            , new SqlParameter("@ReplyContent", replyMessage.Trim())
                            , new SqlParameter("@ReplyDate", DateTime.Now)
                            , new SqlParameter("@UpdateDate", DateTime.Now)
                            , new SqlParameter("@UpdateBy", userData.UserID));

                        message = "Edit message complete.";
                    }
                }
            }
            catch (Exception error)
            {
                success = false;
                message = error.Message;
            }

            var result = new { success, message };

            return JsonConvert.SerializeObject(result);
        }

        #endregion

        public class FileData
        {
            [JsonProperty(PropertyName = "keyFile")]
            public string KeyFile { get; set; }

            [JsonProperty(PropertyName = "fileName")]
            public string FileName { get; set; }

            [JsonProperty(PropertyName = "fullPathFile")]
            public string FullPathFile { get; set; }

            [JsonProperty(PropertyName = "fileUrl")]
            public string FileUrl { get; set; }
        }

        public class MailReplyData
        {
            [JsonProperty(PropertyName = "mailReplyID")]
            public int MailReplyID { get; set; }

            [JsonProperty(PropertyName = "replyContent")]
            public string ReplyContent { get; set; }

            [JsonProperty(PropertyName = "replyDate")]
            public DateTime ReplyDate { get; set; }

            [JsonProperty(PropertyName = "replyUserType")]
            public int ReplyUserType { get; set; }

            [JsonProperty(PropertyName = "replyUserID")]
            public int ReplyUserID { get; set; }

            [JsonProperty(PropertyName = "replyRefID")]
            public int ReplyRefID { get; set; }

            [JsonProperty(PropertyName = "activeReply")]
            public bool ActiveReply { get; set; }

            [JsonProperty(PropertyName = "profileImage")]
            public string ProfileImage { get; set; }

            [JsonProperty(PropertyName = "fullName")]
            public string FullName { get; set; }
        }
    }
}