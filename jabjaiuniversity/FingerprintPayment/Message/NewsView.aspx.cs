using FingerprintPayment.Message.CsCode;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace FingerprintPayment.Message
{
    public partial class NewsView : MessageGateway
    {
        protected NewsInfo _NewsInfo = new NewsInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitialPage();
        }

        private void InitialPage()
        {
            string nid = Request.QueryString["nid"];
            int.TryParse(nid, out int smsID);

            var userData = GetUserData();
            int schoolID = userData.CompanyID;
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string insertScript = "";

                // News info
                var smsObj = en.TSMS.Where(w => w.SchoolID == schoolID && w.nSMS == smsID).FirstOrDefault();
                _NewsInfo.NewsID = smsObj.nSMS;
                _NewsInfo.Title = smsObj.SMSTitle;
                _NewsInfo.MessageType = smsObj.SMSDuration == 0 ? "แจ้งประกาศกิจกรรม" : "แจ้งประกาศข่าวสาร";
                _NewsInfo.SendType = smsObj.SMSType == 0 ? "ส่งทันที" : "ตั้งเวลาการส่ง";
                if (smsObj.SMSType == 1)
                {
                    _NewsInfo.SendDate = smsObj.dSend?.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    _NewsInfo.SendTime = smsObj.dSend?.ToString("HH:mm:ss", new CultureInfo("th-TH"));

                    insertScript += "$('.date-time').show();";
                }

                // 0: ไม่มีการตอบรับ
                // 1: รับทราบ
                // 2: ยืนยัน
                // 3: เห็นด้วย/ไม่เห็นด้วย
                // 4: ยินยอม/ไม่ยินยอม
                // 5: สนใจ/ไม่สนใจ
                // 6: ชอบ/ไม่ชอบ
                // 7: สนใจ/สนใจมาก
                switch (smsObj.nActionType)
                {
                    case 0: _NewsInfo.AcceptType = "ไม่มีการตอบรับ"; break;
                    case 1: _NewsInfo.AcceptType = "รับทราบ"; break;
                    case 2: _NewsInfo.AcceptType = "ยืนยัน"; break;
                    case 3: _NewsInfo.AcceptType = "เห็นด้วย/ไม่เห็นด้วย"; break;
                    case 4: _NewsInfo.AcceptType = "ยินยอม/ไม่ยินยอม"; break;
                    case 5: _NewsInfo.AcceptType = "สนใจ/ไม่สนใจ"; break;
                    case 6: _NewsInfo.AcceptType = "ชอบ/ไม่ชอบ"; break;
                    case 7: _NewsInfo.AcceptType = "สนใจ/สนใจมาก"; break;
                }

                // 1 = รายกลุ่ม, 2 = รายบุคคล
                switch (smsObj.SMSGroupType)
                {
                    case 1: _NewsInfo.GroupType = "รายกลุ่ม"; break;
                    case 2:
                        _NewsInfo.GroupType = "รายบุคคล";
                        insertScript += "$('.send-group').hide(); $('.send-individual').show();";
                        break;
                    default: _NewsInfo.GroupType = "-"; break;
                }

                //if (smsObj.SMSAll?.Trim() == "1" && smsObj.SMSEMP == "1")
                //{
                //    _NewsInfo.Group = "ทั้งหมด (ทุกคนในโรงเรียน)";
                //}
                //else if (smsObj.SMSAll?.Trim() == "1")
                //{
                //    _NewsInfo.Group = "เฉพาะนักเรียน";
                //}
                //else if (smsObj.SMSEMP == "1")
                //{
                //    _NewsInfo.Group = "เฉพาะบุคลากร";
                //}
                //else
                //{
                //    _NewsInfo.Group = "-";
                //    if (smsObj.SMSGroupID != null)
                //    {
                //        var groupObj = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.SMSGroupID == smsObj.SMSGroupID).FirstOrDefault();
                //        if (groupObj != null)
                //        {
                //            _NewsInfo.Group = groupObj.SMSGroupName;
                //        }
                //    }
                //}
                switch (smsObj.SMSAll?.Trim())
                {
                    case "0": //all
                        _NewsInfo.Group = "ทั้งหมด (ทุกคนในโรงเรียน)";
                        break;
                    case "1": //all-employee
                        _NewsInfo.Group = "เฉพาะบุคลากร";
                        break;
                    case "2": //all-student
                        _NewsInfo.Group = "เฉพาะนักเรียน";
                        break;
                    case "4": //all-student-level
                    case "3": //other group
                        _NewsInfo.Group = "-";
                        if (smsObj.SMSGroupID != null)
                        {
                            var groupObj = en.TSMSGroup.Where(w => w.SchoolID == schoolID && w.SMSGroupID == smsObj.SMSGroupID).FirstOrDefault();
                            if (groupObj != null)
                            {
                                _NewsInfo.Group = groupObj.SMSGroupName;
                            }
                        }
                        break;
                }

                _NewsInfo.Message = smsObj.SMSDesp;

                // Get term id from send date
                StudentLogic studentLogic = new StudentLogic(en);
                var termID = studentLogic.GetTermId(smsObj.dSend.Value, userData);

                // Insert script
                ltrScript.Text = string.Format(@"<script>
        $(document).ready(function () {{
            {0}
        }});
    </script>", insertScript);


//                string query = string.Format(@"
//SELECT * FROM [TMessageBox] WHERE SchoolID = {0} AND push_id = {1}
//UNION
//SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox] WHERE SchoolID = {0} AND push_id = {1}
//UNION
//SELECT * FROM [JabjaiSchoolHistory].[dbo].[TMessageBox.old] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);
//                TMessageBox messageBoxObj = en.Database.SqlQuery<TMessageBox>(query).FirstOrDefault();


                string query = string.Format(@"SELECT * FROM [TMessageBox] WHERE SchoolID = {0} AND push_id = {1}", schoolID, smsID);

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
                    // User list
                    // 0: ไม่มีการตอบรับ
                    // 1: รับทราบ
                    // 2: ยืนยัน
                    // 3: เห็นด้วย/ไม่เห็นด้วย
                    // 4: ยินยอม/ไม่ยินยอม
                    // 5: สนใจ/ไม่สนใจ
                    // 6: ชอบ/ไม่ชอบ
                    // 7: สนใจ/สนใจมาก
                    query = string.Format(@"
SELECT *
FROM
(
    SELECT CASE WHEN user_type = 0 THEN sl.SubLevel+'/'+tsl.nTSubLevel2 ELSE '-' END 'Classroom'
    , CASE WHEN user_type = 0 THEN u.sStudentID ELSE ei.Code END 'Code'
    , CASE WHEN user_type = 0 THEN ut.titleDescription+u.sName+' '+u.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
    , CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
    , CASE WHEN read_status = 1 THEN 'อ่านแล้ว' ELSE 'ไม่ได้อ่าน' END 'ReadStatus'
    , CASE 
	    WHEN nActionResult IS NULL 	THEN '-' 
	    ELSE 
		    CASE 
			    WHEN sms.nActionType = 0 THEN '-'
			    WHEN sms.nActionType = 1 AND mu.nActionResult IS NOT NULL THEN 'รับทราบ'
			    WHEN sms.nActionType = 2 AND mu.nActionResult IS NOT NULL THEN 'ยืนยัน'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 1 THEN 'เห็นด้วย'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 0 THEN 'ไม่เห็นด้วย'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 1 THEN 'ยินยอม'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 0 THEN 'ไม่ยินยอม'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 0 THEN 'ไม่สนใจ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 1 THEN 'ชอบ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 0 THEN 'ไม่ชอบ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 0 THEN 'สนใจมาก'
		    END
      END 'MessageReply'
    --, mu.MessageUserID, mu.message_id, UserID, user_type, read_status, nActionResult, sms.nActionType, sms.nSMS
    FROM [TMessage_User] mu 
    LEFT JOIN [TMessageBox] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
    LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
    LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
    LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
    LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
    LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
    LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
    LEFT JOIN TTitleList ut ON u.SchoolID = ut.SchoolID AND u.sStudentTitle = CAST(ut.nTitleid AS VARCHAR(10))
    LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.sID = sch.sID AND sch.nTerm = '{2}' AND sch.cDel = 0
    LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID = tsl.SchoolID AND sch.nTermSubLevel2 = tsl.nTermSubLevel2
    LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel 
    WHERE mu.SchoolID = {0} AND mu.message_id = {1}
    UNION
    SELECT CASE WHEN user_type = 0 THEN sl.SubLevel+'/'+tsl.nTSubLevel2 ELSE '-' END 'Classroom'
    , CASE WHEN user_type = 0 THEN u.sStudentID ELSE ei.Code END 'Code'
    , CASE WHEN user_type = 0 THEN ut.titleDescription+u.sName+' '+u.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
    , CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
    , CASE WHEN read_status = 1 THEN 'อ่านแล้ว' ELSE 'ไม่ได้อ่าน' END 'ReadStatus'
    , CASE 
	    WHEN nActionResult IS NULL 	THEN '-' 
	    ELSE 
		    CASE 
			    WHEN sms.nActionType = 0 THEN '-'
			    WHEN sms.nActionType = 1 AND mu.nActionResult IS NOT NULL THEN 'รับทราบ'
			    WHEN sms.nActionType = 2 AND mu.nActionResult IS NOT NULL THEN 'ยืนยัน'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 1 THEN 'เห็นด้วย'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 0 THEN 'ไม่เห็นด้วย'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 1 THEN 'ยินยอม'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 0 THEN 'ไม่ยินยอม'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 0 THEN 'ไม่สนใจ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 1 THEN 'ชอบ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 0 THEN 'ไม่ชอบ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 0 THEN 'สนใจมาก'
		    END
      END 'MessageReply'
    FROM [JabjaiSchoolHistory].[dbo].[TMessage_User] mu 
    LEFT JOIN [JabjaiSchoolHistory].[dbo].[TMessageBox] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
    LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
    LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
    LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
    LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
    LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
    LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
    LEFT JOIN TTitleList ut ON u.SchoolID = ut.SchoolID AND u.sStudentTitle = CAST(ut.nTitleid AS VARCHAR(10))
    LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.sID = sch.sID AND sch.nTerm = '{2}' AND sch.cDel = 0
    LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID = tsl.SchoolID AND sch.nTermSubLevel2 = tsl.nTermSubLevel2
    LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
    WHERE mu.SchoolID = {0} AND mu.message_id = {1}
    UNION
    SELECT CASE WHEN user_type = 0 THEN sl.SubLevel+'/'+tsl.nTSubLevel2 ELSE '-' END 'Classroom'
    , CASE WHEN user_type = 0 THEN u.sStudentID ELSE ei.Code END 'Code'
    , CASE WHEN user_type = 0 THEN ut.titleDescription+u.sName+' '+u.sLastname ELSE t.titleDescription+e.sName+' '+e.sLastname END 'Name'
    , CASE WHEN user_type = 0 THEN 'นักเรียน' ELSE et.Title END 'Type'
    , CASE WHEN read_status = 1 THEN 'อ่านแล้ว' ELSE 'ไม่ได้อ่าน' END 'ReadStatus'
    , CASE 
	    WHEN nActionResult IS NULL 	THEN '-' 
	    ELSE 
		    CASE 
			    WHEN sms.nActionType = 0 THEN '-'
			    WHEN sms.nActionType = 1 AND mu.nActionResult IS NOT NULL THEN 'รับทราบ'
			    WHEN sms.nActionType = 2 AND mu.nActionResult IS NOT NULL THEN 'ยืนยัน'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 1 THEN 'เห็นด้วย'
			    WHEN sms.nActionType = 3 AND mu.nActionResult = 0 THEN 'ไม่เห็นด้วย'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 1 THEN 'ยินยอม'
			    WHEN sms.nActionType = 4 AND mu.nActionResult = 0 THEN 'ไม่ยินยอม'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 5 AND mu.nActionResult = 0 THEN 'ไม่สนใจ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 1 THEN 'ชอบ'
			    WHEN sms.nActionType = 6 AND mu.nActionResult = 0 THEN 'ไม่ชอบ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 1 THEN 'สนใจ'
			    WHEN sms.nActionType = 7 AND mu.nActionResult = 0 THEN 'สนใจมาก'
		    END
      END 'MessageReply'
    FROM [JabjaiSchoolHistory].[dbo].[TMessage_User.old] mu 
    LEFT JOIN [JabjaiSchoolHistory].[dbo].[TMessageBox.old] mb ON mu.SchoolID = mb.SchoolID AND mu.message_id = mb.nMessageID
    LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
    LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
    LEFT JOIN TEmployeeInfo ei ON e.SchoolID = ei.SchoolID AND e.sEmp = ei.sEmp
    LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = ISNULL(et.nTypeId2, et.nTypeId)
    LEFT JOIN TTitleList t ON e.SchoolID = t.SchoolID AND e.sTitle = CAST(t.nTitleid AS VARCHAR(10))
    LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
    LEFT JOIN TTitleList ut ON u.SchoolID = ut.SchoolID AND u.sStudentTitle = CAST(ut.nTitleid AS VARCHAR(10))
    LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.sID = sch.sID AND sch.nTerm = '{2}' AND sch.cDel = 0
    LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID = tsl.SchoolID AND sch.nTermSubLevel2 = tsl.nTermSubLevel2
    LEFT JOIN TSubLevel sl ON tsl.SchoolID = sl.SchoolID AND tsl.nTSubLevel = sl.nTSubLevel
    WHERE mu.SchoolID = {0} AND mu.message_id = {1}
) A
ORDER BY Code", schoolID, messageBoxObj.nMessageID, termID);

                    int no = 1;
                    string newsUser = "";
                    List<MessageUserData> messageUsers = en.Database.SqlQuery<MessageUserData>(query).ToList();
                    foreach (var mu in messageUsers)
                    {
                        newsUser += string.Format(@"<tr>
                                                <td>{0}.</td>
                                                <td>{1}</td>
                                                <td>{2}</td>
                                                <td>{3}</td>
                                                <td>{4}</td>
                                                <td>{5}</td>
                                                <td>{6}</td>
                                            </tr>", no, mu.Classroom, mu.Code, mu.Name, mu.Type, mu.ReadStatus, mu.MessageReply);
                        no++;
                    }

                    ltrUserList.Text = newsUser;

                    if (smsObj.SMSGroupType == 2)
                    {
                        int countStudentType = messageUsers.Where(w => w.Type == "นักเรียน").Count();
                        if (messageUsers.Count == countStudentType)
                        {
                            _NewsInfo.GroupIndividual = "นักเรียน";
                        }
                        else if (messageUsers.Count > 0 && countStudentType == 0)
                        {
                            _NewsInfo.GroupIndividual = "บุคลากร";
                        }
                        else
                        {
                            _NewsInfo.GroupIndividual = "บุคลากร/นักเรียน";
                        }
                    }
                }


                // File list
                var newsFileList = en.TNewsFiles.Where(w => w.SchoolID == schoolID && w.nSMS == smsID && w.cDel == false).ToList();
                if (newsFileList.Count > 0)
                {
                    string newsFile = "";
                    foreach (var nf in newsFileList)
                    {
                        string fileName = "";
                        string contentType = "";
                        string fileSize = "";
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

                        newsFile += string.Format(@"<div class=""files-uploaded"">
                                                <img src=""{0}"">
                                                <p class=""file-name"">
                                                    {1}<br />
                                                    <span class=""file-size"">{3}</span>
                                                </p>
                                                <p class=""file-action"">
                                                    <button type=""button"" class=""btn btn-info btn-link file-view"" data-id=""{4}"" data-toggle=""tooltip"" data-placement=""top"" title=""View File"" data-contentType=""{5}"" data-url=""{2}"">
                                                        <i class=""material-icons"">zoom_in</i>
                                                    </button>
                                                    <button type=""button"" class=""btn btn-success btn-link file-download"" data-id=""{4}"" data-toggle=""tooltip"" data-placement=""top"" title=""Download File"" data-contentType=""{5}"" data-url=""{2}"">
                                                        <i class=""material-icons"">download</i>
                                                    </button>
                                                </p>
                                            </div>", contentType, fileName, nf.sFileName, fileSize, nf.nNewsFileID, nf.ContentType);
                    }

                    ltrFileList.Text = string.Format(@"<div class=""row"">
                                <div class=""col-md-10 ml-auto mr-auto"">
                                    <div>
                                        <label class=""col-form-label""><span>ไฟล์แนบ : </span></label>
                                    </div>
                                    <div>
                                        <div class=""files-uploaded-list"">
                                            {0}
                                        </div>
                                    </div>
                                </div>
                            </div>", newsFile);
                }
            }
        }


        [WebMethod(EnableSession = true)]
        public static object RemoveNews(int smsID)
        {
            bool success = true;
            string code = "200";
            string message = "Success to remove news data.";

            JWTToken.userData userData = GetUserData();
            int schoolID = userData.CompanyID;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                try
                {
                    var smsObj = en.TSMS.Where(w => w.nSMS == smsID).FirstOrDefault();
                    if (smsObj != null)
                    {
                        smsObj.isDel = true;
                        smsObj.cDel = true;
                        smsObj.UpdatedDate = DateTime.Now;
                        smsObj.UpdatedBy = userData.UserID;

                        en.SaveChanges();

                        var sql = @"
UPDATE TMessageBox SET cDel=1, UpdatedDate=GETDATE(), UpdatedBy=@UpdatedBy WHERE push_id=@PushID;
UPDATE JabjaiSchoolHistory.dbo.TMessageBox SET cDel=1, UpdatedDate=GETDATE(), UpdatedBy=@UpdatedBy WHERE push_id=@PushID;";
                        var updatedBy = new SqlParameter("@UpdatedBy", userData.UserID);
                        var pushID = new SqlParameter("@PushID", smsID);

                        en.Database.ExecuteSqlCommand(sql, new[] { updatedBy, pushID });

                        database.InsertLog(userData.UserID.ToString(), "ลบข้อมูล (" + smsObj.nSMS + ") " + smsObj.SMSTitle, userData.Entities, null, 2, 2, 0);
                    }
                    else
                    {
                        success = false;
                        message = "ไม่พบข้อมูลข่าวสาร.";
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

        public class NewsInfo
        {
            public int NewsID { get; set; }
            public string Title { get; set; }
            public string MessageType { get; set; }
            public string SendType { get; set; }
            public string SendDate { get; set; }
            public string SendTime { get; set; }
            public string AcceptType { get; set; }
            public string GroupType { get; set; }
            public string Group { get; set; }
            public string GroupIndividual { get; set; }
            public string Message { get; set; }
        }

        public class MessageUserData
        {
            [JsonProperty(PropertyName = "classroom")]
            public string Classroom { get; set; }

            [JsonProperty(PropertyName = "code")]
            public string Code { get; set; }

            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            [JsonProperty(PropertyName = "type")]
            public string Type { get; set; }

            [JsonProperty(PropertyName = "readStatus")]
            public string ReadStatus { get; set; }

            [JsonProperty(PropertyName = "messageReply")]
            public string MessageReply { get; set; }
        }

        string GetFileSize(Uri uriPath)
        {
            var webRequest = System.Net.HttpWebRequest.Create(uriPath);
            webRequest.Method = "HEAD";

            using (var webResponse = webRequest.GetResponse())
            {
                var fileSize = webResponse.Headers.Get("Content-Length");
                return SizeSuffix(Convert.ToInt64(fileSize));
            }
        }

        readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        string SizeSuffix(Int64 value, int decimalPlaces = 2)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}", adjustedSize, SizeSuffixes[mag]);
        }
    }
}