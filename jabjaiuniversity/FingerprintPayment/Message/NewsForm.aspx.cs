using FingerprintPayment.Message.CsCode;
using iTextSharp.text;
using iTextSharp.text.pdf;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Services;
using System.Web.UI.WebControls;
using urbanairship;

namespace FingerprintPayment.Message
{
    public partial class NewsForm : MessageGateway
    {
        protected string EnableSendLINE = "disabled";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Execute once
            InitialControl();
        }

        private void InitialControl()
        {
            int schoolID = UserData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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
	WHERE t.SchoolID = {0} AND s.nWorkingStatus = 1 AND t.nTermSubLevel2Status = '1' AND t.nWorkingStatus = 1
) r
ORDER BY r.LevelID, r.sort2", schoolID);
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

            }

            using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
            {
                TCompany schoolObj = mctx.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                if (schoolObj != null)
                {
                    if (schoolObj.IsActiveSendMessageToLINE ?? false == true)
                    {
                        EnableSendLINE = "";
                    }
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
                using (JabJaiEntities dbschool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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
            using (JabJaiEntities dbSchool = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
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
WITH TAddedRowNumber AS
(
	SELECT ROW_NUMBER() OVER(PARTITION BY sID ORDER BY numberYear DESC) 'RowNumber', *
	FROM TB_StudentViews
	WHERE cDel IS NULL AND ISNULL(nStudentStatus, 0) = 0 AND SchoolID = {0} AND (sName <> '' OR sLastname <> '')
	AND (sName LIKE N'%{1}%' OR sLastname LIKE N'%{1}%' OR sStudentID LIKE N'%{1}%') {2} {3} 
)
SELECT TOP 20 sID 'ID', sStudentID 'Code', titleDescription+sName+' '+sLastname 'Name', SubLevel+' / '+nTSubLevel2 'Classroom', 0 'TypeID', 'นักเรียน' 'Type'
FROM TAddedRowNumber
WHERE RowNumber = 1
ORDER BY SubLevel, nTSubLevel2, sStudentID, titleDescription, sName, sLastname", schoolID, keyword, terms, condition);

                        break;
                    case "1": // employee

                        sqlQuery = string.Format(@"
SELECT TOP 20 e.sEmp 'ID', i.Code, t.titleDescription+e.sName+' '+e.sLastname 'Name', '-' 'Classroom', 1 'TypeID', 'บุคลากร' 'Type'
FROM TEmployees e
LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
LEFT JOIN TEmpSalary s ON e.sEmp = s.sEmp AND e.SchoolID = s.SchoolID
LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10)) AND e.SchoolID = t.SchoolID
WHERE e.cDel IS NULL AND ISNULL(s.WorkStatus, 1)=1 AND e.SchoolID = {0} AND (e.sName <> '' OR e.sLastname <> '') AND (e.sName LIKE N'%{1}%' OR e.sLastname LIKE N'%{1}%' OR i.Code LIKE N'%{1}%')
ORDER BY i.Code, t.titleDescription, e.sName, e.sLastname", schoolID, keyword);

                        break;
                }


                result = dbSchool.Database.SqlQuery<AutoCompleteResult>(sqlQuery).ToList();
            }

            return JsonConvert.SerializeObject(result);
        }

        [WebMethod(EnableSession = true)]
        public static object SaveAndSendNews(NewsData newsData)
        {
            bool success = true;
            string code = "200";
            string message = "Success to save & send news data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;
            string schoolName = "";

            // Check file name length
            if (newsData.SendFileWithOwnerOCR)
            {
                string fileProblem = "";
                string fileExtensionProblem = "";

                if (newsData.Files.Count() > 0)
                {
                    var pattern = @"(.*?)(?:\((\d+)\))?(.pdf)$";

                    foreach (var f in newsData.Files)
                    {
                        if (!Regex.IsMatch(f.FileName, pattern))
                        {
                            fileProblem += ", " + f.FileName.Trim();
                            fileExtensionProblem += ", " + f.ContentType;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(fileProblem))
                {
                    fileProblem = fileProblem.Remove(0, 2);
                    fileExtensionProblem = fileExtensionProblem.Remove(0, 2);

                    success = false;
                    code = "500";
                    message = "ไม่สามารถอัพโหลดไฟล์นามสกุล \"" + fileExtensionProblem + "\" นี่ได้! <br />[ไฟล์: " + fileProblem + "]";

                    return JsonConvert.SerializeObject(new { success, code, message });
                }
            }
            else if (newsData.SendFileWithOwner)
            {
                string fileProblem = "";

                if (newsData.Files.Count() > 0)
                {
                    foreach (var f in newsData.Files)
                    {
                        if (f.FileName.Trim().Length > 20) fileProblem += ", " + f.FileName.Trim();
                    }
                }

                if (!string.IsNullOrEmpty(fileProblem))
                {
                    fileProblem = fileProblem.Remove(0, 2);

                    success = false;
                    code = "500";
                    message = "ไม่สามารถอัพโหลดไฟล์ที่มีชื่อไฟล์ยาวกว่า 20 ตัวอักษรหรือชื่อไฟล์ไม่ใช่รหัสบุคลากรหรือรหัสนักเรียนได้! (กรณีส่งข่าวสารแบบระบุขอบเขตผู้รับตามชื่อไฟล์) <br />[ไฟล์: " + fileProblem + "]";

                    return JsonConvert.SerializeObject(new { success, code, message });
                }
            }

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                var companyObj = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();
                if (companyObj != null)
                {
                    schoolName = companyObj.sCompany;
                }

              
                    try
                    {
                        if (!string.IsNullOrEmpty(newsData.SendDate))
                        {
                            newsData.dSendDate = DateTime.ParseExact(newsData.SendDate, "dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                        }

                        //TSMS
                        TSM sms = new TSM();
                        sms.SMSTitle = newsData.Title; // newsData.MessageType == 0 ? "แจ้งประกาศกิจกรรม" : "แจ้งประกาศข่าวสาร";
                        sms.SMSDuration = newsData.MessageType;
                        sms.SMSDesp = newsData.Message;
                        sms.SMSType = newsData.SendType;
                        switch (newsData.SendType)
                        {
                            case 0:
                                sms.SMSSubType = 0; // 0: สแกนเข้าโรงเรียน, 1: สแกนออกโรงเรียน, 2: เข้าโรงเรียนสาย, 3: ขาดเรียน
                                sms.SMSDate = DateTime.Now;
                                sms.dSend = DateTime.Now;
                                break;
                            case 1:
                                sms.SMSSubType = -1;
                                sms.SMSDate = newsData.dSendDate;
                                sms.dSend = newsData.dSendDate;
                                break;
                        }
                        sms.nActionType = newsData.AcceptType;
                        sms.SMSStatus = "-";
                        sms.useradd = userData.UserID;

                        bool studentAllOrAll = newsData.GroupDefault == "all" || newsData.GroupDefault == "all-student";
                        //sms.SMSAll = studentAllOrAll ? "1" : "0";

                        bool employeeAllOrAll = newsData.GroupDefault == "all" || newsData.GroupDefault == "all-employee";
                        //sms.SMSEMP = employeeAllOrAll ? "1" : "0";

                        sms.SMSEMP = "0";
                        switch (newsData.GroupDefault)
                        {
                            case "all":
                                sms.SMSAll = "0";
                                sms.SMSEMP = "1";
                                break;
                            case "all-employee":
                                sms.SMSAll = "1";
                                sms.SMSEMP = "1";
                                break;
                            case "all-student":
                                sms.SMSAll = "2";
                                break;
                            case "all-student-level":
                                sms.SMSAll = "4";
                                break;
                            default:
                                sms.SMSAll = "3";
                                break;
                        }

                        sms.SMSGroupType = newsData.GroupType;
                        sms.SMSGroupID = newsData.Group;

                        sms.CreatedBy = userData.UserID;
                        sms.CreatedDate = DateTime.Now;

                        sms.SchoolID = schoolID;
                        en.TSMS.Add(sms);
                        en.SaveChanges();

                        int smsID = sms.nSMS;

                        //TNewsFile
                        List<string> UserCodeFromAttachFileName = new List<string>();
                        if (newsData.Files.Count() > 0)
                        {
                            if (newsData.SendFileWithOwnerOCR)
                            {
                                // Create temp file from byte data (~/images/news/[SchoolID]/temp/[UserID]/*.*)
                                // Create split temp file

                                // Check & Create folder
                                string tempPath = string.Format(@"~/images/news/{0}/temp/{1}/", schoolID, userData.UserID);
                                if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(tempPath)))
                                {
                                    Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(tempPath));
                                }
                                string tempFullPath = System.Web.HttpContext.Current.Server.MapPath(tempPath);

                                foreach (var f in newsData.Files)
                                {
                                    try
                                    {
                                        // Save file in temp folder.
                                        f.TempFilePath = tempFullPath + f.FileName;

                                        byte[] bytes = Convert.FromBase64String(f.ByteData);
                                        File.WriteAllBytes(f.TempFilePath, bytes);
                                    }
                                    catch
                                    {
                                        throw new Exception("[SEND_FILE_WITH_OWNER_OCR]: Error save temp file from bytes data.");
                                    }

                                    try
                                    {
                                        // Split PDF file
                                        using (PdfReader reader = new PdfReader(f.TempFilePath))
                                        {
                                            f.SplitTempFilePath = new List<SplitFileData>();

                                            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber++)
                                            {
                                                string splitTempFilePath = tempFullPath + Path.GetFileNameWithoutExtension(f.FileName) + "-" + pageNumber + Path.GetExtension(f.FileName);

                                                f.SplitTempFilePath.Add(new SplitFileData { FilePath = splitTempFilePath });

                                                Document document = new Document();
                                                PdfCopy copy = new PdfCopy(document, new FileStream(splitTempFilePath, FileMode.Create));

                                                document.Open();

                                                copy.AddPage(copy.GetImportedPage(reader, pageNumber));

                                                document.Close();
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        throw new Exception("[SEND_FILE_WITH_OWNER_OCR]: Error read & split PDF file.");
                                    }

                                }

                                try
                                {
                                    // Get Student Code from api ocr & Rename split temp file
                                    if (newsData.Files.Count() > 0)
                                    {
                                        var client = new RestClient($@"https://grade.schoolbright.co/api/RenameProject/RenameProjectFiles?schoolId={schoolID}&id={userData.UserID}");
                                        client.Timeout = -1;
                                        var request = new RestRequest(Method.POST);
                                        request.AlwaysMultipartFormData = true;

                                        foreach (var f in newsData.Files)
                                        {
                                            foreach (var tf in f.SplitTempFilePath)
                                            {
                                                request.AddFile("data", tf.FilePath);
                                            }
                                        }

                                        IRestResponse response = client.Execute(request);

                                        if (!string.IsNullOrEmpty(response.Content) && response.StatusCode == HttpStatusCode.OK)
                                        {
                                            List<RenameProjectFilesResponse> renameProjectFilesResponses = JsonConvert.DeserializeObject<List<RenameProjectFilesResponse>>(response.Content);

                                            foreach (var f in newsData.Files)
                                            {
                                                foreach (var tf in f.SplitTempFilePath)
                                                {
                                                    var renameProjectFilesResponse = renameProjectFilesResponses.Where(w => w.FileName == Path.GetFileName(tf.FilePath)).FirstOrDefault();
                                                    if (renameProjectFilesResponse != null)
                                                    {
                                                        string newFilename = tempFullPath + renameProjectFilesResponse.StudentCode + Path.GetExtension(tf.FilePath);
                                                        File.Move(tf.FilePath, newFilename);
                                                        tf.FilePath = newFilename;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    throw new Exception("[SEND_FILE_WITH_OWNER_OCR]: Error call RenameProjectFiles api & rename temp file.");
                                }

                                // Upload file to cloud & save data (news file)
                                try
                                {
                                    if (newsData.Files.Count() > 0)
                                    {
                                        foreach (var f in newsData.Files)
                                        {
                                            foreach (var tf in f.SplitTempFilePath)
                                            {
                                                string urlFile = AzureStorage.UploadFileFromFullFilePath(Path.GetFileName(tf.FilePath), tf.FilePath, "news", schoolID, smsID);
                                                if (!string.IsNullOrEmpty(urlFile))
                                                {
                                                    en.TNewsFiles.Add(new TNewsFile
                                                    {
                                                        ContentType = f.ContentType,
                                                        nSMS = smsID,
                                                        sFileName = urlFile,
                                                        FullFileName = Path.GetFileName(tf.FilePath),
                                                        StudentCodeOwner = newsData.SendFileWithOwner,
                                                        StudentCodeFileName = (newsData.SendFileWithOwner ? Path.GetFileNameWithoutExtension(tf.FilePath).Trim() : null),
                                                        SchoolID = schoolID,
                                                        CreatedBy = userData.UserID,
                                                        CreatedDate = DateTime.Now
                                                    });
                                                    en.SaveChanges();

                                                    tf.UploadComplete = true;
                                                }
                                                else
                                                {
                                                    tf.UploadComplete = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    throw new Exception("[SEND_FILE_WITH_OWNER_OCR]: Error upload file to cloud & save data (news file).");
                                }

                                // Remove temp file
                                foreach (var f in newsData.Files)
                                {
                                    if (File.Exists(f.TempFilePath)) File.Delete(f.TempFilePath);
                                }
                            }
                            else
                            {
                                foreach (var f in newsData.Files)
                                {
                                    string fileName = "";
                                    if (newsData.SendFileWithOwner)
                                    {
                                        fileName = f.FileName;
                                    }
                                    else
                                    {
                                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(f.FileName);
                                    }

                                    string urlFile = AzureStorage.UploadFileFromByteData(fileName, f.ByteData, "news", schoolID, smsID);
                                    if (!string.IsNullOrEmpty(urlFile))
                                    {
                                        en.TNewsFiles.Add(new TNewsFile
                                        {
                                            ContentType = f.ContentType,
                                            nSMS = smsID,
                                            sFileName = urlFile,
                                            FullFileName = f.FileName,
                                            StudentCodeOwner = newsData.SendFileWithOwner,
                                            StudentCodeFileName = (newsData.SendFileWithOwner ? Path.GetFileNameWithoutExtension(f.FileName).Trim() : null),
                                            SchoolID = schoolID,
                                            CreatedBy = userData.UserID,
                                            CreatedDate = DateTime.Now
                                        });
                                        en.SaveChanges();

                                        f.UploadComplete = true;
                                    }
                                    else
                                    {
                                        f.UploadComplete = false;
                                    }
                                }
                            }
                        }

                        if (newsData.SendFileWithOwnerOCR)
                        {
                            if (newsData.Files.Count() > 0)
                            {
                                foreach (var f in newsData.Files)
                                {
                                    var completeFileUpload = f.SplitTempFilePath.Where(w => w.UploadComplete == true).ToList();
                                    foreach (var tfc in completeFileUpload)
                                    {
                                        UserCodeFromAttachFileName.Add(Path.GetFileNameWithoutExtension(tfc.FilePath).Trim());
                                    }
                                }
                            }
                        }
                        else if (newsData.SendFileWithOwner)
                        {
                            if (newsData.Files.Count() > 0)
                            {
                                var completeFileUpload = newsData.Files.Where(w => w.UploadComplete == true).ToList();
                                foreach (var f in completeFileUpload)
                                {
                                    UserCodeFromAttachFileName.Add(Path.GetFileNameWithoutExtension(f.FileName).Trim());
                                }
                            }
                        }

                        // User MessageBox
                        int messageID = 0;
                        var userMessageBox = new List<messagebox.user_messagebox>();
                        switch (newsData.GroupType)
                        {
                            case 1: // รายกลุ่ม
                                if (studentAllOrAll)
                                {
                                    // นักเรียนทั้งหมด
                                    StudentLogic studentLogic = new StudentLogic(en);
                                    var termObj1 = studentLogic.GetTermDATA(DateTime.Today, userData);
                                    var termObj2 = en.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termObj1.dStart && f.SchoolID == schoolID && f.cDel == null);

                                    var userList = en.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termObj1.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();
                                    if (userList.Count == 0) userList = en.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termObj2.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();

                                    var userIDList = userList.Select(s => s.sID).ToList();

                                    //var userMasterList = dbMaster.TUsers.Where(w => w.nCompany == schoolID && userIDList.Contains(w.sID) && w.cType == "0" && w.cDel == null).ToList();
                                    var userSchoolList = en.TUser.Where(w => w.SchoolID == schoolID && userIDList.Contains(w.sID) && w.cDel == null).ToList();

                                    // Send only studentID exist in file name 
                                    if (newsData.SendFileWithOwner)
                                    {
                                        userSchoolList = userSchoolList.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.sStudentID)).ToList();
                                    }

                                    foreach (var u in userSchoolList)
                                    {
                                        int userType = int.Parse(u.cType);
                                        userMessageBox.Add(new messagebox.user_messagebox
                                        {
                                            user_id = u.sID,
                                            user_type = userType
                                        });
                                    }
                                }
                                if (employeeAllOrAll)
                                {
                                    // บุคลากรทั้งหมด
                                    var employeeList = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.cType == "1" && w.cDel == null).ToList();

                                    // Send only employeeID exist in file name 
                                    if (newsData.SendFileWithOwner)
                                    {
                                        var employeeAllInSchool = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList();
                                        var employeeIDInAttachFileName = employeeAllInSchool.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.Code)).Select(s => s.sEmp).ToList();
                                        employeeList = employeeList.Where(w => employeeIDInAttachFileName.Exists(e => e == w.sID)).ToList();
                                    }

                                    foreach (var e in employeeList)
                                    {
                                        int userType = int.Parse(e.cType);
                                        userMessageBox.Add(new messagebox.user_messagebox
                                        {
                                            user_id = e.sID,
                                            user_type = userType
                                        });
                                    }
                                }
                                if (newsData.GroupDefault == "all-student-level")
                                {
                                    // เฉพาะนักเรียนตามลำดับชั้น
                                    StudentLogic studentLogic = new StudentLogic(en);
                                    var termObj1 = studentLogic.GetTermDATA(DateTime.Today, userData);
                                    var termObj2 = en.TTerms.OrderByDescending(d => d.dStart).FirstOrDefault(f => f.dStart < termObj1.dStart && f.SchoolID == schoolID && f.cDel == null);

                                    var studentInTerm = en.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termObj1.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();
                                    if (studentInTerm.Count == 0)
                                    {
                                        studentInTerm = en.TB_StudentViews.Where(w => w.SchoolID == schoolID && w.nTerm == termObj2.nTerm && w.cDel == null && (w.nStudentStatus ?? 0) == 0).AsQueryable().ToList();
                                    }

                                    foreach (var gl in newsData.GroupLevel)
                                    {
                                        TSMSSubLevel smsSubLevel = new TSMSSubLevel();
                                        smsSubLevel.nSMS = smsID;
                                        smsSubLevel.nTSubLevel = gl;
                                        smsSubLevel.SchoolID = schoolID;
                                        en.TSMSSubLevels.Add(smsSubLevel);

                                        var studentInRoom = studentInTerm.Where(w => w.nTermSubLevel2 == gl && w.nTerm == termObj1.nTerm).ToList();
                                        if (studentInRoom.Count == 0)
                                        {
                                            studentInRoom = studentInTerm.Where(w => w.nTermSubLevel2 == gl && w.nTerm == termObj2.nTerm).ToList();
                                        }

                                        // Send only studentID exist in file name
                                        if (newsData.SendFileWithOwner)
                                        {
                                            studentInRoom = studentInRoom.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.sStudentID)).ToList();
                                        }

                                        foreach (var uir in studentInRoom)
                                        {
                                            userMessageBox.Add(new messagebox.user_messagebox
                                            {
                                                user_id = uir.sID,
                                                user_type = 0
                                            });
                                        }
                                    }
                                }
                                if (string.IsNullOrEmpty(newsData.GroupDefault))
                                {
                                    // เลือกกลุ่ม(อื่นๆ)
                                    var userIDList = en.TSMSGroupUser.Where(w => w.SchoolID == schoolID && w.SMSGroupID == newsData.Group && w.cDel == false).Select(s => s.UserID).ToList();
                                    var userMasterList = dbMaster.TUsers.Where(w => w.nCompany == schoolID && userIDList.Contains(w.sID) && w.cDel == null).ToList();

                                    // Send only studentID exist in file name 
                                    // Send only employeeID exist in file name
                                    if (newsData.SendFileWithOwner)
                                    {
                                        var employeeAllInSchool = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList();
                                        var employeeIDInAttachFileName = employeeAllInSchool.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.Code)).Select(s => s.sEmp).ToList();
                                        //userMasterList = userMasterList.Where(w => employeeIDInAttachFileName.Exists(e => e == w.sID)).ToList();

                                        var studentAllInSchool = en.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                                        var studentIDInAttachFileName = studentAllInSchool.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.sStudentID)).Select(s => s.sID).ToList();

                                        userMasterList = userMasterList.Where(w => employeeIDInAttachFileName.Exists(e => e == w.sID) || studentIDInAttachFileName.Exists(e => e == w.sID)).ToList();
                                    }

                                    foreach (var u in userMasterList)
                                    {
                                        int userType = int.Parse(u.cType);
                                        userMessageBox.Add(new messagebox.user_messagebox
                                        {
                                            user_id = u.sID,
                                            user_type = userType
                                        });
                                    }
                                }
                                break;
                            case 2: // รายบุคคล

                                // Send only studentID exist in file name 
                                // Send only employeeID exist in file name
                                if (newsData.SendFileWithOwner)
                                {
                                    var employeeAllInSchool = en.TEmployeeInfoes.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList();
                                    var employeeIDInAttachFileName = employeeAllInSchool.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.Code)).Select(s => s.sEmp).ToList();
                                    var employeeNewsDataUsers = newsData.Users.Where(w => employeeIDInAttachFileName.Exists(e => e == w.sID)).ToList();

                                    var studentAllInSchool = en.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                                    var studentIDInAttachFileName = studentAllInSchool.Where(w => UserCodeFromAttachFileName.Exists(e => e == w.sStudentID)).Select(s => s.sID).ToList();
                                    var studentNewsDataUsers = newsData.Users.Where(w => studentIDInAttachFileName.Exists(e => e == w.sID)).ToList();

                                    newsData.Users.Clear();
                                    newsData.Users.AddRange(employeeNewsDataUsers);
                                    newsData.Users.AddRange(studentNewsDataUsers);
                                }

                                foreach (var u in newsData.Users)
                                {
                                    userMessageBox.Add(new messagebox.user_messagebox
                                    {
                                        user_id = u.sID,
                                        user_type = u.TypeID
                                    });
                                }
                                break;
                        }

                        string listUserIDComma = string.Format(@"""{0}""", string.Join(@""", """, userMessageBox.Select(s => s.user_id)));

                        messageID = messagebox.insert_message(
                          new messagebox.MessageBox
                          {
                              message_type = 5,
                              message_type_id = smsID,
                              school_id = schoolID,
                              user_messagebox = userMessageBox,
                              send_time = sms.dSend
                          });

                        // Check messageID eq -1 (Error)
                        if (messageID == -1)
                        {
                            throw new Exception("An error occurred in function messagebox.insert_message.");
                        }

                        Double differentTime = (DateTime.Now - DateTime.UtcNow).TotalMinutes;
                        string scheduledID = "";
                        if (sms.SMSType == 1)
                        {
                            // 1 = ตั้งเวลาการส่ง
                            //scheduledID = pushdata.scheduledNotAsync("[" + listUserIDComma + "]", sms.SMSDesp, sms.SMSTitle + " : " + schoolName, sms.dSend.Value.AddMinutes(-differentTime), messageID, schoolID);
                            //if (!string.IsNullOrEmpty(scheduledID))
                            //{
                            //    scheduledID = scheduledID.Split('"')[3];
                            //}

                            FMCServicesLogic fMCServices = new FMCServicesLogic(dbMaster);
                            List<string> user_Token = new List<string>();

                            FMCServicesLogic.FirebaseCloudMessaging firebaseCloudMessagings = new FMCServicesLogic.FirebaseCloudMessaging
                            {
                                registration_ids = user_Token,

                                push_mid = messageID,
                                push_mtype = messagebox.News,
                                push_schoolid = schoolID,

                                notification = new FMCServicesLogic.FirebaseCloudMessaging.Notification
                                {
                                    body = sms.SMSDesp,
                                    title = (newsData.MessageType == 0 ? "แจ้งประกาศกิจกรรม" : "แจ้งประกาศข่าวสาร"),

                                    smsId = smsID,
                                    actionType = sms.nActionType,
                                    acceptTypeText = newsData.AcceptTypeText
                                }
                            };

                            string webJobsURL = ConfigurationManager.AppSettings["webJobs"].ToString() + "/api/webjobs/notification";
                            firebaseCloudMessagings.scheduled_time = sms.dSend;
                            firebaseCloudMessagings.registration_ids = new List<string>();
                            HostingEnvironment.QueueBackgroundWorkItem(ct => fMCServices.SendSchedulerFirebaseCloudMessaging(webJobsURL, firebaseCloudMessagings));

                        }
                        else
                        {
                            // 0 = ส่งทันที
                            //scheduledID = pushdata.pushNotAsync("[" + listUserIDComma + "]", sms.SMSDesp, sms.SMSTitle + " : " + schoolName, sms.dSend.Value.AddMinutes(-differentTime), messageID, schoolID);
                            //if (!string.IsNullOrEmpty(scheduledID))
                            //{
                            //    scheduledID = scheduledID.Split('"')[3];
                            //}

                            //
                            FMCServicesLogic fMCServices = new FMCServicesLogic(dbMaster);
                            var q_Token = fMCServices.getTokenFormUserID(userMessageBox.Select(s => s.user_id).ToList(), schoolID);

                            if (q_Token.Count() > 0)
                            {
                                List<string> user_Token = new List<string>();

                                foreach (var s in q_Token)
                                {
                                    user_Token.AddRange(s.Token);
                                }

                                FMCServicesLogic.FirebaseCloudMessaging firebaseCloudMessagings = new FMCServicesLogic.FirebaseCloudMessaging
                                {
                                    registration_ids = user_Token,

                                    push_mid = messageID,
                                    push_mtype = messagebox.News,
                                    push_schoolid = schoolID,

                                    notification = new FMCServicesLogic.FirebaseCloudMessaging.Notification
                                    {

                                        body = sms.SMSDesp,
                                        title = (newsData.MessageType == 0 ? "แจ้งประกาศกิจกรรม" : "แจ้งประกาศข่าวสาร"),
                                    }
                                };

                                HostingEnvironment.QueueBackgroundWorkItem(ct => fMCServices.SendFirebaseCloudMessaging(firebaseCloudMessagings));
                            }
                        }



                        sms.scheduled_id = scheduledID;

                        en.SaveChanges();

                        database.InsertLog(userData.UserID.ToString(), "ส่งข่าวสาร " + sms.SMSTitle, userData.Entities, null, 2, 2, 0);


                        #region LINE MESSENGER

                        if (newsData.SendToLINE && sms.SMSType == 0) // sms.SMSType == 0 = ส่งทันที
                        {
                            new Thread(() =>
                            {
                                try
                                {
                                    using (JabJaiMasterEntities mctx = Connection.MasterEntities(ConnectionDB.Read))
                                    using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
                                    {
                                        // Init data
                                        var masterUsers = mctx.TUsers.Where(w => w.nCompany == schoolID && w.cDel == null).ToList();
                                        var employees = ctx.TEmployees.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                                        var students = ctx.TUser.Where(w => w.SchoolID == schoolID && w.cDel == null).ToList();
                                        var titles = ctx.TTitleLists.Where(w => w.SchoolID == schoolID && w.deleted != "1" && w.workStatus == "working").ToList();

                                        // LINE Push Message
                                        List<LINEUser> allUserLINE = mctx.LINEUsers.Where(w => w.SchoolID == schoolID && w.Status == "Active").ToList();

                                        var listFiles = ctx.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == smsID).ToList(); // smsID
                                        List<MappingLINEUser> listMappingLINEUser = new List<MappingLINEUser>();
                                        foreach (var user in userMessageBox) // userMessageBox - user_messagebox
                                        {
                                            var userLINEs = allUserLINE.Where(w => w.StudentID == user.user_id).ToList();
                                            foreach (var LINE in userLINEs)
                                            {
                                                var userName = "";
                                                var masterUserObj = masterUsers.Where(w => w.sID == user.user_id).FirstOrDefault();
                                                if (masterUserObj != null)
                                                {
                                                    switch (masterUserObj.cType)
                                                    {
                                                        case "0": //student
                                                            var studentObj = students.Where(w => w.sID == user.user_id).FirstOrDefault();
                                                            if (studentObj != null)
                                                            {
                                                                string titleName = "";
                                                                int.TryParse(studentObj.sStudentTitle, out int titleId);
                                                                var titleObj = titles.FirstOrDefault(w => w.nTitleid == titleId);
                                                                if (titleObj != null)
                                                                {
                                                                    titleName = titleObj.titleDescription;
                                                                }
                                                                else
                                                                {
                                                                    titleName = studentObj.sStudentTitle;
                                                                }
                                                                userName = titleName + " " + studentObj.sName + " " + studentObj.sLastname;
                                                            }
                                                            break;
                                                        case "1": //employee
                                                            var employeeObj = employees.Where(w => w.sEmp == user.user_id).FirstOrDefault();
                                                            if (employeeObj != null)
                                                            {
                                                                string titleName = "";
                                                                int.TryParse(employeeObj.sTitle, out int titleId);
                                                                var titleObj = titles.FirstOrDefault(w => w.nTitleid == titleId);
                                                                if (titleObj != null)
                                                                {
                                                                    titleName = titleObj.titleDescription;
                                                                }
                                                                else
                                                                {
                                                                    titleName = employeeObj.sTitle;
                                                                }
                                                                userName = titleName + " " + employeeObj.sName + " " + employeeObj.sLastname;
                                                            }
                                                            break;
                                                    }
                                                }

                                                listMappingLINEUser.Add(new MappingLINEUser { userID = user.user_id, userName = userName, lineID = LINE.LINEUserID });
                                            }
                                        }

                                        //// Chunk mapping LINE user list
                                        //int maxGroupAmount = 150;
                                        //var chunks = GetChunks(listMappingLINEUser, maxGroupAmount);
                                        //int groupID = 1;
                                        //foreach (var chunk in chunks)
                                        //{
                                        //    string LINEUserIDs = "";
                                        //    string studentIDs = "";
                                        //    foreach (var user in chunk)
                                        //    {
                                        //        LINEUserIDs += string.Format(@", ""{0}""", user.lineID);
                                        //        studentIDs += string.Format(@",{0}", user.userID);
                                        //    }

                                        //    if (!string.IsNullOrEmpty(LINEUserIDs))
                                        //    {
                                        //        LINEUserIDs = LINEUserIDs.Remove(0, 2);
                                        //        studentIDs += ",";

                                        //        ctx.TMessageLINEMulticasts.Add(new TMessageLINEMulticast
                                        //        {
                                        //            MessageID = messageID,
                                        //            GroupID = groupID,
                                        //            StreamID = studentIDs,
                                        //            UpdateDate = DateTime.Now,
                                        //            SchoolID = schoolID
                                        //        });
                                        //        ctx.SaveChanges();

                                        //        LINESendMulticastMessage(messageID, sms.SMSDesp, schoolID, groupID, sms.nActionType, newsData.AcceptTypeText, listFiles, LINEUserIDs);

                                        //        groupID++;
                                        //    }
                                        //}

                                        // Chunk mapping LINE user list
                                        string lineErrorMessage = "";
                                        int maxGroupAmount = 150;
                                        var chunks = GetChunks(listMappingLINEUser, maxGroupAmount);
                                        int groupID = 1;
                                        foreach (var chunk in chunks)
                                        {
                                            string LINEUserIDs = "";
                                            string studentIDs = "";
                                            foreach (var user in chunk)
                                            {
                                                LINEUserIDs += string.Format(@", ""{0}""", user.lineID);
                                                studentIDs += string.Format(@",{0}", user.userID);
                                            }

                                            if (!string.IsNullOrEmpty(LINEUserIDs))
                                            {
                                                LINEUserIDs = LINEUserIDs.Remove(0, 2);
                                                studentIDs += ",";

                                                ctx.TMessageLINEMulticasts.Add(new TMessageLINEMulticast
                                                {
                                                    MessageID = messageID,
                                                    GroupID = groupID,
                                                    StreamID = studentIDs,
                                                    UpdateDate = DateTime.Now,
                                                    SchoolID = schoolID
                                                });
                                                ctx.SaveChanges();

                                                // Send LINE message each user
                                                foreach (var user in chunk)
                                                {
                                                    HttpResponseMessage httpResponseMessage = LINESendMulticastMessage(messageID, user.userName, sms.SMSDesp, schoolID, groupID, sms.nActionType, newsData.AcceptTypeText, listFiles, string.Format(@"""{0}""", user.lineID), user.userID);
                                                    if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
                                                    {
                                                        var content = httpResponseMessage.Content.ReadAsStringAsync().Result.Replace("{", "{{").Replace("}", "}}");
                                                        lineErrorMessage += string.Format($@", [{user.userID}:{user.lineID}:{(int)httpResponseMessage.StatusCode}:{content}]");
                                                    }
                                                }

                                                groupID++;
                                            }
                                        }

                                        // Have error message
                                        if (!string.IsNullOrEmpty(lineErrorMessage))
                                        {
                                            //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                                            //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                                            //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                                            //string connectionString = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                                            //SqlConnection connectObj = new SqlConnection(connectionString);

                                            //fcommon.ExecuteNonQuery(connectObj, string.Format("INSERT INTO [dbo].[TB_APILog] ([Info]) VALUES ('Send News & Send LINE [SchoolID:{0}, SmsID:{1}, MessageID:{2}, Error:{3}]')", schoolID, smsID, messageID, lineErrorMessage.Remove(0, 2)));
                                            fcommon.InsertLog(string.Format("INSERT INTO [dbo].[TB_APILog] ([Info]) VALUES ('Send News & Send LINE [SchoolID:{0}, SmsID:{1}, MessageID:{2}, Error:{3}]')", schoolID, smsID, messageID, lineErrorMessage.Remove(0, 2)));


                                        }
                                    }
                                }
                                catch (Exception err2)
                                {
                                    //string datasource = ConfigurationManager.AppSettings["DataSource"].ToString();
                                    //string password = ConfigurationManager.AppSettings["DB_Password"].ToString();
                                    //string userid = ConfigurationManager.AppSettings["DB_UserID"].ToString();

                                    //string connectionString = string.Format("server={0};database=JabjaiMasterSingleDB;uid={1};pwd={2};", datasource, userid, password);

                                    //SqlConnection connectObj = new SqlConnection(connectionString);

                                    fcommon.InsertLog(string.Format("INSERT INTO [dbo].[TB_APILog] ([Info]) VALUES ('Send News & Send LINE [SchoolID:{0}, SmsID:{1}, MessageID:{2}, Error:{3}]')", schoolID, smsID, messageID, err2.Message));
                                }

                            }).Start();
                        }

                        #endregion
                    }
                    catch (Exception err)
                    {
                        success = false;
                        message = err.Message;

                        
                    }
                
            }

            return JsonConvert.SerializeObject(new { success, code, message });
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

            [JsonProperty(PropertyName = "acceptTypeText")]
            public string AcceptTypeText { get; set; }

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

            [JsonProperty(PropertyName = "sendFileWithOwnerOCR")]
            public bool SendFileWithOwnerOCR { get; set; }

            [JsonProperty(PropertyName = "sendToLINE")]
            public bool SendToLINE { get; set; }

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


            public bool? UploadComplete { get; set; }
            public string TempFilePath { get; set; }
            public List<SplitFileData> SplitTempFilePath { get; set; }
        }

        public class SplitFileData
        {
            public string FilePath { get; set; }

            public bool? UploadComplete { get; set; }
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

        public class RenameProjectFilesResponse
        {
            [JsonProperty(PropertyName = "fileName")]
            public string FileName { get; set; }

            [JsonProperty(PropertyName = "studentCode")]
            public string StudentCode { get; set; }

            [JsonProperty(PropertyName = "errorMessage")]
            public string ErrorMessage { get; set; }
        }



        private HttpStatusCode LINESendPushMessage(JabJaiEntities dbSchool, int messageID, int smsID, string message, int? schoolID, int? userID, string lineUserID, int? actionType, string actionTypeText, List<TNewsFile> listFiles)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.line.me/v2/bot/message/push"))
                {
                    string accessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];

                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");

                    // PostbackData : smsreply-[schoolID]-[message_id]-[user_id]-[replyValue]
                    string colorButton = "#F49453";
                    string messageTitle = "";
                    string messagePicture = "";
                    string messageFile = "";
                    string messageButton = "";
                    switch (actionType)
                    {
                        case 0:
                            messageTitle = "ข่าวสารจาก School Bright";
                            break;
                        case 1:
                        case 2:
                            messageTitle = "แบบตอบรับจาก School Bright";
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply-{1}-{2}-{3}-0"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{4}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeText, schoolID, messageID, userID, colorButton);
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            messageTitle = "แบบตอบรับจาก School Bright";

                            string[] actionTypeTexts = actionTypeText.Split('/');
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply-{2}-{3}-{4}-1"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }},
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{1}"",
                                        ""data"": ""smsreply-{2}-{3}-{4}-0"",
                                        ""text"": ""{1}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeTexts[0], actionTypeTexts[1], schoolID, messageID, userID, colorButton);
                            break;
                        default: break;
                    }

                    int rowIndex = 1;
                    int numberFile = 1;
                    //var listFiles = dbSchool.TNewsFiles.Where(w => w.nSMS == smsID).ToList();
                    if (listFiles.Count > 0)
                    {
                        foreach (var f in listFiles)
                        {
                            Regex regex = new Regex(@"(image|jpe?g|png|gif)");
                            Match match = regex.Match(f.ContentType);
                            if (match.Success)
                            {
                                // Is image
                                if (string.IsNullOrEmpty(messagePicture))
                                {
                                    messagePicture = string.Format(@"
                ""hero"": {{
                      ""type"": ""image"",
                      ""url"": ""{0}"",
                      ""size"": ""full"",
                      ""aspectRatio"": ""4:3"",
                      ""aspectMode"": ""fit"",
                      ""action"": {{
                        ""type"": ""uri"",
                        ""label"": ""Picture 1"",
                        ""uri"": ""{0}""
                      }}
                }},
", f.sFileName);
                                }
                            }
                            else
                            {
                                // Is document
                                messageFile += string.Format(@"
                                        {{
                                            ""type"": ""button"",
                                            ""action"": {{
                                                ""type"": ""uri"",
                                                ""label"": ""File {0}"",
                                                ""uri"": ""{1}""
                                            }}
                                        }}
", numberFile, f.sFileName);
                                if (rowIndex < listFiles.Count) messageFile += ", ";

                                numberFile++;
                            }

                            rowIndex++;
                        }

                        if (!string.IsNullOrEmpty(messageFile))
                        {
                            messageFile = string.Format(@",
                                {{
                                    ""type"": ""box"",
                                    ""layout"": ""vertical"",
                                    ""margin"": ""lg"",
                                    ""contents"": [
                                        {0}
                                    ]
                                }}
", messageFile);
                        }
                    }


                    // Replacing escape characters from JSON : Prepare message data
                    message = message.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\r\n", "\\n");

                    string stringContent = "";
                    switch (actionType)
                    {
                        case 0:
                            stringContent = string.Format(@"
{{
    ""to"": ""{0}"",
    ""messages"":[
        {{
            ""type"":""text"",
            ""text"": ""{1}\n{2}""
        }}
    ]
}}
", lineUserID, messageTitle, message);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            stringContent = string.Format(@"
{{
    ""to"": ""{0}"",
    ""messages"": [
        {{
            ""type"": ""flex"",
            ""altText"": ""{1}"",
            ""contents"": {{
                ""type"": ""bubble"",
                ""direction"": ""ltr"",{5}
                ""body"": {{
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""vertical"",
                            ""contents"": [
                                {{
                                    ""type"": ""text"",
                                    ""text"": ""{2}"",
                                    ""align"": ""start"",
                                    ""wrap"": true
                                }}{3}
                            ]
                        }}
                    ]
                }}{4}
            }}
        }}
    ]
}}", lineUserID, messageTitle, message, messageFile, messageButton, messagePicture);
                            break;
                        default: break;
                    }

                    request.Content = new StringContent(stringContent);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    response = httpClient.SendAsync(request).Result;
                }
            }

            return response.StatusCode;
        }

        public class MappingLINEUser
        {
            //nTSubLevel
            public int? userID { get; set; }
            public string userName { get; set; }
            public string lineID { get; set; }
        }
        public static IEnumerable<IEnumerable<T>> GetChunks<T>(IEnumerable<T> elements, int size)
        {
            var list = elements.ToList();
            while (list.Count > 0)
            {
                var chunk = list.Take(size);
                yield return chunk;

                list = list.Skip(size).ToList();
            }
        }
        private static HttpResponseMessage LINESendMulticastMessage(int messageID, string userName, string message, int schoolID, int groupID, int? actionType, string actionTypeText, List<TNewsFile> listFiles, string lineUserID, int? userID)
        {
            HttpResponseMessage response;
            using (var httpClient = new HttpClient())
            {
                // https://api.line.me/v2/bot/message/multicast
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.line.me/v2/bot/message/push"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    string accessToken = ConfigurationManager.AppSettings["ChannelAccessToken"];

                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + accessToken);
                    request.Headers.TryAddWithoutValidation("cache-control", "no-cache");

                    // PostbackData : smsreply2-[schoolID]-[message_id]-[group_id]-[replyValue]
                    string colorButton = "#F49453";
                    string messageTitle = "";
                    string messagePicture = "";
                    string flexMessageFile = "";
                    string normalMessageFile = "";
                    string messageButton = "";
                    switch (actionType)
                    {
                        case 0:
                            messageTitle = "ประกาศข่าวสารถึง " + userName;
                            break;
                        case 1:
                        case 2:
                            messageTitle = "แบบตอบรับถึง " + userName;
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply2-{1}-{2}-{3}-{4}-0"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{5}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeText, schoolID, messageID, groupID, userID, colorButton);
                            break;
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            messageTitle = "แบบตอบรับถึง " + userName;

                            string[] actionTypeTexts = actionTypeText.Split('/');
                            messageButton = string.Format(@",
                ""footer"": {{
                    ""type"": ""box"",
                    ""layout"": ""horizontal"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""horizontal"",
                            ""contents"": [
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{0}"",
                                        ""data"": ""smsreply2-{2}-{3}-{4}-{5}-1"",
                                        ""text"": ""{0}""
                                    }},
                                    ""color"":""{6}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }},
                                {{
                                    ""type"": ""button"",
                                    ""action"": {{
                                        ""type"": ""postback"",
                                        ""label"": ""{1}"",
                                        ""data"": ""smsreply2-{2}-{3}-{4}-{5}-0"",
                                        ""text"": ""{1}""
                                    }},
                                    ""color"":""{6}"",
                                    ""margin"": ""xs"",
                                    ""style"": ""primary""
                                }}
                            ]
                        }}
                    ]
                }}
