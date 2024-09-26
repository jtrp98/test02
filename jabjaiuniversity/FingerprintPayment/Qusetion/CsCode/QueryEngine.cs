using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JabjaiEntity.DB;
using JabjaiMainClass;
using MasterEntity;
using Newtonsoft.Json;

namespace FingerprintPayment.Qusetion.CsCode
{
    public class QueryEngine
    {
        public static string LoadStudentJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
            

            var totalData = 0;
            var filterData = 0;

            List<ListDataStudent> ListDataStudent;
            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                var sqlCondition = "";

                var company = dbMaster.TCompanies.Where(w => w.nCompany == schoolID).FirstOrDefault();

                var userMaster = new int[] { };
                if (string.IsNullOrEmpty(stdName))
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null).Select(s => (int)s.nSystemID).ToArray();
                }
                else
                {
                    userMaster = dbMaster.TUsers.Where(w => w.nCompany == company.nCompany && w.cType == "0" && w.cDel == null
                    && (w.sName.Contains(stdName) || w.sLastname.Contains(stdName) || (w.sName + " " + w.sLastname).Contains(stdName))).Select(s => (int)s.nSystemID).ToArray();
                }

                if (userMaster.Length > 0)
                {
                    sqlCondition += string.Format(@" AND u.sID IN ({0})", string.Join(",", userMaster));
                }

                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND y.nYear = {0}", int.Parse(year)); }
                if (!string.IsNullOrEmpty(term)) { sqlCondition += string.Format(@" AND sch.nTerm = '{0}'", term); }
                if (!string.IsNullOrEmpty(level)) { sqlCondition += string.Format(@" AND sl.nTSubLevel = {0}", level); }
                if (!string.IsNullOrEmpty(className)) { sqlCondition += string.Format(@" AND tsl.nTermSubLevel2 = {0}", className); }
                if (!string.IsNullOrEmpty(stdName)) { sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%')", stdName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TUser u 
LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
WHERE u.cDel IS NULL AND u.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT u.sID, u.sName 'Name', u.sLastname 'Lastname', ISNULL(u.nStudentNumber, 9999) 'No', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , (CASE u.nStudentStatus
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
        WHERE u.cDel IS NULL AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityStudentList> entityStudents = en.Database.SqlQuery<EntityStudentList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = entityStudents.Count();

                ListDataStudent = new List<ListDataStudent>();
                ListDataStudent item;


                foreach (var i in entityStudents)
                {
                    item = new ListDataStudent();

                    var cHeckSDQ = en.TQuestionnaireSDQs.Where(w => w.SchoolID == schoolID && w.sID == i.sID).FirstOrDefault();
                    if (cHeckSDQ == null) item.ValueSDQ = "0";
                    else item.ValueSDQ = "1";

                    var cHeckEQ = en.TQuestionnaireEQ.Where(w => w.SchoolID == schoolID && w.sID == i.sID).FirstOrDefault();
                    if (cHeckEQ == null) item.ValueEQ = "0";
                    else item.ValueEQ = "1";

                    item.no = "";
                    item.No = i.No;
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.Status = i.Status;
                    item.sID = i.sID;
                    item.TermID = i.TermID;
                    ListDataStudent.Add(item);
                }
            }

            CollectionData<ListDataStudent> data = new CollectionData<ListDataStudent>();

            data.draw = draw;
            data.pageIndex = pageIndex;
            data.pageSize = pageSize;
            data.pageCount = (totalData / pageSize) + ((totalData % pageSize) == 0 ? 0 : 1);
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = ListDataStudent;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

        public static string LoadReportEQJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
           

            int totalData = 0;
            int filterData = 0;

            List<ListDataReportEQ> listData;

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
                if (!string.IsNullOrEmpty(stdName)) { sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%' OR u.sIdentification = N'{0}')", stdName.Trim()); }

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
        SELECT u.SchoolID, u.sID, u.sName 'Name', u.sLastname 'Lastname', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , eq.EQ11, eq.EQ11T, eq.EQ12, eq.EQ12T, eq.EQ13, eq.EQ13T, eq.EQ21, eq.EQ21T, eq.EQ22, eq.EQ22T, eq.EQ23, eq.EQ23T, eq.EQ31, eq.EQ31T, eq.EQ32, eq.EQ32T, eq.EQ33, eq.EQ33T, eq.EQSUM, eq.EQSUMT 
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN 
        (
			SELECT A.SchoolID, A.sID
			, A.EQ11, (CASE WHEN A.EQ11 >= 19 THEN 'สูงกว่าปกติ' WHEN A.EQ11 >= 13 THEN 'ปกติ' WHEN A.EQ11 <= 12 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ11T'
			, A.EQ12, (CASE WHEN A.EQ12 >= 22 THEN 'สูงกว่าปกติ' WHEN A.EQ12 >= 16 THEN 'ปกติ' WHEN A.EQ12 <= 15 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ12T'
			, A.EQ13, (CASE WHEN A.EQ13 >= 23 THEN 'สูงกว่าปกติ' WHEN A.EQ13 >= 17 THEN 'ปกติ' WHEN A.EQ13 <= 16 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ13T'
			, A.EQ21, (CASE WHEN A.EQ21 >= 21 THEN 'สูงกว่าปกติ' WHEN A.EQ21 >= 15 THEN 'ปกติ' WHEN A.EQ21 <= 14 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ21T'
			, A.EQ22, (CASE WHEN A.EQ22 >= 20 THEN 'สูงกว่าปกติ' WHEN A.EQ22 >= 14 THEN 'ปกติ' WHEN A.EQ22 <= 13 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ22T'
			, A.EQ23, (CASE WHEN A.EQ23 >= 21 THEN 'สูงกว่าปกติ' WHEN A.EQ23 >= 15 THEN 'ปกติ' WHEN A.EQ23 <= 14 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ23T'
			, A.EQ31, (CASE WHEN A.EQ31 >= 14 THEN 'สูงกว่าปกติ' WHEN A.EQ31 >= 9 THEN 'ปกติ' WHEN A.EQ31 <= 8 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ31T'
			, A.EQ32, (CASE WHEN A.EQ32 >= 23 THEN 'สูงกว่าปกติ' WHEN A.EQ32 >= 16 THEN 'ปกติ' WHEN A.EQ32 <= 15 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ32T'
			, A.EQ33, (CASE WHEN A.EQ33 >= 22 THEN 'สูงกว่าปกติ' WHEN A.EQ33 >= 15 THEN 'ปกติ' WHEN A.EQ33 <= 14 THEN 'ต่ำกว่าปกติ' ELSE '-' END) 'EQ33T'
			, A.EQ11+A.EQ12+A.EQ13+A.EQ21+A.EQ22+A.EQ23+A.EQ31+A.EQ32+A.EQ33 'EQSUM'
			, (CASE 
				WHEN A.EQ11+A.EQ12+A.EQ13+A.EQ21+A.EQ22+A.EQ23+A.EQ31+A.EQ32+A.EQ33 < 140 THEN 'ต่ำกว่าปกติ' 
				WHEN A.EQ11+A.EQ12+A.EQ13+A.EQ21+A.EQ22+A.EQ23+A.EQ31+A.EQ32+A.EQ33 <= 170 THEN 'ปกติ' 
				WHEN A.EQ11+A.EQ12+A.EQ13+A.EQ21+A.EQ22+A.EQ23+A.EQ31+A.EQ32+A.EQ33 > 170 THEN 'สูงกว่าปกติ' 
				ELSE '-'
			   END) 'EQSUMT'
			FROM
			(
				SELECT SchoolID, sID
				, SUM(CASE WHEN QuestionLargeGroup = 1 AND QuestionSmallGroup = 1 THEN QuestionScore ELSE 0 END) 'EQ11'
				, SUM(CASE WHEN QuestionLargeGroup = 1 AND QuestionSmallGroup = 2 THEN QuestionScore ELSE 0 END) 'EQ12'
				, SUM(CASE WHEN QuestionLargeGroup = 1 AND QuestionSmallGroup = 3 THEN QuestionScore ELSE 0 END) 'EQ13'
				, SUM(CASE WHEN QuestionLargeGroup = 2 AND QuestionSmallGroup = 4 THEN QuestionScore ELSE 0 END) 'EQ21'
				, SUM(CASE WHEN QuestionLargeGroup = 2 AND QuestionSmallGroup = 5 THEN QuestionScore ELSE 0 END) 'EQ22'
				, SUM(CASE WHEN QuestionLargeGroup = 2 AND QuestionSmallGroup = 6 THEN QuestionScore ELSE 0 END) 'EQ23'
				, SUM(CASE WHEN QuestionLargeGroup = 3 AND QuestionSmallGroup = 7 THEN QuestionScore ELSE 0 END) 'EQ31'
				, SUM(CASE WHEN QuestionLargeGroup = 3 AND QuestionSmallGroup = 8 THEN QuestionScore ELSE 0 END) 'EQ32'
				, SUM(CASE WHEN QuestionLargeGroup = 3 AND QuestionSmallGroup = 9 THEN QuestionScore ELSE 0 END) 'EQ33'
				FROM TQuestionnaireEQ 
				WHERE SchoolID = {1}
				GROUP BY SchoolID, sID
			) A
        ) eq ON u.SchoolID = eq.SchoolID AND u.sID = eq.sID
        WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityReportEQ> resultFilter = en.Database.SqlQuery<EntityReportEQ>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataReportEQ>();
                ListDataReportEQ item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataReportEQ();
                    item.no = "";
                    item.Code = i.Code;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.EQ11T = i.EQ11T;
                    item.EQ21T = i.EQ21T;
                    item.EQ31T = i.EQ31T;
                    item.EQSUMT = i.EQSUMT;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataReportEQ> data = new CollectionData<ListDataReportEQ>();
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

        public static string LoadReportSDQJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string term, string level, string className, string stdName)
        {
            

            int totalData = 0;
            int filterData = 0;

            List<ListDataReportSDQ> listData;

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
                if (!string.IsNullOrEmpty(stdName)) { sqlCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}' OR u.sStudentID LIKE N'%{0}%' OR u.sIdentification = N'{0}')", stdName.Trim()); }

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
        SELECT u.SchoolID, u.sID, u.sName 'Name', u.sLastname 'Lastname', u.sStudentID 'Code'
        , sl.SubLevel + ' / ' + tsl.nTSubLevel2 'ClassName'
        , sdq.SDQ1, sdq.SDQ1T, sdq.SDQ2, sdq.SDQ2T, sdq.SDQ3, sdq.SDQ3T, sdq.SDQ4, sdq.SDQ4T, sdq.SDQ5, sdq.SDQ5T, sdq.SDQ14, sdq.SDQ14T, sdq.SDQRT 
        FROM TUser u 
        LEFT JOIN TStudentClassroomHistory sch ON u.sID = sch.sID AND u.SchoolID = sch.SchoolID
        LEFT JOIN TTermSubLevel2 tsl ON sch.nTermSubLevel2 = tsl.nTermSubLevel2 AND sch.SchoolID = tsl.SchoolID
        LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID
        LEFT JOIN TTerm t ON sch.nTerm = t.nTerm AND sch.SchoolID = t.SchoolID
        LEFT JOIN TYear y ON t.nYear = y.nYear AND t.SchoolID = y.SchoolID
        LEFT JOIN TTitleList tt ON u.sStudentTitle = CAST(tt.nTitleid AS VARCHAR(10)) AND u.SchoolID = tt.SchoolID
        LEFT JOIN 
        (
	        SELECT A.SchoolID, A.sID
	        , A.SDQ1, (CASE WHEN A.SDQ1 >= 7 THEN 'มีปัญหา' WHEN A.SDQ1 = 6 THEN 'เสี่ยง' WHEN A.SDQ1 >= 0 THEN 'ปกติ' ELSE '-' END) 'SDQ1T'
	        , A.SDQ2, (CASE WHEN A.SDQ2 >= 6 THEN 'มีปัญหา' WHEN A.SDQ2 = 5 THEN 'เสี่ยง' WHEN A.SDQ2 >= 0 THEN 'ปกติ' ELSE '-' END) 'SDQ2T'
	        , A.SDQ3, (CASE WHEN A.SDQ3 >= 7 THEN 'มีปัญหา' WHEN A.SDQ3 = 6 THEN 'เสี่ยง' WHEN A.SDQ3 >= 0 THEN 'ปกติ' ELSE '-' END) 'SDQ3T'
	        , A.SDQ4, (CASE WHEN A.SDQ4 >= 5 THEN 'มีปัญหา' WHEN A.SDQ4 = 4 THEN 'เสี่ยง' WHEN A.SDQ4 >= 0 THEN 'ปกติ' ELSE '-' END) 'SDQ4T'
	        , A.SDQ5, (CASE WHEN A.SDQ5 >= 4 THEN 'ปกติ/มีจุดแข็ง' WHEN A.SDQ5 >= 0 THEN 'เสี่ยง/ไม่มีจุดแข็ง' ELSE '-' END) 'SDQ5T'
	        , A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 'SDQ14'
	        , (CASE WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 19 THEN 'มีปัญหา' WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 17 THEN 'เสี่ยง' WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 0 THEN 'เสี่ยง/ไม่มีจุดแข็ง' ELSE '-' END) 'SDQ14T'
	        , (CASE WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 19 THEN 'มีปัญหา' WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 17 THEN 'เสี่ยง' WHEN A.SDQ1+A.SDQ2+A.SDQ3+A.SDQ4 >= 0 THEN 'เสี่ยง/ไม่มีจุดแข็ง' ELSE '-' END) 'SDQRT'
	        FROM
	        (
		        SELECT SchoolID, sID
		        , SUM(CASE WHEN QuestionGroup = 1 THEN QuestionScore ELSE 0 END) 'SDQ1'
		        , SUM(CASE WHEN QuestionGroup = 2 THEN QuestionScore ELSE 0 END) 'SDQ2'
		        , SUM(CASE WHEN QuestionGroup = 3 THEN QuestionScore ELSE 0 END) 'SDQ3'
		        , SUM(CASE WHEN QuestionGroup = 4 THEN QuestionScore ELSE 0 END) 'SDQ4'
		        , SUM(CASE WHEN QuestionGroup = 5 THEN QuestionScore ELSE 0 END) 'SDQ5'
		        , SUM(CASE WHEN QuestionGroup = 6 THEN QuestionScore ELSE 0 END) 'SDQ6'
		        FROM TQuestionnaireSDQ 
		        WHERE SchoolID = {1}
		        GROUP BY SchoolID, sID
	        ) A
        ) sdq ON u.SchoolID = sdq.SchoolID AND u.sID = sdq.sID
        WHERE (u.cDel IS NULL OR u.cDel = 'G') AND sch.cDel = 0 AND u.SchoolID = {1} {2}
    ) AS T
) AS A
WHERE RowNumber >= {3} AND RowNumber < {4}", sortBy, schoolID, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityReportSDQ> resultFilter = en.Database.SqlQuery<EntityReportSDQ>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                //var schid = ComFunction.Rot13Transform(sEntities);

                listData = new List<ListDataReportSDQ>();
                ListDataReportSDQ item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataReportSDQ();
                    item.no = "";
                    item.Code = i.Code;
                    item.Name = i.Name;
                    item.Lastname = i.Lastname;
                    item.ClassName = i.ClassName;
                    item.SDQ1T = i.SDQ1T;
                    item.SDQ2T = i.SDQ2T;
                    item.SDQ3T = i.SDQ3T;
                    item.SDQ4T = i.SDQ4T;
                    item.SDQ5T = i.SDQ5T;
                    item.SDQ14T = i.SDQ14T;
                    item.SDQRT = i.SDQRT;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataReportSDQ> data = new CollectionData<ListDataReportSDQ>();
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