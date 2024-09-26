using FingerprintPayment.Helper;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using PostgreSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using static System.Data.Entity.Infrastructure.Design.Executor;
using System.Web.Security.AntiXss;

namespace FingerprintPayment.StudentInfo.CsCode
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

        public static string LoadStudentJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataStudent> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && (w.cDel == null || w.cDel == "G")).Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && (w.cDel == null || w.cDel == "G") && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                }

                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%' OR u.sIdentification = N'{0}')", stdName.Trim()); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT u.sID, u.sName 'Name', u.sLastname 'Lastname', ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , (CASE sch.nStudentStatus
	        WHEN 0 THEN N'กำลังศึกษา'
            WHEN 1 THEN N'จำหน่าย'
            WHEN 2 THEN N'ลาออก'
            WHEN 3 THEN N'พักการเรียน'
            WHEN 4 THEN N'สำเร็จการศึกษา'
            WHEN 5 THEN N'ขาดการติดต่อ'
            WHEN 6 THEN N'พ้นสภาพ'
            WHEN 7 THEN N'นักเรียนไปโครงการ'
            ELSE N'กำลังศึกษา'
        END) 'Status', ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', sch.nTerm 'TermID'
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentList> resultFilter = en.Database.SqlQuery<EntityStudentList>(sqlQueryFilter).ToList();

                // Check finger status
                foreach (var r in resultFilter)
                {
                    var mu = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null && w.nSystemID == r.sID).FirstOrDefault();
                    if (mu != null)
                    {
                        r.FingerStatus = (mu.sFinger != null && mu.sFinger2 != null || company.sotfware == true);
                    }
                    else
                    {
                        r.FingerStatus = true;
                    }
                }

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataStudent>();
                ListDataStudent item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudent();
                    item.no = "";
                    item.No = i.No == 9999 ? "" : i.No.ToString();
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.Status = i.Status;
                    item.action = "";
                    item.sid = i.sID.ToString();
                    item.fingerStatus = i.FingerStatus.ToString();
                    item.tid = i.TermID;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudent> data = new CollectionData<ListDataStudent>();
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

        public static string LoadStudentGraduateJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
           

            int totalData = 0;
            int filterData = 0;

            List<ListDataStudent> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel != "1").Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel != "1" && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                }

                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
LEFT JOIN TStudentHIstory sh ON sch.sID = sh.sID AND sch.nTermSubLevel2 = sh.nTermSubLevel2_OLD AND sch.nTerm = sh.nTerm AND u.SchoolID = sch.SchoolID
WHERE ISNULL(u.cDel, '0') <> '1' AND (ISNULL(sch.nStudentStatus, 0) <> 0 OR sh.sID IS NOT NULL) AND (sch.cDel = 0 OR (sch.cDel = 1 AND (sch.nStudentStatus = 1 OR sch.nStudentStatus = 2))) AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT u.sID, u.sName 'Name', u.sLastname 'Lastname', ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , (CASE 
	        WHEN sch.nStudentStatus = 0 AND sh.sID IS NULL THEN N'กำลังศึกษา'
            WHEN sch.nStudentStatus = 1 THEN N'จำหน่าย'
            WHEN sch.nStudentStatus = 2 THEN N'ลาออก'
            WHEN sch.nStudentStatus = 3 THEN N'พักการเรียน'
            WHEN sch.nStudentStatus = 4 THEN N'สำเร็จการศึกษา'
            WHEN sch.nStudentStatus = 5 THEN N'ขาดการติดต่อ'
            WHEN sch.nStudentStatus = 6 THEN N'พ้นสภาพ'
            WHEN sch.nStudentStatus = 7 THEN N'นักเรียนไปโครงการ'
			WHEN sh.sID IS NOT NULL OR u.cDel = 'G' THEN N'สำเร็จการศึกษา'
            ELSE N''
        END) 'Status'
		, ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', sch.nTerm 'TermID'
		--, u.cDel, sch.nStudentStatus, (CASE WHEN sh.sID IS NOT NULL THEN 1 ELSE 0 END) 'InStudentHistory'
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN TStudentHIstory sh ON sch.sID = sh.sID AND sch.nTermSubLevel2 = sh.nTermSubLevel2_OLD AND sch.nTerm = sh.nTerm AND u.SchoolID = sch.SchoolID
        WHERE ISNULL(u.cDel, '0') <> '1' AND (ISNULL(sch.nStudentStatus, 0) <> 0 OR sh.sID IS NOT NULL) AND (sch.cDel = 0 OR (sch.cDel = 1 AND (sch.nStudentStatus = 1 OR sch.nStudentStatus = 2))) AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentList> resultFilter = en.Database.SqlQuery<EntityStudentList>(sqlQueryFilter).ToList();

                // Check finger status
                foreach (var r in resultFilter)
                {
                    var mu = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null && w.nSystemID == r.sID).FirstOrDefault();
                    if (mu != null)
                    {
                        r.FingerStatus = (mu.sFinger != null && mu.sFinger2 != null || company.sotfware == true);
                    }
                    else
                    {
                        r.FingerStatus = false;
                    }
                }

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataStudent>();
                ListDataStudent item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudent();
                    item.no = "";
                    item.No = i.No == 9999 ? "" : i.No.ToString();
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.Status = i.Status;
                    item.action = "";
                    item.sid = i.sID.ToString();
                    item.fingerStatus = i.FingerStatus.ToString();
                    item.tid = i.TermID;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudent> data = new CollectionData<ListDataStudent>();
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

        public static string LoadStudentGraduateMoveToJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
           

            int totalData = 0;
            int filterData = 0;

            List<ListDataStudent> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel != "1").Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel != "1" && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                }

                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
