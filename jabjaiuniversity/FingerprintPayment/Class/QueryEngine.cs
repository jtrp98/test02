using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Class
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

        public static string LoadLearningCenterJsonData(int draw, int pageIndex, int pageSize, string sortBy, string search, string learningType, int SchoolId)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataLearningCenter> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = " AND SchoolID = " + SchoolId;
                //if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (Name LIKE '%{0}%' OR Detail LIKE '%{0}%' OR [Admin] LIKE '%{0}%')", search); }
                if (!string.IsNullOrEmpty(learningType)) { sqlCondition += string.Format(@" AND ([Type] = {0})", learningType); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TLearningCenter WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT ID, [Type], Name, Detail, [Admin]
		FROM TLearningCenter
		WHERE 1 = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityLearningCenter> resultFilter = en.Database.SqlQuery<EntityLearningCenter>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataLearningCenter>();
                ListDataLearningCenter item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataLearningCenter();
                    item.no = "";
                    item.Type = (i.Type == 1 ? "แหล่งเรียนรู้ภายใน" : "แหล่งเรียนรู้ภายนอก");
                    item.Name = i.Name;
                    item.Detail = i.Detail;
                    item.Admin = i.Admin;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataLearningCenter> data = new CollectionData<ListDataLearningCenter>();
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

        public static string LoadVisitHouseJsonData(int draw, int pageIndex, int pageSize, string sortBy, string search, string year, string term, string subLevel, string termSubLevel, int SchoolId)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataVisitHouse> listData;

            string sEntities = (string)HttpContext.Current.Session["sEntities"];
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(sEntities, ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = " AND [User].SchoolID = " + SchoolId;
                //string sqlCondition = "";
                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND [Year].nYear = {0}", year); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND His.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(subLevel)) { sqlCondition += string.Format(@" AND SubLevel.nTSubLevel = {0}", subLevel); }
                if (!string.IsNullOrEmpty(termSubLevel)) { sqlCondition += string.Format(@" AND TermSubLevel.nTermSubLevel2 = {0}", termSubLevel); }
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND ([User].sName LIKE '%{0}%' OR [User].sLastname LIKE '%{0}%' OR [User].sName+' '+[User].sLastname = '{0}' OR [User].sStudentID LIKE '%{0}%')", search); }

                if (!string.IsNullOrEmpty(sqlCondition)) { sqlCondition = string.Format(@" AND ({0})", sqlCondition.Remove(0, 4)); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*)
FROM TUser [User]
LEFT JOIN TStudentClassroomHistory His ON [User].sID = His.sID
LEFT JOIN TTerm Term ON His.nTerm = Term.nTerm
LEFT JOIN TYear [Year] ON Term.nYear = [Year].nYear
LEFT JOIN TTermSubLevel2 TermSubLevel ON [User].nTermSubLevel2 = TermSubLevel.nTermSubLevel2
LEFT JOIN TSubLevel SubLevel ON TermSubLevel.nTSubLevel = SubLevel.nTSubLevel
LEFT JOIN TVisitHouse Visit ON [User].[sID] = Visit.sID
WHERE [User].cDel IS NULL {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT [Year].numberYear 'Year', [User].[sID] 'sID', Visit.VisitHouseID 'vID', TermSubLevel.nTermSubLevel2 'Term', [User].sName 'FirstName', [User].sLastname 'LastName', [User].sStudentID 'StudentCode', Visit.StampDate, Visit.StampTime
        FROM TUser [User]
        LEFT JOIN TStudentClassroomHistory His ON [User].sID = His.sID
        LEFT JOIN TTerm Term ON His.nTerm = Term.nTerm
        LEFT JOIN TYear [Year] ON Term.nYear = [Year].nYear
        LEFT JOIN TTermSubLevel2 TermSubLevel ON [User].nTermSubLevel2 = TermSubLevel.nTermSubLevel2
        LEFT JOIN TSubLevel SubLevel ON TermSubLevel.nTSubLevel = SubLevel.nTSubLevel
        LEFT JOIN TVisitHouse Visit ON [User].[sID] = Visit.sID
        WHERE [User].cDel IS NULL {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityVisitHouse> resultFilter = en.Database.SqlQuery<EntityVisitHouse>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataVisitHouse>();
                ListDataVisitHouse item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataVisitHouse();
                    item.no = "";
                    item.FirstName = i.FirstName;
                    item.LastName = i.LastName;
                    item.StudentCode = i.StudentCode;
                    item.StampDate = (i.StampDate == null ? "" : i.StampDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.sid = i.sID.ToString();
                    item.year = i.Year.ToString();
                    item.term = i.Term.ToString();
                    item.vid = i.vID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataVisitHouse> data = new CollectionData<ListDataVisitHouse>();
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

        public static string LoadMailJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string search)
        {
            
            int totalData = 0;
            int filterData = 0;

            List<ListDataElectronicMail> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (ml.Title LIKE '%{0}%' OR ml.MailContent LIKE '%{0}%')", search.Trim()); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM [JabJai-EduZone].[dbo].TSchoolMailList sml 
LEFT JOIN [JabJai-EduZone].[dbo].TMailList ml ON sml.AgencyID = ml.AgencyID AND sml.MailID = ml.MailID
WHERE sml.SchoolID = {0} AND ml.IsDel = 0 {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT ml.MailID, ml.AgencyID, ml.Title, ml.MailContent, ml.MailDate
        , (SELECT FileUrl 'fileUrl' FROM [JabJai-EduZone].[dbo].TAttachedFile af WHERE af.AgencyID = ml.AgencyID AND af.MailID = ml.MailID AND af.IsDel = 0 FOR JSON AUTO) 'Files' 
        , (SELECT sml.SchoolID 'schoolID', sml.sCompany 'schoolName' FROM (SELECT sml.SchoolID, c.sCompany, sml.AgencyID, sml.MailID, sml.IsDel FROM [JabJai-EduZone].[dbo].TSchoolMailList sml LEFT JOIN JabjaiMasterSingleDB.dbo.TCompany c ON sml.SchoolID = c.nCompany) sml WHERE sml.AgencyID = ml.AgencyID AND sml.MailID = ml.MailID AND sml.IsDel = 0 FOR JSON AUTO) 'Schools'
        FROM [JabJai-EduZone].[dbo].TSchoolMailList sml 
		LEFT JOIN [JabJai-EduZone].[dbo].TMailList ml ON sml.AgencyID = ml.AgencyID AND sml.MailID = ml.MailID
        WHERE sml.SchoolID = {1} AND ml.IsDel = 0 {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityElectronicMail> resultFilter = en.Database.SqlQuery<EntityElectronicMail>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataElectronicMail>();
                ListDataElectronicMail item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataElectronicMail();
                    item.no = "";
                    item.Title = i.Title;
                    item.MailContent = ComFunction.StripHTML(i.MailContent).TruncateAtWord(200);
                    item.MailDate = (i.MailDate == null ? "" : i.MailDate.Value.ToString("HH:mm", new CultureInfo("th-TH")));
                    item.mid = i.MailID.ToString();
                    item.aid = i.AgencyID.ToString();
                    item.files = i.Files;
                    item.schools = i.Schools;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataElectronicMail> data = new CollectionData<ListDataElectronicMail>();
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