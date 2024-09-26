using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FingerprintPayment.Transcript.CsCode
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

//        public static string LoadTranscriptJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string level, string className, string stdName)
//        {
//            JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read);

//            int totalData = 0;
//            int filterData = 0;

//            List<ListDataTranscript> listData;

//            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
//            {
//                int lowerBound = (pageIndex * pageSize) + 1;
//                int upperBound = lowerBound + pageSize;

//                string sqlCondition = "";

//                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

//                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
//                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
//                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
//                if (!string.IsNullOrEmpty(stdName)) { sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

//                string sqlQueryCount = string.Format(@"
//SELECT COUNT(*) 
//FROM (
//	SELECT G.*
//	FROM
//	(
//		SELECT u.sID, tt.titleDescription 'Title', u.sName+' '+u.sLastname 'Name', u.sStudentID 'Code', RIGHT('0000000000' + u.sStudentID, 10) 'CodeP10'
//		, CASE WHEN ISNULL(TranscriptSetNumber, '') <> '' AND ISNULL(TranscriptNumber, '') <> '' THEN 1 ELSE 0 END 'Flag'
//		, CASE WHEN sl.SubLevel IN ('ป.1', 'ป.2', 'ป.3', 'ป.4', 'ป.5', 'ป.6', 'P.1', 'P.2', 'P.3', 'P.4', 'P.5', 'P.6','G.1', 'G.2', 'G.3', 'G.4', 'G.5', 'G.6') THEN 1 WHEN sl.SubLevel IN ('ม.1', 'ม.2', 'ม.3', 'M.1', 'M.2', 'M.3','G.7', 'G.8', 'G.9') THEN 2 WHEN sl.SubLevel IN ('ม.4', 'ม.5', 'ม.6', 'M.4', 'M.5', 'M.6','G.10', 'G.11', 'G.12') THEN 3 ELSE 0 END 'Range'
//		, ISNULL(j.JsonData, '[]') 'JsonData', tsl.nTSubLevel 'Level'
//        FROM TUser u 
//        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
//        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
//        LEFT JOIN (
//			SELECT sch.sID, JsonData = '[' + STRING_AGG('{{""yearID"":' + STRING_ESCAPE(CAST(y.nYear AS VARCHAR(10)), 'json') + ', ""year"":' + STRING_ESCAPE(CAST(y.numberYear AS VARCHAR(10)), 'json') + ', ""termID"":""' + STRING_ESCAPE(sch.nTerm, 'json') + '"", ""term"":""' + STRING_ESCAPE(t.sTerm, 'json') + '"", ""levelName"":""' + STRING_ESCAPE(sl.SubLevel, 'json') + '"", ""levelID"":' + STRING_ESCAPE(CAST(sl.nTSubLevel AS VARCHAR(10)), 'json') + ', ""classID"":' + STRING_ESCAPE(CAST(sch.nTermSubLevel2 AS VARCHAR(10)), 'json') + '}}', ', ') + ']'
//			FROM TStudentClassroomHistory sch
//			LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//			LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//			LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//			LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//			WHERE sch.SchoolID = {0} AND t.cDel IS NULL
//			GROUP BY sch.sID
//		) j ON u.sID = j.sID
//        WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND t.cDel IS NULL AND u.SchoolID = {0} {1}
//	) G
//	GROUP BY sID, Title, Name, Code, CodeP10, Flag, Range, JsonData, Level
//) A ", schoolID, sqlCondition);