LEFT JOIN TStudentHIstory sh ON sch.sID = sh.sID AND sch.nTermSubLevel2 = sh.nTermSubLevel2_OLD AND sch.nTerm = sh.nTerm AND u.SchoolID = sch.SchoolID
WHERE (ISNULL(sch.nStudentStatus, 0) <> 0 OR sh.sID IS NOT NULL) AND (sch.cDel = 0 OR (sch.cDel = 1 AND (sch.nStudentStatus = 1 OR sch.nStudentStatus = 2))) AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT u.sID, ISNULL(tt.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'Name', u.sLastname 'Lastname', ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName', sl.SubLevel 'LevelName'
        , (CASE 
	        WHEN sch.nStudentStatus = 0 AND sh.sID IS NULL THEN N'กำลังศึกษา'
            WHEN sch.nStudentStatus = 1 THEN N'จำหน่าย'
            WHEN sch.nStudentStatus = 2 THEN N'ลาออก'
            WHEN sch.nStudentStatus = 3 THEN N'พักการเรียน'
            WHEN sch.nStudentStatus = 4 THEN N'สำเร็จการศึกษา'
            WHEN sch.nStudentStatus = 5 THEN N'ขาดการติดต่อ'
            WHEN sch.nStudentStatus = 6 THEN N'พ้นสภาพ'
            WHEN sch.nStudentStatus = 7 THEN N'นักเรียนไปโครงการ'
			WHEN sh.sID IS NOT NULL OR u.cDel = 'G' THEN N'สำเร็จการศึกษา'
            ELSE N''
        END) 'Status'
		, ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', sch.nTerm 'TermID', u.Note, y.numberYear 'Year'
		--, u.cDel, sch.nStudentStatus, (CASE WHEN sh.sID IS NOT NULL THEN 1 ELSE 0 END) 'InStudentHistory'
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN TStudentHIstory sh ON sch.sID = sh.sID AND sch.nTermSubLevel2 = sh.nTermSubLevel2_OLD AND sch.nTerm = sh.nTerm AND u.SchoolID = sch.SchoolID
        WHERE (ISNULL(sch.nStudentStatus, 0) <> 0 OR sh.sID IS NOT NULL) AND (sch.cDel = 0 OR (sch.cDel = 1 AND (sch.nStudentStatus = 1 OR sch.nStudentStatus = 2))) AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentList> resultFilter = en.Database.SqlQuery<EntityStudentList>(sqlQueryFilter).ToList();

                // Check finger status
                foreach (var r in resultFilter)
                {
                    var mu = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null && w.nSystemID == r.sID).FirstOrDefault();
                    if (mu != null)
                    {
                        r.FingerStatus = (mu.sFinger != null && mu.sFinger2 != null || company.sotfware == true);
                    }
                    else
                    {
                        r.FingerStatus = false;
                    }
                }

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataStudent>();
                ListDataStudent item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudent();
                    item.no = "";
                    item.No = i.No == 9999 ? "" : i.No.ToString();
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.Status = i.Status;
                    item.action = "";
                    item.sid = i.sID.ToString();
                    item.fingerStatus = i.FingerStatus.ToString();
                    item.tid = i.TermID;
                    item.Year = i.Year.ToString();
                    item.Note = i.Note;
                    item.LevelName = i.LevelName;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudent> data = new CollectionData<ListDataStudent>();
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

        public static string LoadStudentPrintQRCodeJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string userType, string level, string className, string stdName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataStudentPrintQRCode> listData;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                StudentLogic studentLogic = new StudentLogic(en);
                string term = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                string sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == userType && w.cDel == null).Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == userType && w.cDel == null && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    switch (userType)
                    {
                        case "0":
                            sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                            break;
                        case "1":
                            sqlCondition += string.Format(@" AND e.sEmp IN ({0})", string.Join(",", userMaster));
                            break;
                    }
                }

                string sqlQueryCount = "";
                string sqlQueryFilter = "";

                switch (userType)
                {
                    case "0":

                        if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                        if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                        if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                        if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

                        sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TTermSubLevel2 tsl ON u.nTermSubLevel2 = tsl.nTermSubLevel2 AND u.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.SchoolID = sch.SchoolID AND u.sID = sch.sID
WHERE u.cDel IS NULL AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                        sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT u.sID, u.sName 'Name', u.sLastname 'Lastname', ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code'
		, ISNULL(tt.titleDescription, u.sStudentTitle) 'Title'
		FROM TUser u 
        LEFT JOIN TTermSubLevel2 tsl ON u.nTermSubLevel2 = tsl.nTermSubLevel2 AND u.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN TStudentClassroomHistory sch ON u.SchoolID = sch.SchoolID AND u.SchoolID = sch.SchoolID AND u.sID = sch.sID
        WHERE u.cDel IS NULL AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                        break;
                    case "1":

                        if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (e.sName LIKE N'%{0}%' OR e.sLastname LIKE N'%{0}%' OR e.sName+' '+e.sLastname = N'{0}' OR i.FirstNameEn LIKE N'%{0}%' OR i.LastNameEn LIKE N'%{0}%' OR i.FirstNameEn+' '+i.LastNameEn = N'{0}' OR i.Code LIKE N'%{0}%')", stdName); }

                        sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TEmployees e
LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10)) AND e.SchoolID = t.SchoolID
LEFT JOIN TEmpSalary es ON e.sEmp = es.sEmp AND e.SchoolID = es.SchoolID AND es.cDel = 0
WHERE e.cDel IS NULL AND e.SchoolID = {0} {1}", schoolID, sqlCondition);

                        sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT e.sEmp 'sID', e.sName 'Name', e.sLastname 'Lastname', ISNULL(e.sEmp, 9999) 'No', i.Code 'Code'
		, ISNULL(t.titleDescription, e.sTitle) 'Title'
        FROM TEmployees e
        LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
        LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10)) AND e.SchoolID = t.SchoolID
        LEFT JOIN TEmpSalary es ON e.sEmp = es.sEmp AND e.SchoolID = es.SchoolID AND es.cDel = 0
		WHERE e.cDel IS NULL AND e.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                        break;
                }

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentPrintQRCodeList> resultFilter = en.Database.SqlQuery<EntityStudentPrintQRCodeList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataStudentPrintQRCode>();
                ListDataStudentPrintQRCode item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudentPrintQRCode();
                    item.no = "";
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.action = "";
                    item.sid = i.sID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudentPrintQRCode> data = new CollectionData<ListDataStudentPrintQRCode>();
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

        public static string LoadStudentForAPIJsonData(int draw, int pageIndex, int pageSize, string sortBy, string level, string className, string stdName, JWTToken.userData userData)
        {
           

            int totalData = 0;
            int filterData = 0;

            List<ListDataStudentForAPI> listData;

            int schoolID = userData.CompanyID;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null).Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
                        .Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                }

                // Get range date
                StudentLogic studentLogic = new StudentLogic(en);
                string currentTerm = studentLogic.GetTermId(userData);
                var termObj = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == currentTerm).FirstOrDefault();
                if (termObj != null)
                {
                    DateTime startDate = termObj.dStart.Value;

                    // find start date last date term
                    var termObj2 = en.TTerms.Where(w => w.SchoolID == schoolID && w.dEnd <= DateTime.Today).OrderByDescending(o => o.dEnd).FirstOrDefault();
                    if (termObj2 != null)
                    {
                        startDate = termObj2.dEnd.Value;
                    }
                    else
                    {
                        startDate = new DateTime(2020, 4, 20);
                    }

                    DateTime endDate = termObj.dEnd.Value;
                    //sqlCondition += string.Format(@" AND u.moveInDate BETWEEN '{0}' AND '{1}'", startDate.ToString("yyyy/MM/dd"), endDate.AddDays(1).AddSeconds(-1).ToString("yyyy/MM/dd HH:mm:ss"));
                    sqlCondition += string.Format(@" AND (u.moveInDate BETWEEN '{0}' AND '{1}' OR sch.MoveInDate BETWEEN '{0}' AND '{1}')", startDate.ToString("yyyy/MM/dd"), endDate.AddDays(1).AddSeconds(-1).ToString("yyyy/MM/dd HH:mm:ss"));
                    sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", currentTerm);
                }

                //if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                //if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", currentTerm); }
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                if (!string.IsNullOrEmpty(stdName)) { stdName = stdName.Replace("'", "''"); sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
LEFT JOIN TSendDataPSIS sd ON u.sID = sd.sID AND t.nYear = sd.nYear AND u.SchoolID = sd.SchoolID
WHERE u.cDel IS NULL AND sch.cDel = 0 AND (u.moveInDate IS NOT NULL OR sch.MoveInDate IS NOT NULL) AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code', ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', u.sName 'Name', u.sLastname 'Lastname'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , (CASE sch.nStudentStatus
	        WHEN 0 THEN N'กำลังศึกษา'
            WHEN 1 THEN N'จำหน่าย'
            WHEN 2 THEN N'ลาออก'
            WHEN 3 THEN N'พักการเรียน'
            WHEN 4 THEN N'สำเร็จการศึกษา'
            WHEN 5 THEN N'ขาดการติดต่อ'
            WHEN 6 THEN N'พ้นสภาพ'
            WHEN 7 THEN N'นักเรียนไปโครงการ'
            ELSE N'กำลังศึกษา'
        END) 'Status', u.sID, t.nYear 'YearID', sch.nTerm 'TermID', sd.SendDate, sd.StatusCode, sd.ResponseContent, sd.SendDate2, sd.StatusCode2, sd.ResponseContent2
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN TSendDataPSIS sd ON u.sID = sd.sID AND t.nYear = sd.nYear AND u.SchoolID = sd.SchoolID
        WHERE u.cDel IS NULL AND sch.cDel = 0 AND (u.moveInDate IS NOT NULL OR sch.MoveInDate IS NOT NULL) AND u.SchoolID = {1} {2} --AND u.sID NOT IN (SELECT sID FROM TSendDataPSIS WHERE SchoolID = 84 AND SendDate IS NOT NULL)
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentListForAPI> resultFilter = en.Database.SqlQuery<EntityStudentListForAPI>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataStudentForAPI>();
                ListDataStudentForAPI item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudentForAPI();
                    item.no = "";
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.Status = i.Status;
                    item.action = "";
                    item.sid = i.sID.ToString();
                    item.yid = i.YearID.ToString();
                    item.tid = i.TermID;
                    item.SendDate = (i.SendDate == null ? "" : i.SendDate.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")));
                    item.StatusCode = i.StatusCode?.ToString();
                    item.ResponseContent = i.ResponseContent;

                    if (i.StatusCode != null && i.StatusCode == 401)
                    {
                        var rc = JsonConvert.DeserializeObject<RegisResponse>(i.ResponseContent);
                        if (rc.Message == "ไม่สามารถบันทึกข้อมูลเข้าระบบได้")
                        {
                            // Get other error from regis
                            var otherErrors = GetOtherErrorFromRegis(en, schoolID, i.sID);
                            if (otherErrors.Count > 0)
                            {
                                int ro = 1;
                                List<RegisErrors> regisErrors = new List<RegisErrors>();
                                foreach (var o in otherErrors)
                                {
                                    regisErrors.Add(new RegisErrors { Message = o, RecordOrder = ro.ToString() });
                                    ro++;
                                }

                                RegisResponse regisResponse = new RegisResponse();
                                regisResponse.Message = "error";
                                regisResponse.Errors = regisErrors;

                                item.StatusCode = "400";
                                item.ResponseContent = JsonConvert.SerializeObject(regisResponse);
                            }
                        }
                    }

                    item.SendDate2 = (i.SendDate2 == null ? "" : i.SendDate2.Value.ToString("dd/MM/yyyy HH:mm", new CultureInfo("th-TH")));
                    item.StatusCode2 = i.StatusCode2?.ToString();
                    item.ResponseContent2 = i.ResponseContent2;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudentForAPI> data = new CollectionData<ListDataStudentForAPI>();
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

        private static List<string> GetOtherErrorFromRegis(JabJaiEntities ctx, int schoolID, int sID)
        {
            List<string> otherErrors = new List<string>();

            JabjaiEntity.DB.TUser user = ctx.TUser.Where(w => w.sID == sID).FirstOrDefault();
            TFamilyProfile familyProfile = ctx.TFamilyProfiles.Where(w => w.SchoolID == schoolID && w.sID == user.sID).FirstOrDefault();
            try
            {
                // Check length data
                if (user.sIdentification.Length > 13)
                {
                    otherErrors.Add("เลขประจำตัวประชาชนความยาวเกิน 13 หลัก");
                }
                if (user.sStudentID.Length > 15)
                {
                    otherErrors.Add("เลขประจำตัวนักเรียนความยาวเกิน 15 หลัก");
                }
                if (user.sName.Length > 120)
                {
                    otherErrors.Add("ชื่อจริงความยาวเกิน 120 หลัก");
                }
                if (user.sLastname.Length > 120)
                {
                    otherErrors.Add("นามสกุลความยาวเกิน 120 หลัก");
                }
                if (user.sLastname.Length > 120)
                {
                    otherErrors.Add("นามสกุลความยาวเกิน 120 หลัก");
                }
                if (user.sStudentHomeRegisterCode.Length > 11)
                {
                    otherErrors.Add("รหัสประจำบ้านความยาวเกิน 11 หลัก");
                }
                if (familyProfile.houseRegistrationNumber.Length > 30)
                {
                    otherErrors.Add("เลขที่บ้านความยาวเกิน 30 หลัก");
                }
                if (familyProfile.houseRegistrationMuu.Length > 2)
                {
                    otherErrors.Add("หมู่ที่ความยาวเกิน 2 หลัก");
                }
                if (familyProfile.houseRegistrationSoy.Length > 80)
                {
                    otherErrors.Add("ซอยความยาวเกิน 80 หลัก");
                }
                if (familyProfile.houseRegistrationRoad.Length > 80)
                {
                    otherErrors.Add("ถนนความยาวเกิน 80 หลัก");
                }
                if (familyProfile.houseRegistrationPost.Length > 5)
                {
                    otherErrors.Add("รหัสไปรษณีย์ความยาวเกิน 5 หลัก");
                }
                if (user.sPhone.Length > 20)
                {
                    otherErrors.Add("หมายเลขโทรศัพท์ความยาวเกิน 20 หลัก");
                }
                if (user.sEmail.Length > 75)
                {
                    otherErrors.Add("อีเมล์ความยาวเกิน 75 หลัก");
                }
                if (familyProfile.sFatherIdCardNumber.Length > 13)
                {
                    otherErrors.Add("รหัสบัตรประชาชนบิดาความยาวเกิน 13 หลัก");
                }
                if (familyProfile.sMotherIdCardNumber.Length > 13)
                {
                    otherErrors.Add("รหัสบัตรประชาชนมารดาความยาวเกิน 13 หลัก");
                }
            }
            catch { }

            return otherErrors;
        }

//        public static string LoadStudentForSendGradeAPIJsonData(int draw, int pageIndex, int pageSize, string sortBy, string year, string term, string level, string className, string stdName, JWTToken.userData userData)
//        {
//            // Return empty data when term & level is null
//            if (string.IsNullOrEmpty(term) || string.IsNullOrEmpty(level))
//            {
//                CollectionData<ListDataStudentForAPI> data0 = new CollectionData<ListDataStudentForAPI>();
//                data0.draw = draw;
//                data0.pageIndex = pageIndex;
//                data0.pageSize = pageSize;
//                data0.pageCount = 0;
//                data0.recordsTotal = 0;
//                data0.recordsFiltered = 0;
//                data0.data = new List<ListDataStudentForAPI>();

//                var json0 = JsonConvert.SerializeObject(data0);

//                return json0;
//            }
//            //


//            JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read);

//            int totalData = 0;
//            int filterData = 0;

//            List<ListDataStudentForAPI> listData;

//            int schoolID = userData.CompanyID;
//            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
//            using (JabJaiSchoolGradeEntities dbGrade = new JabJaiSchoolGradeEntities(Connection.StringConnectionSchoolGrade(ConnectionDB.Read)))
//            //using (GradeHistoryEntity.GradeHistoryEntities dbGradeHistory = new GradeHistoryEntity.GradeHistoryEntities(Connection.GradeHistoryDBConnectionString(ConnectionDB.Read)))
//            {
//                int lowerBound = (pageIndex * pageSize) + 1;
//                int upperBound = lowerBound + pageSize;

//                string sqlCondition = "";
//                string sqlConditionImportGradeData = "";

//                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

//                //var userMaster = new int[] { };
//                //if (string.IsNullOrEmpty(stdName))
//                //{
//                //    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null).Select(s => (int)s.nSystemID).ToArray();
//                //}
//                //else
//                //{
//                //    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname) == stdName))
//                //        .Select(s => (int)s.nSystemID).ToArray();
//                //}

//                //if (userMaster.Length > 0)
//                //{
//                //    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
//                //}

//                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
//                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
//                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
//                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
//                if (!string.IsNullOrEmpty(stdName))
//                {
//                    stdName = stdName.Replace("'", "''");
//                    sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName);

//                    // [SB-7632] Add Condition Name, Lastname and StudentID
//                    sqlConditionImportGradeData = string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName);
//                }

//                // Load student import grade data
//                string appendImportStudentRows = "";
//                List<EntityStudentListForAPI> importStudentData = new List<EntityStudentListForAPI>();
//                if (!string.IsNullOrEmpty(level) && string.IsNullOrEmpty(className))
//                {
//                    TTerm termObj = en.TTerms.Where(w => w.SchoolID == schoolID && w.nTerm == term).FirstOrDefault();
//                    TSubLevel subLevelObj = en.Database.SqlQuery<TSubLevel>(string.Format(@"SELECT * FROM TSubLevel WHERE SchoolID={0} AND nTSubLevel={1}", schoolID, !string.IsNullOrEmpty(level) ? level : "0")).FirstOrDefault();
//                    List<int> planeIDs = en.Database.SqlQuery<int>(string.Format(@"SELECT sPlaneID FROM TPlane WHERE SchoolID={0} AND nTSubLevel='{1}' AND cDel IS NULL", schoolID, level)).ToList();

//                    string sqlQueryImportData = string.Format(@"
//SELECT DISTINCT gd.sID
//FROM TGradeDetail gd LEFT JOIN TGrade g ON gd.SchoolID = g.SchoolID AND gd.nGradeId = g.nGradeId
//WHERE gd.SchoolID={0} AND gd.cDel=0 AND g.nTerm='{1}' AND g.cDel=0 {2}
//UNION
//SELECT DISTINCT gd.sID
//FROM JabjaiSchoolGradeHistory.dbo.TGradeDetailHistory gd LEFT JOIN JabjaiSchoolGradeHistory.dbo.TGradeHistory g ON gd.SchoolID = g.SchoolID AND gd.nGradeId = g.nGradeId
//WHERE gd.SchoolID={0} AND gd.cDel=0 AND g.nTerm='{1}' AND g.cDel=0 {2}"
//    , schoolID, term, (planeIDs.Count > 0 ? "AND g.sPlaneID IN (" + string.Join(", ", planeIDs) + ")" : ""));
//                    List<int> importSIDs = dbGrade.Database.SqlQuery<int>(sqlQueryImportData).ToList();

//                    sqlQueryImportData = string.Format(@"
//SELECT NULL 'No', sStudentID 'Code', t.titleDescription 'Title', sName 'Name', sLastname 'Lastname', '{0}' 'ClassName', 'ข้อมูลเกรดนำเข้า' 'Status', sID, {1} 'YearID', '{2}' 'Term'
//FROM TUser u LEFT JOIN TTitleList t ON u.SchoolID = t.SchoolID AND u.sStudentTitle = CAST(t.nTitleid AS VARCHAR(10))
//WHERE ISNULL(u.cDel, '0') <> '1' AND u.sID IN ({3}) AND u.sID NOT IN (SELECT DISTINCT sID FROM TStudentClassroomHistory sch LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID=tsl.SchoolID AND sch.nTermSubLevel2=tsl.nTermSubLevel2 WHERE sch.SchoolID={4} AND sch.nTerm='{5}' AND sch.cDel=0 AND tsl.nTSubLevel={6}) {7}"
//    , subLevelObj?.SubLevel, year, termObj?.sTerm, (importSIDs.Count > 0 ? string.Join(", ", importSIDs) : "0"), schoolID, term, level, sqlConditionImportGradeData);
//                    importStudentData = en.Database.SqlQuery<EntityStudentListForAPI>(sqlQueryImportData).ToList();

//                    foreach (var d in importStudentData)
//                    {
//                        appendImportStudentRows += string.Format(@"
//        UNION SELECT NULL 'No', '{0}' 'Code', '{1}' 'Title', '{2}' 'Name', '{3}' 'Lastname', '{4}' 'ClassName', '{5}' 'Status', {6}, {7} 'YearID', '{8}' 'Term'"
//    , d.Code, d.Title, d.Name, d.Lastname, d.ClassName, d.Status, d.sID, d.YearID, d.Term);
//                    }
//                }


//                string sqlQueryCount = string.Format(@"
//SELECT COUNT(*) + {2}
//FROM TUser u 
//LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
//LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
//WHERE ISNULL(u.cDel, '0') <> '1' AND sch.cDel = 0 AND u.SchoolID = {0} {1}", schoolID, sqlCondition, importStudentData.Count);

//                string sqlQueryFilter = string.Format(@"
//SELECT A.*
//FROM 
//(
//	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
//    FROM (
//        SELECT ISNULL(sch.nStudentNumber, 9999) 'No', u.sStudentID 'Code', ISNULL(tt.titleDescription, u.sStudentTitle) 'Title', u.sName 'Name', u.sLastname 'Lastname'
//        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
//        , (CASE sch.nStudentStatus
//	        WHEN 0 THEN N'กำลังศึกษา'
//            WHEN 1 THEN N'จำหน่าย'
//            WHEN 2 THEN N'ลาออก'
//            WHEN 3 THEN N'พักการเรียน'
//            WHEN 4 THEN N'สำเร็จการศึกษา'
//            WHEN 5 THEN N'ขาดการติดต่อ'
//            WHEN 6 THEN N'พ้นสภาพ'
//            WHEN 7 THEN N'นักเรียนไปโครงการ'
//            ELSE N'กำลังศึกษา'
//        END) 'Status', u.sID, t.nYear 'YearID', t.sTerm 'Term'
//        FROM TUser u 
//        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
//        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
//        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
//        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
//        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
//        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
//        WHERE ISNULL(u.cDel, '0') <> '1' AND sch.cDel = 0 AND u.SchoolID = {1} {2} {5}
//    ) AS T
//) AS A
//WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound, appendImportStudentRows);

//                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
//                List<EntityStudentListForAPI> resultFilter = en.Database.SqlQuery<EntityStudentListForAPI>(sqlQueryFilter).ToList();

//                int[] asID = resultFilter.Select(s => s.sID).ToArray();
//                string sIDs = string.Join(", ", asID);

//                totalData = resultCount;
//                filterData = resultFilter.Count();

//                // Get Json Plane
//                var query = string.Format(@"
//SELECT sID, MAX(SendDate) 'LastSendDate', COUNT(*) 'SubjectCount', 
//(
//	SELECT COUNT(*)
//	FROM TSendGradePSIS sg 
//	WHERE sg.SchoolID={0} AND sg.nYear={1} AND sg.nTerm='{2}' AND StatusCode=201 AND sg.sID = a.sID 
//) 'Success',
//(
//	SELECT sg.sPlaneID 'PlaneID', p.sPlaneName 'PlaneName', p.courseCode 'CourseCode', sg.SendDate, sg.StatusCode, sg.ResponseContent
//	FROM TSendGradePSIS sg 
//	LEFT JOIN TPlane p ON sg.SchoolID=p.SchoolID AND sg.sPlaneID=p.sPlaneID 
//	WHERE sg.SchoolID={0} AND sg.nYear={1} AND sg.nTerm='{2}' AND sg.sID = a.sID 
//    FOR JSON PATH
//) 'JsonPlane'
//FROM TSendGradePSIS a
//WHERE SchoolID={0} AND nYear={1} AND nTerm='{2}' AND sID IN (0 {3})
//GROUP BY nYear, nTerm, sID", schoolID, int.Parse(year), term, string.IsNullOrEmpty(sIDs) ? "" : ", " + sIDs);

//                List<EntityJsonPlane> jsonPlane = en.Database.SqlQuery<EntityJsonPlane>(query).ToList();

//                listData = new List<ListDataStudentForAPI>();
//                ListDataStudentForAPI item;
//                foreach (var i in resultFilter)
//                {
//                    item = new ListDataStudentForAPI();
//                    item.no = "";
//                    item.Code = i.Code;
//                    item.Title = i.Title;
//                    item.Name = i.Name;
//                    item.Lastname = i.Lastname;
//                    item.ClassName = i.ClassName;
//                    item.Status = i.Status;
//                    item.action = "";
//                    item.sid = i.sID.ToString();
//                    item.yid = i.YearID.ToString();
//                    item.tid = i.Term;

//                    var jp = jsonPlane.Where(w => w.sID == i.sID).FirstOrDefault();
//                    if (jp != null)
//                    {
//                        item.LastSendDate = jp.LastSendDate.ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
//                        item.SubjectCount = jp.SubjectCount.ToString();
//                        item.Success = jp.Success.ToString(); ;
//                        item.JsonPlane = jp.JsonPlane;
//                    }

//                    listData.Add(item);
//                }
//            }

//            CollectionData<ListDataStudentForAPI> data = new CollectionData<ListDataStudentForAPI>();
//            data.draw = draw;
//            data.pageIndex = pageIndex;
//            data.pageSize = pageSize;
//            data.pageCount = (totalData / pageSize) + ((totalData % pageSize) == 0 ? 0 : 1);
//            data.recordsTotal = totalData;
//            data.recordsFiltered = filterData;
//            data.data = listData;

//            var json = JsonConvert.SerializeObject(data);

//            return json;
//        }

       

        public static string LoadStudentReportFailExam01JsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string grade)
        {
           
            int totalData = 0;
            int filterData = 0;

            List<ListDataStudentReportFailExam01> listData;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                string sqlCondition2 = "";
                string sqlCondition3 = "";

                //if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND nTerm = '{0}'", term); sqlCondition2 = string.Format(@" AND g.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(grade))
                {
                    sqlCondition3 = string.Format(@" AND gd.getGradeLabel IN ('{0}') ", grade.Remove(0, 2).Replace(", ", "', '"));
                }
                else
                {
                    sqlCondition3 = " AND gd.getGradeLabel IN ('0', 'ร', 'มส', 'มผ') ";
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TGradeDetail gd 
LEFT JOIN TGrade g ON gd.SchoolID = g.SchoolID AND gd.nGradeId = g.nGradeId AND g.cDel=0
LEFT JOIN TB_StudentViews sv ON gd.SchoolID = sv.SchoolID AND gd.sID = sv.sID AND g.nTerm = sv.nTerm
LEFT JOIN TPlane p ON g.SchoolID = p.SchoolID AND g.sPlaneID = p.sPlaneID
WHERE gd.SchoolID={0} AND gd.cDel=0 {3} 
AND gd.sID IN (SELECT DISTINCT sID FROM TB_StudentViews WHERE SchoolID={0} {1}) {2}", schoolID, sqlCondition, sqlCondition2, sqlCondition3);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY Level, ClassRoom, StudentID) AS RowNumber, DENSE_RANK() OVER(PARTITION BY Level, ClassRoom ORDER BY Level, ClassRoom, StudentID) AS [No], T.*
    FROM (
        SELECT gd.sID, sv.sStudentID 'StudentID', sv.SubLevel 'Level', sv.nTSubLevel2 'ClassRoom', sv.sStudentID+'-'+sv.titleDescription+sv.sName+' '+sv.sLastname 'Name'
		, (CASE WHEN p.courseCode IS NOT NULL AND p.courseCode <> '' THEN p.courseCode+'' ELSE '' END)+'-'+p.sPlaneName+N'/ผล-'+gd.getGradeLabel 'SubjectResultGrade'
        , p.courseCode 'CourseCode'
		--, sv.numberYear, sv.sTerm, sv.nTerm
		--, sv.sStudentID, sv.nTSubLevel, sv.nTermSubLevel2, sv.numberYear, sv.sTerm, sv.nTerm, g.sPlaneID
		FROM TGradeDetail gd 
		LEFT JOIN TGrade g ON gd.SchoolID = g.SchoolID AND gd.nGradeId = g.nGradeId AND g.cDel=0
		LEFT JOIN TB_StudentViews sv ON gd.SchoolID = sv.SchoolID AND gd.sID = sv.sID AND g.nTerm = sv.nTerm
		LEFT JOIN TPlane p ON g.SchoolID = p.SchoolID AND g.sPlaneID = p.sPlaneID
		WHERE gd.SchoolID={1} AND gd.cDel=0 {6} 
		AND gd.sID IN (SELECT DISTINCT sID FROM TB_StudentViews WHERE SchoolID={1} {2}) {5}
		--ORDER BY sv.SubLevel, sv.nTSubLevel2, sv.sStudentID
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound, sqlCondition2, sqlCondition3);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentReportFailExam01> resultFilter = en.Database.SqlQuery<EntityStudentReportFailExam01>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataStudentReportFailExam01>();
                ListDataStudentReportFailExam01 item;

                foreach (var i in resultFilter)
                {
                    item = new ListDataStudentReportFailExam01();
                    item.No = i.No.ToString();
                    item.Level = i.Level;
                    item.ClassRoom = i.ClassRoom;
                    item.Name = i.Name;
                    item.SubjectResultGrade = i.SubjectResultGrade;
                    item.sid = i.sID.ToString();
                    item.rn = i.RowNumber.ToString();

                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudentReportFailExam01> data = new CollectionData<ListDataStudentReportFailExam01>();
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

        public static string LoadStudentResignationReportJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string startDate, string endDate)
        {
          
            int totalData = 0;
            int filterData = 0;

            List<ListDataStudentResignationReport> listData;

            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                {
                    bool bStartDate = DateTime.TryParseExact(startDate.Trim(), "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dStartDate);
                    bool bEndDate = DateTime.TryParseExact(endDate.Trim(), "dd/MM/yyyy", new CultureInfo("th-TH"), DateTimeStyles.None, out DateTime dEndDate);

                    if (bStartDate && bEndDate)
                    {
                        sqlCondition += string.Format(@" AND A.DropOutDate BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'", dStartDate.ToString("yyyy-MM-dd"), dEndDate.ToString("yyyy-MM-dd"));
                    }
                }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM
(
	SELECT sch.SchoolID, sch.sID, sch.nTerm, sch.nTermSubLevel2, sch.nStudentStatus, sch.MoveOutDate, t.sTerm, y.numberYear
	, ROW_NUMBER()OVER(PARTITION BY sch.sID ORDER BY y.numberYear ASC, t.sTerm ASC) 'GroupNo'
	, u.DayQuit, (CASE WHEN sch.MoveOutDate IS NULL THEN u.DayQuit ELSE sch.MoveOutDate END) 'DropOutDate', u.Note
	, u.sStudentID, ISNULL(tl.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'Fullname', sl.SubLevel, tsl.nTSubLevel2
	FROM TStudentClassroomHistory sch 
	LEFT JOIN TTerm t ON sch.SchoolID=t.SchoolID AND sch.nTerm=t.nTerm
	LEFT JOIN TYear y ON t.SchoolID=y.SchoolID AND t.nYear=y.nYear
	LEFT JOIN TUser u ON sch.SchoolID=u.SchoolID AND sch.sID=u.sID
	LEFT JOIN TTitleList tl ON u.SchoolID=tl.SchoolID AND u.sStudentTitle=CAST(tl.nTitleid AS VARCHAR(10))
	LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID=tsl.SchoolID AND sch.nTermSubLevel2=tsl.nTermSubLevel2
	LEFT JOIN TSubLevel sl ON tsl.SchoolID=sl.SchoolID AND tsl.nTSubLevel=sl.nTSubLevel
	WHERE sch.SchoolID={0} AND sch.nStudentStatus=2 AND ISNULL(sch.cDel, 0)=0
) A 
LEFT JOIN 
(
	SELECT c.SchoolId, t.nTerm, ptsl.nTermSubLevel2, c.CurriculumName, p.PlanName
	FROM TCurriculum c
	INNER JOIN TPlan p ON c.CurriculumId = p.CurriculumId AND c.SchoolId = p.SchoolID
	INNER JOIN TPlanTermSubLevel2 ptsl ON p.PlanId = ptsl.PlanId AND p.SchoolID = ptsl.SchoolID
	INNER JOIN TTerm t ON t.nYear = c.nYear
	WHERE c.SchoolId={0} AND ISNULL(c.cDel, 0)=0 AND ISNULL(p.cDel, 0)=0 AND ISNULL(ptsl.cDel, 0)=0
) B ON A.SchoolID=B.SchoolId AND A.nTerm=B.nTerm AND A.nTermSubLevel2=B.nTermSubLevel2
WHERE A.GroupNo = 1 {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY T.numberYear, T.sTerm, T.SubLevel, T.nTSubLevel2, T.No) AS RowNumber, T.*
    FROM (
        SELECT A.SubLevel, A.nTSubLevel2, A.GroupNo, A.sStudentID 'StudentID', A.Fullname, A.numberYear, A.nTerm, B.CurriculumName, B.PlanName, A.DropOutDate, A.Note, A.sTerm
        , ROW_NUMBER()OVER(PARTITION BY A.numberYear, A.sTerm, A.SubLevel, A.nTSubLevel2 ORDER BY A.sStudentID ASC) 'No', A.SubLevel+' / '+A.nTSubLevel2 'LevelClassRoom', CAST(A.numberYear AS VARCHAR(10))+' / '+A.sTerm+' / '+ISNULL(B.CurriculumName, '')+' / '+ISNULL(B.PlanName, '') 'YearTermCurriculumPlan'
        --, A.SchoolID, A.sID, A.nTermSubLevel2, A.nStudentStatus, A.sTerm, A.MoveOutDate, A.DayQuit
        FROM
        (
	        SELECT sch.SchoolID, sch.sID, sch.nTerm, sch.nTermSubLevel2, sch.nStudentStatus, sch.MoveOutDate, t.sTerm, y.numberYear
	        , ROW_NUMBER()OVER(PARTITION BY sch.sID ORDER BY y.numberYear ASC, t.sTerm ASC) 'GroupNo'
	        , u.DayQuit, (CASE WHEN sch.MoveOutDate IS NULL THEN u.DayQuit ELSE sch.MoveOutDate END) 'DropOutDate', u.Note
	        , u.sStudentID, ISNULL(tl.titleDescription, u.sStudentTitle)+u.sName+' '+u.sLastname 'Fullname', sl.SubLevel, tsl.nTSubLevel2
	        FROM TStudentClassroomHistory sch 
	        LEFT JOIN TTerm t ON sch.SchoolID=t.SchoolID AND sch.nTerm=t.nTerm
	        LEFT JOIN TYear y ON t.SchoolID=y.SchoolID AND t.nYear=y.nYear
	        LEFT JOIN TUser u ON sch.SchoolID=u.SchoolID AND sch.sID=u.sID
	        LEFT JOIN TTitleList tl ON u.SchoolID=tl.SchoolID AND u.sStudentTitle=CAST(tl.nTitleid AS VARCHAR(10))
	        LEFT JOIN TTermSubLevel2 tsl ON sch.SchoolID=tsl.SchoolID AND sch.nTermSubLevel2=tsl.nTermSubLevel2
	        LEFT JOIN TSubLevel sl ON tsl.SchoolID=sl.SchoolID AND tsl.nTSubLevel=sl.nTSubLevel
	        WHERE sch.SchoolID={1} AND sch.nStudentStatus=2 AND ISNULL(sch.cDel, 0)=0
        ) A 
        LEFT JOIN 
        (
	        SELECT c.SchoolId, t.nTerm, ptsl.nTermSubLevel2, c.CurriculumName, p.PlanName
	        FROM TCurriculum c
	        INNER JOIN TPlan p ON c.CurriculumId = p.CurriculumId AND c.SchoolId = p.SchoolID
	        INNER JOIN TPlanTermSubLevel2 ptsl ON p.PlanId = ptsl.PlanId AND p.SchoolID = ptsl.SchoolID
	        INNER JOIN TTerm t ON t.nYear = c.nYear
	        WHERE c.SchoolId={1} AND ISNULL(c.cDel, 0)=0 AND ISNULL(p.cDel, 0)=0 AND ISNULL(ptsl.cDel, 0)=0
        ) B ON A.SchoolID=B.SchoolId AND A.nTerm=B.nTerm AND A.nTermSubLevel2=B.nTermSubLevel2
        WHERE A.GroupNo = 1 {2}
        --ORDER BY A.numberYear, A.sTerm, A.SubLevel, A.nTSubLevel2
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentResignationReport> resultFilter = en.Database.SqlQuery<EntityStudentResignationReport>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataStudentResignationReport>();
                ListDataStudentResignationReport item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataStudentResignationReport();
                    item.ClassRoom = i.LevelClassRoom;
                    item.No = i.No.ToString();
                    item.StudentID = i.StudentID;
                    item.Fullname = i.Fullname;
                    item.YearTermCurriculumPlan = i.YearTermCurriculumPlan;
                    item.DropOutDate = (i.DropOutDate == null ? "" : i.DropOutDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.Note = i.Note;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataStudentResignationReport> data = new CollectionData<ListDataStudentResignationReport>();
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

        public static string LoadRequestUpdateStudentJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string level, string classId, string studentName)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataRequestUpdateStudent> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                StudentLogic studentLogic = new StudentLogic(en);
                string term = studentLogic.GetTermId(new JWTToken.userData { CompanyID = schoolID });

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sv.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(classId)) { sqlCondition += string.Format(@" AND sv.nTermSubLevel2 = {0}", classId); }
                if (!string.IsNullOrEmpty(studentName)) { studentName = studentName.Replace("'", "''"); sqlCondition += string.Format(@" AND (sv.sName LIKE N'%{0}%' OR sv.sLastname LIKE N'%{0}%' OR sv.sName+' '+sv.sLastname = N'{0}' OR sv.sStudentID LIKE N'%{0}%' OR u.sIdentification = N'{0}')", studentName.Trim()); }

                string ApproveStudent_CTE = string.Format($@"
DECLARE @SchoolID INT = {schoolID};
DECLARE @TermID VARCHAR(20) = '{term}';
WITH ApproveStudent_CTE (ID, SchoolID, StudentID, RequestDate, RequestApproveDate, ApproveDate, ApproveStatus, Section)
AS
(
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'profile' 'Section' FROM TApproveStudentProfile WHERE SchoolID = @SchoolID
	UNION
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'permanent-address' 'Section' FROM TApproveStudentPermanentAddress WHERE SchoolID = @SchoolID
	UNION
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'contact-address' 'Section' FROM TApproveStudentContactAddress WHERE SchoolID = @SchoolID
	UNION
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'father-info' 'Section' FROM TApproveStudentFatherInfo WHERE SchoolID = @SchoolID
	UNION
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'mother-info' 'Section' FROM TApproveStudentMotherInfo WHERE SchoolID = @SchoolID
	UNION
	SELECT ID, SchoolID, StudentID, DATEADD(dd, 0, DATEDIFF(dd, 0, RequestApproveDate)) 'RequestDate', RequestApproveDate, ApproveDate, ApproveStatus, 'parent-info' 'Section' FROM TApproveStudentParentInfo WHERE SchoolID = @SchoolID
)
");

                string sqlQueryCount = string.Format($@"
{ApproveStudent_CTE}
SELECT COUNT(*)
FROM
(
	SELECT SchoolID, StudentID, MAX(RequestApproveDate) 'RequestDate', MAX(ApproveDate) 'ApproveDate'
	, SUM(CASE WHEN ApproveStatus = 'pending' THEN 1 ELSE 0 END) 'Pending'
	, SUM(CASE WHEN ApproveStatus = 'approve' THEN 1 ELSE 0 END) 'Approve'
	, SUM(CASE WHEN ApproveStatus = 'reject' THEN 1 ELSE 0 END) 'Reject'
	, COUNT(*) 'All'
	FROM ApproveStudent_CTE
	GROUP BY SchoolID, StudentID, RequestDate
) A LEFT JOIN TB_StudentViews sv ON A.SchoolID = sv.SchoolID AND A.StudentID = sv.sID AND sv.nTerm = @TermID
LEFT JOIN TUser u ON A.SchoolID = u.SchoolID AND A.StudentID = u.sID
WHERE sv.sID IS NOT NULL {sqlCondition}");

                string sqlQueryFilter = string.Format($@"
{ApproveStudent_CTE}
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {sortBy}) AS RowNumber, T.*
    FROM (
        SELECT A.SchoolID, A.StudentID, A.RequestDate, A.ApproveDate
        , (CASE 
	        WHEN A.Pending = A.[All] THEN 'รออนุมัติ' 
	        WHEN A.Approve = A.[All] THEN 'อนุมัติ' 
	        WHEN A.Reject = A.[All] THEN 'ไม่อนุมัติ' 
	        WHEN 0 < A.Approve AND A.Approve < A.[All] THEN 'อนุมัติบางส่วน' 
	        ELSE '?' 
           END) 'ApproveStatus'
        , sv.sStudentID 'StudentCode', sv.SubLevel+'/'+sv.nTSubLevel2 'Classroom', sv.sName 'Name', sv.sLastname 'Lastname'
        FROM
        (
	        SELECT SchoolID, StudentID, MAX(RequestApproveDate) 'RequestDate', MAX(ApproveDate) 'ApproveDate'
	        , SUM(CASE WHEN ApproveStatus = 'pending' THEN 1 ELSE 0 END) 'Pending'
	        , SUM(CASE WHEN ApproveStatus = 'approve' THEN 1 ELSE 0 END) 'Approve'
	        , SUM(CASE WHEN ApproveStatus = 'reject' THEN 1 ELSE 0 END) 'Reject'
	        , COUNT(*) 'All'
	        FROM ApproveStudent_CTE
	        GROUP BY SchoolID, StudentID, RequestDate
        ) A LEFT JOIN TB_StudentViews sv ON A.SchoolID = sv.SchoolID AND A.StudentID = sv.sID AND sv.nTerm = @TermID
        LEFT JOIN TUser u ON A.SchoolID = u.SchoolID AND A.StudentID = u.sID
        WHERE sv.sID IS NOT NULL {sqlCondition}
    ) AS T
) AS A
WHERE RowNumber >= {lowerBound} AND RowNumber < {upperBound}");

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityRequestUpdateStudentList> resultFilter = en.Database.SqlQuery<EntityRequestUpdateStudentList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataRequestUpdateStudent>();
                ListDataRequestUpdateStudent item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataRequestUpdateStudent();
                    item.no = "";
                    item.Code = i.StudentCode;
                    item.Classroom = i.Classroom;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.RequestDate = i.RequestDate.ToString("dd/MM/yyyy<br>HH:mm", new CultureInfo("th-TH"));
                    item.ApproveDate = (i.ApproveDate == null ? "" : i.ApproveDate.Value.ToString("dd/MM/yyyy<br>HH:mm", new CultureInfo("th-TH")));
                    item.Status = i.ApproveStatus;
                    item.action = "";
                    item.sid = i.StudentID.ToString();
                    item.requestDate = i.RequestDate.ToString("yyyy-MM-dd");
                    listData.Add(item);
                }
            }

            CollectionData<ListDataRequestUpdateStudent> data = new CollectionData<ListDataRequestUpdateStudent>();
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