using FingerprintPayment.Message.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI.WebControls;
using urbanairship;

namespace FingerprintPayment.Message
{
    public partial class NewsEditForm : MessageGateway
    {
        protected string NewsDataScriptInit = "";
        protected string NewsUserData = "";
        protected string NewsFileData = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string query = "";

                query = string.Format(@"
SELECT SMSGroupID, SMSGroupName, GroupDefault 
FROM TSMSGroup 
WHERE SchoolID={0} AND Status=1 AND cDel=0
ORDER BY (CASE WHEN GroupDefault='all' THEN 4 WHEN GroupDefault='all-employee' THEN 3 WHEN GroupDefault='all-student' THEN 2 WHEN GroupDefault='all-student-level' THEN 1 ELSE 0 END) DESC, SMSGroupID
", schoolID);
                var listGroup = en.Database.SqlQuery<NewsGroup>(query).ToList();
                //var listGroup = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.Status == 1 && w.cDel == false).ToList();
                foreach (var l in listGroup)
                {
                    this.ltrGroup.Text += string.Format(@"<option value=""{0}"" data-default=""{2}"">{1}</option>", l.SMSGroupID, l.SMSGroupName, l.GroupDefault);
                }

                var listLevel = en.TSubLevels.Where(w => w.SchoolID == schoolID && w.nWorkingStatus == 1).ToList();
                foreach (var l in listLevel)
                {
                    this.ltrLevel.Text += string.Format(@"<option value=""{0}"" data-level=""{2}"">{1}</option>", l.nTSubLevel, l.SubLevel, l.nTLevel);
                }

                query = string.Format(@"
SELECT r.ClassroomID, r.Classroom, r.LevelID , r.LevelName
FROM
(
	SELECT t.nTermSubLevel2 'ClassroomID', s.SubLevel + ' / ' + t.nTSubLevel2 'Classroom', s.nTSubLevel 'LevelID', s.fullName 'LevelName', s.nTLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID);
                var listClassroomData = en.Database.SqlQuery<ClassroomData>(query).ToList();

                var listLevelData = listClassroomData.GroupBy(x => new { x.LevelID, x.LevelName })
                    .Select(x => new LevelData
                    {
                        LevelID = x.Key.LevelID,
                        LevelName = x.Key.LevelName
                    });

                string levelTree = "";
                string classTree = "";
                foreach (var l in listLevelData)
                {
                    classTree = "";

                    var classroomDatas = listClassroomData.Where(w => w.LevelID == l.LevelID).ToList();
                    foreach (var c in classroomDatas)
                    {
                        classTree += string.Format(@"
        <li>
            <label>
                <input class=""hummingbird-end-node classroom"" id=""cr{0}"" data-id=""{0}"" data-name=""{1}"" type=""checkbox"" />
                {1}
            </label>
        </li>", c.ClassroomID, c.Classroom);
                    }

                    levelTree += string.Format(@"
<li>
    <label>
        <input id=""lv{0}"" data-id=""{0}"" class=""level"" type=""checkbox"" />
        {1}
    </label>
    <i class=""fa fa-plus""></i>
    <ul>
        {2}
    </ul>
</li>", l.LevelID, l.LevelName, classTree);
                }
                this.ltrLevelTree.Text = levelTree;


                // Get news data
                int smsID = Convert.ToInt32(Request.QueryString["nid"]);
                var smsObj = en.TSMS.Where(w => w.SchoolID == schoolID && w.nSMS == smsID).FirstOrDefault();
                if (smsObj != null)
                {
                    string treeSMSSubLevel = "";
                    string individualType = "";
                    string sendFileWithOwner = "";
                    if (smsObj.SMSGroupType == 1)
                    {
                        var smsSubLevels = en.TSMSSubLevels.Where(w => w.SchoolID == schoolID && w.nSMS == smsID).ToList();
                        foreach (var sl in smsSubLevels)
                        {
                            treeSMSSubLevel += string.Format(@", 'cr{0}'", sl.nTSubLevel);
                        }

                        if (!string.IsNullOrEmpty(treeSMSSubLevel))
                        {
                            treeSMSSubLevel = string.Format(@"
                            $(""#treeview"").hummingbird('checkNode', {{
                                sel: 'id', 
                                vals: [{0}]
                            }});
                            $(""#treeview_container input[type=checkbox]"").attr('disabled', 'true');", treeSMSSubLevel.Remove(0, 2));
                        }
                    }
                    else if (smsObj.SMSGroupType == 2)
                    {
                        individualType = string.Format(@"$(""#sltIndividualType"").prop('disabled', true).selectpicker('refresh'); $('.send-individual-list').show();");
                    }

                    var isSendFileWithOwner = en.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == smsID && w.StudentCodeOwner == true).Count();
                    if (isSendFileWithOwner > 0)
                    {
                        sendFileWithOwner = string.Format(@"
            $(""#chkSendFileWithOwner"").click();
            $(""#chkSendFileWithOwner"").prop('disabled', true);");
                    }

                    NewsDataScriptInit = string.Format(@"
            $(""#iptTitle"").val('{0}');
            $(""#sltMessageType"").selectpicker('val', '{1}').prop('disabled', true).selectpicker('refresh');
            $(""#sltSendType"").selectpicker('val', '{2}').prop('disabled', true).selectpicker('refresh');
            $(""#iptDate"").val('{3}').prop('disabled', true);
            $(""#iptTime"").val('{4}').prop('disabled', true);
            $(""#sltAcceptType"").selectpicker('val', '{5}').prop('disabled', true).selectpicker('refresh');
            $(""#iptGroupType{6}"").click();
            $(""#iptGroupType1"").prop('disabled', true);
            $(""#iptGroupType2"").prop('disabled', true);
            if ($(""input[name=iptGroupType]:checked"").val() == '1') {{
                // GroupType = 1 
                $(""#sltGroup"").selectpicker('val', '{7}').prop('disabled', true).selectpicker('refresh');
                if ($('option:selected', $(""#sltGroup"")).attr('data-default') == 'all-student-level') {{
                    // For all-student-level
                    {8}
                }}
            }}
            else {{
                // GroupType = 2
                {9}
            }}
            {10}
            $(""#tarMessage"").val(`{11}`).keyup();
", smsObj.SMSTitle, smsObj.SMSDuration, smsObj.SMSType, smsObj.dSend?.ToString("dd /MM/yyyy", new CultureInfo("th-TH")), smsObj.dSend?.ToString("HH:mm", new CultureInfo("th-TH"))
, smsObj.nActionType, smsObj.SMSGroupType, smsObj.SMSGroupID, treeSMSSubLevel, individualType, sendFileWithOwner, smsObj.SMSDesp);
                }


                //                 query = string.Format(@"
                //SELECT * FROM [TMessageBox] WHERE SchoolID = {0} AND push_id = {1}
                //UNION
                //SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox] WHERE SchoolID = {0} AND push_id = {1}
                //UNION
                //SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox.old] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);
                //                TMessageBox messageBoxObj = en.Database.SqlQuery<TMessageBox>(query).FirstOrDefault();
                // User list
                query = string.Format(@"
SELECT * FROM [TMessageBox] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);

                TMessageBox messageBoxObj = en.Database.SqlQuery<TMessageBox>(query).FirstOrDefault();

                if (messageBoxObj == null)
                {
                    query = string.Format(@"SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);
                    messageBoxObj = en.Database.SqlQuery<TMessageBox>(query).FirstOrDefault();
                }

                if (messageBoxObj == null)
                {
                    query = string.Format(@"SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox.old] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);
                    messageBoxObj = en.Database.SqlQuery<TMessageBox>(query).FirstOrDefault();
                }


                if (messageBoxObj != null)
                {
                    query = string.Format(@"
SELECT *
FROM
(
	SELECT *
	FROM
	(
		SELECT CASE WHEN user_type = 0 THEN uv.SubLevel+'/'+uv.nTSubLevel2 ELSE '-' END 'Classroom'
		, CASE WHEN user_type = 0 THEN uv.sStudentID ELSE ei.Code END 'Code'
		, CASE WHEN user_type = 0 THEN uv.titleDescription+uv.sName+' '+uv.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
		, CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
		, mu.UserID, mu.user_type 'TypeID'
		, CASE WHEN user_type = 0 THEN ROW_NUMBER() OVER (PARTITION BY sID ORDER BY numberYear DESC, sTerm DESC) ELSE 1 END 'IDX'
		FROM [TMessage_User] mu 
		LEFT JOIN [TMessageBox] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
		LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
		LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
		LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
		LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
		LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
		LEFT JOIN TB_StudentViews uv ON mu.SchoolID = uv.SchoolID AND mu.UserID = uv.sID 
		WHERE mu.SchoolID = {0} AND mu.message_id = {1}
	) A
	WHERE IDX=1
	UNION
	SELECT *
	FROM
	(
		SELECT CASE WHEN user_type = 0 THEN uv.SubLevel+'/'+uv.nTSubLevel2 ELSE '-' END 'Classroom'
		, CASE WHEN user_type = 0 THEN uv.sStudentID ELSE ei.Code END 'Code'
		, CASE WHEN user_type = 0 THEN uv.titleDescription+uv.sName+' '+uv.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
		, CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
		, mu.UserID, mu.user_type 'TypeID'
		, CASE WHEN user_type = 0 THEN ROW_NUMBER() OVER (PARTITION BY sID ORDER BY numberYear DESC, sTerm DESC) ELSE 1 END 'IDX'
		FROM [JabjaiSchoolHistory].[dbo].[TMessage_User] mu 
		LEFT JOIN [JabjaiSchoolHistory].[dbo].[TMessageBox] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
		LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
		LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
		LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
		LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
		LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
		LEFT JOIN TB_StudentViews uv ON mu.SchoolID = uv.SchoolID AND mu.UserID = uv.sID 
		WHERE mu.SchoolID = {0} AND mu.message_id = {1}
	) A
	WHERE IDX=1
    UNION
	SELECT *
	FROM
	(
		SELECT CASE WHEN user_type = 0 THEN uv.SubLevel+'/'+uv.nTSubLevel2 ELSE '-' END 'Classroom'
		, CASE WHEN user_type = 0 THEN uv.sStudentID ELSE ei.Code END 'Code'
		, CASE WHEN user_type = 0 THEN uv.titleDescription+uv.sName+' '+uv.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
		, CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
		, mu.UserID, mu.user_type 'TypeID'
		, CASE WHEN user_type = 0 THEN ROW_NUMBER() OVER (PARTITION BY sID ORDER BY numberYear DESC, sTerm DESC) ELSE 1 END 'IDX'
		FROM [JabjaiSchoolHistory].[dbo].[TMessage_User.old] mu 
		LEFT JOIN [JabjaiSchoolHistory].[dbo].[TMessageBox.old] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
		LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
		LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
		LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
		LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
		LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
		LEFT JOIN TB_StudentViews uv ON mu.SchoolID = uv.SchoolID AND mu.UserID = uv.sID 
		WHERE mu.SchoolID = {0} AND mu.message_id = {1}
	) A
	WHERE IDX=1
) A
ORDER BY Code", schoolID, messageBoxObj.nMessageID);

                    string newsUserRows = "";
                    List<MessageUserData> messageUsers = en.Database.SqlQuery<MessageUserData>(query).ToList();
                    foreach (var mu in messageUsers)
                    {
                        newsUserRows += string.Format(@", {{""id"": {0}, ""code"": ""{1}"", ""name"": ""{2}"", ""classroom"": ""{3}"", ""typeId"": {4}, ""type"": ""{5}"", ""indb"": true, ""status"": ""modify""}}", mu.UserID, mu.Code, mu.Name, mu.Classroom, mu.TypeID, mu.Type);
                    }

                    if (!string.IsNullOrEmpty(newsUserRows)) NewsUserData = newsUserRows.Remove(0, 2);
                }

                // File list
                var newsFileList = en.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == smsID && w.cDel == false).ToList();
                if (newsFileList.Count > 0)
                {
                    string newsFileRows = "";
                    foreach (var nf in newsFileList)
                    {
                        string fileName = "";
                        string contentType = "";
                        long fileSize = 0;
                        switch (nf.ContentType)
                        {
                            case "application/pdf":
                                contentType = "/images/FileTypes/pdf.png";
                                break;
                            case "application/msword": // WORD 2003
                            case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": // WORD 2007
                                contentType = "/images/FileTypes/doc.png";
                                break;
                            case "application/vnd.ms-excel": // EXECL 2003
                            case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": // EXECL 2007
                                contentType = "/images/FileTypes/xls.png";
                                break;
                            case "application/vnd.ms-powerpoint": // POWERPOINT 2003
                            case "application/vnd.openxmlformats-officedocument.presentationml.presentation": // POWERPOINT 2007
                                contentType = "/images/FileTypes/ppt.png";
                                break;
                            case "image/png":
                            case "image/jpeg":
                            case "image/jpg":
                                contentType = nf.sFileName;
                                break;
                            default:
                                contentType = "/images/FileTypes/file.png";
                                break;
                        }

                        try
                        {
                            Uri uri = new Uri(nf.sFileName);
                            fileName = System.IO.Path.GetFileName(uri.LocalPath);
                            fileSize = GetFileSize(uri);
                        }
                        catch { }

                        newsFileRows += string.Format(@", {{""id"": {0}, ""contentType"": ""{1}"", ""fileName"": ""{2}"", ""indb"": true, ""status"": ""modify"", ""byteData"": """", ""size"": {3}, ""thumbnail"": ""{4}""}}", nf.nNewsFileID, nf.ContentType, fileName, fileSize, contentType);
                    }

                    if (!string.IsNullOrEmpty(newsFileRows)) NewsFileData = newsFileRows.Remove(0, 2);
                }
            }
        }

        [WebMethod(EnableSession = true)]
        public static List<EntityDropdown> LoadTermSubLevel2(string subLevelID)
        {
            List<EntityDropdown> result = null;

            if (!string.IsNullOrEmpty(subLevelID))
            {
                int schoolID = GetUserData().CompanyID;
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
                {
                    int slID = Convert.ToInt32(subLevelID);

                    string query = string.Format(@"
SELECT r.id, r.name
FROM
(
	SELECT CAST(t.nTermSubLevel2 AS VARCHAR(10)) 'id', s.SubLevel + ' / ' + t.nTSubLevel2 'name', s.SubLevel 'sort1', (CASE WHEN ISNUMERIC(t.nTSubLevel2) = 1 THEN RIGHT('0000' + t.nTSubLevel2, 5) ELSE t.nTSubLevel2 END) 'sort2'
	FROM TTermSubLevel2 t 
	LEFT JOIN TSubLevel s ON t.nTSubLevel = s.nTSubLevel 
	WHERE t.SchoolID = {0} AND t.nTSubLevel = {1} AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.sort1, r.sort2", schoolID, slID);
                    result = dbschool.Database.SqlQuery<EntityDropdown>(query).ToList();
                }
            }

            return result;
        }

        [WebMethod]
        public static object GetStudentName(string keyword, string individualType, string levelID, string roomID)
        {
            List<AutoCompleteResult> result = null;

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                // Get current year
                string terms = "";
                StudentLogic studentLogic = new StudentLogic(dbSchool);

                var termObj1 = studentLogic.GetTermDATA(DateTime.Today, userData);
                if (termObj1 != null)
                {
                    terms += string.Format(@", '{0}'", termObj1.nTerm);
                }

                var termObj2 = dbSchool.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termObj1.dStart && f.SchoolID == schoolID && f.cDel == null);
                if (termObj2 != null)
                {
                    terms += string.Format(@", '{0}'", termObj2.nTerm);
                }

                if (!string.IsNullOrEmpty(terms))
                {
                    terms = string.Format(@" AND nTerm IN ({0})", terms.Remove(0, 2));
                }

                string sqlQuery = "";

                switch (individualType)
                {
                    case "0": // student

                        string condition = "";
                        if (!string.IsNullOrEmpty(levelID))
                        {
                            condition += string.Format(@" AND nTSubLevel = {0} ", levelID);
                        }
                        if (!string.IsNullOrEmpty(roomID))
                        {
                            condition += string.Format(@" AND nTermSubLevel2 = {0} ", roomID);
                        }

                        sqlQuery = string.Format(@"
SELECT TOP 20 sID 'ID', sStudentID 'Code', titleDescription+sName+' '+sLastname 'Name', SubLevel+' / '+nTSubLevel2 'Classroom', 0 'TypeID', 'นักเรียน' 'Type'
FROM TB_StudentViews
WHERE cDel IS NULL AND ISNULL(nStudentStatus, 0) = 0 AND SchoolID = {0} AND (sName <> '' OR sLastname <> '') 
AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%') {2} {3}
GROUP BY sID, sStudentID, titleDescription, sName, sLastname, SubLevel, nTSubLevel2
ORDER BY SubLevel, nTSubLevel2, sStudentID, titleDescription, sName, sLastname ", schoolID, keyword, terms, condition);

                        break;
                    case "1": // employee

                        sqlQuery = string.Format(@"
SELECT TOP 20 e.sEmp 'ID', i.Code, t.titleDescription+e.sName+' '+e.sLastname 'Name', '-' 'Classroom', 1 'TypeID', 'บุคลากร' 'Type'
FROM TEmployees e
LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10)) AND e.SchoolID = t.SchoolID
WHERE e.cDel IS NULL AND e.SchoolID = {0} AND (e.sName <> '' OR e.sLastname <> '') AND (e.sName LIKE N'%{1}%' OR e.sLastname LIKE N'%{1}%' OR i.Code LIKE N'%{1}%')
ORDER BY i.Code, t.titleDescription, e.sName, e.sLastname", schoolID, keyword);

                        break;
                }


                result = dbSchool.Database.SqlQuery<AutoCompleteResult>(sqlQuery).ToList();
            }

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object SaveEditNews(NewsData newsData)
        {
            bool success = true;
            string code = "200";
            string message = "Success to save & send news data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;


            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                

                try
                {
                    var smsObj = en.TSMS.Where(w => w.SchoolID == schoolID && w.nSMS == newsData.SMSID).FirstOrDefault();
                    if (smsObj != null)
                    {
                        smsObj.SMSTitle = newsData.Title;
                        smsObj.SMSDesp = newsData.Message;

                        // Manage file
                        if (newsData.Files.Count() > 0)
                        {
                            foreach (var f in newsData.Files)
                            {
                                if (!f.InDB && f.Status == "new")
                                {
                                    // New
                                    string urlFile = AzureStorage.UploadFileFromByteData(Guid.NewGuid().ToString() + Path.GetExtension(f.FileName), f.ByteData, "news", schoolID, smsObj.nSMS);
                                    en.TNewsFiles.Add(new TNewsFile
                                    {
                                        ContentType = f.ContentType,
                                        nSMS = smsObj.nSMS,
                                        sFileName = urlFile,
                                        FullFileName = f.FileName,
                                        StudentCodeOwner = newsData.SendFileWithOwner,
                                        StudentCodeFileName = (newsData.SendFileWithOwner ? Path.GetFileNameWithoutExtension(f.FileName).Trim() : null),
                                        SchoolID = schoolID,
                                        CreatedBy = userData.UserID,
                                        CreatedDate = DateTime.Now
                                    });
                                }
                                else if (f.InDB && f.Status == "delete")
                                {
                                    // Delete file in db
                                    var newsFileObj = en.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nNewsFileID == f.ID).FirstOrDefault();
                                    if (newsFileObj != null)
                                    {
                                        newsFileObj.cDel = true;
                                        newsFileObj.UpdatedBy = userData.UserID;
                                        newsFileObj.UpdatedDate = DateTime.Now;
                                    }
                                }
                            }
                        }

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "แก้ไขข่าวสาร " + smsObj.SMSTitle, userData.Entities, null, 2, 2, 0);
                    }
                    else
                    {
                        success = false;
                        message = "Data not found.";
                    }
                }
                catch (Exception err)
                {
                    success = false;
                    message = err.Message;

                }
            }

            return JsonConvert.SerializeObject(new { success, code, message });
        }

        long GetFileSize(Uri uriPath)
        {
            var webRequest = System.Net.HttpWebRequest.Create(uriPath);
            webRequest.Method = "HEAD";

            using (var webResponse = webRequest.GetResponse())
            {
                var fileSize = webResponse.Headers.Get("Content-Length");
                return Convert.ToInt64(fileSize);
            }
        }

        public class AutoCompleteResult
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }

            [JsonProperty(PropertyName = "typeId")]
            public int TypeID { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }
        }

        public class NewsData
        {
            [JsonProperty(PropertyName = "smsId")]
            public int SMSID { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string Title { get; set; }

            [JsonProperty(PropertyName = "messageType")]
            public int MessageType { get; set; }

            [JsonProperty(PropertyName = "sendType")]
            public int SendType { get; set; }

            [JsonProperty(PropertyName = "sendDate")]
            public string SendDate { get; set; }

            public DateTime dSendDate { get; set; }

            [JsonProperty(PropertyName = "acceptType")]
            public int AcceptType { get; set; }

            [JsonProperty(PropertyName = "groupType")]
            public int GroupType { get; set; }

            [JsonProperty(PropertyName = "group")]
            public int? Group { get; set; }

            [JsonProperty(PropertyName = "groupDefault")]
            public string GroupDefault { get; set; }

            [JsonProperty(PropertyName = "groupLevel")]
            public int[] GroupLevel { get; set; }

            [JsonProperty(PropertyName = "users")]
            public List<UserMessageBoxData> Users { get; set; }

            [JsonProperty(PropertyName = "sendFileWithOwner")]
            public bool SendFileWithOwner { get; set; }

            [JsonProperty(PropertyName = "files")]
            public List<FileData> Files { get; set; }

            [JsonProperty(PropertyName = "message")]
            public string Message { get; set; }
        }

        public class UserMessageBoxData
        {
            [JsonProperty(PropertyName = "sId")]
            public int sID { get; set; }

            [JsonProperty(PropertyName = "typeId")]
            public int TypeID { get; set; }
        }

        public class FileData
        {
            [JsonProperty(PropertyName = "id")]
            public int ID { get; set; }

            [JsonProperty(PropertyName = "contentType")]
            public string ContentType { get; set; }

            [JsonProperty(PropertyName = "fileName")]
            public string FileName { get; set; }

            [JsonProperty(PropertyName = "byteData")]
            public string ByteData { get; set; }

            [JsonProperty(PropertyName = "indb")]
            public bool InDB { get; set; }

            [JsonProperty(PropertyName = "status")]
            public string Status { get; set; }
        }

        public class NewsGroup
        {
            [JsonProperty(PropertyName = "smsGroupId")]
            public int SMSGroupID { get; set; }

            [JsonProperty(PropertyName = "smsGroupName")]
            public string SMSGroupName { get; set; }

            [JsonProperty(PropertyName = "groupDefault")]
            public string GroupDefault { get; set; }
        }

        public class LevelData
        {
            [JsonProperty(PropertyName = "levelId")]
            public int LevelID { get; set; }

            [JsonProperty(PropertyName = "levelName")]
            public string LevelName { get; set; }
        }

        public class ClassroomData
        {
            [JsonProperty(PropertyName = "classroomId")]
            public int ClassroomID { get; set; }

            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }

            [JsonProperty(PropertyName = "levelId")]
            public int LevelID { get; set; }

            [JsonProperty(PropertyName = "levelName")]
            public string LevelName { get; set; }
        }

        public class MessageUserData
        {
            [JsonProperty(PropertyName = "userId")]
            public int UserID { get; set; }

            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "typeId")]
            public int TypeID { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }

            [JsonProperty(PropertyName = "readStatus")]
            public string ReadStatus { get; set; }

            [JsonProperty(PropertyName = "messageReply")]
            public string MessageReply { get; set; }
        }

    }
}