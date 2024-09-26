using AjaxControlToolkit;
using JabjaiEntity.DB;
using JabjaiMainClass;
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

namespace FingerprintPayment.Modules.TimeAttendance
{
    public partial class ReplyMessageReport : SMSGateway
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string LoadReplyMessage()
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

            string sortBy = "UserID";
            switch (sortIndex)
            {
                case "1": sortBy = "Name"; break;
                case "2": sortBy = "Type"; break;
                case "3": sortBy = "ReadStatus"; break;
                case "4": sortBy = "ReplyMessage"; break;
            }
            sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

            //
            string smsID = Convert.ToString(jsonObject["smsID"]);

            var json = LoadReplyMessageJsonData(draw, pageIndex, pageSize, sortBy, GetUserData().CompanyID, smsID);

            return json;
        }

        public static string LoadReplyMessageJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string smsID)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataReplyMessage> listData;
            List<EntityReplyMessageList> listAllData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(smsID)) { sqlCondition += string.Format(@" AND sms.nSMS={0}", smsID); } else { sqlCondition += " AND sms.nSMS=0"; }

                int wt = 1;
                string sqlQueryCount = GenerateQueryCount(wt, schoolID, sqlCondition);
                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                if (resultCount == 0)
                {
                    wt++;
                    sqlQueryCount = GenerateQueryCount(wt, schoolID, sqlCondition);
                    resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                    if (resultCount == 0)
                    {
                        wt++;
                        sqlQueryCount = GenerateQueryCount(wt, schoolID, sqlCondition);
                        resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();

                    }
                }

                string sqlQueryFilter = GenerateQueryPage(wt, sortBy, schoolID, sqlCondition, lowerBound, upperBound);
                List<EntityReplyMessageList> resultFilter = en.Database.SqlQuery<EntityReplyMessageList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = resultFilter.Select(s => new ListDataReplyMessage
                {
                    no = "",
                    Name = s.Name,
                    Type = s.Type,
                    ReadStatus = s.ReadStatus,
                    ReplyMessage = s.ReplyMessage,
                    uid = s.UserID.ToString()
                }).ToList();

                string sqlQueryAll = GenerateQueryAll(wt, schoolID, sqlCondition);
                listAllData = en.Database.SqlQuery<EntityReplyMessageList>(sqlQueryAll).ToList();
            }

            CollectionData<ListDataReplyMessage> data = new CollectionData<ListDataReplyMessage>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + ((totalData % pageSize) == 0 ? 0 : 1);
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            data.readCount = listAllData.Where(w => w.ReadStatus == "อ่านแล้ว").Count();
            data.replyCount = listAllData.Where(w => !string.IsNullOrWhiteSpace(w.ReplyMessage)).Count();

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string GenerateQueryCount(int wt, int schoolID, string sqlCondition)
        {
            string tableMessageUser = "";
            string tableMessageBox = "";

            switch (wt)
            {
                case 1: tableMessageUser = "TMessage_User"; tableMessageBox = "TMessageBox"; break;
                case 2: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox]"; break;
                case 3: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User.old]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox.old]"; break;
            }

            string query = string.Format(@"
SELECT COUNT(*)
FROM
(
	SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	, ISNULL(tl.titleDescription, ISNULL(e.sTitle, ''))+e.sName+' '+e.sLastname 'Name', et.Title 'Type'
	FROM {2} mu 
	LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
	LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = CAST(et.nTypeId AS VARCHAR(10))
	LEFT JOIN TTitleList tl ON e.SchoolID = tl.SchoolID AND e.sTitle = CAST(tl.nTitleid AS VARCHAR(10))
	WHERE mu.user_type='1'
	UNION
	SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	, ISNULL(tl.titleDescription, ISNULL(u.sStudentTitle, ''))+u.sName+' '+u.sLastname 'Name', N'นักเรียน' 'Type'
	FROM {2} mu 
	LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
	LEFT JOIN TTitleList tl ON u.SchoolID = tl.SchoolID AND u.sStudentTitle = CAST(tl.nTitleid AS VARCHAR(10))
	WHERE mu.user_type='0'
) amu 
LEFT JOIN {3} mb ON amu.SchoolID = mb.SchoolID AND amu.message_id = mb.nMessageID
LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
WHERE sms.SchoolID={0} {1}", schoolID, sqlCondition, tableMessageUser, tableMessageBox);

            return query;
        }

        public static string GenerateQueryPage(int wt, string sortBy, int schoolID, string sqlCondition, int lowerBound, int upperBound)
        {
            string tableMessageUser = "";
            string tableMessageBox = "";

            switch (wt)
            {
                case 1: tableMessageUser = "TMessage_User"; tableMessageBox = "TMessageBox"; break;
                case 2: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox]"; break;
                case 3: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User.old]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox.old]"; break;
            }

            string query = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT amu.UserID, amu.Name, amu.Type, amu.ReadStatus
        , (CASE 
	        WHEN sms.nActionType = 0 THEN 'ไม่มีการตอบรับ' 
	        WHEN sms.nActionType = 1 AND amu.nActionResult = 0 THEN 'รับทราบ' 
	        WHEN sms.nActionType = 2 AND amu.nActionResult = 0 THEN 'ยืนยัน' 
	        WHEN sms.nActionType = 3 AND amu.nActionResult = 1 THEN 'เห็นด้วย' 
	        WHEN sms.nActionType = 3 AND amu.nActionResult = 0 THEN 'ไม่เห็นด้วย' 
	        WHEN sms.nActionType = 4 AND amu.nActionResult = 1 THEN 'ยินยอม' 
	        WHEN sms.nActionType = 4 AND amu.nActionResult = 0 THEN 'ไม่ยินยอม' 
	        WHEN sms.nActionType = 5 AND amu.nActionResult = 1 THEN 'สนใจ' 
	        WHEN sms.nActionType = 5 AND amu.nActionResult = 0 THEN 'ไม่สนใจ' 
	        WHEN sms.nActionType = 6 AND amu.nActionResult = 1 THEN 'ชอบ' 
	        WHEN sms.nActionType = 6 AND amu.nActionResult = 0 THEN 'ไม่ชอบ' 
	        WHEN sms.nActionType = 7 AND amu.nActionResult = 1 THEN 'สนใจ' 
	        WHEN sms.nActionType = 7 AND amu.nActionResult = 0 THEN 'สนใจมาก' 
	        ELSE '' END) 'ReplyMessage' 
        FROM
        (
	        SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	        , ISNULL(tl.titleDescription, ISNULL(e.sTitle, ''))+e.sName+' '+e.sLastname 'Name', et.Title 'Type'
	        FROM {5} mu 
	        LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
	        LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = CAST(et.nTypeId AS VARCHAR(10))
	        LEFT JOIN TTitleList tl ON e.SchoolID = tl.SchoolID AND e.sTitle = CAST(tl.nTitleid AS VARCHAR(10))
	        WHERE mu.user_type='1'
	        UNION
	        SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	        , ISNULL(tl.titleDescription, ISNULL(u.sStudentTitle, ''))+u.sName+' '+u.sLastname 'Name', N'นักเรียน' 'Type'
	        FROM {5} mu 
	        LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
	        LEFT JOIN TTitleList tl ON u.SchoolID = tl.SchoolID AND u.sStudentTitle = CAST(tl.nTitleid AS VARCHAR(10))
	        WHERE mu.user_type='0'
        ) amu 
        LEFT JOIN {6} mb ON amu.SchoolID = mb.SchoolID AND amu.message_id = mb.nMessageID
        LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
        WHERE sms.SchoolID={1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound, tableMessageUser, tableMessageBox);

            return query;
        }

        public static string GenerateQueryAll(int wt, int schoolID, string sqlCondition)
        {
            string tableMessageUser = "";
            string tableMessageBox = "";

            switch (wt)
            {
                case 1: tableMessageUser = "TMessage_User"; tableMessageBox = "TMessageBox"; break;
                case 2: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox]"; break;
                case 3: tableMessageUser = "[JabjaiSchoolHistory].[dbo].[TMessage_User.old]"; tableMessageBox = "[JabjaiSchoolHistory].[dbo].[TMessageBox.old]"; break;
            }

            string query = string.Format(@"
SELECT amu.UserID, amu.Name, amu.Type, amu.ReadStatus
, (CASE 
	WHEN sms.nActionType = 0 THEN 'ไม่มีการตอบรับ' 
	WHEN sms.nActionType = 1 AND amu.nActionResult = 0 THEN 'รับทราบ' 
	WHEN sms.nActionType = 2 AND amu.nActionResult = 0 THEN 'ยืนยัน' 
	WHEN sms.nActionType = 3 AND amu.nActionResult = 1 THEN 'เห็นด้วย' 
	WHEN sms.nActionType = 3 AND amu.nActionResult = 0 THEN 'ไม่เห็นด้วย' 
	WHEN sms.nActionType = 4 AND amu.nActionResult = 1 THEN 'ยินยอม' 
	WHEN sms.nActionType = 4 AND amu.nActionResult = 0 THEN 'ไม่ยินยอม' 
	WHEN sms.nActionType = 5 AND amu.nActionResult = 1 THEN 'สนใจ' 
	WHEN sms.nActionType = 5 AND amu.nActionResult = 0 THEN 'ไม่สนใจ' 
	WHEN sms.nActionType = 6 AND amu.nActionResult = 1 THEN 'ชอบ' 
	WHEN sms.nActionType = 6 AND amu.nActionResult = 0 THEN 'ไม่ชอบ' 
	WHEN sms.nActionType = 7 AND amu.nActionResult = 1 THEN 'สนใจ' 
	WHEN sms.nActionType = 7 AND amu.nActionResult = 0 THEN 'สนใจมาก' 
	ELSE '' END) 'ReplyMessage' 
FROM
(
	SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	, ISNULL(tl.titleDescription, ISNULL(e.sTitle, ''))+e.sName+' '+e.sLastname 'Name', et.Title 'Type'
	FROM {2} mu 
	LEFT JOIN TEmployees e ON mu.SchoolID = e.SchoolID AND mu.UserID = e.sEmp
	LEFT JOIN TEmployeeType et ON e.SchoolID = et.SchoolID AND e.cType = CAST(et.nTypeId AS VARCHAR(10))
	LEFT JOIN TTitleList tl ON e.SchoolID = tl.SchoolID AND e.sTitle = CAST(tl.nTitleid AS VARCHAR(10))
	WHERE mu.user_type='1'
	UNION
	SELECT mu.SchoolID, mu.UserID, mu.MessageUserID, mu.message_id, (CASE WHEN mu.read_status = 1 THEN N'อ่านแล้ว' ELSE N'ไม่ได้อ่าน' END) 'ReadStatus', mu.nActionResult
	, ISNULL(tl.titleDescription, ISNULL(u.sStudentTitle, ''))+u.sName+' '+u.sLastname 'Name', N'นักเรียน' 'Type'
	FROM {2} mu 
	LEFT JOIN TUser u ON mu.SchoolID = u.SchoolID AND mu.UserID = u.sID
	LEFT JOIN TTitleList tl ON u.SchoolID = tl.SchoolID AND u.sStudentTitle = CAST(tl.nTitleid AS VARCHAR(10))
	WHERE mu.user_type='0'
) amu 
LEFT JOIN {3} mb ON amu.SchoolID = mb.SchoolID AND amu.message_id = mb.nMessageID
LEFT JOIN TSMS sms ON mb.SchoolID = sms.SchoolID AND mb.push_id = sms.nSMS
WHERE sms.SchoolID={0} {1}", schoolID, sqlCondition, tableMessageUser, tableMessageBox);

            return query;
        }

        public class CollectionData<T>
        {
            public int draw { get; set; }
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int pageCount { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public List<T> data { get; set; }

            public int readCount { get; set; }
            public int replyCount { get; set; }
        }

        /// <summary>
        /// Reply Message entity query
        /// </summary>
        public class EntityReplyMessageList
        {
            public int UserID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string ReadStatus { get; set; }
            public string ReplyMessage { get; set; }
        }

        /// <summary>
        /// Reply Message display columm list
        /// </summary>
        public class ListDataReplyMessage
        {
            public string no { get; set; }
            //ชื่อ - นามสกุล
            public string Name { get; set; }
            //ประเภท ครู/นักเรียน
            public string Type { get; set; }
            //สถานะการอ่าน
            public string ReadStatus { get; set; }
            //การตอบกลับ
            public string ReplyMessage { get; set; }
            public string uid { get; set; }
        }
    }
}