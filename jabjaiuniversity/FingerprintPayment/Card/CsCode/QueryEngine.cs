using FingerprintPayment.Class;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Card.CsCode
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

        public static string LoadBackupCardJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string searchName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataBackupCard> listData;

            using(JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(searchName)) { sqlCondition += string.Format(@" AND (bch.UserName LIKE '%{0}%' OR bc.CardName LIKE '%{0}%' OR bc.BarCode LIKE '%{0}%' OR bc.NFC LIKE '%{0}%')", searchName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TBackupCard bc 
LEFT JOIN TBackupCardHistory bch ON bc.CardID = bch.CardID AND bc.SchoolID = bch.SchoolID
WHERE bc.cDel = 0 AND bc.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT bc.CardID, bc.CardName, bc.BarCode, bc.NFC, bc.Money, bch.CardHistoryID
        , (CASE bch.UserType
	        WHEN 0 THEN N'นักเรียน'
            WHEN 1 THEN N'อาจารย์'
            WHEN 2 THEN N'บุคคลภายนออก'
            ELSE NULL 
        END) 'UserType', bch.UserName, bch.BorrowingDate, bc.Insurance
        FROM TBackupCard bc 
        LEFT JOIN 
        (
	        SELECT bc0.CardID, bch0.CardHistoryID, bch0.SchoolID, bch0.UserName, bch0.BorrowingDate, bch0.UserType
	        FROM TBackupCard bc0 
	        LEFT JOIN TBackupCardHistory bch0 ON bc0.CardID = bch0.CardID AND bc0.SchoolID = bch0.SchoolID
	        WHERE bc0.SchoolID = {1} AND (bch0.BorrowingDate IS NOT NULL AND bch0.ReturnDate IS NULL)
        ) bch ON bc.CardID = bch.CardID AND bc.SchoolID = bch.SchoolID
        WHERE bc.cDel = 0 AND bc.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityBackupCardList> resultFilter = en.Database.SqlQuery<EntityBackupCardList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataBackupCard>();
                ListDataBackupCard item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataBackupCard();
                    item.no = "";
                    item.CardName = i.CardName;
                    item.BarCode = i.BarCode;
                    item.NFC = i.NFC;
                    item.Money = i.Money == null ? "-" : i.Money.Value.ToString("0.00");
                    item.UserType = string.IsNullOrEmpty(i.UserType) ? "-" : i.UserType;
                    item.UserName = string.IsNullOrEmpty(i.UserName) ? "-" : i.UserName;
                    item.BorrowingDate = (i.BorrowingDate == null ? "-" : i.BorrowingDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.cid = i.CardID.ToString();
                    item.chid = i.CardHistoryID == null ? "" : i.CardHistoryID.Value.ToString();
                    item.insurance = i.Insurance == null ? "0.00" : i.Insurance.Value.ToString("0.00");
                    listData.Add(item);
                }
            }

            CollectionData<ListDataBackupCard> data = new CollectionData<ListDataBackupCard>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + 1;
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string LoadBackupCardJsonData(int schoolID, string searchName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataBackupCard> listData;

            using(JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(searchName)) { sqlCondition += string.Format(@" AND (bch.UserName LIKE '%{0}%' OR bc.CardName LIKE '%{0}%' OR bc.BarCode LIKE '%{0}%' OR bc.NFC LIKE '%{0}%')", searchName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TBackupCard bc 
LEFT JOIN TBackupCardHistory bch ON bc.CardID = bch.CardID AND bc.SchoolID = bch.SchoolID
WHERE bc.cDel = 0 AND bc.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT T.*
    FROM (
        SELECT bc.CardID, bc.CardName, bc.BarCode, bc.NFC, bc.Money, bch.CardHistoryID
        , (CASE bch.UserType
	        WHEN 0 THEN N'นักเรียน'
            WHEN 1 THEN N'อาจารย์'
            WHEN 2 THEN N'บุคคลภายนอก'
            ELSE NULL 
        END) 'UserType', bch.UserName, bch.BorrowingDate, bc.Insurance , ISNULL( T2.CountHistory , 0) 'CountUse'
        FROM TBackupCard bc 
        LEFT JOIN 
        (
	        SELECT bc0.CardID, bch0.CardHistoryID, bch0.SchoolID, bch0.UserName, bch0.BorrowingDate, bch0.UserType
	        FROM TBackupCard bc0 
	        LEFT JOIN TBackupCardHistory bch0 ON bc0.CardID = bch0.CardID AND bc0.SchoolID = bch0.SchoolID
	        WHERE bc0.SchoolID = {0} AND (bch0.BorrowingDate IS NOT NULL AND bch0.ReturnDate IS NULL)
        ) bch ON bc.CardID = bch.CardID AND bc.SchoolID = bch.SchoolID

        LEFT JOIN 
        (
	        SELECT bc0.CardID, bc0.SchoolID  , COUNT(1) 'CountHistory'
	        FROM TBackupCard bc0 
	        JOIN TBackupCardHistory bch0 ON bc0.CardID = bch0.CardID AND bc0.SchoolID = bch0.SchoolID
	        WHERE bc0.SchoolID = {0} 
			GROUP BY bc0.CardID, bc0.SchoolID
        ) T2 ON bc.CardID = T2.CardID AND bc.SchoolID = T2.SchoolID

        WHERE bc.cDel = 0 AND bc.SchoolID = {0} {1}
    ) AS T
) AS A", schoolID, sqlCondition);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityBackupCardList> resultFilter = en.Database.SqlQuery<EntityBackupCardList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataBackupCard>();
                ListDataBackupCard item;
                            
                foreach (var i in resultFilter)
                {
                    item = new ListDataBackupCard();
                    item.no = "";
                    item.CardName = i.CardName;
                    item.BarCode = i.BarCode;
                    item.NFC = i.NFC;
                    item.Money = i.Money == null ? "-" : i.Money.Value.ToString("0.00");
                    item.UserType = string.IsNullOrEmpty(i.UserType) ? "-" : i.UserType;
                    item.UserName = string.IsNullOrEmpty(i.UserName) ? "-" : i.UserName;
                    item.BorrowingDate = (i.BorrowingDate == null ? "-" : i.BorrowingDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.cid = i.CardID.ToString();
                    item.chid = i.CardHistoryID == null ? "" : i.CardHistoryID.Value.ToString();
                    item.insurance = i.Insurance == null ? "0.00" : i.Insurance.Value.ToString("0.00");
                    item.IsRemove = (i.CountUse > 0 || i.Money > 0 ? false : true);

                    listData.Add(item);
                }
            }

            CollectionData<ListDataBackupCard> data = new CollectionData<ListDataBackupCard>();
            //data.draw = draw;
            //data.pageIndex = pageIndex;
            //data.pageSize = pageSize;
            //data.pageCount = (totalData / pageSize) + 1;
            //data.recordsTotal = totalData;
            //data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string LoadBackupCardHistoryJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string cid)
        {
           

            int totalData = 0;
            int filterData = 0;

            List<ListDataBackupCardHistory> listData;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                //string sqlCondition = "";

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TBackupCardHistory 
WHERE SchoolID = {0} AND CardID = '{1}'", schoolID, cid);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT BorrowingDate, ReturnDate
        , (CASE UserType
	        WHEN 0 THEN N'นักเรียน'
            WHEN 1 THEN N'อาจารย์'
            WHEN 2 THEN N'บุคคลภายนออก'
            ELSE N'-' 
        END) 'UserType', UserName 
        FROM TBackupCardHistory 
        WHERE SchoolID = {1} AND CardID = '{2}'
    ) AS T
) AS A
--WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, cid, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityBackupCardHistoryList> resultFilter = en.Database.SqlQuery<EntityBackupCardHistoryList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataBackupCardHistory>();
                ListDataBackupCardHistory item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataBackupCardHistory();
                    item.no = "";
                    item.BorrowingDate = i.BorrowingDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
                    item.ReturnDate = (i.ReturnDate == null ? "" : i.ReturnDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.UserType = i.UserType;
                    item.UserName = i.UserName;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataBackupCardHistory> data = new CollectionData<ListDataBackupCardHistory>();
            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + 1;
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string LoadBackupCardHistoryJsonData(int schoolID, string cid)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataBackupCardHistory> listData;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TBackupCardHistory 
WHERE SchoolID = {0} AND CardID = '{1}'", schoolID, cid);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT T.*
    FROM (
        SELECT BorrowingDate, ReturnDate
        , (CASE UserType
	        WHEN 0 THEN N'นักเรียน'
            WHEN 1 THEN N'อาจารย์'
            WHEN 2 THEN N'บุคคลภายนออก'
            ELSE N'-' 
        END) 'UserType', UserName 
        FROM TBackupCardHistory 
        WHERE SchoolID = {0} AND CardID = '{1}'
    ) AS T
) AS A ", schoolID, cid);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityBackupCardHistoryList> resultFilter = en.Database.SqlQuery<EntityBackupCardHistoryList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataBackupCardHistory>();
                ListDataBackupCardHistory item;
                foreach (var i in resultFilter.OrderBy(o => o.BorrowingDate))
                {
                    item = new ListDataBackupCardHistory();
                    item.no = "";
                    item.BorrowingDate = i.BorrowingDate.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH"));
                    item.ReturnDate = (i.ReturnDate == null ? "" : i.ReturnDate.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")));
                    item.UserType = i.UserType;
                    item.UserName = i.UserName;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataBackupCardHistory> data = new CollectionData<ListDataBackupCardHistory>();
            //data.draw = draw;
            //data.pageIndex = pageIndex;
            //data.pageSize = pageSize;
            //data.pageCount = (totalData / pageSize) + 1;
            //data.recordsTotal = totalData;
            //data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }
    }
}