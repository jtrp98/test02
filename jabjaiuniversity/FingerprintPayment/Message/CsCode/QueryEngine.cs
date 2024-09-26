using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FingerprintPayment.Message.CsCode
{
    public class QueryEngine
    {
        public QueryEngine()
        {
            //
            // Add constructor logic here
            //
            /*
             http://localhost:50465//Home/AjaxGetJsonData?draw=1&columns[0][data]=Name&columns[0][name]=&columns[0][searchable]=true&columns[0][orderable]=true&columns[0][search][value]=&columns[0][search][regex]=false&columns[1][data]=Age&columns[1][name]=&columns[1][searchable]=true&columns[1][orderable]=false&columns[1][search][value]=&columns[1][search][regex]=false&columns[2][data]=DoB&columns[2][name]=&columns[2][searchable]=true&columns[2][orderable]=true&columns[2][search][value]=&columns[2][search][regex]=false&order[0][column]=0&order[0][dir]=asc&start=0&length=10&search[value]=&search[regex]=false&_=1437225574923
             */
        }

        public static string LoadGroupJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string groupName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataGroup> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(groupName)) { sqlCondition += string.Format(@" AND g.SMSGroupName LIKE N'%{0}%' ", groupName.Trim()); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TSMSGroup g 
WHERE g.cDel = 0 AND g.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT SMSGroupID, SMSGroupName, SMSGroupNameEn, Status
		FROM TSMSGroup g 
        WHERE g.cDel = 0 AND g.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityGroupList> resultFilter = en.Database.SqlQuery<EntityGroupList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataGroup>();
                ListDataGroup item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataGroup();
                    item.no = "";
                    item.GroupName = i.SMSGroupName;
                    item.GroupNameEn = i.SMSGroupNameEn;
                    item.status = "";
                    item.@switch = "";
                    item.action = "";
                    item.gid = i.SMSGroupID.ToString();
                    item.istatus = i.Status.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataGroup> data = new CollectionData<ListDataGroup>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + ((totalData % pageSize) == 0 ? 0 : 1);
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string LoadNewsJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string startDate, string endDate, string sender)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataNews> listData;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                DateTime? dStartDate = null;
                DateTime? dEndDate = null; ;

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(startDate))
                {
                    if (DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dateTime))
                    {
                        dStartDate = dateTime;
                    }
                }

                if (!string.IsNullOrEmpty(endDate))
                {
                    if (DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dateTime))
                    {
                        dEndDate = dateTime;
                    }
                }

                if (dStartDate != null && dEndDate != null)
                {
                    sqlCondition += string.Format(@" AND s.dSend BETWEEN '{0} 0:00:00' AND '{1} 23:59:59' ", dStartDate?.ToString("yyyy-MM-dd", new CultureInfo("en-US")), dEndDate?.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                }
                else if (dStartDate != null)
                {
                    sqlCondition += string.Format(@" AND s.dSend > '{0} 0:00:00' ", dStartDate?.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                }
                else if (dEndDate != null)
                {
                    sqlCondition += string.Format(@" AND s.dSend < '{0} 23:59:59' ", dEndDate?.ToString("yyyy-MM-dd", new CultureInfo("en-US")));
                }

                if (!string.IsNullOrEmpty(sender)) { sqlCondition += string.Format(@" AND (e.sName LIKE N'%{0}%' OR e.sLastname LIKE N'%{0}%' OR e.sName+' '+e.sLastname LIKE N'%{0}%') ", sender.Trim()); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TSMS s LEFT JOIN TEmployees e ON s.SchoolID = e.SchoolID AND s.useradd = e.sEmp
WHERE s.SchoolID={0} AND ISNULL(s.isDel, 0)=0 {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT s.nSMS 'SmsID', s.dSend 'SendDate', e.sName+' '+e.sLastname 'Recorder'
        , (CASE WHEN s.SMSType = 0 THEN 'ส่งทันที' WHEN s.SMSType = 1 THEN 'ตั้งเวลาการส่ง' ELSE '' END) 'Type', s.SMSTitle 'Title'
        , (CASE WHEN s.SMSGroupType = 1 THEN 'รายกลุ่ม' WHEN s.SMSGroupType = 2 THEN 'รายบุคคล' ELSE '' END) 'Receiver'
        , (CASE WHEN s.SMSDuration = 0 THEN 'แจ้งประกาศกิจกรรม' WHEN s.SMSDuration = 1 THEN 'แจ้งประกาศข่าวสาร' ELSE '' END) 'Duration'
        , SUBSTRING(s.SMSDesp, 1, 55)+(CASE WHEN LEN(s.SMSDesp) > 55 THEN '...' ELSE '' END) 'News' 
        FROM TSMS s LEFT JOIN TEmployees e ON s.SchoolID = e.SchoolID AND s.useradd = e.sEmp
        WHERE s.SchoolID={1} AND ISNULL(s.isDel, 0)=0 {2} 
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityNewsList> resultFilter = en.Database.SqlQuery<EntityNewsList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataNews>();
                ListDataNews item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataNews();
                    item.no = "";
                    item.SendDate = i.SendDate?.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    item.Recorder = i.Recorder;
                    item.Type = i.Type;
                    item.Title = i.Title;
                    item.Receiver = i.Receiver;
                    item.Duration = i.Duration;
                    item.News = i.News;
                    item.action = "";
                    item.nid = i.SmsID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataNews> data = new CollectionData<ListDataNews>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + ((totalData % pageSize) == 0 ? 0 : 1);
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

    }
}