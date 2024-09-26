using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.Employees.CsCode
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

        public static string LoadEmployeeJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string empType, string studentName)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmployee> listData;


            using (JabJaiMasterEntities dbMaster = Connection.MasterEntities(ConnectionDB.Read))
            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                //var company = dbMaster.TCompanies.Where(w => w.sEntities == sEntities).FirstOrDefault();

                string masterUserID = "";
                var masterUser = dbMaster.TUsers.Where(w => w.nCompany == schoolID && w.cType == "1" && w.cDel == null).ToList();
                // dbMaster.TUsers.cType: 0 = Student, 1 = Employee

                string sqlCondition = "";
                string sqlMasterCondition = "";
                if (!string.IsNullOrEmpty(empType))
                {
                    sqlCondition += string.Format(@" AND (e.cType = '{0}')", empType);
                    //sqlMasterCondition += string.Format(@" AND (u.cType = '{0}')", empType);
                }
                if (!string.IsNullOrEmpty(studentName))
                {
                    studentName = studentName.Replace("'", "''");
                    sqlCondition += string.Format(@" AND (e.sName LIKE N'%{0}%' OR e.sLastname LIKE N'%{0}%' OR e.sName+' '+e.sLastname = N'{0}' OR i.FirstNameEn LIKE N'%{0}%' OR i.LastNameEn LIKE N'%{0}%' OR i.FirstNameEn+' '+i.LastNameEn = N'{0}')", studentName);
                    sqlMasterCondition += string.Format(@" AND (u.sName LIKE N'%{0}%' OR u.sLastname LIKE N'%{0}%' OR u.sName+' '+u.sLastname = N'{0}')", studentName);
                }

                //string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmployees e WHERE (e.cDel = '1' OR e.cDel IS NULL) {0}", sqlCondition);
                string sqlMasterQueryCount = string.Format(@"SELECT COUNT(*) FROM TUser u WHERE u.nCompany = {0} AND u.cType = '1' AND u.cDel IS NULL {1}", schoolID, sqlMasterCondition);

                // List userID(nSystemID)
                foreach (var u in masterUser)
                {
                    if (u.nSystemID != null) masterUserID += "," + u.nSystemID.Value;
                }

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT e.sEmp 'ID', e.cType 'Type', i.Code, t.titleDescription 'Title', e.sName 'FirstName', e.sLastname 'LastName', e.sPhone 'PhoneNumber', e.dBirth 'Birthday', e.dUpdate 'UpdateDate'
        , CASE WHEN es.WorkStatus = 1 THEN 'ทำงาน' WHEN es.WorkStatus = 2 THEN 'ลาออก' WHEN es.WorkStatus = 3 THEN 'พักงาน' ELSE 'ทำงาน' END 'WorkStatus'
        FROM TEmployees e
        LEFT JOIN TEmployeeInfo i ON e.sEmp = i.sEmp AND e.SchoolID = i.SchoolID
        LEFT JOIN TTitleList t ON e.sTitle = CAST(t.nTitleid AS VARCHAR(10)) AND e.SchoolID = t.SchoolID
        LEFT JOIN TEmpSalary es ON e.sEmp = es.sEmp AND e.SchoolID = es.SchoolID AND es.cDel = 0
		WHERE e.cDel IS NULL AND e.SchoolID = {5} {1} {4}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound, (string.IsNullOrEmpty(masterUserID) ? "" : string.Format(@" AND e.sEmp IN ({0})", masterUserID.Remove(0, 1))), schoolID);

                //int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                int resultMasterCount = dbMaster.Database.SqlQuery<int>(sqlMasterQueryCount).FirstOrDefault();
                List<EntityEmployee> resultFilter = en.Database.SqlQuery<EntityEmployee>(sqlQueryFilter).ToList();

                //totalData = resultCount;
                totalData = resultMasterCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmployee>();
                ListDataEmployee item;

               
                var typeList = en.TEmployeeTypes
                  .Where(o => o.SchoolID == schoolID && o.IsActive == true && o.IsDel == false)
                  .Select(o => new 
                  {
                      Id = (o.nTypeId2 ?? o.nTypeId) + "",
                      Name = o.Title,
                  })
                  .ToList();

                foreach (var i in resultFilter)
                {
                    string type = "";

                    //switch (i.Type)
                    //{
                    //    case "1": type = "บุคลากรทั่วไป"; break;
                    //    case "2": type = "ครูประจำการ"; break;
                    //    case "3": type = "บุคลากรทางการศึกษา"; break;
                    //    case "4": type = "ครูพิเศษ"; break;
                    //    case "5": type = "ครูพี่เลี้ยง"; break;
                    //    case "6": type = "ผู้บริหาร"; break;
                    //}
                    type = typeList.FirstOrDefault(o => o.Id == i.Type)?.Name + "";

                    item = new ListDataEmployee();
                    item.no = "";
                    item.Type = type;
                    item.Code = i.Code;
                    item.Title = i.Title;
                    item.FirstName = i.FirstName;
                    item.LastName = i.LastName;
                    item.PhoneNumber = i.PhoneNumber;
                    item.Birthday = (i.Birthday == null ? "" : i.Birthday.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.UpdateDate = (i.UpdateDate == null ? "" : i.UpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.WorkStatus = i.WorkStatus;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmployee> data = new CollectionData<ListDataEmployee>();
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

        public static string LoadEmpEducationJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpEducation> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (Institution LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpEducationInfo WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, StudyYear, GraduationYear, LevelID, Institution
        FROM TEmpEducationInfo e
		WHERE 1 = 1 AND e.cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpEducation> resultFilter = en.Database.SqlQuery<EntityEmpEducation>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpEducation>();
                ListDataEmpEducation item;
                foreach (var i in resultFilter)
                {
                    string levelName = "";
                    switch (i.LevelID)
                    {
                        case 1: levelName = "ต่ำกว่าประถมศึกษา"; break;
                        case 2: levelName = "ประถมศึกษา"; break;
                        case 3: levelName = "มัธยมศึกษาหรือเทียบเท่า"; break;
                        case 7: levelName = "ประกาศนียบัตรวิชาชีพ (ปวช.)"; break;
                        case 8: levelName = "ประกาศนียบัตรวิชาชีพขั้นสูง (ปวส.)"; break;
                        case 4: levelName = "ปริญญาตรี หรือเทียบเท่า"; break;
                        case 5: levelName = "ปริญญาโท"; break;
                        case 6: levelName = "ปริญญาเอก"; break;
                        case 9: levelName = "ประกาศนียบัตรบัณฑิตวิชาชีพครู (ป.บัณฑิต)"; break;
                    }

                    item = new ListDataEmpEducation();
                    item.StudyYear = i.StudyYear.ToString();
                    item.GraduationYear = i.GraduationYear.ToString();
                    item.Level = levelName;
                    item.Institution = i.Institution;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpEducation> data = new CollectionData<ListDataEmpEducation>();
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

        public static string LoadEmpFameJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpFame> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND ([Type] LIKE N'%{0}%' OR Department LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpHonor WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, [Type], Department, [Year]
        FROM TEmpHonor
		WHERE 1 = 1 AND cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpFame> resultFilter = en.Database.SqlQuery<EntityEmpFame>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpFame>();
                ListDataEmpFame item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpFame();
                    item.Type = i.Type;
                    item.Department = i.Department;
                    item.Year = i.Year.ToString();
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpFame> data = new CollectionData<ListDataEmpFame>();
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

        public static string LoadEmpTrainingJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpTraining> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (TrainingName LIKE N'%{0}%' OR CourseGroup LIKE N'%{0}%' OR Place LIKE N'%{0}%' OR Province.PROVINCE_NAME LIKE N'%{0}%' OR Country.COUNTRY_NAME LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpTraining WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, ProjectName, TrainingName, StartDate, EndDate
        FROM TEmpTraining
		WHERE 1 = 1 AND cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpTraining> resultFilter = en.Database.SqlQuery<EntityEmpTraining>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpTraining>();
                ListDataEmpTraining item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpTraining();
                    item.ProjectName = i.ProjectName;
                    item.TrainingName = i.TrainingName;
                    item.StartDate = (i.StartDate == null ? "" : i.StartDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.EndDate = (i.EndDate == null ? "" : i.EndDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpTraining> data = new CollectionData<ListDataEmpTraining>();
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

        public static string LoadEmpTOEICJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpTOEIC> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (InstitutionAnnouncement LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpTOEIC WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, TOEICScore, InstitutionAnnouncement, ExpirationDate
        FROM TEmpTOEIC
		WHERE 1 = 1 AND cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpTOEIC> resultFilter = en.Database.SqlQuery<EntityEmpTOEIC>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpTOEIC>();
                ListDataEmpTOEIC item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpTOEIC();
                    item.TOEICScore = (i.TOEICScore == null ? "" : i.TOEICScore.Value.ToString("#,0"));
                    item.InstitutionAnnouncement = i.InstitutionAnnouncement;
                    item.ExpirationDate = (i.ExpirationDate == null ? "" : i.ExpirationDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpTOEIC> data = new CollectionData<ListDataEmpTOEIC>();
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

        public static string LoadEmpAdvancementJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpAdvancement> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND ([Rank].RANK_NAME LIKE N'%{0}%' OR Job.jobDescription LIKE N'%{0}%' OR Department.departmentName LIKE N'%{0}%' OR Province.PROVINCE_NAME LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpAdvancement WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, [Rank].RANK_NAME 'Rank', Job.jobDescription 'Job', Department.departmentName 'Department', Province.PROVINCE_NAME 'Province', AdvancementDate, UpdateDate
        FROM TEmpAdvancement Advancement
        LEFT JOIN TRank [Rank] ON Advancement.RANK_ID = [Rank].RANK_ID
        LEFT JOIN TJobList Job ON Advancement.nJobid = Job.nJobid
        LEFT JOIN TDepartment Department ON Advancement.departmentId = Department.departmentId
        LEFT JOIN JabJaiMaster.dbo.province Province ON Advancement.PROVINCE_ID = Province.PROVINCE_ID
		WHERE 1 = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpAdvancement> resultFilter = en.Database.SqlQuery<EntityEmpAdvancement>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpAdvancement>();
                ListDataEmpAdvancement item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpAdvancement();
                    item.Rank = i.Rank;
                    item.Job = i.Job;
                    item.Department = i.Department;
                    item.Province = i.Province;
                    item.AdvancementDate = (i.AdvancementDate == null ? "" : i.AdvancementDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.UpdateDate = (i.UpdateDate == null ? "" : i.UpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpAdvancement> data = new CollectionData<ListDataEmpAdvancement>();
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

        public static string LoadEmpTeachingJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpTeaching> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND o.sEMP = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (p.courseCode LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TPlane p LEFT JOIN TPlanOwner o ON p.sPlaneID = o.sPlaneID WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT y.numberYear 'Year', p.nTerm 'Term'
        , (CASE   
              WHEN p.courseType = 1 THEN N'ภาษาไทย'
              WHEN p.courseType = 2 THEN N'คณิตศาสตร์'
              WHEN p.courseType = 3 THEN N'วิทยาศาสตร์'
              WHEN p.courseType = 4 THEN N'สังคมศึกษา ศาสนา และวัฒนธรรม'
              WHEN p.courseType = 5 THEN N'สุขศึกษาและพลศึกษา'
              WHEN p.courseType = 6 THEN N'ศิลปะ'
              WHEN p.courseType = 7 THEN N'การงานอาชีพและเทคโนโลยี'
              WHEN p.courseType = 8 THEN N'ภาษาต่างประเทศ'
              WHEN p.courseType = 9 THEN N'กิจกรรมพัฒนาผู้เรียน'
              WHEN p.courseType = 10 THEN N'การศึกษาค้นคว้าด้วยตนเอง'
              WHEN p.courseType = 11 THEN N'อิสลามศึกษา'
           END) 'GroupLearning'
        , p.courseCode 'SubjectCode', s.SubLevel 'Class', p.courseHour 'HourWeek'
        FROM TPlane p 
        LEFT JOIN TSubLevel s ON p.nTSubLevel = s.nTSubLevel
        LEFT JOIN TPlanOwner o ON p.sPlaneID = o.sPlaneID
        LEFT JOIN TSchedule sc ON p.sPlaneID = sc.sPlaneID
        LEFT JOIN TTermTimeTable tt ON sc.nTermTable = tt.nTermTable
        LEFT JOIN TTerm t ON tt.nTerm = t.nTerm
        LEFT JOIN TYear y ON t.nYear = y.nYear
		WHERE 1 = 1 AND p.cDel IS NULL {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpTeaching> resultFilter = en.Database.SqlQuery<EntityEmpTeaching>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpTeaching>();
                ListDataEmpTeaching item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpTeaching();
                    item.Year = i.Year.ToString();
                    item.Term = i.Term.ToString();
                    item.GroupLearning = i.GroupLearning;
                    item.SubjectCode = i.SubjectCode;
                    item.Class = i.Class;
                    item.HourWeek = (i.HourWeek == null ? "" : i.HourWeek.Value.ToString("0.00"));
                    item.action = "";
                    item.id = "";
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpTeaching> data = new CollectionData<ListDataEmpTeaching>();
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

        public static string LoadEmpProfessLicenseJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpProfessLicense> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (LicenseNo LIKE N'%{0}%' OR LicenseName LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpProfessionalLicense WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, LicenseType, LicenseNo, LicenseName, IssuedDate, [ExpireDate]
        FROM TEmpProfessionalLicense 
		WHERE 1 = 1 AND cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpProfessLicense> resultFilter = en.Database.SqlQuery<EntityEmpProfessLicense>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpProfessLicense>();
                ListDataEmpProfessLicense item;
                foreach (var i in resultFilter)
                {
                    string licenseType = "";
                    switch (i.LicenseType)
                    {
                        case 1: licenseType = "ครู"; break;
                        case 2: licenseType = "ผู้บริหารสถานศึกษา"; break;
                        case 3: licenseType = "ผู้บริหารการศึกษา"; break;
                        case 4: licenseType = "ศึกษานิเทศก์"; break;
                    }
                    item = new ListDataEmpProfessLicense();
                    item.LicenseType = licenseType;
                    item.LicenseNo = i.LicenseNo;
                    item.LicenseName = i.LicenseName;
                    item.IssuedDate = (i.IssuedDate == null ? "" : i.IssuedDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.ExpireDate = (i.ExpireDate == null ? "" : i.ExpireDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpProfessLicense> data = new CollectionData<ListDataEmpProfessLicense>();
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

        public static string LoadEmpInsigniaJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpInsignia> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (CAST([Year] AS VARCHAR(4)) LIKE N'%{0}%' OR Grade LIKE N'%{0}%' OR Position LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpInsignia WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, [Year], Grade, Position, [Date]
        FROM TEmpInsignia
		WHERE 1 = 1 AND cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpInsignia> resultFilter = en.Database.SqlQuery<EntityEmpInsignia>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpInsignia>();
                ListDataEmpInsignia item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpInsignia();
                    item.Year = i.Year.ToString();
                    item.Grade = i.Grade;
                    item.Position = i.Position;
                    item.Date = (i.Date == null ? "" : i.Date.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpInsignia> data = new CollectionData<ListDataEmpInsignia>();
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

        public static string LoadEmpFamilyJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpFamily> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (t.titleDescription LIKE N'%{0}%' OR f.FirstName LIKE N'%{0}%' OR f.LastName LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpFamily WHERE cDel=0 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT f.sEmp, f.ID, f.FamilyRelation, t.titleDescription 'Title', f.FirstName, f.LastName, f.Birthday, f.PersonalStatus
        FROM TEmpFamily f
        LEFT JOIN TTitleList t ON f.TitleID = t.nTitleid AND f.SchoolID = t.SchoolID
		WHERE 1 = 1 AND f.cDel = 0 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpFamily> resultFilter = en.Database.SqlQuery<EntityEmpFamily>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpFamily>();
                ListDataEmpFamily item;
                foreach (var i in resultFilter)
                {
                    string personalStatus = "";
                    switch (i.PersonalStatus)
                    {
                        case 1: personalStatus = "โสด"; break;
                        case 2: personalStatus = "สมรส"; break;
                        case 3: personalStatus = "หม้าย"; break;
                        case 4: personalStatus = "หย่า"; break;
                        case 5: personalStatus = "แยกกันอยู่"; break;
                    }

                    item = new ListDataEmpFamily();
                    item.FamilyRelation = i.FamilyRelation;
                    item.Title = i.Title;
                    item.FirstName = i.FirstName;
                    item.LastName = i.LastName;
                    item.Birthday = (i.Birthday == null ? "" : i.Birthday.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.PersonalStatus = personalStatus;
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpFamily> data = new CollectionData<ListDataEmpFamily>();
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

        public static string LoadEmpLeaveJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpLeave> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (CAST([Year] AS VARCHAR(4)) LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpLeave WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, [Year], Late, Sick, Errand, Lacking, ToGovernor, Holiday, Maternity, Ordain, ContinueStudy
        FROM TEmpLeave Leave
		WHERE 1 = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpLeave> resultFilter = en.Database.SqlQuery<EntityEmpLeave>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpLeave>();
                ListDataEmpLeave item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpLeave();
                    item.Year = i.Year.ToString();
                    item.Late = i.Late.ToString();
                    item.Sick = i.Sick.ToString();
                    item.Errand = i.Errand.ToString();
                    item.Lacking = i.Lacking.ToString();
                    item.ToGovernor = i.ToGovernor.ToString();
                    item.Holiday = i.Holiday.ToString();
                    item.Maternity = i.Maternity.ToString();
                    item.Ordain = i.Ordain.ToString();
                    item.ContinueStudy = i.ContinueStudy.ToString();
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpLeave> data = new CollectionData<ListDataEmpLeave>();
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

        public static string LoadEmpNameChangeJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpNameChange> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = string.Format(@" AND sEmp = {0}", empID);
                if (!string.IsNullOrEmpty(search)) { sqlCondition += string.Format(@" AND (OldFirstName LIKE N'%{0}%' OR OldLastName LIKE N'%{0}%' OR NewFirstName LIKE N'%{0}%' OR NewLastName LIKE N'%{0}%' OR ChangePlace LIKE N'%{0}%')", search); }

                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpNameChange WHERE 1 = 1 {0}", sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
		SELECT sEmp, ID, OldFirstName, OldLastName, NewFirstName, NewLastName, ChangeDate, ChangePlace, UpdateDate
        FROM TEmpNameChange Change
		WHERE 1 = 1 {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpNameChange> resultFilter = en.Database.SqlQuery<EntityEmpNameChange>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpNameChange>();
                ListDataEmpNameChange item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpNameChange();
                    item.OldFirstName = i.OldFirstName;
                    item.OldLastName = i.OldLastName;
                    item.NewFirstName = i.NewFirstName;
                    item.NewLastName = i.NewLastName;
                    item.ChangeDate = (i.ChangeDate == null ? "" : i.ChangeDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.ChangePlace = i.ChangePlace;
                    item.UpdateDate = (i.UpdateDate == null ? "" : i.UpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpNameChange> data = new CollectionData<ListDataEmpNameChange>();
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

        public static string LoadEmpHonorJsonData(int draw, int startIndex, int pageSize, string sortBy, int schoolID, int empID, string search)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataEmpHonor> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlQueryCount = string.Format(@"SELECT COUNT(*) FROM TEmpHonor WHERE EmpID = {0}", empID);

                string sqlQueryFilter = string.Format(@"
SELECT EmpID, ID, [Year], [Image], UpdateDate
FROM TEmpHonor Honor
WHERE EmpID = {0} AND (CAST([Year] AS VARCHAR(4)) LIKE '%{1}%')
ORDER BY {2}", empID, search, sortBy);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityEmpHonor> resultFilter = en.Database.SqlQuery<EntityEmpHonor>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataEmpHonor>();
                ListDataEmpHonor item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataEmpHonor();
                    item.Year = i.Year.ToString();
                    item.Image = i.Image;
                    item.UpdateDate = (i.UpdateDate == null ? "" : i.UpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataEmpHonor> data = new CollectionData<ListDataEmpHonor>();
            data.draw = draw;
            data.recordsTotal = totalData;
            data.recordsFiltered = filterData;
            data.data = listData;

            var json = JsonConvert.SerializeObject(data);

            return json;
        }

    }
}