//                string sqlQueryFilter = string.Format(@"
//SELECT A.*
//FROM 
//(
//	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
//    FROM (
//        SELECT G.*
//		FROM
//		(
//			SELECT u.sID, tt.titleDescription 'Title', u.sName+' '+u.sLastname 'Name', u.sStudentID 'Code', RIGHT('0000000000' + u.sStudentID, 10) 'CodeP10'
//			, CASE WHEN ISNULL(TranscriptSetNumber, '') <> '' AND ISNULL(TranscriptNumber, '') <> '' THEN 1 ELSE 0 END 'Flag'
//            , CASE WHEN sl.SubLevel IN ('ป.1', 'ป.2', 'ป.3', 'ป.4', 'ป.5', 'ป.6', 'P.1', 'P.2', 'P.3', 'P.4', 'P.5', 'P.6','G.1', 'G.2', 'G.3', 'G.4', 'G.5', 'G.6') THEN 1 WHEN sl.SubLevel IN ('ม.1', 'ม.2', 'ม.3', 'M.1', 'M.2', 'M.3','G.7', 'G.8', 'G.9') THEN 2 WHEN sl.SubLevel IN ('ม.4', 'ม.5', 'ม.6', 'M.4', 'M.5', 'M.6','G.10', 'G.11', 'G.12') THEN 3 ELSE 0 END 'Range'
//			, ISNULL(j.JsonData, '[]') 'JsonData', tsl.nTSubLevel 'Level'
//            FROM TUser u 
//			LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
//			LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//			LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//			LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//			LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//			LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
//            LEFT JOIN (
//                SELECT A.sID, JsonData = '[' + STRING_AGG('{{""yearID"":' + STRING_ESCAPE(CAST(A.nYear AS VARCHAR(10)), 'json') + ', ""year"":' + STRING_ESCAPE(CAST(A.numberYear AS VARCHAR(10)), 'json') + ', ""termID"":""' + STRING_ESCAPE(A.nTerm, 'json') + '"", ""term"":""' + STRING_ESCAPE(A.sTerm, 'json') + '"", ""levelName"":""' + STRING_ESCAPE(A.SubLevel, 'json') + '"", ""levelID"":' + STRING_ESCAPE(CAST(A.nTSubLevel AS VARCHAR(10)), 'json') + ', ""classID"":' + STRING_ESCAPE(CAST(A.nTermSubLevel2 AS VARCHAR(10)), 'json') + '}}', ', ') WITHIN GROUP (ORDER BY A.numberYear DESC) + ']'
//                FROM
//                (
//                    SELECT sch.sID, y.nYear, y.numberYear, sch.nTerm, t.sTerm, sl.SubLevel, sl.nTSubLevel, sch.nTermSubLevel2
//                    FROM TStudentClassroomHistory sch
//                    LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//                    LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//                    LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//                    LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//                    WHERE sch.SchoolID = {1} AND t.cDel IS NULL AND sch.cDel = 0 
//                ) A
//                GROUP BY A.sID
//            ) j ON u.sID = j.sID
//			WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND t.cDel IS NULL AND u.SchoolID = {1} {2}
//		) G
//		GROUP BY sID, Title, Name, Code, CodeP10, Flag, Range, JsonData, Level
//    ) AS T
//) AS A
//WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

//                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
//                List<EntityTranscriptList> resultFilter = en.Database.SqlQuery<EntityTranscriptList>(sqlQueryFilter).ToList();

//                // Get term from Grade Import
//                List<EntityJsonQuery> entityJsonQueries = new List<EntityJsonQuery>();
//                int[] sIDs = resultFilter.Select(s => s.sID).ToArray();
//                using (GradeHistoryEntity.GradeHistoryEntities _dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
//                using (JabJaiSchoolGradeEntities dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
//                {
//                    var terms = en.TTerms.Where(w => w.SchoolID == schoolID && (string.IsNullOrEmpty(w.cDel) || w.cDel == "0")).ToList();
//                    var years = en.TYears.Where(w => w.SchoolID == schoolID && w.cDel == false).ToList();
//                    var planes = en.TPlanes.Where(w => w.SchoolID == schoolID  && w.cDel == null).ToList();

//                    var termGradesHistory = (from gd in _dbGradeHistory.TGradeDetailHistories
//                                      join g in _dbGradeHistory.TGradeHistories on new { gd.SchoolID, gd.nGradeId } equals new { g.SchoolID, g.nGradeId } into g_join
//                                      from g in g_join.DefaultIfEmpty()
//                                      where gd.SchoolID == schoolID && sIDs.Contains(gd.sID) && !string.IsNullOrEmpty(g.nTerm) && gd.cDel == false
//                                      group new { gd, g } by new
//                                      {
//                                          gd.sID,
//                                          g.nTerm,
//                                          gd.SchoolID,
//                                          g.sPlaneID
//                                      } into g
//                                      select new
//                                      {
//                                          g.Key.sID,
//                                          g.Key.nTerm,
//                                          g.Key.SchoolID,
//                                          g.Key.sPlaneID
//                                      }).ToList();

//                    var termGrades = (from gd in dbGrade.TGradeDetails
//                                      join g in dbGrade.TGrades on new { gd.SchoolID, gd.nGradeId } equals new { g.SchoolID, g.nGradeId } into g_join
//                                      from g in g_join.DefaultIfEmpty()
//                                      where gd.SchoolID == schoolID && sIDs.Contains(gd.sID) && !string.IsNullOrEmpty(g.nTerm) && gd.cDel == false
//                                      group new { gd, g } by new
//                                      {
//                                          gd.sID,
//                                          g.nTerm,
//                                          gd.SchoolID,
//                                          g.sPlaneID
//                                      } into g
//                                      select new
//                                      {
//                                          g.Key.sID,
//                                          g.Key.nTerm,
//                                          g.Key.SchoolID,
//                                          g.Key.sPlaneID
//                                      }).ToList();

//                    if (termGrades != null && termGradesHistory != null)
//                    {
//                        termGrades.AddRange(termGradesHistory);
//                    }

//                    termGrades = termGrades.Distinct().ToList();