", actionTypeTexts[0], actionTypeTexts[1], schoolID, messageID, groupID, userID, colorButton);
                            break;
                        default: break;
                    }

                    int rowIndex = 1;
                    int numberFile = 1;
                    if (listFiles.Count > 0)
                    {
                        foreach (var f in listFiles)
                        {
                            Regex regex = new Regex(@"(image|jpe?g|png|gif)");
                            Match match = regex.Match(f.ContentType);
                            if (match.Success)
                            {
                                // Is image
                                if (string.IsNullOrEmpty(messagePicture))
                                {
                                    messagePicture = string.Format(@"
                ""hero"": {{
                      ""type"": ""image"",
                      ""url"": ""{0}"",
                      ""size"": ""full"",
                      ""aspectRatio"": ""4:3"",
                      ""aspectMode"": ""fit"",
                      ""action"": {{
                        ""type"": ""uri"",
                        ""label"": ""Picture 1"",
                        ""uri"": ""{0}""
                      }}
                }},
", f.sFileName);
                                }
                                else
                                {
                                    // Is picture
                                    flexMessageFile += string.Format(@",
                                        {{
                                            ""type"": ""button"",
                                            ""action"": {{
                                                ""type"": ""uri"",
                                                ""label"": ""{0}. Open Picture"",
                                                ""uri"": ""{1}""
                                            }}
                                        }}
", numberFile, f.sFileName);

                                    numberFile++;
                                }
                            }
                            else
                            {
                                // Is document
                                flexMessageFile += string.Format(@",
                                        {{
                                            ""type"": ""button"",
                                            ""action"": {{
                                                ""type"": ""uri"",
                                                ""label"": ""{0}. Open File"",
                                                ""uri"": ""{1}""
                                            }}
                                        }}
", numberFile, f.sFileName);
                                //if (rowIndex < listFiles.Count) flexMessageFile += ", ";

                                numberFile++;
                            }

                            // For attach file to normal message
                            normalMessageFile += ", " + f.sFileName;

                            rowIndex++;
                        }

                        if (!string.IsNullOrEmpty(flexMessageFile))
                        {
                            flexMessageFile = string.Format(@",
                                {{
                                    ""type"": ""box"",
                                    ""layout"": ""vertical"",
                                    ""margin"": ""lg"",
                                    ""contents"": [
                                        {0}
                                    ]
                                }}
", flexMessageFile.Remove(0, 1));
                        }

                        if (!string.IsNullOrEmpty(normalMessageFile))
                        {
                            normalMessageFile = normalMessageFile.Remove(0, 2);
                        }
                    }


                    // Replacing escape characters from JSON : Prepare message data
                    message = message.Replace("\\", "\\\\")
                        .Replace("\"", "\\\"")
                        .Replace("\r\n", "\\n");

                    // Max Length LINE Message : 5000
                    if (message.Length > 4800) message = message.Substring(0, 4800);

                    string stringContent = "";
                    switch (actionType)
                    {
                        case 0:
                            stringContent = string.Format(@"
{{
    ""to"": {0},
    ""messages"":[
        {{
            ""type"":""text"",
            ""text"": ""{1}\n{2}\n{3}""
        }}
    ]
}}", lineUserID, messageTitle, message, normalMessageFile);
                            break;
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                            stringContent = string.Format(@"
{{
    ""to"": {0},
    ""messages"": [
        {{
            ""type"": ""flex"",
            ""altText"": ""{1}"",
            ""contents"": {{
                ""type"": ""bubble"",
                ""direction"": ""ltr"",
                ""header"": {{
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                        {{
                            ""type"": ""text"",
                            ""text"": ""{1}"",
                            ""weight"": ""bold"",
                            ""wrap"": true,
                            ""contents"": []
                        }}
                    ]
                }},{5}
                ""body"": {{
                    ""type"": ""box"",
                    ""layout"": ""vertical"",
                    ""contents"": [
                        {{
                            ""type"": ""box"",
                            ""layout"": ""vertical"",
                            ""contents"": [
                                {{
                                    ""type"": ""text"",
                                    ""text"": ""{2}"",
                                    ""align"": ""start"",
                                    ""wrap"": true
                                }}{3}
                            ]
                        }}
                    ]
                }}{4}
            }}
        }}
    ]
}}", lineUserID, messageTitle, message, flexMessageFile, messageButton, messagePicture);
                            break;
                        default: break;
                    }

                    request.Content = new StringContent(stringContent);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    response = httpClient.SendAsync(request).Result;
                }
            }

            return response;
        }

    }
}