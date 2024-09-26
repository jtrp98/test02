using FingerprintPayment.Class;
using JabjaiEntity.DB;
using MasterEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace FingerprintPayment.PreRegister.CsCode
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

        public static string LoadRegisterJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string year, string regStatus, string optLevel, string couType, string couTime, string branch, string stdName, string plan)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataRegister> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(year)) { sqlCondition += string.Format(@" AND r.registerYear = {0}", int.Parse(year) - 543); }
                if (!string.IsNullOrEmpty(regStatus) && regStatus != "3")
                {
                    sqlCondition += string.Format(@" AND r.paymentStatus = {0}", regStatus);
                }
                if (!string.IsNullOrEmpty(regStatus) && regStatus == "3")
                {
                    sqlCondition += string.Format(@" AND r.nTermSubLevel2 IS NOT NULL");
                }
                if (!string.IsNullOrEmpty(optLevel)) { sqlCondition += string.Format(@" AND r.optionLevel = {0}", optLevel); }
                if (!string.IsNullOrEmpty(couType)) { sqlCondition += string.Format(@" AND r.optionCourse = {0}", couType); }
                if (!string.IsNullOrEmpty(couTime)) { sqlCondition += string.Format(@" AND r.optionTime = {0}", couTime); }
                if (!string.IsNullOrEmpty(branch)) { sqlCondition += string.Format(@" AND bran.BranchSpecName LIKE N'%{0}%'", branch); }
                if (!string.IsNullOrEmpty(stdName)) { sqlCondition += string.Format(@" AND (r.sName LIKE N'%{0}%' OR r.sLastname LIKE N'%{0}%' OR r.sName+' '+r.sLastname = N'{0}' OR r.sStudentID LIKE '%{0}%')", stdName); }
                if (!string.IsNullOrEmpty(plan)) { sqlCondition += string.Format(@" AND r.RegisterPlanSetupID = {0}", plan); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TPreRegister r 
LEFT JOIN TBranchSpec bran ON r.optionBranch = bran.BranchSpecId AND r.SchoolID = bran.SchoolID
WHERE r.cDel IS NULL AND r.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT r.preRegisterId 'rID', ISNULL(title.titleDescription, '') +''+ ISNULL(r.sName, '') +' '+ ISNULL(r.sLastname, '') 'StudentName', r.addDate 'RegisterDate', r.sStudentID 'StudentCode', r.optionLevel 'LevelID', level2.SubLevel 'LevelName'
        , r.nTermSubLevel2 'RoomID', [level].SubLevel +' / '+ room.nTSubLevel2 'Room'
        , r.paymentStatus 'StatusID'
		, CASE r.paymentStatus   
		    WHEN 0 THEN N'ยังไม่ชำระค่าสมัคร'  
			WHEN 1 THEN N'ชำระค่าสมัครไม่ครบ'  
			WHEN 2 THEN N'ชำระค่าสมัครแล้ว'  
			ELSE ''
		  END 'Status'
		, CASE WHEN r.nTermSubLevel2 IS NOT NULL THEN N'ย้ายเข้า ' + [level].SubLevel +' / '+ room.nTSubLevel2 + (CASE WHEN u.sID IS NOT NULL AND u.cDel = '1' THEN N' [ลบ]' ELSE '' END) ELSE '' END 'MoveStatus'
        , bran.BranchSpecName 'Branch'
		, rer.ExamRoomName 'ExamRoom'
		, r.ExamSeatNo 'ExamSeatNo'
        , CASE r.ExamResults
			WHEN 1 THEN N'ผ่าน'
			WHEN 0 THEN N'ไม่ผ่าน'
			WHEN 2 THEN N'สำรอง'
			ELSE N'ไม่ระบุ'
		  END 'ExamResults'
        , e1.sName+' '+e1.sLastname 'ExamResultsUpdateBy'
		, r.ExamResultsUpdateDate
        , IIF(r.RegisterPlanSetupID=0, '<span class=""fw-bold"">ทั้งหมด</span>', rps.PlanName) 'PlanName'
        , CASE r.CompleteDocuments
			WHEN 1 THEN N'ครบ'
			WHEN 0 THEN N'ไม่ครบ'
			ELSE N'ไม่ระบุ'
		  END 'CompleteDocuments'
		, e2.sName+' '+e2.sLastname 'CompleteDocumentsUpdateBy'
		, r.CompleteDocumentsUpdateDate
        , r.CompleteDocumentsInfo
        FROM TPreRegister r
        LEFT JOIN TTermSubLevel2 room ON r.nTermSubLevel2 = room.nTermSubLevel2 AND r.SchoolID = room.SchoolID
        LEFT JOIN TSubLevel [level] ON room.nTSubLevel = [level].nTSubLevel AND room.SchoolID = [level].SchoolID
        LEFT JOIN TSubLevel level2 ON r.optionLevel = level2.nTSubLevel AND r.SchoolID = level2.SchoolID
        LEFT JOIN TBranchSpec bran ON r.optionBranch = bran.BranchSpecId AND r.SchoolID = bran.SchoolID
        LEFT JOIN TTitleList title ON r.StudentTitle = title.nTitleid AND r.SchoolID = title.SchoolID
        LEFT JOIN TUser u ON r.saveAsSID = u.sID AND r.SchoolID = u.SchoolID
        LEFT JOIN TRegisterExamRoom rer ON r.SchoolID = rer.SchoolID AND r.RegisterExamRoomID = rer.RegisterExamRoomID AND rer.IsDel=0
        LEFT JOIN TRegisterPlanSetup rps ON r.optionLevel = rps.nTSubLevel AND r.RegisterPlanSetupID = rps.RegisterPlanSetupID AND r.SchoolID = rps.SchoolID AND rps.cDel=0
        LEFT JOIN TEmployees e1 ON r.ExamResultsUpdateBy = e1.sEmp
		LEFT JOIN TEmployees e2 ON r.CompleteDocumentsUpdateBy = e2.sEmp
        WHERE r.cDel IS NULL AND r.SchoolID = {4} {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound, schoolID);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityRegisterList> resultFilter = en.Database.SqlQuery<EntityRegisterList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                string code = string.Format(@"{0}{1}", RegisterGateway.GetLettersShuffle(), schoolID.ToString().PadLeft(4, '0'));
                var schid = ComFunction.Rot13Transform(code);

                listData = new List<ListDataRegister>();
                ListDataRegister item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataRegister();
                    item.check = "";
                    item.no = "";
                    item.StudentName = i.StudentName;
                    item.RegisterDate = (i.RegisterDate == null ? "" : i.RegisterDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.StudentCode = i.StudentCode;
                    item.LevelName = i.LevelName;
                    item.Status = i.Status;
                    item.MoveStatus = i.MoveStatus;
                    item.ExamRoom = i.ExamRoom;
                    item.ExamSeatNo = i.ExamSeatNo;
                    item.ExamResults = i.ExamResults;
                    item.ExamResultsUpdateBy = i.ExamResultsUpdateBy;
                    item.ExamResultsUpdateDate = (i.ExamResultsUpdateDate == null ? "" : i.ExamResultsUpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.PlanName = i.PlanName;
                    item.CompleteDocuments = i.CompleteDocuments;
                    item.CompleteDocumentsUpdateBy = i.CompleteDocumentsUpdateBy;
                    item.CompleteDocumentsUpdateDate = (i.CompleteDocumentsUpdateDate == null ? "" : i.CompleteDocumentsUpdateDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.CompleteDocumentsInfo = HttpUtility.HtmlAttributeEncode(i.CompleteDocumentsInfo);
                    item.action = "";
                    item.rid = i.rID.ToString();
                    item.schid = schid;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataRegister> data = new CollectionData<ListDataRegister>();
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

        public static List<EntityReportPreRegister> LoadReportPreRegisterListData(string sortIndex, string orderDir, int schoolID, string searchYear, string searchRegStatus, string searchOptLevel, string searchCouType, string searchCouTime, string searchBranch, string searchStdName, string searchPlan)
        {
            List<EntityReportPreRegister> resultFilter;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition += string.Format(@" AND r.registerYear = {0}", int.Parse(searchYear) - 543); }
                if (!string.IsNullOrEmpty(searchRegStatus) && searchRegStatus != "3")
                {
                    sqlCondition += string.Format(@" AND r.paymentStatus = {0}", searchRegStatus);
                }
                if (!string.IsNullOrEmpty(searchRegStatus) && searchRegStatus == "3")
                {
                    sqlCondition += string.Format(@" AND r.nTermSubLevel2 IS NOT NULL");
                }
                if (!string.IsNullOrEmpty(searchOptLevel)) { sqlCondition += string.Format(@" AND r.optionLevel = {0}", searchOptLevel); }
                if (!string.IsNullOrEmpty(searchCouType)) { sqlCondition += string.Format(@" AND r.optionCourse = {0}", searchCouType); }
                if (!string.IsNullOrEmpty(searchCouTime)) { sqlCondition += string.Format(@" AND r.optionTime = {0}", searchCouTime); }
                if (!string.IsNullOrEmpty(searchBranch)) { sqlCondition += string.Format(@" AND bran.BranchSpecName LIKE N'%{0}%'", searchBranch); }
                if (!string.IsNullOrEmpty(searchStdName)) { sqlCondition += string.Format(@" AND (r.sName LIKE N'%{0}%' OR r.sLastname LIKE N'%{0}%' OR r.sName+' '+r.sLastname = N'{0}' OR r.sStudentID LIKE N'%{0}%' OR r.sStudentIdCardNumber = N'{0}')", searchStdName); }
                if (!string.IsNullOrEmpty(searchPlan)) { sqlCondition += string.Format(@" AND r.RegisterPlanSetupID = {0}", searchPlan); }

                string sortBy = "preRegisterId";
                switch (sortIndex)
                {
                    case "2": sortBy = "StudentName"; break;
                    case "3": sortBy = "AddDate"; break;
                    case "4": sortBy = "StudentID"; break;
                    case "5": sortBy = "SubLevel"; break;
                    case "6": sortBy = "PlanName"; break;
                    //case "7": sortBy = "ExamRoom"; break;//
                    //case "8": sortBy = "ExamSeatNo"; break;//
                    case "9": sortBy = "PaymentStatus"; break;
                    case "10": sortBy = "ExamResults"; break;
                    case "11": sortBy = "CompleteDocuments"; break;
                }
                sortBy = string.Format("{0} {1}", sortBy, orderDir.ToUpper());

                string sqlQueryFilter = string.Format(@"
SELECT r.preRegisterId, ISNULL(r.CreatedDate, r.addDate) 'RegisterDate', r.registerYear + 543 'Year', (CASE r.StudentCategory WHEN '1' THEN N'ไป - กลับ' WHEN '2' THEN N'ประจำ' ELSE '' END) 'StudentCategory', bran.BranchSpecName 'BranchName', (CASE r.optionTime WHEN 1 THEN N'รอบเช้า' WHEN 2 THEN N'บ่าย 1' WHEN 3 THEN N'บ่าย 2' ELSE '' END) 'Time', (CASE r.optionCourse WHEN 1 THEN N'ปกติ' WHEN 3 THEN N'ทวิภาคี' ELSE '' END) 'Course', IIF(r.RegisterPlanSetupID=0, 'ทั้งหมด', [plan].PlanName) 'PlanName'
, sl.SubLevel 'SubLevel', r.sStudentID 'StudentID', r.examCode 'ExamCode', (CASE r.ExamResults WHEN 1 THEN N'ผ่าน' WHEN 0 THEN N'ไม่ผ่าน' WHEN 2 THEN N'สำรอง' ELSE N'ไม่ระบุ' END) 'ExamResults', CASE r.CompleteDocuments WHEN 1 THEN N'ครบ' WHEN 0 THEN N'ไม่ครบ' ELSE N'ไม่ระบุ' END 'CompleteDocuments', r.CompleteDocumentsInfo, (CASE r.cSex WHEN 0 THEN N'ชาย' WHEN 1 THEN N'หญิง' ELSE '' END) 'Sex'
, ISNULL(tt1.titleDescription, '') 'StudentTitle', ISNULL(r.sName, '') 'StudentFirstName', ISNULL(r.sLastname, '') 'StudentLastName', ISNULL(tt1.titleDescription, '')+' '+ISNULL(r.sName, '')+' '+ISNULL(r.sLastname, '') 'StudentName'
, ISNULL(r.sStudentNameEN, '') 'StudentFirstNameEn', ISNULL(r.sStudentLastEN, '') 'StudentLastNameEn', ISNULL(r.sStudentNameEN, '')+' '+ISNULL(r.sStudentLastEN, '') 'StudentNameEn'
, r.sNickName 'NickName', r.sNickNameEN 'NickNameEn', r.sStudentIdCardNumber 'StudentIdentityCard', r.dBirth 'StudentBirth', md9.MasterDes 'StudentRace', md3.MasterDes 'StudentNation', md6.MasterDes 'StudentReligion'
, r.nSonNumber 'SonNumber', r.sStudentHomeNumber 'StudentHomeNumber', r.sStudentSoy 'StudentSoy', r.sStudentMuu 'StudentMuu', r.sStudentRoad 'StudentRoad', r.sStudentProvince 'StudentProvince', r.sStudentAumpher 'StudentAumpher'
, r.sStudentTumbon 'StudentTumbon', r.sStudentPost 'StudentPost', r.sPhone 'Phone', r.sEmail 'Email'
, r.houseRegistrationNumber 'HouseRegistrationHomeNumber', r.houseRegistrationSoy 'HouseRegistrationSoy', r.houseRegistrationMuu 'HouseRegistrationMuu', r.houseRegistrationRoad 'HouseRegistrationRoad', CAST(r.houseRegistrationProvince AS VARCHAR(10)) 'HouseRegistrationProvince', CAST(r.houseRegistrationAumpher AS VARCHAR(10)) 'HouseRegistrationAumpher', CAST(r.houseRegistrationTumbon AS VARCHAR(10)) 'HouseRegistrationTumbon', r.houseRegistrationPost 'HouseRegistrationPost', r.houseRegistrationPhone 'HouseRegistrationPhone'
, r.nWeight 'Weight', r.nHeight 'Height', r.sBlood 'Blood', r.sSickFood 'SickFood', r.sSickDrug 'SickDrug', r.sSickOther 'SickOther'
, r.sSickNormal 'SickNormal', r.sSickDanger 'SickDanger', r.addDate 'AddDate', r.moveInDate 'MoveInDate', r.saveAsSID 'SaveAsSID', cr.ClassRoom
, (CASE r.paymentStatus WHEN 0 THEN 'ยังไม่ชำระค่าสมัคร' WHEN 1 THEN 'ชำระค่าสมัครไม่ครบ' WHEN 2 THEN 'ชำระค่าสมัครแล้ว' ELSE '' END) 'PaymentStatus', r.oldSchoolName 'OldSchoolName', r.oldSchoolProvince 'OldSchoolProvince', r.oldSchoolAumpher 'OldSchoolAumpher', r.oldSchoolTumbon 'OldSchoolTumbon', r.oldSchoolGPA 'OldSchoolGPA'
, (CASE r.oldSchoolGraduated WHEN '1' THEN 'ประถมศึกษาปีที่ 1' WHEN '2' THEN 'ประถมศึกษาปีที่ 2' WHEN '3' THEN 'ประถมศึกษาปีที่ 3' WHEN '4' THEN 'ประถมศึกษาปีที่ 4' WHEN '5' THEN 'ประถมศึกษาปีที่ 5' WHEN '6' THEN 'ประถมศึกษาปีที่ 6' WHEN '7' THEN 'มัธยมศึกษาตอนต้น' WHEN '8' THEN 'มัธยมศึกษาตอนปลาย' WHEN '9' THEN 'ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 1' WHEN '10' THEN 'ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 1' WHEN '11' THEN 'เตรียมอนุบาลศึกษา' WHEN '12' THEN 'อนุบาลศึกษา 1' WHEN '13' THEN 'อนุบาลศึกษา 2' WHEN '14' THEN 'อนุบาลศึกษา 3' WHEN '15' THEN 'ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 2' WHEN '16' THEN 'ประกาศนียบัตรวิชาชีพ ชั้นปีที่ 3' WHEN '17' THEN 'ประกาศนียบัตรวิชาชีพขั้นสูง ชั้นปีที่ 2' ELSE '' END) 'OldSchoolGraduated'
, ISNULL(tt2.titleDescription, '') 'FamilyTitle', ISNULL(r.sFamilyName, '') 'FamilyFirstName', ISNULL(r.sFamilyLast, '') 'FamilyLastName', ISNULL(tt2.titleDescription, '')+' '+ISNULL(r.sFamilyName, '')+' '+ISNULL(r.sFamilyLast, '') 'FamilyName'
, r.sFamilyIdCardNumber 'FamilyIdentityCard', md9fml.MasterDes 'FamilyRace', md3fml.MasterDes 'FamilyNation', md6fml.MasterDes 'FamilyReligion', r.sFamilyHomeNumber 'FamilyHomeNumber', r.sFamilySoy 'FamilySoy', r.sFamilyMuu 'FamilyMuu', r.sFamilyRoad 'FamilyRoad', r.sFamilyProvince 'FamilyProvince', r.sFamilyAumpher 'FamilyAumpher', r.sFamilyTumbon 'FamilyTumbon', r.sFamilyPost 'FamilyPost', r.sPhoneOne 'PhoneOne', r.sPhoneTwo 'PhoneTwo', r.sPhoneThree 'PhoneThree', r.ParentEmail 'ParentEmail', CONVERT(float, r.nFamilyIncome) 'FamilyIncome'
, (CASE r.sFamilyGraduated WHEN 1 THEN 'ต่ำกว่าประถม' WHEN 2 THEN 'ประถมศึกษา' WHEN 3 THEN 'มัธยมศึกษาหรือเทียบเท่า' WHEN 4 THEN 'ปริญญาตรี' WHEN 5 THEN 'ปริญญาโท' WHEN 6 THEN 'ปริญญาเอก' WHEN 7 THEN 'มัธยมต้น' WHEN 8 THEN 'อนุปริญญา' WHEN 9 THEN 'ประกาศนียบัตรวิชาชีพ' WHEN 10 THEN 'ประกาศนียบัตรวิชาชีพชั้นสูง' ELSE '' END) 'FamilyGraduated'
, r.sFamilyWorkPlace 'FamilyWorkPlace', r.dFamilyBirthDay 'FamilyBirthDay', CAST(DATEDIFF(DD, r.dFamilyBirthDay, GETDATE()) / 365.25 AS INT) 'FamilyAge', r.sFamilyJob 'FamilyJob', r.sFamilyRelate 'FamilyRelation'
, ISNULL(tt3.titleDescription, '') 'FatherTitle', ISNULL(r.sFatherFirstName, '') 'FatherFirstName', ISNULL(r.sFatherLastName, '') 'FatherLastName', ISNULL(tt3.titleDescription, '')+' '+ISNULL(r.sFatherFirstName, '')+' '+ISNULL(r.sFatherLastName, '') 'FatherName'
, r.sFatherIdCardNumber 'FatherIdentityCard', md9f.MasterDes 'FatherRace', md3f.MasterDes 'FatherNation', md6f.MasterDes 'FatherReligion', r.sFatherHomeNumber 'FatherHomeNumber', r.sFatherSoy 'FatherSoy', r.sFatherMuu 'FatherMuu', r.sFatherRoad 'FatherRoad', r.sFatherProvince 'FatherProvince', r.sFatherAumpher 'FatherAumpher', r.sFatherTumbon 'FatherTumbon', r.sFatherPost 'FatherPost', r.sFatherPhone 'FatherPhone', r.FatherEmail 'FatherEmail', CONVERT(float, ISNULL(TRY_PARSE(r.fatherIncome AS float), r.nFatherIncome)) 'FatherIncome'
, (CASE r.sFatherGraduated WHEN 1 THEN 'ต่ำกว่าประถม' WHEN 2 THEN 'ประถมศึกษา' WHEN 3 THEN 'มัธยมศึกษาหรือเทียบเท่า' WHEN 4 THEN 'ปริญญาตรี' WHEN 5 THEN 'ปริญญาโท' WHEN 6 THEN 'ปริญญาเอก' WHEN 7 THEN 'มัธยมต้น' WHEN 8 THEN 'อนุปริญญา' WHEN 9 THEN 'ประกาศนียบัตรวิชาชีพ' WHEN 10 THEN 'ประกาศนียบัตรวิชาชีพชั้นสูง' ELSE '' END) 'FatherGraduated'
, r.sFatherWorkPlace 'FatherWorkPlace', r.dFatherBirthDay 'FatherBirthDay', CAST(DATEDIFF(DD, r.dFatherBirthDay, GETDATE()) / 365.25 AS INT) 'FatherAge', r.sFatherJob 'FatherJob'
, ISNULL(tt4.titleDescription, '') 'MotherTitle', ISNULL(r.sMotherFirstName, '') 'MotherFirstName', ISNULL(r.sMotherLastName, '') 'MotherLastName', ISNULL(tt4.titleDescription, '')+' '+ISNULL(r.sMotherFirstName, '')+' '+ISNULL(r.sMotherLastName, '') 'MotherName'
, r.sMotherIdCardNumber 'MotherIdentityCard', md9m.MasterDes 'MotherRace', md3m.MasterDes 'MotherNation', md6m.MasterDes 'MotherReligion', r.sMotherHomeNumber 'MotherHomeNumber', r.sMotherSoy 'MotherSoy', r.sMotherMuu 'MotherMuu', r.sMotherRoad 'MotherRoad', r.sMotherProvince 'MotherProvince', r.sMotherAumpher 'MotherAumpher', r.sMotherTumbon 'MotherTumbon', r.sMotherPost 'MotherPost', r.sMotherPhone 'MotherPhone', r.MotherEmail 'MotherEmail', CONVERT(float, ISNULL(TRY_PARSE(r.motherIncome AS float), r.nMotherIncome)) 'MotherIncome'
, (CASE r.sMotherGraduated WHEN 1 THEN 'ต่ำกว่าประถม' WHEN 2 THEN 'ประถมศึกษา' WHEN 3 THEN 'มัธยมศึกษาหรือเทียบเท่า' WHEN 4 THEN 'ปริญญาตรี' WHEN 5 THEN 'ปริญญาโท' WHEN 6 THEN 'ปริญญาเอก' WHEN 7 THEN 'มัธยมต้น' WHEN 8 THEN 'อนุปริญญา' WHEN 9 THEN 'ประกาศนียบัตรวิชาชีพ' WHEN 10 THEN 'ประกาศนียบัตรวิชาชีพชั้นสูง' ELSE '' END) 'MotherGraduated'
, r.sMotherWorkPlace 'MotherWorkPlace', r.dMotherBirthDay 'MotherBirthDay', CAST(DATEDIFF(DD, r.dMotherBirthDay, GETDATE()) / 365.25 AS INT) 'MotherAge', r.sMotherJob 'MotherJob'
, (CASE r.knowFrom1 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom1'
, (CASE r.knowFrom2 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom2'
, (CASE r.knowFrom3 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom3'
, (CASE r.knowFrom4 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom4'
, (CASE r.knowFrom5 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom5'
, (CASE r.knowFrom6 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom6'
, (CASE r.knowFrom7 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom7'
, (CASE r.knowFrom8 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom8'
, (CASE r.knowFrom9 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom9'
, (CASE r.knowFrom10 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom10'
, (CASE r.knowFrom11 WHEN 1 THEN 'ใช่' ELSE '' END) 'KnowFrom11'
--, rs.OrderPlans, rs.BackupPlans
, rs.OrderPlans, r.BackupPlans
FROM TPreRegister r
LEFT JOIN TBranchSpec bran ON r.optionBranch = bran.BranchSpecId AND r.SchoolID = bran.SchoolID
LEFT JOIN TRegisterPlanSetup [plan] ON r.RegisterPlanSetupID = [plan].RegisterPlanSetupID AND r.optionLevel = [plan].nTSubLevel AND r.SchoolID = [plan].SchoolID
LEFT JOIN TSubLevel sl ON r.optionLevel = sl.nTSubLevel AND r.SchoolID = sl.SchoolID
LEFT JOIN TTitleList tt1 ON r.StudentTitle = tt1.nTitleid AND r.SchoolID = tt1.SchoolID
LEFT JOIN TUser u ON r.saveAsSID = u.sID AND r.SchoolID = u.SchoolID
LEFT JOIN (SELECT tsl.nTermSubLevel2, sl.SubLevel+' / '+tsl.nTSubLevel2 'ClassRoom' FROM TTermSubLevel2 tsl LEFT JOIN TSubLevel sl ON tsl.nTSubLevel = sl.nTSubLevel AND tsl.SchoolID = sl.SchoolID) cr ON r.nTermSubLevel2 = cr.nTermSubLevel2
LEFT JOIN TTitleList tt2 ON r.nFamilyTitle = tt2.nTitleid AND r.SchoolID = tt2.SchoolID
LEFT JOIN TTitleList tt3 ON r.FatherTitle = tt3.nTitleid AND r.SchoolID = tt3.SchoolID
LEFT JOIN TTitleList tt4 ON r.MotherTitle = tt4.nTitleid AND r.SchoolID = tt4.SchoolID
LEFT JOIN TMasterData md9 ON md9.MasterType = '9' AND md9.MasterCode = r.sStudentRace
LEFT JOIN TMasterData md3 ON md3.MasterType = '3' AND md3.MasterCode = r.sStudentNation
LEFT JOIN TMasterData md6 ON md6.MasterType = '6' AND md6.MasterCode = r.sStudentReligion
LEFT JOIN TMasterData md9f ON md9f.MasterType = '9' AND md9f.MasterCode = r.sFatherRace
LEFT JOIN TMasterData md3f ON md3f.MasterType = '3' AND md3f.MasterCode = r.sFatherNation
LEFT JOIN TMasterData md6f ON md6f.MasterType = '6' AND md6f.MasterCode = r.sFatherReligion
LEFT JOIN TMasterData md9m ON md9m.MasterType = '9' AND md9m.MasterCode = r.sMotherRace
LEFT JOIN TMasterData md3m ON md3m.MasterType = '3' AND md3m.MasterCode = r.sMotherNation
LEFT JOIN TMasterData md6m ON md6m.MasterType = '6' AND md6m.MasterCode = r.sMotherReligion
LEFT JOIN TMasterData md9fml ON md9fml.MasterType = '9' AND md9fml.MasterCode = r.sFamilyRace
LEFT JOIN TMasterData md3fml ON md3fml.MasterType = '3' AND md3fml.MasterCode = r.sFamilyNation
LEFT JOIN TMasterData md6fml ON md6fml.MasterType = '6' AND md6fml.MasterCode = r.sFamilyReligion
--LEFT JOIN TRegisterSetup rs ON r.SchoolID = rs.SchoolID AND (r.registerYear+543) = rs.Year AND r.StudentType = rs.StudentType AND r.optionLevel = rs.nTSubLevel AND r.RegisterPlanSetupID = rs.RegisterPlanSetupID AND rs.IsActiveBackupPlan = 1 AND rs.cDel = 0
LEFT JOIN 
(
	SELECT r.SchoolID, r.preRegisterId, COUNT(*) 'OrderPlans'
	--, JSON_VALUE(j.VALUE, '$.planId') 'PlanID', JSON_VALUE(j.VALUE, '$.planName') 'PlanName'
	FROM TPreRegister r CROSS APPLY OPENJSON(BackupPlans) j 
	WHERE r.SchoolID = {0} AND r.BackupPlans IS NOT NULL AND ISNULL(JSON_VALUE(j.VALUE, '$.planName'), '') <> ''
	GROUP BY r.SchoolID, r.preRegisterId
) rs ON r.SchoolID = rs.SchoolID AND r.preRegisterId = rs.preRegisterId
WHERE r.cDel IS NULL AND r.SchoolID = {0} {1}
ORDER BY {2}", schoolID, sqlCondition, sortBy);

                resultFilter = en.Database.SqlQuery<EntityReportPreRegister>(sqlQueryFilter).ToList();
            }

            return resultFilter;
        }

        public static List<EntityReportPreRegisterStudentAmount> LoadReportPreRegisterStudentAmount(int schoolID, string searchYear, string searchRegStatus, string searchOptLevel, string searchCouType, string searchCouTime, string searchBranch, string searchStdName, string searchPlan)
        {
            List<EntityReportPreRegisterStudentAmount> resultFilter;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                string sqlCondition = "";
                if (!string.IsNullOrEmpty(searchYear)) { sqlCondition += string.Format(@" AND r.registerYear = {0}", int.Parse(searchYear) - 543); }
                if (!string.IsNullOrEmpty(searchRegStatus) && searchRegStatus != "3")
                {
                    sqlCondition += string.Format(@" AND r.paymentStatus = {0}", searchRegStatus);
                }
                if (!string.IsNullOrEmpty(searchRegStatus) && searchRegStatus == "3")
                {
                    sqlCondition += string.Format(@" AND r.nTermSubLevel2 IS NOT NULL");
                }
                if (!string.IsNullOrEmpty(searchOptLevel)) { sqlCondition += string.Format(@" AND r.optionLevel = {0}", searchOptLevel); }
                if (!string.IsNullOrEmpty(searchCouType)) { sqlCondition += string.Format(@" AND r.optionCourse = {0}", searchCouType); }
                if (!string.IsNullOrEmpty(searchCouTime)) { sqlCondition += string.Format(@" AND r.optionTime = {0}", searchCouTime); }
                if (!string.IsNullOrEmpty(searchBranch)) { sqlCondition += string.Format(@" AND bran.BranchSpecName LIKE N'%{0}%'", searchBranch); }
                if (!string.IsNullOrEmpty(searchStdName)) { sqlCondition += string.Format(@" AND (r.sName LIKE N'%{0}%' OR r.sLastname LIKE N'%{0}%')", searchStdName); }
                if (!string.IsNullOrEmpty(searchPlan)) { sqlCondition += string.Format(@" AND r.RegisterPlanSetupID = {0}", searchPlan); }

                string sqlQueryFilter = string.Format(@"
SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, r.addDate)) 'RegisterDate', r.optionLevel 'LevelID', level.SubLevel 'LevelName', rps.PlanName
, SUM(CASE WHEN r.cSex = '0' THEN 1 ELSE 0 END) 'SumMale'
, SUM(CASE WHEN r.cSex = '1' THEN 1 ELSE 0 END) 'SumFemale'
, COUNT(*) 'CountAll'
FROM TPreRegister r
LEFT JOIN TSubLevel level ON r.optionLevel = level.nTSubLevel AND r.SchoolID = level.SchoolID
LEFT JOIN TBranchSpec bran ON r.optionBranch = bran.BranchSpecId AND r.SchoolID = bran.SchoolID
LEFT JOIN TRegisterPlanSetup rps ON r.optionLevel = rps.nTSubLevel AND r.RegisterPlanSetupID = rps.RegisterPlanSetupID AND r.SchoolID = rps.SchoolID AND rps.cDel=0
WHERE r.cDel IS NULL AND r.SchoolID = {0} {1}
GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, r.addDate)), r.optionLevel, level.SubLevel, rps.PlanName", schoolID, sqlCondition);

                resultFilter = en.Database.SqlQuery<EntityReportPreRegisterStudentAmount>(sqlQueryFilter).ToList();
            }

            return resultFilter;
        }

        public static string LoadRegisterPlanJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataRegisterPlan> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TRegisterPlanSetup r
WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT r.RegisterPlanSetupID 'rID', r.nTSubLevel 'LevelID', s.SubLevel 'LevelName', r.PlanName, r.PlanCode
        , (CASE WHEN COUNT(rs.PlanID) > 0 THEN 'no' ELSE 'yes' END) 'CanRemove'
        FROM TRegisterPlanSetup r 
        LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel AND r.SchoolID = s.SchoolID
        LEFT JOIN TRegisterSetup rs ON r.RegisterPlanSetupID = rs.RegisterPlanSetupID AND r.nTSubLevel = rs.nTSubLevel AND r.SchoolID = rs.SchoolID
        WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {4} {1}
        GROUP BY r.RegisterPlanSetupID, r.nTSubLevel, s.SubLevel, r.PlanName, r.PlanCode
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound, schoolID);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityRegisterPlanList> resultFilter = en.Database.SqlQuery<EntityRegisterPlanList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataRegisterPlan>();
                ListDataRegisterPlan item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataRegisterPlan();
                    item.no = "";
                    item.LevelName = i.LevelName;
                    item.PlanName = i.PlanName;
                    item.PlanCode = i.PlanCode;
                    item.action = "";
                    item.rid = i.rID.ToString();
                    item.lid = i.LevelID.ToString();
                    item.CanRemove = i.CanRemove;
                    listData.Add(item);
                }
            }

            CollectionData<ListDataRegisterPlan> data = new CollectionData<ListDataRegisterPlan>();
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

        public static string LoadRegisterRecruitmentJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataRegisterRecruitment> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TRegisterSetup r
WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT r.RegisterSetupID 'ID', r.nYear 'YearID', r.Year, r.StudentType 'StudentTypeID', (CASE r.StudentType  WHEN '1' THEN N'นักเรียนใหม่' WHEN '2' THEN N'นักเรียนรักษาสิทธิ์' END) 'StudentTypeName'
        , r.nTSubLevel 'LevelID', s.SubLevel 'LevelName', r.RegisterPlanSetupID 'PlanID', IIF(r.RegisterPlanSetupID=0, '<span class=""fw-bold"">ทั้งหมด</span>', p.PlanName) 'PlanName', r.EndDate, s.MasterCode
        FROM TRegisterSetup r 
        LEFT JOIN TSubLevel s ON r.nTSubLevel = s.nTSubLevel AND r.SchoolID = s.SchoolID
        LEFT JOIN TRegisterPlanSetup p ON r.nTSubLevel = p.nTSubLevel AND r.RegisterPlanSetupID = p.RegisterPlanSetupID AND r.SchoolID = p.SchoolID
        WHERE 1 = 1 AND r.cDel = 0 AND r.SchoolID = {4} {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound, schoolID);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityRegisterRecruitmentList> resultFilter = en.Database.SqlQuery<EntityRegisterRecruitmentList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataRegisterRecruitment>();
                ListDataRegisterRecruitment item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataRegisterRecruitment();
                    item.no = "";
                    item.Year = i.Year.ToString();
                    item.StudentType = i.StudentTypeName;
                    item.LevelName = i.LevelName;
                    item.PlanName = i.PlanName;
                    item.EndDate = (i.EndDate == null ? "" : i.EndDate.Value.ToString("dd/MM/yyyy", new CultureInfo("th-TH")));
                    item.action = "";
                    item.id = i.ID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataRegisterRecruitment> data = new CollectionData<ListDataRegisterRecruitment>();
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

        public static string LoadRegisterExamRoomJsonData(int draw, int pageIndex, int pageSize, string sortBy, int schoolID, string plan, string examRoomName)
        {
            int totalData = 0;
            int filterData = 0;

            List<ListDataRegisterExamRoom> listData;

            using (JabJaiEntities en = new JabJaiEntities(Connection.StringConnectionSchool(schoolID,ConnectionDB.Read)))
            {
                int lowerBound = (pageIndex * pageSize) + 1;
                int upperBound = lowerBound + pageSize;

                string sqlCondition = "";
                if (!string.IsNullOrEmpty(plan)) { sqlCondition += string.Format(@" AND rps.RegisterPlanSetupID = {0}", plan); }
                if (!string.IsNullOrEmpty(examRoomName)) { sqlCondition += string.Format(@" AND rer.ExamRoomName LIKE N'%{0}%'", examRoomName); }

                string sqlQueryCount = string.Format(@"
SELECT COUNT(*) 
FROM TRegisterExamRoom rer 
LEFT JOIN TRegisterPlanSetup rps ON rer.SchoolID = rps.SchoolID AND rer.RegisterPlanSetupID = rps.RegisterPlanSetupID AND rer.nTSubLevel = rps.nTSubLevel
WHERE rer.IsDel = 0 AND rer.SchoolID = {0} {1}", schoolID, sqlCondition);

                string sqlQueryFilter = string.Format(@"
SELECT A.*
FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY {0}) AS RowNumber, T.*
    FROM (
        SELECT rer.RegisterExamRoomID, rps.PlanName, rer.ExamRoomName, rer.Seats
        FROM TRegisterExamRoom rer 
        LEFT JOIN TRegisterPlanSetup rps ON rer.SchoolID = rps.SchoolID AND rer.RegisterPlanSetupID = rps.RegisterPlanSetupID AND rer.nTSubLevel = rps.nTSubLevel
        WHERE rer.IsDel = 0 AND rer.SchoolID = {4} {1}
    ) AS T
) AS A
WHERE RowNumber >= {2} AND RowNumber < {3}", sortBy, sqlCondition, lowerBound, upperBound, schoolID);

                int resultCount = en.Database.SqlQuery<int>(sqlQueryCount).FirstOrDefault();
                List<EntityRegisterExamRoomList> resultFilter = en.Database.SqlQuery<EntityRegisterExamRoomList>(sqlQueryFilter).ToList();

                totalData = resultCount;
                filterData = resultFilter.Count();

                listData = new List<ListDataRegisterExamRoom>();
                ListDataRegisterExamRoom item;
                foreach (var i in resultFilter)
                {
                    item = new ListDataRegisterExamRoom();
                    item.no = "";
                    item.PlanName = i.PlanName;
                    item.ExamRoomName = i.ExamRoomName;
                    item.Seats = i.Seats.ToString();
                    item.action = "";
                    item.erid = i.RegisterExamRoomID.ToString();
                    listData.Add(item);
                }
            }

            CollectionData<ListDataRegisterExamRoom> data = new CollectionData<ListDataRegisterExamRoom>();
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

    }
}