//                    var groupTemp = (from tg in termGrades
//                                     join t in terms on new { tg.nTerm, tg.SchoolID } equals new { t.nTerm, t.SchoolID }
//                                     join y in years on new { nYear = Convert.ToInt32(t.nYear), t.SchoolID } equals new { y.nYear, y.SchoolID }
//                                     join p in planes on new { tg.SchoolID, sPlaneID = Convert.ToInt32(tg.sPlaneID) } equals new { p.SchoolID, p.sPlaneID } into p_join
//                                     from p in p_join.DefaultIfEmpty()
//                                     where tg.SchoolID == schoolID && sIDs.Contains(tg.sID)
//                                     group new { tg, t, y, p } by new
//                                     {
//                                         tg.sID,
//                                         y.nYear,
//                                         y.numberYear,
//                                         tg.nTerm,
//                                         t.sTerm,
//                                         nTSubLevel = p?.nTSubLevel ?? "0"
//                                     } into g
//                                     select new
//                                     {
//                                         g.Key.sID,
//                                         g.Key.nYear,
//                                         g.Key.numberYear,
//                                         g.Key.nTerm,
//                                         g.Key.sTerm,
//                                         g.Key.nTSubLevel
//                                     }).ToList();

//                    entityJsonQueries = groupTemp
//                        .GroupBy(g => g.sID)
//                        .Select(s => new EntityJsonQuery
//                        {
//                            sID = s.Key,
//                            jsonDatas = s.Where(w => w.sID == s.Key).Select(i => new JsonData
//                            {
//                                yearID = i.nYear,
//                                year = i.numberYear,
//                                termID = i.nTerm,
//                                term = i.sTerm,
//                                levelID = i.nTSubLevel
//                            }).ToList()
//                        }).ToList();
//                }

//                totalData = resultCount;
//                filterData = resultFilter.Count();

//                listData = new List<ListDataTranscript>();
//                ListDataTranscript item;
//                foreach (var i in resultFilter)
//                {
//                    item = new ListDataTranscript();
//                    item.no = "";
//                    item.Code = i.Code;
//                    item.Title = i.Title;
//                    item.Name = i.Name;
//                    item.action = "";
//                    item.sid = i.sID.ToString();
//                    item.flag = i.Flag.ToString();
//                    item.range = i.Range.ToString();
//                    item.jsonData = i.JsonData;
//                    item.level = i.Level.ToString();

//                    var jsonDataObj = entityJsonQueries.Where(w => w.sID == i.sID).FirstOrDefault();
//                    if (jsonDataObj != null)
//                    {
//                        item.jsonData2 = JsonConvert.SerializeObject(jsonDataObj.jsonDatas);
//                    }
//                    else
//                    {
//                        item.jsonData2 = "[]";
//                    }

//                    listData.Add(item);
//                }
//            }

//            CollectionData<ListDataTranscript> data = new CollectionData<ListDataTranscript>();
//            data.draw = draw;
//            data.pageIndex = pageIndex;
//            data.pageSize = pageSize;
//            data.pageCount = (totalData / pageSize) + 1;
//            data.recordsTotal = totalData;
//            data.recordsFiltered = filterData;
//            data.data = listData;

//            var json = JsonConvert.SerializeObject(data);

//            return json;
//        }

//        public static List<int> LoadTranscriptJsonData(int schoolID, string year, string level, string classId, string studentName)
//        {
//            List<int> sIDs = new List<int>();
//            using (JabJaiEntities ctx = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
//            {
//                string sqlCondition = "";

//                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
//                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
//                if (!string.IsNullOrEmpty(classId)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", classId); }
//                if (!string.IsNullOrEmpty(studentName)) { sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", studentName); }

//                string query = string.Format(@"
//SELECT u.sID, tt.titleDescription 'Title', u.sName+' '+u.sLastname 'Name', u.sStudentID 'Code', RIGHT('0000000000' + u.sStudentID, 10) 'CodeP10'
//, CASE WHEN ISNULL(TranscriptSetNumber, '') <> '' AND ISNULL(TranscriptNumber, '') <> '' THEN 1 ELSE 0 END 'Flag'
//, CASE WHEN sl.SubLevel IN ('ป.1', 'ป.2', 'ป.3', 'ป.4', 'ป.5', 'ป.6', 'P.1', 'P.2', 'P.3', 'P.4', 'P.5', 'P.6','G.1', 'G.2', 'G.3', 'G.4', 'G.5', 'G.6') THEN 1 WHEN sl.SubLevel IN ('ม.1', 'ม.2', 'ม.3', 'M.1', 'M.2', 'M.3','G.7', 'G.8', 'G.9') THEN 2 WHEN sl.SubLevel IN ('ม.4', 'ม.5', 'ม.6', 'M.4', 'M.5', 'M.6','G.10', 'G.11', 'G.12') THEN 3 ELSE 0 END 'Range'
//FROM TUser u 
//LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
//LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
//WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND t.cDel IS NULL AND u.SchoolID = {0} {1} order by ISNULL(sch.nStudentNumber,999) , u.sStudentID", schoolID, sqlCondition);
//                List<EntityTranscriptList> result = ctx.Database.SqlQuery<EntityTranscriptList>(query).ToList();

//                sIDs = result.Select(s => s.sID).Distinct().ToList();
//            }

//            return sIDs;
//        }

    }
}