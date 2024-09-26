using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.AssetManagement.CsCode
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

        public static string LoadCategoryJsonData(int draw, int pageIndex, int pageSize, string sortBy, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataCategory> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (Code LIKE '%{0}%' OR Category LIKE '%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TAssetCategory WHERE Flag = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT ID, Code, Category
        FROM TAssetCategory
		WHERE Flag = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityCategory> resultFilter = en.Database.SqlQuery<EntityCategory>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataCategory>();
                ListDataCategory item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataCategory();
                    item.no = "";
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataCategory> data = new CollectionData<ListDataCategory>();
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

        public static string LoadProductJsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchCat, string searchProd)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataProduct> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchCat)) { sqlCondition += string.Format(@" AND (ac.Code LIKE '%{0}%' OR ac.Category LIKE '%{0}%')", searchCat); }
                if (!string.IsNullOrEmpty(searchProd)) { sqlCondition += string.Format(@" AND (ap.[Type] LIKE '%{0}%' OR ap.Product LIKE '%{0}%')", searchProd); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetProduct ap 
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON ap.UnitID = u.ID
WHERE ap.Flag = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT ac.Code, ac.Category, ap.ID, ap.[Type], ap.Product, u.Unit
        FROM TAssetProduct ap 
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ap.UnitID = u.ID
        WHERE ap.Flag = 1  {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityProduct> resultFilter = en.Database.SqlQuery<EntityProduct>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataProduct>();
                ListDataProduct item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataProduct();
                    item.no = "";
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.Type = i.Type;
                    item.Product = i.Product;
                    item.Unit = i.Unit;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataProduct> data = new CollectionData<ListDataProduct>();
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

        public static string LoadGetJsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchCat, string searchRec, string searchDate)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataGet> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchCat)) { sqlCondition += string.Format(@" AND (ac.Category LIKE '%{0}%')", searchCat); }
                if (!string.IsNullOrEmpty(searchRec)) { sqlCondition += string.Format(@" AND (e.sName LIKE '%{0}%' OR e.sLastname LIKE '%{0}%')", searchRec); }
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime date = DateTime.ParseExact(searchDate, "dd/MM/yyyy", new CultureInfo("th-TH"));
                    sqlCondition += string.Format(@" AND (ag.DateStamp BETWEEN CONVERT(DATETIME, '{0:yyyy-MM-dd}') AND CONVERT(DATETIME, '{0:yyyy-MM-dd} 23:59:59:998'))", date);
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetGet ag
LEFT JOIN TAssetProduct ap ON ag.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON ag.UnitID = u.ID
LEFT JOIN TDepartment d ON ag.DepID = d.departmentId
LEFT JOIN TEmployees e ON ag.Receiver = e.sEmp
WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT ag.Year, ag.ID, ag.DateStamp, ac.Code, ac.Category, ap.Product, ag.Amount, u.Unit, ag.[Source], d.departmentName 'Department', e.sName+' '+e.sLastname 'Receiver'
        FROM TAssetGet ag
        LEFT JOIN TAssetProduct ap ON ag.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ag.UnitID = u.ID
        LEFT JOIN TDepartment d ON ag.DepID = d.departmentId
        LEFT JOIN TEmployees e ON ag.Receiver = e.sEmp
        WHERE 1 = 1  {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityGet> resultFilter = en.Database.SqlQuery<EntityGet>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataGet>();
                ListDataGet item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataGet();
                    item.no = "";
                    item.DateStamp = (i.DateStamp == null ? "" : i.DateStamp.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")));
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.Source = i.Source;
                    item.Department = i.Department;
                    item.Receiver = i.Receiver;
                    item.action = "";
                    item.year = i.Year.ToString();
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataGet> data = new CollectionData<ListDataGet>();
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

        public static string LoadWithdrawJsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchDate)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataWithdraw> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime date = DateTime.ParseExact(searchDate, "dd/MM/yyyy", new CultureInfo("th-TH"));
                    sqlCondition += string.Format(@" AND (aw.DateStamp BETWEEN CONVERT(DATETIME, '{0:yyyy-MM-dd}') AND CONVERT(DATETIME, '{0:yyyy-MM-dd} 23:59:59:998'))", date);
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetWithdraw aw
LEFT JOIN TAssetProduct ap ON aw.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON aw.UnitID = u.ID
LEFT JOIN TDepartment d ON aw.DepID = d.departmentId
LEFT JOIN TEmployees e ON aw.Receiver = e.sEmp
WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT aw.Year, aw.ID, aw.DateStamp, ac.Code, (CASE aw.TransactionType WHEN '1' THEN 'เบิก' WHEN '2' THEN 'ยืม' END) 'TransType', ap.Product, aw.Amount, u.Unit, d.departmentName 'Department', e.sName+' '+e.sLastname 'Receiver'
        FROM TAssetWithdraw aw
        LEFT JOIN TAssetProduct ap ON aw.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON aw.UnitID = u.ID
        LEFT JOIN TDepartment d ON aw.DepID = d.departmentId
        LEFT JOIN TEmployees e ON aw.Receiver = e.sEmp
        WHERE 1 = 1  {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityWithdraw> resultFilter = en.Database.SqlQuery<EntityWithdraw>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataWithdraw>();
                ListDataWithdraw item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataWithdraw();
                    item.no = "";
                    item.DateStamp = (i.DateStamp == null ? "" : i.DateStamp.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")));
                    item.Code = i.Code;
                    item.TransType = i.TransType;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.Department = i.Department;
                    item.Receiver = i.Receiver;
                    item.action = "";
                    item.year = i.Year.ToString();
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataWithdraw> data = new CollectionData<ListDataWithdraw>();
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

        public static string LoadCuttingJsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchDate)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataCutting> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime date = DateTime.ParseExact(searchDate, "dd/MM/yyyy", new CultureInfo("th-TH"));
                    sqlCondition += string.Format(@" AND (act.DateStamp BETWEEN CONVERT(DATETIME, '{0:yyyy-MM-dd}') AND CONVERT(DATETIME, '{0:yyyy-MM-dd} 23:59:59:998'))", date);
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetCutting act
LEFT JOIN TAssetProduct ap ON act.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON act.UnitID = u.ID
LEFT JOIN TDepartment d ON act.DepID = d.departmentId
LEFT JOIN TEmployees e ON act.Receiver = e.sEmp
WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT act.Year, act.ID, act.DateStamp, ac.Code, ac.Category, ap.Product, act.Amount, u.Unit, d.departmentName 'Department', e.sName+' '+e.sLastname 'Receiver'
        FROM TAssetCutting act
        LEFT JOIN TAssetProduct ap ON act.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON act.UnitID = u.ID
        LEFT JOIN TDepartment d ON act.DepID = d.departmentId
        LEFT JOIN TEmployees e ON act.Receiver = e.sEmp
        WHERE 1 = 1  {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityCutting> resultFilter = en.Database.SqlQuery<EntityCutting>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataCutting>();
                ListDataCutting item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataCutting();
                    item.no = "";
                    item.DateStamp = (i.DateStamp == null ? "" : i.DateStamp.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")));
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.Department = i.Department;
                    item.Receiver = i.Receiver;
                    item.action = "";
                    item.year = i.Year.ToString();
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataCutting> data = new CollectionData<ListDataCutting>();
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

        public static string LoadTransferJsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchDate)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataTransfer> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchDate))
                {
                    DateTime date = DateTime.ParseExact(searchDate, "dd/MM/yyyy", new CultureInfo("th-TH"));
                    sqlCondition += string.Format(@" AND (at.DateStamp BETWEEN CONVERT(DATETIME, '{0:yyyy-MM-dd}') AND CONVERT(DATETIME, '{0:yyyy-MM-dd} 23:59:59:998'))", date);
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetTransfer at
LEFT JOIN TAssetProduct ap ON at.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON at.UnitID = u.ID
LEFT JOIN TDepartment dr ON at.DepIDRequest = dr.departmentId
LEFT JOIN TDepartment dt ON at.DepIDTransfer = dt.departmentId
WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT at.Year, at.ID, at.DateStamp, ac.Code, ap.Product, at.Amount, u.Unit, dr.departmentName 'DepRequest', dt.departmentName 'DepTransfer'
        FROM TAssetTransfer at
        LEFT JOIN TAssetProduct ap ON at.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON at.UnitID = u.ID
        LEFT JOIN TDepartment dr ON at.DepIDRequest = dr.departmentId
        LEFT JOIN TDepartment dt ON at.DepIDTransfer = dt.departmentId
        WHERE 1 = 1  {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityTransfer> resultFilter = en.Database.SqlQuery<EntityTransfer>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataTransfer>();
                ListDataTransfer item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataTransfer();
                    item.no = "";
                    item.DateStamp = (i.DateStamp == null ? "" : i.DateStamp.Value.ToString("dd/MM/yy", new CultureInfo("th-TH")));
                    item.Code = i.Code;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.DepRequest = i.DepRequest;
                    item.DepTransfer = i.DepTransfer;
                    item.action = "";
                    item.year = i.Year.ToString();
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataTransfer> data = new CollectionData<ListDataTransfer>();
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


        /// 
        /// Report
        /// 

        public static string LoadReport01JsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchYear, string searchCategory)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataReport01> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition1 = "";
                string sqlCondition2 = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition1 += string.Format(@" AND ([Year] = {0})", searchYear); }
                if (!string.IsNullOrEmpty(searchCategory)) { sqlCondition2 += string.Format(@" AND (ac.ID = {0})", searchCategory); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM (
	SELECT ProdID, DepID, SUM(Balance) 'Balance' 
	FROM TAssetTransaction 
	WHERE 1 = 1 {0} 
	GROUP BY ProdID, DepID 
) at
LEFT JOIN TAssetProduct ap ON at.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON ap.UnitID = u.ID
LEFT JOIN TDepartment dr ON at.DepID = dr.departmentId
WHERE 1 = 1 {1}", sqlCondition1, sqlCondition2);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT at.ProdID, ac.Code, ac.Category, ap.Product, at.Balance, u.Unit, dr.departmentName 'Department'
        FROM (
	        SELECT ProdID, DepID, SUM(Balance) 'Balance' 
	        FROM TAssetTransaction 
	        WHERE 1 = 1 {1}
	        GROUP BY ProdID, DepID
        ) at
        LEFT JOIN TAssetProduct ap ON at.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ap.UnitID = u.ID
        LEFT JOIN TDepartment dr ON at.DepID = dr.departmentId
        WHERE 1 = 1 {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, sqlCondition1, sqlCondition2, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityReport01> resultFilter = en.Database.SqlQuery<EntityReport01>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataReport01>();
                ListDataReport01 item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataReport01();
                    item.no = "";
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.Department = i.Department;
                    item.id = i.ProdID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataReport01> data = new CollectionData<ListDataReport01>();
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
        public static List<EntityReport01> LoadReport01ListData(string sortIndex, string orderDir, string searchYear, string searchCategory)
        {
            List<EntityReport01> resultFilter;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                string sqlCondition1 = "";
                string sqlCondition2 = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition1 += string.Format(@" AND ([Year] = {0})", searchYear); }
                if (!string.IsNullOrEmpty(searchCategory)) { sqlCondition2 += string.Format(@" AND (ac.ID = {0})", searchCategory); }

                string sortBy = "ProdID";
                switch (sortIndex)
                {
                    case "1": sortBy = "Code"; break;
                    case "2": sortBy = "Category"; break;
                    case "3": sortBy = "Product"; break;
                    case "4": sortBy = "Amount"; break;
                    case "5": sortBy = "Unit"; break;
                    case "6": sortBy = "Department"; break;
                }
                sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT at.ProdID, ac.Code, ac.Category, ap.Product, at.Balance, u.Unit, dr.departmentName 'Department'
        FROM (
	        SELECT ProdID, DepID, SUM(Balance) 'Balance' 
	        FROM TAssetTransaction 
	        WHERE 1 = 1 {1}
	        GROUP BY ProdID, DepID
        ) at
        LEFT JOIN TAssetProduct ap ON at.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ap.UnitID = u.ID
        LEFT JOIN TDepartment dr ON at.DepID = dr.departmentId
        WHERE 1 = 1 {2}
    ) AS T
) AS A", sortBy, sqlCondition1, sqlCondition2);

                resultFilter = en.Database.SqlQuery<EntityReport01>(sqlQueryFilter).ToList();
            }

            return resultFilter;
        }

        public static string LoadReport02JsonData(int draw, int pageIndex, int pageSize, string sortBy, string searchYear, string searchCategory)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataReport02> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition += string.Format(@" AND (aw.[Year] = {0})", searchYear); }
                if (!string.IsNullOrEmpty(searchCategory)) { sqlCondition += string.Format(@" AND (ac.ID = {0})", searchCategory); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TAssetWithdraw aw
LEFT JOIN TAssetProduct ap ON aw.ProdID = ap.ID
LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
LEFT JOIN TUnit u ON ap.UnitID = u.ID
LEFT JOIN TDepartment dr ON aw.DepID = dr.departmentId
LEFT JOIN TEmployees e ON aw.Receiver = e.sEmp
WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT CAST(aw.[Year] AS NVARCHAR(4)) + RIGHT('0000000000' + CAST(aw.ID AS NVARCHAR(10)), 10) 'YearID', ac.Code, ac.Category, ap.Product, aw.Amount, u.Unit, dr.departmentName 'Department', aw.ResponsibleBy, e.sName+' '+e.sLastname 'Receiver', aw.DocumentNo
        FROM TAssetWithdraw aw
        LEFT JOIN TAssetProduct ap ON aw.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ap.UnitID = u.ID
        LEFT JOIN TDepartment dr ON aw.DepID = dr.departmentId
        LEFT JOIN TEmployees e ON aw.Receiver = e.sEmp
        WHERE 1 = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityReport02> resultFilter = en.Database.SqlQuery<EntityReport02>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataReport02>();
                ListDataReport02 item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataReport02();
                    item.no = "";
                    item.Code = i.Code;
                    item.Category = i.Category;
                    item.Product = i.Product;
                    item.Amount = i.Amount;
                    item.Unit = i.Unit;
                    item.Department = i.Department;
                    item.ResponsibleBy = i.ResponsibleBy;
                    item.Receiver = i.Receiver;
                    item.DocumentNo = i.DocumentNo;
                    item.YearID = i.YearID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataReport02> data = new CollectionData<ListDataReport02>();
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
        public static List<EntityReport02> LoadReport02ListData(string sortIndex, string orderDir, string searchYear, string searchCategory)
        {
            List<EntityReport02> resultFilter;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition += string.Format(@" AND (aw.[Year] = {0})", searchYear); }
                if (!string.IsNullOrEmpty(searchCategory)) { sqlCondition += string.Format(@" AND (ac.ID = {0})", searchCategory); }

                string sortBy = "YearID";
                switch (sortIndex)
                {
                    case "1": sortBy = "Code"; break;
                    case "2": sortBy = "Category"; break;
                    case "3": sortBy = "Product"; break;
                    case "4": sortBy = "Amount"; break;
                    case "5": sortBy = "Unit"; break;
                    case "6": sortBy = "Department"; break;
                    case "7": sortBy = "ResponsibleBy"; break;
                    case "8": sortBy = "Receiver"; break;
                    case "9": sortBy = "DocumentNo"; break;
                }
                sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT CAST(aw.[Year] AS NVARCHAR(4)) + RIGHT('0000000000' + CAST(aw.ID AS NVARCHAR(10)), 10) 'YearID', ac.Code, ac.Category, ap.Product, aw.Amount, u.Unit, dr.departmentName 'Department', aw.ResponsibleBy, e.sName+' '+e.sLastname 'Receiver', aw.DocumentNo
        FROM TAssetWithdraw aw
        LEFT JOIN TAssetProduct ap ON aw.ProdID = ap.ID
        LEFT JOIN TAssetCategory ac ON ap.CatID = ac.ID
        LEFT JOIN TUnit u ON ap.UnitID = u.ID
        LEFT JOIN TDepartment dr ON aw.DepID = dr.departmentId
        LEFT JOIN TEmployees e ON aw.Receiver = e.sEmp
        WHERE 1 = 1 {1}
    ) AS T
) AS A", sortBy, sqlCondition);

                resultFilter = en.Database.SqlQuery<EntityReport02>(sqlQueryFilter).ToList();
            }

            return resultFilter;
        }

    }
}